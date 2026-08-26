namespace AT.Core.Research;

/// <summary>
/// Hostile audit: attempt to save Many-Worlds from the Q-conservation collapse argument.
/// AT-X038b: Hostile Audit of the Q-Conservation Collapse Argument
/// </summary>
public static class CollapseHostileAudit
{
    public static List<CollapseAuditMetrics.MwDefense> ExecuteDefenses()
    {
        return new List<CollapseAuditMetrics.MwDefense>
        {
            new(1, "Q counts the global wavefunction, not branches",
                "|Ψ⟩ = a|up⟩|obs-up⟩ + b|down⟩|obs-down⟩ is ONE vector in Hilbert space.\n"
                + "It is a sum, not a set. Q(|Ψ⟩) = 1 because it's one mathematical object.\n"
                + "Branches are not 'things' — they're TERMS in a sum.",
                "Q = β₀({R>0.5}) is a GEOMETRIC definition (AT-117). In configuration space,\n"
                + "|Ψ(x₁,x₂)|² has support on TWO disconnected regions where |Ψ|² is large.\n"
                + "The superlevel set has β₀ = 2, not 1. Q counts DOMAINS, not algebraic terms.\n"
                + "A superposition of two localized wavepackets IS two domains, regardless of\n"
                + "how many '+' signs appear in the algebraic expression.",
                false,
                "Q is defined TOPOLOGICALLY (β₀), not algebraically. The configuration-space\n"
                + "representation reveals distinct domains that the algebraic form conceals.\n"
                + "One vector ≠ one domain when the vector is a superposition of disjoint supports."),

            new(2, "Q is defined WITHIN branches, not across them",
                "Within each branch: Q_branch = 2 (system + observer). Q is well-defined\n"
                + "INTRA-branch. Cross-branch Q is undefined because branches are separate\n"
                + "'worlds' with no interaction.",
                "If Q is only intra-branch, then the TOTAL Q of the universe is UNDEFINED.\n"
                + "X035: Q is the irreducible principle of individuation — it must be\n"
                + "WELL-DEFINED for the whole system at all times. A fundamental principle\n"
                + "cannot be 'undefined for the whole but defined for parts.'\n"
                + "Compare: energy is defined globally. 'Energy is only defined within\n"
                + "branches' would mean energy is not conserved globally. Same for Q.",
                false,
                "Q must be GLOBALLY defined. The whole point of Q is that it counts\n"
                + "ALL distinguishable entities in the universe. Branch-relative Q\n"
                + "abandons the global concept, which abandons individuation."),

            new(3, "Branch decomposition depends on basis choice",
                "|Ψ⟩ = a|up⟩|obs-up⟩ + b|down⟩|obs-down⟩ is not unique. In the rotated basis\n"
                + "|±⟩ = (|up⟩±|down⟩)/√2, the 'branches' are different. Q depends on which\n"
                + "basis you use → Q is not objective → Q conservation is meaningless.",
                "Decoherence selects a PREFERRED BASIS — the pointer basis (Zurek's\n"
                + "einselection). For macroscopic objects, position is the pointer basis\n"
                + "because environmental interactions are local in position. The branch\n"
                + "decomposition in the pointer basis is PHYSICALLY PRIVILEGED.\n"
                + "Q, computed in the pointer basis, is objective. The fact that you CAN\n"
                + "write |Ψ⟩ in other bases doesn't change the physical Q-count — just as\n"
                + "you CAN express energy in different units but the physical energy is fixed.",
                false,
                "Decoherence provides a UNIQUE preferred basis. Branch decomposition in\n"
                + "that basis is objective. Q computed in the pointer basis is well-defined.\n"
                + "Basis dependence of the naive count is resolved by einselection."),

            new(4, "Decoherent sectors are ONE entity with internal structure",
                "The universal wavefunction is ONE entity. Its support on two configuration-\n"
                + "space regions is just internal structure — like a dumbbell has two lobes\n"
                + "but is one object. Q=1 for the universe always. No Q violation.",
                "If Q=1 always, then individuation does not exist. A universe with one\n"
                + "electron and a universe with 10^80 particles both have Q=1. Q loses all\n"
                + "content as 'number of things.' X035 proved Q is the principle of\n"
                + "INDIVIDUATION — it MUST distinguish one entity from many.\n"
                + "If 'one entity with internal structure' is always Q=1, then the entire\n"
                + "concept of distinguishable entities collapses. Q becomes trivial.\n"
                + "This defense SAVES Q conservation but DESTROYS Q's meaning.",
                false,
                "Trivializing Q (Q≡1 always) preserves conservation but eliminates\n"
                + "individuation. This contradicts X035. The cost of saving MW is\n"
                + "abandoning the deepest principle of AT. Too expensive."),

            new(5, "Observer identity splits — both continuations are valid",
                "Pre-measurement: one observer. Post: two observers, each with continuous\n"
                + "psychological connection to the pre-measurement observer. Identity is\n"
                + "not a fundamental concept — it's an emergent pattern that CAN split.\n"
                + "This is like a cell dividing: one becomes two, both are 'the same' cell.",
                "Cell division is PHYSICAL splitting where both daughters exist in the\n"
                + "SAME world. MW splitting puts observers in DIFFERENT worlds with no\n"
                + "causal connection. More fundamentally: A3 (identity persistence) from\n"
                + "X036 says identity is a single trajectory. Splitting → two trajectories\n"
                + "→ identity not preserved. MW must REJECT A3, which means rejecting\n"
                + "the foundation of the complexity-to-quantum theorem. The cost is the\n"
                + "entire derived structure (R, S, Hilbert, Schrödinger, Born).",
                false,
                "Rejecting A3 (identity persistence) unravels the ENTIRE AT derivation\n"
                + "chain from X036. Without identity persistence, there's no self-consistency\n"
                + "requirement, no species, no carriers. MW can't just reject A3 — it must\n"
                + "rebuild the whole theory without it."),

            new(6, "Redefine Q as total measure of existence",
                "Q_new = ∫|ψ|² dx = ‖ψ‖² = 1 (always). This 'Q' is trivially conserved.\n"
                + "The L² norm is the new individuation principle. Entities are distinguished\n"
                + "by their contribution to the total norm, not by domain count.",
                "This is just the L² norm, which is already captured by reversibility (R).\n"
                + "Q in AT is a DIFFERENT concept — it counts TOPOLOGICAL domains, not\n"
                + "probabilistic measure. If Q = ‖ψ‖², then Q provides zero individuation:\n"
                + "all normalized states have Q=1, regardless of entity count.\n"
                + "This is not a 'redefinition' of Q — it's ELIMINATING Q and replacing\n"
                + "it with something already in the theory (R). The principle of\n"
                + "individuation would be lost entirely.",
                false,
                "Q=‖ψ‖² is just R (reversibility). Q is DEFINED as β₀({R>0.5}) in AT.\n"
                + "Changing the definition changes the theory. This is not a defense of\n"
                + "MW within AT — it's a proposal for a DIFFERENT theory."),

            new(7, "Q conservation is a classical theorem, not quantum",
                "AT-116 derived Q conservation for the CLASSICAL PDE. The quantum regime\n"
                + "(R=1,S=1) may have different conservation laws. Q may not be conserved\n"
                + "in the full quantum theory. MW is compatible with quantum AT even if\n"
                + "it violates classical Q conservation.",
                "The classical PDE is the MEAN-FIELD LIMIT of the quantum dynamics.\n"
                + "At R=1,S=1, the reaction barrier is MAXIMAL — Q conservation should\n"
                + "be even STRONGER in the quantum regime. If anything, the classical\n"
                + "derivation is a LOWER BOUND on conservation strength.\n"
                + "Additionally: Q is β₀ of the superlevel set. If the quantum state\n"
                + "develops two disconnected domains, β₀=2 is a mathematical fact,\n"
                + "not a classical approximation. Topology is basis-independent.",
                false,
                "Q = β₀ is a topological invariant. It doesn't depend on whether the\n"
                + "dynamics are classical or quantum — it depends only on the geometry\n"
                + "of the state. Two disconnected domains → β₀=2 → Q=2. Always."),
        };
    }

