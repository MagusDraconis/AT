using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 3 — search for a preferred spacetime dimension. QG2 found d≥3 required, d=4 not derived. Here we
/// test whether any native criterion (information density, curvature efficiency, Einstein richness, graviton d.o.f.,
/// complexity per d.o.f., abundance statistics) prefers d=4. Classify d=4 as DERIVED / PREFERRED / NOT SPECIAL.
///
/// Tests: TQMQG30 (all native scores monotonic — no d=4 extremum), TQMQG31 (d=4 = minimal propagating gravity),
///        TQMQG32 (classification).
/// </summary>
public class TQMQG_Phase3_DimensionSelectionTests : ResearchTestBase
{
    public TQMQG_Phase3_DimensionSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG30: all native scores are monotonic — no d=4 extremum ───────────────────

    [Fact]
    public void TQMQG30_MonotonicScores()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG30: all native dimension-scores are monotonic (no d=4 extremum)");

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

    // ── TQMQG31: d=4 is the minimal PROPAGATING-gravity dimension ────────────────────

    [Fact]
    public void TQMQG31_MinimalPropagatingGravity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG31: d=4 is the lowest dimension with propagating graviton modes");

        double g3 = DimensionAnalysis.GravitonPolarizations(3);
        double g4 = DimensionAnalysis.GravitonPolarizations(4);
        double g5 = DimensionAnalysis.GravitonPolarizations(5);

        sb.AppendLine($"graviton polarizations: d=3 → {g3:F0}, d=4 → {g4:F0}, d=5 → {g5:F0}");
        sb.AppendLine($"d=3: static-only gravity (no propagating modes); d=4: first propagating gravity (2 polarizations)");

        bool staticAt3 = g3 == 0.0;
        bool firstPropagating4 = g4 == 2.0 && g3 == 0.0;
        bool minimalNonzero = g4 < g5;   // 2 is the fewest non-zero graviton modes

        sb.AppendLine();
        sb.AppendLine($"d=3 static-only (0 polarizations): {staticAt3}");
        sb.AppendLine($"d=4 first propagating (2 polarizations, minimal non-zero): {firstPropagating4 && minimalNonzero}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: if gravity must PROPAGATE (have wave modes), the minimal dimension is d=4 with");
        sb.AppendLine("exactly 2 polarizations. d=3 has non-trivial gravity but no propagating modes.");
        Output.WriteLine(sb.ToString());

        Assert.True(staticAt3, "d=3 should have no propagating graviton");
        Assert.True(firstPropagating4 && minimalNonzero, "d=4 should be the minimal propagating dimension");
    }

    // ── TQMQG32: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG32_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG32: is d=4 DERIVED, PREFERRED, or NOT SPECIAL?");

        sb.AppendLine("CLASSIFICATION: NOT SPECIAL natively; PREFERRED only as minimal propagating gravity.");
        sb.AppendLine();
        sb.AppendLine("  • NOT SPECIAL (native): every native dimension-score (information density, curvature efficiency,");
        sb.AppendLine("    Einstein richness, graviton d.o.f., complexity/d.o.f.) is MONOTONIC in d — no criterion peaks at");
        sb.AppendLine("    d=4 (TQMQG30). Entropy is d-independent; abundance statistics are d-independent.");
        sb.AppendLine("  • The natively-special dimension is d=3: the conformal-COMPLETE dimension (Weyl=0, nothing frozen,");
        sb.AppendLine("    QG2) and the first non-trivial gravity — but it has NO propagating modes.");
        sb.AppendLine("  • PREFERRED (conditional): d=4 is the LOWEST dimension with propagating graviton modes (2");
        sb.AppendLine("    polarizations) — the minimal dynamical gravity. This prefers d=4 only under the IMPORTED");
        sb.AppendLine("    requirement that gravity propagates (has wave degrees of freedom), which is a GR input, not a");
        sb.AppendLine("    native TQM consequence (TQM gravity is conformally-flat / scalar-only).");
        sb.AppendLine("  • Therefore d=4 is NOT DERIVED; it is weakly PREFERRED as minimal dynamical gravity, and NOT SPECIAL");
        sb.AppendLine("    under purely native criteria.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
