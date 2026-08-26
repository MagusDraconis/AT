using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 210 — Family Index Exact Origin. Derive the family index exactly — why family = 1, 2, 3
/// and no fourth family — from the D96 spectral structure (octaves, occupancies, Z2, spectral gaps).
/// No fitted parameters, deterministic.
/// </summary>
public class ATQG_Phase210_FamilyIndexExactOriginTests : ResearchTestBase
{
    public ATQG_Phase210_FamilyIndexExactOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2100_FamilyCountIsOctaveBandCount()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2100: the family count is the octave-band count");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - familyCount = floor(log2(span)) + 1 (QG138).");
        sb.AppendLine("  - D96 span = 6.4025 (QG161); octave occupancies [4,4,87].");
        sb.AppendLine();

        double span = FamilyIndexExactOrigin.Span();
        double log2span = FamilyIndexExactOrigin.Log2Span();
        int fromSpan = FamilyIndexExactOrigin.FamilyCountFromSpan();
        int fromOcc = FamilyIndexExactOrigin.FamilyCountFromOccupancies();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  span = {span:F4}, log2(span) = {log2span:F4}");
        sb.AppendLine($"  familyCount = floor({log2span:F4}) + 1 = {fromSpan}");
        sb.AppendLine($"  octave occupancies: {string.Join(", ", FamilyIndexExactOrigin.OctaveOccupancies())} → {fromOcc} bands");
        sb.AppendLine($"  identity holds? {FamilyIndexExactOrigin.FamilyBandIdentity()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The family count = floor(log2(span)) + 1 = 3.");
        sb.AppendLine("  - The three octave bands [4,4,87] are the three families.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, fromSpan);
        Assert.Equal(3, fromOcc);
        Assert.True(FamilyIndexExactOrigin.FamilyBandIdentity(), "the span-derived and occupancy-derived counts must agree");
    }

    [Fact]
    public void ATQG2101_WhyNoFourthFamily()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2101: why there is no fourth family");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - A 4th family would require a 4th octave band [8ω_min, 16ω_min).");
        sb.AppendLine("  - That requires log2(span) ≥ 3, i.e. span ≥ 8.");
        sb.AppendLine();

        double span = FamilyIndexExactOrigin.Span();
        double threshold = FamilyIndexExactOrigin.FourthFamilyThreshold();
        double margin = FamilyIndexExactOrigin.FourthFamilyMargin();
        bool noFourth = FamilyIndexExactOrigin.NoFourthFamily();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  4th-family threshold: span ≥ {threshold:F0} (log2(span) ≥ 3)");
        sb.AppendLine($"  D96 span = {span:F4}");
        sb.AppendLine($"  Margin below threshold: {threshold} − {span:F4} = {margin:F4} ({margin / threshold * 100:F1}%)");
        sb.AppendLine($"  No 4th family? {noFourth}");
        sb.AppendLine($"  Octave band boundaries: {string.Join(", ", FamilyIndexExactOrigin.OctaveBandBoundaries().Select(b => b.ToString("F3", CultureInfo.InvariantCulture)))}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The D96 spectrum spans 2.678 octaves (< 3): the 4th band [8ω_min, 16ω_min) is empty.");
        sb.AppendLine("  - The 20% margin below the threshold excludes the 4th family exactly.");

        Output.WriteLine(sb.ToString());

        Assert.True(noFourth, "the D96 span must be below the 4th-family threshold");
        Assert.True(margin > 1.0, "the margin below threshold must be substantial");
    }

    [Fact]
    public void ATQG2102_ClassificationExactOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2102: classification — EXACT ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The family count is the octave-band count; the index is the band index.");
        sb.AppendLine("  - Consistent with the lepton hierarchy (QG209) and gauge sector (QG161).");
        sb.AppendLine();

        int score = FamilyIndexExactOrigin.OriginScore();
        string classification = FamilyIndexExactOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Origin score (max 5) = {score}");
        sb.AppendLine($"    +1 familyCount = floor(log2(span)) + 1 = 3 ({FamilyIndexExactOrigin.FamilyCountFromSpan()})");
        sb.AppendLine($"    +1 three octave bands ({FamilyIndexExactOrigin.FamilyBands().Length})");
        sb.AppendLine($"    +1 no 4th family (span < 8) ({FamilyIndexExactOrigin.NoFourthFamily()})");
        sb.AppendLine($"    +1 identity holds ({FamilyIndexExactOrigin.FamilyBandIdentity()})");
        sb.AppendLine($"    +1 consistent with hierarchy + gauge ({FamilyIndexExactOrigin.ConsistentWithLeptonHierarchy()} && {FamilyIndexExactOrigin.ConsistentWithGaugeSector()})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - family = 1, 2, 3 are the three octave bands [4,4,87] of the D96 spectrum.");
        sb.AppendLine("  - No 4th family: the span 6.4025 < 8 excludes the 4th octave band.");
        sb.AppendLine("  - The family index is an exact D96 spectral identity.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("EXACT ORIGIN", classification);
        Assert.Equal(5, score);
    }
}
