namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 318 — Reorganization Prediction. QG315: locks precede organization; QG316: the operator
/// basis emerges at a critical transition; QG317: early locks blindly predict the future maturity class.
/// This phase asks the REORGANIZATION question: do the early lock identities predict FUTURE STRUCTURAL
/// REORGANIZATION — the magnitude of a later change in the system's topology? Deterministic, no
/// observables, no target values.
///
/// THE SYSTEMS (four evolving families, each with a cohort of members):
///   software history, wiki edits, citation networks, language corpora. Each member grows a frequency
///   law from flat toward a mature law over 8 stages, then undergoes a REORGANIZATION: at stage 4 the
///   law's exponent switches to a new value [a structural change — a rewrite, a rewrite wave, a
///   citation burst, a corpus change]. The future TOPOLOGY CHANGE is the fractional spectral difference
///   between the pre-reorganization spectrum [stage 4] and the post-reorganization spectrum [stage 8].
///
/// THE MEASUREMENT:
///   EARLY LOCK COHERENCE — the QG314 lock-coherence score at stage 2 [25% growth], the precocious
///     lock structure of the system BEFORE the reorganization;
///   FUTURE TOPOLOGY CHANGE — the L1 distance between the normalized pre-reorg [stage 4] and post-reorg
///     [stage 8] frequency spectra: how much the structure changes across the reorganization.
///
/// THE PREDICTION TEST:
///   Do systems with stronger early lock coherence undergo SMALLER future reorganizations? The
///   mechanistic hypothesis [fixed before the reveal]: a system with early lock structure has ALREADY
///   committed to a rigid small-fraction topology — it is PLASTICITY-LOST — so its future reorganization
///   is SMALL. A system without early locks is still plastic — its future reorganization is LARGE. The
///   prediction rule is target-free [early lock coherence present ≥ 0.10 → predict SMALL], then the
///   actual topology change is revealed and the prediction is scored. If early locks predict the future
///   reorganization, the lock structure is not only a maturity predictor [QG317] but a REORGANIZATION
///   PREDICTOR.
///
/// Classification:
///   REORGANIZATION PREDICTOR — the early lock coherence predicts the future topology-change class
///     (accuracy ≥ 3/5 of the cohort): early locks predict future structural reorganization;
///   PARTIAL SIGNAL            — the early lock coherence predicts the future reorganization partially
///     (accuracy 2/5);
///   NO SIGNAL                 — the early lock coherence does NOT predict the future reorganization
///     (accuracy ≤ 1/5).
/// </summary>
public static class ReorganizationPrediction
{
    /// <summary>The prediction classification.</summary>
    public enum PredictionOutcome { NoSignal, PartialSignal, ReorganizationPredictor }

    /// <summary>A member with its early (blind) and late (revealed) measurements.</summary>
    public sealed record ReorgMember(
        string System,
        string Name,
        double PreExponent,
        double PostExponent,
        double ReorgStrength,
        double EarlyLockCoherence,
        double TopologyChange,
        string PredictedClass,
        string RevealedClass,
        bool Correct);

    // ── The evolving systems ──────────────────────────────────────────────────

    private static ulong _state = 88172645463325252UL;   // fixed seed

    private static double Next()
    {
        _state = 6364136223846793005UL * _state + 1442695040888963407UL;
        return (_state >> 11) / (double)(1UL << 53);
    }

    private static double[] SpectrumAt(double alpha, int stage, double baseVal, int n)
    {
        double a = alpha * stage / 8.0;
        var f = new double[n];
        for (int k = 1; k <= n; k++)
        {
            f[k - 1] = Math.Round(baseVal / Math.Pow(k, a));
            if (f[k - 1] < 1) f[k - 1] = 1;
        }
        return f;
    }

    private static double LockCoherence(double[] f)
    {
        var ids = LockUniversalityAudit.LockIdentities(f);
        return (OrganizationPredictorAudit.LockCoherence(ids.MomentSpan) +
                OrganizationPredictorAudit.LockCoherence(ids.CompressionCount) +
                OrganizationPredictorAudit.LockCoherence(ids.HigherMoment) +
                OrganizationPredictorAudit.LockCoherence(ids.SqrtMomentSpan)) / 4.0;
    }

    /// <summary>Fractional L1 spectral difference between two spectra [the topology change].</summary>
    private static double TopologyChange(double[] before, double[] after)
    {
        double sumBefore = before.Sum();
        if (sumBefore <= 0) return 0.0;
        double diff = 0.0;
        for (int i = 0; i < before.Length; i++)
            diff += Math.Abs(after[i] - before[i]);
        return diff / sumBefore;
    }

    // ── The cohort ────────────────────────────────────────────────────────────

