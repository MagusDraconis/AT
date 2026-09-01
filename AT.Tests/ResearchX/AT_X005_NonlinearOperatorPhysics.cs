using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X005_NonlinearOperatorPhysics : ResearchTestBase
{
    public AT_X005_NonlinearOperatorPhysics(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X005_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X005 Nonlinear Operator Physics");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X001: linearity is the #3 untested assumption.");
        sb.AppendLine("  2. All AT (117-154) uses linear L_Q.");
        sb.AppendLine("  3. Assume nonlinearity only adds small corrections.");
        sb.AppendLine();

        Sec(sb, "1. Nonlinear Theory");
        sb.AppendLine(NonlinearOperatorAnalyzer.NonlinearTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = NonlinearOperatorAnalyzer.Analyze(seed: 42);
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Nonlinearity Sweep");
        sb.AppendLine("  α     │ Regime               │ Eigen? │ Super? │ Hilbert? │ New? │ Solitons");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var r in report.Results)
            sb.AppendLine($"  {r.Alpha,5:F2} │ {r.Regime,-20} │ {(r.EigenmodesSurvive ? "✓" : "✗"),-6} │ {(r.SuperpositionSurvives ? "✓" : "✗"),-6} │ {(r.HilbertSpaceSurvives ? "✓" : "✗"),-8} │ {(r.NewStructuresEmerge ? "✓" : "✗"),-4} │ {r.SolitonCount,4}");
        sb.AppendLine();

        Sec(sb, "3. What Breaks vs What Survives");
        sb.AppendLine("  BREAKS IMMEDIATELY (α > 0):");
        sb.AppendLine("    ✗ Superposition principle");
        sb.AppendLine("    ✗ Hilbert space vector structure");
        sb.AppendLine("    ✗ Orthogonal eigenmodes");
        sb.AppendLine("    ✗ Fourier species (sinusoidal modes)");
        sb.AppendLine("    ✗ Schrödinger correspondence");
        sb.AppendLine("    ✗ Quantum correspondence (all of AT-149-154)");
        sb.AppendLine();
        sb.AppendLine("  SURVIVES:");
        sb.AppendLine("    ✓ Q charge existence");
        sb.AppendLine("    ✓ Q interaction graph");
        sb.AppendLine("    ✓ Fitness law w = r/c");
        sb.AppendLine("    ✓ Selection / differential survival");
        sb.AppendLine("    ✓ Evolution framework (reproduction, variation, selection)");
        sb.AppendLine();

        Sec(sb, "4. New Physics Possibilities");
        sb.AppendLine("  At α ≥ 0.10:");
        sb.AppendLine("    • Solitons (self-localized persistent structures)");
        sb.AppendLine("    • Nonlinear eigenmodes (new species TYPES, not just COUNT)");
        sb.AppendLine("    • Pattern formation (Turing-like instability)");
        sb.AppendLine("    • Mode locking and frequency mixing");
        sb.AppendLine("    • Potentially open-ended innovation (nonlinear landscapes)");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(NonlinearOperatorAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X005 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
