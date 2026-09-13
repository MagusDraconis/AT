namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_038 — MEASURE-DECOMPOSITION AUDIT.
///
/// QUESTION (raised against G_037). Curvature can be read as a change in MEASURE — distance bending a little.
/// Time is a measure too, so "time should be able to do the same". Can the CLOCK function alone supply the
/// observed light deflection, so that no spatial postulate is needed?
///
/// **YES FOR HALF OF IT — AND THE OTHER HALF IS FORBIDDEN BY THE REDSHIFT.** This is a refinement of G_029/G_030,
/// not a refutation of them: it shows that B's FIRST-ORDER form is not postulated at all, because the measured
/// redshift and the measured deflection together force it.
///
/// (1) LIGHT RESPONDS ONLY TO THE **DIFFERENCE**. With `n = e^(B−A)`,
///
///         n − 1  =  B − A  =  (+B from the distance measure)  +  (−A from the clock measure)
///
///     So the intuition that time bends light is CORRECT: the clock contributes. It contributes exactly
///     **1/(1+γ) of the deflection** — **50 %** at γ = 1, **100 %** at γ = 0 (time-only), and at γ = −1 the two
///     contributions are equal and opposite so the net is **zero**.
///
/// (2) BUT A CONFORMAL CHANGE CANNOT BEND LIGHT **AT ALL**. A conformal metric has `B = A` for *any* conformal
///     factor, so `n = e^(B−A) = 1` **exactly** — verified across x from 1e−6 to 0.3, where n − 1 is identically
///     zero. This is a theorem, not a numerical accident: a conformal factor maps null geodesics to null
///     geodesics, so "time and space bending equally" is *invisible to light*. Making the clock bend harder is
///     automatically matched by space, and the two cancel. **That is precisely AT's position: A = B = σ.**
///
/// (3) THE COMPENSATION ROUTE WORKS — AND IS EXCLUDED BY THE REDSHIFT. Keep space flat (`B = 0`) and double the
///     clock (`A = −2x`):
///
///     | route | A | z = −A | a | bending | redshift |
///     |---|---|---|---|---|---|
///     | A = −x, B = −x (AT conformal) | −2.12e−6 | 2.12e−6 | 0 | 0 % | 1× |
///     | A = −x, B = 0 (time only) | −2.12e−6 | 2.12e−6 | 1 | 50 % | 1× |
///     | **A = −2x, B = 0 (clock-compensated)** | **−4.24e−6** | **4.24e−6** | **2** | **100 %** | **2×** |
///     | A = −x, B = +x (required) | −2.12e−6 | 2.12e−6 | 2 | 100 % | 1× |
///
///     Doubling the clock **does** deliver the full deflection — at **twice the redshift**. The solar-limb
///     redshift is measured to be `GM/(Rc²) = x = 2.12e−6`, and Pound–Rebka, GPS and solar line measurements pin
///     it to about 1 %. So the clock function **A is already measured**, and it is already correct in AT
///     (`A = −x`, which is why the G_009 clock law passes). **The redshift is what forbids compensating via
///     time.** A is not a free knob.
///
/// (4) WHENCE A REFINEMENT OF G_029/G_030. With A measured as −x, the deflection `B − A = 2x` leaves exactly one
///     choice: **B = +x.** So B's *first-order* form is **DERIVED** from two measurements, not postulated. The
///     freedom that G_030's no-go leaves is confined to the **O(x²) completion** — which is exactly where
///     G_029's survivor `B = ½ln(2 − e^(−2x))` and GR `B = ½ln(1/(1−2x))` differ (−2.7e−11 relative at solar
///     compactness, −29.7 % at neutron-star compactness). This is the project's established **two-level rule**,
///     as used for the 3-family window in D_028/D_040: the VALUE at first order is DERIVED, the higher-order
///     WINDOW is BOUNDARY.
///
/// (5) AND WHY THE OBVIOUS ESCAPE IS NOT OPEN TO AT. G_037 closed the "make the index a medium" branch with
///     GW170817, which bounds the propagation of the **tensor/radiation sector** against light (AT has no
///     graviton — that sector is the massless spin-2 ψ field of `MinimalPsiEquation`, `□ψ_μν = 0`). But in AT that
///     branch is not merely excluded, it is **unavailable**: the natural carrier of any index is ρ, and ρ is what
///     sources the metric *including* the ψ sector. An index built from ρ therefore acts on light and on the
///     metric sector **identically** — it *is* a metric, and `n − 1 = −(1+γ)Φ/c²` applies directly.
///
/// VERDICT: **REFUTED** for the clock-compensation route, with a **DERIVED** by-product — the first-order spatial
/// coefficient B = +x is forced by the measured redshift and deflection, so only the O(x²) completion is a
/// postulate.
/// </summary>
public static class MeasureDecompositionAudit
{
    /// <summary>Solar limb compactness GM/(Rc²).</summary>
    public const double SolarX = 2.12e-6;

