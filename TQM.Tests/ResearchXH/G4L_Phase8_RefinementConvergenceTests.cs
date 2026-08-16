using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 8 — refinement convergence. Starting from the Phase-7 best native Lorentzian
/// operator H = R1 + A3 + D (negated local-degree diagonal), ask whether refining the causal
/// set (N = 72 → 506) systematically reduces the residual Feynman tail and drives the operator
/// toward the BDG reference.
///
/// Measured per grid: leakage, directionality, KS distance to BDG, and the positive/negative
/// mode ratio. Classified as convergent / plateau / divergent.
///
/// Tests: G4-L80 (tail vs N), G4-L81 (KS + mode ratio vs N), G4-L82 (classification).
/// </summary>
public class G4L_Phase8_RefinementConvergenceTests : ResearchTestBase
{
    public G4L_Phase8_RefinementConvergenceTests(ITestOutputHelper o) : base(o) { }

    // Diamond-shaped grids (tMax = 2·xMax − 1): the causal diamond is complete around the
    // central source. N = (tMax+1)·(2·xMax+1) ≈ the requested 72, 110, 150, 250, 500.
    private static readonly (int tMax, int xMax)[] Grids =
    {
        (7, 4),   // N = 72
        (9, 5),   // N = 110
        (11, 6),  // N = 156 (≈150)
        (15, 8),  // N = 272 (≈250)
        (21, 11), // N = 506 (≈500)
    };

    private static int CenterIndex(int tMax, int xMax) => (tMax / 2) * (2 * xMax + 1) + xMax;

    private readonly record struct Rm(int N, double leak, double dir, double ks, double pratio);

    private static Rm Measure(int tMax, int xMax)
    {
        var cs = CausalSet.BuildGrid(tMax, xMax);
        int tc = tMax / 2;
        var h = LorentzianOperator.NativeLorentzian(cs);
        var resp = LorentzianOperator.GreenResponse(h, CenterIndex(tMax, xMax));
        var m = LorentzianOperator.GreenResponseMetrics(cs, resp, tc, 0);
        double dir = m.past + m.future == 0.0 ? 0.5 : m.future / (m.past + m.future);
        var ev = SpectralCurvature.GeneralEigenvalues(h);
        var sig = LorentzianOperator.Signature(ev);
        double pratio = sig.neg > 0 ? (double)sig.pos / sig.neg : double.NaN;
        var bdgSym = LorentzianOperator.Eigenvalues(LorentzianOperator.BdgReference(cs));
        double ks = SpectralCurvature.KolmogorovSmirnov(ev, bdgSym);
        return new Rm(cs.Count, m.leak, dir, ks, pratio);
    }

    private static bool MonotoneDecreasing(double[] v)
    {
        for (int i = 1; i < v.Length; i++)
            if (v[i] > v[i - 1] + 1e-9) return false;
        return true;
    }

    private static string Classify(double first, double last)
    {
        double d = last - first;
        if (d < -0.05) return "convergent";
        if (d > 0.05) return "divergent";
        return "plateau";
    }

    // ── G4-L80: does the Feynman tail decrease under refinement? ─────────────────────────

    [Fact]
    public void G4_L80_RefinementReducesTail()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L80: does refinement reduce the residual Feynman tail?");

        sb.AppendLine($"{"grid",-8} {"N",6} {"leakage",9} {"direction",11}");
        var rows = new List<Rm>();
        foreach (var (tMax, xMax) in Grids)
        {
            var r = Measure(tMax, xMax);
            rows.Add(r);
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {r.N,6} {r.leak,9:F3} {r.dir,11:F3}");
        }

        var leaks = rows.Select(r => r.leak).ToArray();
        sb.AppendLine();
        sb.AppendLine($"leakage: {string.Join(" → ", leaks.Select(l => l.ToString("F3")))}");
        sb.AppendLine($"monotone decreasing: {MonotoneDecreasing(leaks)}");
        sb.AppendLine($"first→last: {leaks[0]:F3} → {leaks[^1]:F3} (Δ = {leaks[^1] - leaks[0]:+0.000})");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the Feynman tail does NOT systematically decrease — it oscillates");
        sb.AppendLine("around ~0.41–0.55 and persists. The residual tail is intrinsic, not a refinement artifact.");
        Output.WriteLine(sb.ToString());

