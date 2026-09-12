# ResearchY-G_005 — Control Realizability Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_005 (permanent)
**Title:** Control Realizability Audit — why are the large G_003 gravity-control modes not realised?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_005.md`
**Depends on:** ResearchY-G_001 (ρ sources gravity), G_002 (ρ controllable at fixed energy),
G_003 (the large theoretical Δg), G_004 (observed fields are small and calibrated), D_028/D_040
(the classification guard), D_047 (the exact mirror-pairing lock), QG194 (count conservation),
QG15/QG228/QG231 (AT's mandatory fluctuation law)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_005_Tests.cs` (8/8 PASSED, ~0.3 s)
**Shared machinery:** `AT.Tests/Shared/DensityField.cs`, `AT.Tests/Shared/PhysicalUnits.cs`,
`AT.Core/ResearchXH/RhoDynamics.cs`, `AT.Core/ResearchXH/UniversalAttractor.cs`,
`AT.Core/ResearchXH/NativeMetricDynamics.cs`, `AT.Core/ResearchXH/InitialConditionsOrigin.cs`

## Purpose

G_002 established that the actualization density `ρ` is controllable at *fixed* total energy
(51 free directions = `N − A₀`). G_003 established that the corresponding physical gravity change is
**large** at astrophysical scales — the witness reconfigurations give 10⁻⁶ g already for lengths
below the 9.54 kpc threshold — and that any *realised* reconfiguration must therefore be suppressed
by at least **3.746 × 10⁵**. G_004 showed that the observed fields are smaller by exactly that
factor (`g† = 1.06 × 10⁻¹¹ g`).

G_005 asks the dynamical question that G_003 deferred: **why are the large modes not realised?**
Four channels are tested — stability, entropy cost, conservation constraints, dynamical
accessibility — and every `ρ` configuration is classified **ACCESSIBLE / SUPPRESSED / FORBIDDEN**.

**Answer.** The large modes are **SUPPRESSED**, not forbidden. They violate no conservation law and
break no symmetry — they are simply never counted. The suppression is **statistical and dynamical,
not thermodynamic**: the configuration-entropy channel is capped at ln 96 = 4.5643 nats (a 1/96
suppression, 3.6 × 10⁷× short of what is required), while AT's *own* mandatory fluctuation law
(Poisson counting, δ = 1/√⟨N⟩) makes the observed contrast `1.6102e-6` a *typical* fluctuation
(P = 0.61) and the witness contrast `0.15151` a fluctuation with probability 10^(−1.9×10⁹).

## 0. What enters (the input inventory)

| Input | Value | Source | Status |
|-------|-------|--------|--------|
| D96 counting measure | 96 cells, `A₀ = 45`, `L = 0.53125` | D-program (exact) | INPUT |
| Witness reconfiguration | `Δln ρ = 0.15151 → 10⁻⁶ g` at 15 kpc | G_003 | DERIVED |
| Strongest internal witness | `Δln ρ = 0.603175` (degeneracy redistribution) | G_002 | DERIVED |
| Required suppression | `3.746 × 10⁵` | G_003 §3 | DERIVED |
| Observed contrast | `Δln ρ = 1.6102e-6` over 15 kpc | G_003 ambient calibration | DERIVED |
| Diffusion damping | `d = 0.2` | `UniversalAttractor.DefaultDamping` | INPUT (canonical) |
| Attractor parameters | `feedback = 0.7`, `K = 6`, links = `N·K` | `UniversalAttractor` | INPUT (canonical) |
| Lock threshold | `g_c = 1.607`, `K ≥ 10.29 ω₁`, `f(g=1) = 0` | NP_171 | IMPORTED/EMERGENT |

The **only** new imported number is NP_171's lock gate; it is cited, never recomputed, and every
G_005 conclusion holds without it (the gate only quantifies *what an external drive would have to
supply*).

## 1. Channel 1 — stability: the large modes are OFF-ATTRACTOR

The canonical attractor is an **exact fixed point** (`UniversalAttractor.IsExactFixedPoint`), the
basin is total (`BasinFraction ≥ 0.9` over 12 deterministic samples), perturbations recover
(`PerturbationRecovers`), the residual is < 10⁻⁹, and the attractor is universal (`N·K = 576` links).

The uniform counting measure is the **exact fixed point of the canonical scale-space diffusion**:
`DiffuseStep(ρ_uniform, 0.2) = ρ_uniform` to < 10⁻¹⁵. A G_002 witness tilt is *not* that fixed
point and contracts monotonically:

