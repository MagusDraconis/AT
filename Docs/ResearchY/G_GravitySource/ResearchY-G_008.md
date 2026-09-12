# ResearchY-G_008 — Controlled Suppression Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_008 (permanent)
**Title:** Controlled Suppression Audit — can any allowed configuration maintain a high-Δρ state?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_008.md`
**Depends on:** ResearchY-G_002 (the free room), G_003 (the large witnesses), G_005 (SUPPRESSED verdict),
G_006 (the mechanism = the relaxational low-pass filter), G_007 (the operator's origin, the 34× at
`T = m·d = 40`, time refuted); AT-QG QG1, QG194 (count conservation); D_047 (structural protection);
NP_171 (the lock gate, IMPORTED), NP_174 (no non-reciprocal coupling);
`AT.Core/ResearchXH/RhoDynamics.cs`, `UniversalAttractor.cs`, `NativeMetricDynamics.cs`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_008_Tests.cs` (8/8 PASSED, ~0.2 s)

## Purpose

G_005–G_007 established that the relaxation operator (`DiffuseStep`) is the derived, unique low-pass filter
that suppresses the G_002/G_003 gravity-control modes. G_008 asks the *engineer's* question:

> **Can any ALLOWED configuration maintain a high-Δρ state against that suppression?**

Testing four classes — **stationary**, **driven**, **periodically forced**, **boundary-supported** — and
measuring the **suppression factor**, the **lifetime** and the **required drive power**. "Allowed" =
count-conserving (`Σρ = 1`, QG194), `ρ ≥ 0`, no symmetry breaking (D_047), no non-reciprocal coupling
(NP_174), no new primitive.

**Answer.** Nothing persists on its own. The witness class has a **sub-step lifetime** (0.622 steps) and can
be held **only** by a **mode-matched, structured external agent** that injects **0.7998 of the contrast every
step** — a state that is *re-created*, not maintained. The only naturally persistent high-contrast states are
**smooth**, which is exactly the observed class: **metastable** on a 4669-step relaxation horizon.

## 0. The three measures

| measure | definition | D96 values |
|---------|-----------|------------|
| suppression factor | the G_006/G_007 `r(m)`; here reported through the **gain** `1/(1 − μ_k)` | witness 1.2503, smooth 4669.30 |
| lifetime | `τ_k = −1/ln\|μ_k\|` steps for a 1/e decay | τ₉₅ = **0.622**, τ₄₈ = 1.958, τ₂₄ = 8.025, τ₁ = **4668.80** |
| required drive power | the drive needed to hold a profile: `s* = (I − W)ρ*`, in units of the L1 contrast (and of the total count) | witness 0.7301 per unit L1 contrast (L1 = 0.4868 ≈ 49 % of the count per step); smooth 1.364e-4 |

## 1. Stationary profiles — only the uniform measure survives undriven

`μ₀ = 1` is the **unique** unit eigenvalue (`ker(I − W) = span(uniform)`, verified to 1e-15), so the only
undriven stationary profile is the uniform counting measure — zero contrast, zero field. Every other profile
converges to it, at a mode-dependent speed:

| k | 1 | 5 | 12 | 24 | 48 | 72 | 95 |
|---|---|---|----|----|----|----|----|
| μ_k | 0.99978583 | 0.99465733 | 0.96955181 | 0.88284271 | 0.60000000 | 0.31715729 | 0.20021417 |
| lifetime τ_k (steps) | **4668.80** | 186.67 | 32.34 | 8.03 | 1.96 | 0.87 | **0.622** |
| amplitude after 200 steps | **0.9581** | 0.3425 | 2.06e-3 | 1.50e-11 | 4.27e-45 | 1.80e-100 | **1.99e-140** |
| gain `1/(1 − μ_k)` | **4669.30** | 187.17 | 32.84 | 8.54 | 2.50 | 1.46 | **1.2503** |

**Verdict: undriven high-k = SUPPRESSED (sub-step lifetime); undriven smooth = METASTABLE** — long-lived,
*not* stable. This is exactly the observed situation: the galactic AT field is a smooth, scale-free deficit
that keeps 95.8 % of its amplitude over 200 relaxation steps.

