# TQM-QG Phase 29 — Physical Meaning of Q-Events

**Program:** TQM-QG (Unification)
**Phase:** 29 — what is a Q-event physically?
**Status:** COMPLETED — 3/3 xUnit tests pass (90/90 TQM-QG)
**Constraint:** no new primitives (this is an interpretive audit of an existing primitive)

---

## 1. Goal

The foundation reduces to two primitives: **Q-events** and the **generation relation** (which gives causal order,
DERIVED per QG11). Here we fix the minimal physical meaning of a Q-event. Classify: REAL-UNDERIVED / EMERGENT /
NETWORK TRANSITION.

---

## 2. Criteria for a valid physical picture (TQMQG290)

A picture of "what a Q-event is" must satisfy four criteria:

1. **actualization content** — events must "happen" (be generated/updated);
2. **counting compatibility** — must yield the counting measure ρ;
3. **causal-order compatibility** — must support the generation relation → causal order;
4. **primitive status** — must not require a deeper substrate (else Q-events would be emergent).

| picture | score | kind |
|---|---|---|
| temporal-lattice (TRM) | 4/4 | network transition |
| clock-network | 4/4 | network transition |
| time-state-change | 4/4 | network transition |
| network-update | 4/4 | network transition |
| primitive-point (bare) | 1/4 | insufficient |

The bare "primitive point" fails actualization content: a static point cannot "happen", so it cannot generate ρ or
support the generation relation.

---

## 3. Determination (TQMQG291)

- **EMERGENT? No** — Q-events are a primitive (the actualization substrate), not a product of one.
- **REAL-UNDERIVED? Yes** — not reducible to anything deeper within TQM.
- **ρ counts Q-events** — one Q-event = one counted unit of the counting measure ρ.

Therefore a Q-event is **REAL-UNDERIVED** and its minimal content is a **NETWORK TRANSITION** (not a bare point,
not emergent).

---

## 4. Minimal meaning (TQMQG292)

**A Q-event is a REAL-UNDERIVED NETWORK TRANSITION: one local time-state change (a "tick" of actualization).**

- the **generation relation** = the network's update rule (→ causal order);
- the **counting measure ρ** = the density of these updates (Q-event = one counted unit);
- **actualization** = the network updating (local time advancing by one tick).

---

## 5. Conclusion

The minimal physical meaning of a Q-event is a **local time-state change in a temporal network** — not a bare
spacetime point. This is a genuine *primitive* (REAL-UNDERIVED): the theory does not derive Q-events from anything
deeper, it starts from the act of actualization itself. The four transition pictures (TRM temporal lattice, clock
network, time-state change, network update) are equivalent minimal descriptions; the "primitive point" reading is
rejected as under-specified because it lacks actualization content.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG290 `TQMQG290_CriteriaScoring` | PASS (4 transition pictures 4/4; bare point 1/4) |
| TQMQG291 `TQMQG291_Determination` | PASS (REAL-UNDERIVED, not emergent) |
| TQMQG292 `TQMQG292_MinimalMeaning` | PASS (network transition) |

Code: `TQM.Core/ResearchXH/PhysicalMeaningOfQEvents.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase29_PhysicalMeaningOfQEventsTests.cs`.
