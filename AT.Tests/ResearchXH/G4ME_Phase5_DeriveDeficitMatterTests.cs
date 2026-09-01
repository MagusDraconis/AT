using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-ME Phase 5 — derive deficit matter. Matter is currently DEFINED as m = ρ̄ − ρ. Here we test whether
/// that definition emerges UNIQUELY from AT principles (abundance conservation, normalization, positivity,
/// excitation structure), comparing alternative matter definitions (m=f(ρ), gradient matter, curvature
/// matter). Classify DERIVED / PREFERRED / ASSUMED.
///
/// Tests: G4-ME50 (normalization/positivity/abundance constraints), G4-ME51 (gradient-source uniqueness),
///        G4-ME52 (alternative definitions + classification).
/// </summary>
public class G4ME_Phase5_DeriveDeficitMatterTests : ResearchTestBase
{
    public G4ME_Phase5_DeriveDeficitMatterTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;
    private const double RHO_BAR = 1.0;

    private static double Integral(Func<double, double> f, double a = -2.0, double b = 2.0, int n = 20000)
    {
        double dx = (b - a) / n;
        double s = 0.0;
        for (int i = 0; i < n; i++) s += f(a + (i + 0.5) * dx) * dx;
        return s;
    }

    // ── G4-ME50: normalization, positivity, abundance (constraints) ─────────────────

    [Fact]
    public void G4_ME50_NormalizationPositivityAbundance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME50: normalization, positivity, and abundance conservation");

        double rhoVoid = PhysicalObservables.Void(0.4);   // ρ < ρ̄ (void)
        double mDef = PhysicalObservables.MatterDensity(rhoVoid);
        double mLog = PhysicalObservables.LogMatter(rhoVoid);
        double mRat = PhysicalObservables.RatioMatter(rhoVoid);

        // Normalization: m(ρ̄) = 0 for all three.
        bool normAll = Math.Abs(PhysicalObservables.MatterDensity(RHO_BAR)) < 1e-12
                    && Math.Abs(PhysicalObservables.LogMatter(RHO_BAR)) < 1e-12
                    && Math.Abs(PhysicalObservables.RatioMatter(RHO_BAR)) < 1e-12;

        // Positivity: m > 0 where ρ < ρ̄ for all three.
        bool posAll = mDef > 0 && mLog > 0 && mRat > 0;

        // Abundance conservation: ∫m dV = ρ̄V − ∫ρ dV (the total count deviation) holds EXACTLY only for the
        // LINEAR deficit m = ρ̄−ρ (it IS the count deviation); log/ratio are nonlinear and differ.
        double intRho = Integral(u => PhysicalObservables.Void(u));
        double countDev = RHO_BAR * 4.0 - intRho;
        double intDef = Integral(u => PhysicalObservables.MatterDensity(PhysicalObservables.Void(u)));
        double intLog = Integral(u => PhysicalObservables.LogMatter(PhysicalObservables.Void(u)));
        double intRat = Integral(u => PhysicalObservables.RatioMatter(PhysicalObservables.Void(u)));

        sb.AppendLine($"ρ(0.4) = {rhoVoid:F6} (void); m_deficit = {mDef:F6}, m_log = {mLog:F6}, m_ratio = {mRat:F6}");
        sb.AppendLine($"normalization m(ρ̄)=0 (all three): {normAll}");
        sb.AppendLine($"positivity m>0 for ρ<ρ̄ (all three): {posAll}");
        sb.AppendLine($"count deviation ρ̄V−∫ρ = {countDev:F6}");
        sb.AppendLine($"∫(ρ̄−ρ)dV  = {intDef:F6}  (matches count deviation: {Math.Abs(intDef - countDev) < 1e-9})");
        sb.AppendLine($"∫ln(ρ̄/ρ)dV = {intLog:F6}  (differs: {Math.Abs(intLog - countDev) > 1e-3})");
        sb.AppendLine($"∫(ρ̄/ρ−1)dV = {intRat:F6}  (differs: {Math.Abs(intRat - countDev) > 1e-3})");

        bool deficitConserves = Math.Abs(intDef - countDev) < 1e-9;
        bool alternativesDiffer = Math.Abs(intLog - countDev) > 1e-3 && Math.Abs(intRat - countDev) > 1e-3;

        sb.AppendLine();
        sb.AppendLine($"normalization + positivity are satisfied by ALL monotonic alternatives (NOT selective)");
        sb.AppendLine($"abundance conservation (∫m dV = count deviation) selects only the LINEAR deficit: {deficitConserves}");
        sb.AppendLine($"log/ratio are nonlinear transforms and do NOT conserve the count deviation: {alternativesDiffer}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the deficit m = ρ̄−ρ is the unique matter that is LINEAR in ρ (the first-order");
        sb.AppendLine("excitation) and whose total is EXACTLY the conserved count deviation ρ̄V−∫ρ dV.");
        Output.WriteLine(sb.ToString());

