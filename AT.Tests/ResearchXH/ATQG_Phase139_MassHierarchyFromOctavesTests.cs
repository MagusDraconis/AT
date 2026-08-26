using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 139 — Mass hierarchy from octave structure. QG138 established the family count follows the
/// octave quantization of the spectrum. This phase asks whether fermion mass hierarchies can emerge from
/// the octave-band structure.
///
/// Tests: ATQG1390 (band positions + spectral gaps), ATQG1391 (octave scaling + mass-ratio analogs),
/// ATQG1392 (family hierarchy + classification).
/// </summary>
public class ATQG_Phase139_MassHierarchyFromOctavesTests : ResearchTestBase
{
    public ATQG_Phase139_MassHierarchyFromOctavesTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1390_BandPositionsAndSpectralGaps()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1390: octave band positions and spectral gaps");

        sb.AppendLine("OCTAVE BAND POSITIONS (observable sector):");
        foreach (var (b, s, c, m) in MassHierarchyFromOctaves.BandPositions())
            sb.AppendLine($"  band {b}: start={s:F3} center={c:F3} modes={m}");
        sb.AppendLine();
        sb.AppendLine("SPECTRAL GAPS BETWEEN BANDS (next start / band end):");
        foreach (var (g, r) in MassHierarchyFromOctaves.SpectralGaps())
            sb.AppendLine($"  gap {g}: ratio={r:F3}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the observable sector's spectrum splits into discrete octave bands with");
        sb.AppendLine("monotone increasing positions — a banded hierarchy exists in the internal spectrum.");
        Output.WriteLine(sb.ToString());

        var bands = MassHierarchyFromOctaves.BandPositions();
        Assert.True(bands.Length >= 3, "should be at least 3 octave bands");
        Assert.True(bands.All(b => b.Start > 0), "band positions should be positive");
        for (int i = 1; i < bands.Length; i++)
            Assert.True(bands[i].Start > bands[i - 1].Start, "band positions should be monotone increasing");
    }

    [Fact]
    public void ATQG1391_OctaveScalingAndMassRatioAnalogs()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1391: octave scaling and mass-ratio analogs");

        var ratios = MassHierarchyFromOctaves.OctaveCenterRatios();
        bool geometric = MassHierarchyFromOctaves.GeometricOctaveScaling();
        var ma = MassHierarchyFromOctaves.MassRatioAnalogs();

        sb.AppendLine($"OCTAVE CENTER RATIOS = [{string.Join(", ", ratios.Select(r => r.ToString("F3", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine($"geometric (factor-2) octave scaling: {geometric}");
        sb.AppendLine();
        sb.AppendLine("MASS-RATIO ANALOGS:");
        sb.AppendLine($"  octave-implied generation ratios = [{string.Join(", ", ma.OctaveImplied.Select(r => r.ToString("F2", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine($"  observed lepton ratios = [{string.Join(", ", ma.LeptonObserved.Select(r => r.ToString("F1", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine($"  octave lines matching a lepton ratio within 25%: {ma.MatchingLines}");
        sb.AppendLine($"  max deviation = {ma.MaxDeviation:F1}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the octave structure gives a geometric (factor-2) ladder, but the implied");
        sb.AppendLine("mass ratios (1:2:4) do NOT match the observed lepton ratios (1:17:207).");
        Output.WriteLine(sb.ToString());

        Assert.True(geometric, "octave scaling should be geometric (factor-2 ladder)");
        Assert.True(ratios.Length >= 3, "should be at least 3 octave rungs");
        Assert.True(ma.MatchingLines == 0, "octave-implied ratios should NOT match lepton ratios");
        Assert.True(ma.MaxDeviation > 5.0, "deviation between octave and lepton ratios should be large");
    }

    [Fact]
    public void ATQG1392_FamilyHierarchyAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1392: family hierarchy and classification");

        var fh = MassHierarchyFromOctaves.FamilyHierarchy();
        int score = MassHierarchyFromOctaves.HierarchyScore();
        string cls = MassHierarchyFromOctaves.Classify();

        sb.AppendLine("FAMILY HIERARCHY FROM OCTAVE STRUCTURE:");
        sb.AppendLine($"  band count = {fh.BandCount} (observed generation count = 3)");
        sb.AppendLine($"  monotone hierarchy: {fh.IsMonotone}");
        sb.AppendLine($"  matches 3 generations: {fh.MatchesThree}");
        sb.AppendLine();
        sb.AppendLine($"hierarchy-origin score (0..5): {score}");
        sb.AppendLine($"  +1 ≥3 bands: {MassHierarchyFromOctaves.BandPositions().Length >= 3}");
        sb.AppendLine($"  +1 monotone: {fh.IsMonotone}");
        sb.AppendLine($"  +1 count = 3: {fh.MatchesThree}");
        sb.AppendLine($"  +1 geometric scaling: {MassHierarchyFromOctaves.GeometricOctaveScaling()}");
        sb.AppendLine($"  +1 ratio match: {MassHierarchyFromOctaves.MassRatioAnalogs().MatchingLines >= 1}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO RELATION rejected: the octave structure reproduces the family count (3) and a");
        sb.AppendLine("    monotone geometric hierarchy.");
        sb.AppendLine("  • HIERARCHY ORIGIN rejected: the octave-implied ratios (1:2:4) do not match the");
        sb.AppendLine("    observed lepton hierarchy (1:17:207).");
        sb.AppendLine("  • PARTIAL RELATION accepted: the generation COUNT and monotone ordering emerge from");
        sb.AppendLine("    octave structure, but the numerical mass ratios do not.");
        Output.WriteLine(sb.ToString());

        Assert.True(fh.MatchesThree, "octave family count should match the 3 generations");
        Assert.True(fh.IsMonotone, "band positions should form a monotone hierarchy");
        Assert.True(score >= 4, "hierarchy score should be strong");
        Assert.Equal("PARTIAL RELATION", cls);
    }
}
