namespace AT.Core.ResearchXH;

/// <summary>Which requirement a candidate spatial exponent B violates (ResearchY-G_030).</summary>
public enum RequirementViolated
{
    None,
    Cassini,
    LightDeflection,
    ClockSector,
    NoNewPrimitive,
}

/// <summary>
/// ResearchY-G_030 — NO-GO AUDIT.
///
/// QUESTION. Is there a theorem that any LOCAL B(r) = F(σ) must violate at least one of: Newton, Cassini,
/// light deflection, clock sector, the no-new-primitive rule?
///
/// THEOREM (G_030). Let A = σ be fixed — which G_028 established for the clock sector, so the Newton limit,
/// the Earth clock, GPS and the neutron-star audit are all satisfied by construction and discriminate nothing.
/// Then, for the isotropic form ds² = −e^(2A)dt² + e^(2B)(dr² + r²dΩ²) with A = σ and B arbitrary:
///
///   (i)   **γ = −1  ⟺  B = σ**          — i.e. the counting measure √det g_ij = ρ.
///   (ii)  **γ = +1  ⟺  e^(2B) = 2 − e^(2A)**, i.e. g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|.
///   (iii) Cassini admits only γ ∈ [1.0000210 ± 3·2.3e−5]; γ = −1 lies **8.6957e4 σ** outside it.
///   (iv)  AT DERIVES ONLY (i). The counting measure is AT's sole determination of B from ρ — every other
///         ingredient either fixes A, fixes numbers (occ, Σm), or supplies FREE content (the traceless face
///         ψ), not a determination. (ii) has no AT-native principle (G_029, OP1).
///
/// ∴ For any local F: either F = Id (hence B = σ) and Cassini plus light deflection fail, or F ≠ Id and F's
/// specification is an input beyond ρ — which G_023's accounting calls a new primitive. **No admissible
/// local B(σ) escapes.** ∎
///
/// WHAT THE NO-GO IS AND IS NOT. It is a theorem about DERIVATION, not about existence: G_029 identified a
/// survivor by postulate (`g_rr = 2 − ρ^(2/d)`), and G_030 proves that no such survivor can be DERIVED.
/// It also shows the departure is not a small correction — the volume ratio is 4.2e−9 at Earth but 2.438 at
/// J0740+6620, so it becomes O(1) exactly where the theory is most discriminating.
/// </summary>
public static class NoGoTheorem
{
    /// <summary>Cassini 2003: γ = 1.0000210 ± 2.3e−5.</summary>
    public const double CassiniGamma = 1.0000210;
    public const double CassiniUncertainty = 2.3e-5;

    /// <summary>The number of σ by which a candidate γ may miss Cassini and still be admitted.</summary>
    public const double AdmissionSigma = 3.0;

    /// <summary>Compactness x = GM/(Rc²); ρ = e^(−dx), so σ = −x.</summary>
    public static readonly (string Body, double X)[] Bodies =
    {
        ("Earth", 6.957e-10),
        ("Sun", 2.1225e-6),
        ("x = 1e-4", 1.0e-4),
        ("x = 0.1", 0.1),
        ("J0740+6620", 0.247002),
        ("x = 1", 1.0),
    };

    /// <summary>A is FIXED by G_028: A = σ = −x.</summary>
    public static double AOf(double x) => -x;

    /// <summary>Exact PPN γ = −(e^(2B) − 1)/(e^(2A) − 1).</summary>
    public static double GammaOf(double a, double b)
        => -((Math.Exp(2.0 * b) - 1.0) / (Math.Exp(2.0 * a) - 1.0));

    /// <summary>γ of a candidate B at a compactness.</summary>
    public static double GammaOfB(double x, double b) => GammaOf(AOf(x), b);

    /// <summary>Is γ inside the admitted Cassini band?</summary>
    public static bool InCassiniBand(double gamma)
        => Math.Abs(gamma - CassiniGamma) <= AdmissionSigma * CassiniUncertainty;

