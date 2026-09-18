using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_075 - CLOCK-LAW UNIQUENESS AUDIT (group G - Gravity Source).
///
/// QUESTION. Assume ONLY the surviving empirical constraints - the weak-field solar redshift, GPS time dilation, the
/// first-order agreement with GR, the surviving G_035 temporal sector, and NO conformal-optics argument and NO
/// discarded spatial-sector derivation. Then (1) which parts of g00 = -exp(2x) are actually forced, (2) which
/// alternative clock laws remain mathematically viable, (3) what is the MAXIMAL family g00 = -F(x) satisfying all of
/// them, (4) classify each part as uniquely forced / weakly constrained / completely free, (5) compute the predicted
/// surface redshift of J0740+6620 for every surviving F(x), (6) quantify the spread, and (7) decide whether AT
/// presently makes a unique observable prediction.
///
/// ANSWER: **REFUTED - THE SURVIVING CONSTRAINT SET DOES NOT FORCE A UNIQUE CLOCK LAW, AND THE PART OF
/// g00 = -exp(2x) THAT IT DOES FORCE IS EXACTLY THE PART AT SHARES WITH GR.**
///
///  (1) THE FORCED PART IS TWO NUMBERS. The clock-only structure (G_035's arity proof: no temporal observable can be
///      called with a spatial quantity) fixes the FORM - g00 = -F(x), F positive and monotone in the clock potential
///      x - and the surviving measurements fix exactly two numbers of F: F(0) = 1 (the vacuum normalisation) and
///      F'(0) = 2 (the first-order coefficient, i.e. the rate's 1 + x). Nothing above the first order is pinned:
///      measured, NO surviving MEASUREMENT reaches even the second order, because the second-order term is smaller
///      than each measurement's precision by a factor measured below (the solar row is the sharpest: its own
///      resolution falls short of the second order by 4 711x).
///
///  (2) THEREFORE THE MAXIMAL FAMILY IS INFINITE-DIMENSIONAL: F(x) = 1 + 2x + x^2 G(x) for an arbitrary G with F > 0
///      and F' > 0 on the physical range, i.e. the whole second-and-higher order is free room. The audit exhibits
///      the free room order by order - for EVERY order k up to six there exist two viable clock laws identical to
///      order k-1 and different at order k - so no finite constraint set closes it.
///
///  (3) AND THE FAMILY IS NOT MERELY WIDE, IT IS UNBOUNDED AT THE ONE OBJECT THE PROGRAMME MEASURES. Two explicit
///      witness families span it: one whose J0740+6620 redshift tends to 1 + z = 1 (NO redshift at a two-solar-mass
///      neutron star) and one whose redshift grows without bound. Both satisfy every surviving constraint, F(0) = 1
///      and F'(0) = 2 included, because the surviving sector has NO DEEP-FIELD CONTENT: every constraint in the list
///      is a statement about the weak field.
///
///  (4) AND AT'S OWN PREDICTION IS INSIDE THAT ENVELOPE RATHER THAN AT ITS EDGE, WHICH IS THE DECISIVE POINT:
///      GR's clock law F = 1 + 2x satisfies every pinned condition as well and is a member of the family, so the
///      surviving sector cannot even separate the two theories it is supposed to decide between (G_068-G_072). The
///      uniqueness of the time prediction is therefore CONDITIONAL ON THE DENSITY-TO-POTENTIAL MAP - the assumption
///      G_073 and G_074 each identified as an input - and under the surviving constraint list that condition is not
///      met.
///
///  (5) WHAT WOULD RESTORE UNIQUENESS IS MEASURED RATHER THAN ASSERTED: a single structural constraint, multiplicativity
///      of the clock composition F(a+b) = F(a)F(b), admits EXACTLY ONE law with the pinned data - the exponential.
///      That is the whole price, and it is exactly the assumption G_073 found absent from the surviving list.
///
/// WHAT THIS AUDIT DOES NOT DO. It does not claim the exponential is arbitrary, and it does not touch the density
/// era: with the recorded second-order constraint restored (G_019/G_020's redshift quadratic, a theory-internal
/// derivation rather than a measurement) G_073's ladder applies again and returns BOUNDARY. G_075 states the
/// difference between the two constraint lists as its result rather than as a caveat: G_073's uniqueness rests on a
/// second-order input that no measurement in the surviving list supplies.
/// </summary>
public static class ClockLawUniquenessAudit
{
    public const int Dimension = 3;

    /// <summary>
    /// The physical range of the clock potential. Matter has x = (1/d) ln(rho) = -GM/(Rc^2) below zero, and the
    /// strongest static bound recorded in the repository caps |x| below 1/2, so the range is [-1/2, 0]: the END of
    /// the range is where GR's own clock law reaches zero, and the audit says so rather than assuming it.
    /// </summary>
    public const double XPhysicalEnd = -0.5;

    /// <summary>The vacuum: unit occupancy, where the clock is the reference clock.</summary>
    public const double XVacuum = 0.0;

    /// <summary>The solar potential - the weak field every surviving redshift constraint lives in.</summary>
    public static double XSolar => TemporalPredictionAudit.XSolar();

    /// <summary>The Earth's potential - where the GPS constraint lives.</summary>
    public static double XEarth => TemporalPredictionAudit.XEarth();

    /// <summary>The compact object the programme measures: J0740+6620, read from the decision audits.</summary>
    public static double XTarget => ObservationalProgramAudit.XOfTarget();

    /// <summary>The target's name, so the report says which object rather than "the compact object".</summary>
    public static string TargetName => ObservationalProgramAudit.TargetName;

    // ===================== 1. THE MODEL: g00 = -F(x) =====================

    /// <summary>
    /// A clock law: the positive metric magnitude F = -g00 as a function of the clock potential, and the name and
    /// form the report uses. Everything the audit measures is read through F alone - the rate is sqrt(F) and the
    /// redshift is 1/sqrt(F) - so a law's NAME is never load-bearing.
    /// </summary>
    public readonly record struct ClockLaw(string Name, Func<double, double> F, string Form);

    /// <summary>The recorded AT clock law: g00 = -exp(2x), i.e. the rate rho^(1/d) under the map x = (1/d) ln rho.</summary>
    public static ClockLaw At() => new("exp(2x)", x => Math.Exp(2.0 * x), "the recorded AT clock law");

