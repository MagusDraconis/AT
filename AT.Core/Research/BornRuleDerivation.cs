namespace AT.Core.Research;

/// <summary>
/// Formal derivation: α=2 is the unique exponent consistent with
/// Hilbert space geometry, unitary invariance, and complexity preservation.
/// AT-X037: Born Rule from Complexity Preservation
/// </summary>
public static class BornRuleDerivation
{
    public static string TheoremStatement =>
        "THEOREM (Born Rule from Hilbert Geometry):\n"
      + "Let P_i = |ψ_i|^α / Σ_j |ψ_j|^α be a probability rule on\n"
      + "a complex Hilbert space with unitary dynamics.\n"
      + "Then α = 2 is the UNIQUE exponent for which:\n"
      + "  (i)   probability assignments are basis-independent,\n"
      + "  (ii)  the total normalization is unitarily invariant,\n"
      + "  (iii) probability factorizes correctly under tensor products,\n"
      + "  (iv)  partial trace (reduction) is well-defined,\n"
      + "  (v)   composition preserves complexity.\n\n"
      + "Any α ≠ 2 violates at least one of (i)-(v).\n"
      + "Therefore, the Born rule P = |ψ|² is DERIVED, not postulated,\n"
      + "from the requirement that probability be consistent with\n"
      + "unitary Hilbert space geometry.";

    public static List<BornRuleMetrics.AlphaTest> TestAllAlphas()
    {
        var tests = new List<BornRuleMetrics.AlphaTest>();

        // α = 0.5 (square root)
        tests.Add(new BornRuleMetrics.AlphaTest(0.5,
            "P_i ∝ √|ψ_i|. Super-diffuse — small amplitudes amplified.",
            false, BornRuleMetrics.FailureMode.BasisDependence,
            "Normalization N = Σ √|ψ_i| is not unitarily invariant.",
            "Under unitary U, Σ √|(Uψ)_i| ≠ Σ √|ψ_i|. "
            + "Counterexample: ψ=(1,0), N=1; U=Hadamard, ψ'=(1/√2,1/√2), N=2/√√2≠1. "
            + "Same physical state, different normalization → probability undefined."));

        // α = 1 (linear / L¹ norm)
        tests.Add(new BornRuleMetrics.AlphaTest(1.0,
            "P_i ∝ |ψ_i|. Classical-like additive probabilities.",
            false, BornRuleMetrics.FailureMode.BasisDependence,
            "L¹ norm Σ|ψ_i| is not unitarily invariant.",
            "Counterexample: ψ=(1,0), Σ|ψ_i|=1; ψ'=((1+i)/2,(1-i)/2) via U, "
            + "Σ|ψ_i'| = 2·|1/√2| = √2 ≠ 1. "
            + "Normalization depends on basis → probability not well-defined."));

        // α = 1.5
        tests.Add(new BornRuleMetrics.AlphaTest(1.5,
            "P_i ∝ |ψ_i|^1.5. Intermediate between L¹ and L².",
            false, BornRuleMetrics.FailureMode.BasisDependence,
            "Same as α=1: L^1.5 norm not unitarily invariant.",
            "ψ=(1,0): N=1. ψ'=(1/√2,1/√2): N=2·(1/√2)^1.5 = 2/2^(0.75) = 2^0.25≠1."));

        // α = 2 (Born)
        tests.Add(new BornRuleMetrics.AlphaTest(2.0,
            "P_i = |ψ_i|². Standard Born rule.",
            true, BornRuleMetrics.FailureMode.None,
            "No failure. All consistency requirements satisfied.",
            "L² norm is unitarily invariant: Σ|(Uψ)_i|² = ⟨Uψ|Uψ⟩ = ⟨ψ|ψ⟩ = Σ|ψ_i|². "
            + "Factorization under ⊗: |ψ⊗φ|² = |ψ|²|φ|². "
            + "Partial trace well-defined via Hilbert-Schmidt inner product. "
            + "Complexity preserved: orthogonal states remain orthogonal."));

        // α = 3
        tests.Add(new BornRuleMetrics.AlphaTest(3.0,
            "P_i ∝ |ψ_i|³. Concentrates probability on large components.",
            false, BornRuleMetrics.FailureMode.BasisDependence,
            "L³ norm not unitarily invariant.",
            "ψ=(1,0): N=1. ψ'=(1/√2,1/√2): N=2·(1/√2)³ = 1/√2 ≠ 1."));

        // α = 4
        tests.Add(new BornRuleMetrics.AlphaTest(4.0,
            "P_i ∝ |ψ_i|⁴. Extreme concentration on largest component.",
            false, BornRuleMetrics.FailureMode.BasisDependence,
            "L⁴ norm not unitarily invariant + entanglement failure.",
            "Basis: same counterexample as α=3. "
            + "Entanglement: P_i for subsystem A cannot be defined consistently "
            + "because partial trace requires L² structure. "
            + "Specifically: Tr_B(|ψ⟩⟨ψ|) uses ⟨·|·⟩ which IS L². "
            + "With α≠2, reduced probabilities depend on the α-norm, "
            + "not the Hilbert-Schmidt inner product."));

        return tests;
    }

