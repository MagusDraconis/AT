# ResearchY-G_045 — Observed vs Hidden Dimension Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_045 (permanent)
**Title:** Can the apparent 3D world emerge as a projection of a higher-dimensional actualization space?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_045.md`
**Depends on:** G_039 (the ring's 45 levels and the reachable room 51), G_033 (the cube's 16080 distinct levels), G_040 (orbitals = 49 at d = 1), G_043 (orbitals = C(48 + d, d); the monotone growth families), G_041 (the ladder D96^d)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_045_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ObservedHiddenDimensionAudit.cs`

## The question

Can the apparent 3D world emerge as a **projection** of a higher-dimensional **actualization space**?
Compared: the **3D host space** against the **N-dimensional state space**. Measured: **observable
dimension**, **spectral dimension**, **information dimension**, **attractor dimension**.

## The answer: **REFUTED — the two "dimensions" are different indices, and the one that could hide is measured exactly**

## 0. The ground, recomputed rather than cited

The whole `d`-torus spectrum is rebuilt here, and three numbers from three other audits fall out of it:

| d | 96^d modes | distinct levels A₀ | reachable room 96^d − A₀ | state space |
|---|---|---|---|---|
| 1 | 96 | **45** | **51** | 95 |
| 2 | 9 216 | 1 032 | 8 184 | 9 215 |
| 3 | 884 736 | **16 080** | **868 656** | 884 735 |

`A₀(1) = 45` and the room `51` are **G_039's** numbers; `A₀(3) = 16 080` is **G_033's** robust level
count — which in turn fixes the reachable room at `d = 3` to **868 656**, the figure project memory
quotes. **Three cross-audit checks, all reproduced rather than assumed.**

## 1. The four measured dimensions

| family | d = 1 | d = 2 | d = 3 | verdict |
|---|---|---|---|---|
| **observable** (families) | 48 | 1 224 | 20 824 | NEITHER 3 NOR THE STATE SPACE — resolvable, not hidden |
| **spectral** (density of states) | 1.261 | 2.283 | 2.986 | **UNRELIABLE** — biased on a finite substrate |
| **spectral** (the gap) | 0.3863509 | 0.3863509 | 0.3863509 | **CANNOT REVEAL d** — identical at every d |
| **information** (box counting) | 1.000 | 2.000 | 3.000 | **EXACTLY d** — a hidden direction would be visible |
| **attractor / reachable** | 51 | 8 184 | 868 656 | THE CONFIGURATION SPACE — over the substrate |
| symmetry group order (2^d·d!) | 2 | 8 | 48 | **EXACTLY d** — not concealed |

**EXACT PROBES:** information dimension (box counting) · symmetry group order
**FAILED PROBES:** spectral dimension (both routes)

## 2. The information dimension is exactly the factor count

Box counting the uniform measure — **computed by walking every lattice site**, not asserted:

| boxes per axis n | occupied, d = 1 | d = 2 | d = 3 |
|---|---|---|---|
| 2 | 2 | 4 | 8 |
| 4 | 4 | 16 | 64 |
| 8 | 8 | 64 | 512 |
| 16 | 16 | 256 | 4 096 |

Every box is occupied at every scale, so the slope of `ln N` against `ln n` is **exactly 1, 2 and 3**.
A fourth direction would show up here and cannot.

## 3. The symmetry group is exact too

`|B_d| = 2^d · d!` → **2, 8, 48**. The order 48 occurs at **exactly one** dimension, as does 8. Two
independent exact probes name `d` without ambiguity.

## 4. The spectral probes fail — and the audit records its own failure

This is the honest negative. Three findings, all computed:

**(a) The gap is dimension-independent.** It is `0.3863508934` at every `d`, because a **single tensor
factor may be excited while the others stay at zero** — so the lowest positive eigenvalue is the ring's
own, for any number of factors. **A gap cannot count directions.**

**(b) The density-of-states estimator is biased.** `N(μ) ~ μ^(d_s/2)`, fitted over the lowest levels:

| window | d = 1 (truth 1) | d = 2 (truth 2) | d = 3 (truth 3) |
|---|---|---|---|
| lowest 6 | 1.279 | 2.614 | **n/a** |
| lowest 10 | 1.261 | 2.283 | 2.986 |
| lowest 20 | 1.428 | 2.395 | 3.723 |
| lowest 40 | 1.808 | 2.243 | 3.107 |

Off by up to **80 %**, in *both* directions depending on the window.

**(c) The estimator divides by zero** on a perfectly legitimate window (the lowest six levels of the
`d = 3` torus are not distinct enough to fit a slope → `sxx·k − sx² = 0`). My first implementation
returned `Infinity` and printed it into the report; that is now recorded as a **degenerate window**
(`EstimatorIsDegenerate`, `n/a`) rather than papered over. *A probe that returns Infinity for a
legitimate window is not a probe.*

**Consequence:** the spectral dimension is **not usable evidence** on a finite substrate, so it is
reported as a failed probe rather than quoted.

## 5. The observable dimension is neither 3 nor the state space

Families a substrate-constructed measurement can name: `C(48 + d, d) − 1` = **48, 1 224, 20 824**
(G_040 at `d = 1`, G_043 across the ladder). It exceeds 3 by four orders of magnitude — so the
higher-dimensional object is **largely resolvable in principle**, not hidden behind a 3D window.

## 6. Verdict: REFUTED — for a specific reason, not a general discomfort

1. **The apparent three is the tensor-factor count**, and two exact probes say so: the information
   dimension (= 3 exactly) and the symmetry group (order 48 exactly).
2. **The higher-dimensional object is a different index.** The reachable room counts *configurations
   over* a 3-dimensional substrate; the substrate's `d` counts *tensor factors*. They are not the same
   quantity at two sizes.
3. **A hidden direction could not hide.** It would have to evade the information dimension (exactly 3)
   and the symmetry group (order exactly 48). The only probe that genuinely cannot resolve the factor
   count is the **spectral** one — which is precisely the probe that fails to certify anything.
4. Therefore the projection reading fails as an **explanation of the apparent 3**: AT's
   higher-dimensionality is real, growing (51 → 8 184 → 868 656) and largely observable — and it is
   **not** what the world's three dimensions are.

## 7. What this changes elsewhere

- **G_039's room** is now reproduced from a second route (torus spectrum) rather than assumed.
- **G_033's 16 080** is reproduced exactly, and the resulting room 868 656 is confirmed as the number
  project memory cites.
- **A methodological rule is strengthened:** a dimension estimate is only evidence **inside its own
  regime** — the density-of-states route is regime-limited here, and the audit says so instead of
  quoting the estimate (`copilot-instructions` rule 9).

## Success criterion

> Can the apparent 3D world emerge as a projection of a higher-dimensional actualization space?

**REFUTED.** The apparent three is the number of tensor factors — measured exactly twice — while the
higher-dimensional actualization space is the (much larger, largely observable) space of configurations
over that substrate. **The two are different indices, not one space seen from two sides.**
