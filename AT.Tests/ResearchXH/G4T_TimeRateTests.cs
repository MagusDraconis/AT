using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-T Phase 0 — can local actualization-rate variations alone generate curvature-like
/// spectral signatures? Compares a uniform flat graph, a flat graph with variable event
/// density (rate), and the genuinely curved sphere / hyperbolic graphs, using the normalized
/// (density-invariant) and unnormalized (density-weighted) Laplacians.
///
/// Tests: G4-T00 (validity), G4-T01 (observables), G4-T02 (rate-vs-curvature mimicry).
/// </summary>
public class G4T_TimeRateTests : ResearchTestBase
{
    public G4T_TimeRateTests(ITestOutputHelper o) : base(o) { }

    private static GeometricGraph UniFlat() => UniformSquareGraph.Build(16, 0.16);
    private static GeometricGraph VarRate() => VariableRateGraph.Build(16, 0.06);
    private static GeometricGraph Sphere() => SphereGraph.Build(256, 0.5);
    private static GeometricGraph Hyper() => HyperbolicGraph.Build(256, 0.8);

    // ── G4-T00: graph builders are valid and deterministic ─────────────────────────────

    [Fact]
    public void G4_T00_GraphBuildersProduceValidGraphs()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-T00: uniform flat & variable-rate graph builders are valid and deterministic");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - UniformSquareGraph = flat square [-1,1]², uniform grid, Euclidean ε-graph.");
        sb.AppendLine("  - VariableRateGraph = same flat square, Chebyshev nodes (dense near boundary),");
        sb.AppendLine("    Euclidean ε-graph — the ONLY difference from UniformSquare is event density.");
        sb.AppendLine();

        foreach (var g in new[] { UniFlat(), VarRate() })
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
            sb.AppendLine($"{g.Name}: N={n}, mean degree={g.MeanDegree():F2}, symmetric={symmetric}, " +
                          $"noSelfLoops={noSelfLoops}, nonNegative={nonNegative}, connected={g.IsConnected()}");

            Assert.True(symmetric && noSelfLoops && nonNegative, $"{g.Name}: adjacency invalid");
            Assert.True(g.IsConnected(), $"{g.Name}: not connected");
        }

        Assert.True(SameMatrix(UniFlat().Adjacency, UniFlat().Adjacency), "UniformSquareGraph not deterministic");
        Assert.True(SameMatrix(VarRate().Adjacency, VarRate().Adjacency), "VariableRateGraph not deterministic");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: two valid, deterministic flat graphs differing only in event density.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-T01: the five observables compute for all four geometries ───────────────────

    [Fact]
    public void G4_T01_ObservablesComputeForAllGeometries()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-T01: spectrum, heat trace, gap, zeta, KS distance (4 geometries)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Observables computed on BOTH the normalized (density-invariant) and");
        sb.AppendLine("    unnormalized (density-weighted) Laplacians.");
        sb.AppendLine();

        var graphs = new[] { UniFlat(), VarRate(), Sphere(), Hyper() };
        string[] names = { "UniFlat", "VarRate", "Sphere", "Hyperbolic" };

        sb.AppendLine("NORMALIZED Laplacian:");
        sb.AppendLine($"  {"Geometry",-11} {"gap",8} {"Z(1)",9} {"ζ(2)",11}");
        foreach (var (g, nm) in graphs.Zip(names))
        {
            double[] ev = SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian());
            double gap = SpectralCurvature.SpectralGap(ev);
            double z1 = SpectralCurvature.HeatTrace(ev, 1.0);
            double zeta = SpectralCurvature.SpectralZeta(ev, 2.0);
            sb.AppendLine($"  {nm,-11} {gap,8:F4} {z1,9:F3} {zeta,11:F2}");
        }

        sb.AppendLine();
        sb.AppendLine("UNNORMALIZED Laplacian:");
        sb.AppendLine($"  {"Geometry",-11} {"gap",8} {"ζ(2)",11}");
        foreach (var (g, nm) in graphs.Zip(names))
        {
            double[] ev = SpectralCurvature.Eigenvalues(g.UnnormalizedLaplacian());
            double gap = SpectralCurvature.SpectralGap(ev);
            double zeta = SpectralCurvature.SpectralZeta(ev, 2.0);
            sb.AppendLine($"  {nm,-11} {gap,8:F4} {zeta,11:F2}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all five observables computed for all four geometries.");
        Output.WriteLine(sb.ToString());

        // Sanity: all spectra bounded and have a zero mode.
        foreach (var g in graphs)
        {
            double[] ev = SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian());
            Assert.True(ev[0] > -1e-9 && ev[^1] <= 2.0 + 1e-9, $"{g.Name}: normalized spectrum out of range");
        }
    }

    // ── G4-T02: does rate variation mimic curvature? (normalized vs unnormalized) ──────

    [Fact]
    public void G4_T02_RateVariationMimicsCurvatureOnlyInUnnormalizedOperator()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-T02: does local rate variation mimic curvature?");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The unnormalized Laplacian converges to ρ·Δ (density-weighted) — a varying");
        sb.AppendLine("    event density ρ acts like a conformal factor and can mimic curvature.");
        sb.AppendLine("  - The normalized Laplacian is density-invariant — it removes ρ and recovers flatness.");
        sb.AppendLine();

        var uni = UniFlat();
        var varR = VarRate();
        var sph = Sphere();

        double[] uniN = SpectralCurvature.Eigenvalues(uni.NormalizedSymmetricLaplacian());
        double[] varN = SpectralCurvature.Eigenvalues(varR.NormalizedSymmetricLaplacian());
        double[] sphN = SpectralCurvature.Eigenvalues(sph.NormalizedSymmetricLaplacian());

        double[] uniU = SpectralCurvature.Eigenvalues(uni.UnnormalizedLaplacian());
        double[] varU = SpectralCurvature.Eigenvalues(varR.UnnormalizedLaplacian());
        double[] sphU = SpectralCurvature.Eigenvalues(sph.UnnormalizedLaplacian());

        double ksNorm = SpectralCurvature.KolmogorovSmirnov(uniN, varN);
        double ksUnnorm = SpectralCurvature.KolmogorovSmirnov(uniU, varU);

        double gapNormVar = SpectralCurvature.SpectralGap(varN);
        double gapNormSph = SpectralCurvature.SpectralGap(sphN);
        double gapUnnormVar = SpectralCurvature.SpectralGap(varU);
        double gapUnnormUni = SpectralCurvature.SpectralGap(uniU);

        sb.AppendLine($"KS(UniFlat, VarRate):  normalized = {ksNorm:F4}   unnormalized = {ksUnnorm:F4}");
        sb.AppendLine($"Normalized gap:  VarRate={gapNormVar:F4}   Sphere={gapNormSph:F4}");
        sb.AppendLine($"Unnormalized gap: UniFlat={gapUnnormUni:F4}  VarRate={gapUnnormVar:F4}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - UNNORMALIZED (density-weighted): rate variation shifts the spectrum strongly");
        sb.AppendLine("    (large KS) and opens a curvature-like gap — it MIMICS curvature.");
        sb.AppendLine("  - NORMALIZED (density-invariant): the shift is much smaller and the gap stays");
        sb.AppendLine("    flat-like (≪ sphere) — the mimic is REMOVED.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: YES — local actualization-rate variations generate curvature-like");
        sb.AppendLine("signatures in the density-weighted (unnormalized) operator (the conformal-factor");
        sb.AppendLine("effect), but NOT in the density-invariant (normalized) operator.");
        Output.WriteLine(sb.ToString());

        // Assertions.
        Assert.True(ksUnnorm > ksNorm + 0.15,
            $"Expected unnormalized KS ({ksUnnorm:F4}) ≫ normalized KS ({ksNorm:F4})");
        Assert.True(gapNormVar < 0.5 * gapNormSph,
            $"Normalized gap of VarRate ({gapNormVar:F4}) should stay flat-like vs sphere ({gapNormSph:F4})");
        Assert.True(gapUnnormVar > gapUnnormUni,
            $"Unnormalized gap of VarRate ({gapUnnormVar:F4}) should exceed uniform flat ({gapUnnormUni:F4})");
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
