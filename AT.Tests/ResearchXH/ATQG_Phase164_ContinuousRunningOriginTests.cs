using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 164 — Continuous running origin. QG163 established discrete running on the octave
/// ladder (rungs 4 → 8 → 95). This phase asks HOW continuous running emerges from the discrete D96
/// octave structure — with no fitted beta functions, using only D96 spectral geometry.
///
/// Tests: ATQG1640 (partial mode activation), ATQG1641 (linear-in-doublet beta flow + interpolation),
/// ATQG1642 (log-like running + continuum limit + classification).
/// </summary>
public class ATQG_Phase164_ContinuousRunningOriginTests : ResearchTestBase
{
    public ATQG_Phase164_ContinuousRunningOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1640_PartialModeActivation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1640: partial mode activation — continuous denominators");

        sb.AppendLine("ASSUMPTIONS: activating modes one-by-one (N = number of lowest-frequency modes)");
        sb.AppendLine("evolves the coupling denominators continuously; each activated Z2 doublet adds");
        sb.AppendLine("+2 to Σm, +1 to #doublets, +√2 to Σ√m.");
        sb.AppendLine();
        sb.AppendLine("MODE-BY-MODE ACTIVATION (fine staircase):");
        sb.AppendLine("N | Σm | #doublets | Σ√m | 1/α_em | α_weak | α_strong");
        foreach (int n in new[] { 4, 8, 12, 16, 32, 64, 95 })
        {
            int sumM = ContinuousRunningOrigin.ActiveModes(n);
            int d = ContinuousRunningOrigin.ActivatedDoublets(n);
            double s = ContinuousRunningOrigin.ActiveNeutralMoment(n);
            sb.AppendLine($"  {n} | {sumM} | {d} | {s:F2} | {sumM + d:F1} | {3.0 / sumM:F5} | {8.0 / s:F5}");
        }
        sb.AppendLine();
        sb.AppendLine("The denominators evolve continuously as modes activate — the discrete octave");
        sb.AppendLine("rungs (4 → 8 → 95) are a coarse sampling of a fine staircase.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, ContinuousRunningOrigin.ActivatedDoublets(8));
        Assert.True(ContinuousRunningOrigin.ActiveModes(95) == 95, "full activation has 95 modes");
        Assert.True(ContinuousRunningOrigin.ActiveModes(64) < ContinuousRunningOrigin.ActiveModes(95),
            "denominators grow with activation");
    }

    [Fact]
    public void ATQG1641_LinearBetaFlowAndInterpolation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1641: linear-in-doublet beta flow and fractional interpolation");

        sb.AppendLine("ASSUMPTIONS: in the doublet-dominated regime the inverse couplings are LINEAR in");
        sb.AppendLine("G = activated doublet count: 1/α_em = Σm + #doublets = 2G + G = 3G; 1/α_weak = Σm/3");
        sb.AppendLine("= 2G/3; 1/α_strong = Σ√m/8 = (√2/8)·G.");
        sb.AppendLine();
        sb.AppendLine("EMERGENT BETA COEFFICIENTS (D96 constants, no fitting):");
        sb.AppendLine($"  b_em    = 3      (1/α_em = 3·G)");
        sb.AppendLine($"  b_weak  = 2/3    (1/α_weak = (2/3)·G)");
        sb.AppendLine($"  b_strong = √2/8  (1/α_strong = (√2/8)·G)");
        sb.AppendLine($"  linear in doublet count (exact at low activation): {ContinuousRunningOrigin.LinearInDoubletCount()}");
        sb.AppendLine();
        sb.AppendLine("FRACTIONAL INTERPOLATION (continuous flow):");
        sb.AppendLine("L | 1/α_em | α_weak | α_strong");
        foreach (var (l, emInv, wk, st) in ContinuousRunningOrigin.ContinuousCouplings())
            sb.AppendLine($"  {l:F1} | {emInv:F1} | {wk:F5} | {st:F5}");
        sb.AppendLine($"  interpolated flow monotone: {ContinuousRunningOrigin.InterpolatedFlowMonotone()}");
        sb.AppendLine();
        sb.AppendLine("  linear interpolation between adjacent modes gives CONTINUOUS α(L) — the");
        sb.AppendLine("  discrete octave rungs become a smooth flow (spectral interpolation).");
        Output.WriteLine(sb.ToString());

