# ResearchY-G_064 - Canonical Recipe Origin Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_064 (permanent)
**Title:** Why does the canonical state use these specific level weights?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_064.md`
**Depends on:** G_062 (one seed per level of non-zero weight), G_063 (one invariant out of thirteen), G_046 (the construction), G_040 (the hidden count 47)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_064_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/CanonicalRecipeAudit.cs`

## The question

**Why** does the canonical state use these specific level weights? Test the **basis[0] choice**, the **level-weight
formula** and the **zero-weight levels**. Measure **which conclusions change when the recipe changes**. Goal: **separate
the canonical recipe from theory content**.

## The answer: **DERIVED - the construction is PINNED, and by a requirement its own comment does not name**

> **The occupied count is 44 minus the number of zero-weight *non-constant* levels - arithmetic, verified on five
> formulas. The magnitudes are irrelevant. The seed choice decides *which sector* the state occupies, not how much of it.
> And exactly one requirement is satisfied by exactly one construction: phase-freeness.**

## 1. The count law is exact

| formula | zeros (total / non-constant) | predicted | measured |
|---|---|---|---|
| ramp (no modulus) | 0 / 0 | 44 | **44** |
| mod-17 | 3 / 3 | 41 | **41** |
| **canonical mod-23** | **2 / 2** | **42** | **42** |
| mod-15 | 7 / 6 | 38 | **38** |
| mod-11 | 9 / 8 | 36 | **36** |

**occupied = 44 − (zero-weight non-constant levels)**, exact for every formula tested. **The 42 is 44 minus the canonical
formula's two zeros**, and why the formula has two zeros is a property of its **modulus**.

The **constant level is special**: it carries the simplex direction, which is *not* a mode, so zeroing it (as mod-15 and
mod-11 do) does not remove an occupied mode. A first version wrote `44 − zeros` and was off by one on exactly those
formulas.

## 2. The seed choice swaps the two sectors

| seed | visible modes occupied | hidden modes occupied |
|---|---|---|
| **basis[0]** (cosine seeds) | **42** | **0** |
| **basis[^1]** (sometimes sine seeds) | **0** | **42** |

**The same totals in both cases** - the same occupied count (**42**) and the same kernel (**53**) - with the two sectors
**swapped**. So the recipe decides **which sector the state lives in**, not how much of it; the numbers the series quoted
are robust to the seed choice, while the sector is **entirely the seed's doing**. The cause is exact: **basis[0] of a
level is always a cosine**, basis[^1] is **sometimes a sine**, and the sines are the hidden quadratures.

## 3. The requirements, tested per construction

| id | requirement | holds (of 8 substances) | satisfied by |
|---|---|---|---|
| R1 | **GENERIC**: the orbit is large (≥84 of 95) | **4** | canonical, ramp/`basis[0]`, ramp/`basis[^1]`, all-modes |
| R2 | occupies the **full visible sector** | **3** | canonical, ramp/`basis[0]`, all-modes |
| **R3** | **PHASE-FREE** | **1** | **canonical alone** |
| R4 | the hidden count is the doublet count (**47**) | **1** | **all-modes** - a *different* construction |
| R5 | the kernel is a union of whole modes | **7** | all but all-modes |

**Exactly one requirement pins the canonical construction: R3, phase-freeness.** The recipe satisfies every requirement
its own justification names, and it is the only construction that is **both fully occupying and phase-free**.

**Three findings worth separating:**

1. **The named justification does not pin it.** G_046's comment names *genericity*, and that requirement is shared by
   **4** of the 8 constructions - while the canonical recipe's orbit (**84**) is **not** the largest (**95**, reached by
   a state that is *not* phase-free). **The recipe trades orbit size for phase-freeness.**
2. **One requirement pins something else entirely.** R4 - the hidden count equal to the doublet count of **47** - is
   satisfied by the **all-modes** construction, **not** by the canonical one, whose kernel is **53**. So **G_040's 47 and
   G_063's 53 are different quantities**, and the audit reports that rather than merging them.
3. **The magnitudes are irrelevant** (G_063): the same weights at twice the scale are the same construction.

## 4. The separation

| what the RECIPE decides | what the THEORY decides |
|---|---|
| which sector the state occupies (visual or hidden) | what follows once it is there: the amplitude/phase split, the reachable rank, the interactions, the laws |
| how many modes it fills (44 − zeros) | the levels, the distance classes, the bound of 49 and the floor of 47 |
| the kernel's dimension (53 for this recipe) | the hidden-iff-empty equivalence below that bound |

## Verdict

**DERIVED.** The construction is **pinned**: among **8 substances** (recipes up to the overall scale of their weights),
**exactly one requirement - phase-freeness - is satisfied by the canonical construction alone**, and every headline count
follows **arithmetically** from its zero count. The magnitudes encode nothing, the requirement its own comment names does
not pin it, and the **seed choice alone** decides whether the state lives in the visible or the hidden sector.
