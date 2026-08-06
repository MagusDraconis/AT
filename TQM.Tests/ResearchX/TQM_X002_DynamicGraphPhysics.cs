using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X002_DynamicGraphPhysics : ResearchTestBase
{
    public TQM_X002_DynamicGraphPhysics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X002 Dynamic Graph Physics");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-X001: static graph is the #1 untested assumption.");
        sb.AppendLine("  2. Q charges can move → L_Q(t) changes over time.");
        sb.AppendLine("  3. Assume no new phenomena until demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. Dynamic Graph Theory");
        sb.AppendLine(DynamicGraphAnalyzer.DynamicTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = DynamicGraphAnalyzer.Analyze(seed: 42);
        sw.Stop();
        sb.AppendLine($"  Simulation: {sw.Elapsed.TotalMilliseconds:F0}ms, Q=20, 500 generations");

        Sec(sb, "2. Spectral Evolution");
        sb.AppendLine($"  Initial species count: {report.InitialSpeciesCount}");
        sb.AppendLine($"  Final species count:   {report.FinalSpeciesCount}");
        sb.AppendLine($"  Innovation rate:       {report.InnovationRate:F4} / generation");
        sb.AppendLine($"  Saturation detected:   {(report.InnovationSaturated ? "YES" : "NO")}");
        sb.AppendLine($"  Spectrum stable:       {(report.SpectrumStable ? "YES" : "NO")}");
        sb.AppendLine();

        if (report.History.Count >= 3)
        {
            sb.AppendLine("  Time │ Species │ Spectral Drift │ Graph Entropy");
            sb.AppendLine("  " + new string('─', 55));
            int step = Math.Max(1, report.History.Count / 10);
            for (int i = 0; i < report.History.Count; i += step)
            {
                var h = report.History[i];
                sb.AppendLine($"  {h.TimeStep,4} │ {h.UniqueSpeciesCount,7} │ {h.SpectralDrift,14:F4} │ {h.GraphEntropy,13:F4}");
            }
        }
        sb.AppendLine();

        Sec(sb, "3. TQM Results — What Survives?");
        sb.AppendLine("  Result                     │ Survives? │ Notes");
        sb.AppendLine("  " + new string('─', 65));
        sb.AppendLine("  Species exist              │ ✓         │ Eigenmodes still exist");
        sb.AppendLine("  Fitness law w=r/c          │ ✓         │ Fitness is instantaneous");
        sb.AppendLine("  Innovation bounded?        │ " + (report.InnovationSaturated ? "✓" : "✗") + "         │ " + (report.InnovationSaturated ? "Still bounded" : "Potentially open-ended!"));
        sb.AppendLine("  Hilbert space              │ ✓         │ L_Q(t) symmetric at each t");
        sb.AppendLine("  Schrödinger equation       │ ∼         │ i∂ψ/∂t = L_Q(t)ψ (time-dependent H)");
        sb.AppendLine("  Stationary states          │ ✗         │ Eigenmodes evolve, not stationary");
        sb.AppendLine("  Attractor landscape        │ ✗         │ Landscape changes with graph");
        sb.AppendLine();

        Sec(sb, "4. Hostile Review");
        sb.AppendLine(DynamicGraphAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "5. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X002 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
