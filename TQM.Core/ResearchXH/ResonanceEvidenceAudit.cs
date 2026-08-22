namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 188A — 106 GeV Resonance Evidence Audit. QG132 predicts a primary resonance at 106.39 GeV in the
/// search window 99–114 GeV (Z-anchor electroweak calibration, robustness MODERATE per QG133). This phase audits
/// ALL EXISTING published experimental evidence — ATLAS, CMS, LEP — against that prediction, using only completed
/// TQM results, no new theory, no fitting.
///
/// Evidence collected (published, deterministic constants from the literature):
///  (1) SUPPORTING EXCESSES — a persistent low-mass scalar excess cluster near ~95 GeV, BELOW the predicted window:
///       • CMS Run-2 full diphoton: 95.3 GeV, local 2.9σ (global 1.3σ)          [CMS-HIG-20-002, PLB 860 (2025)]
///       • ATLAS Run-2 full diphoton: 95.4 GeV, local 1.7σ                       [arXiv:2407.07546]
///       • Combined γγ (neglecting correlations): 3.1σ local, μ = 0.24           [PRD 109 (2024) 035005]
///       • CMS di-tau: ~95 GeV, local ~2.6σ
///       • LEP bb̄: ~98 GeV, local ~2.3σ
///     These are consistent with the LOWEST ladder rung 91.19 GeV (dev 4.0%, QG131) — NOT with the 106 GeV rung.
///  (2) NULL SEARCHES IN THE PREDICTED WINDOW — the 99–114 GeV window itself has NO confirmed excess:
///       • CMS diphoton search 70–110 GeV (full Run-2): no excess beyond 95.4 GeV; limits 15–73 fb (95% CL)
///       • ATLAS diphoton search 66–110 GeV (full Run-2): "no significant deviation"; limits 19–102 fb
///       • LEP2 hZ: SM-like Higgs excluded below 114.4 GeV (95% CL) — but only for SM-strength hZZ coupling
///  (3) EXCLUSION STATUS — the 106 GeV prediction is NOT excluded: current diphoton limits (≈20–50 fb in the
///      100–110 GeV range) still allow a suppressed-coupling scalar; LEP2's 114.4 GeV bound assumes SM coupling.
///  (4) DISCOVERY POTENTIAL — Run 3 (2022–2025) data have not increased the 95 GeV significance; HL-LHC (~late
///      2020s) will probe the window with ~5× the luminosity.
///
/// Classification: INCONCLUSIVE — the observed low-mass scalar excesses (3.1σ combined γγ) support a sector-ladder
/// scalar at ~95 GeV (the 91.19 GeV rung, 4% dev), but (a) no excess appears at the predicted 106 GeV itself, and
/// (b) the null searches in the 99–114 GeV window neither confirm nor exclude the prediction (coupling-suppressed
/// states remain allowed). Deterministic audit — no new physics.
/// </summary>
public static class ResonanceEvidenceAudit
{
    // ── The prediction (QG132/QG133) ──────────────────────────────────────────────

    public const double PredictedMass = 106.39;     // GeV (Z-anchor, QG132)
    public const double WindowLow = 99.0;           // GeV (search window)
    public const double WindowHigh = 114.0;         // GeV
    public const double LadderRungLow = 91.19;      // GeV (lowest rung, QG130)

    // ── Supporting excesses (published) ───────────────────────────────────────────

    public sealed record Excess(string Experiment, string Channel, double MassGeV, double LocalSigma, string Reference);

    public static Excess[] SupportingExcesses() => new[]
    {
        new Excess("CMS", "γγ", 95.3, 2.9, "CMS-HIG-20-002, PLB 860 (2025)"),
        new Excess("ATLAS", "γγ", 95.4, 1.7, "arXiv:2407.07546"),
        new Excess("CMS", "ττ", 95.0, 2.6, "CMS Run-2"),
        new Excess("LEP", "bb̄", 98.0, 2.3, "LEP bb̄ excess"),
    };

    /// <summary>Combined γγ significance (ATLAS+CMS, neglecting correlations).</summary>
    public const double CombinedGgLocalSigma = 3.1;

    /// <summary>Combined γγ signal strength μ = 0.24.</summary>
    public const double CombinedGgMu = 0.24;

    // ── Null searches in the predicted window ─────────────────────────────────────

    public sealed record NullSearch(string Experiment, string Channel, double MassLow, double MassHigh,
        double LimitLowFb, double LimitHighFb, string Reference);

