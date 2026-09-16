# ResearchY-G_063 - Canonical State Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_063 (permanent)
**Title:** Why does the canonical construction choose exactly the observed weights?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_063.md`
**Depends on:** G_062 (one seed per level of non-zero weight; the eleven can be populated), G_061 (the kernel is the state's orthogonal complement), G_046 (the canonical construction), G_060 (the reachable phase rank is 42 of 53)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_063_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/CanonicalStateAudit.cs`

## The question

**Why** does the canonical construction choose exactly the **observed weights**? Measure the **level weights**, the **zero
weights**, the **occupied modes** and the **kernel size**; compare the canonical, full-weight and alternative-seed
constructions; determine **which conclusions depend on the canonical recipe**. Goal: **separate state-construction
effects from algebraic invariants**.

## The answer: **BOUNDARY - the separation is achieved, and it cuts the series' own conclusions in two: ONE algebraic invariant survives, TWELVE are state-construction effects**

> **Nothing selects the weights. Thirteen conclusions were turned into predicates and evaluated on nine deterministic
> recipes; only the distance-class bound held for all of them. Not even the canonical state's phase-freeness is forced.**

## 1. Nothing selects the weights

| measurement | value |
|---|---|
| \|correlation\| of weight with the level's **eigenvalue** | **3.846E-002** |
| the degenerate levels' weights are **ordinary** members of the distribution | **True** |
| \|correlation\| with the **multiplicity** | 0.176 - **NOT USABLE EVIDENCE** |

The multiplicity correlation is reported and **refused**: multiplicity takes only **two** distinct values across the 45
levels, so a coefficient on it describes those two points rather than a trend. The usable test - that the degenerate
levels' weights sit inside the distribution's range and away from its extremes - passes.

**The zeros are an artifact of the modulus.** The canonical formula`w = ((k+1)·37 mod 23 − 11)/23` vanishes on levels **8
and 31**; the shifted formula `w' = ((k+1)·41 mod 23 − 11)/23` on levels **6 and 29**; the ramp formula on **none**. Which
levels are skipped moves with the modulus. G_046's own comment gives the design reason: **the state must be generic**.

## 2. The recipe family, measured

| recipe | occupied | kernel | row rank | phase norm | of the eleven | empty channels |
|---|---|---|---|---|---|---|
| canonical (`basis[0]`, w) | 42 | **53** | 43 | **9.246E-015** | 0 | 6 |
| alternative seed (`basis[^1]`, w) | 42 | 53 | 43 | **1.048E+000** | 2 | 6 |
| full weight (all 1) | 44 | 51 | 45 | 7.402E-001 | 2 | 4 |
| shifted weight w' | 42 | 53 | 43 | 3.487E-001 | 2 | 6 |
| narrow weight w'' | 42 | 53 | 43 | 2.632E-001 | 2 | 6 |
| ramp weight (no zeros) | 44 | 51 | 45 | 3.657E-001 | 2 | 4 |
| alternating weight | 44 | 51 | 45 | 5.253E-001 | 2 | 4 |
| **coefficient 0.30 (same weights)** | 42 | 53 | 43 | **1.076E-014** | 0 | 6 |
| **all modes occupied** | **95** | **47** | **49** | 1.166E+000 | **11** | 0 |

## 3. Why the canonical state is phase-free - the precise cause

| measurement | value |
|---|---|
| the **first** entry of every level is a **cosine** | **True** |
| the **last** entry is sometimes a **sine** | **True** |

The canonical recipe takes `basis[0]` for every level, and `basis[0]` is always a **cosine**; the alternative takes
`basis[^1]`, which on a doublet is a **sine** - a hidden mode - so that state acquires phase content (**1.048E+000**).
Note also that the **coefficient** is irrelevant: the same weights at a larger scale stay phase-free (**1.076E-014**).

## 4. The separation

| id | holds | classification | conclusion |
|---|---|---|---|
| **C8** | **9 / 9** | **ALGEBRAIC INVARIANT** | the row space cannot exceed 49 distance classes |
| C1 | 2 / 9 | state-construction effect | the state is **phase-free** |
| C2 | 5 / 9 | state-construction effect | the state occupies exactly 42 modes |
| C3 | 5 / 9 | state-construction effect | the kernel has 53 dimensions |
| C4 | 2 / 9 | state-construction effect | **none** of the eleven is occupied |
| C5 | 5 / 9 | state-construction effect | occupied = (non-zero-weight levels) − 1 |
| C6 | 8 / 9 | state-construction effect | at least 51 modes are empty (the free room) |
| C7 | 5 / 9 | state-construction effect | the reachable phase rank is 42 |
| C9 | 8 / 9 | state-construction effect | row rank = occupied modes + the simplex direction |
| C10 | 8 / 9 | state-construction effect | **hidden iff zero occupancy** |
| C11 | 3 / 9 | state-construction effect | channels 14 and 19 empty in **both** quadratures |
| C12 | 8 / 9 | state-construction effect | at least one channel of each degenerate level is empty |
| C13 | 8 / 9 | state-construction effect | no mode is **split** between kernel and row space |

**One refinement is measured rather than argued.** C10 and C13 hold for **every** one-seed recipe and fail for exactly
one - the **all-modes** state - where the row space **saturates** the distance-class bound and a mode can be part kernel
and part row space. So **G_061's central relation is not a separate law**: it is a consequence of the row space staying
below the bound.

## 5. Substrate facts, reported separately

Facts that hold for **every** state are reported apart from the state conclusions, because a predicate that ignores its
state would be a literal wearing a predicate's clothes (rule 6):

| fact | value | basis |
|---|---|---|
| levels in the spectrum | **45** | the rank-6 ring Laplacian, state-independent |
| free room 96 − levels | **51** | a level of multiplicity m leaves m−1 modes empty |
| distance classes | **49** | d = 0…48; bounds every row space |
| the eleven | **11** | five doublets plus the alternating mode |
| degenerate levels | **2** | λ = 12 (mult 5) and λ = 14 (mult 6) |
| kernel floor 96 − 49 | **47** | no state can cross it |

## 6. Defects recorded

- **The kernel basis was built from seeds that span only two dimensions.** `sin(a·seed + b·i) = sin(a·seed)cos(b·i) +
  cos(a·seed)sin(b·i)` lies in a **2-dimensional** space, so a first version returned **2** vectors where it needed
  **53** and reported **51 mismatches** on the very state where the equivalence is known to hold. The fixed builder uses
  a **seed-dependent frequency**, the pattern G_047's builder uses.
- **Three conclusions were constants in disguise** (they ignored their state argument), and were replaced by real
  state-dependent predicates before the classification was read.
- **The multiplicity correlation is not evidence** and is refused in the report rather than quoted.

## Verdict

**BOUNDARY.** The separation is achieved and the split is large: **1 of 13** conclusions is an algebraic invariant (the
distance-class bound), and **12** are state-construction effects. The numbers the series has been quoting - phase-length
freeness, 42 occupied, kernel 53, the eleven empty, the reachable rank 42, hidden-iff-zero-occupancy - are measurements
**of a chosen state**, and the substrate facts that do survive are the level count, the free room, the distance classes,
the structure of the eleven and the **floor of 47**.
