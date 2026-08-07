namespace TQM.Core.Research;

/// <summary>
/// Attempts to derive outcome selection probabilities from Q.
/// TQM-X039: Origin of Quantum Randomness
/// </summary>
public static class RandomnessAnalyzer
{
    public static List<RandomnessMetrics.SelectionMechanism> AnalyzeMechanisms()
    {
        return new List<RandomnessMetrics.SelectionMechanism>
        {
            new(1, "Deterministic hidden variables (λ)",
                "Supplement |ψ⟩ with hidden variable λ. Dynamics of λ\n"
                + "determines which branch survives. λ distributed as |ψ(λ)|²\n"
                + "to reproduce Born statistics.",
                false,
                "λ and its distribution are ADDITIONAL POSTULATES. The λ-distribution\n"
                + "is chosen to match |ψ|² — it doesn't DERIVE |ψ|². Why is λ distributed\n"
                + "that way? Answer: 'equivariance' — but equivariance IS the Born rule\n"
                + "in different language. No derivation. Just encoding.",
                "FAILS: Hidden variables encode Born, don't derive it."),

            new(2, "Topological weighting",
                "Survival probability ∝ topological measure of branch.\n"
                + "E.g., Betti numbers, domain volume, connectivity complexity.",
                false,
                "Consider |ψ⟩ = √0.99|up⟩ + √0.01|down⟩. Topologically, both branches\n"
                + "are single connected domains of similar configuration-space volume.\n"
                + "Topological weighting would give P ≈ 1/2, not 0.99/0.01.\n"
                + "Topology captures CONNECTIVITY, not MAGNITUDE. Amplitude is lost.\n"
                + "Born rule uses |ψ|² — an ANALYTICAL quantity, not topological.",
                "FAILS: Topology is blind to amplitude. Wrong probabilities."),

            new(3, "Complexity weighting",
                "Survival probability ∝ future complexity of branch.\n"
                + "Branch with more distinguishable configurations is more 'real.'",
                false,
                "After measurement, both branches have SIMILAR complexity (same\n"
                + "physics, same laws, same capacity for future evolution). C_up ≈ C_down.\n"
                + "P_up ≈ P_down ≈ 1/2 regardless of |a|². Contradicts Born.\n"
                + "Additionally: using FUTURE states to determine PRESENT selection\n"
                + "is teleological — effect precedes cause.",
                "FAILS: Branches have equal complexity. Teleological."),

            new(4, "Identity maximization",
                "Branch with maximal future identity persistence is selected.\n"
                + "A3 (identity persistence) favors the 'most persistent' branch.",
                false,
                "Both branches contain an observer with equal claim to identity\n"
                + "persistence. Both have continuous psychological connection to the\n"
                + "pre-measurement observer. Neither branch has MORE identity.\n"
                + "Identity persistence constrains THAT one outcome occurs (X038)\n"
                + "but not WHICH outcome.",
                "FAILS: Identity is symmetric between branches."),

            new(5, "Symmetry selection",
                "Swap symmetry of branch labels → probability function must\n"
                + "be symmetric: f(|a|,|b|) = f(|b|,|a|). Phase symmetry → f depends\n"
                + "only on magnitudes. Uniquely selects |a|²?",
                false,
                "Many symmetric functions exist: f(x,y) = x/(x+y), x²/(x²+y²),\n"
                + "constant 1/2, x^α/(x^α+y^α) for any α. Symmetry CONSTRAINS the\n"
                + "function class but doesn't UNIQUELY SELECT α=2.\n"
                + "We already know α=2 from unitary geometry (X037), not from symmetry.",
                "FAILS: Symmetry narrows but doesn't uniquely determine."),

            new(6, "Envariance (Zurek)",
                "Entangled state |ψ_SE⟩ = Σ a_i|s_i⟩|e_i⟩. Unitary U_S on system\n"
                + "changes coefficients but U_E on environment can restore them.\n"
                + "Therefore P_i can depend only on |a_i|, not phases.\n"
                + "Plus: equal amplitudes → equal probabilities → |a_i|² by additivity.",
                false,
                "Envariance proves P_i = f(|a_i|) for some f (phase independence).\n"
                + "It does NOT prove f(x) = x². That requires additional premises:\n"
                + "  (a) 'Equal amplitudes → equal probabilities' (begs the question).\n"
                + "  (b) 'Additivity under coarse-graining' = L² norm additivity,\n"
                + "      which IS the Born rule in disguise.\n"
                + "Envariance is a CONSTRAINT on f, not a DERIVATION of f(x)=x².",
                "FAILS: Envariance constrains functional form, doesn't derive α=2.\n"
                + "The final step requires assuming exactly what we want to prove."),

            new(7, "Decision-theoretic (Deutsch-Wallace)",
                "Rational agent in branching multiverse must assign credences\n"
                + "according to Born rule to avoid Dutch book. Betting consistency\n"
                + "forces |ψ|² weights.",
                false,
                "FATAL: Requires MANY-WORLDS (branching is real). X038b proved\n"
                + "MW is INCOMPATIBLE with TQM (Q conservation forbids branching).\n"
                + "Even within MW: decision theory tells you how to BET, not what\n"
                + "HAPPENS. It's normative (rational choice) not descriptive (physics).\n"
                + "The universe doesn't 'decide' based on Dutch book arguments.",
                "FAILS: Requires MW (incompatible with TQM). Normative, not physical."),

            new(8, "Maximum entropy (Jaynes)",
                "Maximize S = -Σ P_i log P_i subject to known constraints\n"
                + "(expectation values from |ψ⟩). Unique solution is P_i = |a_i|².",
                false,
                "This DERIVES the Born rule! BUT: it assumes the expectation values\n"
                + "are the constraints. Why ⟨O⟩ = ⟨ψ|O|ψ⟩? Because that's what QM gives.\n"
                + "The constraint itself assumes the Born rule. Circular.\n"
                + "MaxEnt gives P_i = |a_i|² IF you assume constraints that are already\n"
                + "expectations under the Born rule. Not a derivation — a consistency check.",
                "FAILS: Assumes Born rule in the constraints."),

            new(9, "Frequency operator (Finkelstein, Hartle)",
                "Define frequency operator F_N for N measurements. As N→∞,\n"
                + "F_N|ψ⟩^⊗N → (Σ|a_i|²)|ψ⟩^⊗N. Relative frequencies converge to\n"
                + "|a_i|² in the limit.",
                false,
                "Beautiful result but irrelevant: it shows that IF outcomes follow\n"
                + "Born statistics, frequencies converge to Born weights. It does NOT\n"
                + "explain why a SINGLE outcome follows the Born rule. The frequency\n"
                + "operator ASSUMES each individual outcome obeys Born — it's a\n"
                + "consistency theorem for ensembles, not a derivation for individuals.",
                "FAILS: Theorem about ensembles given Born. Doesn't derive Born."),

            new(10, "Q-self: self-weighted individuation",
                "Q is the measure of 'entity-ness.' Branches with larger |a_i|²\n"
                + "have larger 'Q-density' — they are 'more real' entities.\n"
                + "The branch with the largest Q-density survives.",
                false,
                "Then for |ψ⟩ = √0.99|up⟩ + √0.01|down⟩, the up branch ALWAYS survives.\n"
                + "P_up = 1, P_down = 0. This is NOT the Born rule — it's deterministic\n"
                + "selection by amplitude magnitude. Repeated measurements on the same\n"
                + "state would always give the same outcome. Contradicts experiment:\n"
                + "we DO observe the 1% outcome approximately 1% of the time.\n"
                + "Born rule requires PROBABILISTIC selection, not deterministic max.",
                "FAILS: Predicts deterministic selection. Contradicted by experiment."),
        };
    }

