using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 114 — 3D connectivity classes. QG87 showed higher cells are derived. This phase asks whether
/// local 3D connectivity (valence + neighborhood geometry) generates discrete classes of network states.
/// Classify: NO RELATION / PARTIAL RELATION / CONNECTIVITY CLASS ORIGIN.
///
/// Tests: ATQG1140 (valence classes + tetrahedral structures), ATQG1141 (local volume geometry + connectivity
/// degeneracies), ATQG1142 (family/color analogs + classification).
/// </summary>
public class ATQG_Phase114_ConnectivityClasses3DTests : ResearchTestBase
{
    public ATQG_Phase114_ConnectivityClasses3DTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1140: valence classes + tetrahedral structures ───────────────────────

    [Fact]
    public void ATQG1140_ValenceClassesAndTetrahedra()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1140: do valence 3,4,5,6 generate discrete spectral classes + tetrahedral structure?");

        int[] valences = { 3, 4, 5, 6 };
        bool distinct = ConnectivityClasses3D.ValenceClassesDistinct(valences);

        sb.AppendLine($"VALENCE CLASSES (circulant graphs, N=120):");
        sb.AppendLine($"  all pairwise KS > 0.1 (distinct classes): {distinct}");
        sb.AppendLine($"  distinct connectivity classes: {ConnectivityClasses3D.DistinctConnectivityClassCount()}");
        sb.AppendLine();
        sb.AppendLine("TETRAHEDRAL STRUCTURE (K₄ cliques per node = local 3D volume cells):");
        foreach (int v in valences)
        {
            var a = ConnectivityClasses3D.ValenceGraph(120, v);
            int tet = ConnectivityClasses3D.TetrahedronCount(a);
            sb.AppendLine($"  valence {v}: {tet} tetrahedra ({(double)tet / 120:F1} per node)");
        }
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: each valence gives a DISTINCT spectral class. Tetrahedral (3D-volume) structure");
        sb.AppendLine("requires sufficient local connectivity (valence 6 in ring-like graphs; the genuine 3D");
        sb.AppendLine("threshold graph hosts dense tetrahedra) — local connectivity generates discrete classes.");
        Output.WriteLine(sb.ToString());

        Assert.True(distinct, "valence classes are spectrally distinct");
        Assert.True(ConnectivityClasses3D.HasTetrahedralStructure(ConnectivityClasses3D.ValenceGraph(120, 6)),
            "valence-6 (dense connectivity) hosts tetrahedral structure");
        Assert.True(ConnectivityClasses3D.HasTetrahedralStructure(ConnectivityClasses3D.ThresholdGraph3D()),
            "genuine 3D connectivity hosts dense tetrahedral structure");
    }

    // ── ATQG1141: local volume geometry + connectivity degeneracies ──────────────

    [Fact]
    public void ATQG1141_VolumeGeometryAndDegeneracies()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1141: local volume geometry and connectivity degeneracies");

        int[] valences = { 3, 4, 5, 6 };
        bool volumeIs3D = ConnectivityClasses3D.VolumeStructureIs3D();
        double gridVol = ConnectivityClasses3D.LocalVolumePerNode(SpectrumRobustness.LinkAdjacency(CausalSet.BuildGrid(6, 6)));
        double th3dVol = ConnectivityClasses3D.LocalVolumePerNode(ConnectivityClasses3D.ThresholdGraph3D());

        sb.AppendLine("LOCAL VOLUME GEOMETRY (tetrahedra per node):");
        sb.AppendLine($"  1+1D causal grid : {gridVol:F2} tetrahedra/node");
        sb.AppendLine($"  3D threshold     : {th3dVol:F2} tetrahedra/node");
        sb.AppendLine($"  volume structure is 3D-specific: {volumeIs3D}");
        sb.AppendLine();
        sb.AppendLine("CONNECTIVITY DEGENERACIES (distinct eigenvalues / N, valence classes):");
        foreach (int v in valences)
        {
            double ratio = ConnectivityClasses3D.DegeneracyRatio(v);
            sb.AppendLine($"  valence {v}: {ratio:F3}  (degenerate: {ConnectivityClasses3D.ValenceClassesDegenerate(v)})");
        }
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: local volume geometry is 3D-CONNECTIVITY-SPECIFIC — the genuine 3D network hosts");
        sb.AppendLine("dense tetrahedral volume while the 1+1D causal grid has none. High-symmetry valence classes");
        sb.AppendLine("are DEGENERATE (few distinct eigenvalues) — connectivity produces discrete degenerate classes.");
        Output.WriteLine(sb.ToString());

        Assert.True(volumeIs3D, "tetrahedral volume structure is specific to 3D connectivity");
        Assert.True(th3dVol >= 1.0, "3D network hosts dense tetrahedral volume");
        Assert.True(gridVol == 0.0, "1+1D causal grid has no tetrahedral volume");
        Assert.True(ConnectivityClasses3D.ValenceClassesDegenerate(4), "valence-4 is degenerate (high symmetry)");
        Assert.True(ConnectivityClasses3D.ValenceClassesDegenerate(6), "valence-6 is degenerate");
    }

    // ── ATQG1142: family/color analogs + classification ──────────────────────────

    [Fact]
    public void ATQG1142_FamilyColorAnalogsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1142: family/color analogs → NO RELATION / PARTIAL / CONNECTIVITY CLASS ORIGIN");

        int classCount = ConnectivityClasses3D.DistinctConnectivityClassCount();
        int smCount = ConnectivityClasses3D.SmFamilyColorCount();
        string cls = ConnectivityClasses3D.Classify();

        sb.AppendLine($"FAMILY/COLOR ANALOG:");
        sb.AppendLine($"  distinct connectivity classes (valence 3,4,5,6): {classCount}");
        sb.AppendLine($"  SM family/color count: {smCount} (QG79/QG80)");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: local 3D connectivity generates REAL discrete classes — distinct");
        sb.AppendLine("    spectral classes per valence, tetrahedral volume structure, and connectivity");
        sb.AppendLine("    degeneracies all exist.");
        sb.AppendLine("  • NOT CONNECTIVITY CLASS ORIGIN: the discrete-class count tracks valence/size and does not");
        sb.AppendLine("    uniquely equal the SM 3-family/3-color count — connectivity classes are real but");
        sb.AppendLine("    underdetermine the internal SM counts.");
        sb.AppendLine("  • PARTIAL RELATION: connectivity generates discrete classes (structural analog),");
        sb.AppendLine("    consistent with QG83 (valence 3 is a graph-theory fact, coincidental with color/family 3)");
        sb.AppendLine("    and QG87 (higher cells are derived).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", cls);
        Assert.True(classCount >= 2, "multiple discrete connectivity classes");
        Assert.True(classCount != smCount, "connectivity class count is not uniquely 3 (no class origin)");
    }
}
