using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_041 — Substrate Dimension Audit.
///
/// QUESTION. AT needs the ring (d = 1) and, as G_033 established, the CUBE (d = 3). What about d = 2 and
/// d = 4 — and is d = 3 SELECTED or assumed? The family is D96^d, the d-fold tensor product of the same
/// 96-cell circulant, whose symmetry is the signed-permutation group B_d (order 2^d * d!; B_3 is the full cubic
/// group O_h of order 48). The d = 1 case is the PERIODIC ring, whose symmetry is dihedral of order 192.
///
/// ANSWER: **BOUNDARY — and the two halves point opposite ways.**
///
///  (1) D96^2 FAILS EXACTLY AS THE RING FAILS. The maximum irrep dimension is 2 for the ring and 2 for the
///      square torus, so neither can supply a dimension-3 sector: the cube is the MINIMAL working dimension, not
///      merely the chosen one. This strengthens G_033.
///  (2) BUT THE IRREP-SUPPLY ARGUMENT DOES NOT SELECT d = 3. The character inner products give the SAME
///      signature at every d >= 2 — vector irreducible (<V,V> = 1), traceless sector split into exactly two
///      pieces (<W,W> = 2, which is E_003's E + T2 at d = 3), no mixing (<V,W> = 0) — so the selector claim is
///      refuted: it selects d >= 3. Only the property that the vector is the LARGEST irrep breaks, and only
///      from d = 4.
///  (3) TWO INDEPENDENT ACCIDENTS DO PIN d = 3: the eps/Hodge accident (the antisymmetric rank-2 has the
///      vector's dimension only at d = 3) and the polarisation match (photon d - 1 against graviton
///      (d+1)(d-2)/2, equal only at 2 and 2; at d = 2 they are 1 and ZERO).
///  (4) AND THE CHOICE IS OBSERVABLE: the clock law is rho^(1/d), so the same density gives 129 415.634,
///      86 277.089 and 64 707.817 s/day at d = 2, 3, 4.
///
/// So: a DERIVED value with an OPEN window — the two-level structure the programme uses elsewhere.
/// </summary>
public class Y_G_041_Tests : ResearchTestBase
{
    public Y_G_041_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_041_TheLadderIsComputedAndBurnsideHolds()
    {
        // The floor: the group is constructed, not asserted, and the irrep spectrum obeys Burnside for every d.
        for (int d = 1; d <= SubstrateDimensionAudit.MaxDimension; d++)
        {
            Assert.Equal(SubstrateDimensionAudit.GroupOrder(d), SubstrateDimensionAudit.DistinctElements(d));
            Assert.True(SubstrateDimensionAudit.SpectrumIsExact(d),
                $"Burnside Sum d_i^2 = |G| failed at d = {d}");
        }

        // the orders on the ladder: B2 = 8, B3 = 48 (the cubic group), B4 = 384
        Assert.Equal(8, SubstrateDimensionAudit.GroupOrder(2));
        Assert.Equal(48, SubstrateDimensionAudit.GroupOrder(3));
        Assert.Equal(384, SubstrateDimensionAudit.GroupOrder(4));

        // the maximum irrep dimension: 2, 2, 3, 8, 20 ...
        Assert.Equal(2, SubstrateDimensionAudit.SuppliedIrrepDimension(1));   // the ring: dihedral, order 192
        Assert.Equal(2, SubstrateDimensionAudit.MaxIrrepDimension(2));
        Assert.Equal(3, SubstrateDimensionAudit.MaxIrrepDimension(3));
        Assert.Equal(8, SubstrateDimensionAudit.MaxIrrepDimension(4));
        Assert.Equal(20, SubstrateDimensionAudit.MaxIrrepDimension(5));
    }

