using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.CouplingFunctionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_009 - Coupling Function Audit (group E - Electromagnetism).
///
/// QUESTION. What determines h(rho)? E_008 left the coupling function as an input. Candidates: constant, rho, rho^2,
/// exp(rho), a derived occupancy law, an actualization law. Requirements: produces non-zero F, compatible with T1 and
/// T2, no new primitive. Measure the field strength, the gauge structure and the minimality. GOAL: derive h(rho).
///
/// ANSWER: **DERIVED - h is not free. AT already computes it: h(rho) = (2 pi / 96) rho^(1/d).**
///
///  (1) THE COUPLING IS AT'S OWN CLOCK LAW: GpsCorrectionOrigin.ClockRate(d, rho) = rho^(1/d), with the exponent fixed
///      by the substrate dimension (ClockExponent(d) = 1/d) and the unit by AT's phase quantum (2 pi / 96). ZERO free
///      parameters.
///  (2) "PRODUCES NON-ZERO F" DOES NOT SELECT IT: sweeping h = rho^p over seven exponents, the field strength
///      vanishes at exactly one, p = 0, because a constant coupling IS the gradient E_008 excluded. The requirement
///      rules the constant out and leaves the rest indistinguishable - the clock law is what selects the exponent.
///  (3) MINIMALITY DECIDES IT, AND IT IS COUNTABLE: every other candidate carries a free parameter; the clock-law
///      candidate carries none, and its two names (occupancy law, actualization law) are one law.
///  (4) THE BOUNDARY IS PRECISE: with A_0 from the clock law, F is purely ELECTRIC (F_0i non-zero, F_ij = 0), because
///      the clock law fixes the time-like component and says nothing about the spatial ones.
/// </summary>
public class Y_E_009_Tests : ResearchTestBase
{
    public Y_E_009_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_009_TheCouplingIsATsOwnClockLaw()
    {
        // the two derived inputs, recomputed rather than quoted
        Assert.Equal(1.0 / 3.0, ClockExponent(), 12);
        Assert.Equal(2.0 * Math.PI / 96.0, PhaseQuantum(), 12);
        Assert.Equal(0.065449847, PhaseQuantum(), 9);

        // the clock law itself, AT's own member
        Assert.Equal(Math.Pow(20.0, 1.0 / 3.0), GpsCorrectionOrigin.ClockRate(3, 20.0), 12);

        // and the derived coupling is the product of the two
        var h = DerivedCoupling();
        Assert.Equal(PhaseQuantum() * Math.Pow(20.0, 1.0 / 3.0), h(20.0), 12);

        // THE DOMAIN IS A CONSTRAINT, DISCOVERED BY GETTING NaN: rho^(1/d) needs a NON-NEGATIVE organisation, and
        // an occupancy is non-negative by nature - E_008's generic test scalar is not
        Assert.True(MinimumOccupancy() > 0.0, $"minimum occupancy {MinimumOccupancy():F6}");
        Assert.True(TheOrganisationIsPhysical());

        // AT supplies the law: the live scan finds its own members
        Assert.True(AtMembersComputingTheClockLaw() >= 1);
        Assert.True(TheClockLawIsATsOwn());
    }

    [Fact]
    public void Y_E_009_OnlyTheConstantCandidateFailsToProduceFieldStrength()
    {
        var measurements = CandidateMeasurements();
        Assert.Equal(6, measurements.Length);

        // the constant is refuted
        var constant = measurements.Single(m => m.Candidate == "constant");
        Assert.True(constant.MaxF < 1e-9);
        Assert.Contains("REFUTED", constant.Verdict);

        // and five candidates produce a field strength
        Assert.Equal(5, CandidatesThatProduceFieldStrength().Length);
        Assert.All(measurements.Where(m => m.Candidate != "constant"),
            m => Assert.True(m.MaxF > 1e-3, $"{m.Candidate} gave {m.MaxF:E3}"));
    }

