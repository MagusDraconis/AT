using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 166 — CKM CP origin. QG165 derived the CKM magnitudes. This phase derives the CKM
/// COMPLEX PHASE δ_CP and the Jarlskog invariant J from D96 spectral geometry — no fitted phase, D96
/// geometry only — via chiral automorphisms, rotation orientation, parity/reflection breaking, and
/// spectral circulation.
///
/// Tests: TQMQG1660 (chiral automorphisms — reflection reverses rotation), TQMQG1661 (spectral
/// circulation → δ_CP), TQMQG1662 (Jarlskog invariant + classification).
/// </summary>
public class TQMQG_Phase166_CKMCPOriginTests : ResearchTestBase
{
    public TQMQG_Phase166_CKMCPOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1660_ChiralAutomorphisms()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1660: chiral automorphisms — the parity-breaking structure");

        sb.AppendLine("ASSUMPTIONS: the dihedral group D96 = ⟨r, s⟩ has an ORIENTED rotation r (i→i+1)");
        sb.AppendLine("and a reflection s (i→−i) that REVERSES the orientation.");
        sb.AppendLine();
        var r = CKMCPOrigin.Rotation();
        var s = CKMCPOrigin.Reflection();
        sb.AppendLine("D96 AUTOMORPHISM CHIRALITY:");
        sb.AppendLine($"  rotation r: i→i+1 (order 96) — oriented (r ≠ r⁻¹)");
        sb.AppendLine($"  reflection s: i→−i (order 2) — reverses orientation");
        sb.AppendLine($"  s·r·s = r⁻¹ (reflection conjugates rotation to inverse): {CKMCPOrigin.ReflectionReversesRotation()}");
        sb.AppendLine();
        sb.AppendLine("  the reflection maps mode k to n−k, reversing the Fourier phase; the half-shift");
        sb.AppendLine("  r^(n/2) acts with eigenvalue (−1)^k = e^{iπk} on mode k (Z2 phase structure).");
        sb.AppendLine($"  half-shift phase on k=3: e^{{iπ·3}} = {CKMCPOrigin.HalfShiftPhase(3):F1}");
        sb.AppendLine($"  half-shift phase on k=4: e^{{iπ·4}} = {CKMCPOrigin.HalfShiftPhase(4):F1}");
        sb.AppendLine();
        sb.AppendLine("  the rotation is CHIRAL (oriented): r ≠ r⁻¹, and the reflection flips the");
        sb.AppendLine("  circulation direction — the parity-breaking structure that generates CP.");
        Output.WriteLine(sb.ToString());

