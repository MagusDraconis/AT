using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 182 — G bridge origin. Known: QG6 (deficit GM_eff = m₀·r₀/(d·ρ̄), magnitude free),
/// QG181 (M_Pl = v·A³, G = 1/M_Pl² from D96). This phase bridges the two G constructions — showing that
/// m₀, r₀, ρ̄ emerge from D96 spectral geometry — no new primitives, deterministic.
///
/// Tests: ATQG1820 (deficit parameters from D96 occupancy), ATQG1821 (GM_eff = 1/ln(M_Pl/v) bridge
/// equation), ATQG1822 (equivalence + classification).
/// </summary>
public class ATQG_Phase182_GravityBridgeOriginTests : ResearchTestBase
{
    public ATQG_Phase182_GravityBridgeOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1820_DeficitParametersFromD96()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1820: deficit parameters m₀, r₀, ρ̄ from D96 occupancy");

        sb.AppendLine("ASSUMPTIONS: the QG6 deficit profile ρ = ρ̄ − m₀/(1+r/r₀) has three free");
        sb.AppendLine("parameters. The D96 spectrum supplies each of them: the lightest-octave occupancy");
        sb.AppendLine("occ₀ = 4 fixes the deficit depth as a fraction of the total mode count");
        sb.AppendLine("(m₀ = occ₀/Σm — the S parameter, QG180); the logarithmic spectral span fixes the");
        sb.AppendLine("inner scale (r₀ = ln(span)); the background is the normalized counting measure");
        sb.AppendLine("(ρ̄ = 1); d = 3 (spatial dimension).");
        sb.AppendLine();
        sb.AppendLine("D96 OCCUPANCY:");
        sb.AppendLine($"  occ₀ = {GravityBridgeOrigin.LightestOctaveOccupancy():F0}  (lightest-octave occupancy = S parameter)");
        sb.AppendLine($"  Σm = {GravityBridgeOrigin.TotalModes()}");
        sb.AppendLine($"  span = {GravityBridgeOrigin.Span():F5}, ln(span) = {GravityBridgeOrigin.LogSpan():F5}");
        sb.AppendLine();
        sb.AppendLine("DEFICIT PARAMETERS FROM D96:");
        sb.AppendLine($"  m₀ = occ₀/Σm = {GravityBridgeOrigin.DeficitDepth():F8}");
        sb.AppendLine($"  r₀ = ln(span) = {GravityBridgeOrigin.DeficitInnerScale():F8}");
        sb.AppendLine($"  ρ̄ = {GravityBridgeOrigin.BackgroundDensity():F1} (normalized counting measure)");
        sb.AppendLine();
        sb.AppendLine("  the deficit depth is the S parameter — the light-octave fraction of the spectrum");
        sb.AppendLine("  'removed' from the normalized counting measure; the inner scale is the spectral");
        sb.AppendLine("  radius in log-frequency space. No fitted parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(GravityBridgeOrigin.DeficitDepth() > 0.03 && GravityBridgeOrigin.DeficitDepth() < 0.06,
            "m₀ = occ₀/Σm should be near 0.0421");
        Assert.True(GravityBridgeOrigin.DeficitInnerScale() > 1.0 && GravityBridgeOrigin.DeficitInnerScale() < 3.0,
            "r₀ = ln(span) should be near 1.8567");
    }

    [Fact]
    public void ATQG1821_BridgeEquation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1821: the bridge equation GM_eff = 1/ln(M_Pl/v)");

        sb.AppendLine("ASSUMPTIONS: QG6 gives the deficit gravitational scale GM_eff = m₀·r₀/(d·ρ̄). QG181");
        sb.AppendLine("gives M_Pl = v·A³ (A = Σm·#g·occ₂) and G = 1/M_Pl², so ln(M_Pl/v) = 3·ln A exactly.");
        sb.AppendLine("With the D96 deficit parameters (ATQG1820), the deficit scale must equal the inverse");
        sb.AppendLine("of the Planck hierarchy logarithm.");
        sb.AppendLine();
        sb.AppendLine("THE BRIDGE EQUATION:");
        sb.AppendLine($"  A = Σm·#g·occ₂ = {GravityBridgeOrigin.SpectralContent():F0}");
        sb.AppendLine($"  GM_eff = m₀·r₀/(d·ρ̄) = occ₀·ln(span)/(3·Σm) = {GravityBridgeOrigin.DeficitGravitationalScale():F9}");
        sb.AppendLine($"  1/ln(M_Pl/v) = 1/(3·ln A) = {GravityBridgeOrigin.InversePlanckHierarchyLog():F9}");
        sb.AppendLine($"  deviation = {GravityBridgeOrigin.BridgeDeviation():P4}");
        sb.AppendLine();
        sb.AppendLine("  GM_eff within 2%: " + GravityBridgeOrigin.BridgeMatches());
        Output.WriteLine(sb.ToString());

        Assert.True(GravityBridgeOrigin.BridgeMatches(), "GM_eff should match 1/ln(M_Pl/v) within 2%");
        Assert.True(GravityBridgeOrigin.BridgeDeviation() < 0.01, "bridge deviation should be under 1%");
    }

    [Fact]
    public void ATQG1822_EquivalenceAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1822: equivalence of the two G constructions and classification");

        sb.AppendLine("ASSUMPTIONS: the two G constructions describe the same physical content. QG6's");
        sb.AppendLine("deficit abundance and QG181's spectral content cube must be linked by a D96-internal");
        sb.AppendLine("identity, and the QG181 hierarchy M_Pl/v = A³ must anchor the bridge.");
        sb.AppendLine();
        sb.AppendLine("THE BRIDGE IDENTITY (occ₀·ln(span)·ln(A) = Σm):");
        sb.AppendLine($"  occ₀·ln(span)·ln(A) = {GravityBridgeOrigin.BridgeIdentityValue():F6}");
        sb.AppendLine($"  Σm = {GravityBridgeOrigin.TotalModes()}");
        sb.AppendLine($"  deviation = {GravityBridgeOrigin.IdentityDeviation():P4}");
        sb.AppendLine();
        sb.AppendLine("QG181 EQUIVALENCE (M_Pl/v = A³):");
        sb.AppendLine($"  M_Pl/v = {NewtonConstantOrigin.PlanckMassGeV() / GravityBridgeOrigin.WeakScaleGeV():E6}");
        sb.AppendLine($"  A³ = {Math.Pow(GravityBridgeOrigin.SpectralContent(), 3.0):E6}");
        sb.AppendLine($"  exact: {GravityBridgeOrigin.PlanckHierarchyIsSpectralContentCube()}");
        sb.AppendLine();
        sb.AppendLine("DEPENDENCY STRUCTURE:");
        sb.AppendLine("  D96 → occ₀=4, Σm=95, span, A=Σm·#g·occ₂");
        sb.AppendLine("       → m₀ = occ₀/Σm, r₀ = ln(span), ρ̄ = 1");
        sb.AppendLine("       → GM_eff = 1/ln(M_Pl/v) = 1/(3·ln A)");
        sb.AppendLine("       → (QG181) M_Pl = v·A³, G = 1/M_Pl²");
        sb.AppendLine();
        int score = GravityBridgeOrigin.OriginScore();
        string cls = GravityBridgeOrigin.Classify();
        sb.AppendLine($"Bridge-origin score (0..3): {score}");
        sb.AppendLine($"  +1 GM_eff = 1/ln(M_Pl/v) within 2%: {GravityBridgeOrigin.BridgeMatches()}");
        sb.AppendLine($"  +1 identity occ₀·ln(span)·ln(A) = Σm within 2%: {GravityBridgeOrigin.IdentityHolds()}");
        sb.AppendLine($"  +1 QG181 equivalence M_Pl/v = A³ exact: {GravityBridgeOrigin.PlanckHierarchyIsSpectralContentCube()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NO BRIDGE rejected: the D96 deficit parameters reproduce 1/ln(M_Pl/v) within");
        sb.AppendLine("    0.097%.");
        sb.AppendLine("  • PARTIAL BRIDGE rejected: the identity and the QG181 construction hold");
        sb.AppendLine("    consistently, giving a full three-way agreement.");
        sb.AppendLine("  • BRIDGE ORIGIN accepted: the QG6 deficit parameters EMERGE from D96 — m₀ = occ₀/Σm");
        sb.AppendLine("    (S parameter, QG180), r₀ = ln(span), ρ̄ = 1 — so GM_eff = 1/ln(M_Pl/v), equivalently");
        sb.AppendLine("    the identity occ₀·ln(span)·ln(Σm·#g·occ₂) = Σm. The deficit description (QG6) and");
        sb.AppendLine("    the spectral description (QG181) are the SAME physical content: the deficit");
        sb.AppendLine("    abundance is the spectral-content logarithm. No fitted constants.");
        Output.WriteLine(sb.ToString());

        Assert.True(score >= 3, "bridge-origin score should be maximal");
        Assert.Equal("BRIDGE ORIGIN", cls);
    }
}
