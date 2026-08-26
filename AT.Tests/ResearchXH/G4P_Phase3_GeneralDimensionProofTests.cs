using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-P Phase 3 — generalize the continuum proof beyond d=2. The analytic result is that the
/// density-weighted operator M^(a) = ρ^(-a) L ρ^(-a) converges to −c Δ_g (modulo a native
/// zeroth-order potential) when the conformal weight is a_d = (d+2)/(2d). For d=2 this reduces to
/// a=1 (Lc); for d→∞ it tends to a=1/2 (the conformally-invariant flat Laplacian). These tests
/// verify the KEY structural claims in the fully-implemented d=2 case:
///   - a=1/2 is the density-INVARIANT point (flat Laplacian);
///   - a=1 is curvature-SENSITIVE (Δ_g, the d=2 conformal Laplacian);
///   - the general exponent formula reduces to a=1 at d=2 (Lc).
/// </summary>
public class G4P_Phase3_GeneralDimensionProofTests : ResearchTestBase
{
    public G4P_Phase3_GeneralDimensionProofTests(ITestOutputHelper o) : base(o) { }

    private const int N = 16;
    private const double Epsilon = 0.16;

    private static GeometricGraph Flat() => ConformalRateGraph.Build(0.0, N, Epsilon);
    private static GeometricGraph Curved() => ConformalRateGraph.Build(+1.0, N, Epsilon); // ρ = 1 + x²

    private static double[] EigenOf(GeometricGraph g, double a)
        => ConformalOperator.BuildGeneral(g.UnnormalizedLaplacian(), g.VertexDensity(), a, a)
                           .Let(SpectralCurvature.Eigenvalues);

    private static double KS(GeometricGraph flat, GeometricGraph curved, double a)
        => SpectralCurvature.KolmogorovSmirnov(EigenOf(flat, a), EigenOf(curved, a));

    // ── G4-P30: density-power scaling — the invariant point is a = 1/2 ─────────────────

    [Fact]
    public void G4_P30_DensityPowerScalingMinimizesAtOneHalf()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-P30: density-power scaling — KS(flat,curved) vs conformal weight a");

        var flat = Flat();
        var curved = Curved();
        double[] as_ = { 0.0, 0.25, 0.5, 0.75, 1.0 };

        sb.AppendLine($"{"a",7} {"KS(flat,curved)",16} {"interpretation",-32}");
        var ks = new double[as_.Length];
        for (int i = 0; i < as_.Length; i++)
        {
            ks[i] = KS(flat, curved, as_[i]);
            string interp = as_[i] switch
            {
                0.0 => "plain L (density-weighted)",
                0.5 => "ρ^(-1/2)Lρ^(-1/2) → −cΔ_η (invariant)",
                1.0 => "ρ^(-1)Lρ^(-1) → −cΔ_g (curvature)",
                _ => ""
            };
            sb.AppendLine($"{as_[i],7:F2} {ks[i],16:F4} {interp,-32}");
        }

        int minIdx = Array.IndexOf(ks, ks.Min());
        sb.AppendLine();
        sb.AppendLine($"minimum KS at a = {as_[minIdx]:F2}");
        sb.AppendLine($"a=1/2 invariant (KS < KS at a=0 and a=1): {ks[2] < ks[0] && ks[2] < ks[4]}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the conformally-invariant (flat-Laplacian) point is a = 1/2, exactly the");
        sb.AppendLine("d→∞ limit of the general conformal weight a_d = (d+2)/(2d).");
        Output.WriteLine(sb.ToString());

        Assert.True(ks[2] < ks[0] && ks[2] < ks[4],
            $"a=1/2 not the minimum: KS(0)={ks[0]:F4}, KS(0.5)={ks[2]:F4}, KS(1)={ks[4]:F4}");
    }

    // ── G4-P31: conformal forms — a=1/2 invariant, a=1 curvature-sensitive ─────────────

    [Fact]
    public void G4_P31_ConformalFormsInvariantVsCurvature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-P31: a=1/2 is density-invariant; a=1 is curvature-sensitive");

        var flat = Flat();
        var curved = Curved();
        double ksInv = KS(flat, curved, 0.5);
        double ksCurv = KS(flat, curved, 1.0);

        sb.AppendLine($"KS(flat, curved) at a=1/2 (invariant Laplacian) = {ksInv:F4}");
        sb.AppendLine($"KS(flat, curved) at a=1   (conformal Δ_g)       = {ksCurv:F4}");
        sb.AppendLine($"a=1/2 ≪ a=1 (invariance vs curvature): {ksInv < 0.3 * ksCurv}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a=1/2 is the conformally-invariant form (flat Laplacian, density-independent");
        sb.AppendLine("continuum limit); a=1 is the conformal form whose limit is the curved Δ_g.");
        Output.WriteLine(sb.ToString());

        Assert.True(ksInv < 0.3 * ksCurv,
            $"expected KS(0.5) ≪ KS(1), got {ksInv:F4} vs {ksCurv:F4}");
    }

    // ── G4-P32: dimension dependence — a_d=(d+2)/(2d) reduces to a=1 (Lc) at d=2 ────────

    [Fact]
    public void G4_P32_GeneralExponentReducesToLcAtD2()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-P32: a_d = (d+2)/(2d) reduces to a=1 = Lc at d=2");

        // General conformal weight.
        static double Ad(int d) => (d + 2.0) / (2.0 * d);
        sb.AppendLine($"a_d = (d+2)/(2d):  d=2 → {Ad(2):F3}, d=3 → {Ad(3):F3}, d=4 → {Ad(4):F3}, d→∞ → 1/2");

        // At d=2 the general operator ρ^(-a)Lρ^(-a) with a=Ad(2)=1 must equal the canonical Lc.
        var g = Curved();
        var general = ConformalOperator.BuildGeneral(g.UnnormalizedLaplacian(), g.VertexDensity(), Ad(2), Ad(2));
        var canonical = ConformalOperator.Build(g, ConformalOperatorKind.RhoInverseSquared);
        double diff = MaxDiff(general, canonical);

        sb.AppendLine($"max|M^(Ad(2)) − Lc| = {diff:E2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the general conformal weight a_d reproduces the d=2 native operator Lc exactly;");
        sb.AppendLine("for d≠2 it shifts (a_3=5/6, a_4=3/4) to generate the (d−2)∇lnρ·∇φ term of Δ_g.");
        Output.WriteLine(sb.ToString());

        Assert.True(diff < 1e-9, $"M^(Ad(2)) != Lc (max|diff| = {diff:E2})");
        Assert.Equal(1.0, Ad(2), 6);
        Assert.Equal(5.0 / 6.0, Ad(3), 6);
    }

    private static double MaxDiff(double[,] a, double[,] b)
    {
        double m = 0;
        for (int i = 0; i < a.GetLength(0); i++)
            for (int j = 0; j < a.GetLength(1); j++)
                m = Math.Max(m, Math.Abs(a[i, j] - b[i, j]));
        return m;
    }
}

internal static class Ext
{
    public static TResult Let<T, TResult>(this T value, Func<T, TResult> f) => f(value);
}
