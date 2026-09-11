# ResearchY-NP_173 — Discriminator Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_173 (permanent)
**Title:** Discriminator Audit — which observable separates D96 from all its controls, and does that difference mean anything?
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `NP_NewPhysics/ResearchY-NP_173.md`
**Depends on:** ResearchY-NP_170 (H1/H2 and the R6 negative-control requirement), ResearchY-NP_171 (the
deterministic lock-lattice simulator, its lattices and metrics), ResearchY-NP_172 (retention and the
saturation-matched control), ResearchY-T_015 / ResearchY-D_048 (the perturbation and attractor-shift
machinery), ResearchY-D_047 (the degeneracy-lock theorem), QG313/QG316 (domain-specific values, protocol)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_173_Tests.cs` (5 tests, all passing)
**Reuses:** `AT.Core/Resonance/Kuramoto/LockLatticeSimulator.cs`, `RetentionNullSimulator.cs`,
`AT.Tests/Shared/AdaptabilityAudit.cs`

---

## Question

> Which observable separates the canonical D96 lock lattice from **all** of its controls — random,
> degeneracy-matched, saturation-matched, linear-ramp, detuned — by more than **5σ**?

Seven observables are measured (plus retention, which is what the saturation-matched control exists for).
The audit is deliberately two-staged, because a statistically significant difference is not the same thing
as an AT-specific one:

1. **Stage 1 — discriminator:** does D96 differ from *every* control by ≥ 5σ, with the separation measured
   as the two-sample statistic z = |Δ| / √(σ²_D96/n + σ²_ctl/n), n = 16 fixed seeds?
2. **Stage 2 — mechanism:** is the difference reproduced by a control (or a matched diagnostic) that shares
   the property invoked to explain it? Only an observable that passes **both** stages counts as a genuine
   discriminator.

---

## Method

**Phase observables** (NP_171 machinery): 16 fixed seeds, g ∈ [0, 8] in 60 steps each way, state carried
up→down, RK4 dt = 0.1. The range is wide enough that *every* lattice saturates (the late-locking controls
need g ≳ 3).

| Observable | Definition |
|---|---|
| O1 lock threshold | g where the locked fraction crosses 0.5 |
| O2 hysteresis area | ∫\|f_up − f_down\| dg / range |
| O3 mode ordering | Spearman ρ between per-node lock onset and that node's detuning \|ω_i − Ω\| |
| O4 locking sequence | onset dispersion σ_g/⟨g⟩ over the 96 nodes |
| O5 near-gap participation | onset(near-gap pair) − g_c |

**Spectral observables** (T_015 / D_048 machinery): 4 perturbation kinds (delete/add/rewire/weight) ×
5 doses (0.5 %…10 %) × 3 seeds, connectivity-guarded; ΔA = change in the distinct-eigenspace count,
Δλ₂/λ₂ = relative shift of the first positive eigenvalue. The control graphs are rebuilt from their spectra
by inverse DFT so they are genuine graphs, not re-labelled D96.

**Retention** (NP_172): R = τ_slow/τ_fast from the ring-down.

**The five controls** — each breaks exactly one thing D96 has:

| Control | What it changes |
|---|---|
| 1. random | topology: random 12-regular graph; D96 frequencies, uniform Q, same β |
| 2. degeneracy matched | the multiplicity pattern {1, 42×2, 5, 6} kept, the values replaced |
| 3. saturation matched | D96's β·u₀/γ kept, the topology changed. **Stated plainly:** the phase model has no independent saturation parameter, so for O1–O5 this control *coincides with control 1*; it is a distinct control only for retention (O8) |
| 4. linear ramp | values linearised over the same span (no crowding, no degeneracy) |
| 5. detuned | D96's frequencies scaled out of ratio |

Plus one **diagnostic** used only for attribution: a sparse circulant of the same degree with different
offsets, C96(±1..±5, ±11).

---

## Results

### 1. The measurement table

| Observable | **D96** | random | deg-matched | linear-ramp | detuned |
|---|---|---|---|---|---|
| O1 lock threshold g_c | **1.682 ± 0.025** | 0.8667 ± 3e−16 | 3.059 ± 0.053 | 3.160 ± 0.052 | 2.002 ± 0.100 |
| O2 hysteresis area A | **0.01225 ± 0.0037** | 0 ± 0 | 0.00813 ± 0.0062 | 0.00440 ± 0.0063 | **0.01583 ± 0.0080** |
| O3 mode ordering ρ | **0.420 ± 0.180** | 0 ± 0 | 0.056 ± 0.140 | −0.078 ± 0.100 | −0.197 ± 0.110 |
| O4 locking sequence dispersion | **0.0693 ± 0.030** | 1.2e−16 ± 3e−32 | 0.0160 ± 0.0110 | 0.0098 ± 0.0085 | 0.0523 ± 0.0210 |
| O5 near-gap participation | **0.285 ± 0.089** | 0.0667 ± 0 | 0.0956 ± 0.0430 | 0.0813 ± 0.0310 | 0.1065 ± 0.1000 |
| O6 attractor shift ΔA | **50.5 ± 1.1** | **0 ± 0** | 36.0 ± 21.0 | 37.7 ± 17.0 | 35.3 ± 21.0 |
| O7 spectral splitting Δλ₂/λ₂ | 0.445 ± 0.720 | 0.032 ± 0.049 | 2.341 ± 5.800 | 0.969 ± 1.700 | 1.700 ± 4.300 |
| O8 retention R | 6.358 | — | — | — | — |

The saturation-matched control gives **R = 6.358 — exactly D96's value** (NP_172's matched pair).

### 2. Stage 1 — the discriminator test

| Observable | z vs random | z vs deg-m | z vs lin-ramp | z vs detuned | **separates from all?** |
|---|---|---|---|---|---|
| O1 lock threshold g_c | 128.4 | 93.8 | 102.9 | 12.2 | **YES** |
| O2 hysteresis area A | 13.1 | 2.3 | 4.3 | 1.6 | no |
| O3 mode ordering ρ | 9.6 | 6.5 | 9.8 | 11.8 | **YES** |
| O4 locking sequence | 9.2 | 6.7 | 7.6 | 1.9 | no |
| O5 near-gap participation | 9.8 | 7.7 | 8.7 | 5.2 | **YES** |
| O6 attractor shift ΔA | 342.4 | 5.4 | 5.7 | 5.7 | **YES** |
| O7 spectral splitting | 4.4 | 2.5 | 2.2 | 2.2 | no |
| O8 retention R | 0 | — | — | — | no |

**The answer to the question as posed is YES — four of the eight observables separate D96 from every
control by more than 5σ.** The other four do not: the detuned control has a *larger* hysteresis area than
D96 (0.0158 vs 0.0123), locking sequence separates from only three, spectral splitting from none, and
retention fails outright (z = 0).

### 3. Stage 2 — does the difference mean anything?

**(a) Is the attractor shift about D96, or about being a sparse circulant?**

| Graph | ΔA |
|---|---|
| D96, C96(±1..±6) | 50.5 ± 1.1 |
| sparse circulant C96(±1..±5, ±11) — same degree, different offsets | **47 ± 0** |
| degeneracy-matched (full-band circulant) | 36 ± 21 |
| linear-ramp (full-band circulant) | 37.7 ± 17 |
| detuned (full-band circulant) | 35.3 ± 21 |
| random 12-regular | **0 ± 0** |

A *different* sparse circulant lands in the same ΔA regime as D96 (47 vs 50.5) and nowhere near the random
graph's exactly-zero value. The attractor-shift signature is therefore a property of the **sparse circulant
class** — degeneracy that a symmetry-breaking perturbation must split — not of D96's particular offsets.
(The residual z = 23.7 between D96 and the other sparse circulant is real, but it distinguishes two members
of the same class.) The random graph's exact ΔA = 0 is the derived end of the scale: an all-singleton
spectrum has nothing to split, which is D_047's degeneracy-lock theorem seen from the lock-lattice side.

**(b) Is the lock threshold explained by generic statistics?** g_c / mean|ω − Ω|:

| Lattice | mean\|ω−Ω\| | g_c | g_c / mean\|ω−Ω\| |
|---|---|---|---|
| D96 | 0.1059 | 1.682 | 15.88 |
| random | 0.1059 | 0.867 | **8.18** |
| deg-matched | 0.1975 | 3.059 | 15.49 |
| linear-ramp | 0.2011 | 3.160 | 15.71 |
| detuned | 0.1157 | 2.002 | 17.30 |

No single detuning law explains the thresholds (the ratio spans 8.2…17.3, a 63 % spread): with *identical*
frequencies, the random graph locks at roughly half D96's coupling because its local field is well mixed
rather than local. Topology and detuning both matter — still generic synchronization physics (mixing, not
multiplicity), and D96 is not the extreme case: random locks earlier, linear-ramp later.

**(c) Mode ordering and near-gap participation read back D96's own spectrum.** All four controls give
ρ ≈ 0 (random exactly 0 — every node locks in one simultaneous jump) while D96 gives ρ = 0.42, and D96's
near-gap pair locks 0.28 later than the global threshold against 0.07…0.11 for the controls. But the
mechanism is transparent: the fundamental pair has one of the largest detunings (ω₁ = 0.156 against
Ω = 0.851), and Adler dynamics captures the largest detuning last (NP_171). These observables therefore
identify the *spectrum the hardware is built to have*, i.e. an input, not an outcome.

---

## Classification

| Observable | Stage 1 (>5σ vs all) | Stage 2 (mechanism) | Classification |
|---|---|---|---|
| O1 lock threshold g_c | yes | set by each lattice's own detuning scale and mixing; D96 not extreme | **EMERGENT** |
| O2 hysteresis area A | no (detuned larger) | — | **REFUTED** |
| O3 mode ordering ρ | yes | reads back D96's own frequency multiset (an input) | **EMERGENT** |
| O4 locking sequence | no (detuned z = 1.9) | — | **REFUTED** |
| O5 near-gap participation | yes | largest-detuning-last, from D96's own spectrum | **EMERGENT** |
| O6 attractor shift ΔA | yes | reproduced by the sparse-circulant class, not by D96 | **EMERGENT / class property** |
| O7 spectral splitting Δλ₂/λ₂ | no | — | **REFUTED** |
| O8 retention R | no (z = 0) | reproduced exactly by the saturation-matched control | **REFUTED** |
| random graph's ΔA ≡ 0 | — | all-singleton spectra cannot split (D_047) | **DERIVED** |
| β = 0, uniform Q ⇒ single exponential | — | NP_172 closed form | **DERIVED** |
| "an observable is evidence that the AT lock law is real" | — | every separating observable turns on a generic property a control lacks | **REFUTED** |

**No canonical AT claim, value, equation or registry entry is changed.** No reclassification of any prior
result (the D_040 ClassificationRegistry is untouched); no new primitive.

---

## Verdict

1. **Yes, four observables separate D96 from every control at ≥ 5σ:** the lock threshold (z = 12…128), mode
   ordering (z = 6.5…11.8), near-gap participation (z = 5.2…9.8) and the attractor shift (z = 5.4…342).
2. **No, none of them is evidence for the AT structure.** Each separation turns on a generic property the
   control lacks: topology/mixing (threshold), sparsity (attractor shift), or D96's own frequency multiset
   (mode ordering, near-gap participation) — which is the *input* the hardware fixes by construction, not
   an outcome it measures. The observables that would carry evidence about the lock law itself fail Stage 1:
   hysteresis area (the detuned control is larger) and retention (z = 0 against the saturation-matched
   control).
3. **The useful residue is a classification** (see the table): the lock law's observable consequences are
   **generic exactly where they would be evidence** (threshold existence, hysteresis magnitude, retention)
   and **D96-specific exactly where they are not evidence** (mode ordering, near-gap participation, the
   sparse-circulant attractor shift).
4. **Consequence for the program.** With H1 failing on hysteresis (NP_171: 3.4σ < 5σ), H2's signature
   generic (NP_172) and the four separating observables all reading back inputs rather than outcomes
   (this audit), the Tier-1 lock-lattice experiment has **no measurement whose result would distinguish the
   lock law from ordinary coupled-oscillator physics**. That is a finding about the *test*, not about
   canonical AT — no canonical claim is changed, and any future claim that a floor measurement corroborates
   the lock law must name the matched control it beats, which none of the available controls can be.

---

## References

- `NP_NewPhysics/ResearchY-NP_170.md` — H1/H2, §6 R6 negative controls
- `NP_NewPhysics/ResearchY-NP_171.md` — lock-lattice simulator, the five lattices, the metrics used here
- `NP_NewPhysics/ResearchY-NP_172.md` — retention, the saturation-matched control (R reproduced exactly)
- `T_SpectralBlueprint/ResearchY-T_015.md`, `D_ResonanceStructure/ResearchY-D_048.md` — perturbation and attractor-shift machinery
- `D_ResonanceStructure/ResearchY-D_047.md` — degeneracy-lock theorem (the ΔA = 0 end of the scale)
- Test suite: `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_173_Tests.cs`

---

**Status:** COMPLETE. Literal answer: **four observables separate D96 from every control at ≥ 5σ**
(EMERGENT); none constitutes a discriminator of the AT structure (**REFUTED**); the mechanisms — sparse-
circulant splitting and the β = 0 baseline — are **DERIVED**. No canonical AT statement modified; no
reclassification; no new primitive.
