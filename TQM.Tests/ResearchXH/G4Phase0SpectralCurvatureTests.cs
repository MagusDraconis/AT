using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4 Phase 0 — does curvature information already live in graph spectra?
/// Builds three deterministic constant-curvature geometries (flat 2-torus, unit 2-sphere,
/// hyperbolic plane) and compares their Laplacian spectral signatures.
///
/// Tests: G4-00 (graph validity), G4-01 (observable consistency), G4-02 (distinguishability).
/// </summary>
public class G4Phase0SpectralCurvatureTests : ResearchTestBase
{
    public G4Phase0SpectralCurvatureTests(ITestOutputHelper o) : base(o) { }

    private const int FlatSide = 16;        // N = 256
    private const int CurvedN = 256;

    private static GeometricGraph Flat() => FlatGraph.Build(FlatSide);
    private static GeometricGraph Sphere() => SphereGraph.Build(CurvedN, 0.5);
    private static GeometricGraph Hyperbolic() => HyperbolicGraph.Build(CurvedN, 0.8);

    // ── G4-00: graph builders produce valid, deterministic geometric graphs ──────────

    [Fact]
    public void G4_00_GraphBuildersProduceValidGeometricGraphs()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-00: flat / sphere / hyperbolic graph builders are valid and deterministic");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Each geometry is a finite, unweighted, undirected graph.");
        sb.AppendLine("  - Flat = 2-torus grid; Sphere = Fibonacci S² + geodesic ε-graph;");
        sb.AppendLine("    Hyperbolic = Poincaré-disk rings + hyperbolic ε-graph.");
        sb.AppendLine("  - All constructions are closed-form (no randomness).");
        sb.AppendLine();

        var graphs = new[] { Flat(), Sphere(), Hyperbolic() };
        foreach (var g in graphs)
        {
            var a = g.Adjacency;
            int n = g.VertexCount;

            bool symmetric = true, noSelfLoops = true, nonNegative = true;
            for (int i = 0; i < n; i++)
            {
                if (a[i, i] != 0.0) noSelfLoops = false;
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] < 0.0) nonNegative = false;
                    if (a[i, j] != a[j, i]) symmetric = false;
                }
            }

            sb.AppendLine($"{g.Name}: N={n}, mean degree={g.MeanDegree():F2}, " +
                          $"symmetric={symmetric}, noSelfLoops={noSelfLoops}, nonNegative={nonNegative}, " +
                          $"connected={g.IsConnected()}");

            Assert.True(n > 0, $"{g.Name}: empty graph");
            Assert.True(symmetric, $"{g.Name}: adjacency not symmetric");
            Assert.True(noSelfLoops, $"{g.Name}: self-loops present");
            Assert.True(nonNegative, $"{g.Name}: negative entries present");
            Assert.True(g.IsConnected(), $"{g.Name}: graph is not connected");
        }

        // Determinism: rebuilding must reproduce the identical adjacency.
        var flat1 = Flat().Adjacency;
        var flat2 = Flat().Adjacency;
        Assert.True(SameMatrix(flat1, flat2), "FlatGraph is not deterministic");

        var sph1 = Sphere().Adjacency;
        var sph2 = Sphere().Adjacency;
        Assert.True(SameMatrix(sph1, sph2), "SphereGraph is not deterministic");

        var hyp1 = Hyperbolic().Adjacency;
        var hyp2 = Hyperbolic().Adjacency;
        Assert.True(SameMatrix(hyp1, hyp2), "HyperbolicGraph is not deterministic");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: three valid, connected, deterministic geometric graphs constructed.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-01: the four spectral observables are computable and consistent ────────────

    [Fact]
    public void G4_01_SpectralObservablesAreComputableAndConsistent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-01: eigenvalue spectrum, heat trace, spectral zeta, Weyl dimension");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Heat trace, spectral zeta and spectral gap are computed on the symmetric");
        sb.AppendLine("    NORMALIZED Laplacian L_sym (bounded in [0,2], density-invariant; G4 C2).");
        sb.AppendLine("  - The Weyl dimension is computed on the UNNORMALIZED Laplacian (which mirrors");
        sb.AppendLine("    the continuum −Δ) via the cumulative counting law N(λ) ∝ λ^(d/2).");
        sb.AppendLine();

        double[] ts = { 0.5, 1.0, 2.0 };
        double[] ss = { 2.0, 3.0 };

        var graphs = new[] { Flat(), Sphere(), Hyperbolic() };
        var rows = new List<(string Name, double[] Evals, double Min, double Max,
            double Gap, double Weyl, double[] Traces, double[] Zetas)>();

        foreach (var g in graphs)
        {
            double[] evals = SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian());
            double[] unnorm = SpectralCurvature.Eigenvalues(g.UnnormalizedLaplacian());
            double min = evals[0], max = evals[^1];
            double gap = SpectralCurvature.SpectralGap(evals);
            double weyl = SpectralCurvature.WeylDimension(unnorm);
            double[] traces = ts.Select(t => SpectralCurvature.HeatTrace(evals, t)).ToArray();
            double[] zetas = ss.Select(s => SpectralCurvature.SpectralZeta(evals, s)).ToArray();

            sb.AppendLine($"{g.Name}:");
            sb.AppendLine($"  N={evals.Length}, λ_min={min:E3}, λ_max={max:F4}, gap={gap:F4}, Weyl d={weyl:F3}");
            sb.AppendLine($"  Z(t) at t={{{string.Join(",", ts)}}} = [{string.Join(", ", traces.Select(v => v.ToString("F3")))}]");
            sb.AppendLine($"  ζ(s) at s={{{string.Join(",", ss)}}} = [{string.Join(", ", zetas.Select(v => v.ToString("F3")))}]");
            sb.AppendLine();

            rows.Add((g.Name, evals, min, max, gap, weyl, traces, zetas));
        }

        sb.AppendLine("CONCLUSION: all four observables are computable and each graph is internally");
        sb.AppendLine("consistent (zero mode, bounded spectrum, positive gap, Weyl d ≈ 2).");
        Output.WriteLine(sb.ToString());

        // Assertions (after the report so values are always visible).
        foreach (var r in rows)
        {
            Assert.True(r.Min > -1e-9 && r.Min < 1e-4, $"{r.Name}: min eigenvalue {r.Min:E3} not ≈ 0");
            Assert.True(r.Max <= 2.0 + 1e-9, $"{r.Name}: max eigenvalue {r.Max:F4} exceeds 2");
            Assert.True(r.Gap > 0.0, $"{r.Name}: connected graph should have a positive spectral gap");

            for (int k = 0; k < ts.Length; k++)
            {
                double z = r.Traces[k];
                Assert.True(z > 1.0 && z < r.Evals.Length, $"{r.Name}: Z({ts[k]})={z:F3} outside (1, N)");
            }
            for (int k = 0; k < ss.Length; k++)
            {
                Assert.True(r.Zetas[k] > 0.0 && double.IsFinite(r.Zetas[k]), $"{r.Name}: ζ({ss[k]}) invalid");
            }

            Assert.True(r.Weyl > 1.4 && r.Weyl < 2.6, $"{r.Name}: Weyl dimension {r.Weyl:F3} not ≈ 2");
        }
    }

    // ── G4-02: distinct geometries yield distinguishable spectral signatures ──────────

    [Fact]
    public void G4_02_DistinctGeometriesProduceDistinguishableSpectralSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-02: distinct geometries → statistically distinguishable spectral signatures");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Distinguishability is measured by the two-sample Kolmogorov–Smirnov distance");
        sb.AppendLine("    between normalized-Laplacian eigenvalue CDFs (scale-free).");
        sb.AppendLine("  - Weyl dimension is a CONTROL: all three are 2D, so any spectral difference");
        sb.AppendLine("    is curvature/geometry, not dimension.");
        sb.AppendLine();

        var graphs = new[] { Flat(), Sphere(), Hyperbolic() };
        double[][] spectra = graphs
            .Select(g => SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian()))
            .ToArray();
        double[][] unnormalized = graphs
            .Select(g => SpectralCurvature.Eigenvalues(g.UnnormalizedLaplacian()))
            .ToArray();

        string[] names = { "Flat", "Sphere", "Hyperbolic" };
        double[] weyl = new double[3], gap = new double[3];

        sb.AppendLine("SPECTRAL SIGNATURES (normalized Laplacian):");
        for (int i = 0; i < 3; i++)
        {
            gap[i] = SpectralCurvature.SpectralGap(spectra[i]);
            weyl[i] = SpectralCurvature.WeylDimension(unnormalized[i]);
            double z1 = SpectralCurvature.HeatTrace(spectra[i], 1.0);
            double zeta = SpectralCurvature.SpectralZeta(spectra[i], 2.0);
            sb.AppendLine($"  {names[i],-10} N={spectra[i].Length} gap={gap[i]:F4} Weyl d={weyl[i]:F3} " +
                          $"Z(1)={z1:F3} ζ(2)={zeta:F3}");
        }
        sb.AppendLine();

        // Pairwise KS distances between eigenvalue CDFs.
        double ksFS = SpectralCurvature.KolmogorovSmirnov(spectra[0], spectra[1]);
        double ksFH = SpectralCurvature.KolmogorovSmirnov(spectra[0], spectra[2]);
        double ksSH = SpectralCurvature.KolmogorovSmirnov(spectra[1], spectra[2]);

        sb.AppendLine("PAIRWISE KS DISTANCE (eigenvalue CDF):");
        sb.AppendLine($"  Flat vs Sphere     : {ksFS:F4}");
        sb.AppendLine($"  Flat vs Hyperbolic : {ksFH:F4}");
        sb.AppendLine($"  Sphere vs Hyperbolic: {ksSH:F4}");
        sb.AppendLine();
        sb.AppendLine($"  Gap ordering check: flat={gap[0]:F4} < sphere={gap[1]:F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: distinct constant-curvature geometries produce statistically");
        sb.AppendLine("distinguishable spectral signatures (KS distance, heat trace, zeta), while");
        sb.AppendLine("sharing dimension d ≈ 2. Curvature information IS encoded in graph spectra.");
        Output.WriteLine(sb.ToString());

        // Assertions (after the report so values are always visible).
        double minKS = Math.Min(ksFS, Math.Min(ksFH, ksSH));
        Assert.True(minKS > 0.05,
            $"Spectral signatures are not distinguishable (min KS = {minKS:F4})");

        for (int i = 0; i < 3; i++)
            Assert.True(weyl[i] > 1.4 && weyl[i] < 2.6, $"{names[i]}: Weyl d={weyl[i]:F3} not ≈ 2");

        Assert.True(gap[0] < gap[1], "Flat torus gap is not below the sphere gap");
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static bool SameMatrix(double[,] a, double[,] b)
    {
        if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1)) return false;
        for (int i = 0; i < a.GetLength(0); i++)
            for (int j = 0; j < a.GetLength(1); j++)
                if (a[i, j] != b[i, j]) return false;
        return true;
    }
}
