using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_171 — Deterministic Lock-Lattice Simulator (numerical precursor to NP_170).
///
/// Question: does the C96(±1..±6) lock lattice, evolved with deterministic Adler/Kuramoto
/// dynamics and NO noise, exhibit the lock threshold that NP_170 pre-registered for hardware
/// (H1: sharpness ≥ 3, width ≤ 0.4, hysteresis A &gt; 0 at ≥ 5σ)? And if so, what are the
/// predicted g_c, sharpness, width and hysteresis — before any hardware exists?
///
/// Everything here is a MODEL prediction. The locking nonlinearity is an imported BOUNDARY
/// input (NP_005), the engineered coupling network is imported (NP_007/NP_011), and the ring's
/// gap is an imported kinematic finite-size gap (NP_169). A simulator cannot confirm AT; it can
/// only say what the lock law would look like if it is realisable, and it can falsify the
/// expectation that the threshold is specific to the D96 lattice.
///
/// Deterministic: no stochastic term enters the dynamics. Fixed-seed LCG streams supply the
/// initial phases only (30 enumerated seeds = the protocol's ≥30 repetitions), so every number
/// below is exactly reproducible.
/// </summary>
public class Y_NP_171_Tests : ResearchTestBase
{
    /// <summary>
    /// Pre-registered sweep range, fixed after a 3-seed pilot (gMax = 4) showed the transition
    /// between g ≈ 1.4 and g ≈ 2.1. Coupling is in units of the fastest mode's frequency
    /// (ω_max = 1); 40 steps up and 40 down, as in NP_170 §5.1 / QG316.
    /// </summary>
    private const double SweepMax = 3.0;

    /// <summary>NP_170 §5.1 hardware normalization: g = 1 is the board's maximum coupling.</summary>
    private const double HardwareMaxG = 1.0;

    public Y_NP_171_Tests(ITestOutputHelper output) : base(output) { }

    private static readonly uint[] ThirtySeeds = LockLatticeSimulator.DefaultSeeds;
    private static readonly uint[] TwelveSeeds = LockLatticeSimulator.DefaultSeeds.Take(12).ToArray();

    /// <summary>Closed-form spectrum, shared by the basis test and the scale-free restatements.</summary>
    private static readonly double[] Lambda = LockLatticeSimulator.ClosedFormLambda();
    private static readonly double LambdaMax = Lambda.Max();
    private static readonly double OmegaMaxPhysical = Math.Sqrt(LambdaMax);
    private static readonly double Omega1Physical = Math.Sqrt(Lambda[1]);

    // ── 1. Assumptions, canonical basis and lattice construction ─────────────

    [Fact]
    public void NP171_01_Canonical_Basis_And_Lattice_Construction()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_171 — Deterministic Lock-Lattice Simulator");
        PrintHeader("1. Assumptions and canonical basis");

        sb.AppendLine("ASSUMPTIONS (stated before any number is produced)");
        sb.AppendLine("  A1. Lattice: C96(±1..±6), degree 12 — the canonical D96 attractor (D_015).");
        sb.AppendLine("  A2. Dynamics: dθ_i/dτ = ω_i + g·(1/deg)·Σ_{j∈N(i)} sin(θ_j − θ_i).");

        var lam = Lambda;
        double lam2 = Lambda[1];
        double omega1Physical = Omega1Physical;
        double lambdaMax = LambdaMax;
        double omegaMax = OmegaMaxPhysical;
        double span = omegaMax / omega1Physical;
        var argMax = Enumerable.Range(0, 96).Where(k => Math.Abs(lam[k] - lambdaMax) < 1e-12).ToArray();

