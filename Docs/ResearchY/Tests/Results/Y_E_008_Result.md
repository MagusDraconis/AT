# Y_E_008 Result - Field Excitation Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_008_Tests.cs`
**Status:** 7/7 PASSED
**Group total:** group E = **55/55 PASSED** (E_001 7 + E_002 6 + E_003 7 + E_004 6 + E_005 7 + E_006 8 + E_007 7 + E_008 7)

## Verdict

**DERIVED** - every gradient is exactly pure gauge; **F != 0 requires a NON-DIFFERENCE coupling**, and the survivor is
**non-uniform rho read locally**.

## F = 0 versus F != 0

| form | coupling | max \|F\| | verdict |
|---|---|---|---|
| `Delta_mu H(rho)` | H = rho | 2.22E-016 | **F = 0** |
| `Delta_mu H(rho)` | H = rho^2 | 2.50E-016 | **F = 0** |
| `Delta_mu H(rho)` | H = exp(rho) | 2.22E-016 | **F = 0** |
| `Delta_mu H(rho)` | H = sin(rho) | 2.22E-016 | **F = 0** |
| `h(rho) Delta_mu rho` | h = 1 | 2.22E-016 | **F = 0** (that *is* the gradient) |
| `h(rho) Delta_mu rho` | h = rho | **0.626** | **F != 0** |
| `h(rho) Delta_mu rho` | h = rho^2 | **0.778** | **F != 0** |
| `h(rho) Delta_mu rho` | h = exp(rho) | **1.126** | **F != 0** |
| `h(rho(x))` | h = rho | **1.063** | **F != 0** |
| `h(rho(x))` | h = rho^2 | **1.595** | **F != 0** |
| `mu h(rho(x))` | h = rho | **2.583** | **F != 0** |

Closed form of the survivor, verified against the plaquette sum (residual **3.33E-016**):
`F_mu_nu = h(rho(x+mu)) - h(rho(x+nu))`.

## Candidate verdicts

| candidate | verdict |
|---|---|
| occupancy gradients | **REFUTED** - exactly pure gauge |
| actualization gradients | **REFUTED** - exactly pure gauge |
| deficit gradients | **REFUTED** - exactly pure gauge |
| **non-uniform rho** | **DERIVED** - read locally, not through its gradient |
| topological defects | **REFUTED as a gradient construction** - the winding phase is pure gauge (6.11E-016 over all plaquettes, cut included), the wrapped variant's 16 lit plaquettes are all whole turns; a genuine defect needs an independently assigned link configuration, i.e. the same mechanism |

## Requirements

local (support 2) | gauge-compatible (5.55E-016) | acts on T1 and T2 (antisymmetric square = vector irrep at d = 3) |
no new primitive (organisation 30 members, phase 5, winding 1 - AT already computes a winding number).

## Errors caught

A **separable** test field made the product form look like F = 0 (an artefact of the test function, not a theorem);
and an unsubstituted `.Replace()` placeholder printed `{NoNewPrimitiveIsNeeded()}` into the report. Both caught by the
tests.
