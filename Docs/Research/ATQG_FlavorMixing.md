# AT-QG Phase 82 — Origin of Flavor Mixing

**Program:** AT-QG (Unification)
**Phase:** 82 — can CKM/PMNS mixing emerge from network family indices?
**Status:** COMPLETED — 3/3 xUnit tests pass (249/249 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether CKM and PMNS mixing can emerge from network family indices. Classify: DERIVED / COMPATIBLE / NEW SECTOR.

---

## 2. Family-index dynamics & link-state mixing (ATQG820)

Once the family index exists (QG81), off-diagonal couplings between indices are representable on the link —
family-index dynamics is the mechanism that hosts mixing.

---

## 3. Oscillations, rotations, CKM/PMNS (ATQG821)

Mixing is a unitary rotation between the flavor and mass bases; oscillations follow directly. CKM (4 real params:
3 angles + 1 CP phase) and PMNS (4 Dirac params + 2 Majorana phases) are representable on the family index, but
their specific entries are FREE inputs, not network outputs.

---

## 4. Classification (ATQG822)

**COMPATIBLE.**

- NOT DERIVED: the specific angles and CP phase are free empirical inputs;
- COMPATIBLE: mixing is a unitary rotation on the family index (off-diagonal link couplings); no new sector needed;
- NOT NEW SECTOR: no additional link content beyond the QG81 family index is required.

---

## 5. Conclusion

Flavor mixing is **COMPATIBLE** (representable) with the network, but not **DERIVED** from it.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG820 `ATQG820_FamilyIndexDynamics` | PASS (off-diagonal couplings representable) |
| ATQG821 `ATQG821_OscillationsAndRotations` | PASS (unitary rotation; entries free) |
| ATQG822 `ATQG822_Classification` | PASS (COMPATIBLE) |

Code: `AT.Core/ResearchXH/FlavorMixing.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase82_FlavorMixingTests.cs`.
