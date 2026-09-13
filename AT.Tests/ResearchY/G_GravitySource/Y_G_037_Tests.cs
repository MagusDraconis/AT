using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_037 — Refractive Lens Audit (group G — Gravity Source).
///
/// QUESTION (from the TRM-era programme). Can light deflection be obtained WITHOUT bending space, by giving the
/// vacuum an effective refractive index and using c_eff = c₀/n_eff? The TRM slide states
/// `n_eff = 2 + λ_time·φ + λ_space·φ²·|μ̇|` with `Δθ_TRM = Δθ_GR·f(κ,b)` and `{β,γ} → {1,1}`, and reports a
/// deflection matching GR.
///
/// ANSWER: **REFUTED for a static index — by an identity, not an argument.**
///
///  (1) THE INDEX **IS** THE PPN γ: for a static metric, n = e^(B−A) gives **n − 1 = −(1+γ)Φ/c²**, so the
///      coefficient a in `n = 1 + a·GM/(c²r)` is exactly **a = 1 + γ**, and the deflection is **Δθ = 2a·GM/(c²b)**.
///      A static, non-dispersive index is therefore not an alternative to space curvature — it is the same
///      object in different variables.
///  (2) AT's **derived** conformal sector (G_031) has A = B ⟹ n = 1 ⟹ **zero** bending. This is G_032's
///      Cassini refutation restated optically, and a conformal factor maps null geodesics to null geodesics.
///  (3) THE RATE TERM CANNOT CARRY AN INDEPENDENT SPATIAL TERM. The needed n − 1 = 2φ is FIRST order;
///      `λ_space·φ²|μ̇|` is second: **9.43e5× too small at the Sun**, 2.0e4× at a white dwarf, 8.1× at
///      J0740+6620.
///  (4) THE EM-ONLY READING IS EXCLUDED. If the mechanism bends light but not space, gravitons do not see it:
///      over GW170817's 40 Mpc a galactic-scale index delays light by ~131 yr against a **1.7 s** bound —
///      **2.4e9×**. The surviving strength is n − 1 ≲ **4.13e−16**.
///  (5) BENDING AND DELAY ARE NOT INDEPENDENT — any static index reproducing the bending also reproduces the
///      Shapiro delay, from the same function.
///  (6) THE THREE GENUINE ESCAPES are dispersion, birefringence and time dependence — each an observational
///      commitment, not a free choice. The last is exactly the TRM φ²|μ̇| term.
///  (7) THE SOURCE CHECK — AND A CORRECTION. Reading `TRM.Core/Shared/PhotonTransportModel.cs` *in situ*
///      falsified this audit's first reading and strengthened the verdict: `λ_time = 1` does **NOT** give half,
///      because the code accelerates by `ar = −n_eff·G·M/(r·r)` and `n_eff` includes the leading `2.0`. That
///      literal 2 **is** (1+γ) = 2, i.e. γ = 1 — the spatial factor is already in the formula, hardcoded. The
///      file also uses both conventions at once (`(n_eff − 2)·v` for travel time, `n_eff·G·M/r²` for the
///      acceleration), its own tests accept γ ∈ [0.40, 1.50] (**23,913× looser than Cassini**) and never ran at
///      solar compactness, and the rate channel needs |μ̇| = 1.57e4 against a physical ≤ 0.43/s.
/// </summary>
public class Y_G_037_Tests : ResearchTestBase
{
    public Y_G_037_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_037_TheIndexIsThePPNGamma()
    {
        // THE IDENTITY: n - 1 = -(1+gamma)*Phi/c^2  =>  the coefficient a in n = 1 + a*GM/(c^2 r) is a = 1+gamma.
        Assert.Equal(0.0, RefractiveLensAudit.IndexCoefficient(-1.0));   // conformal
        Assert.Equal(1.0, RefractiveLensAudit.IndexCoefficient(0.0));    // time-only
        Assert.Equal(2.0, RefractiveLensAudit.IndexCoefficient(1.0));    // GR
        // For a BOUND object Φ < 0, so n − 1 = −(1+γ)Φ/c² is POSITIVE: the vacuum slows light (n > 1).
        Assert.Equal(4.0e-6, RefractiveLensAudit.IndexMinusOneFromGamma(-2.0e-6, 1.0), 12);
        Assert.Equal(2.0e-6, RefractiveLensAudit.IndexMinusOneFromGamma(-2.0e-6, 0.0), 12);
        Assert.Equal(0.0, RefractiveLensAudit.IndexMinusOneFromGamma(-2.0e-6, -1.0), 12);

        // The deflection from an index n = 1 + a*GM/(c^2 r) is 2a*GM/(c^2 b) — closed form checked NUMERICALLY.
        Assert.Equal(4.0, RefractiveLensAudit.DeflectionNumeric(2.0), 3);
        Assert.Equal(2.0, RefractiveLensAudit.DeflectionNumeric(1.0), 3);
        Assert.Equal(0.0, RefractiveLensAudit.DeflectionNumeric(0.0), 6);
        Assert.Equal(RefractiveLensAudit.DeflectionNumeric(2.0),
                     RefractiveLensAudit.DeflectionFromIndex(2.0, 1.0, 1.0), 3);

        // The three physical cases: AT's DERIVED sector gives zero; time-only gives exactly half; GR gives all.
        var cases = RefractiveLensAudit.TheThreeCases();
        Assert.Equal(3, cases.Length);
        Assert.Equal((0.0, 0.0), (cases[0].IndexCoefficient, cases[0].DeflectionInGrUnits));
        Assert.Equal((1.0, 0.5), (cases[1].IndexCoefficient, cases[1].DeflectionInGrUnits));
        Assert.Equal((2.0, 1.0), (cases[2].IndexCoefficient, cases[2].DeflectionInGrUnits));

        // A conformal metric cannot bend light at all: A = B  =>  n = e^(B-A) = 1. This IS G_032's refutation
        // (Cassini, 8.6957e4 sigma) expressed optically.
        Assert.True(RefractiveLensAudit.ConformalCannotBend(-1.0, -1.0));
        Assert.False(RefractiveLensAudit.ConformalCannotBend(-1.0, 1.0));
    }

