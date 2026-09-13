# ResearchY-E_007 - Field Strength Origin Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** E - Electromagnetism
**ID:** ResearchY-E_007 (permanent)
**Title:** Can the D96^3 edge connection produce a non-zero field strength?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `E_Electromagnetism/ResearchY-E_007.md`
**Depends on:** E_006 (the edge connection), E_005 (the direction index; the pure-gauge phase), E_002 (the field equation is derivable from imported premises), G_042 (the antisymmetric square is the vector irrep at d = 3), G_033 (the cube is never instantiated)
**Test suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_007_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/FieldStrengthOriginAudit.cs`

## The question

Can the **D96^3 edge connection** that E_006 derived produce a **non-zero field strength**? Construct **closed
plaquette loops**, a **discrete curvature** and a **discrete curl**; measure **F != 0**; compare **pure gauge**
against a **non-trivial loop**; detect the **first non-trivial field quantity**.

## The answer: **BOUNDARY - both halves of the answer are exact**

> **The connection alone produces nothing (it is flat), and the cube is the first substrate on which anything can
> exist at all. The second half yields the first non-trivial field quantity: the quantised plaquette flux
> `2 pi n / L`, whose smallest non-zero value is `2 pi / 96 = 0.065449847`.**

## 1. The connection does not produce a field strength by itself

E_006's edge connection is a product of lattice **differences**, and differences **commute** on a uniform lattice:

| | |
|---|---|
| commutator of differences (full torus, sampled) | **1.73E-018** |
| field strength of a gradient link field | **6.94E-017** |

The link-language statement is the same: the connection's own content is a **gradient**, and a gradient link field
has an exactly vanishing plaquette holonomy because the four contributions to the closed loop **telescope**. **The
edge connection is flat.**

## 2. But the cube is the first substrate that can carry a field strength

A field strength is the holonomy of a **closed two-dimensional loop**, so it needs **two independent directions**.

| | ring (D96) | cube (D96^3) |
|---|---|---|
| sites | 96 | **884 736** |
| edges | 576 | **2 654 208** |
| **elementary plaquettes** | **0** | **2 654 208** |
| independent cycles `E - V + 1` | 481 | **1 769 473** |

**The ring has no plaquette at all** - it is one-dimensional whatever its link count - which is E_005's
"the antisymmetric square vanishes at one direction" stated concretely.

### Correction to E_005

E_005 reported **27 648** plaquettes as `3L^2`. That is wrong by a factor of `L`: one elementary plaquette exists
per site **and per orientation pair**, so the count is `3L^3` = **2 654 208**. E_005's own cycle count already used
the correct edge count `3L^3`, so the two numbers in that document disagreed with each other. **This audit has
corrected the code, the test and every text that quoted the figure.** The corrected value strengthens the point
rather than weakening it: the cube's plaquette supply is a thousand times the ring's supply of zero.

## 3. F != 0 - the apparatus is exact, not approximate

| quantity | value | status |
|---|---|---|
| commutator of the edge connection's differences | 1.73E-018 | **ZERO** - the connection is flat |
| field strength of a pure gauge link field | 6.94E-017 | **ZERO** exactly (telescoping) |
| curl versus holonomy (Abelian) | 0.00E+000 | **EQUAL** - the curvature *is* the antisymmetrised difference |
| field strength of the minimal non-trivial loop | **0.065449847** | **NON-ZERO** - the first non-trivial quantity |
| gauge invariance of the curvature | 1.11E-015 | **INVARIANT** |
| discrete Bianchi identity (flux field) | 0.00E+000 | **EXACT** |
| discrete Bianchi identity (pure gauge) | 8.33E-017 | **EXACT** |
| link periodicity for the minimal flux | 2.45E-016 | **PERIODIC** - hence the quantisation |

Three of these are worth naming. **The curvature IS the curl** - for an Abelian connection the four link phases of a
closed loop simply add, so the discrete curvature is the antisymmetrised difference *exactly* rather than to leading
order. **The curvature is gauge invariant** under a site-dependent gauge transformation. And **discrete Bianchi
holds exactly**, because the twelve link contributions to the six faces of an elementary cube cancel in pairs.

## 4. The first non-trivial field quantity

Take a link field linear in the transverse coordinate, `A_1 = f y`. Every (1,2) plaquette then carries the same
curvature `-f`. The requirement that the link be **a phase on a closed torus**, `exp(i A(L)) = exp(i A(0))`, forces
`f L` to be a whole turn:

> `f = 2 pi n / L`, so the smallest non-zero field strength the substrate can carry is
> **`2 pi / 96 = 0.065449847`**.

That is **exactly the phase quantum AT already has**. The number E_005 found to be **pure gauge on the ring** is, on
the cube, the **minimal real field strength**.

## 5. Verdict: BOUNDARY - for a specific reason

Everything that **detects** a field strength is derived here: the plaquette, the curvature, the curl, the
quantisation, gauge invariance and Bianchi - and the first non-trivial quantity is exhibited with its value. But the
**connection does not produce it**: it takes a chosen fluctuation, and nothing in AT yet selects one. That is the
**dynamics** layer E_005 located and E_006 left open.

| layer | status |
|---|---|
| kinematics | **DERIVED** (E_006) - and now shown to be **flat** on its own |
| field strength (detection) | **DERIVED HERE** - plaquette, curvature, curl, quantisation, gauge invariance, Bianchi |
| dynamics | **STILL MISSING** - nothing selects the fluctuation |

## 6. Errors this audit caught

1. **A factor-of-`L` error in E_005**, found by recounting the cycles independently rather than citing them (see
   section 2). Corrected in the code, the test and eight texts.
2. **Two syntax errors in my first draft** of the core (`Func<int,int,int,double>` invoked with one argument, and a
   leftover unused local function) - caught by the compiler, not by review.

## Success criterion

> Detect the first non-trivial field quantity.

**Detected: the quantised plaquette flux `F = 2 pi n / L`**, smallest non-zero value **0.065449847**, gauge
invariant, Bianchi-consistent, and non-zero - but requiring a fluctuation the connection does not supply. Verdict
**BOUNDARY**.

## 7. Rule 11, for the third time - and now fixed at its root

Naming a member `PureGaugeFieldStrength` broke **E_006's** test without touching it: E_002's scanner is a regex over
signatures (`public static (double|double[]|float|int) ... (FieldStrength|DivergenceF|Fmunu) ...`), so any audit that
names a member after the thing being counted changes the count of *how many AT members compute a field strength* -
the reading went from 0 to 1. That is the third instance of the same defect in three audits (E_006's first helper had
the same problem, and E_006's own product-lattice scan counted its own token list).

**Fixed at the root rather than patched per file:** E_002's scanner now excludes every file whose name ends in
`Audit.cs`, because the audits are the *apparatus* that measures AT, not part of what is measured. The count is now
immune to what the apparatus decides to call its own members - which is what rule 11 exists to guarantee.

