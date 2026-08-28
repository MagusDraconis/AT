# ResearchY-D_019 — Closure-Only Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_019 (permanent)
**Title:** Closure-Only Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_019.md`
**Depends on:** ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin),
D_017 (scale stability), D_018 (occupancy selection)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_019_Tests.cs`

---

## Purpose

If **all D96-specific selection rules are removed** and only Closure remains, does
Closure still produce N=96? I.e., is N=96 a **true closure theorem** or only a
**selected closure solution**?

## Accepted (from D_015…D_018)

- N=96 is unique by the seed symmetry (6|N) + three-family window (D_015); both are
  SELECTION RULES (D_016).
- Scale metrics (λ₂, ω₁) do NOT select N=96 (D_017); occupancy is a bijection of N, not
  a selector (D_018).

## The Closure mechanism (canonical)

The canonical Closure Principle (Ch4/QG282) states: *the boundary is the fixed point of
the actualization dynamics* — the activity → links → activity feedback
(QG115/QG116) saturates (positive feedback bounded by network capacity) with zero
residual link growth and a content-independent final geometry.

The implemented closure dynamics (`StructureFromContent.AdaptiveNetwork` /
`ActualizationStructures.ReinforcingNetwork`, canonical defaults K=6, damping=0.2,
feedback=0.7) takes **the size N as an INPUT** — the activity array length — and
converges the **link structure** for that fixed size. N itself is never grown or shrunk
by the dynamics.

---

## 1. Does Closure select N?

### 1.1 Closure convergence across N (canonical persistent pattern)

The canonical convergence test (`TopologyConverged`: link-growth rate between steps 40
and 80 < 0.05) applied to the canonical persistent activity pattern `PersistentActivity(n)`
for every N ∈ [32, 300]:

| Pattern | Converged N | Fraction |
|---|---|---|
| persistent | **269 / 269** | 100% |
| spread | 269 / 269 | 100% |
| uniform | 269 / 269 | 100% |
| concentrated | 56 / 269 | 21% |

**Under the canonical persistent pattern, closure converges for ALL N — N=96 is not
selected by closure at all.**

### 1.2 Counterexample: N=96 FAILS closure under the concentrated pattern

The canonical convergence criterion applied to the concentrated activity pattern fails
**even at N=96**:

| N (concentrated) | link-growth rate | converged |
|---|---|---|
| 96 | 0.1198 | **NO** |
| 64 | 0.021 | YES |
| 128 | 0.362 | NO |

Closure convergence is **content-dependent**: the same N=96 either converges or fails
depending on the initial activity pattern. N=96 is therefore not a closure attractor in
any content-independent sense.

### 1.3 The fixed point is a geometry class, not a size

The converged fixed point under the persistent pattern is **always the degree-12 K=6
ring** — links = 6N, uniform degree 12 — for every N:

| N | converged links | links/N | degree |
|---|---|---|---|
| 64 | 384 | 6.000 | 12 (uniform) |
| 90 | 540 | 6.000 | 12 |
| 96 | 576 | 6.000 | 12 |
| 120 | 720 | 6.000 | 12 |
| 128 | 768 | 6.000 | 12 |
| 192 | 1152 | 6.000 | 12 |
| 245 | 1470 | 6.000 | 12 |

Closure produces a **geometry class** (the degree-12 ring) for any size; the size is an
input, not an output.

### 1.4 N=96 has no closure signature

Adjacent N (94, 95, 96, 97, 98) all converge identically (growth = 0.000000) under the
persistent pattern. N=96 is not a boundary, extremum, or special point of the closure
dynamics.

---

## 2. Classification

| Option | Verdict |
|---|---|
| **A) Closure → unique N=96** | **NO** — 269/269 N converge under the canonical persistent pattern |
| **B) Closure → finite set of N** | **NO** — the converging set is all N under persistent/spread/uniform |
| **C) Closure → infinite family of N** | **YES** (observed) — all tested N converge under persistent/spread/uniform |
| **D) Closure does not determine N** | **YES** — the size is an input; closure determines only the geometry class (degree-12 ring) |

**Verdict: D) Closure does not determine N** (equivalently, C — an effectively infinite
family of sizes satisfies closure). N=96 is a **SELECTED closure solution**, not a
closure theorem.

---

## Theorem

> **Theorem (D_019).** Closure alone does not determine N=96. The actualization closure
> dynamics (QG115/116) converges to the degree-12 K=6 ring for essentially every size N
> under the canonical activity patterns (269/269 in [32,300] for persistent, spread, and
> uniform); the size N is an input (the activity array length), never an output of the
> convergence. Closure convergence is additionally content-dependent — under the
> concentrated pattern only 56/269 converge and **N=96 itself fails**. Therefore N=96 is
> a selected closure solution (selected by the D_015/D_016 rules: 6|N + span window),
> not a closure theorem.
>
> *Proof sketch.* (1) The closure criterion is the link-growth rate (steps 40→80) < 0.05
> (QG282). (2) For the canonical persistent pattern, the criterion holds for all 269 N
> in [32,300] (Section 1.1) — no size is excluded. (3) The converged fixed point is the
> degree-12 ring with links = 6N for every tested N (Section 1.3) — the geometry class is
> size-independent; only the size scales. (4) Under the concentrated pattern, N=96 fails
> the criterion (growth 0.1198, Section 1.2) — a direct counterexample to any
> content-independent "closure → N=96". (5) Since the closure dynamics neither excludes
> other sizes nor guarantees N=96, the size N=96 must come from outside the closure —
> the selection rules of D_015/D_016. Hence N=96 is selected, not derived. ∎

