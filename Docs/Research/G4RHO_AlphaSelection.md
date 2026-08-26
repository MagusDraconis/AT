# G4-RHO Phase 1 — α-Selection

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-RHO)
**Phase:** 1 — why is α = 0 selected?
**Status:** COMPLETED — 3/3 xUnit tests pass (6/6 G4-RHO)
**Constraint:** no new primitives

---

## 1. Goal

Flat rotation curves emerge from the log-deficit hierarchy (α=0). G4-RHO0 showed α=0 is *preferred* (unique
scale-invariant) but not *derived*. Here we test whether a native principle — entropy maximization,
abundance-law stationarity, scale-free fixed points, flow equilibria, hierarchy growth — uniquely selects
α=0 among the self-similar family. Classify: DERIVED / PREFERRED / ACCIDENTAL.

---

## 2. Results

### (a) Entropy maximization uniquely selects α=0 (G4-RHO10)

Allocate the total deficit across K octaves with fractions p_k ∝ λ^(−αk). The Shannon entropy
H(α) = −Σ p_k ln p_k is maximized by the **uniform** allocation p_k = 1/K — which is exactly α=0:

| α | H(α) |
|---|---|
| −1.0 | 1.738 |
| −0.5 | 1.978 |
| **0.0** | **2.079 (= ln 8, max)** |
| +0.5 | 1.978 |
| +1.0 | 1.738 |

With only conservation of total deficit (Σp=1) and no preferred scale, the maximum-entropy (least-biased)
allocation is uniform → **α = 0 uniquely**.

### (b) Scale-invariance / RG is NOT selective (G4-RHO11)

Block-spin coarse-graining maps the α-hierarchy to itself (α invariant): CoarseGrainedAlpha(α) = α for
every α ∈ {−1, −0.5, 0, 0.5, 1}. Every α is a fixed point — scale-invariance/stationarity gives a
**continuum**, not a unique selection.

### (c) Two characterizations coincide at α=0 (G4-RHO12)

- **Uniformity:** α=0 gives p_k = 1/K (spread 0); α≠0 is biased (α=1 spread 0.319).
- **Scale-free field (flat rotation):** v² ∝ r^(−α), so v²(3)/v²(9) = 3^α — closest to 1 (flat) at α=0:
  α=0 → 1.18, α=0.5 → 1.90, α=1 → 3.15 (monotonic).

The maximum-entropy and scale-free-field characterizations **coincide at α=0**.

---

## 3. Classification: DERIVED (α=0)

α = 0 is the **unique** maximum-entropy (uniform) allocation of actualization deficit across scales, and
equivalently the unique member with the scale-free field a ∝ 1/r (flat rotation). Scale-invariance alone
(RG) is not selective — it leaves a continuum of fixed points — so the selection is carried by **entropy
maximization** (least bias / indifference): with no preferred scale and only total-deficit conservation, the
least-biased allocation is uniform, and uniform ⟺ α = 0.

This upgrades α=0 from PREFERRED (G4-RHO0) to **DERIVED**.

*Caveat:* maximum entropy is a statistical (least-bias) principle, not a dynamical equation; it uses only the
counting measure (no new primitives) but is a methodological principle rather than an actualization dynamics.

---

## 4. Conclusion

The selection of α = 0 is explained: it is the unique maximum-entropy allocation of actualization deficit
across logarithmic scales, under conservation of total deficit and the absence of any preferred scale. This
is a genuine uniqueness result — the flat rotation curve (scale-free field) and the uniform (maximum-entropy)
deficit distribution coincide exactly at α=0. The remaining gap is not "why α=0" but "why entropy is
maximized" — i.e., the dynamical mechanism that enforces least-bias actualization, which stays open.

---

## Test program

| Test | Verdict |
|---|---|
| G4-RHO10 `G4_RHO10_EntropyMaximization` | PASS (α=0 unique entropy max) |
| G4-RHO11 `G4_RHO11_RgNotSelective` | PASS (all α are RG fixed points) |
| G4-RHO12 `G4_RHO12_UniformityClassification` | PASS (DERIVED) |

Code: `AT.Core/ResearchXH/RhoDynamics.cs` (added `DeficitFractions`, `Entropy`, `Increments`,
`CoarseGrainedAlpha`); tests `AT.Tests/ResearchXH/G4RHO_Phase1_AlphaSelectionTests.cs`.
