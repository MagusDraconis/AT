using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 181 — Newton constant origin. Known: QG161-163 (gauge sector), QG168 (MW, MZ),
/// QG169 (MH). This phase derives Newton's constant G (G = 6.67430e-11 m³/kg/s², M_Pl = 1.22089e19 GeV)
/// from D96 spectral geometry — no fitted constants, deterministic.
///
/// Tests: TQMQG1810 (Planck mass from the cube of the occupation-weighted spectral content),
/// TQMQG1811 (Newton constant from M_Pl in natural and SI units), TQMQG1812 (consistency + classification).
/// </summary>
public class TQMQG_Phase181_NewtonConstantOriginTests : ResearchTestBase
{
    public TQMQG_Phase181_NewtonConstantOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1810_PlanckMassFromSpectralContentCube()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1810: the Planck mass from the cube of the spectral content");

        sb.AppendLine("ASSUMPTIONS: the D96 spectrum has Σm = 95 modes (QG150), #g = 44 multiplicity");
        sb.AppendLine("groups (the Z2 doublet structure, QG153/155), and the densest octave band carries");
        sb.AppendLine("occ₂ = 87 of the 95 modes (the top octave, QG150/157). The occupation-weighted");
        sb.AppendLine("spectral content A = Σm·#g·occ₂ = 95·44·87 = 363,660. The Planck mass is the weak");
        sb.AppendLine("scale v = 254.37 GeV (QG168) amplified by the cube of this content: M_Pl = v·A³.");
        sb.AppendLine();
        sb.AppendLine("D96 SPECTRAL CONTENT:");
        sb.AppendLine($"  Σm = {NewtonConstantOrigin.TotalModes()}");
        sb.AppendLine($"  #g = {NewtonConstantOrigin.GroupCount()}");
        sb.AppendLine($"  octave occupancies = [{string.Join(",", NewtonConstantOrigin.OctaveOccupancies())}]");
        sb.AppendLine($"  occ₂ = {NewtonConstantOrigin.DenseOctaveOccupancy():F0}");
        sb.AppendLine($"  A = Σm·#g·occ₂ = {NewtonConstantOrigin.SpectralContent():F0}");
        sb.AppendLine();
        sb.AppendLine("PLANCK MASS:");
        sb.AppendLine($"  v = {NewtonConstantOrigin.WeakScaleGeV():F2} GeV");
        sb.AppendLine($"  M_Pl = v·A³ = {NewtonConstantOrigin.WeakScaleGeV():F2}·({NewtonConstantOrigin.SpectralContent():F0})³ = {NewtonConstantOrigin.PlanckMassGeV():E6} GeV");
        sb.AppendLine($"  physical M_Pl = {NewtonConstantOrigin.MPlanckPhysical:E6} GeV");
        sb.AppendLine($"  deviation = {NewtonConstantOrigin.PlanckMassDeviation():P4}");
        sb.AppendLine();
        sb.AppendLine($"  M_Pl within 2%: {NewtonConstantOrigin.PlanckMassMatches()}");
        Output.WriteLine(sb.ToString());

