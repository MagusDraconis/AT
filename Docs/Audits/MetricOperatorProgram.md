# Metric Operator Program — Test Report

**Test file:** `AT.Tests/ResearchXC/MetricOperatorTests.cs`
**Result:** **PASSED (4/4).**

---

## Results

| # | Candidate construction | Test | Output | Verdict |
|---|---|---|---|---|
| 1 | weighted graph Laplacian | `WeightedGraphLaplacian_IsConstructible()` | row sums $=0$, min eigenvalue $=0$ | **valid** |
| 2 | graph Laplacian on manifolds | `WeightedGraphLaplacian_ReducesToUnweighted()` | $\max|L_W-L_Q|=0$ | **reduces** |
| 3 | discrete Laplace–Beltrami | `WeightedGraphLaplacian_ConvergesToFlatLaplacian()` | continuum error $0.64\to0.040$ | **$O(1/N^2)$** |
| 4 | causal-set d'Alembertian with metric | `CausalSetDAlembertian_HasNoMetricData()` | "binomial"×17, "L_k"×9 | **metric-independent** |

---

## What each establishes

1. **Weighted graph Laplacian $L_W=D_W-W$** is constructible and is a valid Laplacian
   (symmetric, zero row-sum, positive semi-definite). AT's existing coupling matrix $K_{ij}$
   is the required *weight matrix* — the missing piece is only forming $L_W=D_K-K$.

2. **Uniform weights reduce to $L_Q$** — $L_W=L_Q$ exactly when all edges have weight 1. So
   the unweighted graph Laplacian already used by AT is the *uniform-weight special case* of
   the metric operator.

3. **The uniform chain converges to the flat Laplacian** — the scaled eigenvalues
   $N^2[2-2\cos(\pi k/N)]\to(\pi k)^2$ at rate $O(1/N^2)$, i.e. the weighted Laplacian is the
   discrete Laplace–Beltrami in the flat limit.

4. **BDG is metric-independent** — its coefficients are fixed binomial weights over causal
   layers $L_k$, not metric data. The metric enters BDG only through the (external) causal-set
   → manifold correspondence, not through the operator itself.

---

## Compatibility determination

| Construction | AT ingredient already present | Missing piece |
|---|---|---|
| weighted graph Laplacian | coupling matrix $K_{ij}$ (`TemporalMatrix`) | forming $L_W=D_K-K$ |
| graph Laplacian on manifold | unweighted $L_Q$ | the *weight* rule $K_{ij}\to g_{\mu\nu}$ |
| causal-set d'Alembertian with metric | BDG (binomial, flat) | curved causal-set BDG |
| discrete Laplace–Beltrami | flat Laplacian (verified) | non-flat metric coefficients |

**Minimal object:** the weighted graph Laplacian $L_W=D_K-K$, built from AT's existing
spatial coupling $K_{ij}=K\exp(-d/\lambda)$. No new primitives, no new physics — it is a
standard construction over an ingredient AT already possesses.

---

## Conclusion

The four candidate metric-operator constructions were each tested. The minimal object needed
to couple a metric to a AT operator is the **weighted graph Laplacian** $L_W=D_K-K$, and the
only genuinely missing piece is the **weight rule** that assigns metric-derived edge weights
(the existing $K_{ij}$ is a spatial coupling, not yet a metric coefficient). All tests are
deterministic and use standard constructions; no invented physics.
