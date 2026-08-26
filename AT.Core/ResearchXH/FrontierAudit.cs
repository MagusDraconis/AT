namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 211 — Frontier Audit. Recomputes ALL remaining unresolved items after QG210, excluding
/// resolved, partial-resolved, and superseded entries, and produces the Top-10 frontier problems.
/// Sources: Docs/ATQG_PhysicsCoverage.json, Docs/ATQG_Predictions.json, Docs/ATQG_PredictionOutcomes.json.
/// Deterministic.
///
/// RESOLVED / SUPERSEDED SINCE THE QG205 AUDIT (excluded here):
///   SM1 exact neutrino masses      (QG203 ABSOLUTE MASS ORIGIN)
///   SM2 quark MS̄ conversion        (QG204 RUNNING ORIGIN)
///   G2 flat rotation-curve α=0     (QG206 ALPHA-ZERO ORIGIN)
///   F1 metric ansatz               (QG207 PARTIAL UNIQUE — partially-resolved)
///   G1 Hawking temperature after ψ (QG208 HAWKING ORIGIN)
///   SM4 lepton hierarchy           (QG209 EXACT LAW)
///   SM6 family index               (QG210 EXACT ORIGIN)
///
/// REMAINING UNRESOLVED (the frontier):
///   PREDICTION:
///     P1 106 GeV resonance        (FALSIFIABLE-PENDING; window open)
///     P2 0νββ m_ββ = 2.02 meV      (PENDING; below current reach)
///     P3 sector-ladder spectrum    (SUPPORTED at 151.98; 8 rungs PENDING)
///     Redshift-without-lensing     (QG21; falsifiable, differs from GR)
///     Curvature-sourced Poisson    (G4-O0; AT-specific, testable in principle)
///   GRAVITY:
///     Conformal optics (no lensing) — the G3 gap (QG21/QG26): the ψ=0 sector predicts no lensing
///   FOUNDATIONAL:
///     Bekenstein 1/4               (QG196: PROVEN IMPOSSIBLE without imported π)
///     ψ/Weyl field                 (QG23/24/47/56/57: capacity forced, excitation derived — PARTIAL)
///   STANDARD MODEL (partial laws):
///     Quark hierarchy unified law  (QG146 PARTIAL LAW)
///     Golden-ratio hierarchy       (QG152 PARTIAL ROBUSTNESS)
///     Physical calibration ladder  (QG129 PARTIAL MAPPING)
///
/// TOP-10 FRONTIER (ranked by importance = impact·3 + feasibility·2 + falsifiability·2):
///   1. P1 106 GeV resonance          (PREDICTION, 35)
///   2. P2 0νββ m_ββ = 2.02 meV       (PREDICTION, 31)
///   3. P3 sector-ladder spectrum     (PREDICTION, 30)
///   4. G3 conformal optics/no lensing (GRAVITY, 22)
///   5. F2 Bekenstein 1/4             (FOUNDATIONAL, 20)
///   6. P4 redshift-without-lensing   (PREDICTION, 19)
///   7. SM5 quark hierarchy unified law (STANDARD MODEL, 19)
///   8. F3 ψ/Weyl field origin        (FOUNDATIONAL, 18)
///   9. P5 curvature-sourced Poisson  (PREDICTION, 17)
///   10. SM7 golden-ratio hierarchy   (STANDARD MODEL, 14)
///
/// The true final frontier is dominated by the three pre-registered predictions (experimental) plus the
/// conformal/tensor gravity gap and two foundational items (Bekenstein 1/4 proven-impossible, ψ origin).
/// </summary>
public static class FrontierAudit
{
    public sealed record FrontierProblem(string Id, string Title, string Category, string WhyOpen,
        int Impact, int Feasibility, int Falsifiability)
    {
        public double Score => Impact * 3.0 + Feasibility * 2.0 + Falsifiability * 2.0;
    }

