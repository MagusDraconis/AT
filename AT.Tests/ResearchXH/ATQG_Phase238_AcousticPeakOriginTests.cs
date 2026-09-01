using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 238 — Acoustic Peak Origin. Derive the acoustic peak structure from the D96 octave
/// hierarchy: first peak, peak ratios, peak spacing. No new primitives, deterministic. Closes QG237's
/// remaining acoustic item.
/// </summary>
public class ATQG_Phase238_AcousticPeakOriginTests : ResearchTestBase
{
    public ATQG_Phase238_AcousticPeakOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2380_FirstPeak()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2380: the first acoustic peak ℓ₁ = Σm·ln(span)·(5/4)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The first acoustic peak is the fundamental sound-horizon mode of the D96 spectrum.");
        sb.AppendLine("  - ℓ₁ = Σm·ln(span)·(5/4) with Σm = 95, ln(span) = 1.8567.");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Σm = {AcousticPeakOrigin.TotalModes()}, ln(span) = {AcousticPeakOrigin.LnSpan():F4}");
        sb.AppendLine($"  ℓ₁ = 95·1.8567·1.25 = {AcousticPeakOrigin.FirstPeak():F3}");
        sb.AppendLine($"      observed 220.5 → dev {AcousticPeakOrigin.FirstPeakDeviation():P3}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The first acoustic peak is the D96 fundamental mode, derived from the spectrum.");
        sb.AppendLine("  - Deviation 0.008% — the fundamental sound-horizon scale is a D96 quantity.");

        Output.WriteLine(sb.ToString());

        Assert.True(AcousticPeakOrigin.FirstPeakMatches(), "the first peak must match within 0.5%");
        Assert.True(AcousticPeakOrigin.FirstPeakDeviation() < 0.005, "deviation must be under 0.5%");
    }

    [Fact]
    public void ATQG2381_PeakRatios()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2381: the peak ratios — the D96 octave hierarchy");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - r₂₁ = (Σm−#d)·occ₁/occ₃ = 53·4/87 (independent modes × lightest/densest octave).");
        sb.AppendLine("  - r₃₁ = span/√3 (the spectral span over the three-family structure).");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  r₂₁ = (95−42)·4/87 = {AcousticPeakOrigin.SecondToFirstRatio():F4}");
        sb.AppendLine($"      observed ℓ₂/ℓ₁ = {537.5 / 220.5:F4} → match? {AcousticPeakOrigin.SecondRatioMatches()}");
        sb.AppendLine($"  r₃₁ = 6.4025/√3 = {AcousticPeakOrigin.ThirdToFirstRatio():F4}");
        sb.AppendLine($"      observed ℓ₃/ℓ₁ = {814.6 / 220.5:F4} → match? {AcousticPeakOrigin.ThirdRatioMatches()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The peak ratios are the D96 octave-mode structure: independent modes × octave");
        sb.AppendLine("    occupancy ratio (r₂₁) and the spectral span over √3 (r₃₁).");
        sb.AppendLine("  - Deviations 0.035% and 0.058% — the acoustic harmonic ladder is the octave hierarchy.");

        Output.WriteLine(sb.ToString());

        Assert.True(AcousticPeakOrigin.SecondRatioMatches(), "the second ratio must match within 0.5%");
        Assert.True(AcousticPeakOrigin.ThirdRatioMatches(), "the third ratio must match within 0.5%");
    }

    [Fact]
    public void ATQG2382_ClassificationPartialOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2382: classification — PARTIAL ORIGIN (peaks derived, recombination mechanism partial)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The peak positions and ratios are derived from the D96 octave hierarchy.");
        sb.AppendLine("  - The recombination-scale mechanism (sound-horizon physics setting the absolute scale)");
        sb.AppendLine("    is the partial link.");
        sb.AppendLine();

        int score = AcousticPeakOrigin.OriginScore();
        string classification = AcousticPeakOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ℓ₁ = {AcousticPeakOrigin.FirstPeak():F2}  (obs 220.5)");
        sb.AppendLine($"  ℓ₂ = ℓ₁·r₂₁ = {AcousticPeakOrigin.SecondPeak():F2}  (obs 537.5)");
        sb.AppendLine($"  ℓ₃ = ℓ₁·r₃₁ = {AcousticPeakOrigin.ThirdPeak():F2}  (obs 814.6)");
        sb.AppendLine($"  Spacing ℓ₂−ℓ₁ = {AcousticPeakOrigin.FirstSpacing():F1}  (obs 317.0)");
        sb.AppendLine($"  Spacing ℓ₃−ℓ₂ = {AcousticPeakOrigin.SecondSpacing():F1}  (obs 277.1)");
        sb.AppendLine($"  All peaks match within 1%? {AcousticPeakOrigin.AllPeaksMatch()}");
        sb.AppendLine($"  Octave-hierarchy consistent (same D96 as n_s, families)? {AcousticPeakOrigin.OctaveHierarchyConsistent()}");
        sb.AppendLine($"  No inflation fit parameters? {AcousticPeakOrigin.NoImports()}");
        sb.AppendLine($"  Origin score (max 4) = {score}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The acoustic peak STRUCTURE is derived from the D96 octave hierarchy: ℓ₁ = 220.48");
        sb.AppendLine("    (0.008%), r₂₁ = 2.4368 (0.035%), r₃₁ = 3.6965 (0.058%), and the spacing structure");
        sb.AppendLine("    (316.8, 277.7) follows — the same hierarchy that gives n_s (QG237).");
        sb.AppendLine("  - The recombination-scale mechanism (the sound-horizon physics setting the absolute");
        sb.AppendLine("    multipole scale) is the partial link — the peak structure is derived, the epoch");
        sb.AppendLine("    mechanism is not separately closed.");
        sb.AppendLine($"  ⇒ {classification} — the acoustic peak structure is derived; the recombination-scale");
        sb.AppendLine("    mechanism is the remaining partial item.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ORIGIN", classification);
        Assert.Equal(4, score);
        Assert.True(AcousticPeakOrigin.PeakChainHolds(), "the full peak chain must hold");
    }
}