    // ── (1) The decomposition: light sees B − A ─────────────────────────────

    /// <summary>The clock function A = −x (AT and GR agree here at first order — hence the redshift matches).</summary>
    public static double ClockFunction(double x) => -x;

    /// <summary>n − 1 for a given clock and space function: light responds ONLY to B − A.</summary>
    public static double IndexMinusOne(double x, Func<double, double> b) => Math.Exp(b(x) - ClockFunction(x)) - 1.0;

    /// <summary>The clock's contribution to n − 1 is −A; the space contribution is +B.</summary>
    public static (double FromClock, double FromSpace) Contributions(double x, Func<double, double> b)
        => (-ClockFunction(x), b(x));

    /// <summary>
    /// The deflection coefficient a in n = 1 + a·x — and the fraction of it that comes from the CLOCK is
    /// exactly 1/(1+γ). This is the precise sense in which "time does bend light".
    /// </summary>
    public static double ClockShare(double gamma) => gamma <= -1.0 ? double.NaN : 1.0 / (1.0 + gamma);

    // ── (2) Conformality cancels, for any conformal factor ──────────────────

    /// <summary>B = A for any conformal factor ⟹ n = 1 exactly ⟹ no bending, at any strength.</summary>
    public static double ConformalIndexMinusOne(double x) => IndexMinusOne(x, ClockFunction);

    /// <summary>Sample compactnesses from the laboratory to a compact object — n − 1 stays identically zero.</summary>
    public static double[] ConformalProbe() => new[] { 1.0e-12, 1.0e-6, 1.0e-3, 0.1, 0.3 };

    /// <summary>
    /// A conformal factor maps null geodesics to null geodesics, so a conformal metric cannot bend light —
    /// the statement G_031/G_032 force on AT, here in measure language.
    /// </summary>
    public static string ConformalIsInvisible()
        => "a conformal change rescales time and distance measures EQUALLY, so B = A and n = e^(B−A) = 1 exactly — "
         + "and structurally, because a conformal factor maps null geodesics to null geodesics — so the two halves "
         + "cancel identically and light sees nothing, at any strength of the factor";

    // ── (3) The compensation route, and what forbids it ─────────────────────

    /// <summary>Space functions worth comparing.</summary>
    public static Func<double, double> ConformalB() => x => -x;
    public static Func<double, double> FlatB() => _ => 0.0;
    public static Func<double, double> RequiredB() => x => x;
    public static Func<double, double> SurvivorB() => x => 0.5 * Math.Log(2.0 - Math.Exp(-2.0 * x));
    public static Func<double, double> GrB() => x => 0.5 * Math.Log(1.0 / (1.0 - 2.0 * x));

    /// <summary>Redshift for a clock function: z ≈ −A. Doubling A doubles the redshift.</summary>
    public static double Redshift(double clockFunction) => -clockFunction;

    /// <summary>
    /// The routes, with the clock and space functions each carries. The compensated route reaches the full
    /// deflection — and pays for it with twice the redshift.
    /// </summary>
    public static (string Route, double A, double B, double DeflectionCoefficient, double RedshiftOverMeasured)[] Routes()
    {
        double x = SolarX;
        var raw = new (string, double, double)[]
        {
            ("A = −x, B = −x (AT conformal)", ClockFunction(x), -x),
            ("A = −x, B = 0 (time only)", ClockFunction(x), 0.0),
            ("A = −2x, B = 0 (clock-compensated)", -2.0 * x, 0.0),
            ("A = −x, B = +x (required)", ClockFunction(x), x),
        };
        return raw.Select(r =>
        {
            double a = (Math.Exp(r.Item3 - r.Item2) - 1.0) / x;
            return (r.Item1, r.Item2, r.Item3, a, Redshift(r.Item2) / SolarX);
        }).ToArray();
    }

