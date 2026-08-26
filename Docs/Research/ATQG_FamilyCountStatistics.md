# AT-QG Phase 108 — Family Count Statistics

**Program:** AT-QG (Unification)
**Phase:** 108 — what family counts are statistically preferred in causal networks?
**Status:** COMPLETED — 3/3 xUnit tests pass (327/327 AT-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational audit of the native operator spectrum)

---

## 1. Goal

QG107 found robust octave-band spectral mode families. This phase computes the STATISTICAL distribution of
family counts over a large deterministic ensemble of causal graphs and asks whether any count — in particular
N = 3 (the SM generation count, QG80/81) — is preferred. Classify: NO PREFERENCE / WEAK PREFERENCE / STRONG
PREFERENCE.

---

## 2. Ensemble & family-count distribution (ATQG1080)

Large deterministic ensemble (77 causal graphs): 60 Erdős–Rényi random graphs (4 sizes × 5 densities × 3 fixed
seeds), 8 causal-set grids, 6 2D ε-threshold graphs, 3 perturbed grids.

| family count | networks | fraction |
|---|---|---|
| 0 | 0 | 0.0% |
| 1 | 22 | 28.6% |
| 2 | 17 | 22.1% |
| 3 | 20 | 26.0% |
| 4 | 16 | 20.8% |
| 5 | 2 | 2.6% |

Modal count = 1 (28.6%), mean = 2.47. The distribution is BROAD (1–5 octave families): the count is
size/density dependent, not a single fixed value.

---

## 3. Hierarchy span & size scaling (ATQG1081)

- Hierarchy span (ω_max/ω_min): min 1.34, median 3.85, max 20.43, mean 5.47.
- Across the WHOLE mixed ensemble the family count is DENSITY-dominated: correlation with ln N = 0.06.
- WITHIN the causal-grid class the count grows with size: correlation with ln N = 0.69 (counts 4,4,3,4,5,4,4,5
  at sizes 55,91,81,91,200,187,195,500) — family count ≈ ½log₂N from span ≈ N^(1/d).

Size scaling is a real, class-specific trend within the causal class, not a universal law across densities.

---

## 4. Preference for N=3 (ATQG1082)

- Modal family count: 1 (28.6% of networks).
- Fraction with N = 3: 26.0%; with N = 4: 20.8%; with N ≥ 4: 23.4%.
- N=3 is common (26%) but NOT the dominant mode, and the count shifts with size/density.

---

## 5. Classification (ATQG1082)

**WEAK PREFERENCE.**

- NOT NO PREFERENCE: N=3 families is common (26%, above the 15% threshold) — a real statistical presence.
- NOT STRONG PREFERENCE: 3 is not the modal count (1 is), and N=3 networks are only 26% (well below 40%).
- WEAK PREFERENCE: N=3 is a size/density-window phenomenon — common among mid-density causal networks, not a
  dominant universal count.

---

## 6. Conclusion

The family count in causal networks is broadly distributed (1–5), density-dominated across the mixed ensemble,
and size-growing within the causal-grid class. The SM's N=3 generation count has a WEAK statistical preference
in the ensemble — it is a common mid-density value but not the dominant one, consistent with QG80/81 (the
generation count is not derived by the network).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1080 `ATQG1080_EnsembleAndDistribution` | PASS (77 graphs, broad 1–5 distribution) |
| ATQG1081 `ATQG1081_HierarchySpanAndSizeScaling` | PASS (grid-class r=0.69 with ln N) |
| ATQG1082 `ATQG1082_ThreePreferenceAndClassification` | PASS (WEAK PREFERENCE) |

Code: `AT.Core/ResearchXH/FamilyCountStatistics.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase108_FamilyCountStatisticsTests.cs`.