    public static List<CollapseAuditMetrics.BranchCountTheorem> BuildTheorems()
    {
        return new List<CollapseAuditMetrics.BranchCountTheorem>
        {
            new("Single spin measurement",
                2, 3, 2,
                "2 → 3 if branching, 2 → 2 if collapse. Q conservation selects collapse."),

            new("N sequential measurements",
                2, 1000, 2,
                "Exponential Q growth: 2→2+2^N. Q conservation forbids this."),

            new("Wigner's friend",
                3, 5, 3,
                "Observer + friend + system = 3. Branching → ≥5. Collapse → 3."),

            new("Schrödinger's cat",
                2, 3, 2,
                "Atom + cat = 2. Dead+alive superposition → 3 (two cat domains). Collapse → 2."),
        };
    }

    public static string TheBranchCountTheorem()
    {
        return @"
BRANCH-COUNT THEOREM

THEOREM: For any measurement-like interaction producing N macroscopically
         distinct outcomes, Q(total) under branching satisfies:

           Q_branch ≥ Q_initial + (N-1)·Q_apparatus

         Under collapse: Q_collapse = Q_initial.

         Q conservation ⇒ branching is forbidden.

PROOF: Each macroscopic outcome creates a new connected domain in
        configuration space for the apparatus pointer state. These
        domains are distinguishable (different pointer positions) and
        each has Q ≥ 1 (it's a persistent entity). Summing:

        Q_branch = Q_system + N·Q_apparatus_domain + Q_environment
        Q_initial = Q_system + 1·Q_apparatus + Q_environment

        Since N ≥ 2 for any measurement: Q_branch > Q_initial.

        The only way Q_branch = Q_initial is N = 1 — a SINGLE outcome.
        This is collapse.

COROLLARY: Many-Worlds is INCOMPATIBLE with Q conservation.
           Q conservation is a theorem of AT (AT-116).
           Q is the irreducible principle of individuation (X035).
           Therefore: Many-Worlds is INCOMPATIBLE with AT.
";
    }
}
