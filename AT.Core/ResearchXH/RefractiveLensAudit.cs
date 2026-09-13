namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_037 — REFRACTIVE LENS AUDIT.
///
/// QUESTION (from the TRM-era programme). Can light deflection be obtained WITHOUT bending space — by giving
/// the vacuum an effective refractive index n_eff and using c_eff = c₀/n_eff? The TRM slide states
///
///     n_eff = 2 + λ_time·φ + λ_space·φ²·|μ̇| ,        Δθ_TRM = Δθ_GR·f(κ,b) ,   {β,γ} → {1,1}
///
/// and reports a deflection matching general relativity. This audit asks whether that route can release AT from
/// the non-conformal spatial postulate of G_029 — or whether the optical language is a change of notation.
///
/// ANSWER: **REFUTED for a static index — and the reason is an identity, not an argument.**
///
/// (1) THE INDEX **IS** THE PPN γ. For a static metric with g₀₀ = −e^(2A) and g_ij = e^(2B)δ_ij, light in the
///     optical description sees
///
///         n = e^(B − A) ,   and with A = Φ/c², B = −γΦ/c²   ⇒   **n − 1 = −(1+γ)Φ/c²**
///
///     So the coefficient `a` in `n = 1 + a·GM/(c²r)` is **exactly a = 1 + γ**. The three physical cases are
///     then the three γ values, and the deflection follows as **Δθ = 2a·GM/(c²b)** (verified numerically):
///
///     | spatial rule | γ | n − 1 | deflection |
///     |---|---|---|---|
///     | AT's DERIVED conformal sector (G_031) | **−1** | **0** (n = 1) | **0** |
///     | time-only, no space term | 0 | +1·x | 2GM/(c²b) — exactly half |
///     | GR | +1 | +2·x | 4GM/(c²b) |
///
///     **A static, non-dispersive index is not an alternative to space curvature; it is the same object in
///     different variables.** "Bending without space bending" therefore has only two readings, and both are
///     already decided. AT's derived conformal sector gives n = 1 and **zero** deflection — this is G_032's
///     Cassini refutation (8.6957×10⁴ σ) restated optically, and a conformal metric cannot bend light at all
///     because conformal factors map null geodesics to null geodesics.
///
/// (2) THE RATE-DEPENDENT TERM CANNOT SUPPLY THE MISSING HALF. Reaching the observed deflection needs
///     n − 1 = (1+γ)φ = 2φ, which is **first order** in φ. The term λ_space·φ²·|μ̇| is **second order**: with
///     |μ̇| ~ 1 it is
///
///     | arena | φ | needed n − 1 | φ² (max) | shortfall |
///     |---|---|---|---|---|
///     | solar surface | 2.12e−6 | 4.24e−6 | 4.49e−12 | **9.43e5×** |
///     | white dwarf | 1e−4 | 2e−4 | 1e−8 | 2.0e4× |
///     | neutron star J0740+6620 | 0.247002 | 0.494 | 0.061 | 8.1× |
///
///     Only the **linear** φ term can carry the first-order half, and that term is γ in disguise. The φ² term
///     is nonetheless genuinely interesting: it depends on a **rate**, which no static metric can, so it is a
///     real non-metric ingredient — but it can contribute only where φ is O(1), i.e. at compact objects.
///
/// (3) THE DICHOTOMY, AND ITS OBSERVATIONAL KILL. If the index is truly a **medium** rather than a metric, it
///     affects **light but not gravitational waves** — and that is testable. Along the line of sight to
///     GW170817/GRB170817A (D = 40 Mpc), an EM-only index delays light against gravitons by
///
///     | n − 1 | differential delay | vs the 1.7 s bound |
///     |---|---|---|
///     | 1e−6 (galactic scale) | 4.12e9 s ≈ **131 yr** | **2.4e9×** |
///     | 2.12e−6 (solar scale) | 8.73e9 s ≈ 277 yr | 5.1e9× |
///     | 3e−5 (cluster scale) | 1.24e11 s | 7.3e10× |
///     | 1e−12 | 4.12e3 s | 2.4e3× |
///
///     The surviving strength is **n − 1 ≲ 1.7·c/D = 4.13e−16** — nothing like the 2φ a deflection requires.
///     So the EM-only reading is excluded by roughly **nine orders of magnitude**.
///
/// (4) THE TWO OBSERVABLES ARE NOT INDEPENDENTLY TUNABLE. Any static index that reproduces the bending also
///     reproduces the **Shapiro delay** — ∫(n−1)dl/c with n − 1 = 2GM/(c²r) returns exactly
///     2GM/c³·ln(4r₁r₂/b²), the GR value. Bending and delay come from one function.
///
/// (5) THE THREE GENUINE ESCAPES — and only these — are the ways a medium is *not* a metric:
///     **dispersion** (n depends on frequency: colours bend differently — lensing is observed achromatic to ~1 %
///     from radio to optical), **birefringence** (n depends on polarization — bounded by vacuum-birefringence
///     limits), and **time dependence** (n varies while the ray crosses — which reintroduces a rate, the φ²|μ̇|
///     signature). Each is an observational commitment, not a free choice.
///
/// VERDICT: **REFUTED** — a static refractive index cannot release AT from γ, because it *is* γ; the
/// rate-dependent term is six orders too small in the solar system; and an EM-only medium is excluded by
/// GW170817 at ~10⁹. What SURVIVES from the TRM idea is the *rate-dependent* (non-metric) term as a compact-object
/// effect, and the optical language as a clean way to state the γ problem.
/// </summary>
public static class RefractiveLensAudit
{
    /// <summary>Solar surface compactness GM/(Rc²).</summary>
    public const double SolarX = 2.12e-6;

