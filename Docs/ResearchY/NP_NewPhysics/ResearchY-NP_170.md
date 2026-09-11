# ResearchY-NP_170 — Lock-Lattice Phase Memory (Physical Realizability Test)

**Program:** ResearchY — Wave Geometry Program
**Subject theory:** The Actualization Theory (AT) — a reconstruction of physics from Difference,
Actualization and Spectrum on the 96-site circulant attractor, from two primitives (Difference and
the tensor reference η), classified DERIVED / EMERGENT / BOUNDARY in the canonical monograph v2.0
(Ch1–Ch14). Throughout this document, "AT" denotes that framework only; AT-1xx and QGxxx denote its
internal phase and audit labels.
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_170 (permanent)
**Title:** Lock-Lattice Phase Memory — physical realizability test proposal (LLPM) for the lock law
of The Actualization Theory (AT)
**Status:** PLANNED — proposal only (no experiment performed, no result claimed)
**Date:** 2026-09-11
**File:** `NP_NewPhysics/ResearchY-NP_170.md`
**Depends on:** ResearchY-T_001 (inverse spectral design), ResearchY-D_002/D_009 (mode structure,
ω₁ = 0.6216), ResearchY-D_015…D_020 (N = 96), ResearchY-NP_005 (locking force ABSENT — BOUNDARY),
ResearchY-NP_007/NP_011 (static coupling network REFUTED as physical field), ResearchY-M_002
(phase pinning), ResearchY-M_009 (AT-P042 tick discriminator), AT-QG QG120 (Q = β₀ topological
charge, one-way barrier), QG126 (collective charge wave, cos(Δφ/2)), QG307/312/313/314/316/319
(operator/lock universality, lock values domain-specific, organization phase transition, false-positive
audit, g* ≈ 0.31 on synthetic cohorts), Canonical monograph Ch7 (Operator Basis) / Ch8 (Lock Law)
/ Ch14 (Organization and Prediction)
**Test suite:** PLANNED — `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_170_Tests.cs` (numerical precursor
only; not part of this proposal)

---

## 0. Purpose and scope

This document is a **proposal**, not a result. It specifies one experiment — and only the part of
the idea that is worth doing — together with the exact procedure, metrics, pre-registration rules,
and kill criteria needed to make the outcome informative.

Scope rules honoured here:

- No canonical AT claim is changed, reclassified, or strengthened by this document.
- No new primitive is introduced. Both imported ingredients (a locking nonlinearity and the
  engineered coupling network) are declared as **BOUNDARY** inputs, consistent with NP_005
  (locking force ABSENT) and NP_007/NP_011 (the static coupling network is REFUTED as a physical
  field).
- The experiment cannot *confirm* AT. It can only fail to falsify it, or falsify the physical
  realizability of the lock structure. The negative outcome is a first-class result.

---

## 1. The one idea worth doing

**Not** an energy store. The density argument is fatal and is accepted here: a 96-node lattice holds
µJ-scale energy at kW-scale overhead if cryogenic, i.e. 10⁶–10⁹× behind Li-ion (0.9 MJ kg⁻¹,
~5000 cycles, ~€100 kWh⁻¹) on any storage figure of merit. That framing is closed.

**The idea:** the lock lattice is a **phase-memory element** — a device whose state variable is
*which modes are locked* rather than *how much charge is separated* (capacitor) or *how much
chemical potential is stored* (battery). Its retention hypothesis comes from the AT-120 one-way
barrier (a topological charge, once actualized, cannot un-actualize), and its read-out comes from
the AT-126 phase-interference law. Call it **LLPM — Lock-Lattice Phase Memory**.

Stated as one sentence: *an engineered circulant C₉₆(1..6) oscillator ring is charged into a locked
mode configuration; the locked configuration is predicted to retain its state longer than a matched
unlocked control, with the retention difference produced by the actualization barrier and readable
through Δθ interference.*

The experiment is designed so that the answer is informative whether it is positive or negative,
because AT's own Ch14 already concedes that "standard complexity measures match or beat the lock rule
on the evolving cohort" — a *statistical* audit cannot settle the question. Only a physical phase
transition can.