        Assert.True(ContinuousRunningOrigin.LinearInDoubletCount(), "running should be linear in G");
        Assert.Equal(3.0, ContinuousRunningOrigin.BetaEm());
        Assert.True(Math.Abs(ContinuousRunningOrigin.BetaWeak() - 2.0 / 3.0) < 1e-12, "b_weak = 2/3");
        Assert.True(ContinuousRunningOrigin.InterpolatedFlowMonotone(), "interpolated flow monotone");
    }

    [Fact]
    public void ATQG1642_LogLikeRunningAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1642: log-like running and classification");

        sb.AppendLine("ASSUMPTIONS: the spectral scale is logarithmic (octave ladder: each octave doubles");
        sb.AppendLine("frequency); the QFT beta form is 1/α(E) = 1/α(E0) + b·ln(E/E0).");
        sb.AppendLine();
        sb.AppendLine("LOG-SCALE CONTINUITY (1/α_em vs log2(E)):");
        foreach (var (logE, n, emInv, wk, st) in ContinuousRunningOrigin.LogScaleCouplings())
            sb.AppendLine($"  log2(E)={logE:F2}: N={n}, 1/α_em={emInv:F1}, α_weak={wk:F5}, α_strong={st:F5}");
        sb.AppendLine();
        sb.AppendLine($"  log-like running (1/α grows with log E): {ContinuousRunningOrigin.LogLikeRunning()}");
        var (lowStep, fullStep) = ContinuousRunningOrigin.ContinuumSteps();
        sb.AppendLine($"  continuum limit: relative step {lowStep:F3} (N=4) → {fullStep:F4} (N=95)");
        sb.AppendLine($"  → the staircase becomes a continuous flow as N grows.");
        sb.AppendLine();
        sb.AppendLine("  the QFT beta-function form 1/α(E) = 1/α(E0) + b·ln(E/E0) is recovered as an");
        sb.AppendLine("  EMERGENT spectral flow: the inverse couplings grow with the logarithmic scale,");
        sb.AppendLine("  with the D96 constants (3, 2/3, √2/8) as the beta coefficients.");
        sb.AppendLine();
        int score = ContinuousRunningOrigin.OriginScore();
        string cls = ContinuousRunningOrigin.Classify();
        sb.AppendLine($"Continuous-running-origin score (0..5): {score}");
        sb.AppendLine($"  +1 partial activation (fine staircase): {ContinuousRunningOrigin.ActivatedDoublets(8) == 4}");
        sb.AppendLine($"  +1 linear in doublet count: {ContinuousRunningOrigin.LinearInDoubletCount()}");
        sb.AppendLine($"  +1 interpolated flow monotone: {ContinuousRunningOrigin.InterpolatedFlowMonotone()}");
        sb.AppendLine($"  +1 log-like running: {ContinuousRunningOrigin.LogLikeRunning()}");
        sb.AppendLine($"  +1 continuum limit: {lowStep > fullStep}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: partial mode activation evolves the denominators");
        sb.AppendLine("    continuously (fine staircase within each octave rung).");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the full mechanism holds — linear-in-G beta flow,");
        sb.AppendLine("    fractional interpolation, log-like running, continuum limit.");
        sb.AppendLine("  • CONTINUOUS ORIGIN accepted: continuous running EMERGES from D96 spectral");
        sb.AppendLine("    geometry: partial mode activation gives a fine staircase; in the doublet regime");
        sb.AppendLine("    the inverse couplings are LINEAR in the activated doublet count G(E) with");
        sb.AppendLine("    D96-fixed coefficients (1/α_em = 3G, 1/α_weak = (2/3)G, 1/α_strong = (√2/8)G);");
        sb.AppendLine("    fractional interpolation smooths the octave rungs into a continuous flow; and");
        sb.AppendLine("    the log-scale running recovers the QFT beta-function form as an emergent");
        sb.AppendLine("    spectral flow — with no fitted beta functions.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "continuous-running-origin score should be strong");
        Assert.Equal("CONTINUOUS ORIGIN", cls);
    }
}
