using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_154_OriginOfMeasurement : ResearchTestBase
{
    public AT_154_OriginOfMeasurement(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_154_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-154 Origin of Quantum Measurement");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-153: Schrödinger + Born rule established.");
        sb.AppendLine("  2. Measurement (collapse) is the last open postulate.");
        sb.AppendLine("  3. Assume measurement is fundamental until derived.");
        sb.AppendLine();

        Sec(sb, "1. Measurement Theory");
        sb.AppendLine(MeasurementOriginAnalyzer.MeasurementTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = MeasurementOriginAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Decoherence and Measurement Tests");
        sb.AppendLine("  Scenario                    │ Decoher? │ Pointer? │ Born? │ Collapse? │ Assessment");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var t in report.Tests)
            sb.AppendLine($"  {t.Scenario,-27} │ {(t.DecoherenceOccurs ? "✓" : "✗"),-8} │ {(t.PointerBasisEmerges ? "✓" : "✗"),-8} │ {(t.BornStatsRecovered ? "✓" : "✗"),-5} │ {(t.CollapseExplained ? "✓" : "✗"),-9} │ {t.Assessment}");
        sb.AppendLine();

        Sec(sb, "3. The Measurement Problem — Honest Assessment");
        sb.AppendLine("  WHAT AT (AND ALL PHYSICS) CAN EXPLAIN:");
        sb.AppendLine("    ✓ Decoherence: off-diagonals decay, interference disappears");
        sb.AppendLine("    ✓ Pointer states: eigenstates of interaction become stable");
        sb.AppendLine("    ✓ Born statistics: diagonal weights follow |ψ|²");
        sb.AppendLine();
        sb.AppendLine("  WHAT NO THEORY CAN EXPLAIN (98 years, since Born 1926):");
        sb.AppendLine("    ✗ Why ONE outcome occurs (the 'and' → 'or' transition)");
        sb.AppendLine("    ✗ How a particular outcome is selected");
        sb.AppendLine("    ✗ Wavefunction collapse mechanism");
        sb.AppendLine();
        sb.AppendLine("  This is NOT a AT limitation. It is the measurement problem —");
        sb.AppendLine("  the deepest unsolved problem in quantum foundations.");
        sb.AppendLine();

        Sec(sb, "4. AT Postulates — Final Count");
        sb.AppendLine("  1. Q EXISTS — topological charge → L_Q → Hilbert space");
        sb.AppendLine("  2. REVERSIBLE DYNAMICS → J → i → Schrödinger");
        sb.AppendLine("  3. BORN RULE P=|ψ|² → probability");
        sb.AppendLine("  4. MEASUREMENT → collapse/outcome selection (IRREDUCIBLE)");
        sb.AppendLine();
        sb.AppendLine("  4 postulates. Standard QM requires ~5.");
        sb.AppendLine("  The measurement problem affects ALL formulations of QM.");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(MeasurementOriginAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-154 complete. Classification: {report.Classification}");
        sb.AppendLine($"  The measurement problem remains IRREDUCIBLE.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