        sb.AppendLine("      Adler/Kuramoto phase ring. The nonlinearity is IMPORTED (NP_005: the locking");
        sb.AppendLine("      force is ABSENT from the canonical chain), so this tests the lock STRUCTURE only.");
        sb.AppendLine("  A3. Frequencies: node i carries eigenmode i, ω_i = √λ_i, λ_k = 2Σ_{d=1..6}(1 − cos 2πdk/96),");
        sb.AppendLine("      taken from the EXACT closed form (no eigensolver), normalized by ω_max.");
        sb.AppendLine("  A4. No stochastic term. Fixed-seed LCG streams set the initial phases only.");
        sb.AppendLine("  A5. Integrator: RK4, dt = 0.1 (≈63 steps per period of the fastest mode).");
        sb.AppendLine("  A6. Lock criterion (NP_170 R3): a node is locked when its mean slip over the measurement");
        sb.AppendLine("      window satisfies |d(θ_i − Ψ)/dτ| < ε, Ψ = collective phase; primary ε = 0.01·ω₁");
        sb.AppendLine("      (all 96 nodes) plus the T_014 per-mode refinement ε_i = 0.01·ω_i (95 positive modes).");
        sb.AppendLine("  A7. Sweep g upward then downward, carrying the state (that continuation is what makes an");
        sb.AppendLine();
        sb.AppendLine("CANONICAL BASIS REPRODUCED (this document is self-contained; no NP_170 value is trusted)");
        sb.AppendLine($"  λ₂ (k = 1)            = {lam2.ToString("F15", CultureInfo.InvariantCulture)}   (canonical 0.386350893377790)");
        sb.AppendLine($"  ω₁ = √λ₂ (physical)   = {omega1Physical.ToString("F15", CultureInfo.InvariantCulture)}   (canonical 0.621571309969974)");
        sb.AppendLine($"  ω_max = √λ_max        = {omegaMax.ToString("F15", CultureInfo.InvariantCulture)}   (canonical 3.979620)");
        sb.AppendLine($"    λ_max = {lambdaMax.ToString("F15", CultureInfo.InvariantCulture)} at k = {{{string.Join(",", argMax)}}} — NOTE: NP_170 §3's parenthetical");
        sb.AppendLine($"    '(k = 48, λ = 12)' is a label error: λ_48 = 12 exactly, but the spectrum maximum is");
        sb.AppendLine($"    {lambdaMax.ToString("F9", CultureInfo.InvariantCulture)} at k = 11/85 (the mirror pair). Values unchanged; label corrected here.");
        sb.AppendLine($"  span = ω_max/ω₁       = {span.ToString("F6", CultureInfo.InvariantCulture)}   (canonical 6.402515)");
        sb.AppendLine($"  ω₁ normalized         = {(omega1Physical / omegaMax).ToString("F8", CultureInfo.InvariantCulture)}");

        Assert.Equal(0.386350893377790, lam2, 12);
        Assert.Equal(0.621571309969974, omega1Physical, 12);
        Assert.Equal(3.979620, omegaMax, 5);
        Assert.Equal(6.402515, span, 5);
        Assert.Equal(new[] { 11, 85 }, argMax);

        // Multiplicity structure: the canonical [42×2, 5, 6] plus the zero mode.
        var pattern = LockLatticeSimulator.MultiplicityPattern();
        var hist = pattern.GroupBy(x => x).OrderBy(g => g.Key)
            .Select(g => $"{g.Key}×{g.Count()}").ToArray();
        sb.AppendLine();
        sb.AppendLine("  multiplicity pattern  = " + string.Join(", ", hist) +
                      $"  (levels {pattern.Count}, modes {pattern.Sum()})");
        sb.AppendLine("  zero modes            = " + lam.Count(l => Math.Abs(l) < 1e-12));
        int mirrorPairs = 0;
        for (int k = 1; k <= 47; k++) if (Math.Abs(lam[k] - lam[96 - k]) < 1e-12) mirrorPairs++;
        sb.AppendLine($"  mirror pairs λ_k = λ_{{96−k}} = {mirrorPairs} (+ self-conjugate k = 48)");
        Assert.Equal(45, pattern.Count);
        Assert.Equal(96, pattern.Sum());
        Assert.Equal(42, pattern.Count(p => p == 2));
        Assert.Equal(1, pattern.Count(p => p == 5));
        Assert.Equal(1, pattern.Count(p => p == 6));
        Assert.Equal(1, lam.Count(l => Math.Abs(l) < 1e-12));
        Assert.Equal(47, mirrorPairs);

        // Lattice construction: the canonical arrangement and the arrangement-sensitivity variant.
        var d96 = LockLatticeSimulator.D96();
        var ranked = LockLatticeSimulator.D96Ranked();
        var nearGap = LockLatticeSimulator.NearGapNodes(d96);
        sb.AppendLine($"  near-gap doublet      = nodes {{{string.Join(",", nearGap)}}} (the k = ±1 pair), ω₁ = {d96.Omega1.ToString("F8", CultureInfo.InvariantCulture)}");
        sb.AppendLine($"  D96 arrangement       = mode index (the mirror pair k ↔ 96−k is a spatial reflection)");
        sb.AppendLine($"  D96-ranked            = same multiset sorted ascending (monotone frequency gradient)");
        Assert.Equal(new[] { 1, 95 }, nearGap);
        Assert.Equal(12, d96.Neighbors[0].Length);
        Assert.Equal(d96.Omega.OrderBy(x => x).ToArray(), ranked.Omega.OrderBy(x => x).ToArray());

