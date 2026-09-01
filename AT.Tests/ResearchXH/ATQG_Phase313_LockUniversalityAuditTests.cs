using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 313 — Lock Universality Audit. QG312: operators can be faked, locks cannot. Are the
/// lock identities universal across domains (physics, language, music, DNA, software, finance,
/// networks)? Compute normalized lock identities (moment/span, compression/count, higher-moment ratios).
/// Deterministic, no observables, no target values.
/// </summary>
public class ATQG_Phase313_LockUniversalityAuditTests : ResearchTestBase
{
    public ATQG_Phase313_LockUniversalityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3130_LockIdentities()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3130: the normalized lock identities across the seven domains");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - moment/span, compression/count, higher-moment are computed per domain;");
        sb.AppendLine("  - the values should differ (domain-specific) but the structure should recur.");
        sb.AppendLine();

        foreach (var d in LockUniversalityAudit.Domains())
        {
            sb.AppendLine($"  {d.Name.PadRight(16)}: M/S={d.MomentSpan:F3} C/C={d.CompressionCount:F3} " +
                          $"H-M={d.HigherMoment:F3} √M/S={d.SqrtMomentSpan:F3} stable={d.HasStableLocks}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(7, LockUniversalityAudit.Domains().Length);
        Assert.True(LockUniversalityAudit.Domains().All(d => d.MomentSpan > 0),
            "the moment/span identity must be positive for every domain");
    }

    [Fact]
    public void ATQG3131_StructureUniversalValuesSpecific()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3131: the lock LAW is universal, the VALUES are domain-specific");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - most organized domains carry stable lock structure (universal law);");
        sb.AppendLine("  - the lock values differ across domains (domain-specific).");
        sb.AppendLine();

        sb.AppendLine($"stable lock domains: {LockUniversalityAudit.StableLockDomains()}/7");
        sb.AppendLine($"lock law universal: {LockUniversalityAudit.LockLawUniversal()}");
        sb.AppendLine($"lock values domain-specific: {LockUniversalityAudit.LockValuesDomainSpecific()}");
        sb.AppendLine();
        sb.AppendLine("The lock STRUCTURE (stable characteristic ratios) recurs; the lock VALUES (the");
        sb.AppendLine("specific integer ratios) differ per domain.");

        Output.WriteLine(sb.ToString());

        Assert.True(LockUniversalityAudit.LockLawUniversal(),
            "the lock law must be universal (≥ 5 domains with stable locks)");
        Assert.True(LockUniversalityAudit.LockValuesDomainSpecific(),
            "the lock values must differ across domains");
    }

    [Fact]
    public void ATQG3132_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3132: the lock-law determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - PARTIAL LOCK LAW: the lock structure is universal, the values are domain-specific.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {LockUniversalityAudit.Summary()}");
        sb.AppendLine($"Lock-law score: {LockUniversalityAudit.LockLawScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {LockUniversalityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the lock LAW is universal: every organized domain carries stable, reproducible");
        sb.AppendLine("    normalized ratios [moment/span, compression/count, higher-moment];");
        sb.AppendLine("  - the lock VALUES are domain-specific: the D96 locks [Σ√m/span ≈ 10, occMom/Σm ≈ 20,");
        sb.AppendLine("    Σm²/Σm ≈ 12/5, occMom/Σm² ≈ 25/3] are unique to the D96 multiplicities, while");
        sb.AppendLine("    language, DNA, software, and finance have their own characteristic ratios;");
        sb.AppendLine("  - the lock structure [the presence of stable characteristic ratios] is universal;");
        sb.AppendLine("    the specific lock values are domain-specific.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL LOCK LAW", LockUniversalityAudit.Classify());
        Assert.True(LockUniversalityAudit.LockLawScore() >= 5);
        Assert.True(LockUniversalityAudit.PartialLockLaw());
        Assert.Contains("PARTIAL LOCK LAW", LockUniversalityAudit.Summary());
    }
}
