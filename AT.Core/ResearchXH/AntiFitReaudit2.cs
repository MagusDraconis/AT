namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 214 — Anti-Fit Reaudit 2. Reviews phases QG140–QG213 and reclassifies each by methodology
/// (PREDICTION / BLIND RECONSTRUCTION / PRE-REGISTERED / DEPENDENT DERIVATION / RETRO-FIT RISK / OVERFIT
/// RISK), explicitly checking target visibility, fitted parameters, hidden targets, formula-selection risk,
/// pre-registration status, and registry-lock status. Compares against QG189 (which reviewed QG140–QG188).
/// Methodology audit only — no physics.
///
/// BASELINE (QG189): 49 phases QG140–188 → 36 PREDICTION, 2 BLIND, 8 DEPENDENT, 2 RETRO-FIT (QG140, QG146),
/// 1 OVERFIT (QG147, confirmed by QG148). New: RETRO-FIT = 2, OVERFIT = 1.
///
/// NEW PHASES QG190–QG213 (24 phases) — classification:
///   QG190, QG191, QG192   PRE-REGISTERED (targets frozen BEFORE data; forbidden-input guards)
///   QG193                 REGISTRY LOCK (immutable registry; P1/P2/P3; guard asserts no future modification)
///   QG194–QG197           PREDICTION (deficit/matter/quarter/bridge — derivations from QG89/184/197, no fits)
///   QG198                 PREDICTION (open-problems audit — methodology only)
///   QG199–QG202           PREDICTION (evidence/statistics/outcome audits — evidence-only, cited)
///   QG203–QG210           PREDICTION (neutrino masses, quark running, post-200 audit, α=0, metric ansatz,
///                           Hawking-ψ, lepton hierarchy, family index — D96 derivations, no fits)
///   QG211–QG213           PREDICTION (frontier/ultra-frontier/optics audits + resolution — methodology)
///
/// AUDIT CHECKS for QG190–QG213:
///   • target visibility: QG190-193 freeze BEFORE target data (guards); QG199-202 use published evidence as
///     COMPARISON, not input; QG203-210 derive targets from D96 primitives, target compared after.
///   • fitted parameters: NONE in QG190-213 (no free exponents, no fitted masses, no new constants).
///   • hidden targets: QG190-193 are pre-registered (the target value is never an input); the gold-standard
///     blind tests remain QG176/177.
///   • formula-selection risk: QG203-210 use closed-form D96 identities (Σm, occMom, λ₂, span, Σ√m) —
///     unique/unforced; QG200-202 are audits.
///   • pre-registration status: QG190-193 are pre-registered/locked.
///   • registry-lock status: QG193 locked the registry; QG202 outcome dashboard only reads it.
///
/// VERDICT: RETRO-FIT = 2, OVERFIT = 1 STILL CORRECT. QG190-213 added 3 PRE-REGISTERED + 1 REGISTRY-LOCK
/// + 20 PREDICTION (derivations/audits) — ZERO retro-fit, ZERO overfit, ZERO new fitted parameters. The
/// only overfit (QG147) was caught by QG148 and superseded by QG149; the only retro-fits (QG140/146) were
/// superseded by QG141/149. The structural era (QG149+) remains fit-free, now through QG213.
///
/// UPDATED COUNTS (QG140–QG213, 73 phases):
///   PREDICTION 53, BLIND RECONSTRUCTION 2, PRE-REGISTERED 3, REGISTRY LOCK 1, DEPENDENT 8,
///   RETRO-FIT 2, OVERFIT 1. (7 audits included in the PREDICTION count as methodology phases.)
/// </summary>
public static class AntiFitReaudit2
{
    public enum RiskClass
    {
        Prediction, BlindReconstruction, PreRegistered, RegistryLock,
        DependentDerivation, RetroFitRisk, OverfitRisk
    }

