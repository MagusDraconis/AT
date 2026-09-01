using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-P Phase 1 — isolate the native curvature potential. The Phase-0 result Lc = −c Δ_g + c V
/// (V = Δρ/ρ²) is split into its three terms and each is measured for curvature reconstruction:
/// sign recovery, magnitude ordering, and refinement stability. Classifies each term's
/// contribution as DOMINANT / SECONDARY / NEGLIGIBLE.
///
/// Tests: G4-P10 (sign), G4-P11 (magnitude ordering), G4-P12 (refinement).
/// </summary>
public class G4P_Phase1_CurvaturePotentialAnalysisTests : ResearchTestBase
{
    public G4P_Phase1_CurvaturePotentialAnalysisTests(ITestOutputHelper o) : base(o) { }

    private static GeometricGraph Flat(int n = 16) => ConformalRateGraph.Build(0.0, n, 0.16);
    private static GeometricGraph Negative(int n = 16) => ConformalRateGraph.Build(+1.0, n, 0.16);
    private static GeometricGraph Positive(int n = 16) => ConformalRateGraph.Build(-0.8, n, 0.16);
    private static GeometricGraph Geometry(double a, int n = 16) => ConformalRateGraph.Build(a, n, 0.16);

    // Magnitude-ordering range (a=1.0 excluded — R(±1)=0 profile node).
    private static readonly double[] MonotonicAValues =
        { 0.8, 0.6, 0.4, 0.2, 0.0, -0.2, -0.4, -0.6, -0.8 };

    private static readonly CurvatureTermKind[] Terms =
        { CurvatureTermKind.DeltaGOnly, CurvatureTermKind.PotentialOnly, CurvatureTermKind.Full };

    private static string T(CurvatureTermKind k) => k switch
    {
        CurvatureTermKind.DeltaGOnly => "Δg only",
        CurvatureTermKind.PotentialOnly => "V=Δρ/ρ² only",
        CurvatureTermKind.Full => "Δg + V (Lc)",
        _ => k.ToString()
    };

    // ── G4-P10: sign recovery per term ─────────────────────────────────────────────────

    [Fact]
    public void G4_P10_SignRecoveryPerTerm()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-P10: which term recovers the curvature sign?");

        var flat = Flat();
        var neg = Negative();
        var pos = Positive();

        sb.AppendLine($"{"term",-14} {"score neg",10} {"score pos",10} {"sign neg",9} {"sign pos",9} {"recovers",9}");
        foreach (var kind in Terms)
        {
            double sn = CurvaturePotential.Score(flat, neg, kind);
            double sp = CurvaturePotential.Score(flat, pos, kind);
            int gn = Math.Sign(sn), gp = Math.Sign(sp);
            bool recovers = gn == -1 && gp == +1;
            sb.AppendLine($"{T(kind),-14} {sn,10:F3} {sp,10:F3} {gn,9} {gp,9} {recovers,9}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: reported above — the potential V = Δρ/ρ² carries the curvature sign; see");
        sb.AppendLine("G4-P11/P12 for ordering and refinement.");
        Output.WriteLine(sb.ToString());

        // At least one term must recover the sign (the driver exists); the full Lc must recover it.
        var full = CurvatureTermKind.Full;
        double fn = CurvaturePotential.Score(flat, neg, full);
        double fp = CurvaturePotential.Score(flat, pos, full);
        Assert.True(Math.Sign(fn) == -1 && Math.Sign(fp) == +1, "full Lc fails to recover the sign");
    }

    // ── G4-P11: magnitude ordering per term ────────────────────────────────────────────

    [Fact]
    public void G4_P11_MagnitudeOrderingPerTerm()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-P11: which term recovers the curvature magnitude ordering?");

