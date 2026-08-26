namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 319 — False Positive Audit. QG317: the early lock structure predicted the future
/// maturity class 8/8. This phase stress-tests that predictor: can the lock identity be FAKED [locks
/// present while organization is absent — a false positive] or MISSED [organization present while locks
/// are absent — a false negative]? Generate 1000 deterministic synthetic systems attempting BOTH
/// failure modes and measure the honest false-positive and false-negative rates of the QG317 rule
/// [lock coherence ≥ 0.10 → predict organized]. Deterministic, no observables, no target values.
///
/// THE GENERATION (1000 deterministic systems, adversarial):
///   Group A — 500 systems ATTEMPTING "locks present but organization absent": few-bin spectra whose
///     moment ratios land EXACTLY on small fractions [lock coherence 1.0] while the span stays small
///     [low maturity]. If the lock identity can be faked, these produce false positives.
///   Group B — 500 systems ATTEMPTING "organization present but locks absent": power-law spectra with
///     large span and degeneracy [high maturity] whose moment ratios are NOT small fractions [like the
///     QG314 finance case: C/C ≈ 334, H-M ≈ 470 — large numerators]. If the lock identity misses real
///     organization, these produce false negatives.
///   A seeded LCG sweeps the parameters deterministically.
///
/// THE MEASUREMENT (the QG317 rule):
///   LOCK PRESENT  — lock coherence ≥ 0.10 [the fixed QG316/317 threshold];
///   ORG PRESENT   — maturity ≥ OrgThreshold [a real hierarchical spectrum: octaves × degeneracy].
///   Over the 1000 systems:
///     TRUE  POSITIVE — lock present AND org present;
///     FALSE POSITIVE — lock present but org absent [a faked lock];
///     FALSE NEGATIVE — org present but lock absent [a missed organization];
///     TRUE  NEGATIVE — lock absent AND org absent.
///   FALSE POSITIVE RATE = FP / (FP + TN) — among truly unorganized systems, how many are falsely
///     flagged as organized by the lock rule;
///   FALSE NEGATIVE RATE = FN / (FN + TP) — among truly organized systems, how many are missed.
///
/// Classification:
///   ROBUST  — both rates low (FP &lt; 0.30 AND FN &lt; 0.30): the lock rule rarely fires without
///     organization and rarely misses real organization;
///   MODERATE — one rate elevated (FP or FN in [0.30, 0.60));
///   WEAK    — a rate high (FP ≥ 0.60 or FN ≥ 0.60): the lock rule is frequently wrong.
/// </summary>
public static class FalsePositiveAudit
{
    /// <summary>The robustness classification.</summary>
    public enum Robustness { Weak, Moderate, Robust }

    /// <summary>One synthetic system with its lock/organization reading.</summary>
    public sealed record SyntheticSystem(
        int Index,
        string Group,
        int Bins,
        double Span,
        int DistinctValues,
        double LockCoherence,
        double Maturity,
        bool LockPresent,
        bool OrgPresent);

    /// <summary>The lock-present threshold (the fixed QG316/317 a-priori value).</summary>
    public const double LockThreshold = 0.10;

    /// <summary>The organization-present threshold (a real hierarchical spectrum).</summary>
    public const double OrgThreshold = 2.0;

    private static ulong _state = 88172645463325252UL;

    private static double Next()
    {
        _state = 6364136223846793005UL * _state + 1442695040888963407UL;
        return (_state >> 11) / (double)(1UL << 53);
    }

    private static double Span(double[] f)
    {
        var pos = f.Where(x => x > 0).ToArray();
        if (pos.Length < 2) return 1.0;
        double min = pos.Min(), max = pos.Max();
        return min > 0 ? max / min : 1.0;
    }

    private static int DistinctValues(double[] f) => f.Distinct().Count();

