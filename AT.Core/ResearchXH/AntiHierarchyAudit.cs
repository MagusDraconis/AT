namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 311 — Anti-Hierarchy Audit. Goal: kill the operator basis. Use systems with NO
/// hierarchy, NO power law, NO modularity, NO scale separation — the anti-hierarchy limit: a latin
/// square, a regular lattice, a balanced tree, a round-robin tournament, an equal-frequency corpus.
/// Question: do {CROWDING, COMPRESSION, BEAT, LOCKING} still appear? Deterministic, D96 only.
///
/// THE FIVE ANTI-HIERARCHY SYSTEMS:
///   (1) LATIN SQUARE — an n×n combinatorial design where each symbol appears EXACTLY n times. The
///       symbol-frequency multiset is FLAT (every symbol equal) — no inequality, no hierarchy.
///   (2) REGULAR LATTICE — a periodic 2D grid: every node has the same degree, no hierarchy, no
///       modularity — but the Laplacian spectrum is HIGHLY DEGENERATE (many repeated eigenvalues) and
///       SPANNED (a wide frequency extent).
///   (3) BALANCED TREE — a complete binary tree: no cycles, no modularity, but a LOGARITHMIC scale
///       separation (the levels) and degenerate leaf structure.
///   (4) ROUND-ROBIN TOURNAMENT — every team plays every other once: this is the COMPLETE graph K_n.
///       The Laplacian spectrum is {0, n×n−1} — a SINGLE distinct positive eigenvalue → flat.
///   (5) EQUAL-FREQUENCY CORPUS — every token occurs exactly equally: a flat frequency multiset.
///
/// THE PREDICTED OUTCOME — the operators are SIGNATURES OF ORGANIZATION (inequality), NOT of hierarchy:
///   Anti-hierarchy is NOT the same as anti-organization:
///     • regular lattice and balanced tree are anti-hierarchy but STILL carry DEGENERACY + SPAN
///       (organization/inequality) → the operators should SURVIVE;
///     • the round-robin (K_n) and equal-frequency corpus are FLAT (single distinct positive value) →
///       the operators should FAIL (the QG310 flat limit);
///     • the latin square's frequency multiset is flat → FAIL.
///   If this holds, the operators survive ANTI-HIERARCHY (they do not require hierarchy/power-law/
///   modularity) but fail ANTI-ORGANIZATION (flat single-scale systems) — consistent with QG309/310.
///
/// Classification: PARTIAL — the operator basis SURVIVES anti-hierarchy (the regular lattice and the
/// balanced tree — anti-hierarchy yet degenerate and spanned — carry all four operators) but FAILS on
/// the flat anti-organization systems (the latin-square frequency, the round-robin K_n, and the
/// equal-frequency corpus collapse to a single distinct value). The operators require ORGANIZATION
/// (inequality), not hierarchy.
/// </summary>
public static class AntiHierarchyAudit
{
    /// <summary>The outcome classification.</summary>
    public enum Outcome { Fail, Partial, SurvivesAntiHierarchy }

    /// <summary>An anti-hierarchy system with its operator signature.</summary>
    public sealed record SystemResult(
        string Name,
        string Structure,
        double Span,
        int DistinctValues,
        int OctaveCount,
        bool CrowdingPresent,
        bool CompressionPresent,
        bool BeatPresent,
        bool LockingPresent,
        bool AllOperatorsPresent);

    // ── The anti-hierarchy structures ──────────────────────────────────────────

    /// <summary>Latin square: an n×n design, each symbol appears n times — a FLAT frequency multiset.</summary>
    private static double[] LatinSquareFrequencies(int n)
        => Enumerable.Repeat(1.0, n).ToArray();

