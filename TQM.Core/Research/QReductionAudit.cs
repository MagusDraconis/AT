namespace TQM.Core.Research;

/// <summary>
/// Systematic audit: can Q be reduced to something deeper?
/// TQM-X035: Origin of Q Principle
/// </summary>
public static class QReductionAudit
{
    public static List<QNecessityMetrics.QReductionAttempt> AttemptReductions()
    {
        return new List<QNecessityMetrics.QReductionAttempt>
        {
            new("Graph topology alone",
                "A graph G=(V,E) has connected components. β₀ counts them. Is Q just β₀?",
                QNecessityMetrics.ReductionStatus.PartiallyDerived,
                "Q as domain count IS β₀ of the superlevel set {R>0.5}. TQM-117 confirmed this.",
                "Q as VERTEX CHARGE is not derived from graph topology alone. β₀ counts components; "
                + "Q also assigns integer weight to each vertex. Integer vertex charge ≠ component count.",
                "PARTIAL: domain-count Q is β₀, but vertex-charge Q is extra structure."),

            new("Information theory",
                "Information = -Σp log p. Requires distinguishable states. Does distinguishability imply Q?",
                QNecessityMetrics.ReductionStatus.Irreducible,
                "Nothing. Information PRESUPPOSES distinguishable states.",
                "Information theory needs a set of possible outcomes to define probabilities over. "
                + "Without Q, there are no distinguishable outcomes — just a continuum with no identity. "
                + "Information CANNOT generate distinguishability; it REQUIRES it.",
                "IRREDUCIBLE: Q is the precondition for information, not a consequence."),

            new("Reversibility (R)",
                "d‖ψ‖²/dt = 0. Does norm conservation force discrete charge?",
                QNecessityMetrics.ReductionStatus.Irreducible,
                "Nothing. Reversibility is a constraint on dynamics, not on state space.",
                "A continuous field with reversibility has no discrete charges. "
                + "R conserves the norm but doesn't quantize anything. "
                + "Infinite-dimensional reversible systems exist without integer invariants.",
                "IRREDUCIBLE: Reversibility is charge-neutral."),

            new("Self-consistency F(x)=x",
                "Fixed points give stable structures. Do fixed points force discreteness?",
                QNecessityMetrics.ReductionStatus.Irreducible,
                "Nothing. Fixed points can form continua (center manifolds).",
                "A system can have a continuous family of fixed points — a manifold. "
                + "F(x)=x does not force discrete, countable, integer-labeled structures. "
                + "Discreteness requires a topological obstruction, not just a fixed point.",
                "IRREDUCIBLE: Self-consistency gives stability, not discreteness."),

            new("Noether's theorem (symmetry → conserved charge)",
                "If TQM has U(1) symmetry, the conserved Noether charge IS Q.",
                QNecessityMetrics.ReductionStatus.PartiallyDerived,
                "The PDE has no obvious continuous symmetry. R is real, not complex.",
                "Noether: continuous symmetry → conserved current. TQM's PDE has no U(1). "
                + "Q is TOPOLOGICAL (winding number), not Noetherian. "
                + "BUT: in the quantum limit, ψ is complex and HAS U(1) symmetry → "
                + "Noether charge = particle number. This is Q in the quantum regime.",
                "PARTIAL: in the quantum limit, Q aligns with Noether charge. But the "
                + "topological Q of the classical PDE is not Noether-derived."),

            new("Conservation law",
                "Q is conserved: dQ/dt = 0. Can we define Q as 'whatever is conserved'?",
                QNecessityMetrics.ReductionStatus.Irreducible,
                "Nothing. 'Whatever is conserved' is circular.",
                "This is a definition, not a derivation. Many things can be conserved "
                + "(energy, momentum, norm, etc.). Why THIS conserved quantity? "
                + "Conservation alone doesn't pick out Q from other invariants.",
                "IRREDUCIBLE: conservation is a property of Q, not its origin."),

            new("Complexity maximization",
                "X031: complexity is maximized at (R=1,S=1). Does this force Q?",
                QNecessityMetrics.ReductionStatus.Irreducible,
                "Nothing. Complexity = diversity of species. Species require Q.",
                "Complexity maximization tells us WHERE the optimum is (R=1,S=1), "
                + "but not WHY there are discrete entities to diversify in the first place. "
                + "Without Q, there are no carriers, no species, no complexity to maximize.",
                "IRREDUCIBLE: complexity presupposes Q, cannot generate it."),

            new("Hilbert space structure",
                "Hilbert space = complete inner product space. Does it require Q?",
                QNecessityMetrics.ReductionStatus.Irreducible,
                "Nothing. Hilbert space is the ARENA, not the actors.",
                "Hilbert space provides the vector space and inner product. "
                + "But it doesn't provide discrete labels for basis vectors. "
                + "You can have L²(R) — continuous spectrum, no Q. "
                + "Q is what makes the basis COUNTABLE and LABELED.",
                "IRREDUCIBLE: Hilbert space doesn't force discrete labeling."),

            new("Reality structures (R,S)",
                "X014: R+S → reality. Do reality structures force discrete identity?",
                QNecessityMetrics.ReductionStatus.Irreducible,
                "Nothing. Reality structures are CLASSES of systems, not the entities within them.",
                "R+S determines whether persistent structures CAN exist. "
                + "But it doesn't determine what those structures ARE or how they're labeled. "
                + "At (R=1,S=1), we get carriers — but Q labels which carrier is which.",
                "IRREDUCIBLE: R+S enables existence; Q enables identity."),

            new("Graph vertex existence = Q",
                "A graph G=(V,E) has vertices. A vertex existing IS Q=1 at that location.",
                QNecessityMetrics.ReductionStatus.PartiallyDerived,
                "Binary Q (exists/doesn't) IS vertex existence in the graph.",
                "Integer Q > 1 (multiple charges at one vertex) is NOT captured by vertex existence. "
                + "But Q>1 emerges from mergers: Q(A∪B) = Q(A)+Q(B). "
                + "Binary Q = graph structure. Integer Q = emergent from dynamics.",
                "PARTIAL: binary Q ≡ vertex existence (graph primitive). "
                + "Integer Q follows from additivity + conservation."),
        };
    }

