using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseSelectionPrincipleAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_055 - Phase Selection Principle Audit (group G - Gravity Source).
///
/// QUESTION. Can any EXISTING AT quantity assign a PREFERRED PHASE STATE? Candidates: entropy, free room, actualization
/// density, flux sector, clock functional, field functional. Test: does any quantity break phase degeneracy? Critical
/// question: why this phase instead of another?
///
/// ANSWER: **BOUNDARY - none can, and the audit measures exactly how many phase directions are left free, because a
/// scalar functional's gradient is a single vector and can therefore constrain at most one direction.**
/// </summary>
public class Y_G_055_Tests : ResearchTestBase
{
    public Y_G_055_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_055_ThreeCandidatesArePhaseBlindAndConstrainNothing()
    {
        // the counting bound first: no candidate may constrain more directions than a single gradient can
        Assert.Equal(6, Candidates().Length);
        Assert.True(TheCountingBoundHolds());
        Assert.All(CandidateTable(), t => Assert.True(t.Constrained <= 1, $"{t.Candidate} claims {t.Constrained}"));

        // free room is the simplex constraint (constant gradient), actualization density its scalar level, flux decoupled
        Assert.Equal(3, PhaseBlindCandidates().Length);
        Assert.Contains("free room", PhaseBlindCandidates());
        Assert.Contains("actualization density", PhaseBlindCandidates());
        Assert.Contains("flux sector", PhaseBlindCandidates());
        // the analytic gradients are exact, so these are zero to machine precision rather than to a differencing floor
        // the exact zeros sit at the modes' own sum residual (about 4E-14), documented rather than tuned away
        Assert.True(PhaseProjectionNorm("free room") < 1e-12, $"free room projects {PhaseProjectionNorm("free room"):E3}");
        Assert.True(PhaseProjectionNorm("free room") > 0.0);            // and it is a floor, not an absence
        Assert.True(PhaseProjectionFloor() < 1e-12, $"floor {PhaseProjectionFloor():E3}");
        Assert.Equal(0.0, PhaseProjectionNorm("flux sector"));
        Assert.True(UsesAnalyticGradient("free room") && UsesAnalyticGradient("clock functional"));
    }

    [Fact]
    public void Y_G_055_TheSensitiveCandidatesContributeOneDirectionEach()
    {
        Assert.Equal(3, PhaseSensitiveCandidates().Length);
        Assert.Contains("entropy", PhaseSensitiveCandidates());
        Assert.Contains("clock functional", PhaseSensitiveCandidates());
        Assert.Contains("field functional", PhaseSensitiveCandidates());
        Assert.All(CandidateTable().Where(t => t.Class == "phase-sensitive"),
            t => Assert.Equal(1, t.Constrained));
    }

    [Fact]
    public void Y_G_055_TheDeficiencyIsMeasuredAndNonZero()
    {
        Assert.Equal(53, PhaseDimension());
        Assert.True(ConstraintRank() > 0, "something must be constrained or the question is void");
        Assert.True(ConstraintRank() <= Candidates().Length);
        Assert.True(Deficiency() > 0, $"deficiency {Deficiency()}");
        Assert.Equal(PhaseDimension() - ConstraintRank(), Deficiency());
        Assert.Contains("measured rather than bounded", TheDeficiency());
    }

    [Fact]
    public void Y_G_055_NoSensitiveCandidateIsStationaryAtTheAuditedState()
    {
        // a preference would require a critical point; the state is not one
        Assert.True(NoCandidateIsStationary());
        Assert.All(CandidateTable().Where(t => t.Class == "phase-sensitive"),
            t => Assert.True(LargestPhaseDerivative(t.Candidate) > 1e-9,
                $"{t.Candidate} is stationary in the phase directions"));
    }

    [Fact]
    public void Y_G_055_TheVerdictIsBoundaryAndTheCriticalQuestionIsAnsweredByTheDeficiency()
    {
        Assert.Equal("BOUNDARY", Verdict());
        Assert.Contains($"{Deficiency()} unconstrained directions", TheCriticalQuestion());
        Assert.Contains("NO answer inside AT", TheCriticalQuestion());
    }

    [Fact]
    public void Y_G_055_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_055 - Phase Selection Principle Audit: can any AT quantity prefer a phase state?");

        sb.AppendLine("QUESTION. Can any EXISTING AT quantity assign a PREFERRED PHASE STATE?");
        sb.AppendLine("GIVEN        G_052 (the interface identity), G_054 (the phases are freely assigned),");
        sb.AppendLine("             E_014 (the flux label is an assignment), E_015 (the sector measure is exactly flat)");
        sb.AppendLine("CANDIDATES   entropy | free room | actualization density | flux sector | clock functional |");
        sb.AppendLine("             field functional");
        sb.AppendLine("CRITICAL     why this phase instead of another?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A quantity that could assign a preferred phase must be a SCALAR functional - something one could");
        sb.AppendLine("     extremise - and the gradient of a scalar is one vector, so it constrains at most one phase");
        sb.AppendLine("     direction. The audit measures the rank rather than trusting the bound.");
        sb.AppendLine("  2. A candidate is PHASE-BLIND when its gradient has no phase component at all, which is a stronger");
        sb.AppendLine("     statement than being weak: it constrains nothing in any combination.");
        sb.AppendLine("  3. Gradients are taken by central differences, independent of the phase basis.");
        sb.AppendLine("  4. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputCandidates());
        PrintHeader(OutputDeficiency());
        PrintHeader(OutputCriticalPoint());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
