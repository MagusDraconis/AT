using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 3 — a native retarded-INDEFINITE Lorentzian operator. Combines R1 (causality /
/// retarded) with L3 (alternating layer structure / indefinite) into hybrids, and tests whether
/// one operator preserves BOTH retarded propagation and Lorentzian (indefinite) spectral
/// structure, while being closer to BDG than L3.
///
/// Tests: G4-L30 (propagation asymmetry), G4-L31 (eigenmodes + alternation + KS),
///        G4-L32 (stability under refinement).
/// </summary>
public class G4L_Phase3_RetardedIndefiniteOperatorTests : ResearchTestBase
{
    public G4L_Phase3_RetardedIndefiniteOperatorTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);
    private static int Center(CausalSetData cs) => (cs.Time.Max() / 2) * Nx + XMax;

    private static (double past, double future) Response(CausalSetData cs, double[,] m, int c)
    {
        int tc = cs.Time[c];
        double past = 0.0, future = 0.0;
        for (int j = 0; j < cs.Count; j++)
        {
            if (cs.Time[j] < tc) past += Math.Abs(m[j, c]);
            else if (cs.Time[j] > tc) future += Math.Abs(m[j, c]);
        }
        return (past, future);
    }

    private static double Retardedness((double past, double future) r)
        => r.past + r.future == 0.0 ? 0.5 : r.future / (r.past + r.future);

    private static bool IndefiniteReal(double[] evals)
        => evals.Any(x => x > 1e-9) && evals.Any(x => x < -1e-9);

    // ── G4-L30: propagation asymmetry + retarded-ness ──────────────────────────────────

    [Fact]
    public void G4_L30_HybridIsRetardedBiased()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L30: propagation asymmetry and retarded-ness");

        var cs = Cs;
        int c = Center(cs);
        var r1 = LorentzianOperator.PastDirectedLayer(cs);
        var l3 = LorentzianOperator.BidirectionalLayer(cs);
        var h2 = LorentzianOperator.HybridRetardedAlternating(cs);
        var h3 = LorentzianOperator.HybridRetardedDensityWeighted(cs);

        var rows = new (string name, double[,] m)[] { ("R1", r1), ("L3", l3), ("H2", h2), ("H3", h3) };
        sb.AppendLine($"δ-source at (t={cs.Time[c]}, x=0). Past/future response:");
        sb.AppendLine($"{"operator",-8} {"past",8} {"future",8} {"retarded-ness",14}");
        double l3Ret = 0, h2Ret = 0;
        foreach (var (name, m) in rows)
        {
            var r = Response(cs, m, c);
            double ret = Retardedness(r);
            sb.AppendLine($"{name,-8} {r.past,8:F2} {r.future,8:F2} {ret,14:F3}");
            if (name == "L3") l3Ret = ret;
            if (name == "H2") h2Ret = ret;
        }

        sb.AppendLine();
        sb.AppendLine($"H2 retarded-ness {h2Ret:F3} vs L3 {l3Ret:F3} (1.0 = fully retarded, 0.5 = symmetric).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: H2 = R1 + L3 is forward-BIASED (retarded) — it is more retarded than L3");
        sb.AppendLine("while remaining a single operator (not purely one-sided).");
        Output.WriteLine(sb.ToString());

        var rH2 = Response(cs, h2, c);
        var rL3 = Response(cs, l3, c);
        Assert.True(rH2.future > rH2.past, "H2 is not forward-biased");
        Assert.True(Retardedness(rH2) > Retardedness(rL3), "H2 is not more retarded than L3");
    }

    // ── G4-L31: eigenmodes + alternation + KS to BDG ───────────────────────────────────

    [Fact]
    public void G4_L31_HybridIsIndefiniteAndCloserToBdg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L31: positive/negative eigenmodes, alternation, and KS to BDG");

        var cs = Cs;
        var r1 = LorentzianOperator.PastDirectedLayer(cs);
        var h2 = LorentzianOperator.HybridRetardedAlternating(cs);
        var h3 = LorentzianOperator.HybridRetardedDensityWeighted(cs);

        double[] e1 = SpectralCurvature.GeneralEigenvalues(r1);
        double[] e2 = SpectralCurvature.GeneralEigenvalues(h2);
        double[] e3 = SpectralCurvature.GeneralEigenvalues(h3);

        var s1 = LorentzianOperator.Signature(e1);
        var s2 = LorentzianOperator.Signature(e2);
        var s3 = LorentzianOperator.Signature(e3);

        double[] bdgSym = LorentzianOperator.Eigenvalues(LorentzianOperator.BdgReference(cs));
        double ks1 = SpectralCurvature.KolmogorovSmirnov(e1, bdgSym);
        double ks2 = SpectralCurvature.KolmogorovSmirnov(e2, bdgSym);
        double ks3 = SpectralCurvature.KolmogorovSmirnov(e3, bdgSym);
        double ksL3 = SpectralCurvature.KolmogorovSmirnov(
            LorentzianOperator.Eigenvalues(LorentzianOperator.BidirectionalLayer(cs)), bdgSym);

        sb.AppendLine("Real-part eigenmode signature (n+, n−, n0):");
        sb.AppendLine($"{"operator",-8} {"n+",6} {"n−",6} {"n0",6} {"indefinite",11} {"KS to BDG",11}");
        sb.AppendLine($"{"R1",-8} {s1.pos,6} {s1.neg,6} {s1.zero,6} {IndefiniteReal(e1),11} {ks1,11:F4}");
        sb.AppendLine($"{"H2",-8} {s2.pos,6} {s2.neg,6} {s2.zero,6} {IndefiniteReal(e2),11} {ks2,11:F4}");
        sb.AppendLine($"{"H3",-8} {s3.pos,6} {s3.neg,6} {s3.zero,6} {IndefiniteReal(e3),11} {ks3,11:F4}");
        sb.AppendLine($"{"L3",-8} {"(symmetric, indefinite)"} {ksL3,27:F4}");
        sb.AppendLine();

        var h2Profile = LorentzianOperator.LayerProfile(cs, h2);
        bool h2Alternates = LorentzianOperator.Alternates(h2Profile);
        sb.AppendLine($"H2 layer profile: k=0 {h2Profile[0]:F2}, k=1 {h2Profile[1]:F2}, k=2 {h2Profile[2]:F2}  alternates = {h2Alternates}");
        sb.AppendLine();
        sb.AppendLine($"H2 closer to BDG than L3 (KS {ks2:F4} vs {ksL3:F4}): {ks2 < ksL3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: H2 = R1 + L3 preserves BOTH retarded propagation (G4-L30) and the");
        sb.AppendLine("indefinite Lorentzian spectral structure — it is retarded-biased AND indefinite.");
        Output.WriteLine(sb.ToString());

        Assert.True(IndefiniteReal(e2), "H2 is not indefinite (real part)");
        Assert.True(h2Alternates, "H2 does not alternate over layers");
        Assert.True(ks2 < ksL3, $"H2 ({ks2:F4}) is not closer to BDG than L3 ({ksL3:F4})");
    }

    // ── G4-L32: stability under refinement ─────────────────────────────────────────────

    [Fact]
    public void G4_L32_HybridStableUnderRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L32: stability of the retarded-indefinite hybrid under refinement");

        sb.AppendLine($"{"grid",-10} {"N",6} {"forward-biased",15} {"indefinite",11} {"alternating",12}");
        foreach (var (tMax, xMax) in new[] { (7, 4), (9, 5) })
        {
            var cs = CausalSet.BuildGrid(tMax, xMax);
            int c = (cs.Time.Max() / 2) * (2 * xMax + 1) + xMax;
            var h2 = LorentzianOperator.HybridRetardedAlternating(cs);
            var r = Response(cs, h2, c);
            double[] ev = SpectralCurvature.GeneralEigenvalues(h2);
            var prof = LorentzianOperator.LayerProfile(cs, h2);
            bool fwd = r.future > r.past;
            bool indef = IndefiniteReal(ev);
            bool alt = LorentzianOperator.Alternates(prof);
            sb.AppendLine($"{$"{tMax}x{xMax}",-10} {cs.Count,6} {fwd,15} {indef,11} {alt,12}");
            Assert.True(fwd, $"n={cs.Count}: H2 not forward-biased");
            Assert.True(indef, $"n={cs.Count}: H2 not indefinite");
            Assert.True(alt, $"n={cs.Count}: H2 not alternating");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the retarded-indefinite hybrid H2 is stable under graph refinement —");
        sb.AppendLine("retarded propagation, indefinite spectrum, and layer alternation all persist.");
        Output.WriteLine(sb.ToString());
    }
}
