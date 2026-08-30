# ResearchY-QG_010 — Observable Finiteness Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_010 (permanent)
**Title:** Observable Finiteness Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `QG_GeometryBridge/ResearchY-QG_010.md`
**Depends on:** ResearchY-QG_008 (finite distinguishability), D_039 (state identity
origin), M_004 (measurement information), M_005 (information conservation), NP_018
(distinguishability observable)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_010_Tests.cs`

---

## Purpose

**Why is the observable state space finite if infinite distinguishability is
consistent?** QG_009 established that infinite state spaces CAN be normalized (geometric
ρ_k = (1−r)·r^k sums to 1), CAN carry finite realized entropy (2.0 bits), and CAN
support geometry and measurement — the only failure is the KL-to-uniform observable
chain (no uniform reference on a countable set). This audit asks the natural follow-up:
**does observability itself select finite state spaces?** If a finite measurement event
can only carry finite information, then the set of states it can RESOLVE must be finite —
making observable finiteness a derived consequence of measurement, not a leftover
boundary.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **observable state space** | the set of states a measurement event can resolve |
| **finite observer** | a distinguishable subsystem with finite resolution (M_006) |
| **finite measurement** | an actualization event reading both quadratures of one mode (M_001) |
| **finite resolution** | the granularity at which an event distinguishes outcomes |
| **finite bookkeeping** | the finite count of distinguishable outcomes an event can record |
| **distinguishability** | the state space produced by Difference (D_039) |

---

## 2. Compare: observable finite vs observable infinite

| Property | Observable finite N (95) | Observable infinite N (∞) |
|---|---|---|
| **state identity** | 95 distinct states, fully resolvable (D_039) | infinite states — but only finitely many resolvable per event |
| **measurement** | event reads both quadratures, resolves 1 of 95 (M_001/M_002) | event must resolve 1 of ∞ — impossible with finite information |
| **information gain** | log₂(95) = 6.57 bits per event (M_004) | log₂(N) → ∞ per event — DIVERGES |
| **distinguishability** | 95/95 realized | infinite in principle; finite in observation |

**The crucial asymmetry:** the STATE SPACE can in principle be infinite (QG_009), but
the OBSERVABLE state space — the set an event can resolve — is bounded by the finite
information capacity of a single measurement event (M_004).

---

## 3. Test: state identity

- **Finite:** 95 states, each distinct, each resolvable (D_039). Identity is complete:
  every state can be told apart from every other.
- **Infinite:** states are pairwise distinct in principle (countable), but a single
  event can identify at most one outcome out of the finite set its information allows.
  The identity of the FULL set is unobservable — only the resolved subset is.

**State identity is OBSERVABLE only to the extent the measurement event can resolve it.**

---

## 4. Test: measurement

M_001: a measurement event is an actualization event applied to a distinguishable
state — it reads BOTH quadratures of ONE complex mode (the {cos, sin} basis, D_037).
The event is a FINITE act: it produces one outcome (one pinned phase, one realized
state).

- **Finite N:** one event resolves 1 of 95 outcomes — the outcome alphabet is the
  finite state space. Measurement is complete and idempotent (M_002).
- **Infinite N:** one event would have to resolve 1 of an infinite alphabet. But the
  event carries finite information (M_004) — an infinite alphabet cannot be indexed by
  a finite read. Measurement cannot resolve an infinite observable space.

**Measurement is a finite-resolution act; it selects a finite outcome set.**

---

## 5. Test: information gain

M_004: the maximum information content of ONE actualization event is log₂(95) ≈ 6.57
bits — the size of the distinguishable state space. A measurement reads both
quadratures (M_001), resolving WHICH of the 95 states is realized: gain = log₂(N_obs)
for a uniform space.

- **Finite N:** gain per event = log₂(95) = 6.57 bits (finite, verified).
- **Infinite N:** gain per event = log₂(N_obs) → ∞. A single event would need to carry
  infinite information to identify one state out of infinitely many. A finite act
  cannot.

**Information capacity per event is the binding constraint: log₂(N_obs) must be finite
⟹ N_obs must be finite.**

---

## 6. Test: distinguishability

- **Finite:** distinguishability is fully realized — all 95 states are distinguishable
  AND observable (D_039).
- **Infinite:** distinguishability exists in principle (countable distinctness) but
  observable distinguishability — the number of states an event can tell apart — is
  bounded by the event's information capacity.

**Observable distinguishability ≤ 2^(info per event) < ∞.** The observable state space
is the set of states a finite event can actually tell apart.

---

## 7. What physical property selects finite observability?

**The finite information capacity of a single measurement event (M_004).** A
measurement event is a finite act (M_001: reads both quadratures of one mode); its
maximum information content is log₂(N_obs) (M_004). For the event to resolve WHICH
state is realized, the state space must be indexable by that finite information. An
infinite observable space would require infinite information per event — a
contradiction with the event being a finite act.

The selection chain:

```
finite measurement event (M_001)
 → finite information capacity log₂(N_obs) (M_004)
 → finite outcome alphabet
 → FINITE OBSERVABLE STATE SPACE
