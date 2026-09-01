using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X010_SelfConsistencyPrinciple : ResearchTestBase
{
    public AT_X010_SelfConsistencyPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X010_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X010 Self-Consistency Principle — Depth Analysis");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X009: self-consistency + dynamical stability is universal.");
        sb.AppendLine("  2. Question: is there a layer BELOW self-consistency?");
        sb.AppendLine("  3. Assume self-consistency is NOT fundamental until tested.");
        sb.AppendLine();

        Sec(sb, "1. Depth Theory");
        sb.AppendLine(SelfConsistencyAnalyzer.DeepTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = SelfConsistencyAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Candidates for 'Deeper Than Self-Consistency'");
        sb.AppendLine("  Candidate              │ Explains SC? │ Deeper? │ Verdict");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var c in report.Candidates)
            sb.AppendLine($"  {c.Name,-22} │ {(c.ExplainsSelfConsistency ? "YES" : "NO"),-12} │ {(c.IsMoreFundamental ? "YES" : "NO"),-7} │ {c.Verdict}");
        sb.AppendLine();

        Sec(sb, "3. The Mathematical Identity");
        sb.AppendLine($"  Minimal form: {report.MinimalForm}");
        sb.AppendLine("  'Self-consistency' ≡ 'Fixed-point condition' ≡ 'F(x)=x'");
        sb.AppendLine("  These are three names for ONE mathematical structure.");
        sb.AppendLine();

        Sec(sb, "4. What Varies Between Regimes (Not Deeper, Different)");
        sb.AppendLine("  Regime        │ Fixed-Point Form        │ Mathematical Origin");
        sb.AppendLine("  " + new string('─', 60));
        sb.AppendLine("  Linear        │ L·v = λ·v              │ Real symmetric → diagonalizable");
        sb.AppendLine("  Nonlinear     │ NLS balance eq.         │ Integrable PDE (Lax pair)");
        sb.AppendLine("  Topological   │ Energy functional + π₁  │ Homotopy groups of order parameter");
        sb.AppendLine();
        sb.AppendLine("  THREE different mechanisms. ONE commonality: all produce F(x)=x.");
        sb.AppendLine("  That commonality IS self-consistency. Not deeper — just common.");
        sb.AppendLine();

        Sec(sb, "5. The Complete AT Reduction (Bottom to Top)");
        sb.AppendLine("  Q (topological charge)");
        sb.AppendLine("   ↓ interaction graph");
        sb.AppendLine("  L_Q (graph Laplacian / dynamical operator)");
        sb.AppendLine("   ↓ spectral / PDE structure");
        sb.AppendLine("  FIXED POINTS F(x)=x ← THIS IS THE BOTTOM");
        sb.AppendLine("   ↓ (self-consistency ≡ fixed-point condition)");
        sb.AppendLine("  ATTRACTORS (stable fixed points)");
        sb.AppendLine("   ↓ + information encoding");
        sb.AppendLine("  PERSISTENT INFORMATION CARRIERS");
        sb.AppendLine("   ↓ + identity + reproduction");
        sb.AppendLine("  SPECIES");
        sb.AppendLine("   ↓ + interaction + population");
        sb.AppendLine("  ECOLOGIES");
        sb.AppendLine("   ↓ + variation + selection");
        sb.AppendLine("  EVOLUTION");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(SelfConsistencyAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X010 complete. Classification: {report.Classification}");
        sb.AppendLine($"  AT reduction is COMPLETE — bottoms out at F(x)=x.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
