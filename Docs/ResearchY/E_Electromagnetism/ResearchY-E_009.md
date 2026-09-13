# ResearchY-E_009 - Coupling Function Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_009 (permanent)
**Title:** What determines h(rho)?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_009.md`
**Depends on:** E_008 (the coupling must not be a difference; F != 0 needs a non-gradient coupling), E_007 (the plaquette curvature), E_006 (the tensor square), E_003 (AT's phase quantum), G_016b (the clock law), G_042/G_045 (the clock exponent 1/d)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_009_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/CouplingFunctionAudit.cs`

## The question

What determines `h(rho)`? Candidates: **constant**, **rho**, **rho^2**, **exp(rho)**, a **derived occupancy law**, an
**actualization law**. Requirements: produces non-zero F, compatible with T1 and T2, no new primitive. Measure the
**field strength**, the **gauge structure** and the **minimality**.

## The answer: **DERIVED - h is not free. AT already computes it**

> **`h(rho) = (2 pi / 96) * rho^(1/d)`** - AT's own clock law, in AT's own phase quantum. **Zero free parameters.**

## 1. The coupling is AT's own clock law

AT computes the proper-time rate of a clock as `GpsCorrectionOrigin.ClockRate(d, rho) = rho^(1/d)` - **G_016b's clock
law**, `d tau/dt = rho^(1/d)` - whose **exponent** is fixed by the substrate dimension through
`SubstrateDimensionAudit.ClockExponent(d) = 1/d`, and whose **unit** is AT's phase quantum `2 pi / 96` (E_003). Read as
a link phase that *is* the coupling function of E_008.

| input | source | free parameters |
|---|---|---|
| the exponent `1/d` | G_016b's clock law, `ClockExponent(d) = 1/d` | 0 |
| the unit `2 pi / 96` | AT's phase quantum (E_003) | 0 |

A live scan finds **5** AT members computing a clock rate / exponent / law.

## 2. The candidates

| candidate | max \|F\| | free parameters | source |
|---|---|---|---|
| constant | **0.000E+000** | 1 | chosen - its value |
| rho | 7.510E-001 | 1 | chosen - the ansatz |
| rho^2 | 2.216E+000 | 1 | chosen - the ansatz |
| exp(rho) | 3.362E+000 | 1 | chosen - the ansatz |
| **derived occupancy law** | 1.793E-002 | **0** | AT's clock law + AT's phase quantum |
| actualization law | 1.793E-002 | **0** | the same law under another name |

**The constant is REFUTED** (F = 0 - it *is* the gradient E_008 excluded). The four chosen couplings work but carry a
free parameter. **Only the clock-law candidate carries none.**

## 3. The honest negative: the requirement does not select the exponent

Sweeping `h = rho^p` over seven exponents:

| p | 0 | 1/6 | **1/3** | 1/2 | 1 | 2 | 3 |
|---|---|---|---|---|---|---|---|
| max \|F\| | **0** | 1.448E-001 | **2.740E-001** | 3.895E-001 | 7.510E-001 | 2.216E+000 | 5.062E+000 |

The field strength vanishes at **exactly one** exponent, `p = 0`, because a constant coupling *is* the gradient. Every
other exponent works. So **"produces non-zero F" rules the constant out and leaves the rest indistinguishable** - the
exponent is selected by the **clock law**, not by the requirement. That distinction is the audit's answer to *derive h
instead of choosing it*.

## 4. Minimality is countable, and it decides it

Every other candidate carries a free parameter - the constant its value, and rho, rho^2, exp(rho) their ansatz. The
minimum is **0**, attained by exactly **two** candidates, and those two are **the same law under two names**: no member
anywhere in AT is named for an actualization law. The two law candidates coincide - a **degeneracy, not a choice**.

## 5. The gauge structure - and where the boundary sits

| | |
|---|---|
| max \|F_0i\| (electric) | **1.424E-002** |
| max \|F_ij\| (magnetic) | **0.000E+000** |
| purely electric | **True** |
| `F_0i = -Delta_i h(rho)`, closed-form residual | 0.000E+000 |
| gauge invariance residual | 1.11E-016 |
| discrete Bianchi residual | 0.00E+000 |
| the coupling is not a difference | True |

With `A_0` from the clock law and no spatial phase, the field strength is **purely electric**: the electric field is
exactly minus the gradient of the clock rate, it is **gauge invariant**, and it is automatically **Bianchi-consistent**.

> **The boundary is precise rather than general:** the clock law fixes the **time-like** component of the coupling and
> says nothing whatever about the **spatial** ones, so the **magnetic half** of the field strength is still an input.

## 6. The domain is a constraint - discovered by getting NaN

`rho^(1/d)` requires a **non-negative** organisation. E_008's generic test scalar goes negative, and this audit
**got NaN** the first time it evaluated `rho^(1/3)` on it. An occupancy is non-negative by nature, so the physical
domain satisfies the constraint automatically - but it is a real condition on the coupling, and the core now measures
it (`MinimumOccupancy()`, `TheOrganisationIsPhysical()`). **E_008's own results are unaffected**: its couplings were
polynomials and exponentials, defined on both signs, and its gradient theorem held on the negative values too.

## 7. Errors this audit caught

1. **NaN from taking `rho^(1/3)` of a sign-changing scalar** - which is how the domain constraint above was found
   rather than assumed.
2. **A conceptual error in `TheDerivedCouplingIsNotADifference`.** A first version compared the derived local coupling
   against *another local coupling* (`h = rho`) and expected zero, which is wrong: a **local function of rho is never a
   difference, whatever function it is**. The comparison now runs against E_008's **difference** form, which is the
   form that vanishes. Caught by the test.

## Success criterion

> Derive h(rho) instead of choosing it.

**Derived: `h(rho) = (2 pi / 96) rho^(1/d)`** - exponent from G_016b's clock law, unit from AT's phase quantum, **zero
free parameters**, producing a non-zero, gauge-invariant, Bianchi-consistent, purely **electric** field strength.
**DERIVED**, with the spatial couplings named as the next open item.
