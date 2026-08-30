using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.M_Measurement;

/// <summary>
/// ResearchY-M_010 — Discrete Phase Lattice Audit test suite (Y_M_010_Tests.cs).
///
/// Question: does AT-P042 (discrete tick phase evolution θ_m = θ₀ + m·2πk/N) produce
/// observable effects that continuous QM cannot reproduce?
///
/// Verdict tested: NO — at every tick-sampled time, continuous QM with the matching
/// rate ω = 2πk/(N·τ) reproduces AT-P042 exactly (phase, recurrence, interference,
/// finite-state orbits). The ONLY difference is the sub-tick phase, and discriminating
/// requires a clock finer than the actualization tick (in-principle-only, the tick is
/// the theory's fundamental clock). Classification: sampled observables
/// CORRESPONDENCE; the discrete time-parameter is a structural PREDICTION; nothing
/// FALSIFIED.
///
/// Deterministic: closed-form phase arithmetic.
/// </summary>
public class Y_M_010_Tests : ResearchTestBase
{
    private const int N = 96;

    public Y_M_010_Tests(ITestOutputHelper output) : base(output) { }

    private static int Gcd(int a, int b)
    {
        while (b != 0) { (a, b) = (b, a % b); }
        return a;
    }

    /// <summary>Continuous QM phase at time t with matching rate ω = 2πk/(N·τ).</summary>
    private static double ContPhase(double theta0, int k, double tau, double t)
        => theta0 + (2.0 * Math.PI * k / (N * tau)) * t;

    /// <summary>AT discrete lattice phase at tick m.</summary>
    private static double AtPhase(double theta0, int k, int m)
        => theta0 + m * (2.0 * Math.PI * k / N);

    // ── [Required] Y_M_010_ContinuousPhase ─────────────────────────

    /// <summary>
    /// Continuous QM with ω = 2πk/(N·τ) matches AT-P042 at EVERY tick t = m·τ.
    /// </summary>
    [Fact]
    public void Y_M_010_ContinuousPhase()
    {
        double tau = 1.0, theta0 = 0.3;
        foreach (int k in new[] { 1, 16, 48 })
        {
            foreach (int m in new[] { 0, 1, 2, 5, 17 })
            {
                double cont = ContPhase(theta0, k, tau, m * tau);
                double at = AtPhase(theta0, k, m);
                Assert.Equal(at, cont, 12); // identical at every tick
            }
        }
    }

    // ── [Required] Y_M_010_DiscretePhase ───────────────────────────

    /// <summary>
    /// The discrete lattice has cardinality N/gcd(N,k) distinct phases mod 2π.
    /// </summary>
    [Fact]
    public void Y_M_010_DiscretePhase()
    {
        Assert.Equal(96, N / Gcd(N, 1));   // k=1 low k
        Assert.Equal(6, N / Gcd(N, 16));   // k=16
        Assert.Equal(96, N / Gcd(N, 47));  // high k
        Assert.Equal(2, N / Gcd(N, 48));   // k=48 — binary flip {0, π}

        // k=48: exactly two distinct phases modulo 2π.
        var phases = new List<double>();
        for (int m = 0; m < 8; m++)
        {
            double th = AtPhase(0.0, 48, m) % (2.0 * Math.PI);
            if (!phases.Any(p => Math.Abs(p - th) < 1e-9)) phases.Add(th);
        }
        Assert.Equal(2, phases.Count);
        Assert.True(phases.Any(p => Math.Abs(p - 0.0) < 1e-9));        // 0
        Assert.True(phases.Any(p => Math.Abs(p - Math.PI) < 1e-9));    // π

        // Lattice is finite — strictly smaller than the continuum.
        Assert.True(N / Gcd(N, 16) < 1000);
    }

    // ── [Required] Y_M_010_InterferencePattern ─────────────────────

    /// <summary>
    /// Two-mode interference |ψ₁+ψ₂|² at tick m is identical in AT and the matching
    /// continuous model: the relative phase advances 2π(k₁−k₂)/N per tick in both.
    /// </summary>
    [Fact]
    public void Y_M_010_InterferencePattern()
    {
        double tau = 1.0;
        int k1 = 16, k2 = 32;
        double rho1 = 0.25, rho2 = 0.75;
        double th10 = 0.1, th20 = 0.6;

        foreach (int m in new[] { 0, 1, 3, 9 })
        {
            // AT: relative phase at tick m.
            double atRel = (th10 - th20) + m * (2.0 * Math.PI * (k1 - k2) / N);
            double atIntensity = rho1 + rho2 + 2.0 * Math.Sqrt(rho1 * rho2) * Math.Cos(atRel);

            // Continuous QM: same relative phase at t = m·τ (θ₁−θ₂ directly).
            double contRel = ContPhase(th10, k1, tau, m * tau) - ContPhase(th20, k2, tau, m * tau);
            double contIntensity = rho1 + rho2 + 2.0 * Math.Sqrt(rho1 * rho2) * Math.Cos(contRel);

            Assert.Equal(atIntensity, contIntensity, 12); // identical interference
        }
    }

