using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.ThreeDimensionalityDependencyAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_042 — Three-Dimensionality Dependency Audit.
///
/// QUESTION. Are the known selectors of d = 3 independent, or are they the same structure viewed differently —
/// one root mechanism, or several?
///
/// Inputs: (1) rotation self-duality d(d-1)/2 = d; (2) Hodge duality dim(Lambda^2) = dim(V); (3) the
/// photon/graviton polarisation equality d - 1 = (d+1)(d-2)/2; (4) the D96^d representation structure; (5) the
/// clock exponent rho^(1/d).
///
/// ANSWER: **REDUNDANT — ONE ROOT MECHANISM, and the count of independent selectors is 1.**
///
///  (1) THREE OF THE FIVE ARE THE SAME EQUATION, identically in d — not merely at the shared root. The
///      decisive identity: graviton polarisations = dim(Lambda^2) - 1 = dim(so(d)) - 1 for EVERY d, against
///      photons = dim(V) - 1. Subtract 1 from both and "the polarisations are equal" IS "dim(Lambda^2) = dim(V)".
///  (2) THE FOURTH IS STRICTLY WEAKER IN BOTH READINGS: a 3-dim irrep exists for d >= 3, the vector is the
///      largest irrep for d <= 3; their conjunction reproduces the root, which is why it looked like a second
///      mechanism — but the root implies it, so it is redundant too.
///  (3) THE FIFTH IS NOT A SELECTOR: the clock law holds at every d. It is the law that makes the choice
///      OBSERVABLE.
///  (4) THE ACCIDENTS BELONG TO DIFFERENT DIMENSIONS (checked): bivectors collapse to a scalar at d = 2, ARE
///      the vectors at d = 3, split self-dual at d = 4, and the cross product returns at d = 7.
///  (5) THIS REFINES G_041, which called the Hodge accident and the polarisation match two independent
///      accidents. One condition, two derivations: one independent reason, not two. G_041's BOUNDARY verdict
///      is unchanged.
/// </summary>
public class Y_G_042_Tests : ResearchTestBase
{
    public Y_G_042_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_042_TheIdentitiesMakeThePolarisationRouteTheHodgeRoute()
    {
        // the floor: both polarisation counts are the dimension of a tensor space, minus one
        // ... and the family the fourth selector is about is built from the ring itself
        Assert.Equal(96, SubstrateDimensionAudit.D96Cells);
        Assert.True(PhotonIsVectorMinusOne());
        Assert.True(GravitonIsRotationAlgebraMinusOne());
        Assert.True(RotationsAreTheAntisymmetricSquare());
        for (int d = 1; d <= MaxDimension; d++)
        {
            Assert.Equal(d - 1, PhotonPolarisations(d));
            Assert.Equal(AntisymmetricSquare(d) - 1, GravitonPolarisations(d));
            Assert.Equal(RotationGenerators(d), AntisymmetricSquare(d));
        }

        // and the difference of the two counts vanishes EXACTLY where dim Lambda^2 = dim V
        for (int d = 1; d <= MaxDimension; d++)
        {
            bool equal = PhotonPolarisations(d) == GravitonPolarisations(d);
            Assert.Equal(AntisymmetricSquare(d) == VectorDimension(d), equal);
        }
    }

    [Fact]
    public void Y_G_042_TheThreeGeometricSelectorsAreOneEquation()
    {
        Assert.Equal(new[] { 3 }, SolutionSet(SelfDuality));
        Assert.Equal(new[] { 3 }, SolutionSet(Hodge));
        Assert.Equal(new[] { 3 }, SolutionSet(Polarisations));

        Assert.True(TheGeometricConditionsAreOneEquation());
        Assert.True(ThePolarisationRouteIsTheHodgeRoute());
        Assert.Equal("d(d-3) = 0", RootEquation());
        Assert.Equal(new[] { 0, 3 }, RootSolutions());
    }

