using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 277 — Question Layer Audit. Do measurement classes emerge from more fundamental question
/// classes? D96 only, no observables.
/// </summary>
public class ATQG_Phase277_QuestionLayerAuditTests : ResearchTestBase
{
    public ATQG_Phase277_QuestionLayerAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2770_QuestionClasses()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2770: the five question classes");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - there are five distinct question classes;");
        sb.AppendLine("  - each question selects a DISTINCT measurement class (one-to-one).");
        sb.AppendLine();

        foreach (var q in QuestionLayerAudit.Questions())
            sb.AppendLine($"  {q.Question,-15} asks for {q.AsksFor,-32} → {q.SelectsClass,-12} → {q.Sector}");
        sb.AppendLine();
        sb.AppendLine($"each question selects a distinct class: {QuestionLayerAudit.EachQuestionSelectsDistinctClass()}");
        sb.AppendLine($"mapping bijective: {QuestionLayerAudit.QuestionClassMappingBijective()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, QuestionLayerAudit.Questions().Length);
        Assert.True(QuestionLayerAudit.EachQuestionSelectsDistinctClass(),
            "each question selects a distinct measurement class");
        Assert.True(QuestionLayerAudit.QuestionClassMappingBijective());
    }

    [Fact]
    public void ATQG2771_GenerativeLayer()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2771: the generative layer — question selects the class");

        sb.AppendLine("HYPOTHESIS: the question determines WHAT KIND of read is needed, which selects");
        sb.AppendLine("the measurement class, which determines the equation form.");
        sb.AppendLine();

        sb.AppendLine("  'how much?'     → asks for a magnitude → value read (dimensional)");
        sb.AppendLine("  'how strong?'   → asks for an interaction → ratio read (dimensionless)");
        sb.AppendLine("  'how oriented?' → asks for an alignment → angle read (unitary)");
        sb.AppendLine("  'how global?'   → asks for the whole → log read (scale-invariant)");
        sb.AppendLine("  'what shape?'   → asks for the geometry → power/deficit read");
        sb.AppendLine();
        sb.AppendLine($"question classes are the QG275 axis positions: {QuestionLayerAudit.QuestionsAreAxisPositions()}");
        sb.AppendLine($"generative layer: {QuestionLayerAudit.GenerativeLayer()}");
        sb.AppendLine($"question is the origin: {QuestionLayerAudit.QuestionIsOrigin()}");

        Output.WriteLine(sb.ToString());

        Assert.True(QuestionLayerAudit.QuestionsAreAxisPositions(),
            "the question classes are the LEVEL × NATURE axis positions");
        Assert.True(QuestionLayerAudit.QuestionIsOrigin());
    }

    [Fact]
    public void ATQG2772_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2772: the question-layer determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NO QUESTION LAYER (score ≤ 2), PARTIAL QUESTION LAYER (3-4),");
        sb.AppendLine("    QUESTION LAYER (5-6);");
        sb.AppendLine("  - the hypothesis: measurement classes emerge from question classes.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {QuestionLayerAudit.Summary()}");
        sb.AppendLine($"Question-layer score: {QuestionLayerAudit.QuestionLayerScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {QuestionLayerAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - The question determines WHAT KIND of read is needed (magnitude→value,");
        sb.AppendLine("    interaction→ratio, alignment→angle, whole→log, geometry→power) — the question");
        sb.AppendLine("    class SELECTS the measurement class.");
        sb.AppendLine("  - The question classes ARE the QG275 axis positions (LEVEL × NATURE) — the most");
        sb.AppendLine("    primitive classification of what is asked about the spectrum.");
        sb.AppendLine("  - The generative layer: QUESTION → MEASUREMENT CLASS → EQUATION FORM → OBSERVABLE.");
        sb.AppendLine("    The question is the ORIGIN of the measurement classes.");
        sb.AppendLine("  - Honest caveat (QG275): the question→sector completion retains the");
        sb.AppendLine("    relational-subclass context-dependence.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("QUESTION LAYER", QuestionLayerAudit.Classify());
        Assert.True(QuestionLayerAudit.QuestionLayerScore() >= 5);
        Assert.Contains("QUESTION LAYER", QuestionLayerAudit.Summary());
        Assert.Contains("how much", QuestionLayerAudit.Summary());
    }
}
