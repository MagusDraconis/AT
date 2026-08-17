using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 27 — TRM/TQM observable bridge. Compares three light-propagation prescriptions for the SAME ρ
/// (GR null geodesics / TRM effective propagation / TQM geometry) via the temporal fraction t. Classify:
/// SAME EFFECT / PARTIAL EFFECT / NO EFFECT.
///
/// Tests: TQMQG270 (index & deflection), TQMQG271 (delay & magnification), TQMQG272 (three-way classification).
/// </summary>
public class TQMQG_Phase27_TRMObservableBridgeTests : ResearchTestBase
{
    public TQMQG_Phase27_TRMObservableBridgeTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG270: effective index & deflection ────────────────────────────────────────

    [Fact]
    public void TQMQG270_IndexAndDeflection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG270: effective index & deflection vs temporal fraction");

        double phi = 0.5;   // weak-field potential (natural units)
        double gm = 1.0;    // GM/(b c²)

        double tTqm = TRMObservableBridge.TqmGeometryFraction();
        double tTrm = TRMObservableBridge.TrmEffectiveFraction();

        double nTqm = TRMObservableBridge.EffectiveIndex(phi, tTqm);
        double nTrm = TRMObservableBridge.EffectiveIndex(phi, tTrm);
        double dTqm = TRMObservableBridge.Deflection(gm, tTqm);
        double dTrm = TRMObservableBridge.Deflection(gm, tTrm);
        double dGr = 4.0 * gm;   // Einstein deflection

        sb.AppendLine($"effective index   TQM (t=0): n = {nTqm:F4}   TRM (t=1): n = {nTrm:F4} = e^Φ");
        sb.AppendLine($"deflection        TQM (t=0): α = {dTqm:F4}   TRM (t=1): α = {dTrm:F4}   GR: α = {dGr:F4}");

        bool tqmNoEffect = dTqm == 0.0 && Math.Abs(nTqm - 1.0) < 1e-12;
        bool trmMatchesGr = dTrm == dGr;

        sb.AppendLine();
        sb.AppendLine($"TQM geometry (t=0): index = 1, zero deflection (NO EFFECT): {tqmNoEffect}");
        sb.AppendLine($"TRM effective (t=1): index = e^Φ, deflection equals GR (SAME EFFECT): {trmMatchesGr}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the SAME ρ gives n=1 under TQM's full conformal metric (the conformal factor cancels),");
        sb.AppendLine("but n=e^Φ under TRM's temporal-only effective medium. The difference is the optics, not the density.");
        Output.WriteLine(sb.ToString());

        Assert.True(tqmNoEffect, "TQM geometry should give index 1 and zero deflection");
        Assert.True(trmMatchesGr, "TRM effective propagation should reproduce GR deflection");
    }

    // ── TQMQG271: time delay & magnification ──────────────────────────────────────────

    [Fact]
    public void TQMQG271_DelayAndMagnification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG271: Shapiro delay & magnification vs temporal fraction");

        double gmLog = 1.0;   // GM/c³ · ln(...)
        double sigma = 0.3;   // surface density in units of Σ_crit

        double tTqm = TRMObservableBridge.TqmGeometryFraction();
        double tTrm = TRMObservableBridge.TrmEffectiveFraction();

        double dtTqm = TRMObservableBridge.ShapiroDelay(gmLog, tTqm);
        double dtTrm = TRMObservableBridge.ShapiroDelay(gmLog, tTrm);
        double dtGr = 2.0 * gmLog;

        double kTqm = TRMObservableBridge.Convergence(sigma, tTqm);
        double kTrm = TRMObservableBridge.Convergence(sigma, tTrm);
        double muTqm = TRMObservableBridge.Magnification(kTqm, 0.0);
        double muTrm = TRMObservableBridge.Magnification(kTrm, 0.0);
        double muGr = TRMObservableBridge.Magnification(sigma, 0.0);

        sb.AppendLine($"Shapiro delay  TQM: {dtTqm:F4}   TRM: {dtTrm:F4}   GR: {dtGr:F4}  (units 2GM/c³·ln)");
        sb.AppendLine($"magnification  TQM: {muTqm:F4}   TRM: {muTrm:F4}   GR: {muGr:F4}  (κ=0.3)");

        bool tqmNoEffect = dtTqm == 0.0 && Math.Abs(muTqm - 1.0) < 1e-12;
        bool trmMatchesGr = dtTrm == dtGr && muTrm == muGr;

        sb.AppendLine();
        sb.AppendLine($"TQM geometry (t=0): zero delay, μ=1 (NO EFFECT): {tqmNoEffect}");
        sb.AppendLine($"TRM effective (t=1): delay and magnification equal GR (SAME EFFECT): {trmMatchesGr}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the temporal-only optics (t=1) reproduces GR's delay and focusing exactly; the full conformal");
        sb.AppendLine("metric (t=0) cancels them. Lensing lives in the choice of optics, not in the presence of tensor curvature.");
        Output.WriteLine(sb.ToString());

        Assert.True(tqmNoEffect, "TQM geometry should give zero delay and μ=1");
        Assert.True(trmMatchesGr, "TRM effective propagation should reproduce GR delay and magnification");
    }

    // ── TQMQG272: three-way classification ─────────────────────────────────────────────

    [Fact]
    public void TQMQG272_ThreeWayClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG272: GR / TRM / TQM geometry — which effect class?");

        double gm = 1.0, gmLog = 1.0, sigma = 0.3;
        double tTqm = TRMObservableBridge.TqmGeometryFraction();
        double tTrm = TRMObservableBridge.TrmEffectiveFraction();

        double dGr = TRMObservableBridge.Deflection(gm, 1.0);
        double dTrm = TRMObservableBridge.Deflection(gm, tTrm);
        double dTqm = TRMObservableBridge.Deflection(gm, tTqm);

        sb.AppendLine("three-way comparison (same ρ, potential Φ = (1/d)ln ρ):");
        sb.AppendLine($"  GR null geodesics      deflection = {dGr:F4}   (reference)");
        sb.AppendLine($"  TRM effective medium   deflection = {dTrm:F4}   (t=1, temporal-only optics)");
        sb.AppendLine($"  TQM geometry           deflection = {dTqm:F4}   (t=0, full conformal metric)");
        sb.AppendLine();

        string trmClass = dTrm == dGr ? "SAME EFFECT" : (dTrm > 0 ? "PARTIAL EFFECT" : "NO EFFECT");
        string tqmClass = dTqm == 0 ? "NO EFFECT" : (dTqm == dGr ? "SAME EFFECT" : "PARTIAL EFFECT");

        sb.AppendLine($"  TRM effective propagation vs GR : {trmClass}");
        sb.AppendLine($"  TQM geometry vs GR              : {tqmClass}");
        sb.AppendLine();
        sb.AppendLine("BRIDGE: TQM's ρ CAN generate full GR lensing (SAME EFFECT) — but only under TRM's temporal-only optics (t=1),");
        sb.AppendLine("which ignores the spatial g_ii. TQM's OWN metric (t=0) cancels the conformal factor and gives NO EFFECT. The");
        sb.AppendLine("lensing discrepancy is therefore a choice of LIGHT-PROPAGATION PRESCRIPTION (null geodesic vs effective medium),");
        sb.AppendLine("not the tensor sector. TQM keeps NO EFFECT; TRM's lensing is real but sits outside TQM's null-geodesic optics.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("SAME EFFECT", trmClass);
        Assert.Equal("NO EFFECT", tqmClass);
        Assert.Equal(0.0, dTqm);
        Assert.Equal(dGr, dTrm);
    }
}
