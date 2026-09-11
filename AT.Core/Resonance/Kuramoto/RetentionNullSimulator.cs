namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Retention-null simulator for ResearchY-NP_172: can a TWO-TIME-SCALE ring-down (τ_slow/τ_fast ≥ 10,
/// the NP_170 §6 R4 / H2 signature) arise from models that contain NO AT one-way barrier at all?
///
/// MODEL. A ring of N = 96 coupled complex oscillators,
///
///     da_i/dτ = ( −γ_i + i·ω_i )·a_i  +  g·( Σ_{j∈N(i)} w_ij·a_j ) / deg_i  −  β·|a_i|²·a_i
///
/// with ω_i the (normalized) natural frequency carried by node i, γ_i ≥ 0 the amplitude damping
/// (γ = 1/2Q), g the inter-node coupling and β ≥ 0 the amplitude-dependent (saturating) damping.
/// Nothing else: no topological charge, no one-way barrier, no memory term, no noise. The whole
/// point of the audit is that these three ingredients — damping, coupling, nonlinearity — are
/// generic oscillator physics, so any retention signature they produce is generic too.
///
/// PROTOCOL (NP_170 §6 R4, verbatim in spirit): charge the ring into the locked configuration
/// (the fundamental k = 1 eigenvector at amplitude A₀), then let it ring down with no drive, and
/// record the stored energy E(τ) = Σ_i |a_i|²/2. E is sampled as a moving-window envelope so that
/// the carrier oscillation at ω does not alias the fit.
///
/// FIT (NP_170 §7): single exponential E = a·e^(−τ/τ₁) versus two-time-scale
/// E = a·e^(−τ/τ_fast) + b·e^(−τ/τ_slow), compared by BIC/AIC with the SIMPLER model as the null.
/// The τ's are found on a fixed grid with the amplitudes solved linearly (variable projection), so
/// the fit is deterministic and needs no initial guess. R = τ_slow/τ_fast; H2's pass criterion is
/// R ≥ 10 with the two-time-scale fit preferred over the single-exponential null.
///
/// Deterministic throughout: fixed grid, fixed time step, no RNG of any kind.
/// </summary>
public static class RetentionNullSimulator
{
    public const int N = 96;

    /// <summary>Reference damping: γ = 1/(2Q) with Q = 100.</summary>
    public const double GammaReference = 0.005;

    /// <summary>Charge amplitude of the fundamental mode.</summary>
    public const double Amplitude0 = 1.0;

    /// <summary>Locked coupling carried over from NP_171 (g_c = 1.607, so g = 1.7 is just above).</summary>
    public const double Coupling = 1.7;

    /// <summary>H2 criterion: R = τ_slow/τ_fast ≥ 10 (NP_170 §4).</summary>
    public const double RCriterion = 10.0;

    /// <summary>BIC difference above which the two-time-scale fit is "preferred" (Kass–Raftery: &gt; 10 = strong).</summary>
    public const double StrongBic = 10.0;

    // ── Model specification ───────────────────────────────────────────────────

    /// <summary>A ring-down model: carrier topology/frequencies, per-node damping, nonlinearity.</summary>
    public sealed record ModelSpec(
        string Name,
        string Note,
        LockLatticeSimulator.Lattice Lattice,
        double[] Gamma,
        double Beta,
        double TimeMax = 600.0,
        double Dt = 0.05,
        double EnvelopeWindow = 10.0,
        double SampleEvery = 2.0);

    /// <summary>
    /// Uniform damping (γ_i = γ) or a deterministic Q contrast: γ rises geometrically with the node
    /// index from γ_min to γ_min·contrast. Real resonators do not have identical mode Q's; this is the
    /// second generic mechanism the audit must rule in or out.
    /// </summary>
    public static double[] UniformGamma(double gamma) => Enumerable.Repeat(gamma, N).ToArray();

    public static double[] ContrastGamma(double gammaMin, double contrast)
    {
        var g = new double[N];
        for (int i = 0; i < N; i++) g[i] = gammaMin * Math.Pow(contrast, (double)i / (N - 1));
        return g;
    }

    private static readonly LockLatticeSimulator.Lattice Ring = LockLatticeSimulator.D96();
    private static readonly LockLatticeSimulator.Lattice Random = LockLatticeSimulator.RandomCouplingRing();

    /// <summary>1. D96 lock lattice — the AT candidate, uniform Q, moderate saturation.</summary>
    public static ModelSpec D96LockLattice() => new(
        "D96 lock lattice",
        "canonical C96(±1..±6), uniform Q = 100, β = 0.05 (moderate saturation)",
        Ring, UniformGamma(GammaReference), 0.05);

    /// <summary>2. Random lattice — identical parameters, non-D96 topology (the T_015 control).</summary>
    public static ModelSpec RandomLattice() => new(
        "random lattice",
        "random 12-regular coupling, D96 frequencies, identical γ and β (topology control)",
        Random, UniformGamma(GammaReference), 0.05);

    /// <summary>3. High-Q ring — linear, uniform Q = 1000 (10× the reference).</summary>
    public static ModelSpec HighQRing() => new(
        "high-Q ring",
        "canonical ring, linear (β = 0), uniform Q = 1000",
        Ring, UniformGamma(GammaReference / 10.0), 0.0,
        TimeMax: 6000.0);

    /// <summary>4. Weakly nonlinear ring — uniform Q = 100, β = 0.01.</summary>
    public static ModelSpec WeaklyNonlinearRing() => new(
        "weakly nonlinear ring",
        "canonical ring, uniform Q = 100, β = 0.01 (weak saturation)",
        Ring, UniformGamma(GammaReference), 0.01);

    /// <summary>5. Strongly nonlinear ring — uniform Q = 100, β = 0.5.</summary>
    public static ModelSpec StronglyNonlinearRing() => new(
        "strongly nonlinear ring",
        "canonical ring, uniform Q = 100, β = 0.5 (strong saturation)",
        Ring, UniformGamma(GammaReference), 0.5);

    /// <summary>Probe P1 — pure Q contrast on the canonical ring, linear (β = 0), 100× spread.</summary>
    public static ModelSpec QContrastRing() => new(
        "Q-contrast ring (probe)",
        "canonical ring, LINEAR (β = 0), deterministic Q contrast γ ∈ [0.0005, 0.05] (100×, log-mean = γ_ref)",
        Ring, ContrastGamma(0.0005, 100.0), 0.0,
        TimeMax: 3000.0, SampleEvery: 10.0, EnvelopeWindow: 20.0);

    /// <summary>Probe P2 — the same Q contrast on the random topology.</summary>
    public static ModelSpec QContrastRandom() => new(
        "Q-contrast random (probe)",
        "random topology, LINEAR (β = 0), same deterministic Q contrast (topology independence)",
        Random, ContrastGamma(0.0005, 100.0), 0.0,
        TimeMax: 3000.0, SampleEvery: 10.0, EnvelopeWindow: 20.0);

    /// <summary>The five requested models.</summary>
    public static ModelSpec[] Models() =>
        [D96LockLattice(), RandomLattice(), HighQRing(), WeaklyNonlinearRing(), StronglyNonlinearRing()];

    /// <summary>The two mechanism probes (reported separately — they isolate the mechanism).</summary>
    public static ModelSpec[] Probes() => [QContrastRing(), QContrastRandom()];

    /// <summary>
    /// Verification case (NOT one of the five models): the canonical ring with a SINGLE common
    /// natural frequency, so the charged k = 1 state is an exact eigenmode and the closed-form
    /// envelope must be reproduced by the simulation to high accuracy. The contrast with the D96
    /// lattice (whose nodes carry different ω and therefore dephase) is itself a measured result.
    /// </summary>
    public static ModelSpec CoherentVerificationRing()
    {
        var uniform = Enumerable.Repeat(0.5, N).ToArray();
        var lat = Ring with { Omega = uniform, Multiplicity = Enumerable.Repeat(N, N).ToArray() };
        return new ModelSpec(
            "coherent ring (verification)",
            "canonical ring topology, SINGLE common ω (all nodes equal), uniform Q, β = 0.05 — the case "
            + "in which the charged state is an exact eigenmode and the closed form applies exactly",
            lat, UniformGamma(GammaReference), 0.05,
            TimeMax: 600.0);
    }

    // ── Ring-down ────────────────────────────────────────────────────────────

    /// <summary>One sampled envelope point of the ring-down.</summary>
    public sealed record Sample(double Time, double Energy);

    /// <summary>Deterministic ring-down of a model from the charged fundamental configuration.</summary>
    public static IReadOnlyList<Sample> RingDown(ModelSpec spec)
    {
        var lat = spec.Lattice;
        var re = new double[N];
        var im = new double[N];
        for (int i = 0; i < N; i++)
        {
            // The charged (locked) configuration: the fundamental k = 1 mode, phase 2πi/N.
            double phase = 2.0 * Math.PI * i / N;
            re[i] = Amplitude0 * Math.Cos(phase);
            im[i] = Amplitude0 * Math.Sin(phase);
        }

        double[] k1r = new double[N], k1i = new double[N];
        double[] k2r = new double[N], k2i = new double[N];
        double[] k3r = new double[N], k3i = new double[N];
        double[] k4r = new double[N], k4i = new double[N];
        double[] tr = new double[N], ti = new double[N];

        int steps = (int)Math.Round(spec.TimeMax / spec.Dt);
        int windowSteps = Math.Max(1, (int)Math.Round(spec.EnvelopeWindow / spec.Dt));
        int sampleEvery = Math.Max(1, (int)Math.Round(spec.SampleEvery / spec.Dt));
        var samples = new List<Sample>();
        var window = new Queue<double>();

        void Derivative(double[] r, double[] m, double[] outR, double[] outI)
        {
            for (int i = 0; i < N; i++)
            {
                double ar = r[i], ai = m[i];
                double amp2 = ar * ar + ai * ai;
                var nb = lat.Neighbors[i];
                var wt = lat.Weights[i];
                double sumR = 0.0, sumI = 0.0, deg = 0.0;
                for (int t = 0; t < nb.Length; t++)
                {
                    sumR += wt[t] * r[nb[t]];
                    sumI += wt[t] * m[nb[t]];
                    deg += wt[t];
                }
                if (deg > 0) { sumR /= deg; sumI /= deg; }
                // Reactive (phase) coupling: i·g·Σ w_ij a_j — it shifts frequency, never pumps amplitude.
                // A real coupling here would add g·Re(s) to the growth rate and destabilize the ring.
                double cplR = -Coupling * sumI, cplI = Coupling * sumR;
                outR[i] = -spec.Gamma[i] * ar - lat.Omega[i] * ai + cplR - spec.Beta * amp2 * ar;
                outI[i] = -spec.Gamma[i] * ai + lat.Omega[i] * ar + cplI - spec.Beta * amp2 * ai;
            }
        }

        for (int s = 0; s <= steps; s++)
        {
            double energy = 0.0;
            for (int i = 0; i < N; i++) energy += 0.5 * (re[i] * re[i] + im[i] * im[i]);
            window.Enqueue(energy);
            if (window.Count > windowSteps) window.Dequeue();
            if (s % sampleEvery == 0) samples.Add(new Sample(s * spec.Dt, window.Average()));

            Derivative(re, im, k1r, k1i);
            for (int i = 0; i < N; i++) { tr[i] = re[i] + 0.5 * spec.Dt * k1r[i]; ti[i] = im[i] + 0.5 * spec.Dt * k1i[i]; }
            Derivative(tr, ti, k2r, k2i);
            for (int i = 0; i < N; i++) { tr[i] = re[i] + 0.5 * spec.Dt * k2r[i]; ti[i] = im[i] + 0.5 * spec.Dt * k2i[i]; }
            Derivative(tr, ti, k3r, k3i);
            for (int i = 0; i < N; i++) { tr[i] = re[i] + spec.Dt * k3r[i]; ti[i] = im[i] + spec.Dt * k3i[i]; }
            Derivative(tr, ti, k4r, k4i);
            for (int i = 0; i < N; i++)
            {
                re[i] += spec.Dt / 6.0 * (k1r[i] + 2.0 * k2r[i] + 2.0 * k3r[i] + k4r[i]);
                im[i] += spec.Dt / 6.0 * (k1i[i] + 2.0 * k2i[i] + 2.0 * k3i[i] + k4i[i]);
            }
        }

        return samples;
    }

    /// <summary>R below which the fit cannot resolve two time scales: the τ-grid spacing ratio.</summary>
    public static readonly double GridFloorRatio = GridCount > 1 ? Math.Pow(TauGridSpan, 1.0 / (GridCount - 1)) : 1.0;

    private const int GridCount = 140;
    private const double TauGridSpan = 4000.0;   // tauMax/tauMin with the defaults below

    /// <summary>The fitted ring-down: the two time scales, their ratio and the model comparison.</summary>
    public sealed record RetentionFit(
        string Name,
        double TauFast,
        double TauSlow,
        double R,
        double AmplitudeFast,
        double AmplitudeSlow,
        double BicSingle,
        double BicDouble,
        double AicSingle,
        double AicDouble,
        double EnergyRetained,
        bool TwoScalePreferred)
    {
        /// <summary>ΔBIC = BIC(single) − BIC(two-scale); positive means the simpler model is worse.</summary>
        public double DeltaBic => BicSingle - BicDouble;

        /// <summary>ΔAIC, same convention.</summary>
        public double DeltaAic => AicSingle - AicDouble;

        /// <summary>H2's pass criterion: R ≥ 10 with the two-time-scale fit preferred over the null.</summary>
        public bool PassesH2 => R >= RCriterion && TwoScalePreferred;

        /// <summary>NP_170 §6 R4 storage figure of merit R·E_ret/E_in.</summary>
        public double FigureOfMerit => R * EnergyRetained;
    }

    /// <summary>
    /// Fit E(τ): the τ grid is fixed (log-spaced), the amplitudes are solved linearly, and the
    /// single- versus two-exponential comparison uses BIC/AIC with the SIMPLER model as the null.
    /// </summary>
    public static RetentionFit Fit(ModelSpec spec, IReadOnlyList<Sample> samples)
    {
        var t = samples.Select(s => s.Time).ToArray();
        var y = samples.Select(s => s.Energy).ToArray();
        int n = t.Length;

        double tauMin = spec.TimeMax / 2000.0;
        double tauMax = spec.TimeMax * 2.0;
        var taus = new double[GridCount];
        for (int g = 0; g < GridCount; g++)
            taus[g] = tauMin * Math.Pow(tauMax / tauMin, (double)g / (GridCount - 1));

        // Single exponential: E = a·e^(−τ/tau).
        double bestSse1 = double.PositiveInfinity, bestTau1 = double.NaN, bestA1 = 0.0;
        foreach (double tau in taus)
        {
            double sxx = 0, sxy = 0;
            for (int i = 0; i < n; i++) { double e = Math.Exp(-t[i] / tau); sxx += e * e; sxy += e * y[i]; }
            if (sxx <= 0) continue;
            double a = sxy / sxx;
            double sse = 0;
            for (int i = 0; i < n; i++) { double r = y[i] - a * Math.Exp(-t[i] / tau); sse += r * r; }
            if (sse < bestSse1) { bestSse1 = sse; bestTau1 = tau; bestA1 = a; }
        }

        // Two exponentials: E = a·e^(−τ/τ_fast) + b·e^(−τ/τ_slow), τ_fast < τ_slow.
        double bestSse2 = double.PositiveInfinity, tf = double.NaN, ts = double.NaN, af = 0.0, ab = 0.0;
        var basis = new double[GridCount][];
        for (int g = 0; g < GridCount; g++)
        {
            basis[g] = new double[n];
            for (int i = 0; i < n; i++) basis[g][i] = Math.Exp(-t[i] / taus[g]);
        }

        for (int g1 = 0; g1 < GridCount; g1++)
        {
            var e1 = basis[g1];
            for (int g2 = g1 + 1; g2 < GridCount; g2++)
            {
                var e2 = basis[g2];
                double s11 = 0, s22 = 0, s12 = 0, s1y = 0, s2y = 0;
                for (int i = 0; i < n; i++)
                {
                    s11 += e1[i] * e1[i];
                    s22 += e2[i] * e2[i];
                    s12 += e1[i] * e2[i];
                    s1y += e1[i] * y[i];
                    s2y += e2[i] * y[i];
                }
                double det = s11 * s22 - s12 * s12;
                if (Math.Abs(det) < 1e-30) continue;
                double a = (s1y * s22 - s2y * s12) / det;
                double b = (s11 * s2y - s12 * s1y) / det;
                if (a < 0 || b < 0) continue;   // a physical decay needs non-negative amplitudes
                double sse = 0;
                for (int i = 0; i < n; i++) { double r = y[i] - a * e1[i] - b * e2[i]; sse += r * r; }
                if (sse < bestSse2)
                {
                    bestSse2 = sse; tf = taus[g1]; ts = taus[g2]; af = a; ab = b;
                }
            }
        }

        double Bic(double sse, int k) => n * Math.Log(sse / n) + k * Math.Log(n);
        double Aic(double sse, int k) => n * Math.Log(sse / n) + 2.0 * k;

        double bic1 = Bic(bestSse1, 2), bic2 = Bic(bestSse2, 4);
        double aic1 = Aic(bestSse1, 2), aic2 = Aic(bestSse2, 4);
        double e0 = y[0], eEnd = y[^1];
        double retained = e0 > 0 ? eEnd / e0 : 0.0;

        return new RetentionFit(spec.Name, tf, ts, ts / tf, af, ab,
            bic1, bic2, aic1, aic2, retained, bic1 - bic2 > 0);
    }

    /// <summary>Run and fit one model.</summary>
    public static RetentionFit Run(ModelSpec spec) => Fit(spec, RingDown(spec));

    // ── Closed form for the coherent charge (the analytic null) ──────────────

    /// <summary>
    /// Exact envelope of a coherent uniform-amplitude charge in a uniform-Q ring. With
    /// a_i = A(τ)·e^(i(φ+2πi/N)) the nonlinear term is uniform, the reactive coupling is a pure
    /// frequency shift, and u = A² obeys du/dτ = −2γu − 2βu², so
    ///
    ///     E(τ) = u₀·e^(−2γτ) / ( 1 + (β·u₀/γ)·(1 − e^(−2γτ)) ) .
    ///
    /// Consequences: the LATE-time rate is 2γ for every β (τ_slow → 1/(2γ)); the early decay is
    /// accelerated by βu₀; and the shape of the whole curve depends only on the single
    /// dimensionless number β·u₀/γ — topology does not appear. This is the formula that makes the
    /// two-time-scale appearance a generic amplitude-saturation effect rather than a barrier effect.
    /// </summary>
    public static double CoherentEnvelope(double tau, double gamma, double beta, double u0)
        => u0 * Math.Exp(-2.0 * gamma * tau) / (1.0 + (beta * u0 / gamma) * (1.0 - Math.Exp(-2.0 * gamma * tau)));

    /// <summary>Window average of the closed form, matching the sampler's moving-window envelope.</summary>
    public static double CoherentEnvelopeWindowed(double tau, double gamma, double beta, double u0,
        double window, int steps = 64)
    {
        double lo = Math.Max(0.0, tau - window);
        double h = (tau - lo) / steps;
        double sum = 0.0;
        for (int i = 0; i <= steps; i++)
        {
            double w = (i == 0 || i == steps) ? 1.0 : (i % 2 == 0 ? 2.0 : 4.0);
            sum += w * CoherentEnvelope(lo + i * h, gamma, beta, u0);
        }
        return sum * h / 3.0 / (tau - lo);
    }
}
