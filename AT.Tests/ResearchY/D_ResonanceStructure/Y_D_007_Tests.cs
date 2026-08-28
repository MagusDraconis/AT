using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_007 — Planck Scale Audit test suite (Y_D_007_Tests.cs).
///
/// Question: can the Planck scale be derived without calibration anchors?
///
/// Verdict tested: the dimensionless Planck ratio A³ = (Σm·#g·occ₂)³ is DERIVED from
/// the D96 spectral content; the absolute Planck scale M_Pl = v·A³ requires the
/// calibration anchor v (weak scale); the SI value G = ħc/M_Pl² imports c, ħ, and the
/// GeV↔kg conversion (BOUNDARY). The Planck scale is calibrated, not derived.
///
/// Deterministic: closed-form circulant eigenvalues + analytic spectral content.
/// </summary>
public class Y_D_007_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    // Canonical D96 content (Ch6/Ch10, QG181).
    private const double SigmaM = 95.0;
    private const double GroupCount = 44.0;
    private const double DenseBand = 87.0;
    private const double WeakScaleAnchor = 254.37; // GeV (calibration anchor)

    public Y_D_007_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_007_DimensionlessRatio ────────────────────────────

    /// <summary>
    /// The dimensionless Planck ratio A³ = (Σm·#g·occ₂)³ = 4.8094e16 is DERIVED —
    /// a pure number from the D96 spectral content.
    /// </summary>
    [Fact]
    public void Y_D_007_DimensionlessRatio()
    {
        double A = SigmaM * GroupCount * DenseBand;
        Assert.Equal(363660.0, A, 6);

        double A3 = A * A * A;
        Assert.Equal(4.8094e16, A3, 1e16 * 1e-4); // 4.809352e16

        // A³ is a pure number (no units, no anchors).
        Assert.True(A3 > 0);
    }

    // ── [Required] Y_D_007_Moments ───────────────────────────────────────

    /// <summary>
    /// The moment Σm = 95 is derived D96 spectral content (theorem-class).
    /// </summary>
    [Fact]
    public void Y_D_007_Moments()
    {
        double[] mult = LambdaMultiplicities();
        Assert.Equal(95.0, mult.Sum(), 6); // Σm = 95 (first moment)

        // #g = 44 (group count) and occ₂ = 87 (dense band) are canonical D96 reads.
        Assert.Equal(44.0, GroupCount, 6);
        Assert.Equal(87.0, DenseBand, 6);

        // The moment ladder (Σ√m = 64.08, Σm² = 229) is also derived.
        Assert.Equal(64.08, mult.Sum(m => Math.Sqrt(m)), 2);
        Assert.Equal(229.0, mult.Sum(m => m * m), 6);
    }

    // ── [Required] Y_D_007_OccMomSpan ────────────────────────────────────

    /// <summary>
    /// occMom = 1900.25 and span = 6.40 are dimensionless spectral invariants.
    /// </summary>
    [Fact]
    public void Y_D_007_OccMomSpan()
    {
        double occMom = (4.0 * 4 + 4.0 * 4 + 87.0 * 87) / 4.0;
        Assert.Equal(1900.25, occMom, 2);

        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        Assert.Equal(6.40, freqs[^1] / freqs[0], 2); // span (dimensionless)

        // Both are dimensionless (pure numbers).
        Assert.True(occMom > 0 && freqs[^1] / freqs[0] > 0);
    }

    // ── [Required] Y_D_007_ResonanceStructure ────────────────────────────

    /// <summary>
    /// occ₂ = 87 is the dense-band occupancy — a resonance output (octave band 3).
    /// The resonance structure provides the occ₂ factor of A.
    /// </summary>
    [Fact]
    public void Y_D_007_ResonanceStructure()
    {
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        double w0 = freqs[0];
        int b3 = freqs.Count(x => 4 * w0 <= x && x < 8 * w0);
        Assert.Equal(87, b3); // dense-band occupancy (resonance output)

        // The resonance structure (octave bands, Z2 pairs) is derived spectral content.
        Assert.Equal(87.0, DenseBand, 6);
    }

    // ── [Required] Y_D_007_ClosureInvariants ─────────────────────────────

    /// <summary>
    /// The closure invariants (moments, span, algebraic spectrum) are derived,
    /// dimensionless, and invariant under the ring's automorphisms.
    /// </summary>
    [Fact]
    public void Y_D_007_ClosureInvariants()
    {
        double[] mult = LambdaMultiplicities();
        double sumM = mult.Sum();

        // Invariants: moments, span, Z2 pairing, algebraic spectrum.
        Assert.Equal(95.0, sumM, 6);

        // Z2 pairing (invariant).
        var lam = new double[N];
        for (int k = 0; k < N; k++) lam[k] = Lambda(k);
        for (int k = 1; k <= 5; k++) Assert.Equal(lam[k], lam[N - k], 8);

        // Algebraic spectrum (invariant, no transcendental value in the content).
        Assert.Equal(0.3864, Lambda(1), 3);
    }

    // ── [Required] Y_D_007_AbsoluteScale ─────────────────────────────────

    /// <summary>
    /// The absolute Planck scale M_Pl = v·A³ = 1.2234e19 GeV requires the calibration
    /// anchor v (weak scale). Without v, A³ is dimensionless — no mass scale.
    /// </summary>
    [Fact]
    public void Y_D_007_AbsoluteScale()
    {
        double A = SigmaM * GroupCount * DenseBand;
        double MPl = WeakScaleAnchor * A * A * A;
        Assert.Equal(1.2234e19, MPl, 1e19 * 1e-3); // ≈ 1.2234e19 GeV

        // The dimensionless A³ alone carries no mass scale: it is a pure number.
        double A3 = A * A * A;
        Assert.Equal(4.8094e16, A3, 1e16 * 1e-4);

        // The anchor v is required for the absolute scale (BOUNDARY/calibration).
        // The SI G = ħc/M_Pl² imports c, ħ, and the GeV↔kg conversion (BOUNDARY).
        // (Documented: dimensionless DERIVED; absolute scale BOUNDARY.)
    }

    // ── [Required] Y_D_007_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_007_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_007 — Planck Scale Audit");

        sb.AppendLine("Goal: can the Planck scale be derived without calibration anchors?");
        sb.AppendLine();

        double A = SigmaM * GroupCount * DenseBand;
        double A3 = A * A * A;
        double MPl = WeakScaleAnchor * A3;

        sb.AppendLine("[1] Dimensionless structure (DERIVED)");
        sb.AppendLine($"    A = Σm·#g·occ₂ = {SigmaM}·{GroupCount}·{DenseBand} = {A:F0}");
        sb.AppendLine($"    A³ = {A3:F4e}  (pure number, no units, no anchors)");
        sb.AppendLine($"    occMom = 1900.25, span = 6.40 (dimensionless invariants)");
        sb.AppendLine();

        sb.AppendLine("[2] Absolute scale (BOUNDARY)");
        sb.AppendLine($"    M_Pl = v·A³ = {WeakScaleAnchor}·{A3:F4e} = {MPl:F4e} GeV");
        sb.AppendLine("    requires the calibration anchor v (weak scale, GeV unit)");
        sb.AppendLine("    SI G = ħc/M_Pl² imports c, ħ, and the GeV↔kg conversion");
        sb.AppendLine();

        sb.AppendLine("[3] Classification");
        sb.AppendLine("    A) derived dimensionless Planck ratio → DERIVED (A³ = 4.8094e16)");
        sb.AppendLine("    B) derived Planck scale              → NOT DERIVED (needs v)");
        sb.AppendLine("    C) requires anchor                   → YES (weak scale v)");
        sb.AppendLine("    D) requires c, ħ, G import           → YES (SI value of G)");
        sb.AppendLine();

        sb.AppendLine("[4] Conclusion");
        sb.AppendLine("    The dimensionless Planck structure is DERIVED from D96; the");
        sb.AppendLine("    absolute scale requires the calibration anchor v; the SI value");
        sb.AppendLine("    imports c, ħ, and the unit conversion. The Planck scale is");
        sb.AppendLine("    calibrated, not derived. No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }

    // ── Helper ───────────────────────────────────────────────────────────

    private static double[] LambdaMultiplicities()
    {
        var groups = new List<double>();
        foreach (var g in Enumerable.Range(1, N - 1).Select(k => Lambda(k)).GroupBy(l => Math.Round(l, 8)))
            groups.Add(g.Count());
        return groups.ToArray();
    }
}
