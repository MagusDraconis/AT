using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 90 — Origin of gauge sector splitting. Determines why the link decomposes into three gauge sectors
/// instead of one unified gauge structure. Classify: DERIVED / PARTIAL / POSTULATED.
///
/// Tests: ATQG900 (representation hierarchy + minimal link info), ATQG901 (symmetry breaking + relations +
/// unification), ATQG902 (classification).
/// </summary>
public class ATQG_Phase90_GaugeSectorSplittingTests : ResearchTestBase
{
    public ATQG_Phase90_GaugeSectorSplittingTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG900: representation hierarchy, minimal link information ───────────────

    [Fact]
    public void ATQG900_HierarchyAndMinimalLink()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG900: three sectors on one link — product structure");

        bool independent = GaugeSectorSplitting.ThreeSectorsAreIndependentPostulates();
        bool shared = GaugeSectorSplitting.SectorsShareOneLink();
        bool product = GaugeSectorSplitting.GaugeGroupIsProduct();

        sb.AppendLine($"three sectors (θ, S, C) are INDEPENDENT postulates: {independent}");
        sb.AppendLine($"they share ONE carrier (the single link, QG68): {shared}");
        sb.AppendLine($"total gauge structure is PRODUCT U(1)×SU(2)×SU(3): {product}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: θ (charge), S (spin), C (color) act on DIFFERENT internal spaces, so the gauge group is");
        sb.AppendLine("the product of three groups. Structural unity (one link) does not force a single gauge group.");
        Output.WriteLine(sb.ToString());

        Assert.True(independent, "three independent postulates");
        Assert.True(shared, "they share one link");
        Assert.True(product, "gauge group is a product");
    }

    // ── ATQG901: symmetry breaking, relations, unified candidates ─────────────────

    [Fact]
    public void ATQG901_SymmetryAndUnification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG901: is unification native?");

        bool unifiedNative = GaugeSectorSplitting.UnifiedGroupNative();
        bool splitDerived = GaugeSectorSplitting.SplittingDerived();
        bool unifyDerived = GaugeSectorSplitting.UnificationDerived();

        sb.AppendLine($"grand-unified group (SU(5)/SO(10)) NATIVE: {unifiedNative}");
        sb.AppendLine($"three-sector SPLITTING derived: {splitDerived}");
        sb.AppendLine($"UNIFICATION into one group derived: {unifyDerived}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: no symmetry-breaking chain or relation derives a unified group. A GUT is an ADDITIONAL");
        sb.AppendLine("postulate. Neither the splitting nor the unification is derived — the product structure is empirical.");
        Output.WriteLine(sb.ToString());

        Assert.False(unifiedNative, "unified group is not native");
        Assert.False(splitDerived, "splitting is not derived");
        Assert.False(unifyDerived, "unification is not derived");
    }

    // ── ATQG902: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG902_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG902: DERIVED / PARTIAL / POSTULATED?");

        sb.AppendLine($"CLASSIFICATION: {GaugeSectorSplitting.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: neither the three-sector split nor a unified group is an output of (V,E).");
        sb.AppendLine("  • NOT PARTIAL: there is no partial mechanism relating U(1)/SU(2)/SU(3) — they act on distinct spaces.");
        sb.AppendLine("  • POSTULATED: the three gauge sectors are independent postulates; the product U(1)×SU(2)×SU(3) is");
        sb.AppendLine("    empirical, and a GUT would be an additional postulate.");
        sb.AppendLine();
        sb.AppendLine("So the gauge-sector splitting is POSTULATED (each sector a free input).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("POSTULATED", GaugeSectorSplitting.Classify());
        Assert.True(GaugeSectorSplitting.ThreeSectorsAreIndependentPostulates());
        Assert.False(GaugeSectorSplitting.UnifiedGroupNative());
    }
}
