# AT-QG Phase 85 — Origin of Standard Model Parameters

**Program:** AT-QG (Unification)
**Phase:** 85 — can masses, couplings, generations, and color count emerge from network information content?
**Status:** COMPLETED — 3/3 xUnit tests pass (258/258 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether the SM parameters can emerge from network information content. Classify: DERIVED / COMPATIBLE / POSTULATED.

---

## 2. Parameter counting & link capacity (ATQG850)

The SM has 19 free parameters (3 gauge + 2 Higgs + 9 masses + 4 CKM + 1 θ); +7 if neutrinos are massive. The link's
capacity is ample, but capacity only PERMITS the parameters — it does not fix their values.

---

## 3. Symmetry, family index, mass hierarchies (ATQG851)

Gauge/Lorentz symmetries fix the FORM but not the VALUES. The family count (3) is free, and the mass hierarchy
(up vs top quark) is empirical, not derived.

---

## 4. Classification (ATQG852)

**POSTULATED.**

- NOT DERIVED: the 19 parameter values are not network outputs;
- COMPATIBLE (subordinate): the link has the capacity to host them;
- POSTULATED: masses, couplings, generation count, and color count are FREE empirical inputs.

---

## 5. Conclusion

The SM parameters are **POSTULATED** — compatible, but not derivable.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG850 `ATQG850_ParameterCounting` | PASS (19 params; capacity ≠ determination) |
| ATQG851 `ATQG851_SymmetryAndHierarchies` | PASS (form not values; hierarchy free) |
| ATQG852 `ATQG852_Classification` | PASS (POSTULATED) |

Code: `AT.Core/ResearchXH/SMParameters.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase85_SMParametersTests.cs`.
