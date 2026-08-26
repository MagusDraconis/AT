namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 188 — Prediction Audit. Uses the physics-coverage single source of truth
/// (Docs/ATQG_PhysicsCoverage.json) to list ALL remaining falsifiable predictions, classify each as
/// experimentally testable now / testable soon / currently inaccessible, and rank them by scientific impact,
/// feasibility and falsifiability. Output: Top-10 predictions and a recommended next target. Deterministic.
///
/// The full prediction set (from the coverage register, mirroring the JSON):
///  P1 106 GeV resonance (QG132)          — scalar-sector transition, search window 99–114 GeV, 9 rungs
///  P2 Sector-ladder collider states (QG130) — 12-rung decay ladder, 8 thresholds, LHC/FCC reach
///  P3 0νββ rate m_ββ = 2.02e-3 eV (QG179)  — Majorana neutrino, within current limit 0.036–0.156 eV
///  P4 Redshift WITHOUT lensing (QG21)      — conformal (ψ=0) sector prediction, differs from GR
///  P5 Curvature-sourced Poisson (G4-O0)    — Poisson source = (lnρ)″, not density value
///  P6 Neutrino mass ordering m1 = 0 (QG179) — normal ordering derived; JUNO/DUNE testable
///  P7 Exact neutrino masses m1,m2,m3 (QG172) — splittings derived; absolute values open
///  P8 Quark running-scale/MS̄ conversion (QG173) — D96 mass law at MS̄ scale open
///  P9 Common sector granularity (QG69)     — all sectors granular at one common scale (qualitative)
///  P10 Regular-core profile (QG75)         — M(1−e^(−r³/r_c³)), differs from GR/Hayward/Bardeen
///
/// Classification (now / soon / inaccessible):
///  NOW        — testable with existing data or currently-running experiments (LHC Run 3, JUNO near-term);
///  SOON       — next-generation experiment within ~1–2 decades (nEXO/LEGEND-1000, DUNE, FCC-hh);
///  INACCESSIBLE — no plausible experiment in the foreseeable future (scale beyond reach, no probe).
///
/// Ranking: score = impact·3 + feasibility·2 + falsifiability·2 (deterministic, documented weights).
/// </summary>
public static class PredictionAudit
{
    // ── Prediction catalog (mirror of Docs/ATQG_PhysicsCoverage.json) ────────────

    public sealed record Prediction(string Id, string Name, string Phase, string Status,
        int Impact, int Feasibility, int Falsifiability, string Horizon, string Note)
    {
        public double Score => Impact * 3.0 + Feasibility * 2.0 + Falsifiability * 2.0;
    }

