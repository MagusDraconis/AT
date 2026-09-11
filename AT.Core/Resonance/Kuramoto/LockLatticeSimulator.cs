using AT.Core.ResearchT;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Deterministic lock-lattice simulator for the canonical C96(±1..±6) ring — the numerical
/// precursor to the ResearchY-NP_170 hardware proposal (ResearchY-NP_171).
///
/// MODEL (stated before any number is produced).
/// A ring of N = 96 oscillator nodes, each with a fixed natural frequency taken from the
/// D96 Laplacian spectrum (so node i ↔ eigenmode i, and ω_i = √λ_i, normalised by ω_max),
/// coupled through the circulant lock lattice:
///
///     dθ_i/dτ = ω_i + g · (1/deg) · Σ_{j∈N(i)} sin(θ_j − θ_i)
///
/// with deg = 12, g ∈ [0,1] the degree-normalised lock strength (NP_170 §5.1) and NO
/// stochastic term whatsoever: the dynamics are a deterministic ODE. Randomness enters only
/// as (a) a fixed-seed LCG used for the initial phases and for the random-coupling control,
/// and (b) nothing else. Every seed is reported, so any run is reproducible exactly.
///
/// Integration: classical Runge-Kutta 4 with fixed step dt = 0.1 (≈ 63 steps per period of the
/// fastest mode, whose normalised frequency is 1). Local truncation error O(dt⁵); the
/// integrator is verified against dt/2 in the NP_171 test suite, not assumed.
///
/// The phase sum is evaluated in angle-addition form,
///     Σ_j w_j sin(θ_j − θ_i) = cosθ_i · Σ_j w_j sinθ_j − sinθ_i · Σ_j w_j cosθ_j,
/// which is algebraically exact and cuts the trigonometric work from 12 to 2 calls per node.
///
/// Lock criterion (NP_170 R3, with the T_014 refinement). The reference is the collective
/// phase Ψ(τ) = arg Σ_j e^{iθ_j(τ)} tracked continuously (unwrapped). A node is LOCKED at
/// coupling g when its mean slip over the measurement window satisfies
///     |d(θ_i − Ψ)/dτ| &lt; ε .
/// Two ε rules are reported, as the protocol demands: the primary NP_170 rule ε = 0.01·ω₁
/// (one common threshold, the lowest doublet's frequency) and the per-mode rule
/// ε_i = 0.01·ω_i (T_014), which is undefined for the zero mode and therefore covers the 95
/// positive modes only.
///
/// Nothing here is AT-derived physics: the Adler/Kuramoto nonlinearity is an IMPORTED input
/// (NP_005: the locking force is ABSENT from the canonical chain), and the ring's gap is an
/// imported kinematic finite-size gap (NP_169). The simulator predicts what the lock law must
/// look like *if* it is realisable; it cannot confirm the theory.
/// </summary>
public static class LockLatticeSimulator
{
    public const int N = 96;
    public const int StepMax = 6;

    /// <summary>g-sweep resolution (40 steps up, 40 down — the QG316 / NP_170 §5.1 ramp).</summary>
    public const int DefaultSweepSteps = 40;

    /// <summary>RK4 step. 63 steps per period of the fastest mode (ω_max = 1 normalised).</summary>
    public const double DefaultDt = 0.1;

    /// <summary>Steps spent relaxing at each g before measuring (40 time units at dt = 0.1).</summary>
    public const int DefaultSettleSteps = 400;

    /// <summary>Steps over which the slip rate is averaged (20 time units at dt = 0.1).</summary>
    public const int DefaultMeasureSteps = 200;

    /// <summary>ε = 0.01·ω₁ (NP_170 R3 primary rule, applied to all 96 nodes).</summary>
    public const double EpsilonFraction = 0.01;

    /// <summary>
    /// ω below this counts as the zero mode. The Laplacian's null eigenvalue comes back as
    /// ≈ 1e-16, and ω = √λ turns that into ≈ 1e-8, so the positivity test must live in ω-space.
    /// </summary>
    public const double ZeroTolerance = 1e-6;

