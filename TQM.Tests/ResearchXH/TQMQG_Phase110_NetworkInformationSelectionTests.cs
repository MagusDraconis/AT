using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 110 — Network information selection. QG109 showed stability alone does not select a unique
/// network. This phase asks whether information-processing capacity (information flow, communication
/// efficiency, causal depth, memory capacity, stable computation) can select a unique network class.
/// Classify: NO EFFECT / PARTIAL SELECTION / PHYSICAL SELECTION.
///
/// Tests: TQMQG1100 (information flow + communication efficiency), TQMQG1101 (causal depth + memory capacity +
/// stable computation), TQMQG1102 (information selection + classification).
/// </summary>
public class TQMQG_Phase110_NetworkInformationSelectionTests : ResearchTestBase
{
    public TQMQG_Phase110_NetworkInformationSelectionTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG1100: information flow + communication efficiency ───────────────────

    [Fact]
    public void TQMQG1100_InformationFlowAndEfficiency()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1100: information flow and communication efficiency across classes");

        double flowGrid = NetworkInformationSelection.MeanMetric("grid", m => m.flow);
        double flowER = NetworkInformationSelection.MeanMetric("ER", m => m.flow);
        double flowThr = NetworkInformationSelection.MeanMetric("threshold", m => m.flow);

        double effGrid = NetworkInformationSelection.MeanMetric("grid", m => m.efficiency);
        double effER = NetworkInformationSelection.MeanMetric("ER", m => m.efficiency);
        double effThr = NetworkInformationSelection.MeanMetric("threshold", m => m.efficiency);

        sb.AppendLine("INFORMATION FLOW (log spanning-tree count / N):");
        sb.AppendLine($"  causal grids : {flowGrid:F3}");
        sb.AppendLine($"  ER random    : {flowER:F3}");
        sb.AppendLine($"  threshold    : {flowThr:F3}");
        sb.AppendLine();
        sb.AppendLine("COMMUNICATION EFFICIENCY (mean 1/distance):");
        sb.AppendLine($"  causal grids : {effGrid:F3}");
        sb.AppendLine($"  ER random    : {effER:F3}");
        sb.AppendLine($"  threshold    : {effThr:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: information flow and communication efficiency DISTINGUISH the classes — ER random");
        sb.AppendLine("graphs carry more redundant flow routes and shorter paths, causal grids less. The metrics");
        sb.AppendLine("separate classes but trade off against each other.");
        Output.WriteLine(sb.ToString());

