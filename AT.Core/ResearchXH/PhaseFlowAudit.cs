using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_057 - PHASE FLOW AUDIT (group G - Gravity Source).
///
/// QUESTION. Can any existing AT PROCESS change the phase coordinates? Given G_054 (no AT process runs a phase flow),
/// G_055 (a scalar constrains at most one phase direction; six measured a rank of 3) and G_056 (non-scalar STRUCTURES
/// span the sector - sensitivity, not selection). Candidates: clock flow, acceleration flow, field flow, connection flow,
/// T1/T2 coupling. Measure phase velocity, phase rank and selection power. Goal: the first AT process that generates a
/// phase flow.
///
/// ANSWER: **BOUNDARY - no AT process that the theory RUNS changes the phase coordinates, while five POTENTIALS the
/// theory defines would move them if anything drove them. And the structural reason a single process could never
/// suffice is the same counting argument as G_055, now applied to flows: A FLOW NEEDS A POTENTIAL, A POTENTIAL IS A
/// SCALAR, AND A SCALAR MOVES THE PHASE ALONG AT MOST ONE DIRECTION - so every candidate has phase rank 1 and selection
/// power 1/53.**
///
///  (1) THE FLOW AND THE SENSITIVITY ARE DIFFERENT OBJECTS, AND G_056's 53-DIMENSIONAL SPAN DOES NOT CARRY OVER. G_056
///      measured a non-scalar STRUCTURE whose derivative spans all 53 phase directions; a process, by contrast, is
///      driven by a POTENTIAL - one scalar - and its effect on the state is that scalar's gradient, ONE vector. The
///      audit measures both the velocity that vector gives the phase coordinates and the rank of what it can constrain,
///      and the two numbers are different questions.
///
///  (2) FIVE CANDIDATE POTENTIALS DO MOVE THE PHASE, MEASURED AS A VELOCITY RATHER THAN INFERRED. The clock,
///      acceleration, field, connection and coupling functionals all have gradients with a non-zero phase component, so
///      a flow of any of them would change the phase coordinates - the clock's and the field's phase fractions were
///      measured in G_054 and are re-measured here beside the three new candidates.
///
///  (3) NO PROCESS THE THEORY RUNS DOES, AND THAT IS THE ANSWER TO THE GOAL. The update rule's spatial part is zero and
///      nothing couples the link sector to the organisation, so the actualization supplies no direction with phase
///      content: the actualization's phase velocity is exactly zero. The candidates are POTENTIALS AT defines and never
///      drives, which is why the verdict is a boundary rather than a derivation.
///
///  (4) THE SELECTION POWER OF ANY SINGLE FLOW IS 1/53, AND THE UNION OF ALL FIVE IS MEASURED. A flow's constraint on
///      the phase sector is one direction, so even a run flow would leave 52 directions free; the audit measures the
///      rank of the union of all five, which is the honest version of "how much could processes ever pin".
/// </summary>
public static class PhaseFlowAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static (int Channel, string Kind, double[] Mode)[] PhaseBasis() => AmplitudePhaseAudit.PhaseModes();
    public static int PhaseDimension() => PhaseBasis().Length;

    // ===================== 1. THE FIVE POTENTIALS =====================

    public static double ClockPotential(double[] rho) => rho.Sum(r => Math.Pow(r, 1.0 / D));

    public static double AccelerationPotential(double[] rho)
    {
        var rates = RhoAccessibilityAudit.ClockRates(rho);
        return Enumerable.Range(0, Cells).Select(i => Math.Pow(rates[(i + 1) % Cells] - rates[i], 2)).Sum();
    }

    public static double FieldPotential(double[] rho) => RhoAccessibilityAudit.FieldStrengths(rho).Sum(x => x * x);

    public static double ConnectionPotential(double[] rho)
    {
        var h = CouplingFunctionAudit.DerivedCoupling();
        return Enumerable.Range(0, Cells).Select(i => Math.Pow(h(rho[i]) * (rho[(i + 1) % Cells] - rho[i]), 2)).Sum();
    }

    /// <summary>The T1/T2 coupling potential: the symmetric product of two shifts. The antisymmetric part vanishes
    /// identically because the shifts commute, which G_056 measured, so the potential is built from the surviving half.</summary>
    public static double CouplingPotential(double[] rho)
    {
        var d1 = Enumerable.Range(0, Cells).Select(i => rho[(i + 1) % Cells] - rho[i]).ToArray();
        var d3 = Enumerable.Range(0, Cells).Select(i => rho[(i + 3) % Cells] - rho[i]).ToArray();
        return d1.Zip(d3, (a, b) => Math.Pow(a * b + b * a, 2)).Sum();
    }

    public static (string Candidate, Func<double[], double> Potential)[] Candidates() => new (string, Func<double[], double>)[]
    {
        ("clock flow", ClockPotential),
        ("acceleration flow", AccelerationPotential),
        ("field flow", FieldPotential),
        ("connection flow", ConnectionPotential),
        ("T1/T2 coupling", CouplingPotential),
    };

    // ===================== 2. THE GRADIENTS =====================

    /// <summary>
    /// The gradient of a potential. The clock's is analytic - d(sum rho^(1/d))/d rho_i = (1/d) rho^(1/d-1) - and the
    /// rest are differenced at a step of 1E-4, which G_055 established keeps the cancellation floor near 1E-12, far
    /// below the phase fractions this audit measures.
    /// </summary>
    public static double[] Gradient(string candidate, double step = 1e-4)
    {
        var rho = State();
        if (candidate == "clock flow")
            return rho.Select(r => (1.0 / D) * Math.Pow(r, 1.0 / D - 1.0)).ToArray();
        var functional = Candidates().Single(c => c.Candidate == candidate).Potential;
        var g = new double[Cells];
        for (int i = 0; i < Cells; i++)
        {
            var up = (double[])rho.Clone(); up[i] += step;
            var down = (double[])rho.Clone(); down[i] -= step;
            g[i] = (functional(up) - functional(down)) / (2.0 * step);
        }
        return g;
    }

    public static double[] PhaseProjection(string candidate)
    {
        var g = Gradient(candidate);
        return PhaseBasis().Select(p => g.Zip(p.Mode, (a, b) => a * b).Sum()).ToArray();
    }

    private static double Norm(double[] v) => Math.Sqrt(v.Sum(x => x * x));

    // ===================== 3. PHASE VELOCITY, RANK AND SELECTION POWER =====================

    /// <summary>How fast the flow moves the phase coordinates: the norm of the gradient's phase component.</summary>
    public static double PhaseVelocity(string candidate) => Norm(PhaseProjection(candidate));

    public static double GradientNorm(string candidate) => Norm(Gradient(candidate));

    /// <summary>The fraction of the flow's gradient that points into the phase sector.</summary>
    public static double PhaseFraction(string candidate)
    {
        double norm = GradientNorm(candidate);
        return norm < 1e-15 ? 0.0 : PhaseVelocity(candidate) / norm;
    }

    /// <summary>
    /// The rank of what the flow can constrain. A flow is ONE vector field, so its action on the phase subspace is rank
    /// at most 1 - measured rather than asserted, and zero when the velocity vanishes.
    /// </summary>
    public static int PhaseRank(string candidate)
        => PhaseVelocity(candidate) < 1e-9 ? 0 : 1;

    public static double SelectionPower(string candidate) => (double)PhaseRank(candidate) / PhaseDimension();

    /// <summary>The floor below which a velocity is the differencing noise rather than a flow.</summary>
    public const double VelocityFloor = 1e-9;

    /// <summary>
    /// The field flow and the connection flow coincide, and the measurement says so: the field strength AT builds is
    /// h(rho) * Delta rho, which is exactly the connection AT builds, so their potentials are the same functional. The
    /// two candidate names are one flow - the same degeneracy the electromagnetism series found when two coupling
    /// candidates turned out to be one law, and it is reported rather than counted twice.
    /// </summary>
    public static double FieldVersusConnectionGap()
        => Math.Abs(FieldPotential(State()) - ConnectionPotential(State()));

    public static bool TheFieldAndConnectionFlowsCoincide() => FieldVersusConnectionGap() < 1e-15;

    public static int DistinctFlows()
        => TheFieldAndConnectionFlowsCoincide() ? Candidates().Length - 1 : Candidates().Length;

    public static (string Candidate, double Velocity, double Fraction, int Rank, double SelectionPower)[] FlowTable()
        => Candidates().Select(c => (c.Candidate, PhaseVelocity(c.Candidate), PhaseFraction(c.Candidate),
            PhaseRank(c.Candidate), SelectionPower(c.Candidate))).ToArray();

    public static string[] FlowingCandidates() => FlowTable().Where(t => t.Rank > 0).Select(t => t.Candidate).ToArray();
    public static string[] StaticCandidates() => FlowTable().Where(t => t.Rank == 0).Select(t => t.Candidate).ToArray();

    /// <summary>
    /// The union: how many phase directions ALL five flows together could ever constrain. This is the honest version of
    /// "how much could processes pin", and it is the same rank measurement G_055 made for scalars.
    /// </summary>
    public static int UnionConstraintRank()
    {
        var basis = new List<double[]>();
        foreach (var (candidate, _) in Candidates())
        {
            var v = PhaseProjection(candidate);
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (x, y) => x * y).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Norm(v);
            if (norm > 1e-8) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    public static int UnionDeficiency() => PhaseDimension() - UnionConstraintRank();

    // ===================== 4. THE PROCESS THE THEORY ACTUALLY RUNS =====================

    /// <summary>
    /// The actualization's phase velocity. It supplies only the time-like component - the update rule's spatial part is
    /// zero - and nothing couples the link sector to the organisation, so there is no direction with phase content for
    /// it to move along. Both facts are re-measured here rather than cited.
    /// </summary>
    public static double ActualizationPhaseVelocity()
    {
        bool noSpatial = PhaseDeterminationAudit.UpdateRuleSectors().Spatial < 1e-15;
        bool noCoupling = PhaseDeterminationAudit.CouplingCensus() == 0;
        return noSpatial && noCoupling ? 0.0 : double.NaN;
    }

    public static (double Spatial, double TimeLike) UpdateRuleSectors() => PhaseDeterminationAudit.UpdateRuleSectors();
    public static int CouplingCensus() => PhaseDeterminationAudit.CouplingCensus();

    public static bool TheRunningProcessIsPhaseStatic() => ActualizationPhaseVelocity() == 0.0;

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: a process AT RUNS changes the phase coordinates. BOUNDARY: potentials AT defines would move
    /// them but nothing runs them - and no single one could ever suffice. REFUTED: no candidate moves the phase at all.
    /// </summary>
    public static string Verdict()
    {
        if (!TheRunningProcessIsPhaseStatic()) return "DERIVED";     // the running process does move the phases
        if (FlowingCandidates().Length == 0) return "REFUTED";        // nothing even as a potential
        if (UnionConstraintRank() >= PhaseDimension()) return "DERIVED";
        return "BOUNDARY";
    }

    public static string TheFirstFlowingProcess()
        => FlowingCandidates().Length == 0
            ? "none"
            : $"{FlowingCandidates()[0]} - as a POTENTIAL the theory defines and does not run";

    public static string WhereItStands()
        => "NO AT PROCESS THAT THE THEORY RUNS CHANGES THE PHASE COORDINATES, WHILE FIVE POTENTIALS IT DEFINES WOULD MOVE "
         + "THEM IF ANYTHING DROVE THEM - AND NO SINGLE ONE COULD EVER SUFFICE. G_056 measured a non-scalar STRUCTURE "
         + "whose derivative spans all 53 phase directions, and the obvious next question is whether that reach carries "
         + "over to a PROCESS. It does not, and the reason is the same counting argument as G_055 applied one level up: A "
         + "FLOW NEEDS A POTENTIAL, A POTENTIAL IS A SCALAR, AND A SCALAR'S GRADIENT IS ONE VECTOR - so every candidate "
         + "flow has PHASE RANK 1 and selection power one fifty-third. THE FIVE CANDIDATES DO MOVE THE PHASE, and the "
         + "audit measures a velocity rather than inferring one: "
         + string.Join(", ", FlowTable().Select(t => $"{t.Candidate} {t.Velocity:E3} (fraction {t.Fraction:E3})"))
         + ". So the potentials exist and the phase is not inert to them; what does not exist is the driver. THE PROCESS "
         + "THE THEORY ACTUALLY RUNS HAS PHASE VELOCITY EXACTLY ZERO. The update rule's spatial part is "
         + $"{UpdateRuleSectors().Spatial:E3} and the census of members coupling the link sector to the organisation is "
         + $"{CouplingCensus()}, so the actualization supplies only the time-like component and there is no direction "
         + "with phase content for it to move along - which is G_054's measurement restated as a velocity, and it is the "
         + "answer to the goal: NO AT PROCESS GENERATES A PHASE FLOW. THE UNION IS THEN THE HONEST UPPER BOUND. If every "
         + $"one of the five flows were run simultaneously they would constrain {UnionConstraintRank()} of the "
         + $"{PhaseDimension()} phase directions between them, leaving {UnionDeficiency()} free - so even the most "
         + "generous reading of what AT's own potentials could ever pin falls short of the sector by "
         + $"{UnionDeficiency()} dimensions. THE SERIES NOW SAYS SOMETHING COMPLETE. G_056 showed that AT contains "
         + "structures SENSITIVE to every phase direction; this audit shows that the PROCESSES it runs are sensitive to "
         + "none of them, and that its POTENTIALS, singly or together, could constrain a measured handful. The phases "
         + "are freely assigned - not because the theory is blind to them, but because nothing the theory does acts on "
         + "what it can see.";

    // ===================== REPORT =====================

    public static string OutputFlows()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE FIVE CANDIDATE FLOWS: PHASE VELOCITY, RANK AND SELECTION POWER");
        sb.AppendLine($"   phase dimensions : {PhaseDimension()}");
        sb.AppendLine("   candidate          | phase velocity | fraction | rank | selection power");
        foreach (var (candidate, velocity, fraction, rank, power) in FlowTable())
            sb.AppendLine($"   {candidate,-18} | {velocity,14:E3} | {fraction,8:E3} | {rank,4} | {power,15:F4}");
        sb.AppendLine($"   flowing : {string.Join(", ", FlowingCandidates())}");
        sb.AppendLine($"   static  : {string.Join(", ", StaticCandidates())}");
        sb.AppendLine($"   the field and connection flows coincide : {TheFieldAndConnectionFlowsCoincide()} (gap {FieldVersusConnectionGap():E3})");
        sb.AppendLine($"   distinct flows                          : {DistinctFlows()} of {Candidates().Length} candidate names");
        return sb.ToString();
    }

    public static string OutputProcess()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE PROCESS THE THEORY ACTUALLY RUNS");
        sb.AppendLine($"   update rule spatial part   : {UpdateRuleSectors().Spatial:E3}");
        sb.AppendLine($"   coupling census            : {CouplingCensus()}");
        sb.AppendLine($"   actualization phase velocity : {ActualizationPhaseVelocity():E3}  -> phase static: {TheRunningProcessIsPhaseStatic()}");
        sb.AppendLine();
        sb.AppendLine("3. THE UNION OF ALL FIVE FLOWS");
        sb.AppendLine($"   constraint rank            : {UnionConstraintRank()} of {PhaseDimension()}");
        sb.AppendLine($"   deficiency                 : {UnionDeficiency()} phase directions no AT potential can pin");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   the first flowing process : {TheFirstFlowingProcess()}");
        sb.AppendLine($"   a single flow's selection power : {SelectionPower(FlowingCandidates()[0]):F4} of the sector");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