    public static List<QNecessityMetrics.QNecessityAudit> AuditNecessity()
    {
        return new List<QNecessityMetrics.QNecessityAudit>
        {
            new("Graph structure", false,
                "COLLAPSES. Without Q, vertices are indistinguishable. "
                + "Graph reduces to an unlabeled set with S_N permutation symmetry. "
                + "No vertex has identity. No dynamics can be vertex-specific.",
                "Q is what breaks permutation symmetry and gives vertices identity."),

            new("L_Q construction", false,
                "COLLAPSES. L_Q = D_Q - A requires vertex charges to construct D_Q. "
                + "Without Q, D is just degree matrix — no charge-weighted Laplacian. "
                + "The operator that generates QM doesn't exist without Q.",
                "L_Q literally has Q in its name. Cannot construct without it."),

            new("Carrier formation", false,
                "COLLAPSES. Carriers are persistent information structures. "
                + "Without Q, there's no way to identify WHICH structure persists. "
                + "All carriers become indistinguishable blobs.",
                "Carriers need identity. Q provides it."),

            new("Species formation", false,
                "COLLAPSES. Species = classes of carriers. "
                + "Without carrier identity, there are no species — just a continuum.",
                "Taxonomy requires distinguishable individuals."),

            new("Ecology", false,
                "COLLAPSES. Ecology = interacting populations. "
                + "No distinguishable populations → no ecology.",
                "Ecology is built on countable species."),

            new("Evolution", false,
                "COLLAPSES. Darwinian triad needs identifiable individuals. "
                + "Variation: of WHAT? Selection: among WHAT? Heredity: from WHAT?",
                "Evolution without individuals is meaningless."),

            new("Complexity staircase", false,
                "COLLAPSES. L0-L6 counts levels of organization. "
                + "Organization requires organized THINGS.",
                "Complexity is complexity OF something."),

            new("Quantum Reality", true,
                "SURVIVES. Quantum Reality (R=1,S=1) is a POINT in (R,S) space. "
                + "The point exists regardless of whether we can count entities within it. "
                + "QM at (1,1) is a structural fact about the operator algebra.",
                "QM doesn't need Q to be derived — only to be APPLIED to countable systems."),

            new("Hilbert space", true,
                "SURVIVES. Hilbert space = complete inner product space. "
                + "L²(R) is a valid Hilbert space with no discrete labeling. "
                + "Hilbert structure is independent of Q.",
                "But Q is needed to make the basis physically meaningful."),

            new("Schrödinger equation", true,
                "SURVIVES. i∂ψ/∂t = Hψ is a PDE. "
                + "It holds for any wavefunction, labeled or not. "
                + "But ψ must be a function ON something — what's the domain?",
                "The equation survives. The interpretation doesn't."),

            new("Born rule", true,
                "SURVIVES. P = |ψ|² is a probability measure. "
                + "Works on any Hilbert space, labeled or not.",
                "Probability needs events. Events need distinguishable outcomes. Q provides them."),

            new("Measurement", true,
                "SURVIVES as a mystery. Collapse is irreducible regardless of Q.",
                "The measurement problem is independent of entity labeling."),
        };
    }

    public static string[] WhatQReallyIs()
    {
        return new[]
        {
            "Q is the DISCRETENESS PRIMITIVE — the principle that reality consists of COUNTABLE, DISTINGUISHABLE entities.",
            "Binary Q (0/1): 'This entity exists at this location' — equivalent to vertex existence in the graph.",
            "Integer Q (n): 'n entities exist at this location' — emergent from merger dynamics via additivity.",
            "Q as β₀: domain count in the PDE field configuration — emergent from the field.",
            "Q conservation: dQ/dt = 0 — emergent from the PDE reaction barrier (TQM-117).",
            "",
            "Q is PARTIALLY derivable:",
            "  - Integer Q follows from binary Q + additivity + conservation.",
            "  - Q as domain count follows from field configuration + threshold.",
            "  - Q conservation follows from PDE structure.",
            "",
            "Q is IRREDUCIBLE at its core:",
            "  - Binary Q = 'this entity exists' IS the graph vertex primitive.",
            "  - You cannot have a graph without vertices.",
            "  - You cannot have distinguishable entities without identity labels.",
            "  - Q IS the principle of individuation.",
            "",
            "Q cannot be eliminated because Q IS the graph.",
        };
    }
}
