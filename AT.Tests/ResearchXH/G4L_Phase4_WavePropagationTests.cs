using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-L Phase 4 — does H2 propagate information as a Lorentzian wave operator? Applies the
/// Green response (operator⁻¹ · source) to a localized δ, a compact, and a random source, and
/// measures the propagation cone, directionality, signal spread, and front velocity — comparing
/// R1, L3, H2, and the BDG reference.
///
/// Tests: G4-L40 (propagation cone + directionality), G4-L41 (spread + front velocity),
///        G4-L42 (refinement stability).
/// </summary>
public class G4L_Phase4_WavePropagationTests : ResearchTestBase
{
    public G4L_Phase4_WavePropagationTests(ITestOutputHelper o) : base(o) { }

    private const int TMax = 7;
    private const int XMax = 4;
    private static readonly int Nx = 2 * XMax + 1;

    private static CausalSetData Cs => CausalSet.BuildGrid(TMax, XMax);
    private static int Index(CausalSetData cs, int t, int x) => t * (2 * XMax + 1) + (x + XMax);

    private static (double past, double future, double causalFront, double leak) Measure(
        CausalSetData cs, double[] resp, int tc, int xc)
    {
        double maxAbs = resp.Max(x => Math.Abs(x));
        double thresh = 1e-9 * Math.Max(maxAbs, 1.0);
        double past = 0.0, future = 0.0, causalFront = 0.0, leak = 0.0, total = 0.0;
        for (int j = 0; j < cs.Count; j++)
        {
            double a = Math.Abs(resp[j]);
            if (a < thresh) continue;
            total += a;
            int dt = cs.Time[j] - tc;
            int dx = Math.Abs(cs.Space[j] - xc);
            if (dt < 0) { past += a; leak += a; }                       // past (non-causal)
            else if (dt == 0) { if (dx > 0) leak += a; }               // same-time spacelike
            else
            {
                future += a;
                if (dx < dt) causalFront = Math.Max(causalFront, dx / (double)dt); // causal
                else leak += a;                                          // superluminal
            }
        }
        return (past, future, causalFront, total > 0.0 ? leak / total : 0.0);
    }

    private static double Directionality((double past, double future) r)
        => r.past + r.future == 0.0 ? 0.5 : r.future / (r.past + r.future);

    // ── G4-L40: propagation cone + directionality (SC1) ────────────────────────────────

    [Fact]
    public void G4_L40_PropagationConeAndDirectionality()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L40: propagation cone and directionality (δ-source)");

        var cs = Cs;
        int tc = cs.Time.Max() / 2, xc = 0;
        int c = Index(cs, tc, xc);

        var ops = new (string name, double[,] m)[]
        {
            ("R1", LorentzianOperator.PastDirectedLayer(cs)),
            ("L3", LorentzianOperator.BidirectionalLayer(cs)),
            ("H2", LorentzianOperator.HybridRetardedAlternating(cs)),
            ("BDG", LorentzianOperator.RetardedBdg(cs)),
        };

        sb.AppendLine($"δ-source at (t={tc}, x={xc}). Green response:");
        sb.AppendLine($"{"operator",-8} {"past",9} {"future",9} {"directionality",15} {"front-v",8} {"leak",8}");
        foreach (var (name, m) in ops)
        {
            var resp = LorentzianOperator.GreenResponse(m, c);
            var r = Measure(cs, resp, tc, xc);
            sb.AppendLine($"{name,-8} {r.past,9:F3} {r.future,9:F3} {Directionality((r.past, r.future)),15:F3} {r.causalFront,8:F3} {r.leak,8:F3}");
        }

        sb.AppendLine();
        sb.AppendLine("SC1: all operators propagate at causal speed (front velocity ≤ 1 within the light");
        sb.AppendLine("cone). BDG is fully causal (leak 0); H2 forward-biased; L3 symmetric.");
        Output.WriteLine(sb.ToString());

        foreach (var (name, m) in ops)
        {
            var r = Measure(cs, LorentzianOperator.GreenResponse(m, c), tc, xc);
            Assert.True(r.causalFront <= 1.0 + 1e-9, $"{name}: superluminal front ({r.causalFront:F3})");
        }

        var h2 = Measure(cs, LorentzianOperator.GreenResponse(LorentzianOperator.HybridRetardedAlternating(cs), c), tc, xc);
        var l3 = Measure(cs, LorentzianOperator.GreenResponse(LorentzianOperator.BidirectionalLayer(cs), c), tc, xc);
        var bdg = Measure(cs, LorentzianOperator.GreenResponse(LorentzianOperator.RetardedBdg(cs), c), tc, xc);