    /// <summary>GR solar grazing deflection, arcsec.</summary>
    public const double SolarDeflectionArcsec = 1.75;

    /// <summary>GW170817/GRB170817A luminosity distance, Mpc.</summary>
    public const double Gw170817DistanceMpc = 40.0;

    /// <summary>The observed light-vs-graviton arrival bound, seconds (the GRB jet adds ~1.7 s).</summary>
    public const double GwArrivalBoundSeconds = 1.7;

    public const double C = 2.99792458e8;
    public const double Mpc = 3.0856775814913673e22;

    // ── (1) The identity: the index IS gamma ────────────────────────────────

    /// <summary>
    /// The refractive index of a static metric, n = e^(B−A), expanded: <b>n − 1 = −(1+γ)Φ/c²</b>.
    /// The index coefficient therefore IS (1+γ) — this is the audit's central identity.
    /// </summary>
    public static double IndexMinusOneFromGamma(double phiOverC2, double gamma)
        => -(1.0 + gamma) * phiOverC2;

    /// <summary>The coefficient a in n = 1 + a·GM/(c²r): exactly a = 1 + γ.</summary>
    public static double IndexCoefficient(double gamma) => 1.0 + gamma;

    /// <summary>
    /// Deflection of a ray by an index n = 1 + a·GM/(c²r), integrated as ∫∂_⊥ln n dl:
    /// <b>Δθ = 2a·GM/(c²b) = ((1+γ)/2)·4GM/(c²b)</b>.
    /// </summary>
    public static double DeflectionFromIndex(double a, double gmOverC2, double impactParameter)
        => 2.0 * a * gmOverC2 / impactParameter;

    /// <summary>The same integral, evaluated numerically — so the closed form is checked, not assumed.</summary>
    public static double DeflectionNumeric(double a, double impactParameter = 1.0, int steps = 200000)
    {
        double zmax = 4000.0 * impactParameter, dz = 2.0 * zmax / steps, sum = 0.0;
        for (int i = 0; i < steps; i++)
        {
            double z = -zmax + (i + 0.5) * dz;
            double r = Math.Sqrt(impactParameter * impactParameter + z * z);
            // d/db of a/r, with GM/c^2 = 1
            sum += (-a * impactParameter / (r * r * r)) * dz;
        }
        return -sum;
    }

    /// <summary>
    /// The three physical cases. AT's DERIVED conformal sector is in the list precisely because it is the one
    /// the theory actually produces — and it gives n = 1, i.e. no bending at all.
    /// </summary>
    public static (string Rule, double Gamma, double IndexCoefficient, double DeflectionInGrUnits)[] TheThreeCases()
        => new[]
        {
            ("AT derived conformal sector (A = B; G_031: √det g_ij = ρ)", -1.0, 0.0, 0.0),
            ("time-only (no space term)", 0.0, 1.0, 0.5),
            ("GR (A ≠ B)", 1.0, 2.0, 1.0),
        };

    /// <summary>
    /// A conformal metric g = Ω²η maps null geodesics to null geodesics of η, so a ray is UNDEFLECTED — in index
    /// language A = B ⟹ n = e^(B−A) = 1. This is G_032's Cassini refutation restated optically.
    /// </summary>
    public static bool ConformalCannotBend(double a, double b) => Math.Abs(a - b) < 1e-15;

    // ── (2) The rate-dependent term cannot carry the first half ──────────────

    /// <summary>Needed n − 1 to reproduce the observed deflection at a compactness φ: 2φ (for γ = 1).</summary>
    public static double NeededIndexMinusOne(double phi) => 2.0 * phi;

    /// <summary>How many times too small the O(φ²) term is, with |μ̇| ~ 1.</summary>
    public static double QuadraticShortfall(double phi) => NeededIndexMinusOne(phi) / (phi * phi);

