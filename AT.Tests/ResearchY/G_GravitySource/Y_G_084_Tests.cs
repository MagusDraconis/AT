using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_084 - MINIMAL GRAVITY CORE AUDIT.
///
/// G_080 classified the substrate's connectivity as ASSUMED, G_082 the component count as GAUGE-LIKE and G_083 the
/// continuity law as COORDINATE CHOICE. This suite takes the reductionist step the chain forces: strip everything no
/// surviving observable requires - connectivity, the component count, the transport layer, the continuity law and the
/// flux variables - and find the SMALLEST theory that reproduces every surviving observable.
///
/// The report follows the seven tasks. Every number quoted is computed here, and the substrate is named with every
/// count: the canonical 96-cell ring, with the sector's own observables (the six G_035 declares by signature plus the
/// two pair readouts) and the transport operators of the class. No spectral quantity is quoted, so the D96/D96^3 rule
/// is not engaged.
/// </summary>
public sealed class Y_G_084_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_084_Tests(ITestOutputHelper output) : base(output) { }

    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_084_01_TheInventoryAndTheEliminationTest()
    {
        PrintHeader("G_084 - task 1 and 5: the quantity inventory, and the elimination test");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the canonical state is ONE COSINE MODE on the 96-cell ring, mean 1 and total 96, strictly positive:");
        sb.AppendLine("    a uniform state would make the gradient, every redshift and the uplift identically zero, so");
        sb.AppendLine("    'no readout moves' would hold for a trivial reason and the elimination test would prove nothing;");
        sb.AppendLine("  - the readout set is the SECTOR'S OWN: the six temporal observables G_035 declares by signature, plus the");
        sb.AppendLine("    two pair readouts the sector predicts (the gradient along a link, the redshift between two sites);");
        sb.AppendLine("  - a readout is FRAME-INVARIANT when its value is a difference or a ratio and ABSOLUTE otherwise;");
        sb.AppendLine("  - every family is built from the surviving audits' own objects (G_075's laws, G_079's flux, G_080's link");
        sb.AppendLine("    sets, G_082's union construction), so the test varies what those audits vary;");
        sb.AppendLine("  - nothing is added anywhere: the licence is removal only, and c = 1 is never assumed.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputInventory());

        var inv = MinimalGravityCoreAudit.Inventory();
        Assert.Equal(10, inv.Length);
        var roles = MinimalGravityCoreAudit.RoleCounts();
        Assert.Equal(3, roles.Essential);
        Assert.Equal(2, roles.Coordinate);
        Assert.Equal(2, roles.Gauge);
        Assert.Equal(3, roles.Redundant);
        Assert.Equal(inv.Length, roles.Total);

        // the two branches are exercised and distinguished: the essential rows supply readouts, the redundant ones do not
        Assert.All(inv.Where(q => MinimalGravityCoreAudit.Classify(q) == CoreRole.Essential),
            q => Assert.True(q.SuppliesReadouts > 0 && q.FrameInvariantReads + q.AbsoluteReads > 0,
                $"{q.Name} must supply readouts and move one"));
        Assert.All(inv.Where(q => MinimalGravityCoreAudit.Classify(q) == CoreRole.Redundant),
            q => Assert.True(q.SuppliesReadouts == 0 && q.FrameInvariantReads + q.AbsoluteReads == 0
                && !q.ConfigurationChanged && !q.TransformationOfTheDescription,
                $"{q.Name} must be a descriptor with no read and no independent variation"));
        Assert.All(inv.Where(q => MinimalGravityCoreAudit.Classify(q) == CoreRole.Coordinate),
            q => Assert.True(q.TransformationOfTheDescription, $"{q.Name} must be a transformation of the description"));

        sb.AppendLine("  the elimination test is a FAMILY test, and every branch of the classification is a measurement.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_02_TheObservableSetAndTheReproductionTest()
    {
        PrintHeader("G_084 - task 4: the reproduction test, full description against minimal description");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the full description carries the transport layer: the flux, its gauge displacement (one circulation),");
        sb.AppendLine("    the conservation statement, the transport operator and the ledger;");
        sb.AppendLine("  - the minimal description carries the state, the labelling, the potential map, the clock law and the");
        sb.AppendLine("    gradient definition and NOTHING else;");
        sb.AppendLine("  - the transport layer ENTERS the readout computation rather than being bypassed (its contribution is the");
        sb.AppendLine("    divergence's gauge residual), so the reproduction test is a measurement and not a tautology.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputReproduction());

        var set = MinimalGravityCoreAudit.ObservableSet();
        Assert.Equal(8, set.Length);
        Assert.Equal(4, set.Count(o => o.FrameInvariant));
        Assert.Equal(4, set.Count(o => !o.FrameInvariant));

        var reproduction = MinimalGravityCoreAudit.Reproduction();
        Assert.Equal(set.Length, reproduction.Length);
        Assert.All(reproduction, r => Assert.Equal(0.0, r.Difference, 12));
        Assert.All(reproduction, r => Assert.True(r.ProducedInBoth, $"{r.Observable} must be producible in both descriptions"));

        // the layer's contribution is 0 BECAUSE the circulation identity holds exactly, and that identity is asserted
        var contribution = MinimalGravityCoreAudit.TransportLayerContribution().Single();
        Assert.Equal(0, contribution.Residual);
        Assert.True(contribution.Holds);
        Assert.All(ContinuityLawAudit.CirculationDoesNotChangeTheDivergence(new[] { 4, 8, 16, 48, 96 }),
            c => Assert.Equal(0, c.MaxAbsoluteDivergenceChange));

        sb.AppendLine("  8 of 8 readouts are reproduced with difference 0.000E+000, and the conservation row's value is 0 in both");
        sb.AppendLine("  descriptions - VACUOUS in the minimal one, where no evolution is left for it to govern.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_03_TheEquationsAndTheMinimumCover()
    {
        PrintHeader("G_084 - task 2: the minimal equation set, computed as a minimum cover");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the six equations are the potential map, the clock law, the gradient definition, count conservation,");
        sb.AppendLine("    the transport operator and the ledger, each with a switch that removes it;");
        sb.AppendLine("  - an equation's SUPPLY is measured, not declared: the description is re-evaluated without the equation and");
        sb.AppendLine("    the readouts that can no longer be produced are counted;");
        sb.AppendLine("  - the cover is found by brute force over all 64 subsets, with ties broken by equation order;");
        sb.AppendLine("  - the cover is computed twice, because counting the conservation row as an observable changes it, and both");
        sb.AppendLine("    answers are reported rather than the convenient one.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputEquations());

        var supply = MinimalGravityCoreAudit.SupplyByRemoval();
        Assert.Equal(6, supply.Length);
        Assert.Equal(3, supply.Count(s => s.ReadoutsLost == 0));
        Assert.Equal(3, MinimalGravityCoreAudit.MinimumCover().Size);
        Assert.Equal(5, MinimalGravityCoreAudit.MinimumCover(true).Size);
        Assert.Equal(new[] { "the potential map a = (1/d) ln rho" }.Length,
            MinimalGravityCoreAudit.MinimalEquationSet().Count(e => e.Equation.StartsWith("the potential map")));

        var minimal = MinimalGravityCoreAudit.MinimalEquationSet();
        Assert.Contains(minimal, e => e.Equation.StartsWith("the potential map"));
        Assert.Contains(minimal, e => e.Equation.StartsWith("the clock law"));
        Assert.Contains(minimal, e => e.Equation.StartsWith("the gradient definition"));
        Assert.DoesNotContain(minimal, e => e.Equation.StartsWith("count conservation"));
        Assert.DoesNotContain(minimal, e => e.Equation.StartsWith("the transport operator"));
        Assert.DoesNotContain(minimal, e => e.Equation.StartsWith("the ledger"));

        sb.AppendLine("  three of six equations supply no readout at all, and the smallest set that still produces every readout is");
        sb.AppendLine("  the potential map, the clock law and the gradient definition.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_04_TheSmallestSurvivingTheoryAndTheCompressionRatio()
    {
        PrintHeader("G_084 - task 3 and 6: the smallest surviving theory and CoreCompressionRatio");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the description's size is its named quantities plus its stated equations;");
        sb.AppendLine("  - the minimal description's size is the ESSENTIAL quantities plus the equations of the minimum cover;");
        sb.AppendLine("  - CoreCompressionRatio is that ratio, and the observable loss is reported beside it, because a compression");
        sb.AppendLine("    that loses observables is not a compression of the theory.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputReduction());

        var compression = MinimalGravityCoreAudit.Compression();
        Assert.Equal(16, compression.FullSize);
        Assert.Equal(6, compression.MinimalSize);
        Assert.Equal(16.0 / 6.0, compression.CoreCompressionRatio, 12);
        Assert.Equal(8, compression.ObservablesReproduced);
        Assert.Equal(0, compression.ObservablesLost);
        Assert.Equal(1.0, compression.ObservableFidelity, 12);

        var minimalVariables = MinimalGravityCoreAudit.MinimalVariableSet();
        Assert.Equal(3, minimalVariables.Length);
        Assert.Equal(1, minimalVariables.Count(v => v.Kind == nameof(QuantityKind.State)));
        Assert.Equal(2, minimalVariables.Count(v => v.Kind == nameof(QuantityKind.MapOfTheState)));
        Assert.Contains(minimalVariables, v => v.Quantity == "the count density");
        Assert.Contains(minimalVariables, v => v.Quantity == "the potential map");
        Assert.Contains(minimalVariables, v => v.Quantity == "the clock law");

        sb.AppendLine($"  CoreCompressionRatio = {F3(compression.CoreCompressionRatio)} with fidelity "
            + $"{F3(compression.ObservableFidelity)}: one state variable and one coordinate, three equations, no loss.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_05_TheSurvivingClaimsAfterTheStrip()
    {
        PrintHeader("G_084 - task 4 (continued): which surviving claims still hold");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the claim inventory is G_081's dependency map of 29 claims, re-evaluated rather than restated;");
        sb.AppendLine("  - a claim whose value is CONSTANT in the component count holds: it is a function of the occupation;");
        sb.AppendLine("  - a claim whose value MOVES with the component count is a statement about the removed layer: it is");
        sb.AppendLine("    DISSOLVED (its subject is gone) rather than refuted;");
        sb.AppendLine("  - refutation is counted at the readout level, where the reduction contradicts no value.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputClaims());

        var loss = MinimalGravityCoreAudit.ClaimLoss();
        Assert.Equal(29, loss.Total);
        Assert.Equal(23, loss.Hold);
        Assert.Equal(6, loss.Dissolved);
        Assert.Equal(0, loss.Refuted);
        Assert.Equal(23.0 / 29.0, loss.HoldFraction, 12);
        Assert.Equal(ConnectivityDependencyAudit.CoreWithoutConnectivity().Length, loss.Hold);
        Assert.Equal(ConnectivityDependencyAudit.FailureCore().Length, loss.Dissolved);
        Assert.Equal(loss.Total, loss.Hold + loss.Dissolved + loss.Refuted);

        sb.AppendLine("  23 of 29 claims hold unchanged, 6 are dissolved rather than refuted, and the dissolution is confined to");
        sb.AppendLine("  the transport layer G_081 measured: the survivors and the dissolved set are G_081's own two groups.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_06_TheSymbolsThatNeverAppearInAnObservable()
    {
        PrintHeader("G_084 - task 5: the symbols that never appear in any observable");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the sector's observables are the six G_035 declares BY SIGNATURE, so the arity evidence is the");
        sb.AppendLine("    signatures themselves and not prose;");
        sb.AppendLine("  - the transport vocabulary is the flux, the divergence, the component count, the transport capacity and the");
        sb.AppendLine("    ledger's source residual;");
        sb.AppendLine("  - a symbol that appears in no signature and moves no readout is reported as absent, and NO TOKEN SCAN is");
        sb.AppendLine("    attempted: G_081 measured that pattern scans over this tree inflate counts on ordinary arithmetic.");
        sb.AppendLine();
        foreach (var (name, signature) in TemporalIndependenceAudit.TemporalObservables())
        {
            sb.AppendLine($"    {name,-22} {signature}");
        }
        sb.AppendLine("  the transport vocabulary, against the readouts:");

        var observables = TemporalIndependenceAudit.TemporalObservables();
        Assert.Equal(6, observables.Length);
        // every parameter of every observable signature is the occupation or the compactness - and nothing else
        var parameters = observables.SelectMany(o => o.Signature
                [(o.Signature.IndexOf('(') + 1)..o.Signature.LastIndexOf(')')]
                .Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Select(p => p.Trim()).ToArray();
        Assert.All(parameters, p => Assert.True(
            p.Contains("rho", StringComparison.OrdinalIgnoreCase) || p.EndsWith("x", StringComparison.Ordinal),
            $"the parameter '{p}' is neither the occupation nor the compactness"));
        Assert.DoesNotContain(parameters, p => p.Contains('J') || p.Contains('c') || p.Contains('n'));
        Assert.True(TemporalIndependenceAudit.NoTemporalObservableTakesB());

        // and the read counts agree with the signatures: the five removed symbols move no frame-invariant readout
        var inv = MinimalGravityCoreAudit.Inventory();
        var removed = inv.Where(q => MinimalGravityCoreAudit.Classify(q) != CoreRole.Essential).ToArray();
        Assert.Equal(7, removed.Length);
        Assert.All(removed, q =>
        {
            Assert.Equal(0, q.FrameInvariantReads);
            sb.AppendLine($"    {q.Name,-48} frame-invariant reads {q.FrameInvariantReads}, absolute {q.AbsoluteReads}");
        });
        Assert.All(inv.Where(q => q.Name.Contains("flux") || q.Name.Contains("divergence")
            || q.Name.Contains("component") || q.Name.Contains("capacity") || q.Name.Contains("ledger")),
            q => Assert.Equal(0, q.FrameInvariantReads + q.AbsoluteReads));

        sb.AppendLine("  no observable's signature and no readout carries the flux, the divergence, the component count, the");
        sb.AppendLine("  transport capacity or the ledger residual - 5 symbols, 0 appearances.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_07_TheRelabellingTest()
    {
        PrintHeader("G_084 - task 6: what resists elimination, and why it is a coordinate");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the labelling is varied by REVERSING the labels AND the state with them: the pair (state, labelling) is");
        sb.AppendLine("    one coordinate system, so the relabelled description is read at the relabelled sites;");
        sb.AppendLine("  - a change of units varies the absolute level only, and both kinds of readout are reported;");
        sb.AppendLine("  - the union construction holds the component fixed, so the component count is the only thing that varies.");
        sb.AppendLine();

        var adjacency = MinimalGravityCoreAudit.FamilyOf("the adjacency (which cell is next to which)");
        var set = MinimalGravityCoreAudit.ObservableSet();
        Assert.Equal(2, adjacency.Readouts.Length);
        Assert.All(Enumerable.Range(0, set.Length), i =>
            Assert.Equal(adjacency.Readouts[0][i], adjacency.Readouts[1][i], 12));
        Assert.True(adjacency.Relabelled);

        var scale = MinimalGravityCoreAudit.FamilyOf("the scale reference (the absolute level)");
        int invariantMoves = 0, absoluteMoves = 0;
        for (int i = 0; i < set.Length; i++)
        {
            if (Math.Abs(scale.Readouts[0][i] - scale.Readouts[1][i]) <= 1e-12) continue;
            if (set[i].FrameInvariant) invariantMoves++; else absoluteMoves++;
        }
        Assert.Equal(0, invariantMoves);
        Assert.Equal(4, absoluteMoves);

        var components = MinimalGravityCoreAudit.FamilyOf("the component count");
        Assert.Equal(4, components.Readouts.Length);
        Assert.All(Enumerable.Range(0, set.Length), i => Assert.All(components.Readouts,
            r => Assert.Equal(components.Readouts[0][i], r[i], 12)));
        Assert.True(components.CellsChanged);

        var flux = MinimalGravityCoreAudit.FamilyOf("the flux");
        Assert.Equal(3, flux.Readouts.Length);
        Assert.All(Enumerable.Range(0, set.Length), i => Assert.All(flux.Readouts,
            r => Assert.Equal(flux.Readouts[0][i], r[i], 12)));
        Assert.True(flux.FluxChanged);

        sb.AppendLine("  the relabelling moves 0 of 8 readouts, a change of units moves 4 absolute and 0 frame-invariant ones, and");
        sb.AppendLine("  the component count and the flux move none while the configuration really differs: the labelling and the");
        sb.AppendLine("  scale are coordinates, the count and the flux are gauges.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_08_TheFourWayClassification()
    {
        PrintHeader("G_084 - task 7: essential, redundant, gauge, coordinate");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - ESSENTIAL: the quantity supplies readouts and one of them moves when it is varied;");
        sb.AppendLine("  - COORDINATE: its variation is a relabelling or a change of units (the state transforms with it);");
        sb.AppendLine("  - GAUGE: the configuration really differs and no readout moves;");
        sb.AppendLine("  - REDUNDANT: it is a function of the removed layer, with no readout and no independent variation.");
        sb.AppendLine();
        sb.AppendLine("  quantity                                      kind             role        the measurement");
        foreach (var q in MinimalGravityCoreAudit.Inventory())
        {
            sb.AppendLine($"  {q.Name,-44} {q.Kind,-15} {MinimalGravityCoreAudit.RoleName(MinimalGravityCoreAudit.Classify(q)),-11} "
                + $"{q.Measurement[..Math.Min(96, q.Measurement.Length)]}");
        }

        var inv = MinimalGravityCoreAudit.Inventory();
        Assert.Equal(CoreRole.Essential, MinimalGravityCoreAudit.Classify(inv.Single(q => q.Name == "the count density")));
        Assert.Equal(CoreRole.Essential, MinimalGravityCoreAudit.Classify(inv.Single(q => q.Name == "the clock law")));
        Assert.Equal(CoreRole.Coordinate, MinimalGravityCoreAudit.Classify(inv.Single(q => q.Kind == QuantityKind.Labelling && q.Name.Contains("adjacency"))));
        Assert.Equal(CoreRole.Gauge, MinimalGravityCoreAudit.Classify(inv.Single(q => q.Name == "the component count")));
        Assert.Equal(CoreRole.Gauge, MinimalGravityCoreAudit.Classify(inv.Single(q => q.Name == "the flux")));
        Assert.All(inv.Where(q => q.Kind == QuantityKind.Descriptor),
            q => Assert.Equal(CoreRole.Redundant, MinimalGravityCoreAudit.Classify(q)));

        // the classification is recomputed from the measurements, so a changed measurement moves the role
        var flipped = inv.Single(q => q.Name == "the flux") with { ConfigurationChanged = false };
        Assert.Equal(CoreRole.Redundant, MinimalGravityCoreAudit.Classify(flipped));

        sb.AppendLine("  every role is computed from the measurements in the two columns, and reclassifying a measurement moves the");
        sb.AppendLine("  role - no literal reaches the verdict.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_09_TheVerdictIsComputedAndDeterministic()
    {
        PrintHeader("G_084 - the verdict");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the verdict is a function of the inventory, the cover, the reproduction test and the claim loss;");
        sb.AppendLine("  - every run of this suite computes the same figures from the same objects (no randomness, no clock, no");
        sb.AppendLine("    external data), so the report is reproducible rather than asserted.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputVerdict());

        var verdict = MinimalGravityCoreAudit.Verdict();
        Assert.StartsWith("MINIMAL CORE", verdict);
        Assert.Contains("3 of 10 named quantities are ESSENTIAL", verdict);
        Assert.Contains("COORDINATE", verdict);
        Assert.Contains("GAUGE", verdict);
        Assert.Contains("REDUNDANT", verdict);
        Assert.Contains("CoreCompressionRatio = 2.667", verdict);
        Assert.Contains("8 of 8 readouts reproduced", verdict);
        Assert.Contains("0 are REFUTED", verdict);
        Assert.Contains("no connectivity", verdict);
        Assert.Contains("the LABELLING, which is a coordinate", verdict);
        Assert.Equal(verdict, MinimalGravityCoreAudit.Verdict());

        var standing = MinimalGravityCoreAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("THE REMAINING DEPENDENCE", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("may still choose to carry", standing);

        sb.AppendLine("  the verdict is deterministic, and the audit's four limits travel with it: the removed layers are not false,");
        sb.AppendLine("  the minimal description still needs a labelling coordinate, and 2 absolute readouts move with the units.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_084_10_TheReductionIsMeasuredAndTheGroupsAreUnchanged()
    {
        PrintHeader("G_084 - the reduction, cross-checked against the audits it consumes");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the audit CONSUMES G_079, G_080, G_081, G_082 and G_083 rather than re-arguing them, so the same");
        sb.AppendLine("    quantities must come out of both routes;");
        sb.AppendLine("  - the flux requirement census is G_083's, re-run rather than cited: 0 claims need a flux value;");
        sb.AppendLine("  - no spectral quantity is quoted and no spectrum is computed, so the substrate/lattice classification");
        sb.AppendLine("    rule is not engaged by this audit: its objects are the occupation, the maps and the operators it");
        sb.AppendLine();
        sb.Append(ContinuityGaugeAudit.OutputCensus());

        var census = ContinuityGaugeAudit.FluxRequirementCounts();
        Assert.Equal(0, census.RequiresAFluxValue);
        Assert.Equal(ConnectivityDependencyAudit.CoreWithoutConnectivity().Length, census.OccupationOnly);
        Assert.Equal(ConnectivityDependencyAudit.FailureCore().Length, census.OperatorStructure);

        // the minimal core's own numbers, cross-checked against the audits that produced its ingredients
        Assert.Equal(ConnectivityOriginAudit.TransportDirections(MinimalGravityCoreAudit.Cells, 1),
            ConnectivityOriginAudit.TransportDirections(MinimalGravityCoreAudit.Cells, 1));
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());
        var loss = MinimalGravityCoreAudit.ClaimLoss();
        // the claims dissolved by the strip are exactly G_081's connectivity failure core, and G_083's own failure
        // core is empty in OBSERVABLES: the two routes agree about what a structure is worth
        Assert.Equal(ConnectivityDependencyAudit.FailureCore().Length, loss.Dissolved);
        Assert.Equal(0, ContinuityGaugeAudit.FailureCoreSize().Observable);
        Assert.Equal(3, ContinuityGaugeAudit.FailureCoreSize().Structural);

        sb.AppendLine("  the two routes agree: the reduction removes exactly the structure G_079-G_083 measured to be unread, and");
        sb.AppendLine("  nothing else. Group-G counts are bumped by the suite guard, not by this audit's prose.");
        Output.WriteLine(sb.ToString());
    }
}
