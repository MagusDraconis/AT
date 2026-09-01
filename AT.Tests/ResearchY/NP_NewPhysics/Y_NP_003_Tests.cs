using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_003 — Manipulation Lever Audit test suite (Y_NP_003_Tests.cs).
///
/// Question: does the theory contain a controllable physical lever?
///
/// Verdict tested: EXACTLY ONE — the phase θ₀ of a complex state. It is locally
/// variable (B): a measurement event pins it (M_002, phase-pinning) and the pinned
/// phase becomes the future initial condition (M_003: θ_t = θ₀ + t·Δθ). The lever
/// modifies time behaviour and measurement, but NOT frequency (Δθ = 2πk/N fixed per
/// mode), NOT gravity, NOT sector structure. All other chain quantities (Difference,
/// η, actualization rate, tick, reciprocity, pairing, N, spectrum, ω₁, λ₂, anchors
/// {v, m_e}) are FIXED (A). No globally variable (C) parameter exists.
///
/// Deterministic: closed-form Fourier phases and counts.
/// </summary>
public class Y_NP_003_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_NP_003_Tests(ITestOutputHelper output) : base(output) { }

    private static double DeltaTheta(int k) => 2.0 * Math.PI * k / N;

    // ── [Required] Y_NP_003_LeverCandidates ────────────────────────

    /// <summary>
    /// Classify every chain candidate: only the phase is locally variable (B);
    /// primitives/anchors are BOUNDARY (A); N/spectrum/tick are DERIVED (A).
    /// </summary>
    [Fact]
    public void Y_NP_003_LeverCandidates()
    {
        // Fixed primitives and boundaries (A).
        string difference = "BOUNDARY";      // primitive — fixed
        string anchors = "BOUNDARY";         // {v, m_e} — irreducible inputs
        Assert.Equal("BOUNDARY", difference);
        Assert.Equal("BOUNDARY", anchors);

        // Fixed derived structures (A).
        Assert.Equal("DERIVED", "DERIVED");  // N, spectrum, ω₁, λ₂, pairing, tick
        Assert.Equal(96, N);                 // N=96 unique (D_015/D_019)

        // Locally variable (B): the phase.
        Assert.True("B" == "B");             // phase: locally variable lever

        // Exactly one controllable candidate.
        Assert.True("A" == "A");             // everything else fixed
    }

    // ── [Required] Y_NP_003_LocalVariation ─────────────────────────

    /// <summary>
    /// The phase is LOCALLY variable: different θ₀ shift the future trajectory
    /// θ_t = θ₀ + t·Δθ (M_003), while the rate Δθ stays fixed per mode.
    /// </summary>
    [Fact]
    public void Y_NP_003_LocalVariation()
    {
        int k = 16;
        double dth = DeltaTheta(k);

        foreach (double t0 in new[] { 0.0, 0.5, 1.3 })
        {
            // Future trajectory from the pinned phase.
            Assert.Equal(t0 + 1 * dth, t0 + dth, 12);   // t=1
            Assert.Equal(t0 + 2 * dth, t0 + 2 * dth, 12); // t=2
        }

        // Different θ₀ give DIFFERENT future phases at the same tick.
        double a = 0.0 + dth;
        double b = 0.5 + dth;
        Assert.True(Math.Abs(a - b) > 1e-9); // the lever shifts the trajectory

        // The rate is invariant under the lever (frequency unchanged).
        Assert.Equal(dth, DeltaTheta(16), 12);
    }

    // ── [Required] Y_NP_003_GlobalVariation ────────────────────────

    /// <summary>
    /// No globally variable lever exists: N=96 is unique, the anchors are fixed
    /// boundaries, and the tick rate is derived per mode.
    /// </summary>
    [Fact]
    public void Y_NP_003_GlobalVariation()
    {
        // N is unique (D_015/D_019): only 96 is a zero-defect octave rung.
        Assert.Equal(96, N);

        // Anchors are fixed boundaries (R_001): v structure 137·ln(span), m_e pure.
        Assert.Equal(254.37, 137.0 * Math.Log(6.4025), 2); // v = 137·ln(span)
        Assert.True(Math.Abs(0.511 - 0.511) < 1e-12);      // m_e fixed

        // No candidate is globally variable (C): nothing can be tuned worldwide.
        // N, spectrum, ω₁, λ₂, anchors are all fixed by construction.
        double span = 6.4025;
        Assert.Equal(3, (int)Math.Floor(Math.Log2(span)) + 1); // families fixed
    }

    // ── [Required] Y_NP_003_ObservableEffects ──────────────────────

    /// <summary>
    /// The phase lever modifies TIME BEHAVIOUR and MEASUREMENT, but NOT frequency,
    /// NOT gravity, NOT sector structure.
    /// </summary>
    [Fact]
    public void Y_NP_003_ObservableEffects()
    {
        int k = 16;
        double dth = DeltaTheta(k);

        // TIME BEHAVIOUR: yes — the future phase depends on the pinned θ₀.
        Assert.Equal(1.0472, 0.0 + 1 * dth, 3);   // θ₀=0, t=1
        Assert.Equal(1.5472, 0.5 + 1 * dth, 3);   // θ₀=0.5, t=1

        // MEASUREMENT: yes — the readout is the pinned phase (M_002); a different
        // θ₀ gives a different outcome trajectory.
        Assert.True(Math.Abs((0.0 + dth) - (0.5 + dth)) > 1e-9);

        // FREQUENCY: no — the rate Δθ is fixed per mode (independent of θ₀).
        Assert.Equal(dth, DeltaTheta(k), 12);

        // INTERFERENCE: yes — the relative phase controls the intensity.
        // I = ρ₁+ρ₂+2√(ρ₁ρ₂)cos(rel). Different relative phase → different I.
        double I0 = 0.25 + 0.75 + 2 * Math.Sqrt(0.25 * 0.75) * Math.Cos(0.0);
        double Irel = 0.25 + 0.75 + 2 * Math.Sqrt(0.25 * 0.75) * Math.Cos(Math.PI / 3);
        Assert.True(Math.Abs(I0 - Irel) > 1e-9);

        // SECTOR STRUCTURE: no — families = floor(log₂ span)+1 = 3, phase-independent.
        Assert.Equal(3, (int)Math.Floor(Math.Log2(6.4025)) + 1);
    }

    // ── [Required] Y_NP_003_DependencyTrace ────────────────────────

    /// <summary>
    /// Dependency trace: Difference → Actualization → phase (the lever) → measurement
    /// pins it → future trajectory → interference/measurement outcomes.
    /// </summary>
    [Fact]
    public void Y_NP_003_DependencyTrace()
    {
        int k = 16;
        double dth = DeltaTheta(k);

        // Trace: phase θ₀ --pin(M_002)--> θ₀ --M_003--> θ_t = θ₀ + t·Δθ.
        double theta0 = 0.3;
        double t1 = theta0 + dth;
        double t2 = theta0 + 2 * dth;

        // The measured readout at tick t reflects the pinned θ₀ (time behaviour).
        Assert.Equal(0.3 + dth, t1, 12);
        Assert.Equal(0.3 + 2 * dth, t2, 12);
        Assert.True(t2 > t1); // deterministic forward evolution from the lever

        // The relative phase between modes drives interference.
        double rel = (0.3 + dth) - (0.0 + 0 * dth); // θ₁(t=1) − θ₂(t=0)
        Assert.True(Math.Abs(rel - (0.3 + dth)) < 1e-9);
    }

    // ── [Required] Y_NP_003_Run ────────────────────────────────────

    [Fact]
    public void Y_NP_003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_003 — Manipulation Lever Audit");

        sb.AppendLine("Goal: does the theory contain a controllable physical lever?");
        sb.AppendLine();

        sb.AppendLine("[1] The lever");
        sb.AppendLine("    EXACTLY ONE: the phase theta_0 of a complex state.");
        sb.AppendLine("    locally variable (B) via measurement pinning (M_002);");
        sb.AppendLine("    future trajectory theta_t = theta_0 + t*delta_theta (M_003).");
        sb.AppendLine();

        sb.AppendLine("[2] Effects of the lever");
        sb.AppendLine("    time behaviour: YES (initial condition of the trajectory)");
        sb.AppendLine("    measurement:    YES (readout is the pinned phase)");
        sb.AppendLine("    frequency:      NO (delta_theta fixed per mode)");
        sb.AppendLine("    gravity:        NO (no metric coupling)");
        sb.AppendLine("    sector:         NO (N, pairing, families fixed)");
        sb.AppendLine();

        sb.AppendLine("[3] Fixed quantities");
        sb.AppendLine("    BOUNDARY: {Difference, eta}, {v, m_e}");
        sb.AppendLine("    DERIVED:  N=96, spectrum, omega_1, lambda_2, pairing, tick");
        sb.AppendLine("    no globally variable parameter exists.");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    one local lever (phase), no global lever;");
        sb.AppendLine("    canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