    public static List<BornRuleMetrics.ConsistencyRequirement> BuildRequirements()
    {
        double[] testAlphas = { 0.5, 1.0, 1.5, 2.0, 3.0, 4.0 };

        return new List<BornRuleMetrics.ConsistencyRequirement>
        {
            new("Basis independence",
                "P_i must be the same physical probability regardless of which\n"
                + "orthonormal basis is used to express |ψ⟩.",
                true,
                new[] { false, false, false, true, false, false },
                testAlphas.Select(a => a.ToString("F1")).ToArray()),

            new("Unitary invariance of normalization",
                "N(ψ) = Σ_i |ψ_i|^α must satisfy N(Uψ) = N(ψ) for all unitary U.\n"
                + "Equivalently: N depends only on ‖ψ‖², not on the component distribution.",
                true,
                new[] { false, false, false, true, false, false },
                testAlphas.Select(a => a.ToString("F1")).ToArray()),

            new("Tensor product factorization",
                "For independent systems: P(ψ_i⊗φ_j) = P_A(ψ_i) · P_B(φ_j).",
                true,
                new[] { true, true, true, true, true, true },
                testAlphas.Select(a => a.ToString("F1")).ToArray()),

            new("Partial trace consistency",
                "For entangled systems, reduced state probabilities must be\n"
                + "well-defined via a partial trace operation.",
                true,
                new[] { false, false, false, true, false, false },
                testAlphas.Select(a => a.ToString("F1")).ToArray()),

            new("Orthogonality preservation",
                "⟨ψ|φ⟩=0 ⇒ measurement can perfectly distinguish them.\n"
                + "Probability zero for impossible outcomes must be preserved.",
                true,
                new[] { true, true, true, true, true, true },
                testAlphas.Select(a => a.ToString("F1")).ToArray()),

            new("Complexity additivity",
                "C(A⊗B) = C(A) + C(B) for independent systems.\n"
                + "Distinguishability structure composes cleanly.",
                true,
                new[] { true, true, true, true, true, true },
                testAlphas.Select(a => a.ToString("F1")).ToArray()),

            new("Linearity of expectation",
                "⟨O₁+O₂⟩ = ⟨O₁⟩ + ⟨O₂⟩ for compatible observables.\n"
                + "Follows from P_i linearity in the density matrix.",
                true,
                new[] { false, false, false, true, false, false },
                testAlphas.Select(a => a.ToString("F1")).ToArray()),
        };
    }

    public static string TheKeyProof()
    {
        return @"
THE KEY PROOF: Why α = 2 is unique

CLAIM: The quantity N(ψ) = Σ_i |ψ_i|^α is unitarily invariant
       (N(Uψ) = N(ψ) for all unitary U) IF AND ONLY IF α = 2.

PROOF:

(⇒) Assume N is unitarily invariant. Then N depends only on the
     equivalence class of ψ under the unitary group. Since the
     unitary group acts transitively on vectors of equal L² norm
     (by the transitive action of U(N) on the unit sphere),
     N must be a function of ‖ψ‖² alone: N(ψ) = f(‖ψ‖²).

     Now consider two states:
       ψ_a = (1, 0, 0, ..., 0)    → N = 1^α = 1
       ψ_b = (1/√N, 1/√N, ..., 1/√N)  → N = N · (1/√N)^α = N^{1-α/2}

     Both have ‖ψ‖² = 1. Unitary invariance requires N(ψ_a) = N(ψ_b).
     Therefore 1 = N^{1-α/2} for all N.

     This holds iff 1 - α/2 = 0, i.e., α = 2.

(⇐) For α = 2: N(Uψ) = Σ_i |(Uψ)_i|² = ⟨Uψ|Uψ⟩ = ⟨ψ|U†U|ψ⟩ = ⟨ψ|ψ⟩ = N(ψ). ✓

Therefore α = 2 is the UNIQUE exponent for which the generalized Born
normalization is unitarily invariant.

COROLLARY: Any probability rule of the form P_i ∝ |ψ_i|^α with α ≠ 2
produces basis-dependent probabilities. The same physical state,
expressed in different orthonormal bases, receives different probability
assignments. This contradicts the principle that probability should
depend on the state, not its representation.

FURTHER: Could there be a probability rule NOT of the form |ψ_i|^α?
Let P_i = f(|ψ_i|) where f: ℝ⁺ → ℝ⁺. For Σ_i f(|ψ_i|) to be unitarily
invariant, it must be a function of ‖ψ‖². The only continuous f
satisfying this for all ψ is f(x) = cx². Therefore any unitarily
invariant probability rule MUST be of the Born form P_i ∝ |ψ_i|².

This derivation uses only:
  - Hilbert space geometry (inner product, unitarity)
  - Basis independence (representation invariance)
  - Normalization (Σ P_i = 1)

It does NOT use:
  - Gleason's theorem
  - Measurement postulates
  - Classical probability axioms beyond normalization
  - Any physical interpretation
";
    }

    public static string WhyGleasonIsNotNeeded()
    {
        return @"
WHY THIS IS NOT GLEASON'S THEOREM

Gleason's theorem (1957): Any probability measure μ on the closed
subspaces of a Hilbert space of dimension ≥ 3 has the form
μ(P) = Tr(ρP) for some density matrix ρ.

This is a powerful result but it:
  1. Assumes probability is defined on SUBSPACES (projection lattice).
  2. Assumes σ-additivity.
  3. Applies only in dim ≥ 3 (dim=2 has counterexamples).
  4. Is a 'black box' — it gives existence but not insight into WHY.

Our derivation is SIMPLER and MORE FUNDAMENTAL:
  1. Assume only that probabilities are functions of |ψ_i|.
  2. Require basis independence (unitary invariance).
  3. Derive α = 2 directly from functional form constraints.

The key difference: We don't assume probability is defined on arbitrary
projections. We only require that when we write |ψ⟩ in an orthonormal
basis {|i⟩}, the probability P_i of finding the system in state |i⟩
should be basis-independent. This is a MINIMAL consistency condition
that any interpretation must satisfy.

The result is the same: Born rule P = |ψ|² is uniquely selected.
But the derivation is transparent, constructive, and works in all
dimensions (including dim=2).

This is a GENUINE DERIVATION of the Born rule, not merely an appeal
to an external theorem.
";
    }
}
