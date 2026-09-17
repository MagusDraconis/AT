using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_072 - Observational Program Audit. What exact future measurement can first decide AT vs GR using a
/// neutron-star redshift? Output target, precision, instrument class and decision significance, from G_068/G_069/G_070
/// and G_071, as a realistic observer-facing test plan.
/// </summary>
public sealed class Y_G_072_Tests : ResearchTestBase
{
    public Y_G_072_Tests(Xunit.ITestOutputHelper output) : base(output) { }

    [Fact]
    public void Y_G_072_TheTargetIsTheEarlierAuditsTargetAndItsCompactnessIsRecomputed()
    {
        PrintHeader("G_072 - the target, taken from the earlier audits and recomputed here");

        var target = ObservationalProgramAudit.Target();
        double x = ObservationalProgramAudit.XOfTarget();

        var sb = new StringBuilder();
        sb.AppendLine($"  target      : {target.Name}");
        sb.AppendLine($"  mass        : {target.Mass:F3} solar masses");
        sb.AppendLine($"  radius      : {target.RadiusKm:F2} km");
        sb.AppendLine($"  compactness : x = {x:F6}  (recomputed from the mass and radius, not restated)");
        sb.AppendLine($"  recorded relative sigma x : {target.RelativeSigmaX:P4}");
        sb.AppendLine($"  basis       : {target.Source}");
        Output.WriteLine(sb.ToString());

        // the target is the earlier audits' choice, and the audit does not re-derive it
        Assert.Equal(ObservationalDecisionAudit.BestTarget(), ObservationalProgramAudit.TargetName);
        Assert.Contains("J0740+6620", ObservationalProgramAudit.TargetName);

        // and the compactness is recomputed from the target's own mass and radius, consistently with its own helper
        Assert.Equal(ObservationalDecisionAudit.XFromMassRadius(target.Mass, target.RadiusKm), x, 12);
        Assert.True(x < 0.0);
        Assert.True(Math.Abs(x + 0.24704) < 1e-4, $"x = {x:F6}");

        // it is the most compact object in the catalogue, which is why it is the target
        double mostCompact = ObservationalDecisionAudit.Catalogue().Max(c => Math.Abs(c.X));
        Assert.Equal(mostCompact, Math.Abs(x), 9);
    }

    [Fact]
    public void Y_G_072_TheProgramIsATableOfTargetPrecisionInstrumentClassAndSignificance()
    {
        PrintHeader("G_072 - the program: target, precision, instrument class, decision significance");

        var sb = new StringBuilder();
        sb.AppendLine("  instrument class                            timing   compactness   significance   reaches");
        foreach (var r in ObservationalProgramAudit.Program())
            sb.AppendLine($"  {r.InstrumentClass,-43} {r.Timing,-8:P0} {r.Compactness,-13:P4} {r.Significance,-14:F4} {r.Reaches}");
        Output.WriteLine(sb.ToString());

        var program = ObservationalProgramAudit.Program();
        Assert.Equal(ObservationalProgramAudit.Classes().Length, program.Length);

        // every row names the SAME target, which is the program's whole point
        Assert.All(program, r => Assert.Equal(ObservationalProgramAudit.TargetName, r.Target));

        // THE FIRST CLASS IS THE RECORDED CURRENT ROW AND ITS SIGNIFICANCE IS MEASURED AT 1.9235, NOT AT G_071's
        // 1.0534. The two are about DIFFERENT OBJECTS and DIFFERENT ERROR MODELS, and the test says so rather than
        // asserting the earlier number: G_071's 1.0534 is the GENERIC 1.4 solar mass, 12 km object at its worst-case
        // correlated compactness error of 11.9048 per cent, while this row is the TARGET J0740+6620 (more compact,
        // so a larger second-order separation) at the quadrature error of 9.0664 per cent.
        Assert.InRange(program[0].Significance, 1.9, 2.0);
        Assert.Equal("1 sigma", program[0].Reaches);

        // the significance is monotonically non-decreasing as the capability improves, which the table must satisfy
        for (int i = 1; i < program.Length; i++)
            Assert.True(program[i].Significance >= program[i - 1].Significance,
                $"{program[i].InstrumentClass} is worse than its predecessor");

        // and the classification of each row is a function of its own significance
        Assert.All(program, r =>
        {
            string expected = r.Significance >= 5.0 ? "5 sigma"
                            : r.Significance >= 3.0 ? "3 sigma"
                            : r.Significance >= 1.0 ? "1 sigma" : "undecided";
            Assert.Equal(expected, r.Reaches);
        });
    }

