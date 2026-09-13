# ResearchY-E_010 - Magnetic Sector Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_010 (permanent)
**Title:** What AT structure can generate a non-zero F_ij?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_010.md`
**Depends on:** E_009 (the derived coupling; the purely-electric result and its stated boundary), E_008 (F needs a non-difference coupling), E_007 (the plaquette, the curvature and the uniform flux), E_006 (the tensor square), E_004 (the standard for telling a physical field from a finite-size artefact)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_010_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/MagneticSectorAudit.cs`

## The question

What AT structure can generate a non-zero **F_ij**? Requirements: **local**, **gauge-compatible**, **no new
primitive**, **acting on T1/T2**. Determine: **can magnetic components emerge from occupancy dynamics alone?**

## The answer: **REFUTED - no, and the measurement that decides it is a SCALING one**

## 1. E_009's purely-electric result was a choice, not a theorem

E_009 set the spatial components of the coupling to zero and said so. Completing the coupling **covariantly** - the
only vector a scalar can build - switches the magnetic sector on:

| configuration | max \|F_23\| |
|---|---|
| `A_mu = h(rho) Delta_mu rho`, **derived** coupling (E_009) | **1.422E-003** |
| the same form with the **linear** coupling | **0.000E+000** |
| electric field of the same configuration | 2.203E-003 |

**The magnetic sector exists precisely because the clock law is nonlinear** - a linear coupling telescopes to nothing.

## 2. But it is a finite-size artefact

Refining the lattice at a **fixed physical profile**:

| form | L = 8 | L = 16 | L = 32 | L = 64 | fitted |
|---|---|---|---|---|---|
| `A_mu = h(rho) Delta_mu rho` | 1.42E-003 | 2.06E-004 | 2.61E-005 | 3.32E-006 | **a^2.92** |
| `A_mu = h(rho(x))` (site-local) | 9.91E-003 | 5.05E-003 | 2.55E-003 | 1.27E-003 | **a^0.99** |

**Both vanish in the physical limit.** This programme has already applied exactly this standard once: **E_004** called
the spectral gap a finite-size artefact because `mu_min n^2` settles to a constant, so the gap closes as the substrate
is refined. By the same standard the occupancy-derived magnetic curvature is a **discretisation field** - a real
number on the substrate's own lattice and nothing at all in the physical limit.

## 3. What survives instead

E_007's uniform flux, with the physical strength **held fixed** (turns proportional to the lattice size):

| L | 8 | 16 | 32 | 64 |
|---|---|---|---|---|
| max \|F_23\| | 0.785398 | 0.785398 | 0.785398 | 0.785398 |

It **does not scale away** (`A_2` proportional to the coordinate is not built from rho at all). It is local
(support 2), **gauge-compatible** (residual 1.78E-015), satisfies **Bianchi**, needs **no new primitive**, and acts on
**T1 and T2** through E_006's tensor square - but it is **independently assigned**, which is exactly the non-gradient
structure E_008 identified for the field strength in general.

## 4. The findings

| question | answer |
|---|---|
| does the covariant completion turn F_ij on? | **yes, on a finite lattice** - 1.422E-003 with the derived coupling, 0.000E+000 for the linear one |
| does it survive refining the lattice? | **no** - a^2.92 and a^0.99 at a fixed physical profile |
| does an independently assigned flux survive? | **yes** - 0.785398 at every lattice size |
| **so can occupancy dynamics alone generate a magnetic sector?** | **no** |

## 5. Errors this audit caught

1. **E_007's flux is an ELECTRIC field under this audit's convention.** Direction 1 is the clock's direction (E_009's
   convention), so (2,3) is the magnetic pair - and E_007's `A_1 = f y` sits in (1,2). Measuring it as "magnetic"
   returned **0.000E+000**, correctly. The magnetic analogue is `A_2 = f z`, and the distinction is now in the code.
2. **The wrap belongs on the SUM, not on each term.** A linear flux is periodic only as a *phase*, so its raw
   difference at the seam is not its curvature: the unwrapped version read the seam artefact (**5.497787** = 2 pi -
   f/1), and wrapping each term separately moved the artefact instead of removing it. Wrapping the **sum** gives
   0.785398 uniformly - the correct Abelian holonomy.
3. **A cross-audit check that was not one.** The first version compared E_010's own profile against E_009's published
   electric number; the profiles differ in their last term by construction (they must be refinable), so the check now
   runs on **E_009's own organisation** and reproduces its published 1.424E-002 exactly.

## Success criterion

> Can magnetic components emerge from occupancy dynamics alone?

**No.** The occupancy supplies the **electric** half exactly (E_009 derived it), and every covariant coupling built
from it produces a magnetic curvature that **disappears in the physical limit**. A magnetic sector requires a spatial
link field that is **not a function of the organisation** - the minimal example being the one E_007 already exhibited.
