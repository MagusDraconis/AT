using TQM.App.Models;

namespace TQM.App.Services;

/// <summary>
/// Strongly typed content for the TQM Theory Book. Each section, chapter, and result is built from the
/// completed TQM-QG phases (attached by phase ID), so future phases can extend any chapter without
/// structural changes. All content is drawn from the TQM-QG program (Docs/Research).
/// </summary>
public static class TheoryBookDataService
{
    // ── Section 1 — Introduction ──────────────────────────────────────────────────

    private static readonly TheorySection Introduction = new(
        Slug: "introduction",
        Title: "Introduction",
        Subtitle: "What is TQM?",
        Summary:
            "TQM now frames the program around the minimal hierarchy Difference → Actualization → Spectrum → Physics. " +
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
            new("what-is-tqm", "What is TQM?",
                "TQM is now presented as a hierarchy book: the minimal base is Difference → Actualization → Spectrum → Physics, and the familiar Q-event layer is a derived stage of that chain.",
                [
                    new("Minimal base", "Difference → Actualization → Spectrum → Physics is the newest base layer.", TheoryBadge.Derived, ["QG294", "QG295"]),
                    new("No q-events-first", "Q-events are downstream, not the primitive root.", TheoryBadge.Derived, ["QG296"]),
                ]),
            new("historical-path", "Historical Path (TRM → TQM)",
                "TQM's predecessor, the Temporal Resonance Model (TRM), assembled striking pieces but carried zero candidate physics. TQM re-derived the surviving structure from minimal primitives.",
                [
                    new("Legacy audit", "TRM modules: three absorbed, two rejected, three kept as candidate mathematics.", TheoryBadge.Derived, ["QG-029", "QG-031"]),
                    new("Re-derivation", "TQM re-derived surviving structure from the minimal hierarchy, with Q-events placed downstream.", TheoryBadge.Derived, ["QG294", "QG296"]),
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
            new("Causal order derived", "Full causal order = transitive closure of the generation relation.", TheoryBadge.Derived, ["TQMQG110", "TQMQG111", "TQMQG112"]),
            new("One network primitive", "(V,E) unifies the downstream Q-event layer and psi into one causal-network representation.", TheoryBadge.Derived, ["TQMQG550", "TQMQG551", "TQMQG552"]),
            new("Energy derived", "Energy = Noether conjugate of causal order; measured as actualization rate.", TheoryBadge.Derived, ["TQMQG890", "TQMQG891", "TQMQG892"]),
        ],
        Chapters:
        [
            new("q-events", "Q-Events",
                "A Q-event is a derived actualization tick — one local time-state change inside the downstream network picture.",
                [
                    new("Transition picture", "All 4 transition pictures score 4/4; a bare primitive point fails (a static point cannot happen).", TheoryBadge.Derived, ["TQMQG290", "TQMQG292"]),
                    new("Not primitive anymore", "Q-events are downstream of Difference → Actualization → Spectrum → Physics.", TheoryBadge.Derived, ["QG294", "QG296"]),
                ]),
            new("actualization", "Actualization",
                "Actualization is the process layer that generates the downstream event picture. Entropy maximization (α=0) selects the uniform per-octave attractor.",
                [
                    new("α=0 attractor", "Uniform per-octave increments A_k=m₀/K accumulate to the log-deficit density ρ.", TheoryBadge.Derived, ["TQMQG00"]),
                    new("Criticality", "μ=1 is the unique scale-free branching point (L=1/|ln μ| infinite only at criticality).", TheoryBadge.Derived, ["TQMQG12", "TQMQG72"]),
                    new("Irreducibility", "Actualization remains a fundamental layer in the minimal hierarchy.", TheoryBadge.Derived, ["QG294"]),
                ]),
            new("causal-order", "Causal Order",
                "Causal order is not primitive: it is the transitive closure of the generation relation (event → descendants).",
                [
                    new("Derived partial order", "Ancestor relation is irreflexive + antisymmetric + transitive — a strict partial order.", TheoryBadge.Derived, ["TQMQG110"]),
                    new("Generation primitive", "Generation still supplies the downstream event sequence, but it now sits below the new minimal base.", TheoryBadge.Postulated, ["TQMQG112"]),
                ]),
            new("nodes-and-links", "Nodes and Links",
                "The network (V,E) is ONE primitive. Nodes carry spin-0 content; links carry spin-2 (Weyl) + U(1) phase + SU(2) spin.",
                [
                    new("Unified network", "Q-events (spin-0) and psi (spin-2) unify into one causal-network primitive with dual interior.", TheoryBadge.Derived, ["TQMQG552"]),
                    new("One link, three sectors", "Complete link = single complex rank-2 object L_ij = a_ij e^(iθ_ij): magnitude (trace+traceless) + phase.", TheoryBadge.Derived, ["TQMQG640", "TQMQG641", "TQMQG642"]),
                    new("Four irreducible sectors", "ρ (spin-0), ψ (spin-2), θ (U(1)), S (SU(2)) — sectors of one link, not separate primitives.", TheoryBadge.Derived, ["TQMQG680", "TQMQG682"]),
                ]),
            new("energy-from-actualization", "Energy from Actualization",
                "Energy is not a new sector: it is the conserved generator of time translation — the Noether conjugate of causal-order evolution, measured as actualization rate.",
                [
                    new("Derived concept", "Network time = causal order; energy = its conjugate (actualization activity).", TheoryBadge.Derived, ["TQMQG890"]),
                    new("Storage", "Energy stored in ψ/ρ excitation; E=mc² links the Higgs condensate (rest mass).", TheoryBadge.Compatible, ["TQMQG891"]),
                    new("Value caveat", "Energy VALUES (Hamiltonian, masses) remain empirical (QG85).", TheoryBadge.Postulated, ["TQMQG892"]),
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
            new("ρ from actualization", "α=0 attractor → log-deficit ρ → all four gravity requirements reproduced.", TheoryBadge.Match, ["TQMQG00", "TQMQG01", "TQMQG02"]),
            new("G derived (scale)", "GM_eff = m₀r₀/(d·ρ̄) — the gravitational scale is deficit abundance, not imported.", TheoryBadge.Derived, ["TQMQG60", "TQMQG62"]),
            new("Mercury perihelion", "ρ+ψ unified network restores γ=β=+1 → 42.98 \"/century (MATCH via the ψ graviton).", TheoryBadge.Match, ["TQMQG1030", "TQMQG1031", "TQMQG1032"]),
        ],
        Chapters:
        [
            new("counting-measure", "Counting Measure ρ",
                "ρ is the per-vertex event density (counting measure). Entropy maximization selects the log-deficit profile ρ = ρ̄ − m₀·ln(R_max/r)/ln(R_max/r₀).",
                [
                    new("α=0 attractor", "Uniform per-octave increments accumulate to exactly the log-deficit density.", TheoryBadge.Derived, ["TQMQG00", "TQMQG10"]),
                    new("Microscopic origin", "Critical branching (μ=1) → uniform per-octave counts → log-deficit ρ, exactly.", TheoryBadge.Derived, ["TQMQG11", "TQMQG12"]),
                ]),
            new("geometry-emergence", "Geometry Emergence",
                "The metric emerges from ρ: g = ρ^(2/d)·η (conformally flat). sqrt(−g) = ρ gives the metric origin; Malament-type causal order fixes the conformal class.",
                [
                    new("Metric origin", "sqrt(−g) = ρ > 0 — the counting measure is the volume element.", TheoryBadge.Derived, ["TQMQG01"]),
                    new("Conformal structure", "The conformal factor f = ρ^(2/d) induces the conformally-flat metric.", TheoryBadge.Derived, ["QG-022"]),
                ]),
            new("scalar-gravity", "Scalar Gravity",
                "The conformal sector is scalar gravity: a = −(1/d)∇ln ρ has no free coupling. Attraction dominates (phase-gradient +∇θ); repulsion is locally unstable.",
                [
                    new("No free coupling", "The conformal acceleration is fixed by d and the ρ profile.", TheoryBadge.Derived, ["TQMQG60"]),
                    new("Attraction only", "Repulsive gravity is unstable locally; Dark Energy is the sole metastable exception.", TheoryBadge.Derived, ["QG-029", "QG-031"]),
                ]),
            new("regular-cores", "Regular Cores",
                "Deficit matter saturates: M_eff(r) = M(1 − e^(−r³/r_c³)) — a regular core with M_eff(0)=0, the first quantitative prediction.",
                [
                    new("Saturation profile", "Poisson saturation of the deficit gives a finite regular core.", TheoryBadge.Derived, ["TQMQG750"]),
                    new("Unique prediction", "Distinct from GR, Hayward, and Bardeen profiles — testable via shadow/ISCO.", TheoryBadge.Match, ["TQMQG751", "TQMQG752"]),
                ]),
            new("rotation-curves", "Rotation Curves",
                "The log-deficit ρ produces flat rotation curves (v²(3)/v²(9) = 1.18) without dark matter — the RAR/MOND-like regime emerges from the counting measure.",
                [
                    new("Flat curves", "Log-deficit density → flat rotation-curve ratio ≈ 1.18.", TheoryBadge.Match, ["TQMQG01", "TQMQG11"]),
                    new("RAR link", "g† = c·H₀/(2π) derived with zero free parameters (DATA program).", TheoryBadge.Match, ["DATA-003", "DATA-004"]),
                ]),
            new("mercury-perihelion", "Mercury Perihelion",
                "The unified network recovers Mercury's 42.98 \"/century perihelion advance through the ψ spin-2 graviton.",
                [
                    new("GR baseline", "γ=β=1 → factor 1 → 42.98 \"/century.", TheoryBadge.Match, ["TQMQG1030"]),
                    new("ρ-only limit", "Conformal γ=−1 → factor −1/3 → RETROGRADE −14.33 \"/century; this marks the scalar-only limit, not the final theory.", TheoryBadge.Partial, ["TQMQG1031"]),
                    new("ρ+ψ matches", "The ψ graviton restores γ=β=+1 → +42.98 \"/century (MATCH).", TheoryBadge.Match, ["TQMQG1032"]),
                ]),
            new("schwarzschild-limit", "Schwarzschild Limit",
                "Horizon structure: entropy S ~ Area from horizon counting (area law). Mass-radius relation diverges from Schwarzschild (deficit mass ~ R^d vs M ~ R).",
                [
                    new("Area law", "S = A·ln2 ~ R^(d−1) — entropy scales with area.", TheoryBadge.Match, ["TQMQG120", "TQMQG121"]),
                    new("Temperature mismatch", "Native T~R (anti-Hawking) is a mismatched scaling for the Schwarzschild limit, while the deficit-mass picture remains intact.", TheoryBadge.Partial, ["TQMQG131", "TQMQG132"]),
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
            new("ψ = link Weyl content", "psi is the non-conformal Weyl content of the causal link structure — a rank-2 field, not an external addition.", TheoryBadge.Derived, ["TQMQG540", "TQMQG541", "TQMQG542"]),
            new("Spin-2 uniquely selected", "2 polarizations rule out spin-0; universal attraction rules out spin-1; only spin-2 passes.", TheoryBadge.Preferred, ["TQMQG460", "TQMQG462"]),
            new("Lensing requires ψ", "Conformal γ=−1 kills all lensing observables; a non-conformal (ψ) sector is required.", TheoryBadge.Partial, ["TQMQG260", "TQMQG262"]),
        ],
        Chapters:
        [
            new("why-psi-exists", "Why ψ Exists",
                "Q-events alone cannot produce lensing, Shapiro delay, PPN γ=+1, or GW polarization. ψ is the minimal new postulate required by observational completeness.",
                [
                    new("Scalar insufficiency", "The scalar universe is self-consistent but cannot reproduce 4 observations.", TheoryBadge.Derived, ["TQMQG470", "TQMQG471"]),
                    new("New postulate", "ψ is motivated by observation (GW + bending), preferred in form, not forced by consistency.", TheoryBadge.Postulated, ["TQMQG472"]),
                ]),
            new("spin-2-selection", "Spin-2 Selection",
                "Three independent constraints uniquely select spin-2 as the gravitational extension.",
                [
                    new("Minimal extension", "2 graviton helicities = the minimal additional d.o.f. (max(1,2,0)).", TheoryBadge.Derived, ["TQMQG240", "TQMQG241", "TQMQG242"]),
                    new("Preferred form", "Massless spin-2 (Fierz–Pauli) is the unique ghost-free theory.", TheoryBadge.Derived, ["TQMQG440", "TQMQG442"]),
                    new("Why not 0/1", "Spin-0 fails polarization + full T_μν; spin-1 is repulsive.", TheoryBadge.Derived, ["TQMQG460", "TQMQG461"]),
                ]),
            new("weyl-content", "Weyl Content",
                "The traceless (Weyl) part of the complete link relation is the spin-2 sector — forced as capacity, contingent in value.",
                [
                    new("Forced capacity", "A complete link carries trace + traceless; conformal-only links are the Weyl=0 restriction.", TheoryBadge.Derived, ["TQMQG560", "TQMQG562"]),
                    new("Excitation", "Quadrupole (traceless) sources excite Weyl — the mechanism is derived.", TheoryBadge.Derived, ["TQMQG570", "TQMQG572"]),
                ]),
            new("gravitational-waves", "Gravitational Waves",
                "The scalar sector has only a breathing (monopole) mode — invisible to Michelson interferometers. Observed +/× GWs are tensor (spin-2), requiring ψ.",
                [
                    new("Scalar invisible", "Breathing mode is common-mode → zero differential strain.", TheoryBadge.Derived, ["TQMQG200", "TQMQG201", "TQMQG202"]),
                    new("No fake tensor", "No scalar (collective or otherwise) can source spin-2; psi remains required.", TheoryBadge.Derived, ["TQMQG490", "TQMQG491", "TQMQG492"]),
                    new("GW observation", "Only the raw strain is direct; spin-2 is a model-dependent reconstruction.", TheoryBadge.Partial, ["TQMQG480", "TQMQG481", "TQMQG482"]),
                ]),
            new("lensing-ppn", "Lensing & PPN",
                "Conformally-flat g=ρ^(2/d)η has PPN γ=−1: deflection, convergence, shear, and Shapiro delay all vanish. Only gravitational redshift survives.",
                [
                    new("γ=−1 sector", "All lensing observables scale as (1+γ)/2 and vanish at γ=−1.", TheoryBadge.Derived, ["TQMQG260", "TQMQG261"]),
                    new("Redshift survives", "z = (ρ₂/ρ₁)^(1/d) − 1 (g_00 alone) — gravitational redshift is present.", TheoryBadge.Match, ["TQMQG261"]),
                    new("Restoration via ψ", "A non-conformal (ψ) sector moves γ off −1 and restores lensing.", TheoryBadge.Partial, ["TQMQG22", "TQMQG262"]),
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
            new("Interference from links", "Double-slit |e^{iθ₁}+e^{iθ₂}|² = 2+2cos(θ₁−θ₂) — interference recovered from link phases.", TheoryBadge.Match, ["TQMQG650", "TQMQG651"]),
            new("Measurement = actualization", "A Q-event is a Born-weighted projection (collapse to a definite state).", TheoryBadge.Partial, ["TQMQG730", "TQMQG732"]),
            new("Entanglement needs J", "Non-separable correlations need a joint (2-qubit) link state — a new sector beyond θ+S.", TheoryBadge.Postulated, ["TQMQG710", "TQMQG711", "TQMQG712"]),
        ],
        Chapters:
        [
            new("phase-theta", "Phase θ",
                "The U(1) phase lives on links (gauge phase home), matter phases on nodes, and loop holonomies are derived.",
                [
                    new("Phase home", "Links are the canonical gauge-phase home; Wilson loops derived.", TheoryBadge.Derived, ["TQMQG630", "TQMQG632"]),
                    new("Amplitude primitive", "The complex amplitude (U(1) phase) is a new d.o.f. — compatible, not emergent.", TheoryBadge.Postulated, ["TQMQG620", "TQMQG622"]),
                ]),
            new("interference", "Interference",
                "Path phase accumulation gives interference: a natural consequence of link phases GIVEN the θ primitive.",
                [
                    new("Double-slit", "|e^{iθ₁}+e^{iθ₂}|² reproduces constructive/destructive interference.", TheoryBadge.Match, ["TQMQG651"]),
                    new("Holonomy invariant", "Loop holonomy is gauge-invariant; |e^{iθ}|=1.", TheoryBadge.Derived, ["TQMQG650"]),
                ]),
            new("born-rule", "Born Rule",
                "P = |amplitude|² is consistent with the actualization picture (probability = actualization density).",
                [
                    new("Consistent", "Born rule P=|amplitude|² follows from link-phase amplitudes.", TheoryBadge.Match, ["TQMQG652"]),
                    new("Actualization density", "|ψ|² = actualization density (QM-001 derivation).", TheoryBadge.Derived, ["QM-001"]),
                ]),
            new("entanglement", "Entanglement",
                "Shared fixed phases give CLASSICAL correlations only. Quantum non-separability requires an entangling sector (joint link states).",
                [
                    new("Classical only", "Fixed phases → deterministic correlations, not Bell non-separability.", TheoryBadge.Derived, ["TQMQG700"]),
                    new("Joint link state", "The minimal addition is a joint (2-qubit) link state — new content.", TheoryBadge.Postulated, ["TQMQG711"]),
                ]),
            new("bell-correlations", "Bell Correlations",
                "Superposition, interference, Born rule, entanglement, and Bell correlations are complete given θ+S+J; only collapse was missing.",
                [
                    new("Sector audit", "5/6 quantum features complete with θ+S+J.", TheoryBadge.Derived, ["TQMQG720"]),
                    new("Collapse gap", "The single missing piece was the measurement collapse — resolved by actualization.", TheoryBadge.Partial, ["TQMQG721", "TQMQG722"]),
                ]),
            new("measurement", "Measurement",
                "Measurement collapse is identified with Q-event actualization: a Born-weighted projection to a definite state.",
                [
                    new("Collapse = tick", "A Q-event is a discrete Born-weighted projection.", TheoryBadge.Derived, ["TQMQG730"]),
                    new("General bases", "Arbitrary measurement bases reproduced via unitary rotation (θ+S+J); POVMs via Naimark dilation.", TheoryBadge.Match, ["TQMQG741", "TQMQG742"]),
                ]),
            new("actualization-collapse", "Actualization = Collapse",
                "The measurement problem resolves: actualization IS the collapse, no separate mechanism needed.",
                [
                    new("No extra postulate", "Collapse = actualization; no separate collapse mechanism (QM-004).", TheoryBadge.Derived, ["QM-004"]),
                    new("Binary limit", "Initially a binary projection; generalized to arbitrary bases by the full quantum structure.", TheoryBadge.Partial, ["TQMQG731", "TQMQG732"]),
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
            new("Fermions need new primitive", "Spin-1/2 spinors require a spin structure (SU(2) double cover) — compatible, not derivable.", TheoryBadge.Postulated, ["TQMQG660", "TQMQG662"]),
            new("Family replication compatible", "A degenerate family index on the node/link hosts replication; the count 3 stays postulatory.", TheoryBadge.Compatible, ["TQMQG810", "TQMQG812"]),
            new("Higgs compatible", "Mass generation via a ρ condensate is representable; the VEV and couplings are postulated.", TheoryBadge.Compatible, ["TQMQG840", "TQMQG842"]),
        ],
        Chapters:
        [
            new("spin-structure", "Spin Structure S",
                "The link carries an SU(2) spin sector. Graph orientation (Z2) is NOT a spin structure.",
                [
                    new("SU(2) sector", "The link's spin sector S hosts the SU(2) representation.", TheoryBadge.Derived, ["TQMQG680"]),
                    new("Not derivable", "Spin structure (double cover) is new data — compatible, not native.", TheoryBadge.Postulated, ["TQMQG670", "TQMQG672"]),
                ]),
            new("fermions", "Fermions",
                "Spin-1/2 fermions need a new spin-1/2 primitive; the network alone hosts integer spins only.",
                [
                    new("Integer spins native", "Spin-0 (nodes), spin-2 (links), spin-1 (gauge) are native.", TheoryBadge.Derived, ["TQMQG660"]),
                    new("Half-integer new", "Spinor = section of a spin bundle — not derivable from scalar+rank-2.", TheoryBadge.Postulated, ["TQMQG661", "TQMQG662"]),
                ]),
            new("family-index", "Family Index",
                "Replication is accommodated by a degenerate discrete family index — no new primitive needed for existence.",
                [
                    new("No topological families", "No topological invariant produces families; spin gives a single rep.", TheoryBadge.Derived, ["TQMQG800", "TQMQG810"]),
                    new("Count is postulatory", "The 3-generation count is a postulate, coincidental with color.", TheoryBadge.Postulated, ["TQMQG802"]),
                ]),
            new("ckm-pmns", "CKM / PMNS",
                "Once the family index exists, mixing is a unitary rotation between flavor and mass bases — representable, entries free.",
                [
                    new("Rotation picture", "Mixing = rotation between flavor and mass bases (family-index dynamics).", TheoryBadge.Compatible, ["TQMQG820", "TQMQG821"]),
                    new("Angles free", "CKM (3 angles + 1 phase) and PMNS are representable; specific entries are free inputs.", TheoryBadge.Postulated, ["TQMQG822"]),
                ]),
            new("higgs-compatibility", "Higgs Compatibility",
                "The scalar ρ already exists; a link condensate can serve as the VEV. Mass generation is representable but not derived.",
                [
                    new("ρ as VEV", "Scalar ρ (node occupancy, spin-0) is already derived; a condensate serves as the VEV.", TheoryBadge.Compatible, ["TQMQG840"]),
                    new("Additional content", "The symmetry-breaking potential and Yukawa couplings are postulated.", TheoryBadge.Postulated, ["TQMQG841", "TQMQG842"]),
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
            new("Gauge product structure", "θ, S, C act on different internal spaces → gauge group is the PRODUCT U(1)×SU(2)×SU(3).", TheoryBadge.Postulated, ["TQMQG900", "TQMQG902"]),
            new("SU(3) forced given N=3", "Given 3 colors, SU(3) with 8 gluons is forced/unique; the count 3 is the postulate.", TheoryBadge.Preferred, ["TQMQG791", "TQMQG792"]),
            new("Gauge splitting empirical", "The three sectors are independent postulates; a GUT is additional.", TheoryBadge.Postulated, ["TQMQG901", "TQMQG902"]),
        ],
        Chapters:
        [
            new("u1", "U(1)",
                "The U(1) phase θ lives on links as the charge sector — the gauge-phase home.",
                [
                    new("Charge on links", "Gauge phases live on links (canonical home); matter phases on nodes.", TheoryBadge.Derived, ["TQMQG630", "TQMQG632"]),
                    new("From the circle", "Phase lives on S¹; its isometry group is U(1) (ResearchX Atlas).", TheoryBadge.Derived, ["KeyDiscovery"]),
                ]),
            new("su2", "SU(2)",
                "The SU(2) spin sector S hosts spin and weak isospin structure.",
                [
                    new("Spin sector", "The link's S sector is SU(2) — the smallest non-Abelian group.", TheoryBadge.Derived, ["TQMQG680"]),
                    new("Real-underived", "SU(2) structure is real-underived in the TQM taxonomy.", TheoryBadge.Partial, ["Taxonomy"]),
                ]),
            new("su3", "SU(3)",
                "Color charge can be hosted by an SU(3) connection on the link (lattice QCD analog); the count 3 is empirical.",
                [
                    new("Connection on links", "The link can carry an SU(3) connection; gluons and Wilson loops are SU(3) analogues.", TheoryBadge.Compatible, ["TQMQG780", "TQMQG781"]),
                    new("Count is input", "Color count N=3 is empirical (baryon statistics), not a network output.", TheoryBadge.Postulated, ["TQMQG791"]),
                ]),
            new("why-gauge-splitting", "Why Gauge Splitting?",
                "θ, S, C act on different internal spaces, so the gauge group is the product — there is no derived unified group.",
                [
                    new("Independent spaces", "U(1), SU(2), SU(3) act on charge, spin, color — distinct internal spaces.", TheoryBadge.Derived, ["TQMQG900"]),
                    new("No GUT", "No symmetry-breaking chain derives a unified group; a GUT is additional.", TheoryBadge.Postulated, ["TQMQG901", "TQMQG902"]),
                ]),
            new("open-questions", "Open Questions",
                "The strong force, the color count, and the product structure remain postulatory; SM completeness is an open gap.",
                [
                    new("SM completeness gap", "SU(3), 3 generations, Higgs — compatible but not derived (QG76).", TheoryBadge.Postulated, ["TQMQG760", "TQMQG762"]),
                    new("Gauge values free", "Coupling strengths and gauge parameters are free inputs (QG85).", TheoryBadge.Postulated, ["TQMQG850"]),
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
            new("Expansion derived", "Redshift (QG26) + scale-free ρ → cosmological expansion.", TheoryBadge.Derived, ["TQMQG770"]),
            new("Cosmology audit", "1 derived / 3 compatible / 2 unknown (structure formation, dark energy).", TheoryBadge.Partial, ["TQMQG772"]),
            new("RAR / dark matter", "Log-deficit flat rotation curves — dark-matter effects compatible.", TheoryBadge.Match, ["DATA-003", "TQMQG770"]),
        ],
        Chapters:
        [
            new("expansion", "Expansion",
                "Expansion emerges from redshift (gravitational redshift of the conformal metric) plus scale-free ρ.",
                [
                    new("Derived expansion", "Expansion = redshift + scale-free density (QG-004, QG26).", TheoryBadge.Derived, ["QG-004", "TQMQG770"]),
                    new("Λ(t)", "Λ(t) = α/√V(t) from N(t) growth — the TQM prediction.", TheoryBadge.Derived, ["QG-004"]),
                ]),
            new("frw-compatibility", "FRW Compatibility",
                "The FRW geometry a(t) = ρ^(1/d) is compatible with the network picture.",
                [
                    new("Scale factor", "a = ρ^(1/d) is the FRW-compatible scale factor.", TheoryBadge.Compatible, ["TQMQG770"]),
                    new("Background metric", "Background set by 1-point ρ̄ (conformal, n=1).", TheoryBadge.Compatible, ["TQMQG300"]),
                ]),
            new("cmb-compatibility", "CMB Compatibility",
                "CMB isotropy is compatible with the network; the full CMB spectrum is not re-derived.",
                [
                    new("Isotropy compatible", "CMB isotropy is consistent with the conformal background.", TheoryBadge.Compatible, ["TQMQG770"]),
                    new("Spectrum open", "The detailed CMB spectrum is not derived from the network.", TheoryBadge.Postulated, ["TQMQG762"]),
                ]),
            new("dark-matter-effects", "Dark Matter Effects",
                "Flat rotation curves emerge from the log-deficit density without dark matter; g† = cH₀/(2π) is derived.",
                [
                    new("Flat curves", "v²(3)/v²(9) = 1.18 — flat rotation from the counting measure.", TheoryBadge.Match, ["TQMQG01"]),
                    new("RAR exact", "g† = c·H₀/(2π) with zero free parameters (SPARC-verified).", TheoryBadge.Match, ["DATA-003", "DATA-004"]),
                ]),
            new("structure-formation", "Structure Formation",
                "Structure formation is not yet derived from the network — an open gap.",
                [
                    new("Unknown", "Structure formation is UNKNOWN in the cosmology audit.", TheoryBadge.Postulated, ["TQMQG772"]),
                ]),
            new("dark-energy", "Dark Energy",
                "Metastable repulsive architecture is possible as a cosmological exception; local voids fill at c.",
                [
                    new("DE only", "Repulsive gravity is unstable locally; only the cosmological exception survives.", TheoryBadge.Derived, ["QG-031"]),
                    new("Open gap", "The full dark-energy sector is not yet derived.", TheoryBadge.Postulated, ["TQMQG772"]),
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
            new("Parameters postulated", "19 free SM parameters (+7 for neutrinos): compatible, not derivable.", TheoryBadge.Postulated, ["TQMQG850", "TQMQG852"]),
            new("Every mechanism partial", "Link lengths, ratios, angles, motifs, curvature, resonances → PARTIAL RELATION.", TheoryBadge.Partial, ["TQMQG912", "TQMQG942", "TQMQG992", "TQMQG1002"]),
            new("Spectrum no correspondence", "Network spectra are hierarchical but no ratio numerically matches the SM.", TheoryBadge.Partial, ["TQMQG1042", "TQMQG1082"]),
        ],
        Chapters:
        [
            new("free-parameters", "Free Parameters",
                "The SM has 19 free parameters (+7 with massive neutrinos). Capacity permits, symmetries fix form, values stay free.",
                [
                    new("Count structurally fixed", "The count 19 is fixed by gauge dims + reps + family index.", TheoryBadge.Derived, ["TQMQG861"]),
                    new("Values free", "Masses, couplings, generation count, color count are free inputs.", TheoryBadge.Postulated, ["TQMQG850", "TQMQG852"]),
                ]),
            new("link-lengths", "Link Lengths",
                "Link length IS the network metric; Yukawa/lattice analogies show HOW values could be encoded — exponents stay free.",
                [
                    new("Metric derived", "Link length is derived from ρ — the network metric.", TheoryBadge.Derived, ["TQMQG910"]),
                    new("Encoding compatible", "e^(−m r) suppression is compatible; couplings/angles free.", TheoryBadge.Partial, ["TQMQG911", "TQMQG912"]),
                ]),
            new("ratios", "Ratios",
                "Dimensionless length ratios are scale-invariant and convert to angles via triangle geometry — a direct analog, not a derivation.",
                [
                    new("Scale-invariant", "Length ratios are dimensionless and scale-free.", TheoryBadge.Derived, ["TQMQG970"]),
                    new("Which ratio?", "The network does not specify WHICH ratio maps to WHICH parameter.", TheoryBadge.Partial, ["TQMQG971", "TQMQG972"]),
                ]),
            new("angles", "Angles",
                "CKM/PMNS mixing angles are internal rotations; geometric triangle angles live in spacetime — an analogy across spaces.",
                [
                    new("Real geometric angles", "Triangle + orientation give genuine geometric angles.", TheoryBadge.Derived, ["TQMQG980"]),
                    new("Different spaces", "Internal vs geometric rotations — no native identification.", TheoryBadge.Partial, ["TQMQG981", "TQMQG982"]),
                ]),
            new("motifs", "Motifs",
                "Triangle/loop/branching motifs carry invariants (area, holonomy) — an organizing structure without value selection.",
                [
                    new("Motif spectra", "Motif counts/stability classes provide a structural organizing principle.", TheoryBadge.Derived, ["TQMQG990", "TQMQG991"]),
                    new("Derived composites", "Motifs are derived composites (no independent dof).", TheoryBadge.Partial, ["TQMQG992"]),
                ]),
            new("curvature", "Curvature",
                "Discrete curvature (deficit angle) is real and derived — the G4 spectral extraction object — but values stay free.",
                [
                    new("Deficit angle", "Curvature = 2π − Σ face angles, derived from the metric.", TheoryBadge.Derived, ["TQMQG1000"]),
                    new("Analogy only", "Deficit-angle mass/mixing analogs are suggestive, not determinative.", TheoryBadge.Partial, ["TQMQG1001", "TQMQG1002"]),
                ]),
            new("resonances", "Resonances",
                "The network has normal modes; mass = resonance frequency is a structural analogy. No native dynamics whose spectrum equals the SM.",
                [
                    new("Spectra exist", "Graph Laplacian + stable normal-mode eigenfrequencies are real.", TheoryBadge.Derived, ["TQMQG941", "TQMQG950"]),
                    new("Mapping speculative", "No native operator identified whose spectrum equals the SM parameters.", TheoryBadge.Partial, ["TQMQG942", "TQMQG952"]),
                ]),
            new("global-solution-space", "Global Solution Space",
                "Global consistency carves out a solution-space manifold with parameter correlations — organizing, not determining.",
                [
                    new("Consistency manifold", "Loops, single-valued metric, triangle inequalities → solution space with topology.", TheoryBadge.Derived, ["TQMQG1020"]),
                    new("Non-unique", "Nothing selects a unique solution whose properties equal the SM parameters.", TheoryBadge.Partial, ["TQMQG1021", "TQMQG1022"]),
                ]),
        ]);

    // ── Section 10 — Predictions ──────────────────────────────────────────────────

    private static readonly TheorySection Predictions = new(
        Slug: "predictions",
        Title: "Part IX — Predictions",
        Subtitle: "Network Discreteness, Regular Core, Testable Signatures, Falsification",
        Summary:
            "TQM's unique predictions: a common discreteness scale for all four sectors (network granularity), and the " +
            "regular-core profile M_eff(r)=M(1−e^(−r³/r_c³)). Both are testable and falsifiable in principle; the free " +
            "scale rc makes falsification challenging.",
        KeyResults:
        [
            new("Network discreteness unique", "A COMMON discreteness scale for ρ/ψ/θ/S is the unique TQM prediction absent from GR+SM.", TheoryBadge.Match, ["TQMQG690", "TQMQG692"]),
            new("Regular core unique", "M_eff(r)=M(1−e^(−r³/r_c³)) — distinct from GR, Hayward, Bardeen.", TheoryBadge.Match, ["TQMQG750", "TQMQG751", "TQMQG752"]),
            new("Falsifiable", "Both predictions are testable in principle; free scales limit current falsification power.", TheoryBadge.Partial, ["TQMQG752", "TQMQG692"]),
        ],
        Chapters:
        [
            new("network-discreteness", "Network Discreteness",
                "Spacetime granularity at a common scale is the unique signature of the network — no GR/SM analog.",
                [
                    new("Unique signature", "GW/lensing/BH/quantum all reproduce GR/SM; only network discreteness is unique.", TheoryBadge.Derived, ["TQMQG690", "TQMQG691"]),
                    new("Scale is free", "The discreteness scale is a free parameter (QG14/QG38).", TheoryBadge.Postulated, ["TQMQG691"]),
                ]),
            new("regular-core-prediction", "Regular Core Prediction",
                "The deficit saturates: M_eff(r) = M(1−e^(−r³/r_c³)) with exponent 3 — a finite core with zero central mass.",
                [
                    new("Profile", "Exponent 3 (spatial dimension); M_eff(0)=0, →M asymptotically.", TheoryBadge.Match, ["TQMQG750"]),
                    new("Distinct from GR", "Differs from GR (singular), Hayward, and Bardeen profiles.", TheoryBadge.Match, ["TQMQG751"]),
                ]),
            new("testable-signatures", "Testable Signatures",
                "Shadow, ISCO, lensing, and ringdown can discriminate the regular-core profile.",
                [
                    new("Observables", "Shadow/ISCO/lensing/ringdown are the discriminating observables.", TheoryBadge.Match, ["TQMQG752"]),
                    new("Network spectrum", "Hierarchical discrete spectra are computable (QG104-108) — structural tests.", TheoryBadge.Partial, ["TQMQG1040", "TQMQG1080"]),
                ]),
            new("falsification-criteria", "Falsification Criteria",
                "The predictions are falsifiable in principle: wrong core profile, no discreteness scale, or no spectrum.",
                [
                    new("Core falsifiable", "A GR-singular core or a different saturation exponent falsifies the prediction.", TheoryBadge.Derived, ["TQMQG752"]),
                    new("Free-parameter caveat", "Free rc weakens current falsification power.", TheoryBadge.Partial, ["TQMQG752", "TQMQG692"]),
                ]),
        ]);

    // ── Section 11 — Status ───────────────────────────────────────────────────────

    private static readonly TheorySection Status = new(
        Slug: "status",
        Title: "Part X — Status",
        Subtitle: "Derived, Compatible, Postulated, Open Problems, Future Research",
        Summary:
            "TQM's status: GR is derived (spin-2); QM, gauge, fermions, and the SM are compatible via new sectors; " +
            "cosmology is partially compatible. The open problems are the empirical values of (ℓ,τ,ℏ), the SM " +
            "parameter values, structure formation, and dark energy. 330 TQM-QG phases (876+ tests) are verified " +
            "at 73.2% weighted coverage; the canonical architecture is Difference → Actualization → Spectrum → Physics.",
        KeyResults:
        [
            new("GR derived", "GR (spin-2) is DERIVED in the completeness audit.", TheoryBadge.Derived, ["TQMQG760"]),
            new("QM/SM compatible", "QM, gauge, fermions, SM are COMPATIBLE via new sectors (θ, S, J, C).", TheoryBadge.Compatible, ["TQMQG761"]),
            new("330 phases verified", "330 TQM-QG phases, 876+ tests — verified (73.2% weighted coverage).", TheoryBadge.Match, ["TQMQG3300"]),
        ],
        Chapters:
        [
            new("derived-results", "Derived Results",
                "The derived backbone: causal order, ρ, geometry, scalar gravity, matter, saturation, energy, expansion.",
                [
                    new("Derived chain", "Q-events → ρ → geometry → gravity; ρ → matter; energy = Noether conjugate.", TheoryBadge.Derived, ["TQMQG531", "TQMQG892"]),
                    new("Scalar backbone", "Saturation + redshift + flat curves + regular cores are derived.", TheoryBadge.Derived, ["TQMQG590"]),
                ]),
            new("compatible-results", "Compatible Results",
                "QM, gauge, fermions, SM, and cosmology are compatible with the network — representable without contradiction.",
                [
                    new("SM compatibility", "Charge natural, gauge compatible, fermions unknown (new primitive).", TheoryBadge.Compatible, ["TQMQG600", "TQMQG602"]),
                    new("Cosmology", "Expansion derived, FRW/CMB/dark-matter compatible.", TheoryBadge.Compatible, ["TQMQG770"]),
                ]),
            new("postulates", "Postulates",
                "The postulatory content: (ℓ,τ,ℏ) values, SM parameters, family count, color count, gauge splitting, and the quantum sectors (θ, S, J).",
                [
                    new("Scale triad", "(ℓ, τ, ℏ) numerical values are empirical.", TheoryBadge.Postulated, ["QG-012", "QG-014"]),
                    new("SM values", "19 parameter values, family count, color count are free inputs.", TheoryBadge.Postulated, ["TQMQG850", "TQMQG802"]),
                ]),
            new("open-problems", "Open Problems",
                "The open problems: (ℓ,τ,ℏ) values, M² nonlinearity, N_inf, causal-set→GR bridge, 3+1 dimensionality, structure formation, dark energy.",
                [
                    new("Empirical values", "ℓ, τ, ℏ — numerical values not derived.", TheoryBadge.Postulated, ["LabBook-1"]),
                    new("Cosmology gaps", "Structure formation and dark energy remain unknown.", TheoryBadge.Postulated, ["TQMQG772"]),
                    new("Measurement", "Full QM collapse generalized but the measurement basis requires full θ+S+J.", TheoryBadge.Partial, ["TQMQG742"]),
                ]),
            new("future-research", "Future Research",
                "Future phases attach by ID: spectrum/parameter mapping, structure formation, dark energy, SM value determination.",
                [
                    new("Spectrum program", "The network-spectrum program (QG104-108) continues toward parameter mapping.", TheoryBadge.Partial, ["TQMQG1040", "TQMQG1082"]),
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
}
