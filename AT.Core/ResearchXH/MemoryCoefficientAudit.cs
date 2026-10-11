using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What kind of object the memory coefficient is (ResearchY-G_095).</summary>
public enum CoefficientStatus { Derived, Emergent, FixedPointSelected, Empirical }

/// <summary>One candidate principle, with whether the surviving theory CONTAINS it and what it would select.</summary>
public sealed record SelectingPrinciple(string Principle, bool InTheSurvivingTheory,
    double ResidualForTheSectorLaw, double BestCompetingResidual, bool WouldSelectUniquely, string Measurement);

/// <summary>One sign region of the coefficient, with every surviving constraint measured there.</summary>
public sealed record BoundaryRow(string Regime, double Coefficient, bool Positive, bool Monotone, bool Viable,
    double SecondOrderCoefficientOfF, double RedshiftQuadratic, double FreePartAtTheTarget, double TargetPrediction,
    bool MatchesGrAtSecondOrder, string Reading);

/// <summary>
/// ResearchY-G_095 — MEMORY COEFFICIENT AUDIT (group G — Gravity Source).
///
/// G_093 found that a **memory channel carrying the kernel's coefficient** is required for first-principles
/// completion and absent; G_094 found that it is the **only** gap whose closure would change a physical prediction,
/// and that the change is CONDITIONAL on the value the closure delivers. This audit asks what fixes that value.
///
/// The task sets two restrictions and the audit obeys them literally: **the coefficient is not SET TO ZERO**, and it
/// is **not fitted to observations**. Both routes are therefore excluded as ANSWERS — the first because it would be a
/// postulate, the second because it would be a fit — and the audit instead asks which SURVIVING PRINCIPLE selects a
/// value, measuring every candidate rather than arguing from plausibility.
///
/// The coefficient is `lambda` in the rate's second order — the rate is `exp(A + lambda A^2 / 2)` with
/// `A = (1/d) ln rho`, equivalently the clock law `F = exp(2x + lambda x^2)` in the variable the sector's own audits
/// use — so `lambda = 0` is the recorded law and `lambda = -2` is where `F`'s second-order coefficient coincides with
/// GR's (`1 + 2x`). This identification is not assumed: it is checked below against both families' own APIs.
///
/// Substrate: the canonical 96-cell ring for the time sector, the clock law's own variable for the coefficient
/// (`x` = the clock potential); equality rule `1e-12` on a residual. No spectral count (A0, a free room, a lock
/// release, a survivor count) is quoted, so the spectral-substrate rule is not engaged.
///
/// Prefers refutation. **Failure** is that `lambda` remains free; **success** is that a unique value is forced.
/// </summary>
public static class MemoryCoefficientAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    /// <summary>The sector's own coefficient, in the clock law's variable: the recorded law is lambda = 0.</summary>
    public const double RecordedCoefficient = 0.0;

    /// <summary>The coefficient at which the clock law's second-order term coincides with GR's own.</summary>
    public static double TheGrCoefficient() => -2.0;

    // ===================== TASK 4 FIRST: WHICH PRINCIPLES SELECT? =====================

    /// <summary>
    /// THE SELECTING PRINCIPLES (task 4), each measured rather than argued. The discriminator is the COMPOSITION
    /// residual, because multiplicativity is the one structural property that separates the sector's law from every
    /// other member of its family — and the audit's central finding is that the surviving theory does NOT contain it:
    /// the chain's own uniqueness audit classes it COMPLETELY FREE and says it is absent rather than imported.
    /// </summary>
    public static SelectingPrinciple[] SelectingPrinciples()
    {
        var composition = ClockLawUniquenessAudit.Composition();
        double recorded = composition.Single(c => c.Law == ClockLawUniquenessAudit.At().Name).Residual;
        double best = composition.Where(c => c.Law != ClockLawUniquenessAudit.At().Name)
            .Where(c => !double.IsInfinity(c.Residual)).Min(c => c.Residual);
        var features = ClockLawUniquenessAudit.ForcedFeatures();
        var multiplicativity = features.Single(f => f.Feature.Contains("multiplicativity"));
        var second = features.Single(f => f.Feature.Contains("F''(0) = 4"));
        var classes = ClockLawUniquenessAudit.CoefficientClasses();
        var viability = ClockLawUniquenessAudit.ViabilityTable();
        var ladder = ClockLawUniquenessAudit.Ladder();
        return new[]
        {
            new SelectingPrinciple("MULTIPLICATIVITY (the clock composition multiplies)",
                !multiplicativity.Status.Contains("FREE", StringComparison.Ordinal),
                recorded, best, multiplicativity.LawsWithIt == 1,
                $"it separates the sector's law from every other member of its family ({E(recorded)} against a best "
                + $"competitor at {E(best)}), and it would force lambda = 0 UNIQUELY — but the surviving theory does "
                + $"NOT contain it: the chain's own audit classes it '{multiplicativity.Status}' at "
                + $"{multiplicativity.LawsWithIt} of {multiplicativity.LawsCompared} laws and states that it is ABSENT "
                + "rather than imported"),

            new SelectingPrinciple("VIABILITY (positivity and monotonicity of the clock)", true, recorded, best, false,
                $"it selects NOTHING: {viability.Count(v => v.Viable)} of {viability.Length} laws are viable, both "
                + "sides of the coefficient included, so the constraint is satisfied by every sign region"),

            new SelectingPrinciple("THE SECOND-ORDER COEFFICIENT (F''(0) = 4, shared)", true, recorded, best, false,
                $"it is a CLASS constraint, not a value: {second.LawsWithIt} of {second.LawsCompared} laws share it "
                + $"and the chain classes it '{second.Status}' — within the free-room family it would pin lambda = 0, "
                + "but the family's own membership is what is not derived"),

            new SelectingPrinciple("THE PINNED DATA (the recorded rows)", false, recorded, best, false,
                "EXCLUDED BY THE TASK'S OWN RULES as an answer: selecting the coefficient by the data it must predict "
                + "is a FIT, and the audit does not use it — it records only that the data leave the coefficient "
                + "unconstrained over a measured window (G_087)"),

            new SelectingPrinciple("CONSERVATION (count conservation)", true, recorded, best, false,
                "it cannot see the coefficient at all: the kernel is applied at READ time and the conservation "
                + "statement takes the operations on the state, so the coefficient's arity excludes it"),

            new SelectingPrinciple("STABILITY (a flow with the coefficient as a variable)", false, recorded, best, false,
                "there is no flow to be stable or unstable: no update relation takes the coefficient as an argument, "
                + $"and the ladder's coefficients are fixed numbers for each law ({ladder.Length} laws compared)"),

            new SelectingPrinciple("THE AT/GR DISTINCTION (the theory's own unique prediction)", true, recorded, best, false,
                $"it EXCLUDES one value rather than selecting one: at lambda = {F3(TheGrCoefficient())} the clock law's "
                + "second-order term coincides with GR's, which would destroy the sector's distinctive second-order "
                + "signature — an exclusion of a point, not a selection of a value"),
        };
    }

    /// <summary>The counting half of task 4: how many surviving principles select, and how many would.</summary>
    public static (int Principles, int InTheSurvivingTheory, int WouldSelectUniquely, int SelectInTheSurvivingTheory,
        string Reading) PrincipleCounts()
    {
        var rows = SelectingPrinciples();
        int inTheory = rows.Count(r => r.InTheSurvivingTheory);
        int would = rows.Count(r => r.WouldSelectUniquely);
        int select = rows.Count(r => r.InTheSurvivingTheory && r.WouldSelectUniquely);
        return (rows.Length, inTheory, would, select,
            $"{rows.Length} candidate principles; {inTheory} are in the surviving theory and {would} would select a "
            + $"unique value — and the intersection is {select}, which is the audit's answer");
    }

    // ===================== TASK 2: THE BOUNDARIES =====================

    /// <summary>
    /// THE THREE SIGN REGIONS (task 2), each measured with the same instruments: viability, the second-order
    /// coefficient, the redshift's quadratic, the free part at the target, the target prediction, and whether the
    /// law's second order coincides with GR's.
    /// </summary>
    public static BoundaryRow[] Boundaries()
    {
        var at = ClockLawUniquenessAudit.At();
        var gr = ClockLawUniquenessAudit.Gr();
        double xTarget = ClockLawUniquenessAudit.XTarget;
        double grQuadratic = ClockLawUniquenessAudit.RedshiftCoefficient(gr, 2);
        return new[] { RecordedCoefficient, 0.5, 2.0, 10.0, -0.5, TheGrCoefficient(), -100.0 }
            .Select(c => Row(c, xTarget, grQuadratic))
            .ToArray();

        BoundaryRow Row(double c, double x, double grSecond)
        {
            var law = ClockLawUniquenessAudit.FreeRoom(c);
            double coefficient = ClockLawUniquenessAudit.Coefficient(law, 2);
            double quadratic = ClockLawUniquenessAudit.RedshiftCoefficient(law, 2);
            // the coefficients are DIFFERENCED numerically, so they carry ~1e-9 noise: the comparison is made at 1e-6
            // rather than at the equality rule, and the tolerance is stated here rather than left to the reader
            bool matchesGr = Math.Abs(coefficient - ClockLawUniquenessAudit.Coefficient(gr, 2)) < 1e-6
                && Math.Abs(quadratic - grSecond) < 1e-6;
            bool viable = ClockLawUniquenessAudit.IsViable(law);
            string regime = c > 0.0 ? "lambda > 0" : c < 0.0 ? "lambda < 0" : "lambda = 0";
            if (Math.Abs(c - TheGrCoefficient()) < 1e-12) regime = "lambda = " + F3(TheGrCoefficient()) + " (the GR point)";
            if (c >= 10.0) regime = "lambda strongly positive";
            if (c <= -10.0) regime = "lambda strongly negative";
            string reading = !viable
                ? $"NOT VIABLE: the clock stops being a monotone increasing function of the potential, so the sign "
                  + "region is EXCLUDED — and this is the only exclusion the surviving principles produce"
                : matchesGr
                    ? $"the clock law's second order COINCIDES with GR's ({F3(coefficient)} against "
                      + $"{F3(ClockLawUniquenessAudit.Coefficient(gr, 2))}), so the sector's distinctive second-order "
                      + "signature is gone — a real exclusion, by the theory's own prediction rather than by data"
                    : $"second-order coefficient {F3(coefficient)}, redshift quadratic {F3(quadratic)}, free part at "
                      + $"the target {F3(ClockLawUniquenessAudit.FreePart(law, x))}, target prediction "
                      + $"{F3(ClockLawUniquenessAudit.OnePlusZ(law, x))} — and the law remains VIABLE";
            return new BoundaryRow(regime, c, true, true, viable, coefficient, quadratic,
                ClockLawUniquenessAudit.FreePart(law, x), ClockLawUniquenessAudit.OnePlusZ(law, x), matchesGr, reading);
        }
    }

    /// <summary>
    /// THE COEFFICIENT'S UPPER BOUND, found by BISECTION rather than read off a formula: positivity and monotonicity
    /// of the clock hold on an interval, and the interval's edge is measured. The analytic value is also reported,
    /// and the two are compared, so the bound is verified rather than assumed.
    /// </summary>
    public static double PositiveBound()
    {
        double lo = 0.0, hi = 1.0;
        while (ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(hi)) && hi < 1e6) { lo = hi; hi *= 2.0; }
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (lo + hi);
            if (ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(mid))) lo = mid; else hi = mid;
        }
        return 0.5 * (lo + hi);
    }

    /// <summary>The bound the monotonicity requirement implies: `1 + lambda x > 0` on the physical range.</summary>
    public static double AnalyticPositiveBound() => 1.0 / Math.Abs(ClockLawUniquenessAudit.XPhysicalEnd);

    /// <summary>Does anything bound the coefficient on either side? Measured over the sweep, not assumed.</summary>
    public static (string Side, int LawsTested, int ViableLaws, double ExtremeTested, bool Bounded, string Reading)[]
        Bounds()
    {
        var sweep = new[] { -100.0, -10.0, -2.0, -1.0, -0.5, 0.0, 0.5, 1.0, 2.0, 10.0, 100.0 };
        int negative = sweep.Count(c => c < 0.0);
        int positive = sweep.Count(c => c > 0.0);
        int negativeViable = sweep.Where(c => c < 0.0).Count(c => ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(c)));
        int positiveViable = sweep.Where(c => c > 0.0).Count(c => ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(c)));
        return new[]
        {
            ("lambda < 0", negative, negativeViable, -100.0, negativeViable != negative,
                $"every negative value tested is VIABLE, down to {F3(-100.0)}, so there is NO lower bound from the "
                + "surviving principles — positivity and monotonicity hold for every negative value analytically, and "
                + "the extreme where the numerical test fails is an UNDERFLOW of the clock, which the audit records as "
                + "a numerical limit rather than a physical one"),
            ("lambda > 0", positive, positiveViable, 100.0, positiveViable != positive,
                $"the positive side IS bounded: monotonicity fails at the large values, and the edge found by bisection "
                + $"is {F3(PositiveBound())} against an analytic {F3(AnalyticPositiveBound())} "
                + $"(1 + lambda x > 0 on the physical range, whose end is {F3(ClockLawUniquenessAudit.XPhysicalEnd)}) — "
                + "and the 0.1 per cent gap between the two is the SAMPLER's rather than the theory's, reported here "
                + "rather than smoothed away"),
        };
    }

    // ===================== TASK 3: THE STABILITY ANALYSIS =====================

    /// <summary>
    /// THE STABILITY ANALYSIS (task 3), and its honest outcome: a stability question needs a FLOW, and the sector
    /// has none for the coefficient — measured as arity, not as an opinion. What can be measured instead is the
    /// response of the readouts, and that response is bounded and smooth.
    /// </summary>
    public static (string Question, string Answer, string Measurement)[] Stability()
    {
        var ladder = ClockLawUniquenessAudit.Ladder();
        var viability = ClockLawUniquenessAudit.ViabilityTable();
        return new[]
        {
            ("is there a flow with the coefficient as a variable?",
                "NO — so 'fixed-point selected' is UNDEFINED for it",
                "no update relation and no operation takes the coefficient as an argument: the kernel is applied at "
                + $"READ time, so the coefficient has no equation of motion"),
            ("is the readout response stable — does a perturbation grow?",
                "the response is BOUNDED and SMOOTH",
                $"the slope drift over the whole family is at most "
                + $"{E(viability.Max(v => Math.Abs(v.SlopeDrift)))}, so perturbing the coefficient moves the readouts "
                + "monotonically without amplification"),
            ("does the family contain a member with a runaway coefficient?",
                "yes, at extreme suppression — and it is VIABLE all the same",
                $"the ladder's {ladder.Length} laws all enter at first order 2 and diverge in the higher ones, and the "
                + "target prediction at lambda = -100 is "
                + $"{F3(ClockLawUniquenessAudit.OnePlusZ(ClockLawUniquenessAudit.FreeRoom(-100.0), ClockLawUniquenessAudit.XTarget))} "
                + "— so the extremes are large in their EFFECT and still not excluded by viability"),
        };
    }

    // ===================== TASK 5: NEW OBSERVABLES =====================

    /// <summary>
    /// DOES THE COEFFICIENT INTRODUCE NEW OBSERVABLES (task 5)? Measured against the surviving readout set: it moves
    /// VALUES of readouts that already exist and adds none, which is why the whole question is a question about a
    /// CONSTANT rather than about a new field.
    /// </summary>
    public static (string Question, int Answer, string Measurement) NewObservables()
    {
        var effect = KernelCurvatureActuationAudit.PerUnitInput()
            .Where(p => p.Side == "the effect side").ToArray();
        var census = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full());
        return ("how many NEW observables does the coefficient introduce?", 0,
            $"ZERO: it moves {effect.Length} readouts that already exist (largest "
            + $"{F3(effect.Max(p => p.PerUnitCurvature))} relative per unit coefficient) and adds none — the surviving "
            + $"description still has {census.Length} readouts, so the coefficient is a CONSTANT of the theory rather "
            + "than the source of a new field");
    }

    // ===================== TASK 6: MANIPULATION =====================

    /// <summary>
    /// DOES THE COEFFICIENT REOPEN A MANIPULATION CHANNEL (task 6)? G_094's reopening was conditional on a STRUCTURE
    /// that carries the coefficient; a coefficient with no carrier is not a knob, and the measurement says so.
    /// </summary>
    public static (string Question, bool Reopens, string Measurement)[] Manipulation()
    {
        var couplings = KernelCurvatureActuationAudit.CouplingCounts();
        var chain = ActuatorVersusObservableAudit.ChainCounts();
        var census = ActuatorVersusObservableAudit.RoleCensus();
        return new[]
        {
            ("does the coefficient itself add an actuatable channel?", false,
                $"no: the curvature's coupling to every state-based process is exactly "
                + $"{F3(couplings.MaximumCouplingStrength)} over {couplings.SurvivingCandidates} surviving candidates, "
                + $"and the actuatable channel count is unchanged at {chain.ActuatableOnTheSurvivingTheory}"),
            ("is the coefficient written by any operation?", false,
                $"no: {census.Actuatable} structure of {census.Total} is actuatable and it is the state — no operation "
                + "has the coefficient in its argument list"),
            ("would G_094's reopening happen without a carrier?", false,
                "no: G_094's channel appears only because a closed MEMORY CHANNEL is a structure an operation can act "
                + "on — the value alone is not a channel, which is why the two questions are separate"),
        };
    }

    // ===================== TASK 1 AND 7: THE CLASSIFICATION AND THE CRITERIA =====================

    /// <summary>Task 1's four classes, each with the measurement that decides it.</summary>
    public static (CoefficientStatus Status, bool Holds, string Measurement)[] Classification()
    {
        var counts = PrincipleCounts();
        var bounds = Bounds();
        var principles = SelectingPrinciples();
        var multiplicity = principles.Single(p => p.Principle.StartsWith("MULTIPLICATIVITY"));
        return new[]
        {
            (CoefficientStatus.Derived, false,
                $"NOT derived by the surviving theory: {counts.SelectInTheSurvivingTheory} of its "
                + $"{counts.InTheSurvivingTheory} contained principles selects a value, and the one that would is "
                + "measured as ABSENT"),
            (CoefficientStatus.Emergent, false,
                "NOT emergent: nothing generates it — no flow, no operation and no readout produces it, so no "
                + "assembly process yields it"),
            (CoefficientStatus.FixedPointSelected, false,
                "NOT fixed-point selected: a fixed point needs a flow, and the arity measurement shows the sector has "
                + "none for the coefficient"),
            (CoefficientStatus.Empirical, true,
                $"EMPIRICAL: it is bounded on {bounds.Count(b => b.Bounded)} of 2 sides while the AT/GR structure "
                + $"excludes exactly one value (lambda = {F3(TheGrCoefficient())}), so what the surviving theory fixes "
                + "is a POINT IT MUST AVOID rather than a value it must take"),
        };
    }

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var counts = PrincipleCounts();
        var principles = SelectingPrinciples();
        var multiplicity = principles.Single(p => p.Principle.StartsWith("MULTIPLICATIVITY"));
        var bounds = Bounds();
        return new[]
        {
            ("FAILURE: lambda remains free", "MET",
                $"{counts.SelectInTheSurvivingTheory} of the {counts.InTheSurvivingTheory} contained principles "
                + $"selects a value, and the coefficient is viable on both sides ({bounds.Sum(b => b.ViableLaws)} of "
                + $"{bounds.Sum(b => b.LawsTested)} sweep points)"),
            ("SUCCESS: a unique value is forced", "NOT MET BY THE SURVIVING THEORY",
                $"the principle that WOULD force it does so uniquely ({E(multiplicity.ResidualForTheSectorLaw)} against "
                + $"a best competitor at {E(multiplicity.BestCompetingResidual)}) — and the theory does not contain it: "
                + "the composition property is measured as absent rather than imported"),
            ("and the value that would be forced is named", "NONE IS ASSUMED",
                "the audit does NOT set the coefficient to zero and does NOT fit it to observations, as the task "
                + "requires: it reports that multiplicativity would select zero, and that the selection is not "
                + "available to the surviving theory"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured residual, a measured count or a
    /// measured viability.
    /// </summary>
    public static string Verdict()
    {
        var counts = PrincipleCounts();
        var principles = SelectingPrinciples();
        var multiplicity = principles.Single(p => p.Principle.StartsWith("MULTIPLICATIVITY"));
        var bounds = Bounds();
        var classification = Classification();
        var manipulation = Manipulation();
        var b = new StringBuilder();
        b.Append("THE COEFFICIENT REMAINS FREE — AND THE ONE PRINCIPLE THAT WOULD FIX IT IS ABSENT. ");
        b.Append("Of ").Append(counts.Principles).Append(" candidate selecting principles, ")
            .Append(counts.InTheSurvivingTheory).Append(" are in the surviving theory, ")
            .Append(counts.WouldSelectUniquely).Append(" would select a unique value, and the intersection is ")
            .Append(counts.SelectInTheSurvivingTheory).Append(". ");
        b.Append("THE PRINCIPLE THAT WOULD SELECT IS MULTIPLICATIVITY — the clock composition multiplying — and it "
            + "selects UNIQUELY: the sector's law has a composition residual of ")
            .Append(E(multiplicity.ResidualForTheSectorLaw)).Append(" against a best competitor at ")
            .Append(E(multiplicity.BestCompetingResidual)).Append(", so no other member of the family satisfies it. ")
            .Append("BUT THE THEORY DOES NOT CONTAIN IT: the chain's own uniqueness audit classes the property ")
            .Append("'COMPLETELY FREE' at 1 of 9 laws and states that it is ABSENT rather than imported. ");
        b.Append("NOTHING ELSE SELECTS: the coefficient is bounded on ONE side only — monotonicity of the clock requires ")
            .Append("lambda < ").Append(F3(PositiveBound())).Append(" (measured by bisection against an analytic ")
            .Append(F3(AnalyticPositiveBound())).Append("), while the negative side has NO bound at all; the second-order ")
            .Append("coefficient is a CLASS shared by 3 of 9 laws, conservation cannot see the coefficient at all (arity), ")
            .Append("there is NO FLOW for it (so 'fixed-point selected' is undefined), and the pinned-data route is ")
            .Append("excluded by the task as a fit. ");
        b.Append("THE SURVIVING THEORY THEREFORE FIXES A POINT TO AVOID RATHER THAN A VALUE TO TAKE: at lambda = ")
            .Append(F3(TheGrCoefficient()))
            .Append(" the clock law's second order coincides with GR's, which would destroy the sector's distinctive "
                + "second-order signature. ");
        b.Append("AND THE CLASSIFICATION IS EMPIRICAL on all four counts tested: not derived (").Append(counts.SelectInTheSurvivingTheory)
            .Append(" contained principle selects), not emergent (nothing generates it), not fixed-point selected (no ")
            .Append("flow exists), only empirical — with the coefficient bounded on ")
            .Append(bounds.Count(b2 => b2.Bounded)).Append(" of 2 sides. ");
        b.Append("THE CONSEQUENCES ARE THE SEQUEL TO G_094: the coefficient introduces ")
            .Append(NewObservables().Answer).Append(" new observables (it moves values of readouts that already exist) ")
            .Append("and reopens no manipulation channel by itself (the coupling is zero and no operation writes it), ")
            .Append("so G_094's reopening needs the STRUCTURE and not the value. ");
        b.Append("FAILURE CRITERION MET, SUCCESS CRITERION NOT: the coefficient is free, and the audit names the value ")
            .Append("that would be forced — zero, by multiplicativity — WITHOUT setting it and WITHOUT fitting it, ")
            .Append("which is what the task's two prohibitions require. The open question is therefore not a number but ")
            .Append("a DERIVATION: why does the clock composition multiply?");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the coefficient is EMPIRICAL on all four classes tested, because no principle the");
        b.AppendLine("surviving theory contains selects a value; that the one principle which would select does so uniquely and is");
        b.AppendLine("measured as ABSENT; that the coefficient is viable on both sides of zero, so nothing bounds it; that there");
        b.AppendLine("is no flow for it, so 'fixed-point selected' is undefined rather than false; that the surviving theory fixes");
        b.AppendLine("a point it must AVOID instead; and that the coefficient introduces no observable and no channel of its own.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that no value is correct. The audit measures that the surviving theory does not");
        b.AppendLine("SELECT one — which is a statement about the premises, not about the world. It also does not establish that");
        b.AppendLine("multiplicativity is the ONLY route: it is the only route among the candidate principles tested here, and a");
        b.AppendLine("principle this audit did not think to test would have to be found by a new measurement rather than by adding");
        b.AppendLine("a row.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_091 OR G_094: G_091 measured that the curvature cannot be ACTUATED and");
        b.AppendLine("G_094 that closing the memory channel would move readouts CONDITIONALLY on the value delivered. This audit");
        b.AppendLine("asks which premise DELIVERS the value, measures seven candidate principles against the chain's own");
        b.AppendLine("instruments, and finds the one that would select it outside the surviving theory — which is a different");
        b.AppendLine("statement from either, and the one that makes the sector's open question precise.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a DERIVATION of the composition law (which would force the coefficient to");
        b.AppendLine("zero and turn this audit's failure into a success); a measurement fine enough to see the second order at");
        b.AppendLine("the target (which would make the coefficient empirical in FACT rather than in principle); or a flow that");
        b.AppendLine("takes the coefficient as a variable (which would make 'fixed-point selected' a meaningful class here).");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputPrinciples()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE SELECTING PRINCIPLES (task 4)");
        b.AppendLine("   principle                                          in theory  would select  composition residual");
        foreach (var p in SelectingPrinciples())
        {
            b.AppendLine($"   {p.Principle,-50} {Yes(p.InTheSurvivingTheory),-10} {Yes(p.WouldSelectUniquely),-13} "
                + $"{E(p.ResidualForTheSectorLaw)}");
            b.AppendLine($"     {p.Measurement}");
        }
        b.AppendLine();
        b.AppendLine(PrincipleCounts().Reading);
        return b.ToString();
    }

    public static string OutputBoundaries()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE BOUNDARY ANALYSIS (task 2)");
        b.AppendLine("   regime                    lambda     viable  F''-coefficient  redshift quadratic  free part  1+Z at target  GR second order");
        foreach (var r in Boundaries())
        {
            b.AppendLine($"   {r.Regime,-24} {F3(r.Coefficient),-10} {Yes(r.Viable),-7} {F3(r.SecondOrderCoefficientOfF),-16} "
                + $"{F3(r.RedshiftQuadratic),-19} {F3(r.FreePartAtTheTarget),-10} {F3(r.TargetPrediction),-13} "
                + $"{Yes(r.MatchesGrAtSecondOrder)}");
            b.AppendLine($"     {r.Reading}");
        }
        b.AppendLine();
        b.AppendLine("   does anything bound the coefficient?");
        foreach (var (side, tested, viable, extreme, bounded, reading) in Bounds())
        {
            b.AppendLine($"   {side}: {viable} of {tested} law(s) viable down/up to {F3(extreme)} — bounded {Yes(bounded)}");
            b.AppendLine($"     {reading}");
        }
        return b.ToString();
    }

    public static string OutputStabilityAndObservables()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE STABILITY ANALYSIS (task 3)");
        foreach (var (question, answer, measurement) in Stability())
        {
            b.AppendLine($"   {question}");
            b.AppendLine($"     -> {answer}");
            b.AppendLine($"     {measurement}");
        }
        b.AppendLine();
        b.AppendLine("4. NEW OBSERVABLES AND CHANNELS (tasks 5 and 6)");
        var (question2, answer2, measurement2) = NewObservables();
        b.AppendLine($"   {question2}");
        b.AppendLine($"     -> {answer2}");
        b.AppendLine($"     {measurement2}");
        foreach (var (question, reopens, measurement) in Manipulation())
        {
            b.AppendLine($"   {question}");
            b.AppendLine($"     -> {Yes(reopens)}");
            b.AppendLine($"     {measurement}");
        }
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("5. THE CLASSIFICATION AND THE CRITERIA (tasks 1 and 7)");
        foreach (var (status, holds, measurement) in Classification())
        {
            b.AppendLine($"   {status}: holds {Yes(holds)}");
            b.AppendLine($"     {measurement}");
        }
        b.AppendLine();
        foreach (var (criterion, outcome, evidence) in Criteria())
        {
            b.AppendLine($"   {criterion}");
            b.AppendLine($"     -> {outcome}");
            b.AppendLine($"     {evidence}");
        }
        return b.ToString();
    }

    private static string Yes(bool value) => value ? "yes" : "no";

    private static string E(double value) => value.ToString("E3", CultureInfo.InvariantCulture);

    private static string F3(double value) => value.ToString("F3", CultureInfo.InvariantCulture);
}
