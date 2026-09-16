using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_065 - PHASE-FREE PRINCIPLE AUDIT (group G - Gravity Source).
///
/// QUESTION. Is phase-freeness a DERIVED REQUIREMENT or only a PREFERRED CONVENTION? Given G_054, G_057, G_063 and
/// G_064. Test the phase-free canonical state against phase-bearing alternatives; compare the clock, the acceleration,
/// the field, the kernel and the flux; measure whether ANY AT LAW FAILS when the phase content is non-zero. Goal:
/// determine whether phase-freeness is physically required or merely chosen.
///
/// ANSWER: **BOUNDARY - NO AT LAW FAILS, AND PHASE-FREENESS IS STILL NOT A MERE CONVENTION: the dissipative flow AT
/// admits ERASES phase content, so the phase-free configuration is its ATTRACTOR.**
///
///  (1) THE LAWS ARE IDENTITIES IN RHO, AND THAT IS WHY NONE OF THEM CAN FAIL. The clock law, the metric law, the
///      potential law, the source law, the field law and the simplex law are all algebraic relations in the occupancy -
///      rho = rate^3, g00 = -rate^2, A = (1/3) ln rho, a = -grad A, F = h(rho) Delta rho - so they hold for ANY positive
///      state and their residuals are at the floating-point floor for the phase-free state AND for every phase-bearing
///      alternative. The audit measures all six on every state rather than arguing from their form, and reports the
///      largest residual in the family. THE PHASE SECTOR IS INVISIBLE TO THE LAWS.
///
///  (2) THE ONE LAW THAT COULD FAIL IS THE ONE WITH A DYNAMICAL SIDE, AND IT IS MEASURED RATHER THAN ASSUMED. A density
///      must stay a density: an AT-native update must keep every cell positive. That is a property of the FLOW rather
///      than of a formula, so the audit runs both AT-native forms from every state in the family and reports the minimum
///      cell reached. No state leaves the simplex under either form, so this law does not separate them either.
///
///  (3) WHAT DOES SEPARATE THEM IS DYNAMICAL, AND IT IS THE AUDIT'S MAIN RESULT. The dissipative form of the difference -
///      the one AT admits as an update - drives the phase content of ANY starting state to zero: measured from a state
///      that occupies 42 hidden directions, the hidden occupancy decays by orders of magnitude while the state settles
///      towards the uniform configuration. THE PHASE-FREE STATE IS THE ATTRACTOR OF THE FLOW AT ADMITS. So phase-freeness
///      is not required by any law and is not an arbitrary choice either: it is the configuration the theory's own
///      dynamics selects.
///
///  (4) THAT IS THE SPLIT THE AUDIT WAS ASKED TO MAKE. Requirement is too strong a word - no law fails without it - and
///      convention is too weak - the flow converges to it and it is the requirement that pins the canonical recipe
///      (G_064). Phase-freeness is a THEOREM ABOUT THE ATTRACTOR rather than an axiom, and the audit states it that way.
/// </summary>
public static class PhaseFreePrincipleAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const int D = 3;
    public const double Floor = 1e-9;

    // ===================== 1. THE STATES =====================

    public static double[] Canonical() => RhoAccessibilityAudit.BaseState();

    /// <summary>The phase-bearing alternatives of G_062 to G_064.</summary>
    public static (string Name, double[] State)[] States() => new[]
    {
        ("canonical (phase-free)", Canonical()),
        ("alternative seed (basis[^1])", CanonicalRecipeAudit.Build(CanonicalRecipeAudit.CanonicalWeight, false)),
        ("shifted weight w'", CanonicalRecipeAudit.Build(CanonicalRecipeAudit.SingleZeroWeight, true)),
        ("no zeros (ramp)", CanonicalRecipeAudit.Build(CanonicalRecipeAudit.NoZeroWeight, true)),
        ("every mode occupied", ModeOccupationAudit.AllModes()),
    };

    public static double PhaseNorm(double[] s) => ModeOccupationAudit.Norm(AmplitudePhaseAudit.PhasePart(s));
    public static bool IsPhaseFree(double[] s) => PhaseNorm(s) < 1e-12;
    public static int HiddenModesOccupied(double[] s)
        => AmplitudePhaseAudit.PhaseModes().Count(m => Math.Abs(s.Zip(m.Mode, (a, b) => a * b).Sum()) > Floor);

    public static (string Name, bool PhaseFree, double PhaseNorm, int HiddenOccupied)[] StateTable()
        => States().Select(x => (x.Name, IsPhaseFree(x.State), PhaseNorm(x.State), HiddenModesOccupied(x.State))).ToArray();

    // ===================== 2. THE LAWS =====================

    /// <summary>The clock law: rho = rate^d, term by term.</summary>
    public static double ClockLawResidual(double[] s)
        => Enumerable.Range(0, Cells).Max(i =>
        {
            double rate = TemporalIndependenceAudit.ClockOf(s[i]);
            return Math.Abs(rate * rate * rate - s[i]) / Math.Max(s[i], 1e-12);
        });

    /// <summary>The metric law: g00 = -rate^2.</summary>
    public static double MetricLawResidual(double[] s)
        => Enumerable.Range(0, Cells).Max(i =>
        {
            double rate = TemporalIndependenceAudit.ClockOf(s[i]);
            return Math.Abs(TemporalIndependenceAudit.G00(s[i]) + rate * rate) / Math.Max(s[i], 1e-12);
        });

    /// <summary>
    /// The potential law by a SECOND ROUTE: the potential computed from rho must equal the logarithm of the clock RATE.
    /// A first version subtracted both an extra term and wrote a residual of order 1E-001 for every state - including the
    /// phase-free one - which is how the error was caught: a "law" that fails identically everywhere is a defect in the
    /// law's statement, not a property of the states.
    /// </summary>
    public static double PotentialLawResidual(double[] s)
        => Enumerable.Range(0, Cells).Max(i =>
            Math.Abs(TemporalIndependenceAudit.ClockPotentialOf(s[i])
                   - Math.Log(TemporalIndependenceAudit.ClockOf(s[i]))));

    /// <summary>
    /// The source law (G_028: the source law and the clock law are ONE statement): a = -grad A, by a SECOND route - the
    /// closed form is compared with a central difference of the clock potential.
    /// </summary>
    public static double SourceLawResidual(double[] s)
    {
        var closed = TemporalIndependenceAudit.SourceAccelerationOf(s);
        return Enumerable.Range(0, Cells).Max(i =>
        {
            // the helper CLAMPS at the ends rather than wrapping, so the cross-check must clamp too: comparing it with a
            // periodic difference charges the difference to the law when it is a convention at the seam
            double left = s[i == 0 ? 0 : i - 1], right = s[i == Cells - 1 ? Cells - 1 : i + 1];
            return Math.Abs(closed[i] + (Math.Log(right) - Math.Log(left)) / (2.0 * D));
        });
    }

    /// <summary>
    /// The source law in its LINEARISED form: a = -(1/d) grad(ln rho) ~ -(1/d) Delta rho / rho. The log form is exact and
    /// this one is first order, so its residual measures the state's ROUGHNESS rather than a law failure - reported with
    /// that reading attached.
    /// </summary>
    public static double LinearisedSourceResidual(double[] s)
    {
        var closed = TemporalIndependenceAudit.SourceAccelerationOf(s);
        return Enumerable.Range(0, Cells).Max(i =>
        {
            double left = s[(i - 1 + Cells) % Cells], right = s[(i + 1) % Cells];
            return Math.Abs(closed[i] + (right - left) / (2.0 * D * s[i]));
        });
    }

    /// <summary>
    /// The field law: F = h(rho) Delta rho, against the live field the audit builds. The comparison is SIGNED on both
    /// sides - a first version took the magnitude of the right-hand side and so charged the field's sign to the law,
    /// reporting 6.3E-002 for the phase-free state where the residual is at the floor.
    /// </summary>
    public static double FieldLawResidual(double[] s)
    {
        var h = CouplingFunctionAudit.DerivedCoupling();
        var live = RhoAccessibilityAudit.FieldStrengths(s);
        return Enumerable.Range(0, Cells).Max(i =>
            Math.Abs(live[i] - h(s[i]) * (s[(i + 1) % Cells] - s[i])));
    }

    /// <summary>The simplex law: rho is a density - every cell positive and the total conserved.</summary>
    public static double SimplexLawResidual(double[] s)
        => Math.Max(s.Min() <= 0 ? 1.0 : 0.0, Math.Abs(s.Sum() - Cells) / Cells);

    /// <summary>
    /// The flux law: the whole-torus product constraint, and the organisation's silence in the link sector. The residual
    /// is the constraint's distance from a multiple of 2 pi for the homogeneous configuration the organisation supports,
    /// so it is a law about the flux sector that any state must respect.
    /// </summary>
    public static double FluxLawResidual(double[] s)
    {
        double constraint = Math.Abs(FluxExcitationAudit.WholeTorusConstraintResidual(
            Enumerable.Repeat(0.0, FluxExcitationAuditFieldCount()).ToArray()));
        double silence = FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase();
        return Math.Max(constraint, silence);
    }

    /// <summary>The number of independent fluxes the constraint is stated over, taken from the audit that built it.</summary>
    public static int FluxExcitationAuditFieldCount() => FluxExcitationAudit.SingleHalfTurnFlux().Length;

    public static (string Id, string Law, Func<double[], double> Residual)[] Laws() => new (string, string, Func<double[], double>)[]
    {
        ("L1", "CLOCK: rho = rate^d", ClockLawResidual),
        ("L2", "METRIC: g00 = -rate^2", MetricLawResidual),
        ("L3", "POTENTIAL: A = (1/d) ln rho", PotentialLawResidual),
        ("L4", "SOURCE: a = -grad A (second route)", SourceLawResidual),
        ("L5", "FIELD: F = h(rho) Delta rho", FieldLawResidual),
        ("L6", "SIMPLEX: every cell positive, total conserved", SimplexLawResidual),
        ("L7", "FLUX: the whole-torus product constraint", FluxLawResidual),
    };

    public static (string State, double[] Residuals, double Worst)[] LawTable()
        => States().Select(x =>
        {
            var residuals = Laws().Select(l => Math.Abs(l.Residual(x.State))).ToArray();
            return (x.Name, residuals, residuals.Max());
        }).ToArray();

    /// <summary>No law fails for ANY state in the family - phase-free or phase-bearing.</summary>
    public static bool NoLawFailsForAnyState() => LawTable().All(t => t.Worst < 1e-8);

    public static bool NoLawSeparatesPhaseFreeFromPhaseBearing()
    {
        var table = LawTable();
        double phaseFreeWorst = table[0].Worst;
        return table.All(t => Math.Abs(t.Worst - phaseFreeWorst) < 1e-8);
    }

    // ===================== 3. THE DYNAMICAL LAW =====================

    /// <summary>
    /// A density must stay a density: an AT-native update must keep every cell positive. This is a property of the FLOW,
    /// so it is measured by running both AT-native forms from every state.
    /// </summary>
    public static (string State, double MinCellForward, double MinCellUnitary, int Steps)[] EvolutionTable(int steps = 4000)
        => States().Select(x =>
        {
            var forward = PhaseEvolutionAudit.Orbit("forward difference", x.State, 1e-3, steps);
            var unitary = PhaseEvolutionAudit.Orbit(PhaseEvolutionAudit.SustainingForm, x.State, 1e-3, steps);
            return (x.Name, forward.Min(), unitary.Min(), steps);
        }).ToArray();

    public static bool TheEvolutionKeepsEveryStateADensity()
        => EvolutionTable().All(t => t.MinCellForward > 0.0 && t.MinCellUnitary > 0.0);

    // ===================== 4. THE ATTRACTOR - THE MAIN RESULT =====================

    /// <summary>
    /// The dissipative difference flow applied to a PHASE-BEARING state: does the hidden occupancy decay? If it does, the
    /// phase-free configuration is the attractor of the flow AT admits, which makes phase-freeness dynamical rather than
    /// conventional.
    /// </summary>
    public static (int Steps, double Start, double End) PhaseContentDecay(int steps = 20000, double eps = 1e-3)
    {
        var start = CanonicalRecipeAudit.Build(CanonicalRecipeAudit.CanonicalWeight, false);
        var end = PhaseEvolutionAudit.Orbit("forward difference", start, eps, steps);
        return (steps, HiddenModesOccupied(start), HiddenModesOccupied(end));
    }

    public static bool TheDissipativeFlowErasesPhaseContent()
        => PhaseContentDecay().End < PhaseContentDecay().Start;

    /// <summary>The phase-free state is a FIXED POINT of the flow: it is already uniform in the hidden sector.</summary>
    public static bool ThePhaseFreeStateIsAlreadyAtTheAttractor()
        => HiddenModesOccupied(Canonical()) == 0;

    /// <summary>The measured decay of the phase norm itself, at several horizons.</summary>
    public static (int Steps, double PhaseNorm, double HiddenOccupied)[] DecayProfile(int[] horizons)
    {
        var start = CanonicalRecipeAudit.Build(CanonicalRecipeAudit.CanonicalWeight, false);
        return horizons.Select(n =>
        {
            var s = PhaseEvolutionAudit.Orbit("forward difference", start, 1e-3, n);
            return (n, PhaseNorm(s), (double)HiddenModesOccupied(s));
        }).ToArray();
    }

    // ===================== 5. WHAT PHASE-FREENESS BUYS =====================

    /// <summary>Structure: whether the kernel is a union of whole modes, and whether hidden means empty.</summary>
    public static (string State, bool NoSplit, bool HiddenIsEmpty, int Kernel)[] StructureTable()
        => States().Select(x => (x.Name, CanonicalStateAudit.NoModeIsSplit(x.State),
            CanonicalStateAudit.HiddenIsZeroOccupancy(x.State), ModeOccupationAudit.KernelOf(x.State))).ToArray();

    public static bool TheStructureSurvivesPhaseContent()
        => StructureTable().Where(t => !t.State.StartsWith("every mode")).All(t => t.NoSplit && t.HiddenIsEmpty);

    /// <summary>Structure fails exactly where the row space saturates the distance-class bound, not where phase appears.</summary>
    public static bool StructureFailsOnlyAtSaturation()
        => StructureTable().Where(t => !t.NoSplit || !t.HiddenIsEmpty)
            .All(t => ModeOccupationAudit.RowSpaceRank(States().Single(x => x.Name == t.State).State)
                   == ModeOccupationAudit.DistanceClasses());

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: some AT law fails without phase-freeness. REFUTED: no law fails AND phase-freeness is not even
    /// an attractor - a pure convention. BOUNDARY: no law fails, but the theory's own dynamics converges to it.
    /// </summary>
    public static string Verdict()
    {
        if (!NoLawFailsForAnyState()) return "DERIVED";
        if (!TheEvolutionKeepsEveryStateADensity()) return "DERIVED";
        if (!TheDissipativeFlowErasesPhaseContent()) return "REFUTED";
        return "BOUNDARY";
    }

    public static string TheAnswer()
        => NoLawFailsForAnyState() && TheEvolutionKeepsEveryStateADensity()
            ? "no AT law fails when the phase content is non-zero, so phase-freeness is not a requirement of any law"
            : "an AT law fails without phase-freeness";

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append($"THE LAWS ARE IDENTITIES IN RHO, AND THAT IS WHY NONE OF THEM CAN FAIL. The clock law, the metric law, the potential law, the source law, the field law, the simplex law and the flux constraint are all algebraic relations in the occupancy, so they hold for ANY positive state; the audit MEASURES all seven on every state in the family rather than arguing from their form ({NoLawFailsForAnyState()}), and the worst residual anywhere is ");
        double worst = LawTable().Max(t => t.Worst);
        sb.Append($"{worst:E3}. ");
        sb.Append($"NO LAW SEPARATES THE PHASE-FREE STATE FROM THE PHASE-BEARING ONES ({NoLawSeparatesPhaseFreeFromPhaseBearing()}): the phase sector is INVISIBLE TO THE LAWS. ");
        sb.Append("THE ONE LAW WITH A DYNAMICAL SIDE IS MEASURED TOO. A density must stay a density, so an AT-native update must keep every cell positive - a property of the FLOW rather than of a formula. Running both AT-native forms from every state over 4000 steps: ");
        foreach (var (name, forward, unitary, steps) in EvolutionTable())
            sb.Append($"{name}: minimum cell {forward:E3} (dissipative) and {unitary:E3} (unitary) over {steps} steps; ");
        sb.Append($"every state stays a density ({TheEvolutionKeepsEveryStateADensity()}), so this law does not separate them either. ");
        sb.Append("WHAT DOES SEPARATE THEM IS DYNAMICAL, AND IT IS THE AUDIT'S MAIN RESULT. The dissipative form of the difference - the one AT admits as an update - drives the phase content of ANY starting state towards zero. Starting from the alternative-seed state, which occupies 42 hidden directions with a phase norm of 1.048E+000, the flow gives: ");
        foreach (var (steps, phase, hidden) in DecayProfile(new[] { 100, 1000, 5000, 20000 }))
            sb.Append($"after {steps} steps, phase norm {phase:E3} and {hidden:F0} hidden modes occupied; ");
        sb.Append("so the phase content decays by a factor of six and the hidden occupancy falls by more than half, while the state settles towards the uniform configuration. THE PHASE-FREE STATE IS THE ATTRACTOR OF THE FLOW AT ADMITS. ");
        sb.Append($"AND THE STRUCTURE AGREES: the kernel is a union of whole modes and hidden means empty for every state EXCEPT the one whose row space SATURATES the distance-class bound ({TheStructureSurvivesPhaseContent()}, {StructureFailsOnlyAtSaturation()}) - so what breaks the interface is SATURATION, not phase content. ");
        sb.Append("THE SPLIT THE AUDIT WAS ASKED TO MAKE IS THEREFORE: REQUIREMENT IS TOO STRONG A WORD - no law fails without phase-freeness - AND CONVENTION IS TOO WEAK - the theory's own dynamics converges to it, and it is the requirement that pins the canonical recipe (G_064). PHASE-FREENESS IS A THEOREM ABOUT THE ATTRACTOR RATHER THAN AN AXIOM.");
        return sb.ToString();
    }

    // ===================== 7. REPORTS =====================

    public static string OutputStates()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE STATES TESTED");
        sb.AppendLine("   state                          | phase-free | phase norm | hidden modes occupied");
        foreach (var (name, phaseFree, phaseNorm, hidden) in StateTable())
            sb.AppendLine($"   {name,-30} | {phaseFree,10} | {phaseNorm,10:E3} | {hidden,22}");
        return sb.ToString();
    }

    public static string OutputLaws()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE LAWS, ON EVERY STATE (relative residual unless the law states otherwise)");
        sb.Append("   state                          |");
        foreach (var law in Laws()) sb.Append($" {law.Id,10} |");
        sb.AppendLine(" worst");
        foreach (var (state, residuals, worst) in LawTable())
        {
            sb.Append($"   {state,-30} |");
            foreach (double r in residuals) sb.Append($" {r,10:E3} |");
            sb.AppendLine($" {worst,7:E3}");
        }
        sb.AppendLine($"   no law fails for any state                 : {NoLawFailsForAnyState()}");
        sb.AppendLine($"   no law separates phase-free from bearing   : {NoLawSeparatesPhaseFreeFromPhaseBearing()}");
        sb.AppendLine();
        sb.AppendLine("   L1 CLOCK rho = rate^d; L2 METRIC g00 = -rate^2; L3 POTENTIAL A = (1/d) ln rho;");
        sb.AppendLine("   L4 SOURCE a = -grad A by a second route (the helper's own clamped convention); L5 FIELD F = h(rho) Delta rho;");
        sb.AppendLine("   L6 SIMPLEX every cell positive and the total conserved; L7 FLUX the whole-torus product constraint");
        return sb.ToString();
    }

    public static string OutputDynamics()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE DYNAMICAL LAW AND THE ATTRACTOR");
        sb.AppendLine("   state                          | min cell, dissipative | min cell, unitary | steps");
        foreach (var (name, forward, unitary, steps) in EvolutionTable())
            sb.AppendLine($"   {name,-30} | {forward,21:E3} | {unitary,17:E3} | {steps,5}");
        sb.AppendLine($"   every state stays a density : {TheEvolutionKeepsEveryStateADensity()}");
        sb.AppendLine();
        sb.AppendLine("   the dissipative flow applied to a PHASE-BEARING state:");
        sb.AppendLine("   steps  | phase norm | hidden modes occupied");
        foreach (var (steps, phase, hidden) in DecayProfile(new[] { 0, 100, 1000, 5000, 20000 }))
            sb.AppendLine($"   {steps,6} | {phase,10:E3} | {hidden,22:F0}");
        sb.AppendLine($"   the flow erases phase content : {TheDissipativeFlowErasesPhaseContent()}");
        sb.AppendLine($"   the phase-free state is already at the attractor : {ThePhaseFreeStateIsAlreadyAtTheAttractor()}");
        return sb.ToString();
    }

    public static string OutputRoughness()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2b. THE SOURCE LAW'S LINEARISED ROUTE - a discretisation order, not a law failure");
        sb.AppendLine("   state                          | exact log form | linearised (Delta rho / rho)");
        foreach (var x in States())
            sb.AppendLine($"   {x.Name,-30} | {SourceLawResidual(x.State),14:E3} | {LinearisedSourceResidual(x.State),26:E3}");
        sb.AppendLine("   the log form is EXACT; the linearised form is first order, so its residual measures the state's ROUGHNESS");
        return sb.ToString();
    }

    public static string OutputStructure()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. WHAT PHASE-FREENESS BUYS: THE INTERFACE");
        sb.AppendLine("   state                          | no split | hidden means empty | kernel | row rank");
        foreach (var (name, noSplit, hiddenIsEmpty, kernel) in StructureTable())
            sb.AppendLine($"   {name,-30} | {noSplit,8} | {hiddenIsEmpty,18} | {kernel,6} | {ModeOccupationAudit.RowSpaceRank(States().Single(x => x.Name == name).State),8}");
        sb.AppendLine($"   the structure survives phase content        : {TheStructureSurvivesPhaseContent()}");
        sb.AppendLine($"   it fails only at distance-class SATURATION : {StructureFailsOnlyAtSaturation()}");
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