        // Honest finding: the tail does not vanish and does not decrease monotonically.
        Assert.False(MonotoneDecreasing(leaks), "unexpected: leakage decreases monotonically (convergent)");
        Assert.True(leaks[^1] > 0.3, $"tail unexpectedly vanishes under refinement (last leak {leaks[^1]:F3})");
    }

    // ── G4-L81: KS distance and mode ratio vs N ──────────────────────────────────────────

    [Fact]
    public void G4_L81_ConvergenceToBdg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L81: KS distance to BDG and mode ratio under refinement");

        sb.AppendLine($"{"grid",-8} {"N",6} {"KS→BDG",9} {"pos/neg",9}");
        var rows = new List<Rm>();
        foreach (var (tMax, xMax) in Grids)
        {
            var r = Measure(tMax, xMax);
            rows.Add(r);
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {r.N,6} {r.ks,9:F4} {r.pratio,9:F3}");
        }

        var ks = rows.Select(r => r.ks).ToArray();
        var pr = rows.Select(r => r.pratio).ToArray();
        sb.AppendLine();
        sb.AppendLine($"KS: {string.Join(" → ", ks.Select(k => k.ToString("F4")))}");
        sb.AppendLine($"KS first→last: {ks[0]:F4} → {ks[^1]:F4} (Δ = {ks[^1] - ks[0]:+0.000})");
        sb.AppendLine($"mode ratio (pos/neg): {pr[0]:F3} → {pr[^1]:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: KS drifts only weakly (non-monotonic, ~10% net) and stays far from 0 —");
        sb.AppendLine("the operator does NOT converge to BDG. The mode ratio stays < 1 (both signs persist → indefinite).");
        Output.WriteLine(sb.ToString());

        // Honest finding: KS to BDG does not approach 0 (no convergence) and the spectrum stays indefinite.
        Assert.True(ks[^1] > 0.15, $"KS unexpectedly converges to BDG (last KS {ks[^1]:F4})");
        Assert.True(pr.All(p => p > 0.0), "spectrum lost indefiniteness under refinement");
    }

    // ── G4-L82: classify convergent / plateau / divergent ────────────────────────────────

    [Fact]
    public void G4_L82_ClassifyConvergence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L82: classification of refinement behaviour");

        sb.AppendLine($"{"grid",-8} {"N",6} {"leakage",9} {"direction",11} {"KS→BDG",9} {"pos/neg",9}");
        var rows = new List<Rm>();
        foreach (var (tMax, xMax) in Grids)
        {
            var r = Measure(tMax, xMax);
            rows.Add(r);
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {r.N,6} {r.leak,9:F3} {r.dir,11:F3} {r.ks,9:F4} {r.pratio,9:F3}");
        }

        var leaks = rows.Select(r => r.leak).ToArray();
        var ks = rows.Select(r => r.ks).ToArray();
        string leakCls = Classify(leaks[0], leaks[^1]);
        string ksCls = Classify(ks[0], ks[^1]);

        sb.AppendLine();
        sb.AppendLine($"leakage: {leaks[0]:F3} → {leaks[^1]:F3} (Δ = {leaks[^1] - leaks[0]:+0.000}) → {leakCls.ToUpperInvariant()}.");
        sb.AppendLine($"KS:      {ks[0]:F4} → {ks[^1]:F4} (Δ = {ks[^1] - ks[0]:+0.000}) → {ksCls.ToUpperInvariant()}.");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {leakCls.ToUpperInvariant()} (tail) / {ksCls.ToUpperInvariant()} (KS).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: PLATEAU — refinement neither eliminates the Feynman tail nor drives the");
        sb.AppendLine("operator to BDG. The residual ~40–55% leakage is intrinsic to the native symmetric");
        sb.AppendLine("off-diagonal; closing it requires the BDG −2 diagonal (outside the native constraint).");
        Output.WriteLine(sb.ToString());

        // Honest classification: the tail plateaus (no systematic decrease toward 0).
        Assert.Equal("plateau", leakCls);
    }
}