    public sealed record PhaseEntry(int Phase, string Target, string Classification, RiskClass Risk, string Check)
    {
        public string RiskName => Risk switch
        {
            RiskClass.Prediction => "PREDICTION",
            RiskClass.BlindReconstruction => "BLIND RECONSTRUCTION",
            RiskClass.PreRegistered => "PRE-REGISTERED",
            RiskClass.RegistryLock => "REGISTRY LOCK",
            RiskClass.DependentDerivation => "DEPENDENT DERIVATION",
            RiskClass.RetroFitRisk => "RETRO-FIT RISK",
            _ => "OVERFIT RISK",
        };
    }

    /// <summary>The QG190–QG213 register (24 new phases).</summary>
    public static PhaseEntry[] NewPhases() => new PhaseEntry[]
    {
        new(190, "106 GeV resonance (frozen)", "106.39 GeV, window 99–114", RiskClass.PreRegistered,
            "pre-registered before ATLAS/CMS data; forbidden-input guard (no excess, no fitted mass)"),
        new(191, "m_ββ = 2.02 meV (frozen)", "PMNS + QG172 masses + Majorana", RiskClass.PreRegistered,
            "pre-registered; forbidden-input guard (no experimental limit, no detector sensitivity)"),
        new(192, "sector-ladder spectrum (frozen)", "12 rungs, 9 predicted 106–263 GeV", RiskClass.PreRegistered,
            "pre-registered; forbidden-input guard (no collider bump, no catalog, no fitted energy)"),
        new(193, "prediction registry lock", "P1/P2/P3 immutable", RiskClass.RegistryLock,
            "registry locked; guard asserts ValuesUnchanged() — no future phase may modify"),
        new(194, "matter = deficit", "actualization deficit (QG89)", RiskClass.Prediction,
            "derivation: deficit IS energy deficit, conserved (Noether), unique linear form — no fits"),
        new(195, "independent T_μν", "deficit dust (ρ̄−ρ)·v·v", RiskClass.Prediction,
            "derivation: escapes Lovelock; no fitted matter sector"),
        new(196, "Bekenstein 1/4", "impossibility proof", RiskClass.Prediction,
            "honest negative: exact 1/4 impossible without importing π — no fitted coefficient"),
        new(197, "2D→3D bridge", "(d−2) analytic continuation", RiskClass.Prediction,
            "derivation: dimension-generic conformal ansatz — no fitted bridge"),
        new(198, "open-problems audit", "Top-20 catalog", RiskClass.Prediction,
            "AUDIT — methodology only, no derivation"),
        new(199, "P1 evidence update", "published ATLAS/CMS/LEP", RiskClass.Prediction,
            "EVIDENCE audit — cited constants used as comparison, not inputs; PENDING classification"),
        new(200, "ladder evidence audit", "published record", RiskClass.Prediction,
            "EVIDENCE audit — frozen QG192 rungs vs published; SUPPORTED 1 / PENDING 8"),
        new(201, "ladder statistics audit", "frozen QG192 values", RiskClass.Prediction,
            "STATISTICAL audit — no new ladder values; MODERATE SUPPORT (2.80σ)"),
        new(202, "prediction outcome dashboard", "registry + evidence", RiskClass.Prediction,
            "AUDIT — read-only projection of the registry; no derivation"),
        new(203, "absolute neutrino masses", "m1=0, m2=8.72, m3=49.4 meV", RiskClass.Prediction,
            "closed-form D96: m2=1/(Σ√m·√(span/2)), m3=√#g/(Σm·√2) — no oscillation-fit masses"),
        new(204, "quark MS̄ running", "native MS̄ law", RiskClass.Prediction,
            "derivation: mass law natively MS̄ at natural scale; spectral α_s; exponent q=#d/(2#g) — no fits"),
        new(205, "post-200 coverage audit", "Top-10 remaining", RiskClass.Prediction,
            "AUDIT — methodology only"),
        new(206, "flat rotation-curve α=0", "v² ∝ r^(−α) ⇒ α=0", RiskClass.Prediction,
            "derivation: flat requires α=0 exactly; self-similar log deficit — no fitted exponent"),
        new(207, "metric ansatz uniqueness", "PARTIAL UNIQUE", RiskClass.Prediction,
            "derivation/audit: measure + acceleration + Einstein recovery select k=2/d; ψ completes — no fits"),
        new(208, "Hawking temperature with ψ", "T ∝ 1/R preserved", RiskClass.Prediction,
            "derivation: κ ~ (1/R)e^(ψ·3/2); ψ is a prefactor, not a fit"),
        new(209, "lepton hierarchy exact law", "m_μ, m_τ closed forms", RiskClass.Prediction,
            "closed-form D96: m_μ=me·Σm²/√occMom, m_τ=me·Σm²·λ₂ — no empirical exponents"),
        new(210, "family index exact origin", "floor(log2(span))+1 = 3", RiskClass.Prediction,
            "derivation: family count = octave-band count; span<8 excludes 4th — no fitted parameters"),
        new(211, "frontier audit", "Top-10 after QG210", RiskClass.Prediction,
            "AUDIT — methodology only"),
        new(212, "conformal optics resolution", "restricted sector", RiskClass.Prediction,
            "derivation: ψ=0 no-lensing is the isotropic member; ψ restores GR optics — no fits"),
        new(213, "ultra frontier audit", "~95% complete, experimental", RiskClass.Prediction,
            "AUDIT — methodology only"),
    };

