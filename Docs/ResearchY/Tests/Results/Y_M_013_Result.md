# Y_M_013_Result.md — ResearchY-M_013 Axis-Count Selection Audit

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_013_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_013"`

---

## Summary

**Question:** Why does Actualization select exactly 3 coupled D96 axes? Is 3 unique or
merely sufficient?

**Verdict:** The exact axis count 3 is **NOT uniquely forced by Actualization dynamics —
it is MERELY SUFFICIENT as a hosted value** (any d gives a consistent dD tensor with
DOS p = d; observed space is 3D, CORRESPONDENCE — NP_036/037 unchanged). It is
**UNIQUE in exactly one derived structural sense**: rotation self-duality
d(d−1)/2 = d ⇒ d = 3 (so(3) ≅ ℝ³), the condition that rotations form a vector of the
same dimension as the space. No internal quantity (symmetry order, information, family
count, distinct-level compression, DOS) singles out 3.

## Key verified facts

| Item | Verified result |
|---|---|
| point groups B_d | orders 2^d·d! = 2 (Z₂), 8 (D₄), 48 (O_h), 384 (B₄) — no distinguished member |
| irreducible content | D₄ max dim 2 (no 3D irrep); O_h has genuine 3D irreps {1,1,2,3,3,…}; defining rep of B_d irreducible, dimension = d |
| rotation self-duality | d(d−1)/2 = d has unique solution d = 3 ⇒ so(3) ≅ ℝ³ (angular momentum a vector) |
| information | d·log₂95 bits = 6.57, 13.14, 19.71, 26.28 — strictly additive, no extremum |
| family structure | family count 3 is a single-ring span-window [4,8) quantity (D_020), decoupled from axis count (NP_037); family-3 ≠ axis-3 |
| spectral efficiency | DOS p = d identity (lattice-ball exponents 0.97, 1.95, 2.92 for d=1,2,3); distinct-level ratio monotone 0.46 → 0.11 → 0.026 → 0.0042 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_M_013_PointGroups` | B_d orders 2^d·d! = 2, 8, 48, 384 | ✅ |
| `Y_M_013_IrreducibleContent` | D₄ max 2; O_h has genuine 3D irrep; defining rep dim d | ✅ |
| `Y_M_013_RotationSelfDuality` | d(d−1)/2 = d ⇒ unique d = 3 | ✅ |
| `Y_M_013_InformationAdditive` | info = d·log₂95 linear, no extremum | ✅ |
| `Y_M_013_FamilyDecoupled` | family 3 ring-level, independent of axis count | ✅ |
| `Y_M_013_SpectralEfficiency` | DOS p = d; distinct-level ratio monotone | ✅ |
| `Y_M_013_Classification` | verdict table | ✅ |
| `Y_M_013_Run` | research report | ✅ |

## Conclusion

Actualization does not dynamically force exactly 3 axes: every spectral/information/family
criterion is d-generic or monotone, and any d ≥ 1 hosts a consistent dD Weyl tensor (p = d).
The value 3 is HOSTED by the observed 3D space (CORRESPONDENCE, NP_036/037: only d ≥ 3 is
load-bearing via the (d−2) Einstein factor, QG197). What is genuinely unique about 3 is the
derived identity d(d−1)/2 = d — only in three axes do the rotation generators form a vector
of the same dimension as the space (so(3) ≅ ℝ³), the group-theoretic basis of vectorial
rotations and of the genuine 3D p-wave sector M_012 found EMERGENT at the 3-axis (O_h)
network. No reclassification of prior audits; no new primitive; canonical AT unchanged.
