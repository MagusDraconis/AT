using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.FieldExcitationAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_008 - Field Excitation Audit (group E - Electromagnetism).
///
/// QUESTION. What AT mechanism produces a non-zero field strength F? Requirements: (1) local, (2) gauge-compatible,
/// (3) acts on T1 and T2, (4) no new primitive. Candidates: occupancy gradients, actualization gradients, deficit
/// gradients, non-uniform rho, topological defects. Measure F; compare F = 0 against F != 0.
///
/// ANSWER: **DERIVED - a dichotomy that is exact on both sides.**
///
///  (1) EVERY GRADIENT IS EXACTLY PURE GAUGE. A link phase written as the difference of ANY single-valued scalar -
///      including any function H of the organisation - telescopes to zero around every closed plaquette: worst
///      residual over H = rho, rho^2, exp(rho), sin(rho) on a NON-SEPARABLE organisation is ~1e-16. That kills
///      occupancy gradients, actualization gradients and deficit gradients outright.
///  (2) F != 0 REQUIRES A NON-DIFFERENCE COUPLING. h(rho) * Delta rho with h constant gives 0 (that IS the
///      gradient); h = rho, rho^2, exp(rho) give 0.626, 0.778, 1.126; and a purely site-local h(rho(x)) gives 1.063
///      with the verified closed form F_mu_nu = h(rho(x+mu)) - h(rho(x+nu)). NON-UNIFORM RHO survives, read
///      locally rather than through its gradient.
///  (3) THE TOPOLOGICAL DEFECT CANDIDATE COLLAPSES INTO (2): a phase-gradient vortex is pure gauge (every plaquette
///      exactly 1, cut included), and the wrapped variant's values are all WHOLE TURNS, i.e. the identity. A genuine
///      defect needs an independently assigned link configuration - one mechanism, not two.
///  (4) ALL FOUR REQUIREMENTS HOLD FOR THE SURVIVOR, and what remains open is the selection: which coupling, and
///      what makes the organisation non-uniform.
/// </summary>
public class Y_E_008_Tests : ResearchTestBase
{
    public Y_E_008_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_008_EveryGradientCouplingIsExactlyPureGauge()
    {
        var candidates = GradientCandidates();
        Assert.Equal(3, candidates.Length);
        Assert.All(candidates, c => Assert.True(c.MaxF < 1e-12, $"{c.Candidate} gave {c.MaxF:E3}"));

        // and across every coupling function measured
        Assert.True(EveryGradientIsPureGauge());
        Assert.True(GradientWorstResidual() < 1e-12);
        Assert.Equal(4, Measurements().Count(m => m.Form.StartsWith("gradient", StringComparison.Ordinal)));

        // the organisation is NOT separable - the product form below proves it by lighting up
        Assert.True(MaxFieldStrength(ProductCoupling(Occupancy, s => s)) > 0.1);
    }

    [Fact]
    public void Y_E_008_NonGradientCouplingsProduceFieldStrength()
    {
        // the constant coupling IS the gradient, so it is still zero
        Assert.True(MaxFieldStrength(ProductCoupling(Occupancy, _ => 1.0)) < 1e-12);

        // a NONLINEAR coupling is not a gradient any more
        Assert.True(MaxFieldStrength(ProductCoupling(Occupancy, s => s)) > 0.5);
        Assert.True(MaxFieldStrength(ProductCoupling(Occupancy, s => s * s)) > 0.5);
        Assert.True(MaxFieldStrength(ProductCoupling(Occupancy, Math.Exp)) > 1.0);

        // and a purely site-local coupling, same for every direction
        Assert.True(SurvivorFieldStrength() > 1.0);
        Assert.True(TheSurvivorProducesFieldStrength());

        // the local DIRECTIONAL variant is larger still
        Assert.True(MaxFieldStrength(LocalDirectionalCoupling(Occupancy, s => s)) > 2.0);

        // and the table balances: five zeros against six non-zeros
        Assert.Equal(5, FIsZero().Length);
        Assert.Equal(6, FIsNonZero().Length);
        Assert.Equal(11, Measurements().Length);
    }