## 2. Driven profiles — a genuine steady state, at a mode-dependent price

The driven recursion `ρ ← Wρ + s` with a **mode-matched** drive `s = c·v_k` has the **exact** steady state

```
rho* = c * v_k / (1 - mu_k)
```

verified by iterating the driven dynamics (`k = 24`, `c = 1e-3`: iterated 0.008535533906 vs analytic
0.008535533906, relative error **1.2e-15**). A steady state also requires `Σs = 0` **exactly** (a uniform
drive would change the count and is not an allowed configuration).

The witness-driven state is itself an **allowed configuration**: `ρ_min = 0.0025 > 0`, `Σρ = 1.000000000000`,
and it reproduces the witness tilt to 1.9e-15. **Verdict: STABLE (driven).**

But the price is mode-dependent, and the witness class sits at the expensive end:

| normalisation | witness | smooth (k = 1) | ratio |
|---------------|---------|----------------|-------|
| per unit **peak-to-peak** contrast | 10.247 | 6.546e-3 | **1566×** |
| per unit **L1** contrast | 0.730125 | 1.364e-4 | **5354×** |
| pure-mode (`(1 − μ₉₅)/(1 − μ₁)`) | 0.7998 | 2.1417e-4 | **3734.4×** |

The clean statement: **the highest mode must be injected at 0.7998 of its amplitude per step; the lowest only
at 2.14e-4.** Gravity control of the witness class is not a maintenance problem but a **re-creation**
problem: the drive *is* the state.

## 3. Mode matching — the steady state is a filtered copy of the drive

Because `ρ* = (I − W)^{-1}s`, the steady state contains **only the modes the drive contains** (attenuated by
`1/(1 − μ_k)`). A smooth-only drive therefore holds **exactly zero** high-k content: with the witness
hold-drive projected onto `k ≤ 1`, `HighKShare(ρ*) = 0` (to 1e-12), while the witness itself is
`HighKShare = 0.782`. The witness hold-drive is itself `98.3 %` high-k. So **an unstructured drive cannot
hold a high-k state at all** — not inefficiently, but structurally.

## 4. Periodic forcing — DC is optimal, nothing resonates

The periodic response is `|H_k(ω)| = 1/\|1 − e^{iω}μ_k\|`. Sweeping ω:

| k | μ_k | DC gain | `sup_ω \|H_k\|` | argmax |
|---|-----|---------|-----------------|--------|
| 1 | 0.999786 | 4669.2968 | 4669.2968 | **ω = 0** |
| 5 | 0.994657 | 187.1724 | 187.1724 | **ω = 0** |
| 24 | 0.882843 | 8.5355 | 8.5355 | **ω = 0** |
| 48 | 0.600000 | 2.5000 | 2.5000 | **ω = 0** |
| 95 | 0.200214 | 1.2503 | 1.2503 | **ω = 0** |

The supremum is the **DC gain for every k**, attained at `ω = 0`: a time-dependent drive can never beat a
static one. A Nyquist (alternating) drive is strictly worse (`1/(1 + μ)`), there is **no amplifying band**,
and the only near-resonance is `|μ| → 1` — the marginal regime where nothing needs suppressing.
**Verdict: SUPPRESSED.**

## 5. Boundary-supported profiles — smooth only, capped by ρ ≥ 0

A mean-zero drive supported **only on the edges** (a boundary dipole) has a steady state that is
essentially perfectly smooth:

| quantity | value |
|----------|-------|
| high-k (k ≥ 48) share of the steady profile | **1.42e-6** |
| k = 1 share | **0.9240** |
| gain (contrast per unit L1 drive) | **120.0** (vs 1.2503 for the highest mode) |
| `ρ ≥ 0` cap on the drive | `c ≤ 1.289e-4` |
| ⇒ maximum boundary-supported contrast | **0.0309 (3.09 % of the count)** |

