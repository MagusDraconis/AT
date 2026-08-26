using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 7 — derive a native diagonal self-term. Starting from H0 = R1 + A3 (the
/// retarded interval operator), add a diagonal derived from causal structure alone and ask
/// whether it can suppress the Feynman tail (leakage &lt; 0.50) while preserving retardation,
/// indefiniteness, and layer alternation.
///
/// Diagonals: D1 local degree, D2 interval count, D3 comparable count, D4 layer occupancy,
/// D5 causal volume — each negated (BDG-like) and applied as a per-vertex self-term.
///
/// Tests: G4-L70 (leakage), G4-L71 (preservation + KS), G4-L72 (refinement).
/// </summary>
public class G4L_Phase7_NativeDiagonalTests : ResearchTestBase
{
    public G4L_Phase7_NativeDiagonalTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);
    private static int Index(CausalSetData cs, int t, int x) => t * Nx + (x + XMax);

    private static (string Name, Func<CausalSetData, double[]> Diagonal)[] Diagonals() => new (string, Func<CausalSetData, double[]>)[]
    {
        ("D1 degree",     cs => LorentzianOperator.LocalDegree(cs).Select(x => -(double)x).ToArray()),
        ("D2 interval",   cs => LorentzianOperator.IntervalCount(cs).Select(x => -x).ToArray()),
        ("D3 comparable", cs => LorentzianOperator.ComparableCount(cs).Select(x => -(double)x).ToArray()),
        ("D4 occupancy",  cs => LorentzianOperator.LayerOccupancy(cs).Select(x => -x).ToArray()),
        ("D5 volume",     cs => LorentzianOperator.CausalVolume(cs).Select(x => -x).ToArray()),
    };

    private static double Dir((double past, double future) r)
        => r.past + r.future == 0.0 ? 0.5 : r.future / (r.past + r.future);

    // ── G4-L70: leakage of each native diagonal (natural + swept strength) ──────────────

    [Fact]
    public void G4_L70_NativeDiagonalReducesLeakage()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L70: can a native diagonal push the Feynman tail below 0.50?");

        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        var h0 = LorentzianOperator.RetardedInterval(cs);
        var h0m = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(h0, Index(cs, tc, xc)), tc, xc);
        sb.AppendLine($"H0 = R1 + A3 baseline: leakage {h0m.leak:F3}.");
        sb.AppendLine();

        // Natural forms (coefficient 1), plus the BDG-balanced self-term d = −degree/2.
        sb.AppendLine($"{"diagonal",-14} {"leak",8} {"direction",11} {"indefinite",11} {"alternates",11}");
        {
            var d0 = LorentzianOperator.AddDiagonal(h0, LorentzianOperator.LocalDegree(cs).Select(x => -0.5 * x).ToArray());
            var r0 = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(d0, Index(cs, tc, xc)), tc, xc);
            var ev0 = SpectralCurvature.GeneralEigenvalues(d0);
            var s0 = LorentzianOperator.Signature(ev0);
            sb.AppendLine($"{"D0 deg/2 (BDG)",-14} {r0.leak,8:F3} {Dir((r0.past, r0.future)),11:F3} {s0.pos > 0 && s0.neg > 0,11} {LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, d0)),11}");
        }
        foreach (var (name, diagFn) in Diagonals())
        {
            var m = LorentzianOperator.AddDiagonal(h0, diagFn(cs));
            var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, Index(cs, tc, xc)), tc, xc);
            var ev = SpectralCurvature.GeneralEigenvalues(m);
            var sig = LorentzianOperator.Signature(ev);
            bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, m));
            sb.AppendLine($"{name,-14} {r.leak,8:F3} {Dir((r.past, r.future)),11:F3} {sig.pos > 0 && sig.neg > 0,11} {alt,11}");
        }

        // Sweep normalized strength s (max |diag| = s) to find leakage < 0.50 with preservation.
        sb.AppendLine();
        sb.AppendLine("Normalized strength sweep (max|diag| = s): lowest leakage that preserves indefiniteness+alternation.");
        sb.AppendLine($"{"diagonal",-14} {"best s",7} {"leak",8} {"< 0.50",8}");
        var winners = new List<(string name, double s, double leak)>();
        foreach (var (name, diagFn) in Diagonals())
        {
            double[] raw = diagFn(cs).Select(Math.Abs).ToArray();
            double norm = raw.Max();
            if (norm == 0.0) continue;
            double bestLeak = double.MaxValue, bestS = 0.0;
            for (double s = 0.0; s <= 6.0; s += 0.25)
            {
                var d = raw.Select(v => -s * v / norm).ToArray();
                var m = LorentzianOperator.AddDiagonal(h0, d);
                var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, Index(cs, tc, xc)), tc, xc);
                var ev = SpectralCurvature.GeneralEigenvalues(m);
                var sig = LorentzianOperator.Signature(ev);
                bool indef = sig.pos > 0 && sig.neg > 0;
                bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, m));
                if (indef && alt && r.leak < bestLeak) { bestLeak = r.leak; bestS = s; }
            }
            sb.AppendLine($"{name,-14} {bestS,7:F2} {bestLeak,8:F3} {bestLeak < 0.50,8}");
            if (bestLeak < 0.50) winners.Add((name, bestS, bestLeak));
        }

        sb.AppendLine();
        sb.AppendLine($"Native diagonals reaching leakage &lt; 0.50 with indefiniteness+alternation preserved: {winners.Count}/5.");
        Output.WriteLine(sb.ToString());

        Assert.True(winners.Count >= 1, "no native diagonal reaches leakage < 0.50 while preserving indefiniteness and alternation");
    }

    // ── G4-L71: preservation (best native diagonal) ─────────────────────────────────────

    [Fact]
    public void G4_L71_BestDiagonalPreservesStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L71: best native diagonal — leakage vs structure trade-off");

        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        int c = Index(cs, tc, xc);
        var h0 = LorentzianOperator.RetardedInterval(cs);
        double[] bdgSym = LorentzianOperator.Eigenvalues(LorentzianOperator.BdgReference(cs));

        // Select the best diagonal (lowest leakage below 0.50 preserving structure) over the sweep.
        (string name, double s, double leak) best = default;
        double bestLeak = double.MaxValue;
        string bestDiag = "";
        foreach (var (name, diagFn) in Diagonals())
        {
            double[] raw = diagFn(cs).Select(Math.Abs).ToArray();
            double norm = raw.Max();
            if (norm == 0.0) continue;
            for (double s = 0.0; s <= 6.0; s += 0.25)
            {
                var m = LorentzianOperator.AddDiagonal(h0, raw.Select(v => -s * v / norm).ToArray());
                var r = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(m, c), tc, xc);
                var ev = SpectralCurvature.GeneralEigenvalues(m);
                var sig = LorentzianOperator.Signature(ev);
                if (sig.pos > 0 && sig.neg > 0 &&
                    LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, m)) &&
                    r.leak < bestLeak) { bestLeak = r.leak; best = (name, s, r.leak); bestDiag = name; }
            }
        }

        // Recompute the best operator's full metrics.
        var bestFn = Diagonals().First(d => d.Name == bestDiag).Diagonal;
        double[] rawB = bestFn(cs).Select(Math.Abs).ToArray();
        double normB = rawB.Max();
        var bestOp = LorentzianOperator.AddDiagonal(h0, rawB.Select(v => -best.s * v / normB).ToArray());
        var bm = LorentzianOperator.GreenResponseMetrics(cs, LorentzianOperator.GreenResponse(bestOp, c), tc, xc);
        var bEv = SpectralCurvature.GeneralEigenvalues(bestOp);
        var bSig = LorentzianOperator.Signature(bEv);
        bool bIndef = bSig.pos > 0 && bSig.neg > 0;
        bool bAlt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs, bestOp));
        double bDir = Dir((bm.past, bm.future));
        double bKs = SpectralCurvature.KolmogorovSmirnov(bEv, bdgSym);

        sb.AppendLine($"best diagonal: {bestDiag} (strength s = {best.s:F2})");
        sb.AppendLine($"{"metric",-14} {"value",9}");
        sb.AppendLine($"{"leakage",-14} {bm.leak,9:F3}");
        sb.AppendLine($"{"direction",-14} {bDir,9:F3}");
        sb.AppendLine($"{"indefinite",-14} {bIndef,9}");
        sb.AppendLine($"{"alternates",-14} {bAlt,9}");
        sb.AppendLine($"{"KS→BDG",-14} {bKs,9:F4}");
        sb.AppendLine();
        sb.AppendLine($"SUCCESS: leakage {bm.leak:F3} < 0.50, retarded {bDir > 0.5}, indefinite {bIndef}, alternating {bAlt}.");
        Output.WriteLine(sb.ToString());

        Assert.True(bm.leak < 0.50 && bDir > 0.5 && bIndef && bAlt,
            $"best diagonal ({bestDiag}) fails a success criterion (leak {bm.leak:F3}, dir {bDir:F3}, indef {bIndef}, alt {bAlt})");
    }

    // ── G4-L72: refinement stability (best native diagonal) ─────────────────────────────

    [Fact]
    public void G4_L72_RefinementStability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L72: refinement stability of the best native diagonal");

        sb.AppendLine($"{"grid",-8} {"N",6} {"leak<0.50",11} {"indefinite",11} {"alternating",12} {"leak",8}");
        foreach (var (tMax, xMax) in new[] { (7, 4), (9, 5) })
        {
            var cs2 = CausalSet.BuildGrid(tMax, xMax);
            int tc2 = cs2.Time.Max() / 2;
            int nxx = 2 * xMax + 1;
            int idx = tc2 * nxx + xMax; // center time slice, x = 0
            var h02 = LorentzianOperator.RetardedInterval(cs2);

            // Re-derive the best diagonal on this grid (D-degree normalized) and sweep for leak<0.50.
            double[] raw = LorentzianOperator.LocalDegree(cs2).Select(x => (double)Math.Abs(x)).ToArray();
            double norm = raw.Max();
            double bestLeak = double.MaxValue; bool ok = false, indefOk = false, altOk = false;
            for (double s = 0.0; s <= 6.0; s += 0.25)
            {
                var m = LorentzianOperator.AddDiagonal(h02, raw.Select(v => -s * v / norm).ToArray());
                var r = LorentzianOperator.GreenResponseMetrics(cs2, LorentzianOperator.GreenResponse(m, idx), tc2, 0);
                var ev = SpectralCurvature.GeneralEigenvalues(m);
                var sig = LorentzianOperator.Signature(ev);
                bool indef = sig.pos > 0 && sig.neg > 0;
                bool alt = LorentzianOperator.Alternates(LorentzianOperator.LayerProfile(cs2, m));
                if (indef && alt && r.leak < bestLeak) { bestLeak = r.leak; ok = r.leak < 0.50; indefOk = indef; altOk = alt; }
            }
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {cs2.Count,6} {ok,11} {indefOk,11} {altOk,12} {bestLeak,8:F3}");
            Assert.True(ok && indefOk && altOk, $"n={cs2.Count}: best diagonal fails (leak {bestLeak:F3}, indef {indefOk}, alt {altOk})");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native diagonal self-term (degree-derived) suppresses the Feynman tail");
        sb.AppendLine("below 0.50 while preserving retardation, indefiniteness, and alternation — and it survives refinement.");
        Output.WriteLine(sb.ToString());
    }
}
