# Y_G_070 - Result

**Audit:** ResearchY-G_070 - Observational Decision Audit
**Verdict:** **DERIVED**
**Tests:** 7/7 PASSED

## Answer

**Measure the surface redshift of J0740+6620 to 7.55 % (5σ) or 14.14 % (3σ)** with its published NICER mass and
radius: that single measurement decides the question at **6.4σ**. On current determinations that is a **2.6× to 6.6×**
improvement.

## The targets and what they can decide

| target | x | σ_x/x | separation | 20 % timing | 5 % timing | perfect timing |
|---|---|---|---|---|---|---|
| **J0740+6620 (Riley)** | −0.247001 | 3.661 % | **1.256E-001** | 2.18σ | **6.45σ** | 9.27σ |
| J0740+6620 (Miller) | −0.224245 | 3.477 % | 9.518E-002 | 1.86σ | 6.09σ | 10.26σ |
| J0030+0451 (Riley) | −0.152011 | **2.636 %** | 3.450E-002 | 1.05σ | 4.06σ | **15.43σ** |
| J0030+0451 (Miller) | −0.163355 | 2.790 % | 4.125E-002 | 1.16σ | 4.42σ | 14.31σ |

## The required precision

| target | 1σ | 3σ | 5σ |
|---|---|---|---|
| **J0740+6620**, redshift at the published compactness | **44.58 %** | **14.14 %** | **7.55 %** |
| **J0740+6620**, compactness at 5 % timing | 33.74 % | 10.66 % | 5.64 % |
| J0030+0451, redshift at the published compactness | 20.97 % | 6.87 % | 3.98 % |
| J0030+0451, compactness at 5 % timing | 39.50 % | 9.50 % | **NaN** (budget breached) |

| measurement | value |
|---|---|
| budget shares at J0740+6620, 5σ | timing **31.1 %**, compactness **29.1 %** |
| budget shares at J0030+0451, 5σ | timing **141.5 %** (breached), compactness 10.5 % |
| slack at J0740+6620, 5σ | **2.6×** better than 20 %, **6.6×** better than 50 % |
| the target-flip timing precision | **2.051 %** (both targets then give 8.54σ) |

## Notes

**A draft claim is withdrawn.** The draft assumed the most compact target wins on both counts; the measurement refused it.
The **largest separation** belongs to J0740+6620 and the **best-constrained compactness** to J0030+0451, so the ranking
depends on which error dominates: **J0740+6620 wins at 20 % and 5 % timing**, **J0030+0451 wins with perfect timing**, and
they **swap at 2.051 %**. A first version of the crossing also had its bisection **direction inverted**, which is why the
audit computes the crossing rather than asserting a ranking.

**The compactness is recomputed, not imported:** `x = −1.4770 M/M☉ / (R/km)` reproduces the compactness the earlier audits
recorded for all four published targets.
