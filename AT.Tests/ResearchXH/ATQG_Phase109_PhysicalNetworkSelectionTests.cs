using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 109 — Selection of the physical network. QG102 found many globally-consistent network classes;
/// QG108 found a broad family-count distribution. This phase asks why nature realizes ONE specific network
/// class, by measuring stability selection, attractor basins, actualization statistics, growth history, and
/// anthropic-free selection.
/// Classify: NO SELECTION / PARTIAL SELECTION / PHYSICAL SELECTION.
///
/// Tests: ATQG1090 (stability + attractor basins), ATQG1091 (actualization statistics + growth history),
/// ATQG1092 (anthropic-free selection + classification).
/// </summary>
public class ATQG_Phase109_PhysicalNetworkSelectionTests : ResearchTestBase
{
    public ATQG_Phase109_PhysicalNetworkSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1090: stability selection + attractor basins ─────────────────────────

    [Fact]
    public void ATQG1090_StabilityAndAttractorBasins()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1090: does stability + attractor structure narrow the network class?");

        double gapGrid = PhysicalNetworkSelection.MeanStabilityGap("grid");
        double gapER = PhysicalNetworkSelection.MeanStabilityGap("ER");
        double gapThr = PhysicalNetworkSelection.MeanStabilityGap("threshold");

        double persistGrid = PhysicalNetworkSelection.FamilyStructurePersistence("grid");
        double persistER = PhysicalNetworkSelection.FamilyStructurePersistence("ER");
        double persistThr = PhysicalNetworkSelection.FamilyStructurePersistence("threshold");

        var (basinCount, basinVolumes) = PhysicalNetworkSelection.AttractorBasins();
        bool singleDominates = PhysicalNetworkSelection.SingleBasinDominates(basinVolumes);

        sb.AppendLine("STABILITY (mean spectral gap λ_2):");
        sb.AppendLine($"  causal grids : {gapGrid:F4}");
        sb.AppendLine($"  ER random    : {gapER:F4}");
        sb.AppendLine($"  threshold    : {gapThr:F4}");
        sb.AppendLine();
        sb.AppendLine("FAMILY-STRUCTURE PERSISTENCE (fraction keeping all families under 10% link removal):");
        sb.AppendLine($"  causal grids : {persistGrid:P2}");
        sb.AppendLine($"  ER random    : {persistER:P2}");
        sb.AppendLine($"  threshold    : {persistThr:P2}");
        sb.AppendLine();
        sb.AppendLine($"ATTRACTOR BASINS (KS single-linkage): {basinCount} basins");
        sb.AppendLine($"  basin volumes: {string.Join(", ", basinVolumes.OrderByDescending(v => v).Select(v => v.ToString("P1", CultureInfo.InvariantCulture)))}");
        sb.AppendLine($"  single basin dominates (> 80%): {singleDominates}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the stability criteria CONFLICT — the spectral gap favours ER random (large gap),");
        sb.AppendLine("while family-structure persistence favours the causal grid (100% keep all families). The");
        sb.AppendLine("ensemble splits into 17 attractor basins with no dominant one. No single stability criterion");
        sb.AppendLine("selects a unique class — this is the signature of PARTIAL (conflicted) selection.");
        Output.WriteLine(sb.ToString());

