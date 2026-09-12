# Y_G_005_Result.md — ResearchY-G_005 Control Realizability Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_005_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 8/8 PASSED (~0.3 s) — group G total 40/40 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_005"`

## Summary

**Question:** Why are the large G_003 gravity-control modes not realised in nature?
**Answer:** **SUPPRESSED** — not forbidden. They conserve the count, break no symmetry and are
dynamically available in principle, but their Poisson realisation probability is
`exp(−⟨N⟩Δ²/2)` with `⟨N⟩ = 3.8569e11`: the required G_003 suppression is already reached at a
contrast of `8.1577e-6` (5.07× the observed level), and the strongest witnesses cost 10⁸–10¹⁰ nats.

**The suppression is STATISTICAL/DYNAMICAL, not thermodynamic.** The entropy channel is capped:
`ΔS ≤ ln 96 = 4.5643` ⇒ suppression `≤ 1/96 = 0.010417`, i.e. **3.6e7× short** of the required
`3.746e5`. The witness tilt costs only `ΔS = 0.272565` (a factor 1.31).

## Detail

| Channel | Measure | Result |
|---------|---------|--------|
| 1 Stability | canonical diffusion `DiffuseStep(·, 0.2)` | witness tilt CONTRACTS: std 0.00815358 → 0.000485 (50 steps) → 0.000241 (200 steps), ratio **0.0296**; H rises 4.291783 → 4.564079 toward ln 96 = 4.564348; total conserved to 12 dp; uniform is the **exact** fixed point (< 1e-15). No stationary state at the tilt ⇒ **OFF-ATTRACTOR** |
| 2 Entropy cost | `ΔS = ln 96 − H(ρ)` | uniform `H = 4.564348`; witness tilt `ΔS = 0.272565` ⇒ `e^(−ΔS) = 0.761424`; max `ΔS = ln 96` ⇒ `1/96 = 0.010417` ⇒ **INSUFFICIENT by 3.6e7×** |
| 3 Conservation | count / structure / Planck | `Σρ = 1` exact and `Σ(1/N − ρ) = 0` exactly for every G_002 operation; `A₀ = 45`, lock `0.80231` nats (cube: 884 736 modes, 20 812 eigenspaces, lock `3.948614`, `L = 0.97648`); witness tilt block sums identical (`L1 = 0`); `ρ_max = 1/l_P³ = 2.3687e104 m⁻³`; one-cell ceiling `Δln ρ ≤ ln 96 = 4.5643` |
| 4 Dynamical accessibility | canonical flow probes | `ρ_(k+1) = μρ_k` (same μ per cell) with `g_(k+1) = μ^(2/d)g_k` (residual < 1e-15) ⇒ **arrangement-neutral**; `a(λρ) = a(ρ)` to < 1e-6 at λ = 10⁶; attractor erases initial data; basin ≥ 0.9; no internal drive ⇒ modes need an EXTERNAL agent (NP_171 gate `g_c = 1.607`, `K ≥ 10.29 ω₁`, `f(g=1) = 0`) |

## The accessibility window (AT's mandatory fluctuation law, QG15/QG228/QG231)

| Contrast Δ | −ln P | P | Note |
|------------|-------|---|------|
| 1.6102e-6 | 0.500 | 0.6065 | **observed** — typical |
| 1.8959e-6 | 0.693 | 0.500 | median |
| 3.4554e-6 | 2.303 | 0.100 | 10 % cut |
| 4.8867e-6 | 4.605 | 0.010 | 1 % cut → **accessibility ceiling** |
| 8.1577e-6 | 12.834 | 2.7e-6 | **G_003 requirement reached** (5.07× observed) |
| 0.032121 | 1.99e8 | 10^(−8.6e7) | survivor compression |
| 0.15151 | 4.43e9 | 10^(−1.9e9) | G_003 canonical witness |
| 0.603175 | 7.02e10 | 10^(−3.0e10) | degeneracy redistribution |

`⟨N⟩ = 1/δ² = 3.8569176e11` from the observed contrast, i.e. the observed field is a *typical*
fluctuation of the counting measure.

## Verdicts

* **ACCESSIBLE** — the attractor/uniform state (zero field), the phase directions, and every
  fluctuation `Δ ≤ 4.8867e-6`, **including the observed galactic field** `1.6102e-6` (P = 0.61).
* **SUPPRESSED** — arrangement `0.685714`, degeneracy redistribution `0.603175`, D96 vs random
  `0.333333`, D96³ vs D96 `0.276596`, survivor compression `0.032121`: count-conserving,
  symmetry-preserving, dynamically available — but Poisson-suppressed by 10⁸–10¹⁰ nats,
  off-attractor, and not driven internally.
* **FORBIDDEN** — `Σρ ≠ 1` / `dM ≠ 0`; a changed `A₀` or mirror pairing without a
  symmetry-breaking agent (D_047 protection); a cell above the Planck ceiling `1/l_P³`; any contrast
  above `ln 96`. None of these is a `ρ`-operation.

## Test results

| # | Test | Verdict |
|---|------|---------|
| 1 | `Y_G_005_AccessibilityWindow` | ✅ PASS |
| 2 | `Y_G_005_SuppressionExponent` | ✅ PASS |
| 3 | `Y_G_005_EntropyCostInsufficient` | ✅ PASS |
| 4 | `Y_G_005_Stability` | ✅ PASS |
| 5 | `Y_G_005_ConservationConstraints` | ✅ PASS |
| 6 | `Y_G_005_DynamicalAccessibility` | ✅ PASS |
| 7 | `Y_G_005_Verdicts` | ✅ PASS |
| 8 | `Y_G_005_Run` | ✅ PASS |

## Classification

**No reclassification.** The D_040 `ClassificationRegistry` is untouched; no canonical claim, value
or equation changes; no new primitive. The Poisson channel uses AT's own mandatory fluctuation law in
the *forward* direction (observed contrast → ⟨N⟩), and the lock gate `g_c = 1.607` is NP_171's
**EMERGENT**, imported result — cited, never recomputed, and no verdict depends on its exact value.
The diffusion contraction uses canonical `d = 0.2` and holds for any `d ∈ (0, 0.5)`.

## Consequence for G_003/G_004

G_004's `CALIBRATED` verdict is now explained **dynamically** rather than merely observed: the
gravity source is `ρ` (G_001), its controllable part is real but statistically inaccessible
(G_002/G_005), and its observable magnitude is set by the counting law — the observed field is the
typical, Poisson-natural part of the counting measure, and the huge G_003 modes are an empty tail.
