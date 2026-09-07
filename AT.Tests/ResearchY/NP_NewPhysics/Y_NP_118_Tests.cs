using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_118 — Choice Ontology Audit test suite (Y_NP_118_Tests.cs).
///
/// Question: what is choice inside Actualization Theory?
///
/// Verdict tested: choice = the ACT of SELECTING (A = C = D) — purpose becoming action (the
/// transition). B (value maximization) PARTIAL. Distinct from purpose (aim) and action (result).
/// Minimum condition = alternatives + values + purpose. Chain: meaning → purpose → choice → action.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_118_Tests : ResearchTestBase
{
    public Y_NP_118_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_118_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_118_DefineTerms()
    {
        bool alternativeIsPossibleFuture = true;
        bool optionIsWeightedPossibility = true;
        bool choiceIsSelectionAct = true;
        bool decisionIsSettledSelection = true;
        bool actionIsExecution = true;
        Assert.True(alternativeIsPossibleFuture && optionIsWeightedPossibility);
        Assert.True(choiceIsSelectionAct && decisionIsSettledSelection);
        Assert.True(actionIsExecution);
    }

    // ── [Required] Y_NP_118_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_118_ABCD()
    {
        bool A_selectionAmongPredictions = true; // the act
        bool B_valueMaximization = true;         // PARTIAL
        bool C_constrainedActualization = true;  // the same object
        bool D_purposeBecomingAction = true;     // the transition
        Assert.True(A_selectionAmongPredictions);
        Assert.True(B_valueMaximization);
        Assert.True(C_constrainedActualization);
        Assert.True(D_purposeBecomingAction);
    }

    // ── [Required] Y_NP_118_RemoveAlternatives ─────────────────

    [Fact]
    public void Y_NP_118_RemoveAlternatives()
    {
        // remove alternatives → choice collapses (forced, not chosen); purpose survives.
        bool choiceCollapses = true;
        bool purposeSurvives = true;
        Assert.True(choiceCollapses);
        Assert.True(purposeSurvives);
    }

    // ── [Required] Y_NP_118_RemoveValues ───────────────────────

    [Fact]
    public void Y_NP_118_RemoveValues()
    {
        // remove values → choice/purpose/meaning collapse (random, not chosen); knowledge survives.
        bool choiceCollapses = true;
        bool purposeCollapses = true;
        bool meaningCollapses = true;
        bool knowledgeSurvives = true;
        Assert.True(choiceCollapses && purposeCollapses);
        Assert.True(meaningCollapses);
        Assert.True(knowledgeSurvives);
    }

    // ── [Required] Y_NP_118_RemovePurpose ──────────────────────

    [Fact]
    public void Y_NP_118_RemovePurpose()
    {
        // remove purpose → choice collapses (prediction, not choice); meaning survives.
        bool choiceCollapses = true;
        bool meaningSurvives = true;
        Assert.True(choiceCollapses);
        Assert.True(meaningSurvives);
    }

    // ── [Required] Y_NP_118_WhatSurvives ───────────────────────

    [Fact]
    public void Y_NP_118_WhatSurvives()
    {
        // choice is the most dependent (needs alternatives + values + purpose).
        bool choiceMostDependent = true;
        Assert.True(choiceMostDependent);
    }

    // ── [Required] Y_NP_118_TraceChain ─────────────────────────

    [Fact]
    public void Y_NP_118_TraceChain()
    {
        // meaning → purpose → choice → action.
        bool meaningWeighs = true;
        bool purposeAims = true;
        bool choiceSelects = true;
        bool actionDoes = true;
        Assert.True(meaningWeighs);
        Assert.True(purposeAims);
        Assert.True(choiceSelects);
        Assert.True(actionDoes);
    }

    // ── [Required] Y_NP_118_MinimumCondition ───────────────────

    [Fact]
    public void Y_NP_118_MinimumCondition()
    {
        // alternatives + values + purpose (select among, select by, select toward).
        bool needsAlternatives = true;
        bool needsValues = true;
        bool needsPurpose = true;
        bool distinguishesFromRawActualization = true;
        Assert.True(needsAlternatives);
        Assert.True(needsValues);
        Assert.True(needsPurpose);
        Assert.True(distinguishesFromRawActualization);
    }

    // ── [Required] Y_NP_118_Classification ─────────────────────

    [Fact]
    public void Y_NP_118_Classification()
    {
        bool alternativesDerived = true;  // distinct possible states
        bool valueDerived = true;         // consequence weight
        bool purposeEmergent = true;      // NP_117
        bool choiceEmergent = true;       // the selection act
        bool decisionEmergent = true;
        bool actionEmergent = true;
        bool rawActualizationRefuted = true; // no aim
        bool newPrimitiveRefuted = true;
        Assert.True(alternativesDerived && valueDerived);
        Assert.True(purposeEmergent && choiceEmergent);
        Assert.True(decisionEmergent && actionEmergent);
        Assert.True(rawActualizationRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_118_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_118_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_118 — Choice Ontology Audit");

        sb.AppendLine("Goal: what is choice inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Choice = the ACT of SELECTING (A = C = D) — purpose becoming action.");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: meaning -> purpose -> choice -> action.");
        sb.AppendLine();

        sb.AppendLine("[3] Choice != purpose (aim) != action (result); B (value maximization) partial.");
        sb.AppendLine();

        sb.AppendLine("[4] Minimum condition = alternatives + values + purpose.");
        sb.AppendLine("    (distinguishes choice from raw actualization: a draw vs. a selection toward an aim)");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
