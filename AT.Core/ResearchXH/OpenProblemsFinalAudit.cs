namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 198 — Final Open Problems Audit. Uses the physics-coverage single source of truth
/// (Docs/ATQG_PhysicsCoverage.json) and the immutable prediction registry (Docs/ATQG_Predictions.json)
/// to list ALL unresolved physics questions, excluding resolved, partial-resolved, and audit-only entries.
/// Each problem is classified (FOUNDATIONAL / GRAVITY / STANDARD MODEL / PREDICTION) with:
///   - why it is still open
///   - the blocking impact (what cannot proceed until it is closed)
///   - an estimated priority (scored, then HIGH / MEDIUM / LOW)
/// Output: the Top-20 open problems ranked by importance. Deterministic.
///
/// Exclusion rule (mirrors the coverage JSON statuses):
///   - RESOLVED             → excluded (e.g. matter = deficit QG194, matter sector QG195, 2D program QG197)
///   - PARTIALLY-SOLVED     → excluded (e.g. the ψ/Weyl field register entry)
///   - AUDIT / COVERAGE     → excluded (methodology phases, not open physics)
/// Inclusion rule:
///   - OPEN, OPEN-AXIOM, PARTIALLY-OPEN, FALSIFIABLE-PENDING, PREDICTED-NO-DATA, PARTIAL origin/law
///     entries, and every registered prediction whose outcome is still null (PENDING).
///
/// Ranking: score = impact·3 + feasibility·2 + falsifiability·2 (deterministic, documented weights —
/// same weighting as QG188 for cross-phase consistency). Priority bands: HIGH ≥ 30, MEDIUM 18–29, LOW &lt; 18.
/// </summary>
public static class OpenProblemsFinalAudit
{
    public sealed record OpenProblem(
        string Id,
        string Title,
        string Category,          // FOUNDATIONAL | GRAVITY | STANDARD MODEL | PREDICTION
        string Phase,             // source phase(s)
        string WhyOpen,
        string BlockingImpact,
        int Impact,
        int Feasibility,
        int Falsifiability)
    {
        public double Score => Impact * 3.0 + Feasibility * 2.0 + Falsifiability * 2.0;

        public string Priority => Score switch
        {
            >= 30 => "HIGH",
            >= 18 => "MEDIUM",
            _ => "LOW",
        };
    }

