using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 306 — Compression Law Prediction. Do non-network systems (language, music, DNA,
/// software, finance) also produce CROWDING / COMPRESSION / BEAT / LOCKING? No observables, no
/// fitting, deterministic.
/// </summary>
public class ATQG_Phase306_CompressionLawPredictionTests : ResearchTestBase
{
    public ATQG_Phase306_CompressionLawPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3060_FiveNonNetworkDomains()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3060: the five non-network domains");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - language = Zipf word-frequency; music = harmonic series;");
        sb.AppendLine("  - DNA = codon degeneracy; software = token power law; finance = heavy tail.");
        sb.AppendLine();

        foreach (var d in CompressionLawPrediction.Domains())
        {
            sb.AppendLine($"  {d.Name.PadRight(10)} ({d.Units} units) — {d.StatisticalLaw}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, CompressionLawPrediction.Domains().Length);
        Assert.Equal(5, CompressionLawPrediction.Domains().Select(d => d.Name).Distinct().Count());
    }

    [Fact]
    public void ATQG3061_OperatorSignatures()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3061: the four operators in each non-network domain");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - CROWDING: degenerate frequency groups; COMPRESSION: octave bands;");
        sb.AppendLine("  - BEAT: occurrence span > 2; LOCKING: spectral gap present.");
        sb.AppendLine();

        foreach (var d in CompressionLawPrediction.Domains())
        {
            sb.AppendLine($"  {d.Name}: span={d.Span:F2} groups={d.DegeneracyGroups}/{d.Units} octaves={d.OctaveCount}");
            sb.AppendLine($"     CROWDING={d.CrowdingPresent} COMPRESSION={d.CompressionPresent} BEAT={d.BeatPresent} LOCKING={d.LockingPresent} all={d.AllOperatorsPresent}");
        }
        sb.AppendLine();
        sb.AppendLine($"universal non-network domains: {CompressionLawPrediction.UniversalDomainCount()}/5");

        Output.WriteLine(sb.ToString());

        Assert.True(CompressionLawPrediction.UniversalDomainCount() >= 4,
            "at least 4 non-network domains must carry all four operators");
    }

    [Fact]
    public void ATQG3062_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3062: the compression-law determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - UNIVERSAL COMPRESSION LAW: all five non-network domains carry the operators;");
        sb.AppendLine("  - the operator structure is not network-specific.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {CompressionLawPrediction.Summary()}");
        sb.AppendLine($"Universality score: {CompressionLawPrediction.UniversalityScore()}/5");
        sb.AppendLine($"all domains universal: {CompressionLawPrediction.AllDomainsUniversal()}");
        sb.AppendLine($"CLASSIFICATION = {CompressionLawPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the four operators {CROWDING, COMPRESSION, BEAT, LOCKING} appear in all five");
        sb.AppendLine("    non-network domains:");
        sb.AppendLine("    · language — Zipf 1/k word-frequency (degenerate groups + span);");
        sb.AppendLine("    · music — harmonic-series 1/m overtone law (octave bands at 2:1);");
        sb.AppendLine("    · DNA — 64-codon degeneracy (crowding);");
        sb.AppendLine("    · software — token-usage k^−1.5 power law (long tail / compression);");
        sb.AppendLine("    · finance — heavy-tailed b^−2 price moves (compression);");
        sb.AppendLine("  - the operator structure is the universal COMPRESSION LAW of any");
        sb.AppendLine("    frequency-ordered system, not network-specific.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL COMPRESSION LAW", CompressionLawPrediction.Classify());
        Assert.True(CompressionLawPrediction.UniversalityScore() >= 5);
        Assert.True(CompressionLawPrediction.CompressionLawUniversal());
        Assert.Contains("UNIVERSAL COMPRESSION LAW", CompressionLawPrediction.Summary());
    }
}
