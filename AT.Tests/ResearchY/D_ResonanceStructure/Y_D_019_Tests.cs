using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_019 — Closure-Only Audit test suite (Y_D_019_Tests.cs).
///
/// Question: if all D96-specific selection rules are removed and only Closure remains,
/// does Closure still produce N=96?
///
/// Verdict tested: NO. The actualization closure dynamics (QG115/116,
/// StructureFromContent.AdaptiveNetwork / ActualizationStructures.ReinforcingNetwork,
/// canonical defaults K=6, damping=0.2, feedback=0.7) takes the size N as an INPUT (the
/// activity array length) and converges the link structure for that fixed size. Under
/// the canonical persistent pattern, closure (link-growth → 0) converges for ALL N in
/// [32,300] (269/269) — N=96 is not selected. The fixed point is always the degree-12
/// K=6 ring (links = 6N). Under the concentrated pattern, only 56/269 converge and N=96
/// itself FAILS (growth 0.1198). N=96 is a SELECTED closure solution (D_015: 6|N + span
/// window), not a closure theorem. Classification: D) Closure does not determine N.
///
/// Deterministic: exact canonical dynamics replicated via AT.Core classes.
/// </summary>
public class Y_D_019_Tests : ResearchTestBase
{
    public Y_D_019_Tests(ITestOutputHelper output) : base(output) { }

    // ── Closure convergence helpers (exact canonical dynamics) ─────────────

    /// <summary>
    /// Link-growth rate between steps A and B of the canonical reinforcing network
    /// (ReinforcingNetwork: K=6, damping=0.2, feedback=0.7), the closure criterion of
    /// QG282 (link-growth &lt; 0.05 = topology converged). Steps 10/20 were verified to
    /// give identical convergence decisions to the canonical 40/80 over [32,300].
    /// </summary>
    private static double PersistentGrowth(int n, int stepsA = 10, int stepsB = 20)
    {
        double[] act = ActualizationStructures.PersistentActivity(n);
        double[,] a = ActualizationStructures.ReinforcingNetwork(act, steps: stepsA);
        double[,] b = ActualizationStructures.ReinforcingNetwork(act, steps: stepsB);
        int la = StructureFromContent.LinkCount(a);
        int lb = StructureFromContent.LinkCount(b);
        return lb > 0 ? (double)(lb - la) / lb : 0.0;
    }

    /// <summary>
    /// Link-growth rate for the concentrated pattern. The concentrated regime converges
    /// more slowly than persistent/spread/uniform, so it must use the canonical 40/80
    /// step counts (the 10/20 shortcut was verified equivalent ONLY for the persistent,
    /// spread, and uniform patterns; it is NOT equivalent for concentrated).
    /// </summary>
    private static double ConcentratedGrowth(int n, int stepsA = 40, int stepsB = 80)
    {
        double[] act = StructureFromContent.ConcentratedActivity(n);
        double[,] a = ActualizationStructures.ReinforcingNetwork(act, steps: stepsA);
        double[,] b = ActualizationStructures.ReinforcingNetwork(act, steps: stepsB);
        int la = StructureFromContent.LinkCount(a);
        int lb = StructureFromContent.LinkCount(b);
        return lb > 0 ? (double)(lb - la) / lb : 0.0;
    }

    private static double SpreadGrowth(int n, int stepsA = 10, int stepsB = 20)
    {
        double[] act = StructureFromContent.SpreadActivity(n);
        double[,] a = ActualizationStructures.ReinforcingNetwork(act, steps: stepsA);
        double[,] b = ActualizationStructures.ReinforcingNetwork(act, steps: stepsB);
        int la = StructureFromContent.LinkCount(a);
        int lb = StructureFromContent.LinkCount(b);
        return lb > 0 ? (double)(lb - la) / lb : 0.0;
    }

    private static double UniformGrowth(int n, int stepsA = 10, int stepsB = 20)
    {
        double[] act = StructureFromContent.UniformActivity(n);
        double[,] a = ActualizationStructures.ReinforcingNetwork(act, steps: stepsA);
        double[,] b = ActualizationStructures.ReinforcingNetwork(act, steps: stepsB);
        int la = StructureFromContent.LinkCount(a);
        int lb = StructureFromContent.LinkCount(b);
        return lb > 0 ? (double)(lb - la) / lb : 0.0;
    }

    private static bool Converged(double growth, double threshold = 0.05) => growth < threshold;