---

## 2. The three reasons this is worth doing

Ranked by expected value. Each has its own metric; none of them is energy density.

### Reason 1 — Theory falsification (strongest)

Every existing lock-law result (QG307–QG319) is computed on *datasets* or *synthetic cohorts*.
A hardware ring is the only place where the Ch8 lock law must produce a **dynamical phase
transition** in a physical system rather than a statistic in a table.

| Item | Value |
|---|---|
| Metric that matters | existence of a threshold in the *physical* coupling, with sharpness ≥ 3 and width ≤ 0.4 (QG316 criticality criteria) |
| Bar to matter | hysteresis area A > 0 at ≥ 5σ over ≥ 30 runs, absent in all three controls |
| If it passes | the lock structure is physically realizable — the lock law stops being a candidate descriptive statistic |
| If it fails | the lock law is downgraded to a descriptive statistic (consistent with QG312 partial-fake and QG319 FP = 96.9% / FN = 66.1%), and the storage claim is withdrawn permanently |

This is the highest-value outcome axis because *both* branches are publication-grade information,
and the cost is one bench prototype.

### Reason 2 — Phase / coherence memory

Where density is irrelevant and the metric is energy per operation: write energy, retention time,
read-out fidelity, and device-to-device variability.

| Item | Value |
|---|---|
| Metric that matters | energy per write (fJ), retention time (s), read-out visibility, variability σ/µ |
| Bar to matter | < ~10 fJ/write, retention comparable to a supercapacitor's self-discharge window, variability < 5% (competitive with MRAM/PCM/FeRAM) |
| Why it is plausible | the state variable is *phase*, not charge — writing is phase-pinning (M_002), not charge transfer, so the write energy floor is set by phase-noise, not by Coulomb energy |
| Why it may fail | decoherence and thermal phase diffusion erase phase memory; a room-temperature ring may retain nothing beyond 1/ω₀ × Q |

### Reason 3 — Neuromorphic threshold element

The lock transition is a sigmoidal threshold with hysteresis — an artificial synapse/neuron
primitive. What is AT-specific here is that the threshold position and sharpness are *predicted*
by the operator/lock structure rather than fitted — which is exactly what memristor and
phase-change devices cannot offer.

| Item | Value |
|---|---|
| Metric that matters | energy per switching event, threshold sharpness, device-to-device uniformity |
| Bar to matter | must beat PCM/memristor variability and switching energy at equal node count |
| Why it is plausible | QG316 supplies pre-registered sharpness/width criteria; uniformity is set by lithography, not by material stochasticity |
| Why it may fail | the nonlinearity needed for locking is imported (NP_005) and may dominate variability |

**Explicitly not a reason:** energy storage. See §1.

---

## 3. Canonical basis — the exact numbers the hardware must realize

The observable attractor is the circulant **C₉₆(1..6)** (degree 12), with Laplacian spectrum

```
λ_k = 2 · Σ_{d=1..6} ( 1 − cos( 2π d k / 96 ) )        k = 0 … 95
```

Independently recomputed and verified for this document:

| Quantity | Verified value | Canonical value | Check |
|---|---|---|---|
| λ₂ (first positive eigenvalue, k = 1) | 0.386351 | 0.3864 | ✅ |
| ω₁ = √λ₂ | 0.621571 | 0.6216 | ✅ |
| ω_max = √λ_max (k = 48, λ = 12) | 3.979620 | — | — |
| span = ω_max/ω₁ | 6.402515 | 6.4025 | ✅ |
| octave bands over the 95 positive modes | [4, 4, 87] | [4, 4, 87] | ✅ |
| zero modes | 1 | 1 | ✅ |
| mirror pairs λ_k = λ_{96−k} | 47 pairs + self-conjugate k = 48 | 47 pairs | ✅ |
| multiplicity multiset | 1 + 42 doublets + 5 + 6 = 95 | [42×2, 5, 6] | ✅ |
| distinct eigenvalues | 45 | — | — |
| lock chain (canonical, D96-specific) | occMom/Σm = 20.0026 = 2.4105 × 8.2980 | 20.0026 | Ch8 |

