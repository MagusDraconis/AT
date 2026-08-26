using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

/// <summary>Internal-consistency audit of Phases 148-155.</summary>
public class AT_PhaseConsistencyAudit : ResearchTestBase
{
    public AT_PhaseConsistencyAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void PhaseConsistency_Audit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("Internal Consistency — Phases 148–155");

        S(sb, "Section A — Classification timeline"); sb.AppendLine(SectionA());
        S(sb, "Section B — Strongest contradiction"); sb.AppendLine(SectionB());
        S(sb, "Section C — Strongest unresolved assumption"); sb.AppendLine(SectionC());
        S(sb, "Section D — Confidence breakdown"); sb.AppendLine(SectionD());

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  overall confidence: {PhaseConsistencyAnalyzer.OverallConfidence():F2}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "PhaseConsistency_Report.txt"), sb.ToString());

        Assert.True(PhaseConsistencyAnalyzer.Timeline().Length >= 8);
        Assert.True(PhaseConsistencyAnalyzer.ConfidenceBreakdown().Length == 8);
        Assert.True(PhaseConsistencyAnalyzer.OverallConfidence() > 0.5);
        Assert.True(PhaseConsistencyAnalyzer.OverallConfidence() < 0.95);
    }

    // ---------------------------------------------------------------------

    private static string SectionA()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Classification timeline (Phases 148–155):");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-6} {1,-30} {2}", "phase", "object", "classification"));
        foreach (var t in PhaseConsistencyAnalyzer.Timeline())
            sb.AppendLine(string.Format("  {0,-6} {1,-30} {2}", t.Phase, t.Object, t.Classification));
        return sb.ToString();
    }

    private static string SectionB()
    {
        var sb = new StringBuilder();
        sb.AppendLine("STRONGEST CONTRADICTION:");
        sb.AppendLine();
        sb.AppendLine("  " + PhaseConsistencyAnalyzer.StrongestContradiction);
        sb.AppendLine();
        sb.AppendLine("  Also noted (minor): Phase 152 called the 3 log-normal classes independent ensembles,");
        sb.AppendLine("  but Phase 153 showed independence is UNTESTABLE (a single universe cannot distinguish");
        sb.AppendLine("  one cascade from three). Independence is true descriptively (distinct mechanisms)");
        sb.AppendLine("  but not PROVABLE statistically.");
        return sb.ToString();
    }

    private static string SectionC()
    {
        var sb = new StringBuilder();
        sb.AppendLine("STRONGEST UNRESOLVED ASSUMPTION:");
        sb.AppendLine();
        sb.AppendLine("  " + PhaseConsistencyAnalyzer.StrongestUnresolvedAssumption);
        sb.AppendLine();
        sb.AppendLine("  Consequence: the structure/content split (QG-042/065) needs a THIRD category to");
        sb.AppendLine("  cleanly describe Koide — 'real structure with contingent origin' — distinct from both");
        sb.AppendLine("  'derived form' and 'coincidental content'. Until this is resolved, the word 'contingent'");
        sb.AppendLine("  is ambiguous across Phases 148 and 154.");
        return sb.ToString();
    }

    private static string SectionD()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Confidence in each classification (0..1):");
        sb.AppendLine();
        sb.AppendLine(string.Format("  {0,-34} {1,6}", "item", "confidence"));
        foreach (var c in PhaseConsistencyAnalyzer.ConfidenceBreakdown())
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-34} {1,6:F2}", c.Item, c.Confidence));
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  OVERALL: {0:F2}", PhaseConsistencyAnalyzer.OverallConfidence()));
        sb.AppendLine();
        sb.AppendLine("  Highest: U(1) derived (0.95). Lowest: 3-class independence (0.55, underdetermined).");
        sb.AppendLine("  The overall ~0.81 reflects two soft spots: (1) the selected-to-contingent flip for the");
        sb.AppendLine("  internal 3, and (2) the contingent ambiguity (real structure vs coincidence).");
        return sb.ToString();
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
