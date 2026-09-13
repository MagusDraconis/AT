using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.FluxPopulationAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_013 - Flux Population Audit (group E - Electromagnetism).
///
/// QUESTION. What mechanism populates the ALLOWED balanced flux sectors, given E_012's two findings - a single fluxon
/// is forbidden, a balanced pair is allowed? Requirements: local, gauge compatible, survives the continuum limit, no
/// new primitive. Candidates: occupancy rearrangement, defect pairs, boundary conditions, actualization transitions,
/// spectral transitions. Measure: sector population probability, sector stability, flux lifetime.
///
/// ANSWER: **BOUNDARY for the population, with the mechanism's FORM DERIVED - the two levels kept apart as the
/// D_028/D_040 rule requires.**
///
///  (1) THE PAIRING IS FORCED AND THE MEASUREMENT IS EXACT: one link phase incremented changes its adjacent plaquettes
///      in balanced pairs, never singly, so P(single) = 0 exactly and P(pair) = 1. E_012's forbidden case is not merely
///      disallowed - it is unreachable by any local move.
///  (2) STABILITY IS EXACT AND IS NOT A LIFETIME: gauge invariance residual 0, a single member cannot decay (residual
///      pi), the pair can annihilate (residual 0).
///  (3) THE LIFETIME IS UNDEFINED: the clock law's sensitivity to the flux is zero while its sensitivity to the
///      organisation is not (the control) - no potential, so every flux value is degenerate.
///  (4) THE ACTIVATION IS THE BOUNDARY: the update rule's spatial part is exactly zero (E_009, re-measured), the
///      spectral census is zero against a non-zero control, and the occupancy's gradient limit has no plaquette
///      content at all.
/// </summary>
public class Y_E_013_Tests : ResearchTestBase
{
    public Y_E_013_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_013_EveryLocalMoveIsPairCreatingAndBalanced()
    {
        var census = LocalMoveCensus();
        Assert.Equal(36, census.Length);

        // a local move touches plaquettes in pairs - never one
        Assert.True(MinimumPlaquettesChangedByALocalMove() >= 2,
            $"minimum changed was {MinimumPlaquettesChangedByALocalMove()}");
        Assert.Equal(0, SinglePlaquetteMoves());

        // and what it changes sums to exactly zero, so it can only be balanced
        Assert.True(EveryLocalMoveIsBalanced());
        Assert.True(LargestSignedSumOfALocalMove() < 1e-12);

        // the magnitude of the change is the increment itself
        Assert.True(LargestSinglePlaquetteChange() > 3.0);
        Assert.True(EveryLocalMoveIsPairCreating());
    }

    [Fact]
    public void Y_E_013_PopulationProbabilityIsComputedNotAssumed()
    {
        Assert.Equal(0.0, SingleFluxonPopulationProbability());
        Assert.Equal(1.0, BalancedPairPopulationProbability());
        Assert.Equal("defect pairs", PopulatingMechanism());

        // the pair pattern is E_012's own classification, reused
        Assert.Equal(Math.PI, FluxExcitationAudit.SingleHalfTurnResidual(), 9);
        Assert.True(FluxExcitationAudit.BalancedPairResidual() < 1e-12);
    }

    [Fact]
    public void Y_E_013_ThePairIsStableButHasNoLifetime()
    {
        // stability: gauge compatible, and a member cannot decay alone
        Assert.True(ThePairIsGaugeCompatible());
        Assert.True(PairGaugeInvarianceResidual() < 1e-12);
        Assert.True(AMemberCannotDecayAlone());
        Assert.Equal(Math.PI, SingleMemberDecayResidual(), 9);
        Assert.True(ThePairCanAnnihilate());

        // and its amplitude does not scale away
        Assert.True(ThePairSurvivesEverySize());
        Assert.All(PairAmplitudeSeries(), t => Assert.Equal(Math.PI, t.MaxFlux, 9));

        // lifetime: undefined - the flux is invisible to the only law that could time it
        Assert.True(TheFluxHasNoLifetime());
        Assert.True(ClockLawFluxSensitivity() < 1e-15);
        Assert.True(ClockLawOrganisationSensitivity() > 0.05,
            $"the control read {ClockLawOrganisationSensitivity():F6}");
    }

    [Fact]
    public void Y_E_013_TheActivationIsMissingInThreeIndependentWays()
    {
        // (a) the update rule's own field strength is purely electric
        var (electric, magnetic) = UpdateRuleSectors();
        Assert.True(electric > 1e-3);
        Assert.True(magnetic < 1e-12, $"spatial part {magnetic:E3}");

        // (b) the spectral sector and the link sector are disjoint
        Assert.Equal(0, AtMembersCouplingASpectralIndexToALinkPhase());
        Assert.True(AtMembersTouchingALinkField() > 0,
            "the control found no link-field members at all");
        Assert.True(AtMembersTouchingASpectralIndex() > 0,
            "the control found no spectral members at all");

        // (c) the occupancy's gradient limit has no plaquette content
        Assert.True(ExactGradientPlaquetteResidual() < 1e-12);
        Assert.True(OccupancyScalingExponent() > 1.5);
    }

    [Fact]
    public void Y_E_013_ThreeAreRefutedOneIsDerivedOneIsBoundaryAndTheVerdictIsBoundary()
    {
        Assert.Equal(5, Candidates().Length);
        Assert.Equal(3, RefutedCandidates().Length);
        Assert.Contains("occupancy rearrangement", RefutedCandidates());
        Assert.Contains("actualization transitions", RefutedCandidates());
        Assert.Contains("spectral transitions", RefutedCandidates());

        // the two-level reading: the FORM of any mechanism is derived, the POPULATION is a boundary
        Assert.Equal("DERIVED", FormVerdict());
        Assert.Equal("BOUNDARY", PopulationVerdict());
        Assert.Equal("BOUNDARY", Verdict());
    }

    [Fact]
    public void Y_E_013_TheRequirementsHold()
    {
        var checks = RequirementCheck();
        Assert.Equal(4, checks.Length);
        Assert.Equal(new[] { "local", "gauge compatible", "survives continuum limit", "no new primitive" },
            checks.Select(c => c.Requirement).ToArray());
        Assert.Equal(2, Support());
    }

    [Fact]
    public void Y_E_013_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_013 - Flux Population Audit: what populates the allowed balanced flux sectors?");

        sb.AppendLine("QUESTION. What mechanism populates the ALLOWED balanced flux sectors?");
        sb.AppendLine("KNOWN        single fluxon forbidden; balanced pair allowed (E_012)");
        sb.AppendLine("CANDIDATES   occupancy rearrangement | defect pairs | boundary conditions |");
        sb.AppendLine("             actualization transitions | spectral transitions");
        sb.AppendLine("REQUIREMENTS local | gauge compatible | survives continuum limit | no new primitive");
        sb.AppendLine("MEASURE      sector population probability | sector stability | flux lifetime");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The sector content is the reduced plaquette flux - what a holonomy measurement sees.");
        sb.AppendLine("  2. A local move is a single link phase at a single site, increment by an arbitrary amount.");
        sb.AppendLine("  3. The pair is realised on the lattice as a half-turn link, giving +pi and -pi on two adjacent");
        sb.AppendLine("     plaquettes - E_012's balanced pair, reused rather than redefined.");
        sb.AppendLine("  4. Lifetime needs a potential; the only AT law that could supply one is the clock law.");
        sb.AppendLine("  5. Deterministic throughout; no randomness.");
        sb.AppendLine();

        PrintHeader(OutputPairCreation());
        PrintHeader(OutputStability());
        PrintHeader(OutputCandidates());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
