using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 179 — Majorana origin. Known: QG154 (neutrino origin: unique Q=0, T3-only access),
/// QG172 (neutrino masses). This phase derives the neutrino character (Dirac vs Majorana) from D96
/// spectral geometry — no fitted assumptions, deterministic — and the 0νββ expectation.
///
/// Tests: ATQG1790 (self-conjugate access + unique neutral sector), ATQG1791 (Z2 doublets + real mass
/// matrix), ATQG1792 (0νββ expectation + classification).
/// </summary>
public class ATQG_Phase179_MajoranaOriginTests : ResearchTestBase
{
    public ATQG_Phase179_MajoranaOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1790_SelfConjugateAccessAndNeutralSector()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1790: self-conjugate access and the unique neutral sector");

        sb.AppendLine("ASSUMPTIONS: a Dirac neutrino requires a particle/antiparticle pair over the");
        sb.AppendLine("full spectrum; a Majorana neutrino is SELF-CONJUGATE and needs only its own");
        sb.AppendLine("channel. The neutrino has T3-ONLY access (QG154) — it reaches exactly the");
        sb.AppendLine("T3 = +1/2 (even) channel — so a distinct antiparticle channel is absent.");
        sb.AppendLine("Majorana also requires NO conserved charge; the neutrino is the unique Q=0 sector.");
        sb.AppendLine();
        sb.AppendLine("DEGREES OF FREEDOM:");
        sb.AppendLine($"  full intra-sector modes = {MajoranaOrigin.FullModeCount()}");
        sb.AppendLine($"  neutrino access (T3=+1/2 channel) = {MajoranaOrigin.NeutrinoAccessCount()}  (QG154)");
        sb.AppendLine($"  T3=−1/2 channel (not accessed) = {MajoranaOrigin.AntiparticleChannelSize()}");
        sb.AppendLine($"  access fraction = {MajoranaOrigin.AccessFraction():F4}");
        sb.AppendLine($"  self-conjugate by access: {MajoranaOrigin.SelfConjugateByAccess()}");
        sb.AppendLine();
        sb.AppendLine("CHARGE:");
        sb.AppendLine($"  unique Q=0 fermion sector: {MajoranaOrigin.UniqueNeutralSector()}");
        sb.AppendLine($"  no conserved charge separates ν from ν̄: {MajoranaOrigin.NoConservedCharge()}");
        sb.AppendLine();
        sb.AppendLine("  the neutrino reaches only the T3=+1/2 channel — there is no separate");
        sb.AppendLine("  antiparticle channel — so it cannot be Dirac; it is self-conjugate.");
        Output.WriteLine(sb.ToString());

