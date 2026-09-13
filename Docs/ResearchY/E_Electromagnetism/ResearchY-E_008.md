# ResearchY-E_008 - Field Excitation Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_008 (permanent)
**Title:** What AT mechanism produces a non-zero field strength F?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_008.md`
**Depends on:** E_007 (the plaquette, curvature and curl; F needs a fluctuation), E_006 (the edge connection; the tensor square), E_005 (the direction index; the pure-gauge phase), E_003 (AT's link phase), E_002 (the field equation)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_008_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/FieldExcitationAudit.cs`

## The question

What AT mechanism produces a non-zero field strength F? The mechanism must be (1) **local**, (2) **gauge-compatible**,
(3) **acting on T1 and T2**, (4) requiring **no new primitive**. Candidates: **occupancy gradients**,
**actualization gradients**, **deficit gradients**, **non-uniform rho**, **topological defects**. Measure F; compare
**F = 0** against **F != 0**.

## The answer: **DERIVED - a dichotomy that is exact on both sides**

> **Every gradient is exactly pure gauge (F = 0, for any coupling function). F != 0 requires a NON-DIFFERENCE
> coupling, and the survivor is NON-UNIFORM RHO read LOCALLY rather than through its gradient.**

## 1. The negative side is a theorem

A link phase written as the **difference** of any single-valued scalar - and `H(rho)` is still a single-valued scalar -
has an exactly vanishing holonomy around every closed plaquette, because the four contributions **telescope**.

| coupling | max \|F\| | verdict |
|---|---|---|
| `Delta_mu H(rho)`, H = rho | 2.22E-016 | **F = 0** |
| `Delta_mu H(rho)`, H = rho^2 | 2.50E-016 | **F = 0** |
| `Delta_mu H(rho)`, H = exp(rho) | 2.22E-016 | **F = 0** |
| `Delta_mu H(rho)`, H = sin(rho) | 2.22E-016 | **F = 0** |

Measured on a deliberately **non-separable** organisation. **That settles three of the five candidates at once:**
occupancy gradients, actualization gradients and deficit gradients cannot produce a field strength - *ever*, for any
coupling function.

## 2. The positive side is equally exact

| form | coupling | max \|F\| | verdict |
|---|---|---|---|
| `h(rho) Delta_mu rho` | h = 1 (constant) | 2.22E-016 | **F = 0** - that *is* the gradient |
| `h(rho) Delta_mu rho` | h = rho | **0.626** | **F != 0** |
| `h(rho) Delta_mu rho` | h = rho^2 | **0.778** | **F != 0** |
| `h(rho) Delta_mu rho` | h = exp(rho) | **1.126** | **F != 0** |
| `h(rho(x))` (site-local) | h = rho | **1.063** | **F != 0** |
| `h(rho(x))` (site-local) | h = rho^2 | **1.595** | **F != 0** |
| `mu h(rho(x))` (site-local, directional) | h = rho | **2.583** | **F != 0** |

**The site-local coupling has a closed form the audit verifies against the plaquette sum** (residual 3.33E-016):

> `F_mu_nu = h(rho(x + mu)) - h(rho(x + nu))`

So **non-uniform rho is the surviving candidate** - and it survives *precisely because it is read locally rather than
through its gradient*.

## 3. The topological-defect candidate collapses into the same answer

The natural way to write a defect is a winding phase with a branch cut. That construction is **pure gauge**:
`U_mu = exp(i Delta_mu theta)` **is** `g(x)^-1 g(x+mu)` by construction, so every plaquette equals one - measured over
**all** of them, cut included, at **6.11E-016**. The cut's `+-2 pi` jumps are artefacts, not physics.

The *wrapped* variant does light up plaquettes - **16** of them - but every value is a **whole turn**, and a whole turn
is the identity: the largest is **6.283185**, exactly one turn, and the signed total is **0**.

> **A genuine defect therefore needs an INDEPENDENTLY ASSIGNED link configuration - which is exactly the
> non-difference structure already found. One mechanism, not two.**

## 4. The four requirements hold for the survivor

| requirement | status |
|---|---|
| **local** | support 2 - a single site or one link: **True** |
| **gauge-compatible** | a site-dependent gauge transformation moves F by **5.55E-016**: **True** |
| **acts on T1 and T2** | F lives in the antisymmetric square, which at d = 3 **is** the vector irrep (E_006); the symmetric square is A1 + E + T2: **True** |
| **no new primitive** | measured against AT's own code: organisation **30** members, link phase **5**, winding **1**: **True** |

AT already computes a winding number - `InternalStateAnalyzer.ComputeWindingNumber`, in the oscillator line - and the
plaquette apparatus is E_007's.

## 5. What remains open

The audit locates the **class** of mechanism and excludes its rivals **exactly**; it does not derive **which** coupling,
nor what makes the organisation non-uniform in the first place. That selection is the **dynamics** layer E_005 located
and E_006 and E_007 left open.

## 6. Errors this audit caught

1. **A separable test field made the product form look like F = 0.** The first probe used
   `rho = sin(x) + cos(y)`, which cancels the product coupling's curvature identically, and the reading was roundoff
   (2.2E-016) - i.e. it looked like a theorem when it was an artefact of the test function. Re-probing with a
   **non-separable** organisation gave 0.626. The core now uses a non-separable organisation and the test asserts that
   the product form lights up, which would have caught it.
2. **An unsubstituted placeholder.** A requirement line was built with `.Replace(...)` placeholders and one of them -
   the verdict itself - was never substituted, so the report printed `{NoNewPrimitiveIsNeeded()}` and the test failed.
   Caught by the test, not by reading.

## Success criterion

> Locate the first dynamical source of field strength.

**Located: the non-difference (non-integrable) coupling of the link phase to the organisation** - site-local in rho -
with the exact exclusion of every gradient mechanism as a theorem rather than an observation. **DERIVED**, with the
selection of the coupling named as the open dynamics.
