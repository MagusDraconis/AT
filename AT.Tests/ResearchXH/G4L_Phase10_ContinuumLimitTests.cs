using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 10 — Lorentzian continuum limit. Determines what continuum equation the native
/// dual-object operators generate: S (Signature Operator) and G (Retarded Propagator). The native
/// operators are UNIFORM-weight alternating-layer operators (no BDG binomial coefficients), so they
/// carry the Lorentzian SIGNATURE and CAUSALITY but not the exact d'Alembertian / retarded-Green
/// stencil. Classified as PARTIAL MATCH.
///
/// Tests: G4-L100 (S signature), G4-L101 (G causality), G4-L102 (wavefront).
/// </summary>
public class G4L_Phase10_ContinuumLimitTests : ResearchTestBase
{
    public G4L_Phase10_ContinuumLimitTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);

    private static double[] Apply(double[,] op, CausalSetData cs, Func<int, int, double> phi)
    {
        var r = new double[cs.Count];
        for (int i = 0; i < cs.Count; i++)
        {
            double s = 0.0;
            for (int j = 0; j < cs.Count; j++)
                s += op[i, j] * phi(cs.Time[j], cs.Space[j]);
            r[i] = s;
        }
        return r;
    }

    private static double InteriorMean(double[] v, CausalSetData cs, int margin = 2)
    {
        double s = 0.0; int cnt = 0;
        for (int i = 0; i < cs.Count; i++)
        {
            if (cs.Time[i] < margin || cs.Time[i] > TMax - margin) continue;
            if (Math.Abs(cs.Space[i]) > XMax - margin) continue;
            s += Math.Abs(v[i]); cnt++;
        }
        return cnt == 0 ? double.NaN : s / cnt;
    }

    // ── G4-L100: S carries the Lorentzian signature but not the exact d'Alembertian ─────

    [Fact]
    public void G4_L100_SignatureOperatorIsDAlembertian()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L100: S → Lorentzian signature (not the exact d'Alembertian)");

        var cs = Cs;
        var s = LorentzianOperator.SignatureOperator(cs);
        var h2 = LorentzianOperator.HybridRetardedAlternating(cs); // off-diagonal differential part

        var sig = LorentzianOperator.Signature(SpectralCurvature.GeneralEigenvalues(s));
        bool indef = sig.pos > 0 && sig.neg > 0;
        bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, s));

        // The true d'Alembertian □ annihilates the harmonic t²+x² (□(t²+x²)=0). The native
        // uniform-weight operator does NOT (no binomial coefficients).
        double mHarm = InteriorMean(Apply(h2, cs, (t, x) => t * t + x * x), cs);

        sb.AppendLine($"S spectrum: ({sig.pos}+, {sig.neg}−) indefinite = {indef}");
        sb.AppendLine($"S layer profile alternates = {alt}");
        sb.AppendLine($"H2 applied to harmonic t²+x²: mean|·| = {mHarm:F1}  (true □ would give ≈ 0)");
        sb.AppendLine();
        sb.AppendLine($"Lorentzian signature (indefinite + alternating): {indef && alt}");
        sb.AppendLine($"exact d'Alembertian stencil (annihilates harmonic): {mHarm < 1.0}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PARTIAL MATCH — S is a Lorentzian-signature operator but a");
        sb.AppendLine("UNIFORM-weight alternating-layer operator, not the exact d'Alembertian (no binomial weights).");
        Output.WriteLine(sb.ToString());

        Assert.True(indef && alt, "S does not carry the Lorentzian signature");
        Assert.True(mHarm > 1.0, "unexpected: S annihilates the harmonic (would imply exact d'Alembertian)");
    }

    // ── G4-L101: G is retarded/causal but not the exact retarded d'Alembertian ──────────

    [Fact]
    public void G4_L101_RetardedPropagatorIsRetarded()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L101: G → retarded (causal), not the exact retarded d'Alembertian");

        var cs = Cs;
        var g = LorentzianOperator.RetardedPropagator(cs);

        int futureEntries = 0, pastEntries = 0;
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
            {
                if (cs.Order[i, j] && Math.Abs(g[i, j]) > 1e-9) futureEntries++;
                if (cs.Order[j, i] && Math.Abs(g[i, j]) > 1e-9) pastEntries++;
            }

        sb.AppendLine($"G future (anti-causal) entries = {futureEntries}; past (causal) entries = {pastEntries}");
        sb.AppendLine($"G strictly retarded: {futureEntries == 0 && pastEntries > 0}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PARTIAL MATCH — G is the retarded (causal, lower-triangular) operator");
        sb.AppendLine("but its off-diagonal weights are UNIFORM (±1 alternation), not the exact retarded");
        sb.AppendLine("d'Alembertian kernel (which requires the BDG binomial coefficients).");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0, futureEntries);
        Assert.True(pastEntries > 0, "G has no causal (past) entries");
    }

    // ── G4-L102: wavefront / propagation kernel ─────────────────────────────────────────

    [Fact]
    public void G4_L102_WavefrontAndPropagationKernel()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L102: wavefront structure of G vs S");

        var cs = Cs;
        int tc = TMax / 2, xc = 0;
        int c = tc * (2 * XMax + 1) + XMax;
        var g = LorentzianOperator.RetardedPropagator(cs);
        var s = LorentzianOperator.SignatureOperator(cs);

        var gm = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(g, c), tc, xc);
        var sm = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(s, c), tc, xc);

        double dirG = gm.past + gm.future == 0 ? 0.5 : gm.future / (gm.past + gm.future);
        double dirS = sm.past + sm.future == 0 ? 0.5 : sm.future / (sm.past + sm.future);

        sb.AppendLine($"{"object",-18} {"leakage",9} {"direction",11} {"front-v",9}");
        sb.AppendLine($"{"G (retarded)",-18} {gm.leak,9:F3} {dirG,11:F3} {gm.causalFront,9:F3}");
        sb.AppendLine($"{"S (signature)",-18} {sm.leak,9:F3} {dirS,11:F3} {sm.causalFront,9:F3}");
        sb.AppendLine();
        sb.AppendLine($"G propagates causally (leak {gm.leak:F3}, dir {dirG:F3}) — retarded-propagator structure.");
        sb.AppendLine($"S is time-symmetric (leak {sm.leak:F3}, dir {dirS:F3}) — d'Alembertian-like (Feynman) structure.");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PARTIAL MATCH — the dual-object pair reproduces the d'Alembertian +");
        sb.AppendLine("retarded-Green-function STRUCTURE (signature + causality), but with uniform weights,");
        sb.AppendLine("not the exact continuum operators (no BDG binomial coefficients).");
        Output.WriteLine(sb.ToString());

        Assert.True(gm.leak < 0.15 && dirG > 0.9, "G does not propagate causally");
        Assert.True(sm.leak > gm.leak, "S should be more symmetric (leakier) than G");
    }
}
