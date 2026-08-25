using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 312 — Adversarial Spectrum Audit. Try to break the operator basis: craft fake spectra
/// with large span / large gap / many groups but NO organization, and test whether the four operators
/// can be triggered without it. Deterministic, D96 only.
/// </summary>
public class TQMQG_Phase312_AdversarialSpectrumAuditTests : ResearchTestBase
{
    public TQMQG_Phase312_AdversarialSpectrumAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3120_FakeSpectra()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3120: the three adversarial fakes");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the fakes deliberately trigger operator-like features (span, gap, groups);");
        sb.AppendLine("  - none has real organization.");
        sb.AppendLine();

        foreach (var f in AdversarialSpectrumAudit.Fakes())
        {
            sb.AppendLine($"  {f.Name} — {f.Construction}");
            sb.AppendLine($"     span={f.Span:F1} distinct={f.DistinctValues} octaves={f.OctaveCount}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, AdversarialSpectrumAudit.Fakes().Length);
        Assert.True(AdversarialSpectrumAudit.Fakes()[0].Span > 100,
            "the large-span fake must have a large span");
    }

    [Fact]
    public void TQMQG3121_OperatorTriggering()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3121: can the fakes trigger the operators?");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the large-span fake should trigger BEAT/COMPRESSION;");
        sb.AppendLine("  - the large-gap fake should trigger LOCKING/CROWDING;");
        sb.AppendLine("  - the many-groups fake should FAIL CROWDING (all distinct).");
        sb.AppendLine();

        foreach (var f in AdversarialSpectrumAudit.Fakes())
        {
            sb.AppendLine($"  {f.Name}: CROWDING={f.CrowdingPresent} COMPRESSION={f.CompressionPresent} BEAT={f.BeatPresent} LOCKING={f.LockingPresent} full={f.FullBasisTriggered} locks={f.BeatIdentityLocks}");
        }
        sb.AppendLine();
        sb.AppendLine($"full basis triggered: {AdversarialSpectrumAudit.FullBasisTriggered()}/3");
        sb.AppendLine($"locks not fakable: {AdversarialSpectrumAudit.LocksNotFakable()}");

        Output.WriteLine(sb.ToString());

        Assert.True(AdversarialSpectrumAudit.FullBasisTriggered() <= 2,
            "at most two fakes may trigger the binary presence");
        Assert.True(AdversarialSpectrumAudit.LocksNotFakable(),
            "the organization locks must not be faked by span/gap/group crafting");
    }

    [Fact]
    public void TQMQG3122_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3122: the adversarial robustness determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - PARTIAL FAILURE: the fakes trigger the binary presence partially (two-level");
        sb.AppendLine("    fakes pass CROWDING), but the organization locks cannot be faked.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {AdversarialSpectrumAudit.Summary()}");
        sb.AppendLine($"Robustness score: {AdversarialSpectrumAudit.RobustnessScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {AdversarialSpectrumAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the adversarial fakes CAN trigger the binary presence:");
        sb.AppendLine("    · large-span fake → BEAT/COMPRESSION (span ≈ 200, no rank structure);");
        sb.AppendLine("    · large-gap fake → LOCKING/CROWDING (two clusters, no hierarchy);");
        sb.AppendLine("    · many-groups fake → fails CROWDING (all distinct — no degeneracy);");
        sb.AppendLine("  - the fakes CANNOT fake the ORGANIZATION SIGNATURE: the beat-identity locks");
        sb.AppendLine("    (exact integer ratios Σ√m/span ≈ 10, occMom/Σm ≈ 20) are carried by ZERO of");
        sb.AppendLine("    the fakes;");
        sb.AppendLine("  - the binary presence is partially faked by two-level crafting; the organization");
        sb.AppendLine("    content is robust.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL FAILURE", AdversarialSpectrumAudit.Classify());
        Assert.True(AdversarialSpectrumAudit.RobustnessScore() >= 4);
        Assert.Contains("PARTIAL FAILURE", AdversarialSpectrumAudit.Summary());
    }
}
