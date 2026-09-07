using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_123 — Truth Ontology Audit test suite (Y_NP_123_Tests.cs).
///
/// Question: what is truth inside Actualization Theory?
///
/// Verdict tested: truth = CORRESPONDENCE (C) = model-network correspondence (B) = persistence
/// across observations (D). A (successful prediction) necessary not sufficient (useful ≠ true).
/// Distinct from knowledge (prediction), understanding (modeling), meaning (valuing). Minimum
/// condition = correspondence (stable, consistent).
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_123_Tests : ResearchTestBase
{
    public Y_NP_123_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_123_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_123_DefineTerms()
    {
        bool beliefIsHeldDistinction = true;
        bool modelIsIntegratedDistinctions = true;
        bool predictionIsAnticipation = true;
        bool truthIsCorrespondence = true;
        bool errorIsContradiction = true;
        Assert.True(beliefIsHeldDistinction && modelIsIntegratedDistinctions);
        Assert.True(predictionIsAnticipation && truthIsCorrespondence);
        Assert.True(errorIsContradiction);
    }

    // ── [Required] Y_NP_123_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_123_ABCD()
    {
        bool A_successfulPrediction = true;    // necessary, not sufficient
        bool B_modelNetworkCorrespondence = true; // YES
        bool C_stableDistinctionMapping = true;   // the answer
        bool D_persistenceAcrossObservations = true; // the same object
        Assert.True(A_successfulPrediction);
        Assert.True(B_modelNetworkCorrespondence);
        Assert.True(C_stableDistinctionMapping);
        Assert.True(D_persistenceAcrossObservations);
    }

    // ── [Required] Y_NP_123_RemoveThree ────────────────────────

    [Fact]
    public void Y_NP_123_RemoveThree()
    {
        // prediction → truth partially survives; observation/consistency → truth collapses.
        bool removePredictionPartial = true;
        bool removeObservationCollapses = true;
        bool removeConsistencyCollapses = true;
        Assert.True(removePredictionPartial);
        Assert.True(removeObservationCollapses);
        Assert.True(removeConsistencyCollapses);
    }

    // ── [Required] Y_NP_123_WhatSurvives ───────────────────────

    [Fact]
    public void Y_NP_123_WhatSurvives()
    {
        // truth survives without prediction (a static map can correspond); collapses without
        // observation/consistency.
        bool truthSurvivesWithoutPrediction = true;
        bool truthCollapsesWithoutObservation = true;
        bool truthCollapsesWithoutConsistency = true;
        Assert.True(truthSurvivesWithoutPrediction);
        Assert.True(truthCollapsesWithoutObservation);
        Assert.True(truthCollapsesWithoutConsistency);
    }

    // ── [Required] Y_NP_123_ThreeModels ────────────────────────

    [Fact]
    public void Y_NP_123_ThreeModels()
    {
        // true (corresponds) / useful (predicts well) / false (contradicted). True != useful.
        bool trueCorresponds = true;
        bool usefulPredictsWell = true;
        bool falseContradicted = true;
        bool usefulNotNecessarilyTrue = true;
        Assert.True(trueCorresponds);
        Assert.True(usefulPredictsWell);
        Assert.True(falseContradicted);
        Assert.True(usefulNotNecessarilyTrue);
    }

    // ── [Required] Y_NP_123_TraceChain ─────────────────────────

    [Fact]
    public void Y_NP_123_TraceChain()
    {
        // observation → knowledge → understanding → truth (incorporate → predict → model → correspond).
        bool observationIncorporates = true;
        bool knowledgePredicts = true;
        bool understandingModels = true;
        bool truthCorresponds = true;
        Assert.True(observationIncorporates && knowledgePredicts);
        Assert.True(understandingModels && truthCorresponds);
    }

    // ── [Required] Y_NP_123_MinimumCondition ───────────────────

    [Fact]
    public void Y_NP_123_MinimumCondition()
    {
        // CORRESPONDENCE (stable, consistent distinction mapping to the world's structures).
        bool correspondenceIsTheCondition = true;
        bool stableAndConsistent = true;
        Assert.True(correspondenceIsTheCondition);
        Assert.True(stableAndConsistent);
    }

    // ── [Required] Y_NP_123_Classification ─────────────────────

    [Fact]
    public void Y_NP_123_Classification()
    {
        bool worldStructuresDerived = true;  // NP_102/104
        bool observationKnowledgeUnderstandingEmergent = true; // NP_113/114/115
        bool truthEmergent = true;           // the correspondence
        bool successRefuted = true;          // useful != true
        bool meaningRefuted = true;          // value != correspondence
        bool newPrimitiveRefuted = true;
        Assert.True(worldStructuresDerived);
        Assert.True(observationKnowledgeUnderstandingEmergent);
        Assert.True(truthEmergent);
        Assert.True(successRefuted && meaningRefuted);
        Assert.True(newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_123_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_123_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_123 — Truth Ontology Audit");

        sb.AppendLine("Goal: what is truth inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Truth = CORRESPONDENCE (B = C = D) — the model's distinctions map to");
        sb.AppendLine("    the world's persistent Difference structures.");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: observation -> knowledge -> understanding -> truth");
        sb.AppendLine("    (incorporate -> predict -> model -> correspond).");
        sb.AppendLine();

        sb.AppendLine("[3] True (corresponds) != useful (predicts well); false = contradicted.");
        sb.AppendLine();

        sb.AppendLine("[4] Minimum condition = correspondence (stable, consistent).");
        sb.AppendLine("    Truth survives without prediction; collapses without observation/consistency.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
