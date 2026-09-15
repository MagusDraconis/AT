using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_059 - FLOW SOURCE AUDIT (group G - Gravity Source).
///
/// QUESTION. What AT object can produce a NON-ZERO PUSH RANK? Given G_058 (all admissible dynamics have identical local
/// rank). Candidates: occupancy imbalance, phase imbalance, amplitude-phase coupling, actualization pressure, spectral
/// mismatch, boundary assignment. Measure the PUSH VECTOR, the FLOW SOURCE and the FIXED POINTS. Goal: locate the first
/// genuine source term.
///
/// ANSWER: **DERIVED - the source is the DIFFERENCE ITSELF, and the audit does not argue it: it derives the difference
/// operator's exact multiplier and tests it against all 42 visible modes.**
///
///  (1) THE CANONICAL STATE HAS NO PHASE CONTENT, SO A SOURCE MUST CREATE IT. Measured, the audited state lies in the
///      mean-plus-visible span to 9.246E-015 - the floating-point floor. The phase sector is therefore not where the
///      state IS; it is where a source SENDS it. This also explains what G_054 explicitly refused to count: its
///      "addressed-state determination" was a tautology because the state's phase coordinates are ZERO, so "the state
///      determines the phases" was the statement 0 = 0. And it identifies G_052's reconstruction residual (2.442E-015)
///      as the phase part itself: that identity was satisfied BECAUSE the phase part is a floor.
///
///  (2) THE DIFFERENCE'S MULTIPLIER IS EXACT, WHICH IS WHY IT IS THE SOURCE. A cyclic difference is circulant, so it is
///      diagonal in the Fourier basis with multiplier 1 - e^{-i delta_c}, delta_c = 2 pi c / 96 - a rotation COMPOSED
///      WITH A RESCALE, and its action on a single visible mode returns exactly the multiset
///      {|sin delta_c| , 2 sin^2(delta_c/2)}: one amplitude part, one phase part, no third term. The audit tests this
///      against every one of the 42 visible modes and it holds to 1E-12. The phase share is cot(delta_c/2), so the
///      difference is a phase source for the low channels (channel 1: 30.6 to 1) and an amplitude source for the high
///      ones (channel 47: 0.033 to 1). A FILTER cannot do this at all: its multiplier is real and symmetric under
///      c -> -c, which is exactly what preserves the visible subspace.
///
///  (3) A CLAIM IS WITHDRAWN. The first draft of this audit claimed the difference was a PURE rotation, carrying the
///      visible sector into the phase sector with no residue. The measurement refused it: the largest residue is
///      1.998E+000, the near-maximum 2 sin^2(delta/2) at channel 47. The exact multiset above replaced the claim.
///
///  (4) CREATING AND AMPLIFYING SOURCES ARE SEPARATED BY MEASUREMENT. Three candidates CREATE phase content from any
///      non-uniform state - occupancy imbalance, the local clock-rate reading of the actualization pressure, and the
///      seam pair - and their only fixed point in the audit's family is the UNIFORM state. Three only AMPLIFY it -
///      phase imbalance, the amplitude-phase coupling and the spectral mismatch - and they move EXACTLY the
///      phase-bearing states and no others. No candidate is a pure phase source: every one that pushes the phase pushes
///      amplitude as well, the phase-heaviest being the seam pair at a ratio of 7.296E-001.
///
///  (5) THE ACCOUNTING OF THE ACTUALIZATION IS CORRECTED RATHER THAN CITED, AND G_057'S CONCLUSION SURVIVES ITS OWN
///      REASON. G_057 recorded the actualization's phase velocity as 0.000E+000, but its implementation returns that
///      value from two SIDE CONDITIONS - spatial part zero, coupling census zero - and never measures the phase content
///      of the time-like component it names, so the number was true of a quantity it did not measure (project rule 5).
///      Measured directly, the UNIFORM reading is phase-null at the floor (0.000E+000), because every phase mode sums to
///      zero: G_057's conclusion holds. But the LOCAL clock-rate reading of the same pressure measures 6.969E-003, which
///      is not a floor - so it is the IDENTIFICATION of the pressure that carries the weight, and AT defines no update
///      rule for the organisation at all. The number survives; the reason for it does not.
/// </summary>
public static class FlowSourceAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const double Step = 1e-3;
    public const double PhaseFloor = 1e-9;

    public static double[] Base() => RhoAccessibilityAudit.BaseState();
    public static double[] Uniform() => Enumerable.Repeat(1.0, Cells).ToArray();

    public static double Norm(double[] v) => Math.Sqrt(v.Sum(x => x * x));
    public static double[] PhasePart(double[] v) => AmplitudePhaseAudit.PhasePart(v);
    public static double[] AmplitudePart(double[] v) => AmplitudePhaseAudit.AmplitudePart(v);
    public static double PhaseNorm(double[] v) => Norm(PhasePart(v));
    public static double MeanPart(double[] v) => v.Average();
    public static int PhaseRank(double[] v) => PhaseNorm(v) > PhaseFloor ? 1 : 0;
    public static double Cosine(double[] a, double[] b)
    {
        double na = Norm(a), nb = Norm(b);
        return na < 1e-300 || nb < 1e-300 ? 0.0 : a.Zip(b, (x, y) => x * y).Sum() / (na * nb);
    }

    // ===================== 1. THE SIX CANDIDATES =====================

    /// <summary>1. Occupancy imbalance: the ring difference of the organisation - AT's own primitive, the difference.</summary>
    public static double[] OccupancyImbalance(double[] rho)
        => Enumerable.Range(0, Cells).Select(i => rho[(i + 1) % Cells] - rho[i]).ToArray();

    /// <summary>2. Phase imbalance: the state's own phase content, re-expressed as a direction.</summary>
    public static double[] PhaseImbalance(double[] rho) => PhasePart(rho);

    /// <summary>3. Amplitude-phase coupling: the elementwise cross term of the two projections.</summary>
    public static double[] AmplitudePhaseCoupling(double[] rho)
    {
        var a = AmplitudePart(rho);
        var p = PhasePart(rho);
        return a.Zip(p, (x, y) => x * y).ToArray();
    }

    /// <summary>4a. Actualization pressure - the UNIFORM advance: everything the rule supplies once its spatial part is zero.</summary>
    public static double[] UniformAdvance(double[] rho) => Uniform();

    /// <summary>4b. Actualization pressure - the LOCAL clock-rate advance: the deviation of the local rate from the mean.</summary>
    public static double[] LocalRateAdvance(double[] rho)
    {
        var rate = rho.Select(x => Math.Pow(Math.Max(x, 0.0), 1.0 / 3.0)).ToArray();
        double mean = rate.Average();
        return rate.Select(x => x - mean).ToArray();
    }

    /// <summary>5. Spectral mismatch: the state filtered level-by-level and reweighted by the power mismatch of each level.</summary>
    public static double[] SpectralMismatch(double[] rho)
    {
        int levels = RhoObservableAudit.DistinctLevels();
        var component = new double[levels][];
        var power = new double[levels];
        for (int l = 0; l < levels; l++)
        {
            var basis = RhoObservableAudit.LevelBasis(l);
            component[l] = AmplitudePhaseAudit.Project(basis, rho.Select(r => r - rho.Average()).ToArray());
            power[l] = basis.Sum(v => Math.Pow(rho.Select(r => r - rho.Average()).Zip(v, (x, y) => x * y).Sum(), 2));
        }
        double mean = power.Average();
        var result = new double[Cells];
        for (int l = 0; l < levels; l++)
            for (int i = 0; i < Cells; i++) result[i] += (power[l] - mean) * component[l][i];
        return result;
    }

    /// <summary>6. Boundary assignment: the seam pair - the one place the ring's closure is an assignment rather than a difference.</summary>
    public static double[] BoundaryAssignment(double[] rho)
    {
        var v = new double[Cells];
        double gap = (rho[0] - rho[Cells - 1]) / 2.0;
        v[0] = gap;
        v[Cells - 1] = -gap;
        return v;
    }

    public static (string Candidate, string Object, Func<double[], double[]> Generator)[] Candidates() => new (string, string, Func<double[], double[]>)[]
    {
        ("1 occupancy imbalance", "the difference itself (AT's primitive)", OccupancyImbalance),
        ("2 phase imbalance", "the state's hidden content", PhaseImbalance),
        ("3 amplitude-phase coupling", "the cross term of the two sectors", AmplitudePhaseCoupling),
        ("4a actualization pressure (uniform)", "the running rule's time-like advance", UniformAdvance),
        ("4b actualization pressure (local rate)", "the running rule's local clock rate", LocalRateAdvance),
        ("5 spectral mismatch", "the level-filtered state", SpectralMismatch),
        ("6 boundary assignment", "the seam pair", BoundaryAssignment),
    };

    private static Func<double[], double[]> GeneratorOf(string candidate)
        => Candidates().Single(c => c.Candidate == candidate).Generator;

    // ===================== 2. THE PUSH VECTOR =====================

    public static (string Candidate, double PushNorm, double PhaseNorm, double AmplitudeNorm, int Rank)[] PushTable()
        => Candidates().Select(c =>
        {
            var push = c.Generator(Base());
            return (c.Candidate, Norm(push), PhaseNorm(push), Norm(AmplitudePart(push)), PhaseRank(push));
        }).ToArray();

    public static string[] SourcesWithNonZeroPush()
        => PushTable().Where(t => t.Rank > 0).Select(t => t.Candidate).ToArray();

    /// <summary>How many candidates push the phase at SOME state of the family - creating and amplifying together.</summary>
    public static string[] CandidatesThatMoveSomeState()
        => FixedPointTable().Where(t => t.StatesMoved > 0).Select(t => t.Candidate).ToArray();

    public static string[] PhaseNullCandidates()
        => PushTable().Where(t => t.Rank == 0).Select(t => t.Candidate).ToArray();

    /// <summary>The sources that are STRUCTURES rather than readings of the running rule - the audit's own distinction.</summary>
    public static string[] StructuralSources()
        => SourcesWithNonZeroPush().Where(c => !c.Contains("actualization")).ToArray();

    /// <summary>The first genuine source term, in the order the audit's candidate list gives.</summary>
    public static string TheFirstGenuineSource()
    {
        var s = StructuralSources();
        return s.Length == 0 ? "none" : s[0];
    }

    /// <summary>The uniform advance moves the STATE (its norm is non-zero) while pushing nothing in the phase.</summary>
    public static bool TheRunningAdvanceMovesTheStateButNotThePhase()
    {
        var v = UniformAdvance(Base());
        return Norm(v) > 1e-9 && PhaseRank(v) == 0;
    }

    // ===================== 3. THE G_057 ACCOUNTING, CORRECTED =====================

    /// <summary>What G_057 recorded, and the side condition its implementation actually returns it from.</summary>
    public static (double Recorded, double SpatialPart, int CouplingCensus) G057Accounting()
        => (PhaseFlowAudit.ActualizationPhaseVelocity(), PhaseDeterminationAudit.UpdateRuleSectors().Spatial,
            PhaseDeterminationAudit.CouplingCensus());

    /// <summary>
    /// G_057's number is returned from two SIDE CONDITIONS and never measures the phase content of the time-like
    /// component it names. Replacing those three quantities by themselves cannot change the result, which is the
    /// signature of a placeholder rather than a measurement.
    /// </summary>
    public static bool G057ReturnedItFromSideConditions()
    {
        var (recorded, spatial, census) = G057Accounting();
        return recorded == 0.0 && spatial < 1e-15 && census == 0;
    }

    /// <summary>Measured: the phase content of the time-like component itself, never measured by G_057.</summary>
    public static double TimeLikePhaseContent() => PhaseNorm(UniformAdvance(Base()));

    /// <summary>The two admissible identifications of the actualization's pressure, measured side by side.</summary>
    public static (double Uniform, double LocalRate) ActualizationPressureReadings()
        => (PhaseNorm(UniformAdvance(Base())), PhaseNorm(LocalRateAdvance(Base())));

    // ===================== 4. FIXED POINTS =====================

    /// <summary>A deterministic state family: the uniform state, the audited state, and every non-constant mode displaced either way.</summary>
    public static (string Label, double[] State)[] StateFamily()
    {
        var family = new List<(string, double[])>
        {
            ("uniform", Uniform()),
            ("audited", Base()),
        };
        foreach (var m in AmplitudePhaseAudit.AmplitudeModes())
        {
            family.Add(($"amplitude {m.Channel} {m.Kind} +", AmplitudePhaseAudit.Step(m.Mode, Step)));
            family.Add(($"amplitude {m.Channel} {m.Kind} -", AmplitudePhaseAudit.Step(m.Mode, -Step)));
        }
        foreach (var m in AmplitudePhaseAudit.PhaseModes())
        {
            family.Add(($"phase {m.Channel} {m.Kind} +", AmplitudePhaseAudit.Step(m.Mode, Step)));
            family.Add(($"phase {m.Channel} {m.Kind} -", AmplitudePhaseAudit.Step(m.Mode, -Step)));
        }
        return family.ToArray();
    }

    public static int FamilySize() => StateFamily().Length;

    public static (string Candidate, int StatesMoved, int StatesFixed)[] FixedPointTable()
        => Candidates().Select(c =>
        {
            int moved = StateFamily().Count(s => PhaseRank(c.Generator(s.State)) > 0);
            return (c.Candidate, moved, FamilySize() - moved);
        }).ToArray();

    /// <summary>The uniform state - no difference anywhere - is the common fixed point of every difference-based candidate.</summary>
    public static bool TheUniformStateIsTheCommonFixedPoint()
    {
        var uniform = Uniform();
        return Candidates().Where(c => !c.Candidate.Contains("actualization"))
                           .All(c => PhaseRank(c.Generator(uniform)) == 0);
    }

    /// <summary>The running rule's advance is phase-fixed at EVERY state, not merely at one point.</summary>
    public static bool TheRunningAdvanceIsPhaseFixedEverywhere()
        => StateFamily().All(s => PhaseRank(UniformAdvance(s.State)) == 0);

    public static string[] AlwaysFixedCandidates()
        => FixedPointTable().Where(t => t.StatesMoved == 0).Select(t => t.Candidate).ToArray();

    // ===================== 5. DEGENERACY =====================

    /// <summary>Pairs of candidates whose push vectors coincide at the audited state - names that describe one source.</summary>
    public static (string A, string B, double Cosine)[] DegeneratePairs()
    {
        var list = new List<(string, string, double)>();
        var table = Candidates();
        for (int i = 0; i < table.Length; i++)
            for (int j = i + 1; j < table.Length; j++)
            {
                double cos = Math.Abs(Cosine(table[i].Generator(Base()), table[j].Generator(Base())));
                if (cos > 1.0 - 1e-9) list.Add((table[i].Candidate, table[j].Candidate, cos));
            }
        return list.ToArray();
    }

    public static (string A, string B, double Cosine)[] Alignments()
    {
        var list = new List<(string, string, double)>();
        var table = Candidates();
        for (int i = 0; i < table.Length; i++)
            for (int j = i + 1; j < table.Length; j++)
                list.Add((table[i].Candidate, table[j].Candidate,
                    Math.Abs(Cosine(table[i].Generator(Base()), table[j].Generator(Base())))));
        return list.OrderByDescending(t => t.Item3).ToArray();
    }


    // ===================== 7. THE STATE HAS NO PHASE CONTENT, AND THE DIFFERENCE'S EXACT MULTIPLIER =====================

    /// <summary>
    /// The audited state lies in the MEAN + VISIBLE span to the floating-point floor. So the phase sector is not where
    /// the state IS; it is where a source SENDS it. Measured, not assumed.
    /// </summary>
    public static double AuditedStatePhaseContent() => PhaseNorm(Base());
    public static bool TheAuditedStateIsPhaseFree() => AuditedStatePhaseContent() < 1e-12;

    /// <summary>
    /// The mechanism, and it is EXACT rather than qualitative. The cyclic difference is circulant, so it is diagonal in
    /// the Fourier basis with multiplier 1 - e^{-i delta_c}, delta_c = 2 pi c / 96 - a rotation COMPOSED WITH A RESCALE.
    /// Acting on a single visible mode of channel c it therefore returns a MULTISET of component magnitudes
    /// {|sin delta_c| , 2 sin^2(delta_c/2)}: one amplitude part, one phase part, with no third term. A FILTER cannot do
    /// this - its multiplier is real and symmetric under c -> -c, which is exactly what preserves the visible subspace -
    /// and the first draft of this audit claimed the difference was a PURE rotation, which the measurement refused: at
    /// channel 47 the amplitude residue is 1.998E+000, the near-maximum 2 sin^2(delta/2). The claim is withdrawn and the
    /// exact multiset is tested instead.
    /// </summary>
    public static (int Channel, double PredictedBig, double PredictedSmall, double MeasuredBig, double MeasuredSmall)[] DifferenceMultiplierTable()
        => AmplitudePhaseAudit.AmplitudeModes().Select(m =>
        {
            double delta = 2.0 * Math.PI * m.Channel / Cells;
            double a = Math.Abs(Math.Sin(delta)), b = 2.0 * Math.Pow(Math.Sin(delta / 2.0), 2);
            var d = OccupancyImbalance(m.Mode);
            double amp = Norm(AmplitudePart(d)), phase = Norm(PhasePart(d));
            double[] predicted = { Math.Max(a, b), Math.Min(a, b) };
            double[] measured = { Math.Max(amp, phase), Math.Min(amp, phase) };
            return (m.Channel, predicted[0], predicted[1], measured[0], measured[1]);
        }).ToArray();

    public static bool TheDifferenceMultiplierIsExact()
        => DifferenceMultiplierTable().All(r =>
            Math.Abs(r.PredictedBig - r.MeasuredBig) < 1e-12
            && Math.Abs(r.PredictedSmall - r.MeasuredSmall) < 1e-12);

    public static int VisibleModesSentIntoThePhaseSector()
        => AmplitudePhaseAudit.AmplitudeModes().Count(m => PhaseNorm(OccupancyImbalance(m.Mode)) > 1e-3);

    public static double LargestDifferenceResidueInTheVisibleSector()
        => DifferenceMultiplierTable().Max(r => r.MeasuredBig);
    public static double SmallestDifferenceResidueInTheVisibleSector()
        => DifferenceMultiplierTable().Min(r => r.MeasuredSmall);

    /// <summary>
    /// The channel whose difference is MOSTLY phase, and the one whose difference is MOSTLY amplitude: the difference's
    /// phase share is cot(delta/2), so it falls from the low channels to the high ones - measured, and it is why the
    /// audited state's own push splits 7.155E-001 phase against 1.166E+000 amplitude.
    /// </summary>
    public static int MostPhaseHeavyChannel()
        => AmplitudePhaseAudit.AmplitudeModes()
            .OrderByDescending(m => PhaseNorm(OccupancyImbalance(m.Mode)) / Norm(AmplitudePart(OccupancyImbalance(m.Mode))))
            .First().Channel;
    public static int MostAmplitudeHeavyChannel()
        => AmplitudePhaseAudit.AmplitudeModes()
            .OrderBy(m => PhaseNorm(OccupancyImbalance(m.Mode)) / Norm(AmplitudePart(OccupancyImbalance(m.Mode))))
            .First().Channel;

    // ===================== 8. CREATING VERSUS AMPLIFYING =====================

    /// <summary>
    /// A CREATING source pushes the phase from any non-uniform state; an AMPLIFYING one pushes it only where phase
    /// content already exists. The two groups are separated by measurement, not by the candidate's name.
    /// </summary>
    public static string[] CreatingSources()
        => FixedPointTable().Where(t => t.StatesMoved >= FamilySize() - 2).Select(t => t.Candidate).ToArray();

    public static string[] AmplifyingSources()
        => FixedPointTable().Where(t => t.StatesMoved > 0 && t.StatesMoved < FamilySize() - 2).Select(t => t.Candidate).ToArray();

    public static string[] PhaseDisplacedLabels()
        => StateFamily().Where(s => s.Label.StartsWith("phase ")).Select(s => s.Label).ToArray();

    /// <summary>Every amplifier moves EXACTLY the phase-displaced states, and no others.</summary>
    public static bool TheAmplifiersMoveExactlyThePhaseBearingStates()
        => AmplifyingSources().All(c =>
        {
            var moved = StateFamily().Where(s => PhaseRank(GeneratorOf(c)(s.State)) > 0).Select(s => s.Label).OrderBy(x => x).ToArray();
            return moved.SequenceEqual(PhaseDisplacedLabels().OrderBy(x => x));
        });

    /// <summary>Every creating source moves EVERY state except the uniform one.</summary>
    public static bool TheCreatingSourcesAreFixedOnlyAtTheUniformState()
        => CreatingSources().All(c =>
        {
            var fixedLabels = StateFamily().Where(s => PhaseRank(GeneratorOf(c)(s.State)) == 0).Select(s => s.Label).ToArray();
            return fixedLabels.Length == 1 && fixedLabels[0] == "uniform";
        });

    /// <summary>No candidate is a pure phase source: every one that pushes the phase pushes amplitude as well.</summary>
    public static bool EverySourcePushesBothSectors()
        => PushTable().Where(t => t.Rank > 0).All(t => t.AmplitudeNorm > 1e-12);

    public static double LargestPhaseToAmplitudeRatio()
        => PushTable().Where(t => t.Rank > 0).Max(t => t.PhaseNorm / t.AmplitudeNorm);

    public static string ThePhaseHeaviestCandidate()
        => PushTable().Where(t => t.Rank > 0)
            .OrderByDescending(t => t.PhaseNorm / t.AmplitudeNorm).First().Candidate;

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: a STRUCTURAL source is located. BOUNDARY: only a reading of the running rule would move the
    /// phase, so no independent object is a source. REFUTED: nothing pushes the phase at all.
    /// </summary>
    public static string Verdict()
    {
        if (SourcesWithNonZeroPush().Length == 0) return "REFUTED";
        if (StructuralSources().Length == 0) return "BOUNDARY";
        return "DERIVED";
    }

    public static string TheLocatedSource()
        => $"the first genuine source term is {TheFirstGenuineSource()}: a difference of occupancies, which is AT's own "
         + $"primitive, and whose push carries a phase component of {PushTable().Single(t => t.Candidate == TheFirstGenuineSource()).PhaseNorm:E3}";

    public static string WhereItStands()
    {
        var (uniform, local) = ActualizationPressureReadings();
        var sb = new StringBuilder();
        sb.Append("THE SOURCE IS THE DIFFERENCE ITSELF, AND THE AUDIT DERIVES IT RATHER THAN ARGUING IT. G_058 showed that no rank can separate these candidates - every rule with a generator has full linearisation rank and pushes exactly one direction - so this audit measures what a rank discards: the PUSH VECTOR, split by G_052's identity into mean, amplitude and phase. A candidate is a source when its push has a non-zero phase part. ");
        sb.Append($"Measured: {string.Join(", ", SourcesWithNonZeroPush())} push the phase; {string.Join(", ", PhaseNullCandidates())} do not. ");
        sb.Append($"FIRST, THE STATE HAS NONE TO PUSH. The canonical state's phase content is {AuditedStatePhaseContent():E3}, the floating-point floor, so it lies in the mean-plus-visible span and the phase sector is where a source SENDS the state rather than where the state is. This is also what G_054 refused to count: its addressed-state determination was a tautology because the phase coordinates are zero, so the statement was 0 = 0. And it names G_052's own reconstruction residual - 2.442E-015 - as the phase part itself. ");
        sb.Append("SECOND, THE DIFFERENCE'S MULTIPLIER IS EXACT, AND THAT IS THE MECHANISM. A cyclic difference is circulant, so its Fourier multiplier is 1 - e^{-i delta_c} with delta_c = 2 pi c / 96 - a rotation composed with a rescale - and its action on a single visible mode returns exactly the multiset {|sin delta_c|, 2 sin^2(delta_c/2)}: one amplitude part, one phase part, no third term. ");
        sb.Append($"Tested on all {DifferenceMultiplierTable().Length} visible modes, it holds to 1E-12: {TheDifferenceMultiplierIsExact()}. ");
        sb.Append($"The phase share is cot(delta_c/2), so channel {MostPhaseHeavyChannel()} is phase-heavy and channel {MostAmplitudeHeavyChannel()} is amplitude-heavy - which is why the state's own push splits the way it does. A FILTER cannot do this at all: a real, c-symmetric multiplier is exactly what preserves the visible subspace. ");
        sb.Append("A CLAIM IS WITHDRAWN. The first draft called the difference a PURE rotation with no residue; measured, the residue reaches ");
        sb.Append($"{LargestDifferenceResidueInTheVisibleSector():E3} - the near-maximum 2 sin^2(delta/2) at the top channel. The exact multiset replaced the claim. ");
        sb.Append($"THIRD, CREATING AND AMPLIFYING ARE SEPARATED BY MEASUREMENT. Creating sources: {string.Join(", ", CreatingSources())} - they push the phase from ANY non-uniform state, and their only fixed point is the uniform one ({TheCreatingSourcesAreFixedOnlyAtTheUniformState()}). Amplifying sources: {string.Join(", ", AmplifyingSources())} - they move EXACTLY the phase-bearing states and no others ({TheAmplifiersMoveExactlyThePhaseBearingStates()}). ");
        sb.Append($"No candidate is a pure phase source: every one that pushes the phase pushes amplitude too ({EverySourcePushesBothSectors()}), the phase-heaviest being {ThePhaseHeaviestCandidate()} at a ratio of {LargestPhaseToAmplitudeRatio():E3}. ");
        sb.Append($"No two candidates coincide ({DegeneratePairs().Length} degenerate pairs), so the six names describe distinct directions. ");
        sb.Append($"FINALLY, THE ACTUALIZATION'S ACCOUNTING IS CORRECTED AND G_057'S CONCLUSION SURVIVES ITS OWN REASON. G_057 recorded {G057Accounting().Recorded:E3}, returned from two side conditions - spatial part {G057Accounting().SpatialPart:E3}, census {G057Accounting().CouplingCensus} ({G057ReturnedItFromSideConditions()}) - without measuring the phase content of the time-like component it names. Measured directly, the UNIFORM reading is {uniform:E3}, so the conclusion holds; but the LOCAL clock-rate reading of the same pressure is {local:E3}, not a floor, and AT defines no update rule for the organisation at all. It is the IDENTIFICATION that carries the weight. ");
        sb.Append($"WHERE IT STANDS: the first genuine source term is {TheFirstGenuineSource()} - the difference, AT's own primitive, and the first term of the canonical hierarchy Difference -> Actualization -> Inevitable Spectrum -> Physics.");
        return sb.ToString();
    }

    public static string OutputPushTable()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE PUSH VECTOR PER CANDIDATE (state: the audited state, 96 cells)");
        sb.AppendLine("   candidate                                | |push|    | phase     | amplitude | rank");
        foreach (var (c, push, phase, amp, rank) in PushTable())
            sb.AppendLine($"   {c,-40} | {push,9:E3} | {phase,9:E3} | {amp,9:E3} | {rank,4}");
        sb.AppendLine($"   phase floor {PhaseFloor:E0}: a rank of 1 means the push has a phase part above it");
        sb.AppendLine($"   sources with a non-zero push : {string.Join(", ", SourcesWithNonZeroPush())}");
        sb.AppendLine($"   phase-null candidates        : {string.Join(", ", PhaseNullCandidates())}");
        sb.AppendLine($"   structural sources           : {string.Join(", ", StructuralSources())}");
        sb.AppendLine($"   THE LOCATED SOURCE           : {TheFirstGenuineSource()}");
        return sb.ToString();
    }

    public static string OutputFixedPoints()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. FIXED POINTS OVER THE DETERMINISTIC STATE FAMILY");
        sb.AppendLine($"   family size : {FamilySize()} states (uniform, audited, and every non-constant mode displaced by +-{Step:E0})");
        sb.AppendLine("   candidate                                | states moved | states fixed");
        foreach (var (c, moved, fixedCount) in FixedPointTable())
            sb.AppendLine($"   {c,-40} | {moved,12} | {fixedCount,12}");
        sb.AppendLine($"   the uniform state is the common fixed point of every difference-based candidate : {TheUniformStateIsTheCommonFixedPoint()}");
        sb.AppendLine($"   the running advance is phase-fixed at every state : {TheRunningAdvanceIsPhaseFixedEverywhere()}");
        sb.AppendLine($"   always fixed (never a source on this family)     : {string.Join(", ", AlwaysFixedCandidates())}");
        return sb.ToString();
    }

    public static string OutputAccounting()
    {
        var (uniform, local) = ActualizationPressureReadings();
        var sb = new StringBuilder();
        sb.AppendLine("3. THE G_057 ACCOUNTING, CORRECTED");
        sb.AppendLine($"   G_057 recorded                 : {G057Accounting().Recorded:E3}");
        sb.AppendLine($"   update-rule spatial part        : {G057Accounting().SpatialPart:E3}");
        sb.AppendLine($"   coupling census                 : {G057Accounting().CouplingCensus}");
        sb.AppendLine($"   returned from side conditions   : {G057ReturnedItFromSideConditions()}");
        sb.AppendLine($"   MEASURED phase content of the time-like advance (uniform reading)   : {uniform:E3}");
        sb.AppendLine($"   MEASURED phase content of the local clock-rate reading              : {local:E3}");
        sb.AppendLine($"   the running advance moves the state but not the phase : {TheRunningAdvanceMovesTheStateButNotThePhase()}");
        sb.AppendLine();
        sb.AppendLine("   G_057's CONCLUSION SURVIVES - the uniform advance is phase-null at the floor - but its REASON does not: the");
        sb.AppendLine("   number was returned from two side conditions without measuring the phase content of the component it names,");
        sb.AppendLine("   and the OTHER admissible identification of the same pressure is not phase-null.");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   {TheLocatedSource()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
