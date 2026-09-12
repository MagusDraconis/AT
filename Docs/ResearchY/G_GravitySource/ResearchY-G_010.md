# ResearchY-G_010 — Time Control Feasibility Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_010 (permanent)
**Title:** Time Control Feasibility Audit — what sustained clock shift can exist under allowed driving?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_010.md`
**Depends on:** ResearchY-G_002 (the free room), G_003 (the potential channel), G_005 (the Poisson ceiling
4.8867e-6), G_008 (the drive law: a mode-matched drive costs `1 − μ_k` of the amplitude **per step** and
nothing is self-sustaining), G_009 (the clock law `dτ/dt = ρ^(1/d)`); AT-QG QG197, QG21/QG187, QG194;
`AT.Core/ResearchXH/RhoDynamics.cs`, `UniversalAttractor.cs`, `NativeMetricDynamics.cs`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_010_Tests.cs` (8/8 PASSED, ~0.5 s)

## Purpose

Invert the clock law (G_009) and price the drive (G_008):

> **What sustained clock shift can exist under allowed driving?**
> Targets: **1 ns/day, 1 µs/day, 1 ms/day, 1 s/day** — for each: required Δρ, required drive, steady-state
> profile, power scaling.

**Answer.** 1 ns/day, 1 µs/day and 1 ms/day are **PRACTICAL as numbers** (far inside the accessible band,
negligible excursions, per-step drives of 7.4e-18 … 2.8e-8); the band's top (**0.1407 s/day**) and the
observed galactic field (**0.0464 s/day**) are **ASTROPHYSICAL ONLY**; **1 s/day is REFUTED** spontaneously
(7.105× the Poisson band). But the sharp statement is that *every* target needs an external, mode-matched,
structured driver — and the canonical chain supplies none.

## 1. The four targets (inversion of the clock law)

`f = T/86400` and `Δlnρ = d·f = 3f`:

| target | f | **required Δlnρ** | drive/step k=1 | drive/step k=95 | × band | equivalent well |
|--------|---|-------------------|----------------|-----------------|--------|-----------------|
| 1 ns/day | 1.157407e-14 | **3.472222e-14** | 7.436285e-18 | 2.777034e-14 | 7.11e-9 | **32.25 m/s** |
| 1 µs/day | 1.157407e-11 | **3.472222e-11** | 7.436285e-15 | 2.777034e-11 | 7.11e-6 | **1019.91 m/s** |
| 1 ms/day | 1.157407e-8 | **3.472222e-8** | 7.436285e-12 | 2.777034e-8 | 7.11e-3 | **32.25 km/s** |
| 1 s/day | 1.157407e-5 | **3.472222e-5** | 7.436285e-9 | 2.777034e-5 | **7.11 (>1)** | **1019.91 km/s** |
| *band top (G_005)* | 1.628900e-6 | 4.886700e-06 | 1.046560e-9 | 3.908313e-6 | 1.00 | 382.62 km/s |
| *observed galactic* | 5.367333e-7 | 1.610200e-06 | 3.448485e-10 | 1.287815e-6 | 0.33 | 219.63 km/s |

The **equivalent potential well** is the GR-facing reading of each target: a fractional depth `f` is a well
whose circular velocity is `c√f`. The Earth's surface (60.145 µs/day, G_009) is a **7.91 km/s** well, so
**1 ms/day is 16.63× the Earth's surface depth** and **1 s/day needs a 1020 km/s (cluster-depth) well**.

## 2. Required density change and profile

A sustained shift is a pure Neumann-mode steady state

```
rho_i = rhoBar * (1 + (Delta ln rho / 2) * v_k(i))          (rhoBar = 1/96)
```

with the mode-matched drive `s = c·v_k` and `c = (1 − μ_k)·amplitude` (G_008's exact result, re-verified
here: the driven recursion reproduces the steady state to **1e-9** after 150 000 steps at k = 1).

| target | Σρ | min ρ | excursion/ρ̄ | drive/step (k=1) | accumulated 200 steps (k=95) |
|--------|-----|-------|-------------|------------------|------------------------------|
| 1 ns/day | 1.0000000000 | 1.041667e-2 | 1.736111e-14 | 7.436285e-18 | 5.554068e-12 |
| 1 µs/day | 1.0000000000 | 1.041667e-2 | 1.736111e-11 | 7.436285e-15 | 5.554068e-9 |
| 1 ms/day | 1.0000000000 | 1.041667e-2 | 1.736111e-8 | 7.436285e-12 | 5.554068e-6 |
| 1 s/day | 1.0000000000 | 1.041649e-2 | 1.736111e-5 | 7.436285e-9 | 5.554068e-3 |

**Positivity and count conservation never bind** (the largest excursion is 1.7e-5 of ρ̄). The binding
constraints are the Poisson band above and the missing driver below.

## 3. Power scaling

