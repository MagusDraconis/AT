# ResearchY-QM_005 - Laplacian Dispersion Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_005 (permanent)
**Title:** Can the native D96(1..6) Laplacian produce a Schrodinger-compatible dispersion in any physical regime?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `QM_ManyBody/ResearchY-QM_005.md`
**Depends on:** QM_004 (the fold at channel 11 through shell interference), G_016 (the ring C96(1..6)), QM_003 (the fold's origin in general)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_005_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/LaplacianDispersionAudit.cs`

## The question

Can the **native D96(1..6) Laplacian** produce a **Schrodinger-compatible dispersion in any physical regime**?
Compare the full shell set, the one-shell control, the reduced shell subsets and the continuum expansion; measure
**ω(k)**, the **group velocity**, the **fold position** and the **occupied-mode coverage**. Goal: determine whether the
fold is a consequence of the **six-shell geometry** or an **avoidable representation choice**.

## The answer

> **BOUNDARY - AND THE TWO HALVES SEPARATE CLEANLY. THE POWER LAW IS REPRESENTATION-INDEPENDENT AND THE FOLD IS NOT.**
> The audit enumerates **all 63 non-empty subsets** of the six shells. **Every** subset is Schrodinger-compatible at
> long wavelength (**DERIVED**); **62 of the 63 fold** (**BOUNDARY**: the fold is a choice, but each cure costs a
> different effective coefficient).

## 1. Half one, measured on all 63 subsets: the exponent is forced

| subset | D | power law |
|---|---|---|
| **NATIVE {1..6}** | **91** | **2.0000** |
| one shell {1} | 1 | 2.0000 |
| one shell {2} | 4 | 2.0000 |
| {1,2} | 5 | 2.0000 |
| one shell {6} | 36 | 2.0000 |
| {2,4,6} | 56 | 2.0000 |

**Every non-empty shell set is a Laplacian, and every term `2 − 2cos(rδ)` starts at `(rδ)²`** - so the exponent is
**2 for all 63 subsets**, verified as a limit (`ω` is exactly quadratic: halving δ divides it by four, to eight
digits) and the coefficient is the **second moment** `D = Σr²` for every subset. **Schrodinger compatibility in the
long-wavelength regime is a property of the OPERATOR'S ORDER, not of the shell choice.**

## 2. Half two: the fold arrives with the second shell

| shell | measured fold channel | closed form 48/r |
|---|---|---|
| **{1}** | **0 - never folds** | 48.0 |
| **{2}** | **25** | 24.0 |
| {3} | 17 | 16.0 |
| {4} | 13 | 12.0 |
| {5} | 10 | 9.6 |
| {6} | 9 | 8.0 |

A single shell's group velocity is `2r sin(rδ)`, **non-negative until rδ passes π** - i.e. until **channel 48/r** -
so the fold **arrives with the second shell** and **moves inward** as the shells grow. The measurement agrees with the
closed form to **exactly one channel past 48/r** in every case. **`{1}` alone never folds anywhere in the zone.**

## 3. The census

| subset size | subsets | folding | earliest fold | latest fold |
|---|---|---|---|---|
| 1 shell | 6 | **5** | 9 | 25 |
| 2 shells | 15 | 15 | 9 | **28** |
| 3 shells | 20 | 20 | 9 | 20 |
| 4 shells | 15 | 15 | 10 | 16 |
| 5 shells | 6 | 6 | 10 | 13 |
| **6 shells** | **1** | **1** | **11** | **11** |

**62 of 63 fold; exactly one never does** - the singleton `{1}`. **The latest fold any subset achieves is channel 28
(`{1,2}`)**, so the fold cannot be pushed out of the zone by any shell choice.

## 4. What the cure costs

| subset | D | fold | window | **occupied in window** | first failure |
|---|---|---|---|---|---|
| **NATIVE {1..6}** | **91** | **11** | 3 | **3 of 42** | 4 |
| {1,2} | 5 | 28 | 9 | 9 of 42 | 10 |
| {2,3} | 13 | 19 | 6 | 6 of 42 | 7 |
| {2,3,4} | 29 | 15 | 4 | 4 of 42 | 5 |
| **one shell {1}** | **1** | **none** | **17** | **16 of 42** | 18 |

**The best occupied coverage is 16 of 42 modes at the one-shell cure `{1}`, a factor of 5.33 better than the native
set's 3** - and it costs the effective coefficient, **D = 1 against the native 91**. The cure is a **different
generator with a different effective mass**, not a relabelling of the same one.

## 5. The continuum expansion agrees with the window

`ω = D k² − E k⁴` with `E = Σr⁴/12`:

| subset | D | E | k at 10 % | measured window |
|---|---|---|---|---|
| **NATIVE {1..6}** | 91 | **189.58** | **0.2191** | **3** |
| one shell {1} | 1 | 0.08 | **1.0954** | **17** |
| {1,2} | 5 | 1.42 | 0.5941 | 9 |

**The analytic estimate of where the quartic term takes 10 % of the quadratic predicts the measured window and its
ORDER**: the wider k-regime has the wider window. The six-shell set's quartic coefficient is **2400× the one-shell
coefficient**, which is precisely why its correspondence ends first.

## 6. A numerical defect in the audit's own first version

The dispersion was first computed as `2 − 2cos(rδ)` directly, and the **δ → 0 limit test failed** because that form
**loses digits to cancellation** - the exact regime the power-law fit reaches for. The audit now uses the **half-angle
identity** `2 − 2cos(rδ) = 4sin²(rδ/2)` and **measures the error it removes**:

| δ | safe `4sin²(rδ/2)` | direct `2 − 2cos` | relative error of the direct form |
|---|---|---|---|
| 1E-004 | 9.100E-007 | 9.100E-007 | 0.00 % |
| 1E-005 | 9.100E-009 | 9.100E-009 | 0.00 % |
| 1E-006 | 9.100E-011 | 9.100E-011 | 0.00 % |
| **1E-007** | **9.100E-013** | **9.099E-013** | **0.01 %** |

The audit's first prediction of the size of that error was **too pessimistic by an order of magnitude**, and the
**measured** value is what the test asserts.

## Verdict

**BOUNDARY.** The two halves of the question separate:

| question | answer | basis |
|---|---|---|
| is the power law forced? | **DERIVED** | every one of the 63 subsets gives exponent 2: Schrodinger compatibility at long wavelength follows from the operator's **order** |
| is the fold forced? | **BOUNDARY** | **62 fold, 1 never does** - a single shell `{r}` folds at channel 48/r, so the fold arrives with the **second** shell and the native fold at channel 11 is a property of **which shells interfere** |
| is avoiding the fold free? | **BOUNDARY** | the non-folding cure changes the coefficient from 91 to 1: a **different generator**, not a relabelling |

**The fold is a consequence of the shells that are SUMMED rather than of the six-shell geometry itself - and since
every cure changes the generator, it is an avoidable representation choice with a physical cost.** Schrodinger
compatibility, by contrast, is not a choice at all: it is what a Laplacian *is*.