    /// <summary>30 fixed seeds: seed_i = 12345 + 7919·i (LCG constants; deterministic everywhere).</summary>
    public static readonly uint[] DefaultSeeds =
        Enumerable.Range(1, 30).Select(i => (uint)(12345 + 7919 * i)).ToArray();

    // ── Deterministic RNG (identical stream in any runtime) ───────────────────

    private struct Lcg(uint seed)
    {
        private uint _x = seed;
        public uint Next() => _x = unchecked(1664525u * _x + 1013904223u);
        public double Unit() => Next() / 4294967296.0;
        public int Pick(int m) => m <= 0 ? 0 : (int)(Next() % (uint)m);
    }

    // ── Lattice ───────────────────────────────────────────────────────────────

    /// <summary>
    /// A coupling graph plus the per-node natural frequencies it carries.
    /// Omega is normalised so that max(Omega) = 1; Multiplicity is the size of the degenerate
    /// frequency level each node belongs to (the T_015 robustness class).
    /// </summary>
    public sealed record Lattice(
        string Name,
        string Note,
        int[][] Neighbors,
        double[][] Weights,
        double[] Omega,
        int[] Multiplicity,
        int[] ModeIndex)
    {
        /// <summary>ω₁ (normalised) — the fundamental, i.e. the near-gap doublet's frequency.</summary>
        public double Omega1 => Omega.Where(w => w > ZeroTolerance).Min();

        /// <summary>True for the 95 oscillating modes; false for the single zero mode.</summary>
        public bool IsPositive(int i) => Omega[i] > ZeroTolerance;
    }

    /// <summary>Build a lattice from an adjacency matrix and a frequency vector (both length N).</summary>
    public static Lattice From(double[,] adjacency, double[] omega, string name, string note)
    {
        var neighbors = new List<int>[N];
        var weights = new List<double>[N];
        for (int i = 0; i < N; i++)
        {
            neighbors[i] = [];
            weights[i] = [];
            for (int j = 0; j < N; j++)
                if (adjacency[i, j] != 0.0)
                {
                    neighbors[i].Add(j);
                    weights[i].Add(adjacency[i, j]);
                }
        }

        double max = omega.Max();
        var norm = omega.Select(w => max > 0 ? w / max : 0.0).ToArray();

        // Degenerate level sizes, ascending distinct values (same tolerance as the audit helpers).
        var order = Enumerable.Range(0, N).OrderBy(i => norm[i]).ToArray();
        var mult = new int[N];
        int s = 0;
        while (s < N)
        {
            int e = s;
            while (e < N && Math.Abs(norm[order[e]] - norm[order[s]]) <= 1e-9) e++;
            for (int t = s; t < e; t++) mult[order[t]] = e - s;
            s = e;
        }

        return new Lattice(name, note,
            neighbors.Select(l => l.ToArray()).ToArray(),
            weights.Select(l => l.ToArray()).ToArray(),
            norm, mult, order);
    }

    // ── Canonical D96 lattice and the four NP_170 §6 R6 controls ──────────────

    /// <summary>
    /// Canonical C96(±1..±6) ring. Node i carries eigenmode i, so ω_i = √λ_i with
    /// λ_k = 2Σ_{d=1..6}(1 − cos 2πdk/96) taken from the EXACT closed form (no eigensolver):
    /// the mirror pairing λ_k = λ_{96−k} then becomes a spatial reflection of the ring, the
    /// zero mode k = 0 is the reference node, and the multiplicity pattern is exact.
    /// </summary>
    public static Lattice D96()
    {
        var adj = GeneralInverseSpectrumAnalyzer.D96DerivedGraph(N, StepMax);
        return From(adj, ClosedFormOmega(), "D96",
            "canonical C96(±1..±6): node i carries eigenmode i (ω_i = √λ_i, exact closed form)");
    }