| steps | std(ρ) | ratio | H(ρ) |
|-------|--------|-------|------|
| 0 | 0.00815358 | 1.0000 | 4.291783 |
| 50 | 0.000485 | 0.0595 | 4.563261 |
| 200 | 0.000241 | **0.0296** | 4.564079 |

The total is conserved to 12 dp at every step, and the entropy rises to within 3 × 10⁻⁴ of the
uniform maximum ln 96 = 4.564348. **The flow moves away from the tilt and toward uniformity**: there
is no stationary state at the tilt, so any large-contrast configuration is *transient* unless it is
continuously driven. Verdict for the witness modes: **OFF-ATTRACTOR**.

## 2. Channel 2 — entropy cost: capped, therefore INSUFFICIENT

This is the audit's key **negative** result. The configuration-entropy channel gives a Boltzmann
factor `e^(−ΔS)`:

* `H(uniform) = ln 96 = 4.564348` (the maximum: the uniform counting measure is the maximum-entropy
  configuration of the counting measure).
* `H(witness tilt) = 4.291783` ⇒ `ΔS = 0.272565` ⇒ suppression `e^(−ΔS) = 0.761424` — a factor
  **1.31 only**.
* The **maximum** possible entropy cost is `ΔS = ln 96 = 4.564348` (the one-cell configuration,
  `H = 0`), i.e. a suppression of exactly `1/96 = 0.010417`.

The required suppression is `3.746 × 10⁵`. The entropy channel's ceiling is `1/96`, so it falls short
by **3.6 × 10⁷×**. **The suppression cannot be thermodynamic.** It must be statistical (the counting
law, §3) or dynamical (the absence of a drive, §5).

## 3. The accessibility window — AT's own mandatory fluctuation law

AT requires Poisson counting fluctuations, `δ = 1/√⟨N⟩` (QG15/QG228/QG231). The observed contrast
`1.6102e-6` then *measures* the event count per coherence cell:

```
⟨N⟩ = 1/δ² = 3.8569176e11
```

With `P(Δ) = exp(−⟨N⟩Δ²/2)`:

| contrast Δ | −ln P | probability | interpretation |
|------------|--------|-------------|----------------|
| 1.6102e-6 (observed) | 0.500 | 0.6065 | **typical** (at the median level) |
| 1.8959e-6 | 0.693 | 0.500 | the median |
| 3.4554e-6 | 2.303 | 0.100 | the 10 % cut |
| 4.8867e-6 | 4.605 | 0.010 | the 1 % cut → **the accessibility ceiling** |
| 8.1577e-6 | 12.834 | 2.7e-6 | **the G_003 requirement is reached here** (5.07× observed) |
| 0.032121 | 1.99e8 | 10^(−8.6e7) | survivor compression |
| 0.15151 | 4.43e9 | 10^(−1.9e9) | the G_003 canonical witness |
| 0.603175 | 7.02e10 | 10^(−3.0e10) | degeneracy redistribution |

The window is therefore **exhaustive and narrow**: everything up to ≈ 4.9 × 10⁻⁶ is reachable as a
fluctuation; the required suppression `3.746 × 10⁵` is reached at a contrast of only `8.1577e-6`
(5.07× the observed level); and the G_002/G_003 witnesses are 10⁴–10⁵× beyond it in amplitude and
10⁸–10¹⁰ beyond it in exponent.

## 4. Channel 3 — conservation constraints: what is FORBIDDEN

| Constraint | Value | Consequence |
|------------|-------|-------------|
| Count conservation (QG194) | `Σρ = 1` exact; deficit mass `Σ(1/N − ρ) = 0` exactly | mass-energy is fixed automatically, for *every* G_002 operation |
| Lattice structure (D_047) | `A₀ = 45`, lock release `0.80231` nats, exact and ε-independent | no ρ-operation can change `A₀` or the mirror pairing |
| Within-multiplet invariance | block sums under the witness tilt: `L1 = 0` exactly | the witness moves occupancy *inside* multiplets only |
| Cube lattice control | 884 736 modes, 20 812 eigenspaces, lock `3.948614`, `L = 0.97648` | the same conservation structure at 10⁴× scale |
| Planck floor | `ρ_max = 1/l_P³ = 2.3687 × 10¹⁰⁴ m⁻³` | a hard local ceiling from the shortest length |
| One-cell ceiling | `Δln ρ ≤ ln 96 = 4.5643` | the strongest *count-conserving* contrast |