Two canonical facts govern what the hardware may claim:

1. **QG313 — lock structure is universal, lock values are domain-specific.** Therefore the
   hardware's own lock values must be *derived from its own measured spectrum*; the D96 values
   (20.0026, 2.4105, 8.2980) must **not** be asserted as hardware predictions.
2. **QG316 — g\* ≈ 0.31 is a synthetic-cohort parameter** (a power-law exponent fraction over 40
   steps of an organization ramp, disclosed as synthetic by MONO007 A08). It is **not** a physical
   coupling and is **not** transferable. The hardware prediction is *existence plus criticality
   criteria*, not the number 0.31.

Design recipe: the inverse-spectral construction that turns a target spectrum into coupling weights
is already an accepted ResearchY result (**T_001**, inverse DFT of the eigenvalue list onto the first
circulant row). No new mathematics is needed to specify the hardware.

---

## 4. Hypotheses and pass/fail

| # | Hypothesis | AT basis | Pass | Fail | Interpretation of fail |
|---|---|---|---|---|---|
| **H0** | the ring realizes C₉₆(1..6) | Ch6/D_015 | mirror-pair error max_k \|ω_k − ω_{96−k}\|/ω₁ < 1%; octave bands [4,4,87] | pairing or bands absent | **validity failure, not a theory failure** — fix the apparatus and rerun |
| **H1** | a lock threshold exists in the *physical* coupling | Ch8 c08:thm:phase-transition + QG316 criteria | sharpness ≥ 3 **and** width ≤ 0.4 in f(g), with hysteresis loop A > 0 at ≥ 5σ | f(g) smooth and reversible | lock law demoted to descriptive statistic |
| **H2** | locked configurations retain state longer than controls | AT-120 one-way barrier (**analogy** — see §9) | two-time-scale decay, R = τ_slow/τ_fast ≥ 10, and single-exponential in all three controls | same decay law as controls | the actualization-barrier analogy does **not** transfer to a Laplacian ring; retention claim withdrawn |
| **H3** | phase read-out follows the interference law | AT-126 / QG126 | \|Θ(mid)\| ∝ cos(Δφ/2), visibility V ≥ 0.90 | V < 0.90 or wrong functional form | the collective-wave read-out is not realizable as a hardware observable |
| **H4** | threshold element is competitive | Reason 3 | switching energy and variability beat PCM/memristor benchmarks | worse on both | reason 3 retired; reasons 1–2 unaffected |

H2 is deliberately flagged as the **highest-risk, highest-information** hypothesis: it is an analogy
across two different AT systems (the R-field condensate of AT-120 and the circulant Laplacian ring
of Ch6–Ch8). Testing an analogy is legitimate; asserting it as derived would not be.

---

## 5. The apparatus — exactly what to build

### 5.1 Tier 1 (primary; bench electronic, days, low cost)

A 96-node ring of nonlinear oscillators with programmable circulant coupling.

| Element | Specification |
|---|---|
| Nodes | 96 × active resonator (LC tank + saturating amplifier) at f₀ = 100 kHz – 1 MHz |
| Nonlinearity | tanh-type saturation (Adler/Kuramoto-type phase dynamics) — **imported, BOUNDARY (NP_005)** |
| Coupling | for each node i and each offset d ∈ {1..6}: links to i ± d, each through a voltage-controlled transconductance g_m,d; 12 links per node |
| Coupling programmability | one DAC per offset (6 values) sets w_d; the Laplacian is then circulant with eigenvalues λ_k = 2 Σ_d w_d (1 − cos 2πdk/96) — the SAME family as D96, weights set by T_001's inverse-DFT recipe |
| g sweep | g ≡ Σ_d w_d normalized to Σ_d w_d,max; scan g ∈ [0, 1] in 40 steps (mirrors the QG316 ramp) |
| Control lattices | (a) detuned ring (same Q, ω_k shifted out of ratio); (b) random-coupling ring (same Q and span, w_d randomized per link); (c) linear-ramp spectrum (must fail CROWDING, per QG305/QG308) |
| Phase read-out | 96-channel multiplexed IQ demodulation (lock-in) at f₀; ≥ 1 MS/s per channel; θ_j and amplitude A_j per node |
| Injector | single-channel drive at selectable ω_k with programmable phase θ_in; drive removed abruptly for the retention run |
| Instruments | 2–4 × 16-channel ADC/DAC (e.g. 24-bit audio-band) + FPGA for real-time IQ; total bench budget in the €5–20 k class |

