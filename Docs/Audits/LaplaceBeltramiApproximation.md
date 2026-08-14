# Laplace-Beltrami Approximation — Report

**Test file:** `TQM.Tests/ResearchXC/LaplaceBeltramiTests.cs`
**Result:** **PASSED (3/3).**

---

## Results

| # | Test | Output | Verdict |
|---|---|---|---|
| 1 | `WeightedLaplacian_PreservesFlatLimit()` | low-mode error $0.640\to0.040$ | **$O(1/N^2)$** |
| 2 | `WeightedLaplacian_VariableWeightsChangeSpectrum()` | $\max\|\lambda_{\rm unif}-\lambda_{\rm var}\|=3.9988$ | **metric-dependent** |
| 3 | `WeightedLaplacian_MatchesKnownManifoldExample()` | S¹ error $0.257\to0.016$ | **$O(1/N^2)$** |

---

## What each establishes

1. **Flat limit preserved** — a uniform path graph gives the flat Laplacian eigenvalues
   $(N^2)[2-2\cos(\pi k/N)]\to(\pi k)^2$, so $L_W$ reduces correctly in the flat case.

2. **Metric-dependence** — alternating edge weights ($1,3,1,3,\dots$) shift the spectrum by up
   to $\approx4$, confirming $L_W$ genuinely encodes the metric (non-uniform weights $\neq$
   uniform weights).

3. **Known manifold — the circle $S^1$** — a uniform cycle graph, scaled by $(N/2\pi)^2$,
   converges to the Laplace–Beltrami spectrum $\{k^2\}$ of the unit circle at rate $O(1/N^2)$.
   *(The cycle's nonzero modes are 2-fold degenerate — $e^{\pm ik\theta}$ — and the test reads
   the first occurrence of each mode; see audit note below.)*

---

## Comparison with standard graph-based Laplace–Beltrami constructions

$L_W = D_K - K$ is the **unnormalized weighted graph Laplacian**, the canonical graph-based
approximation to the Laplace–Beltrami operator (Belkin–Niyogi / Coifman–Lafon). For uniform
sampling density it converges to $\Delta_g$; for non-uniform density the *normalized* variants
($I - D^{-1}K$ or $I - D^{-1/2}KD^{-1/2}$) are required. TQM's $L_W$ is the correct *first*
(constant-density) term of this hierarchy — sufficient to couple a (locally flat) metric, not
yet a general curved one.

---

## Audit note (from results)

The first run of Test 3 reported a constant S¹ error $\approx5.0$ — a **false pass** of the
"error decreases with N" assertion. Auditing the numbers revealed the cycle graph's 2-fold
mode degeneracy was ignored (index $k$ instead of $2k-1$). After the fix the error is
$O(1/N^2)$, confirming the convergence. This is why the program is "audit from results", not
"test passes ⇒ done".

---

## Conclusion

The implemented $L_W = D_K - K$ **does** approximate a Laplace–Beltrami operator: it preserves
the flat limit, responds to the metric (weights), and reproduces a known manifold's spectrum
($S^1$, eigenvalues $k^2$) at the correct rate. It is the standard unnormalized graph Laplacian,
valid for uniform density — the first and necessary term of a full curved-space bridge. No new
primitives, no invented physics.
