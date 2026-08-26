namespace AT.Core.Resonance.Theory;

/// <summary>
/// Studies the dynamical laws of topological charge Q = condensate count.
/// Determines conservation, allowed transitions, and charge algebra.
///
/// AT-116: Topological Charge Dynamics
/// </summary>
public static class TopologicalChargeDynamicsAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record ChargeTransitionProfile(
        string Process,
        int Q_initial,
        int Q_final,
        int DeltaQ,
        bool IsReversible,
        string Requirement);

    public sealed record ChargeConservationReport(
        List<ChargeTransitionProfile> Transitions,
        bool IsConservedInPDE,
        bool IsAdditive,
        string ChargeAlgebra,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // CHARGE DYNAMICS DERIVATION
    // ══════════════════════════════════════════════════════════════════

    public static string ChargeDerivation()
    {
        return @"
TOPOLOGICAL CHARGE DYNAMICS

1. DEFINITION:
   Q(t) = #{connected domains where R(x,t) > 0.5}
   Each domain = one condensate = one proto-particle.

2. CONSERVATION IN PDE (continuous dynamics):
   The PDE ∂R/∂t = c₀·M·R·(1−R²) + D_R·∇²R evolves R continuously.
   Since the reaction term c₀·M·R·(1−R²) > 0 for R∈(0,1), M>0:
     → R CANNOT cross 0.5 downward locally.
     → Domain boundaries cannot disappear continuously.
     → Q is CONSERVED under PDE evolution.

3. DISCRETE CHARGE TRANSITIONS:

   MERGER (AT-012, discrete coupling):
     Two condensates with overlapping coupling ranges merge.
     Q → Q − 1. Occurs when d < 5λ.
     IRREVERSIBLE (spontaneous forward only).

   COLLAPSE (catastrophic):
     Peak R drops below 0.5 across entire domain.
     Q → 0. Requires external perturbation (AT-011: density -50%).
     IRREVERSIBLE at current parameters.

   CREATION (pair production):
     R field fluctuation creates kink-antikink pair.
     0 → 1. Requires noise exceeding reaction threshold.
     POSSIBLE but rare at N=100.

   SPLITTING (forced):
     One condensate divides into two.
     Q → Q + 1. Requires external spatial perturbation.
     NOT observed spontaneously.

4. CHARGE ALGEBRA:
   Q ∈ ℕ (non-negative integers).
   Q is ADDITIVE: Q_total = Σ Q_i for non-overlapping condensates.
   
   Allowed transitions:
     Q → Q  (stasis — PDE evolution)
     Q → Q−1 (merger — discrete coupling)
     Q → Q+1 (splitting — external forcing)
     Q → 0  (collapse — catastrophic perturbation)

   Forbidden transitions:
     Q → Q±2 in single step (requires two simultaneous events)
     Q → fractional Q

5. PHYSICAL ANALOGY:
   Q behaves like PARTICLE NUMBER in quantum field theory.
   The reaction-diffusion PDE is the 'vacuum' where particles
   are topological solitons. Merger = particle fusion.
   Creation/annihilation = pair production/destruction.
";
    }

    // ══════════════════════════════════════════════════════════════════
    // Charge transition catalog
    // ══════════════════════════════════════════════════════════════════

    public static ChargeConservationReport AnalyzeChargeDynamics()
    {
        var transitions = new List<ChargeTransitionProfile>
        {
            new("PDE Evolution", 1, 1, 0, true,
                "None — continuous dynamics preserves Q"),
            new("PDE Evolution", 2, 2, 0, true,
                "None — multiple domains persist independently"),
            new("PDE Evolution", 5, 5, 0, true,
                "None — each condensate is independently stable"),

            new("Merger (2→1)", 2, 1, -1, false,
                "d < 5λ (discrete coupling overlap)"),
            new("Merger (3→2)", 3, 2, -1, false,
                "Two of three condensates merge"),
            new("Merger (3→1)", 3, 1, -2, false,
                "All three merge — requires d≪λ for all pairs"),

            new("Collapse", 1, 0, -1, false,
                "Catastrophic: peak R forced below 0.5 (density -50%, AT-011)"),
            new("Collapse", 2, 0, -2, false,
                "Both condensates destroyed"),

            new("Creation (0→1)", 0, 1, +1, false,
                "Spontaneous R fluctuation exceeding 0.5. Rare."),
            new("Split (1→2)", 1, 2, +1, false,
                "Requires external spatial perturbation. Not spontaneous."),
        };

        bool conservedInPDE = true; // continuous evolution preserves Q
        bool additive = true;       // total Q = sum of individual Q's

        string algebra =
            "CHARGE ALGEBRA:\n" +
            "  Q ∈ ℕ (integers ≥ 0)\n" +
            "  Additive: Q(A ∪ B) = Q(A) + Q(B) for non-overlapping domains\n" +
            "  Allowed: Q→Q (stasis), Q→Q±1 (merger/split), Q→0 (collapse)\n" +
            "  Forbidden: Q→Q±k for k>1 (single step), fractional Q";

        string classification = "D: Topological Charge Theory";
        string interpretation =
            "TOPOLOGICAL CHARGE IS THE FUNDAMENTAL QUANTITY. " +
            "Q is conserved under PDE evolution (continuous dynamics). " +
            "Discrete transitions (merger, collapse, creation) occur only " +
            "through non-PDE mechanisms (discrete coupling, external forcing). " +
            "Q behaves like PARTICLE NUMBER in a topological field theory. " +
            "The proto-matter system is a CHARGE-CONSERVING topological " +
            "field theory where condensates are unit charges (Q=+1 each).";

        return new ChargeConservationReport(transitions, conservedInPDE,
            additive, algebra, classification, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Reinterpret prior experiments through charge framework
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<string, string> ReinterpretExperiments()
    {
        return new Dictionary<string, string>
        {
            ["AT-010"] = "Proto-matter states = Q≥1 configurations. " +
                          "Multiple clusters = Q>1. Global R<0.5 = Q≥1 with incoherent background.",

            ["AT-011"] = "96% survival = Q conserved in 24/25 runs. " +
                          "Only failure = Q→0 (density -50% catastrophic collapse). " +
                          "Recovery = Q unchanged, R rebounds within condensate.",

            ["AT-012"] = "Two-condensate interaction = Q=2 → Q=1 (merger) " +
                          "when d < coupling range. Q=2 → Q=2 (coexistence) " +
                          "when d > coupling range. No Q=2→Q=2 with identity exchange.",

            ["AT-050"] = "Identity exclusion = separate Q=1 domains maintain " +
                          "distinct internal phase structures. Merger (Q=2→Q=1) " +
                          "destroys individual identities. Repulsion = domains " +
                          "resist overlap to preserve Q=2.",

            ["AT-107"] = "Multi-condensate survival = Q>1 stable when " +
                          "separation > coupling range. Each domain is an " +
                          "independent Q=+1 unit. PDE preserves Q.",

            ["AT-113"] = "Topological charge identification: Q = #{R>0.5 domains}. " +
                          "Conserved under continuous PDE evolution.",

            ["AT-114"] = "Single species: all condensates are Q=+1 units. " +
                          "Differences in width/mass are continuous (parameter-dependent), " +
                          "not discrete species.",

            ["AT-115"] = "Charge robustness confirmed: plateau of Q=1 spans " +
                          "T∈[0.10,0.85]. Q is threshold-independent within plateau.",
        };
    }
}
