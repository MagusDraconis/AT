using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 310 — Organization Metric Prediction. Hypothesis: operator strength (the degree of
/// CROWDING / COMPRESSION / BEAT / LOCKING) predicts organization level. Compute an organization score
/// from the four operators and rank the domains (random, uniform, language, DNA, software, finance).
/// No observables, no target values, deterministic.
/// </summary>
public class ATQG_Phase310_OrganizationMetricPredictionTests : ResearchTestBase
{
    public ATQG_Phase310_OrganizationMetricPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3100_OrganizationScores()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3100: the organization scores from the four operators");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the organization score combines CROWDING, COMPRESSION, BEAT, LOCKING strengths;");
        sb.AppendLine("  - the unorganized systems (uniform, random) should score below the organized ones.");
        sb.AppendLine();

        foreach (var d in OrganizationMetricPrediction.Domains())
        {
            sb.AppendLine($"  {d.Name.PadRight(10)}: crowding={d.Crowding:F2} compression={d.Compression:F2} " +
                          $"beat={d.Beat:F2} locking={d.Locking:F2} score={d.OrganizationScore:F3}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(6, OrganizationMetricPrediction.Domains().Length);
        Assert.True(OrganizationMetricPrediction.Domains().All(d => d.OrganizationScore >= 0.0 && d.OrganizationScore <= 1.0),
            "every organization score must be in [0,1]");
    }

    [Fact]
    public void ATQG3101_ClassSeparation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3101: the operator structure separates unorganized from organized");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - uniform/random score below language/DNA/software/finance;");
        sb.AppendLine("  - heavy-tailed (software/finance) above Zipf (language/DNA).");
        sb.AppendLine();

        sb.AppendLine($"unorganized below organized: {OrganizationMetricPrediction.UnorganizedBelowOrganized()}");
        sb.AppendLine($"heavy-tailed above Zipf: {OrganizationMetricPrediction.HeavyTailAboveZipf()}");
        sb.AppendLine();
        sb.AppendLine("computed order:");
        foreach (var n in OrganizationMetricPrediction.ComputedOrder())
            sb.AppendLine($"  {n}");

        Output.WriteLine(sb.ToString());

        Assert.True(OrganizationMetricPrediction.UnorganizedBelowOrganized(),
            "the unorganized systems must score below all organized systems");
        Assert.True(OrganizationMetricPrediction.HeavyTailAboveZipf(),
            "the heavy-tailed systems must score above the Zipf systems");
    }

    [Fact]
    public void ATQG3102_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3102: the organization-metric determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - ORGANIZATION LAW: the operator strength ranks organization strength, not just");
        sb.AppendLine("    detects it.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OrganizationMetricPrediction.Summary()}");
        sb.AppendLine($"Prediction score: {OrganizationMetricPrediction.PredictionScore()}/5");
        sb.AppendLine($"ordering reproduced: {OrganizationMetricPrediction.OrderingReproduced()}");
        sb.AppendLine($"CLASSIFICATION = {OrganizationMetricPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the organization score from {CROWDING, COMPRESSION, BEAT, LOCKING} ranks the");
        sb.AppendLine("    domains as predicted: unorganized [uniform, random] below organized");
        sb.AppendLine("    [language, DNA, software, finance], heavy-tailed [software, finance] above");
        sb.AppendLine("    Zipf [language, DNA];");
        sb.AppendLine("  - the operator structure is a genuine ORGANIZATION metric: it ranks organization");
        sb.AppendLine("    strength, not just detects it.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("ORGANIZATION LAW", OrganizationMetricPrediction.Classify());
        Assert.True(OrganizationMetricPrediction.PredictionScore() >= 5);
        Assert.True(OrganizationMetricPrediction.OrderingReproduced());
        Assert.Contains("ORGANIZATION LAW", OrganizationMetricPrediction.Summary());
    }
}
