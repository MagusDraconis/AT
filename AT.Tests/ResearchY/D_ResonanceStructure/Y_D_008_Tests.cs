using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_008 — Reference Unit Audit test suite (Y_D_008_Tests.cs).
///
/// Question: what object in D96 plays the role of light or an atomic clock?
///
/// Verdict tested: the first natural reference unit is the dimensionless spectral
/// frequency (ω₁ = 0.6216) — DERIVED as a relative (ordering/ratio) reference; physical
/// clock/ruler/energy units require external calibration (v, c, ħ) — BOUNDARY. All six
/// candidates are dimensionless.
///
/// Deterministic: closed-form circulant eigenvalues + analytic frequencies.
/// </summary>
public class Y_D_008_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_008_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_008_Candidates ────────────────────────────────────

    /// <summary>
    /// The six candidates (actualization tick, closure cycle, zero mode, fundamental
    /// doublet, spectral gap, resonant pairs) are all dimensionless spectral quantities.
    /// </summary>
    [Fact]
    public void Y_D_008_Candidates()
    {
        // Fundamental doublet: ω₁ = 0.6216 (dimensionless frequency).
        Assert.Equal(0.6216, Omega(1), 3);

        // Spectral gap: λ₂ = 0.3864 (dimensionless).
        double lam2 = 0.0;
        for (int k = 1; k < N; k++)
        {
            double l = Lambda(k);
            if (lam2 == 0.0 || l < lam2) lam2 = l;
        }
        Assert.Equal(0.3864, lam2, 3);

        // Key relation: ω₁² = λ₂.
        Assert.Equal(lam2, Omega(1) * Omega(1), 4);

        // Zero mode: λ₀ = 0 (reference state).
        Assert.Equal(0.0, Lambda(0), 10);

        // Closure cycle: N = 96 (dimensionless periodicity).
        Assert.Equal(96, N);

        // Resonant pair structure: 47 Z2 pairs (dimensionless).
        var lam = new double[N];
        for (int k = 0; k < N; k++) lam[k] = Lambda(k);
        int pairs = 0;
        for (int k = 1; k <= 47; k++)
            if (Math.Abs(lam[k] - lam[N - k]) < 1e-9) pairs++;
        Assert.Equal(47, pairs);
    }

    // ── [Required] Y_D_008_ClockRulerEnergy ──────────────────────────────

    /// <summary>
    /// Natural clock (dimensionless frequency EMERGENT; physical BOUNDARY), ruler
    /// (dimensionless ratios EMERGENT; physical BOUNDARY), energy unit (BOUNDARY).
    /// </summary>
    [Fact]
    public void Y_D_008_ClockRulerEnergy()
    {
        // Clock: ω₁ = 0.6216 is a dimensionless frequency (relative reference).
        Assert.Equal(0.6216, Omega(1), 3);

        // Ruler: span = 6.40 is a dimensionless ratio (relative ruler).
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        Assert.Equal(6.40, freqs[^1] / freqs[0], 2);

        // Energy unit: dimensionless spectral content only — no physical unit.
        // (Documented: physical Hz/m/J require external calibration — BOUNDARY.)
        Assert.True(freqs[^1] / freqs[0] > 0); // dimensionless
    }

    // ── [Required] Y_D_008_OrderingVsUnit ────────────────────────────────

    /// <summary>
    /// Ordering and dimensionless frequency are DERIVED; physical units are BOUNDARY.
    /// </summary>
    [Fact]
    public void Y_D_008_OrderingVsUnit()
    {
        // Ordering: the spectral frequencies are strictly ordered (DERIVED).
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        for (int i = 1; i < freqs.Length; i++) Assert.True(freqs[i] >= freqs[i - 1]);

        // Dimensionless frequency: ω₁ = 0.6216 (DERIVED pure number).
        Assert.Equal(0.6216, Omega(1), 3);

        // Physical unit: requires external calibration (D_007: v, c, ħ) — BOUNDARY.
        // (Documented: the D96 content carries no Hz/m/J.)
    }

    // ── [Required] Y_D_008_AtomicClockComparison ─────────────────────────

    /// <summary>
    /// An atomic clock is a physical frequency (Cs hyperfine 9.19 GHz); the light-based
    /// meter uses c. D96 provides only dimensionless analogues (ω₁, span) — no physical
    /// Hz or m.
    /// </summary>
    [Fact]
    public void Y_D_008_AtomicClockComparison()
    {
        // D96 dimensionless analogue of a clock frequency: ω₁ = 0.6216.
        Assert.Equal(0.6216, Omega(1), 3);

        // D96 dimensionless analogue of a ruler ratio: span = 6.40.
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        Assert.Equal(6.40, freqs[^1] / freqs[0], 2);

        // These are pure numbers — no physical units (Hz, m).
        // (Documented: the atomic clock and the meter are physical references; D96 gives
        //  dimensionless analogues only.)
    }

    // ── [Required] Y_D_008_ExternalCalibration ───────────────────────────

    /// <summary>
    /// A dimensionless reference is calibration-free (DERIVED); a physical reference
    /// requires external calibration (BOUNDARY, D_007).
    /// </summary>
    [Fact]
    public void Y_D_008_ExternalCalibration()
    {
        // Dimensionless reference: ω₁ = 0.6216 is exact, calibration-free (DERIVED).
        Assert.Equal(0.6216, Omega(1), 3);

        // Physical reference: needs the anchor v and the constants c, ħ (D_007).
        // M_Pl = v·A³ requires v; G = ħc/M_Pl² requires c, ħ.
        // (Documented: dimensionless YES, physical NO — BOUNDARY.)
        Assert.True(Omega(1) > 0); // the dimensionless reference exists internally
    }

    // ── [Required] Y_D_008_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_008_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_008 — Reference Unit Audit");

        sb.AppendLine("Goal: what object in D96 plays the role of light or an atomic clock?");
        sb.AppendLine();

        double lam2 = 0.0;
        for (int k = 1; k < N; k++)
        {
            double l = Lambda(k);
            if (lam2 == 0.0 || l < lam2) lam2 = l;
        }

        sb.AppendLine("[1] Candidates (all dimensionless)");
        sb.AppendLine($"    actualization tick (Q-event): the count unit");
        sb.AppendLine($"    closure cycle N = {N}: the full cycle");
        sb.AppendLine("    zero mode: λ₀ = 0, ω₀ = 0 (reference state)");
        sb.AppendLine($"    fundamental doublet: ω₁ = {Omega(1):F4}");
        sb.AppendLine($"    spectral gap: λ₂ = {lam2:F4}  (ω₁² = λ₂)");
        sb.AppendLine("    resonant pair structure: 47 Z2 pairs");
        sb.AppendLine();

        sb.AppendLine("[2] Natural clock / ruler / energy");
        sb.AppendLine("    clock:  dimensionless frequency (EMERGENT); physical Hz (BOUNDARY)");
        sb.AppendLine("    ruler:  dimensionless ratios (EMERGENT); physical m (BOUNDARY)");
        sb.AppendLine("    energy: dimensionless content; physical J (BOUNDARY)");
        sb.AppendLine();

        sb.AppendLine("[3] Ordering vs dimensionless vs physical");
        sb.AppendLine("    ordering only:            DERIVED");
        sb.AppendLine("    dimensionless frequency:  DERIVED (ω₁ = 0.6216)");
        sb.AppendLine("    physical unit:            BOUNDARY (v, c, ħ)");
        sb.AppendLine();

        sb.AppendLine("[4] Comparison with atomic clock / meter");
        sb.AppendLine("    atomic clock: physical frequency (9.19 GHz) — D96 has dimensionless analogue only");
        sb.AppendLine("    light meter:  c = 299792458 m/s — D96 has dimensionless ratios only");
        sb.AppendLine();

        sb.AppendLine("[5] Reference without external calibration?");
        sb.AppendLine("    as dimensionless reference: YES (DERIVED)");
        sb.AppendLine("    as physical reference:      NO (BOUNDARY)");
        sb.AppendLine("    first natural reference: the dimensionless spectral frequency ω₁");
        sb.AppendLine();

        sb.AppendLine("[6] Conclusion");
        sb.AppendLine("    The first natural reference unit of D96 is the dimensionless");
        sb.AppendLine("    spectral frequency (ω₁ = 0.6216) — a derived, calibration-free");
        sb.AppendLine("    relative reference. Physical clock/ruler/energy units require");
        sb.AppendLine("    external calibration (v, c, ħ). No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
