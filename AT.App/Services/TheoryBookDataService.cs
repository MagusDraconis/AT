using AT.App.Models;

namespace AT.App.Services;

/// <summary>
/// Strongly typed content for the AT Theory Book. Each section, chapter, and result is built from the
/// completed AT-QG phases (attached by phase ID), so future phases can extend any chapter without
/// structural changes. All content is drawn from the AT-QG program (Docs/Research).
/// </summary>
public static class TheoryBookDataService
{
    // ── Section 1 — Introduction ──────────────────────────────────────────────────

    private static readonly TheorySection Introduction = new(
        Slug: "introduction",
        Title: "Introduction",
        Subtitle: "What is AT?",
        Summary:
            "AT now frames the program around the minimal hierarchy Difference → Actualization → Spectrum → Physics. " +
            "Q-events, geometry, and the rest of the physics chain sit downstream of that base structure, not above it. " +
            "The theory book is therefore an overview of how the hierarchy grows into gravity, quantum theory, matter, gauge sectors, and cosmology.",
        KeyResults:
        [
            new("Mission", "Show how Difference → Actualization → Spectrum → Physics grows into the full theory.", TheoryBadge.Derived, ["QG294", "QG295"]),
            new("Research path", "Difference → Actualization → Spectrum → Physics → Q-events → ρ → geometry → gravity → quantum → matter → gauge → cosmology.", TheoryBadge.Derived, ["QG294", "QG296"]),
            new("Core principle", "Structure is derivable; content is realized. Q-events are downstream of the minimal base.", TheoryBadge.Derived, ["QG294", "QG296"]),
        ],
        Chapters:
        [
            new("what-is-at", "What is AT?",
                "AT is now presented as a hierarchy book: the minimal base is Difference → Actualization → Spectrum → Physics, and the familiar Q-event layer is a derived stage of that chain.",
                [
                    new("Minimal base", "Difference → Actualization → Spectrum → Physics is the newest base layer.", TheoryBadge.Derived, ["QG294", "QG295"]),
                    new("No q-events-first", "Q-events are downstream, not the primitive root.", TheoryBadge.Derived, ["QG296"]),
                ]),
            new("historical-path", "Historical Path (TRM → AT)",
                "AT's predecessor, the Temporal Resonance Model (TRM), assembled striking pieces but carried zero candidate physics. AT re-derived the surviving structure from minimal primitives.",
                [
                    new("Legacy audit", "TRM modules: three absorbed, two rejected, three kept as candidate mathematics.", TheoryBadge.Derived, ["QG-029", "QG-031"]),
                    new("Re-derivation", "AT re-derived surviving structure from the minimal hierarchy, with Q-events placed downstream.", TheoryBadge.Derived, ["QG294", "QG296"]),
                ]),
            new("core-principles", "Core Principles",
                "No q-events-first framing. Difference, actualization, spectrum, and physics are the minimal layers; everything else is a downstream readout.",
                [
                    new("Emergence ladder", "Difference → Actualization → Spectrum → Physics → Q-events → ρ → geometry → gravity.", TheoryBadge.Derived, ["QG294", "QG295", "QG296"]),
                    new("Minimal primitives", "Difference + Actualization + Spectrum + Physics — the minimal hierarchy.", TheoryBadge.Derived, ["QG294"]),
                ]),
        ]);

    // ── Section 2 — Network Ontology ──────────────────────────────────────────────

    private static readonly TheorySection Ontology = new(
        Slug: "ontology",
        Title: "Part I — Network Ontology",
        Subtitle: "Difference, Actualization, Spectrum, Physics, Causal Order, Nodes and Links",
        Summary:
            "The network (V,E) is still the working causal representation, but the base has moved one layer deeper: " +
            "Difference → Actualization → Spectrum → Physics now underlies the Q-event layer. Q-events are the derived " +
            "actualization ticks, while the links carry the familiar ρ, ψ, θ, and S sectors. Causal order is still " +
            "derived from the generation relation, and energy remains the Noether conjugate of causal-order evolution.",
        KeyResults:
        [
            new("Causal order derived", "Full causal order = transitive closure of the generation relation.", TheoryBadge.Derived, ["ATQG110", "ATQG111", "ATQG112"]),
            new("One network primitive", "(V,E) unifies the downstream Q-event layer and psi into one causal-network representation.", TheoryBadge.Derived, ["ATQG550", "ATQG551", "ATQG552"]),
            new("Energy derived", "Energy = Noether conjugate of causal order; measured as actualization rate.", TheoryBadge.Derived, ["ATQG890", "ATQG891", "ATQG892"]),
        ],
        Chapters:
        [
            new("q-events", "Q-Events",
                "A Q-event is a derived actualization tick — one local time-state change inside the downstream network picture.",
                [
                    new("Transition picture", "All 4 transition pictures score 4/4; a bare primitive point fails (a static point cannot happen).", TheoryBadge.Derived, ["ATQG290", "ATQG292"]),
                    new("Not primitive anymore", "Q-events are downstream of Difference → Actualization → Spectrum → Physics.", TheoryBadge.Derived, ["QG294", "QG296"]),
                ]),
            new("actualization", "Actualization",
                "Actualization is the process layer that generates the downstream event picture. Entropy maximization (α=0) selects the uniform per-octave attractor.",
                [
                    new("α=0 attractor", "Uniform per-octave increments A_k=m₀/K accumulate to the log-deficit density ρ.", TheoryBadge.Derived, ["ATQG00"]),
                    new("Criticality", "μ=1 is the unique scale-free branching point (L=1/|ln μ| infinite only at criticality).", TheoryBadge.Derived, ["ATQG12", "ATQG72"]),
                    new("Irreducibility", "Actualization remains a fundamental layer in the minimal hierarchy.", TheoryBadge.Derived, ["QG294"]),
                ]),
            new("causal-order", "Causal Order",
                "Causal order is not primitive: it is the transitive closure of the generation relation (event → descendants).",
                [
                    new("Derived partial order", "Ancestor relation is irreflexive + antisymmetric + transitive — a strict partial order.", TheoryBadge.Derived, ["ATQG110"]),
                    new("Generation primitive", "Generation still supplies the downstream event sequence, but it now sits below the new minimal base.", TheoryBadge.Postulated, ["ATQG112"]),
                ]),
            new("nodes-and-links", "Nodes and Links",
                "The network (V,E) is ONE primitive. Nodes carry spin-0 content; links carry spin-2 (Weyl) + U(1) phase + SU(2) spin.",
                [
                    new("Unified network", "Q-events (spin-0) and psi (spin-2) unify into one causal-network primitive with dual interior.", TheoryBadge.Derived, ["ATQG552"]),
                    new("One link, three sectors", "Complete link = single complex rank-2 object L_ij = a_ij e^(iθ_ij): magnitude (trace+traceless) + phase.", TheoryBadge.Derived, ["ATQG640", "ATQG641", "ATQG642"]),
                    new("Four irreducible sectors", "ρ (spin-0), ψ (spin-2), θ (U(1)), S (SU(2)) — sectors of one link, not separate primitives.", TheoryBadge.Derived, ["ATQG680", "ATQG682"]),
                ]),
            new("energy-from-actualization", "Energy from Actualization",
                "Energy is not a new sector: it is the conserved generator of time translation — the Noether conjugate of causal-order evolution, measured as actualization rate.",
                [
                    new("Derived concept", "Network time = causal order; energy = its conjugate (actualization activity).", TheoryBadge.Derived, ["ATQG890"]),
                    new("Storage", "Energy stored in ψ/ρ excitation; E=mc² links the Higgs condensate (rest mass).", TheoryBadge.Compatible, ["ATQG891"]),
                    new("Value caveat", "Energy VALUES (Hamiltonian, masses) remain empirical (QG85).", TheoryBadge.Postulated, ["ATQG892"]),
                ]),
        ]);

    // ── Section 3 — Gravity ───────────────────────────────────────────────────────

