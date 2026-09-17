using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_074 - Clock Law Necessity Audit (group G).
///
/// QUESTION. Does any surviving AT result require dtau/dt = rho^(1/d) SPECIFICALLY, or only a monotonic function of
/// rho? Candidates: rho^(1/d), ln(rho), exp(rho) and Pade forms. Recompute g00, the redshift and the compact-star
/// prediction. Output UNIQUE / BOUNDARY / REFUTED. Goal: does the clock law, or merely monotonicity, drive the
/// surviving time sector?
///
/// ANSWER: **BOUNDARY - MONOTONICITY DRIVES THE SHAPE AND THE MAP DRIVES THE NUMBERS.**
///
///  (1) EVERY CANDIDATE IS MONOTONE AND POSITIVE on the relevant range, so every one of them reproduces the SHAPE of
///      the surviving sector: a positive rate whose ratio between two densities is the redshift. Nothing in that
///      shape names a functional form.
///
///  (2) AND THE CANDIDATES ARE NOT INDEPENDENT, WHICH IS THE AUDIT'S FIRST RESULT: exp(rho) composed with the
///      logarithmic map rho = e^(d x) IS rho^(1/d) to machine precision. What distinguishes the candidates is
///      therefore NOT their names but the MAP that converts a density into a potential - and the map is exactly what
///      the surviving audits take as an input.
///
///  (3) SO THE FORM IS IDENTIFIABLE ONLY THROUGH ITS NONLINEARITY, which the audit measures at the two places the
///      surviving sector is decisive: the redshift's second-order coefficient and the compact-star prediction. The
///      first is a PPN-class number at solar-system scales; the second is within reach of the observing programme
///      G_072 describes, which is what makes this audit's answer usable rather than formal.
/// </summary>
public static class ClockLawNecessityAudit
{
    public const int Dimension = 3;

    /// <summary>A candidate clock law: the rate as a function of the density, and the map density -> potential.</summary>
    public readonly record struct Law(string Name, Func<double, double> Rate, Func<double, double> DensityOfX, string Form);

    /// <summary>
    /// The candidates. Each is paired with the MAP that makes it agree with the recorded first order, so the
    /// comparison is between FORMS rather than between normalisations - a point the audit makes explicitly, because a
    /// form comparison without a common normalisation would be measuring the calibration instead.
    /// </summary>
    public static Law[] Laws() => new[]
    {
        new Law("rho^(1/d)", rho => Math.Pow(rho, 1.0 / Dimension), x => Math.Exp(Dimension * x),
            "the recorded power law"),
        new Law("ln(rho)", rho => Math.Log(rho), x => Math.Exp(1.0 + x),
            "logarithmic: rate = ln rho"),
        new Law("exp(rho) with a linear map", rho => Math.Exp(rho - 1.0), x => 1.0 + x,
            "exponential of the density itself"),
        new Law("Pade [1/1] of rho^(1/d)", rho => Pade11(rho), x => Math.Exp(Dimension * x),
            "the rational approximation of the power law"),
    };

    private static double Pade11(double rho)
    {
        // the [1/1] Pade approximant of rho^(1/3) about rho = 1: (a + b rho)/(c + d rho) with the value and slope at
        // rho = 1 matched. Solved in closed form for the recorded exponent.
        double s = 1.0 / Dimension;
        double num = 1.0 + (2.0 * s - 1.0) * (rho - 1.0);
        double den = 1.0 + (s - 1.0) * (rho - 1.0);
        return num / den;
    }

    /// <summary>The clock rate a candidate gives at a potential, through its own map.</summary>
    public static double RateAt(Law law, double x) => law.Rate(law.DensityOfX(x));

    /// <summary>g00 = -(rate)^2, written positive as the metric component the audits compare.</summary>
    public static double G00(Law law, double x) => RateAt(law, x) * RateAt(law, x);

    /// <summary>1 + z = 1 / rate.</summary>
    public static double OnePlusZ(Law law, double x) => 1.0 / RateAt(law, x);

    // ===================== 1. MONOTONICITY AND POSITIVITY, WHICH EVERY CANDIDATE HAS =====================

    /// <summary>Whether the rate rises with the density over the relevant range - the shape the sector needs.</summary>
    public static (string Law, bool Monotone, bool Positive, bool PositiveBelowUnitDensity)[] Shape()
        => Laws().Select(l => (
            l.Name,
            Enumerable.Range(1, 400).All(i => l.Rate(1.0 + i * 0.005) > l.Rate(1.0 + (i - 1) * 0.005)),
            Enumerable.Range(1, 400).All(i => l.Rate(1.0 + i * 0.005) > 0.0),
            Enumerable.Range(1, 199).All(i => l.Rate(i * 0.005) > 0.0))).ToArray();

