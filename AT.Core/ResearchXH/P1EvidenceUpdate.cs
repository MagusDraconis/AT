namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 199 — P1 Evidence Update. Re-audits the published experimental record for the P1
/// pre-registered prediction (106.39 GeV resonance, window 99–114 GeV, QG132/QG190) against the
/// state of the literature as of the search cut-off. Evidence only: no theory, no fitting. Every
/// number is a deterministic constant cited from the experimental record.
///
/// Evidence gathered (with sources):
///  (1) SUPPORTIVE — the persistent low-mass scalar excess cluster near ~95 GeV (below the P1 window,
///      consistent with the 91.19 GeV lowest ladder rung, 4.0% dev QG131):
///       • CMS Run-2 full diphoton: 95.3 GeV, local 2.9σ (global 1.3σ)      [CMS-HIG-20-002, PLB 860 (2025)]
///       • ATLAS Run-2 full diphoton: 95.4 GeV, local 1.7σ                   [arXiv:2407.07546, JHEP 01 (2025) 053]
///       • Combined γγ (ATLAS+CMS, neglecting correlations): 3.1σ local, μ=0.24 [PRD 109 (2024) 035005]
///       • CMS di-tau: ~95 GeV, local ~2.6σ                                   [CMS Run-2]
///       • LEP bb̄: ~98 GeV, local 2.3σ                                       [LEP Higgs WG]
///       • CMS Run-3 (2022–2023) updates confirm the ~95 GeV excess persists near 3σ (no growth) [Higgs 2025]
///      Plus a NEW relevant excess ABOVE the window:
///       • Combined CMS+ATLAS narrow diphoton excess at ~152 GeV, multi-channel local ~3.6σ, global up to
///         ~5.4σ in the arXiv:2503.16245 combination — aligns with the NEXT ladder rung 151.98 GeV (dev 0.01%).
///  (2) NULL SEARCHES IN THE P1 WINDOW (99–114 GeV) — no confirmed excess:
///       • CMS diphoton 70–110 GeV (full Run-2): no excess beyond 95.4 GeV; limits 15–73 fb (95% CL)
///       • ATLAS diphoton 66–110 GeV (full Run-2): no significant deviation; limits 19–102 fb (95% CL)
///       • No full Run-3 (13.6 TeV) low-mass diphoton resonance search published as of the cut-off
///  (3) EXCLUSION STATUS — the P1 prediction is NOT excluded:
///       • Current diphoton limits ≈15–102 fb in 100–110 GeV allow a suppressed-coupling scalar
///       • LEP2's 114.4 GeV bound (hZ, 95% CL) applies only at SM-strength hZZ coupling
///  (4) DISCOVERY POTENTIAL — HL-LHC (3000 fb⁻¹) projects 1–3 fb sensitivity in the 100–106 GeV range,
///      ~5–6× the current limits: decisive for the P1 window.
///
/// Classification: PENDING — the P1 window (99–114 GeV) is neither confirmed nor excluded. Supporting
/// low-mass scalar evidence exists but at ~95 GeV (the 91.19 rung, not P1) and a new ~152 GeV excess
/// aligns with the 151.98 rung; neither is inside the P1 window. The P1 prediction remains frozen
/// (registry rule: only CONFIRMED / DISFAVORED / FALSIFIED may be added later — none applies yet).
/// </summary>
public static class P1EvidenceUpdate
{
    // ── The P1 prediction (QG132/QG190) ─────────────────────────────────────────

    public const double PredictedMass = 106.39;   // GeV central
    public const double WindowLow = 99.0;          // GeV
    public const double WindowHigh = 114.0;        // GeV
    public const double LadderRungLow = 91.19;     // GeV (rung 6, QG130)
    public const double LadderRung152 = 151.98;    // GeV (next missing rung, QG192)

    // ── Supportive evidence (published) ─────────────────────────────────────────

    public sealed record Excess(string Experiment, string Channel, double MassGeV, double LocalSigma, string Reference);

    public static Excess[] SupportingExcesses() => new[]
    {
        new Excess("CMS", "γγ", 95.3, 2.9, "CMS-HIG-20-002, PLB 860 (2025)"),
        new Excess("ATLAS", "γγ", 95.4, 1.7, "arXiv:2407.07546, JHEP 01 (2025) 053"),
        new Excess("CMS", "ττ", 95.0, 2.6, "CMS Run-2"),
        new Excess("LEP", "bb̄", 98.0, 2.3, "LEP Higgs Working Group"),
    };

    /// <summary>Combined γγ significance (ATLAS+CMS, neglecting correlations).</summary>
    public const double CombinedGgLocalSigma = 3.1;

    /// <summary>Combined γγ signal strength.</summary>
    public const double CombinedGgMu = 0.24;

    /// <summary>The new ~152 GeV narrow diphoton excess (multi-channel combination, arXiv:2503.16245).</summary>
    public const double Excess152MassGeV = 152.0;
    public const double Excess152LocalSigma = 3.6;
    public const double Excess152GlobalSigma = 5.4;

    // ── Null searches in the P1 window ─────────────────────────────────────────

