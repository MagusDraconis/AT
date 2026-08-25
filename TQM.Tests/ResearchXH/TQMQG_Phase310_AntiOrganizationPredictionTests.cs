using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 310 — Anti-Organization Prediction. Maximally-unorganized systems (white noise, Poisson
/// process, uniform distribution, complete randomness, maximum-entropy sequence) should LOSE the
/// operator basis. If they keep it, the operators are trivial statistics. Deterministic, D96 only.
/// </summary>
public class TQMQG_Phase310_AntiOrganizationPredictionTests : ResearchTestBase
{
    public TQMQG_Phase310_AntiOrganizationPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3100_FiveUnorganizedSystems()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3100: the five maximally-unorganized systems");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - white noise, Poisson, uniform, randomness, max-entropy are all FLAT");
        sb.AppendLine("    (uniform-frequency) systems — the anti-organization limit.");
        sb.AppendLine();

        foreach (var s in AntiOrganizationPrediction.Systems())
        {
            sb.AppendLine($"  {s.Name.PadRight(26)} — {s.Generator}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, AntiOrganizationPrediction.Systems().Length);
        Assert.Equal(5, AntiOrganizationPrediction.Systems().Select(s => s.Name).Distinct().Count());
    }

    [Fact]
    public void TQMQG3101_OperatorSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3101: the four operators on each unorganized system");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - flat spectra (span = 1, one distinct value) should fail all four operators.");
        sb.AppendLine();

        foreach (var s in AntiOrganizationPrediction.Systems())
        {
            sb.AppendLine($"  {s.Name}: span={s.Span:F2} distinct={s.DistinctValues} octaves={s.OctaveCount}");
            sb.AppendLine($"     CROWDING={s.CrowdingPresent} COMPRESSION={s.CompressionPresent} BEAT={s.BeatPresent} LOCKING={s.LockingPresent} class={s.OrgClass}");
        }
        sb.AppendLine();
        sb.AppendLine($"no-basis count: {AntiOrganizationPrediction.NoBasisCount()}/5");
        sb.AppendLine($"all unorganized lose the basis: {AntiOrganizationPrediction.AllUnorganizedLoseBasis()}");

        Output.WriteLine(sb.ToString());

        Assert.True(AntiOrganizationPrediction.Systems()[0].OrgClass == AntiOrganizationPrediction.Organization.NoBasis,
            "white noise must lose the basis");
        Assert.True(AntiOrganizationPrediction.Systems()[1].OrgClass == AntiOrganizationPrediction.Organization.NoBasis,
            "the Poisson process must lose the basis");
        Assert.True(AntiOrganizationPrediction.Systems()[2].OrgClass == AntiOrganizationPrediction.Organization.NoBasis,
            "the uniform distribution must lose the basis");
        Assert.True(AntiOrganizationPrediction.NoBasisCount() >= 4,
            "at least 4 unorganized systems must lose the basis");
    }

    [Fact]
    public void TQMQG3102_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3102: the organization-signature determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - if the operators vanish on unorganized systems, they are signatures of");
        sb.AppendLine("    organization (UNIVERSAL ORGANIZATION LAW);");
        sb.AppendLine("  - if they persist, they are trivial statistics (STATISTICAL ARTIFACT).");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {AntiOrganizationPrediction.Summary()}");
        sb.AppendLine($"Prediction score: {AntiOrganizationPrediction.PredictionScore()}/5");
        sb.AppendLine($"operators are organization signatures: {AntiOrganizationPrediction.OperatorsAreOrganizationSignatures()}");
        sb.AppendLine($"CLASSIFICATION = {AntiOrganizationPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the maximally-unorganized systems all have FLAT spectra (span = 1, one");
        sb.AppendLine("    distinct value, no octave structure):");
        sb.AppendLine("    · white noise — flat power spectrum;");
        sb.AppendLine("    · Poisson process — critical flat profile;");
        sb.AppendLine("    · uniform distribution — all-equal frequencies;");
        sb.AppendLine("    · complete randomness — max-entropy flat sequence;");
        sb.AppendLine("    · maximum entropy — uniform symbol distribution;");
        sb.AppendLine("  - CROWDING, COMPRESSION, BEAT, LOCKING all fail — the operators are NOT trivial");
        sb.AppendLine("    statistics, they are SIGNATURES OF ORGANIZATION: they appear exactly when a");
        sb.AppendLine("    system has inequality and vanish when it is maximally disordered.");
        sb.AppendLine("  - the anti-organization prediction confirms the UNIVERSAL ORGANIZATION LAW.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL ORGANIZATION LAW", AntiOrganizationPrediction.Classify());
        Assert.True(AntiOrganizationPrediction.PredictionScore() >= 5);
        Assert.True(AntiOrganizationPrediction.OperatorsAreOrganizationSignatures());
        Assert.Contains("UNIVERSAL ORGANIZATION LAW", AntiOrganizationPrediction.Summary());
    }
}
