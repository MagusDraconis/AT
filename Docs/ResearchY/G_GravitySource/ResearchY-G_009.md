# ResearchY-G_009 — Clock Rate Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_009 (permanent)
**Title:** Clock Rate Audit — does the actualization density ρ change clock rates?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_009.md`
**Depends on:** ResearchY-G_001 (ρ is the source), G_002 (the redistributions at fixed energy),
G_003 (the potential channel and the 1.6102e-6 ambient calibration), G_004 (the GPS/calibration checks),
G_005 (the Poisson ceiling), G_008 (nothing is maintainable); AT-QG **QG197** (`g₀₀ = −ρ^(2/d)`),
**QG21/QG187** (the redshift law and the GPS 45.7 μs/day), QG103 (perihelion), QG181 (the derived G),
QG212 (PPN γ = β = +1)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_009_Tests.cs` (8/8 PASSED, ~0.06 s)

## Purpose

Given `g₀₀ = −ρ^(2/d)` (QG197), the clock rate is

```
dtau/dt = sqrt(-g_00) = rho^(1/d)      =>      (rate - 1) = (1/d) Delta ln rho = Delta Phi/c^2
```

the canonical redshift law (QG21/QG187), which G_003 anchored on the Earth-vs-GPS potential (45.74 vs
45.7 μs/day). G_009 computes `dτ/dt` for four cases — **Earth surface**, **GPS orbit**, **Galactic field**,
**G_002 density redistributions** — compares AT with GR, and answers the critical question:

> **Can ρ variations produce measurable time dilation without mass-energy changes?**

**Answer.** **DERIVED** (the law, the Earth/GPS magnitudes, the AT/GR first-order identity, a new galactic
cross-check) · **CORRELATED** (AT and GR differ at second order by ±½(Φ/c²)²; the GPS total needs the
imported SR term) · **REFUTED** (no *realised, sustainable* clock effect from a ρ rearrangement; the phase
directions and a uniform rescaling are exactly clock-neutral).

## 1. The clock law

| statement | value |
|-----------|-------|
| `dτ/dt = ρ^(1/d)` from `g₀₀ = −ρ^(2/d)` | exact |
| `Δ(dτ/dt) = (1/d)Δlnρ = ΔΦ/c²` to first order | exact |
| the arrangement witness (`Δlnρ = 0.685714`) | rate change **0.256803** = 0.228571 (1st) + 0.026122 (2nd) |
| a uniform rescaling `ρ → λρ` | moves *every* clock by `λ^(1/d)`; the **relative** change is **exactly 0** |
| two cells with densities `ρ_A, ρ_B` | rate ratio `= (ρ_B/ρ_A)^(1/d)` — only the ratio is physical |

## 2. Cases 1 and 2 — Earth surface and the GPS orbit

| case | ΔΦ/c² | AT rate dev | GR rate dev | μs/day | observed |
|------|-------|-------------|-------------|--------|----------|
| Earth surface vs ∞ | −6.9613e-10 | −6.9613e-10 | −6.9613e-10 | **−60.145** | −60.15 ✓ |
| GPS orbit (gravitational) | 5.2940e-10 | 5.2940e-10 | 5.2940e-10 | **+45.740** | 45.7 (QG187) ✓ |
| GPS (SR, imported) | −8.3365e-11 | — | — | −7.203 | −7.2 ✓ |
| **GPS total** | 4.4603e-10 | 4.4603e-10 | 4.4603e-10 | **+38.537** | 38.6 (ratio 0.9984) ✓ |

AT's own input for the GPS potential difference is `Δlnρ = d·ΔΦ/c² = 1.5882e-9` (matching G_003's
1.588e-9). **AT ≡ GR to double precision at first order**; the second-order difference is `x²`:
**4.846e-19** at the Earth's surface and **2.803e-19** at the GPS orbit — 2–4× **below** the 1e-18 floor of
the best optical clocks (a *future* test, not a present discrepancy).

## 3. Case 3 — the Galactic field: a new cross-check

The G_003 ambient calibration (the observed galactic AT field = `Δlnρ = 1.6102e-6`) gives

