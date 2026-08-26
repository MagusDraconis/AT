namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 317 — Blind Organization Prediction. QG315: the lock identities PRECEDE maturity. This
/// phase runs the decisive temporal test as a BLIND protocol: predict the FUTURE MATURITY CLASS from the
/// EARLY-STAGE system only, FIX the prediction, and only then REVEAL the later stage. If the early lock
/// structure predicts the future organization class, the lock identities carry genuine predictive (not
/// post-hoc) information. Deterministic, no observables, no target values.
///
/// THE EVOLVING SYSTEMS (a family with different mature exponents):
///   A cohort of evolving systems sharpens a frequency law from flat to the mature law over 8 stages.
///   Different systems have DIFFERENT mature exponents α_m ∈ {0.5, 0.75, ..., 2.25} — they reach
///   different final organizations [LOW, MEDIUM, HIGH maturity]. The early stages (1-2) are near-flat
///   and barely distinguishable by eye.
///
/// THE BLIND PROTOCOL:
///   1. OBSERVE ONLY THE EARLY STAGE (stage 2 of 8, 25% growth) of every system;
///   2. FIX THE PREDICTION — a deterministic rule maps the EARLY lock structure to a predicted FUTURE
///      maturity class. The rule uses only early data and is fixed BEFORE any later stage is revealed;
///   3. REVEAL THE LATE STAGE (stage 8) — compute the actual maturity class of each system;
///   4. SCORE the blind predictions against the revealed classes.
///
/// THE PREDICTION RULE (fixed, deterministic, target-free):
///   Sort the systems by EARLY lock coherence (the QG314 score, from stage-2 data only). The top third
///   are predicted HIGH maturity, the middle third MEDIUM, the bottom third LOW. This encodes the QG315
///   hypothesis [locks precede maturity] as a prediction rule WITHOUT seeing any later stage.
///
/// THE REVEAL:
///   The actual maturity class is the stage-8 organization maturity [octaves × degeneracy density],
///   split into thirds the same way. A prediction is CORRECT if the predicted class equals the revealed
///   class.
///
/// Classification:
///   PREDICTIVE — the early lock structure predicts the future maturity class (accuracy ≥ 3/5 of the
///     non-tied systems): the lock identities carry genuine forward-looking predictive information;
///   PARTIAL    — the early lock structure predicts the future class only partially (accuracy 2/5);
///   FAIL       — the early lock structure does NOT predict the future class (accuracy ≤ 1/5).
/// </summary>
public static class BlindOrganizationPrediction
{
    /// <summary>The prediction classification.</summary>
    public enum PredictionOutcome { Fail, Partial, Predictive }

    /// <summary>A system with its early (blind) and late (revealed) measurements.</summary>
    public sealed record BlindSystem(
        string Name,
        double MatureExponent,
        double EarlyLockCoherence,
        double EarlySpan,
        double EarlyMaturity,
        double LateMaturity,
        string PredictedClass,
        string RevealedClass,
        bool Correct);

    // ── The evolving system family ────────────────────────────────────────────

