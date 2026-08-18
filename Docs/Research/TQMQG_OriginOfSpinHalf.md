# TQM-QG Phase 66 — Origin of Spin-1/2

**Program:** TQM-QG (Unification)
**Phase:** 66 — can fermionic spin-1/2 emerge from network structure?
**Status:** COMPLETED — 3/3 xUnit tests pass (201/201 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

The network natively hosts spin-0 (ρ), spin-2 (ψ), and U(1) (phase). Determine whether spin-1/2 (fermions) emerges.
Classify: DERIVED / COMPATIBLE / REQUIRES NEW PRIMITIVE.

---

## 2. Integer vs half-integer (TQMQG660)

The network's native content is **integer spin** (tensors): 0 (nodes), 2 (links), 1 (gauge). Fermions are
**half-integer spin-1/2 spinors** — a fundamentally different representation (SU(2), double cover). A link
orientation gives only a Z2 sign, not a spinor.

---

## 3. Double cover required (TQMQG661)

A spinor is a section of a spin bundle (a double cover of the frame bundle). It is **not derivable** from scalar
nodes + rank-2 links — a spin structure is new data on the network.

---

## 4. Classification (TQMQG662)

**REQUIRES NEW PRIMITIVE.**

- NOT DERIVED: half-integer spin cannot emerge from integer-spin content;
- COMPATIBLE: a spin structure (double cover) can be added to the network;
- REQUIRES NEW PRIMITIVE: the spinor/double-cover is a new degree of freedom.

---

## 5. Conclusion

Fermions require a **new spin-1/2 (spinor) primitive**, compatible with the network (via a spin structure) but not
derivable from it. This completes the matter picture: the network hosts gravity (spin-0 + spin-2) and gauge
(spin-1), but fermions (spin-1/2) are a genuinely new primitive.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG660 `TQMQG660_IntegerVsHalfInteger` | PASS (spinor ≠ tensor) |
| TQMQG661 `TQMQG661_DoubleCoverRequired` | PASS (spin structure) |
| TQMQG662 `TQMQG662_Classification` | PASS (REQUIRES NEW PRIMITIVE) |

Code: `TQM.Core/ResearchXH/OriginOfSpinHalf.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase66_OriginOfSpinHalfTests.cs`.