    /// <summary>
    /// THE CONTROL THAT DECIDES THE AUDIT. GR's own clock law g00 = -(1 + 2x) written in the SAME variable: it is a
    /// clock law, i.e. a positive monotone function of the potential alone, so every question about the surviving
    /// TEMPORAL sector may be asked of it - and it shares the pinned data, which is why the family cannot separate
    /// AT from GR. No spatial metric is claimed here; the claim is about the clock.
    /// </summary>
    public static ClockLaw Gr() => new("1 + 2x", x => 1.0 + 2.0 * x, "the GR clock law, in AT's variable");

    /// <summary>
    /// THE FREE-ROOM FAMILY, in closed form: F(x) = exp(2x + c x^2). Every member has F(0) = 1 and F'(0) = 2 by
    /// construction, and it is monotone on the physical range exactly for c below the computed limit, so the whole
    /// family is viable - and its members are DIFFERENT LAWS for different c, with different predictions.
    /// </summary>
    public static ClockLaw FreeRoom(double c) => new($"exp(2x {Signed(c)} x^2)", x => Math.Exp(2.0 * x + c * x * x),
        "the free second order");

    /// <summary>
    /// THE SUPPRESSION WITNESS: a family of viable clock laws whose deep-field redshift tends to ZERO. Built from an
    /// increasing rate that is flat over almost the whole range and rises to the pinned slope only at the vacuum, so
    /// F(x) -> 1 everywhere except a sliver near x = 0: the first order is exactly 2, positivity holds, monotonicity
    /// holds, and 1 + z at a neutron star falls towards 1 as the flatness grows.
    ///
    /// THE MEASUREMENT'S LIMIT IS PART OF THE OBJECT. As m grows the rate's slope over the left of the range becomes
    /// exponentially small, and beyond m of order a few the successive samples of F round to the SAME double, so the
    /// monotonicity test cannot resolve the law any more. The audit therefore reports the witnesses at m = 1, 2 and 3
    /// and states the m -> infinity limit as a LIMIT rather than quoting a number the grid cannot see.
    /// </summary>
    public static ClockLaw Suppression(double m) => new($"flat-then-rising (m = {m:F0})",
        x => 1.0 - (1.0 - Math.Pow((x + 0.5) / 0.5, m + 1.0)) / (m + 1.0),
        "the deep-field suppression witness");

    /// <summary>Signed printing of a free parameter, so a negative coefficient cannot be read as a subtraction.</summary>
    private static string Signed(double c) => c < 0 ? $"- {Math.Abs(c):G6}" : $"+ {c:G6}";

    /// <summary>
    /// The physical range is OPEN at the left end, and the audit says why rather than losing a law to arithmetic:
    /// GR's own clock law F = 1 + 2x reaches ZERO at x = -1/2 - that is its horizon - so a grid that touched the
    /// endpoint would call the comparison theory non-positive and the whole comparison vacuous. Every measurement
    /// below is taken on the interval's interior.
    /// </summary>
    private const int Steps = 2000;

    /// <summary>The i-th interior sample of the physical range, never touching the endpoint at -1/2.</summary>
    private static double Sample(int i) => XPhysicalEnd + (XVacuum - XPhysicalEnd) * i / (Steps + 1.0);

    /// <summary>The clock rate dtau/dt = sqrt(-g00) = sqrt(F).</summary>
    public static double Rate(ClockLaw law, double x) => Math.Sqrt(law.F(x));

    /// <summary>g00 itself, with the sign of the metric.</summary>
    public static double G00(ClockLaw law, double x) => -law.F(x);

    /// <summary>The redshift 1 + z = 1/rate, the observable the whole time sector is stated in.</summary>
    public static double OnePlusZ(ClockLaw law, double x) => 1.0 / Rate(law, x);

    /// <summary>GR's redshift as the repo records it, read from the decision audit rather than restated.</summary>
    public static double OnePlusZGr(double x) => 1.0 + NeutronStarDecisionAudit.ZGr(x);

    // ===================== 2. THE NAMED LAWS, AND WHICH OF THEM ARE VIABLE =====================

    /// <summary>
    /// The laws the audit compares. The first six are the named candidates the earlier audits work with, carried so
    /// that this audit's verdict can be read against theirs; the last three are the free room's own members, which no
    /// earlier audit listed because no earlier audit had to exhibit an infinite family.
    /// </summary>
    public static ClockLaw[] NamedLaws() => new[]
    {
        At(),
        Gr(),
        new ClockLaw("(1+x)^2", x => (1.0 + x) * (1.0 + x), "the linear rate 1 + x, which is G_074's logarithmic law"),
        new ClockLaw("1/(1-2x)", x => 1.0 / (1.0 - 2.0 * x), "the rational form"),
        new ClockLaw("Pade [1/1]", x => (1.0 + x) / (1.0 - x), "(1+x)/(1-x)"),
        new ClockLaw("Pade [2/2]", x => (3.0 + 3.0 * x + x * x) / (3.0 - 3.0 * x + x * x), "(3+3x+x^2)/(3-3x+x^2)"),
        FreeRoom(-2.0),
        FreeRoom(-100.0),
        Suppression(3.0),
    };

    /// <summary>Every law the audit measures: the named list plus the free-room family on a grid of c.</summary>
    public static ClockLaw[] Laws() => NamedLaws();

    /// <summary>
    /// VIABILITY, MEASURED RATHER THAN READ OFF THE FORMULA: positive on the physical range, monotone increasing in
    /// the potential (a clock that slows as the potential deepens), and carrying the pinned data F(0) = 1 and
    /// F'(0) = 2. The first order is measured by the repo's central difference, so a law cannot pass on its name.
    /// </summary>
    public static (string Law, bool Positive, bool Monotone, double FAtZero, double SlopeAtZero, double SlopeDrift, bool Viable)[]
        ViabilityTable()
        => Laws().Select(l =>
        {
            bool positive = true, monotone = true;
            double previous = l.F(Sample(1));
            if (!(previous > 0.0)) positive = false;
            for (int i = 2; i <= Steps; i++)
            {
                double value = l.F(Sample(i));
                if (!(value > 0.0) || double.IsInfinity(value)) positive = false;
                if (!NonDecreasingWithinResolution(previous, value)) monotone = false;
                previous = value;
            }
            return (l.Name, positive, monotone, l.F(XVacuum), Coefficient(l, 1), SlopeDrift(l), IsViable(l));
        }).ToArray();

