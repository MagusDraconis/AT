# Y_E_009 Result - Coupling Function Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_009_Tests.cs`
**Status:** 7/7 PASSED
**Group total:** group E = **62/62 PASSED** (E_001 7 + E_002 6 + E_003 7 + E_004 6 + E_005 7 + E_006 8 + E_007 7 + E_008 7 + E_009 7)

## Verdict

**DERIVED** - h is not free. `h(rho) = (2 pi / 96) * rho^(1/d)`, with the exponent from **G_016b's clock law** and the
unit from **AT's phase quantum**: **zero free parameters**.

## The candidates

| candidate | max \|F\| | free params | source |
|---|---|---|---|
| constant | **0.000E+000** | 1 | chosen (value) |
| rho | 7.510E-001 | 1 | chosen (ansatz) |
| rho^2 | 2.216E+000 | 1 | chosen (ansatz) |
| exp(rho) | 3.362E+000 | 1 | chosen (ansatz) |
| **derived occupancy law** | 1.793E-002 | **0** | AT's clock law + phase quantum |
| actualization law | 1.793E-002 | **0** | the same law |

## The exponent sweep - the honest negative

`h = rho^p`: max \|F\| = **0** (p = 0), 0.145 (1/6), **0.274 (1/3)**, 0.390 (1/2), 0.751 (1), 2.216 (2), 5.062 (3).

**Exactly one exponent gives zero, and it is the constant.** The requirement "produces non-zero F" rules the constant
out and leaves the rest indistinguishable; the **clock law** selects the exponent.

## Gauge structure and the boundary

max \|F_0i\| (electric) **1.424E-002**, max \|F_ij\| (magnetic) **0.000E+000** -> **purely electric**. Closed-form
residual 0.000E+000; gauge invariance 1.11E-016; Bianchi 0.00E+000.

The clock law fixes the **time-like** component and says nothing about the **spatial** ones: the magnetic half is
still an input.

## Domain constraint (found by getting NaN)

`rho^(1/d)` needs a **non-negative** organisation; E_008's generic scalar is not. An occupancy is non-negative by
nature, so the physical domain satisfies it - now measured rather than assumed. E_008's results are unaffected (its
couplings were polynomials and exponentials, defined on both signs).

## Errors caught

The **NaN** that revealed the domain constraint, and a conceptual slip in the "not a difference" check - a first
version compared the derived **local** coupling against another *local* coupling and expected zero, when a local
function of rho is never a difference whatever function it is. The comparison now runs against E_008's **difference**
form.
