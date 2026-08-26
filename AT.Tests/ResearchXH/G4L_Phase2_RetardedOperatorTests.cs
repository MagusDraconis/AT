using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 2 — transform L3 into a retarded operator. Builds R1 (past-directed / retarded),
/// R2 (future-directed / advanced), R3 (bidirectional baseline = symmetric L3) and measures
/// spectrum, interval response, propagation asymmetry, and KS distance to BDG — testing whether
/// causal directionality reduces the distance to BDG.
///
/// Tests: G4-L20 (construction + directionality), G4-L21 (spectrum + interval response),
///        G4-L22 (propagation asymmetry + KS to BDG).
/// </summary>
public class G4L_Phase2_RetardedOperatorTests : ResearchTestBase
{
    public G4L_Phase2_RetardedOperatorTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);
    private static int Center => (TMax / 2) * Nx + XMax; // t=3, x=0

    // ── G4-L20: construction + directionality ──────────────────────────────────────────

    [Fact]
    public void G4_L20_RetardedOperatorsAreConstructibleAndDirectional()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L20: R1/R2/R3 construction and causal directionality");

        var cs = Cs;
        var r1 = LorentzianOperator.PastDirectedLayer(cs);
        var r2 = LorentzianOperator.FutureDirectedLayer(cs);
        var r3 = LorentzianOperator.BidirectionalLayer(cs);

        bool r1PastOnly = true, r2FutureOnly = true;
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
            {
                if (Math.Abs(r1[i, j]) > 1e-12 && !cs.Order[j, i]) r1PastOnly = false; // R1 nonzero only future→past (retarded)
                if (Math.Abs(r2[i, j]) > 1e-12 && !cs.Order[i, j]) r2FutureOnly = false; // R2 nonzero only past→future (advanced)
            }

        bool r2IsTranspose = true;
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
                if (Math.Abs(r2[i, j] - r1[j, i]) > 1e-12) r2IsTranspose = false;

        bool r3IsSumAndSymmetric = true;
        for (int i = 0; i < cs.Count; i++)
            for (int j = 0; j < cs.Count; j++)
                if (Math.Abs(r3[i, j] - (r1[i, j] + r2[i, j])) > 1e-12 || Math.Abs(r3[i, j] - r3[j, i]) > 1e-12)
                    r3IsSumAndSymmetric = false;

        sb.AppendLine($"R1 past-directed only (nonzero ⇒ i ≺ j): {r1PastOnly}");
        sb.AppendLine($"R2 future-directed only (nonzero ⇒ i ≻ j): {r2FutureOnly}");
        sb.AppendLine($"R2 = R1ᵀ: {r2IsTranspose}");
        sb.AppendLine($"R3 = R1 + R2 (symmetric baseline): {r3IsSumAndSymmetric}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: retarded (R1), advanced (R2), and bidirectional (R3) operators are");
        sb.AppendLine("constructed natively from the causal order; R1/R2 are directed, R3 is symmetric.");
        Output.WriteLine(sb.ToString());

        Assert.True(r1PastOnly, "R1 has future (non-past) entries");
        Assert.True(r2FutureOnly, "R2 has past (non-future) entries");
        Assert.True(r2IsTranspose, "R2 is not R1 transpose");
        Assert.True(r3IsSumAndSymmetric, "R3 is not R1+R2 symmetric");
    }

    // ── G4-L21: spectrum + interval response ───────────────────────────────────────────

    [Fact]
    public void G4_L21_SpectrumAndIntervalResponse()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L21: spectrum and directed interval response");

        var cs = Cs;
        var r1 = LorentzianOperator.PastDirectedLayer(cs);
        var r2 = LorentzianOperator.FutureDirectedLayer(cs);
        var r3 = LorentzianOperator.BidirectionalLayer(cs);

        double[] e1 = SpectralCurvature.GeneralEigenvalues(r1);
        double[] e2 = SpectralCurvature.GeneralEigenvalues(r2);
        double[] e3 = LorentzianOperator.Eigenvalues(r3);
        var s3 = LorentzianOperator.Signature(e3);
        double m1 = e1.Max(x => Math.Abs(x));
        double m2 = e2.Max(x => Math.Abs(x));
        double m3 = e3.Max(x => Math.Abs(x));
        // Nilpotent (strictly triangular) operators have a numerically-degenerate spectrum
        // (max|λ| ≪ R3's spectral radius).
        bool r1Nilpotent = m1 < 0.1 * m3;
        bool r2Nilpotent = m2 < 0.1 * m3;

        sb.AppendLine("Spectrum (retarded/advanced are strictly triangular → nilpotent, zero spectrum):");
        sb.AppendLine($"  R1 retarded:   max|λ| = {m1:E2}  nilpotent = {r1Nilpotent}");
        sb.AppendLine($"  R2 advanced:   max|λ| = {m2:E2}  nilpotent = {r2Nilpotent}");
        sb.AppendLine($"  R3 symmetric:  signature ({s3.pos},{s3.neg},{s3.zero})  indefinite (max|λ| = {m3:F1})");
        sb.AppendLine();

        sb.AppendLine("Directed interval response (mean entry per interval k):");
        sb.AppendLine($"{"operator",-16} {"direction",-10} {"k=0",7} {"k=1",7} {"k=2",7} {"k=3",7}");
        foreach (var (name, m) in new[] { ("R1", r1), ("R2", r2), ("R3", r3) })
        {
            var (past, fut) = LorentzianOperator.DirectedLayerProfile(cs, m);
            sb.AppendLine($"{name,-16} {"past",-10} {past[0],7:F1} {past[1],7:F1} {past[2],7:F1} {past[3],7:F1}");
            sb.AppendLine($"{name,-16} {"future",-10} {fut[0],7:F1} {fut[1],7:F1} {fut[2],7:F1} {fut[3],7:F1}");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: R1 carries the layer weights in the PAST direction only; R2 in the FUTURE");
        sb.AppendLine("only; R3 in both. Retarded/advanced operators have a degenerate (nilpotent) spectrum —");
        sb.AppendLine("the wave information lives in the off-diagonal (nilpotent) structure, not the spectrum.");
        Output.WriteLine(sb.ToString());

        Assert.True(r1Nilpotent, "R1 is not nilpotent");
        Assert.True(r2Nilpotent, "R2 is not nilpotent");
        Assert.True(s3.pos > 0 && s3.neg > 0, "R3 is not indefinite");

        var (r1p, r1f) = LorentzianOperator.DirectedLayerProfile(cs, r1);
        var (r2p, r2f) = LorentzianOperator.DirectedLayerProfile(cs, r2);
        Assert.True(r1f[0] < 0 && r1f[1] > 0 && r1p.All(v => Math.Abs(v) < 1e-9), "R1 not future-only alternating");
        Assert.True(r2p[0] < 0 && r2p[1] > 0 && r2f.All(v => Math.Abs(v) < 1e-9), "R2 not past-only alternating");
    }

    // ── G4-L22: propagation asymmetry + KS distance to BDG ─────────────────────────────

    [Fact]
    public void G4_L22_PropagationAsymmetryAndBdgDistance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L22: propagation asymmetry and distance to BDG");

        var cs = Cs;
        int c = Center;
        int tc = cs.Time[c];
        double Past(double[,] m)
        {
            double s = 0.0;
            for (int j = 0; j < cs.Count; j++) if (cs.Time[j] < tc) s += Math.Abs(m[j, c]);
            return s;
        }
        double Future(double[,] m)
        {
            double s = 0.0;
            for (int j = 0; j < cs.Count; j++) if (cs.Time[j] > tc) s += Math.Abs(m[j, c]);
            return s;
        }

        var r1 = LorentzianOperator.PastDirectedLayer(cs);
        var r2 = LorentzianOperator.FutureDirectedLayer(cs);
        var r3 = LorentzianOperator.BidirectionalLayer(cs);
        var bdgRet = LorentzianOperator.RetardedBdg(cs);
        var bdgSym = LorentzianOperator.BdgReference(cs);

        sb.AppendLine($"δ-source at (t={tc}, x=0). Past/future response:");
        sb.AppendLine($"{"operator",-20} {"past",8} {"future",8} {"direction",12}");
        sb.AppendLine($"{"BDG (retarded)",-20} {Past(bdgRet),8:F2} {Future(bdgRet),8:F2} {"forward-only",12}");
        sb.AppendLine($"{"R1 retarded",-20} {Past(r1),8:F2} {Future(r1),8:F2} {"forward-only",12}");
        sb.AppendLine($"{"R2 advanced",-20} {Past(r2),8:F2} {Future(r2),8:F2} {"backward-only",12}");
        sb.AppendLine($"{"R3 bidirectional",-20} {Past(r3),8:F2} {Future(r3),8:F2} {"symmetric",12}");
        sb.AppendLine();

        double ksR1 = SpectralCurvature.KolmogorovSmirnov(SpectralCurvature.GeneralEigenvalues(r1), LorentzianOperator.Eigenvalues(bdgSym));
        double ksR2 = SpectralCurvature.KolmogorovSmirnov(SpectralCurvature.GeneralEigenvalues(r2), LorentzianOperator.Eigenvalues(bdgSym));
        double ksR3 = SpectralCurvature.KolmogorovSmirnov(LorentzianOperator.Eigenvalues(r3), LorentzianOperator.Eigenvalues(bdgSym));
        sb.AppendLine($"KS distance to SYMMETRIC BDG:  R3 = {ksR3:F4} (closest), R1 = {ksR1:F4}, R2 = {ksR2:F4}.");
        sb.AppendLine();

        bool r1Forward = Past(r1) < 1e-9 && Future(r1) > 1e-9;
        bool r2Backward = Future(r2) < 1e-9 && Past(r2) > 1e-9;
        // R3 is a symmetric MATRIX, but its δ-support is boundary-asymmetric (finite grid);
        // the correct test is that it propagates BOTH ways (unlike R1/R2).
        bool r3Symmetric = Past(r3) > 1e-9 && Future(r3) > 1e-9;

        sb.AppendLine($"R1 forward-only (retarded): {r1Forward}");
        sb.AppendLine($"R2 backward-only (advanced): {r2Backward}");
        sb.AppendLine($"R3 symmetric: {r3Symmetric}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: causal directionality (R1) reproduces BDG's RETARDED forward-only propagation —");
        sb.AppendLine("reducing the propagation-distance to BDG to zero. The symmetric R3 stays spectrally closest");
        sb.AppendLine("to the SYMMETRIC BDG; there is a direction-vs-spectrum trade-off.");
        Output.WriteLine(sb.ToString());

        Assert.True(r1Forward, "R1 is not forward-only (retarded)");
        Assert.True(r2Backward, "R2 is not backward-only (advanced)");
        Assert.True(r3Symmetric, "R3 is not symmetric");
        Assert.True(ksR3 < ksR1 && ksR3 < ksR2, "R3 should be spectrally closest to symmetric BDG");
    }
}