    [Fact]
    public void Y_G_037_TheRateDependentTermCannotSupplyAnIndependentSpatialTerm()
    {
        // The needed index shift is FIRST order; the phi^2 term is second order and orders of magnitude short.
        var rows = RefractiveLensAudit.QuadraticReach();
        Assert.Equal(3, rows.Length);

        var solar = rows[0];
        Assert.Equal(4.24e-6, solar.Needed, 9);
        Assert.Equal(4.4944e-12, solar.Quadratic, 15);
        Assert.Equal(1.0, solar.Shortfall / 9.43e5, 3);     // ~6 orders too small

        Assert.True(rows[1].Shortfall > 1e4);               // white dwarf: still hopeless
        Assert.True(rows[2].Shortfall < 10.0);              // neutron star: within an order
        Assert.True(rows[2].Shortfall > 5.0);

        // Monotonicity: the shorter the field, the less hopeless — but never first order.
        Assert.True(rows[0].Shortfall > rows[1].Shortfall);
        Assert.True(rows[1].Shortfall > rows[2].Shortfall);

        // And the first-order carrier is the formula's LEADING CONSTANT (2), not lambda_time — which by the
        // identity means that constant IS the spatial metric function.
        Assert.Contains("LEADING CONSTANT (2)", RefractiveLensAudit.FirstOrderCarrier());
        Assert.Contains("γ = 1", RefractiveLensAudit.FirstOrderCarrier());
    }

    [Fact]
    public void Y_G_037_AnEmOnlyMediumIsExcludedByGw170817()
    {
        // If the index is a MEDIUM rather than a metric, light and gravitons do not share it.
        var rows = RefractiveLensAudit.EmOnlyKill();
        Assert.Equal(4, rows.Length);

        // A galactic-scale index would delay light by ~131 years against a 1.7 s bound.
        var galactic = rows[0];
        Assert.Equal(1.0, galactic.DelaySeconds / 4.1171e9, 4);
        Assert.Equal(130.5, galactic.DelaySeconds / 3.156e7, 0);   // years
        Assert.True(galactic.OverBound > 2.0e9);

        // The solar-scale index is worse still; even 1e-12 fails by thousands.
        Assert.True(rows[1].OverBound > galactic.OverBound);
        Assert.True(rows[3].OverBound > 1e3);

        // The largest strength that survives the bound is ~4e-16 — nothing like the 2*phi a deflection needs.
        double survives = RefractiveLensAudit.EmOnlySurvivingStrength();
        Assert.Equal(1.0, survives / 4.13e-16, 2);
        Assert.True(survives < 1e-15);
        Assert.True(RefractiveLensAudit.NeededIndexMinusOne(RefractiveLensAudit.SolarX) / survives > 1e9);
    }

