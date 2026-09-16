# Y_G_063 - Result

**Audit:** ResearchY-G_063 - Canonical State Audit
**Verdict:** **BOUNDARY**
**Tests:** 8/8 PASSED

## Answer

**Nothing selects the weights**, and the separation cuts the series in two: **1 of 13** conclusions is an **algebraic
invariant**, **12** are **state-construction effects**. The invariant is the **distance-class bound**; the substrate
facts (levels, free room, distance classes, the eleven's structure, the floor of 47) are reported separately.

## Measurements

| measurement | value |
|---|---|
| \|correlation\| weight vs **eigenvalue** | **3.846E-002** |
| \|correlation\| weight vs **multiplicity** | 0.176 - **refused as evidence** (two distinct values) |
| degenerate levels' weights ordinary | **True** |
| zero-weight levels: canonical / shifted / ramp | **8, 31** / **6, 29** / **none** |
| first entry of every level is a **cosine** | **True** |
| last entry is sometimes a **sine** | **True** |
| canonical / alternative seed phase norm | **9.246E-015 / 1.048E+000** |
| coefficient 0.30, same weights | **1.076E-014** (still phase-free) |

| conclusion | holds / 9 | classification |
|---|---|---|
| the row space cannot exceed **49 distance classes** | **9** | **ALGEBRAIC INVARIANT** |
| at least 51 modes empty (free room) | 8 | recipe effect |
| row rank = occupied + 1 | 8 | recipe effect |
| hidden iff zero occupancy | 8 | recipe effect |
| no mode split | 8 | recipe effect |
| at least one channel of each degenerate level empty | 8 | recipe effect |
| occupies exactly 42 modes | 5 | recipe effect |
| kernel has 53 dimensions | 5 | recipe effect |
| occupied = non-zero-weight levels − 1 | 5 | recipe effect |
| reachable phase rank is 42 | 5 | recipe effect |
| channels 14 and 19 empty in both quadratures | 3 | recipe effect |
| the state is **phase-free** | 2 | recipe effect |
| **none** of the eleven is occupied | 2 | recipe effect |

## Notes

**A measured refinement:** the hidden-iff-zero-occupancy equivalence and the union-of-modes property fail for exactly one
recipe - the **all-modes** state - where the row space **saturates** the distance-class bound. So G_061's central relation
is a **consequence of staying below the bound**, not a separate law.

**Two defects recorded.** The per-state kernel basis was first built from seeds of the form `sin(a·seed + b·i)`, which
span only **two** dimensions - it returned **2** vectors where it needed **53** and produced **51 false mismatches**. And
three conclusions were constants in disguise (they ignored their state), replaced before any classification was read.
