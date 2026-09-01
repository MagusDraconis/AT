using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 154 — Origin of the neutrino sector. QG138-QG153 derive families, hierarchies, mode access
/// and the Z2 doublets; QG148 showed the linear exponent law overfits (neutrino prediction deviates 103%).
/// This phase asks WHY the neutrino sector deviates from the lepton and quark scaling laws.
///
/// Tests: ATQG1540 (neutral-charge limit + T3-only access), ATQG1541 (doublet occupancy + spectral
/// accessibility), ATQG1542 (neutrino hierarchy + classification).
/// </summary>
public class ATQG_Phase154_NeutrinoOriginTests : ResearchTestBase
{
    public ATQG_Phase154_NeutrinoOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1540_NeutralChargeLimitAndT3OnlyAccess()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1540: neutral-charge limit and T3-only access");

        double pNu = NeutrinoOrigin.NeutrinoExponent();
        double dNu = NeutrinoOrigin.NeutrinoDelta();
        sb.AppendLine($"NEUTRINO HIERARCHY: p_eff = log(ν3/ν1)/log(4) = {pNu:F4}, δ_eff = p/2 = {dNu:F4}");
        sb.AppendLine();
        sb.AppendLine("NEUTRAL-CHARGE LIMIT (Q = 0):");
        sb.AppendLine($"  neutrino is the UNIQUE neutral fermion: {NeutrinoOrigin.UniqueNeutralSector()}");
        sb.AppendLine($"  charge amplification Q^n (n = {NeutrinoOrigin.ChargePower}): {NeutrinoOrigin.NeutrinoChargeAmplification():E3}");
        sb.AppendLine($"  neutral-charge limit holds: {NeutrinoOrigin.NeutralChargeLimit()}");
        sb.AppendLine();
        sb.AppendLine("T3-ONLY ACCESS (no charge channel → Z2 channel access):");
        sb.AppendLine($"  T3=+1/2 Z2 channel Weyl = {NeutrinoOrigin.T3PlusChannelWeyl():F4}");
        sb.AppendLine($"  neutrino δ vs channel Weyl deviation = {NeutrinoOrigin.NeutrinoT3ChannelDeviation():P1}");
        sb.AppendLine($"  T3-only access holds: {NeutrinoOrigin.T3OnlyAccess()}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: with Q = 0 the charge-dependent mode access vanishes identically, so the");
        sb.AppendLine("neutrino reverts to T3-only Z2-channel spectral access (δ ≈ channel Weyl within 3.3%).");
        Output.WriteLine(sb.ToString());