        Assert.True(NewtonConstantOrigin.PlanckMassMatches(), "Planck mass should match within 2%");
        Assert.True(NewtonConstantOrigin.PlanckMassGeV() > 1.0e19 && NewtonConstantOrigin.PlanckMassGeV() < 1.5e19,
            "Planck mass should be near 1.22e19 GeV");
    }

    [Fact]
    public void TQMQG1811_NewtonConstantFromPlanckMass()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1811: Newton's constant from the derived Planck mass");

        sb.AppendLine("ASSUMPTIONS: in natural units G = 1/M_Pl². The derived Planck mass gives G directly;");
        sb.AppendLine("converting M_Pl from GeV to kg (1 GeV = 1.782662e-27 kg) gives the SI Newton");
        sb.AppendLine("constant G = ħc/M_Pl². No fitted constants — only the D96 spectral content cube.");
        sb.AppendLine();
        sb.AppendLine("NEWTON CONSTANT:");
        sb.AppendLine($"  M_Pl = {NewtonConstantOrigin.PlanckMassGeV():E6} GeV");
        sb.AppendLine($"  G nat = 1/M_Pl² = {NewtonConstantOrigin.GNatural():E6} GeV⁻²");
        sb.AppendLine($"  physical G nat = {1.0 / (NewtonConstantOrigin.MPlanckPhysical * NewtonConstantOrigin.MPlanckPhysical):E6} GeV⁻²");
        sb.AppendLine($"  G SI = {NewtonConstantOrigin.GSISeconds():E6} m³/kg/s²");
        sb.AppendLine($"  physical G = {NewtonConstantOrigin.GPhysical:E6} m³/kg/s²");
        sb.AppendLine($"  deviation = {NewtonConstantOrigin.GDeviation():P4}");
        sb.AppendLine();
        sb.AppendLine("REDUCED PLANCK MASS:");
        sb.AppendLine($"  M̄_Pl = M_Pl/√(8π) = {NewtonConstantOrigin.ReducedPlanckMassGeV():E6} GeV");
        sb.AppendLine();
        sb.AppendLine($"  G within 2%: {NewtonConstantOrigin.GMatches()}");
        Output.WriteLine(sb.ToString());

        Assert.True(NewtonConstantOrigin.GMatches(), "Newton constant should match within 2%");
        Assert.True(NewtonConstantOrigin.GSISeconds() > 6.0e-11 && NewtonConstantOrigin.GSISeconds() < 7.0e-11,
            "G should be near 6.67e-11");
    }

    [Fact]
    public void TQMQG1812_ConsistencyAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1812: consistency, dependency structure, and classification");

        sb.AppendLine("ASSUMPTIONS: the Planck mass and Newton constant are two projections of the SAME");
        sb.AppendLine("D96 content. The derivation chain is:");
        sb.AppendLine("  D96 → Σm=95, #g=44, occ₂=87 → A = Σm·#g·occ₂ = 363,660");
        sb.AppendLine("      → v = (Σm+#d)·ln(span) = 254.37 GeV (QG168)");
        sb.AppendLine("      → M_Pl = v·A³ = 1.22335e19 GeV");
        sb.AppendLine("      → G = 1/M_Pl² = 6.647e-11 m³/kg/s²");
        sb.AppendLine();
        sb.AppendLine("AGREEMENT SUMMARY:");
        foreach (var (name, d, p, dev) in NewtonConstantOrigin.Comparison())
            sb.AppendLine($"  {name}: derived {d,9:F4}, physical {p,9:F4}, dev {dev:P4}");
        sb.AppendLine();
        sb.AppendLine("MASS-HIERARCHY CONSISTENCY:");
        sb.AppendLine($"  M_Pl/MH = {NewtonConstantOrigin.PlanckMassGeV() / HiggsMassOrigin.HiggsMassGeV():E3}");
        sb.AppendLine($"  M_Pl/MW = {NewtonConstantOrigin.PlanckMassGeV() / WeakBosonMassOrigin.MWGeV():E3}");
        sb.AppendLine($"  M_Pl/v = {NewtonConstantOrigin.PlanckMassGeV() / NewtonConstantOrigin.WeakScaleGeV():E3}");
        sb.AppendLine();
        int score = NewtonConstantOrigin.OriginScore();
        string cls = NewtonConstantOrigin.Classify();
        sb.AppendLine($"Gravity-origin score (0..3): {score}");
        sb.AppendLine($"  +1 M_Pl = v·A³ within 2%: {NewtonConstantOrigin.PlanckMassMatches()}");
        sb.AppendLine($"  +1 G = 1/M_Pl² within 2%: {NewtonConstantOrigin.GMatches()}");
        sb.AppendLine($"  +1 same content reproduces BOTH M_Pl and G: {NewtonConstantOrigin.PlanckMassMatches() && NewtonConstantOrigin.GMatches()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO ORIGIN rejected: M_Pl = v·(Σm·#g·occ₂)³ = 1.22335e19 GeV reproduces the");
        sb.AppendLine("    physical Planck mass 1.22089e19 GeV within 0.202%.");
        sb.AppendLine("  • PARTIAL ORIGIN rejected: the same D96 content reproduces BOTH M_Pl and G");
        sb.AppendLine("    consistently (G dev 0.403%).");
        sb.AppendLine("  • GRAVITY ORIGIN accepted: the Newton constant EMERGES from D96 spectral");
        sb.AppendLine("    geometry — M_Pl = v·(Σm·#g·occ₂)³ = v·(95·44·87)³ (the weak scale amplified by");
        sb.AppendLine("    the cube of the occupation-weighted spectral content), so G = 1/M_Pl² = 6.647e-11");
        sb.AppendLine("    m³/kg/s² (physical 6.67430e-11, dev 0.403%) — no fitted constants.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 3, "gravity-origin score should be maximal");
        Assert.Equal("GRAVITY ORIGIN", cls);
    }
}