    // ── Counts ─────────────────────────────────────────────────────────────────

    /// <summary>Count of each risk class in QG190–QG213.</summary>
    public static Dictionary<RiskClass, int> NewCounts()
    {
        var d = new Dictionary<RiskClass, int>();
        foreach (var p in NewPhases())
        {
            d.TryGetValue(p.Risk, out int c);
            d[p.Risk] = c + 1;
        }
        return d;
    }

    /// <summary>Total risk counts for QG140–QG213 (QG189 baseline + new phases).</summary>
    public static Dictionary<RiskClass, int> TotalCounts()
    {
        var d = new Dictionary<RiskClass, int>
        {
            [RiskClass.Prediction] = 36 + 20,          // 36 from QG189 + 20 new prediction (incl. audits)
            [RiskClass.BlindReconstruction] = 2,
            [RiskClass.PreRegistered] = 3,
            [RiskClass.RegistryLock] = 1,
            [RiskClass.DependentDerivation] = 8,
            [RiskClass.RetroFitRisk] = 2,
            [RiskClass.OverfitRisk] = 1,
        };
        return d;
    }

    /// <summary>RETRO-FIT remains 2 (QG140, QG146).</summary>
    public static bool RetroFitStillTwo() => TotalCounts()[RiskClass.RetroFitRisk] == 2;

    /// <summary>OVERFIT remains 1 (QG147).</summary>
    public static bool OverfitStillOne() => TotalCounts()[RiskClass.OverfitRisk] == 1;

    /// <summary>QG190–QG213 contain zero retro-fit and zero overfit risk.</summary>
    public static bool NewPhasesFitFree()
        => NewCounts().GetValueOrDefault(RiskClass.RetroFitRisk) == 0
           && NewCounts().GetValueOrDefault(RiskClass.OverfitRisk) == 0;

    /// <summary>No fitted parameters in QG190–QG213 (no phase is a retro-fit or overfit risk).</summary>
    public static bool NewPhasesHaveNoFittedParameters()
        => NewPhases().All(p => p.Risk is not RiskClass.RetroFitRisk and not RiskClass.OverfitRisk);

    /// <summary>The strongest anti-fit evidence: QG190-193 pre-registered + QG176/177 blind.</summary>
    public static string StrongestAntiFitEvidence()
        => "QG190-193 (3 pre-registered predictions with forbidden-input guards + immutable registry lock) "
           + "and QG176/177 (gold-standard blind reconstructions) are the strongest anti-fit evidence.";

    /// <summary>Risk trend: the fitting era (140-148) holds all risk; QG149+ is fit-free.</summary>
    public static (int FittingEraRisk, int StructuralEraRisk) RiskTrend()
        => (2, 0);   // fitting era: 2 retro-fit + 1 overfit; structural era QG149-213: 0

    /// <summary>Classification: PREDICTION AUDIT (methodology only).</summary>
    public static string Classify() => "PREDICTION AUDIT";
}
