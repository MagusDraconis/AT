# ResearchY-E_005 - Propagation Origin Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_005 (permanent)
**Title:** What is the minimal missing ingredient that turns T1(3) into a propagating photon sector and T2(3) into a propagating graviton sector?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_005.md`
**Depends on:** E_004 (the representation is necessary and sufficient; the kinetic form is the shortfall), E_003 (the phase exists but lacks the spacetime index and the fluctuation), E_002 (the field equation is derivable from imported premises; no member computes a field strength), E_001 (EM dynamics declared as strings), G_033 (the cubic substrate is never instantiated), G_042 (polarisation equality is the Hodge equality)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_005_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/PropagationOriginAudit.cs`

## The question

What is the **minimal missing ingredient** that turns T1(3) into a **propagating photon** sector and T2(3) into a
**propagating graviton** sector? Separate **representation / kinematics / dynamics / gauge**, determine the
**first missing step**, and locate the **unique bottleneck shared by photon and graviton**.

## The answer: **BOUNDARY - the first failing layer is KINEMATICS, and the bottleneck is ONE object**

> **A first-order derivative that carries a DIRECTION INDEX - a field-valued connection - together with the
> directions it acts along.**

## 1. The four layers, run in order

| layer | photon | graviton | status |
|---|---|---|---|
| representation | T1, 3 states, multiplicity 1 | E + T2 = 2 + 3 = 5, multiplicity 1 | **SATISFIED** - granted, as in E_003/E_004 |
| kinematics | needs a derivative with a **direction index** | same derivative; same 3-direction index space | **MISSING - the FIRST failure** |
| dynamics | needs a field strength to build a kinetic form from | needs the linearised curvature | MISSING - **BLOCKED** by kinematics |
| gauge | quotient by the image of the derivative | quotient by symmetrised derivatives | MISSING - **BLOCKED** by kinematics |

**Representation content, computed:** photon T1 (dim 3 x 1); graviton E (dim 2 x 1) + T2 (dim 3 x 1); the single
ring tops out at **2** and can host neither.

## 2. Localisation one - the index spaces collapse at ONE direction

| directions | vector | antisym. (field strength) | traceless sym. (graviton) | verdict |
|---|---|---|---|---|
| 1 | 1 | **0** | **0** | field strength **impossible**, no graviton index space |
| 2 | 2 | 1 | 2 | a field strength fits, the two sectors do not |
| 3 | 3 | 3 | 5 | both sectors' index spaces exist |

At a direction rank of 1 the **antisymmetric square is exactly zero** and the **traceless symmetric is exactly
zero**. Neither sector has anywhere to put a field strength; the two sectors as they stand need exactly **3**.

## 3. Localisation two - the substrate's derivative is ONE-DIRECTIONAL

AT's radius-6 Laplacian **is** a sum of six squares of first-order differences:

`L = sum_{r=1..6} D_r^T D_r`, first-row residual **0.000E+000** (a circulant is determined by its first row, so
this *is* the operator identity).

So **first-order operators exist** - but all six strides lie along a single line:

| | |
|---|---|
| direction rank of D96 | **1** |
| direction rank of D96^3 | **3** |
| directions a vector index needs | **3** |
| ring independent loops (E - V + 1) | **481** |
| cube independent loops | **1 769 473** |
| cube elementary plaquettes | **2 654 208** |

Only the cube can carry a **local** field strength rather than a single global number.

## 4. Localisation three - the built-in connection is EXACTLY PURE GAUGE

AT *does* have a phase on links (E_003 established this). This audit computes what it is worth:

| | |
|---|---|
| built-in step 2*pi/96 | **0.065449847** |
| holonomy around the ring | **6.283185** = 2*pi, the identity |
| holonomy modulo 2*pi | **0.000E+000** |
| pure-gauge conjugation residual | **2.45E-016** |

Conjugating the covariant difference by the site-dependent phase `diag(exp(-i theta i))` returns the **ordinary
difference**, and it is periodic **precisely because** the holonomy is one whole turn. The phase is therefore
**removable**: its field strength is identically zero and the only gauge-invariant content a phase on a closed line
could have - the total holonomy - is exactly the residue that vanishes here. **There is nothing in it to
propagate.**

And a phase with a residue is exactly what a field strength *would* be made of (`residue(2*pi) = 0`,
`residue(2*pi + 0.5) = 0.5`).

## 5. Dynamics and gauge are missing but BLOCKED

A kinetic form is a functional of a **field strength**; a gauge orbit is the **image of the derivative**. With no
derivative acting on the field, neither can even be written. So they are not independently missing - they are
**downstream**.

