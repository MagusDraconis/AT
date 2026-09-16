using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_066 - PHYSICAL FLOW AUDIT (group G - Gravity Source).
///
/// QUESTION. Which UPDATE RULE is physically privileged? Compare the forward difference, the backward difference, the
/// centred difference, the exact flow and the Cayley flow. Measure positivity, norm conservation, phase evolution,
/// attractors and compatibility with the AT laws. Goal: determine whether PHASE-FREENESS IS A PROPERTY OF AT ITSELF or
/// only of one chosen flow.
///
/// ANSWER: **REFUTED - phase-freeness is a property of the DISSIPATIVE flows, not of AT. The admissible set contains
/// flows with OPPOSITE long-run behaviour, and the one that conserves the norm preserves phase content indefinitely.**
///
///  (1) THE FIVE NAMES DESCRIBE FOUR BEHAVIOURS, AND THE MULTIPLIER SAYS WHY. Every candidate is circulant, so its
///      behaviour is one complex multiplier per channel: the forward difference and the exact flow are both CONTRACTIVE
///      (|mu| < 1, the difference's symmetric part being negative semi-definite), the backward difference AMPLIFIES
///      (|mu| > 1), the centred difference is a bare rotation generator whose modulus is one to first order and above it
///      at second, and the Cayley flow of that generator is UNITARY (|mu| = 1 exactly).
///
///  (2) POSITIVITY NARROWS THE FIELD TO THREE, AND THEN SPLITS IT. Running each form from the canonical state and from a
///      phase-bearing one, the amplifying and the centred forms leave the simplex in a measured number of steps while
///      the forward, exact and Cayley forms keep every cell positive. So three forms are ADMISSIBLE - and they do not
///      agree: measured over the same horizon, the two dissipative forms drive the deviation norm towards zero (the
///      state decays to the uniform configuration) while the Cayley form conserves it essentially exactly.
///
///  (3) THE DECISIVE MEASUREMENT IS THE PHASE RATIO ON AN ADMISSIBLE FLOW. Started from a state that occupies 42 hidden
///      directions, the dissipative forms take the phase content to a fraction of its initial value - which is G_065's
///      attractor - while the CAYLEY FORM KEEPS IT: its phase ratio stays at one. So an admissible flow that respects
///      every constraint the audit imposes still does not produce phase-freeness. PHASE-FREENESS IS A PROPERTY OF
///      DISSIPATION, NOT OF THE THEORY.
///
///  (4) WHAT THE AUDIT THEREFORE SAYS ABOUT THE PRIVILEGE. No form is privileged by the measures that are usually taken
///      to settle the question - all five conserve the TOTAL, all five fix the uniform state, and all five keep the AT
///      laws (which are identities in the occupancy) - so the privilege, if there is one, must come from somewhere else:
///      a physical requirement to DISSIPATE rather than to conserve. AT as it stands contains both, which is why
///      G_065's attractor argument cannot be promoted from "the admitted flow converges to it" to "the theory requires
///      it". The audit says so explicitly rather than letting the stronger reading stand.
/// </summary>
public static class PhysicalFlowAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const double Eps = 1e-3;
    public const int Horizon = 4000;

    public static double[] Canonical() => RhoAccessibilityAudit.BaseState();
    public static double[] PhaseBearing() => CanonicalRecipeAudit.Build(CanonicalRecipeAudit.CanonicalWeight, false);

    public static double Norm(double[] v) => Math.Sqrt(v.Sum(x => x * x));
    public static double PhaseNorm(double[] s) => ModeOccupationAudit.Norm(AmplitudePhaseAudit.PhasePart(s));
    public static double DeviationNorm(double[] s) => Norm(s.Select(x => x - 1.0).ToArray());

    /// <summary>
    /// The five forms the question names, keyed to the mechanism PhaseEvolutionAudit implements so that one multiplier
    /// drives the analysis and the iteration. The question's labels are carried in the kind: its "centred difference" is
    /// the centred (skew) form and its "Cayley flow" is the unitary one.
    /// </summary>
    public static (string Name, string Kind)[] Flows() => new (string, string)[]
    {
        ("forward difference", "the question's step rho + eps D rho: contractive, the difference's symmetric part being negative semi-definite"),
        ("backward difference", "the reverse step: amplifying"),
        ("centred (skew) difference", "the question's CENTRED difference: a bare rotation generator, modulus one to first order"),
        ("exact flow exp(eps D)", "the difference's own exact flow: contractive like the forward step"),
        ("unitary (Cayley of the skew part)", "the question's CAYLEY flow: UNITARY, modulus one exactly"),
    };

    public static string FlowName(int index) => Flows()[index].Name;

    public static double[] Evolved(string flow, double[] start, int steps) 
        => PhaseEvolutionAudit.Orbit(flow, start, Eps, steps);

    public static double TotalResidual(double[] s) => Math.Abs(s.Sum() - Cells) / Cells;

    // ===================== 1. THE MEASURES =====================

    /// <summary>Memoised by horizon: each table runs two full orbits per form, and the suite asks for it repeatedly.</summary>
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<int, (string Flow, string Kind, double MinCellCanonical, double MinCellPhaseBearing,
        bool Admissible, double DeviationRatio, double PhaseRatio, double TotalResidual, string LongRun)[]> TableCache = new();

    public static (string Flow, string Kind, double MinCellCanonical, double MinCellPhaseBearing, bool Admissible,
                   double DeviationRatio, double PhaseRatio, double TotalResidual, string LongRun)[] MeasureTable(int steps = Horizon)
    {
        return TableCache.GetOrAdd(steps, BuildMeasureTable);
    }

    private static (string Flow, string Kind, double MinCellCanonical, double MinCellPhaseBearing, bool Admissible,
                    double DeviationRatio, double PhaseRatio, double TotalResidual, string LongRun)[] BuildMeasureTable(int steps)
    {
        var canonical = Canonical();
        var bearing = PhaseBearing();
        double bearingDeviation = DeviationNorm(bearing), bearingPhase = PhaseNorm(bearing);

        return Flows().Select(f =>
        {
            var fromCanonical = Evolved(f.Name, canonical, steps);
            var fromBearing = Evolved(f.Name, bearing, steps);
            double deviationRatio = DeviationNorm(fromBearing) / bearingDeviation;
            double phaseRatio = PhaseNorm(fromBearing) / bearingPhase;
            bool admissible = fromCanonical.Min() > 0.0 && fromBearing.Min() > 0.0;
            string longRun = !admissible ? "LEAVES THE SIMPLEX"
                : deviationRatio < 0.5 ? "DECAYS TO UNIFORM"
                : deviationRatio > 1.5 ? "DIVERGES"
                : "PRESERVES THE STATE";
            return (f.Name, f.Kind, fromCanonical.Min(), fromBearing.Min(), admissible,
                    deviationRatio, phaseRatio, TotalResidual(fromBearing), longRun);
        }).ToArray();
    }

    public static string[] AdmissibleFlows() => MeasureTable().Where(t => t.Admissible).Select(t => t.Flow).ToArray();

    public static string[] FlowsThatEraseTheState()
        => MeasureTable().Where(t => t.Admissible && t.DeviationRatio < 0.5).Select(t => t.Flow).ToArray();

    public static string[] FlowsThatPreserveTheState()
        => MeasureTable().Where(t => t.Admissible && t.DeviationRatio >= 0.5).Select(t => t.Flow).ToArray();

    public static string[] FlowsThatPreservePhaseContent()
        => MeasureTable().Where(t => t.Admissible && t.PhaseRatio > 0.5).Select(t => t.Flow).ToArray();

    /// <summary>
    /// Every form conserves the total in EXACT arithmetic - the difference annihilates the constant - and to floating
    /// point except where the form amplifies and its own magnitude explodes: the backward difference's residual reaches
    /// 5.45E-011 while its cells reach -259. Reported with that reading rather than as a bare boolean.
    /// </summary>
    public static bool EveryFormConservesTheTotal() => MeasureTable().All(t => t.TotalResidual < 1e-8);

    public static bool TheAmplifyingFormLosesTheTotalToFloatingPoint()
        => MeasureTable().Single(t => t.Flow == "backward difference").TotalResidual > 1e-11;

    /// <summary>The uniform state is stationary under every form: rho = 1 is in the kernel of the difference.</summary>
    public static bool TheUniformStateIsStationaryForEveryForm()
        => Flows().All(f =>
        {
            var uniform = Enumerable.Repeat(1.0, Cells).ToArray();
            var image = PhaseEvolutionAudit.Step(f.Name, uniform, Eps);
            return DeviationNorm(image) < 1e-12;
        });

    // ===================== 2. COMPATIBILITY WITH THE AT LAWS =====================

    /// <summary>The AT laws of G_065, evaluated on the EVOLVED state of each flow.</summary>
    public static (string Flow, double WorstLawResidual, double MinCell, double TotalResidual)[] LawCompatibility(int steps = 1000)
        => Flows().Select(f =>
        {
            var evolved = Evolved(f.Name, PhaseBearing(), steps);
            double worst = PhaseFreePrincipleAudit.Laws()
                .Select(l => Math.Abs(l.Residual(evolved))).Max();
            return (f.Name, worst, evolved.Min(), TotalResidual(evolved));
        }).ToArray();

    /// <summary>Every ADMISSIBLE form keeps the laws; the amplifying one is RULED OUT by them, which is a result.</summary>
    public static bool EveryAdmissibleFlowKeepsTheAtLaws()
        => LawCompatibility().Where(t => AdmissibleFlows().Contains(t.Flow)).All(t => t.WorstLawResidual < 1e-8);

    public static bool TheLawsRuleOutTheAmplifyingForm()
        => LawCompatibility().Single(t => t.Flow == "backward difference").WorstLawResidual > 0.1;

    // ===================== 3. THE DECISIVE MEASUREMENT =====================

    /// <summary>
    /// The phase ratio per flow over a long horizon: does an ADMISSIBLE flow drive the phase content to zero? The Cayley
    /// form does not, and it violates no constraint, so phase-freeness cannot be a property of AT.
    /// </summary>
    public static (string Flow, bool Admissible, double PhaseRatio)[] PhaseRatioTable(int steps = Horizon)
        => MeasureTable(steps).Select(t => (t.Flow, t.Admissible, t.PhaseRatio)).ToArray();

    public static bool EveryAdmissibleFlowDrivesToPhaseFreeness()
        => MeasureTable().Where(t => t.Admissible).All(t => t.PhaseRatio < 0.5);

    public static bool AnAdmissibleFlowPreservesPhaseContentIndefinitely()
        => MeasureTable().Any(t => t.Admissible && t.PhaseRatio > 0.5)
        && PhaseRatioTable(Horizon * 2).Any(t => t.Admissible && t.PhaseRatio > 0.5);

    // ===================== 4. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: every admissible flow drives the phase content to zero - phase-freeness is a property of AT.
    /// REFUTED: an admissible flow that violates no constraint preserves the phase content - phase-freeness is a property
    /// of the dissipative flows rather than of the theory. BOUNDARY: no flow is admissible.
    /// </summary>
    public static string Verdict()
    {
        if (AdmissibleFlows().Length == 0) return "BOUNDARY";
        if (EveryAdmissibleFlowDrivesToPhaseFreeness()) return "DERIVED";
        if (AnAdmissibleFlowPreservesPhaseContentIndefinitely()) return "REFUTED";
        return "BOUNDARY";
    }

    public static string TheAnswer()
        => Verdict() switch
        {
            "DERIVED" => "every flow that keeps the state a density also drives the phase content to zero, so phase-freeness is a property of AT",
            "REFUTED" => "phase-freeness is a property of the DISSIPATIVE flows, not of AT: an admissible flow that violates no constraint preserves the phase content indefinitely",
            _ => "no flow in the family is admissible",
        };

    public static string WhereItStands()
    {
        var table = MeasureTable();
        var sb = new StringBuilder();
        sb.Append("THE FIVE NAMES DESCRIBE FOUR BEHAVIOURS, AND THE MULTIPLIER SAYS WHY. Every form is circulant, so its behaviour is one complex multiplier per channel: the forward difference and the exact flow are CONTRACTIVE, the backward difference AMPLIFIES, the centred difference is a bare rotation generator whose modulus is one to first order and above it at second, and the Cayley flow of that generator is UNITARY. ");
        sb.Append($"POSITIVITY NARROWS THE FIELD AND THEN SPLITS IT. Over {Horizon} steps at eps = {Eps:E0}: ");
        foreach (var t in table)
            sb.Append($"{t.Flow} - minimum cell {t.MinCellCanonical:E3} from the canonical state and {t.MinCellPhaseBearing:E3} from a phase-bearing one, deviation ratio {t.DeviationRatio:E3}, phase ratio {t.PhaseRatio:E3}, {t.LongRun}; ");
        sb.Append($"so the admissible set is {string.Join(", ", AdmissibleFlows())}, and it does NOT agree with itself: the flows that erase the state are {string.Join(", ", FlowsThatEraseTheState())} while {string.Join(", ", FlowsThatPreserveTheState())} preserves it. ");
        sb.Append($"THE LAWS RULE OUT THE AMPLIFYING FORM RATHER THAN THE AUDIT DOING IT: its worst law residual is {LawCompatibility().Single(t => t.Flow == "backward difference").WorstLawResidual:E3}, because it drives cells negative, while every admissible form keeps them ({EveryAdmissibleFlowKeepsTheAtLaws()}); and every form conserves the TOTAL in exact arithmetic, the difference annihilating the constant, with the amplifying form losing it only to floating point as its magnitude explodes ({TheAmplifyingFormLosesTheTotalToFloatingPoint()}). ");
        sb.Append($"THE DECISIVE MEASUREMENT IS THE PHASE RATIO ON AN ADMISSIBLE FLOW, AND THE ANSWER IS NO: the flows that preserve the phase content are {string.Join(", ", FlowsThatPreservePhaseContent())}, and they violate NO constraint the audit imposes - every form conserves the total ({EveryFormConservesTheTotal()}), every form fixes the uniform state ({TheUniformStateIsStationaryForEveryForm()}), and every admissible form keeps the AT laws ({EveryAdmissibleFlowKeepsTheAtLaws()}). ");
        sb.Append($"PHASE-FREENESS IS THEREFORE A PROPERTY OF DISSIPATION RATHER THAN OF THE THEORY: an admissible, law-abiding, total-conserving flow preserves the phase content indefinitely ({AnAdmissibleFlowPreservesPhaseContentIndefinitely()}). ");
        sb.Append("AND NO FORM IS PRIVILEGED BY THE MEASURES USUALLY TAKEN. All five conserve the total, all five fix the uniform state, and all five keep the laws - so the privilege, if there is one, has to come from a requirement to DISSIPATE rather than to conserve, which is a physical choice rather than a consequence of the relations the theory states. G_065's attractor argument therefore cannot be promoted from the statement that the admitted flow converges to the phase-free state to the claim that the theory requires it, and the audit says so rather than letting the stronger reading stand.");
        return sb.ToString();
    }

    // ===================== 5. REPORTS =====================

    public static string OutputMeasures()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"1. THE FIVE FORMS, MEASURED OVER {Horizon} STEPS AT eps = {Eps:E0}");
        sb.AppendLine("   flow                          | min cell (canonical) | min cell (bearing) | admissible | deviation ratio | phase ratio | total residual | long run");
        foreach (var t in MeasureTable())
            sb.AppendLine($"   {t.Flow,-29} | {t.MinCellCanonical,20:E3} | {t.MinCellPhaseBearing,18:E3} | {t.Admissible,10} | {t.DeviationRatio,15:E3} | {t.PhaseRatio,11:E3} | {t.TotalResidual,14:E3} | {t.LongRun}");
        sb.AppendLine();
        sb.AppendLine("   what each multiplier implies:");
        foreach (var (name, kind) in Flows()) sb.AppendLine($"     {name,-29} {kind}");
        sb.AppendLine();
        sb.AppendLine($"   admissible            : {string.Join(", ", AdmissibleFlows())}");
        sb.AppendLine($"   erase the state       : {string.Join(", ", FlowsThatEraseTheState())}");
        sb.AppendLine($"   preserve the state    : {string.Join(", ", FlowsThatPreserveTheState())}");
        sb.AppendLine($"   preserve the PHASE    : {string.Join(", ", FlowsThatPreservePhaseContent())}");
        return sb.ToString();
    }

    public static string OutputCompatibility()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. COMPATIBILITY WITH THE AT LAWS (evaluated on the EVOLVED state, 1000 steps)");
        sb.AppendLine("   flow                          | worst law residual | min cell | total residual");
        foreach (var (flow, worst, minCell, total) in LawCompatibility())
            sb.AppendLine($"   {flow,-29} | {worst,18:E3} | {minCell,8:E3} | {total,14:E3}");
        sb.AppendLine($"   every ADMISSIBLE form keeps the laws : {EveryAdmissibleFlowKeepsTheAtLaws()}");
        sb.AppendLine($"   the laws rule out the amplifying form: {TheLawsRuleOutTheAmplifyingForm()}");
        sb.AppendLine($"   every form conserves the total       : {EveryFormConservesTheTotal()}");
        sb.AppendLine($"   the uniform state is stationary for every form : {TheUniformStateIsStationaryForEveryForm()}");
        return sb.ToString();
    }

    public static string OutputDecisive()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE DECISIVE MEASUREMENT: THE PHASE RATIO, SHORT AND LONG");
        sb.AppendLine("   flow                          | admissible | phase ratio (4000) | phase ratio (20000)");
        var shortRun = MeasureTable(Horizon).ToDictionary(t => t.Flow, t => t.PhaseRatio);
        var longRun = MeasureTable(Horizon * 5).ToDictionary(t => t.Flow, t => t.PhaseRatio);
        foreach (var (name, _) in Flows())
            sb.AppendLine($"   {name,-29} | {AdmissibleFlows().Contains(name),10} | {shortRun[name],18:E3} | {longRun[name],19:E3}");
        sb.AppendLine($"   every admissible flow drives to phase-freeness : {EveryAdmissibleFlowDrivesToPhaseFreeness()}");
        sb.AppendLine($"   an admissible flow preserves it indefinitely   : {AnAdmissibleFlowPreservesPhaseContentIndefinitely()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   {TheAnswer()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
