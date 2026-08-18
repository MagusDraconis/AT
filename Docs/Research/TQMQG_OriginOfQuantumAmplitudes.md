# TQM-QG Phase 62 — Origin of Quantum Amplitudes

**Program:** TQM-QG (Unification)
**Phase:** 62 — can complex amplitudes emerge from network structure?
**Status:** COMPLETED — 3/3 xUnit tests pass (189/189 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

QG61 showed the network reproduces gravity but not QM. Here we ask whether complex amplitudes emerge from the
network structure. Classify: COMPATIBLE / EMERGENT / REQUIRES NEW PRIMITIVE.

---

## 2. No native phase (TQMQG620)

The network's native content is scalar (nodes) + rank-2 (links) — **no phase**. The links CAN carry a U(1)
connection (lattice gauge theory, QG60), so a phase is **COMPATIBLE**, not native.

---

## 3. No emergence from loops (TQMQG621)

Without a U(1) phase on the links, a closed loop has **holonomy 1** (no interference phase). The loop structure
alone does not produce amplitudes — a phase must be added, so QM does **not** emerge natively.

---

## 4. Classification (TQMQG622)

**REQUIRES NEW PRIMITIVE.**

- COMPATIBLE: a U(1) phase fits on the links as a connection (like the gauge fields of QG60);
- NOT EMERGENT: the scalar/rank-2 content has no phase; closed loops are trivial without one;
- REQUIRES NEW PRIMITIVE: the complex amplitude (a U(1) phase) is a new degree of freedom.

---

## 5. Conclusion

Quantum mechanics requires a **new phase/amplitude primitive**, compatible with the network but not derivable from
it — exactly parallel to how ψ required a new spin-2 primitive (QG23). This completes the Standard Model/QM
compatibility arc (QG60–62): TQM's causal network natively gives gravity (spin-0 + spin-2); matter (fermions),
gauge phases, and quantum amplitudes are all compatible additions that require their own new primitives.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG620 `TQMQG620_NoNativePhase` | PASS (no native phase) |
| TQMQG621 `TQMQG621_NoEmergenceFromLoops` | PASS (trivial holonomy) |
| TQMQG622 `TQMQG622_Classification` | PASS (REQUIRES NEW PRIMITIVE) |

Code: `TQM.Core/ResearchXH/OriginOfQuantumAmplitudes.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase62_OriginOfQuantumAmplitudesTests.cs`.
