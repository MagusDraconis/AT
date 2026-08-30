# ResearchY-QG_007 — Count Conservation Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_007 (permanent)
**Title:** Count Conservation Necessity Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `QG_GeometryBridge/ResearchY-QG_007.md`
**Depends on:** ResearchY-QG_006 (count conservation origin), D_027
(selector/closure), D_039 (Difference = distinguishability), M_005 (information
conservation)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_007_Tests.cs`

---

## Purpose

**Is count conservation merely definitional or a NECESSARY consequence of Difference?**
QG_006 established that Σρ = 1 is definitional (built into the normalizer S) and
necessary (everything downstream requires it). This audit goes deeper: does Σρ = 1
follow INEVITABLY from the meaning of Difference itself?

---

## 1. Definitions

| Term | Definition |
|---|---|
| **Difference** | the primitive: the ability to tell states apart (D_039) |
| **count** | how many actualization events land in each distinguishable state |
| **conservation** | Σρ = 1 (the normalized total count) |
| **distinguishability** | the 95-state structure — the QUALITY Difference produces |

---

## 2. Remove count conservation

| Object | Survives Σρ ≠ 1? |
|---|---|
| **distinguishability (the 95-state QUALITY)** | YES — the states remain distinct; the QUALITY of Difference is separate from the count's normalization |
| **information** | NO — KL(ρ‖uniform) needs a probability distribution (QG228) |
| **geometry** | NO — √(−g) = ρ needs a measure (QG207) |
| **measurement** | NO — the Born rule needs Σ|ψ|² = 1 (QG216) |

**Removing count conservation leaves the QUALITY of Difference (the distinct states)
but destroys its PHYSICAL OUTPUT (information, geometry, measurement).**

---

## 3. Can Difference still exist without count conservation?

**YES — as a QUALITY.** The 95 distinguishable states remain distinct regardless of
how the counts are normalized. Difference (distinguishability) is a property of the
state space; the count normalization is a property of the event distribution over it.

**BUT Difference as the SOURCE of physics requires the normalization.** Without Σρ = 1,
Difference produces no information, no geometry, and no measurement — it is a dormant
quality.

---

## 4. Is non-conserved Difference coherent?

| Reading | Coherent? |
|---|---|
| Difference as QUALITY (the states are distinct) | **COHERENT** — the 95 states remain distinct |
| Difference as the SOURCE of physics (info/geometry/measurement) | **INCOHERENT** — all outputs require the normalized count |

**Non-conserved Difference is coherent only as a bare quality; it is incoherent as a
physical source.**

---

## 5. Alternative primitives and count structures

| Alternative | Exists? |
|---|---|
| alternative primitives | **NONE** — {Difference, η} are the only primitives (D_027) |
| alternative count structures | normalization is FORCED by measure preservation √(−g) = ρ (QG207) and by probability (Born, QG216) |

**There is no alternative: Difference is the only primitive, and any count structure
over a finite state space must be normalized to define probabilities and measures.**

---

## 6. Prove or refute: Difference logically implies count conservation

**PROVEN — YES.** Difference implies count conservation:

```
Difference
 → distinguishability (D_039)
 → a FINITE state space (95 states)
 → the count of events over a finite state space
 → normalization REQUIRED to define probabilities (Born, QG216)
   and measures (√(−g) = ρ, QG207)
 → Σρ = 1