    private static readonly TheorySection Gravity = new(
        Slug: "gravity",
        Title: "Part II — Gravity",
        Subtitle: "Counting Measure ρ, Geometry Emergence, Scalar Gravity, Observables",
        Summary:
            "Gravity emerges from the counting measure ρ: the α=0 actualization attractor generates the log-deficit " +
            "density that reproduces metric origin, deficit matter, Einstein structure, and flat rotation curves. " +
            "The scalar (conformal) sector gives attraction, redshift, and regular cores; the tensor ψ sector restores " +
            "lensing and the Mercury perihelion advance.",
        KeyResults:
        [
            new("ρ from actualization", "α=0 attractor → log-deficit ρ → all four gravity requirements reproduced.", TheoryBadge.Match, ["ATQG00", "ATQG01", "ATQG02"]),
            new("G derived (scale)", "GM_eff = m₀r₀/(d·ρ̄) — the gravitational scale is deficit abundance, not imported.", TheoryBadge.Derived, ["ATQG60", "ATQG62"]),
            new("Mercury perihelion", "ρ+ψ unified network restores γ=β=+1 → 42.98 \"/century (MATCH via the ψ graviton).", TheoryBadge.Match, ["ATQG1030", "ATQG1031", "ATQG1032"]),
        ],
        Chapters:
        [
            new("counting-measure", "Counting Measure ρ",
                "ρ is the per-vertex event density (counting measure). Entropy maximization selects the log-deficit profile ρ = ρ̄ − m₀·ln(R_max/r)/ln(R_max/r₀).",
                [
                    new("α=0 attractor", "Uniform per-octave increments accumulate to exactly the log-deficit density.", TheoryBadge.Derived, ["ATQG00", "ATQG10"]),
                    new("Microscopic origin", "Critical branching (μ=1) → uniform per-octave counts → log-deficit ρ, exactly.", TheoryBadge.Derived, ["ATQG11", "ATQG12"]),
                ]),
            new("geometry-emergence", "Geometry Emergence",
                "The metric emerges from ρ: g = ρ^(2/d)·η (conformally flat). sqrt(−g) = ρ gives the metric origin; Malament-type causal order fixes the conformal class.",
                [
                    new("Metric origin", "sqrt(−g) = ρ > 0 — the counting measure is the volume element.", TheoryBadge.Derived, ["ATQG01"]),
                    new("Conformal structure", "The conformal factor f = ρ^(2/d) induces the conformally-flat metric.", TheoryBadge.Derived, ["QG-022"]),
                ]),
            new("scalar-gravity", "Scalar Gravity",
                "The conformal sector is scalar gravity: a = −(1/d)∇ln ρ has no free coupling. Attraction dominates (phase-gradient +∇θ); repulsion is locally unstable.",
                [
                    new("No free coupling", "The conformal acceleration is fixed by d and the ρ profile.", TheoryBadge.Derived, ["ATQG60"]),
                    new("Attraction only", "Repulsive gravity is unstable locally; Dark Energy is the sole metastable exception.", TheoryBadge.Derived, ["QG-029", "QG-031"]),
                ]),
            new("regular-cores", "Regular Cores",
                "Deficit matter saturates: M_eff(r) = M(1 − e^(−r³/r_c³)) — a regular core with M_eff(0)=0, the first quantitative prediction.",
                [
                    new("Saturation profile", "Poisson saturation of the deficit gives a finite regular core.", TheoryBadge.Derived, ["ATQG750"]),
                    new("Unique prediction", "Distinct from GR, Hayward, and Bardeen profiles — testable via shadow/ISCO.", TheoryBadge.Match, ["ATQG751", "ATQG752"]),
                ]),
            new("rotation-curves", "Rotation Curves",
                "The log-deficit ρ produces flat rotation curves (v²(3)/v²(9) = 1.18) without dark matter — the RAR/MOND-like regime emerges from the counting measure.",
                [
                    new("Flat curves", "Log-deficit density → flat rotation-curve ratio ≈ 1.18.", TheoryBadge.Match, ["ATQG01", "ATQG11"]),
                    new("RAR link", "g† = c·H₀/(2π) derived with zero free parameters (DATA program).", TheoryBadge.Match, ["DATA-003", "DATA-004"]),
                ]),
            new("mercury-perihelion", "Mercury Perihelion",
                "The unified network recovers Mercury's 42.98 \"/century perihelion advance through the ψ spin-2 graviton.",
                [
                    new("GR baseline", "γ=β=1 → factor 1 → 42.98 \"/century.", TheoryBadge.Match, ["ATQG1030"]),
                    new("ρ-only limit", "Conformal γ=−1 → factor −1/3 → RETROGRADE −14.33 \"/century; this marks the scalar-only limit, not the final theory.", TheoryBadge.Partial, ["ATQG1031"]),
                    new("ρ+ψ matches", "The ψ graviton restores γ=β=+1 → +42.98 \"/century (MATCH).", TheoryBadge.Match, ["ATQG1032"]),
                ]),
            new("schwarzschild-limit", "Schwarzschild Limit",
                "Horizon structure: entropy S ~ Area from horizon counting (area law). Mass-radius relation diverges from Schwarzschild (deficit mass ~ R^d vs M ~ R).",
                [
                    new("Area law", "S = A·ln2 ~ R^(d−1) — entropy scales with area.", TheoryBadge.Match, ["ATQG120", "ATQG121"]),
                    new("Temperature mismatch", "Native T~R (anti-Hawking) is a mismatched scaling for the Schwarzschild limit, while the deficit-mass picture remains intact.", TheoryBadge.Partial, ["ATQG131", "ATQG132"]),
                ]),
        ]);

    // ── Section 4 — Tensor Sector ─────────────────────────────────────────────────

    private static readonly TheorySection Tensor = new(
        Slug: "tensor",
        Title: "Part III — Tensor Sector",
        Subtitle: "Why ψ, Spin-2 Selection, Weyl Content, Gravitational Waves, Lensing & PPN",
        Summary:
            "The scalar sector freezes the tensor (Weyl/graviton) degrees of freedom by conformal flatness. The ψ " +
            "field is the traceless (spin-2) content of the complete link relation: it is the minimal new primitive " +
            "that restores lensing, gravitational waves, and the Mercury perihelion advance.",
        KeyResults:
        [
            new("ψ = link Weyl content", "psi is the non-conformal Weyl content of the causal link structure — a rank-2 field, not an external addition.", TheoryBadge.Derived, ["ATQG540", "ATQG541", "ATQG542"]),
            new("Spin-2 uniquely selected", "2 polarizations rule out spin-0; universal attraction rules out spin-1; only spin-2 passes.", TheoryBadge.Preferred, ["ATQG460", "ATQG462"]),
            new("Lensing requires ψ", "Conformal γ=−1 kills all lensing observables; a non-conformal (ψ) sector is required.", TheoryBadge.Partial, ["ATQG260", "ATQG262"]),
        ],
        Chapters:
        [
            new("why-psi-exists", "Why ψ Exists",
                "Q-events alone cannot produce lensing, Shapiro delay, PPN γ=+1, or GW polarization. ψ is the minimal new postulate required by observational completeness.",
                [
                    new("Scalar insufficiency", "The scalar universe is self-consistent but cannot reproduce 4 observations.", TheoryBadge.Derived, ["ATQG470", "ATQG471"]),
                    new("New postulate", "ψ is motivated by observation (GW + bending), preferred in form, not forced by consistency.", TheoryBadge.Postulated, ["ATQG472"]),
                ]),
            new("spin-2-selection", "Spin-2 Selection",
                "Three independent constraints uniquely select spin-2 as the gravitational extension.",
                [
                    new("Minimal extension", "2 graviton helicities = the minimal additional d.o.f. (max(1,2,0)).", TheoryBadge.Derived, ["ATQG240", "ATQG241", "ATQG242"]),
                    new("Preferred form", "Massless spin-2 (Fierz–Pauli) is the unique ghost-free theory.", TheoryBadge.Derived, ["ATQG440", "ATQG442"]),
                    new("Why not 0/1", "Spin-0 fails polarization + full T_μν; spin-1 is repulsive.", TheoryBadge.Derived, ["ATQG460", "ATQG461"]),
                ]),
            new("weyl-content", "Weyl Content",
                "The traceless (Weyl) part of the complete link relation is the spin-2 sector — forced as capacity, contingent in value.",
                [
                    new("Forced capacity", "A complete link carries trace + traceless; conformal-only links are the Weyl=0 restriction.", TheoryBadge.Derived, ["ATQG560", "ATQG562"]),
                    new("Excitation", "Quadrupole (traceless) sources excite Weyl — the mechanism is derived.", TheoryBadge.Derived, ["ATQG570", "ATQG572"]),
                ]),
            new("gravitational-waves", "Gravitational Waves",
                "The scalar sector has only a breathing (monopole) mode — invisible to Michelson interferometers. Observed +/× GWs are tensor (spin-2), requiring ψ.",
                [
                    new("Scalar invisible", "Breathing mode is common-mode → zero differential strain.", TheoryBadge.Derived, ["ATQG200", "ATQG201", "ATQG202"]),
                    new("No fake tensor", "No scalar (collective or otherwise) can source spin-2; psi remains required.", TheoryBadge.Derived, ["ATQG490", "ATQG491", "ATQG492"]),
                    new("GW observation", "Only the raw strain is direct; spin-2 is a model-dependent reconstruction.", TheoryBadge.Partial, ["ATQG480", "ATQG481", "ATQG482"]),
                ]),
            new("lensing-ppn", "Lensing & PPN",
                "Conformally-flat g=ρ^(2/d)η has PPN γ=−1: deflection, convergence, shear, and Shapiro delay all vanish. Only gravitational redshift survives.",
                [
                    new("γ=−1 sector", "All lensing observables scale as (1+γ)/2 and vanish at γ=−1.", TheoryBadge.Derived, ["ATQG260", "ATQG261"]),
                    new("Redshift survives", "z = (ρ₂/ρ₁)^(1/d) − 1 (g_00 alone) — gravitational redshift is present.", TheoryBadge.Match, ["ATQG261"]),
                    new("Restoration via ψ", "A non-conformal (ψ) sector moves γ off −1 and restores lensing.", TheoryBadge.Partial, ["ATQG22", "ATQG262"]),
                ]),
        ]);

    // ── Section 5 — Quantum Sector ────────────────────────────────────────────────

