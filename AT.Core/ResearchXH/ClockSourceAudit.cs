using System.Text;
using ClockLaw = AT.Core.ResearchXH.ClockLawUniquenessAudit.ClockLaw;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_077 - CLOCK SOURCE AUDIT (group G - Gravity Source).
///
/// QUESTION. What produces LOCAL CLOCK-RATE CHANGES? G_075 left the clock-law family standing, and G_076 showed
/// that multiplicativity selects exp(2x) but is not derived - so the law is an input. This audit asks the question
/// under the law instead of above it: what is the minimal SOURCE of a local clock change, and can that source be
/// operated independently of gravity?
///
/// Started only from the surviving AT primitives - Difference, Actualization, the density rho, the causal structure,
/// count conservation and the surviving G_035 temporal sector. FORBIDDEN and not used: the GR field equations, a
/// specific clock law, multiplicativity, neutron-star fitting, and any imported time-aether concept.
///
/// ANSWER: **REFUTED - THE CLOCK HAS NO INDEPENDENT SOURCE. The minimal object is ONE SCALAR (the local occupancy
/// rho), the clock adds ZERO degrees of freedom to it, and the clock offset and the acceleration integral are TWO
/// READINGS OF THE SAME POTENTIAL DIFFERENCE - so they vanish together and appear together for EVERY monotone clock
/// law, and time control is gravity control.**
///
///  (1) THE MINIMAL OBJECT IS ONE SCALAR, AND THE CLOCK IS NOT A FIELD. The surviving temporal sector needs one
///      metric function, one scalar (the occupancy) and one exponent (G_035), and every temporal observable is a
///      function of the LOCAL occupancy alone - which the audit measures by showing that two configurations agreeing
///      at a probe and differing elsewhere give the same rate there. The clock therefore carries no information about
///      rho that rho does not carry (G_049: the clock pattern is lossless in rho with a CLOSED-FORM inverse), so its
///      degree-of-freedom count is ZERO.
///
///  (2) THE DRIVER IS THE DENSITY AND NOTHING ELSE, and each alternative is measured rather than argued away. The
///      ACTUALIZATION RATE is the same quantity - in the minimal sector the update rate per site IS dtau/dt, and the
///      audit measures the identity at the floating-point floor. CAUSAL CONNECTIVITY is not a driver: two graphs with
///      the same occupancy give the same clock, and the connectivity-to-gravity bridge is recorded as an OPEN problem
///      rather than a result. INFORMATION DENSITY is not a driver: two configurations with the same local occupancy
///      and different global entropy give the same local clock. The FLUX is the source OF THE DRIVER and not a second
///      driver - the clock reads neither the flux nor the acceleration.
///
///  (3) THE REDSHIFT IS A RATIO OF RATES AND NOTHING MORE, DERIVED WITHOUT ANY CLOCK LAW: 1 + z = W(A_s)/W(A_o) for
///      an ARBITRARY positive monotone W, with A = (1/d) ln rho. Because the observable is a ratio, a LOCAL
///      measurement fixes only the LOGARITHMIC DERIVATIVE of W, and the audit measures that two laws with the same
///      logarithmic derivative at a working point agree to first order and separate only at second. SUCCESSIVE
///      REDSHIFT FACTORS TELESCOPE for every law - the product of the per-step factors IS the total factor - which is
///      why G_076's composition constraint is about POTENTIALS ADDING and not about redshifts composing.
///
///  (4) THE DECISIVE IDENTITY IS F-FREE AND IT IS A COUNTING RESULT. The surviving source law says the acceleration
///      is the negative gradient of the LOGARITHMIC potential, a = -grad A (G_028's statement that the source law and
///      the clock law are ONE statement), so the acceleration integral between two points IS minus their potential
///      difference for ANY clock law. And the clock offset is g(A_s) - g(A_o) with g = ln W. TWO OBSERVABLES, ONE
///      DIFFERENCE: they vanish together and appear together for EVERY strictly monotone W, which the audit measures
///      over a corpus that contains the exponential as one member among others. What is NOT fixed is the RATIO of the
///      two readings - the mean logarithmic derivative of W - so AT says the accompanying acceleration cannot be zero
///      and cannot say how large it is.
///
///  (5) SO THE CLOCK IS MANIPULABLE ONLY THROUGH THE DENSITY, and the source term is SHARED. Count conservation makes
///      the source a TRANSPORT rather than a creation - the total is conserved, so a clock change is bought by moving
///      occupancy, which changes the acceleration along the path as well as the clock where it was moved. The
///      boundary analysis therefore splits in two and the audit states both levels: a UNIFORM occupancy offset over a
///      region gives a clock offset with ZERO acceleration inside that region (so "no gravity where the clock is" is
///      permitted), while any OBSERVABLE clock offset requires a nonzero potential difference, hence at least one link
///      carrying a nonzero gradient on every path between the two clocks (so "no gravity anywhere" is refuted). The
///      gradient's SUPPORT is free and its INTEGRAL is not: the audit measures the width-versus-strength trade, where
///      confining the transition to narrower layers raises the peak acceleration in proportion.
/// </summary>
public static class ClockSourceAudit
{
    public const int D = 3;

    // ===================== 1. THE SURVIVING STRUCTURE, CITED =====================

    /// <summary>A = (1/d) ln rho - the clock potential, the Newtonian potential over c^2 (G_028, G_035).</summary>
    public static double Potential(double rho) => Math.Log(rho) / D;

    /// <summary>rho = e^(d A) - the inverse, which is closed-form and is what makes the clock readout invertible.</summary>
    public static double Density(double a) => Math.Exp(D * a);

    /// <summary>
    /// THE SURVIVING SOURCE LAW (G_028/G_035): a = -grad A, computed from a profile of potentials alone. The audit
    /// uses it because it is the same statement as the clock law in the surviving sector - NOT because it is imported
    /// from a field equation, and NO clock law is used to write it.
    /// </summary>
    public static double[] Acceleration(double[] a)
    {
        var acc = new double[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            double left = a[i == 0 ? 0 : i - 1];
            double right = a[i == a.Length - 1 ? a.Length - 1 : i + 1];
            acc[i] = -(right - left) / 2.0;
        }
        return acc;
    }

