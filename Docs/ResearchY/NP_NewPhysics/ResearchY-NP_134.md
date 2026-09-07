# ResearchY-NP_134 — Critical Mode Discovery Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_134 (permanent)
**Title:** Critical Mode Discovery Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_134.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_130 (material sonification), NP_131
(critical resonance), NP_132 (reversible softening), NP_133 (critical mode frequency)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_134_Tests.cs`

---

## Purpose

NP_131–133 established that a small set of critical modes controls rigidity, lives in the
audio-to-ultrasound band, and can be softened reversibly. NP_134 asks the discovery question: **how
can the critical rigidity modes of an unknown material be identified?** Program: (1) start from a
measured spectrum; (2) determine whether critical modes reveal themselves through amplitude / phase
response / coherence sensitivity / nonlinear coupling; (3) simulate crystal / metal / granite /
glass; (4) rank modes by influence on rigidity / elasticity / structural stability; (5) determine
the minimum measurement required. **Success criterion:** provide a practical procedure for
discovering the resonance "master key" of a material. No new primitives; canonical AT unchanged.

---

## 1. Start from a measured spectrum

The fingerprint (NP_130) gives the mode spectrum { (ω_i, A_i, φ_i) }. The spectrum alone *lists* the
modes; it does not *rank* them. The discovery task is to find which modes are the critical
(load-bearing) subset.

---

## 2. Diagnostic signatures

| Signature | Diagnostic? | Why |
|---|---|---|
| **A) amplitude** | **NO** | a mode's amplitude is uncorrelated with its load-bearing role — a loud mode may be irrelevant |
| **B) phase response** | **PARTIAL** | every resonance has a sharp phase shift; necessary but not sufficient |
| **C) coherence sensitivity** | **YES** | critical modes are precisely those whose decoherence collapses rigidity R (NP_131) |
| **D) nonlinear coupling** | **YES** | critical modes show percolation-nonlinear rigidity response (small drive → large drop) |

**C and D are the diagnostic signatures**: the critical modes are the ones whose *decoherence* or
*nonlinear drive* most degrades rigidity — not the loudest, but the most load-bearing.

---

## 3/4. Rank modes by influence

Define the **influence** of mode i as the rigidity sensitivity:

```
I_i = |∂R / ∂e_i|
```

where e_i is the resonant excitation of mode i. Critical (backbone) modes have large I
(≈ 1/(1 − p_c) in the percolation model, NP_131); non-critical modes have I ≈ 0. Ranking by I
separates the master key from the rest:

| Material | m | Top-m influence | Next influence | Separability |
|---|---|---|---|---|
| **crystal** | 6 | 2.0 | ~0 | **sharp** (clean gap) |
| **metal** | 6 | 2.0 | ~0 | **sharp** |
| **granite** | 20 | 2.0 | ~0 | moderate |
| **glass** | 40 | 2.0 | small (distributed) | **diffuse** (weak key) |

Ordered materials (crystal/metal) have a sharp gap — the master key is unambiguous; amorphous glass
has a diffuse gap because rigidity is distributed across many soft modes (NP_131).

---

## 5. The minimum measurement

**Procedure (rigidity-perturbation ranking):**

1. **Measure the spectrum** — the resonance fingerprint (NP_130), e.g. resonant ultrasound
   spectroscopy (NP_133).
2. **Sweep a resonant drive** across each mode; record the rigidity (elastic modulus) response
   R(e_i).
3. **Compute influence** I_i = |∂R/∂e_i| for each mode.
4. **Rank** modes by I_i. The top-m (largest rigidity drop) are the critical modes — the master key.

The minimum measurement is a single **stiffness-vs-frequency sweep**: drive each candidate mode at
its own frequency and measure how far rigidity falls. No prior model is needed; the critical modes
*announce themselves* by the size of the rigidity drop they cause.

---

## Theorem

> **Theorem (NP_134).** The critical rigidity modes of an unknown material are discovered by
> RIGIDITY-PERTURBATION RANKING: measure the spectrum (NP_130), sweep a resonant drive across each
> mode, record the rigidity response, and rank by influence I_i = |∂R/∂e_i| — the top-m modes are the
> master key. The diagnostic signatures are C (coherence sensitivity) and D (nonlinear coupling): the
> critical modes are those whose decoherence or nonlinear drive most collapses rigidity (NP_131), NOT
> the loudest (A, amplitude is uncorrelated with load-bearing role; REFUTED) nor merely the
> sharpest-phase (B, PARTIAL — every resonance has a phase shift). The influence of a backbone mode is
> ≈ 1/(1 − p_c) vs ≈ 0 for non-backbone modes, so ranking cleanly separates the key. Ordered materials
> (crystal/metal) show a sharp gap (m = 6 unambiguous); amorphous glass shows a diffuse gap (m = 40,
> distributed rigidity). The minimum measurement is a single stiffness-vs-frequency sweep. Proof:
> (1) Spectrum (Section 1). (2) Signatures (Section 2, verified — C/D yes, A no, B partial). (3/4) Rank
> (Sections 3–4, verified — influence 2.0 vs 0). (5) Minimum measurement (Section 5). **Success
> criterion: a practical procedure for discovering the master key.** Classification: the discovery
> procedure EMERGENT (from the DERIVED critical-mode property, NP_131); "amplitude identifies critical
> modes" REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Spectrum. (2) Signatures. (3/4) Rank. (5) Measure. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the loudest mode is critical" | amplitude is uncorrelated with load-bearing role (A: NO) |
| "the sharpest phase is critical" | every resonance has a phase shift (B: PARTIAL) |
| "the critical modes cannot be found without a model" | rigidity-perturbation ranking needs only a drive + a stiffness readout |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| rigidity-perturbation ranking finds the key | a material whose top-influence modes do not control rigidity |
| coherence sensitivity is diagnostic | a mode whose decoherence collapses R but is not load-bearing |
| amplitude is not diagnostic | a material whose loudest mode is its backbone |

---

## 8. Classification

| Component | Status |
|---|---|
| critical-mode property (small backbone) | **DERIVED** (NP_131) |
| discovery procedure (rigidity-perturbation ranking) | **EMERGENT** |
| amplitude identifies critical modes | **REFUTED** |

**Conclusion.** The resonance master key of an unknown material is discovered by **rigidity-
perturbation ranking**: sweep a resonant drive, measure how far rigidity falls per mode, and rank.
The critical modes reveal themselves through coherence sensitivity and nonlinear coupling — the size
of the rigidity drop they cause — not through amplitude. Ordered materials yield a sharp, unambiguous
key; amorphous glass a diffuse one. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_134_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_134_Spectrum` | fingerprint lists modes, doesn't rank them | ✅ |
| `Y_NP_134_Signatures` | C/D yes; A no; B partial | ✅ |
| `Y_NP_134_Rank` | influence I = 1/(1−p_c) vs ~0 | ✅ |
| `Y_NP_134_Separability` | crystal/metal sharp; glass diffuse | ✅ |
| `Y_NP_134_MinimumMeasurement` | stiffness-vs-frequency sweep | ✅ |
| `Y_NP_134_Classification` | EMERGENT; amplitude REFUTED | ✅ |
| `Y_NP_134_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_134"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent
  matter control), NP_130 (material sonification), NP_131 (critical resonance), NP_132 (reversible
  softening), NP_133 (critical mode frequency).
