using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X049_GaugeGroupSelection : ResearchTestBase
{
    public TQM_X049_GaugeGroupSelection(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X049_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X049 Selection of Gauge Symmetry");

        var groups = GaugeGroupSelectionAnalyzer.EvaluateGroups();

        // 1. Candidate groups
        Sec(sb, "Candidate Gauge Groups");
        sb.AppendLine("  Group              Dim  Rank  Anomaly-Free?  Complexity");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var g in groups)
        {
            string af = g.IsAnomalyFree ? "✓" : "✗";
            sb.AppendLine($"  {g.Group,-18} {g.Dimension,4}  {g.Rank,4}  {af,13}  {g.ComplexityScore,10:F1}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {groups.Count} groups evaluated. Many are anomaly-free.");
        sb.AppendLine($"  SM group (SU(3)×SU(2)×U(1)): dimension 12, rank 4.");
        sb.AppendLine();

        // 2. Complexity ranking
        Sec(sb, "Complexity Ranking");
        var ranked = groups.OrderByDescending(g => g.ComplexityScore).ToList();
        for (int i = 0; i < ranked.Count; i++)
        {
            string marker = ranked[i].Group.Contains("SU(3)×SU(2)×U(1)") ? " ← SM" : "";
            sb.AppendLine($"  {i + 1,2}. {ranked[i].Group,-20} Score: {ranked[i].ComplexityScore,5:F1}{marker}");
        }
        sb.AppendLine();
        sb.AppendLine($"  SM group ranks #{ranked.FindIndex(g => g.Group.Contains("SU(3)×SU(2)×U(1)")) + 1}/{groups.Count}.");
        sb.AppendLine("  Larger groups score HIGHER (more generators = more complexity).");
        sb.AppendLine("  If complexity maximization were the only criterion, E8 would win.");
        sb.AppendLine();

        // 3. Why SM?
        Sec(sb, "Why the Standard Model Group?");
        sb.AppendLine("  SU(3)×SU(2)×U(1) is:");
        sb.AppendLine("    • The MAXIMAL product of the smallest simple Lie groups.");
        sb.AppendLine("    • U(1): 1 generator (minimal Abelian).");
        sb.AppendLine("    • SU(2): 3 generators (minimal non-Abelian).");
        sb.AppendLine("    • SU(3): 8 generators (minimal confining).");
        sb.AppendLine("    • Anomaly-free with minimal matter content.");
        sb.AppendLine("    • Asymptotically free (QCD) + chiral (weak) + Abelian (EM).");
        sb.AppendLine();
        sb.AppendLine("  BUT: This is a POST-HOC observation, not a derivation.");
        sb.AppendLine("  TQM does NOT uniquely predict this group.");
        sb.AppendLine();

        // 4. The analysis
        Sec(sb, "Honest Assessment");
        sb.AppendLine(GaugeGroupSelectionAnalyzer.TheAnalysis());

        // 5. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(GaugeGroupSelectionAnalyzer.HostileReview());

        // 6. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X049 COMPLETE.");
        sb.AppendLine($"  Classification: A — No Preferred Gauge Group.");
        sb.AppendLine($"  SU(3)×SU(2)×U(1) is CONSISTENT with TQM but NOT DERIVED.");
        sb.AppendLine($"  The gauge group selection problem is the largest");
        sb.AppendLine($"  remaining open challenge in the TQM program.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
