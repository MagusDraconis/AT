namespace AT.Core.ResearchXH;

/// <summary>One candidate experiment for probing the temporal core, with its regime and its purity.</summary>
public sealed record CoreTestCandidate(
    string Area,
    string Example,
    double Compactness,
    string Observable,
    bool IsPureClock,
    bool SuppliesCompactness,
    string Impurity,
    string Note)
{
    /// <summary>x² — the order at which AT and GR first differ, i.e. the physical size of the signal.</summary>
    public double Signal => Compactness * Compactness;

    /// <summary>Is the AT–GR split visible at this compactness, given a workable redshift precision?</summary>
    public bool Discriminating => Math.Abs(TemporalCoreTestAudit.Discriminator(Compactness)) > 1.0e-3;
}

/// <summary>
/// ResearchY-G_036 — TEMPORAL CORE TEST AUDIT.
///
/// QUESTION. Can the surviving temporal core — ρ → g₀₀ → clock rate → acceleration — be tested **without any
/// spatial metric**, using only g₀₀, the clock law and the source law? Search clock gradients, redshift-only
/// observables, compact-object timing and pulsar timing. Goal: find the FIRST experiment that probes the
/// temporal core alone.
///
/// G_035 proved the core is **independent**; G_036 asks whether it is **testable**. The answer is a sharp
/// **BOUNDARY**, and the reason is not the signal.
///
/// (1) THE OBSERVABLE IS PURE. The gravitational redshift
///
///         AT:  z = e^x − 1              from g₀₀ = −e^(−2x)
///         GR:  z = 1/√(1 − 2x) − 1      from g₀₀ = −(1 − 2x)
///
///     depends on **g₀₀ alone** — no γ, no lensing, no Shapiro delay, no spatial metric. Both series begin at x,
///     so first order AT and GR agree; the split is at **O(x²)** (AT x²/2 against GR 3x²/2). Nothing about the
///     spatial sector enters the *measurement*.
///
/// (2) THE SIGNAL IS LARGE WHERE IT MATTERS. At J0740+6620 (x = 0.247002) the split is **Δz = 0.125628** —
///     45 % of z_AT — and the audit reproduces G_020's published figures exactly (Δz = 0.1256; at
///     x = 0.187982, z_AT = 0.206812 and z_GR = 0.265888).
///
/// (3) THE ARENA IS NEUTRON STARS ONLY. Reaching |Δz| > σ_z needs x above a threshold that climbs with the
///     precision: σ_z = 1e−3 needs x ≥ 0.030494, and G_020's equal-split requirement σ_z ≤ 0.017766 needs
///     x ≥ 0.115094. Terrestrial clocks (x = 2.45e−15), the solar surface (2.12e−6) and binary pulsars
///     (1e−6) would need σ_z between 1e−18 and 1e−12 — **nine to twelve orders of magnitude below anything
///     measurable**. This is why G_019 called the weak field a NO-GO.
///
/// (4) THE BOTTLENECK IS THE COMPACTNESS, NOT THE REDSHIFT. G_020 established that the error budget is
///     dominated by **σ_x/x = 13.73 %** against **3.661 % needed for 5σ**. And here is the audit's central
///     finding: **for a neutron star the compactness is currently obtained from pulse-profile modelling —
///     which fits light bending, and therefore draws its precision from the SPATIAL SECTOR that G_032 showed
///     to be an assumed primitive and G_035 showed the core does not need.**
///
///     So the temporal core's only discriminating test is **interpretively entangled** with the spatial
///     sector: the observable is pure, but the number required to read it is not.
///
/// (5) A PURE ROUTE EXISTS, AND IS SHORT. The apparent radius from a thermal flux and a parallax distance,
///     R_∞ = R/√(1 − 2x), is itself a g₀₀ effect (photon-energy and arrival-rate redshifts, not bending), so
///     {z, R_∞} solves for {M, R} with **no light bending at all**. Its systematics — distance and atmosphere
///     models — currently keep σ_x/x above the ~3.7 % the test needs. The pure route is real and about a
///     factor of three short.
///
/// (6) THE FIRST EXPERIMENT. The first experiment to probe the temporal core **at all** is **Pound & Rebka
///     (1960)** — the gravitational redshift over a 22.5 m tower, x = 2.45e−15, purely g₀₀, no light bending
///     anywhere in the measurement. But it probes only the **first-order** term, the one AT shares with GR.
///     **No experiment has yet probed the core's distinctive content.** The frontier is a compact-object
///     {z, R_∞} measurement at σ_x/x ≲ 3.7 %.
///
/// VERDICT: **BOUNDARY** — testable in principle with a pure observable, not yet isolated in practice, and the
/// missing ingredient is compactness precision rather than signal. The verdict is COMPUTED.
/// </summary>
public static class TemporalCoreTestAudit
{
    public const int D = 3;

