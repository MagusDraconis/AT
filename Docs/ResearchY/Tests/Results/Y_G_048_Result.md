# Y_G_048 - Result

**Audit:** ResearchY-G_048 - Clock Completeness Audit
**Verdict:** **MAXIMAL**
**Tests:** 7/7 PASSED

## Answer

The clock pattern resolves the **whole** state (95 of 95, information retained 1.000) and is **invertible**
(ρ = rate^d, residual **4.441E-016**) - but it is **maximal and tied**: the acceleration and field-strength patterns
are complete too, and the contraction observables (43) are the only reading that loses information.

## Measurements

| reading | observable dim | information retained | kernel rank |
|---|---|---|---|
| clock pattern | **95** | **1.000** | **53** |
| acceleration pattern | **95** | **1.000** | **53** |
| field-strength pattern | **95** | **1.000** | **53** |
| contraction observables | **43** | **0.453** | **0** |

| control / quantity | value |
|---|---|
| aggregated rate, observable dimension | **1** |
| minimal basis size | **95 readings** |

## Ordering

`clock 95 = acceleration 95 = field 95 (all maximal) > contractions 43 (redundant)`.

- **maximal:** clock, acceleration, field
- **partial:** none
- **redundant:** contractions

## Withdrawn prediction

The draft expected the field-strength pattern to be **partial** because the coupling is evaluated at each cell. The
measurement refused it: the tangent map is diagonal-plus-difference with a positive coupling, hence generically
invertible, and its dimension is **95**. Recorded as withdrawn.
