# ResearchY-G_046 - Rho Accessibility Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_046 (permanent)
**Title:** Can the hidden 47 dimensions of rho ever influence an observable?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_046.md`
**Depends on:** G_040 (95 state dimensions; the contraction ceiling 48 retained, 47 lost; the doublet count), E_013 (zero members couple the spectral sector to the link sector), E_009 (the update rule's spatial part is zero), G_016b (the clock law the rates are read from)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_046_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/RhoAccessibilityAudit.cs`

## The question

Can the **hidden 47 dimensions** of ρ ever influence an **observable**? Given G_040: **95-dimensional ρ**, **49-channel
observable algebra** with a **48-dimension non-addressed ceiling**. Do the hidden orientations affect **clocks**,
**acceleration**, **flux sectors**, **field strengths** - or are they **permanently gauge-like**?

## The answer: **OBSERVABLE - and the first identification of the hidden directions was wrong**

> A validated hidden step **does** reach the local laws: it moves the clock, acceleration and field-strength multisets
> while changing **no** contraction. Only the **flux sector** is untouched.

## 1. The split, and the identification that had to be withdrawn

| quantity | value |
|---|---|
| state dimension (simplex) | **95** |
| invariant dimensions (G_040) | **48** |
| hidden dimensions (doublet count) | **47** |
| the two fill the state | **True** |
| **orbit span of the symmetry** | **84** |
| the orbit IS the hidden set | **False** |

**The first draft identified the hidden directions with the substrate-symmetry ORBIT and was refused by the
measurement.** A symmetry move *is* gauge-like, but for a different reason - it **relabels** - and conflating the two
would have made the whole argument rest on a false premise. The identification is **withdrawn and recorded**.

## 2. Symmetry moves are relabelling-only (the gauge control)

| observable | multiset change over the orbit | addressed change |
|---|---|---|
| clocks | **0.000E+000** | 1.719E-001 |
| acceleration | **0.000E+000** | 1.603E-001 |
| field strengths | **5.140E-016** | 5.823E-002 |
| flux sectors | **0.000E+000** | (the label reads the link phases) |

Controls: the addressed pattern sees the move at **4.937E-001**; moving the phase moves the flux label by **1.000000**.

## 3. The validated hidden step - the decisive measurement

A direction built as the **kernel of the contraction observables** and made **sum-neutral**:

| measurement | value |
|---|---|
| contraction change along the step | **5.116E-013** (nothing, by construction) |
| **clock-rate multiset** | **3.764E-003** |
| **acceleration multiset** | **3.010E-003** |
| **field strength** | **6.327E-004** |
| flux sector | **0.000E+000** |
| members coupling the spectral and link sectors | **0** |

**A hidden step changes no contraction and still moves three of the four local laws.** A first version of the step
renormalised the perturbed state, which also moved it along *observable* directions and made a "hidden" step change a
contraction by **2.061**; the sum-neutral projection is what makes the step honest.

## 4. The targets

| target | status | basis |
|---|---|---|
| clocks | **OBSERVABLE** | hidden step moves the multiset by 3.764E-003 |
| acceleration | **OBSERVABLE** | 3.010E-003 |
| field strengths | **OBSERVABLE** | 6.327E-004 |
| flux sectors | **HIDDEN** | label carried by the link phases; census 0 |

## 5. One number recorded against G_040 rather than smoothed away

The kernel measures **53** dimensions at the audited state against the **ceiling of 47**, retaining **43** against
**48**. G_040's 48 is therefore a **ceiling over states**, not the value at every state: a **refinement** of G_040, not
a contradiction - and the audit reports both numbers side by side.

## Verdict

**OBSERVABLE.** The hidden 47 **reach the local laws**: G_040's ceiling is the price of **not addressing cells**, and
the local laws do. What remains gauge-like is exactly what the theory's own decoupling requires: the **flux sector**,
whose label is carried by the link phases.
