# ResearchY-G_074 - Clock Law Necessity Audit

**Program:** ResearchY - Gravity Source Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_074 (permanent)
**Title:** Does any surviving AT result require dtau/dt = rho^(1/d) specifically, or only a monotonic function of rho?
**Status:** COMPLETE
**Date:** 2026-09-17
**File:** `G_GravitySource/ResearchY-G_074.md`
**Depends on:** G_017/G_019/G_020 (the clock law), G_004/G_009 (the calibration and GPS agreement), G_035 (the time sector is clock-only), G_068 (the AT-vs-GR redshift), G_072 (the observing programme that could resolve a form difference)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_074_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/ClockLawNecessityAudit.cs`

## The question

**Does any surviving AT result require dtau/dt = rho^(1/d) specifically, or only a monotonic function of rho?**
Candidates: **rho^(1/d)**, **ln(rho)**, **exp(rho)** and **Pade forms**. Recompute **g00**, the **redshift** and the
**compact-star prediction**. Output **UNIQUE / BOUNDARY / REFUTED**.

## The answer

> **BOUNDARY - MONOTONICITY DRIVES THE SHAPE AND THE MAP DRIVES THE NUMBERS. And the sharpest measured form of the
> answer: the recorded second-order constraint admits exactly TWO of the four candidates, and they are the SAME LAW -
> the exponential family, written once as a power of the density with a logarithmic map and once as an exponential of
> the density with a linear map. The surviving sector therefore fixes an EQUIVALENCE CLASS, not a formula.**

## 1. The form is not identifiable from the shape, and the identity shows why

| law | monotone | positive | rate x^1 | x^1 in 1+z | **x^2 in 1+z** |
|---|---|---|---|---|---|
| **rho^(1/d)** | yes | yes | 1.0 | -1.0 | **+0.5** |
| **ln(rho)** | yes | yes | 1.0 | -1.0 | **+1.0** |
| **exp(rho), linear map** | yes | yes | 1.0 | -1.0 | **+0.5** |
| **Pade [1/1] of rho^(1/d)** | **no** | **no** | 1.0 | -1.0 | **-2.5** |

**Every candidate is monotone on the physical range, and every one whose map is fitted to the same first order passes
it identically** - so the SHAPE - a positive rate whose ratio between two densities is the redshift - names no
functional form. **The redshift's first coefficient is -1.0 for all four, because it is the negative of the rate's.**

**And the candidates are not four theories.** The audit measures the **reparametrisation identity**: **exp(rho) with a
linear map IS rho^(1/d) with a logarithmic map**, to **0.000E+000**. What distinguishes the candidates is the **MAP**
that converts a density into a potential, and **the map is what the surviving audits take as an input.**

## 2. The requirement table

| requirement | survivors | which |
|---|---|---|
| positive and monotone | **4 of 4** | all |
| the redshift is a ratio of rates | **4 of 4** | all |
| the rate's first order is 1 + x | **4 of 4** | all |
| **the recorded second order** | **2 of 4** | **rho^(1/d) and exp(rho) - the same law** |

**Every shape requirement admits everything; only the second order discriminates, and what it discriminates is not a
formula but a FAMILY.** The Pade form fails positivity and monotonicity at its **pole at rho = 5/2**, inside the
physical range, and `ln(rho)` turns negative **below unit density** - both measured, both reported.

## 3. The compact-star prediction - where the form becomes physical

| law | 1 + z at the compact object | shift from the power law |
|---|---|---|
| **rho^(1/d)** | **1.280180559309** | - |
| **exp(rho), linear map** | **1.280180559309** | **2.220E-016** |
| **ln(rho)** | **1.328023241157** | **+4.784E-002** |
| GR | 1.405807032086 | - |

**The two survivors of the second-order constraint are numerically identical at the compact object** (2.220E-016
apart), which is the equivalence class confirmed dynamically rather than algebraically. **And the log law differs from
them by 4.784E-002 in 1+z - a shift comparable to a third of the AT-vs-GR separation of 1.256E-001 at the same object,
so the observing programme G_072 describes could resolve the clock law's form at the same time as it decides AT vs
GR.** The clock law is therefore a **decidable input** rather than a convention, once the object is measured.

## 4. Defects in the audit's own first version

1. **The maps were not normalised to a common first order**, so my first table compared my own parametrisations
   instead of the laws: `ln(rho)` was given the map `e^(1+2x)` and hence a rate slope of 2.
2. **The reparametrisation identity was mis-stated**, comparing `exp(e^(dx) - 1)` against `e^x` - which is not the
   identity at all - and returning a residual of **1.054** where the true residual is **0.000E+000**. The identity is
   between the exponential with a **linear** map and the power law with a **logarithmic** one.
3. **The first-order requirement was tested on the REDSHIFT, whose first coefficient is -1.0**, so every candidate
   appeared to fail it. The requirement belongs to the **RATE**.
4. **I asserted that the pairwise monotonicity check cannot see a pole. The measurement refused it** - both the
   pairwise check and the sign check catch the Pade form's pole, and my "correction" had to be corrected in turn.

## Where it stands

**Monotonicity carries the structure; the map carries the numbers; and the map is an input.** The surviving sector
needs a positive, monotone rate at densities at or above the vacuum value - which admits the exponential family and
`ln(rho)` - and the recorded second order then separates them, admitting only the exponential family, **whose two
descriptions are one law**.

**So the answer to the question as asked is: NO surviving result requires `rho^(1/d)` BY NAME, and a surviving result
DOES require it BY CONTENT** - the exponential family, uniquely, up to the choice of map. **UNIQUE would overstate it
(the map is an input), REFUTED would understate it (the second order really does exclude the log and the rational
forms), and BOUNDARY is what the measurement supports.**
