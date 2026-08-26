using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X038_OriginOfMeasurement : ResearchTestBase
{
    public AT_X038_OriginOfMeasurement(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X038_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X038 Origin of Measurement from Individuation");

        var report = MeasurementAnalyzer.Analyze();

        // 1. The problem
        Sec(sb, "The Measurement Problem");
        sb.AppendLine("  |ψ⟩ = a|0⟩ + b|1⟩  →  |0⟩ with probability |a|²");
        sb.AppendLine("                        OR |1⟩ with probability |b|²");
        sb.AppendLine();
        sb.AppendLine("  This is NOT unitary evolution.");
        sb.AppendLine("  Can it be DERIVED rather than postulated?");
        sb.AppendLine();

        // 2. Measurement models audit
        Sec(sb, "Measurement Models — Hostile Audit");
        sb.AppendLine(MeasurementAnalyzer.ModelReport(report.Models));

        // 3. Detailed failures
        Sec(sb, "Why Alternatives Fail");
        sb.AppendLine(MeasurementAnalyzer.DetailedFailures(report.Models));

        // 4. Individuation analysis
        Sec(sb, "Individuation Analysis");
        sb.AppendLine(MeasurementAnalyzer.IndividuationReport(report.IndividuationTests));

        // 5. The derivation
        Sec(sb, "Derivation: Q-Individuation Collapse");
        sb.AppendLine(report.Derivation);

        // 6. Key mechanism
        Sec(sb, "Key Mechanism: Q Conservation → Single Outcome");
        sb.AppendLine("  BEFORE MEASUREMENT:");
        sb.AppendLine("    |ψ_system⟩ = a|↑⟩ + b|↓⟩     Q(system) = 1");
        sb.AppendLine("    |apparatus⟩ = |ready⟩         Q(apparatus) = 1");
        sb.AppendLine("    Q(total) = 2");
        sb.AppendLine();
        sb.AppendLine("  ENTANGLEMENT:");
        sb.AppendLine("    |Ψ⟩ = a|↑⟩|up⟩ + b|↓⟩|down⟩  Q(total) = 2 (as joint system)");
        sb.AppendLine();
        sb.AppendLine("  DECOHERENCE:");
        sb.AppendLine("    Branches become macroscopically distinct.");
        sb.AppendLine("    |↑⟩|up⟩ and |↓⟩|down⟩ are in disconnected regions");
        sb.AppendLine("    of configuration space.");
        sb.AppendLine();
        sb.AppendLine("  Q COUNTING:");
        sb.AppendLine("    If BOTH branches persist: Q = system + 2 apparatus domains.");
        sb.AppendLine("    Q(total) ≥ 3. But Q was 2. VIOLATION.");
        sb.AppendLine();
        sb.AppendLine("  RESOLUTION:");
        sb.AppendLine("    Q conservation FORCES single-outcome selection.");
        sb.AppendLine("    Only ONE branch can be realized.");
        sb.AppendLine("    |Ψ⟩ → |↑⟩|up⟩ OR |↓⟩|down⟩, not both.");
        sb.AppendLine("    COLLAPSE IS Q CONSERVATION IN ACTION.");
        sb.AppendLine();

        // 7. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(MeasurementDerivation.HostileReview());

        // 8. Final postulate count
        Sec(sb, "Final Postulate Count");
        sb.AppendLine(MeasurementAnalyzer.FinalPostulateCount());

        // 9. Verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X038 COMPLETE.");
        sb.AppendLine($"  Status: {report.Status}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
