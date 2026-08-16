using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-C Phase 3 — reconstruct curvature MAGNITUDE from Lc = ρ⁻¹ L ρ⁻¹.
/// Generates multiple positive- and negative-curvature strengths via ρ = 1 + a·x² (R(0) = −4a),
/// and tests sign(R), magnitude ordering, and refinement stability of the reconstruction score.
///
/// Tests: G4-C30 (sign), G4-C31 (magnitude ordering), G4-C32 (refinement stability).
/// </summary>
public class G4C_Phase3_CurvatureMagnitudeTests : ResearchTestBase
{
    public G4C_Phase3_CurvatureMagnitudeTests(ITestOutputHelper o) : base(o) { }

    private static GeometricGraph Flat(int n = 16) => ConformalRateGraph.Build(0.0, n, 0.16);
    private static GeometricGraph Geometry(double a, int n = 16) => ConformalRateGraph.Build(a, n, 0.16);

    // All strengths, a sorted DESCENDING (R(0) = −4a sorted ascending).
    private static readonly double[] FullAValues = { 1.0, 0.8, 0.6, 0.4, 0.2, 0.0, -0.2, -0.4, -0.6, -0.8 };

    // Magnitude-ordering range (a=1.0 excluded — R(±1)=0 profile node makes global curvature non-monotonic).
    private static readonly double[] MonotonicAValues = { 0.8, 0.6, 0.4, 0.2, 0.0, -0.2, -0.4, -0.6, -0.8 };

    private static double Score(double a, int n = 16)
        => CurvatureReconstruction.Score(Flat(n), Geometry(a, n));

    // ── G4-C30: sign(R) correct for every strength ─────────────────────────────────────

    [Fact]
    public void G4_C30_SignRecoveredForAllStrengths()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C30: sign(R) recovered for multiple positive and negative strengths");

        sb.AppendLine("Conformal geometries ρ = 1 + a·x², R(0) = −4a.");
        sb.AppendLine($"{"a",7} {"R(0)",7} {"score",10} {"sign",5}  match");
        foreach (double a in FullAValues)
        {
            double r = ConformalRateGraph.ConformalCurvature(a, 0.0);
            double s = Score(a);
            int sign = Math.Sign(s);
            int known = Math.Sign(r);
            sb.AppendLine($"{a,7:F2} {r,7:F2} {s,10:F3} {sign,5}  {sign == known}");
        }

        sb.AppendLine();
        sb.AppendLine($"SC1: all {FullAValues.Length} strengths recover the correct curvature sign.");
        sb.AppendLine("NOTE: at a=1.0 the conformal curvature R(x)=−4(1−x²)/(1+x²)³ vanishes at x=±1");
        sb.AppendLine("(a profile node), so the GLOBAL curvature is non-monotonic in R(0) — sign remains correct.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: sign(R) is recovered across the full strength range.");
        Output.WriteLine(sb.ToString());

        foreach (double a in FullAValues)
        {
            double r = ConformalRateGraph.ConformalCurvature(a, 0.0);
            Assert.Equal(Math.Sign(r), Math.Sign(Score(a)));
        }
    }

    // ── G4-C31: magnitude ordering (score monotonic in R) ──────────────────────────────

    [Fact]
    public void G4_C31_MagnitudeOrderingIsMonotonic()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C31: magnitude ordering — score is monotonic in curvature");

        var scores = MonotonicAValues.Select(a => (a, s: Score(a))).ToList();

        sb.AppendLine("ASSUMPTIONS: a descends ⇒ R(0)=−4a ascends ⇒ the score must strictly INCREASE.");
        sb.AppendLine($"{"a",7} {"R(0)",7} {"score",10}  Δ(score)");
        bool monotonic = true;
        for (int i = 0; i < scores.Count; i++)
        {
            double delta = i == 0 ? 0.0 : scores[i].s - scores[i - 1].s;
            if (i > 0 && delta <= 0.0) monotonic = false;
            sb.AppendLine($"{scores[i].a,7:F2} {ConformalRateGraph.ConformalCurvature(scores[i].a, 0.0),7:F2} " +
                          $"{scores[i].s,10:F3}  {(i == 0 ? "" : delta.ToString("F3", CultureInfo.InvariantCulture))}");
        }

        sb.AppendLine();
        sb.AppendLine($"SC2: score strictly monotonic across {scores.Count} strengths: {monotonic}");
        sb.AppendLine("(a=1.0 is excluded: its R(±1)=0 node makes the global curvature non-monotonic — see report).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: Lc spectral observables recover curvature-magnitude ordering.");
        Output.WriteLine(sb.ToString());

        for (int i = 1; i < scores.Count; i++)
            Assert.True(scores[i].s > scores[i - 1].s,
                $"non-monotonic at a={scores[i].a}: {scores[i - 1].s:F3} -> {scores[i].s:F3}");
    }

    // ── G4-C32: refinement stability of the magnitude ordering ────────────────────────

    [Fact]
    public void G4_C32_MagnitudeOrderingStableUnderRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-C32: magnitude ordering stable under refinement (n=16 → n=24)");

        sb.AppendLine("ASSUMPTIONS: refinement = increase per-axis node count (N = n²).");
        sb.AppendLine();

        foreach (int n in new[] { 16, 24 })
        {
            var scores = MonotonicAValues.Select(a => Score(a, n)).ToList();
            bool monotonic = true;
            for (int i = 1; i < scores.Count; i++)
                if (scores[i] <= scores[i - 1]) monotonic = false;
            sb.AppendLine($"n={n,-3} (N={n*n})  monotonic={monotonic}  " +
                          $"score range=[{scores[0]:F2}, {scores[^1]:F2}]");
            Assert.True(monotonic, $"n={n}: magnitude ordering not monotonic");
        }

        sb.AppendLine();
        sb.AppendLine("SC3: the monotonic magnitude ordering persists under refinement.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: curvature-magnitude reconstruction from Lc is refinement-stable.");
        Output.WriteLine(sb.ToString());
    }
}