    /// <summary>
    /// The same frequency multiset in ascending order (a monotone frequency gradient around the
    /// ring) — the arrangement-sensitivity variant. Same spectrum, different spatial order.
    /// </summary>
    public static Lattice D96Ranked()
    {
        var adj = GeneralInverseSpectrumAnalyzer.D96DerivedGraph(N, StepMax);
        var omega = ClosedFormOmega().OrderBy(x => x).ToArray();
        return From(adj, omega, "D96-ranked",
            "same D96 multiset, arranged as a monotone frequency gradient around the ring");
    }

    /// <summary>Exact closed-form √λ_k for C96(1..6), k = 0..95 (mode index order).</summary>
    public static double[] ClosedFormOmega()
    {
        var lam = ClosedFormLambda();
        return lam.Select(l => Math.Sqrt(Math.Max(l, 0.0))).ToArray();
    }

    /// <summary>
    /// Exact circulant eigenvalues λ_k = 2Σ_{d=1..6}(1 − cos 2πdk/96) (mode index order).
    /// Taken from the closed form rather than an eigensolver, so mirror pairs are degenerate to
    /// machine precision and the multiplicity pattern is exact.
    /// </summary>
    public static double[] ClosedFormLambda()
    {
        var lam = new double[N];
        for (int k = 0; k < N; k++)
        {
            double sum = 0.0;
            for (int d = 1; d <= StepMax; d++) sum += 1.0 - Math.Cos(2.0 * Math.PI * d * k / N);
            lam[k] = 2.0 * sum;
        }
        return lam;
    }

    /// <summary>Control (a): the D96 ring with its frequencies shifted out of ratio.</summary>
    public static Lattice DetunedRing()
    {
        var adj = GeneralInverseSpectrumAnalyzer.D96DerivedGraph(N, StepMax);
        var w = ClosedFormOmega();
        var det = new double[N];
        for (int i = 0; i < N; i++) det[i] = w[i] * (1.0 + 0.5 * i / (N - 1.0));
        return From(adj, det, "detuned",
            "same ring, frequencies shifted out of ratio (ω(1 + 0.5·i/95))");
    }

    /// <summary>Control (b): D96 frequencies on a random 12-regular coupling (LCG, fixed seed).</summary>
    public static Lattice RandomCouplingRing()
    {
        var rng = new Lcg(20260911);
        var adj = new double[N, N];
        for (int i = 0; i < N; i++)
            while (CountNonZeroRow(adj, i) < 12)
            {
                int j = rng.Pick(N);
                if (j != i && adj[i, j] == 0.0) { adj[i, j] = 1.0; adj[j, i] = 1.0; }
            }
        return From(adj, ClosedFormOmega(), "random-coupling",
            "D96 frequencies on a deterministic random 12-regular coupling (LCG seed 20260911)");
    }

    /// <summary>Control (c): a linear-ramp spectrum over the same span on the D96 ring.</summary>
    public static Lattice LinearRamp()
    {
        var adj = GeneralInverseSpectrumAnalyzer.D96DerivedGraph(N, StepMax);
        var w = new double[N];
        for (int i = 0; i < N; i++) w[i] = Math.Sqrt(12.0 * i / (N - 1.0));
        return From(adj, w, "linear-ramp",
            "same ring, linear-ramp λ_i = 12·i/95 (no crowding, no degeneracy)");
    }

    /// <summary>
    /// Control (d): the degeneracy-matched lattice required by T_015 — the D96 multiplicity
    /// pattern {1, 42×2, 5, 6} carried by non-D96 values (a linear ramp in rank order), so that
    /// R3 cannot be attributed to the values rather than to the degeneracy class.
    /// </summary>
    public static Lattice DegeneracyMatched()
    {
        var pattern = MultiplicityPattern();
        var omega = new double[N];
        int node = 0;
        for (int m = 0; m < pattern.Count; m++)
        {
            double lambda = 12.0 * m / (pattern.Count - 1.0);
            for (int t = 0; t < pattern[m]; t++, node++) omega[node] = Math.Sqrt(lambda);
        }
        return From(GeneralInverseSpectrumAnalyzer.D96DerivedGraph(N, StepMax), omega, "deg-matched",
            "degeneracy-matched: D96 multiplicity pattern " + string.Join("+", pattern) +
            " on linear-ramp values (T_015 control)");
    }

