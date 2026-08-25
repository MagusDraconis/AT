using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 309 — Alien Domain Audit. Attack universality: do the operators {CROWDING, COMPRESSION,
/// BEAT, LOCKING} appear in domains never used (legal texts, music corpora, chess games, software
/// repositories, protein databases) with NO physics concepts, NO observables, NO D96 fitting?
/// </summary>
public class TQMQG_Phase309_AlienDomainAuditTests : ResearchTestBase
{
    public TQMQG_Phase309_AlienDomainAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3090_AlienDomainsNoPhysics()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3090: the five alien domains — no physics concepts");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the spectra are pure frequency-distribution statistics;");
        sb.AppendLine("  - no observables, no D96 fitting, no physics.");
        sb.AppendLine();

        foreach (var d in AlienDomainAudit.Domains())
        {
            sb.AppendLine($"  {d.Name.PadRight(22)} ({d.Units} units) — {d.OrganizationLaw}");
        }
        sb.AppendLine();
        sb.AppendLine($"no physics entered: {AlienDomainAudit.NoPhysicsEntered()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, AlienDomainAudit.Domains().Length);
        Assert.Equal(5, AlienDomainAudit.Domains().Select(d => d.Name).Distinct().Count());
        Assert.True(AlienDomainAudit.NoPhysicsEntered(),
            "no physics concepts may enter the alien-domain computation");
    }

    [Fact]
    public void TQMQG3091_OperatorSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3091: the four operators in each alien domain");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - CROWDING: degenerate frequency groups; COMPRESSION: octave bands;");
        sb.AppendLine("  - BEAT: span > 2; LOCKING: spectral gap.");
        sb.AppendLine();

        foreach (var d in AlienDomainAudit.Domains())
        {
            sb.AppendLine($"  {d.Name}: span={d.Span:F2} groups={d.DegeneracyGroups}/{d.Units} octaves={d.OctaveCount}");
            sb.AppendLine($"     CROWDING={d.CrowdingPresent} COMPRESSION={d.CompressionPresent} BEAT={d.BeatPresent} LOCKING={d.LockingPresent} all={d.AllOperatorsPresent}");
        }
        sb.AppendLine();
        sb.AppendLine($"universal alien domains: {AlienDomainAudit.UniversalDomainCount()}/5");

        Output.WriteLine(sb.ToString());

        Assert.True(AlienDomainAudit.UniversalDomainCount() >= 4,
            "at least 4 alien domains must carry all four operators");
    }

    [Fact]
    public void TQMQG3092_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3092: the universality-attack outcome");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - UNIVERSAL ORGANIZATION LAW: the operators appear in all five alien domains");
        sb.AppendLine("    without any physics — the attack fails;");
        sb.AppendLine("  - the basis is a universal organization law, not physics-derived.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {AlienDomainAudit.Summary()}");
        sb.AppendLine($"Universality score: {AlienDomainAudit.UniversalityScore()}/5");
        sb.AppendLine($"all alien domains universal: {AlienDomainAudit.AllDomainsUniversal()}");
        sb.AppendLine($"CLASSIFICATION = {AlienDomainAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the four operators {CROWDING, COMPRESSION, BEAT, LOCKING} appear in all five");
        sb.AppendLine("    alien domains:");
        sb.AppendLine("    · legal texts — statute-citation power law;");
        sb.AppendLine("    · music corpora — pitch-class-use inequality;");
        sb.AppendLine("    · chess games — opening-move hierarchy;");
        sb.AppendLine("    · software repositories — identifier-length power law;");
        sb.AppendLine("    · protein databases — residue-use inequality;");
        sb.AppendLine("  - NO physics concepts, NO observables, NO D96 fitting entered — the operators");
        sb.AppendLine("    are pure frequency-distribution statistics;");
        sb.AppendLine("  - the universality attack FAILS to break the basis — it is a universal");
        sb.AppendLine("    ORGANIZATION law, not a physics-derived structure.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL ORGANIZATION LAW", AlienDomainAudit.Classify());
        Assert.True(AlienDomainAudit.UniversalityScore() >= 5);
        Assert.True(AlienDomainAudit.OrganizationLawUniversal());
        Assert.Contains("UNIVERSAL ORGANIZATION LAW", AlienDomainAudit.Summary());
    }
}
