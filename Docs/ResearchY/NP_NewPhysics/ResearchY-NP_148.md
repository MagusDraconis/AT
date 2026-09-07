# ResearchY-NP_148 — Property Programming Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_148 (permanent)
**Title:** Property Programming Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_148.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_144 (defect spectrum fingerprint),
NP_146 (energy pathway), NP_147 (defect writing)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_148_Tests.cs`

---

## Purpose

NP_144/147 established that defect fingerprints can be read and defect populations written. NP_148
asks the *control* question: **can material properties be programmed through controlled defect
engineering?** Program: (1) define target properties (hardness, yield stress, fatigue life, fracture
toughness, damping); (2) determine which defect changes correlate with each property; (3) test whether
excitation protocols can move materials *toward* desired properties; (4) compare random vs directed
defect evolution; (5) determine whether a measure→excite→measure→excite feedback loop converges;
(6) estimate limits in metals / ceramics / quartz / granite. **Success criterion:** determine whether
resonance control can become true property programming, not simple softening. No new primitives;
canonical AT unchanged.

---

## 1. Target properties

| Property | Defect lever |
|---|---|
| **hardness** | dislocation density ↑ / grain refinement ↓ |
| **yield stress** | dislocation density (Hall–Petch via grain size; forest hardening) |
| **fatigue life** | compressive residual stress, crack closure, surface nanocrystallization |
| **fracture toughness** | grain refinement (usually ↑) vs dislocation pile-up (can ↓) |
| **damping** | dislocation mobility ↑ / grain-boundary sliding |

Each property maps onto a *specific* defect change, so writing a defect state programs a property.

---

## 2. Defect ↔ property correlations

| Defect change | Property move |
|---|---|
| dislocation density ↓ (annihilation) | hardness/yield ↓ (softening) |
| dislocation density ↑ (multiplication) + subgrain refinement | hardness/yield ↑ (hardening) |
| surface nanocrystallization + compressive residual stress | fatigue life ↑, hardness ↑ |
| crack closure/healing | fracture toughness ↑, fatigue life ↑ |
| grain refinement | strength ↑ (Hall–Petch), toughness usually ↑ |

The mapping is **not one-to-one** (e.g. grain refinement raises strength but can trade off ductility),
so "programming" is *correlated*, not exact.

---

## 3. Directed protocols move materials toward properties — established

Ultrasonic peening / UNSM / USRP are established, **parameter-controlled** processes that move a
material toward a *specified* target:

| Process | Target property | Evidence |
|---|---|---|
| ultrasonic shot peening | hardness, fatigue life | nanoscale grains, compressive stress |
| ultrasonic nanocrystal surface modification (UNSM) | fatigue life, hardness | up to −1400 MPa compressive stress; fatigue limit up to 2× |
| ultrasonic impact peening | residual stress, wear | controlled amplitude/load/coverage |

This is *directed* defect evolution, not random: the property change is set by process parameters
(amplitude, load, coverage, feed rate).

---

## 4. Random vs directed defect evolution

| Evolution | Outcome |
|---|---|
| **random** (uncontrolled excitation) | unpredictable mix of hardening/softening; net drift |
| **directed** (parameter-selected excitation) | reproducible property targets (peening maps parameter → property) |

Directed evolution is **established** for surface-hardening processes; it is a mature engineering
practice, not a speculative capability.

---

## 5. Does the measure → excite → measure → excite loop converge?

| Stage | Status |
|---|---|
| measure (read fingerprint, NP_144) | **SUPPORTED** (mechanical spectroscopy, residual-stress mapping) |
| excite (write defect state, NP_147) | **SUPPORTED** (peening/UNSM) |
| feedback loop (closed-loop convergence) | **PARTIAL / emerging** — real-time feedback tuning is an active research area, not yet standard |

The *open-loop* programming (read → excite → read → verify) works today; the *closed-loop* automatic
convergence is emerging, not yet proven general.

---

## 6. Limits per material

| Material | Programmability |
|---|---|
| **metals** (Al, steel) | **highest** — dislocation + grain + residual-stress levers all available |
| **ceramics** | low — brittle, little plasticity; surface damage risk |
| **quartz** | low — crack/contact only; no dislocation ductility |
| **granite** | low–moderate — contact/grain rearrangement, crack closure |

Property programming is **metal-centric**: the defect levers that drive hardness/yield/fatigue are
dislocation- and grain-based, which only metals (and some alloys) possess in usable form.

---

## Theorem

> **Theorem (NP_148).** Resonance control can become TRUE property programming, not simple softening —
> in metals. The target properties (hardness, yield stress, fatigue life, fracture toughness, damping)
> each map onto a specific defect change (dislocation density, grain refinement, residual stress, crack
> closure), and established parameter-controlled ultrasonic processes (shot peening, UNSM, USRP) move a
> material toward a *specified* property — directed, not random, defect evolution. The measure→excite
> loop works open-loop today (read the fingerprint NP_144, write the defect state NP_147, verify); the
> closed-loop automatic convergence is emerging (PARTIAL). Programming is metal-centric: ceramics,
> quartz, and granite lack the dislocation/grain levers and are only weakly programmable. Classification:
> property programming SUPPORTED (open-loop, metals); closed-loop feedback PARTIAL; "random-only defect
> evolution" CONTRADICTED. Proof: (1) Targets (Section 1). (2) Correlate (Section 2). (3) Directed
> (Section 3). (4) Random vs directed (Section 4). (5) Feedback (Section 5). (6) Limits (Section 6).
> **Success criterion: property programming — SUPPORTED.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Targets. (2) Correlate. (3) Directed. (4) Random vs directed. (5) Feedback. (6) Limits. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "resonance only softens" | peening/UNSM program hardness and fatigue life UP, controllably |
| "defect evolution is random" | parameter-controlled peening reproducibly targets properties |
| "programming is exact" | the defect↔property map is correlated, not one-to-one (trade-offs remain) |
| "programming works in all materials" | it is metal-centric (dislocation/grain levers only) |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| property programming (SUPPORTED) | a material where no parameter-controlled excitation moves any target property reproducibly |
| directed beats random | a process where random excitation matches directed peening on a target property |
| metal-centric | a brittle material (ceramic/quartz) whose hardness/yield is programmed by ultrasound |

---

## 9. Classification

| Component | Status |
|---|---|
| defect↔property correlation | **SUPPORTED** (NP_147 → properties) |
| directed defect evolution (parameter-controlled) | **SUPPORTED** (peening/UNSM) |
| open-loop programming (read → write → verify) | **SUPPORTED** |
| closed-loop convergence (measure→excite→measure→excite) | **PARTIAL** (emerging) |
| "random-only defect evolution" | **CONTRADICTED** |
| overall | **SUPPORTED** |

**Conclusion.** Resonance control **can become true property programming** (SUPPORTED), especially in
metals. The defect levers that drive hardness, yield stress, fatigue life, fracture toughness, and
damping are known (NP_147), and established parameter-controlled ultrasonic processes already move
materials *toward* specified properties — directed, not random. The measure→excite loop works open-loop
today; automatic closed-loop convergence is emerging. Programming is metal-centric: ceramics, quartz,
and granite are only weakly programmable because they lack dislocation/grain levers. This upgrades the
whole NP_129/130 "writable resonance score" from a metaphor to a *directed* engineering capability. No
new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_148_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_148_Targets` | five properties each map to a defect lever | ✅ |
| `Y_NP_148_Correlate` | defect ↔ property correlation (not one-to-one) | ✅ |
| `Y_NP_148_Directed` | peening/UNSM move toward specified properties | ✅ |
| `Y_NP_148_RandomVsDirected` | directed > random | ✅ |
| `Y_NP_148_Feedback` | open-loop SUPPORTED; closed-loop PARTIAL | ✅ |
| `Y_NP_148_Materials` | metals highest; brittle low | ✅ |
| `Y_NP_148_Classification` | overall SUPPORTED (metal-centric programming) | ✅ |
| `Y_NP_148_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_148"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent matter
  control), NP_144 (defect spectrum fingerprint), NP_146 (energy pathway), NP_147 (defect writing).
- Real-world record: ultrasonic shot peening (nanoscale grains, compressive stress, fatigue life);
  ultrasonic nanocrystal surface modification (UNSM, up to −1400 MPa, fatigue limit up to 2×);
  ultrasonic impact peening (controlled amplitude/load/coverage); closed-loop property tuning (emerging,
  real-time feedback in adaptive peening).
