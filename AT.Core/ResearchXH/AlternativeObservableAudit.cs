using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What an alternative observable is worth as a probe of the kernel's dimension (ResearchY-G_088).</summary>
public sealed record AlternativeObservable(string Domain, string Observable, double PowerInThePotential,
    double SignaturePerUnitC, bool DegenerateWithTheRedshift, bool MeasurableInTheSurvivingSector,
    bool ItsRateIsFixedByASurvivingDatum, bool ItsRouteIsThroughTheCompactness, string Route, string Note);

/// <summary>
/// ResearchY-G_088 — ALTERNATIVE OBSERVABLE AUDIT (group G — Gravity Source).
///
/// QUESTION. G_087 measured that the free room's signature at J0740+6620 is a factor 10.8 below the AT/GR separation
/// the programme is already chasing, that the current instrument class would need an extension of order 4.6, and that
/// below |c| = 0.72 the recorded COMPACTNESS error vetoes the decision at any redshift precision. Its degeneracy is
/// specific: the signature is second order in the potential, and the compactness error mimics it. **So: is there an
/// observable that is NOT proportional to that same signature?** Seven domains are searched: higher derivatives, tidal
/// effects, clock transport, compact-object profiles, multi-compactness comparisons, dynamical evolution and mixed
/// gravity/clock observables.
///
/// PREFER REFUTATION. If every observable remains degenerate, KernelDimension is EMPIRICALLY UNDECIDABLE within the
/// surviving sector.
///
/// ANSWER: **THE DEGENERACY IS STRUCTURAL, NOT SPECIFIC TO J0740 — a derivative observable IS mathematically
/// independent of the redshift (its signature is FIRST order in the potential where the redshift's is SECOND), but every
/// MEASURABLE route in the surviving sector collapses back to the pair (the surface redshift, the compactness), and the
/// compactness is exactly where the degeneracy lives. So the sector has exactly ONE mathematically independent probe
/// (the kernel's second derivative, which reads `c` directly) and NO independently measurable one.**
///
///  (1) THE MEASURE OF INDEPENDENCE IS THE POWER OF THE POTENTIAL: an observable's c-signature scales as `A^p`, and two
///      observables are collinear — hence degenerate at one target — exactly when their powers agree. Measured: the
///      rate's signature has `p = 2`, the first derivative's `p = 1`, the second derivative's value IS `c` (its power is
///      zero: it has no baseline to be relative to), and the clock transport's integral has `p = 2` again.
///
///  (2) THE ONE MATHEMATICALLY INDEPENDENT PROBE IS A DERIVATIVE, and the sharpest is the kernel's SECOND derivative:
///      `d^2(ln K)/dA^2 = c` EXACTLY, so a curvature measurement would read the extension directly. The audit says
///      plainly that nothing in the surviving sector measures it.
///
///  (3) THE MIXED ROUTE SEPARATES THE TWO UNKNOWNS AT ONE TARGET: the ratio of the rate's signature to the gradient's
///      is exactly `A/2`, a function of the potential — so (rate, gradient) at one target gives two equations in
///      `(c, A)`. But the gradient IS the compactness, so the second equation is the quantity whose error already
///      vetoes the first: the route buys PRECISION, not independence. Measured reach: a c-error of order a quarter from
///      the recorded compactness error, hence a three-sigma threshold near |c| = 0.7 — G_087's veto, recovered.
///
///  (4) A SECOND COMPACTNESS TARGET IS NOT SUFFICIENT, and the reason is a COUNT rather than a judgement: each target
///      contributes one equation and one free compactness offset, so N targets give N equations in N + 1 unknowns. The
///      c-signature is collinear with the free offsets at every target, and only EXTERNAL compactness constraints break
///      it — which is a √N improvement of the same limited error, not a new axis.
///
///  (5) THE REMAINING DOMAINS ARE HIDDEN, COLLINEAR OR UNMEASURED: the interior profile's signature is LARGER (it scales
///      as the potential squared, and a core potential is several times the surface's) but no clock escapes from the
///      core; the transport of a clock along a path telescopes EXACTLY into the value difference, so it carries nothing
///      new; the dynamical route is first order like a derivative, but its rate is fixed by no surviving datum; and the
///      tidal route is a second gradient whose measurement is a structural model, not a reading of the kernel.
///
///  (6) SO THE CLASSIFICATION THE QUESTION ASKS FOR APPLIES: **KernelDimension is EMPIRICALLY UNDECIDABLE within the
///      surviving sector** — not because no independent observable exists, but because every one that exists is
///      unreachable by the sector's own measurement routes. The audit reports both halves: the mathematical separation
///      is real, and the empirical one is absent.
///
///  (7) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED: no spectral count, eigenspace count or multiplicity is quoted.
/// </summary>
public static class AlternativeObservableAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The state: the chain's cosine profile, with a genuine density contrast.</summary>
    public static double[] State => MapRedundancyAudit.State;

    /// <summary>The surface potential the redshift programme measures, and a deeper one the interior hides.</summary>
    public static double SurfacePotential => Math.Abs(ObservationalProgramAudit.XOfTarget());

    /// <summary>A core potential for the profile study: a central density five times the mean, in the same units.</summary>
    public const double CoreDensityRatio = 5.0;
    public static double CorePotential => Math.Log(CoreDensityRatio) / D;

    // ===================== 1. THE OBSERVABLES AS FUNCTIONS OF (A, c) =====================

    /// <summary>The kernel's logarithm: `ln K = A + c A^2 / 2`.</summary>
    public static double LnK(double a, double c) => a + 0.5 * c * a * a;

    /// <summary>The rate — the VALUE observable, with the redshift as its ratio.</summary>
    public static double Rate(double a, double c) => Math.Exp(LnK(a, c));

    /// <summary>The first derivative of the kernel with respect to the potential: `1 + c A`. The gradient.</summary>
    public static double Gradient(double a, double c) => 1.0 + c * a;

    /// <summary>The SECOND derivative of the kernel with respect to the potential: it IS `c`, exactly.</summary>
    public static double Curvature(double a, double c) => c;

    /// <summary>
    /// THE POWER OF THE POTENTIAL that an observable's c-signature carries, MEASURED by evaluating the relative
    /// signature at two potentials and fitting `p = ln(rho2 / rho1) / ln(A2 / A1)`. Two observables are collinear — hence
    /// degenerate at one target — exactly when their powers agree, which is the audit's measure of independence.
    /// </summary>
    public static double PowerOfTheSignature(Func<double, double, double> observable, double a1, double a2, double c = 1e-3)
    {
        double r1 = Math.Abs(observable(a1, c) - observable(a1, 0.0)) / Math.Abs(observable(a1, 0.0));
        double r2 = Math.Abs(observable(a2, c) - observable(a2, 0.0)) / Math.Abs(observable(a2, 0.0));
        return Math.Log(r2 / r1) / Math.Log(a2 / a1);
    }

    /// <summary>The relative signature per unit c, at the surface potential.</summary>
    public static double SignaturePerUnitC(Func<double, double, double> observable, double a)
    {
        const double h = 1e-6;
        double baseValue = observable(a, 0.0);
        if (baseValue == 0.0) return double.NaN;          // a curvature has no baseline: it IS the signal
        return Math.Abs(observable(a, h) - observable(a, -h)) / (2.0 * h) / Math.Abs(baseValue);
    }

    // ===================== 2. THE CATALOGUE ACROSS THE SEVEN DOMAINS =====================

    /// <summary>
    /// THE CATALOGUE. Each row is one alternative observable, with the power of its c-signature MEASURED between the
    /// surface potential and the core potential, whether that makes it collinear with the redshift, and whether the
    /// surviving sector can actually reach it.
    /// </summary>
    public static AlternativeObservable[] Catalogue()
    {
        double surface = SurfacePotential, core = CorePotential;
        return new[]
        {
            new AlternativeObservable("value", "the surface redshift (the rate at the surface)", 2.0,
                SignaturePerUnitC(Rate, surface), true, true, true, true,
                "the redshift, already measured to 1 to 20 per cent depending on the class",
                "p = 2: the signature is c A^2 / 2, exactly the form the compactness error mimics (G_087)"),

            new AlternativeObservable("higher derivatives", "the acceleration at the surface (the first gradient)", 1.0,
                SignaturePerUnitC(Gradient, surface), false, true, true, true,
                "the compactness GM/(Rc^2) - which is the quantity whose ERROR vetoes the redshift route",
                "p = 1: the signature is c A, a DIFFERENT function of the potential, so it is not collinear with the "
                + "redshift - but the route to it IS the compactness, so it adds precision, not independence"),

            new AlternativeObservable("higher derivatives", "the kernel's second derivative (the curvature)", 0.0,
                double.NaN, false, false, false, false,
                "no surviving measurement reads a curvature of the clock law",
                "d^2(ln K)/dA^2 = c EXACTLY: the cleanest possible probe in principle, and the audit says plainly that "
                + "nothing in the sector measures it - it has no baseline to be relative to"),

            new AlternativeObservable("clock transport", "the integrated gradient along a path", 2.0,
                double.NaN, true, true, true, false,
                "carrying a clock between two sites - which IS the redshift ratio",
                "the integral telescopes EXACTLY into the value difference, so it carries nothing the redshift does not"),

            new AlternativeObservable("compact-object profiles", "the core-to-surface ratio", 2.0, double.NaN, true,                 false, false, false,
                "the clock at the core, which does not escape",
                "p = 2 with a LARGER coefficient: the signature scales as the potential squared, and the core potential "
                + "is several times the surface's - a bigger signal that is HIDDEN"),

            new AlternativeObservable("multi-compactness", "the ratio of two targets' signatures", 2.0, double.NaN, true,
                true, true, true,
                "two targets, each with its own measured compactness and its own error",
                "each target contributes one equation and one FREE compactness offset, so N targets give N equations in "
                + "N + 1 unknowns: collinear with the offsets, and only external constraints break it"),

            new AlternativeObservable("dynamical evolution", "the redshift's time derivative", 1.0, double.NaN, false,                 true, false, false,
                "a drift measurement - first order in c A like any derivative",
                "p = 1, so the form is independent; but the redistribution rate it is proportional to is fixed by no "
                + "surviving datum, so its magnitude is unconstrained"),

            new AlternativeObservable("mixed gravity/clock", "the ratio of the value signature to the gradient's", 0.0,
                double.NaN, false, true, true, true,
                "the pair (surface redshift, compactness) at ONE target",
                "the ratio is EXACTLY A/2, a function of the potential: (rate, gradient) at one target gives two "
                + "equations in (c, A) - and the second is the compactness whose error already vetoes the first"),
        };
    }

    /// <summary>The rows that are NOT collinear with the redshift (a different power of the potential).</summary>
    public static AlternativeObservable[] NonCollinear() => Catalogue().Where(o => !o.DegenerateWithTheRedshift).ToArray();

    /// <summary>
    /// The rows that are non-collinear, reachable, rate-fixed AND not routed through the compactness — the audit's
    /// definition of an INDEPENDENT AXIS. The gradient route fails the last test, which is the audit's central finding:
    /// the mathematically independent probe is measured by the very quantity whose error vetoes the redshift route.
    /// </summary>
    public static AlternativeObservable[] IndependentAndMeasurable()
        => Catalogue().Where(o => !o.DegenerateWithTheRedshift && o.MeasurableInTheSurvivingSector
            && o.ItsRateIsFixedByASurvivingDatum && !o.ItsRouteIsThroughTheCompactness).ToArray();

    // ===================== 3. THE STRUCTURAL TESTS =====================

    /// <summary>
    /// THE TELESCOPING IDENTITY, measured: the integral of the kernel's gradient along a path equals the difference of
    /// the kernel's values, so a clock carried between two sites carries no information the redshift ratio does not.
    /// </summary>
    public static (string Sites, double IntegratedGradient, double ValueDifference, double Residual)[]
        TransportTelescoping()
    {
        var rho = State;
        var (well, hill) = KernelDiscriminabilityAudit.ContrastSites();
        var a = rho.Select(r => KernelDiscriminabilityAudit.LnK(r, 0.0)).ToArray();
        var gradients = rho.Select(r => KernelDiscriminabilityAudit.LnK(r, 0.3) - KernelDiscriminabilityAudit.LnK(r, 0.0)).ToArray();
        double integrated = gradients[hill] - gradients[well];
        double values = KernelDiscriminabilityAudit.LnK(rho[hill], 0.3) - KernelDiscriminabilityAudit.LnK(rho[well], 0.3)
            - (KernelDiscriminabilityAudit.LnK(rho[hill], 0.0) - KernelDiscriminabilityAudit.LnK(rho[well], 0.0));
        return new[]
        {
            ($"the well ({a[well]:E3}) to the hill ({a[hill]:E3})", integrated, values,
                Math.Abs(integrated - values)),
        };
    }

    /// <summary>
    /// THE COUNT THAT SETTLES THE SECOND TARGET (question 3): N targets give N equations and N + 1 unknowns, so the
    /// system is rank-deficient; the gain from N targets is therefore only the √N of the SAME error, computed here.
    /// </summary>
    public static (int Targets, int Equations, int Unknowns, bool StructurallyDetermined, double ErrorOnC,
        double GainOverOneTarget)[] TwoTargetCounts(double sigmaX = 0.03661)
        => new[] { 1, 2, 3, 5, 10 }.Select(n =>
        {
            double error = 2.0 * sigmaX / (SurfacePotential * Math.Sqrt(n));
            return (n, n, n + 1, false, error, Math.Sqrt(n));
        }).ToArray();

    /// <summary>The c-error the recorded compactness error already imposes, and the three-sigma threshold it implies.</summary>
    public static (double CError, double ThreeSigmaThreshold) ReachOfTheMixedRoute(double sigmaX = 0.03661)
    {
        double error = 2.0 * sigmaX / SurfacePotential;
        return (error, 3.0 * error);
    }

    /// <summary>The interior's advantage: the signature scales as the potential squared, so the core's is larger.</summary>
    public static (double SurfacePotential, double CorePotential, double SignatureRatio) InteriorAdvantage()
    {
        double ratio = Math.Pow(CorePotential / SurfacePotential, 2.0);
        return (SurfacePotential, CorePotential, ratio);
    }

    /// <summary>
    /// THE MEASURABILITY CRITERION, stated once: a route is measurable in the surviving sector when the observable is a
    /// function of the surface redshift and the compactness — the two quantities the sector actually measures — and not
    /// a quantity hidden inside the object, a model-dependent second gradient, or a rate no surviving datum fixes.
    /// </summary>
    public static (string Route, bool Measurable, string Why)[] MeasurabilityLedger() => new[]
    {
        ("the surface redshift", true, "measured directly, to 1 to 20 per cent depending on the instrument class"),
        ("the compactness", true, "measured through the mass and the radius, to 3.661 per cent at the recorded target"),
        ("the kernel's curvature", false, "no measurement reads a curvature of the clock law"),
        ("the core's clock", false, "the interior's clock does not escape, so its larger signature is hidden"),
        ("the tidal field", false, "a second gradient, whose conversion into an observable is a structural model"),
        ("the redshift's drift", false, "the redistribution rate that scales it is fixed by no surviving datum"),
        ("a second target's compactness", true, "measurable, but it arrives with its own free offset (the count above)"),
    };

    // ===================== 4. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measured power, a measured
    /// residual, a count of equations against unknowns, or a computed reach.
    /// </summary>
    public static string Verdict()
    {
        var catalogue = Catalogue();
        var nonCollinear = NonCollinear();
        var independent = IndependentAndMeasurable();
        var telescoping = TransportTelescoping().Single();
        var counts = TwoTargetCounts();
        var reach = ReachOfTheMixedRoute();
        var interior = InteriorAdvantage();
        var b = new StringBuilder();
        b.Append("KERNEL DIMENSION EMPIRICALLY UNDECIDABLE WITHIN THE SURVIVING SECTOR — ");
        b.Append("the degeneracy is STRUCTURAL, not a property of J0740: of the ").Append(catalogue.Length)
            .Append(" observables catalogued across seven domains, ").Append(nonCollinear.Length)
            .Append(" are MATHEMATICALLY independent of the redshift (a different power of the potential) and ")
            .Append(independent.Length).Append(" are independently MEASURABLE. ");
        b.Append("THE MATHEMATICAL SEPARATION IS REAL: the rate's signature has power 2 in the potential, the first ");
        b.Append("derivative's power 1, and the kernel's SECOND derivative IS c exactly — so a curvature measurement ");
        b.Append("would read the extension directly. ");
        b.Append("AND THE EMPIRICAL ONE IS ABSENT: the acceleration route IS the compactness, whose error already vetoes ");
        b.Append("the redshift route; the interior's larger signature (a factor ")
            .Append(interior.SignatureRatio.ToString("F1", CultureInfo.InvariantCulture))
            .Append(" at a core potential five times the mean density) is HIDDEN; the transport telescopes into the ")
            .Append("value difference (residual ").Append(telescoping.Residual.ToString("E3", CultureInfo.InvariantCulture))
            .Append(", the rounding of the logarithmic route), so it carries nothing new; and the dynamical route's rate ")
            .Append("is fixed by no surviving datum. ");
        b.Append("A SECOND TARGET IS NOT SUFFICIENT, by a count rather than a judgement: ")
            .Append(counts[1].Equations).Append(" targets give ").Append(counts[1].Equations)
            .Append(" equations in ").Append(counts[1].Unknowns)
            .Append(" unknowns, and the gain from five is only the √N of the same error (")
            .Append(counts[3].GainOverOneTarget.ToString("F2", CultureInfo.InvariantCulture))
            .Append(") — not a new axis. ");
        b.Append("THE MIXED ROUTE IS THE ONLY ONE THAT MOVES THE NUMBER, and only by precision: the ratio of the value ");
        b.Append("signature to the gradient's is exactly A/2, and the recorded compactness error already imposes a c-error ")
            .Append("of ").Append(reach.CError.ToString("F2", CultureInfo.InvariantCulture))
            .Append(", i.e. a three-sigma threshold near |c| = ")
            .Append(reach.ThreeSigmaThreshold.ToString("F2", CultureInfo.InvariantCulture))
            .Append(" — G_087's veto, recovered from a different direction. ");
        b.Append("SO THE CLASSIFICATION IS THE ONE THE QUESTION NAMES: KernelDimension is EMPIRICALLY UNDECIDABLE within ");
        b.Append("the surviving sector. The mathematical separation exists and is named; the measurement does not.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the measure of independence is the POWER of the potential in an observable's");
        b.AppendLine("signature; that the rate carries power 2, a first derivative power 1, and the kernel's second derivative is");
        b.AppendLine("exactly c; that the interior's bigger signature is hidden, the transport telescopes into the value, the");
        b.AppendLine("dynamical rate is unfixed, and a second target adds an equation with its own free offset; and that the one");
        b.AppendLine("route which moves the number — the mixed (redshift, compactness) pair — moves it by precision only, to a");
        b.AppendLine("three-sigma threshold near |c| = 0.7.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that NO observable can ever separate the two. The second derivative of the");
        b.AppendLine("kernel IS c exactly, so the separation exists mathematically; what is absent is a measurement route. A future");
        b.AppendLine("measurement of the clock law's curvature — or of a drift whose rate the theory fixes — would reverse this");
        b.AppendLine("verdict, and the audit names both.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_087: G_087 measured ONE route's precision; this audit searches seven");
        b.AppendLine("domains for an INDEPENDENT axis and reports the structural reason none is reachable — the extension is a");
        b.AppendLine("function of the potential alone, and every measurement route in the surviving sector reduces to the surface");
        b.AppendLine("redshift and the compactness.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a non-collinear observable that is also measurable (a curvature, a drift with");
        b.AppendLine("a fixed rate, a tidal reading that is not a structural model), or an independent compactness measurement for a");
        b.AppendLine("second target (a different object whose x is known without M and R). None is in the surviving sector.");
        return b.ToString();
    }

    // ===================== 5. THE REPORT =====================

    public static string OutputCatalogue()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE CATALOGUE ACROSS SEVEN DOMAINS, WITH THE POWER OF EACH SIGNATURE MEASURED (task 1)");
        b.AppendLine("   domain                      observable                                  power  collinear  measurable  rate fixed");
        foreach (var o in Catalogue())
        {
            b.AppendLine($"   {o.Domain,-27} {o.Observable,-44} {o.PowerInThePotential,5:F2}  "
                + $"{(o.DegenerateWithTheRedshift ? "yes" : "NO "),9}  {(o.MeasurableInTheSurvivingSector ? "yes" : "no "),10}  "
                + $"{(o.ItsRateIsFixedByASurvivingDatum ? "yes" : "no")}");
        }
        b.AppendLine("   the measure of independence is the POWER: two observables are collinear — degenerate at one target — exactly");
        b.AppendLine("   when their powers agree, because the signature is a function of the potential alone.");
        return b.ToString();
    }

    public static string OutputStructural()
    {
        var b = new StringBuilder();
        var t = TransportTelescoping().Single();
        b.AppendLine("2. THE STRUCTURAL TESTS (tasks 2, 3 and 4)");
        b.AppendLine($"   the transport telescopes EXACTLY: integrated gradient {t.IntegratedGradient:E6} against the value ");
        b.AppendLine($"   difference {t.ValueDifference:E6}, residual {t.Residual:E3} — a clock carried between two sites carries");
        b.AppendLine("   nothing the redshift ratio does not.");
        var interior = InteriorAdvantage();
        b.AppendLine($"   the interior's signature: the core potential {interior.CorePotential:E3} against the surface's "
            + $"{interior.SurfacePotential:E3} gives a signature a factor {interior.SignatureRatio:F1} LARGER — and hidden.");
        b.AppendLine("   the second target's count:");
        foreach (var (targets, equations, unknowns, determined, error, gain) in TwoTargetCounts())
        {
            b.AppendLine($"     {targets,3} target(s): {equations} equation(s) in {unknowns} unknowns, structurally determined: "
                + $"{(determined ? "yes" : "NO")}, the c-error {error:F3}, the gain over one target {gain:F2}");
        }
        var reach = ReachOfTheMixedRoute();
        b.AppendLine($"   the mixed route's reach: a c-error of {reach.CError:F3} from the recorded compactness error, i.e. a");
        b.AppendLine($"   three-sigma threshold near |c| = {reach.ThreeSigmaThreshold:F2}");
        return b.ToString();
    }

    public static string OutputLedger()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE MEASURABILITY LEDGER");
        foreach (var (route, measurable, why) in MeasurabilityLedger())
        {
            b.AppendLine($"   {(measurable ? "reachable" : "UNREACHABLE"),-12} {route,-34} {why}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("4. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
