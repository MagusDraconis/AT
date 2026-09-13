# Y_E_016 - Result

**Audit:** ResearchY-E_016 - Sector Population Principle Audit
**Verdict:** **REFUTED**
**Tests:** 7/7 PASSED

## Answer

No existing AT quantity breaks the flat sector measure - and the detector was validated before it was trusted.

## Measurements

| quantity | value |
|---|---|
| trivial weight (control) / uniform potential (sanity) | 0.000E+000 / 0.000E+000 |
| **non-uniform local potential (breaker one)** | **1.6E-001** |
| **recipe-phase bridge (breaker two)** | **3.3E-001** |
| detector sensitive | True |
| conditional coarse phase spread | 0.000E+000 (forced flat by symmetry) |
| free room per sector at (4,6) | 4096, 4096, 4096, 4096 |
| multiplicity spread / factorisation residual | 0.000E+000 / 0.000E+000 |
| census of recipe-phase couplings | 0 |
| update spatial / time-like | 0.000E+000 / 1.424E-002 |
| sector-blindness residual (E_014) | 4.163E-017 |

## Candidates

- occupancy free room - **REFUTED** (recipe-side; factorises)
- multiplicity structure - **REFUTED** (sector-symmetric by E_015's bijection)
- D96 hierarchy - **REFUTED** (never reads the phase; census 0)
- compression laws - **REFUTED** (recipe-side; factorisation residual 0)
- actualization rate - **REFUTED** (purely electric update)

## Withdrawn prediction

The first draft expected a coarse phase observable's conditional distribution to differ across sectors. It does **not**:
the half-period symmetry forces the mean to L/2 in every class. The real nearest miss is a **weight**, not an
observable.

## Where a breaker would come from

A **potential for the link phase** or a **recipe-phase coupling** - both **new primitives**.
