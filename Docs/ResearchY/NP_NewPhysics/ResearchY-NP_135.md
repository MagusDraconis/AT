# ResearchY-NP_135 — Coherent Softening Experimental Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_135 (permanent)
**Title:** Coherent Softening Experimental Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_135.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_131 (critical resonance), NP_132
(reversible softening), NP_133 (critical mode frequency), NP_134 (critical mode discovery)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_135_Tests.cs`

---

## Purpose

NP_131–134 established the hypothesis (small set of critical modes controls rigidity; ultrasonic
excitation couples to them) and a discovery procedure. NP_135 asks the falsification question: **what
is the simplest laboratory experiment that could falsify the critical-mode softening hypothesis?**
Program: (1) select aluminum / quartz / steel; (2) define baseline rigidity R₀; (3) apply
off-resonance ultrasound; (4) apply critical-mode ultrasound; (5) measure stiffness / Young modulus /
damping; (6) determine expected ΔR; (7) compare thermal vs coherent effects. **Success criterion:**
produce a decisive pass/fail experiment for coherent softening. No new primitives; canonical AT
unchanged.

---

## 1. Materials & their critical-mode frequency

Critical-mode fundamental f₁ = c_s/(2L) (NP_133). For a 10 cm bar:

| Material | c_s (m/s) | E (GPa) | f₁ (kHz) | dE/E·dT (K⁻¹) |
|---|---|---|---|---|
| **aluminum** | 5100 | 69 | 25.50 | 4.5 × 10⁻⁴ |
| **quartz** | 5750 | 72 | 28.75 | 1.5 × 10⁻⁴ |
| **steel** | 5100 | 200 | 25.50 | 2.4 × 10⁻⁴ |

All three critical modes are **ultrasonic (~25–29 kHz)** — easily excited by a piezoelectric
transducer.

---

## 2. Baseline rigidity R₀

Measure the baseline rigidity by **resonant ultrasound spectroscopy (RUS)** or impulse excitation: the
fundamental resonant frequency f₀ gives the Young modulus E (and shear modulus G). Record R₀ = f₀²
(∝ E) and the quality factor Q₀ (damping).

---

## 3/4. The two drives (the heart of the experiment)

| Drive | Frequency | Role |
|---|---|---|
| **off-resonance** | f_off ≠ f₁ (e.g. 2×f₁) | **thermal control** — deposits the same energy, heats equally, but does NOT drive the critical mode |
| **critical-mode** | f₁ (the critical mode) | **coherent + thermal** — drives the backbone mode |

Both drives run at **matched power** (same energy, same ΔT). This is the decisive control: if rigidity
depends only on heating, the two drives must produce identical ΔR.

---

## 5. Measurements

| Quantity | Method |
|---|---|
| stiffness / Young modulus | shift of the resonant frequency f₀ (ΔE/E = 2Δf₀/f₀) |
| damping | quality factor Q (or loss tangent tan δ) |
| temperature | thermocouple/IR — to confirm matched ΔT |

---

## 6. Expected ΔR

| Effect | Magnitude |
|---|---|
| **thermal** (both drives) | ΔR/R₀ = −α·ΔT ≈ **0.015–0.045 %** per K (tiny) |
| **coherent** (critical-mode only) | large, **reversible**, excess drop beyond the thermal baseline |

The hypothesis (NP_131/132) predicts the critical-mode drive produces a **large excess** rigidity drop
beyond the thermal baseline — reversible (recovers when the drive stops), frequency-selective (absent
off-resonance), and without melting (order survives).

---

## 7. The decisive pass/fail criterion

Define the ratio of rigidity drops at matched power:

```
R = ΔR(f₁) / ΔR(f_off)
```

| Verdict | Condition |
|---|---|
| **PASS** (coherent softening real) | R ≫ 1 **and** the drop is reversible **and** frequency-selective (absent off-resonance) |
| **FAIL** (thermal-only) | R ≈ 1 — the drop depends only on ΔT, not on resonance |

**This is decisive**: the off-resonance drive is the built-in thermal control. Any rigidity drop
*in excess* of the thermal baseline, that is *reversible* and *frequency-selective*, can only be the
coherent critical-mode effect. If no such excess exists, the hypothesis is falsified.

---

## Theorem

> **Theorem (NP_135).** The critical-mode softening hypothesis is falsifiable by a simple matched-
> power experiment: drive a 10 cm bar (aluminum/quartz/steel) with ultrasound OFF-resonance (thermal
> control) and ON-resonance (the critical mode f₁ = c_s/(2L) ≈ 25–29 kHz), at equal power; measure the
> rigidity via the resonant-frequency shift, the damping via Q, and ΔT. Define R = ΔR(f₁)/ΔR(f_off).
> The hypothesis PREDICTS R ≫ 1 (a large, reversible, frequency-selective excess beyond the thermal
> baseline ΔR/R₀ = −α·ΔT ≈ 0.015–0.045 % per K), with order surviving (no melting). The experiment
> FALSIFIES the hypothesis iff R ≈ 1 (the drop depends only on ΔT, not on resonance). The off-resonance
> drive is the built-in thermal control, so the comparison is clean: any reversible excess is coherent.
> Proof: (1) Select (Section 1, verified — f₁ ≈ 25–29 kHz). (2) Baseline (Section 2). (3/4) Drives
> (Sections 3–4). (5) Measure (Section 5). (6) ΔR (Section 6, verified — thermal ~0.015–0.045%/K).
> (7) Compare (Section 7). **Success criterion: a decisive pass/fail experiment.** Classification: the
> experiment design EMERGENT (a falsifiable test of the DERIVED hypothesis NP_131/132); "coherent
> softening is indistinguishable from heating" is the falsification condition (REFUTED if R ≫ 1). No
> new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Select. (2) Baseline. (3/4) Drive. (5) Measure. (6) ΔR. (7) Compare. ∎

---

## 8. Counterexamples / controls

| Concern | Control |
|---|---|
| "the drop is just heating" | matched-power off-resonance drive isolates the thermal baseline |
| "the drop is not reversible" | measure ΔR after the drive stops (must recover) |
| "the drop is not frequency-selective" | scan f; the excess must peak at f₁ only |
| "the sample melted" | measure order (XRD or Q recovery); order must survive |

---

## 9. Classification

| Component | Status |
|---|---|
| critical-mode softening hypothesis | **DERIVED** (NP_131/132) |
| falsification experiment design | **EMERGENT** |
| coherent ≡ thermal (indistinguishable) | **REFUTED** iff R ≫ 1 |

**Conclusion.** A decisive, falsifiable experiment exists: matched-power **off-resonance vs
critical-mode** ultrasound on a 10 cm bar, reading rigidity via resonant-frequency shift and damping.
The off-resonance drive is the thermal control; any reversible, frequency-selective excess (R ≫ 1) is
coherent softening, and R ≈ 1 falsifies the hypothesis. This converts NP_131/132 from a theory to a
testable prediction. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_135_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_135_Materials` | f₁ = c_s/(2L) ≈ 25–29 kHz | ✅ |
| `Y_NP_135_Baseline` | R₀ = f₀² (RUS) | ✅ |
| `Y_NP_135_Drives` | off-resonance (control) vs critical-mode | ✅ |
| `Y_NP_135_ThermalBaseline` | ΔR/R₀ = −α·ΔT ≈ 0.015–0.045 %/K | ✅ |
| `Y_NP_135_Criterion` | R ≫ 1 PASS; R ≈ 1 FAIL | ✅ |
| `Y_NP_135_Classification` | EMERGENT; coherent≡thermal falsification | ✅ |
| `Y_NP_135_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_135"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent
  matter control), NP_131 (critical resonance), NP_132 (reversible softening), NP_133 (critical mode
  frequency), NP_134 (critical mode discovery).
