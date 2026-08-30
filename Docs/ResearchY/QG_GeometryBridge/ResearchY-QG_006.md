# ResearchY-QG_006 — Count Conservation Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_006 (permanent)
**Title:** Count Conservation Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `QG_GeometryBridge/ResearchY-QG_006.md`
**Depends on:** ResearchY-QG_005 (count-to-geometry), D_027 (selector/closure),
M_005 (information conservation), NP_020 (black hole information), NP_021 (information
horizon)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_006_Tests.cs`

---

## Purpose

**Why must count be conserved?** QG_005 established that geometry requires ρ, and the
whole program uses count conservation (Σρ = 1). This audit asks whether count
conservation is MERELY DEFINITIONAL (built into the counting measure) or a DEEPER
necessary principle — and what disappears first if it is removed.

---

## 1. Count conservation by construction

The count density is defined with the normalizer S (QG194):

```
ρ_k = μ^k / S,   Σρ_k = Σμ^k/S = 1
```

**Count conservation is DEFINITIONAL: it is built into the definition of ρ as a
normalized counting measure.** The normalizer S guarantees Σρ = 1 by construction
(QG194/222: count conservation is the native Noether/continuity statement).

---

## 2. Remove count conservation — what disappears first?

| Quantity | Without Σρ = 1 |
|---|---|
| **geometry** | √(−g) = ρ FAILS — the metric is no longer a measure; g = ρ^(2/d)η is undefined as a ruler |
| **information** | I = KL(ρ‖uniform) UNDEFINED — ρ is not a probability distribution |
| **measurement** | Born rule Σ|ψ|² = 1 INVALID — probabilities do not sum to one |
| **black-hole bookkeeping** | H_before ≠ H_after — information is not conserved (NP_020/021) |

**EVERYTHING disappears TOGETHER.** There is no "first" — count conservation is the
foundation; every downstream structure requires it simultaneously.

---

## 3. Definitional or necessary?

| View | Verdict |
|---|---|
| **definitional** | YES — Σρ = 1 is built into the counting measure via the normalizer S (QG194) |
| **necessary** | YES — every downstream structure (geometry, information, measurement, black-hole bookkeeping) requires it |

**Count conservation is BOTH definitional and necessary: it is built into ρ's
definition, and precisely for that reason everything downstream depends on it.** The
"definition" is not arbitrary — it is the minimal consistent way to define a counting
measure, and removing it collapses the entire structure.

---

## 4. Analysis

| Quantity | Requires count conservation? | Why |
|---|---|---|
| **geometry** | YES | √(−g) = ρ (QG207) requires ρ to be a measure |
| **information** | YES | KL(ρ‖uniform) requires a probability distribution |
| **measurement** | YES | the Born rule Σ|ψ|² = 1 (QG216) |
| **black-hole bookkeeping** | YES | H_before = H_after (M_005/NP_020/021) |

---

## Theorem

> **Theorem (QG_006).** Count conservation (Σρ = 1) is DEFINITIONAL — built into the
> counting measure via the normalizer S (ρ_k = μ^k/S, QG194) — and NECESSARY: removing
> it collapses geometry, information, measurement, and black-hole bookkeeping
> SIMULTANEOUSLY (there is no "first" — all require the normalized count). Proof: (1)
> The count density is defined with the normalizer S, giving Σρ_k = Σμ^k/S = 1 by
> construction (QG194/222 — the native Noether/continuity statement). (2) REMOVE count
> conservation: geometry fails (√(−g) = ρ is no longer a measure, QG207); information
> fails (KL(ρ‖uniform) is undefined for a non-normalized ρ, QG228); measurement fails
> (the Born rule Σ|ψ|² = 1 is invalid, QG216); black-hole bookkeeping fails (H_before ≠
> H_after, NP_020/021). (3) There is NO ordering — every quantity fails at once, because
> all are functions of the normalized count. (4) Therefore count conservation is
> definitional (built into ρ's definition) AND necessary (the foundation of every
> downstream structure); the definition is not arbitrary but the minimal consistent one.
> Classification: count conservation DERIVED (from the definition of ρ as a normalized
> counting measure, QG194); geometry/information/measurement/bookkeeping DERIVED (all
> require the normalized count). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Show the construction (Section 1, verified: Σρ = 1 by the
> normalizer). (2) Test removal (Section 2, verified: everything fails together). (3)
> Classify definitional vs necessary (Section 3). (4) Analyze the dependencies
> (Section 4). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability
 → Count Conservation (Σρ = 1, the normalizer S, QG194)
    ├── Geometry (√(−g) = ρ, QG207)
    ├── Information (KL(ρ‖uniform), QG228)
    ├── Measurement (Born Σ|ψ|² = 1, QG216)
    └── Black-hole bookkeeping (H_before = H_after, NP_020/021)
```

---

## 5. Falsification paths

| Claim | Falsification |
|---|---|
| count conservation is definitional | a normalized counting measure without Σρ = 1 |
| all structures require it | a structure (geometry/info/measurement) functioning with Σρ ≠ 1 |
| there is no ordering | a quantity that survives the removal while another fails |

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Count conservation is arbitrary" | it is the minimal consistent definition of a counting measure (the normalizer S) |
| "Geometry survives without it" | √(−g) = ρ requires a normalized ρ (QG207) |
| "Information survives without it" | KL requires a probability distribution (QG228) |
| "Black-hole information is independent" | H_before = H_after is count conservation (NP_020/021) |

---

## Classification

| Component | Status |
|---|---|
| count conservation (Σρ = 1) | **DERIVED** (from the definition of ρ, the normalizer S, QG194) |
| geometry (√(−g) = ρ) | **DERIVED** (requires the normalized count) |
| information (KL(ρ‖uniform)) | **DERIVED** (requires the normalized count) |
| measurement (Born Σ|ψ|² = 1) | **DERIVED** (requires the normalized count) |
| black-hole bookkeeping | **DERIVED** (requires the normalized count) |

**Count conservation is definitional (built into the counting measure) and necessary
(every downstream structure requires it). There is no "first" quantity lost — they all
collapse together. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Normalizer origin (QG_006 OP1).** Whether the normalizer S has a deeper origin
   beyond guaranteeing Σρ = 1 (the minimal-consistency reading).

---

## Next Steps

- **Registry note:** count conservation is the definitional foundation — Σρ = 1 via
  the normalizer S; all downstream structures require it.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_006_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_QG_006_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_006_CountConservation` | Σρ = 1 by construction (normalizer S) | ✅ |
| `Y_QG_006_GeometryRemoval` | geometry fails without Σρ = 1 | ✅ |
| `Y_QG_006_InformationRemoval` | information fails without Σρ = 1 | ✅ |
| `Y_QG_006_MeasurementRemoval` | measurement fails without Σρ = 1 | ✅ |
| `Y_QG_006_BlackHoleBookkeeping` | bookkeeping fails without Σρ = 1 | ✅ |
| `Y_QG_006_Run` | research report | ✅ |

**Conclusion:** Count conservation (Σρ = 1) is DEFINITIONAL — built into the counting
measure via the normalizer S (QG194) — and NECESSARY: removing it collapses geometry,
information, measurement, and black-hole bookkeeping SIMULTANEOUSLY. There is no
"first" quantity lost. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_006"`

---

## References

- ResearchY-QG_005 (count-to-geometry), D_027 (selector/closure), M_005 (information
  conservation), NP_020 (black hole information), NP_021 (information horizon).
- AT-QG: QG194 (normalizer S, count conservation), QG207 (measure preservation),
  QG216 (Born rule), QG222 (metric dynamics), QG228 (information).
