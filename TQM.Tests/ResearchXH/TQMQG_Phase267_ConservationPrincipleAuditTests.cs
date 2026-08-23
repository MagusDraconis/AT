using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 267 — Conservation Principle Audit. Are all conservation laws manifestations of one
/// deeper principle? D96 only, no observables.
/// </summary>
public class TQMQG_Phase267_ConservationPrincipleAuditTests : ResearchTestBase
{
    public TQMQG_Phase267_ConservationPrincipleAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2670_ConservationLaws()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2670: the six conservation laws");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the conservation laws across QG61-74, QG181-223, QG260-266;");
        sb.AppendLine("  - each law is verified deterministically.");
        sb.AppendLine();

        foreach (var l in ConservationPrincipleAudit.Laws())
            sb.AppendLine($"  [{(l.Holds ? "HOLDS" : "FAILS"),-5}] {l.Name,-24} ({l.Phase})");
        sb.AppendLine();
        sb.AppendLine($"Verified: {ConservationPrincipleAudit.VerifiedCount()}/6");

        Output.WriteLine(sb.ToString());

        Assert.Equal(6, ConservationPrincipleAudit.Laws().Length);
        Assert.True(ConservationPrincipleAudit.VerifiedCount() == 6, "all six conservation laws hold");
    }

    [Fact]
    public void TQMQG2671_CommonProjection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2671: the common projection — conservation of the actualization count N");

        sb.AppendLine("HYPOTHESIS: every conservation law is a projection of the single fact that the");
        sb.AppendLine("total actualization count N is conserved.");
        sb.AppendLine();

        foreach (var l in ConservationPrincipleAudit.Laws())
            sb.AppendLine($"  {l.Name,-24} → {l.Projection}");
        sb.AppendLine();
        sb.AppendLine("The primitive statement: N = ∫ρ dV is conserved (Noether / time-translation, QG89).");
        sb.AppendLine("Every other law measures this one conserved quantity differently.");

        Output.WriteLine(sb.ToString());

        Assert.True(ConservationPrincipleAudit.VerifiedCount() == 6);
        // The primitive count conservation holds.
        Assert.True(NativeMetricDynamics.CountConserved(2.0, 8));
        // Norm conservation is the normalized count.
        Assert.True(QuantumAmplitudeOrigin.BornRuleHoldsForAnyMu());
    }

    [Fact]
    public void TQMQG2672_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2672: the unification determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables):");
        sb.AppendLine("  - MULTIPLE CONSERVATIONS (score ≤ 2), PARTIAL UNIFICATION (3-4),");
        sb.AppendLine("    UNIVERSAL CONSERVATION PRINCIPLE (5-6);");
        sb.AppendLine("  - the unification claim: all six laws project onto the conserved count N.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ConservationPrincipleAudit.Summary()}");
        sb.AppendLine($"Unification score: {ConservationPrincipleAudit.UnificationScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {ConservationPrincipleAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - NORM: ρ = μ^k/S is the normalized share of N → Σρ = 1 is 'count normalized';");
        sb.AppendLine("  - COUNT: N itself conserved (the primitive statement);");
        sb.AppendLine("  - TRACE: trace(L) = 2·links = 2·(network event count), fixed by the N=96 attractor;");
        sb.AppendLine("  - UNITARITY: V†V = I preserves the total norm (= the conserved share);");
        sb.AppendLine("  - BIANCHI: ∇·T = 0 from deficit-count conservation; ∇·G = 0 is its geometric form;");
        sb.AppendLine("  - NOETHER: energy = actualization rate = time-conjugate of the count.");
        sb.AppendLine("  - CONCLUSION: the conservation laws are NOT independent — they are different");
        sb.AppendLine("    measurements of ONE principle: the actualization count N is conserved.");
        sb.AppendLine("  - Honest caveat: the trace identity is also a universal graph property (handshake");
        sb.AppendLine("    lemma); its SPECIFIC value is set by the N=96 attractor. The unification is the");
        sb.AppendLine("    reduction of all laws to count conservation, not a claim that the lemma is unique.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL CONSERVATION PRINCIPLE", ConservationPrincipleAudit.Classify());
        Assert.True(ConservationPrincipleAudit.UnificationScore() >= 5);
        Assert.Contains("UNIVERSAL CONSERVATION PRINCIPLE", ConservationPrincipleAudit.Summary());
    }
}
