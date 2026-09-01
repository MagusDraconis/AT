using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X003_DynamicTopologyPhaseDiagram : ResearchTestBase
{
    public AT_X003_DynamicTopologyPhaseDiagram(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X003 Dynamic Topology Phase Diagram");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X002: at μ=0.02, quasi-static.");
        sb.AppendLine("  2. Phase transitions may exist at higher μ.");
        sb.AppendLine("  3. Assume no phase transitions until demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. Phase Diagram Theory");
        sb.AppendLine(DynamicTopologyAnalyzer.PhaseTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var diagram = DynamicTopologyAnalyzer.Analyze(seed: 42);
        sw.Stop();
        sb.AppendLine($"  Sweep: {sw.Elapsed.TotalMilliseconds:F0}ms, 8 mobilities × 3 seeds × 300 gens");

        Sec(sb, "2. Mobility Sweep Results");
        sb.AppendLine("  μ     │ Species Δ │ Innov Rate │ Drift    │ ΔEntropy │ Phase");
        sb.AppendLine("  " + new string('─', 70));
        foreach (var r in diagram.Results)
            sb.AppendLine($"  {r.Mobility,5:F2} │ {r.FinalSpecies - r.InitialSpecies,9} │ {r.InnovationRate,10:F4} │ {r.SpectralDrift,8:F4} │ {r.GraphEntropyChange,8:F4} │ {r.Phase}");
        sb.AppendLine();

        Sec(sb, "3. Phase Diagram");
        sb.AppendLine($"  Phases detected: [{string.Join(" → ", diagram.Phases)}]");
        sb.AppendLine($"  Critical μ₁ (static→quasi-static): {diagram.CriticalMobility1}");
        sb.AppendLine($"  Critical μ₂ (quasi-static→dynamic): {diagram.CriticalMobility2}");
        sb.AppendLine($"  Open-ended detected: {(diagram.OpenEndedDetected ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "4. AT Results by Phase");
        sb.AppendLine("  Result              │ Phase I │ Phase II │ Phase III │ Phase IV");
        sb.AppendLine("  " + new string('─', 70));
        sb.AppendLine("  Species exist       │   ✓     │    ✓     │    ∼      │    ?");
        sb.AppendLine("  Fitness law         │   ✓     │    ✓     │    ✓      │    ?");
        sb.AppendLine("  Innovation bounded  │   ✓     │    ✓     │    ∼      │    ✗?");
        sb.AppendLine("  Stationary states   │   ✓     │    ∼     │    ✗      │    ✗");
        sb.AppendLine("  Attractor landscape │   ✓     │    ∼     │    ✗      │    ✗");
        sb.AppendLine("  Hilbert space       │   ✓     │    ✓     │    ✓      │    ✓");
        sb.AppendLine("  Schrödinger         │   ✓     │    ✓     │    ✓      │    ✓");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(DynamicTopologyAnalyzer.HostileReview(diagram));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {diagram.Classification}");
        sb.AppendLine($"  {diagram.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X003 complete. Classification: {diagram.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
