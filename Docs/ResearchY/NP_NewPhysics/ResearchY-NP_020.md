# ResearchY-NP_020 — Black Hole Information Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_020 (permanent)
**Title:** Black Hole Information Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_020.md`
**Depends on:** ResearchY-D_039 (Difference = distinguishability), M_001 (measurement
event), M_004 (information log₂ 95), M_005 (information conservation), NP_018
(distinguishability observable), NP_019 (information cosmology)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_020_Tests.cs`

---

## Purpose

**Does the Difference → Information chain change black-hole information physics?** The
standard black-hole information problem asks whether information can disappear behind
an event horizon. AT has a distinctive input: Difference = Distinguishability (D_039),
information is DERIVED from distinguishability (M_004), measurement REVEALS it (M_005),
and information is CONSERVED through actualization (M_005). This audit asks whether the
horizon can destroy, hide, redistribute, or preserve information under those premises.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **information** | the distinguishability content of the state space: log₂(95) = 6.57 bits (M_004) |
| **distinguishability** | the number/quality of distinct states (D_039: 95) |
| **event horizon** | a geometric boundary beyond which no causal influence reaches an external observer |
| **black-hole information** | the distinguishability content of states that have crossed the horizon |

---

## 2. Standard picture vs AT picture

| | Standard | AT |
|---|---|---|
| chain | state → horizon → information INACCESSIBLE | Difference → distinguishability → information |
| information source | the quantum state | the primitive Difference (D_039) |
| conservation | debated (the paradox) | CONSERVED through actualization (M_005) |
| measurement | collapse/observation | REVEALS pre-existing distinguishability (M_005) |

**In AT the information is not created by the state entering the hole — it pre-exists
as distinguishability (D_039) and is conserved through actualization (M_005). The
horizon is a geometric feature, not an information-annihilator.**

---

## 3. Test: can information be destroyed, hidden, redistributed, or preserved?

| Option | Verdict |
|---|---|
| **A) destroyed** | **NO** — count is conserved (Σρ = 1, Born QG216) and information is conserved through actualization (M_005); nothing in the chain annihilates distinguishability |
| **B) hidden** | **YES** — from an external observer, states behind the horizon are inaccessible (the standard geometry) |
| **C) redistributed** | **YES** — information is revealed and redistributed through measurement (M_005) |
| **D) preserved** | **YES** — the total information content is conserved; hiding does not remove it |

**The horizon can HIDE information but cannot DESTROY it. Under AT, option A is ruled
out; options B, C, and D hold.**

---

## 4. Analysis: count, positivity, normalization, identity, measurement chain

| Quantity | Property | Horizon effect |
|---|---|---|
| **count** | Σρ = 1 conserved (Born) | unchanged — count does not vanish |
| **positivity** | ρ ≥ 0 (probabilities) | unchanged — no negative amplitudes |
| **normalization** | the state space is normalized | unchanged — crossing does not un-normalize |
| **state identity** | 95 distinct states pre-exist (D_039) | unchanged — the states remain distinct |
| **measurement chain** | reveals + redistributes (M_005) | unchanged — the read works on either side |

**None of the conserved quantities is removed by the horizon.** The geometry hides the
states' outcomes from an external observer but does not erase their distinguishability.

---

## 5. Does an event horizon remove distinguishability?

**NO.** Distinguishability is a property of the state space (D_039: the 95 states
pre-exist and remain distinct), independent of geometry. The horizon removes ACCESS
(observability for an external agent) but not distinguishability itself. The states
behind the horizon are still distinct from each other.

---

## 6. Information balance before/after horizon formation

```
H_before = H_after   (conserved through actualization, M_005)

H_before = the distinguishability content of the in-falling states
H_after  = H_hidden (behind the horizon) + H_observer (external)
```

**The total is conserved: log₂(95) = H_hidden + H_observer.** The horizon changes the
PARTITION of information (how much is externally accessible), not the total.

---

## 7. Prove or refute: a black hole can eliminate Difference

**REFUTED.** A black hole cannot eliminate Difference:
1. Difference is the PRIMITIVE (D_039) — it cannot be removed by a derived feature (a
   geometric horizon).
2. Distinguishability is a state-space property, not a geometric one.
3. Information is conserved through actualization (M_005) — no annihilation channel.
4. Count, positivity, normalization, and identity all survive horizon crossing.

**The required mechanism is HORIZON BOOKKEEPING: information is conserved, hidden
behind the horizon, and re-encoded in the external system (Hawking radiation carries
the redistribution).** AT aligns with the unitarity-conserving resolution of the
information paradox, and grounds it in the primitive structure.

---

## 8. If NO (a black hole cannot eliminate Difference): required mechanism

| Mechanism | Role |
|---|---|
| **storage** | the in-falling states retain their distinguishability |
| **redistribution** | the information is re-encoded in the external system (radiation) |
| **encoding** | the horizon defines a new partition (hidden vs accessible) |
| **horizon bookkeeping** | the conservation law is maintained across the horizon |

---

## 9. AT prediction set

1. **Information is never destroyed** — the total distinguishability content is
   conserved through horizon formation (M_005).
2. **The horizon hides, does not erase** — states behind the horizon remain distinct
   (D_039); only external access is lost.
3. **The information balance holds** — log₂(95) = H_hidden + H_observer, exactly.
4. **Difference is primitive and indestructible** — no geometric feature can eliminate
   the fundamental distinguishability.

---

## Theorem

