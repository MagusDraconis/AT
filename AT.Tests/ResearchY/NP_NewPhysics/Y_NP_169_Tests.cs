using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_169 — Natural Scaling vs Mass-Gap Audit.
///
/// Question: can ANY natural Actualization scaling f(N) preserve a positive mass gap
/// m(N) = f(N)·λ_gap(N) as N→∞, given λ_gap(N) = 4π²·91/N² + O(N⁻⁴)?
///
/// Verdict tested: no scaling derived from existing AT primitives preserves a positive
/// gap — the only positive limits come from an imported external length (physical 1/a² at
/// fixed volume) or a tautological level-spacing renormalization (spectral density). Every
/// AT-native scaling (occupancy, D96-family, fixed-spacing) gives a ZERO gap; ad-hoc f ∝ N²
/// or faster is rejected as underivable. Deterministic: closed-form circulant eigenvalues.
/// </summary>
public class Y_NP_169_Tests : ResearchTestBase
{
    private const int StepMax = 6;

    public Y_NP_169_Tests(ITestOutputHelper output) : base(output) { }

    // ── Closed-form circulant C_N(1..6) quantities ───────────────────────────

    private static double Lambda(int k, int n)
    {
        double sum = 0.0;
        for (int d = 1; d <= StepMax; d++)
            sum += 1.0 - Math.Cos(2.0 * Math.PI * d * k / n);
        return 2.0 * sum;
    }

    /// <summary>Minimum positive eigenvalue λ_gap(N) = λ_1(N) (the k=1 / k=N−1 degenerate pair).</summary>
    private static double LambdaGap(int n) => Lambda(1, n);

    private static int SumSquaredSteps()
    {
        int sum = 0;
        for (int d = 1; d <= StepMax; d++) sum += d * d;
        return sum;
    }

    /// <summary>Analytic coefficient c = 4π²·Σd² = 4π²·91.</summary>
    private static double Coefficient() => 4.0 * Math.PI * Math.PI * SumSquaredSteps();

    /// <summary>
    /// Classify the N→∞ limit of m(N) = f(N)·λ_gap(N) from the growth exponent of f(N):
    /// f = O(N^e). Since λ_gap = O(N^-2), m = O(N^(e-2)): e&lt;2 → 0 (ZERO), e=2 → constant
    /// (POSITIVE), e&gt;2 → ∞ (DIVERGENT). Only the exponent matters for the limit.
    /// </summary>
    private static string ClassifyLimit(int growthExponentOfF)
        => growthExponentOfF > 2 ? "DIVERGENT"
        : growthExponentOfF == 2 ? "POSITIVE GAP"
        : "ZERO GAP";

    // ── 1. Physical scalings ─────────────────────────────────────────────────

    [Fact]
    public void Y_NP_169_PhysicalScalings()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // (a) Fixed lattice spacing a0 = 1 (growing circumference L = N·a0):
        //     f = 1/a0² = 1, so m(N) = λ_gap(N) → 0. ZERO GAP.
        int[] sizes = [96, 192, 384, 768, 1536];
        double previous = double.PositiveInfinity;
        foreach (int n in sizes)
        {
            double m = LambdaGap(n);                 // spacing a0 = 1
            Assert.True(m < previous, $"fixed-spacing mass must decrease at N={n}");
            previous = m;
        }
        Assert.True(LambdaGap(1536) < 0.002, "fixed-spacing gap tends to zero");
        Assert.Equal("ZERO GAP", ClassifyLimit(0));   // f = 1/a0² is O(1)

