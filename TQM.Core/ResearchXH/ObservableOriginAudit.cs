namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 259 — Observable Origin Audit. The QUESTION: for each major result (masses, couplings,
/// mixings, cosmology, GR observables) was the OBSERVABLE selected because a D96 formula matched it
/// (post-hoc), or because D96 naturally points to it (structural / temporal independence)?
///
/// This is an OBSERVABLE audit, NOT a formula audit (no formula complexity, no uniqueness — see QG253
/// for that). It asks only: where did the TARGET come from?
///
/// THE CLASSIFICATION (deterministic, evidence-based):
///   NATURAL TARGET  — D96 structure alone would lead to this observable even without the SM/GR/
///                     cosmology catalog: a structural identity (family count, θ_QCD = 0), a class
///                     forced by the octave organization (lepton ratios), OR the value was frozen /
///                     hidden before measurement (P1/P2/P3 pre-registered, QG176 blind Higgs, QG177
///                     leave-one-out). The observable is D96's own output, not a catalog pick.
///   SECONDARY TARGET — the observable is a standard catalog value (a measured SM/GR/cosmology
///                     quantity); D96 produces a quantity of the right CLASS (dimensionless spectral
///                     ratio, gauge degree, hierarchy), but the specific target was selected from the
///                     measured catalog with its value known at derivation time. D96 class-consistent,
///                     catalog-driven.
///   POST-HOC TARGET — the observable entered the target register BECAUSE a formula matched it, with
///                     no independent D96 pointer: explicitly flagged retro-selection (QG239: n_s,
///                     acoustic peaks), asserted dictionary (QG250: 1/α_em = Σm+#d = 137), or the
///                     superseded fitting era (QG189: QG140/146 RETRO-FIT, QG147 OVERFIT).
///
/// THE EVIDENCE BASE (previous audits):
///   QG239  — formula selection audit: 1 UNIQUE (Λ scaling) / 3 PREFERRED / 2 RETRO-SELECTION RISK
///            (n_s, acoustic peaks) — the observable-level retro flags;
///   QG250  — external referee: F1 parameter leakage + MAJOR "1/α_em=137=Σm+#d asserted dictionary";
///   QG189  — anti-fit audit: 36 PREDICTION / 2 BLIND / 8 DEPENDENT / 2 RETRO-FIT (QG140/146) /
///            1 OVERFIT (QG147), all fitting-era confined to QG140-148;
///   QG252  — independent evidence: 35 postdiction / 21 blind / 3 pre-registered / 1 external;
///   QG253  — formula uniqueness (4/7 published formulas have simpler non-native alternatives) —
///            evidence of target-informed FORMULA choice, referenced here only where it flags the
///            observable as catalog-driven;
///   QG185/196 — Bekenstein: D96 CANNOT produce 1/4 (impossibility proof) — an honest FAILED match,
///            the strongest evidence that not every catalog item is matched.
///
/// THE DETERMINATION (computed from the table below, not asserted):
///   The observable register is predominantly CATALOG-DRIVEN (SECONDARY dominant): the target set
///   mirrors the measured SM/cosmology/GR catalog, so most observables were selected because a known
///   value exists to match. A genuine NATURAL core exists (octave ratios, family count, pre-registered
///   ladder, blind Higgs) and is temporally independent. A small POST-HOC minority (n_s, acoustic
///   peaks, 1/α_em dictionary) is explicitly flagged. The honest verdict is an observable-selection
///   risk of MEDIUM: the register was substantially target-informed, but the structural core and the
///   honest Bekenstein failure show the selection is not pure retro-fitting.
///
/// CLASSIFICATION: MEDIUM observable-selection risk.
/// </summary>
public static class ObservableOriginAudit
{
    public enum Category { Masses, Couplings, Mixings, Cosmology, GeneralRelativity }
    public enum Origin { NaturalTarget, SecondaryTarget, PostHocTarget }

    /// <summary>A major observable and its selection-origin evidence.</summary>
    public sealed record Observable(
        string Name,
        string Phase,
        Category Cat,
        Origin Origin,
        bool StructuralPointer,
        bool TemporalIndependence,
        string Evidence);

    /// <summary>
    /// The observable register. Deterministic — this is the audited record of QG140-258, no randomness.
    /// Each entry's Origin follows from (StructuralPointer, TemporalIndependence, explicit retro flags).
    /// </summary>
    public static Observable[] Observables() => new[]
    {
        // ── MASSES ──────────────────────────────────────────────────────────────────────────────
        new Observable("Family count = 3", "QG138/QG210", Category.Masses, Origin.NaturalTarget,
            true, true, "floor(log2(span))+1 = 3 — exact structural identity; D96 alone answers 'why 3'"),
        new Observable("Lepton hierarchy m_μ/m_e, m_τ/m_μ", "QG140-149/QG209", Category.Masses, Origin.SecondaryTarget,
            true, false, "octave-ratio CLASS is natural, but values known at derivation; QG140/146 originally RETRO-FIT (superseded by QG149 octave law)"),
        new Observable("Quark masses (6)", "QG173", Category.Masses, Origin.SecondaryTarget,
            true, false, "spectral-density hierarchy class natural; absolute values known, anchored at me"),
        new Observable("Neutrino masses Δm²21/Δm²31", "QG172", Category.Masses, Origin.SecondaryTarget,
            true, false, "D96-ratio class; oscillation values known at derivation"),
        new Observable("Higgs mass MH (blind)", "QG176", Category.Masses, Origin.NaturalTarget,
            true, true, "HIDDEN target — rebuilt from pre-Higgs D96 only (0.19%); temporal blindness"),
        new Observable("MH/MW, MH/MZ, λ_H (blind)", "QG176", Category.Masses, Origin.NaturalTarget,
            true, true, "same blind reconstruction; no Higgs input entered the derivation"),
        new Observable("Weak boson masses MW/MZ", "QG168", Category.Masses, Origin.SecondaryTarget,
            true, false, "v = (Σm+#d)·ln span = 254 GeV; catalog values known at derivation"),
        new Observable("P1 — 106 GeV resonance", "QG190", Category.Masses, Origin.NaturalTarget,
            true, true, "PRE-REGISTERED — frozen from D96/QG128-132 before measurement; forbidden ATLAS/CMS input"),
        new Observable("P2 — m_ββ = 2.02 meV", "QG191", Category.Masses, Origin.NaturalTarget,
            true, true, "PRE-REGISTERED — frozen from QG167/172/179 before measurement; forbidden limits input"),
        new Observable("P3 — sector ladder (9 rungs)", "QG192", Category.Masses, Origin.NaturalTarget,
            true, true, "PRE-REGISTERED — 12-rung ladder frozen from QG121-132 before measurement"),

        // ── COUPLINGS ───────────────────────────────────────────────────────────────────────────
        new Observable("1/α_em = 137", "QG162", Category.Couplings, Origin.PostHocTarget,
            false, false, "QG250: 'asserted dictionary' — Σm+#d = 137 matched to the iconic catalog value with no D96 pointer to fine-structure constant"),
        new Observable("α_weak, α_strong", "QG162", Category.Couplings, Origin.SecondaryTarget,
            true, false, "gauge degree 1+3+8=12 IS structural (degree of C_96), but specific coupling values matched from catalog"),
        new Observable("sin²θ_W = 0.2316", "QG162/QG175", Category.Couplings, Origin.SecondaryTarget,
            true, false, "EW-mixing-parameter class natural from group count; catalog value known at derivation"),
        new Observable("Muon g-2 (a_μ)", "QG171", Category.Couplings, Origin.SecondaryTarget,
            true, false, "Schwinger + spectral-fraction class natural; measured a_μ known at derivation"),
        new Observable("Electron g-2 (a_e)", "QG178", Category.Couplings, Origin.SecondaryTarget,
            true, false, "same mechanism; measured a_e known at derivation"),
        new Observable("Yukawa ratios y_τ/y_μ, y_t/y_b", "QG247", Category.Couplings, Origin.SecondaryTarget,
            true, false, "occupation-coupling octave identities class natural; ratios known at derivation"),
        new Observable("θ_QCD = 0 (strong CP)", "QG174", Category.Couplings, Origin.NaturalTarget,
            true, true, "EXACT structural identity — reflection automorphism [L,P]=0 forces real masses, arg det M = 0; no number to match"),

        // ── MIXINGS ─────────────────────────────────────────────────────────────────────────────
        new Observable("CKM Vus/Vcb/Vub", "QG165", Category.Mixings, Origin.SecondaryTarget,
            true, false, "unitary-ratio class natural; measured angles known at derivation (mean dev 0.58%)"),
        new Observable("CKM CP δ_CP, J", "QG166", Category.Mixings, Origin.SecondaryTarget,
            true, false, "chiral-rotation class natural; measured δ known at derivation"),
        new Observable("PMNS θ12/θ23/θ13/δ_ν", "QG167", Category.Mixings, Origin.SecondaryTarget,
            true, false, "unitary-ratio class natural; measured angles known at derivation (mean dev 1.5%)"),

        // ── COSMOLOGY ───────────────────────────────────────────────────────────────────────────
        new Observable("Spectral index n_s", "QG237", Category.Cosmology, Origin.PostHocTarget,
            false, false, "QG239 explicit RETRO-SELECTION RISK — formula selected to match measured n_s; no D96 pointer to the CMB spectral index"),
        new Observable("Acoustic peaks ℓ₁, ℓ₂/ℓ₁, ℓ₃/ℓ₁", "QG238", Category.Cosmology, Origin.PostHocTarget,
            false, false, "QG239 explicit RETRO-SELECTION RISK — peak ratios matched to measured CMB values"),
        new Observable("Cosmological constant Λ", "QG230", Category.Cosmology, Origin.SecondaryTarget,
            true, false, "residual-pressure class IS structural (existence, sign, Λ∝1/R² scaling); numeric value matched via Ω_Λ"),
        new Observable("Density fractions Ω_Λ, Ω_m", "QG234", Category.Cosmology, Origin.SecondaryTarget,
            true, false, "I_occ/ln K ratio class natural; Planck fractions known at derivation"),

        // ── GENERAL RELATIVITY OBSERVABLES ───────────────────────────────────────────────────────
        new Observable("Newton constant G / M_Pl", "QG181", Category.GeneralRelativity, Origin.SecondaryTarget,
            true, false, "occupation-weighted scale class natural; measured G known at derivation"),
        new Observable("Mass-radius M ∝ R", "QG184", Category.GeneralRelativity, Origin.SecondaryTarget,
            true, false, "per-octave deficit derivation class natural; observed rotation-catalog scaling known"),
        new Observable("Bekenstein S = A/4", "QG185/QG196", Category.GeneralRelativity, Origin.SecondaryTarget,
            false, false, "catalog target that D96 CANNOT match (impossibility proof, 1/4 needs imported π) — honest FAILED match, anti-retro evidence"),
        new Observable("Frame dragging (GP-B)", "QG186", Category.GeneralRelativity, Origin.SecondaryTarget,
            true, false, "h_0i sector class natural; measured GP-B/LAGEOS known at derivation"),
        new Observable("GPS time dilation", "QG187", Category.GeneralRelativity, Origin.SecondaryTarget,
            true, false, "QG21 redshift law class natural; measured +38.6 μs/day known at derivation"),
    };

    /// <summary>Count of observables per origin class.</summary>
    public static IReadOnlyDictionary<Origin, int> Counts()
    {
        var dict = new Dictionary<Origin, int>();
        foreach (Origin o in Enum.GetValues<Origin>()) dict[o] = 0;
        foreach (var obs in Observables()) dict[obs.Origin]++;
        return dict;
    }

    /// <summary>Count of observables per category.</summary>
    public static IReadOnlyDictionary<Category, int> CategoryCounts()
    {
        var dict = new Dictionary<Category, int>();
        foreach (Category c in Enum.GetValues<Category>()) dict[c] = 0;
        foreach (var obs in Observables()) dict[obs.Cat]++;
        return dict;
    }

    /// <summary>Total observable entries.</summary>
    public static int Total() => Observables().Length;

    /// <summary>
    /// Observable-selection risk: a weighted fraction where NATURAL contributes 0 risk, SECONDARY 0.5
    /// (catalog-driven but D96-class-consistent) and POST-HOC 1.0 (explicitly retro-selected).
    /// </summary>
    public static double RiskScore()
    {
        var c = Counts();
        return (0.5 * c[Origin.SecondaryTarget] + 1.0 * c[Origin.PostHocTarget]) / (double)Total();
    }

    /// <summary>
    /// Risk class: LOW &lt; 0.25, MEDIUM &lt; 0.60, HIGH ≥ 0.60.
    /// </summary>
    public static string ClassifyRisk()
    {
        double r = RiskScore();
        if (r >= 0.60) return "HIGH";
        if (r >= 0.25) return "MEDIUM";
        return "LOW";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var c = Counts();
        return $"{ClassifyRisk()} observable-selection risk — risk score {RiskScore():F3} "
             + $"(natural {c[Origin.NaturalTarget]} / secondary {c[Origin.SecondaryTarget]} / "
             + $"post-hoc {c[Origin.PostHocTarget]} of {Total()} observables; "
             + $"natural fraction {NaturalFraction():P1})";
    }

    /// <summary>Fraction of observables that are D96-natural targets.</summary>
    public static double NaturalFraction()
        => Counts()[Origin.NaturalTarget] / (double)Total();
}