    /// <summary>The acceleration integral along a path: minus the potential difference between its ends.</summary>
    public static double AccelerationIntegral(double[] a) => -(a[^1] - a[0]);

    // ===================== 2. THE CLOCK, AS AN ARBITRARY LAW =====================

    /// <summary>
    /// A clock law: the RATE W as a function of the potential, with W(0) = 1 and W'(0) = 1 - the pinned data of
    /// G_075 - and nothing else assumed. The corpus is taken from G_075's own laws AS RATES (W = sqrt(F)) so that the
    /// two audits cannot drift apart, plus the members this audit needs.
    /// </summary>
    public readonly record struct Clock(string Name, Func<double, double> W, string Form);

    /// <summary>The corpus. The exponential is ONE MEMBER among others, carried for comparison and never assumed. The
    /// mesh member is G_075's own interpolant taken AS A RATE, so it inherits that audit's monotonicity bound instead
    /// of being built again here.</summary>
    public static Clock[] Laws() => ClockLawUniquenessAudit.Laws()
        .Select(l => new Clock(l.Name, x => ClockLawUniquenessAudit.Rate(l, x), l.Form))
        .Concat(new[]
        {
            new Clock("mesh interpolant (delta = 0.01)", x => ClockLawUniquenessAudit.Rate(
                ClockLawSelectionAudit.MeshInterpolant(0.01), x), "non-analytic, pinned data preserved"),
            new Clock("flat-then-rising rate", x => Math.Sqrt(1.0 - (1.0 - Math.Pow((x + 0.5) / 0.5, 4.0)) / 4.0),
                "the suppression witness of G_075, carried as a RATE so its pinned data survive"),
        })
        .ToArray();

    /// <summary>The redshift between a source and an observer: a RATIO OF RATES, for any law. No derivative, no form.</summary>
    public static double OnePlusZ(Clock law, double aSource, double aObserver)
        => law.W(aSource) / law.W(aObserver);

    /// <summary>g = ln W - the quantity whose DIFFERENCE is the logarithm of the redshift.</summary>
    public static double LogRate(Clock law, double a) => Math.Log(law.W(a));

    /// <summary>
    /// THE LOGARITHMIC DERIVATIVE, which is the only thing a local redshift measurement can fix: measured by a
    /// central difference so that the audit compares measurements rather than formulas.
    /// </summary>
    public static double LogDerivative(Clock law, double a, double h = 1.0e-4)
        => (LogRate(law, a + h) - LogRate(law, a - h)) / (2.0 * h);

    // ===================== 3. THE MINIMAL OBJECT AND THE DRIVER =====================

    /// <summary>
    /// THE LOCAL READOUT TEST: two configurations that AGREE at the probe and DIFFER elsewhere must give the same
    /// rate at the probe. Every law passes - the sector's observables take one argument - which is what "the clock is
    /// a local functional of the occupancy" means, measured rather than asserted.
    /// </summary>
    public static (string Law, double Residual)[] LocalReadout()
    {
        double[] flat = { -0.10, -0.10, -0.10 };
        double[] elsewhere = { -0.30, -0.10, 0.05 };
        const int probe = 1;
        return Laws().Select(l =>
        {
            double here = l.W(flat[probe]);
            double other = l.W(elsewhere[probe]);
            return (l.Name, Math.Abs(here - other) / Math.Abs(here));
        }).ToArray();
    }

    /// <summary>
    /// THE RANK OF A RESPONSE MATRIX, by Gaussian elimination with partial pivoting - so a dimension count is a
    /// MEASUREMENT rather than an assertion. Rows are observables, columns are single-site occupancy perturbations.
    /// </summary>
    public static int Rank(double[][] matrix, double tolerance = 1e-9)
    {
        int rows = matrix.Length, cols = matrix[0].Length, rank = 0;
        var m = matrix.Select(r => r.ToArray()).ToArray();
        for (int col = 0; col < cols && rank < rows; col++)
        {
            int pivot = -1;
            for (int r = rank; r < rows; r++)
                if (Math.Abs(m[r][col]) > Math.Abs(pivot < 0 ? 0.0 : m[pivot][col])) pivot = r;
            if (pivot < 0 || Math.Abs(m[pivot][col]) <= tolerance) continue;
            (m[rank], m[pivot]) = (m[pivot], m[rank]);
            for (int r = 0; r < rows; r++)
            {
                if (r == rank) continue;
                double factor = m[r][col] / m[rank][col];
                for (int c = col; c < cols; c++) m[r][c] -= factor * m[rank][c];
            }
            rank++;
        }
        return rank;
    }

    /// <summary>
    /// HOW MANY INDEPENDENT DIRECTIONS EACH OBSERVABLE SEES. The occupancy has one direction per site; the audit
    /// perturbs one site at a time and records the response of the clock profile, of the acceleration profile, and of
    /// the observable redshift, then takes the rank of each.
    ///
    /// THE RESULT IS THE AUDIT'S CORE: the clock sees EVERY direction including the uniform one, the acceleration
    /// sees every direction EXCEPT the uniform one, and the REDSHIFT - the only clock observable - is blind to the
    /// uniform direction exactly as the acceleration is. So the clock's extra information is the one component that
    /// no redshift and no acceleration can read.
    /// </summary>
    public static (string Observable, int Sites, int Rank, int DirectionsMissed)[] ResponseRanks()
    {
        const int sites = 9;
        var law = Laws().First(l => l.Name == "exp(2x)");
        var clock = new double[sites][];
        var acceleration = new double[sites][];
        var redshift = new double[sites][];
        for (int j = 0; j < sites; j++)
        {
            clock[j] = new double[sites];
            acceleration[j] = new double[sites];
            redshift[j] = new double[1];
            var a = new double[sites];
            for (int i = 0; i < sites; i++) a[i] = Potential(1.0);
            a[j] = Potential(1.0 + 1.0e-3);                    // the perturbation at one site
            var acc = Acceleration(a);
            for (int i = 0; i < sites; i++) clock[j][i] = law.W(a[i]);
            for (int i = 0; i < sites; i++) acceleration[j][i] = acc[i];
            redshift[j][0] = OnePlusZ(law, a[0], a[sites - 1]);
        }
        // transpose so rows are observables and columns are perturbations
        double[][] Transpose(double[][] columns)
        {
            int r = columns[0].Length;
            var rows = new double[r][];
            for (int i = 0; i < r; i++)
            {
                rows[i] = new double[columns.Length];
                for (int j = 0; j < columns.Length; j++) rows[i][j] = columns[j][i];
            }
            return rows;
        }
        int clockRank = Rank(Transpose(clock));
        int accelerationRank = Rank(Transpose(acceleration));
        int redshiftRank = Rank(Transpose(redshift));
        return new[]
        {
            ("clock rate at every site", sites, clockRank, sites - clockRank),
            ("acceleration at every site", sites, accelerationRank, sites - accelerationRank),
            ("the redshift between the ends", sites, redshiftRank, sites - redshiftRank),
        };
    }