    [Fact]
    public void Y_G_041_D96SquaredFailsExactlyAsTheRingFails()
    {
        // Neither the ring nor the square torus can host the dimension-3 sector G_033 requires.
        Assert.False(SubstrateDimensionAudit.SuppliesThreeDimensionalSector(1));
        Assert.False(SubstrateDimensionAudit.SuppliesThreeDimensionalSector(2));
        Assert.True(SubstrateDimensionAudit.SuppliesThreeDimensionalSector(3));

        // so the cube is the MINIMAL working dimension, not merely the chosen one
        int first = Enumerable.Range(1, SubstrateDimensionAudit.MaxDimension)
            .First(SubstrateDimensionAudit.SuppliesThreeDimensionalSector);
        Assert.Equal(3, first);
    }

    [Fact]
    public void Y_G_041_TheSeparationSignatureIsDimensionBlind()
    {
        // THE NEGATIVE RESULT: the signature the programme used does not distinguish d = 3 from d = 4.
        for (int d = 2; d <= SubstrateDimensionAudit.MaxDimension; d++)
        {
            Assert.Equal(1, SubstrateDimensionAudit.VectorSelfInner(d));
            Assert.Equal(2, SubstrateDimensionAudit.TracelessSelfInner(d));   // E_003's E + T2 at d = 3
            Assert.Equal(0, SubstrateDimensionAudit.VectorInsideTraceless(d));
            Assert.Equal(0, SubstrateDimensionAudit.VectorInsideAntisymmetric(d));
        }
        Assert.True(SubstrateDimensionAudit.SeparationSignatureIsDimensionBlind());

        // in particular d = 4 looks EXACTLY like d = 3 by this signature
        Assert.Equal(SubstrateDimensionAudit.VectorSelfInner(3), SubstrateDimensionAudit.VectorSelfInner(4));
        Assert.Equal(SubstrateDimensionAudit.TracelessSelfInner(3), SubstrateDimensionAudit.TracelessSelfInner(4));
    }

    [Fact]
    public void Y_G_041_TheVectorStopsBeingTheLargestIrrepAfterThree()
    {
        Assert.Equal(new[] { 2, 3 }, SubstrateDimensionAudit.DimensionsWhereVectorIsLargest());
        Assert.False(SubstrateDimensionAudit.VectorIsTheLargestIrrep(4));
        Assert.True(SubstrateDimensionAudit.MaxIrrepDimension(4) > 4);   // 8 > 4
    }

    [Fact]
    public void Y_G_041_TwoIndependentAccidentsPinTheDimension()
    {
        // the eps/Hodge accident: dim Lambda^2 = dim V only at d = 3
        Assert.Equal(new[] { 3 }, SubstrateDimensionAudit.DimensionsWithHodgeDuality());
        Assert.Equal(3, SubstrateDimensionAudit.AntisymmetricDimension(3));
        Assert.Equal(3, SubstrateDimensionAudit.VectorSpaceDimension(3));
        Assert.Equal(1, SubstrateDimensionAudit.AntisymmetricDimension(2));   // d = 2: a scalar
        Assert.Equal(6, SubstrateDimensionAudit.AntisymmetricDimension(4));

        // the polarisation match: photon d-1 against graviton (d+1)(d-2)/2, equal only at d = 3
        Assert.Equal(new[] { 3 }, SubstrateDimensionAudit.DimensionsWithMatchingPolarisations());
        Assert.Equal(2, SubstrateDimensionAudit.PhotonPolarisations(3));
        Assert.Equal(2, SubstrateDimensionAudit.GravitonPolarisations(3));
        Assert.Equal(1, SubstrateDimensionAudit.PhotonPolarisations(2));
        Assert.Equal(0, SubstrateDimensionAudit.GravitonPolarisations(2));    // no propagating graviton in 2+1D
        Assert.Equal(3, SubstrateDimensionAudit.PhotonPolarisations(4));
        Assert.Equal(5, SubstrateDimensionAudit.GravitonPolarisations(4));
    }

