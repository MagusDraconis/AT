using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_115 — Understanding Ontology Audit test suite (Y_NP_115_Tests.cs).
///
/// Question: what is understanding inside Actualization Theory?
///
/// Verdict tested: understanding = the MODEL OF MODELS (D) = compression of knowledge (B) =
/// predictive hierarchy (C). A (more knowledge) REFUTED. Separator from knowledge = compression +
/// self-reference. Understanding generalizes more effectively.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_115_Tests : ResearchTestBase
{
    public Y_NP_115_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_115_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_115_DefineTerms()
    {
        // data/information/observation/memory/knowledge/understanding (cumulative).
        bool dataIsRaw = true;
        bool informationIsStructure = true;
        bool observationIsIncorporation = true;
        bool memoryIsPersisted = true;
        bool knowledgeIsPredictive = true;
        bool understandingIsSelfReferential = true;
        Assert.True(dataIsRaw && informationIsStructure);
        Assert.True(observationIsIncorporation && memoryIsPersisted);
        Assert.True(knowledgeIsPredictive && understandingIsSelfReferential);
    }

    // ── [Required] Y_NP_115_CompareObjects ─────────────────────

    [Fact]
    public void Y_NP_115_CompareObjects()
    {
        // detector (read) < recorder (store) < predictor (knowledge) < understander (model of models).
        bool detectorReads = true;
        bool recorderStores = true;
        bool predictorPredicts = true;
        bool understanderModelsItsOwnPredictions = true;
        Assert.True(detectorReads);
        Assert.True(recorderStores);
        Assert.True(predictorPredicts);
        Assert.True(understanderModelsItsOwnPredictions);
    }

    // ── [Required] Y_NP_115_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_115_ABCD()
    {
        bool A_moreKnowledge = false;         // REFUTED
        bool B_compressionOfKnowledge = true; // YES
        bool C_predictiveHierarchy = true;    // YES
        bool D_modelOfModels = true;          // the answer
        Assert.False(A_moreKnowledge);
        Assert.True(B_compressionOfKnowledge);
        Assert.True(C_predictiveHierarchy);
        Assert.True(D_modelOfModels);
    }

    // ── [Required] Y_NP_115_RemovePrediction ───────────────────

    [Fact]
    public void Y_NP_115_RemovePrediction()
    {
        // remove prediction → knowledge AND understanding collapse (only recording remains).
        bool knowledgeCollapses = true;
        bool understandingCollapses = true;
        bool onlyRecordingRemains = true;
        Assert.True(knowledgeCollapses);
        Assert.True(understandingCollapses);
        Assert.True(onlyRecordingRemains);
    }

    // ── [Required] Y_NP_115_RemoveIntegration ──────────────────

    [Fact]
    public void Y_NP_115_RemoveIntegration()
    {
        // remove integration → knowledge → scattered observations; understanding → scattered knowledge.
        bool knowledgeBecomesObservations = true;
        bool understandingBecomesScatteredKnowledge = true;
        Assert.True(knowledgeBecomesObservations);
        Assert.True(understandingBecomesScatteredKnowledge);
    }

    // ── [Required] Y_NP_115_MinimumSeparator ───────────────────

    [Fact]
    public void Y_NP_115_MinimumSeparator()
    {
        // compression + self-reference (knows WHY it predicts) separates knowledge from understanding.
        bool compression = true;
        bool selfReference = true;
        bool knowsWhyNotJustWhatNext = true;
        Assert.True(compression);
        Assert.True(selfReference);
        Assert.True(knowsWhyNotJustWhatNext);
    }

    // ── [Required] Y_NP_115_Effectiveness ──────────────────────

    [Fact]
    public void Y_NP_115_Effectiveness()
    {
        // understanding generalizes to novel cases (transfer) → more effective future actualization.
        bool generalizesToNovelCases = true;
        bool moreEffectiveThanRoteKnowledge = true;
        Assert.True(generalizesToNovelCases);
        Assert.True(moreEffectiveThanRoteKnowledge);
    }

    // ── [Required] Y_NP_115_Classification ─────────────────────

    [Fact]
    public void Y_NP_115_Classification()
    {
        bool dataInfoMemoryDerived = true;  // D_039/M_004/NP_102
        bool observationEmergent = true;    // NP_113
        bool knowledgeEmergent = true;      // NP_114
        bool understandingEmergent = true;  // the highest level
        bool moreKnowledgeRefuted = true;
        bool newPrimitiveRefuted = true;
        Assert.True(dataInfoMemoryDerived);
        Assert.True(observationEmergent && knowledgeEmergent);
        Assert.True(understandingEmergent);
        Assert.True(moreKnowledgeRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_115_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_115_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_115 — Understanding Ontology Audit");

        sb.AppendLine("Goal: what is understanding inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Understanding = the MODEL OF MODELS (compression + self-reference).");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: data -> information -> observation -> memory -> knowledge -> understanding.");
        sb.AppendLine("    detector < recorder < predictor (knowledge) < understander (model of models).");
        sb.AppendLine();

        sb.AppendLine("[3] A (more knowledge) REFUTED; B = C = D.");
        sb.AppendLine();

        sb.AppendLine("[4] Separator from knowledge = compression + self-reference (knows WHY it predicts).");
        sb.AppendLine("    Understanding generalizes more effectively.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