    private static readonly TheorySection Quantum = new(
        Slug: "quantum",
        Title: "Part IV — Quantum Sector",
        Subtitle: "Phase θ, Interference, Born Rule, Entanglement, Measurement",
        Summary:
            "The quantum sector lives on the link: the U(1) phase θ gives superposition and interference; the Born " +
            "rule P=|amplitude|² is consistent; the SU(2) spin structure hosts spin-1/2. Entanglement requires a " +
            "joint link state, and measurement collapse is identified with Q-event actualization.",
        KeyResults:
        [
            new("Interference from links", "Double-slit P_I(±)=½(1±cos(θ₁−θ₂)) — interference recovered from link phases in the interference basis.", TheoryBadge.Match, ["ATQG650", "ATQG651"]),
            new("Measurement = actualization", "A Q-event is a Born-weighted projection in the measurement basis (collapse to a definite outcome).", TheoryBadge.Partial, ["ATQG730", "ATQG732"]),
            new("Entanglement needs J", "Non-separable correlations need a joint (2-qubit) link state — a new sector beyond θ+S.", TheoryBadge.Postulated, ["ATQG710", "ATQG711", "ATQG712"]),
        ],
        Chapters:
        [
            new("phase-theta", "Phase θ",
                "The U(1) phase lives on links (gauge phase home), matter phases on nodes, and loop holonomies are derived.",
                [
                    new("Phase home", "Links are the canonical gauge-phase home; Wilson loops derived.", TheoryBadge.Derived, ["ATQG630", "ATQG632"]),
                    new("Amplitude primitive", "The complex amplitude (U(1) phase) is a new d.o.f. — compatible, not emergent.", TheoryBadge.Postulated, ["ATQG620", "ATQG622"]),
                ]),
            new("interference", "Interference",
                "Path phase accumulation gives interference: a natural consequence of link phases GIVEN the θ primitive.",
                [
                    new("Double-slit", "P_I(±)=½(1±cos(θ₁−θ₂)) reproduces constructive/destructive interference.", TheoryBadge.Match, ["ATQG651"]),
                    new("Holonomy invariant", "Loop holonomy is gauge-invariant; |e^{iθ}|=1.", TheoryBadge.Derived, ["ATQG650"]),
                ]),
            new("born-rule", "Born Rule",
                "P = |amplitude|² is consistent with the actualization picture; in the generation basis the Born probabilities equal the actualization shares.",
                [
                    new("Consistent", "Born rule P=|amplitude|² follows from link-phase amplitudes.", TheoryBadge.Match, ["ATQG652"]),
                    new("Actualization density", "|ψ|² = actualization density (QM-001 derivation).", TheoryBadge.Derived, ["QM-001"]),
                ]),
            new("entanglement", "Entanglement",
                "Shared fixed phases give CLASSICAL correlations only. Quantum non-separability requires an entangling sector (joint link states).",
                [
                    new("Classical only", "Fixed phases → deterministic correlations, not Bell non-separability.", TheoryBadge.Derived, ["ATQG700"]),
                    new("Joint link state", "The minimal addition is a joint (2-qubit) link state — new content.", TheoryBadge.Postulated, ["ATQG711"]),
                ]),
            new("bell-correlations", "Bell Correlations",
                "Superposition, interference, Born rule, entanglement, and Bell correlations are complete given θ+S+J; only collapse was missing.",
                [
                    new("Sector audit", "5/6 quantum features complete with θ+S+J.", TheoryBadge.Derived, ["ATQG720"]),
                    new("Collapse gap", "The single missing piece was the measurement collapse — resolved by actualization.", TheoryBadge.Partial, ["ATQG721", "ATQG722"]),
                ]),
            new("measurement", "Measurement",
                "Measurement collapse is identified with Q-event actualization: a Born-weighted projection to a definite outcome in the measurement basis.",
                [
                    new("Collapse = tick", "A Q-event is a discrete Born-weighted projection.", TheoryBadge.Derived, ["ATQG730"]),
                    new("General bases", "Arbitrary measurement bases reproduced via unitary rotation (θ+S+J); POVMs via Naimark dilation.", TheoryBadge.Match, ["ATQG741", "ATQG742"]),
                ]),
            new("actualization-collapse", "Actualization = Collapse",
                "The measurement problem resolves: actualization IS the collapse, no separate mechanism needed.",
                [
                    new("No extra postulate", "Collapse = actualization; no separate collapse mechanism (QM-004).", TheoryBadge.Derived, ["QM-004"]),
                    new("Binary limit", "Initially a binary projection; generalized to arbitrary bases by the full quantum structure.", TheoryBadge.Partial, ["ATQG731", "ATQG732"]),
                ]),
        ]);

    // ── Section 6 — Matter Sector ─────────────────────────────────────────────────

    private static readonly TheorySection Matter = new(
        Slug: "matter",
        Title: "Part V — Matter Sector",
        Subtitle: "Spin Structure S, Fermions, Family Index, CKM/PMNS, Higgs",
        Summary:
            "The network natively hosts integer spins (spin-0 nodes, spin-2 links, spin-1 gauge). Spin-1/2 fermions " +
            "require a new spin-structure (SU(2) double cover) primitive — compatible but not derivable. The family " +
            "index is a discrete internal label; CKM/PMNS mixing and the Higgs mechanism are representable but their " +
            "specific values stay free.",
        KeyResults:
        [
            new("Fermions need new primitive", "Spin-1/2 spinors require a spin structure (SU(2) double cover) — compatible, not derivable.", TheoryBadge.Postulated, ["ATQG660", "ATQG662"]),
            new("Family replication compatible", "A degenerate family index on the node/link hosts replication; the count 3 stays postulatory.", TheoryBadge.Compatible, ["ATQG810", "ATQG812"]),
            new("Higgs compatible", "Mass generation via a ρ condensate is representable; the VEV and couplings are postulated.", TheoryBadge.Compatible, ["ATQG840", "ATQG842"]),
        ],
        Chapters:
        [
            new("spin-structure", "Spin Structure S",
                "The link carries an SU(2) spin sector. Graph orientation (Z2) is NOT a spin structure.",
                [
                    new("SU(2) sector", "The link's spin sector S hosts the SU(2) representation.", TheoryBadge.Derived, ["ATQG680"]),
                    new("Not derivable", "Spin structure (double cover) is new data — compatible, not native.", TheoryBadge.Postulated, ["ATQG670", "ATQG672"]),
                ]),
            new("fermions", "Fermions",
                "Spin-1/2 fermions need a new spin-1/2 primitive; the network alone hosts integer spins only.",
                [
                    new("Integer spins native", "Spin-0 (nodes), spin-2 (links), spin-1 (gauge) are native.", TheoryBadge.Derived, ["ATQG660"]),
                    new("Half-integer new", "Spinor = section of a spin bundle — not derivable from scalar+rank-2.", TheoryBadge.Postulated, ["ATQG661", "ATQG662"]),
                ]),
            new("family-index", "Family Index",
                "Replication is accommodated by a degenerate discrete family index — no new primitive needed for existence.",
                [
                    new("No topological families", "No topological invariant produces families; spin gives a single rep.", TheoryBadge.Derived, ["ATQG800", "ATQG810"]),
                    new("Count is postulatory", "The 3-generation count is a postulate, coincidental with color.", TheoryBadge.Postulated, ["ATQG802"]),
                ]),
            new("ckm-pmns", "CKM / PMNS",
                "Once the family index exists, mixing is a unitary rotation between flavor and mass bases — representable, entries free.",
                [
                    new("Rotation picture", "Mixing = rotation between flavor and mass bases (family-index dynamics).", TheoryBadge.Compatible, ["ATQG820", "ATQG821"]),
                    new("Angles free", "CKM (3 angles + 1 phase) and PMNS are representable; specific entries are free inputs.", TheoryBadge.Postulated, ["ATQG822"]),
                ]),
            new("higgs-compatibility", "Higgs Compatibility",
                "The scalar ρ already exists; a link condensate can serve as the VEV. Mass generation is representable but not derived.",
                [
                    new("ρ as VEV", "Scalar ρ (node occupancy, spin-0) is already derived; a condensate serves as the VEV.", TheoryBadge.Compatible, ["ATQG840"]),
                    new("Additional content", "The symmetry-breaking potential and Yukawa couplings are postulated.", TheoryBadge.Postulated, ["ATQG841", "ATQG842"]),
                ]),
        ]);

    // ── Section 7 — Gauge Sector ──────────────────────────────────────────────────

    private static readonly TheorySection Gauge = new(
        Slug: "gauge",
        Title: "Part VI — Gauge Sector",
        Subtitle: "U(1), SU(2), SU(3), Why Gauge Splitting?, Open Questions",
        Summary:
            "The three gauge sectors act on different internal spaces and are independent postulates: U(1) θ (charge), " +
            "SU(2) S (spin), and SU(3) C (color). The product structure U(1)×SU(2)×SU(3) is empirical, not derived from " +
            "a unified group.",
        KeyResults:
        [
            new("Gauge product structure", "θ, S, C act on different internal spaces → gauge group is the PRODUCT U(1)×SU(2)×SU(3).", TheoryBadge.Postulated, ["ATQG900", "ATQG902"]),
            new("SU(3) forced given N=3", "Given 3 colors, SU(3) with 8 gluons is forced/unique; the count 3 is the postulate.", TheoryBadge.Preferred, ["ATQG791", "ATQG792"]),
            new("Gauge splitting empirical", "The three sectors are independent postulates; a GUT is additional.", TheoryBadge.Postulated, ["ATQG901", "ATQG902"]),
        ],
        Chapters:
        [
            new("u1", "U(1)",
                "The U(1) phase θ lives on links as the charge sector — the gauge-phase home.",
                [
                    new("Charge on links", "Gauge phases live on links (canonical home); matter phases on nodes.", TheoryBadge.Derived, ["ATQG630", "ATQG632"]),
                    new("From the circle", "Phase lives on S¹; its isometry group is U(1) (ResearchX Atlas).", TheoryBadge.Derived, ["KeyDiscovery"]),
                ]),
            new("su2", "SU(2)",
                "The SU(2) spin sector S hosts spin and weak isospin structure.",
                [
                    new("Spin sector", "The link's S sector is SU(2) — the smallest non-Abelian group.", TheoryBadge.Derived, ["ATQG680"]),
                    new("Real-underived", "SU(2) structure is real-underived in the AT taxonomy.", TheoryBadge.Partial, ["Taxonomy"]),
                ]),
            new("su3", "SU(3)",
                "Color charge can be hosted by an SU(3) connection on the link (lattice QCD analog); the count 3 is empirical.",
                [
                    new("Connection on links", "The link can carry an SU(3) connection; gluons and Wilson loops are SU(3) analogues.", TheoryBadge.Compatible, ["ATQG780", "ATQG781"]),
                    new("Count is input", "Color count N=3 is empirical (baryon statistics), not a network output.", TheoryBadge.Postulated, ["ATQG791"]),
                ]),
            new("why-gauge-splitting", "Why Gauge Splitting?",
                "θ, S, C act on different internal spaces, so the gauge group is the product — there is no derived unified group.",
                [
                    new("Independent spaces", "U(1), SU(2), SU(3) act on charge, spin, color — distinct internal spaces.", TheoryBadge.Derived, ["ATQG900"]),
                    new("No GUT", "No symmetry-breaking chain derives a unified group; a GUT is additional.", TheoryBadge.Postulated, ["ATQG901", "ATQG902"]),
                ]),
            new("open-questions", "Open Questions",
                "The strong force, the color count, and the product structure remain postulatory; SM completeness is an open gap.",
                [
                    new("SM completeness gap", "SU(3), 3 generations, Higgs — compatible but not derived (QG76).", TheoryBadge.Postulated, ["ATQG760", "ATQG762"]),
                    new("Gauge values free", "Coupling strengths and gauge parameters are free inputs (QG85).", TheoryBadge.Postulated, ["ATQG850"]),
                ]),
        ]);

