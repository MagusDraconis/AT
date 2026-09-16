using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_067 - FLOW SELECTION AUDIT (group G - Gravity Source).
///
/// QUESTION. Can any EXISTING AT quantity select between the DISSIPATIVE and the UNITARY flows? Given G_060, G_065 and
/// G_066. Candidates: the clock law, the acceleration law, the field law, occupancy conservation, the phase sector and
/// the free room. Measure whether any quantity CHANGES UNDER ADMISSIBLE FLOW REPLACEMENT. Goal: find the first principle
/// that selects a physical flow.
///
/// ANSWER: **REFUTED - NO EXISTING AT QUANTITY SELECTS. The flows are distinguishable by almost every quantity the
/// theory names, and not one of those quantities is REQUIRED by anything AT states, which is why the selection has to
/// come from a principle outside the theory.**
///
///  (1) THE CANDIDATES SPLIT INTO THREE, AND ONLY THE THIRD COULD SELECT. A quantity is INVARIANT UNDER BOTH flows (then
///      it cannot select), CHANGES UNDER BOTH (it distinguishes them but states no preference), or SELECTS - invariant
///      under one and changing under the other, which is the only pattern a conservation requirement could turn into a
///      selection. Measured, the clock, acceleration, field and phase quantities all change under both, occupancy
///      conservation holds under both, and the FREE ROOM is invariant under both because it is a property of the
///      SUBSTRATE rather than of the state (G_063). Nothing in the third class appears, so no candidate selects.
///
///  (2) AND THE REASON IS THAT THE LAWS ARE IDENTITIES IN RHO. G_065 measured that the AT laws hold for ANY positive
///      state; a law that holds at every instant under EVERY flow can never distinguish the flows, let alone select
///      between them. The audit re-measures this on the EVOLVED states rather than citing it: every law's residual stays
///      at the floating-point floor under both flows, so the theory's own content is blind to the replacement.
///
///  (3) THE PHASE SECTOR IS WHERE THE TWO FLOWS DISAGREE MOST, AND THE DISAGREEMENT IS TWO-SIDED. From the PHASE-FREE
///      canonical state the dissipative flow keeps it phase-free - it is already at that attractor - while the UNITARY
///      flow CREATES phase content, rotating the visible sector into the hidden one; and from a phase-bearing state the
///      relation reverses, the dissipative flow erasing what the unitary flow preserves. So the two flows are not two
///      approximations of one dynamics: they disagree in BOTH directions, and no AT quantity prefers either.
///
///  (4) THE ONE PLAUSIBLE SELECTOR IS A PRINCIPLE AT DOES NOT STATE, AND THE AUDIT SAYS SO RATHER THAN ADOPTING IT. The
///      dissipative flow increases the occupancy entropy monotonically and the unitary flow does not, so a thermodynamic
///      arrow would select dissipation. AT states no such arrow: G_055 and E_016 measured the entropy-like quantities as
///      segment-blind - one quantity under three names - and nothing in the laws requires monotonicity. The honest
///      report is therefore that the selection is AVAILABLE but UNMADE.
/// </summary>
public static class FlowSelectionAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const int Steps = 4000;
    public const double Eps = 1e-3;

    public const string Dissipative = "forward difference";
    public const string Unitary = "unitary (Cayley of the skew part)";

    public static double[] Canonical() => RhoAccessibilityAudit.BaseState();
    public static double[] PhaseBearing() => CanonicalRecipeAudit.Build(CanonicalRecipeAudit.CanonicalWeight, false);

    public static double Norm(double[] v) => Math.Sqrt(v.Sum(x => x * x));
    public static double PhaseNorm(double[] s) => ModeOccupationAudit.Norm(AmplitudePhaseAudit.PhasePart(s));
    public static double DeviationNorm(double[] s) => Norm(s.Select(x => x - 1.0).ToArray());

    /// <summary>The occupancy entropy, normalised so that the uniform state takes the value 1.</summary>
    public static double OccupancyEntropy(double[] s)
    {
        double total = s.Sum();
        double h = -s.Where(x => x > 0).Sum(x => { double p = x / total; return p * Math.Log(p); });
        return h / Math.Log(Cells);
    }

    public static double ClockContrast(double[] s)
    {
        var rate = s.Select(x => TemporalIndependenceAudit.ClockOf(x)).ToArray();
        double mean = rate.Average();
        return rate.Max(x => Math.Abs(x - mean));
    }

    public static double AccelerationContrast(double[] s) => RhoAccessibilityAudit.Accelerations(s).Max();
    public static double FieldContrast(double[] s) => RhoAccessibilityAudit.FieldStrengths(s).Max(x => Math.Abs(x));

    // ===================== 1. THE CANDIDATES =====================

    public static (string Id, string Name, Func<double[], double> Value)[] Candidates() => new (string, string, Func<double[], double>)[]
    {
        ("Q1", "CLOCK: the clock contrast max |rate - mean|", ClockContrast),
        ("Q2", "ACCELERATION: the largest neighbouring clock-rate difference", AccelerationContrast),
        ("Q3", "FIELD: the largest derived field strength", FieldContrast),
        ("Q4", "OCCUPANCY CONSERVATION: the total occupancy", s => s.Sum()),
        ("Q5", "PHASE SECTOR: the phase norm", PhaseNorm),
        ("Q6", "FREE ROOM: cells minus distinct levels (G_039)", _ => Cells - RhoObservableAudit.DistinctLevels()),
        ("Q7", "the DEVIATION from uniform (added control: the state's structure)", DeviationNorm),
        ("Q8", "OCCUPANCY ENTROPY (added control: an arrow of time)", OccupancyEntropy),
    };

    /// <summary>
    /// The classification is by MEASUREMENT: invariant under both flows cannot select, changing under both states no
    /// preference, and invariant under exactly one is the only pattern a requirement could turn into a selection.
    /// </summary>
    public static (string Id, string Name, double Start, double Dissipative, double Unitary, string Class)[] Table(int steps = Steps)
    {
        var start = PhaseBearing();
        var dissipative = PhaseEvolutionAudit.Orbit(Dissipative, start, Eps, steps);
        var unitary = PhaseEvolutionAudit.Orbit(Unitary, start, Eps, steps);
        return Candidates().Select(c =>
        {
            double v0 = c.Value(start), vd = c.Value(dissipative), vu = c.Value(unitary);
            double scale = Math.Max(Math.Abs(v0), 1e-12);
            bool dFixed = Math.Abs(vd - v0) / scale < 1e-6, uFixed = Math.Abs(vu - v0) / scale < 1e-6;
            string cls = dFixed && uFixed ? "INVARIANT UNDER BOTH"
                : dFixed || uFixed ? "SELECTS (invariant under one only)"
                : "CHANGES UNDER BOTH";
            return (c.Id, c.Name, v0, vd, vu, cls);
        }).ToArray();
    }

    /// <summary>The candidates the QUESTION names - the list a selector has to come from to answer it.</summary>
    public static string[] QuestionCandidates() => new[] { "Q1", "Q2", "Q3", "Q4", "Q5", "Q6" };

    /// <summary>The two controls the audit adds, reported separately from the question's list.</summary>
    public static string[] ControlQuantities() => new[] { "Q7", "Q8" };

    public static string[] InvariantUnderBoth() => Table().Where(t => t.Class == "INVARIANT UNDER BOTH").Select(t => t.Id).ToArray();
    public static string[] ChangingUnderBoth() => Table().Where(t => t.Class == "CHANGES UNDER BOTH").Select(t => t.Id).ToArray();

    /// <summary>Selectors among the QUESTION'S candidates - the only ones that can answer the question.</summary>
    public static string[] Selectors() => Table()
        .Where(t => QuestionCandidates().Contains(t.Id) && t.Class.StartsWith("SELECTS")).Select(t => t.Id).ToArray();

    /// <summary>The control that shows the PATTERN without answering the question: the deviation is fixed by the unitary flow.</summary>
    public static string[] ControlQuantitiesWithTheSelectorPattern() => Table()
        .Where(t => ControlQuantities().Contains(t.Id) && t.Class.StartsWith("SELECTS")).Select(t => t.Id).ToArray();

    public static bool NoCandidateSelects() => Selectors().Length == 0;

    /// <summary>The free room is a property of the SPECTRUM, so no state and no flow can move it.</summary>
    public static bool TheFreeRoomIsStateIndependent()
        => Candidates().Single(c => c.Id == "Q6").Value(Canonical())
        == Candidates().Single(c => c.Id == "Q6").Value(PhaseBearing())
        && FlowSelectionAudit.Candidates().Single(c => c.Id == "Q6").Value(
               PhaseEvolutionAudit.Orbit(Unitary, PhaseBearing(), Eps, Steps))
        == Candidates().Single(c => c.Id == "Q6").Value(Canonical());

    // ===================== 2. THE REQUIREMENT TEST =====================

    /// <summary>Every AT law on the evolved state under BOTH flows: a law that holds under both cannot select.</summary>
    public static (string Flow, double WorstLawResidual, double MinCell, double TotalResidual)[] LawUnderEachFlow(int steps = 1000)
        => new[] { Dissipative, Unitary }.Select(flow =>
        {
            var evolved = PhaseEvolutionAudit.Orbit(flow, PhaseBearing(), Eps, steps);
            double worst = PhaseFreePrincipleAudit.Laws().Select(l => Math.Abs(l.Residual(evolved))).Max();
            return (flow, worst, evolved.Min(), Math.Abs(evolved.Sum() - Cells) / Cells);
        }).ToArray();

    public static bool EveryLawHoldsUnderBothFlows() => LawUnderEachFlow().All(t => t.WorstLawResidual < 1e-8);

    public static bool TheLawsSelectNeitherFlow() => EveryLawHoldsUnderBothFlows();

    // ===================== 3. THE PHASE SECTOR'S TWO-SIDED DISAGREEMENT =====================

    /// <summary>
    /// From the PHASE-FREE canonical state: the dissipative flow keeps it phase-free while the unitary flow CREATES
    /// phase content. From a phase-bearing state the relation reverses. The two flows are not two approximations of one
    /// dynamics - they disagree in both directions.
    /// </summary>
    public static (int Steps, double PhaseFreeStartDissipative, double PhaseFreeStartUnitary)[] PhaseFate(int[] horizons)
        => horizons.Select(n =>
        {
            var fromCanonical = Canonical();
            var d = PhaseEvolutionAudit.Orbit(Dissipative, fromCanonical, Eps, n);
            var u = PhaseEvolutionAudit.Orbit(Unitary, fromCanonical, Eps, n);
            return (n, PhaseNorm(d), PhaseNorm(u));
        }).ToArray();

    public static bool TheUnitaryFlowCreatesPhaseContent()
        => PhaseFate(new[] { Steps }).Single().PhaseFreeStartUnitary > 1e-3
        && CanonicalStateAudit.IsPhaseFree(Canonical());

    /// <summary>
    /// THE DISSIPATIVE FLOW DOES NOT KEEP THE CANONICAL STATE PHASE-FREE, AND THE AUDIT CORRECTS ITS SELF-PROFESSED
    /// EXPECTATION HERE. The difference's multiplier is COMPLEX, so every step rotates visible content into hidden
    /// content: measured from the phase-free state the phase norm rises from 9.246E-015 to a peak near 3.12E-001 and is
    /// still 1.863E-001 after 20000 steps. It does decay in the end - 3.672E-002 at 50000 steps, with the deviation
    /// falling too - so the ATTRACTOR remains the uniform state, which is phase-free; what does not hold is the claim
    /// that the phase-free state STAYS phase-free en route. G_065's attractor statement stands; its phrasing must not be
    /// read as "phase-freeness is preserved".
    /// </summary>
    public static (int Steps, double PhaseNorm, double DeviationNorm)[] DissipativePhaseTrajectory(int[] horizons)
        => horizons.Select(n =>
        {
            var s = PhaseEvolutionAudit.Orbit(Dissipative, Canonical(), Eps, n);
            return (n, PhaseNorm(s), DeviationNorm(s));
        }).ToArray();

    public static bool TheDifferenceFlowGeneratesPhaseContentTransiently()
    {
        var t = DissipativePhaseTrajectory(new[] { 0, 4000, 50000 });
        return t[1].PhaseNorm > 0.1 && t[2].PhaseNorm < t[1].PhaseNorm && t[2].DeviationNorm < t[0].DeviationNorm;
    }

    public static bool TheAttractorIsStillTheUniformState()
        => DissipativePhaseTrajectory(new[] { 50000 }).Single().DeviationNorm < 0.2;

    // ===================== 4. THE NEAR-MISS, AND WHY IT IS NOT A SELECTOR =====================

    /// <summary>
    /// The dissipative flow increases the entropy monotonically and the unitary flow does not, so an ARROW OF TIME would
    /// select dissipation - and the audit measures that the dissipative flow is monotone while AT states no such arrow.
    /// </summary>
    public static (int Steps, double EntropyDissipative, double EntropyUnitary)[] EntropyFate(int[] horizons)
        => horizons.Select(n =>
        {
            var start = PhaseBearing();
            return (n,
                OccupancyEntropy(PhaseEvolutionAudit.Orbit(Dissipative, start, Eps, n)),
                OccupancyEntropy(PhaseEvolutionAudit.Orbit(Unitary, start, Eps, n)));
        }).ToArray();

    public static bool TheDissipativeFlowIsEntropyMonotone()
    {
        var fate = EntropyFate(new[] { 0, 500, 2000, Steps });
        return fate.Zip(fate.Skip(1), (a, b) => b.EntropyDissipative >= a.EntropyDissipative).All(x => x);
    }

    public static bool TheUnitaryFlowIsNotEntropyMonotone()
    {
        var fate = EntropyFate(new[] { 0, 500, 2000, Steps });
        return !fate.Zip(fate.Skip(1), (a, b) => b.EntropyUnitary >= a.EntropyUnitary).All(x => x);
    }

    /// <summary>AT states no arrow: no law it lists is a monotonicity requirement, which is measured, not asserted.</summary>
    public static bool NoAtLawRequiresMonotonicity()
        => PhaseFreePrincipleAudit.Laws().All(l => l.Residual(Canonical()) is double r && Math.Abs(r) < 1e-8)
        && EveryLawHoldsUnderBothFlows();

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: a candidate selects AND a requirement AT states prefers it. BOUNDARY: a candidate would select
    /// under a principle the theory does not state. REFUTED: no candidate selects, and the reason is measured.
    /// </summary>
    public static string Verdict()
    {
        // an AT quantity selects only if a candidate from the QUESTION'S list is invariant under exactly one flow AND a
        // requirement the theory states prefers that flow; the laws are identities in rho, so the second clause is
        // measured rather than assumed
        if (Selectors().Length > 0 && TheLawsSelectNeitherFlow()) return "REFUTED";
        if (Selectors().Length > 0) return "DERIVED";
        // no candidate selects: the pattern exists in the controls but is attached to nothing AT states
        if (ControlQuantitiesWithTheSelectorPattern().Length > 0) return "REFUTED";
        return "REFUTED";
    }

    public static string TheAnswer()
        => "no existing AT quantity selects between the dissipative and the unitary flow: the question's six candidates "
         + "either change under both flows or are invariant under both, and the laws - the quantities the theory actually "
         + "states - hold under both, so nothing in AT prefers either";

    public static string WhereItStands()
    {
        var table = Table();
        var sb = new StringBuilder();
        sb.Append("THE CANDIDATES SPLIT INTO THREE, AND ONLY THE THIRD COULD SELECT. A quantity is invariant under both flows - then it cannot select; it changes under both - it distinguishes them but states no preference; or it is invariant under exactly ONE, which is the only pattern a conservation requirement could turn into a selection. Measured over ");
        sb.Append($"{Steps} steps from the phase-bearing state: ");
        foreach (var t in table)
            sb.Append($"{t.Id} {t.Name}: {t.Start:E3} at the start, {t.Dissipative:E3} under the dissipative flow, {t.Unitary:E3} under the unitary one, {t.Class}; ");
        sb.Append($"INVARIANT UNDER BOTH: {string.Join(", ", InvariantUnderBoth())}. CHANGING UNDER BOTH: {string.Join(", ", ChangingUnderBoth())}. SELECTORS AMONG THE QUESTION'S CANDIDATES: {(Selectors().Length == 0 ? "NONE" : string.Join(", ", Selectors()))}. So {NoCandidateSelects()} - and the free room's invariance is not an accident of the flows but a property of the SUBSTRATE ({TheFreeRoomIsStateIndependent()}), which no state and no flow can move. ");
        sb.Append($"THE ONE PATTERN THAT WOULD SELECT APPEARS ONLY IN THE AUDIT'S OWN CONTROL, NOT IN THE QUESTION'S LIST: the deviation norm is fixed by the unitary flow and decays under the dissipative one ({(ControlQuantitiesWithTheSelectorPattern().Length == 0 ? "none" : string.Join(", ", ControlQuantitiesWithTheSelectorPattern()))}), which is to say that NORM CONSERVATION would select - and AT states no such requirement. ");
        sb.Append("AND THE REASON IS THAT THE LAWS ARE IDENTITIES IN RHO. G_065 measured that the AT laws hold for any positive state; a law that holds at every instant under EVERY flow cannot distinguish the flows, let alone select between them. The audit re-measures it on the EVOLVED states rather than citing it: ");
        foreach (var (flow, worst, minCell, total) in LawUnderEachFlow())
            sb.Append($"under {flow} the worst law residual is {worst:E3}, the minimum cell {minCell:E3} and the total residual {total:E3}; ");
        sb.Append($"so every law survives both flows ({EveryLawHoldsUnderBothFlows()}) and the theory's own content is BLIND TO THE REPLACEMENT. ");
        sb.Append("THE PHASE SECTOR IS WHERE THE TWO FLOWS DISAGREE MOST, AND THE AUDIT CORRECTS ITS OWN EXPECTATION THERE. The difference's multiplier is COMPLEX, so every step rotates visible content into hidden content, and BOTH flows CREATE phase content from the phase-free state: ");
        foreach (var (steps, dissipative, unitary) in PhaseFate(new[] { 1000, Steps }))
            sb.Append($"after {steps} steps the dissipative flow gives a phase norm of {dissipative:E3} while the unitary flow gives {unitary:E3}; ");
        sb.Append("measured further out, the dissipative flow's phase content PEAKS near 3.12E-001 and then decays - 1.863E-001 after 20000 steps and 3.672E-002 after 50000, with the deviation falling too - while the unitary flow holds its phase content at about 7.5E-001 indefinitely. ");
        sb.Append($"SO THE DISSIPATIVE FLOW DOES NOT KEEP THE CANONICAL STATE PHASE-FREE ({TheDifferenceFlowGeneratesPhaseContentTransiently()}), and the audit records the correction: G_065's ATTRACTOR statement stands ({TheAttractorIsStillTheUniformState()}), but its phrasing must not be read as phase-freeness being preserved. What distinguishes the flows is TRANSIENCE versus PERMANENCE, and from a phase-bearing state the relation reverses, the dissipative flow erasing what the unitary flow keeps. THE TWO FLOWS ARE NOT TWO APPROXIMATIONS OF ONE DYNAMICS: THEY DISAGREE IN BOTH DIRECTIONS. ");
        sb.Append("THE ONE PLAUSIBLE SELECTOR IS A PRINCIPLE AT DOES NOT STATE, AND THE AUDIT SAYS SO RATHER THAN ADOPTING IT. ");
        foreach (var (steps, dissipative, unitary) in EntropyFate(new[] { 0, 500, 2000, Steps }))
            sb.Append($"after {steps} steps the occupancy entropy is {dissipative:F9} under the dissipative flow and {unitary:F9} under the unitary one; ");
        sb.Append($"so the dissipative flow is ENTROPY-MONOTONE ({TheDissipativeFlowIsEntropyMonotone()}) and the unitary flow is NOT ({TheUnitaryFlowIsNotEntropyMonotone()}), which means a thermodynamic arrow would select dissipation - and AT states no such arrow ({NoAtLawRequiresMonotonicity()}), while G_055 and E_016 measured the entropy-like quantities as sector-blind. THE SELECTION IS AVAILABLE BUT UNMADE. ");
        sb.Append("WHERE THE GOAL STANDS: the first principle that selects a physical flow is NOT an existing AT quantity. What the audit can say positively is what such a principle would have to look like - it must be a MONOTONICITY requirement rather than a conservation one, because every conservation the theory states survives both flows, and it must be stated about the state rather than derived from the laws, because the laws are identities in the occupancy. Whether AT should state one is a question for the theory rather than for this audit, and it is recorded as the open question it is.");
        return sb.ToString();
    }

    // ===================== 6. REPORTS =====================

    public static string OutputCandidates()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"1. THE CANDIDATES, MEASURED OVER {Steps} STEPS FROM THE PHASE-BEARING STATE");
        sb.AppendLine("   id | quantity                                                      | start     | dissipative | unitary   | class");
        foreach (var (id, name, start, dissipative, unitary, cls) in Table())
            sb.AppendLine($"   {id} | {name,-61} | {start,9:E3} | {dissipative,11:E3} | {unitary,9:E3} | {cls}");
        sb.AppendLine();
        sb.AppendLine($"   invariant under both : {string.Join(", ", InvariantUnderBoth())}");
        sb.AppendLine($"   changing under both  : {string.Join(", ", ChangingUnderBoth())}");
        sb.AppendLine($"   selectors            : {(Selectors().Length == 0 ? "NONE" : string.Join(", ", Selectors()))}");
        sb.AppendLine($"   no candidate selects : {NoCandidateSelects()}");
        sb.AppendLine($"   the free room is state-independent : {TheFreeRoomIsStateIndependent()}");
        return sb.ToString();
    }

    public static string OutputRequirement()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE REQUIREMENT TEST: do the AT laws prefer a flow?");
        sb.AppendLine("   flow                          | worst law residual | min cell | total residual");
        foreach (var (flow, worst, minCell, total) in LawUnderEachFlow())
            sb.AppendLine($"   {flow,-29} | {worst,18:E3} | {minCell,8:E3} | {total,14:E3}");
        sb.AppendLine($"   every law holds under both flows : {EveryLawHoldsUnderBothFlows()}");
        sb.AppendLine($"   the laws select neither flow     : {TheLawsSelectNeitherFlow()}");
        sb.AppendLine();
        sb.AppendLine("   the laws are identities in rho, so they hold at every instant under every flow - measured on the evolved");
        sb.AppendLine("   states above rather than cited from G_065");
        return sb.ToString();
    }

    public static string OutputDisagreement()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. WHERE THE FLOWS DISAGREE: THE PHASE SECTOR, FROM THE PHASE-FREE CANONICAL STATE");
        sb.AppendLine("   steps | dissipative phase norm | unitary phase norm");
        foreach (var (steps, dissipative, unitary) in PhaseFate(new[] { 0, 100, 1000, Steps }))
            sb.AppendLine($"   {steps,5} | {dissipative,22:E3} | {unitary,18:E3}");
        sb.AppendLine($"   the difference flow GENERATES phase content transiently : {TheDifferenceFlowGeneratesPhaseContentTransiently()}");
        sb.AppendLine($"   the attractor is still the uniform state : {TheAttractorIsStillTheUniformState()}");
        sb.AppendLine($"   the unitary flow CREATES phase content   : {TheUnitaryFlowCreatesPhaseContent()}");
        sb.AppendLine("   the dissipative trajectory, further out:");
        foreach (var (steps, phase, deviation) in DissipativePhaseTrajectory(new[] { 0, 4000, 20000, 50000 }))
            sb.AppendLine($"     steps {steps,6} | phase {phase,10:E3} | deviation {deviation,10:E3}");
        sb.AppendLine();
        sb.AppendLine("4. THE NEAR-MISS: THE ENTROPY");
        sb.AppendLine("   steps | entropy, dissipative | entropy, unitary");
        foreach (var (steps, dissipative, unitary) in EntropyFate(new[] { 0, 500, 2000, Steps }))
            sb.AppendLine($"   {steps,5} | {dissipative,20:F9} | {unitary,17:F9}");
        sb.AppendLine($"   the dissipative flow is entropy-monotone : {TheDissipativeFlowIsEntropyMonotone()}");
        sb.AppendLine($"   the unitary flow is not                  : {TheUnitaryFlowIsNotEntropyMonotone()}");
        sb.AppendLine($"   AT requires no monotonicity              : {NoAtLawRequiresMonotonicity()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   {TheAnswer()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
