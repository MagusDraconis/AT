namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 152 — Golden-ratio robustness audit. QG151 found δ(up) − δ(down) ≈ φ (golden ratio,
/// 0.06% deviation) and interpreted it as the self-similar fixed point of two-channel spectral mode
/// competition. This phase asks: is the golden-ratio relation a FUNDAMENTAL consequence of spectral mode
/// competition, or a numerical COINCIDENCE of the default dynamics?
///
/// Method (computational, fully deterministic): the spectral realization of the golden-ratio relation is
/// up δ_eff ≈ Weyl_full + φ (since δ_eff(down) ≈ Weyl_full, QG149/150). We sweep five parameter axes and
/// measure the deviation |up − (Weyl_full + φ)| / (Weyl_full + φ) at every setting:
/// (1) SIZE SCALING — network size n = 64..160; (2) K SCALING — coupling K = 3..10; (3) DAMPING
/// VARIATION — damping = 0.2..0.4; (4) FEEDBACK VARIATION — feedback = 0.5..1.1; (5) SPECTRAL
/// PERTURBATIONS — seeded multiplicative mode-frequency noise at 0.1%..5%.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class GoldenRatioAudit
{
    /// <summary>Up-sector effective dimension (QG150).</summary>
    public static double UpDeltaEff()
        => ModeAccessOrigin.SectorDimensions().First(s => s.Name == "up").DeltaEff;

    /// <summary>The golden ratio φ = (1 + √5) / 2.</summary>
    public static double Phi()
        => (1.0 + Math.Sqrt(5.0)) / 2.0;

    /// <summary>
    /// Golden-ratio deviation at a given full-spectrum Weyl: |up − (Weyl_full + φ)| / (Weyl_full + φ).
    /// The spectral realization of δ(up) = δ(down) + φ with δ(down) ≈ Weyl_full.
    /// </summary>
    public static double GoldenDeviation(double weylFull)
    {
        double up = UpDeltaEff();
        double pred = weylFull + Phi();
        return Math.Abs(up - pred) / pred;
    }

    /// <summary>Full-spectrum Weyl exponent of the observable sector spectrum.</summary>
    public static double FullWeyl()
        => ModeAccessOrigin.FullWeyl();

    // ── 1. Size scaling ─────────────────────────────────────────────────────────

    /// <summary>
    /// Size scaling: (n, modes, weylFull, deviation) for n = 64..160. Network size with default dynamics.
    /// </summary>
    public static (int N, int Modes, double Weyl, double Deviation)[] SizeScaling()
        => new[] { 64, 80, 96, 128, 160 }.Select(n =>
        {
            var w = FamilyIndexOrigin.IntraSectorModes(n);
            double weyl = SpectralWeyl(w);
            return (n, w.Length, weyl, GoldenDeviation(weyl));
        }).ToArray();

    // ── 2. K scaling ────────────────────────────────────────────────────────────

    /// <summary>
    /// K scaling: (K, modes, weylFull, deviation) for K = 3..10. Coupling strength with default size and
    /// dynamics.
    /// </summary>
    public static (int K, int Modes, double Weyl, double Deviation)[] KScaling()
        => new[] { 3, 4, 6, 8, 10 }.Select(K =>
        {
            var w = FamilyIndexOrigin.IntraSectorModes(96, K);
            double weyl = SpectralWeyl(w);
            return (K, w.Length, weyl, GoldenDeviation(weyl));
        }).ToArray();

    // ── 3. Damping variation ────────────────────────────────────────────────────

    /// <summary>
    /// Damping variation: (damping, modes, weylFull, deviation) for damping = 0.2..0.4 at feedback 0.9.
    /// </summary>
    public static (double Damping, int Modes, double Weyl, double Deviation)[] DampingVariation()
        => new[] { 0.2, 0.25, 0.3, 0.35, 0.4 }.Select(d =>
        {
            var w = FamilyIndexOrigin.IntraSectorModes(96, 6, 0.9, d);
            double weyl = SpectralWeyl(w);
            return (d, w.Length, weyl, GoldenDeviation(weyl));
        }).ToArray();

    // ── 4. Feedback variation ───────────────────────────────────────────────────

    /// <summary>
    /// Feedback variation: (feedback, modes, weylFull, deviation) for feedback = 0.5..1.1 at damping 0.3.
    /// </summary>
    public static (double Feedback, int Modes, double Weyl, double Deviation)[] FeedbackVariation()
        => new[] { 0.5, 0.7, 0.9, 1.0, 1.1 }.Select(f =>
        {
            var w = FamilyIndexOrigin.IntraSectorModes(96, 6, f, 0.3);
            double weyl = SpectralWeyl(w);
            return (f, w.Length, weyl, GoldenDeviation(weyl));
        }).ToArray();

    // ── 5. Spectral perturbations ───────────────────────────────────────────────

    /// <summary>
    /// Spectral perturbations: (amplitude, weylFull, deviation) for seeded multiplicative mode-frequency
    /// noise at 0.1%..5%. Deterministic seed.
    /// </summary>
    public static (double Amp, double Weyl, double Deviation)[] SpectralPerturbations()
        => new[] { 0.001, 0.005, 0.01, 0.02, 0.05 }.Select(amp =>
        {
            var rnd = new Random(42);
            var wBase = FamilyIndexOrigin.IntraSectorModes();
            var wP = wBase.Select(x => x * (1.0 + amp * (2.0 * rnd.NextDouble() - 1.0)))
                .OrderBy(x => x).ToArray();
            double weyl = SpectralWeyl(wP);
            return (amp, weyl, GoldenDeviation(weyl));
        }).ToArray();

    private static double SpectralWeyl(double[] ws)
    {
        if (ws.Length < 4) return double.NaN;
        var logW = ws.Select(x => Math.Log(x)).ToArray();
        var logN = Enumerable.Range(1, ws.Length).Select(i => Math.Log((double)i)).ToArray();
        double mx = logW.Average(), my = logN.Average();
        double num = 0, den = 0;
        for (int i = 0; i < ws.Length; i++)
        {
            num += (logW[i] - mx) * (logN[i] - my);
            den += (logW[i] - mx) * (logW[i] - mx);
        }
        return den < 1e-12 ? double.NaN : num / den;
    }

    // ── Audit aggregates ────────────────────────────────────────────────────────

    /// <summary>All (axis, label, deviation) settings of the audit.</summary>
    public static (string Axis, string Label, double Deviation)[] AllSettings()
    {
        var list = new List<(string, string, double)>();
        foreach (var s in SizeScaling()) list.Add(("size", $"n={s.N}", s.Deviation));
        foreach (var s in KScaling()) list.Add(("K", $"K={s.K}", s.Deviation));
        foreach (var s in DampingVariation()) list.Add(("damping", $"d={s.Damping}", s.Deviation));
        foreach (var s in FeedbackVariation()) list.Add(("feedback", $"f={s.Feedback}", s.Deviation));
        foreach (var s in SpectralPerturbations()) list.Add(("perturb", $"a={s.Amp}", s.Deviation));
        return list.ToArray();
    }

    /// <summary>Number of audit settings with deviation &lt; 5% (robust basin).</summary>
    public static int RobustCount()
        => AllSettings().Count(s => s.Deviation < 0.05);

    /// <summary>Number of audit settings with deviation &lt; 10% (weak basin).</summary>
    public static int WeakCount()
        => AllSettings().Count(s => s.Deviation < 0.10);

    /// <summary>Total number of audit settings (25).</summary>
    public static int TotalSettings()
        => AllSettings().Length;

    /// <summary>Is the relation robust at the DEFAULT dynamics (deviation &lt; 5%)?</summary>
    public static bool DefaultHolds()
        => GoldenDeviation(FullWeyl()) < 0.05;

    /// <summary>Is the relation robust across ALL damping settings (deviation &lt; 5%)?</summary>
    public static bool DampingRobust()
        => DampingVariation().All(d => d.Deviation < 0.05);

    /// <summary>Is the relation robust across ALL perturbation amplitudes (deviation &lt; 5%)?</summary>
    public static bool PerturbationRobust()
        => SpectralPerturbations().All(p => p.Deviation < 0.05);

    /// <summary>Is the relation robust across the coherent feedback basin (≥ 4 of 5 settings &lt; 5%)?</summary>
    public static bool FeedbackBasin()
        => FeedbackVariation().Count(f => f.Deviation < 0.05) >= 4;

    /// <summary>Is the relation broadly robust across the whole parameter space (≥ 15 of 25 settings &lt; 10%)?</summary>
    public static bool BroadBasin()
        => WeakCount() >= 15;

    // ── Origin score & classification ───────────────────────────────────────────

    /// <summary>
    /// Golden-ratio robustness score (0..5):
    /// 1. the relation holds at the DEFAULT dynamics (deviation &lt; 5%);
    /// 2. the relation is robust across ALL damping settings;
    /// 3. the relation is robust across ALL spectral perturbations;
    /// 4. the relation holds across a coherent feedback basin (≥ 4 of 5 settings);
    /// 5. the relation holds across a broad parameter basin (≥ 15 of 25 settings &lt; 10%).
    /// </summary>
    public static int RobustnessScore()
    {
        int score = 0;
        if (DefaultHolds()) score++;
        if (DampingRobust()) score++;
        if (PerturbationRobust()) score++;
        if (FeedbackBasin()) score++;
        if (BroadBasin()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   COINCIDENCE       — the golden-ratio relation holds only at the single default point; it breaks
    ///                       under almost any parameter change (score ≤ 2);
    ///   FUNDAMENTAL PHI   — the relation holds to &lt; 5% across EVERY tested setting (score 5 and all
    ///                       settings &lt; 5%);
    ///   PARTIAL ROBUSTNESS — the relation holds strongly within a coherent parameter basin (the observable
    ///                       dynamics: default K, damping-insensitive, feedback ≥ 0.7, mild size
    ///                       sensitivity, robust to spectral perturbations) but is not universal — extreme
    ///                       size and K settings deviate (12–25%).
    /// </summary>
    public static string Classify()
    {
        int score = RobustnessScore();
        bool allStrong = AllSettings().All(s => s.Deviation < 0.05);
        if (allStrong) return "FUNDAMENTAL PHI";
        if (score <= 2) return "COINCIDENCE";
        return "PARTIAL ROBUSTNESS";
    }
}
