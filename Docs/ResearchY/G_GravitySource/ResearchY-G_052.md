# ResearchY-G_052 - Amplitude Phase Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_052 (permanent)
**Title:** Can ρ be decomposed uniquely into an amplitude sector (42) and a phase sector (53)?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_052.md`
**Depends on:** G_050 (the phase sector is a union of Fourier modes: 47 + 5 + 1 = 53), G_051 (the phase sector is physical, not gauge), G_047 (the clock resolves the kernel), G_040 (the non-addressed ceiling and the contraction algebra), E_009 (the clock law), E_010 (the coupling)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_052_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/AmplitudePhaseAudit.cs`

## The question

Can ρ be decomposed **uniquely** into an **amplitude sector (42)** and a **phase sector (53)**? Measure
**orthogonality**, **invertibility**, **reconstruction accuracy**, and the **clock / acceleration / field responses**.
Determine whether **every observable** splits into an **amplitude contribution + phase contribution**.

## The answer: **DERIVED - the split is unique, orthogonal and exact, and the interface is an IDENTITY**

> ρ − mean = A + P, with **A** in the span of the 42 visible modes and **P** in the span of the 53 hidden ones. The
> decomposition is **canonical**, and the interface is not a description of where the boundary lies but **the boundary
> itself**:
>
> **(phase sector) = kernel of the contraction observables** · **(amplitude sector) = the contractions' row space minus the mean**

## 1. Orthogonality, reconstruction and uniqueness

| quantity | value |
|---|---|
| amplitude sector (visible modes) | **42** |
| phase sector (hidden modes) | **53** |
| state dimensions | **95** |
| **overlap between the sectors** | **4.418E-015** → orthogonal |
| **reconstruction ρ = mean + A + P** | **2.442E-015** → exact |
| **basis-independence residual** | **3.349E-012** → canonical |

**Uniqueness is not inferred from orthogonality alone.** The audit **rebuilds the amplitude subspace from a different
deterministic spanning set** and recomputes the projection; the two agree to **3.349E-012**, so the decomposition cannot
be an artefact of the modal basis one happens to choose.

## 2. The exact interface (the answer to the goal)

| quantity | value |
|---|---|
| contraction row-space rank | **43** |
| amplitude modes plus the mean | **43** |
| contraction-row vs phase overlap | **7.111E-014** |
| **the interface is an identity** | **True** |

The contraction observables span **the simplex direction plus the amplitude sector and nothing else**: the dimensions
agree, and every contraction row is orthogonal to every phase mode. Together those force the spans to be **equal**:

> **(phase sector) = kernel of the contraction observables (53)** and **(amplitude sector) = the contractions' row space
> minus the mean (42 = 43 − 1)**, with 42 + 53 = 95.

## 3. Does an observable split?

| reading | additivity residual | order |
|---|---|---|
| **linear functional** (mean occupancy) | **2.220E-016** | exact |
| clock | **1.128E-006** | **0.2498** → quadratic |
| acceleration | **4.588E-007** | **0.2503** → quadratic |
| field strength | **2.812E-008** | **0.2502** → quadratic |

A **linear** functional splits **exactly**. AT's own readings do not - the clock rate is a cube root of the occupancy
and the field strength multiplies a nonlinear coupling by an occupancy difference - and the audit measures **what order**
the shortfall is: halving the step **quarters** it, so the discrepancy is a **second-order cross term**, i.e. a curvature
effect rather than a failure of the decomposition. **In the linear-response sense the split is exact for every
observable.**

## 4. The contributions each reading takes from each sector

| reading | amplitude contribution | phase contribution |
|---|---|---|
| clock | **1.168E-003** | **1.031E-003** |
| acceleration | **3.464E-004** | **1.937E-004** |

Every reading takes a contribution from **both** sectors - the interface is a real partition of the response, not a
one-sided story.

## Verdict

**DERIVED.** ρ decomposes **uniquely** into amplitude and phase; the split of an **observable** is exact when the
observable is **linear** and exact **to first order** otherwise.
