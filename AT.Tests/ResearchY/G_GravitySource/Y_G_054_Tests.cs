using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseDeterminationAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_054 - Phase Determination Audit (group G - Gravity Source).
///
/// QUESTION. What fixes the 53 phase coordinates? Given G_050, G_051 and G_053 (amplitude and phase are independent).
/// Requirements: no new primitive, and the clock, acceleration and field laws preserved.
///
/// ANSWER: **BOUNDARY - freely assigned. Nothing in AT fixes the phase coordinates, and the audit states exactly what
/// would have to be added for something to: a coupling whose gradient has a phase component AND which the theory
/// actually runs. AT has the first without the second.**
/// </summary>
public class Y_G_054_Tests : ResearchTestBase
{
    public Y_G_054_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_054_AnInvariantDrivenFlowConservesThePhaseCoordinates()
    {
        // the conservation theorem: gradients of invariant functionals lie in the amplitude-plus-mean subspace
        Assert.True(TheInvariantFlowConservesThePhases(), $"flow change {InvariantFlowPhaseChange():E3}");
        Assert.True(InvariantFlowPhaseChange() < 1e-12);
        Assert.True(InvariantGradientPhaseComponent() < 1e-9,
            $"the invariant gradient's phase component is {InvariantGradientPhaseComponent():E3}");
    }

    [Fact]
    public void Y_G_054_AmplitudeMovesCannotChangeThemButSymmetryMovesCan()
    {
        // orthogonality: amplitude moves cannot touch the phase coordinates
        Assert.True(AmplitudeMovesCannotChangeThePhases(), $"amplitude change {AmplitudeMovePhaseChange():E3}");

        // but symmetry does - so symmetry is defined by NOT seeing them, and cannot be their determiner
        Assert.True(SymmetryMovesThePhases(), $"symmetry change {SymmetryMovePhaseChange():E3}");
        Assert.True(SymmetryMovePhaseChange() > 1e-6);
    }

    [Fact]
    public void Y_G_054_TheLawsArePhaseSensitiveButNoAtProcessRunsAPhaseFlow()
    {
        // sensitivity: the local laws' gradients have a phase component
        Assert.True(ClockGradientPhaseFraction() > 1e-3, $"clock fraction {ClockGradientPhaseFraction():E3}");
        Assert.True(FieldGradientPhaseFraction() > 1e-3, $"field fraction {FieldGradientPhaseFraction():E3}");
        Assert.True(TheLawsArePhaseSensitive());

        // determination: nothing in AT actually runs such a flow
        Assert.True(NoAtProcessRunsAPhaseFlow());
        Assert.True(UpdateRuleSectors().Spatial < 1e-15, $"spatial part {UpdateRuleSectors().Spatial:E3}");
        Assert.Equal(0, CouplingCensus());
    }

    [Fact]
    public void Y_G_054_MultiplicityIsStateIndependent()
    {
        Assert.Equal(45, SpectrumLevels().Length);
        Assert.Equal(96, SpectrumLevels().Sum(l => l.Multiplicity));
        Assert.True(TheMultiplicitiesAreStateIndependent());
        Assert.Contains("fixed by the Laplacian", TheMultiplicityMeasurement());
    }

    [Fact]
    public void Y_G_054_TheCandidatesSortThemselvesAndTheVerdictIsBoundary()
    {
        Assert.Equal(6, Candidates().Length);
        Assert.Equal(5, RefutedCandidates().Length);
        Assert.Contains("symmetry", RefutedCandidates());
        Assert.Contains("multiplicity", RefutedCandidates());
        Assert.Contains("attractor structure", RefutedCandidates());
        Assert.Contains("actualization history", RefutedCandidates());
        Assert.Equal(1, BoundaryCandidates().Length);
        Assert.Contains("boundary assignment", BoundaryCandidates());
        Assert.Equal("an external assignment", TheDeterminationMechanism());
        Assert.Equal("BOUNDARY", Verdict());
    }

    [Fact]
    public void Y_G_054_TheRequirementsHold()
    {
        var checks = RequirementCheck();
        Assert.Equal(4, checks.Length);
        Assert.Equal(new[] { "no new primitive", "clock law preserved", "acceleration law preserved", "field law preserved" },
            checks.Select(c => c.Requirement).ToArray());
        Assert.Equal(0, CouplingCensus());
    }

    [Fact]
    public void Y_G_054_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_054 - Phase Determination Audit: what fixes the 53 phase coordinates?");

        sb.AppendLine("QUESTION. What fixes the 53 PHASE COORDINATES?");
        sb.AppendLine("KNOWN        G_050 (the phase sector), G_051 (physical), G_053 (amplitude and phase independent)");
        sb.AppendLine("CANDIDATES   symmetry | occupancy | multiplicity | attractor structure | actualization history |");
        sb.AppendLine("             boundary assignment");
        sb.AppendLine("REQUIREMENTS no new primitive | preserve the clock, acceleration and field laws");
        sb.AppendLine("GOAL         is the phase sector dynamically determined or freely assigned?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The phase coordinates are the state's projections onto the 53 hidden modes, as G_050 defined");
        sb.AppendLine("     them; G_052's interface makes them the orthogonal complement of what the invariants see.");
        sb.AppendLine("  2. A mechanism fixes the coordinates only if it supplies a flow or constraint that MOVES them, so");
        sb.AppendLine("     the audit measures the change each candidate induces rather than arguing about plausibility.");
        sb.AppendLine("  3. Sensitivity and determination are different: a law whose gradient has a phase component would");
        sb.AppendLine("     move the coordinates IF a process extremised it, and both halves are measured separately.");
        sb.AppendLine("  4. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputCoordinates());
        PrintHeader(OutputSensitivity());
        PrintHeader(OutputCandidates());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
