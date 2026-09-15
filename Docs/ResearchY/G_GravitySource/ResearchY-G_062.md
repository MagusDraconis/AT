# ResearchY-G_062 - Mode Occupation Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_062 (permanent)
**Title:** What determines which Fourier modes are occupied in the canonical state?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_062.md`
**Depends on:** G_052 (the interface identity), G_059 (the canonical state is phase-free), G_060 (the reachable phase rank is 42 of 53), G_061 (the eleven are the unoccupied channels)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_062_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ModeOccupationAudit.cs`

## The question

What **determines** which Fourier modes are **occupied** in the canonical state? Measure the **occupied modes**, the
**empty modes**, the **state construction weights** and the **degeneracy structure**. Test: **can the empty 11 be
populated without changing the theory?** Goal: explain why the canonical state occupies 42 modes and leaves 11 empty.

## The answer: **DERIVED - the occupancy is set by the CONSTRUCTION rather than by any AT law, and the eleven CAN be populated; what survives is an algebraic floor of 47**

## 1. The rule, and it is arithmetic

The canonical state takes **one seed per level** - always `basis[0]` - and weights it, so its span is the mean plus one
mode per level of **non-zero** weight:

| measurement | value |
|---|---|
| levels | **45** |
| levels with a non-zero weight | **43** |
| the rule's prediction: seeds − 1 | **42** |
| measured occupied modes | **42** |
| empty modes | **53** |
| with every weight non-zero | **44** occupied |

The rule is confirmed, and **the number 42 is a property of a chosen formula**: changing nothing but the weights removes
the zeros and the count becomes 44.

**The prompt's phrasing is corrected here.** The canonical state occupies 42 **visible** modes and leaves the whole
**53-direction phase sector** empty; of those 53, the **eleven** are the ones lying in channels it does not occupy at all.

## 2. The empty channels follow the formula's zeros

| state | occupied | empty | empty channels | of the eleven |
|---|---|---|---|---|
| canonical (`basis[0]`, weights w) | 42 | 53 | **14, 19, 24, 32, 40, 48** | **0 of 11** |
| alternative seed (`basis[^1]`, weights w) | 42 | 53 | 6 | 2 of 11 |
| full weight (all weights 1) | 44 | 51 | 4 | 2 of 11 |
| **shifted weight formula w'** | 42 | 53 | **6, 23, 24, 32, 40, 48** | 2 of 11 |
| full weight, alternative seed | 44 | 51 | 4 | 4 of 11 |
| **all modes occupied** | **95** | **0** | **none** | **11 of 11** |

The canonical formula's zeros fall on levels **8 and 31** (emptying channels **14** and **19**); the **shifted** formula
`w'(k) = ((k+1)·41 mod 23 − 11)/23` has its zeros on levels **6 and 29** and empties channels **6 and 23** instead. The
empty channels **follow the formula's zeros**, while **24, 32, 40 and 48 are empty under both** because they come from
the degenerate levels.

## 3. The eleven can be populated, and nothing but the state moves

A state carrying **every** mode gives **95 occupied, 0 empty, no empty channel, 11 of 11** of the eleven occupied. The
levels (**45**), the distance classes (**49**), the contraction rows and the eleven are the **same objects** for every
state - asserted for each - so **no law, operator or coefficient moves**.

## 4. What survives is an algebraic floor of 47

| state | row rank | kernel |
|---|---|---|
| canonical | **43** | **53** |
| alternative seed | 43 | 53 |
| full weight | 45 | 51 |
| shifted formula | 43 | 53 |
| full weight, alternative seed | 45 | 51 |
| **all modes occupied** | **49** | **47** |

The row space is spanned by one row per **distance class** - `A_d ρ` for `d = 0…48` plus the simplex direction - so its
rank **cannot exceed the 49 distance classes** (measured: True for every state). The smallest kernel any state reaches
is therefore **47**, and the **canonical state's 53 is above it by six dimensions of its own construction**.

## Verdict

**DERIVED.** The occupancy is explained: **one seed per level of non-zero weight**, so the count is
`(levels with non-zero weight) − 1 = 42`, and the empty channels are the ones the formula's zeros and the degenerate
levels leave untouched. **The eleven are coordinates**: a state that occupies every mode populates all of them with no
change to any AT object. What is **not** bookkeeping is the **floor** - **47 = 96 − 49** - forced by the size of the
centralizer, which no state can cross. The canonical state leaves **six** more hidden than the algebra requires.
