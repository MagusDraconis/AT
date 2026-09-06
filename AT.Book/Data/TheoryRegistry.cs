using AT.Book.Domain;

namespace AT.Book.Data;

/// <summary>
/// The canonical theory registry: every theory object and audit, seeded in code (no
/// markdown pages). This is the single source of truth the whole book renders from.
/// </summary>
public sealed class TheoryRegistry
{
    public IReadOnlyList<TheoryObject> Objects { get; }
    public IReadOnlyList<TheoryAudit> Audits { get; }
    public IReadOnlyDictionary<string, TheoryObject> ObjectById { get; }
    public IReadOnlyDictionary<string, TheoryAudit> AuditById { get; }

    public TheoryRegistry()
    {
        Audits = SeedAudits();
        AuditById = Audits.ToDictionary(a => a.Id, StringComparer.OrdinalIgnoreCase);
        Objects = SeedObjects();
        ObjectById = Objects.ToDictionary(o => o.Id, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// The audits that constitute evidence for a given theory object (or chapter) id:
    /// audits that depend on it, plus audits explicitly listed on the matching object.
    /// </summary>
    public IReadOnlyList<TheoryAudit> AuditsFor(string id)
    {
        var result = new List<TheoryAudit>();

        foreach (var a in Audits)
            if (a.Dependencies.Any(d => string.Equals(d, id, StringComparison.OrdinalIgnoreCase)))
                result.Add(a);

        if (ObjectById.TryGetValue(id, out var obj) && obj.AuditIds is not null)
            foreach (var auditId in obj.AuditIds)
                if (AuditById.TryGetValue(auditId, out var audit) && !result.Contains(audit))
                    result.Add(audit);

        return result;
    }

    private static List<TheoryObject> SeedObjects() =>
    [
        // ── Layer 0 — Foundations ──────────────────────────────────────────────
        new("difference", "Difference", "The founding primitive: things can differ. Difference IS distinguishability.",
            TheoryLayer.Foundations, TheoryClassification.Boundary, TheoryObjectKind.Primitive, []),
        new("eta", "η", "The second primitive η (the complementary structural parameter).",
            TheoryLayer.Foundations, TheoryClassification.Boundary, TheoryObjectKind.Primitive, []),
        new("actualization", "Actualization", "Difference → Actualization: the discrete tick θ_k = 2πk/N that makes distinctions actual.",
            TheoryLayer.Foundations, TheoryClassification.Derived, TheoryObjectKind.Chapter, ["difference"],
            Narrative: "Actualization is the discrete step by which a potential distinction becomes actual. Its tick is the final canonical boundary (Δθ = 2πk/N).",
            Formula: "θ_k = 2πk/N,  N = 96"),
        new("emergence", "Emergence", "Higher-layer structure emerges from the foundational primitives via derivation.",
            TheoryLayer.Foundations, TheoryClassification.Emergent, TheoryObjectKind.Chapter, ["actualization"]),
        new("boundaries", "Boundaries", "The five-item irreducible boundary set: {Difference, η}, {Z2-paired sector}, {3 octave families}, {SU(2) gauge + j=1/2}, {v, m_e}.",
            TheoryLayer.Foundations, TheoryClassification.Boundary, TheoryObjectKind.Boundary, [],
            References: ["R_001", "D_040"]),

        // ── Layer 1 — Structure ────────────────────────────────────────────────
        new("d96", "D96 Structure Sector", "The canonical 96-site circulant ring C_96(±1..±6): the single structure sector of AT.",
            TheoryLayer.Structure, TheoryClassification.Derived, TheoryObjectKind.Chapter,
            ["difference", "actualization"],
            Narrative: "Difference applied over 96 sites with a ±1..±6 nearest-neighbour coupling gives the D96 spectrum: 95 positive modes, a band [ω₁, ω_max] with span 6.4025, and the octave occupancy [4,4,87]. The ring is 1D — a single integer mode index k gives a linear low-frequency dispersion, so its DOS exponent is p = 1.",
            Formula: "λ_k = Σ_{s=1..6} 2(1 − cos(2πks/96)),  ω_k = √λ_k,  span = ω_max/ω₁ = 6.4025",
            CalculationId: "spectrum",
            References: ["D_008", "D_030", "NP_032", "NP_035"],
            AuditIds: ["np035"]),
        new("occupancy", "Occupancy", "The octave occupancy [4,4,87]: the top-heavy distribution of the 95 modes.",
            TheoryLayer.Structure, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["d96"],
            Formula: "octave_k = #modes with ω ∈ [2^{k−1}ω₁, 2^k ω₁)",
            CalculationId: "occupancy"),
        new("resonance", "Resonance", "The circulant spectrum's mode structure: mirror pairs and the central mode.",
            TheoryLayer.Structure, TheoryClassification.Derived, TheoryObjectKind.Chapter, ["d96"],
            Formula: "λ_k = λ_{N−k} (O(2) mirror degeneracy)"),
        new("symmetry", "Symmetry", "The Z2-paired (complex) sector: observable states come in mirror pairs.",
            TheoryLayer.Structure, TheoryClassification.Boundary, TheoryObjectKind.Boundary, ["resonance"],
            References: ["D_020", "D_021"]),

        // ── Layer 2 — Information ──────────────────────────────────────────────
        new("information-content", "Information Content", "The information density over the occupancy: how non-uniform the D96 mode set is.",
            TheoryLayer.Information, TheoryClassification.Derived, TheoryObjectKind.Chapter, ["occupancy"],
            Formula: "I = KL(ρ ‖ uniform) = Σ ρ_i · ln(ρ_i / (1/K))"),
        new("iocc", "I_occ", "I_occ = KL(ρ‖uniform) = 0.7513 nats — the derived order parameter.",
            TheoryLayer.Information, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["information-content"],
            Formula: "I_occ = Σ ρ_i · ln(ρ_i / (1/K)),  ρ = [4,4,87]/95",
            CalculationId: "iocc",
            References: ["QG_228"]),
        new("kl-selection", "KL Selection", "The Kullback–Leibler selection: the uniform distribution is the reference for information density.",
            TheoryLayer.Information, TheoryClassification.Derived, TheoryObjectKind.Definition,
            ["information-content"],
            Formula: "KL(p‖q) = Σ p_i ln(p_i/q_i)"),

        // ── Layer 3 — Cosmology ────────────────────────────────────────────────
        new("omega-lambda", "ΩΛ — Information Cosmology", "The dark-energy fraction ΩΛ = I_occ/ln K = 0.6839 — a derived information observable (normalized entropy deficit), matched to observation to 0.12%.",
            TheoryLayer.Cosmology, TheoryClassification.Derived, TheoryObjectKind.Chapter,
            ["iocc"],
            Narrative: "Information cosmology: the information density I_occ fixes the density-fraction pair exactly. ΩΛ = I_occ/ln K = 0.7513/ln 3 = 0.6839 matches the observed dark-energy fraction to 0.12%, and Ωm = 1 − ΩΛ = 0.3161. ΩΛ is the normalized ENTROPY DEFICIT — the information surplus of the top-heavy [4,4,87] occupancy over the uniform reference — a DERIVED state descriptor (order parameter), not a cause and not an energy reservoir. The ontology arc (NP_055–NP_064) established that the physical (energy) reading is HOSTED: the information → energy-density bridge rests on the definition 'energy = actualization rate' (QG_089) plus dimensionful anchors, so the match is a genuine correspondence, not a derived physical relation.",
            Formula: "ΩΛ = I_occ / ln K = 0.6839,  Ωm = (ln K − I_occ)/ln K = 0.3161",
            CalculationId: "omegalambda",
            References: ["QG_234", "QG_228"],
            AuditIds: ["np055", "np056", "np057", "np058", "np059", "np060", "np061", "np063"]),
        new("omega-matter", "Ωm", "The matter fraction Ωm = H/ln K = 0.3161 — the realized-entropy fraction (the deficit side, matter = ρ̄ − ρ).",
            TheoryLayer.Cosmology, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["iocc"],
            Narrative: "Ωm is the complement of ΩΛ: the realized-entropy fraction H/ln K, read as the matter deficit m = ρ̄ − ρ (QG194). Matter is the under-occupancy of the counting measure — an effect, not a particle — that sources gravity (T_μν = (ρ̄−ρ)v_μv_ν, flat rotation α=0, M∝R). Unlike the descriptive dark-energy surplus, the deficit has a DERIVED gravitational role (NP_065).",
            Formula: "Ωm = H/ln K = (ln K − I_occ)/ln K = 0.3161",
            References: ["QG_234", "QG_194"],
            AuditIds: ["np065"]),
        new("q0", "q₀ — Deceleration Parameter", "The deceleration parameter q₀ = Ωm/2 − ΩΛ ≈ −0.526 — a hosted FRW closure (assumes w = −1).",
            TheoryLayer.Cosmology, TheoryClassification.Correspondence, TheoryObjectKind.Derivation,
            ["omega-lambda", "omega-matter"],
            Formula: "q₀ = Ωm/2 − ΩΛ",
            CalculationId: "deceleration"),
        new("zacc", "z_acc — Acceleration Redshift", "The transition redshift z_acc = (2ΩΛ/Ωm)^(1/3) − 1 ≈ 0.63 — a hosted FRW closure (assumes w = −1).",
            TheoryLayer.Cosmology, TheoryClassification.Correspondence, TheoryObjectKind.Derivation,
            ["omega-lambda", "omega-matter"],
            Formula: "z_acc = (2ΩΛ/Ωm)^(1/3) − 1",
            CalculationId: "acceleration-redshift"),

        // ── Layer 4 — Physics ──────────────────────────────────────────────────
        new("families", "Three Families", "The three octave families: family count = floor(log₂ span) + 1 = 3.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Chapter, ["d96"],
            Formula: "families = floor(log₂ span) + 1 = 3",
            References: ["QG_210"]),
        new("masses", "Masses", "Fermion masses = anchors × dimensionless D96 ratios.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Chapter, ["families"],
            Formula: "m_u = m_e · Σ√m/√Σm² = 2.164 MeV",
            References: ["QG_173"]),
        new("couplings", "Couplings", "Gauge couplings as spectral ratios: α_weak = 3/Σm, α_strong = 8/Σ√m.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Chapter, ["families"],
            Formula: "α_weak = 3/Σm,  α_strong = 8/Σ√m"),
        new("planck-scale", "Planck Scale", "M_Pl = v·A³ = 254.37·(95·44·87)³ = 1.2234e19 GeV — the derived Planck content.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["d96"],
            Formula: "A = Σm·#g·occ₂ = 95·44·87,  M_Pl = v·A³",
            CalculationId: "planck-scale",
            References: ["QG_181", "QG_183"]),

        // ── Layer 5 — Correspondence ───────────────────────────────────────────
        new("thermodynamics", "Thermodynamics", "An added occupancy layer over the structural modes (temperature is BOUNDARY).",
            TheoryLayer.Correspondence, TheoryClassification.Correspondence, TheoryObjectKind.Chapter,
            ["occupancy"],
            References: ["NP_027", "NP_030", "NP_031"]),
        new("quantum-layer", "Quantum Layer", "The correspondence layer hosting entanglement — an unavoidable consequence of observation.",
            TheoryLayer.Correspondence, TheoryClassification.Correspondence, TheoryObjectKind.Chapter,
            ["symmetry", "information-content"],
            References: ["NP_051", "NP_053"]),
        new("joint-state", "Joint State", "The first irreducible quantum primitive: a normalized rank-2 complex 2×2 matrix (a coherent two-qubit amplitude).",
            TheoryLayer.Correspondence, TheoryClassification.NewPrimitive, TheoryObjectKind.Chapter,
            ["quantum-layer"],
            Formula: "ψ = c_{ij},  Schmidt rank 2 ⇔ det c ≠ 0",
            CalculationId: "bell-state",
            References: ["NP_039", "NP_040", "NP_043"]),
        new("entangling-gate", "Entangling Gate", "The second irreducible quantum primitive: the non-local two-body interaction H_int = J·σ⊗σ (CNOT/CZ/iSWAP/√SWAP).",
            TheoryLayer.Correspondence, TheoryClassification.NewPrimitive, TheoryObjectKind.Chapter,
            ["joint-state"],
            Formula: "U = e^{−i H_int t},  H_int = J·σ⊗σ",
            CalculationId: "d96-rank",
            References: ["NP_047", "NP_048", "NP_050", "NP_052"]),
    ];

    private static List<TheoryAudit> SeedAudits() =>
    [
        new("np023", "O(2) Mirror Search", "The observable sector has an O(2) mirror-pair structure.",
            "CONFIRMED: λ_k = λ_{N−k} for every mode k — exact mirror-pair degeneracy.",
            AuditStatus.Passed, new DateTime(2026, 8, 18), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "symmetry"]),
        new("np024", "O(2) Mirror Pair Prediction", "The O(2) doublet is the strongest falsifiable D96 prediction.",
            "CONFIRMED: mirror-pair frequencies ω_k/ω_{N−k} = 1 exactly; any deviation falsifies.",
            AuditStatus.Passed, new DateTime(2026, 8, 19), TheoryLayer.Structure, TheoryClassification.Derived,
            ["np023"]),
        new("np035", "Density-of-States Origin", "The D96 DOS is 1D because the ring is 1D.",
            "CONFIRMED: one integer mode index ⇒ p=1; only tensor products raise the exponent.",
            AuditStatus.Passed, new DateTime(2026, 8, 30), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "occupancy"]),
        new("np038", "Entanglement Audit", "Canonical D96 generates true entanglement, or only correlation?",
            "ABSENT: canonical D96 yields only correlation; genuine Bell entanglement is REFUTED (success criterion A).",
            AuditStatus.Passed, new DateTime(2026, 9, 4), TheoryLayer.Correspondence, TheoryClassification.Refuted,
            ["d96", "quantum-layer"]),
        new("np045", "CHSH Reality Audit", "Must AT accept CHSH violations as fundamental physics?",
            "CONFIRMED: the loophole-free Bell violation is a fact; the joint-state sector is REQUIRED physics.",
            AuditStatus.Passed, new DateTime(2026, 9, 4), TheoryLayer.Correspondence, TheoryClassification.Correspondence,
            ["np038", "joint-state"]),
        new("np052", "Quantum Primitive Completeness Audit", "Are {Joint State, Entangling Gate} the complete minimal quantum extension?",
            "CONFIRMED: two primitives are COMPLETE — no third primitive, ontology size 2.",
            AuditStatus.Passed, new DateTime(2026, 9, 5), TheoryLayer.Correspondence, TheoryClassification.Derived,
            ["joint-state", "entangling-gate"]),
        new("np055", "Dark Energy Ontology Audit", "What is Dark Energy physically inside Actualization Theory?",
            "ΩΛ is the information-bookkeeping fraction (B = E): a DERIVED information observable whose energy interpretation is HOSTED (no derived vacuum energy, no derived w = −1; QG230's Λ ∝ 1/R² gives w = −1/3).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-lambda", "iocc"]),
        new("np056", "Equation-of-State Audit", "Can the informational ontology generate a unique equation of state?",
            "NO — ΩΛ is a snapshot density fraction with no EoS content: dynamics degenerate (64% H² spread across w), acceleration needs hosted w < −1/3; a time-independent ΩΛ forces w = 0 and deceleration.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Boundary,
            ["omega-lambda"]),
        new("np057", "Dark Energy Meaning Audit", "What does ΩΛ physically represent — and why information as a density fraction?",
            "ΩΛ = the normalized ENTROPY DEFICIT (information surplus) of the [4,4,87] occupancy — a DERIVED state descriptor (order parameter), monotone in top-heaviness, not a cause and not an energy density.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Derived,
            ["omega-lambda", "iocc"]),
        new("np058", "Information-to-Energy Bridge Audit", "Where does the information → energy-density mapping originate?",
            "At QG89, 'energy = actualization rate' — a DEFINITION, inherited at QG230 (Λ = 8πG·ρ_Λ, importing G = ħc/M_Pl²) and realized at QG234; every step before the bridge is pure counting/information.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Boundary,
            ["omega-lambda", "actualization"]),
        new("np059", "Actualization Rate Audit", "Can 'energy = actualization rate' be derived instead of postulated?",
            "NO — it is BOUNDARY. The conserved COUNT (Σρ = 1, Σm = 0) is DERIVED; the step from count to energy has no Noether route (discrete time) and needs anchors (v, m_e, ħ, c).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["actualization", "iocc"]),
        new("np060", "Dark Energy Resource Audit", "Does ΩΛ represent an extractable resource, or only a state descriptor?",
            "ΩΛ CANNOT perform work: no energy scale, no gradient, no temperature channel. The extractable side is Ωm (matter = deficit); the vacuum-work channel is hosted.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Refuted,
            ["omega-lambda"]),
        new("np061", "ΩΛ Coincidence Audit", "Why does ΩΛ = I_occ/ln K match the observed fraction to 0.12%?",
            "A fragile point-correspondence: only {KL measure, K=3, [4,4,87]} matches; perturbing K, occupancy, or the measure breaks it. Precision rests on the non-derived KL measure and the K=3 window (anchored to ΩΛ_obs).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-lambda", "iocc"]),
        new("np062", "High-Order Universe Audit", "Why is the realized D96 occupancy so highly ordered?",
            "Because the count density is the occupancy of a discrete 1D circulant spectrum — top-heavy by construction (linear dispersion + UV cap, ~92% top-octave share). High order is the mandatory, typical structure, not a drift from uniformity.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "occupancy"]),
        new("np063", "Cosmological Coincidence Audit", "Why are ΩΛ_AT (information) and ΩΛ_obs (cosmology) numerically equal?",
            "A DESCRIPTOR link via an unproven common origin (ρ): causal and scaling-law links REFUTED. Four non-derived assumptions — KL measure, N=96 window, QG89 bridge, anchors — carry the equality.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-lambda", "iocc"]),
        new("np064", "Canonical Structure Necessity Audit", "Why does {N=96, K=3, [4,4,87]} exist?",
            "A DERIVED-BOUNDARY hybrid: the period-3 seed (DERIVED) forces N = 3·2^k, the 3-family window [4,8) (BOUNDARY) selects k=5 → N=96 → [4,4,87] → ΩΛ = 0.6839. Root: seed (derived) × window (boundary).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "occupancy"]),
        new("np065", "Dark Matter Ontology Audit", "What is Dark Matter (Ωm) inside Actualization Theory?",
            "Dark Matter = the matter DEFICIT m = ρ̄ − ρ (an effect, not a particle), Ωm = H/ln K = 0.3161 (the realized-entropy fraction). The deficit SOURCES gravity (DERIVED) — flat rotation α=0, M∝R — so Ωm has stronger physical meaning than the descriptive dark-energy surplus; both share the hosted QG89 energy reading.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Derived,
            ["omega-matter", "iocc"]),
        new("np066", "Dark Matter Evidence Audit", "Which observed dark-matter phenomena does the deficit reproduce?",
            "PARTIAL: 2 DERIVED (flat rotation α=0, Ωm = 0.3161) / 2 CORRESPONDENCE (cluster mass degenerate with ΛCDM, LSS seed+growth) / 2 REFUTED (lensing — conformal γ=−1; Bullet Cluster — not a particle). The deficit is a gravitational-potential surrogate, not a full dark matter.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-matter", "iocc"]),
        new("np067", "Lensing Sector Audit", "Why does the deficit source gravity but fail light-bending?",
            "The ρ-only metric is CONFORMALLY FLAT (γ = −1), cancelling the null-geodesic prefactor (1+γ)/2 = 0 — no lensing — while potential effects (g₀₀) survive. The minimal fix is the ψ tensor sector (second primitive), restoring γ = +1 ⇒ full lensing. Determination: B (missing tensor sector), not fatal.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-matter", "d96"]),
        new("np068", "Psi Ontology Audit", "What is ψ physically?",
            "ψ is the spin-2 (Weyl-curvature) graviton — the tensor (traceless) face of the founding Difference — a PRIMITIVE physical degree of freedom (massless spin-2, 2 polarizations), carrying lensing, frame dragging, and gravitational waves. Not auxiliary, not hosted, not emergent (spin-0 cannot source spin-2).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Correspondence, TheoryClassification.Boundary,
            ["omega-matter", "d96"]),
        new("np069", "Expansion Ontology Audit", "What does cosmic expansion physically mean in AT?",
            "Expansion is the branching growth of the actualization count ρ (∂_t ρ = ln(μ)·ρ), carrying the metric g = ρ^(2/d)η. The FRW scale factor a = ρ^(1/d) is a hosted relabeling; at criticality the mean is static and only the variance grows. No native accelerating scale factor.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-lambda", "iocc"]),
        new("np070", "Criticality Audit", "Why is the canonical universe critical (μ=1)?",
            "μ=1 is the UNIQUE branching ratio that is simultaneously marginal-stable, scale-free, and maximum-entropy — three criteria coinciding at μ=1. Criticality is DERIVED (unique), conditional on scale-freeness (the indifference principle) as the single boundary input.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Derived,
            ["d96", "occupancy"]),
        new("np071", "Matter Ontology Audit", "What is matter physically in AT?",
            "Matter is the DEFICIT m = ρ̄ − ρ — a stable, self-bound excitation (a deficit pattern) of the count density, the under-occupancy that sources gravity and clumps. Not a particle, not a fundamental substance, not a soliton (legacy); it is a dynamically stabilized wave structure with masses as its derived spectral content.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Derived,
            ["omega-matter", "occupancy"]),
        new("np072", "Particle Ontology Audit", "If matter is a deficit excitation, what are particles — and what is an electron?",
            "A particle is a RESONANCE CLASS — a mode (frequency attractor) of the D96 spectrum, organized into octave-band families, with mass = anchor × dimensionless D96 ratio. The electron is the lightest fermion mode (octave bottom), its mass m_e the boundary anchor; the muon/tau/quarks are derived ratios. Particles are EMERGENT, not fundamental point objects.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Derived,
            ["omega-matter", "families"]),
        new("np073", "Resonance Selection Audit", "Why do only specific D96 modes appear as particles?",
            "Only 3 of the 95 modes appear as particle generations because the spectrum organizes into 3 octave bands = 3 families, and within each band only the STABLE bottom mode survives (metastability), selected by isospin-constrained mode access. The top band (87/95) is the bulk deficit, not particles.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Derived,
            ["families", "occupancy"]),
        new("np074", "Quantum Number Ontology Audit", "If particles are resonance classes, what are quantum numbers?",
            "Quantum numbers are the GENERATORS of the D96 automorphism group — symmetry charges that act as occupancy-access rules. Charge = U(1) = Z_96 rotation, isospin = SU(2) = Z2 doublet, color = su(3) = 8 (count 3 = postulate), hypercharge = Y = Q − T3. The electron's charge is its U(1) rotation eigenvalue.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Derived,
            ["families", "occupancy"]),
        new("np075", "Force Ontology Audit", "If particles are resonance classes and quantum numbers are symmetry charges, what is a force?",
            "A force is the ACTION of a D96 symmetry generator — a symmetry action that induces a resonance transition between modes (vertex ⟨f|T^a|i⟩). Gauge bosons are the generators (link excitations), not matter particles; the photon is the U(1) = Z_96 rotation generator; gravity is the metric geometry, not a gauge force.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Derived,
            ["families", "occupancy"]),
        new("np076", "Psi Dark Matter Audit", "Can the ψ graviton sector account for part of the observed Dark Matter signal?",
            "ψ (the massless spin-2 graviton) contributes ZERO dark-matter mass: it produces propagating waves only, forms no bound configurations, and supplies no mass density (w = 1/3, Ω_gw ≈ 10⁻⁹ ≪ Ωm). ψ's true role is to restore lensing so the deficit's mass is visible. Dark Matter is DEFICIT-ONLY; ψ is the graviton, not a dark-matter component.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Refuted,
            ["families", "occupancy"]),
        new("np077", "Structure Formation Audit", "Can the deficit field naturally produce halos, galaxy profiles, and cluster structure without particle dark matter?",
            "The deficit produces the SEED and LINEAR GROWTH derivatively (Poisson δ_i = 1/√⟨N⟩, scale-free variance, δ ∝ a, n_s = 0.96497 — QG231/237), but it forms halos/profiles/clusters only with EXTRA ASSUMPTIONS: the flat-rotation SIS profile (v²≈const ⇒ M∝r ⇒ ρ∝r⁻²) requires the α=0 log-deficit abundance law (a symmetry selection, not a dynamical attractor — G4-RHO). The SIS is steeper than the NFW cusp and singular; NFW concentration is fitted. Determination: B.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Partial,
            ["omega-matter", "iocc"]),
        new("np078", "Alpha=0 Necessity Audit", "Why must the universe select α=0 instead of α≠0?",
            "α=0 is the UNIQUE point where flat rotation (v² ∝ r^(−α), slope 0), stability (equal-deficit-per-octave), criticality (μ=1 ⟺ α=0), and maximum entropy coincide. It is DERIVED (unique) as a selection fixed point, NOT a dynamical attractor (conservation → repulsive ρ∝r⁻², scale-freeness → continuum). The conditioning input is scale-freeness (AT-F1 indifference principle) — the FINAL boundary.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Derived,
            ["omega-matter", "iocc"]),
        new("np079", "Scale-Freeness Origin Audit", "Can scale-freeness (AT-F1) be derived from Difference itself, or is it the final irreducible boundary?",
            "Scale-freeness is DERIVED from Difference, NOT the final boundary. Difference is a BINARY (metric-free) relation, so the primitives it grounds (Q-events, the counting measure as a density of weight d, the causal order) carry no scale; scale-freeness follows as the unique renormalization-invariant abundance (the power law = RG fixed point = indifference). The true final boundary is Difference itself.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Derived,
            ["difference", "iocc"]),
        new("np080", "Difference Duality Audit", "Why does Difference split into exactly one scalar face (ρ) and one tensor face (ψ)?",
            "Difference actualizes into a SYMMETRIC rank-2 object (A_ij = A_ji), whose decomposition is exhaustively spin-0 TRACE (1 = ρ, scalar/count/isotropic) ⊕ spin-2 TRACELESS (5 = ψ, tensor/orientation/Weyl, 2 TT polarizations). 6 = 1 + 5: no third component, no vector face, no spin-≥3. The duality is DERIVED (rank-2 decomposition); primitive cost = 1 (Difference, with ρ and ψ as its two faces).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Derived,
            ["difference", "omega-matter"]),
        new("np081", "Energy Ontology Audit", "What is energy physically inside Actualization Theory?",
            "Energy is a RELABELING of actualization dynamics — neither fundamental nor emergent. The conserved object is the COUNT (Σρ = 1, Σm = 0, DERIVED); 'energy' is that count renamed (QG89 'energy = actualization rate', a definition) and unit-ized (anchors v, m_e + ħ, c). Noether fails (discrete time). Removing energy language loses nothing derived.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference", "iocc"]),
        new("np082", "Electron Mass Anchor Audit", "Why does the fermion spectrum require the electron mass anchor — is m_e the true boundary?",
            "m_e is NOT the true remaining matter-scale boundary — it is a REPLACEABLE unit conversion. All mass ratios are DERIVED (dimensionless: m_μ/m_e = 207.03, m_τ/m_μ = 16.842); the absolute scale m_e carries only the DIMENSION (MeV), which no derived D96 invariant supplies. The true boundary is 'one dimensionful scale' (irreducible); m_e is its replaceable instance (m_e ↔ M_Z ↔ v).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Boundary,
            ["electron", "iocc"]),
    ];
}
