# AT-QG Phase 86 — Parameter Origin Audit

**Program:** AT-QG (Unification)
**Phase:** 86 — is there any mechanism within the network that can constrain free SM parameters?
**Status:** COMPLETED — 3/3 xUnit tests pass (261/261 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether any network mechanism can constrain the free Standard Model parameters. Classify: CONSTRAINED / PARTIAL / FULLY FREE.

---

## 2. Information capacity & symmetry constraints (ATQG860)

Capacity only permits values; symmetry fixes which terms EXIST (form) but not their magnitudes. Neither pins down
the values.

---

## 3. Entropy, parameter counting, minimal description (ATQG861)

The COUNT (19) is structurally fixed (gauge dims + reps + family index), and symmetry fixes the FORM. But
entropy/minimal-description selection is NOT native — it would be an additional postulate. So the network
constrains count + form, not values.

---

## 4. Classification (ATQG862)

**PARTIAL.**

- NOT CONSTRAINED: the values are not determined;
- NOT FULLY FREE: the network does constrain the count (19) and the form (symmetry);
- PARTIAL: count + form are constrained; values remain free.

---

## 5. Conclusion

The network **PARTIALLY** constrains the SM parameters (count + form), while the values stay free.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG860 `ATQG860_CapacityAndSymmetry` | PASS (form yes, values no) |
| ATQG861 `ATQG861_EntropyCountingDescription` | PASS (count constrained, values free) |
| ATQG862 `ATQG862_Classification` | PASS (PARTIAL) |

Code: `AT.Core/ResearchXH/ParameterOriginAudit.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase86_ParameterOriginAuditTests.cs`.