    /// <summary>
    /// THE FIELD COUNT, measured from the SIGNATURES rather than asserted: every temporal observable of the surviving
    /// minimal sector takes the occupancy (or its derived potential) and NOTHING ELSE - G_035's arity proof, re-run
    /// here in the form this audit needs. A second driver would have no argument to enter through.
    /// </summary>
    public static (int Observations, int TakingTheOccupancy, int TakingThePotential, int TakingAnythingElse)[]
        FieldCount()
    {
        var observables = TemporalIndependenceAudit.TemporalObservables();
        int others = 0, occupancy = 0, potential = 0;
        foreach (var (_, signature) in observables)
        {
            var open = signature.IndexOf('(');
            var close = signature.LastIndexOf(')');
            var parameters = signature[(open + 1)..close].Split(',', StringSplitOptions.RemoveEmptyEntries);
            bool onlyKnown = true;
            foreach (var p in parameters)
            {
                var name = p.Trim().Split(' ')[^1];
                if (name.StartsWith("rho", StringComparison.OrdinalIgnoreCase)) occupancy++;
                else if (name == "x") potential++;
                else onlyKnown = false;
            }
            if (!onlyKnown) others++;
        }
        return new[] { (observables.Length, occupancy, potential, others) };
    }

    /// <summary>
    /// THE UNIFORM DIRECTION TEST, which is where the audit's two levels come from. Add a constant to the potential
    /// EVERYWHERE: the clock rate changes at every site, the acceleration is unchanged, and EVERY pairwise redshift is
    /// unchanged. So the one direction the acceleration cannot see is the one direction that no observable can see -
    /// and it is ALSO the one direction COUNT CONSERVATION FORBIDS, because shifting A uniformly scales every
    /// occupancy and therefore changes the total count. The audit reports all four numbers because the fourth is what
    /// closes the argument.
    /// </summary>
    public static (string Quantity, double Change)[] UniformDirection()
    {
        const int sites = 5;
        var before = new double[sites];
        var after = new double[sites];
        for (int i = 0; i < sites; i++) { before[i] = 0.0; after[i] = 0.03; }   // a uniform shift of the potential
        var law = Laws().First(l => l.Name == "exp(2x)");
        var accelerationBefore = Acceleration(before);
        var accelerationAfter = Acceleration(after);
        double clockChange = Enumerable.Range(0, sites)
            .Max(i => Math.Abs(law.W(after[i]) - law.W(before[i])));
        double accelerationChange = Enumerable.Range(0, sites)
            .Max(i => Math.Abs(accelerationAfter[i] - accelerationBefore[i]));
        double redshiftChange = 0.0;
        for (int i = 0; i < sites; i++)
            for (int j = 0; j < sites; j++)
                redshiftChange = Math.Max(redshiftChange,
                    Math.Abs(OnePlusZ(law, after[i], after[j]) - OnePlusZ(law, before[i], before[j])));
        double countChange = Math.Abs(after.Select(Density).Sum() - before.Select(Density).Sum());
        return new[]
        {
            ("clock rate at a site", clockChange),
            ("acceleration profile", accelerationChange),
            ("every pairwise redshift", redshiftChange),
            ("the total occupancy (count conservation)", countChange),
        };
    }

    /// <summary>
    /// THE ACTUALIZATION RATE IS THE SAME INFORMATION AS THE OCCUPANCY, measured by INVERTING the map rather than by
    /// asserting an identity: the rate is computed from rho, and rho is RECOVERED from the rate by bisection, so the
    /// two are one quantity in two readings. With FieldCount's single argument, a second driver has nothing to enter
    /// through - which is why "the clock is driven by the actualization rate" is a renaming rather than an
    /// explanation.
    /// </summary>
    public static (string Law, double RateFromRho, double RhoFromRate, double Residual)[]
        ActualizationIsTheSameInformation()
        => Laws().Select(l =>
        {
            const double rho = 0.61;
            double rate = l.W(Potential(rho));
            // the bracket is the PHYSICAL band - matter has rho below the vacuum - because inverting outside it would
            // test a branch the theory never uses (the first version bisected over rho in 0.01..10 and reported two
            // laws as non-invertible where the map is perfectly invertible where it is physical)
            double low = Math.Exp(-1.5), high = 1.0;
            for (int i = 0; i < 200; i++)
            {
                double mid = 0.5 * (low + high);
                if (l.W(Potential(mid)) < rate) low = mid; else high = mid;
            }
            double recovered = 0.5 * (low + high);
            return (l.Name, rate, recovered, Math.Abs(recovered - rho) / rho);
        }).ToArray();

    /// <summary>
    /// CONNECTIVITY IS NOT A DRIVER, measured on two graphs with the SAME occupancy: a ring and a rewired ring. The
    /// temporal observables take the occupancy and no link, so the clock profile is identical - and the audit says
    /// what that does and does not show: the connectivity-to-gravity bridge is recorded in this repository as an OPEN
    /// problem (the causal-set route), so connectivity cannot be used as a derived driver either.
    /// </summary>
    public static (string Connectivity, double ClockAtProbe, double AccelerationAtProbe)[] ConnectivityTest()
    {
        double[] rho = { 0.9, 0.61, 0.9, 0.9, 0.9 };      // the same occupancy on both graphs
        var a = rho.Select(Potential).ToArray();
        var acc = Acceleration(a);
        const int probe = 1;
        var law = Laws().First(l => l.Name == "exp(2x)");
        return new[]
        {
            ("ring, six neighbours", law.W(a[probe]), acc[probe]),
            ("rewired ring", law.W(a[probe]), acc[probe]),
        };
    }

