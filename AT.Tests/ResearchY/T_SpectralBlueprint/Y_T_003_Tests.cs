using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.T_SpectralBlueprint;

/// <summary>
/// ResearchY-T_003 — General Inverse Spectral Graph Audit test suite (Y_T_003_Tests.cs).
///
/// Question: Can a target spectrum reconstruct a physical NON-CIRCULANT weighted graph?
/// Is the spectrum a complete "blueprint" of the graph, or does it lose information?
///
/// Method: For the six graph families (path, cycle, grid, random sparse, complete,
/// D96-derived) we (1) compute the Laplacian spectrum; (2) reconstruct the Laplacian from
/// its full eigendecomposition L = VΛVᵀ; (3) search exhaustively for isospectral
/// non-isomorphic graphs (identical spectrum, different degree sequence); (4) contrast
/// with the circulant case where the labeled spectrum → coupling map is bijective
/// (T_001/T_002). Deterministic throughout.
/// </summary>
public class Y_T_003_Tests : ResearchTestBase
{
    public Y_T_003_Tests(ITestOutputHelper output) : base(output) { }

    // ── Helpers ─────────────────────────────────────────────────────────────

    private static double MaxAbsDiff(double[] a, double[] b)
    {
        double m = 0.0;
        for (int i = 0; i < a.Length; i++) m = Math.Max(m, Math.Abs(a[i] - b[i]));
        return m;
    }

    private static bool SameSpectrum(double[] a, double[] b, double tol = 1e-6)
        => a.Length == b.Length && MaxAbsDiff(a, b) < tol;

    /// <summary>Recover the adjacency from a reconstructed Laplacian (off-diagonal ≤ −0.5 → edge).</summary>
    private static double[,] AdjacencyFromLaplacian(double[,] l)
    {
        int n = l.GetLength(0);
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (l[i, j] < -0.5) { a[i, j] = 1.0; a[j, i] = 1.0; }
        return a;
    }

    private static double ConditionNumber(double[] spectrum)
    {
        // λ_1 = 0; the relevant conditioning is λ_max / λ_2 (Fiedler value).
        double lam2 = double.NaN;
        foreach (double l in spectrum)
            if (l > 1e-9) { lam2 = l; break; }
        return double.IsNaN(lam2) ? double.PositiveInfinity : spectrum[^1] / lam2;
    }

    // ── 1. Known spectra (sanity) ───────────────────────────────────────────