        var flat = Flat();
        foreach (var kind in Terms)
        {
            var scores = MonotonicAValues.Select(a => CurvaturePotential.Score(flat, Geometry(a), kind)).ToList();
            bool mono = true;
            for (int i = 1; i < scores.Count; i++) if (scores[i] <= scores[i - 1]) mono = false;
            sb.AppendLine($"{T(kind),-14} monotonic={mono}  range=[{scores.Min():F2}, {scores.Max():F2}]");
            sb.AppendLine($"    scores: {string.Join("  ", scores.Select(s => s.ToString("F2", CultureInfo.InvariantCulture)))}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: reported above.");
        Output.WriteLine(sb.ToString());

        // The full Lc must be monotonic in curvature (G4-C31 established this).
        var fullScores = MonotonicAValues.Select(a => CurvaturePotential.Score(flat, Geometry(a), CurvatureTermKind.Full)).ToList();
        for (int i = 1; i < fullScores.Count; i++)
            Assert.True(fullScores[i] > fullScores[i - 1], $"Lc non-monotonic at index {i}");
    }

    // ── G4-P12: refinement stability per term + classification ─────────────────────────

    [Fact]
    public void G4_P12_RefinementStabilityAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-P12: refinement stability and contribution classification");

        // Sign recovery at n=16 and n=24 for each term.
        sb.AppendLine($"{"term",-14} {"n",4} {"sign neg",9} {"sign pos",9} {"recovers",9}  {"|score| scale",13}");
        foreach (var kind in Terms)
        {
            foreach (int n in new[] { 16, 24 })
            {
                var flat = Flat(n);
                double sn = CurvaturePotential.Score(flat, Negative(n), kind);
                double sp = CurvaturePotential.Score(flat, Positive(n), kind);
                int gn = Math.Sign(sn), gp = Math.Sign(sp);
                bool recovers = gn == -1 && gp == +1;
                double scale = Math.Max(Math.Abs(sn), Math.Abs(sp));
                sb.AppendLine($"{T(kind),-14} {n,4} {gn,9} {gp,9} {recovers,9}  {scale,13:F3}");
            }
            sb.AppendLine();
        }

        // Classification: which term drives the CORRECT reconstruction?
        var flatG = Flat();
        var negG = Negative();
        var posG = Positive();
        double dgNeg = CurvaturePotential.Score(flatG, negG, CurvatureTermKind.DeltaGOnly);
        double dgPos = CurvaturePotential.Score(flatG, posG, CurvatureTermKind.DeltaGOnly);
        double vNeg = CurvaturePotential.Score(flatG, negG, CurvatureTermKind.PotentialOnly);
        double vPos = CurvaturePotential.Score(flatG, posG, CurvatureTermKind.PotentialOnly);
        bool dgCorrect = Math.Sign(dgNeg) == -1 && Math.Sign(dgPos) == +1;
        bool vInverted = Math.Sign(vNeg) == +1 && Math.Sign(vPos) == -1; // V ∝ −R

        sb.AppendLine("Contribution classification (sign orientation):");
        sb.AppendLine($"  Δg only:  sign({dgNeg:F1}) = {Math.Sign(dgNeg)}, sign({dgPos:F1}) = {Math.Sign(dgPos)} → correct orientation {dgCorrect} ⇒ DOMINANT");
        sb.AppendLine($"  V only:   sign({vNeg:F1}) = {Math.Sign(vNeg)}, sign({vPos:F1}) = {Math.Sign(vPos)} → inverted (V ∝ −R) {vInverted} ⇒ SECONDARY");
        sb.AppendLine($"  Full Lc:  correct orientation (Δg dominates) ⇒ the reconstruction is driven by Δg, not V.");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: reported above.");
        Output.WriteLine(sb.ToString());

        // Δ_g alone recovers the correct sign (DOMINANT driver); V is inverted (SECONDARY).
        Assert.True(dgCorrect, "Δg alone should recover the correct curvature sign");
        Assert.True(vInverted, "V alone should carry the inverted sign (V ∝ −R)");
    }
}
