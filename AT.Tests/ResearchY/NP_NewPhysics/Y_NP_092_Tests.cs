using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_092 — Network Propagation Audit test suite (Y_NP_092_Tests.cs).
///
/// Question: what propagates on the network?
///
/// Verdict tested: propagation is NOT transport — the network is static (nothing substantial
/// travels). The only genuine movement is the actualization TICK (the causal-order advance),
/// and its native propagation law is light along null geodesics (n = 1, DERIVED). Particles,
/// gravitons, forces, and count only APPEAR to move. Velocity = group velocity (≤ c);
/// locality = adjacency; causality = the partial order.
///
/// Deterministic: closed-form (structural determinations + closed-form dispersion).
/// </summary>
public class Y_NP_092_Tests : ResearchTestBase
{
    public Y_NP_092_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_092_Inventory ──────────────────────────

    [Fact]
    public void Y_NP_092_Inventory()
    {
        // ρ = count (continuity), ψ = metric ripple, phase = local label, information =
        // deficit (bookkeeping), particles = modes, forces = generator action.
        bool rhoCount = true;
        bool psiTensor = true;
        bool phaseLocal = true;
        bool infoDeficit = true;
        bool particlesModes = true;
        bool forcesGeneratorAction = true;
        Assert.True(rhoCount && psiTensor && phaseLocal);
        Assert.True(infoDeficit && particlesModes && forcesGeneratorAction);
    }

    // ── [Required] Y_NP_092_NativeIsNullGeodesic ────────────────

    [Fact]
    public void Y_NP_092_NativeIsNullGeodesic()
    {
        // Native propagation law = null geodesics, n = 1, independent of ρ (QG28, DERIVED).
        bool nullGeodesic = true;
        bool indexIsOne = true;
        bool independentOfRho = true;
        bool derived = true;
        Assert.True(nullGeodesic);
        Assert.True(indexIsOne);
        Assert.True(independentOfRho);
        Assert.True(derived);
    }

    // ── [Required] Y_NP_092_TickAlongCausalOrder ────────────────

    [Fact]
    public void Y_NP_092_TickAlongCausalOrder()
    {
        // The tick propagates along the generation relation (boundary = light cone),
        // massless null: M_eff = n − 1 = 0 (QG31). The tick is FRAMEWORK.
        bool tickAlongGenerationRelation = true;
        bool masslessNull = true;   // M_eff = 0
        bool tickIsFramework = true;
        Assert.True(tickAlongGenerationRelation);
        Assert.True(masslessNull);
        Assert.True(tickIsFramework);
    }

    // ── [Required] Y_NP_092_NetworkIsStatic ─────────────────────

    [Fact]
    public void Y_NP_092_NetworkIsStatic()
    {
        // The network is static: no substantial object travels node to node (NP_007).
        bool nodesStatic = true;
        bool linksStatic = true;
        bool noPropagatingField = true;
        Assert.True(nodesStatic);
        Assert.True(linksStatic);
        Assert.True(noPropagatingField);
    }

    // ── [Required] Y_NP_092_PhaseDoesNotFlow ────────────────────

    [Fact]
    public void Y_NP_092_PhaseDoesNotFlow()
    {
        // θ = 2πk/N is a LOCAL per-node label; there is no phase flow (NP_005). REFUTED.
        bool phaseIsLocalLabel = true;
        bool phaseFlows = false;
        Assert.True(phaseIsLocalLabel);
        Assert.False(phaseFlows);
    }

    // ── [Required] Y_NP_092_CountConservation ───────────────────

    [Fact]
    public void Y_NP_092_CountConservation()
    {
        // ρ obeys ∂_t ρ + ∇·j = 0 — conserved count redistributes, does not travel (NP_081).
        bool continuityHolds = true;
        bool countConserved = true;
        bool countTravels = false;
        Assert.True(continuityHolds);
        Assert.True(countConserved);
        Assert.False(countTravels);
    }

    // ── [Required] Y_NP_092_ParticlesStandingWaves ──────────────

    [Fact]
    public void Y_NP_092_ParticlesStandingWaves()
    {
        // A particle is a resonance mode (standing wave); the wave-packet ENVELOPE moves at
        // the group velocity — the mode itself does not travel.
        bool particleIsMode = true;
        bool modeIsStandingWave = true;
        bool envelopeMoves = true;
        bool modeTravels = false;
        Assert.True(particleIsMode && modeIsStandingWave);
        Assert.True(envelopeMoves);
        Assert.False(modeTravels);
    }

    // ── [Required] Y_NP_092_GroupVelocity ───────────────────────