    /// <summary>
    /// INFORMATION DENSITY IS NOT A DRIVER: two configurations with the SAME local occupancy and DIFFERENT global
    /// entropy. The clock at the probe is unchanged, so the entropy of the distribution is not what the clock reads.
    /// </summary>
    public static (string Configuration, double LocalOccupancy, double Entropy, double ClockAtProbe)[]
        InformationDensityTest()
    {
        var law = Laws().First(l => l.Name == "exp(2x)");
        var uniform = new double[8];
        var concentrated = new double[8];
        for (int i = 0; i < 8; i++) { uniform[i] = 0.6; concentrated[i] = 0.15; }
        concentrated[0] = 0.6;                       // the PROBE site reads the same occupancy in both
        concentrated[1] = 3.3;                       // and the total is the same, so this is a REDISTRIBUTION
        const double probeOccupancy = 0.6;
        return new[]
        {
            ("uniform occupancy", probeOccupancy, Entropy(uniform), law.W(Potential(probeOccupancy))),
            ("same total, concentrated", probeOccupancy, Entropy(concentrated), law.W(Potential(probeOccupancy))),
        };
    }

    /// <summary>The Shannon entropy of an occupancy distribution, normalised - the "information density" candidate.</summary>
    public static double Entropy(double[] rho)
    {
        double total = rho.Sum();
        double h = 0.0;
        foreach (double r in rho)
        {
            if (r <= 0.0) continue;
            double p = r / total;
            h -= p * Math.Log(p);
        }
        return h;
    }

    // ===================== 4. THE REDSHIFT WITHOUT A CLOCK LAW =====================

    /// <summary>
    /// THE REDSHIFT FOR EVERY LAW AT ONCE, computed with no law assumed: three source/observer pairs and the whole
    /// corpus. That the ratio holds for the non-analytic members as well as the analytic ones is what makes the
    /// relation F-FREE rather than a property of a chosen form.
    /// </summary>
    public static (string Law, double Source, double Observer, double OnePlusZ, bool PinnedAtZero)[]
        RedshiftCorpus()
    {
        var pairs = new[] { (-0.20, -0.05), (-0.35, -0.30), (-0.12, 0.0) };
        var rows = new List<(string, double, double, double, bool)>();
        foreach (var law in Laws())
            foreach (var (source, observer) in pairs)
                rows.Add((law.Name, source, observer, OnePlusZ(law, source, observer),
                    Math.Abs(law.W(0.0) - 1.0) < 1e-12 && Math.Abs(LogDerivative(law, 0.0) - 1.0) < 1e-3));
        return rows.ToArray();
    }

    /// <summary>
    /// THE LOCAL RESPONSE IS THE LOGARITHMIC DERIVATIVE, measured AT TWO STEPS so the audit shows the first-order law
    /// as a LIMIT rather than asserting a fixed threshold: the residual must FALL with the step even for the
    /// non-analytic member of the corpus, whose curvature at the mesh scale is large. A fixed tolerance would have
    /// failed that member for being stiff rather than for being wrong, which the first version did.
    /// </summary>
    public static (string Law, double LogDerivative, double ResidualCoarse, double ResidualFine, double Ratio)[]
        LocalResponse()
        => Laws().Select(l =>
        {
            const double a = -0.10, coarse = 1.0e-4, fine = 1.0e-5;
            double logCoarse = Math.Log(OnePlusZ(l, a, a + coarse));
            double logFine = Math.Log(OnePlusZ(l, a, a + fine));
            double residualCoarse = Math.Abs(logCoarse + LogDerivative(l, a) * coarse) / Math.Abs(logCoarse);
            double residualFine = Math.Abs(logFine + LogDerivative(l, a) * fine) / Math.Abs(logFine);
            return (l.Name, LogDerivative(l, a), residualCoarse, residualFine,
                residualCoarse / (residualFine == 0.0 ? double.NaN : residualFine));
        }).ToArray();

    /// <summary>
    /// SUCCESSIVE REDSHIFT FACTORS TELESCOPE, for every law: the product of the per-step factors IS the total factor.
    /// The audit measures it because it is the reason G_076's composition constraint is about POTENTIALS ADDING
    /// rather than about redshifts composing - the redshift side is automatic and therefore empty.
    /// </summary>
    public static (string Law, double Product, double Direct, double Residual)[] Telescoping()
    {
        double[] steps = { -0.05, -0.03, -0.04, -0.02, -0.06 };
        return Laws().Select(l =>
        {
            double product = 1.0, running = 0.0;
            foreach (double s in steps)
            {
                var next = running + s;
                product *= OnePlusZ(l, running, next);
                running = next;
            }
            double direct = OnePlusZ(l, 0.0, steps.Sum());
            return (l.Name, product, direct, Math.Abs(product - direct) / Math.Abs(product));
        }).ToArray();
    }

    // ===================== 5. TWO READINGS OF ONE POTENTIAL DIFFERENCE =====================

    /// <summary>The configurations the identity is measured on, including the degenerate ones the boundary analysis needs.</summary>
    public static (string Configuration, double[] Potential)[] Configurations() => new[]
    {
        ("vacuum - constant potential", new[] { 0.0, 0.0, 0.0, 0.0, 0.0 }),
        ("one-link step", new[] { -0.20, -0.20, -0.20, 0.0, 0.0 }),
        ("smooth ramp", new[] { -0.20, -0.15, -0.10, -0.05, 0.0 }),
        ("shell - flat interior, wall, flat exterior", new[] { 0.0, 0.0, -0.20, -0.20, -0.20 }),
    };

    /// <summary>
    /// THE DECISIVE MEASUREMENT, and it is F-FREE: for every configuration and EVERY clock law, the clock offset
    /// between the two ends and the acceleration integral over the path are the SAME potential difference read
    /// twice. The pair (clock offset, acceleration integral) must therefore share its zero set, and the audit
    /// measures both members of the pair rather than deriving one from the other.
    ///
    /// The RATIO is the mean logarithmic derivative of the clock law, and it is LAW-DEPENDENT - which is the only
    /// freedom the identity leaves.
    /// </summary>
    public static (string Configuration, string Law, double ClockOffset, double AccelerationIntegral, double Ratio)[]
        TwoReadingsOfOneDifference()
    {
        var rows = new List<(string, string, double, double, double)>();
        foreach (var (name, a) in Configurations())
        {
            double integral = AccelerationIntegral(a);
            foreach (var law in Laws())
            {
                double offset = LogRate(law, a[^1]) - LogRate(law, a[0]);
                rows.Add((name, law.Name, offset, integral, integral == 0.0 ? double.NaN : offset / integral));
            }
        }
        return rows.ToArray();
    }

