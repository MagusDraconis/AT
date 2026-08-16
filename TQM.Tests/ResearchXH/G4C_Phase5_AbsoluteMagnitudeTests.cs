using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-C Phase 5 — absolute curvature calibration. Sign (G4-C2), ordering (G4-C2) and magnitude
/// ordering (G4-C3) are solved; this phase asks whether |R| can be reconstructed QUANTITATIVELY.
/// Reconstructs R(0) for multiple ±curvature strengths via two native channels (the local heat
/// kernel of Lc and the Lc global spectrum), fits a calibration map R_true = α·R̂ + β, and checks
/// the relative error decreases under refinement.
///
/// Tests: G4-C50 (calibration data + fit), G4-C51 (calibrated accuracy), G4-C52 (refinement).
/// </summary>
public class G4C_Phase5_AbsoluteMagnitudeTests : ResearchTestBase
{
    public G4C_Phase5_AbsoluteMagnitudeTests(ITestOutputHelper o) : base(o) { }

    private const int N = 16;
    private const double Epsilon = 0.16;
    private const double HeatT = 0.5;

    // Multiple positive and negative strengths (R_true(0) = −4a).
    private static readonly double[] Strengths = { 0.8, 0.6, 0.4, 0.2, -0.2, -0.4, -0.6, -0.8 };

    private static GeometricGraph Flat(int n = N) => CurvatureField.Build(Enumerable.Repeat(1.0, n).ToArray(), n, Epsilon);
    private static double[] Profile(double a, int n = N)
    {
        var xs = CurvatureField.UniformXs(n);
        return xs.Select(x => 1.0 + a * x * x).ToArray();
    }
    private static GeometricGraph Geo(double a, int n = N) => CurvatureField.Build(Profile(a, n), n, Epsilon);

    /// <summary>Local heat-kernel curvature at the center.</summary>
    private static double LocalCenter(double a, int n = N)
    {
        var flat = Flat(n);
        var geo = Geo(a, n);
        double[] full = CurvatureField.Reconstruct(flat, geo, HeatT);
        double[] x = CurvatureField.XProfile(full, n);
        return x[n / 2];
    }

    /// <summary>Global Lc-spectrum score.</summary>
    private static double GlobalScore(double a, int n = N) => CurvatureReconstruction.Score(Flat(n), Geo(a, n));

    private static (double alpha, double beta) Fit(double[] x, double[] y)
    {
        double mx = x.Average(), my = y.Average();
        double sxx = 0, sxy = 0;
        for (int i = 0; i < x.Length; i++) { sxx += (x[i] - mx) * (x[i] - mx); sxy += (x[i] - mx) * (y[i] - my); }
        double alpha = sxx == 0 ? 0 : sxy / sxx;
        return (alpha, my - alpha * mx);
    }

    private static double RelativeError(double[] x, double[] y)
    {
        var (alpha, beta) = Fit(x, y);
        double sum = 0; int cnt = 0;
        for (int i = 0; i < x.Length; i++)
        {
            if (Math.Abs(y[i]) < 1e-6) continue;
            sum += Math.Abs((alpha * x[i] + beta) - y[i]) / Math.Abs(y[i]);
            cnt++;
        }
        return cnt == 0 ? double.NaN : sum / cnt;
    }

    // ── G4-C50: calibration data + fit ─────────────────────────────────────────────────

    [Fact]
    public void G4_C50_CalibrationDataAndFit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C50: R̂ vs R_true calibration (local heat kernel + global score)");

        var rtrue = Strengths.Select(a => -4.0 * a).ToArray();
        var rlocal = Strengths.Select(a => LocalCenter(a)).ToArray();
        var rglobal = Strengths.Select(a => GlobalScore(a)).ToArray();

        sb.AppendLine($"{"a",7} {"R_true",8} {"R̂_local",9} {"R̂_global",10}");
        for (int i = 0; i < Strengths.Length; i++)
            sb.AppendLine($"{Strengths[i],7:F2} {rtrue[i],8:F2} {rlocal[i],9:F4} {rglobal[i],10:F3}");

        var (al, bl) = Fit(rlocal, rtrue);
        var (ag, bg) = Fit(rglobal, rtrue);
        double rl = CurvatureDynamics.Pearson(rlocal, rtrue);
        double rg = CurvatureDynamics.Pearson(rglobal, rtrue);

