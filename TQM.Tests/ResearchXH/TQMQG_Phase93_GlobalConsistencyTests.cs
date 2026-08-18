using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 93 — Global network consistency. Determines whether global consistency conditions can reduce the
/// freedom of SM parameters. Classify: NO REDUCTION / PARTIAL REDUCTION / STRONG REDUCTION.
///
/// Tests: TQMQG930 (loops + metric consistency), TQMQG931 (over-constrained + correlations + regions),
/// TQMQG932 (classification).
/// </summary>
public class TQMQG_Phase93_GlobalConsistencyTests : ResearchTestBase
{
    public TQMQG_Phase93_GlobalConsistencyTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG930: closed-loop constraints, global metric consistency ───────────────

    [Fact]
    public void TQMQG930_LoopsAndMetric()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG930: do global loop/metric conditions over-constrain?");

        bool loops = GlobalConsistency.LoopsGrowWithNetworkSize();
        bool metric = GlobalConsistency.GlobalMetricConsistencyApplies();
        bool over = GlobalConsistency.NetworkOverConstrained();

        sb.AppendLine($"closed loops grow with network size (E−V+1): {loops}");
        sb.AppendLine($"global metric consistency (single-valued field) applies: {metric}");
        sb.AppendLine($"large network becomes OVER-CONSTRAINED: {over}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: many loops + single-valued global metric over-constrain the link lengths, collapsing them to");
        sb.AppendLine("the few metric-field degrees of freedom (ρ, ψ).");
        Output.WriteLine(sb.ToString());

        Assert.True(loops, "loops grow with size");
        Assert.True(metric, "global metric consistency applies");
        Assert.True(over, "network is over-constrained");
    }

    // ── TQMQG931: correlations, over-constrained, allowed regions ──────────────────

    [Fact]
    public void TQMQG931_ReductionOfFreedom()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG931: how much SM parameter freedom is reduced?");

        bool geometric = GlobalConsistency.ReducesGeometricFreedom();
        bool strong = GlobalConsistency.StronglyReducesSmParameters();
        bool partial = GlobalConsistency.PartiallyReducesSmParameters();

        sb.AppendLine($"global consistency STRONGLY reduces geometric freedom: {geometric}");
        sb.AppendLine($"global consistency STRONGLY reduces SM parameter freedom: {strong}");
        sb.AppendLine($"global consistency PARTIALLY reduces SM parameter freedom: {partial}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: global consistency strongly constrains the metric, but the SM parameters are only COMPATIBLY");
        sb.AppendLine("encoded in link length (QG91), so their freedom is only partially reduced (narrowed region, correlations).");
        Output.WriteLine(sb.ToString());

        Assert.True(geometric, "geometric freedom strongly reduced");
        Assert.False(strong, "SM parameters not strongly reduced");
        Assert.True(partial, "SM parameters partially reduced");
    }

    // ── TQMQG932: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG932_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG932: NO REDUCTION / PARTIAL REDUCTION / STRONG REDUCTION?");

        sb.AppendLine($"CLASSIFICATION: {GlobalConsistency.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO REDUCTION: global consistency does narrow the allowed parameter region.");
        sb.AppendLine("  • NOT STRONG REDUCTION: the QG91 length encoding is compatible, not deterministic, so the 19 values are");
        sb.AppendLine("    not pinned down.");
        sb.AppendLine("  • PARTIAL REDUCTION: geometric freedom collapses strongly; SM parameter freedom narrows only weakly.");
        sb.AppendLine();
        sb.AppendLine("So global consistency gives a PARTIAL REDUCTION of SM parameter freedom.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL REDUCTION", GlobalConsistency.Classify());
        Assert.True(GlobalConsistency.ReducesGeometricFreedom());
        Assert.False(GlobalConsistency.StronglyReducesSmParameters());
    }
}