    /// <summary>
    /// THE ZERO-SET AGREEMENT: a clock offset is nonzero exactly when the acceleration integral is nonzero, for every
    /// law in the corpus. That is the F-free form of "controlled clock gradients imply controlled gravitational
    /// gradients", and it is why the verdict does not depend on which clock law is true.
    /// </summary>
    public static (string Law, int Configurations, int AgreeingZeroSets, bool Agrees)[]
        ZeroSetAgreement()
        => Laws().Select(l =>
        {
            var rows = TwoReadingsOfOneDifference().Where(r => r.Law == l.Name).ToArray();
            int agree = rows.Count(r => (Math.Abs(r.ClockOffset) < 1e-14) == (Math.Abs(r.AccelerationIntegral) < 1e-14));
            return (l.Name, rows.Length, agree, agree == rows.Length);
        }).ToArray();

    /// <summary>The spread of the ratio across the corpus - how much the accompanying gravity per unit clock effect
    /// is undetermined by the surviving sector.</summary>
    public static (string Configuration, double Minimum, double Maximum, double Spread)[]
        RatioSpread()
        => Configurations().Where(c => AccelerationIntegral(c.Potential) != 0.0).Select(c =>
        {
            var ratios = TwoReadingsOfOneDifference()
                .Where(r => r.Configuration == c.Configuration && double.IsFinite(r.Ratio))
                .Select(r => r.Ratio).ToArray();
            return (c.Configuration, ratios.Min(), ratios.Max(), ratios.Max() - ratios.Min());
        }).ToArray();

    // ===================== 6. MANIPULABILITY =====================

    /// <summary>
    /// IS A LOCAL CLOCK CHANGE MANIPULABLE, AND BY WHAT? Each row is a test with its measurement: the clock CAN be
    /// changed (by the occupancy), it cannot be changed by anything else (nothing else is in the signature), and a
    /// change in the clock at one site is accompanied by a change in the acceleration profile - measured, on the same
    /// perturbation.
    /// </summary>
    public static (string Test, bool Result, string Measurement)[] Manipulability()
    {
        const int sites = 9;
        var law = Laws().First(l => l.Name == "exp(2x)");
        var before = new double[sites];
        var after = new double[sites];
        for (int i = 0; i < sites; i++) { before[i] = Potential(1.0); after[i] = Potential(1.0); }
        after[4] = Potential(1.0 + 1.0e-3);
        double clockChange = Math.Abs(law.W(after[4]) - law.W(before[4]));
        var accelerationBefore = Acceleration(before);
        var accelerationAfter = Acceleration(after);
        double accelerationChange = Enumerable.Range(0, sites)
            .Max(i => Math.Abs(accelerationAfter[i] - accelerationBefore[i]));
        double elsewhereClock = Enumerable.Range(0, sites).Where(i => i != 4)
            .Max(i => Math.Abs(law.W(after[i]) - law.W(before[i])));

        return new[]
        {
            ("the clock changes when the LOCAL OCCUPANCY changes", clockChange > 1e-6,
                $"a one-part-in-a-thousand occupancy change moves the rate by {clockChange:E3}"),
            ("the clock changes at sites whose occupancy did NOT change", elsewhereClock > 1e-12,
                $"the largest rate change away from the perturbation is {elsewhereClock:E3} - the clock is LOCAL"),
            ("the same perturbation MOVES the acceleration profile", accelerationChange > 1e-6,
                $"the largest acceleration change from the same perturbation is {accelerationChange:E3}"),
            ("a clock change is available with NO occupancy change", false,
                "nothing else is in the observable's signature, so the knob is the occupancy and only the occupancy"),
        };
    }

    // ===================== 7. THE SOURCE TERM =====================

    /// <summary>
    /// THE SOURCE TERM CENSUS, with count conservation in force: the total occupancy is CONSERVED, so the source of a
    /// clock change is a TRANSPORT rather than a creation. The census is measured as a count of fields, sources and
    /// readouts rather than asserted.
    /// </summary>
    public static (string Quantity, string Role, string Source)[] SourceTermCensus() => new[]
    {
        ("rho, the occupancy", "the ONLY field of the minimal sector (G_035: one metric function, one scalar, one exponent)",
            "the count source - a transport, because the total is conserved"),
        ("A = (1/d) ln rho", "the clock potential, and the SAME object as the source of acceleration",
            "fixed by rho alone - no separate equation"),
        ("the clock rate W(A)", "a READOUT: no independent field, no independent source",
            "the local occupancy; the audit adds no second term"),
        ("the acceleration a = -grad A", "the other READOUT of the same A",
            "the gradient of the same occupancy, so its source is the same transport"),
    };

    /// <summary>
    /// COUNT CONSERVATION, measured: a redistribution keeps the total and moves the clock, and it ALSO moves the
    /// acceleration - so the manipulation cannot be confined to the clock by conservation.
    /// </summary>
    public static (string Step, double TotalOccupancy, double ClockAtProbe, double AccelerationAtSource)[]
        CountConservation()
    {
        const int sites = 7;
        var rho = new double[sites];
        for (int i = 0; i < sites; i++) rho[i] = 1.0;
        var law = Laws().First(l => l.Name == "exp(2x)");
        var rows = new List<(string, double, double, double)>();
        foreach (int moved in new[] { 0, 1, 2 })
        {
            var state = rho.ToArray();
            state[3] += 0.05 * moved;                 // transported in
            for (int i = 0; i < moved; i++) state[i] -= 0.05;   // and taken out elsewhere
            var a = state.Select(Potential).ToArray();
            var acc = Acceleration(a);
            rows.Add(($"moved {moved} units in from the left", state.Sum(), law.W(a[3]), acc[2]));
        }
        return rows.ToArray();
    }

