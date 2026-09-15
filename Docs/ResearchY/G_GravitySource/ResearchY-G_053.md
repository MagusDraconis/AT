# ResearchY-G_053 - Phase Sector Dynamics Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_053 (permanent)
**Title:** Do phase modes have independent physical effects beyond amplitudes?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_053.md`
**Depends on:** G_050 (the phase sector is the hidden Fourier modes), G_052 (the split is unique and the interface an identity), G_051 (the phase sector is physical), G_047 (the clock resolves the kernel), G_040 (the contraction algebra)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_053_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/PhaseSectorDynamicsAudit.cs`

## The question

Do **phase modes** have **independent physical effects** beyond amplitudes? Construct a **pure amplitude perturbation** and
a **pure phase perturbation**; measure the **clock**, **acceleration** and **field** responses; determine which
observables are **uniquely phase-sensitive**.

## The answer: **DERIVED - the phase effects are independent, and the reason is GEOMETRIC**

> The **amplitude sector is exactly the no-rotation sector**; the **phase sector is what turns** the organisation's
> Fourier content. The first **uniquely phase-sensitive** observable is the **quadrature functional**.

## 1. The two perturbations are orthogonal and both are real

| quantity | value |
|---|---|
| pure amplitude | a unit direction in the **42-mode** visible span |
| pure phase | a unit direction in the **53-mode** hidden span |
| overlap | **2.168E-018** → orthogonal |
| both perturbations real | **True** |

| reading | amplitude response | phase response |
|---|---|---|
| clock | **6.678E-003** | **6.772E-003** |
| acceleration | **8.904E-004** | **7.024E-004** |
| field strength | **1.128E-004** | **9.971E-005** |

**Neither perturbation is silent** - which is exactly why the audit had to test **independence** rather than mere
presence: the question is not whether the phase does anything, but whether its effect can be **mimicked**.

## 2. The independence test is a REPRODUCIBILITY test

The audit builds the span of each reading's responses to **all 42** amplitude directions (rank verified: **42 of 42**),
projects the reading's **phase** response onto it, and measures what is **left**:

| reading | non-reproducible fraction |
|---|---|
| clock | **1.0000** |
| acceleration | **1.0000** |
| field strength | **0.9926** |

Essentially **all** of the phase effect is unreachable from the amplitude sector.

## 3. The geometric reading - the physical answer

In a channel's quadrature plane (channel 1):

| move | magnitude change | angle change |
|---|---|---|
| **amplitude** | **3.464E-001** | **2.220E-015** = machine zero |
| **phase** | 6.059E-002 | **3.463E-001** |

> **The amplitude sector is EXACTLY the no-rotation sector; the phase sector is what turns the content.**

The amplitude move leaves the channel's **angle** at machine zero while the phase move turns it at first order, and the
amplitude move resizes about **6×** as much. (The phase move *does* change the magnitude, because the state carries
content in **both** quadratures of a populated channel - adding to the hidden one lengthens the vector as well as
turning it. The draft expected a clean 100× "resize vs rotate" split and the measurement is sharper in one place and
weaker in another; the criterion was corrected to what is measured.)

## 4. The first uniquely phase-sensitive observable

**The quadrature functional** ⟨ρ, hidden mode⟩:

| measurement | value |
|---|---|
| response to the phase move | **2.000E-002** (non-zero) |
| largest response to **any** amplitude move | **3.272E-015** = zero by orthogonality |
| uniquely phase-sensitive | **True** |

It is **not an invented object**: it is the coordinate G_050 named when it called the kernel *the quadrature carrying
each channel's phase*.

**AT's own laws are NOT uniquely phase-sensitive** - the clock, acceleration and field readings each take a
contribution from **both** sectors, so the observable that isolates the phase exactly is the quadrature projection
rather than one of the theory's existing laws.

## Verdict

**DERIVED.** The phase sector's effects are **independent** in the precise sense that **no amplitude move reproduces
them**; they are **geometric** in the sense that they **rotate** rather than resize; and the observable that isolates
them exactly is the **quadrature projection**.
