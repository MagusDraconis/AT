using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_048 - CLOCK COMPLETENESS AUDIT (group G - Gravity Source).
///
/// QUESTION. Is the clock pattern the MAXIMAL observable of rho? Compare the clock pattern, the acceleration pattern,
/// the field-strength pattern and the contraction observables. Measure: kernel rank, information retained, observable
/// dimension. Goal: decide whether clock > acceleration > contraction, or whether the readings are equivalent.
///
/// ANSWER: **MAXIMAL - the addressed clock pattern resolves the whole state - but it is MAXIMAL AND TIED WITH TWO
/// OTHERS: the acceleration and field-strength patterns resolve the whole state as well, and the contraction
/// observables are the only reading measured here that loses information.**
///
///  (1) THE CLOCK PATTERN IS MAXIMAL. Read at every cell it resolves all 95 dimensions of the simplex tangent space -
///      its observable dimension is 95, its information retained 1.000 - and the map is genuinely invertible: the
///      organisation is recovered from the rates as rho = rate^d, with a residual at the floating-point floor. So the
///      clock pattern determines rho, and nothing can retain more.
///
///  (2) THE ACCELERATION PATTERN IS MAXIMAL TOO, WHICH THE QUESTION'S ORDERING DOES NOT ALLOW FOR. The difference
///      pattern looks like it must lose the global constant, and on an arbitrary vector it does - but the state lives
///      on the SIMPLEX, which already excludes the constant, so on the tangent space the difference map is injective:
///      its observable dimension is also 95. So the ordering "clock > acceleration" is NOT what the measurement shows,
///      and the audit says so.
///
///  (3) THE FIELD-STRENGTH PATTERN IS MAXIMAL TOO, AND THIS AUDIT'S OWN EXPECTATION OF PARTIALITY WAS WRONG. The draft
///      predicted a shortfall on the grounds that the pattern is nonlinear in the organisation - the coupling is
///      evaluated at each cell - and the measurement refused it: its tangent map is diagonal-plus-difference with a
///      positive coupling, which is generically invertible, so its dimension comes out at 95 like the other two. The
///      withdrawn expectation is recorded rather than deleted, because the prediction was plausible and its failure is
///      exactly the kind of thing this programme writes down.
///
///  (4) THE CONTRACTION OBSERVABLES ARE REDUNDANT. They retain 43 dimensions at the audited state (G_046/G_047), which
///      is less than half the state: they are a proper sub-algebra of what the clock pattern already resolves, not a
///      competitor. G_040's ceiling of 48 is the ceiling of that non-addressed sub-algebra, and this audit's comparison
///      is what makes its place in the hierarchy explicit.
///
///  (5) THE ORDERING IS THEREFORE NEITHER CHAIN NOR TOTAL EQUIVALENCE. All THREE addressed readings are maximal and
///      mutually equivalent in information content - clock = acceleration = field strength (95, 1.000) - while the
///      contraction observables are strictly weaker (43, 0.453) and redundant, being a sub-algebra of what the clock
///      already resolves. So "clock > acceleration > contraction" is refuted on BOTH its strict inequalities, and
///      "all are equivalent" is refuted by the contractions. What remains is a two-level structure: the addressed
///      readings are complete, and the non-addressed one is not.
/// </summary>
public static class ClockCompletenessAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;
    public static int StateDimension() => Cells - 1;

    // ===================== 1. THE TANGENT SPACE AND THE READINGS =====================

    private static readonly Lazy<double[][]> TangentCache = new(() => BuildTangentBasis());
    private static readonly Lazy<double[][]> KernelCache = new(() => KernelObservableAudit.KernelBasis());

    /// <summary>An orthonormal basis of the simplex tangent space - the 95 directions a state can move in.</summary>
    public static double[][] TangentBasis() => TangentCache.Value;

    private static double[][] BuildTangentBasis()
    {
        var ones = Enumerable.Repeat(1.0 / Math.Sqrt(Cells), Cells).ToArray();
        var basis = new List<double[]>();
        for (int seed = 0; seed < 600 && basis.Count < StateDimension(); seed++)
        {
            var v = new double[Cells];
            for (int i = 0; i < Cells; i++) v[i] = Math.Sin(1.1 * seed + 0.9 * i) + 0.4 * Math.Cos(0.13 * seed * i);
            for (int pass = 0; pass < 2; pass++)
            {
                double d0 = v.Zip(ones, (a, c) => a * c).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= d0 * ones[i];
                foreach (var b in basis)
                {
                    double dot = v.Zip(b, (a, c) => a * c).Sum();
                    for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
                }
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-8) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.ToArray();
    }

    public static double[][] KernelBasis() => KernelCache.Value;

    /// <summary>A response of a reading to a step along a direction, relative to the audited state.</summary>
    public static double[] Response(Func<double[], double[]> reading, double[] direction, double step = 0.02)
    {
        var rho = RhoAccessibilityAudit.BaseState();
        var moved = RhoAccessibilityAudit.Perturbed(rho, direction, step);
        return reading(moved).Zip(reading(rho), (a, b) => a - b).ToArray();
    }

    private static int Rank(IEnumerable<double[]> vectors)
    {
        var basis = new List<double[]>();
        foreach (var v0 in vectors)
        {
            var v = (double[])v0.Clone();
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (a, c) => a * c).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-8) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    // ===================== 2. THE FOUR READINGS, MEASURED =====================

    public static double[] ClockPattern(double[] rho) => RhoAccessibilityAudit.ClockRates(rho);
    public static double[] AccelerationPattern(double[] rho) => RhoAccessibilityAudit.Accelerations(rho);
    public static double[] FieldPattern(double[] rho) => RhoAccessibilityAudit.FieldStrengths(rho);

    public static int ClockDimension() => Rank(TangentBasis().Select(d => Response(ClockPattern, d)));
    public static int AccelerationDimension() => Rank(TangentBasis().Select(d => Response(AccelerationPattern, d)));
    public static int FieldDimension() => Rank(TangentBasis().Select(d => Response(FieldPattern, d)));
    public static int ContractionDimension() => RhoAccessibilityAudit.MeasuredRetainedDimension();

    public static int ClockKernelRank() => Rank(KernelBasis().Select(d => Response(ClockPattern, d)));
    public static int AccelerationKernelRank() => Rank(KernelBasis().Select(d => Response(AccelerationPattern, d)));
    public static int FieldKernelRank() => Rank(KernelBasis().Select(d => Response(FieldPattern, d)));
    public static int ContractionKernelRank() => 0;      // by construction: the kernel is what they cannot see

    /// <summary>Information retained: the observable dimension over the state dimension.</summary>
    public static double InformationRetained(int dimension) => (double)dimension / StateDimension();

    // ===================== 3. THE CLOCK PATTERN IS INVERTIBLE =====================

    /// <summary>
    /// The organisation recovered from the rates: the clock law is rho^(1/d), so rho = rate^d. A reading that
    /// determines the state must pass this test, because a rank of 95 without an inverse would be a coincidence of the
    /// sampling rather than a completeness property.
    /// </summary>
    public static double InvertibilityResidual()
    {
        var rho = RhoAccessibilityAudit.BaseState();
        var rates = ClockPattern(rho);
        var recovered = rates.Select(r => Math.Pow(r, D)).ToArray();
        return recovered.Zip(rho, (a, b) => Math.Abs(a - b)).Max();
    }

    public static bool TheClockPatternDeterminesTheState() => InvertibilityResidual() < 1e-12;

    /// <summary>
    /// The control that keeps the MAXIMAL claim honest: aggregating the same law destroys almost all of it. The
    /// summed rate is ONE number, so its dimension is 1 - the completeness is a property of ADDRESSING the cells,
    /// not of the law alone.
    /// </summary>
    public static int AggregateDimension() => Rank(TangentBasis().Select(d => new[] { Response(ClockPattern, d).Sum() }));

    public static bool AddressabilityIsRequired()
        => AggregateDimension() == 1 && ClockDimension() == StateDimension();

    // ===================== 4. THE ORDERING =====================

    public static (string Reading, int ObservableDimension, double InformationRetained, int KernelRank)[] Table() => new[]
    {
        ("clock pattern", ClockDimension(), InformationRetained(ClockDimension()), ClockKernelRank()),
        ("acceleration pattern", AccelerationDimension(), InformationRetained(AccelerationDimension()), AccelerationKernelRank()),
        ("field-strength pattern", FieldDimension(), InformationRetained(FieldDimension()), FieldKernelRank()),
        ("contraction observables", ContractionDimension(), InformationRetained(ContractionDimension()), ContractionKernelRank()),
    };

    public static string[] MaximalReadings()
        => Table().Where(t => t.ObservableDimension == StateDimension()).Select(t => t.Reading).ToArray();

    public static string[] PartialReadings()
        => Table().Where(t => t.ObservableDimension < StateDimension() && t.ObservableDimension > ContractionDimension())
                  .Select(t => t.Reading).ToArray();

    public static string[] RedundantReadings()
        => Table().Where(t => t.ObservableDimension <= ContractionDimension()).Select(t => t.Reading).ToArray();

    /// <summary>True when two readings share the top rank - the question's own ordering does not allow for it.</summary>
    public static bool TheTopIsTied() => MaximalReadings().Length > 1;

    public static string Ordering()
        => $"clock {ClockDimension()} = acceleration {AccelerationDimension()} = field {FieldDimension()} "
         + $"(all maximal) > contractions {ContractionDimension()} (redundant), out of a state dimension of "
         + $"{StateDimension()}";

    public static int MinimalBasisSize() => ClockDimension();

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. MAXIMAL means the clock resolves the whole state; PARTIAL means it does not; REDUNDANT means it adds
    /// nothing beyond the contractions. The ordering caveat is reported separately, because the top rank is TIED.
    /// </summary>
    public static string Verdict()
    {
        if (!AddressabilityIsRequired()) return "PARTIAL";                        // the reading principle is broken
        if (ClockDimension() <= ContractionDimension()) return "REDUNDANT";       // it adds nothing
        if (ClockDimension() < StateDimension()) return "PARTIAL";                // it resolves only part
        if (!TheClockPatternDeterminesTheState()) return "PARTIAL";               // rank without an inverse
        return "MAXIMAL";
    }

    public static string WhereItStands()
        => "THE CLOCK PATTERN IS MAXIMAL, BUT IT IS NOT ALONE AT THE TOP - AND THE MEASUREMENT REFUSES THE ORDERING THE "
         + "QUESTION PROPOSED. Read at every cell, the clock pattern resolves all "
         + $"{ClockDimension()} dimensions of the state's tangent space out of {StateDimension()}, with information "
         + $"retained {InformationRetained(ClockDimension()):F3}, and the map is genuinely invertible rather than merely "
         + $"of full rank: the organisation comes back from the rates as rho = rate^d with a residual of "
         + $"{InvertibilityResidual():E3}. Nothing can retain more than everything. THE SECOND READING TIES WITH IT, "
         + "and this is the result a chain-shaped answer would have missed. The acceleration pattern is built from "
         + "neighbouring differences, which on an ARBITRARY vector must lose the global constant - but the state lives "
         + $"on the simplex, which already excludes that constant, so on the tangent space the difference map is "
         + $"injective and its observable dimension is {AccelerationDimension()} as well. The proposed ordering "
         + "\"clock > acceleration\" is therefore NOT what the physics shows: the two addressed differential readings "
         + "are equivalent AND maximal. THE THIRD TIES TOO, AND THIS AUDIT'S OWN PREDICTION WAS WRONG. A first draft "
         + "expected the field-strength pattern to be PARTIAL, on the grounds that the coupling is evaluated at each "
         + "cell and the pattern is therefore nonlinear in the organisation. The measurement refused it: its tangent "
         + $"map is diagonal-plus-difference with a positive coupling, hence generically invertible, and its dimension "
         + $"is {FieldDimension()} with information retained {InformationRetained(FieldDimension()):F3} - the same as "
         + "the other two. The withdrawn expectation is recorded rather than deleted. THE FOURTH IS REDUNDANT, AND IT "
         + "IS THE ONLY READING THAT LOSES ANYTHING: the "
         + $"contraction observables retain {ContractionDimension()} dimensions against the clock's "
         + $"{ClockDimension()}, so they are a proper sub-algebra of what the clock pattern already resolves rather "
         + "than a competitor - which is exactly what G_040's ceiling of 48 always was, the ceiling of the "
         + "NON-ADDRESSED readings. THE ORDERING IS THEREFORE NEITHER A CHAIN NOR TOTAL EQUIVALENCE: "
         + $"{Ordering()}. The question asked whether clock > acceleration > contraction, or whether all are "
         + "equivalent; the measured answer is neither, and it is more informative than either - THREE addressed "
         + "readings are equivalent and complete, and the fourth is strictly weaker. WHAT MAKES THE MAXIMAL CLAIM HONEST IS THE "
         + "CONTROL: aggregating the very same law destroys almost all of it, because the summed rate is one number "
         + $"with observable dimension {AggregateDimension()} out of {StateDimension()}. The completeness is a property "
         + "of ADDRESSING the cells, not of the clock law on its own - which is the same distinction that separates "
         + "G_040's ceiling from this audit's floor, and the reason both results are true at once.";

    // ===================== REPORT =====================

    public static string OutputTable()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE FOUR READINGS: OBSERVABLE DIMENSION, INFORMATION RETAINED, KERNEL RANK");
        sb.AppendLine($"   state dimension (simplex tangent) : {StateDimension()}");
        sb.AppendLine("   reading                  | observable dim | information retained | kernel rank");
        foreach (var (reading, dimension, retained, kernel) in Table())
            sb.AppendLine($"   {reading,-24} | {dimension,14} | {retained,20:F3} | {kernel,11}");
        sb.AppendLine($"   the top rank is tied             : {TheTopIsTied()}");
        sb.AppendLine($"   ordering                         : {Ordering()}");
        return sb.ToString();
    }

    public static string OutputCompleteness()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE CLOCK PATTERN IS COMPLETE, AND THE COMPLETENESS IS IN THE ADDRESSING");
        sb.AppendLine($"   clock observable dimension        : {ClockDimension()} of {StateDimension()}");
        sb.AppendLine($"   invertibility residual (rho = rate^d) : {InvertibilityResidual():E3}");
        sb.AppendLine($"   the clock pattern determines the state : {TheClockPatternDeterminesTheState()}");
        sb.AppendLine($"   aggregated rate, observable dimension  : {AggregateDimension()}  <- the control");
        sb.AppendLine($"   addressability is required        : {AddressabilityIsRequired()}");
        sb.AppendLine($"   minimal basis size                : {MinimalBasisSize()} readings");
        return sb.ToString();
    }

    public static string OutputOrdering()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE ORDERING, MEASURED");
        sb.AppendLine($"   maximal    : {string.Join(", ", MaximalReadings())}");
        sb.AppendLine($"   partial    : {string.Join(", ", PartialReadings())}");
        sb.AppendLine($"   redundant  : {string.Join(", ", RedundantReadings())}");
        sb.AppendLine("   the proposed chain clock > acceleration > contraction is NOT what the measurement shows:");
        sb.AppendLine($"   the top is tied ({TheTopIsTied()}), so the addressed readings are equivalent AND complete.");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
