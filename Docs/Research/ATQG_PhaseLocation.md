# AT-QG Phase 63 — Physical Location of the Quantum Phase

**Program:** AT-QG (Unification)
**Phase:** 63 — where can a U(1) phase live in the network?
**Status:** COMPLETED — 3/3 xUnit tests pass (192/192 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

QG62 showed amplitudes require a new U(1) phase. Here we ask WHERE that phase lives. Classify: NODES / LINKS /
LOOPS / new object.

---

## 2. The three homes (ATQG630)

| location | content |
|---|---|
| NODES | matter wavefunction phases |
| LINKS | gauge connection phases |
| LOOPS | holonomies (derived, gauge-invariant) |

No new object is needed.

---

## 3. Links are canonical, loops derived (ATQG631)

In lattice gauge theory: the gauge connection A_ij = e^(iθ_ij) is a **link (edge) variable**; the Wilson loop is
the **product of link phases** around a closed loop — derived, gauge-invariant, and the physical observable
(interference / Aharonov–Bohm phase). Matter phases sit on the nodes.

---

## 4. Classification (ATQG632)

**LINKS.**

- LINKS (canonical): the U(1) gauge connection lives on the links (consistent with QG60);
- NODES (matter): wavefunction phases live on the nodes;
- LOOPS (derived): holonomies are gauge-invariant observables derived from link phases;
- NO new object: the existing network (nodes + links) suffices.

---

## 5. Conclusion

The quantum phase lives **naturally on the LINKS** (as a gauge connection), with matter phases on the nodes and
loop holonomies as the derived, gauge-invariant observables. No new network object is required — the phase is a new
degree of freedom that slots into the existing node/link structure.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG630 `ATQG630_ThreeHomes` | PASS (nodes/links/loops) |
| ATQG631 `ATQG631_LinksCanonicalLoopsDerived` | PASS (link variable) |
| ATQG632 `ATQG632_Classification` | PASS (LINKS) |

Code: `AT.Core/ResearchXH/PhaseLocation.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase63_PhaseLocationTests.cs`.
