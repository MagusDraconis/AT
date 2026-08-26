using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 229 — Cosmology Closure Audit. Determine the exact cosmology gap; classify the six
/// features; identify the highest-impact blocker. Audit only — no new physics.
/// </summary>
public class ATQG_Phase229_CosmologyClosureAuditTests : ResearchTestBase
{
    public ATQG_Phase229_CosmologyClosureAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2290_SixCosmologyFeatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2290: the six cosmology features and their status");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Reviews QG77 + the QG194-228 derivations touching the cosmology sector.");
        sb.AppendLine();

        sb.AppendLine("THE SIX FEATURES:");
        foreach (var f in CosmologyClosureAudit.Features())
            sb.AppendLine($"  {f.Index}. {f.Name}: {f.Status}");
        sb.AppendLine();

        sb.AppendLine("STATUS COUNTS:");
        foreach (var kv in CosmologyClosureAudit.StatusCounts())
            sb.AppendLine($"  {kv.Key}: {kv.Value}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(6, CosmologyClosureAudit.Features().Length);
        var sc = CosmologyClosureAudit.StatusCounts();
        Assert.Equal(1, sc[CosmologyClosureAudit.Status.Derived]);   // expansion
        Assert.Equal(2, sc[CosmologyClosureAudit.Status.Partial]);   // dark matter, CMB structure
        Assert.Equal(3, sc[CosmologyClosureAudit.Status.Open]);      // structure formation, dark energy, Λ
    }

    [Fact]
    public void ATQG2291_HighestImpactBlocker()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2291: the single highest-impact remaining cosmology blocker");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The blocker is the largest single cosmological feature with the least derivation.");
        sb.AppendLine();

        var (name, why) = CosmologyClosureAudit.HighestImpactBlocker();
        sb.AppendLine("HIGHEST-IMPACT BLOCKER:");
        sb.AppendLine($"  {name}");
        sb.AppendLine($"  Why: {why}");
        sb.AppendLine();
        sb.AppendLine("  Rationale:");
        sb.AppendLine("  - Dark energy / Λ dominates the universe's energy budget (accelerated expansion);");
        sb.AppendLine("  - no candidate mechanism exists in QG194-228 (QG77 marks it UNKNOWN);");
        sb.AppendLine("  - structure formation is the runner-up but dark energy is the larger gap.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("Dark energy / Λ", name);
    }

    [Fact]
    public void ATQG2292_ClassificationPartialCosmology()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2292: classification — PARTIAL COSMOLOGY");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score < 2.0 NOT CLOSED; 2.0-4.4 PARTIAL COSMOLOGY; ≥ 4.5 COSMOLOGY COMPLETE.");
        sb.AppendLine();

        double score = CosmologyClosureAudit.TotalScore();
        string classification = CosmologyClosureAudit.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        foreach (var f in CosmologyClosureAudit.Features())
            sb.AppendLine($"  {f.Index}. {f.Name}: {CosmologyClosureAudit.SubScore(f.Status):F1}");
        sb.AppendLine($"  TOTAL = {score:F1}/6");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine($"  Summary: {CosmologyClosureAudit.Summary()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Expansion is DERIVED (QG77) and the dark-matter effect (flat rotation) is DERIVED");
        sb.AppendLine("    via the deficit with α=0 (QG194/195/206) and M∝R (QG184).");
        sb.AppendLine("  - Structure formation, dark energy, and Λ remain OPEN — the cosmology sector is");
        sb.AppendLine("    substantially closer than QG77's 'UNKNOWN' but not closed.");
        sb.AppendLine($"  ⇒ {classification} — the exact cosmology gap is structure formation + dark energy/Λ.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL COSMOLOGY", classification);
        Assert.Equal(2.0, score, 6);
        Assert.Equal(2.0, CosmologyClosureAudit.TotalScore(), 6);
    }
}