    [Fact]
    public void Y_E_008_TheLocalCouplingHasAVerifiedClosedForm()
    {
        // F_mu_nu = h(rho(x + mu)) - h(rho(x + nu)) - checked against the plaquette sum over the whole lattice
        Assert.True(LocalCouplingClosedFormResidual() < 1e-12,
            $"closed-form residual {LocalCouplingClosedFormResidual():E3}");

        // spot check by hand at one point
        const int x = 2, y = 3, z = 0;
        double expected = Occupancy((x + 1) % L, y, z) - Occupancy(x, (y + 1) % L, z);
        var a = LocalCoupling(Occupancy, s => s);
        Assert.Equal(expected, Plaquette(a, 1, 2, x, y, z), 12);
    }

    [Fact]
    public void Y_E_008_TheTopologicalDefectCandidateCollapsesIntoTheSameAnswer()
    {
        // a phase gradient is a gauge transformation BY CONSTRUCTION, so every plaquette is the identity - the branch
        // cut included
        Assert.True(TheGradientVortexIsPureGauge());
        Assert.True(PhaseGradientPlaquetteResidual() < 1e-12);

        // the wrapped variant does light up plaquettes, but every value is a whole turn - the identity
        var (count, largest, total) = WrappedVortexFlux();
        Assert.True(count > 0);
        Assert.Equal(2.0 * Math.PI, largest, 9);
        Assert.Equal(0.0, total, 9);
        Assert.True(EveryWrappedVortexValueIsAWholeTurn());
    }

    [Fact]
    public void Y_E_008_TheFourRequirementsHoldForTheSurvivor()
    {
        Assert.Equal(4, Requirements.Length);
        Assert.Equal(new[] { "local", "gauge-compatible", "acts on T1 and T2", "no new primitive" }, Requirements);

        Assert.True(TheSurvivorIsLocal());
        Assert.True(TheSurvivorIsGaugeCompatible());
        Assert.True(GaugeInvarianceResidual(Occupancy) < 1e-12);
        Assert.True(TheSurvivorActsOnT1AndT2());

        // the extended check table says the same
        var checks = RequirementCheck();
        Assert.Equal(4, checks.Length);
        Assert.All(checks, c => Assert.Contains(": True", c.Status));
        Assert.True(NoNewPrimitiveIsNeeded());
    }

    [Fact]
    public void Y_E_008_TheVerdictIsDerived()
    {
        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_E_008_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_008 - Field Excitation Audit: what produces a non-zero F?");

        sb.AppendLine("QUESTION. What AT mechanism produces a non-zero field strength F?");
        sb.AppendLine("REQUIREMENTS  local | gauge-compatible | acts on T1 and T2 | no new primitive");
        sb.AppendLine("CANDIDATES    occupancy gradients | actualization gradients | deficit gradients |");
        sb.AppendLine("              non-uniform rho | topological defects");
        sb.AppendLine("MEASURE       F, and F = 0 versus F != 0");
        sb.AppendLine("GOAL          locate the first dynamical source of field strength");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The field strength is E_007's plaquette holonomy F_mu_nu = A_mu + A_nu(x+mu) - A_mu(x+nu) - A_nu.");
        sb.AppendLine("  2. The organisation is deliberately NON-SEPARABLE: with a separable rho the product coupling's field");
        sb.AppendLine("     strength cancels identically, and a first draft of this audit was fooled by exactly that.");
        sb.AppendLine("  3. A coupling is a relation between AT's own objects, not a new primitive: rho and the link phase");
        sb.AppendLine("     are the theory's, and only the way they are tied together is written down.");
        sb.AppendLine("  4. Deterministic throughout: one fixed organisation, one fixed vortex core.");
        sb.AppendLine();

        PrintHeader(OutputCandidates());
        PrintHeader(OutputMeasurements());
        PrintHeader(OutputRequirements());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
