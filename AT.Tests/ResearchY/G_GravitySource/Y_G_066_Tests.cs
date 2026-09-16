using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhysicalFlowAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_066 - Physical Flow Audit (group G - Gravity Source).
///
/// QUESTION. Which update rule is physically privileged? Compare the forward difference, the backward difference, the
/// centred difference, the exact flow and the Cayley flow; measure positivity, norm conservation, phase evolution,
/// attractors and compatibility with the AT laws. Goal: is phase-freeness a property of AT itself, or only of one chosen
/// flow?
///
/// ANSWER: **REFUTED - phase-freeness is a property of the DISSIPATIVE flows, not of AT: an admissible, law-abiding,
/// total-conserving flow preserves the phase content indefinitely.**
/// </summary>
public class Y_G_066_Tests : ResearchTestBase
{
    public Y_G_066_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_066_TheAdmissibleSetSplitsInTwo()
    {
        var table = MeasureTable();
        Assert.Equal(5, table.Length);

        // four of the five are admissible; the amplifying one drives cells negative and is out
        Assert.Equal(4, AdmissibleFlows().Length);
        Assert.DoesNotContain("backward difference", AdmissibleFlows());
        Assert.False(table.Single(t => t.Flow == "backward difference").Admissible);
        Assert.True(table.Single(t => t.Flow == "backward difference").MinCellCanonical < 0);

        // and the admissible four do NOT agree: two erase the state and two preserve it
        Assert.Equal(new[] { "exact flow exp(eps D)", "forward difference" },
            FlowsThatEraseTheState().OrderBy(x => x).ToArray());
        Assert.Equal(new[] { "centred (skew) difference", "unitary (Cayley of the skew part)" },
            FlowsThatPreserveTheState().OrderBy(x => x).ToArray());
    }

    [Fact]
    public void Y_G_066_AnAdmissibleFlowPreservesPhaseContentIndefinitely()
    {
        var longRun = PhaseRatioTable(Horizon * 2).ToDictionary(t => t.Flow, t => t.PhaseRatio);

        // the two dissipative forms take the phase content towards zero
        Assert.InRange(longRun["forward difference"], 0.0, 0.3);
        Assert.InRange(longRun["exact flow exp(eps D)"], 0.0, 0.3);

        // the centred and unitary forms keep it, and the unitary one is admissible, law-abiding and total-conserving
        Assert.True(longRun["unitary (Cayley of the skew part)"] > 0.5);
        Assert.True(longRun["centred (skew) difference"] > 0.5);
        Assert.True(AnAdmissibleFlowPreservesPhaseContentIndefinitely());
        Assert.False(EveryAdmissibleFlowDrivesToPhaseFreeness());
        Assert.Equal(2, FlowsThatPreservePhaseContent().Length);
    }

    [Fact]
    public void Y_G_066_PhaseFreenessIsNotAPropertyOfAT()
    {
        Assert.False(EveryAdmissibleFlowDrivesToPhaseFreeness());
        Assert.True(AnAdmissibleFlowPreservesPhaseContentIndefinitely());
        Assert.Equal("REFUTED", Verdict());
        Assert.Contains("DISSIPATIVE flows", TheAnswer());
        Assert.Contains("not of AT", TheAnswer());
    }

    [Fact]
    public void Y_G_066_TheDissipativeFormsEraseEverythingNotOnlyThePhase()
    {
        var table = MeasureTable();
        foreach (var flow in new[] { "forward difference", "exact flow exp(eps D)" })
        {
            var row = table.Single(t => t.Flow == flow);
            // the DEVIATION decays as well as the phase: the attractor is the uniform state, not merely a phase-free one
            Assert.InRange(row.DeviationRatio, 0.0, 0.5);
            Assert.InRange(row.PhaseRatio, 0.0, 0.5);
            Assert.True(row.PhaseRatio < row.DeviationRatio + 0.2);
        }

        // the two forms agree to three digits - the exact flow IS the forward step's flow
        double forward = table.Single(t => t.Flow == "forward difference").DeviationRatio;
        double exact = table.Single(t => t.Flow == "exact flow exp(eps D)").DeviationRatio;
        Assert.InRange(Math.Abs(forward - exact), 0.0, 1e-3);
    }

    [Fact]
    public void Y_G_066_TheLawsRuleOutTheAmplifyingForm()
    {
        var compatibility = LawCompatibility();
        Assert.Equal(5, compatibility.Length);

        // the amplifying form is ruled out BY the laws: it drives cells negative
        Assert.True(TheLawsRuleOutTheAmplifyingForm());
        Assert.True(compatibility.Single(t => t.Flow == "backward difference").WorstLawResidual > 0.1);
        Assert.True(compatibility.Single(t => t.Flow == "backward difference").MinCell < 0);

        // every admissible form keeps them, and the uniform state is stationary for every form
        Assert.True(EveryAdmissibleFlowKeepsTheAtLaws());
        Assert.True(TheUniformStateIsStationaryForEveryForm());
        Assert.True(EveryFormConservesTheTotal());
        Assert.True(TheAmplifyingFormLosesTheTotalToFloatingPoint());
    }

    [Fact]
    public void Y_G_066_NoFormIsPrivilegedByTheUsualMeasures()
    {
        // every form fixes the uniform state and conserves the total, so those measures do not separate them
        Assert.True(TheUniformStateIsStationaryForEveryForm());
        Assert.True(EveryFormConservesTheTotal());

        // and the phase-free state is NOT the common attractor: four admissible forms, two attractors
        var attractors = MeasureTable().Where(t => t.Admissible).Select(t => t.LongRun).Distinct().ToArray();
        Assert.Equal(2, attractors.Length);
        Assert.Contains("DECAYS TO UNIFORM", attractors);
        Assert.Contains("PRESERVES THE STATE", attractors);
    }

    [Fact]
    public void Y_G_066_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_066 - Physical Flow Audit: which update rule is privileged?");

        sb.AppendLine("QUESTION. Which update rule is PHYSICALLY PRIVILEGED?");
        sb.AppendLine("COMPARE      forward difference | backward difference | centred difference | exact flow | Cayley flow");
        sb.AppendLine("MEASURE      positivity, norm conservation, phase evolution, attractors, law compatibility");
        sb.AppendLine("GOAL         is phase-freeness a property of AT itself, or only of one chosen flow?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Every form is circulant, so one complex multiplier per channel is the whole update; the forms are");
        sb.AppendLine("     keyed to the mechanism PhaseEvolutionAudit implements so that one multiplier drives analysis and");
        sb.AppendLine("     iteration alike. The question\'s labels are carried in the kind.");
        sb.AppendLine("  2. LONG-RUN behaviour is measured from the PHASE-BEARING state, because a phase ratio is meaningless");
        sb.AppendLine("     from a phase-free start; positivity is measured from both starts.");
        sb.AppendLine("  3. A form is ADMISSIBLE when it keeps every cell positive from both starts over the horizon tested -");
        sb.AppendLine("     which is the density constraint rather than a matter of taste.");
        sb.AppendLine("  4. Deterministic throughout; eps = 1E-3 unless a row says otherwise.");
        sb.AppendLine();

        PrintHeader(OutputMeasures());
        PrintHeader(OutputCompatibility());
        PrintHeader(OutputDecisive());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
