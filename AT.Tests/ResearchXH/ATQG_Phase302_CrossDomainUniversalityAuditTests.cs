using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 302 — Cross-Domain Universality Audit. Hypothesis: Difference → Actualization →
/// Spectrum is domain-independent. Test: compute the four operators {CROWDING, COMPRESSION, BEAT,
/// LOCKING} on four deterministic network classes (neural, biological, social, internet). No
/// observables, no target values, deterministic.
/// </summary>
public class ATQG_Phase302_CrossDomainUniversalityAuditTests : ResearchTestBase
{
    public ATQG_Phase302_CrossDomainUniversalityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3020_FourDomains()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3020: the four deterministic network domains");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - neural = layered feed-forward; biological = hierarchical modular;");
        sb.AppendLine("  - social = small-world ring; internet = hub-and-spoke.");
        sb.AppendLine();

        foreach (var d in CrossDomainUniversalityAudit.Domains())
        {
            sb.AppendLine($"  {d.Name.PadRight(11)} ({d.Nodes} nodes, {d.Model})");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(4, CrossDomainUniversalityAudit.Domains().Length);
        Assert.Equal(4, CrossDomainUniversalityAudit.Domains().Select(d => d.Name).Distinct().Count());
    }

    [Fact]
    public void ATQG3021_OperatorSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3021: the four operators in each domain");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - CROWDING: degeneracy groups (multiplicity > 1);");
        sb.AppendLine("  - COMPRESSION: octave bands (span > 2);");
        sb.AppendLine("  - BEAT: span > 2; LOCKING: λ₂ > 0.");
        sb.AppendLine();

        foreach (var d in CrossDomainUniversalityAudit.Domains())
        {
            sb.AppendLine($"  {d.Name}: span={d.Span:F3}, gap={d.SpectralGap:F4}, " +
                          $"groups={d.DegeneracyGroups}, octaves={d.OctaveCount}");
            sb.AppendLine($"     CROWDING={d.CrowdingPresent} COMPRESSION={d.CompressionPresent} " +
                          $"BEAT={d.BeatPresent} LOCKING={d.LockingPresent} all={d.AllOperatorsPresent}");
        }
        sb.AppendLine();
        sb.AppendLine($"universal domains: {CrossDomainUniversalityAudit.UniversalDomainCount()}/4");

        Output.WriteLine(sb.ToString());

        Assert.True(CrossDomainUniversalityAudit.Domains()[0].CrowdingPresent,
            "the neural domain must carry CROWDING (degeneracy groups)");
        Assert.True(CrossDomainUniversalityAudit.Domains()[1].CompressionPresent,
            "the biological domain must carry COMPRESSION (octave bands)");
        Assert.True(CrossDomainUniversalityAudit.Domains()[2].BeatPresent,
            "the social domain must carry BEAT (span > 2)");
        Assert.True(CrossDomainUniversalityAudit.Domains()[3].LockingPresent,
            "the internet domain must carry LOCKING (λ₂ > 0)");
        Assert.True(CrossDomainUniversalityAudit.UniversalDomainCount() >= 3,
            "at least 3 domains must carry all four operators");
    }

    [Fact]
    public void ATQG3022_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3022: the cross-domain universality determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - UNIVERSAL STRUCTURE: all four operators in all four domains;");
        sb.AppendLine("  - the Difference → Actualization → Spectrum chain is domain-independent.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {CrossDomainUniversalityAudit.Summary()}");
        sb.AppendLine($"Universality score: {CrossDomainUniversalityAudit.UniversalityScore()}/5");
        sb.AppendLine($"all domains universal: {CrossDomainUniversalityAudit.AllDomainsUniversal()}");
        sb.AppendLine($"CLASSIFICATION = {CrossDomainUniversalityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the four operators {CROWDING, COMPRESSION, BEAT, LOCKING} appear in every");
        sb.AppendLine("    domain tested: neural [layered feed-forward], biological [hierarchical");
        sb.AppendLine("    modular], social [small-world ring], internet [hub-and-spoke];");
        sb.AppendLine("  - each network's Laplacian spectrum carries the degeneracy groups (CROWDING),");
        sb.AppendLine("    the octave bands (COMPRESSION), the span (BEAT), and the spectral gap (LOCKING);");
        sb.AppendLine("  - the operator basis is the UNIVERSAL spectral structure of any connected");
        sb.AppendLine("    non-trivial network — the Difference → Actualization → Spectrum chain is");
        sb.AppendLine("    DOMAIN-INDEPENDENT.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL STRUCTURE", CrossDomainUniversalityAudit.Classify());
        Assert.True(CrossDomainUniversalityAudit.UniversalityScore() >= 5);
        Assert.True(CrossDomainUniversalityAudit.StructureUniversalAcrossDomains());
        Assert.Contains("UNIVERSAL STRUCTURE", CrossDomainUniversalityAudit.Summary());
    }
}