    // ── Section 8 — Cosmology ─────────────────────────────────────────────────────

    private static readonly TheorySection Cosmology = new(
        Slug: "cosmology",
        Title: "Part VII — Cosmology",
        Subtitle: "Expansion, FRW, CMB, Dark Matter, Structure, Dark Energy",
        Summary:
            "The unified network derives expansion (redshift + scale-free ρ) and FRW geometry (a=ρ^(1/d)), and is " +
            "compatible with CMB isotropy and dark-matter effects (flat rotation curves). Structure formation and dark " +
            "energy remain open gaps.",
        KeyResults:
        [
            new("Expansion derived", "Redshift (QG26) + scale-free ρ → cosmological expansion.", TheoryBadge.Derived, ["ATQG770"]),
            new("Cosmology audit", "1 derived / 3 compatible / 2 unknown (structure formation, dark energy).", TheoryBadge.Partial, ["ATQG772"]),
            new("RAR / dark matter", "Log-deficit flat rotation curves — dark-matter effects compatible.", TheoryBadge.Match, ["DATA-003", "ATQG770"]),
            new("Density gives no mass scales", "The density state ρ produces only dimensionless fractions (ΩΛ = I_occ/ln K = 0.6839, Ωm = 0.3161, DERIVED QG234). No ρ-ratio matches v/m_e ≈ 5e5 — the anchors {v, m_e} are independent of cosmology (spectral/boundary, D_044/D_045).", TheoryBadge.Derived, ["ResearchY-D_045"]),
        ],
        Chapters:
        [
            new("expansion", "Expansion",
                "Expansion emerges from redshift (gravitational redshift of the conformal metric) plus scale-free ρ.",
                [
                    new("Derived expansion", "Expansion = redshift + scale-free density (QG-004, QG26).", TheoryBadge.Derived, ["QG-004", "ATQG770"]),
                    new("Λ(t)", "Λ(t) = α/√V(t) from N(t) growth — the AT prediction.", TheoryBadge.Derived, ["QG-004"]),
                ]),
            new("frw-compatibility", "FRW Compatibility",
                "The FRW geometry a(t) = ρ^(1/d) is compatible with the network picture.",
                [
                    new("Scale factor", "a = ρ^(1/d) is the FRW-compatible scale factor.", TheoryBadge.Compatible, ["ATQG770"]),
                    new("Background metric", "Background set by 1-point ρ̄ (conformal, n=1).", TheoryBadge.Compatible, ["ATQG300"]),
                ]),
            new("cmb-compatibility", "CMB Compatibility",
                "CMB isotropy is compatible with the network; the full CMB spectrum is not re-derived.",
                [
                    new("Isotropy compatible", "CMB isotropy is consistent with the conformal background.", TheoryBadge.Compatible, ["ATQG770"]),
                    new("Spectrum open", "The detailed CMB spectrum is not derived from the network.", TheoryBadge.Postulated, ["ATQG762"]),
                ]),
            new("dark-matter-effects", "Dark Matter Effects",
                "Flat rotation curves emerge from the log-deficit density without dark matter; g† = cH₀/(2π) is derived.",
                [
                    new("Flat curves", "v²(3)/v²(9) = 1.18 — flat rotation from the counting measure.", TheoryBadge.Match, ["ATQG01"]),
                    new("RAR exact", "g† = c·H₀/(2π) with zero free parameters (SPARC-verified).", TheoryBadge.Match, ["DATA-003", "DATA-004"]),
                ]),
            new("structure-formation", "Structure Formation",
                "Structure formation is not yet derived from the network — an open gap.",
                [
                    new("Unknown", "Structure formation is UNKNOWN in the cosmology audit.", TheoryBadge.Postulated, ["ATQG772"]),
                ]),
            new("dark-energy", "Dark Energy",
                "Metastable repulsive architecture is possible as a cosmological exception; local voids fill at c.",
                [
                    new("DE only", "Repulsive gravity is unstable locally; only the cosmological exception survives.", TheoryBadge.Derived, ["QG-031"]),
                    new("Open gap", "The full dark-energy sector is not yet derived.", TheoryBadge.Postulated, ["ATQG772"]),
                ]),
        ]);

    // ── Section 9 — Parameters ────────────────────────────────────────────────────

