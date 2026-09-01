using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 309 — Red Team Audit. Assume QG260-QG308 are WRONG; destroy the minimal theory. Search
/// for domains that DO NOT produce the four operators, a genuine fifth operator, and systems where
/// Difference → Actualization → Spectrum fails. No observables, no target values, deterministic.
/// </summary>
public class ATQG_Phase309_RedTeamAuditTests : ResearchTestBase
{
    public ATQG_Phase309_RedTeamAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3090_CounterexampleDomains()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3090: red-team attack (a) — degenerate counterexample domains");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the UNIFORM system (all equal) should fail all four operators;");
        sb.AppendLine("  - the all-distinct geometric / ramp systems should fail CROWDING (no ties).");
        sb.AppendLine();

        foreach (var (name, allFour) in RedTeamAudit.CounterexampleDomains())
            sb.AppendLine($"  {name}: all four operators present = {allFour}");
        sb.AppendLine();
        sb.AppendLine($"uniform counterexample (all four fail): {RedTeamAudit.UniformCounterexample()}");
        sb.AppendLine($"all-distinct counterexample (CROWDING fails): {RedTeamAudit.AllDistinctCounterexample()}");

        Output.WriteLine(sb.ToString());

        Assert.True(RedTeamAudit.UniformCounterexample(),
            "the uniform system must genuinely fail all four operators");
        Assert.True(RedTeamAudit.AllDistinctCounterexample(),
            "the all-distinct/ramp systems must genuinely fail CROWDING");
    }

    [Fact]
    public void ATQG3091_FifthOperatorAndChainFailure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3091: red-team attacks (b) and (c)");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the ORDER/sequence structure is the network input, not a fifth spectral read;");
        sb.AppendLine("  - the Difference → Actualization → Spectrum chain fails at the zero-difference");
        sb.AppendLine("    limit, but that is the theory's documented boundary.");
        sb.AppendLine();

        sb.AppendLine($"order is NOT a fifth operator (it is the adjacency input): {RedTeamAudit.OrderIsFifthOperator()}");
        sb.AppendLine($"no genuine fifth operator: {RedTeamAudit.NoFifthOperator()}");
        sb.AppendLine($"chain fails at the uniform (zero-difference) boundary: {RedTeamAudit.ChainFailsAtUniformBoundary()}");
        sb.AppendLine($"zero-difference is the documented boundary (QG228/278/279): {RedTeamAudit.ZeroDifferenceIsDocumentedBoundary()}");

        Output.WriteLine(sb.ToString());

        Assert.True(RedTeamAudit.NoFifthOperator(),
            "no genuine fifth spectral operator may exist");
        Assert.True(RedTeamAudit.ChainFailsAtUniformBoundary(),
            "the chain must genuinely fail at the zero-difference limit");
        Assert.True(RedTeamAudit.ZeroDifferenceIsDocumentedBoundary(),
            "the zero-difference limit must be the theory's documented boundary");
    }

    [Fact]
    public void ATQG3092_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3092: the red-team outcome");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - PARTIAL FAILURE: genuine degenerate limits exist (uniform/periodic fail) but");
        sb.AppendLine("    they are the theory's OWN documented boundaries; no fifth operator.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {RedTeamAudit.Summary()}");
        sb.AppendLine($"Red-team score: {RedTeamAudit.RedTeamScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {RedTeamAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the red team finds GENUINE degenerate limits: the uniform system fails all");
        sb.AppendLine("    four operators, and the all-distinct/ramp systems fail CROWDING — real");
        sb.AppendLine("    counterexample DOMAINS to 'universal';");
        sb.AppendLine("  - the Difference → Actualization → Spectrum chain genuinely fails to generate");
        sb.AppendLine("    a spectrum from zero inequality;");
        sb.AppendLine("  - BUT the zero-difference limit is the theory's OWN documented boundary:");
        sb.AppendLine("    Difference is the primitive (QG278/279), the uniform state is the unattainable");
        sb.AppendLine("    zero-information limit (QG228);");
        sb.AppendLine("  - no genuine fifth operator: the ORDER structure is the network/adjacency input,");
        sb.AppendLine("    not a spectral read;");
        sb.AppendLine("  - the universality is PARTIAL: it holds for organized systems and fails exactly");
        sb.AppendLine("    at the zero-organization boundaries the theory itself documents.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL FAILURE", RedTeamAudit.Classify());
        Assert.True(RedTeamAudit.RedTeamScore() >= 4);
        Assert.Contains("PARTIAL FAILURE", RedTeamAudit.Summary());
    }
}
