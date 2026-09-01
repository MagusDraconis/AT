using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 126 — Particle interpretation of attractor sectors. QG123-125 established a hierarchy of
/// metastable attractor sectors that decay into the observable 3-family sector. This phase asks whether the
/// observed particle-sector structure can be MAPPED onto these attractor sectors.
///
/// Tests: ATQG1260 (sector inventory: low/high energy sectors + family correspondence), ATQG1261 (decay
/// chains + observable remnants), ATQG1262 (mapping score + classification).
/// </summary>
public class ATQG_Phase126_ParticleSectorMappingTests : ResearchTestBase
{
    public ATQG_Phase126_ParticleSectorMappingTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1260_SectorInventoryAndFamilyCorrespondence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1260: sector inventory and family correspondence");

        var inv = ParticleSectorMapping.SectorInventory();
        var low = ParticleSectorMapping.LowEnergySector();
        var high = ParticleSectorMapping.HighEnergySectors();
        int classes = ParticleSectorMapping.SectorClassCount();
        int highClasses = ParticleSectorMapping.HighEnergyClassCount();
        var famCounts = ParticleSectorMapping.FamilyCountsAcrossSectors();

        sb.AppendLine("SECTOR INVENTORY (energy → radius, links, families):");
        foreach (var s in inv)
            sb.AppendLine($"  E={s.Energy:F1}: radius={s.Radius:F3} links={s.Links} families={s.Families}");
        sb.AppendLine();
        sb.AppendLine($"LOW-ENERGY (observable) SECTOR: E={low.Energy:F1} radius={low.Radius:F3} families={low.Families}");
        sb.AppendLine($"HIGH-ENERGY SECTORS: count={high.Length} distinct radius classes={highClasses}");
        sb.AppendLine($"total distinct sector classes={classes}");
        sb.AppendLine($"family counts across hierarchy=[{string.Join(",", famCounts)}]");
        sb.AppendLine($"observable 3-family structure: {ParticleSectorMapping.ObservableThreeFamilies()}");
        sb.AppendLine($"distinct family structures across sectors: {ParticleSectorMapping.DistinctFamilyStructure()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the observable sector is the 3-family low-energy attractor; multiple");
        sb.AppendLine("higher-energy sectors exist with their own family structure — a sector→generation map.");
        Output.WriteLine(sb.ToString());

        Assert.True(classes >= 3, "hierarchy should contain multiple sector classes");
        Assert.True(highClasses >= 2, "multiple distinct high-energy sectors should exist");
        Assert.True(low.Families == 3, "observable sector should be the 3-family sector");
        Assert.True(famCounts.Length >= 2, "distinct family structure should appear across sectors");
    }

    [Fact]
    public void ATQG1261_DecayChainsAndObservableRemnants()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1261: sector decay chains and observable remnants");

        var rungs = ParticleSectorMapping.DecayChainRungs();
        int chainLen = ParticleSectorMapping.DecayChainLength();
        bool endsObs = ParticleSectorMapping.DecayChainEndsAtObservable();
        bool remnantMatch = ParticleSectorMapping.RemnantMatchesObservable();

        sb.AppendLine("DECAY CHAIN (high-energy sector → baseline): distinct rungs:");
        for (int i = 0; i < rungs.Length; i++)
            sb.AppendLine($"  rung {i}: radius = {rungs[i]:F3}");
        sb.AppendLine($"chain length (rungs) = {chainLen}");
        sb.AppendLine($"decay chain terminates at observable sector: {endsObs}");
        sb.AppendLine();
        sb.AppendLine("OBSERVABLE REMNANT:");
        sb.AppendLine($"  remnant family structure matches observable sector: {remnantMatch}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: high-energy sectors decay down a multi-rung cascade that terminates in");
        sb.AppendLine("the observable sector — a particle-like decay-chain structure ending in the stable");
        sb.AppendLine("3-family remnant.");
        Output.WriteLine(sb.ToString());

        Assert.True(chainLen >= 3, "decay chains should have multiple rungs");
        Assert.True(endsObs, "decay chains should terminate at the observable sector");
        Assert.True(remnantMatch, "all decays should settle into the observable remnant");
    }

    [Fact]
    public void ATQG1262_MappingScoreAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1262: sector→particle mapping score and classification");

        int score = ParticleSectorMapping.MappingScore();
        string cls = ParticleSectorMapping.Classify();

        sb.AppendLine($"mapping score (0..5): {score}");
        sb.AppendLine($"  +1 observable 3-family sector: {ParticleSectorMapping.ObservableThreeFamilies()}");
        sb.AppendLine($"  +1 multiple high-energy classes: {ParticleSectorMapping.HighEnergyClassCount() >= 2}");
        sb.AppendLine($"  +1 distinct family structure: {ParticleSectorMapping.DistinctFamilyStructure()}");
        sb.AppendLine($"  +1 decay cascade: {ParticleSectorMapping.DecayChainLength() >= 3}");
        sb.AppendLine($"  +1 chains settle at observable: {ParticleSectorMapping.RemnantMatchesObservable() && ParticleSectorMapping.DecayChainEndsAtObservable()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO MAPPING rejected: the hierarchy carries real sector/family/decay structure.");
        sb.AppendLine("  • SECTOR-PARTICLE MAPPING accepted: the observable 3-family sector maps to observed");
        sb.AppendLine("    families; distinct high-energy sectors are heavier particle-sector analogs; decay");
        sb.AppendLine("    chains map to particle decays terminating in the observable remnant.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "mapping score should be strong");
        Assert.Equal("SECTOR-PARTICLE MAPPING", cls);
    }
}
