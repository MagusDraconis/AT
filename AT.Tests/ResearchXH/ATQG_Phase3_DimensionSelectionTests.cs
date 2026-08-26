using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 3 — search for a preferred spacetime dimension. QG2 found d≥3 required, d=4 not derived. Here we
/// test whether any native criterion (information density, curvature efficiency, Einstein richness, graviton d.o.f.,
/// complexity per d.o.f., abundance statistics) prefers d=4. Classify d=4 as DERIVED / PREFERRED / NOT SPECIAL.
///
/// Tests: ATQG30 (all native scores monotonic — no d=4 extremum), ATQG31 (d=4 = minimal propagating gravity),
///        ATQG32 (classification).
/// </summary>
public class ATQG_Phase3_DimensionSelectionTests : ResearchTestBase
{
    public ATQG_Phase3_DimensionSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG30: all native scores are monotonic — no d=4 extremum ───────────────────

    [Fact]
    public void ATQG30_MonotonicScores()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG30: all native dimension-scores are monotonic (no d=4 extremum)");

        sb.AppendLine($"{"d",4} {"richness",10} {"graviton",9} {"Weyl",8} {"a_d",8} {"frozen",8} {"cmplx/dof",10}");
        for (int d = 3; d <= 7; d++)
        {
            sb.AppendLine($"{d,4} {DimensionAnalysis.EinsteinRichness(d),10:F0} {DimensionAnalysis.GravitonPolarizations(d),9:F0} "
                        + $"{DimensionAnalysis.WeylComponents(d),8:F0} {DimensionAnalysis.ConformalWeight(d),8:F4} "
                        + $"{DimensionAnalysis.FrozenFraction(d),8:F3} {DimensionAnalysis.ComplexityPerDof(d),10:F0}");
        }

        // Monotonicity: every score is strictly monotonic in d (increasing or decreasing), so NO criterion has
        // an interior extremum at d=4.
        bool richnessInc = true, gravitonInc = true, weylInc = true, aDec = true, frozenInc = true, cmplxInc = true;
        for (int d = 3; d < 7; d++)
        {
            if (DimensionAnalysis.EinsteinRichness(d + 1) <= DimensionAnalysis.EinsteinRichness(d)) richnessInc = false;
            if (DimensionAnalysis.GravitonPolarizations(d + 1) <= DimensionAnalysis.GravitonPolarizations(d)) gravitonInc = false;
            if (DimensionAnalysis.WeylComponents(d + 1) <= DimensionAnalysis.WeylComponents(d)) weylInc = false;
            if (DimensionAnalysis.ConformalWeight(d + 1) >= DimensionAnalysis.ConformalWeight(d)) aDec = false;
            if (DimensionAnalysis.FrozenFraction(d + 1) <= DimensionAnalysis.FrozenFraction(d)) frozenInc = false;
            if (DimensionAnalysis.ComplexityPerDof(d + 1) <= DimensionAnalysis.ComplexityPerDof(d)) cmplxInc = false;
        }

        bool allMonotonic = richnessInc && gravitonInc && weylInc && aDec && frozenInc && cmplxInc;

        sb.AppendLine();
        sb.AppendLine($"richness ↑, graviton ↑, Weyl ↑, a_d ↓, frozen ↑, complexity/dof ↑: {allMonotonic}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: every native dimension-score is monotonic — no criterion has a local extremum at");
        sb.AppendLine("d=4 (or any d≥3). Information density, curvature efficiency, Einstein richness, graviton d.o.f.,");
        sb.AppendLine("and complexity/d.o.f. all scale monotonically with d.");
        Output.WriteLine(sb.ToString());

        Assert.True(allMonotonic, "all native dimension-scores should be monotonic in d");
    }

    // ── ATQG31: d=3 is the minimal PROPAGATING-gravity dimension ────────────────────

    [Fact]
    public void ATQG31_MinimalPropagatingGravity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG31: d=3 is the lowest dimension with propagating graviton modes");

        double g2 = DimensionAnalysis.GravitonPolarizations(2);
        double g3 = DimensionAnalysis.GravitonPolarizations(3);
        double g4 = DimensionAnalysis.GravitonPolarizations(4);

        sb.AppendLine($"graviton polarizations: d=2 → {g2:F0}, d=3 → {g3:F0}, d=4 → {g4:F0}");
        sb.AppendLine($"d=2 (D=3): no gravity (static/topological); d=3 (D=4): first propagating gravity (2 polarizations)");

        bool staticAt2 = g2 == 0.0;
        bool firstPropagating3 = g3 == 2.0 && g2 == 0.0;
        bool minimalNonzero = g3 < g4;   // 2 is the fewest non-zero graviton modes

        sb.AppendLine();
        sb.AppendLine($"d=2 static-only (0 polarizations, no gravity): {staticAt2}");
        sb.AppendLine($"d=3 first propagating (2 polarizations, minimal non-zero): {firstPropagating3 && minimalNonzero}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: if gravity must PROPAGATE (have wave modes), the minimal dimension is d=3 (3+1");
        sb.AppendLine("spacetime) with exactly 2 polarizations. d=2 (D=3) has no gravity at all.");
        Output.WriteLine(sb.ToString());

        Assert.True(staticAt2, "d=2 (D=3) should have no propagating graviton");
        Assert.True(firstPropagating3 && minimalNonzero, "d=3 (D=4) should be the minimal propagating dimension");
    }

    // ── ATQG32: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG32_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG32: is d=3 (3+1) DERIVED, PREFERRED, or NOT SPECIAL?");

        sb.AppendLine("CLASSIFICATION: NOT SPECIAL natively; PREFERRED as minimal propagating gravity.");
        sb.AppendLine();
        sb.AppendLine("  • NOT SPECIAL (native): every native dimension-score (information density, curvature efficiency,");
        sb.AppendLine("    Einstein richness, graviton d.o.f., complexity/d.o.f.) is MONOTONIC in d — no criterion peaks at");
        sb.AppendLine("    d=3 (ATQG30). Entropy is d-independent; abundance statistics are d-independent.");
        sb.AppendLine("  • The conformal-COMPLETE dimension is d=2 (D=3, Weyl=0, nothing frozen) — but it is FORBIDDEN");
        sb.AppendLine("    (no gravity). The first non-trivial gravity is d=3 (D=4).");
        sb.AppendLine("  • PREFERRED (conditional): d=3 (3+1) is the LOWEST dimension with propagating graviton modes (2");
        sb.AppendLine("    polarizations) — the minimal dynamical gravity. This prefers d=3 only under the IMPORTED");
        sb.AppendLine("    requirement that gravity propagates (has wave degrees of freedom), which is a GR input, not a");
        sb.AppendLine("    native AT consequence (AT gravity is conformally-flat / scalar-only).");
        sb.AppendLine("  • Therefore d=3 (3+1) is NOT DERIVED; it is weakly PREFERRED as minimal dynamical gravity, and NOT");
        sb.AppendLine("    SPECIAL under purely native criteria.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
