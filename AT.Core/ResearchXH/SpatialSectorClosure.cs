namespace AT.Core.ResearchXH;

/// <summary>A parametrisation family for the spatial exponent B, given A = σ fixed by G_028.</summary>
public enum SpatialFamily
{
    /// <summary>B = σ — the counting measure (√det g_ij = ρ). γ = −1.</summary>
    Conformal,

    /// <summary>B = −σ — the linear member. γ = +1 to first order.</summary>
    NegSigma,

    /// <summary>B = ½ln(2 − e^(2σ)) — the unique member with γ = +1 at ALL orders.</summary>
    ExactGr,

    /// <summary>B = −σ + c₂σ² — a polynomial deformation; c₂ free.</summary>
    Polynomial,

    /// <summary>B = −σ·(1 − λ(1 − e^(−μ|σ|))/… ) — a rational/exponential deformation with f′(0) = −1.</summary>
    RationalExponential,
}

/// <summary>One spatial candidate's computed observables at a compactness (ResearchY-G-029).</summary>
public sealed record SpatialCase(
    SpatialFamily Family,
    double X,
    double A,
    double B,
    double Gamma,
    double DeflectionOverGr,
    double ShapiroOverGr,
    double CountingMeasureRatio,
    bool MetricNonDegenerate);

/// <summary>
/// ResearchY-G_029 — SPATIAL SECTOR CLOSURE AUDIT.
///
/// QUESTION. Can any spatial metric B(r) survive all existing constraints WITHOUT introducing a new primitive?
///
/// THE FRAMEWORK (A is no longer free — G_028 closed the clock on A = σ):
///     ds² = −e^(2A)dt² + e^(2B)(dr² + r²dΩ²) ,   A = σ = (1/d)ln ρ = −x
///     γ = −(e^(2B)−1)/(e^(2A)−1) ;   deflection and Shapiro both ∝ (1+γ)/2
///     counting measure: √det g_ij = ρ  ⟺  e^(3B) = ρ  ⟺  B = σ
///
/// WHAT THE REQUIREMENTS ACTUALLY CONSTRAIN. Requirements 1–4 (Newton limit, Earth clock, GPS, the
/// neutron-star audit) depend ONLY on A, which every candidate preserves. Requirements 5–7 (Cassini γ,
/// light deflection, Shapiro delay) depend ONLY on γ. So the eight requirements pin **one number**, not a
/// function — the entire nonlinear completion of B is free.
///
/// THE TWO EXTREMES.
///   B = σ   ⟹ γ = −1 exactly. Deflection 0, Shapiro 0, Cassini separation 8.7e4 σ. REFUTED.
///   B = −σ  ⟹ γ = 1 + 2x. Survives, but only to first order.
///   B = ½ln(2 − e^(2σ))  ⟹ γ = 1 EXACTLY at all orders — and it has the clean closed form
///                              **g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|**
///                          i.e. the spatial metric is the REFLECTION of the temporal one about 1, where the
///                          conformal (γ = −1) member has g_rr = |g₀₀|.
///
/// THE DEGENERACY. e^(2B) = 2 − e^(2A) &gt; 0 requires e^(2A) &lt; 2, i.e. **x &lt; ½ln 2 = 0.3465735903**, i.e.
/// R &gt; 2.8854 GM/c². Every OBSERVED neutron star is inside that range (J0740+6620 has x = 0.247002); the
/// deep strong field is not covered.
///
/// THE CRITICAL RESULT. AT derives no B. Its only equation for B — the counting measure — yields B = σ,
/// which is the observationally excluded member. The survivor is therefore selected by OBSERVATION, not by
/// the theory: the spatial sector closes on a POSTULATE.
/// </summary>
public static class SpatialSectorClosure
{
    public static readonly (string Body, double X)[] Bodies =
    {
        ("Cassini (solar)", 4.0e-6),
        ("Earth", 6.957e-10),
        ("Sun", 2.1225e-6),
        ("J0740+6620", 0.247002),
    };

    /// <summary>Cassini 2003: γ = 1.0000210 ± 2.3e−5.</summary>
    public const double CassiniGamma = 1.0000210;
    public const double CassiniUncertainty = 2.3e-5;

    /// <summary>σ = (1/d)ln ρ. With ρ = e^(−dx) this is −x.</summary>
    public static double Sigma(double x) => -x;

    /// <summary>A is FIXED by G_028's clock closure: A = σ.</summary>
    public static double AOf(double x) => Sigma(x);

    /// <summary>B = σ — the counting-measure member.</summary>
    public static double BConformal(double x) => Sigma(x);

    /// <summary>B = −σ — the linear member.</summary>
    public static double BNegSigma(double x) => -Sigma(x);

    /// <summary>B = ½ln(2 − e^(2σ)) — the unique member with γ = +1 at all orders (g_rr = 2 − ρ^(2/d)).</summary>
    public static double BExactGr(double x)
    {
        double e2 = Math.Exp(2.0 * Sigma(x));
        return e2 < 2.0 ? 0.5 * Math.Log(2.0 - e2) : double.NaN;
    }

    /// <summary>B = −σ + c₂σ².</summary>
    public static double BPolynomial(double x, double c2) => -Sigma(x) + c2 * Sigma(x) * Sigma(x);

    /// <summary>A rational/exponential family with B′(0) = −1: B = −σ·(1 + λ(1 − e^(−|σ|/s))).</summary>
    public static double BRationalExponential(double x, double lambda, double scale)
    {
        double s = Math.Abs(Sigma(x));
        return -Sigma(x) * (1.0 + lambda * (1.0 - Math.Exp(-s / scale)));
    }