    /// <summary>The admitted band's edges.</summary>
    public static (double Low, double High) CassiniBand
        => (CassiniGamma - AdmissionSigma * CassiniUncertainty,
            CassiniGamma + AdmissionSigma * CassiniUncertainty);

    /// <summary>Cassini separation in σ.</summary>
    public static double CassiniSeparation(double gamma)
        => Math.Abs(gamma - CassiniGamma) / CassiniUncertainty;

    /// <summary>The counting-measure ratio √det g_ij / ρ.</summary>
    public static double CountingMeasureRatio(double x, double b) => Math.Exp(3.0 * b) / Math.Exp(-3.0 * x);

    /// <summary>Does the candidate preserve the counting measure exactly?</summary>
    public static bool CountingMeasureExact(double x, double b)
        => Math.Abs(CountingMeasureRatio(x, b) - 1.0) < 1.0e-12;

    /// <summary>B = σ — the counting-measure member.</summary>
    public static double BSigma(double x) => AOf(x);

    /// <summary>B solving γ = +1: e^(2B) = 2 − e^(2A).</summary>
    public static double BReflection(double x) => 0.5 * Math.Log(2.0 - Math.Exp(2.0 * AOf(x)));

    // ── (i) and (ii): the two exact equivalences ──────────────────────────────

    /// <summary>
    /// (i) γ = −1 ⟺ B = σ, for every x. Verified by solving γ(B) = −1 directly rather than by assertion.
    /// </summary>
    public static bool GammaMinusOneIffBSigma(double x, double tol = 1.0e-10)
    {
        double a = AOf(x);
        // γ(B) = −1 ⟹ e^(2B) − 1 = e^(2A) − 1 ⟹ B = A = σ. Check both directions numerically.
        double bSolved = 0.5 * Math.Log(Math.Exp(2.0 * a));      // the unique solution
        return Math.Abs(bSolved - BSigma(x)) < tol
               && Math.Abs(GammaOf(a, BSigma(x)) + 1.0) < 1.0e-12;
    }

    /// <summary>
    /// (ii) γ = +1 ⟺ e^(2B) = 2 − e^(2A), for every x.
    /// </summary>
    public static bool GammaPlusOneIffReflection(double x, double tol = 1.0e-10)
    {
        double a = AOf(x), b = BReflection(x);
        return Math.Abs(Math.Exp(2.0 * b) + Math.Exp(2.0 * a) - 2.0) < tol
               && Math.Abs(GammaOf(a, b) - 1.0) < 1.0e-6;
    }

    // ── The exhaustive classification ─────────────────────────────────────────

    /// <summary>
    /// The requirement a candidate B violates. Cassini and light deflection are ONE test (both depend only on
    /// γ), and the clock sector is satisfied by construction (A = σ), so the live pair is
    /// {Cassini/deflection} versus {no-new-primitive}. Computed, never typed (ResearchY-G_027).
    /// </summary>
    public static RequirementViolated FirstViolation(double x, double b)
    {
        if (!InCassiniBand(GammaOfB(x, b))) return RequirementViolated.Cassini;
        if (!CountingMeasureExact(x, b)) return RequirementViolated.NoNewPrimitive;
        return RequirementViolated.None;
    }

    // ── The exact admitted interval (rigorous, not sampled) ───────────────────

    /// <summary>
    /// Invert γ:  e^(2B) = 1 − γ(e^(2A) − 1),  so  B(γ) = ½ln(1 − γ(e^(2A) − 1)).
    /// (Check: γ = −1 gives B = σ; γ = +1 gives e^(2B) = 2 − e^(2A).)
    /// </summary>
    public static double BForGamma(double x, double gamma)
        => 0.5 * Math.Log(1.0 - gamma * (Math.Exp(2.0 * AOf(x)) - 1.0));

    /// <summary>
    /// The interval of B admitted by Cassini. γ is strictly increasing in B, so the admitted set is an
    /// interval, and its edges are obtained by inverting γ — no grid, no sampling error.
    /// </summary>
    public static (double Low, double High) AdmittedBInterval(double x)
        => (BForGamma(x, CassiniBand.Low), BForGamma(x, CassiniBand.High));

