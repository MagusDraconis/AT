using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_083 - CONTINUITY GAUGE AUDIT.
///
/// G_079 derived the continuity law for the surviving update class and showed that its equivalence with count
/// conservation requires connectivity; G_080 made that connectivity an ASSUMED input; G_081 measured that only 6 of 29
/// surviving claims depend on it; G_082 classified the component count as GAUGE-LIKE, with 0 local observables reading
/// it. This suite asks the sharpest question of the chain: **is the continuity equation part of the physics, or part of
/// the coordinate choice?** Difference, Actualization, the generation relation and count conservation are the allowed
/// evidence; assuming connectedness, fixing c = 1, importing topology and adding source terms are forbidden.
///
/// The report follows the seven tasks. Every number quoted is computed here, and the substrate is named with every
/// count: the canonical 96-cell ring, with the flux and its gauge orbit carried through every comparison.
/// </summary>
public sealed class Y_G_083_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_083_Tests(ITestOutputHelper output) : base(output) { }

    private static string F(double v) => v.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_083_01_TheObservablesReExpressedWithoutContinuity()
    {
        PrintHeader("G_083 - task 1: every surviving observable without the continuity equation");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the state is the canonical uniform occupation on the 96-cell ring, so every figure below is computed");
        sb.AppendLine("    from ONE object and the comparisons are like for like;");
        sb.AppendLine("  - the sector's readouts are the clock rate, the potential, the gradient (the acceleration) and the");
        sb.AppendLine("    redshift between two sites - the observables G_077 and G_079 call the sector's;");
        sb.AppendLine("  - the continuity equation is carried through the comparison EXPLICITLY: the same readout is evaluated with");
        sb.AppendLine("    the flux present, with the flux displaced along its gauge orbit and with the flux absent;");
        sb.AppendLine("  - no source term is added, and c = 1 is never assumed: the audit is written on the canonical ring and the");
        sb.AppendLine("    component count never enters a readout (G_082).");
        sb.AppendLine();
        sb.Append(ContinuityGaugeAudit.OutputObservables());

        var observables = ContinuityGaugeAudit.ObservablesWithoutContinuity();
        Assert.Equal(4, observables.Length);
        Assert.All(observables, o => Assert.Equal(0.0, o.Difference, 12));
        Assert.All(observables, o => Assert.Equal(o.WithTheFlux, o.WithNoFlux, 12));
        Assert.All(observables, o => Assert.Equal(o.WithTheFlux, o.WithTheFluxDisplaced, 12));
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());

        // the re-expression is EXACT: an integer flux displaced by a circulation leaves the divergence unchanged
        var circulation = ContinuityLawAudit.CirculationDoesNotChangeTheDivergence(new[] { 4, 8, 16, 48, 96 });
        Assert.All(circulation, c => Assert.Equal(0, c.MaxAbsoluteDivergenceChange));

        sb.AppendLine("  the re-expression is exact rather than approximate: the same readout is identical with the flux present, displaced and absent,");
        sb.AppendLine("  and a circulation leaves every divergence unchanged on every ring size.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_02_TheFluxRequirementCensus()
    {
        PrintHeader("G_083 - task 2: which claims need J, and which need only rho");
        var sb = new StringBuilder();
        sb.AppendLine("NOT ONE SURVIVING CLAIM IS A FUNCTION OF THE FLUX");
        sb.AppendLine("  the census takes the 29 surviving claims of G_035 and G_068-G_082 through the dependency map's own");
        sb.AppendLine("  classification, so the count is not a fresh judgement: the claims that need more than the occupation need");
        sb.AppendLine("  the OPERATOR's structure, which is a property of the substrate and not a value of any flux.");
        sb.AppendLine();
        sb.Append(ContinuityGaugeAudit.OutputCensus());

        var census = ContinuityGaugeAudit.FluxRequirementCensus();
        var counts = ContinuityGaugeAudit.FluxRequirementCounts();
        var claims = ConnectivityDependencyAudit.Claims().Length;
        Assert.Equal(claims, census.Length);
        Assert.Equal(census.Length, counts.OccupationOnly + counts.OperatorStructure + counts.RequiresAFluxValue);
        Assert.Equal(0, counts.RequiresAFluxValue);
        // and the six that need structure are exactly G_081's direct set
        Assert.Equal(ConnectivityDependencyAudit.FailureCore().Length, counts.OperatorStructure);
        Assert.Equal(ConnectivityDependencyAudit.CoreWithoutConnectivity().Length, counts.OccupationOnly);

        sb.AppendLine($"  {counts.OccupationOnly} of {census.Length} claims need the occupation only, {counts.OperatorStructure} need the operator's structure, "
            + $"and {counts.RequiresAFluxValue} require a flux value.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_03_TheTransportLayerRemoved()
    {
        PrintHeader("G_083 - task 3: remove the transport layer entirely and recompute");
        var sb = new StringBuilder();
        sb.AppendLine("CLOCKS, REDSHIFT, ACCELERATION AND CONSERVATION, WITH NO FLUX, DIVERGENCE OR LEDGER IN THE DESCRIPTION");
        sb.AppendLine();
        sb.Append(ContinuityGaugeAudit.OutputWithoutTheLayer());

        var rows = ContinuityGaugeAudit.WithoutTheTransportLayer();
        Assert.Equal(4, rows.Length);
        Assert.All(rows, r => Assert.Equal(0.0, r.Difference, 12));
        // the three readouts are unchanged to the last bit ...
        Assert.All(rows.Where(r => !r.Quantity.StartsWith("count")), r => Assert.Equal(r.WithTheTransportLayer, r.WithoutIt, 12));
        // ... and conservation is emptied rather than violated
        Assert.Contains(rows, r => r.Quantity.StartsWith("count") && r.Note.Contains("VACUOUS", StringComparison.Ordinal));

        // the layer's own exactness, which is what makes this a description and not an approximation
        var classRows = ContinuityLawAudit.UpdateClassAsContinuity().Where(r => !r.Operator.StartsWith("CONTROL")).ToArray();
        Assert.All(classRows, r => Assert.Equal(0, r.ReconstructionResidual));

        sb.AppendLine($"  0 of the {rows.Length} recomputed quantities changes: the clock, the redshift and the acceleration are identical, "
            + "and conservation is vacuous without an evolution to govern.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_04_SameObservablesDifferentTransportHistories()
    {
        PrintHeader("G_083 - task 4: the equivalence classes");
        var sb = new StringBuilder();
        sb.AppendLine("EXPLICIT CONSTRUCTIONS, EACH PAIR SHARING ONE SET OF OBSERVABLES");
        sb.AppendLine();
        sb.Append(ContinuityGaugeAudit.OutputClasses());

        var classes = ContinuityGaugeAudit.EquivalenceClasses();
        Assert.Equal(3, classes.Length);
        // same observables in every class
        Assert.All(classes, c => Assert.Equal(0.0, c.ObservableDifference, 12));
        // and a different transport history in every class
        Assert.All(classes, c => Assert.True(Math.Abs(c.BookkeepingDifference) > 1e-9,
            $"{c.Class}: the histories must actually differ, or the class is trivial"));
        Assert.Contains(classes, c => c.Class.StartsWith("the flux's gauge orbit"));
        Assert.Contains(classes, c => c.Class.StartsWith("the source-annotated"));
        Assert.Contains(classes, c => c.Class.StartsWith("two histories"));
        // the gauge orbit's bookkeeping difference is the circulation, and the source class differs by the source
        Assert.Equal(5.0, classes.Single(c => c.Class.StartsWith("the flux's")).BookkeepingDifference, 12);
        Assert.True(classes.Single(c => c.Class.StartsWith("the source-annotated")).BookkeepingDifference > 0.0);

        sb.AppendLine($"  {classes.Length} classes, each with an observable difference of exactly {N(0.0)} and a non-zero bookkeeping difference.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_05_PhysicalMeasurementVersusBookkeeping()
    {
        PrintHeader("G_083 - task 5: does continuity change a measurement or a bookkeeping?");
        var sb = new StringBuilder();
        sb.AppendLine("EACH COMPARISON IS TAKEN ACROSS A DIFFERENT DESCRIPTION OF THE SAME PHYSICS");
        sb.AppendLine();
        sb.Append(ContinuityGaugeAudit.OutputClasses().Split("5. ")[1]);

        var comparison = ContinuityGaugeAudit.PhysicalVersusBookkeeping();
        Assert.True(comparison.Length >= 4);
        Assert.All(comparison, c => Assert.Equal(0.0, c.PhysicalDifference, 12));
        Assert.True(comparison.Max(c => Math.Abs(c.BookkeepingDifference)) > 1e-9,
            "at least one description must differ in its bookkeeping, or the audit proves nothing");
        // three descriptions move the bookkeeping, and the row that removes the layer moves NOTHING (there is no
        // bookkeeping left to move), which is the point of that row rather than an exception to it
        Assert.Equal(3, comparison.Count(c => Math.Abs(c.BookkeepingDifference) > 1e-9));
        Assert.Equal(0.0,
            comparison.Single(c => c.Comparison.StartsWith("with the transport layer")).BookkeepingDifference, 12);

        sb.AppendLine($"  the physical difference is {N(comparison.Max(c => Math.Abs(c.PhysicalDifference)))} in all {comparison.Length} comparisons, "
            + $"while the bookkeeping difference reaches {F3(comparison.Max(c => Math.Abs(c.BookkeepingDifference)))}.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_06_TheClassification_IsCoordinateChoiceWithThreeRefutations()
    {
        PrintHeader("G_083 - task 6: classify the continuity equation");
        var sb = new StringBuilder();
        sb.AppendLine("FOUR CANDIDATES, EACH TESTED BY A MEASUREMENT");
        sb.AppendLine();
        sb.Append(ContinuityGaugeAudit.OutputClassification());

        var rows = ContinuityGaugeAudit.Classification();
        Assert.Equal(4, rows.Length);
        Assert.Equal(ContinuityStatus.CoordinateChoice, ContinuityGaugeAudit.Classify());
        Assert.True(rows.Single(r => r.Candidate == "COORDINATE CHOICE").Supported);
        Assert.False(rows.Single(r => r.Candidate == "PHYSICAL LAW").Supported);
        Assert.False(rows.Single(r => r.Candidate == "EFFECTIVE LAW").Supported);
        Assert.False(rows.Single(r => r.Candidate == "GAUGE FIXING").Supported);
        // each refutation carries its number
        Assert.Contains("0 of the sector's observables read a flux", rows.Single(r => r.Candidate == "PHYSICAL LAW").Measurement);
        Assert.Contains("EXACT and not an approximation", rows.Single(r => r.Candidate == "EFFECTIVE LAW").Measurement);
        Assert.Contains("does not REMOVE a freedom", rows.Single(r => r.Candidate == "GAUGE FIXING").Measurement);

        // the GAUGE FIXING refutation rests on the circulation surviving the law
        var circulation = ContinuityLawAudit.CirculationCount(new[] { 4, 8, 16, 48, 96 });
        Assert.All(circulation, c => Assert.Equal(1, c.Nullity));

        sb.AppendLine($"  COMPUTED: {ContinuityGaugeAudit.StatusName(ContinuityGaugeAudit.Classify())} - PHYSICAL LAW, EFFECTIVE LAW and GAUGE FIXING are each refuted by a number.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_07_FailureCoreOfContinuity_IsEmptyInObservables()
    {
        PrintHeader("G_083 - task 7: FailureCore(continuity)");
        var sb = new StringBuilder();
        sb.AppendLine("WHAT FAILS WHEN THE CONTINUITY EQUATION IS REMOVED");
        sb.AppendLine();
        var core = ContinuityGaugeAudit.FailureCore();
        var size = ContinuityGaugeAudit.FailureCoreSize();
        foreach (var c in core)
            sb.AppendLine($"  {c.Kind,-11} {c.Claim}");
        sb.AppendLine();
        sb.AppendLine($"  {size.Observable} observable, {size.Structural} structural, {size.Total} in total.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(0, size.Observable);
        Assert.Equal(3, size.Structural);
        Assert.Equal(3, core.Length);
        Assert.All(core, c => Assert.Equal("structural", c.Kind));
        // and the structural one that matters is a DETECTOR: its residual equals the source exactly
        var ledger = ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12);
        Assert.Equal(Math.Abs(ledger.Source), ledger.ResidualBeforeDeclaration, 12);
        Assert.True(ledger.TheLedgerIsLive);
        Assert.Contains(core, c => c.Why.Contains("DETECTOR", StringComparison.Ordinal));
    }

    [Fact]
    public void Y_G_083_08_Verdict_IsComputed_AndIsCoordinateChoice()
    {
        PrintHeader("G_083 - verdict");
        var sb = new StringBuilder();
        sb.Append(ContinuityGaugeAudit.OutputVerdict());

        string verdict = ContinuityGaugeAudit.Verdict();
        Assert.StartsWith("COORDINATE CHOICE", verdict);
        Assert.Contains("BOOKKEEPING OF A CONSERVED EVOLUTION", verdict);
        Assert.Contains("NOT A MEASUREMENT", verdict);
        Assert.Contains("VACUOUS", verdict);
        Assert.Contains("CARRIES a gauge", verdict);
        Assert.Contains("DETECTOR", verdict);
        Assert.Contains("NOT fundamental physics", verdict);

        // the branches are computed (G_027): the other three candidates are refuted by measurement
        Assert.Equal(ContinuityStatus.CoordinateChoice, ContinuityGaugeAudit.Classify());
        Assert.All(ContinuityGaugeAudit.Classification().Where(r => r.Candidate != "COORDINATE CHOICE"),
            r => Assert.False(r.Supported));

        sb.AppendLine();
        sb.AppendLine("  the classification is computed: the other three candidates are refuted by measured numbers, and the verdict");
        sb.AppendLine("  states what the law IS (a falsifiable detector) rather than only what it is not.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_09_WhereItStands_CarriesTheFourLimits()
    {
        PrintHeader("G_083 - where it stands");
        var sb = new StringBuilder();
        sb.AppendLine("THE LIMITS MUST TRAVEL WITH THE VERDICT");
        sb.AppendLine();
        string standing = ContinuityGaugeAudit.WhereItStands();
        sb.AppendLine(standing);

        Assert.Contains("COORDINATE CHOICE IS NOT 'CONTENTLESS'", standing);
        Assert.Contains("NOT IDENTIFIED WITH COUNT CONSERVATION", standing);
        Assert.Contains("NOT CLAIMS THAT NEED THE LAW", standing);
        Assert.Contains("ABOUT THE SURVIVING SECTOR", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("none is taken", standing);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_083_10_Reproducible()
    {
        PrintHeader("G_083 - reproducibility");
        var sb = new StringBuilder();
        sb.AppendLine("  Every figure in this suite comes from constructed objects and no data:");
        sb.AppendLine("      the canonical 96-cell ring with a uniform occupation, one object for every comparison;");
        sb.AppendLine("      the discrete divergence and the integer flux of the class (ContinuityLawAudit), carried through the");
        sb.AppendLine("      comparison in all three forms (present, displaced by a circulation, absent);");
        sb.AppendLine("      the sector's readouts (clock, potential, gradient, redshift) as the surviving audits define them.");
        sb.AppendLine("  The claims about the flux and its gauge are INTEGER claims (residual 0, nullity 1), so the exactness of the");
        sb.AppendLine("  re-expression does not rest on a tolerance; the floating-point equalities are asserted at 1e-12 and said so.");
        sb.AppendLine("  The SUBSTRATE is named with every count: the canonical 96-cell ring; no spectral quantity is quoted, c = 1 is");
        sb.AppendLine("  never assumed as a premise (it is the canonical ring, which the audit states), and no source term is added.");
        Assert.Equal(ContinuityGaugeAudit.Verdict(), ContinuityGaugeAudit.Verdict());
        Assert.Equal(ContinuityGaugeAudit.WhereItStands(), ContinuityGaugeAudit.WhereItStands());
        Assert.Equal(ContinuityGaugeAudit.EquivalenceClasses().Length, ContinuityGaugeAudit.EquivalenceClasses().Length);
        Assert.All(ContinuityGaugeAudit.ObservablesWithoutContinuity(), o => Assert.Equal(0.0, o.Difference, 12));
        Assert.All(ContinuityGaugeAudit.ObservablesWithoutContinuity(), o => Assert.False(string.IsNullOrWhiteSpace(o.Observable)));
        sb.AppendLine("  The audit is deterministic: fixed enumerations, no randomness and no external data.");
        Output.WriteLine(sb.ToString());
    }
}
