using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using static AT.Core.Research.ConceptMappingMatrix;

namespace AT.Tests.ResearchX;

public class AT_X033_EmergenceGapAudit : ResearchTestBase
{
    public AT_X033_EmergenceGapAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X033_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X033 Emergence Gap Audit");

        // 1. Framework projections
        var (main, rx) = FrameworkProjectionModel.Both();
        Sec(sb, "Framework Projections");
        sb.AppendLine($"  MAIN AT asks:    {main.StartingQuestion}");
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
        sb.AppendLine("  Concept               │ Main AT            │ ResearchX           │ Category");
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
            sb.AppendLine($"  {m.Concept,-22} │ {m.MainATView,-20} │ {m.ResearchXView,-20} │ {cat}");
        }
        sb.AppendLine();
        sb.AppendLine($"  Equivalent: {report.Equivalent}  Implicit: {report.Implicit}  Emergent: {report.Emergent}  Genuine: {report.GenuineGaps}");
        sb.AppendLine();

        // 3. The 4 asymmetries resolved
        Sec(sb, "Resolution of X032's 4 Asymmetries");
        sb.AppendLine("  1. Complexity Staircase  → EMERGENT (spectrum encodes it; Main AT never asked)");
        sb.AppendLine("  2. Finite/Infinite        → EMERGENT (Main AT never explored limit)");
        sb.AppendLine("  3. Quantum Necessity      → EMERGENT (Main AT showed possibility, not necessity)");
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
        sb.AppendLine("  Main AT   = Schrödinger picture: L_Q → Hilbert → Schrödinger → QM.");
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
        sb.AppendLine($"  AT-X033 COMPLETE. {report.Equivalent + report.Implicit + report.Emergent}/{report.TotalConcepts} concepts resolved.");
        sb.AppendLine($"  {report.GenuineGaps} structural feature (operator-independence of ResearchX).");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine($"  Verdict: {report.Verdict}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
