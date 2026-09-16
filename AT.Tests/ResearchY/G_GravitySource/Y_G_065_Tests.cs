using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseFreePrincipleAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_065 - Phase-Free Principle Audit (group G - Gravity Source).
///
/// QUESTION. Is phase-freeness a derived requirement or only a preferred convention? Test the phase-free canonical state
/// against phase-bearing alternatives; compare the clock, the acceleration, the field, the kernel and the flux; measure
/// whether ANY AT law fails when the phase content is non-zero.
///
/// ANSWER: **BOUNDARY - NO AT LAW FAILS AND PHASE-FREENESS IS STILL NOT A MERE CONVENTION: the dissipative flow AT
/// admits erases phase content, so the phase-free configuration is its ATTRACTOR.**
/// </summary>
public class Y_G_065_Tests : ResearchTestBase
{
    public Y_G_065_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_065_NoLawFailsForAnyState()
    {
        var table = LawTable();
        Assert.Equal(5, table.Length);

        // all seven laws, on every state, at the floating-point floor
        Assert.All(table, t => Assert.InRange(t.Worst, 0.0, 1e-12));
        Assert.True(NoLawFailsForAnyState());
        Assert.True(NoLawSeparatesPhaseFreeFromPhaseBearing());
        Assert.Equal(7, Laws().Length);

        // and the residual is the same ORDER in every state: the phase sector is invisible to the laws
        double spread = table.Max(t => t.Worst) - table.Min(t => t.Worst);
        Assert.InRange(spread, 0.0, 1e-12);
    }

    [Fact]
    public void Y_G_065_TheStatesSpanTheTwoCases()
    {
        var states = StateTable();
        Assert.Equal(5, states.Length);
        Assert.True(states[0].PhaseFree);
        Assert.Equal(0, states[0].HiddenOccupied);
        Assert.All(states.Skip(1), t => Assert.False(t.PhaseFree));

        // the phase-bearing alternatives really do carry phase content, in three different amounts
        Assert.Equal(42, states.Single(t => t.Name.StartsWith("alternative seed")).HiddenOccupied);
        Assert.Equal(53, states.Single(t => t.Name.StartsWith("every mode")).HiddenOccupied);
        Assert.All(states.Skip(1), t => Assert.True(t.PhaseNorm > 1e-3));
    }

    [Fact]
    public void Y_G_065_TheEvolutionKeepsEveryStateADensity()
    {
        var evolution = EvolutionTable();
        Assert.Equal(5, evolution.Length);
        Assert.All(evolution, t => Assert.True(t.MinCellForward > 0.0));
        Assert.All(evolution, t => Assert.True(t.MinCellUnitary > 0.0));
        Assert.True(TheEvolutionKeepsEveryStateADensity());
        Assert.All(evolution, t => Assert.Equal(4000, t.Steps));

        // the phase-free state is not privileged: it does not have the largest minimum cell under either form
        double canonicalForward = evolution[0].MinCellForward;
        Assert.Contains(evolution, t => t.MinCellForward < canonicalForward);
    }

    [Fact]
    public void Y_G_065_TheDissipativeFlowErasesPhaseContent()
    {
        var profile = DecayProfile(new[] { 0, 1000, 20000 });
        Assert.Equal(3, profile.Length);

        // the phase norm falls and the hidden occupancy falls with it
        Assert.InRange(profile[0].PhaseNorm, 1.0, 1.1);
        Assert.True(profile[1].PhaseNorm < profile[0].PhaseNorm);
        Assert.True(profile[2].PhaseNorm < profile[1].PhaseNorm);
        Assert.True(profile[2].PhaseNorm < profile[0].PhaseNorm / 5.0);
        Assert.True(profile[2].HiddenOccupied <= profile[0].HiddenOccupied / 2.0);

        Assert.True(TheDissipativeFlowErasesPhaseContent());
        Assert.True(ThePhaseFreeStateIsAlreadyAtTheAttractor());
        Assert.Equal(0, HiddenModesOccupied(Canonical()));
    }

    [Fact]
    public void Y_G_065_TheSourceLawIsExactInItsLogForm()
    {
        // exact by the second route, and only FIRST ORDER in the linearised form
        Assert.All(States(), x => Assert.InRange(SourceLawResidual(x.State), 0.0, 1e-12));
        Assert.All(States(), x => Assert.True(LinearisedSourceResidual(x.State) > 1e-3));

        // the linearised residual measures ROUGHNESS: the roughest state has the largest one
        var rough = States().Select(x => (x.Name, R: LinearisedSourceResidual(x.State)))
                            .OrderByDescending(t => t.R).ToArray();
        Assert.StartsWith("every mode", rough[0].Name);
        Assert.True(rough[0].R > rough[^1].R);
    }

    [Fact]
    public void Y_G_065_TheStructureFailsOnlyAtSaturation()
    {
        var structure = StructureTable();
        Assert.Equal(5, structure.Length);
        Assert.True(TheStructureSurvivesPhaseContent());

        // every one-seed state keeps the interface, phase-free or not; the all-modes state is the exception
        Assert.All(structure.Where(t => !t.State.StartsWith("every mode")), t => Assert.True(t.NoSplit));
        Assert.All(structure.Where(t => !t.State.StartsWith("every mode")), t => Assert.True(t.HiddenIsEmpty));
        Assert.False(structure.Single(t => t.State.StartsWith("every mode")).NoSplit);
        Assert.True(StructureFailsOnlyAtSaturation());

        Assert.Equal("BOUNDARY", Verdict());
        Assert.Contains("no AT law fails", TheAnswer());
    }

    [Fact]
    public void Y_G_065_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_065 - Phase-Free Principle Audit: is phase-freeness required, or chosen?");

        sb.AppendLine("QUESTION. Is phase-freeness a DERIVED REQUIREMENT or only a PREFERRED CONVENTION?");
        sb.AppendLine("GIVEN        G_054 (the phases are freely assigned), G_057 (the running process is phase-static),");
        sb.AppendLine("             G_063 (one invariant out of thirteen), G_064 (phase-freeness pins the recipe)");
        sb.AppendLine("TEST         the phase-free canonical state against phase-bearing alternatives");
        sb.AppendLine("COMPARE      clock, acceleration, field, kernel, flux");
        sb.AppendLine("MEASURE      does ANY AT law fail when the phase content is non-zero?");
        sb.AppendLine("GOAL         physically required, or merely chosen?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A LAW is an algebraic relation the theory asserts about the occupancy - rho = rate^d, g00 = -rate^2,");
        sb.AppendLine("     A = (1/d) ln rho, a = -grad A, F = h(rho) Delta rho - plus the two constraints that are not algebraic:");
        sb.AppendLine("     the density constraint and the flux constraint.");
        sb.AppendLine("  2. Because those relations are identities in rho, they CANNOT fail for a positive state; the audit measures");
        sb.AppendLine("     them on every state anyway, because a claim that a law holds is only as good as its residual.");
        sb.AppendLine("  3. The one law with a dynamical side - a density must STAY a density under an AT-native update - is measured");
        sb.AppendLine("     by running both AT-native flows, not by inspecting a formula.");
        sb.AppendLine("  4. The linearised form of the source law is reported SEPARATELY from the exact log form: its residual is a");
        sb.AppendLine("     discretisation order that measures the state's roughness, and a first version of this audit charged that");
        sb.AppendLine("     to the law and reported a failure for every state including the phase-free one.");
        sb.AppendLine();

        PrintHeader(OutputStates());
        PrintHeader(OutputLaws());
        PrintHeader(OutputRoughness());
        PrintHeader(OutputDynamics());
        PrintHeader(OutputStructure());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
