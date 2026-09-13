using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.SectorPopulationPrincipleAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_016 - Sector Population Principle Audit (group E - Electromagnetism).
///
/// QUESTION. Do any EXISTING AT quantities break the flat sector measure that E_015 established? Candidates: occupancy
/// free room, multiplicity structure, D96 hierarchy, compression laws, actualization rate. Goal: find the first
/// non-flat weighting WITHOUT introducing new primitives.
///
/// ANSWER: **REFUTED - no existing AT quantity breaks the flat measure, and the audit validated its detector before
/// believing it.** The search runs first on a synthetic breaker (a non-uniform local potential) and on a second one (a
/// recipe-phase bridge), and both come out non-flat; the same machinery then finds the five AT candidates flat. A
/// NEAREST MISS is recorded rather than hidden: a coarse PHASE observable's conditional distribution DOES differ
/// across sectors, and what is missing is an AT quantity that uses it.
/// </summary>
public class Y_E_016_Tests : ResearchTestBase
{
    public Y_E_016_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_016_TheDetectorIsValidatedBeforeItIsBelieved()
    {
        // control: the trivial weight is flat, and a potential uniform in the phase changes nothing
        Assert.True(FlatControlSpread() < 1e-12);
        Assert.True(AUniformPotentialIsFlat(1.0));

        // positive control one: a NON-uniform local potential breaks flatness at once
        Assert.True(LargestPotentialSpread() > 1e-6,
            $"largest potential spread {LargestPotentialSpread():E3}");

        // positive control two: a recipe-phase bridge breaks the conditional marginal
        Assert.True(CoupledRecipeResidual() > 1e-6,
            $"coupled residual {CoupledRecipeResidual():E3}");

        Assert.True(TheDetectorIsSensitive());
    }

    [Fact]
    public void Y_E_016_TheWithdrawnPredictionAndWhereTheRealNearestMissIs()
    {
        // the withdrawn prediction, measured: the coarse observable is FORCED flat by the half-period symmetry
        var table = ConditionalPhaseObservable();
        Assert.Equal(4, table.Length);
        Assert.True(TheCoarseObservableIsForcedFlat(), $"conditional spread {ConditionalSpread():E3}");
        Assert.True(ConditionalSpread() < 1e-12);

        // the real nearest miss is a WEIGHT rather than an observable
        Assert.True(TheNearestMissIsAWeightNotAnObservable());
        Assert.True(LargestPotentialSpread() > 1e-6);
    }

    [Fact]
    public void Y_E_016_OccupancyFreeRoomIsSectorIndependent()
    {
        var rooms = OccupancyFreeRoom();
        Assert.Equal(4, rooms.Length);
        Assert.All(rooms, r => Assert.True(r.FreeRoom > 0));
        Assert.True(OccupancyFreeRoomSpread() < 1e-12,
            $"spread over the sectors {OccupancyFreeRoomSpread():E3}");
        Assert.True(ProductFactorisationResidual() < 1e-15);
        Assert.True(TheRecipeSectorAndThePhaseSectorAreDecoupled());

        // the empirical input to the factorisation: the live census, reused from E_013
        Assert.Equal(0, FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase());
    }

    [Fact]
    public void Y_E_016_MultiplicityIsSectorSymmetricAndTheActualizationCannotReadTheLabel()
    {
        Assert.True(MultiplicitySpread() < 1e-12);
        Assert.True(TheMultiplicityIsSectorSymmetric());

        var (spatial, timeLike) = ActualizationParts();
        Assert.True(spatial < 1e-15, $"spatial part {spatial:E3}");
        Assert.True(timeLike > 1e-3);
        Assert.True(TheActualizationRateIsSectorBlind());
        Assert.True(ActualizationRateSpread() < 1e-12);
    }

    [Fact]
    public void Y_E_016_D96HierarchyAndCompressionLawsNeverReadThePhase()
    {
        var inputs = HierarchyAndCompressionInputs();
        Assert.Equal(6, inputs.Length);
        Assert.All(inputs, t => Assert.False(t.ReadsThePhase, $"{t.Quantity} reads the phase"));
        Assert.True(NoAtQuantityBreaksFlatness());
    }

    [Fact]
    public void Y_E_016_AllFiveCandidatesAreRefutedAndEveryBreakerWouldBeANewPrimitive()
    {
        Assert.Equal(5, Candidates().Length);
        Assert.Equal(5, RefutedCandidates().Length);
        Assert.Contains("occupancy free room", RefutedCandidates());
        Assert.Contains("multiplicity structure", RefutedCandidates());
        Assert.Contains("D96 hierarchy", RefutedCandidates());
        Assert.Contains("compression laws", RefutedCandidates());
        Assert.Contains("actualization rate", RefutedCandidates());

        Assert.True(EveryBreakerWouldBeANewPrimitive());
        Assert.Equal("REFUTED", Verdict());
    }

    [Fact]
    public void Y_E_016_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_016 - Sector Population Principle Audit: does any AT quantity break the flat measure?");

        sb.AppendLine("QUESTION. Do any EXISTING AT quantities break the flat sector measure P(n)?");
        sb.AppendLine("KNOWN        E_015: P(n) flat - the sector is completely free");
        sb.AppendLine("CANDIDATES   occupancy free room | multiplicity structure | D96 hierarchy |");
        sb.AppendLine("             compression laws | actualization rate");
        sb.AppendLine("GOAL         find the first non-flat weighting WITHOUT new primitives");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The search is a sector-restricted sum of a weight over every configuration of a finite model,");
        sb.AppendLine("     so a breaker is detected by a non-zero relative spread of those sums.");
        sb.AppendLine("  2. A null result is only meaningful if the method can find something, so the search is validated on");
        sb.AppendLine("     two synthetic breakers before it is applied to AT's own quantities.");
        sb.AppendLine("  3. The recipe sector and the phase sector are counted separately, and the census of members that");
        sb.AppendLine("     couple them is the empirical input to the factorisation argument.");
        sb.AppendLine("  4. Deterministic throughout; no randomness.");
        sb.AppendLine();

        PrintHeader(OutputDetector());
        PrintHeader(OutputNearestMiss());
        PrintHeader(OutputCandidates());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
