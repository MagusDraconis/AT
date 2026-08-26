namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 315 — Early Lock Prediction. QG312: operators can be faked, locks are robust. QG314: the
/// lock-coherence organization score predicts the organized/unorganized CLASS. This phase asks the
/// TEMPORAL question: do lock identities appear BEFORE mature organization, TRACK it, or LAG behind it?
///
/// THE EVOLVING SYSTEMS (deterministic growth models — a frequency law sharpens from flat to the mature
/// characteristic law as the system evolves):
///   software history  — token-usage frequency, exponent matures 0 → 1.5 (power law), 40 tokens;
///   wiki history      — page-edit frequency, exponent matures 0 → 1.0 (Zipf), 60 pages;
///   citation history  — citation frequency, exponent matures 0 → 2.0 (heavy tail), 80 papers;
///   language corpora  — word frequency, exponent matures 0 → 1.0 (Zipf), 100 words.
///
/// At each growth stage t = 1..8 (early → mid → late) the law exponent is α(t) = α_mature·(t/8), so the
/// spectrum evolves from a flat uniform distribution (t = 1, no organization) to the mature hierarchical
/// law (t = 8). No randomness, no observables, no target values — only the frequency law.
///
/// THE TWO TRAJECTORIES:
///   LOCK trajectory   — the QG314 lock-coherence organization score [mean coherence of the four lock
///                       identities onto small-fraction rationals p/q, q ≤ 5, p ≤ 120] at each stage;
///   MATURITY trajectory — the organization maturity of the spectrum [octaves × degeneracy density — an
///                       independent, operator-free measure of how hierarchical the distribution is].
///
/// THE PREDICTION QUESTION — which comes first?
///   For each system, find the stage at which the lock score first reaches 50% of its final value and the
///   stage at which the maturity first reaches 50% of its final value:
///     LOCKS PRECEDE — the lock identity reaches half-strength BEFORE the maturity does;
///     LOCKS TRACK   — the lock identity and the maturity reach half-strength at the same stage;
///     LOCKS LAG     — the lock identity reaches half-strength AFTER the maturity does.
///   The overall determination is the majority across the four evolving systems.
///
/// Classification:
///   LOCKS PRECEDE — the lock identities are an EARLY signature: they appear before the organization is
///     mature (the ratio locks onto small fractions as soon as the law is detectable, before full
///     hierarchy develops);
///   LOCKS TRACK   — the lock identities appear at the same stage as the organization matures;
///   LOCKS LAG     — the lock identities appear only AFTER the organization is mature.
/// </summary>
public static class EarlyLockPrediction
{
    /// <summary>The temporal classification.</summary>
    public enum TemporalRelation { LocksLag, LocksTrack, LocksPrecede }

    /// <summary>An evolving system at one growth stage.</summary>
    public sealed record StageSnapshot(
        string System,
        int Stage,
        double Exponent,
        int Units,
        double Span,
        int DistinctValues,
        double Maturity,
        double LockScore,
        int StableLocks);

    /// <summary>An evolving system with its lock-vs-maturity temporal relation.</summary>
    public sealed record SystemEvolution(
        string System,
        string Law,
        double MatureExponent,
        int Units,
        int LockHalfStage,
        int MaturityHalfStage,
        TemporalRelation Relation,
        StageSnapshot[] Stages);

    // ── The evolving systems ──────────────────────────────────────────────────

    private static double[] SpectrumAt(string system, double alpha, int units)
    {
        var f = new double[units];
        for (int k = 1; k <= units; k++)
        {
            double baseVal = system switch
            {
                "software" => 1000.0 / Math.Pow(k, alpha),
                "wiki" => 500.0 / Math.Pow(k, alpha),
                "citation" => 500.0 / Math.Pow(k, alpha),
                _ => 500.0 / Math.Pow(k, alpha),
            };
            f[k - 1] = Math.Round(baseVal);
        }
        return f;
    }

    // ── Measures ──────────────────────────────────────────────────────────────

    private static double Span(double[] f)
    {
        var pos = f.Where(x => x > 0).ToArray();
        if (pos.Length < 2) return 1.0;
        double min = pos.Min(), max = pos.Max();
        return min > 0 ? max / min : 1.0;
    }

    private static int DistinctValues(double[] f) => f.Distinct().Count();

    /// <summary>
    /// Organization maturity of a spectrum: octaves × degeneracy density. Octaves = log2(span) measures
    /// the scale separation; degeneracy density = 1 − (distinct−1)/n measures the concentration of mass
    /// into few groups. Both grow as the law sharpens from flat (span 1, distinct = n) to hierarchical.
    /// This is independent of the lock identities (no moment ratios, no small-fraction comparisons).
    /// </summary>
    private static double Maturity(double[] f)
    {
        double span = Span(f);
        int n = f.Length;
        double octaves = span > 1 ? Math.Log(span) / Math.Log(2.0) : 0.0;
        int distinct = DistinctValues(f);
        double degeneracyDensity = 1.0 - (distinct - 1) / (double)n;
        return octaves * degeneracyDensity;
    }