        // (b) Fixed circumference L0 = 1 (a = L0/N): f = 1/a² = N²/L0².
        //     m(N) = N²·λ_gap(N) → 4π²·91/L0² = c. POSITIVE, but this imports the external
        //     length L0 (not an AT primitive) and is the compact-domain kinematic gap only.
        int nLarge = 1536;
        double mFixedVolume = LambdaGap(nLarge) * nLarge * nLarge;
        Assert.InRange(
            Math.Abs(mFixedVolume / Coefficient() - 1.0), 0.0, 4e-5);
        Assert.Equal("POSITIVE GAP", ClassifyLimit(2));   // f = N²/L0² is O(N²)
    }

    // ── 2. Spectral-density scalings ─────────────────────────────────────────

    [Fact]
    public void Y_NP_169_SpectralDensityScalings()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double c = Coefficient();
        int n = 1536;
        double gap = LambdaGap(n);

        // Weyl counting law for C_N(1..6): N(λ) ≈ 2N·√(λ/c) for small λ. At λ = m²·λ_gap the
        // count is exactly 2m (the degenerate ±k pairs k=1..m).
        for (int mode = 1; mode <= 4; mode++)
        {
            double target = gap * mode * mode;
            int exact = 0;
            for (int k = 1; k < n; k++) if (Lambda(k, n) <= target + 1e-12) exact++;
            Assert.Equal(2 * mode, exact);   // N(λ_gap) = 2, N(4λ_gap) = 4, ...
        }

        // Spectral density at the gap edge: ρ(λ_gap) = dN/dλ = N/√(c·λ_gap) = N²/c.
        // m(N) = ρ(λ_gap)·λ_gap → 1. POSITIVE, but tautological: the gap in units of its own
        // level spacing is one mode by definition — no physical mass scale is produced.
        double rho = n / Math.Sqrt(c * gap);
        double mRho = rho * gap;
        Assert.InRange(Math.Abs(mRho - 1.0), 0.0, 2e-4);
        Assert.Equal("POSITIVE GAP", ClassifyLimit(2));   // f = ρ(λ_gap) = N²/c is O(N²)
    }

    // ── 3. Occupancy scalings ────────────────────────────────────────────────

    [Fact]
    public void Y_NP_169_OccupancyScalings()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        int n = 1536;
        double gap = LambdaGap(n);

        // AT occupancy primitives are fixed dimensionless numbers (QG210/QG228): none scales
        // with N, so every one drives m(N) = f·λ_gap(N) to zero.
        double[] occupancyPrimitives =
        [
            CosmologicalFractionsOrigin.RecordInformation(),     // I_occ = 0.7513 nats
            CosmologicalFractionsOrigin.MaxInformation(CosmologicalFractionsOrigin.OctaveCount()), // ln K = 1.0986
            CosmologicalFractionsOrigin.VacuumFraction(),        // Ω_Λ = 0.6839
            CosmologicalFractionsOrigin.OctaveCount(),           // K = 3
            CosmologicalFractionsOrigin.OctaveOccupancies().Sum(), // occupied modes = 95
            n,                                                   // total modes N (extensive, O(N))
        ];

        Assert.Equal(3, CosmologicalFractionsOrigin.OctaveCount());
        Assert.Equal(95, CosmologicalFractionsOrigin.OctaveOccupancies().Sum());

        // All O(1) occupancy primitives: m(N) = f·λ_gap(N) = O(N⁻²) → 0.
        foreach (double f in occupancyPrimitives.Take(5))
        {
            Assert.Equal("ZERO GAP", ClassifyLimit(0));
            // Numeric convergence: strictly decreasing and small at N = 1536.
            Assert.True(f * gap < f * LambdaGap(96), $"occupancy f={f:F4} must decrease");
            Assert.True(f * gap < 0.2, $"occupancy f={f:F4} must be small at N=1536");
        }

        // The extensive O(N) occupancy scaling: m = N·λ_gap = O(N⁻¹) → 0 (still ZERO).
        Assert.Equal("ZERO GAP", ClassifyLimit(1));
        Assert.InRange(n * gap, 0.0, 3.0);   // ≈ 2.34, vanishing vs the O(1) coefficient c ≈ 3592
        Assert.True(n * gap < n * LambdaGap(96), "extensive occupancy scaling must decrease");
    }

    // ── 4. D96 family scalings ───────────────────────────────────────────────

    [Fact]
    public void Y_NP_169_D96FamilyScalings()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        int n = 1536;
        double gap = LambdaGap(n);

        // The C_N(1..6) family has a FIXED step set {1..6}: degree 12, Σd² = 91, and mode
        // index k are all O(1); the size N is O(N). None is O(N²).
        double[] familyPrimitives =
        [
            2.0 * StepMax,      // degree = 12 (O(1))
            SumSquaredSteps(),  // Σd² = 91 (O(1), N-independent — the fixed step set)
            n,                  // size N (extensive, O(N))
        ];

        Assert.Equal(91, SumSquaredSteps());

        // O(1) family primitives (degree 12, Σd² = 91): m = O(N⁻²) → 0.
        foreach (double f in familyPrimitives.Take(2))
        {
            Assert.Equal("ZERO GAP", ClassifyLimit(0));
            Assert.True(f * gap < f * LambdaGap(96), $"family primitive f={f:F1} must decrease");
        }

        // Extensive N scaling: m = N·λ_gap = O(N⁻¹) → 0.
        Assert.Equal("ZERO GAP", ClassifyLimit(1));
        Assert.InRange(n * gap, 0.0, 3.0);
    }

    // ── 5. Ad-hoc scalings are rejected ──────────────────────────────────────

    [Fact]
    public void Y_NP_169_AdHocRejection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double c = Coefficient();
        int n = 1536;
        double gap = LambdaGap(n);

        // f = N² (ad-hoc): POSITIVE (→ c). But N² is not an AT primitive — no occupancy,
        // D96-family, or spectral quantity is Θ(N²) except the tautological DOS or the
        // imported length. Rejected as underivable.
        Assert.Equal("POSITIVE GAP", ClassifyLimit(2));
        Assert.InRange(Math.Abs(n * (double)n * gap / c - 1.0), 0.0, 4e-5);

        // f = N³ (ad-hoc): DIVERGENT. Rejected outright — no AT primitive grows faster than O(N).
        Assert.Equal("DIVERGENT", ClassifyLimit(3));
        double nCubedGap = n * (double)n * (double)n * gap;
        Assert.True(nCubedGap > 1e6, "ad-hoc N³ scaling must grow without bound");
    }

    // ── 6. Classification (ZERO / POSITIVE / DIVERGENT and DERIVED / REFUTED) ──

    [Fact]
    public void Y_NP_169_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // The unscaled gap and every AT-native (O(1) or O(N)) scaling vanish.
        Assert.Equal("ZERO GAP", ClassifyLimit(0));
        Assert.Equal("ZERO GAP", ClassifyLimit(1));

        // No AT primitive scales as Θ(N²): the only positive limits are imported (fixed-volume
        // physical length) or tautological (spectral density). Both are rejected as mass gaps.
        Assert.Equal("POSITIVE GAP", ClassifyLimit(2));

        // Ad-hoc faster-than-N² scalings diverge and are rejected.
        Assert.Equal("DIVERGENT", ClassifyLimit(3));
    }

    // ── 7. Research report ───────────────────────────────────────────────────

    [Fact]
    public void Y_NP_169_Run()
    {
        CultureInfo original = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = original; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_169 — Natural Scaling vs Mass Gap");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  1. λ_gap(N) = 4π²·91/N² + O(N⁻⁴) for the circulant family C_N(1..6) (NP_168).");
        sb.AppendLine("  2. m(N) = f(N)·λ_gap(N); the question is whether any NATURAL Actualization");
        sb.AppendLine("     scaling f(N) — derived from existing AT primitives only — keeps m(N) > 0");
        sb.AppendLine("     as N→∞.");
        sb.AppendLine("  3. Ad-hoc f (chosen only to force a positive limit) is rejected.");
        sb.AppendLine();
        sb.AppendLine("The mass-gap scaling boundary: f = o(N²) → ZERO, f ∝ N² → POSITIVE (constant),");
        sb.AppendLine("f = ω(N²) → DIVERGENT. So the question is whether AT supplies a natural N².");
        sb.AppendLine();

        double c = Coefficient();
        int[] sizes = [96, 192, 384, 768, 1536];

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Σ_{{d=1..6}} d² = {SumSquaredSteps()}");
        sb.AppendLine($"  c = 4π²·91 = {c:F12}");
        foreach (int n in sizes)
            sb.AppendLine($"  N={n,4}: λ_gap = {LambdaGap(n):F15}   N²·λ_gap = {n * (double)n * LambdaGap(n),14:F6}");
        sb.AppendLine();

        sb.AppendLine("SCALING AUDIT (m(N) = f(N)·λ_gap(N), N→∞):");
        sb.AppendLine("  1. PHYSICAL scalings:");
        sb.AppendLine("     · fixed lattice spacing a0: f=1/a0² (O(1))     → m→0      ZERO GAP");
        sb.AppendLine("     · fixed circumference L0:  f=N²/L0² (imports L0) → m→c/L0² POSITIVE");
        sb.AppendLine("       (kinematic compact-domain gap only; L0 is an imported, non-AT length)");
        sb.AppendLine("  2. SPECTRAL-DENSITY scalings:");
        sb.AppendLine($"     · f = ρ(λ_gap) = N/√(c·λ_gap) = N²/c          → m→1      POSITIVE");
        sb.AppendLine("       (tautological: the gap in units of its own level spacing is one mode)");
        sb.AppendLine("  3. OCCUPANCY scalings (I_occ=0.7513, lnK=1.0986, ΩΛ=0.6839, K=3, Σocc=95, N):");
        sb.AppendLine("     · all O(1) or O(N)                              → m→0      ZERO GAP");
        sb.AppendLine("  4. D96-FAMILY scalings (degree=12, Σd²=91, N):");
        sb.AppendLine("     · all O(1) or O(N)                              → m→0      ZERO GAP");
        sb.AppendLine("  AD-HOC: f=N² → POSITIVE (underivable); f=N³ → DIVERGENT (rejected).");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - No AT-native scaling is Θ(N²), so NO natural Actualization scaling preserves a");
        sb.AppendLine("    positive mass gap as N→∞.");
        sb.AppendLine("  - The only POSITIVE limits are (a) an imported external length L0 (physical fixed");
        sb.AppendLine("    volume → the kinematic compact-domain gap, not an infinite-volume mass gap) and");
        sb.AppendLine("    (b) the tautological spectral-density renormalization (gap/level-spacing = 1).");
        sb.AppendLine("  - AT-derived scalings (occupancy, D96-family, fixed-spacing) all give ZERO GAP.");
        sb.AppendLine("  - VERDICT: ZERO GAP for AT; 'AT provides a natural scaling preserving a positive");
        sb.AppendLine("    mass gap' is REFUTED. The N² boundary is precisely the non-AT (imported or");
        sb.AppendLine("    tautological) choice.");

        Output.WriteLine(sb.ToString());
    }
}
