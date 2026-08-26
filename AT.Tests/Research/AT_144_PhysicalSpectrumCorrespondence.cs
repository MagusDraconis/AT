using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_144_PhysicalSpectrumCorrespondence : ResearchTestBase
{
    public AT_144_PhysicalSpectrumCorrespondence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_144_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-144 Physical Spectrum Correspondence");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. L = graph Laplacian of Q interactions (AT-142).");
        sb.AppendLine("  2. Theta hierarchy is universal graph-based physics (AT-143).");
        sb.AppendLine("  3. Assume NO physical correspondence until demonstrated.");
        sb.AppendLine("  4. Graph Laplacian spectra may be physically trivial.");
        sb.AppendLine();

        Sec(sb, "1. AT-142/143 Recap");
        sb.AppendLine("  AT-142: L = graph Laplacian → Theta spectra.");
        sb.AppendLine("  AT-143: Hierarchy universal across locally connected graphs.");
        sb.AppendLine("  Q: Do these spectra correspond to known physical systems?");
        sb.AppendLine();

        Sec(sb, "2. Correspondence Theory");
        sb.AppendLine(PhysicalSpectrumAnalyzer.CorrespondenceTheory());
        sb.AppendLine();

        Sec(sb, "3. Spectrum Generation and Comparison");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = PhysicalSpectrumAnalyzer.Analyze();
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Theta geometries tested: {report.GeometriesTested}");
        sb.AppendLine($"  Physical models tested: {report.PhysicalModelsTested}");
        sb.AppendLine($"  Total comparisons: {report.Comparisons.Count}");
        sb.AppendLine();

        Sec(sb, "4. Physical Models");
        sb.AppendLine("  Model                           │ System                    │ Dimension │ Modes");
        sb.AppendLine("  " + new string('─', 75));
        foreach (var m in report.PhysicalModels)
            sb.AppendLine($"  {m.Name,-31} │ {m.System,-25} │ {m.Dimension,9} │ {m.Spectrum.Length,5}");
        sb.AppendLine();

        Sec(sb, "5. Spectrum Comparisons");
        sb.AppendLine(PhysicalSpectrumAnalyzer.ComparisonTable(report.Comparisons));
        sb.AppendLine();

        Sec(sb, "6. Quantitative Analysis");
        sb.AppendLine($"  Mathematical identities:   {report.IdentityMatches}");
        sb.AppendLine($"  Strong matches:            {report.StrongMatches}");
        sb.AppendLine($"  Mean spectral overlap:     {report.MeanSpectralOverlap:P0}");
        sb.AppendLine($"  Physical correspondence:   {(report.PhysicalCorrespondenceExists ? "YES" : "NO")}");
        sb.AppendLine($"  Novel AT prediction:      {(report.NovelPredictionMade ? "YES" : "NO")}");
        sb.AppendLine();
        sb.AppendLine("  Key correspondences:");
        sb.AppendLine("    1. 1D Chain Laplacian ≡ 1D Tight-Binding (MATHEMATICAL IDENTITY)");
        sb.AppendLine("    2. 1D Chain Laplacian ≡ Coupled Oscillator Chain (IDENTITY)");
        sb.AppendLine("    3. 2D Square Laplacian ≡ 2D Tight-Binding (IDENTITY)");
        sb.AppendLine("    4. 3D Cubic Laplacian ≡ 3D Tight-Binding (IDENTITY)");
        sb.AppendLine("    5. 2D Hexagonal → Graphene-like (Dirac cones)");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(PhysicalSpectrumAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Research Questions");
        sb.AppendLine(PhysicalSpectrumAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "9. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-144 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Physical correspondence: {(report.PhysicalCorrespondenceExists ? "ESTABLISHED" : "NOT FOUND")}");
        sb.AppendLine($"  Novel prediction: {(report.NovelPredictionMade ? "YES" : "NO")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
