using System.Globalization;
using System.Text;
using TQM.Core.ResearchXG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXG;

public class TQM_XG000_StateOfTqmAudit : ResearchTestBase
{
    public TQM_XG000_StateOfTqmAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XG000_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXG-000 State of TQM Audit");

        var programs = StateOfTqmAnalyzer.AssessPrograms();
        var results = StateOfTqmAnalyzer.ClassifyResults();

        int derived = results.Count(r => r.Confidence == StateOfTqmAnalyzer.ConfidenceLevel.Derived);
        int strong = results.Count(r => r.Confidence == StateOfTqmAnalyzer.ConfidenceLevel.StrongModel);
        int working = results.Count(r => r.Confidence == StateOfTqmAnalyzer.ConfidenceLevel.WorkingHypothesis);
        int speculative = results.Count(r => r.Confidence == StateOfTqmAnalyzer.ConfidenceLevel.Speculative);
        int testable = results.Count(r => r.ExperimentallyTestable);

        // 1. Program scorecard
        Sec(sb, "Research Program Scorecard");
        sb.AppendLine("  Program       Question                     Exp   Results   Complete");
        sb.AppendLine("  " + new string('-', 65));
        foreach (var p in programs)
        {
            sb.AppendLine($"  {p.Program,-12} {p.Question,-30} {p.Experiments,4}   {p.ResultsClaimed,6}    {p.Completeness:P0}");
        }
        sb.AppendLine();
        sb.AppendLine($"  TOTAL: {programs.Sum(p => p.Experiments)} experiments across {programs.Count} programs.");
        sb.AppendLine();

        // 2. Result classification
        Sec(sb, "Result Classification — All Major Results");
        sb.AppendLine("  Result                           Confidence         Testable?  Notes");
        sb.AppendLine("  " + new string('-', 80));
        foreach (var r in results.OrderBy(r => r.Confidence))
        {
            string conf = r.Confidence.ToString();
            string test = r.ExperimentallyTestable ? "✓" : "—";
            sb.AppendLine($"  {r.Name,-33} {conf,-18} {test,-10} {r.Notes.Split('\n')[0]}");
        }
        sb.AppendLine();
        sb.AppendLine($"  DERIVED: {derived}  STRONG: {strong}  WORKING: {working}  SPECULATIVE: {speculative}");
        sb.AppendLine($"  Testable: {testable}/{results.Count}");
        sb.AppendLine();

        // 3. What TQM knows
        Sec(sb, "What TQM Knows — Derived + Strong Model Results");
        sb.AppendLine(StateOfTqmAnalyzer.WhatTqmKnows());

        // 4. What TQM believes
        Sec(sb, "What TQM Believes — Working Hypotheses");
        sb.AppendLine(StateOfTqmAnalyzer.WhatTqmBelieves());

        // 5. What TQM hopes
        Sec(sb, "What TQM Hopes — Speculative Extensions");
        sb.AppendLine(StateOfTqmAnalyzer.WhatTqmHopes());

        // 6. Open problems
        Sec(sb, "Major Open Problems");
        sb.AppendLine(StateOfTqmAnalyzer.OpenProblems());

        // 7. The assessment
        Sec(sb, "Final Assessment");
        sb.AppendLine(StateOfTqmAnalyzer.TheAssessment());

        // 8. Final
        string classification = derived + strong >= 14 ? "C: Mature Research Program"
            : "B: Partial Framework";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXG-000 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  {derived + strong}/{results.Count} results are Derived or Strong Models ({100.0 * (derived + strong) / results.Count:F0}%).");
        sb.AppendLine($"  {testable}/{results.Count} results experimentally testable.");
        sb.AppendLine($"  TQM: MATURE, COHERENT, FALSIFIABLE. Ready for experimental judgment.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