**FORBIDDEN**: any configuration with `Σρ ≠ 1` or a changed total mass; a changed `A₀`/mirror
pairing without a symmetry-breaking agent; a cell above the Planck ceiling; a contrast above
`ln 96`. None of these is a `ρ`-operation, so no reachable state is lost by excluding them.

## 5. Channel 4 — dynamical accessibility

Three independent canonical probes:

**(a) The internal flow is ARRANGEMENT-NEUTRAL.** Branching continuity (`NativeMetricDynamics`)
is `ρ_(k+1) = μ·ρ_k` with the **same** μ for every cell, and the metric inherits it conformally,
`g_(k+1) = μ^(2/d)·g_k` (residual < 10⁻¹⁵). The density *scales*; occupancy never *moves*. A uniform
rescaling is a gauge transformation of the field — `a(λρ) = a(ρ)` exactly (G_001 scale invariance,
verified to < 10⁻⁶ at λ = 10⁶) — so the G_002 free directions have **no internal drive at all**.

**(b) The attractor ERASES initial arrangement data** (`InitialConditionsOrigin
.AttractorErasesInitialData`, basin fraction ≥ 0.9): an arrangement cannot be *maintained* either.

**(c) The gate.** The only route to a large-contrast mode is a dedicated **external** structured
drive. The canonical (imported, NP_171) lock gate sits at `g_c = 1.607` in units of ω_max with
`f(g = 1) = 0` — the drive must reach 1.61× the nominal maximum coupling and `K ≥ 10.29 ω₁`. AT's
native channels supply none of it.

**Accessibility chain:** `achieved = achievable × stability × realisation probability`, i.e.
`0.603175 × 0.0296 × e^(-4.4e9) ≈ 0` — the same conclusion as G_004's measured suppression, now
derived from the dynamics rather than read off the data.

## 6. Verdict — which ρ configurations are reachable?

| Class | Configuration | Reason |
|-------|---------------|--------|
| **ACCESSIBLE** | attractor / canonical uniform ρ, zero field | exact fixed point, basin 1 |
| **ACCESSIBLE** | Poisson-natural fluctuations `Δ ≤ 4.8867e-6`, incl. the observed galactic field `1.6102e-6` | P ≥ 1 %; the observed field is *typical* (P = 0.61) |
| **ACCESSIBLE** | the phase directions | carry no count and no contrast |
| **SUPPRESSED** | arrangement `0.685714` | −ln P = 9.07e10; contracting |
| **SUPPRESSED** | degeneracy redistribution `0.603175` | −ln P = 7.02e10; contracting |
| **SUPPRESSED** | D96 vs random `0.333333` | −ln P = 2.14e10; no internal drive |
| **SUPPRESSED** | D96³ vs D96 `0.276596` | −ln P = 1.47e10; no internal drive |
| **SUPPRESSED** | survivor compression `0.032121` | −ln P = 1.99e8; undriven |
| **FORBIDDEN** | `Σρ ≠ 1` / `dM ≠ 0` | count conservation (QG194) |
| **FORBIDDEN** | changed `A₀` / mirror pairing without a symmetry-breaking agent | structural invariants exact (D_047) |
| **FORBIDDEN** | cell above `2.3687e104 m⁻³` | Planck floor `1/l_P³` |

The bands are **disjoint and exhaustive** on the contrast axis: `ACCESSIBLE = (0, 4.8867e-6]`,
`SUPPRESSED = (4.8867e-6, 4.5643]`, `FORBIDDEN = (4.5643, ∞) ∪ {structural}`. All five G_003 witnesses
land in SUPPRESSED, and the observed field lands in ACCESSIBLE.

## 7. Classification and caveats

**No reclassification.** The D_040 `ClassificationRegistry` is untouched; G_001–G_004's verdicts
stand unchanged and no canonical claim, value, or equation is modified. G_005 introduces no new
primitive.

* The **Poisson channel** is AT's *own* mandatory fluctuation law, not an imported fit: it is the
  same relation (`δ = 1/√⟨N⟩`) used to derive the counting measure's dispersion elsewhere in the
  program. It is used here in the *forward* direction (from the observed contrast to ⟨N⟩), so the
  window is a DERIVED consequence of an INPUT observation.
