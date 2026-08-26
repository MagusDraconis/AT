# AT-QG Phase 71 — Origin of the Entangling Sector

**Program:** AT-QG (Unification)
**Phase:** 71 — what minimal additional link content produces non-separable correlations?
**Status:** COMPLETED — 3/3 xUnit tests pass (216/216 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

QG70 showed θ gives interference but θ + S do not give Bell entanglement. Find the minimal additional link content.
Classify: DERIVED / COMPATIBLE / NEW SECTOR.

---

## 2. Phase ≠ non-separability (ATQG710)

e^(iθ) is a SINGLE-degree-of-freedom amplitude — it gives interference (QG65) but is separable. Non-separability
requires a JOINT state across TWO degrees of freedom, which a single phase cannot supply.

---

## 3. The joint link state (ATQG711)

The link (connecting exactly two nodes) is the natural home for a JOINT state — e.g. a Bell pair (|00⟩+|11⟩)/√2.
This joint state is the minimal additional content: COMPATIBLE with the link, but new.

---

## 4. Classification (ATQG712)

**NEW SECTOR.**

- NOT DERIVED: the joint non-separable state is not derivable from θ or S;
- COMPATIBLE: the link is the natural home for a pair state;
- NEW SECTOR: the entangling (joint link state) sector is new content beyond θ + S.

---

## 5. Conclusion

The minimal entangling content is a **JOINT LINK STATE** — a new sector, compatible with the link, that produces
Bell-type non-separability. This completes the quantum picture: the network needs θ (phase/superposition), S (spin),
and now the entangling sector (joint link states) for full quantum mechanics.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG710 `ATQG710_PhaseVsNonSeparability` | PASS (separable) |
| ATQG711 `ATQG711_JointLinkState` | PASS (joint state) |
| ATQG712 `ATQG712_Classification` | PASS (NEW SECTOR) |

Code: `AT.Core/ResearchXH/EntanglingSector.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase71_EntanglingSectorTests.cs`.
