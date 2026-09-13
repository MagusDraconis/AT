# ResearchY-E_016 - Sector Population Principle Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_016 (permanent)
**Title:** Do any existing AT quantities break the flat sector measure?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_016.md`
**Depends on:** E_015 (P(n) flat; the bijection; the enumeration), E_014 (no law depends on the sector; the blindness residual), E_013 (no potential for the flux; the zero-coupling census), E_012 (the whole-torus constraint), E_011 (the quantum), E_009 (the update rule is purely electric)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_016_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/SectorPopulationPrincipleAudit.cs`

## The question

Do any **existing** AT quantities **break** the flat sector measure that E_015 established? Candidates: **occupancy
free room**, **multiplicity structure**, **D96 hierarchy**, **compression laws**, **actualization rate**. Goal: find the
first **non-flat weighting** without introducing new primitives.

## The answer: **REFUTED - no existing AT quantity breaks the flat measure**

> And the audit **validated its detector before believing it**: a null result means nothing unless the method could
> have found something.

## 1. The detector, validated before it is trusted

The search is a **sector-restricted sum of a weight** over every configuration of a finite model, so a breaker shows up
as a non-zero **relative spread** of those sums.

| weight | relative spread of the sector sums |
|---|---|
| trivial (the control) | **0.000E+000** |
| a potential uniform in the phase (sanity) | **0.000E+000** |
| **a NON-uniform local potential (breaker one)** | **1.6E-001** |
| **a recipe-phase BRIDGE (breaker two)** | **3.3E-001** |
| the detector is sensitive | **True** |
| a uniform potential is flat | **True** |

The machinery **can** detect a breaker. It then finds **none** among the five candidates.

## 2. A prediction withdrawn, and where the real nearest miss is

The first draft expected the conditional distribution of a **coarse phase observable** to differ across sectors. Measured,
it does **not**:

| sector | mean number of links in the lower half of the phase range |
|---|---|
| 0, 1, 2, 3 | **identical** (conditional spread **0.000E+000**) |

The global **half-period shift** maps the lower half of the phase range onto the upper half while leaving the class
unchanged whenever `(k/2)L` is a multiple of `k`, so the mean is **forced to L/2** in every class: a **symmetry**, not
an accident.

**The real nearest miss is a WEIGHT rather than an observable:** a phase-reading weight breaks the class sums at
**1.6E-001**, so nothing about the configuration space prevents a non-flat measure - what prevents it is that AT
supplies no such weight.

## 3. The five candidates fail for two distinct reasons

**Recipe-side quantities** (the sector labels the *phase* half, and the two are decoupled, so their sums factorise):

| | value |
|---|---|
| free room per sector at (k,l) = (4,6) | **4096, 4096, 4096, 4096** |
| spread over the sectors | **0.000E+000** |
| factorisation residual | **0.000E+000** |
| AT members coupling a spectral index to a link phase | **0** (E_013's census, reused) |

**Phase-side quantities, both sector-symmetric:** multiplicity spread over the sectors **0.000E+000** (E_015's
bijection, reused), and the update rule's spatial part **0.000E+000** against a time-like part of **1.424E-002**, with
E_014's sector-blindness residual at **4.163E-017**.

## 4. What a breaker would have to be

> Either a **POTENTIAL for the link phase** - a weight with a non-uniform local factor, which the validated detector
> breaks at a spread of **1.6E-001** - or a **COUPLING** between the recipe sector and the phase sector, which the same
> machinery breaks at **3.3E-001** while the live census scores it at **0**.

**Both are NEW PRIMITIVES**, which is why the goal as posed is refuted while the question of a breaker stays **open in
principle, closed in practice**.

## Verdict

**REFUTED.** No existing AT quantity produces a non-flat weighting. The verdict is **computed** and has a **live
branch**: a phase-reading quantity with a non-uniform local factor, or a non-zero recipe-phase census, would move it to
**DERIVED**.