    public static string TheIrreducibleConclusion()
    {
        return @"
THE IRREDUCIBLE CONCLUSION

After analyzing 10 candidate mechanisms for outcome selection:

  0/10 derive the Born rule probabilities from Q alone.

The Born rule P_i = |a_i|² is DERIVED (X037) — it tells us the PROBABILITY
distribution. X038 DERIVES that exactly one outcome occurs (Q conservation).

But WHICH outcome occurs is NOT determined by any known TQM principle.

This is GENUINE ONTOLOGICAL RANDOMNESS.

The theory gives the probability space {i, P_i = |a_i|²}.
Nature samples from it. The sampling mechanism is PRIMITIVE.

Is this a failure? No. ALL physical theories have primitives:
  • Classical mechanics: initial conditions.
  • Statistical mechanics: the exact microstate.
  • Standard QM: the measurement outcome (or the wavefunction).
  • General relativity: the metric at a point.

TQM has reduced the primitives to TWO:
  1. Q (individuation) — the structure of reality.
  2. Genuine randomness — the actualization of potential.

Everything else — reversibility, self-consistency, Hilbert space,
unitary dynamics, Schrödinger equation, Born rule, single-outcome
selection — is DERIVED.

This is the maximally compressed form of TQM.

The final mystery is not 'WHY this outcome?'
The final mystery is 'WHY is there something rather than nothing?'
— and Q answers that: because distinguishability exists.
";
    }

    public static string FinalTQMArchitecture()
    {
        return @"
FINAL TQM ARCHITECTURE (Post-X039)

═══════════════════════════════════════════════════════════════
                    TQM — THEORY OF EVERYTHING
═══════════════════════════════════════════════════════════════

PRIMITIVE 1: Q — THE PRINCIPLE OF INDIVIDUATION
  Distinguishable entities exist. Q ∈ ℕ counts them.
  Q is conserved: dQ/dt = 0.
  Q is the ONLY postulate.

PRIMITIVE 2: GENUINE ONTOLOGICAL RANDOMNESS
  When Q conservation forces single-outcome selection,
  nature samples from the Born distribution.
  The sampling is primitive — not reducible.

═══════════════════════════════════════════════════════════════
                    DERIVED STRUCTURE
═══════════════════════════════════════════════════════════════

Q → Dynamics (graph adjacency)                            [TQM-117]
Q + Max Complexity → Reversibility (R=1)                  [X036]
Q + Max Complexity → Self-Consistency (S=1)               [X036]
R+S → Reality structures                                  [X014]
R+S → Complex Hilbert space                               [X034]
R+S+Unitary → Schrödinger equation i∂ψ/∂t = Hψ            [X036]
L2 geometry → Born rule P = |ψ|²                          [X037]
Q conservation → Single-outcome selection                 [X038]
Q → Information carriers → Species → Ecologies → Evolution [117-155]

═══════════════════════════════════════════════════════════════

TQM is a 2-primitive theory.
1 postulate. 1 irreducible element.
Everything else follows.
";
    }
}