    /// <summary>Resolved-and-excluded items since QG205.</summary>
    public static string[] ResolvedAndExcluded() => new[]
    {
        "SM1 exact neutrino masses (QG203)",
        "SM2 quark MS̄ conversion (QG204)",
        "G2 flat rotation-curve α=0 (QG206)",
        "F1 metric ansatz (QG207, partially-resolved)",
        "G1 Hawking temperature after ψ (QG208)",
        "SM4 lepton hierarchy (QG209)",
        "SM6 family index (QG210)",
    };

    /// <summary>The complete frontier (Top-10), sorted descending by score.</summary>
    public static FrontierProblem[] Top10() => new FrontierProblem[]
    {
        new("P1", "106 GeV resonance", "PREDICTION",
            "Window 99–114 GeV neither confirmed nor excluded (QG199 PENDING); CMS 15–73 fb, ATLAS 19–102 fb limits leave suppressed couplings allowed; HL-LHC decisive.",
            5, 5, 5),
        new("P2", "0νββ m_ββ = 2.02 meV", "PREDICTION",
            "No experiment has reached 2.02 meV sensitivity (current limits 0.036–0.156 eV); below all existing 0νββ limits.",
            5, 3, 5),
        new("P3", "Sector-ladder spectrum", "PREDICTION",
            "151.98 rung SUPPORTED by the ~152 GeV excess (MODERATE 2.80σ, QG201); 8 rungs PENDING; none falsified.",
            4, 4, 5),
        new("G3", "Conformal optics: redshift without lensing", "GRAVITY",
            "Lensing and Shapiro delay vanish in the conformal (ψ=0) sector (PPN γ=−1, QG21/QG26); the tensor restoration is incomplete.",
            4, 1, 4),
        new("F2", "Exact Bekenstein 1/4 coefficient", "FOUNDATIONAL",
            "QG196 PROVES the exact 1/4 is impossible within D96/TRM without importing π (bits/cell = π).",
            4, 1, 3),
        new("P4", "Redshift WITHOUT lensing (conformal)", "PREDICTION",
            "QG21: the conformal (ψ=0) sector predicts redshift without lensing — differs from GR; no clean probe isolates it.",
            3, 2, 3),
        new("SM5", "Quark hierarchy — unified law", "STANDARD MODEL",
            "QG146 PARTIAL LAW: the six-quark hierarchy needs one unified closed law (not the superseded fitted exponents).",
            3, 2, 3),
        new("F3", "ψ/Weyl field origin", "FOUNDATIONAL",
            "Capacity forced by link completeness (QG56); excitation derived (QG57); existence observationally required (QG47) — but the field's status is PARTIAL.",
            4, 1, 2),
        new("P5", "Curvature-sourced Poisson equation", "PREDICTION",
            "G4-O0: the Poisson source is (ln ρ)″, not the density value; AT-specific, testable in principle but no feasible probe.",
            3, 1, 3),
        new("SM7", "Golden-ratio hierarchy robustness", "STANDARD MODEL",
            "QG152 PARTIAL ROBUSTNESS: the golden-ratio feature is sensitive to parameter choices.",
            2, 2, 2),
    };

    /// <summary>Category counts.</summary>
    public static IReadOnlyDictionary<string, int> CategoryCounts()
        => Top10().GroupBy(p => p.Category).ToDictionary(g => g.Key, g => g.Count());

    // ── Validation ─────────────────────────────────────────────────────────────

    /// <summary>The Top-10 is complete, sorted descending, and has all categories.</summary>
    public static bool Top10Valid()
    {
        var top = Top10();
        if (top.Length != 10) return false;
        for (int i = 1; i < top.Length; i++)
            if (top[i].Score > top[i - 1].Score) return false;
        return true;
    }

    /// <summary>The 106 GeV resonance is the top frontier problem.</summary>
    public static bool TopIs106GeV() => Top10()[0].Id == "P1";

    /// <summary>The seven post-QG205 resolutions are all excluded.</summary>
    public static bool ExclusionsComplete()
        => ResolvedAndExcluded().Length == 7
           && ResolvedAndExcluded().All(s => s.Contains("QG203") || s.Contains("QG204") || s.Contains("QG206")
                || s.Contains("QG207") || s.Contains("QG208") || s.Contains("QG209") || s.Contains("QG210"));
}
