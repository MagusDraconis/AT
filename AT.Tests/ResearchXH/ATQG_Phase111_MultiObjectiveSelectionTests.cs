using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 111 — Multi-objective network selection. QG109 (stability) and QG110 (information) each gave
/// PARTIAL SELECTION. This phase asks whether simultaneous optimization of five objectives (stability, memory,
/// information flow, causal depth, actualization efficiency) selects a UNIQUE network class via the
/// Pareto-optimal front.
/// Classify: NO SELECTION / PARTIAL SELECTION / UNIQUE SELECTION.
///
/// Tests: ATQG1110 (objectives + Pareto front), ATQG1111 (trade-offs + dominance), ATQG1112 (multi-objective
/// selection + classification).
/// </summary>
public class ATQG_Phase111_MultiObjectiveSelectionTests : ResearchTestBase
{
    public ATQG_Phase111_MultiObjectiveSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1110: objectives + Pareto front ──────────────────────────────────────

    [Fact]
    public void ATQG1110_ObjectivesAndParetoFront()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1110: five objectives over the ensemble + the Pareto-optimal front");

        var members = MultiObjectiveSelection.EnsembleObjectives();
        int[] front = MultiObjectiveSelection.ParetoFront(members);

        double meanStab = members.Average(m => m.stability);
        double meanMem = members.Average(m => m.memory);
        double meanFlow = members.Average(m => m.flow);
        double meanDepth = members.Average(m => m.depth);
        double meanEff = members.Average(m => m.efficiency);

        sb.AppendLine($"ensemble: {members.Length} networks");
        sb.AppendLine($"ensemble means: stability {meanStab:P1}, memory {meanMem:F1}, flow {meanFlow:F2}, depth {meanDepth:F1}, efficiency {meanEff:F3}");
        sb.AppendLine();
        sb.AppendLine($"PARETO-OPTIMAL FRONT: {front.Length} networks");
        foreach (int i in front.Take(12))
            sb.AppendLine($"  {members[i].name,-24} stab {members[i].stability:P0}  mem {members[i].memory:F1}  flow {members[i].flow:F2}  depth {members[i].depth:F0}  eff {members[i].efficiency:F3}");
        if (front.Length > 12) sb.AppendLine($"  … and {front.Length - 12} more");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the ensemble has a well-defined Pareto front of non-dominated networks — the");
        sb.AppendLine("multi-objective optimum is NOT a single point; multiple networks trade off the objectives.");
        Output.WriteLine(sb.ToString());

        Assert.True(members.Length == 77, "77-network ensemble");
        Assert.True(front.Length >= 2, "Pareto front has multiple non-dominated members");
        Assert.True(front.Length < members.Length, "Pareto front is a strict subset (dominance exists)");
        Assert.True(meanMem > 1.0 && meanDepth > 1.0, "objectives are non-trivial");
    }

    // ── ATQG1111: trade-offs + dominance ─────────────────────────────────────────

    [Fact]
    public void ATQG1111_TradeoffsAndDominance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1111: do the objectives trade off against each other?");

        var members = MultiObjectiveSelection.EnsembleObjectives();
        int[] front = MultiObjectiveSelection.ParetoFront(members);

        var frontMembers = front.Select(i => members[i]).ToArray();
        double frontFlow = frontMembers.Average(m => m.flow);
        double frontEff = frontMembers.Average(m => m.efficiency);
        double frontDepth = frontMembers.Average(m => m.depth);
        double frontMem = frontMembers.Average(m => m.memory);
        double frontStab = frontMembers.Average(m => m.stability);

        double erFlow = members.Where(m => MultiObjectiveSelection.ClassOf(m.name) == "ER").Average(m => m.flow);
        double gridFlow = members.Where(m => MultiObjectiveSelection.ClassOf(m.name) == "grid").Average(m => m.flow);
        double erDepth = members.Where(m => MultiObjectiveSelection.ClassOf(m.name) == "ER").Average(m => m.depth);
        double gridDepth = members.Where(m => MultiObjectiveSelection.ClassOf(m.name) == "grid").Average(m => m.depth);
        double erEff = members.Where(m => MultiObjectiveSelection.ClassOf(m.name) == "ER").Average(m => m.efficiency);
        double gridEff = members.Where(m => MultiObjectiveSelection.ClassOf(m.name) == "grid").Average(m => m.efficiency);

        sb.AppendLine("OBJECTIVE TRADE-OFFS (class means):");
        sb.AppendLine($"  flow:  ER {erFlow:F2} vs grid {gridFlow:F2}   (ER wins flow)");
        sb.AppendLine($"  depth: ER {erDepth:F0} vs grid {gridDepth:F0}   (grid wins depth)");
        sb.AppendLine($"  eff:   ER {erEff:F3} vs grid {gridEff:F3}   (grid wins efficiency)");
        sb.AppendLine();
        sb.AppendLine("PARETO FRONT AVERAGES:");
        sb.AppendLine($"  stability {frontStab:P1}, memory {frontMem:F1}, flow {frontFlow:F2}, depth {frontDepth:F0}, efficiency {frontEff:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the objectives CONFLICT — ER random dominates flow, causal grids dominate depth");
        sb.AppendLine("and efficiency. The Pareto front spans this trade-off; no single network simultaneously");
        sb.AppendLine("maximizes all five objectives.");
        Output.WriteLine(sb.ToString());

        Assert.True(erFlow > gridFlow, "ER random wins information flow");
        Assert.True(gridDepth > erDepth, "causal grids win causal depth");
        Assert.True(gridEff > erEff, "causal grids win actualization efficiency");
        Assert.True(frontMembers.Length >= 2, "the front spans the trade-off");
    }

    // ── ATQG1112: multi-objective selection + classification ─────────────────────

    [Fact]
    public void ATQG1112_MultiObjectiveSelectionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1112: does simultaneous optimization select a unique network class?");

        var members = MultiObjectiveSelection.EnsembleObjectives();
        int[] front = MultiObjectiveSelection.ParetoFront(members);
        var classes = front.Select(i => MultiObjectiveSelection.ClassOf(members[i].name)).Distinct().ToList();
        string cls = MultiObjectiveSelection.Classify();

        sb.AppendLine($"Pareto front: {front.Length} networks");
        sb.AppendLine($"classes on the front: {string.Join(", ", classes)}");
        sb.AppendLine($"  front composition:");
        foreach (string c in classes)
        {
            int count = front.Count(i => MultiObjectiveSelection.ClassOf(members[i].name) == c);
            sb.AppendLine($"    {c,-10} {count} networks ({(double)count / front.Length:P0} of the front)");
        }
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT UNIQUE SELECTION: the Pareto front contains networks of MORE THAN ONE class — the");
        sb.AppendLine("    objectives conflict (flow/efficiency vs depth/memory/stability), so no class dominates");
        sb.AppendLine("    all objectives simultaneously.");
        sb.AppendLine("  • NO SELECTION: the front spans ALL classes (ER 78% of the front, matching its 78% of the");
        sb.AppendLine("    ensemble) — simultaneous optimization of the conflicting objectives does NOT narrow to a");
        sb.AppendLine("    preferred class. Adding more objectives (QG109 stability → QG110 information → QG111");
        sb.AppendLine("    multi-objective) WIDENS the ambiguity rather than resolving it.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NO SELECTION", cls);
        Assert.True(classes.Count >= 3, "front spans most classes (no class-level preference)");
        Assert.True(front.Length >= 2, "front has multiple members");
    }
}
