using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X028_FiniteUniverseConsequences : ResearchTestBase
{
    public TQM_X028_FiniteUniverseConsequences(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X028_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X028 Finite Universe Consequences");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-X027: all finite systems saturate. L6 requires infinity.");
        sb.AppendLine("  2. Our universe has finite entropy → finite states.");
        sb.AppendLine("  3. Question: what are the consequences?");
        sb.AppendLine();

        var report = FiniteUniverseAnalyzer.Analyze();

        Sec(sb, "1. Complexity Ceilings Across Domains");
        sb.AppendLine("  Domain                  │ Max States   │ Bound Source        │ Reachable? │ Assessment");
        sb.AppendLine("  " + new string('─', 110));
        foreach (var c in report.Ceilings)
            sb.AppendLine($"  {c.Domain,-23} │ 10^{Math.Log10(c.EstimatedMaximum),4:F0}       │ {c.Bound,-20} │ {(c.PracticallyReachable ? "YES" : "NO"),-10} │ {c.Assessment}");
        sb.AppendLine();
        sb.AppendLine($"  All domains have ceilings: {(report.AllDomainsHaveCeilings ? "YES" : "NO")}");
        sb.AppendLine($"  Practically relevant: {(report.CeilingsArePracticallyRelevant ? "YES — for some domains" : "NO")}");
        sb.AppendLine();

        Sec(sb, "2. The Spectrum of Bounds");
        sb.AppendLine("  ASTRONOMICALLY FAR (never reachable):");
        sb.AppendLine("    • Physical states: 10^120 — irrelevant on any timescale");
        sb.AppendLine("    • Biological species: 10^30 — evolution explores <10^18");
        sb.AppendLine("    • Mathematical theorems: 10^20 — infinite via meta-math");
        sb.AppendLine();
        sb.AppendLine("  PRACTICALLY REACHABLE (may approach within civilization):");
        sb.AppendLine("    • Human knowledge: 10^18 bits — ~10^6 years at current rate");
        sb.AppendLine("    • Scientific theories: 10^12 — diminishing returns observed");
        sb.AppendLine("    • Technological inventions: 10^9 — innovation rate slowing");
        sb.AppendLine("    • AI capability: 10^15 — bounded by physical computation limits");
        sb.AppendLine();

        Sec(sb, "3. The Practical Truth");
        sb.AppendLine("  MATHEMATICALLY: All finite systems have ceilings (X027).");
        sb.AppendLine("  PRACTICALLY: Most ceilings are astronomically far.");
        sb.AppendLine("  L5 (Evolution) is EFFECTIVELY L6 for most domains.");
        sb.AppendLine();
        sb.AppendLine("  The finite-universe principle is TRUE but often IRRELEVANT.");
        sb.AppendLine("  10^120 is so large that 'saturation' means nothing on any");
        sb.AppendLine("  timescale shorter than the heat death of the universe.");
        sb.AppendLine();

        Sec(sb, "4. Where the Ceilings Matter");
        sb.AppendLine("  KNOWLEDGE GROWTH:");
        sb.AppendLine("    Finite vocabulary × finite sentence length");
        sb.AppendLine("    → finite number of expressible theories");
        sb.AppendLine("    → scientific discovery MUST eventually saturate");
        sb.AppendLine("    → already seeing diminishing returns in fundamental physics");
        sb.AppendLine();
        sb.AppendLine("  TECHNOLOGICAL INNOVATION:");
        sb.AppendLine("    Finite matter configurations → finite inventions");
        sb.AppendLine("    → combinatorial explosion but still bounded");
        sb.AppendLine("    → innovation rate already slowing in many fields");
        sb.AppendLine();
        sb.AppendLine("  ARTIFICIAL INTELLIGENCE:");
        sb.AppendLine("    Finite matter + finite energy + finite time");
        sb.AppendLine("    → finite computation (Landauer, Bremermann limits)");
        sb.AppendLine("    → Strong AI has a THEORETICAL ceiling");
        sb.AppendLine("    → Not reached yet, but it EXISTS");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(FiniteUniverseAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X028 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
