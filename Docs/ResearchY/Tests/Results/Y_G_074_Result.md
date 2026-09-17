# Y_G_074 - Result

**Audit:** ResearchY-G_074 - Clock Law Necessity Audit
**Verdict:** **BOUNDARY** - monotonicity drives the shape, the map drives the numbers
**Tests:** 6/6 PASSED

## The measured tables

| law | monotone | positive | rate x^1 | x^1 in 1+z | x^2 in 1+z |
|---|---|---|---|---|---|
| **rho^(1/d)** | yes | yes | 1.0 | -1.0 | **+0.5** |
| **ln(rho)** | yes | yes | 1.0 | -1.0 | **+1.0** |
| **exp(rho), linear map** | yes | yes | 1.0 | -1.0 | **+0.5** |
| **Pade [1/1]** | **no** | **no** | 1.0 | -1.0 | **-2.5** |

| requirement | survivors | which |
|---|---|---|
| positive and monotone | 4 of 4 | all |
| redshift is a ratio of rates | 4 of 4 | all |
| the rate's first order is 1 + x | 4 of 4 | all |
| **the recorded second order** | **2 of 4** | **rho^(1/d) and exp(rho) - the same law** |

**The reparametrisation identity: 0.000E+000** (exp(rho) with a linear map IS rho^(1/d) with a log map).

| law | 1 + z (compact object) | shift |
|---|---|---|
| rho^(1/d) | 1.280180559309 | - |
| exp(rho), linear map | 1.280180559309 | **2.220E-016** |
| ln(rho) | 1.328023241157 | **+4.784E-002** |
| GR | 1.405807032086 | - |

**The two survivors are the same law, confirmed dynamically** (2.220E-016 apart), and **the log law differs by
4.784E-002 - comparable to a third of the AT-vs-GR separation of 1.256E-001**, so the G_072 programme could resolve
the clock law's form while deciding AT vs GR.

## Notes - four defects in the audit's own first version

1. **The maps were not normalised to a common first order** (ln(rho) was given a slope-2 map), so the first table
   compared my parametrisations instead of the laws.
2. **The reparametrisation identity was mis-stated** - residual **1.054** where the truth is **0.000E+000**.
3. **The first-order requirement was tested on the redshift** (whose coefficient is -1.0) instead of on the rate, so
   every candidate appeared to fail.
4. **I asserted the pairwise monotonicity check cannot see a pole; the measurement refused it** - both checks catch
   the Pade pole at rho = 5/2, and the correction needed a correction.

**Group-G count guards bumped:** Y_G_033 43 -> 44 and Y_G_035 74 -> 75 with survives 60 -> 61 (registry census and the
MinimalTimeSector view).
