using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 197 — 2D To 3D Bridge. Can d≥3 gravity be derived from the native 2D program? Answer: YES —
/// the counting measure ρ and the conformal ansatz g = ρ^(2/d)η are dimension-generic; the Einstein tensor
/// components are analytic in d, and the (d−2) factor is the bridge from the 2D degeneracy (G≡0, G4-G0) to the
/// non-trivial d=3 structure (G4-G2/G3). No new primitives. Deterministic.
/// </summary>
public class ATQG_Phase197_D2ToD3BridgeTests : ResearchTestBase
{
    public ATQG_Phase197_D2ToD3BridgeTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1970_TwoDProgramProducesTheGenericConformalAnsatz()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1970: the 2D program produces ρ and the dimension-generic conformal ansatz");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The native 2D program produces ρ (the counting measure) and g = ρ^(2/d)η.");
        sb.AppendLine("  - In d=2, R_μν = (R/2)g_μν always ⇒ the Einstein tensor vanishes identically (G4-G0).");
        sb.AppendLine("  - The 2D degeneracy is a geometric identity, not a failure of the actualization content.");
        sb.AppendLine();

        bool vanishes2D = D2ToD3Bridge.EinsteinVanishesIn2D();
        double g11_2 = HigherDimEinstein.Einstein11(0.4, 1.0, 2);
        double gii_2 = HigherDimEinstein.EinsteinOther(0.4, 1.0, 2);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ρ = 1 + x² (the counting measure, dimension-independent)");
        sb.AppendLine($"  d=2: G_11 = {g11_2:F2e}, G_ii = {gii_2:F2e}  (both vanish)");
        sb.AppendLine($"  Einstein tensor vanishes identically in d=2? {vanishes2D}");
        sb.AppendLine($"  (d−2) factor at d=2: {D2ToD3Bridge.DMinusTwoFactor(2):F1}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The 2D program's output is ρ and the conformal ansatz g = ρ^(2/d)η — both are");
        sb.AppendLine("    dimension-independent in form.");
        sb.AppendLine("  - The d=2 Einstein tensor is identically zero: a geometric identity (R_μν=(R/2)g),");
        sb.AppendLine("    not a defect of the actualization content.");

        Output.WriteLine(sb.ToString());

        Assert.True(vanishes2D, "the Einstein tensor must vanish identically in d=2");
        Assert.Equal(0.0, g11_2, 6);
        Assert.Equal(0.0, gii_2, 6);
    }

    [Fact]
    public void ATQG1971_SameRhoAtD3GivesNonTrivialEinstein()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1971: the SAME ρ at d=3 gives a non-trivial Einstein tensor");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The conformal ansatz g = ρ^(2/d)η is analytic in d — the same ρ, only d changes.");
        sb.AppendLine("  - G_11 = ((d−1)(d−2)/2)(σ′)², G_ii = (d−2)[σ″+((d−3)/2)(σ′)²] — the (d−2) factor is the bridge.");
        sb.AppendLine();

        double g11_3 = D2ToD3Bridge.Einstein11AtD3();
        double gii_3 = D2ToD3Bridge.EinsteinOtherAtD3();
        bool analytic = D2ToD3Bridge.EinsteinIsAnalyticInD();
        bool connects = D2ToD3Bridge.BridgeConnects2DTo3D();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  d=3: G_11 = {g11_3:F6}, G_ii = {gii_3:F6}  (SAME ρ = 1+x², x=0.4)");
        sb.AppendLine($"  (d−2) factor: d=2 → {D2ToD3Bridge.DMinusTwoFactor(2):F0}, d=3 → {D2ToD3Bridge.DMinusTwoFactor(3):F0}, d=4 → {D2ToD3Bridge.DMinusTwoFactor(4):F0}");
        sb.AppendLine($"  Einstein analytic in d (same ρ)? {analytic}");
        sb.AppendLine($"  bridge connects 2D (G≡0) to 3D (G≠0)? {connects}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The SAME counting measure ρ, evaluated at d=3, gives a NON-TRIVIAL Einstein tensor.");
        sb.AppendLine("  - The (d−2) factor is the continuous bridge: zero at d=2, non-zero at d≥3.");
        sb.AppendLine("  - No new primitive, no imported GR — only the native conformal curvature at d=3.");

        Output.WriteLine(sb.ToString());

        Assert.True(g11_3 > 0 && gii_3 > 0, "G must be non-trivial at d=3 for the same ρ");
        Assert.True(analytic, "the Einstein tensor must be analytic in d");
        Assert.True(connects, "the bridge must connect the 2D degeneracy to the 3D structure");
    }

    [Fact]
    public void ATQG1972_ClassificationFullBridge()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1972: 2D→3D bridge classification");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Classification is data-driven from the phase-197 bridge construction.");
        sb.AppendLine("  - Same ρ + analytic continuation + conserved d=3 structure ⇒ FULL BRIDGE.");
        sb.AppendLine();

        int score = D2ToD3Bridge.BridgeScore();
        string classification = D2ToD3Bridge.Classify();
        bool bianchi = D2ToD3Bridge.BianchiHoldsAtD3();
        bool dGe3 = D2ToD3Bridge.DGt3Required();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  BridgeScore (max 3) = {score}");
        sb.AppendLine($"    +1 2D program produces ρ and the dimension-generic conformal ansatz (G≡0 geometric)");
        sb.AppendLine($"    +1 SAME ρ at d=3 gives non-trivial G (analytic continuation via (d−2))");
        sb.AppendLine($"    +1 d=3 G conserved (Bianchi) and d≥3 is the derived requirement (QG2)");
        sb.AppendLine($"  Bianchi (divergence-free) at d=3? {bianchi}");
        sb.AppendLine($"  d≥3 required for gravity (QG2)?   {dGe3}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  The native program starts in 2D, but its content (ρ, the conformal ansatz) is");
        sb.AppendLine("  dimension-generic. The Einstein tensor is analytic in d, with the (d−2) factor");
        sb.AppendLine("  the bridge: G ≡ 0 at d=2 (G4-G0 geometric identity) → G ≠ 0 at d=3 (G4-G2/G3),");
        sb.AppendLine("  conserved (Bianchi), with d≥3 the derived lower bound (QG2). 2D actualization");
        sb.AppendLine("  connects to the 3D Einstein structure with no new primitives.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("FULL BRIDGE", classification);
        Assert.True(score == 3, "all three bridge channels (generic ansatz, analytic continuation, conservation)");
        Assert.True(bianchi, "the d=3 Einstein tensor must be conserved");
    }
}
