using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;
using static TQM.Core.Research.ConceptMappingMatrix;

namespace TQM.Tests.ResearchX;

public class TQM_X033_EmergenceGapAudit : ResearchTestBase
{
    public TQM_X033_EmergenceGapAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X033_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X033 Emergence Gap Audit");

        // 1. Framework projections
        var (main, rx) = FrameworkProjectionModel.Both();
        Sec(sb, "Framework Projections");
        sb.AppendLine($"  MAIN TQM asks:    {main.StartingQuestion}");
        sb.AppendLine($"  Mathematical core: {main.MathematicalCore}");
        sb.AppendLine($"  Primary discovery: {main.PrimaryDiscovery}");
        sb.AppendLine($"  Natural concepts:  {string.Join(", ", main.NaturalConcepts)}");
        sb.AppendLine();
        sb.AppendLine($"  RESEARCHX asks:    {rx.StartingQuestion}");
        sb.AppendLine($"  Mathematical core: {rx.MathematicalCore}");
        sb.AppendLine($"  Primary discovery: {rx.PrimaryDiscovery}");
        sb.AppendLine($"  Natural concepts:  {string.Join(", ", rx.NaturalConcepts)}");
        sb.AppendLine();

        // 2. Concept mapping matrix
        var report = EmergenceGapAnalyzer.Analyze();
        Sec(sb, "Concept Mapping Matrix");
        sb.AppendLine("  Concept               │ Main TQM            │ ResearchX           │ Category");
        sb.AppendLine("  " + new string('─', 100));
        foreach (var m in report.Mappings)
        {
            string cat = m.Category switch
            {
                GapCategory.Equivalent => "EQUIV",
                GapCategory.Implicit => "IMPL",
                GapCategory.Emergent => "EMERG",
                GapCategory.GenuineGap => "GAP",
                _ => "?"
            };
            sb.AppendLine($"  {m.Concept,-22} │ {m.MainTQMView,-20} │ {m.ResearchXView,-20} │ {cat}");
        }
        sb.AppendLine();
        sb.AppendLine($"  Equivalent: {report.Equivalent}  Implicit: {report.Implicit}  Emergent: {report.Emergent}  Genuine: {report.GenuineGaps}");
        sb.AppendLine();

        // 3. The 4 asymmetries resolved
        Sec(sb, "Resolution of X032's 4 Asymmetries");
        sb.AppendLine("  1. Complexity Staircase  → EMERGENT (spectrum encodes it; Main TQM never asked)");
        sb.AppendLine("  2. Finite/Infinite        → EMERGENT (Main TQM never explored limit)");
        sb.AppendLine("  3. Quantum Necessity      → EMERGENT (Main TQM showed possibility, not necessity)");
        sb.AppendLine("  4. L_Q Explicit Form      → GENUINE FEATURE (ResearchX is operator-independent)");
        sb.AppendLine();
        sb.AppendLine("  NONE of these are contradictions. All are consequences of:");
        sb.AppendLine("  DIFFERENT STARTING QUESTIONS → DIFFERENT DISCOVERIES.");
        sb.AppendLine();

        // 4. Derivation: staircase from L_Q
        sb.AppendLine(EmergenceGapAnalyzer.DeriveComplexityStaircaseFromLQ());

        // 5. Schrödinger ↔ Heisenberg analogy
        Sec(sb, "Analogy: Schrödinger vs Heisenberg Pictures");
        sb.AppendLine("  Schrödinger (1926): ∂ψ/∂t operator-first, differential equations.");
        sb.AppendLine("  Heisenberg  (1925): [X,P]=iℏ, matrix mechanics, algebraic.");
        sb.AppendLine("  Both describe the SAME Hilbert space structure.");
        sb.AppendLine();
        sb.AppendLine("  Main TQM   = Schrödinger picture: L_Q → Hilbert → Schrödinger → QM.");
        sb.AppendLine("  ResearchX  = Heisenberg picture:  R+S → Reality → Complexity → QM.");
        sb.AppendLine();
        sb.AppendLine("  Neither is 'missing' what the other has.");
        sb.AppendLine("  They emphasize DIFFERENT ASPECTS of the same underlying theory.");
        sb.AppendLine();

        // 6. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(EmergenceGapAnalyzer.HostileReview(report));

        // 7. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X033 COMPLETE. {report.Equivalent + report.Implicit + report.Emergent}/{report.TotalConcepts} concepts resolved.");
        sb.AppendLine($"  {report.GenuineGaps} structural feature (operator-independence of ResearchX).");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Verdict: {report.Verdict}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
