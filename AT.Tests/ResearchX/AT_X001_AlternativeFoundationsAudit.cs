using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X001_AlternativeFoundationsAudit : ResearchTestBase
{
    public AT_X001_AlternativeFoundationsAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X001 Alternative Foundations Audit");

        var report = TheoryAuditAnalyzer.PerformAudit();

        Sec(sb, "Hidden Assumptions Inventory");
        sb.AppendLine("  Assumption              │ Import │ Depend │ Tested │ Novelty │ Recommendation");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var a in report.Assumptions.OrderByDescending(x => x.ImportanceScore))
            sb.AppendLine($"  {a.Name,-23} │ {a.ImportanceScore,6} │ {a.DependenceScore,6} │ {(a.WasTested ? "✓" : "✗"),-6} │ {a.NoveltyPotential,7} │ {a.Recommendation}");
        sb.AppendLine();

        Sec(sb, "Alternative Operators");
        sb.AppendLine("  Operator              │ AT Survives? │ What Breaks");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var alt in report.Alternatives)
            sb.AppendLine($"  {alt.Name,-22} │ {(alt.ATSurvives ? "YES" : "NO"),-13} │ {alt.WhatBreaks}");
        sb.AppendLine();

        Sec(sb, "Most Critical Untested Assumptions");
        foreach (var a in report.MostCriticalAssumptions)
            sb.AppendLine($"  • {a}");
        sb.AppendLine();

        Sec(sb, "Path Dependencies");
        foreach (var d in report.PathDependencies)
            sb.AppendLine($"  • {d}");
        sb.AppendLine();

        Sec(sb, "Most Promising Future Directions");
        foreach (var d in report.MostPromisingDirections)
            sb.AppendLine($"  • {d}");
        sb.AppendLine();

        Sec(sb, "Verdict");
        sb.AppendLine($"  Framework is biased: {(report.FrameworkIsBiased ? "YES" : "NO")}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X001 complete. Audit finished.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