Design constraints to respect:

- **Node-to-node spread** must be < 1% in f₀ (measure and trim, or calibrate numerically per node)
  — otherwise the H0 mirror-pairing test fails for trivial reasons.
- **Phase noise** sets the retention floor: the locked/unlocked contrast is only measurable while
  1/(Q ω₀) ≪ τ_slow.
- **Nonlinearity strength** is the analogue of QG316's organization parameter. Pre-register the
  mapping *before* data-taking; report the raw control voltage alongside the normalized g.

### 5.2 Tier 2 (photonic chip, months)

96 coupled SiN microring resonators; measure the transmitted spectrum directly (this gives ω_k and
thus H0 immediately); locking via Kerr nonlinearity; read-out by phase-sensitive heterodyne
detection. Costlier, but a far better test of H1 and a far worse test of H2 (loss).

### 5.3 Tier 3 (superconducting, ~1 year)

96 coupled superconducting cavities/transmons, T < 20 mK, Q ≈ 10⁶. The **only** tier where a
long retention claim is meaningful. Justified *only* if Tier 1 yields A > 0 at ≥ 5σ **and**
R ≥ 10 **and** a device-level figure of merit that beats a supercapacitor. Otherwise it cannot be
justified by µJ-scale stored energy.

---

## 6. The protocol — step by step

All runs: ≥ 30 independent repetitions per configuration, seeded/instrument-time-stamped, with the
analysis script frozen before the first run.

**R0 — Calibration.** Measure each node's free-running ω and Q. Trim/calibrate to < 1% spread.
Record node-to-node spread as a systematic.

**R1 — Validity (H0).** Set w_d to the C₉₆(1..6) values (T_001 recipe). Measure ω_k of the ring by
ring-down spectroscopy / spectral scan. Compute mirror-pair error and octave band occupancy
(expect [4,4,87] over the 95 positive modes). **Gate:** if this fails, stop; the apparatus is not a
D96 lattice and nothing downstream is interpretable.

**R2 — Charging and read-out (H3).** Drive at ω_k for a settling time, then probe two adjacent sites
with a controlled relative phase Δφ; measure \|Θ(mid)\|/(\|Θ(x₁)\|+\|Θ(x₂)\|) versus Δφ. Fit
cos(Δφ/2). **Gate:** V ≥ 0.90.

**R3 — Lock threshold and hysteresis (H1).** Sweep g upward through 40 steps, then downward.
At each step, after settling, measure the per-node instantaneous phase difference to the reference
and compute

```
f(g)  =  (fraction of nodes with |dΔθ_j/dt| < ε) ,  ε = 0.01 ω₁
A     =  ∫ | f_up(g) − f_down(g) | dg        (normalized)
sharp =  max|Δf| / mean|Δf|   ;   width =  (g₁₀ → g₉₀ interval)
```

Predictions to test (criteria taken verbatim from QG316): sharp ≥ 3, width ≤ 0.4, A > 0 at ≥ 5σ.
**Report the measured critical coupling g_c as a hardware-specific value** (QG313) — do **not**
compare it to 0.31.

**R4 — Retention (H2).** Two protocols:

1. *Ring-down:* charge into a locked configuration, remove the drive abruptly, record total stored
   energy in the ring (sum over nodes of A_j²/2) versus t for ≥ 10⁴/ω₁.
2. *Cycle test:* N ≥ 10³ charge/hold/discharge cycles; record state fidelity after hold.

