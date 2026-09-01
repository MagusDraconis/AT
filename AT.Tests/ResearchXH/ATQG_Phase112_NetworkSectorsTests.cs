using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 112 — Network sector hypothesis. QG109–111 showed no unique global network is selected. This
/// phase asks whether physical reality can consist of MULTIPLE INTERACTING NETWORK SECTORS rather than one
/// uniform network, by decomposing the ensemble into spectral sectors, checking coexistence, phase-like
/// regions, family/color analogs, and sector interactions.
/// Classify: UNIFORM NETWORK / PARTIAL SECTORING / FULL SECTOR STRUCTURE.
///
/// Tests: ATQG1120 (sector decomposition + coexistence), ATQG1121 (phase-like regions + family/color analogs),
/// ATQG1122 (sector interactions + classification).
/// </summary>
public class ATQG_Phase112_NetworkSectorsTests : ResearchTestBase
{
    public ATQG_Phase112_NetworkSectorsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1120: sector decomposition + coexistence ─────────────────────────────

    [Fact]
    public void ATQG1120_SectorDecompositionAndCoexistence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1120: does the ensemble decompose into coexisting spectral sectors?");

        var (sectorCount, labels) = NetworkSectors.SectorDecomposition();
        var shapes = NetworkSectors.CachedShapes();

        double gridWithin = NetworkSectors.WithinClassKS("grid", shapes);
        double erWithin = NetworkSectors.WithinClassKS("ER", shapes);
        double gridBetween = NetworkSectors.BetweenClassKS("grid", shapes);
        double erBetween = NetworkSectors.BetweenClassKS("ER", shapes);
        double gridSep = NetworkSectors.SeparationRatio("grid");
        double erSep = NetworkSectors.SeparationRatio("ER");

        sb.AppendLine($"SECTOR DECOMPOSITION (KS single-linkage, ε=0.10): {sectorCount} sectors");
        sb.AppendLine();
        sb.AppendLine("COEXISTENCE (within- vs between-class KS):");
        sb.AppendLine($"  grid   : within {gridWithin:F3}  between {gridBetween:F3}  separation {gridSep:F2}");
        sb.AppendLine($"  ER     : within {erWithin:F3}  between {erBetween:F3}  separation {erSep:F2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the ensemble decomposes into MULTIPLE spectral sectors. The causal-grid class is a");
        sb.AppendLine($"SHARP separable sector (separation {gridSep:F2} — within-class KS far smaller than between-class),");
        sb.AppendLine($"while ER random is BROAD (separation {erSep:F2}) because it spans the full density range. The");
        sb.AppendLine("sectors coexist but only PARTIALLY separate.");
        Output.WriteLine(sb.ToString());

        Assert.True(sectorCount >= 2, "ensemble decomposes into multiple spectral sectors");
        Assert.True(gridSep > 1.5, "causal grids are a sharp separable sector");
        Assert.True(gridWithin < gridBetween, "within-class KS smaller than between-class KS (grid)");
        Assert.True(erSep < 1.5, "ER random is broad (spans densities), not a sharp sector");
    }

    // ── ATQG1121: phase-like regions + family/color analogs ──────────────────────

    [Fact]
    public void ATQG1121_PhaseLikeRegionsAndFamilyColor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1121: phase-like network regions and family/color analogs");

        var (meanCent, meanWithin, phaseLike) = NetworkSectors.PhaseLikeRegions();
        int dominant = NetworkSectors.DominantSectorCount();
        int smCount = NetworkSectors.SmFamilyColorCount();

        sb.AppendLine("PHASE-LIKE REGIONS (sector centroid separation vs within-sector KS):");
        sb.AppendLine($"  mean centroid separation  : {meanCent:F3}");
        sb.AppendLine($"  mean within-sector KS     : {meanWithin:F3}");
        sb.AppendLine($"  phase-like (centroid > within): {phaseLike}");
        sb.AppendLine();
        sb.AppendLine("FAMILY/COLOR ANALOG:");
        sb.AppendLine($"  dominant spectral sectors : {dominant}");
        sb.AppendLine($"  SM family/color count     : {smCount} (QG79/QG80)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the sectors are NOT sharply phase-like (centroid separation does not clearly");
        sb.AppendLine("exceed within-sector spread), and the dominant sector count (2) is comparable to but NOT");
        sb.AppendLine("exactly the SM 3-family/3-color structure. The sector structure is a partial analog, not a");
        sb.AppendLine("sharp phase structure and not a derivation of the SM count.");
        Output.WriteLine(sb.ToString());

        Assert.False(phaseLike, "sectors are NOT sharply phase-separated (continuous spectrum between them)");
        Assert.True(dominant >= 2, "at least two dominant sectors coexist");
        Assert.True(Math.Abs(dominant - smCount) <= 1, "dominant sector count comparable to the SM 3-count");
    }

    // ── ATQG1122: sector interactions + classification ───────────────────────────

    [Fact]
    public void ATQG1122_SectorInteractionsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1122: sector interactions → UNIFORM / PARTIAL / FULL?");

        double boundary = NetworkSectors.BoundaryFraction();
        int dominant = NetworkSectors.DominantSectorCount();
        var (meanCent, meanWithin, phaseLike) = NetworkSectors.PhaseLikeRegions();
        string cls = NetworkSectors.Classify();

        sb.AppendLine($"SECTOR INTERACTIONS:");
        sb.AppendLine($"  boundary networks (closer to another class than to own): {boundary:P1}");
        sb.AppendLine($"  dominant sectors: {dominant}");
        sb.AppendLine($"  phase-like separation: {phaseLike}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT UNIFORM NETWORK: the ensemble decomposes into multiple coexisting, separable sectors");
        sb.AppendLine("    (within < between KS); distinct network classes coexist.");
        sb.AppendLine("  • NOT FULL SECTOR STRUCTURE: the sector boundaries are continuous (non-trivial boundary");
        sb.AppendLine("    fraction, centroid separation moderate) and the dominant sector count does not uniquely");
        sb.AppendLine("    equal the SM 3-family/3-color count.");
        sb.AppendLine("  • PARTIAL SECTORING: physical reality as multiple interacting network sectors is PARTIALLY");
        sb.AppendLine("    supported — coexisting interacting sectors, not a sharp phase structure.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL SECTORING", cls);
        Assert.True(boundary > 0.0, "sectors interact (non-zero boundary fraction)");
        Assert.True(dominant >= 2, "multiple dominant sectors");
    }
}