| quantity | value |
|----------|-------|
| AT: `Δlnρ/d` | **5.367333e-7** = **46.374 ms/day** |
| GR/kinematic: `v²/c²` for a flat curve at 220 km/s | **5.385226e-7** = **46.528 ms/day** |
| ratio | **0.99668** (−0.33 %), equivalent rotation velocity **219.63 km/s** |
| exact-match contrast | `Δlnρ = 1.615568e-6` vs the calibration's 1.6102e-6 |
| detectability | **5.367e11×** an optical clock's 1e-18 floor |

**This is a genuinely new consistency:** the density contrast calibrated in G_003 from
`g† = cH₀/(2π)` — with no fitted parameters — reproduces the *observed galactic potential depth* (i.e. the
rotation-curve value `v²/c²`) to **0.33 %**. The Galactic clock effect is ~46 ms/day across the disk: large
by clock standards, but it is exactly the depth GR also predicts.

## 4. Case 4 — the G_002 redistributions at fixed total mass-energy

All witnesses conserve the count exactly (`Σρ = 1`, deficit `Σ(ρ̄ − ρ) = 0`), so mass-energy is fixed:

| configuration | Δlnρ | rate change `Δlnρ/d` | s/day | × clock floor |
|---------------|------|----------------------|-------|---------------|
| arrangement | 0.685714 | **0.228571** | 19 748.6 | 2.29e17 |
| degeneracy redistribution | 0.603175 | 0.201058 | 17 371.4 | 2.01e17 |
| D96 vs random | 0.333333 | 0.111111 | 9 600.0 | 1.11e17 |
| D96³ vs D96 | 0.276596 | 0.092199 | 7 966.0 | 9.22e16 |
| survivor compression | 0.032121 | 0.010707 | 925.1 | 1.07e16 |
| **realised band (G_005 ceiling)** | ≤4.8867e-6 | **≤1.629e-6** | 0.1407 | 1.63e12 |
| **observed level** | 1.6102e-6 | 5.367e-7 | 0.0464 | 5.37e11 |

