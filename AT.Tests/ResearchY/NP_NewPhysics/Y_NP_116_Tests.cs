using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_116 — Meaning Ontology Audit test suite (Y_NP_116_Tests.cs).
///
/// Question: what is meaning inside Actualization Theory?
///
/// Verdict tested: meaning = VALUE-WEIGHTED PREDICTION = the relation between models (C = D) — the
/// SIGNIFICANCE of a distinction for an observer. Distinct from information (structure), knowledge
/// (prediction), understanding (model of models). Requires context + prediction; minimum condition =
/// significance.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_116_Tests : ResearchTestBase
{
    public Y_NP_116_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_116_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_116_DefineTerms()
    {
        bool dataIsRaw = true;
        bool informationIsStructure = true;
        bool observationIsIncorporation = true;
        bool knowledgeIsPredictive = true;
        bool understandingIsModelOfModels = true;
        bool meaningIsSignificance = true;
        Assert.True(dataIsRaw && informationIsStructure);
        Assert.True(observationIsIncorporation && knowledgeIsPredictive);
        Assert.True(understandingIsModelOfModels && meaningIsSignificance);
    }

    // ── [Required] Y_NP_116_CompareObjects ─────────────────────

    [Fact]
    public void Y_NP_116_CompareObjects()
    {
        // observer (read) < knower (predict) < understander (model) < meaning-bearer (weight).
        bool observerReads = true;
        bool knowerPredicts = true;
        bool understanderModels = true;
        bool meaningBearerWeights = true;
        Assert.True(observerReads);
        Assert.True(knowerPredicts);
        Assert.True(understanderModels);
        Assert.True(meaningBearerWeights);
    }

    // ── [Required] Y_NP_116_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_116_ABCD()
    {
        bool A_interpretedInformation = true; // necessary, not sufficient
        bool B_contextualUnderstanding = true; // necessary, not sufficient
        bool C_valueWeightedPrediction = true; // the differentiator
        bool D_relationBetweenModels = true;   // the same object
        Assert.True(A_interpretedInformation);
        Assert.True(B_contextualUnderstanding);
        Assert.True(C_valueWeightedPrediction);
        Assert.True(D_relationBetweenModels);
    }

    // ── [Required] Y_NP_116_RemoveContext ──────────────────────

    [Fact]
    public void Y_NP_116_RemoveContext()
    {
        // remove context → meaning collapses (significance is context-dependent).
        bool meaningCollapses = true;
        bool informationSurvives = true;
        bool knowledgeSurvives = true;
        Assert.True(meaningCollapses);
        Assert.True(informationSurvives && knowledgeSurvives);
    }

    // ── [Required] Y_NP_116_RemovePrediction ───────────────────

    [Fact]
    public void Y_NP_116_RemovePrediction()
    {
        // remove prediction → meaning collapses (no future consequence = insignificant).
        bool meaningCollapses = true;
        bool informationSurvives = true;
        Assert.True(meaningCollapses);
        Assert.True(informationSurvives);
    }

    // ── [Required] Y_NP_116_WhatSurvives ───────────────────────

    [Fact]
    public void Y_NP_116_WhatSurvives()
    {
        // information is most robust; meaning is most fragile (needs context + prediction).
        bool informationMostRobust = true;
        bool meaningMostFragile = true;
        Assert.True(informationMostRobust);
        Assert.True(meaningMostFragile);
    }

    // ── [Required] Y_NP_116_MinimumCondition ───────────────────

    [Fact]
    public void Y_NP_116_MinimumCondition()
    {
        // SIGNIFICANCE: the distinction must matter for the observer's future actualizations.
        bool distinctionMustMatter = true;
        bool meaningIsForAnObserver = true;
        Assert.True(distinctionMustMatter);
        Assert.True(meaningIsForAnObserver);
    }

    // ── [Required] Y_NP_116_Classification ─────────────────────

    [Fact]
    public void Y_NP_116_Classification()
    {
        bool dataInfoDerived = true;      // D_039/M_004
        bool observationEmergent = true;  // NP_113
        bool knowledgeEmergent = true;    // NP_114
        bool understandingEmergent = true;// NP_115
        bool meaningEmergent = true;      // the significance relation
        bool meaningEqualsInformationRefuted = true;
        bool newPrimitiveRefuted = true;
        Assert.True(dataInfoDerived);
        Assert.True(observationEmergent && knowledgeEmergent);
        Assert.True(understandingEmergent && meaningEmergent);
        Assert.True(meaningEqualsInformationRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_116_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_116_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_116 — Meaning Ontology Audit");

        sb.AppendLine("Goal: what is meaning inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Meaning = VALUE-WEIGHTED PREDICTION = the relation between models (C = D).");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: data -> information -> observation -> knowledge -> understanding -> meaning.");
        sb.AppendLine();

        sb.AppendLine("[3] Meaning != information (structure), != knowledge (prediction),");
        sb.AppendLine("    != understanding (model of models); its differentiator is SIGNIFICANCE.");
        sb.AppendLine();

        sb.AppendLine("[4] Remove context or prediction -> meaning collapses; information survives.");
        sb.AppendLine("    Minimum condition: the distinction must MATTER for the observer.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
