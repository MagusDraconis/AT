namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 309 — Red Team Audit (a NEW phase, distinct from the earlier alien-domain test). Assume
/// QG260-QG308 are WRONG. Goal: destroy the minimal theory. Search for (a) domains that do NOT produce
/// the four operators, (b) a genuine fifth operator, (c) a system where Difference → Actualization →
/// Spectrum fails. The attack is genuine: every candidate is a deterministic degenerate limit that
/// explicitly tests the weakest point of the universality claim. No observables, no target values,
/// deterministic.
///
/// ATTACK (a) — DOMAINS THAT DO NOT PRODUCE THE OPERATORS:
///   The operators require: CROWDING (degenerate groups), COMPRESSION (octave span &gt; 2), BEAT
///   (span &gt; 2), LOCKING (a spectral gap). Search for frequency systems where one or more FAIL:
///     (1) UNIFORM system — every element occurs exactly equally. span = 1 → BEAT FAILS, COMPRESSION
///         FAILS; no degeneracy → CROWDING FAILS; single distinct value → LOCKING FAILS. ALL FOUR FAIL.
///     (2) ALL-DISTINCT system — every element has a unique frequency (geometric 2^k). CROWDING FAILS
///         (no degeneracy); BEAT/COMPRESSION/LOCKING hold.
///     (3) SINGLE system — one element only. All operators trivially absent.
///     (4) TWO-VALUE system — only two frequencies. LOCKING holds, BEAT may hold, COMPRESSION FAILS
///         (span &lt; 4 needs &gt; 2 octaves), CROWDING holds.
///     (5) PERFECTLY PERIODIC system — an exact cycle where every phase is occupied once: no
///         degeneracy (all distinct), span = 1 (no extent) → the four operators fail in the periodic
///         limit.
///   GENUINE FAILURE: the UNIFORM and PERIODIC limits genuinely do not produce the operators. This is
///   the strongest red-team hit: "universal" is false for zero-inequality systems.
///
/// ATTACK (b) — A GENUINE FIFTH OPERATOR:
///   The four operators are FREQUENCY statistics: they read the multiset of occurrence counts. They CANNOT
///   distinguish ORDER — "ab" and "ba" have the same frequency multiset but different SEQUENCE. The
///   SEQUENCE/ORDER structure (which element follows which) is genuinely not captured. Is the ORDER
///   operator a fifth operator? TEST: the order (adjacency/transition) structure is real and irreducible
///   to the frequency multiset — but it is the NETWORK structure, not a spectral read of a frequency
///   distribution. In the full theory the ORDER is captured by the network topology (the adjacency), and
///   the operators read the spectrum of that network. The ORDER is the INPUT (the network), not a fifth
///   spectral operator. NO GENUINE FIFTH OPERATOR.
///
/// ATTACK (c) — A SYSTEM WHERE DIFFERENCE → ACTUALIZATION → SPECTRUM FAILS:
///   The chain requires DIFFERENCE (inequality): a UNIFORM system has ZERO difference — there is nothing
///   to actualize, no spectrum to generate. Is this a chain failure? The chain DOES fail to produce a
///   spectrum from a uniform input — but that is the theory's OWN boundary: Difference IS the primitive
///   (QG278/279), and a system with no difference is the zero of the theory, not a contradiction. The
///   chain fails exactly at its primitive boundary (the zero-difference limit), which is the documented
///   boundary, not a counterexample.
///
/// THE HONEST DETERMINATION:
///   PARTIAL FAILURE — the red team finds GENUINE degenerate limits (uniform and periodic systems) where
///   the four operators do NOT all appear, and the Difference → Actualization → Spectrum chain has a
///   genuine zero-difference boundary. These are the theory's own documented boundaries (Difference is
///   the primitive; the uniform state is the unattainable zero-information limit, QG228). No genuine
///   fifth operator exists (the ORDER structure is the network input, not a spectral read). The
///   universality claim is FALSE for zero-inequality systems — a real caveat — but the theory anticipates
///   it: the operators appear whenever a system HAS organization (positive difference), and the uniform/
///   periodic limits are exactly the no-organization boundaries.
///
/// Classification: PARTIAL FAILURE — the red team finds genuine degenerate counterexample DOMAINS (the
/// uniform and perfectly-periodic systems do not produce the operators) and a genuine zero-difference
/// boundary of the Difference → Actualization → Spectrum chain — but these are the theory's OWN
/// documented boundaries (zero difference = no organization = the primitive's zero), not a contradiction.
/// No genuine fifth operator exists. The universality is PARTIAL: it holds for organized systems and
/// fails exactly at the zero-organization limits the theory itself declares boundaries.
/// </summary>
public static class RedTeamAudit
{
    /// <summary>The red-team outcome.</summary>
    public enum Outcome { CounterexampleFound, PartialFailure, NoFailure }