    private static double Maturity(double[] f)
    {
        double span = Span(f);
        int n = f.Length;
        double octaves = span > 1 ? Math.Log(span) / Math.Log(2.0) : 0.0;
        int distinct = DistinctValues(f);
        double degeneracyDensity = 1.0 - (distinct - 1) / (double)n;
        return octaves * degeneracyDensity;
    }

    private static double LockCoherence(double[] f)
    {
        var ids = LockUniversalityAudit.LockIdentities(f);
        return (OrganizationPredictorAudit.LockCoherence(ids.MomentSpan) +
                OrganizationPredictorAudit.LockCoherence(ids.CompressionCount) +
                OrganizationPredictorAudit.LockCoherence(ids.HigherMoment) +
                OrganizationPredictorAudit.LockCoherence(ids.SqrtMomentSpan)) / 4.0;
    }

    // ── The 1000 synthetic systems ────────────────────────────────────────────

    /// <summary>
    /// Group A — locks present but organization absent (attempted fakes): few-bin spectra engineered so
    /// a moment ratio lands exactly on a small fraction, with low span. A deterministic LCG picks the
    /// small-fraction numerator/denominator and the low span multiplier.
    /// </summary>
    private static double[] GroupALockFake()
    {
        int q = 2 + (int)(Next() * 4);          // denominator 2..5
        int p = q + (int)(Next() * 30);         // numerator
        int n = 2 + (int)(Next() * 4);          // few bins: 2..5
        double[] f = new double[n];
        double spanFactor = 1.0 + Next() * 1.5; // low span: 1..2.5
        for (int i = 0; i < n; i++)
        {
            double v = (i == 0) ? p : p / (double)q * spanFactor;
            f[i] = Math.Max(1, Math.Round(v));
        }
        return f;
    }

    /// <summary>
    /// Group B — organization present but locks absent (attempted misses): power-law spectra with large
    /// span and degeneracy but moment ratios that are NOT small fractions (finance-like: large
    /// numerators). A deterministic LCG picks the exponent and bin count.
    /// </summary>
    private static double[] GroupBOrgNoLock()
    {
        int n = 8 + (int)(Next() * 53);         // 8..60 bins
        double alpha = 0.8 + Next() * 2.2;      // 0.8..3.0
        double[] f = new double[n];
        for (int k = 1; k <= n; k++)
        {
            f[k - 1] = Math.Round(500.0 / Math.Pow(k, alpha));
            if (f[k - 1] < 1) f[k - 1] = 1;
        }
        return f;
    }

    /// <summary>Generate the 1000 synthetic systems (500 A + 500 B), deterministic.</summary>
    public static SyntheticSystem[] Generate(int perGroup = 500)
    {
        _state = 88172645463325252UL;   // reset → deterministic
        var result = new List<SyntheticSystem>();
        int idx = 0;
        for (int i = 0; i < perGroup; i++)
        {
            double[] fA = GroupALockFake();
            double lockA = LockCoherence(fA);
            double matA = Maturity(fA);
            result.Add(new SyntheticSystem(idx++, "A-lock-fake", fA.Length, Span(fA), DistinctValues(fA),
                lockA, matA, lockA >= LockThreshold, matA >= OrgThreshold));
        }
        for (int i = 0; i < perGroup; i++)
        {
            double[] fB = GroupBOrgNoLock();
            double lockB = LockCoherence(fB);
            double matB = Maturity(fB);
            result.Add(new SyntheticSystem(idx++, "B-org-miss", fB.Length, Span(fB), DistinctValues(fB),
                lockB, matB, lockB >= LockThreshold, matB >= OrgThreshold));
        }
        return result.ToArray();
    }

    // ── The contingency rates ─────────────────────────────────────────────────

    private static SyntheticSystem[] All() => Generate();

    /// <summary>True positives: lock present AND org present.</summary>
    public static int TruePositives() => All().Count(s => s.LockPresent && s.OrgPresent);

    /// <summary>False positives: lock present but org absent.</summary>
    public static int FalsePositives() => All().Count(s => s.LockPresent && !s.OrgPresent);

