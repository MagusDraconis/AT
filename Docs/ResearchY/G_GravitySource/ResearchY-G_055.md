# ResearchY-G_055 - Phase Selection Principle Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_055 (permanent)
**Title:** Can any existing AT quantity assign a preferred phase state?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_055.md`
**Depends on:** G_052 (the interface identity), G_054 (the phase coordinates are freely assigned), E_014 (the flux label is an assignment), E_015 (the sector measure is exactly flat), G_050 (the phase sector)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_055_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/PhaseSelectionPrincipleAudit.cs`

## The question

Can any **existing** AT quantity assign a **preferred phase state**? Candidates: **entropy**, **free room**,
**actualization density**, **flux sector**, **clock functional**, **field functional**. Test: does any quantity **break
phase degeneracy**? The critical question: **why this phase instead of another?**

## The answer: **BOUNDARY - none can, and the deficiency is measured**

> A quantity that could assign a preferred phase must be a **scalar functional**, and the gradient of a scalar is **one
> vector**: it constrains **at most one** phase direction. Six candidates cannot pin 53 directions.

## 1. The candidates, measured

| candidate | phase projection norm | directions | class |
|---|---|---|---|
| entropy | **3.334E-004** | 1 | phase-sensitive |
| free room | **3.952E-014** | 0 | **PHASE-BLIND** |
| actualization density | **4.104E-016** | 0 | **PHASE-BLIND** |
| flux sector | **0.000E+000** | 0 | **PHASE-BLIND** |
| clock functional | **1.241E-002** | 1 | phase-sensitive |
| field functional | **1.678E-003** | 1 | phase-sensitive |

**Three of the six constrain nothing at all** - a stronger statement than "weakly": free room *is* the simplex
constraint (constant gradient), actualization density's scalar level *is* that same constant, and the flux sector is
decoupled from the organisation (**census 0**).

## 2. The counting fact and the measured deficiency

| quantity | value |
|---|---|
| phase dimensions | **53** |
| candidates | **6** |
| measured constraint rank | **3** |
| counting bound holds | **True** |
| **DEFICIENCY (free phase directions)** | **50** |

The rank is measured rather than bounded, and it is **3**: **50 of the 53 phase directions are constrained by nothing
the theory contains**, in any combination.

## 3. Is the audited state a critical point of anything?

| candidate | largest phase directional derivative |
|---|---|
| entropy | 2.808E-004 |
| free room | 4.263E-010 |
| actualization density | 4.441E-012 |
| flux sector | 0.000E+000 |
| clock functional | **1.038E-002** |
| field functional | **6.252E-004** |

**No sensitive candidate is stationary**: nothing is extremised at this state either, so even the three directions the
candidates do see carry no preference here.

## 4. The critical question, answered in its own terms

> **Why this phase instead of another** has **NO answer inside AT** for the **50** unconstrained directions, because no
> quantity the theory contains is sensitive to them.

**What would break the degeneracy:** a set of **53** independent scalar functionals, or one non-scalar structure whose
gradient spans the phase sector. AT has neither.

## Two numerical defects caught by the measurements

1. **Cancellation in the gradients.** Free room's gradient is *exactly* the constant direction (zero phase projection),
   but differencing a function of order **96** with a step of **1E-6** leaves a floor of **2.540E-008** and made a
   phase-blind candidate look phase-sensitive. Gradients are now **analytic** where they are trivial (free room,
   actualization density, flux, clock), differenced only where genuinely nonlinear, with the step raised to **1E-4**.
2. **The exact-zero floor, stated with its origin.** The zeros now sit at about **4E-14**, because a phase mode's own
   sum vanishes only to ~1E-16 and is multiplied by 96 unit components; the classification threshold **1E-9** is six
   orders above that floor and four below the smallest genuine response (**3.3E-004**).
3. **A delegate-identity bug:** locating a candidate by `ReferenceEquals` on its method group threw immediately, since a
   method group converts to a **new** delegate on every evaluation; the lookup is now **by name**.

## Verdict

**BOUNDARY.** No existing AT quantity assigns a preferred phase; **3** directions are constrained and **50** are free.
