using TQM.App.Models;

namespace TQM.App.Services;

/// <summary>
/// Strongly typed, in-memory data for the TQM v1.0 research atlas.
/// All content is drawn from TQM_v1_0_Monograph_Final (no new physics).
/// </summary>
public static class AtlasDataService
{
    public static IReadOnlyList<HeroStat> HeroStats { get; } =
    [
        new("4", "Primitives", "#4DD0E1"),
        new("15", "Test Suites", "#64B5F6"),
        new("47", "Tests", "#81C784"),
        new("1", "Open Door", "#FFB74D"),
    ];

    public static IReadOnlyList<string> HeroChain { get; } =
    [
        "Network", "Quantum", "Space", "Gravity", "Cosmology",
    ];

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
        new("TQM v1.0", "Release",
            "Two primitives plus one number. READY AS A FOUNDATION MONOGRAPH.",
            "bi-rocket-takeoff", "cyan"),
    ];

    public static IReadOnlyList<PrimitiveModel> Primitives { get; } =
    [
        new("Q", "Individuation",
            "The irreducible act by which a discrete event comes to be individuated from nothing. Q underwrites the derivation of structure.",
            "Partially formalized", "Underwrites structure (ontology layer)"),
        new("RA", "Random Actualization",
            "Genuine ontological chance (assumption A-03): given Q, the realized event locations are random within the causal structure.",
            "Assumption (A-03)", "Underwrites content"),
        new("(ℓ, τ, ℏ)", "The scale triad",
            "The irreducible physical triple: a spacetime scale ℓ, a clock τ, and an action ℏ. Unit conventions, not free parameters.",
            "Formalized (constants)", "Fixes units"),
        new("M²", "Nonlinearity parameter",
            "The single continuous nonlinearity parameter of the dynamics, pinned by the derivation hierarchy to M² ≈ 5.",
            "Partially formalized (parameter)", "Single continuous number"),
    ];

    public static IReadOnlyList<DerivationNodeModel> DerivationRoots { get; } = BuildDerivationGraph();

    public static IReadOnlyList<ClassificationItemModel> Taxonomy { get; } =
    [
        new("U(1)", ClassificationKind.Derived, "0.95"),
        new("Spatial 3", ClassificationKind.Derived, "0.85"),
        new("N ≥ 3", ClassificationKind.Derived, "0.90"),
        new("Log-normal law", ClassificationKind.Derived, "theorem"),
        new("SU(2)", ClassificationKind.RealUnderived, "0.70"),
        new("SU(3) structure", ClassificationKind.RealUnderived, "0.10"),
        new("Koide Q=2/3 (reality)", ClassificationKind.RealUnderived, "0.90"),
        new("Koide 45° (origin)", ClassificationKind.RealUnderived, "0.70"),
        new("Yukawas / couplings / Ω_DM", ClassificationKind.Drawn, "—"),
        new("N ≤ 3", ClassificationKind.Drawn, "0.70"),
        new("Color count 3", ClassificationKind.Drawn, "—"),
        new("Internal N = 3", ClassificationKind.Derived, "0.70 (derived ∩ drawn)"),
        new("Neutrino-Koide", ClassificationKind.Falsified, "0.90"),
    ];

    public static IReadOnlyList<ContinuumChainModel> ContinuumChains { get; } =
    [
        new("The elliptic chain",
            "L_Q  →  −∇²  →  Schrödinger",
            "Controlled and tested",
            "GraphLaplacianContinuumTests, CurvedSchrodingerTests",
            "One-dimensional, unweighted, elliptic only."),
        new("The Lorentzian chain",
            "BDG  →  □",
            "Controlled, O(h²)",
            "BDGOperatorContinuumTests",
            "Operator imported from causal-set theory; leading-order tolerance."),
        new("The metric-origin chain",
            "Q-events → causal order → conformal class (Malament) → conformal factor (ρ^{2/d}) → metric",
            "Origin closed; dynamics imported",
            "MetricGenerationTests, MetricEmergenceTests, ConformalStructureTests, MetricOriginTests",
            "Native metric→operator coupling (G4) remains open."),
    ];

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
            "qg115-117-structure-from-actualization",
            "Structure Emerges From Actualization",
            "TQM-QG Breakthrough · Phases 115–117",
            "Actualization patterns actively shape network geometry (QG115, PARTIAL FEEDBACK). Strong-feedback dynamics drives all tested content patterns toward a single universal geometry class (QG116, STRUCTURE ORIGIN). The universal attractor is DYNAMICAL — not accidental, not inevitable — and geometry is controlled by feedback/damping parameters (QG116b). Dynamic parameters then generate discrete attractor geometry classes (QG117, ATTRACTOR ORIGIN).",
            "The activity→links→activity feedback loop of the QG115 model couples Q-events (activity) to structure (links). Weak feedback gives PARTIAL FEEDBACK (content shapes geometry, uniform content builds nothing). Sustained strong feedback (damping 0.2, feedback 0.7, K=6) drives every tested pattern to one universal geometry class — the N·K circulant, an exact fixed point (residual 0) with 100% basin and size universality (QG116b). The (feedback, damping) parameter plane maps to a discrete ladder of stable geometry classes (radius 2 and 6 for K=6) with sharp threshold at f/d ≈ 2.",
            "For non-experts: we tested whether the network that underlies reality builds itself. It can — activity creates links, links feed back into activity. With strong feedback every starting pattern converges to the same geometry (a single universal attractor), and tuning two knobs (feedback strength and damping) produces a small set of distinct, stable geometries — the kind of discrete structure particle families would need.",
            "The direction of explanation is reversed: Actualization → Structure → Physics (instead of Structure → Physics).",
            "“Geometry is not primary. Geometry emerges from actualization.”",
            true,
            [
                new("TQM-QG 115", "PARTIAL FEEDBACK",
                    "Activity-driven feedback changes the geometry; content shapes structure (concentrated 4 families / spread 3 / uniform 0 links).",
                    "https://github.com/MagusDraconis/TQM/blob/TQM_v1.1/Docs/Research/TQMQG_StructureFromContent.md"),
                new("TQM-QG 116", "STRUCTURE ORIGIN",
                    "Sustained self-reinforcing dynamics drives every activity pattern to the same geometry class (single universal attractor, 576 links, span 6.40).",
                    "https://github.com/MagusDraconis/TQM/blob/TQM_v1.1/Docs/Research/TQMQG_ActualizationStructures.md"),
                new("TQM-QG 116b", "DYNAMICAL",
                    "The universal attractor is an exact stable fixed point (residual 0, 100% basin, size-universal) but its radius depends on the feedback/damping ratio — a dynamical selection, not an accident.",
                    "https://github.com/MagusDraconis/TQM/blob/TQM_v1.1/Docs/Research/TQMQG_UniversalAttractor.md"),
                new("TQM-QG 117", "ATTRACTOR ORIGIN",
                    "The parameter plane generates discrete attractor geometry classes (radius 2↔6, sharp threshold f/d≈2) — distinct stable geometries controlled by dynamics parameters.",
                    "https://github.com/MagusDraconis/TQM/blob/TQM_v1.1/Docs/Research/TQMQG_AttractorParameterOrigin.md"),
            ]),
    ];

    public static IReadOnlyList<MilestoneCounterModel> ResearchMilestones { get; } =
    [
        new("119", "TQM-QG Phases", "#4DD0E1"),
        new("357", "Tests Passing", "#81C784"),
        new("4", "Structure-From-Actualization Phases", "#BA68C8"),
        new("1", "Milestone Breakthrough", "#FFB74D"),
    ];

    public static IReadOnlyList<TimelineEventModel> Timeline { get; } =
    [
        new("TRM", "Temporal Resonance Model",
            "The predecessor framework (Time Field, RAR, Frame Dragging, Memory Channel, Theta Chain, m=3 Closure, Quantum Engine, Temporal Drift, Unified Action). The legacy audit found zero candidate physics."),
        new("Early TQM", "TQM-001–TQM-008",
            "Oscillator experiments established spontaneous synchronization, a critical resonance density (ρ_c ≈ 0.09), and one universal coherent mode (F4)."),
        new("ResearchX", "X001–X034",
            "Alternative foundations: reversibility plus self-consistency is minimally sufficient for unitary quantum mechanics; quantum reality is the unique global maximum of finite complexity."),
        new("Continuum Program", "L_Q → −∇² and BDG → □",
            "Two controlled continuum limits, disjoint in signature; the metric-origin chain is closed, the metric dynamics are not."),
        new("Hostile Reviews", "Round 1 and Round 2",
            "Framing corrections, not retracted physics: 6 resolved, 6 partially resolved, 0 open (Round 2)."),
        new("TQM v1.0", "Release",
            "The compressed theory: two primitives plus one number; READY AS A FOUNDATION MONOGRAPH."),
        new("TQM-QG 115–117", "Structure emerges from actualization",
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
            VerificationStatus = "GraphLaplacianContinuumTests; TQM-149–151",
        };

        var hilbert = new DerivationNodeModel
        {
            Id = "hilbert",
            Title = "Hilbert Space",
            Summary = "The eigenbasis of the graph Laplacian L_Q = D − A is the Hilbert space of the theory.",
            Classification = ClassificationKind.Derived,
            VerificationStatus = "TQM-149",
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
