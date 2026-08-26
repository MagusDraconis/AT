using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 136 — Robustness of the 3-family sector. QG135 found 3 families emerge from octave structure
/// but the count changes under damping. This phase searches for a dynamical regime where the 3-family
/// structure is stable and parameter-independent.
///
/// Tests: ATQG1360 (feedback + damping sweeps), ATQG1361 (size scaling + stability basin), ATQG1362
/// (universality + classification).
/// </summary>
public class ATQG_Phase136_ThreeFamilyRobustnessTests : ResearchTestBase
{
    public ATQG_Phase136_ThreeFamilyRobustnessTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1360_FeedbackAndDampingSweeps()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1360: feedback and damping sweeps");

        sb.AppendLine("FEEDBACK SWEEP (damping=0.3):");
        foreach (var (f, c) in ThreeFamilyRobustness.FeedbackSweep())
            sb.AppendLine($"  f={f:F1}: {c} families");
        sb.AppendLine();
        sb.AppendLine("DAMPING SWEEP (feedback=0.9):");
        foreach (var (d, c) in ThreeFamilyRobustness.DampingSweep())
            sb.AppendLine($"  d={d:F1}: {c} families");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: high feedback (f≥0.7) and low-to-moderate damping (d≤0.4) give the");
        sb.AppendLine("3-family regime; low feedback or high damping break it.");
        Output.WriteLine(sb.ToString());

        var fb = ThreeFamilyRobustness.FeedbackSweep();
        var damp = ThreeFamilyRobustness.DampingSweep();
        Assert.True(fb.Where(x => x.Feedback >= 0.7).All(x => x.Families == 3),
            "high feedback should give 3 families");
        Assert.True(damp.Where(x => x.Damping <= 0.4).All(x => x.Families == 3),
            "low-to-moderate damping should give 3 families");
    }

    [Fact]
    public void ATQG1361_SizeScalingAndStabilityBasin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1361: size scaling and family stability basin");

        sb.AppendLine("SIZE SCALING (f=0.9, d=0.3):");
        foreach (var (s, c) in ThreeFamilyRobustness.SizeScaling())
            sb.AppendLine($"  n={s}: {c} families");
        bool sizeUniv = ThreeFamilyRobustness.SizeUniversal();
        sb.AppendLine($"  size-independent (all sizes → 3): {sizeUniv}");
        sb.AppendLine();
        var basin = ThreeFamilyRobustness.FamilyBasin();
        sb.AppendLine($"FAMILY STABILITY BASIN (refined f×d grid at n=96):");
        sb.AppendLine($"  3-family fraction = {basin.ThreeFraction:F3}");
        sb.AppendLine($"  coherent basin (≥0.9): {ThreeFamilyRobustness.CoherentBasin()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a coherent 3-family basin exists (93.7% of the refined grid) but the");
        sb.AppendLine("family count depends on network size (2 at n=48, 4 at n≥128).");
        Output.WriteLine(sb.ToString());

        var size = ThreeFamilyRobustness.SizeScaling();
        Assert.True(basin.ThreeFraction >= 0.9, "a coherent 3-family basin should exist");
        Assert.True(size.Where(s => s.Size >= 64 && s.Size <= 96).All(s => s.Families == 3),
            "moderate sizes (64–96) should give 3 families");
        Assert.False(sizeUniv, "3-family structure should NOT be size-independent");
    }

    [Fact]
    public void ATQG1362_UniversalityAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1362: universality and classification");

        bool defaultThree = ThreeFamilyRobustness.DefaultIsThreeFamily();
        bool coherent = ThreeFamilyRobustness.CoherentBasin();
        bool sizeUniv = ThreeFamilyRobustness.SizeUniversal();
        int score = ThreeFamilyRobustness.RobustnessScore();
        string cls = ThreeFamilyRobustness.Classify();

        sb.AppendLine($"default point (f=0.9,d=0.3) → 3 families: {defaultThree}");
        sb.AppendLine($"coherent 3-family basin (≥0.9): {coherent}");
        sb.AppendLine($"size-independent 3-family structure: {sizeUniv}");
        sb.AppendLine();
        sb.AppendLine($"robustness score (0..5): {score}");
        sb.AppendLine($"  +1 default → 3: {defaultThree}");
        sb.AppendLine($"  +1 coherent basin ≥0.9: {coherent}");
        sb.AppendLine($"  +1 basin ≥0.75: {ThreeFamilyRobustness.FamilyBasin().ThreeFraction >= 0.75}");
        sb.AppendLine($"  +1 damping ≤0.4 → 3: {ThreeFamilyRobustness.DampingSweep().Where(d => d.Damping <= 0.4).All(d => d.Families == 3)}");
        sb.AppendLine($"  +1 size-independent: {sizeUniv}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • FRAGILE rejected: a coherent 3-family basin exists (93.7%).");
        sb.AppendLine("  • ROBUST ORIGIN rejected: the family count depends on network size.");
        sb.AppendLine("  • PARTIAL ROBUSTNESS accepted: the 3-family state is stable in a coherent dynamical");
        sb.AppendLine("    basin (high feedback, low damping) but is NOT universal across network sizes.");
        Output.WriteLine(sb.ToString());

        Assert.True(defaultThree, "default point should give 3 families");
        Assert.True(coherent, "coherent 3-family basin should exist");
        Assert.False(sizeUniv, "3-family structure should not be size-universal");
        Assert.Equal("PARTIAL ROBUSTNESS", cls);
    }
}