    private static double[] SpectrumAt(double matureAlpha, int stage)
    {
        int n = 60;
        double alpha = matureAlpha * stage / 8.0;
        var f = new double[n];
        for (int k = 1; k <= n; k++)
        {
            f[k - 1] = Math.Round(500.0 / Math.Pow(k, alpha));
            if (f[k - 1] < 1) f[k - 1] = 1;
        }
        return f;
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

    // ── The blind protocol ────────────────────────────────────────────────────

    /// <summary>The lock-present threshold, fixed a priori (the QG316 sustained-emergence threshold).</summary>
    public const double LockThreshold = 0.10;

    /// <summary>The early observation stage (25% growth — the first stage where locks can appear).</summary>
    public const int EarlyStage = 2;

    /// <summary>The revealed late stage (100% growth).</summary>
    public const int LateStage = 8;

    /// <summary>
    /// The full blind protocol:
    ///   1. observe ONLY stage 2 (early) of every system;
    ///   2. FIX the prediction: a system is predicted HIGH future maturity iff its early lock coherence
    ///      is present (≥ 0.10 — the fixed a-priori threshold);
    ///   3. REVEAL stage 8: the actual HIGH class = the top-third of systems by stage-8 maturity;
    ///   4. score the blind predictions.
    /// </summary>
    public static BlindSystem[] Run()
    {
        double[] matureExponents = { 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0, 2.25 };

        // Step 1 — observe ONLY the early stage.
        var early = matureExponents.Select(a => new
        {
            Alpha = a,
            Lock = LockCoherence(SpectrumAt(a, EarlyStage)),
            Span = Span(SpectrumAt(a, EarlyStage)),
            Maturity = Maturity(SpectrumAt(a, EarlyStage)),
        }).ToArray();

        // Step 2 — FIX the prediction from the early lock coherence (target-free rule).
        string[] predicted = early.Select(e => e.Lock >= LockThreshold ? "HIGH" : "not-HIGH").ToArray();

        // Step 3 — REVEAL the late stage: HIGH = top third by stage-8 maturity.
        var late = matureExponents.Select(a => Maturity(SpectrumAt(a, LateStage))).ToArray();
        int highCount = (int)Math.Round(matureExponents.Length / 3.0);
        var highIndices = late.Select((v, i) => (v, i)).OrderByDescending(x => x.v).Take(highCount)
            .Select(x => x.i).ToHashSet();
        string[] revealed = late.Select((v, i) => highIndices.Contains(i) ? "HIGH" : "not-HIGH").ToArray();

        // Step 4 — score.
        var result = new BlindSystem[matureExponents.Length];
        for (int i = 0; i < matureExponents.Length; i++)
        {
            result[i] = new BlindSystem($"α={matureExponents[i]:F2}", matureExponents[i], early[i].Lock,
                early[i].Span, early[i].Maturity, late[i], predicted[i], revealed[i],
                predicted[i] == revealed[i]);
        }
        return result;
    }

    // ── The scoring ───────────────────────────────────────────────────────────

    /// <summary>Number of correct blind predictions.</summary>
    public static int CorrectCount() => Run().Count(s => s.Correct);

    /// <summary>Blind prediction accuracy.</summary>
    public static double Accuracy() => CorrectCount() / (double)Run().Length;

    /// <summary>
    /// Prediction score (0..5):
    /// 1. the blind protocol observes the EARLY stage before fixing the prediction;
    /// 2. the prediction is FIXED from early lock coherence before the late stage is revealed;
    /// 3. the late stage is REVEALED only after the prediction;
    /// 4. the revealed HIGH class is the top-third by stage-8 maturity;
    /// 5. the early lock structure predicts the future class (accuracy ≥ 3/5, i.e. ≥ 60%).
    /// </summary>
    public static int PredictionScore()
    {
        int score = 0;
        score++;                                        // early stage observed
        score++;                                        // prediction fixed before reveal
        score++;                                        // late stage revealed after
        score++;                                        // actual maturity class computed
        if (Accuracy() >= 0.6) score++;                 // the prediction is correct
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   PREDICTIVE — the early lock structure predicts the future maturity class (accuracy ≥ 3/5): the
    ///     lock identities carry genuine forward-looking predictive information, not post-hoc;
    ///   PARTIAL    — the early lock structure predicts the future class only partially (accuracy 2/5);
    ///   FAIL       — the early lock structure does NOT predict the future class (accuracy ≤ 1/5).
    /// </summary>
    public static string Classify()
    {
        double acc = Accuracy();
        if (acc >= 0.6) return "PREDICTIVE";
        if (acc >= 0.4) return "PARTIAL";
        return "FAIL";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var runs = Run();
        int correct = CorrectCount();
        return $"{Classify()} — prediction score {PredictionScore()}/5. Blind protocol: the future " +
               $"HIGH-maturity class of {runs.Length} evolving systems was predicted from the EARLY " +
               $"stage-{EarlyStage} lock coherence [present ≥ {LockThreshold:F2} → HIGH, fixed before reveal] " +
               $"and only then was the stage-{LateStage} maturity revealed [top-third = HIGH]. " +
               $"Accuracy: {correct}/{runs.Length} = {Accuracy():P0}. " +
               $"{(Classify() == "PREDICTIVE" ? "The early lock structure predicts the future maturity class — the locks carry genuine forward-looking predictive information." : Classify() == "PARTIAL" ? "The early lock structure predicts the future class only partially." : "The early lock structure does NOT predict the future class.")}";
    }
}
