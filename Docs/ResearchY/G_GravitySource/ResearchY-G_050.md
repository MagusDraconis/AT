# ResearchY-G_050 - Kernel Structure Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_050 (permanent)
**Title:** What physical structure do the 53 kernel directions represent?
**Status:** COMPLETE
**Date:** 2026-09-14
**File:** `G_GravitySource/ResearchY-G_050.md`
**Depends on:** G_040 (95 = 48 + 47; the intra-doublet orientations), G_046 (the hidden set is the kernel), G_047 (53 readings resolve it), G_049 (three of four readings are lossless)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_050_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/KernelStructureAudit.cs`

## The question

What **physical structure** do the **53 kernel directions** represent? Measure **basis vectors**, **symmetry classes**,
**multiplicity relation**, **clock / acceleration / field signatures**. Goal: does the kernel have an **independent
physical interpretation**?

## The answer: **DERIVED - the kernel is the PHASE SECTOR of the organisation**

> One hidden **quadrature** per populated doublet - G_040's **intra-doublet orientation** - plus **both** quadratures of
> every channel the state leaves **empty**, plus the alternating mode: **47 + 5 + 1 = 53**.

## 1. Two hypotheses were refuted before the right one was named

**(a) "The kernel is a union of whole doublets" - REFUTED.** The first argument was dihedral: an operator commuting
with a group cannot mix its irreducible channels, so each 2-dimensional doublet would be wholly hidden or wholly
visible. The measurement refuses it: **42 of the 47 doublets are HALF hidden**, because a real 2-dimensional
irreducible admits a 2×2 commutant.

**The correct argument is the sharper one:** the contractions are **circulant**, hence diagonal in the individual
Fourier modes. It is **translation invariance** - not reflection symmetry - that forbids mixing. Measured per mode:
**no mode is split anywhere** (all 95 modes are cleanly hidden or visible).

**(b) "The kernel is the short-wavelength sector" - REFUTED.** The draft expected the hidden modes to be the
fine-structure ones, since distance-class contractions average over cells. Measured: the hidden and visible modes are
**interleaved channel by channel** (**separated in frequency: False**), and the clock-signature correlation against
frequency is **-0.0279**, i.e. none.

## 2. The multiplicity relation, and the name

| quantity | value |
|---|---|
| state dimensions | **95** |
| hidden Fourier modes | **53** |
| visible Fourier modes | **42** |
| **half-hidden doublet channels** | **42** |
| **empty channels** | **5** |
| the empty channels ARE the doubly-hidden ones | **True** |

**The named structure:** `47 (one hidden quadrature per populated doublet) + 5 (both quadratures of each empty
channel) + 1 (the alternating mode) = 53`, and the visible **42** are the **magnitudes** of the populated channels.

**This reconciles G_040 exactly.** G_040's loss of **47** is the **generic** case - 47 orientations, no empty channel -
while this state hides five extra channels' worth because the state carries nothing in them.

## 3. The signatures

Every one of the **53** hidden modes carries a signature from all three readings (clock, acceleration, field): no mode
is invisible to the lossless readings, so the interpretation does not depend on which reading one uses. What the
signatures do **not** show is any frequency ordering - the observation that withdrew hypothesis (b).

## 4. What the kernel is not

- **not a gauge orbit** - G_046 measured the symmetry orbit at **84** dimensions, a different object;
- **not a numerical artefact** - every mode is whole to 1E-3 or better;
- **not the whole remainder of the state** - the visible 42 are the magnitudes, real named content.

## Verdict

**DERIVED.** The kernel has an **independent physical interpretation**: it is the **phase sector** - the quadrature
completing each populated channel's magnitude, plus the whole of each empty channel, plus the alternating mode. The
fact that it is a union of whole Fourier modes is what makes "the hidden directions" well defined at all.