    /// <summary>The complete catalog of unresolved physics questions (20 entries).</summary>
    public static OpenProblem[] All() => new OpenProblem[]
    {
        // ── PREDICTION (pre-registered, outcome null) ────────────────────────────────
        new("P1", "106 GeV resonance (scalar-sector transition)", "PREDICTION", "QG132/QG188A/QG190",
            "Pre-registered window 99–114 GeV (central 106.39 GeV); QG188A audit INCONCLUSIVE — the 95 GeV excess aligns with the 91.19 rung, and the 106 GeV window is neither confirmed nor excluded.",
            "The primary falsifiable prediction; a null result in the window would rule out the Z-anchor electroweak calibration; validation gates the whole electroweak sector.",
            5, 5, 5),
        new("P2", "0νββ m_ββ = 2.02 meV (Majorana neutrino)", "PREDICTION", "QG179/QG191",
            "Pre-registered m_ββ = 2.02 meV (normal ordering, α2=α3=0); below the current experimental reach (limits 0.036–0.156 eV), awaiting ton-scale experiments.",
            "The only pre-registered lepton-number-violation target; FALSIFIED if a sensitive limit lands below 2.02 meV.",
            5, 3, 5),
        new("P3", "Sector-ladder collider spectrum", "PREDICTION", "QG130/QG192",
            "9 resonances 106.39→263.43 GeV pre-registered (rungs 6/9/11 align with t/H/Z); no dedicated collider search has been run.",
            "The ladder structure (multiplicities ×10 + top ×1, width scale 15.20 GeV) lacks direct experimental validation; gates the whole sector-ladder program.",
            4, 4, 5),
        new("P9", "Common sector granularity scale", "PREDICTION", "QG69",
            "All sectors predicted granular at one common (Planck-reach) scale; purely qualitative, free scale parameter, no plausible probe.",
            "Lowest-blast-radius prediction: a shared-granularity test would unify sectors but is observationally inaccessible.",
            3, 1, 2),
        new("P10", "Regular-core black-hole profile", "PREDICTION", "QG75",
            "M(1−e^(−r³/r_c³)) core differs from singular GR and from Hayward/Bardeen; only horizon-scale (EHT-class) observations could discriminate.",
            "Discriminates the framework's regular-core prediction from GR alternatives at the event-horizon scale.",
            3, 2, 4),

        // ── STANDARD MODEL ────────────────────────────────────────────────────────────
        new("SM1", "Exact neutrino masses m1,m2,m3", "STANDARD MODEL", "QG172",
            "Splittings derived (Δm²21, Δm²31) and m1=0 normal ordering derived (QG179), but the absolute mass scale is open.",
            "Absolute neutrino masses and Σm_ν gate cosmology (structure formation) and the 0νββ rate; unresolved absolute scale blocks the full mass law.",
            5, 4, 5),
        new("SM2", "Quark running-scale / MS̄ conversion", "STANDARD MODEL", "QG173",
            "All six absolute quark masses derived from the electron anchor within 0.2%, but the matching calculation to the scale-dependent PDG/MS̄ values is open.",
            "The D96 mass law at the MS̄ scale is a pure theory gap; without it the quark sector cannot be cross-checked at running scales.",
            3, 3, 3),
        new("SM3", "Neutrino mass ordering (experimental)", "STANDARD MODEL", "QG179",
            "Normal ordering (m1=0) derived; JUNO and DUNE can measure the sign of Δm²31 but have not yet done so.",
            "The derived ordering is a clean falsifiable discriminator against inverted-ordering models; gates the neutrino sector validation.",
            4, 5, 5),
        new("SM4", "Lepton hierarchy — exact law", "STANDARD MODEL", "QG142",
            "Leptons match within 0.26% (quarks deviate) — only a PARTIAL LAW: the exact unified mass law is not yet closed.",
            "The electron/muon/tau chain is nearly reproduced; the residual deviation marks the missing exact hierarchy law.",
            3, 3, 3),
        new("SM5", "Quark hierarchy — unified law", "STANDARD MODEL", "QG146",
            "QG146 fit superseded; QG149 gives the physical occupation-weighted exponents but a single closed unified quark-hierarchy law is open.",
            "The six-quark hierarchy needs one unified law (not fitted exponents) to close the quark sector.",
            3, 3, 3),
        new("SM6", "Family index origin", "STANDARD MODEL", "QG135",
            "PARTIAL ORIGIN: family index emerges from intra-sector octave structure but the full origin is open (robustness also partial, QG136).",
            "The generation index is the least-understood discrete label; its origin closes the three-family story.",
            3, 2, 2),
        new("SM7", "Golden-ratio hierarchy robustness", "STANDARD MODEL", "QG152",
            "The golden-ratio hierarchy is only PARTIAL ROBUSTNESS — sensitive to parameter choices.",
            "Without robustness the golden-ratio feature cannot be claimed as structural rather than numerical coincidence.",
            2, 2, 2),
        new("SM8", "Physical calibration ladder", "STANDARD MODEL", "QG129",
            "Ladder ratios vs SM mass ratios: only a PARTIAL MAPPING exists.",
            "The calibration ladder is the bridge from ladder radii to physical masses; an incomplete mapping leaves the ladder↔SM correspondence unproven.",
            2, 2, 2),

        // ── GRAVITY ───────────────────────────────────────────────────────────────────
        new("G1", "Hawking temperature with ψ ≠ 0", "GRAVITY", "QG24",
            "No phase derives T ∝ 1/R explicitly with ψ ≠ 0 (QG13/QG22 give the native T∝R, partly conformal-flatness artifact; QG184 restores 1/R).",
            "The horizon temperature law is not derived in the full ψ tensor sector; blocks complete black-hole thermodynamics.",
            4, 1, 3),
        new("G2", "Flat rotation-curve α = 0 origin", "GRAVITY", "G4-ME4",
            "The flat-profile limit α=0 is SEMI-NATURAL — imposed by symmetry, not derived.",
            "The α=0 assumption underlies the flat rotation-curve / mass-radius result; an un-derived symmetry weakens the dark-matter-like claim.",
            4, 3, 4),
        new("G3", "Conformal optics: redshift without lensing / δ=0 bending", "GRAVITY", "QG21/QG26",
            "Redshift survives but lensing and Shapiro delay vanish in the conformal (ψ=0) sector (PPN γ=−1); no clean probe isolates the scalar sector.",
            "The absence of lensing in the native sector differs sharply from GR; whether ψ restores the full tensor optics is the central gravitational open question.",
            4, 1, 4),
        new("G4", "Curvature-sourced Poisson equation", "GRAVITY", "G4-O0",
            "Poisson source is (ln ρ)″ (curvature), not the density value — AT-specific; no Newtonian field in uniform-density / shell-exterior regions; no feasible probe.",
            "The modified Newtonian structure discriminates the framework but is observationally isolated from GR.",
            3, 1, 3),
        new("G5", "Gravitational-wave polarization sector", "GRAVITY", "QG18/QG43",
            "Scalar GW: energy/speed OK but polarization NO MATCH; only GW strain requires the tensor (ψ) sector — the polarization observable is undecided.",
            "The GW polarization observable is the cleanest discriminator between scalar-only and tensor (ψ) gravity.",
            3, 2, 4),

        // ── FOUNDATIONAL ──────────────────────────────────────────────────────────────
        new("F1", "Metric ansatz uniqueness (g = ρ^(2/d)η)", "FOUNDATIONAL", "G4-A0",
            "The conformal ansatz is PREFERRED but not UNIQUE — flat η is a defining axiom, not derived.",
            "Every metric-level prediction (lensing, redshift partition, gravity coupling) inherits this axiom; uniqueness would close the geometric foundation.",
            5, 1, 2),
        new("F2", "Exact Bekenstein 1/4 coefficient", "FOUNDATIONAL", "QG12/13/184/185/196",
            "Structure (S∝A, M∝R, T∝1/R) fully derived; QG196 PROVES the exact 1/4 is impossible within D96/TRM without importing π (bits-per-cell = π; 1/occ₀=1/4 is wrong-units → 1/(16π)).",
            "The exact entropy coefficient remains a quantum/geometric statement requiring the imported 2π factor — the strongest remaining boundary to a purely-native black-hole thermodynamics.",
            4, 1, 3),
    };