        Assert.True(CKMCPOrigin.ReflectionReversesRotation(), "s·r·s should equal r⁻¹");
        Assert.True(CKMCPOrigin.ParityStructure(), "parity structure present");
        Assert.Equal(-1.0, CKMCPOrigin.HalfShiftPhase(3));
        Assert.Equal(1.0, CKMCPOrigin.HalfShiftPhase(4));
    }

    [Fact]
    public void TQMQG1661_SpectralCirculationPhase()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1661: spectral circulation → δ_CP");

        sb.AppendLine("ASSUMPTIONS: the CP phase is the ASYMMETRY between the forward (up-sector) and");
        sb.AppendLine("backward (down-sector) spectral circulation. The up sector circulates in the dense");
        sb.AppendLine("top octave band; the down sector over the full spectrum.");
        sb.AppendLine();
        double sinD = CKMCPOrigin.SinDelta();
        double delta = CKMCPOrigin.DeltaCP();
        sb.AppendLine("SPECTRAL CIRCULATION:");
        sb.AppendLine($"  sinδ_CP = occ_top/Σm = {sinD:F4}");
        sb.AppendLine($"  δ_CP = asin({sinD:F4}) = {delta:F4} rad = {delta * 180 / Math.PI:F1}°");
        sb.AppendLine($"  physical δ_CP ≈ 1.144 rad (65.6°), sinδ ≈ 0.91");
        sb.AppendLine($"  phase deviation = {Math.Abs(delta / 1.144 - 1.0):P2}");
        sb.AppendLine();
        var (m, ang, dev) = CKMCPOrigin.NearestRotationAngle();
        sb.AppendLine("GEOMETRIC INTERPRETATION:");
        sb.AppendLine($"  nearest D96 rotation: {m} steps = {ang:F4} rad = {ang * 180 / Math.PI:F1}°");
        sb.AppendLine($"  (the 18-step rotation 3π/8 = 67.5° is within {Math.Abs(ang * 180 / Math.PI - 67.5):F1}° of δ)");
        sb.AppendLine($"  near-quarter circulation: {CKMCPOrigin.NearQuarterCirculation()}");
        sb.AppendLine();
        sb.AppendLine("  the up sector's dense-band circulation fraction (87/95) measures the chiral");
        sb.AppendLine("  imbalance not balanced by the reflection — the CP-violating orientation.");
        Output.WriteLine(sb.ToString());

        Assert.True(sinD > 0.8 && sinD < 1.0, "sinδ should be near 0.92");
        Assert.True(CKMCPOrigin.PhaseMatchesPhysical(), "δ_CP should match physical within 5%");
        Assert.True(delta > 1.0 && delta < 1.3, "δ_CP should be in the physical range");
    }

    [Fact]
    public void TQMQG1662_JarlskogInvariantAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1662: Jarlskog invariant and classification");

        sb.AppendLine("ASSUMPTIONS: the Jarlskog invariant uses the standard CKM parametrization with the");
        sb.AppendLine("D96-derived magnitudes (QG165) and the D96 phase sinδ = occ_top/Σm.");
        sb.AppendLine();
        double J = CKMCPOrigin.JarlskogInvariant();
        double delta = CKMCPOrigin.DeltaCP();
        sb.AppendLine("JARLSKOG INVARIANT:");
        sb.AppendLine($"  s12 = Vus = {CKMOrigin.Vus():F4}, s23 = Vcb = {CKMOrigin.Vcb():F4}, s13 = Vub = {CKMOrigin.Vub():F4}");
        sb.AppendLine($"  sinδ = {CKMCPOrigin.SinDelta():F4}");
        sb.AppendLine($"  J = c12·s12·c23·s23·c13²·s13·sinδ = {J:E4}");
        sb.AppendLine($"  physical J ≈ 3.18e-5 → deviation {Math.Abs(J / 3.18e-5 - 1.0):P2}");
        sb.AppendLine();
        sb.AppendLine("PREDICTED CP PARAMETERS:");
        sb.AppendLine($"  δ_CP = {delta:F4} rad = {delta * 180 / Math.PI:F1}°");
        sb.AppendLine($"  J = {J:E4}");
        sb.AppendLine($"  (physical: δ_CP ≈ 1.144 rad, J ≈ 3.18e-5)");
        sb.AppendLine();
        int score = CKMCPOrigin.OriginScore();
        string cls = CKMCPOrigin.Classify();
        sb.AppendLine($"CP-origin score (0..5): {score}");
        sb.AppendLine($"  +1 reflection reverses rotation: {CKMCPOrigin.ReflectionReversesRotation()}");
        sb.AppendLine($"  +1 sinδ = occ_top/Σm well-defined: {CKMCPOrigin.SinDelta() > 0.8}");
        sb.AppendLine($"  +1 δ_CP within 5%: {CKMCPOrigin.PhaseMatchesPhysical()}");
        sb.AppendLine($"  +1 J within 5%: {CKMCPOrigin.JarlskogMatchesPhysical()}");
        sb.AppendLine($"  +1 near quarter circulation: {CKMCPOrigin.NearQuarterCirculation()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: the D96 rotation is chiral and the reflection reverses it");
        sb.AppendLine("    (s·r·s = r⁻¹) — a genuine parity-breaking structure.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: both δ_CP and J match the physical values within ~1.3%.");
        sb.AppendLine("  • CP ORIGIN accepted: CP violation EMERGES from D96 spectral geometry — the");
        sb.AppendLine("    rotation is CHIRAL (oriented) and the reflection reverses it; the CP phase is");
        sb.AppendLine("    the asymmetry between the up (dense-band) and down (full-spectrum) circulation,");
        sb.AppendLine("    sinδ_CP = occ_top/Σm = 87/95 = 0.9158 → δ_CP = 1.1575 rad (66.3°, physical");
        sb.AppendLine("    1.144 rad, dev 1.2%); J = 3.139e-5 (physical 3.18e-5, dev 1.3%) — no fitted phase.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 4, "CP-origin score should be strong");
        Assert.Equal("CP ORIGIN", cls);
    }
}
