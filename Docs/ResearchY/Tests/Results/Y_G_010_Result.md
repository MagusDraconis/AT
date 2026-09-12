# Y_G_010_Result.md — ResearchY-G_010 Time Control Feasibility Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_010_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 8/8 PASSED (~0.5 s) — group G total 82/82 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_010"`

## Summary

**Question:** What sustained clock shift can exist under allowed driving? (Given `dτ/dt = ρ^(1/d)` from
G_009 and the G_008 suppression/drive law.)
**Answer:** 1 ns/day, 1 µs/day and 1 ms/day are **PRACTICAL as numbers**; the band's top (0.1407 s/day) and
the observed galactic field (0.0464 s/day) are **ASTROPHYSICAL ONLY**; **1 s/day is REFUTED** spontaneously —
and *every* target needs an external mode-matched driver that the canonical chain does not supply.

## Detail

| target | f = T/86400 | required Δlnρ = 3f | drive/step k=1 | drive/step k=95 | × band | equiv. well | verdict |
|--------|-------------|--------------------|----------------|-----------------|--------|-------------|---------|
| 1 ns/day | 1.157407e-14 | **3.472222e-14** | 7.436285e-18 | 2.777034e-14 | 7.11e-9 | 32.25 m/s | PRACTICAL |
| 1 µs/day | 1.157407e-11 | **3.472222e-11** | 7.436285e-15 | 2.777034e-11 | 7.11e-6 | 1019.91 m/s | PRACTICAL |
| 1 ms/day | 1.157407e-8 | **3.472222e-8** | 7.436285e-12 | 2.777034e-8 | 7.11e-3 | 32.25 km/s | PRACTICAL (numbers) |
| 1 s/day | 1.157407e-5 | **3.472222e-5** | 7.436285e-9 | 2.777034e-5 | **7.11 (>1)** | 1019.91 km/s | **REFUTED** |
| band top (G_005) | 1.628900e-6 | 4.886700e-6 | 1.046560e-9 | 3.908313e-6 | 1.00 | 382.62 km/s | ASTROPHYSICAL ONLY |
| observed galactic | 5.367333e-7 | 1.610200e-6 | 3.448485e-10 | 1.287815e-6 | 0.33 | 219.63 km/s | ASTROPHYSICAL ONLY |

| measure | result |
|---------|--------|
| steady-state profile | a pure Neumann mode `ρ = ρ̄(1 + (Δlnρ/2)v_k)`; Σρ = 1.0000000000 for all four targets; min ρ = 1.041667e-2 (1 s/day: 1.041649e-2); excursion/ρ̄ = 1.736111e-14 … 1.736111e-5 |
| drive law re-verified | the driven recursion reproduces `c·v_k/(1 − μ_k)` to **1e-9** after 150 000 steps (k = 1); the drive must be mode-matched to 1e-18 |
| positivity / count | never binding (largest excursion 1.7e-5 of ρ̄) |
| power scaling | linear in the target (7.436e-18 → 7.436e-9 exactly 1e3 steps apart); quadratic in frequency (cost(2)/cost(1) = 4.00, cost(4)/cost(2) = 4.00); `N⁻²` at fixed wavelength (1024-site lattice **113.7×** cheaper); cost per unit shift **6.4250e-4** (k = 1) vs **2.3994** (k = 95) → ratio **3734.4** |
| accumulated drive | 200 steps at k = 95: 5.554e-12 (1 ns/day) … 5.554e-3 (1 s/day) |
| detectability | all four targets are ≥1.2e4× a 1e-18 optical-clock floor — measurable once produced |
| no driver | branching arrangement-neutral (`|Δa|` < 1e-6 at 10⁶ρ), basin 1.0, undriven local lifetime **0.622 steps**, Poisson cap **4.8867e-6** |

## Verdicts

* **PRACTICAL** — 1 ns/day, 1 µs/day, 1 ms/day: ≤7.11e-3 of the accessible band, per-step drives ≤7.44e-12
  (smooth) / 2.78e-8 (cell-scale), excursions ≤1.8e-10 count units, and each ≥1.2e4× the clock floor. As
  *numbers*, given a driver.
* **ASTROPHYSICAL ONLY** — 0.0464 s/day (the observed galactic field, 33 % of the band, a 220 km/s well)
  through the band's top **0.1407 s/day** (a 383 km/s well): sustained shifts above ~10 ms/day are
  galactic/cluster-scale phenomena.
* **REFUTED** — 1 s/day spontaneously (7.105× the Poisson ceiling, a 1020 km/s well); and any sustained
  clock shift at all without an external, mode-matched, structured driver.

## Test results

| # | Test | Verdict |
|---|------|---------|
| 1 | `Y_G_010_TargetInventory` | ✅ PASS |
| 2 | `Y_G_010_RequiredDensity` | ✅ PASS |
| 3 | `Y_G_010_RequiredDriveAndProfile` | ✅ PASS |
| 4 | `Y_G_010_PowerScaling` | ✅ PASS |
| 5 | `Y_G_010_PracticalBounds` | ✅ PASS |
| 6 | `Y_G_010_NoDriver` | ✅ PASS |
| 7 | `Y_G_010_Verdicts` | ✅ PASS |
| 8 | `Y_G_010_Run` | ✅ PASS |

## Classification

**No reclassification.** D_040 is untouched; G_005's band, G_008's drive law and G_009's clock law are used
as inputs, not revised; no canonical claim, value or equation changes; no new primitive.

## Consequence

Time control is **feasible as an engineering statement about numbers and refuted as a physics statement
about what the theory produces on its own.** The numbers are strikingly small (a 1 ms/day sustained shift
needs a 3.47e-8 density contrast and a 7.4e-12 per-step drive — a 32 km/s-well-scale configuration), but
nothing in the canonical chain can supply the required mode-matched drive, and the largest shift the
accessible band permits is 0.1407 s/day — of which the observed galactic field already uses a third.
