# TQM-QG Phase 90 — Origin of Gauge Sector Splitting

**Program:** TQM-QG (Unification)
**Phase:** 90 — why does the link decompose into three gauge sectors instead of one unified structure?
**Status:** COMPLETED — 3/3 xUnit tests pass (273/273 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine why the link decomposes into three gauge sectors (U(1), SU(2), SU(3)) instead of one unified gauge structure. Classify: DERIVED / PARTIAL / POSTULATED.

---

## 2. Representation hierarchy & minimal link info (TQMQG900)

θ (charge), S (spin), C (color) act on DIFFERENT internal spaces, so the gauge group is the PRODUCT
U(1)×SU(2)×SU(3). They share one carrier (the single link, QG68), but that structural unity does not force a
single gauge group.

---

## 3. Symmetry breaking, relations, unified candidates (TQMQG901)

No symmetry-breaking chain or relation derives a unified group. A GUT (SU(5)/SO(10)) is an ADDITIONAL postulate.
Neither splitting nor unification is derived — the product structure is empirical.

---

## 4. Classification (TQMQG902)

**POSTULATED.**

- NOT DERIVED: neither the split nor a unified group is an output of (V,E);
- NOT PARTIAL: no partial mechanism relates U(1)/SU(2)/SU(3) — distinct spaces;
- POSTULATED: the three gauge sectors are independent postulates; a GUT would be an additional postulate.

---

## 5. Conclusion

The gauge-sector splitting is **POSTULATED** (each sector a free input).

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG900 `TQMQG900_HierarchyAndMinimalLink` | PASS (product structure) |
| TQMQG901 `TQMQG901_SymmetryAndUnification` | PASS (no native unification) |
| TQMQG902 `TQMQG902_Classification` | PASS (POSTULATED) |

Code: `TQM.Core/ResearchXH/GaugeSectorSplitting.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase90_GaugeSectorSplittingTests.cs`.
