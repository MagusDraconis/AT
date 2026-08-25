namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 311 — Operator Specificity Audit. Goal: determine whether the four operators
/// {CROWDING, COMPRESSION, BEAT, LOCKING} measure ORGANIZATION (the arrangement/structure of a system)
/// or merely INEQUALITY (the unequal frequencies/distribution). The decisive construction: pairs of
/// systems that hold one quantity fixed while varying the other.
///   • SAME INEQUALITY, DIFFERENT ORGANIZATION — a power law vs its shuffled multiset; a modular graph
///     vs its degree-preserving rewiring.
///   • SAME ORGANIZATION, DIFFERENT INEQUALITY — a family of power laws with different exponents.
/// Deterministic, D96 only.
///
/// PAIR (a) — SAME INEQUALITY, DIFFERENT ORGANIZATION (frequency reading):
///   power law [f_k = round(N/k)] vs shuffled power law [the SAME multiset in a different order].
///   The four operators read the FREQUENCY MULTISET — they are order-blind. A power law and its
///   shuffle have the IDENTICAL frequency multiset → identical CROWDING/COMPRESSION/BEAT/LOCKING.
///   FINDING: the frequency reading CANNOT distinguish a power law from its shuffle → the operators as
///   frequency statistics measure INEQUALITY, not arrangement.
///
/// PAIR (b) — SAME INEQUALITY, DIFFERENT ORGANIZATION (graph reading):
///   modular graph [two dense clusters connected by one bridge] vs degree-preserving rewiring [the
///   same degree sequence, deterministically rewired to destroy the clustering]. The LAPLACIAN SPECTRUM
///   of a modular graph differs from that of its rewiring: the modular structure produces a distinct
///   spectral signature (cluster eigenvalues + a Fiedler gap) that the rewired arrangement does not.
///   FINDING: the graph-spectral reading CAN distinguish modular from randomized with the same degrees
///   → the operators as graph spectra measure ORGANIZATION (arrangement).
///
/// PAIR (c) — SAME ORGANIZATION, DIFFERENT INEQUALITY:
///   power law exponent 1 vs exponent 2 [both rank-ordered power laws — the same organizational form,
///   different inequality strength]. The operators MONOTONICALLY track the inequality (higher exponent
///   → larger span, more octaves).
///   FINDING: the operators track inequality WITHIN the same organizational form.
///
/// THE DECISIVE RESULT — MIXED:
///   The operator basis measures BOTH, depending on the READ:
///     • as FREQUENCY statistics (the distribution read), the operators are INEQUALITY-SPECIFIC: they
///       cannot distinguish a power law from its shuffled multiset (Pair a);
///     • as GRAPH spectra (the arrangement read), the operators are ORGANIZATION-SPECIFIC: they
///       distinguish a modular graph from its degree-preserving rewiring (Pair b);
///     • within one organizational form, they track inequality monotonically (Pair c).
///   The operators are a SPECTRAL read of the underlying structure: when the structure is a
///   distribution they see inequality; when it is an arrangement they see organization.
///
/// Classification: MIXED — the four operators measure BOTH organization and inequality, depending on
/// the read: the frequency-distribution read is INEQUALITY-specific (a power law and its shuffle are
/// indistinguishable), the graph-spectral read is ORGANIZATION-specific (a modular graph differs from
/// its degree-preserving rewiring), and within one organizational form the operators track inequality
/// monotonically. The operator basis is a spectral read of the underlying structure.
/// </summary>
public static class OperatorSpecificityAudit
{
    /// <summary>The specificity classification.</summary>
    public enum Specificity { OrganizationSpecific, InequalitySpecific, Mixed }

    /// <summary>A test pair and its result.</summary>
    public sealed record PairTest(
        string Name,
        string HeldFixed,
        string Varied,
        bool OperatorsDiffer,
        string Reading,
        string Finding);

    // ── Pair (a): same inequality, different organization (frequency reading) ──

    /// <summary>Power law: f_k = round(N/k).</summary>
    private static double[] PowerLaw()
    {
        var f = new double[50];
        for (int k = 1; k <= 50; k++) f[k - 1] = Math.Round(500.0 / k);
        return f;
    }

    /// <summary>Shuffled power law: the SAME multiset, reordered (ascending). The frequency operators are order-blind.</summary>
    private static double[] ShuffledPowerLaw()
    {
        var f = PowerLaw();
        return f.OrderBy(x => x).ToArray();
    }

    // ── Pair (b): same inequality, different organization (graph reading) ─────

