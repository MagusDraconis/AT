using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X008_InformationCarrierTaxonomy : ResearchTestBase
{
    public TQM_X008_InformationCarrierTaxonomy(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X008_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X008 Information Carrier Taxonomy");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-X007: species = persistent information carriers.");
        sb.AppendLine("  2. Known carriers: eigenmodes (linear), solitons (nonlinear).");
        sb.AppendLine("  3. Hypothesis: additional carrier classes exist.");
        sb.AppendLine();

        Sec(sb, "1. Taxonomy Theory");
        sb.AppendLine(InformationCarrierAnalyzer.TaxonomyTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = InformationCarrierAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Complete Carrier Taxonomy");
        sb.AppendLine("  Carrier Class              │ Regime        │ Local? │ Topo? │ Info? │ Diversity");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var c in report.Classes)
            sb.AppendLine($"  {c.Name,-26} │ {c.Regime,-13} │ {(c.IsLocalized ? "YES" : "NO"),-6} │ {(c.IsTopological ? "YES" : "NO"),-5} │ {(c.CarriesInformation ? "YES" : "NO"),-5} │ {c.DiversityScore,4}");
        sb.AppendLine();

        Sec(sb, "3. Regime Summary");
        sb.AppendLine($"  Total classes:    {report.TotalClasses}");
        sb.AppendLine($"  Linear:           {report.LinearClasses}");
        sb.AppendLine($"  Nonlinear:        {report.NonlinearClasses}");
        sb.AppendLine($"  Topological:      {report.TopologicalClasses}");
        sb.AppendLine($"  Richest regime:   {report.RichestRegime}");
        sb.AppendLine();

        Sec(sb, "4. Carrier Hierarchy");
        sb.AppendLine("  PERSISTENT STRUCTURES (Level 0)");
        sb.AppendLine("  ├── LINEAR: Eigenmodes, Composite Modes");
        sb.AppendLine("  ├── WEAKLY NONLINEAR: Perturbed Eigenmodes, Amplitude Breathers");
        sb.AppendLine("  ├── STRONGLY NONLINEAR: Bright/Dark/Vector Solitons, Breather Solitons");
        sb.AppendLine("  ├── TOPOLOGICAL: Vortices, Domain Walls, Edge States");
        sb.AppendLine("  └── HYBRID: Soliton-Mode Hybrids, Localized Attractors");
        sb.AppendLine();
        sb.AppendLine("  Each class → multiple species (e.g., bright soliton N=1,2,3,...).");
        sb.AppendLine("  Each species → multiple instances (can coexist in populations).");
        sb.AppendLine();

        Sec(sb, "5. The Deeper Invariant");
        sb.AppendLine("  Beneath all 5 regimes, the invariant is:");
        sb.AppendLine("  PERSISTENCE + LOCALIZATION/EXTENT + INFORMATION ENCODING");
        sb.AppendLine();
        sb.AppendLine("  The true core of TQM is INFORMATION CARRIER PHYSICS.");
        sb.AppendLine("  Q physics provides the substrate (graph Laplacian).");
        sb.AppendLine("  Carrier physics provides the structure (species, ecology, evolution).");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(InformationCarrierAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X008 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
