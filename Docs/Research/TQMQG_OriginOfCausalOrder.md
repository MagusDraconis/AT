# TQM-QG Phase 11 — Origin of Causal Order

**Program:** TQM-QG (Unification)
**Phase:** 11 — can causal order emerge from a more primitive actualization process?
**Status:** COMPLETED — 3/3 xUnit tests pass (36/36 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

The derivation begins with Q-events + causal order. Here we test whether the causal (partial) order itself
emerges from the actualization/generation process, via event relations, temporal ordering, branching
consistency, causal-set growth, and order-theoretic fixed points. Classify: DERIVED / PREFERRED /
REAL-UNDERIVED.

---

## 2. Results

### (a) The ancestor relation is a partial order (TQMQG110)

Model actualization as a deterministic branching tree. The ancestor relation (transitive closure of the
parent→child generation relation) is:

- **irreflexive** (no event is its own ancestor) ✅
- **antisymmetric** (no two distinct events are mutual ancestors) ✅
- **transitive** (ancestor-of-ancestor is ancestor) ✅

so it is a strict **partial order** — the causal order. No separate "order" primitive is needed.

### (b) Temporal ordering + branching consistency (TQMQG111)

The generation (layer) order is a **linear extension** of the partial order (ancestor ⟹ earlier generation) —
the temporal ordering. Branching is **consistent**: every non-root event has a unique parent in a strictly
earlier generation (acyclic).

### (c) Classification (TQMQG112)

**DERIVED** — causal order = transitive closure of the generation relation.

---

## 3. Classification: DERIVED

- The **full causal order is DERIVED**: it is the transitive closure of the parent→child generation relation,
  which is automatically a strict partial order (TQMQG110). Temporal ordering is the generation linear
  extension (TQMQG111).
- The remaining **REAL-UNDERIVED primitive is the generation relation itself** — "an event generates
  descendants" — i.e., the actualization dynamics (critical branching, TQM-QG1/QG7). This is more minimal than
  causal order: the full partial order is reconstructed from the single-step generation relation.
- This **replaces the primitive pair (Q-events + causal order) with (Q-events + generation relation)**.

---

## 4. Conclusion

Causal order is **not a separate primitive**: it is the order-theoretic content of the actualization generation
relation — the ancestor relation of the branching process, which is automatically a partial order. This reduces
the foundation's primitive list by one: instead of "causal order", the primitive is "an event generates
descendants" (the actualization dynamics), from which the partial order, temporal ordering, and (via the earlier
arc) geometry and gravity all follow. The deepest remaining primitive is therefore the **actualization dynamics
itself**.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG110 `TQMQG110_AncestorRelationIsPartialOrder` | PASS (ancestor relation = partial order) |
| TQMQG111 `TQMQG111_TemporalOrderingAndConsistency` | PASS (linear extension + consistency) |
| TQMQG112 `TQMQG112_Classification` | PASS (DERIVED) |

Code: `TQM.Core/ResearchXH/CausalOrder.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase11_OriginOfCausalOrderTests.cs`.
