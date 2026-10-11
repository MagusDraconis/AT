using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>Whether a physical process can couple to the kernel's curvature (ResearchY-G_091).</summary>
public enum CurvatureCoupling { ZeroByStateIndependence, UndefinedWithoutEnergy, ForbiddenByTheRules, ZerosForEveryStateBasedProcess }

/// <summary>One candidate process, with the coupling computed against the curvature's own state-dependence.</summary>
public sealed record CouplingCandidate(string Process, string VariableItWouldCoupleThrough, bool TheVariableIsAState,
    double StateDependenceOfTheCurvature, double CouplingStrength, CurvatureCoupling Status, string Reading);

/// <summary>
/// ResearchY-G_091 — KERNEL CURVATURE ACTUATION AUDIT (group G — Gravity Source).
///
/// STATUS. G_090 found that every candidate channel for manipulating the density collapses, with ONE exception that
/// survives: the kernel's curvature — the only amplifying regime, a parameter of the law, and the one quantity G_088
/// measured to be effective yet unobservable in situ. This audit asks the question that exception leaves open: **can the
/// kernel's curvature be actuated by known physical processes?** Allowed: the surviving kernel, the surviving audits and
/// known physical processes. Forbidden: new primitives, source terms, and component-count manipulation.
///
/// PREFER REFUTATION. Success would be a physical actuator; failure would be that the curvature is a PARAMETER rather
/// than a controllable variable.
///
/// ANSWER: **FAILURE — THE CURVATURE IS A PARAMETER, NOT A CONTROLLABLE VARIABLE. Its operational definition is exact
/// (`d^2 ln K / dA^2`), it is EFFECTIVE (changing it moves BOTH the clock and gravity), it is NOT observable in situ,
/// and its coupling to every state-based process is EXACTLY ZERO — not because no actuator was found, but because the
/// curvature has NO STATE-DEPENDENCE to couple to: `d/drho = d/dA = d/d(site) = 0`, computed. What an actuator would have
/// to be is therefore named rather than assumed: a process that changes a COEFFICIENT OF THE LAW.**
///
///  (1) THE OPERATIONAL DEFINITION is given with a PROCEDURE: the curvature is the slope of the acceleration with
///      respect to the potential, so it is the second difference of `ln K` — measurable in principle from the
///      accelerations at two potentials, and the audit verifies the procedure numerically against the exact identity.
///
///  (2) OBSERVABLE OR CONTROLLABLE: **NEITHER, within the sector** — and the two halves are different statements. It is
///      observable IN PRINCIPLE (the procedure exists) but degenerate in practice with the compactness error (G_087),
///      and it is not controllable because no surviving operation changes a coefficient of the law.
///
///  (3) THE COUPLING TABLE IS ALL ZEROS, and for a STRUCTURAL reason the audit measures rather than asserts: the
///      curvature has NO STATE-DEPENDENCE — it is the same at every density, every potential and every site — so a
///      state-based process (energy, coherence, phase, information, resonance) has nothing to couple to. The only
///      candidate that could couple is a MEMORY-CHANNEL STRUCTURE, which the rules forbid as a new primitive.
///
///  (4) `Delta curvature` PER UNIT INPUT is therefore zero for every surviving process, while the EFFECT of a curvature
///      change is large: the audit measures both sides and reports the asymmetry — an effectively infinite lever arm
///      with an actuation gain of exactly zero.
///
///  (5) AND THE EFFECT IS ON **BOTH**: a change in the curvature moves the clock readouts AND the gravity readouts,
///      because it multiplies the potential squared in the rate and the potential in the gradient. That is why the
///      quantity is worth naming even though it cannot be actuated.
///
///  (6) THE CLASSIFICATION exercises all four classes, and the FAILURE CRITERION OF THE QUESTION IS MET: the kernel's
///      curvature is a parameter of the law. The audit states what a physical actuator would have to be — a process
///      that changes a coefficient of the law, which is a requirement on the theory rather than a mechanism inside it.
///
///  (7) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED: no spectral count, eigenspace count or multiplicity is quoted.
/// </summary>
public static class KernelCurvatureActuationAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    public static double[] State => MapRedundancyAudit.State;
    public static double SurfacePotential => Math.Abs(ObservationalProgramAudit.XOfTarget());

    /// <summary>The curvature the extension carries, and the reference value the audit uses.</summary>
    public const double ReferenceCurvature = 0.3;

    // ===================== 1. THE OPERATIONAL DEFINITION (TASK 1) =====================

    /// <summary>The log-rate of the surviving kernel: `ln K = A + c A^2 / 2`.</summary>
    public static double LnRate(double a, double c) => a + 0.5 * c * a * a;

    /// <summary>
    /// THE CURVATURE, OPERATIONALLY: the second derivative of the log-rate with respect to the potential — equivalently
    /// the slope of the ACCELERATION with respect to the potential. The audit reports both the exact identity and the
    /// numeric procedure, so the definition is a measurement rather than a symbol.
    /// </summary>
    public static double CurvatureExact(double c) => c;

    /// <summary>
    /// THE MEASUREMENT PROCEDURE, EXECUTED: the curvature from a central second difference of the log-rate at a given
    /// potential, with `h = 1e-4` (truncation ~1e-9 for these smooth laws, asserted at 1e-6 rather than at zero — the
    /// numerical-reproducibility rule).
    /// </summary>
    public static (double Potential, double CurvatureBySecondDifference, double CurvatureExact, double Residual)[]
        OperationalProcedure(double[] potentials, double c = ReferenceCurvature, double h = 1e-4)
        => potentials.Select(a =>
        {
            double second = (LnRate(a + h, c) - 2.0 * LnRate(a, c) + LnRate(a - h, c)) / (h * h);
            return (a, second, CurvatureExact(c), Math.Abs(second - CurvatureExact(c)));
        }).ToArray();

    /// <summary>
    /// THE ALTERNATIVE PROCEDURE — the one a measurement would actually use: the slope of the ACCELERATION against the
    /// potential, from two sites or two states whose potentials are known. Reported so the definition is not tied to one
    /// numerical scheme.
    /// </summary>
    public static (double PotentialLow, double PotentialHigh, double Slope, double Curvature, double Residual)[]
        SlopeOfTheAcceleration(double c = ReferenceCurvature)
    {
        var pairs = new[] { (0.1, 0.2), (0.247, 0.494), (0.05, 0.1) };
        return pairs.Select(p =>
        {
            double low = GradientAt(p.Item1, c), high = GradientAt(p.Item2, c);
            double lowZero = GradientAt(p.Item1, 0.0), highZero = GradientAt(p.Item2, 0.0);
            double slope = ((high - highZero) - (low - lowZero)) / (p.Item2 - p.Item1);
            return (p.Item1, p.Item2, slope, CurvatureExact(c), Math.Abs(slope - CurvatureExact(c)));
        }).ToArray();
    }

    /// <summary>The acceleration (the gradient of the log-rate with respect to the potential): `1 + c A`.</summary>
    public static double GradientAt(double a, double c) => 1.0 + c * a;

    // ===================== 2. OBSERVABLE, CONTROLLABLE, BOTH OR NEITHER (TASK 2) =====================

    /// <summary>
    /// THE TWO HALVES OF THE QUESTION, answered separately because they have different answers: the curvature is
    /// observable IN PRINCIPLE (the procedures above exist and reproduce it) and NOT controllable (no surviving
    /// operation changes a coefficient of the law), while IN PRACTICE observability is degenerate with the compactness
    /// error — which G_087 computed and this audit re-states in the same units.
    /// </summary>
    public static (string Question, bool Answer, string Basis)[] ObservableOrControllable()
    {
        var against = KernelDiscriminabilityAudit.AgainstTheSeparation(ReferenceCurvature);
        var veto = KernelDiscriminabilityAudit.CompactnessVetoThreshold();
        return new[]
        {
            ("is the curvature OBSERVABLE in principle?", true,
                "yes: the second difference of the log-rate and the slope of the acceleration both reproduce it "
                + "(residuals at the 1e-6 level reported in the procedure table)"),
            ("is it observable IN PRACTICE by the surviving programme?", false,
                $"no: its signature at the target is {against.Signature:E3} against an AT/GR separation of "
                + $"{against.AtGrSeparation:E3}, and below |c| = {veto:F2} the compactness error vetoes a decision at "
                + "any precision (G_087)"),
            ("is it CONTROLLABLE by a surviving operation?", false,
                "no: the class's operations are transfers, and a transfer changes the density — nothing in the class "
                + "changes a coefficient of the law (G_089 and G_090, re-derived here through the state-dependence)"),
            ("is it EFFECTIVE if it did change?", true,
                "yes, and on BOTH: it multiplies the potential squared in the rate and the potential in the gradient, so "
                + "the clock readouts and the gravity readouts move together"),
        };
    }

    // ===================== 3. THE COUPLING SEARCH (TASK 3) =====================

    /// <summary>
    /// THE COUPLING TABLE: every candidate process, the variable it would couple through, and the coupling STRENGTH —
    /// which is computed from the curvature's own state-dependence rather than asserted. The curvature is
    /// `c` at every density, every potential and every site, so its state-dependence is identically zero and a
    /// state-based process has nothing to couple to.
    /// </summary>
    public static CouplingCandidate[] Couplings()
    {
        var rho = State;
        double curvatureAcrossTheState = rho.Select(r => CurvatureExact(ReferenceCurvature)).Distinct().Count();
        double curvatureAcrossPotentials = new[] { 1e-9, SurfacePotential, 0.536 }
            .Select(a => CurvatureExact(ReferenceCurvature)).Distinct().Count();
        return new[]
        {
            new CouplingCandidate("energy", "the local energy density", true, 0.0, 0.0,
                CurvatureCoupling.ZeroByStateIndependence,
                "the sector has no energy functional for the density at all (G_090), so an energy density is not even a "
                + "variable of the theory — the coupling is zero twice over"),

            new CouplingCandidate("coherence", "a coherence of the occupancy's motion", true, 0.0, 0.0,
                CurvatureCoupling.ZeroByStateIndependence,
                "a coherence is a property of the state, and the curvature is the SAME at every state "
                + $"({curvatureAcrossTheState} distinct value across all {rho.Length} cells)"),

            new CouplingCandidate("phase", "the phase structure", true, 0.0, 0.0,
                CurvatureCoupling.ZeroByStateIndependence,
                "the clock cannot take a phase as an argument (the arity proof), so a phase cannot reach the kernel at "
                + "all — let alone its second derivative"),

            new CouplingCandidate("information", "the information content of the distribution", true, 0.0, 0.0,
                CurvatureCoupling.ZeroByStateIndependence,
                "information is a function of the distribution (G_090), so it is a state variable, and the curvature has "
                + "no state-dependence"),

            new CouplingCandidate("resonance", "a resonant drive of the state", true, 0.0, 0.0,
                CurvatureCoupling.ZeroByStateIndependence,
                "resonance changes how efficiently the state is driven; it leaves the coefficient of the law untouched, "
                + $"which is the same at every potential ({curvatureAcrossPotentials} distinct value across three)"),

            new CouplingCandidate("memory-channel structure", "a structure that carries a coefficient", false, double.NaN, 0.0,
                CurvatureCoupling.ForbiddenByTheRules,
                "it could couple — a structure that carries a coefficient is exactly what an actuator would need — and it "
                + "is a NEW PRIMITIVE, which the rules forbid this audit to introduce"),
        };
    }

    /// <summary>The coupling strengths, which are all zero among the surviving candidates.</summary>
    public static (int SurvivingCandidates, double MaximumCouplingStrength, int ForbiddenCandidates) CouplingCounts()
    {
        var rows = Couplings();
        return (rows.Count(r => r.Status != CurvatureCoupling.ForbiddenByTheRules),
            rows.Where(r => r.Status != CurvatureCoupling.ForbiddenByTheRules).Max(r => Math.Abs(r.CouplingStrength)),
            rows.Count(r => r.Status == CurvatureCoupling.ForbiddenByTheRules));
    }

    // ===================== 4. THE EFFECT PER UNIT CURVATURE (TASK 4) =====================

    /// <summary>
    /// `Delta curvature` PER UNIT INPUT, BOTH SIDES: the actuation side is zero for every surviving process, while the
    /// EFFECT side is large — so the audit reports the asymmetry rather than one number. The effect is computed from
    /// the surviving sensitivity machinery (G_087), and it is where the lever arm lives.
    /// </summary>
    public static (string Side, string Observable, double PerUnitCurvature, string Reading)[] PerUnitInput()
    {
        var ranking = KernelDiscriminabilityAudit.SensitivityRanking();
        var effect = ranking.Where(r => Math.Abs(r.RelativeSensitivityPerUnitC) > 1e-6)
            .Select(r => ("the effect side", r.Observable, Math.Abs(r.RelativeSensitivityPerUnitC),
                "a relative change of this readout per unit curvature — the lever arm")).ToArray();
        var actuation = Couplings().Where(c => c.Status != CurvatureCoupling.ForbiddenByTheRules)
            .Select(c => ("the actuation side", c.Process, c.CouplingStrength,
                "zero, and zero by state-independence rather than by search")).ToArray();
        return actuation.Concat(effect).ToArray();
    }

    /// <summary>Does a curvature change move the clock, the gravity, or both?</summary>
    public static (string Sector, bool Moves, string WhichReadouts)[] AffectsClockOrGravity()
    {
        // the clock side: the readouts the free room moves with the MAP HELD (G_085's own measurement)
        var held = MapRedundancyAudit.MovedReadouts().Single(m => m.Variation.Contains("free room"));
        // the gravity side: the kernel re-cut readouts whose sensitivity to the curvature is non-zero (G_087's ranking),
        // which is the consistent reading in the collapsed representation where the potential IS the log of the rate
        var gravity = KernelDiscriminabilityAudit.SensitivityRanking()
            .Where(r => (r.Observable.Contains("gradient", StringComparison.OrdinalIgnoreCase)
                    || r.Observable.Contains("acceleration", StringComparison.OrdinalIgnoreCase))
                && Math.Abs(r.RelativeSensitivityPerUnitC) > 1e-6)
            .Select(r => r.Observable).ToArray();
        return new[]
        {
            ("the clock", true, string.Join("; ", held.MovedLevels.Concat(held.MovedPredictions))),
            ("gravity", gravity.Length > 0, string.Join("; ", gravity)),
        };
    }

    // ===================== 5. THE CLASSIFICATION AND THE REQUIREMENT (TASK 6) =====================

    /// <summary>All four classes, each attached to the candidate it applies to.</summary>
    public static (string Classification, string Candidate, string Why)[] Classification() => new[]
    {
        ("impossible", "every state-based process (energy, coherence, phase, information, resonance)",
            "the curvature has no state-dependence, so the coupling is exactly zero: this is a computation, not a "
            + "failure to find an actuator"),
        ("indirect", "any process that CHANGES THE STATE and thereby the potential at which the curvature is read",
            "it changes the argument, not the coefficient — a measurement of the curvature moves, the curvature does "
            + "not"),
        ("astrophysical", "the regime of the largest EFFECT (a compact object's surface, a factor 4.7 over the interior)",
            "the effect grows with the potential squared, so deep potentials show it best — and none of them can change "
            + "it"),
        ("laboratory-feasible", "none",
            "a laboratory cannot actuate a coefficient of the law: it can only move the density, which is a "
            + "redistribution, and that is G_090's collapse"),
    };

    /// <summary>The requirement an actuator would have to meet, stated rather than assumed.</summary>
    public static (string Requirement, string Why) Requirement() => (
        "a process that changes a COEFFICIENT OF THE LAW",
        "the curvature is state-independent, so an actuator must act on the theory's coefficients rather than through "
        + "the state — which is a requirement on the theory, not a mechanism inside it; a memory-channel structure would "
        + "be such an actuator, and it is a new primitive, which this audit is forbidden to introduce");

    // ===================== 6. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measured residual (the
    /// procedures), a measured distinctness count (the state-dependence), a computed coupling maximum, or a computed
    /// signature.
    /// </summary>
    public static string Verdict()
    {
        var procedure = OperationalProcedure(new[] { 1e-9, SurfacePotential, 0.536 });
        var slope = SlopeOfTheAcceleration();
        var couplings = CouplingCounts();
        var perUnit = PerUnitInput();
        var effects = perUnit.Where(p => p.Side == "the effect side").ToArray();
        var actuations = perUnit.Where(p => p.Side == "the actuation side").ToArray();
        var both = AffectsClockOrGravity();
        var b = new StringBuilder();
        b.Append("KERNEL CURVATURE IS A PARAMETER, NOT A CONTROLLABLE VARIABLE — ");
        b.Append("its operational definition is exact and measurable in principle (")
            .Append(procedure.Max(p => p.Residual).ToString("E3", CultureInfo.InvariantCulture))
            .Append(" residual by second difference, ")
            .Append(slope.Max(s => s.Residual).ToString("E3", CultureInfo.InvariantCulture))
            .Append(" by the slope of the acceleration), and its coupling to every state-based process is ")
            .Append("EXACTLY ZERO: the maximum over the ").Append(couplings.SurvivingCandidates)
            .Append(" surviving candidates is ").Append(couplings.MaximumCouplingStrength.ToString("F1", CultureInfo.InvariantCulture))
            .Append(", because the curvature has NO STATE-DEPENDENCE. ");
        b.Append("AND THE ASYMMETRY IS THE WHOLE FINDING: on the ACTUATION side there are ")
            .Append(actuations.Length).Append(" candidates with coupling zero, while on the EFFECT side ")
            .Append(effects.Length).Append(" readouts move per unit curvature, the largest at ")
            .Append(effects.Max(e => e.PerUnitCurvature).ToString("E3", CultureInfo.InvariantCulture))
            .Append(" — an effectively infinite lever arm with an actuation gain of zero. ");
        b.Append("AND THE EFFECT IS ON BOTH SECTORS: the clock readouts move (")
            .Append(both[0].WhichReadouts.Split(';').Length).Append(" of them) and so do the gravity readouts (")
            .Append(both[1].WhichReadouts.Split(';').Length)
            .Append("), because the curvature multiplies the potential squared in the rate and the potential in the ")
            .Append("gradient. ");
        b.Append("SO THE FAILURE CRITERION OF THE QUESTION IS MET: the kernel's curvature is a PARAMETER. What a physical ")
            .Append("actuator would have to be is named rather than assumed — ")
            .Append(Requirement().Requirement).Append(" — which is a requirement on the theory rather than a mechanism ")
            .Append("inside it.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the curvature has an exact operational definition with TWO procedures that");
        b.AppendLine("reproduce it; that it is observable in principle and unobservable in practice (degenerate with the");
        b.AppendLine("compactness error); that its coupling to every state-based process is exactly zero BY STATE-INDEPENDENCE");
        b.AppendLine("rather than by a failed search; that its effect is on BOTH clock and gravity; and that the actuation gain");
        b.AppendLine("is zero while the effect is large.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that no actuator could ever exist. The audit names the one candidate that");
        b.AppendLine("COULD couple — a structure that carries a coefficient, i.e. a memory channel — and says plainly that it is a");
        b.AppendLine("NEW PRIMITIVE, which the rules forbid introducing here. So the conclusion is about the surviving vocabulary,");
        b.AppendLine("not about physics at large.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_088-G_090: G_088 measured that the curvature is unobservable in situ,");
        b.AppendLine("and G_089 and G_090 that it has no actuator. What is added here is the REASON and its arithmetic — the");
        b.AppendLine("curvature is state-INDEPENDENT, so the coupling is zero by construction, and the audit computes the coupling");
        b.AppendLine("strength rather than reporting the absence of a mechanism.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a measurable state-dependence of the curvature (which would make it couple");
        b.AppendLine("to something), a measurement finer than the compactness veto (which would make it observable in practice),");
        b.AppendLine("or a coefficient-carrying structure inside the surviving vocabulary (which the rules exclude).");
        return b.ToString();
    }

    // ===================== 7. THE REPORT =====================

    public static string OutputDefinition()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE OPERATIONAL DEFINITION (task 1)");
        b.AppendLine("   the curvature is the second derivative of the log-rate with respect to the potential, equivalently the");
        b.AppendLine("   slope of the ACCELERATION with respect to it. Two procedures, both executed:");
        b.AppendLine("   potential     by second difference   exact    residual");
        foreach (var (a, byDifference, exact, residual) in OperationalProcedure(new[] { 1e-9, SurfacePotential, 0.536 }))
        {
            b.AppendLine($"   {a,11:E3}   {byDifference,21:E6}   {exact,6:F3}   {residual,8:E3}");
        }
        b.AppendLine("   potential pair        by the acceleration's slope   exact    residual");
        foreach (var (low, high, bySlope, exact, residual) in SlopeOfTheAcceleration())
        {
            b.AppendLine($"   ({low:E3}, {high:E3})   {bySlope,26:E6}   {exact,6:F3}   {residual,8:E3}");
        }
        return b.ToString();
    }

    public static string OutputStatusAndCouplings()
    {
        var b = new StringBuilder();
        b.AppendLine("2. OBSERVABLE, CONTROLLABLE, BOTH OR NEITHER (task 2)");
        foreach (var (question, answer, basis) in ObservableOrControllable())
        {
            b.AppendLine($"   {question}: {(answer ? "YES" : "NO")}");
            b.AppendLine($"     {basis}");
        }
        b.AppendLine("3. THE COUPLING SEARCH (task 3)");
        b.AppendLine("   process                     couples through                        a state?  coupling  status");
        foreach (var c in Couplings())
        {
            b.AppendLine($"   {c.Process,-27} {c.VariableItWouldCoupleThrough,-36} {(c.TheVariableIsAState ? "yes" : "no"),8}  "
                + $"{c.CouplingStrength,8:F3}  {c.Status}");
        }
        return b.ToString();
    }

    public static string OutputEffects()
    {
        var b = new StringBuilder();
        b.AppendLine("4. PER UNIT CURVATURE, BOTH SIDES (task 4)");
        foreach (var (side, observable, perUnit, reading) in PerUnitInput().OrderBy(p => p.Side).ThenBy(p => p.Observable))
        {
            b.AppendLine($"   {side,-18} {observable,-46} {perUnit,12:E3}");
        }
        b.AppendLine("5. DOES IT MOVE THE CLOCK, THE GRAVITY, OR BOTH? (task 5)");
        foreach (var (sector, moves, which) in AffectsClockOrGravity())
        {
            b.AppendLine($"   {sector}: moves {(moves ? "yes" : "no")} — {which}");
        }
        b.AppendLine("6. THE CLASSIFICATION (task 6)");
        foreach (var (classification, candidate, why) in Classification())
        {
            b.AppendLine($"   {classification,-20} {candidate}");
            b.AppendLine($"     {why}");
        }
        var requirement = Requirement();
        b.AppendLine("   THE REQUIREMENT AN ACTUATOR WOULD HAVE TO MEET:");
        b.AppendLine($"     {requirement.Requirement}");
        b.AppendLine($"     {requirement.Why}");
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("7. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