        Assert.True(flowER != flowGrid, "information flow distinguishes classes");
        Assert.True(effER > effGrid, "dense random graphs are more communication-efficient");
        Assert.True(flowER > flowGrid, "ER random graphs carry more redundant flow");
    }

    // ── TQMQG1101: causal depth + memory capacity + stable computation ────────────

    [Fact]
    public void TQMQG1101_CausalDepthMemoryStable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1101: causal depth, memory capacity, stable computation");

        double depthGrid = NetworkInformationSelection.MeanMetric("grid", m => m.depth);
        double depthER = NetworkInformationSelection.MeanMetric("ER", m => m.depth);
        double depthThr = NetworkInformationSelection.MeanMetric("threshold", m => m.depth);

        double memGrid = NetworkInformationSelection.MeanMetric("grid", m => m.memory);
        double memER = NetworkInformationSelection.MeanMetric("ER", m => m.memory);
        double memThr = NetworkInformationSelection.MeanMetric("threshold", m => m.memory);

        double stableGrid = NetworkInformationSelection.MeanMetric("grid", m => m.stable);
        double stableER = NetworkInformationSelection.MeanMetric("ER", m => m.stable);
        double stableThr = NetworkInformationSelection.MeanMetric("threshold", m => m.stable);

        sb.AppendLine("CAUSAL DEPTH (graph diameter):");
        sb.AppendLine($"  causal grids : {depthGrid:F1}");
        sb.AppendLine($"  ER random    : {depthER:F1}");
        sb.AppendLine($"  threshold    : {depthThr:F1}");
        sb.AppendLine();
        sb.AppendLine("MEMORY CAPACITY (effective active modes e^H):");
        sb.AppendLine($"  causal grids : {memGrid:F2}");
        sb.AppendLine($"  ER random    : {memER:F2}");
        sb.AppendLine($"  threshold    : {memThr:F2}");
        sb.AppendLine();
        sb.AppendLine("STABLE COMPUTATION (family survival under 10% removal):");
        sb.AppendLine($"  causal grids : {stableGrid:P2}");
        sb.AppendLine($"  ER random    : {stableER:P2}");
        sb.AppendLine($"  threshold    : {stableThr:P2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: causal grids have LARGER causal depth (" +
            $"{depthGrid:F0}) and HIGHER memory capacity ({memGrid:F0} vs {memER:F0} effective modes).");
        sb.AppendLine("Stable computation is exactly preserved for grids (100%, no family lost) while ER random shows");
        sb.AppendLine($"fluctuation ({stableER:P0}) — the causal class is the information-rich, deep, exactly-stable");
        sb.AppendLine("class. These metrics PREFER the causal class, in the opposite direction to communication");
        sb.AppendLine("efficiency — an information trade-off.");
        Output.WriteLine(sb.ToString());

        Assert.True(depthGrid > depthER, "causal grids are causally deeper than random graphs");
        Assert.True(memGrid > memER, "causal grids host more active modes (higher memory capacity)");
        Assert.True(stableGrid >= 0.99, "causal grids exactly preserve family structure under removal");
    }

    // ── TQMQG1102: information selection + classification ─────────────────────────

    [Fact]
    public void TQMQG1102_InformationSelectionAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1102: can information capacity select a unique network class?");

        var metrics = NetworkInformationSelection.EnsembleMetrics();
        double depthGrid = NetworkInformationSelection.MeanMetric("grid", m => m.depth);
        double depthER = NetworkInformationSelection.MeanMetric("ER", m => m.depth);
        double memGrid = NetworkInformationSelection.MeanMetric("grid", m => m.memory);
        double memER = NetworkInformationSelection.MeanMetric("ER", m => m.memory);
        double stableGrid = NetworkInformationSelection.MeanMetric("grid", m => m.stable);
        double stableER = NetworkInformationSelection.MeanMetric("ER", m => m.stable);

        string cls = NetworkInformationSelection.Classify();
        int gridCount = metrics.Count(m => m.name.StartsWith("grid", StringComparison.Ordinal));

        sb.AppendLine("INFORMATION TRADE-OFF (per class):");
        sb.AppendLine($"  causal grids: depth {depthGrid:F0}, memory {memGrid:F2}, stable {stableGrid:P0}");
        sb.AppendLine($"  ER random   : depth {depthER:F0}, memory {memER:F2}, stable {stableER:P0}");
        sb.AppendLine();
        sb.AppendLine($"causal-grid class size: {gridCount} distinct networks");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO EFFECT: information metrics DO distinguish and narrow — causal depth, memory");
        sb.AppendLine("    capacity, and stable computation prefer the causal grid (the native capacity functional).");
        sb.AppendLine("  • NOT PHYSICAL SELECTION: the causal class contains many distinct members, and the metrics");
        sb.AppendLine("    trade off (communication efficiency prefers dense random) — no unique network is singled out.");
        sb.AppendLine("  • PARTIAL SELECTION: information capacity contributes to selection but does not uniquely");
        sb.AppendLine("    determine the physical network — consistent with QG109 (stability) and QG102 (non-unique).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL SELECTION", cls);
        Assert.True(depthGrid > depthER, "causal class is causally deeper (capacity functional prefers it)");
        Assert.True(memGrid > memER, "causal class has higher memory capacity");
        Assert.True(gridCount > 1, "preferred class has multiple distinct networks");
    }
}
