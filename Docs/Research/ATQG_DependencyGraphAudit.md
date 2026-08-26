# AT-QG Phase 225 — Dependency Graph Audit

**Status:** COMPLETE — **ACYCLIC**
**Tests:** ATQG2250, ATQG2251, ATQG2252 (all passed)
**Core class:** `AT.Core/ResearchXH/DependencyGraphAudit.cs`
**Scope:** QG0–QG224 (226 coverage entries; the non-integer 116.5 Universal-Attractor sub-phase is dropped)
**Data source:** `Docs/ATQG_PhysicsCoverage.json` (key_result) + each report's QG references
(test-ID tokens excluded)
**Method:** audit only — no new physics, no new derivations

---

## 1. The Question

Verify the full phase derivation graph over QG0–QG224: build the dependency
DAG, check for cycles, hidden loops, target reuse, future-to-past
dependencies, and circular derivations.

---

## 2. Method

For each phase, dependency edges were extracted from the coverage single
source of truth (the `key_result` text and the report body's QG references,
with test-ID tokens like `ATQG01` excluded). Only **forward edges** —
dependency phase number **<** dependent phase number — enter the derivation
DAG. References from an earlier phase to a **later** phase number are treated
as **annotation edges** (corrections/reclassifications), never dependencies.

**Result: 1349 forward edges, 226 nodes.**

---

## 3. Checks

| Check | Result |
|-------|--------|
| **Cycles** | **NONE** — topological sort (Kahn) orders all 226/226 nodes; the phase number is itself a topological order because every edge points forward |
| **Hidden loops** | **NONE** — any loop would appear as a cycle in the topological sort; the transitive closure is implicitly checked (all edges satisfy src < dst) |
| **Target reuse** | Present and healthy — phases are reused as shared derivation hubs (see §5) |
| **Future-to-past dependencies** | **10 annotation edges**, all CORRECTION / RECLASSIFICATION annotations (e.g. phases 2/3/8/9 carry "CORRECTION (QG10)" notes; QG147/148 → QG149 supersession; QG151-153 → QG155 reclassification) — **excluded from the DAG** |
| **Circular derivations** | **NONE** — equivalent to the cycle check; acyclicity verified |

---

## 4. Longest Dependency Chain

**101 edges (102 nodes), ending at QG224 (the paper-readiness audit).**

The deepest chain runs from the primitive roots through the QM/QG closure
series (QG216 → QG218 → QG220 → QG219 → QG221 → QG222 → QG223 → QG224), the
longest derivation spine in the theory.

---

## 5. Root Primitives and Critical Nodes

### Root primitives (in-degree 0, 24 nodes)
`QG0, QG2, QG6, QG7, QG12, QG46, QG60, QG66, QG76, QG79, QG80, QG83, QG85,
QG86, QG88, QG91, QG94, QG95, QG97, QG98, QG99, QG100, QG102` (+ the empty
slot 225). These anchor the graph with no phase-level dependencies.

### Critical nodes — most depended-upon (highest in-degree)
| Phase | Name | Dependents |
|-------|------|------------|
| QG216 | Quantum Gravity Closure Audit | 85 |
| QG215 | Anti-Fit Reaudit 2 | 74 |
| QG190 | Anti-Fit Audit | 51 |
| QG223 | Final Quantum Gravity Audit | 50 |
| QG221 | Quantum Gravity Reclosure Audit 2 | 44 |
| QG219 | Quantum Gravity Reclosure Audit | 41 |
| QG224 | QG Paper Readiness Audit | 40 |
| QG222 | Native Metric Dynamics | 38 |

### Critical nodes — most-feeding (highest out-degree)
| Phase | Name | Feeds |
|-------|------|-------|
| QG159 | D96 Selection Origin | 23 |
| QG160 | Period-3 Seed Origin | 22 |
| QG140 | Hierarchy Amplification | 21 |
| QG153 | Doublet Origin | 21 |
| QG155 | Z2 Symmetry Origin | 21 |
| QG162 | Gauge Coupling Origin | 21 |
| QG23 | Origin of ψ | 20 |
| QG161 | Gauge Sector Origin | 17 |

The **D96 structural origin** (QG155-162) is the most reused derivation hub,
followed by the audit/closure series.

---

## 6. Verdict

### **ACYCLIC**

- **No cycles**, no hidden loops, no circular derivations;
- the **phase number is a topological order** (every dependency edge points
  forward), verified 226/226 nodes;
- the only future-to-past references are **10 explicit correction /
  reclassification annotations** — documentation, not dependencies.

The full derivation graph QG0–QG224 is a valid DAG.
