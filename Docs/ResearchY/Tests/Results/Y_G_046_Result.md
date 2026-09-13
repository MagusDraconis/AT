# Y_G_046 - Result

**Audit:** ResearchY-G_046 - Rho Accessibility Audit
**Verdict:** **OBSERVABLE**
**Tests:** 7/7 PASSED

## Answer

A validated hidden step changes **no contraction** and still moves the **clock** (3.764E-003), **acceleration**
(3.010E-003) and **field-strength** (6.327E-004) multisets. Only the **flux sector** is untouched (0.000E+000; census
0).

## Measurements

| quantity | value |
|---|---|
| state / invariant / hidden dimensions | 95 / 48 / 47 |
| orbit span of the symmetry | 84 (so the orbit is **not** the hidden set) |
| contraction change along the hidden step | 5.116E-013 |
| clock / acceleration / field multiset change | 3.764E-003 / 3.010E-003 / 6.327E-004 |
| flux sector change | 0.000E+000 |
| symmetry-move multisets (clocks/accel/fields) | 0.000E+000 / 0.000E+000 / 5.140E-016 |
| controls: addressed pattern / phase moves label | 4.937E-001 / 1.000000 |
| kernel at the audited state vs G_040's ceiling | 53 vs 47 (retained 43 vs 48) |

## Withdrawn identification

The hidden directions are **not** the symmetry orbit (span 84 ≠ 47). A symmetry move is gauge-like because it
**relabels**; the hidden set is the **kernel of the contractions**.

## Bug caught by the audit

The first hidden step **renormalised** the perturbed state, moving it along observable directions too and making a
"hidden" step change a contraction by **2.061**. The **sum-neutral projection** is what makes the step honest.

## Targets

- clocks - **OBSERVABLE**
- acceleration - **OBSERVABLE**
- field strengths - **OBSERVABLE**
- flux sectors - **HIDDEN**