    [Fact]
    public void Y_G_037_BendingAndDelayComeFromOneFunction()
    {
        // A static index that bends light also delays it — the two are not independently tunable.
        Assert.Contains("Shapiro", RefractiveLensAudit.ShapiroIsNotOptional());
        Assert.Contains("one function", RefractiveLensAudit.ShapiroIsNotOptional());

        // The escapes are exactly three, and each carries a constraint rather than a free choice.
        var escapes = RefractiveLensAudit.EscapeRoutes();
        Assert.Equal(3, escapes.Length);
        Assert.Contains(escapes, e => e.Escape.Contains("dispersion"));
        Assert.Contains(escapes, e => e.Escape.Contains("birefringence"));
        Assert.Contains(escapes, e => e.Escape.Contains("time dependence"));
        Assert.All(escapes, e => Assert.True(e.Constraint.Length > 20, $"{e.Escape} must state a constraint"));
        // The rate-dependent escape is the TRM term itself.
        Assert.Contains(escapes, e => e.Constraint.Contains("compact-object"));
    }

    [Fact]
    public void Y_G_037_VerdictIsRefuted()
    {
        Assert.Equal("REFUTED", RefractiveLensAudit.Verdict());

        string where = RefractiveLensAudit.WhereItStands();
        Assert.Contains("n − 1 = −(1+γ)Φ/c²", where);
        Assert.Contains("same object in different variables", where);
        Assert.Contains("9.4e5", where);
        Assert.Contains("2.4e9", where);

        // What SURVIVES is stated, not discarded: the rate-dependent term and the optical language.
        Assert.Contains("compact-object", where);
        Assert.Contains("optical language", where);
    }

    [Fact]
    public void Y_G_037_TheSourceCheckFalsifiesTheHalfReading()
    {
        // ── (7) THE SOURCE CHECK. The code accelerates by ar = -n_eff*G*M/(r*r), and n_eff INCLUDES the
        //        leading 2.0 — so the first-order coefficient is n_eff itself, NOT lambda_time.
        Assert.Equal(2.0, RefractiveLensAudit.TrmLeadingConstant);
        Assert.Equal(2.00000212, RefractiveLensAudit.TrmEffectiveIndexAtSolar(), 8);
        Assert.Equal(1.000001, RefractiveLensAudit.TrmEffectiveIndexAtSolar() / 2.0, 6);   // FULL, not half

        // By the identity a = 1 + gamma, a leading constant of 2 means gamma = 1: the spatial factor is
        // already in the formula, hardcoded.
        Assert.Equal(1.0, RefractiveLensAudit.GammaImpliedByLeadingConstant());
        Assert.Equal(2.0, RefractiveLensAudit.IndexCoefficient(RefractiveLensAudit.GammaImpliedByLeadingConstant()));

        // The other convention the same function uses: travel time subtracts the 2, which IS half.
        Assert.Equal(2.12e-6, RefractiveLensAudit.TrmPhysicalIndexShiftAtSolar(), 12);
        Assert.Equal(1.06e-6, RefractiveLensAudit.TrmPhysicalIndexShiftAtSolar() / 2.0, 12);   // half

        // ── TRM's own deflection tests: what they actually accept.
        var bands = RefractiveLensAudit.TrmTestBands();
        Assert.Equal(3, bands.Length);
        Assert.Equal(0.40, bands[0].GammaLow, 10);      // EL03 low  = 2(0.70) - 1
        Assert.Equal(1.50, bands[0].GammaHigh, 10);     // EL03 high = 2(1.25) - 1
        Assert.Equal(1.10, bands[0].GammaHigh - bands[0].GammaLow, 10);
        Assert.Equal(1.50, bands[2].GammaHigh, 10);     // EL04 Euler high

        // Cassini pins gamma to 1 +/- 2.3e-5; TRM's widest band is 23,913x looser.
        Assert.Equal(1.1 / (2.0 * RefractiveLensAudit.CassiniSigmaGamma), RefractiveLensAudit.BandLooseness(), 3);
        Assert.True(RefractiveLensAudit.BandLooseness() > 2.3e4);

        // And every run is G = 1, c = 1, b = 1 at eps = 1e-3..1e-2 — never the solar regime.
        Assert.Equal(471.7, RefractiveLensAudit.TestRegimeOutsideSolarLow(), 1);
        Assert.Equal(4717.0, RefractiveLensAudit.TestRegimeOutsideSolarHigh(), 0);
        Assert.True(RefractiveLensAudit.TestRegimeOutsideSolarLow() > 400.0);

        // ── The one non-metric channel: |mu_dot| needed vs |mu_dot| available.
        Assert.Equal(1.0 / (30.0 * RefractiveLensAudit.SolarX), RefractiveLensAudit.RequiredMuDot(RefractiveLensAudit.SolarX), 6);
        Assert.True(RefractiveLensAudit.RequiredMuDot(RefractiveLensAudit.SolarX) > 1.5e4);
        Assert.True(RefractiveLensAudit.MuDotFromRadialSweep() < 1.0);
        Assert.True(RefractiveLensAudit.MuDotFromAcceleration() < 1.0e-5);
        Assert.True(RefractiveLensAudit.RateTermShortfallAtSolar() > 3.6e4);
    }