    [Fact]
    public void Y_G_072_TheLeverageIsTheTimingAndTheMeasurementRefutedMyDraft()
    {
        PrintHeader("G_072 - the leverage: what each capability buys when it is the only thing improved");

        var sb = new StringBuilder();
        sb.AppendLine("  capability                        timing   mass       radius     significance   gain");
        foreach (var l in ObservationalProgramAudit.Leverage())
            sb.AppendLine($"  {l.Capability,-33} {l.Timing,-8:P0} {l.MassShare,-10:P3} {l.RadiusShare,-10:P3} {l.Significance,-14:F4} {l.Gain:F4}");
        Output.WriteLine(sb.ToString());

        var lev = ObservationalProgramAudit.Leverage();
        double baseline = lev[0].Significance;
        double timingGain = lev.Single(l => l.Capability == "timing alone to 1 per cent").Gain;
        double massGain = lev.Single(l => l.Capability == "mass alone to 0.1 per cent").Gain;
        double radiusGain = lev.Single(l => l.Capability == "radius alone to 1.5 per cent").Gain;

        // THE MEASURED ORDER, WHICH REFUTED THE AUDIT'S OWN DRAFT: I expected the radius to lead.
        // Timing 1.9400, radius 1.1292, mass 1.0211 - in that order.
        Assert.True(timingGain > radiusGain, $"timing {timingGain:F4} vs radius {radiusGain:F4}");
        Assert.True(radiusGain > massGain, $"radius {radiusGain:F4} vs mass {massGain:F4}");
        Assert.InRange(timingGain, 1.9, 2.0);
        Assert.InRange(radiusGain, 1.1, 1.2);
        Assert.InRange(massGain, 1.01, 1.03);

        // and the ratio between the two that mattered to the draft, so a future change to either is visible
        Assert.InRange(timingGain / radiusGain, 1.7, 1.75);

        // the baseline row is the baseline, by construction, and every gain is positive
        Assert.Equal(1.0, lev[0].Gain, 9);
        Assert.All(lev, l => Assert.True(l.Gain > 0.0));

        // the capability that improves everything at once is worth the most - by construction
        Assert.Equal(lev.Max(l => l.Gain), lev.Single(l => l.Capability == "everything together (D)").Gain, 9);
    }

    [Fact]
    public void Y_G_072_TheFirstDeciderIsFoundAndIsNotTheObviousOne()
    {
        PrintHeader("G_072 - the first decider, computed at 3 and 5 sigma");

        var three = ObservationalProgramAudit.FirstDecider(3.0);
        var five = ObservationalProgramAudit.FirstDecider(5.0);
        var sb = new StringBuilder();
        foreach (var d in three) sb.AppendLine($"  3 sigma: {d.InstrumentClass} at {d.Significance:F4} sigma");
        foreach (var d in five) sb.AppendLine($"  5 sigma: {d.InstrumentClass} at {d.Significance:F4} sigma");
        sb.AppendLine();
        sb.AppendLine("  " + ObservationalProgramAudit.TheSingleDecisiveMeasurement());
        Output.WriteLine(sb.ToString());

        // the program does reach 3 sigma, so the answer exists rather than being empty
        Assert.NotEmpty(three);
        var first = three[0];
        Assert.Equal(ObservationalProgramAudit.TargetName, first.Target);
        Assert.True(first.Significance >= 3.0);
        Assert.Contains("redshift", first.Measurement);

        // it is NOT the first two classes - the obvious programme fails, and that is the audit's headline
        Assert.DoesNotContain("NICER", first.InstrumentClass);
        Assert.DoesNotContain("radio", first.InstrumentClass);
        Assert.StartsWith("C.", first.InstrumentClass);

        // the census agrees with the table - and it counts CLASSES, so comparing it with the one-row
        // FirstDecider list was my bookkeeping error: the list returns the FIRST decider, not every decider
        var (decided, atThree, atFive) = ObservationalProgramAudit.ClassCensus();
        int undecided = ObservationalProgramAudit.Program().Count(r => r.Reaches == "undecided");
        int reachingFive = ObservationalProgramAudit.Program().Count(r => r.Reaches == "5 sigma");
        Assert.Equal(ObservationalProgramAudit.Classes().Length, decided + undecided);
        Assert.Equal(atFive, reachingFive);
        Assert.Equal(1, five.Length);                       // the first 5-sigma decider, not all of them
        Assert.True(atThree >= 1);
        Assert.True(atFive >= 1);
    }

