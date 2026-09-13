# Y_E_007 Result - Field Strength Origin Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_007_Tests.cs`
**Status:** 7/7 PASSED
**Group total:** group E = **48/48 PASSED** (E_001 7 + E_002 6 + E_003 7 + E_004 6 + E_005 7 + E_006 8 + E_007 7)

## Verdict

**BOUNDARY** - the connection alone is **flat** (F = 0), the cube is the **first substrate that can carry a field
strength at all**, and the first non-trivial quantity is the **quantised plaquette flux** `2 pi n / L`.

## F != 0 ?

| quantity | value | status |
|---|---|---|
| commutator of the edge connection's differences | 1.73E-018 | **ZERO** - flat |
| pure gauge link field | 6.94E-017 | **ZERO** (telescoping) |
| curl versus holonomy (Abelian) | 0.00E+000 | **EQUAL** - the curvature *is* the curl |
| minimal non-trivial loop | **0.065449847** | **NON-ZERO** |
| gauge invariance | 1.11E-015 | **INVARIANT** |
| discrete Bianchi (flux / pure gauge) | 0.00E+000 / 8.33E-017 | **EXACT** |
| link periodicity for the minimal flux | 2.45E-016 | **PERIODIC** |

## The census that decides it

| | ring | cube |
|---|---|---|
| elementary plaquettes | **0** | **2 654 208** |
| independent cycles | 481 | **1 769 473** |

The ring has **no 2-cycle**, so F has nowhere to live; the cube has one plaquette per site and per orientation pair.

## The first non-trivial quantity

`F = 2 pi n / L`, smallest non-zero value **2 pi / 96 = 0.065449847** - exactly the phase quantum AT already has.
The number that is **pure gauge on the ring** is the **minimal real field strength on the cube**.

## Correction to E_005

E_005 reported `3L^2` = **27 648** plaquettes while its own cycle count used the correct `3L^3` edges - a
factor-of-`L` inconsistency. Corrected to **2 654 208** in the code, the test and eight texts; E_005's verdict and
all its other numbers are unchanged.

## Errors caught

The factor-of-`L` error in E_005 (found by recounting, not citing) and two syntax errors in my own first draft of the
core, caught by the compiler.