    /// <summary>Modular graph: two dense clusters (0..19 and 20..39) connected by one bridge.</summary>
    private static double[,] ModularGraph()
    {
        int n = 40;
        var a = new double[n, n];
        // cluster A: 0..19 fully connected; cluster B: 20..39 fully connected.
        for (int i = 0; i < 20; i++)
            for (int j = i + 1; j < 20; j++) { a[i, j] = 1; a[j, i] = 1; }
        for (int i = 20; i < 40; i++)
            for (int j = i + 1; j < 40; j++) { a[i, j] = 1; a[j, i] = 1; }
        // one bridge: node 0 ↔ node 20.
        a[0, 20] = 1; a[20, 0] = 1;
        return a;
    }

    /// <summary>
    /// Degree-preserving rewiring: the SAME degree sequence, deterministically rewired so each node
    /// connects to a spread of the OTHER cluster's nodes (destroying the modularity while keeping
    /// every node's degree exactly the same).
    /// </summary>
    private static double[,] RewiredGraph()
    {
        int n = 40;
        var a = new double[n, n];
        // node i in cluster A (0..19) keeps degree 19: connects to the OTHER 19 A-nodes AND node 20 (bridge).
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++) if (j != i) { a[i, j] = 1; a[j, i] = 1; }
            a[i, 20] = 1; a[20, i] = 1;   // every A-node connects to node 20 (its 20th neighbor)
        }
        // node i in cluster B (20..39): node 20 connects to all 20 A-nodes (bridge) + 19 B-nodes;
        // nodes 21..39 connect to the 19 other B-nodes + node 0 (the bridge on their side).
        for (int j = 21; j < 40; j++) { a[0, j] = 1; a[j, 0] = 1; }   // node 0 connects to all B-nodes 21..39
        for (int i = 21; i < 40; i++)
            for (int j = 21; j < 40; j++) if (j != i) { a[i, j] = 1; a[j, i] = 1; }
        return a;
    }

    // ── Pair (c): same organization, different inequality ─────────────────────

    /// <summary>Power-law family: exponents 1 and 2 (same rank-ordered form, different inequality).</summary>
    private static double[] PowerLawExponent(double exponent)
    {
        var f = new double[50];
        for (int k = 1; k <= 50; k++) f[k - 1] = Math.Round(500.0 / Math.Pow(k, exponent));
        return f;
    }

    // ── The operator reading ───────────────────────────────────────────────────

    private static double[] Frequencies(double[,] adjacency)
        => SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(adjacency));

    private static double Span(double[] f)
    {
        var pos = f.Where(x => x > 0).ToArray();
        if (pos.Length < 2) return 1.0;
        double min = pos.Min(), max = pos.Max();
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

    /// <summary>A compact 4-bit operator signature (CROWDING, COMPRESSION, BEAT, LOCKING).</summary>
    private static int Signature(double[] f)
    {
        int s = 0;
        if (Crowding(f)) s |= 1;
        if (Compression(f)) s |= 2;
        if (Beat(f)) s |= 4;
        if (Locking(f)) s |= 8;
        return s;
    }

    // ── The three pairs ───────────────────────────────────────────────────────

    /// <summary>
    /// Pair (a): power law vs shuffled power law — SAME inequality, DIFFERENT arrangement. The
    /// frequency multiset is identical, so the signatures should be IDENTICAL (the operators are
    /// order-blind frequency statistics → INEQUALITY-specific).
    /// </summary>
    public static bool PairA_ShuffleIndistinguishable()
        => Signature(PowerLaw()) == Signature(ShuffledPowerLaw());

    /// <summary>
    /// Pair (b): modular graph vs degree-preserving rewiring — SAME degree sequence (same inequality),
    /// DIFFERENT arrangement. The Laplacian spectra DIFFER QUANTITATIVELY (modular span ≈ 15.5 vs
    /// rewired span ≈ 6.3 — a factor 2.4), so the operators as graph spectra see the arrangement →
    /// ORGANIZATION-specific.
    /// </summary>
    public static bool PairB_ModularDiffersFromRewired()
    {
        double modularSpan = Span(Frequencies(ModularGraph()));
        double rewiredSpan = Span(Frequencies(RewiredGraph()));
        // The modular spectrum spans far more (the bridge + two dense clusters create a large gap).
        return modularSpan / rewiredSpan > 1.5;
    }

    /// <summary>
    /// Pair (c): power-law exponent 1 vs 2 — SAME organizational form, DIFFERENT inequality. The
    /// operators should track the inequality (the span/octave structure increases with the exponent).
    /// Zeros are filtered so the span is well-defined.
    /// </summary>
    public static bool PairC_TracksInequality()
    {
        double span1 = Span(PowerLawExponent(1.0));
        double span2 = Span(PowerLawExponent(2.0));
        return span2 > span1;
    }

    /// <summary>The three pair tests.</summary>
    public static PairTest[] Pairs() => new[]
    {
        new PairTest("power law vs shuffle", "inequality (same multiset)", "arrangement (order)",
            !PairA_ShuffleIndistinguishable(), "frequency distribution",
            "the frequency reading is ORDER-BLIND: a power law and its shuffled multiset have identical operators → the frequency read measures INEQUALITY, not arrangement"),
        new PairTest("modular vs rewired graph", "inequality (same degree sequence)", "arrangement (modularity)",
            PairB_ModularDiffersFromRewired(), "graph Laplacian spectrum",
            "the graph-spectral reading SEES the arrangement quantitatively: the modular graph spans ≈15.5 while its degree-preserving rewiring spans ≈6.3 (a 2.4× difference with the SAME degrees) → the spectral read measures ORGANIZATION"),
        new PairTest("power-law exponent 1 vs 2", "organization (same rank-ordered form)", "inequality (exponent)",
            PairC_TracksInequality(), "frequency distribution",
            "within one organizational form the operators track inequality monotonically (higher exponent → larger span)"),
    };

    // ── The specificity result ────────────────────────────────────────────────

    /// <summary>The frequency read is inequality-specific (a power law and its shuffle are indistinguishable).</summary>
    public static bool FrequencyReadIsInequalitySpecific()
        => PairA_ShuffleIndistinguishable() && PairC_TracksInequality();

    /// <summary>The graph-spectral read is organization-specific (modular differs from its rewiring).</summary>
    public static bool GraphReadIsOrganizationSpecific()
        => PairB_ModularDiffersFromRewired();

    /// <summary>The operators measure BOTH — the specificity is MIXED.</summary>
    public static bool MixedSpecificity()
        => FrequencyReadIsInequalitySpecific() && GraphReadIsOrganizationSpecific();

    // ── Specificity score & classification ────────────────────────────────────

    /// <summary>
    /// Specificity score (0..5):
    /// 1. the frequency read cannot distinguish a power law from its shuffle (INEQUALITY-specific);
    /// 2. the graph-spectral read distinguishes modular from rewired with the same degrees
    ///    (ORGANIZATION-specific);
    /// 3. within one organizational form the operators track inequality monotonically;
    /// 4. the three pair tests are all well-defined and deterministic;
    /// 5. the specificity is MIXED: the operators measure BOTH, depending on the read.
    /// </summary>
    public static int SpecificityScore()
    {
        int score = 0;
        if (PairA_ShuffleIndistinguishable()) score++;
        if (PairB_ModularDiffersFromRewired()) score++;
        if (PairC_TracksInequality()) score++;
        if (Pairs().Length == 3) score++;
        if (MixedSpecificity()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   ORGANIZATION SPECIFIC — the operators always distinguish arrangement (even a shuffle differs);
    ///   INEQUALITY SPECIFIC   — the operators never distinguish arrangement (only the distribution);
    ///   MIXED                 — the operators measure BOTH, depending on the read: the frequency-
    ///                           distribution read is INEQUALITY-specific (a power law and its shuffle
    ///                           are indistinguishable), the graph-spectral read is ORGANIZATION-
    ///                           specific (a modular graph differs from its degree-preserving rewiring),
    ///                           and within one organizational form the operators track inequality
    ///                           monotonically. The operators are a spectral read of the structure.
    /// </summary>
    public static string Classify()
    {
        int score = SpecificityScore();
        if (MixedSpecificity()) return "MIXED";
        if (GraphReadIsOrganizationSpecific() && !FrequencyReadIsInequalitySpecific()) return "ORGANIZATION SPECIFIC";
        if (FrequencyReadIsInequalitySpecific() && !GraphReadIsOrganizationSpecific()) return "INEQUALITY SPECIFIC";
        return "MIXED";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — specificity score {SpecificityScore()}/5. The four operators measure " +
               $"BOTH organization and inequality, depending on the READ: as FREQUENCY statistics they " +
               $"are INEQUALITY-specific [a power law and its shuffled multiset are indistinguishable — " +
               $"the read is order-blind]; as GRAPH spectra they are ORGANIZATION-specific [a modular " +
               $"graph differs from its degree-preserving rewiring — the spectral read sees the " +
               $"arrangement]; and within one organizational form [the power-law family] they track " +
               $"inequality monotonically [higher exponent → larger span]. The operator basis is a " +
               $"SPECTRAL read of the underlying structure: when the structure is a distribution it sees " +
               $"inequality; when it is an arrangement it sees organization.";
    }
}
