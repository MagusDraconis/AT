# ResearchY-NP_174 — Program Synthesis: does any discriminator remain?

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_174 (permanent)
**Title:** Lock-lattice program synthesis — is any observable AT-specific rather than generic or D96-specific?
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `NP_NewPhysics/ResearchY-NP_174.md`
**Reviews:** ResearchY-NP_170 (proposal), ResearchY-NP_171 (H1), ResearchY-NP_172 (H2/H3-side retention),
ResearchY-NP_173 (discriminators)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_174_Tests.cs` (5 tests, all passing)
**Decision:** **CLOSE** the lock-lattice physical-realizability program as specified, with one explicitly
conditional REVISE trigger — no canonical claim is changed.

---

## Question

> Does the lock-lattice program contain any remaining discriminator between the **AT lock law** and
> **ordinary coupled-oscillator physics**?

Success = at least one **AT-SPECIFIC** observable. Failure = none remains. Output: CONTINUE / REVISE / CLOSE.

**Classification rule, fixed before the table was read:**

| Class | Meaning |
|---|---|
| **GENERIC** | reproducible by ordinary coupled-oscillator physics with no AT content; a positive measurement is not evidence about the lock law |
| **D96-SPECIFIC** | depends on the canonical D96 spectrum/structure — but that structure is an **input** the hardware is built to have, so measuring it tests the build, not the law (QG313 already forbids transferring the D96 lock *values*) |
| **AT-SPECIFIC** | would be produced by the AT lock law / one-way barrier and by nothing generic — the only class that can corroborate the lock law |

---

## 1. Every observable, classified

| # | Observable | Source | Class | Evidence |
|---|---|---|---|---|
| 1 | H0 validity (mirror pairing, bands) | NP_170 | D96-SPECIFIC | a build gate, not a test of the law |
| 2 | lock threshold g_c (existence) | NP_170/171 | GENERIC | NP_171: sharp + narrow for every lattice; NP_173: controls span 0.867…3.160 |
| 3 | sharpness, width of f(g) | NP_171 | GENERIC | 23.69 / 0.132 for D96; all lattices sharp (15…60) |
| 4 | hysteresis area A | NP_170/171 | GENERIC | NP_173: the **detuned** control is larger (0.01583 vs 0.01225) |
| 5 | per-mode onsets / locking order | NP_171 | D96-SPECIFIC | NP_173: ρ(onset~detuning) = 0.42 vs ≈ 0 for controls |
| 6 | near-gap participation | NP_171/173 | D96-SPECIFIC | largest detuning captured last; ω₁ = 0.156 vs Ω = 0.851 is an input |
| 7 | retention R = τ_slow/τ_fast | NP_170/172 | GENERIC | NP_172: barrier-free ring passes R = 10.248; saturation-matched control z = 0 |
| 8 | decay-law shape (two-exponential) | NP_172 | GENERIC | produced by β·u₀/γ alone |
| 9 | τ_fast, τ_slow individually | NP_172 | GENERIC | closed form: τ_slow → 1/(2γ) for every β |
| 10 | E_retained, storage FOM | NP_170/172 | GENERIC | damping bookkeeping |
| 11 | attractor shift ΔA | NP_173 | D96-SPECIFIC* | a *different* sparse circulant gives ΔA = 47 ± 0 vs D96 50.5 ± 1.1 — a class property |
| 12 | spectral splitting Δλ₂/λ₂ | NP_173 | GENERIC | separates from **no** control (z = 2.2…4.4) |
| 13 | mode ordering ρ(onset~detuning) | NP_173 | D96-SPECIFIC | ring-local coupling preserves the input spectrum's fingerprint |
| 14 | interference visibility (H3) | NP_170 | GENERIC | measured in §2 — a superposition identity, V = 1 on **both** lattices |
| 15 | **directional / non-reciprocal response** | NP_170 (QG120) | **AT-SPECIFIC?** | measured in §3 — **identically zero**: the canonical coupling is exactly reciprocal |
| 16 | D96 lock values 20.0026 / 2.4105 / 8.2980 | NP_170 | D96-SPECIFIC | QG313: domain-specific, must **not** be transferred |
| 17 | g\* ≈ 0.31 | NP_170 | — excluded | QG316/MONO007: a synthetic-cohort parameter |
| 18 | energy density / storage | NP_170 §1 | — refuted | µJ at kW overhead, 10⁶–10⁹× behind Li-ion; abandoned in the proposal itself |

**AT-SPECIFIC rows: 1** — row 15, the only observable the one-way barrier could ever produce. §3 closes it.

---

## 2. H3 — the interference read-out (measured here for the first time)

NP_170 pre-registers |Θ(mid)|/(|Θ(x₁)|+|Θ(x₂)|) ∝ cos(Δφ/2) with **V ≥ 0.90**. Measured exactly, without a
phase sweep: the midpoint response to two drives with relative phase Δφ is |G_mid,x₁ + e^{iΔφ}·G_mid,x₂|, so
the whole curve is fixed by the two single-arm responses, and the visibility of two sources of amplitudes
a₁, a₂ is the standard V = 2√(a₁a₂)/(a₁+a₂).

| Lattice | \|G_mid,24\| | \|G_mid,72\| | arm imbalance | visibility V | cos(Δφ/2)? |
|---|---|---|---|---|---|
| symmetric ring (single common ω) | 0.006398 | 0.006398 | 0 | **1.0000** | exact |
| **D96 ring** | 0.006398 | 0.006398 | 7.7e−15 | **1.0000** | exact |

**Why the D96 arms are also exactly equal — and why that is the point.** The probes (24, 72) form a
**mirror pair** of the ring: 96 − 24 = 72, and the reflection j → 96 − j fixes the midpoint 48. The
canonical spectrum is mirror-symmetric (ω_j = ω_{96−j} — the pairing NP_170's H0 checks), so the lattice is
invariant under that reflection and the arms are equal to machine precision. H3's gate therefore cannot
fail on D96 either: it is satisfied by an **exact symmetry of the canonical lattice**, not by anything the
experiment would discover.

**Classification: GENERIC (tautological).** The read-out *is* cos(Δφ/2), on the ideal ring and on the real
D96 ring alike, because it is linear superposition constrained by a symmetry the lattice already has. Like
H0, H3 confirms the build, not the law.

---

## 3. The barrier's only route — and why it is structurally closed

The AT-120 one-way barrier (QG120) is the only AT-specific ingredient the program ever invoked. To be a
physical claim it must break **reciprocity**.

**(a) Structural test (exact, no integration):**

| Lattice | max \|w_ij − w_ji\| over all pairs | reciprocal? |
|---|---|---|
| D96 ring | 0.000e+00 | **YES** |
| random control | 0.000e+00 | **YES** |

**(b) Dynamic corroboration** — driven asymmetry between (16, 40) in the linear limit:

| Settle time | D96 ring | random ring |
|---|---|---|
| 20 000 steps (5 τ) | 9.8e−17 | 2.0e−16 |
| 60 000 steps (15 τ) | 3.9e−16 | 1.4e−16 |

Machine zero on both, independently of settle time — exactly as the structural result requires.

**(c) The confound, for completeness** — asymmetry with the ring as proposed (β = 0.05):

| Lattice | drive 0.02 | drive 0.002 (10× smaller) |
|---|---|---|
| D96 | 1.098e−2 | 1.051e−4 |
| random | 1.852e−3 | 1.914e−5 |

It shrinks by two orders of magnitude when the drive drops 10× — **quadratically**, i.e. the saturation
term β\|a\|² — on both lattices alike. Amplitude-dependent saturation vanishes as the signal → 0, whereas
genuine non-reciprocity does not. A hardware run reporting this number as a one-way barrier would be
reporting its own saturation.

**Why the linear result is structural.** The coupling is symmetric by construction: w_ij = w_ji, and the
phase term sin(θ_j − θ_i) is antisymmetric, so both directions enter with equal magnitude; for symmetric A
the linear response G = (iωI − A)⁻¹ is itself symmetric, hence |G_ij| = |G_ji| **exactly**. The canonical
chain supplies **no** antisymmetric (non-reciprocal) coupling — the NP_005 locking term κ·sin(θ_B−θ_A) is
antisymmetric too, i.e. also reciprocal.

**Classification: the single candidate AT-SPECIFIC observable is REFUTED** — structurally, not merely
empirically.

### 3.1 A modelling defect found and fixed by this audit

The reciprocity probe exposed a real bug. A first measurement gave a *finite* asymmetry on the random
control (0.071) and none on D96. The cause: the coupling was normalized **per node** (÷d_i), i.e. by
D = diag(d_i), and D⁻¹W is symmetric **only when all degrees agree**. The canonical ring has uniform degree
12 (so no C96 result changes), but the "random 12-regular" control is not exactly regular (mean degree
14.917), so it acquired a spurious one-way character. **Fix:** the divisor is now the single lattice
constant ⟨d⟩ = (Σ_ij w_ij)/N (`Lattice.MeanDegree`), which restores exact reciprocity for every lattice.

**Impact:** none on any cited result. Re-measured after the fix: D96 g_c = 1.695 (unchanged), random-control
g_c = 0.867 (unchanged), retention R = 6.358 for both D96 and the saturated random control (unchanged), and
all other lattices unchanged (their mean degree is already exactly 12). The only quantity that moved is the
reciprocity asymmetry itself — 0.071 → 1e−16 — which is precisely the intended effect.

---

## 4. Verification of the cited results

| Cited | Re-measured here | Agreement |
|---|---|---|
| NP_171 g_c = 1.607 ± 0.030 (30 seeds) | 1.604 (4 seeds) | ✓ |
| NP_172 R = 6.358 for D96 and for the saturation-matched control | 6.358 / 6.358 | ✓ (z = 0) |
| NP_173 ΔA: D96 50.5 ± 1.1, random 0 ± 0 (n = 60) | 50.5, 0.0 (n = 60) | ✓ |

All cited values reproduce within their stated uncertainties, so §1 rests on measurements that are stable
under re-run (deterministic simulators, fixed seeds, no noise).

---

## 5. Decision

**SUCCESS CRITERION: at least one AT-SPECIFIC observable. RESULT: none.**

Of 18 audited observables and claims, every one is **GENERIC** (reproducible without AT), **D96-SPECIFIC**
(the canonical spectrum — an input the hardware is built to have), or already excluded/refuted (g\* ≈ 0.31,
the energy-storage framing). The single AT-SPECIFIC candidate — the directional response the one-way barrier
would have to produce — is **structurally zero** (§3).

**Summary of the four audits**

- **NP_170** proposal: H1 (threshold), H2 (retention), H3 (read-out), H4 (device benchmark); boundaries
  declared, the barrier flagged as an **analogy**, energy storage abandoned.
- **NP_171** H1 in the deterministic model: sharp (23.69) and narrow (0.132) at g_c = 1.607 ± 0.030 — but the
  hysteresis sub-criterion fails at 3.4σ < 5σ, and f(g = 1) = 0: nothing locks at the board's nominal
  maximum coupling.
- **NP_172** H2: two-time-scale retention is **generic** — a barrier-free nonlinear ring passes R = 10.248;
  D96 and a random lattice give R = 6.358 identically; a 100× Q contrast gives nothing (ΔBIC < 0).
- **NP_173** discriminators: four observables separate D96 from every control at ≥ 5σ, but each turns on a
  generic property a control lacks; hysteresis and retention fail outright.
- **NP_174** synthesis: H3 is a superposition identity (V = 1 on both lattices); the barrier's only route is
  structurally closed. **No AT-specific observable remains.**

### DECISION: **CLOSE**

Close the lock-lattice **physical-realizability** program (NP_170 Tiers 1–3) at the proposal stage. As
specified it has no measurement whose outcome could distinguish the lock law from ordinary
coupled-oscillator physics: the tests that would be evidence are generic (threshold existence, hysteresis,
retention, read-out), and the observables that are D96-specific are inputs rather than outcomes.

**What this does NOT say.** It does not touch canonical AT: no claim, value, equation or registry entry
changes, and the D_040 ClassificationRegistry is untouched. The Ch8 lock law remains what it was — a
statistical regularity with domain-specific values (QG313), evidenced on datasets and synthetic cohorts
(QG307–QG319), and honestly labelled as such. The *question* is not closed, only this test of it.

### The one conditional REVISE trigger (recorded, not pursued)

A revised program becomes possible only if the theory derives a genuinely **non-reciprocal** term — a
coupling whose forward and backward strengths differ. That is the sole structural prerequisite for an
AT-specific observable in a lattice experiment (precisely what §3 shows is missing), and it would be **new
physics**, not in the canonical chain. The concrete reopening test is therefore a *theory* audit first:
does any AT-native construction yield w_ij ≠ w_ji? Until one does, the hardware program cannot be revived by
better engineering. A second, weaker revision path — testing the lock law on datasets under blinded,
pre-registered protocols, with QG319's FP = 96.9 % / FN = 66.1 % as the obstacle to beat — is a statistics
program, not a hardware one, and requires no board.

---

## References

- `NP_NewPhysics/ResearchY-NP_170.md`, `ResearchY-NP_171.md`, `ResearchY-NP_172.md`, `ResearchY-NP_173.md`
- `NP_NewPhysics/ResearchY-NP_005.md` — the locking term is antisymmetric, hence reciprocal
- `D_ResonanceStructure/ResearchY-D_047.md` — degeneracy-lock theorem (the ΔA = 0 end of the scale)
- `T_SpectralBlueprint/ResearchY-T_015.md`, `D_ResonanceStructure/ResearchY-D_048.md` — perturbation machinery
- QG120 (one-way barrier), QG313 (domain-specific values), QG316/QG319 (protocol and classifier rates)
- Simulators: `AT.Core/Resonance/Kuramoto/LockLatticeSimulator.cs`, `RetentionNullSimulator.cs`
- Test suite: `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_174_Tests.cs`

---

**Status:** COMPLETE. Output: **CLOSE** (lock-lattice physical-realizability program), with one conditional
REVISE trigger (a derived non-reciprocal coupling). No canonical AT statement modified; no reclassification;
no new primitive.