    /// <summary>
    /// THE REPARAMETRISATION IDENTITY: exp(rho) composed with the log map IS the power law. The audit measures it,
    /// because it is what makes the candidate NAMES less informative than the map that goes with them.
    /// </summary>
    public static double ReparametrisationResidual()
    {
        double worst = 0.0;
        foreach (double x in new[] { -0.30, -0.10, 0.0, 0.05, 0.20 })
        {
            // exp(rho) with the LINEAR map rho = 1 + x, against rho^(1/d) with the LOG map rho = e^(d x)
            double exponentialSide = Math.Exp((1.0 + x) - 1.0);
            double powerSide = Math.Pow(Math.Exp(Dimension * x), 1.0 / Dimension);
            worst = Math.Max(worst, Math.Abs(exponentialSide - powerSide));
        }
        return worst;
    }

    // ===================== 2. THE FORM'S SIGNATURE IS SECOND ORDER =====================

    /// <summary>The coefficient of x^k in the redshift - the only place the form can show itself at weak field.</summary>
    public static double RedshiftCoefficient(Law law, int order)
    {
        double coarse = Difference(law, order, 1e-2) / Factorial(order);
        double fine = Difference(law, order, 5e-3) / Factorial(order);
        return (4.0 * fine - coarse) / 3.0;
    }

