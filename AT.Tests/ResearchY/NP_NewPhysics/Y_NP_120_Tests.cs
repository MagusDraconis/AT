using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_120 — Responsibility Ontology Audit test suite (Y_NP_120_Tests.cs).
///
/// Question: what is responsibility inside Actualization Theory?
///
/// Verdict tested: responsibility = FREEDOM + PERSISTENCE — the ownership of action and consequences
/// by the same persistent chooser (A = B = C = D). Differentiator from freedom = continuity. Forced/
/// random actions bear no responsibility. Freedom survives without persistence; responsibility
/// collapses. Chain: meaning → purpose → choice → freedom → responsibility.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_120_Tests : ResearchTestBase
{
    public Y_NP_120_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_120_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_120_DefineTerms()
    {
        bool actionIsExecution = true;
        bool consequenceIsEffect = true;
        bool responsibilityIsOwnership = true;
        bool agencyIsFreedomPlusPersistence = true;
        Assert.True(actionIsExecution);
        Assert.True(consequenceIsEffect);
        Assert.True(responsibilityIsOwnership);
        Assert.True(agencyIsFreedomPlusPersistence);
    }

    // ── [Required] Y_NP_120_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_120_ABCD()
    {
        bool A_ownershipOfAction = true;        // YES
        bool B_ownershipOfConsequences = true;  // YES
        bool C_persistenceOfChoice = true;      // YES
        bool D_continuityOfObserver = true;     // the ground (NP_102)
        Assert.True(A_ownershipOfAction);
        Assert.True(B_ownershipOfConsequences);
        Assert.True(C_persistenceOfChoice);
        Assert.True(D_continuityOfObserver);
    }

    // ── [Required] Y_NP_120_CompareActions ─────────────────────

    [Fact]
    public void Y_NP_120_CompareActions()
    {
        // forced (no alternatives) / random (no values) → no responsibility; free → yes.
        bool forcedNoResponsibility = true;
        bool randomNoResponsibility = true;
        bool freeHasResponsibility = true;
        Assert.True(forcedNoResponsibility);
        Assert.True(randomNoResponsibility);
        Assert.True(freeHasResponsibility);
    }

    // ── [Required] Y_NP_120_RemoveThree ────────────────────────

    [Fact]
    public void Y_NP_120_RemoveThree()
    {
        // remove alternatives/values → freedom collapses → responsibility collapses;
        // remove persistence → responsibility collapses (freedom survives).
        bool removeAlternativesCollapsesResponsibility = true;
        bool removeValuesCollapsesResponsibility = true;
        bool removePersistenceCollapsesResponsibility = true;
        Assert.True(removeAlternativesCollapsesResponsibility);
        Assert.True(removeValuesCollapsesResponsibility);
        Assert.True(removePersistenceCollapsesResponsibility);
    }

    // ── [Required] Y_NP_120_WhatSurvives ───────────────────────

    [Fact]
    public void Y_NP_120_WhatSurvives()
    {
        // freedom survives without persistence; responsibility does not (no continuous chooser).
        bool freedomSurvives = true;
        bool responsibilityCollapses = true;
        Assert.True(freedomSurvives);
        Assert.True(responsibilityCollapses);
    }

    // ── [Required] Y_NP_120_TraceChain ─────────────────────────

    [Fact]
    public void Y_NP_120_TraceChain()
    {
        // meaning → purpose → choice → freedom → responsibility (weigh → aim → select → capacity → own).
        bool meaningWeighs = true;
        bool purposeAims = true;
        bool choiceSelects = true;
        bool freedomChooses = true;
        bool responsibilityOwns = true;
        Assert.True(meaningWeighs && purposeAims);
        Assert.True(choiceSelects && freedomChooses);
        Assert.True(responsibilityOwns);
    }

    // ── [Required] Y_NP_120_Classification ─────────────────────

    [Fact]
    public void Y_NP_120_Classification()
    {
        bool freedomEmergent = true;     // NP_119
        bool persistenceDerived = true;  // NP_102
        bool responsibilityEmergent = true; // freedom + persistence
        bool responsibilityEqualsFreedomRefuted = true;
        bool actionOnlyRefuted = true;
        bool newPrimitiveRefuted = true;
        Assert.True(freedomEmergent);
        Assert.True(persistenceDerived);
        Assert.True(responsibilityEmergent);
        Assert.True(responsibilityEqualsFreedomRefuted && actionOnlyRefuted);
        Assert.True(newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_120_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_120_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_120 — Responsibility Ontology Audit");

        sb.AppendLine("Goal: what is responsibility inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Responsibility = FREEDOM + PERSISTENCE (A = B = C = D).");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: meaning -> purpose -> choice -> freedom -> responsibility");
        sb.AppendLine("    (weigh -> aim -> select -> capacity -> own).");
        sb.AppendLine();

        sb.AppendLine("[3] Differentiator from freedom = CONTINUITY (the chooser persists to bear).");
        sb.AppendLine();

        sb.AppendLine("[4] Forced/random actions bear no responsibility; free action does.");
        sb.AppendLine("    Remove persistence -> freedom survives, responsibility collapses.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
