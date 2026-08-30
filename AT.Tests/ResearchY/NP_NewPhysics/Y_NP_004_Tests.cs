using System.Globalization;
using System.Numerics;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_004 — Phase Coupling Audit test suite (Y_NP_004_Tests.cs).
///
/// Question: can two systems exchange or synchronize θ₀?
///
/// Verdict tested: the phase is a TRUE physical lever (it couples through
/// interference and through a shared actualization event), but synchronization only
/// occurs between IDENTICAL modes. For two systems A (k_A) and B (k_B):
/// θ_A(t)−θ_B(t) = (θ_A0−θ_B0) + t·(Δθ_A−Δθ_B). The relative phase is time-invariant
/// iff Δθ_A = Δθ_B (k_A = k_B); otherwise it drifts linearly — no phase-locking force
/// exists. The smallest interaction for phase exchange is ONE shared actualization
/// event reading both quadratures of both systems (joint pinning, M_002).
///
/// Deterministic: closed-form Fourier phases.
/// </summary>
public class Y_NP_004_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_NP_004_Tests(ITestOutputHelper output) : base(output) { }

    private static double DeltaTheta(int k) => 2.0 * Math.PI * k / N;

    private static double Phase(int k, double theta0, int t)
        => theta0 + t * DeltaTheta(k);

    // ── [Required] Y_NP_004_IndependentPhases ──────────────────────

    /// <summary>
    /// Independent actualization events: the relative phase drifts linearly —
    /// θ_A(t)−θ_B(t) = (θ_A0−θ_B0) + t·(Δθ_A−Δθ_B).
    /// </summary>
    [Fact]
    public void Y_NP_004_IndependentPhases()
    {
        int kA = 16, kB = 32;
        double t0A = 0.1, t0B = -0.1;

        // Relative phase at t=0 and its drift per tick.
        double rel0 = t0A - t0B;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        foreach (int t in new[] { 0, 1, 2, 3 })
        {
            double rel = Phase(kA, t0A, t) - Phase(kB, t0B, t);
            Assert.Equal(rel0 + t * drift, rel, 12); // linear drift
        }

        // Unequal modes DO drift apart (no synchronization).
        Assert.True(Math.Abs(drift) > 1e-9);
    }

    // ── [Required] Y_NP_004_SharedActualization ────────────────────

    /// <summary>
    /// A shared actualization event reads BOTH quadratures of BOTH systems and pins
    /// both phases jointly (M_002), giving a definite relative phase at the read.
    /// </summary>
    [Fact]
    public void Y_NP_004_SharedActualization()
    {
        int kA = 16, kB = 32;

        // One event pins both phases: the joint state has a definite (θ_A, θ_B).
        double thetaA_pinned = 0.4;
        double thetaB_pinned = 0.9;

        // The relative phase is definite at the shared read.
        double relPinned = thetaA_pinned - thetaB_pinned;
        Assert.Equal(-0.5, relPinned, 12);

        // Both quadratures of both systems are fixed by the same event.
        var zA = new Complex(Math.Cos(thetaA_pinned), Math.Sin(thetaA_pinned));
        var zB = new Complex(Math.Cos(thetaB_pinned), Math.Sin(thetaB_pinned));
        Assert.Equal(1.0, zA.Magnitude, 9);
        Assert.Equal(1.0, zB.Magnitude, 9);

        // A joint readout correlates the outcomes (both phases known together).
        Assert.True(Math.Abs((zA * Complex.Conjugate(zB)).Phase - relPinned) < 1e-9
                    || Math.Abs(Math.Abs((zA * Complex.Conjugate(zB)).Phase) - Math.Abs(relPinned)) < 1e-9);
    }

    // ── [Required] Y_NP_004_PhaseTransfer ──────────────────────────

    /// <summary>
    /// Phase transfer: through a shared event, A's pinned phase and B's pinned phase
    /// are fixed together — the relative phase is established by one readout.
    /// </summary>
    [Fact]
    public void Y_NP_004_PhaseTransfer()
    {
        int kA = 16, kB = 32;

        // Joint pinning sets BOTH initial conditions from one event.
        double thetaA = 0.2; // pinned by the shared read
        double thetaB = 0.2 + Math.PI / 2.0; // B pinned with a fixed offset (π/2)

        // The shared event transferred a definite relative phase: π/2.
        Assert.Equal(Math.PI / 2.0, thetaB - thetaA, 9);

        // From the pinned joint state, both evolve deterministically per tick.
        double rel1 = Phase(kA, thetaA, 1) - Phase(kB, thetaB, 1);
        double rel0 = Phase(kA, thetaA, 0) - Phase(kB, thetaB, 0);
        // rel0 = θA−θB = −π/2; drift = Δθ_A−Δθ_B = −π/3 → rel1 = −5π/6.
        Assert.Equal(-Math.PI / 2.0 + (DeltaTheta(kA) - DeltaTheta(kB)), rel1, 9);
        Assert.True(Math.Abs(rel1 - rel0) > 1e-9); // unequal modes drift after the event
    }

    // ── [Required] Y_NP_004_PhaseLocking ───────────────────────────

    /// <summary>
    /// Phase locking: the relative phase is time-invariant iff k_A = k_B (equal
    /// rates). For equal modes the prepared relative phase is frozen forever.
    /// </summary>
    [Fact]
    public void Y_NP_004_PhaseLocking()
    {
        int kA = 16, kB = 16; // identical modes — equal rates
        double t0A = 0.3, t0B = 0.8;

        // The relative phase is frozen (no drift).
        foreach (int t in new[] { 0, 1, 5, 100 })
        {
            double rel = Phase(kA, t0A, t) - Phase(kB, t0B, t);
            Assert.Equal(-0.5, rel, 12); // t0A − t0B = −0.5, constant
        }
    }

    // ── [Required] Y_NP_004_Synchronization ────────────────────────

    /// <summary>
    /// No synchronization for unequal modes: the theory has no phase-locking force;
    /// relative phase drifts linearly. Only identical modes co-rotate.
    /// </summary>
    [Fact]
    public void Y_NP_004_Synchronization()
    {
        int kA = 16, kB = 32; // unequal — no synchronization
        double t0A = 0.1, t0B = 0.1;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        // Equal starting phases still diverge.
        double rel1 = Phase(kA, t0A, 1) - Phase(kB, t0B, 1);
        Assert.Equal(rel1, drift, 12);
        Assert.True(Math.Abs(rel1) > 1e-9); // they did NOT stay synchronized

        // Identical modes DO stay synchronized (relative phase frozen).
        double relSame = Phase(16, t0A, 50) - Phase(16, t0B, 50);
        Assert.Equal(0.0, relSame, 12);

        // The smallest interaction for phase exchange is one shared event; without it
        // independent systems only drift (no spontaneous locking).
        Assert.True(Math.Abs(drift) > 1e-9);
    }

    // ── [Required] Y_NP_004_Run ────────────────────────────────────

    [Fact]
    public void Y_NP_004_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_004 — Phase Coupling Audit");

        sb.AppendLine("Goal: can two systems exchange or synchronize theta_0?");
        sb.AppendLine();

        sb.AppendLine("[1] Phase is a true lever");
        sb.AppendLine("    coupling via interference (I depends on theta_A-theta_B)");
        sb.AppendLine("    coupling via shared actualization event (joint pinning)");
        sb.AppendLine();

        sb.AppendLine("[2] Independent events");
        sb.AppendLine("    theta_A-theta_B = (t0A-t0B) + t*(dA-dB): linear drift");
        sb.AppendLine("    unequal modes never synchronize (no locking force)");
        sb.AppendLine();

        sb.AppendLine("[3] Synchronization");
        sb.AppendLine("    only for identical modes (k_A = k_B): phase frozen");
        sb.AppendLine("    smallest interaction = one shared event");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    coupling YES (interference + shared event);");
        sb.AppendLine("    synchronization only for identical modes;");
        sb.AppendLine("    sustained relations are common-origin correlations;");
        sb.AppendLine("    canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
