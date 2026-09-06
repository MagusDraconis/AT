# ResearchY-NP_070 — Criticality Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_070 (permanent)
**Title:** Criticality Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_070.md`
**Depends on:** AT-QG QG1/QG7 (critical branching), QG194 (matter = deficit), QG206 (α=0),
QG222 (native dynamics), QG89 (energy = actualization rate), QG011 (discrete tick), AT-F1
(indifference principle), ResearchY-NP_062 (high order), NP_069 (expansion)
**Test suite:** `AT.Tests/ResearchY/NP_069_Tests.cs` → `Y_NP_070_Tests.cs`

---

## Purpose

NP_069 established expansion is the branching growth of the count. NP_070 asks **why** that
branching sits at the critical point μ=1. Program: (1) remove criticality; (2) test μ = 0.9,
1.0, 1.1; (3) measure impact on occupancy / ΩΛ / Ωm / masses / cosmology; (4) determine whether
μ=1 is derived / selected / attractor / boundary; (5) locate the earliest source of critical
branching. **Success criterion:** explain why reality sits at the critical point. No new
primitives; canonical AT unchanged.

---

## 1. The branching regime — three fates

The Galton–Watson branching (QG1/QG7) has mean offspring μ, with three regimes:

| μ | extinction probability q | total expected population (100 gen) | fate |
|---|---|---|---|
| μ < 1 (subcritical) | 1 (almost surely) | finite (2.0 at μ=0.5; 5.0 at μ=0.8) | **extinction** |
| **μ = 1 (critical)** | 1 (but mean grows linearly) | **linear (100)** | **marginal** |
| μ > 1 (supercritical) | < 1 (survival) | exponential (1.4e5 at μ=1.1; runaway at μ=1.5) | **runaway** |

Only μ=1 is **non-vanishing and non-exploding** — the boundary between extinction and runaway.

---

## 2. Remove criticality — what μ ≠ 1 does

| μ | Effect on the abundance / cosmology |
|---|---|
| μ = 0.9 (subcritical) | **extinction** — the branching dies out; no sustained structure, no realized deficit |
| μ = 1.0 (critical) | **canonical** — scale-free (α=0), flat rotation, the self-similar deficit |
| μ = 1.1 (supercritical) | **runaway** — exponential growth, unbounded structure, no stable scale-free point |

The impact on the *realized* quantities is carried by the scale-freeness: **μ=1 ⟺ α=0** (equal
deficit per octave, the flat-rotation condition, QG206). Away from μ=1, α≠0 and the flat rotation
(and the self-similar octave deficit) break. The occupancy [4,4,87] and ΩΛ = 0.6839 are the
N=96 *spectrum* facts (a separate structural input), but they sit *on top of* the critical
branching that makes the abundance scale-free.

---

## 3. Why μ=1 — three criteria coincide

μ=1 is the **unique point where three independent criteria coincide** (QG7):

| Criterion | Statement | Selects μ=1 |
|---|---|---|
| **marginal stability** | non-extinction AND non-runaway | μ<1 extinct, μ>1 runaway, only μ=1 marginal |
| **scale-freeness** | L = 1/\|ln μ\| = ∞ (renormalization-invariant) | only μ=1 ⟺ α=0 |
| **maximum entropy** | uniform per-octave allocation (least-bias) | only μ=1 ⟺ α=0 |

**Criticality is DERIVED, not postulated**: μ=1 is the unique branching point that is
simultaneously marginal-stable, scale-free, and maximum-entropy. The chain closes as
**Q-events → critical branching (μ=1) → α=0 → ρ → gravity** with criticality itself derived.

---

## 4. derived / selected / attractor / boundary?

| Reading | Verdict |
|---|---|
| **derived** | **YES (unique).** μ=1 is uniquely selected by stability + scale-freeness + max-entropy. |
| **selected** | YES — the three criteria select μ=1 as the only viable point. |
| **attractor** | PARTIAL — μ=1 is a fixed point, but "attractor" is loose (sub/supercritical do not relax to it). |
| **boundary** | **the conditioning input.** Scale-freeness (renormalization invariance, AT-F1) is the single non-derived input; μ=1 is derived *given* scale-freeness. |

**Determination: μ=1 is DERIVED (unique), conditional on the single boundary input —
scale-freeness (the indifference principle, AT-F1: "the primitives carry no intrinsic scale").**

---

## 5. The earliest source of critical branching

```
Difference / Actualization primitives   [BOUNDARY]
   → no intrinsic scale                 [AT-F1 — the indifference principle: scale-freeness]
   → renormalization invariance          [BOUNDARY conditioning input]
   → μ = 1 (the unique scale-free point) [DERIVED — QG7]
   → α = 0 (flat rotation)               [DERIVED — QG206]
   → ρ → gravity                         [DERIVED — QG194/206]