    [Fact]
    public void Y_G_037_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_037 — Refractive Lens Audit: can light bend without space bending?");

        sb.AppendLine("QUESTION (TRM-era). Give the vacuum an effective index n_eff and set c_eff = c₀/n_eff —");
        sb.AppendLine("does that produce the observed deflection WITHOUT a spatial metric, releasing AT from the");
        sb.AppendLine("non-conformal postulate of G_029? The TRM slide states:");
        sb.AppendLine("    n_eff = 2 + λ_time·φ + λ_space·φ²·|μ̇|,   Δθ_TRM = Δθ_GR·f(κ,b),   {β,γ} → {1,1}");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The index is STATIC and NON-DISPERSIVE unless an escape route is explicitly invoked.");
        sb.AppendLine("  2. φ is the dimensionless potential Φ/c² (the natural reading for a weak-field PPN statement);");
        sb.AppendLine("     if TRM's φ is an order-1 order parameter instead, the O(φ²) conclusion changes and the");
        sb.AppendLine("     specific f(κ,b) would need the TRM definitions.");
        sb.AppendLine("  3. Deflection is computed to first order in the potential (the PPN regime).");
        sb.AppendLine();

        PrintHeader("1. THE IDENTITY — THE INDEX **IS** THE PPN γ");
        sb.AppendLine("  For a static metric g₀₀ = −e^(2A), g_ij = e^(2B)δ_ij, light sees n = e^(B−A).");
        sb.AppendLine("  With A = Φ/c² and B = −γΦ/c²:");
        sb.AppendLine("        n − 1  =  −(1+γ)Φ/c²        ⇒   a = 1 + γ  in  n = 1 + a·GM/(c²r)");
        sb.AppendLine("        Δθ     =  2a·GM/(c²b)       =   ((1+γ)/2)·4GM/(c²b)");
        sb.AppendLine();
        sb.AppendLine("  a static, non-dispersive index is NOT an alternative to space curvature —");
        sb.AppendLine("  it is the SAME OBJECT in different variables.");
        sb.AppendLine();
        sb.AppendLine("  rule                                                 γ      n−1 coeff    deflection");
        foreach (var c in RefractiveLensAudit.TheThreeCases())
            sb.AppendLine($"  {c.Rule,-50} {c.Gamma,+4:F0}   {c.IndexCoefficient,+9:F0}x   "
                        + $"{c.DeflectionInGrUnits * 4:F2}GM/(c²b)");
        sb.AppendLine();
        sb.AppendLine($"  numerical check of the integral: a=2 → {RefractiveLensAudit.DeflectionNumeric(2.0):F6} (analytic 4), "
                      + $"a=1 → {RefractiveLensAudit.DeflectionNumeric(1.0):F6} (analytic 2)");
        sb.AppendLine("  ⇒ AT's DERIVED conformal sector has A = B ⟹ n = 1 ⟹ ZERO bending. This is G_032's");
        sb.AppendLine("    Cassini refutation (8.6957e4 σ) restated optically: a conformal factor maps null");
        sb.AppendLine("    geodesics to null geodesics, so it cannot bend light at all.");
        sb.AppendLine();

