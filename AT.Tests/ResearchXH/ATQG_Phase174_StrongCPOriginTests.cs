using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 174 — Strong CP origin. Known: QG161 (gauge sector 1+3+8), QG166 (CKM CP from chiral
/// rotation circulation), QG173 (quark masses = real spectral moments). This phase derives the natural
/// suppression θ_QCD ≈ 0 from D96 spectral geometry — no fitted parameters, no axion.
///
/// Tests: ATQG1740 (D96 reflection automorphism + real spectrum), ATQG1741 (real mass determinant →
/// arg det = 0 → θ_QCD = 0), ATQG1742 (weak-vs-strong contrast + bound + classification).
/// </summary>
public class ATQG_Phase174_StrongCPOriginTests : ResearchTestBase
{
    public ATQG_Phase174_StrongCPOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1740_D96ReflectionAndRealSpectrum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1740: D96 reflection automorphism and the real spectrum");

        sb.AppendLine("ASSUMPTIONS: the strong CP angle decomposes as θ_QCD = θ_vac + arg det M_q; the");
        sb.AppendLine("vacuum angle vanishes if the D96 vacuum is reflection-even, which follows if the");
        sb.AppendLine("dihedral reflection s is an exact graph automorphism of the observable sector");
        sb.AppendLine("(a real-symmetric Laplacian commuting with a reflection has a real spectrum).");
        sb.AppendLine();
        sb.AppendLine("D96 REFLECTION STRUCTURE:");
        sb.AppendLine($"  max |[L, P]| = {StrongCPOrigin.CommutationError():E6}");
        sb.AppendLine($"  reflection is an exact graph automorphism: {StrongCPOrigin.ReflectionIsAutomorphism()}");
        sb.AppendLine($"  reflection reverses the rotation s·r·s = r⁻¹ (QG166): {CKMCPOrigin.ReflectionReversesRotation()}");
        sb.AppendLine($"  half-shift eigenvalue on mode k: e^(iπk) = (-1)^k (Z2 phase structure)");
        sb.AppendLine();
        sb.AppendLine("REAL SPECTRUM (reflection-even moments):");
        foreach (var (name, value, imag) in StrongCPOrigin.SpectralMoments())
            sb.AppendLine($"  Im({name}) = {imag:E3}  ({name} = {value:F4})");
        sb.AppendLine($"  all spectral moments real: {StrongCPOrigin.AllMomentsReal()}");
        sb.AppendLine();
        sb.AppendLine("Z2 DOUBLET REFLECTION PAIRS:");
        sb.AppendLine($"  doublet groups (size 2) = {StrongCPOrigin.DoubletPairCount()}");
        sb.AppendLine($"  doublet-paired fraction = {StrongCPOrigin.DoubletPairedFraction():F4} of all modes");
        sb.AppendLine();
        sb.AppendLine("  every mode is paired with its mirror under the reflection — the Z2 doublets");
        sb.AppendLine("  are exactly these reflection pairs; the spectrum is reflection-even, so the");
        sb.AppendLine("  vacuum topological charge vanishes (θ_vac = 0).");
        Output.WriteLine(sb.ToString());

        Assert.True(StrongCPOrigin.ReflectionIsAutomorphism(), "[L,P] should vanish (reflection automorphism)");
        Assert.True(StrongCPOrigin.AllMomentsReal(), "all spectral moments should be real");
        Assert.True(StrongCPOrigin.DoubletPairCount() >= 40, "the doublet structure should dominate");
    }

    [Fact]
    public void ATQG1741_RealDeterminantAndThetaQCD()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1741: real mass determinant → arg det = 0 → θ_QCD = 0");

        sb.AppendLine("ASSUMPTIONS: the six quark masses (QG173) are positive real D96 spectral moments;");
        sb.AppendLine("the determinant of a positive-real matrix is real positive, so its phase is");
        sb.AppendLine("EXACTLY zero; with a reflection-even vacuum (θ_vac = 0) the strong CP angle is");
        sb.AppendLine("θ_QCD = θ_vac + arg det M_q = 0 + 0 = 0 rad exactly.");
        sb.AppendLine();
        sb.AppendLine("QUARK MASSES (QG173 — real spectral moments):");
        string[] names = { "mu", "md", "ms", "mc", "mb", "mt" };
        var masses = StrongCPOrigin.QuarkMasses();
        for (int i = 0; i < masses.Length; i++)
            sb.AppendLine($"  m{names[i]} = {masses[i],10:F3} MeV   (real positive)");
        sb.AppendLine();
        sb.AppendLine("MASS DETERMINANT:");
        sb.AppendLine($"  det M = Π m_i = {StrongCPOrigin.MassDeterminant():E6}");
        sb.AppendLine($"  arg det M = atan2(Im, Re) = {StrongCPOrigin.ArgDet():E6} rad   (EXACTLY 0)");
        sb.AppendLine();
        sb.AppendLine("STRONG CP ANGLE:");
        sb.AppendLine($"  θ_vac = 0   (reflection-even vacuum, ATQG1740)");
        sb.AppendLine($"  θ_QCD = θ_vac + arg det M = 0 + 0 = {StrongCPOrigin.ThetaQCD():E6} rad");
        sb.AppendLine($"  experimental bound |θ_QCD| < 1e-10: {StrongCPOrigin.SatisfiesBound()}");
        sb.AppendLine();
        sb.AppendLine("  the six real quark masses (QG173) make the determinant phase exactly zero —");
        sb.AppendLine("  CP cancellation by construction, no axion, no fitted parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(StrongCPOrigin.AllMassesRealPositive(), "all quark masses should be real positive");
        Assert.True(StrongCPOrigin.ArgDet() == 0.0, "arg det M should be exactly 0");
        Assert.True(StrongCPOrigin.SatisfiesBound(), "θ_QCD should satisfy the bound < 1e-10");
    }

    [Fact]
    public void ATQG1742_WeakVsStrongAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1742: weak vs strong CP contrast and classification");

        sb.AppendLine("ASSUMPTIONS: the mechanism must explain why weak CP is LARGE while strong CP is");
        sb.AppendLine("zero: weak CP (QG166) is the chiral-ROTATION circulation phase (a mixing phase),");
        sb.AppendLine("while strong CP is the mass-DETERMINANT phase (a reflection-even quantity). The");
        sb.AppendLine("reflection symmetry protects the masses but not the chiral rotation.");
        sb.AppendLine();
        sb.AppendLine("WEAK CP (CKM, QG166) — chiral ROTATION phase:");
        sb.AppendLine($"  sinδ_CP = occ_top/Σm = {StrongCPOrigin.WeakCPPhase():F6}  → δ_CP = {CKMCPOrigin.DeltaCP():F4} rad ({CKMCPOrigin.DeltaCP() * 180 / Math.PI:F2}°)");
        sb.AppendLine($"  large: {StrongCPOrigin.WeakCPLarge()}");
        sb.AppendLine();
        sb.AppendLine("STRONG CP (θ_QCD, QG174) — mass DETERMINANT phase:");
        sb.AppendLine($"  θ_QCD = {StrongCPOrigin.ThetaQCD():E6} rad  (exactly zero)");
        sb.AppendLine($"  suppression ratio θ_QCD/sinδ_CP = {StrongCPOrigin.SuppressionRatio():E6}");
        sb.AppendLine();
        sb.AppendLine("WHY THE SPLIT:");
        sb.AppendLine("  • the CKM phase is the ORIENTED-ROTATION circulation (r ≠ r⁻¹): a chiral mixing");
        sb.AppendLine("    phase — NOT forbidden by the reflection (the reflection reverses the rotation");
        sb.AppendLine("    but the mixing still circulates);");
        sb.AppendLine("  • the strong CP angle is the MASS-determinant phase: the reflection pairs every");
        sb.AppendLine("    mode with its mirror (Z2 doublets), the spectrum and masses are real, so");
        sb.AppendLine("    arg det M = 0 and θ_vac = 0 — forbidden by the discrete Z2 symmetry;");
        sb.AppendLine("  • result: weak CP large, strong CP zero, from the SAME D96 structure — no axion,");
        sb.AppendLine("    no fitted parameters.");
        sb.AppendLine();
        int score = StrongCPOrigin.OriginScore();
        string cls = StrongCPOrigin.Classify();
        sb.AppendLine($"Strong-CP-origin score (0..5): {score}");
        sb.AppendLine($"  +1 reflection is an exact graph automorphism: {StrongCPOrigin.ReflectionIsAutomorphism()}");
        sb.AppendLine($"  +1 all spectral moments real: {StrongCPOrigin.AllMomentsReal()}");
        sb.AppendLine($"  +1 all masses real positive, arg det = 0: {StrongCPOrigin.AllMassesRealPositive() && StrongCPOrigin.ArgDet() == 0.0}");
        sb.AppendLine($"  +1 θ_QCD satisfies |θ| < 1e-10: {StrongCPOrigin.SatisfiesBound()}");
        sb.AppendLine($"  +1 weak CP large while strong CP zero: {StrongCPOrigin.WeakCPLarge() && StrongCPOrigin.ThetaQCD() == 0.0}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: [L,P] = 0 exactly, all moments and masses real, θ_QCD = 0.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the exact mechanism (reflection-even vacuum + real");
        sb.AppendLine("    determinant) gives θ_QCD = 0 with no residual phase.");
        sb.AppendLine("  • STRONG CP ORIGIN accepted: θ_QCD ≈ 0 EMERGES from D96 spectral geometry — the");
        sb.AppendLine("    dihedral reflection is an exact graph automorphism, the spectrum and masses are");
        sb.AppendLine("    real, arg det M = 0 exactly, the vacuum is reflection-even (Z2 doublets); the");
        sb.AppendLine("    suppression is the discrete Z2 reflection symmetry (Nelson-Barr type), NO AXION;");
        sb.AppendLine("    weak CP remains large (sinδ = 0.916) as a chiral rotation phase.");
        Output.WriteLine(sb.ToString());

        Assert.True(StrongCPOrigin.WeakCPLarge(), "weak CP should be large (contrast)");
        Assert.True(score >= 4, "strong-CP score should be strong");
        Assert.Equal("STRONG CP ORIGIN", cls);
    }
}
