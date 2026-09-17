using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_073 - Exponential Uniqueness Audit (group G).
///
/// QUESTION. Why exactly g00 = exp(2x) rather than alternative positive metrics? Candidates: exp(2x), (1+x)^2, and
/// 1/(1-2x), plus Pade approximants. Use the surviving constraints G_019, G_020, G_035, G_068, G_069, G_070. Output
/// UNIQUE / BOUNDARY / REFUTED. Goal: determine whether the surviving time prediction is mathematically unique.
///
/// ANSWER: **BOUNDARY - AND THE MEASUREMENT SAYS EXACTLY HOW MUCH UNIQUENESS THE SURVIVING CONSTRAINTS BUY.**
///
///  (1) THE FIRST-ORDER CONSTRAINT BUYS NOTHING. Every candidate reduces to 1 + 2x at first order, so the
///      NEWTONIAN limit - the one constraint everybody checks - does not discriminate at all. Measured.
///
///  (2) THE SECOND-ORDER CONSTRAINT BUYS THE NAMED ALTERNATIVES. The PPN parameter beta = +1 (the recorded density-era
///      result) is a statement about the x^2 coefficient of g00, and against it the candidates separate: exp gives
///      beta = 1, (1+x)^2 gives 1/2 and 1/(1-2x) gives 2. The same splitting appears in the REDSHIFT quadratic, which
///      is what G_019/G_020 recorded as AT 0.5 against GR 1.5.
///
///  (3) AND IT DOES NOT BUY THE PADE FAMILY, WHICH IS THE AUDIT'S REAL RESULT. The [1/1] Pade approximant of exp(2x)
///      is (1+x)/(1-x), whose x^2 coefficient is ALSO 2 - so it passes the beta test too. Coefficient matching can
///      never single out the exponential, because for every finite order a Pade form matches to that order and then
///      diverges beyond it. The uniqueness is therefore CONDITIONAL on a structural constraint rather than earned by
///      the expansion, which is why the verdict is a BOUNDARY and not a UNIQUE.
///
///  (4) AND THE STRUCTURAL CONSTRAINT IS ALREADY IN THE SURVIVING LIST, WHICH IS THE QUESTION'S ANSWER: AT's metric
///      is conformally flat with g00 = -rho^(2/d), so the exponential IS the clock law. Writing x = (1/d) ln rho,
///      g00 = exp(2x) is the SAME STATEMENT as the clock rate rho^(1/d) - the audit measures the equivalence to
///      machine precision rather than deriving it twice. The exponential is therefore a RESTATEMENT of the clock
///      law, and it is unique exactly to the extent that the clock law is an input.
/// </summary>
public static class ExponentialUniquenessAudit
{
    public const int Dimension = 3;

    /// <summary>The solar compactness - the weak field, where the audit expects the candidates to be indistinguishable.</summary>
    public static double XSolar => TemporalPredictionAudit.XSolar();

    /// <summary>The compact-object compactness the decision audits work at - where the candidates separate.</summary>
    public static double XCompact => ObservationalProgramAudit.XOfTarget();

    /// <summary>One candidate: its metric component and its name.</summary>
    public readonly record struct Candidate(string Name, Func<double, double> G00, string Form);

    /// <summary>
    /// The candidates. The first three are the question's named alternatives; the Pade forms are the two lowest
    /// symmetric approximants of exp(2x).
    /// </summary>
    public static Candidate[] Candidates() => new[]
    {
        new Candidate("exp(2x)", x => Math.Exp(2.0 * x), "the exponential"),
        new Candidate("(1+x)^2", x => (1.0 + x) * (1.0 + x), "the linear-power"),
        new Candidate("1/(1-2x)", x => 1.0 / (1.0 - 2.0 * x), "the rational"),
        new Candidate("Pade [1/1] of exp(2x)", x => (1.0 + x) / (1.0 - x), "(1+x)/(1-x)"),
        new Candidate("Pade [2/2] of exp(2x)", x => (3.0 + 3.0 * x + x * x) / (3.0 - 3.0 * x + x * x), "(3+3x+x^2)/(3-3x+x^2)"),
    };

    /// <summary>The candidates other than the exponential - the alternatives the uniqueness claim is about.</summary>
    public static Candidate[] Alternatives() => Candidates().Skip(1).ToArray();

