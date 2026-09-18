using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_078 - SOURCE MANIPULATION AUDIT.
///
/// G_077 established that the clock and the acceleration share one source: the local occupancy rho, through the
/// potential A = (1/d) ln rho. This suite asks whether that source can be CHANGED LOCALLY, and it prefers refutation:
/// a proof that only redistribution exists would kill practical gravity engineering, while a genuine source term
/// would open one.
///
/// The report is organised around the six questions of the audit assignment. Every number quoted is computed here.
/// </summary>
public sealed class Y_G_078_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_078_Tests(ITestOutputHelper output) : base(output) { }

    private static string Num(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_078_01_WhichProcesses_CanAlterLocalRho()
    {
        PrintHeader("G_078 - Q1: which processes can alter local rho?");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the source is the local occupancy rho of the actualization count, as G_077 established;");
        sb.AppendLine("  - the clock potential is A = (1/d) ln rho, d = 3 (G_028/G_035);");
        sb.AppendLine("  - allowed: actualization, density transport, count conservation, causal structure;");
        sb.AppendLine("  - FORBIDDEN and not used: new fields, imported matter sectors, imported GR equations.");
        sb.AppendLine();
        sb.Append(SourceManipulationAudit.OutputChannels());

        var census = SourceManipulationAudit.ChannelCensus();
        var operators = SourceManipulationAudit.OperatorClass(
            SourceManipulationAudit.Uniform(8).Select((_, i) => 1.0 + 0.3 * Math.Sin(i)).ToArray());
        var divergences = operators.Where(o => !o.Operator.StartsWith("CONTROL")).ToArray();
        var control = operators.Single(o => o.Operator.StartsWith("CONTROL"));

        // the detector can see a source: the control leaks the count while every divergence conserves it
        Assert.True(Math.Abs(control.CountChangeOnARing) > 1.0,
            "the non-divergence control must change the total, otherwise the detector is blind");
        Assert.All(divergences, o => Assert.True(Math.Abs(o.CountChangeOnARing) < 1e-9,
            $"{o.Operator} must conserve the count on a closed ring"));

        // and on an OPEN chain the first-order forms leak exactly the boundary term the chain drops: a bounded
        // region has nowhere to send its boundary flux. This is Q4's answer arriving from the operator side.
        Assert.All(operators.Where(o => o.Operator.StartsWith("local") || o.Operator.StartsWith("centred")),
            o => Assert.True(o.OpenLeakEqualsDroppedTerm,
                $"{o.Operator}: the open-chain leak must equal the dropped boundary term"));
        Assert.True(operators.Any(o => Math.Abs(o.CountChangeOnAnOpenChain) > 1e-9),
            "the open-chain column must show a leak, otherwise it is not a test");

        // and no channel of the allowed class is a source
        Assert.DoesNotContain(census, c => c.Status.Contains("SUPPLIED") && !c.Status.Contains("NOT"));
        Assert.Contains(census, c => c.Channel == "density transport" && c.Status.StartsWith("AVAILABLE"));

        Output.WriteLine(sb.ToString());
        Output.WriteLine($"divergences measured: {divergences.Length}; largest count leak among them: "
            + $"{Num(divergences.Max(o => Math.Abs(o.CountChangeOnARing)))}; control leak: {Num(Math.Abs(control.CountChangeOnARing))}");
    }

    [Fact]
    public void Y_G_078_02_Classify_Redistribution_Amplification_Suppression()
    {
        PrintHeader("G_078 - Q2: redistribution, amplification, or suppression?");
        var sb = new StringBuilder();
        sb.AppendLine("CLASSIFICATION UNDER COUNT CONSERVATION");
        sb.AppendLine("  transport is a DIVERGENCE: it moves count, it does not make it. A local rise is therefore the");
        sb.AppendLine("  visible half of a redistribution, and the audit must say what happens to the other half.");
        sb.AppendLine();

        var pair = SourceManipulationAudit.BalancedPair(16, 0.5);
        double before = SourceManipulationAudit.Total(SourceManipulationAudit.Uniform(16));
        double after = SourceManipulationAudit.Total(pair);
        double well = SourceManipulationAudit.Potential(pair[0]);
        double hill = SourceManipulationAudit.Potential(pair[1]);
        var ledger = SourceManipulationAudit.RegionLedger(SourceManipulationAudit.LedgerPattern);

        sb.AppendLine($"  uniform total   {Num(before)}");
        sb.AppendLine($"  pair total      {Num(after)}   (identical)");
        sb.AppendLine($"  well potential  {Num(well)}      hill potential {Num(hill)}");
        sb.AppendLine($"  the pair IS a redistribution: amplification at one cell and suppression at another, one statement.");
        sb.AppendLine();
        sb.AppendLine("  the region ledger, which is the measured form of 'the other half must be somewhere':");
        foreach (var l in ledger)
            sb.AppendLine($"    {l.Region,-15} change inside {Num(l.ChangeInside),10}   net boundary flux {Num(l.NetBoundaryFlux),10}   residual {Num(l.Residual)}");

        Assert.Equal(before, after, 12);
        Assert.True(hill > 0 && well < SourceManipulationAudit.Potential(1.0));
        Assert.All(ledger, l => Assert.True(l.Residual < 1e-12,
            $"{l.Region}: the change inside must equal minus the net boundary flux"));

        // a single-cell region cannot change alone: its own residual is not the interesting quantity, its
        // boundary flux is - and that flux is non-zero for exactly those single cells whose count changes
        var oneCells = SourceManipulationAudit.CellLedger(SourceManipulationAudit.LedgerPattern);
        Assert.All(oneCells, l => Assert.True(l.Residual < 1e-12));
        int moving = oneCells.Count(l => Math.Abs(l.ChangeInside) > 1e-12);        Assert.True(moving > 0, "the pattern must move at least one cell, otherwise the ledger proves nothing");
        Assert.All(oneCells.Where(l => Math.Abs(l.ChangeInside) > 1e-12),
            l => Assert.True(Math.Abs(l.NetBoundaryFlux) > 1e-12,
                $"cell {l.Cell}: changed with no boundary flux"));
        Output.WriteLine(sb.ToString());
        Output.WriteLine($"  moving single cells: {moving} of {oneCells.Length}; none changed without a boundary flux.");
    }

    [Fact]
    public void Y_G_078_03_MaximumLocalEnhancement_UnderCountConservation()
    {
        PrintHeader("G_078 - Q3: the maximum achievable local enhancement");
        var sb = new StringBuilder();
        sb.AppendLine("THE BOUND COMES FROM THE COUNT AND FROM POSITIVITY, NOT FROM ANY DYNAMICS");
        sb.AppendLine("  occupancy is positive, so no cell can hold more than the total, and no distribution can beat it.");
        sb.AppendLine("  The uplift cap is tested in TWO READINGS, and the contrast cap is tested BRANCH BY BRANCH, because");
        sb.AppendLine("  this audit's first formulation of an 'unbounded depression' did not survive the empty-cell question.");
        sb.AppendLine();
        sb.Append(SourceManipulationAudit.OutputExtremes());

        var caps = SourceManipulationAudit.UpliftCap(96, 96);
        var open = caps.Single(c => !c.FloorAssumed);
        var floor = caps.Single(c => c.FloorAssumed);
        Assert.Equal(96.0, open.MaxOccupancy, 12);
        Assert.Equal(96.0, open.RateRatio * open.RateRatio * open.RateRatio, 9);
        Assert.Equal(Math.Log(96.0) / 3.0, open.Uplift, 12);
        Assert.Equal(Math.Log(97.0) / 3.0, floor.Uplift, 12);

        // THE ROBUSTNESS CHECK: the uplift cap barely moves between the two readings. A cap that moved with the
        // reading would be an artifact of the reading.
        double relative = Math.Abs(floor.Uplift - open.Uplift) / open.Uplift;
        Assert.True(relative < 0.01, $"the uplift cap must be robust to the reading, but moved {relative:E3}");
        sb.AppendLine($"  uplift cap under the two readings: {Num(open.Uplift)} and {Num(floor.Uplift)} - "
            + $"a relative difference of {Num(relative)}, so the cap is a property of positivity.");

        // the uplift grows only LOGARITHMICALLY with the available count: each e^3 = 20.09x of count adds exactly 1
        var growth = new[] { 4, 16, 48, 96 }.Select(m => SourceManipulationAudit.UpliftCap(m, m).Single(c => !c.FloorAssumed)).ToArray();
        for (int i = 1; i < growth.Length; i++)
            Assert.Equal(Math.Log(growth[i].MaxOccupancy / growth[i - 1].MaxOccupancy) / 3.0,
                growth[i].Uplift - growth[i - 1].Uplift, 12);
        // e^3 = 20.0855x of the count must add exactly 1.0 to the potential
        double decade = SourceManipulationAudit.UpliftCap(1928, 0).Single(c => !c.FloorAssumed).Uplift
                      - SourceManipulationAudit.UpliftCap(96, 0).Single(c => !c.FloorAssumed).Uplift;
        Assert.Equal(1.0, decade, 3);
        sb.AppendLine("  and the uplift is LOGARITHMIC in the count: each 20.09x of count (e^3) adds exactly 1.0 to the potential.");

        // THE CONTRAST IS CONDITIONAL. Under a unit floor it is capped by the same number as the uplift; only an
        // empty cell makes it diverge, and then the divergence is the sector's logarithm, not the count.
        var branches = SourceManipulationAudit.ContrastBranches(96, 96);
        var floorBranch = branches.Single(b => b.Branch.StartsWith("unit floor"));
        var openBranch = branches.Single(b => b.Branch.StartsWith("empty cell"));
        Assert.True(double.IsInfinity(openBranch.MaxContrast));
        Assert.False(double.IsInfinity(floorBranch.MaxContrast));
        // under a floor the maximum CONTRAST has the same POTENTIAL DIFFERENCE as the maximum UPLIFT: the asymmetry
        // between the two sides is what disappears, and this identity is the measured form of that disappearance
        Assert.Equal(floor.Uplift, Math.Log(floorBranch.MaxContrast), 12);
        Assert.Equal(floor.RateRatio, floorBranch.MaxContrast, 12);
        sb.AppendLine($"  THE SELF-REFUTATION: under a floor the contrast potential difference is {Num(floor.Uplift)}, the SAME as "
            + $"the uplift cap (rate ratio {Num(floorBranch.MaxContrast)} vs {Num(floor.RateRatio)}) - so the asymmetry "
            + $"VANISHES under a floor; only the empty-cell branch diverges, at {openBranch.MaxContrast}.");

        // the floorless branch, kept as the illustration of where the divergence comes from - and labelled as such
        var contrast = SourceManipulationAudit.NoFloorContrast(2.0);
        Assert.All(contrast, c => Assert.Equal(2.0, c.Total, 12));
        Assert.All(contrast, c => Assert.Equal(Math.Pow(c.HillOccupancy / c.WellOccupancy, 1.0 / 3.0), c.RateRatio, 9));
        for (int i = 1; i < contrast.Length; i++)
            Assert.True(contrast[i].RateRatio > contrast[i - 1].RateRatio, "each deeper well must raise the contrast");
        // and the growth per 100x of well depth is the CUBE ROOT of a hundred (4.64), not a hundred: a claim of a
        // linear response would be wrong, so the closed form is checked rather than described
        double depthRatio = (contrast[^1].HillOccupancy / contrast[^2].HillOccupancy)
                          * (contrast[^2].WellOccupancy / contrast[^1].WellOccupancy);
        Assert.Equal(Math.Pow(depthRatio, 1.0 / 3.0), contrast[^1].RateRatio / contrast[^2].RateRatio, 9);
        Assert.True(depthRatio > 99.0, "the last two rows must differ by ~100x of depth for this check to mean anything");
        Assert.True(contrast[^1].RateRatio / contrast[^2].RateRatio < 5.0,
            "the contrast grows only as the cube root of the depth ratio, never as the depth ratio");
        sb.AppendLine($"  and {Num(depthRatio)}x of well depth raises the contrast by only {Num(contrast[^1].RateRatio / contrast[^2].RateRatio)}x -");
        sb.AppendLine("  the cube root, because A = (1/d) ln rho and d = 3. A linear response would have been wrong.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_078_04_BoundedRegion_CannotChangeAlone()
    {
        PrintHeader("G_078 - Q4: can a bounded region create delta-rho with no compensating deficit?");
        var sb = new StringBuilder();
        sb.AppendLine("THE LEDGER TEST, RUN ON MANY FLUX PATTERNS RATHER THAN ONE");
        sb.AppendLine("  if a bounded region could change its count without a compensating deficit, its change would have to");
        sb.AppendLine("  differ from minus its net boundary flux. The audit measures every region of several patterns.");
        sb.AppendLine();

        var patterns = new[]
        {
            SourceManipulationAudit.LedgerPattern,
            new[] { 0.0, 0.0, 0.1, -0.1, 0.0, 0.0, 0.0, 0.0 },
            new[] { 0.3, -0.1, 0.0, 0.2, -0.2, 0.0, -0.1, 0.1 },
        };
        double worst = 0.0; int rows = 0; int changedAlone = 0;
        foreach (var flux in patterns)
        {
            sb.AppendLine($"  pattern flux ({string.Join(", ", flux)})");
            foreach (var l in SourceManipulationAudit.RegionLedger(flux))
            {
                rows++;
                worst = Math.Max(worst, l.Residual);
                if (Math.Abs(l.ChangeInside) > 1e-12 && Math.Abs(l.NetBoundaryFlux) < 1e-12) changedAlone++;
                sb.AppendLine($"    {l.Region,-15} inside {Num(l.ChangeInside),10}   flux {Num(l.NetBoundaryFlux),10}   residual {Num(l.Residual)}");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"  rows measured {rows}, worst residual {Num(worst)}, regions changed with no boundary flux {changedAlone}");
        sb.AppendLine("  the whole ring is the ONE region with no boundary - and there the change is zero, because the");
        sb.AppendLine("  transport is a divergence. So the answer is: the change must be paid for, and WHERE it is paid for is");
        sb.AppendLine("  free; the flux decides the distance, which is an engineering choice and not a theorem.");

        sb.AppendLine();
        sb.AppendLine("  the ledger rows must be NON-TRIVIAL: a table of zeros would prove nothing.");
        Assert.All(SourceManipulationAudit.RegionLedger(SourceManipulationAudit.LedgerPattern),
            l => Assert.True(l.Residual < 1e-12, $"{l.Region}: residual must vanish"));
        var nonZero = SourceManipulationAudit.RegionLedger(SourceManipulationAudit.LedgerPattern)
            .Where(l => !l.Region.StartsWith("the whole")).ToArray();
        Assert.All(nonZero, l => Assert.True(Math.Abs(l.ChangeInside) > 1e-12,
            $"{l.Region}: the ledger row is trivial and proves nothing"));

        Assert.True(worst < 1e-12);
        Assert.Equal(0, changedAlone);
        // the claim "a bounded region can change alone" is REFUTED, and the refutation is measured, not asserted
        var bounded = SourceManipulationAudit.MirrorImplication().Single(q => q.Question.StartsWith("a bounded region"));
        Assert.False(bounded.Answer);
        Assert.Contains("cannot change alone", bounded.Measurement);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_078_05_EveryGravitationalManipulation_CarriesAClockSignature()
    {
        PrintHeader("G_078 - Q5: does every gravitational manipulation create a clock signature?");
        var sb = new StringBuilder();
        sb.AppendLine("THE IMPLICATION IS MEASURED ACROSS THE CLOCK-LAW CORPUS, NOT FOR ONE LAW");
        sb.AppendLine("  a change of the acceleration field requires a gradient of the occupancy; the clock is a STRICTLY");
        sb.AppendLine("  MONOTONE readout of the occupancy. The audit tests the implication wherever the acceleration changes.");
        sb.AppendLine();
        sb.Append(SourceManipulationAudit.OutputImplications());

        var gravity = SourceManipulationAudit.GravityImpliesClock();
        Assert.All(gravity, g => Assert.True(g.ImplicationHolds,
            $"{g.Law}: an acceleration change with no clock change was found"));
        Assert.Equal(0, gravity.Sum(g => g.AccelerationWithoutClock));
        Assert.True(gravity.Length >= 9, "the implication must be tested across the whole corpus");

        sb.AppendLine($"  VERDICT: YES - {gravity.Length} laws x 4 depths, "
            + $"{gravity.Sum(g => g.AccelerationWithoutClock)} counterexamples. A gravitational manipulation that left");
        sb.AppendLine("  every clock unchanged would need a step in the acceleration with no step in the occupancy, and the");
        sb.AppendLine("  acceleration IS the occupancy's gradient in this sector.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_078_06_EveryClockManipulation_CarriesAGravitationalSignature_ExceptOne()
    {
        PrintHeader("G_078 - Q6: does every clock manipulation create a gravitational signature?");
        var sb = new StringBuilder();
        sb.AppendLine("THE CONVERSE HAS EXACTLY ONE EXCEPTION, AND COUNT CONSERVATION FORBIDS IT");
        sb.AppendLine();
        sb.Append(SourceManipulationAudit.OutputImplications());

        var escape = SourceManipulationAudit.UniformEscape(new[] { 8, 96 });
        Assert.All(escape, u =>
        {
            Assert.True(u.ClockChange > 1e-6, "the uniform direction must move the clock");
            Assert.True(u.AccelerationChange < 1e-12, "the uniform direction must not move the acceleration");
            Assert.True(Math.Abs(u.CountChange) > 1e-6, "the uniform direction must change the total count");
        });

        var mirror = SourceManipulationAudit.MirrorImplication();
        var bounded = mirror.Single(q => q.Question.StartsWith("a bounded region"));
        Assert.False(bounded.Answer, "the audit REFUTES that a bounded region can change alone");

        sb.AppendLine("  SO: every clock manipulation that count conservation permits DOES carry a gravitational signature;");
        sb.AppendLine("  the one move that would not (a uniform scaling) is exactly the move that changes the total, so it is");
        sb.AppendLine("  not a manipulation of the conserved source at all. The theory closes the one channel that would have");
        sb.AppendLine("  separated time from gravity.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_078_07_Verdict_IsComputed_AndRefutes()
    {
        PrintHeader("G_078 - verdict");
        var sb = new StringBuilder();
        sb.Append(SourceManipulationAudit.OutputVerdict());

        string verdict = SourceManipulationAudit.Verdict();
        Assert.StartsWith("REFUTED", verdict);
        Assert.Contains("REDISTRIBUTION IS THE ONLY ELEMENTARY CHANNEL", verdict);
        // the verdict's figures are checked as NUMBERS, never by parsing its prose (G_075 rule 3)
        var figures = SourceManipulationAudit.VerdictFigures();
        var floorCap = SourceManipulationAudit.UpliftCap(96, 96).Single(c => c.FloorAssumed);
        Assert.Equal(floorCap.Uplift, figures.Uplift, 12);
        Assert.Equal(floorCap.RateRatio, figures.RateRatio, 12);
        Assert.Equal(floorCap.MetricRatio, figures.MetricRatio, 12);
        // and the conditional structure must be visible in the figures themselves: bounded under a floor, divergent without
        Assert.True(figures.ContrastIsBoundedUnderAFloor);
        Assert.True(double.IsInfinity(figures.NoFloorContrast));
        Assert.False(double.IsInfinity(figures.FloorContrast));
        Assert.Equal(figures.Uplift, Math.Log(figures.FloorContrast), 12);
        Assert.Equal(figures.RateRatio, figures.FloorContrast, 12);
        // the verdict must carry its own self-refutation, not only the positive claim
        Assert.Contains("REFUTES THE OBVIOUS FORMULATION", verdict);
        Output.WriteLine(sb.ToString());
        Output.WriteLine($"  verdict figures (numbers, not parsed prose): uplift {Num(figures.Uplift)}, rate {Num(figures.RateRatio)}, "
            + $"metric {Num(figures.MetricRatio)}, contrast under a floor {Num(figures.FloorContrast)}, "
            + $"contrast without a floor {figures.NoFloorContrast}");
    }

    [Fact]
    public void Y_G_078_08_Boundary_TheSourceTermIsAbsent_NotImpossible()
    {
        PrintHeader("G_078 - boundary analysis: what the audit does and does not prove");
        var sb = new StringBuilder();
        sb.Append(SourceManipulationAudit.WhereItStands());

        var census = SourceManipulationAudit.ChannelCensus();
        var sourceRow = census.Single(c => c.Channel == "a source term");
        Assert.Contains("IMPORT", sourceRow.Status);

        // the honest limits must be present in the audit's own report, not only in prose elsewhere
        string standing = SourceManipulationAudit.WhereItStands();
        Assert.Contains("ABSENT", standing);
        Assert.Contains("ARTIFACT OF THE EMPTY CELL", standing);
        Assert.Contains("CONSTRAINT", standing);
        Assert.Contains("CONVENTION-SENSITIVE", standing);

        // and the deep-field claim must not be presented as measured
        sb.AppendLine();
        sb.AppendLine("  TESTED: the only regime where the contrast runs away is rho -> 0, i.e. the deep field, and G_075");
        sb.AppendLine("  measured that no surviving datum probes it. So the unbounded contrast is a statement about the");
        sb.AppendLine("  sector's structure, and the audit says so rather than trading on it.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_078_09_Reproducible()
    {
        PrintHeader("G_078 - reproducibility");
        var sb = new StringBuilder();
        sb.AppendLine("  Every figure in this suite is computed from the two sector statements");
        sb.AppendLine("      A = (1/d) ln rho,  d = 3          (the source law, G_028/G_035)");
        sb.AppendLine("      the clock is a strictly monotone readout of A   (G_077)");
        sb.AppendLine("  and the audit is deterministic: the operator class, the ledger, the extremes and the implication table");
        sb.AppendLine("  are fixed enumerations with no randomness and no external data.");

        var a = SourceManipulationAudit.Verdict();
        var b = SourceManipulationAudit.Verdict();
        Assert.Equal(a, b);
        Assert.Equal(SourceManipulationAudit.WhereItStands(), SourceManipulationAudit.WhereItStands());
        Output.WriteLine(sb.ToString());
    }
}
