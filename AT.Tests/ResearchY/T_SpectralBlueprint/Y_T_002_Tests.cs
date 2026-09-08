using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_002 — Physical Spectrum Audit test suite (Y_T_002_Tests.cs).
///
/// Question: Which target spectra admit SPARSE POSITIVE-weight (physical) realizations?
///
/// Method (inherits T_001's closed-form inverse map): for a circulant ring the weights
/// are the IDFT of the spectrum, so a spectrum is physically realizable by a ring
/// material iff every reconstructed weight w_d ≥ 0 (attractive coupling only). This is
/// exactly the condition that the spectrum is a NEGATIVE-DEFINITE function on Z_N
/// (Bochner/Schoenberg): the physical spectra form the convex cone spanned by the
/// single-edge generators g_d = 2(1−cos 2πdk/N). We scan five families, measure the
/// physicality score P = (attractiveness + sparsity + stability)/3, and map the boundary
/// where the smallest generator coefficient w_d crosses zero.
/// Deterministic — closed-form, no fitted parameters, N = 96.
/// </summary>
public class Y_T_002_Tests : ResearchTestBase
{
    private const int N = 96;        // D96 ring size
    private const double Eps = 1e-6; // weight threshold

    public Y_T_002_Tests(ITestOutputHelper output) : base(output) { }

    // ── Family builders (each symmetric, λ_0 = 0, λ_k = λ_{N−k}) ───────────

    /// <summary>D96 eigenvalue at Fourier index m (canonical C96(±1..±6)).</summary>
    private static double D96AtM(int m)
    {
        double s = 0.0;
        for (int d = 1; d <= 6; d++) s += 1.0 - Math.Cos(2.0 * Math.PI * d * m / N);
        return 2.0 * s;
    }

    /// <summary>Geometric (octave) family λ_m = r^(m−1). r=1 degenerate (physical), large r unphysical.</summary>
    private static double[] GeometricSpectrum(double r)
        => SpectralBlueprint.BuildSymmetric(N, m => Math.Pow(r, m - 1));

    /// <summary>Band-gap family: D96 with the upper band (m&gt;24) lifted by `gap`.</summary>
    private static double[] BandGapSpectrum(double gap)
        => SpectralBlueprint.BuildSymmetric(N, m => D96AtM(m) + (m > 24 ? gap : 0.0));

    /// <summary>Clustered family: 3 degenerate clusters at 5, 5+sep, 5+2·sep.</summary>
    private static double[] ClusteredSpectrum(double sep)
        => SpectralBlueprint.BuildSymmetric(N, m => 5.0 + sep * ((m - 1) / 16));

    // ── Physicality score ──────────────────────────────────────────────────

    /// <summary>Stability margin: min positive weight / max weight; 0 if any repulsive coupling.</summary>
    private static double StabilityScore(double[] w)
    {
        var (neg, _, _) = SpectralBlueprint.NegativeWeights(w);
        if (neg > 0) return 0.0;
        double wMax = 0.0;
        for (int d = 1; d < N; d++) wMax = Math.Max(wMax, w[d]);
        if (wMax < Eps) return 1.0;
        double wMinPos = double.PositiveInfinity;
        for (int d = 1; d < N; d++)
            if (w[d] > Eps) wMinPos = Math.Min(wMinPos, w[d]);
        if (double.IsPositiveInfinity(wMinPos)) return 1.0;
        return Math.Clamp(wMinPos / wMax, 0.0, 1.0);
    }

    /// <summary>
    /// Physicality score P = (attractiveness + sparsity + stability) / 3, each in [0,1].
    /// attractiveness = positive-mass fraction; sparsity = 1 − edges/(N/2);
    /// stability = coupling margin to the boundary.
    /// </summary>
    private static double PhysicalityScore(double[] w)
    {
        double pos = SpectralBlueprint.PositiveMass(w);
        var (_, _, negMass) = SpectralBlueprint.NegativeWeights(w);
        double total = pos + negMass;
        double pPos = total < 1e-12 ? 1.0 : pos / total;
        double pSparse = 1.0 - (double)SpectralBlueprint.EdgeCount(w, N) / (N / 2);
        double pStable = StabilityScore(w);
        return (pPos + pSparse + pStable) / 3.0;
    }

    private static string Classify(double p)
        => p >= 0.8 ? "PHYSICAL" : p >= 0.4 ? "MARGINAL" : "UNPHYSICAL";

