# Y_E_005 Result - Propagation Origin Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_005_Tests.cs`
**Status:** 7/7 PASSED
**Group total:** group E = **33/33 PASSED** (E_001 7 + E_002 6 + E_003 7 + E_004 6 + E_005 7)

## Verdict

**BOUNDARY** - the first failing layer is **KINEMATICS**, and the bottleneck is **ONE object**: a first-order
derivative that carries a **direction index** (a field-valued connection).

## The ladder

| layer | photon | graviton | status |
|---|---|---|---|
| representation | T1, 3 x 1 | E + T2 = 2 + 3 = 5 | **SATISFIED** (granted) |
| kinematics | direction index needed | same 3-direction index space | **MISSING - FIRST** |
| dynamics | field strength needed | linearised curvature needed | MISSING - blocked |
| gauge | image of the derivative | symmetrised derivatives | MISSING - blocked |

## Three localisations that agree

1. **The index spaces collapse.** directions 1 / 2 / 3 -> antisymmetric square **0** / 1 / 3 and traceless
   symmetric **0** / 2 / 5. At one direction there is nowhere to put a field strength.
2. **The substrate's derivative is one-directional.** `L = sum_r D_r^T D_r` holds exactly (residual
   **0.000E+000**), so first-order operators exist - but the direction rank is **1** (ring) against **3** needed
   (cube). Loops: ring **481**, cube **1 769 473**, elementary plaquettes **27 648**.
3. **The built-in connection is exactly pure gauge.** Step 2*pi/96 = **0.065449847**; holonomy **6.283185** =
   2*pi = the identity; residue **0.000E+000**; gauge conjugation residual **2.45E-016**. Nothing to propagate.

## The shared count

| sector | components | orbit | physical |
|---|---|---|---|
| photon | 3 | 1 | **2** |
| graviton | 5 | 3 | **2** |

## Two rivals refuted

- **the representation** - complete (3 and 5); not what is missing.
- **the exterior complex** - the graviton is not a form: antisymmetric dim 3 versus symmetric dim 5.

## Errors the audit caught on itself

`total % 2*pi` returns a whole turn when the value lands just below the modulus; a real exponential was used where
a complex phase was needed (residual 534 before the fix); and the shared `RankOf` helper cannot take
one-component vectors. All three recorded in the doc, none hidden.