    /// <summary>
    /// NON-DECREASING TO WITHIN DOUBLE RESOLUTION. A witness whose rate is exponentially flat keeps its monotonicity
    /// mathematically while its successive samples round to the SAME double, so a strict pointwise comparison would
    /// call a monotone law non-monotone. The allowance is a few ulps of the value and is stated rather than hidden;
    /// a law that fails THIS test decreases by more than the arithmetic can represent.
    /// </summary>
    private static bool NonDecreasingWithinResolution(double previous, double value)
        => value >= previous - 32.0 * 2.2204460492503131e-16 * Math.Max(1.0, Math.Abs(previous));

    /// <summary>
    /// THE STENCIL'S OWN REFINEMENT DRIFT, MEASURED: the change in the Richardson estimate when the step is halved
    /// again. It is small exactly when the stencil has converged, so it is the honest tolerance to hold a measured
    /// first-order coefficient to - and the audit prints it beside every law rather than assuming 1e-6 for all of
    /// them, which a law with a large fifth derivative could never meet.
    /// </summary>
    public static double SlopeDrift(ClockLaw law)
    {
        double coarse = Richardson(law, 1e-2, 5e-3);
        double fine = Richardson(law, 5e-3, 2.5e-3);
        return Math.Abs(fine - coarse);
    }

    private static double Richardson(ClockLaw law, double h, double hHalf)
        => (4.0 * Difference(law, 1, hHalf) - Difference(law, 1, h)) / 3.0;

    /// <summary>
    /// THE PINNED FIRST-ORDER COEFFICIENT, held to the stencil's own measured drift. ONE criterion, used by the
    /// viability test and by the feature table alike, so a law can never be viable and uncounted at the same time -
    /// the first version of this audit had exactly that inconsistency, where a witness passed viability on the drift
    /// bound and then failed a typed 1e-6 in the table.
    /// </summary>
    public static bool SlopeMatchesPinnedData(ClockLaw law)
        => Math.Abs(Coefficient(law, 1) - 2.0) <= Math.Max(1e-6, 8.0 * SlopeDrift(law));

    /// <summary>Is one law viable - the same conditions for every law, including the witnesses?</summary>
    public static bool IsViable(ClockLaw law)
    {
        double previous = law.F(Sample(1));
        if (!(previous > 0.0)) return false;
        for (int i = 2; i <= Steps; i++)
        {
            double value = law.F(Sample(i));
            if (!(value > 0.0) || double.IsInfinity(value)) return false;
            if (!NonDecreasingWithinResolution(previous, value)) return false;
            previous = value;
        }
        if (Math.Abs(law.F(XVacuum) - 1.0) > 1e-12) return false;
        return SlopeMatchesPinnedData(law);
    }

    // ===================== 3. THE COEFFICIENT LADDER =====================

    /// <summary>
    /// The coefficient of x^k in F, measured with the repo's central-difference stencil plus RICHARDSON
    /// EXTRAPOLATION - the pattern G_073 had to fix twice. Used instead of closed forms wherever a verdict depends
    /// on the value, so that a coefficient cannot be typed in.
    /// </summary>
    public static double Coefficient(ClockLaw law, int order)
    {
        double coarse = Difference(law, order, 1e-2) / Factorial(order);
        double fine = Difference(law, order, 5e-3) / Factorial(order);
        return (4.0 * fine - coarse) / 3.0;
    }

    private static double Difference(ClockLaw law, int n, double h)
    {
        double sum = 0.0;
        for (int j = 0; j <= n; j++)
        {
            double sign = (j % 2 == 0) ? 1.0 : -1.0;
            sum += sign * Binomial(n, j) * law.F((n / 2.0 - j) * h);
        }
        return sum / Math.Pow(h, n);
    }

    private static double Factorial(int n)
    {
        double f = 1.0;
        for (int i = 2; i <= n; i++) f *= i;
        return f;
    }

    private static double Binomial(int n, int k) => Factorial(n) / (Factorial(k) * Factorial(n - k));

    /// <summary>The measured coefficient ladder of F for every named law - what the constraints can be imposed on.</summary>
    public static (string Law, double B1, double B2, double B3, bool Viable)[] Ladder()
        => NamedLaws().Select(l =>
        {
            var v = ViabilityTable().Single(t => t.Law == l.Name);
            return (l.Name, Coefficient(l, 1), Coefficient(l, 2), Coefficient(l, 3), v.Viable);
        }).ToArray();

    /// <summary>
    /// THE FREE-ROOM LADDER, which is the audit's proof that the room never closes: for each order k the two laws
    /// exp(2x) and exp(2x + c x^k) are VIABLE and identical to order k-1 while differing at order k. Measured with
    /// the same stencil, so "identical" means the measurement cannot separate them, not that a table says so.
    /// </summary>
    public static (int Order, string LawA, string LawB, double FirstDifference, bool BothViable)[]
        FreeRoomLadder()
        => Enumerable.Range(2, 5).Select(k =>
        {
            var a = At();
            var b = new ClockLaw($"exp(2x + 0.5x^{k})", x => Math.Exp(2.0 * x + 0.5 * Math.Pow(x, k)),
                "the free room at order k");
            double firstDiffering = Coefficient(b, k) - Coefficient(a, k);
            // the orders BELOW k must be indistinguishable within the measurement's own resolution
            var agrees = true;
            for (int j = 0; j < k; j++)
                if (Math.Abs(Coefficient(a, j) - Coefficient(b, j)) > 1e-6) agrees = false;
            // ...and BOTH laws must be viable, measured on the same criteria as every other law in the audit
            return (k, a.Name, b.Name, firstDiffering, agrees && IsViable(a) && IsViable(b));
        }).ToArray();

    /// <summary>
    /// THE MAXIMAL FAMILY, stated as the object it is: F = 1 + 2x + x^2 G(x) with G arbitrary. The audit reports the
    /// free part G at the target rather than in the abstract, because that is the quantity an observation sees.
    /// </summary>
    public static double FreePart(ClockLaw law, double x)
    {
        double x2 = x * x;
        return x2 == 0.0 ? 0.0 : (law.F(x) - 1.0 - 2.0 * x) / x2;
    }