    /// <summary>The exponent B for a family at a compactness.</summary>
    public static double BOf(SpatialFamily family, double x) => family switch
    {
        SpatialFamily.Conformal => BConformal(x),
        SpatialFamily.NegSigma => BNegSigma(x),
        SpatialFamily.ExactGr => BExactGr(x),
        SpatialFamily.Polynomial => BPolynomial(x, -2.0),
        _ => BRationalExponential(x, 0.25, 0.05),
    };

    /// <summary>Exact PPN γ = −(e^(2B) − 1)/(e^(2A) − 1).</summary>
    public static double GammaOf(double a, double b)
        => -((Math.Exp(2.0 * b) - 1.0) / (Math.Exp(2.0 * a) - 1.0));

    /// <summary>Deflection and Shapiro are both proportional to (1+γ)/2 — their ratio to GR.</summary>
    public static double DeflectionOverGr(double gamma) => (1.0 + gamma) / 2.0;

    /// <summary>Same factor for the Shapiro delay.</summary>
    public static double ShapiroOverGr(double gamma) => (1.0 + gamma) / 2.0;

    /// <summary>The counting-measure ratio √det g_ij / ρ = e^(3B)·e^(3x) at d = 3.</summary>
    public static double CountingMeasureRatio(double x, double b) => Math.Exp(3.0 * b) / Math.Exp(-3.0 * x);

    /// <summary>Cassini separation of a candidate's γ, in σ.</summary>
    public static double CassiniSeparation(double gamma)
        => Math.Abs(gamma - CassiniGamma) / CassiniUncertainty;

    /// <summary>
    /// Where e^(2B) = 2 − e^(2A) would vanish. With A = σ = −x this is A = +½ln2, i.e. **x = −½ln2** — a
    /// NEGATIVE compactness, i.e. a repulsive/negative-mass object. The exact member therefore has **NO
    /// degeneracy anywhere in the physical domain x &gt; 0**, and g_rr = 2 − ρ^(2/d) is bounded in (1, 2).
    /// </summary>
    public static double DegenerateX => -0.5 * Math.Log(2.0);

    /// <summary>The lower bound of g_rr on the exact member for x ≥ 0.</summary>
    public static double GrrLowerBound => 1.0;

    /// <summary>The upper bound of g_rr on the exact member as x → ∞.</summary>
    public static double GrrUpperBound => 2.0;

    /// <summary>Is the metric non-degenerate (g_rr &gt; 0) at a compactness?</summary>
    public static bool NonDegenerate(SpatialFamily family, double x)
    {
        double b = BOf(family, x);
        return !double.IsNaN(b) && Math.Exp(2.0 * b) > 0.0;
    }

    /// <summary>The computed case for a family at a compactness.</summary>
    public static SpatialCase CaseFor(SpatialFamily family, double x)
    {
        double a = AOf(x), b = BOf(family, x);
        double g = double.IsNaN(b) ? double.NaN : GammaOf(a, b);
        return new SpatialCase(family, x, a, b, g,
            double.IsNaN(g) ? double.NaN : DeflectionOverGr(g),
            double.IsNaN(g) ? double.NaN : ShapiroOverGr(g),
            CountingMeasureRatio(x, b), NonDegenerate(family, x));
    }

    /// <summary>Every family at every body.</summary>
    public static SpatialCase[] Table()
        => (from f in Enum.GetValues<SpatialFamily>()
            from b in Bodies
            select CaseFor(f, b.X)).ToArray();

    // ── The verdicts, COMPUTED from the observables (never typed) ─────────────

    /// <summary>
    /// The verdict for a family, DERIVED from its γ and its counting-measure ratio — so it cannot be a
    /// literal (ResearchY-G_027).
    /// </summary>
    public static string VerdictOf(SpatialFamily family)
    {
        var c = CaseFor(family, 4.0e-6);                      // the solar-system point Cassini probes
        if (!c.MetricNonDegenerate) return "REFUTED (metric degenerate)";
        if (CassiniSeparation(c.Gamma) > 5.0) return "REFUTED (Cassini)";
        bool grOptics = Math.Abs(c.Gamma - 1.0) < 1e-9;
        bool countingMeasure = Math.Abs(c.CountingMeasureRatio - 1.0) < 1e-9;
        if (countingMeasure && !grOptics) return "REFUTED (Cassini)";
        return grOptics ? "SURVIVES (exact GR optics)" : "SURVIVES (to first order)";
    }

    /// <summary>
    /// The unique B giving γ = +1 at ALL orders, as a closed form in ρ: **g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|**.
    /// </summary>
    public static double GrrOfExactMember(double rho) => 2.0 - Math.Pow(rho, 2.0 / 3.0);

    /// <summary>The conformal member's g_rr, for contrast: g_rr = ρ^(2/d) = |g₀₀|.</summary>
    public static double GrrOfConformalMember(double rho) => Math.Pow(rho, 2.0 / 3.0);

    /// <summary>
    /// How tightly Cassini constrains the quadratic coefficient of B = −σ + c₂σ². Solving
    /// |γ − γ_Cassini| ≤ 3σ_Cassini gives |c₂ + 2| ≤ this.
    /// </summary>
    public static double CassiniAllowedC2Deviation()
        => (CassiniGamma - 1.0 + 3.0 * CassiniUncertainty) / Bodies[0].X;

    /// <summary>Was every admissible B DERIVABLE? Only B = σ is, and it is excluded — so: no.</summary>
    public static bool AnyDerivableBSurvives()
        => Math.Abs(CassiniSeparation(CaseFor(SpatialFamily.Conformal, 4.0e-6).Gamma)) <= 5.0;
}
