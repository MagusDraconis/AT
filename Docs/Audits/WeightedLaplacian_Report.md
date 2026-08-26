# Weighted Laplacian — Report

**Implementation:** `TemporalMatrix.BuildWeightedLaplacian()` (AT.Core)
**Test file:** `AT.Tests/ResearchXC/WeightedLaplacianTests.cs`
**Result:** **PASSED (4/4).**

---

## Implementation

Added a weighted graph Laplacian builder to the existing coupling matrix:

$$L_W = D_K - K,\qquad (D_K)_{ii}=\sum_j K_{ij}$$

where $K_{ij}$ is the existing spatial coupling (`TemporalMatrix`, via `FillSpatialCoupling`
or `SetCoupling`). The builder uses **only the existing coupling matrix** — no new weights, no
new parameters.

---

## Results

| # | Test | Output | Verdict |
|---|---|---|---|
| 1 | `WeightedLaplacian_IsSymmetric()` | max asymmetry $=0$ | **PASS** |
| 2 | `WeightedLaplacian_HasZeroRowSum()` | max $|\sum_j L_{W,ij}|=1.9\times10^{-16}$ | **PASS** |
| 3 | `WeightedLaplacian_IsPositiveSemidefinite()` | min eigenvalue $=0$ | **PASS** |
| 4 | `WeightedLaplacian_ReducesToUnweighted()` | $\max|L_W-L_Q|=0$ | **PASS** |

---

## What each establishes

1. **Symmetry** — $L_W=D_K-K$ is symmetric because $K$ is symmetric (`FillSpatialCoupling`
   sets $K_{ij}=K_{ji}$).
2. **Zero row-sum** — the constant vector is in the kernel, as required for a Laplacian.
3. **Positive semi-definiteness** — all eigenvalues $\ge 0$ (min $=0$, the constant mode).
4. **Reduction** — when the coupling is binary (unit weights on edges), $L_W$ equals the
   unweighted graph Laplacian $L_Q=D-A$ exactly.

---

## Conclusion

The weighted graph Laplacian $L_W=D_K-K$ is now implemented over AT's existing coupling
matrix $K_{ij}$ and verified to be a valid Laplacian (symmetric, zero row-sum, positive
semi-definite) that reduces to the unweighted $L_Q$ in the binary limit. This supplies the
missing **weight rule** identified in `MetricOperatorProgram.md` — the discrete
Laplace–Beltrami operator for the weighted graph — using only existing ingredients. No new
physics, no new parameters.
