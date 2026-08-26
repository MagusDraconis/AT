# AT Physics Correspondence

## What AT Matches and Where It Fails

---

## Exact Correspondences (Mathematical Identities)

| AT Structure | Physical System | Type | Source |
|--------------|----------------|------|--------|
| L_Q (1D chain) | Tight-binding Hamiltonian H = -t Σ(c†_i c_j + h.c.) | IDENTITY | AT-142, 144 |
| L_Q eigenmodes | Phonon normal modes ω_k = 2√(K/m)·sin(πk/(2N)) | IDENTITY | AT-144 |
| L_Q eigenvalues | Electronic band structure E(k) | IDENTITY | AT-144 |
| λ_1 ∝ 1/Q² | Particle-in-a-box E₁ = π²ℏ²/(2mL²) | EXACT | AT-146 |
| E = trace(L) ∝ Q | Extensive energy (thermodynamics) | EXACT | AT-145 |
| C = log₂(Q) | Boltzmann entropy S = k_B·ln(W) | EXACT | AT-145 |
| ρ = 1 | Weyl's law N(λ) ∝ λ^(d/2) in 1D | EXACT | AT-145 |
| ∂u/∂t = L_Q v, ∂v/∂t = -L_Q u | Schrödinger i∂ψ/∂t = L_Q ψ | EXACT | AT-150 |
| J = [[0,1],[-1,0]] | Complex unit i | EXACT | AT-150 |

---

## Strong Correspondences

| AT Structure | Physical System | Type | Source |
|--------------|----------------|------|--------|
| L_Q (2D square) | 2D Tight-binding | Strong | AT-144 |
| L_Q (hexagonal) | Graphene Dirac cones | Strong | AT-144 |
| L_Q (3D cubic) | 3D Tight-binding | Strong | AT-144 |
| Spin-wave spectrum | 1D Heisenberg ferromagnet | Strong | AT-144 |
| Fitness law w=r/c | Ecological fitness | Strong | AT-136 |
| Darwinian selection | Resource-limited competition | Strong | AT-135 |

---

## Where AT Fails

| System | AT Predicts | Actual Physics | Reason |
|--------|-------------|---------------|--------|
| **1D Ising Chain** | Δ ∝ 1/Q² | Δ ∝ 1/Q (domain wall energy = 2J) | Ising has discrete domain wall excitations, not continuous eigenmodes |
| **1D Heisenberg AFM** | Δ ∝ 1/Q² | Δ ∝ 1/Q (spinon gap from Bethe ansatz) | Heisenberg spinons are fractional excitations, not Laplacian eigenmodes |
| **2D Percolation** | No prediction | Conductivity exponent t ≈ 1.3 | Percolation is a geometric phase transition, not a spectral problem |
| **Random Resistor Net** | R ∝ L (uniform) | R ∝ L^ζ (ζ ≠ 1) | Disorder breaks the uniform Laplacian structure |

---

## Why AT Fails Where It Does

AT's domain of applicability = **systems governed by graph Laplacians**.

This includes:
- Harmonic oscillators (coupled masses + springs)
- Tight-binding electrons (nearest-neighbor hopping)
- Diffusion (random walks on graphs)
- Spin waves (linearized magnon theory)
- Phonons (harmonic lattice vibrations)

This EXCLUDES:
- Systems with non-harmonic interactions (Ising, Heisenberg)
- Systems with topological/geometric phase transitions (percolation)
- Systems with strong disorder (random resistor networks)
- Systems with long-range or many-body interactions

**This is a properly delimited scientific theory.** AT makes no claims outside its domain.

---

## The Domain Boundary

The boundary between AT's domain and external physics is:

**Graph Laplacian ⇔ Non-Laplacian dynamics**

Mathematically: if the system's Hamiltonian/evolution operator can be expressed as a graph Laplacian (or a direct sum/product of graph Laplacians), AT applies. Otherwise, it does not.

---

## Novel Physical Predictions?

**Honest assessment**: AT does NOT make novel physical predictions beyond standard graph theory. All exact correspondences are mathematical identities known from spectral graph theory and condensed matter physics.

AT's contribution is **conceptual**: providing an evolutionary interpretation of graph spectral theory, and deriving Hilbert space + Schrödinger dynamics from topological charge + reversibility.

---

*AT-155: Physics Correspondence. Domain clearly delimited.*
