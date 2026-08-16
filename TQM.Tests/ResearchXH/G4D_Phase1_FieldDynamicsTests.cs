using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-D Phase 1 — local curvature fields. Moves from mean-field dynamics to spatial fields:
/// a local density ρ(x,t) generates a local curvature map R̂(x,t) via the diagonal heat kernel of
/// Lc = ρ⁻¹ L ρ⁻¹. Measures the local map, its propagation, and its stability, and compares the
/// field evolution against the mean-field (global score) evolution.
///
/// Tests: G4-D10 (local map), G4-D11 (propagation), G4-D12 (stability + field vs mean-field).
/// </summary>
public class G4D_Phase1_FieldDynamicsTests : ResearchTestBase
{
    public G4D_Phase1_FieldDynamicsTests(ITestOutputHelper o) : base(o) { }

    private const int N = 16;            // per-side grid (N² vertices)
    private const double Epsilon = 0.16;
    private const double HeatT = 0.5;    // heat-kernel time

    private static GeometricGraph Flat(int n = N) => CurvatureField.Build(Enumerable.Repeat(1.0, n).ToArray(), n, Epsilon);

    /// <summary>Gaussian-bump density profile ρ(x) = 1 + A·exp(−((x−x0)/σ)²).</summary>
    private static double[] Bump(double A, double x0, double sigma, int n = N)
    {
        var xs = CurvatureField.UniformXs(n);
        var rho = new double[n];
        for (int i = 0; i < n; i++)
        {
            double z = (xs[i] - x0) / sigma;
            rho[i] = 1.0 + A * Math.Exp(-z * z);
        }
        return rho;
    }

    private static double[] ReconstructX(double[] rhoX, int n = N)
        => CurvatureField.XProfile(CurvatureField.Reconstruct(Flat(n), CurvatureField.Build(rhoX, n, Epsilon), HeatT), n);

    // ── G4-D10: local curvature map ─────────────────────────────────────────────────────

    [Fact]
    public void G4_D10_LocalDensityGeneratesLocalCurvature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-D10: does a local density bump generate a local curvature map?");

        double[] rho = Bump(0.5, 0.0, 0.5);
        double[] rx = ReconstructX(rho);
        double[] rAnalytic = CurvatureField.AnalyticCurvature(rho, N);
        var xs = CurvatureField.UniformXs(N);

        sb.AppendLine($"{"x",7} {"ρ(x)",7} {"R̂(x)",9} {"R_analytic(x)",13}");
        for (int i = 0; i < N; i += 2)
            sb.AppendLine($"{xs[i],7:F2} {rho[i],7:F2} {rx[i],9:F4} {rAnalytic[i],13:F4}");

        double corr = CurvatureDynamics.Pearson(rx, rAnalytic);
        int cIdx = N / 2, tailIdx = 1;
        double centerSign = Math.Sign(rx[cIdx]);
        double analyticSign = Math.Sign(rAnalytic[cIdx]);

        sb.AppendLine();
        sb.AppendLine($"Pearson(R̂, R_analytic) = {corr:F4}");
        sb.AppendLine($"center sign: R̂({xs[cIdx]:F1}) = {rx[cIdx]:F4} ({centerSign}), analytic = {rAnalytic[cIdx]:F4} ({analyticSign})");
        sb.AppendLine($"localization: |R̂(center)| = {Math.Abs(rx[cIdx]):F4} vs |R̂(tail)| = {Math.Abs(rx[tailIdx]):F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: local ρ(x) generates a local R̂(x) that matches the analytic conformal curvature.");
        Output.WriteLine(sb.ToString());

