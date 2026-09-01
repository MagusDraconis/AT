using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 134 — Boson-fermion calibration split. QG133 showed boson anchors agree while fermion
/// anchors shift. This phase asks why the ladder calibrates consistently to bosons but not fermions.
///
/// Tests: ATQG1340 (boson vs fermion sector mapping), ATQG1341 (family-index effects + generation gap),
/// ATQG1342 (calibration universality + classification).
/// </summary>
public class ATQG_Phase134_BosonFermionSplitTests : ResearchTestBase
{
    public ATQG_Phase134_BosonFermionSplitTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1340_BosonAndFermionSectorMapping()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1340: boson vs fermion sector mapping");

        sb.AppendLine($"ladder radius span = {BosonFermionSplit.LadderRadiusSpan:F3}");
        sb.AppendLine();
        sb.AppendLine("BOSON RATIOS (single family-index states):");
        foreach (var (n, r) in BosonFermionSplit.BosonRatios())
            sb.AppendLine($"  {n} = {r:F3}  within ladder span: {r <= BosonFermionSplit.LadderRadiusSpan + 1e-9}");
        sb.AppendLine($"  all boson ratios within ladder span: {BosonFermionSplit.BosonsWithinLadderSpan()}");
        sb.AppendLine($"  all boson ratios on single-index O(1)-few scale: {BosonFermionSplit.BosonsSingleIndexScale()}");
        sb.AppendLine();
        sb.AppendLine("FERMION RATIOS (3-generation states):");
        foreach (var (n, r) in BosonFermionSplit.FermionRatios())
            sb.AppendLine($"  {n} = {r:F1}  beyond ladder span: {r > BosonFermionSplit.LadderRadiusSpan + 1e-9}");
        sb.AppendLine($"  any fermion ratio beyond ladder span: {BosonFermionSplit.FermionsBeyondLadderSpan()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: boson masses sit within the ladder span (O(1)-few × Z) while the lepton");
        sb.AppendLine("generation ratios vastly exceed it — the ladder resolves bosons, not generations.");
        Output.WriteLine(sb.ToString());

        Assert.True(BosonFermionSplit.BosonsWithinLadderSpan(), "boson ratios should lie within the ladder span");
        Assert.True(BosonFermionSplit.BosonsSingleIndexScale(), "boson ratios should be O(1)-few");
        Assert.True(BosonFermionSplit.FermionsBeyondLadderSpan(), "fermion generation ratios should exceed the ladder span");
    }

    [Fact]
    public void ATQG1341_FamilyIndexEffectsAndGenerationGap()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1341: family-index effects and generation hierarchy gap");

        int fam = BosonFermionSplit.ObservableFamilyCount();
        int idxClasses = BosonFermionSplit.FamilyIndexClasses();
        double gap = BosonFermionSplit.GenerationGapFactor();

        sb.AppendLine("FAMILY-INDEX EFFECTS:");
        sb.AppendLine($"  observable sector (radius 6) family count = {fam}");
        sb.AppendLine($"  observable sector is a 3-family sector: {BosonFermionSplit.ObservableIsThreeFamily()}");
        sb.AppendLine($"  family-index classes resolved by the observable sector = {idxClasses}");
        sb.AppendLine("  ⇒ fermion generations are carried by a family index WITHIN the observable sector,");
        sb.AppendLine("    not by separate ladder rungs (bosons are single family-index states per rung).");
        sb.AppendLine();
        sb.AppendLine("GENERATION HIERARCHY GAP:");
        sb.AppendLine($"  largest lepton ratio / ladder radius span = {gap:F1}");
        sb.AppendLine($"  generation gap is large (>> ladder span): {BosonFermionSplit.GenerationGapLarge()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the observable sector is 3-family — the generations live inside it, so a");
        sb.AppendLine("single linear ladder cannot place them on separate rungs (generation gap factor ~1200).");
        Output.WriteLine(sb.ToString());

        Assert.True(BosonFermionSplit.ObservableIsThreeFamily(), "observable sector should be 3-family");
        Assert.True(idxClasses >= 2, "family index should resolve multiple classes");
        Assert.True(BosonFermionSplit.GenerationGapLarge(), "generation gap should greatly exceed the ladder span");
    }

    [Fact]
    public void ATQG1342_CalibrationUniversalityAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1342: calibration universality and classification");

        double bosonAgree = BosonFermionSplit.BosonAnchorAgreement();
        double fermionSpread = BosonFermionSplit.FermionAnchorSpread();
        bool universal = BosonFermionSplit.BosonsCalibrateUniversally();
        int score = BosonFermionSplit.SplitScore();
        string cls = BosonFermionSplit.Classify();

        sb.AppendLine($"boson-anchor agreement (Z vs W): {100 * bosonAgree:F2}%");
        sb.AppendLine($"fermion-anchor spread (H vs t): {100 * fermionSpread:F2}%");
        sb.AppendLine($"bosons calibrate universally (agreement ≪ spread): {universal}");
        sb.AppendLine();
        sb.AppendLine($"split score (0..5): {score}");
        sb.AppendLine($"  +1 bosons within ladder span: {BosonFermionSplit.BosonsWithinLadderSpan()}");
        sb.AppendLine($"  +1 bosons on single-index scale: {BosonFermionSplit.BosonsSingleIndexScale()}");
        sb.AppendLine($"  +1 fermions beyond ladder span: {BosonFermionSplit.FermionsBeyondLadderSpan()}");
        sb.AppendLine($"  +1 observable 3-family sector: {BosonFermionSplit.ObservableIsThreeFamily()}");
        sb.AppendLine($"  +1 bosons calibrate universally: {universal}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO SPLIT rejected: bosons and fermions map differently by structure.");
        sb.AppendLine("  • FUNDAMENTAL SPLIT accepted: bosons are single family-index states on ladder rungs");
        sb.AppendLine("    (ratios within span, anchors agree), while fermions are 3-family states whose");
        sb.AppendLine("    generations are resolved by a family index WITHIN the observable sector (ratios");
        sb.AppendLine("    far beyond the span, anchors spread).");
        Output.WriteLine(sb.ToString());

        Assert.True(bosonAgree < 0.1 * fermionSpread, "boson anchors should agree much tighter than fermion anchors");
        Assert.True(score >= 4, "split score should be strong");
        Assert.Equal("FUNDAMENTAL SPLIT", cls);
    }
}