Analysis:

```
locked  :  E(t) = a·exp(−t/τ_fast) + b·exp(−t/τ_slow)
control :  E(t) = a·exp(−t/τ_c)                     (single exponential)
retention R = τ_slow / τ_fast          storage FOM = R · E_ret/E_in
```

**Pass:** R ≥ 10 with a two-exponential fit preferred by BIC/AIC against the single-exponential
null, and all three controls single-exponential. **Fail:** indistinguishable decay laws.

**R5 — Threshold-element benchmark (H4, optional, only if R3 passes).** Measure switching energy
per node, threshold sharpness, and device-to-device variability across 5 identical boards; compare
with published PCM/memristor figures.

**R6 — Controls and blinding (mandatory, all runs).** Labels for {locked lattice, control a, b, c}
are hidden from the analyst until the analysis script has produced numbers (QG319: FP = 96.9%,
FN = 66.1% — a lock classifier applied to a small spectrum is exactly the failure mode to guard).
The lock classifier itself (how f(g) and "locked" are computed) is fixed before R3.

---

## 7. Statistics, blinding and reproducibility

| Rule | Setting |
|---|---|
| Repeats | ≥ 30 per configuration; report mean ± 95% CI and effect size, never a single trace |
| Significance | A > 0 and R ≥ 10 both at ≥ 5σ |
| Model comparison | BIC and AIC for single- vs two-time-scale decay; the null is always the *simpler* model |
| Blinding | label blinding under a third-party (or scripted) unblinding step |
| Freezing | analysis script hash recorded before R3; any change restarts the run |
| Negative controls | detuned, random-coupling, linear-ramp (must fail H1 and H2) |
| Systematic accounting | node-frequency spread, phase noise, ADC clock jitter, temperature drift, cable delay (as an explicit ε budget) |
| Reproducibility | raw IQ traces archived with instrument settings and seeds |

---

## 8. Kill criteria and decision tree

Decide in advance, so the answer is not negotiated afterwards:

```
R1 fails                 → apparatus problem → repair and rerun (do NOT report as theory result)
R1 passes, R3 null       → H1 falsified → withdraw the lock-storage claim permanently;
                           record the lock law as a descriptive statistic (consistent with QG312/QG319)
R3 passes, R4 null       → the threshold is real, retention is not → keep the
                           neuromorphic/threshold device direction (reason 3), drop phase memory (reason 2)
R3+R4 pass, FOM < 10× supercap → stop; do not escalate to Tier 2/3 on storage grounds
R3+R4 pass, FOM ≥ 10× supercap → escalate to Tier 2, then Tier 3 with a device-level paper
```

Explicit non-escalation rule: cryogenics (Tier 3) is never justified by the energy stored.

---

## 9. Boundaries — what is imported, and what must not be claimed

| Imported | Status | Consequence |
|---|---|---|
| The locking nonlinearity | **BOUNDARY** (NP_005: locking force ABSENT) | the experiment tests the lock *structure*, not a derived interaction |
| The engineered coupling network | **BOUNDARY** (NP_007/NP_011: static coupling network REFUTED as a physical field) | the lattice is built, never claimed emergent |
| The retention mechanism (AT-120 barrier) | **analogy across systems** | H2 is a hypothesis about transferability, not a derived prediction |
| g\* = 0.31 | **synthetic-cohort parameter** (QG316, MONO007 A08) | a hardware g_c is expected to differ; reporting 0.31 as a hardware number would be a fit |
| D96 lock values 20.0026 / 2.4105 / 8.2980 | **D96-specific** (QG313) | derive the hardware's own lock values from its own spectrum |
| Energy-storage claims | out of scope | density argument is accepted as fatal (§1) |

This document does **not** reclassify any value. The ResearchY classification-guard rule is respected:
no DERIVED / EMERGENT / BOUNDARY status of any existing result is altered here.

---

## 10. What each outcome means

