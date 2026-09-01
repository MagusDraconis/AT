using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 125 — Stability of high-energy sectors. QG124 found an energy-ordered sector hierarchy.
/// This phase asks whether the higher sectors remain stable or decay into the observable 3-family sector.
///
/// Tests: ATQG1250 (sector lifetime + attractor stability), ATQG1251 (downward transitions + metastability),
/// ATQG1252 (observable remnants + classification).
/// </summary>
public class ATQG_Phase125_HighEnergySectorStabilityTests : ResearchTestBase
{
    public ATQG_Phase125_HighEnergySectorStabilityTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1250_SectorLifetimeAndAttractorStability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1250: sector lifetime and attractor stability");

        var (_, obsAdj) = HighEnergySectorStability.ObservableSector();
        double baselineRadius = HighEnergySectorStability.RadiusOf(obsAdj);
        var (radii, collapseStep, collapseRadius) = HighEnergySectorStability.SectorLifetime();
        bool fixedPoint = HighEnergySectorStability.HighEnergySectorIsFixedPoint();

        sb.AppendLine("OBSERVABLE BASELINE SECTOR (built at ceiling=1.0):");
        sb.AppendLine($"  radius = {baselineRadius:F3}");
        sb.AppendLine();
        sb.AppendLine("HIGH-ENERGY SECTOR (built at ceiling=8.0) — LIFETIME AFTER ENERGY REMOVAL:");
        for (int i = 0; i < Math.Min(radii.Length, 12); i++)
            sb.AppendLine($"  step {i,2}: radius = {radii[i]:F3}");
        sb.AppendLine($"  collapse step = {collapseStep} (radius falls to baseline at step {collapseStep})");
        sb.AppendLine($"  collapse radius = {collapseRadius:F3}");
        sb.AppendLine();
        sb.AppendLine($"high-energy sector is a fixed point at its own ceiling: {fixedPoint}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the high-energy sector is a STABLE attractor while its energy regime is");
        sb.AppendLine("maintained, but decays toward the observable sector once the energy regime is removed.");
        Output.WriteLine(sb.ToString());

        Assert.True(fixedPoint, "high-energy sector should be a fixed point at its own ceiling");
        Assert.True(collapseStep >= 1 && collapseStep < 40, "energy removal should trigger a downward collapse");
        Assert.True(collapseRadius <= baselineRadius + 0.01, "collapse should land at the observable baseline");
    }

    [Fact]
    public void ATQG1251_DownwardTransitionsAndMetastability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1251: downward transitions and metastability");

        var ladder = HighEnergySectorStability.DownwardLadder();
        int rungs = HighEnergySectorStability.DownwardRungCount();
        var recovery = HighEnergySectorStability.RecoveryAfterDip();

        sb.AppendLine("DOWNWARD CEILING RAMP (high→baseline): distinct radius plateaus:");
        var seen = new List<double>();
        foreach (var (c, r) in ladder)
        {
            if (seen.Count == 0 || Math.Abs(r - seen[^1]) > 1e-6) seen.Add(r);
        }
        for (int i = 0; i < seen.Count; i++)
            sb.AppendLine($"  rung {i}: radius = {seen[i]:F3}");
        sb.AppendLine($"downward rung count = {rungs}");
        sb.AppendLine();
        sb.AppendLine("METASTABILITY (energy dip then restore):");
        sb.AppendLine($"  original radius = {recovery.Original:F3}");
        sb.AppendLine($"  after dip (5 steps at baseline) = {recovery.AfterDip:F3}");
        sb.AppendLine($"  after restore (150 steps at high ceiling) = {recovery.AfterRestore:F3}");
        bool recovered = recovery.AfterRestore >= recovery.Original - 1e-6;
        sb.AppendLine($"  sector re-emerges on energy restoration: {recovered}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: higher sectors decay DOWNWARD through intermediate rungs when energy is");
        sb.AppendLine("removed, but RE-EMERGE when energy is restored — energy-supported (metastable) sectors.");
        Output.WriteLine(sb.ToString());

        Assert.True(rungs >= 2, "downward decay should visit multiple discrete rungs");
        Assert.True(recovery.AfterDip < recovery.Original, "energy dip should trigger decay");
        Assert.True(recovered, "sector should re-emerge after energy restoration");
    }

    [Fact]
    public void ATQG1252_ObservableRemnantsAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1252: observable remnants and classification");

        var remnant = HighEnergySectorStability.ObservableRemnant();
        string cls = HighEnergySectorStability.Classify();

        sb.AppendLine("OBSERVABLE REMNANT (after full decay at baseline):");
        sb.AppendLine($"  remnant radius = {remnant.RemnantRadius:F3}, observable radius = {remnant.ObservableRadius:F3}");
        sb.AppendLine($"  remnant family count = {remnant.RemnantFamilies}");
        sb.AppendLine($"  observable family count = {remnant.ObservableFamilies}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • UNSTABLE rejected: the high-energy sector is a fixed point at its own ceiling.");
        sb.AppendLine("  • STABLE rejected: energy removal collapses the sector to the observable baseline.");
        sb.AppendLine("  • METASTABLE accepted: stable while energy is present, decays downward when it is");
        sb.AppendLine("    removed, and re-emerges when it is restored.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(remnant.ObservableFamilies, remnant.RemnantFamilies);
        Assert.True(Math.Abs(remnant.RemnantRadius - remnant.ObservableRadius) < 0.01,
            "remnant should land in the observable radius class");
        Assert.Equal("METASTABLE", cls);
    }
}
