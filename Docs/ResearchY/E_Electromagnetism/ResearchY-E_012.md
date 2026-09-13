# ResearchY-E_012 - Flux Excitation Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_012 (permanent)
**Title:** What AT mechanism populates a non-trivial flux sector?
**Status:** COMPLETE
**File:** `E_Electromagnetism/ResearchY-E_012.md`
**Depends on:** E_011 (the origin of the flux quantum; the closed-cycle holonomy; the sector label), E_010 (no occupancy-derived curvature survives refinement), E_009 (the coupling is the clock law; the time-like component only), E_008 (a winding is a gradient, so it has no curvature), E_007 (the uniform flux and its quantum)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_012_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/FluxExcitationAudit.cs`

## The question

What AT **mechanism** populates a non-trivial **flux sector**? Candidates: **occupancy defects**, **topological
defects**, **winding sectors**, **boundary conditions**, **actualization transitions**. Requirements: **create
F != 0**, **survive the continuum limit**, **no new primitive**.

## The answer: **BOUNDARY - nothing inside AT populates the sector, and the constraint on what may populate it is exact**

> E_011 found the **origin** of the flux quantum. This audit asks what ever puts a configuration **into** a non-trivial
> one - and answers by elimination, with a measurement behind each elimination.

## 1. The organisation cannot do it - re-measured, not cited

The occupancy route `A_mu = h(rho) Delta_mu rho` loses its curvature as the substrate is refined:

| L | occupancy route max \|F\| |
|---|---|
| 8 | 1.691E-002 |
| 16 | 4.442E-003 |
| 32 | 1.124E-003 |
| 64 | 2.820E-004 |

The fitted exponent is **a^2.00**. The occupancy can **move flux around**; it cannot **put any in**.

## 2. The flux content is an ASSIGNMENT with a GLOBAL CONSTRAINT

Summing plaquette holonomies over the **whole torus** counts every link **twice with opposite signs**, so the reduces
fluxes must sum to a multiple of `2 pi` - measured at **0.00E+000** for a uniform field.

The consequence is concrete:

| pattern | constraint residual | verdict |
|---|---|---|
| **single half-turn flux** | 3.141593 | **FORBIDDEN** |
| **balanced pair (pi, -pi)** | 0.000E+000 | **ALLOWED** |

A single fluxon is forbidden **by the shape of the substrate**, and the smallest non-trivial configuration is a
**balanced pair**. The constraint is a **global balance**: a half-turn must be compensated by a partner *somewhere* on
the torus, not necessarily on the same slice - a slice's product is free, since a slice's boundary is a
**non-contractible cycle** (measured **2.000000** away from the identity with one half-turn on it).

## 3. The pair meets the requirement the occupancy route fails

| L | balanced pair max \|F\| | occupancy route max \|F\| |
|---|---|---|
| 8 | 3.141593 | 1.691E-002 |
| 16 | 3.141593 | 4.442E-003 |
| 32 | 3.141593 | 1.124E-003 |
| 64 | 3.141593 | 2.820E-004 |

The pair's amplitude is **pi at every lattice size** - it does **not** scale away. So a non-trivial flux with a
surviving amplitude **exists**; what does not exist is anything inside AT that **creates** it.

## 4. The candidates

| candidate | status | basis |
|---|---|---|
| occupancy defects | **REFUTED** | the route falls as a^2.00; it moves flux, it does not add it |
| topological defects | **BOUNDARY** | a balanced pair is legitimate and survives, but nothing creates one |
| winding sectors | **REFUTED** | a winding is a gradient: zero curvature (E_008), whole-turn closed holonomy (E_011) |
| boundary conditions | **BOUNDARY** | the answer: the sector is populated by an ASSIGNMENT under the global constraint |
| actualization transitions | **REFUTED** | the process supplies the time-like component only (E_009) |

## 5. A claim withdrawn while doing this

A first draft held that "the product of plaquette holonomies around a single **slice** is the identity, so a single
half-turn flux is impossible". The identity is true for the **whole torus** and **false for a slice** - a slice's
boundary is a non-contractible **cycle**, so its product is that cycle's holonomy. The constraint that forbids the
single fluxon is the **whole-torus sum**, and that is what the audit measures. The first version of the probe
"confirms" the wrong claim because it lossy-folded the configuration at the seam; the audit records the correction
rather than the claim.

## Verdict

**BOUNDARY.** The sector is populated by an **assignment**, subject to a **global constraint** that forbids the single
fluxon; a **balanced pair** is the minimal non-trivial assignment and its amplitude survives refinement, but nothing
inside AT creates one. The assignment must come from outside the configurations AT derives - exactly the conclusion
E_011 reached from the **origin** side.
