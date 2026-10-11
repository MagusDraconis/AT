using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_082 - COMPONENT COUNT AUDIT.
///
/// G_080 established that the substrate's connectivity is ASSUMED and that 0 of the 4 allowed primitives detect a cut;
/// G_081 then measured that connectivity acts on the surviving gravity/time sector ONLY through the component count c
/// (6 of 29 claims depend on it, 23 are independent). This suite asks what is left: **what sets c**, and is c physical
/// or a coordinate choice? Difference, Actualization, the generation relation and count conservation are allowed;
/// assuming connectedness, importing topology and fixing c = 1 are forbidden.
///
/// The report follows the six tasks of the assignment. Every number quoted is computed here, and the substrate is
/// named with every count: the union construction copies ONE component of 24 cells, so c = 1, 2, 4, 8 give 24, 48, 72
/// and 96 cells and the component — hence the local physics — is held fixed.
/// </summary>
public sealed class Y_G_082_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_082_Tests(ITestOutputHelper output) : base(output) { }

    private static string F(double v) => v.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_082_01_TheRedundancyMap_ChangingCChangesNoLocalMeasurement()
    {
        PrintHeader("G_082 - tasks 2 and 5: the redundancy map (the union construction)");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the union construction copies ONE component of 24 cells, so the component is FIXED and only c varies:");
        sb.AppendLine("    a counterfactual that instead fixes the total cell count would shrink the component, which changes local");
        sb.AppendLine("    physics for a trivial reason and would not be a statement about c;");
        sb.AppendLine("  - the local readouts are the potential, the clock rate and the gradient inside a component, and the one");
        sb.AppendLine("    relational readout is the redshift between two cells of the SAME component;");
        sb.AppendLine("  - the SHAPE of a read set is every value divided by the largest, which is the sector's own logic (only");
        sb.AppendLine("    ratios are read, G_079) turned into a comparison;");
        sb.AppendLine("  - c = 1 is NOT assumed anywhere: it is one column of the table.");
        sb.AppendLine();
        sb.Append(ComponentCountAudit.OutputRedundancy());

        var unnormalized = ComponentCountAudit.UnionRedundancy();
        var normalized = ComponentCountAudit.UnionRedundancy(true);
        Assert.Equal(4, unnormalized.Length);

        // the redundancy: no local shape, no local value and no ratio moves, in either reading, at any c
        Assert.All(unnormalized, r =>
        {
            Assert.Equal(0, r.ShapeChanges);
            Assert.Equal(0, r.RatioChanges);
            Assert.Equal(0, r.ValueChanges);                  // unnormalized: the relative reads are identical too
        });
        Assert.All(normalized, r =>
        {
            Assert.Equal(0, r.ShapeChanges);                  // the SHAPE is invariant even when the level is not
            Assert.Equal(0, r.RatioChanges);
            Assert.Equal(0, r.ValueChanges);                  // the RELATIVE reads cancel the normalization exactly
        });

        // what DOES move: the global descriptors, and the absolute level the normalization shifts
        Assert.All(unnormalized.Where(r => r.Copies >= 2), r => Assert.Equal(3, r.GlobalDescriptorsChanged));
        Assert.Equal(0, unnormalized.Single(r => r.Copies == 1).GlobalDescriptorsChanged);
        Assert.All(unnormalized, r => Assert.Equal(1.0, r.AbsoluteScale, 12));
        Assert.All(normalized.Where(r => r.Copies >= 2), r => Assert.True(Math.Abs(r.AbsoluteScale - 1.0) > 1e-9,
            "the normalization must shift the absolute level, which is the whole point of the gauge argument"));
        Assert.Equal(Math.Pow(2, -1.0 / 3.0), normalized.Single(r => r.Copies == 2).AbsoluteScale, 12);

        sb.AppendLine($"  the shape is invariant at every c in both readings; the only moving local quantity is the uniform scale "
            + $"({N(normalized.Single(r => r.Copies == 2).AbsoluteScale)} at c = 2), and it multiplies every read equally.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_082_02_TheObservableCensus_NoLocalReadoutReadsC()
    {
        PrintHeader("G_082 - tasks 1 and 3: is c observable, and does any observable read absolute c?");
        var sb = new StringBuilder();
        sb.AppendLine("THE CENSUS OF READOUTS, AND WHICH OF THEM SEE THE COMPONENT COUNT");
        sb.AppendLine();
        sb.Append(ComponentCountAudit.OutputCensus());

        var census = ComponentCountAudit.ObservableCensus();
        var counts = ComponentCountAudit.ObservableCounts();
        Assert.Equal(census.Length, counts.Local + counts.Ratio + counts.CrossComponent + counts.Global);
        // task 3: NO observable of the local or ratio kind reads c
        Assert.Equal(0, ComponentCountAudit.ObservablesReadingAbsoluteC());
        Assert.All(census.Where(c => c.Kind is "local" or "ratio"), c => Assert.False(c.ReadsTheComponentCount));
        // the cross-component redshift cannot read c because it has no value at all
        var cross = census.Single(c => c.Kind == "cross-component");
        Assert.False(cross.ReadsTheComponentCount);
        Assert.Contains("0 links across a cut", cross.Measurement);
        // task 1: c IS observable, but only globally
        Assert.True(counts.ReadingTheComponentCount >= 3);
        Assert.All(census.Where(c => c.ReadsTheComponentCount),
            c => Assert.StartsWith("global", c.Kind, StringComparison.Ordinal));
        Assert.True(counts.Global >= 3);

        sb.AppendLine($"  {counts.Local + counts.Ratio + counts.CrossComponent} of the {census.Length} readouts cannot see c at all, and the "
            + $"{counts.ReadingTheComponentCount} that can are all global.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_082_03_TheClassification_IsGaugeLikeWithThreeRefutations()
    {
        PrintHeader("G_082 - task 4: classify c");
        var sb = new StringBuilder();
        sb.AppendLine("FOUR CANDIDATES, EACH TESTED BY A MEASUREMENT");
        sb.AppendLine();
        sb.Append(ComponentCountAudit.OutputClassification());

        var rows = ComponentCountAudit.Classification();
        Assert.Equal(4, rows.Length);
        Assert.Equal(ComponentCountKind.GaugeLike, ComponentCountAudit.Classify());
        Assert.True(rows.Single(r => r.Candidate == "GAUGE-LIKE").Supported);
        Assert.False(rows.Single(r => r.Candidate == "DERIVED").Supported);
        Assert.False(rows.Single(r => r.Candidate == "EMERGENT").Supported);
        Assert.False(rows.Single(r => r.Candidate == "PHYSICAL INPUT").Supported);
        // each refutation carries its number, so the branches are measurements rather than adjectives
        Assert.Contains("0 of 4 allowed primitives", rows.Single(r => r.Candidate == "DERIVED").Measurement);
        Assert.Contains("0 Laplacian entries between components", rows.Single(r => r.Candidate == "EMERGENT").Measurement);
        Assert.Contains("LIVE CONTROL of 4", rows.Single(r => r.Candidate == "EMERGENT").Measurement);
        Assert.Contains("0 local readouts read c", rows.Single(r => r.Candidate == "GAUGE-LIKE").Measurement);
        Assert.Contains("0 observables read c", rows.Single(r => r.Candidate == "PHYSICAL INPUT").Measurement);

        sb.AppendLine($"  COMPUTED: {ComponentCountAudit.KindName(ComponentCountAudit.Classify())} - DERIVED, EMERGENT and PHYSICAL INPUT are each refuted by a number.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_082_04_PhysicalOrCoordinate_TheSplitIsMeasured()
    {
        PrintHeader("G_082 - task 6: is c = 1 physical, or a coordinate choice?");
        var sb = new StringBuilder();
        sb.AppendLine("THE SPLIT IS G_081's OWN DEPENDENCY MAP, NOT A NEW JUDGEMENT");
        sb.AppendLine("  the claims that are independent of c cannot tell the difference (coordinate choice); the claims that");
        sb.AppendLine("  depend on it are the ones for which c is a physical statement.");
        sb.AppendLine();
        var split = ComponentCountAudit.PhysicalOrCoordinate();
        sb.AppendLine($"  coordinate choice (c-independent) : {split.CoordinateChoice} of {split.Total} claims, fraction {F3(split.CoordinateFraction)}");
        sb.AppendLine($"  physical          (c-dependent)   : {split.PhysicalLayer} of {split.Total}");
        sb.AppendLine();
        foreach (var c in ConnectivityDependencyAudit.CoreWithoutConnectivity())
            sb.AppendLine($"  COORDINATE  {c.Claim}  [{c.Audit}]");
        foreach (var c in ConnectivityDependencyAudit.FailureCore())
            sb.AppendLine($"  PHYSICAL    {c.Claim}  [{c.Audit}]");
        Output.WriteLine(sb.ToString());

        Assert.Equal(29, split.Total);
        Assert.Equal(23, split.CoordinateChoice);
        Assert.Equal(6, split.PhysicalLayer);
        Assert.Equal(23.0 / 29.0, split.CoordinateFraction, 12);
        Assert.True(split.CoordinateFraction > 0.75, "the coordinate side must be the wide one");
    }

    [Fact]
    public void Y_G_082_05_TheMapThatChangesCIsInvisibleToTheFourPrimitives()
    {
        PrintHeader("G_082 - cross-check: the map that changes c, against the allowed primitives");
        var sb = new StringBuilder();
        sb.AppendLine("WHY THE REDUNDANCY IS NOT AN ARTEFACT OF THE AUDIT'S OWN READOUTS");
        sb.AppendLine("  the map that changes c is the disjoint union, and G_080 measured it against the four allowed primitives:");
        sb.AppendLine();
        foreach (var c in ConnectivityOriginAudit.DisjointUnionClosure())
            sb.AppendLine($"  {c.Primitive,-22} content changed: {c.ContentChanged}");
        sb.AppendLine();
        var descriptors = ConnectivityOriginAudit.GlobalDescriptors();
        foreach (var d in descriptors)
            sb.AppendLine($"  global: {d.Descriptor,-48} {d.OnTheSubstrate} -> {d.OnTheDisjointUnion}");
        Output.WriteLine(sb.ToString());

        Assert.All(ConnectivityOriginAudit.DisjointUnionClosure(), c => Assert.False(c.ContentChanged));
        Assert.All(descriptors, d => Assert.NotEqual(d.OnTheSubstrate, d.OnTheDisjointUnion));
        // and the same map is the audit's own union construction: its global count moves and its local shape does not
        var redundancy = ComponentCountAudit.UnionRedundancy();
        Assert.Equal(0, redundancy.Max(r => r.ShapeChanges));
        Assert.True(redundancy.Max(r => r.GlobalDescriptorsChanged) > 0);
    }

    [Fact]
    public void Y_G_082_06_TheTotalCountIsNormalizedAway()
    {
        PrintHeader("G_082 - task 3 continued: the one global quantity the union changes is a convention");
        var sb = new StringBuilder();
        sb.AppendLine("THE TOTAL COUNT IS THE ONLY GLOBAL OUANTITY THE UNION MOVES, AND THE THEORY NORMALIZES IT");
        sb.AppendLine("  the Born normalization sum rho = 1 is exact by construction in this theory, so the state that carries a");
        sb.AppendLine("  second component carries half the density in each - the reading in which c is visible through the total is");
        sb.AppendLine("  therefore a CONVENTION and not a measurement.");
        sb.AppendLine();

        foreach (var c in ComponentCountAudit.ComponentCounts)
        {
            var raw = ComponentCountAudit.UnionState(c);
            var norm = ComponentCountAudit.UnionState(c, true);
            sb.AppendLine($"  c = {c}: total (unnormalized) {F3(raw.Sum())}, total (normalized) {F3(norm.Sum())}, "
                + $"component sum (normalized) {F3(norm.Take(ComponentCountAudit.ComponentCells).Sum())}");
        }
        Output.WriteLine(sb.ToString());

        // the normalized union is a state of the same theory: its total is one, and its per-component sum is one over c
        Assert.All(ComponentCountAudit.ComponentCounts, c =>
        {
            Assert.Equal(1.0, ComponentCountAudit.UnionState(c, true).Sum(), 12);
            Assert.Equal(1.0 / c,
                ComponentCountAudit.UnionState(c, true).Take(ComponentCountAudit.ComponentCells).Sum(), 12);
        });
        // and the local shape is invariant in that reading too, so nothing local can read c
        string reference = ComponentCountAudit.LocalReadShape(1, true);
        Assert.All(ComponentCountAudit.ComponentCounts, c =>
            Assert.Equal(reference, ComponentCountAudit.LocalReadShape(c, true)));
    }

    [Fact]
    public void Y_G_082_07_Verdict_IsComputed_AndIsGaugeLike()
    {
        PrintHeader("G_082 - verdict");
        var sb = new StringBuilder();
        sb.Append(ComponentCountAudit.OutputVerdict());

        string verdict = ComponentCountAudit.Verdict();
        Assert.StartsWith("GAUGE-LIKE", verdict);
        Assert.Contains("NO LOCAL PHYSICAL CONTENT", verdict);
        Assert.Contains("ONE GLOBAL EXCEPTION", verdict);
        Assert.Contains("UNPHYSICAL", verdict);
        Assert.Contains("COORDINATE CHOICE", verdict);
        Assert.Contains("REFUTED BY MEASUREMENT", verdict);

        // the branches are computed, not asserted: the classifier's other three kinds are refuted by measurements
        Assert.Equal(ComponentCountKind.GaugeLike, ComponentCountAudit.Classify());
        Assert.All(ComponentCountAudit.Classification().Where(r => r.Candidate != "GAUGE-LIKE"),
            r => Assert.False(r.Supported));
        // nothing in the theory sets c: the primitives are blind and the class conserves it
        Assert.False(ConnectivityDependencyAudit.Classification().Single(r => r.Kind == "LOCAL INPUT").Supported);
        Assert.All(ConnectivityOriginAudit.DisjointUnionClosure(), c => Assert.False(c.ContentChanged));
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_082_08_WhereItStands_CarriesTheFourLimits()
    {
        PrintHeader("G_082 - where it stands");
        var sb = new StringBuilder();
        sb.AppendLine("THE LIMITS MUST TRAVEL WITH THE VERDICT");
        sb.AppendLine();
        string standing = ComponentCountAudit.WhereItStands();
        sb.AppendLine(standing);

        Assert.Contains("GAUGE-LIKE IS NOT 'PURE GAUGE'", standing);
        Assert.Contains("FIXES THE COMPONENT SIZE", standing);
        Assert.Contains("UNOBSERVABLE ONLY GIVEN G_079", standing);
        Assert.Contains("THEORY'S OWN CONVENTION", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("neither is taken", standing);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_082_09_TheRefutationAttempts()
    {
        PrintHeader("G_082 - the refutation attempts, and why the gauge branch survives");
        var sb = new StringBuilder();
        sb.AppendLine("THE AUDIT PREFERS REFUTATION, SO IT TRIES BOTH SIDES AND REPORTS WHAT IT FOUND");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 1 - refute GAUGE-LIKE by finding a local readout that reads c:");
        var census = ComponentCountAudit.ObservableCensus();
        foreach (var c in census.Where(c => c.Kind is "local" or "ratio" or "cross-component"))
            sb.AppendLine($"    searched: {c.Readout,-52} reads c: {c.ReadsTheComponentCount}");
        int localReaders = ComponentCountAudit.ObservablesReadingAbsoluteC();
        sb.AppendLine($"    RESULT: {localReaders} local or ratio readouts read c - the refutation FAILS, and the audit reports the one place it does not:");
        foreach (var c in census.Where(c => c.ReadsTheComponentCount))
            sb.AppendLine($"      (global) {c.Readout}");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 2 - refute PHYSICAL INPUT by finding any observable that reads c:");
        sb.AppendLine($"    RESULT: {localReaders} observables read absolute c of {census.Length} readouts, so the physical-input reading is refuted");
        sb.AppendLine("    while the two GLOBAL readings that see it are disclosed (a reachability test needs two components at once).");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 3 - refute the REDUNDANCY by finding a local quantity that moves with c:");
        var redundancy = ComponentCountAudit.UnionRedundancy();
        sb.AppendLine($"    RESULT: shape changes {redundancy.Max(r => r.ShapeChanges)}, ratio changes {redundancy.Max(r => r.RatioChanges)} across c = 1, 2, 4, 8");
        sb.AppendLine("    - the redundancy stands, and only the global descriptors move.");
        Output.WriteLine(sb.ToString());

        // the three attempts are exhaustive over the census, and the survivor is the gauge branch
        Assert.All(census.Where(c => c.Kind is "local" or "ratio" or "cross-component"),
            c => Assert.False(c.ReadsTheComponentCount));
        Assert.Equal(ComponentCountKind.GaugeLike, ComponentCountAudit.Classify());
        Assert.Equal(0, redundancy.Max(r => r.ShapeChanges));
        Assert.Equal(0, redundancy.Max(r => r.RatioChanges));
    }

    [Fact]
    public void Y_G_082_10_Reproducible()
    {
        PrintHeader("G_082 - reproducibility");
        var sb = new StringBuilder();
        sb.AppendLine("  Every figure in this suite comes from three constructed objects and no data:");
        sb.AppendLine("      ONE component of 24 cells with a deterministic occupation, and c copies of it (the union);");
        sb.AppendLine("      the link set of the union, one closed ring per copy, so the component size is held fixed;");
        sb.AppendLine("      the sector's own readouts (potential, clock rate, gradient), compared as SHAPES (value over maximum)");
        sb.AppendLine("      and as RATIOS, never as absolute values - which is the sector's own logic and not an audit choice.");
        sb.AppendLine("  Tolerances are stated where they are used: 1e-12 for an exact equality of values and of the scale factor.");
        sb.AppendLine("  The SUBSTRATE is named with every count: the union of c rings of 24 cells, so 24, 48, 72 and 96 cells at");
        sb.AppendLine("  c = 1, 2, 4 and 8; no spectral quantity is quoted anywhere in this audit, and c = 1 is never assumed - it is");
        sb.AppendLine("  a column of every table.");
        Assert.Equal(ComponentCountAudit.Verdict(), ComponentCountAudit.Verdict());
        Assert.Equal(ComponentCountAudit.WhereItStands(), ComponentCountAudit.WhereItStands());
        Assert.Equal(ComponentCountAudit.LocalReadShape(4), ComponentCountAudit.LocalReadShape(4));
        Assert.All(ComponentCountAudit.UnionRedundancy(), r => Assert.Equal(r.Copies * ComponentCountAudit.ComponentCells, r.TotalCells));
        Assert.All(ComponentCountAudit.Classification(), r => Assert.False(string.IsNullOrWhiteSpace(r.Measurement)));
        sb.AppendLine("  The audit is deterministic: fixed enumerations, no randomness and no external data.");
        Output.WriteLine(sb.ToString());
    }
}