        Assert.True(NeutrinoOrigin.NeutralChargeLimit(), "the neutrino should be the unique neutral sector with vanishing charge channel");
        Assert.True(NeutrinoOrigin.T3OnlyAccess(), "the neutrino dimension should match the T3=+1/2 channel Weyl");
        Assert.True(NeutrinoOrigin.NeutrinoT3ChannelDeviation() < 0.05, "T3-channel match should be tight");
    }

    [Fact]
    public void ATQG1541_DoubletOccupancyAndSpectralAccessibility()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1541: doublet occupancy and spectral accessibility");

        sb.AppendLine("DOUBLET OCCUPANCY (T3=+1/2 vs T3=−1/2 member of each weak doublet):");
        foreach (var (d, r, l2) in NeutrinoOrigin.DoubletOccupancy())
            sb.AppendLine($"  {d}: r31 ratio = {r:F3}  log2 = {l2:F4}");
        sb.AppendLine($"  lepton doublet inverted (neutrino NOT the enhanced member): {NeutrinoOrigin.LeptonDoubletInverted()}");
        sb.AppendLine();
        sb.AppendLine("SPECTRAL ACCESSIBILITY (effective dimensions δ = p/2):");
        foreach (var (n, de) in NeutrinoOrigin.AllSectorDeltas())
            sb.AppendLine($"  {n}: δ_eff = {de:F4}");
        sb.AppendLine($"  neutrino is the MINIMUM dimension: {NeutrinoOrigin.NeutrinoIsMinimum()}");
        sb.AppendLine($"  neutrino δ / full-spectrum Weyl = {NeutrinoOrigin.NeutrinoVsFullWeyl():F4}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: in the quark doublet the up (T3=+1/2) member is enhanced (log2 = 6.47); in");
        sb.AppendLine("the lepton doublet the electron is enhanced and the neutrino is the suppressed neutral");
        sb.AppendLine("member — the neutrino has the LOWEST dimension of all fermion sectors.");
        Output.WriteLine(sb.ToString());

        var occ = NeutrinoOrigin.DoubletOccupancy();
        Assert.True(occ[0].Log2 > 4.0, "quark doublet should show a strong up-enhancement");
        Assert.True(occ[1].Log2 > 1.0, "lepton doublet ratio should be substantial");
        Assert.True(NeutrinoOrigin.NeutrinoIsMinimum(), "neutrino should be the minimum dimension");
    }

    [Fact]
    public void ATQG1542_NeutrinoHierarchyAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1542: neutrino hierarchy and classification");

        double pNu = NeutrinoOrigin.NeutrinoExponent();
        sb.AppendLine("NEUTRINO HIERARCHY:");
        sb.AppendLine($"  ν3/ν1 = {NeutrinoOrigin.NeutrinoHierarchyRatio()}");
        sb.AppendLine($"  p_eff = log(500)/log(4) = {pNu:F4}");
        sb.AppendLine();
        sb.AppendLine("THE QG147 LINEAR LAW OVERFIT:");
        sb.AppendLine($"  predicted neutrino p (Q=0, T3=+1/2) = {NeutrinoOrigin.LinearLawNeutrinoPrediction():F4}");
        sb.AppendLine($"  observed = {pNu:F4}, deviation = {NeutrinoOrigin.LinearLawNeutrinoDeviation():P1}");
        sb.AppendLine($"  linear law fails for the neutrino: {NeutrinoOrigin.LinearLawFailsForNeutrino()}");
        sb.AppendLine();
        int score = NeutrinoOrigin.OriginScore();
        string cls = NeutrinoOrigin.Classify();
        sb.AppendLine($"neutrino-origin score (0..5): {score}");
        sb.AppendLine($"  +1 neutral-charge limit: {NeutrinoOrigin.NeutralChargeLimit()}");
        sb.AppendLine($"  +1 T3-only access: {NeutrinoOrigin.T3OnlyAccess()}");
        sb.AppendLine($"  +1 lepton doublet inverted: {NeutrinoOrigin.LeptonDoubletInverted()}");
        sb.AppendLine($"  +1 neutrino is minimum: {NeutrinoOrigin.NeutrinoIsMinimum()}");
        sb.AppendLine($"  +1 linear law fails: {NeutrinoOrigin.LinearLawFailsForNeutrino()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: the neutral-charge limit, T3-only access, and doublet");
        sb.AppendLine("    inversion provide a complete mechanism.");
        sb.AppendLine("  • NEUTRINO ORIGIN accepted: the neutrino deviates because it is the ONLY neutral");
        sb.AppendLine("    fermion. The charge-dependent mode amplification vanishes identically (Q^n = 0),");
        sb.AppendLine("    the charge×isospin enhancement that boosts other T3=+1/2 sectors cannot act, and");
        sb.AppendLine("    the neutrino reverts to T3-only Z2-channel spectral access (δ ≈ T3=+1/2 channel");
        sb.AppendLine("    Weyl, 3.3%), making it the lowest (suppressed) sector. This explains why the");
        sb.AppendLine("    QG147 linear law overfits: it predicts a charge-enhanced neutrino that cannot");
        sb.AppendLine("    exist.");
        Output.WriteLine(sb.ToString());

        Assert.True(NeutrinoOrigin.LinearLawFailsForNeutrino(), "the QG147 law should fail for the neutrino");
        Assert.True(score >= 4, "neutrino-origin score should be strong");
        Assert.Equal("NEUTRINO ORIGIN", cls);
    }
}