    [Fact]
    public void Y_G_042_TheRepresentationRouteIsStrictlyWeakerButItsConjunctionReproducesTheRoot()
    {
        // the supply reading admits every d >= 3 ...
        Assert.Equal(Enumerable.Range(3, 10).ToArray(), SolutionSet(SuppliesThreeDimensionalIrrep));
        // ...and the maximality reading admits 1, 2 and 3
        Assert.Equal(new[] { 1, 2, 3 }, SolutionSet(VectorIsTheLargestIrrep));

        // their conjunction reproduces the root exactly — which is why it LOOKED like a second mechanism
        Assert.True(TheRepresentationConjunctionReproducesTheRoot());
    }

    [Fact]
    public void Y_G_042_TheClockIsNotASelector()
    {
        // the clock law holds at every dimension, so it constrains nothing
        Assert.Equal(Enumerable.Range(1, MaxDimension).ToArray(), SolutionSet(ClockLawHolds));
        var row = Classify().Single(r => r.Selector.Contains("clock"));
        Assert.Equal("REDUNDANT", row.Status);
    }

    [Fact]
    public void Y_G_042_TheClassificationIsOneRootAndEverythingElseRedundant()
    {
        Assert.Equal(1, IndependentRootCount());
        Assert.Single(IndependentSelectors());
        Assert.Contains("rotation self-duality", IndependentSelectors()[0]);

        // the two restatements of the root, the two weaker consequences, and the non-selector
        Assert.Equal(3, RedundantSelectors().Length);
        Assert.Equal(2, DerivedSelectors().Length);
        Assert.Equal("REDUNDANT", Verdict());

        // every implication in the graph is derived from the solution sets, never asserted
        var graph = DependencyGraph();
        Assert.NotEmpty(graph);
        Assert.All(graph, e => Assert.True(e.Implies));
        // the root implies BOTH representation readings, and neither implies it
        Assert.Contains(graph, e => e.A.Contains("self-duality") && e.B.Contains("3-dim irrep"));
        Assert.Contains(graph, e => e.A.Contains("self-duality") && e.B.Contains("largest irrep"));
        Assert.DoesNotContain(graph, e => e.A.Contains("3-dim irrep") && e.B.Contains("self-duality"));
    }

    [Fact]
    public void Y_G_042_TheAccidentsBelongToDifferentDimensions()
    {
        Assert.Equal(new[] { 2 }, DimensionsWhereBivectorsAreScalars());
        Assert.Equal(new[] { 3 }, DimensionsWhereBivectorsAreVectors());
        Assert.Contains(4, DimensionsAdmittingASelfDualSplit());
        Assert.DoesNotContain(3, DimensionsAdmittingASelfDualSplit());

        // the family of special dimensions is real and each membership differs —
        // but only one membership selects d = 3, and it does so once
        Assert.NotEqual(DimensionsWhereBivectorsAreScalars()[0], DimensionsWhereBivectorsAreVectors()[0]);
    }

    [Fact]
    public void Y_G_042_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_042 — Three-Dimensionality Dependency Audit: one root, or several mechanisms?");

        sb.AppendLine("QUESTION. Are the known selectors of d = 3 independent, or the same structure viewed");
        sb.AppendLine("differently?  INPUTS  1 rotation self-duality d(d-1)/2 = d | 2 Hodge dim(L2) = dim V |");
        sb.AppendLine("3 polarisation equality d-1 = (d+1)(d-2)/2 | 4 the D96^d representation structure |");
        sb.AppendLine("5 the clock exponent rho^(1/d).");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A selector is a predicate on the dimension d; its solution set is computed over");
        sb.AppendLine("     d = 1..12 by evaluating the predicate, never by quoting an algebraic root.");
        sb.AppendLine("  2. A implies B exactly when S(A) is contained in S(B) — the containment IS the");
        sb.AppendLine("     dependency, so the graph is derived rather than drawn.");
        sb.AppendLine("  3. Selectors with EQUAL solution sets are one mechanism: the first is the representative,");
        sb.AppendLine("     the rest are restatements. A selector that holds at every d is not a selector.");
        sb.AppendLine("  4. Input 4 is tested in both of its readings, because G_041 needed both halves.");
        sb.AppendLine();

        PrintHeader(ThreeDimensionalityDependencyAudit.OutputIdentities());
        PrintHeader(ThreeDimensionalityDependencyAudit.OutputGraph());
        PrintHeader(ThreeDimensionalityDependencyAudit.OutputClassification());

        Output.WriteLine(sb.ToString());
    }
}
