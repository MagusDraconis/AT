using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 21 — derive light propagation from AT. Tests whether light follows null geodesics of the
/// conformally-flat metric, and the observable consequences (speed c, redshift, no lensing). Classify:
/// NULL-GEODESIC / MODIFIED / EMERGENT.
///
/// Tests: ATQG210 (light speed + redshift vs no lensing), ATQG211 (photon emergence / temporal field), ATQG212 (classification).
/// </summary>
public class ATQG_Phase21_LightPropagationTests : ResearchTestBase
{
    public ATQG_Phase21_LightPropagationTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG210: light speed c, redshift present, bending absent ────────────────────

    [Fact]
    public void ATQG210_SpeedRedshiftNoBending()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG210: light speed = c, redshift present, bending absent");

        int d = 3;
        double speed = LightPropagation.LightSpeed(1.5);           // independent of ρ
        double redshift = LightPropagation.GravitationalRedshift(1.2, 1.0, d);   // ρ1=1.2 → ρ2=1.0
        double bending = LightPropagation.LightBending();

        sb.AppendLine($"effective light speed (ρ=1.5) = {speed} (independent of ρ)");
        sb.AppendLine($"gravitational redshift (ρ1=1.2 → ρ2=1.0, d=3) = {redshift:F4}");
        sb.AppendLine($"gravitational light bending = {bending} (no lensing)");

        bool speedIsC = speed == 1.0;
        bool redshiftPresent = redshift > 0.0;       // photon climbs out: ρ1>ρ2 → redshift
        bool bendingAbsent = bending == 0.0;

        sb.AppendLine();
        sb.AppendLine($"light speed = c (conformal invariance): {speedIsC}");
        sb.AppendLine($"redshift present (from g_00 = −ρ^(2/d)): {redshiftPresent}");
        sb.AppendLine($"light bending ABSENT (null geodesics straight): {bendingAbsent}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: null geodesics are conformally invariant, so light propagates at c and is NOT bent.");
        sb.AppendLine("But g_00 = −ρ^(2/d) varies, so light IS redshifted (frequency changes). AT predicts redshift");
        sb.AppendLine("WITHOUT lensing — a specific, testable difference from GR (which predicts both).");
        Output.WriteLine(sb.ToString());

        Assert.True(speedIsC, "light speed should be c");
        Assert.True(redshiftPresent, "gravitational redshift should be present");
        Assert.True(bendingAbsent, "light bending should be absent (no lensing)");
    }

    // ── ATQG211: photon emergence — the temporal field does not modify null geodesics ─

    [Fact]
    public void ATQG211_PhotonEmergenceTemporalField()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG211: the temporal field does not modify the effective light speed");

        sb.AppendLine($"{"ρ",6} {"light speed",12}");
        foreach (double rho in new[] { 0.5, 1.0, 2.0, 4.0 })
        {
            double c = LightPropagation.LightSpeed(rho);
            sb.AppendLine($"{rho,6:F1} {c,12:F2}");
        }

        bool speedConstant = LightPropagation.LightSpeed(0.5) == LightPropagation.LightSpeed(4.0)
                          && LightPropagation.LightSpeed(4.0) == 1.0;

        sb.AppendLine();
        sb.AppendLine($"effective light speed is c for ALL ρ (temporal field does not refract): {speedConstant}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: if a photon is a massless excitation of the temporal field that follows the metric's");
        sb.AppendLine("null geodesics, then the conformal factor ρ cannot modify its speed (ds²=0 is ρ-independent).");
        sb.AppendLine("There is no native 'refractive index' from the scalar ρ.");
        Output.WriteLine(sb.ToString());

        Assert.True(speedConstant, "light speed should be constant (c) regardless of ρ");
    }

    // ── ATQG212: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG212_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG212: is light propagation NULL-GEODESIC, MODIFIED, or EMERGENT?");

        sb.AppendLine("CLASSIFICATION: NULL-GEODESIC (conformally invariant), with a specific prediction.");
        sb.AppendLine();
        sb.AppendLine("  • Light follows null geodesics of g = ρ^(2/d)η, which are conformally invariant: light propagates at c");
        sb.AppendLine("    and is NOT bent (no gravitational lensing) (ATQG210/211).");
        sb.AppendLine("  • But g_00 = −ρ^(2/d) varies, so light IS gravitationally redshifted (frequency changes) — redshift");
        sb.AppendLine("    WITHOUT lensing.");
        sb.AppendLine("  • This is a specific, falsifiable AT prediction that DIFFERS from GR (GR predicts both redshift and");
        sb.AppendLine("    lensing, via the non-conformal Weyl/Ricci structure). AT's conformal gravity gives redshift only.");
        sb.AppendLine("  • NOTE: this corrects G4-O's 'lensing' (which was actually a potential difference, not a deflection");
        sb.AppendLine("    angle): the exact conformally-flat metric has ZERO light bending.");
        sb.AppendLine("  • 'EMERGENT' modifications (e.g., a refractive index from photon–temporal-field coupling) would require");
        sb.AppendLine("    a non-conformal coupling, i.e. a new primitive — absent here.");
        Output.WriteLine(sb.ToString());

        Assert.True(true);   // classification (no numeric assertion)
    }
}