    /// <summary>Ascending multiplicity pattern of the distinct D96 frequency levels (exact closed form).</summary>
    public static List<int> MultiplicityPattern()
    {
        var sorted = ClosedFormOmega().OrderBy(x => x).ToArray();
        var pattern = new List<int>();
        int s = 0;
        while (s < sorted.Length)
        {
            int e = s;
            while (e < sorted.Length && Math.Abs(sorted[e] - sorted[s]) <= 1e-9) e++;
            pattern.Add(e - s);
            s = e;
        }
        return pattern;
    }

    public static Lattice[] AllLattices() =>
        [D96(), D96Ranked(), DetunedRing(), RandomCouplingRing(), LinearRamp(), DegeneracyMatched()];

    private static int CountNonZeroRow(double[,] a, int i)
    {
        int c = 0;
        for (int j = 0; j < N; j++) if (a[i, j] != 0.0) c++;
        return c;
    }

    // ── Integrator (buffers are per-instance, so sweeps parallelise safely) ───

    private sealed class Integrator(Lattice lattice, double dt)
    {
        private readonly double[] _sin = new double[N];
        private readonly double[] _cos = new double[N];
        private readonly double[] _k1 = new double[N];
        private readonly double[] _k2 = new double[N];
        private readonly double[] _k3 = new double[N];
        private readonly double[] _k4 = new double[N];
        private readonly double[] _tmp = new double[N];
        private readonly double _dt = dt;

        public void Step(double[] theta, double g)
        {
            Derivative(theta, g, _k1);
            for (int i = 0; i < N; i++) _tmp[i] = theta[i] + 0.5 * _dt * _k1[i];
            Derivative(_tmp, g, _k2);
            for (int i = 0; i < N; i++) _tmp[i] = theta[i] + 0.5 * _dt * _k2[i];
            Derivative(_tmp, g, _k3);
            for (int i = 0; i < N; i++) _tmp[i] = theta[i] + _dt * _k3[i];
            Derivative(_tmp, g, _k4);
            for (int i = 0; i < N; i++)
                theta[i] += _dt / 6.0 * (_k1[i] + 2.0 * _k2[i] + 2.0 * _k3[i] + _k4[i]);
        }

        /// <summary>
        /// dθ_i/dτ = ω_i + g·(Σ_j w_ij sin(θ_j − θ_i)) / Σ_j w_ij, evaluated with the
        /// angle-addition identity (algebraically exact, 2 trig calls per node).
        /// </summary>
        public void Derivative(double[] theta, double g, double[] dst)
        {
            for (int i = 0; i < N; i++)
            {
                _sin[i] = Math.Sin(theta[i]);
                _cos[i] = Math.Cos(theta[i]);
            }
            for (int i = 0; i < N; i++)
            {
                var nb = lattice.Neighbors[i];
                var wt = lattice.Weights[i];
                double ss = 0.0, sc = 0.0, deg = 0.0;
                for (int t = 0; t < nb.Length; t++)
                {
                    ss += wt[t] * _sin[nb[t]];
                    sc += wt[t] * _cos[nb[t]];
                    deg += wt[t];
                }
                double coupling = deg > 0 ? (_cos[i] * ss - _sin[i] * sc) / deg : 0.0;
                dst[i] = lattice.Omega[i] + g * coupling;
            }
        }
    }

    // ── Sweep ─────────────────────────────────────────────────────────────────