        Assert.True(corr > 0.8, $"local map correlation {corr:F4} not > 0.8");
        Assert.Equal(analyticSign, centerSign);
        Assert.True(Math.Abs(rx[cIdx]) > Math.Abs(rx[tailIdx]), "curvature is not localized at the bump");
    }

    // ── G4-D11: propagation of the local curvature peak ─────────────────────────────────

    [Fact]
    public void G4_D11_CurvaturePeakPropagatesWithDensity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-D11: does the local curvature peak track a moving density bump?");

        var xs = CurvatureField.UniformXs(N);
        int T = 7;
        sb.AppendLine($"{"t",3} {"x0(t)",7} {"peak x",7}  track");
        var peaks = new List<double>();
        var x0s = new List<double>();
        bool track = true;
        for (int t = 0; t < T; t++)
        {
            double x0 = -0.6 + 1.2 * t / (T - 1.0);
            double[] rho = Bump(0.5, x0, 0.5);
            double[] rx = ReconstructX(rho);
            int peakIdx = 0;
            for (int i = 1; i < N; i++) if (Math.Abs(rx[i]) > Math.Abs(rx[peakIdx])) peakIdx = i;
            double peakX = xs[peakIdx];
            bool ok = Math.Abs(peakX - x0) <= (xs[1] - xs[0]) * 1.5; // within ~1.5 grid cells
            if (!ok) track = false;
            peaks.Add(peakX);
            x0s.Add(x0);
            sb.AppendLine($"{t,3} {x0,7:F2} {peakX,7:F2}  {ok}");
        }

        double corr = CurvatureDynamics.Pearson(peaks.ToArray(), x0s.ToArray());
        sb.AppendLine();
        sb.AppendLine($"peak location tracks x0(t): {track}; Pearson(peak, x0) = {corr:F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the local curvature peak propagates with the moving density perturbation.");
        Output.WriteLine(sb.ToString());

        Assert.True(track, "curvature peak does not track the moving bump");
        Assert.True(corr > 0.9, $"peak propagation correlation {corr:F4} not > 0.9");
    }

    // ── G4-D12: stability + field vs mean-field ─────────────────────────────────────────

    [Fact]
    public void G4_D12_FieldIsStableAndResolvesWhatMeanFieldMisses()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-D12: field stability + field vs mean-field");

        double[] rho = Bump(0.5, 0.0, 0.5);
        double[] rx = ReconstructX(rho);
        double[] rAnalytic = CurvatureField.AnalyticCurvature(rho, N);
        var geo = CurvatureField.Build(rho, N, Epsilon);
        double globalScore = CurvatureReconstruction.Score(Flat(), geo);

        int cIdx = N / 2;
        int fieldCenterSign = Math.Sign(rx[cIdx]);
        int analyticSign = Math.Sign(rAnalytic[cIdx]);
        int globalSign = Math.Sign(globalScore);
        double spread = rx.Max() - rx.Min();

        sb.AppendLine($"local R̂(center) = {rx[cIdx]:F4} (sign {fieldCenterSign}); analytic R(0) = {rAnalytic[cIdx]:F4} (sign {analyticSign})");
        sb.AppendLine($"global (mean-field) score = {globalScore:F4} (sign {globalSign})");
        sb.AppendLine($"field spatial spread (max−min) = {spread:F4}");
        sb.AppendLine();
        sb.AppendLine($"Field resolves the LOCAL sign correctly ({fieldCenterSign} == analytic {analyticSign}), while the");
        sb.AppendLine($"mean-field aggregate can misattribute it (global sign {globalSign} ≠ local sign {fieldCenterSign}) —");
        sb.AppendLine("demonstrating why field-level dynamics are needed beyond the Phase-0 mean field.");

        // Refinement stability: n = 16 → 20.
        sb.AppendLine();
        sb.AppendLine($"{"n",4} {"Pearson(R̂16,R̂n)",18} {"center R̂",10} {"tail R̂",10}");
        double[] rx16 = ReconstructX(Bump(0.5, 0.0, 0.5), 16);
        var refineRows = new List<(int n, double corr, double center, double tail)>();
        foreach (int n in new[] { 16, 20 })
        {
            double[] rn = ReconstructX(Bump(0.5, 0.0, 0.5, n), n);
            double corr = CurvatureDynamics.Pearson(rx16, Interpolate(rn, 16));
            int c = n / 2, tIdx = 1;
            refineRows.Add((n, corr, rn[c], rn[tIdx]));
            sb.AppendLine($"{n,4} {corr,18:F4} {rn[c],10:F4} {rn[tIdx],10:F4}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the field resolves local structure the mean field misses, and is refinement-stable.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(analyticSign, fieldCenterSign);
        Assert.NotEqual(globalSign, fieldCenterSign);
        Assert.True(spread > 1e-4, "local map has no spatial spread (degenerate)");
        Assert.True(refineRows[1].corr > 0.9, $"refinement correlation {refineRows[1].corr:F4} not > 0.9");
    }

    /// <summary>Linear interpolation of a length-m profile onto length-n samples.</summary>
    private static double[] Interpolate(double[] m, int n)
    {
        var r = new double[n];
        for (int i = 0; i < n; i++)
        {
            double u = (m.Length - 1) * i / (double)(n - 1);
            int lo = (int)Math.Floor(u), hi = Math.Min(m.Length - 1, lo + 1);
            double f = u - lo;
            r[i] = m[lo] * (1 - f) + m[hi] * f;
        }
        return r;
    }
}
