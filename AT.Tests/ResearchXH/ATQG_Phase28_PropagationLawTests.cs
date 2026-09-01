using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 28 — derive the light-propagation law. Tests which rule (null geodesics vs TRM effective medium)
/// follows from actualization dynamics. Classify: DERIVED / PREFERRED / IMPORTED.
///
/// Tests: ATQG280 (index & conformal invariance), ATQG281 (mechanism census), ATQG282 (classification).
/// </summary>
public class ATQG_Phase28_PropagationLawTests : ResearchTestBase
{
    public ATQG_Phase28_PropagationLawTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG280: effective index & conformal invariance ──────────────────────────────

    [Fact]
    public void ATQG280_IndexAndConformalInvariance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG280: effective index — null geodesic (n=1) vs TRM (n=ρ^(1/d))");

        int d = 3;
        double rhoOver = 1.1;   // overdensity
        double rhoVac = 1.0;

        double nNullOver = PropagationLaw.NullGeodesicIndex();
        double nNullVac = PropagationLaw.NullGeodesicIndex();
        double nTrmOver = PropagationLaw.TrmEffectiveIndex(d, rhoOver);
        double nTrmVac = PropagationLaw.TrmEffectiveIndex(d, rhoVac);

        sb.AppendLine($"null geodesic index  n = {nNullOver:F6} (overdensity)  n = {nNullVac:F6} (vacuum)  — independent of ρ");
        sb.AppendLine($"TRM effective index  n = {nTrmOver:F6} (overdensity)  n = {nTrmVac:F6} (vacuum)  — varies with ρ");

        bool nullNoLensing = !PropagationLaw.ProducesLensing(nNullOver, nNullVac);
        bool trmLensing = PropagationLaw.ProducesLensing(nTrmOver, nTrmVac);

        sb.AppendLine();
        sb.AppendLine($"null geodesics produce lensing: {!nullNoLensing}  (index constant → no refraction)");
        sb.AppendLine($"TRM index produces lensing:     {trmLensing}  (index varies → refraction)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the conformal factor ρ^(2/d) multiplies g_00 and g_ii equally, so the null index is exactly 1");
        sb.AppendLine("and light is conformally invariant. Only the temporal-only TRM index n = ρ^(1/d) refracts light.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1.0, nNullOver);
        Assert.True(nullNoLensing, "null geodesics should not lens");
        Assert.True(trmLensing, "TRM index should refract");
    }

    // ── ATQG281: mechanism census — which mechanisms are native? ─────────────────────

    [Fact]
    public void ATQG281_MechanismCensus()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG281: do the five mechanisms yield n=1 (native) or n=e^Φ (imported)?");

        int d = 3;
        double rho = 1.1;
        string[] mechanisms =
        {
            "event-to-event", "branching-path", "correlation-kernel",
            "effective-refractive-index", "null-geodesic-limit",
        };

        int native = 0, imported = 0;
        foreach (var m in mechanisms)
        {
            bool isNative = PropagationLaw.IsNative(m);
            bool isImported = PropagationLaw.IsImported(m);
            double index = isNative ? PropagationLaw.NullGeodesicIndex()
                                   : PropagationLaw.TrmEffectiveIndex(d, rho);
            string status = isNative ? "NATIVE (n=1)" : (isImported ? "IMPORTED (n=e^Φ)" : "?");
            sb.AppendLine($"{m,-26} -> {status}   n = {index:F6}");
            if (isNative) native++;
            if (isImported) imported++;
        }

        sb.AppendLine();
        sb.AppendLine($"native mechanisms (n=1, no lensing):   {native}");
        sb.AppendLine($"imported mechanism (n=e^Φ, lensing):    {imported}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: actualization dynamics supplies ONLY the causal order (→ conformal class → null geodesics, n=1).");
        sb.AppendLine("Branching and correlations give ρ — the conformal factor — which cannot refract light. The refractive index");
        sb.AppendLine("n = e^Φ is an additional temporal-only assumption, not derivable from the actualization primitives.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(4, native);
        Assert.Equal(1, imported);
    }

    // ── ATQG282: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG282_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG282: null geodesics vs TRM propagation — DERIVED, PREFERRED, or IMPORTED?");

        sb.AppendLine("NULL GEODESICS: DERIVED.");
        sb.AppendLine("  • The causal order fixes the CONFORMAL CLASS (the light cone). The counting measure ρ supplies only the");
        sb.AppendLine("    conformal factor ρ^(2/d), which is a conformal rescaling — it leaves the light cone invariant.");
        sb.AppendLine("  • Therefore light propagates along the causal-order light cone: null geodesics, n = 1, no lensing.");
        sb.AppendLine();
        sb.AppendLine("TRM EFFECTIVE MEDIUM: IMPORTED.");
        sb.AppendLine("  • n = e^Φ = ρ^(1/d) requires treating the temporal rate (g_00) ALONE as a refractive medium, ignoring the");
        sb.AppendLine("    spatial g_ii. This is not in AT's primitives — it is the ψ ≠ 0 (non-conformal) sector in disguise");
        sb.AppendLine("    (n = e^(−ψd/(d−1)) for the ψ-perturbed metric), i.e. the very new primitive QG23/QG24 identified.");
        sb.AppendLine();
        sb.AppendLine("FINAL: null geodesics are the NATIVE (DERIVED) propagation law; TRM's lensing kernel is IMPORTED.");
        Output.WriteLine(sb.ToString());

        Assert.True(PropagationLaw.IsNative("null-geodesic-limit"));
        Assert.True(PropagationLaw.IsImported("effective-refractive-index"));
        Assert.Equal(1.0, PropagationLaw.NullGeodesicIndex());
    }
}