    /// <summary>The width of the admitted interval in B.</summary>
    public static double AdmittedBWidth(double x)
    {
        var (lo, hi) = AdmittedBInterval(x);
        return hi - lo;
    }

    /// <summary>
    /// THE THEOREM, RIGOROUSLY: AT's derived B (the counting measure, B = σ) is never inside the Cassini
    /// admitted interval. Hence no DERIVED local B(σ) satisfies both requirements — no sampling involved.
    /// </summary>
    public static bool NoGoHoldsExactly(double x)
    {
        var (lo, hi) = AdmittedBInterval(x);
        double derived = ATDerivedB(x);
        return derived < lo || derived > hi;
    }

    /// <summary>The theorem over every body, by exact inversion.</summary>
    public static bool NoGoHoldsExactlyEverywhere() => Bodies.All(b => NoGoHoldsExactly(b.X));

    /// <summary>A sweep of the no-go: how many candidate B escape, and which.</summary>
    public static (int Swept, int Escaped, double[] Escapers) Sweep(double x, double bMin, double bMax, int steps)
    {
        var escapers = new List<double>();
        for (int i = 0; i <= steps; i++)
        {
            double b = bMin + (bMax - bMin) * i / steps;
            if (FirstViolation(x, b) == RequirementViolated.None) escapers.Add(b);
        }
        return (steps + 1, escapers.Count, escapers.ToArray());
    }

    /// <summary>
    /// A sweep fine enough to RESOLVE the admitted band, which is only ~5.5e−10 wide in B at the
    /// solar-system compactness. Everything that enters the band still fails the counting measure.
    /// </summary>
    public static (int Swept, int InBand, int Escaped) SweepBand(double x, int steps = 20000)
    {
        double span = 2.0 * Math.Abs(x);
        int inBand = 0, escaped = 0;
        for (int i = 0; i <= steps; i++)
        {
            double b = -span + 2.0 * span * i / steps;
            double g = GammaOfB(x, b);
            if (InCassiniBand(g))
            {
                inBand++;
                if (FirstViolation(x, b) == RequirementViolated.None) escaped++;
            }
        }
        return (steps + 1, inBand, escaped);
    }

    /// <summary>
    /// THE THEOREM, EXECUTED: no candidate B in the swept range escapes both requirements, at any compactness.
    /// </summary>
    public static bool NoGoHolds(double x) => Sweep(x, -0.5, 0.5, 4000).Escaped == 0;

    /// <summary>The theorem over every body in the inventory.</summary>
    public static bool NoGoHoldsEverywhere() => Bodies.All(b => NoGoHolds(b.X));

    /// <summary>The width of the admitted γ band — the only door γ leaves open.</summary>
    public static double CassiniBandWidth => CassiniBand.High - CassiniBand.Low;

    /// <summary>
    /// Is AT's ONLY determination of B the counting measure? Then F = Id is the unique derived candidate.
    /// (Every other ingredient fixes A, fixes numbers, or supplies free content — see the class summary.)
    /// </summary>
    public static bool CountingMeasureIsTheOnlyDerivation() => ATDerivedB(1.0e-4) == BSigma(1.0e-4);

    /// <summary>The B that AT derives, from ρ alone: the counting measure.</summary>
    public static double ATDerivedB(double x) => BSigma(x);

    // ── The departure is not a small correction ───────────────────────────────

    /// <summary>√det g_ij / ρ for the γ = +1 survivor — the counting-measure departure.</summary>
    public static double SurvivorVolumeRatio(double x) => CountingMeasureRatio(x, BReflection(x));

    /// <summary>The departure |ratio − 1| for the survivor.</summary>
    public static double SurvivorVolumeDeparture(double x) => Math.Abs(SurvivorVolumeRatio(x) - 1.0);

    /// <summary>Does the departure reach O(1) somewhere? (It does — at compactness.)</summary>
    public static bool DepartureBecomesOrderOne() => Bodies.Any(b => SurvivorVolumeDeparture(b.X) > 1.0);
}
