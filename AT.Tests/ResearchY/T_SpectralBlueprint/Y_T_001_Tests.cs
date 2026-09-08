using System.Globalization;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_001 — Spectral Blueprint: Inverse Spectral Design test suite
/// (Y_T_001_Tests.cs).
///
/// Question: Can material properties be designed by specifying a TARGET spectrum and
/// reconstructing the coupling graph (Laplacian), instead of the forward path
/// topology → simulation → observed spectrum?
///
/// Method: For circulant (ring) graphs C_N the Laplacian is a circulant matrix whose
/// eigenvalues λ_k are the discrete Fourier transform (DFT) of the first row c_d, and
/// whose first row is the inverse DFT (IDFT) of the eigenvalues:
///       λ_k = Σ_{d=1..N-1} w_d · (1 − cos 2πdk/N)      (forward, weights → spectrum)
///       w_d = −(1/N) Σ_{k=0..N-1} λ_k · cos 2πdk/N     (inverse, spectrum → weights)
/// with edge weights w_d = −c_d ≥ 0 for physical (attractive) coupling. The inverse
/// problem is therefore CLOSED FORM: target spectrum → IDFT → coupling weights →
/// material topology. Deterministic — closed-form, no randomness, no fitted
/// parameters. No AT assumption beyond "Laplacian ↔ spectrum".
/// </summary>
public class Y_T_001_Tests : ResearchTestBase
{
    private const int N = 96;        // D96 ring size (canonical attractor)
    private const double Eps = 1e-6; // weight threshold (an edge with |w_d| ≤ Eps is absent)

    public Y_T_001_Tests(ITestOutputHelper output) : base(output) { }

    // ── Target spectra (each symmetric: λ_0 = 0, λ_k = λ_{N−k}) ─────────────

    /// <summary>Build a symmetric spectrum from a per-Fourier-index profile m = min(k, N−k).</summary>
    private static double[] BuildSymmetric(Func<int, double> valueAtM)
        => SpectralBlueprint.BuildSymmetric(N, valueAtM);

    /// <summary>Canonical D96 spectrum: C96(±1..±6), λ_k = 2Σ_{d=1..6}(1−cos 2πdk/96).</summary>
    private static double[] D96Spectrum()
        => SpectralBlueprint.CirculantSpectrum(N, 6);

    /// <summary>Band-gap target: two separated parabolic bands with an empty interval between.</summary>
    private static double[] BandGapSpectrum()
        => BuildSymmetric(m => m <= 24 ? 0.02 * m * m : 30.0 + 0.02 * (m - 24.0) * (m - 24.0));

    /// <summary>Clustered target: three degenerate clusters (16/16/16 distinct Fourier indices).</summary>
    private static double[] ClusteredSpectrum()
        => BuildSymmetric(m => m <= 16 ? 5.0 : m <= 32 ? 25.0 : 60.0);

    /// <summary>Octave-spaced target: geometric mode frequencies ω = 2^((m−1)/8), λ = ω² = 2^((m−1)/4).</summary>
    private static double[] OctaveSpectrum()
        => BuildSymmetric(m => Math.Pow(2.0, (m - 1) / 4.0));

    /// <summary>Maximally-separated target: uniform integer spacing λ_m = m (all distinct, maximally spread).</summary>
    private static double[] MaxSeparatedSpectrum()
        => BuildSymmetric(m => (double)m);

    // ── Forward / inverse maps (DFT / IDFT) ─────────────────────────────────

    /// <summary>Forward map: coupling weights w_d → Laplacian spectrum λ_k.</summary>
    private static double[] ForwardSpectrum(double[] w)
        => SpectralBlueprint.ForwardSpectrum(w, N);

    /// <summary>Inverse map: target spectrum λ_k → coupling weights w_d (IDFT).</summary>
    private static double[] ReconstructWeights(double[] lam)
        => SpectralBlueprint.ReconstructWeights(lam, N);

    // ── Metrics ─────────────────────────────────────────────────────────────

    private static double MaxAbs(double[] a)
        => SpectralBlueprint.MaxAbs(a);

    /// <summary>Absolute RMS error between two spectra.</summary>
    private static double Rms(double[] a, double[] b)
        => SpectralBlueprint.Rms(a, b);

    /// <summary>Distinct unordered edges (distances 1..N/2) with weight above threshold.</summary>
    private static int EdgeCount(double[] w, double eps = Eps)
        => SpectralBlueprint.EdgeCount(w, N, eps);

    /// <summary>Total coupling mass Σ_d |w_d| (a graph-complexity proxy).</summary>
    private static double CouplingMass(double[] w)
        => SpectralBlueprint.CouplingMass(w);

