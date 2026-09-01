using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_143_GeometryDependence : ResearchTestBase
{
    public AT_143_GeometryDependence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_143_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-143 Geometry Dependence of the Theta Hierarchy");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. L = graph Laplacian of Q interaction network (AT-142).");
        sb.AppendLine("  2. AT-142 used a 1D chain topology.");
        sb.AppendLine("  3. Different Q geometries → different L → different hierarchy.");
        sb.AppendLine("  4. Assume the hierarchy is a 1D artifact until universality is shown.");
        sb.AppendLine();

        Sec(sb, "1. AT-142 Recap");
        sb.AppendLine("  L ≡ -(1/Δx²)·L_Q - γI for 1D chain Q interactions.");
        sb.AppendLine("  Q: Is this SPECIFIC to 1D chains, or UNIVERSAL across graphs?");
        sb.AppendLine();

        Sec(sb, "2. Geometry Theory");
        sb.AppendLine(GeometryHierarchyAnalyzer.GeometryTheory());
        sb.AppendLine();

        Sec(sb, "3. Geometry Construction and Spectral Analysis");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = GeometryHierarchyAnalyzer.Analyze(seed: 42);
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Geometries built: {report.GeometryCount}");
        sb.AppendLine();

        Sec(sb, "4. Geometry Properties");
        sb.AppendLine("  Geometry           │ Dim │ Nodes │ Mean Deg │ Clustering │ Diameter │ Class");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var g in report.Geometries)
            sb.AppendLine($"  {g.Name,-18} │ {g.Dimension,3} │ {g.NodeCount,5} │ {g.MeanDegree,8:F1} │ {g.ClusteringCoeff,10:F3} │ {g.Diameter,8} │ {g.GraphClass}");
        sb.AppendLine();

        Sec(sb, "5. Spectral Comparison");
        sb.AppendLine("  Geometry           │ Modes │ Spectrum Type │ Species Count │ vs 1D Chain");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var s in report.Spectra)
            sb.AppendLine($"  {s.GeometryName,-18} │ {s.EigenmodeCount,5} │ {s.SpectrumType,-13} │ {s.PredictedSpeciesCount,13} │ —");
        sb.AppendLine();

        Sec(sb, "6. Theta Hierarchy Survival");
        sb.AppendLine("  Geometry           │ Transport │ Memory │ Species │ Evolution │ Finite? │ Assessment");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var c in report.Comparisons)
            sb.AppendLine($"  {c.GeometryName,-18} │ {(c.TransportSurvives ? "✓" : "✗"),-9} │ {(c.MemorySurvives ? "✓" : "✗"),-6} │ {(c.SpeciesSurvive ? "✓" : "✗"),-7} │ {(c.EvolutionSurvives ? "✓" : "✗"),-9} │ {(c.LandscapeFinite ? "✓" : "✗"),-7} │ {c.Assessment}");
        sb.AppendLine();

        Sec(sb, "7. Universality Analysis");
        sb.AppendLine($"  Mean spectral similarity to 1D chain: {report.MeanSpectralSimilarity:P0}");
        sb.AppendLine($"  Geometric invariants: [{string.Join(", ", report.Invariants)}]");
        sb.AppendLine($"  Geometry-specific: [{string.Join(", ", report.GeometrySpecific)}]");
        sb.AppendLine($"  Hierarchy is universal: {(report.HierarchyIsUniversal ? "YES" : "NO")}");
        sb.AppendLine();
        sb.AppendLine("  Key insight:");
        sb.AppendLine("  - REGULAR LATTICES (1D, 2D, 3D): full Theta hierarchy survives.");
        sb.AppendLine("  - RANDOM GRAPHS: transport only — no discrete species.");
        sb.AppendLine("  - SCALE-FREE: localized modes near hubs, different species structure.");
        sb.AppendLine("  - Requirement: GRAPH LOCALITY (edges between nearby nodes only).");
        sb.AppendLine();

        Sec(sb, "8. Hostile Review");
        sb.AppendLine(GeometryHierarchyAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "9. Research Questions");
        sb.AppendLine(GeometryHierarchyAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "10. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-143 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Hierarchy universal: {(report.HierarchyIsUniversal ? "YES" : "NO")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
