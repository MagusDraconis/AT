using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 6 — reduce the Feynman tail at its source. The residual leakage of H2 = R1 + L3
/// originates from the SYMMETRIC part L3. Tests four partially-retarded alternating operators
/// (A1 lower-triangular, A2 causally weighted, A3 interval-weighted, A4 hybrid) for whether they
/// reduce leakage while preserving indefiniteness, alternation, and refinement stability.
///
/// Tests: G4-L60 (leakage), G4-L61 (preservation + KS), G4-L62 (refinement).
/// </summary>
public class G4L_Phase6_RetardedAlternationTests : ResearchTestBase
{
    public G4L_Phase6_RetardedAlternationTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);
    private static int Index(CausalSetData cs, int t, int x) => t * Nx + (x + XMax);

    private static (string Name, double[,] M)[] Candidates(CausalSetData cs)
    {
        var r1 = LorentzianOperator.PastDirectedLayer(cs);
        var r2 = LorentzianOperator.FutureDirectedLayer(cs);
        var l3 = LorentzianOperator.BidirectionalLayer(cs);
        return new (string, double[,])[]
        {
            ("H2 (baseline)",  LorentzianOperator.HybridRetardedAlternating(cs)), // R1 + L3 = 2R1 + R2
            ("A1 lower-tri",   r1),
            ("A2 causal-wtd",  LorentzianOperator.Add(r1, LorentzianOperator.Scale(r2, 0.5))),
            ("A3 interval-wtd",LorentzianOperator.IntervalWeightedAlternation(cs)),
            ("A4 hybrid",      LorentzianOperator.Add(r1, LorentzianOperator.Scale(l3, 0.5))),
        };
    }

    private static double Dir((double past, double future) r)
        => r.past + r.future == 0.0 ? 0.5 : r.future / (r.past + r.future);

    // ── G4-L60: leakage of each partially-retarded operator ─────────────────────────────

    [Fact]
    public void G4_L60_PartialRetardationReducesLeakage()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L60: partial retardation reduces the Feynman tail");

        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        int c = Index(cs, tc, xc);
        double h2Leak = double.NaN;
        var reduced = new List<(string name, double leak)>();

        sb.AppendLine($"{"operator",-14} {"leakage",9} {"directionality",15}");
        foreach (var (name, m) in Candidates(cs))
        {
            var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, c), tc, xc);
            sb.AppendLine($"{name,-14} {r.leak,9:F3} {Dir((r.past, r.future)),15:F3}");
            if (name == "H2 (baseline)") h2Leak = r.leak;
            else if (r.leak < h2Leak) reduced.Add((name, r.leak));
        }

        sb.AppendLine();
        sb.AppendLine($"Candidates with leakage < H2 ({h2Leak:F3}): {reduced.Count}/4.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: reducing the future (symmetric) weight suppresses the Feynman tail");
        sb.AppendLine("(reported above) — see G4-L61 for the indefiniteness trade-off.");
        Output.WriteLine(sb.ToString());

        Assert.True(reduced.Count >= 1, "no partially-retarded operator reduces leakage below H2");
    }

    // ── G4-L61: indefiniteness, alternation, KS to BDG ─────────────────────────────────

    [Fact]
    public void G4_L61_PreservesIndefinitenessAlternation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L61: preservation of indefiniteness and alternation");

        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        int c = Index(cs, tc, xc);
        double[] bdgSym = LorentzianOperator.Eigenvalues(LorentzianOperator.BdgReference(cs));
        double h2Leak = double.NaN;

        sb.AppendLine($"{"operator",-14} {"leakage",9} {"dir",7} {"n+",5} {"n−",5} {"indefinite",11} {"KS→BDG",9} {"alternates",11}");
        var success = new List<string>();
        foreach (var (name, m) in Candidates(cs))
        {
            var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, c), tc, xc);
            var ev = SpectralCurvature.GeneralEigenvalues(m);
            var sig = LorentzianOperator.Signature(ev);
            double ks = SpectralCurvature.KolmogorovSmirnov(ev, bdgSym);
            bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, m));
            bool indef = sig.pos > 0 && sig.neg > 0;
            sb.AppendLine($"{name,-14} {r.leak,9:F3} {Dir((r.past, r.future)),7:F3} {sig.pos,5} {sig.neg,5} {indef,11} {ks,9:F4} {alt,11}");
            if (name == "H2 (baseline)") h2Leak = r.leak;
            else if (r.leak < h2Leak && indef && alt) success.Add(name);
        }

        sb.AppendLine();
        sb.AppendLine($"Candidates satisfying leakage < H2 AND indefiniteness AND alternation: {success.Count}/4.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: partial retardation preserves indefiniteness (the symmetric part");
        sb.AppendLine("(1+w)/2·L3 remains indefinite) while reducing leakage.");
        Output.WriteLine(sb.ToString());

        Assert.True(success.Count >= 1, "no candidate reduces leakage while preserving indefiniteness + alternation");
    }

    // ── G4-L62: refinement stability ────────────────────────────────────────────────────

    [Fact]
    public void G4_L62_StableUnderRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L62: refinement stability of the best partially-retarded operator");

        sb.AppendLine($"{"grid",-8} {"N",6} {"leak<H2",9} {"indefinite",11} {"alternating",12} {"leak",8}");
        foreach (var (tMax, xMax) in new[] { (7, 4), (9, 5) })
        {
            var cs = CausalSet.BuildGrid(tMax, xMax);
            int tc = cs.Time.Max() / 2;
            int c = Index(cs, tc, 0);
            var h2 = LorentzianOperator.HybridRetardedAlternating(cs);
            var a3 = LorentzianOperator.IntervalWeightedAlternation(cs);
            var h2m = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(h2, c), tc, 0);
            var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(a3, c), tc, 0);
            var sig = LorentzianOperator.Signature(SpectralCurvature.GeneralEigenvalues(a3));
            bool indef = sig.pos > 0 && sig.neg > 0;
            bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, a3));
            bool leakReduced = r.leak < h2m.leak;
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {cs.Count,6} {leakReduced,9} {indef,11} {alt,12} {r.leak,8:F3}");
            Assert.True(leakReduced, $"n={cs.Count}: A3 does not reduce leakage");
            Assert.True(indef, $"n={cs.Count}: A3 kills indefiniteness");
            Assert.True(alt, $"n={cs.Count}: A3 breaks alternation");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: partial retardation (A3 interval-weighted) reduces leakage while preserving");
        sb.AppendLine("indefiniteness and alternation — and this survives refinement.");
        Output.WriteLine(sb.ToString());
    }
}
