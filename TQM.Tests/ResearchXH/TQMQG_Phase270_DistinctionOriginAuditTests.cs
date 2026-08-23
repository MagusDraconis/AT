using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 270 — Distinction Origin Audit. What is being distinguished, and does distinction arise
/// from structure, actualization, or a deeper principle? D96 only, no observables.
/// </summary>
public class TQMQG_Phase270_DistinctionOriginAuditTests : ResearchTestBase
{
    public TQMQG_Phase270_DistinctionOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2700_PositionCandidates()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2700: causal vs network position");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - causal positions ARE distinguishable (distinct shares ρ_k = μ^k/S);");
        sb.AppendLine("  - network positions are NOT (the network is regular — no structural labels).");
        sb.AppendLine();

        sb.AppendLine("CAUSAL POSITION (μ=2, K=8):");
        foreach (var (k, share) in DistinctionOriginAudit.CausalShares(2.0, 8))
            sb.AppendLine($"  generation {k}: share {share:F4}");
        sb.AppendLine($"causal positions distinct (μ≠1): {DistinctionOriginAudit.CausalPositionsDistinct(2.0, 8)}");
        sb.AppendLine($"critical shares uniform (μ=1): {DistinctionOriginAudit.CriticalSharesUniform(8)}");
        sb.AppendLine();
        sb.AppendLine("NETWORK POSITION:");
        sb.AppendLine($"network regular (all degree {InvariantOriginAudit.CommonDegree()}): {DistinctionOriginAudit.NetworkProvidesNoDistinction()}");
        sb.AppendLine("→ all 96 nodes structurally identical — NO network-position distinction");

        Output.WriteLine(sb.ToString());

        Assert.True(DistinctionOriginAudit.CausalPositionsDistinct(2.0, 8), "causal positions distinguishable");
        Assert.True(DistinctionOriginAudit.NetworkProvidesNoDistinction(), "regular graph — no structural labels");
    }

    [Fact]
    public void TQMQG2701_DifferenceStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2701: the difference structure — what is distinguished");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - a Q-event is a before→after transition (the event IS a difference);");
        sb.AppendLine("  - the D96 spectrum: one zero mode (background) + positive modes (differences).");
        sb.AppendLine();

        sb.AppendLine($"state difference exists (projection): {DistinctionOriginAudit.StateDifferenceExists()}");
        sb.AppendLine($"state difference is binary (tick/no-tick): {DistinctionOriginAudit.StateDifferenceIsBinary()}");
        sb.AppendLine($"actualization IS a difference (network transition): {DistinctionOriginAudit.ActualizationIsDifference()}");
        sb.AppendLine($"ρ counts differences (events): {DistinctionOriginAudit.RhoCountsDifferences()}");
        sb.AppendLine();
        sb.AppendLine("D96 DIFFERENCE STRUCTURE:");
        sb.AppendLine($"zero mode in kernel (background): {DistinctionOriginAudit.ZeroModeBackground()}");
        sb.AppendLine($"positive modes (differences from background): {DistinctionOriginAudit.PositiveModeCount()}");
        sb.AppendLine($"distinct frequencies (44 groups): {DistinctionOriginAudit.DistinctFrequencies()}");

        Output.WriteLine(sb.ToString());

        Assert.True(DistinctionOriginAudit.StateDifferenceExists());
        Assert.True(DistinctionOriginAudit.ActualizationIsDifference());
        Assert.True(DistinctionOriginAudit.ZeroModeBackground());
        Assert.Equal(95, DistinctionOriginAudit.PositiveModeCount());
        Assert.Equal(44, DistinctionOriginAudit.DistinctFrequencies());
    }

    [Fact]
    public void TQMQG2702_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2702: the distinction-origin determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no formulas):");
        sb.AppendLine("  - STRUCTURE FUNDAMENTAL (score ≤ 2), ACTUALIZATION FUNDAMENTAL (3),");
        sb.AppendLine("    DISTINCTION FUNDAMENTAL (4), UNIVERSAL DIFFERENCE PRINCIPLE (5-6);");
        sb.AppendLine("  - the question: what is being distinguished, and where does distinction come from?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {DistinctionOriginAudit.Summary()}");
        sb.AppendLine($"Origin score: {DistinctionOriginAudit.OriginScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {DistinctionOriginAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - NOT from STRUCTURE: the network is regular (all nodes degree 12) — there is no");
        sb.AppendLine("    structural position, label, or geometry that separates the nodes.");
        sb.AppendLine("  - From ACTUALIZATION — yes, but only because actualization IS a difference: the");
        sb.AppendLine("    event is a before→after transition, and the transition is the difference.");
        sb.AppendLine("    Actualization is not a separate source of distinction; it IS a difference.");
        sb.AppendLine("  - The D96 structure confirms: one zero mode (the background) + 95 positive modes");
        sb.AppendLine("    (the differences from it); 44 distinct frequencies. Distinction = the difference");
        sb.AppendLine("    between the background and each mode.");
        sb.AppendLine("  - CONCLUSION: distinction arises from DIFFERENCE, the most primitive notion. What");
        sb.AppendLine("    is distinguished is differences themselves: the before→after transitions, the");
        sb.AppendLine("    distinct shares μ^k/S, the positive modes against the zero background.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL DIFFERENCE PRINCIPLE", DistinctionOriginAudit.Classify());
        Assert.True(DistinctionOriginAudit.OriginScore() >= 5);
        Assert.Contains("UNIVERSAL DIFFERENCE PRINCIPLE", DistinctionOriginAudit.Summary());
    }
}