> **Theorem (NP_020).** Under AT, a black hole CANNOT eliminate Difference, and
> information is conserved through horizon formation — the horizon hides but never
> destroys. Proof: (1) Information is DERIVED from distinguishability (D_039: 95
> distinct states pre-exist) and CONSERVED through actualization (M_005: measurement
> reveals and redistributes, never creates or destroys). (2) The conserved quantities
> — count (Σρ = 1, Born QG216), positivity (ρ ≥ 0), normalization, and state identity
> (the 95 states remain distinct) — all survive horizon crossing; none is removed by
> the geometric boundary. (3) An event horizon removes ACCESS (external observability)
> but not DISTINGUISHABILITY: the states behind it are still distinct from each other
> (D_039 is a state-space property, not a geometric one). (4) Therefore the
> information balance holds exactly: H_before = H_after = log₂(95) = H_hidden +
> H_observer. (5) Hence: A) destroyed — NO (conservation, M_005); B) hidden — YES
> (external inaccessibility); C) redistributed — YES (measurement/radiation re-encoding,
> M_005); D) preserved — YES (the total is conserved). The required mechanism is
> HORIZON BOOKKEEPING: storage (states retain distinguishability), redistribution
> (re-encoding in the external system), encoding (new hidden/accessible partition),
> and bookkeeping (conservation across the horizon). AT resolves the information
> paradox in the UNITARITY-CONSERVING direction and grounds it in the primitives.
> COMPARISON: GR — information can classically disappear; QM — unitarity implies
> conservation but the mechanism is debated; black-hole thermodynamics — entropy ~
> area/4 (Bekenstein-Hawking); AT — information conserved through actualization, the
> horizon only repartitions it. FALSIFICATION: information NON-conservation across
> horizon formation (a measurable H_before ≠ H_after) would falsify the chain.
> Classification: information conservation DERIVED (M_005); distinguishability
> primitive-invariance DERIVED (D_039); horizon as a partition (not an annihilator)
> EMERGENT; horizon bookkeeping PREDICTION (the conservation mechanism); the
> Bekenstein-Hawking entropy-area relation BOUNDARY (not derived here). No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Compare the standard and AT chains (Section 2). (2) Test the four
> fates (Section 3, verified: destroyed NO, hidden/redistributed/preserved YES). (3)
> Check the conserved quantities (Section 4). (4) Show the horizon does not remove
> distinguishability (Section 5). (5) Balance the information (Section 6, verified:
> H_before = H_after). (6) Refute Difference elimination and identify the mechanism
> (Sections 7–8). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (D_039: 95 states)
 → Information (log₂ 95 = 6.57 bits, M_004)
 → Conservation (M_005: reveal + redistribute, never create/destroy)
 → Black Hole
 → Information Fate
    → destroyed: NO
    → hidden: YES (external inaccessibility)
    → redistributed: YES (radiation/measurement)
    → preserved: YES (H_before = H_after)
 → Horizon bookkeeping (the required mechanism)
```

---

## 10. Falsification paths

| Claim | Falsification |
|---|---|
| information conserved across the horizon | a measured H_before ≠ H_after (non-conservation) |
| the horizon hides, does not erase | external recovery of ALL information with no re-encoding (no hidden partition) |
| Difference is indestructible | a mechanism that annihilates distinguishability (removes state distinctness) |

---

## 11. Comparison table

| Framework | Information fate |
|---|---|
| **GR** | can classically disappear behind the horizon |
| **QM** | unitarity implies conservation; the mechanism is debated |
| **Black-hole thermodynamics** | entropy ~ area/4 (Bekenstein-Hawking) — an AREA law, no annihilation |
| **AT** | conserved through actualization; the horizon repartitions (hidden/accessible), never destroys |

---

## Classification

| Component | Status |
|---|---|
| information conservation | **DERIVED** (M_005) |
| distinguishability primitive-invariance | **DERIVED** (D_039) |
| horizon as a partition (not annihilator) | **EMERGENT** |
| horizon bookkeeping (the conservation mechanism) | **PREDICTION** |
| Bekenstein-Hawking entropy-area relation | **BOUNDARY** (not derived here) |

**AT resolves the black-hole information paradox in the conservation direction: the
horizon hides and repartitions information but can never destroy Difference. No new
primitive; canonical AT unchanged.**

---

## Open Problems

1. **Horizon bookkeeping mechanism (NP_020 OP1).** The exact re-encoding of the hidden
   information into the external (radiation) system — the AT statement is conservation,
   the detailed mechanism is open.

---

## Next Steps

- **Registry note:** AT aligns with unitarity-conserving information recovery; the
  horizon is a partition, not an annihilator.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_020_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_020_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_020_DifferenceConservation` | Difference/disting. is conserved | ✅ |
| `Y_NP_020_InformationBalance` | H_before = H_after | ✅ |
| `Y_NP_020_StateIdentity` | 95 states remain distinct | ✅ |
| `Y_NP_020_HorizonCrossing` | conserved quantities survive | ✅ |
| `Y_NP_020_InformationFate` | destroyed NO; hidden/redistributed/preserved YES | ✅ |
| `Y_NP_020_DependencyTrace` | chain to horizon bookkeeping | ✅ |
| `Y_NP_020_Run` | research report | ✅ |

**Conclusion:** A black hole cannot eliminate Difference. Information is conserved
through horizon formation (M_005): the horizon hides and repartitions it but never
destroys it. AT resolves the paradox in the conservation direction with horizon
bookkeeping. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_020"`

---

## References

- ResearchY-D_039 (Difference = distinguishability), M_001 (measurement event), M_004
  (information log₂ 95), M_005 (information conservation), NP_018 (distinguishability
  observable), NP_019 (information cosmology).
- AT-QG: QG216 (Born rule, count conservation).
