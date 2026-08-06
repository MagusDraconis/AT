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

| Quantum Structure | TQM Derivation | Source |
|------------------|---------------|--------|
| Hilbert space | L_Q eigenvectors form orthonormal basis | TQM-149 |
| Complex structure | J from antisymmetric coupling | TQM-150 |
| Imaginary unit i | J² = -I → J ≡ i | TQM-150 |
| Schrödinger equation | M = J⊗L_Q → i∂ψ/∂t = L_Q ψ | TQM-149 |
| Unitary evolution | ψ(t) = exp(-iL_Q t) ψ(0) | TQM-149 |
| Stationary states | ψ_k = exp(-iλ_k t) v_k | TQM-149 |
| Norm conservation | d/dt ||ψ||² = 0 from antisymmetry | TQM-151 |
| Born rule uniqueness | Gleason's theorem (|ψ|² only additive measure) | TQM-153 |

---

## What Is NOT Derived (Postulated)

| Quantum Structure | Status | Source |
|------------------|--------|--------|
| Q exists | Postulate 1 | TQM-117-122 |
| Reversible dynamics | Postulate 2 | TQM-152 |
| Born rule (additivity) | Postulate 3 | TQM-153 |
| Measurement (collapse) | Postulate 4 | TQM-154 |

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

## TQM vs Standard QM — Postulate Count

| Postulate | Standard QM | TQM |
|----------|------------|-----|
| Hilbert space | POSTULATED | DERIVED (from L_Q) |
| Observables = Hermitian operators | POSTULATED | DERIVED (L_Q is Hermitian) |
| Schrödinger equation | POSTULATED | DERIVED (from reversibility) |
| Born rule | POSTULATED | POSTULATED (Gleason provides uniqueness) |
| Measurement | POSTULATED | POSTULATED (unsolved) |
| **Total** | **~5** | **4** |

---

*TQM-155: Quantum Correspondence. August 2026.*
