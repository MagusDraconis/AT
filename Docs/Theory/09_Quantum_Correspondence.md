# 9. Quantum Correspondence

## Emergence of Quantum Mechanics from Q Networks

---

## Derivation Chain

```
Q (topological charge, Postulate 1)
    ↓ interaction graph
L_Q (graph Laplacian)
    ↓ symmetry: L_Q^T = L_Q
Hilbert space (eigenvector basis, real eigenvalues)
    ↓ norm conservation (Postulate 2: d/dt ||ψ||² = 0)
M^T = -M (antisymmetric evolution generator)
    ↓ simplest 2×2 antisymmetric
J = [[0,1],[-1,0]] (complex structure)
    ↓ J² = -I
i (imaginary unit emerges as J)
    ↓ M = J ⊗ L_Q
i∂ψ/∂t = L_Q ψ (Schrödinger equation)
    ↓ stationary states
ψ_k(t) = exp(-iλ_k t) · v_k (quantum evolution)
    ↓ additivity (Postulate 3)
P = |ψ|² (Born rule, Gleason's theorem)
    ↓ measurement (Postulate 4)
Outcome selection / collapse
```

---

## What Is Derived

| Quantum Structure | AT Derivation | Source |
|------------------|---------------|--------|
| Hilbert space | L_Q eigenvectors form orthonormal basis | AT-149 |
| Complex structure | J from antisymmetric coupling | AT-150 |
| Imaginary unit i | J² = -I → J ≡ i | AT-150 |
| Schrödinger equation | M = J⊗L_Q → i∂ψ/∂t = L_Q ψ | AT-149 |
| Unitary evolution | ψ(t) = exp(-iL_Q t) ψ(0) | AT-149 |
| Stationary states | ψ_k = exp(-iλ_k t) v_k | AT-149 |
| Norm conservation | d/dt ||ψ||² = 0 from antisymmetry | AT-151 |
| Born rule uniqueness | Gleason's theorem (|ψ|² only additive measure) | AT-153 |

---

## What Is NOT Derived (Postulated)

| Quantum Structure | Status | Source |
|------------------|--------|--------|
| Q exists | Postulate 1 | AT-117-122 |
| Reversible dynamics | Postulate 2 | AT-152 |
| Born rule (additivity) | Postulate 3 | AT-153 |
| Measurement (collapse) | Postulate 4 | AT-154 |

---

## Decoherence (Partial Explanation)

System S coupled to environment E on Q-networks:

**H = L_S ⊗ I_E + I_S ⊗ L_E + g · V_int**

- Reduced density matrix ρ_S(t) → diagonal in pointer basis
- Purity Tr(ρ²) ~ exp(-γt) with γ ∝ g²·N_env
- Born statistics emerge on diagonal
- Pointer states = eigenstates of interaction Hamiltonian

**What decoherence explains**: Why interference disappears, which basis is stable.

**What decoherence does NOT explain**: Why ONE outcome occurs, how collapse happens.

---

## AT vs Standard QM — Postulate Count

| Postulate | Standard QM | AT |
|----------|------------|-----|
| Hilbert space | POSTULATED | DERIVED (from L_Q) |
| Observables = Hermitian operators | POSTULATED | DERIVED (L_Q is Hermitian) |
| Schrödinger equation | POSTULATED | DERIVED (from reversibility) |
| Born rule | POSTULATED | POSTULATED (Gleason provides uniqueness) |
| Measurement | POSTULATED | POSTULATED (unsolved) |
| **Total** | **~5** | **4** |

---

*AT-155: Quantum Correspondence. August 2026.*
