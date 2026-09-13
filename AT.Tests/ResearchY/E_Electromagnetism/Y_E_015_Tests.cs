using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.SectorWeightAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_015 - Sector Weight Audit (group E - Electromagnetism).
///
/// QUESTION. Can AT assign a probability or weight to flux sectors n? Candidates: occupancy measure, actualization
/// count, entropy, free room, multiplicity structure, sector topology. Compute P(n). Does any AT-derived quantity
/// prefer n = 0 or |n| > 0?
///
/// ANSWER: **the measure is EXACTLY FLAT - P(n) = 1/k for every n - so the sector is COMPLETELY FREE. Entropy, free
/// room and multiplicity structure turn out to be ONE quantity under three names, and they assign the uniform weight;
/// the occupancy measure is sector-blind, the actualization count does not exist, and the topological charge is the
/// same for every sector. The RATIO of the weights is derived exactly; the absolute normalisation is a BOUNDARY.**
/// </summary>
public class Y_E_015_Tests : ResearchTestBase
{
    public Y_E_015_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_015_TheMeasureIsExactlyUniformAndItIsCounted()
    {
        // every configuration of every model is enumerated and binned by holonomy class
        Assert.True(TheMeasureIsExactlyUniformOnEveryModel());
        foreach (var (k, l) in ModelSizes())
        {
            var counts = ClassCounts(k, l);
            Assert.Equal(k, counts.Length);
            Assert.All(counts, c => Assert.Equal(ConfigurationsPerSector(k, l), c));
            Assert.Equal(TotalConfigurations(k, l), counts.Sum());
        }

        // and P(n) is flat in n
        Assert.Equal(1.0 / 4, Probability(4, 6), 12);
        Assert.Equal(1.0 / 8, Probability(8, 4), 12);
        Assert.Equal(1.0, ProbabilityRatio(4, 6), 12);
    }

    [Fact]
    public void Y_E_015_TheShiftIsABijectionMovingTheClassByOne()
    {
        // the equality of the bins is a consequence of a bijection, not a coincidence of the sizes tested
        Assert.True(TheShiftMovesEveryClassByOne());
        foreach (var (k, l) in ModelSizes())
        {
            var (distinct, moved, sampled) = ShiftCensus(k, l);
            Assert.Equal(sampled, moved);
            Assert.True(distinct > 0);
        }

        // and the shift is exactly invertible (E_014, reused)
        Assert.True(SectorSelectionAudit.ShiftInvertibilityResidual() < 1e-12);
    }

    [Fact]
    public void Y_E_015_EntropyFreeRoomAndMultiplicityAreOneQuantity()
    {
        Assert.True(LargestEntropySpread() < 1e-15);
        Assert.Equal(1.0, SmallestFreeRoomRatio(), 12);
        Assert.True(ThreeCandidatesAreOneQuantity());

        Assert.Equal(3, DerivedCandidates().Length);
        Assert.Contains("entropy", DerivedCandidates());
        Assert.Contains("free room", DerivedCandidates());
        Assert.Contains("multiplicity structure", DerivedCandidates());
    }

    [Fact]
    public void Y_E_015_TheOccupancyMeasureIsSectorBlind()
    {
        Assert.True(TheOccupancyMeasureIsSectorBlind());
        Assert.True(OccupancyWeightSpread() < 1e-15);

        // the control: the sectors are plainly different objects
        Assert.True(Control_TheSectorsDiffer() > 0.5);
        Assert.Equal(1.0, Control_TheSectorsDiffer(), 9);
        Assert.Contains("occupancy measure", RefutedCandidates());
    }

    [Fact]
    public void Y_E_015_TheActualizationCannotChangeTheLabel()
    {
        Assert.True(TheActualizationCannotChangeTheLabel());
        Assert.True(ActualizationSpatialPart() < 1e-15);
        Assert.True(ActualizationTimeLikePart() > 1e-3);
        Assert.True(SectorTransitionsAreUnreachable());
        Assert.Contains("actualization count", RefutedCandidates());
    }

    [Fact]
    public void Y_E_015_SectorTopologyIsTheSameForEverySectorAndTheFreeEnergyDifferenceIsZero()
    {
        var table = TopologyTable();
        Assert.Equal(5, table.Length);
        Assert.All(table, t => Assert.True(t.HolonomyPhaseDistance < 1e-12,
            $"n = {t.N} holonomy distance {t.HolonomyPhaseDistance:E3}"));
        Assert.True(TheTopologicalChargeIsTheSameForEverySector());

        // but the strength does differ, so the sectors are not being conflated
        Assert.True(TheStrengthDiffers());

        Assert.True(TheFreeEnergyDifferenceIsZero());
        Assert.True(FreeEnergyDifference() < 1e-12);
        Assert.Contains("sector topology", RefutedCandidates());

        // six candidates: three refuted, three derived (one quantity under three names)
        Assert.Equal(6, Candidates().Length);
        Assert.Equal(3, RefutedCandidates().Length);
        Assert.Equal(3, DerivedCandidates().Length);
        Assert.Equal("BOUNDARY", Verdict());
    }

    [Fact]
    public void Y_E_015_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_015 - Sector Weight Audit: can AT weight the flux sectors n?");

        sb.AppendLine("QUESTION. Can AT assign a probability or weight to flux sectors n? Compute P(n).");
        sb.AppendLine("KNOWN        n exists | n is quantised | n is not selected (E_011, E_014)");
        sb.AppendLine("CANDIDATES   occupancy measure | actualization count | entropy |");
        sb.AppendLine("             free room | multiplicity structure | sector topology");
        sb.AppendLine("TEST         does any AT-derived quantity prefer n = 0 or |n| > 0?");
        sb.AppendLine("GOAL         is the flux sector WEIGHTED or COMPLETELY FREE?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The measure is counted on a finite model - a periodic chain whose link phases take k equally");
        sb.AppendLine("     spaced values - so the sector measure is enumerated rather than modelled.");
        sb.AppendLine("  2. The sector of a configuration is its holonomy class, which is the phase-periodic, gauge-invariant");
        sb.AppendLine("     content of the chain - the same quantity E_011 quantised.");
        sb.AppendLine("  3. Sector-blindness and the sector label are read through E_014's machinery, reused rather than");
        sb.AppendLine("     redefined.");
        sb.AppendLine("  4. A Boltzmann weight would need an action, so AT's sector-blindness residual IS the free-energy");
        sb.AppendLine("     difference between sectors.");
        sb.AppendLine("  5. Deterministic throughout; no randomness.");
        sb.AppendLine();

        PrintHeader(OutputTheMeasure());
        PrintHeader(OutputThreeNames());
        PrintHeader(OutputRefutations());
        PrintHeader(OutputCandidates());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
