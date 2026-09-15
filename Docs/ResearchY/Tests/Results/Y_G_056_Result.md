# Y_G_056 - Result

**Audit:** ResearchY-G_056 - Non-Scalar Selection Audit
**Verdict:** **DERIVED**
**Tests:** 6/6 PASSED

## Answer

The **occupancy-gradient vector field** spans the phase sector **exactly** (phase rank **53 of 53**), as do its connection
and tensor descendants. G_055's ceiling was a ceiling on **scalars** - and spanning is **sensitivity**, not
**determination**.

## Measurements

| candidate | outputs | phase rank | verdict |
|---|---|---|---|
| phase vector field | 96 | **53** | DERIVED |
| connection structure | 96 | **53** | DERIVED |
| T1/T2 sector coupling | 288 | **53** | DERIVED |
| edge-holonomy network | 96 | **0** | REFUTED |
| causal-order tensor | 96 | **0** | REFUTED |

| quantity | value |
|---|---|
| scalar ceiling (G_055) | **3** |
| phase directions with non-zero derivative | **53 of 53** |
| coupling's T1 half | **0.000E+000** (vanishes identically) |
| no AT process runs a phase flow | **True** |

## Two honesty notes

1. **The T1 half vanishes** because the shifts commute; the coupling's rank comes from its **symmetric** half - reported
   rather than relied on silently.
2. **The candidate table carries no verdict literals**: verdicts are computed from the measured rank against the ceiling,
   as this project's rule 6 requires.
