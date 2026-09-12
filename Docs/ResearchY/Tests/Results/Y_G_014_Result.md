# Y_G_014_Result.md — ResearchY-G_014 Physical Rho Mapping Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_014_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 9/9 PASSED (~0.1 s) — group G total 126/126 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_014"`

## Summary

**Question:** what measurable laboratory quantity corresponds to ρ? (probability density, occupation density,
mode population, energy density, information density, coherence density; requirements: G_001 source law,
G_009 clock law, G_013 actuator, G_007 suppression)
**Answer:** the **diagonal occupation (probability) density** is the first measurable ρ analogue — the unique
candidate with κ = 1 (positive, normalised, affine, cellwise), so all four requirements hold identically. The
mode population and energy density are CORRELATED; the information and coherence densities are REFUTED.

## Detail — the mapping criterion

| κ | flow error `‖F⁻¹WF(ρ) − Wρ‖` | actuator error | clock factor |
|---|---|---|---|
| 0.5 | 6.661085e-3 | 3.229213 | 2.0000 |
| **1.0** | **0.0** | **0.0** | **1.0000** |
| 2.0 | 1.047221e-2 | 4.668155e-2 | 0.5000 |
| 3.0 | 1.724505e-2 | 4.822371e-3 | 0.3333 |

κ = 1 is the only exact map (the relaxation and the G_013 stencil are linear).

| candidate | verdict | evidence |
|---|---|---|
| **probability density** | **PHYSICAL** | `q = \|ψ\|²` **is** ρ: `L1 = 2.484991379e-16` (κ = 1 exactly); field max\|a\| = 0.6031746 exact; suppression 33.7781483; carries the phase sector |
| **occupation density** | **PHYSICAL** | κ = 1; its shot noise **IS** the band: `⟨N⟩ = 3.856917553651e11`, `δ = 1.610200e-6` = the observed contrast, 1 % ceiling `4.886722e-6` = G_005's band, `P = 0.6065` |
| mode population | **CORRELATED** | power spectrum is **reversal-invariant** (`max\|Δ\|w_k\|\| = 4.1598669e-15`, energy ratio 1.0000000000000018) while `max\|Δa\| = 0.8864864874` (correlation ≈ 0.10); reproduces the suppression and the modal gain |
| energy density | **CORRELATED** | the D96 weight has a **zero mode** (ε = 0, ln undefined); with a positive weight the commutation defect is **3.7873408e-4 vs 2.0916667e-2 (1.81 %)** and the clock factor spreads `[0.5689248, 1.1378497]`; non-injective (fibre 94) |
| information density | **REFUTED** | global, permutation-invariant: `ΔKL = 0` while `L1 = 0.6583333` and the field moves 1.0031746; zero at the uniform measure |
| coherence density | **REFUTED** | the ψ-sector: coherent sum `0.0324196281 → 9.1171821879` (**281.2241449×**) at `L1(ρ,\|ψ\|²) = 2.5e-16`, `\|Δa\| < 1e-9` |

## The critical answer

> **The diagonal occupation (probability) density `q_i`** — measurable by site-resolved imaging, photon
> counting or mode-resolved population.

Recipe: normalise the counts → `ln q` → field `a = −(1/d)∇ln q` (G_001) → clock `Δτ/τ = Δln q/d` (G_009) →
hold with `(I − W)q` (G_013). The readable contrast floor is `1/√⟨N⟩ = 1.6102e-6` = **46.374 ms/day** of clock
depth (G_009's galactic cross-check). The residual gap is the **identification premise**, exactly the metric
coupling G_011b showed is not borrowed.

## Classification and caveats

**No reclassification.** G_001's labels (energy/spectral CORRELATED; information REFUTED) are unchanged.
D_040 untouched; no canonical claim, value or equation changes; no new primitive. Deterministic, no randomness.

* A κ ≠ 1 observable reproduces the laws up to a recoverable calibration constant `1/κ`; what it cannot do is
  inherit the *linear* actuator and flow without a nonlinear device.
* The mode-population verdict means the *power spectrum* is the wrong invariant (reversal-blind), while the
  complex amplitudes are faithful.
* The PHYSICAL verdicts are about *structure*; they do not reopen G_011b's finding that a lab pattern produces
  no metric effect — carrying ρ is not the same as sourcing it.

## Open problems (OP1–OP4)

1. Which platform gives the best κ = 1 read, and what achievable `⟨N⟩` (hence contrast floor)?
2. Can a *complex-amplitude* measurement verify the 281× coherent-sum change at Δρ = 0?
3. Can the energy-density channel be calibrated to κ = 1 by measuring the per-cell spectral weight?
4. Is there a κ = 1 observable immune to the identification premise (a derived metric coupling)?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_014.md`
* `AT.Tests/Shared/RhoActuators.cs`, `AT.Core/ResearchXH/RhoDynamics.cs`
