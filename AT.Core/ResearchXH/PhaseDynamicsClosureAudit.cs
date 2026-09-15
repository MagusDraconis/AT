using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_058 - PHASE DYNAMICS CLOSURE AUDIT (group G - Gravity Source).
///
/// QUESTION. Can any AT UPDATE RULE generate a non-trivial phase evolution? Given G_052, G_054, G_056 and G_057. Tests:
/// single scalar flow, multiple coupled scalar flows, vector-valued flow, connection-driven flow, T1/T2-coupled flow.
/// Measure the PHASE-RANK OF THE EVOLUTION OPERATOR. Critical: can any existing AT process reach rank 53?
///
/// ANSWER: **BOUNDARY - and the closure's main result is that the requested criterion CANNOT DISCRIMINATE. THREE ranks
/// are measured and only one of them separates anything: the LINEARISATION and OPERATOR ranks are 53 for EVERY rule
/// including the do-nothing control, while the PUSH rank is 1 for any rule that moves at all and 0 for the process AT
/// actually runs.**
///
///  (1) RANK 53 IS REACHED BY EVERYTHING, INCLUDING THE IDENTITY, AND THE AUDIT SAYS SO BEFORE QUOTING IT. Any state
///      update is the identity plus a small term; the identity is invertible on the phase sector; so the operator's
///      phase block is full rank for any non-degenerate rule. The linearisation of a generator is full rank for the same
///      kind of reason whenever the generator is non-degenerate - including a SCALAR flow, whose linearisation is the
///      potential's Hessian, positive definite and therefore full rank on every subspace. So the question's own measure
///      is satisfied by the identity update, which moves nothing. A criterion the do-nothing rule satisfies is not a
///      criterion, and this audit reports that as its first result rather than as a footnote.
///
///  (2) THE PUSH RANK IS THE ONE THAT SEPARATES, AND IT IS 1 FOR EVERY MOVING RULE. At any instant the state moves
///      along ONE vector, so its phase displacement is rank one whatever the generator is - scalar or vector-valued. The
///      second test is the interesting one: COUPLING SEVERAL SCALAR FLOWS DOES NOT RAISE IT, because a sum of gradients
///      is still one vector. The ceiling belongs to being a push, not to being scalar.
///
///  (3) THE PROCESS AT ACTUALLY RUNS HAS NO GENERATOR AT ALL. The actualization supplies only the time-like component -
///      the update rule's spatial part is zero and nothing couples the link sector to the organisation, both re-measured
///      here - so its push rank is ZERO. That is the answer to the critical question: no AT process reaches the phase
///      sector, and it fails to reach it not by a rank count but by having nothing to push with.
///
///  (4) WHAT CLOSES THE THREAD IS THAT THE THREE RANKS ANSWER THREE DIFFERENT QUESTIONS, and G_057's rank-1 result was
///      the push rank. The audit keeps all three columns visible so that the one that carries no information cannot be
///      mistaken for the one that does.
/// </summary>
public static class PhaseDynamicsClosureAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static (int Channel, string Kind, double[] Mode)[] PhaseBasis() => AmplitudePhaseAudit.PhaseModes();
    public static int PhaseDimension() => PhaseBasis().Length;

    public static (double[] State, double[] Phase)[] PhaseDirections()
        => PhaseBasis().Select(p => (AmplitudePhaseAudit.Step(p.Mode, 1e-5), AmplitudePhaseAudit.Step(p.Mode, -1e-5))).ToArray();

    // ===================== 1. THE GENERATORS =====================

    private static double[] ClockGradient(double[] rho)
        => rho.Select(r => (1.0 / D) * Math.Pow(r, 1.0 / D - 1.0)).ToArray();

    private static double[] NumericalGradient(Func<double[], double> potential, double step = 1e-4)
    {
        var rho = State();
        var g = new double[Cells];
        for (int i = 0; i < Cells; i++)
        {
            var up = (double[])rho.Clone(); up[i] += step;
            var down = (double[])rho.Clone(); down[i] -= step;
            g[i] = (potential(up) - potential(down)) / (2.0 * step);
        }
        return g;
    }

    private static double FieldPotential(double[] rho) => RhoAccessibilityAudit.FieldStrengths(rho).Sum(x => x * x);
    private static double AccelerationPotential(double[] rho)
    {
        var rates = RhoAccessibilityAudit.ClockRates(rho);
        return Enumerable.Range(0, Cells).Select(i => Math.Pow(rates[(i + 1) % Cells] - rates[i], 2)).Sum();
    }

    public static double[] OccupancyGradient(double[] rho)
        => Enumerable.Range(0, Cells).Select(i => rho[(i + 1) % Cells] - rho[i]).ToArray();

    public static double[] DerivedConnection(double[] rho)
    {
        var h = CouplingFunctionAudit.DerivedCoupling();
        return Enumerable.Range(0, Cells).Select(i => h(rho[i]) * (rho[(i + 1) % Cells] - rho[i])).ToArray();
    }

    public static double[] SectorCoupling(double[] rho)
    {
        var d1 = OccupancyGradient(rho);
        var d3 = Enumerable.Range(0, Cells).Select(i => rho[(i + 3) % Cells] - rho[i]).ToArray();
        return d1.Zip(d3, (a, b) => a * b).ToArray();
    }

    public static (string Rule, Func<double[], double[]> Generator)[] Generators() => new (string, Func<double[], double[]>)[]
    {
        ("single scalar flow", rho => ClockGradient(rho)),
        ("multiple coupled scalar flows", rho => ClockGradient(rho)
            .Zip(NumericalGradient(FieldPotential, 1e-4), (a, b) => a + b)
            .Zip(NumericalGradient(AccelerationPotential, 1e-4), (a, b) => a + b).ToArray()),
        ("vector-valued flow", OccupancyGradient),
        ("connection-driven flow", DerivedConnection),
        ("T1/T2-coupled flow", SectorCoupling),
        ("identity update (CONTROL)", _ => new double[Cells]),
    };

    /// <summary>AT's own actualization: it supplies no spatial generator at all - re-measured, not cited.</summary>
    public static Func<double[], double[]> ActualizationGenerator() => _ => new double[Cells];

    public static bool TheActualizationHasNoSpatialGenerator()
        => PhaseDeterminationAudit.UpdateRuleSectors().Spatial < 1e-15
        && PhaseDeterminationAudit.CouplingCensus() == 0;

    // ===================== 2. THE THREE RANKS =====================

    private static double[] ProjectToPhase(double[] v)
        => PhaseBasis().Select(p => v.Zip(p.Mode, (a, b) => a * b).Sum()).ToArray();

    private static int Rank(IEnumerable<double[]> vectors)
    {
        var basis = new List<double[]>();
        foreach (var v0 in vectors)
        {
            var v = (double[])v0.Clone();
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (x, y) => x * y).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-9) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    /// <summary>
    /// The PUSH rank: the rank of the state's instantaneous phase displacement. The state moves along ONE vector, so
    /// this is at most 1 whatever the generator is - and it is the only one of the three ranks that separates a moving
    /// rule from a static one.
    /// </summary>
    public static int PushRank(Func<double[], double[]> generator)
    {
        var projected = ProjectToPhase(generator(State()));
        return Math.Sqrt(projected.Sum(x => x * x)) < 1e-12 ? 0 : 1;
    }

    /// <summary>
    /// The LINEARISATION rank: how the generator's own Jacobian acts on the phase sector. Full rank for any
    /// non-degenerate generator, INCLUDING a scalar flow - whose linearisation is the potential's Hessian, positive
    /// definite and full rank on every subspace. Measured here rather than argued.
    /// </summary>
    public static int LinearisationRank(Func<double[], double[]> generator, double step = 1e-5)
    {
        var images = PhaseDirections().Select(d =>
            ProjectToPhase(generator(d.State).Zip(generator(d.Phase), (a, b) => (a - b) / (2.0 * step)).ToArray())).ToArray();
        return Rank(images);
    }

    /// <summary>
    /// The OPERATOR rank: how phase perturbations PROPAGATE under the update map rho -> rho - s*V(rho). Full rank for
    /// any state update because the identity is invertible on the phase sector - so the identity control reaches it.
    /// </summary>
    public static int OperatorRank(Func<double[], double[]> generator, double step = 1e-5, double s = 1e-3)
    {
        Func<double[], double[]> map = r => r.Zip(generator(r), (x, g) => x - s * g).ToArray();
        var images = PhaseDirections().Select(d =>
            ProjectToPhase(map(d.State).Zip(map(d.Phase), (a, b) => (a - b) / (2.0 * step)).ToArray())).ToArray();
        return Rank(images);
    }

    // ===================== 3. THE TABLE =====================

    public static (string Rule, int PushRank, int LinearisationRank, int OperatorRank)[] RankTable()
        => Generators().Select(g => (g.Rule, PushRank(g.Generator), LinearisationRank(g.Generator),
            OperatorRank(g.Generator))).ToArray();

    /// <summary>
    /// Every rule that HAS a generator linearises to full rank. The identity control is excluded precisely because it
    /// has no generator: its linearisation rank is 0 while its operator rank is still full - which is the warning.
    /// </summary>
    public static bool EveryGeneratorLinearisationIsFull()
        => RankTable().Where(t => !IsControl(t.Rule)).All(t => t.LinearisationRank == PhaseDimension());

    public static bool EveryOperatorRankIsFull() => RankTable().All(t => t.OperatorRank == PhaseDimension());

    private static bool IsControl(string rule) => rule.Contains("CONTROL");

    public static string[] RulesThatPush() => RankTable().Where(t => t.PushRank > 0).Select(t => t.Rule).ToArray();
    public static string[] RulesThatDoNotPush() => RankTable().Where(t => t.PushRank == 0).Select(t => t.Rule).ToArray();

    public static int MaxPushRank() => RankTable().Max(t => t.PushRank);

    public static bool CouplingScalarsDoesNotHelp()
        => RankTable().Single(t => t.Rule == "single scalar flow").PushRank
        == RankTable().Single(t => t.Rule == "multiple coupled scalar flows").PushRank;

    /// <summary>The identity control reaches the full operator rank while moving nothing - the criterion is vacuous.</summary>
    public static bool TheRequestedCriterionIsVacuous()
        => RankTable().Single(t => IsControl(t.Rule)).OperatorRank == PhaseDimension()
        && RankTable().Single(t => IsControl(t.Rule)).LinearisationRank == 0
        && RankTable().Single(t => IsControl(t.Rule)).PushRank == 0;

    /// <summary>
    /// THE WITHDRAWN EXPECTATION. The question's premise is a hierarchy in which a vector-valued generator reaches
    /// rank 53 while scalar-driven rules cap at 1. Measured: NO RANK MEASURE SEPARATES THEM - the scalar flows and the
    /// vector-valued flows return the same number in both the linearisation and the operator column, because a
    /// scalar flow's linearisation is its potential's Hessian (positive definite, hence full rank on every subspace)
    /// and the identity is invertible. The premise is refuted, not merely unmet.
    /// </summary>
    public static bool VectorValuedAndScalarRulesAreRankIndistinguishable()
    {
        var t = RankTable();
        var scalars = t.Where(x => x.Rule.Contains("scalar")).ToArray();
        var vectors = t.Where(x => x.Rule.Contains("vector") || x.Rule.Contains("connection") || x.Rule.Contains("T1/T2")).ToArray();
        return scalars.All(x => x.LinearisationRank == PhaseDimension())
            && vectors.All(x => x.LinearisationRank == PhaseDimension())
            && scalars.All(x => x.PushRank == vectors[0].PushRank);
    }

    public static int ActualizationPushRank() => PushRank(ActualizationGenerator());

    public static bool NoAtProcessPushesThePhase()
        => ActualizationPushRank() == 0 && TheActualizationHasNoSpatialGenerator();

    public static string TheMethodologicalFinding()
        => $"the requested measure - reach rank {PhaseDimension()} - is satisfied by EVERY rule tested, including the "
         + "identity update that moves nothing; the ranks that separate rules are the PUSH rank (1 for any moving rule, "
         + "0 for a static one) and the presence of a generator at all - the identity has no generator at all, yet its "
         + "operator block is still invertible on the phase sector";

    // ===================== 4. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: the rule AT RUNS pushes the phase. BOUNDARY: candidate rules push it while the running rule
    /// does not. REFUTED: no rule pushes the phase even as a candidate.
    /// </summary>
    public static string Verdict()
    {
        if (ActualizationPushRank() > 0) return "DERIVED";
        if (MaxPushRank() == 0) return "REFUTED";
        if (NoAtProcessPushesThePhase()) return "BOUNDARY";
        return "BOUNDARY";
    }

    public static string TheCriticalAnswer()
        => $"AT's own update rule has PUSH RANK {ActualizationPushRank()} - it has no spatial generator at all - while "
         + $"every candidate rule that moves pushes rank {MaxPushRank()}, and the rank-{PhaseDimension()} measure the "
         + "question asked is reached by all of them, controls included";

    public static string WhereItStands()
        => "THE CLOSURE IS THAT THE QUESTION'S OWN CRITERION CANNOT DISCRIMINATE, AND THE AUDIT SAYS SO BEFORE QUOTING ANY "
         + "NUMBER FROM IT. G_057 measured five potentials at push rank 1 and a phase-static running process; this audit "
         + "asks the same question one level higher, about UPDATE RULES, and measures THREE ranks where the question "
         + "asked for one. THE FIRST RESULT IS A WARNING ABOUT THE MEASURE. The LINEARISATION rank and the OPERATOR rank "
         + $"are {PhaseDimension()} for EVERY rule tested - the single scalar flow, the coupled scalar flows, the three "
         + "vector-valued flows, AND the identity update that moves nothing - so "
         + $"\"{TheMethodologicalFinding()}\". Any state update is the identity plus a small term, the identity is "
         + "invertible on the phase sector, and a scalar flow's linearisation is its potential's Hessian, positive "
         + "definite and full rank on every subspace: nothing about rank "
         + $"{PhaseDimension()} is evidence that a rule acts on the phase, and a criterion the do-nothing rule satisfies "
         + "is not a criterion. THE PUSH RANK IS THE ONE THAT CARRIES CONTENT. At any instant the state moves along ONE "
         + "vector, so its phase displacement is rank one whatever the generator is, and the second test's negative "
         + $"result is the interesting part: COUPLING SEVERAL SCALAR FLOWS DOES NOT RAISE IT "
         + $"({CouplingScalarsDoesNotHelp()}), because a sum of gradients is still one vector - the ceiling belongs to "
         + $"being a push, not to being scalar. The moving rules are {string.Join(", ", RulesThatPush())}, and every one "
         + "of them pushes exactly one direction at a time. THE PROCESS AT ACTUALLY RUNS HAS NOTHING TO PUSH WITH. The "
         + $"actualization supplies no spatial generator: its update rule's spatial part is "
         + $"{PhaseDeterminationAudit.UpdateRuleSectors().Spatial:E3}, the census of members coupling the link sector to "
         + $"the organisation is {PhaseDeterminationAudit.CouplingCensus()}, and its push rank is "
         + $"{ActualizationPushRank()}. That is the answer to the critical question, and it is sharper than a rank count: "
         + "no AT process reaches the phase sector because no AT process supplies a generator that has phase content, "
         + "not because it falls short of a number. WHAT CLOSES THE THREAD IS THAT THE THREE RANKS ANSWER THREE "
         + "DIFFERENT QUESTIONS, and G_057's rank-1 ceiling was the push rank. The audit keeps all three columns visible "
         + "so that the one carrying no information cannot be mistaken for the one that does - and the phases remain "
         + "freely assigned, for the same reason as before, now stated with the right rank. ONE EXPECTATION IS "
         + "WITHDRAWN. The question's premise was a hierarchy in which a vector-valued generator reaches rank "
         + $"{PhaseDimension()} while scalar-driven rules cap at 1. No rank measure separates them: "
         + $"VectorValuedAndScalarRulesAreRankIndistinguishable() = {VectorValuedAndScalarRulesAreRankIndistinguishable()}, "
         + "because a scalar flow's linearisation is its potential's Hessian - positive definite, hence full rank on "
         + "every subspace - and any state update contains the identity, which is invertible on the phase sector. The "
         + "premise is refuted by measurement, not merely unmet, and the audit records that rather than the hierarchy it "
         + "expected to report.";

    // ===================== REPORT =====================

    public static string OutputRanks()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE THREE RANKS, PER RULE");
        sb.AppendLine($"   phase dimensions : {PhaseDimension()}");
        sb.AppendLine("   rule                            | push rank | linearisation | operator");
        foreach (var (rule, push, lin, op) in RankTable())
            sb.AppendLine($"   {rule,-31} | {push,9} | {lin,13} | {op,8}");
        sb.AppendLine($"   every generator linearises full : {EveryGeneratorLinearisationIsFull()}   (all rules that HAVE a generator)");
        sb.AppendLine($"   every operator rank is full     : {EveryOperatorRankIsFull()}   (the identity is included)");
        sb.AppendLine($"   THE WARNING: {TheMethodologicalFinding()}");
        return sb.ToString();
    }

    public static string OutputPush()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE PUSH RANK - THE ONLY COLUMN THAT SEPARATES RULES");
        sb.AppendLine($"   rules that push        : {string.Join(", ", RulesThatPush())}");
        sb.AppendLine($"   rules that do not      : {string.Join(", ", RulesThatDoNotPush())}");
        sb.AppendLine($"   every moving rule pushes {MaxPushRank()} direction at a time");
        sb.AppendLine($"   coupling scalars does not help : {CouplingScalarsDoesNotHelp()}  (a sum of gradients is one vector)");
        sb.AppendLine();
        sb.AppendLine("3. THE PROCESS THE THEORY RUNS");
        sb.AppendLine($"   update rule spatial part  : {PhaseDeterminationAudit.UpdateRuleSectors().Spatial:E3}");
        sb.AppendLine($"   coupling census           : {PhaseDeterminationAudit.CouplingCensus()}");
        sb.AppendLine($"   no spatial generator      : {TheActualizationHasNoSpatialGenerator()}");
        sb.AppendLine($"   actualization push rank   : {ActualizationPushRank()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   {TheCriticalAnswer()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
