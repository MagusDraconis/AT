using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 292 — Foundation Stress Test. Remove one foundation item at a time
/// ({Difference, η}, QG291) and determine which layers survive: Actualization, Conservation,
/// Resonance, Spectrum, Physics. No observables, no target values, D96 only, deterministic.
/// </summary>
public class ATQG_Phase292_FoundationStressTestTests : ResearchTestBase
{
    public ATQG_Phase292_FoundationStressTestTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2920_CaseADifferenceRemoved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2920: Case A — Difference removed: NOTHING survives");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - count conservation is the DEFINITIONAL identity of the primitive (QG268);");
        sb.AppendLine("  - the N=96 network is the Difference-driven actualization attractor (QG282);");
        sb.AppendLine("  - without Difference: no unit, no count, no network, no spectrum, no physics.");
        sb.AppendLine();

        sb.AppendLine($"count conservation is Difference's identity: {FoundationStressTest.CountConservationIsDifferenceIdentity()}");
        sb.AppendLine($"network is the actualization attractor: {FoundationStressTest.NetworkIsActualizationAttractor()}");
        sb.AppendLine($"conservation is a graph identity: {FoundationStressTest.ConservationIsGraphIdentity()}");
        sb.AppendLine();
        sb.AppendLine("LAYER SURVIVAL (Case A — Difference removed):");
        foreach (var l in FoundationStressTest.Layers())
        {
            sb.AppendLine($"  {l.Layer.PadRight(14)} → {l.CaseA}");
        }
        sb.AppendLine();
        sb.AppendLine($"layers surviving: {FoundationStressTest.CaseASurviving()}/5");
        sb.AppendLine($"nothing survives: {FoundationStressTest.CaseANothingSurvives()}");

        Output.WriteLine(sb.ToString());

        Assert.True(FoundationStressTest.CountConservationIsDifferenceIdentity(),
            "count conservation must be the definitional identity of Difference");
        Assert.True(FoundationStressTest.NetworkIsActualizationAttractor(),
            "the network must be the Difference-driven attractor");
        Assert.True(FoundationStressTest.CaseANothingSurvives(),
            "removing Difference must collapse all five layers");
        Assert.Equal(0, FoundationStressTest.CaseASurviving());
    }

    [Fact]
    public void ATQG2921_CaseBEtaRemoved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2921: Case B — η removed: the counting/scalar chain survives");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - conservation is a GRAPH identity (handshake lemma), not a metric identity;");
        sb.AppendLine("  - the spectrum is the graph Laplacian eigenspectrum — no η needed;");
        sb.AppendLine("  - ψ enters no scalar prediction (QG287) — the scalar physics is a ρ-face read;");
        sb.AppendLine("  - only the TENSOR (Weyl) sector needs η (ψ = difference from conformal flatness).");
        sb.AppendLine();

        sb.AppendLine($"conservation is a graph identity: {FoundationStressTest.ConservationIsGraphIdentity()}");
        sb.AppendLine($"spectrum is the graph Laplacian: {FoundationStressTest.SpectrumIsGraphLaplacian()}");
        sb.AppendLine($"scalar physics does not need η: {FoundationStressTest.ScalarPhysicsDoesNotNeedEta()}");
        sb.AppendLine($"Weyl content defined against η: {FoundationStressTest.WeylDefinedAgainstEta()}");
        sb.AppendLine();
        sb.AppendLine("LAYER SURVIVAL (Case B — η removed):");
        foreach (var l in FoundationStressTest.Layers())
        {
            sb.AppendLine($"  {l.Layer.PadRight(14)} → {l.CaseB}");
        }
        sb.AppendLine();
        sb.AppendLine($"layers surviving: {FoundationStressTest.CaseBSurviving()}/5");
        sb.AppendLine($"counting/scalar chain survives: {FoundationStressTest.CaseBCountingChainSurvives()}");
        sb.AppendLine($"η necessary only for tensor: {FoundationStressTest.EtaNecessaryOnlyForTensor()}");

        Output.WriteLine(sb.ToString());

        Assert.True(FoundationStressTest.ConservationIsGraphIdentity(),
            "conservation must be a graph identity");
        Assert.True(FoundationStressTest.ScalarPhysicsDoesNotNeedEta(),
            "ψ/η must enter no scalar prediction");
        Assert.True(FoundationStressTest.WeylDefinedAgainstEta(),
            "the Weyl content must be defined against η");
        Assert.True(FoundationStressTest.CaseBCountingChainSurvives(),
            "the counting/spectral/scalar chain must survive η removal");
        Assert.True(FoundationStressTest.EtaNecessaryOnlyForTensor(),
            "η must be necessary only for the tensor sector");
    }

    [Fact]
    public void ATQG2922_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2922: the foundation determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - FOUNDATION REDUNDANT: removing a foundation item changes nothing;");
        sb.AppendLine("  - FOUNDATION NECESSARY: removing an item destroys the layers it supports;");
        sb.AppendLine("  - MINIMAL FOUNDATION CONFIRMED: Difference is the root (Case A: nothing survives)");
        sb.AppendLine("    and η is the tensor reference (Case B: scalar chain survives, Weyl fails).");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FoundationStressTest.Summary()}");
        sb.AppendLine($"Stress score: {FoundationStressTest.StressScore()}/5");
        sb.AppendLine($"Case A (Difference removed): {FoundationStressTest.CaseASurviving()}/5 layers survive");
        sb.AppendLine($"Case B (η removed): {FoundationStressTest.CaseBSurviving()}/5 layers survive");
        sb.AppendLine($"CLASSIFICATION = {FoundationStressTest.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - Difference is the ROOT: removing it collapses ALL five layers (Actualization,");
        sb.AppendLine("    Conservation, Resonance, Spectrum, Physics) — count conservation is its");
        sb.AppendLine("    definitional identity (QG268) and the N=96 network is its attractor (QG282).");
        sb.AppendLine("  - η is necessary ONLY for the TENSOR/Weyl sector: the counting/spectral/scalar");
        sb.AppendLine("    chain (4/5 layers) survives without it — conservation is a graph identity");
        sb.AppendLine("    (2E = N·d), the spectrum is the graph Laplacian eigenspectrum, and ψ enters no");
        sb.AppendLine("    scalar prediction (QG287) — but the Weyl content ψ is defined against η (QG285).");
        sb.AppendLine("  - The minimal foundation {Difference, η} is CONFIRMED: Difference is the universal");
        sb.AppendLine("    root, η is the tensor-sector reference. Neither is redundant.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MINIMAL FOUNDATION CONFIRMED", FoundationStressTest.Classify());
        Assert.True(FoundationStressTest.StressScore() >= 5);
        Assert.Contains("MINIMAL FOUNDATION CONFIRMED", FoundationStressTest.Summary());
    }
}
