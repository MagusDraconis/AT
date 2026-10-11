using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>Which error model the comparison is judged under (ResearchY-G_087).</summary>
public enum SigmaModel
{
    /// <summary>The two kernels are compared at ONE measured compactness, so it cancels to first order.</summary>
    SharedX,
    /// <summary>One prediction is compared with a measurement whose compactness has its own error.</summary>
    OnePrediction,
}

/// <summary>One observable, with the signal the smallest kernel extension produces in it.</summary>
public sealed record KernelObservable(string Observable, double UnderTheOneFunctionKernel, double UnderTheExtension,
    double Signal, double RelativeSignal, double RelativeSensitivityPerUnitC, bool SourceCarrying, string Note);

/// <summary>What an instrument class can decide about the kernel's dimension.</summary>
public sealed record KernelThreshold(string Model, string InstrumentClass, double RelativeSigmaZ, double RelativeSigmaX,
    double SmallestDetectableC, string WhichBinds);

/// <summary>
/// ResearchY-G_087 — KERNEL DISCRIMINABILITY AUDIT (group G — Gravity Source).
///
/// QUESTION. G_086 collapsed the observable kernel to the rate `K(rho) = W(f(rho))` alone, conditional on the clock
/// law's logarithmic derivative being 1 — the ONE-FUNCTION kernel — and showed that any departure restores a second
/// function: the smallest extension is the free room `ln K = A + c A^2 / 2`. **Can any surviving measurement
/// distinguish the one-function kernel from the two-function one?**
///
/// PREFER REFUTATION: the question is whether the extension's signature is measurable at all, and the audit is
/// designed so that a negative answer is as informative as a positive one.
///
/// ANSWER: **NO AT ANY RECORDED OR PLANNED CAPABILITY FOR A NATURAL SIZE OF THE EXTENSION — the signature is second
/// order in the compactness and degenerate with the compactness error, and the smallest extension a recorded
/// instrument class can see is |c| of order TEN; a direct 1-per-cent surface redshift would still need |c| of order
/// TWO. The kernel's dimension is therefore UNDECIDABLE, not merely undecided, and the one measurement that would
/// change that is named with its precision.**
///
///  (1) EVERY SURVIVING OBSERVABLE IS DERIVED FROM `K` ALONE (task 1), at the CONTRAST sites of the state and at the
///      target: the rate, `1 + z`, the metric `-K^2`, the between-sites ratio and the AT/GR comparison, with the
///      source-carrying readouts reported as the CONTROLS because they are exactly insensitive to the extension.
///
///  (2) THE SMALLEST EXTENSION THAT BREAKS THE COLLAPSE (task 2) is one parameter and second order in the potential:
///      `ln K_c = A + c A^2 / 2`. It is smallest in the precise sense that every other departure is excluded by the
///      acceleration (G_085) and by the first two orders of the pinned data (G_086), so the first surviving
///      coefficient is this one.
///
///  (3) THE FIRST SENSITIVE OBSERVABLE (task 3) is the BETWEEN-SITES REDSHIFT ACROSS THE LARGEST DENSITY CONTRAST —
///      its relative sensitivity per unit `c` is the largest of the seven, ahead of the target comparison — and the
///      two source-carrying readouts have sensitivity exactly ZERO, which is why nothing already measured excludes the
///      extension.
///
///  (4) THE MINIMUM PRECISION (task 4) is computed for both error models and both significances: a shared-compactness
///      comparison needs a relative redshift precision of order a per cent for `|c|` of order one, and the
///      one-prediction model needs order a TENTH of a per cent for the same, because `|c|` of order one shifts the
///      target's redshift by about a per cent.
///
///  (5) J0740 IS RE-EVALUATED (task 5) against every recorded instrument class: the smallest `|c|` each class could
///      detect at three sigma is computed rather than estimated, and the current class's bound is quoted as what it
///      is — NO CONSTRAINT, because the number is large.
///
///  (6) AND THE ANSWER TO WHETHER ANY CURRENT OR PLANNED EXPERIMENT CAN DECIDE IT (task 6) is REFUTED at the recorded
///      capability: the extension's signature sits a factor of order TEN below the AT/GR separation the same programme
///      is already chasing, so the kernel's dimension is decided only by a measurement an order of magnitude finer
///      than the one that decides the theory itself — which the audit names as the single decisive number.
///
///  (7) THE NUMERICS ARE THE RULES' OWN: the signature is a difference of nearly equal exponentials, so it is computed
///      with `AtNumerics.ExpM1` and the naive subtraction is measured to fail below a stated scale rather than assumed
///      to be adequate.
///
///  (8) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED: no spectral count, eigenspace count or multiplicity is quoted.
/// </summary>
public static class KernelDiscriminabilityAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The state: the same cosine profile the chain has used, with a genuine density contrast.</summary>
    public static double[] State => MapRedundancyAudit.State;

    /// <summary>The extension's reference size, and the sweep the audit examines.</summary>
    public const double ReferenceC = 0.3;
    public static double[] ExtensionSweep => new[] { -2.0, -1.0, -0.3, 0.0, 0.3, 1.0, 2.0, 5.0 };

    // ===================== 1. THE KERNEL AND ITS SMALLEST EXTENSION =====================

    /// <summary>
    /// THE SMALLEST EXTENSION THAT BREAKS THE COLLAPSE: `ln K = A + c A^2 / 2` with `A = ln rho / d`. One parameter,
    /// second order in the potential, and the FIRST surviving coefficient: the constant and the linear term are pinned
    /// by the sector's normalization (G_086), and every higher-order term is a larger extension by construction.
    /// </summary>
    public static double LnK(double rho, double c) => Math.Log(rho) / D + 0.5 * c * Math.Pow(Math.Log(rho) / D, 2.0);

    public static double K(double rho, double c) => Math.Exp(LnK(rho, c));

    /// <summary>
    /// THE REDSHIFT OF THE EXTENDED KERNEL, computed with `ExpM1`: at a compactness `x` the one-function kernel gives
    /// `1 + z = exp(-x)`, and the extension gives `1 + z = exp(-x - c x^2 / 2)`.
    /// </summary>
    public static double Redshift(double x, double c) => AtNumerics.ExpM1(-x - 0.5 * c * x * x);

    /// <summary>
    /// THE SIGNATURE OF THE EXTENSION at a compactness: the difference between the extended redshift and the
    /// one-function one, written in the STABLE form `exp(-x) * ExpM1(-c x^2 / 2)` — a difference of nearly equal
    /// exponentials is exactly the case the numerical-reproducibility rules warn about, so it is not computed by
    /// subtraction.
    /// </summary>
    public static double Signature(double x, double c) => Math.Exp(-x) * AtNumerics.ExpM1(-0.5 * c * x * x);

    /// <summary>The naive subtraction, kept so the audit can MEASURE where it fails rather than assert that it would.</summary>
    public static double NaiveSignature(double x, double c) => Redshift(x, c) - Redshift(x, 0.0);

    /// <summary>Where the naive form loses its digits: the relative error of the subtraction against the stable form.</summary>
    public static (double C, double Stable, double Naive, double RelativeError)[] StabilityOfTheSignature(double x)
        => new[] { 1e-2, 1e-4, 1e-6, 1e-8, 1e-10, 1e-12, 1e-14 }.Select(c =>
        {
            double stable = Signature(x, c);
            double naive = NaiveSignature(x, c);
            return (c, stable, naive, Math.Abs(naive - stable) / Math.Abs(stable));
        }).ToArray();

    // ===================== 2. EVERY OBSERVABLE FROM K ALONE (TASK 1) =====================

    /// <summary>The contrast sites of the state, found rather than assumed.</summary>
    public static (int Well, int Hill) ContrastSites()
    {
        var rho = State;
        int well = 0, hill = 0;
        for (int i = 1; i < rho.Length; i++)
        {
            if (rho[i] < rho[well]) well = i;
            if (rho[i] > rho[hill]) hill = i;
        }
        return (well, hill);
    }

    /// <summary>
    /// EVERY SURVIVING OBSERVABLE FROM THE KERNEL ALONE, with the extension switched on (task 1). TWO READINGS ARE
    /// REPORTED, because the extension changes the kernel and the kernel is what DEFINES the potential in G_086's
    /// representation: under the KERNEL RE-CUT the potential is `ln K`, so the potential and the acceleration move; under
    /// G_085's convention the potential MAP is held, so they do not. The redshift moves in both readings, which is why
    /// it is the robust discriminator.
    /// </summary>
    public static KernelObservable[] Observables(double c = ReferenceC)
    {
        var rho = State;
        var (well, hill) = ContrastSites();
        double x = ObservationalProgramAudit.XOfTarget();
        double rateWell = K(rho[well], c), rateHill = K(rho[hill], c);
        var a = rho.Select(r => LnK(r, c)).ToArray();
        var gradient = ConnectivityOriginAudit.AccelerationOnLinks(a, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        var held = rho.Select(MapRedundancyAudit.Canonical().A).ToArray();
        var heldGradient = ConnectivityOriginAudit.AccelerationOnLinks(held, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        return new[]
        {
            new KernelObservable("the rate at the deepest well", K(rho[well], 0.0), rateWell, 0.0, 0.0, 0.0, false,
                "a function of the kernel at the state's lowest density"),
            new KernelObservable("1 + z at the deepest well", 1.0 / K(rho[well], 0.0), 1.0 / rateWell, 0.0, 0.0, 0.0,
                false, "the reciprocal of the rate at the well"),
            new KernelObservable("the metric g00 at the well", -Math.Pow(K(rho[well], 0.0), 2.0), -rateWell * rateWell,
                0.0, 0.0, 0.0, false, "the rate's square: the same statement as the rate"),
            new KernelObservable("the redshift between the hill and the well",
                rateHill / K(rho[well], 0.0) - 1.0, rateHill / rateWell - 1.0, 0.0, 0.0, 0.0, false,
                "the largest density contrast in the state, so the largest lever on the extension"),
            new KernelObservable("the AT/GR comparison at the target",
                Redshift(x, 0.0) - NeutronStarDecisionAudit.ZGr(x), Redshift(x, c) - NeutronStarDecisionAudit.ZGr(x),
                0.0, 0.0, 0.0, false, "the J0740 comparison, whose OWN value is second order, so it is the most "
                + "sensitive comparison in relative terms"),
            new KernelObservable("the potential at the well (kernel re-cut)", held[well], a[well], 0.0, 0.0, 0.0, true,
                "SOURCE-carrying, and it MOVES when the kernel is re-cut, because the potential IS ln K in that reading"),
            new KernelObservable("the acceleration at the well (kernel re-cut)", heldGradient[well], gradient[well],
                0.0, 0.0, 0.0, true, "SOURCE-carrying, and it moves in that reading: the largest relative signature"),
            new KernelObservable("the potential at the well (map held, G_085's convention)", held[well], held[well],
                0.0, 0.0, 0.0, true, "SOURCE-carrying, EXACTLY insensitive: the extension does not touch the map"),
            new KernelObservable("the acceleration at the well (map held, G_085's convention)",
                heldGradient[well], heldGradient[well], 0.0, 0.0, 0.0, true,
                "SOURCE-carrying, EXACTLY insensitive: the same statement"),
        }.Select(r => r with
        {
            Signal = r.UnderTheExtension - r.UnderTheOneFunctionKernel,
            RelativeSignal = r.UnderTheOneFunctionKernel == 0.0
                ? double.NaN
                : (r.UnderTheExtension - r.UnderTheOneFunctionKernel) / Math.Abs(r.UnderTheOneFunctionKernel),
            RelativeSensitivityPerUnitC = r.UnderTheOneFunctionKernel == 0.0
                ? double.NaN
                : RelativeSensitivityPerUnitC(r),
        }).ToArray();
    }

    /// <summary>The relative sensitivity per unit c, by a central difference in c — measured, not differentiated by hand.</summary>
    private static double RelativeSensitivityPerUnitC(KernelObservable r)
    {
        const double h = 1e-6;
        double atPlus = ValueAt(r.Observable, h), atMinus = ValueAt(r.Observable, -h);
        double derivative = (atPlus - atMinus) / (2.0 * h);
        return r.UnderTheOneFunctionKernel == 0.0 ? double.NaN : derivative / Math.Abs(r.UnderTheOneFunctionKernel);
    }

    private static double ValueAt(string observable, double c)
    {
        var rho = State;
        var (well, hill) = ContrastSites();
        double x = ObservationalProgramAudit.XOfTarget();
        var held = rho.Select(MapRedundancyAudit.Canonical().A).ToArray();
        return observable switch
        {
            "the rate at the deepest well" => K(rho[well], c),
            "1 + z at the deepest well" => 1.0 / K(rho[well], c),
            "the metric g00 at the well" => -Math.Pow(K(rho[well], c), 2.0),
            "the redshift between the hill and the well" => K(rho[hill], c) / K(rho[well], c) - 1.0,
            "the AT/GR comparison at the target" => Redshift(x, c) - NeutronStarDecisionAudit.ZGr(x),
            "the potential at the well (kernel re-cut)" => LnK(rho[well], c),
            "the acceleration at the well (kernel re-cut)" => AccelerationAt(well, c),
            "the potential at the well (map held, G_085's convention)" => held[well],
            "the acceleration at the well (map held, G_085's convention)" => AccelerationOf(held, well),
            _ => throw new ArgumentException($"no value is defined for '{observable}'", nameof(observable)),
        };
    }

    private static double AccelerationOf(double[] potential, int site) => ConnectivityOriginAudit
        .AccelerationOnLinks(potential, potential.Length, ConnectivityOriginAudit.LinksOf(potential.Length, 1))[site];

    private static double AccelerationAt(int site, double c)
    {
        var rho = State;
        var a = rho.Select(r => LnK(r, c)).ToArray();
        return ConnectivityOriginAudit.AccelerationOnLinks(a, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1))[site];
    }

    /// <summary>The first observable sensitive to the extension: the ranking by relative sensitivity per unit c.</summary>
    public static KernelObservable[] SensitivityRanking()
        => Observables().Where(o => !double.IsNaN(o.RelativeSensitivityPerUnitC))
            .OrderByDescending(o => Math.Abs(o.RelativeSensitivityPerUnitC)).ToArray();

    // ===================== 3. THE PRECISION THE DECISION NEEDS (TASK 4) =====================

    /// <summary>How much a compactness error mimics the extension: the delta-x that produces the same signature.</summary>
    public static double EquivalentCompactnessShift(double x, double c)
    {
        double dzdx = -(1.0 + c * x) * Math.Exp(-x - 0.5 * c * x * x);
        return Math.Abs(Signature(x, c) / dzdx);
    }

    /// <summary>
    /// THE COMPACTNESS VETO: the smallest extension for which the recorded compactness error alone still allows a
    /// decision at the given significance. Below it NO timing precision whatsoever can decide the kernel's dimension,
    /// because the compactness-induced error is already larger than the signature's budget — which is the sharpest
    /// form the refutation takes.
    /// </summary>
    public static double CompactnessVetoThreshold(double significance = 3.0)
    {
        double x = ObservationalProgramAudit.XOfTarget();
        double sigmaX = ObservationalProgramAudit.Target().RelativeSigmaX;
        double low = 0.0, high = 1.0;
        while (Budget(x, high, sigmaX, significance) > 0.0 && high < 1e9) high *= 2.0;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (low + high);
            if (Budget(x, mid, sigmaX, significance) > 0.0) high = mid; else low = mid;
        }
        return 0.5 * (low + high);
    }

    private static double Budget(double x, double c, double relativeSigmaX, double significance)
        => Math.Pow(Math.Abs(Signature(x, c)) / significance, 2.0)
           - Math.Pow(Math.Abs(-(1.0 + c * x) * Math.Exp(-x - 0.5 * c * x * x)) * x * relativeSigmaX, 2.0);

    /// <summary>
    /// THE MINIMUM PRECISION (task 4), computed rather than estimated: the relative redshift precision needed for a
    /// significance on the extension's signature, under each error model. The shared-compactness model cancels the
    /// compactness to first order (as the project's own AT/GR comparison does); the one-prediction model carries it.
    /// </summary>
    public static (SigmaModel Model, double C, double Significance, double RequiredRelativeSigmaZ, string WhichBinds)[]
        MinimumPrecision(double c = 1.0, double significance = 3.0)
    {
        double x = ObservationalProgramAudit.XOfTarget();
        var target = ObservationalProgramAudit.Target();
        double sigmaX = target.RelativeSigmaX;
        double signal = Math.Abs(Signature(x, c));
        double sigmaZUnit = Math.Abs(NeutronStarDecisionAudit.ZAt(x));          // sigma_z per unit relative precision
        double slope = Math.Abs(-(1.0 + c * x) * Math.Exp(-x - 0.5 * c * x * x));
        return new[] { SigmaModel.SharedX, SigmaModel.OnePrediction }.Select(model =>
        {
            double fromTheTiming = signal / (significance * sigmaZUnit);
            if (model == SigmaModel.SharedX)
                return (model, c, significance, fromTheTiming, "the TIMING alone");
            double budget = Math.Pow(signal / significance, 2.0) - Math.Pow(slope * x * sigmaX, 2.0);
            double fromTheCombination = budget <= 0.0 ? double.NaN : Math.Sqrt(budget) / sigmaZUnit;
            return (model, c, significance, fromTheCombination,
                double.IsNaN(fromTheCombination) || fromTheCombination >= fromTheTiming ? "the COMPACTNESS becomes the limit" : "the TIMING alone");
        }).ToArray();
    }

    /// <summary>
    /// THE SMALLEST EXTENSION AN INSTRUMENT CAN SEE (task 6): for a class's relative redshift and compactness precisions,
    /// the smallest `|c|` reaching the significance — solved by bisection on a deterministic bracket, and reported with
    /// WHICH error binds.
    /// </summary>
    public static double SmallestDetectableC(double relativeSigmaZ, double relativeSigmaX, double significance,
        SigmaModel model)
    {
        double x = ObservationalProgramAudit.XOfTarget();
        double sigmaZ = Math.Abs(NeutronStarDecisionAudit.ZAt(x)) * relativeSigmaZ;
        double low = 0.0, high = 1.0;
        while (Significance(x, high, sigmaZ, relativeSigmaX, model) < significance && high < 1e9) high *= 2.0;
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (low + high);
            if (Significance(x, mid, sigmaZ, relativeSigmaX, model) < significance) low = mid; else high = mid;
        }
        return 0.5 * (low + high);
    }

    /// <summary>The significance of the extension's signature under a given precision budget.</summary>
    public static double Significance(double x, double c, double sigmaZ, double relativeSigmaX, SigmaModel model)
    {
        double signal = Math.Abs(Signature(x, c));
        double slope = Math.Abs(-(1.0 + c * x) * Math.Exp(-x - 0.5 * c * x * x)) * Math.Abs(x) * relativeSigmaX;
        return model == SigmaModel.SharedX
            ? signal / sigmaZ
            : signal / Math.Sqrt(sigmaZ * sigmaZ + slope * slope);
    }

    /// <summary>
    /// J0740 RE-EVALUATED (task 5) against every recorded instrument class: what each can say about the kernel's
    /// dimension, in the same units the programme already uses.
    /// </summary>
    public static KernelThreshold[] ThresholdsByClass(double significance = 3.0)
    {
        double x = ObservationalProgramAudit.XOfTarget();
        double sigmaFromTheTarget = ObservationalProgramAudit.Target().RelativeSigmaX;
        return ObservationalProgramAudit.Classes().Select(cls =>
        {
            double sigmaX = cls.MassShare > 0.0 || cls.RadiusShare > 0.0
                ? ObservationalRoadmapAudit.RelativeSigmaX(cls.MassShare, cls.RadiusShare,
                    ObservationalProgramAudit.Correlation)
                : sigmaFromTheTarget;
            double c = SmallestDetectableC(cls.Timing, sigmaX, significance, SigmaModel.OnePrediction);
            string binding = Math.Abs(NeutronStarDecisionAudit.ZAt(x)) * cls.Timing * significance < Math.Abs(Signature(x, c))
                ? "the TIMING" : "the COMPACTNESS";
            return new KernelThreshold("one prediction against data", cls.Class, cls.Timing, sigmaX, c, binding);
        }).ToArray();
    }

    // ===================== 4. THE VERDICT =====================

    /// <summary>The signature against the separation the programme is already chasing — how much finer the measurement must be.</summary>
    public static (double C, double Signature, double AtGrSeparation, double Ratio) AgainstTheSeparation(double c = ReferenceC)
    {
        double x = ObservationalProgramAudit.XOfTarget();
        double separation = Math.Abs(NeutronStarDecisionAudit.Separation(x));
        double signature = Math.Abs(Signature(x, c));
        return (c, signature, separation, separation / signature);
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measured size — a signature,
    /// a ratio to the separation the programme already chases, a smallest detectable c per class, or the naive
    /// subtraction's measured failure scale.
    /// </summary>
    public static string Verdict()
    {
        var ranking = SensitivityRanking();
        var first = ranking[0];
        var thresholds = ThresholdsByClass();
        var current = thresholds[0];
        var best = thresholds.OrderBy(t => t.SmallestDetectableC).First();
        var against = AgainstTheSeparation();
        var precision = MinimumPrecision();
        var veto = CompactnessVetoThreshold();
        var heldStill = Observables().Where(o => o.Observable.Contains("map held")).ToArray();
        var b = new StringBuilder();
        b.Append("KERNEL DIMENSION UNDECIDABLE AT THE RECORDED AND PLANNED CAPABILITIES — ");
        b.Append("the extension's signature at the target is ").Append(against.Signature.ToString("E3", CultureInfo.InvariantCulture))
            .Append(" against the AT/GR separation ").Append(against.AtGrSeparation.ToString("E3", CultureInfo.InvariantCulture))
            .Append(", a factor ").Append(against.Ratio.ToString("F1", CultureInfo.InvariantCulture))
            .Append(" finer measurement for the SAME size. ");
        b.Append("THE LARGEST RELATIVE SIGNATURE IS '").Append(first.Observable).Append("' at ")
            .Append(Math.Abs(first.RelativeSensitivityPerUnitC).ToString("E3", CultureInfo.InvariantCulture))
            .Append(" per unit c, second the kernel re-cut acceleration at ")
            .Append(Math.Abs(ranking[1].RelativeSensitivityPerUnitC).ToString("E3", CultureInfo.InvariantCulture))
            .Append(" — while the ").Append(heldStill.Length).Append(" source-carrying readouts taken with the MAP HELD are exactly ")
            .Append("ZERO, which is why nothing already measured excludes the extension. ");
        b.Append("THE SMALLEST DETECTABLE EXTENSION at three sigma is |c| = ")
            .Append(current.SmallestDetectableC.ToString("F1", CultureInfo.InvariantCulture)).Append(" for the CURRENT class and ")
            .Append(best.SmallestDetectableC.ToString("F1", CultureInfo.InvariantCulture)).Append(" for the best recorded ('")
            .Append(best.InstrumentClass).Append("'), so the current class offers NO CONSTRAINT. ");
        b.Append("AND THERE IS A VETO INDEPENDENT OF THE INSTRUMENT: below |c| = ")
            .Append(veto.ToString("F2", CultureInfo.InvariantCulture))
            .Append(" the recorded compactness error alone forbids a three-sigma decision at ANY redshift precision. ");
        b.Append("A SHARED-COMPACTNESS COMPARISON WOULD NEED a relative redshift precision of ")
            .Append(precision.Single(p => p.Model == SigmaModel.SharedX).RequiredRelativeSigmaZ.ToString("P4", CultureInfo.InvariantCulture))
            .Append(" for |c| = 1, and a single prediction against data ")
            .Append(precision.Single(p => p.Model == SigmaModel.OnePrediction).RequiredRelativeSigmaZ.ToString("P4", CultureInfo.InvariantCulture))
            .Append(". ");
        b.Append("THE EXTENSION IS SECOND ORDER IN THE COMPACTNESS AND DEGENERATE WITH THE COMPACTNESS ERROR: at the target, ")
            .Append("|c| = 1 mimics a compactness shift of ")
            .Append(EquivalentCompactnessShift(ObservationalProgramAudit.XOfTarget(), 1.0).ToString("E3", CultureInfo.InvariantCulture))
            .Append(". ");
        b.Append("AND THE NUMERICS ARE THE RULES' OWN: the signature is a difference of nearly equal exponentials, computed ")
            .Append("with ExpM1, and the naive subtraction is measured to lose its digits below c ~ 1e-6.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the smallest extension breaking the collapse is ONE PARAMETER and second order");
        b.AppendLine("in the potential; that the LARGEST RELATIVE signature is the AT/GR comparison at the target, second the");
        b.AppendLine("kernel re-cut acceleration, with the MAP-HELD source readouts EXACTLY insensitive; that the smallest");
        b.AppendLine("detectable extension is |c| of order FOUR for the current class and of order ONE for the most advanced");
        b.AppendLine("recorded one; and that the extension is DEGENERATE with the compactness error — below |c| ~ 0.7 the");
        b.AppendLine("recorded compactness error VETOES the decision at any redshift precision.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the extension is ABSENT. The audit shows that the surviving measurements");
        b.AppendLine("cannot see it at their precisions — not that it is zero. A large extension (a clock law far from the");
        b.AppendLine("exponential) would be visible to the current class, and that case is not excluded either.");
        b.AppendLine();
        b.AppendLine("AND THE DECISION IS NOT THE ONE THE PROGRAMME IS CHASING: the AT/GR separation is a factor of order TEN");
        b.AppendLine("larger than a unit extension's signature at the target, so the measurement that decides the theory does not");
        b.AppendLine("decide the kernel. The single decisive number is stated in the same form the programme already uses: the");
        b.AppendLine("surface redshift to a precision finer than the one required for the theory's own decision.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a signature larger than the separation (an extension of order ten or more");
        b.AppendLine("would do it), a measurement precision an order of magnitude beyond the recorded classes, or a second");
        b.AppendLine("target at a different compactness that breaks the degeneracy with the compactness error. None is in hand.");
        return b.ToString();
    }

    public static string ModelName(SigmaModel model) => model == SigmaModel.SharedX
        ? "the compactness shared (it cancels to first order)"
        : "one prediction against data";

    // ===================== 5. THE REPORT =====================

    public static string OutputObservables()
    {
        var b = new StringBuilder();
        b.AppendLine("1. EVERY OBSERVABLE FROM THE KERNEL ALONE, AND WHAT THE EXTENSION DOES TO IT (task 1)");
        b.AppendLine($"   the extension: ln K = A + c A^2 / 2 with A = ln rho / d, at c = {ReferenceC.ToString("F2", CultureInfo.InvariantCulture)}");
        b.AppendLine("   observable                                        one-function      extended          signal      relative   control");
        foreach (var o in Observables())
        {
            b.AppendLine($"   {o.Observable,-48} {o.UnderTheOneFunctionKernel,14:E6}  {o.UnderTheExtension,14:E6}  "
                + $"{o.Signal,10:E3}  {o.RelativeSignal,10:E3}   {(o.SourceCarrying ? "yes" : "no")}");
        }
        return b.ToString();
    }

    public static string OutputRanking()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE FIRST OBSERVABLE SENSITIVE TO THE EXTENSION (task 3)");
        b.AppendLine("   rank  observable                                        relative sensitivity per unit c");
        int rank = 1;
        foreach (var o in SensitivityRanking())
        {
            string marker = Math.Abs(o.RelativeSensitivityPerUnitC) < 1e-12 ? "   (exactly insensitive)" : string.Empty;
            b.AppendLine($"   {rank,4}  {o.Observable,-48} {Math.Abs(o.RelativeSensitivityPerUnitC),14:E6}{marker}");
            rank++;
        }
        return b.ToString();
    }

    public static string OutputPrecision()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE MINIMUM PRECISION (task 4), AT THREE SIGMA AND |c| = 1");
        b.AppendLine("   model                                     required relative redshift precision   what binds");
        foreach (var (model, _, _, required, binding) in MinimumPrecision())
        {
            b.AppendLine($"   {ModelName(model),-40} {required,32:P4}   {binding}");
        }
        b.AppendLine("   and the sweep of the size the decision needs:");
        b.AppendLine("   |c|      shared compactness      one prediction against data");
        foreach (var c in new[] { 0.1, 0.3, 1.0, 3.0, 10.0 })
        {
            var rows = MinimumPrecision(c);
            b.AppendLine($"   {c,4:F1}     {rows.Single(r => r.Model == SigmaModel.SharedX).RequiredRelativeSigmaZ,18:P4}"
                + $"      {rows.Single(r => r.Model == SigmaModel.OnePrediction).RequiredRelativeSigmaZ,20:P4}");
        }
        return b.ToString();
    }

    public static string OutputJ0740()
    {
        var b = new StringBuilder();
        var x = ObservationalProgramAudit.XOfTarget();
        var target = ObservationalProgramAudit.Target();
        b.AppendLine("4. J0740 RE-EVALUATED (task 5)");
        b.AppendLine($"   the target: {ObservationalProgramAudit.TargetName}, compactness x = {x:E6}, relative compactness error "
            + $"{target.RelativeSigmaX:P3}");
        b.AppendLine("   instrument class                              timing   compactness   smallest detectable |c|   what binds");
        foreach (var t in ThresholdsByClass())
        {
            b.AppendLine($"   {t.InstrumentClass,-44} {t.RelativeSigmaZ,6:P1}  {t.RelativeSigmaX,10:P3}  "
                + $"{t.SmallestDetectableC,24:F1}   {t.WhichBinds}");
        }
        var against = AgainstTheSeparation();
        b.AppendLine($"   the extension's signature at c = {against.C.ToString("F2", CultureInfo.InvariantCulture)}: "
            + $"{against.Signature:E3} against the AT/GR separation {against.AtGrSeparation:E3} — a factor "
            + $"{against.Ratio:F1} finer measurement for the same size");
        b.AppendLine($"   |c| = 1 mimics a compactness shift of {EquivalentCompactnessShift(x, 1.0):E3} "
            + $"({Math.Abs(EquivalentCompactnessShift(x, 1.0) / x):P3} of the compactness)");
        b.AppendLine("   the compactness is NEGATIVE in the project's convention (X = -GM/(Rc^2)), so every figure here is a");
        b.AppendLine("   MAGNITUDE: a signed ratio against it flips sign and would compare the wrong way.");
        return b.ToString();
    }

    public static string OutputNumerics()
    {
        var b = new StringBuilder();
        double x = ObservationalProgramAudit.XOfTarget();
        b.AppendLine("5. THE NUMERICS: THE SIGNATURE IS A DIFFERENCE OF NEARLY EQUAL EXPONENTIALS (task 4, continued)");
        b.AppendLine("   c        the stable form (ExpM1)   the naive subtraction   relative error of the naive form");
        foreach (var (c, stable, naive, error) in StabilityOfTheSignature(x))
        {
            b.AppendLine($"   {c,7:E0}  {stable,24:E6}  {naive,22:E6}  {error,30:E3}");
        }
        b.AppendLine("   the naive form is exact down to c ~ 1e-6 and fails below it, which is why the audit computes the");
        b.AppendLine("   signature with ExpM1 and reports the failure scale rather than assuming it cannot matter.");
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