    private static readonly TheorySection Parameters = new(
        Slug: "parameters",
        Title: "Part VIII — Parameters",
        Subtitle: "Free Parameters, Link Lengths, Ratios, Angles, Motifs, Curvature, Resonances",
        Summary:
            "SM parameters are POSTULATED: capacity permits them, symmetries fix their form, and stability/RG " +
            "constrain ranges — but the 19 values stay free. Every geometric mechanism (link lengths, ratios, angles, " +
            "motifs, curvature, resonances, solution space) gives a PARTIAL relation, not value determination. The " +
            "network spectrum (QG104-108) confirms hierarchical structure without numerical correspondence.",
        KeyResults:
        [
            new("Parameters postulated", "19 free SM parameters (+7 for neutrinos): compatible, not derivable.", TheoryBadge.Postulated, ["ATQG850", "ATQG852"]),
            new("Every mechanism partial", "Link lengths, ratios, angles, motifs, curvature, resonances → PARTIAL RELATION.", TheoryBadge.Partial, ["ATQG912", "ATQG942", "ATQG992", "ATQG1002"]),
            new("Spectrum no correspondence", "Network spectra are hierarchical but no ratio numerically matches the SM.", TheoryBadge.Partial, ["ATQG1042", "ATQG1082"]),
        ],
        Chapters:
        [
            new("free-parameters", "Free Parameters",
                "The SM has 19 free parameters (+7 with massive neutrinos). Capacity permits, symmetries fix form, values stay free.",
                [
                    new("Count structurally fixed", "The count 19 is fixed by gauge dims + reps + family index.", TheoryBadge.Derived, ["ATQG861"]),
                    new("Values free", "Masses, couplings, generation count, color count are free inputs.", TheoryBadge.Postulated, ["ATQG850", "ATQG852"]),
                ]),
            new("link-lengths", "Link Lengths",
                "Link length IS the network metric; Yukawa/lattice analogies show HOW values could be encoded — exponents stay free.",
                [
                    new("Metric derived", "Link length is derived from ρ — the network metric.", TheoryBadge.Derived, ["ATQG910"]),
                    new("Encoding compatible", "e^(−m r) suppression is compatible; couplings/angles free.", TheoryBadge.Partial, ["ATQG911", "ATQG912"]),
                ]),
            new("ratios", "Ratios",
                "Dimensionless length ratios are scale-invariant and convert to angles via triangle geometry — a direct analog, not a derivation.",
                [
                    new("Scale-invariant", "Length ratios are dimensionless and scale-free.", TheoryBadge.Derived, ["ATQG970"]),
                    new("Which ratio?", "The network does not specify WHICH ratio maps to WHICH parameter.", TheoryBadge.Partial, ["ATQG971", "ATQG972"]),
                ]),
            new("angles", "Angles",
                "CKM/PMNS mixing angles are internal rotations; geometric triangle angles live in spacetime — an analogy across spaces.",
                [
                    new("Real geometric angles", "Triangle + orientation give genuine geometric angles.", TheoryBadge.Derived, ["ATQG980"]),
                    new("Different spaces", "Internal vs geometric rotations — no native identification.", TheoryBadge.Partial, ["ATQG981", "ATQG982"]),
                ]),
            new("motifs", "Motifs",
                "Triangle/loop/branching motifs carry invariants (area, holonomy) — an organizing structure without value selection.",
                [
                    new("Motif spectra", "Motif counts/stability classes provide a structural organizing principle.", TheoryBadge.Derived, ["ATQG990", "ATQG991"]),
                    new("Derived composites", "Motifs are derived composites (no independent dof).", TheoryBadge.Partial, ["ATQG992"]),
                ]),
            new("curvature", "Curvature",
                "Discrete curvature (deficit angle) is real and derived — the G4 spectral extraction object — but values stay free.",
                [
                    new("Deficit angle", "Curvature = 2π − Σ face angles, derived from the metric.", TheoryBadge.Derived, ["ATQG1000"]),
                    new("Analogy only", "Deficit-angle mass/mixing analogs are suggestive, not determinative.", TheoryBadge.Partial, ["ATQG1001", "ATQG1002"]),
                ]),
            new("resonances", "Resonances",
                "The network has normal modes; mass = resonance frequency is a structural analogy. The D96 standing-wave structure is fully derived — including the Z2 pairing origin (ResearchY-D_021), the derived su(2) selector (ResearchY-D_027), and the derived spectral span (ResearchY-D_028).",
                [
                    new("Spectra exist", "Graph Laplacian + stable normal-mode eigenfrequencies are real.", TheoryBadge.Derived, ["ATQG941", "ATQG950"]),
                    new("Z2 pairing derived", "The pair {cos, sin} at each ω_k is the two-quadrature structure of ONE real oscillation — both eigenfunctions of L at λ_k, forced by λ_k = λ_{N−k}. Not a weak-isospin-only input.", TheoryBadge.Derived, ["ResearchY-D_021"]),
                    new("Pairing completeness boundary", "0-unpaired pairing is N-arithmetic (λ=12 self-conjugate degeneracy: 5-fold at N=96/192, 1-fold at N=64/128) — the Z2-paired sector requirement (D_020 input). REFINED by D_035: complete pairing is DERIVED from complex observability.", TheoryBadge.Partial, ["ResearchY-D_020", "ResearchY-D_021", "ResearchY-D_035"]),
                    new("su(2) selector derived", "Positivity (share ≥ 0), normalization (Born rule = count conservation), and stability (closure fixed point) are DERIVED from the primitives; they select the compact form su(2) for the weak sector.", TheoryBadge.Derived, ["ResearchY-D_026", "ResearchY-D_027"]),
                    new("SU(2) gauge input boundary", "The SU(2) gauge algebra itself is an independent input (sector S); the doublet is the EMERGENT attachment surface, the compact-form choice EMERGENT from observability.", TheoryBadge.Partial, ["ResearchY-D_023", "ResearchY-D_026"]),
                    new("Span derived", "span = ω_max/ω_min ~ 0.0578·N (DERIVED value): ω_max→√12 (antipodal), ω_min~(2π√91)/N. span(96)=6.4025 is the N=96 point; 3 families = floor(log₂ span)+1 is the consequence. The span ∈ [4,8) 3-family WINDOW is the observable-sector INPUT (BOUNDARY, D_020); N=96 is DERIVED (D_040).", TheoryBadge.Derived, ["ResearchY-D_028", "ResearchY-D_040"]),
                    new("Octave rung derived", "n = p·2^k is DERIVED: floor(log₂ span)+1 is an octave partition, and ω(k)~c·k makes k→2k a frequency octave (ω(2)/ω(1)=1.97). Only the seed period p=3 is boundary.", TheoryBadge.Derived, ["ResearchY-D_030"]),
                    new("Time not the first dimension", "The actualization tick is a dimensionless ordering (DERIVED, QG220) that serves as the natural time PARAMETER (EMERGENT): θ_k=2πk/N advances 2π/N per tick, N ticks close the cycle. Frequency EMERGES from the tick phase rate (ω₁≈√91·2π/N); E=ħω and seconds require anchors (BOUNDARY, D_010/D_012). Time is NOT the first physical dimension — the tick is the first dimensionless parameter.", TheoryBadge.Derived, ["ResearchY-D_041"]),
                    new("Span is the derived π", "span = ω_max/ω₁ = 6.4025 is the structural ratio of the C96 ring (π's role) but DERIVED where π is BOUNDARY: algebraic (integer-matrix spectrum) vs π transcendental (B_002). Invariant under N-preserving ring automorphisms; NOT universal across N (span~0.0578·N). Ratio family (λmax/λ₂=40.99, ω₂/ω₁≈1.97, A³=4.81e16) generates the family/mode/scale/Planck hierarchies — all DERIVED.", TheoryBadge.Derived, ["ResearchY-D_042"]),
                    new("Dual anchor emergent", "The {v, m_e} dual-anchor necessity is EMERGENT from sector splitting: the D96 dimensionless structure hosts the bosonic sector (M_W/M_Z/M_H/M_Pl = v·(dimensionless)) and the fermionic sector (m_u..m_t = m_e·(dimensionless)); each needs its own absolute scale and no canonical factor links them (m_e/v ~ 2e-6, D_013 H1 REFUTED). One anchor fails; anchor count 2 is irreducible.", TheoryBadge.Partial, ["ResearchY-D_043"]),
                    new("Anchor origin", "v = 137·ln(span) = 254.37 GeV (QG168): 137 = Σm+#d (1/α_em denominator) and ln(span) are D96-DERIVED — only the GeV unit is BOUNDARY. m_e = 0.511 MeV has NO D96 construction (pure BOUNDARY, fermionic anchor). Neither defines the other (v/m_e ~ 5e5, D_013 H1/H2/H3 REFUTED). M_Pl/v = A³ = 4.81e16 DERIVED (D_007).", TheoryBadge.Partial, ["ResearchY-D_044"]),
                    new("Seed period derived", "p=3 is the UNIQUE period with complete Z2 pairing (0 unpaired) at its natural octave-rung size + convergence (p=2/4→64, p=5→80 have 1 unpaired; p=6 fails convergence). The Z2-paired sector requirement itself is the D_020 boundary input.", TheoryBadge.Derived, ["ResearchY-D_031"]),
                    new("Pairing completeness boundary", "0-unpaired pairing is the observable-sector requirement that every frequency carry a doublet structure: the self-conjugate mode k=N/2 has sin(πn)=0 and must sit in a degenerate group (λ=12 5-fold at 96/192, 1-fold at 64/80/128). REFINED by D_035: now DERIVED from complex observability — the boundary is 'the observable sector is complex'.", TheoryBadge.Derived, ["ResearchY-D_032", "ResearchY-D_035"]),
                    new("Singlet prohibition", "A lone unpaired mode is mathematically allowed (L·cos₃₂=12·cos₃₂ at N=64) but physically excluded: it breaks reciprocity/phase/representation closure and weak-isospin attachment. The observable sector is a reciprocal pair structure ('no isolated oscillator').", TheoryBadge.Partial, ["ResearchY-D_033"]),
                    new("Reciprocity derived", "Reciprocity = the [magnitude, phase] complex structure (QG218): magnitude |ψ|=√ρ (count, QG216) + phase θ (link, QG63) — the two DOFs are DERIVED; complex gives interference, real-only loses it. Reciprocity is EMERGENT; complete pairing BOUNDARY (D_020).", TheoryBadge.Derived, ["ResearchY-D_034"]),
                    new("Complete pairing derived from complex observability", "The self-conjugate mode k=N/2 is REAL-ONLY (sin(πn)=0); its eigenvalue λ=12 is 1D at N=64/80/128 (isolated singlet) and 5D at N=96/192. Complex observability (every frequency carries [magnitude, phase], QG218/D_034) requires multiplicity ≥ 2 — at N=96 all eigenvalues qualify. Complete pairing (0 unpaired) is DERIVED. REFINED by D_036: 'the observable sector is complex' reduces to the Z2-paired sector requirement (D_020); the pairing STRUCTURE itself remains DERIVED (D_021).", TheoryBadge.Derived, ["ResearchY-D_035", "ResearchY-D_036"]),
                    new("Complex states derived", "ψ = |ψ|·e^{iθ} is DERIVED: the two real DOFs are the two faces of the SAME tick k — magnitude |ψ|=√ρ (count, QG216) and phase θ=2πk/N (circulation, QG220). The phase is the pairing discriminator (cos even under k↔N−k, sin odd); removing it collapses the mirror pairs. Interference P=2+2cos(θ₁−θ₂) is a consequence, not the cause.", TheoryBadge.Derived, ["ResearchY-D_036"]),
                    new("Reciprocity from observability", "Observability = complete state reconstruction: the complex state's two DOFs need BOTH quadrature channels. The {cos, sin} pair (eigenfunctions at λ_k=λ_{N−k}, orthogonal, equal norm) IS the reciprocal measurement basis — z=a+ib reconstructs exactly, a alone leaves θ ambiguous. A singlet's phase is unobservable. Reciprocity EMERGENT; complete pairing DERIVED; Z2-paired sector requirement BOUNDARY (D_020).", TheoryBadge.Partial, ["ResearchY-D_037"]),
                    new("State identity forces two DOFs", "Observability = state identity: magnitude-only collapses the [4,4,87] occupancy to 3 distinct states for 95 modes (mirror k/N−k identical); phase-only loses probability (uniform). The complex state ψ=|ψ|·e^{iθ} is the minimal complete identity: 95/95 injective with Born rule Σρ=1 exact. State identity EMERGENT; complex state DERIVED.", TheoryBadge.Partial, ["ResearchY-D_038"]),
                    new("Difference = distinguishability", "The primitive 'Difference' IS the act of distinguishing; state identity is the primitive applied to the state space, not a separate boundary. The real-only space collapses 95 modes to 48 real states (mirror pairs cos-identical — no Difference between them) and to 3 magnitude buckets; the complex space realizes Difference fully (95/95 distinct, Born rule exact). Boundaries: {Difference, η} (D_027) + Z2-paired sector requirement (D_020).", TheoryBadge.Derived, ["ResearchY-D_039"]),
                    new("Four irreducible boundary inputs", "B_final = {Difference, η} (primitives, D_027/D_039) ∪ {Z2-paired (complex) sector} (D_020; 'observable sector is complex' reduces to it, D_036) ∪ {3 octave families} (span ∈ [4,8), D_020) ∪ {SU(2) gauge + j=1/2} (D_022/D_024). Everything else in the D-chain (pairing, reciprocity, complex states, p=3, N=96) is DERIVED or EMERGENT. Complete pairing BOUNDARY→DERIVED (D_035); p=3/N=96 BOUNDARY→DERIVED (D_031); su(2) compact-form BOUNDARY→EMERGENT (D_026); state identity EMERGENT→DERIVED (D_039). Chain monotone; no contradictions remain.", TheoryBadge.Derived, ["ResearchY-D_040"]),
                    new("V2.1 boundary program complete", "The V2.1 origin program is COMPLETE (R_001): the final irreducible boundary set has FIVE items — {Difference, η}, {Z2-paired (complex) sector}, {3 octave families}, {SU(2) gauge + j=1/2}, {v, m_e}. 20 objects DERIVED (pairing, complex states, p=3, N=96, span, ΩΛ/Ωm, v structure, M_Pl/v), 10 EMERGENT, 0 OPEN. The anchors v (structure 137·ln span derived, GeV unit boundary) and m_e (pure boundary) are independent of the cosmological density (ΩΛ/Ωm dimensionless fractions only).", TheoryBadge.Derived, ["ResearchY-R_001"]),
                    new("V2.1 release ready", "The V2.1 boundary program is tagged-ready (R_002): release notes (Docs/Publication/RELEASE_NOTES_V2_1.md), changelog [2.1.0] — 2026-08-30, tag v2.1-boundary-program, no migration (canonical AT V2.0 unchanged), 372 tests passing.", TheoryBadge.Derived, ["ResearchY-R_002"]),
                    new("V2.2 roadmap", "Top-10 V2.2 targets (NP_001) ranked by novelty × testability × V2.1-dependence: T1 measurement origin (D_037 basis + D_039 identity + QG73 collapse), T2 information-encoded measurement, T3 baryon asymmetry (CP QG166), T4 neutrino hierarchy (P2 pending), T5 CMB spectrum, T6 dark-matter status, T7 v/m_e reducibility, T8 Λ coincidence, T9 collapse/observer, T10 initial conditions. Constraints: no new primitives; five-item boundary set respected.", TheoryBadge.Derived, ["ResearchY-NP_001"]),
                    new("Eight new predictions", "ResearchY yields 8 predictions absent from V2.0 (D_046): P1 spectral doublets O(2)-type not SU(2); P2 su(2) from unitarity; P3 N=96 selected not closure-produced (only 96 is a zero-defect octave rung); P4 ω₁≈√91·(2π/N); P5 span = N-specific π-analogue; P6 v = 137·ln(span) = 254.37 GeV; P7 v/m_e irreducible; P8 families = floor(log₂ span)+1 = 3. Each has a falsification path; no canonical change.", TheoryBadge.Derived, ["ResearchY-D_046"]),
                    new("Measurement is the top V2.2 program", "MEASUREMENT ORIGIN ranks highest (19/20) among ten V2.2 candidates (NP_002): it uses the derived chain most directly — Difference (D_039), magnitude, phase, complex states (D_036), the reciprocal two-quadrature measurement basis (D_037), and Actualization. Completing the QM chain (QG73 collapse + QG74 measurement + D_037 + D_039) is the recommended first project.", TheoryBadge.Derived, ["ResearchY-NP_002"]),
                    new("Measurement event derived", "A measurement event is an ACTUALIZATION EVENT on a DISTINGUISHABLE state (M_001): it reads both quadratures of one complex mode (the {cos, sin} basis, D_037), actualizing the state's identity with Born weight (QG216). State identity/observability/probability DERIVED; the measurement event and its collapse reading EMERGENT — QG73 resolved as the event's binary reading.", TheoryBadge.Derived, ["ResearchY-M_001"]),
                    new("Measurement disturbs minimally", "The minimal unavoidable disturbance of a measurement is PHASE-PINNING (M_002): reading both quadratures extracts and fixes the phase; magnitude/identity/probability survive. Measurement without disturbance is impossible; repeated measurements are idempotent; the complex state is basis-invariant. Disturbance DERIVED from the read.", TheoryBadge.Derived, ["ResearchY-M_002"]),
                    new("Measurement feeds forward", "A measurement outcome necessarily changes future evolution (M_003): the pinned phase becomes the initial condition of the deterministic trajectory, θ_t = θ₀ + t·Δθ (Δθ = 2πk/N, D_041). Measured future fixed, unmeasured a superposition; future interference needs the outcome fed back. Feedback DERIVED; evolution DERIVED (D_041).", TheoryBadge.Derived, ["ResearchY-M_003"]),
                    new("Measurement information limit", "The maximum information content of one measurement event is log₂(95) ≈ 6.57 bits (M_004) — the size of the distinguishable state space (D_039). A measurement resolves which of 95 states is realized (gain log₂ 95); repeated measurements are idempotent (0 additional info); measurement creates information. Information DERIVED; event EMERGENT (M_001).", TheoryBadge.Derived, ["ResearchY-M_004"]),
                    new("Information conserved in measurement", "Measurement REVEALS pre-existing distinguishability and REDISTRIBUTES it — it does NOT create information (M_005). The 6.57 bits pre-exist in the state space (D_039); log₂ 95 = outcome + observer (conserved). Underlying: count conservation (Born rule Σ|ψ|²=1, QG216).", TheoryBadge.Derived, ["ResearchY-M_005"]),
                    new("Observer is the epistemic recipient", "The observer's role (M_006) is the RECIPIENT of the information redistribution — it changes only epistemic access, not the ontic state. The state (D_039), observability, and the reconstruction map z=a+ib (D_037) are observer-independent (DERIVED); the observer role and epistemic access are EMERGENT. Measurement program complete (M_001–M_006).", TheoryBadge.Derived, ["ResearchY-M_006"]),
                    new("Measurement chain classified", "The chain D96 → pairing → complex state → reciprocity → observability → measurement is fully classified (M_007): pairing/complex state/observability DERIVED; reciprocity/measurement/observer EMERGENT; disturbance/feedback/information/conservation DERIVED. Only the five R_001 boundaries remain.", TheoryBadge.Derived, ["ResearchY-M_007"]),
                    new("Two measurement predictions", "The measurement chain is mostly QM-equivalent (idempotence, basis, complementarity, Born), with two candidate AT-specific predictions (M_008): AT-P042 — post-measurement phase advances per actualization tick (Δθ=2πk/N, discrete time); AT-P043 — one event reveals at most log₂ 95 ≈ 6.57 bits.", TheoryBadge.Derived, ["ResearchY-M_008"]),
                    new("Discriminator: one prediction survives", "The M_009 discriminator keeps exactly one uniquely-AT prediction. AT-P042 is C — genuinely new: the discrete tick time-parameter gives a FINITE phase lattice {θ₀+m·2πk/N} of cardinality N/gcd(N,k) ≤ 96 vs QM's phase continuum (step DERIVED, D_041). AT-P043 is A — already implied: log₂(d) per event is the standard d-outcome Shannon bound (QM imposes the same); only the value d=95 is AT-derived (D_039). AT-P042 remains PREDICTION; AT-P043 downgraded to CORRESPONDENCE.", TheoryBadge.Derived, ["ResearchY-M_009"]),
                    new("Phase lattice is observably QM-equivalent", "M_010: AT-P042's discrete phase evolution produces NO observable effect beyond continuous QM at any tick-sampled time. Continuous QM with ω=2πk/(N·τ) reproduces phase, recurrence (N/gcd(N,k)), interference (2π(k₁−k₂)/N per tick), and finite-state orbits exactly at ticks. Only the sub-tick phase differs (in-principle-only — the tick is the fundamental clock). Tick observables CORRESPONDENCE; discrete time-parameter structural PREDICTION; nothing falsified.", TheoryBadge.Derived, ["ResearchY-M_010"]),
                    new("The phase is the one lever", "NP_003: the theory has exactly ONE controllable lever — the phase θ₀ of a complex state. Locally variable (B): a measurement pins it (M_002) and it becomes the future initial condition (θ_t = θ₀ + t·Δθ, M_003). Changes time behaviour + measurement; NOT frequency, gravity, or sector structure. Everything else (Difference, η, N=96, spectrum, ω₁, λ₂, anchors {v,m_e}) is fixed; no global lever exists.", TheoryBadge.Derived, ["ResearchY-NP_003"]),
                    new("Phase couples, but does not synchronize", "NP_004: the phase is a TRUE lever — it couples through interference (I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B)) and through a shared actualization event (joint pinning, M_002). But synchronization requires IDENTICAL modes: θ_A−θ_B = (θ_A0−θ_B0) + t(Δθ_A−Δθ_B) is time-invariant only if k_A = k_B; unequal modes drift (no locking force). Sustained phase relations are common-origin correlations.", TheoryBadge.Derived, ["ResearchY-NP_004"]),
                    new("Missing synchronization mechanism", "NP_005: unequal-mode phase locking requires a cross-phase feedback term κ·sin(θ_B−θ_A) absent from the canonical chain (the update θ(t+1)=θ(t)+Δθ has only the self-rate). The term gives dψ/dt = Δθ_A−Δθ_B−2κ·sin(ψ), a stable fixed point iff κ ≥ |Δθ_A−Δθ_B|/2 (0.5236 for k=16,32). Equal modes synchronize trivially; the locking force is BOUNDARY.", TheoryBadge.Derived, ["ResearchY-NP_005"]),
                    new("Locking term is Born-derived in form", "NP_006: the locking term's form is the interference gradient — ∂I/∂θ_A = 2√(ρ_Aρ_B)·sin(θ_B−θ_A) (I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B), Born QG216) — with coefficient κ = 2√(ρ_Aρ_B), a DERIVED cross-amplitude, not free. But the MECHANISM (a gradient-following phase update) is absent in canonical AT — it would be EMERGENT only under a variational principle.", TheoryBadge.Derived, ["ResearchY-NP_006"]),
                    new("Coupling is a static Born network", "NP_007: Actualization defines a STATIC COUPLING NETWORK — the interference cross-term 2√(ρ_Aρ_B)·cos(θ_A−θ_B) is the link between any two superposed states, with link weight κ = 2√(ρ_Aρ_B) (DERIVED, Born). The network carries count/information flow (M_005), is reciprocal (D_037), and yields collective modes (in-phase (√ρ_A+√ρ_B)², anti-phase (√ρ_A−√ρ_B)²) — but NO phase flow and NO propagating field (BOUNDARY).", TheoryBadge.Derived, ["ResearchY-NP_007"]),
                    new("Extremum principle is hidden in I", "NP_008: canonical Actualization extremizes NOTHING (option D) — the self-rate update θ(t+1)=θ(t)+Δθ drifts I non-monotonically (1.760→0.980). The extrema of I are in-phase max ((√ρ_A+√ρ_B)²) and anti-phase min ((√ρ_A−√ρ_B)²); its gradient ∂I/∂θ_A = 2√(ρ_Aρ_B)·sin(θ_B−θ_A) IS the missing sync term — a variational phase update would lock rel at an extremum. Principle EMERGENT/BOUNDARY.", TheoryBadge.Derived, ["ResearchY-NP_008"]),
                    new("No hidden objective in actualization", "NP_009: canonical Actualization obeys NO extremum principle (D) — it IGNORES I (drifts 1.760→0.260; no increase/decrease/conservation). No hidden objective exists (count conserved M_005, info static M_004, distinguishability static D_039, I not fed back). Smallest modification: one gradient term θ+=Δθ+η·∂I/∂θ — a gradient flow (d rel/dt=−2ηκ·sin(rel), κ=2√(ρ_Aρ_B)) with stable fixed point rel=0 = max(I) — so actualization WOULD follow max(I) and sync would emerge. EMERGENT/BOUNDARY.", TheoryBadge.Derived, ["ResearchY-NP_009"]),
                    new("Synchronization needs a second layer", "NP_010: Network 1 (Actualization, local self-rate) cannot synchronize. κ = 2√(ρ_Aρ_B) is a LINK property (depends on both endpoints, symmetric) — DERIVED. But no canonical mechanism carries phase flow (reciprocity is a read basis D_037; info flow redistributes counts M_005; shared events pin once M_002). Network 2 (phase-flow/gradient layer) is structurally present, dynamically absent — BOUNDARY; sync would live there.", TheoryBadge.Derived, ["ResearchY-NP_010"]),
                    new("Network 2 is not a physical field", "NP_011: the coupling network (κ = 2√(ρ_Aρ_B)) is a MATHEMATICAL STRUCTURE, not a physical field. It fails all criteria: no state-independent existence (κ=0 without states), no stored structure (no field variables), no information/phase/energy transport, and κ is descriptive only (the canonical update has no κ term). Every observable (interference, collective modes) comes from the state structure alone. A physical coupling field would be BOUNDARY.", TheoryBadge.Derived, ["ResearchY-NP_011"]),
                    new("Unique predictions are spectral", "NP_012: after removing all QM-equivalent interpretations, the measurement/coupling programs leave NO observationally-testable unique prediction (event, pinning, feedback, log₂95, conservation, observer are all QM-equivalent; AT-P043 downgraded; AT-P042 structural only). The surviving uniquely-AT predictions are the N=96 spectral values: ω₁=√91·(2π/N)≈0.624 (FIRST), families=floor(log₂ span)+1=3, O(2) doublet, v=137·ln(span)=254.37 GeV.", TheoryBadge.Derived, ["ResearchY-NP_012"]),
                    new("Strongest D96 prediction is the O(2) doublet", "NP_013: the strongest falsifiable D96-specific prediction is the O(2) EXACT DOUBLET DEGENERACY (λ_k=λ_{N−k} for every mode k, D_021) — absent from QM/GR/SM. Ranking (uniqueness×impact×feasibility): O(2) doublets 13, family count 13, ω₁=√91·(2π/N)≈0.6244 (12), v=137·ln(span)=254.37 GeV (11), span=6.4025 (10). All five are uniquely D96 PREDICTIONS.", TheoryBadge.Derived, ["ResearchY-NP_013"]),
                    new("Synchronization is optional", "NP_014: physics does NOT require synchronization (option B). Comparing U1 (canonical, no locking) with U2 (gradient locking), every canonical law survives identically (measurement M_002, conservation Σρ=1 & log₂95, reciprocity D_037, 95 states D_039, identity D_036). U2 only collapses relative-phase diversity (continuum → one). The canonical absence is a FEATURE preserving the information channel.", TheoryBadge.Derived, ["ResearchY-NP_014"]),
                    new("O(2) doublets give exact mirror frequencies", "NP_015: the O(2) exact doublet degeneracy (λ_k=λ_{N−k}, D_021) predicts observable mirror-pair frequencies — ω_k/ω_{N−k}=1 exactly, 47 mirror pairs + central mode k=48, O(2) reflection symmetry. Any |Δλ|>0, missing pair, or triplet falsifies it. Distinct from QM (no fixed spectrum), SM (weak doublets are non-degenerate gauge pairs), GR (no frequencies). PREDICTION.", TheoryBadge.Derived, ["ResearchY-NP_015"]),
                    new("Mirror pairs are native to the ring", "NP_016: the O(2) mirror-pair degeneracy is native to the D96 ring modes (ω_k=ω_{N−k} for every k≠48, |Δλ|=0). Target ranking: (1) ring resonance spectrum HIGH; (2) cosmological acoustic peaks MEDIUM (peak ratios, D96-derived); (3) GW ringdown LOW (damped); (4) SM particles LOW (weak doublets split); (5) neutrinos LOW (ordering open). Mirror pairs observable only in C96-ring systems.", TheoryBadge.Derived, ["ResearchY-NP_016"]),
                    new("CMB carries the D96 octaves", "NP_017: nature contains an APPROXIMATE D96 signature — the CMB acoustic peak ratios follow the D96 octave hierarchy (ℓ₁=220.48, 0.008%; r₂₁=2.4368, 0.035%; r₃₁=3.6965, 0.058%; n_s=0.96497 — QG237/238, CORRESPONDENCE). But no natural system shows the exact O(2) mirror-pair degeneracy (PREDICTION, unobserved). CMB is the strongest candidate; no domain falsified.", TheoryBadge.Derived, ["ResearchY-NP_017"]),
                    new("Distinguishability is directly observable", "NP_018: distinguishability generates direct observables — the state count (95, D_039), entropy (log₂95=6.57 bits, M_004), information density (I_occ=0.7513 nats, QG228), and — strongest — the cosmological fraction ΩΛ=I_occ/ln K=0.6839 (QG234, OBSERVED to 0.12%). QM/SM/GR have no fundamental observable as a function of distinguishability.", TheoryBadge.Derived, ["ResearchY-NP_018"]),
                    new("Information cosmology is the density pair", "NP_019: the information density I_occ fixes EXACTLY the density-fraction pair — ΩΛ=I_occ/ln K=0.6839, Ωm=(ln K−I_occ)/ln K=0.3161 — and their ratio 2.1636. n_s (0.96497) and ℓ₁ (220.48) are D96-spectral, not I_occ functions; H₀/σ₈/BAO/growth have no direct relation. I_occ is a genuine but narrow cosmological variable.", TheoryBadge.Derived, ["ResearchY-NP_019"]),
                    new("Black holes cannot destroy information", "NP_020: a black hole CANNOT eliminate Difference — information is conserved through horizon formation (M_005). The conserved quantities (count Σρ=1, positivity, normalization, 95-state identity D_039) survive crossing; the horizon removes ACCESS, not distinguishability. Fates: destroyed NO; hidden/redistributed/preserved YES. Mechanism: HORIZON BOOKKEEPING. Resolves the paradox in the conservation direction.", TheoryBadge.Derived, ["ResearchY-NP_020"]),
                    new("Horizon bookkeeping: store, redistribute, encode", "NP_021: information is conserved across a horizon by HORIZON BOOKKEEPING — storage (states retain distinguishability, D_039), redistribution (external radiation re-encodes, M_005), encoding (hidden/accessible partition). State-space expansion is REFUTED (state space fixed at 95). Balance: log₂95 = H_hidden + H_observer, conserved.", TheoryBadge.Derived, ["ResearchY-NP_021"]),
                    new("Geometry and information share ρ", "QG_001: both geometry and information EMERGE from the count density ρ (option C). Geometry: g = ρ^(2/d)η (QG197/222). Information: I = KL(ρ‖uniform) = 0.7513 nats (QG228), feeding ΩΛ = I_occ/ln K = 0.6839 (QG234). Neither generates the other; the bridge is ρ. Black holes, cosmology, and measurement all confirm the shared root.", TheoryBadge.Derived, ["ResearchY-QG_001"]),
                    new("Geometry is a manifestation of distinguishability", "QG_002: the metric g = ρ^(2/d)η is a PURE FUNCTION of the N=96 state structure (N → spectrum → ρ → g). Metric information is inferable from distinguishability alone. AT is INFORMATION-FIRST (distinguishability → spectrum → ρ → metric), not geometry-first. Horizon, ΩΛ, and measurement all confirm the chain.", TheoryBadge.Derived, ["ResearchY-QG_002"]),
                    new("Geometry is not informationally complete", "QG_003: information alone cannot reconstruct the metric. I = KL(ρ‖uniform) is a single scalar; ρ is a full distribution (many share the same KL). ΩΛ fixes ln K (the size), not ρ. g = ρ^(2/d)η needs ρ. The chain is state structure → ρ → {I, g}; the inverse fails. The state structure (N=96) is the primitive.", TheoryBadge.Derived, ["ResearchY-QG_003"]),
                    new("Mapping speculative", "No native operator identified whose spectrum equals the SM parameters.", TheoryBadge.Partial, ["ATQG942", "ATQG952"]),
                ]),
            new("global-solution-space", "Global Solution Space",
                "Global consistency carves out a solution-space manifold with parameter correlations — organizing, not determining.",
                [
                    new("Consistency manifold", "Loops, single-valued metric, triangle inequalities → solution space with topology.", TheoryBadge.Derived, ["ATQG1020"]),
                    new("Non-unique", "Nothing selects a unique solution whose properties equal the SM parameters.", TheoryBadge.Partial, ["ATQG1021", "ATQG1022"]),
                ]),
        ]);

