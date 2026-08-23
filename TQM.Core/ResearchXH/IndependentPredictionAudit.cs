namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 252 — Independent Prediction Audit. The QG250 hostile referee asked (F2) whether TQM's
/// validation is self-confirming. This phase measures how much of the evidence comes from genuine
/// prediction vs reconstruction, by classifying every validation result in the independent-evidence
/// phases (QG176, QG177, QG190-193, QG199-202, QG240) as POSTDICTION / BLIND RECONSTRUCTION /
/// PRE-REGISTERED PREDICTION / EXTERNAL SUPPORT. Deterministic, audit only.
///
/// THE CLASSIFICATION (target-knowledge criterion):
///  POSTDICTION              — the target value was KNOWN when the formula was built and compared
///                             (the bulk of the tested observable register: masses, mixings, couplings).
///  BLIND RECONSTRUCTION     — the target was HIDDEN from the derivation machinery (allowed-list / LOO /
///                             locked-step) so the derivation could not see it; but the value was already
///                             measured, so the blindness is METHODOLOGICAL, not temporal.
///  PRE-REGISTERED PREDICTION — the value was FROZEN before measurement (P1/P2/P3, QG190-193) —
///                             genuinely temporal: the target did not exist at derivation time.
///  EXTERNAL SUPPORT         — an independent experiment subsequently matched a frozen value (P3
///                             SUPPORTED: the 151.98 rung ~ 152 GeV excess, 2.80σ, QG200/201).
///
/// THE INVENTORY (evidence units):
///  POSTDICTION: 35 units — the tested observable register (target known during derivation):
///      quark masses 6, lepton masses 3, neutrino masses 3, CKM 3, PMNS 3, couplings 5
///      (1/α_em, α_w, α_s, sin²θ_W, θ_QCD), MW/MZ/MH/v 4, g-2 2, G/M_Pl 2, Ω_Λ/Ω_m 2,
///      precision-EW 6 (sin²θ_eff, ΓZ, ΓW, ΓH, R_b, A_FB), S/T/U 3 → the register's tested count.
///  BLIND RECONSTRUCTION: 21 units — QG176 (5: MH, ΓH, MH/MW, MH/MZ, λ_H hidden, rebuilt from
///      pre-Higgs D96), QG177 (12: leave-one-out, each observable hidden, mean dev 0.58%), QG240
///      (4: n_s, ℓ₁, ℓ₂/ℓ₁, ℓ₃/ℓ₁ locked from D96 only, max dev 0.058%).
///  PRE-REGISTERED PREDICTION: 3 units — P1 (106 GeV resonance, QG190), P2 (m_ββ = 2.02 meV, QG191),
///      P3 (sector-ladder spectrum, QG192) — all frozen before measurement.
///  EXTERNAL SUPPORT: 1 unit — P3 SUPPORTED (151.98 rung matches the ~152 GeV diphoton excess,
///      local 3.6σ / global up to 5.4σ, z = 2.80σ, QG200/201); P1/P2 remain PENDING (0 support yet).
///
/// TOTAL: 60 units. POSTDICTION 35 / BLIND 21 / PRE-REGISTERED 3 / EXTERNAL 1.
///
/// INDEPENDENT-EVIDENCE FRACTION (without knowledge of target values):
///  (a) METHODOLOGICAL independence — the derivation machinery never sees the target:
///      BLIND + PRE-REGISTERED + EXTERNAL = 21 + 3 + 1 = 25/60 = 41.7%.
///  (b) TEMPORAL independence (strictest) — the target did not exist at derivation time:
///      PRE-REGISTERED + EXTERNAL = 3 + 1 = 4/60 = 6.7%.
///
/// DETERMINATION: MEDIUM independent-evidence strength — 42% of validation units are produced with the
/// target hidden from the derivation (methodological blindness), of which the temporally-predictive core
/// (pre-registered + externally supported) is 6.7%. The referee's F2 claim is only PARTIALLY mitigated:
/// the bulk of numerical validation (58%) is still POSTDICTION against known targets; the genuinely
/// temporal prediction content is small but nonzero and externally supported (P3).
/// </summary>
public static class IndependentPredictionAudit
{
    public enum Category { Postdiction, BlindReconstruction, PreRegisteredPrediction, ExternalSupport }

    /// <summary>A validation result.</summary>
    public sealed record Result(
        string Phase,
        string Name,
        Category Category,
        int Units,
        string Note);