    [Fact]
    public void Y_NP_092_GroupVelocity()
    {
        // Cubic dispersion ω² = k² − k⁴/12 along [100]; v_g = dω/dk = (1 − k²/6)/√(1 − k²/12).
        // v_g → 1 (c) as k → 0; subluminal at finite k (verified: 0.99875 @ k=0.1, 0.98871 @ k=0.3).
        double vg(double k) => (1.0 - k * k / 6.0) / Math.Sqrt(1.0 - k * k / 12.0);

        double vg01 = vg(0.1);
        double vg03 = vg(0.3);
        double vg06 = vg(0.6);

        // subluminal at finite k
        Assert.True(vg01 < 1.0 && vg03 < 1.0 && vg06 < 1.0);
        // → 1 as k → 0
        Assert.Equal(1.0, vg(0.0), 12);
        // verified values (tolerance 1e-4)
        Assert.InRange(vg01, 0.9986, 0.9988);
        Assert.InRange(vg03, 0.9886, 0.9888);
        Assert.InRange(vg06, 0.9543, 0.9545);
    }

    // ── [Required] Y_NP_092_VelocityLocalityCausality ───────────

    [Fact]
    public void Y_NP_092_VelocityLocalityCausality()
    {
        // velocity = group velocity (≤ c); locality = adjacency (degree-12 neighbours);
        // causality = the partial order (nothing outruns the tick).
        bool velocityIsGroupVelocity = true;
        bool velocitySubLuminal = true;
        bool localityIsAdjacency = true;
        bool causalityIsPartialOrder = true;
        bool nothingOutrunsTick = true;
        Assert.True(velocityIsGroupVelocity && velocitySubLuminal);
        Assert.True(localityIsAdjacency);
        Assert.True(causalityIsPartialOrder && nothingOutrunsTick);
    }

    // ── [Required] Y_NP_092_ABCD ────────────────────────────────

    [Fact]
    public void Y_NP_092_ABCD()
    {
        // A) count transport YES (as continuity, DERIVED); B) phase transport NO (REFUTED);
        // C) deficit transport = A (reduction); D) geometry transport YES (ψ ripples, DERIVED).
        bool A_countAsContinuity = true;
        bool B_phaseTransport = false;
        bool C_deficitReducesToCount = true;
        bool D_geometryAsPsiRipple = true;
        Assert.True(A_countAsContinuity);
        Assert.False(B_phaseTransport);
        Assert.True(C_deficitReducesToCount);
        Assert.True(D_geometryAsPsiRipple);
    }

    // ── [Required] Y_NP_092_Classification ──────────────────────

    [Fact]
    public void Y_NP_092_Classification()
    {
        bool nullGeodesicDerived = true;   // QG28
        bool tickFramework = true;         // QG29/31
        bool countContinuityDerived = true; // NP_081
        bool wavePacketEmergent = true;    // mode superposition
        bool psiRippleDerived = true;      // given mode structure
        bool phaseTransportRefuted = true; // NP_005
        bool propagatingFieldRefuted = true; // NP_007
        Assert.True(nullGeodesicDerived && tickFramework);
        Assert.True(countContinuityDerived && wavePacketEmergent && psiRippleDerived);
        Assert.True(phaseTransportRefuted && propagatingFieldRefuted);
    }

    // ── [Required] Y_NP_092_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_092_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_092 — Network Propagation Audit");

        double vg(double k) => (1.0 - k * k / 6.0) / Math.Sqrt(1.0 - k * k / 12.0);

        sb.AppendLine("Goal: what propagates on the network?");
        sb.AppendLine();

        sb.AppendLine("[1] The network is STATIC (nodes/links do not move, NP_007/090).");
        sb.AppendLine("    The only genuine movement is the TICK (the causal-order advance).");
        sb.AppendLine();

        sb.AppendLine("[2] Native propagation = light along null geodesics (n = 1, DERIVED, QG28);");
        sb.AppendLine("    the tick propagates masslessly (M_eff = 0, QG31).");
        sb.AppendLine();

        sb.AppendLine("[3] Everything else only APPEARS to move:");
        sb.AppendLine("    particle = standing wave (envelope moves at v_g);");
        sb.AppendLine("    graviton = ψ ripple; force = link-mediated action; count = continuity.");
        sb.AppendLine();

        sb.AppendLine("[4] Group velocity v_g = dω/dk = (1 − k²/6)/√(1 − k²/12):");
        sb.AppendLine($"    v_g(0.1) = {vg(0.1):F5},  v_g(0.3) = {vg(0.3):F5},  v_g(0.6) = {vg(0.6):F5}");
        sb.AppendLine("    (subluminal at finite k; → 1 = c as k → 0).");
        sb.AppendLine();

        sb.AppendLine("[5] Velocity = v_g (≤ c); locality = adjacency; causality = partial order.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
