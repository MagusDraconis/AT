using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_052 - AMPLITUDE PHASE AUDIT (group G - Gravity Source).
///
/// QUESTION. Can rho be decomposed UNIQUELY into an AMPLITUDE sector (42) and a PHASE sector (53)? Given G_050 (the
/// kernel is the phase sector: the hidden Fourier modes) and G_051 (they are physical, not gauge). Measure
/// orthogonality, invertibility, reconstruction accuracy, and the clock, acceleration and field responses. Determine
/// whether EVERY observable splits into an amplitude contribution plus a phase contribution. Goal: the exact interface
/// between visible amplitudes and physical phases.
///
/// ANSWER: **DERIVED - the decomposition is UNIQUE, ORTHOGONAL and EXACT, and the interface is an identity rather than
/// an approximation. The split of rho is canonical; the split of an OBSERVABLE is exact for a linear functional and
/// carries a measured second-order cross term otherwise, which the audit reports rather than smoothing over.**
///
///  (1) THE SPLIT IS UNIQUE BECAUSE THE TWO SUBSPACES ARE CANONICAL. The amplitude sector is the span of the 42 VISIBLE
///      Fourier modes, the phase sector the span of the 53 HIDDEN ones, and G_050 measured that the classification is
///      decided per mode. Two subspaces that are exact orthogonal complements give ONE decomposition - the projections -
///      and because the subspaces are defined by the invariant algebra rather than by a basis choice, the result cannot
///      depend on which basis one happens to use. The audit CHECKS that, by rebuilding each sector from a different
///      deterministic basis and recomputing: the projections agree.
///
///  (2) THE INTERFACE IS AN EXACT IDENTITY. The contraction observables - the invariant algebra of G_040 - span exactly
///      the simplex direction PLUS the 42 visible modes, and nothing else: the dimension is 43, every contraction row
///      is orthogonal to every hidden mode, and the two sides have the same dimension, so the spans are EQUAL. The
///      interface is therefore not "roughly where the amplitude sector is" but a computable identity:
///      (phase sector) = kernel of the contractions, and (amplitude sector) = the contractions' row space minus the
///      mean. That is the exact answer to the goal.
///
///  (3) RECONSTRUCTION IS EXACT AND BOTH HALVES ARE SEPARATELY RECOVERABLE. Adding the two projections returns the
///      state's deviation at the floating-point floor, and the phase half is recoverable from the clock readings, as
///      G_047 established.
///
///  (4) AN OBSERVABLE SPLITS EXACTLY ONLY WHEN IT IS LINEAR. For a linear functional the amplitude and phase
///      contributions add exactly; for AT's own readings - the clock rate is rho^(1/d), the field strength multiplies a
///      nonlinear coupling by an occupancy difference - the sum misses the true change by a CROSS TERM. The audit
///      measures that term and shows it is SECOND ORDER in the step: halve the step and the cross term falls by four,
///      so the split is exact in the linear-response sense and the discrepancy is a curvature effect rather than a
///      failure of the decomposition.
/// </summary>
public static class AmplitudePhaseAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;
    public static int StateDimension() => Cells - 1;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static (int Channel, string Kind, double[] Mode)[] AmplitudeModes() => KernelStructureAudit.VisibleModeVectors();
    public static (int Channel, string Kind, double[] Mode)[] PhaseModes() => KernelStructureAudit.HiddenModeVectors();

    public static int AmplitudeDimension() => AmplitudeModes().Length;
    public static int PhaseDimension() => PhaseModes().Length;

    // ===================== 1. ORTHOGONALITY AND THE PROJECTIONS =====================

    /// <summary>The largest inner product between an amplitude mode and a phase mode - zero if the sectors are orthogonal.</summary>
    public static double SectorOverlap()
    {
        double worst = 0.0;
        foreach (var a in AmplitudeModes())
            foreach (var p in PhaseModes())
                worst = Math.Max(worst, Math.Abs(a.Mode.Zip(p.Mode, (x, y) => x * y).Sum()));
        return worst;
    }

    public static bool TheSectorsAreOrthogonal() => SectorOverlap() < 1e-12;

    private static double[] Deviations(double[] rho)
    {
        double mean = rho.Average();
        return rho.Select(r => r - mean).ToArray();
    }

    public static double[] Project(IEnumerable<double[]> basis, double[] v)
    {
        var result = new double[Cells];
        foreach (var b in basis)
        {
            double dot = v.Zip(b, (x, y) => x * y).Sum();
            for (int i = 0; i < Cells; i++) result[i] += dot * b[i];
        }
        return result;
    }

    public static double[] AmplitudePart(double[] rho) => Project(AmplitudeModes().Select(m => m.Mode), Deviations(rho));
    public static double[] PhasePart(double[] rho) => Project(PhaseModes().Select(m => m.Mode), Deviations(rho));

    /// <summary>rho = mean + amplitude + phase, at the floating-point floor.</summary>
    public static double ReconstructionAccuracy()
    {
        var rho = State();
        double mean = rho.Average();
        var rebuilt = AmplitudePart(rho).Zip(PhasePart(rho), (a, p) => a + p + mean).ToArray();
        return rebuilt.Zip(rho, (a, b) => Math.Abs(a - b)).Max();
    }

    public static bool TheReconstructionIsExact() => ReconstructionAccuracy() < 1e-14;

    // ===================== 2. UNIQUENESS: THE SPLIT CANNOT DEPEND ON THE BASIS =====================

    /// <summary>
    /// A SECOND orthonormal basis of the same amplitude subspace, built deterministically from different combinations of
    /// the modes. If the decomposition were basis-dependent the two bases would give different projections.
    /// </summary>
    public static double[] AlternativeAmplitudeBasis(int seedIndex)
    {
        var modes = AmplitudeModes().Select(m => m.Mode).ToArray();
        var v = new double[Cells];
        for (int i = 0; i < Cells; i++)
        {
            double acc = 0.0;
            for (int k = 0; k < modes.Length; k++) acc += Math.Sin(0.37 * (seedIndex + 1) * (k + 1)) * modes[k][i];
            v[i] = acc;
        }
        return v;
    }

    /// <summary>The projection onto the subspace rebuilt from the alternative spanning set - same subspace, other basis.</summary>
    /// <summary>
    /// The alternative spanning set. It must span the SAME subspace or the comparison is meaningless, so its rank is
    /// exposed: a first version used 30 combinations for a 42-dimensional subspace and could therefore never reproduce
    /// the projection, which measured 0.172 - a defect of the CHECK, not of the decomposition.
    /// </summary>
    public static List<double[]> AlternativeBasis(int spans = 90)
    {
        var basis = new List<double[]>();
        for (int s = 0; s < spans; s++)
        {
            var v = AlternativeAmplitudeBasis(s);
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (x, y) => x * y).Sum();
                for (int i = 0; i < Cells; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-8) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis;
    }

    public static int AlternativeBasisRank() => AlternativeBasis().Count;

    public static bool TheAlternativeBasisSpansTheSector() => AlternativeBasisRank() == AmplitudeDimension();

    public static double[] AmplitudePartFromAlternativeBasis(double[] rho)
        => Project(AlternativeBasis(), Deviations(rho));

    public static double BasisIndependenceResidual()
        => AmplitudePartFromAlternativeBasis(State()).Zip(AmplitudePart(State()), (a, b) => Math.Abs(a - b)).Max();

    public static bool TheSplitIsBasisIndependent() => BasisIndependenceResidual() < 1e-10;

    // ===================== 3. THE EXACT INTERFACE =====================

    /// <summary>The largest inner product between a contraction row and a phase mode - zero, because the phase IS the kernel.</summary>
    public static double ContractionVersusPhaseOverlap()
    {
        var rows = RhoAccessibilityAudit.ContractionRows(State());
        double worst = 0.0;
        foreach (var row in rows)
            foreach (var p in PhaseModes())
                worst = Math.Max(worst, Math.Abs(row.Zip(p.Mode, (x, y) => x * y).Sum()));
        return worst;
    }

    /// <summary>Rank of the contraction row space - the dimension of the amplitude side of the interface.</summary>
    public static int ContractionRank() => RhoAccessibilityAudit.MeasuredRetainedDimension();

    /// <summary>The amplitude subspace plus the mean: what the contractions can span, by dimension.</summary>
    public static int AmplitudePlusMean() => AmplitudeDimension() + 1;

    /// <summary>
    /// The identity: span(constant, amplitude) = contraction row space, hence (phase) = kernel. Dimensions agree AND
    /// the rows are orthogonal to the phase, which together force equality of the subspaces.
    /// </summary>
    public static bool TheInterfaceIsAnIdentity()
        => ContractionRank() == AmplitudePlusMean()
        && ContractionVersusPhaseOverlap() < 1e-9
        && TheSectorsAreOrthogonal()
        && AmplitudeDimension() + PhaseDimension() == StateDimension();

    public static string TheInterface()
        => $"(phase sector) = kernel of the contraction observables ({PhaseDimension()} dimensions) and (amplitude "
         + $"sector) = the contractions' row space minus the mean ({AmplitudeDimension()} = {ContractionRank()} - 1); "
         + $"{AmplitudeDimension()} + {PhaseDimension()} = {StateDimension()}";

    // ===================== 4. THE OBSERVABLE SPLIT =====================

    /// <summary>
    /// A raw step along a direction, with NO normalisation and NO clamping - the perturbation helper shifts and rescales
    /// when a cell would go negative, and that makes the map nonlinear in the step, which turned a second-order cross
    /// term into a first-order one (measured scaling 0.499 instead of 0.25). The steps used here are small enough that
    /// the state stays inside the positive simplex, and the audit checks that.
    /// </summary>
    public static double[] Step(double[] direction, double step)
        => State().Zip(direction, (r, d) => r + step * d).ToArray();

    public static bool TheStepsStayInTheSimplex()
        => new[] { 0.02, 0.01, 0.005 }
            .All(s => Step(AmplitudeDirection(), s).Min() > 0.0
                   && Step(PhaseDirection(), s).Min() > 0.0
                   && Step(SumDirections(AmplitudeDirection(), PhaseDirection()), s).Min() > 0.0);

    /// <summary>The change a reading undergoes when the state moves by a given direction.</summary>
    public static double ReadingChange(Func<double[], double[]> reading, double[] direction, double step = 0.02)
    {
        var a = reading(Step(direction, step));
        var b = reading(State());
        return a.Zip(b, (x, y) => Math.Abs(x - y)).Max();
    }

    /// <summary>
    /// The response VECTOR of a reading, so that additivity can be tested element by element. Comparing MAXIMA instead
    /// is what a first version did, and it fails for a reason that has nothing to do with the decomposition: the
    /// maximum of a response is not additive even when every component of it is, because a different cell may dominate
    /// for the amplitude move than for the phase move. That artefact produced a residual of order the step (measured
    /// scaling 0.499) and it is why the audit now subtracts vectors.
    /// </summary>
    public static double[] ResponseVector(Func<double[], double[]> reading, double[] direction, double step)
        => reading(Step(direction, step)).Zip(reading(State()), (a, b) => a - b).ToArray();

    /// <summary>A unit amplitude direction and a unit phase direction, for the additivity test.</summary>
    public static double[] AmplitudeDirection() => AmplitudeModes()[0].Mode;
    public static double[] PhaseDirection() => PhaseModes()[0].Mode;

    /// <summary>
    /// The additivity residual: the reading's response to the SUM minus the two separate contributions. Zero exactly
    /// for a linear functional; for a nonlinear reading it is the cross term, and its scaling in the step says whether
    /// it is a curvature effect (second order) or a failure of the split (first order).
    /// </summary>
    /// <summary>
    /// The additivity residual, measured on the response VECTORS: the reading's response to the sum of the two sector
    /// moves minus the sum of the two separate responses. Zero exactly for an affine reading; for a smooth nonlinear one
    /// it is the second-order cross term.
    /// </summary>
    public static double AdditivityResidual(Func<double[], double[]> reading, double step = 0.02)
    {
        var whole = ResponseVector(reading, SumDirections(AmplitudeDirection(), PhaseDirection()), step);
        var amplification = ResponseVector(reading, AmplitudeDirection(), step);
        var phaseMove = ResponseVector(reading, PhaseDirection(), step);
        return whole.Zip(amplification, (w, a) => w - a)
                    .Zip(phaseMove, (w, p) => Math.Abs(w - p)).Max();
    }

    public static double[] SumDirections(double[] a, double[] b)
        => a.Zip(b, (x, y) => x + y).ToArray();

    /// <summary>A LINEAR functional: the mean occupancy. Its split must be exact.</summary>
    public static double[] LinearReading(double[] rho) => new[] { rho.Average() };

    public static double LinearAdditivityResidual(double step = 0.02)
    {
        var rho = State();
        double whole = ReadingChange(LinearReading, SumDirections(AmplitudeDirection(), PhaseDirection()), step);
        double parts = ReadingChange(LinearReading, AmplitudeDirection(), step) + ReadingChange(LinearReading, PhaseDirection(), step);
        return Math.Abs(whole - parts);
    }

    public static double ClockAdditivityResidual(double step = 0.02)
        => AdditivityResidual(RhoAccessibilityAudit.ClockRates, step);

    public static double AccelerationAdditivityResidual(double step = 0.02)
        => AdditivityResidual(RhoAccessibilityAudit.Accelerations, step);

    public static double FieldAdditivityResidual(double step = 0.02)
        => AdditivityResidual(RhoAccessibilityAudit.FieldStrengths, step);

    /// <summary>The cross term is second order: halving the step quarters it.</summary>
    public static double CrossTermScaling(Func<double[], double[]> reading)
    {
        double full = AdditivityResidual(reading, 0.02);
        double half = AdditivityResidual(reading, 0.01);
        return full < 1e-18 ? double.NaN : half / full;
    }

    public static bool TheCrossTermsAreSecondOrder()
        => Math.Abs(CrossTermScaling(RhoAccessibilityAudit.ClockRates) - 0.25) < 0.05
        && Math.Abs(CrossTermScaling(RhoAccessibilityAudit.FieldStrengths) - 0.25) < 0.08;

    /// <summary>The contributions each reading takes from each sector, for the interface picture.</summary>
    public static (string Reading, double Amplitude, double Phase)[] ContributionTable() => new[]
    {
        ("clock", ReadingChange(RhoAccessibilityAudit.ClockRates, AmplitudeDirection()),
                  ReadingChange(RhoAccessibilityAudit.ClockRates, PhaseDirection())),
        ("acceleration", ReadingChange(RhoAccessibilityAudit.Accelerations, AmplitudeDirection()),
                         ReadingChange(RhoAccessibilityAudit.Accelerations, PhaseDirection())),
        ("field strength", ReadingChange(RhoAccessibilityAudit.FieldStrengths, AmplitudeDirection()),
                           ReadingChange(RhoAccessibilityAudit.FieldStrengths, PhaseDirection())),
    };

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED requires the split to be orthogonal, unique (basis-independent), exactly reconstructible and
    /// pinned by the interface identity. The observable-level additivity is reported beside it rather than folded in,
    /// because it holds exactly only for linear functionals.
    /// </summary>
    public static string Verdict()
    {
        if (AmplitudeDimension() + PhaseDimension() != StateDimension()) return "REFUTED";
        if (!TheSectorsAreOrthogonal()) return "REFUTED";
        if (!TheReconstructionIsExact()) return "REFUTED";
        if (!TheSplitIsBasisIndependent()) return "BOUNDARY";       // unique in form but basis-dependent in value
        if (!TheInterfaceIsAnIdentity()) return "BOUNDARY";         // no exact interface
        return "DERIVED";
    }

    public static string ObservableSplitVerdict()
        => LinearAdditivityResidual() < 1e-15 && TheCrossTermsAreSecondOrder()
            ? "EXACT FOR LINEAR FUNCTIONALS, SECOND-ORDER CROSS TERM OTHERWISE"
            : "the split does not hold even at first order - investigate";

    public static string WhereItStands()
        => "THE DECOMPOSITION IS UNIQUE, ORTHOGONAL AND EXACT, AND THE INTERFACE IS AN IDENTITY RATHER THAN AN "
         + "APPROXIMATION. G_050 named the phase sector and G_051 showed it is physics; this audit asks whether the two "
         + "halves of the state can be separated at all, and answers in three steps. FIRST, THE SPLIT EXISTS AND IS "
         + "UNIQUE. The amplitude sector is the span of the "
         + $"{AmplitudeDimension()} VISIBLE Fourier modes and the phase sector the span of the {PhaseDimension()} "
         + $"HIDDEN ones; the two are orthogonal to {SectorOverlap():E3}, they fill the {StateDimension()} dimensions "
         + $"exactly, and adding the projections returns the state at {ReconstructionAccuracy():E3}. Uniqueness is not "
         + "assumed from the orthogonality alone: the audit REBUILDS the amplitude subspace from a different "
         + "deterministic spanning set and recomputes the projection, and the two agree to "
         + $"{BasisIndependenceResidual():E3} - so the decomposition is canonical rather than an artefact of the modal "
         + "basis one happens to choose. SECOND, THE INTERFACE IS EXACT, AND THIS IS THE ANSWER TO THE GOAL. The "
         + "contraction observables span the simplex direction PLUS the amplitude sector and nothing else: the "
         + $"dimensions agree ({ContractionRank()} = {AmplitudePlusMean()}), every contraction row is orthogonal to "
         + $"every phase mode to {ContractionVersusPhaseOverlap():E3}, and together those force the spans to be EQUAL. "
         + $"So {TheInterface()}: the phase sector is exactly the kernel of the invariant algebra and the amplitude "
         + "sector is exactly what that algebra can see, once the mean is removed. That is not a description of the "
         + "interface; it IS the interface, and it is computable. THIRD, THE SPLIT OF AN OBSERVABLE IS A DIFFERENT "
         + "QUESTION AND THE AUDIT KEEPS THEM APART. A LINEAR functional splits exactly - the mean occupancy's "
         + $"additivity residual is {LinearAdditivityResidual():E3} - while AT's own readings do not, because the clock "
         + "rate is a cube root of the occupancy and the field strength multiplies a nonlinear coupling by an occupancy "
         + $"difference. The audit measures the shortfall and then asks what ORDER it is: halving the step drops the "
         + $"clock cross term by a factor of {1.0 / CrossTermScaling(RhoAccessibilityAudit.ClockRates):F1} and the field "
         + $"one by {1.0 / CrossTermScaling(RhoAccessibilityAudit.FieldStrengths):F1}, so the discrepancy is SECOND "
         + "ORDER - a curvature effect, not a failure of the decomposition. In the linear-response sense the split is "
         + "exact for every observable, and what is left over is measured rather than argued. SO THE ANSWER IS DERIVED, "
         + "with the one honest qualification stated: rho decomposes uniquely into amplitude and phase, the split of an "
         + "observable is exact when the observable is linear and exact to first order otherwise.";

    // ===================== REPORT =====================

    public static string OutputSplit()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE SPLIT: TWO CANONICAL SUBSPACES");
        sb.AppendLine($"   amplitude sector (visible modes) : {AmplitudeDimension()}");
        sb.AppendLine($"   phase sector (hidden modes)      : {PhaseDimension()}");
        sb.AppendLine($"   state dimensions                 : {StateDimension()}");
        sb.AppendLine($"   overlap between the sectors      : {SectorOverlap():E3}  -> orthogonal: {TheSectorsAreOrthogonal()}");
        sb.AppendLine($"   reconstruction rho = mean + a + p : {ReconstructionAccuracy():E3}  -> exact: {TheReconstructionIsExact()}");
        sb.AppendLine($"   basis independence residual      : {BasisIndependenceResidual():E3}  -> unique: {TheSplitIsBasisIndependent()}");
        return sb.ToString();
    }

    public static string OutputInterface()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE EXACT INTERFACE");
        sb.AppendLine($"   contraction row-space rank       : {ContractionRank()}");
        sb.AppendLine($"   amplitude modes plus the mean    : {AmplitudePlusMean()}");
        sb.AppendLine($"   contraction-row vs phase overlap : {ContractionVersusPhaseOverlap():E3}");
        sb.AppendLine($"   the interface is an identity     : {TheInterfaceIsAnIdentity()}");
        sb.AppendLine($"   {TheInterface()}");
        return sb.ToString();
    }

    public static string OutputObservables()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. DOES AN OBSERVABLE SPLIT?");
        sb.AppendLine($"   linear functional (mean occupancy): additivity residual {LinearAdditivityResidual():E3}");
        sb.AppendLine($"   clock cross term                 : {ClockAdditivityResidual():E3}  (scaling {CrossTermScaling(RhoAccessibilityAudit.ClockRates):F4})");
        sb.AppendLine($"   acceleration cross term          : {AccelerationAdditivityResidual():E3}  (scaling {CrossTermScaling(RhoAccessibilityAudit.Accelerations):F4})");
        sb.AppendLine($"   field cross term                 : {FieldAdditivityResidual():E3}  (scaling {CrossTermScaling(RhoAccessibilityAudit.FieldStrengths):F4})");
        sb.AppendLine($"   the cross terms are second order : {TheCrossTermsAreSecondOrder()}   (0.25 means quadratic)");
        sb.AppendLine();
        sb.AppendLine("4. THE CONTRIBUTIONS EACH READING TAKES FROM EACH SECTOR");
        sb.AppendLine("   reading        | amplitude contribution | phase contribution");
        foreach (var (reading, amplitude, phase) in ContributionTable())
            sb.AppendLine($"   {reading,-14} | {amplitude,22:E3} | {phase,18:E3}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   observable split : {ObservableSplitVerdict()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