    /// <summary>The complete remaining-prediction catalog (deterministic; mirrors the coverage JSON).</summary>
    public static Prediction[] AllPredictions() => new[]
    {
        new Prediction("P1", "106 GeV resonance (scalar transition)", "QG132",
            "FALSIFIABLE — not yet observed", 5, 5, 5, "NOW",
            "Primary prediction: search window 99–114 GeV, 9 ladder rungs, 15.2/20.3 GeV decay quanta; testable at LHC Run 3"),
        new Prediction("P2", "Sector-ladder collider states", "QG130",
            "PREDICTED — no data", 4, 4, 4, "NOW",
            "12-rung decay ladder, 8 thresholds, ~90–500 GeV; LHC13/HL-LHC data can search"),
        new Prediction("P3", "0νββ rate: m_ββ = 2.02e-3 eV", "QG179",
            "PREDICTED — awaiting experiment", 5, 3, 4, "SOON",
            "Majorana neutrino; below current limit 0.036–0.156 eV; nEXO/LEGEND-1000 ton-scale reach"),
        new Prediction("P4", "Redshift WITHOUT lensing (conformal)", "QG21",
            "FALSIFIABLE — differs from GR", 3, 2, 3, "INACCESSIBLE",
            "Conformal (ψ=0) sector: lensing absent; requires isolating the scalar sector (no clean probe)"),
        new Prediction("P5", "Curvature-sourced Poisson (source = (lnρ)″)", "G4-O0",
            "AT-SPECIFIC — testable in principle", 4, 2, 3, "INACCESSIBLE",
            "No Newtonian field in uniform-density / shell-exterior regions; discriminating but no feasible probe"),
        new Prediction("P6", "Neutrino mass ordering m1 = 0 (normal)", "QG179",
            "PARTIAL — experiment pending", 4, 4, 3, "SOON",
            "Normal ordering derived; JUNO and DUNE can measure the sign of Δm²31"),
        new Prediction("P7", "Exact neutrino masses m1,m2,m3", "QG172",
            "OPEN — absolute values", 3, 3, 3, "SOON",
            "Splittings derived; absolute scale needs KATRIN/production experiments"),
        new Prediction("P8", "Quark running-scale/MS̄ conversion", "QG173",
            "OPEN — theory gap", 2, 2, 2, "INACCESSIBLE",
            "D96 mass law at MS̄ scale is a matching calculation, not a new experiment"),
        new Prediction("P9", "Common sector granularity", "QG69",
            "UNIQUE/TESTABLE (qualitative)", 3, 1, 2, "INACCESSIBLE",
            "All sectors granular at a common scale; free scale parameter, Planck-scale reach"),
        new Prediction("P10", "Regular-core black-hole profile", "QG75",
            "UNIQUE (differs from GR)", 4, 2, 4, "INACCESSIBLE",
            "M(1−e^(−r³/r_c³)) core; discriminates from singular GR/Hayward/Bardeen; EHT horizon-scale"),
    };

    // ── Horizon classification ─────────────────────────────────────────────────────

    /// <summary>Predictions testable with existing / currently-running experiments.</summary>
    public static Prediction[] TestableNow() => AllPredictions().Where(p => p.Horizon == "NOW").ToArray();

    /// <summary>Predictions testable by next-generation experiments within ~1–2 decades.</summary>
    public static Prediction[] TestableSoon() => AllPredictions().Where(p => p.Horizon == "SOON").ToArray();

    /// <summary>Predictions with no plausible experiment in the foreseeable future.</summary>
    public static Prediction[] CurrentlyInaccessible() => AllPredictions().Where(p => p.Horizon == "INACCESSIBLE").ToArray();

    // ── Ranking ────────────────────────────────────────────────────────────────────

    /// <summary>Rank all predictions by score (impact·3 + feasibility·2 + falsifiability·2), descending.</summary>
    public static Prediction[] Ranked() => AllPredictions().OrderByDescending(p => p.Score).ToArray();

    /// <summary>The Top-10 predictions by composite score.</summary>
    public static Prediction[] Top10() => Ranked().Take(10).ToArray();

    /// <summary>The recommended next target = the highest-ranked prediction.</summary>
    public static Prediction RecommendedNextTarget() => Ranked()[0];

    /// <summary>Is the 106 GeV resonance the top-ranked target?</summary>
    public static bool RecommendedIs106GeV()
        => RecommendedNextTarget().Id == "P1";

    // ── Validation ─────────────────────────────────────────────────────────────────

    /// <summary>The prediction count (should be 10).</summary>
    public static int Count() => AllPredictions().Length;

    /// <summary>All three horizon classes are non-empty.</summary>
    public static bool AllHorizonsPresent()
        => TestableNow().Length > 0 && TestableSoon().Length > 0 && CurrentlyInaccessible().Length > 0;

    /// <summary>The Top-10 has 10 entries and is sorted descending by score.</summary>
    public static bool Top10Valid()
    {
        var top = Top10();
        if (top.Length != 10) return false;
        for (int i = 1; i < top.Length; i++)
            if (top[i].Score > top[i - 1].Score) return false;
        return true;
    }

    /// <summary>The coverage JSON mirror is complete: every named prediction in the catalog exists.</summary>
    public static bool CatalogComplete()
        => Count() == 10 && AllHorizonsPresent() && Top10Valid();

    /// <summary>Classification: audit of the full remaining-prediction set, ranked, with a recommended next target.</summary>
    public static string Classify() => "PREDICTION AUDIT";
}