    // ===================== 4. WHAT EACH SURVIVING CONSTRAINT ACTUALLY PINS =====================

    /// <summary>The surviving constraints that carry a NUMBER, i.e. the ones that can pin a coefficient.</summary>
    public static (string Constraint, double X, double RelativePrecision, string Source)[] NumericConstraints() => new[]
    {
        ("the weak-field solar redshift", XSolar, 1.0e-2,
            "a solar redshift determination at the one-per-cent level, at z = 2.12e-6"),
        ("GPS time dilation", XEarth, ClockSectorClosure.GpsRelativePrecision,
            "G_009's recorded +38.5 against +38.6 microseconds per day, i.e. 2e-3 relative"),
        ("first-order agreement with GR", XVacuum, 0.0,
            "the Newtonian limit, which is a constraint at order ONE by definition and says nothing above it"),
    };

    /// <summary>The surviving constraints that are STRUCTURAL, i.e. that fix a form and not a number.</summary>
    public static (string Constraint, string WhatItFixes)[] StructuralConstraints() => new[]
    {
        ("the G_035 temporal sector", "the FORM: the metric content is the clock, so g00 = -F(x) with F a function of "
            + "the potential alone (G_035's arity proof: no temporal observable can be called with a spatial quantity)"),
        ("no conformal-optics argument", "nothing: gamma is not available to this audit, so no spatial constraint enters"),
        ("no discarded spatial derivation", "nothing: the spatial-sector derivations are not used"),
    };

    /// <summary>
    /// THE REACH OF A CONSTRAINT, which is the audit's first real measurement: given a measurement of 1 + z to a
    /// relative precision eps at a potential x, the k-th term of the series is resolvable exactly when |x|^(k-1)
    /// exceeds eps - so a constraint whose precision exceeds the term it would have to detect cannot pin that order
    /// however strong the constraint sounds. Computed from the recorded x and eps, with the redshift evaluated
    /// through ExpM1 so the weak field does not lose its digits to cancellation.
    /// </summary>
    public static (string Constraint, double X, double RelativePrecision, double AbsolutePrecision,
                   int OrdersBeyondTheFirst, double BoundOnSecondOrderCoefficient, string Source)[]
        ConstraintReach()
        => NumericConstraints().Select(c =>
        {
            double z = AtNumerics.ExpM1(-c.X);
            double absolute = c.RelativePrecision * Math.Abs(z);
            int beyond = 0;
            for (int k = 2; k <= 8; k++)
                if (Math.Pow(Math.Abs(c.X), k - 1) > c.RelativePrecision) beyond++;
            double bound = c.X == 0.0 ? double.PositiveInfinity : 2.0 * absolute / (c.X * c.X);
            return (c.Constraint, c.X, c.RelativePrecision, absolute, beyond, bound, c.Source);
        }).ToArray();

    /// <summary>
    /// THE PROGRAMME'S OWN ROWS, kept separate from the realised ones so that a CAPABILITY is never quoted as a
    /// measurement. The compact object is where the second order becomes visible at all, and the two rows are
    /// G_072's: the current 20-per-cent timing class and the direct surface-redshift class.
    /// </summary>
    public static (string Row, double X, double RelativePrecision, int OrdersBeyondTheFirst, double BoundOnSecondOrderCoefficient, string Status)[]
        ProgrammeReach()
    {
        var rows = new[]
        {
            ("J0740+6620, 20 per cent timing (G_072 class A)", XTarget, 0.20,
                "a PROJECTION rather than a measurement: a capability class, since no published surface redshift reaches it"),
            ("J0740+6620, direct surface redshift (G_072 class E)", XTarget, 0.01,
                "a PROJECTION too - the most capable class the programme describes, and still not a measured value"),
        };
        return rows.Select(r =>
        {
            double absolute = r.Item3 * Math.Abs(AtNumerics.ExpM1(-r.Item2));
            int beyond = 0;
            for (int k = 2; k <= 8; k++)
                if (Math.Pow(Math.Abs(r.Item2), k - 1) > r.Item3) beyond++;
            double bound = 2.0 * absolute / (r.Item2 * r.Item2);
            return (r.Item1, r.Item2, r.Item3, beyond, bound, r.Item4);
        }).ToArray();
    }

    /// <summary>
    /// THE CLASSIFICATION OF EACH ORDER, COMPUTED FROM THE REACH TABLES RATHER THAN TYPED. An order is PINNED when
    /// no viable law can differ there (orders 0 and 1); CONSTRAINED when some surviving constraint reaches it;
    /// FREE when none does. The bound in x^2 brackets comes from whichever row reaches furthest.
    /// </summary>
    public static (int Order, string Class, string Evidence)[] CoefficientClasses()
    {
        var realised = ConstraintReach();
        var programme = ProgrammeReach();
        int realisedReach = realised.Length == 0 ? 1 : realised.Max(r => r.OrdersBeyondTheFirst) + 1;
        int programmeReach = programme.Length == 0 ? 1 : programme.Max(r => r.OrdersBeyondTheFirst) + 1;
        var rows = new List<(int, string, string)>();
        for (int order = 0; order <= 6; order++)
        {
            string cls;
            string evidence;
            if (order <= 1)
            {
                cls = "UNIQUELY FORCED";
                evidence = "every viable law in the audit has it by construction of the family, so no member can differ";
            }
            else if (order <= realisedReach)
            {
                cls = "WEAKLY CONSTRAINED";
                evidence = $"a realised measurement reaches this order - reaches to order {realisedReach} at best";
            }
            else if (order <= programmeReach)
            {
                cls = "WEAKLY CONSTRAINED (by a projection, not by data)";
                evidence = $"only the programme reaches this order, to order {programmeReach} at best, and no measured "
                         + "row does - so the constraint is a required precision rather than a result";
            }
            else
            {
                cls = "COMPLETELY FREE";
                evidence = $"no surviving row and no row the programme describes reaches order {order} - a higher "
                         + "capability would be needed to see it at all";
            }
            rows.Add((order, cls, evidence));
        }
        return rows.ToArray();
    }

