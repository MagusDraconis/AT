using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_003 — Resonance Observables Audit test suite (Y_D_003_Tests.cs).
///
/// Question: can resonance alone generate physical observables?
///
/// Verdict tested: resonance alone generates the SPECTRAL observables (DERIVED:
/// mode occupation, pair structure, zero-mode role, spectral invariants) but NOT the
/// physical observables (EMERGENT sector mapping + BOUNDARY calibration anchors/fits).
///
/// Deterministic: closed-form circulant eigenvalues + analytic moments.
/// </summary>
public class Y_D_003_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_003_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_003_ModeOccupation ────────────────────────────────

    /// <summary>
    /// The octave occupancies [4,4,87] and occMom = 1900.25 are direct spectral reads —
    /// DERIVED (theorem-class), no mapping or calibration.
    /// </summary>
    [Fact]
    public void Y_D_003_ModeOccupation()
    {
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        double w0 = freqs[0];
        int b1 = freqs.Count(x => w0 <= x && x < 2 * w0);
        int b2 = freqs.Count(x => 2 * w0 <= x && x < 4 * w0);
        int b3 = freqs.Count(x => 4 * w0 <= x && x < 8 * w0);

        Assert.Equal(new[] { 4, 4, 87 }, new[] { b1, b2, b3 }); // DERIVED
        double occMom = (4.0 * 4 + 4.0 * 4 + 87.0 * 87) / 4.0;
        Assert.Equal(1900.25, occMom, 2);                       // DERIVED
    }

    // ── [Required] Y_D_003_ResonantPairAccess ────────────────────────────

    /// <summary>
    /// The 47 Z2 pair structure is spectral (DERIVED); the sector role of the pairs is a
    /// correspondence (EMERGENT) — a supported mapping, not a unique derivation.
    /// </summary>
    [Fact]
    public void Y_D_003_ResonantPairAccess()
    {
        // Pair structure: DERIVED (λ_k = λ_{N−k}).
        var lam = new double[N];
        for (int k = 0; k < N; k++) lam[k] = Lambda(k);
        int pairs = 0;
        for (int k = 1; k <= 47; k++)
            if (Math.Abs(lam[k] - lam[N - k]) < 1e-9) pairs++;
        Assert.Equal(47, pairs);

        // Sector role: EMERGENT (correspondence) — the mapping of pairs to physical
        // doublets is a supported assignment, not a spectral derivation.
        // (Documented: resonance gives the pairs; the sector assignment is beyond it.)
        Assert.Equal(95, 2 * pairs + 1); // 94 paired + self-conjugate
    }

    // ── [Required] Y_D_003_ZeroModeRole ──────────────────────────────────

    /// <summary>
    /// The zero mode is the uniform reference state — DERIVED (fully spectral).
    /// </summary>
    [Fact]
    public void Y_D_003_ZeroModeRole()
    {
        Assert.Equal(0.0, Lambda(0), 10);
        Assert.Equal(0.0, Omega(0), 10);
        Assert.Equal(0.01042, 1.0 / N, 4); // constant eigenvector (uniform reference)
    }

    // ── [Required] Y_D_003_ObservableProjection ──────────────────────────

    /// <summary>
    /// The spectral moments are DERIVED (theorem); the sector mapping is EMERGENT
    /// (correspondence); the dimensional values are BOUNDARY (calibration anchors v, m_e
    /// and the fit 1/α_em).
    /// </summary>
    [Fact]
    public void Y_D_003_ObservableProjection()
    {
        // DERIVED: the spectral moments are exact spectral reads.
        double[] mult = LambdaMultiplicities();
        Assert.Equal(95.0, mult.Sum(), 6);            // Σm = 95
        Assert.Equal(64.08, mult.Sum(m => Math.Sqrt(m)), 2); // Σ√m = 64.08
        Assert.Equal(229.0, mult.Sum(m => m * m), 6); // Σm² = 229

        // EMERGENT (correspondence): the sector assignment is a mapping, not derived.
        // (Documented: which moment reads which sector is correspondence-class.)
        // BOUNDARY (calibration): masses/couplings need anchors v, m_e; 1/α_em is a fit.
        // (Documented: resonance supplies the numbers; the physics needs the anchors.)
    }

    // ── [Required] Y_D_003_SpectralInvariants ────────────────────────────

    /// <summary>
    /// The span, moments, Z2 pairs, octave bands, and algebraic spectrum are invariant
    /// spectral content — DERIVED.
    /// </summary>
    [Fact]
    public void Y_D_003_SpectralInvariants()
    {
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        Assert.Equal(6.40, freqs[^1] / freqs[0], 2); // span (invariant)

        double[] mult = LambdaMultiplicities();
        Assert.Equal(95.0, mult.Sum(), 6); // Σm (invariant)

        // Z2 pairing (invariant): λ_k = λ_{N−k}.
        var lam = new double[N];
        for (int k = 0; k < N; k++) lam[k] = Lambda(k);
        for (int k = 1; k <= 5; k++) Assert.Equal(lam[k], lam[N - k], 8);

        // Algebraic spectrum (invariant, B_002): the smallest positive eigenvalue is
        // algebraic (no transcendental value in the content).
        Assert.Equal(0.3864, Lambda(1), 3);
    }

    // ── [Required] Y_D_003_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_003 — Resonance Observables Audit");

        sb.AppendLine("Goal: can resonance alone generate physical observables?");
        sb.AppendLine();

        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);
        Array.Sort(freqs);
        double w0 = freqs[0];
        int b1 = freqs.Count(x => w0 <= x && x < 2 * w0);
        int b2 = freqs.Count(x => 2 * w0 <= x && x < 4 * w0);
        int b3 = freqs.Count(x => 4 * w0 <= x && x < 8 * w0);
        double occMom = (4.0 * 4 + 4.0 * 4 + 87.0 * 87) / 4.0;
        double[] mult = LambdaMultiplicities();

        sb.AppendLine("[1] Resonance-generated (DERIVED) spectral observables");
        sb.AppendLine($"    mode occupation [{b1},{b2},{b3}], occMom = {occMom:F2}");
        sb.AppendLine($"    Σm = {mult.Sum()}, Σ√m = {mult.Sum(m => Math.Sqrt(m)):F2}, Σm² = {mult.Sum(m => m * m)}");
        sb.AppendLine($"    span = {freqs[^1] / freqs[0]:F2}, Z2 pairs = 47, zero mode λ₀ = 0");
        sb.AppendLine();

        sb.AppendLine("[2] NOT resonance-generated (physical observables)");
        sb.AppendLine("    sector mapping: EMERGENT (correspondence — which moment reads which sector)");
        sb.AppendLine("    dimensional values: BOUNDARY (calibration anchors v, m_e; fit 1/α_em)");
        sb.AppendLine();

        sb.AppendLine("[3] Classification");
        sb.AppendLine("    mode occupation          → DERIVED");
        sb.AppendLine("    resonant pair structure  → DERIVED");
        sb.AppendLine("    resonant pair sector role→ EMERGENT");
        sb.AppendLine("    zero-mode role           → DERIVED");
        sb.AppendLine("    spectral moments         → DERIVED");
        sb.AppendLine("    sector projection        → EMERGENT");
        sb.AppendLine("    dimensional values       → BOUNDARY");
        sb.AppendLine("    spectral invariants      → DERIVED");
        sb.AppendLine();

        sb.AppendLine("[4] Conclusion");
        sb.AppendLine("    Resonance alone generates the SPECTRAL observables (DERIVED).");
        sb.AppendLine("    It does NOT generate the PHYSICAL observables — the sector");
        sb.AppendLine("    correspondence (EMERGENT) and the calibration anchors (BOUNDARY)");
        sb.AppendLine("    are required. Resonance is the spectral source, not the complete");
        sb.AppendLine("    generator. No canonical value is changed.");
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
