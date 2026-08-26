using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 103 — Mercury perihelion revalidation. Computes the observed 42.98 "/century from first principles
/// and compares the scalar-only conformal sector (γ=−1) against the unified (ρ+ψ, spin-2) sector.
/// Classify: MATCH / PARTIAL / FAIL.
///
/// Tests: ATQG1030 (GR baseline + PPN factor machinery), ATQG1031 (conformal FAIL vs unified MATCH),
/// ATQG1032 (classification).
/// </summary>
public class ATQG_Phase103_MercuryRevalidationTests : ResearchTestBase
{
    public ATQG_Phase103_MercuryRevalidationTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1030: weak-field metric → GR baseline & PPN factor ────────────────────

    [Fact]
    public void ATQG1030_GrBaseline()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1030: GR baseline perihelion (42.98 \"/century)");

        double gm = MercuryRevalidation.SolarGravitationalParameter();
        double perOrbit = MercuryRevalidation.PerihelionPerOrbit(
            gm, MercuryRevalidation.MercurySemiMajorAxis, MercuryRevalidation.MercuryEccentricity);
        double orbits = MercuryRevalidation.OrbitsPerCentury(MercuryRevalidation.MercuryPeriodDays);

        double factor = MercuryRevalidation.PpnPerihelionFactor(
            MercuryRevalidation.GrGamma(), MercuryRevalidation.GrBeta());
        double gr = MercuryRevalidation.GrPerihelionArcsecPerCentury();

        sb.AppendLine($"GM_sun               = {gm:E8} m^3 s^-2");
        sb.AppendLine($"perihelion/orbit     = {perOrbit:E6} rad");
        sb.AppendLine($"orbits/century       = {orbits:F3}");
        sb.AppendLine($"PPN factor (γ=β=1)   = {factor:F6}");
        sb.AppendLine($"GR perihelion        = {gr:F4} arcsec/century");
        sb.AppendLine($"observed             = {MercuryRevalidation.ObservedPerihelion()} arcsec/century");
        sb.AppendLine($"relative error       = {MercuryRevalidation.RelativeError(gr):E4}");

        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the GR (massless spin-2) baseline reproduces the observed 42.98 \"/century to within numerical");
        sb.AppendLine("precision (factor = 1). This is the target the unified network must match.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(1.0, factor, 6);
        Assert.Equal(42.98, gr, 1);   // within 0.05 "/century
        Assert.True(Math.Abs(MercuryRevalidation.RelativeError(gr)) < 1e-3, "GR should match observation");
    }

    // ── ATQG1031: conformal (ρ-only) FAIL vs unified (ρ+ψ) MATCH ──────────────────

    [Fact]
    public void ATQG1031_ConformalVsUnified()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1031: scalar-only conformal vs unified (ρ+ψ) network");

        double gc = MercuryRevalidation.ConformalGamma();
        double bc = MercuryRevalidation.ConformalBeta();
        double factorC = MercuryRevalidation.PpnPerihelionFactor(gc, bc);
        double conf = MercuryRevalidation.PerihelionFor(gc, bc);

        double gs = MercuryRevalidation.Spin2Gamma();
        double bs = MercuryRevalidation.Spin2Beta();
        double factorS = MercuryRevalidation.PpnPerihelionFactor(gs, bs);
        double uni = MercuryRevalidation.PerihelionFor(gs, bs);

        sb.AppendLine($"conformal (ρ-only): γ={gc:+0.0;-0.0}, β={bc:+0.0;-0.0} → factor {factorC:+0.0000;-0.0000} → {conf:+0.00;-0.00} \"/century");
        sb.AppendLine($"unified (ρ+ψ)     : γ={gs:+0.0;-0.0}, β={bs:+0.0;-0.0} → factor {factorS:+0.0000;-0.0000} → {uni:+0.00;-0.00} \"/century");
        sb.AppendLine($"observed                            → +42.98 \"/century");

        bool conformalFails = conf < 0.0;                    // retrograde = wrong sign
        bool unifiedMatches = MercuryRevalidation.UnifiedMatchesObserved();
        bool conformalMatches = MercuryRevalidation.ConformalMatchesObserved();

        sb.AppendLine();
        sb.AppendLine($"conformal sector is RETROGRADE (wrong sign): {conformalFails}");
        sb.AppendLine($"conformal sector matches observation: {conformalMatches}");
        sb.AppendLine($"unified network matches observation: {unifiedMatches}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the ρ-only conformal sector gives γ=−1 → factor −1/3 → a RETROGRADE advance of −14.33 \"/century");
        sb.AppendLine("(wrong sign and magnitude). The ψ (spin-2) sector restores γ=β=+1 → factor 1 → +42.98 \"/century. Mercury");
        sb.AppendLine("perihelion is therefore recovered ONLY through the tensor ψ, confirming it as the graviton sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(conformalFails, "conformal perihelion should be retrograde");
        Assert.False(conformalMatches, "conformal sector must not match observation");
        Assert.True(unifiedMatches, "unified network should match observation");
        Assert.Equal(-1.0 / 3.0, factorC, 6);
        Assert.Equal(1.0, factorS, 6);
    }

    // ── ATQG1032: classification ──────────────────────────────────────────────────

    [Fact]
    public void ATQG1032_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1032: MATCH / PARTIAL / FAIL?");

        double conf = MercuryRevalidation.PerihelionFor(
            MercuryRevalidation.ConformalGamma(), MercuryRevalidation.ConformalBeta());
        double uni = MercuryRevalidation.PerihelionFor(
            MercuryRevalidation.Spin2Gamma(), MercuryRevalidation.Spin2Beta());

        sb.AppendLine($"scalar-only conformal sector: {conf:+0.00;-0.00} \"/century  (FAIL — retrograde)");
        sb.AppendLine($"unified (ρ+ψ) network      : {uni:+0.00;-0.00} \"/century  (MATCH)");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {MercuryRevalidation.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • FAIL   (ρ alone): conformal γ=−1 gives a retrograde advance −14.33 \"/century.");
        sb.AppendLine("  • MATCH  (ρ+ψ):    the ψ spin-2 graviton (Fierz-Pauli, QG44) restores γ=β=+1 → +42.98 \"/century.");
        sb.AppendLine("  • PARTIAL would apply only if the value were close but not exact; the spin-2 sector reproduces it exactly.");
        sb.AppendLine();
        sb.AppendLine("So the unified network RECOVERS Mercury's perihelion through the ψ (spin-2) sector — a MATCH, with the");
        sb.AppendLine("scalar-only conformal sector as the (known) failure mode.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("MATCH", MercuryRevalidation.Classify());
        Assert.True(MercuryRevalidation.UnifiedMatchesObserved());
        Assert.False(MercuryRevalidation.ConformalMatchesObserved());
    }
}
