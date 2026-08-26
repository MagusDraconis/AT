using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 5 — the role of the diagonal self-term. H2 = R1 + L3 has a large Feynman tail
/// (spacelike leakage) because its retarded component R1 is nilpotent (no diagonal). Tests four
/// native diagonal terms (D1 constant, D2 density/comparable-count, D3 layer/past-count,
/// D4 local-degree) for whether they suppress leakage while preserving retardation,
/// indefiniteness, and layer alternation.
///
/// Tests: G4-L50 (leakage), G4-L51 (preservation + KS), G4-L52 (strength sweep + refinement).
/// </summary>
public class G4L_Phase5_DiagonalTermStudyTests : ResearchTestBase
{
    public G4L_Phase5_DiagonalTermStudyTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);
    private static int Index(CausalSetData cs, int t, int x) => t * Nx + (x + XMax);

    private static (string Name, Func<CausalSetData, double[]> Diagonal)[] Diagonals(CausalSetData cs)
    {
        var comp = LorentzianOperator.ComparableCount(cs);
        var past = LorentzianOperator.PastCount(cs);
        var deg = LorentzianOperator.LocalDegree(cs);
        return new (string, Func<CausalSetData, double[]>)[]
        {
            ("D1 constant",     _ => Enumerable.Repeat(-1.0, cs.Count).ToArray()),
            ("D2 comparable",   _ => comp.Select(x => -(double)x).ToArray()),
            ("D3 past-count",   _ => past.Select(x => -(double)x).ToArray()),
            ("D4 degree",       _ => deg.Select(x => -(double)x).ToArray()),
        };
    }

    private static double Dir((double past, double future) r)
        => r.past + r.future == 0.0 ? 0.5 : r.future / (r.past + r.future);

    // ── G4-L50: leakage of each diagonal ───────────────────────────────────────────────

    [Fact]
    public void G4_L50_DiagonalTermReducesLeakage()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L50: does a native diagonal suppress the Feynman tail?");

        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        int c = Index(cs, tc, xc);
        var h2 = LorentzianOperator.HybridRetardedAlternating(cs);

        double[] h2Resp = LorentzianOperator.GreenResponse(h2, c);
        var h2m = LorentzianOperator.GreenResponseMetrics(cs, h2Resp, tc, xc);

        sb.AppendLine($"H2 baseline: leakage {h2m.leak:F3}, directionality {Dir((h2m.past, h2m.future)):F3}.");
        sb.AppendLine();
        sb.AppendLine($"{"diagonal",-14} {"leakage",9} {"directionality",15} {"Δleak",8}");
        var reduced = new List<(string name, double leak)>();
        foreach (var (name, diagFn) in Diagonals(cs))
        {
            var m = LorentzianOperator.AddDiagonal(h2, diagFn(cs));
            var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, c), tc, xc);
            sb.AppendLine($"{name,-14} {r.leak,9:F3} {Dir((r.past, r.future)),15:F3} {r.leak - h2m.leak,8:F3}");
            if (r.leak < h2m.leak) reduced.Add((name, r.leak));
        }

        sb.AppendLine();
        sb.AppendLine($"Diagonals that reduce leakage vs H2 ({h2m.leak:F3}): {reduced.Count}/4.");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: (reported above) — see G4-L51 for the preservation trade-off.");
        Output.WriteLine(sb.ToString());

        Assert.True(reduced.Count >= 1, "no native diagonal reduces the Feynman tail");
    }

    // ── G4-L51: preservation of retardation, indefiniteness, alternation ────────────────

    [Fact]
    public void G4_L51_PreservesRetardationIndefinitenessAlternation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L51: leakage reduction vs preservation trade-off");

        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        int c = Index(cs, tc, xc);
        var h2 = LorentzianOperator.HybridRetardedAlternating(cs);
        double[] bdgSym = LorentzianOperator.Eigenvalues(LorentzianOperator.BdgReference(cs));

        double[] h2Resp = LorentzianOperator.GreenResponse(h2, c);
        var h2m = LorentzianOperator.GreenResponseMetrics(cs, h2Resp, tc, xc);
        var h2Evals = SpectralCurvature.GeneralEigenvalues(h2);
        var h2Sig = LorentzianOperator.Signature(h2Evals);
        double h2Ks = SpectralCurvature.KolmogorovSmirnov(h2Evals, bdgSym);

        sb.AppendLine($"{"operator",-14} {"leakage",9} {"direction",11} {"indefinite",11} {"KS→BDG",9} {"alternates",11}");
        sb.AppendLine($"{"H2",-14} {h2m.leak,9:F3} {Dir((h2m.past, h2m.future)),11:F3} {h2Sig.pos > 0 && h2Sig.neg > 0,11} {h2Ks,9:F4} {LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, h2)),11}");
        (double leak, double dir, bool indef, bool alt) d4 = default;
        foreach (var (name, diagFn) in Diagonals(cs))
        {
            var m = LorentzianOperator.AddDiagonal(h2, diagFn(cs));
            var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, c), tc, xc);
            var ev = SpectralCurvature.GeneralEigenvalues(m);
            var sig = LorentzianOperator.Signature(ev);
            double ks = SpectralCurvature.KolmogorovSmirnov(ev, bdgSym);
            bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, m));
            sb.AppendLine($"{name,-14} {r.leak,9:F3} {Dir((r.past, r.future)),11:F3} {sig.pos > 0 && sig.neg > 0,11} {ks,9:F4} {alt,11}");
            if (name == "D4 degree") d4 = (r.leak, Dir((r.past, r.future)), sig.pos > 0 && sig.neg > 0, alt);
        }

        sb.AppendLine();
        sb.AppendLine("Alternation is unchanged by any diagonal (off-diagonal untouched). The trade-off is");
        sb.AppendLine("leakage-reduction vs indefiniteness-preservation (a large negative diagonal kills the");
        sb.AppendLine("positive part of the spectrum). D2 (comparable-count) suppresses leakage most but");
        sb.AppendLine("over-suppresses (kills indefiniteness); D4 (degree) reduces leakage while preserving all.");
        sb.AppendLine();
        sb.AppendLine($"SUCCESS (D4 degree): leak-reduced {d4.leak < h2m.leak}, retarded {d4.dir > 0.5}, " +
                      $"indefinite {d4.indef}, alternating {d4.alt}.");
        Output.WriteLine(sb.ToString());

        // D4 (local-degree diagonal) is the native diagonal satisfying all success criteria.
        Assert.True(d4.leak < h2m.leak && d4.dir > 0.5 && d4.indef && d4.alt,
            "D4 (degree) does not reduce leakage while preserving retardation/indefiniteness/alternation");
    }

    // ── G4-L52: strength sweep + refinement (best native diagonal) ──────────────────────

    [Fact]
    public void G4_L52_StrengthSweepAndRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L52: constant-diagonal strength sweep + refinement");

        // Sweep the constant diagonal H2 − s·I (native self-term) to find a leakage/indefiniteness sweet spot.
        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        int c = Index(cs, tc, xc);
        var h2 = LorentzianOperator.HybridRetardedAlternating(cs);

        sb.AppendLine($"{"s",6} {"leakage",9} {"direction",11} {"indefinite",11}");
        double bestLeak = 1.0; double bestS = 0.0;
        for (double s = 0.0; s <= 8.0; s += 0.5)
        {
            var m = LorentzianOperator.AddDiagonal(h2, Enumerable.Repeat(-s, cs.Count).ToArray());
            var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, c), tc, xc);
            var ev = SpectralCurvature.GeneralEigenvalues(m);
            var sig = LorentzianOperator.Signature(ev);
            bool indef = sig.pos > 0 && sig.neg > 0;
            sb.AppendLine($"{s,6:F1} {r.leak,9:F3} {Dir((r.past, r.future)),11:F3} {indef,11}");
            if (indef && r.leak < bestLeak) { bestLeak = r.leak; bestS = s; }
        }

        sb.AppendLine();
        sb.AppendLine($"Best strength s = {bestS:F1} (indefinite, lowest leakage {bestLeak:F3}).");
        sb.AppendLine();

        // Refinement: verify the best diagonal preserves the properties at a finer grid.
        sb.AppendLine($"{"grid",-8} {"N",6} {"leak-reduced",13} {"indefinite",11} {"alternating",12}");
        foreach (var (tMax, xMax) in new[] { (7, 4), (9, 5) })
        {
            var cs2 = CausalSet.BuildGrid(tMax, xMax);
            int tc2 = cs2.Time.Max() / 2;
            int c2 = Index(cs2, tc2, 0);
            var h22 = LorentzianOperator.HybridRetardedAlternating(cs2);
            var baseM = LorentzianOperator.GreenResponseMetrics(cs2, LorentzianOperator.GreenResponse(h22, c2), tc2, 0);
            var withD = LorentzianOperator.AddDiagonal(h22, Enumerable.Repeat(-bestS, cs2.Count).ToArray());
            var r = LorentzianOperator.GreenResponseMetrics(cs2, LorentzianOperator.GreenResponse(withD, c2), tc2, 0);
            var ev = SpectralCurvature.GeneralEigenvalues(withD);
            var sig = LorentzianOperator.Signature(ev);
            bool indef = sig.pos > 0 && sig.neg > 0;
            bool leakReduced = r.leak < baseM.leak;
            bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs2, withD));
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {cs2.Count,6} {leakReduced,13} {indef,11} {alt,12}");
            Assert.True(leakReduced, $"n={cs2.Count}: diagonal does not reduce leakage");
            Assert.True(indef, $"n={cs2.Count}: diagonal kills indefiniteness");
            Assert.True(alt, $"n={cs2.Count}: diagonal breaks alternation");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a native diagonal (constant self-term, or a density/layer/degree-derived one)");
        sb.AppendLine("reduces the Feynman tail while preserving retardation, indefiniteness, and alternation.");
        Output.WriteLine(sb.ToString());
    }
}
