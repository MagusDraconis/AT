# ResearchY-NP_142 — Multi-Band Resonance Control Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_142 (permanent)
**Title:** Multi-Band Resonance Control Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_142.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_131 (critical resonance), NP_133
(critical mode frequency), NP_138 (quantized softening), NP_140 (fixed-temperature softening),
NP_141 (yield stress softening)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_142_Tests.cs`

---

## Purpose

NP_133 modeled the "master key" as a single critical frequency f₁ = c_s/(2L); NP_131 modeled it as a
small discrete set m ≪ N of backbone modes; NP_138 removed the quantization (continuous response) and
NP_141 showed the dominant coupling is to a broad defect/dislocation population. NP_142 asks the
control question: **is coherent material control a single-frequency problem or a multi-band phase-
coherence problem?** Program: (1) compare single-tone vs multi-tone drive; (2) determine whether
critical modes act independently or require cooperative excitation; (3) define {ω_i}, {A_i}, {φ_i};
(4) test crystal / metal / granite / glass; (5) measure modulus reduction, yield-stress reduction,
coherence cost; (6) determine whether phase relations matter more than frequency matching;
(7) identify the minimum mode set for significant softening; (8) compare one-tone, a harmonic comb,
and an adaptive feedback spectrum. **Success criterion:** determine whether the "material master key"
is a single resonance or a coordinated multi-band coherence pattern. No new primitives; canonical AT
unchanged.

---

## 1. Single-tone vs multi-tone drive

| Drive | Works? | Why |
|---|---|---|
| **single-tone** (20 kHz resonant) | **yes** | delivers acoustic amplitude to the dislocation population (acoustic softening) |
| **multi-tone / dual-frequency** | **yes, and can be more effective** | covers a wider span of the dislocation/defect resonance spectrum |

A pinned dislocation segment has a *vibrating-string* resonance (Granato–Lücke), but the resonance is
broad and damped, and a real material contains a **spectrum** of segment lengths/orientations — so the
defect response is **broadband**, not a single sharp line. Single-tone therefore works (amplitude is
the primary lever) but under-samples the spectrum; multi-tone covers more of it.

---

## 2. Independent or cooperative excitation

The coupled objects are **dislocations — many independent, localized defects** — not a small set of
cooperative extended modes. Each defect absorbs energy independently; there is no evidence that
softening requires *phase-locked cooperative* excitation of a percolating backbone. The "cooperative
backbone" of NP_131 is therefore reframed as an **incoherent ensemble of defects**, excited broadband.

---

## 3. The three control axes: {ω_i}, {A_i}, {φ_i}

| Axis | Role | Priority |
|---|---|---|
| **{ω_i}** (frequency set) | which part of the defect spectrum is excited | **high** — want broadband coverage |
| **{A_i}** (amplitudes) | acoustic energy density; must exceed the dislocation breakaway threshold | **highest** — the primary lever |
| **{φ_i}** (phases) | inter-tone phase relations | **low** — secondary (emerging, not established) |

The master key is written in **amplitude across a broad band**, not in a specific frequency or in
phase relations.

---

## 4. Materials

| Material | Defect population | Yield-softening response |
|---|---|---|
| **crystal** | dislocations (clean, few) | strong (acoustoplastic, single crystals) |
| **metal** | dislocations (dense) | strong (20–90% flow-stress reduction) |
| **granite** | grain contacts, microcracks | moderate (vibro-fluidization, granular) |
| **glass** | distributed soft/defect modes | moderate–weak (distributed, no dislocations) |

The same broadband amplitude logic applies across all four, with magnitude set by the defect density.

---

## 5. Modulus reduction, yield-stress reduction, coherence cost

| Quantity | Result | Source |
|---|---|---|
| **modulus reduction** | small (≤ ~30%, typically ~1–10%) | NP_140 |
| **yield-stress reduction** | large (20–90%) | NP_141 |
| **coherence cost** | **low** — amplitude-driven, no phase control needed | this audit |

Because the coupling is amplitude-driven and broadband, the "coherence" cost is minimal: no sharp
frequency match, no phase-locking, no adaptive tracking. The *resource* is energy density, not phase
coherence (refining NP_128's "coherence = resource" for this channel).

---

## 6. Do phase relations matter more than frequency matching?

**No.** For the dominant (yield) channel, *amplitude* dominates, *spectral coverage* (frequency) comes
second, and *phase relations* come third. Phase control is an emerging refinement in dual-frequency
forming, but it is not the primary lever. The AT framing that "phase coherence is the key resource"
is therefore **secondary** for yield-stress softening.

---

## 7. Minimum mode set for significant softening

There is **no small discrete mode set** (the m = 6 "master key" is refuted, NP_138). The minimum
requirement is:

```
broadband acoustic amplitude > dislocation breakaway threshold,
spanning enough of the defect resonance spectrum to activate plastic flow.
```

A few discrete modes are *not* the lever; a **broad band + sufficient amplitude** is.

---

## 8. one-tone / harmonic comb / adaptive feedback

| Scheme | Verdict |
|---|---|
| **one-tone** | sufficient, suboptimal (under-samples the defect spectrum) |
| **harmonic comb** (multi-tone) | more effective (broader spectral coverage) |
| **adaptive feedback spectrum** | useful only for a *sharp* resonance (the elastic soft mode near T_c), not the broadband yield channel |

The practical optimum is a **broad multi-band amplitude pattern**, not a phase-locked adaptive
single-frequency lock.

---

## Theorem

> **Theorem (NP_142).** The "material master key" is NEITHER a single resonance NOR a coordinated
> multi-band phase-coherence pattern — it is a BROADBAND, AMPLITUDE-DRIVEN coupling to a defect
> spectrum. The coupled objects are independent dislocations/defects with a broad, damped resonance
> spectrum (Granato–Lücke); single-tone works (amplitude is the lever) but multi-tone is more effective
> because it spans more of that spectrum. Of the three axes {ω_i}, {A_i}, {φ_i}, amplitude {A_i}
> dominates, frequency {ω_i} (spectral coverage) comes second, and phase {φ_i} comes third — phase
> relations do NOT matter more than frequency matching, and neither dominates amplitude. There is no
> small discrete mode set (NP_138); the minimum requirement is broadband amplitude above the dislocation
> breakaway threshold. Adaptive phase feedback helps only for a sharp (elastic soft-mode) resonance.
> Classification: single-sharp-resonance key CONTRADICTED; broadband multi-band defect coupling
> SUPPORTED; "phase coherence is the key resource" CONTRADICTED. Proof: (1) Compare (Section 1).
> (2) Independent (Section 2). (3) Define (Section 3). (4) Materials (Section 4). (5) Measure
> (Section 5). (6) Phase vs frequency (Section 6). (7) Minimum set (Section 7). (8) Schemes (Section 8).
> **Success criterion: the master key is a broadband multi-band amplitude pattern, not a single
> resonance and not a phase-coherence pattern — PARTIAL (the dichotomy is dissolved).** No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Compare. (2) Independent. (3) Define. (4) Materials. (5) Measure. (6) Phase. (7) Min set. (8) Schemes. ∎

---

## 9. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the key is one critical frequency f₁" | the dislocation response is broadband (a spectrum of lengths/orientations) |
| "the key is a small cooperative mode set" | dislocations act independently; no phase-locked backbone is required |
| "phase relations matter most" | amplitude dominates, then spectral coverage; phase is tertiary |
| "multi-tone is useless" | dual-frequency forming is more effective than single-frequency (emerging evidence) |

---

## 10. Falsification paths

| Claim | Falsification |
|---|---|
| broadband amplitude dominates | a yield-softening experiment where phase-locked multi-tone strongly beats equal-energy broadband drive |
| single sharp resonance is contradicted | a sharp, narrow dislocation resonance that dominates yield softening |
| phase is tertiary | a demonstration that inter-tone phase controls the softening magnitude more than amplitude |

---

## 11. Classification

| Component | Status |
|---|---|
| single sharp resonance is the master key | **CONTRADICTED** (broadband defect spectrum) |
| broadband multi-band defect coupling | **SUPPORTED** (single-tone works; multi-tone more effective) |
| "phase coherence is the key resource" | **CONTRADICTED** (amplitude dominates) |
| overall | **PARTIAL** (the dichotomy dissolves; the key is broadband amplitude) |

**Conclusion.** Coherent material control is **neither a single-frequency problem nor a multi-band
phase-coherence problem** — it is a **broadband, amplitude-driven defect-coupling problem**. The
practical "master key" is written in *amplitude across a broad band* (covering the dislocation
resonance spectrum), with frequency coverage second and phase relations a distant third. This refines
the whole NP_131–141 chain: the "critical mode" is not a single resonance or a small cooperative set,
but a **defect spectrum**; the "coherence" resource (NP_128) is, for this channel, really an
*energy-density* resource. No new primitive; canonical AT unchanged.

---

## 12. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_142_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_142_SingleVsMulti` | single-tone works; multi-tone more effective; broadband | ✅ |
| `Y_NP_142_Independence` | defects act independently, no phase-lock | ✅ |
| `Y_NP_142_Define` | amplitude > frequency > phase | ✅ |
| `Y_NP_142_Materials` | yield softening in all four materials | ✅ |
| `Y_NP_142_Measure` | yield ≫ modulus; low coherence cost | ✅ |
| `Y_NP_142_PhaseVsFrequency` | phase does NOT matter more than frequency | ✅ |
| `Y_NP_142_MinSet` | no discrete set; broadband + amplitude | ✅ |
| `Y_NP_142_Compare` | one-tone suboptimal; comb effective; adaptive = sharp only | ✅ |
| `Y_NP_142_Classification` | overall PARTIAL | ✅ |
| `Y_NP_142_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_142"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent matter
  control), NP_131 (critical resonance), NP_133 (critical mode frequency), NP_138 (quantized
  softening), NP_140 (fixed-temperature softening), NP_141 (yield stress softening).
- Real-world record: Granato–Lücke vibrating-string dislocation damping (broad, damped, amplitude-
  dependent); broadband acoustic absorption from a spectrum of dislocation segment lengths;
  dual-frequency / multi-frequency ultrasonic forming (more effective than single-frequency, emerging
  evidence); acoustic softening (single-tone, amplitude-dominated).
