# TQM Physics Correspondence

## What TQM Matches and Where It Fails

---

## Exact Correspondences (Mathematical Identities)

| TQM Structure | Physical System | Type | Source |
|--------------|----------------|------|--------|
| L_Q (1D chain) | Tight-binding Hamiltonian H = -t Σ(c†_i c_j + h.c.) | IDENTITY | TQM-142, 144 |
| L_Q eigenmodes | Phonon normal modes ω_k = 2√(K/m)·sin(πk/(2N)) | IDENTITY | TQM-144 |
| L_Q eigenvalues | Electronic band structure E(k) | IDENTITY | TQM-144 |
| λ_1 ∝ 1/Q² | Particle-in-a-box E₁ = π²ℏ²/(2mL²) | EXACT | TQM-146 |
| E = trace(L) ∝ Q | Extensive energy (thermodynamics) | EXACT | TQM-145 |
| C = log₂(Q) | Boltzmann entropy S = k_B·ln(W) | EXACT | TQM-145 |
| ρ = 1 | Weyl's law N(λ) ∝ λ^(d/2) in 1D | EXACT | TQM-145 |
| ∂u/∂t = L_Q v, ∂v/∂t = -L_Q u | Schrödinger i∂ψ/∂t = L_Q ψ | EXACT | TQM-150 |
| J = [[0,1],[-1,0]] | Complex unit i | EXACT | TQM-150 |

---

## Strong Correspondences

| TQM Structure | Physical System | Type | Source |
|--------------|----------------|------|--------|
| L_Q (2D square) | 2D Tight-binding | Strong | TQM-144 |
| L_Q (hexagonal) | Graphene Dirac cones | Strong | TQM-144 |
| L_Q (3D cubic) | 3D Tight-binding | Strong | TQM-144 |
| Spin-wave spectrum | 1D Heisenberg ferromagnet | Strong | TQM-144 |
| Fitness law w=r/c | Ecological fitness | Strong | TQM-136 |
| Darwinian selection | Resource-limited competition | Strong | TQM-135 |

---

## Where TQM Fails

| System | TQM Predicts | Actual Physics | Reason |
|--------|-------------|---------------|--------|
| **1D Ising Chain** | Δ ∝ 1/Q² | Δ ∝ 1/Q (domain wall energy = 2J) | Ising has discrete domain wall excitations, not continuous eigenmodes |
| **1D Heisenberg AFM** | Δ ∝ 1/Q² | Δ ∝ 1/Q (spinon gap from Bethe ansatz) | Heisenberg spinons are fractional excitations, not Laplacian eigenmodes |
| **2D Percolation** | No prediction | Conductivity exponent t ≈ 1.3 | Percolation is a geometric phase transition, not a spectral problem |
| **Random Resistor Net** | R ∝ L (uniform) | R ∝ L^ζ (ζ ≠ 1) | Disorder breaks the uniform Laplacian structure |

---

## Why TQM Fails Where It Does

TQM's domain of applicability = **systems governed by graph Laplacians**.

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

**This is a properly delimited scientific theory.** TQM makes no claims outside its domain.

---

## The Domain Boundary

The boundary between TQM's domain and external physics is:

**Graph Laplacian ⇔ Non-Laplacian dynamics**

Mathematically: if the system's Hamiltonian/evolution operator can be expressed as a graph Laplacian (or a direct sum/product of graph Laplacians), TQM applies. Otherwise, it does not.

---

## Novel Physical Predictions?

**Honest assessment**: TQM does NOT make novel physical predictions beyond standard graph theory. All exact correspondences are mathematical identities known from spectral graph theory and condensed matter physics.

TQM's contribution is **conceptual**: providing an evolutionary interpretation of graph spectral theory, and deriving Hilbert space + Schrödinger dynamics from topological charge + reversibility.

---

*TQM-155: Physics Correspondence. Domain clearly delimited.*