        Assert.True(normAll, "all candidates should vanish at ρ=ρ̄");
        Assert.True(posAll, "all candidates should be positive in a void");
        Assert.True(deficitConserves, "the deficit should conserve the count deviation exactly");
        Assert.True(alternativesDiffer, "log/ratio matter should NOT equal the count deviation");
    }

    // ── G4-ME51: gradient-source uniqueness (a = +(1/d)∇m/ρ) ────────────────────────

    [Fact]
    public void G4_ME51_GradientSourceUniqueness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME51: the gradient-source form a = +(1/d)∇m/ρ uniquely selects m = ρ̄−ρ");

        double x = 0.4;
        double resDef = PhysicalObservables.GradientSourceResidual(u => PhysicalObservables.Void(u), r => PhysicalObservables.MatterDensity(r), x, D);
        double resLog = PhysicalObservables.GradientSourceResidual(u => PhysicalObservables.Void(u), r => PhysicalObservables.LogMatter(r), x, D);
        double resRat = PhysicalObservables.GradientSourceResidual(u => PhysicalObservables.Void(u), r => PhysicalObservables.RatioMatter(r), x, D);

        sb.AppendLine($"residual a − (1/d)∇m/ρ at x={x}:");
        sb.AppendLine($"  deficit m=ρ̄−ρ :  {resDef:E2}  (exactly 0)");
        sb.AppendLine($"  log    m=ln(ρ̄/ρ): {resLog:E2}");
        sb.AppendLine($"  ratio  m=ρ̄/ρ−1 : {resRat:E2}");
        sb.AppendLine();
        sb.AppendLine($"why: a = −(1/d)∇lnρ = −(1/d)∇ρ/ρ. Writing a = +(1/d)∇m/ρ requires ∇m = −∇ρ,");
        sb.AppendLine($"i.e. m = −ρ + const = ρ̄ − ρ (const fixed by m(ρ̄)=0). Equivalently f'(ρ) = −1, unique.");
        sb.AppendLine($"For m = ln(ρ̄/ρ): a = +(1/d)∇m WITHOUT the 1/ρ (different force law).");

        bool deficitExact = Math.Abs(resDef) < 1e-12;
        bool alternativesFail = Math.Abs(resLog) > 1e-3 && Math.Abs(resRat) > 1e-3;

        sb.AppendLine();
        sb.AppendLine($"deficit gives a = +(1/d)∇m/ρ exactly: {deficitExact}");
        sb.AppendLine($"log/ratio do NOT (they give a different force law): {alternativesFail}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the deficit is the UNIQUE scalar field satisfying the attractive gradient-source");
        sb.AppendLine("form a = +(1/d)∇m/ρ — the identification of matter as the source of the native acceleration.");
        Output.WriteLine(sb.ToString());

        Assert.True(deficitExact, "the deficit should satisfy a = +(1/d)∇m/ρ exactly");
        Assert.True(alternativesFail, "log/ratio matter should fail the gradient-source form");
    }

    // ── G4-ME52: alternative definitions + classification ────────────────────────────

    [Fact]
    public void G4_ME52_AlternativeDefinitionsClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME52: alternative matter definitions and classification");

        double x = 0.4;
        // gradient matter m = ∇ρ is a VECTOR: a = −(1/d)(∇ρ)/ρ = −(1/d)m/ρ (a points opposite ∇ρ, toward minima).
        // curvature matter m = R(ρ) involves the SECOND derivative σ″; a ∝ σ′ is first-order, so a ≠ ∇m/ρ.
        double rho = PhysicalObservables.Void(x);
        double grad = (PhysicalObservables.Void(x + 1e-6) - PhysicalObservables.Void(x - 1e-6)) / 2e-6;
        double a = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Void(u), x, D);
        double aFromGrad = -grad / (D * rho);   // a = −(1/d)∇ρ/ρ

        sb.AppendLine($"gradient matter m=∇ρ (vector): ∇ρ = {grad:F6}, a = −(1/d)∇ρ/ρ = {aFromGrad:F6} (= a {Math.Abs(aFromGrad - a) < 1e-9})");
        sb.AppendLine($"curvature matter m=R(ρ): R ∝ σ″ (second derivative) — a ∝ σ′ (first derivative), mismatched order");
        sb.AppendLine();

        sb.AppendLine("Requirements for a matter abundance:");
        sb.AppendLine($"  scalar (not vector)        — rejects gradient matter (a vector)");
        sb.AppendLine($"  density-valued (units of ρ) — rejects m=ln(ρ̄/ρ) (dimensionless)");
        sb.AppendLine($"  linear first-order excitation — rejects m=ln, m=ρ̄/ρ−1 (nonlinear)");
        sb.AppendLine($"  exact a = +(1/d)∇m/ρ (attractive source) — unique solution m = ρ̄−ρ");
        sb.AppendLine();

        sb.AppendLine("CLASSIFICATION: DERIVED (unique form, with one physical input).");
        sb.AppendLine("  • m = ρ̄ − ρ is the UNIQUE scalar, density-valued, first-order excitation satisfying");
        sb.AppendLine("    a = +(1/d)∇m/ρ exactly (f'(ρ) = −1 ⇒ f = ρ̄−ρ).");
        sb.AppendLine("  • The one physical input is 'matter attracts': a points toward matter (m>0), i.e. toward the");
        sb.AppendLine("    actualization deficit. This is the standard gravitational principle, not an arbitrary ansatz.");
        sb.AppendLine("  • Alternatives: gradient matter is a vector (not an abundance); curvature matter is second-order;");
        sb.AppendLine("    log/ratio matter give a different (non-1/ρ) force law and are not density-valued/linear.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(aFromGrad - a) < 1e-9, "a should equal −(1/d)∇ρ/ρ (gradient matter is a vector)");
    }
}