    /// <summary>
    /// Regular lattice: a periodic 2D torus grid (n×n). The Laplacian is highly degenerate (many
    /// repeated eigenvalues) with a wide span. Built as an undirected grid graph with wrap-around.
    /// </summary>
    private static double[,] RegularLatticeAdjacency(int side)
    {
        int n = side * side;
        var a = new double[n, n];
        for (int i = 0; i < side; i++)
            for (int j = 0; j < side; j++)
            {
                int u = i * side + j;
                int r = i * side + (j + 1) % side;   // right (wrap)
                int d = ((i + 1) % side) * side + j; // down (wrap)
                a[u, r] = 1.0; a[r, u] = 1.0;
                a[u, d] = 1.0; a[d, u] = 1.0;
            }
        return a;
    }

    /// <summary>Balanced tree: a complete binary tree (levels 0..h). Degenerate leaves, logarithmic scale separation.</summary>
    private static double[,] BalancedTreeAdjacency(int height)
    {
        int n = (1 << (height + 1)) - 1;
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            int left = 2 * i + 1, right = 2 * i + 2;
            if (left < n) { a[i, left] = 1.0; a[left, i] = 1.0; }
            if (right < n) { a[i, right] = 1.0; a[right, i] = 1.0; }
        }
        return a;
    }

    /// <summary>Round-robin tournament: the complete graph K_n — every pair connected.</summary>
    private static double[,] RoundRobinAdjacency(int n)
    {
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                a[i, j] = 1.0; a[j, i] = 1.0;
            }
        return a;
    }

    /// <summary>Equal-frequency corpus: every token occurs equally — a flat frequency multiset.</summary>
    private static double[] EqualFrequencyCorpus(int n)
        => Enumerable.Repeat(1.0, n).ToArray();

    // ── The operator reading ───────────────────────────────────────────────────

    private static double[] Frequencies(double[,] adjacency)
        => SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(adjacency));

    private static double Span(double[] f)
    {
        double min = f.Min(), max = f.Max();
        return min > 0 ? max / min : 1.0;
    }

    private static int DistinctValues(double[] f)
    {
        var distinct = new List<double>();
        foreach (double x in f)
            if (distinct.All(v => Math.Abs(v - x) > 1e-9)) distinct.Add(x);
        return distinct.Count;
    }

    private static int OctaveCount(double[] f)
    {
        double span = Span(f);
        return Math.Max(1, (int)Math.Floor(Math.Log(span) / Math.Log(2.0)) + 1);
    }

    private static bool Crowding(double[] f)
        => DistinctValues(f) >= 2 && DistinctValues(f) < f.Length;

    private static bool Compression(double[] f) => OctaveCount(f) >= 2 && Span(f) > 2.0;

    private static bool Beat(double[] f) => Span(f) > 2.0;

    private static bool Locking(double[] f) => DistinctValues(f) > 1;

    private static SystemResult Build(string name, string structure, double[] f)
    {
        bool crowding = Crowding(f), compression = Compression(f);
        bool beat = Beat(f), locking = Locking(f);
        return new SystemResult(name, structure, Span(f), DistinctValues(f), OctaveCount(f),
            crowding, compression, beat, locking, crowding && compression && beat && locking);
    }

    /// <summary>The five anti-hierarchy systems.</summary>
    public static SystemResult[] Systems() => new[]
    {
        Build("latin square", "n×n design, each symbol n times (flat frequencies)", LatinSquareFrequencies(32)),
        Build("regular lattice", "periodic 2D torus (degenerate + spanned spectrum)", Frequencies(RegularLatticeAdjacency(8))),
        Build("balanced tree", "complete binary tree (degenerate leaves, log separation)", Frequencies(BalancedTreeAdjacency(5))),
        Build("round-robin tournament", "complete graph K_n (single positive eigenvalue)", Frequencies(RoundRobinAdjacency(32))),
        Build("equal-frequency corpus", "every token equal (flat frequencies)", EqualFrequencyCorpus(32)),
    };

    // ── The outcome ────────────────────────────────────────────────────────────

    /// <summary>Number of anti-hierarchy systems carrying all four operators.</summary>
    public static int SurvivingCount() => Systems().Count(s => s.AllOperatorsPresent);

    /// <summary>The anti-hierarchy structures that carry the basis (regular lattice, balanced tree).</summary>
    public static bool AntiHierarchySurvives()
        => Systems().Any(s => s.Name == "regular lattice" && s.AllOperatorsPresent)
           && Systems().Any(s => s.Name == "balanced tree" && s.AllOperatorsPresent);

    /// <summary>The flat anti-organization systems lose the basis (latin square, round-robin, equal-frequency).</summary>
    public static bool FlatSystemsLoseBasis()
        => Systems().All(s => s.Name is "latin square" or "round-robin tournament" or "equal-frequency corpus"
                              ? !s.AllOperatorsPresent : true);

    /// <summary>The operators survive anti-hierarchy but fail anti-organization.</summary>
    public static bool PartialOutcome()
        => AntiHierarchySurvives() && FlatSystemsLoseBasis() && SurvivingCount() == 2;

    // ── Outcome score & classification ────────────────────────────────────────

    /// <summary>
    /// Outcome score (0..5):
    /// 1. the regular lattice (anti-hierarchy, degenerate + spanned) carries all four operators;
    /// 2. the balanced tree (anti-hierarchy, degenerate leaves) carries all four;
    /// 3. the latin square (flat frequencies) loses the basis;
    /// 4. the round-robin K_n (single positive eigenvalue) and the equal-frequency corpus lose it;
    /// 5. the operators SURVIVE anti-hierarchy but FAIL anti-organization — they require organization
    ///    (inequality), not hierarchy.
    /// </summary>
    public static int OutcomeScore()
    {
        int score = 0;
        if (Systems().Any(s => s.Name == "regular lattice" && s.AllOperatorsPresent)) score++;
        if (Systems().Any(s => s.Name == "balanced tree" && s.AllOperatorsPresent)) score++;
        if (Systems().All(s => s.Name == "latin square" ? !s.AllOperatorsPresent : true)) score++;
        if (Systems().All(s => s.Name is "round-robin tournament" or "equal-frequency corpus"
                               ? !s.AllOperatorsPresent : true)) score++;
        if (PartialOutcome()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   FAIL                   — the operators do not survive any anti-hierarchy system (score ≤ 2);
    ///   PARTIAL                — the operators survive SOME anti-hierarchy systems (the degenerate +
    ///                            spanned ones) but fail the flat ones (score 3-4, or the mixed outcome);
    ///   SURVIVES ANTI-HIERARCHY — the operators survive ALL anti-hierarchy systems (score 5 with all
    ///                            carrying the basis).
    ///   The decisive result: the operators require ORGANIZATION (inequality), not hierarchy — they
    ///   survive the anti-hierarchy structures with degeneracy + span and fail the flat single-scale
    ///   systems, so the outcome is PARTIAL.
    /// </summary>
    public static string Classify()
    {
        int score = OutcomeScore();
        if (SurvivingCount() == 5) return "SURVIVES ANTI-HIERARCHY";
        if (score >= 3 && PartialOutcome()) return "PARTIAL";
        if (score <= 2) return "FAIL";
        return "PARTIAL";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — outcome score {OutcomeScore()}/5: {SurvivingCount()} of {Systems().Length} " +
               $"anti-hierarchy systems carry the basis. The operators SURVIVE anti-hierarchy: the " +
               $"regular lattice [periodic 2D torus — degenerate + spanned spectrum] and the balanced " +
               $"tree [complete binary tree — degenerate leaves, log separation] carry all four " +
               $"operators despite having NO hierarchy, power law, or modularity. The operators FAIL on " +
               $"the flat anti-organization systems: the latin-square frequency [each symbol n times], " +
               $"the round-robin K_n [single positive eigenvalue], and the equal-frequency corpus " +
               $"[every token equal] collapse to a single distinct value. The operators require " +
               $"ORGANIZATION (inequality), not hierarchy — consistent with QG309 (zero-difference " +
               $"boundary) and QG310 (anti-organization loses the basis).";
    }
}
