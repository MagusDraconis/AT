using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-T Phase 1 — do actualization-rate gradients generate effective conformal geometry?
/// Chain: ρ(x) → f(x)=ρ^(2/d) → conformally-flat metric g=f·η → curvature R=−(2/f)(ln ρ)''.
/// Compares rate-induced spectral response to true curvature (sphere / hyperbolic).
///
/// Tests: G4-T1-00 (conformal curvature + builder), G4-T1-01 (observables),
/// G4-T1-02 (rate-induced vs true curvature).
/// </summary>
public class G4T_Phase1_ConformalActualizationTests : ResearchTestBase
{
    public G4T_Phase1_ConformalActualizationTests(ITestOutputHelper o) : base(o) { }

    private static GeometricGraph Flat() => UniformSquareGraph.Build(16, 0.16);
    private static GeometricGraph RatePositive() => ConformalRateGraph.Build(+1.0, 16, 0.16);   // R(0) < 0
    private static GeometricGraph RateNegative() => ConformalRateGraph.Build(-0.8, 16, 0.16);    // R(0) > 0
    private static GeometricGraph Sphere() => SphereGraph.Build(256, 0.5);
    private static GeometricGraph Hyper() => HyperbolicGraph.Build(256, 0.8);

    // ── G4-T1-00: conformal curvature is analytic and the builder is valid ──────────────

