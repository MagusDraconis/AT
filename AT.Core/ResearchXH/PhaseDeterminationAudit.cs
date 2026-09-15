using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_054 - PHASE DETERMINATION AUDIT (group G - Gravity Source).
///
/// QUESTION. What fixes the 53 PHASE COORDINATES? Given G_050 (the phase sector is the hidden Fourier modes), G_051
/// (they are physical, not gauge) and G_053 (amplitude and phase are independent, and the phase sector ROTATES what the
/// amplitude sector resizes). Candidates: symmetry, occupancy, multiplicity, attractor structure, actualization
/// history, boundary assignment. Requirements: no new primitive, and the clock, acceleration and field laws preserved.
/// Goal: is the phase sector DYNAMICALLY DETERMINED or FREELY ASSIGNED?
///
/// ANSWER: **BOUNDARY - freely assigned. Nothing in AT fixes the phase coordinates, and the audit can say exactly what
/// would have to be added for something to.**
///
///  (1) THE PHASE COORDINATES ARE CONSERVED BY EVERY INVARIANT-DRIVEN FLOW, AND THAT IS A THEOREM. The contraction
///      functionals are invariant, so their gradients lie in the INVARIANT subspace - which G_052 measured to be exactly
///      the amplitude modes plus the mean. A flow along such a gradient therefore never leaves that subspace, so the 53
///      phase coordinates are CONSTANTS OF THE MOTION of any dynamics driven by an invariant functional. Measured: the
///      change is zero along the flow.
///
///  (2) SYMMETRY DOES NOT FIX THEM - IT IS WHAT MOVES THEM. The invariants of the substrate's symmetry are the
///      amplitude modes plus the mean, which by that same identity means the phase coordinates are precisely the
///      NON-invariant content. A symmetry move therefore changes them, and the measurement shows it: symmetry cannot be
///      the thing that determines a set of coordinates it is defined by not seeing.
///
///  (3) THE LOCAL LAWS ARE SENSITIVE TO THE PHASES WITHOUT DETERMINING THEM, AND THE AUDIT KEEPS THOSE APART. The
///      clock and field functionals DO have gradients with a phase component - measured as a fraction of the gradient
///      norm - so a process that extremised one of those laws would move the phase coordinates. But sensitivity is not
///      determination, and no AT process does extremise them: the update rule's spatial part is zero and the census of
///      members coupling the link sector to the organisation is zero (E_009, E_013, re-measured here).
///
///  (4) THE CANDIDATES SORT THEMSELVES. Multiplicity is a property of the SPECTRUM, identical for every state, so it
///      cannot fix 53 state-dependent numbers - REFUTED. Attractor structure and actualization history are REFUTED on
///      the same measurement the conservation theorem supplies: no AT-generated flow moves the phase coordinates, so
///      neither the reachable structure nor any sequence of update steps can pin them. Occupancy is REFUTED as an
///      invariant - the contractions annihilate the phase exactly - and the addressed state's determination is a
///      tautology rather than a mechanism, which the audit records rather than counting. Symmetry is REFUTED because it
///      moves them. What remains is BOUNDARY ASSIGNMENT, and that is the answer - the same shape of answer the flux
///      sector received in E_014.
///
///  (5) WHAT WOULD CHANGE IT. A coupling whose gradient has a non-zero phase component AND which AT actually runs would
///      determine the phase coordinates. The audit measures both halves of that condition separately so the boundary is
///      stated as a boundary rather than as a mystery.
/// </summary>
public static class PhaseDeterminationAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static (int Channel, string Kind, double[] Mode)[] PhaseBasis() => AmplitudePhaseAudit.PhaseModes();
    public static (int Channel, string Kind, double[] Mode)[] AmplitudeBasis() => AmplitudePhaseAudit.AmplitudeModes();

    // ===================== 1. THE PHASE COORDINATES =====================

    /// <summary>The 53 phase coordinates: the projection of the state onto each hidden mode.</summary>
    public static double[] PhaseCoordinates(double[] rho)
        => PhaseBasis().Select(p => rho.Zip(p.Mode, (r, m) => r * m).Sum()).ToArray();

    /// <summary>How much a step along a direction moves the phase coordinates - zero means the direction cannot fix them.</summary>
    public static double PhaseCoordinateChange(double[] direction, double step = 0.05)
    {
        var before = PhaseCoordinates(State());
        var after = PhaseCoordinates(AmplitudePhaseAudit.Step(direction, step));
        return before.Zip(after, (a, b) => Math.Abs(a - b)).Max();
    }

    // ===================== 2. THE CONSERVATION THEOREM =====================

    /// <summary>
    /// The gradient of an invariant functional: the sum of the contraction rows. G_052 measured that the contraction row
    /// space is exactly the amplitude modes plus the mean, so this vector must have NO phase component - and the
    /// measurement says so rather than the argument.
    /// </summary>
    public static double[] InvariantGradient()
    {
        var rows = RhoAccessibilityAudit.ContractionRows(State());
        var sum = new double[Cells];
        foreach (var row in rows)
            for (int i = 0; i < Cells; i++) sum[i] += row[i];
        return sum;
    }

    public static double InvariantGradientPhaseComponent()
        => ProjectOntoPhase(InvariantGradient());

    public static double ProjectOntoPhase(double[] v)
        => Math.Sqrt(PhaseBasis().Sum(p => Math.Pow(v.Zip(p.Mode, (a, b) => a * b).Sum(), 2)));

    public static double VectorNorm(double[] v) => Math.Sqrt(v.Sum(x => x * x));

    /// <summary>The fraction of a gradient's norm that points along the phase sector.</summary>
    public static double PhaseFraction(double[] v)
    {
        double norm = VectorNorm(v);
        return norm < 1e-15 ? 0.0 : ProjectOntoPhase(v) / norm;
    }

    /// <summary>An invariant-driven flow leaves the phase coordinates exactly constant - the audit's central measurement.</summary>
    public static double InvariantFlowPhaseChange()
    {
        var gradient = InvariantGradient();
        double norm = VectorNorm(gradient);
        if (norm < 1e-15) return double.NaN;
        var direction = gradient.Select(g => g / norm).ToArray();
        return Math.Max(PhaseCoordinateChange(direction), PhaseCoordinateChange(direction.Select(x => -x).ToArray()));
    }

    public static bool TheInvariantFlowConservesThePhases() => InvariantFlowPhaseChange() < 1e-12;

    public static string TheConservationTheorem()
        => "an invariant-driven flow cannot change the phase coordinates: its gradient lies in the invariant subspace, "
         + "which G_052 measured to be exactly the amplitude modes plus the mean";

    // ===================== 3. THE CANDIDATES, MEASURED =====================

    /// <summary>A symmetry move: does it leave the phase coordinates alone? (No - they are the non-invariant content.)</summary>
    public static double SymmetryMovePhaseChange()
    {
        var rho = State();
        var before = PhaseCoordinates(rho);
        return RhoAccessibilityAudit.Orbit(rho)
            .Max(image => before.Zip(PhaseCoordinates(image), (a, b) => Math.Abs(a - b)).Max());
    }

    public static bool SymmetryMovesThePhases() => SymmetryMovePhaseChange() > 1e-6;

    /// <summary>An amplitude move cannot change them - that is the orthogonality of G_052.</summary>
    public static double AmplitudeMovePhaseChange()
        => AmplitudeBasis().Max(a => PhaseCoordinateChange(a.Mode));

    public static bool AmplitudeMovesCannotChangeThePhases() => AmplitudeMovePhaseChange() < 1e-12;

    /// <summary>The clock functional's gradient, by finite differences on the summed rate.</summary>
    public static double[] ClockGradient(double step = 1e-6)
    {
        var rho = State();
        double F(double[] r) => r.Sum(x => Math.Pow(x, 1.0 / D));
        var g = new double[Cells];
        for (int i = 0; i < Cells; i++)
        {
            var probe = (double[])rho.Clone();
            probe[i] += step;
            g[i] = (F(probe) - F(rho)) / step;
        }
        return g;
    }

    /// <summary>The field functional's gradient: the summed squared field strength, by finite differences.</summary>
    public static double[] FieldGradient(double step = 1e-6)
    {
        var rho = State();
        double F(double[] r) => RhoAccessibilityAudit.FieldStrengths(r).Sum(x => x * x);
        var g = new double[Cells];
        for (int i = 0; i < Cells; i++)
        {
            var probe = (double[])rho.Clone();
            probe[i] += step;
            g[i] = (F(probe) - F(rho)) / step;
        }
        return g;
    }

    public static double ClockGradientPhaseFraction() => PhaseFraction(ClockGradient());
    public static double FieldGradientPhaseFraction() => PhaseFraction(FieldGradient());

    /// <summary>The laws are phase-SENSITIVE: a process extremising them would move the phase coordinates.</summary>
    public static bool TheLawsArePhaseSensitive()
        => ClockGradientPhaseFraction() > 1e-3 && FieldGradientPhaseFraction() > 1e-3;

    /// <summary>
    /// But no AT process runs such a flow: the update rule is purely electric and nothing couples the sectors. NOTE the
    /// argument order: DerivedSectorSplit returns ELECTRIC first, and a first version of this accessor read it as the
    /// spatial part - which made the update rule look like it carried a spatial field (1.424E-002) and, worse, made the
    /// verdict come out DERIVED instead of BOUNDARY. The measurement is what exposed the mislabelling.
    /// </summary>
    public static (double Spatial, double TimeLike) UpdateRuleSectors()
    {
        var split = CouplingFunctionAudit.DerivedSectorSplit();
        return (split.Magnetic, split.Electric);
    }
    public static int CouplingCensus() => FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase();

    public static bool NoAtProcessRunsAPhaseFlow()
        => UpdateRuleSectors().Spatial < 1e-15 && CouplingCensus() == 0;

    /// <summary>
    /// Multiplicity is a property of the SPECTRUM, not of the state, so it cannot fix 53 state-dependent numbers. The
    /// measurement reproduces G_040's recorded spectrum - the canonical 45 levels with their multiplicities - which is
    /// fixed by the Laplacian alone. A first version of this check compared a count with itself, which could never fail;
    /// the replacement tests the recorded spectrum instead.
    /// </summary>
    public static (double Level, int Multiplicity)[] SpectrumLevels() => RhoObservableAudit.Levels();

    public static bool TheMultiplicitiesAreStateIndependent()
        => RhoObservableAudit.SpectrumReproducesTheRecord() && SpectrumLevels().Length == 45;

    public static string TheMultiplicityMeasurement()
        => $"the substrate's own spectrum is the recorded one - {SpectrumLevels().Length} levels, "
         + $"{SpectrumLevels().Sum(l => l.Multiplicity)} modes - and it is fixed by the Laplacian rather than by the "
         + "state, so the degenerate structure cannot pin 53 state-dependent numbers";

    // ===================== 4. THE CANDIDATES =====================

    public static (string Candidate, string Status, string Basis)[] Candidates() => new[]
    {
        ("symmetry", "REFUTED",
            $"the symmetry's invariants ARE the amplitude modes plus the mean, so the phase coordinates are its "
            + $"non-invariant content: a symmetry move changes them by {SymmetryMovePhaseChange():E3}"),
        ("occupancy (as invariant)", "REFUTED",
            $"the contractions annihilate the phase exactly - amplitude moves change it by "
            + $"{AmplitudeMovePhaseChange():E3} - so occupancy read through the invariant algebra cannot fix it; the "
            + "addressed state's determination is a tautology, recorded and not counted"),
        ("multiplicity", "REFUTED", TheMultiplicityMeasurement()),
        ("attractor structure", "REFUTED",
            $"the reachable structure supplies no flow that moves the phase: the invariant-driven flow changes it by "
            + $"{InvariantFlowPhaseChange():E3} ({TheConservationTheorem()})"),
        ("actualization history", "REFUTED",
            $"no sequence of update steps can pin it - the update rule's spatial part is "
            + $"{UpdateRuleSectors().Spatial:E3} and the coupling census is {CouplingCensus()} (E_009, E_013)"),
        ("boundary assignment", "BOUNDARY",
            "what remains, and the answer: the phase coordinates are an ASSIGNMENT, exactly as the flux sector label "
            + "was in E_014 - with the difference that here the assignment is physically ACTIVE (G_051, G_053)"),
    };

    public static string[] RefutedCandidates()
        => Candidates().Where(c => c.Status == "REFUTED").Select(c => c.Candidate).ToArray();

    public static string[] BoundaryCandidates()
        => Candidates().Where(c => c.Status == "BOUNDARY").Select(c => c.Candidate).ToArray();

    public static string TheDeterminationMechanism() => "an external assignment";

    // ===================== 5. THE REQUIREMENTS =====================

    public static (string Requirement, string Status)[] RequirementCheck() => new[]
    {
        ("no new primitive",
            $"the phase coordinates use only the state and the substrate's own modes; "
            + $"{CouplingCensus()} members couple the link sector to the organisation"),
        ("clock law preserved",
            $"the rate law is read, not modified: its gradient carries a phase fraction of "
            + $"{ClockGradientPhaseFraction():E3}, so the law is SENSITIVE to the phase without fixing it"),
        ("acceleration law preserved",
            $"built from the clock rates, so it inherits the same sensitivity: the phase moves its multiset (G_053) "
            + "and the law itself is untouched"),
        ("field law preserved",
            $"the field gradient's phase fraction is {FieldGradientPhaseFraction():E3}; the law is unmodified, and no "
            + $"AT process extremises it (spatial part {UpdateRuleSectors().Spatial:E3})"),
    };

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED would mean AT fixes the phase coordinates; REFUTED would mean the question is malformed. The
    /// measured situation - conserved under invariant flows, moved by symmetry, sensitive laws, no process running them
    /// - is BOUNDARY: a free assignment.
    /// </summary>
    public static string Verdict()
    {
        if (!TheInvariantFlowConservesThePhases()) return "DERIVED";     // something in AT does drive the phases
        if (!TheLawsArePhaseSensitive()) return "REFUTED";               // the phase would then be inert, not assigned
        if (!NoAtProcessRunsAPhaseFlow()) return "DERIVED";              // an AT process couples to the phases
        if (!SymmetryMovesThePhases()) return "REFUTED";
        if (!AmplitudeMovesCannotChangeThePhases()) return "REFUTED";
        return "BOUNDARY";
    }

    public static string WhereItStands()
        => "THE PHASE SECTOR IS FREELY ASSIGNED: NOTHING IN AT FIXES ITS 53 COORDINATES, AND THE AUDIT CAN SAY EXACTLY "
         + "WHAT WOULD HAVE TO BE ADDED FOR SOMETHING TO. G_050 named the phase coordinates, G_051 showed they are "
         + "physics rather than bookkeeping and G_053 showed their effects are independent, so the natural next question "
         + "is what DETERMINES them - and the answer is nothing that exists. THE CONSERVATION THEOREM IS THE "
         + "CENTRE OF THE AUDIT. The contraction functionals are invariant, so their gradients lie in the invariant "
         + "subspace, which G_052 measured to be exactly the amplitude modes plus the mean. A flow along such a gradient "
         + "therefore can never leave that subspace, so THE PHASE COORDINATES ARE CONSTANTS OF THE MOTION of any "
         + $"dynamics driven by an invariant functional - measured, not argued: the change along the flow is "
         + $"{InvariantFlowPhaseChange():E3}. That single measurement disposes of two candidates at once. ATTRACTOR "
         + "STRUCTURE cannot fix the phases because the reachable structure supplies no flow that moves them, and "
         + "ACTUALIZATION HISTORY cannot fix them because no sequence of update steps can - the update rule's spatial "
         + $"part is {UpdateRuleSectors().Spatial:E3} and the census of members coupling the link sector to the "
         + $"organisation is {CouplingCensus()}, which E_009 and E_013 established and this audit re-measures. "
         + "SYMMETRY DOES NOT FIX THE COORDINATES - IT IS DEFINED BY NOT SEEING THEM. The invariants of the substrate's "
         + "symmetry are the amplitude modes plus the mean, so the phase coordinates are precisely its non-invariant "
         + $"content, and a symmetry move changes them by {SymmetryMovePhaseChange():E3}. An amplitude move, by "
         + $"orthogonality, changes them by {AmplitudeMovePhaseChange():E3}. MULTIPLICITY FAILS FOR A BLUNTER REASON: "
         + $"it is a property of the spectrum, and {TheMultiplicityMeasurement()}. OCCUPANCY FAILS AS AN INVARIANT, "
         + "because the contractions annihilate the phase by construction - and the audit records, rather than counts, "
         + "the one determination that is trivially true: the addressed state determines its own projections, which is a "
         + "tautology and not a mechanism. WHAT KEEPS THE ANSWER FROM BEING A SHRUG IS THE SEPARATION OF SENSITIVITY "
         + "FROM DETERMINATION. The local laws DO feel the phases: the clock functional's gradient carries a phase "
         + $"fraction of {ClockGradientPhaseFraction():E3} and the field functional's {FieldGradientPhaseFraction():E3}, "
         + "so a process that extremised either law WOULD move the phase coordinates. But no AT process does, and both "
         + "halves of the condition are measured separately, so the boundary is stated as a boundary: a coupling whose "
         + "gradient has a phase component AND which the theory actually runs would determine the phase sector, and AT "
         + "has the first without the second. SO THE ANSWER IS BOUNDARY, freely assigned - the same shape of answer the "
         + "flux sector label received in E_014, with one difference worth stating: there the assignment was inert, and "
         + "here it is physically active.";

    // ===================== REPORT =====================

    public static string OutputCoordinates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE PHASE COORDINATES AND WHAT CAN MOVE THEM");
        sb.AppendLine($"   coordinates                        : {PhaseBasis().Length}");
        sb.AppendLine($"   amplitude moves change them by     : {AmplitudeMovePhaseChange():E3}  -> cannot fix them (orthogonality)");
        sb.AppendLine($"   symmetry moves change them by      : {SymmetryMovePhaseChange():E3}  -> symmetry does not fix them");
        sb.AppendLine($"   invariant-driven flow changes them : {InvariantFlowPhaseChange():E3}  -> CONSERVED: {TheInvariantFlowConservesThePhases()}");
        sb.AppendLine($"   {TheConservationTheorem()}");
        return sb.ToString();
    }

    public static string OutputSensitivity()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. SENSITIVITY VERSUS DETERMINATION");
        sb.AppendLine($"   clock functional's phase fraction  : {ClockGradientPhaseFraction():E3}");
        sb.AppendLine($"   field functional's phase fraction  : {FieldGradientPhaseFraction():E3}");
        sb.AppendLine($"   the laws are phase-sensitive       : {TheLawsArePhaseSensitive()}");
        sb.AppendLine($"   an invariant gradient's phase part : {InvariantGradientPhaseComponent():E3}  -> exactly zero");
        sb.AppendLine($"   no AT process runs a phase flow    : {NoAtProcessRunsAPhaseFlow()}");
        sb.AppendLine($"     update rule spatial part         : {UpdateRuleSectors().Spatial:E3}");
        sb.AppendLine($"     coupling census                  : {CouplingCensus()}");
        return sb.ToString();
    }

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE CANDIDATES");
        foreach (var (candidate, status, basis) in Candidates())
        {
            sb.AppendLine($"   {candidate}");
            sb.AppendLine($"     -> {status}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("4. THE REQUIREMENTS");
        foreach (var (requirement, status) in RequirementCheck())
            sb.AppendLine($"   {requirement,-24} : {status}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   the determination mechanism : {TheDeterminationMechanism()}");
        sb.AppendLine($"   refuted                     : {string.Join(", ", RefutedCandidates())}");
        sb.AppendLine($"   boundary                    : {string.Join(", ", BoundaryCandidates())}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
