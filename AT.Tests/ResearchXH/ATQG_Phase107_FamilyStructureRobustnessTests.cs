using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 107 — Family structure robustness. QG106 found stable octave-band spectral mode families
/// (4–5 classes) on causal grids. This phase asks whether spectral families are a GENERIC feature of causal
/// networks by testing random topologies, causal grids, perturbed networks, and sparse vs dense graphs.
/// Classify: ACCIDENTAL / ROBUST / UNIVERSAL.
///
/// Tests: ATQG1070 (random topologies vs causal grids), ATQG1071 (perturbed + sparse/dense graphs),
/// ATQG1072 (family-count statistics + classification).
/// </summary>
public class ATQG_Phase107_FamilyStructureRobustnessTests : ResearchTestBase
{
    public ATQG_Phase107_FamilyStructureRobustnessTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1070: random topologies vs causal grids ──────────────────────────────

    [Fact]
    public void ATQG1070_RandomVsCausalGrids()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1070: octave families in random topologies vs causal grids");

        var grids = FamilyStructureRobustness.CausalGrids();
        int[] gCounts = FamilyStructureRobustness.FamilyCounts(grids);

        // sparse random (low p) vs dense random (high p)
        double[,] sparse91 = FamilyStructureRobustness.RandomErdosRenyi(91, 0.05, 101);
        double[,] sparse91b = FamilyStructureRobustness.RandomErdosRenyi(91, 0.10, 202);
        double[,] sparse200 = FamilyStructureRobustness.RandomErdosRenyi(200, 0.05, 404);
        double[,] dense91 = FamilyStructureRobustness.RandomErdosRenyi(91, 0.20, 303);
        double[,] dense500 = FamilyStructureRobustness.RandomErdosRenyi(500, 0.20, 606);

        int cSparse91 = FamilyStructureRobustness.FamilyCount(sparse91);
        int cSparse91b = FamilyStructureRobustness.FamilyCount(sparse91b);
        int cSparse200 = FamilyStructureRobustness.FamilyCount(sparse200);
        int cDense91 = FamilyStructureRobustness.FamilyCount(dense91);
        int cDense500 = FamilyStructureRobustness.FamilyCount(dense500);

        sb.AppendLine("CAUSAL grids (deterministic):");
        for (int i = 0; i < gCounts.Length; i++)
            sb.AppendLine($"  grid N={grids[i].Count} → {gCounts[i]} octave families");
        sb.AppendLine();
        sb.AppendLine("RANDOM topologies (Erdős–Rényi, fixed seeds):");
        sb.AppendLine($"  sparse ER n=91, p=0.05  → {cSparse91} octave families");
        sb.AppendLine($"  sparse ER n=91, p=0.10  → {cSparse91b} octave families");
        sb.AppendLine($"  sparse ER n=200, p=0.05 → {cSparse200} octave families");
        sb.AppendLine($"  dense ER n=91, p=0.20   → {cDense91} octave families");
        sb.AppendLine($"  dense ER n=500, p=0.20  → {cDense500} octave families");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: causal grids ALWAYS have ≥ 4 octave families (100%). Random topologies show");
        sb.AppendLine("octave families at low density (sparse ER give 2–3) but DENSE random graphs collapse to 1");
        sb.AppendLine("(compressed Laplacian spectrum, small hierarchy span). Octave families are NOT accidental");
        sb.AppendLine("to the grid — they appear in random topologies too — but the family COUNT depends on the");
        sb.AppendLine("spectral hierarchy span, which density erodes.");
        Output.WriteLine(sb.ToString());

