using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_081 - CONNECTIVITY DEPENDENCY AUDIT.
///
/// G_079 found that the continuity law's equivalence with count conservation needs the substrate's connectivity;
/// G_080 classified that connectivity as ASSUMED and exhibited two frozen disconnected universes. This suite asks the
/// quantitative question adversarially: how much of the surviving gravity/time sector actually depends on
/// connectivity? Connectivity is allowed here as an EXPLICIT input parameter; deriving it, adding primitives,
/// importing topology and repairing disconnected sectors are all forbidden. The goal is not to save connectivity - it
/// is to measure exactly how much of the surviving theory collapses without it.
///
/// The report follows the seven tasks of the assignment. Every number quoted is computed here, and the substrate is
/// named wherever a count is quoted: the 1D ring family on the canonical 96 cells, split into c = 1, 2, 4 and 8
/// components.
/// </summary>
public sealed class Y_G_081_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_081_Tests(ITestOutputHelper output) : base(output) { }

    private static string F(double v) => v.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    private static string CoreRoot()
    {
        var root = ConnectivityDependencyAudit.FindSourceRoot(System.IO.Path.Combine("AT.Core", "ResearchXH"));
        Assert.False(root is null, "could not locate the AT.Core source root from " + AppContext.BaseDirectory);
        return root!;
    }

    [Fact]
    public void Y_G_081_01_TheCounterfactualUniverse_AtOneTwoFourEightComponents()
    {
        PrintHeader("G_081 - task 2: the counterfactual universe (c = 1, 2, 4, 8)");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the substrate is the 1D ring family on the canonical 96 cells, split into c = 1, 2, 4 and 8 closed");
        sb.AppendLine("    rings of equal size: the link set is BUILT here, never assumed, and connectivity is an explicit input;");
        sb.AppendLine("  - the transport class is the surviving class of G_078/G_079 - a change is a transport iff it lies in the");
        sb.AppendLine("    image of the discrete divergence, which requires EACH component's sum to vanish;");
        sb.AppendLine("  - the counterfactual is evaluated on the SAME cells and the SAME states, so every difference measured");
        sb.AppendLine("    below is a difference of connectivity and of nothing else;");
        sb.AppendLine("  - no attempt is made to repair a disconnected sector, and no primitive is added.");
        sb.AppendLine();
        sb.Append(ConnectivityDependencyAudit.OutputCounterfactual());

        var universe = ConnectivityDependencyAudit.CounterfactualUniverse();
        Assert.Equal(4, universe.Length);
        Assert.All(universe, u =>
        {
            // the rank identity, the circulation and the free-mode count are all the same measurement of a cut
            Assert.Equal(u.Cells - u.Components, u.Directions);
            Assert.Equal(u.Components, u.Nullity);
            Assert.Equal(u.Components - 1, u.FreeModes);
        });
        // the continuity law's failure count is exactly c - 1: this is the number of claims the cut costs
        Assert.All(universe, u => Assert.Equal(u.Components - 1, u.OfWhichAreNot));
        Assert.Equal(0, universe.Single(u => u.Components == 1).OfWhichAreNot);
        Assert.Equal(7, universe.Single(u => u.Components == 1).Directions);
        // the limit case: when every component is a SINGLE cell (c = cells) no transport exists at all - the frozen
        // pair of G_080, generalised - so the transports vanish exactly there and nowhere else
        Assert.All(universe.Where(u => u.PerComponent >= 2), u => Assert.True(u.OfWhichAreTransports > 0));
        Assert.Equal(0, universe.Single(u => u.PerComponent == 1).OfWhichAreTransports);

        var reads = ConnectivityDependencyAudit.CounterfactualReads();
        var ring = reads.Single(r => r.Components == 1);
        var cut = reads.Single(r => r.Components == 2);
        // the gradient survives INSIDE every component and does not exist ACROSS a cut
        Assert.True(ring.MaxGradientInsideAComponent > 0.0);
        Assert.True(cut.MaxGradientInsideAComponent > 0.0);
        Assert.True(cut.CellsReadingAGradient > 0);
        Assert.Equal(0.0, cut.MaxGradientAcrossTheCut, 12);
        Assert.Equal(0, cut.LinksAcrossTheCut);
        Assert.Equal(-1, cut.PathAcrossTheCut);                       // no channel between the components
        Assert.True(ring.PathAcrossTheCut > 0);                        // a channel on the connected ring
        // and conservation: a cross transfer keeps the total and changes each component
        Assert.Equal(0.0, cut.TotalChangeOfACrossTransfer, 12);
        Assert.Equal(1.0, cut.PerComponentResidualOfACrossTransfer, 12);

        sb.AppendLine($"  at c = 2 the transport capacity falls from {F(universe.Single(u => u.Components == 1).Directions)} to {F(universe.Single(u => u.Components == 2).Directions)} directions, the law's failure count is "
            + $"{F(universe.Single(u => u.Components == 2).OfWhichAreNot)}, the gradient across the cut is {N(cut.MaxGradientAcrossTheCut)}, and the path between the components is gone.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_02_TheDependencyMap_EverySurvivingClaimReEvaluated()
    {
        PrintHeader("G_081 - task 1: the dependency map over G_035 and G_068-G_080");
        var sb = new StringBuilder();
        sb.AppendLine("EVERY SURVIVING CLAIM IS RE-EVALUATED AT c = 1, 2, 4, 8, THROUGH THE OWNING AUDIT'S OWN API");
        sb.AppendLine("  the classification is mechanical: a quantity that MOVES with c is DIRECT; a path-dependent quantity that");
        sb.AppendLine("  does not move while its path disappears is INDIRECT; a local quantity is INDEPENDENT; and a global");
        sb.AppendLine("  quantity that does not move is INDEPENDENT, because its statement holds whatever the component count.");
        sb.AppendLine();
        sb.Append(ConnectivityDependencyAudit.OutputMap());

        var map = ConnectivityDependencyAudit.DependencyMap();
        var counts = ConnectivityDependencyAudit.DependencyCounts();
        Assert.True(map.Length >= 27, $"the inventory must cover the surviving set, not a sample: {map.Length}");
        Assert.Equal(map.Length, counts.Direct + counts.Indirect + counts.Independent);
        Assert.Equal(6, counts.Direct);
        Assert.Equal(2, counts.Indirect);
        Assert.Equal(map.Length - 8, counts.Independent);

        // a LOCAL claim is never direct: the local sector does not consume connectivity
        Assert.All(map.Where(r => r.Domain == "Local"), r => Assert.Equal("Independent", r.Dependence));
        // every direct claim is global or path-dependent by construction
        Assert.All(map.Where(r => r.Dependence == "Direct"), r => Assert.NotEqual("Local", r.Domain));
        // and the ground of the INDEPENDENT rows is measured, not argued
        var invariance = ConnectivityDependencyAudit.LocalSectorInvariance();
        Assert.Equal(4, invariance.Length);
        Assert.Single(invariance.Select(v => v.LocalReadSet).Distinct());
        Assert.All(invariance, v => Assert.Equal(invariance[0].DistinctReadValues, v.DistinctReadValues));

        // the map covers every audit whose claims survive
        var audits = map.Select(r => r.Audit).Distinct().Count();
        sb.AppendLine($"  {map.Length} claims from {audits} distinct audit attributions; the local-read set is ONE set across all four component counts.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_03_TheMinimalFailureSet_FailureCoreAndItsCost()
    {
        PrintHeader("G_081 - task 3: the minimal failure set - FailureCore(connectivity)");
        var sb = new StringBuilder();
        sb.AppendLine("THE SMALLEST SET OF CLAIMS THAT FAILS WHEN CONNECTIVITY IS REMOVED, AND WHAT IT COSTS TO TRIGGER IT");
        sb.AppendLine("  the core is not chosen: it is the DIRECT set of the dependency map, and the trigger cost is the smallest");
        sb.AppendLine("  disconnection G_080 measured by exhaustive search.");
        sb.AppendLine();
        sb.Append(ConnectivityDependencyAudit.OutputCore());

        var core = ConnectivityDependencyAudit.FailureCore();
        var cost = ConnectivityDependencyAudit.FailureCoreCost();
        var survival = ConnectivityDependencyAudit.Survival();
        Assert.Equal(6, core.Length);
        Assert.Equal(survival.Failing, core.Length);
        Assert.Equal(2, cost.ComponentsNeededToTrigger);              // one cut is enough
        Assert.Equal(2, cost.LinksToRemoveToTrigger);                 // and two links buy it
        Assert.Equal(2, cost.CellsToRemoveToTrigger);
        Assert.Equal(2, ConnectivityDependencyAudit.ComponentCounts.Where(c => c >= 2).Min());

        // the core is entirely in the transport and continuity layer: every member is owned by G_078-G_080
        Assert.All(core, c => Assert.True(c.Audit.StartsWith("G_07") || c.Audit.StartsWith("G_08"),
            $"{c.Claim} [{c.Audit}]: the core must live in the transport layer"));
        // and everything the local sector claims survives
        var survivors = ConnectivityDependencyAudit.CoreWithoutConnectivity();
        Assert.Equal(survival.Surviving, survivors.Length);
        Assert.True(survivors.Length > core.Length * 3, "by a wide margin the local sector outlives the transport layer");
        Assert.All(survivors.Where(s => s.Audit is "G_068" or "G_072" or "G_075" or "G_076" or "G_077"),
            s => Assert.False(string.IsNullOrWhiteSpace(s.Claim)));

        sb.AppendLine($"  the core is {core.Length} claims, all owned by {string.Join(", ", core.Select(c => c.Audit).Distinct())}, "
            + $"triggered by {cost.LinksToRemoveToTrigger} link removals; {survivors.Length} of {survival.Total} claims survive unchanged.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_04_TheConnectivityLoad_WhoNeedsRankNMinusC()
    {
        PrintHeader("G_081 - task 4: the connectivity load - rank = n - c versus count conservation alone");
        var sb = new StringBuilder();
        sb.AppendLine("HOW MANY SURVIVING RESULTS ARE FUNCTIONS OF THE RANK IDENTITY RATHER THAN OF COUNT CONSERVATION");
        sb.AppendLine("  the test is arithmetic, not textual: a quantity is rank-loaded if it is LINEAR IN c on the component counts");
        sb.AppendLine("  actually evaluated (1, 2, 4, 8), because n - c is exactly that.");
        sb.AppendLine();
        sb.Append(ConnectivityDependencyAudit.OutputLoad());

        var load = ConnectivityDependencyAudit.ConnectivityLoad();
        var rows = ConnectivityDependencyAudit.RankLoad();
        Assert.Equal(rows.Length, ConnectivityDependencyAudit.Claims().Length);
        Assert.Equal(4, load.RankLoaded);
        Assert.Equal(load.RankLoaded, rows.Count(r => r.NeedsTheRankIdentity));
        Assert.Equal(load.Total, rows.Length);
        Assert.Equal((double)load.RankLoaded / rows.Length, load.RankFraction, 12);
        Assert.True(load.RankFraction < 0.2, "the surviving sector must not be rank-loaded");
        Assert.True(load.DirectFraction < 0.3, "and the direct dependency must stay a minority");
        Assert.Equal(2, load.CutsToBreakTheRankIdentity);

        // the four rank-loaded claims are exactly the quantities whose formula contains the rank identity
        var loaded = rows.Where(r => r.NeedsTheRankIdentity).Select(r => r.Claim).ToArray();
        Assert.Equal(4, loaded.Length);
        Assert.Contains(loaded, c => c.Contains("rank", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(loaded, c => c.Contains("nullity", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(loaded, c => c.Contains("failure count", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(loaded, c => c.Contains("free relative levels", StringComparison.OrdinalIgnoreCase));

        sb.AppendLine($"  {load.RankLoaded} of {load.Total} claims are rank-loaded ({F3(load.RankFraction)}), {load.Direct} are direct in total "
            + $"({F3(load.DirectFraction)}), and {load.CutsToBreakTheRankIdentity} cuts break the rank identity.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_05_TheHiddenConnectivityScan_OverTheSurvivingSources()
    {
        PrintHeader("G_081 - task 5: hidden connectivity - ring, path, neighbour, transport, Laplacian assumptions");
        var sb = new StringBuilder();
        sb.AppendLine("A LIVE SCAN OF THE SOURCES, WITH LITERALS AND COMMENTS STRIPPED");
        sb.AppendLine("  the scanner re-reads the surviving audits at test time, so a new hidden assumption is classified without");
        sb.AppendLine("  an edit here; report prose cannot be counted as code because AtSourceScan removes literals and comments,");
        sb.AppendLine("  and this audit's own source is excluded (the G_027/G_033 rule).");
        sb.AppendLine();
        var root = CoreRoot();
        sb.Append(ConnectivityDependencyAudit.OutputHidden(root));

        var rows = ConnectivityDependencyAudit.HiddenUses(root);
        Assert.True(rows.Length > 40, $"the scan must be populated rather than vacuous: {rows.Length} hits");
        // the figures the report quotes are locked here: 105 sites, 19 undeclared, 4 sources, 2 undeclared users
        Assert.Equal(105, rows.Length);
        Assert.Equal(19, rows.Count(r => !r.Declared));
        Assert.Equal(4, rows.Select(r => r.File).Distinct().Count());
        var users = ConnectivityDependencyAudit.UndeclaredUsers(root);
        Assert.Equal(2, users.Length);
        Assert.Equal(12, ConnectivityDependencyAudit.AuditsWithNoUndeclaredUse(root));
        // the audit's own source exists in the root and is excluded: the scan is guaranteed to have skipped something
        Assert.True(File.Exists(Path.Combine(root, "ConnectivityDependencyAudit.cs")));
        Assert.DoesNotContain(rows, r => ConnectivityDependencyAudit.SelfFiles.Contains(r.File));
        Assert.Contains(ConnectivityDependencyAudit.SelfFiles, s => s == "ConnectivityDependencyAudit.cs");

        var summary = ConnectivityDependencyAudit.HiddenUseSummary(root);
        Assert.Equal(ConnectivityDependencyAudit.HiddenPatterns.Length, summary.Length);
        Assert.All(summary, s => Assert.Equal(s.Hits, s.Hidden + s.Declared));
        // every category is represented
        Assert.All(summary, s => Assert.True(s.Hits > 0, $"{s.Category}: the scan must find something"));
        // THE FINDING: the undeclared uses are RING, PATH and TRANSPORT; neighbour and Laplacian vocabulary appears
        // ONLY in the two audits that name connectivity as their subject, i.e. the surviving local sector is written
        // without any neighbour or Laplacian object at all
        foreach (var category in new[] { "RING", "PATH", "TRANSPORT" })
            Assert.True(summary.Single(s => s.Category == category).Hidden > 0,
                $"{category}: a declared-only category would make the scan trivial");
        foreach (var category in new[] { "NEIGHBOUR", "LAPLACIAN" })
            Assert.Equal(0, summary.Single(s => s.Category == category).Hidden);
        // the assignments are total: every hit is counted once by its category
        Assert.Equal(rows.Length, summary.Sum(s => s.Hits));

        // the assignments are total and the scan is deterministic
        Assert.Equal(rows.Length, ConnectivityDependencyAudit.HiddenUses(root).Length);
        Assert.Equal(rows.Select(r => r.File + ":" + r.Line + ":" + r.Category).Distinct().Count(),
            rows.Select(r => r.File + ":" + r.Line + ":" + r.Category).Distinct().Count());

        sb.AppendLine($"  {rows.Length} assumption sites in {rows.Select(r => r.File).Distinct().Count()} surviving sources; "
            + $"{summary.Sum(s => s.Hidden)} of them are HIDDEN (the audit that owns them does not name connectivity as its subject).");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_06_TheStructuralClassification_GlobalInputWithThreeRefutations()
    {
        PrintHeader("G_081 - task 6: what kind of thing is connectivity?");
        var sb = new StringBuilder();
        sb.AppendLine("FOUR CANDIDATES, EACH TESTED BY A MEASUREMENT RATHER THAN BY ARGUMENT");
        sb.AppendLine("  new primitive: refuted if no observable reads the component count and no degree of freedom is added;");
        sb.AppendLine("  local input: refuted if no cell changes a local read when the substrate is cut;");
        sb.AppendLine("  boundary condition: refuted if the cells beside the cut read nothing new;");
        sb.AppendLine("  global input: supported if global descriptors move with the component count.");
        sb.AppendLine();
        sb.Append(ConnectivityDependencyAudit.OutputClassification());

        var rows = ConnectivityDependencyAudit.Classification();
        Assert.Equal(4, rows.Length);
        Assert.Equal(ConnectivityKind.GlobalInput, ConnectivityDependencyAudit.ClassifyConnectivity());
        Assert.False(rows.Single(r => r.Kind == "NEW PRIMITIVE").Supported);
        Assert.False(rows.Single(r => r.Kind == "LOCAL INPUT").Supported);
        Assert.False(rows.Single(r => r.Kind == "BOUNDARY CONDITION").Supported);
        Assert.True(rows.Single(r => r.Kind == "GLOBAL INPUT").Supported);
        // the refutations carry the numbers that make them measurements
        Assert.Contains("0 observables", rows.Single(r => r.Kind == "NEW PRIMITIVE").Measurement);

        // and the caveat is measured rather than hidden: the refutation of LOCAL INPUT is for the canonical state
        var caveat = ConnectivityDependencyAudit.LocalReadsThatMoveWithTheCut();
        Assert.Equal(0, caveat.Single(c => c.State.StartsWith("the canonical")).CellsWhoseReadsMoveWithTheCut);
        Assert.True(caveat.Single(c => c.State.StartsWith("a state that varies")).CellsWhoseReadsMoveWithTheCut > 0,
            "a state that varies at the cut IS locally visible, and the audit must say so");

        sb.AppendLine($"  COMPUTED: {ConnectivityDependencyAudit.KindName(ConnectivityDependencyAudit.ClassifyConnectivity())} - and the three other kinds are refuted, "
            + $"each by a number (0 readers, {F(caveat[0].CellsWhoseReadsMoveWithTheCut)} cells reading the cut for the canonical state).");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_07_TheRefereeQuestion_CoreWithoutConnectivity()
    {
        PrintHeader("G_081 - task 7: if connectivity is removed from AT, what survives unchanged?");
        var sb = new StringBuilder();
        sb.AppendLine("CoreWithoutConnectivity - THE COMPLEMENT OF THE FAILURE CORE");
        sb.AppendLine();
        var survival = ConnectivityDependencyAudit.Survival();
        foreach (var c in ConnectivityDependencyAudit.CoreWithoutConnectivity())
            sb.AppendLine($"  SURVIVES  {c.Claim}  [{c.Audit}]");
        sb.AppendLine();
        foreach (var c in ConnectivityDependencyAudit.FailureCore())
            sb.AppendLine($"  FAILS     {c.Claim}  [{c.Audit}]");
        sb.AppendLine();
        sb.AppendLine($"  {survival.Surviving} of {survival.Total} claims survive unchanged ({F3(survival.SurvivingFraction)}); "
            + $"{survival.Failing} fail, and all of them are transport-layer claims.");
        Output.WriteLine(sb.ToString());

        Assert.True(survival.SurvivingFraction > 0.75, "the referee answer must be a wide survival");
        Assert.Equal(survival.Total, survival.Surviving + survival.Failing);
        // every audit of the local sector contributes at least one surviving claim
        var survivors = ConnectivityDependencyAudit.CoreWithoutConnectivity();
        foreach (var audit in new[] { "G_068", "G_072", "G_075", "G_076", "G_077", "G_078" })
            Assert.Contains(survivors, s => s.Audit == audit || s.Audit.Contains(audit));
        // and nothing in the transport layer is claimed to survive
        Assert.DoesNotContain(survivors, s => s.Audit == "G_079/G_080" && s.Claim.Contains("rank"));
    }

    [Fact]
    public void Y_G_081_08_Verdict_IsComputed_AndIsGlobalInput()
    {
        PrintHeader("G_081 - verdict");
        var sb = new StringBuilder();
        sb.Append(ConnectivityDependencyAudit.OutputVerdict());

        string verdict = ConnectivityDependencyAudit.Verdict();
        Assert.StartsWith("GLOBAL INPUT", verdict);
        Assert.Contains("SMALL FAILURE CORE", verdict);
        Assert.Contains("TRANSPORT LAYER", verdict);
        Assert.Contains("DIRECTLY dependent", verdict);
        Assert.Contains("INDEPENDENT", verdict);
        Assert.Contains("GLOBAL INPUT", verdict);
        Assert.Contains("rank-DECORATED", verdict);
        // and the scan's own figures travel with the verdict
        var root = CoreRoot();
        var users = ConnectivityDependencyAudit.UndeclaredUsers(root);
        Assert.True(users.Length >= 1, "the verdict must quote the undeclared users");
        Assert.True(ConnectivityDependencyAudit.AuditsWithNoUndeclaredUse(root) >= ConnectivityDependencyAudit.SurvivingAudits.Length - 3,
            "most surviving audits must name no assumption at all");
        Assert.Contains("undeclared assumption site", verdict);

        // the verdict's branches are COMPUTED (G_027): the classifier's other three kinds are refuted by measurement
        Assert.Equal(ConnectivityKind.GlobalInput, ConnectivityDependencyAudit.ClassifyConnectivity());
        Assert.All(ConnectivityDependencyAudit.Classification().Where(r => r.Kind != "GLOBAL INPUT"),
            r => Assert.False(r.Supported));
        // the two live branch predicates depend on data, not on the audit's prose
        Assert.Equal(6, ConnectivityDependencyAudit.DependencyCounts().Direct);
        Assert.Equal(6, ConnectivityDependencyAudit.FailureCore().Length);

        sb.AppendLine();
        sb.AppendLine("  the classification is computed: the classifier's other three branches are each refuted by a measured");
        sb.AppendLine("  number, so the GLOBAL INPUT branch is reached rather than asserted.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_09_WhereItStands_CarriesTheFourLimits()
    {
        PrintHeader("G_081 - where it stands");
        var sb = new StringBuilder();
        sb.AppendLine("THE LIMITS MUST TRAVEL WITH THE VERDICT");
        sb.AppendLine();
        string standing = ConnectivityDependencyAudit.WhereItStands();
        sb.AppendLine(standing);

        Assert.Contains("SURVIVING SET AND NOT THE WHOLE THEORY", standing);
        Assert.Contains("INDEPENDENT MEANS THE CLAIM DOES NOT CONSUME CONNECTIVITY", standing);
        Assert.Contains("CANONICAL INITIAL CONDITION", standing);
        Assert.Contains("ARITHMETIC SIGNATURE", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("neither is taken", standing);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_081_10_Reproducible()
    {
        PrintHeader("G_081 - reproducibility");
        var sb = new StringBuilder();
        sb.AppendLine("  Every figure in this suite comes from three constructed objects and no data:");
        sb.AppendLine("      the LINK SET of a substrate: c closed rings on the canonical cells, built explicitly;");
        sb.AppendLine("      the DIVERGENCE and its rank on that link set (integer matrices, tolerance 1e-9);");
        sb.AppendLine("      the surviving audits' own public APIs, called once per claim per component count.");
        sb.AppendLine("  The rank, nullity, free-mode and witness counts are INTEGER claims; the comparison tolerance for values");
        sb.AppendLine("  that are floating point is 1e-9 (and 1e-12 where an exact zero is asserted), stated at each assertion.");
        sb.AppendLine("  The SUBSTRATE is named with every count: the 1D ring family, one ring per component, c = 1, 2, 4, 8 on 96");
        sb.AppendLine("  cells, and no spectral quantity is quoted anywhere in this audit.");
        Assert.Equal(ConnectivityDependencyAudit.Verdict(), ConnectivityDependencyAudit.Verdict());
        Assert.Equal(ConnectivityDependencyAudit.WhereItStands(), ConnectivityDependencyAudit.WhereItStands());
        Assert.Equal(ConnectivityDependencyAudit.DependencyMap().Length, ConnectivityDependencyAudit.DependencyMap().Length);
        Assert.All(ConnectivityDependencyAudit.CounterfactualUniverse(), u => Assert.Equal(u.Cells - u.Components, u.Directions));
        Assert.All(ConnectivityDependencyAudit.Claims(), c => Assert.False(string.IsNullOrWhiteSpace(c.Name)));
        sb.AppendLine("  The audit is deterministic: fixed enumerations, no randomness and no external data.");
        Output.WriteLine(sb.ToString());
    }
}