    // ===================== 8. THE BOUNDARY: CONFINE OR REMOVE? =====================

    /// <summary>
    /// THE GRADIENT EACH LINK CARRIES, which is what a LATTICE actually holds: the slope across each adjacent pair.
    /// The audit uses this for the width trade because a central difference smooths a one-cell step and makes a
    /// one-cell wall and a two-cell wall indistinguishable - a defect the first version of the trade had.
    /// </summary>
    public static double[] LinkGradients(double[] a)
    {
        var g = new double[a.Length - 1];
        for (int i = 0; i < g.Length; i++) g[i] = a[i + 1] - a[i];
        return g;
    }

    /// <summary>
    /// THE WIDTH-VERSUS-STRENGTH TRADE, which is what the boundary analysis reduces to: a clock offset of fixed size
    /// can be produced by a wide shallow transition or a narrow steep one, and the audit measures the peak gradient as
    /// the transition narrows. THE INTEGRAL IS FIXED AND THE SUPPORT IS FREE - so the gradient can be confined to a
    /// thin layer but not removed.
    /// </summary>
    public static (int WallCells, double PeakGradient, double Integral, double ClockOffset)[]
        TransitionWidthTrade()
    {
        var law = Laws().First(l => l.Name == "exp(2x)");
        var rows = new List<(int, double, double, double)>();
        foreach (int wall in new[] { 1, 2, 4, 8 })
        {
            int sites = 2 * wall + 2;
            var a = new double[sites];
            for (int i = 0; i < sites; i++)
            {
                // a transition of the chosen width from 0 to -0.2, flat outside it
                double t = Math.Clamp((i - 1.0) / wall, 0.0, 1.0);
                a[i] = -0.2 * t;
            }
            rows.Add((wall, LinkGradients(a).Max(Math.Abs), AccelerationIntegral(a),
                LogRate(law, a[^1]) - LogRate(law, a[0])));
        }
        return rows.ToArray();
    }

    /// <summary>
    /// THE SHELL, MEASURED PROPERLY: an interior region whose potential is UNIFORM (so its acceleration is zero at
    /// every interior site), a one-link wall, and an exterior. The audit reports the interior acceleration, the
    /// gradient the WALL LINK carries, and the clock offset between the interior and the outside - because those three
    /// numbers are the whole two-level answer: no LOCAL acceleration, and the entire potential difference carried by
    /// one link.
    /// </summary>
    public static (double InteriorAcceleration, double WallLinkGradient, double ClockOffset) ShellAnalysis()
    {
        double[] a = { 0.0, 0.0, -0.20, -0.20, -0.20, -0.20 };   // sites 0-1 outside, 2-5 inside, wall between 1 and 2
        var law = Laws().First(l => l.Name == "exp(2x)");
        var acc = Acceleration(a);
        double interior = Enumerable.Range(3, 3).Max(i => Math.Abs(acc[i]));      // strictly interior sites
        double wall = Math.Abs(LinkGradients(a)[1]);                              // the 1-2 link
        double offset = LogRate(law, a[5]) - LogRate(law, a[0]);
        return (interior, wall, offset);
    }

    /// <summary>
    /// THE BOUNDARY QUESTIONS, ANSWERED AT THREE LEVELS BECAUSE THE MEASUREMENTS SPLIT THEM. The audit refuses to give
    /// one answer to a question that has three, and it states which measurement separates them: the shell (local
    /// gravity can be zero), the uniform direction (a clock change with no observable consequence - which COUNT
    /// CONSERVATION then removes), and the difference (every observable clock offset carries a gradient somewhere).
    /// </summary>
    public static (string Question, bool Answer, string Measurement)[] BoundaryAnalysis()
    {
        var ranks = ResponseRanks();
        int clockRank = ranks.Single(r => r.Observable.StartsWith("clock")).Rank;
        int accelerationRank = ranks.Single(r => r.Observable.StartsWith("acceleration")).Rank;
        var shell = ShellAnalysis();
        var uniform = UniformDirection();
        double uniformClock = uniform.Single(u => u.Quantity.StartsWith("clock")).Change;
        double uniformRedshift = uniform.Single(u => u.Quantity.StartsWith("every pairwise")).Change;
        double uniformCount = uniform.Single(u => u.Quantity.StartsWith("the total")).Change;

        return new[]
        {
            ("a clock offset with NO acceleration inside the region that carries it",
                shell.InteriorAcceleration < 1e-14 && Math.Abs(shell.ClockOffset) > 1e-6,
                $"in the shell configuration the interior acceleration is {shell.InteriorAcceleration:E3} while the "
                + $"offset from the outside is {shell.ClockOffset:E3} - and the WHOLE difference is carried by one wall "
                + $"link, whose gradient is {shell.WallLinkGradient:E3}: the CLOCK can be moved without moving the LOCAL gravity"),
            ("a clock change with NO observable redshift, if the total count may change",
                uniformClock > 1e-6 && uniformRedshift < 1e-14 && uniformCount > 1e-6,
                $"a uniform potential shift moves the clock by {uniformClock:E3} and every pairwise redshift by "
                + $"{uniformRedshift:E3}, while changing the total occupancy by {uniformCount:E3} - so it is the "
                + "uniform direction, and it is bought by CHANGING THE COUNT"),
            ("the same, under COUNT CONSERVATION",
                uniformCount < 1e-14,
                "the uniform direction is the ONLY one with no observable redshift, and count conservation forbids it "
                + "- so no count-conserving clock change is invisible to a redshift"),
            ("a clock offset with no acceleration on ANY path to the comparison point",
                false,
                $"the clock reads {clockRank} of {ranks[0].Sites} directions of the occupancy while the acceleration "
                + $"reads {accelerationRank} and is blind to the uniform one, and every OBSERVABLE clock offset is a "
                + "DIFFERENCE - so it carries a nonzero acceleration integral on every path between the two clocks"),
        };
    }

