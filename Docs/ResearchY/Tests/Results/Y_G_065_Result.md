# Y_G_065 - Result

**Audit:** ResearchY-G_065 - Phase-Free Principle Audit
**Verdict:** **BOUNDARY**
**Tests:** 7/7 PASSED

## Answer

**No AT law fails** when the phase content is non-zero: all **seven** laws hold at the floating-point floor for every
state, the phase-free one included, and **no law separates** them. But phase-freeness is **not a mere convention** either:
the theory's own **dissipative flow converges to it** - the phase norm of a state occupying 42 hidden directions falls
**1.048E+000 → 1.610E-001** over 20 000 steps, and the hidden occupancy falls from **42 to 18**.

## Measurements

| state | phase-free | phase norm | hidden occupied | worst law residual |
|---|---|---|---|---|
| canonical | **True** | 9.246E-015 | 0 | **4.441E-016** |
| alternative seed | False | 1.048E+000 | **42** | 4.394E-016 |
| shifted w' | False | 2.285E-001 | 1 | 4.411E-016 |
| ramp | False | 3.657E-001 | 2 | 8.882E-016 |
| all modes | False | 1.166E+000 | **53** | 4.396E-016 |

| measurement | value |
|---|---|
| laws measured | **7** (clock, metric, potential, source, field, simplex, flux) |
| laws failing for any state | **0** |
| laws separating phase-free from phase-bearing | **0** |
| worst residual in the family | **8.882E-016** |
| every state stays a density (both flows, 4000 steps) | **True** |
| the flow's phase-norm decay | **1.048E+000 → 1.610E-001** |
| the flow's hidden occupancy | **42 → 18** |
| the interface (no split, hidden ⟺ empty) | holds for **every one-seed** state; fails only at **saturation** |

## Notes

**Three laws were mis-stated at first and the measurement caught all three:** each gave a residual of order **1E-2 for
every** state, including the phase-free one - the signature of a defect in the law's statement. The potential law
subtracted a term too many; the source law compared a **clamped** helper against a **periodic** difference; and the field
law took the magnitude of a **signed** quantity.

**The linearised source law is a discretisation order, not a law:** the log form is **exact** (residual 0 everywhere) while
`Δρ/ρ` is first order, with a residual that measures the state's **roughness** (1.4E-002 for the canonical state, 5.98E-001
for the all-modes one).
