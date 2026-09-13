# ResearchY-G_047 - Kernel Observable Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_047 (permanent)
**Title:** Which observable detects the 47 kernel directions directly?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_047.md`
**Depends on:** G_046 (the hidden set is the kernel; the validated hidden step), G_040 (95 = 48 contractions + 47 hidden), E_009 (the clock law read per cell), G_016b (the rate law)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_047_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/KernelObservableAudit.cs`

## The question

Which observable detects the **47 kernel directions** **directly**? Requirements: **responds to hidden directions**,
**distinguishes kernel states**, **independent of the contraction observables**. Measure **clock change**,
**acceleration change**, **field strength change**. Construct the **minimal observable basis**.

## The answer: **DERIVED - the first observable is AT's own clock law read at each cell**

> No new primitive: the clock law the theory already has, read **per cell** rather than in aggregate.

## 1. The kernel, built at the audited state

| quantity | value |
|---|---|
| state dimension | **96** (95 on the simplex) |
| contraction rank measured here | **42** (+ the simplex direction = 43) |
| **kernel dimension measured here** | **53** |
| G_040's ceiling for the hidden count | **47** |
| every basis direction changes no contraction | **True** (**5.684E-013**) |

## 2. The candidate readings

| reading | response to the kernel | rank on the kernel |
|---|---|---|
| **addressed clock** | **1.726E-002** | **53** |
| acceleration | **3.298E-002** | **53** |
| field strength | **6.479E-003** | (responds, smaller amplitude) |
| every contraction | **5.684E-013** | **0**, by definition |

## 3. The minimal observable basis

**The addressed clock-rate pattern, 53 independent readings** - one per kernel dimension at the audited state, from a
maximum of 96 cells. Two different kernel states differ by **2.338E-002**.

Minimality is **structural, not fitted**: a single cell's clock rate has a gradient along one direction, so resolving
`d` kernel dimensions needs `d` independent cell readings, and the clock pattern supplies exactly that many.

## 4. The requirements

| requirement | status |
|---|---|
| responds to hidden directions | clock **1.726E-002** while every contraction stays at **5.684E-013** |
| distinguishes kernel states | rank **53** on a **53**-dimensional kernel; neighbouring directions differ by **2.338E-002** |
| independent of contractions | contractions move **5.684E-013** where the clock moves **1.726E-002**; they retain **43**, the clock resolves **53** |

## 5. One number reported against G_040

The kernel measures **53** dimensions at the audited state against G_040's **ceiling of 47**; the basis is sized to the
**measurement**, and both numbers are stated so a future audit with a different state can compare.

## Verdict

**DERIVED.** The first observable that resolves the hidden sector is the **clock pattern**; it is independent of the
contractions **by measurement rather than by construction**, and the instrument needs **one reading per hidden
dimension**.
