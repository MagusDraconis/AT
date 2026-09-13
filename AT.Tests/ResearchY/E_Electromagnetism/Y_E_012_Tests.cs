using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.FluxExcitationAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_012 - Flux Excitation Audit (group E - Electromagnetism).
///
/// QUESTION. What AT mechanism POPULATES a non-trivial flux sector? Candidates: occupancy defects, topological
/// defects, winding sectors, boundary conditions, actualization transitions. Requirements: create F != 0, survive the
/// continuum limit, need no new primitive.
///
/// ANSWER: **BOUNDARY - nothing inside AT populates the sector; the population is an ASSIGNMENT, globally
/// constrained, and the smallest non-trivial assignment is a balanced pair whose amplitude does not scale away.**
///
///  (1) THE ORGANISATION CANNOT: the occupancy route is re-measured here, falling as a^2.
///  (2) THE CONTENT IS AN ASSIGNMENT WITH A GLOBAL CONSTRAINT: summing over the WHOLE torus counts every link twice,
///      so the reduced fluxes must sum to a multiple of 2 pi - a SINGLE half-turn flux violates this, a BALANCED PAIR
///      obeys it exactly.
///  (3) THE PAIR'S AMPLITUDE IS pi AT EVERY SIZE - the one requirement the occupancy route fails.
///  (4) WINDING SECTORS and ACTUALIZATION TRANSITIONS are refuted by earlier results; TOPOLOGICAL DEFECTS and
///      BOUNDARY CONDITIONS are the two non-mechanisms, and the latter is the answer.
///  (5) A FIRST-DRAFT CLAIM WAS WITHDRAWN: the product of holonomies around a SLICE is the cycle's holonomy, not the
///      identity - the identity holds for the whole torus. The audit measures the right one.
/// </summary>
public class Y_E_012_Tests : ResearchTestBase
{
    public Y_E_012_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_012_TheWholeTorusConstraintForbidsTheSingleFluxon()
    {
        // the identity holds for the whole torus
        Assert.True(WholeTorusProductDistance() < 1e-12);

        // but NOT for a slice - its boundary is a non-contractible cycle
        Assert.True(SliceProductDistance() > 0.1,
            $"a slice's product was {SliceProductDistance():F6} from the identity");

        // the whole-torus sum forbids a single half-turn flux and allows a balanced pair
        Assert.True(TheSingleHalfTurnIsForbidden());
        Assert.True(SingleHalfTurnResidual() > 0.1);
        Assert.True(TheBalancedPairIsAllowed());
        Assert.True(BalancedPairResidual() < 1e-12);
    }

    [Fact]
    public void Y_E_012_TheOccupancyRouteCannotPopulateTheSector()
    {
        var series = OccupancySeries();
        Assert.Equal(4, series.Length);
        Assert.True(series[0].MaxFlux > series[^1].MaxFlux);
        Assert.True(TheOccupancyRouteScalesAway());
        Assert.True(OccupancyScalingExponent() > 1.5,
            $"occupancy exponent {OccupancyScalingExponent():F2}");
    }

    [Fact]
    public void Y_E_012_ThePairAmplitudeDoesNotScaleAway()
    {
        var series = PairAmplitudeSeries();
        Assert.Equal(4, series.Length);
        Assert.All(series, t => Assert.Equal(Math.PI, t.MaxFlux, 9));
        Assert.True(ThePairAmplitudeDoesNotScaleAway());

        // and the occupancy route over the same sizes falls
        var occupancy = OccupancySeries();
        Assert.True(series[0].MaxFlux > 100.0 * occupancy[0].MaxFlux);
        Assert.True(series[^1].MaxFlux > 1000.0 * occupancy[^1].MaxFlux);
    }

    [Fact]
    public void Y_E_012_ThreeCandidatesAreRefutedAndTwoAreBoundary()
    {
        Assert.Equal(5, Candidates().Length);
        Assert.Equal(3, RefutedCandidates().Length);
        Assert.Contains("occupancy defects", RefutedCandidates());
        Assert.Contains("winding sectors", RefutedCandidates());
        Assert.Contains("actualization transitions", RefutedCandidates());

        Assert.Equal(2, BoundaryCandidates().Length);
        Assert.Contains("topological defects", BoundaryCandidates());
        Assert.Contains("boundary conditions", BoundaryCandidates());
        Assert.Equal("boundary conditions", Answer());
    }

    [Fact]
    public void Y_E_012_TheRequirementsHoldAndTheAnswerIsBoundary()
    {
        var checks = RequirementCheck();
        Assert.Equal(3, checks.Length);
        Assert.Equal("create F != 0", checks[0].Requirement);
        Assert.Contains("survive continuum limit", checks.Select(c => c.Requirement));
        Assert.Contains("no new primitive", checks.Select(c => c.Requirement));
        Assert.True(NoNewPrimitiveNeeded());

        Assert.Equal("BOUNDARY", Verdict());
    }

    [Fact]
    public void Y_E_012_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_012 - Flux Excitation Audit: what populates a non-trivial flux sector?");

        sb.AppendLine("QUESTION. What AT mechanism populates a non-trivial flux sector?");
        sb.AppendLine("CANDIDATES   occupancy defects | topological defects | winding sectors |");
        sb.AppendLine("             boundary conditions | actualization transitions");
        sb.AppendLine("REQUIREMENTS create F != 0 | survive continuum limit | no new primitive");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The flux content is what a holonomy measurement sees, i.e. the reduced plaquette flux.");
        sb.AppendLine("  2. A flux pattern is realized by a link field whose increments produce it, so every pattern tested");
        sb.AppendLine("     is a real configuration rather than a table of numbers.");
        sb.AppendLine("  3. The whole-torus constraint is the product of all plaquette holonomies, which counts every link");
        sb.AppendLine("     twice with opposite signs - a slice is NOT the whole torus, and a first draft confused them.");
        sb.AppendLine("  4. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputConstraint());
        PrintHeader(OutputSurvival());
        PrintHeader(OutputCandidates());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
