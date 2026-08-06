# 3. Q Theory

## The Fundamental Topological Charge

---

## Definition

Q is a quantized, conserved, indivisible topological charge with:
- Spatial position: x_i ∈ [0, L]
- Phase: θ_i ∈ [0, 2π)
- Charge value: Q_i ∈ ℤ (integer)

**Status**: ASSUMED (Postulate 1). Q is the irreducible microscopic degree of freedom.

**Source**: TQM-117 through TQM-122.

---

## Properties

### Quantization
Q takes only discrete integer values. Q_i ∈ ℤ.

### Conservation
Q_total = Σ_i Q_i = constant. Topological charge is neither created nor destroyed (in the absence of topological transitions).

### Indivisibility
Q is the minimal unit of topological charge. There are no fractional charges.

### Topological Nature
Q is defined by a winding number or topological invariant, not by local field values. Q is robust against continuous deformations.

---

## Q Interactions

Q charges interact pairwise. The coupling strength depends on spatial separation:

**J_ij = exp(-|x_i - x_j| / r_c)**

where r_c is the coupling range.

**Local interaction regime**: r_c ≪ L → nearest-neighbor coupling only.
**Long-range regime**: r_c ∼ L → all-to-all coupling.

TQM-143 demonstrated that the full Theta hierarchy requires LOCAL interactions (graph locality). Random and scale-free graphs (long-range connections) break the discrete eigenmode structure.

---

## Q Ensemble

An ensemble of N_Q Q charges forms:

1. **Spatial configuration**: positions {x_i} determine the interaction topology
2. **Phase configuration**: phases {θ_i} determine the collective state
3. **Interaction graph**: edges where J_ij > threshold
4. **Graph Laplacian**: L_Q = D - A, where D_ii = Σ_j A_ij

**Density**: ρ_Q = N_Q / L (charges per unit length in 1D).

**Autonomy threshold** (TQM-128): Θ becomes autonomous at ρ_Q > 0.3.

---

## Q Conservation Laws

1. **Charge conservation**: Q_total = constant
2. **Topological protection**: Q cannot change by continuous deformation
3. **Graph structure**: For a fixed spatial configuration, the interaction graph and L_Q are invariant

**Note**: Q conservation (scalar count) does NOT imply norm conservation ||ψ||² = constant (vector norm). These are distinct conservation laws. Q conservation is topological; norm conservation is dynamical (Postulate 2).

---

## Q → L_Q Derivation

1. Place N_Q charges at positions x_i (e.g., uniformly on [0, L]).
2. Compute pairwise interactions: J_ij = exp(-|x_i - x_j|/r_c).
3. Build adjacency matrix: A_ij = 1 if J_ij > threshold, 0 otherwise.
4. Compute degree matrix: D_ii = Σ_j A_ij.
5. Graph Laplacian: L_Q = D - A.
6. Theta operator: L = -(1/Δx²) · L_Q - γ · I.

**Result**: L_Q is a real symmetric positive semi-definite matrix. Its eigenvectors form a complete orthonormal basis (Hilbert space). Its eigenvalues λ_k determine physical observables.

---

## Q Network Topologies

| Topology | Graph Class | Spectrum | Species? | Source |
|----------|------------|----------|----------|--------|
| 1D Chain | Regular | Discrete sinusoidal | YES | TQM-142 |
| 1D Ring | Regular | Discrete (periodic) | YES | TQM-143 |
| 2D Square | Regular | 2D sinusoidal | YES | TQM-143 |
| 2D Hexagonal | Regular | Dirac cones | YES | TQM-143 |
| 3D Cubic | Regular | 3D sinusoidal | YES | TQM-143 |
| Random (ER) | Random | Wigner semicircle | NO | TQM-143 |
| Small-World | Small-World | Spectral gap + band | YES | TQM-143 |
| Scale-Free | Scale-Free | Power-law | NO | TQM-143 |
| Fully Connected | Regular (degenerate) | 1 dominant + N-1 degenerate | Limited | TQM-143 |
| Community | Modular | Multiple clusters | YES | TQM-143 |

**Requirement for species**: Graph locality — edges only between nearby nodes. This ensures discrete eigenmodes. Random and scale-free graphs lack locality and have no discrete species.

---

*TQM-155: Q Theory. August 2026.*