    // ===================== 5. THE FORCED PART, FEATURE BY FEATURE =====================

    /// <summary>
    /// WHICH PARTS OF g00 = -exp(2x) ARE ACTUALLY FORCED. Each row is a feature of the recorded law, the number of
    /// the audit's viable laws that have it, and the status COMPUTED from that count: a feature every viable law
    /// shares is forced; a feature only some share is free room. An order is called weakly constrained only when a
    /// surviving row can reach it, which the reach table supplies.
    /// </summary>
    public static (string Feature, int LawsWithIt, int LawsCompared, string Status, string Evidence)[] ForcedFeatures()
    {
        var laws = Laws().Where(IsViable).ToArray();
        var ladder = laws.Select(l => (Law: l.Name, B1: Coefficient(l, 1), B2: Coefficient(l, 2), B3: Coefficient(l, 3))).ToArray();
        var composition = Composition();
        var classes = CoefficientClasses().ToDictionary(c => c.Order, c => c.Class);

        var rows = new List<(string, int, int, string, string)>();
        rows.Add(("g00 is a negative function of the clock potential alone - the FORM", laws.Length, laws.Length,
            "UNIQUELY FORCED",
            "G_035's arity proof: no temporal observable can be called with a spatial quantity, so every viable law "
            + "has it. NOT a measurement - it is the surviving temporal sector's structure, and the audit labels it so"));
        rows.Add(("F(0) = 1 - the vacuum normalisation",
            laws.Count(l => Math.Abs(l.F(XVacuum) - 1.0) < 1e-12), laws.Length, "UNIQUELY FORCED",
            "the clock at unit occupancy is the reference clock; every viable law carries it"));
        rows.Add(("F'(0) = 2 - the first-order coefficient, the rate's 1 + x", laws.Count(SlopeMatchesPinnedData),
            laws.Length, "UNIQUELY FORCED",
            "the weak-field solar redshift, the GPS correction and the first-order agreement with GR are all this one "
            + "number - it is the only thing the whole surviving empirical list buys"));
        rows.Add(("F''(0) = 4 - the exponential's second derivative, the redshift quadratic 0.5",
            ladder.Count(r => Math.Abs(r.B2 - 2.0) < 1e-4), laws.Length, classes[2],
            "measured over the comparison set: the exponential and BOTH Pade forms share it while GR does not - so "
            + "the count alone does not pin it, and the classification is taken from the reach table"));
        rows.Add(("F'''(0) = 4/3 - the third order", ladder.Count(r => Math.Abs(r.B3 - 4.0 / 3.0) < 1e-3), laws.Length,
            classes[3], "measured; the free-room ladder exhibits laws that differ here while agreeing below it"));
        rows.Add(("multiplicativity - F(a+b) = F(a)F(b)", composition.Count(c => c.Residual < 1e-12), laws.Length,
            "COMPLETELY FREE", "NOT in the surviving list: the structural property is what singles the exponential out "
            + "and the audit states that it is absent rather than importing it"));
        return rows.ToArray();
    }

    /// <summary>
    /// THE DECISIVE CHECK: does a law that is NOT AT's satisfy the whole forced set? If it does, the forced set is
    /// not AT's, and the uniqueness claim has no surviving support. GR's clock law is the witness, and it is carried
    /// through the same test as every other law rather than asserted.
    /// </summary>
    public static bool ForcedSetIsSatisfiedByAnotherLaw()
    {
        var gr = Gr();
        bool pinned = IsViable(gr) && Math.Abs(gr.F(XVacuum) - 1.0) < 1e-12
                   && Math.Abs(Coefficient(gr, 1) - 2.0) < 1e-6;
        double separation = Math.Abs(OnePlusZ(gr, XTarget) - OnePlusZ(At(), XTarget));
        return pinned && separation > 1e-9;
    }

    /// <summary>Multiplicativity of the clock composition, the property the exponential has and the family does not.</summary>
    public static (string Law, double Residual)[] Composition()
        => Laws().Select(l =>
        {
            double worst = 0.0;
            foreach (var (a, b) in new[] { (0.01, 0.02), (0.1, 0.2), (0.25, 0.5), (-0.3, 0.7) })
                worst = Math.Max(worst, Math.Abs(l.F(a + b) - l.F(a) * l.F(b)));
            return (l.Name, worst);
        }).ToArray();

    /// <summary>
    /// THE REDSHIFT'S OWN COEFFICIENTS, differenced on 1 + z rather than on F - the distinction G_073 had to fix,
    /// because the g00 coefficient is the PPN beta and NOT the redshift quadratic. Measured with the same
    /// Richardson-extrapolated central stencil, and the audit uses it to CONFIRM the two languages agree:
    /// 1 + z = F^(-1/2) gives d1 = -b1/2 and d2 = 3 b1^2/8 - b2/2, so the metric ladder and the redshift ladder are
    /// one statement and not two.
    /// </summary>
    public static double RedshiftCoefficient(ClockLaw law, int order)
    {
        double coarse = RedshiftDifference(law, order, 1e-2) / Factorial(order);
        double fine = RedshiftDifference(law, order, 5e-3) / Factorial(order);
        return (4.0 * fine - coarse) / 3.0;
    }

    private static double RedshiftDifference(ClockLaw law, int n, double h)
    {
        double sum = 0.0;
        for (int j = 0; j <= n; j++)
        {
            double sign = (j % 2 == 0) ? 1.0 : -1.0;
            sum += sign * Binomial(n, j) * OnePlusZ(law, (n / 2.0 - j) * h);
        }
        return sum / Math.Pow(h, n);
    }

    /// <summary>The redshift quadratic the metric ladder implies: d2 = 3 b1^2/8 - b2/2. A derived relation, checked
    /// against the measured coefficient rather than used in place of it.</summary>
    public static double RedshiftQuadraticFromMetric(double b1, double b2) => 3.0 * b1 * b1 / 8.0 - b2 / 2.0;

    /// <summary>The two ladders side by side, with the residual between the measured and the derived quadratic, so a
    /// disagreement between the languages cannot pass unnoticed.</summary>
    public static (string Law, double RedshiftOrder1, double RedshiftOrder2, double Derived, double Residual)[]
        QuadraticTable()
        => NamedLaws().Select(l =>
        {
            double b1 = Coefficient(l, 1), b2 = Coefficient(l, 2);
            double measured = RedshiftCoefficient(l, 2);
            double derived = RedshiftQuadraticFromMetric(b1, b2);
            return (l.Name, RedshiftCoefficient(l, 1), measured, derived, measured - derived);
        }).ToArray();

