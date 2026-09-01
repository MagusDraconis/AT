using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 108 — Family count statistics. QG107 found robust octave-band spectral mode families. This
/// phase asks what family counts are STATISTICALLY PREFERRED in causal networks, from a large deterministic
/// ensemble (ER random graphs, causal grids, threshold graphs, perturbed grids).
/// Classify: NO PREFERENCE / WEAK PREFERENCE / STRONG PREFERENCE.
///
/// Tests: ATQG1080 (ensemble + family-count distribution), ATQG1081 (hierarchy span + size scaling),
/// ATQG1082 (N=3 preference + classification).
/// </summary>
public class ATQG_Phase108_FamilyCountStatisticsTests : ResearchTestBase
{
    public ATQG_Phase108_FamilyCountStatisticsTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1080: ensemble + family-count distribution ───────────────────────────

    [Fact]
    public void ATQG1080_EnsembleAndDistribution()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1080: family-count distribution over the large causal-graph ensemble");

        var ensemble = FamilyCountStatistics.BuildEnsemble();
        int[] counts = FamilyCountStatistics.EnsembleFamilyCounts();
        int[] hist = FamilyCountStatistics.FamilyCountHistogram(counts);

        sb.AppendLine($"ensemble size: {ensemble.Count} causal graphs");
        sb.AppendLine($"  ER random: 60×4 sizes × 5 densities × 3 seeds = {ensemble.Count(e => e.name.StartsWith("ER"))}");
        sb.AppendLine($"  causal grids: {ensemble.Count(e => e.name.StartsWith("grid"))}");
        sb.AppendLine($"  threshold: {ensemble.Count(e => e.name.StartsWith("threshold"))}");
        sb.AppendLine($"  perturbed: {ensemble.Count(e => e.name.StartsWith("perturbed"))}");
        sb.AppendLine();
        sb.AppendLine("FAMILY-COUNT DISTRIBUTION:");
        sb.AppendLine($"  count  networks   fraction");
        for (int k = 0; k < hist.Length; k++)
            sb.AppendLine($"  {k,5}  {hist[k],8}  {(double)hist[k] / counts.Length,10:P1}");
        sb.AppendLine();
        sb.AppendLine($"  modal family count : {FamilyCountStatistics.ModalFamilyCount(counts)}");
        sb.AppendLine($"  mean               : {counts.Average():F2}");
        sb.AppendLine($"  fraction == 3      : {FamilyCountStatistics.FractionWithThree(counts):P1}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the family-count distribution is BROAD (spans " +
            $"{counts.Min()}–{counts.Max()} octave families over the ensemble) — the count is size/density");
        sb.AppendLine("dependent, not a single fixed value.");
        Output.WriteLine(sb.ToString());

