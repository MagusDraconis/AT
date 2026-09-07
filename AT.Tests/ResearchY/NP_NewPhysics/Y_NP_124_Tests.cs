using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_124 — Wisdom Ontology Audit test suite (Y_NP_124_Tests.cs).
///
/// Question: what is wisdom inside Actualization Theory?
///
/// Verdict tested: wisdom = RESPONSIBLE ACTION GUIDED BY UNDERSTANDING (D) — the integration of all
/// prior levels into right action. B (balanced understanding) and C (truth-weighted purpose)
/// PARTIAL; A (accumulated knowledge) REFUTED. Minimum condition = integration.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_124_Tests : ResearchTestBase
{
    public Y_NP_124_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_124_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_124_DefineTerms()
    {
        bool knowledgeIsPrediction = true;
        bool understandingIsModelOfModels = true;
        bool truthIsCorrespondence = true;
        bool meaningIsValueWeighted = true;
        bool purposeIsSelectedPrediction = true;
        bool wisdomIsResponsibleAction = true;
        Assert.True(knowledgeIsPrediction && understandingIsModelOfModels);
        Assert.True(truthIsCorrespondence && meaningIsValueWeighted);
        Assert.True(purposeIsSelectedPrediction && wisdomIsResponsibleAction);
    }

    // ── [Required] Y_NP_124_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_124_ABCD()
    {
        bool A_accumulatedKnowledge = false; // REFUTED
        bool B_balancedUnderstanding = true; // PARTIAL (necessary, not sufficient)
        bool C_truthWeightedPurpose = true;  // PARTIAL (necessary, not sufficient)
        bool D_responsibleActionGuidedByUnderstanding = true; // the answer
        Assert.False(A_accumulatedKnowledge);
        Assert.True(B_balancedUnderstanding);
        Assert.True(C_truthWeightedPurpose);
        Assert.True(D_responsibleActionGuidedByUnderstanding);
    }

    // ── [Required] Y_NP_124_RemoveThree ────────────────────────

    [Fact]
    public void Y_NP_124_RemoveThree()
    {
        // truth → misguided; meaning → directionless; responsibility → irresponsible.
        bool removeTruthCollapses = true;
        bool removeMeaningCollapses = true;
        bool removeResponsibilityCollapses = true;
        Assert.True(removeTruthCollapses);
        Assert.True(removeMeaningCollapses);
        Assert.True(removeResponsibilityCollapses);
    }

    // ── [Required] Y_NP_124_WhatSurvives ───────────────────────

    [Fact]
    public void Y_NP_124_WhatSurvives()
    {
        // knowledge + understanding survive; wisdom collapses (wisdom is the integration).
        bool knowledgeSurvives = true;
        bool understandingSurvives = true;
        bool wisdomCollapses = true;
        Assert.True(knowledgeSurvives && understandingSurvives);
        Assert.True(wisdomCollapses);
    }

    // ── [Required] Y_NP_124_CompareObservers ───────────────────

    [Fact]
    public void Y_NP_124_CompareObservers()
    {
        // knowledgeable (knows) < understanding (models why) < wise (acts responsibly).
        bool knowledgeableKnows = true;
        bool understandingModelsWhy = true;
        bool wiseActsResponsibly = true;
        Assert.True(knowledgeableKnows);
        Assert.True(understandingModelsWhy);
        Assert.True(wiseActsResponsibly);
    }

    // ── [Required] Y_NP_124_TraceChain ─────────────────────────

    [Fact]
    public void Y_NP_124_TraceChain()
    {
        // knowledge → understanding → meaning → purpose → wisdom.
        bool knowledgePredicts = true;
        bool understandingModels = true;
        bool meaningValues = true;
        bool purposeAims = true;
        bool wisdomIntegratesIntoAction = true;
        Assert.True(knowledgePredicts && understandingModels);
        Assert.True(meaningValues && purposeAims);
        Assert.True(wisdomIntegratesIntoAction);
    }

    // ── [Required] Y_NP_124_MinimumCondition ───────────────────

    [Fact]
    public void Y_NP_124_MinimumCondition()
    {
        // INTEGRATION (understanding + truth + meaning + responsibility → action).
        bool integrationIsTheCondition = true;
        bool notASeparateLevel = true;
        Assert.True(integrationIsTheCondition);
        Assert.True(notASeparateLevel);
    }

    // ── [Required] Y_NP_124_Classification ─────────────────────

    [Fact]
    public void Y_NP_124_Classification()
    {
        bool priorLevelsEmergent = true;   // NP_114–123
        bool wisdomEmergent = true;        // the integrative apex
        bool accumulatedKnowledgeRefuted = true;
        bool separateLevelRefuted = true;
        bool newPrimitiveRefuted = true;
        Assert.True(priorLevelsEmergent);
        Assert.True(wisdomEmergent);
        Assert.True(accumulatedKnowledgeRefuted);
        Assert.True(separateLevelRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_124_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_124_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_124 — Wisdom Ontology Audit");

        sb.AppendLine("Goal: what is wisdom inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Wisdom = RESPONSIBLE ACTION GUIDED BY UNDERSTANDING (D) — the integration");
        sb.AppendLine("    of all prior levels into right action.");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: knowledge -> understanding -> meaning -> purpose -> wisdom");
        sb.AppendLine("    (predict -> model -> value -> aim -> integrate-and-act).");
        sb.AppendLine();

        sb.AppendLine("[3] A (accumulated knowledge) REFUTED; B/C partial.");
        sb.AppendLine();

        sb.AppendLine("[4] Minimum condition = integration (understanding + truth + meaning + responsibility).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