```

---

## 8. Search: finite observer, measurement, resolution, bookkeeping

| Candidate | Status | Role |
|---|---|---|
| **finite observer** | YES (M_006) | the observer is ITSELF a distinguishable subsystem — a finite-resolution receiver of the redistribution |
| **finite measurement** | YES (M_001) | the actualization event reads both quadratures of one mode — a finite act |
| **finite resolution** | YES (M_002/M_004) | the event resolves ONE outcome; information per event = log₂(N_obs) |
| **finite bookkeeping** | YES (M_005, NP_021) | the outcome + observer record carries finite information; horizon bookkeeping is finite (log₂95 conserved) |

ALL four are finite — and they are the mechanism by which observability selects a
finite state space. The observer (finite), the act (finite), the resolution (finite),
and the bookkeeping (finite) jointly bound the observable state space.

---

## 9. Prove or refute: observability requires finite distinguishability

**PROVEN.** Observability requires finite distinguishability because:

1. A measurement event is a FINITE act (M_001) — it reads both quadratures of one
   complex mode and produces one outcome.
2. Its information capacity is finite: log₂(N_obs) bits per event (M_004).
3. To resolve WHICH state is realized, the event must index the state space — and an
   infinite observable state space would require log₂(N) → ∞ bits per event.
4. A finite act cannot carry infinite information.
5. Therefore the OBSERVABLE state space is finite. Observability — the act of
   measuring — selects finite state spaces.

This does NOT contradict QG_009: infinite state spaces are structurally consistent
(they normalize, carry finite entropy, support geometry and measurement-in-principle).
But they are not OBSERVABLE — a finite event cannot resolve an infinite alphabet. The
observable projection of any state space is finite.

---

## 10. Resolution of QG_009 OP1

QG_009 asked whether OBSERVABILITY (not Difference) is what pins N < ∞. **ANSWER:
YES.** The finiteness of the OBSERVABLE state space is DERIVED from the finite
information capacity of the measurement event (M_004), not from Difference. Difference
produces distinguishability (D_039); observability — the finite act of resolving a
state — forces the RESOLVABLE set to be finite. The finite state space that physics
uses is the observable projection: the largest set a single actualization event can
resolve.

---

## Theorem

> **Theorem (QG_010).** Observability requires finite distinguishability: the
> observable state space is finite because a measurement event is a finite act with
> finite information capacity. Proof: (1) A measurement event reads both quadratures
> of one complex mode (M_001, D_037) and produces ONE outcome — a finite act. (2) Its
> maximum information content is log₂(N_obs) (M_004): resolving WHICH of N_obs states
> is realized carries log₂(N_obs) bits; for the uniform space this is log₂(95) = 6.57
> bits (verified). (3) If the observable state space were infinite, resolving one
> outcome would require log₂(N) → ∞ bits per event (verified: log₂(10⁹) = 29.9 bits
> and growing). (4) A finite act cannot carry infinite information — the event cannot
> index an infinite alphabet. (5) Therefore N_obs < ∞: the observable state space is
> finite. (6) The selection mechanism is the finite chain: finite measurement event
> (M_001) → finite information capacity (M_004) → finite outcome alphabet → finite
> observable state space; the finite observer (M_006), resolution (M_002), and
> bookkeeping (M_005/NP_021) are the finite links. (7) This does not contradict
> QG_009 (infinite state spaces are structurally consistent) — it REFINES it: the
> observable projection of any state space is finite, resolving QG_009 OP1
> (observability, not Difference, pins the finite observable state space).
> Classification: observable finiteness DERIVED (from the finite measurement event's
> information capacity, M_004); the finite measurement event EMERGENT (M_001); the
> finite information capacity DERIVED (M_004); state identity DERIVED (D_039); the
> underlying state space's finiteness remains BOUNDARY (QG_008 — required for the
> KL-to-uniform chain); observable distinguishability DERIVED (bounded by the event's
> capacity). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define observable space (Section 1). (2) Compare finite/infinite
> (Section 2). (3) Test identity/measurement/info/distinguishability (Sections 3–6).
> (4) Identify the selecting property (Sections 7–8). (5) Prove the implication
> (Section 9) and resolve QG_009 OP1 (Section 10). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (D_039)
 → Finite Measurement Event (M_001 — reads both quadratures of one mode)
 → Finite Information Capacity log₂(N_obs) (M_004)
 → Observable Finiteness (N_obs < ∞) [DERIVED]
    ├── Information (log₂ N_obs finite; I_occ = KL(ρ‖uniform) well-defined)
    └── Geometry (measure √(−g) = ρ on the finite observable space)
```