    /// <summary>Negative-weight statistics: count of negative edges and the most negative weight.</summary>
    private static (int count, double min) NegativeWeightStats(double[] w, double eps = Eps)
    {
        var (c, mn, _) = SpectralBlueprint.NegativeWeights(w, eps);
        return (c, mn);
    }

    /// <summary>
    /// Keep only the m largest-magnitude distinct distances (sparse truncation), zero the
    /// rest. Each unordered distance d ∈ [1, N/2] has two orientations (w_d and w_{N−d});
    /// both are retained so the truncated graph remains symmetric.
    /// </summary>
    private static double[] TruncateToTopM(double[] w, int m)
    {
        var keepDist = Enumerable.Range(1, N / 2)
            .OrderByDescending(d => Math.Abs(w[d]))
            .Take(m)
            .ToHashSet();
        var wt = new double[N];
        for (int d = 1; d < N; d++)
        {
            int dist = d <= N / 2 ? d : N - d;
            if (keepDist.Contains(dist)) wt[d] = w[d];
        }
        return wt;
    }

    // ── 1. Inverse spectral design is exact (round trip closes) ─────────────

    [Fact]
    public void Y_T_001_InverseIsExact()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var spectra = new (string Name, double[] Lambda)[]
        {
            ("D96", D96Spectrum()),
            ("band-gap", BandGapSpectrum()),
            ("clustered", ClusteredSpectrum()),
            ("octave", OctaveSpectrum()),
            ("max-separated", MaxSeparatedSpectrum()),
        };