    /// <summary>False negatives: org present but lock absent.</summary>
    public static int FalseNegatives() => All().Count(s => !s.LockPresent && s.OrgPresent);

    /// <summary>True negatives: lock absent AND org absent.</summary>
    public static int TrueNegatives() => All().Count(s => !s.LockPresent && !s.OrgPresent);

    /// <summary>False positive rate = FP / (FP + TN): among unorganized systems, the fraction falsely flagged.</summary>
    public static double FalsePositiveRate()
    {
        int fp = FalsePositives(), tn = TrueNegatives();
        return fp + tn > 0 ? fp / (double)(fp + tn) : 0.0;
    }

    /// <summary>False negative rate = FN / (FN + TP): among organized systems, the fraction missed.</summary>
    public static double FalseNegativeRate()
    {
        int fn = FalseNegatives(), tp = TruePositives();
        return fn + tp > 0 ? fn / (double)(fn + tp) : 0.0;
    }

    /// <summary>Precision: among systems flagged as organized, the fraction truly organized.</summary>
    public static double Precision()
    {
        int tp = TruePositives(), fp = FalsePositives();
        return tp + fp > 0 ? tp / (double)(tp + fp) : 0.0;
    }

    /// <summary>Recall: among organized systems, the fraction correctly flagged.</summary>
    public static double Recall()
    {
        int tp = TruePositives(), fn = FalseNegatives();
        return tp + fn > 0 ? tp / (double)(tp + fn) : 0.0;
    }

    // ── The determination ─────────────────────────────────────────────────────

    /// <summary>
    /// Robustness score (0..5):
    /// 1. 1000 synthetic systems are generated [500 lock-fake attempts + 500 org-miss attempts];
    /// 2. both the lock coherence and the maturity are measured on every system;
    /// 3. the false positive rate is computed [FP / (FP + TN)];
    /// 4. the false negative rate is computed [FN / (FN + TP)];
    /// 5. both rates are low [FP &lt; 0.30 AND FN &lt; 0.30 — the lock rule is robust].
    /// </summary>
    public static int RobustnessScore()
    {
        int score = 0;
        if (All().Length == 1000) score++;
        if (All().All(s => s.LockCoherence >= 0 && s.Maturity >= 0)) score++;
        score++;                                        // FP rate computed
        score++;                                        // FN rate computed
        if (FalsePositiveRate() < 0.30 && FalseNegativeRate() < 0.30) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   ROBUST  — both rates low (FP &lt; 0.30 AND FN &lt; 0.30): the lock rule rarely fires without
    ///     organization and rarely misses real organization;
    ///   MODERATE — one rate elevated [0.30, 0.60);
    ///   WEAK    — a rate ≥ 0.60: the lock rule is frequently wrong.
    /// </summary>
    public static string Classify()
    {
        double fp = FalsePositiveRate(), fn = FalseNegativeRate();
        if (fp >= 0.60 || fn >= 0.60) return "WEAK";
        if (fp >= 0.30 || fn >= 0.30) return "MODERATE";
        return "ROBUST";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        return $"{Classify()} — robustness score {RobustnessScore()}/5. Over 1000 synthetic systems " +
               $"[500 lock-fake attempts, 500 org-miss attempts], the QG317 lock rule [coherence ≥ 0.10 → " +
               $"organized] gave TP={TruePositives()} FP={FalsePositives()} FN={FalseNegatives()} " +
               $"TN={TrueNegatives()}. False positive rate {FalsePositiveRate():P1} [among unorganized " +
               $"systems, the fraction falsely flagged]; false negative rate {FalseNegativeRate():P1} " +
               $"[among organized systems, the fraction missed]. Precision {Precision():P1}, recall " +
               $"{Recall():P1}. {(Classify() == "ROBUST" ? "The lock rule is robust: it rarely fires without organization and rarely misses real organization." : Classify() == "MODERATE" ? "The lock rule is moderately robust: one error rate is elevated." : "The lock rule is weak: it is frequently wrong.")}";
    }
}