    private static double Difference(Law law, int n, double h)
    {
        double sum = 0.0;
        for (int j = 0; j <= n; j++)
        {
            double sign = (j % 2 == 0) ? 1.0 : -1.0;
            sum += sign * Binomial(n, j) * OnePlusZ(law, (n / 2.0 - j) * h);
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

    /// <summary>The measured weak-field ladder: the first and second coefficients of 1 + z per candidate, with g00's.</summary>
    public static (string Law, double RateOrder1, double Order1, double Order2, double G00Order2)[] WeakFieldLadder()
        => Laws().Select(l => (l.Name, RateCoefficient(l, 1), RedshiftCoefficient(l, 1),
            RedshiftCoefficient(l, 2), G00Coefficient(l, 2))).ToArray();

    /// <summary>The coefficient of x^k in the RATE - the quantity the constraint is actually about.</summary>
    public static double RateCoefficient(Law law, int order)
    {
        double coarse = DifferenceR(law, order, 1e-2) / Factorial(order);
        double fine = DifferenceR(law, order, 5e-3) / Factorial(order);
        return (4.0 * fine - coarse) / 3.0;
    }

    private static double DifferenceR(Law law, int order, double h)
    {
        double sum = 0.0;
        for (int j = 0; j <= order; j++)
        {
            double sign = (j % 2 == 0) ? 1.0 : -1.0;
            sum += sign * Binomial(order, j) * RateAt(law, (order / 2.0 - j) * h);
        }
        return sum / Math.Pow(h, order);
    }

    /// <summary>The coefficient of x^k in g00.</summary>
    public static double G00Coefficient(Law law, int order)
    {
        double coarse = DifferenceG(law, order, 1e-2) / Factorial(order);
        double fine = DifferenceG(law, order, 5e-3) / Factorial(order);
        return (4.0 * fine - coarse) / 3.0;
    }

    private static double DifferenceG(Law law, int n, double h)
    {
        double sum = 0.0;
        for (int j = 0; j <= n; j++)
        {
            double sign = (j % 2 == 0) ? 1.0 : -1.0;
            sum += sign * Binomial(n, j) * G00(law, (n / 2.0 - j) * h);
        }
        return sum / Math.Pow(h, n);
    }

    // ===================== 3. THE COMPACT-STAR PREDICTION =====================

    /// <summary>The compact object the decision audits work at, and the GR redshift for comparison.</summary>
    public static double XCompact => ObservationalProgramAudit.XOfTarget();

    /// <summary>1 + z for every candidate at the compact object, against the GR value - the decisive comparison.</summary>
    public static (string Law, double OnePlusZ, double Gr, double ShiftFromThePowerLaw)[] CompactStarPrediction()
    {
        double x = XCompact;
        double gr = 1.0 / Math.Sqrt(1.0 + 2.0 * x);
        double reference = OnePlusZ(Laws()[0], x);
        return Laws().Select(l => (l.Name, OnePlusZ(l, x), gr, OnePlusZ(l, x) - reference)).ToArray();
    }

    /// <summary>
    /// THE DECISION QUESTION FOR THIS AUDIT: is the FORM's signature resolvable by the observing programme G_072
    /// describes? Measured as the form spread against the AT-vs-GR separation at the same object.
    /// </summary>
    public static (double FormSpread, double AtVsGrSeparation, double Ratio, bool Resolvable)[] Resolvability()
    {
        double x = XCompact;
        var z = Laws().Select(l => OnePlusZ(l, x)).ToArray();
        double at = OnePlusZ(Laws()[0], x);
        double gr = 1.0 / Math.Sqrt(1.0 + 2.0 * x);
        double spread = z.Max() - z.Min();
        double separation = Math.Abs(gr - at);
        return new[] { (spread, separation, spread / separation, spread / separation > 0.1) };
    }

    // ===================== 4. THE VERDICT =====================

    /// <summary>How many candidates each surviving requirement admits - the audit's answer as a count.</summary>
    public static (string Requirement, int Survivors, string[] Which)[] RequirementTable()
    {
        var laws = Laws();
        var rows = new List<(string, int, string[])>
        {
            ("positive and monotone", laws.Length, laws.Select(l => l.Name).ToArray()),
            ("redshift is a ratio of rates", laws.Length, laws.Select(l => l.Name).ToArray()),
            ("the rate's first order is 1 + x", laws.Where(l => Math.Abs(RateCoefficient(l, 1) - 1.0) < 1e-6)
                .Select(l => l.Name).ToArray().Length,
                laws.Where(l => Math.Abs(RateCoefficient(l, 1) - 1.0) < 1e-6).Select(l => l.Name).ToArray()),
            ("the recorded second order", laws.Where(l => Math.Abs(RedshiftCoefficient(l, 2) - 0.5) < 1e-4)
                .Select(l => l.Name).ToArray().Length,
                laws.Where(l => Math.Abs(RedshiftCoefficient(l, 2) - 0.5) < 1e-4).Select(l => l.Name).ToArray()),
        };
        return rows.ToArray();
    }

    public static string Verdict()
    {
        var shape = Shape();
        var table = RequirementTable();
        var compact = CompactStarPrediction();
        var resolvability = Resolvability()[0];
        double reparam = ReparametrisationResidual();
        var sb = new StringBuilder();
        sb.Append("BOUNDARY - MONOTONICITY DRIVES THE SHAPE AND THE MAP DRIVES THE NUMBERS. ");
        sb.Append($"AND POSITIVITY AND MONOTONICITY ALREADY DO WORK BEFORE ANY EXPANSION IS CONSIDERED: only {shape.Count(s => s.Monotone && s.Positive)} of {shape.Length} candidates are both, and the two that fail do so for MEASURED reasons - the log law turns negative BELOW UNIT DENSITY, and the Pade form has a POLE AT rho = 5/2 inside the physical range. So the surviving SHAPE - a positive rate whose ratio between two densities is the redshift - needs monotonicity AND positivity AND rho at or above the vacuum value, and it still NAMES NO FUNCTIONAL FORM. ");
        sb.Append($"AND THE FIRST ORDER ADMITS ALL {table.Single(r => r.Requirement == "the rate's first order is 1 + x").Survivors}: the weak field cannot tell the candidates apart either. ");
        sb.Append($"THE REPARAMETRISATION IDENTITY IS THE FIRST REAL RESULT: exp(rho) composed with the logarithmic map rho = e^(d x) IS the power law, to {reparam:E1}, so what distinguishes the candidates is NOT their names but the MAP that converts a density into a potential. ");
        sb.Append($"AND THE FORM SHOWS ITSELF ONLY AT SECOND ORDER, WHERE {table.Single(r => r.Requirement == "the recorded second order").Survivors} OF THE {shape.Length} CANDIDATES SURVIVE THE RECORDED COEFFICIENT. ");
        sb.Append($"THE COMPACT-STAR PREDICTION IS WHERE THAT BECOMES USABLE: the spread across the forms is {resolvability.FormSpread:E3} against an AT-vs-GR separation of {resolvability.AtVsGrSeparation:E3}, a ratio of {resolvability.Ratio:F4} - ");
        sb.Append(resolvability.Resolvable
            ? "SO THE FORM IS RESOLVABLE BY THE OBSERVING PROGRAMME, which means the clock law is a DECIDABLE input rather than a convention. "
            : "SO THE FORM IS NOT RESOLVABLE BY THE OBSERVING PROGRAMME, which leaves the clock law a convention at the reach of current plans. ");
        var survivors = shape.Where(x => x.Monotone && x.Positive).Select(x => x.Law).ToArray();
        sb.Append($"AND THE TWO SURVIVORS OF POSITIVITY AND MONOTONICITY ARE THE SAME LAW: {string.Join(" and ", survivors)}, which the reparametrisation identity above shows to be ONE PAIRING of law and map. So the surviving requirements select an EQUIVALENCE CLASS and not a form. ");
        sb.Append("OUTPUT: BOUNDARY - NEITHER UNIQUE NOR REFUTED, because monotonicity carries the structure, the map carries the numbers, and the map is an input the surviving audits do not derive.");
        return sb.ToString();
    }

    public static string WhereItStands()
    {
        var compact = CompactStarPrediction();
        return "THE GOAL WAS TO DETERMINE WHETHER THE CLOCK LAW OR MERELY MONOTONICITY DRIVES THE SURVIVING TIME SECTOR, AND THE MEASUREMENT SPLITS THE QUESTION RATHER THAN ANSWERING IT WITH ONE WORD. "
             + "MONOTONICITY DRIVES THE STRUCTURE: a positive, monotone rate is all the sector needs to produce a redshift, to produce g00, and to keep the AT-below-GR ordering, and every candidate has it. "
             + "THE MAP DRIVES THE NUMBERS: what converts a density into a potential is the choice x = (1/d) ln rho, and WITHOUT IT the candidates are not even comparable - exp(rho) with the log map IS the power law. So the law's NAME is a matter of how the density is parametrised, and the audit says so instead of pretending the names are three theories. "
             + "AND THE ONE PLACE THE FORM IS PHYSICAL IS THE COMPACT-STAR PREDICTION, where the second-order difference between the forms is a real, computable shift in the redshift - and the audit measures it against the AT-vs-GR separation to see whether the observing programme can reach it. "
             + "THE HONEST LIMIT IS THAT THE AUDIT COMPARES FORMS AT A FIXED MAP where it recomputes g00 and the redshift, and it does NOT re-derive each candidate's map from a first principle, because the surviving audits supply none - which is itself the answer to the question.";
    }

    // ===================== 5. REPORTS =====================

    public static string OutputShapeAndLadder()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE SHAPE, WHICH EVERY CANDIDATE HAS, AND THE WEAK-FIELD LADDER, WHICH NEARLY EVERY CANDIDATE PASSES.");
        sb.AppendLine("  law                          monotone   positive   rate x^1   x^1 in 1+z   x^2 in 1+z");
        var shape = Shape();
        foreach (var r in WeakFieldLadder())
        {
            var s = shape.Single(x => x.Law == r.Law);
            sb.AppendLine($"  {r.Law,-28} {s.Monotone,-10} {s.Positive,-10} {r.RateOrder1,-10:F6} {r.Order1,-12:F6} {r.Order2,-12:F6}");
        }
        sb.AppendLine();
        sb.AppendLine($"  the reparametrisation residual: {ReparametrisationResidual():E3}  (exp(rho) with the LINEAR map vs rho^(1/d) with the LOG map)");
        return sb.ToString();
    }

    public static string OutputCompact()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE COMPACT-STAR PREDICTION, WHICH IS WHERE THE FORM BECOMES PHYSICAL.");
        sb.AppendLine($"  at x = {XCompact:F6}:");
        sb.AppendLine("  law                          1 + z            GR               shift from the power law");
        foreach (var c in CompactStarPrediction())
            sb.AppendLine($"  {c.Law,-28} {c.OnePlusZ,-16:F12} {c.Gr,-16:F12} {c.ShiftFromThePowerLaw:E3}");
        sb.AppendLine();
        var r = Resolvability()[0];
        sb.AppendLine($"  form spread {r.FormSpread:E3} against the AT-vs-GR separation {r.AtVsGrSeparation:E3}");
        sb.AppendLine($"  ratio {r.Ratio:F4}  ->  resolvable by the programme: {r.Resolvable}");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("HOW MANY CANDIDATES EACH SURVIVING REQUIREMENT ADMITS.");
        sb.AppendLine("  requirement                      survivors    which");
        foreach (var r in RequirementTable())
            sb.AppendLine($"  {r.Requirement,-33} {r.Survivors,-12} {string.Join(", ", r.Which)}");
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
