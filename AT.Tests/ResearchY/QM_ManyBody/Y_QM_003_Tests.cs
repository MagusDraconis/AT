using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.QM_ManyBody;

/// <summary>
/// ResearchY-QM_003 - Dispersion Closure Audit (group QM). Can the lattice dispersion sin(delta) be replaced or
/// derived into delta without introducing a new primitive? Compare the ring, D96^3, the continuum limit and modified
/// generators, measuring arg(m), the group velocity and the zone-edge behaviour.
/// </summary>
public sealed class Y_QM_003_Tests : ResearchTestBase
{
    public Y_QM_003_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    private static string Ring => DispersionClosureAudit.Candidates()[0].Name;

    [Fact]
    public void Y_QM_003_TheZoneEdgeIsStationaryForEveryBandedCandidate()
    {
        PrintHeader("QM_003 - the theorem: oddness forces f(pi) = 0 for every local antisymmetric generator");

        var table = DispersionClosureAudit.ZoneEdgeTable();
        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                              f(pi)         banded");
        foreach (var t in table)
            sb.AppendLine($"  {t.Name,-52} {t.AtZoneEdge,-12:E3} {t.Banded}");
        sb.AppendLine();
        sb.AppendLine("  AN ODD 2pi-PERIODIC SYMBOL satisfies f(pi) = f(-pi) = -f(pi), so f(pi) = 0 IDENTICALLY. The zone edge is");
        sb.AppendLine("  therefore a STATIONARY MODE for EVERY antisymmetric lattice generator - not a feature of the");
        sb.AppendLine("  nearest-neighbour stencil, and not something a better stencil can repair.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, table.Length);
        Assert.True(DispersionClosureAudit.EveryLocalStencilIsStationaryAtTheZoneEdge());
        Assert.True(DispersionClosureAudit.OddnessForcesTheZoneEdgeToZero());

        // Banded candidates are stationary; the dense one is not, and its value is pi itself.
        foreach (var t in table.Where(t => t.Banded)) Assert.True(Math.Abs(t.AtZoneEdge) < 1e-12);
        var spectral = table.Single(t => !t.Banded);
        Assert.Equal(Math.PI, Math.Abs(spectral.AtZoneEdge), 9);

        // The identity is checked across a sweep of delta, not only at the edge.
        for (double d = 0.05; d < Math.PI; d += 0.05)
            Assert.Equal(-DispersionClosureAudit.SymbolAt(Ring, d),
                DispersionClosureAudit.SymbolAt(Ring, 2.0 * Math.PI - d), 12);
    }

    [Fact]
    public void Y_QM_003_EveryBandedCandidateFoldsAndOnlyTheDenseOneDoesNot()
    {
        PrintHeader("QM_003 - the fold: measured as a sign change of the group velocity");

        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                              has fold   fold channel   fold/zone");
        foreach (var c in DispersionClosureAudit.Candidates())
            sb.AppendLine($"  {c.Name,-52} {DispersionClosureAudit.HasAFold(c.Name),-10} {DispersionClosureAudit.FoldChannel(c.Name),-14:F3} "
                + $"{DispersionClosureAudit.FoldZoneFraction(c.Name):F4}");
        sb.AppendLine();
        sb.AppendLine("  the fold IS the sign change of the group velocity, which is the derivative of the dispersion.");
        sb.AppendLine("  EVERY banded candidate folds, and only the dense (spectral) one does not.");
        Output.WriteLine(sb.ToString());

        // Every local candidate folds; the non-local one does not.
        foreach (var c in DispersionClosureAudit.Candidates().Where(c => c.Local))
            Assert.True(DispersionClosureAudit.HasAFold(c.Name));
        Assert.False(DispersionClosureAudit.HasAFold(DispersionClosureAudit.Candidates()[3].Name));

        // The nearest-neighbour fold is exactly at the zone's midpoint, to the bisection's tolerance.
        Assert.InRange(DispersionClosureAudit.FoldChannel(Ring), 23.999, 24.001);
        Assert.InRange(DispersionClosureAudit.FoldZoneFraction(Ring), 0.4999, 0.5001);

        // The group velocity is positive below the fold, zero at it, and negative above it.
        Assert.True(DispersionClosureAudit.GroupVelocity(Ring, 23) > 0.0);
        Assert.True(DispersionClosureAudit.GroupVelocity(Ring, 25) < 0.0);

        // And the spectral derivative has unit group velocity everywhere - no sign change at all.
        for (int c = 1; c <= 48; c++)
            Assert.Equal(1.0, DispersionClosureAudit.GroupVelocity(DispersionClosureAudit.Candidates()[3].Name, c), 6);
    }

    [Fact]
    public void Y_QM_003_AWiderStencilPushesTheFoldAndCostsANeighbourShell()
    {
        PrintHeader("QM_003 - the stencil trade: what each order buys and what it costs");

        var table = DispersionClosureAudit.StencilTradeTable();
        var sb = new StringBuilder();
        sb.AppendLine("  stencil order                     band   linear window   fold channel   f(pi)");
        foreach (var s in table)
            sb.AppendLine($"  {s.Name,-52} {s.Band,-6} {s.LinearWindow,-15} {s.FoldChannel,-14:F3} {s.ZoneEdgeSymbol:E3}");
        sb.AppendLine();
        sb.AppendLine("  THE LINEAR WINDOW GROWS WITH THE ORDER AND THE FOLD IS ONLY PUSHED: 1 -> 6 -> 11 channels of linearity");
        sb.AppendLine("  against a fold moving 24.00 -> 27.46 -> 29.58, with the zone edge still exactly stationary at every order.");
        sb.AppendLine("  Each order costs ANOTHER NEIGHBOUR SHELL, which is a new primitive in the sense the question means.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, table.Length);
        Assert.Equal(1, table[0].LinearWindow);
        Assert.Equal(6, table[1].LinearWindow);
        Assert.Equal(11, table[2].LinearWindow);
        Assert.True(table[1].FoldChannel > table[0].FoldChannel);
        Assert.True(table[2].FoldChannel > table[1].FoldChannel);
        Assert.True(table[2].FoldChannel < 48.0);                        // pushed, never out of the zone
        foreach (var s in table) Assert.True(s.HasFold);
        foreach (var s in table) Assert.True(Math.Abs(s.ZoneEdgeSymbol) < 1e-12);

        // The band grows by exactly one shell per order.
        Assert.Equal(1, table[0].Band);
        Assert.Equal(2, table[1].Band);
        Assert.Equal(3, table[2].Band);
    }

    [Fact]
    public void Y_QM_003_TheFoldIsThePriceOfLocality()
    {
        PrintHeader("QM_003 - locality: the candidate with no fold is the candidate that is not banded");

        var locality = DispersionClosureAudit.LocalityTable();
        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                              band   non-zeros per row   local   has fold");
        foreach (var l in locality)
            sb.AppendLine($"  {l.Name,-52} {l.Band,-6} {l.NonZerosPerRow,-19} {l.Local,-7} "
                + $"{DispersionClosureAudit.HasAFold(l.Name)}");
        sb.AppendLine();
        sb.AppendLine("  THE REPLACEMENT THAT NEEDS NO NEW PRIMITIVE IS THE SPECTRAL DERIVATIVE ON THE SAME 96 SITES: symbol");
        sb.AppendLine("  EXACTLY d, no fold, no stationary edge, unit group velocity everywhere. It costs LOCALITY - 96");
        sb.AppendLine("  non-zeros per row against the stencil's 2 - and locality is exactly what the difference primitive");
        sb.AppendLine("  supplies. THE FOLD IS THE PRICE OF LOCALITY, NOT OF DISCRETENESS.");
        Output.WriteLine(sb.ToString());

        Assert.True(DispersionClosureAudit.TheFoldIsThePriceOfLocality());
        var spectral = locality.Single(l => !l.Local);
        Assert.Equal(96, spectral.NonZerosPerRow);
        foreach (var l in locality.Where(l => l.Local)) Assert.Equal(2 * l.Band, l.NonZerosPerRow);

        // The spectral symbol is exactly delta: linear everywhere, including where the stencil folds.
        for (int c = 1; c <= 48; c++)
            Assert.Equal(DispersionClosureAudit.Delta(c), DispersionClosureAudit.Symbol(DispersionClosureAudit.Candidates()[3].Name, c), 12);
    }

    [Fact]
    public void Y_QM_003_HalfTheOccupiedSectorReversesItsGroupVelocity()
    {
        PrintHeader("QM_003 - the group velocity on the occupied sector");

        var census = DispersionClosureAudit.GroupVelocityCensus();
        var sb = new StringBuilder();
        sb.AppendLine("  candidate                                              reversed   reversed occupied   stationary   occupied");
        foreach (var c in census)
            sb.AppendLine($"  {c.Name,-52} {c.Reversed,-10} {c.ReversedOccupied,-20} {c.Stationary,-12} {c.Occupied}");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, census.Length);
        var ring = census.Single(c => c.Name.StartsWith("ring: nearest"));
        Assert.Equal(24, ring.Reversed);
        Assert.Equal(21, ring.ReversedOccupied);
        Assert.Equal(42, ring.Occupied);
        Assert.Equal(1, ring.Stationary);                                // the zone edge

        // A wider stencil reverses FEWER channels - the fold is pushed past them - but never zero.
        var order4 = census.Single(c => c.Name.Contains("4th order"));
        var order6 = census.Single(c => c.Name.Contains("6th order"));
        Assert.True(order4.ReversedOccupied < ring.ReversedOccupied);
        Assert.True(order6.ReversedOccupied < order4.ReversedOccupied);
        Assert.True(order6.ReversedOccupied > 0);

        // The spectral candidate reverses nothing at all.
        Assert.Equal(0, census.Single(c => c.Name.Contains("spectral")).Reversed);
    }

    [Fact]
    public void Y_QM_003_TheCubicSubstrateMakesTheFoldWorseAndDoesNotFixIt()
    {
        PrintHeader("QM_003 - D96^3: the same per-axis symbol, and a larger folded fraction");

        var byDimension = DispersionClosureAudit.FoldedFractionByDimension();
        var sb = new StringBuilder();
        sb.AppendLine("  dimensions   folded fraction of the zone");
        foreach (var d in byDimension)
            sb.AppendLine($"  {d.Dimensions,-11} {d.FoldedFraction:F4}");
        sb.AppendLine();
        sb.AppendLine("  THE CUBE KEEPS THE SAME PER-AXIS SYMBOL - each axis carries the same nearest-neighbour stencil - so the");
        sb.AppendLine("  fold per axis is unchanged: same channel 24, same 21 of 42 occupied modes. What changes is the");
        sb.AppendLine("  DIMENSION OF MOMENTUM SPACE, and since a mode is folded when ANY component reverses:");
        sb.AppendLine("    folded fraction = 1 - (1/2)^d,  i.e. 1/2 in 1D against 7/8 in 3D.");
        sb.AppendLine("  SO D96^3 DOES NOT FIX THE FOLD; IT MULTIPLIES IT.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, byDimension.Length);
        Assert.Equal(0.5, byDimension[0].FoldedFraction, 12);
        Assert.Equal(0.75, byDimension[1].FoldedFraction, 12);
        Assert.Equal(0.875, byDimension[2].FoldedFraction, 12);

        // The cube's per-axis candidate has the same fold as the ring's.
        var cube = DispersionClosureAudit.Candidates().Single(c => c.Name.StartsWith("D96^3"));
        Assert.Equal(DispersionClosureAudit.FoldChannel(Ring), DispersionClosureAudit.FoldChannel(cube.Name), 9);
        Assert.Equal(21, DispersionClosureAudit.GroupVelocityCensus().Single(c => c.Name.StartsWith("D96^3")).ReversedOccupied);
    }

    [Fact]
    public void Y_QM_003_TheContinuumLimitLeavesTheFoldWhereItIs()
    {
        PrintHeader("QM_003 - the continuum limit: a route, not a fix");

        var table = DispersionClosureAudit.RefinementTable();
        var sb = new StringBuilder();
        sb.AppendLine("  cells   fixed-wavenumber dev   fixed-mode-index dev   zone-edge dev   fold channel   fold/zone");
        foreach (var r in table)
            sb.AppendLine($"  {r.Cells,-7} {r.FixedWavenumberDeviation,-22:E3} {r.FixedModeIndexDeviation,-22:E3} {r.ZoneEdgeDeviation,-15:E3} {r.FoldChannel,-14:F1} {r.FoldFraction:F4}");
        sb.AppendLine();
        sb.AppendLine("  AT A FIXED PHYSICAL WAVENUMBER THE DEVIATION DOES NOT MOVE: delta is constant when the mode number rises");
        sb.AppendLine("  with the cell count, so THE FOLD SURVIVES EVERY REFINEMENT. The deviation falls as 1/N^2 only at a fixed");
        sb.AppendLine("  MODE INDEX, which is a longer wave - a different state, not the same one measured better. The zone edge");
        sb.AppendLine("  is the zone edge at every resolution, and the fold stays at the same ZONE FRACTION.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, table.Length);
        // the fixed-wavenumber deviation is identical at every resolution
        Assert.True(DispersionClosureAudit.TheFoldSurvivesRefinementAtFixedPhysicalWavenumber());
        foreach (var r in table)
            Assert.Equal(table[0].FixedWavenumberDeviation, r.FixedWavenumberDeviation, 12);
        Assert.InRange(table[0].FixedWavenumberDeviation, 0.0996, 0.0998);

        // the fixed-mode-index deviation falls as 1/N^2 - a different state
        Assert.InRange(DispersionClosureAudit.RefinementOrder(), 1.9, 2.1);
        Assert.True(table[3].FixedModeIndexDeviation < table[0].FixedModeIndexDeviation / 50.0);

        // the zone edge is stationary and at the SAME zone fraction at every resolution
        Assert.True(DispersionClosureAudit.TheFoldIsScaleInvariantInTheZoneFraction());
        foreach (var r in table)
        {
            Assert.Equal(1.0, r.ZoneEdgeDeviation, 12);
            Assert.Equal(r.Cells / 4.0, r.FoldChannel, 9);
            Assert.Equal(0.5, r.FoldFraction, 12);
        }
    }

    [Fact]
    public void Y_QM_003_TheVerdictIsBoundaryAndTheOriginIsLocated()
    {
        PrintHeader("QM_003 - the verdict and the origin");

        var routes = DispersionClosureAudit.Routes();
        var counts = DispersionClosureAudit.VerdictCounts();

        var sb = new StringBuilder();
        foreach (var r in routes)
        {
            sb.AppendLine($"  {r.Route}: {r.Status}");
            sb.AppendLine($"    costs: {r.Costs}");
        }
        sb.AppendLine();
        sb.AppendLine(DispersionClosureAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine(DispersionClosureAudit.WhereItStands());
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, routes.Length);
        Assert.Equal(1, counts.Derived);
        Assert.Equal(1, counts.Boundary);
        Assert.Equal(0, counts.Refuted);

        var verdict = DispersionClosureAudit.Verdict();
        Assert.Contains("BOUNDARY", verdict);
        // the origin is stated, and it is NOT discreteness
        Assert.Contains("PRICE OF LOCALITY", verdict);
        Assert.Contains("NOT OF DISCRETENESS", verdict);
        Assert.Contains("ANTISYMMETRIC", verdict);
        Assert.Contains("LOCAL", verdict);
        Assert.Contains("PERIODIC", DispersionClosureAudit.WhereItStands());
    }
}
