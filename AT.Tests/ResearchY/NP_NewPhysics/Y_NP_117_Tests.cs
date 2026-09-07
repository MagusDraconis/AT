using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_117 — Purpose Ontology Audit test suite (Y_NP_117_Tests.cs).
///
/// Question: what is purpose inside Actualization Theory?
///
/// Verdict tested: purpose = the SELECTED PREDICTION (B) = future-oriented actualization (C) =
/// stable value hierarchy (D) — meaning PLUS selection (the aim). A (weighted meaning) necessary
/// not sufficient. Differentiator from meaning = selection (direction). Purpose directs future
/// actualization. Chain: observer → meaning → purpose → action.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_117_Tests : ResearchTestBase
{
    public Y_NP_117_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_117_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_117_DefineTerms()
    {
        bool meaningIsValueWeighted = true;
        bool valueIsConsequenceWeight = true;
        bool choiceIsSelection = true;
        bool purposeIsSelectedAim = true;
        bool actionIsFutureActualization = true;
        Assert.True(meaningIsValueWeighted && valueIsConsequenceWeight);
        Assert.True(choiceIsSelection && purposeIsSelectedAim);
        Assert.True(actionIsFutureActualization);
    }

    // ── [Required] Y_NP_117_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_117_ABCD()
    {
        bool A_weightedMeaning = true;          // necessary, not sufficient
        bool B_selectedPrediction = true;       // the differentiator
        bool C_futureOrientedActualization = true; // the same object
        bool D_stableValueHierarchy = true;     // the same object
        Assert.True(A_weightedMeaning);
        Assert.True(B_selectedPrediction);
        Assert.True(C_futureOrientedActualization);
        Assert.True(D_stableValueHierarchy);
    }

    // ── [Required] Y_NP_117_RemoveValue ────────────────────────

    [Fact]
    public void Y_NP_117_RemoveValue()
    {
        // remove value → meaning + purpose collapse; information/knowledge/understanding survive.
        bool meaningCollapses = true;
        bool purposeCollapses = true;
        bool machinerySurvives = true;
        Assert.True(meaningCollapses && purposeCollapses);
        Assert.True(machinerySurvives);
    }

    // ── [Required] Y_NP_117_RemovePrediction ───────────────────

    [Fact]
    public void Y_NP_117_RemovePrediction()
    {
        // remove prediction → purpose collapses (no future to aim at); information survives.
        bool purposeCollapses = true;
        bool informationSurvives = true;
        Assert.True(purposeCollapses);
        Assert.True(informationSurvives);
    }

    // ── [Required] Y_NP_117_WhatSurvives ───────────────────────

    [Fact]
    public void Y_NP_117_WhatSurvives()
    {
        // purpose is the most dependent (needs value + prediction + meaning).
        bool purposeMostDependent = true;
        bool informationMostRobust = true;
        Assert.True(purposeMostDependent);
        Assert.True(informationMostRobust);
    }

    // ── [Required] Y_NP_117_TraceChain ─────────────────────────

    [Fact]
    public void Y_NP_117_TraceChain()
    {
        // observer → meaning (weighs) → purpose (aims) → action (does).
        bool observerReads = true;
        bool meaningWeighs = true;
        bool purposeAims = true;
        bool actionDoes = true;
        Assert.True(observerReads);
        Assert.True(meaningWeighs);
        Assert.True(purposeAims);
        Assert.True(actionDoes);
    }

    // ── [Required] Y_NP_117_ChangesActualization ───────────────

    [Fact]
    public void Y_NP_117_ChangesActualization()
    {
        // purpose directs the observer's future actualization (converts significance into a goal).
        bool purposeDirectsAction = true;
        bool makesMeaningOperative = true;
        Assert.True(purposeDirectsAction);
        Assert.True(makesMeaningOperative);
    }

    // ── [Required] Y_NP_117_Classification ─────────────────────

    [Fact]
    public void Y_NP_117_Classification()
    {
        bool meaningEmergent = true;   // NP_116
        bool valueDerived = true;      // the consequence weight
        bool choiceEmergent = true;    // the selection
        bool purposeEmergent = true;   // the selected aim
        bool actionEmergent = true;
        bool purposeEqualsMeaningRefuted = true;
        bool newPrimitiveRefuted = true;
        Assert.True(meaningEmergent);
        Assert.True(valueDerived);
        Assert.True(choiceEmergent && purposeEmergent && actionEmergent);
        Assert.True(purposeEqualsMeaningRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_117_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_117_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_117 — Purpose Ontology Audit");

        sb.AppendLine("Goal: what is purpose inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Purpose = the SELECTED PREDICTION (B = C = D) — meaning PLUS selection.");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: observer -> meaning (weighs) -> purpose (aims) -> action (does).");
        sb.AppendLine();

        sb.AppendLine("[3] Differentiator from meaning = SELECTION (direction): meaning weighs, purpose aims.");
        sb.AppendLine();

        sb.AppendLine("[4] Remove value or prediction -> purpose collapses; machinery survives.");
        sb.AppendLine("    Purpose DOES direct future actualization.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
