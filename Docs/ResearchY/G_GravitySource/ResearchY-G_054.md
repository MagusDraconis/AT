# ResearchY-G_054 - Phase Determination Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_054 (permanent)
**Title:** What fixes the 53 phase coordinates?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_054.md`
**Depends on:** G_050 (the phase sector is the hidden Fourier modes), G_051 (physical, not gauge), G_053 (independent of amplitude), G_052 (the interface is an identity), E_009 and E_013 (the update rule is purely electric; the coupling census is zero), E_014 (the flux label is an assignment)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_054_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/PhaseDeterminationAudit.cs`

## The question

What **fixes** the **53 phase coordinates**? Candidates: **symmetry**, **occupancy**, **multiplicity**, **attractor
structure**, **actualization history**, **boundary assignment**. Requirements: **no new primitive**, and the **clock**,
**acceleration** and **field** laws **preserved**. Goal: is the phase sector **dynamically determined** or **freely
assigned**?

## The answer: **BOUNDARY - freely assigned**

> Nothing in AT fixes the phase coordinates. The audit states exactly what would have to be added for something to: a
> coupling whose **gradient has a phase component** *and* which the theory **actually runs**. AT has the **first**
> without the **second**.

## 1. The conservation theorem (the centre of the audit)

| measurement | value |
|---|---|
| amplitude moves change the coordinates by | **3.232E-015** (orthogonality) |
| symmetry moves change them by | **2.540E-001** |
| **invariant-driven flow changes them by** | **1.431E-015** → **CONSERVED** |
| an invariant gradient's phase component | **7.625E-012** → zero |

The contraction functionals are **invariant**, so their gradients lie in the **invariant subspace** - which G_052
measured to be exactly the amplitude modes plus the mean. A flow along such a gradient can never leave that subspace,
so **the phase coordinates are constants of the motion of any dynamics driven by an invariant functional.**

## 2. Sensitivity is not determination

| quantity | value |
|---|---|
| clock functional's phase fraction | **3.768E-003** |
| field functional's phase fraction | **8.572E-002** |
| the laws are phase-sensitive | **True** |
| **no AT process runs a phase flow** | **True** |
| update rule spatial part | **0.000E+000** |
| coupling census | **0** |

The local laws **do feel** the phases - a process that extremised one **would** move the coordinates - but no AT process
does. Both halves are measured separately, which is what lets the audit call this a **boundary** rather than a mystery.

## 3. The candidates

| candidate | status | basis |
|---|---|---|
| symmetry | **REFUTED** | its invariants **are** the amplitude modes plus the mean, so the phase coordinates are its **non-invariant** content; a symmetry move changes them by **2.540E-001** |
| occupancy (as invariant) | **REFUTED** | the contractions annihilate the phase exactly; the addressed state's determination is a **tautology**, recorded and not counted |
| multiplicity | **REFUTED** | the recorded spectrum - **45 levels, 96 modes** - is fixed by the Laplacian, not by the state |
| attractor structure | **REFUTED** | no flow from the reachable structure moves the phase (**1.431E-015**) |
| actualization history | **REFUTED** | the update rule's spatial part is **0.000E+000** and the census is **0** |
| **boundary assignment** | **BOUNDARY** | what remains, and the answer |

**The determination mechanism: an external assignment** - the same shape of answer the **flux label** received in
**E_014**, with one difference worth stating: there the assignment was **inert**, and here it is **physically active**
(G_051, G_053).

## 4. The requirements

| requirement | status |
|---|---|
| no new primitive | the coordinates use only the state and the substrate's own modes; **0** members couple the link sector to the organisation |
| clock law preserved | read, not modified - its gradient carries a phase fraction of **3.768E-003**, so the law is **sensitive** without **fixing** |
| acceleration law preserved | inherits the same sensitivity through the clock rates; the law is untouched |
| field law preserved | gradient phase fraction **8.572E-002**; no AT process extremises it (**0.000E+000** spatial part) |

## Verdict

**BOUNDARY.** The phase sector is **freely assigned**. A coupling whose gradient has a phase component **and** which the
theory actually runs would **determine** it - and AT has the first without the second.
