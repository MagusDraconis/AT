using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.E_Electromagnetism;

/// <summary>
/// ResearchY-E_002 — Field Equation Derivation Audit (group E — Electromagnetism).
///
/// QUESTION. E_001 found AT's electromagnetic dynamics DECLARED but never COMPUTED: the Lagrangian, field
/// strength, covariant derivative and currents exist as string-returning members, and the sourced Maxwell
/// equation is recorded MISSING. So: can AT derive an actual field equation — `dF = 0` and `partial F = J` —
/// or is that impossible?
///
/// ANSWER: **BOUNDARY — both ARE derivable, and this suite DERIVES them by computation, but not from AT's
/// premises.**
///
///  (1) `dF = 0` is an IDENTITY: with F = dA the cyclic sum vanishes for any A — EXACTLY, to roundoff, since the
///      cross second-differences cancel term by term — and it FAILS for fields that are not of the form dA. Its
///      content is "no magnetic charge", and it costs nothing, so it is not a dynamical achievement.
///  (2) `partial F = J` is derived HERE, by actually performing the variation: the action on a periodic
///      lattice is differentiated link by link by brute force, and the result equals the discrete divergence
///      of F minus J to roundoff. Stationarity of the action IS the sourced Maxwell equation.
///  (3) But the derivation runs on the action principle, locality, 4-D dimensional counting and Lorentz
///      invariance — premises AT does not supply — and the coupling that fixes the normalisation contradicts
///      itself inside AT. So the equation is earned by standard means, not by AT.
///  (4) Continuity is derived TWICE, independently: by antisymmetry, and by gauge invariance (computed).
///  (5) The photon is exactly massless BECAUSE gauge invariance forbids the mass; the dispersion follows from
///      the field equation. But nothing in AT derives the spin-1 field itself.
///
/// E_001's defect is therefore one of METHOD, not impossibility: the declared Lagrangian is the standard one,
/// its field equation is the standard one, they are correct, and nobody had done the variation.
/// </summary>
public class Y_E_002_Tests : ResearchTestBase
{
    public Y_E_002_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_E_002_BianchiIsAnIdentityAndItsContentIsNoMagneticCharge()
    {
        // F = dA: the cyclic sum vanishes for ANY A — and EXACTLY, not merely to discretisation error, because
        // the cross second-differences cancel term by term. So judge it at ROUNDOFF, flat in h.
        Assert.True(FieldEquationDerivationAudit.BianchiIsExactToRoundoff());
        Assert.True(FieldEquationDerivationAudit.BianchiWorstResidual() < 1e-13);
        // Both ends of the h-range sit at roundoff, so the residual cannot be made to "converge" — that IS the
        // result. (An earlier draft asserted a decreasing ratio here and failed: it assumed a scheme error that
        // does not exist, because the cancellation is algebraic rather than asymptotic.)
        Assert.True(FieldEquationDerivationAudit.BianchiExactResidual(0.01) < 1e-13);

        // The identity has CONTENT: a directly-built antisymmetric F fails it, and so does B = r_hat.
        Assert.True(FieldEquationDerivationAudit.BianchiNonExactResidual(0.05) > 0.5);
        Assert.True(FieldEquationDerivationAudit.BianchiRadialResidual(0.05) > 0.5);

        // The radial control does not vanish as h -> 0 — it is a magnetic-charge violation, not noise.
        Assert.True(FieldEquationDerivationAudit.BianchiRadialResidual(0.01) > 0.5);
    }

    [Fact]
    public void Y_E_002_TheVariationIsActuallyPerformedAndGivesTheFieldEquation()
    {
        // Brute-force dS/dA equals the closed form. This is the core claim of the audit.
        Assert.True(FieldEquationDerivationAudit.VariationAgreement() < 1e-8,
            $"variation agreement {FieldEquationDerivationAudit.VariationAgreement():E3}");
        Assert.True(FieldEquationDerivationAudit.VariationAgreementAbsolute() < 1e-9);

        // With J built from A, A is stationary — the field equation IS solved.
        Assert.True(FieldEquationDerivationAudit.StationarityResidualWhenSolved() < 1e-7);

        // With a generic external J it is not: the identity is about the action, not a triviality.
        Assert.True(FieldEquationDerivationAudit.StationarityResidualWhenNotSolved() > 1e-3);
    }

    [Fact]
    public void Y_E_002_ContinuityFollowsTwiceAndIndependently()
    {
        // (a) antisymmetry
        Assert.True(FieldEquationDerivationAudit.DoubleDivergenceResidual() < 1e-9);
        Assert.True(FieldEquationDerivationAudit.SourcedCurrentDivergence() < 1e-9);

        // (b) gauge invariance — the coupling term's change IS the divergence term
        var (direct, divForm) = FieldEquationDerivationAudit.GaugeVariationOfCoupling();
        Assert.True(Math.Abs(direct) > 1e-6, "the gauge variation must not be trivially zero");
        Assert.True(Math.Abs(direct - divForm) < 1e-10 * Math.Abs(direct),
            $"direct {direct:E6} vs divergence form {divForm:E6}");

        // The control: a non-conserved periodic current has |grad J| = 2*pi/L, so invariance is a real
        // constraint rather than an empty one.
        Assert.True(FieldEquationDerivationAudit.NonConservedCurrentDivergence() > 1.0);
        Assert.True(FieldEquationDerivationAudit.NonConservedCurrentDivergence()
                  < FieldEquationDerivationAudit.NonConservedCurrentDivergenceExpected());
    }

