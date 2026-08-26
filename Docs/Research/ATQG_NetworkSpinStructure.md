# AT-QG Phase 67 — Network Spin Structure

**Program:** AT-QG (Unification)
**Phase:** 67 — can a causal network naturally carry a spin structure?
**Status:** COMPLETED — 3/3 xUnit tests pass (204/204 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

QG66 showed spin-1/2 requires a spin structure. Here we ask whether the causal network can naturally carry one.
Classify: DERIVED / COMPATIBLE / REQUIRES NEW PRIMITIVE.

---

## 2. Orientation ≠ spin structure (ATQG670)

A graph orientation assigns a direction (Z2) to each link; a spin structure is a **double cover** with a consistent
sign on each cycle — a richer (SU(2)/Z2^cycles) object. Orientation alone is insufficient.

---

## 3. Compatible but not naturally present (ATQG671)

The network naturally has orientation (Z2) and a U(1) phase, but **not** the double-cover/SU(2) data. A spin
structure can be added (compatible), but it is new data, not derivable from the network.

---

## 4. Classification (ATQG672)

**REQUIRES NEW PRIMITIVE.**

- NOT DERIVED: the double cover / SU(2) is not derivable from scalar + rank-2 + U(1) content;
- COMPATIBLE: a spin structure can be added;
- REQUIRES NEW PRIMITIVE: the spin structure (SU(2) connection) is new data.

---

## 5. Conclusion

The causal network **can carry** a spin structure (compatible), but it is not naturally present — confirming QG66:
fermions require a new spin-1/2 (spin structure) primitive.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG670 `ATQG670_OrientationVsSpinStructure` | PASS (Z2 ≠ double cover) |
| ATQG671 `ATQG671_CompatibleButNotPresent` | PASS (compatible, new data) |
| ATQG672 `ATQG672_Classification` | PASS (REQUIRES NEW PRIMITIVE) |

Code: `AT.Core/ResearchXH/NetworkSpinStructure.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase67_NetworkSpinStructureTests.cs`.
