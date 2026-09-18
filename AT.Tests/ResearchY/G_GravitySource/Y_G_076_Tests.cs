using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_076 - Clock-Law Selection Audit. With G_075's result in hand - F(0) = 1, F'(0) = 2, and the family
/// F = 1 + 2x + x^2 G(x) with GR a member - search the ALLOWED STRUCTURAL sources (composition of successive
/// redshifts, clock synchronization consistency, path independence, group properties, conservation laws, locality,
/// the actualization-density interpretation, multiplicativity, operational clock transport) for one that selects a
/// unique clock law. No astrophysical fitting, no assumed exponential, no imported GR.
/// </summary>
public sealed class Y_G_076_Tests : ResearchTestBase
{
    public Y_G_076_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_G_076_TheAllowedSourcesAreNotIndependent()
    {
        PrintHeader("G_076 - the equivalence collapse: the allowed sources are one equation in several languages");

        var sb = new StringBuilder();
        sb.AppendLine("  law                                 composition   redshift      metric        log-additive  path          same zero set");
        foreach (var r in ClockLawSelectionAudit.EquivalenceCollapse())
            sb.AppendLine($"  {r.Law,-35} {r.Composition,-13:E3} {r.RedshiftFactor,-13:E3} {r.Metric,-13:E3} {r.LogAdditive,-13:E3} {r.PathIndependence,-13:E3} {r.SameZeroSet}");
        Output.WriteLine(sb.ToString());

        var collapse = ClockLawSelectionAudit.EquivalenceCollapse();
        Assert.Equal(ClockLawSelectionAudit.Corpus().Length, collapse.Length);

        // THE MEASUREMENT: the four rows that come from DIFFERENT allowed sources have the SAME ZERO SET - they are
        // one equation, so the source list is not four independent chances at a selector
        Assert.All(collapse, c => Assert.True(c.SameZeroSet, $"{c.Law}: the rows must vanish together"));

        // and the composition residual must FAIL for the free room - which the FIRST version of this measurement got
        // wrong, because an ABSOLUTE residual cannot see a constraint in the deep field where both rates are tiny
        double at = collapse.Single(c => c.Law == "exp(2x)").Composition;
        double free = collapse.Single(c => c.Law.StartsWith("exp(2x - 100")).Composition;
        Assert.True(at <= ClockLawSelectionAudit.CompositionTolerance, $"the exponential must satisfy it, got {at:E3}");
        Assert.True(free > 0.1, $"the free room must FAIL it, got {free:E3}");

        // the path-independence row is the same equation reached from the transport side, not a second constraint
        var exponential = collapse.Single(c => c.Law == "exp(2x)");
        var freeRoom = collapse.Single(c => c.Law.StartsWith("exp(2x - 100"));
        Assert.True(exponential.PathIndependence <= ClockLawSelectionAudit.CompositionTolerance);
        Assert.True(freeRoom.PathIndependence > 0.1);
    }

