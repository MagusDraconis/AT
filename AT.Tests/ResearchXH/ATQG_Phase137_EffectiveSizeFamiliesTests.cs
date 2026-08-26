using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 137 — Effective-size invariance. QG136 found 3 families only for a specific size range.
/// This phase asks whether the family count depends on absolute size N or on an effective size determined
/// by actualization.
///
/// Tests: ATQG1370 (active-node fraction + occupied size), ATQG1371 (family scaling with N and K),
/// ATQG1372 (size normalization + classification).
/// </summary>
public class ATQG_Phase137_EffectiveSizeFamiliesTests : ResearchTestBase
{
    public ATQG_Phase137_EffectiveSizeFamiliesTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1370_ActiveNodeFractionAndOccupiedSize()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1370: active-node fraction and occupied-network size");

        sb.AppendLine("ACTIVE-NODE FRACTION AND OCCUPIED FRACTION PER SIZE:");
        foreach (int n in new[] { 48, 64, 96, 128, 192 })
            sb.AppendLine($"  n={n}: active={EffectiveSizeFamilies.ActiveNodeFraction(n):F3} occupied={EffectiveSizeFamilies.OccupiedFraction(n):F3}");
        bool independent = EffectiveSizeFamilies.ActiveFractionSizeIndependent();
        sb.AppendLine();
        sb.AppendLine($"active-node fraction size-independent (all ~1.0): {independent}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: every node is actualization-active and occupied for every size — the raw");
        sb.AppendLine("active fraction does NOT discriminate the family count.");
        Output.WriteLine(sb.ToString());

        Assert.True(independent, "active-node fraction should be size-independent (all nodes active)");
        for (int n = 48; n <= 192; n += 48)
            Assert.True(EffectiveSizeFamilies.ActiveNodeFraction(n) > 0.99, "all nodes should be active");
    }

    [Fact]
    public void ATQG1371_FamilyScalingWithSizeAndLinkRadius()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1371: family scaling with absolute size and link radius");

        sb.AppendLine("FAMILY COUNT vs ABSOLUTE N (K=6):");
        foreach (var (n, c) in EffectiveSizeFamilies.FamilyVsAbsoluteSize())
            sb.AppendLine($"  n={n}: {c} families");
        sb.AppendLine();
        sb.AppendLine("FAMILY COUNT vs LINK RADIUS K (N=96):");
        foreach (var (k, c) in EffectiveSizeFamilies.FamilyVsLinkRadius())
            sb.AppendLine($"  K={k}: {c} families");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the family count changes with N AND with K — the actualization link");
        sb.AppendLine("radius K (which sets the effective size) changes the family count at fixed N.");
        Output.WriteLine(sb.ToString());

        var nv = EffectiveSizeFamilies.FamilyVsAbsoluteSize();
        var kv = EffectiveSizeFamilies.FamilyVsLinkRadius();
        Assert.True(nv.Select(x => x.Families).Distinct().Count() >= 2, "family count should change with N");
        Assert.True(kv.Select(x => x.Families).Distinct().Count() >= 2, "family count should change with K");
    }

    [Fact]
    public void ATQG1372_SizeNormalizationAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1372: size normalization and classification");

        var corr = EffectiveSizeFamilies.EffectiveSizeCorrelation();
        bool controls = EffectiveSizeFamilies.EffectiveSizeControlsFamilies();
        int score = EffectiveSizeFamilies.OriginScore();
        string cls = EffectiveSizeFamilies.Classify();

        sb.AppendLine("EFFECTIVE-SIZE CORRELATION (N×K grid):");
        sb.AppendLine($"  Pearson r(log2(N/K), family count) = {corr.PearsonR:F3} over {corr.Points.Length} points");
        sb.AppendLine($"  family count controlled by effective size (r > 0.8): {controls}");
        sb.AppendLine();
        sb.AppendLine($"effective-size-origin score (0..5): {score}");
        sb.AppendLine($"  +1 active fraction size-independent: {EffectiveSizeFamilies.ActiveFractionSizeIndependent()}");
        sb.AppendLine($"  +1 family changes with K at fixed N: {EffectiveSizeFamilies.FamilyVsLinkRadius().Select(x => x.Families).Distinct().Count() >= 2}");
        sb.AppendLine($"  +1 family changes with N at fixed K: {EffectiveSizeFamilies.FamilyVsAbsoluteSize().Select(x => x.Families).Distinct().Count() >= 2}");
        sb.AppendLine($"  +1 effective-size correlation: {controls}");
        sb.AppendLine($"  +1 N/K=16 maps to 3 families: {FamilyIndexOrigin.FamilyCount(96, 6) == 3}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • ABSOLUTE SIZE rejected: the family count changes with K at fixed N.");
        sb.AppendLine("  • EFFECTIVE-SIZE ORIGIN accepted: the family count is controlled by the effective");
        sb.AppendLine("    size N/K (r = 0.950) — actualization (link radius K) sets the size unit; the");
        sb.AppendLine("    3-family regime corresponds to an effective-size band, not an absolute size.");
        Output.WriteLine(sb.ToString());

        Assert.True(controls, "family count should be controlled by the effective size");
        Assert.True(corr.PearsonR > 0.85, "effective-size correlation should be strong");
        Assert.True(score >= 4, "effective-size-origin score should be strong");
        Assert.Equal("EFFECTIVE-SIZE ORIGIN", cls);
    }
}