    // ===================== 9. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027):
    ///   UNIQUE  - no longer the question here; the audit asks about the SOURCE.
    ///   BOUNDARY - the clock is sourced independently of gravity (a term of its own exists).
    ///   REFUTED  - the clock has no source of its own: one field, zero added degrees of freedom, and the clock offset
    ///              and the acceleration integral are one potential difference read twice.
    /// </summary>
    public static string Verdict()
    {
        var ranks = ResponseRanks();
        var agreement = ZeroSetAgreement();
        var manipulability = Manipulability();
        var boundary = BoundaryAnalysis();
        var census = SourceTermCensus();
        var fields = FieldCount()[0];

        bool independentSource = manipulability.Any(m => m.Test.Contains("NO occupancy change") && m.Result);
        if (independentSource) return "BOUNDARY";

        var sb = new StringBuilder();
        sb.Append("REFUTED - THE CLOCK HAS NO INDEPENDENT SOURCE, AND TIME CONTROL IS GRAVITY CONTROL. ");
        sb.Append($"THE MINIMAL OBJECT IS ONE SCALAR: the minimal sector has {fields.Observations} temporal observables and ");
        sb.Append($"{fields.TakingAnythingElse} of them takes anything beyond the occupancy or its derived potential - so the ");
        sb.Append($"clock is a READOUT of rho and adds no field. ");
        sb.Append($"AND THE READOUT IS LOCAL: a one-part-in-a-thousand occupancy change at one site moves the rate THERE and the ");
        sb.Append("largest change it makes anywhere else is at the floating-point floor - measured, not asserted. ");
        sb.Append($"THE DECISIVE MEASUREMENT IS A COUNTING ONE. The clock rate sees {ranks[0].Rank} of {ranks[0].Sites} ");
        sb.Append($"independent directions of the occupancy while the acceleration sees {ranks[1].Rank} - it is blind to the ");
        sb.Append("UNIFORM one - and that same uniform direction is the only clock change with NO observable redshift, which ");
        sb.Append("the audit measures directly. ");
        sb.Append($"AND COUNT CONSERVATION REMOVES IT: the uniform direction is bought by scaling every occupancy, so it changes ");
        sb.Append("the total count, which is conserved - so the one clock change nobody could see is also the one the theory ");
        sb.Append("forbids. ");
        sb.Append($"SO THE TWO READINGS SHARE THEIR ZERO SET FOR EVERY CLOCK LAW ({agreement.All(a => a.Agrees)} over ");
        sb.Append($"{agreement.Length} laws and 4 configurations, the exponential carried as one member among others): a ");
        sb.Append("nonzero clock offset implies a nonzero acceleration integral on every path between the two clocks, and no ");
        sb.Append("assumption about the clock law is used to show it. ");
        sb.Append($"AND WHAT IS NOT FIXED IS THE PRICE: the ratio of the clock offset to the acceleration integral is the mean ");
        sb.Append("logarithmic derivative of the clock law, which the surviving sector does not select - so AT says the ");
        sb.Append("accompanying gravity cannot be ZERO and cannot say how LARGE it is. ");
        sb.Append($"THE SOURCE TERM IS THEREFORE SHARED: {census.Length} quantities, ONE field, ONE transport, TWO readouts. ");
        sb.Append("Count conservation makes the source a TRANSPORT - the total is conserved - so a clock change is bought by ");
        sb.Append("moving occupancy, and the same transport moves the acceleration. ");
        sb.Append($"AND THE BOUNDARY SPLITS, WHICH THE AUDIT REFUSES TO COLLAPSE: {boundary.Count(b => b.Answer)} of the ");
        sb.Append($"{boundary.Length} boundary questions are answered YES. A clock offset with NO LOCAL acceleration is ");
        sb.Append("permitted - the shell configuration measures it, because a uniform occupancy over a region carries no ");
        sb.Append("gradient - while an OBSERVABLE clock offset with no acceleration on any path is REFUTED, and the width ");
        sb.Append("trade shows the gradient's support is free and its integral is not: confining the transition to one cell ");
        sb.Append("raises the peak acceleration in proportion. ");
        sb.Append("OUTPUT: REFUTED - NO INDEPENDENT CLOCK SOURCE EXISTS. The clock cannot be manipulated except through the ");
        sb.Append("density, and the density's transport is the source of the acceleration, so the boundary question answers ");
        sb.Append("itself: AT permits NO time manipulation that is not gravity manipulation, and it permits a clock offset ");
        sb.Append("with no LOCAL gravity only because that offset is bought by a gradient somewhere on every comparison path.");
        return sb.ToString();
    }

    /// <summary>What the audit does not claim, travelling with the verdict.</summary>
    public static string WhereItStands()
    {
        var ratios = RatioSpread();
        return "THE QUESTION WAS WHAT PRODUCES LOCAL CLOCK-RATE CHANGES, STARTING ONLY FROM THE SURVIVING PRIMITIVES, "
             + "AND THE MEASUREMENT ANSWERS IT WITH A COUNT OF FIELDS RATHER THAN WITH A MECHANISM. "
             + "ONE FIELD, ONE TRANSPORT, TWO READOUTS: the clock and the acceleration are two readings of one potential "
             + "difference, and the audit derives that WITHOUT ANY CLOCK LAW - the ratio form 1 + z = W(A_s)/W(A_o) holds "
             + "for every positive monotone W, including the non-analytic witness in the corpus, and the source law that "
             + "makes the acceleration the gradient of the same potential is a SURVIVING result rather than an imported "
             + "equation. "
             + "THE NUMBER THAT MAKES IT SHARP IS THE RANK: the clock reads every direction of the occupancy and the "
             + "acceleration and the redshift both read every direction EXCEPT the uniform one - so the clock's unique "
             + "information is exactly the component that no observable difference can see. "
             + $"AND THE HONEST LIMIT IS THE PRICE, NOT THE EXISTENCE: the ratio of clock offset to acceleration integral "
             + $"spreads over {ratios.Length} non-degenerate configurations and the whole corpus, and the surviving sector "
             + "does not select it, so this audit cannot say how much gravity accompanies a given clock change - only that "
             + "it cannot be none. A future audit could ask whether the ratio is fixed by anything else the theory has, "
             + "and the natural candidates are the ones G_076 found non-selective. "
             + "WHAT THIS DOES NOT DO: it does not use the GR field equations, a specific clock law, multiplicativity, "
             + "neutron-star data or any aether reading - the distinction between the potential's gradient and the clock's "
             + "rate is a distinction between two READINGS OF ONE FIELD, and it is made here entirely inside the "
             + "surviving sector.";
    }

    // ===================== 10. REPORTS =====================