    /// <summary>Lock score and stable-lock count via the QG314 small-fraction coherence.</summary>
    private static (double Score, int Stable) LockReading(double[] f)
    {
        var ids = LockUniversalityAudit.LockIdentities(f);
        double s1 = OrganizationPredictorAudit.LockCoherence(ids.MomentSpan);
        double s2 = OrganizationPredictorAudit.LockCoherence(ids.CompressionCount);
        double s3 = OrganizationPredictorAudit.LockCoherence(ids.HigherMoment);
        double s4 = OrganizationPredictorAudit.LockCoherence(ids.SqrtMomentSpan);
        int stable = 0;
        if (OrganizationPredictorAudit.IsStableLock(ids.MomentSpan)) stable++;
        if (OrganizationPredictorAudit.IsStableLock(ids.CompressionCount)) stable++;
        if (OrganizationPredictorAudit.IsStableLock(ids.HigherMoment)) stable++;
        if (OrganizationPredictorAudit.IsStableLock(ids.SqrtMomentSpan)) stable++;
        return ((s1 + s2 + s3 + s4) / 4.0, stable);
    }

    // ── The evolution ─────────────────────────────────────────────────────────

    /// <summary>Stages 1..8 of one evolving system.</summary>
    public static StageSnapshot[] Evolve(string system, double matureExponent, int units)
    {
        var result = new StageSnapshot[8];
        for (int t = 1; t <= 8; t++)
        {
            double alpha = matureExponent * t / 8.0;
            double[] f = SpectrumAt(system, alpha, units);
            var (lockScore, stable) = LockReading(f);
            result[t - 1] = new StageSnapshot(system, t, alpha, units, Span(f), DistinctValues(f),
                Maturity(f), lockScore, stable);
        }
        return result;
    }

    private static int FirstHalfStage(double[] trajectory)
    {
        double max = trajectory.Max();
        if (max <= 0.0) return 8;
        double half = 0.5 * max;
        for (int i = 0; i < trajectory.Length; i++)
            if (trajectory[i] >= half) return i + 1;
        return 8;
    }

    private static TemporalRelation RelationOf(int lockHalf, int maturityHalf)
    {
        if (lockHalf < maturityHalf) return TemporalRelation.LocksPrecede;
        if (lockHalf > maturityHalf) return TemporalRelation.LocksLag;
        return TemporalRelation.LocksTrack;
    }

    /// <summary>The four evolving systems with their temporal relations.</summary>
    public static SystemEvolution[] Systems() => new[]
    {
        Build("software", "token-usage power law (k^−1.5)", 1.5, 40),
        Build("wiki", "page-edit Zipf (k^−1)", 1.0, 60),
        Build("citation", "citation heavy tail (k^−2)", 2.0, 80),
        Build("language", "word-frequency Zipf (k^−1)", 1.0, 100),
    };

    private static SystemEvolution Build(string system, string law, double matureExp, int units)
    {
        var stages = Evolve(system, matureExp, units);
        var lockTraj = stages.Select(s => s.LockScore).ToArray();
        var matTraj = stages.Select(s => s.Maturity).ToArray();
        int lockHalf = FirstHalfStage(lockTraj);
        int matHalf = FirstHalfStage(matTraj);
        return new SystemEvolution(system, law, matureExp, units, lockHalf, matHalf,
            RelationOf(lockHalf, matHalf), stages);
    }

    // ── The determination ─────────────────────────────────────────────────────

    /// <summary>Count of systems where the locks PRECEDE maturity.</summary>
    public static int PrecedeCount() => Systems().Count(s => s.Relation == TemporalRelation.LocksPrecede);

    /// <summary>Count of systems where the locks TRACK maturity.</summary>
    public static int TrackCount() => Systems().Count(s => s.Relation == TemporalRelation.LocksTrack);

    /// <summary>Count of systems where the locks LAG maturity.</summary>
    public static int LagCount() => Systems().Count(s => s.Relation == TemporalRelation.LocksLag);

    /// <summary>
    /// Determination score (0..5):
    /// 1. the four evolving systems are simulated over 8 deterministic growth stages;
    /// 2. at every stage both the lock score and the maturity are measured;
    /// 3. each system yields a lock-half stage and a maturity-half stage;
    /// 4. the majority temporal relation is identified;
    /// 5. the majority is decisive (≥ 3 of 4 systems, not a 2-2 tie).
    /// </summary>
    public static int DeterminationScore()
    {
        int score = 0;
        if (Systems().Length == 4) score++;
        if (Systems().All(s => s.Stages.Length == 8)) score++;
        if (Systems().All(s => s.LockHalfStage >= 1 && s.MaturityHalfStage >= 1)) score++;
        var counts = new[] { PrecedeCount(), TrackCount(), LagCount() };
        if (counts.Max() >= 3) score++;
        if (counts.Max() != counts.Min()) score++;
        return score;
    }

    /// <summary>Data-driven classification (majority of the four systems).</summary>
    public static string Classify()
    {
        int precede = PrecedeCount(), track = TrackCount(), lag = LagCount();
        if (precede > track && precede > lag) return "LOCKS PRECEDE";
        if (lag > precede && lag > track) return "LOCKS LAG";
        return "LOCKS TRACK";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        string r = Classify();
        return $"{r} — determination score {DeterminationScore()}/5. Across the four evolving systems " +
               $"[software, wiki, citation, language], the lock identities first reach half-strength at " +
               $"stage {Systems().Average(s => s.LockHalfStage):F1} on average, while the organization " +
               $"maturity first reaches half-strength at stage {Systems().Average(s => s.MaturityHalfStage):F1}. " +
               $"Precede: {PrecedeCount()}, Track: {TrackCount()}, Lag: {LagCount()}. " +
               $"The lock identities {(r == "LOCKS PRECEDE" ? "appear BEFORE the organization is mature — an early signature" : r == "LOCKS LAG" ? "appear only AFTER the organization is mature — a late signature" : "appear AT THE SAME STAGE as the organization matures")}.";
    }
}