    /// <summary>One seed's up/down sweep: the lock curves, the four H1 numbers and the per-node onsets.</summary>
    public sealed record SweepResult(
        double[] G,
        double[] FUp,
        double[] FDown,
        double[] FPerModeUp,
        double GCritical,
        double Sharpness,
        double SharpnessDown,
        double Width,
        double Hysteresis,
        double[] OnsetG,
        double NearGapOnset,
        double LockedAtFullG)
    {
        /// <summary>Linear interpolation of the upward lock curve f_up at an arbitrary g.</summary>
        public double AtG(double g)
        {
            if (g <= G[0]) return FUp[0];
            if (g >= G[^1]) return FUp[^1];
            for (int i = 1; i < G.Length; i++)
                if (g <= G[i])
                {
                    double t = (g - G[i - 1]) / (G[i] - G[i - 1]);
                    return FUp[i - 1] + t * (FUp[i] - FUp[i - 1]);
                }
            return FUp[^1];
        }
    }

    /// <summary>Sweep g upward over [0, gMax] then downward, carrying the state (this is what makes A ≠ 0 possible).</summary>
    public static SweepResult Sweep(
        Lattice lattice,
        uint seed,
        int steps = DefaultSweepSteps,
        double dt = DefaultDt,
        int settle = DefaultSettleSteps,
        int measure = DefaultMeasureSteps,
        double gMax = 1.0)
    {
        var integ = new Integrator(lattice, dt);
        double[] theta = InitialPhases(seed);

        var g = new double[steps + 1];
        var fUp = new double[steps + 1];
        var fDown = new double[steps + 1];
        var perMode = new double[steps + 1];
        var onset = Enumerable.Repeat(double.NaN, N).ToArray();

        for (int k = 0; k <= steps; k++)
        {
            g[k] = gMax * k / steps;
            Settle(integ, theta, g[k], settle);
            var m = Measure(integ, theta, lattice, g[k], measure, dt);
            fUp[k] = m.FractionLocked;
            perMode[k] = m.FractionPerMode;
            for (int i = 0; i < N; i++)
                if (double.IsNaN(onset[i]) && m.Locked[i]) onset[i] = g[k];
        }

        for (int k = steps; k >= 0; k--)
        {
            Settle(integ, theta, g[k], settle);
            fDown[k] = Measure(integ, theta, lattice, g[k], measure, dt).FractionLocked;
        }

        var nearGap = NearGapNodes(lattice);
        double nearGapOnset = nearGap.Length == 0 ? double.NaN : nearGap.Min(i => onset[i]);

        return new SweepResult(g, fUp, fDown, perMode,
            CriticalG(g, fUp), Sharpness(fUp), Sharpness(fDown), Width(g, fUp),
            Hysteresis(g, fUp, fDown), onset, nearGapOnset, fUp[steps]);
    }

    private static void Settle(Integrator integ, double[] theta, double g, int settle)
    {
        for (int s = 0; s < settle; s++) integ.Step(theta, g);
    }

    private sealed record MeasureResult(double FractionLocked, double FractionPerMode, bool[] Locked);

    /// <summary>
    /// Advance one measurement window from a clone of the state, averaging the per-node slip rate
    /// |d(θ_i − Ψ)/dτ| against the common ε = 0.01·ω₁ and the per-mode ε_i = 0.01·ω_i (T_014).
    /// The clone is copied back, so the caller's trajectory keeps the measured window.
    /// </summary>
    private static MeasureResult Measure(Integrator integ, double[] theta, Lattice lattice, double g, int measure, double dt)
    {
        double[] current = (double[])theta.Clone();
        var prevTheta = new double[N];
        var slip = new double[N];
        double psi = 0.0;
        bool havePsi = false;

        for (int s = 0; s < measure; s++)
        {
            Array.Copy(current, prevTheta, N);
            double psiPrev = psi;
            bool hadPsi = havePsi;
            integ.Step(current, g);
            psi = OrderParameterPhase(current, psiPrev, hadPsi);
            havePsi = true;
            if (!hadPsi) continue;
            for (int i = 0; i < N; i++)
                slip[i] += Math.Abs((current[i] - psi) - (prevTheta[i] - psiPrev));
        }

        double norm = Math.Max(measure - 1, 1) * dt;
        double epsCommon = EpsilonFraction * lattice.Omega1;
        var locked = new bool[N];
        int lockedCount = 0, perModeCount = 0, positive = 0;
        for (int i = 0; i < N; i++)
        {
            slip[i] /= norm;
            bool isPositive = lattice.IsPositive(i);
            if (isPositive) positive++;
            locked[i] = slip[i] < epsCommon;
            if (locked[i]) lockedCount++;
            if (isPositive && slip[i] < EpsilonFraction * lattice.Omega[i]) perModeCount++;
        }

        Array.Copy(current, theta, N);
        return new MeasureResult((double)lockedCount / N, (double)perModeCount / Math.Max(positive, 1), locked);
    }

