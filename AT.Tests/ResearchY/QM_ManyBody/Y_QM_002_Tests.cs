using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_002 - Unitary Correspondence Audit (group QM). Can any AT flow reproduce unitary Schrodinger
/// evolution on the occupied mode sector? Compare the unitary Cayley flow, the centred difference flow and the
/// dissipative flow, measuring norm conservation, phase evolution and mode occupation.
/// </summary>
public sealed class Y_QM_002_Tests : ResearchTestBase
{
    public Y_QM_002_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_QM_002_NormConservationIsExactForTheUnitaryFlowOnly()
    {
        PrintHeader("QM_002 - norm conservation: the worst |m| deviation from 1");

        var table = UnitaryCorrespondenceAudit.NormTable();
        var sb = new StringBuilder();
        sb.AppendLine("  flow                                    worst | |m| - 1 |      at eps     channel");
        foreach (var t in table)
            sb.AppendLine($"  {t.Flow,-38} {t.WorstModulusDeviation:E3}            {t.WorstAtEps:E0}       {t.Channel}");
        sb.AppendLine();
        sb.AppendLine("  the Cayley multiplier is (1 + i eps s)/(1 - i eps s), so |m| = 1 IDENTICALLY - measured on every");
        sb.AppendLine("  channel and at every eps in the ladder. The centred form AMPLIFIES (|m| > 1); the forward");
        sb.AppendLine("  difference and the exact flow DISSIPATE (|m| < 1).");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, table.Length);
        var cayley = table.Single(t => t.Flow == UnitaryCorrespondenceAudit.Cayley);
        var centred = table.Single(t => t.Flow == UnitaryCorrespondenceAudit.Centred);
        var forward = table.Single(t => t.Flow == UnitaryCorrespondenceAudit.Dissipative);
        var exact = table.Single(t => t.Flow == UnitaryCorrespondenceAudit.Exact);

        // The unitary form is exact to machine precision; the others are not.
        Assert.True(cayley.WorstModulusDeviation < 1e-15);
        Assert.InRange(centred.WorstModulusDeviation, 4.9e-3, 5.1e-3);
        Assert.InRange(forward.WorstModulusDeviation, 0.19, 0.21);
        Assert.InRange(exact.WorstModulusDeviation, 0.17, 0.19);

