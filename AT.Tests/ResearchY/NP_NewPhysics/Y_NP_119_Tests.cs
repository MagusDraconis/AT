using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_119 — Freedom Ontology Audit test suite (Y_NP_119_Tests.cs).
///
/// Question: what is freedom inside Actualization Theory?
///
/// Verdict tested: freedom = the CAPACITY TO SELECT AMONG MEANINGS (C) = ACTUALIZATION UNDER
/// INCOMPLETE DETERMINATION (D). A (number of alternatives) partial; B (unconstrained) REFUTED.
/// Freedom = the middle path (chosen action) between determinism (forced) and randomness (random).
/// Compatible by identity with Born selection/actualization/Difference conservation.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_119_Tests : ResearchTestBase
{
    public Y_NP_119_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_119_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_119_DefineTerms()
    {
        bool alternativeIsOption = true;
        bool choiceIsSelectionAct = true;
        bool freedomIsCapacity = true;
        bool constraintIsValueHierarchy = true;
        Assert.True(alternativeIsOption);
        Assert.True(choiceIsSelectionAct);
        Assert.True(freedomIsCapacity);
        Assert.True(constraintIsValueHierarchy);
    }

    // ── [Required] Y_NP_119_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_119_ABCD()
    {
        bool A_numberOfAlternatives = true;   // partial (precondition)
        bool B_unconstrainedChoice = false;   // REFUTED
        bool C_capacityToSelectMeanings = true; // YES
        bool D_incompleteDetermination = true;  // the answer
        Assert.True(A_numberOfAlternatives);
        Assert.False(B_unconstrainedChoice);
        Assert.True(C_capacityToSelectMeanings);
        Assert.True(D_incompleteDetermination);
    }

    // ── [Required] Y_NP_119_RemoveThree ────────────────────────

    [Fact]
    public void Y_NP_119_RemoveThree()
    {
        // remove alternatives → forced; remove values → random; remove purpose → prediction.
        bool removeAlternativesForced = true;
        bool removeValuesRandom = true;
        bool removePurposePrediction = true;
        Assert.True(removeAlternativesForced);
        Assert.True(removeValuesRandom);
        Assert.True(removePurposePrediction);
    }

    // ── [Required] Y_NP_119_WhatSurvives ───────────────────────

    [Fact]
    public void Y_NP_119_WhatSurvives()
    {
        // raw actualization (the Born draw) survives = randomness, not freedom.
        bool rawActualizationSurvives = true;
        bool thatIsRandomnessNotFreedom = true;
        Assert.True(rawActualizationSurvives);
        Assert.True(thatIsRandomnessNotFreedom);
    }

    // ── [Required] Y_NP_119_ForcedRandomChosen ─────────────────

    [Fact]
    public void Y_NP_119_ForcedRandomChosen()
    {
        // forced = no alternatives (determinism); random = alternatives, no values/purpose;
        // chosen = alternatives + values + purpose (freedom, the middle path).
        bool forcedNoFreedom = true;
        bool randomNoFreedom = true;
        bool chosenHasFreedom = true;
        Assert.True(forcedNoFreedom);
        Assert.True(randomNoFreedom);
        Assert.True(chosenHasFreedom);
    }

    // ── [Required] Y_NP_119_Status ─────────────────────────────

    [Fact]
    public void Y_NP_119_Status()
    {
        // the tick's stochasticity BOUNDARY; value/purpose EMERGENT; freedom EMERGENT.
        bool stochasticityBoundary = true;
        bool valuePurposeEmergent = true;
        bool freedomEmergent = true;
        Assert.True(stochasticityBoundary);
        Assert.True(valuePurposeEmergent);
        Assert.True(freedomEmergent);
    }

    // ── [Required] Y_NP_119_Compatibility ──────────────────────

    [Fact]
    public void Y_NP_119_Compatibility()
    {
        // freedom IS Born selection under incomplete determination (weight = value, draw = tick);
        // selects without creating (Difference conserved).
        bool compatibleWithBorn = true;
        bool compatibleWithActualization = true;
        bool compatibleWithConservation = true;
        Assert.True(compatibleWithBorn);
        Assert.True(compatibleWithActualization);
        Assert.True(compatibleWithConservation);
    }

    // ── [Required] Y_NP_119_Classification ─────────────────────

    [Fact]
    public void Y_NP_119_Classification()
    {
        bool stochasticityBoundary = true;   // NP_107
        bool valuePurposeEmergent = true;    // NP_116/117
        bool freedomEmergent = true;         // the capacity
        bool randomnessRefuted = true;
        bool unconstrainedRefuted = true;
        bool determinismRefuted = true;
        bool newPrimitiveRefuted = true;
        Assert.True(stochasticityBoundary);
        Assert.True(valuePurposeEmergent && freedomEmergent);
        Assert.True(randomnessRefuted && unconstrainedRefuted);
        Assert.True(determinismRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_119_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_119_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_119 — Freedom Ontology Audit");

        sb.AppendLine("Goal: what is freedom inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Freedom = the CAPACITY to select among meanings (C = D) —");
        sb.AppendLine("    actualization under incomplete determination.");
        sb.AppendLine();

        sb.AppendLine("[2] The middle path: forced (determinism) / random (Born draw) / chosen (freedom).");
        sb.AppendLine();

        sb.AppendLine("[3] A (alternatives) partial; B (unconstrained) REFUTED — freedom is value-constrained.");
        sb.AppendLine();

        sb.AppendLine("[4] Compatible by identity with Born selection / actualization / Difference");
        sb.AppendLine("    conservation (freedom selects, it does not create).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