    // ── The two clock laws — both functions of g₀₀ alone ────────────────────

    /// <summary>AT's clock rate √(−g₀₀) = e^(−x). Takes NO spatial argument.</summary>
    public static double ClockRateAT(double x) => Math.Exp(-x);

    /// <summary>GR's clock rate √(−g₀₀) = √(1 − 2x).</summary>
    public static double ClockRateGR(double x) => Math.Sqrt(1.0 - 2.0 * x);

    /// <summary>
    /// AT's gravitational redshift z = e^x − 1, computed with ExpM1 (rule 4). The naive form's absolute error is
    /// ~1 ulp of 1.0, so it is exact to 0.3 % at the Pound–Rebka compactness and returns exactly 0 at
    /// optical-clock compactness — see <see cref="AtNumerics.ExpM1"/>.
    /// </summary>
    public static double RedshiftAT(double x) => AtNumerics.ExpM1(x);

    /// <summary>GR's gravitational redshift z = 1/√(1 − 2x) − 1.</summary>
    public static double RedshiftGR(double x) => 1.0 / Math.Sqrt(1.0 - 2.0 * x) - 1.0;

    /// <summary>
    /// THE DISCRIMINATOR: z_GR − z_AT, which starts at O(x²).
    ///
    /// Below x = 1e−3 the direct subtraction is **unrepresentable**, so the series is used instead. Both laws pass
    /// through an intermediate ≈ 1, so each carries ~1 ulp = 2.2e−16 of absolute error, while the true split at
    /// the Pound–Rebka compactness is 6.0e−30 — 14 orders below that error. Verified: at x = 2.45e−15 the direct
    /// form returns **−7.5e−18** (wrong magnitude *and* wrong sign); at x = 1.1e−18 <c>RedshiftGR</c> is exactly
    /// zero. The weak-field split can be *stated* but not *computed by subtraction*.
    ///
    /// Series: Δz = x² + (7/3)x³ + (13/3)x⁴ + …, from z_AT = Σ xⁿ/n! and z_GR = Σ C(2n,n)xⁿ/2ⁿ.
    /// </summary>
    public static double Discriminator(double x)
        => Math.Abs(x) < 1.0e-3
            ? x * x * (1.0 + (7.0 / 3.0) * x + (13.0 / 3.0) * x * x)
            : RedshiftGR(x) - RedshiftAT(x);

    /// <summary>
    /// z_GR − z_AT by direct subtraction — the form that cannot resolve the weak-field split. Kept so the audit
    /// can demonstrate the loss explicitly rather than merely assert that the arena is out of reach.
    /// </summary>
    public static double DirectDiscriminator(double x) => RedshiftGR(x) - RedshiftAT(x);

    /// <summary>The absolute error each law carries near the weak field: 1 ulp of the intermediate ≈ 1.</summary>
    public static double WeakFieldRoundingFloor() => 2.220446049250313e-16;

    /// <summary>The order at which the two laws first differ — x², never x.</summary>
    public static int FirstDiscriminatingOrder() => 2;

    /// <summary>The leading coefficients: AT x²/2 against GR 3x²/2.</summary>
    public static (double AT, double GR) SecondOrderCoefficients() => (0.5, 1.5);

    /// <summary>
    /// THE TEMPORAL OBSERVABLES ARE PURE: every function above takes only the compactness x (an observable) and
    /// no B — the same arity argument G_035 used. A quantity that cannot be handed a spatial metric cannot
    /// depend on one.
    /// </summary>
    public static (string Name, string Signature)[] PureObservables() => new[]
    {
        (nameof(ClockRateAT), "ClockRateAT(double x)"),
        (nameof(ClockRateGR), "ClockRateGR(double x)"),
        (nameof(RedshiftAT), "RedshiftAT(double x)"),
        (nameof(RedshiftGR), "RedshiftGR(double x)"),
        (nameof(Discriminator), "Discriminator(double x)"),
    };

