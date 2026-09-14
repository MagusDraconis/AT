# ResearchY-G_048 - Clock Completeness Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_048 (permanent)
**Title:** Is the clock pattern the maximal observable of rho?
**Status:** COMPLETE
**Date:** 2026-09-14
**File:** `G_GravitySource/ResearchY-G_048.md`
**Depends on:** G_047 (the clock pattern is the first observable that resolves the kernel), G_046 (the kernel and the validated hidden step), G_040 (the non-addressed ceiling of 48), E_009 (the clock law), G_016b (the rate law), E_010 (the coupling that makes the field pattern nonlinear in the organisation)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_048_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ClockCompletenessAudit.cs`

## The question

Is the **clock pattern** the **maximal observable** of ρ? Compare the **clock pattern**, the **acceleration pattern**,
the **field-strength pattern** and the **contraction observables**. Measure **kernel rank**, **information retained**,
**observable dimension**. Goal: decide whether **clock > acceleration > contraction**, or whether the readings are
**equivalent**.

## The answer: **MAXIMAL - and it is maximal AND TIED WITH TWO OTHERS**

> The three **addressed** readings are all complete; the **contraction observables** are the only reading measured here
> that loses information.

## 1. The four readings

| reading | observable dim | information retained | kernel rank |
|---|---|---|---|
| **clock pattern** | **95** | **1.000** | **53** |
| **acceleration pattern** | **95** | **1.000** | **53** |
| **field-strength pattern** | **95** | **1.000** | **53** |
| contraction observables | **43** | **0.453** | **0** |

State dimension (simplex tangent space): **95**. Top rank tied: **True**.

## 2. The clock pattern is complete - and invertible, not merely of full rank

| quantity | value |
|---|---|
| clock observable dimension | **95 of 95** |
| invertibility residual (ρ = rate^d) | **4.441E-016** |
| the clock pattern determines the state | **True** |
| **aggregated rate, observable dimension** (the control) | **1** |
| addressability is required | **True** |
| minimal basis size | **95 readings** |

The **control** is what keeps the maximal claim honest: aggregating the *same* law destroys almost all of it (dimension
**1**). The completeness is a property of **addressing the cells**, not of the clock law on its own.

## 3. Two other readings tie at the top

**The acceleration pattern.** Built from neighbouring differences - which on an *arbitrary* vector must lose the global
constant - but the state lives on the **simplex**, which already excludes it, so on the tangent space the difference map
is **injective**: dimension **95**.

**The field-strength pattern, and a withdrawn prediction.** The draft expected this one to be **partial**, on the
grounds that the coupling is evaluated at each cell and the pattern is therefore nonlinear in the organisation. The
measurement **refused it**: the tangent map is diagonal-plus-difference with a positive coupling, hence generically
invertible, and its dimension is **95** as well. The expectation is recorded as **withdrawn**.

## 4. The ordering is neither a chain nor total equivalence

| tier | readings |
|---|---|
| **maximal** | clock pattern, acceleration pattern, field-strength pattern |
| **partial** | *(none)* |
| **redundant** | contraction observables |

`clock 95 = acceleration 95 = field 95 (all maximal) > contractions 43 (redundant)`.

So **"clock > acceleration > contraction" is refuted on both its strict inequalities**, and **"all are equivalent"** is
refuted by the contractions. What remains is a **two-level structure**: the addressed readings are complete, the
non-addressed one is not.

## Verdict

**MAXIMAL.** The clock pattern resolves the whole state and is invertible; it is **tied** with the acceleration and
field-strength patterns; and the **contraction observables** are a proper sub-algebra of what it already resolves -
which is what G_040's ceiling of 48 always was, the ceiling of the **non-addressed** readings.