    [Fact]
    public void Y_G_076_TheNonSelectiveSourcesSelectNothing()
    {
        PrintHeader("G_076 - the sources that are satisfied by EVERY law, and why that is not a selection");

        var table = ClockLawSelectionAudit.SelectionTable();
        var corpus = ClockLawSelectionAudit.Corpus();

        var sb = new StringBuilder();
        sb.AppendLine("  constraint                                             survivors   spread       kind");
        foreach (var r in table)
            sb.AppendLine($"  {r.Constraint,-52} {r.Survivors,3} of {r.Corpus,-3} {r.Spread,11:E3}   {r.Kind}");
        Output.WriteLine(sb.ToString());
        // LOCALITY: the rate at a probe is the same in two configurations that differ in their gradient
        Assert.All(corpus, l => Assert.Equal(0.0, ClockLawSelectionAudit.LocalityResidual(l), 15));
        // SYNCHRONIZATION TRANSITIVITY: equality of rates is an equivalence relation for any law of the family
        Assert.All(corpus, l => Assert.Equal(0.0, ClockLawSelectionAudit.SynchronizationResidual(l), 15));
        // COUNT CONSERVATION: the residual contains no F at all, so it is the SAME number for every law
        Assert.Equal(0.0, corpus.Max(l => ClockLawSelectionAudit.CountConservationResidual(l))
                        - corpus.Min(l => ClockLawSelectionAudit.CountConservationResidual(l)), 15);

        // ...so those rows are NON-SELECTIVE for a MEASURED reason: the residual is law-independent
        foreach (string nonSelective in new[] { "locality", "synchronization transitivity", "count conservation" })
            Assert.StartsWith("NON-SELECTIVE", table.Single(t => t.Constraint == nonSelective).Kind);
        Assert.True(table.Count(t => t.Kind.StartsWith("NON-SELECTIVE")) >= 3);

        // and the non-selective rows are not a small effect: they are satisfied by ALL of the corpus
        Assert.All(table.Where(t => t.Constraint == "locality"), t => Assert.Equal(corpus.Length, t.Survivors));
    }

    [Fact]
    public void Y_G_076_TheActualizationCompositionIsRefutedRatherThanSelecting()
    {
        PrintHeader("G_076 - the actualization composition rule, tested and refused");

        var census = ClockLawSelectionAudit.CompositionRuleCensus();
        var sb = new StringBuilder();
        sb.AppendLine("  rule                R1         R2         combined rho   rule value   power law value   contradiction");
        foreach (var r in census)
            sb.AppendLine($"  {r.Rule,-19} {r.R1,-10:F6} {r.R2,-10:F6} {r.CombinedDensity,-14:F6} {r.RuleValue,-12:F6} {r.PowerLawValue,-17:F6} {r.Contradiction:E3}");
        Output.WriteLine(sb.ToString());

        // the PRODUCT rule reproduces the recorded power law exactly, so the composition rule IS constrained...
        Assert.Equal(0.0, census.Single(r => r.Rule == "product").Contradiction, 12);
        // ...but the two rules the actualization reading suggests - minimum ("all subsystems must advance") and the
        // mean - contradict it by a measured amount, so they are REFUTED rather than selective
        Assert.True(census.Single(r => r.Rule == "minimum").Contradiction > 0.01);
        Assert.True(census.Single(r => r.Rule == "arithmetic mean").Contradiction > 0.01);
        Assert.True(census.Single(r => r.Rule == "geometric mean").Contradiction > 0.01);

        // and the row is in the table as refuted, with no survivor counted
        var row = ClockLawSelectionAudit.SelectionTable()
            .Single(t => t.Constraint.StartsWith("actualization composition"));
        Assert.StartsWith("REFUTED", row.Kind);

        // the row is law-level rather than a constant: the contradiction is measured against each law's OWN rate
        var at = ClockLawUniquenessAudit.At();
        Assert.True(ClockLawSelectionAudit.MinimumRuleContradiction(at) > 0.01);
        Assert.True(ClockLawSelectionAudit.ResidualOf("actualization composition: all subsystems advance",
            ClockLawUniquenessAudit.Gr()) > 0.01);
        // ...and a law that does not carry the pinned data is not an admissible candidate for it, which is stated
        var trivial = ClockLawSelectionAudit.Corpus().Single(l => l.Name == "F = 1");
        Assert.True(double.IsPositiveInfinity(ClockLawSelectionAudit.ResidualOf(
            "actualization composition: all subsystems advance", trivial)));
    }

