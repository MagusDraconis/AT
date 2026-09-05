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
        new("omega-lambda", "ΩΛ — Information Cosmology", "The dark-energy fraction ΩΛ = I_occ/ln K = 0.6839 — the flagship derived cosmological observable.",
            TheoryLayer.Cosmology, TheoryClassification.Derived, TheoryObjectKind.Chapter,
            ["iocc"],
            Narrative: "Information cosmology: the information density I_occ fixes the density-fraction pair exactly. ΩΛ = I_occ/ln K = 0.7513/ln 3 = 0.6839 matches the observed dark-energy fraction to 0.12%, and Ωm = 1 − ΩΛ = 0.3161. This is the strongest single-number correspondence in the ResearchY program.",
            Formula: "ΩΛ = I_occ / ln K = 0.6839,  Ωm = (ln K − I_occ)/ln K = 0.3161",
            CalculationId: "omegalambda",
            References: ["QG_234", "QG_228"],
            AuditIds: ["np035"]),
        new("omega-matter", "Ωm", "The matter fraction Ωm = 1 − ΩΛ = 0.3161.",
            TheoryLayer.Cosmology, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["iocc"],
            Formula: "Ωm = 1 − ΩΛ = 0.3161"),
        new("q0", "q₀ — Deceleration Parameter", "The deceleration parameter q₀ = Ωm/2 − ΩΛ < 0 (accelerating).",
            TheoryLayer.Cosmology, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["omega-lambda", "omega-matter"],
            Formula: "q₀ = Ωm/2 − ΩΛ",
            CalculationId: "deceleration"),
        new("zacc", "z_acc — Acceleration Redshift", "The transition redshift z_acc = (2ΩΛ/Ωm)^(1/3) − 1.",
            TheoryLayer.Cosmology, TheoryClassification.Derived, TheoryObjectKind.Derivation,
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
    ];
}