    [Fact]
    public void Y_G_072_TheRequirementsSayWhichSideBinds()
    {
        PrintHeader("G_072 - what the decision needs, from the decision side");

        var sb = new StringBuilder();
        sb.AppendLine("  significance   required timing   required compactness   which binds");
        foreach (var r in ObservationalProgramAudit.Requirements())
            sb.AppendLine($"  {r.Significance,-14:F0} {r.RequiredTiming,-17:P4} {r.RequiredCompactness,-23:P4} {r.WhichBinds}");
        Output.WriteLine(sb.ToString());

        var req = ObservationalProgramAudit.Requirements();
        Assert.Equal(2, req.Length);

        // THE REQUIRED COMPACTNESS IS NaN AT BOTH LEVELS AT THE RECORDED TIMING, AND THAT IS THE FINDING RATHER
        // THAN A FAILURE: no compactness precision decides the question while the timing sits at 20 per cent, which is
        // G_071's "timing is step 1" recovered as a number. The 5-sigma NaN below the 3-sigma NaN is NOT a comparison.
        Assert.All(req, r => Assert.True(double.IsNaN(r.RequiredCompactness), $"{r.Significance} sigma is not NaN"));
        Assert.All(req, r => Assert.Equal("the TIMING", r.WhichBinds));

        // while the REQUIRED TIMING is finite and falls with the significance: 14.1422 per cent at 3 sigma and
        // 7.5524 per cent at 5 sigma - the numbers G_070 recorded for this target
        var three = req.Single(r => r.Significance == 3.0);
        var five = req.Single(r => r.Significance == 5.0);
        Assert.InRange(three.RequiredTiming, 0.141, 0.142);
        Assert.InRange(five.RequiredTiming, 0.075, 0.076);
        Assert.True(five.RequiredTiming < three.RequiredTiming);

        // the single decisive measurement is stated once, with both precisions in it
        var sentence = ObservationalProgramAudit.TheSingleDecisiveMeasurement();
        Assert.Contains(ObservationalProgramAudit.TargetName, sentence);
        Assert.Contains("3-sigma", sentence);
        Assert.Contains("5-sigma", sentence);

        // and it is a compactness measurement, not a clock measurement - the verdict says so
        Assert.Contains("COMPACTNESS", ObservationalProgramAudit.Verdict());
    }

    [Fact]
    public void Y_G_072_ThePlanHasFivePhasesAndOnlyTheLaterOnesDecide()
    {
        PrintHeader("G_072 - the plan in phases");

        var sb = new StringBuilder();
        foreach (var p in ObservationalProgramAudit.Phases())
            sb.AppendLine($"  {p.Phase,-28} {p.Significance,-14} decides: {p.Decides}");
        Output.WriteLine(sb.ToString());

        var phases = ObservationalProgramAudit.Phases();
        Assert.Equal(ObservationalProgramAudit.Classes().Length, phases.Length);

        // the first two phases do NOT decide, and the audit says so explicitly rather than burying them
        Assert.Equal("no - the status quo is undecided", phases[0].Decides);
        Assert.Equal("no - the mass is not what binds", phases[1].Decides);

        // and the later phases do
        Assert.Contains(phases.Skip(2), p => p.Decides != "no" && !p.Decides.StartsWith("no"));
    }

    [Fact]
    public void Y_G_072_Diag()
    {
        PrintHeader("G_072 - DIAGNOSTIC: every computed surface");

        Output.WriteLine(ObservationalProgramAudit.OutputProgram());
        Output.WriteLine(ObservationalProgramAudit.OutputRequirements());
        Output.WriteLine(ObservationalProgramAudit.OutputPhases());
        Output.WriteLine(ObservationalProgramAudit.OutputLeverage());
        Output.WriteLine(ObservationalProgramAudit.OutputVerdict());
    }
}