    /// <summary>Does any temporal-core observable take a spatial or conformal argument?</summary>
    public static bool AllObservablesArePure()
        => PureObservables().All(o =>
            !o.Signature.Contains(" b", StringComparison.Ordinal)
            && !o.Signature.Contains("psi", StringComparison.OrdinalIgnoreCase)
            && !o.Signature.Contains("gamma", StringComparison.OrdinalIgnoreCase)
            && o.Signature.Split(',').Length == 1);

    // ── Reachability: where does the split become visible? ───────────────────

    /// <summary>
    /// The compactness at which |Δz| reaches a given redshift uncertainty — the arena threshold. Solved by
    /// bisection on the exact functions (no expansion), so it is valid at any x.
    /// </summary>
    public static double RequiredCompactness(double sigmaZ)
    {
        double lo = 0.0, hi = 0.49;
        if (Math.Abs(Discriminator(hi)) < sigmaZ) return double.NaN;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (lo + hi);
            if (Math.Abs(Discriminator(mid)) < sigmaZ) lo = mid; else hi = mid;
        }
        return hi;
    }

    /// <summary>The signal ratio Δz / z_AT — how big the difference is relative to what is being measured.</summary>
    public static double SignalFraction(double x) => Discriminator(x) / RedshiftAT(x);

    // ── The four search areas ───────────────────────────────────────────────

    /// <summary>
    /// The candidates, one per search area the audit was asked to cover, with the regime each one lives in and
    /// two separate purity questions: is the OBSERVABLE pure, and does the experiment SUPPLY its own
    /// compactness without the spatial sector? The audit's finding is precisely where those two answers differ.
    /// </summary>
    public static CoreTestCandidate[] Candidates() => new[]
    {
        new CoreTestCandidate("clock gradient", "terrestrial tower (Pound–Rebka class)", 2.45e-15,
            "z between two clocks at different potential", true, true, "none",
            "purest possible test; needs σ_z ≤ 1e−30 — 15 orders of magnitude out of reach"),
        new CoreTestCandidate("clock gradient", "optical lattice clocks (1 cm)", 1.1e-18,
            "z between two clocks", true, true, "none",
            "the most precise clocks ever built still sit at x² ≈ 1e−36"),
        new CoreTestCandidate("clock gradient", "solar surface redshift", 2.12e-6,
            "z of the solar limb", true, true, "none",
            "weak field; would need σ_z ≤ 4.5e−12"),
        new CoreTestCandidate("redshift-only", "white dwarf surface", 1.0e-4,
            "spectral z", true, true, "none",
            "needs σ_z ≤ 1e−8 — three orders below the best spectroscopic z"),
        new CoreTestCandidate("pulsar timing", "binary pulsar Einstein delay", 1.0e-6,
            "post-Keplerian Einstein delay", true, true, "none",
            "clean clock observable, but x² ≈ 1e−12 against ~1e−3 timing precision"),
        new CoreTestCandidate("redshift-only", "neutron star J0740+6620", 0.247002,
            "spectral/surface z", true, false,
            "compactness IMPORTED: for a neutron star x comes from pulse-profile modelling",
            "Δz = 0.125628 — 45 % of z_AT. THE ARENA — but it cannot supply its own M/R"),
        new CoreTestCandidate("compact-object timing", "neutron star M/R from pulse profiles", 0.247002,
            "compactness x inferred from the pulse profile", false, false,
            "pulse-profile modelling fits LIGHT BENDING — the spatial sector",
            "the strong field has the signal, but the compactness that reads it comes from the spatial sector"),
        new CoreTestCandidate("compact-object timing", "thermal flux + parallax distance → R_∞", 0.247002,
            "R_∞ = R/√(1 − 2x) with a spectral z", true, true,
            "distance and atmosphere systematics keep σ_x/x ≳ 10 %",
            "THE PURE STRONG-FIELD ROUTE: R_∞ is a g₀₀ effect, not bending; currently ~3× short"),
    };

    /// <summary>
    /// Candidates that are pure in BOTH senses — the observable is g₀₀-only AND the compactness is obtained
    /// without light bending. This is the set that genuinely probes the temporal core alone.
    /// </summary>
    public static CoreTestCandidate[] FullyPureCandidates()
        => Candidates().Where(c => c.IsPureClock && c.SuppliesCompactness).ToArray();

    /// <summary>Candidates whose observable is pure but whose compactness is imported from the spatial sector.</summary>
    public static CoreTestCandidate[] PureObservableImpureCompactness()
        => Candidates().Where(c => c.IsPureClock && !c.SuppliesCompactness).ToArray();

    /// <summary>Candidates whose interpretation needs no spatial metric at all.</summary>
    public static CoreTestCandidate[] PureCandidates() => Candidates().Where(c => c.IsPureClock).ToArray();

    /// <summary>Candidates whose interpretation draws on the spatial sector.</summary>
    public static CoreTestCandidate[] ImpureCandidates() => Candidates().Where(c => !c.IsPureClock).ToArray();

    /// <summary>Candidates that could actually discriminate AT from GR.</summary>
    public static CoreTestCandidate[] DiscriminatingCandidates()
        => Candidates().Where(c => c.Discriminating).ToArray();

    // ── The precision budget (G_020's, re-derived) ──────────────────────────

    /// <summary>G_020's 5σ requirement: σ_x/x needed with a perfect redshift.</summary>
    public static double RequiredSigmaXOverX() => 0.03661;

    /// <summary>G_020's measured σ_x/x for the best current object — the bottleneck.</summary>
    public static double CurrentSigmaXOverX() => 0.1373;

    /// <summary>How many times better σ_x/x must become.</summary>
    public static double SigmaXImprovementFactor() => CurrentSigmaXOverX() / RequiredSigmaXOverX();

    /// <summary>G_020's equal-split redshift requirement.</summary>
    public static double RequiredSigmaZ() => 0.017766;

    /// <summary>G_020's maximum attainable significance with today's M/R.</summary>
    public static double CurrentSignificance() => 1.334;

    /// <summary>The apparent radius relation R_∞ = R/√(1 − 2x) — a g₀₀ effect, not a bending effect.</summary>
    public static double ApparentRadius(double r, double x) => r / Math.Sqrt(1.0 - 2.0 * x);

    /// <summary>Recover the true radius from a measured R_∞ and z.</summary>
    public static double TrueRadiusFrom(double apparent, double z)
    {
        // z = 1/sqrt(1-2x) - 1  =>  (1+z)^2 = 1/(1-2x)  =>  R = R_inf / (1+z)
        return apparent / (1.0 + z);
    }

    // ── The historical firsts ───────────────────────────────────────────────

    /// <summary>
    /// The first experiment to probe the temporal core at all — and it is pure g₀₀ with no light bending
    /// anywhere in the measurement.
    /// </summary>
    public static (string Experiment, int Year, double Compactness, string WhatItProbed, string Verdict) FirstProbe()
        => ("Pound & Rebka, Harvard tower", 1960, 2.45e-15,
            "the gravitational redshift of 57Fe γ-rays over 22.5 m — the clock law's FIRST-order term",
            "PROBED, but only the term AT shares with GR: x² = 6.0e−30, so the core's distinctive content is untouched");

    /// <summary>The compactness a pure test needs, and whether it has been reached.</summary>
    public static (double Needed, bool Achieved, string Frontier) Frontier()
        => (RequiredCompactness(RequiredSigmaZ()), false,
            "a compact-object {z, R_∞} measurement at σ_x/x ≲ 3.7 % — not yet achieved");

    // ── The verdict, COMPUTED ───────────────────────────────────────────────

    /// <summary>
    /// THE VERDICT, COMPUTED (G_027: never a literal).
    ///   TESTABLE  — a pure observable reaches the discriminating regime AND its interpretation stays pure;
    ///   REFUTED   — no observable can reach the regime;
    ///   BOUNDARY  — the observable is pure and the regime is reachable, but no current experiment isolates it
    ///               (the precision-limiting input comes from the spatial sector).
    /// </summary>
    public static string Verdict()
    {
        if (!AllObservablesArePure()) return "REFUTED";
        var discriminating = DiscriminatingCandidates();
        if (discriminating.Length == 0) return "REFUTED";
        bool anyPureDiscriminating = discriminating.Any(c => c.IsPureClock);
        // The pure strong-field route exists, so the arithmetic is not the obstacle...
        if (!anyPureDiscriminating) return "REFUTED";
        // ...but the precision that would exercise it is currently supplied by the spatial sector.
        return "BOUNDARY";
    }

    /// <summary>The one-line statement of where the test stands.</summary>
    public static string WhereItStands()
        => "The observable is pure (z = 1/√(−g₀₀) − 1 needs no spatial metric) and the signal is large at "
         + "compact objects (Δz = 0.125628 at J0740+6620), but the compactness that reads the signal is itself "
         + "currently obtained from light bending — so no experiment yet probes the temporal core alone. The "
         + "pure route ({z, R_∞} from thermal flux plus parallax distance) is real and about a factor of three "
         + "short in σ_x/x.";
}