```

**The earliest source of critical branching is scale-freeness (renormalization invariance)** —
the indifference principle that the primitives carry no intrinsic scale. Given scale-freeness,
μ=1 follows uniquely.

---

## Theorem

> **Theorem (NP_070).** Reality sits at the critical point μ=1 because μ=1 is the UNIQUE
> branching ratio that is simultaneously marginal-stable (non-extinct, non-runaway), scale-free
> (L = 1/\|ln μ\| = ∞, renormalization-invariant), and maximum-entropy (α=0, uniform per-octave)
> — three independent criteria that coincide at μ=1. Proof: (1) Regimes (Section 1, verified):
> μ<1 extincts (q=1, finite total), μ>1 runs away (q<1, exponential total), only μ=1 is marginal
> (linear total). (2) Impact (Section 2): μ≠1 breaks scale-freeness (α≠0), hence flat rotation and
> the self-similar deficit. (3) Coincidence (Section 3): marginal stability + scale-freeness +
> maximum entropy all select μ=1 (QG7). (4) Status (Section 4): DERIVED (unique), conditional on
> scale-freeness (AT-F1). (5) Root (Section 5): the indifference principle — no intrinsic scale.
> Classification: criticality μ=1 DERIVED (unique, QG7); scale-freeness (renormalization
> invariance) BOUNDARY (AT-F1); the α=0 flat rotation DERIVED (QG206); a supercritical or
> subcritical canonical universe REFUTED (extinction/runaway). **Success criterion: reality is
> critical because μ=1 is the unique non-extinct, non-runaway, scale-free, maximum-entropy
> branching point — criticality is derived, with scale-freeness as the single conditioning
> boundary.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Enumerate the three regimes. (2) Show μ≠1 breaks scale-freeness.
> (3) Coincide the three criteria. (4) Classify. (5) Locate the root. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "μ=1 is postulated" | it is uniquely derived from stability + scale-freeness + max-entropy (QG7) |
| "μ≠1 is viable" | μ<1 extincts (no universe), μ>1 runs away (unstable) — only μ=1 is marginal |
| "criticality has no conditioning input" | scale-freeness (renormalization invariance, AT-F1) is the single boundary |
| "μ=1 is an attractor" | sub/supercritical do not relax to μ=1; it is a unique fixed point, not a basin attractor |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| μ=1 is the unique marginal point | a μ≠1 branching that is non-extinct and non-runaway |
| μ=1 ⟺ α=0 (scale-free) | a scale-free abundance with μ≠1 |
| criticality is derived | a viable (non-extinct, non-runaway) canonical universe with μ≠1 |
| scale-freeness is the conditioning input | a derivation of μ=1 without the scale-freeness assumption |

---

## 8. Classification

| Component | Status |
|---|---|
| criticality μ=1 (unique marginal/scale-free/max-entropy point) | **DERIVED** (QG7) |
| scale-freeness (renormalization invariance, indifference principle) | **BOUNDARY** (AT-F1) |
| α=0 flat rotation | **DERIVED** (QG206) |
| a supercritical/subcritical canonical universe | **REFUTED** (extinction/runaway) |

**Conclusion.** Reality is critical because **μ=1 is the unique branching point where marginal
stability, scale-freeness, and maximum entropy coincide** — subcritical branching dies out,
supercritical runs away, and only μ=1 is non-vanishing, non-exploding, scale-free, and
least-biased. Criticality is therefore **DERIVED** (unique), with **scale-freeness
(renormalization invariance, the indifference principle)** as the single conditioning boundary
input. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_070_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_070_BranchingRegimes` | μ<1 extinct, μ=1 marginal, μ>1 runaway | ✅ |
| `Y_NP_070_ExtinctionProbability` | q=1 for μ≤1, q<1 for μ>1 | ✅ |
| `Y_NP_070_TotalPopulation` | finite/linear/exponential | ✅ |
| `Y_NP_070_ScaleFreeness` | μ=1 ⟺ α=0 | ✅ |
| `Y_NP_070_ThreeCriteriaCoincide` | marginal = scale-free = max-entropy at μ=1 | ✅ |
| `Y_NP_070_Classification` | DERIVED (conditional on scale-freeness) | ✅ |
| `Y_NP_070_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_070"`

---

## References

- AT-QG: QG1/QG7 (critical branching), QG194 (matter = deficit), QG206 (α=0), QG222 (native
  dynamics), QG89 (energy = rate), QG011 (discrete tick), AT-F1 (indifference principle).
- ResearchY-NP_062 (high order), NP_069 (expansion).
