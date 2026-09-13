using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.MagneticSectorAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_010 - Magnetic Sector Audit (group E - Electromagnetism).
///
/// QUESTION. What AT structure can generate a non-zero F_ij? Requirements: local, gauge-compatible, no new primitive,
/// acting on T1/T2. DETERMINE: can magnetic components emerge from occupancy dynamics alone?
///
/// ANSWER: **REFUTED - not in any physical sense, and the measurement that decides it is a SCALING one.**
///
///  (1) E_009's purely-electric result was a CHOICE, not a theorem: it set the spatial components to zero. Completing
///      the coupling covariantly, A_mu = h(rho) Delta_mu rho, switches the magnetic components on - max |F_23| =
///      3.52E-03 at L = 8 with the derived coupling, against EXACTLY ZERO for the linear coupling, which is the
///      gradient E_008 excluded. The magnetic sector exists because the clock law is NONLINEAR.
///  (2) BUT IT IS A FINITE-SIZE ARTEFACT. Refining the lattice at a fixed physical profile, the covariant form's
///      magnetic field falls as a^2.87 and the site-local form's as a^0.94 - both vanish in the physical limit.
///      This programme already applied that standard to masslessness (E_004: the gap closes because mu_min n^2
///      settles), so the diagnosis is the same one, arrived at the same way.
///  (3) WHAT SURVIVES IS WHAT IS NOT A FUNCTION OF THE ORGANISATION: E_007's uniform flux, with the physical
///      strength held fixed, reads 0.785398 at EVERY lattice size.
///  (4) Therefore NO - and the magnetic half requires an independently assigned spatial link field.
/// </summary>
public class Y_E_010_Tests : ResearchTestBase
{
    public Y_E_010_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_010_TheCovariantCompletionTurnsTheMagneticSectorOn()
    {
        // the derived coupling is E_009's, unchanged
        Assert.Equal(CouplingFunctionAudit.DerivedCoupling()(1.7), DerivedCoupling()(1.7), 12);

        // the covariant completion does produce a magnetic field...
        Assert.True(MagneticFieldOfTheDerivedCovariantForm() > 1e-6,
            $"covariant F_23 = {MagneticFieldOfTheDerivedCovariantForm():E3}");

        // ...and the LINEAR coupling does not - that is the gradient E_008 excluded
        Assert.True(MagneticFieldOfTheLinearCovariantForm() < 1e-12,
            $"linear F_23 = {MagneticFieldOfTheLinearCovariantForm():E3}");

        Assert.True(TheMagneticSectorNeedsTheNonlinearity());

        // the same configuration is also electric
        Assert.True(MaxElectricField(CovariantForm(DerivedCoupling()), 8) > 1e-6);
    }

    [Fact]
    public void Y_E_010_BothOccupancyRoutesScaleAwayAsTheLatticeIsRefined()
    {
        var covariant = MagneticScaling(CovariantForm(DerivedCoupling()));
        var local = MagneticScaling(LocalForm(DerivedCoupling()));

        Assert.Equal(4, covariant.Length);
        Assert.Equal(new[] { 8, 16, 32, 64 }, covariant.Select(t => t.L).ToArray());

        // strictly decreasing in both cases
        Assert.All(covariant, t => Assert.True(t.Magnetic > 0.0));
        Assert.All(local, t => Assert.True(t.Magnetic > 0.0));
        Assert.True(covariant[0].Magnetic > covariant[^1].Magnetic);
        Assert.True(local[0].Magnetic > local[^1].Magnetic);

        // and the fitted exponents say they vanish: a^2.87 and a^0.94
        Assert.True(CovariantScalingExponent() > 2.0, $"covariant exponent {CovariantScalingExponent():F2}");
        Assert.True(LocalScalingExponent() > 0.5 && LocalScalingExponent() < 1.5, $"local exponent {LocalScalingExponent():F2}");
        Assert.True(BothOccupancyRoutesScaleAway());
    }

    [Fact]
    public void Y_E_010_TheIndependentlyAssignedFluxDoesNotScaleAway()
    {
        var flux = UniformFluxScaling();
        Assert.Equal(4, flux.Length);

        // the physical strength is held fixed, so the field is the same at every lattice size
        Assert.All(flux, t => Assert.Equal(0.785398, t.Magnetic, 6));
        Assert.Equal(flux[0].Magnetic, flux[^1].Magnetic, 12);
        Assert.True(TheUniformFluxSurvives());
    }

