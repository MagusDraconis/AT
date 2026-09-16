# Y_QM_004 - Result

**Audit:** ResearchY-QM_004 - Schrodinger Correspondence Audit
**Verdict:** **PARTIAL**
**Tests:** 8/8 PASSED

## Answer

**PARTIAL - and the candidate list contains the WRONG DIFFERENTIAL ORDER.** All four named candidates are
**first-order** generators (**ω ∝ k**); the reference **ω = k²** is **second-order**. The generator that corresponds -
AT's own **Laplacian** - is not among them.

## The decisive measure: the power law

| candidate | d log ω / d log k | Schrodinger-like? |
|---|---|---|
| local difference (S − 1) | **1.0000** | no |
| centred difference (skew) | **1.0000** | no |
| Cayley flow of the skew part | **1.0000** | no |
| **spectral derivative** | **1.0000** | **no** - exact about the **wrong operator** |
| **AT-native Laplacian (C96(1..6))** | **2.0000** | **yes** |
| nearest-neighbour Laplacian | **2.0000** | yes |

## The velocities (channel 24, k = π/2)

| candidate | phase velocity | group velocity | error at the edge |
|---|---|---|---|
| **reference (Schrodinger)** | **1.570796** | **3.141593** | 0 |
| local difference | 0.636620 | **0.000000** | **100.00 %** |
| centred difference | 0.636620 | **0.000000** | **100.00 %** |
| Cayley flow | 1.273239 | **0.000000** | **100.00 %** |
| spectral derivative | 1.000000 | 1.000000 | 68.17 % |
| **AT-native Laplacian** | **8.912677** | **6.000000** | 98.66 % |
| nearest-neighbour Laplacian | 1.273240 | 2.000000 | 59.47 % |

The raw error against ω = k² **diverges** as k falls: **1426 %** at the first channel (local difference), **2954 %**
(Cayley flow). **A first-order generator cannot match a second-order law at any scale.**

## The native generator, verified

`μ(δ) = Σ_{r=1..6}(2 − 2cos rδ)` reproduces the recorded spectrum to a worst gap of **7.11E-15** over 49 channels;
zero mode at channel 0, gap **0.386350893**, **μ(π) = 12**, maximum **15.837372 at channel 11**. Long-wavelength
expansion: **ω = D k² with D = Σr² = 91** - a Schrodinger propagator with a coefficient the **shell set** fixes.

| finding | value |
|---|---|
| native group velocity peaks at | **channel 5** |
| native first fold at | **channel 11** (δ = 0.7199) |
| native reversals / sign changes | **23** / **5** |
| one-shell reversals / sign changes | **0** / 0 (it saturates at π/2) |
| native μ(π) | **12** (even symbol: not QM_003's oddness fold) |

## The Schrodinger window (contiguous from the longest wavelength, 10 %)

| candidate | channels in window | **occupied in window** | error at channel 1 |
|---|---|---|---|
| local difference | **0** | 0 of 42 | 1426.80 % |
| centred difference | **0** | 0 of 42 | 1426.80 % |
| Cayley flow | **0** | 0 of 42 | 2953.59 % |
| spectral derivative | **0** | 0 of 42 | 1427.89 % |
| **AT-native Laplacian** | **3** | **3 of 42** | **0.89 %** |
| nearest-neighbour Laplacian | **17** | **16 of 42** | 0.04 % |

## Notes

**Three defects in the audit's own first version are recorded.** (1) The sign-change counter missed a reversal
through an **exact zero** - the first-order family turns over exactly at the fold - so those candidates were counted
as having **no** reversal; it now carries the last non-zero sign. (2) **"Does not reverse" was conflated with "is
monotone"**: the one-shell Laplacian never reverses and still is not monotone, because it peaks at π/2. (3) **The
window count was not contiguous**, so the local difference "passed" at **2 interior channels** (and the spectral
derivative at 3) where `sin δ/δ²` crosses unity - a coincidence of the ratio, not a correspondence; the window is now
a contiguous run and the excluded passes are reported.

**The mismatch is one of differential order, not discretisation.** A Schrodinger equation evolves with a
**second-order** operator whose symbol is **even**; AT's update rules are built from the **first** difference, whose
symbol is **odd**.

**No group-G change.** The G_027, G_033 and G_035 scanners were re-run after the new core was added.
