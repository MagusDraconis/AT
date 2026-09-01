using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_140_SpectralOriginOfLandscape : ResearchTestBase
{
    public AT_140_SpectralOriginOfLandscape(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_140_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-140 Spectral Origin of the Information Landscape");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Theta field dynamics can be approximated by a discrete Laplacian operator.");
        sb.AppendLine("  2. Stable species = stable eigenmodes of this operator.");
        sb.AppendLine("  3. ~13 species from AT-139 are the test targets.");
        sb.AppendLine("  4. Assume NO spectral origin until eigenmode mapping is demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. AT-139 Recap — Finite Attractor Landscape");
        sb.AppendLine("  AT-139: 13 attractors, 5 components, 2 hubs, 13 bottlenecks.");
        sb.AppendLine("  The landscape is finite and structured but its ORIGIN is unknown.");
        sb.AppendLine("  Hypothesis: species = eigenmodes of the Theta field operator.");
        sb.AppendLine();

        Sec(sb, "2. Spectral Theory");
        sb.AppendLine(ThetaSpectralAnalyzer.SpectralTheory());
        sb.AppendLine();

        Sec(sb, "3. Theta Field Operator and Eigenmode Computation");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ThetaSpectralAnalyzer.Analyze();
        sw.Stop();

        sb.AppendLine($"  Analysis completed in {sw.Elapsed.TotalMilliseconds:F0}ms.");
        sb.AppendLine($"  Field points: N=10");
        sb.AppendLine($"  Damping: γ=0.1, Coupling coefficient: 1/(Δx²)");
        sb.AppendLine($"  Total eigenmodes: {report.TotalEigenmodes}");
        sb.AppendLine($"  Spectral families: {report.TotalFamilies}");
        sb.AppendLine();

        Sec(sb, "4. Eigenmode Spectrum");
        sb.AppendLine(ThetaSpectralAnalyzer.ModeTable(report.Eigenmodes));
        sb.AppendLine();

        Sec(sb, "5. Species-to-Eigenmode Mapping");
        sb.AppendLine($"  AT-139 species mapped: {report.MappedSpecies}/{report.TotalEigenmodes}");
        sb.AppendLine($"  Mean pattern overlap: {report.MeanMappingOverlap:F3}");
        sb.AppendLine();

        sb.AppendLine(ThetaSpectralAnalyzer.MappingTable(report.SpeciesMappings));
        sb.AppendLine();

        Sec(sb, "6. Spectral Predictions vs AT-139 Observations");
        sb.AppendLine($"  Predicted species count:  {report.PredictedAttractorCount}");
        sb.AppendLine($"  AT-139 observed count:   ~13 (from gradient descent)");
        sb.AppendLine($"  AT-138 observed count:   ~19 (from evolution)");
        sb.AppendLine();
        sb.AppendLine($"  Families match components:  {(report.FamiliesMatchComponents ? "YES ✓" : "no ✗")}");
        sb.AppendLine($"  Hubs match low-k modes:     {(report.HubsMatchLowOrderModes ? "YES ✓" : "no ✗")}");
        sb.AppendLine($"  Bottlenecks match high-k:   {(report.BottlenecksMatchHighOrder ? "YES ✓" : "no ✗")}");
        sb.AppendLine();

        sb.AppendLine("  Key spectral insights:");
        sb.AppendLine("    1. Eigenmodes are ANALYTIC — no simulation required.");
        sb.AppendLine("    2. Species count = number of stable modes.");
        sb.AppendLine("    3. Mode families (k=0,1,2,...) → graph components.");
        sb.AppendLine("    4. Low-k modes → hubs (wider basins, more connections).");
        sb.AppendLine("    5. High-k modes → bottlenecks (narrow basins, fewer connections).");
        sb.AppendLine("    6. Gaps in spectrum → forbidden pattern configurations.");
        sb.AppendLine();

        Sec(sb, "7. Spectral Family Analysis");
        sb.AppendLine("  Family        │ Modes │ Frequency │ Stability │ → Component?");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var f in report.Families)
            sb.AppendLine($"  {f.FamilyName,-13} │ {f.ModeCount,5} │ {f.CentralFrequency,9:F3} │ {f.MeanStability,8:F1} │ {(f.CorrespondsToGraphComponent ? "YES" : "no")}");
        sb.AppendLine();

        Sec(sb, "8. Hostile Review");
        sb.AppendLine(ThetaSpectralAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "9. Research Questions");
        sb.AppendLine(ThetaSpectralAnalyzer.ResearchQuestions(report));
        sb.AppendLine();

        Sec(sb, "10. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Experiment AT-140 completed.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Spectral origin: {(report.SpectralOriginConfirmed ? "CONFIRMED" : "NOT CONFIRMED")}");
        sb.AppendLine($"  Species = eigenmodes: {(report.SpectralOriginConfirmed ? "YES — analytically derivable" : "NOT YET")}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
