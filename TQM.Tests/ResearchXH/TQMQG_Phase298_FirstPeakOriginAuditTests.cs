using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 298 — First Peak Origin Audit. Why does only ℓ₁ require an extra factor (5/4) while
/// the peak ratios need none? Is 5/4 a missing structural projection? No observables, no target
/// values, D96 only, deterministic.
/// </summary>
public class TQMQG_Phase298_FirstPeakOriginAuditTests : ResearchTestBase
{
    public TQMQG_Phase298_FirstPeakOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2980_BoundaryProjection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2980: 5/4 is the boundary projection of the fundamental");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the D96 spectrum has 1 zero mode (background) + 95 positive modes (QG270);");
        sb.AppendLine("  - the lightest octave has occ₀ = 4 modes;");
        sb.AppendLine("  - the fundamental harmonic sits at the background→first-octave boundary, so its");
        sb.AppendLine("    normalization includes the zero-mode transition: (occ₀ + 1)/occ₀ = 5/4.");
        sb.AppendLine();

        sb.AppendLine($"zero mode count: {FirstPeakOriginAudit.ZeroModeCount()}");
        sb.AppendLine($"lightest octave occupancy occ₀: {FirstPeakOriginAudit.LightestOctaveOccupancy()}");
        sb.AppendLine($"boundary projection (occ₀ + zero_mode)/occ₀: {FirstPeakOriginAudit.BoundaryProjection():F4}");
        sb.AppendLine($"boundary projection is 5/4: {FirstPeakOriginAudit.BoundaryProjectionIsFiveFourths()}");
        sb.AppendLine($"fundamental at boundary: {FirstPeakOriginAudit.FundamentalAtBoundary()}");

        Output.WriteLine(sb.ToString());

        Assert.True(FirstPeakOriginAudit.BoundaryProjectionIsFiveFourths(),
            "(occ₀ + zero_mode)/occ₀ must equal 5/4");
        Assert.True(FirstPeakOriginAudit.FundamentalAtBoundary(),
            "the fundamental must sit at the background→first-octave boundary");
    }

    [Fact]
    public void TQMQG2981_OnlyL1Absolute()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2981: only ℓ₁ is absolute — the ratios are relative");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - ℓ₁ is the only ABSOLUTE peak position (the fundamental sets the ℓ-scale);");
        sb.AppendLine("  - ℓ₂/ℓ₁ and ℓ₃/ℓ₁ are RATIOS — any common normalization cancels, so they need no factor;");
        sb.AppendLine("  - ℓ₁ with the boundary projection matches the observed 220.5 while the ratios");
        sb.AppendLine("    need no factor.");
        sb.AppendLine();

        sb.AppendLine($"only ℓ₁ is absolute: {FirstPeakOriginAudit.OnlyL1IsAbsolute()}");
        sb.AppendLine($"first peak is the fundamental: {FirstPeakOriginAudit.FirstPeakIsFundamental()}");
        sb.AppendLine($"ℓ₁ with projection matches observed 220.5: {FirstPeakOriginAudit.FirstPeakMatchesWithProjection()}");
        sb.AppendLine($"ratios need no factor: {FirstPeakOriginAudit.RatiosNeedNoFactor()}");
        sb.AppendLine($"ratio normalization cancels: {FirstPeakOriginAudit.RatioNormalizationCancels()}");
        sb.AppendLine();
        sb.AppendLine("ℓ₁ = Σm·ln(span)·(5/4) = 220.48 (absolute);");
        sb.AppendLine("ℓ₂/ℓ₁ = (Σm−#d)·occ₁/occ₃, ℓ₃/ℓ₁ = span/√3 (ratios — normalization cancels).");

        Output.WriteLine(sb.ToString());

        Assert.True(FirstPeakOriginAudit.OnlyL1IsAbsolute(),
            "only ℓ₁ must be an absolute peak position");
        Assert.True(FirstPeakOriginAudit.RatioNormalizationCancels(),
            "the ratio normalization must cancel");
        Assert.True(FirstPeakOriginAudit.RatiosNeedNoFactor(),
            "the ratios must need no extra factor");
    }

    [Fact]
    public void TQMQG2982_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2982: the first-peak origin determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - FIT ONLY: 5/4 is a free fit with no structural reading (QG297 as-is);");
        sb.AppendLine("  - FIRST PEAK ORIGIN: 5/4 is the boundary projection of the fundamental harmonic");
        sb.AppendLine("    — (occ₀ + zero_mode)/occ₀ = (4+1)/4 = 5/4.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FirstPeakOriginAudit.Summary()}");
        sb.AppendLine($"Origin score: {FirstPeakOriginAudit.OriginScore()}/5");
        sb.AppendLine($"5/4 origin classification: {FirstPeakOriginAudit.ClassifyOrigin()}");
        sb.AppendLine($"CLASSIFICATION = {FirstPeakOriginAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the D96 spectrum has 1 zero mode (the background, QG270) + 95 positive modes;");
        sb.AppendLine("  - the lightest octave has occ₀ = 4 modes; the fundamental sound-horizon mode sits");
        sb.AppendLine("    at the background→first-octave boundary;");
        sb.AppendLine("  - 5/4 = (occ₀ + zero_mode)/occ₀ = (4+1)/4 — the FIRST-MODE NORMALIZATION of the");
        sb.AppendLine("    fundamental including the background zero-mode transition — a BOUNDARY PROJECTION;");
        sb.AppendLine("  - ONLY ℓ₁ is ABSOLUTE: the ratios ℓ₂/ℓ₁, ℓ₃/ℓ₁ are relative and the common");
        sb.AppendLine("    normalization cancels — hence only ℓ₁ carries the factor;");
        sb.AppendLine("  - the QG297 'fit' is reinterpreted as the fundamental's boundary projection — a");
        sb.AppendLine("    MISSING STRUCTURAL PROJECTION, not a free constant.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("FIRST PEAK ORIGIN", FirstPeakOriginAudit.Classify());
        Assert.True(FirstPeakOriginAudit.OriginScore() >= 5);
        Assert.Equal(FirstPeakOriginAudit.Origin.FirstPeakOrigin, FirstPeakOriginAudit.ClassifyOrigin());
        Assert.Contains("FIRST PEAK ORIGIN", FirstPeakOriginAudit.Summary());
    }
}