The extremum sits at the driven edge (a boundary ramp). **Verdict: STABLE for smooth profiles; SUPPRESSED
for the witness class** — the boundary solution contains no high-k content at all.

## 6. Verdict — can any gravity-control state persist?

| configuration | high-k (witness class) | smooth (observed class) |
|---------------|------------------------|-------------------------|
| stationary, undriven | **SUPPRESSED** (τ = 0.62 steps) | **METASTABLE** (τ = 4669 steps) |
| driven, mode-matched | **STABLE** (drive = 0.7998/step) | **STABLE** (drive = 2.14e-4/step) |
| periodic forcing | **SUPPRESSED** (gain ≤ 1.2503) | STABLE (DC optimal) |
| boundary-supported | **SUPPRESSED** (high-k ≈ 0) | STABLE (gain 120, capped at 3.1 %) |

**GOAL ANSWER: NO gravity-control state persists on its own.** The witness class lives 0.62 steps undriven
and can be held only by a mode-matched structured external agent injecting 0.7998 of the contrast per step
(L1 = 0.4868 against a total count of 1). The canonical chain supplies **no such agent**: the branching flow
is arrangement-neutral (G_006/G_007) and the lock gate `g_c = 1.607` is an imported input (NP_171), with no
non-reciprocal coupling available to self-amplify (NP_174). The only naturally persistent high-contrast
states are **smooth** — the observed class — and they are metastable rather than stable.

## 7. Classification and caveats

**No reclassification.** D_040 is untouched; G_005's ACCESSIBLE/SUPPRESSED/FORBIDDEN verdicts stand and are
here sharpened from *"not realised"* to ***"not maintainable"***: the state must be re-created every step.
No canonical claim, value or equation changes; no new primitive.

* The gain/lifetime/drive numbers are exact functions of the G_006 spectrum and the canonical `d = 0.2`; by
  G_007 they are BOUNDARY in `T = m·d` (the price would rescale with `T`, the mode-dependence would not).
* The operator remains the canonical relaxation (G_007 locality/isotropy/conservation/positivity/scale-free
  uniqueness).
* The `ρ ≥ 0` cap is a genuine constraint on *driven* steady states, not a modelling choice.

## 8. Open problems (OP1–OP4)

1. Which physical process could supply a mode-matched drive at 0.7998 per step? (None in the chain; NP_171's
   gate is imported and NP_174 closes the self-amplification route.)
2. Can a driven *smooth* state at the 3.1 % contrast cap be distinguished observationally from the ambient
   galactic field? (The observed contrast is 1.6102e-6 — far below the cap.)
3. Is the boundary-dipole Green's function the optimal boundary drive, or can a different edge profile beat
   its 120× gain for the same positivity cost?
4. Does the metastable smooth lifetime (4669 steps) survive the index identification of G_007 OP3?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_008_Tests.cs` — **8/8 PASSED** (~0.2 s)
**Group total:** G_001–G_008 = **66/66 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_008"`

| label | content |
|-------|---------|
| **STABLE** | driven steady states (every mode, count-conserving and positive) and smooth boundary-supported profiles — but the witness needs a mode-matched external agent at 0.7998 of the contrast per step, and boundary support is capped at 3.09 % of the count |
| **METASTABLE** | undriven smooth profiles: `τ₁ = 4668.80` steps, 95.81 % of the amplitude after 200 steps (the observed galactic situation) |
| **SUPPRESSED** | every undriven high-k state (τ₉₅ = 0.622 steps, 1.99e-140 after 200 steps); periodic forcing (sup gain = DC gain, no amplifying band); boundary-only drives for the witness class (high-k share 1.42e-6); and any unmode-matched drive (zero high-k content) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_005.md` … `ResearchY-G_007.md`
* `Docs/ResearchY/Tests/Results/Y_G_008_Result.md`
* `AT.Core/ResearchXH/RhoDynamics.cs` (`DiffuseStep`), `UniversalAttractor.cs`, `NativeMetricDynamics.cs`
* ResearchY-NP_171 (the lock gate, IMPORTED), ResearchY-NP_174 (no non-reciprocal coupling)