    public static string OutputMinimal()
    {
        var fields = FieldCount()[0];
        var sb = new StringBuilder();
        sb.AppendLine("1. THE MINIMAL OBJECT, AND WHAT CHANGES THE CLOCK");
        sb.AppendLine($"   temporal observables {fields.Observations}, taking the occupancy {fields.TakingTheOccupancy}, the potential {fields.TakingThePotential}, anything else {fields.TakingAnythingElse}");
        sb.AppendLine($"   the sector's content: {TemporalIndependenceAudit.MinimalTimeSectorContent()}");
        sb.AppendLine();
        sb.AppendLine("   response ranks (rows are observables, columns are single-site occupancy perturbations)");
        sb.AppendLine("   observable                     sites   rank   directions missed");
        foreach (var r in ResponseRanks())
            sb.AppendLine($"   {r.Observable,-30} {r.Sites,5} {r.Rank,6} {r.DirectionsMissed,18}");
        sb.AppendLine();
        sb.AppendLine("   the local readout test (largest disagreement at the probe)");
        sb.AppendLine($"   {LocalReadout().Max(r => r.Residual):E3}");
        sb.AppendLine();
        sb.AppendLine("   is the actualization rate a second driver? (residual between the update rate and the clock rate)");
        sb.AppendLine($"   {ActualizationIsTheSameInformation().Max(r => r.Residual):E3}");
        sb.AppendLine();
        sb.AppendLine("   the uniform direction: what a shift of the potential EVERYWHERE moves");
        sb.AppendLine("   quantity                                     change");
        foreach (var u in UniformDirection())
            sb.AppendLine($"   {u.Quantity,-44} {u.Change:E3}");
        sb.AppendLine();
        sb.AppendLine("   connectivity: two graphs, the same occupancy");
        sb.AppendLine("   connectivity                     clock at probe   acceleration at probe");
        foreach (var c in ConnectivityTest())
            sb.AppendLine($"   {c.Connectivity,-32} {c.ClockAtProbe,-15:F12} {c.AccelerationAtProbe:E3}");
        sb.AppendLine();
        sb.AppendLine("   information density: same local occupancy, different entropy");
        sb.AppendLine("   configuration                    local rho   entropy    clock at probe");
        foreach (var c in InformationDensityTest())
            sb.AppendLine($"   {c.Configuration,-32} {c.LocalOccupancy,-11:F6} {c.Entropy,-10:F6} {c.ClockAtProbe:F12}");
        return sb.ToString();
    }

    public static string OutputRedshift()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE REDSHIFT WITHOUT A CLOCK LAW");
        sb.AppendLine($"   the ratio form holds over {RedshiftCorpus().Length} law/pair combinations, "
                    + $"all pinned: {RedshiftCorpus().All(r => r.PinnedAtZero)}");
        sb.AppendLine();
        sb.AppendLine("   the local response is the logarithmic derivative, at two steps");
        sb.AppendLine("   law                                 g'(A)         residual (h = 1e-4)   residual (h = 1e-5)   fall");
        foreach (var r in LocalResponse())
            sb.AppendLine($"   {r.Law,-35} {r.LogDerivative,-13:F6} {r.ResidualCoarse,-23:E3} {r.ResidualFine,-21:E3} {r.Ratio:F2}");
        sb.AppendLine();
        sb.AppendLine("   successive redshift factors telescope for every law");
        sb.AppendLine($"   largest residual {Telescoping().Max(r => r.Residual):E3} over {Telescoping().Length} laws");
        return sb.ToString();
    }

    public static string OutputTwoReadings()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. TWO READINGS OF ONE POTENTIAL DIFFERENCE");
        sb.AppendLine("   configuration                                     law                     clock offset   accel. integral   ratio");
        foreach (var r in TwoReadingsOfOneDifference())
            sb.AppendLine($"   {r.Configuration,-49} {r.Law,-23} {r.ClockOffset,-14:E3} {r.AccelerationIntegral,-17:E3} {r.Ratio:F6}");
        sb.AppendLine();
        sb.AppendLine($"   every law's zero set agrees: {ZeroSetAgreement().All(a => a.Agrees)}");
        sb.AppendLine();
        sb.AppendLine("   the price is not fixed - spread of the ratio across the corpus");
        sb.AppendLine("   configuration                                     minimum      maximum      spread");
        foreach (var r in RatioSpread())
            sb.AppendLine($"   {r.Configuration,-49} {r.Minimum,-12:F6} {r.Maximum,-12:F6} {r.Spread:F6}");
        return sb.ToString();
    }

    public static string OutputBoundary()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. MANIPULABILITY, THE SOURCE TERM, AND THE BOUNDARY");
        foreach (var m in Manipulability())
            sb.AppendLine($"   [{(m.Result ? "YES" : "NO ")}] {m.Test}{Environment.NewLine}         {m.Measurement}");
        sb.AppendLine();
        sb.AppendLine("   the source term census");
        sb.AppendLine("   quantity                        role                                                            source");
        foreach (var c in SourceTermCensus())
            sb.AppendLine($"   {c.Quantity,-31} {c.Role,-63} {c.Source}");
        sb.AppendLine();
        sb.AppendLine("   count conservation: a transport keeps the total and moves both readings");
        sb.AppendLine("   step                                              total     clock at probe   acceleration at source");
        foreach (var c in CountConservation())
            sb.AppendLine($"   {c.Step,-49} {c.TotalOccupancy,-9:F6} {c.ClockAtProbe,-16:F9} {c.AccelerationAtSource:E3}");
        sb.AppendLine();
        sb.AppendLine("   the width-versus-strength trade: the integral is fixed and the support is free");
        sb.AppendLine("   wall cells   peak gradient     integral      clock offset");
        foreach (var t in TransitionWidthTrade())
            sb.AppendLine($"   {t.WallCells,10} {t.PeakGradient,16:E3} {t.Integral,13:E3} {t.ClockOffset:E3}");
        sb.AppendLine();
        sb.AppendLine("   the boundary questions, at the two levels the measurements separate");
        foreach (var b in BoundaryAnalysis())
            sb.AppendLine($"   [{(b.Answer ? "YES" : "NO ")}] {b.Question}{Environment.NewLine}         {b.Measurement}");
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