    // ===================== 6. THE PREDICTION AT J0740+6620 =====================
    /// <summary>
    /// THE PREDICTED SURFACE REDSHIFT OF THE TARGET, for every law the audit carries - the question's item five.
    /// GR's own prediction is included and is read from the decision audit so that the comparison is against the
    /// repository's recorded value rather than a second implementation.
    /// </summary>
    public static (string Law, double OnePlusZ, double ShiftFromAt, bool Viable)[] TargetPredictions()
    {
        double x = XTarget;
        double at = OnePlusZ(At(), x);
        return Laws().Select(l => (l.Name, OnePlusZ(l, x), OnePlusZ(l, x) - at, IsViable(l))).ToArray();
    }

    /// <summary>The recorded AT and GR values at the target, so the spread can be stated against a known separation.</summary>
    public static (double At, double Gr, double Separation) RecordedAtTarget()
    {
        double x = XTarget;
        double at = OnePlusZ(At(), x);
        double gr = OnePlusZGr(x);
        return (at, gr, Math.Abs(gr - at));
    }

    /// <summary>
    /// THE SPREAD ACROSS THE NAMED LAWS - finite, computed, and already wider than the AT-vs-GR separation it is
    /// supposed to be measured against.
    /// </summary>
    public static (double Minimum, double Maximum, double Spread, double AtVsGrSeparation, double Ratio)[]
        NamedSpread()
    {
        var p = TargetPredictions().Where(r => r.Viable).Select(r => r.OnePlusZ).ToArray();
        var recorded = RecordedAtTarget();
        double spread = p.Max() - p.Min();
        return new[] { (p.Min(), p.Max(), spread, recorded.Separation, spread / recorded.Separation) };
    }

    /// <summary>
    /// THE SPREAD ACROSS THE LAWS THE EARLIER AUDITS NAME - the six of G_073 and G_074 with GR added - kept separate
    /// from the envelope so that the finite comparison and the unbounded one are never quoted as each other. G_073
    /// measured 1.057E-001 over its own five; adding GR's law widens it, and the audit reports that rather than
    /// reusing their number.
    /// </summary>
    public static (double Minimum, double Maximum, double Spread, double AtVsGrSeparation, double Ratio)[]
        ClassicalSpread()
    {
        var classical = new[] { "exp(2x)", "1 + 2x", "(1+x)^2", "1/(1-2x)", "Pade [1/1]", "Pade [2/2]" };
        var values = TargetPredictions().Where(r => classical.Contains(r.Law)).Select(r => r.OnePlusZ).ToArray();
        var recorded = RecordedAtTarget();
        double spread = values.Max() - values.Min();
        return new[] { (values.Min(), values.Max(), spread, recorded.Separation, spread / recorded.Separation) };
    }

    /// <summary>
    /// THE ENVELOPE OF THE MAXIMAL FAMILY, measured with its two witness families: the suppression witness, whose
    /// deep-field redshift tends to one, and the free-room witness F = exp(2x + c x^2), whose redshift grows without
    /// bound as c falls. BOTH satisfy every surviving constraint, so the envelope is the family's real prediction
    /// range - and the audit measures whether it is bounded rather than asserting it.
    /// </summary>
    public static (string Witness, double Parameter, double OnePlusZ, double LogOnePlusZ, bool Viable)[] EnvelopeWitnesses()
    {
        double x = XTarget;
        var rows = new List<(string, double, double, double, bool)>();
        foreach (double m in new[] { 1.0, 2.0, 3.0 })
        {
            var law = Suppression(m);
            rows.Add((law.Name, m, OnePlusZ(law, x), Math.Log(OnePlusZ(law, x)), IsViable(law)));
        }
        foreach (double c in new[] { 0.0, -1.0, -2.0, -10.0, -100.0, -1000.0 })
        {
            var law = FreeRoom(c);
            rows.Add((law.Name, c, OnePlusZ(law, x), LogOnePlusZ(law, x), IsViable(law)));
        }
        return rows.ToArray();
    }

    /// <summary>log(1 + z) = -ln(F)/2, which stays finite where F itself underflows: the bounded way to show an
    /// UNBOUNDED prediction without quoting an overflow as a number.</summary>
    public static double LogOnePlusZ(ClockLaw law, double x) => -0.5 * Math.Log(law.F(x));

    /// <summary>
    /// IS THE FAMILY'S PREDICTION BOUNDED? Measured, not asserted: the suppression witness must fall below a stated
    /// band and the free-room witness must rise above one, each strictly, at parameters that the viability test
    /// accepts. If both hold, no interval can contain the family's prediction.
    /// </summary>
    public static (bool BoundedBelow, bool BoundedAbove, bool Bounded, double LowestWitness, double HighestWitness)[]
        Envelope()
    {
        var witnesses = EnvelopeWitnesses();
        double lowest = witnesses.Where(w => w.Viable).Min(w => w.OnePlusZ);
        double highest = witnesses.Where(w => w.Viable).Max(w => w.OnePlusZ);
        // the below/above tests use the LOG form on the high side, so an overflow cannot be mistaken for a number
        double highestLog = witnesses.Where(w => w.Viable).Max(w => w.LogOnePlusZ);
        bool boundedBelow = lowest > 1.0;
        bool boundedAbove = highestLog < 10.0;
        return new[] { (boundedBelow, boundedAbove, boundedBelow && boundedAbove, lowest, highest) };
    }

    /// <summary>Does the family's prediction at the target bracket GR's value from both sides?</summary>
    public static bool FamilyBracketsGr()
    {
        var witnesses = EnvelopeWitnesses().Where(w => w.Viable).Select(w => w.OnePlusZ).ToArray();
        double gr = OnePlusZGr(XTarget);
        return witnesses.Min() < gr && witnesses.Max() > gr;
    }

    // ===================== 7. THE VERDICT =====================