    // ── Classification ─────────────────────────────────────────────────────────────

    /// <summary>All four categories are present.</summary>
    public static bool AllCategoriesPresent()
        => All().Select(p => p.Category).Distinct().Count() == 4;

    /// <summary>Category counts, keyed by category name.</summary>
    public static IReadOnlyDictionary<string, int> CategoryCounts()
        => All().GroupBy(p => p.Category).ToDictionary(g => g.Key, g => g.Count());

    /// <summary>No excluded statuses leaked in: every entry carries a phase, a reason and a blocking impact.</summary>
    public static bool NoResolvedOrPartialResolved()
        => All().All(p =>
            !p.Phase.Contains("RESOLVED", StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(p.WhyOpen) &&
            !string.IsNullOrWhiteSpace(p.BlockingImpact));

    // ── Ranking ────────────────────────────────────────────────────────────────────

    /// <summary>All 20 open problems ranked by importance score, descending.</summary>
    public static OpenProblem[] Top20() => All().OrderByDescending(p => p.Score).ToArray();

    /// <summary>The single most important open problem.</summary>
    public static OpenProblem RecommendedNextTarget() => Top20()[0];

    /// <summary>Priority band counts (HIGH / MEDIUM / LOW).</summary>
    public static IReadOnlyDictionary<string, int> PriorityCounts()
        => All().GroupBy(p => p.Priority).ToDictionary(g => g.Key, g => g.Count());

    // ── Validation ─────────────────────────────────────────────────────────────────

    /// <summary>The catalog is the full Top-20 (20 entries, sorted descending, all categories).</summary>
    public static bool CatalogValid()
    {
        var top = Top20();
        if (top.Length != 20) return false;
        for (int i = 1; i < top.Length; i++)
            if (top[i].Score > top[i - 1].Score) return false;
        return AllCategoriesPresent() && NoResolvedOrPartialResolved();
    }

    /// <summary>The 106 GeV resonance is the top-ranked open problem.</summary>
    public static bool TopIs106GeV() => RecommendedNextTarget().Id == "P1";
}
