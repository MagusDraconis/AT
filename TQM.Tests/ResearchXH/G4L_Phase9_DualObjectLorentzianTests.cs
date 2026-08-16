using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 9 — dual-object Lorentzian structure. Following the Phase-8 audit (the signature–
/// causality tension), test whether causality and Lorentzian signature must live in TWO operators:
/// a Signature Operator S = H2 + D (indefinite, time-symmetric/Feynman) and a Retarded Propagator
/// G = D + 2R1 (strictly causal, elliptic) — the native analogue of BDG's symmetric d'Alembertian
/// + retarded Green-function split.
///
/// Tests: G4-L90 (complementary properties), G4-L91 (causal propagation of G), G4-L92 (the
/// S = G + R2 relationship and whether the pair resolves the tension).
/// </summary>
public class G4L_Phase9_DualObjectLorentzianTests : ResearchTestBase
{
    public G4L_Phase9_DualObjectLorentzianTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);
    private static int Index(CausalSetData cs, int t, int x) => t * Nx + (x + XMax);

    private static double Dir((double past, double future) r)
        => r.past + r.future == 0.0 ? 0.5 : r.future / (r.past + r.future);

    private static double MaxDiff(double[,] a, double[,] b)
    {
        double m = 0;
        for (int i = 0; i < a.GetLength(0); i++)
            for (int j = 0; j < a.GetLength(1); j++)
                m = Math.Max(m, Math.Abs(a[i, j] - b[i, j]));
        return m;
    }

    // ── G4-L90: complementary properties ────────────────────────────────────────────────

    [Fact]
    public void G4_L90_DualObjectsCarryComplementaryProperties()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L90: do S and G carry complementary (split) properties?");

        var cs = Cs;
        int tc = TMax / 2, xc = 0;
        var s = LorentzianOperator.SignatureOperator(cs);
        var g = LorentzianOperator.RetardedPropagator(cs);
        var h = LorentzianOperator.NativeLorentzian(cs); // the single-object compromise

        var sigS = LorentzianOperator.Signature(SpectralCurvature.GeneralEigenvalues(s));
        var sigG = LorentzianOperator.Signature(SpectralCurvature.GeneralEigenvalues(g));
        double leakS = Leak(cs, s, tc, xc);
        double leakG = Leak(cs, g, tc, xc);
        double leakH = Leak(cs, h, tc, xc);
        bool indefS = sigS.pos > 0 && sigS.neg > 0;
        bool indefG = sigG.pos > 0 && sigG.neg > 0;

        sb.AppendLine($"{"object",-18} {"leakage",9} {"(n+,n−)",10} {"indefinite",11} {"role",-22}");
        sb.AppendLine($"{"S (signature)",-18} {leakS,9:F3} {($"({sigS.pos},{sigS.neg})"),10} {indefS,11} {"Lorentzian signature",-22}");
        sb.AppendLine($"{"G (retarded)",-18} {leakG,9:F3} {($"({sigG.pos},{sigG.neg})"),10} {indefG,11} {"strictly causal",-22}");
        sb.AppendLine($"{"H (single, native)",-18} {leakH,9:F3} {"—",10} {"—",11} {"compromise",-22}");
        sb.AppendLine();
        sb.AppendLine($"S indefinite (signature) and G causal (leak {leakG:F3} < S leak {leakS:F3}): " +
                      $"{indefS && leakG < leakS}.");
        Output.WriteLine(sb.ToString());

        Assert.True(indefS, "signature operator S is not indefinite");
        Assert.False(indefG, "retarded propagator G should be elliptic (not indefinite)");
        Assert.True(leakG < leakS, $"G ({leakG:F3}) should leak far less than S ({leakS:F3})");
    }

    // ── G4-L91: causal propagation of G ──────────────────────────────────────────────────

    [Fact]
    public void G4_L91_RetardedPropagatorPropagatesCausally()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L91: does the retarded propagator G propagate causally?");

        var cs = Cs;
        int tc = TMax / 2, xc = 0;
        int c = Index(cs, tc, xc);
        var g = LorentzianOperator.RetardedPropagator(cs);
        var s = LorentzianOperator.SignatureOperator(cs);
        var bdgRet = LorentzianOperator.RetardedBdg(cs);

        var gm = Metrics(cs, g, c, tc, xc);
        var sm = Metrics(cs, s, c, tc, xc);
        var bm = Metrics(cs, bdgRet, c, tc, xc);

        sb.AppendLine($"{"object",-18} {"leakage",9} {"direction",11} {"front-v",9}");
        sb.AppendLine($"{"G (retarded)",-18} {gm.leak,9:F3} {Dir((gm.past, gm.future)),11:F3} {gm.causalFront,9:F3}");
        sb.AppendLine($"{"S (signature)",-18} {sm.leak,9:F3} {Dir((sm.past, sm.future)),11:F3} {sm.causalFront,9:F3}");
        sb.AppendLine($"{"BDG_ret (ref)",-18} {bm.leak,9:F3} {Dir((bm.past, bm.future)),11:F3} {bm.causalFront,9:F3}");
        sb.AppendLine();
        sb.AppendLine($"G causal: leak {gm.leak:F3} < 0.15, direction {Dir((gm.past, gm.future)):F3} > 0.9, front-v {gm.causalFront:F3} ≤ 1.");
        Output.WriteLine(sb.ToString());

        Assert.True(gm.leak < 0.15, $"G leaks ({gm.leak:F3}), expected causal");
        Assert.True(Dir((gm.past, gm.future)) > 0.9, $"G not forward-directed (dir {Dir((gm.past, gm.future)):F3})");
        Assert.True(gm.causalFront <= 1.0, $"G has superluminal front (v = {gm.causalFront:F3})");
    }

    // ── G4-L92: S = G + R2 and the tension resolution ────────────────────────────────────

    [Fact]
    public void G4_L92_DualObjectResolvesTension()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L92: does the dual-object pair resolve the signature–causality tension?");

        var cs = Cs;
        int tc = TMax / 2, xc = 0;
        var s = LorentzianOperator.SignatureOperator(cs);
        var g = LorentzianOperator.RetardedPropagator(cs);
        var r2 = LorentzianOperator.FutureDirectedLayer(cs);

        // S = G + R2 (signature = retarded + future) — the structural link.
        double d = MaxDiff(s, LorentzianOperator.Add(g, r2));

        var sigS = LorentzianOperator.Signature(SpectralCurvature.GeneralEigenvalues(s));
        bool indefS = sigS.pos > 0 && sigS.neg > 0;
        double leakG = Leak(cs, g, tc, xc);
        double leakH = Leak(cs, LorentzianOperator.NativeLorentzian(cs), tc, xc);

        sb.AppendLine($"S = G + R2 (max|diff| = {d:E2})");
        sb.AppendLine($"G leak {leakG:F3} (causal), S indefinite {indefS} (signature)");
        sb.AppendLine($"single-object native H leak {leakH:F3} (compromise: causal-ish + indefinite-ish)");
        sb.AppendLine();
        sb.AppendLine($"The PAIR jointly satisfies causality (G leak {leakG:F3} < 0.15) AND signature (S indefinite {indefS}) —");
        sb.AppendLine($"two criteria no single native operator met (H compromises at leak {leakH:F3}).");
        Output.WriteLine(sb.ToString());

        Assert.True(d < 1e-9, $"S != G + R2 (max|diff| = {d:E2})");
        Assert.True(leakG < 0.15 && indefS,
            $"dual-object pair fails: G leak {leakG:F3}, S indefinite {indefS}");
    }

    private static double Leak(CausalSetData cs, double[,] op, int tc, int xc)
        => Metrics(cs, op, Index(cs, tc, xc), tc, xc).leak;

    private static (double past, double future, double causalFront, double leak) Metrics(
        CausalSetData cs, double[,] op, int c, int tc, int xc)
        => LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(op, c), tc, xc);
}
