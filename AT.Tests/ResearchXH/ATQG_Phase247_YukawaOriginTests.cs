using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 247 — Yukawa Origin. Derive the Yukawa interaction y_f ψ̄ψ φ from native D96
/// structure — no new primitives, deterministic, rejects the imported SM mechanism.
/// </summary>
public class ATQG_Phase247_YukawaOriginTests : ResearchTestBase
{
    public ATQG_Phase247_YukawaOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2470_FormAndCouplings()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2470: the Yukawa form and the derived couplings");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The Higgs is the collective occupation-density deviation φ = ρ − ρ̄ (QG84/161/246);");
        sb.AppendLine("  - The Yukawa interaction is the DENSITY ACTION on the fermion mode (QG243 analog in");
        sb.AppendLine("    the scalar sector): the mode density ψ̄ψ contracts with the collective scalar φ;");
        sb.AppendLine("  - The coupling is the mass-to-VEV ratio y_f = m_f/v (both D96-derived).");
        sb.AppendLine();

        sb.AppendLine($"FORM: {YukawaOrigin.YukawaForm()}");
        sb.AppendLine($"Occupation-density coupling present? {YukawaOrigin.OccupationDensityCoupling()}");
        sb.AppendLine($"Scalar is the occupation-density field (QG84/246)? {YukawaOrigin.ScalarIsOccupationDensity()}");
        sb.AppendLine($"Fermion mode exists (QG216 amplitude)? {YukawaOrigin.FermionModeExists()}");
        sb.AppendLine($"VEV derived (QG246/QG168)? {YukawaOrigin.VevDerived()}");
        sb.AppendLine($"v = {YukawaOrigin.WeakScaleGeV():F3} GeV");
        sb.AppendLine();

        sb.AppendLine("THE DERIVED YUKAWA COUPLINGS y_f = m_f/v:");
        foreach (var (name, value) in YukawaOrigin.YukawaValues())
            sb.AppendLine($"  {name,-4} = {value:E4}");

        Output.WriteLine(sb.ToString());

        Assert.Contains("y_f ψ̄ψ φ", YukawaOrigin.YukawaForm());
        Assert.True(YukawaOrigin.OccupationDensityCoupling());
        Assert.True(YukawaOrigin.ScalarIsOccupationDensity());
        Assert.True(YukawaOrigin.FermionModeExists());
        Assert.True(YukawaOrigin.VevDerived());
        Assert.Equal(9, YukawaOrigin.YukawaValues().Length);
        Assert.True(YukawaOrigin.TopYukawa() > YukawaOrigin.ElectronYukawa());
    }

    [Fact]
    public void ATQG2471_HierarchyAndMechanism()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2471: the Yukawa hierarchy and the mass-generation mechanism");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The Yukawa hierarchy equals the derived mass hierarchy (the three families are the");
        sb.AppendLine("    three D96 octave bands, QG210);");
        sb.AppendLine("  - After SSB (QG246) φ = v + h, so y_f ψ̄ψφ → m_f ψ̄ψ + y_f h ψ̄ψ (m_f = y_f·v).");
        sb.AppendLine();

        sb.AppendLine("HIERARCHY (exact D96 octave identities):");
        sb.AppendLine($"  y_τ/y_μ = √occMom·λ₂ = {YukawaOrigin.TauMuonRatio():F3}   (physical {LeptonHierarchyExactLaw.TauPhysical() / LeptonHierarchyExactLaw.MuonPhysical():F3})");
        sb.AppendLine($"  y_μ/y_e = Σm²/√occMom = {YukawaOrigin.MuonElectronRatio():F2}   (physical {LeptonHierarchyExactLaw.MuonPhysical() / PhysicalCalibration.MElectron:F2})");
        sb.AppendLine($"  y_t/y_b = {YukawaOrigin.TopBottomRatio():F3}   (physical {QuarkMassOrigin.TopMass() / QuarkMassOrigin.BottomMass():F3})");
        sb.AppendLine($"  y_t/y_c = {YukawaOrigin.TopCharmRatio():F2}");
        sb.AppendLine($"  Hierarchy matches the derived masses? {YukawaOrigin.HierarchyMatchesMasses()}");
        sb.AppendLine();

        sb.AppendLine("MECHANISM m_f = y_f·v (QG245's OPEN identity):");
        sb.AppendLine($"  MassFromMechanism(y_t) = {YukawaOrigin.MassFromMechanism(YukawaOrigin.TopYukawa()):F4} GeV  (m_t = {QuarkMassOrigin.TopMass() / 1000.0:F4} GeV)");
        sb.AppendLine($"  MassFromMechanism(y_τ) = {YukawaOrigin.MassFromMechanism(YukawaOrigin.TauYukawa()):F4} GeV  (m_τ = {LeptonHierarchyExactLaw.TauMass() / 1000.0:F4} GeV)");
        sb.AppendLine($"  Mechanism closes? {YukawaOrigin.MechanismCloses()}");

        Output.WriteLine(sb.ToString());

        Assert.True(YukawaOrigin.HierarchyMatchesMasses(), "the Yukawa hierarchy must equal the derived mass ratios");
        Assert.True(Math.Abs(YukawaOrigin.TauMuonRatio() / (LeptonHierarchyExactLaw.TauPhysical() / LeptonHierarchyExactLaw.MuonPhysical()) - 1.0) < 0.01,
            "y_τ/y_μ must match the physical lepton ratio within 1%");
        Assert.True(YukawaOrigin.MechanismCloses(), "m_f = y_f·v must close for every fermion");
    }

    [Fact]
    public void ATQG2472_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2472: summary — YUKAWA ORIGIN");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The Yukawa sector is complete iff the form, the couplings, the hierarchy, and the");
        sb.AppendLine("    mechanism are all derived from D96 (no imported vertices, no free Yukawa parameters).");
        sb.AppendLine();

        sb.AppendLine("KEY QUANTITIES:");
        foreach (var (name, derived, note) in YukawaOrigin.Quantities())
            sb.AppendLine($"  {name,-10} = {derived,12:F6}   {note}");

        sb.AppendLine();
        sb.AppendLine($"Origin score = {YukawaOrigin.OriginScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {YukawaOrigin.Classify()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, YukawaOrigin.OriginScore());
        Assert.Equal("YUKAWA ORIGIN", YukawaOrigin.Classify());
    }
}