        PrintHeader("2. THE RATE-DEPENDENT TERM CANNOT CARRY AN INDEPENDENT SPATIAL TERM");
        sb.AppendLine("  the observed deflection needs n − 1 = 2φ — FIRST order in φ;");
        sb.AppendLine("  λ_space·φ²·|μ̇| is SECOND order:");
        sb.AppendLine();
        sb.AppendLine("  arena                                   φ      needed n−1    φ² (max)     shortfall");
        foreach (var r in RefractiveLensAudit.QuadraticReach())
            sb.AppendLine($"  {r.Arena,-34}{r.Phi,11:E3}{r.Needed,14:E3}{r.Quadratic,13:E3}{r.Shortfall,13:E3}");
        sb.AppendLine();
        sb.AppendLine("  ⇒ the first-order coefficient is the formula's LEADING CONSTANT (2), not λ_time — and by");
        sb.AppendLine("    the identity a = 1+γ that constant IS the spatial metric function. The φ² term is");
        sb.AppendLine("    nevertheless genuinely interesting: it depends on a RATE, which no static metric can,");
        sb.AppendLine("    so it is a real non-metric ingredient — but only where φ is O(1), at compact objects.");
        sb.AppendLine();

        PrintHeader("3. THE DICHOTOMY — AND ITS OBSERVATIONAL KILL");
        sb.AppendLine("  If the index is a MEDIUM rather than a metric, it affects light but NOT gravitational waves:");
        sb.AppendLine($"  over GW170817/GRB170817A's {RefractiveLensAudit.Gw170817DistanceMpc:F0} Mpc "
                      + $"({RefractiveLensAudit.Gw170817DistanceMpc * RefractiveLensAudit.Mpc / RefractiveLensAudit.C / 3.156e7:F0} yr light-travel),");
        sb.AppendLine("  an EM-only index delays light against gravitons by:");
        sb.AppendLine();
        sb.AppendLine("  strength (n−1)                differential delay            vs the 1.7 s bound");
        foreach (var r in RefractiveLensAudit.EmOnlyKill())
            sb.AppendLine($"  {r.Strength + " / " + r.IndexMinusOne.ToString("E0"),-28}"
                        + $"{r.DelaySeconds,18:E4} s   ({r.DelaySeconds / 3.156e7,10:F1} yr)"
                        + $"{r.OverBound,14:E3}x");
        sb.AppendLine();
        sb.AppendLine($"  ⇒ the surviving strength is n − 1 ≲ {RefractiveLensAudit.EmOnlySurvivingStrength():E3}");
        sb.AppendLine("    — nothing like the 2φ a deflection requires. EM-only is excluded by ~10⁹.");
        sb.AppendLine();

        PrintHeader("4. BENDING AND DELAY ARE NOT INDEPENDENTLY TUNABLE");
        sb.AppendLine("  " + RefractiveLensAudit.ShapiroIsNotOptional());
        sb.AppendLine();

        PrintHeader("5. THE THREE GENUINE ESCAPES — AND ONLY THESE");
        foreach (var e in RefractiveLensAudit.EscapeRoutes())
        {
            sb.AppendLine($"  · {e.Escape}");
            sb.AppendLine($"      why not a metric: {e.Why}");
            sb.AppendLine($"      constraint:       {e.Constraint}");
        }
        sb.AppendLine();