        foreach (var (name, lam) in spectra)
        {
            double[] w = ReconstructWeights(lam);
            double[] lamRecon = ForwardSpectrum(w);
            double err = Rms(lam, lamRecon);
            double tol = 1e-9 * (1.0 + MaxAbs(lam));
            Assert.True(err < tol,
                $"{name}: round-trip RMS error {err:E3} exceeds tolerance {tol:E3}");
        }
    }

    // ── 2. D96 round trip: recover the connection set {±1..±6} exactly ──────

    [Fact]
    public void Y_T_001_D96RoundTrip()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double[] w = ReconstructWeights(D96Spectrum());

        // Recovered weights are exactly 1 on distances 1..6, 0 elsewhere.
        for (int d = 1; d <= 6; d++)
        {
            Assert.Equal(1.0, w[d], 10);
            Assert.Equal(1.0, w[N - d], 10);
        }
        for (int d = 7; d < N - 6; d++)
            Assert.Equal(0.0, w[d], 10);

        Assert.Equal(6, EdgeCount(w));                    // sparse: 6 distinct edges
        var (neg, _) = NegativeWeightStats(w);
        Assert.Equal(0, neg);                             // all couplings physical (attractive)

        // Degree c_0 = Σ_d w_d = 12 (the unweighted D96 degree).
        double degree = 0.0;
        for (int d = 1; d < N; d++) degree += w[d];
        Assert.Equal(12.0, degree, 8);
    }

    // ── 3. Physicality: which target spectra need dense / negative coupling ─

    [Fact]
    public void Y_T_001_PhysicalityByTarget()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var spectra = new (string Name, double[] Lambda)[]
        {
            ("D96", D96Spectrum()),
            ("band-gap", BandGapSpectrum()),
            ("clustered", ClusteredSpectrum()),
            ("octave", OctaveSpectrum()),
            ("max-separated", MaxSeparatedSpectrum()),
        };

        var d96W = ReconstructWeights(D96Spectrum());
        int d96Edges = EdgeCount(d96W);

        foreach (var (name, lam) in spectra)
        {
            double[] w = ReconstructWeights(lam);
            int edges = EdgeCount(w);
            var (neg, min) = NegativeWeightStats(w);

            if (name == "D96")
            {
                Assert.Equal(6, edges);
                Assert.Equal(0, neg);
            }
            else if (name is "band-gap" or "clustered")
            {
                // Discontinuous targets (steps/gaps) require dense coupling AND
                // negative (repulsive) couplings — not a simple oscillator material.
                Assert.True(edges > d96Edges, $"{name}: expected dense coupling, got {edges} edges");
                Assert.True(neg > 0, $"{name}: expected negative (repulsive) couplings");
                Assert.True(min < -Eps);
            }
        }
    }

    // ── 4. Robustness: the inverse map is an isometry (condition number 1) ───

    [Fact]
    public void Y_T_001_IsometryRobustness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        double[] lam0 = D96Spectrum();

        // Two perturbation magnitudes to demonstrate LINEARITY (no amplification).
        // δλ_k = ε·cos(2π·7·k/N) is symmetric AND zero-mean (Σ_k cos = 0), so the
        // degree term Δc₀ = (1/N)Σ_k δλ_k = 0 and the inverse map is a pure isometry.
        foreach (double eps in new[] { 1e-3, 1e-6 })
        {
            var deltaLambda = new double[N];
            for (int k = 0; k < N; k++)
                deltaLambda[k] = eps * Math.Cos(2.0 * Math.PI * 7.0 * k / N);

            // Reconstruct from the perturbed spectrum and from the baseline.
            double[] wPert = ReconstructWeights(lam0.Select((l, i) => l + deltaLambda[i]).ToArray());
            double[] wBase = ReconstructWeights(lam0);

            double normL = 0.0, normW = 0.0;
            for (int i = 0; i < N; i++)
            {
                normL += deltaLambda[i] * deltaLambda[i];
                double dw = wPert[i] - wBase[i];
                normW += dw * dw;
            }
            normL = Math.Sqrt(normL);
            normW = Math.Sqrt(normW);

            // Parseval: ||Δw||₂ = (1/√N) ||Δλ||₂ — a constant, independent of ε.
            double ratio = normW / normL;
            Assert.Equal(1.0 / Math.Sqrt(N), ratio, 9);
        }
    }

    // ── 5. Sparsity–accuracy tradeoff ───────────────────────────────────────

    [Fact]
    public void Y_T_001_SparsityAccuracyTradeoff()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // D96 is exactly recoverable from its 6 edges.
        double[] d96W = ReconstructWeights(D96Spectrum());
        double[] d96Top6 = TruncateToTopM(d96W, 6);
        double d96Err = Rms(D96Spectrum(), ForwardSpectrum(d96Top6));
        Assert.True(d96Err < 1e-9,
            $"D96 top-6 truncation should be exact, got RMS {d96Err:E3}");

        // A band-gap spectrum cannot be reproduced by 6 edges — it needs dense coupling.
        double[] bg = BandGapSpectrum();
        double[] bgW = ReconstructWeights(bg);
        double[] bgTop6 = TruncateToTopM(bgW, 6);
        double bgErr6 = Rms(bg, ForwardSpectrum(bgTop6));
        double bgErrFull = Rms(bg, ForwardSpectrum(bgW)); // ~0 (exact)
        Assert.True(bgErr6 > 100.0 * bgErrFull + 1e-6,
            $"band-gap top-6 error {bgErr6:E3} should be far above the exact error {bgErrFull:E3}");
    }

    // ── 6. Numerical consistency: closed-form spectrum == EVD of explicit L ──

    [Fact]
    public void Y_T_001_NumericalEigenvalueConsistency()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Explicit circulant Laplacian L of C96(±1..±6).
        var L = new double[N, N];
        for (int i = 0; i < N; i++)
            for (int d = 1; d <= 6; d++)
            {
                int jp = (i + d) % N;
                int jm = (i - d + N) % N;
                L[i, jp] -= 1.0;
                L[i, jm] -= 1.0;
                L[i, i] += 2.0;
            }

        var mat = Matrix<double>.Build.DenseOfArray(L);
        var evd = mat.Evd(Symmetricity.Symmetric);
        double[] evals = evd.EigenValues.Select(c => c.Real).OrderBy(x => x).ToArray();
        double[] closed = D96Spectrum().OrderBy(x => x).ToArray();

        double maxDiff = 0.0;
        for (int i = 0; i < N; i++) maxDiff = Math.Max(maxDiff, Math.Abs(evals[i] - closed[i]));
        Assert.True(maxDiff < 1e-8,
            $"closed-form DFT spectrum differs from numerical EVD by {maxDiff:E3}");
    }

    // ── Research report ─────────────────────────────────────────────────────

    [Fact]
    public void Y_T_001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_001 — Spectral Blueprint (Inverse Spectral Design)");

        sb.AppendLine("Question: can material properties be designed by specifying a TARGET");
        sb.AppendLine("          spectrum and reconstructing the coupling graph (Laplacian)?");
        sb.AppendLine();
        sb.AppendLine("Assumption (only AT primitive used): Laplacian <-> spectrum.");
        sb.AppendLine("Method: circulant (ring) Laplacian eigenvalues are the DFT of the");
        sb.AppendLine("        coupling row; the coupling row is the IDFT of the spectrum.");
        sb.AppendLine("        Inverse spectral design is therefore CLOSED FORM:");
        sb.AppendLine("        λ_k = Σ_d w_d (1 − cos 2πdk/N);  w_d = −(1/N) Σ_k λ_k cos 2πdk/N.");
        sb.AppendLine("Deterministic: no randomness, no fitted parameters, N = 96.");
        sb.AppendLine();

        var spectra = new (string Name, double[] Lambda)[]
        {
            ("D96 (canonical)", D96Spectrum()),
            ("band-gap", BandGapSpectrum()),
            ("clustered", ClusteredSpectrum()),
            ("octave-spaced", OctaveSpectrum()),
            ("max-separated", MaxSeparatedSpectrum()),
        };

        sb.AppendLine("[1] Inverse reconstruction results");
        sb.AppendLine($"    {"target",-15} {"RMS err",11} {"edges",6} {"neg.w",6} {"min.w",9} {"mass",10}");
        foreach (var (name, lam) in spectra)
        {
            double[] w = ReconstructWeights(lam);
            double[] rec = ForwardSpectrum(w);
            double err = Rms(lam, rec);
            int edges = EdgeCount(w);
            var (neg, min) = NegativeWeightStats(w);
            double mass = CouplingMass(w);
            sb.AppendLine($"    {name,-15} {err,11:E2} {edges,6} {neg,6} {min,9:F3} {mass,10:F2}");
        }
        sb.AppendLine();

        sb.AppendLine("[2] Key findings");
        sb.AppendLine("    (a) Round trip closes for ALL targets: inverse spectral design");
        sb.AppendLine("        reproduces the target spectrum to floating-point precision.");
        sb.AppendLine("    (b) The inverse map is an ISOMETRY (||Δw|| = ||Δλ||/√N, condition");
        sb.AppendLine("        number 1) — maximally robust to spectral perturbations.");
        sb.AppendLine("    (c) Physicality selects spectra: D96 (3 octave bands, span 6.4)");
        sb.AppendLine("        reconstructs to SPARSE all-ATTRACTIVE coupling (6 edges, no");
        sb.AppendLine("        negative weights); max-separated (uniform ramp) is also");
        sb.AppendLine("        all-attractive but needs 24 edges. Band-gap, clustered, and");
        sb.AppendLine("        extreme-octave (span ~3444) spectra require DENSE coupling with");
        sb.AppendLine("        NEGATIVE (repulsive) weights — exotic couplings a simple");
        sb.AppendLine("        oscillator material cannot provide.");
        sb.AppendLine("    (d) D96 is the canonical example: its spectrum reconstructs the");
        sb.AppendLine("        connection set {±1..±6} exactly — 6 edges, all weight 1, degree 12.");
        sb.AppendLine("    (e) Sparsity-accuracy: 6 edges reproduce D96 exactly; a band-gap");
        sb.AppendLine("        spectrum needs many more edges for any accuracy.");
        sb.AppendLine();

        sb.AppendLine("[3] Verdicts (DERIVED / EMERGENT / CORRESPONDENCE / REFUTED)");
        sb.AppendLine("    V1 inverse-map exactness (spectrum ↔ coupling via DFT)   → DERIVED");
        sb.AppendLine("       (closed-form identity for circulant graphs)");
        sb.AppendLine("    V2 isometry robustness (condition number 1)              → DERIVED");
        sb.AppendLine("       (Parseval / unitarity of the DFT)");
        sb.AppendLine("    V3 physicality constraint selects sparse band spectra     → CORRESPONDENCE");
        sb.AppendLine("       (designable spectra ↔ physically realizable ring materials)");
        sb.AppendLine("    V4 hypothesis 'target spectrum → low-complexity graph'    → SUPPORTED for");
        sb.AppendLine("       smooth, moderate-span spectra (D96 = 6 edges all-physical;");
        sb.AppendLine("       max-separated = 24 edges all-physical); REFUTED for discontinuous or");
        sb.AppendLine("       extreme-span spectra (band-gap/cluster/extreme-octave need dense +");
        sb.AppendLine("       repulsive coupling).");
        sb.AppendLine();

        sb.AppendLine("[4] Conclusion");
        sb.AppendLine("    Spectral-blueprint design is exact and optimally robust for circulant");
        sb.AppendLine("    (ring) topologies: the spectrum IS the Fourier transform of the coupling,");
        sb.AppendLine("    so target spectrum -> material topology is an inverse DFT. The finding");
        sb.AppendLine("    that AT's canonical D96 structure is the SPARSE, ALL-PHYSICAL solution");
        sb.AppendLine("    (6 edges, no repulsive coupling), while discontinuous or extreme-span");
        sb.AppendLine("    spectra demand repulsive couplings, ties the spectral blueprint");
        sb.AppendLine("    directly to the D96 attractor geometry. Inverse spectral design is");
        sb.AppendLine("    viable; physicality (non-negative coupling) is the binding constraint");
        sb.AppendLine("    on which spectra a real material can realize.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