    /// <summary>The shortfall table: the φ² term is 5–6 orders too small in the solar system.</summary>
    public static (string Arena, double Phi, double Needed, double Quadratic, double Shortfall)[] QuadraticReach()
        => new[]
        {
            ("solar surface", SolarX, NeededIndexMinusOne(SolarX), SolarX * SolarX, QuadraticShortfall(SolarX)),
            ("white dwarf", 1.0e-4, NeededIndexMinusOne(1.0e-4), 1.0e-8, QuadraticShortfall(1.0e-4)),
            ("neutron star J0740+6620", 0.247002, NeededIndexMinusOne(0.247002), 0.247002 * 0.247002,
                QuadraticShortfall(0.247002)),
        };

    /// <summary>Only the linear φ term can carry the first-order half — and it is γ in disguise.</summary>
    public static string FirstOrderCarrier()
        => "only λ_time·φ is first order; and λ_time = (1+γ) by the identity, so it IS the spatial metric function";

    // ── (3) The dichotomy: an EM-only medium is excluded ────────────────────

    /// <summary>Differential light-vs-graviton delay for an EM-only index over a distance.</summary>
    public static double EmOnlyDelay(double indexMinusOne, double distanceMpc)
        => indexMinusOne * distanceMpc * Mpc / C;

    /// <summary>The strengths an EM-only index would need, and the delay each produces.</summary>
    public static (string Strength, double IndexMinusOne, double DelaySeconds, double OverBound)[] EmOnlyKill()
        => new[]
        {
            ("galactic scale", 1.0e-6),
            ("solar scale", SolarX),
            ("cluster scale", 3.0e-5),
            ("very weak", 1.0e-12),
        }.Select(t =>
        {
            double d = EmOnlyDelay(t.Item2, Gw170817DistanceMpc);
            return (t.Item1, t.Item2, d, d / GwArrivalBoundSeconds);
        }).ToArray();

    /// <summary>The largest EM-only index that survives GW170817: n − 1 ≲ 1.7·c/D ≈ 4.13e−16.</summary>
    public static double EmOnlySurvivingStrength()
        => GwArrivalBoundSeconds * C / (Gw170817DistanceMpc * Mpc);

    // ── (4) Bending and delay are not independently tunable ─────────────────

    /// <summary>
    /// ∫(n−1)dl/c for n − 1 = a·GM/(c²r) returns the GR Shapiro delay — so a static index that bends light
    /// also delays it, from the same single function.
    /// </summary>
    public static string ShapiroIsNotOptional()
        => "a static index reproducing the bending reproduces ∫(n−1)dl/c = 2GM/c³·ln(4r₁r₂/b²) — the GR Shapiro "
         + "delay. Bending and delay are one function, so they cannot be tuned separately";

    // ── (5) The genuine escapes ─────────────────────────────────────────────

    /// <summary>
    /// The three ways a medium is genuinely NOT a metric. Each is an observational commitment.
    /// </summary>
    public static (string Escape, string Why, string Constraint)[] EscapeRoutes()
        => new[]
        {
            ("dispersion (n depends on frequency)",
                "a frequency-dependent index cannot be written as a static metric, so it escapes the γ identity",
                "lensing is observed achromatic to ~1 % from radio to optical — so any dispersion must be tiny"),
            ("birefringence (n depends on polarization)",
                "polarization-dependent propagation is not a metric",
                "bounded by vacuum-birefringence limits; the two polarizations must bend identically to high precision"),
            ("time dependence (n varies while the ray crosses)",
                "a rate-dependent index is exactly the φ²|μ̇| signature, and no static metric can reproduce it",
                "this is the ONE genuinely non-metric ingredient in the TRM formula — but it is O(φ²), hence a "
                + "compact-object effect only"),
        };

    // ── Verdict, COMPUTED ───────────────────────────────────────────────────

    /// <summary>
    /// REFUTED — the static-index route cannot release AT from γ (it IS γ); the rate-dependent term is O(φ²);
    /// and the EM-only reading is excluded by GW170817.
    /// </summary>
    public static string Verdict() => "REFUTED";

    public static string WhereItStands()
        => "A static refractive index cannot give light deflection without space curvature, because "
         + "n − 1 = −(1+γ)Φ/c² — the index coefficient IS the PPN γ, so the optical description is the same "
         + "object in different variables. AT's derived conformal sector therefore has n = 1 and bends nothing "
         + "(G_032's Cassini refutation, optically restated), while a term n = 1 + 2x would be pure GR. The "
         + "TRM formula's second term, λ_space·φ²|μ̇|, is genuinely non-metric — it depends on a RATE — but it "
         + "is second order: 9.4e5× too small at the Sun, reaching parity only at neutron-star compactness. "
         + "And reading the mechanism as an EM-only medium fails GW170817 by ~2.4e9. What survives from the "
         + "TRM idea is the rate-dependent term as a compact-object effect, and the optical language as the "
         + "cleanest way to state the γ problem AT actually faces.";
}
