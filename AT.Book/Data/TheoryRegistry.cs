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
            Narrative: "Information cosmology: the information density I_occ fixes the density-fraction pair exactly. ΩΛ = I_occ/ln K = 0.7513/ln 3 = 0.6839 matches the observed dark-energy fraction to 0.12%, and Ωm = 1 − ΩΛ = 0.3161. ΩΛ is the normalized ENTROPY DEFICIT — the information surplus of the top-heavy [4,4,87] occupancy over the uniform reference — a DERIVED state descriptor (order parameter), not a cause and not an energy reservoir. A sequence of dark-energy audits established that the physical (energy) reading is HOSTED: the information → energy-density bridge rests on the definition 'energy = actualization rate' plus dimensionful anchors, so the match is a genuine correspondence, not a derived physical relation.",
            Formula: "ΩΛ = I_occ / ln K = 0.6839,  Ωm = (ln K − I_occ)/ln K = 0.3161",
            CalculationId: "omegalambda",
            References: ["QG_234", "QG_228"],
            AuditIds: ["np055", "np056", "np057", "np058", "np059", "np060", "np061", "np063"]),
        new("omega-matter", "Ωm", "The matter fraction Ωm = H/ln K = 0.3161 — the realized-entropy fraction (the deficit side, matter = ρ̄ − ρ).",
            TheoryLayer.Cosmology, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["iocc"],
            Narrative: "Ωm is the complement of ΩΛ: the realized-entropy fraction H/ln K, read as the matter deficit m = ρ̄ − ρ. Matter is the under-occupancy of the counting measure — an effect, not a particle — that sources gravity (T_μν = (ρ̄−ρ)v_μv_ν, flat rotation from equal deficit per octave, mass proportional to radius). Unlike the descriptive dark-energy surplus, the deficit has a DERIVED gravitational role.",
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
        new("gravity-source", "Gravity Source", "Gravity is sourced by the counting measure ρ — the only dimensionless, local candidate; the field equation needs no coupling constant.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["planck-scale", "occupancy"],
            Narrative: "Five candidates were adjudicated (energy density, actualization density, spectral density, information density, the curvature–density law). "
                + "ρ wins because the conformal factor ρ^(2/d) must be dimensionless and local; GR's source is dimensionful and reaches curvature dimensions only "
                + "through κ. Energy density and spectral density are CORRELATED (the first is an exact re-expression of the deficit whose energy reading is hosted; "
                + "the second supplies the source's parameters and the magnitude of G but has no position index). Information density and the curvature–density law "
                + "used as a source are REFUTED.",
            Formula: "g = ρ^(2/d)·η,  R = F(ρ),  a = −(1/d)·∇ln ρ,  ρ_{k+1} = μ·ρ_k",
            CalculationId: "gravity-source",
            References: ["ResearchY-G_001"],
            AuditIds: ["g001"]),
        new("density-control", "Density Control", "ρ can be rearranged at fixed total mass-energy; the free room is exactly the degeneracy structure, N − A₀.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["gravity-source", "d96"],
            Narrative: "Σm = 0 identically (QG194), so the ARRANGEMENT of ρ is never tied to the energy. The free directions number N − A₀ = 51 of 96 for D96, "
                + "which equals the D_048 latent fraction L = 0.53125 exactly. Phase coherence is inert (ρ, R, a unchanged) and rescaling moves only the energy.",
            Formula: "free room = Σ(m_i − 1) = N − A₀ = 51/96 = L",
            CalculationId: "density-control",
            References: ["ResearchY-G_002", "D_047", "D_048"],
            AuditIds: ["g002"]),
        new("gravity-magnitude", "Gravity Magnitude", "The SI size of a Δρ: a pure-number potential channel and a threshold-length force channel.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["gravity-source", "density-control"],
            Narrative: "ΔΦ/c² = Δa_AT needs no length scale (the potential, clock and redshift channel is MEASURABLE); Δa = c²Δa_AT/L and ΔR = ΔR_AT/L² need "
                + "the physical scale L (ASTROPHYSICAL ONLY: 1e-6 g for L < 9.54 kpc, 1e-9 g < 9.54 Mpc, 1e-12 g < 9.54 Gpc). Phase and rescaling are PRACTICALLY ZERO. "
                + "The observed galactic field corresponds to Δln ρ = 1.6102e-6, so any realised reconfiguration must be suppressed by ≥ 3.746e5.",
            Formula: "ΔΦ/c² = Δa_AT;  Δa = c²·Δa_AT/L;  ΔR = ΔR_AT/L²;  ΔM = Δa·L²/G",
            CalculationId: "gravity-magnitude",
            References: ["ResearchY-G_003", "QG_187"],
            AuditIds: ["g003"]),
        new("gravity-calibration", "Gravity Calibration", "a_pred/a_obs at four scales with no free parameters: Earth 0.99600, Sun–Earth 0.99600, RAR 0.86850, cluster 0.51759.",
            TheoryLayer.Physics, TheoryClassification.Partial, TheoryObjectKind.Derivation,
            ["gravity-magnitude", "planck-scale"],
            Narrative: "Earth and Sun–Earth reproduce measured gravity to 0.40 %, the residual being entirely the derived value of G (QG181). The galaxy RAR scale "
                + "g† = cH₀/(2π) is parameter-free and lands within 8–13 % of the literature determinations. The cluster modified-gravity channel fails by 1.93× "
                + "(the project's own X063 finding), so AT leans on the deficit-as-mass channel there. Locally the theory is exactly Newtonian with the derived G, "
                + "which is why the counterfactual 1e-6 g effects are absent.",
            Formula: "Earth & Sun–Earth: G_AT/G_CODATA = 0.9959996;  RAR: g† = c·H₀/(2π) = 1.04220e-10 m/s²",
            CalculationId: "gravity-calibration",
            References: ["ResearchY-G_004", "QG_080", "QG_181"],
            AuditIds: ["g004"]),
        new("gravity-control", "Gravity Control Realizability", "The large G_003 modes are SUPPRESSED, not forbidden: Poisson counting, not entropy, is what makes them unobservable.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["gravity-calibration", "gravity-magnitude"],
            Narrative: "The large fixed-energy reconfigurations violate no conservation law, break no symmetry and occupy genuinely free directions — they are simply never counted. "
                + "The entropy channel is capped (max ΔS = ln 96 = 4.564348 ⇒ a suppression of only 1/96 = 0.010417, 3.6e7× short of the required 3.746e5; the witness tilt costs a mere 0.272565 nats), "
                + "so the mechanism is AT's own mandatory Poisson law δ = 1/√⟨N⟩: the observed contrast 1.6102e-6 implies ⟨N⟩ = 3.8569e11, making the observed field typical (P = 0.61), "
                + "and the required suppression is reached already at 8.1577e-6 (5.07× observed). The internal flow is arrangement-neutral (ρ_(k+1) = μρ_k with the same μ per cell; a(λρ) = a(ρ)) "
                + "and the attractor erases arrangements, so the modes have no internal drive and a witness tilt contracts ~34× in 200 canonical steps.",
            Formula: "P(Δ) = exp(−⟨N⟩Δ²/2);  ⟨N⟩ = 1/δ²;  ΔS ≤ ln 96",
            CalculationId: "gravity-control",
            References: ["ResearchY-G_005", "QG_194", "D_047"],
            AuditIds: ["g005"]),
        new("gravity-suppression", "Suppression Mechanism", "The G_005 34× is derived in closed form: the relaxation operator is a low-pass filter whose every mode decays geometrically.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["gravity-control", "gravity-magnitude"],
            Narrative: "The suppressing term is the relaxation/coarse-graining operator (RhoDynamics.DiffuseStep), a LINEAR low-pass filter on the eigenspace-occupancy index "
                + "with the exact spectrum μ_k = 1 − 2d(1 − cos(πk/N)): every Neumann mode is an exact eigenvector and decays geometrically, so the suppression at any horizon is "
                + "computable in closed form and reproduces direct iteration to < 1e-9 at m = 1…50 000. The 34× is 1/r(200) = 33.78 = exp(3.5198 nats). Over 1…200 a power law fits "
                + "the aggregate better (R² 0.9945 vs 0.7579) — a real illusion — but the tail is a single exponential at slope ln μ₁ (1.85e-10) and the rate converges to 2.1419e-4 "
                + "instead of zero, so power-law decay as the LAW is refuted. Entropy is downstream (exactly linear operator; H + (N/2)E → ln 96 with residual 9.7e-7) and the "
                + "branching flow is arrangement-neutral (a(λρ) = a(ρ)), so neither drives it. μ_k depends on N only: the mechanism is arrangement-selective, and the observed smooth "
                + "field survives on the slow mode (μ₁^200 = 0.958) while the within-multiplet free directions are erased.",
            Formula: "mu_k = 1 − 2d(1 − cos(pi k/N));   r(m) = sqrt(Σ_{k≥1} w_k²mu_k^2m / Σ_{k≥1} w_k²);   1/r(200) = 33.78",
            CalculationId: "suppression-mechanism",
            References: ["ResearchY-G_006", "ResearchY-G_005"],
            AuditIds: ["g006"]),
        new("suppression-origin", "Suppression Origin", "DiffuseStep is not imported: it is the canonical coarse-graining's infinitesimal form, unique up to one scalar rate.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["gravity-suppression", "gravity-control"],
            Narrative: "Tracing Difference → Actualization → ρ evolution → DiffuseStep shows every link is canonical: the counting measure (Σρ = 1), the branching flow (arrangement-neutral), the "
                + "exactly RG-invariant coarse-graining of the per-octave increments, and finally the Euler step of the Laplacian flow on that index. Imposing locality, constant coefficients, "
                + "symmetry and row-sum conservation leaves exactly one family — W(b) = b·left + (1−2b)·a + b·right — so the operator is UNIQUE up to a single scalar rate, and isotropy is forced "
                + "(an anisotropic weight leaks at reflecting boundaries). Positivity of ρ forces 0 ≤ d ≤ ½ (DERIVED); at d = ½ the fastest mode oscillates (μ₉₅ = −0.999465) and selectivity "
                + "vanishes, while the mechanism needs selectivity |1 − 4d|. The values d = 0.2 and m = 200 are BOUNDARY: at fixed T = m·d = 40 the factor varies by 0.17 % across a 20× range of d. "
                + "Of the four replacements only the nearest-neighbour average is admissible — it IS d = ½ — and it destroys the mechanism (2.01 vs 33.78). Time is not the cause: the branching flow "
                + "is diagonal on the arrangement and suppresses nothing at any μ.",
            Formula: "W = I − d·L;  0 <= d <= 1/2;  selectivity ~ |1 − 4d|;  factor = f(T = m·d)",
            CalculationId: "suppression-origin",
            References: ["ResearchY-G_007", "QG_194", "NP_174"],
            AuditIds: ["g007"]),
        new("controlled-suppression", "Controlled Suppression", "Nothing holds a high-Δρ state: undriven it dies in 0.622 steps; driven it must be re-created every step at 0.7998 of its amplitude.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["suppression-origin", "gravity-suppression"],
            Narrative: "The kernel of the relaxation is one-dimensional, so the only undriven stationary profile is the uniform counting measure; lifetimes τₖ = −1/ln|μₖ| span 4668.80 steps (k = 1, the "
                + "observed smooth class — METASTABLE) to 0.622 steps (k = 95, the witness class — SUPPRESSED). A driven recursion with a mode-matched drive has the exact steady state "
                + "c·v_k/(1 − μ_k) and is an allowed configuration (count-conserving, positive), but the price is (1 − μ_k) of the amplitude per step: 2.14e-4 for the smooth class and 0.7998 for the "
                + "highest mode (a 3734× penalty). Because the steady state is a filtered copy of the drive, an unmode-matched drive holds zero high-k content; periodic forcing never beats DC "
                + "(sup_ω |H_k| = the DC gain); and boundary-only support holds a smooth ramp whose contrast is capped at 3.09 % of the count by ρ ≥ 0. G_005's SUPPRESSED verdict becomes "
                + "NOT MAINTAINABLE.",
            Formula: "rho* = c·v_k/(1 − mu_k);  tau_k = −1/ln|mu_k|;  drive = (1 − mu_k) per step",
            CalculationId: "controlled-suppression",
            References: ["ResearchY-G_008", "ResearchY-G_005"],
            AuditIds: ["g008"]),
        new("clock-rate", "Clock Rate", "dτ/dt = ρ^(1/d) from g₀₀ = −ρ^(2/d): AT ≡ GR to first order, ±½ at second (1e-19), and a 0.33 % galactic cross-check.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["gravity-source", "controlled-suppression"],
            Narrative: "The clock law dτ/dt = √(−g₀₀) = ρ^(1/d) gives (1/d)Δlnρ = ΔΦ/c², the canonical redshift law. Earth surface: −6.9613e-10 → −60.145 μs/day, identical to GR to double precision; GPS gravitational "
                + "+5.2940e-10 → +45.740 μs/day (QG187: 45.7), with the total +38.537 vs the observed 38.6 only once the imported SR term is added (CORRELATED). The Galactic field is a NEW cross-check: the "
                + "G_003 ambient contrast gives 5.367333e-7 = 46.374 ms/day versus the rotation curve's v²/c² = 5.385226e-7 (220 km/s) — 0.33 %, equivalent v = 219.63 km/s. AT and GR agree to first order "
                + "and split at second order (±½ x²: 4.846e-19 at the Earth's surface, 2.803e-19 at GPS — below the 1e-18 optical-clock floor). The G_002 redistributions would move clocks by up to 22.86 % at "
                + "fixed total mass-energy, but nothing realised or maintainable delivers it (G_005/G_008): what is realised is exactly the potential depth GR already predicts.",
            Formula: "dtau/dt = rho^(1/d);  (1/d)Delta ln rho = Delta Phi/c^2;  AT: exp(x) vs GR: sqrt(1+2x)",
            CalculationId: "clock-rate",
            References: ["ResearchY-G_009", "QG_197", "QG_187"],
            AuditIds: ["g009"]),
        new("time-control", "Time Control Feasibility", "Priced clock shifts: 1 ns/µs/ms per day are PRACTICAL as numbers, the band tops at 0.1407 s/day, and 1 s/day is refuted.",
            TheoryLayer.Physics, TheoryClassification.Partial, TheoryObjectKind.Derivation,
            ["clock-rate", "controlled-suppression"],
            Narrative: "Inverting dτ/dt = ρ^(1/d) gives Δlnρ = 3f, and G_008 prices the drive at (1 − μ_k) of the amplitude per step. The four targets: 1 ns/day needs Δlnρ = 3.472222e-14 and "
                + "a 7.436285e-18 per-step drive (a 32.25 m/s well); 1 µs/day 3.472222e-11 and 7.436285e-15 (1.02 km/s); 1 ms/day 3.472222e-8 and 7.436285e-12 (32.25 km/s); 1 s/day 3.472222e-5 "
                + "and 7.436285e-9 (1019.91 km/s). The G_005 band's top is 0.140737 s/day and the observed galactic field is 0.046373 s/day (33 % of the band, a 219.63 km/s well). Positivity "
                + "never binds (the largest excursion is 1.7e-5 of ρ̄), power scales as P ∝ f·(k/N)² (cost per unit shift 6.4250e-4 at k = 1 vs 2.3994 at k = 95), and every target needs an "
                + "external mode-matched driver the canonical chain does not supply — so time control is feasible as arithmetic and refuted as physics.",
            Formula: "Delta ln rho = 3f;  drive/step = (1 − mu_k)·Delta ln rho;  P ~ f·(k/N)^2",
            CalculationId: "time-control",
            References: ["ResearchY-G_010", "ResearchY-G_005", "ResearchY-G_008"],
            AuditIds: ["g010"]),
        new("rho-actuator", "Rho Actuator", "No physical quantity acts on ρ: five candidates are functions of ρ, two are independent but ρ-inert, and ρ is fully actuable only by an imported source.",
            TheoryLayer.Physics, TheoryClassification.Partial, TheoryObjectKind.Derivation,
            ["gravity-source", "density-control"],
            Narrative: "The actuator criterion: q is an actuator iff it is independent of ρ, determines ρ locally, conserves the count, costs no energy and adds no primitive. ENERGY DENSITY is correlated — "
                + "E = ⟨λ,ρ⟩ is identical to 1e-12 between the canonical measure and the tilt (every eigenspace total equal to 12 digits) while ρ moves by L1 = 0.6666667 and the field is created out of an exact "
                + "zero (0 → 0.603175); E is non-injective (the fixed-E fibre contains the whole 51-dimensional degeneracy space) and re-ordering the same multiset moves E by 1.5401766 (12.8 %). SPECTRAL "
                + "ORGANIZATION is correlated — the orthonormal DCT-II is a bijection (‖CCᵀ − I‖ = 1.37e-14, round trip 4.2e-16) with the DC coefficient equal to the count, leaving 95 free coordinates; the "
                + "witness is high-k (dominant mode k = 94, k ≥ 48 share 0.7965733) and μ_k carries no ρ-dependence (μ₄₈ = 0.6 exactly). DEGENERACY ENGINEERING is correlated — the 51 within-eigenspace "
                + "directions conserve count, energy, spectrum and A₀ = 45 while creating the field. PHASE COHERENCE and SYNCHRONIZATION are refuted — the only candidates independent of ρ, and exactly "
                + "ρ-inert: L1(ρ,|ψ|²) ≤ 2.5e-16 and |Δa| < 1e-9 for the canonical grid, a global shift, the mirror and the locked configuration, while |Σψ| runs 0.0324196281 → 9.1171821879 (a factor 281.22) "
                + "and Kuramoto r goes 8.0788382e-17 → 1.0. COMPRESSION is refuted as a quantity (a ρ → ρ map whose smooth difference survives 200 steps 1.1207× against the witness's 33.78×) and INFORMATION "
                + "DENSITY is refuted (exactly permutation- and reversal-invariant, ΔKL = 0, while ρ moves by L1 = 0.6583333 and the field grows to 1.0031746). THE POSITIVE RESULT: ρ is fully actuable — for "
                + "any target s = (I − W)ρ* is count-neutral (Σs = 2.1e-17) and unique, with all gains finite (4669.296831218 at k = 1, 2.500000000 at k = 48, 1.250334722 at k = 95), the inverse reconstructs "
                + "to 1.9e-15 and the driven recursion converges to < 1e-12 — so the vacancy is a SOURCE, not a knob. G_002's CONTROLLABLE verdicts concern the operations on ρ and stand unchanged.",
            Formula: "s = (I − W)·rho*;  gains 1/(1 − mu_k) in [1.250334722, 4669.296831218];  E = <lambda, rho>",
            CalculationId: "rho-actuator",
            References: ["ResearchY-G_011", "ResearchY-G_001", "ResearchY-G_002"],
            AuditIds: ["g011"]),
        new("labor-rho", "Labor Rho", "A bench chain with d = 0.2 IS DiffuseStep and the derived d ≤ ½ IS the CFL bound — but no bench device can turn a density pattern into a clock.",
            TheoryLayer.Physics, TheoryClassification.Partial, TheoryObjectKind.Derivation,
            ["rho-actuator", "suppression-origin"],
            Narrative: "Five laboratory operator families: oscillator lattice / coupled modes / D96 controls are the Neumann chain (λ_max = 3.998929, admissible d ≤ 0.500134, rate spread 3734.44), the resonator "
                + "network is a nearest-neighbour ring (λ_max = 4.0, d ≤ 0.5, spread 934.11), graph diffusion on the D96 circulant would need d ≤ 0.126284 and a hub graph d ≤ 0.020833 — so the canonical "
                + "d = 0.2 is admissible for degree-2 lattices only (0.3999 of the chain bound, 1.5837× over the circulant's, 9.6000× over the hub's). The derived range IS the CFL bound: ρ ≥ 0 ⇔ |1 − dλ| ≤ 1 "
                + "⇔ d ≤ ½, with cos(πk(i+½)/N) an exact eigenvector of DiffuseStep (2.7e-14) and cos(2πkj/N) of the ring step (≤4.9e-15). The drive is trivial (7.436285e-18 of the held amplitude per step "
                + "for 1 ns/day at k = 1; 0.48675 for the G_002 witness) and the steady state is exact (Σρ = 1 to 1e-12, excursion ≤ 1.74e-5 of ρ̄, reproduced to < 1e-12 after 20 000 steps; 1 mJ in 1 µs needs "
                + "7.4363e-15 W). THE GRAVITY LADDER: M/r = f·c²/G — the Earth self-check reproduces 6.9613e-10 with M/r = M⊕/R⊕ = 9.3740e17 kg/m; 1 kg at 1 m gives 7.4262e-28 (1.35e9× below the 1e-18 "
                + "clock floor); 1 kg moved 1 m at fixed total energy gives 3.7131e-28 (2.69e9× below); the clock floor needs 1.3466e9 kg/m (1.35 Mt/m), 1 ns/day 1.5586e13, 1 ms/day 1.5586e19 and the G_005 "
                + "band top 2.1935e21 kg/m (2340 Earths/m). AT predicts exactly the Newtonian field for any real mass rearrangement (G_004's 0.99600), so a bench density pattern changes no clock: the analogue "
                + "readout is a voltage ratio, not the actualization density of spacetime.",
            Formula: "M/r = f·c^2/G;  0 <= d <= 1/2 (CFL);  drive/step = (1 − mu_k)·Delta ln rho",
            CalculationId: "labor-rho",
            References: ["ResearchY-G_011b", "ResearchY-G_007", "ResearchY-G_009"],
            AuditIds: ["g011b"]),
        new("local-actuator", "Local Rho Actuator", "A three-point local feedback freezes any density profile exactly — the first ACTUATOR in the group — but it is marginal, so it holds and cannot create.",
            TheoryLayer.Physics, TheoryClassification.Partial, TheoryObjectKind.Derivation,
            ["rho-actuator", "labor-rho"],
            Narrative: "Five local candidates against four requirements (local, finite drive, survives DiffuseStep, no imported primitive). THE HOLD-DRIVE IS LOCAL: s = (I − W)ρ* is a three-point stencil (perturbing ρ_j moves only "
                + "s_{j−1}, s_j, s_{j+1}), count-neutral (Σs = 2.1e-17), finite (‖s‖₁ = 0.48675 for the witness, max|s| = 0.01866667; 3.331453e-10 for the band top) and exact to < 1e-12 over 20 000 steps. ACTUATOR — the incremental "
                + "local feedback s = ρ − Wρ makes the closed loop the IDENTITY, freezing ANY configuration exactly: the witness tilt to 4.336809e-18 over 5000 steps, the uniform measure and the highest mode to < 1e-15, and a 1e-6 "
                + "perturbation retained 100.000 % (8.674e-19). It is MARGINAL (95/95 modes neutral) — a perfect MEMORY with no restoring force. THE THREE-POINT THEOREM: for the count-conserving family s = β(Wρ − ρ) the closed-loop "
                + "eigenvalues are c_k = μ_k(1 + β) − β, and c_k = 1 for every k only at β = −1 (95/95 neutral against 0/95 at β = 0). THE RESTORING FAMILY s = λ(ρ − ρ̄) IS LIMITED TO THE SMOOTHEST MODE: the closed-loop spectrum is "
                + "μ_k + λ, so stability requires λ in [−1.200214165, 2.141650094e-4] and a fixed point carrying mode k needs λ = 1 − μ_k — only k = 1 fits (k = 2 needs 8.564307e-4; the k = 48 attempt at λ = 0.4 gives "
                + "μ_1 + λ = 1.3998 > 1). k = 1 is exact to 1e-15 over 20 000 steps, k = 2 decays at 6.422657e-4/step (τ = 1556.99), a mixture resolves onto its k = 1 part (2.4e-12), and above threshold the smooth mode runs away at "
                + "1.000214165/step, saturating from a Poisson seed in 64 516 steps. NO LOCAL CREATION: the gain of (I − W)⁻¹ decreases with k (4669.2968 → 1.2503), giving HighKShare(ρ*) ≤ 2.8666657e-7·D_high/w_1²; compact masks "
                + "hold smooth profiles (block w = 2: 6.630273e-3 → w = 64: 6.368861e-9; edge dipole 0.1888945; staggered global 3.281127e-4) and the best structured ±1 mask reaches 0.6117676 (77 % of the witness) but is prescribed "
                + "data; the cellwise test kills the witness for state-dependent generators (spread 1.58e-3 … 9.5e-3 within equal-ρ groups against max|s| = 0.01866667) while a pure mode is exactly linear (9.3e-15). Mode injection is "
                + "REFUTED (full support; 98.3 % high-k for the witness), synchronization is REFUTED (a locked patch leaves ρ bit-identical, L1 = 2.45e-16, max|Δa| < 1e-9), and the lattice/NESS framing is CORRELATED (the medium: "
                + "undriven attractor uniform, responses 2.5–120; a NESS *is* s = (I − W)ρ*). READOUTS: Δτ/τ = Δlnρ/3 — band top 0.140737 s/day, 3:1 31 640.03 s/day, 10:1 66 314.45 s/day, all inside the SUPPRESSED band. REFINEMENT "
                + "(not a reclassification): G_010's 'the canonical chain supplies no driver' stands — an ENGINEERED local feedback realises one, marginally; G_011 labels the QUANTITY, G_012 the LOCAL GENERATOR.",
            Formula: "s = rho - W rho (identity closed loop);  c_k = mu_k(1 + beta) - beta;  HighKShare <= 2.8666657e-7 D_high/w_1^2",
            CalculationId: "local-actuator",
            References: ["ResearchY-G_012", "ResearchY-G_008", "ResearchY-G_011"],
            AuditIds: ["g012"]),
        new("physical-actuator", "Physical Actuator", "The stencil is a negative Laplacian (anti-diffusion, d = 0.2) and a balanced pump-and-drain; a feedback controller or an NIC realizes it, and exactness — not power — is the constraint.",
            TheoryLayer.Physics, TheoryClassification.Partial, TheoryObjectKind.Derivation,
            ["local-actuator", "labor-rho"],
            Narrative: "Which physical process realizes s = (I − W)ρ* locally? THE STENCIL IN PHYSICAL FORM: s_i = −d(ρ_{i−1} − 2ρ_i + ρ_{i+1}) — a NEGATIVE LAPLACIAN, anti-diffusion of strength d = 0.2 on the nearest-neighbour chain "
                + "(verified to 3.5e-18) — and a BALANCED PUMP-AND-DRAIN (exactly 50.0000 % of ‖s‖₁ = 0.48675 injecting, 50.0000 % extracting; max|s| = 0.01866667; count-neutral, three-point local). PHYSICAL: the FEEDBACK CONTROLLER (sense ρ "
                + "and neighbours, compute (I − W)ρ, actuate; exact, maintains ρ* to < 1e-15 over 5000 steps) and ACTIVE DIFFUSION CANCELLATION — the operator IS a nearest-neighbour NEGATIVE CONDUCTANCE, so an NIC realizes it element by element. "
                + "ANALOGUE: the OSCILLATOR LATTICE with a node-wise gain (a flat gain is mode-independent: residual error |γ − (1 − μ_k)| with minimax 0.3997858349905463 at γ = 0.4 against a required spread of 3734.437 — 1866.7× wrong on "
                + "the smoothest mode) and COUPLED RESONATORS (band-limited: a 16-mode bank leaves 96.4 % of the witness uncompensated; shares k ≤ 1/4/8/16/24/48 = 3.240559e-4 / 1.479430e-3 / 4.833883e-3 / 3.626161e-2 / 8.417047e-2 / "
                + "2.035517e-1). REFUTED: PUMP/LOSS NETWORKS — the source is balanced but the balance is SCALAR, with fixed points on the single Neumann modes (G_012) and an imbalance growing at the fastest mode's rate (a factor e in 125 steps "
                + "at 1 %). THE BINDING CONSTRAINT IS EXACTNESS: with residual loop gain (1 + ε) the closed loop is I + ε(I − W), so mode k evolves at 1 + ε(1 − μ_k) (verified per step); hold times 1/(ε(1 − μ_k)) at ε = 1e-6/1e-5/1e-4/1e-3/1e-2 "
                + "are 1.25e6 / 125 033 / 12 503 / 1250 / 125 steps for the fastest mode against 5.0e9 / 4.7e8 / 4.7e7 / 4.669e6 / 4.669e5 for the smoothest, so a 0.1 % tolerance holds the SMOOTH class for 4.67e6 steps but the witness for only "
                + "1250. A ONE-STEP DELAY IS TOLERABLE (roots {1, μ_k − 1} ∈ [−0.7997858350, 0]; a 1e-3 perturbation retained at 0.9986e-3 … 0.9995e-3 after 2000 steps). POWER IS NOT BINDING: ΔH = 0.2170247 nats per step for the witness ⇒ "
                + "Landauer 8.6295e-20 J per lattice per step (8.63e-14 W at 1 µs) and free energy 1.5192e-19 J (1.52e-13 W), ten orders below a 1 mW controller, while the band-top SMOOTH profile costs ΔH = 0.0 (thermodynamically free). "
                + "Sensor resolution: q ≤ 1.0416667e-10 count units for 1 % of ρ̄ over 1e6 steps. The PHYSICAL devices inherit G_012's marginal character: they hold, they do not create.",
            Formula: "s = -d * Laplacian(rho*);  loop eigenvalue 1 + eps (1 - mu_k);  P_min = k_B T Delta H / tau",
            CalculationId: "physical-actuator",
            References: ["ResearchY-G_013", "ResearchY-G_012", "ResearchY-G_011b"],
            AuditIds: ["g013"]),
        new("rho-mapping", "Rho Mapping", "The measurable ρ analogue: the diagonal occupation (probability) density — the unique κ = 1 observable, whose counting noise IS the theory's own Poisson band.",
            TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["physical-actuator", "gravity-source"],
            Narrative: "Which measurable laboratory quantity corresponds to ρ? THE MAPPING CRITERION: q = F(ρ) is a ρ analogue iff it is positive cellwise, normalised (Σq = 1), AFFINE — κ = d ln q/d ln ρ constant, because both the relaxation W and the "
                + "G_013 stencil (I − W) are LINEAR — and CELLWISE (so it carries the gradient). THE AFFINE TEST IS DECISIVE: for q = ρ^κ the flow error ‖F⁻¹WF(ρ) − Wρ‖ and the actuator error ‖(I − W)F(ρ) − F′(ρ)(I − W)ρ‖ are BOTH EXACTLY ZERO "
                + "at κ = 1 and nonzero otherwise (κ = 0.5: actuator 3.229213; κ = 2: flow 1.047221e-2, actuator 4.668155e-2; κ = 3: flow 1.724505e-2, actuator 4.822371e-3), with the recovered clock factor exactly 1/κ. PHYSICAL: (1) the PROBABILITY "
                + "DENSITY — q = |ψ|² with ψ_j = √ρ_j e^{iθ_j} (QG220) IS ρ (L1 = 2.484991379e-16), so κ = 1 exactly and the map is the identity: the source law a = −(1/d)∇ln q (max|a| = 0.6031746), the clock law Δln q/d, the G_013 stencil (I − W)q "
                + "and the G_007 suppression (33.7781483) all hold identically, and it is the only candidate carrying the ψ-sector (phase); (2) the OCCUPATION DENSITY — the counting face, also κ = 1, whose OWN SHOT NOISE IS the theory's Poisson law: "
                + "⟨N⟩ = 1.6102e-6⁻² = 3.856917553651e11 counts per cell gives δ = 1/√⟨N⟩ = 1.610200e-6 (exactly the observed galactic contrast), a 1 % ceiling of 4.886722e-6 (exactly G_005's accessible band) and P(observed) = 0.6065. CORRELATED: "
                + "(3) the MODE POPULATION — the DCT of the reversal multiplies mode k by (−1)^k, so the POWER SPECTRUM IS INVARIANT (max |Δ|w_k|| = 4.1598669e-15, total spectral energy ratio 1.0000000000000018) while the arrangements are physically "
                + "opposite (max|Δa| = 0.8864864874, acceleration correlation ≈ 0.10): it reproduces the suppression and gives G_013's modal-gain view but is nonlocal; (4) the ENERGY DENSITY — a SPECTRALLY WEIGHTED re-expression: the D96 weight has a "
                + "(numerically) ZERO mode, so ε = λ·ρ vanishes there and ln ε is undefined, and with a positive weight (1 + λ/λ_max ∈ [1,2]) the flow commutation defect is 3.7873408e-4 against a reference 2.0916667e-2 (1.81 %) with the implied clock "
                + "factor spread over [0.5689248, 1.1378497]; non-injective (fixed-energy fibre 94); G_001's verdict stands. REFUTED: (5) the INFORMATION DENSITY — a GLOBAL, permutation-invariant functional (ΔKL = 0 exactly under a roll while L1 = "
                + "0.6583333 and the field moves 1.0031746), zero at the uniform measure; (6) the COHERENCE DENSITY — the ψ-sector: a phase change moves the coherent sum by 281.2241449× at L1(ρ,|ψ|²) = 2.5e-16 and |Δa| < 1e-9, exactly ρ-inert. "
                + "CRITICAL ANSWER: the first experimentally measurable ρ analogue is the DIAGONAL OCCUPATION (PROBABILITY) DENSITY q_i (site-resolved imaging, photon counting, mode-resolved population; κ = 1 exactly); the readable contrast floor is "
                + "1/√⟨N⟩ = 1.6102e-6 = 46.374 ms/day of clock depth (G_009's galactic cross-check), and the residual gap is the IDENTIFICATION premise — the metric coupling G_011b showed is not borrowed.",
            Formula: "kappa = d ln q / d ln rho = 1;  delta = 1/sqrt(<N>) = 1.6102e-6;  P = exp(-<N> Delta^2/2)",
            CalculationId: "rho-mapping",
            References: ["ResearchY-G_014", "ResearchY-G_005", "ResearchY-G_009"],
            AuditIds: ["g014"]),

        new("rho-metric", "Rho To Metric", "Whether a laboratory q profile can move a clock: the analogue contrast is MEASURABLE, the metric channel is REFUTED at laboratory scale, and every metric effect that exists is ASTROPHYSICAL ONLY.", TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["rho-mapping", "clock-rate"],
            Narrative: "Can a LABORATORY q profile produce any measurable metric effect (photon occupation, cavity modes, resonator lattice, oscillator lattice; compute Δτ, ΔΦ and the equivalent gravity)? THE AUDIT ONLY BECOMES WELL-POSED ONCE G_014 IS USED, because the map is the IDENTITY (κ = 1, L1(|ψ|², ρ) = 2.484991379e-16) and so TWO CHANNELS separate: the ANALOGUE channel is the density CONTRAST itself, Δτ/τ = Δln q/d — a measurable RATIO needing no mass — and the METRIC channel is a REAL time dilation, whose only laboratory handle is mass-energy (G_011b: the coupling is not borrowed), giving m = E/c², ΔΦ/c² = G m/(R c²), a = G m/R², for which AT predicts exactly the Newtonian field (G_004's 0.99600 self-checks). THE CLOCK LADDER (f = Δτ/τ ⇒ Δlnρ = 3f ⇒ M/r = f c²/G): the MEASUREMENT FLOOR 1e-18 needs M/r = 1.3466e9 kg/m — 13 466 TONNES WITHIN A CENTIMETRE (1.3466e7 kg) — and Δlnρ = 3.0e-18; 1 ns/day needs 1.5586e13 kg/m; the OBSERVED GALACTIC field (5.3673e-7, Δlnρ = 1.6102e-6) needs 7.2276e20 kg/m; the G_005 band top (1.6289e-6, Δlnρ = 4.8867e-6) needs 2.1935e21 kg/m. The Earth self-check reproduces G_004 (M/r = 9.3740e17 kg/m for 6.9613e-10). THE FOUR CASES (ΔΦ/c² with m = E/c²): (1) PHOTON OCCUPATION — 1 J (5.034116567542709e18 photons at 1 µm) in a 5 mm cavity, m = 1.112650e-17 kg, ΔΦ/c² = 1.6525e-42, a = 2.9705e-23 m/s², 1.65e-24× the floor; (2) CAVITY MODES — 1 kJ in 6 cm (SRF, Q = 1e10), 1.112650e-14 kg, 1.3771e-40, 2.0628e-22, 1.38e-22×; (3) RESONATOR LATTICE — 1 mJ on 1 cm, 1.112650e-20 kg, 8.2627e-46, 7.4262e-27, 8.26e-28×; (4) OSCILLATOR LATTICE — 1 nJ on 1 cm (the G_011b chain), 1.112650e-26 kg, 8.2627e-52, 7.4262e-33, 8.26e-34×. Ordering cavity modes > photon occupation > resonator lattice > oscillator lattice, ALL REFUTED, the best case still TWENTY-TWO ORDERS below the floor (a ≈ 1e-22 m/s² ≈ 1e-23 g). THE STRONGEST POSSIBLE LABORATORY CASE — no device beats an energy-density argument, so the best densities known go into a 1 m ball (V = 4.189 m³): chemical 1e9 J/m³ → 3.4611e-35 (3.46e-17× the floor), capacitor 1e12 → 3.4611e-32 (3.46e-14×), magnetic 3e13 → 1.0383e-30 (1.04e-12×), NUCLEAR SCALE 1e18 → 3.4611e-26 (3.46e-8×, still 2.889e7× SHORT), neutron-star core 1e34 → 3.4611e-10 (3.46e8× ⇒ ASTROPHYSICAL). REQUIRED DENSITIES: u_floor = f c⁴/(G·(4/3)πR) = 2.889272332033454e25 J/m³ (2.889e7× the nuclear scale) and u_band = 4.706335701649293e37 J/m³ (4706.3× a neutron-star core). THE MEASURABLE CHANNEL, PRICED: the ANALOGUE contrast is real and large and its EQUIVALENT MASS is the whole point — the counting floor 1.6102e-6 (= 1/√⟨N⟩, ⟨N⟩ = 3.856917553651e11, exactly G_014's shot noise and G_009's galactic cross-check) is 0.046374 s/day (5.4e11× the floor) but needs 7.2276e18 kg at 1 cm; the G_002 witness 4.8:1 is 45 176.138 s/day needing 7.0409e24 kg (1.18 Earth masses); a 20:1 contrast is 86 277.089 s/day needing 1.3447e25 kg (2.25 Earth masses). CRITICAL ANSWER: NO in the METRIC sense — the strongest laboratory configuration (nuclear-scale density in a metre) is 3.4611e-26 against a 1e-18 floor, a factor 2.889e7 short, and the floor itself is not laboratory physics (13 466 tonnes within a centimetre, 2.889e7× nuclear density); YES in the ANALOGUE sense — Δln q/d is a κ = 1 observable (G_014), readable at 0.046374 s/day at the counting floor and 86 277.089 s/day at 20:1. WHY THE METRIC EFFECTS THAT EXIST ARE ASTROPHYSICAL: the observed galactic field costs 0.0464 s/day at M/r = 7.2276e20 kg/m and the band top 0.1407 s/day at 2.1935e21 kg/m, nine orders above the floor's requirement, so G_004's answer stands. VERDICTS: MEASURABLE = the analogue contrast channel (all four cases, κ = 1) · ASTROPHYSICAL ONLY = every metric effect that exists · REFUTED = a laboratory q profile producing a metric effect (best lab case 3.4611e-26 = 3.46e-8 of the floor). The METRIC verdict is an UPPER BOUND (nuclear density deliberately generous); the laboratory can MEASURE ρ (G_014) and HOLD a pattern (G_012/G_013) but cannot make ρ pull on clocks.",
            Formula: "Delta Phi/c^2 = G m/(R c^2);  M/r = f c^2 / G;  u_floor = 2.889272332033454e25 J/m^3",
            CalculationId: "rho-metric",
            References: ["ResearchY-G_015", "ResearchY-G_014", "ResearchY-G_011b", "ResearchY-G_009"],
            AuditIds: ["g015"]),

        new("watch-ontology", "Watch Ontology", "Whether mass-energy is required to generate ρ: it is not — ρ and the spectrum are siblings, and energy is the rank-1 pairing E = ⟨λ,ρ⟩ that keeps only 1.0526 % of the occupancy.", TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["rho-metric", "density-control"],
            Narrative: "Is MASS-ENERGY required to generate ρ? THE AT-NATIVE CHAIN (no energy appears in it): Difference → distinguishability → the coupling lattice → { a CAPACITY structure (spectrum λ with multiplicities m) , an OCCUPANCY ρ (counting measure, Σρ = 1, QG194) } → E = ⟨λ,ρ⟩ → g₀₀ = −ρ^(2/d) → clocks dτ/dt = ρ^(1/d) (QG197). ρ and λ are SIBLINGS off the lattice; E is their PAIRING. (1) ρ FROM PRIMITIVES ALONE — DERIVED: positivity and Σρ = 1 need ONLY distinguishability; verified A₀ = 45 eigenspaces, multiplicity histogram {1:1, 2:42, 5:1, 6:1}, Σm = 96, free room Σ(m−1) = 51 = N − A₀, total spectral weight Σλ = 1152 (a ρ-blind capacity invariant, λ₀ = 0), and Σρ = 1.0000000000000000 for every configuration. (2) MASS-ENERGY FROM ρ — DERIVED, AND IT IS A PAIRING: E = ⟨λ,ρ⟩ (QG180/QG181) evaluates the spectral weight ON the occupancy — a covector paired with a probability vector; E(uniform) = Σλ/N = 1152/96 = 12.0 EXACTLY; on the 95-dimensional affine set {Σρ = 1} the functional has RANK 1 and a 94-DIMENSIONAL KERNEL = 51 (energy-free BY DEGENERACY, λ constant within a multiplet) + 43 (zero-net mixing of distinct λ), so E retains 1.0526315789473684 % of ρ. Non-degeneracy rearrangements DO move E: the same multiset assigned comonotonically with λ gives 13.540176608029563 (+12.834805066913027 %), anticomonotonically 10.015359929915876 (spread 3.5248166781136874), reverse-witness 12.095189171364584 (+9.518917136458427e-2). (3) DOES ρ REQUIRE MASS-ENERGY? REFUTED, constructively: move density between two cells of the SAME multiplet (λ constant there) so Σρ = 1 stays exact and E is EXACTLY invariant while ρ moves — verified in the m = 6 multiplet at δ = 0.005 and 0.01 (|ΔE| ≤ 1.776e-15, ρ ratios 2.8462 and 49.0000); the canonical witness tilt is ITSELF a pure within-multiplet move (L1 = 0.6666666666666667, ΔE = 0 exactly, max|a| = 0.6031746). The 51-DIMENSIONAL FREE ROOM IS THE PROOF: 51 independent directions in which ρ changes and the total mass-energy does not. (4) THE MINIMAL ρ-CARRYING OBSERVABLE — DERIVED: the CELLWISE counting density itself (the identity map, κ = 1). Dimension ladder: cellwise ρ = 95 dims; the per-multiplet totals (the degeneracy distribution) = 44 dims, losing EXACTLY the 51-dim free room; E = 1 dim, losing 94 — so every coarser carrier destroys precisely the energy-free room. Candidate verdicts: actualization density = SOURCE (the primitive); probability density q = |ψ|² = CARRIER (the identity, QG220/G_014); occupation density = CARRIER (the same read by counting, κ = 1); degeneracy distribution = CORRELATED (the CAPACITY side — it SIZES the room but is invariant under any within-room move, block-sum L1 = 0 to 1e-12); survivor compression = CORRELATED (a FUNCTIONAL of ρ, and not even energy-free: +4.248925e-3 at 48 kept, −2.959751e-1 at 24 kept); E = ⟨λ,ρ⟩ = BOOKKEEPING. CRITICAL ANSWER — does any ρ-carrying observable change the clock rate while Σm stays fixed? YES, EXACTLY: the canonical witness is an allowed configuration with Σρ = 1 and ΔE = 0 (exact in the algebra), yet its cells carry a 20 : 1 contrast — the clock ratio between its extreme cells is 20^(1/3) = 2.7144176165949063 and the clock separation (1/d)ln20 = 0.9985774245179969 = 86 277.089 s/day, with max|a| = 0.6031746 = 3.7459607502174e5 × the observed galactic contrast (reproducing G_005's 3.746e5 requirement); the REALISED band caps at 4.8867e-6 = 0.14073696 s/day and the observed level is 0.04637376 s/day, so the CARRIER exists and the DYNAMICS (G_005/G_008), not the ontology, forbids realisation. VERDICTS: SOURCE = the actualization density ρ, DERIVED from counting alone (the source law and the clock law depend on ρ and NOTHING else — no coupling constant, no mass, no energy) · CARRIER = the occupation (probability) density, the cellwise identity κ = 1 · BOOKKEEPING = mass-energy E = ⟨λ,ρ⟩, a rank-1 pairing discarding 94 of ρ's 95 dimensions, DERIVED as a functional with the VALUE Σλ = 1152 (hence E = 12) BOUNDARY (inherited from the D96 lattice and K = 6). WHAT THIS DOES TO G_015: its metric ladder was priced through m = E/c²; the import was not \"ρ needs energy\" (FALSE) but \"the only laboratory handle on the metric is energy\" (TRUE) — the rank-1 projection of a 95-dimensional object. No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new primitive.",
            Formula: "E = <lambda, rho>;  rank 1 of 95;  kernel 94 = 51 + 43;  free room Sigma(m - 1) = 51",
            CalculationId: "watch-ontology",
            References: ["ResearchY-G_016", "ResearchY-G_002", "ResearchY-G_011", "ResearchY-G_014"],
            AuditIds: ["g016"]),

        new("mass-independence", "Mass Independence", "Whether ρ is free of the energy or the reverse: ρ is independent of E (94-dimensional kernel) while E is fully determined by ρ — and at fixed energy the clock separation is unbounded.", TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["watch-ontology", "density-control"],
            Narrative: "Can two states have the SAME energy but DIFFERENT ρ, or the SAME ρ but DIFFERENT energy? THE THREE ANSWERS ARE NOT SYMMETRIC. INDEPENDENT — same E, different ρ: the kernel of ρ ↦ E on {Σρ = 1} is 94-DIMENSIONAL, explicitly 51 (DEGENERACY — λ constant within a multiplet, so E is invariant by symmetry) + 43 (λ-MIXING — zero net over DISTINCT λ). The canonical witness tilt is a pure within-multiplet move: ΔE = 0 EXACTLY, Δρ = L1 = 0.6666666666666667, contrast 20 : 1, Δτ/τ = (1/d)ln20 = 0.9985774245179969 = 86 277.089 s/day. Pairwise moves inside an m = 2 multiplet (Δρ = 2δ exactly): δ = 0.002 → ΔE = −1.776e-15, Δρ = 0.004, contrast 1.4752, Δτ = 0.129609; δ = 0.005 → Δρ = 0.010, contrast 2.8462, Δτ = 0.348656. The 43-dimensional λ-mixing room is populated explicitly: cells 94/92/90 with λ = 15.837372467014836, 15.790176186632262, 15.414213562373096 (three DIFFERENT multiplets) and v = (1, −1.1255345008711273, 0.12553450087112727) satisfying Σv = 0 and ⟨λ,v⟩ = 0 EXACTLY; at scale 0.004 → ΔE = −1.776e-15, Δρ = 0.009004, ρ_min = 0.005915, contrast 2.437501, Δτ/τ = 0.29699105 = 25 660.0263 s/day. DEPENDENT — E is a FUNCTION of ρ (given the capacity λ): one ρ, one E, so the independence is strictly ONE-DIRECTIONAL (many ρ per E, never many E per ρ). REFUTED — same ρ, different E as a STATE change: impossible on a fixed lattice; the only handle is the CAPACITY. With the SAME uniform ρ = 1/96 the pairing gives E = 2K EXACTLY for K = 1…6 (2, 4, 6, 8, 10, 12) because Σλ = 192K (192, 384, 576, 768, 960, 1152), with A₀ = 49/47/45/47/45/45 and L1(ρ_K, ρ_6) = 0 — so E moves only through K, which is a BOUNDARY input (G_007). THE PHASE SECTOR: four phase assignments give L1(|ψ|², ρ) < 2.5e-16, ΔE = 0 and Δτ = 0 exactly, while the coherent sum |Σψ| moves 0.03241962809423954 → 9.117182187865382 (ratio 281.22414487183346, exactly G_011/G_014) — energy-inert and clock-inert. THE FIXED-E FAMILY AND WHY Δτ IS UNBOUNDED: tilting every multiplet with fraction f on its first cell (fr = 1 for m = 1) gives EXACTLY ΔE = 0, contrast = 5f/(1−f) (f ≥ 1/2), L1(f) = (2/96)[42|2f−1| + |5f−1| + |6f−1|] and Δτ/τ = (1/d)ln(5f/(1−f)) = T ⇔ f = 1/(1 + 5e^{−3T}); the ladder is f = 0.8, 0.947377910367, 0.987757963984, 0.999383331494, 0.999998470491 at T = (1/d)ln20, 1.5, 2, 3, 5 — and since L1 → 1.0625 while ρ_min → 0 with ΔE = 0 throughout, **the clock separation at fixed energy is UNBOUNDED: no target Δτ is forbidden by energy conservation**. Only positivity and the G_005 band bound it (T = 4.8867e-6 needs f = 0.1666687028016166, contrast 1.0000146602074598; realised 0.14073696 s/day against observed 0.04637376 s/day). At the canonical T = (1/d)ln20 the inversion returns f = 0.8 — the canonical TiltFractions IS the 20 : 1 solution of this family. VERDICTS: INDEPENDENT = same energy, different ρ · DEPENDENT = E is a function of ρ, so the independence is one-directional · REFUTED = same ρ, different energy as a state change (only the BOUNDARY capacity K does it). Independence is not symmetry: ρ is independent OF E while E is fully determined BY ρ. No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new primitive.",
            Formula: "kernel of rho -> E on {Sigma rho = 1}: 94 = 51 + 43;  E = 2K;  contrast = 5f/(1 - f) at DeltaE = 0",
            CalculationId: "mass-independence",
            References: ["ResearchY-G_016b", "ResearchY-G_016", "ResearchY-G_002", "ResearchY-G_005"],
            AuditIds: ["g016b"]),

        new("metric-coupling", "Metric Coupling", "Whether a real optical |ψ|² can move a clock: the substrate reading is derived and consistent, the laboratory identification is a boundary input, and its naive form is experimentally excluded at bench scale.", TheoryLayer.Physics, TheoryClassification.Derived, TheoryObjectKind.Derivation,
            ["mass-independence", "rho-metric"],
            Narrative: "Does any EXPERIMENTALLY REALIZABLE |ψ|² profile produce a MEASURABLE clock shift? Systems: optical cavity, resonator array, photon lattice, oscillator network; measure Δτ, ΔΦ and the clock signal; compare AT, GR and the observed limits. THREE READINGS, ONE LAW: AT-substrate (ρ = the actualization density, G_014) with Δτ/τ = ΔΦ/c² = (1/d)Δlnρ; AT-naive (a laboratory intensity profile IS ρ, so ρ ∝ I) with Δτ/τ = (1/d)Δln I; GR (the stored energy U with m = U/c²) with ΔΦ/c² = G U/(R c⁴). The observable is a FRACTIONAL FREQUENCY RATIO, so the ceiling is a clock's fractional resolution: 1e-18 (best optical clocks), 1e-12 (a crude laboratory systematic). DERIVED — the SUBSTRATE reading is NOT refuted: the Earth's surface gives Δlnρ/d = 2.320443e-10 = 2.320e8 × the floor (G_004's AT/GR = 0.99600) and the galactic field 5.367333e-7 = 0.046374 s/day = 5.367e11 × the floor (G_009's cross-check 0.99668); the logarithmic form is derived too (depths add where ratios multiply). THE FOUR SYSTEMS, with stored energies recomputed from F, Q, P, λ and the lattice depth: (1) OPTICAL CAVITY (F = 1e6, 1 W, 0.3 m; U = (F·P_in/π)·2L/c = 6.370605e-4 J; Gaussian contrast 1e2 from Δln I = 2r²/w₀²) gives AT-naive 0.6666666667 vs GR 3.509234e-47, ratio 1.900e46, exclusion 6.667e17; (2) RESONATOR ARRAY (Q = 1e7, 1 mW, 1550 nm; U = QP/ω = 8.228698e-12 J, V = λ³ = 3.723875e-18 m³, u = 2.209714e6 J/m³, R = 9.615433e-7 m; on/off 1e4) gives 3.0701134573 vs 7.071071e-50, ratio 4.342e49, exclusion 3.070e18; (3) PHOTON LATTICE (Sr clock: E_rec = 2.272842e-30 J at λ = 813 nm, 88 u, depth 100 E_rec, 300 a.u. polarizability → I = 2.439413e8 W/m²; node/antinode 1e3) gives 2.3025850930, exclusion 2.303e18; (4) OSCILLATOR NETWORK (Q = 1e6, 1 pW, 6 GHz; U = 2.652582e-17 J, u = 2.652582e-8 J/m³, R = 6.203505e-4 m; 10:1) gives 0.7675283643 vs 3.533090e-58, ratio 2.17e57, exclusion 7.675e17. So AT-naive spans 0.7675 … 3.0701 = 77–307 % while GR spans 1e-47 … 1e-58. THE SHARPEST TEST: a Sr LATTICE CLOCK INSIDE ITS OWN STANDING WAVE operates at 2.439413e8 W/m² with node/antinode contrast ≥ 1e3 and reads a reproducible frequency to 1e-18 every day, while AT-naive predicts a 230.26 % shift across its own lattice — so the naive identification is excluded by 2.303e18 by the very experiment that best tests it (contrast ladder 1e2/1e3/1e4/1e6 → 1.53505673 / 2.30258509 / 3.07011346 / 4.60517019, exclusions 1.5351e18 / 2.3026e18 / 3.0701e18 / 4.6052e18). EXCLUSION AGAINST EVERY CEILING (cavity/resonator/lattice/network): 1e-12 → 6.667e11 / 3.070e12 / 2.303e12 / 7.675e11; 1e-15 → 6.667e14 / 3.070e15 / 2.303e15 / 7.675e14; 1e-18 → 6.667e17 / 3.070e18 / 2.303e18 / 7.675e17; 1e-19 → 6.667e18 / 3.070e19 / 2.303e19 / 7.675e18 — no ceiling exists at which any realizable profile survives, and the failure is STRUCTURAL not engineering, because the law is LOGARITHMIC: Δτ/τ = (1/3)ln(contrast) is O(1) for every realizable contrast (> 10), with no tuning that makes it small. CRITICAL ANSWER: NO — no experimentally realizable |ψ|² produces a measurable AT metric clock shift; the effect that exists is the SUBSTRATE's and is consistent with GR to the precision AT predicts. VERDICTS: DERIVED = the logarithmic clock law and the substrate reading (2.320443e-10 / 5.367333e-7 = 0.046374 s/day, matching 0.99600 / 0.99668) · BOUNDARY = the identification of a laboratory |ψ|² with ρ, and the scale invariance (only ratios of ρ are physical; the coupling is a boundary input a readout does not borrow, G_011b) · REFUTED = any experimentally realizable |ψ|² metric clock shift (6.667e17 … 3.070e18 at the 1e-18 ceiling; 6.667e11 … 3.070e12 even at 1e-12; AT-naive vs GR = 1.900e46 … 4.342e49 with the observation agreeing with GR). WHY THE MISMATCH IS STRUCTURAL: for the same bench system the two readings differ by 46 to 49 orders of magnitude, so the identification premise flagged as residual in G_014/G_015 is NOT merely unproven but EXPERIMENTALLY EXCLUDED AT BENCH SCALE — G_011b's 'the coupling is not borrowed' promoted from a structural statement to a measured exclusion. G_014's PHYSICAL verdict is about κ = 1 STRUCTURE (which observable to measure), not about the coupling, so it remains consistent. No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new primitive.",
            Formula: "DeltaTau/tau = (1/d) Delta ln I;  AT-naive 0.7675 .. 3.0701 vs GR 1e-47 .. 1e-58;  exclusion 6.667e17 .. 3.070e18",
            CalculationId: "metric-coupling",
            References: ["ResearchY-G_017", "ResearchY-G_014", "ResearchY-G_015", "ResearchY-G_011b"],
            AuditIds: ["g017"]),

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
            "ABSENT: the canonical ring yields only classical correlation; genuine Bell entanglement is not produced.",
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
            "ΩΛ is the information-bookkeeping fraction: a DERIVED information observable whose energy interpretation is HOSTED (no derived vacuum energy, no derived equation of state w = −1; a 1/R² scaling of the cosmological constant would give w = −1/3).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-lambda", "iocc"]),
        new("np056", "Equation-of-State Audit", "Can the informational ontology generate a unique equation of state?",
            "NO — ΩΛ is a snapshot density fraction with no equation-of-state content: the dynamics are degenerate (a 64% spread in H² across w), acceleration needs a hosted w < −1/3, and a time-independent ΩΛ forces w = 0 and deceleration.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Boundary,
            ["omega-lambda"]),
        new("np057", "Dark Energy Meaning Audit", "What does ΩΛ physically represent — and why information as a density fraction?",
            "ΩΛ = the normalized ENTROPY DEFICIT (information surplus) of the [4,4,87] occupancy — a DERIVED state descriptor (order parameter), monotone in top-heaviness, not a cause and not an energy density.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Derived,
            ["omega-lambda", "iocc"]),
        new("np058", "Information-to-Energy Bridge Audit", "Where does the information → energy-density mapping originate?",
            "At the definition 'energy = actualization rate' — a DEFINITION, inherited by the cosmological-constant relation (Λ = 8πG·ρ_Λ, importing G = ħc/M_Pl²) and realized as the density fraction; every step before this bridge is pure counting and information.",
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
            "A DESCRIPTOR link via an unproven common origin (the count density ρ): causal and scaling-law links are REFUTED. Four non-derived assumptions — the KL measure, the N=96 window, the energy-definition bridge, and the dimensionful anchors — carry the equality.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-lambda", "iocc"]),
        new("np064", "Canonical Structure Necessity Audit", "Why does {N=96, K=3, [4,4,87]} exist?",
            "A DERIVED-BOUNDARY hybrid: the period-3 seed (DERIVED) forces N = 3·2^k, the 3-family window [4,8) (BOUNDARY) selects k=5 → N=96 → [4,4,87] → ΩΛ = 0.6839. Root: seed (derived) × window (boundary).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "occupancy"]),
        new("np065", "Dark Matter Ontology Audit", "What is Dark Matter (Ωm) inside Actualization Theory?",
            "Dark Matter = the matter DEFICIT m = ρ̄ − ρ (an effect, not a particle), Ωm = H/ln K = 0.3161 (the realized-entropy fraction). The deficit SOURCES gravity (DERIVED) — flat rotation (equal deficit per octave) and mass proportional to radius — so Ωm has stronger physical meaning than the descriptive dark-energy surplus; both share the hosted energy reading (the 'energy = actualization rate' definition).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Derived,
            ["omega-matter", "iocc"]),
        new("np066", "Dark Matter Evidence Audit", "Which observed dark-matter phenomena does the deficit reproduce?",
            "PARTIAL: 2 DERIVED (flat rotation from equal deficit per octave, Ωm = 0.3161) / 2 CORRESPONDENCE (cluster mass degenerate with ΛCDM, large-scale-structure seed + growth) / 2 REFUTED (lensing — the conformally flat metric does not bend light; Bullet Cluster — not a particle). The deficit is a gravitational-potential surrogate, not a full dark matter.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Correspondence,
            ["omega-matter", "iocc"]),
        new("np067", "Lensing Sector Audit", "Why does the deficit source gravity but fail light-bending?",
            "The density-only metric is CONFORMALLY FLAT (γ = −1), cancelling the null-geodesic prefactor (1+γ)/2 = 0 — no lensing — while potential effects (the time component g₀₀) survive. The minimal fix is the ψ tensor sector (the second primitive), restoring γ = +1 and full lensing. This is a missing tensor sector, not a fatal failure.",
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
            "The deficit produces the SEED and LINEAR GROWTH of structure derivatively (Poisson seed δ_i = 1/√⟨N⟩, scale-free variance, δ ∝ a, spectral index n_s = 0.96497), but it forms halos, galaxy profiles, and clusters only with EXTRA ASSUMPTIONS: the flat-rotation profile (v² ≈ const ⇒ M ∝ r ⇒ ρ ∝ r⁻²) requires the equal-deficit-per-octave (log-deficit) abundance law — a symmetry selection, not a dynamical attractor. The profile is steeper than the NFW cusp and singular at the centre; the NFW concentration is fitted.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Partial,
            ["omega-matter", "iocc"]),
        new("np078", "Alpha=0 Necessity Audit", "Why must the universe select α=0 instead of α≠0?",
            "The equal-deficit-per-octave point (α=0) is the UNIQUE point where flat rotation (v² ∝ r^(−α), slope 0), stability, criticality (μ=1 ⟺ α=0), and maximum entropy coincide. It is DERIVED (unique) as a selection fixed point, NOT a dynamical attractor (conservation gives the repulsive ρ∝r⁻², scale-freeness a continuum). The conditioning input is scale-freeness (the indifference principle — the primitives carry no intrinsic scale).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Cosmology, TheoryClassification.Derived,
            ["omega-matter", "iocc"]),
        new("np079", "Scale-Freeness Origin Audit", "Can scale-freeness (the indifference principle — the primitives carry no intrinsic scale) be derived from Difference itself, or is it the final irreducible boundary?",
            "Scale-freeness is DERIVED from Difference, NOT the final boundary. Difference is a BINARY (metric-free) relation, so the primitives it grounds (Q-events, the counting measure as a density of weight d, the causal order) carry no scale; scale-freeness follows as the unique renormalization-invariant abundance (the power law = RG fixed point = indifference). The true final boundary is Difference itself.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Derived,
            ["difference", "iocc"]),
        new("np080", "Difference Duality Audit", "Why does Difference split into exactly one scalar face (ρ) and one tensor face (ψ)?",
            "Difference actualizes into a SYMMETRIC rank-2 object (A_ij = A_ji), whose decomposition is exhaustively spin-0 TRACE (1 = ρ, scalar/count/isotropic) ⊕ spin-2 TRACELESS (5 = ψ, tensor/orientation/Weyl, 2 TT polarizations). 6 = 1 + 5: no third component, no vector face, no spin-≥3. The duality is DERIVED (rank-2 decomposition); primitive cost = 1 (Difference, with ρ and ψ as its two faces).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Derived,
            ["difference", "omega-matter"]),
        new("np081", "Energy Ontology Audit", "What is energy physically inside Actualization Theory?",
            "Energy is a RELABELING of actualization dynamics — neither fundamental nor emergent. The conserved object is the COUNT (Σρ = 1, Σm = 0, DERIVED); 'energy' is that count renamed ('energy = actualization rate', a definition) and unit-ized (anchors v, m_e + ħ, c). Noether's theorem fails (discrete time). Removing energy language loses nothing derived.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference", "iocc"]),
        new("np082", "Electron Mass Anchor Audit", "Why does the fermion spectrum require the electron mass anchor — is m_e the true boundary?",
            "m_e is NOT the true remaining matter-scale boundary — it is a REPLACEABLE unit conversion. All mass ratios are DERIVED (dimensionless: m_μ/m_e = 207.03, m_τ/m_μ = 16.842); the absolute scale m_e carries only the DIMENSION (MeV), which no derived D96 invariant supplies. The true boundary is 'one dimensionful scale' (irreducible); m_e is its replaceable instance (m_e ↔ M_Z ↔ v).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Boundary,
            ["electron", "iocc"]),
        new("np083", "Threefold Structure Audit", "Do the remaining appearances of 3 (color count 3, family count 3) share a common origin?",
            "YES — a hidden common derivation: both descend from the period-3 seed p = 3 (DERIVED), which forces the factor 3 in N = 3·2^k → 3 octave bands → 3 families AND the su(3) color algebra (8 = 3²−1 from the 3 families). Two boundary residues remain: the family window [4,8) and the color-count identification (a postulate).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["families", "occupancy"]),
        new("np084", "Eta Framework Audit", "What is η physically — the reference against which trace, traceless, conformal flatness, and Weyl are defined?",
            "η is the conformal reference metric — a FRAMEWORK boundary, irreducible and necessary, but not a physics primitive. It defines the trace (ρ), traceless (ψ), conformal flatness, Weyl content, metric g = ρ^(2/d)η, and PPN γ. Removing η breaks the entire geometric reading. Not derivable from Difference: the contraction presupposes η, so {Difference, η} = content + reading, genuinely two.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference", "eta"]),
        new("np085", "Completeness Frontier Audit", "What major physical phenomena remain outside the current derived ontology?",
            "The frontier map: DERIVED — foundations, D96 structure, information (ΩΛ = 0.6839, Ωm = 0.3161), matter (deficit), particles (resonance classes, mass ratios, quantum numbers, forces), scalar gravity. CORRESPONDENCE (hosted) — energy reading, acceleration, lensing/GW (ψ), clusters, weak/strong couplings, Bose statistics. BOUNDARY — w, temperature, seven inputs + one scale. MISSING — condensed-matter physics and nuclear structure. REFUTED — blackbody, Bullet Cluster.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Correspondence, TheoryClassification.Partial,
            ["difference", "omega-matter"]),
        new("np086", "Difference Necessity Audit", "Is Difference itself necessary, or could a weaker primitive generate the same ontology?",
            "Difference is the MINIMAL and UNIQUE primitive — the bare logical possibility of distinction. Not derivable, not replaceable: identity is insufficient (self-identity does not give a ≠ b), relation presupposes distinct relata, count/information/symmetry are downstream. Identity is the logical dual but the preservation relation (conservation, higher); Difference is the generation relation (bottom). No weaker primitive exists.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference"]),
        new("np087", "Nuclear Structure Audit", "Can Actualization Theory explain nuclear structure — binding energies, magic numbers, shell structure?",
            "Nuclei are CORRESPONDENCE-ONLY (D). The nucleons are DERIVED (proton/neutron quark composites, isospin doublet) and the strong coupling is a CORRESPONDENCE (α_strong = 8/Σ√m); but nuclear structure is MISSING — binding energies, magic numbers [2,8,20,28,50,82,126], and shells. Reason: the D96 ring is 1D (mirror-pair degeneracies), while nuclear shells are 3D spherical harmonics + spin-orbit.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Physics, TheoryClassification.Correspondence,
            ["families", "omega-matter"]),
        new("np088", "D96 Network Geometry Audit", "Is the ontology truly a single 1D ring, or does it already contain a higher-dimensional network geometry?",
            "The theory ALREADY contains an EMERGENT 3D geometry: D96 ⊗ D96 ⊗ D96 raises the DOS exponent to p = 3 (cubic lattice), and d = 3 is DERIVED (QG197's (d−2) bridge). D96 is a seed AND a node (Weyl law p = d). But nuclear structure is still not rescued: the cubic lattice has octahedral symmetry (irreps 1,2,3), while nuclear shells need rotational symmetry (2l+1) — the magic numbers still do not follow. Refines NP_087: '1D' was too narrow; 'missing' survives.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Emergent,
            ["d96", "occupancy"]),
        new("np089", "Rotational Symmetry Emergence Audit", "Can the cubic D96 network generate effective rotational symmetry O(3) at large scale?",
            "O(3) is APPROXIMATE ONLY. The free lattice dispersion ω² = k² − (k_x⁴+k_y⁴+k_z⁴)/12 + … is isotropic to leading order (k²), but the cubic correction breaks O(3) with an O((ka)²) anisotropy that is suppressed yet never vanishes. The theory is discrete (N=96), so the exact continuum (a→0) is never reached; the 2l+1 degeneracies split, and nuclear structure remains missing.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Emergent,
            ["d96", "occupancy"]),
        new("np090", "D96 Network Ontology Audit", "What physically is the D96 network — what are the nodes, what are the links, and what propagates?",
            "The D96 network is the ORGANIZED DIFFERENCE STRUCTURE. Nodes = distinctions (Difference events = actualizations); links = the adjacency (the symmetric rank-2 connectivity A_ij = A_ji); what propagates = the 95 resonance modes (particles) and the two faces ρ (trace → metric) and ψ (traceless → curvature). 96 nodes × degree 12 = 576 links, trace Σλ = 1152. Particles are modes of one D96 ring; geometry is emergent from connectivity.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "difference"]),
        new("np091", "Network Geometry → Spacetime Audit", "How does the D96 network become the observed spacetime?",
            "Three steps: network (nodes = distinctions, links = adjacency) → geometry (g = ρ^(2/d)η = the trace face; ψ = the traceless/Weyl curvature) → spacetime (3D space EMERGENT via D96⊗D96⊗D96 with d=3 derived; +1 time = the actualization tick, a framework residue). Only space is emergent; time is the tick. ρ does double duty (metric factor + expansion a = ρ^(1/d)). First non-derived step = {Difference, η} + the tick.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Emergent,
            ["d96", "difference"]),
        new("np092", "Network Propagation Audit", "What propagates on the network?",
            "Propagation is NOT transport: the network is static (nodes/links do not move), and nothing substantial travels. The only genuine movement is the actualization TICK (the causal-order advance, massless null M_eff=0); its native propagation law is light along null geodesics (n=1, DERIVED). Everything else only APPEARS to move: a particle is a standing wave (envelope moves at the group velocity v_g); the graviton is a ψ ripple; a force is link-mediated action; count redistributes by continuity. Velocity = v_g (≤ c); locality = adjacency; causality = the partial order.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "difference"]),
        new("np093", "Actualization Selection Audit", "If nothing travels, what determines WHICH node actualizes?",
            "The node-selection law is the BORN RULE: at each tick, one count is realized on one node, selected with probability ρ_k = |ψ_k|² (the conserved, normalized count share, Σρ = 1 EXACT). The phase advances deterministically (Δθ = 2πk/N, D_041) and is NOT the selector; the count realizes probabilistically (Born) and IS the selector. The weight is DERIVED (count conservation, QG216); the only boundary is the irreducible stochastic realization — one outcome per tick — the discrete tick (the deepest boundary).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "difference"]),
        new("np094", "Inertia Ontology Audit", "What is inertia inside Actualization Theory?",
            "Inertia = RESONANCE PERSISTENCE: a particle is a resonance mode (a frequency attractor), and its frequency ω₀ (mass) and wave number k (momentum) are fixed spectral labels. Free actualization — the tick, the Born-rule selection, and the deterministic phase advance — does not change k, so the envelope keeps moving at constant v_g = dω/dk. Only a generator action (a force) changes k. Newton I (F=0 → v=const) is the network statement that a mode's phase gradient is conserved under free actualization. Momentum = the phase gradient k; mass = the rest frequency ω₀; the electron (ω₀>0) has inertia, the photon/graviton (ω₀=0) have none.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "difference"]),
        new("np095", "Friction Ontology Audit", "What is friction inside Actualization Theory?",
            "Friction = RESONANCE SCATTERING realized as MODE MIXING: the incoherent accumulation of generator actions (resonance transitions) between a propagating mode and the deficit excitations (matter) in its path. Each scatter changes the wave number k (momentum); the aggregate redistributes k into the material (dissipation) while preserving the resonance class ω₀ (identity). Friction scales with matter density — zero in vacuum (k conserved, pure inertia), growing gas → liquid → solid. It is the exact opposite of inertia: inertia = no transitions (k conserved), friction = transitions (k changes); both reduce to the generator action (once = force, many = friction).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "difference"]),
        new("np096", "Heat Ontology Audit", "What is heat inside Actualization Theory?",
            "Heat = RANDOM PHASE = MODE MULTIPLICITY = RESONANCE DECOHERENCE, realized as count redistribution under count conservation. Friction's lost phase gradient k becomes incoherent (random-phase) mode excitation spread over many modes — heat. Entropy H = −Σρ ln ρ is its measure (mode multiplicity); entropy growth = increasing mode access. Heat erases information (I_occ = ln K − H → 0). One ontology: motion (coherent phase) → friction (scattering) → heat (random phase) → entropy (mode multiplicity).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "difference"]),
        new("np097", "Temperature Ontology Audit", "What is temperature inside Actualization Theory?",
            "Temperature = the COUNT-DISTRIBUTION WIDTH — the thermodynamic conjugate of entropy: T = ∂U/∂S, where U is the conserved count (energy) and S = H = −Σρ ln ρ is the entropy/mode multiplicity. Derived from the occupancy ρ alone (dS/dU = β exact). Cold = narrow occupancy (few modes), hot = wide (many modes); H and the width monotonically track T. The dimensionless temperature is DERIVED; the absolute Kelvin scale is BOUNDARY (the k_B unit anchor, like m_e/v).",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Derived,
            ["d96", "difference"]),
        new("np098", "Localization Ontology Audit", "What is localization inside Actualization Theory?",
            "Localization = the CONSTRUCTIVE INTERFERENCE of a WAVE PACKET (a superposition of resonance modes) that peaks |ψ|² = ρ at one node. A particle is a propagating resonance (its localization is the Born distribution's envelope peak, its trajectory the actualization chain); a single node is refuted. Fourier uncertainty Δx·Δk = 1: a single mode is delocalized (|ψ|² uniform), a wave packet is localized. Position = the envelope peak; time = the tick (envelope at v_g). Wave packet + Born weight DERIVED; localization EMERGENT; node basis FRAMEWORK.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Emergent,
            ["d96", "difference"]),
        new("np099", "Classicality Emergence Audit", "How does classical reality emerge from actualization?",
            "Classicality = DECOHERED LOCALIZATION: a localized resonance (wave packet) is quantum while its interference term survives; friction scatters it until γ·t ≫ 1, when the fringes vanish and the object follows a single non-interfering worldline. Decoherence (A) is the mechanism, via repeated actualization (B) on a stable resonance hierarchy (C), with entropy dominance (D) the signature. Macroscopic bodies (~10²³ modes) decohere instantly. Classical limit (ℏ→0) = decoherence-dominant (γ·t ≫ 1) = large-N/high-entropy. Classicality is EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Emergent,
            ["d96", "difference"]),
        new("np100", "Bound Structure Audit", "What is a bound structure inside Actualization Theory?",
            "Binding = RESONANCE LOCKING = PHASE SYNCHRONIZATION = DEFICIT CLUSTERING (A = B = D), held by PERSISTENT GENERATOR ACTION (C). A bound state is a stable mutual configuration of resonances whose relative phase is locked (constant across ticks); it persists because the lock is a stable fixed point of actualization (the bound-state analogue of inertia). Cascade: modes → pair (e⁻+p) → atom (hydrogen, 13.6 eV) → molecule. Binding DERIVED; the bound structure EMERGENT; binding energies BOUNDARY.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Emergent,
            ["d96", "difference"]),
        new("np101", "Hierarchical Structure Audit", "Why do stable structures appear at many different scales?",
            "ONE universal, scale-free stability principle: a stable structure = a DEFICIT CLUSTERING (matter, NP_071) locked at a GENERATOR-BALANCED FIXED POINT (resonance locking, NP_100). A = B = C = D. Scale-free (AT-F1), it repeats self-similarly over 36 orders of magnitude (particle 10⁻¹⁵ m → galaxy 10²¹ m): particle → atom → molecule → crystal → planet → galaxy. Principle DERIVED; the hierarchy EMERGENT; sizes/energies BOUNDARY.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Structure, TheoryClassification.Emergent,
            ["d96", "difference"]),
        new("np102", "Existence Ontology Audit", "What does it mean for something to exist inside Actualization Theory?",
            "To exist = to be a PERSISTENT, DISTINGUISHABLE structure — a Difference that endures actualization. Minimum condition = Difference (distinguishability) + stability (persistence). A = B = C (persistence = stable resonance = bound deficit structure); D (observability) is a consequence. Remove stability → everything dissolves, only Difference survives. Difference BOUNDARY; stability DERIVED; the hierarchy EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference"]),
        new("np103", "Nonexistence Ontology Audit", "What ceases to exist inside Actualization Theory?",
            "Nonexistence = the negation of existence: a thing ceases to exist when it loses its DISTINGUISHABILITY (Difference) or its PERSISTENCE (stability) — NOT(Existence) = ¬Difference ∨ ¬Persistence. Decay (¬Persistence) and thermalization (¬Difference) are the full channels; loss of localization/binding is partial. Information is REDISTRIBUTED, not destroyed; the final stage is structure → pattern → noise → uniform (ρ_k=1/K) → Difference. Nonexistence reaches uniform noise, never nothing.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference"]),
        new("np104", "Difference Persistence Audit", "Can Difference itself ever cease?",
            "NO — Difference is INDESTRUCTIBLE and CONSERVED. Complete uniformity is uniform OCCUPANCY (ρ_k = 1/K), still with K=95 distinct modes. Difference cannot decay, thermalize, or become uniform — every process presupposes the distinctions it operates on. There is no state with no distinctions; 'Difference ceased' is itself a difference. Difference BOUNDARY (the ground); conservation DERIVED; disappearance REFUTED.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference"]),
        new("np105", "Difference Conservation Audit", "Is Difference the true conserved quantity of Actualization Theory?",
            "YES — Difference is the PARENT conservation law (A = B = C); D (independent) is REFUTED. Every conserved quantity projects from the indestructible Difference structure: count (Σρ=1) = the measure, information = the structure, momentum/charge = the Noether symmetries (1+3+8 = 12 generators). Existence is NOT conserved (can cease). Remove Difference → everything breaks. Conservation of Difference BOUNDARY; projections DERIVED.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference"]),
        new("np106", "Actualization Necessity Audit", "Why must Difference actualize?",
            "Difference must actualize because a Difference is an ACT, not a state: a 'static Difference' is a contradiction (an un-drawn distinction is no distinction). Actualization is a LOGICAL NECESSITY (C); not an independent primitive (B refuted); only the discreteness of the tick is the boundary residue. Remove actualization → nothing survives. The minimal reason: a distinction must be drawn to be a distinction.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference"]),
        new("np107", "Tick Necessity Audit", "Why does actualization occur as discrete ticks?",
            "Because Difference is BINARY (a discrete cut, not a continuum). The tick's discreteness is a LOGICAL NECESSITY (A), DERIVED from the binary nature; continuous actualization is incoherent (blurs the cut into undifferentiated unity). Refines QG011: the deepest boundary is the binary nature, not the tick. Discreteness DERIVED; the binary nature BOUNDARY.",
            AuditStatus.Passed, new DateTime(2026, 9, 6), TheoryLayer.Foundations, TheoryClassification.Boundary,
            ["difference"]),
        new("np108", "Falsification Frontier Audit", "What observation would directly falsify Actualization Theory?",
            "AT is FALSIFIABLE at every level. Strongest = the single-valued numerics (n_s = 0.96497, ℓ₁ = 220.48, ΩΛ = 0.6839, Ωm = 0.3161, mass ratios, 0νββ m_ββ = 2.02 meV); strongest uniquely-AT = the discrete tick (AT-P042); weakest = ontological claims; strongest vulnerability = nuclear structure (O(3) approximate only). Anchors and w = −1 are BOUNDARY (imported). 'AT is unfalsifiable' REFUTED.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Derived,
            ["difference", "d96"]),
        new("np109", "Weakest Link Audit", "Where is Actualization Theory most likely to fail?",
            "The weakest link is NUCLEAR STRUCTURE (O(3) approximate only — magic numbers not exact), with condensed matter the largest unmapped domain. Foundations/particles/forces ROBUST; cosmology/gravity PARTIAL (w=−1 hosted, ψ boundary); nuclear/condensed MISSING. Strongest evidence = the tight cosmology numerics; weakest = nuclear. Single most-likely falsifier = exact magic numbers from the cubic lattice.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Derived,
            ["difference", "d96"]),
        new("np110", "Condensed Matter Gateway Audit", "Can the Difference → D96 ontology naturally generate collective matter?",
            "YES — condensed matter is the NEXT NATURAL LAYER above binding and hierarchy, NOT beyond D96. Crystal = repeated bound network; phonon = collective mode; magnet = phase-locked network; superconductor = phase coherence. A = B = C; D (new ontology) refuted. Only larger networks needed. First failure = the cubic O_h anisotropy (same as nuclear). DERIVED/EMERGENT/PARTIAL.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np111", "Observer Ontology Audit", "What is an observer inside Actualization Theory?",
            "An observer = a PERSISTENT DIFFERENCE STRUCTURE that actualizes (reads) distinctions — a distinguisher within the network. Observation = actualization = information acquisition = difference recognition = persistent-structure interaction (A = B = C = D). Particle < detector < observer (continuous). Observers special only in integration scale, not kind. Resolves M_001 OP1.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Derived,
            ["difference"]),
        new("np112", "Reality Appearance Audit", "Why does a persistent observer experience a stable reality?",
            "Reality = A = B = C = D: the sequence of actualizations integrated by a persistent observer into an emergent narrative. Two stabilities multiply (world persists + observer persists, decohered) → repeated readings agree → a stable, continuous reality. Reality is STRUCTURE-RELATIVE (persistent structures reading persistent structures), not observer-relative. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np113", "Observed Ontology Audit", "What does \"observed\" mean inside Actualization Theory?",
            "'Observed' = a distinction INCORPORATED INTO A PERSISTENT OBSERVER (D). Distinct from actualized (universal), localized (state-property), measured (read event), recorded (persistent trace). Observation changes the observer + the phase, not the structure (reality is revealed, not created). Chain: actualization → localization → measurement → observation → recording. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np114", "Knowledge Ontology Audit", "What is knowledge inside Actualization Theory?",
            "Knowledge = STABLE, INTEGRATED, PREDICTIVE DISTINCTIONS (B = C = D), realized via stored observations (A). Chain: Difference → observation → memory → knowledge. Distinct from information (structure) and observation (event); differentiator = prediction. Remove memory → observation survives, knowledge collapses. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np115", "Understanding Ontology Audit", "What is understanding inside Actualization Theory?",
            "Understanding = the MODEL OF MODELS (D) = compression of knowledge (B) = predictive hierarchy (C) — knowledge raised to self-reference (knows WHY it predicts). A (more knowledge) REFUTED. Separator = compression + self-reference. Understanding generalizes more effectively. Chain: data → information → observation → memory → knowledge → understanding. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np116", "Meaning Ontology Audit", "What is meaning inside Actualization Theory?",
            "Meaning = VALUE-WEIGHTED PREDICTION = the relation between models (C = D) — the SIGNIFICANCE of a distinction for an observer (how much it matters for its future actualizations). Distinct from information (structure), knowledge (prediction), understanding (model of models); differentiator = significance. Remove context or prediction → meaning collapses. Minimum condition = significance. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np117", "Purpose Ontology Audit", "What is purpose inside Actualization Theory?",
            "Purpose = the SELECTED PREDICTION (B = C = D) — meaning PLUS selection (the aim). Meaning weighs; purpose aims (directs future actualization). A (weighted meaning) necessary not sufficient. Differentiator from meaning = selection. Remove value or prediction → purpose collapses. Chain: observer → meaning → purpose → action. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np118", "Choice Ontology Audit", "What is choice inside Actualization Theory?",
            "Choice = the ACT OF SELECTING (A = C = D) — constrained actualization = purpose becoming action. Distinct from purpose (aim) and action (result). B (value maximization) partial. Minimum condition = alternatives + values + purpose (separates choice from raw actualization). Chain: meaning → purpose → choice → action. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np119", "Freedom Ontology Audit", "What is freedom inside Actualization Theory?",
            "Freedom = the CAPACITY TO SELECT AMONG MEANINGS (C = D) — actualization under incomplete determination (the value-weighted, purpose-directed middle path between determinism and randomness). A partial; B (unconstrained) REFUTED. Compatible by identity with Born selection/actualization/Difference conservation (freedom selects, not creates). EMERGENT, on the BOUNDARY tick.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np120", "Responsibility Ontology Audit", "What is responsibility inside Actualization Theory?",
            "Responsibility = FREEDOM + PERSISTENCE — the ownership of action and consequences by the same persistent chooser (A = B = C = D). Differentiator from freedom = continuity. Forced/random actions bear no responsibility. Freedom survives without persistence; responsibility collapses. Chain: meaning → purpose → choice → freedom → responsibility. DERIVED/EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np121", "Identity Ontology Audit", "What is identity inside Actualization Theory?",
            "Identity = the SAMENESS of a persistent Difference structure (A = D) — structural persistence, not content persistence. The observer's content changes every tick, but its structure persists (NP_104). B (memory) and C (actualization pattern) PARTIAL. Minimum condition = persistence. Distinct from existence (being) and responsibility (owning). DERIVED/EMERGENT/PARTIAL.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np122", "Consciousness Ontology Audit", "What is consciousness inside Actualization Theory?",
            "Consciousness = the RECURSIVE SELF-MODEL (D) = SELF-OBSERVATION (B) — the observer's model turned on itself, so it observes its own observing. A (observation) necessary not sufficient; C (integrated observation) partial. Minimum condition = the self-model. NOT a new primitive (the same ontology recursive). Chain: observation → knowledge → understanding → identity → consciousness. EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np123", "Truth Ontology Audit", "What is truth inside Actualization Theory?",
            "Truth = CORRESPONDENCE (C = B = D) — the model's distinctions map to the world's persistent Difference structures. A (successful prediction) necessary not sufficient (useful ≠ true). Distinct from knowledge (prediction), understanding (modeling), meaning (valuing). Minimum condition = correspondence (stable, consistent). EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np124", "Wisdom Ontology Audit", "What is wisdom inside Actualization Theory?",
            "Wisdom = RESPONSIBLE ACTION GUIDED BY UNDERSTANDING (D) — the integration of all prior levels into right action. B (balanced understanding) and C (truth-weighted purpose) PARTIAL; A (accumulated knowledge) REFUTED. Minimum condition = integration (understanding + truth + meaning + responsibility → action). Chain: knowledge → understanding → meaning → purpose → wisdom. EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np125", "Cooperation Ontology Audit", "What is cooperation inside Actualization Theory?",
            "Cooperation = ALIGNED MEANINGS = SHARED PURPOSES = INTEGRATED SELF-MODELS (A = B = C) — multiple observers' aligned values and aims, mutually modeled. D (optimization) PARTIAL. Distinct from individual purpose (one aim → many aligned aims). Freedom retained; responsibility distributed. Social extension: consciousness = model of self; cooperation = model of other + alignment. EMERGENT.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Foundations, TheoryClassification.Emergent,
            ["difference"]),
        new("np126", "Resonance Sailing Audit", "Can a bound structure couple to an existing propagating resonance and gain net momentum?",
            "YES — resonance sailing = RADIATION PRESSURE (DERIVED), NOT reactionless drive (REFUTED). A bound structure phase-locks to a propagating mode (photon/graviton/ψ wave) and gains its momentum k (absorption +k, reflection +2k). Spacecraft rides the wave as a sail rides the wind (P = 2I/c). Momentum/causality/thermodynamics conserved.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Derived,
            ["difference", "d96"]),
        new("np127", "Resonance Engineering Audit", "What useful technologies become possible if matter is fundamentally a bound resonance structure?",
            "The ontology uniquely suggests COHERENCE ENGINEERING (mode/phase/coupling manipulation). Levers A = B = C = D. Allowed: propulsion (sailing), communication (phase), sensing (spectrum), energy (coherent coupling), materials (phase-locked crystals). Rejected (conservation): reactionless drive, perpetual motion, FTL, info destruction. Unifying theme = coherence.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np128", "Coherence Resource Audit", "Is coherence a physical resource?",
            "YES — coherence = A (measurable resource) = D (independent physical capability); B (bookkeeping) and C (hidden information) PARTIAL. Coherence is DISTINCT from information/energy/entropy: it is the phase-lock's persistence (NP_100) — the ORDER those quantities presuppose. It is a DEGRADABLE resource with a fundamental trade-off coherence ↔ entropy: friction (NP_095) converts coherence (low entropy) into entropy (NP_096). Every capability (motion, binding, sailing) is coherent phase-locking, and all five technologies (communication, sensing, power, materials, propulsion) benefit from maximizing it — the most practically valuable quantity.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np129", "Coherent Matter Control Audit", "Can bound matter structures be created, modified, or dissolved through coherent phase control rather than thermal heating?",
            "YES — phase engineering OUTPERFORMS thermal processing. Binding = phase-locking (NP_100) + heat = decoherence (NP_096) means a structure is a set of phase-locked modes, so phase can build/modify/break it. Thermal melting is mode-blind (equipartition, dS = N·ln2, efficiency 1/N); coherent disruption is mode-selective (one resonant quantum into the target lock, dS = ln2, efficiency ~1) — an N× advantage (~95×) at the cost of coherence (NP_128). Conservation/2nd law HOLD (coherent is MORE reversible).",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np130", "Material Sonification & Inversion Audit", "Can a material be represented by a coherent resonance signature and can the inverse signature modify the material?",
            "YES — matter is a WRITABLE RESONANCE SCORE (material music). Binding = phase-locking (NP_100) means a material's fingerprint is its mode spectrum {(ω_i, A_i, φ_i)} — one point in a ≥2^95 identity space. Sonification is a bijective scale map to the audible band (~212.6 Hz/mode). Invertibility EXACT for the full signature, PARTIAL for frequency-only (phase lost). Coherent excitation (NP_129) writes the score — soften/reshape/disorder/re-order — at an N× (~95×) energy/entropy advantage over thermal melting.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np131", "Critical Resonance Audit", "Does every material possess a small set of critical resonance modes whose coherent excitation can drastically reduce structural rigidity?",
            "YES — a resonance MASTER KEY: a small backbone set m ≪ N of critical (load-bearing) modes carries macroscopic rigidity (binding = phase-locking, NP_100). Backbone size is material-specific: crystal/metal m = 6 (~15.8× energy advantage), granite m = 20 (~4.8×), glass m = 40 (~2.4×). Rigidity is collective (percolation): unlocking one of 6 critical modes drops R 1.00 → 0.67; the full set drives R → 0. Coherent critical-mode excitation spends m resonant quanta (m·E_bind) vs thermal N·E_bind, entropy m·ln2 ≪ N·ln2 — transient, gel-like, REVERSIBLE softening without melting.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np132", "Reversible Softening Audit", "Can coherent excitation of critical modes produce a reversible low-rigidity state without thermal melting?",
            "YES — materials can be temporarily softened and reshaped without heating. Softening and melting are DISTINCT: softening drives the m critical modes open (R → 0) while lattice order S SURVIVES (the N−m modes stay locked) — a gel-like (unjammed) solid, ΔS = m·ln2, REVERSIBLE (re-lock restores R). Melting heats all N modes (R → 0 AND S → 0) — a disordered liquid, ΔS = N·ln2, IRREVERSIBLE (re-nucleate order). Reversibility advantage N/m (~15.8× crystal/metal, ~4.8× granite, ~2.4× glass).",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np133", "Critical Mode Frequency Audit", "What physical frequency ranges contain the critical rigidity modes?",
            "The critical (backbone) modes are long-wavelength acoustic phonons at f_1 = c_s/(2L) — ~25–27.5 kHz for a 10 cm sample, ~2.5–2.75 kHz for 1 m — so they live in the AUDIO-to-ULTRASOUND band (~kHz–MHz). The Debye bond band f_D = c_s/(2a) ~ 8.3–9.2 THz is ~10^8× higher and physically distinct. Coherent softening is therefore ULTRASONIC-scale (audio for meter-scale); THz couples to bonds (heating/chemistry, NP_096), not rigidity. Excitable by piezo transducers, horns, SAW, phased arrays.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np134", "Critical Mode Discovery Audit", "How can the critical rigidity modes of an unknown material be identified?",
            "By RIGIDITY-PERTURBATION RANKING: measure the spectrum (NP_130), sweep a resonant drive across each mode, record the rigidity response R(e_i), and rank by influence I_i = |∂R/∂e_i| — the top-m modes are the master key. Signatures C (coherence sensitivity) and D (nonlinear coupling) are diagnostic; A (amplitude) is REFUTED; B (phase) is PARTIAL. Backbone influence ≈ 1/(1−p_c) vs ≈ 0 for non-backbone. Crystal/metal show a sharp gap (m = 6); glass a diffuse gap (m = 40). Minimum measurement: a single stiffness-vs-frequency sweep.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np135", "Coherent Softening Experimental Audit", "What is the simplest laboratory experiment that could falsify the critical-mode softening hypothesis?",
            "A matched-power ultrasound experiment on a 10 cm bar (aluminum f_1 = 25.5 kHz, quartz 28.75 kHz, steel 25.5 kHz). Measure baseline rigidity R_0 = f_0^2 (RUS); drive OFF-resonance (thermal control) vs ON-resonance (critical mode) at matched power; measure rigidity (frequency shift), damping (Q), ΔT. Thermal baseline ΔR/R_0 = −α·ΔT ≈ 0.015–0.045 %/K. Decisive criterion R = ΔR(f_1)/ΔR(f_off): PASS iff R ≫ 1 AND reversible AND frequency-selective; FAIL (thermal-only) iff R ≈ 1. The off-resonance drive is the built-in thermal control.",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),
        new("np136", "Variable Rigidity Bounds Audit", "What is the maximum reversible rigidity reduction physically achievable through coherent critical-mode control?",
            "Bounded only by failure modes, not the mechanism — coherent softening is a TRUE variable-rigidity technology (10–90%+ reversible). R(x) = max(0, (1−x−p_c)/(1−p_c)): ordered (crystal/metal/ceramic, m = 6) one critical mode → ~33% reduction, two → ~67%, three → ~100% (gel); granite (m = 20) ~10% steps. 1% is below the step size (thermal-scale); 10/50/90% achievable (90% near-gel, unloaded). Bounded by fracture (loaded), heating (power), decoherence (selectivity).",
            AuditStatus.Passed, new DateTime(2026, 9, 7), TheoryLayer.Physics, TheoryClassification.Emergent,
            ["difference", "d96"]),

        // ── G-program — Gravity Source, Control, Magnitude, Calibration (ResearchY-G_001…G_004) ──
        new("g001", "Gravity Source Audit", "What variable actually sources gravity in AT?",
            "SOURCE = the actualization density ρ (counting measure), and for the attractive sector its standardised deficit m = ρ̄ − ρ: "
            + "ρ is the dimensionless, LOCAL input of g = ρ^(2/d)η, R = F(ρ), a = −(1/d)∇ln ρ and ρ_{k+1} = μρ_k, and it needs NO coupling "
            + "constant (GR's source reaches curvature dimensions only through κ). CORRELATED = energy density (T00 = (ρ̄−ρ)v² is rank-identical "
            + "to the deficit; Lovelock forces the geometric tensor to G/κ; the energy reading is hosted, QG89) and spectral density (it supplies "
            + "m₀ = occ₀/Σm = 0.042105, r₀ = ln span = 1.856691 and the magnitude of G = 6.6476e-11, 0.40%, but has no position index — permuting "
            + "a profile leaves every spectral moment unchanged while the field reverses sign). REFUTED = information density (permutation-invariant "
            + "global functional; identical I = 0.106440 nats with fields −0.222222 vs +0.095238; it is the non-gravitating surplus ΩΛ = 0.6839) and "
            + "the curvature–density law R = F(ρ) used as a source (it is the OUTPUT of the law and is not injective in ρ). No reclassification.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["planck-scale", "omega-lambda", "d96"]),
        new("g002", "Density Control Audit", "Can the actualization density ρ change independently of mass-energy?",
            "YES — CONTROLLABLE, and for a structural reason: count conservation makes the total deficit vanish identically (Σm = 0 exactly, "
            + "QG194), so no rearrangement of ρ can change the total mass-energy. Spectral organisation (the same multiset reordered leaves E "
            + "exactly fixed while a monotone density and its reversal give a of opposite sign at every probe), a redistribution INSIDE degenerate "
            + "multiplets (spectrum and per-multiplet totals untouched at ΔE = 0, taking the field from EXACTLY zero to max|a| = 0.603175), "
            + "fixed-total survivor compression (ΔE = 0, L1 up to 5.1569) and the lattice choice (random is an exact zero-field null; D96³ 97.6% vs "
            + "D96 53.1% energy-free) are all CONTROLLABLE. CORRELATED: unfixed compression (E −47/−73/−86%) and rescaling (E ×3.7 while a is "
            + "exactly invariant). REFUTED: phase coherence (ρ = |ψ|² is phase-blind). The free room is exactly Σ(m_i − 1) = N − A₀ = 51 of 96 for "
            + "D96 = the D_048 latent fraction L = 0.53125 exactly.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["d96", "occupancy", "iocc"]),
        new("g003", "Gravity Magnitude Audit", "What physical gravitational change corresponds to a measured Δρ?",
            "The canonical chain g₀₀ = −ρ^(2/d) (QG197) → ΔΦ/c² = (1/d)Δln ρ (QG21/QG187) → Δa = −(c²/d)∇ln ρ (G4-O3) gives, with "
            + "Δln ρ = d·Δa_AT per cell: ΔΦ/c² = Δa_AT (a PURE NUMBER, no length scale), Δa = c²·Δa_AT/L, ΔR = ΔR_AT/L², ΔM = Δa·L²/G. "
            + "Anchor: the Earth-vs-GPS potential reproduces 45.74 μs/day (QG187: 45.7). The witnesses (Δa_AT = 0.032…0.686) give 3–69 % "
            + "potential changes and, at 15 kpc, 1.36e-5 g with ΔM = 2.157e17 M_☉. THRESHOLDS AT FIXED TOTAL ENERGY (yes to all three): "
            + "1e-12 g for L < 9.54 Gpc, 1e-9 g for L < 9.54 Mpc, 1e-6 g for L < 9.54 kpc; the observable-universe radius (~14.3 Gpc) exceeds "
            + "every 1e-12 g window. Detectability: 1e-12 g = 0.0941 g†, 1e-9 g = 94.1 g†, 1e-6 g = 9.41e4 g†. Falsifiable requirement: the "
            + "observed galactic field corresponds to Δln ρ = 1.6102e-6 over 15 kpc, so any realised reconfiguration must be suppressed by ≥ 3.746e5.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g001", "g002", "planck-scale"]),
        new("g004", "Gravity Calibration Audit", "Can a_AT be calibrated to measured gravity?",
            "CALIBRATED at three of four scales with NO free parameters (nothing fitted to the data it is compared with): Earth surface "
            + "a_pred/a_obs = 0.99600 (GM_⊕/R_⊕² = 9.820250 vs 9.780965 m/s²; the entire residual is the derived-G offset "
            + "G_AT/G_CODATA = 0.9959996, 0.40%), Sun–Earth 0.99600 (plus the ψ-sector perihelion +42.98 ″/century and PPN γ = β = +1), and the "
            + "galaxy RAR 0.86850 with ZERO fitted parameters (g† = cH₀/(2π) = 1.04220e-10 m/s²; 0.9226 against the project's combined "
            + "a₀/cH₀ = 0.1725). CORRELATED: the RAR interpolating function (the AT-native statement is the α = 0 log deficit, semi-natural). "
            + "REFUTED: the cluster modified-gravity channel — Coma gives a_pred/a_obs = 0.51759 (1.93× short; MOND 0.610), the project's own "
            + "X063 finding that AT modified gravity is insufficient at cluster scale, so AT must use the deficit-as-mass channel (≈ ΛCDM, ~85 % "
            + "dark, fraction not derived) — and a uniform cosmic AT gradient (104× above the ephemeris bound). CRITICAL: 1e-6 g is a "
            + "counterfactual (it needs Δln ρ = 0.15151 over 15 kpc); the realised field is g† = 1.06e-11 g, 9.41e4× below, and locally a "
            + "point-like deficit gives exactly a = −G_AT·M/r² with no anomalous term (G4-ME22).",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Partial,
            ["g001", "g002", "g003", "planck-scale", "omega-matter"]),
        new("g005", "Control Realizability Audit", "Why are the large G_003 gravity-control modes not realised?",
            "SUPPRESSED, not forbidden. The large modes conserve the count exactly (Σρ = 1, deficit mass Σ(1/N − ρ) = 0, QG194), break no symmetry (the D_047 lattice invariants — "
            + "A₀ = 45, lock release 0.80231 nats — are exact and untouched: the witness tilt leaves every within-multiplet block sum bit-identical, L1 = 0), and occupy genuinely free "
            + "directions (G_002: 51 of 96). STABILITY: the uniform counting measure is the exact fixed point of the canonical diffusion (< 1e-15) while a witness tilt is OFF-ATTRACTOR and "
            + "contracts ~34× in 200 steps (std 0.00815358 → 0.000241, ratio 0.0296) with the entropy rising 4.291783 → 4.564079 toward ln 96 = 4.564348 and the total conserved to 12 dp. "
            + "ENTROPY COST (the key negative): the Boltzmann channel is CAPPED — max ΔS = ln 96 = 4.564348 gives only 1/96 = 0.010417, i.e. 3.6e7× SHORT of the required 3.746e5, and the "
            + "witness tilt costs only ΔS = 0.272565 (a factor 1.31). THE MECHANISM IS POISSON COUNTING: AT's mandatory δ = 1/√⟨N⟩ (QG15/QG228/QG231) read forward from the observed contrast "
            + "1.6102e-6 gives ⟨N⟩ = 3.8569e11, so the observed field is TYPICAL (P = 0.6065), the 10 %/1 % cuts are 3.4554e-6/4.8867e-6, the required 3.746e5 suppression is reached at "
            + "8.1577e-6 (5.07× observed), and the witnesses cost −ln P = 1.99e8 (0.032121) … 7.02e10 (0.603175) — the canonical 0.15151 witness having probability 10^−1.9e9. "
            + "DYNAMICAL ACCESSIBILITY: branching continuity ρ_(k+1) = μρ_k uses the SAME μ for every cell and the metric inherits it conformally (g_(k+1) = μ^(2/d)g_k, residual < 1e-15), "
            + "so the flow scales the density and never moves occupancy — a uniform rescaling is a gauge transformation (a(λρ) = a(ρ) to < 1e-6 at λ = 10⁶) — and the attractor erases "
            + "initial arrangement data (basin ≥ 0.9); the only route to a large mode is an external drive (NP_171 gate g_c = 1.607, K ≥ 10.29 ω₁, f(g=1) = 0 — IMPORTED). VERDICTS: "
            + "ACCESSIBLE = the attractor state, the phase directions and every fluctuation Δ ≤ 4.8867e-6 including the observed galactic field; SUPPRESSED = all five G_003 witnesses; "
            + "FORBIDDEN = Σρ ≠ 1 / dM ≠ 0, a changed A₀ or mirror pairing without a symmetry-breaking agent, a cell above the Planck floor 1/l_P³ = 2.3687e104 m⁻³, or a contrast above "
            + "ln 96. CONSEQUENCE: G_004's CALIBRATED verdict is explained dynamically — the observable gravity source is the Poisson-natural part of the counting measure and the huge "
            + "G_003 modes are an empty tail. No reclassification (D_040 untouched); no canonical claim changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g001", "g002", "g003", "g004", "planck-scale"]),
        new("g006", "Suppression Mechanism Audit", "What term suppresses large-density rearrangements, and can the 34x be derived?",
            "THE RELAXATION (COARSE-GRAINING) OPERATOR — a LINEAR LOW-PASS FILTER on the eigenspace-occupancy index with the exact spectrum "
            + "μ_k = 1 − 2d(1 − cos(πk/N)), d = 0.2, N = 96. Every Neumann cosine mode is an EXACT eigenvector (one step multiplies it by a constant, "
            + "verified < 1e-12 for k = 1, 5, 48, 95): μ₁ = 0.999785834991 (1/e after 4669 steps, survives 200 steps at 0.958067), μ₄₈ = 0.6 exactly, "
            + "μ₉₅ = 0.200214165009 (1.99e-140 after 200 steps) — a 10^140 rate spread. CLOSED FORM: r(m) = sqrt(Σ_{k≥1} w_k²μ_k^{2m}/Σ_{k≥1} w_k²), "
            + "w = DCT-II(ρ), k = 0 excluded — reproduces direct iteration to < 1e-9 at EVERY horizon tested (m = 1 … 50 000). THE 34× DERIVED: "
            + "1/r(200) = 33.78 = exp(200 × 0.0175991) = exp(3.5198 nats); the ladder is 2.27 (m = 1), 6.15 (10), 16.79 (50), 25.16 (100), 33.78 (200), "
            + "48.78 (500), 65.13 (1000), 162.10 (5000), 2.49e6 (50 000) — DERIVED as a law, EMERGENT as a number. EXPONENTIAL, NOT A POWER LAW: over "
            + "1…200 a power law fits the aggregate BETTER (R² 0.9945 with α = 0.5612 versus 0.7579 for one exponential; the window rate 8.0787e-3 is 37.7× "
            + "the true rate) — but over m = 5000…50 000 the envelope is a SINGLE exponential with slope −2.1418813131e-4 against ln μ₁ = −2.1418794605e-4 "
            + "(difference 1.85e-10, R² = 1.0), and the instantaneous rate CONVERGES to the geometric floor 2.1419e-4 instead of decaying to zero: power-law "
            + "decay as the LAW is REFUTED, its finite-window appearance EMERGENT. ENTROPY DRIVEN REFUTED: the operator is EXACTLY linear "
            + "(|D(ax+by) − aDx − bDy| = 3.47e-18) so it cannot read the entropy, and H + (N/2)·E → ln 96 with residual 9.7e-7 at m = 200 — entropy is "
            + "DOWNSTREAM of the Dirichlet-energy decay. BRANCHING DRIVEN REFUTED: ρ_(k+1) = μρ_k multiplies every cell by the same μ (a(λρ) = a(ρ) to "
            + "5.56e-11 at λ = 10⁶, both static at criticality, count conserved), the basin is 1 at every size (universal across size), and the tilt's field "
            + "(max|a| = 0.6032) is removed only by relaxation (→ 1.8746e-3 = 322×). CASES at m = 200: D96 33.78; D96³ 7.33 (884 736 modes, A₀ = 20 812, "
            + "free room 863 924); Random = witness class EMPTY (A₀ = 96, every multiplicity 1, free room 0), extremal alternation 55.13. STRUCTURAL: μ_k "
            + "depends on N ONLY — D96 and Random share the identical operator and the cube's μ₁ = 0.99999999999748 never decays — so the mechanism is "
            + "ARRANGEMENT-selective, not lattice-selective. CONSEQUENCE: this closes G_005's open mechanism — the free directions are within-multiplet "
            + "(high-k) and are erased fastest, while the smooth scale-free deficit behind the observed galactic field sits on the slow mode and survives "
            + "(μ₁^200 = 0.958). No reclassification (D_040 untouched); no canonical claim changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g002", "g003", "g005"]),
        new("g007", "Suppression Origin Audit", "Is DiffuseStep derived or imported? And does time have anything to do with the suppression?",
            "DERIVED FORM, DERIVED RANGE, BOUNDARY VALUES — and TIME REFUTED. TRACE: Difference → counting measure (Σρ = 1, QG194); Actualization → ρ_(k+1) = μρ_k, "
            + "count-conserving and ARRANGEMENT-NEUTRAL (|Δa| at 10⁶ρ = 0.00e+00); ρ evolution → the per-octave increments' coarse-graining conserves the total and is "
            + "EXACTLY RG-invariant (CoarseGrainedAlpha(α) = α to 1e-12, α = 0…2.5); DiffuseStep → the Euler step of the Laplacian flow on the occupancy index "
            + "(tridiagonal support 3, symmetric to 1e-15, rows summing to 1, semigroup to 1e-15). UNIQUENESS: locality + constant coefficients + symmetry + row-sum "
            + "conservation leave exactly W(b) = b·left + (1−2b)·a + b·right — one free scalar (W(0.2) ≡ DiffuseStep) — and isotropy is FORCED, since an anisotropic weight "
            + "leaks at reflecting boundaries (Σρ − 1 = (l−r)(a_0 − a_{N−1})) and the canonical chain has no antisymmetric coupling (NP_174). ADMISSIBLE RANGE DERIVED "
            + "FROM ρ ≥ 0: the update is a convex combination iff 0 ≤ d ≤ ½ (min ρ = +1.04e-2 at d = 0.2 and ½, −8.96e-2 at d = 0.6), with |μ_k| ≤ 1 and μ₉₅ = −0.999465 "
            + "at d = ½ (oscillation, no decay); selectivity |μ₉₅|/|μ₁| is |1−4d|-like: 0.800, 0.600, 0.200, 0.000268 (d = 0.25, maximal), 0.200, 0.600, 1.000000 (d = ½, "
            + "flat). SENSITIVITY IN THE RATE: factors at m = 200 are 16.70 / 25.12 / 33.78 / 36.74 / 39.43 / 44.34 / 2.01 (d = 0.05…0.50) but at fixed T = m·d = 40 are "
            + "33.75 / 33.76 / 33.76 / 33.78 / 33.81 — 0.17 % across a 20× range of d — so (d = 0.2, m = 200) is a BOUNDARY representative of T = 40. REPLACEMENTS: the "
            + "nearest-neighbour average IS d = ½ (verified to 1e-15) and collapses the witness factor to 2.01 (|μ₉₅|²⁰⁰ = 0.898); the spectral cutoff (k_c = 48) is NON-LOCAL "
            + "(delta → 96 cells in one step vs 3) and NON-POSITIVE (min ρ = −4.52e-2); the biharmonic is a 5-point next-nearest stencil, non-positive (min ρ = −1.46e-2) and "
            + "unstable beyond κ = 1/16 (−0.5991 at κ = 0.1), and weaker anyway (7.15); the identity gives 1.00 and contradicts G_004's ≥ 3.746e5. WITNESS DICHOTOMY: 32.4 "
            + "(DiffuseStep, HOLDS), 1.81 (d = ½, FAILS), 7.1 (biharmonic), 1.0 (identity) — above 8 for every 0 < d < ½. TIME REFUTED: the branching flow is diagonal on the "
            + "arrangement (support 1 vs 3), leaves the normalised profile and field exactly invariant for any μ over any duration (|Δa| at 2¹⁰⁰⁰ρ = 0.00e+00), the factor "
            + "carries no rate (scale-free to 2.34e-13) and density/metric are static at criticality — m is a COARSE-GRAINING HORIZON (≈729 steps to reach 3.746e5), not a "
            + "duration. No reclassification (D_040 untouched; G_006's 'EMERGENT 34' sharpened to BOUNDARY at fixed T = m·d); no canonical claim changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g002", "g005", "g006"]),
        new("g008", "Controlled Suppression Audit", "Can any allowed configuration maintain a high-Delta-rho state against DiffuseStep suppression?",
            "NOTHING PERSISTS ON ITS OWN. ALLOWED = Σρ = 1 (QG194), ρ ≥ 0, no symmetry breaking (D_047), no non-reciprocal coupling (NP_174). STATIONARY: μ₀ = 1 is the UNIQUE unit "
            + "eigenvalue (ker(I − W) = span(uniform)), so the only undriven stationary profile is uniform; lifetimes τₖ = −1/ln|μₖ| = 4668.80 (k = 1), 186.67 (5), 32.34 (12), 8.03 (24), "
            + "1.96 (48), 0.87 (72), 0.622 (95) steps, with amplitudes after 200 steps from 0.9581 down to 1.99e-140 ⇒ undriven high-k SUPPRESSED, smooth METASTABLE (the observed galactic "
            + "situation). DRIVEN: ρ ← Wρ + s with a mode-matched drive s = c·v_k has the EXACT steady state c·v_k/(1 − μ_k) (iterated 0.008535533906 vs analytic 0.008535533906, 1.2e-15) and "
            + "requires Σs = 0 exactly; the witness-driven state is allowed (ρ_min = 0.0025 > 0, Σρ = 1, reproduces the tilt to 1.9e-15) ⇒ STABLE, with gains 1/(1 − μ_k) from 4669.30 (k = 1) "
            + "to 1.2503 (k = 95). THE PRICE: per unit peak-to-peak contrast the witness needs 10.247 vs 6.546e-3 (1566×), per unit L1 contrast 0.730125 vs 1.364e-4 (5354×), pure-mode ratio "
            + "(1 − μ₉₅)/(1 − μ₁) = 3734.4 — the highest mode must be injected at 0.7998 of its amplitude EVERY STEP (L1 = 0.4868 ≈ 49 % of the count) and the lowest only at 2.14e-4: the witness "
            + "is RE-CREATED, not maintained. MODE MATCHING: ρ* is a filtered copy of the drive, so a smooth-only drive holds exactly zero high-k content (1e-12) while the witness is 78.2 % "
            + "high-k. PERIODIC: sup_ω |H_k| = 1/|1 − e^{iω}μ_k| equals the DC gain for every k at ω = 0 (4669.2968 / 187.1724 / 8.5355 / 2.5000 / 1.2503), Nyquist strictly worse, no amplifying "
            + "band ⇒ SUPPRESSED. BOUNDARY: a mean-zero edge dipole holds a SMOOTH ramp (high-k share 1.42e-6, k = 1 share 0.9240) with gain 120.0 per unit L1 drive, capped by ρ ≥ 0 at a "
            + "contrast of 0.0309 (3.09 % of the count) ⇒ STABLE smooth / SUPPRESSED witness. GOAL: no gravity-control state persists without a mode-matched structured external agent, which the "
            + "canonical chain does not supply. Classification: DERIVED = the kernel statement, the exact driven fixed point, the gain/lifetime/drive-power laws and mode matching; BOUNDARY = the "
            + "canonical d = 0.2; SUPPRESSED = undriven high-k, periodic forcing, boundary-only and unmode-matched drives. G_005's verdict sharpened from 'not realised' to 'not maintainable'. No "
            + "reclassification (D_040 untouched); no canonical claim changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g002", "g005", "g006", "g007"]),
        new("g009", "Clock Rate Audit", "Does the actualization density rho change clock rates?",
            "dτ/dt = ρ^(1/d) FROM g₀₀ = −ρ^(2/d), whose first-order form is the canonical redshift law (1/d)Δlnρ = ΔΦ/c². CASE 1 EARTH SURFACE: ΔΦ/c² = −6.9613e-10 and the AT rate deviation "
            + "−6.9613e-10 equal the GR value −GM/(Rc²) to double precision ⇒ −60.145 μs/day (measured −60.15). CASE 2 GPS ORBIT: gravitational +5.2940e-10 ⇒ +45.740 μs/day (QG187: 45.7), with AT's "
            + "own input Δlnρ = 1.5882e-9 (G_003's 1.588e-9); the imported SR term −8.3365e-11 ⇒ −7.203 μs/day gives TOTAL +38.537 vs the observed 38.6 (0.9984) — CORRELATED, since the kinematic half "
            + "is not AT content. CASE 3 GALACTIC FIELD (NEW CROSS-CHECK): the G_003 ambient contrast 1.6102e-6 gives Δlnρ/d = 5.367333e-7 = 46.374 ms/day versus the kinematic v²/c² = 5.385226e-7 for "
            + "220 km/s — ratio 0.99668 (−0.33 %), equivalent v = 219.63 km/s, exact match needing Δlnρ = 1.615568e-6, detectability 5.367e11× a 1e-18 clock. CASE 4 G_002 REDISTRIBUTIONS at FIXED total "
            + "mass-energy (Σρ = 1, Σ(ρ̄−ρ) = 0 exactly): rate changes 0.228571 / 0.201058 / 0.111111 / 0.092199 / 0.010707 = 19 748.6 / 17 371.4 / 9 600.0 / 7 966.0 / 925.1 s/day and 2.29e17 / "
            + "2.01e17 / 1.11e17 / 9.22e16 / 1.07e16 × the clock floor; the REALISED band (G_005 ceiling 4.8867e-6) tops out at 1.629e-6 = 0.1407 s/day (3.03× the observed 5.367e-7 = 0.0464 s/day), and "
            + "the witnesses are 3.746e5× the observed level. AT vs GR: AT = exp(x) = 1 + x + x²/2 against GR = √(1+2x) = 1 + x − x²/2 (coefficients +0.500001 / −0.499995 at x = 1e-5, difference x² = "
            + "1.0000e-10): first order IDENTICAL, second order OPPOSITE SIGN (AT clocks run slightly fast), in situ 4.846e-19 (Earth) and 2.803e-19 (GPS) — 2–4× BELOW the 1e-18 optical-clock floor. "
            + "The derived G at 0.40 % is a common factor that cancels in any rate ratio; scale invariance is exact (ρ → λρ moves every clock by λ^(1/d) with zero RELATIVE change) and the phase directions "
            + "change nothing. CRITICAL QUESTION: YES in the theory (Σm = 0 yet up to 22.86 %, 2.3e17× the clock floor — the rate follows the ARRANGEMENT, and the degeneracy freedom needs no new mass) but "
            + "NO physically (G_005 caps realised contrasts at 4.8867e-6; G_008 gives a 0.622-step lifetime and 0.7998 injection per step), and what IS realised (46.4 ms/day) is exactly the potential depth "
            + "GR predicts from the rotation curve to 0.33 % ⇒ no NEW clock effect; locally AT = GR with the derived G. VERDICTS: DERIVED (the law, the identity, the Earth/GPS magnitudes, the galactic "
            + "cross-check, the redistribution rates) · CORRELATED (the GPS total via the imported SR term; the ±½ second-order coefficient) · REFUTED (a realised sustained clock effect from a ρ "
            + "rearrangement; the phase directions; a uniform rescaling as a relative effect). No reclassification (D_040 untouched); no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g002", "g003", "g004", "g005", "g008"]),
        new("g010", "Time Control Feasibility Audit", "What sustained clock shift can exist under allowed driving?",
            "Inverts dτ/dt = ρ^(1/d) (Δlnρ = 3f) and prices the drive with G_008's law ((1 − μ_k) of the amplitude per step). THE FOUR TARGETS: 1 ns/day → f = 1.157407e-14, Δlnρ = "
            + "3.472222e-14, drive/step 7.436285e-18 (k = 1) / 2.777034e-14 (k = 95), 7.11e-9 of the band, equivalent well 32.25 m/s; 1 µs/day → 1.157407e-11, 3.472222e-11, 7.436285e-15 / "
            + "2.777034e-11, 7.11e-6, 1019.91 m/s; 1 ms/day → 1.157407e-8, 3.472222e-8, 7.436285e-12 / 2.777034e-8, 7.11e-3, 32.25 km/s; 1 s/day → 1.157407e-5, 3.472222e-5, 7.436285e-9 / "
            + "2.777034e-5, 7.11 TIMES the band, 1019.91 km/s. Reference rows: the G_005 band top = 4.8867e-6 = 0.140737 s/day (382.62 km/s well) and the observed galactic field = 1.6102e-6 = "
            + "0.046373 s/day (33 % of the band, 219.63 km/s well). The equivalent well is c√f — the Earth's surface (60.145 µs/day) is 7.91 km/s, so 1 ms/day is 16.63× it. STEADY-STATE PROFILE: "
            + "a pure Neumann mode ρ = ρ̄(1 + (Δlnρ/2)v_k), Σρ = 1.0000000000, min ρ = 1.041667e-2; the excursion/ρ̄ is 1.736111e-14 … 1.736111e-5, so POSITIVITY AND COUNT NEVER BIND; the driven "
            + "recursion reproduces c·v_k/(1 − μ_k) to 1e-9 after 150 000 steps and the drive must be mode-matched to 1e-18. POWER SCALING: linear in the target (7.436e-18 → 7.436e-9); quadratic "
            + "in relative frequency (1 − μ_k ≈ d(πk/N)²: cost(2)/cost(1) = 4.00; the continuum form overestimates at high k — exact 0.4000 vs 0.4935 at k = 48); N⁻² at fixed wavelength (1024-site "
            + "113.7× cheaper); cost per unit shift d(1 − μ_k) = 6.4250e-4 (k = 1) vs 2.3994 (k = 95) → 3734.4. Accumulated drive over 200 steps at k = 95: 5.554e-12 … 5.554e-3. VERDICTS: PRACTICAL "
            + "(1 ns/day, 1 µs/day, 1 ms/day — ≤7.11e-3 of the band, drives ≤7.44e-12 smooth, ≥1.2e4× a 1e-18 clock: as NUMBERS, given a driver) · ASTROPHYSICAL ONLY (0.0464 s/day through "
            + "0.1407 s/day) · REFUTED (1 s/day spontaneously at 7.105× the band and a 1020 km/s well; and ANY sustained shift without an external mode-matched driver — branching is "
            + "arrangement-neutral, basin 1.0, undriven local lifetime 0.622 steps, Poisson cap 4.8867e-6). The clock channel is length-independent, so only the drive's POWER scales with a physical L. "
            + "No reclassification (D_040 untouched); no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Partial,
            ["g005", "g008", "g009"]),
        new("g011", "Rho Actuator Audit", "Can any physical quantity change rho?",
            "THE ACTUATOR CRITERION: q is an actuator iff it is INDEPENDENT of rho, determines rho locally, conserves the count, costs no energy and adds no primitive. NO CANDIDATE IS AN ACTUATOR. "
            + "ENERGY DENSITY (CORRELATED): E = <lambda, rho> is identical to 1e-12 (E = 12.000000000000) between the canonical measure and the tilt with every eigenspace total equal to 12 digits, while rho "
            + "moves by L1 = 0.6666667 and the field is created out of an exact zero (0 → 0.603175); E is non-injective (the fixed-E fibre contains the whole 51-dimensional degeneracy space, 94 dimensions in "
            + "total) and re-ordering the SAME multiset moves E by 1.5401766 (12.8 %); density-space reading: a 4.8:1 occupancy contrast, Delta ln rho = ln 4.8 = 1.5686159 ⇒ Delta Phi/c^2 = 0.5228720 = "
            + "45 176.1 s/day. SPECTRAL ORGANIZATION (CORRELATED): the orthonormal DCT-II is a bijection (||CC^T - I|| = 1.37e-14, round trip 4.2e-16) whose DC coefficient IS the count (Sigma rho = 1), "
            + "leaving 95 free coordinates; the witness is high-k (dominant mode k = 94, k >= 48 share 0.7965733) and the operator's mu_k carries no rho-dependence at all (mu_48 = 0.6 exactly, "
            + "1 - mu_1 = 2.141650094e-4, 1 - mu_95 = 0.799785835). DEGENERACY ENGINEERING (CORRELATED): the within-eigenspace redistribution conserves count, every eigenspace total, the energy and "
            + "A0 = 45 while creating the field 0 → 0.603175 with ||s||_1 = 0.48675 and a 33.7781x contraction. PHASE COHERENCE AND SYNCHRONIZATION (REFUTED): the ONLY candidates independent of rho, and "
            + "exactly rho-inert — L1(rho, |psi|^2) <= 2.5e-16 and |Delta a| < 1e-9 for the canonical grid theta_j = 2 pi j/N, a global shift, the mirror and the locked configuration, while |Sigma psi| runs "
            + "0.0324196281 → 9.1171821879 (a factor 281.22) and the canonical grid is the maximally INCOHERENT state (the 96th roots of unity sum to zero: Kuramoto r = 8.0788382e-17 → 1.0 on locking); the "
            + "two-mode interference term still runs 4 → 2 → 0. ATTRACTOR COMPRESSION (REFUTED as an actuator): a rho → rho map — L1 = 2.8669638 and Delta a = 0.032121 on the G_002/G_003 base profile, with a "
            + "SMOOTH difference that survives 200 steps essentially unchanged (base std contracting only 1.1207x against the witness's 33.78x). INFORMATION DENSITY (REFUTED): I_occ = KL(rho || uniform) is "
            + "EXACTLY permutation- and reversal-invariant (Delta KL = 0 to 1e-15) while rho moves by L1 = 0.6583333 and the field can GROW to |Delta a| = 1.0031746; KL(uniform) = 0, KL(tilt) = 0.2725652026 "
            + "= ln 96 - H, decreasing monotonically under the flow (0.27257 → 1.0873e-3 at 50 → 2.6946e-4 at 200). THE POSITIVE RESULT — rho IS FULLY ACTUABLE: for any target, s = (I - W) rho* is "
            + "count-neutral (Sigma s = 2.1e-17) and UNIQUE, since (I - W) is diagonal in the DCT basis with all gains finite (4669.296831218 at k = 1, 2.500000000 at k = 48, 1.250334722 at k = 95 — no "
            + "unreachable direction, no zero-gain mode); the inverse reconstructs the target to 1.9e-15 and the driven recursion converges to < 1e-12; the uniform state is the unique undriven fixed point. "
            + "Price of the witness: ||s||_1 = 0.48675 per step (49 % of the count), max|s| = 0.01866667. VERDICTS: ACTUATOR = none of the seven; the only handle is the IMPORTED source s (the missing driver of "
            + "G_008/G_010) · CORRELATED = energy density, spectral organization, degeneracy engineering · REFUTED = phase coherence, synchronization, attractor compression, information density. G_002's "
            + "CONTROLLABLE verdicts concern the OPERATIONS on rho and stand unchanged (the G_011 labels concern the QUANTITY). No reclassification (D_040 untouched); no canonical claim, value or equation "
            + "changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Partial,
            ["g001", "g002", "g008", "g010"]),
        new("g011b", "Labor Rho Audit", "Can any laboratory system implement a controlled rho profile?",
            "FIVE LABORATORY OPERATOR FAMILIES: oscillator lattice / coupled modes / D96 controls = the Neumann chain (lambda_max = 3.998929, admissible d <= 0.500134, 0.2/d_max = 0.3999, rate spread "
            + "3734.44); resonator network = a nearest-neighbour ring (lambda_max = 4.0, d <= 0.5, 0.4000, spread 934.11); graph diffusion on the D96 circulant C96(+-1..+-6) (lambda_max = 15.837372, "
            + "d <= 0.126284, 1.5837x OVER the bound at d = 0.2); hub/star control (lambda_max = 96, d <= 0.020833, 9.6000x over). The canonical d = 0.2 is admissible for degree-2 lattices only: the "
            + "occupancy index is an ORDERED CHAIN, not the D96 mutation ring. Eigenvectors: cos(pi k(i+1/2)/N) is exact for RhoDynamics.DiffuseStep (residual 2.7e-14 over k = 1..95) and cos(2 pi k j/N) for "
            + "the periodic ring step (<= 4.9e-15). THE DERIVED RANGE IS THE CFL BOUND: rho >= 0 ⇔ |1 - d lambda| <= 1 ⇔ d <= 1/2 are the SAME inequality; at d = 0.2 all 96 eigenvalues lie in "
            + "[0.200214, 1] and d = 0.6 leaves [-1, 1]. REQUIRED DRIVE (k = 1 / k = 95 per step): 1 ns/day 7.436285e-18 / 2.777034e-14; 1 us/day 7.436285e-15 / 2.777034e-11; 1 ms/day 7.436285e-12 / "
            + "2.777034e-8; 1 s/day 7.436285e-9 / 2.777034e-5; the G_002 witness class 0.48675 (49 % of the count, max|s| = 0.01866667). Kinematic room = the one-cell counting ceiling ln 96 = 4.564348 ⇒ "
            + "a 1.521449 fractional shift = 131 453 s/day at a k = 1 drive of 9.775237e-4/step. STEADY STATE: the pure Neumann mode rho = rhoBar(1 + (Delta ln rho/2) v_k) with Sigma rho = 1 (1e-12), "
            + "min rho > 0 and a log contrast of 2 atanh((Delta ln rho/2)|v_1|max) = Delta ln rho · 0.9998661; the excursion/rhoBar is at most 1.74e-5 so positivity never binds; the driven recursion "
            + "rho <- W rho + s reproduces the target to < 1e-12 after 20 000 steps at exactly (1 - mu_1) of the mode amplitude per step with Sigma s = 0. POWER: 1 mJ held in a 1 us step needs 7.4363e-15 W for "
            + "a 1 ns/day excursion. GRAVITY LADDER (measured G, M/r = f c^2/G): the Earth self-check reproduces GM_Earth/(R_Earth c^2) = 6.9613e-10 with M/r = M_Earth/R_Earth = 9.3740e17 kg/m; 1 kg at 1 m = "
            + "7.4262e-28 (1.35e9x BELOW the 1e-18 clock floor); 1 kg moved 1 m from a 1 m separation at fixed total energy = 3.7131e-28 (2.69e9x below); 1000 kg at 1 m = 7.4262e-25; the clock floor needs "
            + "M/r = 1.3466e9 kg/m (1.35 million tonnes per metre); 1 ns/day 1.5586e13; 1 ms/day 1.5586e19; the G_005 band top 2.1935e21 kg/m (2340 Earths per metre). NO METRIC COUPLING: AT's prediction "
            + "for any real mass-energy arrangement IS Newtonian (GM_Earth/R_Earth^2 = 9.820250 with the derived G at 0.99600, AT = GR to double precision), so a bench rho rearrangement produces no new effect "
            + "at any magnitude; the analogue readout (Delta ln rho/3 = 1.157407e-14 for 1 ns/day) is a voltage ratio, not the actualization density of spacetime. VERDICTS: PRACTICAL (the rho dynamics, the "
            + "drive, the steady state, the analogue readout) · ASTROPHYSICAL (every real clock/gravity readout, 1.35e6 t/m through 2.19e21 kg/m) · REFUTED (a bench-side metric effect from a rho "
            + "rearrangement). No reclassification (D_040 untouched); no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Partial,
            ["g004", "g005", "g007", "g009"]),
        new("g012", "Local Rho Actuator Audit", "Can any physically realizable local process act as a rho source?",
            "FOUR REQUIREMENTS: (1) local implementation, (2) finite drive, (3) survives DiffuseStep, (4) no imported primitive. REQUIREMENT 1: s = (I - W) rho* is a THREE-POINT stencil (perturbing rho_j moves only "
            + "s_{j-1}, s_j, s_{j+1}), count-neutral (Sigma s = 2.1e-17) and exact. REQUIREMENTS 2 AND 3: ||s||_1 = 0.48675 per step for the witness (max|s| = 0.01866667), 3.331453e-10 for the band-top profile, and the driven "
            + "recursion reproduces any target to < 1e-12 over 20 000 steps. ACTUATOR — THE INCREMENTAL LOCAL FEEDBACK: s = rho - W rho makes the closed loop the IDENTITY, so it FREEZES ANY CONFIGURATION EXACTLY (the witness tilt "
            + "to 4.336809e-18 over 5000 steps; the uniform measure and the highest mode to < 1e-15; a 1e-6 perturbation retained 100.000 %, 8.674e-19). It is MARGINAL — all 95 closed-loop eigenvalues are 1 — so it is a perfect "
            + "MEMORY with no restoring force: it holds a configuration, it cannot create one. THE THREE-POINT THEOREM: for the count-conserving family s = beta(W rho - rho) the closed-loop eigenvalues are "
            + "c_k = mu_k(1 + beta) - beta, and c_k = 1 for EVERY k only at beta = -1 (95/95 neutral against 0/95 at beta = 0, and 0.999893/0.800000/0.600107 at beta = -0.5). THE RESTORING FAMILY s = lambda(rho - rhoBar) IS "
            + "LIMITED TO THE SMOOTHEST MODE: the closed-loop spectrum is mu_k + lambda, so stability requires lambda in [-(1 + mu_95), 1 - mu_1] = [-1.200214165, 2.141650094e-4], and a fixed point carrying mode k needs "
            + "lambda = 1 - mu_k — only k = 1 fits (k = 2 needs 8.564307e-4; the k = 48 attempt at lambda = 0.4 gives mu_1 + lambda = 1.3998 > 1). k = 1 is exact to 1e-15 over 20 000 steps while k = 2 decays at 6.422657e-4 per "
            + "step (tau = 1556.99, matching (mu_2 + lambda)^5000 = 0.0402614767) and a k = 1 + k = 2 mixture resolves onto its k = 1 part (2.4e-12); above threshold (lambda = 2(1 - mu_1)) the smooth mode RUNS AWAY at "
            + "1.000214165 per step, saturating from a Poisson seed in 64 516 steps. NO LOCAL CREATION: the gain of (I - W)^-1 decreases with k (4669.2968 to 1.2503), giving HighKShare(rho*) <= 2.8666657e-7 D_high/w_1^2 "
            + "(verified for every family); compact masks hold SMOOTH profiles (block w = 2: 6.630273e-3 down to w = 64: 6.368861e-9; edge dipole 0.1888945 with max|rho*| = 4.9479167; fully staggered global source "
            + "3.281127e-4) and the best STRUCTURED +-1 mask reaches 0.6117676 (77 % of the witness's 0.7965733) but a prescribed mask is imported information; the CELLWISE TEST excludes the witness for state-dependent "
            + "generators (spread 1.583333e-3 ... 9.5e-3 within groups of equal rho against max|s| = 0.01866667) while a pure mode is exactly linear (9.3e-15). MODE INJECTION REFUTED (s = (1 - mu_k)c v_k has full support; "
            + "the witness drive is 98.3 % high-k against the profile's 79.7 %). SYNCHRONIZED OSCILLATORS REFUTED (a locally locked patch leaves rho bit-identical: L1 = 2.45e-16, max|Delta a| < 1e-9, coherent sum 0.7537609). "
            + "DRIVEN D96 LATTICE and NON-EQUILIBRIUM STEADY STATES CORRELATED (the lattice is the MEDIUM — undriven attractor uniform, L1 0.6666667 to 0.0201006 in 200 steps, response ratios 2.5, 5.0, 10.0, 20.0, 40.0, "
            + "120.0 — and a NESS is *defined* by s = (I - W) rho*, so its taxonomy IS the source taxonomy). READOUTS: Delta tau/tau = Delta ln rho/3 — band top 0.140737 s/day, a 3:1 contrast 31 640.03 s/day, 10:1 "
            + "66 314.45 s/day, all inside G_005's SUPPRESSED band. VERDICTS: ACTUATOR = the incremental local feedback (any profile, marginal/memory) · CORRELATED = the driven lattice and the NESS framing · REFUTED = mode "
            + "injection, synchronized oscillators, every restoring gain beyond k = 1, and local creation of the witness class. REFINEMENT (not a reclassification): G_010's 'the canonical chain supplies no driver' stands — "
            + "an ENGINEERED local feedback realises one, marginally and only as a memory; G_011 labels the QUANTITY, G_012 the LOCAL GENERATOR. No reclassification (D_040 untouched); no canonical claim, value or equation "
            + "changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Partial,
            ["g005", "g008", "g010", "g011", "g011b"]),
        new("g013", "Physical Actuator Audit", "What physical process can realize s = (I - W) rho* locally?",
            "THE STENCIL IN PHYSICAL FORM: s_i = -d(rho_{i-1} - 2 rho_i + rho_{i+1}) — a NEGATIVE LAPLACIAN, anti-diffusion of strength d = 0.2 on the nearest-neighbour chain (verified to 3.5e-18) — and a BALANCED "
            + "PUMP-AND-DRAIN: exactly 50.0000 % of ||s||_1 = 0.48675 is injection and 50.0000 % extraction (max|s| = 0.01866667; count-neutral; three-point local). PHYSICAL: (a) the FEEDBACK CONTROLLER — sense rho_i and its "
            + "neighbours, compute (I - W)rho, actuate; exact stencil, maintains rho* to < 1e-15 over 5000 steps; (b) ACTIVE DIFFUSION CANCELLATION — the operator IS a nearest-neighbour NEGATIVE CONDUCTANCE, so an NIC "
            + "realizes it element by element, half the elements sourcing and half sinking. ANALOGUE: (c) the OSCILLATOR LATTICE with a NODE-WISE gain — a flat gain is mode-independent, residual error |gamma - (1 - mu_k)| "
            + "with minimax 0.3997858349905463 at gamma = 0.4 against a required spread of 3734.437 (a 1866.7x relative error on the smoothest mode); (d) COUPLED RESONATORS — band-limited: the witness's spectral share "
            + "below the cut bounds what it holds, k <= 1/4/8/16/24/48 giving 3.240559e-4 / 1.479430e-3 / 4.833883e-3 / 3.626161e-2 / 8.417047e-2 / 2.035517e-1, i.e. a 16-mode bank leaves 96.4 % uncompensated. REFUTED: "
            + "(e) PUMP/LOSS NETWORKS — the required source is balanced, but a pump/loss balance is a SCALAR condition whose fixed points are the single Neumann modes (G_012) and whose imbalance grows at the fastest mode's "
            + "rate (a factor e in 125 steps at 1 %). THE BINDING CONSTRAINT IS EXACTNESS: with residual loop gain (1 + eps) the closed loop is I + eps(I - W), so mode k evolves at 1 + eps(1 - mu_k) — verified step by step "
            + "(eps = 1e-5 gives 1.000007998 per step on v_95 and the n-step law holds). Hold times tau = 1/(eps(1 - mu_k)): for eps = 1e-6/1e-5/1e-4/1e-3/1e-2 the fastest mode lasts 1.25e6 / 125 033 / 12 503 / 1250 / 125 "
            + "steps against 5.0e9 / 4.7e8 / 4.7e7 / 4.669e6 / 4.669e5 for the smoothest — a 0.1 % tolerance holds the SMOOTH class for 4.67e6 steps but the witness for only 1250 (a 3734x split). Sensor/actuation "
            + "resolution: q <= 1.0416667e-10 count units for 1 % of rhoBar over 1e6 steps. A ONE-STEP CONTROL DELAY IS TOLERABLE: roots {1, mu_k - 1} in [-0.7997858350, 0] — stable, with an alternating transient — and a "
            + "1e-3 perturbation is RETAINED (0.9986e-3 ... 0.9995e-3 after 2000 steps), so the loop stays a marginal memory. POWER IS NOT BINDING: the diffusion produces dH = 0.2170247 nats per step for the witness, so "
            + "Landauer costs k_B T dH = 8.6295e-20 J per lattice per step (8.63e-14 W at a 1 us step) and the free-energy rate sum_i s_i ln(rho_i/rhoBar) is 1.5192e-19 J (1.52e-13 W) — ten orders below the ~1 mW quiescent "
            + "draw of any electronic controller; the BAND-TOP SMOOTH profile costs dH = 0.0 to double precision (H is stationary in the k = 1 direction), i.e. it is thermodynamically free. VERDICTS: PHYSICAL = feedback "
            + "controller and active diffusion cancellation · ANALOGUE = oscillator lattice (node-wise gain) and coupled resonators (band-limited) · REFUTED = pump/loss networks. The PHYSICAL devices inherit G_012's marginal "
            + "character: they hold a configuration, they do not create one. No reclassification (D_040 untouched); no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Partial,
            ["g006", "g007", "g008", "g011b", "g012"]),
        new("g014", "Physical Rho Mapping Audit", "What measurable laboratory quantity corresponds to rho?",
            "THE MAPPING CRITERION: q = F(rho) is a rho analogue iff it is (A1) positive cellwise, (A2) normalised (Sigma q = 1), (A3) AFFINE — kappa = d ln q/d ln rho constant, because both the relaxation W and the "
            + "G_013 stencil are LINEAR — and (A4) CELLWISE (so it carries the gradient). THE AFFINE TEST IS DECISIVE: for q = rho^kappa the flow error ||F^-1 W F(rho) - W rho|| and the actuator error "
            + "||(I - W)F(rho) - F'(rho)(I - W)rho|| are BOTH EXACTLY ZERO at kappa = 1 and nonzero otherwise (kappa = 0.5: actuator 3.229213; kappa = 2: flow 1.047221e-2, actuator 4.668155e-2; kappa = 3: flow "
            + "1.724505e-2, actuator 4.822371e-3), with the recovered clock factor exactly 1/kappa. PHYSICAL: (1) the PROBABILITY DENSITY — q = |psi|^2 with psi_j = sqrt(rho_j) e^{i theta_j} (QG220) IS rho "
            + "(L1 = 2.484991379e-16), so kappa = 1 exactly and the map is the identity: the source law a = -(1/d) grad ln q (max|a| = 0.6031746), the clock law Delta ln q/d, the G_013 stencil (I - W)q and the "
            + "G_007 suppression (33.7781483) all hold identically, and it is the only candidate carrying the psi-sector; (2) the OCCUPATION DENSITY — the counting face, also kappa = 1, whose OWN SHOT NOISE IS the theory's Poisson law: "
            + "<N> = 1.6102e-6^-2 = 3.856917553651e11 counts per cell gives delta = 1/sqrt(<N>) = 1.610200e-6 (exactly the observed galactic contrast), a 1 % ceiling of 4.886722e-6 (exactly G_005's accessible band) "
            + "and P(observed) = 0.6065. CORRELATED: (3) the MODE POPULATION — the DCT of the reversal multiplies mode k by (-1)^k, so the POWER SPECTRUM IS INVARIANT (max |Delta|w_k|| = 4.1598669e-15, total spectral "
            + "energy ratio 1.0000000000000018) while the arrangements are physically opposite (max|Delta a| = 0.8864864874, acceleration correlation about 0.10): it reproduces the suppression and gives G_013's modal-gain view but is "
            + "nonlocal; (4) the ENERGY DENSITY — a SPECTRALLY WEIGHTED re-expression: the D96 weight has a (numerically) ZERO mode, so eps = lambda rho vanishes there and ln eps is undefined, and with a positive weight "
            + "(1 + lambda/lambda_max in [1,2]) the flow commutation defect is 3.7873408e-4 against a reference 2.0916667e-2 (1.81 %) with the implied clock factor spread over [0.5689248, 1.1378497]; non-injective (fibre 94); "
            + "G_001's verdict stands. REFUTED: (5) the INFORMATION DENSITY — a GLOBAL, permutation-invariant functional (Delta KL = 0 exactly under a 37-cell roll while L1 = 0.6583333 and the field moves 1.0031746), zero at the "
            + "uniform measure; (6) the COHERENCE DENSITY — the psi-SECTOR: a phase change moves the coherent sum by 281.2241449x at L1(rho, |psi|^2) = 2.5e-16 and |Delta a| < 1e-9, exactly rho-inert. CRITICAL ANSWER: the first "
            + "experimentally measurable rho analogue is the DIAGONAL OCCUPATION (PROBABILITY) DENSITY q_i (site-resolved imaging, photon counting, mode-resolved population; kappa = 1 exactly); the readable contrast floor is "
            + "1/sqrt(<N>) = 1.6102e-6 = 46.374 ms/day of clock depth (G_009's galactic cross-check), and the residual gap is the IDENTIFICATION premise — the metric coupling G_011b showed is not borrowed. No reclassification "
            + "(G_001's labels unchanged); D_040 untouched; no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g001", "g003", "g005", "g009", "g011", "g011b", "g013"]),
        new("g015", "Rho To Metric Audit", "Can a laboratory q profile produce any measurable metric effect?",
            "The audit only becomes well-posed once G_014's identity map is used: the ANALOGUE channel (the density CONTRAST itself, Delta tau/tau = Delta ln q/d) is a measurable RATIO needing no mass, while the METRIC channel (a REAL time dilation) has " +
            "mass-energy as its only laboratory handle (G_011b) — m = E/c^2, Delta Phi/c^2 = G m/(R c^2), a = G m/R^2 — for which AT predicts exactly the Newtonian field (G_004). THE CLOCK LADDER (f => Delta ln rho = 3f => M/r = f c^2/G): the 1e-18 " +
            "MEASUREMENT FLOOR needs M/r = 1.3466e9 kg/m = 13 466 TONNES WITHIN A CENTIMETRE (1.3466e7 kg); 1 ns/day 1.5586e13; the OBSERVED GALACTIC field (5.3673e-7) 7.2276e20; the G_005 band top (1.6289e-6) 2.1935e21 kg/m; the Earth self-check " +
            "reproduces G_004's 9.3740e17 kg/m. THE FOUR LABORATORY CASES (Delta Phi/c^2, m = E/c^2): PHOTON OCCUPATION (1 J = 5.034116567542709e18 photons at 1 um in 5 mm) gives m = 1.112650e-17 kg, 1.6525e-42, a = 2.9705e-23 m/s^2, 1.65e-24x the " +
            "floor; CAVITY MODES (1 kJ in 6 cm, SRF, Q = 1e10) 1.112650e-14 kg, 1.3771e-40, 2.0628e-22, 1.38e-22x; RESONATOR LATTICE (1 mJ on 1 cm) 1.112650e-20 kg, 8.2627e-46, 7.4262e-27, 8.26e-28x; OSCILLATOR LATTICE (1 nJ on 1 cm) 1.112650e-26 kg, " +
            "8.2627e-52, 7.4262e-33, 8.26e-34x — ordering cavity modes > photon occupation > resonator lattice > oscillator lattice, ALL REFUTED, the best case twenty-two orders below the floor. THE STRONGEST POSSIBLE LABORATORY CASE (the best energy " +
            "densities in a 1 m ball, V = 4.189 m^3): chemical 1e9 -> 3.4611e-35 (3.46e-17x), capacitor 1e12 -> 3.4611e-32 (3.46e-14x), magnetic 3e13 -> 1.0383e-30 (1.04e-12x), NUCLEAR SCALE 1e18 -> 3.4611e-26 (3.46e-8x, still 2.889e7x SHORT), " +
            "neutron-star core 1e34 -> 3.4611e-10 (3.46e8x, astrophysical). REQUIRED DENSITIES: u_floor = 2.889272332033454e25 J/m^3 (2.889e7x nuclear) and u_band = 4.706335701649293e37 J/m^3 (4706.3x a neutron-star core). THE MEASURABLE CHANNEL, " +
            "PRICED: the counting floor 1.6102e-6 (= 1/sqrt(<N>), <N> = 3.856917553651e11 — exactly G_014's shot noise and G_009's galactic cross-check) is 0.046374 s/day at 5.4e11x the floor but needs 7.2276e18 kg at 1 cm; the G_002 witness 4.8:1 is " +
            "45 176.138 s/day needing 7.0409e24 kg (1.18 Earth masses); 20:1 is 86 277.089 s/day needing 1.3447e25 kg (2.25 Earth masses). CRITICAL ANSWER: NO in the METRIC sense — the strongest laboratory configuration is 3.4611e-26 against a 1e-18 floor, " +
            "2.889e7x short, and the floor itself needs 13 466 tonnes within a centimetre (2.889e7x nuclear density); YES in the ANALOGUE sense — Delta ln q/d is a kappa = 1 observable (G_014), 0.046374 s/day at the counting floor and 86 277.089 s/day at 20:1. " +
            "WHY THE METRIC EFFECTS THAT EXIST ARE ASTROPHYSICAL: the observed galactic field costs 0.0464 s/day (M/r = 7.2276e20) and the band top 0.1407 s/day (2.1935e21 kg/m), nine orders above the floor's requirement. VERDICTS: MEASURABLE = the analogue " +
            "contrast channel (all four cases) · ASTROPHYSICAL ONLY = every metric effect that exists · REFUTED = a laboratory q profile producing a metric effect. The METRIC verdict is an UPPER BOUND (nuclear density deliberately generous); the laboratory " +
            "can MEASURE rho (G_014) and HOLD a pattern (G_012/G_013) but cannot make rho pull on clocks. No reclassification (G_004's magnitudes, G_009's clock law, G_011b's no-borrowed-coupling and G_014's kappa = 1 are unchanged inputs); D_040 untouched; " +
            "no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g001", "g003", "g004", "g005", "g009", "g010", "g011b", "g014"]),
        new("g016", "Watch Ontology Audit", "Is mass-energy required to generate rho?",
            "THE AT-NATIVE CHAIN (no energy appears in it): Difference -> distinguishability -> the coupling lattice -> { CAPACITY: spectrum lambda with multiplicities m , OCCUPANCY: rho, counting measure, Sigma rho = 1, QG194 } -> E = <lambda, rho> -> g00 = -rho^(2/d) -> clocks " +
            "dtau/dt = rho^(1/d) (QG197). rho and lambda are SIBLINGS off the lattice; E is their PAIRING, and nothing in rho's definition mentions energy. (1) rho FROM PRIMITIVES ALONE - DERIVED: positivity and Sigma rho = 1 need ONLY distinguishability; verified A0 = 45 eigenspaces, multiplicity " +
            "histogram {1:1, 2:42, 5:1, 6:1}, Sigma m = 96, free room Sigma(m - 1) = 51 = N - A0, total spectral weight Sigma lambda = 1152 (a rho-blind capacity invariant, lambda_0 = 0) and Sigma rho = 1.0000000000000000 for every configuration. (2) MASS-ENERGY FROM rho - DERIVED, AND IT IS A " +
            "PAIRING: E = <lambda, rho> (QG180/QG181) evaluates the spectral weight ON the occupancy; E(uniform) = Sigma lambda/N = 12.0 EXACTLY; on the 95-dimensional affine set {Sigma rho = 1} it has RANK 1 and a 94-DIMENSIONAL KERNEL = 51 (energy-free BY DEGENERACY, lambda constant within a " +
            "multiplet) + 43 (zero-net mixing of distinct lambda), so it retains 1.0526315789473684 % of rho. Non-degeneracy rearrangements DO move it: comonotone 13.540176608029563 (+12.834805066913027 %), anticomonotone 10.015359929915876 (spread 3.5248166781136874), reverse-witness " +
            "12.095189171364584 (+9.518917136458427e-2). (3) DOES rho REQUIRE MASS-ENERGY? REFUTED constructively: move density between two cells of the SAME multiplet (lambda constant there) so Sigma rho = 1 stays exact and E is EXACTLY invariant while rho moves - verified in the m = 6 multiplet " +
            "at delta = 0.005 and 0.01 (|dE| <= 1.776e-15, ratios 2.8462 and 49.0000); the canonical witness tilt is ITSELF a pure within-multiplet move (L1 = 0.6666666666666667, dE = 0 exactly, max|a| = 0.6031746). THE 51-DIMENSIONAL FREE ROOM IS THE PROOF: 51 independent directions in " +
            "which rho changes and the total mass-energy does not. (4) THE MINIMAL CARRIER - DERIVED: the CELLWISE counting density itself (the identity map, kappa = 1). Dimension ladder: cellwise rho = 95; the per-multiplet totals (the degeneracy distribution) = 44, losing EXACTLY the 51-dim free " +
            "room; E = 1, losing 94 - so every coarser carrier destroys precisely the energy-free room. Verdicts: actualization density = SOURCE; probability density q = |psi|^2 = CARRIER (identity, QG220/G_014); occupation density = CARRIER (the same read by counting); degeneracy distribution = " +
            "CORRELATED (CAPACITY side - sizes the room but carries no rho, block-sum L1 = 0 to 1e-12); survivor compression = CORRELATED (a FUNCTIONAL of rho, not even energy-free: +4.248925e-3 / -2.959751e-1); E = BOOKKEEPING. CRITICAL ANSWER: YES, EXACTLY - the canonical witness is " +
            "allowed with Sigma rho = 1 and dE = 0 yet carries a 20 : 1 contrast, so the clock ratio between its extreme cells is 20^(1/3) = 2.7144176165949063 and the clock separation 0.9985774245179969 = 86 277.089 s/day, with max|a| = 3.7459607502174e5 x the observed contrast (G_005's " +
            "3.746e5); the realised band caps at 4.8867e-6 = 0.14073696 s/day against the observed 0.04637376 s/day, so the CARRIER exists and the DYNAMICS, not the ontology, forbids realisation. VERDICTS: SOURCE = rho (DERIVED from counting alone) . CARRIER = the occupation/probability density, " +
            "the cellwise identity kappa = 1 . BOOKKEEPING = E = <lambda, rho>, a rank-1 pairing discarding 94 of rho's 95 dimensions, DERIVED as a functional with the value Sigma lambda = 1152 BOUNDARY. WHAT THIS DOES TO G_015: the import there was not rho-needs-energy (FALSE) but the-only-" +
            "laboratory-handle-is-energy (TRUE) - the rank-1 projection of a 95-dimensional object. No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g001", "g002", "g005", "g008", "g009", "g014", "g015"]),
        new("g016b", "Mass Independence Audit", "Can two states have the same energy but different rho, or the same rho but different energy?",
            "THE THREE ANSWERS ARE NOT SYMMETRIC. INDEPENDENT - same E, different rho: the kernel of rho -> E on {Sigma rho = 1} is 94-DIMENSIONAL, explicitly 51 (DEGENERACY - lambda constant within a multiplet) + 43 (lambda-MIXING - zero net over DISTINCT lambda). " +
            "The canonical witness tilt is a pure within-multiplet move: DeltaE = 0 EXACTLY, DeltaRho = L1 = 0.6666666666666667, contrast 20 : 1, DeltaTau/tau = (1/d) ln 20 = 0.9985774245179969 = 86 277.089 s/day. Pairwise m = 2 moves (DeltaRho = 2 delta exactly): " +
            "delta = 0.002 -> DeltaE = -1.776e-15, DeltaRho = 0.004, contrast 1.4752, DeltaTau = 0.129609; delta = 0.005 -> DeltaRho = 0.010, contrast 2.8462, DeltaTau = 0.348656. The 43-dimensional lambda-mixing room is populated explicitly: cells 94/92/90 with lambda = " +
            "15.837372467014836, 15.790176186632262, 15.414213562373096 (three DIFFERENT multiplets) and v = (1, -1.1255345008711273, 0.12553450087112727) satisfying Sigma v = 0 and <lambda,v> = 0 EXACTLY; at scale 0.004 -> DeltaE = -1.776e-15, DeltaRho = 0.009004, " +
            "rho_min = 0.005915, contrast 2.437501, DeltaTau/tau = 0.29699105 = 25 660.0263 s/day. DEPENDENT - E is a FUNCTION of rho (one rho, one E), so the independence is strictly ONE-DIRECTIONAL. REFUTED - same rho, different E as a STATE change: with the SAME uniform rho = 1/96 the " +
            "pairing gives E = 2K EXACTLY for K = 1..6 (2, 4, 6, 8, 10, 12) because Sigma lambda = 192K (192, 384, 576, 768, 960, 1152), A0 = 49/47/45/47/45/45, L1(rho_K, rho_6) = 0 - only the BOUNDARY capacity K (G_007) moves it. THE PHASE SECTOR: four phase assignments give " +
            "L1(|psi|^2, rho) < 2.5e-16, DeltaE = 0 and DeltaTau = 0 exactly, while |Sigma psi| moves 0.03241962809423954 -> 9.117182187865382 (281.22414487183346x, exactly G_011/G_014) - energy-inert and clock-inert. THE FIXED-E FAMILY AND WHY DeltaTau IS UNBOUNDED: tilting every " +
            "multiplet with fraction f on its first cell (fr = 1 for m = 1) gives EXACTLY DeltaE = 0, contrast = 5f/(1 - f) (f >= 1/2), L1(f) = (2/96)[42|2f - 1| + |5f - 1| + |6f - 1|] and DeltaTau/tau = (1/d) ln(5f/(1 - f)) = T <=> f = 1/(1 + 5 e^{-3T}); the ladder is f = 0.8, " +
            "0.947377910367, 0.987757963984, 0.999383331494, 0.999998470491 at T = (1/d) ln 20, 1.5, 2, 3, 5 - and since L1 -> 1.0625 while rho_min -> 0 with DeltaE = 0 throughout, THE CLOCK SEPARATION AT FIXED ENERGY IS UNBOUNDED: no target DeltaTau is forbidden by energy " +
            "conservation. Only positivity and the G_005 band bound it (T = 4.8867e-6 needs f = 0.1666687028016166, contrast 1.0000146602074598; realised 0.14073696 s/day against observed 0.04637376 s/day). At the canonical T the inversion returns f = 0.8 - the canonical " +
            "TiltFractions IS the 20 : 1 solution. VERDICTS: INDEPENDENT = same energy, different rho . DEPENDENT = E is a function of rho . REFUTED = same rho, different energy as a state change. Independence is not symmetry: rho is independent OF E while E is fully determined BY rho. " +
            "No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g002", "g005", "g007", "g011", "g016"]),
        new("g017", "Metric Coupling Audit", "Does any experimentally realizable |psi|^2 profile produce a measurable clock shift?",
            "Systems: optical cavity, resonator array, photon lattice, oscillator network; measure DeltaTau, Delta Phi and the clock signal; compare AT, GR and observed limits. WHY THIS IS NOT G_015: G_017 takes the |psi|^2 reading literally for OPTICAL fields, where the intensity IS the measured field and its contrast is enormous, so the prediction becomes O(1) and can be confronted with real clock performance. " +
            "THREE READINGS: AT-substrate (rho = the actualization density, G_014) with DeltaTau/tau = Delta Phi/c^2 = (1/d) Delta ln rho; AT-naive (a lab intensity profile IS rho) with DeltaTau/tau = (1/d) Delta ln I; GR (stored energy U with m = U/c^2) with Delta Phi/c^2 = G U/(R c^4). The observable is a FRACTIONAL FREQUENCY RATIO, so the ceiling is a clock's fractional resolution (1e-18 best optical clocks, 1e-12 a crude systematic). " +
            "DERIVED - the SUBSTRATE reading is NOT refuted: Earth's surface Delta ln rho/d = 2.320443e-10 = 2.320e8 x the floor (G_004's 0.99600) and the galactic field 5.367333e-7 = 0.046374 s/day = 5.367e11 x the floor (G_009's 0.99668); the logarithmic form is derived too (depths add where ratios multiply). THE FOUR SYSTEMS with recomputed stored energies: OPTICAL CAVITY (F = 1e6, 1 W, 0.3 m; U = (F P_in/pi) x 2L/c = 6.370605e-4 J; Gaussian contrast 1e2) gives AT-naive 0.6666666667 vs GR 3.509234e-47, ratio 1.900e46, exclusion 6.667e17; " +
            "RESONATOR ARRAY (Q = 1e7, 1 mW, 1550 nm; U = QP/omega = 8.228698e-12 J, V = lambda^3 = 3.723875e-18 m^3, u = 2.209714e6 J/m^3, R = 9.615433e-7 m; on/off 1e4) gives 3.0701134573 vs 7.071071e-50, ratio 4.342e49, exclusion 3.070e18; PHOTON LATTICE (Sr clock: E_rec = 2.272842e-30 J at lambda = 813 nm, 88 u, depth 100 E_rec, 300 a.u. -> I = 2.439413e8 W/m^2; node/antinode 1e3) gives 2.3025850930, exclusion 2.303e18; " +
            "OSCILLATOR NETWORK (Q = 1e6, 1 pW, 6 GHz; U = 2.652582e-17 J, u = 2.652582e-8 J/m^3, R = 6.203505e-4 m; 10:1) gives 0.7675283643 vs 3.533090e-58, ratio 2.17e57, exclusion 7.675e17. So AT-naive spans 0.7675 to 3.0701 = 77-307 % while GR spans 1e-47 to 1e-58. " +
            "THE SHARPEST TEST: a Sr LATTICE CLOCK INSIDE ITS OWN STANDING WAVE operates at 2.439413e8 W/m^2 with contrast at least 1e3 and reads a reproducible frequency to 1e-18 every day, while AT-naive predicts a 230.26 % shift across its own lattice - so the naive identification is excluded by 2.303e18 by the very experiment that best tests it (contrast ladder 1e2/1e3/1e4/1e6 -> 1.53505673 / 2.30258509 / 3.07011346 / 4.60517019, exclusions 1.5351e18 / 2.3026e18 / 3.0701e18 / 4.6052e18). " +
            "EXCLUSION AGAINST EVERY CEILING (cavity/resonator/lattice/network): 1e-12 -> 6.667e11 / 3.070e12 / 2.303e12 / 7.675e11; 1e-15 -> 6.667e14 / 3.070e15 / 2.303e15 / 7.675e14; 1e-18 -> 6.667e17 / 3.070e18 / 2.303e18 / 7.675e17; 1e-19 -> 6.667e18 / 3.070e19 / 2.303e19 / 7.675e18 - no ceiling exists at which any realizable profile survives, and the failure is STRUCTURAL not engineering because the law is LOGARITHMIC: " +
            "DeltaTau/tau = (1/3) ln(contrast) is O(1) for every realizable contrast (> 10) with no tuning that makes it small. CRITICAL ANSWER: NO - no experimentally realizable |psi|^2 produces a measurable AT metric clock shift; the effect that exists is the SUBSTRATE's and is consistent with GR to the precision AT predicts. " +
            "VERDICTS: DERIVED = the logarithmic clock law and the substrate reading . BOUNDARY = the identification of a laboratory |psi|^2 with rho, and the scale invariance (the coupling is a boundary input a readout does not borrow, G_011b) . REFUTED = any experimentally realizable |psi|^2 metric clock shift (6.667e17 to 3.070e18 at 1e-18; 6.667e11 to 3.070e12 even at 1e-12; AT-naive vs GR = 1.900e46 to 4.342e49 with the observation agreeing with GR). " +
            "WHY THE MISMATCH IS STRUCTURAL: for the same bench system the two readings differ by 46 to 49 orders of magnitude, so the identification premise flagged as residual in G_014/G_015 is NOT merely unproven but EXPERIMENTALLY EXCLUDED AT BENCH SCALE - G_011b's coupling-is-not-borrowed promoted from a structural statement to a measured exclusion. G_014's PHYSICAL verdict is about kappa = 1 STRUCTURE, not about the coupling, so it remains consistent. No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new primitive.",
            AuditStatus.Passed, new DateTime(2026, 9, 12), TheoryLayer.Physics, TheoryClassification.Derived,
            ["g004", "g009", "g011b", "g014", "g015", "g016b"]),
    ];
}
