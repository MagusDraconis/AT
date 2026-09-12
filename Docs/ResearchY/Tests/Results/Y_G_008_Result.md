# Y_G_008_Result.md — ResearchY-G_008 Controlled Suppression Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_008_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 8/8 PASSED (~0.2 s) — group G total 66/66 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_008"`

## Summary

**Question:** Can any allowed configuration maintain a high-Δρ state against `DiffuseStep` suppression?
**Answer:** **No state persists on its own.** Stationary undriven high-k states are **SUPPRESSED**
(`τ₉₅ = 0.622` steps); driven states are **STABLE** but the witness class needs a **mode-matched external
agent** injecting **0.7998 of the contrast per step**; periodic forcing is **SUPPRESSED** (DC is optimal);
boundary support is **STABLE** only for smooth profiles and capped at 3.09 % of the count; undriven smooth
profiles are **METASTABLE** (`τ₁ = 4668.80` steps — the observed galactic situation).

Allowed = `Σρ = 1` (QG194), `ρ ≥ 0`, no symmetry breaking (D_047), no non-reciprocal coupling (NP_174).

## Detail

| test | measurement | result |
|------|-------------|--------|
| **1. stationary** | `μ₀ = 1` is the unique unit eigenvalue (`ker(I − W) = span(uniform)`, 1e-15); lifetimes τₖ = −1/ln\|μₖ\|: **4668.80** (k = 1), 186.67 (5), 32.34 (12), 8.03 (24), 1.96 (48), 0.87 (72), **0.622** (95); amplitudes after 200 steps **0.9581** (k = 1) → **1.99e-140** (k = 95) | undriven high-k **SUPPRESSED**, smooth **METASTABLE** |
| **2. driven** | `ρ* = c·v_k/(1 − μ_k)` exact: iterated 0.008535533906 vs analytic 0.008535533906 (**1.2e-15**) at k = 24; `Σs = 0` required exactly; witness-driven state is allowed (`ρ_min = 0.0025 > 0`, `Σρ = 1.000000000000`, reproduces the tilt to 1.9e-15); gains 4669.30 (k = 1) → 1.2503 (k = 95) | **STABLE (driven)** |
| **3. drive power** | per unit peak-to-peak contrast: witness 10.247 vs smooth 6.546e-3 → **1566×**; per unit L1 contrast: 0.730125 vs 1.364e-4 → **5354×**; pure-mode `(1 − μ₉₅)/(1 − μ₁)` = **3734.4**; witness hold-drive L1 = **0.4868 per step** (≈ 49 % of the count), high-k share **0.9831** | the drive **is** the state |
| mode matching | a smooth-only (k ≤ 1) drive holds `HighKShare = 0` exactly (1e-12), while the witness is 0.782 | an unmode-matched drive cannot hold high-k at all |
| **4. periodic** | `sup_ω \|H_k\|` = the DC gain for **every** k, argmax `ω = 0` (4669.2968 / 187.1724 / 8.5355 / 2.5000 / 1.2503); Nyquist strictly worse (`1/(1 + μ)`); no amplifying band | **SUPPRESSED** |
| **5. boundary** | edge dipole: steady-state high-k share **1.42e-6**, k = 1 share **0.9240**, gain **120.0** per unit L1 drive; `ρ ≥ 0` caps the drive at 1.289e-4 ⇒ max contrast **0.0309 (3.09 % of the count)**; extremum at the driven edge | smooth **STABLE**, witness **SUPPRESSED** |
| **6. allowed** | the driven witness state is count-conserving, positive and structurally untouched (`A₀ = 45`, block sums identical to 1e-12); the branching flow contributes nothing (`\|Δa\|` at 10⁶ρ < 1e-6); attractor basin 1 | needs an external structured agent (NP_171 `g_c = 1.607`; NP_174) |

## Verdicts

| configuration | high-k (witness) | smooth (observed) |
|---------------|------------------|-------------------|
| stationary, undriven | SUPPRESSED (τ = 0.62 steps) | METASTABLE (τ = 4669 steps) |
| driven, mode-matched | STABLE (drive = 0.7998/step) | STABLE (drive = 2.14e-4/step) |
| periodic forcing | SUPPRESSED (gain ≤ 1.2503) | STABLE (DC optimal) |
| boundary-supported | SUPPRESSED (high-k ≈ 0) | STABLE (gain 120, capped) |

**Goal answer: no gravity-control state persists on its own** — the witness must be re-created every step.

## Test results

| # | Test | Verdict |
|---|------|---------|
| 1 | `Y_G_008_StationaryProfiles` | ✅ PASS |
| 2 | `Y_G_008_DrivenSteadyState` | ✅ PASS |
| 3 | `Y_G_008_RequiredDrivePower` | ✅ PASS |
| 4 | `Y_G_008_PeriodicForcing` | ✅ PASS |
| 5 | `Y_G_008_BoundarySupported` | ✅ PASS |
| 6 | `Y_G_008_AllowedConfigurations` | ✅ PASS |
| 7 | `Y_G_008_Verdicts` | ✅ PASS |
| 8 | `Y_G_008_Run` | ✅ PASS |

## Classification

**No reclassification.** D_040 is untouched; G_005's verdicts are *sharpened* from "not realised" to
**"not maintainable"**; no canonical claim, value or equation changes; no new primitive. The numbers are
exact functions of the G_006 spectrum at the canonical `d = 0.2`, which G_007 shows is BOUNDARY in
`T = m·d` (the price rescales with `T`; the mode-dependence does not).

## Consequence

The G-program's gravity-control question is now closed in the negative, with a price tag: **the large modes
cannot be maintained by anything the theory provides.** What persists is the smooth class — which is what
nature exhibits, and what G_004 calibrated.