    [Fact]
    public void Y_G_076_MultiplicativitySelectsExactlyOneLawInThePotential()
    {
        PrintHeader("G_076 - the one source that selects, and how far it gets");

        var table = ClockLawSelectionAudit.SelectionTable();
        var row = table.Single(t => t.Constraint == "composition AND the pinned data (the conjunction)");
        Assert.Equal("SELECTS UNIQUELY", row.Kind);
        Assert.Equal(1, row.Survivors);

        // the composition law ALONE does not select - the constant of exp(kx) stays free, which the table shows as a
        // PARTIAL row rather than hiding it inside the conjunction
        var alone = table.Single(t => t.Constraint == "composition of successive redshifts");
        Assert.StartsWith("PARTIAL", alone.Kind);
        Assert.True(alone.Survivors > 1, $"the composition law alone must leave a family, got {alone.Survivors}");

        // and the survivor is the exponential, measured on the same corpus as everything else
        var survivors = ClockLawSelectionAudit.Corpus()
            .Where(l => ClockLawSelectionAudit.ConjunctionResidual(l) <= 1.0)
            .Select(l => l.Name).ToArray();
        Assert.Contains("exp(2x)", survivors);
        Assert.Single(survivors);

        // the conjunction's presupposition is stated rather than hidden: the row ADDS a composition law, and the audit
        // also states that the ALONE row does not finish the job
        Assert.Contains("composition law", row.Presupposes);
        Assert.Contains("neither finishes alone", row.Presupposes);
    }

    [Fact]
    public void Y_G_076_TheSelectorIsOnlyAsStrongAsTheResolutionItIsTestedAt()
    {
        PrintHeader("G_076 - the mesh witness: multiplicativity satisfied exactly where it can be tested");

        var mesh = ClockLawSelectionAudit.MeshMultiplication(1.0e-2)[0];
        var sb = new StringBuilder();
        sb.AppendLine($"  law: {mesh.Law}");
        sb.AppendLine($"  composition residual ON the mesh      {mesh.OnTheMesh:E3}");
        sb.AppendLine($"  composition residual BETWEEN the mesh {mesh.BetweenMesh:E3}");
        sb.AppendLine($"  worst deviation from exp(2x)          {mesh.MaxDeviation:E3}");
        sb.AppendLine($"  1 + z shift at the target             {mesh.TargetShift:E3}");
        Output.WriteLine(sb.ToString());

        // ON THE MESH the law is multiplicative exactly - an observer testing there cannot see the difference
        Assert.True(mesh.OnTheMesh <= ClockLawSelectionAudit.SelectionTolerance,
            $"the interpolant must satisfy every mesh composition exactly, got {mesh.OnTheMesh:E3}");
        // BETWEEN the mesh points it is a DIFFERENT LAW, by a measured amount
        Assert.True(mesh.BetweenMesh > 1.0e-3, $"the interpolant must fail between the points, got {mesh.BetweenMesh:E3}");
        Assert.True(mesh.MaxDeviation > 1.0e-3, $"the interpolant must deviate from exp(2x), got {mesh.MaxDeviation:E3}");
        Assert.True(mesh.TargetShift > 1.0e-3, $"the deviation must reach the observable, got {mesh.TargetShift:E3}");

        // the interpolant is a law of the family, not a pathology: positive, monotone and carrying the pinned value
        var law = ClockLawSelectionAudit.MeshInterpolant(1.0e-2);
        Assert.Equal(1.0, law.F(0.0), 12);
        for (int i = 1; i <= 400; i++)
        {
            double x = -0.5 + i * 0.5 / 400.0;
            Assert.True(law.F(x) > 0.0);
            Assert.True(law.F(x) > law.F(x - 0.5 / 400.0), $"monotonicity must hold at x = {x:F6}");
        }
        // ...and its slope at the vacuum is the pinned 2, measured at a step FINER than the mesh this time: the
        // resolution of the measurement decides what the constraint can see, which is the audit's point
        double h = 5.0e-5;
        Assert.Equal(2.0, (law.F(h) - law.F(-h)) / (2.0 * h), 4);
    }

