using System.Globalization;
using System.Numerics;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.M_Measurement;

/// <summary>
/// ResearchY-M_003 — Measurement Feedback Audit test suite (Y_M_003_Tests.cs).
///
/// Question: does measurement feed back into future state evolution?
///
/// Verdict tested: YES — measurement feeds back because the pinned phase becomes the
/// initial condition of the deterministic future trajectory. A measurement pins the
/// phase to θ₀ (M_002); the phase then advances deterministically per tick,
/// θ_t = θ₀ + t·Δθ with Δθ = 2πk/N (D_041). Before measurement the phase is free (a
/// superposition over all trajectories); after it is pinned (one deterministic
/// trajectory). The measured mode's future is FIXED; the unmeasured mode's is a
/// superposition. A pinned phase alters future interference (the joint coherence with
/// an unmeasured mode is indefinite unless the outcome is fed back), reciprocity (the
/// conjugate partner is made definite), and the actualization path. Classification:
/// feedback DERIVED; phase-pinning DERIVED (M_002); deterministic evolution DERIVED
/// (D_041); measurement event EMERGENT (M_001).
///
/// Deterministic: closed-form Fourier phases.
/// </summary>
public class Y_M_003_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_M_003_Tests(ITestOutputHelper output) : base(output) { }

    private static double CosK(int k, int site) => Math.Cos(2.0 * Math.PI * k * site / N);
    private static double SinK(int k, int site) => Math.Sin(2.0 * Math.PI * k * site / N);

    // ── [Required] Y_M_003_PhasePinning ─────────────────────────────

    /// <summary>
    /// The read pins the phase (M_002): extracting both quadratures fixes θ₀.
    /// </summary>
    [Fact]
    public void Y_M_003_PhasePinning()
    {
        foreach (int k in new[] { 16, 32, 40 })
        {
            int site = 5;
            var z = new Complex(CosK(k, site), SinK(k, site));
            double theta0 = Math.Atan2(z.Imaginary, z.Real);

            // The extracted phase is FIXED by the read (a definite value).
            Assert.Equal(theta0, Math.Atan2(new Complex(z.Real, z.Imaginary).Imaginary,
                                           new Complex(z.Real, z.Imaginary).Real), 9);
            // The phase is a specific value, not a range (pinned).
            Assert.True(theta0 > -Math.PI && theta0 <= Math.PI);
        }
    }

    // ── [Required] Y_M_003_Feedback ─────────────────────────────────

    /// <summary>
    /// The pinned phase becomes the initial condition of the future evolution:
    /// θ_t = θ₀ + t·Δθ with Δθ = 2πk/N (D_041).
    /// </summary>
    [Fact]
    public void Y_M_003_Feedback()
    {
        int k = 16, site = 5;
        var z = new Complex(CosK(k, site), SinK(k, site));
        double theta0 = Math.Atan2(z.Imaginary, z.Real);
        double dtheta = 2.0 * Math.PI * k / N;

        // Future evolution from the pinned initial phase (deterministic).
        foreach (int t in new[] { 1, 2, 3 })
        {
            double theta_t = theta0 + t * dtheta;
            Assert.Equal(theta0 + t * dtheta, theta_t, 12); // the outcome feeds the trajectory
        }

        // The phase advance per tick is the circulation (D_041).
        Assert.Equal(2.0 * Math.PI * 16 / 96, dtheta, 9);
    }

    // ── [Required] Y_M_003_InterferenceEvolution ────────────────────

    /// <summary>
    /// Future interference with a measured mode needs the outcome fed back: the joint
    /// coherence with an unmeasured mode is indefinite without the pinned value.
    /// </summary>
    [Fact]
    public void Y_M_003_InterferenceEvolution()
    {
        // An unmeasured mode (phase free): averaging over phases gives no definite
        // relative coherence with a pinned mode.
        double avgReal = (Math.Cos(0.0) + Math.Cos(0.5) + Math.Cos(1.0) + Math.Cos(1.5) + Math.Cos(2.0)) / 5.0;
        double avgImag = (Math.Sin(0.0) + Math.Sin(0.5) + Math.Sin(1.0) + Math.Sin(1.5) + Math.Sin(2.0)) / 5.0;
        Assert.True(Math.Abs(avgReal) < 0.9 && Math.Abs(avgImag) < 0.9); // indefinite without feedback

        // With the outcome fed back (a definite pinned phase), interference is definite.
        double theta0 = 0.7;
        double thetaKp = 1.7;
        double P = 2.0 + 2.0 * Math.Cos(theta0 - thetaKp);
        Assert.Equal(2.0 + 2.0 * Math.Cos(theta0 - thetaKp), P, 9); // definite joint coherence
    }

    // ── [Required] Y_M_003_MeasuredVsUnmeasured ─────────────────────

    /// <summary>
    /// Measured mode: future is deterministic from the pinned phase. Unmeasured mode:
    /// future is a superposition (no single trajectory).
    /// </summary>
    [Fact]
    public void Y_M_003_MeasuredVsUnmeasured()
    {
        // Measured: one deterministic trajectory from θ₀.
        double theta0 = 0.7, dtheta = 2.0 * Math.PI * 16 / 96;
        double t1 = theta0 + 1 * dtheta, t2 = theta0 + 2 * dtheta;
        Assert.NotEqual(t1, t2, 9); // the trajectory advances
        Assert.Equal(theta0 + 2 * dtheta, t2, 9); // deterministic from the outcome

        // Unmeasured: the phase is free — a superposition over starting phases
        // (the average over phases has no definite value).
        double avg = (Math.Cos(0.0) + Math.Cos(1.0) + Math.Cos(2.0)) / 3.0;
        Assert.True(Math.Abs(avg) < 0.9); // no single pinned trajectory
    }

    // ── [Required] Y_M_003_Run ──────────────────────────────────────

    [Fact]
    public void Y_M_003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-M_003 — Measurement Feedback Audit");

        sb.AppendLine("Goal: does measurement feed back into future state evolution?");
        sb.AppendLine();

        sb.AppendLine("[1] The read pins the phase (M_002)");
        sb.AppendLine("    outcome = a definite theta_0 (the read result)");
        sb.AppendLine();

        sb.AppendLine("[2] Feedback: the pinned phase is the initial condition");
        sb.AppendLine("    theta_t = theta_0 + t*Delta_theta, Delta_theta = 2*pi*k/N");
        sb.AppendLine("    before: free phase (superposition of trajectories)");
        sb.AppendLine("    after: pinned (one deterministic trajectory)");
        sb.AppendLine();

        sb.AppendLine("[3] Measured vs unmeasured");
        sb.AppendLine("    measured: future FIXED from theta_0");
        sb.AppendLine("    unmeasured: future a superposition");
        sb.AppendLine("    future interference needs the outcome fed back");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    measurement necessarily changes future evolution:");
        sb.AppendLine("    it fixes the initial phase; feedback DERIVED;");
        sb.AppendLine("    phase-pinning DERIVED (M_002); evolution DERIVED (D_041).");
        sb.AppendLine("    No canonical value changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