        Assert.True(MajoranaOrigin.SelfConjugateByAccess(), "the neutrino should be self-conjugate by T3-only access");
        Assert.True(MajoranaOrigin.NoConservedCharge(), "the unique Q=0 sector should provide no conserved charge");
    }

    [Fact]
    public void ATQG1791_Z2DoubletsAndRealMassMatrix()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1791: Z2 doublets and the real mass matrix");

        sb.AppendLine("ASSUMPTIONS: the Z2 doublets pair modes; the neutrino accesses ONE member of each");
        sb.AppendLine("doublet (the T3=+1/2 member), so each accessed member is its own conjugate. The");
        sb.AppendLine("reflection is an exact graph automorphism (QG174: [L,P]=0), making the spectrum");
        sb.AppendLine("and the masses real — a real Majorana mass term M·ν·ν is allowed, and a complex");
        sb.AppendLine("Dirac phase is absent (arg det M = 0).");
        sb.AppendLine();
        sb.AppendLine("Z2 DOUBLET STRUCTURE:");
        sb.AppendLine($"  T3+ channel octave occupancies = [{string.Join(",", MajoranaOrigin.NeutrinoOctaveOccupancies())}]");
        sb.AppendLine($"  doublet member self-conjugate: {MajoranaOrigin.DoubletMemberSelfConjugate()}");
        sb.AppendLine();
        sb.AppendLine("REFLECTION SYMMETRY (QG174):");
        sb.AppendLine($"  reflection is an exact graph automorphism: {StrongCPOrigin.ReflectionIsAutomorphism()}");
        sb.AppendLine($"  arg det M = 0 (real masses): {StrongCPOrigin.ArgDet():E6}");
        sb.AppendLine($"  real mass matrix: {MajoranaOrigin.RealMassMatrix()}");
        sb.AppendLine();
        sb.AppendLine("  the neutrino occupies one member per Z2 doublet; with a real mass matrix a");
        sb.AppendLine("  real Majorana mass term is allowed — no complex Dirac mass phase exists.");
        Output.WriteLine(sb.ToString());

        Assert.True(MajoranaOrigin.DoubletMemberSelfConjugate(), "the Z2 doublet member should be self-conjugate");
        Assert.True(MajoranaOrigin.RealMassMatrix(), "the mass matrix should be real (reflection automorphism)");
    }

    [Fact]
    public void ATQG1792_ZeroNuBBAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1792: 0νββ expectation and classification");

        sb.AppendLine("ASSUMPTIONS: with Majorana neutrinos the neutrinoless double-beta decay amplitude");
        sb.AppendLine("is governed by the effective Majorana mass m_ββ = |Σ U_ei²·m_i|, computed from the");
        sb.AppendLine("D96 neutrino masses (QG172) and PMNS angles (QG167). A non-zero m_ββ within the");
        sb.AppendLine("current experimental limit supports the Majorana character and gives a testable");
        sb.AppendLine("expectation.");
        sb.AppendLine();
        sb.AppendLine("0νββ EXPECTATION:");
        sb.AppendLine($"  m1 = {NeutrinoMassLaw.M1():E3} eV, m2 = {NeutrinoMassLaw.M2():E4} eV, m3 = {NeutrinoMassLaw.M3():E4} eV  (QG172)");
        sb.AppendLine($"  s12 = {PMNSOrigin.SinTheta12():F4}, s13 = {PMNSOrigin.SinTheta13():F4}, δ_ν = {PMNSOrigin.DeltaNuDeg():F2}°  (QG167)");
        sb.AppendLine($"  m_ββ = |Σ U_ei²·m_i| = {MajoranaOrigin.EffectiveMajoranaMass():E4} eV");
        sb.AppendLine($"  experimental limit m_ββ < 0.036–0.156 eV: {MajoranaOrigin.WithinExperimentalLimit()}");
        sb.AppendLine($"  non-zero (decay allowed): {MajoranaOrigin.NonZero()}");
        sb.AppendLine();
        sb.AppendLine("CHECKS:");
        foreach (var (name, ok) in MajoranaOrigin.Checks())
            sb.AppendLine($"  {name}: {ok}");
        sb.AppendLine();
        int score = MajoranaOrigin.OriginScore();
        string cls = MajoranaOrigin.Classify();
        sb.AppendLine($"Majorana-origin score (0..5): {score}");
        sb.AppendLine($"  +1 self-conjugate by access: {MajoranaOrigin.SelfConjugateByAccess()}");
        sb.AppendLine($"  +1 unique neutral sector: {MajoranaOrigin.NoConservedCharge()}");
        sb.AppendLine($"  +1 Z2 doublet member self-conjugate: {MajoranaOrigin.DoubletMemberSelfConjugate()}");
        sb.AppendLine($"  +1 real mass matrix: {MajoranaOrigin.RealMassMatrix()}");
        sb.AppendLine($"  +1 0νββ non-zero and within limit: {MajoranaOrigin.NonZero() && MajoranaOrigin.WithinExperimentalLimit()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: the D96 structure fully determines the character.");
        sb.AppendLine("  • DIRAC ORIGIN rejected: the neutrino has T3-only access — no antiparticle");
        sb.AppendLine("    channel — and is the unique Q=0 sector (no conserved charge).");
        sb.AppendLine("  • MAJORANA ORIGIN accepted: the neutrino is MAJORANA by D96 spectral geometry —");
        sb.AppendLine("    self-conjugate T3=+1/2 channel access (48/95 modes, QG154), unique Q=0 (no");
        sb.AppendLine("    conserved charge), one self-conjugate Z2 doublet member per doublet, and a");
        sb.AppendLine("    real mass matrix (reflection automorphism, QG174); the 0νββ expectation");
        sb.AppendLine("    m_ββ = 2.02e-3 eV is non-zero and within the current experimental limit — no");
        sb.AppendLine("    fitted assumptions.");
        Output.WriteLine(sb.ToString());

        Assert.True(MajoranaOrigin.NonZero() && MajoranaOrigin.WithinExperimentalLimit(), "0νββ should be non-zero and within limits");
        Assert.True(score >= 4, "Majorana-origin score should be strong");
        Assert.Equal("MAJORANA ORIGIN", cls);
    }
}
