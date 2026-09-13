# Y_E_010 Result - Magnetic Sector Audit

**Suite:** `AT.Tests/ResearchY/E_Electromagnetism/Y_E_010_Tests.cs`
**Status:** 7/7 PASSED
**Group total:** group E = **69/69 PASSED** (E_001-E_009 = 62, plus E_010 7)

## Verdict

**REFUTED** - magnetic components do **not** emerge from occupancy dynamics alone, and the measurement that decides
it is a **scaling** one rather than a value.

## The covariant completion

| configuration | max \|F_23\| |
|---|---|
| `A_mu = h(rho) Delta_mu rho`, derived coupling (E_009) | **1.422E-003** |
| the same form, **linear** coupling (the gradient) | **0.000E+000** |
| electric field of the same configuration | 2.203E-003 |

The magnetic sector exists **because the clock law is nonlinear**.

## The decisive measurement

| form | L = 8 | L = 16 | L = 32 | L = 64 | F ~ |
|---|---|---|---|---|---|
| `A_mu = h(rho) Delta_mu rho` | 1.42E-003 | 2.06E-004 | 2.61E-005 | 3.32E-006 | **a^2.92** |
| `A_mu = h(rho(x))` local | 9.91E-003 | 5.05E-003 | 2.55E-003 | 1.27E-003 | **a^0.99** |

Both **vanish in the physical limit** - the same diagnosis E_004 gave the spectral gap.

## What survives

E_007's uniform flux with the physical strength held fixed: **0.785398 at every L = 8, 16, 32, 64**. Local (support
2), gauge-compatible (**1.78E-015**), Bianchi-consistent, no new primitive, acting on T1/T2 - but **independently
assigned**.

## Errors caught

E_007's flux is an **electric** field under this convention (its `A_1 = f y` sits in the (1,2) pair); the wrap belongs
on the **sum** of the four link phases, not on each term (the first two versions read the seam artefact **5.497787**
or a branch artefact); and a cross-audit check compared two different profiles, so it now runs on E_009's own
organisation and reproduces its published **1.424E-002**.
