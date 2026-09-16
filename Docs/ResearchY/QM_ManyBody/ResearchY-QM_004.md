# ResearchY-QM_004 - Schrodinger Correspondence Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_004 (permanent)
**Title:** Does AT's unitary flow reproduce the dispersion of the Schrodinger equation?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `QM_ManyBody/ResearchY-QM_004.md`
**Depends on:** QM_002 (the fold), QM_003 (the origin of the fold), G_016 (the ring C96(1..6)), G_040/G_052 (the contractions and the amplitude/phase split)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_004_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/SchrodingerCorrespondenceAudit.cs`

## The question

Does AT's unitary flow reproduce the dispersion of the **Schrodinger equation**? Compare the **local difference**, the
**centred difference**, the **Cayley flow** and the **spectral derivative**; measure the **phase velocity**, the
**group velocity** and the **dispersion error**. Reference: **ω = k²**. Goal: determine whether any AT-native
evolution behaves like a **Schrodinger propagator**.

## The answer

> **PARTIAL - and the candidate list contains the WRONG DIFFERENTIAL ORDER.** All four named candidates are
> **first-order** generators, whose dispersion is **ω ∝ k**; the reference **ω = k²** is a **second-order** law. The
> generator that does correspond is one the question does not list: **AT's own Laplacian**.

## 1. The decisive measure: the power law

| candidate | kind | d log ω / d log k | Schrodinger-like? |
|---|---|---|---|
| **local difference (S − 1)** | first order | **1.0000** | no |
| **centred difference (skew)** | first order | **1.0000** | no |
| **Cayley flow of the skew part** | first order, unitarised | **1.0000** | no |
| **spectral derivative** | first order, exact | **1.0000** | **no** |
| **AT-native Laplacian (C96(1..6))** | **second order** | **2.0000** | **yes** |
| **nearest-neighbour Laplacian** | second order, one shell | **2.0000** | **yes** |

**The spectral derivative is exact about the WRONG OPERATOR**: it matches ω = k exactly. The reference requires
exponent **2**, and **four of the six candidates give 1**.

## 2. Both velocities must rise with k, and the first-order family fails both

| candidate | phase velocity at channel 24 (k = π/2) | group velocity at channel 24 | error at the zone edge |
|---|---|---|---|
| reference (Schrodinger) | **1.570796** (= k) | **3.141593** (= 2k) | 0 |
| local difference | 0.636620 | **0.000000** | **100.00 %** |
| centred difference | 0.636620 | **0.000000** | **100.00 %** |
| Cayley flow | 1.273239 | **0.000000** | **100.00 %** |
| spectral derivative | 1.000000 | 1.000000 | 68.17 % |
| **AT-native Laplacian** | **8.912677** | **6.000000** | 98.66 % |
| nearest-neighbour Laplacian | 1.273240 | 2.000000 | 59.47 % |

Schrodinger's phase velocity **rises** (it is k itself). The first-order family's phase velocity is `sin δ/δ`, which
**falls**, and its group velocity is `cos δ`, which **changes sign** - at channel 24 it is **exactly zero**. The raw
error against ω = k² **diverges** as k falls: **1426 %** at the first channel for the local difference and **2954 %**
for the Cayley flow. **A first-order generator cannot match a second-order law at any scale.**

## 3. The native generator, verified against the recorded spectrum

The audit **reproduces its input rather than importing a formula**: AT's mode operator is the graph Laplacian of the
circulant **C96(1..6)**, six neighbour shells, degree 12, with symbol
**μ(δ) = Σ_{r=1..6} (2 − 2cos rδ)** - recovered from the recorded spectrum to a worst gap of **7.11E-15** over all 49
channels, with the zero mode at channel 0, the recorded gap **0.386350893**, **μ(π) = 12** and its maximum **15.837372
at channel 11**.

Its long-wavelength expansion is **ω = D k² with D = Σr² = 91**, the second moment of the shell set: **a Schrodinger
propagator with an effective coefficient the shell set fixes**, not a mismatch of the power law.

## 4. But the native generator is Schrodinger-like only at the longest wavelengths

**The window, taken contiguously from the longest wavelength** (the contiguity matters - see §6):

| candidate | channels in the 10 % window | **occupied in the window** | error at channel 1 |
|---|---|---|---|
| local difference | **0** | 0 of 42 | 1426.80 % |
| centred difference | **0** | 0 of 42 | 1426.80 % |
| Cayley flow | **0** | 0 of 42 | 2953.59 % |
| spectral derivative | **0** | 0 of 42 | 1427.89 % |
| **AT-native Laplacian** | **3** | **3 of 42** | **0.89 %** |
| nearest-neighbour Laplacian | **17** | **16 of 42** | 0.04 % |

**The native Laplacian's group velocity peaks at channel 5 and folds at channel 11** - less than half the δ of the
one-shell fold - because **six shells interfere**. So the agreement with ω = Dk² holds over the first three channels
(0.89 %, 3.51 %, 7.73 %) and ends at the fourth (13.35 %): **3 of the 42 occupied modes**.

## 5. Two different fold mechanisms

| candidate | reversed channels | sign changes | reverses? |
|---|---|---|---|
| **first-order family** | **24** | **1** | **yes** - symbol **odd**, vanishes at the zone edge (QM_003) |
| **AT-native Laplacian** | **23** | **5** | **yes** - symbol **even**, **μ(π) = 12 ≠ 0**, so this is **shell interference** |
| nearest-neighbour Laplacian | **0** | 0 | **no** - it saturates with a single hump at π/2 |
| spectral derivative | 0 | 0 | no |

**The native fold is not QM_003's oddness fold.** Its symbol is even and does not vanish at the zone edge; it reverses
23 channels anyway, with five sign changes, because six shells interfere. **A different mechanism with the same
consequence.**

## 6. Three defects in the audit's own first version, recorded

1. **The sign-change counter missed a reversal through an exact zero.** The first-order family turns over *exactly* at
   the fold, and multiplying consecutive velocities fails when one is exactly zero - so those candidates were counted
   as having **no** reversal. The counter now carries the last non-zero sign.
2. **"Does not reverse" was conflated with "is monotone".** The one-shell Laplacian never reverses (v_g = 2sin δ ≥ 0)
   and is **still not monotone**, because it peaks at π/2. The report now names **reversal** as the physical
   discriminator and states the saturation separately.
3. **The window count was not contiguous**, so the **local difference "passed" at 2 interior channels** - and the
   spectral derivative at 3 - where `sin δ/δ²` happens to cross unity. That is a coincidence of the ratio, not a
   correspondence; the window is now the **contiguous run from the longest wavelength**, and the excluded interior
   passes are reported rather than hidden.

## Verdict

**PARTIAL.** **All four named candidates are REFUTED** against ω = k² - they are first-order generators, and the
dispersion error **diverges** rather than converging. **AT's native Laplacian is PARTIAL**: its power law is exactly
**2**, so it is a Schrodinger propagator with an effective coefficient **D = 91** fixed by the shell set, but its
agreement holds only over the **longest wavelengths** - **3 of the 42 occupied modes** in a 10 % window - because six
shells interfere and fold its group velocity at **channel 11**.

**The mismatch the question is probing is one of differential order, not of discretisation.** A Schrodinger equation
evolves with a **second-order** operator, whose symbol is **even**; AT's update rules are built from the **first**
difference, whose symbol is **odd**. AT contains both objects - and the one the question lists is the wrong family.
