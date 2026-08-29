using AT.App.Models;

namespace AT.App.Services;

/// <summary>
/// Strongly typed, in-memory data for the AT v1.0 research atlas.
/// All content is drawn from AT_v1_0_Monograph_Final (no new physics).
/// </summary>
public static class AtlasDataService
{
    public static IReadOnlyList<string> WhyQuestions { get; } =
    [
        "The Standard Model explains much.",
        "But why these laws?",
        "Why these symmetries?",
        "Why these dimensions?",
        "Why these constants?",
    ];

    public static IReadOnlyList<KeyDiscoveryModel> KeyDiscoveries { get; } =
    [
        new("U(1) from the circle", "Phase lives on S¹; its isometry group is U(1).", ClassificationKind.Derived),
        new("Schrödinger from reversibility", "i∂_t ψ = L_Q ψ follows from norm conservation.", ClassificationKind.Derived),
        new("3+1 from complexity", "The unique dimension satisfying five independent constraints.", ClassificationKind.Derived),
        new("The continuum limits", "L_Q → −∇² and BDG → □, each controlled and tested.", ClassificationKind.Derived),
        new("Koide Q = 2/3", "Real, predictive, RG-stable — and underivable.", ClassificationKind.RealUnderived),
        new("The open door", "The internal-3 node; G4; the discriminating prediction.", ClassificationKind.Partial),
    ];

    public static IReadOnlyList<StoryChapterModel> StoryChapters { get; } =
    [
        new("TRM", "Temporal Resonance Model",
            "The predecessor framework: Time Field, RAR, Frame Dragging, Memory Channel, Theta Chain, m=3 Closure, Quantum Engine, Temporal Drift, Unified Action.",
            "bi-diagram-2", "blue"),
        new("Problems Found", "The legacy audit",
            "TRM carried zero candidate physics: tired-light cosmology falsified, the Quantum Engine non-unitary.",
            "bi-exclamation-triangle", "red"),
        new("ResearchX", "Alternative foundations",
            "Reversibility plus self-consistency is minimally sufficient for unitary quantum mechanics; quantum reality is the unique global maximum of finite complexity.",
            "bi-lightbulb", "green"),
        new("Structure/Content Split", "The boundary",
            "Structure is derivable; content is realized. The three-category taxonomy (DERIVED, REAL-UNDERIVED, DRAWN) is born.",
            "bi-columns-gap", "amber"),
        new("Hostile Reviews", "Framing corrections",
            "Round 1 and Round 2: not retracted physics, but overstatements downgraded — 6 resolved, 6 partially resolved, 0 open.",
            "bi-shield-check", "orange"),
        new("AT v1.0", "Release",
            "Two primitives plus one number. READY AS A FOUNDATION MONOGRAPH.",
            "bi-rocket-takeoff", "cyan"),
    ];

    public static IReadOnlyList<DerivationNodeModel> DerivationRoots { get; } = BuildDerivationGraph();

    public static IReadOnlyList<TestGroupModel> TestGroups { get; } =
    [
        new("Continuum",
        [
            new("GraphLaplacianContinuumTests", 1, "PASS", "Continuum"),
            new("BDGOperatorContinuumTests", 1, "PASS", "Continuum"),
        ]),
        new("Schrödinger",
        [
            new("WeightedLaplacianTests", 4, "PASS", "Schrödinger"),
            new("LaplaceBeltramiTests", 3, "PASS", "Schrödinger"),
            new("CurvedSchrodingerTests", 3, "PASS", "Schrödinger"),
        ]),
        new("Bridge",
        [
            new("QuantumGravityBridgeTests", 3, "PASS", "Bridge"),
            new("CurvedSpaceBridgeTests", 3, "PASS", "Bridge"),
        ]),
        new("Einstein",
        [
            new("EinsteinRecoveryTests", 3, "PARTIAL", "Einstein"),
            new("EinsteinTensorTests", 4, "PASS", "Einstein"),
            new("EinsteinTensorIntegrationTests", 4, "PASS", "Einstein"),
        ]),
        new("Metric",
        [
            new("MetricOperatorTests", 4, "PASS", "Metric"),
            new("MetricGenerationTests", 4, "PASS", "Metric"),
            new("MetricEmergenceTests", 4, "PASS", "Metric"),
            new("ConformalStructureTests", 3, "PASS", "Metric"),
            new("MetricOriginTests", 3, "PASS", "Metric"),
        ]),
    ];