    [Fact]
    public void Y_E_002_GaugeInvarianceForbidsThePhotonMass()
    {
        var (maxwell, proca) = FieldEquationDerivationAudit.MassTermViolation();
        Assert.True(maxwell < 1e-9, $"Maxwell density must be gauge invariant, got {maxwell:E3}");
        Assert.True(proca > 1e-3, $"Proca density must NOT be gauge invariant, got {proca:E3}");

        // The dispersion: null wave vector satisfies the field equation, a non-null one does not.
        var (nullR, massiveR) = FieldEquationDerivationAudit.DispersionViolation();
        Assert.True(nullR < 1e-6, $"null wave vector residual {nullR:E3}");
        Assert.True(massiveR > 1e-2, $"non-null wave vector residual {massiveR:E3}");
        Assert.True(massiveR > 1000.0 * nullR);
    }

    [Fact]
    public void Y_E_002_AtItselfComputesNoFieldStrength()
    {
        // AT declares the dynamics as strings (E_001's proof, reused) and computes no F anywhere.
        Assert.True(FieldEquationDerivationAudit.AtDeclaresRatherThanComputes().Length > 0);
        Assert.Equal(0, FieldEquationDerivationAudit.ComputableFieldStrengthMethods());
        Assert.True(FieldEquationDerivationAudit.NoSpin1WaveEquation());

        // The coupling that would fix the normalisation contradicts itself inside AT.
        var contra = FieldEquationDerivationAudit.CouplingContradiction();
        Assert.Contains("137", contra.First);
        Assert.NotEqual(contra.First, contra.Second);

        Assert.Equal("BOUNDARY", FieldEquationDerivationAudit.Verdict());
    }

    [Fact]
    public void Y_E_002_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_E_002 — Field Equation Derivation Audit: can AT derive an actual field equation?");

        sb.AppendLine("QUESTION. E_001 found AT's EM dynamics DECLARED but never COMPUTED — the Lagrangian, the field");
        sb.AppendLine("strength, D_mu and the currents all exist as string-returning members, and the sourced");
        sb.AppendLine("Maxwell equation d_mu F^mu_nu = J_nu is recorded MISSING. Can AT derive an actual field");
        sb.AppendLine("equation, and specifically dF = 0 and partial F = J — or is that impossible?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. Euclidean signature for the lattice work; the variational identity g/h^4 =");
        sb.AppendLine("     sum_mu grad_mu F_mu_nu - J_nu is signature-independent (Minkowski would only relabel E and B).");
        sb.AppendLine("  2. The action is the MINIMAL one, -1/4 F^2 - J.A — the form AT itself writes down.");
        sb.AppendLine("  3. The premises of the derivation (action principle, locality, 4-D counting, Lorentz");
        sb.AppendLine("     invariance) are stated explicitly in section 5, because they are NOT AT's.");
        sb.AppendLine("  4. Deterministic: explicit wave numbers, no RNG, invariant-culture formatting.");
        sb.AppendLine();

        PrintHeader(FieldEquationDerivationAudit.OutputBianchi());
        PrintHeader(FieldEquationDerivationAudit.OutputVariation());
        PrintHeader(FieldEquationDerivationAudit.OutputContinuity());
        PrintHeader(FieldEquationDerivationAudit.OutputPhoton());
        PrintHeader(FieldEquationDerivationAudit.OutputWhatAtSupplies());

        PrintHeader("6. VERDICT");
        sb.AppendLine($"  {FieldEquationDerivationAudit.Verdict()}");
        sb.AppendLine();
        sb.AppendLine("  " + FieldEquationDerivationAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine("  SUCCESS CRITERION, ANSWERED");
        sb.AppendLine("   · dF = 0        : DERIVED — an identity; true for any A, and vacuous (it costs nothing).");
        sb.AppendLine("   · partial F = J : DERIVED HERE, in full — but from an action whose form is selected and");
        sb.AppendLine("                     whose coupling AT cannot supply. The premises are not AT's.");
        sb.AppendLine("   · continuity    : DERIVED, twice, independently (antisymmetry; gauge invariance).");
        sb.AppendLine("   · photon sector : masslessness DERIVED given gauge invariance; the FIELD ITSELF absent.");
        sb.AppendLine("   · therefore     : BOUNDARY — earned by standard means, not by AT. And E_001's defect is");
        sb.AppendLine("                     METHOD, not impossibility: nobody had performed the variation.");

        Output.WriteLine(sb.ToString());
    }
}
