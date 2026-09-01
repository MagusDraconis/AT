using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 288 — Dependency Rebuild Audit. Rebuilds dependencies using only the reduced chain
/// Difference → Actualization → Conservation → Resonance → Physics, ignoring the historical derivation
/// path. Classifies each QG result: DERIVED AGAIN / DEPENDENT ON OLD PATH / UNREACHABLE.
/// </summary>
public class ATQG_Phase288_DependencyRebuildAuditTests : ResearchTestBase
{
    public ATQG_Phase288_DependencyRebuildAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2880_ReducedChainPrimitives()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2880: the reduced-chain primitives rebuild the structural layer");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the chain Difference → Actualization → Conservation → Resonance → Physics is the");
        sb.AppendLine("    only dependency spine (historical path ignored);");
        sb.AppendLine("  - the resonance primitives (Σm, #d, #g, occMom, λ₂, span, occupancies) are the sole");
        sb.AppendLine("    inputs of every structural read.");
        sb.AppendLine();

        sb.AppendLine($"chain: {string.Join(" → ", DependencyRebuildAudit.Chain())}");
        sb.AppendLine($"conservation Σλ = 2E = N·d: {DependencyRebuildAudit.ConservationHolds()} ({DependencyRebuildAudit.ConservationTrace():F1})");
        sb.AppendLine($"count conservation (self-consistency): {DependencyRebuildAudit.CountConservationHolds()}");
        sb.AppendLine($"difference duality {{ρ, ψ}}: {DependencyRebuildAudit.DifferenceDualityHolds()}");
        sb.AppendLine($"N=96 closure (fixed point): {DependencyRebuildAudit.ClosureHolds()}");
        sb.AppendLine();
        sb.AppendLine($"family count from span: {DependencyRebuildAudit.FamilyCount()}");
        sb.AppendLine($"beat identity Σ√m/span: {DependencyRebuildAudit.BeatIdentity():F4} (≈10, 0.09%)");

        Output.WriteLine(sb.ToString());

        Assert.True(DependencyRebuildAudit.ConservationHolds(),
            "Σλ = 2E = N·d must hold (handshake lemma)");
        Assert.True(DependencyRebuildAudit.CountConservationHolds() && DependencyRebuildAudit.DifferenceDualityHolds(),
            "the difference-layer results must hold");
        Assert.True(DependencyRebuildAudit.ClosureHolds(),
            "N=96 must be the actualization fixed point");
        Assert.Equal(3, DependencyRebuildAudit.FamilyCount());
    }

    [Fact]
    public void ATQG2881_StructuralResultsRebuilt()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2881: structural physics is rebuilt from the resonance primitives alone");

        sb.AppendLine("HYPOTHESIS:");
        sb.AppendLine("  - every structural result (ratios, couplings, mixings, cosmology) is a pure function");
        sb.AppendLine("    of the resonance primitives + the assignment law — DERIVED AGAIN.");
        sb.AppendLine();

        sb.AppendLine("REBUILT (frozen vs rebuilt):");
        sb.AppendLine($"  m_μ/me:  frozen 207.03  rebuilt {DependencyRebuildAudit.MuonElectronRatio():F3}");
        sb.AppendLine($"  m_τ/m_μ: frozen 16.842  rebuilt {DependencyRebuildAudit.TauMuonRatio():F4}");
        sb.AppendLine($"  sin²θ_W: frozen 0.2316  rebuilt {DependencyRebuildAudit.Sin2ThetaW():F5}");
        sb.AppendLine($"  α_W:     frozen 0.03158 rebuilt {DependencyRebuildAudit.AlphaWeak():F6}");
        sb.AppendLine($"  Vus:     frozen 0.2211  rebuilt {DependencyRebuildAudit.Vus():F5}");
        sb.AppendLine($"  θ12:     frozen 33.35°  rebuilt {DependencyRebuildAudit.Theta12Deg():F3}°");
        sb.AppendLine($"  Ω_Λ:     frozen 0.6839  rebuilt {DependencyRebuildAudit.VacuumFraction():F5}");
        sb.AppendLine($"  Ω_m:     frozen 0.3161  rebuilt {DependencyRebuildAudit.MatterFraction():F5}");
        sb.AppendLine($"  n_s:     frozen 0.9650  rebuilt {DependencyRebuildAudit.SpectralIndex():F6}");
        sb.AppendLine($"  ℓ₂/ℓ₁:   frozen 2.4368  rebuilt {DependencyRebuildAudit.SecondToFirstPeakRatio():F5}");
        sb.AppendLine($"  ℓ₃/ℓ₁:   frozen 3.6965  rebuilt {DependencyRebuildAudit.ThirdToFirstPeakRatio():F5}");
        sb.AppendLine();
        sb.AppendLine($"max derived deviation: {DependencyRebuildAudit.MaxDerivedDeviation():F6} (< 1%)");

        Output.WriteLine(sb.ToString());

        Assert.True(DependencyRebuildAudit.MaxDerivedDeviation() < 0.01,
            "every DERIVED AGAIN result must recompute within 1%");
        Assert.True(DependencyRebuildAudit.DerivedAgainCount() > DependencyRebuildAudit.DependentCount(),
            "the structural (derived-again) class must dominate");
    }

    [Fact]
    public void ATQG2882_DependencyMapAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2882: the post-reduction dependency map");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - DERIVED AGAIN: pure function of the reduced-chain primitives;");
        sb.AppendLine("  - DEPENDENT ON OLD PATH: structure chain-derived, absolute value needs an anchor;");
        sb.AppendLine("  - UNREACHABLE: needs a free constant or structural import.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {DependencyRebuildAudit.Summary()}");
        sb.AppendLine($"Rebuild score: {DependencyRebuildAudit.RebuildScore()}/5");
        sb.AppendLine($"DERIVED AGAIN: {DependencyRebuildAudit.DerivedAgainCount()}");
        sb.AppendLine($"DEPENDENT ON OLD PATH: {DependencyRebuildAudit.DependentCount()}");
        sb.AppendLine($"UNREACHABLE: {DependencyRebuildAudit.UnreachableCount()}");
        sb.AppendLine($"derived-again fraction: {DependencyRebuildAudit.DerivedAgainFraction():P0}");
        sb.AppendLine($"CLASSIFICATION = {DependencyRebuildAudit.Classify()}");
        sb.AppendLine();

        sb.AppendLine("DEPENDENCY MAP:");
        foreach (var r in DependencyRebuildAudit.Map())
        {
            sb.AppendLine($"  [{r.Reach.ToString().ToUpperInvariant().PadRight(20)}] {r.Name} ({r.QgPhase}) — {r.Note}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal("DERIVED AGAIN", DependencyRebuildAudit.Classify());
        Assert.True(DependencyRebuildAudit.RebuildScore() >= 5);
        Assert.True(DependencyRebuildAudit.DerivedAgainCount() >= 20,
            "the structural class must be the majority of the map");
        Assert.Contains("DERIVED AGAIN", DependencyRebuildAudit.Summary());
    }
}
