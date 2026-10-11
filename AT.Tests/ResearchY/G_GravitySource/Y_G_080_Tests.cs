using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_080 - CONNECTIVITY ORIGIN AUDIT.
///
/// G_079 derived the continuity law on the surviving update class and proved that its equivalence with count
/// conservation rests on ONE premise the law cannot supply for itself: the connectivity of the substrate. This suite
/// asks the origin question. Is that connectivity DERIVED from the theory's own primitives, EMERGENT from the update
/// relation, or ASSUMED as an input? The audit is allowed to use Difference, Actualization, the generation relation
/// and count conservation, and it may not import a graph-connectivity axiom or a topology.
///
/// The report follows the five questions of the assignment. Every number quoted is computed here, and the
/// SUBSTRATE is named wherever a count is quoted: the lattice is the 1D ring family, one ring per component, from
/// 2 cells up to the canonical 96 — no spectral quantity is quoted anywhere in this suite, so the counts are graph
/// statements about a link set and hold for any decomposition of the ring into c parts.
/// </summary>
public sealed class Y_G_080_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_080_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string F(double v) => v.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_080_01_DisconnectedUniversesExist_AndTheSmallestOneIsFrozen()
    {
        PrintHeader("G_080 - Q1: can disconnected universes exist?");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the substrate is a link set BUILT here: one ring per component, `components` rings of equal size;");
        sb.AppendLine("  - the count transport is the surviving class of G_078/G_079: a change is a transport iff it lies in the");
        sb.AppendLine("    image of the discrete divergence, which requires EACH component's sum to vanish;");
        sb.AppendLine("  - the state asked about is the per-component-uniform occupation, because the canonical initial");
        sb.AppendLine("    condition of the theory is the uniform occupation, so this state is not a contrivance;");
        sb.AppendLine("  - connectivity is never assumed: the component count of every link set is COMPUTED by union-find.");
        sb.AppendLine();
        sb.Append(ConnectivityOriginAudit.OutputCapacity());
        sb.AppendLine();
        sb.Append(ConnectivityOriginAudit.OutputSurvivors());

        // the transport capacity is n - c: each cut costs exactly one direction, measured over the whole family
        var capacity = ConnectivityOriginAudit.TransportCapacity();
        Assert.All(capacity, c => Assert.Equal(c.Cells - c.Components, c.Directions));
        Assert.All(capacity, c => Assert.Equal(c.Components - 1, c.DirectionsLostToTheCuts));

        // a disconnected universe is a solution: an exact fixed point, count-conserving, with a non-zero gap
        var survivors = ConnectivityOriginAudit.DisconnectedSurvivors();
        Assert.All(survivors, s => Assert.Equal(0.0, s.MaxUpdateOfTheComponentUniformState, 12));
        var split = survivors.Single(s => s.Substrate == "two 4-cell rings");
        Assert.True(split.ConservingWitnesses > split.OfWhichAreTransports, "the cut must forbid a change");
        Assert.Equal(split.Components - 1, split.OfWhichAreForbiddenTransfers);
        Assert.True(split.PotentialGapBetweenTheComponents > 0.0, "a survivor with a gap is not a trivial state");

        // ...and the smallest one is FROZEN: rank 0, no non-zero transport at all, one forbidden change
        var frozen = survivors.Single(s => s.Substrate == "the smallest split pair");
        Assert.Equal(2, frozen.Cells);
        Assert.Equal(2, frozen.Components);
        Assert.Equal(0, ConnectivityOriginAudit.TransportDirections(frozen.Cells, frozen.Components));
        Assert.Equal(0, frozen.OfWhichAreTransports);
        Assert.Equal(1, frozen.OfWhichAreForbiddenTransfers);
        Assert.Equal(1, frozen.ConservingWitnesses);
        Assert.Equal(0.0, frozen.MaxUpdateOfTheComponentUniformState, 12);
        Assert.True(frozen.PotentialGapBetweenTheComponents > 0.0);

        // the LIVE CONTROL: the same two cells connected admit one transport direction, so 0 is a measurement
        Assert.Equal(1, ConnectivityOriginAudit.TransportDirections(2, 1));

        sb.AppendLine();
        sb.AppendLine($"  the smallest disconnected survivor is {frozen.Cells} cells in {frozen.Components} components:");
        sb.AppendLine($"  rank {ConnectivityOriginAudit.TransportDirections(2, 2)}, {frozen.OfWhichAreTransports} transports and {frozen.OfWhichAreForbiddenTransfers} forbidden transfer -");
        sb.AppendLine("  frozen, and the same two cells CONNECTED admit 1 direction, so the zero is measured rather than defined.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_02_TheFourAllowedPrimitivesAreBlindToTheCut()
    {
        PrintHeader("G_080 - Q1 continued: can the allowed primitives see connectivity at all?");
        var sb = new StringBuilder();
        sb.AppendLine("THE DISJOINT-UNION TEST, WHICH IS THE AUDIT'S CENTRAL REFUTATION");
        sb.AppendLine("  take a substrate and adjoin a DISJOINT COPY of it: nothing about the theory changes except that the");
        sb.AppendLine("  universe now has two components. If a primitive could yield connectivity, its certificate would have to");
        sb.AppendLine("  change. Each certificate is the SET of realised values: a primitive states WHICH differences, ticks,");
        sb.AppendLine("  generators and component sums exist, not how many copies of the substrate they occur in.");
        sb.AppendLine();
        sb.Append(ConnectivityOriginAudit.OutputBlindness());

        var closure = ConnectivityOriginAudit.DisjointUnionClosure();
        Assert.Equal(4, closure.Length);
        Assert.All(closure, c => Assert.False(c.ContentChanged, $"{c.Primitive}: the content must not change"));

        // ...while every GLOBAL descriptor does change - so the audit is not blind, the allowed vocabulary is
        var descriptors = ConnectivityOriginAudit.GlobalDescriptors();
        Assert.Equal(4, descriptors.Length);
        Assert.All(descriptors, d => Assert.NotEqual(d.OnTheSubstrate, d.OnTheDisjointUnion));
        Assert.Equal(1, descriptors.Single(d => d.Descriptor.StartsWith("connected components")).OnTheSubstrate);
        Assert.Equal(2, descriptors.Single(d => d.Descriptor.StartsWith("connected components")).OnTheDisjointUnion);

        // the audit's OWN detector is live on the cut: rank = n - c, so the cut is measurable by the audit
        Assert.Equal(7, ConnectivityOriginAudit.TransportDirections(8, 1));
        Assert.Equal(6, ConnectivityOriginAudit.TransportDirections(8, 2));
        Assert.Equal(4, ConnectivityOriginAudit.TransportDirections(8, 4));

        // and the reach of each primitive is the audit's explanation: three are local, the fourth is a sum
        var certificates = ConnectivityOriginAudit.PrimitiveCertificates(ConnectivityOriginAudit.WorkhorseRho, 8, 1);
        Assert.Equal(3, certificates.Count(c => c.Reach.StartsWith("LOCAL", StringComparison.Ordinal)));
        Assert.Equal(1, certificates.Count(c => c.Reach.Contains("SUM", StringComparison.Ordinal)));

        sb.AppendLine($"  ZERO of {closure.Length} allowed primitives detect the union, while {descriptors.Count(d => d.OnTheSubstrate != d.OnTheDisjointUnion)} of {descriptors.Length} global descriptors do");
        sb.AppendLine("  - and three of the four primitives are LOCAL (radius one link) while the fourth is a SUM.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_03_ConnectivityIsConservedByTheUpdateRelation_NotProduced()
    {
        PrintHeader("G_080 - Q2: is connectivity generated automatically by the update relation?");
        var sb = new StringBuilder();
        sb.AppendLine("THE CLASS IS WRITTEN ON THE LINK SET, SO IT IS BLOCK DIAGONAL ACROSS A CUT");
        sb.AppendLine("  an update that merely moves count cannot create the link it would need to cross. The audit measures the");
        sb.AppendLine("  weight the transport Laplacian carries between different components, and it checks the criterion against");
        sb.AppendLine("  a LIVE CONTROL: the same partition of the index set in HALF, which no link set can influence, is joined");
        sb.AppendLine("  by positive weight on the connected ring. A zero on the cut substrate therefore means something.");
        sb.AppendLine();
        sb.Append(ConnectivityOriginAudit.OutputUpdate());

        var independence = ConnectivityOriginAudit.ComponentIndependence();
        var cutRows = independence.Where(r => r.Components >= 2).ToArray();
        Assert.All(cutRows, r =>
        {
            Assert.Equal(0, r.EntriesJoiningDifferentComponents);
            Assert.Equal(0.0, r.WeightFromOneComponentToTheOthers, 12);
        });
        // the live control: on the connected ring the two halves ARE joined, on the cut they are not
        Assert.Equal(4, independence.Single(r => r.Substrate == "the 8-cell ring").EntriesJoiningTheTwoHalves);
        Assert.Equal(0, independence.Single(r => r.Substrate == "two 4-cell rings").EntriesJoiningTheTwoHalves);
        Assert.Equal(4, independence.Single(r => r.Substrate == "the canonical ring").EntriesJoiningTheTwoHalves);
        Assert.Equal(0, independence.Single(r => r.Substrate == "the canonical ring split in two").EntriesJoiningTheTwoHalves);

        // the influence across a pair that is a LINK on one substrate and a CUT on the other
        var influence = ConnectivityOriginAudit.CrossComponentInfluence();
        Assert.Equal(1.0, influence.Single(i => i.Substrate.StartsWith("the 8-cell ring")).InfluenceAcrossTheCut, 12);
        Assert.Equal(0.0, influence.Single(i => i.Substrate.StartsWith("two 4-cell rings")).InfluenceAcrossTheCut, 12);
        Assert.Equal(1.0, influence.Single(i => i.Substrate.StartsWith("the canonical ring (")).InfluenceAcrossTheCut, 12);
        Assert.Equal(0.0, influence.Single(i => i.Substrate.StartsWith("the canonical ring split")).InfluenceAcrossTheCut, 12);

        // generation-connectivity is NOT transport-connectivity: one root can generate both components
        var generation = ConnectivityOriginAudit.GenerationVersusTransport();
        var oneRoot = generation.Single(g => g.Case.StartsWith("one root generating"));
        Assert.Equal(1, oneRoot.ComponentsOfTheGenerationRelation);
        Assert.Equal(2, oneRoot.ComponentsOfTheTransportSubstrate);
        Assert.Equal(2, generation.Single(g => g.Case.StartsWith("one root per component")).ComponentsOfTheGenerationRelation);
        Assert.True(generation.Single(g => g.Case.StartsWith("generation along")).ComponentsOfTheGenerationRelation
            > generation.Single(g => g.Case.StartsWith("generation along")).ComponentsOfTheTransportSubstrate);

        Assert.False(ConnectivityOriginAudit.UpdateProducesConnectivity(),
            "no operator of the surviving class may move count across a cut");
        Assert.False(ConnectivityOriginAudit.PrimitivesSeparateConnectivity(),
            "no allowed primitive may separate connected from disconnected");

        sb.AppendLine("  SO: the update relation CONSERVES the component count instead of generating it, and the generation");
        sb.AppendLine("  relation can be connected while the substrate is not - so it cannot supply the law's missing premise");
        sb.AppendLine("  either. A conserved integer that no dynamics sets is an INITIAL DATUM.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_04_TheGenerativeRuleAssumesTheRingInsteadOfBuildingIt()
    {
        PrintHeader("G_080 - Q2 continued: does the actualization rule itself generate connectivity?");
        var sb = new StringBuilder();
        sb.AppendLine("THE RULE IS ARITHMETIC ON THE WHOLE INDEX SET: `(i + d) mod n`");
        sb.AppendLine("  the repository's actualization link rule grows links from each active cell to its next `k` RING");
        sb.AppendLine("  neighbours. It never tests whether a link exists — it computes one. Applied to a genuinely");
        sb.AppendLine("  disconnected substrate it therefore does not fail: it manufactures links ACROSS the cut and returns a");
        sb.AppendLine("  link set with ONE component where the substrate has c. Connectivity is in the RULE'S ARITHMETIC.");
        sb.AppendLine();
        sb.Append(ConnectivityOriginAudit.OutputUpdate());

        var rule = ConnectivityOriginAudit.RingRuleCannotRepresentACut();
        Assert.All(rule, r => Assert.Equal(1, r.ComponentsOfTheRuleOutput));
        Assert.All(rule.Where(r => r.Components == 1), r => Assert.Equal(0, r.LinksCrossingTheCut));
        Assert.All(rule.Where(r => r.Components >= 2), r => Assert.True(r.LinksCrossingTheCut > 0,
            $"{r.Substrate}: the rule must create a link across the cut"));

        // the smallest case is the sharpest: the rule manufactures the very link whose absence froze the pair
        var smallest = rule.Single(r => r.Substrate == "the smallest split pair");
        Assert.Equal(1, smallest.LinksCrossingTheCut);
        Assert.Equal(2, smallest.Components);
        Assert.Equal(1, smallest.ComponentsOfTheRuleOutput);

        sb.AppendLine();
        sb.AppendLine($"  the sharpest case: on the smallest split pair ({smallest.Cells} cells, {smallest.Components} components) the rule manufactures");
        sb.AppendLine($"  {smallest.LinksCrossingTheCut} link across the cut and returns {smallest.ComponentsOfTheRuleOutput} component - the rule cannot represent the very substrate");
        sb.AppendLine("  it is supposed to build on.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_05_TheSmallestCutOfTheCanonicalSubstrateIsTwo()
    {
        PrintHeader("G_080 - Q3: how small is a disconnected survivor, and how small is a cut?");
        var sb = new StringBuilder();
        sb.AppendLine("TWO EXHAUSTIVE SEARCHES, ONE AT EACH END OF THE QUESTION");
        sb.AppendLine("  the survivor end: the FEWEST cells a disconnected substrate can have. The cut end: the FEWEST removals");
        sb.AppendLine("  that disconnect the canonical substrate. Both are searched exhaustively at size one and size two, so a");
        sb.AppendLine("  single removal is verified NOT to suffice and the answer 2 is a measurement rather than a definition.");
        sb.AppendLine();
        sb.Append(ConnectivityOriginAudit.OutputCut());

        var cuts = ConnectivityOriginAudit.SmallestCut(new[] { 2, 3, 4, 8, 96 });
        Assert.Equal(1, cuts.Single(c => c.Cells == 2).MinLinksToOneDisconnect);
        Assert.All(cuts.Where(c => c.Cells >= 3 && c.Cells <= 96),
            c => Assert.Equal(2, c.MinLinksToOneDisconnect));
        Assert.All(cuts.Where(c => c.Cells >= 4),
            c => Assert.Equal(2, c.MinCellsToOneDisconnect));
        Assert.All(cuts.Where(c => c.Cells >= 3), c => Assert.NotEqual(1, c.MinCellsToOneDisconnect));

        // the two sides of the question, as one statement: the smallest survivor is a frozen pair, and the
        // canonical substrate cannot be disconnected by any single defect
        var frozen = ConnectivityOriginAudit.DisconnectedSurvivors().Single(s => s.Substrate == "the smallest split pair");
        var canonical = cuts.Single(c => c.Cells == ConnectivityOriginAudit.CanonicalCells);
        Assert.Equal(2, frozen.Cells);
        Assert.Equal(0, ConnectivityOriginAudit.TransportDirections(frozen.Cells, frozen.Components));
        Assert.Equal(2, canonical.MinLinksToOneDisconnect);
        Assert.Equal(2, canonical.MinCellsToOneDisconnect);

        // the pair that IS joined: 2 cells connected admit 1 direction where the split pair admits 0
        Assert.Equal(1, ConnectivityOriginAudit.TransportDirections(2, 1));
        Assert.Equal(0, ConnectivityOriginAudit.TransportDirections(2, 2));

        sb.AppendLine($"  the smallest survivor is {frozen.Cells} cells (rank {ConnectivityOriginAudit.TransportDirections(2, 2)}, one forbidden transfer, frozen), and the canonical");
        sb.AppendLine($"  substrate needs {canonical.MinLinksToOneDisconnect} links or {canonical.MinCellsToOneDisconnect} cells removed to be disconnected - so a SINGLE defect never cuts it.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_06_TheSignatureOfADisconnectedSectorIsAFrozenUnreadableOffset()
    {
        PrintHeader("G_080 - Q4: what would a disconnected sector look like from inside?");
        var sb = new StringBuilder();
        sb.AppendLine("A READ THAT NEEDS A LINK IS A READ A CUT SILENCES");
        sb.AppendLine("  the sector's reads are the clock rate, the potential, the acceleration and the redshift. All are");
        sb.AppendLine("  functions of the local occupation; the acceleration is a GRADIENT ALONG THE LINKS, and the redshift is a");
        sb.AppendLine("  RATIO of two rates. The audit measures which of them survives a cut, and then measures whether the one");
        sb.AppendLine("  that is written across the cut can be reached at all.");
        sb.AppendLine();
        sb.Append(ConnectivityOriginAudit.OutputObservables());

        var reach = ConnectivityOriginAudit.AccelerationReach();
        var frozen = reach.Single(a => a.Substrate == "the smallest split pair");
        Assert.Equal(0.0, frozen.MaxAccelerationOverTheSweep, 12);
        Assert.Equal(0, frozen.CellsReadingAnAcceleration);
        // the live control: the same two cells connected DO read an acceleration
        Assert.True(reach.Single(a => a.Substrate == "the smallest ring").MaxAccelerationOverTheSweep > 0.0);
        Assert.True(reach.Single(a => a.Substrate == "the canonical ring").CellsReadingAnAcceleration > 0);

        // the free level is WRITTEN into the redshift - it moves with it ...
        var redshift = ConnectivityOriginAudit.RedshiftAcrossTheCut();
        Assert.True(redshift.Max(r => r.RedshiftBetweenTheComponents) - redshift.Min(r => r.RedshiftBetweenTheComponents) > 0.5,
            "the redshift between components must MOVE with the free level");
        Assert.NotEqual(redshift.Single(r => r.Level1 == 4 && r.Level2 == 4).RedshiftBetweenTheComponents,
            redshift.Single(r => r.Level1 == 4 && r.Level2 == 9).RedshiftBetweenTheComponents, 6);
        // ... and it is UNREACHABLE: no channel joins the components
        Assert.Equal(0, ConnectivityOriginAudit.LinksAcrossTheCut(8, 2));
        Assert.Equal(-1, ConnectivityOriginAudit.PathLength(8, ConnectivityOriginAudit.LinksOf(8, 2), 3, 4));
        Assert.Equal(1, ConnectivityOriginAudit.PathLength(8, ConnectivityOriginAudit.LinksOf(8, 1), 3, 4));
        Assert.Equal(-1, ConnectivityOriginAudit.PathLength(2, ConnectivityOriginAudit.LinksOf(2, 2), 0, 1));

        // and the flux, the one quantity that WOULD carry a cross-cut comparison, is read by nothing at all
        Assert.Equal(0, ConnectivityOriginAudit.ObservablesReadingAFlux());
        Assert.True(TemporalIndependenceAudit.TemporalObservables().Length >= 6);

        sb.AppendLine($"  SO: the free level is DEFINED in an observable ({F3(redshift.Single(r => r.Level1 == 4 && r.Level2 == 9).RedshiftBetweenTheComponents)} redshift between");
        sb.AppendLine($"  components 4 and 9) and UNREACHABLE ({ConnectivityOriginAudit.LinksAcrossTheCut(8, 2)} links across the cut, path length {ConnectivityOriginAudit.PathLength(8, ConnectivityOriginAudit.LinksOf(8, 2), 3, 4)}), and the");
        sb.AppendLine($"  smallest survivor reads an acceleration of exactly {N(frozen.MaxAccelerationOverTheSweep)}. The signature is a frozen,");
        sb.AppendLine("  unreadable offset - not invisibility, UNREACHABILITY.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_07_TheBlindnessIsSwept_AndNothingIsAxiomatised()
    {
        PrintHeader("G_080 - robustness: the blindness is swept, and no connectivity axiom is imported");
        var sb = new StringBuilder();
        sb.AppendLine("THE CONCLUSION MUST NOT DEPEND ON THE ONE INSTANCE THE REPORT PRINTS");
        sb.AppendLine("  the disjoint-union test is run over a SWEEP of substrates and states, and the block-diagonal test over the");
        sb.AppendLine("  whole substrate family; and the audit checks that it reached its verdict WITHOUT importing a");
        sb.AppendLine("  graph-connectivity axiom: every component count it quotes is computed from an explicit link set.");
        sb.AppendLine();

        var states = new[]
        {
            new[] { 4.0, 7, 4, 8, 9, 4, 5, 6 },
            new[] { 1.0, 1, 1, 1 },
            new[] { 2.0, 5, 2, 5 },
            new[] { 3.0, 3, 7, 3, 3, 7 },
        };
        int instances = 0, changes = 0;
        foreach (var rho in states)
            foreach (int components in new[] { 1, 2 })
            {
                if (rho.Length % components != 0) continue;
                var closure = ConnectivityOriginAudit.DisjointUnionClosure(rho, components);
                instances++;
                changes += closure.Count(c => c.ContentChanged);
                Assert.All(closure, c => Assert.False(c.ContentChanged,
                    $"{c.Primitive} changed its content for {rho.Length} cells in {components} components"));
            }
        Assert.True(instances >= 6, $"the sweep must be populated, not vacuous: {instances}");

        // the block-diagonal property over the WHOLE family: measured, and zero with no exceptions
        int cutSubstrates = 0, nonZeroCrossEntries = 0, forbidden = 0;
        foreach (var s in ConnectivityOriginAudit.Substrates().Where(s => s.Components >= 2))
        {
            cutSubstrates++;
            nonZeroCrossEntries += ConnectivityOriginAudit.EntriesJoiningDifferentComponents(s.Cells, s.Components);
            forbidden += ConnectivityOriginAudit.DisconnectedSurvivors()
                .Single(d => d.Substrate == s.Substrate).OfWhichAreForbiddenTransfers;
        }
        Assert.True(cutSubstrates >= 6, "the family must be populated");
        Assert.Equal(0, nonZeroCrossEntries);
        Assert.True(forbidden > 0, "the cut must forbid something, or the audit is vacuous");

        // no axiom: the component count of every substrate is COMPUTED from its own link set, and the transport
        // operator is built from that same link set — so nothing here is assumed, and a defect in either would show
        Assert.All(ConnectivityOriginAudit.Substrates(), s =>
            Assert.Equal(s.Components, ConnectivityOriginAudit.ComponentsOf(s.Cells, ConnectivityOriginAudit.LinksOf(s.Cells, s.Components))));
        Assert.All(ConnectivityOriginAudit.Substrates(), s =>
        {
            var links = ConnectivityOriginAudit.LinksOf(s.Cells, s.Components);
            var l = ConnectivityOriginAudit.Laplacian(s.Cells, links);
            int offDiagonal = Enumerable.Range(0, s.Cells).Sum(i =>
                Enumerable.Range(0, s.Cells).Count(j => i != j && Math.Abs(l[i][j]) > 0));
            Assert.Equal(2 * links.Length, offDiagonal);
        });

        sb.AppendLine($"  swept: {instances} (substrate, state) instances, {changes} content changes across all four primitives.");
        sb.AppendLine($"  measured: {cutSubstrates} disconnected substrates, {nonZeroCrossEntries} Laplacian entries joining different components,");
        sb.AppendLine($"  {forbidden} forbidden transfers - and every component count recomputed from its own link set.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_08_Verdict_IsComputed_AndIsAssumed()
    {
        PrintHeader("G_080 - verdict");
        var sb = new StringBuilder();
        sb.Append(ConnectivityOriginAudit.OutputVerdict());

        string verdict = ConnectivityOriginAudit.Verdict();
        Assert.StartsWith("ASSUMED", verdict);
        Assert.Contains("NOT DERIVED", verdict);
        Assert.Contains("NOT EMERGENT", verdict);
        Assert.Contains("GLOBAL", verdict);
        Assert.Contains("FROZEN", verdict);
        Assert.Contains("UNREACHABLE", verdict);
        Assert.Contains("initial datum", verdict);

        // each of the two refuted branches must be COMPUTED (G_027): the criterion is a measurement, and both
        // alternatives have a live test that they fail
        Assert.False(ConnectivityOriginAudit.PrimitivesSeparateConnectivity());
        Assert.False(ConnectivityOriginAudit.UpdateProducesConnectivity());

        string where = ConnectivityOriginAudit.WhereItStands();
        Assert.Contains("WHAT IS NOT CLAIMED", where);
        Assert.Contains("LOCAL BLINDNESS IS NOT TOTAL BLINDNESS", where);
        Assert.Contains("GENERATION AND TRANSPORT ARE DIFFERENT RELATIONS", where);
        Assert.Contains("UNREACHABLE", where);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", where);

        sb.AppendLine();
        sb.AppendLine("  the classification is COMPUTED: PrimitivesSeparateConnectivity() and UpdateProducesConnectivity() are the");
        sb.AppendLine($"  two live branches (both false: {ConnectivityOriginAudit.PrimitivesSeparateConnectivity()} / {ConnectivityOriginAudit.UpdateProducesConnectivity()}), so the verdict is the third branch and");
        sb.AppendLine("  neither alternative was dismissed by prose.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_09_WhereItStands_CarriesTheFourHonestLimits()
    {
        PrintHeader("G_080 - where it stands");
        var sb = new StringBuilder();
        sb.AppendLine("THE LIMITS MUST TRAVEL WITH THE VERDICT, NOT LIVE IN PROSE ELSEWHERE");
        sb.AppendLine();
        string standing = ConnectivityOriginAudit.WhereItStands();
        sb.AppendLine(standing);

        // limit 1: the scope is the surviving class over the ring family, and no more
        Assert.Contains("SURVIVING CLASS AND THE RING FAMILY", standing);
        Assert.Contains("none of the four ALLOWED primitives", standing);
        // limit 2: local blindness is not total blindness - a state that varies at the cut IS locally visible
        Assert.Contains("LOCAL BLINDNESS IS NOT TOTAL BLINDNESS", standing);
        Assert.Contains("INITIAL condition", standing);
        // limit 3: the generation relation is not the transport relation, and the rule assumes the ring
        Assert.Contains("GENERATION AND TRANSPORT ARE DIFFERENT RELATIONS", standing);
        Assert.Contains("ASSUMES the ring", standing);
        // limit 4: a disconnected sector is unreachable rather than invisible
        Assert.Contains("IT IS UNREACHABLE", standing);
        Assert.Contains("no acceleration observable at all", standing);
        // and the audit must state what would overturn it rather than presenting the answer as forced
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("neither is taken", standing);
        Assert.StartsWith("ASSUMED", ConnectivityOriginAudit.Verdict());
        Assert.DoesNotContain("IS DERIVED", ConnectivityOriginAudit.Verdict());

        sb.AppendLine();
        sb.AppendLine("  the four limits are present, and the verdict is presented as an INPUT rather than as a derivation.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_080_10_Reproducible()
    {
        PrintHeader("G_080 - reproducibility");
        var sb = new StringBuilder();
        sb.AppendLine("  Every figure in this suite comes from two constructed objects and no data:");
        sb.AppendLine("      the LINK SET of a substrate: one ring per component, built explicitly, never assumed;");
        sb.AppendLine("      the divergence matrix M of that link set and the transport Laplacian L of the same link set.");
        sb.AppendLine("  The central claims are integer claims - the divergence's rank is n - c on integer matrices, the");
        sb.AppendLine("  witnesses are +1/-1 changes, the forbidden transfers conserve the total EXACTLY - so a tolerance of");
        sb.AppendLine("  1e-9 is far below any possible rounding, and the two quantities that are floating point (the");
        sb.AppendLine("  acceleration over a state sweep and the redshift ratio) are stated with the tolerance the test applies.");
        sb.AppendLine("  The SUBSTRATE is named with every count: the 1D ring family, one ring per component, from 2 cells to");
        sb.AppendLine("  the canonical 96, and no spectral quantity is quoted anywhere in this audit.");

        Assert.Equal(ConnectivityOriginAudit.Verdict(), ConnectivityOriginAudit.Verdict());
        Assert.Equal(ConnectivityOriginAudit.WhereItStands(), ConnectivityOriginAudit.WhereItStands());
        Assert.Equal(ConnectivityOriginAudit.DisjointUnionClosure().Length, ConnectivityOriginAudit.DisjointUnionClosure().Length);
        Assert.All(ConnectivityOriginAudit.DisjointUnionClosure(), c => Assert.False(c.ContentChanged));
        Assert.All(ConnectivityOriginAudit.TransportCapacity(), c => Assert.Equal(c.Cells - c.Components, c.Directions));

        sb.AppendLine("  The audit is deterministic: fixed enumerations, no randomness and no external data.");
        Output.WriteLine(sb.ToString());
    }
}
