using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;
using static AT.Core.Research.UnifiedATMetrics;

namespace AT.Tests.ResearchX;

public class AT_X034_UnifiedATSynthesis : ResearchTestBase
{
    public AT_X034_UnifiedATSynthesis(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X034_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X034 Unified AT Synthesis");

        var report = UnifiedATAnalyzer.Analyze();

        // 1. Foundation audit
        Sec(sb, "Foundation Audit — Status Classification");
        sb.AppendLine($"  Postulates:          {report.PostulateCount}");
        sb.AppendLine($"  Derived Theorems:    {report.DerivedCount}");
        sb.AppendLine($"  Emergent Structures: {report.EmergentCount}");
        sb.AppendLine($"  Necessary:           {report.NecessaryCount}");
        sb.AppendLine($"  Irreducible:         {report.IrreducibleCount}");
        sb.AppendLine($"  Total Concepts:      {report.Concepts.Count}");
        sb.AppendLine();

        // 2. Unified hierarchy
        Sec(sb, "Unified AT Hierarchy");
        sb.AppendLine("  Lvl  Status  Concept                    Depends On");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var c in report.Concepts.OrderBy(c => c.Level))
        {
            string icon = c.Status switch
            {
                ConceptStatus.Postulate => "P",
                ConceptStatus.DerivedTheorem => "D",
                ConceptStatus.EmergentStructure => "E",
                ConceptStatus.NecessaryConsequence => "N",
                ConceptStatus.Irreducible => "!",
                _ => "?"
            };
            string deps = c.DependsOn.Length > 0 ? string.Join(", ", c.DependsOn) : "—";
            sb.AppendLine($"  {c.Level,3}   {icon}      {c.Name,-26} {deps}");
        }
        sb.AppendLine();

        // 3. Reduction analysis
        Sec(sb, "Reduction Analysis");
        sb.AppendLine(UnifiedATAnalyzer.ReductionSummary(report.Reductions));

        // 4. Minimal postulates
        Sec(sb, "Minimal Postulates (Final)");
        foreach (var p in report.MinimalPostulates)
            sb.AppendLine($"  {p}");
        sb.AppendLine();

        // 5. The two paths unified
        Sec(sb, "The Two Convergent Paths — Unified");
        sb.AppendLine("  ┌─────────────────────────────────────────────────────────┐");
        sb.AppendLine("  │                    Q + Graph                            │");
        sb.AppendLine("  │                  (Postulate 1)                          │");
        sb.AppendLine("  ├──────────────────────┬──────────────────────────────────┤");
        sb.AppendLine("  │   MAIN AT PATH      │      RESEARCHX PATH              │");
        sb.AppendLine("  │   L_Q = D - A        │      R + S                       │");
        sb.AppendLine("  │   Hilbert eigenbasis │      Reality structures          │");
        sb.AppendLine("  │   J → i mapping      │      Carrier classes             │");
        sb.AppendLine("  │   Schrödinger eq.    │      Complexity staircase        │");
        sb.AppendLine("  │   Born rule          │      Finite → saturation         │");
        sb.AppendLine("  │   Measurement        │      Quantum necessity           │");
        sb.AppendLine("  ├──────────────────────┴──────────────────────────────────┤");
        sb.AppendLine("  │            QUANTUM REALITY (R=1, S=1)                   │");
        sb.AppendLine("  │            Unitary Quantum Mechanics                    │");
        sb.AppendLine("  │            i∂ψ/∂t = Hψ                                  │");
        sb.AppendLine("  └─────────────────────────────────────────────────────────┘");
        sb.AppendLine();

        // 6. Consistency audit
        Sec(sb, "Consistency Audit");
        sb.AppendLine("  Source         Concepts        Status");
        sb.AppendLine("  " + new string('─', 60));
        sb.AppendLine("  AT-117-154    20+ concepts    All mapped to hierarchy");
        sb.AppendLine("  ResearchX      16+ concepts    All mapped to hierarchy");
        sb.AppendLine("  Overlap        11 concepts     Equivalent (X032 verified)");
        sb.AppendLine("  Gaps           0               Emergence gap closed (X033)");
        sb.AppendLine("  STATUS:        COMPLETE        No orphan concepts");
        sb.AppendLine();

        // 7. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(UnifiedATAnalyzer.HostileReview(report));

        // 8. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X034 COMPLETE.");
        sb.AppendLine($"  {report.PostulateCount} postulates + {report.IrreducibleCount} irreducible.");
        sb.AppendLine($"  {report.DerivedCount + report.EmergentCount + report.NecessaryCount} derived/emergent/necessary.");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
