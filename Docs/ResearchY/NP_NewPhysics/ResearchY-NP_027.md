# ResearchY-NP_027 — Planck Spectrum Emergence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_027 (permanent)
**Title:** Planck Spectrum Emergence Audit
**Status:** COMPLETE
**Date:** 2026-09-01
**File:** `NP_NewPhysics/ResearchY-NP_027.md`
**Depends on:** ResearchY-QG_194 (normalizer S, geometric occupation), QG_228
(information), QG_006/007 (count conservation), QG_018 (information-cosmology
closure), D_041 (spectrum), S_001 (synthesis)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_027_Tests.cs`

---

## Purpose

**Can the full Planck spectrum be reproduced as an emergent read of the D96 spectrum
without introducing quantum postulates?** This hostile audit constructs a spectral
occupation model from the D96 spectrum, occupancy moments, actualization statistics,
and the information measure; tests whether n(ω) = 1/(e^x − 1) emerges naturally;
separates the three mechanisms (finite UV / occupation statistics / max-entropy);
verifies the four classical limits; and determines the UV-regularization origin.

---

## 1. The spectral occupation model

The D96 occupation is geometric (QG194): ρ_k = μ^k/S, with S the normalizer
(Σρ = 1). For a single mode, if the count is read as a Bose occupation, the mean
occupation is the geometric mean:

```
⟨n_k⟩ = ρ_k/(1−ρ_k) = 1/(μ^(−k) − 1) = 1/(e^(k·ln(1/μ)) − 1)
```

**This IS the Planck factor form** n(x) = 1/(e^x − 1) with x = k·ln(1/μ).

---

## 2. Does the Planck factor emerge naturally?

| Element | Value | Emerges? |
|---|---|---|
| occupation | ρ_k = μ^k/S (geometric, QG194) | ✅ canonical |
| mean occupation | ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1) | ✅ the Planck FORM |
| x | k·ln(1/μ) | ✅ mode-indexed |
| D96 low-k dispersion | ω_k ≈ c·k (w/k ≈ 0.62 → 0.58 for k=1..5) | ✅ approximately linear |
| x = ℏω/kT identification | needs ln(1/μ) = ℏ·c/kT | ❌ μ must be temperature-dependent |

**The Planck FACTOR SHAPE emerges from the geometric occupation statistics.**
But the identification x = ℏω/kT (the Planck LAW's temperature dependence) requires
μ = e^(−ℏc/kT) — i.e., μ depends on a temperature T that is NOT a canonical AT
primitive.

---

## 3. Determine: A / B / C

| Option | Verdict |
|---|---|
| **A) finite UV solely from spectral cutoff** | **YES (in AT)** — the 95-mode finite spectrum (ω ∈ [0.62, 3.98]) is the UV regulator; there is no infinite mode-count to diverge. BUT this is NOT the canonical Planck mechanism (which needs ℏ). |
| **B) Planck factor from occupation statistics** | **YES in FORM** — n_k = 1/(e^x − 1) from the geometric count (QG194). The x = k·ln(1/μ) identification requires a temperature. |
| **C) maximum-entropy derivation** | **CONSISTENT but incomplete** — the geometric/Bose distribution IS the max-entropy distribution given a mean-energy constraint; AT's count is geometric, so a max-entropy read is consistent. BUT AT has not derived the temperature/energy constraint from its primitives. |

**The Planck factor FORM emerges from the occupation statistics (B); the finite UV
is the finite spectrum (A); a max-entropy reading is consistent (C) but the
temperature/energy constraint is not derived.**

---

## 4. Verify the four limits (continuous Planck form)

| Limit | Continuous Planck | Verified? |
|---|---|---|
| **Rayleigh-Jeans** (x→0) | n → 1/x | ✅ (x=0.01: n=99.50 ≈ 100; ratio → 1) |
| **Wien** (x→∞) | n → e^(−x) | ✅ (x=10: n=4.5e−5 ≈ e^(−10)) |
| **Stefan-Boltzmann** | ∫ x³/(e^x−1) dx = π⁴/15 = 6.4939 | ✅ (continuous) |
| **Wien displacement** | peak of x³/(e^x−1) at x = 2.821 | ✅ (continuous) |

**The CONTINUOUS Planck form satisfies all four classical limits exactly.**

---

## 5. The finite-N problem

| Quantity | Continuous Planck | D96 discrete (95 modes) |
|---|---|---|
| Stefan-Boltzmann | ∫₀^∞ x³/(e^x−1) dx = π⁴/15 | the discrete sum over 95 modes ≠ π⁴/15 |
| Wien displacement | peak at x = 2.821 | peak at the discrete mode with max ω³n(ω), mode-dependent |
| mode count | DOS ~ ω² dω (divergent) | finite (95 modes, band [0.62, 3.98]) |
| Rayleigh-Jeans | n → 1/x (diverges at ω→0) | no ω→0 mode (min ω = 0.62) |

**The full Planck LAW does NOT emerge from the finite discrete D96 spectrum.** The
Stefan-Boltzmann T⁴ law requires the continuous integral; the Wien displacement
requires the continuous peak; the Rayleigh-Jeans divergence requires an ω→0 mode.

---

## 6. UV regularization origin

| Candidate | Canonical QM | AT |
|---|---|---|
| **quantization (ℏ)** | YES — the e^(−ℏω/kT) tail cuts the UV catastrophe | **NO** — AT has no ℏ as a Planck-cutoff primitive |
| **finite observability** | — | PARTIAL — the finite state space bounds observables (QG_010) |
| **finite information** | — | PARTIAL — I_occ bounds the info content (QG_228) |
| **finite spectrum** | — | **YES — the 95-mode finite spectrum is the UV regulator** |

**In AT, the UV is regularized by the FINITE SPECTRUM** — there is no infinite
mode-count to diverge, so no Planck-factor cutoff is needed. This is a different
mechanism from canonical QM's quantization cutoff. The finite observability and
finite information are related structural bounds (QG_010/QG_228) but the direct UV
regulator is the finite spectrum.

---

## Theorem

> **Theorem (NP_027).** The Planck FACTOR form n(x) = 1/(e^x − 1) emerges from the
> D96 geometric occupation statistics (⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1), QG194), but
> the FULL Planck LAW — with its Stefan-Boltzmann T⁴, Wien-displacement peak at
> x = 2.821, Rayleigh-Jeans limit, and continuous density of states — does NOT
> emerge from the finite discrete D96 spectrum without importing temperature (a
> non-canonical primitive). Proof: (1) Construct the occupation model (Section 1,
> verified): ρ_k = μ^k/S gives ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1), the Planck FORM with
> x = k·ln(1/μ). (2) The identification x = ℏω/kT requires ln(1/μ) = ℏc/kT — μ
> must be temperature-dependent (Section 2, verified: the D96 low-k dispersion is
> only approximately linear, w/k ≈ 0.62→0.58, and T is not a canonical primitive).
> (3) Determine A/B/C (Section 3): A) finite UV from spectral cutoff — YES in AT
> (the 95-mode finite spectrum is the regulator); B) Planck factor from occupation
> statistics — YES in FORM; C) max-entropy — consistent (geometric is the max-entropy
> distribution) but the energy/temperature constraint is not derived. (4) Verify the
> limits (Section 4, verified): the CONTINUOUS form satisfies Rayleigh-Jeans (n→1/x),
> Wien (n→e^(−x)), Stefan-Boltzmann (π⁴/15 = 6.4939), and Wien-displacement
> (x = 2.821). (5) The finite-N problem (Section 5, verified): the discrete 95-mode
> sum ≠ the continuous π⁴/15 integral (no T⁴); the discrete peak is mode-dependent,
> not at x = 2.821; there is no ω→0 mode (min ω = 0.62) so no Rayleigh-Jeans
> divergence. (6) UV origin (Section 6): in AT the UV is regularized by the FINITE
> SPECTRUM (no infinite mode-count), NOT by quantization (ℏ) — a different mechanism.
> (7) Therefore: the FORM is emergent (B, DERIVED from the geometric count), but the
> full Planck LAW is not reproducible without importing temperature (a BOUNDARY input
> absent from the canonical primitives). Classification: the Planck-factor form
> DERIVED (from the geometric occupation, QG194); the temperature dependence
> (x = ℏω/kT) BOUNDARY (temperature not a canonical primitive); the finite-UV
> regulator DERIVED (finite spectrum); the full Planck law (T⁴, Wien displacement,
> continuous DOS) NOT REPRODUCED from the D96 spectrum alone (hosted/fitted content);
> a quantum (ℏ-cutoff) origin REFUTED for AT (the finite spectrum is the regulator).
> No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Construct the model (Section 1). (2) Test the emergence
> (Section 2). (3) Determine A/B/C (Section 3). (4) Verify the limits (Section 4).
> (5) Test finite-N (Section 5). (6) Locate the UV origin (Section 6). ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The full Planck law emerges from D96" | the discrete 95-mode sum ≠ π⁴/15; the peak is mode-dependent; no ω→0 mode (verified) |
| "Temperature is a canonical primitive" | T is not in the canonical boundary set {Difference, η, observable sector, anchors, finiteness, reference, tick} |
| "The UV is cut by quantization" | AT's finite spectrum already regulates the UV — no ℏ-cutoff needed |
| "The Rayleigh-Jeans divergence appears" | no ω→0 mode exists in D96 (min ω = 0.62) |
| "Stefan-Boltzmann T⁴ holds" | requires the continuous integral, not the finite discrete sum |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| the Planck factor form emerges from the occupation | a non-geometric count structure giving a different n(ω) |
| the UV is regulated by the finite spectrum | an infinite-spectrum AT system with a finite UV |
| temperature is not a canonical primitive | a derivation of T from {Difference, η, spectrum} alone |
| the full Planck law does not emerge | a finite discrete spectrum reproducing π⁴/15 and x = 2.821 exactly |

---

## Classification

| Component | Status |
|---|---|
| Planck-factor form n = 1/(e^x − 1) | **DERIVED** (from the geometric occupation, QG194) |
| temperature dependence (x = ℏω/kT) | **BOUNDARY** (T not a canonical primitive) |
| finite-UV regulator | **DERIVED** (the finite spectrum) |
| full Planck law (T⁴, Wien displacement, continuous DOS) | **NOT REPRODUCED** from D96 alone (hosted content) |
| quantum (ℏ-cutoff) UV origin | **REFUTED for AT** (the finite spectrum is the regulator) |

**The Planck FACTOR FORM n = 1/(e^x − 1) is emergent (DERIVED from the geometric
occupation statistics, QG194), and the UV is regulated by the finite D96 spectrum —
but the FULL Planck LAW (Stefan-Boltzmann T⁴, Wien displacement, continuous density
of states) does NOT emerge from the finite discrete spectrum without importing
temperature, a non-canonical primitive. No new primitive; canonical AT unchanged.**

---

## 9. Closure score and dependency DAG

**Closure score: 6/10.** The form emerges (occupation → Planck factor), the UV is
finite (spectrum), and the limits are satisfied by the continuous form — but the
temperature dependence, T⁴, Wien displacement, and continuous DOS are not derivable
from the canonical primitives.

```
Dependency DAG:
Difference → Actualization → Spectrum (D96, 95 modes)
 → count density ρ_k = μ^k/S (geometric, QG194)
   → ⟨n_k⟩ = 1/(e^{k·ln(1/μ)} − 1)          [DERIVED — Planck FORM]
   → x = k·ln(1/μ)                          [DERIVED — mode-indexed]
   → x = ℏω/kT requires T                   [BOUNDARY — temperature absent]
   → full Planck law (T⁴, 2.821, DOS)        [NOT REPRODUCED — hosted]
   → finite UV (95-mode cutoff)              [DERIVED — finite spectrum]