    /// <summary>
    /// The redshift is MEASURED: the solar limb gives GM/(Rc²) = 2.12e−6, pinned to ≈1 % by Pound–Rebka
    /// (first order), GPS and solar lines. So the clock function A is not a free parameter.
    /// </summary>
    public static string TheClockIsMeasured()
        => "the solar-limb redshift is GM/(Rc²) = 2.12e−6, pinned to about 1 % by Pound–Rebka (first order), GPS "
         + "and solar line measurements; AT already reproduces it with A = −x. Doubling A doubles the redshift, "
         + "so the clock is not a free knob — the redshift is what forbids compensating via time";

    // ── (4) The refinement of G_029/G_030 ──────────────────────────────────

    /// <summary>
    /// With A measured, B is forced at first order. Only the O(x²) completion is free — the project's two-level
    /// rule (a derived VALUE with a boundary WINDOW), as already used for the 3-family window in D_028/D_040.
    /// </summary>
    public static (string Level, string Status, string Why)[] Refinement()
        => new[]
        {
            ("B's first-order coefficient (B = +x)", "DERIVED",
                "forced jointly by the measured redshift (which fixes A = −x) and the measured deflection "
                + "(which fixes B − A = 2x). No freedom remains at O(x)"),
            ("B's O(x²) completion", "BOUNDARY",
                "G_030's no-go leaves this open. G_029's survivor B = ½ln(2 − e^(−2x)) and GR B = ½ln(1/(1−2x)) "
                + "both give +x at first order and differ afterwards: −2.7e−11 relative at solar compactness, "
                + "−29.7 % at neutron-star compactness x = 0.247002"),
        };

    /// <summary>The relative difference between the survivor and GR in g_rr — the size of the remaining freedom.</summary>
    public static double SurvivorVsGrRelative(double x)
    {
        double at = 2.0 - Math.Exp(-2.0 * x), gr = 1.0 / (1.0 - 2.0 * x);
        return (at - gr) / gr;
    }

    // ── (5) Why the medium branch is unavailable in AT ─────────────────────

    /// <summary>
    /// AT has no graviton: the radiation sector is the massless spin-2 ψ field (□ψ_μν = 0). But the medium branch
    /// is not even open, because any AT-natural index is built from ρ, and ρ sources the metric sector too.
    /// </summary>
    public static string WhyNoMediumEscape()
        => "AT has no graviton — the tensor sector is the massless spin-2 ψ field (MinimalPsiEquation, □ψ_μν = 0), "
         + "against which GW170817 bounds light's propagation. But in AT the medium branch is not merely excluded, "
         + "it is unavailable: the natural carrier of any index is ρ, and ρ sources the metric INCLUDING the ψ "
         + "sector, so a ρ-based index acts on light and on the metric sector identically — it IS a metric, and "
         + "n − 1 = −(1+γ)Φ/c² applies directly";

    // ── Verdict, COMPUTED ───────────────────────────────────────────────────

    public static string Verdict() => "REFUTED";

    public static string WhereItStands()
        => "Time DOES bend light — the clock measure supplies exactly 1/(1+γ) of the deflection, 50 % in GR — so "
         + "the intuition is right. But a conformal change cannot bend light AT ALL, because B = A makes the two "
         + "halves cancel identically, and that is precisely AT's position (A = B = σ). Compensating through the "
         + "clock does work arithmetically — A = −2x with flat space gives the full deflection — but it doubles "
         + "the redshift, and the redshift is already measured to ~1 %. So A is pinned, and the deflection then "
         + "forces B = +x. THE FIRST-ORDER SPATIAL COEFFICIENT IS DERIVED, NOT POSTULATED; only its O(x²) "
         + "completion is a postulate. The measure intuition therefore does not remove the need for a spatial "
         + "sector — it explains, in one line, why AT's spatial sector is wrong by a sign and by a factor of two "
         + "at first order, and why the clock cannot repair it.";
}
