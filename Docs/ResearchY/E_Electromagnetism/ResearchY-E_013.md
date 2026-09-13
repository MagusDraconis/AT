# ResearchY-E_013 - Flux Population Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_013 (permanent)
**Title:** What mechanism populates the allowed balanced flux sectors?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_013.md`
**Depends on:** E_012 (a single fluxon is forbidden, a balanced pair is allowed), E_011 (the flux quantum and its origin), E_010 (occupancy-derived curvature scales away), E_009 (the update rule's field strength is purely electric), E_008 (a gradient has no curvature), E_007 (the uniform flux), E_003 (the phase)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_013_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/FluxPopulationAudit.cs`

## The question

What mechanism **populates** the **allowed balanced** flux sectors, given E_012's two findings - a single fluxon is
**forbidden**, a balanced pair is **allowed**? Candidates: **occupancy rearrangement**, **defect pairs**, **boundary
conditions**, **actualization transitions**, **spectral transitions**. Requirements: **local**, **gauge compatible**,
**survives the continuum limit**, **no new primitive**. Measure: **sector population probability**, **sector
stability**, **flux lifetime**.

## The answer: **BOUNDARY for the population, with the mechanism's FORM DERIVED**

> The two levels are kept apart, as the D_028/D_040 rule requires: the **shape of any populating move is derived** -
> locality and E_012's constraint force a pair - while the **population itself is a boundary**, because no AT process
> performs it, nothing sizes it, and nothing times it.

## 1. The pair-creation law - the population probability is computed, not assumed

Increment **one** link phase on the lattice and the plaquette content changes **only in pairs**: for each orientation
containing that direction, the plaquette at the site and the plaquette before it move by **+delta** and **-delta**.

Measured over **three directions**, **three increments** and **four lattice sizes** (36 configurations):

| quantity | value |
|---|---|
| minimum plaquettes changed by any local move | **4** |
| local moves populating a **single** plaquette | **0** |
| signed sum of what a move changes | **0.000E+000** |
| P(single fluxon) | **0.0** |
| P(balanced pair) | **1.0** |

So E_012's forbidden case is not merely **disallowed** - it is **unreachable by any local move**.

## 2. Stability is exact - and it is not a lifetime

| quantity | value |
|---|---|
| pair amplitude at L = 8 / 16 / 32 / 64 | 3.141593 / 3.141593 / 3.141593 / 3.141593 |
| pure gauge field changes the content by | 6.939E-017 |
| single member decayed alone, residual | 3.141593 -> **forbidden** |
| pair annihilated, residual | 0.000E+000 -> **allowed** |

The pair is stable **as a pair**, its individual members are **pinned** by E_012's constraint, and its content
**cannot be gauged away**.

## 3. The lifetime is undefined, not merely long

| quantity | value |
|---|---|
| clock law sensitivity to the **flux** | 0.000E+000 |
| clock law sensitivity to the **organisation** (control) | 1.061E-001 |

The only AT law that could time anything is the clock law, and the flux is **invisible** to it. With no potential,
every flux value is degenerate: **a lifetime is an input, not a prediction**.

## 4. The activation is missing - three independent measurements

| measurement | value |
|---|---|
| the update rule's spatial part, max \|F_ij\| | 0.000E+000 |
| the update rule's time-like part, max \|F_0i\| | 1.424E-002 |
| AT members coupling a spectral index to a link phase | **0** |
| control: members touching a link field / a spectral index | 25 / non-zero |
| the occupancy's gradient limit, plaquette content | 6.939E-017 (a telescoping floor) |

## 5. The candidates

| candidate | status | basis |
|---|---|---|
| occupancy rearrangement | **REFUTED** | scales away as a^1.97; its constant-coupling limit is an exact gradient |
| **defect pairs** | **DERIVED** | forced, not chosen: 4 plaquettes changed, never 1, signed sum 0.000E+000 |
| boundary conditions | **BOUNDARY** | the amount and the initial pattern are unselected; the lifetime is undefined |
| actualization transitions | **REFUTED** | the update rule is purely electric (max \|F_ij\| = 0.000E+000) |
| spectral transitions | **REFUTED** | 0 members couple the two sectors, against 25 + non-zero controls |

## Verdict

**BOUNDARY.** AT derives the **shape** of any populating move - a pair, forced by locality - and derives nothing that
**makes** one, nothing that **sizes** it, and nothing that **times** it. This refines E_012 rather than repeating it:
that audit established that nothing creates a pair; this one establishes that **anything which ever does must be a
pair**, because the alternative is not forbidden but **unreachable**.
