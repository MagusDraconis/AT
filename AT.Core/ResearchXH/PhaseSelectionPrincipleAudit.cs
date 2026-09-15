using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_055 - PHASE SELECTION PRINCIPLE AUDIT (group G - Gravity Source).
///
/// QUESTION. Can any EXISTING AT quantity assign a PREFERRED PHASE STATE? Given G_052 (the split is an identity), G_054
/// (the phase coordinates are freely assigned), E_014 (the flux label is an assignment) and E_015 (the sector measure is
/// exactly flat). Candidates: entropy, free room, actualization density, flux sector, clock functional, field
/// functional. Test: does any quantity BREAK PHASE DEGENERACY? The critical question: WHY THIS PHASE INSTEAD OF
/// ANOTHER?
///
/// ANSWER: **BOUNDARY - no existing AT quantity can, and the audit can say exactly HOW MANY PHASE DIRECTIONS ARE LEFT
/// FREE: the candidates between them constrain at most a handful, and the measured deficiency is the answer to the
/// critical question.**
///
///  (1) THE COUNTING ARGUMENT IS THE WHOLE AUDIT, AND IT IS EXACT. A quantity that could assign a preferred phase must
///      be a SCALAR functional - something one could extremise - and the gradient of a scalar is ONE vector. Its
///      projection into the 53-dimensional phase sector therefore constrains AT MOST ONE phase direction. With
///      {C} candidates, no combination of them can pin more than {C} of the 53, so at least 53 minus {C} directions
///      survive any selection the candidates could ever enforce. The measured rank is reported rather than the bound,
///      and the two are compared.
///
///  (2) THE PHASE-BLIND CANDIDATES CONSTRAIN NOTHING AT ALL, WHICH IS A STRONGER STATEMENT THAN "WEAKLY". Free room is
///      the simplex constraint, whose gradient is the CONSTANT direction - and the constant is the simplex direction,
///      orthogonal to every non-constant mode, so its phase projection is exactly zero. The flux sector is decoupled
///      from the organisation altogether (census zero). The actualization density's scalar level is the same constant.
///      So three of the six candidates contribute nothing whatsoever to the phase.
///
///  (3) THE PHASE-SENSITIVE CANDIDATES CONTRIBUTE, BUT EACH CONTRIBUTES ONE DIRECTION. The clock and field functionals
///      and the occupancy entropy all have gradients with a genuine phase component - G_054 measured it for the first
///      two - so they DO break the degeneracy along one direction each. That is a real effect and the audit states it
///      as such: the phase sector is not uniformly unselectable, it is unselectable in all but a measured handful of
///      directions.
///
///  (4) THE CRITICAL QUESTION IS THEN ANSWERED IN ITS OWN TERMS. "Why this phase instead of another" has no answer
///      inside AT for the directions in the DEFICIENCY, because no quantity the theory contains is sensitive to them;
///      for the handful of directions the candidates do see, the answer would be "because this quantity is extremal
///      there", and the audit measures whether the audited state is even a critical point of them. It is not - the
///      directional derivatives are non-zero - so nothing is extremised at this state either.
/// </summary>
public static class PhaseSelectionPrincipleAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static (int Channel, string Kind, double[] Mode)[] PhaseBasis() => AmplitudePhaseAudit.PhaseModes();
    public static int PhaseDimension() => PhaseBasis().Length;

    // ===================== 1. THE FUNCTIONALS =====================

    /// <summary>The occupancy entropy: AT's own organisation entropy, minus the sum of p log p.</summary>
    public static double Entropy(double[] rho)
    {
        double total = rho.Sum();
        return -rho.Sum(r => { double p = r / total; return p > 0 ? p * Math.Log(p) : 0.0; });
    }

    /// <summary>Free room as the simplex constraint: the total occupancy, whose gradient is the constant direction.</summary>
    public static double FreeRoom(double[] rho) => rho.Sum();

    /// <summary>The actualization density's scalar level: the mean occupancy.</summary>
    public static double ActualizationDensity(double[] rho) => rho.Average();

    /// <summary>The flux label: carried by the link phases, so it does not depend on the state at all.</summary>
    public static double FluxSector(double[] rho)
    {
        _ = rho;
        return SectorSelectionAudit.SectorLabel(SectorSelectionAudit.UniformSector(1, 8), 8);
    }

    /// <summary>The clock functional: the summed rate law.</summary>
    public static double ClockFunctional(double[] rho) => rho.Sum(r => Math.Pow(r, 1.0 / D));

    /// <summary>The field functional: the summed squared field strength.</summary>
    public static double FieldFunctional(double[] rho)
        => RhoAccessibilityAudit.FieldStrengths(rho).Sum(x => x * x);

    public static (string Candidate, Func<double[], double> Functional)[] Candidates() => new (string, Func<double[], double>)[]
    {
        ("entropy", Entropy),
        ("free room", FreeRoom),
        ("actualization density", ActualizationDensity),
        ("flux sector", FluxSector),
        ("clock functional", ClockFunctional),
        ("field functional", FieldFunctional),
    };

    // ===================== 2. THE PHASE PROJECTION OF EACH GRADIENT =====================

    /// <summary>
    /// The gradient of a scalar functional. Where the gradient is ANALYTIC it is used exactly, and only the genuinely
    /// nonlinear functionals are differenced - because a first version differenced everything and the CANCELLATION FLOOR
    /// of central differences made two phase-BLIND candidates look sensitive: free room is the simplex constraint, whose
    /// gradient is exactly the constant direction, yet the differenced version reported a phase projection of 2.540E-008
    /// where the truth is zero (values of order 96 with a step of 1E-6 lose about ten digits). The differencing step is
    /// also raised to 1E-4, which puts the remaining floor near 1E-12 - below every threshold the audit tests.
    /// </summary>
    public static double[] Gradient(string candidate, double step = 1e-4)
    {
        var rho = State();
        switch (candidate)
        {
            case "free room":                                    // d(sum rho)/d rho_i = 1
                return Enumerable.Repeat(1.0, Cells).ToArray();
            case "actualization density":                        // d(mean rho)/d rho_i = 1/N
                return Enumerable.Repeat(1.0 / Cells, Cells).ToArray();
            case "flux sector":                                  // the label reads the links, not the state
                return new double[Cells];
            case "clock functional":                             // d(sum rho^(1/d))/d rho_i = (1/d) rho^(1/d - 1)
                return rho.Select(r => (1.0 / D) * Math.Pow(r, 1.0 / D - 1.0)).ToArray();
            default:
            {
                var functional = Candidates().Single(c => c.Candidate == candidate).Functional;
                var g = new double[Cells];
                for (int i = 0; i < Cells; i++)
                {
                    var up = (double[])rho.Clone(); up[i] += step;
                    var down = (double[])rho.Clone(); down[i] -= step;
                    g[i] = (functional(up) - functional(down)) / (2.0 * step);
                }
                return g;
            }
        }
    }

    /// <summary>True when the candidate's gradient is analytic rather than differenced.</summary>
    public static bool UsesAnalyticGradient(string candidate)
        => candidate is "free room" or "actualization density" or "flux sector" or "clock functional";

    /// <summary>The gradient's components along the 53 phase directions - how much it could constrain them.</summary>
    public static double[] PhaseProjection(string candidate)
    {
        var g = Gradient(candidate);
        return PhaseBasis().Select(p => g.Zip(p.Mode, (a, b) => a * b).Sum()).ToArray();
    }

    /// <summary>
    /// A candidate's phase projection, located by NAME. A first version located it by delegate identity and threw at
    /// once: a method group converts to a NEW delegate on every evaluation, so ReferenceEquals never matched. The
    /// lookup is by name because that is what actually identifies a candidate.
    /// </summary>
    public static double[] PhaseProjectionOf(string candidate) => PhaseProjection(candidate);

    public static double PhaseProjectionNorm(string candidate)
        => Math.Sqrt(PhaseProjectionOf(candidate).Sum(x => x * x));

    public static double GradientNorm(string candidate)
        => Math.Sqrt(Gradient(candidate).Sum(x => x * x));

    /// <summary>
    /// A phase-BLIND candidate constrains nothing: its phase projection is zero. The threshold is stated with its
    /// origin rather than chosen: the exact zeros sit at about 4E-14, because a phase mode's own sum is zero only to
    /// about 1E-16 and it is multiplied by 96 unit components - so 1E-9 is six orders of magnitude above the floor and
    /// four orders below the smallest genuine response (the entropy's 3.3E-004).
    /// </summary>
    public static bool IsPhaseBlind(string candidate)
        => PhaseProjectionNorm(candidate) < 1e-9;

    /// <summary>The floor an exact zero can show, measured on the constant direction whose phase projection is zero.</summary>
    public static double PhaseProjectionFloor()
        => Math.Sqrt(PhaseBasis().Sum(p => Math.Pow(p.Mode.Sum(), 2)));

    /// <summary>How many phase directions a candidate constrains - at most one, because a gradient is one vector.</summary>
    public static int ConstrainedDirections(string candidate)
        => IsPhaseBlind(candidate) ? 0 : 1;

    public static (string Candidate, double ProjectionNorm, int Constrained, string Class)[] CandidateTable()
        => Candidates().Select(c =>
        {
            double norm = PhaseProjectionNorm(c.Candidate);
            return (c.Candidate, norm, ConstrainedDirections(c.Candidate), IsPhaseBlind(c.Candidate) ? "PHASE-BLIND" : "phase-sensitive");
        }).ToArray();

    public static string[] PhaseBlindCandidates() => CandidateTable().Where(t => t.Class == "PHASE-BLIND").Select(t => t.Candidate).ToArray();
    public static string[] PhaseSensitiveCandidates() => CandidateTable().Where(t => t.Class == "phase-sensitive").Select(t => t.Candidate).ToArray();

    // ===================== 3. THE RANK OF WHAT THE CANDIDATES CAN CONSTRAIN =====================

    /// <summary>The number of independent phase directions the candidates collectively constrain.</summary>
    public static int ConstraintRank()
    {
        var basis = new List<double[]>();
        foreach (var (candidate, _) in Candidates())
        {
            var v = PhaseProjectionOf(candidate);
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (x, y) => x * y).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-8) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    /// <summary>The dimensions of the phase sector that NO candidate constrains - the measured answer to "why this phase".</summary>
    public static int Deficiency() => PhaseDimension() - ConstraintRank();

    public static bool TheCountingBoundHolds() => ConstraintRank() <= Candidates().Length;

    public static string TheDeficiency()
        => $"measured rather than bounded: {ConstraintRank()} of the {PhaseDimension()} phase directions are constrained "
         + $"by the {Candidates().Length} candidates and {Deficiency()} are not - and those {Deficiency()} are free for "
         + "every combination the candidates could ever form, because a scalar functional's gradient is a single vector";

    // ===================== 4. IS THE STATE EVEN A CRITICAL POINT? =====================

    /// <summary>The directional derivative of a functional along a phase direction - zero only at a critical point.</summary>
    public static double PhaseDirectionalDerivative(string candidate, double[] direction, double step = 1e-4)
    {
        var functional = Candidates().Single(c => c.Candidate == candidate).Functional;
        var up = AmplitudePhaseAudit.Step(direction, step);
        var down = AmplitudePhaseAudit.Step(direction, -step);
        return (functional(up) - functional(down)) / (2.0 * step);
    }

    /// <summary>Largest directional derivative over the phase basis: non-zero means the state is NOT an extremum.</summary>
    public static double LargestPhaseDerivative(string candidate)
        => PhaseBasis().Max(p => Math.Abs(PhaseDirectionalDerivative(candidate, p.Mode)));

    public static (string Candidate, double LargestDerivative)[] CriticalPointTable()
        => Candidates().Select(c => (c.Candidate, LargestPhaseDerivative(c.Candidate))).ToArray();

    /// <summary>The sensitive candidates are not extremised here: nothing is stationary in the phase directions.</summary>
    public static bool NoCandidateIsStationary()
        => Candidates()
            .Where(c => !IsPhaseBlind(c.Candidate))
            .All(c => LargestPhaseDerivative(c.Candidate) > 1e-9);

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: the candidates pin every phase direction, so a preferred phase exists. REFUTED: none of them
    /// constrains anything, so the question is void. BOUNDARY: they constrain a measured subset and the rest is free.
    /// </summary>
    public static string Verdict()
    {
        if (!TheCountingBoundHolds()) return "REFUTED";        // the rank exceeded the number of functionals: impossible
        if (ConstraintRank() == 0) return "REFUTED";           // nothing can distinguish any phase
        if (ConstraintRank() >= PhaseDimension()) return "DERIVED";   // a preferred phase would exist
        return "BOUNDARY";                                     // a measured subset is constrained, the rest is free
    }

    public static string TheCriticalQuestion()
        => Deficiency() > 0
            ? $"why this phase instead of another has NO answer inside AT for the {Deficiency()} unconstrained "
              + "directions, because no quantity the theory contains is sensitive to them"
            : "the candidates would pin every phase direction, so a preferred phase would exist";

    public static string WhereItStands()
        => "NO EXISTING AT QUANTITY CAN ASSIGN A PREFERRED PHASE STATE, AND THE AUDIT CAN SAY EXACTLY HOW MUCH OF THE "
         + "PHASE SECTOR IS LEFT FREE. G_054 established that the phase coordinates are freely assigned; this audit asks "
         + "whether any quantity the theory already contains could break that degeneracy, and the answer turns on a "
         + "counting fact that is exact rather than heuristic. THE COUNTING ARGUMENT IS THE WHOLE AUDIT. A quantity that "
         + "could assign a preferred phase must be a SCALAR functional - something one could extremise - and the "
         + "gradient of a scalar is ONE vector, whose projection into the "
         + $"{PhaseDimension()}-dimensional phase sector therefore constrains AT MOST ONE phase direction. Six "
         + $"candidates cannot pin {PhaseDimension()} directions however they are combined, and the bound is measured "
         + $"rather than assumed: the rank is {ConstraintRank()}, so the DEFICIENCY is {Deficiency()} directions that no "
         + "combination of these quantities can ever constrain. THREE OF THE SIX CONSTRAIN NOTHING AT ALL, and the audit "
         + "states that separately because it is a stronger claim than weakness. Free room is the simplex constraint, "
         + "whose gradient is the CONSTANT direction - and the constant is the simplex direction, orthogonal to every "
         + "non-constant mode, so its phase projection is exactly zero "
         + $"({PhaseProjectionNorm("free room"):E3}). The flux sector is decoupled from the organisation altogether, with a "
         + $"coupling census of {FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase()}. And the "
         + "actualization density's scalar level is that same constant. THE OTHER THREE DO CONTRIBUTE - "
         + $"{string.Join(", ", PhaseSensitiveCandidates())} - each along ONE direction, and the audit reports the norms "
         + "rather than treating them as negligible: the phase sector is not uniformly unselectable, it is unselectable "
         + "in all but a measured handful of directions. THE CRITICAL QUESTION IS THEN ANSWERED IN ITS OWN TERMS. Asked "
         + "why this phase instead of another, the theory has no answer for the "
         + $"{Deficiency()} unconstrained directions, because nothing it contains is sensitive to them; for the "
         + "directions the candidates do see, the answer WOULD be that the quantity is extremal there - and the audit "
         + $"tests whether the audited state is even a critical point of them. It is not: the largest phase directional "
         + "derivative is non-zero for every sensitive candidate "
         + $"({string.Join(", ", CriticalPointTable().Where(t => t.Item2 > 1e-9).Select(t => $"{t.Candidate} {t.LargestDerivative:E3}"))}), "
         + "so nothing is extremised at this state either. WHAT WOULD BREAK THE DEGENERACY, stated positively: a set of "
         + $"{PhaseDimension()} independent scalar functionals, or one non-scalar structure whose gradient spans the "
         + "phase sector. AT has neither, which is why the answer is BOUNDARY rather than a shrug: the degeneracy is "
         + "broken in a measured number of directions and survives in the rest.";

    // ===================== REPORT =====================

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE CANDIDATES: WHAT EACH CAN CONSTRAIN");
        sb.AppendLine("   candidate              | phase projection norm | directions | class");
        foreach (var (candidate, norm, constrained, cls) in CandidateTable())
            sb.AppendLine($"   {candidate,-22} | {norm,21:E3} | {constrained,10} | {cls}");
        sb.AppendLine($"   phase-blind     : {string.Join(", ", PhaseBlindCandidates())}");
        sb.AppendLine($"   phase-sensitive : {string.Join(", ", PhaseSensitiveCandidates())}");
        return sb.ToString();
    }

    public static string OutputDeficiency()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE COUNTING FACTS AND THE MEASURED DEFICIENCY");
        sb.AppendLine($"   phase dimensions                       : {PhaseDimension()}");
        sb.AppendLine($"   candidates                             : {Candidates().Length}");
        sb.AppendLine($"   a scalar's gradient is ONE vector, so it constrains at most one direction");
        sb.AppendLine($"   measured constraint rank               : {ConstraintRank()}");
        sb.AppendLine($"   counting bound holds                   : {TheCountingBoundHolds()}");
        sb.AppendLine($"   DEFICIENCY (free phase directions)     : {Deficiency()}");
        sb.AppendLine($"   {TheDeficiency()}");
        return sb.ToString();
    }

    public static string OutputCriticalPoint()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. IS THE AUDITED STATE A CRITICAL POINT OF ANYTHING?");
        sb.AppendLine("   candidate              | largest phase directional derivative");
        foreach (var (candidate, derivative) in CriticalPointTable())
            sb.AppendLine($"   {candidate,-22} | {derivative,36:E3}");
        sb.AppendLine($"   no sensitive candidate is stationary : {NoCandidateIsStationary()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   the critical question : {TheCriticalQuestion()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
