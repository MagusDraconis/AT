# Y_G_045 — Observed vs Hidden Dimension Audit — Result

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_045_Tests.cs` — **7/7 PASSED**
**Core:** `AT.Core/ResearchXH/ObservedHiddenDimensionAudit.cs`
**Doc:** `Docs/ResearchY/G_GravitySource/ResearchY-G_045.md`

## Question

Can the apparent 3D world emerge as a **projection** of a higher-dimensional **actualization space**?

## Answer: **REFUTED** — the two dimensions are different indices, and the one that could hide is measured exactly

## Computed result

| d | 96^d modes | distinct levels A₀ | reachable room | state space |
|---|---|---|---|---|
| 1 | 96 | 45 | 51 | 95 |
| 2 | 9 216 | 1 032 | 8 184 | 9 215 |
| 3 | 884 736 | 16 080 | 868 656 | 884 735 |

- `A₀(1) = 45` and room `51` = **G_039's** numbers, reproduced
- `A₀(3) = 16 080` = **G_033's** robust level count, reproduced → room `868 656`

## The four measured dimensions

| family | d = 1 | d = 2 | d = 3 | verdict |
|---|---|---|---|---|
| observable (families) | 48 | 1 224 | 20 824 | resolvable, not hidden |
| spectral (density of states) | 1.261 | 2.283 | 2.986 | **UNRELIABLE** (biased; truth 1, 2, 3) |
| spectral (the gap) | 0.3863509 | 0.3863509 | 0.3863509 | **cannot reveal d** |
| information (box counting) | 1.000 | 2.000 | 3.000 | **exactly d** |
| attractor / reachable | 51 | 8 184 | 868 656 | the configuration space |
| symmetry group order 2^d·d! | 2 | 8 | 48 | **exactly d** |

## Two exact probes, two failures

**EXACT:** information dimension (`ln N / ln n` = 1, 2, 3 because every box is occupied at every
scale) · symmetry group order (2, 8, 48; order 48 at exactly one dimension).

**FAILED — the audit's own negative result:** the spectral gap is **identical at every d**
(`0.3863508934`, since one factor may be excited alone), and the density-of-states estimate is biased
by up to **80 %** (1.26–1.81 / 2.28–2.61 / 2.99–3.72 against the true 1, 2, 3) — and **divides by zero**
on the lowest-six-levels window at `d = 3` (first implementation printed `Infinity`; now recorded as a
degenerate window).

## Why the refutation is specific

1. The apparent **3 is the tensor-factor count**, named exactly twice (information dimension; symmetry
   group).
2. The higher-dimensional object carries a **different index**: the reachable room counts
   *configurations over* the substrate, not *directions of* it.
3. A hidden direction **could not hide** — it would have to evade two exact probes; the only probe that
   cannot resolve `d` is the spectral one, which certifies nothing here.

## Tests

`7/7 PASSED` — ground (3 cross-audit checks) · information dimension exact · symmetry group exact ·
spectral probes fail (gap invariance, estimator bias, degeneracy) · observable dimension ≠ 3 ≠ state
space · verdict/registry shape · full report.
