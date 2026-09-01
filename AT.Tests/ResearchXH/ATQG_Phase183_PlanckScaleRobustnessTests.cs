using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 183 — Planck scale robustness. Known: QG181 derives M_Pl = v·A³ (A = Σm·#g·occ₂) and
/// G = 1/M_Pl². This phase tests WHY exactly cubic — whether the exponent 3 is uniquely selected by the
/// physical Planck scale — no fitted exponents, D96 only, deterministic.
///
/// Tests: ATQG1830 (physical exponent pinned at 3), ATQG1831 (only the cube reproduces M_Pl among
/// A¹..A⁴ and nearby exponents), ATQG1832 (uniqueness among alternative A definitions + classification).
/// </summary>
public class ATQG_Phase183_PlanckScaleRobustnessTests : ResearchTestBase
{
    public ATQG_Phase183_PlanckScaleRobustnessTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1830_PhysicalExponentIsCubic()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1830: the physical Planck mass pins the exponent at 3");

        sb.AppendLine("ASSUMPTIONS: the physical Planck mass M_Pl = 1.22089e19 GeV and the D96 weak scale");
        sb.AppendLine("v = 254.37 GeV (QG168) fix the exponent p = ln(M_Pl/v)/ln(A) with NO fitting. If the");
        sb.AppendLine("cubic scaling is real, p must land at 3 to high precision; p near 2 or 4 would");
        sb.AppendLine("select a different power.");
        sb.AppendLine();
        sb.AppendLine("D96 QUANTITIES:");
        sb.AppendLine($"  v = {PlanckScaleRobustness.WeakScaleGeV():F4} GeV");
        sb.AppendLine($"  A = Σm·#g·occ₂ = {PlanckScaleRobustness.SpectralContent():F0}");
        sb.AppendLine($"  M_Pl/v = {PlanckScaleRobustness.MPlanckPhysical / PlanckScaleRobustness.WeakScaleGeV():E6}");
        sb.AppendLine();
        sb.AppendLine("PHYSICAL EXPONENT:");
        double p = PlanckScaleRobustness.PhysicalExponent();
        sb.AppendLine($"  p = ln(M_Pl/v)/ln(A) = {p:F8}");
        sb.AppendLine($"  |p − 3| = {PlanckScaleRobustness.ExponentDeviation():E4}");
        sb.AppendLine($"    (cubic to 1 part in {1.0 / PlanckScaleRobustness.ExponentDeviation():F0})");
        sb.AppendLine($"  |p − 2| = {Math.Abs(p - 2.0):F3}  (quadratic would need p=2)");
        sb.AppendLine($"  |p − 4| = {Math.Abs(p - 4.0):F3}  (quartic would need p=4)");
        sb.AppendLine();
        sb.AppendLine($"  exponent pinned at 3 within 1%: {PlanckScaleRobustness.ExponentIsCubic()}");
        Output.WriteLine(sb.ToString());

        Assert.True(PlanckScaleRobustness.ExponentIsCubic(), "physical exponent should be near 3");
        Assert.True(Math.Abs(p - 3.0) < 0.01, "exponent should be 3 within 1%");
        Assert.True(Math.Abs(p - 2.0) > 0.9 && Math.Abs(p - 4.0) > 0.9,
            "exponent should be far from 2 and 4");
    }

    [Fact]
    public void ATQG1831_OnlyTheCubeReproducesPlanckMass()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1831: only the cube reproduces M_Pl among A¹..A⁴ and nearby exponents");

        sb.AppendLine("ASSUMPTIONS: if the cubic is uniquely selected, v·A³ must match the physical M_Pl");
        sb.AppendLine("while A¹, A², A⁴ and nearby exponents (2.9, 2.95, 3.05, 3.1) fail by wide margins.");
        sb.AppendLine();
        sb.AppendLine("POWER TEST (v·A^p vs physical M_Pl):");
        (string, double)[] powers =
        {
            ("A^1", 1.0), ("A^2", 2.0), ("A^3", 3.0), ("A^4", 4.0),
        };
        foreach (var (name, e) in powers)
            sb.AppendLine($"  {name}: {PlanckScaleRobustness.PowerScale(e):E6} GeV  dev {PlanckScaleRobustness.PowerDeviation(e):P4}");
        sb.AppendLine();
        sb.AppendLine("NEARBY EXPONENTS:");
        foreach (double e in new[] { 2.9, 2.95, 3.0, 3.05, 3.1 })
            sb.AppendLine($"  A^{e:F2}: dev {PlanckScaleRobustness.PowerDeviation(e):P3}");
        sb.AppendLine();
        sb.AppendLine($"  cubic dev = {PlanckScaleRobustness.CubicDeviation():P4}");
        sb.AppendLine($"  quadratic dev = {PlanckScaleRobustness.QuadraticDeviation():P4}");
        sb.AppendLine($"  quartic dev = {PlanckScaleRobustness.QuarticDeviation():P4}");
        sb.AppendLine();
        sb.AppendLine($"  cube uniquely reproduces M_Pl: {PlanckScaleRobustness.CubicIsUnique()}");
        Output.WriteLine(sb.ToString());

        Assert.True(PlanckScaleRobustness.CubicIsUnique(), "only A³ should reproduce M_Pl");
        Assert.True(PlanckScaleRobustness.CubicDeviation() < 0.02, "cubic deviation should be under 2%");
        Assert.True(PlanckScaleRobustness.QuadraticDeviation() > 0.5, "quadratic should fail");
        Assert.True(PlanckScaleRobustness.QuarticDeviation() > 0.5, "quartic should fail");
    }

    [Fact]
    public void ATQG1832_UniquenessAmongAlternativesAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1832: uniqueness among alternative A definitions and classification");

        sb.AppendLine("ASSUMPTIONS: the cubic is robust only if the QG181 A = Σm·#g·occ₂ is the UNIQUE");
        sb.AppendLine("D96 product that (a) implies p ≈ 3 and (b) reproduces M_Pl at A³. Alternative D96");
        sb.AppendLine("products must fail at least one test. The 3-factor structure (Σm·#g·occ₂, 3 octave");
        sb.AppendLine("bands, d = 3) makes the cube the natural exponent.");
        sb.AppendLine();
        sb.AppendLine("ALTERNATIVE A DEFINITIONS:");
        sb.AppendLine("  " + "A definition".PadRight(22) + "A".PadLeft(10) + "p".PadLeft(8) + "  A^3 dev");
        foreach (var (name, aval, p, dev3) in PlanckScaleRobustness.Alternatives())
            sb.AppendLine($"  {name,-22} {aval,-10:F0} {p,-8:F4} {dev3:P3}");
        sb.AppendLine();
        sb.AppendLine("D96 STRUCTURE:");
        sb.AppendLine($"  A has {PlanckScaleRobustness.SpectralContentFactors()} multiplicative factors");
        sb.AppendLine($"  octave bands = {PlanckScaleRobustness.OctaveBandCount()} (occupancies [4,4,87])");
        sb.AppendLine($"  spatial dimension d = 3, families = 3 (QG80)");
        sb.AppendLine();
        sb.AppendLine($"  A uniquely selects cubic: {PlanckScaleRobustness.AIsUniqueSelection()}");
        sb.AppendLine($"  3-factor structure holds: {PlanckScaleRobustness.ThreeFactorStructureHolds()}");
        sb.AppendLine();
        int score = PlanckScaleRobustness.OriginScore();
        string cls = PlanckScaleRobustness.Classify();
        sb.AppendLine($"Robustness score (0..3): {score}");
        sb.AppendLine($"  +1 exponent pinned at 3: {PlanckScaleRobustness.ExponentIsCubic()}");
        sb.AppendLine($"  +1 only the cube reproduces M_Pl: {PlanckScaleRobustness.CubicIsUnique()}");
        sb.AppendLine($"  +1 A uniquely selects cubic + 3-factor structure: {PlanckScaleRobustness.AIsUniqueSelection() && PlanckScaleRobustness.ThreeFactorStructureHolds()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • COINCIDENCE rejected: the physical Planck mass pins the exponent to p = 2.99984");
        sb.AppendLine("    (cubic to 1e-4), and no other power or A definition reproduces M_Pl.");
        sb.AppendLine("  • PARTIAL rejected: A¹, A², A⁴ and nearby exponents fail by 47%–3.6e7%, and every");
        sb.AppendLine("    alternative D96 product fails either the exponent or the cubic test.");
        sb.AppendLine("  • ROBUST ORIGIN accepted: the cubic is UNIQUELY selected — M_Pl = v·A³ with");
        sb.AppendLine("    A = Σm·#g·occ₂ is the only power (3) and the only product (of Σm, #g, occ₂) that");
        sb.AppendLine("    reproduces the Planck scale (0.2%), because A is a three-factor spectral content");
        sb.AppendLine("    in a 3-band, 3-dimensional spectrum. No fitted exponents.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 3, "robustness score should be maximal");
        Assert.Equal("ROBUST ORIGIN", cls);
    }
}