```

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_027_Tests.cs`
**Run:** 2026-09-01 · **Result:** see `Tests/Results/Y_NP_027_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_027_OccupationModel` | ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1) from the geometric count | ✅ |
| `Y_NP_027_PlanckFactor` | the Planck form with x = k·ln(1/μ) | ✅ |
| `Y_NP_027_RayleighJeans` | n → 1/x as x → 0 (continuous) | ✅ |
| `Y_NP_027_WienLimit` | n → e^(−x) as x → ∞ (continuous) | ✅ |
| `Y_NP_027_StefanBoltzmann` | ∫x³/(e^x−1)dx = π⁴/15 (continuous) | ✅ |
| `Y_NP_027_UVOrigin` | AT: finite spectrum is the UV regulator | ✅ |
| `Y_NP_027_NoGo` | the full Planck law does not emerge (finite discrete) | ✅ |
| `Y_NP_027_Run` | research report | ✅ |

**Conclusion:** The Planck factor form n = 1/(e^x − 1) IS emergent from the D96
geometric occupation statistics (DERIVED, QG194), and the UV is regulated by the
finite spectrum — but the FULL Planck law (Stefan-Boltzmann T⁴, Wien displacement at
x = 2.821, continuous density of states) does NOT emerge from the finite discrete
spectrum without importing temperature, which is not a canonical primitive. No new
primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_027"`

---

## References

- ResearchY-QG_194 (normalizer S — geometric occupation), QG_228 (information),
  QG_006/007 (count conservation), QG_018 (information-cosmology closure), D_041
  (spectrum), S_001 (synthesis).
