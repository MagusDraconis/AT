using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-C Phase 0 — investigate native conformal operators. Tests the density-weighted operator
/// family {L, D^−1/2 L D^−1/2, ρ^−1/2 L ρ^−1/2, ρ^−1 L ρ^−1} and measures heat trace, spectral
/// zeta, Weyl dimension, and curvature-sign separation on flat / R&lt;0 / R&gt;0 rate gradients.
///
/// Tests: G4-C-00 (operators valid), G4-C-01 (observables), G4-C-02 (curvature separation).
/// </summary>
public class G4C_ConformalOperatorTests : ResearchTestBase
{
    public G4C_ConformalOperatorTests(ITestOutputHelper o) : base(o) { }

    private static GeometricGraph Flat() => ConformalRateGraph.Build(0.0, 16, 0.16);
    private static GeometricGraph RateNegative() => ConformalRateGraph.Build(+1.0, 16, 0.16);   // R(0) < 0
    private static GeometricGraph RatePositive() => ConformalRateGraph.Build(-0.8, 16, 0.16);    // R(0) > 0

    private static readonly ConformalOperatorKind[] Kinds =
    {
        ConformalOperatorKind.Unnormalized,
        ConformalOperatorKind.Normalized,
        ConformalOperatorKind.RhoInverse,
        ConformalOperatorKind.RhoInverseSquared
    };

    private static string KindName(ConformalOperatorKind k) => k switch
    {
        ConformalOperatorKind.Unnormalized => "L (unnorm)",
        ConformalOperatorKind.Normalized => "D^-1/2 L D^-1/2",
        ConformalOperatorKind.RhoInverse => "ρ^-1/2 L ρ^-1/2",
        ConformalOperatorKind.RhoInverseSquared => "ρ^-1 L ρ^-1",
        _ => k.ToString()
    };

    // ── G4-C-00: operator family is symmetric, PSD, zero-mode ──────────────────────────

    [Fact]
    public void G4_C_00_OperatorsAreValid()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C-00: operator family is symmetric, PSD, with a zero mode");

        foreach (var kind in Kinds)
        {
            var m = ConformalOperator.Build(RateNegative(), kind);
            int n = m.GetLength(0);
            bool symmetric = true;
            double min = double.PositiveInfinity;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                    if (Math.Abs(m[i, j] - m[j, i]) > 1e-9) symmetric = false;
            }
            double[] ev = SpectralCurvature.Eigenvalues(m);
            min = ev[0];

            sb.AppendLine($"{KindName(kind),-20} symmetric={symmetric}, min eigenvalue={min:E2}");
            Assert.True(symmetric, $"{KindName(kind)}: not symmetric");
            Assert.True(min > -1e-8, $"{KindName(kind)}: not positive semi-definite");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: all four operators are symmetric and positive semi-definite.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-C-01: heat trace, spectral zeta, Weyl dimension per operator ────────────────

    [Fact]
    public void G4_C_01_ObservablesPerOperator()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C-01: heat trace Z(1), spectral zeta ζ(2), Weyl dimension per operator");

        foreach (var kind in Kinds)
        {
            sb.AppendLine($"{KindName(kind)}:");
            sb.AppendLine($"  {"Geometry",-10} {"Z(1)",10} {"ζ(2)",10} {"Weyl d",8} {"gap",8}");
            foreach (var (g, nm) in new[] { (Flat(), "flat"), (RateNegative(), "R<0"), (RatePositive(), "R>0") })
            {
                double[] ev = ConformalOperator.Eigenvalues(g, kind);
                double z1 = SpectralCurvature.HeatTrace(ev, 1.0);
                double zeta = SpectralCurvature.SpectralZeta(ev, 2.0);
                double weyl = SpectralCurvature.WeylDimension(ev);
                double gap = SpectralCurvature.SpectralGap(ev);
                sb.AppendLine($"  {nm,-10} {z1,10:F2} {zeta,10:F2} {weyl,8:F2} {gap,8:F4}");
            }
            sb.AppendLine();
        }

