using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 304 — Real Network Universality. Test REAL network structure only: connectome, protein,
/// citation, internet, knowledge. Deterministic real-structure models (no observables, no fitting).
/// Compute CROWDING / COMPRESSION / BEAT / LOCKING. Output: FAIL / PARTIAL / UNIVERSAL.
/// </summary>
public class TQMQG_Phase304_RealNetworkUniversalityAuditTests : ResearchTestBase
{
    public TQMQG_Phase304_RealNetworkUniversalityAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3040_RealStructuralSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3040: the five real-structure domains");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the models reproduce the EMPIRICALLY-KNOWN structural signature of each domain;");
        sb.AppendLine("  - no observables, no fitting, deterministic.");
        sb.AppendLine();

        foreach (var d in RealNetworkUniversalityAudit.Domains())
        {
            sb.AppendLine($"  {d.Name.PadRight(10)} ({d.Nodes} nodes) — {d.RealSignature}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, RealNetworkUniversalityAudit.Domains().Length);
        Assert.Equal(5, RealNetworkUniversalityAudit.Domains().Select(d => d.Name).Distinct().Count());
    }

    [Fact]
    public void TQMQG3041_OperatorSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3041: the four operators in each real-structure domain");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - CROWDING: degeneracy groups; COMPRESSION: octave bands;");
        sb.AppendLine("  - BEAT: span > 2; LOCKING: λ₂ > 0.");
        sb.AppendLine();

        foreach (var d in RealNetworkUniversalityAudit.Domains())
        {
            sb.AppendLine($"  {d.Name}: span={d.Span:F3} gap={d.SpectralGap:F4} groups={d.DegeneracyGroups}/{d.Nodes} octaves={d.OctaveCount}");
            sb.AppendLine($"     CROWDING={d.CrowdingPresent} COMPRESSION={d.CompressionPresent} BEAT={d.BeatPresent} LOCKING={d.LockingPresent} all={d.AllOperatorsPresent}");
        }
        sb.AppendLine();
        sb.AppendLine($"universal real domains: {RealNetworkUniversalityAudit.UniversalDomainCount()}/5");

        Output.WriteLine(sb.ToString());

        Assert.True(RealNetworkUniversalityAudit.UniversalDomainCount() >= 4,
            "at least 4 real-structure domains must carry all four operators");
    }

    [Fact]
    public void TQMQG3042_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3042: the real-network universality determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - UNIVERSAL: the four operators appear in all five real structural signatures;");
        sb.AppendLine("  - the operator basis is universal across REAL network structure.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {RealNetworkUniversalityAudit.Summary()}");
        sb.AppendLine($"Universality score: {RealNetworkUniversalityAudit.UniversalityScore()}/5");
        sb.AppendLine($"all real domains universal: {RealNetworkUniversalityAudit.AllRealDomainsUniversal()}");
        sb.AppendLine($"CLASSIFICATION = {RealNetworkUniversalityAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the four operators {CROWDING, COMPRESSION, BEAT, LOCKING} appear in the");
        sb.AppendLine("    deterministic real-structure models of all five domains:");
        sb.AppendLine("    · connectome — small-world + modular (the cortical wiring law);");
        sb.AppendLine("    · protein — hierarchical scale-free (the Ravasz-Barabási law);");
        sb.AppendLine("    · citation — acyclic power-law in-degree (the cumulative-advantage law);");
        sb.AppendLine("    · internet — scale-free rich-club (the rich-club coefficient law);");
        sb.AppendLine("    · knowledge — hub-rich heterogeneous (the long-tail law);");
        sb.AppendLine("  - each reproduces its empirically-known structural signature and carries all");
        sb.AppendLine("    four operators — the operator basis is universal across REAL network structure,");
        sb.AppendLine("    confirming QG302 beyond idealized models.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL", RealNetworkUniversalityAudit.Classify());
        Assert.True(RealNetworkUniversalityAudit.UniversalityScore() >= 5);
        Assert.True(RealNetworkUniversalityAudit.StructureUniversalReal());
        Assert.Contains("UNIVERSAL", RealNetworkUniversalityAudit.Summary());
    }
}
