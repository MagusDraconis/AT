# ResearchY-G_061 - Residual Phase Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_061 (permanent)
**Title:** What is special about the 11 unreachable phase directions?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_061.md`
**Depends on:** G_060 (the reachable phase rank is 42 of 53 by two independent routes), G_059 (the canonical state has no phase content), G_050 (the kernel is the phase sector), G_040 (hidden = changes no contraction)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_061_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ResidualPhaseAudit.cs`

## The question

What is **special** about the **11 unreachable phase directions**? Identify the **alternating mode**, the **empty
channels**, the **kernel relation** and the **symmetry properties**. Test: **can any existing AT operator reach these
directions?** Goal: **explain the 53 = 42 + 11 split**.

## The answer: **DERIVED - they are not special directions of the substrate at all**

> **They are the modes the canonical state does not occupy, and the split is a property of SHIFT-INVARIANCE rather than
> of the directions: no circulant AT operator can ever reach them, while AT's state-dependent ones reach all eleven at
> full rank.**

## 1. The eleven, identified

| channel | kind | frequency | orbit size | occupancy |
|---|---|---|---|---|
| 14 | cos | 0.782477 | 192 | -8.327E-016 |
| 14 | sin | 0.782477 | 96 | 6.009E-015 |
| 19 | cos | 1.357121 | 192 | -6.849E-015 |
| 19 | sin | 1.357121 | 96 | 4.136E-015 |
| 24 | cos | 2.000000 | 192 | -4.376E-015 |
| 24 | sin | 2.000000 | 192 | 5.135E-015 |
| 32 | cos | 3.000000 | **3** | 8.743E-015 |
| 32 | sin | 3.000000 | 192 | -6.356E-015 |
| 40 | cos | 3.732051 | 192 | -4.247E-015 |
| 40 | sin | 3.732051 | 192 | -6.731E-015 |
| 48 | cos | 4.000000 | **2** | 3.414E-015 |

The **empty channels** are **14, 19, 24, 32, 40** (both quadratures each) plus the **alternating channel 48**. Every
occupancy is at the floating-point floor. The group has order **192**, the eleven form a symmetry-**invariant set** as a
span, and the two most symmetric directions are measurable: the **alternating mode's orbit is 2** (a shift by two fixes
it) and **channel 32's cosine has orbit 3** (its rotation angle is 2π/3).

## 2. The kernel relation: hidden **if and only if** the state has no content

A contraction row is `A_d ρ` with `A_d` the distance-`d` relation, and **`A_d` is circulant**, so on a single mode `e` of
channel `c` it acts as `⟨A_d ρ, e⟩ = λ_d(c)·⟨ρ, e⟩` with `λ_0(c) = 1`. The row therefore vanishes on `e` **exactly** when
the state has no content in it.

| measurement | value |
|---|---|
| hidden ⟺ zero occupancy (all 95 non-constant modes) | **True** |
| occupied modes | **42** |
| occupied span (mean + occupied modes) | **43** |
| kernel dimension | **53** |
| 43 + 53 | **96** |

So the phase directions are hidden **not because the contractions are blind to them but because the state says nothing
about them**: the kernel **is** the state's orthogonal complement.

## 3. Where the eleven come from

| measurement | value |
|---|---|
| levels over 96 modes | **45** |
| modes any one-vector-per-level state leaves empty | **Σ(m−1) = 51** = 96 − 45 (the free room) |
| degenerate level 1 | index **13**, eigenvalue **12.0**, multiplicity **5**, channels **16, 32, 48** |
| degenerate level 2 | index **35**, eigenvalue **14.0**, multiplicity **6**, channels **8, 24, 40** |
| levels with a vanishing construction weight | indices **8, 31** → channels **14, 19** |
| **from degeneracy** | **7** (six quadratures of channels 32, 24, 40 + the alternating mode) |
| **from vanishing weights** | **4** (channels 14 and 19, both quadratures) |
| total | **11**, and the parts are **disjoint** (14 and 19 are not in a degenerate level) |

The canonical state takes exactly **one** basis vector per level - always `basis[0]` - so a level of multiplicity `m`
leaves **m−1** of its modes empty. Two of those nine fall in channels the level *does* occupy (channel 16 and channel 8),
so they are among the **42** contingently hidden; the other **7** join the **4** from the two vanishing-weight levels.

## 4. Can an existing AT operator reach them?

| operator | projection onto the eleven | rank |
|---|---|---|
| difference (circulant) | 9.512E-015 | **0** |
| centred difference (circulant) | 3.675E-015 | **0** |
| connection `h(ρ)·Δρ` (state-dependent) | **2.968E-003** | **11** |
| T1/T2 coupling (state-dependent) | **1.455E-001** | **11** |

**Both halves are measured.** A circulant operator is diagonal in the Fourier basis and can never mix channels, so from
the state's own span it reaches each occupied channel's *other* quadrature (42) and can never enter a channel the state
does not occupy. AT's state-dependent operators are **not** circulant - the connection it builds is a **multiplication**
by `h(ρ)` composed with the difference - and multiplication by a non-constant function **does** mix channels.

**So the answer to the audit's own test is YES for AT's state-dependent operators and NO for its shift-invariant ones,
and those two answers are the two halves of the split.**

## 5. The decisive experiment: the eleven move

Rebuilding the same construction from the **last** basis vector of each level instead of the first:

| state | empty channels | unreachable directions |
|---|---|---|
| canonical (`basis[0]`) | 14, 19, 24, 32, 40, **48** | **11** |
| alternative (`basis[^1]`) | 8, 14, 16, 19, 24, 32 | **12** |

The **set moves** - the alternating mode becomes occupied and channels 8, 16, 24 and 32 lose their content - so the
eleven are **not substrate-invariant**. What *is* invariant is the count the degeneracy forces: a level of multiplicity
`m` leaves `m−1` modes empty for **any** such state, so **51** modes are always empty. The remaining two come from a
weight that **vanishes**, which is a property of a **chosen formula** rather than of the substrate.

## Verdict

**DERIVED.** The split is explained: **42** are the *contingently* hidden other quadratures of the channels the state
occupies (reachable by any channel-preserving operator), and **11** are the modes of channels the state does not occupy
at all - **7** forced by the two degenerate levels and **4** by a vanishing construction weight. No **circulant** AT
operator can reach them; AT's **state-dependent** operators reach all eleven at rank **11**. The invariant content of the
split is `Σ(m−1) = 51`, the free room.
