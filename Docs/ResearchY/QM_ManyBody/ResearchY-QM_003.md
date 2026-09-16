# ResearchY-QM_003 - Dispersion Closure Audit

**Program:** ResearchY - Wave Geometry Program
**Group:** QM - Many-Body Correspondence
**ID:** ResearchY-QM_003 (permanent)
**Title:** Can the lattice dispersion sin(delta) be replaced or derived into delta without introducing a new primitive?
**Status:** COMPLETE
**Date:** 2026-09-16
**File:** `QM_ManyBody/ResearchY-QM_003.md`
**Depends on:** QM_002 (the fold: symbol sin d, zone edge stationary, 21 of 42 occupied modes past the fold), G_052/G_061 (the Fourier basis as the amplitude/phase decomposition), G_066/G_067 (the flow forms)
**Test suite:** `AT.Tests/ResearchY/QM_ManyBody/Y_QM_003_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/DispersionClosureAudit.cs`

## The question

Can the lattice dispersion **sin(δ)** be **replaced or derived into δ** without introducing a **new primitive**?
Compare the **ring**, **D96³**, the **continuum limit** and **modified generators**; measure **arg(m)**, the **group
velocity** and the **zone-edge behaviour**. Output **DERIVED / BOUNDARY / REFUTED**. Goal: **locate the origin of the
lattice fold**.

## The answer

> **BOUNDARY - and the origin is located exactly. THE FOLD IS NOT CAUSED BY DISCRETENESS. It is forced by the
> conjunction of three AT facts: the generator is ANTISYMMETRIC (which is what makes a norm-preserving flow possible
> at all), it is LOCAL (a stencil on the links), and the lattice is PERIODIC.**

## 1. The theorem, checked rather than cited

A real **antisymmetric** operator on a periodic ring has an **odd** symbol `f`, and an odd 2π-periodic function obeys
`f(π) = f(−π) = −f(π)`, so **`f(π) = 0` identically**. The zone edge is therefore a **stationary mode for every
antisymmetric lattice generator** - not a feature of the nearest-neighbour stencil, and not something a better stencil
can repair. Measured:

| candidate | band | f(π) |
|---|---|---|
| ring: nearest-neighbour antisymmetric | 1 | **1.22E-016** |
| ring: 5-point antisymmetric (4th order) | 2 | **2.04E-016** |
| ring: 7-point antisymmetric (6th order) | 3 | **2.69E-016** |
| ring: **spectral (exact) derivative** on the same 96 sites | 48 (dense) | **3.14E+000** |
| D96³: the same stencil per axis | 1 | **1.22E-016** |

The identity `f(2π − δ) = −f(δ)` is verified across a sweep of δ, not only at the edge.

## 2. The fold is the sign change of the group velocity

| candidate | linear window | fold channel | fold / zone | reversed channels | **reversed occupied** |
|---|---|---|---|---|---|
| **nearest-neighbour** | **1** | **24.00** | 0.5000 | 24 | **21 of 42** |
| 5-point (4th order) | **6** | 27.46 | 0.5722 | 21 | 18 of 42 |
| 7-point (6th order) | **11** | 29.58 | 0.6163 | 19 | 16 of 42 |
| **spectral (exact)** | **48** | **none** | - | **0** | **0 of 42** |
| D96³ per axis | 1 | 24.00 | 0.5000 | 24 | 21 of 42 |

**A wider stencil buys linearity and only PUSHES the fold**: the linear window grows **1 → 6 → 11** channels while
the fold moves **24.00 → 27.46 → 29.58**, with the zone edge still exactly stationary at every order - at the cost of
**one more neighbour shell per order**, which is a new primitive.

## 3. The replacement that needs no new primitive, and what it costs

**The spectral (exact) derivative on the SAME 96 sites** has symbol **exactly δ**: no fold, no stationary edge, unit
group velocity everywhere, so the whole occupied sector becomes Schrödinger-like. It is available **at no new
primitive**, because the Fourier basis is already AT's own amplitude/phase decomposition (G_052).

| candidate | band | non-zeros per row | local |
|---|---|---|---|
| nearest-neighbour | 1 | **2** | yes |
| 4th order | 2 | 4 | yes |
| 6th order | 3 | 6 | yes |
| **spectral (exact)** | 48 | **96** | **no** |

**The fold and the locality are in exact opposition** (verified candidate by candidate): every local candidate folds,
and the only candidate without a fold is the one that is not banded. **THE FOLD IS THE PRICE OF LOCALITY, NOT OF
DISCRETENESS.**

## 4. D96³ does not fix the fold - it multiplies it

The cube keeps the **same per-axis symbol** (each axis carries the same nearest-neighbour stencil), so the per-axis
fold is unchanged: same channel 24, same 21 of 42 occupied modes. What changes is the **dimension of momentum space**,
and a mode is folded when **any** component reverses:

| dimensions | folded fraction of the zone |
|---|---|
| 1 | **0.5000** |
| 2 | 0.7500 |
| 3 | **0.8750** |

**`1 − (1/2)^d`**: the cube raises the folded fraction from **one half** in 1D to **seven eighths** in 3D.

## 5. The continuum limit leaves the fold where it is

| cells | fixed-physical-wavenumber deviation | fixed-mode-index deviation | zone-edge deviation | fold channel | fold / zone |
|---|---|---|---|---|---|
| 96 | **9.968E-002** | 9.968E-002 | **1.000E+000** | 24.0 | 0.5000 |
| 192 | **9.968E-002** | 2.550E-002 | **1.000E+000** | 48.0 | 0.5000 |
| 384 | **9.968E-002** | 6.413E-003 | **1.000E+000** | 96.0 | 0.5000 |
| 768 | **9.968E-002** | 1.606E-003 | **1.000E+000** | 192.0 | 0.5000 |

**At a fixed PHYSICAL wavenumber the mode number rises with the cell count, so δ is constant and the deviation DOES
NOT MOVE - the fold survives every refinement.** The deviation falls as **1/N^1.97** only at a fixed **mode index**,
which is a longer wave: **a different state, not the same one measured better**. The zone edge is the zone edge at
every resolution, and the fold stays at the **same zone fraction**.

## Verdict

**BOUNDARY.** The replacement **exists** and is **exact** - the spectral derivative turns `sin δ` into `δ` with no new
primitive - and it costs the one thing the difference primitive supplies: **locality**. Every route that preserves
locality costs a new primitive:

| route | status | costs |
|---|---|---|
| a wider local stencil | **pushes** the fold only | another neighbour shell per order (a new primitive) |
| the spectral (exact) derivative | **removes** the fold exactly | locality: 96 non-zeros per row against 2 |
| the cubic substrate D96³ | **makes it worse** | same per-axis symbol; folded fraction 1/2 → 7/8 |
| the continuum limit | **leaves it at a fixed zone fraction** | new cells; at fixed physical wavenumber the deviation does not move |

**The origin, located:** the fold is the joint consequence of **(i) antisymmetry** - required for a norm-preserving
flow - **(ii) locality** - a stencil reaching only neighbouring cells - and **(iii) periodicity** - the zone edge is a
fixed point of δ → −δ. Together they force the symbol to be an **odd trigonometric polynomial**, which **must vanish
at the zone edge** and **must be non-monotone across the zone**. Discreteness is not the cause: the spectral
derivative lives on the same 96 cells and has the exact linear dispersion.

**2 of the 4 routes are REFUTED as fixes, 1 is DERIVED (exact but non-local), and the closure itself is a BOUNDARY.**