    // ── Section 10 — Predictions ──────────────────────────────────────────────────

    private static readonly TheorySection Predictions = new(
        Slug: "predictions",
        Title: "Part IX — Predictions",
        Subtitle: "Network Discreteness, Regular Core, Testable Signatures, Falsification",
        Summary:
            "AT's unique predictions: a common discreteness scale for all four sectors (network granularity), and the " +
            "regular-core profile M_eff(r)=M(1−e^(−r³/r_c³)). Both are testable and falsifiable in principle; the free " +
            "scale rc makes falsification challenging.",
        KeyResults:
        [
            new("Network discreteness unique", "A COMMON discreteness scale for ρ/ψ/θ/S is the unique AT prediction absent from GR+SM.", TheoryBadge.Match, ["ATQG690", "ATQG692"]),
            new("Regular core unique", "M_eff(r)=M(1−e^(−r³/r_c³)) — distinct from GR, Hayward, Bardeen.", TheoryBadge.Match, ["ATQG750", "ATQG751", "ATQG752"]),
            new("Falsifiable", "Both predictions are testable in principle; free scales limit current falsification power.", TheoryBadge.Partial, ["ATQG752", "ATQG692"]),
        ],
        Chapters:
        [
            new("network-discreteness", "Network Discreteness",
                "Spacetime granularity at a common scale is the unique signature of the network — no GR/SM analog.",
                [
                    new("Unique signature", "GW/lensing/BH/quantum all reproduce GR/SM; only network discreteness is unique.", TheoryBadge.Derived, ["ATQG690", "ATQG691"]),
                    new("Scale is free", "The discreteness scale is a free parameter (QG14/QG38).", TheoryBadge.Postulated, ["ATQG691"]),
                ]),
            new("regular-core-prediction", "Regular Core Prediction",
                "The deficit saturates: M_eff(r) = M(1−e^(−r³/r_c³)) with exponent 3 — a finite core with zero central mass.",
                [
                    new("Profile", "Exponent 3 (spatial dimension); M_eff(0)=0, →M asymptotically.", TheoryBadge.Match, ["ATQG750"]),
                    new("Distinct from GR", "Differs from GR (singular), Hayward, and Bardeen profiles.", TheoryBadge.Match, ["ATQG751"]),
                ]),
            new("testable-signatures", "Testable Signatures",
                "Shadow, ISCO, lensing, and ringdown can discriminate the regular-core profile.",
                [
                    new("Observables", "Shadow/ISCO/lensing/ringdown are the discriminating observables.", TheoryBadge.Match, ["ATQG752"]),
                    new("Network spectrum", "Hierarchical discrete spectra are computable (QG104-108) — structural tests.", TheoryBadge.Partial, ["ATQG1040", "ATQG1080"]),
                ]),
            new("falsification-criteria", "Falsification Criteria",
                "The predictions are falsifiable in principle: wrong core profile, no discreteness scale, or no spectrum.",
                [
                    new("Core falsifiable", "A GR-singular core or a different saturation exponent falsifies the prediction.", TheoryBadge.Derived, ["ATQG752"]),
                    new("Free-parameter caveat", "Free rc weakens current falsification power.", TheoryBadge.Partial, ["ATQG752", "ATQG692"]),
                ]),
        ]);

