using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.FieldStrengthOriginAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_007 - Field Strength Origin Audit (group E - Electromagnetism).
///
/// QUESTION. Can the D96^3 edge connection that E_006 derived produce a NON-ZERO field strength? Construct closed
/// plaquette loops, a discrete curvature and a discrete curl; measure F != 0; compare a pure gauge configuration
/// against a non-trivial loop; detect the FIRST non-trivial field quantity.
///
/// ANSWER: **BOUNDARY - and both halves of the answer are exact.**
///
///  (1) THE CONNECTION ALONE PRODUCES ZERO. E_006's edge connection is a product of lattice differences, and
///      differences COMMUTE on a uniform lattice, so its curvature vanishes exactly. The link-language statement is
///      the same: a gradient link field has an exactly vanishing plaquette holonomy, because the four contributions
///      telescope. The edge connection is FLAT.
///  (2) BUT THE CUBE IS THE FIRST SUBSTRATE THAT CAN CARRY A FIELD STRENGTH AT ALL: the ring has ZERO elementary
///      plaquettes (one-dimensional), the cube has 3L^3 = 2654208 at L = 96 inside an independent cycle space of
///      1769473. This audit also CORRECTS E_005, which reported 3L^2 = 27648 plaquettes while its own cycle count
///      used the correct 3L^3 edges.
///  (3) THE FIRST NON-TRIVIAL QUANTITY IS THE QUANTISED PLAQUETTE FLUX: F = -f for a linear link field, with
///      periodicity forcing f = 2 pi n / L, so the smallest non-zero field strength is 2 pi / 96 = 0.065449847 -
///      exactly the phase quantum AT already has.
///  (4) THE APPARATUS IS EXACT: for an Abelian connection the discrete curvature IS the antisymmetrised difference,
///      the curvature is gauge invariant, and discrete Bianchi holds because the six faces of a cube telescope.
/// </summary>
public class Y_E_007_Tests : ResearchTestBase
{
    public Y_E_007_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_007_TheConnectionItselfIsFlat()
    {
        // differences commute - exhaustively at small size, sampled on the full torus
        Assert.True(CommutatorResidual(8) < 1e-12);
        Assert.True(CommutatorResidual(L, 13) < 1e-12);
        Assert.True(DifferencesCommute());

        // and a gradient link field has an exactly vanishing field strength
        Assert.True(PureGaugeFieldStrength() < 1e-12, $"pure gauge gave {PureGaugeFieldStrength():E3}");
    }

    [Fact]
    public void Y_E_007_BothHalvesOfTheAnswerAreExact()
    {
        // the first non-trivial quantity exists, with the value the audit reports
        Assert.Equal(0.0, MinimalNonZeroFlux() - 2.0 * Math.PI / 96.0, 12);
        Assert.Equal(0.065449847, MinimalNonZeroFlux(), 9);
        Assert.Equal(MinimalNonZeroFlux(), FluxPerPlaquette(1, L), 12);

        // the link field really is periodic for the quantised flux - hence the quantisation
        Assert.True(PeriodicityResidual(1, L) < 1e-12);
        Assert.True(PeriodicityResidual(2, L) < 1e-12);

        // and the field strength is genuinely non-zero, sampled across the full torus
        Assert.True(MaxFieldStrength(FluxField(1, L), L, 7) > 0.06);
        Assert.Equal(MinimalNonZeroFlux(), MaxFieldStrength(FluxField(1, L), L, 7), 12);
    }

    [Fact]
    public void Y_E_007_TheCubeIsTheFirstSubstrateThatCanCarryOne()
    {
        // the ring has no 2-cycle at all
        Assert.Equal(0, RingElementaryPlaquettes());

        // the cube: one plaquette per site and per orientation pair, inside the cycle space
        Assert.Equal(884736, Sites(L));
        Assert.Equal(2654208, Edges(L));
        Assert.Equal(2654208, ElementaryPlaquettes(L));
        Assert.Equal(1769473, IndependentLoops(L));
        Assert.True(TheCubeIsTheFirstSubstrateWithPlaquettes());

        // the corrected E_005 figure: 3L^3, not 3L^2
        Assert.Equal(2654208, PropagationOriginAudit.CubeElementaryPlaquettes());
        Assert.Equal(PropagationOriginAudit.CubeIndependentLoops(), IndependentLoops(L));
    }

    [Fact]
    public void Y_E_007_TheCurvatureIsTheCurlAndIsGaugeInvariant()
    {
        // for an Abelian connection the four link phases add: the curvature IS the antisymmetrised difference
        Assert.True(CurlVersusHolonomyResidual(FluxField(1, 8), 8) < 1e-12);
        Assert.True(CurlVersusHolonomyResidual(PureGaugeField(8), 8) < 1e-12);

        // and it does not move under a site-dependent gauge transformation
        Assert.True(GaugeInvarianceResidual() < 1e-12, $"gauge residual {GaugeInvarianceResidual():E3}");
    }

    [Fact]
    public void Y_E_007_DiscreteBianchiHoldsForEveryConfiguration()
    {
        Assert.True(BianchiResidualOnFlux() < 1e-12);
        Assert.True(BianchiResidualOnPureGauge() < 1e-12);
    }

    [Fact]
    public void Y_E_007_TheVerdictIsBoundaryAndTheFirstQuantityIsNamed()
    {
        var measurements = Measurements();
        Assert.Equal(8, measurements.Length);
        Assert.Contains(measurements, m => m.Status.Contains("NON-ZERO", StringComparison.Ordinal));
        Assert.Contains(measurements, m => m.Status.Contains("ZERO - the connection is flat", StringComparison.Ordinal));
        Assert.Contains("2 pi n / L", FirstNonTrivialQuantity(), StringComparison.Ordinal);
        Assert.Equal("BOUNDARY", Verdict());
    }

    [Fact]
    public void Y_E_007_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_007 - Field Strength Origin Audit: can the edge connection produce F != 0?");

        sb.AppendLine("QUESTION. Can the D96^3 edge connection that E_006 derived produce a NON-ZERO field strength?");
        sb.AppendLine("CONSTRUCT   closed plaquette loops, discrete curvature, discrete curl");
        sb.AppendLine("MEASURE     F != 0 ?      COMPARE   pure gauge versus a non-trivial loop");
        sb.AppendLine("GOAL        detect the first non-trivial field quantity");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The connection is taken as E_006 left it: the product of lattice differences, three factors.");
        sb.AppendLine("  2. A field strength is the holonomy of a CLOSED PLAQUETTE, so it needs two independent");
        sb.AppendLine("     directions - which is exactly what a one-dimensional substrate cannot supply.");
        sb.AppendLine("  3. The connection is Abelian here, so the four link phases of a loop add and the discrete");
        sb.AppendLine("     curvature is the antisymmetrised difference EXACTLY rather than to leading order.");
        sb.AppendLine("  4. Quantisation follows from the link being a phase on a closed torus: exp(i A(L)) = exp(i A(0)).");
        sb.AppendLine("  5. Exhaustive scans run at small size, where every site is visited, and the full L = 96 torus is");
        sb.AppendLine("     sampled on a fixed stride; the algebra is local, so the sample is representative.");
        sb.AppendLine();

        PrintHeader(OutputConstruction());
        PrintHeader(OutputMeasurements());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