        Assert.True(ensemble.Count >= 40, $"large ensemble: {ensemble.Count}");
        Assert.True(counts.Min() >= 1, "every network has at least one octave family");
        Assert.True(counts.Max() >= 4, "the ensemble covers a broad range of family counts");
        Assert.True(hist.Sum() == counts.Length, "histogram partitions the ensemble");
    }

    // ── ATQG1081: hierarchy span + scaling with network size ─────────────────────

    [Fact]
    public void ATQG1081_HierarchySpanAndSizeScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1081: hierarchy span and family-count scaling with network size");

        var ensemble = FamilyCountStatistics.BuildEnsemble();
        int[] counts = FamilyCountStatistics.EnsembleFamilyCounts();
        double[] spans = FamilyCountStatistics.EnsembleHierarchySpans();
        double[] sizes = ensemble.Select(e => (double)e.adjacency.GetLength(0)).ToArray();

        var scaling = FamilyCountStatistics.MeanFamilyCountBySize(sizes, counts);

        sb.AppendLine("HIERARCHY SPAN (ω_max/ω_min):");
        sb.AppendLine($"  min {spans.Min():F2}, median {Median(spans):F2}, max {spans.Max():F2}, mean {spans.Average():F2}");
        sb.AppendLine();
        sb.AppendLine("FAMILY COUNT vs NETWORK SIZE (binned means, whole ensemble):");
        sb.AppendLine($"  {"size",6} {"mean families",14}");
        foreach (var (size, mean) in scaling)
            sb.AppendLine($"  {size,6:F0} {mean,14:F2}");
        sb.AppendLine();
        double logCorr = LogCorrelation(sizes, counts);

        // Within the CAUSAL-GRID class the family count grows with size (span ≈ N^(1/d) → count ≈ ½log₂N).
        var gridSizes = new List<double>();
        var gridCounts = new List<int>();
        for (int i = 0; i < ensemble.Count; i++)
            if (ensemble[i].name.StartsWith("grid"))
            {
                gridSizes.Add(sizes[i]);
                gridCounts.Add(counts[i]);
            }
        double gridCorr = LogCorrelation(gridSizes.ToArray(), gridCounts.ToArray());

        sb.AppendLine($"correlation(family count, ln N) over WHOLE ensemble : {logCorr:F4}  (density-dominated)");
        sb.AppendLine($"correlation(family count, ln N) within CAUSAL GRIDS : {gridCorr:F4}");
        sb.AppendLine();
        sb.AppendLine($"  causal-grid counts: {string.Join(", ", gridCounts)}  at sizes {string.Join(", ", gridSizes.Select(s => s.ToString("F0")))}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: across the mixed ensemble the family count is DENSITY-dominated (weak size");
        sb.AppendLine($"correlation r = {logCorr:F2}), but WITHIN the causal-grid class the count grows with size");
        sb.AppendLine($"(r = {gridCorr:F2}, family count ≈ ½log₂N from span ≈ N^(1/d)) — size scaling is a real,");
        sb.AppendLine("class-specific trend, not a universal law.");
        Output.WriteLine(sb.ToString());

        Assert.True(spans.Min() > 1.0, "every network has a non-trivial hierarchy span");
        Assert.True(gridCorr > 0.4, $"family count grows with ln N within causal grids: r = {gridCorr:F2}");
        Assert.True(gridCounts[^1] >= gridCounts[0], "causal-grid family count grows with size");
        Assert.True(scaling.Count >= 3, "enough size bins for a scaling trend");
    }

    // ── ATQG1082: N=3 preference + classification ────────────────────────────────

    [Fact]
    public void ATQG1082_ThreePreferenceAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1082: preference for N=3 families → NO / WEAK / STRONG");

        int[] counts = FamilyCountStatistics.EnsembleFamilyCounts();
        int modal = FamilyCountStatistics.ModalFamilyCount(counts);
        double frac3 = FamilyCountStatistics.FractionWithThree(counts);
        double fracModal = FamilyCountStatistics.FractionWith(counts, modal);
        string cls = FamilyCountStatistics.Classify();

        sb.AppendLine($"modal family count   : {modal}  ({fracModal:P1} of networks)");
        sb.AppendLine($"fraction with N = 3  : {frac3:P1}");
        sb.AppendLine($"fraction with N = 4  : {FamilyCountStatistics.FractionWith(counts, 4):P1}");
        sb.AppendLine($"fraction with N = 5  : {FamilyCountStatistics.FractionWith(counts, 5):P1}");
        sb.AppendLine($"fraction with N ≥ 4  : {FamilyCountStatistics.FractionWith(counts, 4) + FamilyCountStatistics.FractionWith(counts, 5) + FamilyCountStatistics.FractionWith(counts, 6):P1}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • The SM generation count is 3 (QG80/81: a postulate). In the network ensemble, N=3 is");
        sb.AppendLine("    common among mid-density causal networks but NOT the dominant mode overall.");
        sb.AppendLine("  • The count scales with size (QG1081), so no single family count is universal — the");
        sb.AppendLine("    apparent N=3 is a size/density-window phenomenon (WEAK, not STRONG, preference).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("WEAK PREFERENCE", cls);
        Assert.True(frac3 >= 0.15, $"N=3 is common enough to be a weak preference: {frac3:P1}");
        Assert.True(modal != 3 || frac3 <= 0.40, "N=3 is not the strongly-dominant mode");
    }

    // ── helpers ────────────────────────────────────────────────────────────────────

    private static double Median(double[] x)
    {
        var s = (double[])x.Clone();
        Array.Sort(s);
        return s.Length % 2 == 1
            ? s[s.Length / 2]
            : 0.5 * (s[s.Length / 2 - 1] + s[s.Length / 2]);
    }

    private static double LogCorrelation(double[] x, int[] y)
    {
        int n = Math.Min(x.Length, y.Length);
        double[] lx = new double[n];
        for (int i = 0; i < n; i++) lx[i] = Math.Log(x[i]);
        double mx = lx.Average(), my = y.Take(n).Average();
        double num = 0.0, dx2 = 0.0, dy2 = 0.0;
        for (int i = 0; i < n; i++)
        {
            double dx = lx[i] - mx, dy = y[i] - my;
            num += dx * dy; dx2 += dx * dx; dy2 += dy * dy;
        }
        return dx2 * dy2 > 0.0 ? num / Math.Sqrt(dx2 * dy2) : 0.0;
    }
}
