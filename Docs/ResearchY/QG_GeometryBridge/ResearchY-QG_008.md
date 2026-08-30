# ResearchY-QG_008 — Finite Distinguishability Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_008 (permanent)
**Title:** Finite Distinguishability Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `QG_GeometryBridge/ResearchY-QG_008.md`
**Depends on:** ResearchY-QG_004 (ρ nature), QG_005 (count-to-geometry), QG_006 (count
conservation origin), QG_007 (count conservation necessity)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_008_Tests.cs`

---

## Purpose

**Why must distinguishability be finite?** QG_007 showed that count conservation
follows from Difference via the FINITENESS of the state space. This audit asks whether
the finiteness itself is a NECESSARY consequence of Difference or a REMAINING BOUNDARY
assumption — and what breaks first if infinite distinguishability is allowed.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **Difference** | the primitive: the act of telling states apart |
| **distinguishability** | the state space produced by Difference |
| **finite state space** | a finite number of distinguishable states (95) |
| **infinite state space** | N → ∞ |

---

## 2. Compare: N finite vs N → ∞

| Property | Finite N (95) | Infinite N (∞) |
|---|---|---|
| **normalization** | Σρ = 1 well-defined | requires a convergent sum — NOT automatic |
| **count conservation** | clear | must be defined via a limit |
| **geometry** | √(−g) = ρ well-defined | measure needs a limit structure |
| **information** | log₂(95) = 6.57 bits | log₂(∞) = ∞ — NO finite information |

---

## 3. Does normalization survive N → ∞?

**NO — only with an additional convergence assumption.** A normalized count over an
infinite state space requires the series Σρ_k to converge; that is not automatic and
is a separate assumption.

---

## 4. Does count conservation survive N → ∞?

**ONLY AS A LIMIT.** Σρ = 1 must be defined via a limiting procedure; the finite case
is well-defined, the infinite case is not automatic.

---

## 5. Does geometry survive N → ∞?

**PARTIALLY.** √(−g) = ρ requires a measure; over an infinite space the measure needs
a limit structure. The geometry does not straightforwardly extend.

---

## 6. Does information survive N → ∞?

**NO — this is the FIRST breakdown.** The information content is log₂(N); as N → ∞,
log₂(N) → ∞. There is NO finite information content in an infinite state space.

---

## 7. Determine

| Option | Verdict |
|---|---|
| A) finiteness required | PARTIAL — required for finite information, normalization, and well-defined geometry |
| B) finiteness emergent | NO — Difference does not by itself force the count to be finite |
| C) finiteness boundary | **YES — finiteness is a BOUNDARY (a property of the state space)** |

**Finiteness is a BOUNDARY: the VALUE N=96 is derived (D_015/D_019 closure), but the
FINITENESS itself is a separate property of the state space — Difference (the act of
distinguishing) does not by itself imply a finite count.**

---

## 8. Search: smallest principle forcing finite distinguishability

The smallest principle forcing finiteness would be a convergence/completeness
requirement: the state space must admit a well-defined normalization (Σρ = 1), a
finite information content (log₂ N < ∞), and a measure (√(−g) = ρ). These jointly
require N < ∞. This is a CONSISTENCY boundary: physics (as AT defines it) requires
finiteness, but the finiteness is not derived from the meaning of Difference alone.

---

## 9. Prove or refute: Difference implies finite state count

**REFUTED — Difference does not by itself imply finiteness.** Difference produces
distinguishability (the act of telling states apart), but the COUNT of distinguishable
states is a separate input. The VALUE 96 is derived (closure, D_015/D_019), but the
finiteness of the state space is a BOUNDARY assumption — required for physics but not
logically implied by Difference.

---

## 10. If infinite distinguishability is allowed: first breakdown

| Breakdown | Order | Why |
|---|---|---|
| **information** | **FIRST** | log₂(N) → ∞ — no finite information content (QG_007's normalization still works as a limit, but information diverges immediately) |
| normalization | second | requires convergence (not automatic) |
| geometry | second | measure needs a limit structure |
| measurement | second | Born rule needs a well-defined distribution |

**The FIRST breakdown is INFORMATION: log₂(N) diverges as N → ∞.** Normalization,
measurement, and geometry can in principle be extended with limits; information cannot —
it becomes infinite.

---

## Theorem

> **Theorem (QG_008).** Finite distinguishability is a BOUNDARY — required for physics
> but not logically implied by Difference. Proof: (1) Compare finite N with N → ∞:
> normalization, count conservation, and geometry all survive only with convergence/
> limit assumptions; information does NOT survive (log₂(N) → ∞ — the FIRST breakdown).
> (2) Difference produces distinguishability (the act of telling states apart), but the
> COUNT of distinguishable states is a separate input: the VALUE N=96 is derived
> (closure, D_015/D_019), while the FINITENESS itself is a boundary property of the
> state space. (3) The smallest principle forcing finiteness is the CONSISTENCY
> requirement that the state space admit a well-defined normalization (Σρ = 1), a
> finite information content (log₂ N < ∞), and a measure (√(−g) = ρ) — these jointly
> require N < ∞. (4) Therefore: A) finiteness required — PARTIAL (required for
> information/normalization/geometry); B) finiteness emergent — NO; C) finiteness
> boundary — YES. (5) If infinite distinguishability is allowed, the FIRST breakdown is
> INFORMATION (log₂ N diverges); normalization, geometry, and measurement are second
> (limit-assumption dependent). Classification: finiteness BOUNDARY (a property of the
> state space, required for physics); the value N=96 DERIVED (closure, D_015/D_019);
> normalization/count conservation DERIVED (from finiteness, QG_007); information
> DERIVED (finite only); geometry DERIVED (needs the finite measure). No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Define the terms (Section 1). (2) Compare finite/infinite
> (Section 2, verified: information diverges). (3) Test each survival (Sections 3–6).
> (4) Determine the classification (Section 7). (5) Refute the implication and identify
> the first breakdown (Sections 9–10). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability
 → Finite State Space [BOUNDARY — required, not implied]
 → Normalization (Σρ = 1)
 → Count Conservation
 → ρ
    ├── Geometry (√(−g) = ρ)
    └── Information (log₂ N — finite only)
```