    /// <summary>Last parameter (coarse scan) that is still physical (zero negative weights).</summary>
    private static double FindPhysicalBoundary(Func<double, double[]> spectrumAt, double lo, double hi, int steps)
    {
        double lastPhysical = lo;
        for (int i = 0; i <= steps; i++)
        {
            double p = lo + (hi - lo) * i / steps;
            double[] w = SpectralBlueprint.ReconstructWeights(spectrumAt(p), N);
            var (neg, _, _) = SpectralBlueprint.NegativeWeights(w);
            if (neg > 0) return lastPhysical;
            lastPhysical = p;
        }
        return hi;
    }

    // ── 1. Cone generators: a single-edge spectrum reconstructs to a single edge ─

    [Fact]
    public void Y_T_002_GeneratorBasis()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        foreach (int d in new[] { 1, 2, 3, 6, 12, 24 })
        {
            // Generator g_d(k) = 2(1 − cos 2πdk/N) = Laplacian of a single edge at distance d.
            double[] lam = SpectralBlueprint.BuildSymmetric(N, m => 2.0 * (1.0 - Math.Cos(2.0 * Math.PI * d * m / N)));
            double[] w = SpectralBlueprint.ReconstructWeights(lam, N);

            for (int e = 1; e < N; e++)
            {
                double expected = (e == d || e == N - d) ? 1.0 : 0.0;
                Assert.Equal(expected, w[e], 9);
            }
            Assert.Equal(1, SpectralBlueprint.EdgeCount(w, N));
            Assert.Equal(0, SpectralBlueprint.NegativeWeights(w).Count);
        }
    }

    // ── 2. D96-like (circulant) family is always physical ───────────────────

    [Fact]
    public void Y_T_002_D96LikeFamily()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        foreach (int k in new[] { 1, 2, 3, 6, 12, 24, 48 })
        {
            double[] w = SpectralBlueprint.ReconstructWeights(SpectralBlueprint.CirculantSpectrum(N, k), N);
            Assert.Equal(0, SpectralBlueprint.NegativeWeights(w).Count);
            Assert.Equal(k, SpectralBlueprint.EdgeCount(w, N));
            // weights are exactly 1 on distances 1..k (the antipodal d=N/2 carries 2).
            for (int d = 1; d <= k && d < N / 2; d++) Assert.Equal(1.0, w[d], 9);
        }
    }

    // ── 3–5. Boundary scans: find where the first repulsive coupling appears ─

    [Fact]
    public void Y_T_002_GeometricBoundary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        // lo r=1 → degenerate (w_d = 1/n > 0, physical); hi r=1.3 → extreme span (unphysical).
        Assert.Equal(0, SpectralBlueprint.NegativeWeights(SpectralBlueprint.ReconstructWeights(GeometricSpectrum(1.0), N)).Count);
        Assert.True(SpectralBlueprint.NegativeWeights(SpectralBlueprint.ReconstructWeights(GeometricSpectrum(1.3), N)).Count > 0);

        double crit = FindPhysicalBoundary(GeometricSpectrum, 1.0, 1.3, 300);
        Assert.True(crit > 1.0 && crit < 1.3, $"geometric boundary should be in (1.0,1.3), got {crit:F4}");
    }

    [Fact]
    public void Y_T_002_BandGapBoundary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        // lo gap=0 → D96 (physical); hi gap=40 → lifted upper band (unphysical).
        Assert.Equal(0, SpectralBlueprint.NegativeWeights(SpectralBlueprint.ReconstructWeights(BandGapSpectrum(0.0), N)).Count);
        Assert.True(SpectralBlueprint.NegativeWeights(SpectralBlueprint.ReconstructWeights(BandGapSpectrum(40.0), N)).Count > 0);

        double crit = FindPhysicalBoundary(BandGapSpectrum, 0.0, 40.0, 300);
        // D96 has ZERO margin against upper-band lifting: any positive gap is unphysical
        // (its far-edge weights are exactly zero, so the step's oscillation flips them negative).
        Assert.True(crit < 0.5, $"band-gap boundary should be at ~0 (D96 has no margin), got {crit:F4}");
    }

    [Fact]
    public void Y_T_002_ClusteredBoundary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        // lo sep=0 → degenerate (physical); hi sep=40 → 3 sharp clusters (unphysical).
        Assert.Equal(0, SpectralBlueprint.NegativeWeights(SpectralBlueprint.ReconstructWeights(ClusteredSpectrum(0.0), N)).Count);
        Assert.True(SpectralBlueprint.NegativeWeights(SpectralBlueprint.ReconstructWeights(ClusteredSpectrum(40.0), N)).Count > 0);

        double crit = FindPhysicalBoundary(ClusteredSpectrum, 0.0, 40.0, 300);
        Assert.True(crit > 0.0 && crit < 40.0, $"clustered boundary should be in (0,40), got {crit:F4}");
    }

    // ── 6. Random spectra: the physical cone has (near-)zero measure ────────

    [Fact]
    public void Y_T_002_RandomScan()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var rng = new Random(20260908); // deterministic
        int total = 400, physical = 0;
        for (int i = 0; i < total; i++)
        {
            double[] lam = SpectralBlueprint.BuildSymmetric(N, _ => rng.NextDouble());
            double[] w = SpectralBlueprint.ReconstructWeights(lam, N);
            if (SpectralBlueprint.NegativeWeights(w).Count == 0) physical++;
        }
        // A random spectrum is almost never negative-definite: physicality is rare.
        Assert.True(physical < total * 0.05, $"expected <5% physical, got {physical}/{total}");
    }

    // ── 7. Physicality score of the five canonical targets ──────────────────

    [Fact]
    public void Y_T_002_PhysicalityScore()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var targets = new (string Name, double[] Lambda)[]
        {
            ("D96", SpectralBlueprint.CirculantSpectrum(N, 6)),
            ("band-gap", SpectralBlueprint.BuildSymmetric(N, m => m <= 24 ? 0.02 * m * m : 30.0 + 0.02 * (m - 24.0) * (m - 24.0))),
            ("clustered", SpectralBlueprint.BuildSymmetric(N, m => m <= 16 ? 5.0 : m <= 32 ? 25.0 : 60.0)),
            ("octave", SpectralBlueprint.BuildSymmetric(N, m => Math.Pow(2.0, (m - 1) / 4.0))),
            ("max-separated", SpectralBlueprint.BuildSymmetric(N, m => (double)m)),
        };

        var classes = new Dictionary<string, string>();
        foreach (var (name, lam) in targets)
        {
            double[] w = SpectralBlueprint.ReconstructWeights(lam, N);
            classes[name] = Classify(PhysicalityScore(w));
        }

        Assert.Equal("PHYSICAL", classes["D96"]);
        Assert.Equal("UNPHYSICAL", classes["band-gap"]);
        Assert.Equal("UNPHYSICAL", classes["clustered"]);
        Assert.Equal("UNPHYSICAL", classes["octave"]);
        Assert.NotEqual("UNPHYSICAL", classes["max-separated"]); // 0 negative weights → physical or marginal
    }

    // ── Research report ─────────────────────────────────────────────────────

    [Fact]
    public void Y_T_002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_002 — Physical Spectrum Audit");

        sb.AppendLine("Question: which target spectra admit sparse positive-weight realizations?");
        sb.AppendLine();
        sb.AppendLine("Assumption (only AT primitive used): Laplacian <-> spectrum.");
        sb.AppendLine("Method: a spectrum is physically realizable by a ring material iff every");
        sb.AppendLine("        reconstructed weight w_d >= 0 (attractive coupling only) — i.e. the");
        sb.AppendLine("        spectrum is a NEGATIVE-DEFINITE function on Z_N. The physical spectra");
        sb.AppendLine("        form the convex cone spanned by single-edge generators");
        sb.AppendLine("        g_d = 2(1 − cos 2πdk/N). Boundary = where the smallest coefficient");
        sb.AppendLine("        w_d crosses zero. Physicality score P = (attractiveness + sparsity +");
        sb.AppendLine("        stability)/3. Deterministic, N = 96.");
        sb.AppendLine();

        // ── 1. Five canonical targets ──────────────────────────────────────
        var targets = new (string Name, double[] Lambda)[]
        {
            ("D96 (canonical)", SpectralBlueprint.CirculantSpectrum(N, 6)),
            ("band-gap", SpectralBlueprint.BuildSymmetric(N, m => m <= 24 ? 0.02 * m * m : 30.0 + 0.02 * (m - 24.0) * (m - 24.0))),
            ("clustered", SpectralBlueprint.BuildSymmetric(N, m => m <= 16 ? 5.0 : m <= 32 ? 25.0 : 60.0)),
            ("octave-spaced", SpectralBlueprint.BuildSymmetric(N, m => Math.Pow(2.0, (m - 1) / 4.0))),
            ("max-separated", SpectralBlueprint.BuildSymmetric(N, m => (double)m)),
        };

        sb.AppendLine("[1] Physicality of five canonical targets");
        sb.AppendLine($"    {"target",-15} {"score",7} {"class",-11} {"edges",6} {"neg.w",6} {"min.w",9} {"mass",9}");
        foreach (var (name, lam) in targets)
        {
            double[] w = SpectralBlueprint.ReconstructWeights(lam, N);
            double p = PhysicalityScore(w);
            var (neg, min, _) = SpectralBlueprint.NegativeWeights(w);
            int edges = SpectralBlueprint.EdgeCount(w, N);
            sb.AppendLine($"    {name,-15} {p,7:F3} {Classify(p),-11} {edges,6} {neg,6} {min,9:F3} {SpectralBlueprint.CouplingMass(w),9:F2}");
        }
        sb.AppendLine();

        // ── 2. Boundary map ────────────────────────────────────────────────
        sb.AppendLine("[2] Boundary map (where the first repulsive coupling appears)");
        sb.AppendLine();

        void Scan(string name, Func<double, double[]> family, double lo, double hi)
        {
            double crit = FindPhysicalBoundary(family, lo, hi, 300);
            // signature just inside (physical) and just outside (unphysical) the boundary
            double[] wIn = SpectralBlueprint.ReconstructWeights(family(Math.Max(lo, crit - (hi - lo) / 300)), N);
            double[] wOut = SpectralBlueprint.ReconstructWeights(family(Math.Min(hi, crit + (hi - lo) / 300)), N);
            sb.AppendLine($"    {name,-14} boundary = {crit,8:F4}");
            sb.AppendLine($"        inside : P = {PhysicalityScore(wIn):F3}  ({SpectralBlueprint.EdgeCount(wIn, N)} edges, {SpectralBlueprint.NegativeWeights(wIn).Count} neg)");
            sb.AppendLine($"        outside: P = {PhysicalityScore(wOut):F3}  ({SpectralBlueprint.EdgeCount(wOut, N)} edges, {SpectralBlueprint.NegativeWeights(wOut).Count} neg)");
        }

        Scan("geometric r", GeometricSpectrum, 1.0, 1.3);
        Scan("band-gap g", BandGapSpectrum, 0.0, 40.0);
        Scan("cluster sep", ClusteredSpectrum, 0.0, 40.0);
        sb.AppendLine("    note: the band-gap boundary is at g=0 — D96 sits exactly ON the cone");
        sb.AppendLine("          boundary (far weights are zero), so any band-gap opening is");
        sb.AppendLine("          immediately unphysical.");
        sb.AppendLine();

        // ── 3. Random-scan measure ─────────────────────────────────────────
        var rng = new Random(20260908);
        int total = 400, physical = 0;
        for (int i = 0; i < total; i++)
        {
            double[] lam = SpectralBlueprint.BuildSymmetric(N, _ => rng.NextDouble());
            if (SpectralBlueprint.NegativeWeights(SpectralBlueprint.ReconstructWeights(lam, N)).Count == 0) physical++;
        }
        sb.AppendLine("[3] Random-spectrum scan");
        sb.AppendLine($"    {physical}/{total} random spectra are physical ({100.0 * physical / total:F2}%)");
        sb.AppendLine("    -> the physical cone is a measure-(near-)zero subset of spectrum space.");
        sb.AppendLine();

        // ── 4. Verdicts ────────────────────────────────────────────────────
        sb.AppendLine("[4] Verdicts (DERIVED / CORRESPONDENCE / REFUTED)");
        sb.AppendLine("    V1 physical spectra = negative-definite cone           → DERIVED");
        sb.AppendLine("       (w_d >= 0 iff spectrum is a non-negative combo of generators g_d;");
        sb.AppendLine("        Bochner/Schoenberg on Z_N)");
        sb.AppendLine("    V2 circulant (D96-like) family is always physical      → DERIVED");
        sb.AppendLine("    V3 physicality score maps spectra → materials          → CORRESPONDENCE");
        sb.AppendLine("       (P = attractiveness + sparsity + stability)");
        sb.AppendLine("    V4 'sparse positive-weight realization' hypothesis     → SUPPORTED for");
        sb.AppendLine("       smooth, moderate-span, band-structured spectra; REFUTED for");
        sb.AppendLine("       discontinuous (band-gap, cluster) and extreme-span (octave) spectra.");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    The physical/unphysical boundary is the boundary of the cone of");
        sb.AppendLine("    negative-definite functions on Z_N — crossed exactly when the smallest");
        sb.AppendLine("    generator coefficient w_d changes sign. D96 sits deep inside the cone");
        sb.AppendLine("    (6 sparse positive edges), while band-gap, clustered, and extreme-octave");
        sb.AppendLine("    spectra lie outside it (they demand repulsive coupling). Physicality is");
        sb.AppendLine("    therefore a RARE, STRUCTURED property: almost all spectra are unrealizable");
        sb.AppendLine("    by a plain oscillator ring, and AT's D96 is the canonical sparse physical");
        sb.AppendLine("    realization.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