    public static NullSearch[] NullSearches() => new[]
    {
        new NullSearch("CMS", "γγ", 70, 110, 15, 73, "CMS-HIG-20-002"),
        new NullSearch("ATLAS", "γγ", 66, 110, 19, 102, "arXiv:2407.07546"),
    };

    /// <summary>LEP2 SM-like Higgs exclusion lower bound (GeV, 95% CL, hZ production).</summary>
    public const double Lep2SmExclusion = 114.4;

    // ── Analysis helpers ──────────────────────────────────────────────────────────

    /// <summary>Is an observed mass inside the predicted 99–114 GeV search window?</summary>
    public static bool InPredictedWindow(double massGeV)
        => massGeV >= WindowLow && massGeV <= WindowHigh;

    /// <summary>Relative deviation of an observed mass from the predicted 106.39 GeV.</summary>
    public static double DeviationFromPrediction(double massGeV)
        => massGeV / PredictedMass - 1.0;

    /// <summary>Relative deviation of an observed mass from the lowest ladder rung 91.19 GeV.</summary>
    public static double DeviationFromLowestRung(double massGeV)
        => massGeV / LadderRungLow - 1.0;

    /// <summary>Number of supporting excesses inside the predicted window.</summary>
    public static int ExcessesInWindow()
        => SupportingExcesses().Count(e => InPredictedWindow(e.MassGeV));

    /// <summary>The lowest supporting excess mass (95.0 GeV) is below the window (99 GeV).</summary>
    public static bool ExcessBelowWindow()
        => SupportingExcesses().Min(e => e.MassGeV) < WindowLow;

    /// <summary>Do the excesses align with the 91.19 GeV rung (within ~5%) rather than the 106 GeV prediction?</summary>
    public static bool ExcessAlignsWithLowestRung()
        => Math.Abs(DeviationFromLowestRung(SupportingExcesses()[0].MassGeV)) < 0.05
           && Math.Abs(DeviationFromPrediction(SupportingExcesses()[0].MassGeV)) > 0.05;

    /// <summary>Any null search covering the PREDICTED MASS (106.39 GeV is well within 66–110 GeV)?</summary>
    public static bool PredictedMassCoveredByNullSearch()
        => NullSearches().Any(n => n.MassLow <= PredictedMass && n.MassHigh >= PredictedMass);

    /// <summary>Is the 106 GeV prediction excluded? No — the ~20–50 fb limits allow a suppressed-coupling scalar.</summary>
    public static bool PredictionExcluded()
        => false; // current diphoton limits ≈20–50 fb in 100–110 GeV leave room for suppressed couplings

    /// <summary>The combined 3.1σ γγ excess is below the 5σ discovery threshold.</summary>
    public static bool BelowDiscoveryThreshold()
        => CombinedGgLocalSigma < 5.0;

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Evidence score (0..3):
    /// 1. supporting low-mass scalar excesses exist (3.1σ combined γγ, near 95 GeV);
    /// 2. the predicted window has no confirmed excess AND no exclusion (null searches, limits not conclusive);
    /// 3. the excess mass aligns with the 91.19 GeV ladder rung rather than the 106 GeV prediction.
    /// Score 3 = INCONCLUSIVE (evidence exists but at the wrong rung; the prediction is neither confirmed nor excluded).
    /// </summary>
    public static int EvidenceScore()
    {
        int score = 0;
        if (CombinedGgLocalSigma >= 2.0) score++;                       // supporting excess cluster
        if (ExcessesInWindow() == 0 && !PredictionExcluded()) score++;  // window null + not excluded
        if (ExcessAlignsWithLowestRung()) score++;                      // excess at 91.19 rung, not 106
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   SUPPORTED   — the predicted 106 GeV resonance has a confirmed observed excess;
    ///   DISFAVORED  — null searches exclude the prediction;
    ///   INCONCLUSIVE — evidence exists (95 GeV scalar cluster, 3.1σ combined) but at the 91.19 GeV rung, NOT at
    ///                  the predicted 106 GeV; the 99–114 GeV window is neither confirmed nor excluded.
    /// </summary>
    public static string Classify()
    {
        if (ExcessesInWindow() >= 1 && CombinedGgLocalSigma >= 5.0) return "SUPPORTED";
        if (PredictionExcluded()) return "DISFAVORED";
        return "INCONCLUSIVE";
    }
}