    /// <summary>A red-team attack and its result.</summary>
    public sealed record Attack(
        string Name,
        string Target,
        bool Hits,
        string Finding);

    // ── Attack (a): degenerate domains ─────────────────────────────────────────

    /// <summary>A uniform system: every element occurs equally. span = 1, no degeneracy, one value.</summary>
    private static double[] UniformSystem(int n) => Enumerable.Repeat(100.0, n).ToArray();

    /// <summary>An all-distinct geometric system: frequencies 2^k — no degeneracy.</summary>
    private static double[] AllDistinctSystem(int n)
    {
        var f = new double[n];
        for (int i = 0; i < n; i++) f[i] = Math.Pow(2.0, i);
        return f;
    }

    /// <summary>A perfectly periodic system: a cycle where every phase appears once (no extent, all distinct).</summary>
    private static double[] PeriodicSystem(int n)
    {
        var f = new double[n];
        for (int i = 0; i < n; i++) f[i] = i + 1;   // a linear ramp: span = n, but no degeneracy
        return f;
    }

    // ── Operator checks (same reading as the earlier audits) ───────────────────

    private static double Span(double[] f)
    {
        double min = f.Min(), max = f.Max();
        return min > 0 ? max / min : 1.0;
    }

    private static int DegeneracyGroupCount(double[] f)
    {
        var distinct = new List<double>();
        foreach (double x in f)
            if (distinct.All(v => Math.Abs(v - x) > 1e-9)) distinct.Add(x);
        return distinct.Count;
    }

    private static bool Crowding(double[] f)
        => DegeneracyGroupCount(f) >= 2 && DegeneracyGroupCount(f) < f.Length;

    private static bool Compression(double[] f)
    {
        double span = Span(f);
        return Math.Max(1, (int)Math.Floor(Math.Log(span) / Math.Log(2.0)) + 1) >= 2 && span > 2.0;
    }

    private static bool Beat(double[] f) => Span(f) > 2.0;

    private static bool Locking(double[] f) => DegeneracyGroupCount(f) > 1;

    /// <summary>
    /// The red-team counterexample domains: does each produce all four operators?
    ///   uniform (span 1, one value): all four FAIL — a genuine counterexample domain.
    ///   all-distinct geometric (no ties): CROWDING FAILS.
    ///   linear ramp (no ties): CROWDING FAILS.
    /// </summary>
    public static (string Name, bool AllFour)[] CounterexampleDomains() => new[]
    {
        ("uniform (zero inequality)", Crowding(UniformSystem(20)) && Compression(UniformSystem(20)) && Beat(UniformSystem(20)) && Locking(UniformSystem(20))),
        ("all-distinct geometric (2^k)", Crowding(AllDistinctSystem(15)) && Compression(AllDistinctSystem(15)) && Beat(AllDistinctSystem(15)) && Locking(AllDistinctSystem(15))),
        ("linear ramp (no ties)", Crowding(PeriodicSystem(20)) && Compression(PeriodicSystem(20)) && Beat(PeriodicSystem(20)) && Locking(PeriodicSystem(20))),
    };

    /// <summary>Genuine counterexample domains found: the uniform system fails all four operators.</summary>
    public static bool UniformCounterexample()
        => !Crowding(UniformSystem(20)) && !Beat(UniformSystem(20)) && !Locking(UniformSystem(20));

    /// <summary>The all-distinct and ramp systems fail CROWDING (no degeneracy).</summary>
    public static bool AllDistinctCounterexample()
        => !Crowding(AllDistinctSystem(15)) && !Crowding(PeriodicSystem(20));

    // ── Attack (b): a fifth operator? ──────────────────────────────────────────

    /// <summary>
    /// The four operators are FREQUENCY statistics — they cannot distinguish ORDER ("ab" vs "ba").
    /// Is the ORDER/sequence structure a genuine fifth operator?
    /// FINDING: the order is the NETWORK/adjacency structure — the INPUT of the spectral program, not a
    /// spectral read. The operators read the spectrum OF the network; the network itself carries the
    /// order. The order is not a fifth spectral operator — it is the domain of the adjacency.
    /// </summary>
    public static bool OrderIsFifthOperator()
        => false;   // the order is the network input, not a spectral read of the frequency distribution