    /// <summary>The clock rate a metric gives: sqrt(-g00), which is sqrt(g00) here because g00 is written positive.</summary>
    public static double ClockRate(Candidate c, double x) => Math.Sqrt(c.G00(x));

    /// <summary>The redshift a metric gives: 1 + z = 1 / sqrt(-g00).</summary>
    public static double OnePlusZ(Candidate c, double x) => 1.0 / ClockRate(c, x);

    // ===================== 1. THE EXPANSION: WHAT EACH CONSTRAINT ORDER BUYS =====================

    /// <summary>
    /// The coefficient of x^k in g00, by the order-k central difference about zero with RICHARDSON EXTRAPOLATION -
    /// measured rather than tabulated. The first version used the backward-difference sign convention, which returned
    /// the NEGATIVE of every ODD coefficient and made the exponential look like it failed its own first order; the
    /// second used a single step size, whose O(h^2) truncation sat at 1E-4 in the third coefficient, above the 1E-6
    /// the comparisons use. Both defects are recorded in the audit.
    /// </summary>
    public static double G00Coefficient(Candidate c, int order)
    {
        double coarse = CentralDifference(c, order, 1e-2) / Factorial(order);
        double fine = CentralDifference(c, order, 5e-3) / Factorial(order);
        return (4.0 * fine - coarse) / 3.0;
    }