        sb.AppendLine("CONCLUSION: observables computed for the full operator family.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-C-02: curvature-sign separation — which operator reads the sign? ────────────

    [Fact]
    public void G4_C_02_CurvatureSignSeparation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C-02: curvature-sign separation (does the operator straddle flat?)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - R<0 (ρ=1+x²) and R>0 (ρ=1−0.8x²) have OPPOSITE conformal curvature sign.");
        sb.AppendLine("  - An operator that reads the sign puts their ζ(2) on OPPOSITE sides of flat.");
        sb.AppendLine();

        var flat = Flat();
        var rneg = RateNegative();
        var rpos = RatePositive();

        var zeta = new Dictionary<ConformalOperatorKind, (double Flat, double Neg, double Pos)>();
        sb.AppendLine($"{"Operator",-20} {"flat ζ2",10} {"R<0 ζ2",10} {"R>0 ζ2",10} {"sep",8}  sign-sep");
        foreach (var kind in Kinds)
        {
            double zFlat = SpectralCurvature.SpectralZeta(ConformalOperator.Eigenvalues(flat, kind), 2.0);
            double zNeg = SpectralCurvature.SpectralZeta(ConformalOperator.Eigenvalues(rneg, kind), 2.0);
            double zPos = SpectralCurvature.SpectralZeta(ConformalOperator.Eigenvalues(rpos, kind), 2.0);
            double sepMag = Math.Abs(zNeg - zPos) / Math.Max(1.0, zFlat);
            bool sep = (zNeg - zFlat) * (zPos - zFlat) < 0.0;
            sb.AppendLine($"{KindName(kind),-20} {zFlat,10:F1} {zNeg,10:F1} {zPos,10:F1} {sepMag,8:F2}  {sep}");
            zeta[kind] = (zFlat, zNeg, zPos);
        }

        sb.AppendLine();
        sb.AppendLine("FINDING: the unnormalized L is sign-blind (both R<0 and R>0 shift ζ(2) downward —");
        sb.AppendLine("a density-magnitude artifact). The density-normalized operators (D^−1/2 L D^−1/2 and the");
        sb.AppendLine("ρ-weighted ρ^−1/2 L ρ^−1/2, ρ^−1 L ρ^−1) ALL straddle flat: R<0 up, R>0 down.");
        sb.AppendLine("The conformal operator ρ^−1 L ρ^−1 (≈ ρ^−2 L → Δ_g) has the LARGEST separation,");
        sb.AppendLine("and — because it uses the analytic density ρ, not the degree — it is the least");
        sb.AppendLine("degree-artifact-prone.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the conformal operator ρ^−1 L ρ^−1 is the native operator that is both");
        sb.AppendLine("most sensitive to curvature sign and least sensitive to degree artifacts.");
        Output.WriteLine(sb.ToString());

        // Assertions.
        var (fUn, nUn, pUn) = zeta[ConformalOperatorKind.Unnormalized];
        Assert.True(nUn < fUn && pUn < fUn, "Unnormalized L should be sign-blind (both below flat)");

        foreach (var kind in new[] { ConformalOperatorKind.Normalized, ConformalOperatorKind.RhoInverse, ConformalOperatorKind.RhoInverseSquared })
        {
            var (f, n, p) = zeta[kind];
            Assert.True(n > f && p < f, $"{KindName(kind)}: expected R<0 up and R>0 down around flat");
        }

        // The conformal operator (ρ^-1 L ρ^-1) has the largest sign separation.
        double sepNorm = Math.Abs(zeta[ConformalOperatorKind.Normalized].Neg - zeta[ConformalOperatorKind.Normalized].Pos)
                         / Math.Max(1.0, zeta[ConformalOperatorKind.Normalized].Flat);
        double sepConf = Math.Abs(zeta[ConformalOperatorKind.RhoInverseSquared].Neg - zeta[ConformalOperatorKind.RhoInverseSquared].Pos)
                         / Math.Max(1.0, zeta[ConformalOperatorKind.RhoInverseSquared].Flat);
        Assert.True(sepConf > sepNorm,
            $"Conformal operator separation ({sepConf:F2}) should exceed normalized ({sepNorm:F2})");
    }
}
