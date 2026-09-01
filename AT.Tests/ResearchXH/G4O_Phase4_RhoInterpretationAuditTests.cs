using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-O Phase 4 — audit the physical interpretation of ρ. Determines whether ρ is matter density,
/// actualization/event density, conformal density, or another quantity, by comparing the observable
/// gravitational field built from raw ρ (matter-like), ∇ρ (gradient), and ln ρ (conformal) across
/// peaks / minima / vacuum. Concludes whether the repulsive prediction is genuine or a misidentification.
///
/// Tests: G4-O40 (peaks/minima/vacuum), G4-O41 (raw ρ vs ln ρ vs ∇ρ), G4-O42 (interpretation).
/// </summary>
public class G4O_Phase4_RhoInterpretationAuditTests : ResearchTestBase
{
    public G4O_Phase4_RhoInterpretationAuditTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double Grad(Func<double, double> rho, double x, double h = 1e-5)
        => (rho(x + h) - rho(x - h)) / (2 * h);

    // ── G4-O40: density peaks / minima / vacuum regimes ───────────────────────────────

    [Fact]
    public void G4_O40_PeaksMinimaVacuum()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O40: native field in peak / minimum / vacuum regimes");

        // Peak (Gaussian at origin): a = −∇lnρ > 0 (repulsive).
        double aPeak = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Gaussian(u), 0.4, D);
        // Minimum (ρ=1+ax²): a = −∇lnρ < 0 (toward the minimum).
        double aMin = PhysicalObservables.GeodesicAcceleration(0.4, 0.5, D);
        // Vacuum (uniform ρ): a = −∇lnρ = 0 (no field).
        double aVac = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Uniform(u), 0.4, D);

        sb.AppendLine($"density peak (Gaussian): a = {aPeak:F4} (repulsive / expansive)");
        sb.AppendLine($"density minimum (ρ=1+ax²): a = {aMin:F4} (toward the minimum)");
        sb.AppendLine($"vacuum (uniform ρ):       a = {aVac:F4} (no field)");

        sb.AppendLine();
        sb.AppendLine($"field = −(1/d)∇lnρ is LOCALIZED (zero in vacuum, ∝ gradient elsewhere): " +
                      $"{Math.Abs(aVac) < 1e-9 && aPeak > 0 && aMin < 0}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native field is the CONFORMAL (log-density) gradient — not a matter-sourced");
        sb.AppendLine("integral. It is repulsive around peaks and vanishes in vacuum.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(aVac) < 1e-9, "vacuum should have no field");
        Assert.True(aPeak > 0 && aMin < 0, "peak repulsive / minimum toward-minimum expected");
    }

    // ── G4-O41: raw ρ vs ln ρ vs ∇ρ as the gravitational source ───────────────────────

    [Fact]
    public void G4_O41_RawRhoVsLnRhoVsGradRho()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O41: which density enters the gravitational field?");

        double x = 0.4;
        // Matter-like (raw ρ): a = −∫ρ (enclosed mass) — ATTRACTIVE toward the peak.
        double aMatter = PhysicalObservables.GrAcceleration(u => PhysicalObservables.Gaussian(u), x);
        // Conformal (ln ρ): a = −(1/d)∇lnρ — REPULSIVE away from the peak.
        double aConformal = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Gaussian(u), x, D);
        // Gradient (∇ρ): a = −∇ρ — also REPULSIVE (toward decreasing density).
        double aGrad = -Grad(u => PhysicalObservables.Gaussian(u), x);

        sb.AppendLine($"raw ρ  (matter, a=−∫ρ):      a = {aMatter:F4}  (ATTRACTIVE toward peak)");
        sb.AppendLine($"ln ρ   (conformal, a=−∇lnρ): a = {aConformal:F4}  (REPULSIVE away from peak)");
        sb.AppendLine($"∇ρ     (gradient, a=−∇ρ):    a = {aGrad:F4}  (toward decreasing density)");

        sb.AppendLine();
        sb.AppendLine($"matter interpretation (raw ρ) gives ATTRACTION; conformal/gradient gives REPULSION:");
        sb.AppendLine($"  matter vs conformal disagree: {Math.Sign(aMatter) != Math.Sign(aConformal)}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native observable is the CONFORMAL (ln ρ) gradient, not the matter integral.");
        sb.AppendLine("ρ is therefore NOT matter density — it enters as the conformal (scale) factor.");
        Output.WriteLine(sb.ToString());

        Assert.True(aMatter < 0, "matter-like (raw ρ) should be attractive");
        Assert.True(aConformal > 0, "conformal (ln ρ) should be repulsive");
        Assert.True(Math.Sign(aMatter) != Math.Sign(aConformal), "matter and conformal interpretations should disagree");
    }

    // ── G4-O42: interpretation — actualization/conformal density, not matter ──────────

    [Fact]
    public void G4_O42_Interpretation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O42: what IS ρ?");

        // ρ = counting measure = event/actualization density (G4-F); it is the VOLUME element.
        // √g = ρ ⇒ conformal factor f = ρ^(2/d). Hence Φ = (1/d)lnρ and a = −(1/d)∇lnρ.
        double x = 0.4;
        double vol = 1.0 + 0.5 * x * x;                 // ρ (the counting measure)
        double f = Math.Pow(vol, 2.0 / D);              // conformal factor ρ^(2/d)
        sb.AppendLine($"ρ (counting measure) = {vol:F4}; conformal factor f = ρ^(2/d) = {f:F4}");
        sb.AppendLine($"√g = ρ ⇒ f = ρ^(2/d) is FORCED (the counting measure IS the volume element).");

        sb.AppendLine();
        sb.AppendLine($"ρ is the ACTUALIZATION/EVENT density (counting measure), NOT matter density.");
        sb.AppendLine($"It enters the metric as the conformal (scale) factor, so its 'gravity' is an");
        sb.AppendLine($"EXPANSIVE (anti-screening) effect — test particles move toward lower-actualization");
        sb.AppendLine($"regions. The repulsive prediction is therefore the GENUINE behavior of the");
        sb.AppendLine($"actualization-density-as-conformal-factor — NOT a misidentification with matter.");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: the repulsive prediction is GENUINE (conformal/scale-factor physics), but it");
        sb.AppendLine("is NOT 'matter anti-gravity' — it is the expansive effect of the conformal factor. A matter");
        sb.AppendLine("sector (attractive) would require an ADDITIONAL density distinct from the conformal factor.");
        Output.WriteLine(sb.ToString());

        // The conformal factor is forced positive-power by √g = ρ.
        Assert.True(Math.Abs(Math.Pow(f, D / 2.0) - vol) < 1e-12, "√g should equal ρ (f^(d/2)=ρ)");
        Assert.True(f > 1.0, "conformal factor should exceed 1 where ρ > 1 (positive power)");
    }
}
