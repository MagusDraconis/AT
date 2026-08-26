# 4. Q Networks and Graph Laplacian

## From Q Interactions to the Theta Operator

---

## The Graph Laplacian L_Q

For a graph with N nodes, adjacency matrix A, and degree matrix D:

**(L_Q)_ii = Σ_j A_ij** (diagonal: degree of node i)

**(L_Q)_ij = -A_ij** for i ≠ j (off-diagonal: -1 if edge exists)

**Properties**:
- Real symmetric: L_Q^T = L_Q
- Positive semi-definite: λ_k ≥ 0 for all k
- Row sums = 0: Σ_j (L_Q)_ij = 0
- Number of zero eigenvalues = number of connected components

---

## The Theta Operator L

**L = -(1/Δx²) · L_Q - γ · I**

where:
- Δx = 1/(N+1): lattice spacing (for 1D chain of N nodes on unit interval)
- γ: damping coefficient (phenomenological, not derived from Q)
- The minus sign makes eigenvalues negative (decay/damping).

**Continuum limit** (N → ∞, Δx → 0):
L_Q → -d²/dx² (continuum Laplacian)
L → (1/Δx²)·d²/dx² - γ = c²·d²/dx² - γ

---

## Eigenvalue Spectrum (1D Chain, N nodes)

**Exact eigenvalues**:
λ_k = -(1/Δx²) · [2 - 2·cos(πk/(N+1))] - γ,  k = 1, 2, ..., N

**Large-N approximation**:
λ_k ≈ -(1/Δx²) · [π²k²/N²] - γ

**Eigenvectors** (normalized):
v_k[n] = √(2/(N+1)) · sin(πk(n+1)/(N+1))

---

## Eigenvalue Spectrum (2D Square Lattice)

For N_x × N_y lattice:

λ_{kx,ky} = -(1/Δx²) · [4 - 2cos(πkx/(N_x+1)) - 2cos(πky/(N_y+1))] - γ

kx = 1..N_x, ky = 1..N_y. Total modes: N_x · N_y.

---

## Spectral Properties

| Property | 1D Chain | 2D Square | 3D Cubic |
|----------|---------|-----------|----------|
| λ_min | π²/(Δx²·N²) | 2π²/(Δx²·N) | 3π²/(Δx²·N^(2/3)) |
| λ_max | 4/Δx² | 8/Δx² | 12/Δx² |
| Spectral gap Δ | 3π²/(Δx²·N²) | ∝ 1/N | ∝ 1/N^(2/3) |
| Mode density ρ | 1 | 1 | 1 |

---

## Physical Observables from L_Q

| Observable | Formula | Physical Meaning |
|-----------|---------|-----------------|
| Effective mass m_eff | 1/λ_1 | Inertia of slowest collective mode |
| Total energy E | trace(L) = Σ deg(i) | Total coupling energy |
| Spectral gap Δ | λ_2 - λ_1 | Energy to first excitation |
| Correlation length ξ | 1/√(λ_1) | Spatial extent of ground state |
| Transport coefficient D | λ_1 | Diffusion constant |
| Information capacity C | log₂(N) | Bits encodable in mode space |
| Mode density ρ | N_modes / N | Modes per degree of freedom = 1 |

All derivable analytically from λ_k. Source: AT-145.

---

## Damping Term γ

The damping coefficient γ appears in L = -(1/Δx²)L_Q - γI.

**Status**: PHENOMENOLOGICAL — not derived from Q.

**Physical origin**: Energy dissipation into unmodeled degrees of freedom. In open quantum systems, γ represents coupling to a thermal bath.

**Effect**: γ shifts all eigenvalues by -γ. Stable modes require |λ_k| > γ. For large γ, high-k modes become unstable → fewer species.

**Open question**: Can γ be derived from Q-environment coupling?

---

## The Graph Laplacian Identity

The central result of AT-142: **L_Q IS the tight-binding Hamiltonian.**

1D chain: (L_Q)_ij = 2δ_ij - δ_i,j+1 - δ_i,j-1
Tight-binding: H_ij = ε·δ_ij - t·(δ_i,j+1 + δ_i,j-1)

With ε = 2t: **H = t·L_Q** (mathematical identity).

This is not an analogy or approximation — it is an identity. The graph Laplacian and the tight-binding Hamiltonian are the same matrix.

---

*AT-155: Q Networks and Graph Laplacian. August 2026.*