    [Fact]
    public void Y_G_041_TheChoiceIsObservableThroughTheClockLaw()
    {
        // rho^(1/d): the same density gives a different clock rate at every dimension.
        Assert.Equal(1.0 / 3.0, SubstrateDimensionAudit.ClockExponent(3), 12);
        Assert.Equal(86277.089, SubstrateDimensionAudit.RequiredDimensionClockRate(), 3);
        Assert.Equal(129415.634, SubstrateDimensionAudit.ClockRatePerDay(2), 3);
        Assert.Equal(64707.817, SubstrateDimensionAudit.ClockRatePerDay(4), 3);
        Assert.True(SubstrateDimensionAudit.TheChoiceIsObservable());

        // and the mode counts the choice costs
        Assert.Equal(9216L, SubstrateDimensionAudit.ModeCount(2));
        Assert.Equal(884736L, SubstrateDimensionAudit.ModeCount(3));
        Assert.Equal(84934656L, SubstrateDimensionAudit.ModeCount(4));
    }

    [Fact]
    public void Y_G_041_TheVerdictIsBoundaryOnTheWindow()
    {
        Assert.Equal("BOUNDARY", SubstrateDimensionAudit.Verdict());

        // derived: the supply argument, the Hodge accident, the polarisation match
        var derived = SubstrateDimensionAudit.DerivedSelectors();
        Assert.Contains("the eps / Hodge accident", derived);
        Assert.Contains("the polarisation match", derived);
        Assert.Contains("irrep dimension 3 is supplied", derived);

        // refuted as selectors: the irrep-supply ARGUMENT, and the exclusion of d = 4
        var refuted = SubstrateDimensionAudit.RefutedSelectors();
        Assert.Contains("...and that SELECTS d = 3", refuted);
        Assert.Contains("d = 4 is excluded", refuted);
    }

    [Fact]
    public void Y_G_041_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_041 — Substrate Dimension Audit: is d = 3 selected, or assumed?");

        sb.AppendLine("QUESTION. AT needs the ring (d = 1) and, as G_033 established, the CUBE (d = 3). Do d = 2");
        sb.AppendLine("and d = 4 change anything the programme should reckon with — and is d = 3 SELECTED?");
        sb.AppendLine();
        sb.AppendLine("CANDIDATES  irrep-dimension supply, sector separation, the eps/Hodge accident, the");
        sb.AppendLine("            polarisation match, the clock-law exponent, mode economy");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The family is D96^d: the d-fold tensor product of the SAME 96-cell circulant C96(1..6).");
        sb.AppendLine("     Its symmetry is the signed-permutation group B_d = C2^d : S_d of order 2^d d! — B_3 is the");
        sb.AppendLine("     full cubic group O_h of order 48, the group E_003/E_004/G_033 used.");
        sb.AppendLine("  2. d = 1 is NOT B_1: AT's ring is periodic, so its symmetry is dihedral of order 192, whose");
        sb.AppendLine("     irrep budget E_003 computed (4 one-dimensional + 47 two-dimensional -> maximum 2).");
        sb.AppendLine("  3. The requirement inherited from G_033 is a dimension-3 sector: the photon needs T1(3) and the");
        sb.AppendLine("     metric's trace-free part needs T2(3).");
        sb.AppendLine("  4. The clock law is dtau/dt = rho^(1/d) (G_016b), so the exponent IS the substrate dimension and");
        sb.AppendLine("     G_016b's 86 277.089 s/day is a d = 3 number.");
        sb.AppendLine("  5. Everything is computed: the groups are constructed, the irrep spectra derived by the");
        sb.AppendLine("     pair-of-partitions classification, and Burnside's sum rule verified rather than assumed.");
        sb.AppendLine();

        PrintHeader(SubstrateDimensionAudit.OutputLadder());
        PrintHeader(SubstrateDimensionAudit.OutputSignature());
        PrintHeader(SubstrateDimensionAudit.OutputSelectors());
        PrintHeader(SubstrateDimensionAudit.OutputImpact());

        Output.WriteLine(sb.ToString());
    }
}