    [Fact]
    public void Y_E_009_TheExponentSweepShowsTheRequirementDoesNotSelectTheExponent()
    {
        var sweep = ExponentSweep();
        Assert.Equal(7, sweep.Length);

        // exactly one exponent gives F = 0, and it is the constant
        Assert.True(OnlyTheConstantFails());
        var zeros = ExponentsWithZeroFieldStrength();
        Assert.Single(zeros);
        Assert.Equal(0.0, zeros[0], 12);

        // every other exponent works - including the clock law's 1/3
        Assert.All(sweep.Where(t => t.Exponent > 0.0), t => Assert.True(t.MaxF > 1e-3));
        Assert.Contains(sweep, t => Math.Abs(t.Exponent - ClockExponent()) < 1e-12 && t.MaxF > 1e-3);
    }

    [Fact]
    public void Y_E_009_MinimalityIsCountableAndDecidesIt()
    {
        Assert.Equal(0, MinimumFreeParameters());
        Assert.Equal(2, ZeroFreeParameterCandidates().Length);
        Assert.Contains("derived occupancy law", ZeroFreeParameterCandidates());
        Assert.Contains("actualization law", ZeroFreeParameterCandidates());

        // and the four chosen candidates each carry one free parameter
        Assert.All(Candidates().Where(c => c.FreeParameters > 0), c => Assert.Equal(1, c.FreeParameters));

        // the two law candidates are the same law - no member is named for an actualization law
        var candidates = Candidates().ToDictionary(c => c.Candidate, c => c.H);
        Assert.Equal(candidates["derived occupancy law"](17.0), candidates["actualization law"](17.0), 12);
    }

    [Fact]
    public void Y_E_009_TheDerivedConfigurationIsPurelyElectricAndGaugeInvariant()
    {
        var (electric, magnetic) = DerivedSectorSplit();
        Assert.True(electric > 1e-3, $"electric {electric:E3}");
        Assert.True(magnetic < 1e-12, $"magnetic {magnetic:E3}");
        Assert.True(TheDerivedConfigurationIsPurelyElectric());

        // F_0i = -Delta_i h(rho), verified against the closed form
        Assert.True(ElectricFieldClosedFormResidual() < 1e-12);

        // gauge invariant and Bianchi-consistent, through E_007's own apparatus
        Assert.True(TheDerivedFieldIsGaugeInvariant());
        Assert.True(DerivedGaugeResidual() < 1e-12);
        Assert.True(DerivedBianchiResidual() < 1e-12);

        // and it is not a difference, which is why it is not the gradient
        Assert.True(TheDerivedCouplingIsNotADifference());
    }

    [Fact]
    public void Y_E_009_TheThreeRequirementsHoldAndTheVerdictIsDerived()
    {
        var checks = RequirementCheck();
        Assert.Equal(3, checks.Length);
        Assert.All(checks, c => Assert.EndsWith("True", c.Status, StringComparison.Ordinal));

        Assert.True(MaxFieldStrengthFor(DerivedCoupling()) > 1e-3);
        Assert.True(ConnectionOriginAudit.OneDerivativeReachesBothSectors());
        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_E_009_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_009 - Coupling Function Audit: what determines h(rho)?");

        sb.AppendLine("QUESTION. What determines h(rho)? E_008 showed a non-difference coupling produces F but left the");
        sb.AppendLine("          coupling FUNCTION as an input.");
        sb.AppendLine("CANDIDATES  constant | rho | rho^2 | exp(rho) | derived occupancy law | actualization law");
        sb.AppendLine("REQUIREMENTS produces non-zero F | compatible with T1 and T2 | no new primitive");
        sb.AppendLine("MEASURE     field strength | gauge structure | minimality");
        sb.AppendLine("GOAL        derive h(rho) instead of choosing it");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The field strength is E_007's plaquette curvature and the coupling form is E_008's local form,");
        sb.AppendLine("     h(rho(x)) - the one E_008 showed to be a non-difference and therefore non-zero.");
        sb.AppendLine("  2. AT's clock law is read as a LINK PHASE: the proper-time rate d tau/dt = rho^(1/d) is the phase");
        sb.AppendLine("     transported across one step, in units of the theory's own phase quantum 2 pi / 96.");
        sb.AppendLine("  3. Free parameters are counted honestly: a candidate carries one if its value or its ansatz has to be");
        sb.AppendLine("     supplied, and none if AT already computes it.");
        sb.AppendLine("  4. Direction 1 is the clock's own direction; directions 2 and 3 are spatial.");
        sb.AppendLine("  5. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputCandidates());
        PrintHeader(OutputMeasurements());
        PrintHeader(OutputGaugeAndMinimality());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
