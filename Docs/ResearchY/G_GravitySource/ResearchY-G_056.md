# ResearchY-G_056 - Non-Scalar Selection Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_056 (permanent)
**Title:** Can any existing non-scalar AT structure span the 53-dimensional phase sector?
**Status:** COMPLETE
**Date:** 2026-09-15
**File:** `G_GravitySource/ResearchY-G_056.md`
**Depends on:** G_055 (a scalar constrains at most one phase direction; six measured a rank of 3), G_054 (the phases are freely assigned), G_052 (the interface identity), G_009 and G_010 (the occupancy gradient and the connection AT builds from it), E_013 (the coupling census)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_056_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/NonScalarSelectionAudit.cs`

## The question

Can any **existing non-scalar** AT structure span the **53-dimensional phase sector**? Candidates: **phase vector
field**, **connection structure**, **T1/T2 sector coupling**, **edge-holonomy network**, **causal-order tensor**.
Measure the **phase rank**; require **rank > 3**.

## The answer: **DERIVED - the gradient-based structures span the phase sector EXACTLY**

> G_055's ceiling was a ceiling on **scalars**. **What this does NOT do is select a phase:** G_054 measured that no AT
> process runs the flow, so **spanning is SENSITIVITY, not DETERMINATION**.

## 1. The candidates and their phase rank

| candidate | outputs | **phase rank** | verdict |
|---|---|---|---|
| **phase vector field** | 96 | **53** | **DERIVED** |
| **connection structure** | 96 | **53** | **DERIVED** |
| **T1/T2 sector coupling** | 288 | **53** | **DERIVED** |
| edge-holonomy network | 96 | **0** | REFUTED |
| causal-order tensor | 96 | **0** | REFUTED |

Scalar ceiling to beat (G_055): **3** · phase dimensions: **53** · requirement met: **True**.

## 2. Why the gradient structures reach the full rank

| measurement | value |
|---|---|
| phase directions with a non-zero derivative | **53 of 53** |
| differentiation is injective on the phase sector | **True** |
| the coupling's T1 half vanishes identically | **True** (norm **0.000E+000**) |

**Differentiation is injective on every non-constant mode**, and the phase sector contains only non-constant modes, so a
field built from differences necessarily sees **all** of them. The reason is **structural, not numerical**.

**The T1 half is reported as vanishing.** The two shifts commute, so the antisymmetric part of the coupling is
identically zero and the candidate's rank comes from its **symmetric** half: a candidate whose T1 half is zero is weaker
than its name suggests, and saying so is the point of measuring it.

## 3. The null candidates, reported rather than omitted

- The **edge-holonomy network read from the link phases** is **decoupled** from the organisation (the census is **0**), so
  it cannot see a phase at all. The holonomy built instead from the **occupancy-derived** connection is **not a sixth
  route** but the connection candidate under another name, which is why the two are reported together.
- The **causal-order tensor**, built from the **sign** of occupancy differences, is **piecewise constant**: its
  derivative vanishes almost everywhere. An order structure cannot span a continuous sector.

## 4. Spanning is not selecting

| measurement | value |
|---|---|
| no AT process runs a phase flow | **True** |
| update rule spatial part / coupling census | **0.000E+000** / **0** |

A phase rank of **53** says the structure **would** move if a process drove it, and G_054 measured that **no AT process
does**. The phases remain **freely assigned**, and what has changed is the reason for G_055's deficiency: it was a
deficiency of **scalars**, not of AT.

> **The sharpest statement of the whole thread: AT contains structures sensitive to every phase direction and runs none
> of them.**

## Verdict

**DERIVED** - the **occupancy-gradient vector field** (with its connection and tensor descendants) spans the phase sector
in full, clearing the scalar ceiling by a factor of **17**.
