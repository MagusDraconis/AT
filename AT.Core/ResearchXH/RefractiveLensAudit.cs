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
///     **The deficit is not in the formula, though.** The slide's leading constant is the literal `2.0`, and the
///     code accelerates by `ar = −n_eff·GM/r²` — so the first-order coefficient the deflection sees is `n_eff`
///     itself, i.e. `2`, not `λ_time`. **The literal 2 IS (1+γ) = 2, i.e. γ = 1**, so the spatial factor is
///     already present, hardcoded (see section 6). What the φ² term would have to supply is therefore not "the
///     missing half" but an *independent* spatial term — and it cannot. The term is nonetheless genuinely
///     interesting: it depends on a **rate**, which no static metric can, so it is a real non-metric ingredient —
///     but it can contribute only where φ is O(1), i.e. at compact objects.
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
/// (6) THE SOURCE CHECK — AND A CORRECTION TO THIS AUDIT'S OWN FIRST READING. `TRM.Core/Shared/`
///     `PhotonTransportModel.cs` was still on disk, so the formula was read *in situ* instead of inferred.
///     Two things changed, and the conclusion got stronger.
///     - **`λ_time = 1` does NOT give half.** The acceleration is `ar = −n_eff·G·M/(r·r)` with
///       `n_eff = 2 + λ_time·φ + λ_space·φ²|μ̇|`, so at the solar limb `n_eff = 2.000002122` → ratio 1.000001
///       → **FULL deflection**. An earlier reading of this audit treated `λ_time` as the index coefficient;
///       the code uses the whole `n_eff` as the force multiplier. Half would require `grad(n_eff − 2)`.
///     - **The file uses BOTH conventions at once.** Travel time is `(n_eff − 2)·v` (physical index is
///       `n_eff − 2` ⟹ HALF); the acceleration keeps the `2` (⟹ FULL). One function, two conventions, so the
///       reported result depends on which line is read — the model is not well defined until one is chosen.
///     - **The literal 2 is the whole first-order physics.** At solar compactness every other term is ≲1e−5
///       relative: `KBase = 2 + 2Aφ + 3Bφ²` with `A = −0.1701452243330672`, `B = −8.484408441898648` moves the
///       factor by −7.2e−7. The deflection is 1.75″ *because the code multiplies GM/r² by the literal 2* —
///       which is exactly γ = 1, i.e. AT's G_029 postulate in other variables. **"No space bending" is false:
///       the space bending is typed in as a constant.**
///     - **TRM's own deflection tests cannot support "matches GR".** `EL04` asserts `ratio ∈ [0.95, 1.08]`
///       (γ ∈ [0.90, 1.16]) and `ratioEuler ∈ [0.85, 1.25]` (γ ∈ [0.70, 1.50]) with `|EL − TRM|/Schw ≤ 0.30`;
///       `EL03` allows `[0.70, 1.25]` (γ ∈ [0.40, 1.50]). The widest window is 1.10 wide against Cassini's
///       4.6e−5 — **23,913× looser**. And every run is `G = 1, c = 1, b = 1` at ε = 1e−3…1e−2, i.e.
///       **471×–4,717× outside the solar regime**: solar-compactness deflection was never tested.
///     - **The one non-metric channel needs a primitive AT does not have.** Reaching first order needs
///       `|μ̇| = λ_time/(λ_space·φ) = 1.57e4` at solar compactness; the code's own `ComputeAbsDmuDtBase` yields
///       `|μ̇| ~ O(1.8e−6 … 0.43)` /s there → **shortfall ≥ 3.6e4**. And `|μ̇|` is a new field: AT's temporal
///       core is `ρ → g₀₀ → clock` (G_035) with no such variable, and G_029/G_030's no-new-primitive rule
///       forbids introducing one.
///     - **TRM's own documents already said so.** `docs/Archive/TRM_Geodesic_Derivation.md` calls the second
///       term "a natural **candidate** for the missing spatial / **curvature-like** contribution" and lists
///       `λ_s`'s derivation as the NEXT OPEN THEORETICAL STEP; `V3_4/main.tex` nonclaims read *"No GR
///       replacement is claimed."*
///
/// VERDICT: **REFUTED** — a static refractive index cannot release AT from γ, because it *is* γ; the
/// rate-dependent term is six orders too small in the solar system; and an EM-only medium is excluded by
/// GW170817 at ~10⁹. Reading the TRM source strengthens this rather than weakening it: the only ingredient
/// that ever produced the observed deflection is the literal constant `2`, i.e. γ = 1 — the very quantity
/// G_030 proved cannot be derived. What SURVIVES from the TRM idea is the *rate-dependent* (non-metric) term as
/// a compact-object effect, and the optical language as a clean way to state the γ problem.
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

    /// <summary>
    /// The first-order coefficient is the formula's leading constant, not λ_time — and that constant is γ in
    /// disguise.
    /// </summary>
    public static string FirstOrderCarrier()
        => "the first-order coefficient is the formula's LEADING CONSTANT (2), not λ_time; and by the identity "
         + "a coefficient of 2 means γ = 1, so that constant IS the spatial metric function";

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

    // ── (6) The source check: the slide's formula already contains the spatial factor ──────

    /// <summary>
    /// The leading constant of the TRM formula. The code accelerates by `ar = −n_eff·G·M/(r·r)`, so this
    /// constant — not λ_time — is the first-order coefficient the deflection sees.
    /// </summary>
    public const double TrmLeadingConstant = 2.0;

    /// <summary>Solar radius, m — used only to put a scale on |μ̇|.</summary>
    public const double SolarRadius = 6.96e8;

    /// <summary>Cassini's 1σ uncertainty on γ — the measurement TRM's tests would have had to match.</summary>
    public const double CassiniSigmaGamma = 2.3e-5;

    /// <summary>
    /// The coefficients as they stand in `TRM.Core/Shared/PhotonTransportModel.Parameters` (defaults), with the
    /// source's own status annotation. Three are fitted to 16 significant digits; the file's own summary says
    /// the λ terms are CALIBRATED, and it annotates EulerBridgeScale as "NOT a fundamental physical constant".
    /// </summary>
    public static (string Symbol, double Value, string Status)[] TrmCoefficients()
        => new[]
        {
            ("LambdaTime", 1.0, "hardcoded 1.0 — NOT the index coefficient"),
            ("LambdaSpace", 30.0, "hardcoded 30.0; summary says 'CALIBRATED (lambda terms)'"),
            ("A", -0.1701452243330672, "fitted to 16 digits"),
            ("B", -8.484408441898648, "fitted to 16 digits"),
            ("Lambda", 30.79445857638716, "fitted to 16 digits"),
            ("EulerBridgeScale", 0.85, "'17/20 ... NOT a fundamental physical constant'"),
        };

    /// <summary>
    /// n_eff at solar compactness WITH the leading constant — what the acceleration actually uses. It is
    /// 2.000002122, so the deflection ratio against GR is 1.000001: <b>FULL, not half</b>.
    /// </summary>
    public static double TrmEffectiveIndexAtSolar() => TrmLeadingConstant + 1.0 * SolarX;

    /// <summary>
    /// The same quantity WITHOUT the leading constant — the convention the travel-time line uses,
    /// `timeAccumDerivative = (n_eff − 2)·v`. Under this reading the deflection is HALF.
    /// The file uses both conventions in one function.
    /// </summary>
    public static double TrmPhysicalIndexShiftAtSolar() => 1.0 * SolarX;

    /// <summary>
    /// The γ implied by the leading constant, via this audit's identity a = 1 + γ. A constant of 2 means γ = 1:
    /// the formula already contains the spatial factor, hardcoded.
    /// </summary>
    public static double GammaImpliedByLeadingConstant() => TrmLeadingConstant - 1.0;

    /// <summary>
    /// The γ window TRM's own deflection tests accept, from `EL03`/`EL04` in
    /// `TRM.Tests/RealityTests/PhotonTransportModel_GeodesicSolverTests.cs`, converted by γ = 2·ratio − 1.
    /// </summary>
    public static (string Test, double RatioLow, double RatioHigh, double GammaLow, double GammaHigh)[] TrmTestBands()
        => new[] { ("EL03 TRM", 0.70, 1.25), ("EL04 transport", 0.95, 1.08), ("EL04 Euler", 0.85, 1.25) }
            .Select(t => (t.Item1, t.Item2, t.Item3, 2.0 * t.Item2 - 1.0, 2.0 * t.Item3 - 1.0))
            .ToArray();

    /// <summary>How many times looser the widest accepted TRM γ window is than Cassini's 1σ.</summary>
    public static double BandLooseness()
    {
        var bands = TrmTestBands();
        double widest = bands.Max(b => b.GammaHigh - b.GammaLow);
        return widest / (2.0 * CassiniSigmaGamma);
    }

    /// <summary>
    /// The lower end of TRM's tested compactness range (ε = 1e−3 with G = c = b = 1), expressed as how far
    /// outside the solar regime the deflection validation sits.
    /// </summary>
    public static double TestRegimeOutsideSolarLow() => 1.0e-3 / SolarX;

    /// <summary>The upper end of the same gap (ε = 1e−2).</summary>
    public static double TestRegimeOutsideSolarHigh() => 1.0e-2 / SolarX;

    /// <summary>|μ̇| required for λ_space·φ²|μ̇| to reach first order at a compactness φ.</summary>
    public static double RequiredMuDot(double phi, double lambdaTime = 1.0, double lambdaSpace = 30.0)
        => lambdaTime / (lambdaSpace * phi);

    /// <summary>
    /// |d(v̂)/dt| = a_⊥/c ≈ 2GM/(R²c) = 2·φ_sun·c/R at the solar limb — from the code's own
    /// `ComputeAbsDmuDtBase`, which divides the baseline acceleration by |v| = c.
    /// </summary>
    public static double MuDotFromAcceleration() => 2.0 * SolarX * C / SolarRadius;

    /// <summary>|d(ê_r)/dt| ≈ c/R at the solar limb — the other term of the same expression.</summary>
    public static double MuDotFromRadialSweep() => C / SolarRadius;

    /// <summary>
    /// How far short the rate channel falls at the Sun: the required |μ̇| over the largest physically
    /// available one.
    /// </summary>
    public static double RateTermShortfallAtSolar()
        => RequiredMuDot(SolarX) / Math.Max(MuDotFromAcceleration(), MuDotFromRadialSweep());

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
         + "And reading the mechanism as an EM-only medium fails GW170817 by ~2.4e9. READING THE SOURCE "
         + "STRENGTHENS THIS. The slide's leading constant is the literal 2.0, and the code accelerates by "
         + "ar = −n_eff·G·M/(r·r), so the deflection is full *only because a hardcoded 2 supplies γ = 1* — the "
         + "same content as AT's G_029 postulate, renamed. TRM's own tests accept γ ∈ [0.40, 1.50] (23,913× "
         + "looser than Cassini) and never ran at solar compactness, and the rate channel needs |μ̇| = 1.57e4 "
         + "against a physical ≤ 0.43/s. So the only ingredient that ever produced the observed deflection is "
         + "the literal γ = 1 — which G_030 proved cannot be derived. What survives from the TRM idea is the "
         + "rate-dependent term as a compact-object effect, and the optical language as the cleanest way to "
         + "state the γ problem AT actually faces.";
}