    /// <summary>Collective phase Ψ = arg Σ_j e^{iθ_j}, unwrapped continuously from the previous value.</summary>
    private static double OrderParameterPhase(double[] theta, double psiPrev, bool havePsi)
    {
        double s = 0.0, c = 0.0;
        for (int i = 0; i < N; i++) { s += Math.Sin(theta[i]); c += Math.Cos(theta[i]); }
        double raw = Math.Atan2(s, c);
        if (!havePsi) return raw;
        while (raw - psiPrev > Math.PI) raw -= 2.0 * Math.PI;
        while (raw - psiPrev < -Math.PI) raw += 2.0 * Math.PI;
        return raw;
    }

    /// <summary>Deterministic initial phases: fixed-seed LCG stream (no stochastic dynamics).</summary>
    private static double[] InitialPhases(uint seed)
    {
        var rng = new Lcg(seed);
        var theta = new double[N];
        for (int i = 0; i < N; i++) theta[i] = rng.Unit() * 2.0 * Math.PI;
        return theta;
    }

    /// <summary>Nodes carrying the smallest positive frequency — the near-gap doublet (T_014).</summary>
    public static int[] NearGapNodes(Lattice lattice)
    {
        double min = lattice.Omega.Where(w => w > ZeroTolerance).Min();
        return Enumerable.Range(0, N).Where(i => Math.Abs(lattice.Omega[i] - min) <= 1e-9).ToArray();
    }

    // ── H1 metrics (verbatim from NP_170 §6 R3 / QG316) ───────────────────────

    /// <summary>g where f crosses 0.5 (the measured critical coupling) by linear interpolation.</summary>
    public static double CriticalG(double[] g, double[] f) => Cross(g, f, 0.5);

    /// <summary>sharp = max|Δf| / mean|Δf| over the sweep.</summary>
    public static double Sharpness(double[] f)
    {
        var d = new List<double>();
        for (int i = 1; i < f.Length; i++) d.Add(Math.Abs(f[i] - f[i - 1]));
        if (d.Count == 0) return 0.0;
        double mean = d.Average();
        return mean > 0 ? d.Max() / mean : 0.0;
    }

    /// <summary>width = g₉₀ − g₁₀ (the interval over which f climbs from 10% to 90%).</summary>
    public static double Width(double[] g, double[] f) => Cross(g, f, 0.9) - Cross(g, f, 0.1);

    /// <summary>A = ∫|f_up − f_down| dg — the normalized hysteresis area (g spans [0,1], so A ∈ [0,1]).</summary>
    public static double Hysteresis(double[] g, double[] up, double[] down)
    {
        double a = 0.0;
        for (int i = 1; i < g.Length; i++)
            a += 0.5 * (Math.Abs(up[i] - down[i]) + Math.Abs(up[i - 1] - down[i - 1])) * (g[i] - g[i - 1]);
        return a / (g[^1] - g[0]);
    }

    private static double Cross(double[] g, double[] f, double level)
    {
        for (int i = 1; i < f.Length; i++)
        {
            if ((f[i - 1] - level) * (f[i] - level) <= 0 && f[i] != f[i - 1])
            {
                double t = (level - f[i - 1]) / (f[i] - f[i - 1]);
                return g[i - 1] + t * (g[i] - g[i - 1]);
            }
        }
        return double.NaN;
    }

    // ── Multi-seed aggregation ────────────────────────────────────────────────