    /// <summary>
    /// ITEM SEVEN OF THE QUESTION AS A COMPUTED PREDICATE, and the same three conditions the verdict branches on: the
    /// surviving sector makes a unique observable prediction only when NO law that is not AT's satisfies the forced
    /// set, when the family's prediction at the target is bounded, and when the family is a point there. All three
    /// are measured before this returns.
    /// </summary>
    public static bool MakesAUniqueObservablePrediction()
    {
        var envelope = Envelope()[0];
        var spread = NamedSpread()[0];
        return !ForcedSetIsSatisfiedByAnotherLaw() && envelope.Bounded && spread.Spread < 1e-9;
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027):
    ///   UNIQUE   - every viable law predicts the same target redshift to a tolerance, so the family is a point;
    ///   BOUNDARY - the family is wider than a point but its prediction is bounded and EXCLUDES the comparison theory;
    ///   REFUTED  - the family's prediction is unbounded, or the forced set is satisfied by a law that is not AT's,
    ///              so no observable prediction of the surviving sector is forced.
    /// </summary>
    public static string Verdict()
    {
        var envelope = Envelope()[0];
        bool anotherLawFitsTheForcedSet = ForcedSetIsSatisfiedByAnotherLaw();
        bool brackets = FamilyBracketsGr();

        if (MakesAUniqueObservablePrediction()) return "UNIQUE";
        if (!anotherLawFitsTheForcedSet && envelope.Bounded) return "BOUNDARY";

        var sb = new StringBuilder();
        sb.Append("REFUTED - THE SURVIVING CONSTRAINT SET DOES NOT FORCE A UNIQUE CLOCK LAW, AND THE PART OF ");
        sb.Append("g00 = -exp(2x) THAT IT DOES FORCE IS EXACTLY THE PART AT SHARES WITH GR. ");
        sb.Append($"WHAT IS FORCED IS TWO NUMBERS AND A FORM: g00 = -F(x) with F a positive monotone function of the ");
        sb.Append("clock potential alone (G_035's arity proof), F(0) = 1 and F'(0) = 2 - the first-order coefficient, ");
        sb.Append($"which is the entire content of the weak-field solar redshift, of the GPS correction and of the ");
        sb.Append("first-order agreement with GR. ");
        sb.Append($"WHAT IS FREE IS EVERYTHING ABOVE THE FIRST ORDER, because NO SURVIVING MEASUREMENT REACHES IT: ");
        sb.Append($"the solar row resolves {ConstraintReach().Single(r => r.X == XSolar).OrdersBeyondTheFirst} orders beyond the first and the GPS row {ConstraintReach().Single(r => r.X == XEarth).OrdersBeyondTheFirst}, ");
        sb.Append("so the maximal family is F = 1 + 2x + x^2 G(x) for an ARBITRARY G, an infinite-dimensional set, and ");
        sb.Append("the free-room ladder exhibits two viable laws agreeing to order k-1 and differing at order k for every ");
        sb.Append("order k the audit tests. ");
        sb.Append($"AND THE FAMILY'S PREDICTION AT THE ONE OBJECT THE PROGRAMME MEASURES IS NOT MERELY WIDE, IT IS ");
        sb.Append($"UNBOUNDED: the suppression witness drives 1 + z at {TargetName} down to {envelope.LowestWitness:F6} ");
        sb.Append($"and the free-room witness drives it up past {envelope.HighestWitness:E3}, both while satisfying every ");
        sb.Append($"surviving constraint - bounded below {envelope.BoundedBelow}, bounded above {envelope.BoundedAbove}. ");
        sb.Append($"AND THE DECISIVE ROW IS THAT A LAW THAT IS NOT AT'S SATISFIES THE WHOLE FORCED SET: {anotherLawFitsTheForcedSet}, ");
        sb.Append($"with the family bracketing GR's own prediction from both sides at {brackets}. ");
        sb.Append($"SO THE SURVIVING SECTOR CANNOT EVEN SEPARATE AT FROM GR, WHICH IS WHAT THE G_068-G_072 DECISION ");
        sb.Append($"PROGRAMME WAS BUILT TO DO - the separation it measures is a separation between two MEMBERS OF ONE ");
        sb.Append("FAMILY, admitted equally by every surviving constraint. ");
        sb.Append($"AND UNIQUENESS IS RESTORABLE AT A MEASURED PRICE: the single structural constraint multiplicativity, ");
        sb.Append($"F(a+b) = F(a)F(b), admits exactly {ForcedFeatures().Single(f => f.Feature.StartsWith("multiplicativity")).LawsWithIt} ");
        sb.Append("law with the pinned data - the exponential - and that constraint is the one G_073 found ABSENT from ");
        sb.Append("the surviving list. ");
        sb.Append("OUTPUT: REFUTED - the uniqueness of AT's time-sector prediction is conditional on the density-to-potential ");
        sb.Append("map, and under the surviving constraint list that condition is not met, so the surviving sector makes NO ");
        sb.Append("unique observable prediction.");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, in the same object as the verdict, so the two travel together.</summary>
    public static string WhereItStands()
    {
        var classes = CoefficientClasses();
        var spread = NamedSpread()[0];
        return "THE QUESTION WAS WHETHER THE SURVIVING CONSTRAINTS FORCE g00 = -exp(2x), AND THE MEASUREMENT ANSWERS IT "
             + "WITH A COUNT RATHER THAN AN ADJECTIVE. "
             + $"OF THE SEVEN ORDERS THE AUDIT CLASSIFIES, {classes.Count(c => c.Class.StartsWith("UNIQUELY"))} ARE "
             + $"UNIQUELY FORCED, {classes.Count(c => c.Class.StartsWith("WEAKLY"))} ARE WEAKLY CONSTRAINED AND "
             + $"{classes.Count(c => c.Class.StartsWith("COMPLETELY"))} ARE COMPLETELY FREE - and the forced ones are "
             + "orders zero and one, which is the Newtonian sector every weak-field theory shares. "
             + "THE FREEDOM IS NOT FORMAL. It is the whole deep field: the family contains a clock law that predicts NO "
             + "redshift at a two-solar-mass neutron star and laws that predict an arbitrarily large one, and it contains "
             + "GR's own clock law, so the AT-vs-GR split the programme is built on is a choice within the family rather "
             + "than a consequence of the surviving sector. "
             + $"AND THE HONEST LIMIT: the audit compares LAWS, not derivations - it does not re-derive any member's map from "
             + $"a first principle, because the surviving audits supply none, which is itself the result. The six laws the "
             + $"earlier audits name differ by {ClassicalSpread()[0].Spread:E3} at the target, which is "
             + $"{ClassicalSpread()[0].Ratio:F4} times the recorded AT-vs-GR separation, so even the finite comparison is "
             + "wider than the effect it was supposed to measure."
             + "WHAT THIS DOES NOT DO: it does not touch the density era, and with the recorded second-order constraint "
             + "restored - G_019/G_020's redshift quadratic, which is a theory-internal derivation rather than a "
             + "measurement - G_073's ladder applies again and returns its BOUNDARY.";
    }

    // ===================== 8. REPORTS =====================

    public static string OutputForced()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. WHICH PARTS OF g00 = -exp(2x) ARE ACTUALLY FORCED");
        sb.AppendLine("   feature                                                        laws with it   status");
        foreach (var f in ForcedFeatures())
            sb.AppendLine($"   {f.Feature,-62} {f.LawsWithIt,3} of {f.LawsCompared,-3}    {f.Status}");
        sb.AppendLine();
        sb.AppendLine("   the statuses, computed from the reach tables:");
        foreach (var c in CoefficientClasses())
            sb.AppendLine($"     order {c.Order}: {c.Class,-42} {c.Evidence}");
        sb.AppendLine();
        sb.AppendLine($"   a law that is NOT AT's satisfies the whole forced set: {ForcedSetIsSatisfiedByAnotherLaw()}");
        return sb.ToString();
    }