---

## 11. Necessity Proof

Observability NECESSARILY requires finite distinguishability: the measurement event is
a finite act (M_001), its information capacity is finite (M_004), and an infinite
outcome alphabet would demand infinite information per event — impossible. The
necessity is therefore CONDITIONAL on measurement being a finite act (M_001) with
finite capacity (M_004) — both of which are established measurement-program results.
This is the strongest sense in which observable finiteness is DERIVED, not assumed.

---

## 12. Counterexamples

| Attempt | Why it fails |
|---|---|
| "An infinite observable state space has finite per-event info" | log₂(N_obs) → ∞ — per-event information diverges (verified) |
| "A finite event can resolve 1 of ∞ outcomes" | a finite act cannot index an infinite alphabet — needs infinite info |
| "Observability is independent of state-space size" | measurement resolves state identity (M_001/M_002) — the resolvable set IS the observable space |
| "The observer can be infinite" | the observer is a distinguishable subsystem (M_006) — finite resolution |
| "QG_009 makes observability infinite-consistent" | QG_009 is structural; observability projects any space onto a finite resolvable set |

---

## 13. Falsification paths

| Claim | Falsification |
|---|---|
| observable finiteness is derived from event capacity | an observable state space larger than 2^(info per event) |
| the measurement event is finite | an event resolving an infinite alphabet with finite information |
| per-event info is log₂(N_obs) | a resolution mechanism whose information gain exceeds the event's capacity |
| the observer is finite | an infinite observer with infinite resolution (contradicts M_006) |

---

## Classification

| Component | Status |
|---|---|
| **observable state space finiteness** | **DERIVED** (from the finite measurement event's information capacity, M_004) |
| the finite measurement event | **EMERGENT** (M_001 — the actualization readout) |
| finite information capacity log₂(N_obs) | **DERIVED** (M_004) |
| state identity (95 states) | **DERIVED** (D_039) |
| underlying state-space finiteness | **BOUNDARY** (QG_008 — required for the KL-to-uniform chain) |
| observable distinguishability | **DERIVED** (bounded by the event's capacity: N_obs ≤ 2^(bits/event)) |

**Observability requires finite distinguishability — the finite measurement event
(M_001) with finite information capacity (M_004) forces the observable state space to
be finite. This resolves QG_009 OP1: observability, not Difference, pins the finite
observable state space. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **None new.** QG_009 OP1 (does observability pin finiteness?) is RESOLVED — YES:
   the observable state space is finite because the measurement event is a finite act
   with finite information capacity.

---

## Next Steps

- **Registry note:** observable finiteness is DERIVED (from the finite measurement
   event); the underlying state-space finiteness remains BOUNDARY (QG_008); the KL-to-
   uniform chain (I_occ, ΩΛ) is well-defined precisely because observability makes the
   observable space finite.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_010_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_QG_010_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_010_FiniteObservability` | finite N: identity/measurement/info/distinguishability all work | ✅ |
| `Y_QG_010_InfiniteObservability` | infinite N: per-event info diverges; unobservable | ✅ |
| `Y_QG_010_InformationCapacity` | log₂(N_obs) per event; finite ⟹ N_obs finite | ✅ |
| `Y_QG_010_MeasurementResolution` | finite event resolves a finite outcome set | ✅ |
| `Y_QG_010_Run` | research report | ✅ |

**Conclusion:** Observability requires finite distinguishability. The finite
measurement event (M_001) has finite information capacity log₂(N_obs) (M_004), so it
can only resolve a finite outcome alphabet — the observable state space is finite.
This resolves QG_009 OP1: observability (not Difference) pins the finite observable
state space. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_010"`

---

## References

- ResearchY-QG_008 (finite distinguishability), QG_009 (infinite state space
  consistency).
- ResearchY-M_001 (measurement event), M_002 (measurement disturbance), M_004
  (measurement information), M_005 (information conservation), M_006 (observer role).
- ResearchY-D_037 (two-quadrature basis), D_039 (state identity origin).
- ResearchY-NP_018 (distinguishability observable), NP_021 (horizon bookkeeping).
- AT-QG: QG228 (information), QG234 (ΩΛ).
