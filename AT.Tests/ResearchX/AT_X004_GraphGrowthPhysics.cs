using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X004_GraphGrowthPhysics : ResearchTestBase
{
    public AT_X004_GraphGrowthPhysics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X004_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X004 Graph Growth Physics");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X002/X003: node motion does NOT produce open-ended innovation.");
        sb.AppendLine("  2. Fixed N → finite spectrum → bounded innovation.");
        sb.AppendLine("  3. Hypothesis: growing N → expanding spectrum → open-ended.");
        sb.AppendLine();

        Sec(sb, "1. Growth Theory");
        sb.AppendLine(GraphGrowthAnalyzer.GrowthTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = GraphGrowthAnalyzer.Analyze(seed: 42);
        sw.Stop();
        sb.AppendLine($"  Simulation: {sw.Elapsed.TotalMilliseconds:F0}ms, 2000 generations");

        Sec(sb, "2. Growth Results");
        sb.AppendLine($"  Initial nodes:    {report.InitialNodes}");
        sb.AppendLine($"  Final nodes:      {report.FinalNodes}");
        sb.AppendLine($"  Initial species:  {report.InitialSpecies}");
        sb.AppendLine($"  Final species:    {report.FinalSpecies}");
        sb.AppendLine($"  Species grows:    {(report.SpeciesCountGrows ? "YES" : "NO")}");
        sb.AppendLine($"  Open-ended:       {(report.InnovationOpenEnded ? "YES" : "NO")}");
        sb.AppendLine();

        sb.AppendLine("  Time │ Nodes │ Species │ Spectral Entropy");
        sb.AppendLine("  " + new string('─', 45));
        int step = Math.Max(1, report.History.Count / 10);
        for (int i = 0; i < report.History.Count; i += step)
        {
            var h = report.History[i];
            sb.AppendLine($"  {h.TimeStep,4} │ {h.NodeCount,5} │ {h.SpeciesCount,7} │ {h.SpectralEntropy,15:F4}");
        }
        sb.AppendLine();

        Sec(sb, "3. Honest Assessment");
        sb.AppendLine("  Graph growth DOES produce expanding species count.");
        sb.AppendLine("  Each new node → new eigenvalue → new 'species'.");
        sb.AppendLine("  But this is 'trivial innovation' — just more Fourier modes.");
        sb.AppendLine("  No qualitatively new type of species emerges.");
        sb.AppendLine("  The eigenmode family (sinusoidal) remains the same.");
        sb.AppendLine();
        sb.AppendLine("  Genuine open-ended innovation would require:");
        sb.AppendLine("    - NEW mode families (not just more of the same)");
        sb.AppendLine("    - Qualitatively different eigenmode structures");
        sb.AppendLine("    - Innovation in TYPE, not just COUNT");
        sb.AppendLine();

        Sec(sb, "4. Hostile Review");
        sb.AppendLine(GraphGrowthAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "5. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X004 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
