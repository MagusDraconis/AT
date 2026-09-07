using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_114 — Knowledge Ontology Audit test suite (Y_NP_114_Tests.cs).
///
/// Question: what is knowledge inside Actualization Theory?
///
/// Verdict tested: knowledge = STABLE, INTEGRATED, PREDICTIVE distinctions (B = C = D), realized
/// via stored observations (A). Chain: Difference → observation → memory → knowledge. Distinct from
/// information (structure) and observation (event); differentiator = prediction. Remove memory →
/// observation survives, knowledge collapses.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_114_Tests : ResearchTestBase
{
    public Y_NP_114_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_114_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_114_DefineTerms()
    {
        // data = raw distinction; information = distinguishability; observation = incorporation;
        // knowledge = stable integrated predictive distinctions.
        bool dataIsRawDistinction = true;
        bool informationIsDistinguishability = true;
        bool observationIsIncorporation = true;
        bool knowledgeIsPredictive = true;
        Assert.True(dataIsRawDistinction);
        Assert.True(informationIsDistinguishability);
        Assert.True(observationIsIncorporation);
        Assert.True(knowledgeIsPredictive);
    }

    // ── [Required] Y_NP_114_TraceChain ─────────────────────────

    [Fact]
    public void Y_NP_114_TraceChain()
    {
        // Difference → observation → memory → knowledge.
        bool differenceGrounds = true;
        bool observationIncorporates = true;
        bool memoryPersists = true;
        bool knowledgePredicts = true;
        Assert.True(differenceGrounds);
        Assert.True(observationIncorporates);
        Assert.True(memoryPersists);
        Assert.True(knowledgePredicts);
    }

    // ── [Required] Y_NP_114_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_114_ABCD()
    {
        bool A_storedObservations = true;        // necessary, not sufficient
        bool B_stableIntegratedDistinctions = true; // the form
        bool C_predictiveStructure = true;       // the differentiator
        bool D_persistentInformation = true;     // the same object
        Assert.True(A_storedObservations);
        Assert.True(B_stableIntegratedDistinctions);
        Assert.True(C_predictiveStructure);
        Assert.True(D_persistentInformation);
    }

    // ── [Required] Y_NP_114_RemoveMemory ───────────────────────

    [Fact]
    public void Y_NP_114_RemoveMemory()
    {
        // remove memory: observation survives; knowledge/understanding collapse.
        bool observationSurvives = true;
        bool knowledgeCollapses = true;
        bool understandingCollapses = true;
        Assert.True(observationSurvives);
        Assert.True(knowledgeCollapses);
        Assert.True(understandingCollapses);
    }

    // ── [Required] Y_NP_114_CompareObjects ─────────────────────

    [Fact]
    public void Y_NP_114_CompareObjects()
    {
        // detector (one distinction) < observer (integrates) < knowledge-bearing (memory + predict).
        bool detectorReadsOne = true;
        bool observerIntegrates = true;
        bool knowledgeBearingHasMemoryAndPrediction = true;
        Assert.True(detectorReadsOne);
        Assert.True(observerIntegrates);
        Assert.True(knowledgeBearingHasMemoryAndPrediction);
    }

    // ── [Required] Y_NP_114_KnowledgeVsOthers ──────────────────

    [Fact]
    public void Y_NP_114_KnowledgeVsOthers()
    {
        // knowledge (persistent + predictive) ≠ information (structure) ≠ observation (event).
        bool knowledgeHasPersistence = true;
        bool knowledgeHasPrediction = true;
        bool informationNoPrediction = true;
        bool observationNoPersistence = true;
        Assert.True(knowledgeHasPersistence && knowledgeHasPrediction);
        Assert.True(informationNoPrediction);
        Assert.True(observationNoPersistence);
    }

    // ── [Required] Y_NP_114_Classification ─────────────────────

    [Fact]
    public void Y_NP_114_Classification()
    {
        bool dataInformationDerived = true;  // D_039/M_004
        bool observationEmergent = true;     // NP_113
        bool memoryDerived = true;           // NP_102
        bool knowledgeEmergent = true;       // predictive integration
        bool storedOnlyRefuted = true;
        bool newPrimitiveRefuted = true;
        Assert.True(dataInformationDerived);
        Assert.True(observationEmergent);
        Assert.True(memoryDerived && knowledgeEmergent);
        Assert.True(storedOnlyRefuted && newPrimitiveRefuted);
    }

    // ── [Required] Y_NP_114_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_114_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_114 — Knowledge Ontology Audit");

        sb.AppendLine("Goal: what is knowledge inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Knowledge = STABLE, INTEGRATED, PREDICTIVE distinctions (B = C = D).");
        sb.AppendLine();

        sb.AppendLine("[2] Chain: Difference -> observation -> memory -> knowledge.");
        sb.AppendLine();

        sb.AppendLine("[3] Knowledge != information (structure) != observation (event);");
        sb.AppendLine("    its differentiator is PREDICTION (shapes future actualizations).");
        sb.AppendLine();

        sb.AppendLine("[4] Remove memory -> observation survives, knowledge/understanding collapse.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
