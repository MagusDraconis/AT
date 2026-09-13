# Y_E_012 - Result

**Audit:** ResearchY-E_012 - Flux Excitation Audit
**Verdict:** **BOUNDARY**
**Tests:** 6/6 PASSED

## Answer

Nothing inside AT populates a non-trivial flux sector. The population is an **assignment**, it is **globally
constrained**, and the smallest non-trivial assignment is a **balanced pair** whose amplitude does not scale away.

## Measurements

| quantity | value |
|---|---|
| whole-torus product of reduced holonomies, distance from identity | 0.00E+000 |
| slice product with one half-turn, distance from identity | 2.000000 |
| single half-turn flux, constraint residual | 3.141593 -> FORBIDDEN |
| balanced pair, constraint residual | 0.000E+000 -> ALLOWED |
| occupancy route max \|F\| at L = 8 / 16 / 32 / 64 | 1.691E-002 / 4.442E-003 / 1.124E-003 / 2.820E-004 |
| occupancy route fitted exponent | a^2.00 |
| balanced pair max \|F\| at every size | 3.141593 = pi |

## Candidates

- occupancy defects - **REFUTED** (scaling)
- topological defects - **BOUNDARY** (legitimate pair, no creator)
- winding sectors - **REFUTED** (gradient: zero curvature E_008, whole-turn holonomy E_011)
- boundary conditions - **BOUNDARY** (the answer)
- actualization transitions - **REFUTED** (time-like component only, E_009)

## Note

A first-draft claim was withdrawn: the product of holonomies around a **slice** is the cycle's holonomy, not the
identity. The constraint that forbids the single fluxon is the **whole-torus** sum.