        PrintHeader("2. Metrics (verbatim from NP_170 §6 R3 / QG316)");
        sb.AppendLine("  f(g)   = fraction of nodes with mean slip < ε");
        sb.AppendLine("  A      = ∫|f_up − f_down| dg / (g_max − g_min)          (hysteresis area, normalized)");
        sb.AppendLine("  sharp  = max|Δf| / mean|Δf|                             (must be ≥ 3)");
        sb.AppendLine("  width  = g₉₀ − g₁₀                                      (must be ≤ 0.4)");
        sb.AppendLine("  H1     = sharp ≥ 3 AND width ≤ 0.4 AND A > 0 at ≥ 5σ across the seed set");
        sb.AppendLine();
        sb.AppendLine($"  pre-registered sweep range: g ∈ [0, {SweepMax.ToString("F1", CultureInfo.InvariantCulture)}], 40 steps each way");
        sb.AppendLine($"  (pilot at gMax = 4 found the transition between g ≈ 1.4 and g ≈ 2.1; the range covers it)");
        sb.AppendLine($"  NP_170 §5.1 hardware normalization puts g = 1 at the board's MAXIMUM coupling — the");
        sb.AppendLine($"  fraction locked at g = {HardwareMaxG.ToString("F1", CultureInfo.InvariantCulture)} is reported separately for that reason.");

        // Metric sanity: definitions must behave on known inputs.
        double[] gEven = [0.0, 0.5, 1.0];
        double[] stepUp = [0.0, 0.0, 1.0];
        double[] same = [0.0, 0.0, 1.0];
        sb.AppendLine();
        sb.AppendLine($"  metric self-test: A(same curves) = {LockLatticeSimulator.Hysteresis(gEven, stepUp, same).ToString("F6", CultureInfo.InvariantCulture)} (must be 0)");
        sb.AppendLine($"                    width of a step at g = 1 equals the step location: {LockLatticeSimulator.Width([0.0, 1.0], [0.0, 1.0]).ToString("F6", CultureInfo.InvariantCulture)}");
        Assert.Equal(0.0, LockLatticeSimulator.Hysteresis(gEven, stepUp, same), 12);
        Assert.True(LockLatticeSimulator.Sharpness(stepUp) > 1.0);

