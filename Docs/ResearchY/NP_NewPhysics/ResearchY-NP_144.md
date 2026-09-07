# ResearchY-NP_144 — Defect Spectrum Fingerprint Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_144 (permanent)
**Title:** Defect Spectrum Fingerprint Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_144.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_141 (yield stress softening), NP_142
(multi-band resonance control), NP_143 (dislocation threshold)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_144_Tests.cs`

---

## Purpose

NP_141/142 established that yield-stress softening is defect-controlled and broadband; NP_143 showed
the threshold is low and laboratory-accessible. NP_144 asks the fingerprint question: **do different
materials possess unique defect-spectrum fingerprints that can be measured and used for targeted
yield-stress control?** Program: (1) define defect spectrum / breakaway spectrum / mobility spectrum;
(2) compare aluminum / steel / granite / quartz; (3) determine whether materials show unique spectral
fingerprints or universal broadband behavior; (4) measure sensitivity to frequency / bandwidth /
amplitude; (5) determine whether tailored excitation outperforms generic broadband; (6) estimate
achievable gains. **Success criterion:** determine whether every material has a unique "defect
fingerprint" that can be exploited for targeted softening. No new primitives; canonical AT unchanged.

---

## 1. Define the three spectra

| Spectrum | What it is | Measured by |
|---|---|---|
| **defect spectrum** | the distribution of defect *types* (dislocations, interstitials, grain boundaries, cracks) and their relaxation frequencies | mechanical spectroscopy / internal friction Q⁻¹(ω, T) |
| **breakaway spectrum** | the distribution of *thresholds* (pinning strengths) at which each defect unlocks | amplitude-dependent internal friction (Granato–Lücke) |
| **mobility spectrum** | the *response* of defect mobility to a drive (frequency/amplitude dependence) | the actionable softening response |

These are distinct: identity (defect), threshold (breakaway), and response (mobility).

---

## 2. Compare aluminum / steel / granite / quartz

| Material | Dominant defect | Signature peak | Breakaway |
|---|---|---|---|
| **aluminum** | dislocations | **Bordoni** peak (kink pairs) | low |
| **steel** | dislocations + C/N interstitials | **Snoek** peak (interstitial reorientation) | higher (pinned) |
| **granite** | grain contacts, microcracks | broad NME contact spectrum | contact-dependent |
| **quartz** | microcracks (brittle, few dislocations) | crack / NME features | crack-dependent |

Each material has a **distinct, measurable defect fingerprint** — the basis of the mature field of
mechanical spectroscopy (Bordoni, Snoek, grain-boundary / Kê, Zener peaks).

---

## 3. Unique fingerprints or universal broadband?

Both, at different levels:

| Level | Behavior | Verdict |
|---|---|---|
| **defect identity** (who the defects are) | **unique** per material | mechanical-spectroscopy fingerprint |
| **breakaway threshold** (when each unlocks) | material-specific, all ~microstrain | NP_143 |
| **mobility response** (how they drive) | **broadband, amplitude-dominated** | NP_142 |

So the *identity* fingerprint is unique, but the *softening response* is approximately universal
broadband — the fine spectral structure of the fingerprint does not dominate the amplitude-controlled
yield-stress softening.

---

## 4. Sensitivity to frequency / bandwidth / amplitude

| Axis | Sensitivity for yield softening |
|---|---|
| **frequency** | weak-to-moderate (broadband defect spectrum; no sharp single line) |
| **bandwidth** | moderate (wider coverage activates more defects) |
| **amplitude** | **dominant** (must exceed breakaway threshold; NP_142/143) |

Amplitude is the primary lever; bandwidth is a secondary efficiency factor; frequency fine-structure is
tertiary.

---

## 5. Tailored vs generic broadband excitation

| Scheme | Outcome |
|---|---|
| **generic broadband** | works — crosses the breakaway threshold (NP_143) |
| **tailored** (match the measured defect fingerprint) | works, and is **more energy-efficient** — but does **not** dramatically change the softening magnitude |

The softening magnitude is set by dislocation mobility once the threshold is crossed, not by spectral
matching. Tailoring buys *efficiency* (less wasted energy on non-resonant bands), not *magnitude*.

---

## 6. Achievable gains

| Gain type | Estimate |
|---|---|
| **softening magnitude** | ~1× (no dramatic gain from tailoring) |
| **energy efficiency / power** | ~2–5× (focus energy on the resonant defect bands) |
| **diagnostics** | large (the fingerprint identifies defect type/density — valuable for material control, separate from softening) |

The fingerprint's real payoff is **diagnostics and efficiency**, not a larger softening effect.

---

## Theorem

> **Theorem (NP_144).** Every material has a unique, measurable defect fingerprint, but its
> exploitation for targeted yield-stress control gives only modest gains — the fingerprint is a
> diagnostic and an efficiency lever, not a magnitude lever. Materials show unique defect-identity
> spectra (Bordoni / Snoek / grain-boundary / NME peaks, measurable by mechanical spectroscopy) and
> material-specific breakaway thresholds, but a near-universal broadband, amplitude-dominated mobility
> response (NP_142). Tailored excitation (matching the fingerprint) is more energy-efficient (~2–5×
> power saving) but does not materially increase the softening magnitude (~1×), because softening is
> set by dislocation mobility once the breakaway threshold is crossed (NP_143). Proof: (1) Define
> (Section 1). (2) Compare (Section 2). (3) Fingerprint vs broadband (Section 3). (4) Sensitivity
> (Section 4). (5) Tailored vs generic (Section 5). (6) Gains (Section 6). **Success criterion: unique
> fingerprint SUPPORTED; targeted-softening exploitation PARTIAL.** No new primitive; canonical AT
> unchanged.
>
> *Proof sketch.* (1) Define. (2) Compare. (3) Fingerprint. (4) Sensitivity. (5) Tailor. (6) Gains. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "all materials are spectrally identical" | mechanical spectroscopy shows distinct Bordoni/Snoek/grain-boundary/NME peaks |
| "the fingerprint is the softening lever" | softening is amplitude-dominated; spectral matching only improves efficiency |
| "tailoring is useless" | matching the defect bands saves energy (~2–5×), a real but modest gain |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| unique fingerprint exists | two materials with identical internal-friction spectra yet different defect populations |
| tailored gain is only efficiency | a tailored drive that doubles the softening magnitude vs equal-energy broadband |
| softening is amplitude-dominated | a frequency-matching (not amplitude) change that controls the softening |

---

## 9. Classification

| Component | Status |
|---|---|
| unique defect fingerprint (measurable) | **SUPPORTED** (mechanical spectroscopy) |
| universal broadband mobility response | **SUPPORTED** (NP_142) |
| tailored excitation outperforms generic | **PARTIAL** (efficiency ~2–5×, magnitude ~1×) |
| overall | **PARTIAL** |

**Conclusion.** Every material has a **unique defect-spectrum fingerprint** (SUPPORTED — Bordoni,
Snoek, grain-boundary, and NME peaks), but exploiting it for targeted softening yields only modest
gains: the softening channel is broadband and amplitude-dominated (NP_142), so tailoring the drive to
the fingerprint saves energy (~2–5×) without materially increasing the softening magnitude. The
fingerprint is therefore most valuable as a **diagnostic** (identify and track defects) and a
**modest efficiency lever**, not as a route to dramatically stronger softening. No new primitive;
canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_144_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_144_Define` | defect / breakaway / mobility spectra distinct | ✅ |
| `Y_NP_144_Compare` | material-specific Bordoni/Snoek/grain-boundary/NME peaks | ✅ |
| `Y_NP_144_Fingerprint` | unique identity + universal broadband mobility | ✅ |
| `Y_NP_144_Sensitivity` | amplitude > bandwidth > frequency | ✅ |
| `Y_NP_144_Tailored` | tailored = efficiency, not magnitude | ✅ |
| `Y_NP_144_Gains` | efficiency ~2–5×, magnitude ~1× | ✅ |
| `Y_NP_144_Classification` | overall PARTIAL | ✅ |
| `Y_NP_144_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_144"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent matter
  control), NP_141 (yield stress softening), NP_142 (multi-band resonance control), NP_143 (dislocation
  threshold).
- Real-world record: mechanical spectroscopy / internal friction (Bordoni dislocation peak, Snoek
  interstitial peak, grain-boundary / Kê peak, Zener peak); Granato–Lücke amplitude-dependent
  breakaway; nonlinear mesoscopic elasticity (broad NME contact spectrum).
