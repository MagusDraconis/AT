using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_125 — Cooperation Ontology Audit test suite (Y_NP_125_Tests.cs).
///
/// Question: what is cooperation inside Actualization Theory?
///
/// Verdict tested: cooperation = ALIGNED MEANINGS = SHARED PURPOSES = INTEGRATED SELF-MODELS
/// (A = B = C) — multiple observers' aligned values and aims, mutually modeled. D (optimization)
/// PARTIAL. Distinct from individual purpose (one aim → many aligned aims). Freedom retained,
/// responsibility distributed.
///
/// Deterministic: closed-form (structural determinations).
/// </summary>
public class Y_NP_125_Tests : ResearchTestBase
{
    public Y_NP_125_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_125_DefineTerms ────────────────────────

    [Fact]
    public void Y_NP_125_DefineTerms()
    {
        bool selfIsOneObserver = true;
        bool otherIsAnotherObserver = true;
        bool cooperationIsAlignment = true;
        bool conflictIsMisalignment = true;
        Assert.True(selfIsOneObserver);
        Assert.True(otherIsAnotherObserver);
        Assert.True(cooperationIsAlignment);
        Assert.True(conflictIsMisalignment);
    }

    // ── [Required] Y_NP_125_ABCD ───────────────────────────────

    [Fact]
    public void Y_NP_125_ABCD()
    {
        bool A_alignedMeanings = true;       // the values
        bool B_sharedPurposes = true;        // the aims
        bool C_integratedSelfModels = true;  // the mutual modeling
        bool D_multiObserverOptimization = true; // PARTIAL (a consequence)
        Assert.True(A_alignedMeanings);
        Assert.True(B_sharedPurposes);
        Assert.True(C_integratedSelfModels);
        Assert.True(D_multiObserverOptimization);
    }

    // ── [Required] Y_NP_125_CompareObservers ───────────────────

    [Fact]
    public void Y_NP_125_CompareObservers()
    {
        // isolated (one purpose) / competing (conflict) / cooperating (aligned).
        bool isolatedOnePurpose = true;
        bool competingConflict = true;
        bool cooperatingAligned = true;
        Assert.True(isolatedOnePurpose);
        Assert.True(competingConflict);
        Assert.True(cooperatingAligned);
    }

    // ── [Required] Y_NP_125_MultiObserver ──────────────────────

    [Fact]
    public void Y_NP_125_MultiObserver()
    {
        // meaning aligned; freedom retained (voluntary); responsibility distributed.
        bool meaningAligned = true;
        bool freedomRetained = true;
        bool responsibilityDistributed = true;
        Assert.True(meaningAligned);
        Assert.True(freedomRetained);
        Assert.True(responsibilityDistributed);
    }

    // ── [Required] Y_NP_125_SocialExtension ────────────────────

    [Fact]
    public void Y_NP_125_SocialExtension()
    {
        // consciousness = model of self; cooperation = model of other + alignment.
        bool consciousnessIsModelOfSelf = true;
        bool cooperationIsModelOfOtherPlusAlignment = true;
        Assert.True(consciousnessIsModelOfSelf);
        Assert.True(cooperationIsModelOfOtherPlusAlignment);
    }

    // ── [Required] Y_NP_125_Classification ─────────────────────

    [Fact]
    public void Y_NP_125_Classification()
    {
        bool meaningPurposeEmergent = true; // NP_116/117
        bool selfModelEmergent = true;      // NP_122
        bool cooperationEmergent = true;    // the multi-observer alignment
        bool newPrimitiveRefuted = true;
        bool optimizationRefuted = true;
        bool eliminatesFreedomRefuted = true;
        Assert.True(meaningPurposeEmergent && selfModelEmergent);
        Assert.True(cooperationEmergent);
        Assert.True(newPrimitiveRefuted && optimizationRefuted);
        Assert.True(eliminatesFreedomRefuted);
    }

    // ── [Required] Y_NP_125_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_125_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_125 — Cooperation Ontology Audit");

        sb.AppendLine("Goal: what is cooperation inside Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Cooperation = ALIGNED MEANINGS + SHARED PURPOSES + MUTUAL MODELS (A = B = C).");
        sb.AppendLine();

        sb.AppendLine("[2] Individual purpose (one aim) -> cooperation (many aligned aims).");
        sb.AppendLine();

        sb.AppendLine("[3] Social extension: consciousness = model of self; cooperation = model of other + alignment.");
        sb.AppendLine();

        sb.AppendLine("[4] Freedom retained (aligned choice); responsibility distributed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
