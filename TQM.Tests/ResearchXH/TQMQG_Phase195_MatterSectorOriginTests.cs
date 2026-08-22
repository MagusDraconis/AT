using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 195 — Matter Sector Origin. Can an independent T_μν be recovered without defining T ≡ G/κ?
/// Answer: the matter sector is the DEFICIT DUST T_μν = (ρ̄−ρ)·v_μ·v_ν — built from the conserved deficit
/// mass (QG194) and the actualization flow, conserved (Noether + geodesic), and independent of G (escapes
/// the G4-G4 Lovelock obstruction). No new primitives.
/// </summary>
public class TQMQG_Phase195_MatterSectorOriginTests : ResearchTestBase
{
    public TQMQG_Phase195_MatterSectorOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG1950_NetworkStressIsDeficitDust()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1950: the matter tensor is the deficit dust (network stress = deficit mass)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Matter = deficit mass density ρ_m = ρ̄ − ρ (QG194, DEFICIT ORIGIN).");
        sb.AppendLine("  - Energy = actualization rate (QG89); the deficit carries rest mass (E = mc²).");
        sb.AppendLine("  - T_μν = ρ_m·v_μ·v_ν (dust from the conserved deficit mass and the actualization flow).");
        sb.AppendLine();

        double rhoBar = 1.0, rho = 0.916;
        double rhoM = MatterSectorOrigin.DeficitMassDensity(rhoBar, rho);
        double v0 = 1.0, v1 = 0.3;
        double t00 = MatterSectorOrigin.MatterTensor00(rhoBar, rho, v0);
        double t01 = MatterSectorOrigin.MatterTensor0i(rhoBar, rho, v0, v1);
        double t11 = MatterSectorOrigin.MatterTensorij(rhoBar, rho, v1, v1);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ρ̄ = {rhoBar:F3}, ρ(void) = {rho:F3}");
        sb.AppendLine($"  deficit mass density ρ_m = ρ̄ − ρ = {rhoM:F4}");
        sb.AppendLine($"  flow 4-velocity v = ({v0:F1}, {v1:F1}, 0, 0)");
        sb.AppendLine($"  T^00 = ρ_m·v0² = {t00:F4}");
        sb.AppendLine($"  T^01 = ρ_m·v0·v1 = {t01:F4}");
        sb.AppendLine($"  T^11 = ρ_m·v1² = {t11:F4}");
        sb.AppendLine($"  deficit carries rest mass (E=mc²)? {MatterSectorOrigin.DeficitCarriesRestMass()}");
        sb.AppendLine($"  energy = actualization rate (QG89)? {MatterSectorOrigin.EnergyIsActualizationRate()}");
        sb.AppendLine($"  deficit positive in voids (attractive)? {MatterSectorOrigin.DeficitPositiveInVoids(rhoBar, rho)}");

        Output.WriteLine(sb.ToString());

        Assert.True(t00 > 0, "T00 is the deficit mass energy density (positive in voids)");
        Assert.True(MatterSectorOrigin.DeficitCarriesRestMass() && MatterSectorOrigin.EnergyIsActualizationRate(),
            "the deficit is link energy carrying rest mass");
        Assert.True(MatterSectorOrigin.DeficitPositiveInVoids(rhoBar, rho));
    }

    [Fact]
    public void TQMQG1951_ConservationAndIndependence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1951: the deficit dust is conserved and independent of G");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - ∇_μT^μν = v^ν·∇_μ(ρ_m v^μ) + ρ_m·v^μ∇_μv^ν = 0 via (a) Noether mass conservation");
        sb.AppendLine("    and (b) geodesic flow — the dust is a valid conserved stress-energy.");
        sb.AppendLine("  - T is built from ρ_m and v (matter), NOT from the metric geometry — it escapes the");
        sb.AppendLine("    G4-G4 Lovelock obstruction (which forces geometric tensors to be G/κ).");
        sb.AppendLine();

        bool conserved = MatterSectorOrigin.DustIsConserved();
        bool independent = MatterSectorOrigin.IndependentOfG();
        bool distinct = MatterSectorOrigin.MatterTensorDistinctFromG();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  deficit mass conserved (Noether, QG194)?  {MatterSectorOrigin.DeficitMassConserved()}");
        sb.AppendLine($"  flow is geodesic (QG20-21)?               {MatterSectorOrigin.FlowIsGeodesic()}");
        sb.AppendLine($"  dust conserved (mass × geodesic)?          {conserved}");
        sb.AppendLine($"  G4-G4 Lovelock forces geometric tensors → G/κ? {MatterSectorOrigin.G4G_LovelockForcesGeometricTensor()}");
        sb.AppendLine($"  deficit dust independent of G (escapes)?    {independent}");
        sb.AppendLine($"  matter tensor distinct from G/κ?            {distinct}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - T_μν = ρ_m·v_μ·v_ν is conserved: the deficit mass current is conserved (Noether)");
        sb.AppendLine("    and the actualization flow is geodesic — both established TRM results.");
        sb.AppendLine("  - T involves the deficit VALUE and the flow velocity, not the metric geometry alone,");
        sb.AppendLine("    so the G4-G4 Lovelock obstruction does not apply. G = κT is a dynamical relation,");
        sb.AppendLine("    not an identity.");

        Output.WriteLine(sb.ToString());

        Assert.True(conserved, "the deficit dust must be conserved");
        Assert.True(independent, "the deficit dust must be independent of G (escapes Lovelock)");
        Assert.True(distinct, "T must be distinct from G/κ");
    }

    [Fact]
    public void TQMQG1952_ClassificationMatterOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1952: matter-sector origin classification");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Classification is data-driven from the phase-195 derivation.");
        sb.AppendLine("  - Deficit dust + conservation + independence ⇒ MATTER ORIGIN.");
        sb.AppendLine();

        int score = MatterSectorOrigin.OriginScore();
        string classification = MatterSectorOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  OriginScore (max 3) = {score}");
        sb.AppendLine($"    +1 matter tensor = deficit dust (network stress = deficit mass, link energy)");
        sb.AppendLine($"    +1 dust conserved (Noether mass conservation + geodesic flow)");
        sb.AppendLine($"    +1 T independent of G (escapes G4-G4 Lovelock); no new primitives");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  The matter sector is recovered WITHOUT defining T ≡ G/κ:");
        sb.AppendLine("    • T_μν = (ρ̄−ρ)·v_μ·v_ν — the deficit dust, from the conserved deficit mass (QG194)");
        sb.AppendLine("      and the actualization flow;");
        sb.AppendLine("    • conserved: Noether deficit-mass conservation + geodesic flow;");
        sb.AppendLine("    • independent of G: built from ρ_m and v, escaping the G4-G4 Lovelock obstruction;");
        sb.AppendLine("    • G = κT becomes a DYNAMICAL field equation (the deficit sources curvature).");
        sb.AppendLine("  No new primitives.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MATTER ORIGIN", classification);
        Assert.True(score == 3, "all three evidence channels (dust, conservation, independence)");
    }
}