---

## Counterexamples

1. **N=95, 97, 120, 128, 192, 245 all converge** under the canonical persistent pattern —
   closure admits these sizes as fixed points (Section 1.1, 1.3).
2. **N=96 fails closure under the concentrated pattern** (growth 0.1198 > 0.05) — the
   "closure → N=96" implication fails for a valid canonical activity pattern.
3. **The converged geometry is identical in class at all N** — links = 6N, degree-12 ring —
   so no closure quantity distinguishes N=96 from any other size.

---

## Dependency Graph

```
Difference
 → Actualization (process face)
 → Closure (fixed point of activity→links→activity, QG115/116)
 → geometry class (degree-12 K=6 ring) — size-independent
 → size N — INPUT (activity array length), not output
 → selection rules (D_015: 6|N + span window; D_016) — N=96
 → physics
```

---

## Research Conclusions

1. **Closure converges for all N** under the canonical persistent/spread/uniform
   patterns (269/269 in [32,300]) — closure alone does NOT single out N=96.
2. **Closure convergence is content-dependent**: under the concentrated pattern only
   56/269 converge, and **N=96 itself fails** (growth 0.1198) — a direct
   counterexample to any content-independent closure → N=96.
3. **The closure fixed point is a geometry class** (degree-12 ring, links = 6N), not a
   size — the size N is an input (activity array length), never an output of the
   dynamics.
4. **N=96 is a SELECTED closure solution** — selected by the D_015/D_016 rules
   (6|N + span window) — **not a closure theorem**.
5. Classification: **D) Closure does not determine N** (equivalently C — an effectively
   infinite family of sizes satisfies closure).

---

## Classification

| Component | Status |
|---|---|
| closure convergence (fixed point exists) | **DERIVED** (the dynamics does converge to a ring) |
| geometry class (degree-12 K=6 ring) | **DERIVED** (size-independent output) |
| closure → unique N=96 | **REFUTED** (269/269 converge; N=96 fails under concentrated) |
| size N as input | **BOUNDARY** (the size is imposed, not derived) |
| N=96 selection (6|N + span) | **BOUNDARY** (selection rules, D_015/D_016) |

---

## Open Problems

1. **Closure-source of N=96 (D_017 OP1, now sharpened).** The canonical claim that
   "N=96 is the closure fixed point" (Ch3 Thm N96, QG282) is not supported by the
   implemented closure dynamics — closure admits all sizes. The origin of the size claim
   in the canonical text remains an open consistency question.
2. **Content-dependence of convergence (D_019 OP2).** Why does the concentrated pattern
   fail convergence at N=96 while persistent/spread/uniform succeed? (The fixed point
   exists for the latter; the former is a partial-feedback regime — the content
   dependence is the QG115 PARTIAL FEEDBACK structure.)
3. **What selects N=96 (carried).** With closure (D_019), scale (D_017), and occupancy
   (D_018) all failing to select N=96, only the D_015/D_016 selection rules (6|N + span
   window) remain — the "why these rules" question is still BOUNDARY.

---

## Next Steps

- **ResearchY-D_020 (or synthesis):** the closure-source consistency question (OP1) —
  compare the canonical Ch3/QG282 "N=96 closure" claim against the implemented dynamics;
  this is a fidelity audit of the closure claim, not a new selection mechanism.
- **Synthesis D_015→D_019:** every N=96 selection mechanism has now been tested:
  family (D_016, partial), scale (D_017, NO), occupancy (D_018, NO), closure (D_019,
  NO). The positive selector is the D_015 combination (6|N + span window), which is a
  BOUNDARY selection rule.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_019_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_019_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_019_ClosureOnly` | closure converges for ALL N under persistent pattern (269/269) | ✅ |
| `Y_D_019_FixedPoints` | fixed point = degree-12 ring (links = 6N) for all tested N | ✅ |
| `Y_D_019_AttractorCount` | converging set is all N (persistent/spread/uniform); 56/269 concentrated | ✅ |
| `Y_D_019_N96Uniqueness` | N=96 has no closure signature (adjacent N converge identically) | ✅ |
| `Y_D_019_Counterexamples` | N=96 FAILS closure under concentrated pattern (growth 0.1198) | ✅ |
| `Y_D_019_SizeIsInput` | size enters as the activity array length; never changed by the dynamics | ✅ |
| `Y_D_019_Selection` | classification D — closure does not determine N; N=96 selected (D_015) | ✅ |
| `Y_D_019_Run` | Research report | ✅ |

**Conclusion:** Closure alone does NOT produce N=96. Under the canonical persistent
pattern, closure converges for all 269/269 N in [32,300] to the degree-12 K=6 ring (a
geometry class, links = 6N); the size N is an input, not an output. Under the
concentrated pattern, N=96 itself FAILS closure (growth 0.1198). N=96 is therefore a
**SELECTED closure solution** (D_015/D_016 rules), not a closure theorem.
Classification: **D) Closure does not determine N**. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_019"`

---

## References

- ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin), D_017 (scale
  stability), D_018 (occupancy selection).
- Monograph V2.0: Ch3 (actualization, Thm N96), Ch4 (closure principle), Ch5 (spectrum).
- AT-QG: QG115 (structure-from-content), QG116 (universal attractor), QG282 (closure
  principle / boundary origin).
- `AT.Core/ResearchXH/StructureFromContent.cs` (adaptive network dynamics),
  `AT.Core/ResearchXH/ActualizationStructures.cs` (closure convergence tests).