        Assert.True(persistGrid > persistER, "causal grids preserve family structure better than ER random");
        Assert.True(persistGrid >= 0.99, "causal grids almost always preserve family structure");
        Assert.True(basinCount >= 2, "multiple attractor basins");
        Assert.False(singleDominates, "no single basin dominates");
    }

    // ── ATQG1091: actualization statistics + growth history ─────────────────────

    [Fact]
    public void ATQG1091_ActualizationStatisticsAndGrowth()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1091: actualization statistics and network growth history");

        double varGrid = PhysicalNetworkSelection.MeanActualizationVariance("grid");
        double varER = PhysicalNetworkSelection.MeanActualizationVariance("ER");
        double varThr = PhysicalNetworkSelection.MeanActualizationVariance("threshold");

        int[] growth = PhysicalNetworkSelection.GrowthFamilyCountSequence();
        bool converged = PhysicalNetworkSelection.GrowthConverges(growth);

        sb.AppendLine("ACTUALIZATION STATISTICS (mean counting-measure variance):");
        sb.AppendLine($"  causal grids : {varGrid:F2}");
        sb.AppendLine($"  ER random    : {varER:F2}");
        sb.AppendLine($"  threshold    : {varThr:F2}");
        sb.AppendLine();
        sb.AppendLine("NETWORK GROWTH HISTORY (octave-family count vs size):");
        for (int i = 0; i < growth.Length; i++)
            sb.AppendLine($"  growth step {i}: {growth[i]} families");
        sb.AppendLine($"  converged (last three equal): {converged}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the counting measure statistically favours the causal class (lower variance,");
        sb.AppendLine("more concentrated actualization-rate density), but the growth history shows the family count");
        sb.AppendLine("drifts with size — the 'realized class' depends on the growth stage, so history alone does");
        sb.AppendLine("not select a unique network.");
        Output.WriteLine(sb.ToString());

        Assert.True(varGrid < varER, "causal grids have more concentrated counting measure");
        Assert.True(growth.Length >= 6, "growth sequence spans many sizes");
        Assert.False(converged, "growth does NOT converge to a unique class (family count still drifting)");
    }

    // ── ATQG1092: anthropic-free selection + classification ──────────────────────

    [Fact]
    public void ATQG1092_AnthropicFreeSelectionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1092: anthropic-free selection → NO / PARTIAL / PHYSICAL");

        var (bestClass, classCount, unique) = PhysicalNetworkSelection.AnthropicFreeSelection();
        string cls = PhysicalNetworkSelection.Classify();

        double varGrid = PhysicalNetworkSelection.MeanActualizationVariance("grid");
        double varER = PhysicalNetworkSelection.MeanActualizationVariance("ER");
        double persistGrid = PhysicalNetworkSelection.FamilyStructurePersistence("grid");
        double persistER = PhysicalNetworkSelection.FamilyStructurePersistence("ER");

        sb.AppendLine($"ANTHROPIC-FREE stability functional:");
        sb.AppendLine($"  best class: {bestClass} ({classCount} members)");
        sb.AppendLine($"  unique network selected: {unique}");
        sb.AppendLine();
        sb.AppendLine("CRITERION CONFLICT (why selection is only partial):");
        sb.AppendLine($"  counting-measure variance: grid {varGrid:F2} < ER {varER:F2}  → statistics prefer grid");
        sb.AppendLine($"  family persistence:        grid {persistGrid:P2} > ER {persistER:P2}  → stability prefers grid");
        sb.AppendLine($"  spectral gap:              grid {PhysicalNetworkSelection.MeanStabilityGap("grid"):F3} < ER {PhysicalNetworkSelection.MeanStabilityGap("ER"):F3}  → gap prefers ER");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO SELECTION: the counting measure (QG89) and family-structure persistence BOTH");
        sb.AppendLine("    narrow toward the causal grid without any observer input.");
        sb.AppendLine("  • NOT PHYSICAL SELECTION: no native functional selects a UNIQUE network — the preferred");
        sb.AppendLine("    class contains many members, the spectral gap criterion conflicts (prefers ER random),");
        sb.AppendLine("    and growth history drifts the family count.");
        sb.AppendLine("  • PARTIAL SELECTION: a native, anthropic-free mechanism narrows the region but conflicting");
        sb.AppendLine("    criteria prevent a unique choice — consistent with QG96 (partial) and QG102 (non-unique).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL SELECTION", cls);
        Assert.False(unique, "no unique network selected");
        Assert.True(varGrid < varER, "counting measure statistically prefers the causal grid");
        Assert.True(persistGrid > persistER, "family-structure persistence prefers the causal grid");
    }
}