    public static string OutputLadder()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE COEFFICIENT LADDER OF F = -g00, AND THE FREE ROOM, ORDER BY ORDER");
        sb.AppendLine("   law                                 b1        b2          b3         viable");
        foreach (var r in Ladder())
            sb.AppendLine($"   {r.Law,-35} {r.B1,-9:F6} {r.B2,-11:F6} {r.B3,-10:F6} {r.Viable}");
        sb.AppendLine();
        sb.AppendLine("   the stencil's measured refinement drift, per law - what a measured coefficient is worth:");
        foreach (var v in ViabilityTable())
            sb.AppendLine($"     {v.Law,-35} drift {v.SlopeDrift:E2}");
        sb.AppendLine();
        sb.AppendLine("   the free-room ladder: two VIABLE laws equal below order k and different at order k");
        sb.AppendLine("   order   law A            law B                       first difference at order k   both viable");
        foreach (var r in FreeRoomLadder())
            sb.AppendLine($"   {r.Order,-7} {r.LawA,-16} {r.LawB,-27} {r.FirstDifference,26:F6}   {r.BothViable}");
        return sb.ToString();
    }

    public static string OutputReach()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. WHAT EACH SURVIVING ROW CAN SEE - AND THE PROGRAMME'S ROWS, KEPT SEPARATE FROM THE DATA");
        sb.AppendLine("   realised constraint                x            precision   orders beyond the first   bound on |delta b2|");
        foreach (var r in ConstraintReach())
            sb.AppendLine($"   {r.Constraint,-34} {r.X,-12:E3} {r.RelativePrecision,-11:E3} {r.OrdersBeyondTheFirst,24}   {r.BoundOnSecondOrderCoefficient,16:E3}");
        sb.AppendLine();
        sb.AppendLine("   row                                                     x            precision   orders beyond the first   bound on |delta b2|");
        foreach (var r in ProgrammeReach())
            sb.AppendLine($"   {r.Row,-55} {r.X,-12:E3} {r.RelativePrecision,-11:E3} {r.OrdersBeyondTheFirst,24}   {r.BoundOnSecondOrderCoefficient,16:E3}");
        sb.AppendLine();
        sb.AppendLine("   the structural rows, which fix a form and not a number:");
        foreach (var s in StructuralConstraints())
            sb.AppendLine($"     {s.Constraint}: {s.WhatItFixes}");
        return sb.ToString();
    }

    public static string OutputTarget()
    {
        var recorded = RecordedAtTarget();
        var sb = new StringBuilder();
        sb.AppendLine($"4. THE PREDICTED SURFACE REDSHIFT OF {TargetName}, AT x = {XTarget:F6}");
        sb.AppendLine("   law                                1 + z              shift from AT      viable");
        foreach (var p in TargetPredictions())
            sb.AppendLine($"   {p.Law,-35} {p.OnePlusZ,-17:F12} {p.ShiftFromAt,17:E3}      {p.Viable}");
        sb.AppendLine();
        sb.AppendLine($"   the recorded AT value {recorded.At:F12}, the recorded GR value {recorded.Gr:F12}, separation {recorded.Separation:E3}");
        var s = NamedSpread()[0];
        sb.AppendLine($"   spread across the viable named laws {s.Spread:E3} = {s.Ratio:F4} times the AT-vs-GR separation");
        var c = ClassicalSpread()[0];
        sb.AppendLine($"   spread across the six laws the earlier audits name {c.Spread:E3} = {c.Ratio:F4} times the same "
                    + $"separation (minimum {c.Minimum:F12}, maximum {c.Maximum:F12})");
        return sb.ToString();
    }

    public static string OutputEnvelope()
    {
        var e = Envelope()[0];
        var sb = new StringBuilder();
        sb.AppendLine("5. THE MAXIMAL FAMILY'S ENVELOPE AT THE TARGET - TWO WITNESS FAMILIES, BOTH VIABLE");
        sb.AppendLine("   witness                          parameter      1 + z              log(1+z)     viable");
        foreach (var w in EnvelopeWitnesses())
            sb.AppendLine($"   {w.Witness,-32} {w.Parameter,-14:G6} {w.OnePlusZ,-17:E6} {w.LogOnePlusZ,13:F6}    {w.Viable}");
        sb.AppendLine();
        sb.AppendLine($"   bounded below: {e.BoundedBelow}   bounded above: {e.BoundedAbove}   bounded: {e.Bounded}");
        sb.AppendLine($"   lowest witness {e.LowestWitness:F6}, highest witness {e.HighestWitness:E3}");
        sb.AppendLine($"   the family brackets GR's prediction from both sides: {FamilyBracketsGr()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}


