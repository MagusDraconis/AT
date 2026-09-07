# ResearchY-NP_139 — Soft Mode Literature Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_139 (permanent)
**Title:** Soft Mode Literature Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_139.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_131 (critical resonance), NP_132
(reversible softening), NP_133 (critical mode frequency), NP_137 (real-world evidence), NP_138
(quantized softening)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_139_Tests.cs`

---

## Purpose

NP_131–133 proposed that rigidity is controlled by a small set of critical (load-bearing) modes that
a frequency-matched coherent drive can soften reversibly; NP_137/138 then grounded the mechanism in
real data and removed the quantization (the softening is continuous). NP_139 asks the *identity*
question: **does known condensed-matter physics already contain the equivalent of the AT critical-mode
softening mechanism?** Program: (1) inventory soft modes, acoustic softening, giant elastic anomalies,
martensitic transitions, ferroelastic transitions, ultrasonic modulus reduction; (2) compare AT's
continuous softening against the experimental literature; (3) determine whether AT predicts A) known
soft-mode physics, B) an extension, or C) a genuinely new effect; (4) locate the strongest
correspondence and strongest discrepancy. **Success criterion:** determine whether the surviving
NP_131–138 mechanism is new physics or a new interpretation of existing soft-mode physics. No new
primitives; canonical AT unchanged.

---

## 1. Inventory of known phenomena

| # | Phenomenon | What it is | Band / variable |
|---|---|---|---|
| 1 | **Soft modes** (Cochran–Anderson) | a specific phonon mode whose frequency softens ω² ∝ (T−T_c) → 0, driving a displacive phase transition | temperature |
| 2 | **Acoustic softening** (acoustoplastic) | ultrasound lowers the flow/yield stress, athermally and reversibly (Langenecker) | 20 kHz–1 MHz |
| 3 | **Giant elastic anomalies** | a specific elastic constant (e.g. C′ = (C₁₁−C₁₂)/2) softens strongly toward a structural transition | temperature |
| 4 | **Martensitic transitions** | diffusionless first-order transition preceded by acoustic-phonon (TA₂) softening | temperature |
| 5 | **Ferroelastic transitions** | spontaneous strain is the order parameter; the relevant elastic susceptibility diverges | temperature |
| 6 | **Ultrasonic modulus reduction** (nonlinear mesoscopic elasticity) | continuous, amplitude-dependent elastic-modulus reduction | strain amplitude |

---

## 2. AT continuous softening vs the literature

AT's surviving mechanism (NP_131–133, refined by NP_137/138) has four ingredients. Each maps onto a
known phenomenon:

| AT ingredient | Known equivalent | Match |
|---|---|---|
| rigidity carried by a small set of critical modes | a single soft mode / soft elastic constant dominates the structural instability | **strong** |
| critical modes are long-wavelength acoustic phonons (kHz–MHz, NP_133) | soft TA₂ acoustic phonon / soft C′ elastic constant | **strong** |
| frequency-matched drive reduces rigidity reversibly & athermally | acoustic softening (acoustoplastic) / nonlinear mesoscopic elasticity | **strong** |
| softening is **continuous** (NP_138) | continuous amplitude-dependent modulus reduction | **strong** |

The *ingredients* of the AT mechanism are textbook condensed-matter physics. What distinguishes AT is
the *operational claim* and the *framing*, not the ingredients.

---

## 3. A / B / C determination

| Determination | Content | Status |
|---|---|---|
| **A) known soft-mode physics** | "critical modes = soft modes"; "rigidity controlled by a few modes" | **CORRESPONDENCE** — this IS soft-mode / elastic-anomaly physics |
| **B) an extension** | a frequency-matched coherent (acoustic) drive produces a **reversible, athermal, order-preserving** rigidity reduction at **fixed T** — a variable-rigidity dial | **EXTENSION** — beyond temperature-driven soft-mode condensation |
| **C) a genuinely new effect** | a distinct mechanism with no known analog | **REFUTED** — the mechanism is a reinterpretation/extension, not new |

The verdict is **B — an extension**. AT is a *new interpretation and extension* of known soft-mode and
acoustic-softening physics: its "critical modes" are the condensed-matter "soft modes", but its
operational claim (coherent, low-entropy, reversible, order-preserving softening at fixed temperature)
goes beyond the canonical soft mode, which softens with temperature and *culminates in a phase
transition*.

---

## 4. Strongest correspondence and strongest discrepancy

**Strongest correspondence — soft-mode theory + premartensitic elastic softening.** Cochran's soft
mode (ω² ∝ T−T_c) and the C′ shear-constant softening measured by resonant ultrasound spectroscopy in
shape-memory alloys (NiTi, Cu-based, Ni–Mn–Ga) are *literally* AT's "critical mode": a single,
long-wavelength acoustic mode whose softening controls the macroscopic rigidity/instability. This is
the NP_131 "master key" and the NP_133 "acoustic critical mode", known and measured for decades.

**Strongest discrepancy — the driving variable and the transition.** A canonical soft mode softens with
**temperature** and terminates in an actual **phase transition** (ferroelectric, martensitic,
ferroelastic — order changes). AT claims a **room-temperature, resonant, order-preserving (S ≈ 1),
reversible** rigidity reduction *without* a transition. The known soft mode is the *harbinger of a
structural change*, not a fixed-order reversible dial. No soft-mode experiment demonstrates a
fixed-temperature, field-driven, order-preserving variable rigidity; the nearest phenomena (acoustic
softening, NME) are athermal but continuous and do not invoke a "master key" or "coherence as a
resource".

---

## Theorem

> **Theorem (NP_139).** The surviving NP_131–138 mechanism is an EXTENSION (B) — a new interpretation
> and extension of known soft-mode physics, not genuinely new physics. Its ingredients are textbook
> condensed matter: AT's "critical modes" are Cochran–Anderson soft modes; "rigidity controlled by a
> small mode set" is the soft elastic constant / soft acoustic phonon; "frequency-matched reversible
> softening" is acoustic softening (acoustoplastic) and nonlinear mesoscopic elasticity; "continuous
> softening" is the observed amplitude-dependent modulus reduction. What AT adds is operational, not
> physical: a frequency-matched coherent (acoustic) drive producing a REVERSIBLE, ATHERMAL,
> ORDER-PRESERVING rigidity reduction at FIXED temperature — a variable-rigidity dial — whereas the
> canonical soft mode softens with temperature and culminates in a phase transition. So A (known
> soft-mode physics) is a CORRESPONDENCE; B (the fixed-T coherent dial) is an EXTENSION; C (a genuinely
> new effect) is REFUTED. Proof: (1) Inventory (Section 1). (2) Compare (Section 2, verified — all four
> ingredients match). (3) Determine (Section 3). (4) Locate (Section 4). **Success criterion: the
> mechanism is a new interpretation/extension of existing soft-mode physics, not new physics.** No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Compare. (3) Determine. (4) Locate. ∎

---

## 5. Counterexamples

| Attempt | Why it fails |
|---|---|
| "AT's critical mode is a new object" | it is the Cochran–Anderson soft mode / soft acoustic phonon, known since 1959 |
| "fixed-T coherent softening is known physics" | known soft modes soften with temperature and end in a phase transition; no experiment shows a fixed-T order-preserving dial |
| "AT predicts a genuinely new effect" | every ingredient has a known analog; only the operational framing is new |
| "AT is pure rediscovery (A only)" | the fixed-T, order-preserving, coherent reversible drive is an operational extension beyond soft-mode condensation |

---

## 6. Falsification paths

| Claim | Falsification |
|---|---|
| mechanism is an extension (B), not new (C) | a soft-mode / elastic-anomaly result with NO known analog in condensed matter |
| critical modes = soft modes | an AT critical mode that does not correspond to any soft phonon / soft elastic constant |
| fixed-T coherent dial is the extension | an existing fixed-T, field-driven, order-preserving reversible modulus dial |

---

## 7. Classification

| Component | Status |
|---|---|
| critical modes = soft modes | **CORRESPONDENCE** (known soft-mode physics) |
| rigidity controlled by a small mode set | **CORRESPONDENCE** (soft elastic constant / soft phonon) |
| frequency-matched, athermal, reversible softening | **CORRESPONDENCE** (acoustic softening / NME, NP_137) |
| fixed-T, order-preserving, coherent variable-rigidity dial | **EXTENSION** (B) |
| genuinely new effect | **REFUTED** (C) |

**Conclusion.** The surviving NP_131–138 mechanism is a **new interpretation and extension (B)** of
existing soft-mode physics, not new physics. AT's "critical modes" are the soft modes of condensed
matter; its "master key" is the soft elastic constant / soft acoustic phonon; its "reversible coherent
softening" is acoustic softening and nonlinear mesoscopic elasticity. The genuinely new element is
*operational and interpretative* — a fixed-temperature, order-preserving, coherent variable-rigidity
dial framed as low-entropy coherence — not a new physical effect. This is a positive result for the
project's honesty: the mechanism is anchored in real, well-understood physics, and the open question is
whether the *extension* (fixed-T coherent reversible dial) survives NP_135's decisive experiment. No
new primitive; canonical AT unchanged.

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_139_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_139_Inventory` | six phenomena present | ✅ |
| `Y_NP_139_Compare` | four AT ingredients map to known physics | ✅ |
| `Y_NP_139_Determine` | A correspondence; B extension; C refuted; overall B | ✅ |
| `Y_NP_139_Strongest` | correspondence = soft mode + C′ anomaly; discrepancy = T-driven transition vs fixed-T dial | ✅ |
| `Y_NP_139_Classification` | overall B (extension); decomposition CORRESPONDENCE/EXTENSION/REFUTED | ✅ |
| `Y_NP_139_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_139"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent matter
  control), NP_131 (critical resonance), NP_132 (reversible softening), NP_133 (critical mode
  frequency), NP_137 (real-world evidence), NP_138 (quantized softening).
- Real-world record: Cochran (1959/1960) & Anderson (1960) soft-mode theory (ω² ∝ T−T_c); Lyddane–
  Sachs–Teller relation; premartensitic C′ = (C₁₁−C₁₂)/2 softening and TA₂ phonon softening in shape-
  memory alloys (laser-based RUS); Blaha & Langenecker acoustic softening; nonlinear mesoscopic
  elasticity / dynamic acoustoelasticity.