* The **diffusion contraction** uses the canonical damping `d = 0.2`; the contraction is qualitative
  and holds for any `d ∈ (0, 0.5)` (the operator's spectral radius is < 1 for every non-uniform mode).
* The **lock gate** `g_c = 1.607` is NP_171's EMERGENT result in an imported (Adler/Kuramoto) model
  and is flagged IMPORTED. No G_005 verdict depends on its exact value.
* The **entropy ceiling** is exact (`ΔS ≤ ln 96` for a 96-cell counting measure) and is the reason
  the thermodynamic channel is *refuted* as the suppression mechanism.

## 8. Falsification paths

1. **A driven mode.** G_005 predicts that a *spontaneously* realised unit-amplitude mode is
   impossible, but a *deliberately driven* one is not. A laboratory experiment that drives a
   structured density contrast to `Δln ρ ≳ 10⁻⁴` at fixed total energy would falsify C1 (the
   suppression claim) without touching any conservation law.
2. **An improbable fluctuation.** The window is statistical: a single coherence cell realising
   `Δ > 8.1577e-6` (P < 2.7 × 10⁻⁶) at the observed event count would be evidence against the
   Poisson channel itself, i.e. against QG15/QG228/QG231.
3. **A symmetry-breaking agent.** If any process in the canonical chain can change `A₀` or the mirror
   pairing (D_047 says the release is exact and ε-independent), the FORBIDDEN class shrinks and the
   stabilisation argument weakens.
4. **A non-canonical damping.** If the scale-space diffusion is non-contracting for some mode
   (spectral radius ≥ 1), channel 1 fails for that mode.

## 9. Open problems

1. The residual **0.40 % `G` gap** (QG181/182) and the **7.7–13 % RAR offset** (G_004) remain
   unexplained; G_005 explains the *magnitude* of the suppression, not these residuals.
2. The coherence length `L` in `Δa = (c²/d)·Δln ρ/L` is still a declared BOUNDARY (G_003).
3. Whether *any* physical process realises the G_002 witness tilt remains open — G_005 shows it must
   be externally driven and cannot be spontaneous, which converts the question into a search for a
   structured drive rather than a search for a natural configuration.
4. The relationship between the diffusive contraction rate (a dynamics statement) and the Poisson
   counting window (a statistics statement) is not derived from a single principle; both originate in
   the counting measure, but the route from one to the other is not exhibited.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_005_Tests.cs` — **8/8 PASSED** (~0.3 s)
**Group total:** G_001–G_005 = **40/40 PASSED** (~0.3 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_005"`

| Verdict | Result |
|---------|--------|
| ACCESSIBLE | the attractor state, the phase directions, and every fluctuation `Δ ≤ 4.8867e-6` — **including the observed galactic field** `1.6102e-6` (P = 0.61) |
| SUPPRESSED | all five large G_003 modes (0.032121 … 0.685714): count-conserving, symmetry-preserving, dynamically available — but Poisson-suppressed by 10⁸–10¹⁰ nats, off-attractor (contracting by ~34× in 200 canonical steps), and not driven internally |
| FORBIDDEN | `Σρ ≠ 1`, `dM ≠ 0`, a changed `A₀`/mirror pairing without a symmetry-breaking agent, a cell above `1/l_P³`, or any contrast above ln 96 |

**Bottom line.** Nature exhibits exactly the Poisson-natural part of the counting measure. The large
G_003 modes are the tail of that distribution, and the tail is empty for a structural reason: at
`⟨N⟩ = 3.8569 × 10¹¹` a contrast of 0.15 has probability 10^(−1.9×10⁹). G_004's `CALIBRATED` verdict is
therefore explained *dynamically* rather than merely observed: the gravity source is `ρ` (G_001), its
controllable part is real but statistically inaccessible (G_002/G_005), and its observable magnitude
is set by the counting law.

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_001.md` … `G_004.md`
* `Docs/ResearchY/Tests/Results/Y_G_005_Result.md`
* `AT.Core/ResearchXH/RhoDynamics.cs`, `UniversalAttractor.cs`, `NativeMetricDynamics.cs`,
  `InitialConditionsOrigin.cs`, `CausalDiscretenessModel.cs`
* `Docs/Research/ATQG_GravityBridgeOrigin.md`, `G4RHO_DynamicalOrigin.md`, `G4O_RhoInterpretationAudit.md`
* ResearchY-NP_171 (the lock threshold — IMPORTED)
