# G4-RHO Phase 3 — Microscopic Origin of Entropy Maximization

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-RHO)
**Phase:** 3 — why does actualization evolve toward maximal entropy?
**Status:** COMPLETED — 3/3 xUnit tests pass (12/12 G4-RHO)
**Constraint:** no new primitives

---

## 1. Goal

G4-RHO2 showed α=0 emerges from entropy gradient flow. Here we ask *why* actualization maximizes entropy,
testing Q-event branching, abundance-law dynamics, counting-measure statistics, random actualization, and
maximum-likelihood evolution. Classify: DERIVED / PREFERRED / POSTULATED.

---

## 2. Results

### (a) Counting statistics: the uniform allocation has the most microstates (G4-RHO30)

Distributing N identical deficit quanta over K octaves, the number of microstates is the multinomial
W = N!/(∏n_k!), and by Stirling ln W = N·H(α):

| α | ln W = N·H(α) |
|---|---|
| 0.0 | 2079.4 (max) |
| 0.5 | 1978.3 |
| 1.0 | 1738.0 |

ln(W(0)/W(1)) = N·(H(0)−H(1)) = 341 (i.e. W(0)/W(1) ≈ 10¹⁴⁸). The uniform α=0 allocation is
**astronomically** more likely.

### (b) The diffusion is the maximum-likelihood evolution (G4-RHO31)

Along the scale-space diffusion (G4-RHO2), H increases monotonically from H(α=1)=1.738 to H(final)=2.079 =
ln 8 (the maximum). Each diffusion step increases the number of accessible microstates — it *is* the
maximum-likelihood (entropy-increasing) evolution.

### (c) Exact counting + classification (G4-RHO32)

Exact small-N counting: N=12 quanta, K=4 octaves. Uniform [3,3,3,3] (α=0) has W = 369,600 microstates;
biased [4,3,3,2] (α>0) has W = 277,200. The uniform allocation is the maximum-likelihood state.

---

## 3. Classification: DERIVED (entropy maximization = maximum likelihood), with one postulate

- The uniform allocation having the **most microstates** is a pure **combinatorial fact** (counting) — derived.
- The system being **most likely** in the maximum-microstate configuration is the standard statistical-mechanics
  bridge (maximum likelihood / ergodic principle) — the bridge from counting to probability.
- The **one postulate** is **indifference**: actualization is unbiased across scales (no preferred scale, all
  microstates equiprobable). This is AT's scale-freeness, already established as native.

Entropy maximization is therefore **DERIVED** from counting + indifference; only the indifference principle
itself is postulated, and it is the same scale-freeness that AT already assumes.

---

## 4. Conclusion

This completes the full ρ-dynamics arc at the microscopic level:

- **G4-RHO0** — α=0 PREFERRED (unique scale-invariant).
- **G4-RHO1** — α=0 DERIVED (unique maximum-entropy allocation).
- **G4-RHO2** — α=0 DERIVED (stable attractor of the evolution equation ∂_t A_k = D·Δ_k A_k).
- **G4-RHO3** — entropy maximization DERIVED (the maximum-likelihood state of unbiased actualization).

The chain is closed: actualization, unbiased across scales (indifference), most likely occupies the uniform
α=0 allocation (maximum microstates = maximum entropy), to which the native scale-space diffusion (entropy
gradient flow) relaxes — producing the log-deficit ρ ∝ ln(Rmax/r) and hence the flat rotation curve. The only
remaining postulate is indifference (no preferred scale), which is AT's scale-freeness itself.

---

## Test program

| Test | Verdict |
|---|---|
| G4-RHO30 `G4_RHO30_CountingStatisticsMaximumLikelihood` | PASS (uniform = maximum microstates) |
| G4-RHO31 `G4_RHO31_MaximumLikelihoodEvolution` | PASS (diffusion is entropy-increasing) |
| G4-RHO32 `G4_RHO32_ExactCountingClassification` | PASS (DERIVED, indifference postulate) |

Code: `AT.Core/ResearchXH/RhoDynamics.cs` (added `LogMicrostates`, `EntropyOf`);
tests `AT.Tests/ResearchXH/G4RHO_Phase3_EntropyOriginTests.cs`.