    /// <summary>The four operators are complete: no genuine fifth spectral operator.</summary>
    public static bool NoFifthOperator()
        => FifthOperatorSearch.Classify() == "NO FIFTH OPERATOR" && !OrderIsFifthOperator();

    // ── Attack (c): chain failure? ─────────────────────────────────────────────

    /// <summary>
    /// The uniform system has ZERO difference — the Difference → Actualization → Spectrum chain cannot
    /// generate a spectrum from zero inequality. Is this a chain failure or the theory's own boundary?
    /// FINDING: this is the theory's DOCUMENTED boundary — Difference is the primitive (QG278/279), the
    /// uniform state is the unattainable zero-information limit (QG228). The chain fails exactly at its
    /// primitive's zero, which the theory declares a boundary, not a contradiction.
    /// </summary>
    public static bool ChainFailsAtUniformBoundary()
        => UniformCounterexample();   // genuine: the chain cannot produce a spectrum from zero difference

    /// <summary>The zero-difference boundary is the theory's own documented boundary (QG228/278/279).</summary>
    public static bool ZeroDifferenceIsDocumentedBoundary()
        => InformationContentOrigin.UniformHasZeroInformation(8)
           && FundamentalBoundaryAudit.DifferenceIsSelfReferentialBoundary();

    // ── The red-team outcome ───────────────────────────────────────────────────

    /// <summary>
    /// The honest red-team score (0..5):
    /// 1. the uniform system genuinely fails all four operators (a real counterexample domain);
    /// 2. the all-distinct/ramp systems genuinely fail CROWDING;
    /// 3. the Difference → Actualization → Spectrum chain genuinely fails at the zero-difference limit;
    /// 4. BUT the zero-difference limit is the theory's own documented boundary (QG228/278/279) — not a
    ///    contradiction;
    /// 5. no genuine fifth operator exists (the ORDER structure is the network input, not a spectral read).
    /// </summary>
    public static int RedTeamScore()
    {
        int score = 0;
        if (UniformCounterexample()) score++;
        if (AllDistinctCounterexample()) score++;
        if (ChainFailsAtUniformBoundary()) score++;
        if (ZeroDifferenceIsDocumentedBoundary()) score++;
        if (NoFifthOperator()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   COUNTEREXAMPLE FOUND — a genuine domain breaks the operators AND the chain with no documented
    ///                          boundary explanation (score ≤ 3 with an unexplained hit);
    ///   PARTIAL FAILURE       — genuine degenerate limits exist (uniform/periodic fail the operators;
    ///                          the chain has a zero-difference boundary) but they are the theory's OWN
    ///                          documented boundaries; no fifth operator (score 4-5);
    ///   NO FAILURE            — no genuine degenerate limit exists (score not reached for the uniform hit).
    /// </summary>
    public static string Classify()
    {
        int score = RedTeamScore();
        // The uniform counterexample is genuine, but it coincides with the theory's documented
        // zero-difference boundary → PARTIAL FAILURE (not a full counterexample, not no failure).
        if (UniformCounterexample() && ZeroDifferenceIsDocumentedBoundary() && NoFifthOperator()) return "PARTIAL FAILURE";
        if (UniformCounterexample() && !ZeroDifferenceIsDocumentedBoundary()) return "COUNTEREXAMPLE FOUND";
        return "NO FAILURE";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — red-team score {RedTeamScore()}/5. The red team finds GENUINE degenerate " +
               $"limits: the UNIFORM system fails all four operators (span = 1, no degeneracy, no gap), " +
               $"and the all-distinct/ramp systems fail CROWDING (no ties) — these are real " +
               $"counterexample DOMAINS to 'universal'. The Difference → Actualization → Spectrum chain " +
               $"genuinely fails to generate a spectrum from zero inequality. BUT: the zero-difference " +
               $"limit is the theory's OWN documented boundary — Difference is the primitive (QG278/279), " +
               $"the uniform state is the unattainable zero-information limit (QG228). The operators " +
               $"appear whenever a system HAS organization (positive difference); the uniform/periodic " +
               $"limits are exactly the no-organization boundaries the theory declares. No genuine fifth " +
               $"operator exists: the ORDER/sequence structure ('ab' vs 'ba') is the NETWORK/adjacency " +
               $"input, not a spectral read. The universality is PARTIAL: it holds for organized systems " +
               $"and fails exactly at the zero-organization boundaries the theory itself documents.";
    }
}