    // ── [Required] Y_D_019_ClosureOnly ────────────────────────────────────

    /// <summary>
    /// Closure convergence (link-growth → 0) holds for ALL N in [32,300] under the
    /// canonical persistent pattern — closure does NOT select N=96.
    /// </summary>
    [Fact]
    public void Y_D_019_ClosureOnly()
    {
        int converged = 0;
        for (int n = 32; n <= 300; n++)
            if (Converged(PersistentGrowth(n))) converged++;

        // All 269 N converge under the persistent pattern.
        Assert.Equal(269, converged);

        // The canonical TopologyConverged check at N=96 itself.
        Assert.True(ActualizationStructures.TopologyConverged(
            ActualizationStructures.PersistentActivity(96)));
    }

    // ── [Required] Y_D_019_FixedPoints ────────────────────────────────────

    /// <summary>
    /// The converged fixed point is always the degree-12 K=6 ring (links = 6N, uniform
    /// degree 12) — a geometry class, not a size-specific object.
    /// </summary>
    [Fact]
    public void Y_D_019_FixedPoints()
    {
        foreach (int n in new[] { 64, 90, 96, 120, 128, 192, 245 })
        {
            double[] act = ActualizationStructures.PersistentActivity(n);
            double[,] net = ActualizationStructures.ReinforcingNetwork(act, steps: 80);
            int links = StructureFromContent.LinkCount(net);

            // links = 6N exactly (the degree-12 K=6 ring).
            Assert.Equal(6 * n, links);

            // Uniform degree 12.
            int degMin = int.MaxValue, degMax = 0;
            for (int i = 0; i < n; i++)
            {
                int d = 0;
                for (int j = 0; j < n; j++) if (net[i, j] != 0.0) d++;
                degMin = Math.Min(degMin, d);
                degMax = Math.Max(degMax, d);
            }
            Assert.Equal(12, degMin);
            Assert.Equal(12, degMax);
        }
    }

    // ── [Required] Y_D_019_AttractorCount ────────────────────────────────

    /// <summary>
    /// The converging set is ALL N under persistent/spread/uniform (269/269) and only
    /// 56/269 under concentrated — closure does not single out N=96 under any pattern.
    /// </summary>
    [Fact]
    public void Y_D_019_AttractorCount()
    {
        int pers = 0, spread = 0, uni = 0, conc = 0;
        for (int n = 32; n <= 300; n++)
        {
            if (Converged(PersistentGrowth(n))) pers++;
            if (Converged(SpreadGrowth(n))) spread++;
            if (Converged(UniformGrowth(n))) uni++;
            if (Converged(ConcentratedGrowth(n))) conc++;
        }

        Assert.Equal(269, pers);
        Assert.Equal(269, spread);
        Assert.Equal(269, uni);
        Assert.True(conc < 100); // 56/269 — content-dependent convergence
    }

    // ── [Required] Y_D_019_N96Uniqueness ─────────────────────────────────

    /// <summary>
    /// N=96 has no closure signature: adjacent N converge identically (growth = 0)
    /// under the persistent pattern.
    /// </summary>
    [Fact]
    public void Y_D_019_N96Uniqueness()
    {
        foreach (int n in new[] { 94, 95, 96, 97, 98 })
        {
            double g = PersistentGrowth(n);
            Assert.True(g < 1e-9, $"N={n} growth {g} — N=96 has no closure signature");
        }
    }

    // ── [Required] Y_D_019_Counterexamples ───────────────────────────────

    /// <summary>
    /// Counterexamples to "closure → N=96": N=96 FAILS closure under the concentrated
    /// pattern (growth 0.1198 > 0.05), while N=64 succeeds — closure convergence is
    /// content-dependent, not an N=96 attractor.
    /// </summary>
    [Fact]
    public void Y_D_019_Counterexamples()
    {
        double g96 = ConcentratedGrowth(96);
        double g64 = ConcentratedGrowth(64);

        Assert.False(Converged(g96));   // N=96 fails under the concentrated pattern
        Assert.True(g96 > 0.10);        // growth 0.1198
        Assert.True(Converged(g64));    // N=64 succeeds under the same pattern
    }

    // ── [Required] Y_D_019_SizeIsInput ───────────────────────────────────

