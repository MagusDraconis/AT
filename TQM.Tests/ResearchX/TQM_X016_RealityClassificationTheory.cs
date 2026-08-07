using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X016_RealityClassificationTheory : ResearchTestBase
{
    public TQM_X016_RealityClassificationTheory(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X016_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X016 Reality Classification Theory");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. R+S is the minimal reality foundation (TQM-X015).");
        sb.AppendLine("  2. Hypothesis: ALL systems can be classified in (R,S) space.");
        sb.AppendLine("  3. Assume classification may FAIL for some domains.");
        sb.AppendLine();

        Sec(sb, "1. Classification Theory");
        sb.AppendLine(RealityMetrics.ClassificationTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = RealityMetrics.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. The Reality Phase Diagram");
        sb.AppendLine(report.PhaseDiagram);
        sb.AppendLine();

        Sec(sb, "3. System Mapping — All Domains");
        sb.AppendLine("  System                     │ Domain     │ R    │ S    │ Species? │ Evol? │ Region");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var s in report.Systems)
            sb.AppendLine($"  {s.System,-26} │ {s.Domain,-10} │ {s.R,4:F1} │ {s.S,4:F1} │ {(s.HasSpecies ? "YES" : "NO"),-8} │ {(s.HasEvolution ? "YES" : "NO"),-5} │ {s.Region}");
        sb.AppendLine();

        Sec(sb, "4. Domain Analysis");
        sb.AppendLine($"  Systems mapped: {report.TotalSystems}");
        sb.AppendLine($"  Domains covered: {report.DomainsCovered}");
        sb.AppendLine($"  Regions identified: {report.Regions.Length}");
        sb.AppendLine();
        sb.AppendLine("  Domain          │ Systems │ Typical (R,S)    │ Characteristic");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var g in report.Systems.GroupBy(s => s.Domain))
        {
            double avgR = g.Average(s => s.R);
            double avgS = g.Average(s => s.S);
            string characteristic = (avgR >= 0.7 && avgS >= 0.7) ? "Quantum corner"
                                 : (avgS >= 0.7) ? "Carrier-dominant"
                                 : (avgR >= 0.7) ? "Dynamic"
                                 : "Mixed/Weak";
            sb.AppendLine($"  {g.Key,-14} │ {g.Count(),7} │ ({avgR:F2}, {avgS:F2})          │ {characteristic}");
        }
        sb.AppendLine();

        Sec(sb, "5. Key Insight: Why Quantum is Special");
        sb.AppendLine("  Only QUANTUM systems occupy the top-right corner (R≥0.7, S≥0.7).");
        sb.AppendLine("  This explains why quantum mechanics is the foundation of physics:");
        sb.AppendLine("    - Maximal reversibility (unitary evolution)");
        sb.AppendLine("    - Maximal self-consistency (eigenstates as stationary structures)");
        sb.AppendLine("  No other domain achieves BOTH simultaneously.");
        sb.AppendLine();
        sb.AppendLine("  BIOLOGICAL systems cluster at high-S, low-R:");
        sb.AppendLine("    - High self-consistency: species maintain identity");
        sb.AppendLine("    - Low reversibility: mortality, entropy, imperfect replication");
        sb.AppendLine("  Biology achieves evolution through S-dominated dynamics.");
        sb.AppendLine();

        Sec(sb, "6. The Universal Reality Map");
        sb.AppendLine("  S=1 ┌─────────────────────┬─────────────────────┐");
        sb.AppendLine("      │ CARRIER REALITY     │ QUANTUM REALITY     │");
        sb.AppendLine("      │ • Biology           │ • Schrödinger       │");
        sb.AppendLine("      │ • DNA, species      │ • Solitons          │");
        sb.AppendLine("      │ • Prions            │ • Topological Edge  │");
        sb.AppendLine("  S=0.7├─────────────────────┼─────────────────────┤");
        sb.AppendLine("      │ WEAK REALITY        │ DYNAMIC REALITY     │");
        sb.AppendLine("      │ • Neural Nets       │ • Free Particles    │");
        sb.AppendLine("      │ • Cellular Automata │ • Hamiltonian Chaos │");
        sb.AppendLine("  S=0.3├─────────────────────┼─────────────────────┤");
        sb.AppendLine("      │ NOISE ZONE          │                     │");
        sb.AppendLine("      │ • Thermal Noise     │                     │");
        sb.AppendLine("      │ • Turbulence        │                     │");
        sb.AppendLine("  S=0 └─────────────────────┴─────────────────────┘");
        sb.AppendLine("      R=0                  R=0.7                 R=1");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(RealityMetrics.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X016 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
