# ResearchY-G_014 — Physical Rho Mapping Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_014 (permanent)
**Title:** Physical Rho Mapping Audit — what measurable laboratory quantity corresponds to ρ?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_014.md`
**Depends on:** ResearchY-G_001 (the source law a = −(1/d)∇lnρ), G_003 (the ambient calibration and the spectral
weight), G_005 (the Poisson band and its ceiling), G_006/G_007 (the relaxation, its spectrum and its range),
G_009 (the clock law), G_011 (the actuator algebra and the phase-inertness), G_011b (the metric coupling is not
borrowed), G_012/G_013 (the actuator and its physical realization); AT-QG QG220 (ψ = √ρ e^{iθ}), QG15/QG228/QG231
(the Poisson counting law), QG180/QG181 (E = Σmλ)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_014_Tests.cs` (9/9 PASSED, ~0.1 s)

## Purpose

The group has the source (G_001), the clock law (G_009), an actuator (G_012) and a device (G_013). G_014 asks
the measurement question:

> **What measurable laboratory quantity corresponds to ρ?**
> Candidates: probability density, occupation density, mode population, energy density, information density,
> coherence density.
> Requirements: **(1) reproduces the G_001 source law, (2) reproduces the G_009 clock law, (3) supports the
> G_013 actuator, (4) survives G_007 suppression.**
> Measure: ρ correlation, field correlation, clock correlation.

**Answer.** The first experimentally measurable ρ analogue is the **diagonal occupation (probability)
density**: it is the *unique* candidate with κ = 1 — positive, normalised, cellwise and affine — so all four
requirements hold *identically*. The mode population and the energy density are **CORRELATED** (spectrally
blind and spectrally weighted respectively), and the information and coherence densities are **REFUTED** (a
global functional and the ψ-sector).

## 1. The mapping criterion

A lab observable `q = F(ρ)` is a ρ analogue iff

* **(A1)** `q_i > 0` cellwise — otherwise `ln q` (and with it the source and clock laws) does not exist;
* **(A2)** `Σq = 1` — it is a counting measure;
* **(A3)** `F` is **affine**, `κ = d ln q / d ln ρ` constant — because the relaxation `W` and the G_013 stencil
  `(I − W)` are **linear**, so a nonlinear map breaks the commutation;
* **(A4)** `q` is **cellwise** — otherwise it carries no gradient and there is no field.

The affine test is decisive. For `q = ρ^κ`:

| κ | flow error `‖F⁻¹WF(ρ) − Wρ‖` | actuator error `‖(I−W)F(ρ) − F′(ρ)(I−W)ρ‖` | recovered clock factor |
|---|---|---|---|
| 0.5 | 6.661085e-3 | **3.229213** | 2.0000 |
| **1.0** | **0.0** | **0.0** | **1.0000** |
| 2.0 | 1.047221e-2 | 4.668155e-2 | 0.5000 |
| 3.0 | 1.724505e-2 | 4.822371e-3 | 0.3333 |

**κ = 1 is the only exact mapping.** A non-affine observable needs a *calibration constant* (recoverable); a
non-cellwise one needs a *transform* (and loses the arrangement).

## 2. The candidate verdicts

| candidate | positive | normalised | affine | cellwise | **verdict** |
|---|---|---|---|---|---|
| **probability density** | yes | yes | **yes** (κ = 1) | yes | **PHYSICAL** |
| **occupation density** | yes | yes | **yes** (κ = 1) | yes | **PHYSICAL** |
| mode population | yes | yes | yes\* | **no** | **CORRELATED** |
| energy density | **no** (λ₀ = 0) | yes | no | yes | **CORRELATED** |
| information density | n/a | n/a | n/a | **no** | **REFUTED** |
| coherence density | yes | n/a | n/a | n/a | **REFUTED** |

\* diagonal in the modal basis, which is why it reproduces the suppression exactly.

## 3. PHYSICAL — the probability density and the occupation density

