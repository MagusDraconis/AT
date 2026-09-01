using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 311 — Operator Specificity Audit. Do the four operators measure organization or merely
/// inequality? Construct pairs with same inequality/different organization and same organization/
/// different inequality. Deterministic, D96 only.
/// </summary>
public class ATQG_Phase311_OperatorSpecificityAuditTests : ResearchTestBase
{
    public ATQG_Phase311_OperatorSpecificityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3110_PairA_ShuffleIndistinguishable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3110: Pair (a) — same inequality, different arrangement (frequency read)");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - a power law and its shuffled multiset have the IDENTICAL frequency multiset;");
        sb.AppendLine("  - the frequency operators are order-blind → indistinguishable.");
        sb.AppendLine();

        sb.AppendLine($"power law vs shuffled power law indistinguishable: {OperatorSpecificityAudit.PairA_ShuffleIndistinguishable()}");
        sb.AppendLine();
        sb.AppendLine("The frequency reading is ORDER-BLIND: a power law and its shuffled multiset have");
        sb.AppendLine("identical CROWDING/COMPRESSION/BEAT/LOCKING → the frequency read measures INEQUALITY,");
        sb.AppendLine("not arrangement.");

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorSpecificityAudit.PairA_ShuffleIndistinguishable(),
            "the power law and its shuffle must be indistinguishable to the frequency operators");
    }

    [Fact]
    public void ATQG3111_PairB_ModularDiffers()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3111: Pair (b) — same degree sequence, different arrangement (graph read)");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - a modular graph and its degree-preserving rewiring have the SAME degrees");
        sb.AppendLine("    (same inequality) but different Laplacian spectra;");
        sb.AppendLine("  - the graph-spectral operators should differ → ORGANIZATION-specific.");
        sb.AppendLine();

        sb.AppendLine($"modular graph differs from its degree-preserving rewiring: {OperatorSpecificityAudit.PairB_ModularDiffersFromRewired()}");
        sb.AppendLine($"power-law exponent 1 vs 2 tracks inequality: {OperatorSpecificityAudit.PairC_TracksInequality()}");
        sb.AppendLine();
        sb.AppendLine("The graph-spectral reading SEES the arrangement: a modular graph and its");
        sb.AppendLine("degree-preserving rewiring have different operators → the spectral read measures");
        sb.AppendLine("ORGANIZATION.");

        Output.WriteLine(sb.ToString());

        Assert.True(OperatorSpecificityAudit.PairB_ModularDiffersFromRewired(),
            "the modular graph must differ from its degree-preserving rewiring spectrally");
        Assert.True(OperatorSpecificityAudit.PairC_TracksInequality(),
            "within one organizational form the operators must track inequality");
    }

    [Fact]
    public void ATQG3112_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3112: the operator-specificity determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - MIXED: the frequency read is inequality-specific, the graph read is");
        sb.AppendLine("    organization-specific — the operators measure both, depending on the read.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OperatorSpecificityAudit.Summary()}");
        sb.AppendLine($"Specificity score: {OperatorSpecificityAudit.SpecificityScore()}/5");
        sb.AppendLine($"frequency read is inequality-specific: {OperatorSpecificityAudit.FrequencyReadIsInequalitySpecific()}");
        sb.AppendLine($"graph read is organization-specific: {OperatorSpecificityAudit.GraphReadIsOrganizationSpecific()}");
        sb.AppendLine($"CLASSIFICATION = {OperatorSpecificityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("THE PAIR TESTS:");
        foreach (var p in OperatorSpecificityAudit.Pairs())
        {
            sb.AppendLine($"  {p.Name} — fixed {p.HeldFixed}, varied {p.Varied}");
            sb.AppendLine($"      {p.Finding}");
        }
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the operators are a SPECTRAL read of the underlying structure:");
        sb.AppendLine("    · as frequency statistics — INEQUALITY-specific (order-blind);");
        sb.AppendLine("    · as graph spectra — ORGANIZATION-specific (sees the arrangement);");
        sb.AppendLine("    · within one form — tracks inequality monotonically.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MIXED", OperatorSpecificityAudit.Classify());
        Assert.True(OperatorSpecificityAudit.MixedSpecificity());
        Assert.True(OperatorSpecificityAudit.SpecificityScore() >= 5);
        Assert.Contains("MIXED", OperatorSpecificityAudit.Summary());
    }
}