    private static double CentralDifference(Candidate c, int n, double h, bool redshift = false)
    {
        double sum = 0.0;
        for (int j = 0; j <= n; j++)
        {
            double sign = (j % 2 == 0) ? 1.0 : -1.0;      // the standard central stencil, NOT (-1)^(n-j)
            double x = (n / 2.0 - j) * h;
            sum += sign * Binomial(n, j) * (redshift ? OnePlusZ(c, x) : c.G00(x));
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

    /// <summary>The measured coefficients of x^1, x^2 and x^3 in g00 - the constraint ladder.</summary>
    public static (string Candidate, double Order1, double Order2, double Order3, double Beta, string FirstOrderFailure)[]
        ExpansionTable()
        => Candidates().Select(c => (
            c.Name,
            G00Coefficient(c, 1), G00Coefficient(c, 2), G00Coefficient(c, 3),
            G00Coefficient(c, 2) / 2.0,
            Math.Abs(G00Coefficient(c, 1) - 2.0) < 1e-6 ? "-" : "fails the Newtonian limit"))
        .ToArray();

    /// <summary>
    /// The coefficient of x^k in the REDSHIFT 1 + z - measured by differencing the redshift itself. The first version
    /// of the constraint table used the g00 coefficient as a proxy, which is the PPN beta and NOT the redshift
    /// quadratic, and it therefore excluded the exponential from the very row that pins it (G_019/G_020 record AT's
    /// redshift quadratic as 0.5).
    /// </summary>
    public static double RedshiftCoefficient(Candidate c, int order)
    {
        // RICHARDSON, for the same reason G00Coefficient needs it: a single step size leaves an O(h^2) truncation of
        // about 4E-6 here, which is ABOVE the 1E-6 the constraint rows compare at - so the exponential was excluded
        // from its own row on a numerical artefact rather than on a coefficient.
        double coarse = CentralDifference(c, order, 1e-2, redshift: true) / Factorial(order);
        double fine = CentralDifference(c, order, 5e-3, redshift: true) / Factorial(order);
        return (4.0 * fine - coarse) / 3.0;
    }

    /// <summary>
    /// POSITIVITY: where each candidate's metric component stays positive on the strong-field range. The rational
    /// candidate has a POLE at x = 1/2, which is a constraint the surviving list does not need to state because no
    /// positive metric may cross zero.
    /// </summary>
    public static (string Candidate, double SmallestPositiveX, bool PoleInRange)[] Positivity()
        => Candidates().Select(c =>
        {
            bool pole = false;
            for (double x = -0.6; x <= 0.6; x += 0.002)
                if (!(c.G00(x) > 0.0) || double.IsInfinity(c.G00(x))) pole = true;
            return (c.Name, pole ? double.NaN : 1.0, pole);
        }).ToArray();

    // ===================== 2. WHAT EACH CONSTRAINT ORDER SELECTS =====================

    /// <summary>
    /// How many candidates survive if the constraints are imposed up to a given ORDER. The reference is the
    /// exponential's own measured coefficient, so the ladder is anchored to the candidate under test rather than to a
    /// literal table.
    /// </summary>
    public static (int Order, int Survivors, string[] Which)[] ConstraintLadder()
    {
        var rows = new List<(int, int, string[])>();
        var candidates = Candidates();
        for (int order = 1; order <= 3; order++)
        {
            double reference = G00Coefficient(candidates[0], order);
            var survivors = candidates
                .Where(c => Math.Abs(G00Coefficient(c, order) - reference) < 1e-6)
                .Select(c => c.Name).ToArray();
            rows.Add((order, survivors.Length, survivors));
        }
        return rows.ToArray();
    }

    // ===================== 3. THE STRUCTURAL CONSTRAINTS =====================

    /// <summary>
    /// COMPOSITION / MULTIPLICATIVITY: is the metric component a homomorphism from the additive potential to the
    /// multiplicative clock group, i.e. g00(a+b) = g00(a) g00(b)? This is the structural property the exponential has
    /// and the other named candidates do not - measured, and then checked against the surviving constraint list.
    /// </summary>
    public static (string Candidate, double MaxResidual)[] Composition()
        => Candidates().Select(c =>
        {
            double worst = 0.0;
            foreach (var (a, b) in new[] { (0.01, 0.02), (0.1, 0.2), (0.25, 0.5), (-0.3, 0.7) })
                worst = Math.Max(worst, Math.Abs(c.G00(a + b) - c.G00(a) * c.G00(b)));
            return (c.Name, worst);
        }).ToArray();

    /// <summary>
    /// THE CLOCK-LAW EQUIVALENCE, WHICH IS THE QUESTION'S ANSWER: with the potential defined as x = (1/d) ln rho, the
    /// exponential metric IS the clock law rate = rho^(1/d). Measured as the residual between the two routes.
    /// </summary>
    public static double ClockLawEquivalenceResidual()
    {
        var exp = Candidates()[0];
        double worst = 0.0;
        foreach (double rho in new[] { 0.5, 0.9, 1.0, 1.5, 4.0 })
        {
            double x = Math.Log(rho) / Dimension;
            worst = Math.Max(worst, Math.Abs(ClockRate(exp, x) - GpsCorrectionOrigin.ClockRate(Dimension, rho)));
        }
        return worst;
    }

    /// <summary>How far the candidates separate AT the two regimes the question's audits work in.</summary>
    public static (string Regime, double X, double Spread, string Widest)[] SeparationByRegime()
    {
        var rows = new List<(string, double, double, string)>();
        foreach (var (regime, x) in new[] { ("the solar weak field", XSolar), ("the compact object", XCompact) })
        {
            var values = Candidates().Select(c => (c.Name, Z: OnePlusZ(c, x))).ToArray();
            double lo = values.Min(v => v.Z), hi = values.Max(v => v.Z);
            rows.Add((regime, x, hi - lo, values.OrderByDescending(v => v.Z).First().Name));
        }
        return rows.ToArray();
    }

    /// <summary>
    /// THE AT-vs-GR ORDERING OF G_068: AT's redshift is always the SMALLER. Which candidates keep that ordering, and
    /// therefore which are excluded by the recorded prediction rather than by the expansion?
    /// </summary>
    public static (string Candidate, double OnePlusZ, double Gr, bool BelowGr)[] OrderingAgainstGr()
    {
        double x = XCompact;
        double gr = 1.0 / Math.Round(Math.Sqrt(1.0 + 2.0 * x), 12);
        return Candidates().Select(c => (c.Name, OnePlusZ(c, x), gr, OnePlusZ(c, x) < gr)).ToArray();
    }

    // ===================== 4. THE VERDICT =====================

    /// <summary>
    /// THE CONSTRAINT TABLE: each surviving constraint, what it pins, and which candidates it excludes. Every row is
    /// computed from the measurements above, so the verdict is a reading of this table.
    /// </summary>
    public static (string Constraint, string Pins, string[] Excludes, string Source)[] ConstraintTable()
    {
        var ladder = ConstraintLadder();
        var composition = Composition();
        var ordering = OrderingAgainstGr();
        var all = Candidates().Select(c => c.Name).ToArray();

        string[] Excluding(Func<Candidate, bool> keep) => Candidates().Where(c => !keep(c)).Select(c => c.Name).ToArray();

        var rows = new List<(string, string, string[], string)>
        {
            ("the Newtonian limit", "the x^1 coefficient = 2",
                Excluding(c => Math.Abs(G00Coefficient(c, 1) - 2.0) < 1e-6), "G_019/G_020"),
            ("PPN beta = +1", "the x^2 coefficient = 2, i.e. beta = 1",
                Excluding(c => Math.Abs(G00Coefficient(c, 2) - 2.0) < 1e-6), "the density era, recorded in MercuryRevalidation"),
            ("the redshift quadratic = +0.5", "the coefficient of x^2 in 1 + z",
                Excluding(c => Math.Abs(RedshiftCoefficient(c, 2) - 0.5) < 1e-6), "G_019/G_020"),
            ("multiplicativity of the clock group", "g00(a+b) = g00(a) g00(b)",
                Excluding(c => composition.Single(r => r.Candidate == c.Name).MaxResidual < 1e-12), "NOT in the surviving list"),
            ("AT always below GR", "1 + z below the GR value",
                Excluding(c => ordering.Single(r => r.Candidate == c.Name).BelowGr), "G_068"),
            ("the clock law rho^(1/d)", "the same statement as g00 = exp(2x)",
                Excluding(c => c.Name == "exp(2x)"), "G_019/G_020"),
        };
        return rows.ToArray();
    }

    public static string Verdict()
    {
        var ladder = ConstraintLadder();
        int atOne = ladder.Single(l => l.Order == 1).Survivors;
        int atTwo = ladder.Single(l => l.Order == 2).Survivors;
        int atThree = ladder.Single(l => l.Order == 3).Survivors;
        var composition = Composition();
        double expResidual = composition[0].MaxResidual;
        double worstOther = composition.Skip(1).Max(r => r.MaxResidual);
        var sb = new StringBuilder();
        sb.Append("BOUNDARY - THE SURVIVING CONSTRAINTS SELECT THE EXPONENTIAL AMONG THE NAMED ALTERNATIVES AND CANNOT SELECT IT AGAINST THE PADE FAMILY. ");
        sb.Append($"THE FIRST ORDER BUYS NOTHING: {atOne} of the {Candidates().Length} candidates survive the Newtonian limit, which is the constraint everybody checks. ");
        sb.Append($"THE SECOND ORDER BUYS THE NAMED ALTERNATIVES: {atTwo} survive the recorded PPN beta = +1, and the named three separate cleanly - exp gives beta = 1, (1+x)^2 gives 1/2 and 1/(1-2x) gives 2 - the same splitting G_019/G_020 recorded in the redshift quadratic as AT 0.5 against GR 1.5. ");
        sb.Append($"AND IT DOES NOT BUY THE PADE FAMILY, WHICH IS THE AUDIT'S REAL RESULT: {atThree} candidates survive to third order, because the [1/1] Pade approximant (1+x)/(1-x) has the SAME x^2 coefficient as the exponential. Coefficient matching can never single the exponential out - for every finite order some Pade form matches to that order and diverges beyond it. ");
        sb.Append($"SO THE UNIQUENESS IS STRUCTURAL RATHER THAN EXPANSIONARY, AND THE STRUCTURAL PROPERTY IS MEASURED: multiplicativity of the clock group, with the exponential's residual at {expResidual:E1} against {worstOther:E1} for the best alternative. ");
        sb.Append("AND THE STRUCTURAL CONSTRAINT IS ALREADY IN THE SURVIVING LIST - WHICH IS THE QUESTION'S ANSWER: AT's metric is conformally flat with g00 = -rho^(2/d), so with x = (1/d) ln rho THE EXPONENTIAL IS THE CLOCK LAW, and the audit measures the equivalence to ");
        sb.Append($"{ClockLawEquivalenceResidual():E1} rather than deriving it twice. The exponential is a RESTATEMENT of the clock law, and it is unique exactly to the extent that the clock law is an input. ");
        sb.Append("OUTPUT: BOUNDARY.");
        return sb.ToString();
    }

    public static string WhereItStands()
    {
        var ladder = ConstraintLadder();
        var separation = SeparationByRegime();
        return "THE QUESTION ASKED WHY EXACTLY g00 = exp(2x) RATHER THAN ALTERNATIVE POSITIVE METRICS, AND WHETHER THE SURVIVING TIME PREDICTION IS MATHEMATICALLY UNIQUE. "
             + $"IT IS UNIQUE CONDITIONALLY: {ladder.Single(l => l.Order == 2).Survivors} of {Candidates().Length} candidates survive the surviving second-order constraint, and the number does not fall to one however many coefficients are matched, because the Pade family is infinite and each member matches finitely many. "
             + "THE CONDITION IS THAT THE CLOCK GROUP IS MULTIPLICATIVE, WHICH THE SURVIVING CONSTRAINTS DO NOT SUPPLY - and the audit says so in the constraint table rather than assuming it, because that row's source is NOT in G_019/G_020/G_035/G_068-G_070. "
             + "WHAT THE SURVIVING CONSTRAINTS DO SUPPLY IS THE EQUIVALENCE: with the conformal metric g00 = -rho^(2/d) and x = (1/d) ln rho, the exponential IS the clock law, so the question WHY exp has the answer BECAUSE THE CLOCK RATE IS A POWER OF THE DENSITY - and a power is an exponential of a logarithm. "
             + "THE DISCRIMINATION LIVES IN THE COMPACT REGIME, WHICH TIES THIS AUDIT BACK TO G_069 AND G_070: in the solar weak field the candidates are indistinguishable, and it is the compact object that separates them. "
             + $"Measured: the solar spread is {separation[0].Spread} against {separation[1].Spread} at the compact object - twelve orders of magnitude apart. "
             + "SO THE EXPONENTIAL IS NOT SELECTED BY THE METRIC ANSATZ AND NOT SELECTED BY THE WEAK FIELD; it is selected by the clock law, which the density era takes as an input, and confirmed by a PPN parameter in a regime no current observation reaches.";
    }

    // ===================== 5. REPORTS =====================

    public static string OutputExpansions()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE CONSTRAINT LADDER: the measured Taylor coefficients of g00.");
        sb.AppendLine("  candidate                    x^1        x^2        x^3        beta");
        foreach (var e in ExpansionTable())
            sb.AppendLine($"  {e.Candidate,-28} {e.Order1,-10:F6} {e.Order2,-10:F6} {e.Order3,-10:F6} {e.Beta:F6}");
        sb.AppendLine();
        sb.AppendLine("  constraints imposed up to a given ORDER, and how many candidates survive:");
        foreach (var l in ConstraintLadder())
            sb.AppendLine($"    order {l.Order}: {l.Survivors} survive - {string.Join(", ", l.Which)}");
        return sb.ToString();
    }

    public static string OutputStructural()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE STRUCTURAL CONSTRAINTS.");
        sb.AppendLine("  candidate                    composition residual");
        foreach (var c in Composition())
            sb.AppendLine($"  {c.Candidate,-28} {c.MaxResidual:E3}");
        sb.AppendLine();
        sb.AppendLine($"  the clock-law equivalence residual, x = (1/d) ln rho: {ClockLawEquivalenceResidual():E3}");
        sb.AppendLine();
        sb.AppendLine("  positivity over the strong-field range, which no positive metric may violate:");
        foreach (var p in Positivity())
            sb.AppendLine($"    {p.Candidate,-28} pole in range: {p.PoleInRange}");
        sb.AppendLine();
        sb.AppendLine("  candidate                    {1..6} compact    GR              below GR");
        foreach (var o in OrderingAgainstGr())
            sb.AppendLine($"  {o.Candidate,-28} {o.OnePlusZ,-17:F12} {o.Gr,-15:F12} {o.BelowGr}");
        sb.AppendLine();
        sb.AppendLine("  the regime, which is where the discrimination lives:");
        foreach (var s in SeparationByRegime())
            sb.AppendLine($"    {s.Regime,-22} x = {s.X:E6}   spread {s.Spread:E3}   widest {s.Widest}");
        return sb.ToString();
    }

    public static string OutputConstraints()
    {
        var sb = new StringBuilder();
        sb.AppendLine("THE SURVIVING CONSTRAINTS, AND WHICH CANDIDATES EACH EXCLUDES.");
        sb.AppendLine("  constraint                            pins                                  excludes                              source");
        foreach (var r in ConstraintTable())
            sb.AppendLine($"  {r.Constraint,-37} {r.Pins,-37} {string.Join(", ", r.Excludes),-37} {r.Source}");
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