The witnesses are **3.746e5×** the observed level (G_003's ratio, now in clock units); the accessible band
tops out at only **3.03×** the observed galactic level. The phase directions (G_002 REFUTED) leave ρ — hence
the rate — *exactly* unchanged.

## 5. AT versus GR

```
AT:  exp(x)      = 1 + x + x^2/2      GR:  sqrt(1 + 2x) = 1 + x - x^2/2       (x = Phi/c^2)
```

verified at `x = 1e-5`: coefficients **+0.500001** and **−0.499995**, difference `x² = 1.0000e-10`.
First order is **identical** (DERIVED); the second-order terms have **opposite sign** (CORRELATED), so AT
clocks run slightly *fast* relative to GR. The only AT-specific *local* content is therefore (i) the derived
`G` at 0.40 % — a **common factor** that cancels in any ratio of clock rates — and (ii) the ±½ second-order
coefficient at 1e-19.

## 6. The critical question

> Can ρ variations produce measurable time dilation **without mass-energy changes**?

**YES in the theory.** `Σm = 0` exactly for every G_002 operation, yet `(1/d)Δlnρ` reaches **0.228571**
(22.86 %, 19 749 s/day, **2.3e17×** an optical clock's floor): the clock rate depends on the *arrangement*
of ρ, not on its total. (Every potential theory shares this generic feature; the AT-specific content is the
exact relation `Δ(dτ/dt) = Δlnρ/d` and the **degeneracy** freedom, which needs no new mass and leaves the
spectrum and `A₀` untouched.)

**NO physically.** Two independent audits close the route: G_005 caps realised contrasts at **4.8867e-6**
(Poisson counting — so ≤1.63e-6 in clock units), and G_008 shows even a *driven* witness lives **0.622
steps** and needs **0.7998** of its amplitude injected every step.

**What IS realised is exactly what GR already predicts:** 5.367e-7 (46.4 ms/day) across the galactic disk,
agreeing with `v²/c²` to 0.33 %. Locally AT = GR with the derived `G`, and the AT-specific local signature
is 1e-19 — below today's clock floor.

## 7. Verdict

| claim | verdict |
|-------|---------|
| `dτ/dt = ρ^(1/d)` from `g₀₀ = −ρ^(2/d)` | **DERIVED** |
| `(1/d)Δlnρ = ΔΦ/c²` (the redshift law) | **DERIVED** |
| Earth −60.145 μs/day; GPS gravitational +45.740 μs/day (AT ≡ GR ≡ observation) | **DERIVED** |
| GPS total +38.537 vs 38.6 observed | **CORRELATED** (the SR term is imported) |
| Galactic 5.367e-7 vs `v²/c²` — agreement to 0.33 % | **DERIVED / CORRELATED** (new cross-check) |
| AT vs GR first order | **DERIVED** (identical) |
| AT vs GR second order (+½ vs −½, 4.8e-19 at Earth) | **CORRELATED** (below the 1e-18 floor) |
| Redistribution rate changes up to 22.86 % at fixed total mass-energy | **DERIVED** (theoretically exact) |
| A *realised, sustained* clock effect from a ρ rearrangement | **REFUTED** (G_005 not counted; G_008 not maintainable) |
| Phase directions change the clock rate | **REFUTED** (exactly zero) |
| A uniform rescaling produces a relative clock effect | **REFUTED** (a gauge: exactly zero) |

## 8. Classification and caveats

**No reclassification.** D_040 is untouched; G_003's potential channel and G_004's GPS check are here
extended, not revised. No canonical claim, value or equation changes; no new primitive.

* The clock law is *derived from* `g₀₀ = −ρ^(2/d)` (QG197) — the AT-specific input is the conformal
  exponent `2/d`; the first-order agreement with GR is then automatic because the redshift law is the same.
* The 0.33 % galactic agreement is **not** a fit: G_003's contrast came from `g† = cH₀/(2π)`, and the
  comparator `v²/c²` uses the measured rotation velocity (220 km/s).
* The second-order `±½` difference is a *prediction* of the exponential conformal factor; it is unmeasurable
  today (1e-19 vs a 1e-18 floor).

## 9. Open problems (OP1–OP4)

1. An optical-clock comparison at **1e-19** fractional resolution (2–4× beyond today's best) would resolve
   the sign of the second-order coefficient — the only *local* clock test that distinguishes AT from GR.
2. Can a *realised* Galactic clock comparison (pulsar timing across the disk, 46 ms/day) constrain the
   ambient contrast independently of the rotation curve?
3. Does the 0.33 % galactic agreement tighten or loosen when the contrast is derived from a different
   anchor (`a₀` literature mean vs combined determination)?
4. The index/step identification of G_007 OP3 is inherited here through the lifetime and drive numbers of
   G_008, which set the "no realised effect" part of the verdict.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_009_Tests.cs` — **8/8 PASSED** (~0.06 s)
**Group total:** G_001–G_009 = **74/74 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_009"`

| label | content |
|-------|---------|
| **DERIVED** | `dτ/dt = ρ^(1/d)` and `(1/d)Δlnρ = ΔΦ/c²`; Earth −60.145 μs/day and GPS gravitational +45.740 μs/day (AT ≡ GR ≡ observation); the galactic cross-check (5.367e-7 vs `v²/c²`, 0.33 %); the redistribution rate changes at fixed total mass-energy (up to 22.86 %, 19 749 s/day) |
| **CORRELATED** | the GPS total (the SR term is imported, 0.16 % agreement); the AT/GR second-order split (+½ vs −½: 4.846e-19 at the Earth's surface, 2.803e-19 at GPS, below the 1e-18 floor) |
| **REFUTED** | any *realised, sustained* clock effect from a ρ rearrangement (G_005: not counted; G_008: not maintainable); the phase directions (exactly zero); a uniform rescaling (a gauge — no relative effect) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_003.md`, `G_004.md`, `G_005.md`, `G_008.md`
* `Docs/ResearchY/Tests/Results/Y_G_009_Result.md`
* `AT.Tests/Shared/PhysicalUnits.cs` (GM_⊕, R_⊕, R_GPS, GDagger, G_SI/G_CODATA)
* QG197 (`g₀₀ = −ρ^(2/d)`), QG21/QG187 (the redshift law, GPS 45.7 μs/day), QG103, QG212
