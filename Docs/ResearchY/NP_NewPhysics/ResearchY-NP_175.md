# ResearchY-NP_175 — Lock-Lattice Closure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_175 (permanent)
**Title:** Lock-Lattice Closure Audit — formal closure of the NP_170 physical-realizability program
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `NP_NewPhysics/ResearchY-NP_175.md`
**Reviews (completed audits only):** ResearchY-NP_170 (proposal), NP_171 (H1 in the deterministic model),
NP_172 (retention null audit), NP_173 (discriminator audit), NP_174 (program synthesis)
**Test suite:** — (closure audit; consolidates completed results, generates no new measurement)
**Registry effect:** status/archival labels only — **no canonical AT claim, value, equation or
classification-registry entry is modified; no file is deleted.**

---

## Question

> Does any AT-specific discriminator remain in the lock-lattice program?

Evidence base: **only completed audits**, NP_170 → NP_174. No new measurement is introduced here; this
document consolidates, classifies and closes.

---

## 1. Evidence base (completed audits only)

| Audit | Status | Result carried into this closure |
|---|---|---|
| **NP_170** | PLANNED (proposal, never executed) | Pre-registers H1 (sharpness ≥ 3, width ≤ 0.4, hysteresis A > 0 at ≥ 5σ), H2 (two-time-scale retention, R ≥ 10, controls single-exponential), H3 (|Θ(mid)| ∝ cos(Δφ/2), V ≥ 0.90), H4 (device benchmark). Declares the locking nonlinearity imported (NP_005), the coupling network engineered (NP_007/NP_011), the AT-120 barrier an **analogy**, g\* ≈ 0.31 a synthetic-cohort parameter (QG316), and abandons energy storage on density grounds |
| **NP_171** | COMPLETE | H1 in the deterministic model: sharpness **23.69 ± 5.28** (pass), width **0.132 ± 0.083** (pass), hysteresis **A = 0.0251 ± 0.0074 = 3.4σ < 5σ** (fail); g_c = 1.607 ± 0.030; **f(g = 1) = 0.0000** — nothing locks at the board's nominal maximum (K ≥ 6.394 = 10.29·ω₁); the random control locks earlier (0.863) and the degeneracy-matched control never locks for g ≤ 3; the near-gap doublet locks **last** (1.688 vs g_c = 1.607) |
| **NP_172** | COMPLETE | H2's retention signature is **generic**: a barrier-free strongly nonlinear ring passes **R = 10.248**; D96 and a random lattice give **R = 6.358 identically** (four significant digits); the saturation-matched control reproduces D96 exactly (z = 0); a 100× Q contrast produces nothing (ΔBIC < 0). Closed form E(τ) = u₀e^(−2γτ)/(1 + (βu₀/γ)(1 − e^(−2γτ))): τ_slow → 1/(2γ) for every β, shape set by β·u₀/γ alone |
| **NP_173** | COMPLETE | Four of eight observables separate D96 from **every** control at ≥ 5σ — lock threshold (z = 12.2…128.4), mode ordering ρ (6.5…11.8), near-gap participation (5.2…9.8), attractor shift ΔA (5.4…342.4) — but each turns on a generic property a control lacks (mixing, sparsity, D96's own input spectrum). **Hysteresis and retention fail outright** (the detuned control's A = 0.01583 exceeds D96's 0.01225; retention z = 0) |
| **NP_174** | COMPLETE | 18 observables classified: **0 AT-SPECIFIC**. H3 measured for the first time: a **superposition identity**, V = 1.0000 on the ideal ring *and* on D96 (arms 0.006398/0.006398, imbalance 7.7e−15) because (24, 72) is a mirror pair fixing the midpoint 48. The barrier's only route is **structurally zero**: max \|w_ij − w_ji\| = 0, driven asymmetry 1e−16; the nonlinear residue is quadratic in the drive (1.1e−2 → 1.1e−4) = saturation. Modelling defect found and fixed (per-node degree normalization); all C96 results re-verified unchanged |

---

## 2. Required conclusions

### 2.1 NP_170 proposal status

**CLOSED AS A TEST PROGRAM — NOT REFUTED AS A PROPOSAL.** NP_170 was never executed and remains internally
sound: it declared its imports (the locking nonlinearity via NP_005, the engineered coupling network via
NP_007/NP_011), flagged the AT-120 barrier as an analogy rather than a derivation, excluded the
synthetic-cohort g\* ≈ 0.31 from hardware claims, and abandoned the energy-storage framing on density
grounds. Nothing in NP_171–NP_174 contradicts any of that. What closes is the *physical-realizability test
program*: as specified it contains no measurement whose outcome could distinguish the lock law from
ordinary coupled-oscillator physics.

### 2.2 H1 status

**Not confirmed in the model, and not usable as a discriminator.** H1's existence sub-criteria are met
decisively (sharpness 23.69 ≥ 3; width 0.132 ≤ 0.4), but (i) the hysteresis sub-criterion fails at
**3.4σ < 5σ** — the loop is real but below the significance the proposal itself pre-registered; (ii) the
threshold lies at **1.61× the board's nominal maximum coupling** (f(g = 1) = 0.0000; K ≥ 6.394 = 10.29·ω₁),
so a board built to the nominal normalization measures nothing; and (iii) a lock threshold is **GENERIC** —
every lattice has one, the controls spanning 0.867…3.160 with the random graph locking *earlier* than D96.
H1 is therefore not a discriminator, independently of whether hardware would pass it.

### 2.3 H2 status

**Generic; not AT-specific.** The pre-registered retention signature is produced by amplitude-dependent
(saturating) damping alone: a barrier-free generic ring reaches **R = 10.248 ≥ 10**, while D96 and a random
lattice are **indistinguishable at R = 6.358** and the saturation-matched control reproduces D96 exactly
(z = 0). A realistic 100× Q contrast supplies no signature at all (ΔBIC < 0). Retention therefore cannot
serve as evidence for the one-way barrier, and the AT-120 analogy remains an analogy — now for a quantified
reason.

### 2.4 Discriminator status

**No AT-specific discriminator remains.** NP_173 found four observables that separate D96 from all five
controls at ≥ 5σ, but each is explained by a property a control lacks or by D96's own input structure: the
attractor shift is a **sparse-circulant class** property (a different sparse circulant gives ΔA = 47 ± 0
against D96's 50.5 ± 1.1, random exactly 0); the threshold follows each lattice's own detuning **and**
mixing (g_c/mean\|ω − Ω\| spans 8.2…17.3); and mode ordering plus near-gap participation read back the
canonical spectrum the board is built to have. NP_174 then closed the two remaining routes: H3 is a
**superposition identity** (V = 1.0000 on both lattices, by the mirror pairing H0 itself checks), and the
**only** observable a one-way barrier could produce — a directional, non-reciprocal response — is
**structurally zero**, because the canonical coupling is exactly reciprocal and the chain supplies no
antisymmetric term.

### 2.5 Remaining open questions

1. **Does any AT-native construction yield a non-reciprocal coupling (w_ij ≠ w_ji)?** This is the single
   structural prerequisite for an AT-specific observable in a lattice experiment. It is a **theory**
   question, not an engineering one, and it is the sole reopening condition for a hardware program.
2. **Can the lock law beat QG319's classifier rates** (FP = 96.9 %, FN = 66.1 %) on datasets under a
   blinded, pre-registered protocol? A **statistics** question; it needs no board and is not closed here.
3. **Should the lock law's status be re-stated in the monograph index** as a descriptive regularity with
   domain-specific values (QG313, evidenced on datasets and synthetic cohorts QG307–QG319)? A
   **documentation** question.
4. **Minor, for any future device program:** the H0/H3 results show that mirror-pairing and superposition
   checks are **validity gates** (they confirm the build), and must never be reported as evidence about the
   law.

### 2.6 Recommended project status

**CLOSE** the NP_170 physical-realizability program (Tiers 1–3). Keep NP_170 and NP_171–NP_174 as the
closed evidence record; retain the lock law's statistical status unchanged.

---

## 3. Observable classification (complete, from NP_174)

| Class | Count | Observables |
|---|---|---|
| **GENERIC** | 11 | H1 threshold existence; sharpness; width; hysteresis area A; retention R; decay-law shape; τ_fast; τ_slow; E_retained/storage FOM; spectral splitting Δλ₂/λ₂; interference visibility H3 |
| **D96-SPECIFIC** (reads back the built-in structure — an **input**, not an outcome) | 5 | H0 validity (mirror pairing, bands); per-mode onsets / locking order; near-gap participation; mode ordering ρ(onset ~ detuning); attractor shift ΔA |
| **AT-SPECIFIC** | **0** | — (the single candidate, the directional response, is structurally zero) |
| Excluded / refuted before this audit | 2 | g\* ≈ 0.31 (synthetic-cohort parameter, QG316/MONO007); energy density / storage (abandoned in NP_170 §1) |

---

## 4. Decision rules applied

| Rule | Application | Outcome |
|---|---|---|
| IF AT-SPECIFIC count = 0 THEN recommend CLOSE | count = 0 (§3) | **CLOSE** |
| IF an observable is generic oscillator physics THEN it cannot be used as evidence for AT | 11 observables are generic (§2.2–2.4) | they carry no evidential weight for the lock law |
| IF an observable only reads back the built-in D96 structure THEN classify D96-SPECIFIC, not AT-SPECIFIC | 5 observables are D96-SPECIFIC | they confirm the build, not the law |

---

## 5. Required output

```
ProgramStatus: CLOSE

ClosureReason:
  The NP_170 physical-realizability program is closed because no measurement survives that could
  distinguish the AT lock law from ordinary coupled-oscillator physics: NP_171 found H1's
  pre-registered hysteresis criterion failing at 3.4σ with the threshold at 1.61× the board's
  nominal maximum coupling (f(g = 1) = 0), NP_172 showed H2's retention signature to be generic
  (a barrier-free ring passes R = 10.248 while D96 and a random lattice give R = 6.358 identically
  and a saturation-matched control reproduces D96 exactly), NP_173 found that although four
  observables separate D96 from every control at ≥ 5σ each is explained by a generic property a
  control lacks or by the canonical spectrum that is an input to the experiment, and NP_174 closed
  the last two routes by measuring H3 as a superposition identity (V = 1.0000 on both lattices, by
  the mirror pairing the build already checks) and the one-way barrier's only possible signature,
  a non-reciprocal directional response, as structurally zero (max |w_ij − w_ji| = 0; asymmetry
  1e−16), leaving 18 audited observables with zero AT-specific ones — so the program's own decision
  rule (AT-SPECIFIC count = 0 → CLOSE) applies. The proposal itself is NOT refuted: NP_170 declared
  its imports, labelled the AT-120 barrier an analogy, excluded the synthetic-cohort g* ≈ 0.31 from
  hardware claims and abandoned energy storage on density grounds, and none of that is contradicted.
  What is closed is the test program, not the theory: the Ch8 lock law remains a statistical
  regularity with domain-specific values (QG313), and no canonical AT claim, value, equation or
  classification-registry entry is modified by this closure.

ArchiveRecommendation:
  1. KEEP NP_170 (`NP_NewPhysics/ResearchY-NP_170.md`, `Publication/ResearchY-NP_170-Lock-Lattice-Program.tex`/.pdf)
     and NP_171–NP_174 unchanged as the closed evidence record. Do not delete any file.
  2. MARK SUPERSEDED: label NP_170's status as CLOSED (superseded by ResearchY-NP_175) in the ResearchY
     index and status registries, and add a single archival pointer line to the NP_170 document header.
     No claim, number, criterion or boundary statement inside NP_170 is altered.
  3. PRESERVE REPRODUCIBILITY: keep the deterministic harnesses
     (`AT.Core/Resonance/Kuramoto/LockLatticeSimulator.cs`, `RetentionNullSimulator.cs`) and the test
     suites (`Y_NP_171_Tests.cs` 4/4, `Y_NP_172_Tests.cs` 5/5, `Y_NP_173_Tests.cs` 5/5,
     `Y_NP_174_Tests.cs` 5/5 — all passing, fixed seeds, no randomness), so that every number cited in
     the closure can be regenerated at any time. The correction recorded in NP_174 (per-node degree
     normalization → the lattice constant ⟨d⟩) is part of that record.
```

---

## 6. Boundary conditions honoured

- **No canonical AT claim, value, equation or equation-form is modified.** This closure changes no
  physics; it changes the status of a *test*.
- **No registry classification is modified.** The D_040 ClassificationRegistry is untouched; no
  DERIVED / EMERGENT / BOUNDARY label is added, moved or reinterpreted. NP_171's ω_max label corrigendum
  (k = 11/85, λ_max = 15.837372, values unchanged) was recorded in NP_171 and is repeated nowhere as a
  reclassification.
- **No file is deleted.** NP_170 and its publication artefacts remain in place, unchanged apart from the
  archival pointer.
- **Archival status only.** The only registry edits are status labels: NP_175 added; NP_170 marked
  CLOSED (superseded).

---

## FINAL VERDICT

```
================================================================================
 FINAL VERDICT — ResearchY-NP_175
================================================================================
 NP_170                        : NOT REFUTED
 NP_170 TEST PROGRAM           : CLOSED
 AT-SPECIFIC OBSERVABLES       : 0 of 18 audited
 PROGRAM STATUS                : CLOSE

 REASON
   No remaining AT-specific measurement survives NP_171–NP_174.

   · H1 (NP_171): sharp and narrow in the model, but the pre-registered hysteresis
     criterion fails at 3.4σ < 5σ, nothing locks at the board's nominal maximum
     coupling (f(g = 1) = 0; K ≥ 10.29·ω₁), and a threshold is generic to every lattice.
   · H2 (NP_172): retention is generic — a barrier-free ring passes R = 10.248, D96 and
     a random lattice give R = 6.358 identically, and a saturation-matched control
     reproduces D96 exactly (z = 0).
   · Discriminators (NP_173): the four observables that separate D96 from every control
     at ≥ 5σ are generic-driven or read back the built-in D96 spectrum; hysteresis and
     retention fail outright.
   · Synthesis (NP_174): H3 is a superposition identity (V = 1.0000 on both lattices,
     by the mirror pairing the build already checks), and the one-way barrier's only
     possible observable — a directional, non-reciprocal response — is structurally
     zero (max |w_ij − w_ji| = 0; asymmetry 1e−16; the nonlinear residue is quadratic
     in the drive, i.e. saturation).

 WHAT REMAINS OPEN (not closed by this audit)
   · A theory audit: does any AT-native construction yield a non-reciprocal coupling
     (w_ij ≠ w_ji)? That is the single structural prerequisite that could reopen a
     hardware test.
   · A statistics program: can the lock law beat QG319's classifier rates (FP 96.9 % /
     FN 66.1 %) on datasets under a blinded, pre-registered protocol? No board needed.

 WHAT IS UNCHANGED
   · Canonical AT: no claim, value, equation or boundary statement modified.
   · The D_040 classification registry: untouched.
   · The Ch8 lock law: a statistical regularity with domain-specific values (QG313),
     evidenced on datasets and synthetic cohorts (QG307–QG319) — exactly as before.
================================================================================
```

---

## References

- `NP_NewPhysics/ResearchY-NP_170.md` — the proposal (kept, marked superseded)
- `NP_NewPhysics/ResearchY-NP_171.md`, `ResearchY-NP_172.md`, `ResearchY-NP_173.md`, `ResearchY-NP_174.md`
- `NP_NewPhysics/ResearchY-NP_005.md` — the locking term is antisymmetric, hence reciprocal
- QG120 (one-way barrier), QG313 (domain-specific values), QG316/QG319 (protocol and classifier rates)
- Deterministic harnesses: `AT.Core/Resonance/Kuramoto/LockLatticeSimulator.cs`,
  `RetentionNullSimulator.cs`; suites `Y_NP_171_Tests.cs` (4/4), `Y_NP_172_Tests.cs` (5/5),
  `Y_NP_173_Tests.cs` (5/5), `Y_NP_174_Tests.cs` (5/5)

---

**Status:** COMPLETE — closure audit. ProgramStatus: **CLOSE**. NP_170 NOT REFUTED; NP_170 TEST PROGRAM
CLOSED. No canonical AT claim modified; no registry classification modified; no file deleted; archival
status recommended and applied as labels only.