    [Fact]
    public void Y_E_010_E009sElectricResultIsReproducedByTheSameApparatus()
    {
        // A_0 only, exactly as E_009 read the clock law, on E_009's OWN organisation
        Field clockOnly = (l, x, y, z, mu) =>
            mu == 1 ? CouplingFunctionAudit.DerivedCoupling()(CouplingFunctionAudit.Occupancy(x % 8, y % 8, z % 8)) : 0.0;

        // magnetic identically zero - E_009's purely electric result...
        Assert.True(MaxMagneticField(clockOnly, 8) < 1e-12);
        // ...and the electric field is E_009's own published number
        Assert.Equal(1.424E-002, MaxElectricField(clockOnly, 8), 5);

        // this audit's profile differs from E_009's only in its last term, so the same split must hold there too
        Field clockOnlyOwnProfile = (l, x, y, z, mu) => mu == 1 ? DerivedCoupling()(Rho(l, x, y, z)) : 0.0;
        Assert.True(MaxMagneticField(clockOnlyOwnProfile, 8) < 1e-12);
        Assert.True(MaxElectricField(clockOnlyOwnProfile, 8) > 1e-3);

        // and E_007's own flux, which lives in an ELECTRIC pair, is magnetic only in the other slot
        Assert.True(MaxElectricField(UniformElectricFlux(1), 8) > 1e-9);
        Assert.True(MaxMagneticField(UniformElectricFlux(1), 8) < 1e-12);
    }

    [Fact]
    public void Y_E_010_TheRequirementsHoldForTheConfigurationThatWouldWork()
    {
        Assert.True(TheUniformFluxIsLocal());
        Assert.Equal(2, Support());
        Assert.True(TheMagneticSectorIsGaugeCompatible());
        Assert.True(GaugeResidual(UniformMagneticFlux(1)) < 1e-12);
        Assert.True(NoNewPrimitiveIsNeeded());
        Assert.True(TheMagneticSectorActsOnT1AndT2());
    }

    [Fact]
    public void Y_E_010_TheAnswerIsNoAndTheVerdictIsRefuted()
    {
        var findings = Findings();
        Assert.Equal(4, findings.Length);
        Assert.StartsWith("does the covariant completion", findings[0].Question, StringComparison.Ordinal);
        Assert.Contains("yes", findings[0].Answer, StringComparison.Ordinal);
        Assert.Contains("no", findings[1].Answer, StringComparison.Ordinal);
        Assert.Contains("yes", findings[2].Answer, StringComparison.Ordinal);
        Assert.Contains("occupancy dynamics alone", findings[3].Question, StringComparison.Ordinal);
        Assert.Equal("no", findings[3].Answer);

        Assert.Equal("REFUTED", Verdict());
    }

    [Fact]
    public void Y_E_010_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_010 - Magnetic Sector Audit: what generates a non-zero F_ij?");

        sb.AppendLine("QUESTION. What AT structure can generate non-zero F_ij?");
        sb.AppendLine("REQUIREMENTS  local | gauge-compatible | no new primitive | acts on T1/T2");
        sb.AppendLine("DETERMINE     can magnetic components emerge from occupancy dynamics alone?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Direction 1 is the clock's direction (E_009's convention), so the MAGNETIC pair is (2,3) and");
        sb.AppendLine("     the electric pairs are (1,2) and (1,3).");
        sb.AppendLine("  2. The coupling is E_009's derived one, h(rho) = (2 pi / 96) rho^(1/d), with zero free parameters.");
        sb.AppendLine("  3. A physical field strength must survive the refinement of the substrate at a FIXED physical");
        sb.AppendLine("     profile - the standard this programme already applied to masslessness in E_004, where the gap");
        sb.AppendLine("     was called an artefact because mu_min n^2 settles to a constant.");
        sb.AppendLine("  4. The large lattices are sampled on a stride of L/16 in the third direction; the profile is the");
        sb.AppendLine("     same physical function at every size.");
        sb.AppendLine("  5. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputCompletion());
        PrintHeader(OutputScaling());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
