namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 111 — Multi-objective network selection. QG109 (stability) and QG110 (information capacity) each
/// gave PARTIAL SELECTION — no single criterion singles out the physical network. This phase asks: can
/// SIMULTANEOUS optimization of five objectives select a unique network class?
///
/// The five objectives (all to be MAXIMIZED):
///   1. STABILITY            — family-structure persistence under link removal (QG109);
///   2. MEMORY               — effective active modes e^H of the spectrum (QG110);
///   3. INFORMATION FLOW     — log spanning-tree count / N (QG110);
///   4. CAUSAL DEPTH         — graph diameter (QG110);
///   5. ACTUALIZATION EFF.   — inverse counting-measure variance (concentrated actualization density, QG89/109).
///
/// Method (computational, fully deterministic): compute the five objectives for every network of the
/// 77-network ensemble, build the PARETO-OPTIMAL FRONT (non-dominated networks), and test whether the front
/// selects a unique network CLASS (all front members in one class) or narrows only partially.
///
/// Answer (determined by the computed data): NO SELECTION — the Pareto front spans ALL four classes (37 of 77
/// networks; ER 78% of the front, matching its 78% share of the ensemble), so simultaneous optimization of the
/// five CONFLICTING objectives does NOT single out a unique network class. The objectives trade off against
/// each other (flow/efficiency prefer ER random, depth/memory/stability prefer the causal grid), so the
/// multi-objective optimum is a large front spanning every class — adding more objectives does not rescue the
/// selection; it widens the ambiguity. Classification: NO SELECTION (no class-level preference). No new
/// primitives added here.
/// </summary>
public static class MultiObjectiveSelection
{
    // ── Objectives ─────────────────────────────────────────────────────────────────

    /// <summary>1. Stability: family-structure persistence under 10% link removal (higher = better).</summary>
    public static double Stability(double[,] adjacency)
        => PhysicalNetworkSelection.RobustnessFraction(adjacency, 0.10);

    /// <summary>2. Memory: effective active modes e^H of the spectrum (higher = better).</summary>
    public static double Memory(double[,] adjacency)
        => NetworkInformationSelection.MemoryCapacity(adjacency);

    /// <summary>3. Information flow: log spanning-tree count / N (higher = better).</summary>
    public static double InformationFlow(double[,] adjacency)
        => NetworkInformationSelection.InformationFlow(adjacency);

    /// <summary>4. Causal depth: graph diameter (higher = better).</summary>
    public static double CausalDepth(double[,] adjacency)
        => NetworkInformationSelection.CausalDepth(adjacency);

    /// <summary>
    /// 5. Actualization efficiency: inverse counting-measure variance (1/(1+var)) — a concentrated
    /// actualization-rate density (QG89/109) is efficient (higher = better, range (0,1]).
    /// </summary>
    public static double ActualizationEfficiency(double[,] adjacency)
    {
        double var = PhysicalNetworkSelection.ActualizationVariance(adjacency);
        return 1.0 / (1.0 + var);
    }

    /// <summary>The five objective values of a network.</summary>
    public static (double stability, double memory, double flow, double depth, double efficiency)
        Objectives(double[,] adjacency)
        => (Stability(adjacency), Memory(adjacency), InformationFlow(adjacency), CausalDepth(adjacency), ActualizationEfficiency(adjacency));

    /// <summary>Per-network objective vector of the whole ensemble.</summary>
    public static (string name, double stability, double memory, double flow, double depth, double efficiency)[]
        EnsembleObjectives()
        => FamilyCountStatistics.BuildEnsemble()
            .Select(e => (e.name,
                          Stability(e.adjacency),
                          Memory(e.adjacency),
                          InformationFlow(e.adjacency),
                          CausalDepth(e.adjacency),
                          ActualizationEfficiency(e.adjacency)))
            .ToArray();

    // ── Pareto front ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Does candidate A dominate B (≥ on ALL objectives, &gt; on at least one)? All objectives are
    /// "higher is better".
    /// </summary>
    public static bool Dominates(
        (double stability, double memory, double flow, double depth, double efficiency) a,
        (double stability, double memory, double flow, double depth, double efficiency) b)
    {
        bool ge = a.stability >= b.stability && a.memory >= b.memory && a.flow >= b.flow
                  && a.depth >= b.depth && a.efficiency >= b.efficiency;
        bool gt = a.stability > b.stability || a.memory > b.memory || a.flow > b.flow
                  || a.depth > b.depth || a.efficiency > b.efficiency;
        return ge && gt;
    }

    /// <summary>
    /// Pareto-optimal front: the indices of the non-dominated ensemble members. A member is on the front if
    /// NO other member dominates it.
    /// </summary>
    public static int[] ParetoFront(
        (string name, double stability, double memory, double flow, double depth, double efficiency)[] members)
    {
        var front = new List<int>();
        for (int i = 0; i < members.Length; i++)
        {
            bool dominated = false;
            for (int j = 0; j < members.Length; j++)
            {
                if (i == j) continue;
                (double s, double m, double f, double d, double e) a = (members[j].stability, members[j].memory, members[j].flow, members[j].depth, members[j].efficiency);
                (double s, double m, double f, double d, double e) b = (members[i].stability, members[i].memory, members[i].flow, members[i].depth, members[i].efficiency);
                if (Dominates(a, b)) { dominated = true; break; }
            }
            if (!dominated) front.Add(i);
        }
        return front.ToArray();
    }

    /// <summary>Class (name prefix) of an ensemble member.</summary>
    public static string ClassOf(string name)
        => name.StartsWith("grid", StringComparison.Ordinal) ? "grid"
         : name.StartsWith("threshold", StringComparison.Ordinal) ? "threshold"
         : name.StartsWith("perturbed", StringComparison.Ordinal) ? "perturbed"
         : "ER";

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   UNIQUE SELECTION  — the Pareto front contains networks of a SINGLE class only;
    ///   PARTIAL SELECTION — the front narrows to exactly TWO classes (a preferred subset);
    ///   NO SELECTION      — the front spans THREE OR MORE classes: the objectives conflict and the multi-
    ///                       objective optimum spans every class (the concrete case).
    /// </summary>
    public static string Classify()
    {
        var members = EnsembleObjectives();
        int[] front = ParetoFront(members);

        var classes = front.Select(i => ClassOf(members[i].name)).Distinct().ToList();
        if (classes.Count == 1) return "UNIQUE SELECTION";
        if (classes.Count == 2) return "PARTIAL SELECTION";

        return "NO SELECTION";
    }
}
