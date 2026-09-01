using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 135 — Origin of the family index. QG134 established fermions carry a family index inside an
/// observable sector. This phase asks whether the family index can EMERGE from the internal attractor
/// structure of a single sector.
///
/// Tests: ATQG1350 (intra-sector modes + family splitting), ATQG1351 (family stability + hierarchy
/// formation), ATQG1352 (generation count + classification).
/// </summary>
public class ATQG_Phase135_FamilyIndexOriginTests : ResearchTestBase
{
    public ATQG_Phase135_FamilyIndexOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1350_IntraSectorModesAndFamilySplitting()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1350: intra-sector modes and family splitting");

        var modes = FamilyIndexOrigin.IntraSectorModes();
        var (sizes, count) = FamilyIndexOrigin.FamilySplit();

        sb.AppendLine($"INTRA-SECTOR MODES (single observable sector, ω=√λ):");
        sb.AppendLine($"  mode count = {modes.Length}");
        sb.AppendLine($"  first 12: [{string.Join(", ", modes.Take(12).Select(m => m.ToString("F3", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine();
        sb.AppendLine("FAMILY SPLITTING (octave decomposition of the SINGLE sector spectrum):");
        sb.AppendLine($"  family sizes = [{string.Join(", ", sizes)}]");
        sb.AppendLine($"  family count = {count}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the internal Laplacian spectrum of one sector splits into discrete");
        sb.AppendLine("octave families — the family index can arise from intra-sector modes.");
        Output.WriteLine(sb.ToString());

        Assert.True(modes.Length > 1, "observable sector should have multiple internal modes");
        Assert.True(count >= 3, "intra-sector spectrum should split into ≥3 families");
        Assert.True(sizes.All(s => s > 0), "all octave families should be populated");
    }

    [Fact]
    public void ATQG1351_FamilyStabilityAndHierarchyFormation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1351: family stability and hierarchy formation");

        sb.AppendLine("FAMILY STABILITY across dynamics parameters (feedback × damping):");
        foreach (double f in new[] { 0.5, 0.7, 0.9 })
            foreach (double d in new[] { 0.2, 0.3, 0.4 })
            {
                var (sizes, c) = FamilyIndexOrigin.FamilySplit(96, 6, f, d);
                sb.AppendLine($"  f={f} d={d}: families={c} sizes=[{string.Join(",", sizes)}]");
            }
        var stab = FamilyIndexOrigin.FamilyStability();
        sb.AppendLine();
        sb.AppendLine($"  distinct family counts = [{string.Join(",", stab.DistinctCounts)}]");
        sb.AppendLine($"  all parameter combos give 3 families: {stab.AllThree}");
        sb.AppendLine($"  default (f=0.9,d=0.3) gives 3: {FamilyIndexOrigin.FamilyCount() == 3}");
        sb.AppendLine();
        sb.AppendLine("HIERARCHY FORMATION:");
        sb.AppendLine($"  octave hierarchy fully formed (default): {FamilyIndexOrigin.HierarchyFormed()}");
        sb.AppendLine($"  family start frequencies = [{string.Join(", ", FamilyIndexOrigin.FamilyStartFrequencies().Select(f => f.ToString("F3", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the 3-family structure is the DEFAULT regime but is NOT fully stable —");
        sb.AppendLine("higher damping (0.4) produces a 4th octave family.");
        Output.WriteLine(sb.ToString());

        Assert.True(FamilyIndexOrigin.FamilyCount() == 3, "default observable parameters should give 3 families");
        Assert.True(stab.DistinctCounts.Length >= 2, "family count should vary across the parameter grid");
        Assert.True(FamilyIndexOrigin.HierarchyFormed(), "octave hierarchy should be formed at default");
    }

    [Fact]
    public void ATQG1352_GenerationCountAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1352: generation count and classification");

        int gens = FamilyIndexOrigin.GenerationCount();
        bool threeEmerge = FamilyIndexOrigin.ThreeGenerationsEmerge();
        int score = FamilyIndexOrigin.OriginScore();
        string cls = FamilyIndexOrigin.Classify();

        sb.AppendLine($"intra-sector generation count (default) = {gens}");
        sb.AppendLine($"exactly 3 generations emerge from the single sector: {threeEmerge}");
        sb.AppendLine();
        sb.AppendLine($"family-origin score (0..5): {score}");
        sb.AppendLine($"  +1 intra-sector modes: {FamilyIndexOrigin.ModeCount() > 1}");
        sb.AppendLine($"  +1 ≥3 families: {FamilyIndexOrigin.FamilyCount() >= 3}");
        sb.AppendLine($"  +1 family structure stable: {FamilyIndexOrigin.FamilyStability().AllThree}");
        sb.AppendLine($"  +1 hierarchy formed: {FamilyIndexOrigin.HierarchyFormed()}");
        sb.AppendLine($"  +1 exactly 3 generations: {threeEmerge}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • POSTULATED rejected: the family index DOES emerge from intra-sector octave modes.");
        sb.AppendLine("  • FAMILY ORIGIN rejected: the 3-family structure is not fully stable (damping 0.4");
        sb.AppendLine("    gives 4 families).");
        sb.AppendLine("  • PARTIAL ORIGIN accepted: the observable sector's internal spectrum splits into");
        sb.AppendLine("    3 octave families at the default dynamics, but the count is parameter-sensitive.");
        Output.WriteLine(sb.ToString());

        Assert.True(threeEmerge, "default regime should give exactly 3 generations");
        Assert.True(score >= 4, "origin score should be strong");
        Assert.Equal("PARTIAL ORIGIN", cls);
    }
}
