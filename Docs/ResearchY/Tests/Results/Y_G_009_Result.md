# Y_G_009_Result.md — ResearchY-G_009 Clock Rate Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_009_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 8/8 PASSED (~0.06 s) — group G total 74/74 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_009"`

## Summary

**Question:** Does the actualization density ρ change clock rates? Given `g₀₀ = −ρ^(2/d)`.
**Answer:** `dτ/dt = ρ^(1/d)`, so `(1/d)Δlnρ = ΔΦ/c²` — **DERIVED**; AT and GR agree to first order and split
at second order by ±½(Φ/c²)² (**CORRELATED**); no *realised, sustainable* clock effect follows from a ρ
rearrangement (**REFUTED**).

**Critical question** (measurable time dilation without mass-energy changes?): **YES in the theory** — up to
**22.86 %** (19 749 s/day, 2.3e17× the clock floor) at `Σm = 0` exactly — **NO physically** (G_005 caps
realised contrasts at 4.8867e-6; G_008 makes the witnesses unmaintainable). What *is* realised agrees with
GR's galactic depth to 0.33 %.

## Detail

| case | ΔΦ/c² | rate deviation | physical clock | comparator |
|------|-------|----------------|----------------|------------|
| **Earth surface vs ∞** | −6.9613e-10 | −6.9613e-10 | **−60.145 μs/day** | −60.15 (measured); AT ≡ GR to double precision |
| **GPS orbit (grav.)** | 5.2940e-10 | 5.2940e-10 | **+45.740 μs/day** | 45.7 (QG187); AT Δlnρ = 1.5882e-9 |
| GPS (SR, imported) | −8.3365e-11 | — | −7.203 μs/day | −7.2 |
| **GPS total** | 4.4603e-10 | 4.4603e-10 | **+38.537 μs/day** | 38.6 (ratio 0.9984) |
| **Galactic field** | 5.367333e-7 | 5.367333e-7 | **46.374 ms/day** | `v²/c²` = 5.385226e-7 → ratio **0.99668**; equivalent v = **219.63 km/s** |
| **arrangement** | 0.228571 | 0.228571 | **19 748.6 s/day** | 2.29e17× the clock floor; Δlnρ = 0.685714 |
| degeneracy redistribution | 0.201058 | 0.201058 | 17 371.4 s/day | Δlnρ = 0.603175 |
| D96 vs random | 0.111111 | 0.111111 | 9 600.0 s/day | Δlnρ = 0.333333 |
| D96³ vs D96 | 0.092199 | 0.092199 | 7 966.0 s/day | Δlnρ = 0.276596 |
| survivor compression | 0.010707 | 0.010707 | 925.1 s/day | Δlnρ = 0.032121 |
| realised band (G_005 ceiling) | ≤1.629e-6 | ≤1.629e-6 | 0.1407 s/day | 3.03× the observed level |
| observed level | 5.367e-7 | 5.367e-7 | 0.0464 s/day | 5.37e11× the clock floor |

| structured statement | measurement |
|----------------------|-------------|
| clock law | `dτ/dt = ρ^(1/d)`; rate change 0.256803 for Δlnρ = 0.685714 = 0.228571 (1st) + 0.026122 (2nd) |
| scale invariance | `ρ → λρ` moves every clock by `λ^(1/d)`; the **relative** change is exactly 0 |
| AT vs GR | `exp(x) = 1 + x + x²/2` vs `sqrt(1+2x) = 1 + x − x²/2`; at x = 1e-5 coefficients **+0.500001 / −0.499995**, difference `x² = 1.0000e-10` |
| second order in situ | `x² = 4.846e-19` (Earth), `2.803e-19` (GPS) — **below** the 1e-18 optical-clock floor |
| phase directions | exactly zero clock change |
| derived-G offset | a **common factor** (0.9959996): cancels in any ratio of clock rates |
| no-realised-effect chain | G_005 ceiling 4.8867e-6 (1 %) and G_008 lifetime 0.622 steps / 0.7998 injection per step |

## Verdicts

* **DERIVED** — the clock law and its first-order redshift form; Earth −60.145 μs/day and GPS gravitational
  +45.740 μs/day (AT ≡ GR ≡ observation); the **new galactic cross-check** (0.33 %); the redistribution rate
  changes at fixed total mass-energy.
* **CORRELATED** — the GPS total (imported SR term, 0.16 %); the AT/GR second-order split (+½ vs −½, 1e-19).
* **REFUTED** — a *realised, sustained* clock effect from a ρ rearrangement; the phase directions; a uniform
  rescaling as a relative effect.

## Test results

| # | Test | Verdict |
|---|------|---------|
| 1 | `Y_G_009_ClockLaw` | ✅ PASS |
| 2 | `Y_G_009_EarthAndGps` | ✅ PASS |
| 3 | `Y_G_009_GalacticField` | ✅ PASS |
| 4 | `Y_G_009_Redistributions` | ✅ PASS |
| 5 | `Y_G_009_GrComparison` | ✅ PASS |
| 6 | `Y_G_009_CriticalQuestion` | ✅ PASS |
| 7 | `Y_G_009_Verdicts` | ✅ PASS |
| 8 | `Y_G_009_Run` | ✅ PASS |

## Classification

**No reclassification.** D_040 is untouched; G_003's potential channel and G_004's GPS check are extended,
not revised; no canonical claim, value or equation changes; no new primitive. The only AT-specific local
signature is the ±½ second-order coefficient at 1e-19 (below the 1e-18 clock floor) plus the 0.40 % derived
`G` (a common scale factor).

## Consequence

The clock channel is the G-program's *cleanest* agreement: AT reproduces the measured solar-system clock
effects exactly, adds a parameter-free galactic cross-check at 0.33 %, and its only distinguishing local
prediction sits a factor 2–4 below current clock resolution. Meanwhile the dramatic fixed-energy
redistributions — up to 22.86 % clock changes with `Σm = 0` — remain theoretically exact but physically
unreachable, closing the gravity-control question a second time (this time in clock units).
