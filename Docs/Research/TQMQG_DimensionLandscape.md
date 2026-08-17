# TQM-QG Phase 8 — Dimension Landscape

**Program:** TQM-QG (Unification)
**Phase:** 8 — what dimensions are physically viable?
**Status:** COMPLETED — 3/3 xUnit tests pass (27/27 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

d is supplied. Here we profile d=1..20 across eight native criteria and classify each dimension as
FORBIDDEN / ALLOWED / PREFERRED, producing a phase space of dimensions.

---

## 2. The Eight Criteria

1. **Einstein richness** — independent Einstein components (d+1)(d+2)/2.
2. **Graviton modes** — propagating polarizations d(d−3)/2.
3. **Curvature complexity** — Weyl components d(d+1)(d+2)(d−3)/12.
4. **Deficit gravity** — geodesic acceleration prefactor 1/d.
5. **Rotation curves** — flat value v² = |s|/d.
6. **Entropy production** — configurational entropy ln d + ln K.
7. **Information density** — complexity per active d.o.f. (d+1)(d+2)/2.
8. **Frozen metric fraction** — graviton/(graviton+1).

---

## 3. The Phase Space (d=1..20)

| d | richness | graviton | Weyl | gravity 1/d | rot v² | frozen | class |
|---|---|---|---|---|---|---|---|
| 1 | 3 | 0 | 0 | 1.000 | 1.000 | 0.000 | FORBIDDEN |
| 2 | 6 | 0 | 0 | 0.500 | 0.500 | 0.000 | FORBIDDEN |
| **3** | **10** | **0** | **0** | **0.333** | **0.333** | **0.000** | **PREFERRED** |
| **4** | **15** | **2** | **10** | **0.250** | **0.250** | **0.667** | **PREFERRED** |
| 5 | 21 | 5 | 35 | 0.200 | 0.200 | 0.833 | ALLOWED |
| 6 | 28 | 9 | 84 | 0.167 | 0.167 | 0.900 | ALLOWED |
| … | … | … | … | … | … | →1 | ALLOWED |
| 20 | 231 | 170 | 5×10⁴ | 0.050 | 0.050 | 0.994 | ALLOWED |

---

## 4. Classification

- **FORBIDDEN (d=1,2):** Einstein tensor identically zero — no gravity (and d=1 has no transverse directions).
- **PREFERRED (d=3):** first non-trivial gravity AND conformal-complete (Weyl=0, frozen fraction=0) — the
  unique dimension where TQM's conformally-flat scalar gravity is COMPLETE (freezes nothing out).
- **PREFERRED (d=4):** minimal PROPAGATING gravity (2 graviton polarizations, the fewest non-zero wave modes).
- **ALLOWED (d≥5):** gravity exists, but the conformal-flatness assumption freezes an ever-growing fraction of
  the metric (frozen fraction → 1), making them increasingly "inefficient".

| category | dimensions | count (d=1..20) |
|---|---|---|
| FORBIDDEN | 1, 2 | 2 |
| PREFERRED | 3, 4 | 2 |
| ALLOWED | 5..20 | 16 |

---

## 5. Conclusion

The dimension phase space has a **unique efficient point (d=3**, conformal-complete, nothing frozen) and a
**unique minimal-dynamical point (d=4**, fewest propagating graviton modes), with all d≥5 viable-but-inefficient
(frozen fraction → 1) and d≤2 forbidden. This consolidates the dimension arc (QG2–QG7): d is supplied, but the
native criteria carve out exactly two preferred dimensions — d=3 (complete scalar gravity) and d=4 (minimal
dynamical gravity) — consistent with the observed 3+1 spacetime being the *combination* of the conformal-complete
and minimal-propagating choices.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG80 `TQMQG80_PhaseSpaceTable` | PASS (2 FORBIDDEN, 2 PREFERRED, 16 ALLOWED) |
| TQMQG81 `TQMQG81_ViabilityCategories` | PASS (pathological/efficient/minimal/inefficient) |
| TQMQG82 `TQMQG82_LandscapeSummary` | PASS (landscape summary) |

Code: `TQM.Core/ResearchXH/DimensionLandscape.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase8_DimensionLandscapeTests.cs`.