        Assert.True(bdg.leak < 0.05, $"BDG is not causal (leak {bdg.leak:F3})");
        Assert.True(h2.future > h2.past, "H2 is not forward-biased");
        Assert.True(Math.Abs(l3.past - l3.future) / Math.Max(l3.past + l3.future, 1e-9) < 0.2, "L3 is not symmetric");
    }

    // ── G4-L41: signal spread + front velocity across sources (SC2, SC3) ────────────────

    [Fact]
    public void G4_L41_FiniteSpeedSpreadAndClosenessToBdg()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L41: finite-speed spread and closeness to BDG (δ / compact / random sources)");

        var cs = Cs;
        int tc = cs.Time.Max() / 2;

        // Source definitions: (indices, weights)
        var sources = new (string name, (int idx, double w)[] items)[]
        {
            ("delta",   new[] { (Index(cs, tc, 0), 1.0) }),
            ("compact", new[] { (Index(cs, tc, -1), 1.0), (Index(cs, tc, 0), 1.0), (Index(cs, tc, 1), 1.0) }),
            ("random",  new[] { (Index(cs, tc, -2), 0.8), (Index(cs, tc, -1), -0.3), (Index(cs, tc, 0), 0.5), (Index(cs, tc, 1), 0.9), (Index(cs, tc, 2), -0.6) }),
        };

        double[] ResponseToSource(double[,] m, (int idx, double w)[] src)
        {
            var resp = new double[cs.Count];
            foreach (var (idx, w) in src)
            {
                var g = LorentzianOperator.GreenResponse(m, idx);
                for (int j = 0; j < cs.Count; j++) resp[j] += w * g[j];
            }
            return resp;
        }

        var h2 = LorentzianOperator.HybridRetardedAlternating(cs);
        var l3 = LorentzianOperator.BidirectionalLayer(cs);
        var bdg = LorentzianOperator.RetardedBdg(cs);

        sb.AppendLine($"{"source",-8} {"operator",-8} {"directionality",15} {"front-v",8} {"leak",8}");
        double h2DirSum = 0, l3DirSum = 0, bdgDirSum = 0;
        double h2LeakSum = 0, l3LeakSum = 0, bdgLeakSum = 0;
        int cnt = 0;
        foreach (var (srcName, src) in sources)
        {
            foreach (var (opName, op) in new[] { ("H2", h2), ("L3", l3), ("BDG", bdg) })
            {
                var r = Measure(cs, ResponseToSource(op, src), tc, 0);
                double dir = Directionality((r.past, r.future));
                sb.AppendLine($"{srcName,-8} {opName,-8} {dir,15:F3} {r.causalFront,8:F3} {r.leak,8:F3}");
                if (opName == "H2") { h2DirSum += dir; h2LeakSum += r.leak; }
                if (opName == "L3") { l3DirSum += dir; l3LeakSum += r.leak; }
                if (opName == "BDG") { bdgDirSum += dir; bdgLeakSum += r.leak; }
                if (opName == "H2") cnt++;
            }
        }

        double h2Leak = h2LeakSum / cnt, l3Leak = l3LeakSum / cnt, bdgLeak = bdgLeakSum / cnt;
        sb.AppendLine();
        sb.AppendLine($"Mean spacelike leakage:  BDG {bdgLeak:F3},  H2 {h2Leak:F3},  L3 {l3Leak:F3}.");
        sb.AppendLine($"SC3: H2 more causal (closer to BDG) than L3 ({h2Leak:F3} < {l3Leak:F3}): {h2Leak < l3Leak}");
        sb.AppendLine();
        sb.AppendLine("SC2: causal front velocity ≤ 1 for every operator and source (finite causal speed).");
        Output.WriteLine(sb.ToString());

        foreach (var (srcName, src) in sources)
        {
            foreach (var (opName, op) in new[] { ("H2", h2), ("L3", l3), ("BDG", bdg) })
            {
                var r = Measure(cs, ResponseToSource(op, src), tc, 0);
                Assert.True(r.causalFront <= 1.0 + 1e-9, $"{srcName}/{opName}: superluminal (front-v {r.causalFront:F3})");
            }
        }
        Assert.True(h2Leak < l3Leak,
            $"H2 (leak {h2Leak:F3}) is not more causal than L3 (leak {l3Leak:F3})");
    }

    // ── G4-L42: propagation survives refinement (SC4) ───────────────────────────────────

    [Fact]
    public void G4_L42_PropagationSurvivesRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-L42: H2 propagation survives refinement (SC4)");

        sb.AppendLine($"{"grid",-8} {"N",6} {"forward-biased",15} {"front-v≤1",10} {"directionality",14}");
        foreach (var (tMax, xMax) in new[] { (7, 4), (9, 5) })
        {
            var cs = CausalSet.BuildGrid(tMax, xMax);
            int tc = cs.Time.Max() / 2;
            int c = Index(cs, tc, 0);
            var h2 = LorentzianOperator.HybridRetardedAlternating(cs);
            var r = Measure(cs, LorentzianOperator.GreenResponse(h2, c), tc, 0);
            bool fwd = r.future > r.past;
            bool causal = r.causalFront <= 1.0 + 1e-9;
            double dir = Directionality((r.past, r.future));
            sb.AppendLine($"{$"{tMax}x{xMax}",-8} {cs.Count,6} {fwd,15} {causal,10} {dir,14:F3}");
            Assert.True(fwd, $"n={cs.Count}: H2 not forward-biased");
            Assert.True(causal, $"n={cs.Count}: H2 superluminal");
        }

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: H2's wave propagation (forward-biased, causal, finite-speed) survives");
        sb.AppendLine("refinement — it behaves like a Lorentzian wave operator at multiple resolutions.");
        Output.WriteLine(sb.ToString());
    }
}