    // ── Section 11 — Status ───────────────────────────────────────────────────────

    private static readonly TheorySection Status = new(
        Slug: "status",
        Title: "Part X — Status",
        Subtitle: "Derived, Compatible, Postulated, Open Problems, Future Research",
        Summary:
            "AT's status: GR is derived (spin-2); QM, gauge, fermions, and the SM are compatible via new sectors; " +
            "cosmology is partially compatible. The open problems are the empirical values of (ℓ,τ,ℏ), the SM " +
            "parameter values, structure formation, and dark energy. 330 AT-QG phases (876+ tests) are verified " +
            "at 72.6% weighted coverage; the canonical architecture is Difference → Actualization → Spectrum → Physics.",
        KeyResults:
        [
            new("GR derived", "GR (spin-2) is DERIVED in the completeness audit.", TheoryBadge.Derived, ["ATQG760"]),
            new("QM/SM compatible", "QM, gauge, fermions, SM are COMPATIBLE via new sectors (θ, S, J, C).", TheoryBadge.Compatible, ["ATQG761"]),
            new("334 phases verified", "334 AT-QG phases, 876+ tests — verified (72.6% weighted coverage).", TheoryBadge.Match, ["ATQG3300"]),
        ],
        Chapters:
        [
            new("derived-results", "Derived Results",
                "The derived backbone: causal order, ρ, geometry, scalar gravity, matter, saturation, energy, expansion.",
                [
                    new("Derived chain", "Q-events → ρ → geometry → gravity; ρ → matter; energy = Noether conjugate.", TheoryBadge.Derived, ["ATQG531", "ATQG892"]),
                    new("Scalar backbone", "Saturation + redshift + flat curves + regular cores are derived.", TheoryBadge.Derived, ["ATQG590"]),
                ]),
            new("compatible-results", "Compatible Results",
                "QM, gauge, fermions, SM, and cosmology are compatible with the network — representable without contradiction.",
                [
                    new("SM compatibility", "Charge natural, gauge compatible, fermions unknown (new primitive).", TheoryBadge.Compatible, ["ATQG600", "ATQG602"]),
                    new("Cosmology", "Expansion derived, FRW/CMB/dark-matter compatible.", TheoryBadge.Compatible, ["ATQG770"]),
                ]),
            new("postulates", "Postulates",
                "The postulatory content: (ℓ,τ,ℏ) values, SM parameters, family count, color count, gauge splitting, and the quantum sectors (θ, S, J).",
                [
                    new("Scale triad", "(ℓ, τ, ℏ) numerical values are empirical.", TheoryBadge.Postulated, ["QG-012", "QG-014"]),
                    new("SM values", "19 parameter values, family count, color count are free inputs.", TheoryBadge.Postulated, ["ATQG850", "ATQG802"]),
                ]),
            new("open-problems", "Open Problems",
                "The open problems: (ℓ,τ,ℏ) values, M² nonlinearity, N_inf, causal-set→GR bridge, 3+1 dimensionality, structure formation, dark energy.",
                [
                    new("Empirical values", "ℓ, τ, ℏ — numerical values not derived.", TheoryBadge.Postulated, ["LabBook-1"]),
                    new("Cosmology gaps", "Structure formation and dark energy remain unknown.", TheoryBadge.Postulated, ["ATQG772"]),
                    new("Measurement", "Full QM collapse generalized but the measurement basis requires full θ+S+J.", TheoryBadge.Partial, ["ATQG742"]),
                ]),
            new("future-research", "Future Research",
                "Future phases attach by ID: spectrum/parameter mapping, structure formation, dark energy, SM value determination.",
                [
                    new("Spectrum program", "The network-spectrum program (QG104-108) continues toward parameter mapping.", TheoryBadge.Partial, ["ATQG1040", "ATQG1082"]),
                    new("Extension path", "New phases extend chapters by phase ID without structural changes.", TheoryBadge.Derived, ["Architecture"]),
                ]),
        ]);

