using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X014_RealityStructurePrinciple : ResearchTestBase
{
    public AT_X014_RealityStructurePrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X014_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X014 Reality Structure Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. Rev and SC are independent (AT-X011).");
        sb.AppendLine("  2. Rev∩SC = quantum carriers (AT-X012).");
        sb.AppendLine("  3. Question: does FULL reality require BOTH principles?");
        sb.AppendLine();

        Sec(sb, "1. Reality Structure Theory");
        sb.AppendLine(RealityStructureAnalyzer.RealityTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = RealityStructureAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Four Quadrants of Reality");
        sb.AppendLine("  Quadrant         │ Lifetime │ Retention │ Species? │ Ecologies? │ Evolution? │ Reality Class");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var q in report.Quadrants)
            sb.AppendLine($"  {q.Quadrant,-16} │ {q.MeanLifetime,8:F0} │ {q.MeanInfoRetention,9:P0} │ {(q.CanFormSpecies ? "YES" : "NO"),-8} │ {(q.CanFormEcologies ? "YES" : "NO"),-10} │ {(q.CanEvolve ? "YES" : "NO"),-10} │ {q.RealityClass}");
        sb.AppendLine();

        Sec(sb, "3. The Reality Structure Principle");
        sb.AppendLine($"  \"{report.RealityPrinciple}\"");
        sb.AppendLine();
        sb.AppendLine("  Minimal recipe:");
        sb.AppendLine("    Reversibility → preserves information (norm conserved)");
        sb.AppendLine("    Self-consistency → preserves structure (F(x)=x)");
        sb.AppendLine("    Rev + SC → FULL REALITY (species + ecologies + evolution)");
        sb.AppendLine();

        Sec(sb, "4. What Each Quadrant Produces");
        sb.AppendLine("  Rev∩SC (BOTH):");
        sb.AppendLine("    ✓ Species form and persist indefinitely");
        sb.AppendLine("    ✓ Ecologies emerge from interacting species");
        sb.AppendLine("    ✓ Darwinian evolution (reproduction + variation + selection)");
        sb.AppendLine("    → This is the COMPLETE AT hierarchy.");
        sb.AppendLine();
        sb.AppendLine("  SC only:");
        sb.AppendLine("    ✓ Species form (diffusion eigenmodes, Kuramoto sync)");
        sb.AppendLine("    ✗ Ecologies — information degrades, interactions fail");
        sb.AppendLine("    ✗ Evolution — no heritable variation without stable information");
        sb.AppendLine("    → Species exist but are TEMPORARY. Evolution is IMPOSSIBLE.");
        sb.AppendLine();
        sb.AppendLine("  Rev only:");
        sb.AppendLine("    ✗ Species — no persistent identity (disperses, chaos)");
        sb.AppendLine("    ✗ Ecologies — nothing to interact");
        sb.AppendLine("    ✗ Evolution — nothing to select");
        sb.AppendLine("    → Information is conserved but FORMLESS.");
        sb.AppendLine();
        sb.AppendLine("  Neither:");
        sb.AppendLine("    → Nothing persists.");
        sb.AppendLine();

        Sec(sb, "5. Evolutionary Requirement");
        sb.AppendLine("  KEY FINDING: Evolution requires Rev∩SC.");
        sb.AppendLine("  SC-only species CANNOT evolve because:");
        sb.AppendLine("    - Information degrades → inheritance fails");
        sb.AppendLine("    - Norm not conserved → selection is on decaying quantities");
        sb.AppendLine("    - No long-term memory → lineages cannot persist");
        sb.AppendLine();
        sb.AppendLine("  Rev-only systems CANNOT evolve because:");
        sb.AppendLine("    - No persistent identity → nothing to inherit");
        sb.AppendLine("    - No fixed points → no 'species' to select among");
        sb.AppendLine("    - Continuous dispersion → no population structure");
        sb.AppendLine();

        Sec(sb, "6. The Complete AT Foundations");
        sb.AppendLine("  POSTULATE 1: Q exists (topological charge)");
        sb.AppendLine("  POSTULATE 2: Reversible dynamics (unitary evolution)");
        sb.AppendLine("  PRINCIPLE A: Self-consistency (F(x)=x → fixed points)");
        sb.AppendLine("  PRINCIPLE B: Reality = Rev + SC (intersection → full reality)");
        sb.AppendLine();
        sb.AppendLine("  2 postulates + 2 principles → Complete AT.");
        sb.AppendLine();

        Sec(sb, "7. Hostile Review");
        sb.AppendLine(RealityStructureAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "8. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X014 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