    /// <summary>The inventory of validation results across the independent-evidence phases.</summary>
    public static Result[] Results() => new[]
    {
        // ── QG176: Higgs blind reconstruction (target hidden, already measured) ──
        new Result("QG176", "Higgs blind reconstruction", Category.BlindReconstruction, 5,
            "MH, ΓH, MH/MW, MH/MZ, λ_H hidden; rebuilt from pre-Higgs D96 only — 125.49/125.25 GeV (0.19%)"),
        // ── QG177: leave-one-out validation (each observable hidden) ────────────
        new Result("QG177", "Leave-one-out (12 observables)", Category.BlindReconstruction, 12,
            "each observable hidden and rebuilt from the primitive D96 base; mean dev 0.58%, max 1.89%"),
        // ── QG190-193: pre-registered predictions (frozen before measurement) ────
        new Result("QG190", "P1 — 106 GeV resonance", Category.PreRegisteredPrediction, 1,
            "frozen QG190: window 99–114 GeV; PENDING (no confirmed signal yet)"),
        new Result("QG191", "P2 — 0νββ m_ββ = 2.02 meV", Category.PreRegisteredPrediction, 1,
            "frozen QG191: ±10% (1.8–2.2 meV); PENDING (below current experimental reach)"),
        new Result("QG192", "P3 — sector-ladder spectrum", Category.PreRegisteredPrediction, 1,
            "frozen QG192: 9 rungs 106.39 → 263.43 GeV; width 15.20 GeV"),
        // ── QG199-202: external evidence audits ─────────────────────────────────
        new Result("QG200/201", "P3 — 151.98 rung ~ 152 GeV excess", Category.ExternalSupport, 1,
            "EXTERNAL SUPPORT: CMS+ATLAS ~152 GeV diphoton excess (arXiv:2503.16245), local 3.6σ / global up to 5.4σ, z = 2.80σ; SM anchors Z/H/t confirm the ladder scale"),
        new Result("QG199", "P1 — evidence update", Category.Postdiction, 0,
            "no confirmed 106 GeV signal; window still open — PENDING (not an independent result yet)"),
        new Result("QG191", "P2 — evidence status", Category.Postdiction, 0,
            "no experiment at 2.02 meV sensitivity — PENDING (not an independent result yet)"),
        // ── QG240: cosmology blind reproduction (locked before comparison) ──────
        new Result("QG240", "Cosmology blind reproduction", Category.BlindReconstruction, 4,
            "n_s, ℓ₁, ℓ₂/ℓ₁, ℓ₃/ℓ₁ locked from D96 only; compared after locking — max dev 0.058% (BLIND SUCCESS)"),
        // ── The postdiction base (target known during derivation) ───────────────
        new Result("QG140-249", "Tested observable register", Category.Postdiction, 35,
            "masses, mixings, couplings, EW precision, gravity, cosmological fractions — targets KNOWN when derived"),
    };

    /// <summary>Category counts (units).</summary>
    public static IReadOnlyDictionary<Category, int> UnitCounts()
    {
        var dict = new Dictionary<Category, int>();
        foreach (Category c in Enum.GetValues<Category>()) dict[c] = 0;
        foreach (var r in Results()) dict[r.Category] += r.Units;
        return dict;
    }

    /// <summary>Total evidence units.</summary>
    public static int TotalUnits()
        => UnitCounts().Values.Sum();

    /// <summary>
    /// Methodological independence fraction: the derivation machinery never sees the target
    /// (BLIND + PRE-REGISTERED + EXTERNAL) / total.
    /// </summary>
    public static double MethodologicalFraction()
    {
        var u = UnitCounts();
        return (u[Category.BlindReconstruction] + u[Category.PreRegisteredPrediction]
                + u[Category.ExternalSupport]) / (double)TotalUnits();
    }

    /// <summary>
    /// Temporal independence fraction (strictest): the target did not exist at derivation time
    /// (PRE-REGISTERED + EXTERNAL) / total.
    /// </summary>
    public static double TemporalFraction()
    {
        var u = UnitCounts();
        return (u[Category.PreRegisteredPrediction] + u[Category.ExternalSupport]) / (double)TotalUnits();
    }

    /// <summary>Is the referee's F2 claim fully mitigated? No — 58% of units remain postdictions.</summary>
    public static bool F2FullyMitigated()
        => MethodologicalFraction() >= 0.5;

    /// <summary>
    /// Independent-evidence strength (methodological criterion): LOW &lt; 20%, MEDIUM 20-60%,
    /// HIGH &gt; 60%.
    /// </summary>
    public static string Classify()
    {
        double f = MethodologicalFraction();
        if (f >= 0.60) return "HIGH";
        if (f >= 0.20) return "MEDIUM";
        return "LOW";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var u = UnitCounts();
        return $"{Classify()} independent evidence — methodological {MethodologicalFraction():P1} "
             + $"(blind {u[Category.BlindReconstruction]} + pre-registered {u[Category.PreRegisteredPrediction]} "
             + $"+ external {u[Category.ExternalSupport]} of {TotalUnits()}); temporal (strictest) "
             + $"{TemporalFraction():P1}; postdiction {u[Category.Postdiction]}/{TotalUnits()} "
             + $"(target known)";
    }
}
