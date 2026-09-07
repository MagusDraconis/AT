# ResearchY-NP_140 — Fixed-Temperature Softening Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_140 (permanent)
**Title:** Fixed-Temperature Softening Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_140.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_131 (critical resonance), NP_132
(reversible softening), NP_133 (critical mode frequency), NP_135 (experimental audit), NP_136
(variable rigidity bounds), NP_137 (real-world evidence), NP_138 (quantized softening), NP_139 (soft
mode literature)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_140_Tests.cs`

---

## Purpose

NP_139 identified the *extension* that separates AT from known soft-mode physics: a fixed-temperature,
order-preserving, coherent variable-rigidity dial — large reversible rigidity reduction at constant T
without a phase transition. NP_140 asks whether that dial actually exists. Program: (1) inventory the
known experiments (resonant ultrasound, acoustic softening, ferroelastic softening, martensitic
precursors); (2) separate temperature-driven effects from drive-induced effects; (3) determine the
maximum documented ΔE/E and ΔG/G **at fixed T**; (4) compare with NP_136's 10% / 50% / 90% tiers;
(5) identify the strongest evidence FOR and AGAINST a true variable-rigidity dial. **Success
criterion:** determine whether a large, reversible, fixed-temperature softening mechanism is
SUPPORTED, PARTIAL, CONTRADICTED, or UNKNOWN. No new primitives; canonical AT unchanged.

---

## 1. Inventory of known experiments

| # | Experiment | What is measured | Driven by |
|---|---|---|---|
| 1 | **Resonant ultrasound (RUS)** | eigenfrequencies → elastic moduli E, G | temperature (and small strain) |
| 2 | **Acoustic softening** (acoustoplastic) | flow/yield stress under ultrasound | **drive** (ultrasonic amplitude) |
| 3 | **Ferroelastic softening** | elastic susceptibility near a ferroelastic transition | **temperature** |
| 4 | **Martensitic precursors** | C′ = (C₁₁−C₁₂)/2 / TA₂ phonon softening toward M_s | **temperature** |
| 5 | **Nonlinear mesoscopic elasticity (NME)** | elastic modulus vs strain amplitude | **drive** (strain amplitude) |

Two of these are temperature-driven (3, 4); two are drive-induced (2, 5); one is a probe (1).

---

## 2. Separate temperature-driven from drive-induced effects

| Effect class | Mechanism | Fixed-T? | Reversible? | Channel |
|---|---|---|---|---|
| **temperature-driven** (soft mode, ferroelastic, martensitic) | ω² ∝ (T−T_c) → 0 | no (needs T → T_c) | no (phase transition; order changes) | elastic modulus |
| **drive-induced — plastic** (acoustic softening) | ultrasonic energy → dislocation motion | **yes** | **yes** (re-hardens off-ultrasound) | **flow stress** |
| **drive-induced — elastic** (NME) | strain-amplitude nonlinearity at contacts/microcracks | **yes** | yes but **slow** (slow dynamics) | elastic modulus |

The distinction is decisive: **large** elastic-modulus reduction is temperature-driven and phase-
transition-coupled; **drive-induced** reduction is either large-but-plastic or elastic-but-small.

---

## 3. Maximum documented ΔE/E, ΔG/G at fixed T

| Quantity | Fixed-T reversible maximum | Source | Notes |
|---|---|---|---|
| **ΔE/E, ΔG/G** (elastic, drive-induced) | **≈ 0.01–0.30** (typically ~0.01–0.10) | NME / dynamic acoustoelasticity | continuous, slow-reversible, in disordered/granular media |
| **Δσ/σ** (plastic, drive-induced) | **≈ 0.50–0.90** | acoustic softening | transient, athermal — but *not* rigidity |
| **ΔE/E** (elastic, temperature-driven) | → **≈ 1.00** at T_c | soft mode / ferroelastic / martensitic | a phase transition (order changes), not fixed-T |

At fixed T, the largest documented **elastic** rigidity reduction is ~0.30 (and typically much less);
the large 0.50–0.90 effects are **plastic flow stress**, and the ~1.00 elastic softening is
**temperature-driven**.

---

## 4. Compare with NP_136 tiers (10% / 50% / 90%)

| NP_136 tier | Fixed-T elastic status | Evidence |
|---|---|---|
| **10%** | **PARTIAL** — reachable at high sub-damage strain in soft granular/rock, not a clean dial | NME ~10–30% |
| **50%** | **NOT supported** in the elastic channel — reached only by *plastic* flow stress | acoustic softening |
| **90%** | **NOT supported** — reached only by temperature-driven phase transitions | soft mode / martensitic |

NP_136's "variable-rigidity technology (10–90%)" is therefore **overstated for the fixed-T elastic
channel**: only the 10% tier has partial fixed-T elastic support; 50–90% exist only as plastic or
temperature-driven effects.

---

## 5. Strongest evidence FOR and AGAINST

**FOR — the dial's principle is real.** Acoustic softening shows a *drive-induced, athermal, reversible*
reduction that reverts when ultrasound stops (Langenecker), and nonlinear mesoscopic elasticity shows a
*drive-induced, fixed-T, reversible* elastic-modulus reduction (up to ~30%). So "a coherent drive
reversibly softens a material at fixed T" is supported — but small (elastic) or plastic (large).

**AGAINST — the large fixed-T elastic dial is not observed.** The elastic modulus is a collective
(long-wavelength) property carried by the whole lattice, not a few discrete modes (this is why NP_138
found continuous, not quantized, softening). Large elastic reductions require either approaching a
phase transition (soft mode → T_c, order changes) or extensive damage (irreversible). No experiment
shows a large (≥50%), reversible, order-preserving **elastic-modulus** reduction at fixed T in an
ordered material.

---

## Theorem

> **Theorem (NP_140).** A large, reversible, fixed-temperature elastic-rigidity dial is PARTIAL —
> its principle is supported, but its magnitude is not. Drive-induced fixed-T softening exists: acoustic
> softening gives a large (0.50–0.90) but PLASTIC (flow-stress) reduction, and nonlinear mesoscopic
> elasticity gives an ELASTIC (modulus) reduction of only ~0.01–0.30 (typically ~0.01–0.10), slow-
> reversible and in disordered/granular media. Large (≈1.00) elastic softening is temperature-driven
> (soft mode / ferroelastic / martensitic) and terminates in a phase transition, not a fixed-T order-
> preserving dial. Against NP_136's 10/50/90% tiers, only 10% has partial fixed-T elastic support; 50%
> is plastic-only and 90% is temperature-only. Proof: (1) Inventory (Section 1). (2) Separate
> (Section 2). (3) Max fixed-T (Section 3, verified — elastic ≤ 0.30, plastic ≤ 0.90, thermal ≤ 1.00).
> (4) Compare (Section 4). (5) For/against (Section 5). **Success criterion: PARTIAL — the principle is
> supported, the large fixed-T elastic magnitude is UNKNOWN (unobserved).** No new primitive; canonical
> AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Separate. (3) Bound. (4) Compare. (5) Locate. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "acoustic softening proves the fixed-T elastic dial" | it reduces *flow stress* (plastic), not the elastic modulus |
| "martensitic C′ softening proves the fixed-T dial" | temperature-driven and phase-transition-coupled; order changes |
| "NME proves a 90% fixed-T dial" | NME elastic reduction caps near ~30% (typically ~1–10%) |
| "the dial is impossible" | drive-induced fixed-T reversible softening (NME) IS observed, just small |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| large fixed-T elastic dial is PARTIAL/UNKNOWN | a reproducible ≥50% reversible elastic-modulus reduction at fixed T, order-preserving |
| elastic modulus is collective, not few-mode | a material whose elastic modulus is carried by a small discrete mode set that unlocks without a phase transition |
| 90% is temperature-only | a fixed-T elastic ΔE/E ≈ 0.9 without a transition |

---

## 8. Classification

| Component | Status |
|---|---|
| drive-induced, fixed-T, athermal, reversible softening (principle) | **SUPPORTED** (acoustic softening, NME) |
| large (≥50%) fixed-T elastic-rigidity reduction | **UNKNOWN** (not observed; only plastic or thermal) |
| small (≤30%) fixed-T elastic-rigidity reduction | **SUPPORTED** (NME) |
| fixed-T order-preserving elastic dial in an ordered material | **UNKNOWN** |

**Conclusion.** The overall verdict is **PARTIAL**: the *principle* of a drive-induced, fixed-
temperature, reversible softening dial is experimentally supported, but the *magnitude* AT requires —
a large (50–90%), reversible, order-preserving **elastic-modulus** reduction at constant T — is not
documented; it is UNKNOWN (unobserved), because large elastic softening is temperature-driven and
phase-transition-coupled, while drive-induced elastic softening is small (≤30%) and the large drive-
induced effects are plastic. NP_136's "10–90% variable-rigidity technology" should be read as
**≤ ~30% for fixed-T elastic rigidity**, with 50–90% confined to plastic or thermal channels. The
decisive test remains NP_135 (matched-power, reading the *elastic modulus*), now with a corrected
expectation: look for a small-to-moderate, continuous, slow-reversible fixed-T modulus drop, not a
quantized 33/67/100% or a 90% dial. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_140_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_140_Inventory` | five experiments present | ✅ |
| `Y_NP_140_Separate` | temperature-driven vs drive-induced split | ✅ |
| `Y_NP_140_MaxFixedT` | elastic ≤ 0.30; plastic ≤ 0.90; thermal ≤ 1.00 | ✅ |
| `Y_NP_140_CompareNp136` | 10% partial; 50% plastic; 90% thermal | ✅ |
| `Y_NP_140_Strongest` | FOR = NME + acoustoplastic; AGAINST = large elastic only via phase transition | ✅ |
| `Y_NP_140_Classification` | overall PARTIAL | ✅ |
| `Y_NP_140_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_140"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent matter
  control), NP_131 (critical resonance), NP_132 (reversible softening), NP_133 (critical mode
  frequency), NP_135 (experimental audit), NP_136 (variable rigidity bounds), NP_137 (real-world
  evidence), NP_138 (quantized softening), NP_139 (soft mode literature).
- Real-world record: nonlinear mesoscopic elasticity / dynamic acoustoelasticity (fixed-T elastic
  modulus reduction, slow dynamics); Blaha & Langenecker acoustic softening (fixed-T athermal flow-
  stress reduction); Cochran–Anderson soft mode and premartensitic C′ / TA₂ softening (temperature-
  driven elastic reduction culminating in a phase transition).