    public static IReadOnlyList<ResearchNewsModel> ResearchNews { get; } =
    [
        new(
            "researchy-d032-pairing-requirement",
            "Complete Z2 Pairing Is the Observable-Sector Requirement, Not an Accident",
            "ResearchY Milestone · D_032 (Wave Geometry Program)",
            "Why must the observable sector have 0 unpaired modes? The mirror-pairing structure itself is derived from oscillation, but its COMPLETENESS (every frequency sitting in a doublet) is the observable-sector requirement — the boundary input. Everything downstream (p=3, N=96) follows from it.",
            "D_032 analyzed the self-conjugate mode k=N/2: at this antipodal harmonic sin(πn) = 0, so only the cos quadrature survives. Complete pairing (0 unpaired) requires this mode to sit in a degenerate group — λ(N/2) = 12 is 5-fold at N=96/192 (k = 16, 32, 48, 64, 80 share it) but 1-fold at N=64/80/128, leaving the mode a lone singlet. An unpaired mode has no doublet partner: phase freedom (no sin quadrature), representation closure (no 2D rep), symmetry closure (reflection maps cos→cos), and the weak-isospin attachment (D_022) all fail. The test across N=64/80/96/128/192 shows complete pairing is NOT required by count conservation (the count is conserved regardless) or by closure (convergence is independent of pairing) — it is required by the doublet-structure observability, i.e. the observable-sector construction (D_020). The pairing STRUCTURE is DERIVED (oscillation quadrature, D_021); the COMPLETENESS is BOUNDARY.",
            "For non-experts: the spectrum has frequencies that come in mirror-pairs, and one special frequency (the very top one) has only a cosine part — no sine part. We asked why the theory insists that even this one frequency sit inside a larger degenerate group instead of standing alone. The answer: it is a requirement of what the observable sector is — the weak-force doublet structure needs every frequency to be part of a pair. This requirement is not derivable from the deeper layers; it is the one genuine input, and the size 96 follows from it.",
            "The mirror-pairing is derived; its completeness is the observable-sector input.",
            "“Every frequency must belong to a pair — that is the requirement, not an accident.”",
            true,
            [
                new("ResearchY-D_021", "OSCILLATION SYMMETRY",
                    "The Z2 pairing structure is DERIVED (cos/sin quadrature pair of one oscillation).",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_021.md"),
                new("ResearchY-D_031", "SEED-ORIGIN AUDIT",
                    "p=3 is derived from complete pairing + convergence — the completeness is the input this audit analyzes.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_031.md"),
                new("ResearchY-D_032", "PAIRING-REQUIREMENT AUDIT",
                    "Pairing structure DERIVED (D_021); completeness (0 unpaired) BOUNDARY (observable sector, D_020); self-conjugate degeneracy DERIVED (6|N).",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_032.md"),
            ]),
        new(
            "researchy-d031-seed-origin",
            "The Period-3 Seed Is Derived From Pairing Completeness",
            "ResearchY Milestone · D_031 (Wave Geometry Program)",
            "Why does everything begin with a period-3 seed? The seed period p=3 is not a final boundary assumption — it is derived: it is the unique period whose natural octave-rung size has complete Z2 pairing (0 unpaired modes) and converges. Only the pairing requirement itself (the observable sector) is boundary.",
            "D_031 scanned the seed periods p=2..6 at their natural octave-rung sizes n = p·2^k in the 3-family window [60,120). Complete Z2 pairing (0 unpaired, weak-isospin doublets, D_020) selects p=3 uniquely: p=2/4 → n=64 (1 unpaired, incomplete), p=5 → n=80 (1 unpaired, incomplete), p=6 → n=96 but FAILS convergence (density 1/6), and p=3 → n=96 (0 unpaired, converges). The canonical Period3SeedOrigin classifies this INEVITABLE. p=3 is the MINIMAL period with complete pairing. Removing p=3 breaks the pairing completeness first (any other converging period gives 1 unpaired mode). Chain: Difference → Actualization → observable sector (BOUNDARY) → p=3 (DERIVED) → 6|N → octave ladder → N=96.",
            "For non-experts: the theory's building blocks start with a repeating pattern, and we asked why the repeat length is 3. The answer: the pattern length is not chosen freely — it is the smallest length that makes the spectrum's mirror-pairing complete (no leftover unpaired frequencies) while still settling into a stable network. Every shorter pattern leaves an unpaired frequency; a length-6 pattern never settles. So the '3' is a consequence of the pairing and stability requirements, not an arbitrary input.",
            "The seed period 3 is the minimal complete-pairing period, not an arbitrary input.",
            "“The seed is three because that is the smallest pattern that pairs completely.”",
            true,
            [
                new("ResearchY-D_020", "SELECTION PRECONDITION",
                    "Complete Z2 pairing + 3 families are the observable-sector INPUT; the period-3 seed is derived from pairing completeness.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_020.md"),
                new("ResearchY-D_030", "OCTAVE-RUNG AUDIT",
                    "Octave ladder n = p·2^k is derived; q=2 EMERGENT; the seed period p remains the input D_031 resolves.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_030.md"),
                new("ResearchY-D_031", "SEED-ORIGIN AUDIT",
                    "p=3 DERIVED: unique period with complete Z2 pairing at the natural size + convergence; pairing requirement BOUNDARY.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_031.md"),
            ]),
        new(
            "researchy-d030-octave-rung",
            "The Octave-Rung Structure Is Derived, Not a Boundary Assumption",
            "ResearchY Milestone · D_030 (Wave Geometry Program)",
            "The chain N = p·2^k (octave rungs) that discriminates N=96 is not a remaining boundary assumption — it is derived. The family count floor(log₂ span)+1 is itself an octave (factor-2) partition, and the long-wavelength dispersion of the ring makes mode doubling a frequency octave. Only the seed period p=3 is boundary.",
            "D_030 tested why the scale step is q=2. Two derived sources: (1) the family count floor(log₂ span)+1 is an octave (factor-2) partition of the spectrum (D_016) — the octave IS the family band; (2) the long-wavelength dispersion ω(k) ~ (2π·k·√91)/N is LINEAR in k, so mode doubling k→2k is a frequency octave (verified: ω(2)/ω(1) = 1.97 at N=96). Hence the doubling chain n = p·2^k is the discrete octave ladder. Comparing bases: q=2 is the UNIQUE pure scale-step base whose rung chain hits a zero-defect ring (only 96); q=6 hits 108 but mixes the seed (3·6^k = 3^(k+1)·2^k); q=3/4/5 have no zero-defect rung. Removing the octave rung leaves 11 zero-defect rings (60…120) — N=96 is not unique without it. Minimal principle: p (seed period) × q^k (scale step) with q=2.",
            "For non-experts: to pick the size 96, the theory needs a ladder of candidate sizes. We asked why the ladder doubles each step (96, then 192, then 384…). The answer: the spectrum's families are measured in octaves (frequency doubling), and at long wavelengths the ring's frequencies grow linearly with the mode number — so doubling the mode is exactly doubling the frequency. The doubling ladder is therefore not an extra assumption; it is what the spectrum itself does. The one genuine input is the size of the seed pattern (period 3).",
            "The octave ladder is the spectrum's own structure; only the seed period is input.",
            "“The ladder is not imposed. The spectrum builds it.”",
            true,
            [
                new("ResearchY-D_020", "SELECTION PRECONDITION",
                    "N=96 selected by the observable-sector construction; seed period p=3 derived from Z2 completeness.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_020.md"),
                new("ResearchY-D_029", "CLOSURE-DEFECT AUDIT",
                    "Closure removes structural defects (zero-defect set {60..120}); the octave rung discriminates 96.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_029.md"),
                new("ResearchY-D_030", "OCTAVE-RUNG AUDIT",
                    "Octave structure DERIVED (dispersion ω~c·k + octave partition); q=2 EMERGENT; seed period p=3 BOUNDARY.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_030.md"),
            ]),
        new(
            "researchy-d028-span-origin",
            "The Spectral Span Is a Derived Function of N, Not a Selector",
            "ResearchY Milestone · D_028 (Wave Geometry Program)",
            "The spectral span (ω_max/ω_min = 6.4025 at N=96) is not the selection quantity behind D96 — it is a DERIVED monotone function of N. The span value follows from N=96 through the ring spectrum, and the 3-family count is a derived consequence of that span, not a root cause.",
            "D_028 derived the span origin: span = ω_max/ω_min. The numerator ω_max → √12 ≈ 3.46 is the antipodal mode (k = N/2, fixed for even N), and the denominator ω_min ~ (2π√91)/N ≈ 59.9/N is the fundamental mode (the spectral gap). Hence span ~ 0.0578·N — a monotonically increasing function with no special point at 96; span(96) = 6.4025 is just the N=96 point. Removing any candidate selector (closure D_019, Z2 completeness, octave-rung, resonance density, information distribution) leaves span(96) unchanged — the value is N-determined, not selector-determined. The family count = floor(log₂ 6.4025)+1 = 3 is the D_016 identity applied to the derived span. Chain: Difference → Actualization → Closure (N=96, BOUNDARY, D_020) → Spectrum → span 6.4025 (DERIVED) → 3 families (DERIVED).",
            "For non-experts: the theory's spectrum has a highest and a lowest frequency; their ratio is about 6.4. We asked whether that ratio is what picks the size 96 of the network. The answer is no — the ratio is just a consequence of the size: as the network grows, the lowest frequency shrinks proportionally, so the ratio grows smoothly through every size. The number 96 was selected by a different, deeper structure; the 6.4 and the 'three families' it implies are consequences, not causes.",
            "The span value is a consequence of N=96, not its selector.",
            "“The ratio is not the cause. It is the echo of the size.”",
            true,
            [
                new("ResearchY-D_020", "SELECTION PRECONDITION",
                    "N=96 is selected by the observable-sector construction (Z2 pairing + 3 families) — the BOUNDARY input.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_020.md"),
                new("ResearchY-D_028", "SPAN-ORIGIN AUDIT",
                    "span ~ 0.0578·N (DERIVED): ω_max→√12 (antipodal), ω_min~(2π√91)/N. span(96)=6.4025 is the N=96 point; 3 families is the D_016 consequence.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_028.md"),
            ]),
        new(
            "researchy-d027-selector-origin",
            "Positivity, Normalization, and Stability Are Derived From the Primitives",
            "ResearchY Milestone · D_027 (Wave Geometry Program)",
            "The criteria that select the SU(2) weak-isospin gauge form — positivity, normalization, and stability — are not a final boundary input: they are derived from Difference → Actualization. Positivity is intrinsic to the count/share construction, normalization is the Born rule (the normalized actualization share), and stability is the closure fixed point. Only the primitive set {Difference, η} itself is boundary.",
            "D_027 traced each selector criterion to the canonical primitives. Positivity: ρ_k = μ^k/S ≥ 0 (verified for μ = 0.5, 1, 2) — the share of a count is intrinsically non-negative. Normalization: the Born rule Σ|ψ|² = 1 holds exactly by construction as the normalization of the actualization share (Ch9/QG216), and count conservation — the definitional identity of Difference (Ch3/QG268) — is what makes the share normalizable (Σρ_k = 1.0000000000 exactly). Stability: the closure principle states the boundary IS the stable fixed point (Ch4/QG282); without it the spectrum would not close. The D_026 su(2) selector (positivity + normalization + stability) is therefore a consequence of the minimal hierarchy. Removing any ingredient (count conservation, positivity, stability, or the primitives themselves) breaks the observable sector.",
            "For non-experts: we asked where the basic requirements that pick the weak-force math come from — the rules that probabilities are positive, add up to one, and stay bounded. The answer is that they are not extra assumptions: they fall out of the theory's foundation. Counting is conserved by definition, shares of a count are positive and add to one, and the whole structure settles into a stable fixed point. The only real input is the primitive difference itself.",
            "The su(2) selector is derived from the primitives; only {Difference, η} is boundary.",
            "“The rules of probability are not extra. They are what counting is.”",
            true,
            [
                new("ResearchY-D_026", "COMPACT-FORM AUDIT",
                    "Positivity + normalization + stability select the compact form su(2) (finite-dim unitary reps).",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_026.md"),
                new("ResearchY-D_027", "SELECTOR-ORIGIN AUDIT",
                    "The selector is DERIVED: positivity (share), normalization (Born rule, count conservation), stability (closure fixed point). Only primitives are boundary.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_027.md"),
            ]),
        new(
            "researchy-d021-oscillation-symmetry",
            "Z2 Pairing Is Derived From Oscillation Symmetry, Not Weak-Isospin",
            "ResearchY Milestone · D_021 (Wave Geometry Program)",
            "The D96 Z2 pairing that carries the weak-isospin doublet structure is not an independent weak-isospin input: it is the two-quadrature structure of a single real oscillation. Each frequency ω_k hosts the pair {cos, sin}, both eigenfunctions of the graph Laplacian at the same λ_k, forced by the spectral mirror symmetry λ_k = λ_{N−k}. The weak-isospin doublet reading is emergent, not the pairing's source.",
            "The audit tested the three Z2 candidates: +A↔−A and cos(ωt)↔−cos(ωt) are per-mode phase gauges (they do not pair modes), while k↔N−k is the pairing generator — cos(2π(N−k)n/N) = cos(2πkn/N) and sin(2π(N−k)n/N) = −sin(2πkn/N), giving the 2D eigenspace {cos, sin} at one frequency. Both harmonics are verified eigenfunctions of L at the same λ_k, so the pair is intrinsic to the standing wave (oscillation necessity), not an import. Standing-wave completeness survives removal of Z2 pairing — completeness is a basis property (the Fourier basis is complete at N=64, 96, 128), pairing is a degeneracy property. Only the COMPLETENESS of pairing (0 unpaired modes) is a boundary: it is N-arithmetic (the λ=12 self-conjugate mode is 5-fold at N=96/192 but 1-fold at N=64/128), the selection input established in D_020.",
            "For non-experts: we asked where the mirror-pairing of the theory's spectrum comes from. The answer is that it is not imposed from the physics side — it is what any real oscillation looks like. A standing wave at one frequency needs both a cosine and a sine part (its two phases), and the mirror symmetry of the ring forces those two parts to share the frequency. The weak-force doublets are a reading of that derived structure, not its origin.",
            "Z2 pairing is the two-quadrature structure of one oscillation — derived, not imported.",
            "“The pair is not two particles. It is the two phases of one standing wave.”",
            true,
            [
                new("ResearchY-D_001", "STANDING WAVES",
                    "Fourier modes are time-harmonic eigenfunctions of L (ψ=φ(n)cos(ωt), ω=√λ); center-free.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_001.md"),
                new("ResearchY-D_002", "STANDING WAVE MODEL",
                    "Ψ=Σ[a cos+b sin]cos(ωt); 47 Z2 pairs (94 real modes) + 1 self-conjugate; hybrid center-free.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_002.md"),
                new("ResearchY-D_020", "SELECTION PRECONDITION",
                    "Complete Z2 pairing + 3 families are the observable-sector INPUT; p=3, 6|N, octave rung, N=96 derived.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_020.md"),
                new("ResearchY-D_021", "OSCILLATION SYMMETRY",
                    "Z2 pairing is the cos/sin quadrature pair of one oscillation — DERIVED; weak-isospin reading EMERGENT; completeness BOUNDARY.",
                    "https://github.com/MagusDraconis/AT/blob/feature/v2.1-boundary-program/Docs/ResearchY/D_ResonanceStructure/ResearchY-D_021.md"),
            ]),
        new(
            "qg313-319-locks-and-final-architecture",
            "Locks Precede Organization and the Final Architecture Is Canonical",
            "AT-QG Milestone · Phases 313–319",
            "The lock identities — exact moment-ratio coincidences of the D96 spectrum — are universal in structure, class-separating, and they precede mature organization. The blind protocol predicts the future HIGH-organization class from early-stage locks alone, and the canonical minimal architecture is now fixed: Difference → Actualization → Spectrum → Physics.",
            "QG313 establishes the lock law: the lock STRUCTURE is universal across domains, the VALUES are domain-specific fingerprints. QG314-315 show locks separate the organized class and PRECEDE organization. QG316 finds the operator basis completes at a critical parameter (g* ≈ 0.31) — a phase transition. QG317 runs a strict blind protocol: the future HIGH class is predicted from early-stage lock coherence with 8/8 accuracy. QG318 traces the lock origin: the moment-chain identity occMom/Σm = (Σm²/Σm)·(occMom/Σm²) holds exactly — the locks are resonance fixed points in structure, D96-emergent in value. QG318 (reissue 2) then fixes the FINAL AT ARCHITECTURE: 4 layers [primitive, dynamic, spectrum, physics], 20 concepts classified [FOUNDATIONAL/DERIVED/EMERGENT/BOUNDARY], dependency graph verified acyclic.",
            "For non-experts: we tested whether the 'lock' coincidences in the D96 spectrum could predict the future of a growing system. They can — the future organization class is visible in the early structure. And we fixed the canonical architecture of the whole theory: four layers from the primitive difference, through the actualization dynamics and the emergent spectrum, to physics. The honest open items (SM dynamics, Bekenstein 1/4, ψ status) are classified as boundary.",
            "The final architecture is Difference → Actualization → Spectrum → Physics, verified acyclic.",
            "“The locks are real, and the architecture is now canonical.”",
            true,
            [
                new("AT-QG 313", "PARTIAL LOCK LAW",
                    "Lock structure universal, lock values domain-specific — the lock law.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_LockUniversalityAudit.md"),
                new("AT-QG 316", "ORGANIZATION PHASE TRANSITION",
                    "The operator basis completes at the critical parameter g* ≈ 0.31.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_OrganizationPhaseTransitionAudit.md"),
                new("AT-QG 317", "PREDICTIVE",
                    "Blind protocol: early lock coherence predicts the future HIGH class 8/8.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_BlindOrganizationPrediction.md"),
                new("AT-QG 318", "PARTIAL ORIGIN",
                    "Lock origin: the moment-chain identity lock1 = lock2 × lock3 holds exactly.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_LockOriginAudit.md"),
                new("AT-QG 318", "FINAL AT ARCHITECTURE",
                    "Canonical architecture: 4 layers, acyclic dependency graph, 20 concepts classified.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_FinalTheoryArchitecture.md"),
                new("AT-QG 319", "NO ADVANTAGE",
                    "Competing predictors: entropy/gini/exponent/gap match or beat the locks (100% vs 83%).",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_CompetingPredictorAudit.md"),
            ]),
        new(
            "qg203-209-sm-closure",
            "The Standard-Model Layer Closes in Closed-Form D96 Laws",
            "AT-QG Milestone · Phases 203–209",
            "The last open standard-model derivations are now exact closed-form D96 laws. Absolute neutrino masses (m1 = 0, m2 = 8.72 meV, m3 = 49.4 meV), the quark MS̄ running (native at the natural scale), and the lepton hierarchy (m_μ = me·Σm²/√occMom, m_τ = me·Σm²·λ₂) are all derived with no fitted parameters.",
            "QG203 derives the absolute neutrino masses as closed-form D96 expressions: m2 = 1/(Σ√m·√(span/2)), m3 = √#g/(Σm·√2), with the exact ratio m2/m3 = 2Σm/(Σ√m·√(span·#g)). QG204 shows the quark mass law is natively MS̄ at the natural scale (all six within 0.2%) with spectral α_s = 8/Σ√m and the running exponent q = #d/(2·#g). QG209 closes the lepton hierarchy: m_μ = me·Σm²/√occMom (0.13%), m_τ = me·Σm²·λ₂ (0.28%). The gravity side closed in parallel: α=0 derived (QG206), metric ansatz PARTIAL UNIQUE (QG207), Hawking T ∝ 1/R preserved in the ψ sector (QG208).",
            "For non-experts: the framework now derives the fermion mass spectrum — electrons, muons, taus, neutrinos, quarks — from the same D96 spectral geometry, with no fitted exponents. The remaining open frontier is experimental: the three pre-registered predictions await data.",
            "The standard-model layer is closed; the broader program now spans 334 phases (212 tested, 12 partial, 12 untested, 98 audit) at 72.6% weighted coverage, with the frontier concentrated in the latest foundation audits and the three pre-registered predictions.",
            "“The derivations are done. The experiments decide.”",
            true,
            [
                new("AT-QG 203", "ABSOLUTE MASS ORIGIN",
                    "Neutrino masses m2 = 1/(Σ√m·√(span/2)) = 8.72 meV, m3 = √#g/(Σm·√2) = 49.4 meV; exact ratio.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_AbsoluteNeutrinoMassOrigin.md"),
                new("AT-QG 204", "RUNNING ORIGIN",
                    "Quark MS̄ running: native at natural scale (0.2%); spectral α_s; exponent q = #d/(2·#g).",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_QuarkRunningOrigin.md"),
                new("AT-QG 209", "EXACT LAW",
                    "Lepton hierarchy: m_μ = me·Σm²/√occMom (0.13%), m_τ = me·Σm²·λ₂ (0.28%).",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_LeptonHierarchyExactLaw.md"),
            ]),
        new(
            "qg295-300-first-peak-origin",
            "The First Peak Carries a Fitted Multiplier",
            "AT-QG Frontier · Phases 295–300",
            "The minimal hierarchy now reconstructs the major QG223–295 results, and the newest audits isolate the remaining 5/4 issue to the absolute first peak: only ℓ₁ carries the factor, while peak ratios remain pure spectral quantities.",
            "QG296 closes the reconstruction audit, QG297 classifies 5/4 as an exception that remains a fit, and QG300 resolves the first peak: ℓ₁ = Σm·ln(span)·(5/4) is the absolute fundamental harmonic, while ℓ₂/ℓ₁ and ℓ₃/ℓ₁ are ratios that cancel the normalization. The 5/4 factor is documented as a fitted multiplier (QG297), removable in principle (QG289); it is not a derived constant.",
            "For non-experts: the latest frontier work says the broad theory is already reconstructed. The remaining question is a single normalization on the first acoustic peak, and it is documented as a fitted multiplier (QG297) that belongs only to the absolute peak, not to the ratios.",
            "Only the absolute first peak carries the fitted normalization.",
            "“Only ℓ₁ needs the fitted multiplier; the ratios do not.”",
            true,
            [
                new("AT-QG 296", "MINIMAL THEORY",
                    "Difference → Actualization → Spectrum → Physics is the minimal hierarchy.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_MinimalTheoryAudit.md"),
                new("AT-QG 297", "INEVITABLE SPECTRUM",
                    "The spectrum is the inevitable output of the actualization attractor.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_SpectrumNecessityAudit.md"),
                new("AT-QG 298", "COMPLETE RECONSTRUCTION",
                    "The minimal theory reconstructs all major QG223–295 results.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_ReconstructionAudit.md"),
                new("AT-QG 299", "EXCEPTION REMAINS",
                    "5/4 is a fit, not a derivation or boundary.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_ExceptionAudit.md"),
                new("AT-QG 300", "FIRST PEAK ORIGIN",
                    "ℓ₁ alone carries the 5/4 fitted multiplier (QG297), removable (QG289); peak ratios cancel it.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_FirstPeakOriginAudit.md"),
            ]),
        new(
            "qg115-117-structure-from-actualization",
            "Structure Emerges From Actualization",
            "AT-QG Breakthrough · Phases 115–117",
            "Actualization patterns actively shape network geometry (QG115, PARTIAL FEEDBACK). Strong-feedback dynamics drives all tested content patterns toward a single universal geometry class (QG116, STRUCTURE ORIGIN). The universal attractor is DYNAMICAL — not accidental, not inevitable — and geometry is controlled by feedback/damping parameters (QG116b). Dynamic parameters then generate discrete attractor geometry classes (QG117, ATTRACTOR ORIGIN).",
            "The activity→links→activity feedback loop of the QG115 model couples Q-events (activity) to structure (links). Weak feedback gives PARTIAL FEEDBACK (content shapes geometry, uniform content builds nothing). Sustained strong feedback (damping 0.2, feedback 0.7, K=6) drives every tested pattern to one universal geometry class — the N·K circulant, an exact fixed point (residual 0) with 100% basin and size universality (QG116b). The (feedback, damping) parameter plane maps to a discrete ladder of stable geometry classes (radius 2 and 6 for K=6) with sharp threshold at f/d ≈ 2.",
            "For non-experts: we tested whether the network that underlies reality builds itself. It can — activity creates links, links feed back into activity. With strong feedback every starting pattern converges to the same geometry (a single universal attractor), and tuning two knobs (feedback strength and damping) produces a small set of distinct, stable geometries — the kind of discrete structure particle families would need.",
            "The direction of explanation is reversed: Actualization → Structure → Physics (instead of Structure → Physics).",
            "“Geometry is not primary. Geometry emerges from actualization.”",
            true,
            [
                new("AT-QG 115", "PARTIAL FEEDBACK",
                    "Activity-driven feedback changes the geometry; content shapes structure (concentrated 4 families / spread 3 / uniform 0 links).",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_StructureFromContent.md"),
                new("AT-QG 116", "STRUCTURE ORIGIN",
                    "Sustained self-reinforcing dynamics drives every activity pattern to the same geometry class (single universal attractor, 576 links, span 6.40).",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_ActualizationStructures.md"),
                new("AT-QG 116b", "DYNAMICAL",
                    "The universal attractor is an exact stable fixed point (residual 0, 100% basin, size-universal) but its radius depends on the feedback/damping ratio — a dynamical selection, not an accident.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_UniversalAttractor.md"),
                new("AT-QG 117", "ATTRACTOR ORIGIN",
                    "The parameter plane generates discrete attractor geometry classes (radius 2↔6, sharp threshold f/d≈2) — distinct stable geometries controlled by dynamics parameters.",
                    "https://github.com/MagusDraconis/AT/blob/AT_v1.1/Docs/Research/ATQG_AttractorParameterOrigin.md"),
            ]),
    ];

    public static IReadOnlyList<TimelineEventModel> Timeline { get; } =
    [
        new("TRM", "Temporal Resonance Model",
            "The predecessor framework (Time Field, RAR, Frame Dragging, Memory Channel, Theta Chain, m=3 Closure, Quantum Engine, Temporal Drift, Unified Action). The legacy audit found zero candidate physics."),
        new("Early AT", "AT-001–AT-008",
            "Oscillator experiments established spontaneous synchronization, a critical resonance density (ρ_c ≈ 0.09), and one universal coherent mode (F4)."),
        new("ResearchX", "X001–X034",
            "Alternative foundations: reversibility plus self-consistency is minimally sufficient for unitary quantum mechanics; quantum reality is the unique global maximum of finite complexity."),
        new("Continuum Program", "L_Q → −∇² and BDG → □",
            "Two controlled continuum limits, disjoint in signature; the metric-origin chain is closed, the metric dynamics are not."),
        new("Hostile Reviews", "Round 1 and Round 2",
            "Framing corrections, not retracted physics: 6 resolved, 6 partially resolved, 0 open (Round 2)."),
        new("AT v1.0", "Release",
            "The compressed theory: two primitives plus one number; READY AS A FOUNDATION MONOGRAPH."),
        new("AT-QG 115–117", "Structure emerges from actualization",
            "Actualization patterns shape network geometry (QG115, PARTIAL FEEDBACK); strong feedback drives every pattern to one universal geometry class (QG116, STRUCTURE ORIGIN); the attractor is dynamical, not accidental (QG116b); dynamic parameters generate discrete attractor geometry classes (QG117, ATTRACTOR ORIGIN) — the direction of explanation reverses: Actualization → Structure → Physics."),
    ];

    public static IReadOnlyList<HostileReviewModel> HostileReviews { get; } =
    [
        new("Primitives", "Undefined as mathematical objects", "Partially resolved"),
        new("Schrödinger derivation", "Circular / incomplete", "Resolved"),
        new("Gauge derivations", "Elementary topology", "Partially resolved"),
        new("Complexity argument", "Not a derivation", "Partially resolved"),
        new("Structure/content split", "Immunization", "Partially resolved"),
        new("Composite objects", "Admitted", "Partially resolved"),
        new("Internal-3", "Unresolved while 'complete'", "Resolved"),
        new("T-09 at 0.10", "Cannot close", "Resolved"),
        new("'Structurally complete'", "Overstatement", "Resolved"),
        new("Gravity → Einstein", "Ontological, not derivation", "Resolved"),
        new("Aut(S¹) = U(1)", "As an EM derivation", "Resolved"),
        new("Continuum limit", "No controlled limit", "Partially resolved"),
    ];

    public static IReadOnlyList<ReferenceModel> References { get; } =
    [
        new("D. B. Malament, “The class of continuous timelike curves determines the topology of spacetime,” J. Math. Phys. 18, 1399 (1977)."),
        new("S. W. Hawking, A. R. King, P. J. McCarthy, “A new topology for curved space–time…,” J. Math. Phys. 17, 174 (1976)."),
        new("J. Myrheim, “Statistical geometry,” CERN-TH-2538 (1978)."),
        new("D. M. T. Benincasa, F. Dowker, “Scalar curvature of a causal set,” Phys. Rev. Lett. 104, 181301 (2010); L. Glaser, Class. Quant. Grav. 31, 095007 (2014)."),
        new("A. M. Gleason, “Measures on the closed subspaces of a Hilbert space,” J. Math. Mech. 6, 885 (1957)."),
        new("R. D. Sorkin, “Causal sets: discrete gravity,” Proceedings of the Valdivia Summer School (2003)."),
        new("M. Belkin, P. Niyogi, “Laplacian eigenmaps for dimensionality reduction…,” Neural Computation 15, 1373 (2003)."),
        new("Y. Koide, “Fermion mass relation and the generation structure,” Phys. Rev. D 28, 252 (1983) [letter 1981]."),
        new("J. Bertrand, “Théorème relatif au mouvement d’un point attiré vers un centre fixe,” C. R. Acad. Sci. 77, 849 (1873)."),
    ];

    private static IReadOnlyList<DerivationNodeModel> BuildDerivationGraph()
    {
        var schrodinger = new DerivationNodeModel
        {
            Id = "schrodinger",
            Title = "Schrödinger",
            Summary = "Conserved norm forces an anti-Hermitian generator; the simplest anti-Hermitian object on the graph gives i∂_t ψ = L_Q ψ. Quantum mechanics is produced, not postulated.",
            Classification = ClassificationKind.Derived,
            VerificationStatus = "GraphLaplacianContinuumTests; AT-149–151",
        };

        var hilbert = new DerivationNodeModel
        {
            Id = "hilbert",
            Title = "Hilbert Space",
            Summary = "The eigenbasis of the graph Laplacian L_Q = D − A is the Hilbert space of the theory.",
            Classification = ClassificationKind.Derived,
            VerificationStatus = "AT-149",
            Children = [schrodinger],
        };

        var space = new DerivationNodeModel
        {
            Id = "space",
            Title = "Space",
            Summary = "Space is the Q-interaction graph (and, on the causal side, a causal set).",
            Classification = ClassificationKind.Derived,
            VerificationStatus = "Analytical (graph structure)",
            Children = [hilbert],
        };

        var time = new DerivationNodeModel
        {
            Id = "time",
            Title = "Time",
            Summary = "Time is the partial order of actualization events: E1 < E2 iff E2 depends on E1's outcome.",
            Classification = ClassificationKind.Derived,
            VerificationStatus = "Analytical (X040)",
        };

        var u1 = new DerivationNodeModel
        {
            Id = "u1",
            Title = "U(1)",
            Summary = "Phase lives on S¹; its isometry group is U(1), and the winding yields integer charge. The cleanest result of the gauge program.",
            Classification = ClassificationKind.Derived,
            VerificationStatus = "Topological theorem (confidence 0.95)",
        };

        var cosmology = new DerivationNodeModel
        {
            Id = "cosmology",
            Title = "Cosmology",
            Summary = "Expansion is an FLRW interpretation; causal-set Λ ~ 1/√N is a postdiction; the CMB is an accepted partial computational layer.",
            Classification = ClassificationKind.Partial,
            VerificationStatus = "DATA-001/002; CMB closure audits (partial)",
        };

        var gravity = new DerivationNodeModel
        {
            Id = "gravity",
            Title = "Gravity",
            Summary = "Causal set → GR in the continuum limit; the Einstein recovery is logical, not mathematical (metric and BDG action imported).",
            Classification = ClassificationKind.Imported,
            VerificationStatus = "EinsteinRecoveryTests (PARTIAL)",
            Children = [cosmology],
        };

        var q = new DerivationNodeModel
        {
            Id = "q",
            Title = "Q",
            Summary = "The individuation principle: the irreducible act by which a discrete event is individuated from nothing.",
            Classification = ClassificationKind.Partial,
            VerificationStatus = "Partially formalized (measure/action missing)",
            Children = [time, space, u1, gravity],
        };

        return [q];
    }
}
