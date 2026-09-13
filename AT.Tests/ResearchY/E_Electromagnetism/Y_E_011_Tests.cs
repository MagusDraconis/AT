using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.FluxOriginAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_011 - Flux Origin Audit (group E - Electromagnetism).
///
/// QUESTION. What generates the surviving non-trivial loop flux F = 2 pi / 96 that E_007 exhibited? Candidates:
/// occupancy structure, D96 topology, winding number, actualization process, boundary assignment. Requirements:
/// survives the continuum limit, gauge compatible, local, acts on T1 and T2. Measure flux, holonomy, field strength.
///
/// ANSWER: **DERIVED - the origin is D96's own closed topology together with the compactness of the phase, and
/// 2 pi / 96 is the substrate's cycle length speaking.**
///
///  (1) A CLOSED cycle makes a holonomy no gauge transformation can remove: the gauge function's contribution
///      telescopes to zero exactly, because a single-valued function returns to its own value (measured ~1e-16 at the
///      substrate and at a shorter cycle).
///  (2) A COMPACT phase quantises the flux: exp(i A(L)) = exp(i A(0)) forces f L = 2 pi n, so f = 2 pi n / L. A
///      half-sector flux is NOT periodic, and the audit checks that too.
///  (3) THE QUANTUM IS THE CYCLE LENGTH: quantum x L = 2 pi at every length, and at L = 96 it is 2 pi / 96 =
///      0.065449847 - E_007's figure, reproduced rather than quoted.
///  (4) THE CANDIDATES: occupancy structure REFUTED (E_010's scaling exponents), D96 topology DERIVED, winding
///      number REFUTED (a gradient: zero curvature AND a whole-turn holonomy), actualization process REFUTED (the
///      time-like component only), boundary assignment BOUNDARY (the integer sector).
///  (5) WHAT SURVIVES IS MEASURED RATHER THAN ASSUMED: the CYCLE holonomy is a whole turn - the identity - while the
///      PLAQUETTE holonomy is non-trivial, so the content is LOCAL CURVATURE, not a topological charge; and at fixed
///      sector the strength falls as 1/L, so what survives refinement is the EXISTENCE of a non-trivial loop
///      configuration, not its magnitude.
/// </summary>
public class Y_E_011_Tests : ResearchTestBase
{
    public Y_E_011_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_011_TheClosedCycleMakesTheHolonomyGaugeProof()
    {
        Assert.True(TheGaugeContributionTelescopes());
        Assert.True(TelescopingSum(L96) < 1e-12);
        Assert.True(TelescopingSum(17) < 1e-12);
        Assert.True(ClosedLoopGaugeResidual() < 1e-12);
    }

    [Fact]
    public void Y_E_011_TheCompactPhaseQuantisesTheFlux()
    {
        // allowed sectors are periodic
        Assert.All(Enumerable.Range(1, 3), n => Assert.True(PeriodicityResidual(n, L96) < 1e-12));
        // a half-sector flux is NOT periodic, so it is not allowed - a condition that rejects something
        Assert.True(OffSectorIsNotPeriodic());
        Assert.True(TheFluxIsQuantisedByCompactness());
    }

    [Fact]
    public void Y_E_011_TheQuantumIsTheCycleLengthAndReproducesE007()
    {
        var series = QuantumSeries();
        Assert.Equal(4, series.Length);
        Assert.All(series, t => Assert.Equal(2.0 * Math.PI, t.TimesL, 12));
        Assert.True(TheQuantumIsTheInverseCycleLength());

        Assert.Equal(0.065449847, TheSubstrateQuantum(), 9);
        Assert.True(ReproducesE007());

        // E_007's own member, re-derived here rather than assumed
        Assert.Equal(FieldStrengthOriginAudit.MinimalNonZeroFlux(), TheSubstrateQuantum(), 15);
    }

    [Fact]
    public void Y_E_011_TheNonTrivialityIsLocalAndExistsAtEverySize()
    {
        // the cycle holonomy is a whole turn - the identity
        Assert.True(CycleHolonomyPhaseDistance(1, L96) < 1e-12);
        Assert.Equal(2.0 * Math.PI, CycleHolonomy(1, L96), 12);

        // the plaquette holonomy is not
        Assert.True(PlaquetteHolonomyDistance(1, L96) > 1e-3);
        Assert.True(TheNonTrivialityIsLocal());

        // and a non-trivial configuration exists at every size, though its strength falls
        Assert.True(ANonTrivialConfigurationExistsAtEverySize());
        Assert.True(TheStrengthFallsWithTheCycle());
        var strengths = FixedSectorStrength();
        Assert.Equal(0.065449847, strengths[^1].Strength, 9);
    }

    [Fact]
    public void Y_E_011_ThreeCandidatesAreRefutedAndTheOriginIsLocated()
    {
        Assert.Equal(3, RefutedCandidates().Length);
        Assert.Contains("occupancy structure", RefutedCandidates());
        Assert.Contains("winding number", RefutedCandidates());
        Assert.Contains("actualization process", RefutedCandidates());
        Assert.Equal("D96 topology", Origin());

        // and the boundary candidate is the sector label
        var boundary = Candidates().Single(c => c.Status == "BOUNDARY");
        Assert.Equal("boundary assignment", boundary.Candidate);
        Assert.Contains("integer", boundary.Basis, StringComparison.Ordinal);
    }

    [Fact]
    public void Y_E_011_TheRequirementsHoldAndTheVerdictIsDerived()
    {
        var checks = RequirementCheck();
        Assert.Equal(4, checks.Length);
        Assert.Equal("survives continuum limit", checks[0].Requirement);
        Assert.Contains("gauge compatible", checks.Select(c => c.Requirement));
        Assert.Contains("acts on T1 and T2", checks.Select(c => c.Requirement));

        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_E_011_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_011 - Flux Origin Audit: what generates F = 2 pi / 96?");

        sb.AppendLine("QUESTION. What generates the surviving non-trivial loop flux F = 2 pi / 96?");
        sb.AppendLine("CANDIDATES   occupancy structure | D96 topology | winding number |");
        sb.AppendLine("             actualization process | boundary assignment");
        sb.AppendLine("REQUIREMENTS survives continuum limit | gauge compatible | local | acts on T1 and T2");
        sb.AppendLine("MEASURE      flux | holonomy | field strength");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The flux is E_007's uniform one, A_1 = f y, whose field strength is f on every plaquette.");
        sb.AppendLine("  2. Compactness is what makes the link a PHASE: exp(i A) is the physical variable, so the cycle's");
        sb.AppendLine("     two ends must agree as phases and only f L = 2 pi n is allowed.");
        sb.AppendLine("  3. A closed cycle's holonomy is gauge-proof because a gauge function is single-valued - which is");
        sb.AppendLine("     the exact complement of E_008's theorem that a contractible loop's gradient flux is zero.");
        sb.AppendLine("  4. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputOrigin());
        PrintHeader(OutputMeasured());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