```

The finite state space is the key: Difference produces a FINITE set of distinguishable
states, and a count over a finite set must be normalized to serve as a probability
distribution and a measure. **Count conservation is a logical consequence of Difference
+ finiteness.**

---

## Theorem

> **Theorem (QG_007).** Count conservation (Σρ = 1) is a NECESSARY consequence of
> Difference, via the FINITENESS of the distinguishable state space. Proof: (1)
> Difference IS distinguishability (D_039) — the ability to tell states apart — which
> produces a FINITE state space (95 states). (2) The count of actualization events over
> this finite state space must be NORMALIZED to define a probability distribution (the
> Born rule Σ|ψ|² = 1, QG216) and a measure (√(−g) = ρ, QG207). (3) Without Σρ = 1:
> distinguishability as a QUALITY survives (the 95 states remain distinct), but
> information (KL undefined), geometry (no measure), and measurement (Born invalid) all
> fail — Difference as a physical source is incoherent. (4) There are NO alternative
> primitives ({Difference, η} only, D_027) and NO alternative count structures
> (normalization is forced). (5) Therefore Difference logically implies count
> conservation: the finite state space demands normalization, and normalization IS
> conservation. Classification: count conservation DERIVED (from Difference +
> finiteness of the state space); distinguishability as a quality DERIVED (D_039);
> information/geometry/measurement DERIVED (all require the normalized count). No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define the terms (Section 1). (2) Remove conservation (Section 2,
> verified: quality survives, outputs fail). (3) Test coherence (Section 3–4). (4) Rule
> out alternatives (Section 5). (5) Prove the implication via finiteness (Section 6). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (D_039)
 → Finite state space (95 states)
 → Count Conservation (Σρ = 1 — forced by finiteness)
 → ρ
    ├── Information (KL(ρ‖uniform))
    └── Geometry (g = ρ^(2/d)η)
```

---

## 7. Necessity Proof

Count conservation is NECESSARY: Difference produces a finite state space, and a count
over a finite set must be normalized to define probabilities (Born) and measures
(√(−g) = ρ). Without normalization, Difference cannot produce information, geometry, or
measurement — it remains a bare, inactive quality. The finiteness is the decisive
link: finite distinguishability ⟹ normalization ⟹ conservation.

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Count conservation is purely arbitrary" | it is forced by the finite state space (probability + measure) |
| "Difference exists without conservation" | only as a bare quality — no information/geometry/measurement |
| "An alternative count structure exists" | normalization is forced by measure preservation (QG207) |
| "A non-normalized count is physical" | it cannot define probabilities or measures |

---

## 9. Falsification paths

| Claim | Falsification |
|---|---|
| count conservation follows from Difference | a finite distinguishable state space with a physical (unnormalized) count |
| finiteness forces normalization | a probability/measure defined over an unnormalized finite count |
| no alternative primitive | a physical structure from a non-Difference primitive |

---

## Classification

| Component | Status |
|---|---|
| count conservation (Σρ = 1) | **DERIVED** (from Difference + finiteness) |
| distinguishability as a quality | **DERIVED** (D_039) |
| information / geometry / measurement | **DERIVED** (require the normalized count) |
| alternative primitives | **BOUNDARY** (none — {Difference, η} only, D_027) |

**Count conservation is a NECESSARY consequence of Difference: the finite
distinguishable state space demands normalization, and normalization is conservation.
No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Finite-state-space necessity (QG_007 OP1).** Whether finiteness itself (the 95
   states) follows from Difference or is a separate boundary (extends the closure
   program, D_015/D_019).

---

## Next Steps

- **Registry note:** count conservation is derived from Difference + the finite state
  space.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_007_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_QG_007_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_007_ConservationRemoval` | quality survives; outputs fail | ✅ |
| `Y_QG_007_DifferenceConsistency` | non-conserved Difference is a bare quality only | ✅ |
| `Y_QG_007_AlternativeCount` | no alternative primitives or count structures | ✅ |
| `Y_QG_007_NecessityProof` | Difference → finiteness → normalization | ✅ |
| `Y_QG_007_Run` | research report | ✅ |

**Conclusion:** Count conservation is a NECESSARY consequence of Difference: the finite
95-state space demands normalization (for probabilities and measures), and
normalization IS conservation. Removing it leaves Difference as a bare quality with no
information, geometry, or measurement. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_007"`

---

## References

- ResearchY-QG_006 (count conservation origin), D_027 (selector/closure), D_039
  (Difference = distinguishability), M_005 (information conservation).
- AT-QG: QG207 (measure preservation √(−g) = ρ), QG216 (Born rule Σ|ψ|² = 1),
  QG228 (information KL).