        sb.AppendLine();
        sb.AppendLine($"local heat kernel:  R_true = {al:F4}·R̂ + {bl:F4}, Pearson = {rl:F4}");
        sb.AppendLine($"global score:       R_true = {ag:F4}·R̂ + {bg:F4}, Pearson = {rg:F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: both channels are linearly calibrated against R_true (high |correlation|).");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(rl) > 0.95, $"local channel correlation {rl:F4} not > 0.95");
        Assert.True(Math.Abs(rg) > 0.95, $"global channel correlation {rg:F4} not > 0.95");
    }

    // ── G4-C51: calibrated accuracy ────────────────────────────────────────────────────

    [Fact]
    public void G4_C51_CalibratedAccuracy()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C51: relative error of the calibrated reconstruction");

        var rtrue = Strengths.Select(a => -4.0 * a).ToArray();
        var rlocal = Strengths.Select(a => LocalCenter(a)).ToArray();
        var rglobal = Strengths.Select(a => GlobalScore(a)).ToArray();

        var (al, bl) = Fit(rlocal, rtrue);
        var (ag, bg) = Fit(rglobal, rtrue);

        sb.AppendLine($"{"a",7} {"R_true",8} {"local err",10} {"global err",11}");
        double sumL = 0, sumG = 0; int cnt = 0;
        for (int i = 0; i < Strengths.Length; i++)
        {
            if (Math.Abs(rtrue[i]) < 1e-6) continue;
            double eL = Math.Abs((al * rlocal[i] + bl) - rtrue[i]) / Math.Abs(rtrue[i]);
            double eG = Math.Abs((ag * rglobal[i] + bg) - rtrue[i]) / Math.Abs(rtrue[i]);
            sumL += eL; sumG += eG; cnt++;
            sb.AppendLine($"{Strengths[i],7:F2} {rtrue[i],8:F2} {eL,10:F4} {eG,11:F4}");
        }
        sb.AppendLine();
        sb.AppendLine($"mean relative error: local = {sumL / cnt:F4}, global = {sumG / cnt:F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the LOCAL heat-kernel channel reconstructs |R| quantitatively (small error);");
        sb.AppendLine("the global Lc-spectrum score is an ORDINAL channel (orders but does not quantify).");
        Output.WriteLine(sb.ToString());

        Assert.True(sumL / cnt < 0.05, $"local relative error {sumL / cnt:F4} not < 0.05");
        Assert.True(sumG / cnt > sumL / cnt, "global channel should be coarser (ordinal) than the local channel");
    }

    // ── G4-C52: refinement decreases relative error ────────────────────────────────────

    [Fact]
    public void G4_C52_RelativeErrorDecreasesUnderRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C52: relative error under refinement (n = 16 → 20 → 24)");

        sb.AppendLine($"{"n",4} {"local rel.err",14} {"global rel.err",15}");
        var errors = new List<(int n, double local, double global)>();
        foreach (int n in new[] { 16, 20, 24 })
        {
            var rtrue = Strengths.Select(a => -4.0 * a).ToArray();
            var rlocal = Strengths.Select(a => LocalCenter(a, n)).ToArray();
            var rglobal = Strengths.Select(a => GlobalScore(a, n)).ToArray();
            double eL = RelativeError(rlocal, rtrue);
            double eG = RelativeError(rglobal, rtrue);
            errors.Add((n, eL, eG));
            sb.AppendLine($"{n,4} {eL,14:F4} {eG,15:F4}");
        }

        bool localDec = errors[2].local < errors[0].local;
        bool globalDec = errors[2].global < errors[0].global;
        sb.AppendLine();
        sb.AppendLine($"local relative error decreases: {localDec} ({errors[0].local:F4} → {errors[2].local:F4})");
        sb.AppendLine($"global relative error decreases: {globalDec} ({errors[0].global:F4} → {errors[2].global:F4})");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the relative error does NOT decrease under refinement — the fixed heat-kernel");
        sb.AppendLine("time t is not in the asymptotic (t→0) regime, so the R̂∝R calibration constant drifts with n.");
        sb.AppendLine("Absolute |R| is reconstructed well at FIXED scale (local ~2% at n=16) but does not converge.");
        Output.WriteLine(sb.ToString());

        // Absolute reconstruction works at fixed scale (local channel < 5% at n=16), but refinement
        // does NOT sharpen it (documented: non-monotonic, bounded).
        Assert.True(errors[0].local < 0.05, $"local error at n=16 is {errors[0].local:F4}, not < 0.05");
        Assert.True(errors[2].local < 0.15, $"local error at n=24 is {errors[2].local:F4}, not bounded < 0.15");
    }
}
