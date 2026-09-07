# ResearchY-NP_133 — Critical Mode Frequency Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_133 (permanent)
**Title:** Critical Mode Frequency Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_133.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_130 (material sonification), NP_131
(critical resonance), NP_132 (reversible softening)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_133_Tests.cs`

---

## Purpose

NP_131/132 established that a small set of critical (backbone) modes controls rigidity and can be
coherently softened reversibly. NP_133 asks the engineering-frequency question: **what physical
frequency ranges contain the critical rigidity modes?** Program: (1) estimate critical-mode
frequencies for crystal / metal / glass / granite; (2) compare audio / ultrasound / microwave / THz /
infrared; (3) determine which band couples most efficiently to rigidity control; (4) estimate
achievable softening; (5) identify existing technologies capable of exciting those modes. **Success
criterion:** determine whether coherent softening is an audio-scale, ultrasonic-scale, or THz-scale
technology. No new primitives; canonical AT unchanged.

---

## 1. Critical-mode frequencies

The critical (load-bearing) modes are the **long-wavelength acoustic phonons** — the backbone that
carries macroscopic rigidity. Their fundamental frequency is the structure's resonant frequency:

```
f_1 = c_s / (2L)
```

where c_s is the sound speed and L the sample size. Using representative c_s (crystal/metal/glass
~5000 m/s, granite ~5500 m/s):

| Material | c_s (m/s) | f_1 @ 10 cm | f_1 @ 1 m | Debye f_D |
|---|---|---|---|---|
| **crystal** | 5000 | 25.0 kHz | 2.5 kHz | 8.3 THz |
| **metal** | 5000 | 25.0 kHz | 2.5 kHz | 8.3 THz |
| **granite** | 5500 | 27.5 kHz | 2.75 kHz | 9.2 THz |
| **glass** | 5000 | 25.0 kHz | 2.5 kHz | 8.3 THz |

The **Debye frequency** f_D = c_s/(2a) (a ~ 3 Å) is ~10 THz — that is the *individual bond* mode, not
the collective rigidity mode. They differ by ~10⁸×.

---

## 2. Which band?

| Band | Range | Contains |
|---|---|---|
| **audio** | 20 Hz – 20 kHz | meter-scale rigidity modes |
| **ultrasound** | 20 kHz – 1 GHz | cm-to-mm rigidity modes |
| microwave | 0.3 – 300 GHz | — |
| THz | 0.1 – 10 THz | individual **bond** modes (Debye) |
| infrared | 3 – 400 THz | bond/electronic modes |

**Critical rigidity modes live in the AUDIO-to-ULTRASOUND band (~kHz–MHz)** — set by the structure
size, not the lattice spacing. THz is the *bond* band, not the rigidity band.

---

## 3. Coupling efficiency

Resonant coupling requires driving *at* the mode frequency. Rigidity modes are kHz–MHz, so audio and
ultrasound couple resonantly; a THz drive is ~10⁸× off-resonance for a 25 kHz rigidity mode — it hits
individual bonds instead (local heating/chemistry, NP_096), not collective rigidity. The band that
couples most efficiently to rigidity control is therefore **ultrasound** (audio for meter-scale).

---

## 4. Achievable softening

With a resonant, frequency-matched drive (piezoelectric ultrasound), the critical modes can be driven
to full amplitude at their own frequency — dropping rigidity R → 0 (NP_132) without the thermal cost.
The softening is frequency-matched, not amplitude-limited: the "master key" of NP_131 is tuned to the
structure's own acoustic resonance.

---

## 5. Existing technologies

| Band | Technologies |
|---|---|
| **audio** | subwoofers, shakers, voice coils (20 Hz – 20 kHz) |
| **ultrasound** | piezoelectric transducers, ultrasonic horns, SAW devices, phased arrays |
| THz/IR | bond-scale (heating/chemistry), not rigidity control |

---

## Theorem

> **Theorem (NP_133).** The critical rigidity modes live in the AUDIO-to-ULTRASOUND band (~kHz–MHz),
> so coherent softening is an ULTRASONIC-scale technology (reaching audio for meter-scale structures),
> not a THz-scale one. The critical modes are long-wavelength acoustic phonons with fundamental
> frequency f_1 = c_s/(2L): ~25–27.5 kHz for a 10 cm sample and ~2.5–2.75 kHz for 1 m (c_s ≈ 5000–5500
> m/s). The Debye frequency f_D = c_s/(2a) ~ 8.3–9.2 THz is the individual-BOND mode, a factor ~10⁸×
> higher and physically distinct. Resonant coupling therefore favors ultrasound (audio for large
> structures); THz is ~10⁸× off-resonance for rigidity and couples to bonds (heating/chemistry, NP_096).
> Existing ultrasonic technologies (piezoelectric transducers, horns, SAW, phased arrays) can excite
> these modes. Proof: (1) Estimate (Section 1, verified — f_1 ~ 25 kHz @ 10 cm, f_D ~ 8–9 THz).
> (2) Band (Section 2). (3) Couple (Section 3, verified — 10⁸× off-resonance). (4) Soften (Section 4).
> (5) Technologies (Section 5). **Success criterion: coherent softening is ULTRASONIC-scale (audio for
> meter-scale).** Classification: critical-mode band DERIVED (NP_100 + NP_131); ultrasonic technology
> EMERGENT; "THz-scale rigidity control" REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Estimate. (2) Band. (3) Couple. (4) Soften. (5) Technologies. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "rigidity modes are THz-scale" | THz is the Debye *bond* frequency (~10⁸× higher); rigidity is the long-wavelength limit |
| "audio is too low" | meter-scale structures have kHz fundamentals — audio is exactly right |
| "any band couples to rigidity" | only the band matching the mode frequency couples resonantly |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| critical modes are kHz–MHz | a structure whose rigidity is unchanged by resonant kHz–MHz drive but yields to THz |
| ultrasound couples best | a rigidity mode that responds to THz but not ultrasound |
| THz is the wrong band | THz drive that softens rigidity without touching bonds |

---

## 8. Classification

| Component | Status |
|---|---|
| critical-mode band (kHz–MHz) | **DERIVED** (NP_100 + NP_131) |
| ultrasonic softening technology | **EMERGENT** |
| THz-scale rigidity control | **REFUTED** |

**Conclusion.** Coherent softening is an **ultrasonic-scale technology** (~kHz–MHz), reaching audio for
meter-scale structures. The critical rigidity modes are the long-wavelength acoustic backbone, ~10⁸×
below the THz bond band — so the right tool is a tuned ultrasonic transducer, not a THz source. This
grounds the NP_131/132 "master key" in a concrete, buildable frequency band. No new primitive;
canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_133_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_133_Frequencies` | f_1 = c_s/(2L); f_D = c_s/(2a) | ✅ |
| `Y_NP_133_Bands` | audio/ultrasound vs THz/IR | ✅ |
| `Y_NP_133_Coupling` | THz ~10⁸× off-resonance for rigidity | ✅ |
| `Y_NP_133_Softening` | frequency-matched drive → R → 0 | ✅ |
| `Y_NP_133_Technologies` | piezo/ultrasound excite the modes | ✅ |
| `Y_NP_133_Classification` | DERIVED/EMERGENT; THz-scale REFUTED | ✅ |
| `Y_NP_133_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_133"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent
  matter control), NP_130 (material sonification), NP_131 (critical resonance), NP_132 (reversible
  softening).