## 6. The bottleneck is unique - and two rivals are refuted

| candidate | status | basis (computed) |
|---|---|---|
| the representation | **REFUTED** | both sectors exist as irreps (3 and 5); it is complete, so it is not what is missing |
| the exterior complex (`d`, `d^2 = 0`) | **REFUTED** | the photon's field strength is antisymmetric (**dim 3**) while the graviton's field is traceless **symmetric** (**dim 5**) - different irreps, so the graviton is **not a form** and `d` alone cannot serve both |
| a first-order derivative carrying the direction index | **THE UNIQUE BOTTLENECK** | one object supplies both index spaces, both field strengths, both gauge orbits, both kinetic terms; without it Lambda^2(1) = 0 |

**The shared count proves the sharing - one rule, two sectors:**

| sector | components | gauge parameter | orbit | physical |
|---|---|---|---|---|
| photon | 3 | a scalar (1) | 1 | **2** |
| graviton | 5 | a vector (3) | 3 | **2** |

## 7. Refinements of the predecessors

- **E_003** named the missing primitive as *a phase on spacetime links with its own dynamics* - the spacetime index
  and the fluctuation. **E_005 agrees and locates it:** the spacetime index **is** the direction index, and it is
  the **kinematics** layer, i.e. the first missing step. This audit adds the computed reason the existing phase
  cannot stand in for it (it is exactly pure gauge).
- **E_004** located the shortfall at *the choice of kinetic form* that takes three states to two. **E_005 refines
  that to the second step:** a kinetic form needs a field strength, and there is none until a first-order
  derivative carrying the direction index exists. E_004's finding that the representation supplies no gauge orbit
  is reproduced here. **No reclassification** - both remain BOUNDARY.

## 8. Errors this audit caught while running

1. **The remainder operator is not a holonomy reduction.** `96 x (2*pi/96)` can land a hair *below* `2*pi` in
   floating point, and `total % 2*pi` then returns the **whole turn** instead of nothing. Replaced by counting
   turns (`(total/2*pi - round(total/2*pi)) * 2*pi`). The test caught it, not the derivation.
2. **A real exponential where a complex phase was needed.** The gauge conjugation was first written with
   `Math.Exp(i*theta)`, which is the *real* exponential `e^{0.0654i}` - growing to 534 by the end of the ring.
   Fixed to `Complex.Exp(new Complex(0, ...))`; the residual fell to **2.45E-016**, and the value it fell *from*
   is recorded here rather than deleted.
3. **A shared helper cannot take short vectors.** `PhotonOntologyAudit.RankOf` indexes eight columns
   unconditionally, so it throws on a one-component direction vector. This audit ranks locally and records the
   observation rather than silently widening the input.

## Success criterion

> Locate the unique bottleneck shared by photon and graviton.

**Located: the first-order directional derivative**, at the **kinematics** layer - the first missing step. The
verdict is **BOUNDARY** because the location and the uniqueness are *computed here*, while the ingredient itself
must be **supplied**: AT's first-order structure is one-directional (rank 1) and its built-in connection is exactly
pure gauge.

## Resolved by E_006 (no reclassification)

**E_006 - Connection Origin Audit** (DERIVED) takes this audit's bottleneck and asks what AT already has that could be
it. The answer: the **D96^3 edge connection**, the tensor product of the ring's own difference operators - local,
directional (rank **3**), first-order, and acting on both sectors (antisymmetric part = T1, symmetric part =
A1 + E + T2). So the ingredient this audit said "has to be supplied" **need not be invented - no new primitive is
required** - but it does have to be **built and coupled**, which AT has never done (a live scan finds 0
product-lattice constructions; 0 members compute a field strength). **E_005 remains BOUNDARY**; its bottleneck is
resolved at the kinematic level, not reclassified.

## Corrected by E_007 (arithmetic, no verdict change)

This audit reported **27 648** elementary plaquettes on the cube as `3L^2`. That is wrong by a factor of `L`: there is
one elementary plaquette per site **and per orientation pair**, so the count is `3L^3` = **2 654 208** at L = 96. The
audit's own cycle count already used the correct edge count `3L^3`, so the two numbers in this document disagreed with
each other. **ResearchY-E_007** caught it by recounting the cycles independently and has corrected the code, the test
and every text that quoted the figure. **The verdict and every other number are unchanged** - and the corrected value
strengthens rather than weakens the point, since the cube's plaquette supply is a thousand times the ring's (zero).

