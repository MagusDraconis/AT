# AT Mathematical Foundation

## Complete Formula Collection

---

## 1. Topological Charge Q

**Definition**: Q is a quantized, conserved, indivisible topological charge.

**Conservation**: Q_total = Σ_i Q_i = constant

**Interaction**: J_ij = exp(-|x_i - x_j| / r_c) where r_c = coupling range.

---

## 2. Graph Laplacian L_Q

For a graph with adjacency matrix A and degree matrix D:

**(L_Q)_ii = Σ_j A_ij** (degree)

**(L_Q)_ij = -A_ij** (i≠j)

**Properties**: Real symmetric, positive semi-definite, eigenvalues λ_k ≥ 0.

---

## 3. Theta Operator L

**L = -(1/Δx²) · L_Q - γ · I**

where Δx = lattice spacing, γ = damping coefficient.

---

## 4. Eigenvalues — 1D Chain (Q nodes)

**λ_k = -(1/Δx²) · [2 - 2·cos(πk/(Q+1))] - γ**

For large Q: λ_k ≈ -(1/Δx²) · (π²k²/Q²) - γ

**Eigenvectors**: v_k[n] = sin(πk(n+1)/(Q+1))

---

## 5. Eigenvalues — 1D Ring (Q nodes)

**λ_k = -(1/Δx²) · [2 - 2·cos(2πk/Q)] - γ**

Degeneracy: 2 for k ≠ 0, Q/2.

---

## 6. Eigenvalues — 2D Square Lattice (N_x × N_y)

**λ_{kx,ky} = -(1/Δx²) · [4 - 2cos(πkx/(N_x+1)) - 2cos(πky/(N_y+1))] - γ**

---

## 7. Physical Observables from Q (1D Chain)

| Observable | Formula | Scaling (large Q) |
|-----------|---------|-------------------|
| Spectral gap Δ | λ_2 - λ_1 | 3π²/(Δx²·Q²) |
| Effective mass m_eff | 1/λ_1 (inverse gap) | Q²·Δx²/π² |
| Total energy E | trace(L) = Σ deg(i) | 2(Q-1)/Δx² |
| Correlation length ξ | 1/√(λ_1) | Q·Δx/π |
| Transport coefficient D | λ_1 | π²/(Δx²·Q²) |
| Information capacity C | log₂(Q) | log₂(Q) |
| Mode density ρ | N_modes / Q | 1 |

---

## 8. Schrödinger Correspondence

**ψ = u + iv** (complex wavefunction from two real fields)

**Coupled real system**:
∂u/∂t = L_Q v
∂v/∂t = -L_Q u

**Complex form**: i∂ψ/∂t = L_Q ψ

**Stationary states**: ψ_k(t) = exp(-iλ_k t) · v_k

---

## 9. Complex Structure

**J = [[0, 1], [-1, 0]]** — antisymmetric coupling matrix

**Property**: J² = -I (J acts as the imaginary unit)

**Norm conservation**: d/dt(u²+v²) = 0 ⇒ evolution generator M is antisymmetric: M^T = -M.

**Full evolution**: d/dt [u;v] = (J ⊗ L_Q) · [u;v]

---

## 10. Information Species and Evolution

**Fitness law**: w = r / c

where r = reproduction rate, c = resource consumption.

**Selection coefficient**: s_i = (dN_i/dt) / (r_i · N_i)

**Replicator dynamics**: dx_i/dt = x_i · (f_i - ⟨f⟩)

**Inheritance coefficient**: H(parent, child) = pattern_similarity(parent, child)

**Mutation rate**: μ = 1 - ⟨H⟩

---

## 11. Born Rule Uniqueness

For ψ = α|0⟩ + β|1⟩:

|α|² + |β|² = 1 (only exponent 2)

|α|^p + |β|^p = 1 ⇒ p = 2

**Gleason's theorem (1957)**: Any probability measure on Hilbert space (dim ≥ 3) additive for orthogonal projectors must be P = Tr(ρ·E). For pure states: P = |⟨φ|ψ⟩|².

---

## 12. Decoherence

**System-environment Hamiltonian**: H = L_S ⊗ I_E + I_S ⊗ L_E + g·V_int

**Reduced density matrix**: ρ_S(t) → diagonal in pointer basis as t → ∞

**Purity decay**: Tr(ρ²) ≈ exp(-γt) with γ ∝ g² · N_env

---

## 13. Key Identities

| AT | Standard Physics | Relationship |
|-----|-----------------|-------------|
| L_Q (1D) | Tight-binding H | IDENTITY |
| L_Q eigenmodes | Phonon modes | IDENTITY |
| λ_1 ∝ 1/Q² | Particle-in-a-box E₁ | IDENTITY |
| E = trace(L) | Extensive energy | IDENTITY |
| C = log₂(Q) | Boltzmann entropy S = k_B·ln W | IDENTITY |
| ρ = 1 | Weyl's law (1D) | IDENTITY |

---

## 14. Scaling Laws

| Quantity | 1D Scaling | 2D Scaling | Universal? |
|----------|-----------|-----------|------------|
| m_eff | ∝ Q² | ∝ Q | No (dimension-dependent) |
| E | ∝ Q | ∝ Q | Yes (extensive) |
| Δ | ∝ 1/Q² | ∝ 1/Q | No |
| ξ | ∝ Q | ∝ √Q | No |
| D | ∝ 1/Q² | ∝ 1/Q | No |
| C | ∝ log(Q) | ∝ log(Q) | Yes |

---

*AT-155: Mathematical Foundation. All formulas with derivation sources.*
