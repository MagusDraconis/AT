# 8. Physical Correspondence

## What TQM Matches and Where It Fails

---

## Exact Mathematical Identities

| TQM Structure | Physical System | Relationship | Source |
|--------------|----------------|-------------|--------|
| L_Q (1D chain) | Tight-binding Hamiltonian | IDENTITY (up to scaling) | TQM-142, 144 |
| L_Q eigenvalues | Electronic band structure E(k) | IDENTITY | TQM-144 |
| L_Q eigenmodes | Phonon normal modes | IDENTITY | TQM-144 |
| L_Q eigenmodes | Coupled oscillator modes | IDENTITY | TQM-144 |
| L_Q eigenmodes | Spin-wave magnon modes | Strong | TQM-144 |
| λ_1 ∝ 1/Q² | Particle-in-a-box E₁ ∝ 1/L² | EXACT | TQM-146 |
| E = trace(L) ∝ Q | Extensive energy | EXACT | TQM-145 |
| C = log₂(Q) | Boltzmann entropy S = k_B·ln W | EXACT | TQM-145 |
| ρ = 1 | Weyl's law in 1D | EXACT | TQM-145 |

---

## Where TQM Fails

| System | TQM Predicts | Actual Physics | Reason |
|--------|-------------|---------------|--------|
| 1D Ising Chain | Δ ∝ 1/Q² | Δ ∝ 1/Q | Domain wall physics ≠ Laplacian eigenmodes |
| 1D Heisenberg AFM | Δ ∝ 1/Q² | Δ ∝ 1/Q (Bethe ansatz) | Spinons ≠ Laplacian eigenmodes |
| 2D Percolation | No prediction | t ≈ 1.3 | Geometric phase transition |
| Random Resistor Net | R ∝ L (uniform) | R ∝ L^ζ | Disorder breaks uniformity |

---

## Domain of Applicability

**TQM applies to**: Graph-Laplacian-governed systems:
- Tight-binding electrons
- Harmonic oscillators
- Diffusion on networks
- Spin waves (linearized)
- Phonons (harmonic)

**TQM does NOT apply to**:
- Systems with non-harmonic interactions
- Systems with topological phase transitions
- Systems with strong disorder
- Systems with many-body interactions

---

## Scaling Laws (1D Chain)

| Observable | Scaling | Physical Match |
|-----------|---------|---------------|
| m_eff | ∝ Q² | Particle-in-a-box |
| E | ∝ Q | Extensive energy |
| Δ | ∝ 1/Q² | Finite-size gap |
| ξ | ∝ Q | System size |
| D | ∝ 1/Q² | Diffusion constant |
| C | ∝ log(Q) | Boltzmann entropy |

---

*TQM-155: Physical Correspondence. August 2026.*