        Assert.True(gCounts.All(c => c >= 4), "every causal grid has ≥ 4 octave families");
        Assert.True(cSparse91 >= 3 && cSparse91b >= 3, "sparse low-n random graphs have octave families");
        Assert.True(cDense91 < 3 && cDense500 < 3, "dense random graphs collapse to < 3 families");
        Assert.True(cSparse91 > cDense91, "family count decreases with density (span erosion)");
    }

    // ── ATQG1071: perturbed networks + sparse vs dense graphs ────────────────────

    [Fact]
    public void ATQG1071_PerturbedAndSparseDense()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1071: octave families under perturbations and across density");

        var perturbed = FamilyStructureRobustness.PerturbedNetworks();
        int[] pCounts = FamilyStructureRobustness.FamilyCounts(perturbed);

        // threshold graphs at several densities (all deterministic)
        string[] tNames = { "ε=0.05", "ε=0.10", "ε=0.30", "ε=0.50" };
        int[] tCounts = new int[tNames.Length];
        for (int i = 0; i < tNames.Length; i++)
        {
            var g = ConformalRateGraph.Build(0.0, 12, new[] { 0.05, 0.10, 0.30, 0.50 }[i]);
            tCounts[i] = FamilyStructureRobustness.FamilyCount(g.Adjacency);
        }

        sb.AppendLine("PERTURBED networks (deterministic link removal 5/10/20% of causal grids):");
        for (int i = 0; i < pCounts.Length; i++)
            sb.AppendLine($"  perturbation {i}: {pCounts[i]} octave families");
        sb.AppendLine($"  stats: min {pCounts.Min()}, max {pCounts.Max()}, frac ≥3 {(double)pCounts.Count(c => c >= 3) / pCounts.Length:P0}");
        sb.AppendLine();
        sb.AppendLine("THRESHOLD graphs (2D, ε-threshold at several densities):");
        for (int i = 0; i < tCounts.Length; i++)
            sb.AppendLine($"  {tNames[i]}: {tCounts[i]} octave families");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: octave families PERSIST under link perturbations (5–20% removal: 100% have ≥ 3,");
        sb.AppendLine("count stays 4–5) and across threshold-graph densities (all ε ≥ 3). The family structure is");
        sb.AppendLine("robust to perturbation of the causal network — it is not destroyed by removing links.");
        Output.WriteLine(sb.ToString());

        Assert.True(pCounts.All(c => c >= 3), "every perturbed network has ≥ 3 octave families");
        Assert.True(pCounts.Min() >= 4, "perturbed causal grids keep ≥ 4 families");
        Assert.True(tCounts.All(c => c >= 3), "every threshold graph has ≥ 3 octave families");
    }

    // ── ATQG1072: family-count statistics + classification ───────────────────────

    [Fact]
    public void ATQG1072_FamilyCountStatisticsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1072: family-count statistics across all network classes → ACCIDENTAL / ROBUST / UNIVERSAL");

        int[] random = FamilyStructureRobustness.FamilyCounts(FamilyStructureRobustness.RandomTopologies());
        int[] causal = FamilyStructureRobustness.FamilyCounts(FamilyStructureRobustness.CausalGrids());
        int[] perturbed = FamilyStructureRobustness.FamilyCounts(FamilyStructureRobustness.PerturbedNetworks());
        int[] sparseDense = FamilyStructureRobustness.FamilyCounts(FamilyStructureRobustness.SparseDenseGraphs());

        var rStat = FamilyStructureRobustness.Statistics(random);
        var cStat = FamilyStructureRobustness.Statistics(causal);
        var pStat = FamilyStructureRobustness.Statistics(perturbed);
        var sStat = FamilyStructureRobustness.Statistics(sparseDense);

        int[] all = random.Concat(causal).Concat(perturbed).Concat(sparseDense).ToArray();
        var stat = FamilyStructureRobustness.Statistics(all);

        string cls = FamilyStructureRobustness.Classify();

        sb.AppendLine($"{"class",-12} {"n",4} {"min",4} {"max",4} {"mean",6} {"frac ≥3",8}");
        sb.AppendLine($"{"random",-12} {random.Length,4} {rStat.min,4} {rStat.max,4} {rStat.mean,6:F2} {rStat.fractionAtLeast3,8:P0}");
        sb.AppendLine($"{"causal",-12} {causal.Length,4} {cStat.min,4} {cStat.max,4} {cStat.mean,6:F2} {cStat.fractionAtLeast3,8:P0}");
        sb.AppendLine($"{"perturbed",-12} {perturbed.Length,4} {pStat.min,4} {pStat.max,4} {pStat.mean,6:F2} {pStat.fractionAtLeast3,8:P0}");
        sb.AppendLine($"{"sparse/dense",-12} {sparseDense.Length,4} {sStat.min,4} {sStat.max,4} {sStat.mean,6:F2} {sStat.fractionAtLeast3,8:P0}");
        sb.AppendLine();
        sb.AppendLine($"TOTAL population: {stat.totalNetworks} networks, family-count min {stat.min}, max {stat.max}, mean {stat.mean:F2}");
        sb.AppendLine($"fraction with ≥ 3 octave families: {stat.fractionAtLeast3:P2}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT ACCIDENTAL: the causal class (grids + perturbed) ALWAYS has ≥ 4 families (100%),");
        sb.AppendLine("    and sparse random / threshold graphs show ≥ 3 — the structure is not a grid accident.");
        sb.AppendLine("  • NOT UNIVERSAL: dense Erdős–Rényi graphs collapse to 1–2 families (compressed spectrum).");
        sb.AppendLine("  • ROBUST: octave families are a robust property of the CAUSAL network class; the family");
        sb.AppendLine("    count (3–5) depends on spectral hierarchy span, which dense random graphs lose.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("ROBUST", cls);
        Assert.Equal(1.0, cStat.fractionAtLeast3, 6);       // causal class always ≥ 3
        Assert.Equal(1.0, pStat.fractionAtLeast3, 6);       // perturbed always ≥ 3
        Assert.True(stat.min < 3, "not universal (dense random collapse)");
        Assert.True(stat.totalNetworks >= 10, "enough networks in the population");
    }
}