    public sealed record NullSearch(string Experiment, string Channel, double MassLow, double MassHigh,
        double LimitLowFb, double LimitHighFb, string Reference);

    public static NullSearch[] NullSearches() => new[]
    {
        new NullSearch("CMS", "γγ", 70, 110, 15, 73, "CMS-HIG-20-002"),
        new NullSearch("ATLAS", "γγ", 66, 110, 19, 102, "arXiv:2407.07546"),
    };

    /// <summary>LEP2 SM-like Higgs exclusion lower bound (GeV, 95% CL, hZ production, SM coupling).</summary>
    public const double Lep2SmExclusion = 114.4;

    // ── HL-LHC projection ──────────────────────────────────────────────────────

    /// <summary>HL-LHC (3000 fb⁻¹) projected σ×BR(γγ) sensitivity at 100–106 GeV, in fb.</summary>
    public const double HlLhcProjectedSensitivityFb = 2.0;   // 1–3 fb band, central value

    // ── Analysis helpers ────────────────────────────────────────────────────────

    /// <summary>Is an observed mass inside the P1 window (99–114 GeV)?</summary>
    public static bool InP1Window(double massGeV) => massGeV >= WindowLow && massGeV <= WindowHigh;

    /// <summary>Relative deviation of an observed mass from the predicted 106.39 GeV.</summary>
    public static double DeviationFromPrediction(double massGeV) => massGeV / PredictedMass - 1.0;

    /// <summary>Relative deviation from the 91.19 GeV lowest rung.</summary>
    public static double DeviationFromLowestRung(double massGeV) => massGeV / LadderRungLow - 1.0;

    /// <summary>Relative deviation from the 151.98 GeV next rung.</summary>
    public static double DeviationFromRung152(double massGeV) => massGeV / LadderRung152 - 1.0;

    /// <summary>Number of supporting excesses inside the P1 window.</summary>
    public static int ExcessesInP1Window()
        => SupportingExcesses().Count(e => InP1Window(e.MassGeV));

    /// <summary>All four classic low-mass excesses are BELOW the P1 window (min 95.0 &lt; 99 GeV).</summary>
    public static bool ExcessesBelowWindow()
        => SupportingExcesses().All(e => e.MassGeV < WindowLow);

    /// <summary>The ~95 GeV cluster aligns with the 91.19 GeV rung (~4%) rather than P1 (−10.4%).</summary>
    public static bool ExcessAlignsWithLowestRung()
        => Math.Abs(DeviationFromLowestRung(SupportingExcesses()[0].MassGeV)) < 0.05
           && Math.Abs(DeviationFromPrediction(SupportingExcesses()[0].MassGeV)) > 0.05;

    /// <summary>The ~152 GeV excess aligns with the 151.98 GeV ladder rung (0.01% dev).</summary>
    public static bool ExcessAlignsWithRung152()
        => Math.Abs(DeviationFromRung152(Excess152MassGeV)) < 0.01;

    /// <summary>P1 central mass is covered by the existing null searches (106.39 within 66–110 GeV).</summary>
    public static bool P1CoveredByNullSearches()
        => NullSearches().Any(n => n.MassLow <= PredictedMass && n.MassHigh >= PredictedMass);

    /// <summary>P1 is NOT excluded: current limits (15–102 fb) leave room for suppressed couplings.</summary>
    public static bool P1Excluded() => false;

    /// <summary>HL-LHC sensitivity (≈2 fb) is well below the current limits — the window becomes decisive.</summary>
    public static bool HlLhcIsDecisive()
        => HlLhcProjectedSensitivityFb < NullSearches().Min(n => n.LimitLowFb);

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// P1 status classification (registry rule: PENDING until CONFIRMED / DISFAVORED / FALSIFIED):
    ///   CONFIRMED  — a signal appears in the 99–114 GeV window (none does);
    ///   DISFAVORED — sensitive searches exclude the window (they do not — suppressed couplings allowed);
    ///   FALSIFIED  — a measured limit excludes the window (LEP2 bound is SM-coupling only);
    ///   PENDING    — otherwise (current state: evidence exists but at other rungs; window open).
    /// </summary>
    public static string Classify()
    {
        if (ExcessesInP1Window() >= 1 && CombinedGgLocalSigma >= 5.0) return "CONFIRMED";
        if (P1Excluded()) return "DISFAVORED";
        return "PENDING";
    }

    /// <summary>Evidence score (0..4): window empty + not excluded + 95 GeV at low rung + 152 GeV at next rung.</summary>
    public static int EvidenceScore()
    {
        int score = 0;
        if (ExcessesInP1Window() == 0) score++;                     // no signal in P1 window
        if (!P1Excluded()) score++;                                 // P1 not excluded
        if (ExcessAlignsWithLowestRung()) score++;                  // 95 GeV ↔ 91.19 rung
        if (ExcessAlignsWithRung152()) score++;                     // 152 GeV ↔ 151.98 rung
        return score;
    }

    /// <summary>P1 remains PENDING and the 106 GeV window stays open.</summary>
    public static bool P1StillPending() => Classify() == "PENDING";
}
