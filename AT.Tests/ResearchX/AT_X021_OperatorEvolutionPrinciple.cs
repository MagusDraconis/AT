using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X021_OperatorEvolutionPrinciple : ResearchTestBase
{
    public AT_X021_OperatorEvolutionPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X021_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X021 Operator Evolution Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X020: graph evolution ≠ new carrier classes.");
        sb.AppendLine("  2. Hypothesis: operator evolution IS necessary for L6.");
        sb.AppendLine("  3. Assume operator evolution is irrelevant until proven.");
        sb.AppendLine();

        Sec(sb, "1. Operator Evolution Theory");
        sb.AppendLine(OperatorEvolutionAnalyzer.OperatorTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = OperatorEvolutionAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Operator Family Registry");
        sb.AppendLine("  Operator Family        │ Carrier Class            │ Capacity │ Reachable?");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var f in report.Families)
            sb.AppendLine($"  {f.Name,-22} │ {f.CarrierClass,-25} │ {f.SpeciesCapacity,8} │ {(f.IsReachableFromLQ ? "YES" : "NO")}");
        sb.AppendLine();
        sb.AppendLine($"  Total families: {report.TotalFamilies}");
        sb.AppendLine($"  Reachable from L_Q: {report.ReachableFamilies}");
        sb.AppendLine($"  Operator space bounded: {(report.OperatorSpaceIsBounded ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "3. The Deepest Bottleneck Chain");
        sb.AppendLine("  Level 5 (Evolution):");
        sb.AppendLine("    Operates WITHIN fixed operator families.");
        sb.AppendLine("    Species diversify within Fourier or soliton classes.");
        sb.AppendLine("    Innovation saturates at family capacity.");
        sb.AppendLine();
        sb.AppendLine("  Level 6 (Open-Ended):");
        sb.AppendLine("    Requires TRANSITIONS between operator families.");
        sb.AppendLine("    Species must access NEW operator families.");
        sb.AppendLine("    Carrier class diversity = operator family diversity.");
        sb.AppendLine();
        sb.AppendLine("  THE GAP:");
        sb.AppendLine("    Within-family evolution → BOUNDED (saturates).");
        sb.AppendLine("    Between-family evolution → POTENTIALLY UNBOUNDED.");
        sb.AppendLine("    No mechanism exists for between-family transitions.");
        sb.AppendLine();

        Sec(sb, "4. The Nonlinearity Bridge");
        sb.AppendLine("  THE ONLY KNOWN MECHANISM:");
        sb.AppendLine("    L(ψ) = L_Q + α|ψ|²");
        sb.AppendLine("    α = 0: Linear regime → Fourier eigenmodes");
        sb.AppendLine("    α small: Weakly nonlinear → perturbed eigenmodes");
        sb.AppendLine("    α large: Strongly nonlinear → solitons");
        sb.AppendLine();
        sb.AppendLine("  α is a CONTINUOUS parameter connecting operator families.");
        sb.AppendLine("  If species can modulate α (population density, energy),");
        sb.AppendLine("    they CAN explore different operator regimes.");
        sb.AppendLine();
        sb.AppendLine("  BUT:");
        sb.AppendLine("    - α is determined by SYSTEM parameters, not species activity");
        sb.AppendLine("    - α-space is bounded for any finite-energy system");
        sb.AppendLine("    - Only 2 families (linear, NLS) are connected via α");
        sb.AppendLine("    - Other families (magnetic, hypergraph) require external changes");
        sb.AppendLine();

        Sec(sb, "5. The L6 Bottleneck — Final Form");
        sb.AppendLine("  L0-L5:   WITHIN operator family (bounded).");
        sb.AppendLine("  L5→L6:   BETWEEN operator families (requires evolution).");
        sb.AppendLine();
        sb.AppendLine("  L6 requires ONE of:");
        sb.AppendLine("    A. Species-modulated α (density → nonlinearity)");
        sb.AppendLine("    B. External operator changes (magnetic field, 3-body coupling)");
        sb.AppendLine("    C. Operator space exploration via meta-dynamics");
        sb.AppendLine();
        sb.AppendLine("  NONE of these have been tested in AT.");
        sb.AppendLine("  The operator evolution barrier is the DEEPEST L6 bottleneck.");
        sb.AppendLine();

        Sec(sb, "6. The Complete Bottleneck Hierarchy");
        sb.AppendLine("  Level  | Bottleneck              | Found By");
        sb.AppendLine("  " + new string('─', 50));
        sb.AppendLine("  1      | Finite spectrum         | AT-138");
        sb.AppendLine("  2      | Static graph            | AT-X001");
        sb.AppendLine("  3      | Fixed carrier classes   | AT-X019");
        sb.AppendLine("  4      | Graph ≠ operator type   | AT-X020");
        sb.AppendLine("  5      | OPERATOR FAMILY BARRIER | AT-X021 ← DEEPEST");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(OperatorEvolutionAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X021 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