| law | statement | verified |
|-----|-----------|----------|
| linear in the target | drive/step = 7.436e-18, 7.436e-15, 7.436e-12, 7.436e-9 for 1 ns → 1 s per day (exactly 1e3 apart) | ✓ |
| quadratic in relative frequency | `1 − μ_k = 2d(1 − cos(πk/N)) ≈ d(πk/N)²`; cost(2)/cost(1) = 4.00, cost(4)/cost(2) = 4.00 | ✓ (k = 1, 2, 4, 8 within 2 %; the continuum form *over*estimates at high k — exact 0.4000 vs 0.4935 at k = 48) |
| `N⁻²` at fixed physical wavelength | a 1024-site lattice is **113.7×** cheaper than a 96-site one | ✓ |
| **cost per unit clock shift** | `d(1 − μ_k)` = **6.4250e-4** (k = 1) vs **2.3994** (k = 95) → ratio **3734.4** (G_008's number) | ✓ |

So `P ∝ f · (k/N)²`: linear in the target shift, quadratic in the relative spatial frequency, and cheaper
in larger systems.

## 4. Verdicts

| target | required Δlnρ | drive/step (smooth) | × band | equivalent well | **verdict** |
|--------|---------------|---------------------|--------|-----------------|-------------|
| 1 ns/day | 3.472222e-14 | 7.436285e-18 | 7.11e-9 | 32.3 m/s | **PRACTICAL** |
| 1 µs/day | 3.472222e-11 | 7.436285e-15 | 7.11e-6 | 1020 m/s | **PRACTICAL** |
| 1 ms/day | 3.472222e-8 | 7.436285e-12 | 7.11e-3 | 32.3 km/s | **PRACTICAL** (as numbers) |
| 1 s/day | 3.472222e-5 | 7.436285e-9 | **7.1** | 1020 km/s | **REFUTED** (7.1× the band) |

* **PRACTICAL** — all three sit far inside the accessible band (7.10e-9, 7.10e-6, 7.10e-3 of it) with
  density excursions of 1.8e-16, 1.8e-13 and 1.8e-10 count units, per-step drives below 1e-8, and
  detectability far above a 1e-18 optical clock (ratios 1.2e4, 1.2e7, 1.2e10). **As numbers, given a driver.**
* **ASTROPHYSICAL ONLY** — the band's top is **0.1407 s/day** and the **observed galactic field is already
  33 % of it (0.0464 s/day, a 220 km/s well)**: sustained shifts above ~10 ms/day live at galactic/cluster
  scale.
* **REFUTED** — **1 s/day** as a spontaneously realised or sustained state (Δlnρ = 3.472e-5 = 7.105× the
  Poisson ceiling; a 1020 km/s well); and — the sharper statement — **any sustained clock shift whatsoever
  without an external mode-matched driver**, because the canonical chain supplies none: the branching flow
  is arrangement-neutral, the attractor erases arrangements (basin 1), an undriven local state lives
  **0.622 steps**, and G_005 caps spontaneous contrasts at **4.8867e-6**.

## 5. Classification and caveats

**No reclassification.** D_040 is untouched; G_009's clock law and G_008's drive law are used, not revised.
No canonical claim, value or equation changes; no new primitive.

* The "drive" is expressed in units of the *relative* density change (so `drive/Δlnρ = 1 − μ_k`); G_008's
  alternative normalisation (per unit L1 contrast) is 0.730125 vs 1.364e-4, the same 3734× story.
* The targets' *physical* extent is **not** constrained by the contrast (the clock channel is
  length-independent, G_003): what scales with a physical length is the *power* behind the drive, not the
  required Δρ.
* The four targets are all **measurable** with existing optical clocks (≥1.2e4× the 1e-18 floor) — the
  obstacle is producing the configuration, not seeing it.

## 6. Open problems (OP1–OP4)

1. Can any external agent be **mode-matched** to the AT occupancy index? (The same gap G_008 OP1 identified;
   NP_171's gate is imported and NP_174 closes self-amplification.)
2. Does the **`N⁻²` scaling** make a larger effective lattice a cheaper route to a given shift?
3. What is the **physical power** behind the drive once the lattice is mapped to a physical scale `L`
   (G_003's coherence length)?
4. Is the 0.1407 s/day band top firm, or does a different G_005 probability cut move the practical ceiling
   (the ceiling is a 1 % Poisson cut)?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_010_Tests.cs` — **8/8 PASSED** (~0.5 s)
**Group total:** G_001–G_010 = **82/82 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_010"`

| label | content |
|-------|---------|
| **PRACTICAL** | 1 ns/day (Δlnρ = 3.47e-14, drive 7.44e-18/step), 1 µs/day (3.47e-11, 7.44e-15), 1 ms/day (3.47e-8, 7.44e-12) — all ≤7.1e-3 of the band, positivity never binding, measurable once produced |
| **ASTROPHYSICAL ONLY** | the band's top 0.1407 s/day and the observed galactic field 0.0464 s/day (33 % of the band, a 220 km/s well); 1 s/day would need a 1020 km/s cluster-depth well |
| **REFUTED** | 1 s/day spontaneously (7.105× the band); and any sustained shift at all without an external mode-matched driver (branching arrangement-neutral, attractor erases, 0.622-step local lifetime, Poisson cap 4.8867e-6) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_008.md`, `ResearchY-G_009.md`, `ResearchY-G_005.md`
* `Docs/ResearchY/Tests/Results/Y_G_010_Result.md`
* `AT.Core/ResearchXH/RhoDynamics.cs`, `UniversalAttractor.cs`, `NativeMetricDynamics.cs`