| Outcome | Meaning for AT | Meaning for engineering |
|---|---|---|
| R1 only | the D96 lattice is realizable — a hardware platform exists | an oscillator-ring testbed with a derivable spectrum |
| R1 + R3 | lock structure is physically realizable, not merely statistical | a threshold device with predicted criticality criteria |
| R1 + R3 + R4 | phase memory with barrier-protected retention | a candidate phase/coherence memory element (reason 2) |
| R1 + R4 without R3 | two-time-scale decay without a lock threshold — suspects an ordinary high-Q effect | no device claim; the lock reading is unsupported |
| R1 only, R3 null | lock law demoted to descriptive statistic | the platform stays useful; the storage idea ends |

The **negative** branch is not a failure of the program: it is the honest closure of an open
question, and it is the branch AT's own Ch14 warns about ("standard complexity measures match or
beat the lock rule on the evolving cohort").

---

## 11. Deliverables and registration

1. **This proposal** (PLANNED).
2. **Numerical precursor (planned):** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_170_Tests.cs` — a
   deterministic Kuramoto/Adler ring simulation on C₉₆(1..6) (fixed seeds, no randomness) that
   pre-computes the expected sharpness/width curves and the two-time-scale decay signature, so R3/R4
   have an *a priori* prediction to compare against. Research-test rules apply (assumptions,
   intermediate calculations, conclusions, deterministic output).
3. **Publication version:** `Docs/ResearchY/Publication/ResearchY-NP_170-Lock-Lattice-Program.tex`
   → `.pdf` (this proposal, publication layout).
4. **Registry entry (ready, not applied):** the proposal corresponds to **AT-P043** in the canonical
   prediction registry (AT-P042 is the tick discriminator, M_009). The canonical publication registry
   is not modified by this document; the ready-to-paste block is:

```latex
\predstart{AT-P043}{PROPOSED}
\pfield{Title}{Lock-lattice physical realizability: a lock threshold in an engineered C96 ring.}
\pfield{Derived From}{Difference $\to$ Actualization $\to$ Spectrum (D96) $\to$ Locking operator
$\to$ lock law (Ch8); criticality criteria from QG316; design recipe from ResearchY-T_001.}
\pfield{Prediction}{An engineered circulant C96(1..6) nonlinear oscillator ring exhibits a lock
threshold in the physical coupling (sharpness $\ge 3$, width $\le 0.4$) with hysteresis, and a
locked configuration decays with two time scales against a single-exponential control. Structure
universal (QG313); the threshold value and lock values are hardware-specific and are NOT predicted.}
\pfield{Experimental Test}{Bench electronic 96-node ring, runs R1--R4 (mirror pairing, interference
visibility, hysteresis sweep, retention), $\ge 30$ repeats, blinded labels, three negative controls.}
\pfield{Current Evidence}{None — proposal. All existing lock-law evidence is statistical (datasets
and synthetic cohorts, QG307--QG319).}
\pfield{Future Validation Path}{Tier 1 bench prototype; escalate to photonic/superconducting only if
the device-level figure of merit justifies it.}
\pfield{Falsification Criterion}{No hysteresis (f(g) smooth and reversible), or locked retention
decay indistinguishable from all three controls.}
```

Coverage-record entry (for `ATQG_PhysicsCoverage`), if and when a result exists: domain
`experiment`, status `PROPOSED`, phase `NP_170`, key result = the measured branch of §10.

---

## 12. Conclusion

One idea survives the density objection: the lock lattice as a **phase-memory / threshold element**
whose distinguishing feature is a *predicted* criticality, tested on hardware for the first time.
Three reasons justify it — falsification of the lock law as a dynamical claim (strongest), phase
memory where density is irrelevant, and a neuromorphic threshold element with predicted sharpness.
The cost of the deciding experiment (Tier 1) is one bench prototype, and both branches of the
outcome are informative. Energy storage is explicitly abandoned; the nonlinearity and the coupling
network are explicitly imported; and the retention hypothesis is explicitly an analogy awaiting
test, not a derivation.

---

**Status:** PLANNED — proposal only. No result claimed. No canonical AT statement modified.