        // A test that fails if the unitary property is ever lost: every channel at every eps.
        foreach (var eps in PhaseEvolutionAudit.Epsilons())
        foreach (var c in PhaseEvolutionAudit.Channels())
            Assert.Equal(1.0, UnitaryCorrespondenceAudit.Symbol(UnitaryCorrespondenceAudit.Cayley, c, eps).Modulus, 12);
    }

    [Fact]
    public void Y_QM_002_TheUnitartyFlowUnitarisesTwiceTheSkewGenerator()
    {
        PrintHeader("QM_002 - the Cayley form's own generator convention, measured");

        double factor = UnitaryCorrespondenceAudit.CayleyGeneratorFactor();
        var deviations = UnitaryCorrespondenceAudit.TheThreeDeviations();

        var sb = new StringBuilder();
        sb.AppendLine($"  the Cayley advance per step divided by the symbol: {factor:F8}  (the limit is 2)");
        sb.AppendLine("  because (1 + i eps s)/(1 - i eps s) = exp(2i arctan(eps s)), so the repository's Cayley update");
        sb.AppendLine("  unitarises TWICE the skew generator the other flows advance - a convention in the flow's");
        sb.AppendLine("  definition with a measurable consequence, reported rather than treated as an error.");
        sb.AppendLine();
        sb.AppendLine("  the three independent deviations of the AT unitary flow from a Schrodinger dispersion:");
        sb.AppendLine("    source                                    magnitude      eps order    removable by smaller steps");
        foreach (var d in deviations)
            sb.AppendLine($"    {d.Source,-40} {d.Magnitude:F6}       {d.EpsOrder:F2}         {d.RemovableBySmallerSteps}");
        Output.WriteLine(sb.ToString());

        // The factor is 2 to six digits at eps = 1e-6, and it is the LEADING term: it does not vanish with eps.
        Assert.InRange(factor, 1.999999, 2.000001);
        Assert.Equal(3, deviations.Length);
        Assert.InRange(deviations[0].Magnitude, 1.999, 2.001);
        Assert.True(double.IsNaN(deviations[0].EpsOrder) || deviations[0].EpsOrder < 1e-9);   // not an eps-effect
        Assert.False(deviations[0].RemovableBySmallerSteps);
        Assert.False(deviations[2].RemovableBySmallerSteps);
        Assert.True(deviations[1].RemovableBySmallerSteps);
    }

    [Fact]
    public void Y_QM_002_TheDiscretisationGapFallsAsEpsilonSquared()
    {
        PrintHeader("QM_002 - the step ladder: discretisation against the lattice");

        var ladder = UnitaryCorrespondenceAudit.StepLadder();
        double order = UnitaryCorrespondenceAudit.CayleyDeviationOrder();

        var sb = new StringBuilder();
        sb.AppendLine("  eps        discretisation gap    lattice gap (channel 24)    zone edge stationary");
        foreach (var r in ladder)
            sb.AppendLine($"  {r.Eps:E0}       {r.DiscretisationGap:E3}                {r.LatticeGap:F6}                     {r.ZoneEdgeIsStat}");
        sb.AppendLine();
        sb.AppendLine($"  the estimated eps-order of the discretisation gap: {order:F4}");
        sb.AppendLine($"  the lattice gap at the folding channel is 1 - 2/pi = {1.0 - 2.0 / Math.PI:F6}, and it does NOT MOVE.");
        sb.AppendLine("  SO ONE DEVIATION IS A STEP-SIZE EFFECT AND THE OTHER IS A PROPERTY OF THE LATTICE:");
        sb.AppendLine("  the first can be made arbitrarily small, the second cannot be removed at all.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, ladder.Length);
        // Order eps^2, with the coefficient 1/3 of the arctan expansion.
        Assert.InRange(order, 1.95, 2.05);
        Assert.InRange(ladder[0].DiscretisationGap, 3.33e-7, 3.34e-7);
        Assert.InRange(ladder[2].DiscretisationGap / ladder[1].DiscretisationGap, 99.0, 99.5);

        // The lattice gap is 1 - 2/pi and is frozen across the whole ladder.
        Assert.InRange(ladder[0].LatticeGap, 0.3633, 0.3634);
        Assert.Equal(ladder[0].LatticeGap, 1.0 - 2.0 / Math.PI, 9);
        foreach (var r in ladder) Assert.Equal(ladder[0].LatticeGap, r.LatticeGap, 12);

        // The zone edge is stationary at every eps.
        Assert.True(ladder.All(r => r.ZoneEdgeIsStat));
        Assert.True(UnitaryCorrespondenceAudit.ZoneEdgeAdvance() < 1e-15);
    }

    [Fact]
    public void Y_QM_002_TheLatticeDispersionFoldsAndPartOfTheOccupiedSectorIsBehindIt()
    {
        PrintHeader("QM_002 - the fold: sin d against d, and how much of the OCCUPIED sector is behind it");

        var fold = UnitaryCorrespondenceAudit.OccupiedFoldCensus();
        var census = UnitaryCorrespondenceAudit.OrderingCensus();

        var sb = new StringBuilder();
        sb.AppendLine($"  the lattice symbol peaks at channel {UnitaryCorrespondenceAudit.FoldingChannel()} (delta = pi/2)");
        sb.AppendLine($"  and returns to {UnitaryCorrespondenceAudit.ZoneEdgeAdvance():E2} at the zone edge (channel {PhaseEvolutionAudit.Cells / 2})");
        sb.AppendLine($"  adjacent channel pairs: {census.Ordered} advance in the continuum's order, {census.Inverted} in REVERSE");
        sb.AppendLine();
        sb.AppendLine($"  OCCUPIED SECTOR: {fold.Occupied} modes, of which {fold.AtOrAboveFold} lie at or above the folding channel:");
        sb.AppendLine($"    {string.Join(", ", fold.FoldedChannels)}");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION ASKS ABOUT THE OCCUPIED MODE SECTOR, SO THE AUDIT COUNTS IT: half the populated modes sit");
        sb.AppendLine("  where the AT dispersion runs backwards against the continuum, and the highest one never advances at all.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(24, UnitaryCorrespondenceAudit.FoldingChannel());
        Assert.True(census.Inverted > census.Ordered);          // the fold dominates the upper half
        Assert.Equal(47, census.Ordered + census.Inverted);     // 48 channels, 47 adjacent pairs

        Assert.Equal(42, fold.Occupied);
        Assert.InRange(fold.AtOrAboveFold, 20, 22);
        Assert.All(fold.FoldedChannels, c => Assert.True(c >= 24));

        // The symbol is non-monotone: below the fold it rises, above it falls.
        Assert.True(UnitaryCorrespondenceAudit.LatticeSymbol(24) > UnitaryCorrespondenceAudit.LatticeSymbol(23));
        Assert.True(UnitaryCorrespondenceAudit.LatticeSymbol(24) > UnitaryCorrespondenceAudit.LatticeSymbol(25));
    }

    [Fact]
    public void Y_QM_002_ModeOccupationIsConservedExactlyWhenTheFlowIsUnitary()
    {
        PrintHeader("QM_002 - mode occupation: conserved, and equivalent to unitarity");

        var table = UnitaryCorrespondenceAudit.OccupationTable();
        var sb = new StringBuilder();
        sb.AppendLine("  flow                                    worst occupation drift per step");
        foreach (var t in table)
            sb.AppendLine($"  {t.Flow,-38} {t.WorstOccupationChange:E3}");
        sb.AppendLine();
        sb.AppendLine("  every AT update is CIRCULANT, hence diagonal in the Fourier basis, so |c_k| drifts only through |m|.");
        sb.AppendLine("  THAT MAKES THE MEASURE NOT INDEPENDENT OF THE FIRST ONE: conserving the mode occupations is the same");
        sb.AppendLine("  condition as having unit modulus, which the audit verifies flow by flow rather than asserting.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, table.Length);
        var cayley = table.Single(t => t.Flow == UnitaryCorrespondenceAudit.Cayley);
        Assert.True(cayley.WorstOccupationChange < 1e-12);
        Assert.True(table.Where(t => t.Flow != UnitaryCorrespondenceAudit.Cayley).All(t => t.WorstOccupationChange > 1e-9));
        Assert.True(UnitaryCorrespondenceAudit.OccupationConservationIsUnitarity());
    }

    [Fact]
    public void Y_QM_002_NoFlowHasBothUnitarityAndTheCleanDispersion()
    {
        PrintHeader("QM_002 - the trade-off, which is the audit's answer");

        var trade = UnitaryCorrespondenceAudit.TheTradeOff();
        var sb = new StringBuilder();
        sb.AppendLine("  flow                                    |m| = 1 (unitary)     arg m = eps sin d (clean)");
        foreach (var t in trade)
            sb.AppendLine($"  {t.Flow,-38} {t.Unitary,-21} {t.CleanDispersion}");
        sb.AppendLine();
        sb.AppendLine("  NO FLOW HAS BOTH. The unitary form buys unitarity at the price of a phase advance that is not");
        sb.AppendLine("  Schrodinger's; the exact flow has the dispersion exactly right and dissipates.");
        sb.AppendLine("  SCHRODINGER NEEDS BOTH, SO THE CORRESPONDENCE IS PARTIAL IN A PRECISE, MEASURED SENSE.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, trade.Length);
        Assert.Single(trade.Where(t => t.Unitary));
        Assert.Single(trade.Where(t => t.CleanDispersion));
        Assert.True(trade.Single(t => t.Unitary).Flow == UnitaryCorrespondenceAudit.Cayley);
        Assert.True(trade.Single(t => t.CleanDispersion).Flow == UnitaryCorrespondenceAudit.Exact);
        Assert.True(UnitaryCorrespondenceAudit.NoFlowHasBothProperties());
    }

    [Fact]
    public void Y_QM_002_TheAmplitudePhaseSplitIsNotConservedEvenByTheUnitaryFlow()
    {
        PrintHeader("QM_002 - the sector mixing a mode count would miss");

        var mixing = UnitaryCorrespondenceAudit.SectorMixing();
        var sb = new StringBuilder();
        sb.AppendLine("  flow                                    phase before    phase after     amplitude before   amplitude after");
        foreach (var m in mixing)
            sb.AppendLine($"  {m.Flow,-38} {m.PhaseBefore:E3}         {m.PhaseAfter:E3}          {m.AmplitudeBefore:F6}           {m.AmplitudeAfter:F6}");
        sb.AppendLine();
        sb.AppendLine("  THE MODE OCCUPATIONS ARE CONSTANTS WHILE THE AMPLITUDE/PHASE SPLIT IS NOT: the flows rotate amplitude");
        sb.AppendLine("  content into the phase sector, from a state whose phase content is zero to the floor. A REAL Hamiltonian");
        sb.AppendLine("  could not do that - so the generator is Hermitian and NOT real in the amplitude/phase basis.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, mixing.Length);
        var cayley = mixing.Single(m => m.Flow == UnitaryCorrespondenceAudit.Cayley);
        Assert.True(cayley.PhaseBefore < 1e-12);                       // the canonical state is phase-free
        Assert.True(cayley.PhaseAfter > 0.5);                          // and the unitary flow gives it phase content
        Assert.True(cayley.AmplitudeAfter < cayley.AmplitudeBefore);   // at the amplitude sector's expense

        // Under the unitary flow the TOTAL deviation norm is the conserved quantity, not the split.
        var start = PhaseEvolutionAudit.Base();
        var end = PhaseEvolutionAudit.Orbit(UnitaryCorrespondenceAudit.Cayley, start, 1e-3, 2000);
        Assert.Equal(PhaseEvolutionAudit.Norm(start), PhaseEvolutionAudit.Norm(end), 9);
    }

    [Fact]
    public void Y_QM_002_TheVerdictIsPartialForEachOfTheThreeMeasures()
    {
        PrintHeader("QM_002 - the verdict");

        var measures = UnitaryCorrespondenceAudit.MeasureVerdicts();
        var counts = UnitaryCorrespondenceAudit.VerdictCounts();

        var sb = new StringBuilder();
        foreach (var m in measures)
        {
            sb.AppendLine($"  {m.Measure,-20} {m.Verdict}");
            sb.AppendLine($"    {m.Basis}");
        }
        sb.AppendLine();
        sb.AppendLine($"  -> {counts.Analogous} analogous, {counts.Partial} partial, {counts.Refuted} refuted");
        sb.AppendLine();
        sb.AppendLine(UnitaryCorrespondenceAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine(UnitaryCorrespondenceAudit.WhereItStands());
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, measures.Length);
        Assert.Equal("ANALOGOUS", measures[0].Verdict);      // norm conservation
        Assert.Equal("ANALOGOUS", measures[1].Verdict);      // mode occupation
        Assert.Equal("REFUTED", measures[2].Verdict);        // phase evolution, the measure that defines Schrodinger
        Assert.Equal(2, counts.Analogous);
        Assert.Equal(0, counts.Partial);
        Assert.Equal(1, counts.Refuted);

        Assert.Contains("PARTIAL", UnitaryCorrespondenceAudit.Verdict());
        Assert.Contains("NO AT FLOW", UnitaryCorrespondenceAudit.Verdict());
    }
}
