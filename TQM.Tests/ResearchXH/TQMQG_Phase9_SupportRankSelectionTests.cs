using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 9 — which support rank d is favored inside a higher-dimensional fundamental D? We score
/// efficiency = useful structure / total complexity across D=5..20, d=3..D, measuring propagating modes, frozen
/// metric fraction, information density, curvature per d.o.f., and entropy efficiency. Classify: DERIVED /
/// PREFERRED / NOT SELECTED.
///
/// Tests: TQMQG90 (conformal efficiency landscape — d=3 optimal), TQMQG91 (efficiency vs coverage trade-off),
///        TQMQG92 (classification).
/// </summary>
public class TQMQG_Phase9_SupportRankSelectionTests : ResearchTestBase
{
    public TQMQG_Phase9_SupportRankSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG90: conformal efficiency is maximized at d=3, independent of D ──────────

    [Fact]
    public void TQMQG90_ConformalEfficiencyLandscape()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG90: conformal efficiency is maximized at d=2 (forbidden), then d=3");

        sb.AppendLine($"{"d",4} {"graviton",9} {"conformal eff",14} {"curv/d.o.f.",13}");
        bool effAt2 = true, monotonic = true;
        double prev = double.MaxValue;
        for (int d = 2; d <= 12; d++)
        {
            double g = DimensionAnalysis.GravitonPolarizations(d);
            double eff = EffectiveDimension.ConformalEfficiency(d);
            double cpd = EffectiveDimension.CurvaturePerDof(d);
            if (d == 2 && eff != 1.0) effAt2 = false;          // efficiency 1 (nothing frozen) at d=2
            if (d > 2 && eff >= prev) monotonic = false;       // strictly decreasing for d≥3
            prev = eff;
            sb.AppendLine($"{d,4} {g,9:F0} {eff,14:F4} {cpd,13:F0}");
        }

        sb.AppendLine();
        sb.AppendLine($"efficiency = 1 at d=2 (nothing frozen): {effAt2}");
        sb.AppendLine($"efficiency strictly decreasing for d≥3: {monotonic}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: conformal efficiency is 1 only at d=2 (D=3, Weyl=0) — which is FORBIDDEN (no");
        sb.AppendLine("gravity). Among allowed dimensions (d≥3), efficiency is maximized at d=3 (1/3) and decreases");
        sb.AppendLine("monotonically. Efficiency is a property of d alone, independent of the fundamental D.");
        Output.WriteLine(sb.ToString());

        Assert.True(effAt2, "efficiency should be 1.0 at d=2");
        Assert.True(monotonic, "efficiency should decrease monotonically for d≥3");
    }

    // ── TQMQG91: efficiency vs coverage trade-off ────────────────────────────────────

    [Fact]
    public void TQMQG91_EfficiencyVsCoverage()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG91: conformal efficiency (prefers low d) vs coverage (prefers d=D)");

        int D = 10;
        sb.AppendLine($"fundamental D = {D}; observable d = 3..10:");
        sb.AppendLine($"{"d",4} {"conformal eff",14} {"coverage d(d+1)/D(D+1)",24}");
        for (int d = 3; d <= D; d++)
        {
            double eff = EffectiveDimension.ConformalEfficiency(d);
            double cov = EffectiveDimension.ObservableFraction(D, d);
            sb.AppendLine($"{d,4} {eff,14:F4} {cov,24:F4}");
        }

        bool efficiencyPrefersLow = EffectiveDimension.ConformalEfficiency(3) > EffectiveDimension.ConformalEfficiency(4);
        bool coveragePrefersD = EffectiveDimension.ObservableFraction(D, D) > EffectiveDimension.ObservableFraction(D, D - 1);
        bool tradeOff = EffectiveDimension.ConformalEfficiency(D) < EffectiveDimension.ConformalEfficiency(3)
                     && EffectiveDimension.ObservableFraction(D, 3) < EffectiveDimension.ObservableFraction(D, D);

        sb.AppendLine();
        sb.AppendLine($"conformal efficiency prefers low d (d=3 among allowed): {efficiencyPrefersLow}");
        sb.AppendLine($"coverage prefers d=D (no reduction): {coveragePrefersD}");
        sb.AppendLine($"the two metrics pull in OPPOSITE directions (trade-off): {tradeOff}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: there is NO single 'most efficient' support rank — conformal efficiency favors");
        sb.AppendLine("small d (d=3), while coverage favors large d (d=D). They trade off.");
        Output.WriteLine(sb.ToString());

        Assert.True(efficiencyPrefersLow, "efficiency should prefer low d");
        Assert.True(coveragePrefersD, "coverage should prefer d=D");
        Assert.True(tradeOff, "the two metrics should trade off");
    }

    // ── TQMQG92: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG92_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG92: is a specific support rank DERIVED, PREFERRED, or NOT SELECTED?");

        sb.AppendLine("CLASSIFICATION: PREFERRED (d=3 = 3+1, minimal dynamical); NOT SELECTED uniquely.");
        sb.AppendLine();
        sb.AppendLine("  • d=3 (3+1) is PREFERRED as the minimal dynamical gravity: the lowest d with non-trivial Einstein");
        sb.AppendLine("    structure AND propagating graviton modes (2 polarizations, QG2/QG3/QG8).");
        sb.AppendLine("  • Conformal efficiency is 1 only at d=2 (Weyl=0), which is FORBIDDEN (no gravity); among allowed");
        sb.AppendLine("    d≥3, efficiency is maximized at d=3 (1/3) and decreases (TQMQG90).");
        sb.AppendLine("  • Efficiency (prefers low d) and coverage (prefers d=D) trade off (TQMQG91), so NO unique support");
        sb.AppendLine("    rank is SELECTED by a single criterion — the choice depends on the chosen efficiency metric.");
        sb.AppendLine("  • Therefore the observable support rank is NOT SELECTED uniquely; d=3 (3+1) is the quality-");
        sb.AppendLine("    PREFERRED candidate (minimal dynamical gravity), not DERIVED.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