    [Fact]
    public void Y_G_076_EveryWeakerFormSurvives()
    {
        PrintHeader("G_076 - the weaker laws that survive the strongest constraint");

        var sb = new StringBuilder();
        sb.AppendLine("  approximate multiplicativity: a surviving INTERVAL, not a point");
        sb.AppendLine("  tolerance   max |c|      span in 1 + z at the target");
        foreach (var a in ClockLawSelectionAudit.ApproximateMultiplicativity())
            sb.AppendLine($"  {a.Tolerance,-11:E3} {a.MaxC,-12:E3} {a.TargetSpread:E3}");
        sb.AppendLine();
        sb.AppendLine("  multiplicativity without the pinned slope: a ONE-PARAMETER family");
        sb.AppendLine("  k        composition residual   1 + z at the target   carries the pinned slope");
        foreach (var s in ClockLawSelectionAudit.SlopeFamily())
            sb.AppendLine($"  {s.K,-8:F1} {s.CompositionResidual,-23:E3} {s.OnePlusZAtTarget,-21:F12} {s.CarriesThePinnedSlope}");
        Output.WriteLine(sb.ToString());

        // APPROXIMATE multiplicativity leaves a continuum: the surviving set is an interval around c = 0, whose edge
        // the audit BISECTS on the residual it actually uses rather than deriving from a formula
        var approximate = ClockLawSelectionAudit.ApproximateMultiplicativity();
        Assert.All(approximate, a => Assert.True(a.MaxC > 0.0));
        double eps = 1.0e-3;
        double maxC = approximate.Single(a => a.Tolerance == eps).MaxC;
        double x = ClockLawUniquenessAudit.XTarget;
        // the endpoints and the midpoints all pass the tolerance, which makes the survivor set an INTERVAL, not a point
        foreach (double c in new[] { 0.0, maxC * 0.5, -maxC * 0.5, maxC * 0.9 })
        {
            var law = ClockLawUniquenessAudit.FreeRoom(c);
            Assert.True(ClockLawSelectionAudit.CompositionResidual(law) <= eps,
                $"c = {c:E3} must be inside the surviving interval");
        }
        Assert.True(ClockLawSelectionAudit.CompositionResidual(ClockLawUniquenessAudit.FreeRoom(2.0 * maxC)) > eps);
        // a tighter tolerance buys a narrower interval, never a point
        Assert.True(approximate.All(a => approximate.First().MaxC >= a.MaxC));
        Assert.True(approximate.Single(a => a.Tolerance == 1.0e-3).TargetSpread > 0.0);

        // multiplicativity WITHOUT the pinned slope leaves the constant free
        var slopes = ClockLawSelectionAudit.SlopeFamily();
        // multiplicity of the metric magnitude and log-additivity are the SAME equation, measured on the same corpus
        Assert.All(slopes, s => Assert.True(s.CompositionResidual <= ClockLawSelectionAudit.CompositionTolerance));
        Assert.Single(slopes, s => s.CarriesThePinnedSlope);
        Assert.True(slopes.Select(s => s.OnePlusZAtTarget).Distinct().Count() == slopes.Length);
    }

    [Fact]
    public void Y_G_076_TheSubgroupFreedomIsClosedByContinuityNotByComposition()
    {
        PrintHeader("G_076 - the subgroup witness: what actually closes the selector");

        var rows = ClockLawSelectionAudit.SubgroupApproach(2.9);
        var sb = new StringBuilder();
        sb.AppendLine("  the exponent A(m + n*sqrt(2)) = 2m + lambda*n is ADDITIVE on the subgroup generated by 1 and sqrt(2)");
        sb.AppendLine("  n        x (subgroup element)   exponent            F at that x");
        foreach (var g in rows)
            sb.AppendLine($"  {g.N,-8} {g.X,-24:E6} {g.Exponent,-19:F6} {g.F:E6}");
        sb.AppendLine($"  ...and the only lambda keeping the exponent linear is 2*sqrt(2) = {ClockLawSelectionAudit.SubgroupLambdaForLinearity():F6}");
        Output.WriteLine(sb.ToString());

        // the subgroup elements approach the vacuum while the VALUES do not: the law is unbounded near F(0) = 1
        Assert.True(Math.Abs(rows.Last().X) < Math.Abs(rows.First().X));
        Assert.True(Math.Abs(rows.Last().Exponent) > Math.Abs(rows.First().Exponent));
        Assert.True(Math.Abs(rows.Last().F - 1.0) > 0.5,
            $"the witness must be far from the vacuum value, got F = {rows.Last().F:E3} at x = {rows.Last().X:E3}");

        // and the lambda that makes it linear is exactly 2*sqrt(2), which is what turns the composition law into the
        // exponential - so CONTINUITY (a separate regularity primitive) is doing the closing, not the composition
        double lambda = ClockLawSelectionAudit.SubgroupLambdaForLinearity();
        foreach (var (m, n) in new[] { (3, -2), (17, -12), (99, -70), (239, -169) })
        {
            double x = m + n * Math.Sqrt(2.0);
            Assert.Equal(2.0 * x, ClockLawSelectionAudit.SubgroupExponent(lambda, m, n), 10);
        }
    }

