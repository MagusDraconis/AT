# ResearchY-G_049 - Clock Primacy Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** G - Gravity Source
**ID:** ResearchY-G_049 (permanent)
**Title:** Is the clock pattern the unique lossless observable of ρ?
**Status:** COMPLETE
**Date:** 2026-09-14
**File:** `G_GravitySource/ResearchY-G_049.md`
**Depends on:** G_048 (the four readings and their ranks), G_047 (the clock pattern resolves the kernel), G_046 (the kernel), G_040 (the non-addressed ceiling), E_009 (the clock law), E_010 (the coupling)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_049_Tests.cs` (6/6 PASSED)
**Core:** `AT.Core/ResearchXH/ClockPrimacyAudit.cs`

## The question

Is the **clock pattern** the **unique lossless** observable of ρ? Compare the clock pattern, the acceleration pattern,
the field-strength pattern and the contraction observables. Measure **invertibility**, **retained information**,
**rank**, **kernel**. Determine which observables are **information-equivalent to ρ**. Goal: test whether **time is the
primary observable** of ρ.

## The answer: **LOSSLESS - but NOT UNIQUE**

> Three of the four readings are **information-equivalent to ρ**; only the **contractions** lose information. What is
> special about the clock is that its inverse is **closed-form**.

## 1. The four readings, and the inversion test

| reading | rank | retained | inverse | verdict |
|---|---|---|---|---|
| **clock pattern** | **95** | **1.000** | closed form ρ = rate^d, residual **4.441E-016** | **LOSSLESS** |
| acceleration pattern | **95** | **1.000** | searched: **0** collisions, residual **0.000E+000** | **LOSSLESS** |
| field-strength pattern | **95** | **1.000** | searched: **0** collisions, residual **0.000E+000** | **LOSSLESS** |
| contraction observables | **43** | **0.453** | NONE - **52** dimensions missing | **LOSSY** |

**Lossless readings: 3.** The clock pattern is **not** unique.

## 2. The uniform test is honest in both directions

Each reading's inverse is attempted by **projected gradient descent on the simplex** from three deterministic displaced
starts, with the audited state as the **control** - which recovers itself, so the machinery is known to be able to
succeed before its failures are believed. A reading is lossless only if it has full rank, **no** alternative state
reproduces its pattern, and the control converges.

## 3. A withdrawn proof, and a hypothesis that was not confirmed

The audit set out to show that **rank does not imply invertibility**, and its first instrument was an explicit
**collision**: reflecting the state through its own mean reverses every difference, and the acceleration pattern uses
absolute differences, so the reflection *should* collide. **The measurement refused it** - the reflection reverses the
differences of **ρ**, while the pattern is built from differences of the **rate**, and the cube root is not linear,
so the reflected pair sat **1.951E-002** apart. The collision is recorded as **withdrawn**.

Its replacement - the search - found **zero collisions for all three full-rank readings** and converged back onto the
audited state from every displaced start. So the hypothesis is **not confirmed** for these four readings: here full
rank and invertibility **coincided**, which is recorded as an **empirical outcome of this state, not a theorem**.

## 4. The contractions are lossy by dimension

They retain **43 of 95** dimensions (**0.453**), leaving **52** unrecoverable in principle - no search needed, and
none applicable.

## Verdict

**LOSSLESS.** Time is **a** primary observable of the organisation: the clock pattern is lossless and
information-equivalent to ρ. It is **not** the unique one - three of the four readings compared are lossless - and the
property the measurement leaves to the clock is **invertibility in closed form**, one call per cell, where the other
two lossless readings can only be inverted by search.
