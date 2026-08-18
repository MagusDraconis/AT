# TQM-QG Phase 85 — Origin of Standard Model Parameters

**Program:** TQM-QG (Unification)
**Phase:** 85 — can masses, couplings, generations, and color count emerge from network information content?
**Status:** COMPLETED — 3/3 xUnit tests pass (258/258 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether the SM parameters can emerge from network information content. Classify: DERIVED / COMPATIBLE / POSTULATED.

---

## 2. Parameter counting & link capacity (TQMQG850)

The SM has 19 free parameters (3 gauge + 2 Higgs + 9 masses + 4 CKM + 1 θ); +7 if neutrinos are massive. The link's
capacity is ample, but capacity only PERMITS the parameters — it does not fix their values.

---

## 3. Symmetry, family index, mass hierarchies (TQMQG851)

Gauge/Lorentz symmetries fix the FORM but not the VALUES. The family count (3) is free, and the mass hierarchy
(up vs top quark) is empirical, not derived.

---

## 4. Classification (TQMQG852)

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
| TQMQG850 `TQMQG850_ParameterCounting` | PASS (19 params; capacity ≠ determination) |
| TQMQG851 `TQMQG851_SymmetryAndHierarchies` | PASS (form not values; hierarchy free) |
| TQMQG852 `TQMQG852_Classification` | PASS (POSTULATED) |

Code: `TQM.Core/ResearchXH/SMParameters.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase85_SMParametersTests.cs`.