---

## 11. Necessity Proof

Finiteness is NECESSARY for physics as AT defines it: without N < ∞, information
diverges (log₂ N → ∞), normalization requires a convergence assumption, and the measure
needs a limit structure. But the necessity is CONDITIONAL — it is required FOR the
theory's observables, not logically implied BY the meaning of Difference. The state
space's finiteness is an input (boundary); its value (96) is derived.

---

## 12. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Difference implies finiteness" | Difference produces distinguishability, not a count — the count is a separate input |
| "Infinite distinguishability has finite information" | log₂(N) → ∞ as N → ∞ (divergence) |
| "Normalization is automatic for infinite N" | the series Σρ_k requires convergence — not automatic |
| "Geometry extends to infinite N" | √(−g) = ρ needs a limit structure for the measure |

---

## 13. Falsification paths

| Claim | Falsification |
|---|---|
| finiteness is a boundary | a derivation of finiteness from Difference alone |
| information breaks first | an infinite state space with finite information content |
| the value 96 is derived | a different closure value consistent with the requirements |

---

## Classification

| Component | Status |
|---|---|
| finiteness of the state space | **BOUNDARY** (required for physics, not implied by Difference) |
| the value N = 96 | **DERIVED** (closure, D_015/D_019) |
| normalization / count conservation | **DERIVED** (from finiteness, QG_007) |
| information (log₂ N) | **DERIVED** (finite only) |
| geometry (√(−g) = ρ) | **DERIVED** (needs the finite measure) |

**Finite distinguishability is a BOUNDARY: required for physics (finite information,
well-defined normalization and measure) but not logically implied by Difference. The
first breakdown with infinite distinguishability is INFORMATION. No new primitive;
canonical AT unchanged.**

---

## Open Problems

1. **Finiteness origin (QG_008 OP1).** Whether a deeper principle (beyond Difference)
   could force finiteness — the current status is that it is a boundary.

---

## Next Steps

- **Registry note:** finiteness is a boundary; the value 96 is derived; information is
   the first casualty of infinite distinguishability.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_008_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_QG_008_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_008_FiniteStates` | finite N: normalization/geometry/info well-defined | ✅ |
| `Y_QG_008_InfiniteStates` | infinite N: information diverges | ✅ |
| `Y_QG_008_NormalizationLimit` | normalization needs a convergence assumption | ✅ |
| `Y_QG_008_CountConservation` | count conservation survives via a limit | ✅ |
| `Y_QG_008_GeometryLimit` | geometry needs a limit measure | ✅ |
| `Y_QG_008_InformationLimit` | information breaks first (log₂ N → ∞) | ✅ |
| `Y_QG_008_Run` | research report | ✅ |

**Conclusion:** Finite distinguishability is a BOUNDARY — required for physics but not
logically implied by Difference (the value N=96 is derived; the finiteness is an
input). With infinite distinguishability, INFORMATION breaks first (log₂ N diverges);
normalization, geometry, and measurement are second. No new primitive; canonical AT
unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_008"`

---

## References

- ResearchY-QG_004 (ρ nature), QG_005 (count-to-geometry), QG_006 (count conservation
  origin), QG_007 (count conservation necessity).
- AT-QG: QG207 (measure preservation), QG216 (Born rule), QG228 (information).
- D-chain: D_015/D_019 (N=96 uniqueness).