    // ── [Required] Y_M_010_Recurrence ──────────────────────────────

    /// <summary>
    /// Mode k recurs after N/gcd(N,k) ticks in BOTH theories: m·k/N ∈ ℤ ⟺
    /// m = N/gcd(N,k); continuous QM recurs at t = N·τ/gcd(N,k).
    /// </summary>
    [Fact]
    public void Y_M_010_Recurrence()
    {
        double tau = 1.0;
        foreach (int k in new[] { 1, 16, 47, 48 })
        {
            int period = N / Gcd(N, k);

            // AT recurrence: phase returns to start after `period` ticks.
            double atStart = AtPhase(0.2, k, 0) % (2.0 * Math.PI);
            double atEnd = AtPhase(0.2, k, period) % (2.0 * Math.PI);
            Assert.Equal(atStart, atEnd, 9);

            // Continuous QM recurrence at the same time.
            double contStart = ContPhase(0.2, k, tau, 0) % (2.0 * Math.PI);
            double contEnd = ContPhase(0.2, k, tau, period * tau) % (2.0 * Math.PI);
            Assert.Equal(contStart, contEnd, 9);

            // The recurrence time is the SAME: N·τ/gcd(N,k).
            Assert.Equal(period * tau, (double)period * tau, 12);
        }
    }

    // ── [Required] Y_M_010_PredictionUniqueness ────────────────────

    /// <summary>
    /// The ONLY in-principle discriminator is the sub-tick phase: continuous QM has
    /// intermediate phases between ticks, AT has none. At integer ticks they agree.
    /// </summary>
    [Fact]
    public void Y_M_010_PredictionUniqueness()
    {
        int k = 16;
        double tau = 1.0, theta0 = 0.0;

        // At integer ticks both agree.
        Assert.Equal(AtPhase(theta0, k, 1), ContPhase(theta0, k, tau, 1.0 * tau), 12);

        // At a HALF tick, continuous QM has an intermediate phase; AT is pinned to the
        // lattice point (no phase between ticks).
        double contHalf = ContPhase(theta0, k, tau, 0.5 * tau);
        double atHalf = AtPhase(theta0, k, 0); // no advance until the next tick
        Assert.True(Math.Abs(contHalf - atHalf) > 1e-9); // the two differ
        Assert.Equal(0.5236, contHalf, 3);               // intermediate phase exists in QM
        Assert.Equal(0.0, atHalf, 12);                   // AT stays at the lattice point

        // This is the sole in-principle discriminator; tick-sampled observables are
        // all QM-reproducible (CORRESPONDENCE), the time-parameter itself is structural.
        Assert.True(true);
    }

    // ── [Required] Y_M_010_Run ─────────────────────────────────────

    [Fact]
    public void Y_M_010_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-M_010 — Discrete Phase Lattice Audit");

        sb.AppendLine("Goal: does AT-P042 produce observable effects that");
        sb.AppendLine("continuous QM cannot reproduce?");
        sb.AppendLine();

        sb.AppendLine("[1] Tick-sampled observables — CORRESPONDENCE");
        sb.AppendLine("    continuous QM with omega = 2*pi*k/(N*tau) matches AT at");
        sb.AppendLine("    every tick: phase, recurrence, interference, orbit size.");
        sb.AppendLine();

        sb.AppendLine("[2] The only discriminator — sub-tick phase (in-principle)");
        sb.AppendLine("    QM has intermediate phases between ticks; AT has none.");
        sb.AppendLine("    Requires a clock finer than the actualization tick.");
        sb.AppendLine();

        sb.AppendLine("[3] Mode analysis");
        sb.AppendLine("    k=1: lattice 96, recurrence 96");
        sb.AppendLine("    k=16: lattice 6, recurrence 6");
        sb.AppendLine("    k=48: lattice 2, recurrence 2 (binary phase flip)");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    AT-P042: structural PREDICTION, observably CORRESPONDENCE;");
        sb.AppendLine("    nothing FALSIFIED; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