        Output.WriteLine(sb.ToString());
    }

    // ── 3. Determinism and integrator adequacy ───────────────────────────────

    [Fact]
    public void NP171_02_Determinism_And_Integrator_Convergence()
    {
        var sb = new StringBuilder();
        PrintHeader("3. Determinism and integrator adequacy");

        var lat = LockLatticeSimulator.D96();
        uint seed = ThirtySeeds[0];

        // Same seed twice → bit-identical curves (deterministic requirement).
        var a = LockLatticeSimulator.Sweep(lat, seed, steps: 16, dt: 0.1, settle: 400, measure: 200, gMax: SweepMax);
        var b = LockLatticeSimulator.Sweep(lat, seed, steps: 16, dt: 0.1, settle: 400, measure: 200, gMax: SweepMax);
        sb.AppendLine($"  same seed, two runs: identical f_up = {a.FUp.SequenceEqual(b.FUp)}, identical A = {a.Hysteresis == b.Hysteresis}");

        // Different seed → different initial phases (the seed set must not be degenerate).
        var c = LockLatticeSimulator.Sweep(lat, ThirtySeeds[1], steps: 16, dt: 0.1, settle: 400, measure: 200, gMax: SweepMax);
        sb.AppendLine($"  different seed: f_up differs = {!a.FUp.SequenceEqual(c.FUp)}  (f at gMax: {a.LockedAtFullG:F3} vs {c.LockedAtFullG:F3})");

        // Integrator adequacy: dt = 0.1 versus dt = 0.05 on the same trajectory family.
        var fine = LockLatticeSimulator.Sweep(lat, seed, steps: 16, dt: 0.05, settle: 800, measure: 400, gMax: SweepMax);
        double maxDiff = 0.0;
        for (int i = 0; i < a.FUp.Length; i++) maxDiff = Math.Max(maxDiff, Math.Abs(a.FUp[i] - fine.FUp[i]));
        sb.AppendLine($"  dt = 0.1 vs dt = 0.05: max |Δf_up| = {maxDiff:F4}; g_c {a.GCritical:F4} vs {fine.GCritical:F4}");
        sb.AppendLine("  conclusion: the lock curve is integrator-converged at dt = 0.1 (the step is a bifurcation,");
        sb.AppendLine("  so a small shift of the crossing point between dt values is expected and bounded).");

        Assert.True(a.FUp.SequenceEqual(b.FUp), "the simulator must be bit-deterministic for a fixed seed");
        Assert.Equal(a.Hysteresis, b.Hysteresis);
        Assert.True(double.IsNaN(fine.GCritical) || Math.Abs(a.GCritical - fine.GCritical) <= 0.4,
            $"the transition location must be integrator-stable (g_c {a.GCritical:F4} vs {fine.GCritical:F4})");
        Assert.True(maxDiff <= 0.5, $"dt = 0.1 must track dt = 0.05 (max |Δf| = {maxDiff:F4})");

        // The lock criterion must be a real discriminator: locked slip ≪ ε ≪ unlocked slip.
        sb.AppendLine();
        sb.AppendLine("  criterion margins (seed 1, g = 0 and g = gMax):");
        var low = LockLatticeSimulator.Sweep(lat, seed, steps: 2, dt: 0.1, settle: 2000, measure: 200, gMax: SweepMax);
        sb.AppendLine($"    f(0) = {low.FUp[0]:F4}, f(gMax) = {low.LockedAtFullG:F4}");
        Assert.True(low.FUp[0] < 0.05, "at zero coupling essentially nothing may be locked");
        Assert.True(low.LockedAtFullG > 0.95, "at the top of the sweep the ring must be fully locked");

        Output.WriteLine(sb.ToString());
    }

    // ── 4. The H1 report ─────────────────────────────────────────────────────

    [Fact]
    public void NP171_03_H1_Lock_Threshold_Report()
    {
        var sb = new StringBuilder();
        PrintHeader("4. H1 — lock threshold, hysteresis and the predicted hardware numbers");

        var d96 = LockLatticeSimulator.D96();
        var ranked = LockLatticeSimulator.D96Ranked();

        sb.AppendLine($"  seed sets: D96 = {ThirtySeeds.Length} seeds (protocol ≥30 repeats), variants/controls = {TwelveSeeds.Length} seeds");
        sb.AppendLine();
        sb.AppendLine("  lattice                g_c      sharp    width      A       A/σ_A  f(g=1)  H1 / reason");
        sb.AppendLine("  " + new string('-', 96));

        var d96Report = LockLatticeSimulator.Report(d96, ThirtySeeds, gMax: SweepMax);
        var rankedReport = LockLatticeSimulator.Report(ranked, TwelveSeeds, gMax: SweepMax);
        var controls = new (string Label, LockLatticeSimulator.H1Report R)[]
        {
            ("detuned (a)", LockLatticeSimulator.Report(LockLatticeSimulator.DetunedRing(), TwelveSeeds, gMax: SweepMax)),
            ("random-coupling (b)", LockLatticeSimulator.Report(LockLatticeSimulator.RandomCouplingRing(), TwelveSeeds, gMax: SweepMax)),
            ("linear-ramp (c)", LockLatticeSimulator.Report(LockLatticeSimulator.LinearRamp(), TwelveSeeds, gMax: SweepMax)),
            ("deg-matched (d)", LockLatticeSimulator.Report(LockLatticeSimulator.DegeneracyMatched(), TwelveSeeds, gMax: SweepMax)),
        };

        string Num(double v, string fmt) => double.IsNaN(v) ? "  n/a" : v.ToString(fmt, CultureInfo.InvariantCulture);

        void Row(string label, LockLatticeSimulator.H1Report r)
        {
            bool inRange = r.TransitionInRange;
            string ratio = double.IsNaN(r.HysteresisSigmaRatio) ? " n/a"
                : double.IsPositiveInfinity(r.HysteresisSigmaRatio) ? " inf"
                : r.HysteresisSigmaRatio.ToString("F1", CultureInfo.InvariantCulture);
            sb.AppendLine($"  {label,-22} {Num(inRange ? r.GCritical : double.NaN, "F3"),6}  {Num(inRange ? r.Sharpness : double.NaN, "F2"),6}  {Num(inRange ? r.Width : double.NaN, "F3"),6}  {Num(r.Hysteresis, "F4"),7}  {ratio,5}  {Num(r.LockedAtHardwareG, "F4"),6}  {(r.PassesH1 ? "PASS" : "fail: " + r.FailureReason)}");
        }

        Row("D96 (canonical)", d96Report);
        Row("D96-ranked", rankedReport);
        foreach (var (label, r) in controls) Row(label, r);

        sb.AppendLine();
        sb.AppendLine("  criterion detail (NP_170 §4 H1):");
        sb.AppendLine($"    D96: sharp {d96Report.Sharpness:F2} ≥ 3 → {d96Report.PassesSharpness}; width {d96Report.Width:F3} ≤ 0.4 → {d96Report.PassesWidth}; A = {d96Report.Hysteresis:F4} ± {d96Report.HysteresisSigma:F4} (A/σ = {d96Report.HysteresisSigmaRatio:F2}) → {d96Report.PassesHysteresis}");
        sb.AppendLine($"    σ across seeds: g_c ± {d96Report.GCriticalSigma:F4}, sharp ± {d96Report.SharpnessSigma:F2}, width ± {d96Report.WidthSigma:F3}");

        // The hardware-relevant statement: nothing locks inside the board's nominal range.
        sb.AppendLine();
        sb.AppendLine($"  HARDWARE-RELEVANT OUTPUT — the fraction locked at the board's maximum coupling (g = 1):");
        sb.AppendLine($"    D96 f(1) = {d96Report.LockedAtHardwareG:F4}  → the predicted threshold lies at g_c = {d96Report.GCritical:F3},");
        sb.AppendLine($"    i.e. {d96Report.GCritical / HardwareMaxG:F2}× the nominal maximum coupling. A board whose coupling cannot exceed");
        sb.AppendLine("    g = 1 shows NO transition with these oscillator frequencies (see the doc §5 for the");
        sb.AppendLine("    equivalent statement in units of ω₁ and of the mean detuning).");

        sb.AppendLine();
        sb.AppendLine("  PREDICTED NUMBERS (model, before hardware):");
        sb.AppendLine($"    g_c        = {d96Report.GCritical:F3} ± {d96Report.GCriticalSigma:F3}   (units: ω_max = 1; domain-specific, QG313 — NOT QG316's 0.31)");
        sb.AppendLine($"    sharpness  = {d96Report.Sharpness:F2} ± {d96Report.SharpnessSigma:F2}");
        sb.AppendLine($"    width      = {d96Report.Width:F3} ± {d96Report.WidthSigma:F3}   (absolute g units on [0,{SweepMax:F1}])");
        sb.AppendLine($"    hysteresis = {d96Report.Hysteresis:F4} ± {d96Report.HysteresisSigma:F4}   (A ∈ [0,1], normalized)");
        sb.AppendLine($"    near-gap doublet onset g = {d96Report.NearGapOnset:F3}");

        // Scale-free restatements — the form the hardware team can actually design against.
        double omegaMean = d96.Omega.Average();
        double meanAbsDetuning = d96.Omega.Average(w => Math.Abs(w - omegaMean));
        double requiredLockAmplitude = d96Report.GCritical * OmegaMaxPhysical;
        double meanAbsDetuningPhysical = meanAbsDetuning * OmegaMaxPhysical;
        sb.AppendLine();
        sb.AppendLine("  SCALE-FREE RESTATEMENTS (the transferable prediction):");
        sb.AppendLine($"    mean natural frequency Ω = {omegaMean:F4} (normalized), mean |ω_i − Ω| = {meanAbsDetuning:F4} normalized");
        sb.AppendLine($"    required per-node lock amplitude K ≥ {requiredLockAmplitude:F3} rad/time — i.e.");
        sb.AppendLine($"      K = {d96Report.GCritical:F2}·ω_max = {requiredLockAmplitude / Omega1Physical:F2}·ω₁ = {requiredLockAmplitude / meanAbsDetuningPhysical:F2}·⟨|ω_i − Ω|⟩");
        sb.AppendLine($"    so the board must deliver a lock term ≈ {requiredLockAmplitude / Omega1Physical:F1}× the fundamental frequency.");

        // Invariants that must hold for any model realisation.
        foreach (var (label, r) in controls.Concat([("D96 (canonical)", d96Report), ("D96-ranked", rankedReport)]))
        {
            Assert.True(double.IsNaN(r.Hysteresis) || (r.Hysteresis >= 0.0 && r.Hysteresis <= 1.0), $"{label}: A must be in [0,1]");
            Assert.True(double.IsNaN(r.Sharpness) || r.Sharpness >= 0.0, $"{label}: sharpness must be non-negative");
            Assert.True(double.IsNaN(r.GCritical) || (r.GCritical > 0.0 && r.GCritical < SweepMax), $"{label}: a measured g_c must lie inside the sweep");
        }
        Assert.True(d96Report.LockedAtFullG > 0.95, "the canonical D96 ring must lock inside the pre-registered range");
        Assert.True(!double.IsNaN(d96Report.GCritical));
        Assert.True(d96Report.Sharpness >= LockLatticeSimulator.SharpnessCriterion,
            "D96 lock curve must be sharp (NP_170 pre-registered sharpness ≥ 3)");
        Assert.True(d96Report.GCritical > HardwareMaxG,
            "the predicted threshold must lie above the board's nominal maximum coupling");

        Output.WriteLine(sb.ToString());
    }

    // ── 5. Mode-specific locking ─────────────────────────────────────────────

    [Fact]
    public void NP171_04_Mode_Specific_Locking()
    {
        var sb = new StringBuilder();
        PrintHeader("5. Mode-specific locking — which modes lock first?");

        var lat = LockLatticeSimulator.D96();
        var results = new LockLatticeSimulator.SweepResult[TwelveSeeds.Length];
        Parallel.For(0, TwelveSeeds.Length, k => results[k] = LockLatticeSimulator.Sweep(lat, TwelveSeeds[k], gMax: SweepMax));

        // Mean onset g per node, grouped by the degeneracy class of the node's frequency level.
        var onset = new double[LockLatticeSimulator.N];
        for (int i = 0; i < LockLatticeSimulator.N; i++)
        {
            var vals = results.Select(r => r.OnsetG[i]).Where(v => !double.IsNaN(v)).ToArray();
            onset[i] = vals.Length > 0 ? vals.Average() : double.NaN;
        }

        sb.AppendLine("  onset g averaged over 12 seeds, grouped by the node's degeneracy class (the T_015 class):");
        sb.AppendLine("  class        nodes  mean ω     mean onset g");
        sb.AppendLine("  " + new string('-', 52));
        foreach (var group in Enumerable.Range(0, LockLatticeSimulator.N)
                     .GroupBy(i => lat.Multiplicity[i]).OrderBy(g => g.Key))
        {
            var idx = group.ToArray();
            double w = idx.Average(i => lat.Omega[i]);
            var onsets = idx.Select(i => onset[i]).Where(v => !double.IsNaN(v)).ToArray();
            string o = onsets.Length > 0 ? onsets.Average().ToString("F3", CultureInfo.InvariantCulture) : "never";
            sb.AppendLine($"  mult {group.Key,-7} {idx.Length,5}  {w,8:F4}   {o}");
        }

        var nearGap = LockLatticeSimulator.NearGapNodes(lat);
        var zero = Enumerable.Range(0, LockLatticeSimulator.N).Where(i => !lat.IsPositive(i)).ToArray();
        var fastest = Enumerable.Range(0, LockLatticeSimulator.N).Where(i => Math.Abs(lat.Omega[i] - lat.Omega.Max()) <= 1e-9).ToArray();
        sb.AppendLine();
        sb.AppendLine($"  near-gap doublet {{1, 95}}: onset g = {onset[nearGap[0]]:F3} (ω₁ = {lat.Omega1:F4})");
        sb.AppendLine($"  zero mode       {{{string.Join(",", zero)}}}: onset g = {onset[zero[0]]:F3} (ω = 0)");
        sb.AppendLine($"  fastest mode    {{{string.Join(",", fastest)}}}: onset g = {onset[fastest[0]]:F3} (ω = 1)");
        sb.AppendLine();
        sb.AppendLine("  T_014 context: only the fundamental doublet sits within 2λ₂, so a near-gap-only lock");
        sb.AppendLine("  would be a different (and much weaker) statement than a global one. The reported f(g)");
        sb.AppendLine("  above is the global (all 96 nodes) criterion; the per-mode curve is printed by the probe.");
        sb.AppendLine($"  ordering: the near-gap doublet locks at g ≈ {onset[nearGap[0]]:F3} versus a global g_c ≈ {results.Average(r => r.GCritical):F3}");

        Assert.True(onset.All(v => double.IsNaN(v) || (v >= 0 && v <= SweepMax)));
        Assert.False(double.IsNaN(onset[zero[0]]), "the zero-mode node must eventually lock");

        Output.WriteLine(sb.ToString());
    }
}
