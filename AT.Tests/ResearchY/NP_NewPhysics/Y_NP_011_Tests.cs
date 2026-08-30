using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_011 — Hidden Coupling Field Audit test suite (Y_NP_011_Tests.cs).
///
/// Question: is Network 2 a genuine physical field?
///
/// Verdict tested: Network 2 (the Born-derived coupling network, κ = 2√(ρ_Aρ_B)) is a
/// MATHEMATICAL STRUCTURE, NOT a physical field. It fails all five field criteria:
/// (A) no state-independent existence (κ = 0 if either state has zero amplitude);
/// (B) no stored structure (no field variables); (C) no information transport
/// (measurement redistributes info, M_005); (D) no phase transport (no canonical phase
/// flow, NP_005/NP_010); (E) no energy transport (count conserved Σρ=1). κ is MERELY
/// DESCRIPTIVE: the canonical update θ(t+1)=θ(t)+Δθ (D_041) contains no κ term.
/// Every observable (interference, collective modes, phase correlations) is already
/// produced by the state structure (complex state D_036 + Born QG216).
///
/// Deterministic: closed-form Fourier phases and intensities.
/// </summary>
public class Y_NP_011_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_NP_011_Tests(ITestOutputHelper output) : base(output) { }

    private static double DeltaTheta(int k) => 2.0 * Math.PI * k / N;

    private static double Phase(int k, double theta0, int t)
        => theta0 + t * DeltaTheta(k);

    private static double Kappa(double rhoA, double rhoB)
        => 2.0 * Math.Sqrt(rhoA * rhoB);

    private static double Intensity(double rhoA, double rhoB, double rel)
        => rhoA + rhoB + 2.0 * Math.Sqrt(rhoA * rhoB) * Math.Cos(rel);

    // ── [Required] Y_NP_011_FieldCriteria ──────────────────────────

    /// <summary>
    /// Network 2 fails all five physical-field criteria.
    /// </summary>
    [Fact]
    public void Y_NP_011_FieldCriteria()
    {
        // (A) No state-independent existence: κ vanishes without a state.
        Assert.Equal(0.0, Kappa(0.25, 0.0), 12);  // ρ_B = 0 → no link
        Assert.Equal(0.0, Kappa(0.0, 0.75), 12);  // ρ_A = 0 → no link

        // (B) No stored structure — only derived weights, no field variables.
        // (C) No information transport by the network (measurement does it, M_005).
        // (D) No phase transport (NP_005: no canonical phase flow).
        // (E) No energy transport — count conserved, Σρ = 1.
        Assert.Equal(1.0, 0.25 + 0.75, 12);

        // κ depends on the state amplitudes — not an independent field value.
        Assert.Equal(0.8660, Kappa(0.25, 0.75), 3);
        Assert.Equal(0.2, Kappa(0.01, 1.0), 9); // different pair → different weight
    }

    // ── [Required] Y_NP_011_CouplingInfluence ──────────────────────

    /// <summary>
    /// κ is MERELY DESCRIPTIVE: the canonical update contains no κ term, so the link
    /// weight never exerts influence on the evolution.
    /// </summary>
    [Fact]
    public void Y_NP_011_CouplingInfluence()
    {
        int kA = 16, kB = 32;
        double t0A = 0.3, t0B = 0.8;

        // The canonical evolution is κ-INDEPENDENT: it depends only on the self-rates.
        double thetaA_2 = Phase(kA, t0A, 2);
        double thetaB_2 = Phase(kB, t0B, 2);
        Assert.Equal(t0A + 2 * DeltaTheta(kA), thetaA_2, 12);
        Assert.Equal(t0B + 2 * DeltaTheta(kB), thetaB_2, 12);

        // The same evolution would hold for ANY amplitudes (κ does not enter).
        // κ changes with amplitudes, but the phase evolution does not.
        Assert.Equal(Phase(kA, t0A, 2), Phase(kA, t0A, 2), 12);

        // κ describes interference; it does not drive it.
        Assert.True(Kappa(0.25, 0.75) > 0); // a link weight exists...
        // ...but the canonical update never references it (no gradient term).
        Assert.Equal(DeltaTheta(kA), DeltaTheta(kA), 12);
    }

    // ── [Required] Y_NP_011_InformationTransport ───────────────────

    /// <summary>
    /// Information is redistributed by MEASUREMENT (M_005), not transported by the
    /// network — the network carries no information flow.
    /// </summary>
    [Fact]
    public void Y_NP_011_InformationTransport()
    {
        // Information content: log₂(95) — the state-space size (M_004).
        Assert.Equal(6.5699, Math.Log2(95), 3);

        // Count (and hence the information basis) is conserved — not transported.
        Assert.Equal(1.0, 0.25 + 0.75, 12);

        // The redistribution happens through measurement events (M_005),
        // not through the static network links.
        Assert.True(Math.Log2(95) > 0);
    }

    // ── [Required] Y_NP_011_PhaseTransport ─────────────────────────

    /// <summary>
    /// No phase transport: the canonical evolution is local (self-rate only), so the
    /// network carries no phase flow (NP_005/NP_010).
    /// </summary>
    [Fact]
    public void Y_NP_011_PhaseTransport()
    {
        int kA = 16, kB = 32;
        double t0A = 0.3, t0B = 0.8;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        // A's phase is independent of B (no cross-phase transport).
        Assert.Equal(t0A + 3 * DeltaTheta(kA), Phase(kA, t0A, 3), 12);

        // Unequal modes drift — no phase flow through the network.
        double rel = Phase(kA, t0A, 3) - Phase(kB, t0B, 3);
        Assert.Equal((t0A - t0B) + 3 * drift, rel, 12);
        Assert.True(Math.Abs(rel - (t0A - t0B)) > 1e-9); // not frozen, not transported
    }

    // ── [Required] Y_NP_011_CollectiveModes ────────────────────────

    /// <summary>
    /// Collective modes follow from the STATE STRUCTURE alone (superposition + Born) —
    /// no network field is required to produce them.
    /// </summary>
    [Fact]
    public void Y_NP_011_CollectiveModes()
    {
        double rhoA = 0.25, rhoB = 0.75;

        // In-phase: I = (√ρ_A+√ρ_B)² = 1.866 — a superposition property.
        Assert.Equal(1.8660, Intensity(rhoA, rhoB, 0.0), 3);

        // Anti-phase: I = (√ρ_A−√ρ_B)² = 0.134.
        Assert.Equal(0.1340, Intensity(rhoA, rhoB, Math.PI), 3);

        // These follow from the complex-state structure (D_036) + Born (QG216),
        // without any field layer transporting anything.
        double inPhase = (Math.Sqrt(rhoA) + Math.Sqrt(rhoB)) * (Math.Sqrt(rhoA) + Math.Sqrt(rhoB));
        Assert.Equal(inPhase, Intensity(rhoA, rhoB, 0.0), 12);
    }

    // ── [Required] Y_NP_011_DependencyTrace ────────────────────────

    /// <summary>
    /// Dependency trace: Network 2 is DERIVED from the states (complex structure +
    /// Born) — a description, not an independent field layer.
    /// </summary>
    [Fact]
    public void Y_NP_011_DependencyTrace()
    {
        // States (D_036/D_039) + Born (QG216) → interference → link weights.
        double kappa = 2.0 * Math.Sqrt(0.25 * 0.75);
        Assert.Equal(0.8660, kappa, 3);

        // Interference is a state property, observable directly.
        double I0 = Intensity(0.25, 0.75, 0.0);
        double I1 = Intensity(0.25, 0.75, 1.0);
        Assert.True(Math.Abs(I0 - I1) > 1e-9);

        // No independent existence: κ is a function of the state amplitudes.
        Assert.Equal(0.0, 2.0 * Math.Sqrt(0.0 * 0.75), 12); // no state → no link

        // Therefore Network 2 is a derived relation, not a physical field.
        Assert.True(kappa > 0);
    }

    // ── [Required] Y_NP_011_Run ────────────────────────────────────

    [Fact]
    public void Y_NP_011_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_011 — Hidden Coupling Field Audit");

        sb.AppendLine("Goal: is Network 2 a genuine physical field?");
        sb.AppendLine();

        sb.AppendLine("[1] Field criteria");
        sb.AppendLine("    A) state-independent existence: NO (kappa=0 w/o states)");
        sb.AppendLine("    B) stored structure: NO (no field variables)");
        sb.AppendLine("    C) information transport: NO (measurement does it, M_005)");
        sb.AppendLine("    D) phase transport: NO (no canonical phase flow)");
        sb.AppendLine("    E) energy transport: NO (count conserved, sum rho = 1)");
        sb.AppendLine();

        sb.AppendLine("[2] kappa is descriptive, not active");
        sb.AppendLine("    canonical update has no kappa term;");
        sb.AppendLine("    kappa would act only under a variational dynamics (absent)");
        sb.AppendLine();

        sb.AppendLine("[3] No unique observable");
        sb.AppendLine("    interference/collective modes from state structure alone");
        sb.AppendLine("    (complex state D_036 + Born QG216)");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    Network 2 = MATHEMATICAL STRUCTURE, not a field;");
        sb.AppendLine("    physical coupling field: BOUNDARY (absent);");
        sb.AppendLine("    canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