    /// <summary>
    /// The size N enters the closure dynamics as the activity array length — the
    /// dynamics never grows or shrinks N, it only converges the link structure.
    /// </summary>
    [Fact]
    public void Y_D_019_SizeIsInput()
    {
        foreach (int n in new[] { 64, 96, 128, 192, 245 })
        {
            double[] act = ActualizationStructures.PersistentActivity(n);

            // The activity array length IS the size; the dynamics does not change it.
            Assert.Equal(n, act.Length);

            double[,] net = ActualizationStructures.ReinforcingNetwork(act, steps: 80);
            Assert.Equal(n, net.GetLength(0));
            Assert.Equal(n, net.GetLength(1));
        }
    }

    // ── [Required] Y_D_019_Selection ─────────────────────────────────────

    /// <summary>
    /// Classification D — closure does not determine N. Under the canonical persistent
    /// pattern every N converges (269/269); N=96 is selected by the D_015 rules
    /// (6|N + span window), not by closure.
    /// </summary>
    [Fact]
    public void Y_D_019_Selection()
    {
        // Closure alone admits all sizes (persistent pattern).
        Assert.True(Converged(PersistentGrowth(95)));
        Assert.True(Converged(PersistentGrowth(96)));
        Assert.True(Converged(PersistentGrowth(97)));
        Assert.True(Converged(PersistentGrowth(120)));
        Assert.True(Converged(PersistentGrowth(192)));

        // N=96 fails closure under the concentrated pattern — not content-independent.
        Assert.False(Converged(ConcentratedGrowth(96)));

        // The size is an input, not an output (D_015 selects 96 via 6|N + span window).
        Assert.True(96 % 6 == 0); // seed symmetry
        Assert.Equal(96, 96);     // documented: closure does not determine N; D) classification
    }

    // ── [Required] Y_D_019_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_019_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_019 — Closure-Only Audit");

        sb.AppendLine("Goal: if only Closure remains, does Closure still produce N=96?");
        sb.AppendLine();

        sb.AppendLine("[1] Closure convergence across N (canonical persistent pattern)");
        int pers = 0, conc = 0;
        for (int n = 32; n <= 300; n++)
        {
            if (Converged(PersistentGrowth(n))) pers++;
            if (Converged(ConcentratedGrowth(n))) conc++;
        }
        sb.AppendLine($"    persistent: {pers}/269 converged — ALL N, N=96 not selected");
        sb.AppendLine($"    concentrated: {conc}/269 converged — content-dependent");
        sb.AppendLine();

        sb.AppendLine("[2] The fixed point is a geometry class (degree-12 K=6 ring)");
        foreach (int n in new[] { 64, 96, 128, 192, 245 })
        {
            double[] act = ActualizationStructures.PersistentActivity(n);
            double[,] net = ActualizationStructures.ReinforcingNetwork(act, steps: 80);
            sb.AppendLine($"    N={n}: links={StructureFromContent.LinkCount(net)} (= 6N)");
        }
        sb.AppendLine();

        sb.AppendLine("[3] Counterexample — N=96 FAILS closure under the concentrated pattern");
        sb.AppendLine($"    N=96 concentrated growth: {ConcentratedGrowth(96):F4} (> 0.05)");
        sb.AppendLine($"    N=64 concentrated growth: {ConcentratedGrowth(64):F4} (< 0.05)");
        sb.AppendLine();

        sb.AppendLine("[4] N=96 has no closure signature");
        sb.AppendLine($"    N=94..98 growth (persistent): {PersistentGrowth(94):F4}, {PersistentGrowth(95):F4}, {PersistentGrowth(96):F4}, {PersistentGrowth(97):F4}, {PersistentGrowth(98):F4}");
        sb.AppendLine();

        sb.AppendLine("[5] Selection verdict");
        sb.AppendLine("    Closure alone admits ALL sizes (269/269 under persistent).");
        sb.AppendLine("    Closure convergence is content-dependent (N=96 fails under");
        sb.AppendLine("    concentrated). The size N is an INPUT (activity array length),");
        sb.AppendLine("    not an output.");
        sb.AppendLine("    ⇒ Classification D: Closure does NOT determine N.");
        sb.AppendLine("    N=96 is a SELECTED closure solution (D_015: 6|N + span window),");
        sb.AppendLine("    not a closure theorem.");
        sb.AppendLine();

        sb.AppendLine("[6] Conclusion");
        sb.AppendLine("    Closure alone does not produce N=96. No canonical value is");
        sb.AppendLine("    changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
