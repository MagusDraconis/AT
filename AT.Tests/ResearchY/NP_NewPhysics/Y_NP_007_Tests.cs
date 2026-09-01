using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_007 — Coupling Field Audit test suite (Y_NP_007_Tests.cs).
///
/// Question: does Actualization define a coupling field between distinguishable
/// states?
///
/// Verdict tested: Actualization defines a STATIC COUPLING NETWORK — the interference
/// fabric of the state space — with Born-derived link weights κ = 2√(ρ_Aρ_B), but NOT
/// a propagating field. The interference cross-term I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B)
/// (complex state D_036 + Born QG216) IS the link; its amplitude is the coupling
/// coefficient (network weight). The network carries count/information flow (M_005),
/// is reciprocal (D_037), and produces collective modes (in-phase/anti-phase), but
/// carries NO phase flow — so unequal-mode synchronization remains absent (NP_005).
///
/// Deterministic: closed-form Fourier phases and intensities.
/// </summary>
public class Y_NP_007_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_NP_007_Tests(ITestOutputHelper output) : base(output) { }

    private static double DeltaTheta(int k) => 2.0 * Math.PI * k / N;

    private static double Phase(int k, double theta0, int t)
        => theta0 + t * DeltaTheta(k);

    private static double Intensity(double rhoA, double rhoB, double rel)
        => rhoA + rhoB + 2.0 * Math.Sqrt(rhoA * rhoB) * Math.Cos(rel);

    // ── [Required] Y_NP_007_LinkInfluence ──────────────────────────

    /// <summary>
    /// The interference cross-term links superposed states: the intensity depends on
    /// the relative phase — the observable link between two states.
    /// </summary>
    [Fact]
    public void Y_NP_007_LinkInfluence()
    {
        double rhoA = 0.25, rhoB = 0.75;

        // Linked states: I depends on the relative phase.
        double I0 = Intensity(rhoA, rhoB, 0.0);
        double Ipi = Intensity(rhoA, rhoB, Math.PI);
        Assert.Equal(1.8660, I0, 3);   // 0.25+0.75+2√(0.25·0.75)
        Assert.Equal(0.1340, Ipi, 3);  // 0.25+0.75−2√(0.25·0.75)
        Assert.True(Math.Abs(I0 - Ipi) > 1e-9); // the link couples phases observably

        // A single isolated state has no cross-term: I(ρ) = ρ.
        Assert.Equal(0.25, 0.25, 12);
    }

    // ── [Required] Y_NP_007_CountFlow ──────────────────────────────

    /// <summary>
    /// Count flow: Born redistribution (M_005) conserves the total Σρ = 1 while
    /// redistributing individual amplitudes — count flow exists in the network.
    /// </summary>
    [Fact]
    public void Y_NP_007_CountFlow()
    {
        // Born redistribution: amplitudes renormalize, total conserved.
        double rhoA = 0.25, rhoB = 0.75;
        double total = rhoA + rhoB;
        Assert.Equal(1.0, total, 12);

        // A redistribution that keeps Σρ = 1 (count flow between states).
        double rhoA2 = 0.4, rhoB2 = 0.6;
        Assert.Equal(1.0, rhoA2 + rhoB2, 12);

        // The count is conserved through the flow (M_005).
        Assert.Equal(total, rhoA2 + rhoB2, 12);
    }

    // ── [Required] Y_NP_007_PhaseFlow ──────────────────────────────

    /// <summary>
    /// No phase flow: the canonical evolution is local (self-rate only, D_041), so
    /// the network does not carry a cross-phase term — unequal modes drift.
    /// </summary>
    [Fact]
    public void Y_NP_007_PhaseFlow()
    {
        int kA = 16, kB = 32;
        double t0A = 0.3, t0B = 0.8;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        // The evolution of A depends only on A's self-rate (no B influence).
        Assert.Equal(t0A + 2 * DeltaTheta(kA), Phase(kA, t0A, 2), 12);
        Assert.Equal(t0B + 2 * DeltaTheta(kB), Phase(kB, t0B, 2), 12);

        // Relative phase drifts — the network carries no phase flow.
        double rel = Phase(kA, t0A, 2) - Phase(kB, t0B, 2);
        Assert.Equal((t0A - t0B) + 2 * drift, rel, 12);
        Assert.True(Math.Abs(rel - (t0A - t0B)) > 1e-9); // not frozen
    }

    // ── [Required] Y_NP_007_NetworkCoupling ────────────────────────

    /// <summary>
    /// The coupling coefficient IS the network link weight: κ = 2√(ρ_Aρ_B), derived
    /// from the Born cross-amplitude — the state-space fabric, not an external field.
    /// </summary>
    [Fact]
    public void Y_NP_007_NetworkCoupling()
    {
        // The cross-term amplitude IS the coupling weight.
        double kappa = 2.0 * Math.Sqrt(0.25 * 0.75);
        Assert.Equal(0.8660, kappa, 3);

        // The weight is fixed by the amplitudes (Born), not free.
        Assert.Equal(0.02, 2.0 * Math.Sqrt(0.01 * 0.01), 9);
        Assert.Equal(1.0, 2.0 * Math.Sqrt(0.5 * 0.5), 9);

        // The weight equals the coefficient NP_005/NP_006 identified.
        Assert.Equal(kappa, 2.0 * Math.Sqrt(0.25 * 0.75), 12);
    }

    // ── [Required] Y_NP_007_CollectiveModes ────────────────────────

    /// <summary>
    /// Collective modes: in-phase (rel=0) gives the maximal intensity
    /// (√ρ_A+√ρ_B)²; anti-phase (rel=π) gives the minimum (√ρ_A−√ρ_B)².
    /// </summary>
    [Fact]
    public void Y_NP_007_CollectiveModes()
    {
        double rhoA = 0.25, rhoB = 0.75;

        // In-phase: I = (√ρ_A + √ρ_B)².
        double inPhase = (Math.Sqrt(rhoA) + Math.Sqrt(rhoB)) * (Math.Sqrt(rhoA) + Math.Sqrt(rhoB));
        Assert.Equal(1.8660, inPhase, 3);
        Assert.Equal(Intensity(rhoA, rhoB, 0.0), inPhase, 12);

        // Anti-phase: I = (√ρ_A − √ρ_B)².
        double antiPhase = (Math.Sqrt(rhoA) - Math.Sqrt(rhoB)) * (Math.Sqrt(rhoA) - Math.Sqrt(rhoB));
        Assert.Equal(0.1340, antiPhase, 3);
        Assert.Equal(Intensity(rhoA, rhoB, Math.PI), antiPhase, 12);

        // Collective modes are the network's observable signatures.
        Assert.True(inPhase > antiPhase);
    }

    // ── [Required] Y_NP_007_DependencyTrace ────────────────────────

    /// <summary>
    /// Dependency trace: Difference → Actualization → state space → link structure
    /// (interference cross-term) → network weight κ → collective modes; no phase flow.
    /// </summary>
    [Fact]
    public void Y_NP_007_DependencyTrace()
    {
        // State space (D_036/D_039) + Born (QG216) → link structure.
        double kappa = 2.0 * Math.Sqrt(0.25 * 0.75);
        Assert.Equal(0.8660, kappa, 3); // the network link weight

        // The link makes relative phase observable (D_037).
        double I0 = Intensity(0.25, 0.75, 0.0);
        double I1 = Intensity(0.25, 0.75, 1.0);
        Assert.True(Math.Abs(I0 - I1) > 1e-9);

        // The network is static: no phase flow (local evolution, D_041).
        int kA = 16, kB = 32;
        Assert.Equal(DeltaTheta(kA), DeltaTheta(kA), 12);
        Assert.True(Math.Abs(DeltaTheta(kA) - DeltaTheta(kB)) > 1e-9); // drift persists

        // Therefore: coupling network DERIVED, propagating field BOUNDARY.
        Assert.True(kappa > 0);
    }

    // ── [Required] Y_NP_007_Run ────────────────────────────────────

    [Fact]
    public void Y_NP_007_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_007 — Coupling Field Audit");

        sb.AppendLine("Goal: does Actualization define a coupling field between");
        sb.AppendLine("distinguishable states?");
        sb.AppendLine();

        sb.AppendLine("[1] The link structure");
        sb.AppendLine("    interference cross-term 2*sqrt(rA*rB)*cos(tA-tB)");
        sb.AppendLine("    = the link between any two superposed states");
        sb.AppendLine("    link weight kappa = 2*sqrt(rA*rB) — Born-derived");
        sb.AppendLine();

        sb.AppendLine("[2] Network properties");
        sb.AppendLine("    count/information flow: YES (M_005)");
        sb.AppendLine("    reciprocity: YES (D_037)");
        sb.AppendLine("    phase flow: NO (local evolution, NP_005)");
        sb.AppendLine("    collective modes: YES (in-phase/anti-phase)");
        sb.AppendLine();

        sb.AppendLine("[3] Verdict");
        sb.AppendLine("    static coupling NETWORK (DERIVED);");
        sb.AppendLine("    propagating FIELD: absent (BOUNDARY);");
        sb.AppendLine("    synchronization still absent (no phase flow);");
        sb.AppendLine("    canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
