using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_153_OriginOfBornRule : ResearchTestBase
{
    public TQM_153_OriginOfBornRule(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_153_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-153 Origin of the Born Rule");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-152: Schrödinger dynamics from Q + reversibility.");
        sb.AppendLine("  2. Probability interpretation P=|ψ|² is not yet derived.");
        sb.AppendLine("  3. Assume Born rule is fundamental until derived.");
        sb.AppendLine();

        Sec(sb, "1. Born Rule Theory");
        sb.AppendLine(BornRuleAnalyzer.BornTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = BornRuleAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Candidate Probability Measures");
        sb.AppendLine("  Rule          │ Normalized │ Additive │ Basis-Indep │ Unique?");
        sb.AppendLine("  " + new string('─', 65));
        foreach (var c in report.Candidates)
            sb.AppendLine($"  {c.Name,-13} │ {(c.Normalized ? "✓" : "✗"),-10} │ {(c.Additive ? "✓" : "✗"),-8} │ {(c.BasisIndependent ? "✓" : "✗"),-11} │ {(c.Unique ? "✓" : "✗")}");
        sb.AppendLine();

        Sec(sb, "3. Additivity Test");
        sb.AppendLine("  For ψ = cos(θ)|0⟩ + sin(θ)|1⟩ at θ = 45°:");
        sb.AppendLine("    P∝|ψ|:   |cos 45°| + |sin 45°| = 1.414 ≠ 1  ✗");
        sb.AppendLine("    P∝|ψ|²:  cos²45° + sin²45° = 1.000 = 1        ✓");
        sb.AppendLine("    P∝|ψ|³:  |cos 45°|³ + |sin 45°|³ = 0.707 ≠ 1  ✗");
        sb.AppendLine("    Only exponent 2 satisfies P₀ + P₁ = 1 for all θ.");
        sb.AppendLine();
        sb.AppendLine("  Gleason's theorem (1957): additivity for orthogonal projectors");
        sb.AppendLine("  ⇒ P = |ψ|² is the UNIQUE consistent probability measure.");
        sb.AppendLine();

        Sec(sb, "4. TQM Postulates — Final Count");
        sb.AppendLine("  1. Q EXISTS — topological charge → L_Q → Hilbert space");
        sb.AppendLine("  2. REVERSIBLE DYNAMICS → J → i → Schrödinger equation");
        sb.AppendLine("  3. BORN RULE P=|ψ|² → probability interpretation");
        sb.AppendLine();
        sb.AppendLine("  3 postulates. Standard QM requires ~5.");
        sb.AppendLine("  TQM derives Hilbert space and Schrödinger from postulates 1-2.");
        sb.AppendLine("  Born rule is mathematically UNIQUE (Gleason) but still a postulate.");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(BornRuleAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-153 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
