using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 27 — TRM/AT observable bridge. Compares three light-propagation prescriptions for the SAME ρ
/// (GR null geodesics / TRM effective propagation / AT geometry) via the temporal fraction t. Classify:
/// SAME EFFECT / PARTIAL EFFECT / NO EFFECT.
///
/// Tests: ATQG270 (index & deflection), ATQG271 (delay & magnification), ATQG272 (three-way classification).
/// </summary>
public class ATQG_Phase27_TRMObservableBridgeTests : ResearchTestBase
{
    public ATQG_Phase27_TRMObservableBridgeTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG270: effective index & deflection ────────────────────────────────────────

    [Fact]
    public void ATQG270_IndexAndDeflection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG270: effective index & deflection vs temporal fraction");

        double phi = 0.5;   // weak-field potential (natural units)
        double gm = 1.0;    // GM/(b c²)

        double tAt = TRMObservableBridge.AtGeometryFraction();
        double tTrm = TRMObservableBridge.TrmEffectiveFraction();

        double nAt = TRMObservableBridge.EffectiveIndex(phi, tAt);
        double nTrm = TRMObservableBridge.EffectiveIndex(phi, tTrm);
        double dAt = TRMObservableBridge.Deflection(gm, tAt);
        double dTrm = TRMObservableBridge.Deflection(gm, tTrm);
        double dGr = 4.0 * gm;   // Einstein deflection

        sb.AppendLine($"effective index   AT (t=0): n = {nAt:F4}   TRM (t=1): n = {nTrm:F4} = e^Φ");
        sb.AppendLine($"deflection        AT (t=0): α = {dAt:F4}   TRM (t=1): α = {dTrm:F4}   GR: α = {dGr:F4}");

        bool atNoEffect = dAt == 0.0 && Math.Abs(nAt - 1.0) < 1e-12;
        bool trmMatchesGr = dTrm == dGr;

        sb.AppendLine();
        sb.AppendLine($"AT geometry (t=0): index = 1, zero deflection (NO EFFECT): {atNoEffect}");
        sb.AppendLine($"TRM effective (t=1): index = e^Φ, deflection equals GR (SAME EFFECT): {trmMatchesGr}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the SAME ρ gives n=1 under AT's full conformal metric (the conformal factor cancels),");
        sb.AppendLine("but n=e^Φ under TRM's temporal-only effective medium. The difference is the optics, not the density.");
        Output.WriteLine(sb.ToString());

        Assert.True(atNoEffect, "AT geometry should give index 1 and zero deflection");
        Assert.True(trmMatchesGr, "TRM effective propagation should reproduce GR deflection");
    }

    // ── ATQG271: time delay & magnification ──────────────────────────────────────────

    [Fact]
    public void ATQG271_DelayAndMagnification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG271: Shapiro delay & magnification vs temporal fraction");

        double gmLog = 1.0;   // GM/c³ · ln(...)
        double sigma = 0.3;   // surface density in units of Σ_crit

        double tAt = TRMObservableBridge.AtGeometryFraction();
        double tTrm = TRMObservableBridge.TrmEffectiveFraction();

        double dtAt = TRMObservableBridge.ShapiroDelay(gmLog, tAt);
        double dtTrm = TRMObservableBridge.ShapiroDelay(gmLog, tTrm);
        double dtGr = 2.0 * gmLog;

        double kAt = TRMObservableBridge.Convergence(sigma, tAt);
        double kTrm = TRMObservableBridge.Convergence(sigma, tTrm);
        double muAt = TRMObservableBridge.Magnification(kAt, 0.0);
        double muTrm = TRMObservableBridge.Magnification(kTrm, 0.0);
        double muGr = TRMObservableBridge.Magnification(sigma, 0.0);

        sb.AppendLine($"Shapiro delay  AT: {dtAt:F4}   TRM: {dtTrm:F4}   GR: {dtGr:F4}  (units 2GM/c³·ln)");
        sb.AppendLine($"magnification  AT: {muAt:F4}   TRM: {muTrm:F4}   GR: {muGr:F4}  (κ=0.3)");

        bool atNoEffect = dtAt == 0.0 && Math.Abs(muAt - 1.0) < 1e-12;
        bool trmMatchesGr = dtTrm == dtGr && muTrm == muGr;

        sb.AppendLine();
        sb.AppendLine($"AT geometry (t=0): zero delay, μ=1 (NO EFFECT): {atNoEffect}");
        sb.AppendLine($"TRM effective (t=1): delay and magnification equal GR (SAME EFFECT): {trmMatchesGr}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the temporal-only optics (t=1) reproduces GR's delay and focusing exactly; the full conformal");
        sb.AppendLine("metric (t=0) cancels them. Lensing lives in the choice of optics, not in the presence of tensor curvature.");
        Output.WriteLine(sb.ToString());

        Assert.True(atNoEffect, "AT geometry should give zero delay and μ=1");
        Assert.True(trmMatchesGr, "TRM effective propagation should reproduce GR delay and magnification");
    }

    // ── ATQG272: three-way classification ─────────────────────────────────────────────

    [Fact]
    public void ATQG272_ThreeWayClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG272: GR / TRM / AT geometry — which effect class?");

        double gm = 1.0, gmLog = 1.0, sigma = 0.3;
        double tAt = TRMObservableBridge.AtGeometryFraction();
        double tTrm = TRMObservableBridge.TrmEffectiveFraction();

        double dGr = TRMObservableBridge.Deflection(gm, 1.0);
        double dTrm = TRMObservableBridge.Deflection(gm, tTrm);
        double dAt = TRMObservableBridge.Deflection(gm, tAt);

        sb.AppendLine("three-way comparison (same ρ, potential Φ = (1/d)ln ρ):");
        sb.AppendLine($"  GR null geodesics      deflection = {dGr:F4}   (reference)");
        sb.AppendLine($"  TRM effective medium   deflection = {dTrm:F4}   (t=1, temporal-only optics)");
        sb.AppendLine($"  AT geometry           deflection = {dAt:F4}   (t=0, full conformal metric)");
        sb.AppendLine();

        string trmClass = dTrm == dGr ? "SAME EFFECT" : (dTrm > 0 ? "PARTIAL EFFECT" : "NO EFFECT");
        string atClass = dAt == 0 ? "NO EFFECT" : (dAt == dGr ? "SAME EFFECT" : "PARTIAL EFFECT");

        sb.AppendLine($"  TRM effective propagation vs GR : {trmClass}");
        sb.AppendLine($"  AT geometry vs GR              : {atClass}");
        sb.AppendLine();
        sb.AppendLine("BRIDGE: AT's ρ CAN generate full GR lensing (SAME EFFECT) — but only under TRM's temporal-only optics (t=1),");
        sb.AppendLine("which ignores the spatial g_ii. AT's OWN metric (t=0) cancels the conformal factor and gives NO EFFECT. The");
        sb.AppendLine("lensing discrepancy is therefore a choice of LIGHT-PROPAGATION PRESCRIPTION (null geodesic vs effective medium),");
        sb.AppendLine("not the tensor sector. AT keeps NO EFFECT; TRM's lensing is real but sits outside AT's null-geodesic optics.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("SAME EFFECT", trmClass);
        Assert.Equal("NO EFFECT", atClass);
        Assert.Equal(0.0, dAt);
        Assert.Equal(dGr, dTrm);
    }
}