        PrintHeader("6. THE SOURCE CHECK — THE FORMULA ALREADY CONTAINS THE SPATIAL FACTOR");
        sb.AppendLine("  TRM.Core/Shared/PhotonTransportModel.cs was still on disk, so the formula was read");
        sb.AppendLine("  in situ rather than inferred:");
        sb.AppendLine("      ar = −n_eff·G·M/(r·r),    n_eff = 2 + λ_time·φ + λ_space·φ²·|μ̇|");
        sb.AppendLine("  The force multiplier is n_eff ITSELF — it includes the leading 2.0. At the solar limb:");
        sb.AppendLine($"      n_eff = {RefractiveLensAudit.TrmEffectiveIndexAtSolar():F9}"
                      + $"   ->   ratio vs GR = {RefractiveLensAudit.TrmEffectiveIndexAtSolar() / 2.0:F9}");
        sb.AppendLine("  ⇒ λ_time = 1 gives FULL deflection, NOT half. This audit's first reading treated λ_time");
        sb.AppendLine("    as the index coefficient; the code uses the whole n_eff. CORRECTED.");
        sb.AppendLine();
        sb.AppendLine("  AND THE FILE USES BOTH CONVENTIONS AT ONCE:");
        sb.AppendLine($"      travel time : (n_eff − 2)·v = {RefractiveLensAudit.TrmPhysicalIndexShiftAtSolar():E3}"
                      + "   -> HALF");
        sb.AppendLine("      acceleration:  n_eff·G·M/r²                     -> FULL");
        sb.AppendLine("  one function, two conventions; the reported result depends on which line is read.");
        sb.AppendLine();
        sb.AppendLine("  SO THE FULL DEFLECTION COMES FROM A LITERAL 2.0 — and by the identity that IS γ = 1:");
        sb.AppendLine($"      leading constant {RefractiveLensAudit.TrmLeadingConstant:F1}"
                      + $"   ->   gamma = {RefractiveLensAudit.GammaImpliedByLeadingConstant():F1}");
        sb.AppendLine("  the formula is therefore not an alternative to space curvature — the spatial factor is");
        sb.AppendLine("  already in it, hardcoded. 'No space bending' is false.");
        sb.AppendLine();
        sb.AppendLine("  coefficients as they stand (TRM Parameters defaults):");
        foreach (var t in RefractiveLensAudit.TrmCoefficients())
            sb.AppendLine($"      {t.Symbol,-18}{t.Value,22:G17}   {t.Status}");
        sb.AppendLine();
        sb.AppendLine("  TRM's OWN TESTS CANNOT SUPPORT 'MATCHES GR':");
        sb.AppendLine("      test                ratio band        gamma accepted");
        foreach (var b in RefractiveLensAudit.TrmTestBands())
            sb.AppendLine($"      {b.Test,-18}[{b.RatioLow:F2}, {b.RatioHigh:F2}]      "
                        + $"[{b.GammaLow:+0.00;-0.00}, {b.GammaHigh:+0.00;-0.00}]");
        sb.AppendLine($"  ⇒ Cassini pins γ to 1 ± {RefractiveLensAudit.CassiniSigmaGamma:E1}; the widest accepted");
        sb.AppendLine($"    TRM window is {RefractiveLensAudit.BandLooseness():N0}x LOOSER. And every run is");
        sb.AppendLine($"    G = 1, c = 1, b = 1 at ε = 1e−3 … 1e−2 — that is {RefractiveLensAudit.TestRegimeOutsideSolarLow():N0}x to "
                      + $"{RefractiveLensAudit.TestRegimeOutsideSolarHigh():N0}x");
        sb.AppendLine("    OUTSIDE the solar regime. Solar-compactness deflection was never tested.");
        sb.AppendLine();
        sb.AppendLine("  THE ONE NON-METRIC CHANNEL IS DEAD:");
        sb.AppendLine($"      required |μ̇| at the Sun = λ_time/(λ_space·φ) = "
                      + $"{RefractiveLensAudit.RequiredMuDot(RefractiveLensAudit.SolarX):E4}");
        sb.AppendLine($"      physical  |μ̇| from the code's own definition = O("
                      + $"{RefractiveLensAudit.MuDotFromAcceleration():E2} … "
                      + $"{RefractiveLensAudit.MuDotFromRadialSweep():E2}) /s");
        sb.AppendLine($"      ⇒ shortfall {RefractiveLensAudit.RateTermShortfallAtSolar():E3}x");
        sb.AppendLine("  and |μ̇| is a NEW PRIMITIVE — AT's temporal core is ρ → g₀₀ → clock (G_035) with no");
        sb.AppendLine("  such variable, and G_029/G_030's no-new-primitive rule forbids adding one.");
        sb.AppendLine();
        sb.AppendLine("  TRM's own documents agree: TRM_Geodesic_Derivation.md calls the second term 'a natural");
        sb.AppendLine("  CANDIDATE for the missing spatial / CURVATURE-LIKE contribution' and lists λ_s's");
        sb.AppendLine("  derivation as the next open step; V3_4/main.tex nonclaims: 'No GR replacement is claimed.'");
        sb.AppendLine();

        PrintHeader("VERDICT");
        sb.AppendLine($"  {RefractiveLensAudit.Verdict()}");
        sb.AppendLine("  " + RefractiveLensAudit.WhereItStands());
        sb.AppendLine();
        sb.AppendLine("  WHAT SURVIVES FROM THE TRM IDEA");
        sb.AppendLine("   · the OPTICAL LANGUAGE — n − 1 = −(1+γ)Φ/c² is the cleanest statement of the");
        sb.AppendLine("     γ problem AT actually faces, and it shows the problem is not notation");
        sb.AppendLine("   · the RATE-DEPENDENT term as a genuinely non-metric, compact-object effect");
        sb.AppendLine("   · and a new falsification handle: any EM-only lens must beat GW170817, which");
        sb.AppendLine("     no astrophysically natural strength does");

        Output.WriteLine(sb.ToString());
    }
}