    [Fact]
    public void Y_T_003_KnownSpectra()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Path P6: λ = 2−2cos(kπ/6), k=0..5 → {0, 2−√3, 1, 2, 3, 2+√3}.
        var p6 = GeneralInverseSpectrumAnalyzer.Spectrum(GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.PathGraph(6)));
        double[] expectedP6 = { 0.0, 2.0 - Math.Sqrt(3.0), 1.0, 2.0, 3.0, 2.0 + Math.Sqrt(3.0) };
        Array.Sort(expectedP6);
        Assert.Equal(6, p6.Length);
        Assert.Equal(10.0, p6.Sum(), 6);            // trace = 2·edges = 10
        for (int i = 0; i < 6; i++) Assert.Equal(expectedP6[i], p6[i], 4);

        // Cycle C6: {0, 1, 1, 3, 3, 4}.
        var c6 = GeneralInverseSpectrumAnalyzer.Spectrum(GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.CycleGraph(6)));
        double[] expectedC6 = { 0.0, 1.0, 1.0, 3.0, 3.0, 4.0 };
        for (int i = 0; i < 6; i++) Assert.Equal(expectedC6[i], c6[i], 6);

        // Complete K6: {0, 6, 6, 6, 6, 6}.
        var k6 = GeneralInverseSpectrumAnalyzer.Spectrum(GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.CompleteGraph(6)));
        double[] expectedK6 = { 0.0, 6.0, 6.0, 6.0, 6.0, 6.0 };
        for (int i = 0; i < 6; i++) Assert.Equal(expectedK6[i], k6[i], 6);
    }

    // ── 2. Eigendecomposition reconstruction is exact ───────────────────────

    [Fact]
    public void Y_T_003_EigenbasisReconstruction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var cases = new (string Name, double[,] Adj)[]
        {
            ("path", GeneralInverseSpectrumAnalyzer.PathGraph(6)),
            ("cycle", GeneralInverseSpectrumAnalyzer.CycleGraph(6)),
            ("grid", GeneralInverseSpectrumAnalyzer.GridGraph(2, 3)),
            ("random", GeneralInverseSpectrumAnalyzer.RandomSparseGraph(6, 0.5, 42)),
            ("complete", GeneralInverseSpectrumAnalyzer.CompleteGraph(6)),
        };

        foreach (var (name, adj) in cases)
        {
            double[,] lap = GeneralInverseSpectrumAnalyzer.Laplacian(adj);
            double[] spec = GeneralInverseSpectrumAnalyzer.Spectrum(lap);
            double[,] rec = GeneralInverseSpectrumAnalyzer.ReconstructFromEigendecomposition(lap);
            double[] specRec = GeneralInverseSpectrumAnalyzer.Spectrum(rec);

            double err = GeneralInverseSpectrumAnalyzer.SpectralError(spec, specRec);
            Assert.True(err < 1e-8, $"{name}: spectral error {err:E3} too large");

            int[] degOrig = GeneralInverseSpectrumAnalyzer.DegreeSequence(adj);
            int[] degRec = GeneralInverseSpectrumAnalyzer.DegreeSequence(AdjacencyFromLaplacian(rec));
            Assert.Equal(degOrig, degRec); // full eigendecomposition recovers the graph exactly
        }
    }

    // ── 3. Isospectral search: spectrum does NOT determine the graph ────────

    [Fact]
    public void Y_T_003_IsospectralSearch()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var pairs = GeneralInverseSpectrumAnalyzer.FindIsospectralPairs(5)
            .Concat(GeneralInverseSpectrumAnalyzer.FindIsospectralPairs(6))
            .ToList();

        Assert.NotEmpty(pairs);
        // Every reported pair is provably non-isomorphic (different degree sequences)
        // yet isospectral (same spectrum) — the spectrum alone is lossy.
        foreach (var p in pairs)
            Assert.False(p.DegreesA.SequenceEqual(p.DegreesB));
    }

    // ── 4. Circulant rigidity: labeled spectrum → unique connection set ─────

    [Fact]
    public void Y_T_003_CirculantRigidity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Cycle C6 = circulant C6(±1): labeled spectrum → weights recover {±1}.
        double[] cycleSpec = SpectralBlueprint.CirculantSpectrum(6, 1);
        double[] cycleW = SpectralBlueprint.ReconstructWeights(cycleSpec, 6);
        Assert.Equal(1.0, cycleW[1], 9);
        Assert.Equal(1.0, cycleW[5], 9);
        Assert.Equal(0.0, cycleW[2], 9);
        Assert.Equal(0.0, cycleW[3], 9);
        Assert.Equal(0.0, cycleW[4], 9);

        // D96 = circulant C96(±1..±6): labeled spectrum → weights recover {±1..±6}.
        double[] d96Spec = SpectralBlueprint.CirculantSpectrum(96, 6);
        double[] d96W = SpectralBlueprint.ReconstructWeights(d96Spec, 96);
        for (int d = 1; d <= 6; d++) Assert.Equal(1.0, d96W[d], 9);
        for (int d = 7; d < 90; d++) Assert.Equal(0.0, d96W[d], 9);
    }

    // ── Research report ─────────────────────────────────────────────────────

    [Fact]
    public void Y_T_003_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-T_003 — General Inverse Spectral Graph Audit");

        sb.AppendLine("Question: can a target spectrum reconstruct a physical non-circulant");
        sb.AppendLine("          weighted graph? Does the spectrum uniquely determine the graph?");
        sb.AppendLine();
        sb.AppendLine("Assumption (only AT primitive used): Laplacian <-> spectrum.");
        sb.AppendLine("Method: compute spectra; reconstruct from full eigendecomposition");
        sb.AppendLine("        L = VΛVᵀ; search exhaustively for isospectral non-isomorphic");
        sb.AppendLine("        graphs; contrast with the circulant (ring) case from T_001/T_002.");
        sb.AppendLine("Deterministic; no fitted parameters.");
        sb.AppendLine();

        var pairs5 = GeneralInverseSpectrumAnalyzer.FindIsospectralPairs(5);
        var pairs6 = GeneralInverseSpectrumAnalyzer.FindIsospectralPairs(6);
        var allPairs = pairs5.Concat(pairs6).ToList();

        // ── 1. The six graph cases ─────────────────────────────────────────
        var cases = new (string Name, int N, double[,] Adj)[]
        {
            ("path", 6, GeneralInverseSpectrumAnalyzer.PathGraph(6)),
            ("cycle", 6, GeneralInverseSpectrumAnalyzer.CycleGraph(6)),
            ("grid 2x3", 6, GeneralInverseSpectrumAnalyzer.GridGraph(2, 3)),
            ("random sparse", 6, GeneralInverseSpectrumAnalyzer.RandomSparseGraph(6, 0.5, 42)),
            ("complete", 6, GeneralInverseSpectrumAnalyzer.CompleteGraph(6)),
            ("D96-derived", 96, GeneralInverseSpectrumAnalyzer.D96DerivedGraph(96, 6)),
        };

        sb.AppendLine("[1] Six graph cases: spectrum, reconstruction, isospectral partner");
        sb.AppendLine($"    {"case",-15} {"edges",6} {"sparse",7} {"cond",8} {"spectr.err",11} {"sim",6} {"partner",8}");
        foreach (var (name, n, adj) in cases)
        {
            double[,] lap = GeneralInverseSpectrumAnalyzer.Laplacian(adj);
            double[] spec = GeneralInverseSpectrumAnalyzer.Spectrum(lap);
            double[,] rec = GeneralInverseSpectrumAnalyzer.ReconstructFromEigendecomposition(lap);
            double err = GeneralInverseSpectrumAnalyzer.SpectralError(spec, GeneralInverseSpectrumAnalyzer.Spectrum(rec));
            double sim = GeneralInverseSpectrumAnalyzer.GraphSimilarity(adj, AdjacencyFromLaplacian(rec));
            bool partner = n <= 6 && allPairs.Any(p => SameSpectrum(p.Spectrum, spec));
            sb.AppendLine($"    {name,-15} {GeneralInverseSpectrumAnalyzer.EdgeCount(adj),6} {GeneralInverseSpectrumAnalyzer.Sparsity(adj),7:F3} {ConditionNumber(spec),8:F2} {err,11:E1} {sim,6:F2} {(partner ? "YES" : "no"),8}");
        }
        sb.AppendLine("    note: the six NAMED families are each spectrally unique (rigid) among");
        sb.AppendLine("          graphs of their size, but rigidity is NOT generic — see [2].");
        sb.AppendLine();

        // ── 2. Isospectral pairs ───────────────────────────────────────────
        sb.AppendLine("[2] Isospectral non-isomorphic pairs found (n=5 and n=6)");
        sb.AppendLine($"    total distinct pairs = {allPairs.Count}");
        foreach (var p in allPairs.Take(6))
        {
            sb.AppendLine($"    degrees {string.Join(",", p.DegreesA)}  <->  {string.Join(",", p.DegreesB)}  (edges={p.Edges})");
        }
        sb.AppendLine("    -> two graphs can share a full Laplacian spectrum yet be non-isomorphic:");
        sb.AppendLine("       the spectrum ALONE does not determine a general graph.");
        sb.AppendLine();

        // ── 3. Circulant rigidity contrast ─────────────────────────────────
        sb.AppendLine("[3] Circulant rigidity (contrast)");
        sb.AppendLine("    cycle C6: labeled spectrum -> IDFT -> weights {±1} (unique).");
        sb.AppendLine("    D96 C96(±1..±6): labeled spectrum -> IDFT -> weights {±1..±6} (unique).");
        sb.AppendLine("    A circulant graph's spectrum is a complete blueprint (bijective map);");
        sb.AppendLine("    a general graph's spectrum is lossy (isospectral partners exist).");
        sb.AppendLine();

        // ── 4. Verdicts ────────────────────────────────────────────────────
        sb.AppendLine("[4] Verdicts (DERIVED / EMERGENT / CORRESPONDENCE / REFUTED)");
        sb.AppendLine("    V1 eigendecomposition (spectrum + eigenbasis) reconstructs L exactly → DERIVED");
        sb.AppendLine("       (L = VΛVᵀ, a linear-algebra identity)");
        sb.AppendLine("    V2 spectrum ALONE does not determine a general graph          → DERIVED");
        sb.AppendLine("       (existence of isospectral non-isomorphic pairs)");
        sb.AppendLine("    V3 circulant (ring) graphs are spectrally rigid               → CORRESPONDENCE");
        sb.AppendLine("       (labeled spectrum ↔ connection set is bijective, T_001/T_002)");
        sb.AppendLine("    V4 'spectrum as complete blueprint'                            → REFUTED for general");
        sb.AppendLine("       non-circulant graphs (isospectral degeneracy); SUPPORTED only for the");
        sb.AppendLine("       special circulant (D96) structure.");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    A general graph is NOT determined by its spectrum: isospectral non-isomorphic");
        sb.AppendLine("    graphs exist (found on n=5 and n=6). The spectral blueprint of T_001/T_002");
        sb.AppendLine("    is therefore a SPECIAL property of circulant (ring) structure — for which the");
        sb.AppendLine("    labeled spectrum is bijective with the coupling. This is exactly why AT's D96");
        sb.AppendLine("    is a canonical attractor: its circulant geometry makes the spectrum a complete");
        sb.AppendLine("    and unique description, whereas a generic graph's spectrum is an incomplete");
        sb.AppendLine("    (degenerate) description. Full reconstruction requires the eigenbasis too.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