    /// <summary>The complete ordered list of theory-book sections.</summary>
    public static IReadOnlyList<TheorySection> Sections { get; } =
    [
        Introduction,
        Ontology,
        Gravity,
        Tensor,
        Quantum,
        Matter,
        Gauge,
        Cosmology,
        Parameters,
        Predictions,
        Status,
    ];

    /// <summary>Look up a section by its route slug (case-insensitive).</summary>
    public static TheorySection? GetSection(string slug)
        => Sections.FirstOrDefault(s => string.Equals(s.Slug, slug, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Claim-status registry for major AT claims, mirroring
    /// Docs/Research/ATQG_ClaimClassificationRegistry.md. UI-only display data.
    /// </summary>
    public static IReadOnlyList<ClaimStatusInfo> ClaimStatuses { get; } =
    [
        new("N=96", ClaimStatus.Necessity, "Unique within the accepted structural class (period-3 seed, Z2 half-shift, three-family octave window, tested set); a global proof is not claimed."),
        new("D96 spectrum", ClaimStatus.Theorem, "Eigenspectrum of the canonical attractor graph C96(±1..±6); reproduces multiplicities [42×2,5,6], 95+1 modes, moments, and span 6.40 exactly."),
        new("Moment hierarchy", ClaimStatus.Theorem, "Exact spectral values (Σ√m, Σm, Σm², occMom); the assignment of sector access roles is a supported mapping, not a unique derivation."),
        new("Sector mappings", ClaimStatus.Correspondence, "Supported assignments over the forced moment ladder; not a globally unique mapping."),
        new("1+3+8 dimensions", ClaimStatus.Correspondence, "D96 sector counts supply a 1+3+8 partition (1 background, 3 octaves, 8 light modes) matching dim U(1)+dim SU(2)+dim SU(3) = 12 — a dimensional correspondence."),
        new("Gauge groups U(1)×SU(2)×SU(3)", ClaimStatus.Hosted, "The gauge groups and their Lie algebras are hosted; the D96 structure provides only the dimensional correspondence 1+3+8."),
        new("CKM", ClaimStatus.Correspondence, "Spectral ratios matched to observation (0.58% deviation); no free constant; the ratio forms are selected."),
        new("PMNS", ClaimStatus.Correspondence, "T3-only spectral reads matched to observation (1.5% deviation); secondary catalog match."),
        new("Neutrino splittings", ClaimStatus.Correspondence, "Closed-form D96 ratios (Δm²21, Δm²31); the eV² units are calibrated."),
        new("Higgs mass", ClaimStatus.Calibration, "Calibrated reconstruction via the anchor v (blind reconstruction, natural-core status); σ_occ = 39.127 (√Var[4,4,87], occupation-density scalar) defined at first use."),
        new("Couplings", ClaimStatus.Correspondence, "1/α_em is a post-hoc fit (no defined renormalization scale); α_weak and α_strong correspond to spectral ratios."),
        new("Gravity", ClaimStatus.Calibration, "D96 natural-unit content × the anchor v, plus SI conversion; black-hole relations import the flat-rotation-curve profile."),
        new("Spacetime", ClaimStatus.Hosted, "The conformal factor and dynamics are derived from ρ; the metric tensor and its signature are primitive inputs via η."),
        new("CMB peak existence", ClaimStatus.Theorem, "A first peak exists structurally: the fundamental doublet is isolated by the dominant spectral gap."),
        new("CMB peak location", ClaimStatus.Fit, "ℓ₁ = 220.48 requires the 5/4 factor — a fitted multiplier (QG297), not a derivation."),
        new("Peak ratios", ClaimStatus.Correspondence, "Pure spectral ratios (r21=2.4368, r31=3.6965), no fitted constant; specific ratio forms selected."),
    ];
}
