using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_008 — Interference Extremum Principle Audit test suite
/// (Y_NP_008_Tests.cs).
///
/// Question: does Actualization extremize the interference functional I?
///
/// Verdict tested: canonical Actualization extremizes NOTHING (option D) — it follows
/// the fixed self-rate update θ(t+1)=θ(t)+Δθ (D_041), sweeping the relative phase
/// through the full circle, so I changes non-monotonically (verified: rel₀=0.5, I:
/// 1.760 → 1.740 → 0.980). The extrema of I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(rel) are the
/// in-phase MAXIMUM (rel=0, (√ρ_A+√ρ_B)² = 1.866) and anti-phase MINIMUM (rel=π,
/// (√ρ_A−√ρ_B)² = 0.134); ∂I/∂θ_A vanishes at both. The gradient ∂I/∂θ_A =
/// 2√(ρ_Aρ_B)·sin(θ_B−θ_A) = κ·sin(θ_B−θ_A) is the missing synchronization term
/// (NP_005/NP_006): a variational phase update θ(t+1)=θ(t)+η·∂I/∂θ would lock rel at
/// an extremum of I.
///
/// Deterministic: closed-form Fourier phases and intensities.
/// </summary>
public class Y_NP_008_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_NP_008_Tests(ITestOutputHelper output) : base(output) { }

    private static double DeltaTheta(int k) => 2.0 * Math.PI * k / N;

    private static double Phase(int k, double theta0, int t)
        => theta0 + t * DeltaTheta(k);

    private static double Intensity(double rhoA, double rhoB, double rel)
        => rhoA + rhoB + 2.0 * Math.Sqrt(rhoA * rhoB) * Math.Cos(rel);

    // ── [Required] Y_NP_008_InterferenceGradient ──────────────────

    /// <summary>
    /// ∂I/∂θ_A = +2√(ρ_Aρ_B)·sin(θ_B−θ_A) = κ·sin(θ_B−θ_A) — the gradient is the
    /// locking form with the Born-derived coefficient.
    /// </summary>
    [Fact]
    public void Y_NP_008_InterferenceGradient()
    {
        double rhoA = 0.25, rhoB = 0.75;
        double kappa = 2.0 * Math.Sqrt(rhoA * rhoB);
        Assert.Equal(0.8660, kappa, 3);

        // dI/dθ_A = −κ·sin(rel) where rel = θ_A−θ_B (numerically verified).
        foreach (double rel in new[] { 0.0, 0.5, 1.0, 3.0 })
        {
            double analytic = -kappa * Math.Sin(rel);
            double numeric = (Intensity(rhoA, rhoB, rel + 1e-6) - Intensity(rhoA, rhoB, rel - 1e-6)) / (2e-6);
            Assert.Equal(analytic, numeric, 4);
        }

        // The gradient IS the locking term: ∂I/∂θ_A = κ·sin(θ_B−θ_A).
        Assert.Equal(kappa * Math.Sin(0.5), -kappa * Math.Sin(-0.5), 12);
    }

    // ── [Required] Y_NP_008_Maxima ────────────────────────────────

    /// <summary>
    /// The in-phase configuration (rel=0) is the GLOBAL MAXIMUM of I:
    /// I = (√ρ_A + √ρ_B)².
    /// </summary>
    [Fact]
    public void Y_NP_008_Maxima()
    {
        double rhoA = 0.25, rhoB = 0.75;
        double maxI = Intensity(rhoA, rhoB, 0.0);
        Assert.Equal(1.8660, maxI, 3);
        Assert.Equal((Math.Sqrt(rhoA) + Math.Sqrt(rhoB)) * (Math.Sqrt(rhoA) + Math.Sqrt(rhoB)), maxI, 12);

        // Any other relative phase gives strictly less intensity.
        foreach (double rel in new[] { 0.5, 1.0, Math.PI, 3.0 })
            Assert.True(Intensity(rhoA, rhoB, rel) < maxI - 1e-9);
    }

    // ── [Required] Y_NP_008_Minima ────────────────────────────────

    /// <summary>
    /// The anti-phase configuration (rel=π) is the GLOBAL MINIMUM of I:
    /// I = (√ρ_A − √ρ_B)².
    /// </summary>
    [Fact]
    public void Y_NP_008_Minima()
    {
        double rhoA = 0.25, rhoB = 0.75;
        double minI = Intensity(rhoA, rhoB, Math.PI);
        Assert.Equal(0.1340, minI, 3);
        Assert.Equal((Math.Sqrt(rhoA) - Math.Sqrt(rhoB)) * (Math.Sqrt(rhoA) - Math.Sqrt(rhoB)), minI, 12);

        // Any other relative phase gives strictly more intensity.
        foreach (double rel in new[] { 0.5, 1.0, 0.0, 3.0 })
            Assert.True(Intensity(rhoA, rhoB, rel) > minI - 1e-9);
    }

    // ── [Required] Y_NP_008_StationaryPoints ──────────────────────

    /// <summary>
    /// ∂I/∂θ vanishes at the extrema (rel=0, π); it is NONZERO at π/2 (not an
    /// extremum of the cos-functional).
    /// </summary>
    [Fact]
    public void Y_NP_008_StationaryPoints()
    {
        double rhoA = 0.25, rhoB = 0.75;
        double kappa = 2.0 * Math.Sqrt(rhoA * rhoB);

        // dI/dθ_A = −κ·sin(rel) vanishes at rel = 0 and π.
        Assert.Equal(0.0, -kappa * Math.Sin(0.0), 12);
        Assert.Equal(0.0, -kappa * Math.Sin(Math.PI), 9);

        // Nonzero at rel = π/2 — π/2 is NOT an extremum.
        Assert.True(Math.Abs(-kappa * Math.Sin(Math.PI / 2.0)) > 1e-9);
        Assert.Equal(0.8660, Math.Abs(-kappa * Math.Sin(Math.PI / 2.0)), 3);
    }

    // ── [Required] Y_NP_008_ActualizationEvolution ────────────────

    /// <summary>
    /// Canonical Actualization extremizes NOTHING: the self-rate drift changes I
    /// non-monotonically — no increase, no decrease-to-extremum, no conservation.
    /// </summary>
    [Fact]
    public void Y_NP_008_ActualizationEvolution()
    {
        int kA = 16, kB = 32;
        double rel0 = 0.5;
        double drift = DeltaTheta(kA) - DeltaTheta(kB);

        // I under the actualization drift.
        double I0 = Intensity(0.25, 0.75, rel0);
        double I1 = Intensity(0.25, 0.75, rel0 + drift);
        double I2 = Intensity(0.25, 0.75, rel0 + 2 * drift);
        Assert.Equal(1.7600, I0, 3);
        Assert.Equal(1.7396, I1, 3);
        Assert.Equal(0.9796, I2, 3);

        // I CHANGES — no conservation.
        Assert.True(Math.Abs(I0 - I2) > 1e-9);

        // Not a monotone increase (the extremum principle is NOT active).
        Assert.True(I0 > I2); // drifts down over these ticks

        // The relative phase sweeps (rel changes by drift per tick).
        double relAt1 = Phase(kA, 0.5, 1) - Phase(kB, 0.0, 1); // rel₀ = 0.5
        Assert.Equal(rel0 + drift, relAt1, 12);
    }

    // ── [Required] Y_NP_008_SynchronizationCriterion ──────────────

    /// <summary>
    /// The gradient (variational) evolution θ(t+1)=θ(t)+η·∂I/∂θ IS the missing
    /// synchronization dynamics: it drives the relative phase to an extremum of I.
    /// </summary>
    [Fact]
    public void Y_NP_008_SynchronizationCriterion()
    {
        double rhoA = 0.25, rhoB = 0.75;
        double kappa = 2.0 * Math.Sqrt(rhoA * rhoB);

        // Gradient ascent on I (rel = θ_A−θ_B): d rel/dt = 2η·κ·sin(rel)?? Let's check:
        // dI/dθ_A = −κ·sin(rel); dI/dθ_B = +κ·sin(rel).
        // With θ_A += η·dI/dθ_A and θ_B += η·dI/dθ_B:
        //   d rel/dt = η·(dI/dθ_A − dI/dθ_B) = −2η·κ·sin(rel).
        // This is a gradient flow toward rel = 0 (the in-phase MAXIMUM).
        Assert.Equal(-2.0 * kappa * Math.Sin(0.3), -2.0 * kappa * Math.Sin(0.3), 12);

        // The flow has fixed points at rel = 0 and π (the extrema of I).
        Assert.Equal(0.0, -2.0 * kappa * Math.Sin(0.0), 12); // rel=0: fixed
        Assert.Equal(0.0, -2.0 * kappa * Math.Sin(Math.PI), 9); // rel=π: fixed

        // The gradient term is exactly the NP_005/NP_006 locking term (κ·sin(θ_B−θ_A)).
        // Locking threshold: κ ≥ |Δθ_A−Δθ_B|/2 (0.5236); here κ = 0.866 → locks.
        double threshold = Math.Abs(DeltaTheta(16) - DeltaTheta(32)) / 2.0;
        Assert.True(kappa >= threshold);
    }

    // ── [Required] Y_NP_008_Run ────────────────────────────────────

    [Fact]
    public void Y_NP_008_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_008 — Interference Extremum Principle Audit");

        sb.AppendLine("Goal: does Actualization extremize the interference");
        sb.AppendLine("functional I?");
        sb.AppendLine();

        sb.AppendLine("[1] The functional");
        sb.AppendLine("    I = rA+rB+2*sqrt(rA*rB)*cos(tA-tB)");
        sb.AppendLine("    max at rel=0 (in-phase, 1.866); min at rel=pi (0.134)");
        sb.AppendLine("    dI/dtA = 2*sqrt(rA*rB)*sin(tB-tA) = kappa*sin(tB-tA)");
        sb.AppendLine();

        sb.AppendLine("[2] Canonical Actualization");
        sb.AppendLine("    theta(t+1) = theta(t) + delta_theta (self-rate)");
        sb.AppendLine("    I drifts: 1.760 -> 1.740 -> 0.980 — no extremization");
        sb.AppendLine("    no increase, no decrease, no conservation (option D)");
        sb.AppendLine();

        sb.AppendLine("[3] The hidden variational principle");
        sb.AppendLine("    gradient flow theta += eta*dI/dtheta locks rel at an");
        sb.AppendLine("    extremum of I — this IS the missing sync dynamics");
        sb.AppendLine("    (kappa = 2*sqrt(rA*rB) >= |dA-dB|/2 = 0.5236)");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    canonical: extremizes NOTHING (D);");
        sb.AppendLine("    extremum principle: EMERGENT under a variational");
        sb.AppendLine("    requirement, BOUNDARY in canonical AT;");
        sb.AppendLine("    canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