    [Fact]
    public void G4_T1_00_ConformalCurvatureAndBuilderValid()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-T1-00: ρ(x)=1+a·x² → f=ρ → R=−(2/f)(ln ρ)'' , builder valid");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - d=2 ⇒ conformal factor f = ρ^(2/d) = ρ.");
        sb.AppendLine("  - Conformally-flat g = f·η has R = −(2/f)·(ln ρ)'' = −4a(1−a x²)/(1+a x²)³.");
        sb.AppendLine("  - R(0) = −4a: a>0 ⇒ R<0, a<0 ⇒ R>0.");
        sb.AppendLine();

        sb.AppendLine($"R(0) for a=+1.0 : {ConformalRateGraph.ConformalCurvature(+1.0, 0.0):F3} (negative)");
        sb.AppendLine($"R(0) for a=−0.8 : {ConformalRateGraph.ConformalCurvature(-0.8, 0.0):F3} (positive)");
        sb.AppendLine();

        Assert.Equal(-4.0, ConformalRateGraph.ConformalCurvature(1.0, 0.0), 6);
        Assert.Equal(+3.2, ConformalRateGraph.ConformalCurvature(-0.8, 0.0), 6);

        foreach (var g in new[] { RatePositive(), RateNegative() })
        {
            var a = g.Adjacency;
            bool symmetric = true, noSelfLoops = true;
            for (int i = 0; i < g.VertexCount; i++)
            {
                if (a[i, i] != 0.0) noSelfLoops = false;
                for (int j = 0; j < g.VertexCount; j++)
                    if (a[i, j] != a[j, i]) symmetric = false;
            }
            sb.AppendLine($"{g.Name}: N={g.VertexCount}, deg={g.MeanDegree():F2}, " +
                          $"symmetric={symmetric}, connected={g.IsConnected()}");
            Assert.True(symmetric && noSelfLoops && g.IsConnected(), $"{g.Name}: invalid");
        }

        Assert.True(SameMatrix(RatePositive().Adjacency, RatePositive().Adjacency), "rate graph not deterministic");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: conformal curvature analytic; rate-gradient graphs valid and deterministic.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-T1-01: spectral observables for rate gradients vs true curvature ─────────────

    [Fact]
    public void G4_T1_01_ObservablesForRateGradientsAndCurvature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-T1-01: heat trace, zeta, gap — rate gradients vs true curvature");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - UNNORMALIZED Laplacian is density-weighted (L→ρ·Δ): the native conformal-factor carrier.");
        sb.AppendLine("  - NORMALIZED Laplacian is density-invariant (removes ρ).");
        sb.AppendLine();

        var graphs = new[] { Flat(), RatePositive(), RateNegative(), Sphere(), Hyper() };
        string[] names = { "Flat", "Rate(ρ=1+x²) R<0", "Rate(ρ=1−0.8x²) R>0", "Sphere", "Hyperbolic" };

        sb.AppendLine("UNNORMALIZED Laplacian:");
        sb.AppendLine($"  {"Geometry",-20} {"gap",8} {"ζ(2)",10} {"KS→flat",8}");
        double[] flatU = SpectralCurvature.Eigenvalues(Flat().UnnormalizedLaplacian());
        foreach (var (g, nm) in graphs.Zip(names))
        {
            double[] ev = SpectralCurvature.Eigenvalues(g.UnnormalizedLaplacian());
            double gap = SpectralCurvature.SpectralGap(ev);
            double zeta = SpectralCurvature.SpectralZeta(ev, 2.0);
            double ks = SpectralCurvature.KolmogorovSmirnov(flatU, ev);
            sb.AppendLine($"  {nm,-20} {gap,8:F4} {zeta,10:F2} {ks,8:F3}");
        }

        sb.AppendLine();
        sb.AppendLine("NORMALIZED Laplacian:");
        sb.AppendLine($"  {"Geometry",-20} {"gap",8} {"ζ(2)",10} {"KS→flat",8}");
        double[] flatN = SpectralCurvature.Eigenvalues(Flat().NormalizedSymmetricLaplacian());
        foreach (var (g, nm) in graphs.Zip(names))
        {
            double[] ev = SpectralCurvature.Eigenvalues(g.NormalizedSymmetricLaplacian());
            double gap = SpectralCurvature.SpectralGap(ev);
            double zeta = SpectralCurvature.SpectralZeta(ev, 2.0);
            double ks = SpectralCurvature.KolmogorovSmirnov(flatN, ev);
            sb.AppendLine($"  {nm,-20} {gap,8:F4} {zeta,10:F2} {ks,8:F3}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: observables computed for rate gradients and true curvature.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-T1-02: rate-induced response is magnitude-dominated, normalized removes it ───

    [Fact]
    public void G4_T1_02_RateInducedResponseIsMagnitudeDominated()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-T1-02: does the rate-induced signal track the conformal curvature SIGN?");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - RatePositive has R(0)=−4 (negative); RateNegative has R(0)=+3.2 (positive).");
        sb.AppendLine("  - If the spectral response tracked the conformal sign, the two should move OPPOSITELY.");
        sb.AppendLine();

        double[] flatU = SpectralCurvature.Eigenvalues(Flat().UnnormalizedLaplacian());
        double[] flatN = SpectralCurvature.Eigenvalues(Flat().NormalizedSymmetricLaplacian());

        double[] posU = SpectralCurvature.Eigenvalues(RatePositive().UnnormalizedLaplacian());
        double[] negU = SpectralCurvature.Eigenvalues(RateNegative().UnnormalizedLaplacian());
        double[] posN = SpectralCurvature.Eigenvalues(RatePositive().NormalizedSymmetricLaplacian());
        double[] negN = SpectralCurvature.Eigenvalues(RateNegative().NormalizedSymmetricLaplacian());

        double ksPosU = SpectralCurvature.KolmogorovSmirnov(flatU, posU);
        double ksNegU = SpectralCurvature.KolmogorovSmirnov(flatU, negU);
        double ksPosN = SpectralCurvature.KolmogorovSmirnov(flatN, posN);
        double ksNegN = SpectralCurvature.KolmogorovSmirnov(flatN, negN);

        double zFlatU = SpectralCurvature.SpectralZeta(flatU, 2.0);
        double zPosU = SpectralCurvature.SpectralZeta(posU, 2.0);
        double zNegU = SpectralCurvature.SpectralZeta(negU, 2.0);

        sb.AppendLine($"Unnormalized KS→flat:  R<0 (ρ=1+x²)={ksPosU:F3}   R>0 (ρ=1−0.8x²)={ksNegU:F3}");
        sb.AppendLine($"Normalized   KS→flat:  R<0={ksPosN:F3}   R>0={ksNegN:F3}");
        sb.AppendLine($"Unnormalized ζ(2): flat={zFlatU:F2}, R<0={zPosU:F2}, R>0={zNegU:F2}");
        sb.AppendLine();
        sb.AppendLine("FINDING: BOTH R<0 and R>0 gradients shift the unnormalized ζ(2) DOWNWARD (magnitude");
        sb.AppendLine("response), i.e. the sign of the conformal curvature is NOT read by the graph Laplacian.");
        sb.AppendLine("The normalized Laplacian reduces the shift (density-invariance), but neither operator");
        sb.AppendLine("cleanly encodes the conformal-curvature SIGN — that requires the conformal operator");
        sb.AppendLine("Δ_g = ρ⁻¹Δ_η = L/ρ², not the plain graph Laplacian.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: rate gradients DO define conformal geometry (R≠0), but the graph Laplacian");
        sb.AppendLine("response is density-magnitude-dominated, not a genuine curvature-sign detector.");
        Output.WriteLine(sb.ToString());

        // Assertions.
        Assert.True(ksPosU > 0.15 && ksNegU > 0.15,
            "Rate gradients should strongly shift the unnormalized spectrum");
        Assert.True(ksPosN < ksPosU && ksNegN < ksNegU,
            "Normalized Laplacian should reduce the rate-induced shift");
        Assert.True(zPosU < zFlatU && zNegU < zFlatU,
            "Both signs of conformal curvature shift unnormalized ζ(2) downward (magnitude, not sign)");
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
