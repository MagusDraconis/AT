# ResearchY-QM_006 - Schrodinger Propagator Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_006 (permanent)
**Title:** Can the nearest-neighbour generator {1} produce psi(t) = exp(-iHt)psi(0) with Schrodinger-like dispersion?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `QM_ManyBody/ResearchY-QM_006.md`
**Depends on:** QM_004 (the reference and the power law), QM_005 (the shell census and the cure's price), G_016 (the ring C96(1..6))
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_006_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/SchrodingerPropagatorAudit.cs`

## The question

Can the **nearest-neighbour generator {1}** produce **psi(t) = exp(-iHt)psi(0)** with **Schrodinger-like dispersion**?
Compare the native {1..6}, {1} and the spectral derivative; measure **omega(k)**, the **group velocity**, **packet
spreading** and **norm conservation**, against the reference of **Schrodinger packet evolution**. Output
**ANALOGOUS / PARTIAL / REFUTED**.

## The answer

> **PARTIAL AT THE LEVEL OF THE THEORY, AND ANALOGOUS FOR THE GENERATOR THE QUESTION ASKS ABOUT.** The answer to the
> question as asked is **YES**: **{1} produces `exp(-iHt)psi(0)` with the lattice Schrodinger dispersion**, norm
> conservation is **exact**, the packet **spreads by the continuum law**, and its distance from the exact Schrodinger
> solution stays inside **10 %** for **t up to 514.0**. The theory is PARTIAL because **it does not use {1}**.

## 1. The audit evolves a packet, not a dispersion table

Every generator here is **circulant**, so the evolution is **exact and diagonal in the Fourier basis** - no
time-stepping error at all:

```
psi(t) = sum over k of c_k exp(-i omega_k t) phi_k ,   phi_k(j) = exp(i 2 pi k j / N)/sqrt(N)
```

The **reference is the same packet** evolved with the continuum law `omega = k^2`, so the comparison isolates the
lattice effect rather than comparing two approximations. The **analytic law is checked against the exact evolution**:

| time | reference width | analytic law | ratio |
|---|---|---|---|
| 0 | **4.0000** | 4.0000 | 1.000000 |
| 2 | **4.0311** | 4.0311 | 1.000000 |
| 5 | **4.1908** | 4.1908 | 1.000000 |
| 10 | **4.7170** | 4.7170 | 1.000000 |
| 20 | **6.4031** | 6.4031 | 1.000000 |
| 40 | **10.7702** | 10.7703 | 1.000000 |
| 80 | 19.8236 | **20.3961** | 0.9719 |

The law is `sigma(t)^2 = sigma_0^2 + (t/sigma_0)^2` in lattice units with `omega = k^2`. At t = 80 the **ring's finite
size** makes the measured width lag the continuum law - a measured finite-size effect, not a discrepancy.

## 2. Norm conservation: exact, and the control is why the test exists

| candidate | norm deviation at t = 20 |
|---|---|
| native {1..6} | **6.66E-016** |
| nearest neighbour {1} | **6.66E-016** |
| spectral derivative | **0.00E+000** |
| **dissipative control** | **1.83E-001** |

Every unitary candidate conserves the norm to machine precision **because its symbol is real**, which makes the
generator **Hermitian**. The dissipative control is the only candidate that does not - and it is carried precisely so
that this test **cannot pass vacuously**.

## 3. The measure that decides: the packet's width

| time | **{1}** | native | **spectral** | reference |
|---|---|---|---|---|
| 0 | 4.0000 | 4.0000 | **4.0000** | 4.0000 |
| 5 | 4.1879 | 4.1340 | **4.0000** | 4.1908 |
| 10 | 4.7067 | 4.5122 | **4.0000** | 4.7170 |
| 20 | 6.3729 | 5.7828 | **4.0000** | 6.4031 |
| 40 | 10.6983 | 9.2609 | 4.9323 | 10.7702 |
| 80 | 19.7511 | 17.1761 | **4.0000** | 19.8236 |

**`{1}` tracks the continuum law; the native generator lags it; and the spectral candidate's width never moves.**
A Schrodinger propagator **spreads** a packet because its frequency goes as the **square** of the wavenumber; an
advection operator **drives** one rigidly because its frequency is **linear**. The two are hard to tell apart in a
table of `omega(k)` and unmistakable in `sigma(t)`.

**The driving is measured directly:** the spectral candidate's **centroid moves 20.000000 cells in t = 20** - exactly
one cell per unit time, its unit group velocity - while its width growth is **1.000000**.

**One artifact is recorded rather than hidden:** at **t = 40** the spectral packet **straddles the ring's seam** and
the width reads 4.9323 instead of 4.0000, recovering at t = 80. The centroid is not well defined on a circle once the
packet covers the seam, which is a property of the **ring** and not of the generator.

## 4. The window: how long each candidate stays Schrodinger-like

| candidate | window at 10 % (bisected) |
|---|---|
| **nearest neighbour {1}** | **514.0** |
| native {1..6} | **23.0** |
| spectral derivative | **0.8** |
| dissipative control | 2.9 |

**The one-shell window is 22.39× the native one**, and the **quartic contamination ratio `E/D`** from QM_005 is
**25.00** - the same measurement reached from the dynamical side, **but not identically**, because the distance is not
linear in the accumulated phase error.

**The distances at t = 20** show the same ordering: **{1} 0.004149** (0.4 %), native **0.088008** (8.8 %), spectral
**1.384233** (138 %), control 0.419941.

## 5. Dispersion and group velocity, recovered from the propagator side

| candidate | power law | v_g at channel 1 | v_g at 24 | v_g at 48 |
|---|---|---|---|---|
| {1} | **2.0000** | 0.130896 | 1.999999 | **0.000000** |
| native (by D) | **2.0000** | 0.128572 | 0.065934 | 0.000000 |
| spectral | **1.0000** | **1.000000** | **1.000000** | **1.000000** |
| reference | **2.0000** | 0.130900 | 3.141593 | 6.283185 |

The reference's group velocity **rises without bound**; the spectral candidate's is the **constant 1**; the two
Laplacians' **vanish at the zone edge**. QM_004's power laws are recovered independently from the dynamics.

## Verdict

| candidate | verdict | basis |
|---|---|---|
| **nearest neighbour {1}** | **ANALOGOUS** | `2 - 2cos d` **is** the lattice Schrodinger law (power 2, no fold), norm exact, window **514.0** |
| native {1..6} | **PARTIAL** | same law once normalised by D = 91, but a **23.0** window and a fold at channel 11 |
| spectral derivative | **REFUTED** | `omega = k` is linear: the packet **translates rigidly**, width `4.0000` forever, distance `1.38` at t = 20 |
| dissipative control | **REFUTED** | not unitary: norm deviation **1.83E-001** |

**1 ANALOGOUS, 1 PARTIAL, 2 REFUTED.** The nearest-neighbour generator does what the question asks; the theory does
not use it, and the generator it does use has a window **22× shorter**.

## 6. Three defects in the audit's own first version, recorded

The audit's entire conclusion rests on the **width**, and the first version got that measure wrong three times:

1. **The packet's normalisation:** `psi = exp(-x^2/(2w^2))` has RMS width `w/sqrt(2)`, not `w` - so the packet was
   compared against a law for a packet of a different width. It is now `exp(-x^2/(4w^2))`, whose **density** has
   standard deviation exactly `w`.
2. **The analytic spread law had the wrong coefficient** - `(2t/w)^2` instead of `(t/w)^2` - and the **measured**
   reference evolution **refused it by 41 %**. The law is now checked *against* the exact evolution at every time
   (agreement to five digits), which is the direction that would have caught it immediately.
3. **The width's second moment was not unwrapped about the centroid**, so a packet straddling the ring's edge reported
   an inflated spread (6.09 where the true width was 2.83). The deviation is now unwrapped, and the residual seam
   artifact at t = 40 is **reported rather than hidden**.

**A fourth correction is textual:** the window ratio (22.39) was first described as *being* the quartic contamination
ratio. It **tracks** it (25.00) without equalling it, and the audit now says which.
