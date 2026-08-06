# 10. Mathematical Foundation

## Complete Formula Collection

---

## Q Interaction Network

J_ij = exp(-|x_i - x_j| / r_c) — pairwise coupling

A_ij = 1 if J_ij > threshold, else 0 — adjacency

D_ii = Σ_j A_ij — degree matrix

L_Q = D - A — graph Laplacian

---

## Theta Operator

L = -(1/Δx²) · L_Q - γ · I

---

## 1D Chain Eigenvalues (N nodes)

λ_k = -(1/Δx²) · [2 - 2·cos(πk/(N+1))] - γ, k = 1..N

Large N: λ_k ≈ -(1/Δx²) · [π²k²/N²] - γ

Eigenvectors: v_k[n] = √(2/(N+1)) · sin(πk(n+1)/(N+1))

---

## 1D Ring (N nodes)

λ_k = -(1/Δx²) · [2 - 2·cos(2πk/N)] - γ, k = 0..N-1

Degeneracy: 2 (except k=0, N/2)

---

## 2D Square Lattice (N_x × N_y)

λ_{kx,ky} = -(1/Δx²) · [4 - 2cos(πkx/(N_x+1)) - 2cos(πky/(N_y+1))] - γ

---

## Physical Observables (1D Chain)

| Observable | Formula | Large-N Scaling |
|-----------|---------|-----------------|
| m_eff | 1/λ_1 | N²·Δx²/π² |
| E | trace(L) = 2(N-1)/Δx² | ∝ N |
| Δ | λ_2 - λ_1 | 3π²/(Δx²·N²) |
| ξ | 1/√(λ_1) | N·Δx/π |
| D | λ_1 | π²/(Δx²·N²) |
| C | log₂(N) | log₂(N) |
| ρ | N_modes/N = 1 | 1 |

---

## Schrödinger Correspondence

ψ = u + iv (complex wavefunction)

∂u/∂t = L_Q v, ∂v/∂t = -L_Q u ⇔ i∂ψ/∂t = L_Q ψ

J = [[0,1],[-1,0]], J² = -I

d/dt ||ψ||² = 0 ⇒ M^T = -M

ψ_k(t) = exp(-iλ_k t) · v_k

---

## Information Evolution

Fitness: w = r / c

Selection coefficient: s_i = (dN_i/dt) / (r_i · N_i)

Inheritance: H(parent, child) = pattern_similarity(parent, child)

Mutation rate: μ = 1 - ⟨H⟩

---

## Born Rule Uniqueness

For ψ = α|0⟩ + β|1⟩: |α|² + |β|² = 1 (only p=2 works)

Gleason (1957): Any additive probability measure on Hilbert space (dim≥3) must be P = Tr(ρ·E).

---

## Physical Identities

L_Q (1D) ≡ Tight-binding H (up to scaling)

λ_1 ∝ 1/N² ≡ Particle-in-a-box E₁

C = log₂(N) ≡ Boltzmann entropy

ρ = 1 ≡ Weyl's law (1D)

---

*TQM-155: Mathematical Foundation. All governing equations.*
