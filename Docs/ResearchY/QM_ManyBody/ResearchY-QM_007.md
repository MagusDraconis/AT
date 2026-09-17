# ResearchY-QM_007 - Generator Selection Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_007 (permanent)
**Title:** Why does AT use {1..6} instead of the Schrodinger-compatible {1}?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `QM_ManyBody/ResearchY-QM_007.md`
**Depends on:** QM_004 (the recorded spectrum and the reference), QM_005 (the shell census), QM_006 (the packet window), G_016/G_039 (the ring C96(1..6) and its 45 levels), G_061/G_062 (the free room and the mode table)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_007_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/GeneratorSelectionAudit.cs`

## The question

**Why does AT use {1..6} instead of the Schrodinger-compatible {1}?** Compare {1}, {1,2} and {1..6}. Measure
**locality**, the **dispersion error**, the **fold position**, **packet evolution** and the **existing AT
requirements**. Goal: identify which AT constraint forces the native generator away from the Schrodinger optimum.

## The answer

> **BOUNDARY - AND THE CONSTRAINT IS COMPUTED RATHER THAN ARGUED. It is the SUBSTRATE'S OWN SPECTRAL FINGERPRINT, and
> it is an INPUT to the theory rather than a consequence of any dynamical law.** The trace already in the repository,
> **1152 = 2 × 6 × 96**, fixes the shell **count** at six; the recorded **45 levels** with maximum **15.837372** then
> fix **which** six - and of all **63** subsets, **exactly one** reproduces the recorded spectrum.

## 1. The trace alone fixes the shell count

A circulant Laplacian's trace is `2|S|·96`, so **the trace counts the shells**:

| candidate | shells | trace | matches the record (1152) |
|---|---|---|---|
| **{1}** | 1 | 192 | no (factor 6) |
| **{1,2}** | 2 | 384 | no (factor 3) |
| **{1..6}** | 6 | **1152** | **yes** |

**The recorded trace 1152 = 2 × 6 × 96 is the number six, already in the repository.** Only one of the 63 subsets
matches it - the six-shell one.

## 2. And the rest of the fingerprint fixes which six

| requirement | recorded | **{1}** | **{1,2}** | **{1..6}** | discriminates |
|---|---|---|---|---|---|
| trace | 1152 | 192 | 384 | ***1152*** | yes |
| distinct levels | 45 | 49 | 47 | ***45*** | yes |
| free room (96 − levels) | 51 | 47 | 49 | ***51*** | yes |
| maximum eigenvalue | 15.837372 | 4.000000 | 6.249689 | ***15.837372*** | yes |
| reproduces all 96 eigenvalues | yes | no | no | ***yes*** | yes |
| amplitude/phase split 42/53 | 42 / 53 | 42 / 53 | 42 / 53 | 42 / 53 | **NO - \|S\|-independent** |

**Of the 63 subsets: 1 matches the trace, 9 match the level count, and exactly 1 reproduces the whole spectrum.**
No propagation requirement is involved anywhere in that selection.

**And one requirement that looks like a constraint is not one:** the **42/53 amplitude/phase split is
|S|-independent** - it follows from the ring's reflection pairing and the canonical state's construction - so it is

> **REFINEMENT (QM_008, 2026-09-17).** The `|S|`-independence claim above was made by reading a **mask-free helper**,
> which by construction returns the native answer, rather than by rebuilding the split on another generator. QM_008
> rebuilt it - levels, level basis, canonical state and observable split, all from the candidate spectrum - and the
> split **MOVES**: **1 + 46 + 49** for `{1}`, **1 + 44 + 51** for `{1,2}` and **1 + 42 + 53** natively, tied to the
> recorded 43 / 42 / 53 / 53 through five independent quantities. **QM_007's `|S|`-independence claim is REFUTED.**
> What survives of it is the structural half: every candidate's kernel is still a union of Fourier modes (zero split
> modes in all three) and the partition is exact in all three. QM_008 supersedes this one row; the rest of QM_007 -
> the trace identification, the 63-subset census and the five measures - is unaffected.
the same for every shell set and **cannot select a generator**. The audit reports that rather than omitting it.

## 3. Nothing dynamical selects the native set

| measure | **{1}** | **{1,2}** | **{1..6}** | prefers |
|---|---|---|---|---|
| **locality** (non-zeros per row) | 2 | 4 | **12** | the **native** set |
| dispersion error at the zone edge | **59.47 %** | 91.89 % | 98.66 % | the **singleton** |
| fold position (channel) | **none** | 28 | 11 | the **singleton** |
| packet window at 10 % | **514** | 153 | 23 | the **singleton** |
| existing AT requirements | fails all | fails all | **satisfies all** | the **native** set |

**Two of the five measures prefer the native set, and the first is explained by the second:** a 12-regular graph is
**local by construction**. **Everything about propagation prefers the singleton** - the dispersion error by 1.66×, the
fold not at all against channel 11, and the packet window by **22×**.

## 4. The packet evolution orders the three generators

| time | **{1}** | **{1,2}** | **{1..6}** | reference |
|---|---|---|---|---|
| 0 | 4.0000 | 4.0000 | 4.0000 | 4.0000 |
| 5 | 4.1879 | 4.1812 | 4.1340 | 4.1908 |
| 10 | 4.7067 | 4.6829 | 4.5122 | 4.7170 |
| 20 | 6.3729 | 6.3024 | 5.7828 | 6.4031 |
| 40 | 10.6983 | 10.5299 | 9.2609 | 10.7702 |

**The ordering is monotone in the shell count at every time.** Windows at 10 %: **{1} 514, {1,2} 153, {1..6} 23**.
Distances from the exact Schrodinger solution at t = 20: **{1} 0.004149, {1,2} 0.013906, {1..6} 0.088008**.

## Verdict

| question | answer | basis |
|---|---|---|
| which constraint forces the native generator? | **DERIVED** | the substrate's **spectral fingerprint**: the trace 1152 fixes the count at six, the 45 levels and the maximum fix which six, and **exactly 1 of 63** reproduces the spectrum |
| is the forcing dynamical? | **REFUTED** | no: only **locality** and the **requirements themselves** prefer the native set, and the first follows from the second |
| is the constraint a law? | **BOUNDARY** | no: it is an **INPUT**. The fingerprint is what the earlier audits derived the **free room 51**, the **mode table** and the **42/53 decomposition** from |

**1 DERIVED, 1 BOUNDARY, 1 REFUTED.** The question's premise is confirmed rather than assumed: **{1} really is the
Schrodinger optimum** - it never folds, has the smallest dispersion error, and propagates a packet about **22×**
longer. **What AT cannot do is use it**, because the substrate is a 12-regular graph and its spectrum is the record
from which the free room, the mode table and the amplitude/phase split were derived.

**The fold is therefore not a dynamical mistake, and not a mistake at all: it is the signature of the substrate the
theory actually has.**

## 5. Two defects in the audit's own first version, recorded

1. **The trace comparison used exact `double` equality.** The recorded trace is a **rounded integer** (1152) while the
   computed symbol sum lands on **1151.9999999999998**, so a match was reported as a mismatch - the repository's
   rule-1 trap in miniature. The comparison now carries a **1E-6 tolerance**, and `TraceMatchesTheRecord` is used by
   the census and the table alike.
2. **The table's trace cell was fed the MASK rather than the trace** (1, 3, 63 instead of 192, 384, 1152), from a
   local helper whose parameter I renamed but whose call sites I did not. The cells now go through the same
   `TraceMatchesTheRecord` helper the census uses, so a mismatch between the two is impossible by construction.