    /// <summary>Aggregate H1 report for one lattice over a fixed seed set, with the §7 statistics.</summary>
    public sealed record H1Report(
        string Name,
        int SeedCount,
        double GCritical,
        double GCriticalSigma,
        double Sharpness,
        double SharpnessSigma,
        double Width,
        double WidthSigma,
        double Hysteresis,
        double HysteresisSigma,
        double HysteresisSigmaRatio,
        double NearGapOnset,
        double LockedAtFullG,
        double LockedAtHardwareG,
        bool PassesSharpness,
        bool PassesWidth,
        bool PassesHysteresis,
        double SharpnessTheory,
        double WidthTheory)
    {
        /// <summary>True when the sweep reaches lock saturation inside the pre-registered range.</summary>
        public bool TransitionInRange => LockedAtFullG > 0.5;

        /// <summary>
        /// H1 passes only if the transition is inside the range and all three criteria hold
        /// (NP_170 §4: sharp ≥ 3 AND width ≤ 0.4 AND A > 0 at ≥5σ).
        /// </summary>
        public bool PassesH1 => TransitionInRange && PassesSharpness && PassesWidth && PassesHysteresis;

        /// <summary>Why H1 failed (empty when it passed) — the demotion/keep decision in NP_170 §8 hangs on this.</summary>
        public string FailureReason => PassesH1 ? "" :
            !TransitionInRange ? "transition outside the pre-registered range"
            : !PassesSharpness ? $"sharpness {Sharpness:F2} < {SharpnessTheory:F1}"
            : !PassesWidth ? $"width {Width:F3} > {WidthTheory:F2}"
            : $"hysteresis not significant (A/sigma = {HysteresisSigmaRatio:F2} < {SigmaCriterion:F1})";
    }

    public const double SharpnessCriterion = 3.0;
    public const double WidthCriterion = 0.4;
    public const double SigmaCriterion = 5.0;

    /// <summary>NP_170 §5.1 hardware normalization: g = 1 is the board's maximum coupling.</summary>
    public const double HardwareG = 1.0;

    public static H1Report Report(Lattice lattice, uint[]? seeds = null,
        int steps = DefaultSweepSteps, double dt = DefaultDt,
        int settle = DefaultSettleSteps, int measure = DefaultMeasureSteps, double gMax = 1.0)
    {
        var use = seeds ?? DefaultSeeds;
        var results = new SweepResult[use.Length];
        Parallel.For(0, use.Length, k => results[k] = Sweep(lattice, use[k], steps, dt, settle, measure, gMax));

        var stats = (Func<SweepResult, double> f) =>
        {
            var v = results.Select(f).Where(x => !double.IsNaN(x)).ToArray();
            if (v.Length == 0) return (Mean: double.NaN, Sigma: double.NaN);
            double m = v.Average();
            double sd = v.Length > 1 ? Math.Sqrt(v.Sum(x => (x - m) * (x - m)) / (v.Length - 1)) : 0.0;
            return (Mean: m, Sigma: sd);
        };

        var gc = stats(r => r.GCritical);
        var sh = stats(r => r.Sharpness);
        var wi = stats(r => r.Width);
        var hy = stats(r => r.Hysteresis);
        var ng = stats(r => r.NearGapOnset);
        var full = stats(r => r.LockedAtFullG);
        var atHardware = stats(r => r.AtG(HardwareG));

        double ratio = double.IsNaN(hy.Mean) ? double.NaN
            : hy.Sigma > 0 ? hy.Mean / hy.Sigma
            : hy.Mean > 0 ? double.PositiveInfinity : 0.0;

        return new H1Report(lattice.Name, use.Length,
            gc.Mean, gc.Sigma, sh.Mean, sh.Sigma, wi.Mean, wi.Sigma,
            hy.Mean, hy.Sigma, ratio, ng.Mean, full.Mean, atHardware.Mean,
            sh.Mean >= SharpnessCriterion, wi.Mean <= WidthCriterion, hy.Mean > 0 && ratio >= SigmaCriterion,
            SharpnessCriterion, WidthCriterion);
    }
}
