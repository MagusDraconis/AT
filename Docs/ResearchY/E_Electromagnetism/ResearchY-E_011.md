# ResearchY-E_011 - Flux Origin Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_011 (permanent)
**Title:** What generates the surviving non-trivial loop flux F = 2 pi / 96?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_011.md`
**Depends on:** E_007 (the uniform flux, the quantisation and the number 2 pi / 96), E_010 (no occupancy-derived curvature survives refinement), E_009 (the coupling is the clock law; the time-like component only), E_008 (a winding is a gradient, so it has no curvature), E_003 (the phase, and its compactness)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_011_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/FluxOriginAudit.cs`

## The question

What **generates** the surviving non-trivial loop flux `F = 2 pi / 96`? Candidates: **occupancy structure**, **D96
topology**, **winding number**, **actualization process**, **boundary assignment**. Requirements: **survives the
continuum limit**, **gauge compatible**, **local**, **acts on T1 and T2**. Measure **flux**, **holonomy**, **field
strength**.

## The answer: **DERIVED - the origin is D96's own closed topology together with the compactness of the phase**

> **The number `2 pi / 96` is the substrate's cycle length speaking.**

## 1. The two ingredients, both computed

**A closed cycle makes the holonomy gauge-proof.** A gauge function is single-valued, so its contribution around a
closed cycle **telescopes to zero**: measured **2.498E-016** at L = 96 and **0.000E+000** at L = 17. This is the exact
complement of E_008's theorem - a *contractible* loop's gradient flux can be gauged away, a *closed cycle*'s cannot.

**A compact phase quantises the flux.** The link variable is a **phase**, so its value at the far end of the cycle must
equal its value at the near end. For a uniform field `A_1 = f y` that forces `f L = 2 pi n`:

| sector | periodicity residual at L = 96 |
|---|---|
| n = 1 | 2.4E-016 |
| n = 2 | 4.9E-016 |
| n = 3 | 7.3E-016 |
| **half a sector** | **NOT periodic - not allowed** |

The audit checks the half-sector case too, because a condition that *rejects* something is worth more than one that
accepts everything.

## 2. The quantum is the cycle length

| L | quantum `2 pi / L` | quantum x L |
|---|---|---|
| 8 | 0.785398163 | 6.283185307 |
| 16 | 0.392699082 | 6.283185307 |
| 32 | 0.196349541 | 6.283185307 |
| **96** | **0.065449847** | 6.283185307 |

**E_007's figure is reproduced here rather than quoted** (agreement to 1E-15). A number that comes out of the length
of a loop is not a free parameter - it is a property of the loop.

## 3. The candidates

| candidate | status | basis |
|---|---|---|
| occupancy structure | **REFUTED** | E_010: every occupancy-derived curvature scales away, exponents **2.92** and **0.99** |
| **D96 topology** | **DERIVED** | the closed cycle makes the holonomy gauge-proof (**2.50E-016**) and the compact phase quantises it |
| winding number | **REFUTED** | a winding is a gradient - zero curvature (E_008) - **and** its cycle holonomy is a whole turn (**2.45E-016** from the identity) |
| actualization process | **REFUTED** | it supplies the time-like component only (E_009), which cannot contribute to a spatial strength |
| boundary assignment | **BOUNDARY** | every integer `n` is allowed and nothing in AT selects one - the **sector label** |

## 4. Flux, holonomy and field strength - what exactly survives

| quantity | value |
|---|---|
| flux per plaquette (field strength) | **0.065449847** |
| cycle holonomy | **6.283185307** = one whole turn, distance from the identity **2.45E-016** |
| plaquette holonomy `\|exp(i f) - 1\|` | **6.544E-002** - the non-triviality is **local** |

**At fixed sector the strength falls as `1 / L`:** 0.785398163 (L = 8) → 0.392699082 (16) → 0.196349541 (32) →
**0.065449847** (96). A non-trivial loop configuration **exists at every size**.

> So the flux's content is **local curvature, not a topological charge**, and what genuinely survives the refinement is
> the **existence** of a non-trivial loop configuration, not its magnitude.

## 5. Verdict: DERIVED - with the sector label named as the honest residue

The **origin** is identified by computation: a **closed cycle** (making the flux gauge-proof) plus a **compact phase**
(quantising it). The **quantum** `2 pi / 96` follows from the cycle length and nothing else. Three of the four
substantive candidates are refuted, each on a measured witness, and the fifth - assigning the integer - is a **sector
label**, exactly as in gauge theory on a torus: the quantisation is derived, the integer is a boundary condition.

## Success criterion

> Identify the origin of the first non-trivial field quantity.

**Identified: the closed topology of the substrate together with the compactness of the phase.** E_007 *exhibited* the
quantity; E_010 showed the occupancy cannot supply it; this audit says why it exists at all.