    [Fact]
    public void Y_G_076_TheCostAndTheVerdict()
    {
        PrintHeader("G_076 - the cost of every added constraint, and the verdict");

        var cost = ClockLawSelectionAudit.CostTable();
        var sb = new StringBuilder();
        sb.AppendLine("  constraint set                                          survivors   spread       primitives");
        foreach (var c in cost)
            sb.AppendLine($"  {c.ConstraintSet,-55} {c.SurvivorsOfTheCorpus,3}         {c.SpreadAtTheTarget,11:E3}   {c.PrimitivesAdded}");
        Output.WriteLine(sb.ToString());

        // the running total: the empirical sector alone leaves a family; the composition law alone does NOT finish it;
        // composition plus the pinned data gives one law; the continuum form adds a regularity primitive; the map is
        // the third and last
        Assert.Equal(0, cost[0].PrimitivesAdded);
        Assert.True(cost[1].SurvivorsOfTheCorpus > 1, "the composition law alone must NOT select - the constant stays free");
        Assert.Equal(1, cost[2].SurvivorsOfTheCorpus);
        Assert.Equal(1, cost[2].PrimitivesAdded);
        Assert.Equal(2, cost[3].PrimitivesAdded);
        Assert.Equal(3, cost[^1].PrimitivesAdded);
        Assert.Equal(0.0, cost[^1].SpreadAtTheTarget, 12);

        string verdict = ClockLawSelectionAudit.Verdict();
        Output.WriteLine(verdict);
        Output.WriteLine(ClockLawSelectionAudit.WhereItStands());

        Assert.StartsWith("REFUTED", verdict);
        Assert.Contains("STRUCTURALLY UNDERDETERMINED", verdict);
        Assert.Contains("OUTPUT: THE CLOCK SECTOR IS STRUCTURALLY UNDERDETERMINED", verdict);
        Assert.DoesNotContain("OUTPUT: UNIQUE", verdict);

        // the verdict is computed from live branches, and the audit asserts which branch fired
        var table = ClockLawSelectionAudit.SelectionTable();
        Assert.Equal(1, table.Count(t => t.Kind == "SELECTS UNIQUELY"));
        Assert.True(table.Count(t => t.Kind.StartsWith("NON-SELECTIVE")) >= 3);
        Assert.True(table.Count(t => t.Kind.StartsWith("REFUTED")) >= 1);
    }

    [Fact]
    public void Y_G_076_Diag()
    {
        PrintHeader("G_076 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(ClockLawSelectionAudit.OutputCatalogue());
        Output.WriteLine(ClockLawSelectionAudit.OutputSelection());
        Output.WriteLine(ClockLawSelectionAudit.OutputResiduals());
        Output.WriteLine(ClockLawSelectionAudit.OutputWeaker());
        Output.WriteLine(ClockLawSelectionAudit.OutputCost());
        Output.WriteLine(ClockLawSelectionAudit.OutputVerdict());
    }
}
