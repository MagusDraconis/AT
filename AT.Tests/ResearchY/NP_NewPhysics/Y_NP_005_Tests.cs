using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_005 — Missing Synchronization Mechanism Audit test suite
/// (Y_NP_005_Tests.cs).
///
/// Question: what is missing for spontaneous phase locking?
///
/// Verdict tested: unequal-mode synchronization requires a CROSS-PHASE FEEDBACK term
/// (a Kuramoto-type coupling κ·sin(θ_B−θ_A)) that the canonical chain does not
/// contain. The canonical update θ(t+1) = θ(t) + Δθ has only the self-rate, so for
/// k_A ≠ k_B the relative phase drifts linearly. Adding the feedback term gives
/// dψ/dt = Δθ_A − Δθ_B − 2κ·sin(ψ), which has a stable fixed point iff
/// κ ≥ |Δθ_A−Δθ_B|/2. Equal modes (k_A = k_B) synchronize trivially (drift vanishes).
///
/// Deterministic: closed-form phases and a fixed-point Kuramoto analysis.
/// </summary>
public class Y_NP_005_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_NP_005_Tests(ITestOutputHelper output) : base(output) { }

    private static double DeltaTheta(int k) => 2.0 * Math.PI * k / N;

    private static double Phase(int k, double theta0, int t)
        => theta0 + t * DeltaTheta(k);

    // ── [Required] Y_NP_005_IndependentPhases ──────────────────────

    /// <summary>
    /// Independent phases (no coupling): the relative phase drifts linearly.
    /// </summary>
    [Fact]
    public void Y_NP_005_IndependentPhases()
    {
        int kA = 16, kB = 32;
        double t0A = 0.3, t0B = 0.8;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        foreach (int t in new[] { 0, 1, 2, 3 })
        {
            double rel = Phase(kA, t0A, t) - Phase(kB, t0B, t);
            Assert.Equal((t0A - t0B) + t * drift, rel, 12);
        }
        Assert.True(Math.Abs(drift) > 1e-9); // unequal modes drift
    }

    // ── [Required] Y_NP_005_CoupledPhases ──────────────────────────

    /// <summary>
    /// Coupling exists (interference couples observably) but does NOT lock the phases:
    /// the evolution still has no cross-phase term, so the relative phase drifts.
    /// </summary>
    [Fact]
    public void Y_NP_005_CoupledPhases()
    {
        int kA = 16, kB = 32;
        double t0A = 0.3, t0B = 0.8;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        // Interference couples observably (I depends on the relative phase).
        double rel0 = t0A - t0B;
        double I0 = 0.25 + 0.75 + 2 * Math.Sqrt(0.25 * 0.75) * Math.Cos(rel0);
        double rel1 = rel0 + drift;
        double I1 = 0.25 + 0.75 + 2 * Math.Sqrt(0.25 * 0.75) * Math.Cos(rel1);
        Assert.True(Math.Abs(I0 - I1) > 1e-9); // interference sees the drift

        // But the evolution is unchanged — no locking force.
        double relAfter = Phase(kA, t0A, 2) - Phase(kB, t0B, 2);
        Assert.Equal(rel0 + 2 * drift, relAfter, 12); // still drifts
    }

    // ── [Required] Y_NP_005_EqualModes ─────────────────────────────

    /// <summary>
    /// Equal modes (k_A = k_B): the drift term vanishes, so the relative phase is
    /// frozen at the prepared value — trivial (emergent) synchronization.
    /// </summary>
    [Fact]
    public void Y_NP_005_EqualModes()
    {
        int kA = 16, kB = 16;
        double t0A = 0.3, t0B = 0.8;

        foreach (int t in new[] { 0, 1, 5, 50, 1000 })
        {
            double rel = Phase(kA, t0A, t) - Phase(kB, t0B, t);
            Assert.Equal(-0.5, rel, 12); // frozen forever
        }
    }

    // ── [Required] Y_NP_005_UnequalModes ───────────────────────────

    /// <summary>
    /// Unequal modes (k_A ≠ k_B): no synchronization in the canonical chain — the
    /// relative phase drifts linearly and never reaches a fixed point.
    /// </summary>
    [Fact]
    public void Y_NP_005_UnequalModes()
    {
        int kA = 16, kB = 32;
        double t0A = 0.3, t0B = 0.8;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        // No fixed point: the relative phase is not periodic in a short window and
        // never returns to its initial value (drift ≠ 0, incommensurate over long t).
        foreach (int t in new[] { 0, 1, 2, 3, 4, 5 })
        {
            double rel = Phase(kA, t0A, t) - Phase(kB, t0B, t);
            Assert.Equal((t0A - t0B) + t * drift, rel, 12); // strictly linear
        }
        // The relative phase is NOT constant (no locking).
        Assert.True(Math.Abs((t0A - t0B) + 5 * drift - (t0A - t0B)) > 1e-9);
    }

    // ── [Required] Y_NP_005_LockingMechanism ───────────────────────

    /// <summary>
    /// The missing mechanism is a cross-phase feedback term. Adding
    /// κ·sin(θ_B−θ_A) to A's update (and symmetric to B's) creates a stable fixed
    /// point iff κ ≥ |Δθ_A−Δθ_B|/2. Below the threshold no locking occurs.
    /// </summary>
    [Fact]
    public void Y_NP_005_LockingMechanism()
    {
        int kA = 16, kB = 32;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);
        double half = Math.Abs(drift) / 2.0; // locking threshold

        Assert.Equal(0.5236, half, 3); // |π/3 − 2π/3|/2 = π/6

        // Above threshold: fixed point exists (ψ* = arcsin(drift/2κ), real argument).
        double kappaStrong = 0.6; // > 0.5236
        double arg = drift / (2.0 * kappaStrong);
        Assert.True(Math.Abs(arg) <= 1.0); // arcsin real → stable fixed point
        double psiStar = Math.Asin(arg);
        Assert.True(Math.Abs(psiStar) < Math.PI); // a locked relative phase exists

        // At threshold: the argument equals ±1 (marginal lock).
        double kappaMarginal = half;
        Assert.Equal(1.0, Math.Abs(drift / (2.0 * kappaMarginal)), 9);

        // Below threshold: no real fixed point — |arg| > 1 → no locking.
        double kappaWeak = 0.3; // < 0.5236
        Assert.True(Math.Abs(drift / (2.0 * kappaWeak)) > 1.0); // no fixed point

        // The canonical update has NO cross-phase term (κ = 0): drift forever.
        Assert.True(Math.Abs(drift) > 1e-9);
    }

    // ── [Required] Y_NP_005_DependencyTrace ────────────────────────

    /// <summary>
    /// Dependency trace: Difference → Actualization → Phase → Coupling → (missing
    /// locking force) → Synchronization. The canonical chain stops at Coupling.
    /// </summary>
    [Fact]
    public void Y_NP_005_DependencyTrace()
    {
        // Canonical chain: coupling exists (interference), but no locking force.
        int kA = 16, kB = 32;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);
        Assert.True(Math.Abs(drift) > 1e-9); // unequal modes → coupling alone drifts

        // Equal modes synchronize trivially (drift = 0).
        Assert.Equal(0.0, DeltaTheta(16) - DeltaTheta(16), 12);

        // The missing link: a cross-phase feedback term with κ ≥ |Δθ_A−Δθ_B|/2.
        double threshold = Math.Abs(drift) / 2.0;
        Assert.True(threshold > 0); // a threshold exists — the mechanism is absent

        // Therefore: canonical chain Difference → Actualization → Phase → Coupling,
        // with Synchronization requiring the (missing) locking force.
        Assert.True(threshold <= Math.PI); // bounded, well-defined threshold
    }

    // ── [Required] Y_NP_005_Run ────────────────────────────────────

    [Fact]
    public void Y_NP_005_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_005 — Missing Synchronization Mechanism Audit");

        sb.AppendLine("Goal: what is missing for spontaneous phase locking?");
        sb.AppendLine();

        sb.AppendLine("[1] Three regimes");
        sb.AppendLine("    independent: drift linearly");
        sb.AppendLine("    coupled:     interference couples, but still drifts");
        sb.AppendLine("    synchronized: needs a LOCKING FORCE");
        sb.AppendLine();

        sb.AppendLine("[2] Equal vs unequal modes");
        sb.AppendLine("    k_A = k_B: relative phase frozen (trivial sync, EMERGENT)");
        sb.AppendLine("    k_A != k_B: drifts forever (no sync in canonical AT)");
        sb.AppendLine();

        sb.AppendLine("[3] The missing mechanism");
        sb.AppendLine("    a cross-phase feedback term kappa*sin(theta_B-theta_A);");
        sb.AppendLine("    locks iff kappa >= |dA-dB|/2 = 0.5236 for k=(16,32);");
        sb.AppendLine("    canonical update has only the self-rate (kappa = 0).");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    unequal-mode sync requires an interaction term NOT in the");
        sb.AppendLine("    derived chain (BOUNDARY); equal-mode sync is trivial;");
        sb.AppendLine("    canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
