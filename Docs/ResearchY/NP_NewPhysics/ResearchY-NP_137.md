# ResearchY-NP_137 — Real-World Evidence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_137 (permanent)
**Title:** Real-World Evidence Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_137.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_131 (critical resonance), NP_132
(reversible softening), NP_133 (critical mode frequency), NP_134 (critical mode discovery), NP_135
(experimental audit), NP_136 (variable rigidity bounds)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_137_Tests.cs`

---

## Purpose

NP_131–136 built the theory chain: a small set of critical (load-bearing) modes controls rigidity,
lives in the audio–ultrasound band, and can be softened reversibly by a frequency-matched coherent
drive. NP_137 asks the evidence question that NP_135 prepared but did not answer: **does the existing
laboratory record already contain evidence for coherent critical-mode softening?** Program:
(1) inventory the known phenomena (resonant ultrasound spectroscopy, acoustic softening, nonlinear
elasticity, phononic crystals, ultrasonic welding, acoustic fluidization, dynamic modulus reduction);
(2) for each, record measured rigidity change, frequency dependence, reversibility, and thermal
contribution; (3) compare each against the NP_131 critical-mode model and the NP_132
reversible-softening model; (4) grade each as A (observed but unexplained), B (partially consistent),
or C (inconsistent); (5) extract realistic upper bounds ΔE/E and ΔG/G; (6) name the strongest
supporting experiment and the strongest contradiction. **Success criterion:** determine whether
existing data SUPPORT, PARTIALLY support, or CONTRADICT coherent critical-mode softening. No new
primitives; canonical AT unchanged.

---

## 1. Inventory of known phenomena

| # | Phenomenon | What it is | Band |
|---|---|---|---|
| 1 | **Resonant ultrasound spectroscopy (RUS)** | measures a sample's discrete eigenfrequencies; inverts them to elastic moduli | kHz–MHz |
| 2 | **Acoustic softening (acoustoplastic effect)** | ultrasound lowers the flow/yield stress during deformation | 20 kHz–1 MHz |
| 3 | **Nonlinear (mesoscopic) elasticity** | elastic modulus decreases with strain amplitude in rocks/granular media | Hz–kHz |
| 4 | **Phononic crystals** | engineered periodicity opens band gaps that block/guide acoustic waves | kHz–GHz |
| 5 | **Ultrasonic welding** | high-amplitude vibration softens/joins a material at the interface | 20–40 kHz |
| 6 | **Acoustic fluidization** | strong vibration reduces friction/yield strength of granular/rock debris | Hz–kHz |
| 7 | **Dynamic modulus reduction (DMA/viscoelastic)** | storage modulus G′ falls with temperature or strain rate | sub-Hz–kHz |

These seven span the whole claim surface: measurement (1), elastic-rigidity change (3), plastic/yield
change (2, 6), engineered wave control (4), thermal joining (5), and rate/temperature softening (7).

---

## 2. Measured rigidity change / frequency dependence / reversibility / thermal contribution

| # | Phenomenon | Measured rigidity change | Frequency dependence | Reversibility | Thermal contribution |
|---|---|---|---|---|---|
| 1 | RUS | **none** (low-amplitude probe; maps f_i → E, G) | resonant (resolves each mode) | N/A | negligible |
| 2 | Acoustic softening | **flow stress ↓ ~50–90%** in single crystals (Blaha–Langenecker); tens of % in alloys | **yes** — ultrasonic, resonant to dislocation dynamics | **yes while drive is on** (re-hardens on removal); small residual | **athermal excess dominates** at low amplitude |
| 3 | Nonlinear elasticity | **elastic modulus ↓ ~1–30%** at strain ~10⁻⁶–10⁻⁴ | weak (amplitude-dependent) | **yes but slow** (slow dynamics, min–hr) | none (sub-damage, room-T) |
| 4 | Phononic crystals | **none** (band-gap wave control, not softening) | engineered bands | N/A | none |
| 5 | Ultrasonic welding | localized soften→flow→bond | 20–40 kHz | **no** (permanent joint) | **dominant** (frictional heating) |
| 6 | Acoustic fluidization | **friction/yield ↓ factor ~5–10** | yes (amplitude/frequency) | **transient** (re-locks when shaking stops) | secondary (vibration heating) |
| 7 | Dynamic modulus reduction | G′ ↓ with T / strain rate | rate/temperature | partial | **dominant** (thermal) |

---

## 3. Comparison against NP_131 (critical modes) and NP_132 (reversible softening)

NP_131/132 make three *jointly testable* predictions:

1. **Rigidity is carried by a small set of critical modes** (m ≪ N, percolation backbone).
2. **A frequency-matched drive reduces rigidity** at the structure's own resonance (NP_133: kHz–MHz).
3. **The softening is reversible, order-preserving (S ≈ 1), and non-thermal** — a gel-like unjamming,
   not melting.

The evidence maps onto the predictions as follows:

| Prediction | Observed? | Where |
|---|---|---|
| small set dominates rigidity | **partial** | percolation/backbone rigidity is real (granular contact networks, jammed packings), but a *discrete m = 6 "master key"* with quantized steps is not isolated in the literature |
| frequency-matched drive reduces rigidity/strength | **yes, qualitatively** | acoustic softening (2), acoustic fluidization (6), nonlinear elasticity (3) — all are frequency/amplitude-selective acoustic effects |
| reversible, order-preserving, non-thermal | **partial** | athermal acoustic softening reverts on removal (2); NME modulus recovery is slow-reversible (3); acoustic fluidization re-locks (6) — but none demonstrate a *full R → 0 gel with order survival* |

**Verdict: the three predictions are matched qualitatively, not quantitatively.** The record contains
frequency-matched acoustic reduction of strength/rigidity that is (partly) athermal and reversible —
the *signature* NP_131/132 predict — but it does not contain the specific quantized backbone (m = 6)
or the full order-preserving gel transition.

---

## 4. Evidence grade per phenomenon (A / B / C)

| # | Phenomenon | Grade | Reasoning |
|---|---|---|---|
| 1 | Resonant ultrasound spectroscopy | **—** (tool) | provides the *spectrum premise* (NP_130/134) but measures, not softens |
| 2 | Acoustic softening (acoustoplastic) | **B** | **strongest support** — frequency-matched, athermal, reversible strength reduction; but it is *plastic flow stress*, not elastic rigidity |
| 3 | Nonlinear (mesoscopic) elasticity | **B** | elastic-modulus reduction, amplitude-dependent, slow-reversible — but continuous, not quantized |
| 4 | Phononic crystals | **—** (adjacent) | confirms geometry controls acoustic response; says nothing about rigidity softening |
| 5 | Ultrasonic welding | **C** | softening is thermal/frictional — supports the NP_135 *thermal control*, not a coherent excess |
| 6 | Acoustic fluidization | **B** | transient vibration-driven strength/friction loss (unjamming) in granular/rock (m = 20–40) |
| 7 | Dynamic modulus reduction (DMA) | **C** | mostly thermal/rate softening, not a coherent resonant effect |

No phenomenon is grade **A** in its current reading: the historical "unexplained" component (the
athermal excess of acoustic softening) has since been attributed to preferential energy absorption at
dislocations — which is itself a *mode/defect-selective* mechanism, i.e. it moved from A to B.

---

## 5. Realistic upper bounds (ΔE/E, ΔG/G)

The two moduli scale together (NP_136: R = E/E₀ = G/G₀), so one bound serves both. The literature
separates **elastic (rigidity)** change from **plastic (flow-stress)** change:

| Quantity | Observed reversible upper bound | Source |
|---|---|---|
| **ΔE/E, ΔG/G** (elastic rigidity) | **~0.01–0.30** (1–30%) | nonlinear mesoscopic elasticity / dynamic acoustoelasticity at high sub-damage strain |
| **Δσ/σ** (plastic flow stress) | **~0.5–0.9** (50–90%) transient | acoustic softening / acoustoplastic effect |

**This is the decisive quantitative gap.** NP_136 predicts *elastic* rigidity drops of 33–100% in
discrete steps. The real record shows *elastic*-modulus reduction capped near ~30% (continuous, slow),
while the large 50–90% effects are *plastic yield* reductions — a dislocation-mediated flow process,
not a reversible backbone unlock. So the observed upper bound on the quantity AT actually predicts
(reversible elastic rigidity) is **ΔE/E ≈ ΔG/G ≈ 0.3**, an order below the predicted 0.33–1.0 steps.

---

## 6. Strongest supporting experiment and strongest contradiction

**Strongest support — Langenecker's acoustoplastic experiments (1955–1966).** Ultrasound on zinc
single crystals cut the stress required for deformation by large factors with little temperature rise;
the material re-hardened when the ultrasound stopped, and the softening could *not* be reproduced by
raising the average temperature. This is precisely NP_131/132's signature — a frequency-matched
acoustic drive that reduces mechanical resistance *athermally and reversibly* — observed for real,
decades before the AT reading. The caveat: the reduced quantity is the *flow stress* (plastic), not
the *elastic modulus* (rigidity).

**Strongest contradiction — the missing quantized elastic drop and the missing R → 0 gel.** No
experiment shows an ordered solid's *elastic* modulus fall by a discrete ~33% step (m = 6) under a
coherent resonant drive, nor a reversible, order-preserving transition to R → 0 (gel) that fully
re-locks. Every large observed softening is either (a) plastic flow (acoustic softening), (b)
continuous and sub-damage (NME ≤ ~30%), or (c) thermal (welding, DMA). The AT-predicted "master-key"
magnitude and the full unjamming transition are, as of the current record, **UNTESTED** in the elastic
channel.

---

## Theorem

> **Theorem (NP_137).** Existing laboratory data PARTIALLY support coherent critical-mode softening:
> the record contains frequency-matched, partly athermal, reversible acoustic reduction of strength
> and rigidity — the NP_131/132 signature — but does not yet contain the specific quantized backbone
> or the full order-preserving gel transition. Three of the seven inventoried phenomena (acoustic
> softening, nonlinear mesoscopic elasticity, acoustic fluidization) are grade B (partially
> consistent); ultrasonic welding and dynamic-modulus reduction are grade C (thermal); RUS and
> phononic crystals are neutral tools. The strongest support is Langenecker's acoustoplastic effect
> (ultrasound lowers flow stress athermally and reversibly); the strongest contradiction is the
> absence of a quantized elastic-modulus drop and of a reversible R → 0 gel. The observed reversible
> upper bound on *elastic* rigidity is ΔE/E ≈ ΔG/G ≈ 0.3, while the large 0.5–0.9 effects are plastic
> flow stress, not rigidity — so the quantitative claim of NP_136 (33–100% elastic steps) is not yet
> corroborated by elastic-channel data. Proof: (1) Inventory (Section 1). (2) Measure (Section 2).
> (3) Compare (Section 3). (4) Grade (Section 4). (5) Bound (Section 5, verified — elastic ≤ 0.30,
> plastic ≤ 0.90). (6) Strongest support/contradiction (Section 6). **Success criterion: PARTIAL
> support.** Classification: the *signature* (frequency-matched, athermal, reversible acoustic
> softening) PARTIAL-CONSISTENT; the *magnitude* (quantized elastic drops) UNTESTED; "coherent
> softening ≡ heating" CONTRADICTED (athermal excess exists); "full elastic gel already observed"
> REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Measure. (3) Compare. (4) Grade. (5) Bound. (6) Locate. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "acoustic softening is pure heating" | Langenecker: the softening exceeds the measured thermal effect and reverts off-ultrasound |
| "ultrasonic welding proves coherent softening" | welding is frictional/thermal — the NP_135 thermal control, not a coherent excess |
| "phononic crystals prove rigidity control" | band gaps control wave *propagation*, not load-bearing rigidity |
| "elastic modulus drops by 90% under ultrasound" | the 50–90% effects are *flow stress* (plastic); reversible *elastic* drops cap near ~30% |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| existing data partially support the model | a survey finding *no* frequency-matched athermal reversible softening in any literature |
| elastic upper bound ~0.3 | a reproducible, reversible elastic ΔE/E > 0.3 (not plastic) under a resonant drive |
| full gel is UNTESTED | an existing experiment showing an ordered solid at R → 0 with order survival |

---

## 9. Classification

| Component | Status |
|---|---|
| frequency-matched, athermal, reversible acoustic softening (the signature) | **PARTIAL-CONSISTENT (B)** |
| quantized elastic-magnitude (33–100% steps, NP_136) | **UNTESTED** (no elastic-channel corroboration) |
| coherent softening ≡ heating | **CONTRADICTED** (athermal excess observed) |
| full elastic gel already observed | **REFUTED** (not in the record) |

**Conclusion.** The overall verdict is **PARTIAL**: real-world experiments already exhibit the
qualitative signature of coherent critical-mode softening — a frequency-matched acoustic drive that
reduces strength/rigidity athermally and reversibly — but they do not yet exhibit the quantitative
master-key claims (discrete m = 6 steps, 33–100% elastic drops, a reversible order-preserving gel).
The decisive experiment remains NP_135's matched-power off-resonance vs critical-mode test, but it
must measure the **elastic modulus** (frequency shift of f₀), not the flow stress, to close the gap
between the plastic softening that is already observed and the elastic rigidity that AT predicts.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_137_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_137_Inventory` | seven phenomena present | ✅ |
| `Y_NP_137_PhenomenonAssessment` | rigidity/frequency/reversibility/thermal flags | ✅ |
| `Y_NP_137_CompareModels` | predictions vs evidence; grades A/B/C | ✅ |
| `Y_NP_137_Bounds` | elastic ΔE/E,ΔG/G ≤ 0.30; plastic ≤ 0.90 | ✅ |
| `Y_NP_137_Strongest` | support = acoustoplastic; contradiction = no quantized elastic drop | ✅ |
| `Y_NP_137_Classification` | overall PARTIAL; UNTESTED/CONTRADICTED/REFUTED sub-statuses | ✅ |
| `Y_NP_137_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_137"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent matter
  control), NP_131 (critical resonance), NP_132 (reversible softening), NP_133 (critical mode
  frequency), NP_134 (critical mode discovery), NP_135 (experimental audit), NP_136 (variable rigidity
  bounds).
- Real-world record: Blaha & Langenecker (1955) / Langenecker (1966) — acoustoplastic effect;
  Melosh (1979, 1996) and later granular fault-gouge studies — acoustic fluidization; nonlinear
  mesoscopic elasticity (Guyer–Johnson / slow-dynamics literature); resonant ultrasound spectroscopy
  (migler/Leisure et al.); ultrasonic welding (20–40 kHz); phononic-crystal band-gap engineering.