**Probability density.** `q = |ψ|²` with `ψ_j = √ρ_j e^{iθ_j}` (QG220) **is** ρ: `L1(|ψ|², ρ) = 2.484991379e-16`,
so κ = 1 *exactly* and the map is the identity. All four requirements hold identically — the source law
`a = −(1/d)∇ln q` (max|a| = 0.6031746, matching the direct read), the clock law `Δln q/d`, the G_013 stencil
`(I − W)q` (exactly equal to the ρ stencil) and the G_007 suppression `std(ρ)/std(W²⁰⁰ρ) = 33.7781483`. It is
also the only candidate that carries the **ψ-sector** (phase), so G_002/G_011's exact phase-inertness tests are
meaningful on it.

**Occupation density.** The counting face, also κ = 1, and its **own shot noise is the theory's Poisson law**:

| quantity | value |
|---|---|
| counts per cell fixed by the observed galactic contrast | `⟨N⟩ = 1.6102e-6⁻² = 3.856917553651e11` |
| counting noise | `δ = 1/√⟨N⟩ = 1.610200e-6` — **exactly** the observed contrast (G_003) |
| 1 % ceiling | `4.886722e-6` — **exactly** G_005's accessible band |
| probability of the observed field | `exp(−⟨N⟩Δ²/2) = 0.6065` (G_005's "typical" P = 0.61) |

No other candidate's noise *is* the theory's band: for the occupation density the G_005 accessible/suppressed
classification is a property of the *measurement*, not an extra assumption.

## 4. CORRELATED — the mode population and the energy density

**Mode population (spectrally blind).** The DCT of the reversal multiplies mode k by `(−1)^k`, so the **power
spectrum is invariant**: `max |Δ|w_k|| = 4.1598669e-15` and the total spectral energy ratio is
`1.0000000000000018`. Yet the two arrangements are physically opposite: `max|Δa| = 0.8864864874` with an
acceleration correlation of only ≈ 0.10. It reproduces the G_007 suppression exactly (the flow is diagonal in
the same basis) and gives G_013's *modal-gain* view — hence CORRELATED: an exact coordinate that cannot see the
arrangement.

**Energy density (spectrally weighted).** The D96 weight has a (numerically) **zero mode**, so `ε = λ·ρ`
vanishes at that cell and `ln ε` is undefined — the source and clock laws fail outright there. With a positive
weight (`1 + λ/λ_max ∈ [1,2]`) the flow commutation defect is **3.7873408e-4** against a reference
**2.0916667e-2**, i.e. **1.81 %**, and the implied clock factor spreads over **[0.5689248, 1.1378497]**
(a twofold spread in the recovered Δlnρ). It is also non-injective (the fixed-energy fibre is 94-dimensional).
**G_001's verdict stands**: an exact re-expression *once the spectral weight is known*.

## 5. REFUTED — the information and coherence densities

* **Information density** is a **global, permutation-invariant** functional: `ΔKL = 0` exactly under a
  37-cell roll while the density moves by `L1 = 0.6583333` and the field by `1.0031746`. It is zero at the
  uniform measure, where the field is also zero, so it cannot be normalised into a counting measure carrying a
  gradient. (Consistent with G_001 and G_011.)
* **Coherence density** is the **ψ-sector**: a change of phase moves the coherent sum by a factor
  **281.2241449** (`0.0324196281 → 9.1171821879`) at `L1(ρ,|ψ|²) = 2.5e-16` and `|Δa| < 1e-9`. It is a genuine
  physical variable that is *exactly ρ-inert* (G_002/G_011).

## 6. The critical answer — the first measurable ρ analogue

> **The diagonal occupation (probability) density `q_i`.**

It is measurable today by site-resolved imaging, photon counting or mode-resolved population measurement, and
it satisfies A1–A4 with κ = 1 exactly. The measurement recipe is fully specified by the group's laws:

1. count or image the density cellwise → `q_i = n_i / Σn`;
2. form `ln q` and read the field as `a = −(1/d)∇ln q` (G_001);
3. read the clock ratio as `Δτ/τ = Δln q/d` (G_009);
4. hold patterns with the three-point stencil `(I − W)q` (G_013), realizable by a negative conductance or a
   feedback controller.

The readable contrast floor is the counting noise `1/√⟨N⟩ = 1.6102e-6`, which by the clock law is
**46.374 ms/day** of depth — exactly G_009's galactic cross-check. The residual gap is the **identification
premise** (the lab's `q` must *be* the actualization density), which is precisely the metric coupling G_011b
showed is not borrowed. Everything else is structure — and that structure is what makes the four laws testable.

## 7. Verdicts

| label | content |
|-------|---------|
| **PHYSICAL** | **probability density** (`q = |ψ|²` *is* ρ to 2.5e-16: κ = 1 exactly, all four requirements identical, carries the phase sector) and **occupation density** (the counting face: κ = 1 and its shot noise *is* the G_005 Poisson band, ⟨N⟩ = 3.8569e11, δ = 1.6102e-6, ceiling 4.8867e-6, P = 0.61). |
| **CORRELATED** | **mode population** (reversal-invariant power spectrum to 4.16e-15 while the field flips by 0.8864865 with 0.10 correlation; reproduces the suppression and the modal-gain view) and **energy density** (a zero mode makes ε = 0 and ln ε undefined; 1.81 % commutation defect and a twofold clock-factor spread with a positive weight; non-injective). |
| **REFUTED** | **information density** (global, permutation-invariant: ΔKL = 0 while the field moves 1.0031746) and **coherence density** (the ψ-sector: 281.22× change at Δρ = 0). |

## 8. Classification and caveats

**No reclassification.** G_001's labels (energy and spectral density CORRELATED; information density REFUTED)
are unchanged and restated here for the same reasons. D_040 untouched; no canonical claim, value or equation
changes; no new primitive. Deterministic: exact algebra, no randomness.

* The affine criterion (A3) is a *structure* statement: a κ ≠ 1 observable still reproduces the laws up to a
  recoverable calibration constant `1/κ`; what it cannot do is inherit the linear actuator and the linear flow
  without a nonlinear device.
* The mode-population verdict is not a criticism of spectroscopy: it is the precise statement that the *power
  spectrum* is the wrong invariant (it is reversal-blind), while the *complex* amplitudes (G_011's
  coordinates) are faithful.
* The PHYSICAL verdicts are about *structure*: they do not reopen G_011b's finding that a laboratory pattern
  produces no metric effect. G_014 identifies the observable that *carries* ρ; G_011b showed that carrying it
  is not the same as sourcing it.

## 9. Open problems (OP1–OP4)

1. Which concrete platform gives the best κ = 1 density read — site-resolved atoms, photon counting, or
   mode-resolved population — and what is the achievable `⟨N⟩` (hence the contrast floor `1/√⟨N⟩`)?
2. Does a *complex-amplitude* measurement (`ψ` itself, not `|ψ|²`) let a laboratory see the phase-inert
   channel, i.e. verify the 281× coherent-sum change at Δρ = 0?
3. Can the energy-density channel be *calibrated* to κ = 1 by measuring the spectral weight per cell?
4. Is there a measurable observable with κ = 1 that is *also* immune to the identification premise — i.e. one
   whose coupling to the metric is derived rather than assumed?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_014_Tests.cs` — **9/9 PASSED** (~0.1 s)
**Group total:** G_001–G_014 = **126/126 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_014"`

| label | content |
|-------|---------|
| **PHYSICAL** | the diagonal occupation (probability) density — the first measurable ρ analogue (κ = 1 exactly) |
| **CORRELATED** | mode population (reversal-blind) and energy density (spectrally weighted, zero mode) |
| **REFUTED** | information density (global functional) and coherence density (the ρ-inert ψ-sector) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_001.md`, `ResearchY-G_003.md`, `ResearchY-G_005.md`,
  `ResearchY-G_009.md`, `ResearchY-G_011.md`, `ResearchY-G_011b.md`, `ResearchY-G_013.md`
* `Docs/ResearchY/Tests/Results/Y_G_014_Result.md`
* `AT.Tests/Shared/RhoActuators.cs`, `DensityField.cs`; `AT.Core/ResearchXH/RhoDynamics.cs`