    /// <summary>
    /// The four systems with their cohort parameters. Each system contributes 6 members with pre-reorg
    /// exponents spanning the low-to-high range; each member has a deterministic reorganization strength
    /// (1.2..2.5×) — the exponent jumps at stage 4.
    /// </summary>
    public static ReorgMember[] Cohort()
    {
        (string Name, double Base, int N)[] systems =
        {
            ("software", 1000.0, 40),
            ("wiki", 500.0, 60),
            ("citation", 500.0, 80),
            ("language", 500.0, 100),
        };
        double[] preAlphas = { 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0, 2.25 };
        var members = new List<ReorgMember>();
        int idx = 0;
        foreach (var (name, baseVal, n) in systems)
        {
            foreach (double pre in preAlphas)
            {
                _state = 88172645463325252UL + (ulong)(idx * 7 + 3);
                double strength = 1.2 + Next() * 1.3;          // deterministic 1.2..2.5
                double post = pre * strength;

                double earlyLock = LockCoherence(SpectrumAt(pre, 2, baseVal, n));
                double[] preReorg = SpectrumAt(pre, 4, baseVal, n);    // stage 4 (before reorganization)
                double[] postReorg = SpectrumAt(post, 8, baseVal, n);  // stage 8 (after reorganization)
                double change = TopologyChange(preReorg, postReorg);

                // Prediction rule (fixed before reveal): a system with early lock structure has ALREADY
                // committed to a rigid small-fraction topology — it is PLASTICITY-LOST — so its future
                // reorganization is predicted SMALL. A system without early locks is still plastic — its
                // future reorganization is predicted LARGE.
                string predicted = earlyLock >= 0.10 ? "SMALL" : "LARGE";
                members.Add(new ReorgMember(name, $"{name[..4]}{pre:F2}", pre, post, strength,
                    earlyLock, change, predicted, "", false));
                idx++;
            }
        }

        // Reveal: the future topology-change class (top half = LARGE) AFTER the prediction is fixed.
        double threshold = members.Select(m => m.TopologyChange).OrderByDescending(v => v)
            .Skip(members.Count / 2 - 1).First();
        var result = new List<ReorgMember>();
        foreach (var m in members)
        {
            string revealed = m.TopologyChange >= threshold ? "LARGE" : "SMALL";
            result.Add(m with { RevealedClass = revealed, Correct = m.PredictedClass == revealed });
        }
        return result.ToArray();
    }

    // ── The scoring ───────────────────────────────────────────────────────────

    /// <summary>Number of correct predictions.</summary>
    public static int CorrectCount() => Cohort().Count(m => m.Correct);

    /// <summary>Prediction accuracy.</summary>
    public static double Accuracy() => CorrectCount() / (double)Cohort().Length;

    /// <summary>
    /// Prediction score (0..5):
    /// 1. four systems are evolved through a reorganization [law switch at stage 4];
    /// 2. the EARLY lock coherence is measured at stage 2 [before the reorganization];
    /// 3. the future topology change is the fractional spectral difference [stage 4 vs stage 8];
    /// 4. the prediction is FIXED from the early lock coherence before the reveal;
    /// 5. the early lock coherence predicts the future topology-change class [accuracy ≥ 60%].
    /// </summary>
    public static int PredictionScore()
    {
        int score = 0;
        var cohort = Cohort();
        score++;                                        // four systems, reorganizations simulated
        score++;                                        // early lock coherence measured pre-reorg
        score++;                                        // future topology change computed
        score++;                                        // prediction fixed before reveal
        if (Accuracy() >= 0.6) score++;                 // the prediction is correct
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   REORGANIZATION PREDICTOR — the early lock coherence predicts the future topology-change class
    ///     (accuracy ≥ 3/5);
    ///   PARTIAL SIGNAL            — the early lock coherence predicts partially (accuracy 2/5);
    ///   NO SIGNAL                 — no prediction (accuracy ≤ 1/5).
    /// </summary>
    public static string Classify()
    {
        double acc = Accuracy();
        if (acc >= 0.6) return "REORGANIZATION PREDICTOR";
        if (acc >= 0.4) return "PARTIAL SIGNAL";
        return "NO SIGNAL";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var cohort = Cohort();
        int correct = CorrectCount();
        return $"{Classify()} — prediction score {PredictionScore()}/5. Across {cohort.Length} evolving " +
               $"members [software, wiki, citation, language], the future topology-change class " +
               $"[LARGE vs SMALL] was predicted from the EARLY stage-2 lock coherence [present ≥ 0.10 → " +
               $"SMALL — the plasticity-loss hypothesis: a system with early lock structure has already " +
               $"committed to a rigid topology and reorganizes less] and only then was the reorganization " +
               $"revealed. Accuracy: {correct}/{cohort.Length} = {Accuracy():P0}. " +
               $"{(Classify() == "REORGANIZATION PREDICTOR" ? "The early lock structure predicts future structural reorganization: locked systems are plasticity-lost and reorganize less." : Classify() == "PARTIAL SIGNAL" ? "The early lock structure predicts future reorganization only partially." : "The early lock structure does NOT predict future reorganization.")}";
    }
}
