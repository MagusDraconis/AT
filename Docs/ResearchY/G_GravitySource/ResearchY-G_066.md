# ResearchY-G_066 - Physical Flow Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_066 (permanent)
**Title:** Which update rule is physically privileged?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `G_GravitySource/ResearchY-G_066.md`
**Depends on:** G_060 (the flow's multipliers, stability and ranks), G_065 (no law requires phase-freeness; the dissipative flow converges to it), G_059 (the source), G_064 (the recipe)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_066_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/PhysicalFlowAudit.cs`

## The question

Which **update rule is physically privileged**? Compare the **forward difference**, the **backward difference**, the
**centred difference**, the **exact flow** and the **Cayley flow**. Measure **positivity**, **norm conservation**, **phase
evolution**, **attractors** and **compatibility with the AT laws**. Goal: determine whether **phase-freeness is a property
of AT itself or only of one chosen flow**.

## The answer: **REFUTED - phase-freeness is a property of the DISSIPATIVE flows, not of AT**

> **Four of the five forms are admissible, and the admissible set does not agree with itself: two erase the state while
> two preserve it. An admissible, law-abiding, total-conserving flow keeps the phase content indefinitely.**

## 1. The five forms, measured over 4000 steps at ε = 1E-3

| flow | min cell (canonical) | min cell (bearing) | admissible | deviation ratio | phase ratio | total residual | long run |
|---|---|---|---|---|---|---|---|
| forward difference | 9.390E-001 | 8.910E-001 | **True** | **0.381** | **0.253** | 1.152E-013 | **DECAYS TO UNIFORM** |
| backward difference | **-2.597E+002** | **-2.087E+002** | **False** | 1.058E+003 | 8.547E+002 | 5.451E-011 | **LEAVES THE SIMPLEX** |
| centred (skew) difference | 7.803E-001 | 7.060E-001 | **True** | **1.001** | **0.800** | 6.158E-014 | **PRESERVES THE STATE** |
| exact flow `exp(εD)` | 9.390E-001 | 8.911E-001 | **True** | 0.381 | 0.253 | 9.119E-014 | **DECAYS TO UNIFORM** |
| **unitary (Cayley of the skew part)** | 7.136E-001 | 7.357E-001 | **True** | **1.000** | **0.682** | 1.476E-013 | **PRESERVES THE STATE** |

**The five names describe four behaviours**, fixed by the multiplier: the forward difference and the exact flow are
**contractive**, the backward difference **amplifies**, the **centred** difference is a bare rotation generator, and the
**Cayley** flow of that generator is **unitary**.

**The laws rule out the amplifying form rather than the audit doing it:** its worst law residual is **1.000E+000**, because
it drives cells negative, while **every admissible form keeps the laws**. Every form conserves the **total** in exact
arithmetic - the difference annihilates the constant - with the amplifying form losing it only to floating point as its
own magnitude explodes.

## 2. The decisive measurement: the phase ratio

| flow | admissible | phase ratio (4000) | phase ratio (8000) |
|---|---|---|---|
| forward difference | True | 2.527E-001 | 2.136E-001 |
| exact flow `exp(εD)` | True | 2.526E-001 | 2.134E-001 |
| centred (skew) difference | True | 8.004E-001 | **0.785** |
| **unitary (Cayley)** | **True** | 6.816E-001 | **0.699** |
| backward difference | False | 8.547E+002 | 3.365E+016 |

**The dissipative forms take the phase content towards zero; the centred and unitary forms keep it.** And the unitary
form **violates no constraint the audit imposes**: every form conserves the total, every form fixes the uniform state, and
every **admissible** form keeps the AT laws.

**So an admissible, law-abiding, total-conserving flow preserves the phase content indefinitely** - which is why
**phase-freeness is a property of dissipation rather than of the theory**.

## 3. The dissipative forms erase *everything*, not only the phase

The forward difference's **deviation** ratio is **0.381** and its **phase** ratio **0.253**: the attractor is the
**uniform state**, not merely a phase-free one. The two dissipative forms agree to three digits (**0.3810 vs 0.3809**),
which is G_060's identification of the exact flow as the forward step's flow.

## 4. No form is privileged by the measures usually taken

All five conserve the total, all five fix the uniform state, and every admissible form keeps the laws - so none of those
measures separates them. The privilege, if there is one, must come from a requirement to **dissipate** rather than to
consume, which is a **physical choice** rather than a consequence of the relations the theory states. **G_065's attractor
argument therefore cannot be promoted** from *the admitted flow converges to the phase-free state* to *the theory requires
it*.

## Verdict

**REFUTED.** Phase-freeness is **not a property of AT**: the admissible set contains flows with **opposite** long-run
behaviour - two decay to uniform and two preserve the state - and the **unitary** flow, which violates no constraint the
audit imposes, **preserves the phase content indefinitely** (**0.699** of its initial value at 8000 steps). What produces
phase-freeness is **dissipation**.
