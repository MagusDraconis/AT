# Y_G_049 - Result

**Audit:** ResearchY-G_049 - Clock Primacy Audit
**Verdict:** **LOSSLESS** (not unique)
**Tests:** 6/6 PASSED

## Answer

The clock pattern is lossless and information-equivalent to ρ (closed-form inverse ρ = rate^d, residual **4.441E-016**,
**0** collisions). It is **not unique**: the acceleration and field-strength patterns are lossless too. Only the
contractions (43/95) lose information.

## Measurements

| reading | rank | retained | collisions | best residual | verdict |
|---|---|---|---|---|---|
| clock pattern | **95** | **1.000** | **0** | — (closed form 4.441E-016) | **LOSSLESS** |
| acceleration pattern | **95** | **1.000** | **0** | **0.000E+000** | **LOSSLESS** |
| field-strength pattern | **95** | **1.000** | **0** | **0.000E+000** | **LOSSLESS** |
| contraction observables | **43** | **0.453** | n/a | n/a | **LOSSY** |

- lossless readings: **3**
- the clock is unique: **False**
- control start recovers the state: **True**

## Withdrawn proof

The reflection-through-the-mean collision was expected to make the acceleration pattern lossy. The measurement refused
it (**1.951E-002** apart): the reflection reverses differences of **ρ**, the pattern uses differences of the **rate**,
and the cube root is not linear.

## Hypothesis not confirmed

**No** full-rank reading was measured lossy. Rank and invertibility coincided for all three here - recorded as an
empirical outcome of this state, not a theorem.
