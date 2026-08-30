# ResearchY-NP_021 — Information Horizon Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_021 (permanent)
**Title:** Information Horizon Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_021.md`
**Depends on:** ResearchY-NP_018 (distinguishability observable), NP_019 (information
cosmology), NP_020 (black hole information)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_021_Tests.cs`

---

## Purpose

**If information is conserved, where is it stored across a horizon?** NP_020
established that a black hole cannot destroy information: it hides and repartitions it.
This audit traces the information before and after the horizon and determines the
specific mechanism — storage, redistribution, encoding, or state-space expansion —
that preserves distinguishability.

---

## 1. Trace information before the horizon

| Quantity | Value | Status |
|---|---|---|
| total information H | log₂(95) = 6.57 bits | conserved (M_004/M_005) |
| state space | 95 distinct states (D_039) | the primitive distinguishability |
| count | Σρ = 1 (Born, QG216) | conserved |
| location | in the distinguishable states themselves | pre-existing (D_039) |

**Before the horizon, the information lives in the 95-state distinguishability —
it is a property of the states, not of the geometry.**

---

## 2. Trace information after the horizon

| Quantity | Value | Status |
|---|---|---|
| hidden information H_hidden | ≤ H | behind the horizon (inaccessible externally) |
| observer information H_observer | H − H_hidden | externally accessible (radiation) |
| total H | H_hidden + H_observer = log₂(95) | CONSERVED (M_005) |
| state space | still 95 distinct states | distinguishability intact (D_039) |

**After the horizon, the same information is PARTITIONED: part hidden, part in the
external radiation. The total is unchanged.**

---

## 3. Test: storage, redistribution, encoding, state-space expansion

| Mechanism | Works? | Why |
|---|---|---|
| **storage** | **YES** | the in-falling states retain their distinguishability (D_039) — the information is stored in the distinct states behind the horizon |
| **redistribution** | **YES** | the external system (Hawking radiation) re-encodes the information over time (M_005: redistribute) |
| **encoding** | **YES** | the horizon defines a new partition: hidden vs accessible — the same information in a different split |
| **state-space expansion** | **NO** | the state space is FIXED at 95 (D_039 — the primitive distinguishability count); it cannot expand to "absorb" the information |

**The conservation mechanism is HORIZON BOOKKEEPING: storage + redistribution +
encoding. State-space expansion is NOT available — the state space is the primitive,
fixed at 95.**

---

## 4. Which mechanism preserves distinguishability?

**Storage + redistribution + encoding preserve distinguishability:**

```
H = log₂(95)              (the conserved total, M_004/M_005)
  = H_hidden              (stored in the distinct states behind the horizon)
  + H_observer            (re-encoded in the external radiation)
```

The distinguishability itself (the 95-state structure, D_039) is never reduced; only
its PARTITION (how much is externally accessible) changes.

---

## Theorem

> **Theorem (NP_021).** Information conservation across a horizon is implemented by
> HORIZON BOOKKEEPING — storage, redistribution, and encoding — NOT by state-space
> expansion. Proof: (1) The total information H = log₂(95) = 6.57 bits is conserved
> through actualization (M_004/M_005). (2) Before the horizon, the information lives
> in the 95-state distinguishability (D_039) — a property of the states, not the
> geometry. (3) After the horizon, the same information is PARTITIONED: H = H_hidden +
> H_observer, with the hidden part stored in the distinct states behind the horizon and
> the observer part re-encoded in the external radiation (M_005: reveal + redistribute).
> (4) STORAGE works because the in-falling states retain their distinguishability
> (D_039); REDISTRIBUTION works because the external system re-encodes the information
> over time; ENCODING works because the horizon defines a new hidden/accessible
> partition. (5) STATE-SPACE EXPANSION does NOT occur: the state space is the PRIMITIVE
> distinguishability count, FIXED at 95 (D_039) — it cannot grow to "absorb" the
> information. (6) Therefore the conservation mechanism is horizon bookkeeping
> (storage + redistribution + encoding), and the balance H_hidden + H_observer =
> log₂(95) holds exactly. FALSIFICATION: a horizon where the accessible information
> exceeds H_hidden's complement (violating the partition) or where the state count
> changes (≠ 95) would falsify the bookkeeping. Classification: the information balance
> is DERIVED (M_005 conservation); storage in distinct states DERIVED (D_039);
> redistribution into the external system EMERGENT (the radiation/measurement channel);
> encoding (the hidden/accessible partition) EMERGENT; horizon bookkeeping as a whole
> PREDICTION (the conservation mechanism); state-space expansion REFUTED (the state
> space is fixed, D_039). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Trace before (Section 1, verified: H in the 95 states). (2) Trace
> after (Section 2, verified: H = H_hidden + H_observer). (3) Test the mechanisms
> (Section 3, verified: storage/redistribution/encoding yes; expansion no). (4) Identify
> the preserving mechanism (Section 4). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (95 states, D_039)
 → Information (log₂ 95 = 6.57 bits, M_004)
 → Conservation (M_005)
 → Horizon
 → Information Fate
    → storage (states retain distinguishability)
    → redistribution (external radiation re-encodes)
    → encoding (hidden/accessible partition)
    → state-space expansion: REFUTED (state space fixed at 95)
 → Information balance (H_hidden + H_observer = log₂ 95)
```

---

## 5. Falsification paths

| Claim | Falsification |
|---|---|
| H = H_hidden + H_observer (partition) | an accessible information content inconsistent with the partition (total ≠ log₂ 95) |
| the state space is fixed at 95 | a measured state count ≠ 95 (state-space change) |
| storage in distinct states | a mechanism that merges the hidden states (losing distinguishability) |
| redistribution into radiation | a horizon with NO re-encoding channel (information permanently inaccessible) |

---

## Classification

| Component | Status |
|---|---|
| information balance (H = H_hidden + H_observer) | **DERIVED** (M_005) |
| storage in distinct states | **DERIVED** (D_039) |
| redistribution into the external system | **EMERGENT** (radiation/measurement channel) |
| encoding (hidden/accessible partition) | **EMERGENT** |
| horizon bookkeeping (the whole mechanism) | **PREDICTION** |
| state-space expansion | **REFUTED** (state space fixed at 95) |

**Information conservation across a horizon is implemented by horizon bookkeeping —
storage + redistribution + encoding — with the state space fixed at 95. State-space
expansion is refuted. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Re-encoding rate (NP_021 OP1).** The detailed rate at which the hidden information
   is redistributed into the external radiation (the AT statement is conservation; the
   re-encoding dynamics is open).

---

## Next Steps

- **Registry note:** the horizon stores, redistributes, and re-encodes information;
   the state space does not expand.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_021_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_021_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_021_PreHorizon` | information in the 95 states before | ✅ |
| `Y_NP_021_PostHorizon` | H = H_hidden + H_observer after | ✅ |
| `Y_NP_021_InformationStorage` | storage in distinct states (D_039) | ✅ |
| `Y_NP_021_InformationRedistribution` | redistribution into the external system | ✅ |
| `Y_NP_021_Run` | research report | ✅ |

**Conclusion:** Information conservation across a horizon is implemented by horizon
bookkeeping — storage (in the distinct states), redistribution (into the external
radiation), and encoding (the hidden/accessible partition) — with the state space fixed
at 95. State-space expansion is refuted. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_021"`

---

## References

- ResearchY-NP_018 (distinguishability observable), NP_019 (information cosmology),
  NP_020 (black hole information), D_039 (state identity), M_004 (information),
  M_005 (conservation).
- AT-QG: QG216 (Born rule, count conservation).
