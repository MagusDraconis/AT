# AT Project Memory — Archive Part 2: Gravity-Source & Gauge Era

Historical detail split out of `Docs/NewChat_Start.md` (2026-10-11), moved **verbatim**. Covers the V2.2 lock-lattice
(NP) program that opened the era, the gravity-source audit chain ResearchY-G_001 - G_080, the electromagnetism audits
ResearchY-E_001 - E_016, and the unnumbered gravity-era audits of the same sequence.

Current state: `Docs/NewChat_Start.md`. Open problems: `Docs/NewChat_OpenProblems.md`. Audit catalogue:
`Docs/AuditIndex.md`.

---
# The Actualization Theory — New Chat Context, Part 3

Historical detail split out of `Docs/NewChat_Start.md` (2026-10-11). The content below is moved **verbatim**, with no
edit: every ResearchY per-audit section (Lock-Lattice Program, G_001-G_080, E_001-E_016, QM_001-QM_011) and the
"Major Milestones" log (V2.1 wave-geometry program, the AT-QG phase series, and the V2.0 monograph editorial passes).

The concise current state, the open problems, the status table and the architecture summary live in
`Docs/NewChat_Start.md`. Part 1 (mission and early phases) and Part 2 (chronological phase log) remain separate.

---
## New Research Direction — Lock-Lattice Program (proposal, 2026-09-11)

ResearchY-NP_170 (PLANNED — proposal only, no result claimed): the Ch8 lock law has, so far,
only statistical evidence (datasets and synthetic cohorts, QG307–QG319). NP_170 specifies the
first *physical* test.

- **The one idea (energy storage is ABANDONED).** The density objection is accepted as fatal
  (µJ at kW overhead, 10⁶–10⁹× behind Li-ion). What survives is the lock lattice as a
  **phase-memory / threshold element (LLPM)**: the state variable is *which modes are locked*,
  not charge separated (capacitor) or chemical potential stored (battery).
- **Three reasons.** (1) *Theory falsification* — a hardware ring is the only place the lock law
  must produce a dynamical phase transition (QG316 criteria: sharpness ≥ 3, width ≤ 0.4) rather
  than a statistic; AT's own Ch14 concedes standard complexity measures match or beat the lock
  rule on the evolving cohort. (2) *Phase/coherence memory* — density irrelevant; metrics are
  fJ/write, retention, variability < 5%. (3) *Neuromorphic threshold element* — the sharpness is
  predicted, not fitted (must beat PCM/memristor variability).
- **How.** Tier 1 bench ring: 96 LC nodes with tanh saturation, 12 programmable links per node
  (6 DACs set w_d), realizing C₉₆(1..6) with λ_k = 2Σ_d w_d(1−cos 2πdk/96) via the accepted
  inverse-spectral recipe (ResearchY-T_001). Runs: R0 calibration → R1 mirror-pairing validity
  (λ_k = λ_{96−k}) → R2 interference visibility (cos(Δφ/2), V ≥ 0.90) → R3 hysteresis sweep
  (A > 0 at ≥ 5σ) → R4 retention (two-time-scale fit, R = τ_slow/τ_fast ≥ 10 vs three
  single-exponential controls) → R5 benchmark → R6 blinding; kill criteria pre-registered.
- **Boundaries declared.** Locking nonlinearity imported (NP_005: locking force ABSENT); coupling
  network engineered (NP_007/NP_011: REFUTED as a physical field); retention mechanism (AT-120
  one-way barrier) is an **analogy across systems, not a derivation**; g* ≈ 0.31 is a
  synthetic-cohort parameter and is **not** a hardware prediction; D96 lock values
  (20.0026/2.4105/8.2980) are domain-specific per QG313.
- **Numbers verified for the proposal.** λ₂ = 0.386351, ω₁ = 0.621571, span = 6.402515,
  octave bands [4,4,87], 47 mirror pairs, multiplicities {2×42, 5, 6}, zero mode 1.
- **Deliverables.** `Docs/ResearchY/NP_NewPhysics/ResearchY-NP_170.md`;
  `Docs/ResearchY/Publication/ResearchY-NP_170-Lock-Lattice-Program.tex` (+ compiled PDF, 7 pp).
  Drafted but NOT applied: registry entry **AT-P043** (AT-P042 = tick discriminator, M_009); the
  canonical registry is unmodified. No canonical claim changed, no reclassification, no new
  primitive.
- **Next open question.** Does the lock threshold exist in hardware (deciding experiment = one
  bench prototype), and does the AT-120 barrier transfer to a Laplacian ring (H2, highest risk,
  highest information)? *The pre-hardware half of the threshold question is now answered by
  ResearchY-NP_171 (next section): the deterministic model predicts a sharp, narrow transition at
  g_c = 1.607 — but only a 3.4σ hysteresis, and nothing at all locks at the board's nominal
  maximum coupling.*
- **REV. 2 (2026-09-11, after the post-fetch results NP_169 + T_007–T_015).** No incoming result
  invalidates the experiment and none of those commits touched the prediction/classification/
  coverage registries (so AT-P043 stays unclaimed). Four consequences were applied to NP_170:
  (1) **method** — T_015 refutes "D96 outperforms random" and shows robustness is controlled by the
  *degeneracy* structure (D96 Δλ₂/λ₂ = 0.0077 with ΔA = 30 vs random 0.0003/ΔA = 0), so a fourth
  **mandatory degeneracy-matched control** ({1+42×2+5+6}) is added, R4 is scored against it, and no
  spectral-superiority claim is made; (2) **framing** — T_010/T_012/T_014 make D96 the *compressed,
  low-capacity* lattice (5 survivors vs 17; only 2 modes within 2λ₂), so D96 is chosen for
  derivability, not capacity, with D96³ as a non-canonical engineering variant; (3) **framing** —
  NP_169 (λ_gap = 4π²·91/N² + O(N⁻⁴)) makes the ring's gap an imported kinematic finite-size gap, so
  λ₂ is not a physical mass-gap scale in hardware; (4) **protocol** — T_014 requires per-mode
  ε_j = 0.01 ω_j. Unchanged: core experiment R1–R5, the three reasons, kill criteria, all verified
  D96 numbers. Files: `ResearchY-NP_170.md` (rev. 2, §13 revision note) and the 10-page PDF.

## Lock-Lattice Simulator — H1 predicted before hardware (2026-09-11)

ResearchY-NP_171 (COMPLETE): the deterministic numerical precursor to NP_170. Canonical
C₉₆(±1..±6) ring, node i ↔ eigenmode i (ω_i = √λ_i from the exact closed form, normalized by
ω_max), Adler/Kuramoto dθ_i/dτ = ω_i + g·(1/deg)·Σ_j sin(θ_j − θ_i), **no stochastic term**,
fixed-seed LCG initial phases (30 seeds = the protocol's ≥30 repeats), RK4 dt = 0.1 verified
against dt/2; metrics verbatim from NP_170 §6 R3 / QG316 (g swept 40 steps each way over [0, 3]
carrying the state up→down).

- **H1 is predicted to FAIL — for exactly one reason.** D96: sharpness **23.69 ± 5.28 ≥ 3 PASS**,
  width **0.132 ± 0.083 ≤ 0.4 PASS**, but hysteresis **A = 0.0251 ± 0.0074 → 3.4σ < 5σ FAIL**. The
  loop is real (at g = 1.6 the up-sweep gives f = 0.22 while the down-sweep still holds 0.62), but
  not at the pre-registered significance: the *noiseless ideal* ring cannot supply H1's bistability.
- **Threshold and the hardware requirement.** **g_c = 1.607 ± 0.030** (units ω_max = 1) with
  **f(g = 1) = 0.0000** — at the board's nominal maximum coupling NOTHING locks. The design
  requirement is K ≥ 6.394 rad/time = 1.61·ω_max = **10.29·ω₁** = 15.17·⟨|ω_i − Ω|⟩.
- **No D96 superiority (again, consistent with T_015).** The random-coupling control locks EARLIER
  (g_c = 0.863; the only lattice fully locked at g = 1) and has EXACTLY zero hysteresis; the T_015
  degeneracy-matched control never locks within g ≤ 3 (f(3) = 0.167), so the multiplicity pattern
  alone does not reproduce D96 — the values matter. All four §6 controls fail H1 as §7 requires.
  **H1's existence test is therefore NOT a discriminator** between the lock lattice and its
  controls; the discriminating test must be R4 retention.
- **Arrangement matters:** the canonical mode-index arrangement (g_c = 1.607) versus the identical
  multiset sorted ascending (1.746) differ by 9%.
- **Mode-specific locking (T_014):** the near-gap doublet {1, 95} locks at g = 1.688 — AFTER the
  global half-lock point, because its detuning from Ω is among the largest (largest detuning is
  captured last). Class spread is only 1.635–1.688, so the transition is collective, and a
  near-gap-only lock is not the same event as a global one.
- **Corrigendum (labels only, values unchanged):** NP_170 §3's "ω_max = √λ_max (k = 48, λ = 12)"
  should read **k = 11/85 with λ_max = 15.837372** (λ₄₈ = 12 exactly). ω_max = 3.979620 and
  span = 6.402515 are unaffected.
- **Classification.** Dynamics and nonlinearity IMPORTED/BOUNDARY (NP_005, NP_007/NP_011); the
  sharp-threshold *existence* is EMERGENT (standard synchronization physics, not new AT content);
  g_c is EMERGENT and domain-specific (QG313 — not comparable to QG316's 0.31); "AT predicts a
  physical phase transition" remains OPEN/UNTESTED (only the board can settle it, then NP_170 §8's
  kill criteria apply). No canonical claim changed; no reclassification (D_040 registry untouched);
  no new primitive.
- **Files.** `Docs/ResearchY/NP_NewPhysics/ResearchY-NP_171.md`;
  `AT.Core/Resonance/Kuramoto/LockLatticeSimulator.cs`;
  `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_171_Tests.cs` (4/4 ✅, bit-reproducible).
- **Next open question.** If the Tier-1 board shows A at ≥ 5σ, that hysteresis is NOT coming from
  the lock structure (the noiseless model reaches only 3.4σ) — the imported nonlinearity would be
  doing more work than the lock law; if it shows nothing at g ≤ 1, NP_171 has already explained why.

## Retention Null Audit — H2's signature is generic (2026-09-11)

ResearchY-NP_172 (COMPLETE): the null test of NP_170's **H2** (retention). Question: can a
two-time-scale ring-down (R = τ_slow/τ_fast ≥ 10) arise from models with **no AT barrier at all**?
Five deterministic models, charged into the locked configuration and ringed down with no drive:
da_i/dτ = (−γ_i + iω_i)a_i + i·g·(Σ w a)/deg − β|a_i|²a_i — damping, a **reactive** coupling and
saturating damping only; no topological charge, no one-way term, no memory, no noise.

- **Verdict: REFUTED — the signature is generic, not AT-specific.**
  - The generic **strongly nonlinear ring** reaches **R = 10.248 ≥ 10** and PASSES H2 with no barrier.
  - The **D96 lock lattice and the random lattice give R = 6.358 — identical to four significant
    digits** (τ_fast 15.398 vs 13.665, τ_slow 97.901 vs 86.888), and *neither* passes R ≥ 10 at the
    reference parameters (Q = 100, β = 0.05, g = 1.7). The AT topology contributes nothing to R.
  - Model table (τ_fast | τ_slow | R): D96 15.398 | 97.901 | 6.358 · random 13.665 | 86.888 | 6.358 ·
    high-Q linear ring 979.014 | 1039.210 | 1.061 (τ-grid floor = single exponential) · weakly
    nonlinear 29.683 | 110.311 | 3.716 · strongly nonlinear 0.779 | 7.987 | 10.248.
- **DERIVED closed form** (verified against its own ODE to < 1e−6 and against a coherent-ring
  simulation to 8.5e−4): for a coherent uniform-amplitude charge in a uniform-Q ring,
  **E(τ) = u₀·e^(−2γτ)/(1 + (β·u₀/γ)(1 − e^(−2γτ)))** — so (i) the late-time rate is 2γ for *every*
  β (τ_slow → 1/(2γ)), (ii) the early decay is accelerated by βu₀, (iii) the whole shape depends only
  on the single dimensionless ratio **β·u₀/γ** — *topology does not appear in the formula*. With β = 0
  the decay is single-exponential **exactly**, at any Q.
- **β sweep** (canonical lattice): R = 1.06 (β = 0) → 2.927 (0.005) → 6.358 (0.05) → **13.811 (0.25,
  PASS)** → 10.248 (0.5). R crosses 10 once β·u₀/γ ≳ 50 and then *falls back* because τ_fast ≈ 0.78
  leaves the envelope/sampling resolution (window 10, sample 2) — the fitted R is a **lower bound**
  once τ_fast is unresolved, a limitation that applies to NP_170's R4 **as written**.
- **Q-contrast probe** (the second candidate mechanism; linear, 100× spread γ ∈ [0.0005, 0.05], both
  topologies): R at the grid floor with **ΔBIC negative** (−10.1 ring, −9.6 random) — the single
  exponential is *preferred*. The reactive coupling keeps energy equipartitioned, so the decay follows
  the mean damping and realistic mode-dependent loss supplies **no** retention signature.
- **Consequence for NP_170.** NP_171 left R4 retention as the *only* remaining discriminating test.
  Its signature is now shown to be generic: a positive R4 on hardware would demonstrate that the
  board's oscillators **saturate**, not that the lock law or the one-way barrier works. R4 must be
  scored against the degeneracy-matched control (T_015) **and** a **saturation-matched** control (same
  β·u₀/γ), and must resolve τ_fast. The AT-120 barrier analogy stays an analogy — now for a
  quantified reason.
- **Classification.** Closed form + τ_slow → 1/(2γ) + β = 0 exactness **DERIVED**; D96's signature
  reproduced by a random non-AT lattice (R identical to 4 digits) **CORRESPONDENCE** (non-
  discriminating); the ~30% deviation of both lattices from the coherent closed form is the D96
  spectrum's dephasing; "two-time-scale retention requires the AT barrier / is AT-specific"
  **REFUTED**; H2's discriminating expectation **REFUTED**; "a Q contrast supplies the signature"
  **REFUTED**. No canonical claim changed; no reclassification (D_040 registry untouched); no new
  primitive.
- **Files.** `Docs/ResearchY/NP_NewPhysics/ResearchY-NP_172.md`;
  `AT.Core/Resonance/Kuramoto/RetentionNullSimulator.cs` (reuses the NP_171 lattice builders);
  `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_172_Tests.cs` (5/5 ✅, deterministic).
- **Next open question.** With H1 predicted to fail on hysteresis (NP_171) and H2's retention signature
  shown generic (NP_172), **both** of NP_170's discriminating criteria are now answered in the negative
  *in the model*. Is any part of the lock-lattice program distinguishable from generic oscillator
  physics at all — and if not, what would the Tier-1 board be for?

## Discriminator Audit — what separates D96 from its controls, and does it mean anything (2026-09-11)

ResearchY-NP_173 (COMPLETE): eight observables measured against five controls (random,
degeneracy-matched, saturation-matched, linear-ramp, detuned), 16 fixed seeds, g ∈ [0, 8] in 60
steps each way, plus the T_015/D_048 perturbation machinery for the spectral ones. Two stages:
**(1)** does D96 differ from EVERY control by ≥ 5σ? **(2)** is the difference reproduced by a
control sharing the property invoked to explain it? Only (1)+ (2) makes a discriminator.

- **Literal answer: YES — four of eight separate D96 from every control at ≥ 5σ.** Lock threshold
  (z = 12.2…128.4), mode ordering ρ (z = 6.5…11.8), near-gap participation (z = 5.2…9.8),
  attractor shift ΔA (z = 5.4…342.4). The other four do not: hysteresis area (the **detuned**
  control is *larger*: 0.0158 vs D96's 0.0123), locking sequence (detuned z = 1.9), spectral
  splitting (z = 2.2…4.4 against *every* control), retention (z = 0).
- **Measured table** (D96 | random | deg-matched | linear-ramp | detuned): g_c 1.682 ± 0.025 |
  0.8667 | 3.059 | 3.160 | 2.002 · A 0.01225 | 0 | 0.00813 | 0.00440 | 0.01583 · ρ 0.420 | 0
  (exactly) | 0.056 | −0.078 | −0.197 · sequence dispersion 0.0693 | 1.2e−16 | 0.0160 | 0.0098 |
  0.0523 · near-gap participation 0.285 | 0.0667 | 0.0956 | 0.0813 | 0.1065 · ΔA 50.5 ± 1.1 |
  **0 ± 0** | 36 ± 21 | 37.7 ± 17 | 35.3 ± 21 · Δλ₂/λ₂ 0.445 | 0.032 | 2.34 | 0.97 | 1.70 ·
  retention R 6.358 vs saturation-matched 6.358.
- **No — none of the four is evidence for the AT structure** (Stage 2):
  - **ΔA is a sparse-circulant CLASS property, not D96's**: a different sparse circulant of the same
    degree, C96(±1..±5,±11), gives ΔA = 47 ± 0 versus D96's 50.5 ± 1.1 and the random graph's
    **exact 0** (D_047's degeneracy-lock theorem from the lock-lattice side).
  - **The threshold is set by each lattice's own detuning AND mixing**: g_c/mean|ω−Ω| spans
    8.2…17.3 (63 %); with *identical* frequencies the random graph locks at half D96's coupling
    because its local field is well mixed rather than local. D96 is not extreme (controls span
    0.867…3.160, a factor 3.6).
  - **Mode ordering and near-gap participation read back D96's own frequency multiset** — the
    derived spectrum the hardware is built to have (the fundamental pair carries one of the largest
    detunings, ω₁ = 0.156 vs Ω = 0.851, and the largest detuning is captured last). An INPUT, not
    an outcome.
- **Output classification.** DERIVED: random ΔA ≡ 0 exactly; the β = 0 uniform-Q single-exponential
  baseline with R reproduced by the saturation-matched control. EMERGENT: the four separating
  observables (real and ≥5σ, but properties of the (spectrum, topology) pair). REFUTED: "one of
  these observables is evidence that the AT lock law is real", and consequently the program's claim
  to a discriminating measurement.
- **Where the program now stands.** H1 fails on hysteresis (NP_171, 3.4σ), H2's signature is
  generic (NP_172), and the four observables that do separate D96 are all reading back inputs
  (NP_173). The lock law's consequences are **generic exactly where they would be evidence** and
  **D96-specific exactly where they are not evidence**. A finding about the TEST — no canonical
  claim changed, no reclassification (D_040 untouched), no new primitive.
- **Files.** `Docs/ResearchY/NP_NewPhysics/ResearchY-NP_173.md`;
  `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_173_Tests.cs` (5/5 ✅, deterministic; reuses the NP_171
  simulator and `AdaptabilityAudit`).
- **Next open question.** Is any *falsifiable* claim left in the lock-lattice program that is not
  already generic — i.e. does NP_170 have a measurement whose outcome could distinguish the lock law
  from ordinary coupled-oscillator physics? If not, the honest step is to record the program as
  closed at the proposal stage rather than to build the board.

## Program Synthesis — no AT-specific observable remains; CLOSE the hardware program (2026-09-11)

ResearchY-NP_174 (COMPLETE): reviews NP_170–NP_173 and classifies **every** observable as
GENERIC / D96-SPECIFIC / AT-SPECIFIC. Success criterion = at least one AT-SPECIFIC observable.

- **RESULT: none. 18 audited observables/claims, zero AT-SPECIFIC.** Every one is GENERIC
  (reproducible without AT), D96-SPECIFIC (the canonical spectrum — an **input** the hardware is
  built to have), or already excluded/refuted (g* ≈ 0.31 is a synthetic-cohort number; the
  energy-storage framing was abandoned inside NP_170 itself).
- **The single candidate — the one-way barrier's only possible route — is structurally closed.**
  A one-way barrier must break reciprocity. Measured: max |w_ij − w_ji| = **0** for D96 and the
  random control, and the driven asymmetry is **1e−16** (machine zero) at 5τ and 15τ on both. The
  canonical chain supplies no antisymmetric coupling (NP_005's κ·sin(θ_B−θ_A) is antisymmetric,
  i.e. also reciprocal). The apparent nonlinear asymmetry (D96 1.1e−2, random 1.9e−3 at β = 0.05)
  shrinks **quadratically** with the drive (→1.1e−4 / 1.9e−5) — saturation, not transport.
- **NEW MEASUREMENT — H3 (untested until now) is a superposition identity.** The interference
  read-out gives **V = 1.0000 on the ideal ring AND on the real D96 ring** (arm imbalance 7.7e−15),
  because the probe pair (24, 72) is a **mirror pair** of the ring reflection that fixes the
  midpoint 48 — i.e. H0's mirror pairing. The gate V ≥ 0.90 cannot fail, so H3 confirms the build,
  not the law (GENERIC/tautological).
- **MODELLING DEFECT FOUND AND FIXED.** The reciprocity probe exposed a per-node degree
  normalization (÷d_i): D = diag(d_i) makes D⁻¹W symmetric **only** for uniform degree. The
  canonical ring's degree is uniformly 12, so **no C96 result moved**; the "random 12-regular"
  control (true mean degree 14.917) had acquired a spurious one-way character (asymmetry 0.071 →
  1e−16 after the fix). The divisor is now the lattice constant ⟨d⟩ = (Σ_ij w_ij)/N. Re-verified
  after the fix: D96 g_c = 1.695, random g_c = 0.867, retention R = 6.358 for D96 and the
  saturation-matched control — all unchanged.
- **DECISION: CLOSE** the lock-lattice physical-realizability program (NP_170 Tiers 1–3) at the
  proposal stage. As specified it has no measurement whose outcome could distinguish the lock law
  from ordinary coupled-oscillator physics: the tests that would be evidence are generic (threshold
  existence, hysteresis, retention, read-out), and the D96-specific observables are inputs, not
  outcomes. **This is not a change to canonical AT** — no claim, value, equation or registry entry
  changes (D_040 untouched); the Ch8 lock law stays a statistical regularity with domain-specific
  values (QG313).
- **ONE CONDITIONAL REVISE TRIGGER (recorded, not pursued).** A revised program becomes possible
  only if the theory derives a genuinely **non-reciprocal** coupling (w_ij ≠ w_ji) — new physics,
  not in the canonical chain — so the reopening test is a **theory** audit first. A weaker second
  path is a blinded, pre-registered **statistics** program on datasets (QG319's FP = 96.9 % /
  FN = 66.1 % is the obstacle to beat), which needs no board.
- **Files.** `Docs/ResearchY/NP_NewPhysics/ResearchY-NP_174.md`;
  `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_174_Tests.cs` (5/5 ✅, deterministic; reuses the NP_171/172
  simulators). Registry note: the NP_171 **index** row had been lost by an earlier insertion and was
  restored in this commit (the status table was intact).
- **Next open question.** Does any AT-native construction yield a non-reciprocal coupling? That is
  the single question that could reopen a hardware test of the lock law, and it is a theory
  question, not an experiment.

## Lock-Lattice Program — FORMALLY CLOSED (ResearchY-NP_175, 2026-09-11)

ResearchY-NP_175 (COMPLETE) is the closure audit: it consolidates NP_170–NP_174 from **completed
results only** (no new measurement) and formally closes the program. **NP_170 NOT REFUTED; NP_170
TEST PROGRAM CLOSED.** NP_170 now carries a front-page closure banner and its registry status reads
CLOSED (superseded by NP_175); no claim, number, criterion or boundary statement inside NP_170 was
altered, and no file was deleted.

- **Closure reason (one line each).** H1 (NP_171): sharp/narrow pass but hysteresis fails at
  3.4σ < 5σ, nothing locks at the board's nominal maximum (f(g = 1) = 0, K ≥ 10.29·ω₁), and a
  threshold is generic. H2 (NP_172): retention is generic — a barrier-free ring passes R = 10.248,
  D96 and a random lattice give R = 6.358 identically, the saturation-matched control reproduces D96
  exactly. Discriminators (NP_173): the four observables separating D96 from every control at ≥5σ are
  generic-driven or read back the input spectrum; hysteresis and retention fail outright. Synthesis
  (NP_174): H3 is a superposition identity (V = 1.0000 on both lattices) and the barrier's only
  possible observable — a non-reciprocal directional response — is structurally zero.
- **Classification (18 observables): 11 GENERIC · 5 D96-SPECIFIC (inputs) · 0 AT-SPECIFIC · 2
  excluded/refuted.** Decision rule applied: AT-SPECIFIC count = 0 → CLOSE.
- **ProgramStatus: CLOSE.**
- **Archive recommendation (applied as labels only).** (1) KEEP NP_170 and NP_171–NP_174 unchanged as
  the closed evidence record. (2) MARK SUPERSEDED: NP_170 status = CLOSED (superseded by NP_175) in the
  ResearchY index/status registries plus a front-page closure banner; no claim altered. (3) PRESERVE
  REPRODUCIBILITY: keep the deterministic harnesses (`LockLatticeSimulator.cs`,
  `RetentionNullSimulator.cs`) and the suites `Y_NP_171_Tests.cs` (4/4), `Y_NP_172_Tests.cs` (5/5),
  `Y_NP_173_Tests.cs` (5/5), `Y_NP_174_Tests.cs` (5/5), all passing with fixed seeds.
- **What stays open (explicitly NOT closed here).** (a) A **theory** audit: does any AT-native
  construction yield a non-reciprocal coupling (w_ij ≠ w_ji)? — the single structural prerequisite that
  could reopen a hardware test. (b) A **statistics** program: can the lock law beat QG319's classifier
  rates (FP 96.9 % / FN 66.1 %) on datasets under a blinded, pre-registered protocol? — no board
  needed. (c) Documentation: restate the lock law's descriptive status, and record H0/H3 as validity
  gates.
- **Unchanged by this closure:** canonical AT (no claim, value, equation or boundary statement), the
  D_040 classification registry, and the Ch8 lock law's status as a statistical regularity with
  domain-specific values (QG313) evidenced on datasets and synthetic cohorts (QG307–QG319).
- **Files.** `Docs/ResearchY/NP_NewPhysics/ResearchY-NP_175.md` (with FINAL VERDICT section).

## Gravity Source Audit — the source term is the actualization density (ResearchY-G_001, 2026-09-12)

New ResearchY group **G (Gravity Source)**, first investigation G_001: a five-candidate adjudication of
what actually sources gravity in AT. Verdict: **SOURCE = actualization density ρ** (the counting
measure) — and, for the attractive Newtonian sector, its standardised **deficit m = ρ̄ − ρ**.

- **Why ρ wins (and the sharpest new statement).** The conformal factor ρ^(2/d) of g = ρ^(2/d)η must be
  dimensionless, so the variable entering it must be dimensionless and local. ρ is both. GR's source is
  dimensionful and reaches curvature dimensions only through κ = 8πG/c⁴ (κ·T → [L⁻²]) — a coupling AT
  would have to import (G4-L12 BDG −2 NO MATCH). **AT's field equation needs no coupling at all**: with
  ρ dimensionless, [R] = L⁻² comes entirely from ρ's own derivatives. Tested: a(λρ) = a(ρ) exactly while
  R(λρ) = λ^(−2/d)·R(ρ).
- **CORRELATED — energy density.** T₀₀ = (ρ̄−ρ)v² is rank-identical to the deficit; the only conserved
  tensor built from the scalar geometry is G/κ (Lovelock; measured Bianchi residual 1e−12 vs a kinetic
  candidate's divergence 0.0209). The energy *reading* is HOSTED (QG89/NP_058). Same content, other units.
- **CORRELATED — spectral density.** It supplies the source's PARAMETERS (m₀ = occ₀/Σm = 0.042105,
  r₀ = ln span = 1.856691) and the MAGNITUDE of G (A = Σm·#g·occ₂ = 363,660, M_Pl = v·A³, G = 6.6476e−11
  within 0.40%). It is GLOBAL (no position index): flipping a profile leaves every spectral moment
  unchanged while the field reverses sign (−0.222222 vs +0.095238) → never the local field.
- **REFUTED — information density.** Permutation-invariant global functional: identical I = 0.106440
  nats with fields of opposite sign; and it is the *non-gravitating surplus* (ΩΛ = I_occ/ln K = 0.6839,
  NP_055–NP_064). The deficit gravitates; the surplus only describes (NP_065).
- **REFUTED — the curvature–density law R = F(ρ) as a source.** R is the OUTPUT of the law and is not
  injective in ρ: ρ = 1+0.5x² and ρ = 1+0.5x²+5x⁴ share R(0) = −1.333333. Inverting the law to source ρ
  is circular. (The law itself remains DERIVED, G4-G2, exact.)
- **Limits.** vacuum ρ = ρ̄ → a = R = 0 exactly; M_eff(12) = 0.078367 = 94.04% of m₀r₀/(dρ̄) = 0.083333;
  d ln|a|/d ln r = −1.9703 (target −2) and −1.1524 for the log deficit (target −1); ρ → 0 gives a
  curvature divergence ∝ ρ^(−2/3) with √(−g) = ρ → 0; μ = 1 is stationary.
- **Comparison.** g† = cH₀/(2π) = 1.0422e−10 m/s², break ratio √2 at g_bar = g†, deep limit
  g_obs → √(g_bar g†); α = 0 is the unique flat member (ratios 1.1748/1.4914/1.9022/3.1476/9.0900 for
  α = 0/0.25/0.5/1/2); the conformal sector has R = 0 at uniform ρ, so lensing/tensor waves remain the
  separate ψ sector (QG21/24/43/212).
- **Classification (two-level, no reclassification).** Source *variable* DERIVED (QG197/G4-G2/G4-O3/QG222);
  the attractive-deficit *reading* is a HYPOTHESIS (G4-ME0); its energy reading HOSTED (QG89); α = 0
  DERIVED given criticality (QG206) with scale-freeness BOUNDARY (AT-F1/NP_070); SI conversion of G
  BOUNDARY. The D_040 ClassificationRegistry is untouched.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_001.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_001_Tests.cs` (9/9 ✅),
  `Docs/ResearchY/Tests/Results/Y_G_001_Result.md`.
- **Next open questions (G_001 OP1–OP4).** The dynamical origin of ρ's profile (still OPEN, G4-RHO);
  why the *deficit* branch rather than the repulsive conserved flux (G4-RHO01); whether a LOCAL spectral
  functional exists; the ψ boundary.

## Density Control Audit — ρ changes at fixed energy; degeneracy is the free room (ResearchY-G_002, 2026-09-12)

G_002 asks the control question that G_001 leaves open: **can the actualization density ρ change
independently of mass-energy?** Answer: **YES — CONTROLLABLE**, and the reason is structural.

- **The structural reason.** Count conservation makes the **total deficit vanish identically**
  (`Σm = 0` exactly, QG194: with ρ̄ = 1/N and Σρ = 1). So no rearrangement of ρ can change the total
  mass-energy — "fixed total energy" is the *generic* case, and the question becomes which operations
  move ρ *inside* that constraint.
- **CONTROLLABLE.** (1) *Spectral organization*: the same occupancy multiset in a different order
  leaves E exactly fixed (L1 = 0.7583) while a monotone density and its reversal give a of **opposite
  sign at every interior probe** (max|Δa| = 0.685714) — the density-space analogue of NP_171's
  arrangement sensitivity (canonical g_c = 1.607 vs sorted 1.746, 9%). (3) *Degeneracy structure* —
  the mechanism: a redistribution **inside** degenerate multiplets leaves every per-multiplet total
  identical to 12 dp (spectrum and mode energies untouched) at ΔE = 0 exactly, and takes the field
  from **exactly zero** to max|a| = 0.603175. (4) *Survivor compression with the total held fixed*:
  ΔE = 0 exactly, L1 up to 5.1569. (5) *D96 vs random*: same N and same total — the degeneracy-free
  lattice is an **exact zero-field null** (uniform ρ ⇒ a = R = 0), D96 is not. (6) *D96³ vs D96*: same
  normalized total, **97.6%** of the cube's directions energy-free (863 924 / 884 736) versus 53.1%
  for D96.
- **The free room is exactly the degeneracy structure:** `Σ(m_i − 1) = N − A₀` = **51 of 96 for D96**,
  which equals the D_048 latent fraction **L = 0.53125 exactly**, and 0 for random. *New cross-link:*
  L is the fraction of **energy-free reconfiguration directions of the counting measure** — the D_048
  adaptability ceiling and the free room of ρ are the same number (D_047's release 0.80231 is the
  entropy of using them).
- **CORRELATED.** The same compression **without** fixing the total lowers E by 46.8/73.0/86.4%
  (k = 48/24/12) — ρ and E move together; and the control ρ → 3.7ρ moves E by exactly ×3.7 while **a is
  exactly invariant** (scale invariance, G_001) and R merely rescales by λ^(−2/d).
- **REFUTED.** *Phase coherence*: ρ = |ψ|² is phase-blind, so ρ, R and a are unchanged (L1 = 0.0000;
  Δa < 1e-9, ΔR < 1e-6) under a global phase shift and under a mirror-pair relative-phase flip, while
  the interference term 2 + 2cos(Δθ) still runs 4 → 2 → 0. Phase is a real observable that does not
  touch the density.
- **Consequence — G_001 OP1 is sharpened.** ρ's profile cannot be fixed by conservation: **51 of 96
  directions are unconstrained at fixed energy**. A dynamical principle is genuinely required (this
  turns G4-RHO's "dynamical origin of ρ: OPEN" into a counting statement). Also: a degeneracy-free
  (all-singleton) spectrum has *no* energy-free room — its density is forced uniform and its field
  vanishes identically.
- **Classification.** DERIVED: the CONTROLLABLE verdict, the N − A₀ count, the exact-ΔE rearrangements,
  free-room = L, the zero-field baseline, phase-blindness. BOUNDARY: ρ̄ = 1/N, the exponent 2/d, the
  cell-lattice definition. EMERGENT: the particular witness tilt (one member of a free family). **No
  reclassification** — the D_040 ClassificationRegistry is untouched and G_001's verdicts are used, not
  modified.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_002.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_002_Tests.cs` (9/9 ✅, 155 ms),
  `AT.Tests/Shared/DensityField.cs` (shared with G_001),
  `Docs/ResearchY/Tests/Results/Y_G_002_Result.md`.
- **Next open questions (G_002 OP1–OP4).** Which of the N − A₀ directions does the actualization flow
  select? Does any physical AT process realise the witness tilt (the D_047 lock-release mechanism is
  the natural candidate)? Can the ρ̄ = 1/N reference and the lattice definition be derived? Is a
  sourceless density reconfiguration observationally distinguishable from a mass redistribution?

## Gravity Magnitude Audit — the physical size of a Δρ, and the threshold lengths (ResearchY-G_003, 2026-09-12)

G_003 converts a G_002-class reconfiguration of ρ into physical units and answers the critical
threshold question. **Verdicts: MEASURABLE (potential/clock) · ASTROPHYSICAL ONLY (acceleration) ·
PRACTICALLY ZERO (phase, rescaling). Critical answer: YES to 10⁻¹² g, 10⁻⁹ g and 10⁻⁶ g at fixed
total energy.**

- **The conversion chain (canonical).** `g₀₀ = −ρ^(2/d)` (QG197) → `ΔΦ/c² = (1/d)Δln ρ` (QG21/QG187) →
  `Δa = −(c²/d)∇ln ρ` (G4-O3) → `ΔR_phys = ΔR_AT/L²`. Inverting `a = −(1/d)∇ln ρ` over one cell gives
  `Δln ρ = d·Δa_AT`, hence **`ΔΦ/c² = Δa_AT` (a pure number — NO length scale)**, `Δa_phys = c²Δa_AT/L`,
  `ΔR_phys = ΔR_AT/L²`, `ΔM_eq = Δa·L²/G`.
- **Anchor (canonical cross-check).** The Earth-surface vs GPS-orbit potential gives **45.74 μs/day**,
  matching QG187's 45.7 μs/day; in AT the same potential is a counting-measure contrast
  `Δln ρ = 1.588e-9`. The chain is anchored on an existing result, not asserted.
- **Magnitudes are LARGE, not small.** At 15 kpc: arrangement `Δa_AT = 0.685714` → `1.3315e-4 m/s² =
  1.358e-5 g` with `ΔΦ/c² = 0.686` (**a 68.6 % potential change**) and `ΔM_eq = 2.157e17 M_☉`
  (3.6e6 Milky Ways); degeneracy `0.603175` → 1.194e-5 g; D96-vs-random `0.333333` → 6.600e-6 g;
  D96³-vs-D96 `0.276596` → 5.477e-6 g; survivor compression `0.032121` → 6.360e-7 g; phase and
  rescaling exactly zero.
- **Structure asserted exactly:** `ΔΦ` is L-free while `Δa ∝ 1/L` (ratio a(1 kpc)/a(1 Mpc) = 1000) and
  `ΔR ∝ 1/L²` (halving L quadruples R). Rescaling changes R only by the overall `λ^(−2/d)` (deviation
  < 1e−6) → the observable geometry is untouched.
- **Threshold lengths at fixed total energy (the critical answer).** 10⁻¹² g for L < **9.54 Gpc**
  (guaranteed, weakest witness; arrangement 203.66 Gpc), 10⁻⁹ g for L < **9.54 Mpc**, 10⁻⁶ g for
  L < **9.54 kpc**; phase and rescaling never. The observable-universe radius (~14.3 Gpc) exceeds every
  10⁻¹² g window, so the practically-zero regime of the force channel lies **beyond the horizon**.
  The binding constraint is the physical scale L, **not** the energy budget (Σm = 0, QG194).
- **Detectability.** 10⁻¹² g = **0.0941 g†**, 10⁻⁹ g = **94.1 g†**, 10⁻⁶ g = **9.41e4 g†** with
  `g† = cH₀/(2π) = 1.04220e-10 m/s²` (itself 1.06e-11 g); the witnesses are **6.03e17×** above an optical
  clock's 10⁻¹⁸ floor.
- **Falsifiable requirement (sharpest number).** The whole observed galactic AT field corresponds to
  `Δln ρ = 1.6102e-6` over 15 kpc, so the G_002 witnesses are **3.746e5×** the observed ambient
  contrast: any *realised* fixed-energy reconfiguration must be **suppressed by ≥ 3.7e5** — the
  quantitative form of G_002 OP2 (no known process realises one).
- **Classification.** DERIVED: the conversion laws and anchors, the threshold lengths/nesting and
  exact-threshold identities, the exact zeros, the ambient calibration and suppression requirement.
  BOUNDARY: realising a lattice-level Δa_AT across one physical length L; the values of G and g†;
  ρ̄ = 1/N and 2/d. EMERGENT: per-case magnitudes. **No reclassification** — D_040 untouched.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_003.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_003_Tests.cs` (7/7 ✅, 252 ms),
  `AT.Tests/Shared/PhysicalUnits.cs` (new shared anchors), `Docs/ResearchY/Tests/Results/Y_G_003_Result.md`.
- **Next open questions (G_003 OP1–OP4).** What fixes the physical coherence length L? Why should a
  realisable reconfiguration be suppressed by ≥ 3.7e5? Is a Δln ρ ≲ 1.6e-6 superposition distinguishable
  from a mass redistribution? And the strong-field (ΔΦ/c² ≫ 1) translation is not derived.

## Gravity Calibration Audit — a_pred/a_obs at four scales, and why 10⁻⁶ g is not seen locally (ResearchY-G_004, 2026-09-12)

G_004 closes the G-series by asking whether `a_AT` can be **calibrated to measured gravity**, at four
scales, with **no free parameters** (no quantity fitted to the data it is compared with).
**Verdicts: CALIBRATED (Earth, Sun–Earth, Galaxy RAR) · CORRELATED (the RAR interpolating function) ·
REFUTED (the cluster modified-gravity channel, and a uniform cosmic gradient).**

| # | scale | a_obs [m/s²] | a_pred [m/s²] | **a_pred/a_obs** | fitted params | verdict |
|---|---|---|---|---|---|---|
| 1 | Earth surface | 9.820250 | 9.780965 | **0.99600** | M_⊕ | **CALIBRATED** |
| 2 | Sun–Earth (1 AU) | 5.930084e-3 | 5.906361e-3 | **0.99600** | GM_☉ | **CALIBRATED** |
| 3 | Galaxy RAR | 1.200e-10 | 1.04220e-10 = cH₀/2π | **0.86850** | **NONE** | **CALIBRATED** |
| 4 | Cluster (Coma) | 6.3650e-11 | 3.2944e-11 | **0.51759** | f_b = 0.15 | **REFUTED** (mod-gravity) |

- **Earth / Sun–Earth.** The residual IS the derived-`G` offset `G_AT/G_CODATA = 0.9959996` (0.40%,
  QG181). `GM_⊕/R_⊕² = 9.820250 m/s²` is the like-for-like comparator (the geophysical effective
  `g = 9.80665` is 0.13% below it — centrifugal + oblateness). Independent checks: GPS **+38.5 vs
  +38.6 μs/day** (0.26%, QG187; potential term 45.74 vs 45.7), perihelion **+42.98 ″/century** (QG103),
  PPN **γ = β = +1** via ψ (QG212).
- **Galaxy RAR — the parameter-free prediction.** `g† = cH₀/(2π)` = 1.04220e-10 m/s²: **0.86850** vs the
  unweighted literature mean (6 determinations 1.00–1.21e-10; scatter 6.5%, spread 17%) and **0.9226**
  vs the project's own combined `a₀/cH₀ = 0.1725`. `a₀` is *not* an input.
- **Cluster — the documented failure.** Coma (`R_500` = 1.48 Mpc, `M_500` = 1e15 M_☉) gives
  `a_obs = 6.49e-12 g = 0.61 g†`; from **baryons only** the AT/RAR law gives 1.93× **short** (MOND
  1.64× short). This restates the project's own **X063 / AT_ClusterMassAudit**: AT modified gravity is
  INSUFFICIENT at cluster scale; AT matches clusters only through the **deficit-as-mass** channel
  (a mass inventory == ΛCDM, ~85% dark, ratio 4–10×) whose dark fraction is **not derived** (X065).
- **Critical question — why is a 10⁻⁶ g effect not observed locally?** Because it is a **counterfactual**:
  (a) it needs `Δln ρ = 0.15151` over 15 kpc — a unit-amplitude whole-lattice reconfiguration; (b) the
  **realised** field is fixed by the observed RAR to `g† = 1.0627e-11 g`, **9.41e4× below** 10⁻⁶ g
  (G_003's requirement: ≥3.746e5 against the G_002 witnesses); (c) the ambient gradient is
  `d ln ρ/dr = d·g†/c² = 3.4788e-27 m⁻¹`; (d) a **uniform** cosmic gradient would give 1.06e-11 g
  *everywhere*, ~**104× above** the ~10⁻¹² m/s² ephemeris/LLR bound → **REFUTED**; (e) the resolution is
  the **point-mass reduction** — a point-like deficit gives `M_eff → const` and exactly `a = −G_AT·M/r²`
  (G4-ME22) with **no anomalous term**, because the flat-curve 1/r regime needs a scale-free **extended**
  deficit (G4-ME21) absent locally. Local AT signatures are only `G` at 0.40%, the GPS potential at
  0.26%, and the perihelion at <0.1% — none at 10⁻⁶ g.
- **Classification.** DERIVED: `G`, `g†`, the redshift/GPS law, the ψ-sector perihelion/PPN,
  `M_eff → const`, the four ratios, the local no-anomaly statement. BOUNDARY: the masses/radii/`H₀`
  anchors, the RAR interpolating form, the galactic deficit parameters, `Ω_b/Ω_m`, the cluster dark
  fraction. EMERGENT: the per-scale residuals. **No reclassification** — D_040 untouched; the cluster
  result is X063 restated in calibration-ratio form.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_004.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_004_Tests.cs` (7/7 ✅, 33 ms),
  `Docs/ResearchY/Tests/Results/Y_G_004_Result.md`; `AT.Tests/Shared/PhysicalUnits.cs` extended with the
  GM_⊕/GM_☉/AU/GPS and MOND-a₀ anchors.
- **Next open questions (G_004 OP1–OP4).** Is the 0.40% `G` gap real? Is `cH₀/(2π)` exactly the RAR
  scale (the combined determination prefers 1.13e-10)? Can AT derive the cluster dark fraction without
  importing Ω_DM? And what exactly suppresses the cosmic AT gradient inside a bound system?

## Control Realizability Audit — the large modes are SUPPRESSED, not forbidden (ResearchY-G_005, 2026-09-12)

G_005 answers the question G_003 left open: **why are the large fixed-energy gravity-control modes not
realised?** Four channels are tested — stability, entropy cost, conservation constraints, dynamical
accessibility — and every configuration is classified **ACCESSIBLE / SUPPRESSED / FORBIDDEN**.
**Verdict: SUPPRESSED (they violate nothing — they are simply never counted).**

| channel | probe | result |
|---|---|---|
| 1 stability | canonical `RhoDynamics.DiffuseStep(·, 0.2)` | witness tilt **contracts ~34×** in 200 steps (std 0.00815358 → 0.000241, ratio **0.0296**), H rises 4.291783 → 4.564079 toward ln 96 = 4.564348, total conserved to 12 dp; uniform is the **exact** fixed point (< 1e-15) → **OFF-ATTRACTOR** |
| 2 entropy cost | `ΔS = ln 96 − H(ρ)` | uniform H = **4.564348** (max); witness tilt `ΔS = 0.272565` ⇒ `e^−ΔS = 0.761424` (a factor 1.31); absolute max `ΔS = ln 96` ⇒ **1/96 = 0.010417** → **3.6e7× SHORT** of the required 3.746e5 |
| 3 conservation | count / structure / Planck | `Σρ = 1` and deficit mass `Σ(1/N−ρ) = 0` **exactly** for every operation (QG194); `A₀ = 45`, lock `0.80231` nats untouched, witness tilt block sums `L1 = 0`; `ρ_max = 1/l_P³ = 2.3687e104 m⁻³`; one-cell ceiling `Δln ρ ≤ ln 96` |
| 4 dynamical accessibility | canonical flow | `ρ_(k+1) = μρ_k` (**same μ per cell**) with `g_(k+1) = μ^(2/d)g_k` (residual < 1e-15) ⇒ **arrangement-neutral**; `a(λρ) = a(ρ)` < 1e-6 at λ = 10⁶; attractor erases arrangements (basin ≥ 0.9); **no internal drive** |

- **The mechanism is AT's own Poisson counting law** (`δ = 1/√⟨N⟩`, QG15/QG228/QG231) **read forward**:
  the observed contrast 1.6102e-6 gives **⟨N⟩ = 3.8569176e11**; then `P(Δ) = exp(−⟨N⟩Δ²/2)` makes the
  observed field **typical (P = 0.6065**, median 1.8959e-6), puts the 1 % ceiling at **4.8867e-6**, and
  reaches the required 3.746e5 suppression already at **8.1577e-6 (5.07× observed)**.
  Witness costs: `−ln P` = 1.99e8 (0.032121), 1.47e10 (0.276596), 2.14e10 (0.333333), 7.02e10
  (0.603175), 4.43e9 (0.15151 — probability 10^−1.9e9).
- **Verdicts.** **ACCESSIBLE**: the attractor/uniform state (zero field), the phase directions, and every
  fluctuation `Δ ≤ 4.8867e-6` **including the observed galactic field**. **SUPPRESSED**: all five G_003
  witnesses. **FORBIDDEN**: `Σρ ≠ 1` / `dM ≠ 0`; a changed `A₀`/mirror pairing without a
  symmetry-breaking agent (D_047); `ρ > 1/l_P³`; `Δ > ln 96` — none is a ρ-operation.
- **Consequence.** G_004's CALIBRATED verdict is now explained **dynamically** rather than merely
  observed: the gravity source is `ρ` (G_001), its controllable part is real but statistically
  inaccessible (G_002/G_005), and the observable magnitude is set by the counting law — nature exhibits
  the Poisson-natural part of the counting measure and the huge G_003 modes are an empty tail.
- **Classification.** DERIVED: the Poisson window, the entropy ceiling, the conservation invariants,
  arrangement-neutrality, the band partition. IMPORTED/EMERGENT inputs: the observed contrast, the
  anchors, NP_171's lock gate (`g_c = 1.607`, `K ≥ 10.29 ω₁`, `f(g=1) = 0`). **No reclassification** —
  D_040 untouched; no canonical claim or equation changes; no new primitive.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_005.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_005_Tests.cs` (8/8 ✅, ~0.3 s; group G total 40/40),
  `Docs/ResearchY/Tests/Results/Y_G_005_Result.md`; AT.App (Research News + Theory Book + TestGroup) and
  AT.Book (`gravity-control` calculation, theory object + audit `g005`).
- **Next open questions (G_005 OP1–OP4).** The 0.40% `G` gap and the 7.7–13% RAR offset are still
  unexplained. `L` is still a BOUNDARY. Could a *driven* mode (not a spontaneous one) be realised in a
  laboratory — the only route that could falsify the suppression claim? And the diffusive contraction
  rate and the Poisson counting window, though both rooted in the counting measure, are not yet derived
  from a single principle.

## Suppression Mechanism Audit — the term is the relaxation operator, and the 34× is derived (ResearchY-G_006, 2026-09-12)

G_006 names the term G_005 left unnamed (the "off-attractor contraction") by measuring four quantities —
contraction rate, attractor basin, entropy production, deficit evolution — on three cases (D96, D96³,
Random) against four candidates: exponential decay, power-law decay, entropy driven, actualization driven.
**Verdict: the RELAXATION (coarse-graining) operator — DERIVED as the mechanism; power law, entropy and
branching all REFUTED.**

- **The operator.** `Dx = x + d(x_{i−1} − 2x_i + x_{i+1})` (Neumann boundaries, `d = 0.2`) is a **linear
  low-pass filter** on the eigenspace-occupancy index with the **exact** spectrum
  `μ_k = 1 − 2d(1 − cos(πk/N))`. Every Neumann cosine mode is an **exact eigenvector**: one step multiplies
  it by a constant (verified < 1e-12 for `k` = 1, 5, 48, 95). `μ₁ = 0.999785834991` (**1/e after 4669
  steps**; survives 200 steps at 0.958067), `μ₄₈ = 0.6` exactly (`λ₄₈ = 12`), `μ₉₅ = 0.200214165009`
  (1.99e-140 after 200 steps) — a **10¹⁴⁰ rate spread**.
- **The closed form** (no fitting): `r(m) = sqrt(Σ_{k≥1} w_k²μ_k^{2m} / Σ_{k≥1} w_k²)`, `w = DCT-II(ρ)`,
  mean excluded — reproduces direct iteration to **< 1e-9 at every horizon tested** (m = 1 … 50 000).
- **The 34× DERIVED:** `1/r(200) = 33.78 = exp(200 × 0.0175991) = exp(3.5198 nats)`. The ladder:
  2.27 (m = 1) · 6.15 (10) · 16.79 (50) · 25.16 (100) · **33.78 (200)** · 48.78 (500) · 65.13 (1000) ·
  162.10 (5000) · 2.49e6 (50 000) — so the LAW is DERIVED while the VALUE is **EMERGENT**.
- **Exponential, not a power law — with an honest illusion.** Over the accessible window 1…200 a power law
  fits the AGGREGATE better: `R² = 0.9945` (α = 0.5612) versus **0.7579** for one exponential, and the
  window rate (8.0787e-3) is **37.7×** the true asymptotic rate. The resolution is asymptotic: over
  m = 5000…50 000 the envelope is a **single exponential** with slope **−2.1418813131e-4** against
  `ln μ₁ = −2.1418794605e-4` (difference **1.85e-10**, R² = 1.0), and the instantaneous rate **converges**
  to the geometric floor `|ln μ₁| = 2.1419e-4` instead of decaying to zero. A power law has no eigennumber
  to converge to ⇒ power-law decay as the LAW is REFUTED, its finite-window appearance EMERGENT.
- **Entropy driven — REFUTED.** The operator is **exactly linear**
  (`|D(ax+by) − aDx − bDy| = 3.47e-18`), so it cannot read the entropy; and empirically
  `H + (N/2)·E → ln 96` with residual **9.7e-7** at m = 200 ⇒ the entropy rise is the second-order
  expansion of the Dirichlet-energy decay. **Entropy is downstream.**
- **Actualization/branching driven — REFUTED.** `ρ_(k+1) = μρ_k` multiplies every cell by the same μ
  (`a(λρ) = a(ρ)` to 5.56e-11 at λ = 10⁶; both density and metric static at criticality), so it contracts
  nothing; the attractor basin is 1 at **every** size (universal across size), so it cannot discriminate
  either; the tilt's field is removed only by relaxation (`max|a|` 0.6032 → 1.8746e-3 = **322×**), with
  `Σρ = 1` conserved at every step and the deficit L1/max decaying monotonically.
- **Cases at m = 200:** **D96 33.78** · **D96³ 7.33** (884 736 modes, A₀ = 20 812, free room 863 924) ·
  **Random: witness class EMPTY** (A₀ = 96, every multiplicity 1, free room `Σ(m_i − 1) = 0`), its extremal
  admissible arrangement (cell-scale alternation) annihilated at **55.13**.
- **Structural finding.** `μ_k` depends on the chain length `N` **only**: D96 and Random share the
  *identical* operator, while the cube's `μ₁ = 0.99999999999748` (`|ln μ₁| < 1e-11`) never decays. The
  mechanism is therefore **arrangement-selective (a low-pass filter), not lattice-selective** — no property
  of the D96 spectrum is doing the suppressing.
- **Consequence (closes G_005's open mechanism).** The G_002 free directions are *within-multiplet*
  rearrangements, i.e. **high-k** content — exactly what the filter removes fastest — while the smooth,
  scale-free deficit behind the observed galactic field (G4-ME21 one void per octave, effectively the
  k = 1 mode) sits on the **slowest** mode and survives (`μ₁²⁰⁰ = 0.958`). **Suppression and observability
  are the two ends of the same filter.**
- **Classification.** DERIVED: the mechanism statement, the exact spectrum, the closed form, the asymptotic
  single-exponential tail, linearity and the downstream entropy identity, arrangement-vs-lattice
  selectivity. EMERGENT: the numeric 33.78 and the apparent power-law exponent. REFUTED: power-law decay as
  the law, entropy-driven and branching-driven suppression. **No reclassification** — D_040 untouched,
  G_005's verdicts explained rather than revised, no canonical claim/value/equation changes, no new
  primitive.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_006.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_006_Tests.cs` (9/9 ✅, ~1 s; group G total 49/49),
  `Docs/ResearchY/Tests/Results/Y_G_006_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`suppression-mechanism` calculation, theory object + audit `g006`).
- **Next open questions (G_006 OP1–OP4).** Why `d = 0.2` (BOUNDARY — the law holds for any `d ∈ (0, ½)`,
  the value scales with it)? Could a shorter-horizon suppression be realised dynamically (2.27× at m = 1)?
  The exact map from *spatial* smoothness to *occupancy-index* smoothness assumed in the closing argument.
  And the cube's weaker 7.33× suggests multiplicity structure sets the filter's passband — untested.

## Suppression Origin Audit — the smoothing is DERIVED, the rate is BOUNDARY, and time is not the cause (ResearchY-G_007, 2026-09-12)

G_007 asks whether `DiffuseStep` (G_006's suppressing term) is **derived or imported**, tracing
**Difference → Actualization → ρ evolution → DiffuseStep**, and tests four replacements — nearest-neighbour
average, spectral cutoff, higher-order (biharmonic) diffusion, identity — measured against the suppression
factor, the G_006 34× and the G_003/G_005 witnesses. It also answers: **does TIME have anything to do with
this suppression?**

| link | canonical content | verified |
|---|---|---|
| Difference | counting measure, `Σρ = 1` (QG194) | `Σρ = 1.000000000000` |
| Actualization | `ρ_(k+1) = μρ_k`, count-conserving, **arrangement-neutral** | `|Δa|` at `10⁶ρ` = **0.00e+00** |
| ρ evolution | increments coarse-grain; **exact RG invariance** | `CoarseGrainedAlpha(α) = α` to 1e-12 (α = 0…2.5) |
| DiffuseStep | **Euler step of the Laplacian flow** on that index: `W = I − d·L` | tridiagonal (3), symmetric, rows = 1, semigroup to 1e-15 |

- **UNIQUENESS (DERIVED):** locality + constant coefficients + symmetry + row-sum conservation leave exactly
  the **one-parameter** family `W(b) = b·left + (1−2b)·a + b·right`; `W(0.2) ≡ DiffuseStep`.
  **Isotropy is forced, not assumed**: an anisotropic weight leaks at reflecting boundaries
  (`Σρ − 1 = (l−r)(a₀ − a_N₋₁)`), and the canonical chain has no antisymmetric coupling (NP_174).
- **The admissible RANGE is DERIVED by `ρ ≥ 0`:** the update is a convex combination iff `0 ≤ d ≤ ½`
  (min ρ = +1.04e-2 at d = 0.2 and ½; **−8.96e-2 at d = 0.6**); `|μ_k| ≤ 1`, with `μ₉₅ = −0.999465` at
  d = ½ — the fastest mode **oscillates** instead of decaying. Selectivity `|μ₉₅|/|μ₁|`:
  **0.800 · 0.600 · 0.200 · 0.000268 (d = ¼, maximal) · 0.200 · 0.600 · 1.000000 (d = ½, ZERO)**.
- **SENSITIVITY IS IN THE RATE:** m = 200 factors 16.70 / 25.12 / **33.78** / 36.74 / 39.43 / 44.34 / **2.01**
  for d = 0.05…0.50, but at fixed `T = m·d = 40` they are **33.75 / 33.76 / 33.76 / 33.78 / 33.81** — a
  **0.17 % spread across a 20× range of d**. `T` is the physical control parameter; `(d = 0.2, m = 200)` is a
  **BOUNDARY** representative of `T = 40`.
- **The four replacements:** the **nearest-neighbour average IS `d = ½`** (verified to 1e-15) and collapses
  the mechanism (**2.01**; the worst mode keeps 90 % after 200 steps); the **spectral cutoff** is
  **non-local** (delta → 96 cells in one step vs 3) and **non-positive** (min ρ = −4.52e-2); the
  **biharmonic** is a 5-point next-nearest stencil, non-positive (min ρ = −1.46e-2) and **unstable beyond
  κ = 1/16** (weaker at 7.15); the **identity** gives 1.00 and contradicts G_004's ≥ 3.746e5.
- **Witness dichotomy** (witness vs observed smooth mode): **32.4 HOLDS** (DiffuseStep) · 1.81 FAILS
  (d = ½) · 7.1 (biharmonic) · 1.0 REFUTED (identity) — above 8 for **every** `0 < d < ½`.
- **TIME — REFUTED as the cause.** The branching flow is **diagonal** on the arrangement (support 1 vs 3:
  the relaxation mixes *adjacent levels*) and leaves the normalised profile and field **exactly invariant**
  for any μ over any duration (`|Δa|` at `2¹⁰⁰⁰ρ` = 0.0); the factor carries no rate (scale-free to 2.34e-13);
  density and metric are static at criticality. **`m` is a coarse-graining horizon** (≈729 steps to reach
  3.746e5 by relaxation alone), **not a duration**; the only temporal readings require identifying one
  relaxation step with one generation — an extra assumption, and even then μ cancels.
- **Verdicts.** DERIVED: the form, uniqueness up to one scalar, the admissible range, the qualitative
  verdict for all `0 < d < ½`. BOUNDARY: `d = 0.2`, `m = 200`, hence 34 (`T = m·d = 40`) and the index
  identification inherited from G_005/G_006. REFUTED: "DiffuseStep is imported/arbitrary"; the
  cutoff/biharmonic/identity as canonical operators; the nearest-neighbour average as an equivalent
  suppressor; **time as the cause**.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_007.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_007_Tests.cs` (9/9 ✅, ~1 s; group G total 58/58),
  `Docs/ResearchY/Tests/Results/Y_G_007_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`suppression-origin` calculation, theory object + audit `g007`).
- **Next open questions (G_007 OP1–OP5).** What fixes `d = 0.2` / `T = 40`? What fixes the step count
  (`m ≈ 200`)? Is the occupancy-index ↔ octave-chain identification exact or itself BOUNDARY? Do
  biharmonic-like deformations appear anywhere in the canonical chain (none found)? And is the 0.17 %
  `T`-invariance exact in the continuum limit?

## Controlled Suppression Audit — nothing holds a high-Δρ state (ResearchY-G_008, 2026-09-12)

G_008 asks the engineer's question: can any **allowed** configuration (`Σρ = 1`, `ρ ≥ 0`, no symmetry
breaking, no non-reciprocal coupling) maintain a high-Δρ state against the relaxation suppression? Four
classes (stationary, driven, periodic, boundary-supported) × three measures (suppression factor, lifetime,
required drive power).

**Answer: nothing persists on its own.** Undriven high-k = SUPPRESSED; driven = STABLE but the witness must
be **re-created**; periodic = SUPPRESSED; boundary support = smooth-only and capped.

| k | 1 | 5 | 12 | 24 | 48 | 72 | 95 |
|---|---|---|---|---|---|---|---|
| μ_k | 0.99978583 | 0.99465733 | 0.96955181 | 0.88284271 | 0.60000000 | 0.31715729 | 0.20021417 |
| lifetime τ_k (steps) | **4668.80** | 186.67 | 32.34 | 8.03 | 1.96 | 0.87 | **0.622** |
| amplitude after 200 steps | **0.9581** | 0.3425 | 2.06e-3 | 1.50e-11 | 4.27e-45 | 1.80e-100 | **1.99e-140** |
| gain `1/(1 − μ_k)` | **4669.30** | 187.17 | 32.84 | 8.54 | 2.50 | 1.46 | **1.2503** |

- **Stationary (undriven):** `μ₀ = 1` is the **unique** unit eigenvalue (`ker(I − W) = span(uniform)`,
  1e-15) ⇒ the only undriven stationary profile is uniform. Undriven high-k **SUPPRESSED**, undriven smooth
  **METASTABLE** — the observed galactic situation.
- **Driven:** `ρ* = c·v_k/(1 − μ_k)` **exact** (iterated 0.008535533906 vs analytic 0.008535533906, 1.2e-15);
  requires `Σs = 0` exactly; the witness-driven state is allowed (`ρ_min = 0.0025`, `Σρ = 1.000000000000`,
  reproduces the tilt to 1.9e-15) ⇒ **STABLE**.
- **Price tag:** per unit peak-to-peak contrast **1566×**, per unit L1 contrast **5354×**, pure-mode
  `(1 − μ₉₅)/(1 − μ₁)` = **3734.4**. The highest mode must be injected at **0.7998 of its amplitude every
  step** (L1 = 0.4868 ≈ 49 % of the whole count); the lowest needs only 2.1417e-4. **The witness is
  re-created, not maintained.**
- **Mode matching:** `ρ* = (I − W)⁻¹s` is a filtered **copy** of the drive — a smooth-only drive holds
  **exactly zero** high-k content (1e-12) while the witness is 78.2 % high-k (its hold-drive 98.3 %).
- **Periodic:** `sup_ω \|H_k\|` = the **DC gain** for every k, at `ω = 0` (4669.2968 / 187.1724 / 8.5355 /
  2.5000 / 1.2503); Nyquist strictly worse (`1/(1 + μ)`); no amplifying band ⇒ **SUPPRESSED**.
- **Boundary:** an edge dipole holds a **smooth ramp** (high-k share **1.42e-6**, k = 1 share 0.9240) with gain
  **120.0**, but `ρ ≥ 0` caps the drive at 1.289e-4 ⇒ maximum contrast **0.0309 (3.09 % of the count)**.
- **Goal answer:** no gravity-control state persists without a mode-matched structured external agent, which
  the canonical chain does not supply (the branching flow is arrangement-neutral; NP_171's gate is imported;
  NP_174 closes self-amplification). G_005's verdict is sharpened from "not realised" to **"not maintainable"**.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_008.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_008_Tests.cs` (8/8 ✅, ~0.2 s; group G total 66/66),
  `Docs/ResearchY/Tests/Results/Y_G_008_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`controlled-suppression` calculation, theory object + audit `g008`).
- **Next open questions (G_008 OP1–OP4).** What physical process could supply a mode-matched drive at 0.7998
  per step? Can a driven smooth state at the 3.1 % contrast cap be distinguished from the ambient galactic
  field (observed contrast 1.6102e-6)? Is the boundary-dipole Green's function the optimal edge drive? And
  does the 4669-step metastable lifetime survive the G_007 OP3 index identification?

## Clock Rate Audit — AT ≡ GR at first order, ±½ at second, and a 0.33 % galactic cross-check (ResearchY-G_009, 2026-09-12)

G_009 takes `g₀₀ = −ρ^(2/d)` (QG197) to the clock: `dτ/dt = √(−g₀₀) = ρ^(1/d)`, hence
`(1/d)Δlnρ = ΔΦ/c²` (QG21/QG187), and computes four cases against GR.

| case | ΔΦ/c² | rate dev | clock | comparator |
|---|---|---|---|---|
| Earth surface vs ∞ | −6.9613e-10 | −6.9613e-10 | **−60.145 μs/day** | −60.15 measured; AT ≡ GR to double precision |
| GPS orbit (grav.) | 5.2940e-10 | 5.2940e-10 | **+45.740 μs/day** | 45.7 (QG187); AT Δlnρ = 1.5882e-9 |
| GPS (SR, imported) | −8.3365e-11 | — | −7.203 μs/day | −7.2 |
| GPS total | 4.4603e-10 | 4.4603e-10 | **+38.537 μs/day** | 38.6 (0.9984) — CORRELATED (SR imported) |
| **Galactic field** | 5.367333e-7 | 5.367333e-7 | **46.374 ms/day** | `v²/c²` = 5.385226e-7 → **0.99668**; equivalent v = **219.63 km/s** |
| arrangement | 0.228571 | 0.228571 | **19 748.6 s/day** | 2.29e17× the clock floor, at `Σm = 0` |
| degeneracy / D96-vs-random / D96³ / compression | 0.201058 / 0.111111 / 0.092199 / 0.010707 | — | 17 371 / 9 600 / 7 966 / 925 s/day | unchanged ranking |
| realised band (G_005 ceiling) | ≤1.629e-6 | — | 0.1407 s/day | 3.03× the observed level |

- **The law is DERIVED**, and it is scale-free: `ρ → λρ` moves every clock by `λ^(1/d)` with a **relative**
  change of exactly 0 — only *ratios* of ρ are physical. The phase directions change nothing.
- **AT vs GR:** `AT = exp(x) = 1 + x + x²/2` against `GR = √(1+2x) = 1 + x − x²/2`; at x = 1e-5 the
  coefficients are **+0.500001 / −0.499995** and the difference is `x² = 1.0000e-10`. First order
  **IDENTICAL**; second order **opposite sign** (AT clocks run slightly fast), giving in situ
  **4.846e-19** (Earth) and **2.803e-19** (GPS) — **2–4× BELOW the 1e-18 optical-clock floor**.
- **NEW cross-check:** the G_003 ambient contrast (calibrated from `g† = cH₀/(2π)`, nothing fitted) converts
  to a galactic potential depth agreeing with the rotation-curve `v²/c²` (220 km/s) to **0.33 %**
  (equivalent v = **219.63 km/s**, 46 ms/day across the disk, 5.37e11× the clock floor).
- **Critical question:** **YES in the theory** — `Σm = 0` exactly yet up to **22.86 %** (19 749 s/day,
  2.3e17× the clock floor), because the rate follows the *arrangement* — **NO physically**: G_005 caps
  realised contrasts at 4.8867e-6 and G_008 gives a 0.622-step lifetime with 0.7998 injected per step. What
  is realised (46.4 ms/day) is exactly the depth GR predicts, so **no new clock effect**; locally AT = GR
  with the derived `G` (a common factor that cancels in any rate ratio).
- **Verdicts.** DERIVED: the law, the identity, the Earth/GPS magnitudes, the galactic cross-check, the
  redistribution rate changes. CORRELATED: the GPS total (SR) and the ±½ second-order split. REFUTED: a
  realised sustained clock effect from a ρ rearrangement; the phase directions; a uniform rescaling.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_009.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_009_Tests.cs` (8/8 ✅, ~0.06 s; group G total 74/74),
  `Docs/ResearchY/Tests/Results/Y_G_009_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`clock-rate` calculation, theory object + audit `g009`).
- **Next open questions (G_009 OP1–OP4).** An optical-clock comparison at **1e-19** resolution (2–4× beyond
  today's best) would resolve the sign of the second-order coefficient — the only *local* clock test that
  distinguishes AT from GR. Can pulsar timing across the disk constrain the ambient contrast independently of
  the rotation curve? Does the 0.33 % agreement tighten with a different `a₀` anchor? And G_007's index
  identification is inherited through G_008's lifetimes.

## Time Control Feasibility Audit — every clock target is arithmetic-feasible, none is physics-feasible
  (ResearchY-G_010, 2026-09-12)

G_010 closes the group's practical question. The clock law `dτ/dt = ρ^(1/d)` (G_009) inverts to
**`Δlnρ = 3f`** for a fractional rate shift `f` per day, and the G_008 law prices the drive at
**`(1 − μ_k)Δlnρ` per step**. The equivalent well is `c√f`.

- **The four targets.** 1 ns/day → `Δlnρ = 3.472222e-14`, drive **7.436285e-18**/step, 7.11e-9 of the
  band, a **32.25 m/s** well. 1 µs/day → 3.472222e-11, 7.436285e-15, 7.11e-6, **1019.91 m/s**. 1 ms/day →
  3.472222e-8, 7.436285e-12, 7.11e-3, **32.25 km/s**. 1 s/day → 3.472222e-5, 7.436285e-9, **7.105× the
  band**, **1019.91 km/s**.
- **The band is the ceiling.** G_005's Poisson cap `Δlnρ ≤ 4.8867e-6` is **0.140737 s/day** (a 382.62 km/s
  well), and the **observed** galactic field (1.6102e-6 = 0.046373 s/day, a 219.63 km/s well) already uses
  **33 %** of it. The Earth's surface is a 7.91 km/s well, so 1 ms/day is 16.63× deeper than the Earth.
- **Positivity never binds.** The steady state is a pure Neumann mode `ρ = ρ̄(1 + (Δlnρ/2)v_k)` with
  `Σρ = 1.0000000000` and `min ρ = 1.041667e-2`: the excursion/ρ̄ runs 1.74e-14 … 1.74e-5, so **only** the
  Poisson band and the missing driver constrain the targets. The driven recursion reproduces
  `c·v_k/(1 − μ_k)` to 1e-9 after 150 000 steps, and the drive must be **mode-matched to 1e-18**.
- **Power scaling.** `P ∝ f·(k/N)²` — linear in the target (7.436e-18 → 7.436e-9); quadratic in relative
  frequency (`cost(2)/cost(1) = 4.00`); **N⁻²** in lattice size at fixed wavelength (a 1024-site lattice is
  **113.7×** cheaper); cost per unit shift `d(1 − μ_k)` = **6.4250e-4** (k = 1) vs **2.3994** (k = 95),
  re-deriving G_008's 3734.4 selectivity.
- **The critical answer.** No G_002 configuration reaches 1 ns/day, 1 µs/day or 1 ms/day **without an
  external mode-matched driver** — and the canonical chain supplies none (branching is arrangement-neutral,
  the attractor basin is 1.0, an undriven local state lives 0.622 steps, the Poisson cap is 4.8867e-6).
- **Verdicts.** PRACTICAL (all three small targets as *numbers*, given a driver) · ASTROPHYSICAL ONLY
  (0.0464 → 0.1407 s/day) · REFUTED (1 s/day at 7.105× the band; and **any** sustained shift without an
  external driver). The clock channel is length-independent — only the drive's *power* scales with a
  physical `L`.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_010.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_010_Tests.cs` (8/8 ✅, ~0.5 s; group G total 82/82),
  `Docs/ResearchY/Tests/Results/Y_G_010_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`time-control` calculation, theory object + audit `g010`).
- **Next open questions (G_010 OP1–OP4).** Does any canonical process supply a mode-matched drive at
  1e-18 relative accuracy? Can a boundary-supported smooth profile (G_008's 0.0309 cap) be extended by
  external matter coupling? Would a 1 ms/day target be distinguishable from the Earth's own 60.1 µs/day
  well in a local experiment? And is the N⁻² scaling exploitable as a *small-device* argument for a
  laboratory test?

## Labor Rho Audit — the dynamics is bench-top, the clock is not (ResearchY-G_011b, 2026-09-12)

G_011b maps the canonical relaxation onto five laboratory operator families.

- **The five systems are two families.** Oscillator lattice, coupled modes and the D96 controls are the
  Neumann **chain** (`λ_max = 3.998929`, admissible `d ≤ 0.500134`, rate spread 3734.44); the resonator
  network is a nearest-neighbour **ring** (`λ_max = 4.0`, `d ≤ 0.5`, spread 934.11); graph diffusion on the
  D96 circulant would need `d ≤ 0.126284` and a hub graph `d ≤ 0.020833`. So the canonical `d = 0.2` is
  admissible for degree-2 lattices only (0.3999 of the chain bound, **1.5837× over** the circulant's,
  **9.6000× over** the hub's): the occupancy index is an **ordered chain**, not the D96 mutation ring.
- **The derived range IS the CFL bound:** `ρ ≥ 0 ⇔ |1 − dλ| ≤ 1 ⇔ d ≤ ½` — the counting measure's
  positivity and the explicit scheme's stability are the *same* inequality. `cos(πk(i+½)/N)` is an exact
  eigenvector of `DiffuseStep` (residual **2.7e-14**) and `cos(2πkj/N)` of the ring step (≤4.9e-15).
- **The drive is trivial:** 1 ns/day needs `7.436285e-18` of the held amplitude per step at k = 1 (1 ms/day
  `7.436285e-12`), the G_002 witness class `0.48675` (49 % of the count). The steady state is exact
  (`Σρ = 1` to 1e-12, excursion ≤ **1.74e-5** of ρ̄ — positivity never binds) and the driven recursion holds
  it to `< 1e-12` after 20 000 steps. **1 mJ held for 1 µs needs 7.4363e-15 W.** Kinematic room
  `ln 96 = 4.564348` ⇒ 131 453 s/day.
- **The gravity ladder** (`M/r = f·c²/G`, measured G): the Earth self-check reproduces `GM⊕/(R⊕c²)` =
  6.9613e-10 with `M/r = M⊕/R⊕ = 9.3740e17`; 1 kg at 1 m = `7.4262e-28` (**1.35e9× below** the 1e-18 clock
  floor); 1 kg moved 1 m at fixed total energy = `3.7131e-28` (**2.69e9× below**); the floor needs
  `1.3466e9 kg/m` (1.35 Mt/m); 1 ns/day `1.5586e13`; 1 ms/day `1.5586e19`; the G_005 band top `2.1935e21`
  (2340 Earths/m).
- **No metric coupling to borrow:** AT predicts exactly the Newtonian field for any real mass arrangement
  (G_004's 0.99600), so a bench pattern's fractional amplitude is a measured voltage ratio — **not** a clock
  shift. **PRACTICAL** (dynamics) · **ASTROPHYSICAL** (readouts) · **REFUTED** (bench metric effect).
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_011b.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_011b_Tests.cs` (7/7 ✅, ~0.1 s; group G total 98/98),
  `Docs/ResearchY/Tests/Results/Y_G_011b_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`labor-rho` calculation, theory object + audit `g011b`).
- **Next open questions (G_011b OP1–OP4).** Can a mode-matched bench source reach the 1e-18 relative
  tolerance G_010 requires? Is the `N⁻²` scaling exploitable as a small-device argument? Does the
  chain-vs-ring distinction constrain any physical realisation of the occupancy index? Can the ψ-sector
  (281× coherent-sum change at zero ρ-response) be read out as a non-metric AT observable?

## Rho Actuator Audit — nothing acts on ρ, and the vacancy is a source (ResearchY-G_011, 2026-09-12)

G_011 asks the control question directly: can any physical quantity *change* ρ? Criterion: `q` is an actuator
iff it is **independent of ρ**, determines ρ locally, conserves the count, costs no energy and adds no
primitive.

- **No candidate is an actuator.** Five are **functions of ρ**: energy density
  (`E = ⟨λ,ρ⟩` identical to 1e-12 at fixed eigenspace totals while ρ moves by `L1 = 0.6666667` and the field
  goes `0 → 0.603175`; non-injective, the fixed-E fibre contains the whole 51-dimensional degeneracy space;
  re-ordering the same multiset moves `E` by 1.5401766 = 12.8 %), spectral organization (the orthonormal
  DCT-II is a bijection, `‖CCᵀ − I‖ = 1.37e-14`, round trip 4.2e-16, with `DC = Σρ = 1` fixed ⇒ 95 free
  coordinates; witness dominant mode k = 94, `k ≥ 48` share 0.7965733; `μ_k` carries no ρ-dependence),
  degeneracy engineering (the 51-direction energy-free room *is* the free part of ρ), compression (a ρ → ρ
  map, its smooth difference surviving 200 steps 1.1207×) and information density (exactly
  permutation-invariant, `ΔKL = 0`, while ρ moves by 0.6583333 and the field grows to 1.0031746).
- **Two are independent of ρ and exactly ρ-inert:** phase coherence and synchronization. All four phase
  assignments (canonical `θ_j = 2πj/N`, a global shift, the mirror, fully locked) give `L1(ρ,|ψ|²) ≤ 2.5e-16`
  and `|Δa| < 1e-9`, while `|Σψ|` runs **0.0324196281 → 9.1171821879 (281.22×)** and Kuramoto `r` goes
  `8.0788382e-17 → 1.0`. The canonical grid — the 96th roots of unity, summing to zero — is the *maximally
  incoherent* state. The one degree of freedom outside ρ cannot touch ρ.
- **But ρ is completely controllable:** `s = (I − W)ρ*` is count-neutral (`Σs = 2.1e-17`) and **unique**,
  since `(I − W)` is diagonal in the DCT basis with every gain finite — **4669.296831218** (k = 1),
  **2.500000000** (k = 48), **1.250334722** (k = 95) — no unreachable direction, no zero-gain mode. The
  inverse reconstructs the target to **1.9e-15**, the driven recursion converges to `< 1e-12`, and the
  uniform state is the unique undriven fixed point. Price of the witness: `‖s‖₁ = 0.48675` per step.
- **Verdicts.** ACTUATOR: none of the seven — the only handle is the **imported** source `s`, the missing
  driver of G_008/G_010. CORRELATED: energy density, spectral organization, degeneracy engineering.
  REFUTED: phase coherence, synchronization, attractor compression, information density. (G_002's
  CONTROLLABLE verdicts concern the **operations** on ρ and stand unchanged — G_011 labels the **quantity**.)
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_011.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_011_Tests.cs` (9/9 ✅, ~0.1 s; group G total 98/98),
  `Docs/ResearchY/Tests/Results/Y_G_011_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`rho-actuator` calculation, theory object + audit `g011`).
- **Next open questions (G_011 OP1–OP4).** Can any external agent be mode-matched to the occupancy index?
  Is a non-Hermitian coupling allowed in an open system (NP_174 closes it canonically)? Does any other AT
  observable couple to the ψ-sector's 281× coherent sum? Is there an entropy argument forcing the 51 free
  directions into equiprobability?

## Local Rho Actuator Audit — the first ACTUATOR, and it is a memory (ResearchY-G_012, 2026-09-12)

G_012 tests five local processes against four requirements (local implementation, finite drive, survives
DiffuseStep, no imported primitive). **The group finally has an ACTUATOR — and it is marginal.**

- **The hold-drive is local:** `s = (I − W)ρ*` is a **three-point stencil** (perturbing `ρ_j` moves only
  `s_{j−1}, s_j, s_{j+1}`), count-neutral (`Σs = 2.1e-17`), finite (`‖s‖₁ = 0.48675` for the witness,
  `max|s| = 0.01866667`; `3.331453e-10` for the band top) and exact (any target held to `< 1e-12` over
  20 000 steps). G_008/G_010's "mode matching" was about *knowing* the target, not about the stencil.
- **ACTUATOR — the incremental local feedback `s = ρ − Wρ` freezes ANY configuration exactly:** the closed
  loop is the **IDENTITY**, so the witness tilt is held to **4.336809e-18** over 5000 steps, the uniform
  measure and the highest mode to `< 1e-15`, and a 1e-6 perturbation is retained **100.000 %** (8.674e-19).
  It is **MARGINAL** — all 95 closed-loop modes neutral — so it is a perfect **memory** with no restoring
  force: it can *hold* a configuration, never *create* one.
- **Three-point theorem:** for the count-conserving family `s = β(Wρ − ρ)` the closed-loop eigenvalues are
  `c_k = μ_k(1 + β) − β`, and `c_k = 1` for **every** k only at **β = −1** (95/95 neutral, against 0/95 at
  β = 0). The freezing feedback is the unique non-trivial member and it freezes all modes at once.
- **The restoring family `s = λ(ρ − ρ̄)` is limited to the smoothest mode:** the closed-loop spectrum is
  `μ_k + λ`, so stability requires `λ ∈ [−1.200214165, 2.141650094e-4]` and a fixed point carrying mode k needs
  `λ = 1 − μ_k` — only k = 1 fits (k = 2 needs 8.564307e-4; the k = 48 attempt at λ = 0.4 gives
  `μ_1 + λ = 1.3998 > 1`). Holding is exact at k = 1 while k = 2 decays at 6.422657e-4/step (τ = 1556.99),
  a mixture resolves onto its k = 1 part (2.4e-12), and **above threshold the smooth mode runs away** at
  1.000214165/step (saturation from a Poisson seed in 64 516 steps).
- **No local creation:** the gain of `(I − W)⁻¹` decreases with k (4669.2968 → 1.2503), giving
  `HighKShare(ρ*) ≤ 2.8666657e-7·D_high/w_1²`. Compact masks hold **smooth** profiles (block w = 2:
  6.630273e-3 → w = 64: 6.368861e-9; edge dipole 0.1888945; staggered global 3.281127e-4); the best
  **structured** ±1 mask reaches 0.6117676 (77 % of the witness's 0.7965733) but a mask is *prescribed data*.
  The **cellwise test** excludes the witness for state-dependent generators (spread 1.58e-3 … 9.5e-3 within
  groups of equal ρ, against `max|s| = 0.01866667`) while a pure mode is exactly linear (9.3e-15).
- **REFUTED:** mode injection (full support; 98.3 % high-k for the witness) and synchronization (a locally
  locked patch leaves ρ bit-identical, `L1 = 2.45e-16`, `max|Δa| < 1e-9`). **CORRELATED:** the driven D96
  lattice (the medium; responses 2.5, 5, 10, 20, 40, 120; undriven attractor uniform) and a NESS
  (`s = (I − W)ρ*` by definition).
- **Readouts:** Δτ/τ = Δlnρ/3 — band top 0.140737 s/day, a 3:1 contrast 31 640.03 s/day, 10:1 66 314.45 s/day.
- **Refinement note (not a reclassification):** G_010's "the canonical chain supplies no driver" **stands** —
  G_012 shows an **engineered** local feedback realises one, marginally and only as a memory; G_011 labels the
  **quantity** (a function of ρ → CORRELATED there), G_012 the **local generator**.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_012.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_012_Tests.cs` (10/10 ✅, ~0.5 s; group G total 108/108),
  `Docs/ResearchY/Tests/Results/Y_G_012_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`local-actuator` calculation, theory object + audit `g012`).
- **Next open questions (G_012 OP1–OP4).** Can a **nonlinear** local generator hold a multi-mode profile with
  an asymptotically stable fixed point? Does the marginal freeze admit a **noise-driven** version under G_005's
  Poisson fluctuations? Can the incremental feedback be built in the G_011b lattice families within a finite
  gain tolerance (the window is a single point)? And is there any **canonical** term whose closed-loop spectrum
  is the identity on the zero-sum subspace — does AT itself contain a memory channel?

## Physical Actuator Audit — a negative resistor, and accuracy is the whole problem (ResearchY-G_013, 2026-09-12)

G_013 asks which *device* realizes `s = (I − W)ρ*` locally (requirements: local, finite power, exact stencil,
maintains ρ*; measures: drive, stability, power, error).

- **The stencil in physical form:** `s_i = −d(ρ_{i−1} − 2ρ_i + ρ_{i+1})` — a **negative Laplacian**,
  anti-diffusion of strength `d = 0.2` on the nearest-neighbour chain (verified to **3.5e-18**) — and a
  **balanced pump-and-drain**: exactly **50.0000 %** of `‖s‖₁ = 0.48675` injects and **50.0000 %** extracts,
  with `max|s| = 0.01866667`, count-neutral and three-point local.
- **PHYSICAL:** the **feedback controller** (sense ρ and neighbours, compute (I − W)ρ, actuate; exact stencil,
  maintains ρ* to `< 1e-15` over 5000 steps) and **active diffusion cancellation** — the operator *is* a
  nearest-neighbour **negative conductance**, so an NIC realizes it element by element, half the elements
  sourcing and half sinking.
- **ANALOGUE:** the **oscillator lattice** with a **node-wise** gain (a flat gain is mode-independent, so the
  residual error is `|γ − (1 − μ_k)|`, minimax **0.3997858349905463** at γ = 0.4 against a required spread of
  **3734.437** — 1866.7× wrong on the smoothest mode) and **coupled resonators** (band-limited: a 16-mode bank
  leaves **96.4 %** of the witness uncompensated; shares at k ≤ 1/4/8/16/24/48 are 3.240559e-4 / 1.479430e-3 /
  4.833883e-3 / 3.626161e-2 / 8.417047e-2 / 2.035517e-1).
- **REFUTED:** **pump/loss networks** — the required source is balanced, but a pump/loss balance is a
  **scalar** condition whose fixed points are the single Neumann modes (G_012) and whose imbalance grows at the
  fastest mode's rate (a factor e in **125 steps** at 1 %).
- **The binding constraint is exactness:** with residual loop gain `(1 + ε)` the closed loop is
  `I + ε(I − W)`, so mode k evolves at `1 + ε(1 − μ_k)` (verified per step: ε = 1e-5 gives 1.000007998 on v₉₅).
  Hold times `1/(ε(1 − μ_k))` at ε = 1e-6…1e-2 are 1.25e6 / 125 033 / 12 503 / **1250** / 125 steps for the
  fastest mode against 5.0e9 / 4.7e8 / 4.7e7 / **4.669e6** / 4.669e5 for the smoothest — so **a 0.1 % device
  holds the smooth class for 4.67e6 steps but the witness class for only 1250**.
- **A one-step delay is tolerable:** roots `{1, μ_k − 1} ∈ [−0.7997858350, 0]`; a 1e-3 perturbation is
  retained (0.9986e-3 … 0.9995e-3 after 2000 steps) — the loop stays a marginal memory.
- **Power is not binding:** the diffusion produces ΔH = **0.2170247 nats** per step (witness), so Landauer
  costs 8.6295e-20 J per lattice per step (**8.63e-14 W** at 1 µs) and the free-energy rate 1.5192e-19 J
  (**1.52e-13 W**) — ten orders below a ~1 mW controller; the band-top **smooth** profile costs ΔH = **0.0**
  (thermodynamically free). Sensor resolution: `q ≤ 1.0416667e-10` count units for 1 % of ρ̄ over 1e6 steps.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_013.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_013_Tests.cs` (9/9 ✅, ~0.4 s; group G total 117/117),
  `Docs/ResearchY/Tests/Results/Y_G_013_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`physical-actuator` calculation, theory object + audit `g013`).
- **Next open questions (G_013 OP1–OP4).** Can a **self-referencing** controller (no externally supplied
  target) realize the stencil, or is the target always imported? What loop bandwidth does the one-step-delay
  result require once the lattice has a physical step time τ? Is there a **passive** network whose
  constitutive relation is exactly `s = −d Δρ` (a parametric pump)? And how does a real NIC's noise floor
  compare with the `q ≤ 1.04e-10` resolution requirement?

## Physical Rho Mapping Audit — what you measure is the counting density (ResearchY-G_014, 2026-09-12)

G_014 asks which **measurable laboratory quantity** corresponds to ρ. The four requirements (G_001 source law,
G_009 clock law, G_013 actuator, G_007 suppression) reduce to **four structural conditions**: positive,
normalised, **AFFINE** (κ = 1 — because the relaxation `W` and the G_013 stencil are *linear*) and **cellwise**.

- **The affine test is decisive.** For `q = ρ^κ` the flow error `‖F⁻¹WF(ρ) − Wρ‖` and the actuator error
  `‖(I − W)F(ρ) − F′(ρ)(I − W)ρ‖` are **BOTH EXACTLY ZERO at κ = 1** (κ = 0.5: actuator 3.229213; κ = 2: flow
  1.047221e-2, actuator 4.668155e-2; κ = 3: flow 1.724505e-2), and the recovered **clock factor is exactly 1/κ**.
- **PHYSICAL — the probability density:** `q = |ψ|²` with `ψ = √ρ e^{iθ}` (QG220) **is** ρ to
  **L1 = 2.484991379e-16**, so κ = 1 exactly: the source law (`max|a| = 0.6031746`), the clock law `Δln q/d`,
  the G_013 stencil `(I − W)q` and the **33.7781483×** suppression all hold identically — and it is the only
  candidate carrying the **ψ-sector**, so G_002/G_011's phase tests are meaningful on it.
- **PHYSICAL — the occupation density:** the counting face, also κ = 1, and its **own shot noise IS the
  theory's Poisson law**: `⟨N⟩ = 1.6102e-6⁻² = 3.856917553651e11` counts per cell gives
  `δ = 1/√⟨N⟩ = 1.610200e-6` — *exactly* the observed galactic contrast — a 1 % ceiling of **4.886722e-6** —
  *exactly* G_005's accessible band — and `P(observed) = 0.6065`. **The band is a property of the measurement,
  not an extra assumption.**
- **CORRELATED — the mode population:** the DCT of the reversal multiplies mode k by `(−1)^k`, so the **power
  spectrum is invariant** (`max |Δ|w_k|| = 4.1598669e-15`, energy ratio 1.0000000000000018) while the
  arrangements are physically **opposite** (`max|Δa| = 0.8864864874`, acceleration correlation ≈ 0.10). It
  reproduces the suppression and gives G_013's modal-gain view, but it is nonlocal: **the power spectrum is
  reversal-blind**.
- **CORRELATED — the energy density:** the D96 weight has a (numerically) **zero mode**, so `ε = λ·ρ` vanishes
  at that cell and `ln ε` is undefined; with a positive weight (`1 + λ/λ_max ∈ [1,2]`) the flow commutation
  defect is **3.7873408e-4 vs 2.0916667e-2 (1.81 %)** and the implied clock factor spreads over
  **[0.5689248, 1.1378497]**. Non-injective (fibre 94). **G_001's verdict stands.**
- **REFUTED — information density** (a *global*, permutation-invariant functional: `ΔKL = 0` under a roll while
  `L1 = 0.6583333` and the field moves 1.0031746) and **coherence density** (the **ψ-sector**: a **281.2241449×**
  coherent-sum change at `L1(ρ,|ψ|²) = 2.5e-16` — exactly ρ-inert).
- **Critical answer:** the first experimentally measurable ρ analogue is the **diagonal occupation
  (probability) density** `q_i` — site-resolved imaging, photon counting or mode-resolved population. Recipe:
  normalise the counts → `ln q` → `a = −(1/d)∇ln q` → `Δτ/τ = Δln q/d` → hold with `(I − W)q`. Readable floor
  `1/√⟨N⟩ = 1.6102e-6` = **46.374 ms/day** of clock depth (G_009's galactic cross-check). The residual gap is
  the **identification premise** — the metric coupling G_011b showed is not borrowed.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_014.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_014_Tests.cs` (9/9 ✅, ~0.1 s; group G total 126/126),
  `Docs/ResearchY/Tests/Results/Y_G_014_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`rho-mapping` calculation, theory object + audit `g014`).
- **Next open questions (G_014 OP1–OP4).** Which platform gives the best κ = 1 read (and what `⟨N⟩`)? Can a
  *complex-amplitude* measurement verify the 281× coherent-sum change at Δρ = 0? Can the energy channel be
  calibrated to κ = 1 by measuring the per-cell spectral weight? And is there a κ = 1 observable whose metric
  coupling is *derived* rather than assumed?

## Rho To Metric Audit — the laboratory reads the density but cannot move a clock (ResearchY-G_015, 2026-09-12)

- **Question.** Can a **laboratory q profile** produce any measurable metric effect? Cases: photon occupation,
  cavity modes, resonator lattice, oscillator lattice. Compute `Δτ`, `ΔΦ`, the equivalent gravity. Uses
  G_010, G_011b, G_014.
- **The audit only becomes well-posed once G_014 is used**, because the map is the **identity**
  (`κ = 1`, `L1(|ψ|², ρ) = 2.484991379e-16`). Two channels then separate:
  **ANALOGUE** — the density *contrast itself*, `Δτ/τ = Δln q/d`, is a measurable **ratio** needing no mass;
  **METRIC** — a *real* time dilation, whose only laboratory handle is mass-energy (G_011b: the coupling is
  not borrowed), so `m = E/c²`, `ΔΦ/c² = G m/(R c²)`, `a = G m/R²`, for which AT predicts **exactly** Newton
  (G_004's 0.99600 self-checks).
- **The clock ladder** (`f = Δτ/τ` ⇒ `Δlnρ = 3f` ⇒ `M/r = f c²/G`): the measurement floor `1e-18` needs
  `M/r = 1.3466e9 kg/m` — **13 466 tonnes within a centimetre** (`1.3466e7 kg`) — and `Δlnρ = 3.0e-18`;
  1 ns/day → `1.5586e13`; the observed galactic field (`5.3673e-7`) → `7.2276e20`; the G_005 band top
  (`1.6289e-6`) → `2.1935e21 kg/m`. Earth self-check reproduces G_004 (`9.3740e17 kg/m` for `6.9613e-10`).
- **The four cases** (`ΔΦ/c²` with `m = E/c²`): photon occupation (1 J = 5.034116567542709e18 photons at 1 µm
  in 5 mm) **1.6525e-42** (`a = 2.9705e-23`, 1.65e-24× the floor) · cavity modes (1 kJ in 6 cm, SRF,
  Q = 1e10) **1.3771e-40** (`2.0628e-22`, 1.38e-22×) · resonator lattice (1 mJ on 1 cm) **8.2627e-46**
  (`7.4262e-27`, 8.26e-28×) · oscillator lattice (1 nJ on 1 cm) **8.2627e-52** (`7.4262e-33`, 8.26e-34×).
  Ordering cavity modes > photon occupation > resonator lattice > oscillator lattice; **all REFUTED** and the
  best case is still **twenty-two orders** below the floor.
- **The strongest possible laboratory case** (no device beats an energy-density argument — the best densities
  in a 1 m ball, `V = 4.189 m³`): chemical 1e9 → 3.4611e-35 · capacitor 1e12 → 3.4611e-32 · magnetic 3e13 →
  1.0383e-30 · **nuclear scale 1e18 → 3.4611e-26 (3.46e-8× the floor, still 2.889e7× SHORT)** ·
  neutron-star core 1e34 → 3.4611e-10 (astrophysical). The floor needs `u = 2.889272332033454e25 J/m³`
  (2.889e7× nuclear); the band top needs `4.706335701649293e37 J/m³` (4706.3× a neutron-star core).
- **The measurable channel, priced:** the counting floor `1.6102e-6` (= `1/√⟨N⟩`, `⟨N⟩ = 3.856917553651e11` —
  exactly G_014's shot noise and G_009's galactic cross-check) is **0.046374 s/day** but needs `7.2276e18 kg`
  at 1 cm; G_002's 4.8:1 witness is **45 176.138 s/day** needing 1.18 Earth masses; 20:1 is
  **86 277.089 s/day** needing 2.25 Earth masses.
- **Critical answer:** **NO** in the metric sense — the strongest laboratory configuration is 3.4611e-26
  against a 1e-18 floor, 2.889e7× short, and the floor itself is not laboratory physics. **YES** in the
  analogue sense — `Δln q/d` is a κ = 1 observable (G_014). Verdicts: **MEASURABLE** = the analogue contrast
  channel (all four cases) · **ASTROPHYSICAL ONLY** = every metric effect that exists (galactic 0.0464 →
  band top 0.1407 s/day) · **REFUTED** = a laboratory q profile producing a metric effect. The laboratory can
  **measure** ρ (G_014) and **hold** a pattern (G_012/G_013), but it cannot make ρ pull on clocks.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_015.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_015_Tests.cs` (7/7 ✅, ~0.03 s; group G total 133/133),
  `Docs/ResearchY/Tests/Results/Y_G_015_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`rho-metric` calculation, theory object + audit `g015`).
- **Next open questions (G_015 OP1–OP5).** Is there a κ = 1 laboratory quantity whose metric coupling is
  *derived* rather than assumed? What is the smallest mass at which AT's prediction differs from Newton's
  `ΔΦ/c²`? Is there a *differential* analogue test separating AT from GR at equal energy? Can the G_005 band
  top be read astrophysically with an independently known energy density? And is there any non-mass-energy
  metric handle not bounded by nuclear density?

## Watch Ontology Audit — mass is a one-dimensional shadow of the density (ResearchY-G_016, 2026-09-12)

- **Question.** Is **mass-energy required to generate ρ?** Trace Difference → Actualization → ρ → metric →
  clocks; tests: ρ from primitives only, mass-energy from ρ, the converse, the minimal carrier. Candidates:
  occupation density, probability density, degeneracy distribution, survivor compression, actualization
  density. Output: **SOURCE / CARRIER / BOOKKEEPING**.
- **The AT-native chain (no energy appears in it):** Difference → distinguishability → the coupling lattice →
  { **CAPACITY** (spectrum λ, multiplicities m) , **OCCUPANCY** (ρ, counting measure, Σρ = 1, QG194) } →
  **E = ⟨λ,ρ⟩** → g₀₀ = −ρ^(2/d) → clocks dτ/dt = ρ^(1/d). ρ and λ are **siblings**; E is their **pairing**.
- **1. ρ from primitives only — DERIVED.** Positivity and Σρ = 1 need **only** distinguishability. A₀ = 45,
  histogram **{1:1, 2:42, 5:1, 6:1}**, free room **Σ(m−1) = 51 = N − A₀**, `Σλ = 1152` (a ρ-blind capacity
  invariant, λ₀ = 0), `Σρ = 1.0000000000000000`.
- **2. mass-energy from ρ — DERIVED, and it is a PAIRING.** `E = ⟨λ,ρ⟩` (QG180/QG181) evaluates the spectral
  weight **on** the occupancy. `E(uniform) = Σλ/N = 12.0` exactly; on the 95-dim affine set `{Σρ = 1}` it has
  **rank 1** and a **94-dim kernel = 51 (energy-free *by degeneracy*) + 43 (zero-net λ-mixing)**; it sees
  **1.0526 %** of ρ. Non-degeneracy rearrangements do move it: comonotone **13.540176608029563**
  (+**12.835 %**), anticomonotone **10.015359929915876**, reverse-witness 12.095189171364584.
- **3. does ρ require mass-energy? — REFUTED.** Move density between two cells of the **same multiplet**: λ is
  constant there, so `Σρ = 1` stays exact and `E` is exactly invariant while ρ moves (m = 6 multiplet,
  δ = 0.005/0.01: `|ΔE| ≤ 1.776e-15`, ratios 2.8462 / 49.0000). The **canonical witness is itself** such a
  move (`L1 = 0.6666666666666667`, `ΔE = 0` exactly, `max|a| = 0.6031746`). **The 51-dim free room is the
  proof.**
- **4. minimal carrier — DERIVED: the cellwise counting density.** Dimension ladder **95 → 44 (the
  per-multiplet totals lose exactly the 51-dim free room) → 1 (E loses 94)**: every coarser carrier destroys
  precisely the energy-free room. Verdicts: actualization density = **SOURCE** · `|ψ|²` = **CARRIER**
  (the identity, κ = 1) · occupation density = **CARRIER** · degeneracy distribution = **CORRELATED**
  (capacity; block-sum L1 = 0 to 1e-12) · survivor compression = **CORRELATED** (+4.248925e-3 / −2.959751e-1)
  · `E = ⟨λ,ρ⟩` = **BOOKKEEPING**.
- **Critical answer: YES.** The witness is allowed with `Σρ = 1` and `ΔE = 0`, yet carries a **20 : 1**
  contrast — clock ratio **2.7144176165949063**, separation **0.9985774245179969 = 86 277.089 s/day**,
  `max|a| = 3.7459607502174e5` × the observed contrast (= G_005's 3.746e5 requirement). The **carrier
  exists**; the **dynamics** (G_005/G_008), not the ontology, forbids realisation.
- **What this does to G_015:** the imported step was not "ρ needs energy" (false) but "the only laboratory
  handle on the metric is energy" (true) — now identified as the **rank-1 projection** of a 95-dim object.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_016.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_016_Tests.cs` (7/7 ✅, ~0.07 s; group G total 140/140),
  `Docs/ResearchY/Tests/Results/Y_G_016_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`watch-ontology` calculation, theory object + audit `g016`).
- **Next open questions (G_016 OP1–OP5).** A laboratory quantity that reads the **energy-free room**
  directly? Are the 51 within-multiplet directions a *calibration-free* reference for the clock law? Does the
  43-dim energy-neutral mixing have a distinguishable signature? What is the next spectral functional
  (⟨λ²,ρ⟩?) and is it measurable? Is the identification premise now "the lab density occupies the same 51-dim
  free room as ρ"?

## Mass Independence Audit — density is free of energy, not the reverse (ResearchY-G_016b, 2026-09-12)

- **Question.** Can two states have the **same energy but different ρ**, or the **same ρ but different
  energy**? Construct explicit examples; measure `ΔE`, `Δρ`, `Δτ`. Output: **INDEPENDENT / DEPENDENT /
  REFUTED**. **The three answers are not symmetric.**
- **INDEPENDENT — same E, different ρ.** The kernel of `ρ ↦ E` on `{Σρ = 1}` is **94-dimensional**,
  explicitly **51 (DEGENERACY — λ constant within a multiplet) + 43 (λ-MIXING — zero net over *distinct*
  λ)**. The canonical witness tilt is a pure within-multiplet move: **`ΔE = 0` exactly**,
  **`Δρ = L1 = 0.6666666666666667`**, contrast **20 : 1**, **`Δτ/τ = (1/d)ln20 = 0.9985774245179969` =
  86 277.089 s/day**. Pairwise m = 2 moves: δ = 0.002 → `Δρ = 0.004`, contrast 1.4752, `Δτ = 0.129609`;
  δ = 0.005 → `Δρ = 0.010`, contrast 2.8462, `Δτ = 0.348656`. The **43-dim λ-mixing room** is built
  explicitly on cells 94/92/90 (λ = 15.837372467014836, 15.790176186632262, 15.414213562373096) with
  `v = (1, −1.1255345008711273, 0.12553450087112727)`, `Σv = 0` and `⟨λ,v⟩ = 0` **exactly**; at scale 0.004 →
  `Δρ = 0.009004`, contrast **2.437501**, `Δτ/τ = 0.29699105` = **25 660.0263 s/day**.
- **DEPENDENT — E is a function of ρ.** One ρ, one E, so the independence is strictly **one-directional**
  (many ρ per E, never many E per ρ).
- **REFUTED — same ρ, different E as a state change.** The **same** uniform ρ = 1/96 gives **`E = 2K`
  exactly** for K = 1…6 (2, 4, 6, 8, 10, 12) because **`Σλ = 192K`** (192…1152), with A₀ = 49/47/45/47/45/45
  and `L1(ρ_K, ρ_6) = 0`. The only handle is the capacity K, which is a **BOUNDARY** input (G_007).
- **Phase sector.** Four phase assignments: `L1(|ψ|², ρ) < 2.5e-16`, **`ΔE = 0` and `Δτ = 0` exactly**,
  while `|Σψ|` moves 0.03241962809423954 → 9.117182187865382 (**281.22414487183346×**, exactly G_011/G_014).
- **Δτ at fixed energy is UNBOUNDED.** The family `fr = (f, (1−f)/(m−1))` (fr = 1 for m = 1) gives **exactly**
  `ΔE = 0`, `contrast = 5f/(1−f)`, `L1(f) = (2/96)[42|2f−1| + |5f−1| + |6f−1|]` and `Δτ/τ = T ⇔
  f = 1/(1 + 5e^{−3T})`. At `T = (1/d)ln20` the inversion returns **f = 0.8** — the canonical `TiltFractions`
  **is** the 20 : 1 solution. Since `L1 → 1.0625` and `ρ_min → 0` with `ΔE = 0` throughout, **no target clock
  separation is forbidden by energy conservation**; only positivity and the G_005 band bound it
  (`T = 4.8867e-6` needs `f = 0.1666687028016166`, contrast 1.0000146602074598).
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_016b.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_016b_Tests.cs` (7/7 ✅, ~0.07 s; group G total 147/147),
  `Docs/ResearchY/Tests/Results/Y_G_016b_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`mass-independence` calculation, theory object + audit `g016b`).
- **Next open questions (G_016b OP1–OP5).** The **diameter** of the fixed-E fibre (family gives 1.0625; the
  simplex bound is 2)? Can the 43-dim λ-mixing room be *measured* apart from the 51-dim degeneracy room? Is
  the factor 5 a D96 artefact of m = 6 (does it generalise to random / D96³)? Is there any AT-native quantity
  that is *not* a function of ρ? Does the unbounded family change G_010's drive accounting (priced per Δlnρ,
  not per `L1`)?

## Metric Coupling Audit — the laboratory has already run the test (ResearchY-G_017, 2026-09-12)

- **Question.** Does any **experimentally realizable |ψ|²** profile produce a **measurable clock shift**?
  Systems: optical cavity, resonator array, photon lattice, oscillator network. Measure `Δτ`, `ΔΦ`, the clock
  signal. Compare **AT, GR and observed limits**. Output: **DERIVED / BOUNDARY / REFUTED**.
- **Why this is not G_015.** G_015 priced a metric effect through mass-energy. G_017 takes the |ψ|² reading
  **literally for optical fields**, where the intensity *is* the measured field and its contrast is enormous —
  so the prediction becomes **O(1)** and can be confronted with real clock performance. That turns the
  identification premise from "unproven" into "experimentally excluded at bench scale".
- **Three readings, one law.** **AT-substrate** (ρ = the actualization density, G_014): `Δτ/τ = ΔΦ/c² =
  (1/d)Δlnρ`. **AT-naive** (a lab intensity profile *is* ρ, so `ρ ∝ I`): `Δτ/τ = (1/d)Δln I`. **GR** (stored
  energy `U`, `m = U/c²`): `ΔΦ/c² = G U/(R c⁴)`. The observable is a **fractional frequency ratio**, so the
  ceiling is a clock's fractional resolution (**1e-18**; 1e-12 crude).
- **DERIVED — the substrate reading is NOT refuted.** Earth's surface `Δlnρ/d = 2.320443e-10` = **2.320e8** ×
  the floor (G_004's 0.99600); the galactic field `5.367333e-7` = **0.046374 s/day** = **5.367e11** × the floor
  (G_009's 0.99668). The logarithmic form is derived (depths add where ratios multiply).
- **The four systems** (stored energies recomputed from F, Q, P, λ, lattice depth):

| system | contrast | AT-naive `Δτ/τ` | GR `ΔΦ/c²` | AT/GR | exclusion @1e-18 |
|---|---|---|---|---|---|
| optical cavity (F = 1e6, 1 W, 0.3 m; `U = 6.370605e-4 J`) | Gaussian 1e2 | **0.6666666667** | 3.509234e-47 | **1.900e46** | **6.667e17** |
| resonator array (Q = 1e7, 1 mW, 1550 nm; `U = 8.228698e-12 J`, `V = 3.723875e-18 m³`, `u = 2.209714e6 J/m³`, `R = 9.615433e-7 m`) | 1e4 | **3.0701134573** | 7.071071e-50 | **4.342e49** | **3.070e18** |
| photon lattice (Sr: `E_rec = 2.272842e-30 J` at 813 nm, 88 u, 100 `E_rec`, 300 a.u. → `I = 2.439413e8 W/m²`) | 1e3 | **2.3025850930** | — | large | **2.303e18** |
| oscillator network (Q = 1e6, 1 pW, 6 GHz; `U = 2.652582e-17 J`, `u = 2.652582e-8 J/m³`, `R = 6.203505e-4 m`) | 10 : 1 | **0.7675283643** | 3.533090e-58 | **2.17e57** | **7.675e17** |

- **The sharpest test — a Sr lattice clock inside its own standing wave.** It runs at `I = 2.439413e8 W/m²`
  with node/antinode contrast ≥ 1e3 and reads **1e-18 every day**, while AT-naive predicts a **230.26 %** shift
  across its own lattice → excluded by **2.303e18** by the very experiment that best tests it. Ladder
  1e2/1e3/1e4/1e6 → 1.53505673 / 2.30258509 / 3.07011346 / 4.60517019 (exclusions 1.5351e18 / 2.3026e18 /
  3.0701e18 / 4.6052e18).
- **Exclusion against every ceiling** (cavity / resonator / lattice / network): 1e-12 → 6.667e11 / 3.070e12 /
  2.303e12 / 7.675e11 · 1e-15 → 6.667e14 / 3.070e15 / 2.303e15 / 7.675e14 · **1e-18 → 6.667e17 / 3.070e18 /
  2.303e18 / 7.675e17** · 1e-19 → 6.667e18 / 3.070e19 / 2.303e19 / 7.675e18. **No ceiling exists at which any
  realizable profile survives.**
- **Why structural, not engineering:** the law is **logarithmic**, so `Δτ/τ = (1/3)ln(contrast)` is **O(1) for
  every realizable contrast** (> 10) — no tuning makes it small.
- **Critical answer: NO.** No experimentally realizable |ψ|² produces a measurable AT metric clock shift; the
  effect that exists is the **substrate's** and is consistent with GR to the precision AT predicts. The
  identification premise is **experimentally excluded at bench scale** — G_011b's "the coupling is not
  borrowed" promoted to a measured exclusion.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_017.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_017_Tests.cs` (7/7 ✅, ~0.03 s; group G total 154/154),
  `Docs/ResearchY/Tests/Results/Y_G_017_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`metric-coupling` calculation, theory object + audit `g017`).
- **Next open questions (G_017 OP1–OP5).** A **pre-registered** clock experiment phrasing a bound on `Δν/ν`
  versus optical intensity? Can the 2.303e18 be turned into a **quantitative bound on the identification
  coupling**? Is there any system with a controlled **near-unity intensity contrast** (the only regime that
  could hide an effect)? Can a **differential** test separate the substrate reading from GR at any contrast?
  Does G_016b's unbounded fixed-energy family have a laboratory counterpart through a *non-intensity*
  ρ-carrying observable?

## Rho Identity Audit — the identity is an arrangement, and the method ledger (ResearchY-G_018, 2026-09-12)

- **Question.** After G_017 excluded the laboratory |ψ|² identification, **which physical quantity remains as
  the identity of ρ?** Candidates: occupancy measure, actualization count, state accessibility, degeneracy
  occupancy, survivor distribution. Output: **DERIVED / BOUNDARY / REFUTED**.
- **The criterion (no external physics).** Q is the identity of ρ iff **(I1)** defined by counting over
  distinguishable states (Difference → distinguishability only), **(I2)** dimensionless and normalised,
  **(I3)** survived G_017 (never held a lab number), **(I4)** determines ρ (not a lossy functional).
- **DERIVED — the occupancy measure.** 96 cells, A₀ = 45, free room 51, state dimension 95; Σρ = 1 and
  ρ_min > 0 always. **I3 is the pivot**: nothing in "ρ = counting" was ever a laboratory number, so G_017
  removed an *identification*, not the quantity. **I4** holds uniquely — it *is* ρ (zero loss).
- **REFUTED — the actualization count is a label.** Exact scale invariance: `max|a| = 0.6031746016455657` and
  clock separation `0.9985774245179969` **identical** at scales 1, 1e3, 1e−6 and 96. Every observable is a
  ratio, so `N·ρ` carries **zero content**.
- **DERIVED content / BOUNDARY dimension — the degeneracy occupancy.** Exactly the part of ρ energy cannot see
  (ΔE = 0; a 0.012 in-multiplet transfer leaves |ΔE| < 1e-13): **51/95 = 53.6842 %** of the state is invisible
  to everything except arrangement, **94/95 = 98.9474 %** invisible to energy; λ-mixing room 43, energy
  direction 1. Dimension `51 = Σ(m−1)` is BOUNDARY (two-level rule, D_028/D_040).
- **REFUTED — the survivor distribution is a lossy readout.** Per-multiplet totals keep **44** of 95
  dimensions (discarding **exactly the 51-dim room**), `E` keeps 1 of 95 (discarding 94), compaction **moves
  E** — and two *different* states share the same survivor data.
- **BOUNDARY — state accessibility is the lattice.** K = 1…6 → A₀ = **49/47/45/47/45/45** (free room N − A₀),
  with the **same** uniform ρ for every lattice (`L1 = 0`): a capacity input (G_007) bounding the reachable set.
- **What remains:** *ρ is an **arrangement*** — meaning entirely in the **shape** of the occupancy, none in its
  **size**.
- **THE METHOD LEDGER (the audit's own honesty check).** This audit imports **no constant at all** — no SI
  quantity, no measured number, no laboratory system. That makes it the **most AT-native audit of the group**
  and correspondingly **unfalsifiable by itself**. The resulting **provenance asymmetry** is the group's real
  finding about method:

| audit type | imports | status |
|---|---|---|
| combinatorial / invariance (G_001, G_002, G_005–G_008, G_011–G_014, G_016, G_016b, **G_018**) | nothing | DERIVED structure — not falsifiable alone |
| boundary-identified (G_003, G_004, G_009, G_010, G_015, G_017) | `G`, `c`, GM/R, finesse, `Q`, polarizability, 1e-18 | falsifiable — and where testable **excluded** or a **rank-1 shadow** |

- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_018.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_018_Tests.cs` (7/7 ✅, ~0.07 s; group G total 161/161),
  `Docs/ResearchY/Tests/Results/Y_G_018_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`rho-identity` calculation, theory object + audit `g018`).
- **Next open questions (G_018 OP1–OP5).** Is there a **third** audit type that imports a constant *and*
  survives observation? Can the 43-dim λ-mixing room be boundary-identified without reintroducing intensity?
  Is the free room 51 physical or a lattice artefact? Does "arrangement-only" imply an observational
  degeneracy beyond G_016b's family? Can the provenance asymmetry be made a quantitative **falsifiability
  budget**?

## Second-Order Signature Audit — the test that can decide it, and AT survives (ResearchY-G_019, 2026-09-12)

- **G_018 OP1, answered.** The missing item was an audit that imports measured constants and **survives**. This
  is it, and the answer is **yes**.
- **The signature.** `dτ/dt = ρ^(1/d)` gives `e^σ` where GR gives `√(1+2σ)`, so
  **`AT/GR = e^x/√(1+2x) = 1 + x² − (4/3)x³ + …`** with `x = Φ/c² < 0` for a bound object: **first order
  identical, second order opposite sign**, and **the discriminator is `x²`**. Series verified:
  `(ratio − 1)/x²` = 1.0000889006 (−1e−6), 1.0001333539 (−1e−4), 1.0135879522 (−1e−2), 1.2774576758 (−0.15).
  AT clocks run **faster** (`e^−0.15 = 0.8607079764 > √0.7 = 0.8366600265`), so AT's redshift is **always
  smaller** (0.1618342427 vs 0.1952286093). At `x = 1e−9` the difference 1e−18 is **below one ulp of 1.0**
  (1.11e−16) — *unrepresentable* at solar-system depths.
- **REFUTED — the weak-field route as a discriminator:** Earth **4.845934e−19** = 0.4846× the 1e−18 floor
  (2.1× short); ground-vs-GPS differential **4.567944e−19**; Sun **4.505017e−12** vs 1e−5 (2.2e6× short);
  Sirius B **6.619702e−8** vs a 2 % HST redshift (3.3e6× short; its own Δz/z is only 3.0e−4).
- **The live arena — NICER neutron stars** (3–10 % in the rate, **17–31 % in the redshift**): J0030+0451
  `x = −0.152011`, rate +0.029638, z 0.1641732 vs 0.1986775 (**−17.367 %**); J0740+6620 (Riley)
  `x = −0.247002`, +0.098133, 0.2801814 vs 0.4058088 (**−30.957 %**, absolute gap 0.1256); J0740+6620 (Miller)
  `x = −0.224246`, +0.076057, 0.2513786 vs 0.3465546 (**−27.463 %**).
- **The inverse map is sharper:** `z = 0.35` needs **R = 6.890 km** under AT against **9.164 km** under GR
  (−24.81 %) — while every NICER radius is 11–14 km. **A NICER-compatible object cannot have z = 0.35 under AT.**
- **AT SURVIVES (the OP1 result):** M = 1.4 ± 0.05 M☉, R = 12 ± 1 km → `z_AT = 0.188055 ± 0.024372` vs
  `z_GR = 0.235259 ± 0.038665`; separation **0.047205** vs σ **0.045706** = **1.033 σ**. 3σ needs
  **`σ_z ≤ 0.015735` = 8.37 % of `z_AT`** (6.69 % of `z_GR`); current NS redshifts are 20–50 % →
  **SHORT BY 2.4× TO 6.0×**. The survival is **not luck**: the signal is quadratic and no solar-system or
  white-dwarf measurement reaches `x²`.
- **Horizon corollary and the g₀₀-only boundary:** AT's `g₀₀ = −e^(2x)` **never vanishes** — at `y = 0.4999`,
  `z_AT = 0.6485564` against GR's 69.71068; at `y = 1/2`, **0.6487213 against a DIVERGENCE**; for
  `y = 0.6/1.0/5.0` GR has **no real surface** while AT gives 0.8221188 / 1.718282 / 147.4132. Every number in this section is **`g₀₀`-only**. ⚠ **CORRECTED by `ResearchY-G_021`** — this section
  originally said "AT supplies no spatial metric, so no light bending, Shapiro delay or shadow size follows
  — **the most distinctive consequence is the least derivable one.**" **Both sentences are false.** AT has
  the full metric (**conformally flat**, `g = ρ^(2/d)η`, so `g_rr = +ρ^(2/d)`), the chain is **closed**,
  and the derived optics are **`γ = −1` exactly** — all lensing observables and the Shapiro delay vanish
  while the redshift survives, which **Cassini excludes at 8.6957e4 σ**. The missing piece is `ψ`, not the
  spatial metric.
- **Verdicts: DERIVED** = the signature, the weak-field no-go, the horizon corollary · **BOUNDARY** = the
  absolute-depth anchor `x = GM/(Rc²)` (imported M, R, z) and the `g₀₀`-only scope of its own numbers ·
  **REFUTED** = the weak-field route. Riders: one object class, and the requirement is a **joint** measurement on the same object (`x` to
  ≈3 %, `z` to ≈8 %).
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_019.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_019_Tests.cs` (7/7 ✅, ~0.03 s; group G total 168/168),
  `Docs/ResearchY/Tests/Results/Y_G_019_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`second-order-signature` calculation, theory object + audit `g019`).
- **Next open questions (G_019 OP1–OP5).** Which single neutron star can deliver ≈3 % M–R **and** ≈8 % z?
  Can the radius inversion be phrased as a **pre-registered** kill criterion for a named object? Is NICER's
  joint posterior tight enough anywhere for `σ_x/x ≤ 3 %` without a new redshift? Any strong-field
  electrodynamic observable avoiding atmosphere-model systematics? And is there an **AT-native derivation of
  the spatial metric**, which would bring the horizon corollary (where GR's z diverges) into play?

## Neutron-Star Redshift Audit — the bottleneck is the weighing, not the spectroscopy (ResearchY-G_020, 2026-09-12)

- **Question.** Can any **currently measured** NS redshift exclude AT's exponential clock law? Sources: NICER
  masses and radii, X-ray burst redshifts, published uncertainties. Output: **EXCLUDED / ALLOWED / PREFERRED**.
  Critical: the precision on `z` and `M/R` for **5σ**.
- **Framework.** `x = GM/(Rc²)`, `z_AT = e^x − 1`, `z_GR = (1−2x)^(−1/2) − 1`, `Δz > 0`, and
  `S = Δz/√(σ_z² + (dz/dx)²σ_x²)` with `dz/dx = (1−2x)^(−3/2)`; AT always predicts the **smaller** redshift.
- **THE DECISIVE RESULT — the error budget is binding, not the signal.** Max significance with a **perfect**
  redshift (`σ_z = 0`): J0030+0451 (Riley) **0.614σ** · (Miller) **0.752σ** · **J0740+6620 (Riley) 1.334σ** ·
  (Miller) **0.778σ** · generic **1.221σ**. **No object reaches even 2σ**: the `(dz/dx)σ_x` term exceeds `Δz/2`,
  `Δz/3` and `Δz/5` for every one — so **no `z` precision whatsoever can reach 3σ, let alone 5σ**.
- **ALLOWED.** A representative `z = 0.30` with a 20 % systematic gives AT 1.17/1.55/1.87σ at R = 10/11/12 km
  (M = 1.4) and **0.33σ** at 2.072 M☉/12.39 km (GR 1.76σ); inside 1σ with a 50 % systematic. GR also allowed →
  no discrimination at this precision.
- **EXCLUDED — not achievable** (max 1.334σ with a perfect `z`).
- **PREFERRED — NOT supported.** The nominal AT-favoured ratio **4.483** (J0740+6620, `z = 0.30 ± 0.06`) **flips
  sign** across the plausible grid (**1.718 → 0.068**), only **1 of 9** grid points favours AT, and the data lean
  **toward GR** for `R ≥ 11 km`. It is an artefact of borrowing one object's `z` for another's `x`.
- **Radius inversion:** a measured `z` demands a smaller radius under AT — 0.20 → 11.342 vs 13.535 km;
  0.25 → 9.267 vs 11.488; 0.30 → 7.881 vs 10.129; **0.35 → 6.890 vs 9.164 km (−24.81 %)**.
- **The dangerous case:** the **unconfirmed** `z = 0.35` of EXO 0748−676 (Cottam 2002) excludes **both** laws at
  M = 1.4, R = 11 km (`z_AT = 0.206812`, `z_GR = 0.265888`) — it only demands a smaller radius (AT would need
  6.890 km, 4.110 km below 11 km; GR 9.164 km).
- **CRITICAL — 5σ requires BOTH** (J0740+6620, Riley 2021): `σ_x/x ≤ 3.661 %` (perfect `z`) **or**
  `σ_z ≤ 0.025125 = 8.97 % of z_AT` (perfect M/R) **or** the equal split `σ_x/x ≤ 2.589 %` **and**
  `σ_z ≤ 0.017766 = 6.34 % of z_AT`. Against today's 13.73 % M/R: **3.75–5.30× better M/R plus 3.2–7.9× better
  `z`, won jointly**. (3σ for the record: `σ_z ≤ 0.041876` or `σ_x/x ≤ 6.102 %`.)
- **Structural lesson:** the deficit is in the **error budget**, not the signal — `Δz = 0.1256` is large in
  absolute terms; it is `σ_x` that keeps the test out of reach. **The bottleneck is the weighing, not the
  spectroscopy.**
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_020.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_020_Tests.cs` (7/7 ✅, ~0.04 s; group G total 175/175),
  `Docs/ResearchY/Tests/Results/Y_G_020_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`neutron-star-redshift` calculation, theory object + audit `g020`).
- **Next open questions (G_020 OP1–OP5).** Any single NS with `σ_x/x ≤ 3.7 %`? Pre-register the radius
  inversion as a kill criterion for a named object? What will NICER-extended / STROBE-X / eXTP / Athena
  deliver against the 3.75–5.30× gap? A strong-field constraint on `x` **without** a mass measurement (burst
  oscillations, kHz QPOs)? And does PREFERRED change if `z` and M–R are ever measured on the **same** object?

## Light-Propagation Audit — AT *does* have the spatial metric, and its derived optics are excluded (ResearchY-G_021, 2026-09-12)
**Status:** Complete. **This audit CORRECTS `ResearchY-G_019` §5**, which claimed *"AT supplies no spatial metric,
so no light bending, Shapiro delay or shadow size follows from it — the theory's most distinctive consequence is
also its least derivable one."* **Both sentences were false**, and false in opposite directions: they understated
what AT derives *and* understated the severity of the result.
- **AT has the full metric, and the chain is CLOSED.** `g_uv = ρ^(2/d) η_uv` (**conformally flat**), so
  `g₀₀ = −ρ^(2/d)` **and** `g_rr = +ρ^(2/d)`. `Docs/Audits/MetricOriginClosure.md`: *class × factor = full
  `g_uv`* — the conformal **class** is **IMPORTED** (Malament 1977 / Hawking–King–McCarthy 1976, a *proven*
  theorem; a native reconstruction also exists, G4-M Phase 0) and the conformal **factor** `ρ^(2/d)` is **NATIVE**
  (the counting measure). **There is no missing derivation.**
- **The optics: `γ = −1` exactly.** With `σ = (1/d) ln ρ`, AT's own source law `a = −(1/d)∇ln ρ = −∇σ`
  fixes `Φ = σ`, so `g_rr = +(1 + 2σ) = 1 − 2γΦ` gives `γ = −1`. This is **exact**, not first order: with
  `Φ = (1 − g₀₀)/2 = (e^(2σ) − 1)/2`, `γ = −(e^(2σ) − 1)/(e^(2σ) − 1) = −1` for every `σ ≠ 0`.
- **Consequence:** every lensing observable is `∝ (1 + γ)/2` (QG26), so **deflection = κ = shear = μ−1 = Shapiro
  delay = 0** while the **redshift survives** (`g₀₀` alone). **AT predicts redshift WITHOUT lensing.** This is a
  **DERIVED FALSIFICATION, not a gap.**
- **Excluded:** **Cassini** (Bertotti/Iess/Tortora 2003, `γ = 1.0000210 ± 2.3e−5`) at **8.6957e4 σ**; **VLBA**
  (Fomalont 2009, `0.9998000 ± 3.0e−4`) at **6.6660e3 σ**; **Gaia** (2022, `0.9970000 ± 1.6e−2`) at
  **1.2481e2 σ** — plus thousands of observed lensing systems.
- **The escape and its price — derived HERE.** The `ψ`-completed metric `g₀₀ = −ρ^(2/d)e^(2ψ)`,
  `g_ii = ρ^(2/d)e^(−2ψ/(d−1))` (Fierz–Pauli, QG44; QG207/QG212) gives `γ = +1`. Because a timelike
  geodesic's acceleration is fixed by `g₀₀` **alone**, `Φ = σ + ψ`, hence
  **`γ(ψ) = −[σ − ψ/(d−1)]/(σ + ψ)`**, `γ = −1` at `ψ = 0`, and
  **`γ = +1 ⇔ ψ = −2σ(d−1)/(d−2)`** — i.e. **`ψ = −4σ` in `d = 3`**, `−3σ` in `d = 4`, and **no finite
  solution in `d = 2`** (consistent with `G_uv ≡ 0`, QG180). Along that direction the exact `γ` is `e^(6σ)`, i.e.
  `+1` to first order with a second-order residual — the order at which PPN `γ` is defined. **But the requirement
  is NOT free:** `Φ = σ + ψ = −3σ` then **contradicts** the native `a = −(1/d)∇ln ρ`, so restoring
  `γ = +1` **necessarily changes the SOURCING relation**. `ψ` is a **MINIMAL NEW PRIMITIVE** (QG24); QG43 notes a
  **1-d.o.f. scalar** suffices for `γ` and the Shapiro delay (only GW polarization needs spin-2).
- **Why the G-chain is unaffected.** *Every* audit G_001–G_020 used **`g₀₀` only** (source law, clock law,
  0.99600, 0.99668, the `x²` signature, `z_NICER`). Those results **stand**; the chain is **silent** on optics
  rather than wrong about them. G_019 is **not reclassified**.
- **One recorded consequence.** With `ψ ≠ 0` the clock law is **`dτ/dt = ρ^(1/d)e^(ψ)`**, and `e^ψ` enters at
  **FIRST order** — at `ψ = 1e−3` that is **1000.5×** G_019's `x²` term — so **any nonzero `ψ` swamps the
  second-order signature** the G-chain proposes to test. The G-chain's clock results are the `ψ = 0` limit.
- ⚠ **REFINED by `ResearchY-G_022`:** `ψ = −4σ` holds only if `Φ` is defined by `g₀₀`. Once AT's **source law**
  fixes `Φ = σ` independently, the acceleration law `−A′ = −σ′` forces `ψ′ = 0` and the clock law
  `e^A = e^σ` forces **`ψ = 0`** — so the `ψ` completion gives `γ = −1`, and the route is **EMPTY**. At
  `ψ = −4σ` the surface redshift is `z = −0.523366` at J0740+6620 — a **blueshift at a bound object**.
- **Verdicts: DERIVED** = the conformal spatial metric `g_ij = ρ^(2/d)δ_ij` (class × factor closed) · `γ = −1`
  exactly · all lensing observables and the Shapiro delay zero with the redshift surviving · `γ = +1 ⇔ ψ = −4σ`
  (`d = 3`) · **BOUNDARY** = the conformal class (imported theorem), the `ψ` sector (new primitive), and the
  `Φ = −3σ` conflict · **REFUTED** = the `ψ = 0` conformal optics (8.6957e4 σ Cassini).
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_021.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_021_Tests.cs` (7/7 ✅, ~0.03 s; group G total 182/182),
  `Docs/ResearchY/Tests/Results/Y_G_021_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`light-propagation` calculation, theory object + audit `g021`). G_019's own files carry the
  correction in place.
- **Next open questions (G_021 OP1–OP5).** **Is `ψ` physical?** (if so it must equal `−(4/3)ln ρ`). Can `ψ` be
  sourced without breaking `a = −(1/d)∇ln ρ`? What bound does the **clock precision** work of G_019/G_020 set
  on `ψ`, given that `e^ψ` is first order? Is the conformal-class import irreducible? Does the **horizon
  corollary** survive the `ψ` completion (`g₀₀ = −ρ^(2/d)e^(2ψ)` — does it still never vanish)?

## Spatial Metric Audit — the three laws are blind to it, and the fix is a postulate (ResearchY-G_022, 2026-09-12)
**Status:** Complete. **Question:** can a spatial metric be **derived** that preserves the **clock law**, the
**source law** and the **acceleration law**, while reproducing **`γ ≈ +1`**? **Answer: NO — structurally.**
**Refines `G_021`** (the `ψ` route is **empty**).
- **The three laws are `γ`-BLIND.** With `x = GM/(Rc²) > 0` and `σ = (1/d) ln ρ = −x`: **clock** `dτ/dt =
  √(−g₀₀) = ρ^(1/d)` (on `g₀₀` only), **source** `a = −(1/d)∇ln ρ` (on `ρ` only), **slow-particle
  acceleration** `d²r/dt² ≈ −A′` (on `g₀₀` only). **None contains the spatial exponent**, so the question as
  posed **constrains nothing about `γ`** — sliding the spatial exponent leaves all three laws bitwise identical.
  The only `B`-dependent quantity is the *proper* acceleration of a **held** observer, `â = A′e^(−B)`:
  `1.392e−9` at the Earth's surface — a measure convention, below every stated precision.
- **The fourth condition gives `γ = −1`, FOR ANY CONFORMAL FACTOR.** Conformal flatness `g = Ω²η` forces
  `g_rr = −g₀₀`, hence `γ = −(Ω²−1)/(Ω²−1) = −1` for **every** `Ω² ≠ 1`. Verified for `Ω² = ρ^(2/d)`
  (0.999998000002), `ρ^(−2/d)` (1.000002000002), `e^(−0.6x)` (0.999999400000), `1.5`, `0.9` — **all −1**. So
  `γ = −1` is a **THEOREM about 4D conformal flatness plus `Φ = σ`**, *not* an artefact of the counting-measure
  factor: **flipping the factor changes nothing.**
- **`γ = +1` needs TWO DIFFERENT FACTORS.** `g_rr = e^(2βσ)` gives **`γ = −β`** (`+1 → −1`; `0 → 0`; `−1 → +1`).
  So keep `g₀₀ = −ρ^(2/d)` **and** set `g_rr = +ρ^(−2/d)`. That passes **every** requirement the question
  sets — clock law **exact**, source law **exact**, acceleration law **exact**, `z_AT = e^x − 1 = 0.2801817`
  identical to `G_020`, Cassini 0.9130 σ (VLBA 0.6667, Gaia 0.1875) — but `g_rr + g₀₀ = 2 sinh(2x) = 1.028687 ≠ 0`:
  **NOT conformally flat**, so the **causal-order → conformal-class step does not produce it**. Two factors is a **POSTULATE**.
- **The price is visible.** Native `Ω = ρ^(1/d)` has volume measure `Ω³ = ρ` **exactly** (the counting measure);
  the `γ = +1` sector needs `Ω = ρ^(−1/d)`, i.e. `Ω³ = 1/ρ` — **the reciprocal of the counting measure**.
  The choice is not "which factor" but "the counting measure or its reciprocal".
- **The exclusion table:** `γ = −1` → **8.6957e4 / 6.6660e3 / 124.8125 σ (REFUTED)**; `γ = 0` (flat space) →
  4.3479e4 / 3.3327e3 / 62.3125 σ **(REFUTED)**; `γ = +1` → 0.9130 / 0.6667 / 0.1875 σ **(ALLOWED)**.
- **The `ψ` route is EMPTY (refines `G_021`).** `ψ′ = 0` (acceleration law) then `ψ = 0` (clock law) — **forced by
  both** — so the `ψ` completion delivers `γ = −1`, not `+1`: it **cannot be the fix at all**. At `ψ = −4σ` the
  surface redshift is `z = e^(−3x) − 1`: `−2.0e−9` (Earth), `−6.4e−6` (Sun), **`−0.523366`** at J0740+6620 — a
  **BLUESHIFT at a bound object**, refuted by any positive measured neutron-star redshift. The clock violation
  `|e^ψ − 1|` grows from `2.785e−9` at Earth to **`1.685879` (169 %)** at a neutron star.
- **NOTHING EARLIER IS DAMAGED — the question that motivated this audit.** **Every** `G_001–G_020` agreement with
  GR is `γ`-blind and therefore **invariant under any spatial sector**: `G_004`'s `0.99600 = G_SI/G_CODATA`, the
  `G_009`/`G_015`/`G_016b`/`G_017` clock rates and `Δτ = 86 277.089 s/day`, `G_019`'s `x²` signature, `G_020`'s
  `z_AT = 0.2801817`, and the source-law/four-scale/MOND phenomenology. **`γ` is a SEPARATE observable class**
  (light bending, Shapiro delay) that no G-chain audit exercised before `G_021`. **The numbers that agreed still agree.**
- **Verdicts: DERIVED** = the `γ`-blindness of the three laws · the conformal-flatness theorem for **any** factor ·
  the unique native `Ω = ρ^(1/d)` with volume measure exactly `ρ` · the explicit `γ = +1` sector ·
  **BOUNDARY** = the `γ = +1` sector is a **postulate** (reciprocal factors; cost: volume measure `1/ρ`) and the
  `ψ` route is **empty** · **REFUTED** = a *derived* `γ ≈ +1`, and the `ψ` completion (surface blueshift).
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_022.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_022_Tests.cs` (7/7 ✅, ~0.04 s; group G total 189/189),
  `Docs/ResearchY/Tests/Results/Y_G_022_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`spatial-metric` calculation, theory object + audit `g022`).
- **Next open questions (G_022 OP1–OP5).** Is there any **AT-native principle selecting a reciprocal spatial
  factor**? Can a **non-conformal** spatial sector be sourced by `ρ` alone? Does the reciprocal sector preserve
  the **four-scale calibration** once the `e^(−B)` factor propagates (`4.2e−6` at the Sun, `0.49` at a neutron
  star)? What does `Ω³ = 1/ρ` mean for the **counting measure**? Is the **conformal-class import** binding?

## Spatial Sector Closure — one number stands in the way, and the theory cannot supply it (ResearchY-G_023, 2026-09-12)
**Status:** Complete. **Question:** can **any** spatial metric be derived **without new primitives** yielding
**`γ ≈ +1`** while preserving the **clock law**, the **source law** and the **acceleration law**?
**Success criterion:** `γ ≈ +1` **without changing `g₀₀` physics**.  **Answer: NO** — one conformal invariant
obstructs it, and AT's construction pins it to zero. **Closes `G_022`.**
- **The framework (isotropic form — where PPN `γ` is DEFINED):**
  `ds² = −e^(2A)dt² + e^(2B)(dR² + R²dΩ²)`, `Φ = (e^(2A)−1)/2`, so **`γ = −(e^(2B)−1)/(e^(2A)−1) ≈ −1 + k/x`**
  with **`k := B − A`**. A static spherical metric has exactly **two** functions and `γ` is controlled by **one**
  number — their difference.
- **`k` is the conformal invariant.** `g → Ω²g` shifts `A` and `B` by the same `ω`, so `k` is **unchanged**:
  `k` is exactly the class data the causal-order → conformal-class step supplies. Conformal flatness means
  `g = Ω²η`, i.e. `A = B`, i.e. `g_rr = −g₀₀` — which **is** `k = 0`. Hence
  **conformal flatness ⇔ `k = 0` ⇔ `γ = −1` for EVERY `A`** (G_022's theorem, restated as `k = 0`).
- **AT pins `k = 0` from TWO independent directions.** Clock law `√(−g₀₀) = ρ^(1/d) = e^σ` ⇒ **`A = σ`**;
  counting measure — the spatial volume **is** the count, `√(det g_ij) = ρ` ⇒ `e^(3B) = ρ` ⇒ **`B = σ`**.
  Both express the **same primitive** (`ρ`) and pin their exponent to the **same scalar**, so **`k = 0`
  identically** and **`γ = −1` at every compactness** (Earth −6.96133e−10, Sun −2.122503e−6, 1e−4,
  J0740+6620 −0.247002, all with volume/ρ = 1.000000000). **A theorem about AT's construction, not a
  coincidence.** Violating either breaks a named law.
- **What `γ = +1` costs.** Keeping `A = σ` (**`g₀₀` physics unchanged**) and demanding `γ = +1` gives
  `e^(2B) = 2 − e^(2A)` ⇒ **`B = ½ln(2 − e^(−2x))`**, with `k = 2x` to first order. `γ = +1` holds to 1e−9
  in every row, and the price is the spatial volume `e^(3k)`: 1.000000 (Earth), 1.000013 (Sun), 1.000600
  (x = 1e−4), and **3.437585 at J0740+6620** (B = 0.1645877, k = 0.4115897 vs k_lin = 0.494004).
  **Changing the volume measure IS changing a primitive.**
- **The primitive inventory — CLOSURE.** Q-event counts, `ρ`, `σ`, DiffuseStep `Λ = I − W`, the D96 lattice,
  the spectral/`λ` structure and information content are **ALL SCALARS** — each fixes ONE function, and any
  `δ = B − σ` breaks the volume by `e^(3δ)` and moves `γ` to `−1 + δ/x`. The **one non-scalar ingredient** is
  the **causal ORDER** (it supplies the conformal CLASS) — and **AT's order is the FLAT D96 ring order**: the
  ring collapses **96 cells onto only 45 eigenvalues** (histogram `{1:1, 2:42, 5:1, 6:1}`, free room 51) while
  the **degeneracy-free random control has 96 distinct** values, so its class is `[η]` and `k = 0`. The
  complement candidates for a natural `1/ρ` are all **constants** (51/95 = 0.536842, the 95 → 44 → 1 chain =
  2.159091, 3.746e5, 1152) and **none matches `1/ρ` at any compactness**. **NO EXISTING PRIMITIVE CAN MOVE `k`.**
- **The success criterion, split.** Half one **IS satisfiable** — `A = σ` is kept exactly, so the clock law,
  source law, acceleration law and `z_AT = 0.2801817` are untouched (they are `γ`-blind). Half two is **NOT**:
  `γ = −1` is forced and excluded (Cassini **8.6957e4** / VLBA 6.6660e3 / Gaia 124.8125 σ) while `γ = +1`
  needs `k = 2x` and would pass (0.9130 / 0.6667 / 0.1875 σ); flat space (`γ = 0`) is REFUTED too
  (4.3479e4 / 3.3327e3 / 62.3125 σ).
- **Verdicts: DERIVED** = the closure identity `γ = −1 + k/x` with `k = B − A` · `k` the conformal invariant ·
  conformal flatness ⇔ `k = 0` · the two native conditions pinning `A` and `B` to the same `σ` · the exact
  `γ = +1` solution with `k = 2x` · **BOUNDARY** = the single scalar of class data that would do it (`k = 2x`;
  the spatial volume becoming `1/ρ`, a factor 3.437585), supplying it a **new primitive**, with `ψ` **empty**
  (G_022) and the class **imported** (Malament) · **REFUTED** = any spatial metric from AT's existing primitives
  giving `γ ≈ +1` while preserving the three laws, and both the conformal sector and flat space.
- **Files.** `Docs/ResearchY/G_GravitySource/ResearchY-G_023.md`,
  `AT.Tests/ResearchY/G_GravitySource/Y_G_023_Tests.cs` (7/7 ✅, ~0.06 s; group G total 196/196),
  `Docs/ResearchY/Tests/Results/Y_G_023_Result.md`; AT.App (Research News + Theory Book chapter + TestGroup)
  and AT.Book (`spatial-closure` calculation, theory object + audit `g023`).
- **Next open questions (G_023 OP1–OP5).** Is there **any AT-native principle selecting `k ≠ 0`**? Can the
  counting-measure **volume identity** be relaxed without abandoning the counting measure? **Is AT's causal
  order provably flat** (the isotropy evidence here is spectral)? Would `k = 2x` alter the **four-scale
  calibration** once `e^(−B)` propagates (`4.2e−6` at the Sun, `0.49` at a neutron star)? Is the
  **conformal-class import** the last irreducible import in the metric chain?

## Optics Were Already Solved — restoring QG212 and retracting three G-chain errors (ResearchY-G_024, 2026-09-12)
**Status:** Complete. **Why:** the AT-QG programme **resolved conformal optics before the G-chain began**, and
the G-chain mis-stated that status.
- **The canonical result — AT-QG QG212** (`Docs/Research/ATQG_ConformalOpticsResolution.md`): **Status COMPLETE —
  OPTICS RESOLVED**, tests `ATQG2120/2121/2122` **all passed**, core class
  `AT.Core/ResearchXH/ConformalOpticsResolution.cs`.

  | Sector | PPN `γ` | Lensing | Shapiro | Frame dragging | Redshift |
  |---|---|---|---|---|---|
  | **`ψ = 0`** (conformal `g = ρ^(2/d)η`) | **−1** | **0** | **0** | **0** | yes |
  | **`ψ ≠ 0`** (QG207 completion) | **+1** | **GR** | **GR** | **restored** | yes |

  Verdict: conformal no-lensing is a **RESTRICTED SECTOR** — *"not a numerical artifact, not physical GR, the
  physical sector is `ψ ≠ 0`"* — and the method line reads **"no new primitives"**.
- **THREE RETRACTIONS.**
  (1) G_021/G_022/G_023 call `ψ` *"a MINIMAL NEW PRIMITIVE (QG24)"* — **SUPERSEDED** by QG285/QG286/QG292: the
  trace/traceless decomposition of the one Difference at `d = 3` gives **6 = 1 trace (ρ) + 5 traceless**, of which
  **2 are TT** (`ψ`, spin-2), so the minimal primitive set is **{Difference, η}**. **WITHDRAWN.**
  (2) **G_022 §6 "the `ψ` route is EMPTY"** — the derivation of `ψ = 0` assumed the `ρ`-only clock law is
  non-negotiable, but **that IS the `ψ = 0` slice**; the physical sector has `√(−g₀₀) = ρ^(1/d)e^(ψ)`.
  **WITHDRAWN.**
  (3) **G_023's REFUTED verdict** rested on (1), so its **reason is void**.
- **WHAT SURVIVES AND IS SHARPENED.** (a) The `ψ = 0` slice is **excluded by measurement** at a computable
  significance: **Cassini 8.6957e4 σ**, VLBA 6.6660e3 σ, Gaia 1.2481e2 σ. (b) **The invariant split**: to first
  order `γ = −B/A`, so **`ψ = 0` ⇔ `A = B = σ` ⇔ `γ = −1`** (the restricted isotropic slice) and
  **`γ = +1` ⇔ `A + B = 0`** (`h₀₀ = h_ii`). Two realisations: keep the clock law (`A = σ`, `B = −σ = +x`) giving
  **G_023's `k = 2x`** — the clock-law-preserving special case; or the QG207 completion at `ψ = −4σ` giving
  `A = −3σ`, `B = +3σ`, **`k = −6x`**. So `ψ = 0 ⇔ A = B` **is** the quantitative content of QG212's word
  "isotropic".
- **THE ONE GENUINE OPEN ITEM ADDED.** In the QG207 parametrisation `γ = +1` requires `ψ = −4σ`, so the
  completion is **NOT redshift-neutral** — the clock shift is `e^(4x) − 1` = **2.784532e−9** at the Earth's
  surface, 8.490012e−6 at the Sun. Both are far below the verified tests (GPS +38.5 vs +38.6 μs/day = 0.2 %;
  Cassini 2.3e−5 on `γ`), so **no solar-system conflict**, and the implied bound `|ψ| ≲ 2e−3` from GPS is
  **7.2e5× looser** than required. **BUT** at compactness the leading-order redshift `z = e^(−3x) − 1` turns
  **NEGATIVE** (−0.259182 at x = 0.10, −0.403664 at 0.172317, **−0.523366 at 0.247002**). PPN fixes only the
  **first-order** `ψ = −4σ`, so the `O(x²)` form of the completion is **load-bearing and uncomputed** — a
  **BOUNDARY flag, NOT a refutation**.
- **Verdicts: DERIVED** = the two-sector structure as QG212 states it · the `ψ = 0` slice's measured exclusion ·
  the invariant split · `ψ` introduces no new primitive · **BOUNDARY** = the exact (nonlinear) completion is
  unspecified (the `O(x²)` form is load-bearing for the strong-field redshift) and `ψ`'s ontological status is a
  documented boundary (QG299) · **REFUTED** = G_022 §6 and G_023's verdict reason.
- **No reclassification of the QG212 result — this audit RESTORES it**; D_040 untouched; no canonical claim,
  value or equation changes; no new primitive; 7/7 PASSED.

## The Optics Result Was Not Actually Derived — an off-by-one and a hard-coded γ, now corrected (ResearchY-G_025, 2026-09-12)
**Status:** Complete. **Why:** G_024 restored QG212 **on the strength of its own documentation**. The instruction
was explicit: *do not believe the document — verify the old result and try to find errors in it.* Doing so
found **two real defects**.
- **DEFECT 1 — `d`-vs-`(d−1)` off-by-one in the ψ-perturbed determinant.** `MetricAnsatzAudit` claimed
  `det g = −ρ²` **for ANY ψ** (so `√(−g) = ρ` unchanged), and `PerturbedVolumeElement(x, d, b)`
  returned `Profile(x, a)` = ρ **by construction** — it never computed a determinant. **The spatial block has
  `d` factors, not `d − 1`.** Correct:

  ```text
  det g       = −ρ^(2(d+1)/d) · e^(−2ψ/(d−1))
  √(−det g)   = ρ^((d+1)/d) · e^(−ψ/(d−1))        4-volume
  √(det g_ij) = ρ · e^(−dψ/(d−1))                spatial — the counting measure
  ```

  | `b`, `x = 1` | former claim | corrected spatial | error | corrected 4-volume | error |
  |---:|---:|---:|---:|---:|---:|
  | 0.0 | 2.0000000000 | 2.0000000000 | 0.00 % | 2.5198420998 | 25.99 % |
  | 0.3 | 2.0000000000 | 1.2752563032 | **36.24 %** | 2.1688481946 | 8.44 % |
  | 0.5 | 2.0000000000 | 0.9447331055 | **52.76 %** | 1.9624550005 | 1.88 % |
  | −3.0 | 2.0000000000 | 180.0342626010 | **8901.71 %** | 11.2931487976 | 464.66 % |

  **The error is unbounded in ψ** — which is exactly why the shipped test (`Assert.True(sameVolume)`) could never
  catch it. **Consequence:** *"√(−g) = ρ is preserved for ANY ψ"* is **FALSE**, and **ψ = 0 is the ONLY
  member that preserves the counting measure** — which **STRENGTHENS** the selection rather than weakening it.
- **DEFECT 2 — γ was a hard-coded constant.** `GammaPsiNonZero() => NonTensorLensing.GrGamma()` (a literal
  `1.0`) and `GammaPsiZero() => ConformalGamma()` (`−1.0`): **no code path computed γ from the metric**, so
  origin-score items 2/3 were `(1+1)/2 = 1` and `Shapiro(1) = 2` — **arithmetic on a literal, unable to fail**.
  Nothing therefore detected that the **coded** metric `ψ = b·x` has `γ ≠ +1` anywhere:
  `b = 0.3` → **+0.3352 / +0.0022 / −0.0930 / −0.0694** at `x = 0.1 / 0.5 / 1.0 / 2.0`. And `γ = +1`
  requires `ψ = −4σ`, i.e. `−(4/3)ln(1+ax²)` — **quadratic**, not the **linear** `ψ = b·x`.
- **CORRECTED at 12 sites across 5 AT.Core files.** The determinant now **computes**; `PerturbedDet` /
  `PerturbedVolumeElement4D` **added**; `PsiPerturbationPreservesMeasure` → **`false`**; `PsiPerturbationBreaksMeasure`
  / `ConformalIsTheMeasurePreservingMember` **added**; **γ is now DERIVED** (`GammaFromPsiMetric` =
  `(g_ii − 1)/(g_00 + 1)`, exact; `GammaPsiZero` = **−1** and `GammaPsiNonZero` = **+1**, both computed);
  `GammaFromPsiMetricFirstOrder`, `PsiForGrOptics`, `GammaPsiNonZeroExact` **added**; `ConformalIsRestrictedSector`
  **rebased**; the `Redshift` doc corrected with `PsiClockShiftFactor` / `PsiSectorShiftsClock` **added**; and
  **`TRMCompatibilityAudit`'s `"metric-origin"` row moved `UNCHANGED` → `MODIFIED`** (matrix now **4 UNCHANGED /
  2 MODIFIED / 0 BROKEN**).
- **DERIVED (γ computed, not asserted).** At `ρ = 1.000001 / 1.5 / 2.0`: `γ(ψ = 0, exact)` =
  **−1.0000000000** in every row; `γ(ψ = −4σ, first order)` = **+1.0000000000** in every row;
  `γ(ψ = −4σ, exact)` = +1.0000020001 / +2.2500000000 / +4.0000000000 = **`e^(6σ) = ρ²`** at
  `d = 3` — **`γ = +1` holds only in the weak field**.
- **WHAT SURVIVES.** `γ = −1` for `g = ρ^(2/d)η` **STANDS** (now computed); `γ = +1` for `ψ ≠ 0`
  **STANDS** (now computed, at the **derived** `ψ = −4σ`); zero lensing / Shapiro / frame dragging at
  `ψ = 0` and **full GR** at `ψ ≠ 0` **STAND**; the **restricted-sector** classification **STANDS**, on
  corrected grounds. **WHAT DOES NOT:** *"the `ψ` sector preserves the counting measure"* (**REFUTED**);
  *"`metric-origin` ⇒ `UNCHANGED`"* (**REFUTED** → **MODIFIED**); *"`γ = +1` as a property of `ψ = b·x`"*
  (**REFUTED** — it was a constant). **`OPTICS RESOLVED` (4/4) is RESTORED, on the corrected basis.**
- **SCORECARD: 1 of QG212's 4 origin-score points was substantive** before the fix (only `γ = −1`).
- **BOUNDARY (exposed, unchanged in place).** The exact (non-linearised) `ψ` completion: `γ` drifts as
  `ρ²` and the clock shifts by `e^(−4σ) = ρ^(−4/3)` (2.784532e−9 at Earth, **O(1)** at
  compactness), so the `O(x²)` form is **load-bearing and still unspecified**. `AT.Core` now exposes
  `PsiClockShiftFactor`, `PsiSectorShiftsClock`, `PsiSectorBreaksMeasure` and `GammaPsiNonZeroExact` so it can be
  exercised rather than assumed.
- **CORRECTION RECORD:** `Docs/Research/ATQG_ConformalOpticsDeterminantCorrection.md` (**AT-QG phase 320**) \
u2014 amends QG207, QG212 and QG32; **issues no new physics claim**.
- **Verdicts: DERIVED** = the corrected determinant identities, `√(det g_ij) = ρ ⇔ ψ = 0`, `γ = −1`
  and `γ = +1` both computed, the drift `ρ²`, the clock factor `ρ^(−4/3)` · **BOUNDARY** = the exact
  `ψ` completion · **REFUTED** = the off-by-one determinant and everything resting on it, the hard-coded `γ`,
  and `metric-origin ⇒ UNCHANGED`.
- **Lesson recorded:** *a document marked RESOLVED with green tests is not the same as a resolved derivation, and a
  test that cannot fail is not a test.*
- **No reclassification**; D_040 untouched; no canonical claim, value or equation changes; no new primitive;
  **7/7 PASSED** (group G total **210/210**).

## The Defect Class Is Systemic — four QG audits, four contradictory test suites, all passing (ResearchY-G_026, 2026-09-12)
**Status:** Complete. **Why:** G_025 found one instance of a defect class and asked whether it was isolated. It is not.
- **The fingerprint:** a member or data field whose **name** claims a computation or a verification verdict, whose
  **value** is a literal, and which a **score, classification or test** then consumes.
- **FINDING 1 — THE QG VERDICT LADDER (🔴).** Four successive audits used **identical method names** and
  differed only in literals:

  | audit | `IsQuantumMechanicsDerived` | `SamePrimitiveForBoth` | `IsSpacetimeEmergent` | verdict |
  |---|---|---|---|---|
  | `QuantumGravityClosureAudit` (QG215) | `=> false` | `=> false` | `=> false` | PARTIAL QG |
  | `QuantumGravityReclosureAudit` (QG219) | `=> true` | `=> true` | `=> false` | EFFECTIVE QG |
  | `QuantumGravityReclosureAudit2` (QG221) | `=> true` | `=> true` | `=> false` | NEAR-COMPLETE |
  | `FinalQuantumGravityAudit` (QG223) | `=> true` | `=> true` | `=> true` | **COMPLETE QG** |

  The escalation **PARTIAL → EFFECTIVE → NEAR-COMPLETE → COMPLETE was produced by editing a literal.** In
  QG221/QG223 the six **sub-scores were a separate set of typed `1.0`s that never read the criteria**, so
  `TotalScore()` returned 6.0 and `Classify()` returned `"COMPLETE QG"` whatever the criteria said; in
  `QuantumGravityReclosureAudit2`, `IsSpacetimeEmergent() => false` coexisted with `SpacetimeSubScore() => 0.5`.
  **And the tests asserted the literals:** `ATQG_Phase215` asserts QM **not** derived while
  `ATQG_Phase219/221/223` assert QM **is** derived — **all four pass.** `FinalQuantumGravityAudit.PsiIsNewPrimitive()
  => true` also contradicted G_024/QG285/QG286/QG292.
- **FINDING 2 — THE BORN RULE (🟠).** `Survives` and the pass arrays were typed;
  `AllRequirementsUniquelySatisfied(reqs, tests)` **accepted the executed tests and never read them**. And the old
  test `Count(p => p) != 1` on **every** row returned false **unconditionally** (the factorization / orthogonality /
  additivity rows pass for **all six** exponents) — so `"D: Mathematical Derivation — α = 2 is UNIQUELY selected"`
  was **DEAD CODE**; X037's real output was `"C: Strong Theorem"`.
- **FINDINGS 3–5.** RAR completion: 7 of 11 rows typed `Derived = true` + the literal label `"DERIVED ✓"`, then
  counted. `G`: genuinely computed in `NewtonConstantOrigin` but duplicated as an unchecked literal in
  `FrameDraggingOrigin.G_D96` and `PhysicalUnits.G_SI`. `IdentityHoldsAcrossGrid(feedback, damping)` declared the
  dynamics parameters and silently answered for the **defaults**.
- **THE FIX (verdicts UNCHANGED).** A new **`QgCriterion`** type makes a criterion a **whole value** —
  Name + Status (None/Partial/Full) + **mandatory cited Basis** — with a **derived** Score, so every accessor,
  sub-score, total, classification and progression is a function of the criteria table, and `AllBasesCited()` /
  `ClassificationFollowsFromCriteria()` are checkable. The **derived ladder reproduces the historical rungs exactly**
  (**2.0 / 4.0 / 5.0 / 6.0 → PARTIAL / EFFECTIVE / NEAR-COMPLETE / COMPLETE**). A new **`AlphaInvarianceScreen`**
  **executes** the physics — `N(ψ) = Σ|ψ_i|^α` invariant under every unitary **iff α = 2** (deterministic DFT +
  fixed Givens, dims 2–5): max relative violation **2.343702 / 1.236068 / 0.4953488 / 2.220446e−16 / 0.5527864 /
  0.8000000** for α = 0.5/1/1.5/**2**/3/4. **`RequirementEvidence`** splits Computed (4) from Analytic (3), disclosed
  in the classification. **`CompletionEvidence`** marks RAR rows with the three **limit rows now computed** from
  `g_obs = g_bar·√(1+g†/g_bar)` (Newtonian ratio 1.000000000; deep-MOND ratio 1.000000000).
  `CacheAgreesWithComputation` checks the G caches (computed 6.64670e−11; 0.014 % from `G_D96`, 0.414 % from CODATA).
  ψ corrected to the canonical position, with `PsiWasCalledNewPrimitive()` preserving the record.
- **ROBUSTNESS DEFECT FOUND EN ROUTE.** The α selection compared **formatted number strings** (`"2.0"`) —
  **culture-dependent**: a comma-decimal culture would silently collapse the classification. Now `InvariantCulture`
  and a numeric comparison.
- **DETECTORS (1181/1221 literal-bodied members)** — session artifacts: **248** literal-bodied members feeding scores;
  **16** members with declared-but-unused parameters (decidable, not heuristic); **49** evidence-boolean literals.
- **VERIFIED NON-FINDINGS** (so the fix does not over-reach): `ConservationPrincipleAudit.Laws()` and
  `MajoranaOrigin.Checks()` pass **computed calls**; `TemporalWaveObservables.* => 0.0` are documented analytic null
  results; `MetricOrigin.SqrtMinusG_Const`, `LightPropagation.LightSpeed(rho) => 1.0`, `Anchor.IsTrueInput`,
  `MeasurementClass.StructurallyUnique` and `Rule.Derivable` are honest constants / input taxonomies.
- **BOUNDARY:** the criteria **statuses** remain the authored QG223 adjudication (restructured, not re-adjudicated);
  **3 of 7** Born requirements remain **Analytic**; `(feedback, damping)` do not move the tested spectrum.
- **Lesson recorded:** *a test that asserts a literal against itself is not evidence, and four passing suites can
  assert mutually contradictory verdicts.* — **8/8 PASSED** (group G total **218/218**); affected pre-existing
  suites **390/390 PASSED**; no reclassification; D_040 untouched.

## The Rule Is Now Enforced, Not Just Observed (ResearchY-G_027, 2026-09-13)
**Status:** Complete. **The rule:** *no result path may let a classification depend on a literal; every verdict must be traceable to computed evidence.* A path
`literal boolean (verdict name) -> score / classification / verdict function` is **REFUTED** unless **COMPUTED** or adjudicated **SAFE**/**BOUNDARY** with a cited reason. **FAIL IF:** literal -> verdict without a computation path.
- **WHY AN ENFORCEMENT, NOT ANOTHER SWEEP.** G_025 and G_026 fixed instances by hand — and the class still recurred **four times in the QG ladder alone**. So `AT.Core/ResearchXH/LiteralVerdictAudit.cs` **re-reads the AT.Core source at test time**; a **new** literal-to-verdict path **fails the build** until it is computed or explicitly adjudicated.
- **LOCATION.** (1) parse every `record` for verdict-vocabulary **boolean** parameters, **by name and by position** (the codebase writes both `Holds: true` and positional `..., true, ...`); (2) find those booleans supplied as **literals** at construction sites; (3) require the file to **consume** the flag from `Score|Classify|Verdict|Rank|Assess|Grade|Count(`. Condition (3) keeps the inventory honest — a literal nobody scores is inert.
- **INVENTORY: 7 paths, ALL BOUNDARY.** Every one is an **authored judgement or input taxonomy with its reason cited in place**, none a physics result:

  | file | verdict field |
  |---|---|
  | `SelectionPrincipleAudit.cs` | `Rule.Derivable` |
  | `AnchorInventoryAudit.cs` | `Anchor.IsTrueInput` |
  | `PrincipleCompetitionAudit.cs` | `Principle.Consistent` |
  | `MeasurementClassAudit.cs` | `MeasurementClass.StructurallyUnique` |
  | `ActualizationOriginAudit.cs` | `DependencyFact.SupportsDerivedFromDifference` |
  | `ReorganizationPrediction.cs` | `ReorgMember.Correct` |
  | `MicroscopicChargeProfile.cs` | `FragmentationAttempt.IsValidCharge` |

- **ZERO REFUTED REMAIN.** The registry **structurally forbids** a REFUTED entry, because a refuted path is **fixed**, not tolerated.
- **THE ONE REFUTED PATH — FOUND AND FIXED.** `ResearchQG/GdaggerOriginAnalyzer.cs`: every evidence flag, the ratio **and the score** were literals at all six construction sites, and `Score` feeds `OrderByDescending` (**the ranking**). The Coincidence row was even **internally inconsistent** — `RatioToA0` typed `1.0` with a `NaN` numerator. All four are now **derived** from the predicted g†:

  ```text
  HasExactTwoPi = |predicted - cH0/(2pi)| / (cH0/(2pi)) < 1e-6
  RatioToA0     = predicted / a0            (NaN propagates honestly)
  Matches       = |RatioToA0 - 1| <= 0.15   (the criterion the Verdict text states)
  Score         = 3.0 if exact 2pi and matching; else 1.5 if not falsifiable; else 0.5
  ```

  **Verified to reproduce the previously TYPED values for all six mechanisms** (3.0 / 3.0 / 0.5 / 0.5 / 0.5 / 1.5) — the fix changes **provenance, not the physics**.
- **REGRESSION RECORD.** `FixedByComputation` lists the **11 sites** fixed by G_025/G_026/G_027 (the optics determinant and γ; the four QG criteria tables; the Born-rule `Survives` and pass arrays; the RAR `Derived` rows; the g† flags, ratio and score). They no longer **scan**, so a regression — a literal reappearing — is caught and **cannot be excused by the registry**.
- **SCOPE.** The scan covers **AT.Core**; AT.Tests/AT.App/AT.Book are downstream of the adjudicated objects (OP3).
- **WHAT IT DOES NOT CLAIM.** The audit does not decide whether a verdict is **correct** — only whether it is **computed**, forcing the difference into the open instead of leaving it implied by a method name.
- **Output: SAFE / BOUNDARY / REFUTED.** **5/5 PASSED**; group G total **223/223**; no reclassification; D_040 untouched.

## CLOCK CLOSED — on `dτ/dt = ρ^(1/d)` (ResearchY-G_028, 2026-09-13)
**Question:** is the physical clock law `ρ^(1/d)` or `ρ^(1/d)·e^ψ`? **Answer: `ρ^(1/d)` — the clock sector closes.**
- **THE ONE FACT THAT DECIDES IT.** The Newtonian potential **IS** the time exponent:

  ```text
  g₀₀ = −e^(2A) ,  A = σ + ψ ,  σ = (1/d)ln ρ ,  Φ/c² = A
  ⇒  a = −∇Φ = −(1/d)∇ln ρ − ∇ψ
  ```

  The stated source law `a = −(1/d)∇ln ρ` therefore holds **iff ∇ψ = 0** — a *constant* ψ, which is only a global
  time-unit choice (it cancels from the redshift ratio).
- **CRITICAL QUESTION — can ψ be nonzero without changing the source law? → NO.** `A − σ = ψ` **identically**, so any
  nonzero ψ moves the time exponent. This is a **proof**, not a judgement.
- **THE OPTICS PRICE.** In the trace-preserving QG207 direction (`A = σ + ψ`, `B = σ − ψ/2` at d = 3), the condition
  `γ = +1` (i.e. `A + B = 0`) fixes **`ψ = −4σ` UNIQUELY**, forcing **`A = −3σ = +3x > 0`**. The potential becomes
  **positive**: the surface clock runs FAST and every gravitating body is **BLUESHIFTED**. The sign inverts.

  | body | x | z (ρ-only) | z (QG207, ψ=−4σ) |
  |---|---:|---:|---:|
  | Earth | 6.957e−10 | **+6.957e−10** | **−2.087e−9** |
  | Sun | 2.1225e−6 | +2.1225e−6 | −6.3675e−6 |
  | 1e−4 | 1e−4 | +1.0000e−4 | −3.0000e−4 |
  | J0740+6620 | 0.247002 | **+0.2801817** | **−0.5233658** |

- **FIVE REQUIREMENTS.** ρ-only satisfies **ALL FIVE**: (1) source law; (2) Earth/GPS at **0.000 σ**; (3) the G_020
  neutron-star audit, `z_AT = 0.2801817`; (4) positivity at **every** body including compact objects; (5) no new primitive.
  QG207(ψ=−4σ) fails 1 (the ψ term is **4×** the ρ term), 2 (**2000.4 σ** at Earth), 3 and 4 — passing only 5.
- **CORRECTION TO G_024 AND TO THE RESTORED QG212.** G_024 concluded the QG207 completion produces **“no solar-system
  conflict”**, quoting `|ψ| ≲ 2e−3` as 7.2e5× **loose**. That comparison is **wrong in kind**: it treated the shift
  `e^(4x) − 1 = 2.78e−9` as *additive* on a clock of 1, but the observable is the redshift **relative to infinity**, where the
  conformal law already gives `z ≈ +x = 6.957e−10`. The shift is **4× the whole effect** and **reverses its sign**.
  The correct bound is **`|ψ| ≲ 1.391e−12`** — **1.44e9× TIGHTER** — and `ψ = −4σ` is excluded at **2000 σ** by the
  Earth redshift alone. So **QG212's “physical sector” is a REPULSIVE geometry** and is observationally refuted.
- **BOUNDARY — the spatial route.** `A` held at `σ` with `B = ½ln(2 − e^(2σ))` is the **only** `γ = +1` that leaves the
  clock untouched: it satisfies 1–4 and fails 5 (it leaves the trace-free direction; the volume measure becomes **3.437585×**
  the count at J0740+6620).
- **THE STRUCTURAL RESULT.** ψ and the clock are **NOT independent**. Every attempt to repair γ from the trace-free direction
  repays the debt in the clock, and the repayment is a **sign inversion**. So the optics fix **cannot** come from ψ — it must
  be spatial.
- **CLOCK CLOSED — and the closure RELOCATES the failure.** The clock sector is sound (γ = −1 is excluded at Cassini
  8.6957e4 σ, but optics is **not** a clock-sector requirement); the **entire** open problem is now the **SPATIAL sector**.
- **Opened:** fix optics spatially (G_023's route, volume cost 3.437585 — decide whether that is a primitive); re-audit QG212's
  “physical sector”; re-derive the GPS bound for **every** ψ proposal (the 2e−3 error is a **method** error, not a typo).
  Core: `AT.Core/ResearchXH/ClockSectorClosure.cs`; **7/7 PASSED**, group G total **230/230**.

## THE SPATIAL SECTOR CLOSES — on a postulate: `g_rr = 2 − ρ^(2/d)` (ResearchY-G_029, 2026-09-13)
**Question:** with `A = σ` **fixed by G_028**, can any spatial metric `B(r)` survive all eight constraints without a new primitive?
- **THE REQUIREMENTS CONSTRAIN ONE NUMBER, NOT A FUNCTION.** Reqs 1–4 (Newton limit, Earth clock, GPS, neutron-star audit) depend **only on A** and are passed by **every** candidate. Reqs 5–7 (Cassini γ, deflection, Shapiro) depend **only on γ**. So the whole nonlinear completion of `B` stays free.
- **CANDIDATE TABLE** (γ at J0740+6620):

  | family | B | γ | defl/GR | Shapiro/GR | √det g_ij/ρ | verdict |
  |---|---|---:|---:|---:|---:|---|
  | **B = σ** (counting measure) | −0.247002 | **−1.000000** | **0** | **0** | **1.000000** | **REFUTED** — Cassini **8.6957e4 σ** |
  | B = −σ (linear) | +0.247002 | 1.638865 | 1.319433 | 1.319433 | 4.401793 | SURVIVES (1st order) |
  | **B = ½ln(2−e^(2σ))** | +0.1645877 | **1.000000** | **1.000000** | **1.000000** | 3.437585 | **SURVIVES — IDENTIFIED** |
  | B = −σ − 2σ² | +0.12596 | 0.350133 | 0.675067 | 0.675067 | — | SURVIVES (c₂ free) |
  | rational / exponential | — | ≈1+O(x) | ≈1 | ≈1 | — | SURVIVES (unselected) |

- **THE UNIQUE SURVIVOR, IDENTIFIED:**

  ```text
  e^(2B) = 2 − e^(2A)   ⇔   g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|
  ```

  **The spatial metric is the REFLECTION of the temporal one about 1** (the conformal γ = −1 member is `g_rr = |g₀₀|`). To first order `g_rr = 1 + 2x` — the GR value.
- **NON-DEGENERACY — CORRECTED IN THIS AUDIT.** `2 − e^(2A)` vanishes only at `A = +½ln2`, i.e. **x = −½ln2 = −0.3465735903** — a *negative* compactness (a repulsive object). So the exact member is **non-degenerate everywhere physical**, with **`g_rr ∈ (1, 2)`** for every `x > 0`, tending to 2 as x → ∞: space is uniformly **stretched**. *(My first derivation had a sign error and claimed a degeneracy at R = 2.8854 GM/c²; that was wrong.)*
- **AT DERIVES NO B.** Its only equation — the counting measure — yields `B = σ`, the member Cassini **excludes**. So the survivor is selected by **observation**, and the price is exact:

  ```text
  counting measure → B = σ              → γ = −1 → REFUTED (8.6957e4 σ)
  GR optics        → g_rr = 2 − ρ^(2/d)  → γ = +1 → SURVIVES, volume cost 3.437585
  ```

  The cost **reproduces G_023's recorded 3.437585 exactly** (computed 3.437584871).
- **THE COMPLETION IS UNSELECTED.** Cassini bounds the quadratic coefficient only by **`|c₂ + 2| ≲ 22.5`** at 3σ, so the requirements pin **γ**, not **B**.
- **Output: SURVIVES / BOUNDARY / REFUTED.** **SURVIVES** = the γ = +1 family, exactly one member at all orders — identified above. **BOUNDARY** = γ pinned but B not; the counting measure traded away; and AT deriving no B, so the survivor is a **POSTULATE**. **REFUTED** = `B = σ` and the counting measure **as the equation for B**.
- **Critical outcomes.** *All DERIVABLE B fail* (only `B = σ` is derivable) → **the spatial sector is CLOSED as a derivation.** *Exactly one survives* the strengthened requirement → **the sector CLOSES ON A POSTULATE**, at an exact and known price.
- **Opened:** is there an AT-native principle for `g_rr = 2 − |g₀₀|`? Does the 3.437585 volume cost break the four-scale calibration, the deficit accounting (QG181/182) or the spectral identification of `occ`? Re-examine the horizon structure and the Hawking-temperature chain (QG208) on the new B, since `g_rr → 2` while `|g₀₀| → 0`.
- Core: `AT.Core/ResearchXH/SpatialSectorClosure.cs`; **7/7 PASSED**, group G total **237/237**.

## NO-GO PROVED — the spatial sector is not derivable (ResearchY-G_030, 2026-09-13)
**Question:** is there a theorem that any local `B(r) = F(σ)` must violate one of Newton / Cassini / light deflection / clock sector / no-new-primitive? **YES — NO-GO.**
- **SIX OF THE EIGHT CONSTRAINTS ARE INERT.** Newton, the Earth clock, GPS, the neutron-star audit and the clock sector depend **only on A** (untouched by B); light deflection is `(1+γ)/2` — **the same test as Cassini**. The live pair is **{Cassini/deflection} vs {no-new-primitive}**.
- **TWO EXACT EQUIVALENCES** (verified by solving, via `B(γ) = ½ln(1 − γ(e^(2A) − 1))`):

  ```text
  (i)   gamma = -1  <=>  B = sigma              (the counting measure  sqrt(det g_ij) = rho)
  (ii)  gamma = +1  <=>  e^(2B) = 2 - e^(2A)    (g_rr = 2 - rho^(2/d) = 2 - |g00|)
  ```

  **The two special γ values correspond EXACTLY to the two special B's — and only the excluded one is AT-derived.**
- **THE PROOF.** `F = Id` — the counting measure, AT's **only** determination of B from ρ — gives `B = σ`, hence `γ = −1`: **8.6957e4 σ** outside the Cassini band `[0.9999520, 1.0000900]`, deflection **identically 0**. `F ≠ Id` places no AT-derived value in B, so `F`'s specification is an input beyond ρ — **a new primitive** by G_023's accounting. Every other ingredient fixes **A**, fixes **numbers** (`occ`, `Σm`, `#g`), or supplies **free content** (the traceless face ψ); none determines B. ∎
- **THE ADMITTED BAND IS A SLIVER ON THE WRONG SIDE OF ZERO.** At solar compactness it is **~5.5e−10 wide** and sits at `B ≈ +x = −σ`, while the derived value sits at `B = σ = −x` — **opposite sides of zero**. The derived B has the wrong **sign**, not merely the wrong size. Established **rigorously by exact inversion** (no sampling; true at every body) and confirmed by a **band-resolving sweep** (20 001 points over `[−2x, 2x]` per body, **zero escapes**).
- **THE DEPARTURE IS NOT A SMALL CORRECTION.** Survivor `√det g_ij/ρ`:

  | body | ratio | departure |
  |---|---:|---:|
  | Earth | 1.000000004 | 4.2e−9 |
  | Sun | 1.000012735 | 1.3e−5 |
  | x = 0.1 | 1.733052388 | 7.3e−1 |
  | **J0740+6620** | **3.437584871** | **2.44** |
  | x = 1 | 51.142808724 | 50.1 |

  Growth beyond **10⁵**, reaching **O(1) exactly where the theory is most discriminating**.
- **Output: NO-GO.**
- **Goal — existence of a surviving spatial sector:** **REFUTED as a DERIVATION** (G_029's *"chosen, not derived"* **elevated to a theorem**: G_029 found the survivor by postulate, G_030 proves why no derivation exists); **AFFIRMED as a POSTULATE** (`g_rr = 2 − ρ^(2/d)`).
- **Sharpest form:** `γ = −1 ⇔ B = σ` (refuted) and `γ = +1 ⇔ g_rr = 2 − |g₀₀|` (underivable). **There is no third.**
- **Consequence — a dilemma with exactly two horns.** (1) **Keep the counting measure** → `γ = −1` → excluded at 8.6957e4 σ, zero deflection, zero Shapiro. (2) **Abandon it** → correct optics, but the volume–count identification is replaced by an input the theory does not supply. The choice is **not numerical**: it is a choice about **which of AT's identifications is primitive**, and G_030 proves the two cannot be had together.
- Core: `AT.Core/ResearchXH/NoGoTheorem.cs`; **8/8 PASSED**, group G total **245/245**.

## G_031 - Spatial-Origin Audit (COMPLETE, REQUIRED)

`B = sigma` **does not enter as an assumption anywhere.** With `g_uv = rho^(2n) eta_uv`: `A = B = n ln rho`,
`sqrt(-g00) = rho^n`, `sqrt(det g_ij) = rho^(n d)`. In d = 3:

  `n = 1/d  <=>  sqrt(-g00) = rho^(1/d)  <=>  sqrt(det g_ij) = rho`

**The clock law and the counting measure are THE SAME EQUATION** - two readings of one exponent choice from
two slots that conformal flatness already made equal. Verified exhaustively (401 x 401 in (n, rho)): **0 mismatches**.

- Provenance chain: Difference->Density **DERIVED** (QG285/286/292); Density->Metric **ASSUMED** (QG207, source of A=B); exponent `n=1/d` **ASSUMED** (= the clock law); Metric->Spatial measure **DERIVED** (algebra); rho as geometric volume **CORRESPONDENCE** (G_018).
- Usage: **5 LoadBearing** (metric ansatz; N = int rho dV QG194/222; deficit accounting QG181/182; horizon area->entropy QG185/259; cosmological densities), **2 Neutral** (|psi|^2 = rho QG216; RAR scale QG080).
- Breakage if removed: geometric/count volume ratio 1.000012735 (Sun) -> **3.437584871 (J0740+6620)** -> 51.142808724 (x=1). Four breakages at 2.44.
- **Renarrates G_023**: there are NOT two pins. Conformal flatness supplies A = B; one exponent choice supplies both. The counting measure is a theorem, not a premise.

**Sharper dilemma (supersedes G_030 phrasing):** not *counting measure vs optics*, but **conformal flatness + clock law vs optics**.

Tests: `Y_G_031_Tests` 7/7. Group G: **252/252**.

## G_032 - Conformal Assumption Audit (COMPLETE, BOUNDARY)

**`g = Omega^2 eta` is ASSUMED, not derived.** It first becomes necessary at the **rho -> metric** step - the step
where the Difference acquires its **TENSOR face**. The chain is metric-free for its first two steps
(Difference -> Counting, Counting -> rho). The imported object is **eta**, AT's second primitive.

- Grounded in the repo's own records: QG288 - "the tensor face requires eta [conformal reference, QG285], the scalar face does not"; QG289/QG77 (AnchorInventoryAudit) - eta is a **TRUE THEORY INPUT**: "part of the geometry, not a choice".
- **The criterion, proved:** `g = Omega^2 eta  <=>  A = B` (area gauge: `e^(B_area) = 1 - R A_R`, iff B' = A'). 0 mismatches; Weyl W = 1.0e-30 (A=B) vs 0.25-15.4 (A!=B).
- **The G_029 survivor is NOT conformally flat:** deficit `-1.5x^2 + (13/6)x^3 - ...` - conformal to first order, violated at second (W = 0.2537).
- **NO primitive forces it.** Setting eta aside (self-referential), the forcing set is EMPTY: rho, psi, d, pi are all rank 0. 10 metric components -> 1 conformal function, discarding 9; AT supplies 0 of 5 traceless components. G_029's family B = F(sigma) IS that leftover freedom.
- **Counting is a determinant, not a shape:** diag(rho^2,1,1) and diag(rho^(2/3),...) share sqrt(det) = rho but differ in conformal status. With A = sigma fixed: **conformality <=> counting measure <=> B = sigma** (0 mismatches).
- **DECISIVE:** A = B + A = sigma => B = sigma => **gamma = -1 EXACTLY at every body** => **Cassini 86957.4348 sigma**, zero deflection and Shapiro. The band (~1.84e-10 wide) sits at ~ -sigma while conformality demands +sigma: opposite sides of zero, violated by a **factor of 2.000013**. If the ansatz were entailed, AT would be refuted.
- **AT's own metric form `g = rho^(2/d) eta` IS the conformal member**, so it predicts gamma = -1 (no light bending) - the earlier programme concern now traced to its primitive root.

**Amends G_029/G_030/G_031:** the survivor is non-conformal; G_030's dilemma restates as **"conformal flatness vs optics"** (root = eta); G_031's theorem's premise is eta, an input.

Tests: `Y_G_032_Tests` 8/8. Group G: **260/260**.

## G_033 - Cubic Substrate Audit: does gravity/time need D96^3? (COMPLETE, PARTIAL)

Question: the programme established that D96 (x) D96 (x) D96 ("D96^3") is required to explain physics (M_012:
a genuine 3D sector exists on the cubic lattice but is ABSENT from a single D96 ring, max irrep dim 2). Does
the gravity/time search (group G) show the same behaviour? **Answer: PARTIAL - same requirement, different locus.**

- **IN CODE, ERA-LOCAL.** The audit re-reads the group-G sources at test time (G_027 pattern). 34 suites
  classified: **20 reference D96 in code** (the density era, G_002-G_011b, G_012-G_014, G_016, G_016b, G_018,
  G_023, G_024, G_026), **1 comment-only** (G_001), **13 with NO reference at all** - exactly the metric /
  closure era: **G_015, G_017, G_019-G_022, G_025, G_027-G_032**.
- **IN STRUCTURE, INHERITED.** The spatial metric is a symmetric rank-2 tensor in d = 3: 6 = 1 (trace, l=0) +
  5 (traceless, l=2), and l = 2 subducts as **Eg(2) + T2g(3)**. So gravity REQUIRES a dimension-3 irrep - the
  same structure a single D96 ring (dihedral D_96, irreps of dim 1-2) cannot supply. Reproduces M_012 exactly
  (norms^2 = 1,1,2,3,4; l=2 -> 2+3; l=1 -> 3). d = 3 is the UNIQUE fixed point of rotation self-duality
  d(d-1)/2 = d (M_013).
- **THE LOCUS IS eta (G_032).** The metric era imports 3D space through the primitive eta instead of generating
  it from a 96^3 spectrum. G_032 and G_033 are ONE FACT SEEN TWICE: the shape of space is an input, and that
  input is what the cubic lattice would otherwise have supplied. The D96^3 requirement was ABSORBED, not removed.

**DEFECT FOUND WHILE CHECKING.** The figure **A0 = 20 812 eigenspaces** that group G cites for D96^3 (G_002,
G_005, G_006, DensityField, SpectralCaseCatalog, AT.App, AT.Book) is **NOT an invariant**. It is the count of
distinct binary64 sums from the implementation's `Dictionary<double,int>` keying, so it depends on the last bits
of `Math.Cos`: the SAME IEEE-754 algorithm gives **20 812** (.NET) and **20 440** (Python), and a one-ulp
(1e-16) perturbation of the 1D spectrum swings it over **20 488 ... 21 369** (range 881). The **ROBUST** count -
plateau across 7-10 decimal places, spread **0** under 1e-16 and 1e-13 noise - is **16 080**, which is M_012's
own figure AND the value the repo uses elsewhere (`NewChat_Start`: "D96^3 136 005 of **868 656**";
868 656 = 884 736 - 16 080). The two lineages never met. Free room / L: 863 924 / 0.976477 (artifact) versus
868 656 / 0.981825 (robust). The QUALITATIVE conclusion (D96^3 is ~98 % energy-free) SURVIVES; the specific
numbers (20 812, 863 924, L = 0.97648, and lock release 3.948614 derived from them) do NOT. NOT FIXED here
deliberately: the keying is shared infrastructure consumed by other research groups, so changing it renumbers
A0 programme-wide and is a programme-level decision. Registered as an open item.

Tests: `Y_G_033_Tests` 5/5. Group G: **265/265**.

## G_034 - A0 Robustness Audit: which conclusions depend on A0? (COMPLETE, REFUTED-for-values)

**The keying is REPLACED and the fallout measured: NO G-series conclusion breaks, but SIX pinned values were artifacts.**

- **THE REPLACEMENT, APPLIED.** `SpectralCaseCatalog.D96CubedBreakdown` keyed its 49^3 triple sums on EXACT
  binary64 equality; it now CLUSTERS at a tolerance - and the tolerance is not invented: it is the rule **D_047
  already documents and applies** (`private const double TolCube = 1e-8; // 3-factor sums carry ~1e-14 noise;
  tol above it`). **This was never "two lineages that never met" - it was two equality rules in one programme.**
- **5/5 INDEPENDENT MATCH.** The corrected spectrum reproduces D_047 exactly: A0 = **16 080** (16 080),
  A0(m>1) = **16 079** (16 079), max multiplicity = **738** (738), lock release = **4.165691** (4.16569),
  free room = **868 656** (868 656) - plus M_012's independently reported 16 080.
- **ROOT CAUSE - MIXED EQUALITY DISCIPLINE.** D96's own spectrum arrives via a real TOLERANT eigensolver
  (A0 = 45, lock 0.802314, free room 51); D96^3's construction used exact-double keying. Same quantity, two rules.
- **WHAT MOVED:** A0 20 812 -> 16 080 (-22.7%); free room 863 924 -> 868 656 (+0.55%); L 0.976477 -> 0.981825
  (+0.55%); **lock release 3.948614 -> 4.165691 (+5.50%)**; max multiplicity 562 -> 738 (+31.3%).
  **The asymmetry is the point:** the count moves 23% while the fractions move 0.55% - which is why the
  conclusions survive and the counts do not.
- **7 UNCHANGED / 2 BOUNDARY / 6 REFUTED** over fifteen G-series claims (computed, never typed - G_027).
  REFUTED: G_002 A0 = 20 812; G_002 max mult 562; G_003 D96^3-vs-D96 0.276596 -> **0.263158**; G_005 cube lock
  3.948614 -> **4.165691**; G_005 cube L 0.97648 -> **0.981825**; G_006 restatement. **In their own tolerances:**
  G_003 **13 438x**, G_005 lock **21 708x**, G_005 L **535x**. BOUNDARY: "97.6% energy-free" -> **98.2%**;
  cube contraction at m = 200 **7.33 -> 7.10**.
- **UNCHANGED = every inequality, identity and ordering.** Cube still ~98% energy-free (> 0.97 either way);
  free room still exactly N - A0 and still the D_048 latent fraction L; **D96's own invariants never moved**;
  the ordering arrangement > degeneracy > lattice-average > survivor compression is intact; still CONTROLLABLE.
  **The refuted items are numbers, not conclusions.**
- **BLAST RADIUS (shared infrastructure), measured:** G_002/003/005/006 - four assertions updated; G_033 - live
  scanner now excludes TWO meta-audits; **T_012 - headline UNCHANGED** (D96^3 -> S-inf = 16, distance 3; random 17
  nearest), sub-claim 21 -> **14**; **T_013** - assertions pass, A = **16 079**, C = **1004.9**, max mult **738**,
  "~150x" -> "~114x"; **T_014, T_008-T_011, D_047 - ALL PASS UNCHANGED** (D_047 needed nothing; it was correct).

Tests: `Y_G_034_Tests` 6/6. Group G: **271/271**. Affected set: **321/321**.

## G_035 - Temporal Independence Audit: what survives without the conformal metric? (COMPLETE, SURVIVES)

**AT's TIME HALF IS NOT HOSTAGE TO ITS SPACE HALF.** Classifying all 36 group-G suites by the metric component
each result needs: **25 need g00 only (SURVIVES), 8 need g_ij (BOUNDARY), 3 require conformal flatness (REFUTED)**.
*(Registry extension: G_036 was later added as Spatial/BOUNDARY, triaged `ScanDetectsIt: false` - the counts become
**25/9/3 of 37**, boundary index still 23, no prior classification changed.)*
The axis maps 1:1 onto the labels, so the verdict is not a judgement call - a result requiring only g00 cannot be
affected by removing conformality, because it never refers to the spatial block at all.

- **THE SHARP BOUNDARY: G_020 -> G_021.** EVERY audit from G_001 to G_020 is g00-ONLY - the source law, the clock
  law, control/realizability, SI magnitudes, calibration, the relaxation mechanism, the actuation chain,
  mass-independence, watch-ontology, the second-order signature, the neutron-star redshift. The spatial
  requirement begins **EXACTLY at G_021 (Light-Propagation)** - the first audit to ask for light bending.
  Boundary index **23 of 36** (temporal era 22 entries), computed from the registry, never chosen.
  BOUNDARY eight: G_021, G_022, G_023, G_025, G_028, G_029, G_030, G_033. REFUTED three: G_024, G_031, G_032.
  Metric-free three: G_026, G_027, G_034.
- **THE MINIMAL TIME SECTOR:** 1 metric function (g00 = -rho^(2/d)) + 1 scalar (rho) + 1 exponent (1/d, d = 3 by
  rotation self-duality, M_013) - **NO spatial metric, NO conformal factor, NO reference eta.** The time half is
  ONE EQUATION ON ONE SCALAR.
- **THE SOURCE LAW IS THE CLOCK LAW:** A = 0.5 ln(-g00) = sigma = (1/d) ln rho (the clock potential = Phi/c^2);
  dtau/dt = sqrt(-g00) = rho^(1/d); and **a = -(1/d) grad ln rho = -grad A** - the NEGATIVE gradient of the time
  exponent, exactly as an attractive force requires (G_028). Verified on a PEAKED profile: the acceleration points
  INWARD with the peak as an equilibrium.
- **THE ARITY PROOF (structural, not numerical):** the temporal observables are functions whose signature
  contains NO B (G00, ClockOf, ClockPotentialOf, RedshiftOf, SourceAccelerationOf, SecondOrderRatioOf); the spatial
  ones DO take B (GammaOf, AdmittedBand, SurvivorB). A result that cannot be CALLED with B cannot REQUIRE B.
- **THE CHECK IS HONEST ABOUT ITS LIMITS.** The live scan strips COMMENTS AND STRING LITERALS (so a narrative
  mention cannot mask a dependency). **TWO patterns were removed after measurement** - `double b` matched a
  LEAST-SQUARES SLOPE (G_006) and a TRIDIAGONAL PARAMETER (G_007, G_016b); `K(` matched any method. Bare `psi` is
  EXCLUDED ON PURPOSE: it names the WAVEFUNCTION in the density era (|psi|^2 = rho, G_014) and the metric's
  traceless face elsewhere. **Rule adopted: a token qualifies as a marker only if it cannot plausibly mean
  anything else.** Two claims declare ScanDetectsIt = false WITH WRITTEN REASONS (G_025: an exponent in
  rho*exp(-d*psi/(d-1)); G_033: no metric vocabulary at all, a group-theoretic T2g argument). Check strength is
  two-directional: HARD (asserted) - ClockOnly must show ZERO executable spatial references, holds for all 25;
  SOFT - spatial/conformal should show at least one unless explicitly triaged.

**CONSEQUENCE:** G_031 showed conformal flatness IS the counting measure; G_032 showed it is an ASSUMED PRIMITIVE
eta whose imposition is REFUTED (gamma = -1, Cassini 8.6957e4 sigma). G_035 shows the cost is CONFINED - **the
conformal problem is confined to the space sector.** The dilemma is real but it is ABOUT SPACE.

Tests: `Y_G_035_Tests` 6/6. Group G: **277/277**.

## G_036 - Temporal Core Test Audit: can the core be tested without any spatial metric? (COMPLETE, BOUNDARY)

**THE OBSERVABLE IS PURE BUT THE PRECISION IS BORROWED.** G_035 proved the temporal core is **independent**;
G_036 asks whether it is **testable** using only g00, the clock law and the source law (gamma, lensing, Shapiro
delay and g_ij all rejected). Answer: a sharp **BOUNDARY**, and the obstacle is **not the signal**.

- **THE OBSERVABLE IS PURE:** AT `z = e^x - 1` from `g00 = -e^(-2x)`; GR `z = 1/sqrt(1-2x) - 1`. They **agree at
  O(x)** and **first differ at O(x^2)**: `dz = x^2 + (7/3)x^3 + (13/3)x^4`. `z` depends on **g00 ALONE** - no
  gamma, no lensing, no Shapiro, no g_ij anywhere in the measurement. The temporal observables are functions whose
  **signature contains no B** (G_035's arity argument). **G_020's published figures reproduced EXACTLY:**
  dz = **0.125628** (G_020: 0.1256) at x = 0.247002; at x = 0.187982, z_AT = 0.206812 and z_GR = 0.265888.
- **THE ARENA IS NEUTRON STARS ONLY.** `|dz| > sigma_z` needs a compactness threshold that CLIMBS with precision:
  sigma_z = 1e-3 -> **x >= 0.030494**; 0.01 -> 0.089439; G_020's equal split 0.017766 -> **0.115094**;
  0.025125 -> 0.133350. Weak-field arenas (tower 2.45e-15, optical clock 1.1e-18, Sun 2.12e-6, binary pulsar
  1e-6, white dwarf 1e-4) would need sigma_z between **1e-30 and 1e-12** - nine to twelve orders out of reach.
  This is the arithmetic behind G_019's "only neutron stars are a live arena".
- **THE WEAK-FIELD SPLIT IS UNREPRESENTABLE, NOT JUST UNMEASURABLE.** Each law carries ~1 ulp = **2.2204e-16** of
  absolute error while the true split at the Pound-Rebka compactness is **6.0025e-30**. **Measured:** the direct
  subtraction `z_GR - z_AT` returns **-7.5093e-18** at x = 2.45e-15 - **wrong magnitude AND wrong sign** - and at
  x = 1.1e-18, `z_GR` is **annihilated outright** (exactly 0). The split can be STATED but not COMPUTED BY
  SUBTRACTION; the audit uses the series below x = 1e-3.
- **THE BOTTLENECK IS THE COMPACTNESS, NOT THE REDSHIFT.** G_020's budget re-derived: **sigma_x/x = 13.73 %**
  achieved against **3.661 %** needed for 5 sigma - still **3.75x** - with **1.334 sigma** attainable today. The
  signal is **44.84 %** of z_AT. **AND FOR A NEUTRON STAR THE COMPACTNESS IS ITSELF OBTAINED FROM LIGHT BENDING:**
  pulse-profile modelling fits thermal emission from hot spots *including bending* - the spatial sector G_032
  showed to be an ASSUMED PRIMITIVE. **CENTRAL FINDING: the observable is pure, but the number required to READ
  it is not.** Hence BOUNDARY rather than TESTABLE.
- **A PURE ROUTE EXISTS AND IS ~3x SHORT:** `R_inf = R/sqrt(1-2x)` from a thermal flux plus a parallax distance is
  a **g00 effect** (photon energy and arrival rate, NOT bending), so `{z, R_inf}` solves `{M, R}` with **no light
  bending at all**. Distance and atmosphere systematics keep sigma_x/x above the ~3.7 % needed.
- **THE FIRST EXPERIMENT: Pound & Rebka (1960)** - the 22.5 m tower redshift, x = 2.45e-15. **Pure g00**, no light
  bending anywhere in the measurement, and the first probe of the core at all - but it reaches only the
  **first-order** term, the one AT **shares with GR** (x^2 = 6.0e-30). **No experiment has yet probed the core's
  distinctive content.** Frontier: a compact-object `{z, R_inf}` measurement at **sigma_x/x <= 3.7 %**.

**CONSEQUENCE:** the temporal core is the one part of the theory that is both **independent** (G_035) and, in
principle, **cleanly testable** (G_036) - which makes closing the sigma_x gap more valuable than adding another
weak-field clock in a regime the theory was never going to be distinguished in.

Tests: `Y_G_036_Tests` 6/6. Group G: **283/283**.

## Light Bending - does AT need space curvature, or can time alone do it? (answered 2026-09-13)

**AT NEEDS A SPATIAL SECTOR, AND TIME ALONE CANNOT REPLACE IT.** This settles the recurring "light bending was
solved long ago" confusion quantitatively.

| candidate spatial rule | gamma | solar grazing deflection | Cassini (1.0000210 +/- 2.3e-5) |
|---|---|---|---|
| **time-tick only** (no space curvature) | 0 | **0.8750 arcsec** (half) | **43,479 sigma** - EXCLUDED |
| **AT's DERIVED conformal metric** (the counting measure) | **-1** | **0.0000 arcsec** (none) | **86,957 sigma** - EXCLUDED |
| AT's surviving spatial rule (G_029) `g_rr = 2 - rho^(2/d)` | **+1** | 1.7500 arcsec | 1 sigma - PASSES |
| GR `g_rr = (1-2x)^-1` | +1 | 1.7500 arcsec | 1 sigma |

- Measured deflection is `((1+gamma)/2) x 4GM/(c^2 b)`. The **spatial half is required**: gamma = 0 (time dilation
  alone) gives exactly **half** the observed bending, excluded at **43,479 sigma**. A changing tick rate cannot
  explain light bending.
- AT does **NOT** import GR's space curvature. It DERIVES its spatial structure (G_021/G_031), and the derived form
  is **conformal**, giving **gamma = -1 exactly** - zero bending, zero Shapiro delay - which is why the G-chain had
  to retract it (G_024/G_025) and why G_032 calls imposing it REFUTED.
- What survives every constraint is **G_029's postulate `g_rr = 2 - rho^(2/d)`** (i.e. `B = 0.5 ln(2 - e^(-2x))`),
  which gives **gamma = +1 exactly** - Cassini passes at 1 sigma - and is **NOT conformal** (A != B). It is a
  POSTULATE: G_030 proved the NO-GO that the spatial sector is not derivable.
- **Same O(x^2) theme as the rest of the theory:** AT's survivor and GR agree at O(x) and differ at O(x^2). In
  `g_rr` the relative difference is **-2.7e-11 at solar compactness** (invisible, far below Cassini's 2.3e-5) but
  **-29.7 % at neutron-star compactness x = 0.247002** - so the spatial sector is indistinguishable from GR in the
  solar system and sharply distinguishable at compact objects.

## G_038 - Measure-Decomposition Audit: can the clock bend light on its own? (COMPLETE, REFUTED + DERIVED)

**TIME DOES BEND LIGHT - IT SUPPLIES EXACTLY HALF - BUT THE REDSHIFT FORBIDS IT SUPPLYING THE OTHER HALF, AND THIS
MAKES THE FIRST-ORDER SPATIAL COEFFICIENT DERIVED RATHER THAN POSTULATED.** Raised by the observation that
curvature is a change in MEASURE ("space is distance bending a little") and that time is a measure too, so time
"should be able to do the same".

- **LIGHT RESPONDS ONLY TO THE DIFFERENCE:** with `n = e^(B-A)`, `n - 1 = B - A = (+B distance) + (-A clock)`. The
  clock's share is exactly **1/(1+gamma)**: **100 % at gamma = 0**, **50 % at gamma = 1**, and at **gamma = -1 the
  halves are equal and opposite so the net is ZERO** - AT's case. **The intuition is correct: time does bend light.**
- **BUT A CONFORMAL CHANGE CANNOT BEND LIGHT AT ALL:** `B = A` for ANY conformal factor gives `n = e^(B-A) = 1`
  EXACTLY - verified across x = 1e-12, 1e-6, 1e-3, 0.1, 0.3, where n - 1 is identically zero. This is a THEOREM,
  not a numerical accident: **a conformal factor maps null geodesics to null geodesics.** So "time and space
  bending equally" is INVISIBLE to light, and making the clock bend harder makes space bend equally harder - the
  two cancel. That is precisely AT's position (A = B = sigma).
- **THE COMPENSATION ROUTE WORKS - AND IS EXCLUDED BY THE REDSHIFT.** Keeping space FLAT (B = 0) and doubling the
  clock (A = -2x) gives n - 1 = 2x, hence the FULL deflection (a = 2) - at **TWICE the redshift** (z = 4.24e-6 vs
  the measured 2.12e-6). The solar-limb redshift is measured as `GM/(Rc^2) = 2.12e-6` to about **1 %**
  (Pound-Rebka first order, GPS, solar lines), and AT already reproduces it with A = -x (which is why the G_009
  clock law passes). **A is PINNED - the redshift is what forbids compensating via time.**
- **THEREFORE B = +x IS FORCED - A REFINEMENT OF G_029/G_030 (the two-level rule).** With A measured as -x, the
  deflection B - A = 2x leaves exactly one choice. **B's FIRST-ORDER COEFFICIENT IS DERIVED** (jointly from the
  measured redshift and the measured deflection - no freedom at O(x)); only **its O(x^2) COMPLETION IS BOUNDARY**.
  Same structure the project already uses for the 3-family window (D_028/D_040): a derived VALUE with a boundary
  WINDOW. Remaining freedom, measured: `g_rr` survivor vs GR = **-2.697e-11** relative at solar compactness,
  **-29.68 %** at neutron-star x = 0.247002.
- **THE MEDIUM ESCAPE IS UNAVAILABLE IN AT (not merely excluded).** AT has **no graviton** - the tensor sector is
  the massless **spin-2 psi field** (`Box psi_mu_nu = 0`, MinimalPsiEquation), against which GW170817 bounds
  light's propagation. But any AT-natural index is built from **rho**, and rho sources the metric INCLUDING that
  sector, so a rho-based index acts on light and on the metric sector IDENTICALLY - it IS a metric, and
  `n - 1 = -(1+gamma)*Phi/c^2` applies directly (G_037).

**CONSEQUENCE FOR THE SPATIAL SECTOR:** the measure intuition does not remove the need for a spatial sector - it
explains IN ONE LINE why AT's spatial sector is wrong by a **sign** and a **factor of two** at first order
(`B = -x` where `+x` is required), and why the clock cannot repair it. It also shows the G_029 postulate is far
less arbitrary than G_029 alone suggested: its leading behaviour is fixed by two measurements.

Tests: `Y_G_038_Tests` 7/7. Group G: **297/297**.

## G_037 - Refractive Lens Audit: can light bend without space bending? (COMPLETE, REFUTED)

**AT CANNOT GET BENDING FROM A REFRACTIVE INDEX WITHOUT SPACE CURVATURE - THE INDEX *IS* THE PPN gamma.**
This settles the TRM-era idea (`n_eff = 2 + lambda_time*phi + lambda_space*phi^2*|mu_dot|`, `c_eff = c0/n_eff`),
which reported a deflection matching GR.

- **THE IDENTITY:** for a static metric, `n = e^(B-A)`, and with `A = phi/c^2`, `B = -gamma*phi/c^2`:
  **`n - 1 = -(1+gamma)*phi/c^2`**, so the coefficient `a` in `n = 1 + a*GM/(c^2 r)` is **exactly `a = 1 + gamma`**,
  and **`dtheta = 2a*GM/(c^2 b)`** (closed form checked numerically: 4.000000 at a = 2, 2.000000 at a = 1).
  **A static, non-dispersive index is not an alternative to space curvature - it is the SAME OBJECT in different
  variables.** So "bending without space bending" has only two readings, and both are already decided.
- **AT's DERIVED conformal sector (A = B; G_031) has n = 1 and bends NOTHING** - this is G_032's Cassini
  refutation (8.6957e4 sigma) restated optically, with a one-line reason: a conformal factor maps null geodesics
  to null geodesics. The three cases: conformal `gamma = -1, n-1 = 0, deflection 0`; time-only `gamma = 0,
  n-1 = +1x, HALF`; GR `gamma = +1, n-1 = +2x, full`.
- **THE RATE TERM CANNOT SUPPLY THE MISSING HALF.** The needed `n - 1 = 2*phi` is FIRST order; the TRM second
  term `lambda_space*phi^2*|mu_dot|` is SECOND order: **9.43e5x too small at the solar surface** (phi = 2.12e-6,
  needed 4.24e-6 vs phi^2 = 4.49e-12), 2.0e4x at a white dwarf, **8.1x at J0740+6620**. Only the LINEAR term can
  carry the half - and by the identity that term IS the spatial metric function.
- **THE EM-ONLY READING IS EXCLUDED by GW170817.** If the index is a MEDIUM rather than a metric, it affects
  light but not gravitons. Over GW170817/GRB170817A's 40 Mpc (130 Myr light-travel): a galactic-scale index
  (1e-6) delays light by **4.12e9 s = 131 yr against the 1.7 s bound = 2.4e9x**; solar-scale 5.1e9x; cluster-scale
  7.3e10x; even 1e-12 fails by 2.4e3x. The surviving strength is `n - 1 <= 1.7*c/D = 4.13e-16` - nothing like the
  `2*phi` a deflection requires.
- **BENDING AND DELAY ARE NOT INDEPENDENT:** any static index reproducing the bending also reproduces the GR
  Shapiro delay `2GM/c^3*ln(4 r1 r2/b^2)` - one function, so they cannot be tuned separately.
- **THE THREE GENUINE ESCAPES (and only these):** **dispersion** `n(omega)` (lensing is achromatic to ~1 % from
  radio to optical), **birefringence** (vacuum-birefringence limits), and **time dependence** - which is exactly
  the TRM `phi^2*|mu_dot|` signature, a genuinely NON-METRIC ingredient, but O(phi^2), hence compact-object only.

**WHAT SURVIVES FROM THE TRM IDEA:** the OPTICAL LANGUAGE - `n - 1 = -(1+gamma)*phi/c^2` is the cleanest
statement of AT's actual gamma problem and shows the problem is NOT notation; the RATE-DEPENDENT term as a
genuine non-metric compact-object effect (within an order of magnitude at neutron-star compactness); and a NEW
FALSIFICATION HANDLE - any EM-only lens must beat GW170817's 1.7 s bound, which no astrophysically natural
strength does.

**CAVEATS (both now CLOSED - see provenance below):** (1) phi is read as the dimensionless potential `Phi/c^2`;
if TRM's phi is an order-1 order parameter the O(phi^2) conclusion changes. (2) TRM's `{beta,gamma} -> {1,1}` at
kappa->0.3, b->1.248 is a TUNED-TO-TARGET statement; whether `f(kappa,b) -> 1` is derived or fitted cannot be
settled from the published formula.

**TRM PROVENANCE - AND A FALSIFICATION OF MY OWN FIRST READING (2026-09-13).** The TRM code is still on disk, so
the two caveats above were CHECKED directly. The check falsified my first reading and STRENGTHENED the conclusion.
- **`phi` IS the dimensionless potential.** `TRM.Core/Shared/PhotonTransportModel.cs` defines
  `Phi(G,M,c,r) = G*M/(c*c*r)`, so the G_037 reading `phi = Phi/c^2` is CANONICAL. (Caveat 1 closed.)
- **CORRECTION - `lambda_time = 1` does NOT give half.** The acceleration is `ar = -nEff*G*M/(r*r)` and `nEff`
  INCLUDES the leading `2.0`, so the multiplier is `nEff` itself, not `lambda_time`: at the solar limb
  `nEff = 2.000002122`, ratio 1.000001 - **FULL deflection**. My earlier sentence "lambda_time = 1 gives half" read
  `lambda_time` as the index coefficient; the code uses the whole `nEff` as the force multiplier.
- **THE FILE USES BOTH CONVENTIONS AT ONCE.** Time accumulation writes `(nEff - 2.0) * v` (physical index is
  `nEff - 2` -> HALF); the acceleration writes `-nEff*G*M/(r*r)` (full). One function, two conventions. The
  reported result depends on which line you read, so the model is not well-defined until one is chosen.
- **SO THE FULL DEFLECTION COMES FROM A LITERAL `2.0`.** `ar = -nEff*G*M/r^2` with `nEff ~ 2` is the "2x Newton"
  flat-space photon law. By G_037's identity (`a = 1 + gamma`) that literal 2 **IS `gamma = 1`**. So "no space
  bending" is false - it is space bending typed in as a constant, and it is exactly AT's G_029 postulate in other
  variables, so it removes nothing.
- **THE TEST SUITE CANNOT SUPPORT "MATCHES GR".** `EL04` asserts `ratio in [0.95, 1.08]` (gamma 0.90..1.16),
  `ratioEuler in [0.85, 1.25]` (gamma 0.70..1.50) and `|EL-TRM|/Schw <= 0.30` (branches may disagree by 30%);
  `EL03` allows `[0.70, 1.25]` (gamma 0.40..1.50) with `relDiff <= 0.35`. Widest accepted gamma window = 1.10 wide
  vs Cassini's 4.6e-5 - **23,913x looser**. And every one runs `G=1, c=1, b=1` at `epsilon = 1e-3..1e-2`, i.e.
  **472x..4,717x OUTSIDE the solar regime** (phi_sun = 2.12e-6). Solar-compactness deflection was never tested.
- **THE ONE NON-METRIC CHANNEL IS DEAD.** To lift `lambda_s*phi^2*|mu_dot|` to first order needs
  `|mu_dot| = lambda_t/(lambda_s*phi) = 1.57e4` at solar compactness; the code's own `ComputeAbsDmuDtBase` gives
  `|mu_dot| ~ O(1.8e-6 .. 0.43)` /s there -> **shortfall >= 3.6e4**. With `|mu_dot| ~ 1` the term is `1.35e-10`
  against the `2.12e-6` it must match (1.57e4x short).
- **TWO MORE BLOCKERS.** `|mu_dot|` is a NEW PRIMITIVE (a directional-change rate field); AT's temporal core is
  `rho -> g00 -> clock` (G_035) with no such variable and G_029/G_030's no-new-primitive rule forbids adding one.
  And AT's DERIVED metric is conformal (`A = B` -> `n = 1` exactly, gamma = -1, G_032), so any nonzero index
  coefficient BREAKS conformality - which by G_037 means it IS the spatial metric.
- **TRM'S OWN DOCS ALREADY SAID SO.** `docs/Archive/TRM_Geodesic_Derivation.md` calls the second term "a natural
  **candidate** for the missing spatial / **curvature-like** contribution" and lists *"`lambda_s` can be derived
  from a dimensionless coupling structure"* as the NEXT OPEN STEP (`20/17` itself is section 12, "Open Derivation
  Task"). `V3_4/main.tex` nonclaims: *"No GR replacement is claimed."* Slide content (n_eff, PPN, deflection)
  reflects the TRANSPORT SUBSYSTEM (TRM34-41, TRM78), which the published paper chose not to claim.
- **NET (checked, not assumed): the TRM index does NOT replace space curvature in AT.** Full deflection -> it IS
  `gamma = 1`, i.e. AT's G_029 postulate renamed. Half -> 43,479 sigma. Recovering the second half -> needs a
  primitive AT's no-go forbids, and is 3.6e4x short even if granted. **This is an independent HISTORICAL
  confirmation of G_029/G_030: the only ingredient that ever worked was the literal `gamma = 1`, which G_030
  proved cannot be derived.**

Tests: `Y_G_037_Tests` 7/7. Group G: **297/297**.

## ResearchY-E_001 - Electromagnetism Inventory Audit (COMPLETE, BOUNDARY)

**AT'S ELECTROMAGNETIC KINEMATICS IS DERIVED; ITS DYNAMICS IS DECLARED BUT NEVER COMPUTED.** Group E opens with an
inventory of charge, current, gauge symmetry, Maxwell equations, vector potential, photon sector and wave equation.

- **A FIRST DRAFT GOT IT WRONG, AND THE LIVE SCAN CAUGHT IT.** The draft claimed the EM dynamics was absent - no
  current, no kinetic term, no Lagrangian. FALSE: `ResearchXH/LagrangianOrigin.cs` (QG244) contains ALL of them -
  the Noether currents J^μ_em/J^μ_W/J^μ_s, `F^a_μν = ∂_μA^a_ν - ∂_νA^a_μ + g f^abc A^b_μ A^c_ν`, the kinetic term
  `L_gauge = -(1/4)F^a F^a`, `D_μ`, the matter term and the full density `L = -(1/4)F^aF^a + i psi-bar gamma^mu D_mu psi - m psi-bar psi`
  - **every one as a member returning a STRING.** And the "derived" booleans are CONJUNCTIONS of other audits'
  predicates (`NoetherCurrentsExist`, `QedLagrangianDerived`). Both facts are checked mechanically.
- **THE PROGRAM ALREADY KNEW:** QG242 recorded the gauge dynamics (interaction Lagrangian, vertices, propagators)
  as **HOSTED/OPEN**; QG243/244 close it by declaration. Hence ASSUMED, not DERIVED.
- **CONSERVATION IS NOT THE FIELD EQUATION.** What QG243 offers as its field equation is **∂_μJ^μ = 0** - kinematic.
  The **sourced Maxwell equation ∂_μF^μν = J^ν** is never written or solved, and **no massless spin-1 wave equation
  exists** (AT's one massless wave equation is the spin-2 Fierz-Pauli `Box psi_μν = 0`).
- **THE LAGRANGIAN IS SELECTED, NOT DERIVED:** the file says its form is "the unique minimal action consistent with
  the D96 symmetries" - a minimality argument, and the form written IS the conventional minimal one. AT derives the
  SYMMETRIES and the COUPLINGS (its real achievement); the functional form is selected.
- **TWO CONTRADICTIONS:** (a) **U(1) has two incompatible origins** - Z_96 ⊂ D96 (QG161) vs Aut(S^1) of the vortex
  moduli space (AT-X050), neither referencing the other (the G_026 pattern); (b) **α disagrees with itself** -
  QG162 gives `1/α = 95 + 42 = 137` (0.03 % of 137.036) while `FineStructureAnalyzer` gives **α^-1 ≈ 100**, concedes
  "not a precise derivation", and names α "the LARGEST REMAINING FREE PARAMETER in AT".
- **ONE CLAIM IS NOT EVIDENCE:** `GaugeSymmetryAnalyzer.SimulateEmergence` is `new Random(42)` with hand-picked
  thresholds (0.15/0.07/0.18/0.02) presented as a "COMPUTATIONAL EXPERIMENT"; its conclusion is a function of the
  literals. The mechanism claim stands; the simulation is not support (G_027 applied to group E).
- **MINIMAL ROUTE TO MAXWELL - three steps, only the third is new physics:** (i) COMPUTE what is declared (turn
  `ConservedCurrents()` into a real Noether current); (ii) make the action a real functional, not a string;
  (iii) elevate from ∂_μJ^μ = 0 to the SOURCED ∂_μF^μν = J^ν and read off `Box A_μ = 0`.
- **INSTRUMENT DEFECT FOUND - IT AFFECTS G_027 / G_033 / G_035.** Those scanners strip with a PER-LINE regex, which
  cannot see a verbatim (`@"..."`) string spanning lines - and this repo writes most report text in exactly such
  blocks, so PROSE IS COUNTED AS EXECUTABLE CODE. Live proof on `J_Q` (pure prose): per-line strip counts >= 1,
  whole-file state machine counts 0. The fix is `AT.Core/ResearchXH/AtSourceScan.cs`; **the three named audits
  should be re-run with it.**
- *Related:* `J_Q` is AT's only current-like object - a **proto-matter condensate** current (continuity equation plus
  a Fick/drift form), stated ONLY in prose, with no gauge index and no coupling to A_μ. A candidate PRECURSOR of the
  electromagnetic current, not that current.

Tests: `Y_E_001_Tests` 7/7. Group E: **26/26**.

## ResearchY-E_002 - Field Equation Derivation Audit (COMPLETE, BOUNDARY)

**AT CAN DERIVE THE FIELD EQUATION - BY STANDARD MEANS, WHICH IS NOT THE SAME AS DERIVING IT.** E_001 found the
dynamics *declared, never computed*. E_002 asks the follow-up directly: can AT derive `dF = 0` and
`∂F = J`? **Both are derivable, and this audit derives them by computation rather than asserting them - but the
premises the derivation needs are not AT's.**

- **`dF = 0` IS AN IDENTITY, AND IT IS FREE.** With F = dA the cyclic sum vanishes for ANY A because partials
  commute - and on a uniform grid it vanishes **EXACTLY**, because the cross second-differences cancel term by
  term. The computed residual is **5.551e-15, ROUNDOFF and FLAT in h** (2.776e-16 at h = 0.4, 5.551e-15 at
  h = 0.01) - an *algebraic cancellation*, not a converging discretisation error. **It is not a dynamical
  achievement**: it costs nothing and is available to anyone who writes F = dA. Its only CONTENT is *"no magnetic
  charge"*, demonstrated by fields that FAIL it - a directly-built antisymmetric F (residual **2.0**, constant in
  h) and the radial `B = r̂`, whose divergence is `2/r` (computed **4.64-4.78**), so `B = r̂` is not any curl.
- **`∂F = J` IS DERIVED HERE, IN FULL - BY ACTUALLY DOING THE VARIATION.** The audit builds
  `S = Σ h⁴[−¼F_μνF_μν − J_μA_μ]` on a periodic 4-D lattice and computes `∂S/∂A_ν(y)` by **brute force**
  (perturb one link, recompute the whole action twice). Brute force vs the closed form agrees to **3.945e-12**
  relative (absolute 1.364e-12), and with `J` built from `A` the stationarity residual is **4.036e-12** against
  **33.76** when the EOM is not satisfied. Since `dS/dA_ν(y) = h³Σ_μ[F_μν(y) − F_μν(y−μ̂)] − h⁴J_ν(y)`,
  **stationarity of the action IS `∂_μF^μν = J^ν`.** Computed dynamics - the element E_001 found missing.
- **CONTINUITY IS DERIVED TWICE, INDEPENDENTLY.** (a) Antisymmetry: `∂_ν∂_μF^μν = 0` (residual **1.554e-14**), and
  the field-sourced current `J := ∂F` is automatically conserved (relative divergence **3.309e-16**).
  (b) Gauge invariance: `ΔS_c = −Σh⁴χ ∂_μJ^μ`, direct change **8.917034e-1** against the divergence form
  **8.917034e-1**, agreement **8.771e-15** - so invariance for every χ forces `∂_μJ^μ = 0`. Control: a
  lattice-periodic non-conserved current has `|∂J| = 2.5` against the analytic `2π/L = 2.618`.
- **THE PHOTON SECTOR IS DERIVED-FROM-GIVEN, ABSENT-AS-DERIVED.** Gauge invariance forbids the mass: the Maxwell
  density moves by **2.235e-13** under `A → A + ∂χ` while the Proca density moves by **5.017e-1**. The
  dispersion follows from the field equation - for `A_ν = ε_νcos(k·x)`, `ε·k = 0`, in **Minkowski** signature the
  divergence is `ε_ν(ω² − κ²)cos(k·x)`, so **8.327e-9** at the null wave vector against **1.539e-1** (analytic
  0.1539) for a non-null one. So `ω = c|k|` and the photon is **exactly massless** - *if* U(1) is a gauge
  symmetry. But AT's only computable massless wave equation is the **spin-2** `□ψ_μν = 0`; numeric massless
  spin-1 wave equations in AT.Core: **0**.
- **WHY BOUNDARY RATHER THAN DERIVED.** The derivation runs on the **action principle, locality, 4-D dimensional
  counting and Lorentz invariance** - premises AT does not supply - and the coupling that would fix the
  normalisation **contradicts itself inside AT** (QG162: `1/α = 95 + 42 = 137`, 0.03 % of 137.036, against
  `FineStructureAnalyzer`'s ~100, whose own review calls α "the LARGEST REMAINING FREE PARAMETER"). AT computes
  **0** field strengths and declares **6** as strings.
- **THE FINDING THAT MATTERS: E_001's defect is one of METHOD, NOT IMPOSSIBILITY.** The Lagrangian AT writes down
  is the standard one, its field equation is the standard one, and both are **correct** - nobody had performed the
  variation. AT never *earned* what it wrote down.
- **THREE OF THE AUDIT'S OWN ERRORS WERE CAUGHT BY ITS OWN TESTS WHILE RUNNING IT**, and are recorded rather than
  silently fixed: (1) the brute-force step must be **1e-2, not 1e-6** - `S` is exactly quadratic in `A`, so the
  central difference is exact for any step and 1e-6 reported the roundoff floor (3.6e-8) as a disagreement;
  (2) the non-conserved control must be **lattice-periodic**, since `J_0 = x₀` makes the forward difference report
  the wrap jump (5) instead of the derivative; (3) the dispersion check must use **Minkowski** signs - Euclidean
  `k² = ω² + κ²` has no real null vector, and a Euclidean draft reported 1.62 where zero was expected.

- **A FOURTH SELF-INFLICTED FINDING, AND IT IS METHODOLOGICAL: AN AUDIT'S OWN DERIVATION MUST BE EXCLUDED FROM
  OTHER AUDITS' SCANS.** This audit's core file reproduces a sourced Maxwell divergence and a massless vacuum
  wave equation *by hand*, to test whether AT has them. E_001's whole-tree scan counted them and **two E_001 tests
  failed** — its two `Missing` components picked up `DocumentedCount 3/7` and `ExecutableCount 0/2`, i.e. E_001
  briefly concluded AT *has* a computable massless spin-1 wave equation, the exact opposite of its own finding.
  Fixed with the established self-reference exclusion (`FieldEquationDerivationAudit.cs` added to E_001's
  `SelfFiles`, reason recorded in the code). **General rule: an audit that DERIVES a standard result will
  otherwise be read as evidence that the theory CONTAINS it.**

Tests: `Y_E_002_Tests` 6/6. Group E: **26/26**.

## ResearchY-E_003 - Photon Ontology Audit (COMPLETE, BOUNDARY)

**AT NEED NOT IMPORT U(1) AS AN AXIOM - AND IT CANNOT SUPPLY THE PHOTON WITHOUT THE CUBIC SUBSTRATE. THE PHOTON
AND THE GRAVITON ARE BLOCKED BY THE SAME MISSING SUBSTRATE.** What would the photon be in AT? Trace
Difference -> Actualization -> Spectrum -> ? and test massless / spin-1 / gauge invariance / Maxwell limit /
propagation.

- **THE OBSTRUCTION IS REPRESENTATION-THEORETIC AND IT IS DECISIVE.** The photon is spin-1, hence the `l = 1`
  (vector / p-wave) sector, hence **THREE dimensions**. The single ring's group D_96 is the **dihedral group of
  order 192** - verified ON THE PERMUTATIONS: rotation order 96, reflection order 2, `s r s^-1 = r^-1`, generated
  order 192 - and its irreps are **4 one-dimensional + 47 two-dimensional** with **4*1 + 47*4 = 192 = the group
  order** (checked for every even n from 4 to 200). **Maximum irrep dimension 2, so the vector sector is
  IMPOSSIBLE on the ring.** The ring's adjacency spectrum pairs `k` with `96-k` (the 47 doublets) and is purely
  scalar: **45 distinct eigenvalues**.
- **AT'S OWN AUDITS ALREADY CONTAIN BOTH HALVES.** M_011 found no O(3) and no vector sector on a single ring;
  M_012 found a genuine dimension-3 irreducible sector on the CUBIC network `C96 □ C96 □ C96` - `l = 1 -> T1(3)`
  with `(chi, chi) = 1` - with group O_h. **And the graviton's traceless part is `l = 2 -> E(2) + T2(3)`, so it
  needs a dimension-3 irrep for the same reason. The photon and the graviton are blocked by the SAME thing.**
  G_033 established what became of that requirement: the metric era **never instantiates 96^3**, because it
  imports 3D space through the primitive **eta**, which G_032 proved is an **assumed input**. **The photon
  therefore inherits exactly gravity's gap.**
- **U(1) IS DERIVED AND COMPACT - BUT GLOBAL.** U(1) = `Z_96` = the rotation subgroup of `Aut(C96(1..6))`: no
  axiom needed, and because `Z_96` is FINITE the emergent U(1) is **COMPACT** - the property that lets a compact
  U(1) with a conserved current carry a massless vector without a Goldstone boson. But the symmetry is **RIGID**:
  the rotation IS an adjacency automorphism while **0 of 6** site-local transpositions are.
- **A PROGRAMME CLAIM FALSIFIED: "the 12 link-directions ARE the 12 gauge generators" (QG161, matching 1+3+8).**
  The offsets `{+-1..+-6}` are **NOT closed under addition** (witness `1 + 95 = 0`, outside the set), their
  additive closure inside Z_96 is **96 not 12**, and the algebra is **abelian** - against `u(1)+su(2)+su(3)`,
  which is **11-dimensional NON-abelian**. Cardinality match, no algebraic map. **BUT the su(2)-from-doublets
  claim PASSES**: the Lie closure of the doublet generators has dimension **3** (genuine su(2)), so the weak
  sector has a real basis while "12 = 12" does not.
- **A CORRECTION TO A FIRST READING, AND IT MATTERS: AT ALREADY HAS A LINK PHASE.** `PhaseOrigin` assigns
  `2*pi/96` per ring link, with a path phase over a full cycle of exactly `2*pi`, a **compact** loop holonomy,
  a two-slit law `2 + 2 cos(delta)` (4.000 at delta = 0, 0.000 at pi) and a phased Born rule. So the missing
  primitive is **NOT the phase** - it is the **SPACETIME INDEX** (these are 1-D ring links, not 4-D) and the
  **DYNAMICS** (the step is the fixed constant `2*pi/N`, not a fluctuating field).
- **AT'S MASSLESS CONTENT, AND ITS SPIN.** The substrate's Laplacian `mu_k = 2k - lambda_k` has **EXACTLY ONE
  zero eigenvalue** - the constant mode, **spin 0** - with gap **0.386351**. The computed massless waves are
  **both scalars** (the zero mode and the `TemporalField` scalar wave equation); the spin-2 `Box psi_mu_nu = 0`
  is **POSTULATED** (`Derived() = false`, `Postulated() = true`); **spin-1 is ABSENT**. This corrects E_001/E_002,
  which described the spin-2 as AT's computable massless wave equation.
- **THE POLARISATION COUNT PINS THE GAP.** A massless spin-1 has exactly TWO transverse polarisations. A scalar
  phase gives **1** (longitudinal); `grad(rho)` gives **1**; a gauge-reduced `A_mu` gives **2**; Proca gives 3.
  **So the cheap AT-native routes are excluded by counting, and the missing primitive is pinned to a
  spacetime-indexed connection.**
- **THREE SELF-CORRECTIONS, ALL CAUGHT BY THE AUDIT'S OWN TESTS AND RECORDED RATHER THAN ABSORBED:** (i) the
  site-dependent test family `p[i] = i + 1 + (a*i) mod 5` degenerates to the RIGID rotation at a = 5, reporting
  one phase that "worked"; (ii) the spectral-degeneracy bound is ONE-SIDED - `k <-> 96-k` makes 49 an UPPER bound
  and the measured count is 45; (iii) AT **does** already have a link phase, found by reconnaissance.
- **THE NATIVE ROUTE, IF TAKEN:** (i) instantiate the cubic `C96 □ C96 □ C96` instead of importing 3D space as
  eta - the SAME step gravity needs; (ii) promote the rigid rotation to a local one (an independent phase per
  link, which is genuinely new structure since a site-dependent phase is not an automorphism); (iii) verify the
  longitudinal mode is removed - which a **compact** U(1) with a conserved current can do without a Goldstone,
  making AT's finite Z_96 an advantage; (iv) the Maxwell limit is then E_002's derivation, needing no new work.

Tests: `Y_E_003_Tests` 7/7. Group E: **26/26**.

## ResearchY-E_004 - Vector Sector Audit (COMPLETE, BOUNDARY)

**THE T1(3) SECTOR IS A REAL VECTOR SECTOR, AND IT IS NOT YET A GAUGE FIELD.** E_003 located the photon's
obstruction in representation theory; E_004 shows how far the representation takes you and where it stops. Can
the D96^3 T1(3) sector support a genuine spin-1 FIELD? Requirements: vector degrees of freedom, two physical
polarisations, gauge redundancy, massless propagation, Maxwell limit. Compare single D96 against D96^3.

- **REQUIREMENT 1 MET - ON D96^3 ONLY.** `l = 1` subducts onto the octahedral group as a SINGLE multiplet **T1**,
  dimension **3**, multiplicity **1** - so it is IRREDUCIBLE. The single ring's maximum irrep dimension is **2**,
  so it has **no vector sector at all**. Requirement 1 is satisfied, by the cubic substrate only.
- **A LORENTZ 4-VECTOR NEEDS *BOTH* SUBSTRATES - SHARPER THAN E_003.** `A_mu = (A_0, A_i)` reduces as
  **A1(1) + T1(3) = 4**. The single ring can supply only the timelike `A_0`; the cubic substrate only the spatial
  `A_i`. **Neither substrate alone can carry a Lorentz vector.**
- **REQUIREMENT 2 MET GIVEN A REASON TO PROJECT.** The transverse projector `P = I - k k^T` has rank **2** for
  every one of 12 momentum directions tested, so the three components reduce to two physical states with one
  removed - *if* there is a reason to remove it.
- **REQUIREMENT 3 NOT MET - AND THIS IS THE DECISIVE COMPUTATION.** Two quadratic forms live on the SAME
  three-dimensional space: the **IDENTITY** form `V.V` (rank **3**, kernel **0** -> **three** physical states, a
  **PROCA** field - and it is the representation's own NATURAL INVARIANT) and the **CURL** form `F_ij F_ij`
  (matrix `2(I - k k^T)`, rank **2**, kernel **1** -> **two** states, MAXWELL-capable). Evaluated on the modes:
  the curl form ANNIHILATES the longitudinal mode EXACTLY (**0.000e0**, against 1.000000 for the identity). An
  irreducible representation has **NO GAUGE ORBIT** - gauge directions: **0 from the representation**, **1 from
  the field space** (the curl form's gradient kernel). **So T1(3) alone delivers a MASSIVE vector**, and the
  gauge redundancy must be imposed from outside.
- **REQUIREMENT 4 MET IN THE LIMIT, NOT ON ANY LATTICE.** The vector sector is gapped: **0.386351** at N = 96.
  But `mu_min * n^2` settles to **~3591** (12.0 -> 0.02435 as n goes 12 -> 384; spread **0.84 %** for n >= 96),
  so `mu_min ~ 3591/n^2 -> 0`. **The gap is a FINITE-SIZE ARTEFACT, not a predicted mass.** The cubic gap is
  **3x** the ring's, by the Cartesian-product sum rule, so it closes too.
- **REQUIREMENT 5 IMPORTED** - E_002's derivation, from premises that are not AT's.
- **WHAT IS MISSING IS NOW LOCATED EXACTLY: THE CHOICE OF KINETIC FORM THAT TAKES THREE STATES TO TWO.** Not the
  phase (E_003 found AT already has one), not the group, and not the vector representation. D96^3 is necessary
  AND sufficient at the level of REPRESENTATION and insufficient at the level of DYNAMICS.

Tests: `Y_E_004_Tests` 6/6. Group E: **26/26**.

## ResearchY-G_039 - Rho Realization Audit (COMPLETE, BOUNDARY)

**FOUR OF FIVE REQUIREMENTS MET BY COMPUTATION, ONE CARRIER IDENTIFIED, AND THE FILTER REDUCES TO ONE BINDING
CONSTRAINT.** *(Requested as G_036, which is already taken by the Temporal Core Test Audit delivered earlier in
this session; the group-G ID space is permanent and is keyed by the index and the classification registry, so the
next free number was used.)* What observable can carry rho while remaining compatible with G_016, G_016b, G_017,
G_018 and G_035? Candidates: occupancy distributions, state populations, degeneracy occupation, attractor
occupation, survivor distributions. Requirements: measurable, not energy, not phase, not information, preserves
the clock law.

- **THE SPECTRUM IS RECOMPUTED, NOT CITED - AND ALL FOUR FIGURES REPRODUCE THE RECORD.** 96 cells; **45** distinct
  levels (A0, matching G_016); multiplicity histogram **{1:1, 2:42, 5:1, 6:1}** (exact match); free room
  **51 = 96 - 45** (G_016/G_018); state dimension **95**; Laplacian trace **1152 = 2 x 576 links**. The audit
  therefore stands on recomputed ground rather than on citations.
- **REQUIREMENTS 2-4 FALL OUT OF A SINGLE OBSERVATION.** Energy is the pairing `E = <lambda, rho>`, ONE number on
  a **95**-dimensional space, so its kernel has dimension **94** - and the audit shows the split is exact:
  **51 (within-multiplet, where lambda is CONSTANT so the energy cannot move) + 43 (level mixing, 45 levels less
  normalisation and energy) = 94**. The same argument applies one level up: the entropy is ALSO one number on the
  same space, so it is lossy by the same **94** dimensions. **Energy and information cannot carry rho.**
- **THE COMPUTED WITNESS** (on the unique m = 6 multiplet, lambda = -2): a 20:1 tilt gives **energy change
  0.000e0 EXACTLY** and **rho change L1 = 1.2666666667**, while shifting the clock by **0.9985774245 =
  86 277.089 s/day**. A 20:1 tilt moves NO energy and nearly a full day per day of clock.
- **THE INFORMATION WITNESS IS CONSTRUCTED, NOT ASSERTED** (bisection): two DIFFERENT occupancy vectors,
  (0.5000000000, 0.5000000000) and (0.7729078048, 0.1135460976, 0.1135460976), share **H = ln 2 = 0.6931471806**
  exactly and separate by **0.7729** in L1. The entropy is a FUNCTION of rho but NOT injective.
- **PHASE IS SEPARATED SECTORIALLY, NOT NUMERICALLY.** rho counts **SITES** while the phase lives on **LINKS**
  (E_003): a phase rotation leaves the counts invariant (**rho change 0.0e0**) while moving the holonomy by
  **0.065450**. The clock law is reproduced to G_016b's own figure.
- **THE CANDIDATES ARE DECIDED BY LOSS, NOT BY MEASURABILITY.** Zero-loss: **occupancy distributions**,
  **attractor occupation**, **survivor distributions** - and these are **ONE object read at three stages**, since
  the dynamics selects one attractor from all initial conditions (G_005/G_008). Refuted as the identity:
  **state populations** and **degeneracy occupation**, because averaging over a multiplet discards the
  **51-dimensional within-multiplet room** - which is the FREE ROOM, and the only part an actuator can actually
  move (G_002/G_012/G_013).
- **THE ONE UNMET REQUIREMENT IS MEASURABILITY, AND THAT IS THE WHOLE POINT OF ASKING SEPARATELY.** G_018 could
  conclude DERIVED for the **IDENTITY** of rho, because for that question G_017 removed an **identification**
  rather than the **quantity**. A **REALIZATION** asks for an **observable**, and the natural laboratory
  realization - reading rho off an optical intensity - is exactly what **G_017 EXCLUDED**. What remains is the
  clock signature, priced by G_004/G_009 as real but orders of magnitude below local sensitivity. **The identity
  is DERIVED (G_018); the realization is BOUNDARY.**
- **THREE OF THE AUDIT'S OWN SLIPS WERE CAUGHT BY ITS OWN TESTS AND RECORDED RATHER THAN ABSORBED:** the
  link-count arithmetic in the report (288 for 576), an equality at the 9th decimal sitting exactly on the
  rounding boundary, and a truncated `(int)` cast of a floating-point trace.
- **A FOURTH WAS CAUGHT BY ANOTHER AUDIT — G_033'S LIVE SCANNER.** The constants were named `Cells`/`Radius`, so
  the D96 scanner filed this audit with the **D96-free** (metric / closure) era even though it recomputes the D96
  ring spectrum. Renamed to **`D96Cells`**; G_033's counts move to **24 substrate / 16 D96-free / 1 comment-only**
  (41 classified suites) and `Y_G_033_Tests` was updated with them.

Registry: added as **ClockOnly -> SURVIVES** (rho, the source law and the clock law are all g00 statements),
triaged `ScanDetectsIt: false`; counts become **26/11/3 of 40**; boundary index unchanged; no prior
classification changed. Tests: `Y_G_039_Tests` 7/7. Group G: **304/304**.

## ResearchY-G_040 - Rho Observable Audit (COMPLETE, REFUTED)

**Question.** Can any measurable quantity retain all 95 dimensions of rho? Candidates: occupancy patterns, mode
populations, detector counts, attractor occupancy, survivor occupancy. Test: dimension retained, information
loss, invertibility.

**Answer: REFUTED — and the ceiling is a SYMMETRY THEOREM, not a detector limitation.**

- **THE GROUND IS RECOMPUTED**: 96 cells, 45 levels, {1:1, 2:42, 5:1, 6:1}, trace 1152, state space **95**.
- **THE SUBSTRATE'S SYMMETRY IS BUILT, NOT ASSERTED**: the 192 permutations of the dihedral group, checked in
  code for distinctness, closure under composition and transitivity (1 orbit on the cells).
- **ONE NUMBER DECIDES THE QUESTION**: the **centralizer algebra** — the space of operators AT can construct —
  has dimension the number of orbitals, computed FOUR independent ways, all **49**: brute-force enumeration of the
  9216 ordered cell pairs; Burnside (1/|G|) Sum fix(g)^2 = **9408/192**; the multiplicity-free irrep
  decomposition (49 irreps, each appearing once); and the sum over the 45 levels of the algebra's restricted rank
  (**1 + 42 x 1 + 3 + 3**).
- **47 DIMENSIONS ARE UNREACHABLE, AND THE AUDIT NAMES THEM**: the state splits as **48 magnitudes + 47
  intra-doublet orientations** — one angle per two-dimensional irrep, and D96 has exactly **47** of those. By
  Schur's lemma a centralizer operator is a scalar inside each irrep.
- **THE LADDER, COMPUTED**: site-addressed cell counts retain **95, loss 0** (but that IS rho, and naming every
  cell is what G_017 excluded); **distance-class contractions** <rho, A_d rho> retain **48, loss 47** (the ceiling
  for anything AT can build); **spectral level populations** retain **44, loss 51** — **exactly G_039's free
  room**. The 4 extra dimensions the contraction rung reclaims are the two multi-irrep levels (m = 5, m = 6,
  three irreps each).
- **TWO WITNESSES MAKE INVERTIBILITY FAIL EXACTLY**: rotating one doublet's orientation by 1.1 rad moves the
  state by **1.997e-1** (L1, contrasty state) or **6.93e-3** (flat state) while moving EVERY contraction by at
  most **2.78e-17**; and all **192** group images of a generic state report ONE distinct reading (spread
  **3.12e-17**) while sitting up to **9.23e-2** apart — a fibre of at least 192-to-1.
- **REGIME CAVEAT, SELF-CAUGHT**: the retained dimension is a GENERIC-STATE statement — a state confined to four
  channels collapses to rank **4**. Both regimes are asserted rather than the convenient one.
- **FIVE SLIPS CAUGHT AND RECORDED, NOT ABSORBED**: (1) the rank threshold must be relative to the WHOLE matrix,
  not to each row — a per-row tolerance accepted a row of pure roundoff (A_24 restricted to channel 1 is exactly
  zero, so its norm 2.8e-32 exceeded 1e-8 times itself), which read every doublet as rank 2 and summed the levels
  to **143** instead of 49; (2) a level's real block is ONE cos/sin pair per channel — a per-mode guard doubles
  the pair and the "projector" is not idempotent (|P^2 - P| = 1/24); (3) a level projector lives in the MODE
  basis, not a cell-indexed mask; (4) the witness must use the invariant contraction <rho, A_d rho>, not the
  equivariant vector A_d rho, which the group permutes and which showed a spurious 2.1e-2 "difference".

Registry: added as **ClockOnly -> SURVIVES**, `ScanDetectsIt: false`; counts become **27/11/3 of 41**. G_039's
BOUNDARY verdict STANDS — G_040 explains its measurability finding rather than revising it. No reclassification.
G_033's live classifier now reads **24** substrate suites (39 classified in total). Tests: `Y_G_040_Tests` 9/9.
## ResearchY-G_041 - Substrate Dimension Audit (COMPLETE, BOUNDARY: one half strengthens, one half weakens)

**Question.** AT needs the ring (d = 1) and, per G_033, the cube (d = 3). What about D96^2 and D96^4 - and is
d = 3 SELECTED or ASSUMED? The family is D96^d (the d-fold tensor product of the same 96-cell circulant), whose
symmetry is the signed-permutation group **B_d of order 2^d*d!** - and **B_3 IS the cubic group O_h of order 48**,
the group E_003/E_004/G_033 used. d = 1 is NOT B_1: AT's ring is PERIODIC, so its group is **dihedral of order
192** (E_003's budget).

**Answer: BOUNDARY - the two halves point opposite ways.**

- **THE LADDER, COMPUTED.** Groups constructed and checked for distinctness; irrep spectra from the
  pair-of-partitions classification `dim(lambda,mu) = C(d,|lambda|)*f^lambda*f^mu` (hook-length formula); and
  **Burnside's Sum d_i^2 = |G| verified as an identity for every d**. Max irrep dim: **2** (ring, order 192),
  **2** (d=2, order 8), **3** (d=3, order 48), **8** (d=4, order 384), **20** (d=5), **80** (d=6).
- **D96^2 FAILS EXACTLY AS THE RING FAILS - this STRENGTHENS G_033.** Max irrep 2 supplies no dimension-3 sector
  (the photon needs T1(3), the trace-free metric needs T2(3)), so **the cube is the MINIMAL working dimension, not
  merely the chosen one**.
- **BUT THE IRREP-SUPPLY ARGUMENT DOES NOT *SELECT* d = 3 - REFUTED AS A SELECTOR (the part the programme has
  been leaning on).** Character inner products over the group give the **SAME signature at every d >= 2**:
  `<V,V> = 1` (the vector is irreducible everywhere), `<W,W> = 2` (the traceless sector ALWAYS splits into exactly
  two pieces - E_003's E + T2 at d=3 was never a d=3 coincidence), `<V,W> = 0` and `<V,A> = 0` (they never mix).
  The signature is **dimension-blind**, so the argument selects **d >= 3**, not d = 3. Only "the vector is the
  largest irrep" breaks, and only from d = 4 (max irrep 3 -> 8 -> 20).
- **TWO INDEPENDENT ACCIDENTS DO PIN d = 3, both computed over the whole ladder and both unique.** (a) The
  **eps/Hodge accident**: dim Lambda^2 = dim V **only at d = 3** (3 = 3) - at d = 2 it is **1, a SCALAR**, at
  d = 4 it is 6, d = 5 it is 10; this is exactly the axial T2(3) E_003/G_033 need. (b) The **polarisation
  match**: massless photon d-1 against graviton (d+1)(d-2)/2, equal **only at d = 3** (**2 and 2**) - d = 2 gives
  **1 and 0**, i.e. NO propagating graviton at all, d = 4 gives 3 and 5.
- **AND THE CHOICE IS OBSERVABLE, NOT CONVENTIONAL.** The clock law is `rho^(1/d)`, so the same 20:1 contrast
  gives **129 415.634 / 86 277.089 / 64 707.817 s/day** at d = 2/3/4 with **9 216 / 884 736 / 84 934 656 modes**.
  **The published 86 277.089 figure (G_016b/G_039/G_040) is a d = 3 number**, and every observable inheriting
  the clock law inherits the dimension.
- **HYPOTHESIS WEAKENED AND RECORDED, NOT SMOOTHED OVER.** G_033's positive claim is STRENGTHENED (the cube is
  minimal). What is weakened is any reading that treats "the cube supplies T1(3) and T2(3)" as *selecting* d = 3:
  the computed signature is dimension-blind and d = 4 also supplies a vector sector. AT's claim to d = 3 therefore
  rests on the eps accident, the polarisation match and the clock exponent - **not** on the irrep supply alone.
- **CAVEATS.** d = 4 is barred by the two accidents, the polarisation mismatch, the clock exponent and cost (96x)
  - NOT by the representation theory. The d = 1 row must use the dihedral group (a periodic ring is not B_1).
  The irrep spectra here are EXACT (finite-group representations), so this audit is immune to the A0 robustness
  problem G_033 documented for spectral level counting.

Registry: added as **ClockOnly -> SURVIVES**, `ScanDetectsIt: false`; counts become **28/11/3 of 42**. No
reclassification. G_033's live classifier now reads **24** substrate suites (40 classified in total). Tests:
`Y_G_041_Tests` 8/8; group G 321/321.
## ResearchY-G_042 - Three-Dimensionality Dependency Audit (COMPLETE, REDUNDANT: ONE ROOT)

**Question.** Are the known selectors of d = 3 independent, or the same structure viewed differently - one root
mechanism or several? INPUTS 1 rotation self-duality d(d-1)/2 = d | 2 Hodge dim Lambda^2 = dim V | 3
polarisation equality d-1 = (d+1)(d-2)/2 | 4 the D96^d representation structure | 5 the clock exponent
rho^(1/d).

**Answer: REDUNDANT - ONE ROOT MECHANISM. The number of INDEPENDENT selectors of d = 3 is 1, not several.**

- **THE DECISIVE IDENTITY, verified for EVERY d:** the graviton carries **dim Lambda^2 - 1 = dim so(d) - 1**
  polarisations against the photon's **dim V - 1**. Subtracting one from each side turns "the polarisations are
  equal" into "dim Lambda^2 = dim V" - **identically, not merely at the shared root**. So input 3 is not a
  coincidence that shares the root; it is the SAME EQUATION reached by a different derivation (little-group
  counting instead of tensor algebra).
- **INPUTS 1 AND 2 ARE THE SAME FORMULA**: dim Lambda^2 = d(d-1)/2 = the number of independent rotations. All
  three geometric inputs reduce to **d(d-3) = 0**, roots **{0, 3}** - physically: *the number of directions
  equals the number of independent rotations* (the cross product / Hodge dual).
- **THE DEPENDENCY GRAPH IS COMPUTED, NOT DRAWN** (A implies B iff S(A) is contained in S(B); all solution sets
  obtained by EVALUATING the predicates over d = 1..12): S1 = S2 = S3 = **{3}**; S4a (supplies a 3-dim irrep) =
  **{3..12}**; S4b (the vector is the largest irrep) = **{1,2,3}**; S5 (clock) = **{1..12}**. Nothing implies
  selector 1.
- **4a AND 4b = {3}**: the representation conjunction **reproduces the root exactly** - which is why G_033/G_041
  treated it as a second mechanism - but the root implies both halves, so it is **DERIVED FROM**, not
  independent.
- **CLASSIFICATION: 1 INDEPENDENT** (rotation self-duality - the root); **2 REDUNDANT** (Hodge duality; the
  polarisation equality - same solution set AND same equation); **2 DERIVED FROM** (4a, 4b); **1 REDUNDANT**
  (the clock law - it holds at EVERY d, so it is a LAW, not a selector: rho^(1/d) is what makes the choice
  OBSERVABLE, REDUNDANT as a selector and INDEPENDENT in role).
- **THE ACCIDENTS BELONG TO DIFFERENT DIMENSIONS - checked, not assumed:** bivectors collapse to a **scalar** at
  d = 2 (dim Lambda^2 = 1), **ARE** the vectors at d = 3, split **self-dual / anti-self-dual** at d = 4 (dim 6 =
  3 + 3; even Lambda^2 also at d = 5, 8, 9, 12), and the cross product returns at **d = 7** (Hurwitz - cited,
  not computed). The family of special dimensions is real and its memberships differ, but **only one membership
  selects d = 3, and it does so once**.
- **REFINES / WEAKENS G_041.** G_041 called the eps/Hodge accident and the polarisation match "two independent
  accidents". **They are one condition with two derivations, so the number of independent reasons for d = 3 is
  1, not 2.** G_041's positive results and its **BOUNDARY verdict STAND UNCHANGED** (d = 4 is still not excluded
  by the representations). Carried in code as `SubstrateDimensionAudit.RefinementFromG042()`, printed in G_041's
  own report, and noted in the G_041 doc and registry narrative.
- **A MARKDOWN DEFECT FOUND AND FIXED WHILE WRITING THIS:** the `ResearchY_Index.md` rows inserted by G_041 and
  G_042 had been mashed onto one physical line (the row separators were lost on insertion), so the table rows
  were not separate lines. Splitting them restored the table; the G_041 row had been affected since its own
  commit.

Registry: added as **ClockOnly -> SURVIVES**, `ScanDetectsIt: false`; counts become **29/11/3 of 43**. No
reclassification. G_033's live classifier now reads **24** substrate suites (41 classified in total). Tests:
`Y_G_042_Tests` 7/7; group G 328/328.
## ResearchY-G_043 - Minimality Audit (COMPLETE, BOUNDARY: outcome over-determined, mechanism not)

**Question.** Why does nature stop at the first working dimension (d = 3) instead of continuing to d = 4, 5, ...?
Is there a quantity optimal ONLY at d = 3? Compared D96^3/D96^4/D96^5; measured photon and graviton
polarisations, the Hodge mismatch, representation growth, state-space growth, clock-law scaling, plus two
observability measures and one count bound.

**Answer: BOUNDARY - the OUTCOME is over-determined and the MECHANISM is undetermined.**

- **NINE FAMILIES MEASURED: SEVEN MONOTONE (no optimum), ONE UNIQUELY ZERO AT d = 3.** The only optimal-at-3
  family is the **Hodge mismatch** |dim Lambda^2 - dim V| = 1, **0**, 2, 5, 9 for d = 2..6 - which is **G_042's
  root**, so it is not a new mechanism.
- **THE STOP IS NOT AN ECONOMY OPTIMUM.** Every growth family gets WORSE with d: representation growth 2, 3, 8,
  20, 80; state space 96^d - 1; observability fraction (orbitals/state) 0.516, 0.133, 0.0235, 0.00319; states per
  observable 1.94, 7.52, 42.5, 314, 2841. An economy principle would stop at d = 1 or 2 - **which do not work**.
- **THE ONE OTHER EXTREMAL QUANTITY IS THE SAME FAMILY.** The count bound "the graviton's polarisations fit
  within the number of directions" holds through d = 3 (`d^2 - 3d - 2 <= 0`) - the same quadratic as the Hodge
  equality (`d^2 - 3d = 0`) at a different offset. One family, two members.
- **THE REAL FINDING - FIVE CRITERIA TURN OVER AT THE SAME DIMENSION.** Three turn **ON at d = 3** (a dimension-3
  irrep is supplied {3..8}, the graviton count becomes positive {3..8}, the Hodge mismatch vanishes {3}); two
  turn **OFF after it** (the graviton fits within the dimensions {1,2,3}, the vector is the largest irrep
  {1,2,3}). Their **intersection is exactly {3}**. So "the first dimension that works" and "the only dimension
  where the mismatch vanishes" are the SAME dimension, and no measured quantity can separate them: the audit can
  say WHY each candidate fails at d = 4, but not WHICH is doing the work.
- **BUT THE OUTCOME IS ROBUST, NOT KNIFE-EDGE.** The whole family `d^2 - 3d - c` separates 3 from 4 for
  **c = 0, 1, 2, 3**, a window containing both selected members (equality c = 0, bound c = 2). So d = 4 is
  excluded by a family of conditions, not one tuned coincidence.
- **CROSS-AUDIT CHECK.** The orbital count of the D96^d lattice is `C(48 + d, d)`, which gives **49 at d = 1** -
  exactly the centralizer dimension **G_040** computed for the ring from the dihedral group. Two audits, two
  different routes, the same integer.

Registry: added as **ClockOnly -> SURVIVES**, `ScanDetectsIt: false`; counts become **30/11/3 of 44**. No
reclassification - G_042's root is **reproduced, not extended**; G_041 and G_033 are unchanged inputs. G_033's
live classifier now reads **26** substrate suites (43 classified in total). Tests: `Y_G_043_Tests` 8/8; group G
336/336.
## ResearchY-G_044 - Minimal Working Substrate Audit (COMPLETE, EMERGENT)

**Question.** Is D96^3 selected because it is the FIRST WORKING substrate, or because it MINIMISES COMPLEXITY?
Compared D96^2/D96^3/D96^4/D96^5; measured state-space size, irreps, photon support, graviton support,
observability fraction, states per observable.

**Answer: EMERGENT - the two proposed explanations are the SAME statement, and the third is REFUTED.**

- **THE WORKING SET IS AN UP-SET, COMPUTED:** a substrate works when both sectors carry **propagating** states
  (photons d-1 > 0, gravitons (d+1)(d-2)/2 > 0) AND the symmetry supplies a dimension-3 irrep - giving
  **{3, 4, 5, 6}**, one lower edge with no gaps.
- **WHY d = 2 FAILS, PRECISELY:** the graviton's sector **EXISTS** there (the traceless symmetric rank-2 is
  2-dimensional in two dimensions) but carries **0 propagating states**. Availability is not physics.
- **ALL SIX MEASURES ARE MONOTONE IN COST:** state space 9 215 -> 884 735 -> 84 934 655 -> 8 153 726 975; irreps
  5 -> 10 -> 20 -> 36; photon support 2 -> 3 -> 4 -> 5; graviton support 2 -> 5 -> 9 -> 14; observability fraction
  **falls** 0.1329 -> 0.02354 -> 0.00319 -> 0.00035; states per observable rises 7.522 -> 42.484 -> 313.730 ->
  2 841.332.
- **THEREFORE MINIMALITY IS IDENTICAL TO FIRSTNESS - A THEOREM, NOT A FINDING.** With strictly increasing costs
  the cheapest member of ANY set is that set's smallest element, so the minimum-complexity working substrate IS
  the first working substrate for all six measures at once - not because they agree here but because **they
  cannot disagree**. *"Selected for being first"* and *"selected for minimising complexity"* are ONE STATEMENT,
  which is why the minimality is **EMERGENT** (a corollary of first-ness) rather than an independent optimum.
- **OPTIMALITY IS REFUTED:** the unconstrained optimum of EVERY measure lies at **d = 1 or 2, outside the working
  set** - D96^3 is **96x** the state space of D96^2, **5.65x** its states per observable and **5.65x** less
  observable. The best WORKING substrate, and a poor substrate outright.
- **COMPUTED ASIDE:** the observability cost factor per step is **3.88** (1->2), **5.65** (2->3), **7.38** (3->4),
  **9.11** (4->5), so among the steps the theory is ALLOWED to take, the MANDATORY one (2->3) is the LEAST
  expensive - the compulsory move is the cheapest move. A convenience, not a selection argument.
- **THE OPTIONS:** MINIMAL -> **EMERGENT**; OPTIMAL -> **REFUTED**; FIRST -> **DERIVED** (the working set is
  {3,4,5,6}, an up-set).
- **WHAT REMAINS OPEN:** G_043's question of WHY the working set begins at 3. This audit shows only that
  **everything above the lower edge is irrelevant to the choice** - under monotonically increasing costs no
  substrate beyond the first working one can ever be preferred, whatever weighting is used.

Registry: added as **ClockOnly -> SURVIVES**, `ScanDetectsIt: false`; counts become **31/11/3 of 45**. No
reclassification; G_041/G_042/G_043/G_033 are unchanged inputs. G_033's live classifier now reads **26**
substrate suites (43 classified in total). Tests: `Y_G_044_Tests` 7/7; group G 343/343.
## ResearchY-E_005 - Propagation Origin Audit (COMPLETE, BOUNDARY)

**Question.** What is the minimal missing ingredient that turns T1(3) into a PROPAGATING photon sector and T2(3)
into a PROPAGATING graviton sector? Separate representation / kinematics / dynamics / gauge.
**Goal.** Locate the UNIQUE bottleneck shared by photon and graviton.

**Answer: BOUNDARY - the first failing layer is KINEMATICS, and the bottleneck is ONE object: a first-order
derivative carrying a DIRECTION INDEX (a field-valued connection).**

- **THE LADDER, RUN IN ORDER:** representation SATISFIED (T1 3 x 1; E + T2 = 2 + 3 = 5; the ring tops out at 2) -
  KINEMATICS **MISSING, THE FIRST FAILURE** - dynamics and gauge **MISSING BUT BLOCKED**, because a kinetic form is
  a functional of a field strength and a gauge orbit is the image of the derivative.
- **LOCALISATION ONE - THE INDEX SPACES COLLAPSE.** At a direction rank of 1 the antisymmetric square is exactly
  **0** and the traceless symmetric is exactly **0**; directions 1/2/3 -> 0/1/3 and 0/2/5.
- **LOCALISATION TWO - THE DERIVATIVE IS ONE-DIRECTIONAL.** `L = sum_r D_r^T D_r` holds exactly (residual
  **0.000E+000**), so first-order operators exist - but the direction rank is **1** against the **3** needed.
  Loops: ring **481**, cube **1 769 473** (elementary plaquettes **2 654 208**).
- **LOCALISATION THREE - THE BUILT-IN CONNECTION IS EXACTLY PURE GAUGE.** Step **0.065449847**, holonomy
  **6.283185** = 2*pi = the identity, conjugation residual **2.45E-016**. Its field strength is identically zero.
- **TWO RIVALS REFUTED:** the representation (complete) and the exterior complex (antisymmetric 3 vs symmetric 5 -
  the graviton is not a form). **THE SHARED COUNT:** 3 - 1 = **2** (photon) and 5 - 3 = **2** (graviton).
- **REFINEMENTS:** E_004's "choice of kinetic form" is the **second** step, not the first; E_003's "spacetime
  index" **is** the direction index. No reclassification - both remain BOUNDARY.
- **THREE ERRORS CAUGHT BY THE TESTS:** `%` is not a holonomy reduction; a real exponential was used where a
  complex phase was needed (534 before the fix); the shared `RankOf` helper cannot take one-component vectors.

No group-G registry claim (the registry is group-G only), so no counts change. Tests: `Y_E_005_Tests` 7/7;
group E 33/33.
## ResearchY-E_006 - Connection Origin Audit (COMPLETE, DERIVED)

**Question.** What AT structure could supply the missing direction index that E_005 located? Requirements: (1) local,
(2) directional, (3) first-order, (4) acts on T1 and T2, (5) no new primitive. Candidates: D96 ring derivative,
D96^3 edge connection, occupancy gradients, actualization flow, causal-order links.

**Answer: DERIVED - the unique missing object between representation and field theory is the D96^3 EDGE CONNECTION,
and it needs NO NEW PRIMITIVE.**

- **FOUR REFUTED, ONE DERIVES.** Ring derivative: rank **1**, largest ring irrep **2**. Occupancy gradients: domain =
  scalars; double gradient antisymmetric part **1.11E-016** vs traceless **0.879** (the same mechanism as E_005's
  pure-gauge phase). Actualization flow: **0** members returning a flow field. Causal-order links: closure reaches
  **95 of 96** cells vs a direct support of **2**.
- **THE OBJECT, ALL FIVE MARKS COMPUTED:** local (support **2** per row); directional (rank **3**); first-order
  (identity to **8.88E-016**; `|sigma(k)|/k` -> 1.000000 while `|sigma(k)|/k^2` diverges 9.996 -> 1000.0);
  **acts on T1 and T2 - antisymmetric = T1 (dim 3), symmetric = A1 + E + T2 (dims 1+2+3 = 6)** from the octahedral
  characters of the tensor square; and no new primitive (the tensor product of the ring's own differences).
- **ONE DERIVATIVE ACTING ON THE VECTOR SECTOR REPRODUCES BOTH SECTORS** - the photon's field strength in the vector
  irrep itself (G_042's Hodge identity) and the graviton's traceless sector.
- **LAYERS:** representation SATISFIED already; **KINEMATICS DERIVED HERE**; dynamics and gauge STILL MISSING.
- **BOUNDARY INSIDE THE VERDICT:** a live scan finds **0** product-lattice constructions in AT's code (derivable but
  NOT INSTANTIATED, G_033's finding from the code side) and **0** members compute a field strength (nothing couples
  it). Derived, not built, and not coupled.
- **FOUR ERRORS CAUGHT BY THE AUDIT ITSELF:** the scan counted its own token list (rule 11); the helper named
  `MembersThatComputeAFieldStrength` matched E_002's own signature regex and changed its own count 0 -> 1 (rule 11
  again); an over-claim that a gradient cannot reach the traceless sector was **false** (0.879) and was corrected; a
  linearity threshold of 10 sat above the k = 0.1 sample's 9.996.
- **REFINES E_005** (ingredient need not be invented, only built and coupled). No reclassification.

No group-G registry claim, so claim counts are unchanged. Tests: `Y_E_006_Tests` 8/8; group E 41/41.
## ResearchY-E_007 - Field Strength Origin Audit (COMPLETE, BOUNDARY)

**Question.** Can the D96^3 edge connection that E_006 derived produce a non-zero field strength? Construct closed
plaquette loops, discrete curvature, discrete curl; measure F != 0; compare pure gauge against a non-trivial loop.

**Answer: BOUNDARY - both halves are exact: the connection alone produces NOTHING, and the cube is the FIRST substrate
that can carry a field strength at all.**

- **THE CONNECTION IS FLAT.** It is a product of lattice differences and differences commute on a uniform lattice
  (commutator **1.73E-018**); in link language its content is a gradient, whose plaquette holonomy vanishes by
  telescoping (**6.94E-017**).
- **THE CENSUS DECIDES THE OTHER HALF.** Ring elementary plaquettes: **0**. Cube: **2 654 208** (one per site per
  orientation pair) inside **1 769 473** independent cycles.
- **THE FIRST NON-TRIVIAL QUANTITY IS THE QUANTISED PLAQUETTE FLUX.** A_1 = f y gives F = -f, and torus
  periodicity forces f = 2 pi n / L, so the smallest non-zero field strength is **2 pi / 96 = 0.065449847** - exactly
  AT's phase quantum. The number E_005 found pure gauge on the ring is the minimal real field strength on the cube.
- **THE APPARATUS IS EXACT:** the discrete curvature IS the antisymmetrised difference (residual **0.00E+000**);
  gauge invariance **1.11E-015**; discrete Bianchi **0.00E+000** (flux) and **8.33E-017** (pure gauge); periodicity
  **2.45E-016**.
- **BOUNDARY FOR A SPECIFIC REASON:** everything that DETECTS F is derived and the first non-trivial quantity is
  exhibited, but the connection does not produce it - it takes a chosen fluctuation, and nothing in AT selects one
  (the dynamics layer).
- **CORRECTION TO E_005:** that audit reported 3L^2 = **27 648** plaquettes while its own cycle count used the
  correct 3L^3 edges (inconsistent by a factor of L). Corrected to **2 654 208** here, in the code, the test and eight
  texts; E_005's verdict and other numbers unchanged.
- **TWO SYNTAX ERRORS in my own first draft of the core**, caught by the compiler.

Layers: kinematics DERIVED (E_006) but flat on its own; field-strength detection DERIVED HERE; dynamics STILL MISSING.
No group-G registry claim, so claim counts are unchanged. Tests: `Y_E_007_Tests` 7/7; group E 48/48.
## ResearchY-E_008 - Field Excitation Audit (COMPLETE, DERIVED)

**Question.** What AT mechanism produces a non-zero field strength F? Requirements: local, gauge-compatible, acts on
T1 and T2, no new primitive. Candidates: occupancy gradients, actualization gradients, deficit gradients, non-uniform
rho, topological defects.

**Answer: DERIVED - a dichotomy exact on both sides: every gradient is exactly pure gauge, and F != 0 requires a
NON-DIFFERENCE coupling. The survivor is NON-UNIFORM RHO read LOCALLY.**

- **THE NEGATIVE SIDE IS A THEOREM.** `Delta_mu H(rho)` for ANY single-valued H telescopes to zero around every
  plaquette: residual **2.50E-016** across H = rho, rho^2, exp(rho), sin(rho) on a NON-SEPARABLE organisation. That
  refutes occupancy, actualization and deficit gradients for every coupling function at once.
- **THE POSITIVE SIDE IS EQUALLY EXACT.** `h(rho)Delta rho` with h constant -> **2.22E-016** (that IS the gradient);
  h = rho/rho^2/exp -> **0.626 / 0.778 / 1.126**; site-local `h(rho(x))` -> **1.063** (rho^2: 1.595; directional:
  2.583), with the verified closed form **F_mu_nu = h(rho(x+mu)) - h(rho(x+nu))** (residual 3.33E-016).
- **THE DEFECT CANDIDATE COLLAPSES INTO IT.** A winding phase is PURE GAUGE (`U_mu = exp(i Delta_mu theta)` IS
  g(x)^-1 g(x+mu) by construction: **6.11E-016** over all plaquettes, cut included); the wrapped variant's **16** lit
  plaquettes are all WHOLE TURNS (largest **6.283185** = one turn, total 0) - the identity. A genuine defect needs an
  independently assigned link configuration: one mechanism, not two.
- **ALL FOUR REQUIREMENTS HOLD:** local (support 2); gauge-compatible (**5.55E-016**); acts on T1 and T2 (F is in the
  antisymmetric square = the vector irrep at d = 3; the symmetric square is A1 + E + T2); no new primitive
  (organisation **30** members, link phase **5**, winding **1** - AT already computes a winding number via
  `InternalStateAnalyzer.ComputeWindingNumber`).
- **OPEN:** which coupling, and what makes rho non-uniform - the dynamics layer.
- **TWO ERRORS CAUGHT BY THE TESTS:** a SEPARABLE test field made the product form falsely look like F = 0; and an
  unsubstituted `.Replace()` placeholder printed `{NoNewPrimitiveIsNeeded()}` into the report.

No group-G registry claim, so claim counts are unchanged. Tests: `Y_E_008_Tests` 7/7; group E 55/55.
## ResearchY-E_009 - Coupling Function Audit (COMPLETE, DERIVED)

**Question.** What determines h(rho)? Candidates: constant, rho, rho^2, exp(rho), derived occupancy law,
actualization law. Requirements: produces non-zero F, compatible with T1 and T2, no new primitive. Measure: field
strength, gauge structure, minimality. **Goal: derive h(rho) instead of choosing it.**

**Answer: DERIVED - h is not free. AT already computes it: `h(rho) = (2 pi / 96) rho^(1/d)`, ZERO free parameters.**

- **THE COUPLING IS AT'S OWN CLOCK LAW:** `GpsCorrectionOrigin.ClockRate(d, rho) = rho^(1/d)` (G_016b, d tau/dt =
  rho^(1/d)), exponent from `ClockExponent(d) = 1/d`, unit from AT's phase quantum 2 pi / 96 (E_003). Live scan: **5**
  AT members computing a clock rate/exponent/law.
- **CANDIDATES:** constant -> **0.000E+000** (REFUTED - it IS the gradient); rho -> 0.751; rho^2 -> 2.216; exp(rho) ->
  3.362 (all with **1** free parameter); **derived occupancy law -> 1.793E-002, 0 free parameters**; actualization law
  -> the same law (DEGENERACY: no AT member is named for it).
- **THE HONEST NEGATIVE:** `h = rho^p` over seven exponents gives zero at EXACTLY ONE (p = 0); every other exponent
  works (1/3 -> 0.274). So "produces non-zero F" rules out the constant and leaves the rest indistinguishable - the
  CLOCK LAW selects the exponent, not the requirement.
- **MINIMALITY:** countable - only the clock-law candidate has 0 free parameters.
- **GAUGE STRUCTURE + BOUNDARY:** with A_0 from the clock law the field strength is **PURELY ELECTRIC**
  (max |F_0i| = **1.424E-002** vs max |F_ij| = **0.000E+000**), gauge invariant (**1.11E-016**), Bianchi-consistent
  (**0.00E+000**), and exactly minus the gradient of the clock rate (closed form **0.000E+000**). The clock law fixes
  the TIME-LIKE component only: the MAGNETIC half is still an input.
- **TWO ERRORS CAUGHT BY THE TESTS:** `rho^(1/3)` of E_008's sign-changing scalar gave **NaN** - which is how the
  DOMAIN constraint was found (rho^(1/d) needs a non-negative organisation; automatic for a density, and E_008's
  results are unaffected); and the "not a difference" check first compared the derived LOCAL coupling against another
  LOCAL coupling expecting zero - a conceptual error, since a local function of rho is never a difference.

No group-G registry claim, so claim counts are unchanged. Tests: `Y_E_009_Tests` 7/7; group E 62/62.
## ResearchY-E_010 - Magnetic Sector Audit (COMPLETE, REFUTED)

**Question.** What AT structure can generate non-zero F_ij? Requirements: local, gauge-compatible, no new primitive,
acts on T1/T2. **Determine: can magnetic components emerge from occupancy dynamics alone?**

**Answer: REFUTED - no, and the measurement that decides it is a SCALING one.**

- **E_009's purely-electric result was a CHOICE, not a theorem** (it set A_i = 0). Completing the coupling covariantly
  - `A_mu = h(rho) Delta_mu rho`, the only vector a scalar can build - switches the magnetic sector ON:
  **max |F_23| = 1.422E-003** with the derived coupling, against **0.000E+000** for the linear coupling (the gradient
  E_008 excluded). **The magnetic sector exists because the clock law is NONLINEAR.**
- **BUT IT IS A FINITE-SIZE ARTEFACT:** refining the lattice at a fixed physical profile, the covariant form falls as
  **a^2.92** (1.42E-003 / 2.06E-004 / 2.61E-005 / 3.32E-006 at L = 8/16/32/64) and the site-local form as **a^0.99**
  (9.91E-003 / 5.05E-003 / 2.55E-003 / 1.27E-003). Both vanish in the physical limit - the same standard E_004
  applied to masslessness (the gap closes because mu_min n^2 settles).
- **WHAT SURVIVES:** E_007's uniform flux with the physical strength held fixed reads **0.785398 at every lattice
  size** - local, gauge-compatible (1.78E-015), Bianchi-consistent, no new primitive, acting on T1/T2 - but
  **independently assigned**: the non-gradient structure E_008 identified.
- **THREE ERRORS CAUGHT BY THE TESTS:** E_007's flux is an ELECTRIC field under this convention (its A_1 = f y sits in
  the (1,2) pair; the magnetic analogue is A_2 = f z); the WRAP belongs on the SUM of the four link phases, not on
  each term (the first version read the seam artefact 5.497787, the second a branch artefact); and a cross-audit check
  that compared two different profiles now runs on E_009's own organisation, reproducing its published 1.424E-002.

No group-G registry claim, so claim counts are unchanged. Tests: `Y_E_010_Tests` 7/7; group E 69/69.
## ResearchY-E_011 - Flux Origin Audit (COMPLETE, DERIVED)

**Question.** What generates the surviving non-trivial loop flux F = 2 pi / 96? Candidates: occupancy structure, D96
topology, winding number, actualization process, boundary assignment. Requirements: survives continuum limit, gauge
compatible, local, acts on T1/T2. Measure flux, holonomy, field strength.

**Answer: DERIVED - the origin is D96's own CLOSED TOPOLOGY plus the COMPACTNESS OF THE PHASE; 2 pi / 96 is the
substrate's cycle length speaking.**

- **TWO INGREDIENTS, BOTH COMPUTED.** (1) A CLOSED CYCLE makes the holonomy gauge-proof: a single-valued gauge
  function's contribution telescopes to zero - **2.498E-016** at L = 96, **0.000E+000** at L = 17 - the exact
  complement of E_008's theorem for contractible loops. (2) A COMPACT PHASE quantises the flux: `f L = 2 pi n`, with
  periodicity residuals 2.4E-016 / 4.9E-016 / 7.3E-016 for n = 1/2/3 and **half a sector NOT periodic**.
- **THE QUANTUM IS THE CYCLE LENGTH:** 2 pi/L = 0.785398163 (8), 0.392699082 (16), 0.196349541 (32), **0.065449847
  (96)**, with quantum x L = 6.283185307 every time; E_007's figure **reproduced** (agreement 1E-15).
- **CANDIDATES:** occupancy structure **REFUTED** (E_010 exponents 2.92 / 0.99); **D96 topology DERIVED**; winding
  number **REFUTED twice over** (a gradient - zero curvature - and a whole-turn holonomy, 2.45E-016 from the
  identity); actualization process **REFUTED** (time-like component only); boundary assignment **BOUNDARY** (the
  integer sector label).
- **WHAT SURVIVES IS MEASURED:** cycle holonomy **6.283185307** = one whole turn = the IDENTITY, while the plaquette
  holonomy is non-trivial at **6.544E-002** - so the flux's content is **local curvature, not a topological charge**;
  and at fixed sector the strength falls as **1/L**, so what survives is the **existence** of the configuration, not
  its magnitude.

No group-G registry claim, so claim counts are unchanged. Tests: `Y_E_011_Tests` 7/7; group E 76/76.
## ResearchY-E_012 - Flux Excitation Audit (COMPLETE, BOUNDARY)

**Question.** What AT mechanism populates a non-trivial flux sector? Candidates: occupancy defects, topological
defects, winding sectors, boundary conditions, actualization transitions. Requirements: create F != 0, survive the
continuum limit, no new primitive.

**Answer: BOUNDARY - nothing inside AT populates the sector, and the constraint on what MAY populate it is exact.**

- **The organisation cannot, re-measured rather than cited.** The occupancy route `A_mu = h(rho) Delta_mu rho` falls
  as **a^1.97** (max |F| 1.691E-002, 4.442E-003, 1.124E-003, 2.820E-004 at L = 8/16/32/64). The occupancy **moves** flux;
  it cannot **put any in**.
- **The flux content is an assignment with a global constraint, and this is the sharpest result of the audit.**
  Summing plaquette holonomies over the **whole torus** counts every link **twice with opposite signs**, so the reduced
  fluxes must sum to a multiple of `2 pi` (measured **0.00E+000** for a uniform field). Consequence: a **SINGLE
  half-turn flux is FORBIDDEN** (residual **3.141593**) while a **BALANCED PAIR (pi, -pi) is ALLOWED exactly**
  (**0.000E+000**). A single fluxon is forbidden by the **shape of the substrate**; the smallest non-trivial
  configuration is a **pair**. The constraint is a **global balance** - the partner may sit on another slice, because a
  **slice's product is free** (its boundary is a **non-contractible cycle**: **2.000000** from the identity with one
  half-turn on it).
- **The pair meets the requirement the occupancy route fails.** Its amplitude is **pi at every lattice size**
  (3.141593 at L = 8/16/32/64), so a non-trivial flux with a **surviving amplitude** exists; what does not exist is
  anything in AT that **creates** one.
- **The candidates.** occupancy defects **REFUTED** (the scaling); topological defects **BOUNDARY** (a legitimate pair
  without a creator); winding sectors **REFUTED twice over** (a gradient: zero curvature by E_008, whole-turn holonomy
  by E_011); boundary conditions **BOUNDARY** and it is the **answer**; actualization transitions **REFUTED** (the
  time-like component only, E_009).
- **A first-draft claim was withdrawn.** The draft held the product of holonomies around a single **slice** to be the
  identity, which would forbid the single half-turn a second time. It is true for the **whole torus** and false for a
  slice. The first probe **appeared to confirm** the wrong claim because it **folded the configuration at the seam**
  before summing - silently destroying exactly the quantity under test. The audit measures the whole-torus sum instead.

**What this changes.** E_011 identified the flux's **origin** and left the integer sector as a boundary assignment;
E_012 refines that: the assignment is itself subject to a **global constraint** that forbids a single half-turn. The
open item sharpens again - AT derives the flux quantum, its gauge-proofness and the constraint on allowed patterns, but
derives nothing that **selects** one.

No group-G registry claim, so claim counts are unchanged. Registry: added as **Boundary** (`flux-excitation-audit`),
reclassifying nothing. Tests: `Y_E_012_Tests` 6/6; group E **82/82**.

## ResearchY-E_013 - Flux Population Audit (COMPLETE, BOUNDARY; form DERIVED)

**Question.** What mechanism populates the allowed balanced flux sectors, given that a single fluxon is forbidden and a
balanced pair allowed? Candidates: occupancy rearrangement, defect pairs, boundary conditions, actualization
transitions, spectral transitions. Requirements: local, gauge compatible, survives the continuum limit, no new
primitive. Measure: sector population probability, sector stability, flux lifetime.

**Answer: BOUNDARY for the population, with the mechanism's FORM DERIVED** - the two levels kept apart as the
D_028/D_040 rule requires.

- **The pairing is forced and the measurement is exact.** Increment **one** link phase and the plaquette content changes
  **only in pairs**: minimum **4** plaquettes touched, moves touching a single plaquette **0**, signed sum
  **0.000E+000** (36 configurations over 3 directions, 3 increments, L = 8/16/32/64). So E_012's forbidden case is not
  merely disallowed but **unreachable by any local move**: **P(single) = 0.0**, **P(pair) = 1.0**.
- **Stability is exact and is not a lifetime.** Pair amplitude **3.141593 at every L**; a pure gauge field changes its
  content by **6.939E-017**; a single member decayed alone leaves **3.141593** (forbidden) while annihilating the pair
  leaves **0.000E+000**.
- **The lifetime is undefined, not merely long.** The clock law's sensitivity to the flux is **0.000E+000** against
  **1.061E-001** for the organisation (the control): no potential, so every flux value is degenerate and a lifetime is
  an **input**.
- **The activation is missing, measured three ways.** The update rule is purely electric (**max |F_ij| = 0.000E+000**
  vs **max |F_0i| = 1.424E-002**); a live census finds **0** members coupling a spectral index to a link phase against
  **25** touching a link field and a non-zero spectral count (a disjointness, not an absence); and the occupancy's
  constant-coupling limit is an exact gradient with plaquette content **6.939E-017**.
- **Candidates.** occupancy rearrangement **REFUTED** (a^1.97); **defect pairs DERIVED**; boundary conditions
  **BOUNDARY**; actualization transitions **REFUTED**; spectral transitions **REFUTED**.

**What this changes.** E_012 established that nothing creates a pair; E_013 establishes that anything which ever does
must be a pair, because the alternative is not forbidden but **unreachable**. The open item narrows once more: AT
derives the **shape** of any populating move and derives nothing that makes one, sizes one or times one.

No group-G registry claim, so claim counts are unchanged. Registry: added as **Boundary**
(`flux-population-audit`), reclassifying nothing. Tests: `Y_E_013_Tests` 7/7; group E **89/89**.

## ResearchY-E_014 - Sector Selection Audit (COMPLETE, BOUNDARY)

**Question.** What selects the flux sector label n, and why is n != 0? Known: n exists, n is quantised, n survives
refinement. Candidates: boundary conditions, topological constraints, actualization process, initial conditions,
occupancy arrangement.

**Answer: BOUNDARY - the origin of sector occupation is an external assignment, and the negative is proved rather than
declared.**

- **The family is generated by one operation, and the operation is an assignment.** Adding one quantum of uniform
  field (`A_1 -> A_1 + (2 pi / L) y`) is legitimate because the link variable is a **compact phase** - the real field
  at the far end is **6.283**, not zero, but the phase is the same to **2.449E-016** - and it moves the label by **1**,
  generating **0 -> 1 -> 2 -> 3** from the vacuum and inverting to **0.000E+000**. The label stays **1** at
  L = 8/16/32/64 while the strength falls **8x** (0.785398 -> 0.098175).
- **Nothing in AT depends on the sector, measured through linearity:** the occupancy contribution is **2.203E-003 at
  n = 0 against 2.203E-003 at n = 1**; blindness residual **4.163E-017** against a **control of 1.000000 quanta**.
- **The constraint permits every sector:** E_012's whole-torus residual at machine precision for every label
  (3.553E-015 at n = -3, **0.000E+000** at n = 0, 7.105E-015 at n = 3).
- **No operation collapses a non-zero sector to zero:** translation preserves the label, reflection and negation
  reverse its sign. Nothing produces zero - so **"why n != 0" has the same status as "why n = 0"**.
- **Candidates.** boundary conditions **BOUNDARY** (the origin); topological constraints **REFUTED**; actualization
  process **REFUTED** (purely electric); initial conditions **BOUNDARY** (the same origin under another name - a
  degeneracy recorded, not resolved); occupancy arrangement **REFUTED** (sector-blind, scaling away as a^1.97).
- **A bug caught by the audit's own uniformity check:** the first version read the label from the **unreduced**
  plaquette, so the seam cell reported **1.257E+001** and a uniform sector looked non-uniform; label and uniformity
  check now read the **reduced** phase (the same seam E_012/E_013 had to handle).

**What this changes.** E_011 called the integer a label, E_012 constrained the allowed patterns, E_013 showed nothing
makes a pair; E_014 locates the **occupation** itself and shows it cannot be inside AT. The verdict has a **live
branch**: a single sector-dependent law would move it to **DERIVED**.

No group-G registry claim, so claim counts are unchanged. Registry: added as **Boundary** (`sector-selection-audit`),
reclassifying nothing. Tests: `Y_E_014_Tests` 7/7; group E **96/96**.

## ResearchY-E_015 - Sector Weight Audit (COMPLETE, BOUNDARY over a DERIVED FLATNESS)

**Question.** Can AT assign a probability or weight to flux sectors n? Candidates: occupancy measure, actualization
count, entropy, free room, multiplicity structure, sector topology. Compute P(n). Does any AT-derived quantity prefer
n = 0 or |n| > 0?

**Answer: BOUNDARY over a DERIVED FLATNESS - the measure is exactly flat, so the sector is completely free.**

- **The measure is counted rather than argued.** Every configuration of a finite model, binned by holonomy class:
  **(4,6) gives 1024 per bin of 4096**, (6,5) 1296 of 7776, (8,4) 512 of 4096, (5,7) 15625 of 78125; bin counts at
  (4,6) are **1024, 1024, 1024, 1024**; **P(n) = 0.250000000** and **P(n)/P(m) = 1.000000**.
- **The equality is a theorem, not a coincidence of the sizes tested:** the shift adding one quantum to one link is a
  **bijection** moving the class by exactly one, verified on every model.
- **Three of the six candidates are one quantity under three names:** entropy, free room and multiplicity structure are
  all the number of configurations per sector (**spread 0.000E+000**, **free-room ratio 1.000000**) - they assign the
  **uniform** weight, with no preference in it.
- **The other three are refuted, each measured:** occupancy measure sector-blind (**0.000E+000** against a
  **1.000000** control); actualization count does not exist (spatial part **0.000E+000**); sector topology identical for
  every sector (holonomy distance **4.899E-016** while the strength differs).
- **Free-energy difference between sectors: 4.163E-017** (E_014's sector-blindness residual, reused).
- **The honest split:** the **ratio** P(n)/P(m) = 1 is derived exactly; the **absolute normalisation** is not (a flat
  measure over an unbounded label needs a regulator, and AT supplies none).

**What this changes.** E_014 located the occupation outside AT; E_015 closes every **counting-based** route to a
preferred sector. The verdict is computed with a **live branch in every direction**.

No group-G registry claim, so claim counts are unchanged. Registry: added as **Boundary** (`sector-weight-audit`),
reclassifying nothing. Tests: `Y_E_015_Tests` 7/7; group E **103/103**.

## ResearchY-G_077 - Clock Source Audit (COMPLETE, REFUTED - the clock has no independent source)

**Question.** Starting only from the surviving AT primitives - **Difference**, **Actualization**, **the density ρ**,
**causal structure**, **count conservation** and the surviving **G_035 temporal sector**, with the **GR field
equations, a specific clock law, multiplicativity, neutron-star fitting and any time-aether concept all excluded** -
what produces **local clock-rate changes**? Define the minimal object, decide which quantity drives it, derive **clock
change → redshift without assuming an F**, decide whether local clock changes are **passive observables or manipulable
states**, identify the **required source term**, test whether **controlled clock gradients imply controlled
gravitational gradients**, and analyse whether AT can permit **time manipulation without gravity manipulation**.

**Answer.** **REFUTED - THE CLOCK HAS NO INDEPENDENT SOURCE, AND TIME CONTROL IS GRAVITY CONTROL.**

**The minimal object is one scalar, and the clock adds no field.** Of the sector's **6 temporal observables**, **5 take
the occupancy** and **1 takes the potential** (the same field re-expressed), and **0 take anything else** - so a second
driver has **no argument to enter through**. The readout is **LOCAL**, measured: a one-part-in-a-thousand occupancy
change at one site moves the rate there (**3.045E-002**) and the largest change anywhere else is **0.000E+000**.
**The response ranks are the sharpest measurement:** the **clock rate reads 9 of 9 directions** of the occupancy, the
**acceleration reads 8** (blind to the **uniform** one) and the redshift between the ends is a single number.

**The driver is the density and every alternative is measured.** The **actualization rate is the same quantity**
(reconstructing the occupancy from the rate by bisection has a residual of **9.100E-016**); **causal connectivity is
not a driver** (two graphs with the same occupancy give the same clock *and* the same acceleration, and the
connectivity-to-gravity bridge is an **open problem**); **information density is not a driver** (same local occupancy,
different global entropy, same local clock); the **phase sector is not available** (G_049: the clock pattern is
**lossless in ρ** with a closed-form inverse).

**The redshift, with no clock law assumed.** **1 + z = W(A_s)/W(A_o)** for an **arbitrary positive monotone W**, with
**A = (1/d) ln ρ** - measured over **33 law/pair combinations** carrying the pinned data, the **exponential as one
member among others** and a **non-analytic** member among them. A local measurement fixes only the **logarithmic
derivative** of W, reported at **two steps** so the first-order law is a **limit** - the exponential sits at the
round-off floor (**9.564E-013** against **4.057E-013**) and the stiff non-analytic member falls **~10× per decade**.
**Successive redshift factors telescope for every law**, which is why **G_076's composition constraint is about
potentials adding** and not about redshifts composing.

**The decisive measurement is a counting one and it is F-free.** The surviving source law makes the acceleration the
gradient of the **same** logarithmic potential, so the **acceleration integral between two points IS minus their
potential difference for any clock law**, while the clock offset is `g(A_s) − g(A_o)`. **The two readings share their
zero set for every law** (True over **11 laws and 4 configurations**; the vacuum gives **0.000E+000** for both), and
what is **not** fixed is their **ratio** - the mean logarithmic derivative - so **AT says the accompanying gravity
cannot be zero and cannot say how large it is**.

**Manipulable, but only through the density.** The perturbation moves the rate (**3.045E-002**), nothing elsewhere,
and the **acceleration profile by 1.666E-004**; **no clock change is available without an occupancy change**. The
**source term census** is **four quantities, ONE field, ONE transport, TWO readouts**, and **count conservation makes
the source a transport** (a redistribution keeps the total at **7.000000** exactly while raising the clock at the probe
and the acceleration at the source **by the same act**).

**The boundary splits in three, and the audit refuses to collapse it.** (a) A clock offset with **no LOCAL
acceleration** is **PERMITTED** - the shell gives interior acceleration **0.000E+000** with an offset of **2.000E-001**,
the whole difference carried by **one wall link**. (b) A clock change with **no observable redshift** is permitted
**only if the total count may change** (a uniform shift moves the clock by **3.045E-002** and every pairwise redshift
by **0.000E+000** while changing the total occupancy by **4.709E-001**). (c) **Under count conservation it is
REFUTED** - the uniform direction is the only one nobody can see, and it is the one the conserved count forbids.
(d) A clock offset with **no acceleration on any path** is **REFUTED**. The gradient's **support is free and its
integral is not** (the width-versus-strength trade, measured on the gradient each **lattice link** carries).

**Six defects in the audit's own first version are recorded.** (1) **The uniform direction was first reported through
the rank of the redshift, which is 1** because the redshift between two fixed points is **one number** - a rank cannot
show *which* directions a single observable is blind to; the audit now measures the uniform shift directly, and **count
conservation is what closes the argument**. (2) The suppression witness was carried as **F rather than as a rate**, so
its logarithmic derivative came out **1.201** instead of 1. (3) The mesh witness was **rebuilt at an amplitude that
broke monotonicity** (0.6 against G_076's bound **2δ/π = 0.0064**), so the audit's own non-analytic member was not a
viable clock law. (4) The width trade measured the peak with a **central difference**, which **smooths a one-cell
step** and made a one-cell and a two-cell wall indistinguishable. (5) The first-order law was asserted with a **fixed
tolerance** that the stiff member failed **for being stiff rather than for being wrong**. (6) The rate→ρ inversion was
bisected **outside the physical band**, reporting two laws as non-invertible where they invert perfectly where the
theory is used.

**Status.** COMPLETE. Tests `Y_G_077_Tests` **9/9**. Core `AT.Core/ResearchXH/ClockSourceAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_077.md`. Registry: added as **Refuted** (`clock-source-audit`),
reclassifying nothing. **Group-G count guards bumped:** `Y_G_033` 46 -> **47** and `Y_G_035` 77 -> **78** with survives
63 -> **64** in both the registry census and the `MinimalTimeSector` view.

## ResearchY-G_078 - Source Manipulation Audit (COMPLETE, REFUTED - and the audit refuted its own headline)

**Question.** G_077 established that the clock and the acceleration **share one source**, the local occupancy ρ whose
logarithm is the potential. Can that source be **changed locally**? Allowed: **actualization, density transport,
count conservation, causal structure**. Forbidden and not used: **new fields, imported matter sectors, imported GR
equations**. (1) Which processes alter local ρ? (2) Redistribution, amplification or suppression? (3) Under count
conservation, the **maximum local enhancement**? (4) Can a bounded region hold `Δρ ≠ 0` with **no compensating
deficit**? (5) Does every **clock** manipulation create a gravitational signature? (6) Does every **gravitational**
manipulation create a clock signature?

**Verdict: REFUTED - redistribution is the only elementary channel, and the symmetry of its bounds turns on one
assumption the audit refuses to make silently.**

**No source term is supplied.** Every natural operator of the update class is a **DIVERGENCE**: upwind, centred and
Laplacian all leak **0.000E+000** of the count on a **closed ring**, while the deliberately non-divergent **CONTROL**
leaks **8.166E+000** - so **the detector can see a source and there is none in the class**. A source term would be an
**import**, which the question forbids: **absent rather than excluded**. On an **open chain** the two first-order forms
leak **exactly the boundary term the chain drops** (1.971E-001), which is **question 4's answer arriving already**.

**The uplift cap is robust.** Positivity caps concentration at the total, so the cap is computed in **two readings
that must agree if the cap is real**:

| reading | max occupancy | uplift `ΔA` | rate ratio | metric ratio |
|---|---|---|---|---|
| all of the count in one cell (others empty) | 96 | 1.5214 | 4.5789 | 20.9659 |
| one unit in every cell + the surplus in one | 97 | 1.5249 | 4.5947 | 21.1113 |

They differ by **2.270E-003 relative**, so **the cap is a property of positivity rather than of the reading**. The
uplift is **logarithmic** - each **e³ = 20.09×** of count adds exactly **1.0** (96 cells → 1.5214, 9 216 → 3.0429,
1 000 000 → 4.6052) - so **enhancement is expensive: 20 000× the count per unit of potential**.

**The contrast cap is conditional, and this refutes the audit's own first headline.** The first version claimed *the
depression is unbounded where the uplift is capped*, and therefore that redistribution leaves a balanced pair whose
ends diverge without limit. **That is not a result of count conservation**: under a **unit floor** in every cell the
maximum contrast between a well and a hill of the **same count** is **4.5947 - the same number as the uplift cap - so
the asymmetry VANISHES**. It exists **only if an EMPTY CELL is admitted**, where the contrast is **unbounded**
(**1.260E+002** at a well occupancy of **1.000E-006** with the total held fixed) and **the reason is A = (1/d) ln ρ
diverging rather than anything the count does**. The audit reports both branches and **refutes its unconditional
formulation**.

**A bounded region can never change alone.** The change inside a region **equals minus its net boundary flux to
4.857E-017 over 8 cells**, with **0** regions changed and no boundary flux. **The compensation is compulsory; only its
location is free** - the flux decides the distance.

**The two implications are asymmetric, and that is the sharpest pair.**

| claim | answer | measurement |
|---|---|---|
| every gravitational manipulation carries a clock signature | **YES** | **9 clock laws × 4 depths, 0 counterexamples** |
| every clock manipulation carries a gravitational signature | **NO - exactly one exception** | uniform scaling moves the clock **6.623E-003**, the acceleration **0.000E+000**, the total count **1.920E+000** |
| a bounded region can change alone | **NO** | the ledger identity, 0 exceptions |

**Count conservation forbids exactly the one move that would have decoupled the clock from gravity.**

**Seven defects in the audit's own first version are recorded.** (1) **A ledger of zeros proves nothing** - the region
table used fixed starts against a flux pattern that left those regions unchanged, so every row read **0.000E+000** and
the identity was satisfied **vacuously**; the regions now start at cell 0 and the rows are **asserted non-trivial**.
(2) **An inverted verdict row** - `MirrorImplication`'s third row encoded `Answer` as `residual > 1e-12`, answering
**true exactly when the identity failed**. (3) **The verdict was culture-dependent** - its figures were interpolated
with the machine's culture (`1,5214`), so a test that re-formatted a computed number **passed on en-US and failed on
de-DE**: the never-compare-formatted-strings rule biting in a **new place, locale rather than format**; fixed by
invariant formatting **and** by a `VerdictFigures()` accessor returning the quantities **as numbers**. (4) **A wrong
growth law** - the contrast was asserted to grow **10× per decade of depth** when the truth is **10^(1/3) = 2.15×**,
because the contrast is a **cube root**. (5) **The open-chain identity is form-specific** - it holds for the two
first-order divergence forms and not for the control or the end-duplicated Laplacian, so that column is labelled
**convention-sensitive**. (6) **The headline was conditional with the condition invisible.** (7) A missing method
signature lost to a mechanical edit and caught by the compiler.

**Status.** COMPLETE. Tests `Y_G_078_Tests` **9/9**. Core `AT.Core/ResearchXH/SourceManipulationAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_078.md`. Registry: added as **Refuted** (`source-manipulation-audit`),
reclassifying nothing. **Group-G count guards bumped:** `Y_G_033` 47 -> **48** and `Y_G_035` 78 -> **79** with survives
64 -> **65** in both the registry census and the `MinimalTimeSector` view. **Surfaces updated:** AT.Book
`TheoryRegistry`, AT.App Theory Book chapter + **new Open Problem "May a cell be empty?"** + Research News + suite
table.

## ResearchY-G_080 - Connectivity Origin Audit (COMPLETE, ASSUMED - connectivity is an input at the level of the primitives)

**Question.** G_079 derived the continuity law and proved that its equivalence with count conservation rests on one
premise the law cannot supply for itself: the **connectivity** of the substrate. (1) Can disconnected universes exist?
(2) Is connectivity generated automatically by the update relation? (3) Smallest disconnected survivor? (4) Observable
signatures of disconnected sectors? (5) Derived, emergent or assumed? Allowed as evidence: **Difference,
Actualization, the generation relation, count conservation**; forbidden: a **graph-connectivity axiom** and an
**imported topology**.

**Verdict: ASSUMED - connectivity is an input at the level of the primitives, and BOTH alternatives are refuted by
measurement.** The substrate is the 1D ring family, one ring per component, from 2 cells to the canonical 96; every
substrate is **built** from an explicit link set, its component count is **computed** from that set, and only then is
each allowed primitive asked whether it can see the difference. No spectral quantity is quoted anywhere in the audit.

**1. NOT DERIVED - the four allowed primitives are blind to the cut.** Adjoin a **disjoint copy** of a substrate to
itself: **0 of 4** primitives changes the content of its certificate — the same differences `{−5,−3,1,2,3,4}`, ticks
`{−6,−3,0,5,6,7}`, in-degrees `{0,1}`, component sums `{47}` with leak 0 — while **every** global descriptor moves:
components **1 → 2**, transport directions (the rank of the divergence) **7 → 14**, generation roots **1 → 2**. The
reason is structural and it is the audit's sharpest sentence: **three of the four primitives are local — radius one
link — and the fourth is a sum, and a sum cannot see which component its terms came from.** Swept over 8
(state, decomposition) instances with **0** content changes.

**2. NOT EMERGENT - the update class conserves the component count instead of producing it.** The class is written on
the link set, so its Laplacian is **block diagonal** across a cut: the weight carried between different components is
**0.000E+000** against a **live control** of **4** entries joining the two **halves** of the connected ring — a
partition no link set can influence. The pair that is a **link** on the 8-cell ring and a **cut** on two 4-cell rings
has influence **1 against 0**. Transport capacity is `n − c` across the whole family (1/0 at 2 cells, 7/6/4 at 8 cells,
95/94 at the canonical 96), so **every cut costs exactly one direction**. A conserved integer that no dynamics sets is
an **initial datum**, which is what the count itself is — and the component count now joins it.

**3. The generation relation does not repair it, and the generative rule presupposes the ring.** One root can generate
**both** components: **1** generation component against **2** transport components, so the two relations are different
objects and the law needs the second. And the actualization link rule `(i + d) mod n` never tests a link because it
computes one: applied to a genuinely disconnected substrate it **silently reconnects it**, manufacturing **1** crossing
link on the smallest split pair (returning **1** component where the substrate has **2**), **9** on two 4-cell rings
and **13** on the canonical split. **Connectivity is in the rule's arithmetic, not in its output.**

**4. The smallest disconnected survivor is two cells, and it is frozen.** One cell per component: transport rank
**0**, **no non-zero transport at all**, and the single non-trivial count-conserving change it does admit is the
**forbidden** transfer (1 of 1 witness) — while the same two cells **connected** admit **1** direction. Exhaustive
search gives the smallest cut of the canonical substrate as **2 links or 2 cells**, with one removal verified not to
suffice: **a single defect can never cut it.**

**5. The signature of a disconnected sector is a frozen, unreadable offset.** The per-component-uniform configuration
is an **exact fixed point** (max update **0.000E+000**) with a potential gap of **0.270**; on the smallest survivor
there is **no acceleration observable at all** (max **0.000E+000** over a state sweep, 0 cells reading one, against a
positive reading on the connected pair); and the **redshift between components is defined and moves** with the free
level (**−0.370 to +0.310**) while being **unreachable** — **0 links across the cut**, infinite path length — with
**0 of 6** temporal observables reading a flux. Not invisibility: **unreachability**.

**Defects recorded (five, two of them the same class):** a control that was **zero by construction** — twice, the
"other component" of a one-component substrate and then `EntriesJoiningDifferentComponents(cells, 1)` — replaced by a
partition no link set can influence; a smallest-cut search that counted **removed cells as components** and so
reported a disconnection it had created; a **nonsense assertion** that would have passed vacuously; and a helper left
behind by an edit, found by reading the file **after** the tests were green.

**Status.** COMPLETE. Tests `Y_G_080_Tests` **10/10**. Core `AT.Core/ResearchXH/ConnectivityOriginAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_080.md`. Registry: added as **Boundary** (`connectivity-origin-audit`),
the audit's own vocabulary being **ASSUMED**. **Group-G count guards bumped:** `Y_G_033` 49 -> **50** and `Y_G_035`
80 -> **81** with survives 66 -> **67**. **Surfaces updated:** AT.Book `TheoryRegistry`; AT.App Theory Book chapter,
the **answered** Open Problem on connectivity plus a **new Open Problem "What sets the component count?"**, a Research
News entry and a suite-table row.

## ResearchY-G_079 - Continuity Law Audit (COMPLETE, BOUNDARY - the law is derived and one premise is an input)

**Question.** G_077 made ρ the sole surviving clock source and G_078 found transport only, with **no source and no
sink**. Is **∂tρ + ∇·J = 0** forced? (1) Can every surviving update be rewritten as a continuity equation? (2) Is count
conservation equivalent to continuity? (3) Does any surviving update create local source terms? (4) Can source terms
be added without breaking previous audits? (5) Are the observables uniquely determined once `J` is specified? (6) Does
a source-free theory limit gravity engineering to redistribution?

**Verdict: BOUNDARY - THE CONTINUITY LAW IS DERIVED FOR THE SURVIVING CLASS, AND ITS THREE PREMISES ARE MEASURED
RATHER THAN ASSUMED.**

**1. The law is a theorem, not a tolerance.** The flux of each operator is **written down** — `J_i = ρ[i]` (upwind),
`2 J_i = ρ[i+1] + ρ[i]` (centred, at twice scale), `J_i = ρ[i+1] − ρ[i]` (Laplacian) — reconstructed by prefix sums
and verified as an **integer equality**: **residual 0** for all three. The non-divergent **control fails** the
membership test (`rank([M|v]) == rank(M)`), so the criterion is **live**.

**2. The flux is a gauge.** `rank = n − c`, `nullity = c`:

| substrate | cells | components | rank | nullity |
|---|---|---|---|---|
| one connected ring | 8 | 1 | 7 | 1 |
| two rings | 8 | 2 | 6 | 2 |
| four rings | 8 | 4 | 4 | 4 |
| one ring, canonical | 96 | 1 | 95 | 1 |

**Nullity 1 on every ring size** (4, 8, 16, 48, 96) — and the circulation is **unobservable**: **0 of 6** temporal
observables read a flux.

**3. Conservation is NOT continuity — connectivity is the missing premise.**

| substrate | witnesses | conserving & transport | conserving & **not** transport |
|---|---|---|---|
| one connected ring | 8 | **8** | **0** |
| two rings | 10 | 8 | **2** |
| four rings | 12 | 8 | **4** |

A **transfer across a cut** conserves the total and is **not any flux** — a **source/sink pair**. The check must be a
**rank** check and not a sum: the failing witness has total `0`.

**4. A source could be added without breaking a single surviving measurement.** The pinned rows are functions of the
**state** (9 of 9 laws carry the pinned slope, all 9 target rows) — so what a source breaks is the **closure**. The
sector supplies a **detector**, and it is **live**: a source of `0.03` makes the ledger residual **exactly 0.030**, and
**declaring** it restores the identity to `0.0E+000`.

**5. The observables need one mode the flux cannot supply — the conserved count supplies it.** The kernel of the
divergence is the **uniform mode** (dim `1`), so ratios and differences are determined and the **absolute clock rate is
not**; the total fixes exactly that mode (**0** free on a connected ring). A **disconnected** substrate leaves
**c − 1** relative levels free (**1** with two rings, **3** with four).

**6. Interior versus boundary.** The class supplies **no** interior source; a **boundary flux** — the one genuinely
source-like term the framework can express — is exactly the open-chain leak (**0.000E+000** closed against
**1.971E-001** open). **A source is available to any substrate with an edge.**

**Five defects are recorded**: the control's residual printed as **0** (the number that means "reconstructed exactly")
where a non-divergent change has **no flux at all**, now rendered **n/a**; every cross-transfer was **counted twice**
(8 of 16 instead of 2 of 10); `Single` was used where the substrate name is **not unique** (`"one connected ring"`
matches both the 8-cell and the 96-cell rows), throwing in three report functions and caught **by the tests**; a
garbage assertion would have **passed vacuously** on an empty string; and the centred operator's **halves** nearly put
floating point into the one place the audit claims **exactness**, so its flux stays at twice scale.

**Status.** COMPLETE. Tests `Y_G_079_Tests` **9/9**. Core `AT.Core/ResearchXH/ContinuityLawAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_079.md`. Registry: added as **Boundary**
(`continuity-law-audit`) — the two-level pattern of D_028/D_040: **derived law, boundary requirement**. **Group-G count
guards bumped:** `Y_G_033` 48 -> **49** and `Y_G_035` 79 -> **80** with survives 65 -> **66**. **Surfaces updated:**
AT.Book `TheoryRegistry`, AT.App Theory Book chapter + **new Open Problem "Is connectivity a consequence of the theory
or an input to it?"** + Research News + suite table.

## ResearchY-G_076 - Clock-Law Selection Audit (COMPLETE, REFUTED - the clock sector is structurally underdetermined)

**Question.** With G_075's result in hand - **F(0) = 1**, **F'(0) = 2** and the family **F = 1 + 2x + x²G(x)** with
**GR a member** - is there any **independent structural constraint**, drawn only from the allowed sources
(**composition of successive redshifts**, **clock synchronization consistency**, **path independence**, **group
properties**, **conservation laws**, **locality**, the **actualization-density interpretation**, **multiplicativity**,
**operational clock transport**), that selects a unique clock law? **No fitting, no assumed exponential, no imported
GR.** Determine which constraints force a unique F, whether multiplicativity uniquely yields exp(2x), whether any
weaker law survives, and the **cost of every added constraint**.

**Answer.** **REFUTED - NO INDEPENDENT SELECTOR EXISTS, AND THE CLOCK SECTOR IS STRUCTURALLY UNDERDETERMINED.** Of the
**10 allowed sources as functional equations**, **3 are NON-SELECTIVE**, **1 is REFUTED**, **5 are PARTIAL** and **1
SELECTS UNIQUELY - and the one that selects is the CONJUNCTION of the composition law with the pinned measurement**,
so it selects by **assuming** the composition law.

**The allowed sources are not independent, which is the first measurement.** Four rows from four different sources have
the **same zero set** on the 13-law corpus, so the list is **one equation in four languages**:

| law | composition | (1+z) factor | metric magnitude | log-additivity | path independence | same zero set |
|---|---|---|---|---|---|---|
| **exp(2x)** | **2.155E-016** | 2.288E-016 | 2.091E-016 | 2.949E-016 | 4.250E-016 | **yes** |
| (1+x)^2 | 1.459E-001 | 1.459E-001 | 2.705E-001 | 1.577E-001 | 1.830E-002 | yes |
| 1/(1-2x) | 1.489E-001 | 1.489E-001 | 1.289E+000 | 1.612E-001 | 7.156E-002 | yes |
| Pade [2/2] | 4.627E-003 | 4.627E-003 | 9.233E-003 | 4.638E-003 | 1.930E-005 | yes |

**The non-selective sources are measured as LAW-INDEPENDENT, not merely satisfied:** **locality** (the gradient enters
nothing - the family is by construction a function of the potential alone), **synchronization transitivity** (equality
of rates is an equivalence relation for any function of the potential) and **count conservation** (the residual
contains **no F at all**) each give **13 of 13 survivors at a spread of 0.000E+000**.

**The actualization composition is refuted rather than selecting.** With the occupancy multiplying: the **product**
rule reproduces the recorded rate exactly (**0.000E+000**), the **minimum** - what "all subsystems must advance"
implies - contradicts it by **6.046E-002**, the arithmetic mean by **1.029E-001** and the geometric mean by
**1.019E-001**. So **the rule is constrained, but to the product, which is multiplicativity again**; the row is carried
as **REFUTED (0 of 13)**, and a law not carrying the pinned data is marked **∞ (not an admissible candidate)** rather
than scored as a pass.

**And the composition law alone does not select.** exp(0.5x), exp(x), exp(1.5x), exp(2x) and exp(3x) all satisfy it to
machine precision (**~2E-016**) and predict **1.063696687867**, **1.131450643780**, **1.203520302273**,
**1.280180559309** and **1.448461117984** at J0740+6620 - **the constant is fixed by the measurement and the law by
the assumption.**

**Every weaker form survives, measured.** (a) **The mesh witness:** **e^(2x)(1 + β sin²(πx/δ))** with **β at 99.9 % of
the monotonicity bound 2δ/π** is positive, monotone, carries the pinned data, satisfies **every mesh composition
EXACTLY (1.110E-016)** while failing **between** the points by **4.145E-003** (worst deviation **6.227E-003**, **1 + z
shift 2.655E-003** at the target) - and **monotonicity bounds the modulation**, so the strength of the constraint is
set by the **operational resolution**. (b) **Approximate multiplicativity leaves a continuum**: at tolerance 1e-2 the
surviving interval is **|c| ≤ 4.893E-002** (1 + z span **3.822E-003**), at 1e-3 **4.871E-003** (**3.805E-004**), at
1e-4 **4.869E-004** and at 1e-6 **4.869E-006** - a tighter tolerance buys a **narrower interval, never a point**.
(c) **The subgroup witness:** with the exponent additive on the subgroup generated by **1** and **√2** the law is
multiplicative there and carries **F(0) = 1** exactly, while its values are **unbounded in every neighbourhood of the
vacuum** (**F = 2.411E-031 at x = -3.589E-004**); the only exponent keeping it bounded, monotone and continuous is the
**linear** one, **λ = 2√2 = 2.828427** - so **regularity is doing the closing, not the composition law**.

**The cost table has a running total.** From the surviving empirical sector alone (**10** survivors of the corpus,
spread **2.590E+001**, **0** primitives) to the composition law alone (**4** survivors, **4.485E-001**, **1** primitive
- and it does **not** finish the job) to composition **plus** the pinned data (**1** survivor, **0.000E+000**) to the
continuum form (**2** primitives) to a fixed map (**3** primitives, spread zero). **Three primitives take the infinite
family to one law, and none of the three is derived by any surviving result.**

**Six defects in the audit's own first version are recorded, and three are the same defect class.** (1) **The
composition residual was ABSOLUTE**, so it cannot see a multiplicative constraint in the deep field where both rates are
tinier than any tolerance: the free-room law exp(2x - 100x²) was reported as satisfying it at **1.786E-001** while its
log-additivity residual was **12.5**. (2) **The pair set was made of mesh points** - (0.01, 0.02), (0.05, −0.02) and so
on are all multiples of the mesh spacing - **so the audit's own witness satisfied every composition test exactly** and
was invisible to the measurement that existed to catch it. (3) **The path-independence routes had the same defect**
(0.24, 0.12 and 0.06 are mesh points). (4) **The pinned data was measured with the standard stencil**, whose step is
half the mesh spacing and therefore read the modulator as a slope (**2(1+β)** instead of 2), excluding the witness by
**the step of the probe** rather than by its own behaviour; the pinned residual now uses **h = 1E-5**. (5) **The
conjunction had to be added as its own row because the first version labelled the composition row itself as selecting
uniquely, which was FALSE** - the free constant of exp(kx) refutes it. (6) **The actualization row was a constant**, so
the law-independent branch classified it as non-selective even though **no admissible law passes it**.

**Status.** COMPLETE. Tests `Y_G_076_Tests` **9/9**. Core `AT.Core/ResearchXH/ClockLawSelectionAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_076.md`. Registry: added as **Refuted** (`clock-law-selection-audit`),
reclassifying nothing - **G_075's family, G_074's map result and G_073's conditional uniqueness all stand**. **Group-G
count guards bumped:** `Y_G_033` 45 -> **46** and `Y_G_035` 76 -> **77** with survives 62 -> **63** in both the registry
census and the `MinimalTimeSector` view. Group G **562/562** after the change.

## ResearchY-G_075 - Clock-Law Uniqueness Audit (COMPLETE, REFUTED - the forced part is the part shared with GR)

**Question.** Assume **only the surviving empirical constraints** - the **weak-field solar redshift**, **GPS time
dilation**, the **first-order agreement with GR** and the surviving **G_035 temporal sector**, with **no
conformal-optics argument** and **no discarded spatial-sector derivation**. Which parts of **g00 = -exp(2x)** are
forced? What is the **maximal family g00 = -F(x)**? Compute the **surface redshift of J0740+6620** for every surviving
F(x), quantify the spread, and decide whether AT makes a **unique observable prediction**. Preference: refutation.

**Answer.** **REFUTED - THE SURVIVING CONSTRAINT SET DOES NOT FORCE A UNIQUE CLOCK LAW, AND THE PART OF
g00 = -exp(2x) THAT IT DOES FORCE IS EXACTLY THE PART AT SHARES WITH GR.**

**What is forced is a form and two numbers.** The form **g00 = -F(x)** with F positive and monotone in the clock
potential alone, from **G_035's arity proof** (no temporal observable can be *called* with a spatial quantity); then
**F(0) = 1** and **F'(0) = 2**, carried by **9 of 9 laws** the audit compares - because the weak-field solar
redshift, the GPS correction and the first-order agreement with GR are **all three the same number**.

| feature | laws with it | status |
|---|---|---|
| the form g00 = -F(x), F positive and monotone | 9 of 9 | **UNIQUELY FORCED** (structure, not a measurement) |
| **F(0) = 1** | 9 of 9 | **UNIQUELY FORCED** |
| **F'(0) = 2** | 9 of 9 | **UNIQUELY FORCED** |
| **F''(0) = 4** (the redshift quadratic 0.5) | 3 of 9 | **WEAKLY CONSTRAINED, by a projection only** |
| **F'''(0) = 4/3** | 2 of 9 | **WEAKLY CONSTRAINED, by a projection only** |
| **multiplicativity** F(a+b) = F(a)F(b) | **1 of 9** | **COMPLETELY FREE** - absent from the surviving list |

**Nothing above the first order is pinned, because no realised measurement reaches it.** The measured reach rule: a
redshift known to a relative precision eps at a potential x resolves the k-th term exactly when **|x|^(k-1) > eps**.

| realised constraint | x | precision | orders beyond the first | bound on \|delta b2\| |
|---|---|---|---|---|
| **weak-field solar redshift** | **-2.123E-006** | 1.000E-002 | **0** | **9.420E+003** |
| **GPS time dilation** | **-6.961E-010** | 2.000E-003 | **0** | **5.746E+006** |
| first-order agreement with GR | 0 | - | 0 | unbounded |

**The AT-vs-GR difference is |F''(0)_AT - F''(0)_GR| = |4 - 0| = 4**, so the solar redshift would have to improve by
about **4 711x** to see it and the GPS row by a further **610x**. **The compact object is the only row that reaches
the second order at all - and it is a projection of G_072's programme rather than an input to it.** So the
classification is **2 UNIQUELY FORCED (orders 0, 1) + 3 WEAKLY CONSTRAINED (2, 3, 4 - by projections) + 2 COMPLETELY
FREE (5, 6)**.

**The maximal family, and the free room that never closes.** **F(x) = 1 + 2x + x^2 G(x) for an ARBITRARY G** - two
numbers pinned and a whole function free. The free-room ladder exhibits, **for every order k**, two **viable** laws
(**exp(2x)** and **exp(2x + 0.5 x^k)**) that are **identical below order k** and **different at order k**. **The
physical range is OPEN at x = -1/2, and the audit says why:** GR's own clock law F = 1 + 2x reaches **zero** exactly
there - that is its horizon - so a grid touching the endpoint would exclude the comparison theory by an endpoint
convention and the comparison would be vacuous.

**The prediction at J0740+6620, and the unbounded spread.** At **x = -0.247001**:

| law | 1 + z | shift from AT |
|---|---|---|
| **exp(2x) - the recorded AT law** | **1.280180559309** | - |
| **1 + 2x - GR's own clock law** | **1.405807032086** | **+1.256E-001** |
| (1+x)^2 (G_074's logarithmic law) | 1.328023241157 | +4.784E-002 |
| 1/(1-2x) | 1.222293851693 | -5.789E-002 |
| Pade [1/1] | 1.286874695654 | +6.694E-003 |
| Pade [2/2] | 1.280154024836 | -2.653E-005 |
| exp(2x - 100x^2) | **2.704E+001** | +2.576E+001 |
| **exp(2x - 1000x^2)** | **2.266E+013** | **+2.266E+013** |
| **flat-then-rising (m = 3)** | **1.142287863594** | -1.379E-001 |

**All nine are viable under ONE criterion, measured identically for each**: positive on the interval's interior,
**non-decreasing to within double resolution**, F(0) = 1, and the pinned slope held to **the stencil's own measured
refinement drift** (printed per law, because a typed 1e-6 can never be met by a law with a large fifth derivative).
**Bounded below: True. Bounded above: False. Bounded: False.** The family **brackets GR's prediction from both
sides**, and the finite comparison is already wider than the effect it was built to measure: the **six named laws span
1.835E-001 = 1.46x** the AT-vs-GR separation. The three classical values **reproduce G_073 exactly**.

**The decisive row: the family contains GR.** **GR's clock law F = 1 + 2x is a member** - positive, monotone,
carrying F(0) = 1 and F'(0) = 2, predicting **1.405807032086** against AT's **1.280180559309**. So **the surviving
sector cannot separate AT from GR**, which is what G_068-G_072 was built to do: the separation it measures is a
separation between **two members of one family** - and the AT-below-GR ordering is not a consequence of the surviving
sector either.

**The repair, priced.** The single structural constraint **multiplicativity** admits **exactly 1 of the 9 laws** with
the pinned data - **4.441E-016** for exp(2x) against **8.400E-001** (1 + 2x), **5.439E-001** ((1+x)^2), an infinity
(1/(1-2x), through its pole), **2.000E+000** (Pade [1/1]), **4.633E-002** (Pade [2/2]) and **3.960E-002**
(exp(2x - 100x^2)) - **and that is exactly the constraint G_073 found ABSENT from the surviving list**.

**Five defects in the audit's own first version are recorded, and the first would have made the whole comparison
vacuous.** (1) **The physical range was CLOSED at x = -1/2, which made GR's clock law non-viable** - F reaches zero
exactly there, because that IS its horizon - so the comparison theory was excluded by an endpoint convention rather
than by a measurement. (2) **The pinned slope was held to a typed 1e-6 while viability was held to a measured drift**,
so a witness passed viability and was then uncounted in the feature table (**8 of 9**); one criterion now feeds both.
(3) **The stencil's Richardson step divided by h a second time**, giving a drift of **466.7** for a linear law whose
true drift is **2.2E-014**. (4) **The second-order comparison mixed two languages**, differencing b2 (2 against 0)
where the forced-set difference is F''(0) = 2 b2 (4 against 0). (5) **The suppression witness was first run at m = 50
and m = 1000**, where successive samples of F round to the same double so the monotonicity test called a monotone law
non-monotone; the witnesses are now at m = 1, 2, 3 and the **m -> infinity limit is stated as a limit** rather than
quoted as a number the grid cannot see.

**Status.** COMPLETE. Tests `Y_G_075_Tests` **8/8**. Core `AT.Core/ResearchXH/ClockLawUniquenessAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_075.md`. Registry: added as **Refuted** (`clock-law-uniqueness-audit`),
reclassifying nothing - **G_073 and G_074 keep their own verdicts, which are conditional on their own constraint
lists**. **Group-G count guards bumped:** `Y_G_033` 44 -> **45** and `Y_G_035` 75 -> **76** with survives 61 -> **62**
in both the registry census and the `MinimalTimeSector` view. Group G **553/553** after the change.

## ResearchY-G_074 - Clock Law Necessity Audit (COMPLETE, BOUNDARY - monotonicity shapes it, the map numbers it)

**Question.** Does any surviving AT result require **dtau/dt = rho^(1/d)** specifically, or only a **monotonic
function of rho**? Candidates: **rho^(1/d)**, **ln(rho)**, **exp(rho)**, **Pade forms**; recompute **g00**, the
**redshift** and the **compact-star prediction**.

**Answer.** **BOUNDARY - MONOTONICITY DRIVES THE SHAPE AND THE MAP DRIVES THE NUMBERS.** And the sharpest measured
form: **the recorded second-order constraint admits exactly 2 of the 4 candidates, and those two are the SAME LAW.**

| law | monotone | positive | rate x^1 | x^1 in 1+z | **x^2 in 1+z** |
|---|---|---|---|---|---|
| **rho^(1/d)** | yes | yes | 1.0 | -1.0 | **+0.5** |
| **ln(rho)** | yes | yes | 1.0 | -1.0 | **+1.0** |
| **exp(rho), linear map** | yes | yes | 1.0 | -1.0 | **+0.5** |
| **Pade [1/1]** | **no** | **no** | 1.0 | -1.0 | **-2.5** |

**The shape requirements admit everything** - positive and monotone **4 of 4**, the redshift a ratio of rates **4 of
4**, the rate's first order **1 + x** for **4 of 4** (the redshift's own first coefficient is **-1.0**, being the
negative of the rate's) - so **the structure names no functional form**.

**And the candidates are not four theories:** the reparametrisation identity **exp(rho) with a LINEAR map IS rho^(1/d)
with a LOGARITHMIC map** is measured at **0.000E+000**. What distinguishes them is the **MAP**, and the map is what
the surviving audits take as an **input**.

**Only the second order discriminates**, and it admits **2 of 4 - the two descriptions of one law**. The **Pade [1/1]**
fails positivity and monotonicity at its **pole at rho = 5/2** inside the physical range, and `ln(rho)` turns
**negative below unit density**.

**The compact-star prediction confirms the class dynamically:** the two survivors give **1 + z = 1.280180559309**,
agreeing to **2.220E-016**, while `ln(rho)` gives **1.328023241157** - a shift of **+4.784E-002, comparable to a third
of the AT-vs-GR separation of 1.256E-001** (GR 1.405807032086). **So the G_072 programme could resolve the clock
law's FORM at the same time as it decides AT vs GR, making the power law a decidable input rather than a convention.**

**Four defects in the audit's own first version are recorded.** (1) **The maps were not normalised to a common first
order**, so the first table compared **my parametrisations** instead of the laws - I reported the *logarithm* as
failing the first-order constraint when it was **my own map** that failed. (2) **The reparametrisation identity was
mis-stated**, measuring **1.054** where the truth is **0.000E+000**. (3) **The first-order requirement was tested on
the redshift** (coefficient -1.0) instead of on the **rate**. (4) **I asserted the pairwise monotonicity check cannot
see a pole; the measurement refused it** - both checks catch the Pade pole, and the correction needed a correction.

**Status.** COMPLETE. Tests `Y_G_074_Tests` **6/6**. Core `AT.Core/ResearchXH/ClockLawNecessityAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_074.md`. Registry: added as **Boundary**
(`clock-law-necessity-audit`). **Group-G count guards bumped:** `Y_G_033` 43 -> **44** and `Y_G_035` 74 -> **75** with
survives 60 -> **61**.

## ResearchY-G_073 - Exponential Uniqueness Audit (COMPLETE, BOUNDARY - unique conditionally)

**Question.** **Why exactly g00 = exp(2x) rather than alternative positive metrics?** Candidates: **exp(2x)**,
**(1+x)^2**, **1/(1-2x)** and **Pade approximants**, against the surviving constraints G_019, G_020, G_035, G_068,
G_069 and G_070. Goal: determine whether the surviving time prediction is **mathematically unique**.

**Answer.** **BOUNDARY - THE SURVIVING CONSTRAINTS SELECT THE EXPONENTIAL AMONG THE NAMED ALTERNATIVES AND CANNOT
SELECT IT AGAINST THE PADE FAMILY.**

**The constraint ladder is measured coefficient by coefficient.** For (x^1, x^2, x^3) with the implied PPN beta:
**exp(2x) 2, 2, 1.333333, beta = 1**; **(1+x)^2 2, 1, 0, beta = 1/2**; **1/(1-2x) 2, 4, 7.999998, beta = 2**;
**Pade [1/1] 2, 2, 2, beta = 1**; **Pade [2/2] 2, 2, 1.333333, beta = 1**.

| constraints to order | survivors | which |
|---|---|---|
| 1 (the Newtonian limit) | **5 of 5** | all - **the constraint everybody checks buys nothing** |
| 2 (PPN beta = +1) | **3 of 5** | exp(2x), Pade [1/1], Pade [2/2] |
| 3 | **2 of 5** | exp(2x), Pade [2/2] |

**The named alternatives die at order 2** - exp gives beta = 1, (1+x)^2 gives 1/2, 1/(1-2x) gives 2 - the same
splitting G_019/G_020 recorded in the **redshift** quadratic as **AT 0.5 against GR 1.5**.

**And the PADE FAMILY SURVIVES, which is the audit's real result:** the **[1/1]** approximant (1+x)/(1-x) has the
**same x^2 coefficient** as the exponential and **[2/2]** matches it to order four, so **coefficient matching can
never single the exponential out - for every finite order some Pade form matches to that order and diverges beyond
it.**

**The uniqueness is therefore STRUCTURAL rather than expansionary**, and the structural property is
**multiplicativity of the clock group**: residual **4.441E-016** for the exponential against **5.439E-001** ((1+x)^2),
a **pole at x = 1/2** (1/(1-2x)), **2.000E+000** (Pade [1/1]) and **4.633E-002** (Pade [2/2]). **And that row's source
is NOT in the surviving list**, which the constraint table states rather than hides.

**And the exponential IS the clock law - the question's answer.** AT's metric is conformally flat with
**g00 = -rho^(2/d)**, so with **x = (1/d) ln rho**, g00 = exp(2x) is the **same statement** as the clock rate
**rho^(1/d)** - measured as a residual of **0.0E+000**. **The exponential is a restatement rather than a derivation**,
and the answer to *why exp* is **because the clock rate is a power of the density**: a power is an exponential of a
logarithm.

**The discrimination lives in the compact regime:** the spread across the five candidates is **6.761E-012** in the
solar weak field against **1.057E-001** at the compact object, **eight orders of magnitude apart** - so **no
solar-system observation can separate the candidates**, and the exponential is confirmed only where a neutron star
can be measured (G_069/G_070). **And one constraint that looked discriminating is not:** the G_068 ordering excludes
**nothing**, every candidate sitting below the GR redshift (**exp 1.280180559309** against **GR 1.405807032087**).

**Five defects in the audit's own first version are recorded**, and one is the defect class this repository has a
standing rule about: **a numerical artefact rather than a coefficient excluded the exponential from the very
constraint row that pins it**, because the redshift was differenced with one step whose **~4E-6** truncation exceeded
the **1E-6** tolerance. The stencil also used the **backward-difference sign convention** (every odd coefficient came
back negative); a single step size left an **O(h^2) truncation of 1E-4**; the redshift-quadratic row used the **g00
coefficient as a proxy**; and a bound of mine (**1E-12** against the measured **6.761E-012**) was wrong.

**Status.** COMPLETE. Tests `Y_G_073_Tests` **6/6**. Core `AT.Core/ResearchXH/ExponentialUniquenessAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_073.md`. Registry: added as **Boundary**
(`exponential-uniqueness-audit`), reclassifying nothing. **Group-G count guards bumped:** `Y_G_033` 42 -> **43** and
`Y_G_035` 73 -> **74** with survives 59 -> **60** in both the registry census and the `MinimalTimeSector` view.

## ResearchY-G_072 - Observational Program Audit (COMPLETE, DERIVED - one measurement on one object)

**Note on the ID.** G_071 was already taken by the **Observational Roadmap Audit** (complete, commit `6893facb`). This
audit was assigned the **next free ID, G_072**, and G_071 is preserved under its own.

**Question.** **What exact future measurement can first decide AT vs GR using a neutron-star redshift?** Output
**target**, **precision**, **instrument class** and **decision significance**, as a realistic observer-facing test
plan, from G_068, G_069, G_070 and G_071.

**Answer.** **DERIVED - THE PROGRAM IS ONE MEASUREMENT ON ONE OBJECT, AND THE FIRST REALISTIC DECIDER IS A
NEXT-GENERATION TIMING MEASUREMENT.**

| output | value |
|---|---|
| **Target** | **J0740+6620 (Riley 2021)**, M = 2.072 M☉, R = 12.39 km, **x = −0.24704** |
| **Precision** | surface redshift / compactness to **14.1422 %** (3σ) and **7.5524 %** (5σ) |
| **Instrument class** | **large-area X-ray timing**: 10 % timing with 1 % and 3 % marginals |
| **Decision significance** | **4.1374σ**, rising to **44.8377σ** for a direct surface-redshift measurement to 1 % |

**The target is the earlier audits' choice and is not re-opened: J0740+6620 is the most compact published object, so
the second-order AT-vs-GR split is largest on it.** The correlation is taken at **ZERO - the conservative choice**, so
every significance is a **floor**.

| instrument class | timing | compactness | significance | reaches |
|---|---|---|---|---|
| A. current X-ray timing (NICER-class) | 20 % | 9.0664 % | **1.9235** | 1σ |
| B. A plus radio pulsar timing | 20 % | 8.3339 % | **1.9641** | 1σ |
| **C. next-generation X-ray timing** | **10 %** | **3.1623 %** | **4.1374** | **3σ** |
| D. next-generation timing and spectroscopy | 5 % | 1.5811 % | **8.2749** | **5σ** |
| **E. a direct surface-redshift measurement** | **1 %** | — | **44.8377** | **5σ** |

**The precision the decision needs** reproduces G_070's recorded numbers from the decision side: **14.1422 % timing at
3σ** and **7.5524 % at 5σ** - **and its third column is a null that matters more than the other two: the required
compactness is NaN at the recorded 20 % timing, i.e. no compactness precision decides the question until the timing
improves.** That is **G_071's "timing is step 1" recovered as a computation rather than quoted**.

**And the leverage refuted the audit's own draft, which expected the radius.** Each capability improved **alone**:

| capability | significance | gain |
|---|---|---|
| **timing alone to 1 %** | 3.7316 | **1.9400** |
| mass alone to 0.1 % | 1.9641 | 1.0211 |
| radius alone to 1.5 % | 2.1720 | 1.1292 |
| everything together | 8.2749 | 4.3020 |

**Timing leads the radius by a factor of 1.7200 and the mass is nearly useless alone** - and that is **G_071's finding
recovered rather than contradicted**, because improving one capability at the current budget is a **timing** question
while equalising the marginals **at a fixed significance** is the **radius** question. The two audits agree once the
**operation** is named.

**The plan in phases:** 1 (in hand) 1.9235σ, **no**; 2 (near term, radio-timing masses) 1.9641σ, **no - the mass is
not what binds**; 3 (next generation, 10 % timing) **4.1374σ, 3σ**; 4 (with spectroscopy) **8.2749σ, 5σ**;
5 (the redshift itself to 1 %) **44.8377σ, 5σ**. **The first two phases fail, and better masses do not buy the
decision.**

**Three defects in the audit's own first version.** (1) **The draft headline was refuted**: I claimed the decision
"is not bought with timing" and that the radius carries the leverage. (2) **My expectation for the current row was
wrong** - I expected 1.05–1.2σ from G_071 and the measurement gives **1.9235σ**, because G_071's 1.0534σ is the
**generic** 1.4 M☉, 12 km object at its **worst-case correlated** error of 11.9048 % while this row is the **target**
at the **quadrature** error of 9.0664 %. (3) **A bookkeeping error**: I compared the class census (counting classes)
with the length of the FirstDecider list (returning only the first decider).

> **REFINEMENT (G_072, 2026-09-17): THE OPERATION MATTERS.** The radius leverage above is about **equalising the two
> marginals at a FIXED significance**. G_072 measured the complementary operation - **improving one capability alone
> at the current budget** - and there the **timing** leads by **1.7200** (gain 1.9400 against the radius's 1.1292 and
> the mass's 1.0211), because **at the recorded 20 % timing the required compactness is NaN**: no compactness
> precision decides the question until the timing improves. **Both statements are measurements of different
> questions, and neither supersedes the other.**

**Honest limits.** The classes are **capability triples, not mission commitments**; the correlation is **zero**, so
the numbers are **floors**; and class E assumes a resolved surface feature exists - the strongest route by two orders
of magnitude and the one with no current instrument behind it.

**Status.** COMPLETE. Tests `Y_G_072_Tests` **7/7**. Core `AT.Core/ResearchXH/ObservationalProgramAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_072.md`. Registry: added as **Derived** (`observational-program-audit`),
reclassifying nothing. **Group-G count guards bumped and verified:** `Y_G_033` 41 → **42**, and `Y_G_035` 72 → **73**
with survives 58 → **59** in **both** the registry census and the `MinimalTimeSector` view - the second of which the
guard itself caught.

## ResearchY-G_071 - Observational Roadmap Audit (COMPLETE, DERIVED - one object, three precisions, a frontier)

**Question.** What exact observations are required to decide AT redshift vs GR redshift first - with the target
object, the required timing, **mass** and **radius** precisions, and the resulting significance? Output
CURRENT / 3SIGMA / 5SIGMA.

**Answer.** The target is **J0740+6620 (Riley 2021)**; the program is a **5 % surface-redshift determination** with the
mass and the radius **each to 7.54 % (3σ) or 3.99 % (5σ) if their errors are independent** - **±0.156 / ±0.083 M☉**
and **±0.934 / ±0.494 km** - or **5.33 % / 2.82 % each if the errors add**.

| row | timing | compactness | each axis (ρ = 0) | σ_M | σ_R | worst case each | significance |
|---|---|---|---|---|---|---|---|
| **CURRENT** | 20 % | 3.661 % (published) | 2.59 % | 0.054 M☉ | 0.321 km | 1.83 % | **2.1791 σ** |
| **3SIGMA** | **5 %** | 10.66 % | **7.54 %** | **0.156 M☉** | **0.934 km** | **5.33 %** | 3.0000 σ |
| **5SIGMA** | **5 %** | 5.64 % | **3.99 %** | **0.083 M☉** | **0.494 km** | **2.82 %** | 5.0000 σ |

**The decomposition the earlier audits never made.** For `x = −kM/R`, `(σ_x/x)² = u² + v² − 2ρuv` with `u = σ_M/M`,
`v = σ_R/R` - so **a single compactness number does not determine the two axis requirements**; the split is fixed only
once the **correlation** is named, which no prior audit carried. The repository carries marginals for **exactly one
object** (G_068's deciding experiment, M = 1.4 ± 0.05 M☉, R = 12 ± 1 km): `u = 3.5714 %`, `v = 8.3333 %`, **v/u = 2.333**.

**The audit refutes its own input.** G_069's scenario table carries the literal **`0.1190`**, called "a generic NICER
compactness (11.90 %)". The **worst-case extreme** of those marginals is **11.9048 %** (agreement **4.76E-5**, implied
**ρ = −0.998096**) - the recorded value is the **worst case presented as generic**. The **independent** form is
**9.0664 %** (**1.313×** smaller), so the current significance rises from **1.0534 σ** (reproducing G_069's 1.05 σ, the
cross-check that the trace is right) to **1.1255 σ** - **6.9 % higher than recorded**. Same defect class as the
corrected **A₀ = 20 812** artifact.

**The frontier at 3σ (10.66 %):** worst case `u + v` (each 5.33 %), independent `√(u²+v²)` (each 7.54 %), cancellation
`|u − v|` (**the split is free**) - so the worst case demands **each axis 1.414× better**.

**Two counter-intuitive results, both measured.**
1. **A better timing RELAXES the mass-and-radius requirement** (3σ compactness 8.41 % at 10 % timing, 10.66 % at 5 %,
   **11.32 % at perfect**), and **at the published 20 % timing it is UNDEFINED (NaN)** - the timing term alone exceeds
   the budget, so no mass-radius precision reaches it. **The timing is therefore the first step**, and the requirement
   has a **floor of 11.32 % (3σ) / 6.79 % (5σ)**.
2. **The radius carries the leverage:** radius to perfection **2.539×**, equalising down **1.795×**, mass to
   perfection **1.088×**, and **equalising up makes it 0.769× worse**.

**Two of the audit's own assertions are recorded as wrong** rather than quietly fixed: the perfect-timing limit is
**not model-independent** (requirement ∝ 1/|slope|), and at this target the single-theory model's **shallower** slope
demands a **looser** compactness (**13.24 %** against **11.32 %**).

**Status.** COMPLETE. Tests `Y_G_071_Tests` **8/8**. Core `AT.Core/ResearchXH/ObservationalRoadmapAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_071.md`. Registry: added as **Derived**
(`observational-roadmap-audit`); group-G consistency counts **41** without-substrate / **72** registry /
**58** survives / **58** minimal time sector.

## ResearchY-G_070 - Observational Decision Audit (COMPLETE, DERIVED - one target, one measurement)

**Question.** What exact neutron-star measurements would decide AT vs GR first, with current NICER limits and the
published mass-radius ranges?

**Answer.** **Measure the surface redshift of J0740+6620 to 7.55 % (5σ) or 14.14 % (3σ)** with its published NICER
mass and radius: one measurement **decides the question at 6.4σ**. Published determinations run 20-50 %, so this is a
**stated factor of 2.6×-6.6×**, not a new instrument.

**The catalogue, recomputed not imported** (x = −1.4770 M/M☉ / (R/km), verified against G_019/G_069):

| target | x | σ_x/x | separation | 20 % / 5 % / perfect timing |
|---|---|---|---|---|
| J0740+6620 (Riley 2021) | **−0.247001** | 3.661 % | **1.256E-001** | 2.18 / **6.45** / 9.27σ |
| J0740+6620 (Miller 2021) | −0.224245 | 3.477 % | 9.518E-002 | 1.86 / 6.09 / 10.26σ |
| J0030+0451 (Riley 2019) | −0.152011 | **2.636 %** | 3.450E-002 | 1.05 / 4.06 / **15.43σ** |
| J0030+0451 (Miller 2019) | −0.163355 | 2.790 % | 4.125E-002 | 1.16 / 4.42 / 14.31σ |

**Requirements at J0740+6620** (redshift precision at the published compactness / compactness precision at 5 %
timing): 1σ **44.58 %** / 33.74 %; 3σ **14.14 %** / 10.66 %; 5σ **7.55 %** / 5.64 %. At J0030+0451: 20.97 / 39.50,
6.87 / 9.50, 3.98 / **undefined**.

**Budget shares at 5σ:** J0740+6620 timing **31.1 %** against compactness **29.1 %** - **comparable**, so no single
axis binds; the two requirement numbers are **equivalent conditions**. J0030+0451 timing **141.5 %** (**breached**)
against compactness 10.5 %.

**Withdrawn draft claim.** The draft assumed the most compact target wins on **both** counts. The measurement refused
it: the **largest separation** belongs to J0740+6620 and the **best-constrained compactness** to J0030+0451 (2.636 %
against 3.661 %). The audit therefore **computes the crossing**: J0740+6620 wins at 20 % and 5 % timing, J0030+0451
wins with **perfect** timing, and they **swap at a 2.051 % timing precision** (both 8.54σ). A first version of the
bisection had its comparison **inverted** and returned the search window's boundary - the same class of defect as
G_064's constants-in-disguise, fixed by asserting the **ordering** at three precisions and reporting the crossing as a
computed number.

**The program.** Step 1: J0740+6620's surface redshift to **7.55 %** (5σ) / **14.14 %** (3σ) with its published
NICER mass and radius - **excludes whichever theory the measurement does not fit**. Step 2: the same measurement on
J0030+0451 as a method check, which **becomes the better target once timing reaches 2.051 %**. Step 3: a NICER-class
mass-radius improvement, which excludes nothing alone but lowers the timing the other steps need.

**Status.** COMPLETE. Tests `Y_G_070_Tests` **7/7**. Core `AT.Core/ResearchXH/ObservationalDecisionAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_070.md`. Registry: added as **Derived**
(`observational-decision-audit`); group-G consistency counts **40** without-substrate / **71** registry /
**57** survives / **57** minimal time sector.

## ResearchY-G_069 - Neutron Star Decision Audit (COMPLETE, REACHABLE - at x = -0.121, and the compactness assumption matters as much)

**Question.** For which **compactness** does AT differ from GR by **1σ, 3σ, 5σ** given **current and projected NICER-class
uncertainties**? Measure **Δz** and **σ_obs**. Output **CURRENTLY UNDECIDED / REACHABLE / EXCLUDED**. Goal: the **first
realistic observation** that decides AT vs GR.

**Answer: REACHABLE - 3σ at x = −0.121, and the verdict leans on the compactness assumption too.**

1. **Three uncertainty models are carried**, because the earlier audits used a third one and the choice matters: the
   **single-theory** test (what *excluding* AT means), the **shared-x difference** (the tightest legitimate model, since
   the slopes agree to first order and the compactness uncertainty **cancels**), and the **quadrature** form the earlier
   audits used. At the earlier deciding object: **0.044812 / 0.040233 / 0.070118** → **1.05 / 1.17 / 0.67 σ** — a factor
   of nearly two. The single-theory form **reproduces the earlier audit's 1.03σ**, so it is **modelling, not arithmetic**.
2. **Slopes:** `dz_AT/dx = −1.1881`, `dz_GR/dx = −1.8848`, difference **−0.6968**.
3. **Thresholds (projected: 5 % timing, best current NICER compactness 3.661 %):** **1σ at x = −0.046**, **3σ at
   −0.121**, **5σ at −0.188**. The **current quadrature** form would need **x = −0.490** even for 3σ.
4. **The map:** **3 CURRENTLY UNDECIDED** (0.4, 0.6, 0.8 M☉ at 12 km), **10 REACHABLE** (from 1.0 M☉ up), **NONE
   EXCLUDED** (current significances reach only **1.96σ** at 2.2 M☉). **The first realistic observation is ~1.0 M☉ at
   12 km (x = −0.123)** — inside the published NICER mass range.
5. **The requirement is timing:** at 1.0 M☉, **1σ needs 15.81 %**, **3σ 5.11 %**, **5σ 2.86 %**; at 1.8 M☉, 3σ 11.77 %.
6. **And the assumption is measured:** the 3σ threshold is **−0.121** with the best current compactness, **−0.118** with
   perfect compactness knowledge, **−0.490** with a **generic** one — so **with a generic compactness the decision is
   unreachable inside the physical range** (x = −0.5 is GR's horizon).

**Status.** COMPLETE. Tests `Y_G_069_Tests` **7/7**. Core `AT.Core/ResearchXH/NeutronStarDecisionAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_069.md`. Registry: added as **Boundary** (`neutron-star-decision-audit`);
group-G consistency counts 39/**70** registry/**56** survives/**56** minimal time sector. **No reclassification.**

## ResearchY-G_068 - Temporal Prediction Audit (COMPLETE, DERIVED - one line, second order, undecided)

**Question.** What **unique measurable prediction** does the surviving AT time sector make that **differs from GR**?
Output the **observable**, its **magnitude** and the **current measurement status**. Goal: return from ρ-structure to
experimental time physics.

**Answer: DERIVED - the prediction is exact, unique and undecided.**

1. **The prediction is one line.** AT has `g₀₀ = −e^(2x)` so **`1 + z = e^(−x)`**; GR has `g₀₀ = −(1 + 2x)` so
   **`1 + z = (1 + 2x)^(−1/2)`**, with `x = −GM/(Rc²)` the surface potential. **The observable is a compact object's
   surface redshift** (equivalently a clock's rate at its surface). Neither is fitted.
2. **The magnitude is SECOND ORDER:** `z_AT = −x + x²/2 − x³/6` and `z_GR = −x + 3x²/2 − 5x³/2`, so the split is
   **`−x² + (7/3)x³`** (the next term sets the measured 2.333E-6 relative deviation at `x = −1E-6`). **No weak-field
   measurement can see it:** Earth **2.1× short** (−4.846E-19 vs a 1E-18 floor), Sun **2.2E+6× short**, Sirius B
   **3.0E+5× short**; at `x = −1E-9` the split is **below one ulp of unity** → **unrepresentable**.
3. **The live arena:** J0030+0451 **−17.367 %**; J0740+6620 (Riley) **−30.957 %**; Miller **−27.464 %**; generic NICER
   **−20.065 %**. **The sign is uniform: AT's redshift is always SMALLER.**
4. **The status is ALLOWED but UNDECIDED:** separation **0.047205** against a combined **0.046394** → **1.017σ now**;
   **3σ needs σ_z ≤ 0.015735 = 8.37 % of z_AT** and 5σ **0.009441 = 5.02 %**, against published **20–50 %** (short by
   **2.4×–6.0×**). **Allowed everywhere, preferred nowhere.**
5. **A qualitative difference needing no precision:** AT's `g₀₀` **never vanishes** (no clock-stopping surface) while
   GR's `1 + 2x` vanishes at `y = 0.5` where its redshift diverges.
6. **Two numerical facts are measured, not cautioned:** the naive subtraction at `x = −1E-9` is **inflated by 82.2×**
   (−8.224E-17 vs −1.000E-18) while the series route holds to **6E-8 relative**; the constants are **recomputed**, the
   Sun's potential agreeing with G_019's recorded value to **2.6E-4** (the residual being the choice of solar mass).

**Status.** COMPLETE. Tests `Y_G_068_Tests` **7/7**. Core `AT.Core/ResearchXH/TemporalPredictionAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_068.md`. Registry: added as **Derived** (`temporal-prediction-audit`); group-G
consistency counts 38/**69** registry/**55** survives/**55** minimal time sector. **No reclassification.**

## ResearchY-G_067 - Flow Selection Audit (COMPLETE, REFUTED - nothing in AT selects a flow)

**Question.** Can any **existing AT quantity** select between the **dissipative** and the **unitary** flows? Candidates:
clock law, acceleration law, field law, occupancy conservation, phase sector, free room. Measure whether any quantity
**changes under admissible flow replacement**. Goal: the **first principle that selects a physical flow**.

**Answer: REFUTED - no existing AT quantity selects.**

1. **A quantity can only select three ways:** invariant under both (cannot select), changing under both (distinguishes
   but states no preference), or **invariant under exactly one** - the only pattern a requirement could turn into a
   selection. Measured, the six candidates give: clock **9.485E-002 -> 3.754E-002 / 9.829E-002**; acceleration
   **1.399E-001 -> 1.165E-002 / 1.450E-001**; field **2.745E-002 -> 2.370E-003 / 2.891E-002**; occupancy total
   **96 / 96**; phase norm **1.048E+000 -> 2.648E-001 / 7.143E-001**; free room **51 / 51**. **None selects.**
2. **The free room cannot select anything:** it is `96 - 45 = 51` for every state and every flow - a property of the
   **spectrum**, not of the state.
3. **The requirement test:** on the evolved states the worst law residual is **5.122E-014** (dissipative) and
   **5.773E-014** (unitary) - **every law holds under both**, because the laws are **identities in rho**. The theory's own
   content is blind to the replacement.
4. **A correction and a refinement.** The difference's multiplier is **complex**, so **both** flows create phase content
   from the phase-free state: the phase norm rises **9.246E-015 -> 3.033E-001 (1000 steps) -> a peak near 3.118E-001
   (4000)**, decaying to **1.863E-001 (20000)** and **3.672E-002 (50000)**. The **attractor remains the uniform state**
   (G_065 stands) but **its phrasing must not be read as phase-freeness being preserved**; the unitary flow holds
   **~0.75** indefinitely - **transience versus permanence**.
5. **Two near-misses, neither answering the question:** the selector **pattern** appears only in the audit's control
   (the deviation is fixed by the unitary flow, so **norm conservation** would select), and the **entropy** is monotone
   under the dissipative flow only (**0.99874 -> 0.99982** vs unmoved) - and AT states neither requirement.
6. **What would select is a MONOTONICITY requirement about the state**, not a conservation requirement derived from the
   laws. **The selection is available but unmade.**

**Status.** COMPLETE. Tests `Y_G_067_Tests` **7/7**. Core `AT.Core/ResearchXH/FlowSelectionAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_067.md`. Registry: added as **Refuted** (`flow-selection-audit`); group-G
consistency counts 37/**68** registry/**54** survives/**54** minimal time sector. **No reclassification**, one phrasing
refined.

## ResearchY-G_066 - Physical Flow Audit (COMPLETE, REFUTED - phase-freeness belongs to dissipation, not to AT)

**Question.** Which **update rule is physically privileged**? Compare the **forward difference**, **backward difference**,
**centred difference**, **exact flow** and **Cayley flow**; measure **positivity**, **norm conservation**, **phase
evolution**, **attractors** and **law compatibility**. Goal: is phase-freeness a property of **AT itself** or only of one
**chosen flow**?

**Answer: REFUTED - phase-freeness is a property of the DISSIPATIVE flows, not of AT.**

1. **The five names describe four behaviours**, fixed by the multiplier: forward difference and exact flow **contractive**;
   backward difference **amplifying**; centred difference a bare **rotation generator**; Cayley flow **unitary**.
2. **Four of the five are admissible, and they do not agree.** At 4000 steps, eps = 1E-3: forward difference min cell
   **9.390E-001**, deviation ratio **0.381**, phase ratio **0.253** (DECAYS TO UNIFORM); exact flow **0.381 / 0.253** (the
   same to three digits); centred **1.001 / 0.800** (PRESERVES); **UNITARY 1.000 / 0.682** (PRESERVES); backward drives
   cells to **-2.597E+002** (LEAVES THE SIMPLEX).
3. **The laws rule out the amplifying form**, its worst law residual being **1.000E+000**, while every admissible form
   keeps them below **1E-13**.
4. **The decisive measurement:** the **unitary** form - total-conserving, uniform state stationary, every law intact -
   **preserves the phase content indefinitely** (**0.699** at 8000 steps). So **an admissible, law-abiding,
   total-conserving flow keeps the phase**, and phase-freeness belongs to **dissipation**.
5. **The dissipative forms erase everything, not only the phase** (deviation **0.381** vs phase **0.253**): their attractor
   is the **uniform state**, and the two agree to three digits (**0.3810 / 0.3809**), confirming G_060.
6. **No form is privileged by the measures usually taken** (total, uniform fixed point, laws), so **G_065's attractor
   argument cannot be promoted** to a requirement of the theory.

**A performance defect is recorded:** the first suite took **4m38s** because the exact and unitary forms evaluated **48
complex exponentials per step**; memoising the multiplier and the table brought it to **19s**.

**Status.** COMPLETE. Tests `Y_G_066_Tests` **7/7**. Core `AT.Core/ResearchXH/PhysicalFlowAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_066.md`. Registry: added as **Refuted** (`physical-flow-audit`); group-G
consistency counts 36/**67** registry/**53** survives/**53** minimal time sector. **No reclassification**, but a
restriction: G_065's finding stands as a measurement of the dissipative flow.

## ResearchY-G_065 - Phase-Free Principle Audit (COMPLETE, BOUNDARY - no law needs it, the flow produces it)

**Question.** Is phase-freeness a **derived requirement** or only a **preferred convention**? Test the phase-free
canonical state against **phase-bearing alternatives**; compare the **clock**, the **acceleration**, the **field**, the
**kernel** and the **flux**; measure whether **any AT law fails** when the phase content is non-zero.

**Answer: BOUNDARY - no AT law fails, and phase-freeness is still not a mere convention: the dissipative flow erases
phase content, so the phase-free configuration is its ATTRACTOR.**

1. **Seven laws on five states.** L1 clock (`rho = rate^d`), L2 metric (`g00 = -rate^2`), L3 potential (cross-route), L4
   source (`a = -grad A` by a second route), L5 field (`F = h(rho) Delta rho`), L6 simplex, L7 flux.
2. **No law fails and no law separates the states:** the worst residual anywhere in the family is **8.882E-016**, and the
   phase-free and phase-bearing states agree to that order. **The phase sector is invisible to the laws.**
3. **Three of the seven laws were mis-stated at first and the measurement caught all three** - each gave **1E-2 for
   *every* state** including the phase-free one: a term subtracted twice (potential), a **clamped** helper compared with a
   **periodic** difference (source), and the **magnitude** of a **signed** quantity (field). The **linearised** source law
   is reported separately as a **discretisation order** (1.441E-002 to 5.980E-001, measuring **roughness**).
4. **The one dynamical law** - a density must stay a density - was measured by **running both AT-native flows**: every
   state survives 4000 steps (minimum cell **2.223E-001** at the worst), and the phase-free state is **not even the most
   robust**.
5. **The main result is dynamical:** the dissipative flow **erases phase content** - from the alternative-seed state
   (**1.048E+000**, 42 hidden directions) the phase norm falls to **4.937E-001** at 1000 steps and **1.610E-001** at
   20000, hidden occupancy **42 -> 18**. **The phase-free state is the attractor and is already there.**
6. **The interface is not phase-freeness' doing:** kernel-as-union-of-modes and hidden-iff-empty hold for **every
   one-seed state** and fail **only** at distance-class **saturation**. What breaks the interface is saturation, not
   phase content.

**Status.** COMPLETE. Tests `Y_G_065_Tests` **7/7**. Core `AT.Core/ResearchXH/PhaseFreePrincipleAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_065.md`. Registry: added as **Boundary** (`phase-free-principle-audit`);
group-G consistency counts 35/**66** registry/**52** survives/**52** minimal time sector. **No reclassification.**

## ResearchY-G_064 - Canonical Recipe Origin Audit (COMPLETE, DERIVED - pinned by phase-freeness)

**Question.** **Why** these specific level weights? Test the **basis[0] choice**, the **level-weight formula**, the
**zero-weight levels**; measure which conclusions change when the recipe changes. Goal: **separate the recipe from theory
content**.

**Answer: DERIVED - the construction is PINNED, and by a requirement its own comment does NOT name.**

1. **The count law is exact:** `occupied = 44 − (zero-weight NON-CONSTANT levels)`, verified on five formulas - ramp **(0
   zeros) 44**, mod-17 **(3) 41**, **canonical mod-23 (2) 42**, mod-15 **(6) 38**, mod-11 **(8) 36**. **The 42 is 44 minus
   the canonical formula's two zeros**, and why the formula has two zeros is a property of its **modulus**. The
   **constant level is special** (it carries the simplex direction, not a mode), so zeroing it removes no occupied mode -
   a first version of the law was **off by one** on exactly those formulas.
2. **The seed choice swaps the two sectors:** `basis[0]` → **42 visible, 0 hidden**; `basis[^1]` → **0 visible, 42
   hidden** - the **same totals** (occupied 42, kernel **53**) with the sectors **swapped**. So the recipe decides **which
   sector the state lives in**, not how much of it. The cause is exact: `basis[0]` is **always a cosine**, `basis[^1]` is
   **sometimes a sine**, and the sines are the hidden quadratures.
3. **The requirements, per substance (recipe up to scale; 8 substances):** **R3 phase-free - 1 of 8, the canonical
   construction alone - PINS IT**; R4 hidden count = 47 - 1 of 8, pinning the **all-modes** construction, **not** the
   canonical one; R2 full visible sector - 3; R1 generic (orbit ≥ 84 of 95) - **4**; R5 union of whole modes - 7.
4. **Three findings:** the **named justification does not pin it** (genericity is shared by 4 of 8, and the largest orbit
   of **95** belongs to a construction that is **not** phase-free - the recipe **trades orbit size for phase-freeness**);
   **one requirement pins something else** (**47 ≠ 53** - G_040's hidden count and the canonical kernel are different
   quantities); and the **magnitudes are irrelevant**.
5. **The separation:** the **recipe** decides which sector the state occupies, how many modes it fills and the kernel's
   dimension; the **theory** decides everything downstream plus the levels, the distance classes, the bound of 49 and the
   floor of 47.

**Status.** COMPLETE. Tests `Y_G_064_Tests` **6/6**. Core `AT.Core/ResearchXH/CanonicalRecipeAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_064.md`. Registry: added as **Derived** (`canonical-recipe-origin-audit`);
group-G consistency counts 34/**65** registry/**51** survives/**51** minimal time sector. **No reclassification.**

## ResearchY-G_063 - Canonical State Audit (COMPLETE, BOUNDARY - one invariant out of thirteen)

**Question.** **Why** does the canonical construction choose exactly the observed **weights**? Measure the level weights,
the zero weights, the occupied modes, the kernel size; compare the canonical, full-weight and alternative-seed
constructions; determine **which conclusions depend on the canonical recipe**. Goal: **separate state-construction
effects from algebraic invariants**.

**Answer: BOUNDARY - the separation is achieved, and it cuts the series' own conclusions in two: ONE algebraic invariant
survives, TWELVE are state-construction effects.**

1. **Nothing selects the weights.** The weight is uncorrelated with the level's **eigenvalue** (|r| = **3.846E-002**), the
   degenerate levels' weights are **ordinary** members of the distribution, and the **multiplicity correlation (0.176) is
   REFUSED AS EVIDENCE** - multiplicity takes only **two** distinct values across the 45 levels. The zeros are an artifact
   of the modulus: `w` vanishes on levels **8, 31**, `w'` on **6, 29**, the ramp formula on **none**. G_046's own comment
   gives the design reason: **the state must be generic**.
2. **Thirteen conclusions were turned into predicates and evaluated on nine deterministic recipes.** The **one** survivor
   is the **distance-class bound** (row space ≤ **49**, 9 of 9). The twelve effects include **phase-freeness** (**2** of 9),
   **42 occupied** (**5**), **kernel 53** (**5**), **none of the eleven occupied** (**2**), and **hidden-iff-zero-occupancy**
   (**8**).
3. **Why the canonical state is phase-free:** every seed it takes is the **FIRST** entry of its level and the first entry is
   **always a cosine**, while the **last** entry is **sometimes a sine** - the alternative-seed state acquires phase
   content (**1.048E+000**). The **coefficient is irrelevant** (**1.076E-014** at twice the scale, still phase-free).
4. **A measured refinement:** hidden-iff-zero-occupancy and the union-of-modes property fail for **exactly one** recipe -
   the all-modes state - where the row space **saturates** the bound. So **G_061's central relation is a consequence of
   staying below the bound**, not a separate law.
5. **Substrate facts are reported separately:** levels **45**, free room **51**, distance classes **49**, the eleven **11**,
   degenerate levels **2**, kernel floor **47**.
6. **Two defects recorded:** the per-state kernel basis was first built from seeds `sin(a*seed + b*i)`, which span only
   **TWO** dimensions (they equal sin(a·seed)cos(b·i) + cos(a·seed)sin(b·i)) - it returned **2** vectors where it needed
   **53** and produced **51 false mismatches**; and **three conclusions were constants in disguise**.

**Status.** COMPLETE. Tests `Y_G_063_Tests` **8/8**. Core `AT.Core/ResearchXH/CanonicalStateAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_063.md`. Registry: added as **Boundary** (`canonical-state-audit`); group-G
consistency counts 33/**64** registry/**50** survives/**50** minimal time sector. **No reclassification**, but a
qualification of G_059-G_062: those measured the canonical state correctly and their headline numbers are properties of
the recipe.

## ResearchY-G_062 - Mode Occupation Audit (COMPLETE, DERIVED - the boundary is bookkeeping down to a floor of 47)

**Question.** What **determines** which Fourier modes are **occupied** in the canonical state? Measure the occupied
modes, the empty modes, the **construction weights** and the **degeneracy structure**. Test: **can the empty 11 be
populated without changing the theory?**

**Answer: DERIVED - the occupancy is set by the CONSTRUCTION, and the eleven CAN be populated by changing only the
state; what survives is an algebraic floor of 47.**

1. **The rule is arithmetic:** the state takes **one seed per level** (`basis[0]`) weighted, so its span is the mean plus
   one mode per level of **non-zero weight** and the count is `(levels with non-zero weight) − 1`. Measured: **45 levels,
   43 non-zero weights, rule 42, measurement 42**; with *every* weight non-zero it becomes **44** - so **42 belongs to a
   chosen formula**.
2. **The phrasing is corrected:** the state occupies 42 **visible** modes and leaves the whole **53-direction phase
   sector** empty; of those 53 the **eleven** lie in channels it does not occupy at all.
3. **The empty channels follow the formula's zeros.** Canonical weight vanishes on levels **8, 31** (channels **14, 19**);
   a **shifted** formula `w' = ((k+1)·41 mod 23 − 11)/23` vanishes on levels **6, 29** and empties channels **6, 23**
   instead - while **24, 32, 40, 48 stay empty under both**, being the **degenerate levels'** doing.
4. **The eleven can be populated, and nothing but the state moves:** an all-modes state gives **95 occupied, 0 empty,
   11 of 11**, while the **levels, distance classes, contraction rows and the eleven are the same objects** for every
   state. The walk toward them: canonical **0**, alternative seed **2**, full weight **2**, shifted formula **2**, full
   weight + alternative seed **4**, all modes **11**.
5. **The floor is the structure:** the row space is one row per **distance class**, so its rank cannot exceed **49**;
   measured, the smallest kernel any state reaches is **47** and the canonical **53** sits **six** above it.

**A probe defect is recorded:** a first all-modes state used a weight formula with its own zeros and left **eight** modes
empty, making the audit report that one of the eleven could not be occupied - false, and a defect in the **probe**.

**Status.** COMPLETE. Tests `Y_G_062_Tests` **7/7**. Core `AT.Core/ResearchXH/ModeOccupationAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_062.md`. Registry: added as **Derived** (`mode-occupation-audit`); group-G
consistency counts 32/**63** registry/**49** survives/**49** minimal time sector. **No reclassification.**

## ResearchY-G_061 - Residual Phase Audit (COMPLETE, DERIVED - the eleven are coordinates, not structure)

**Question.** What is **special** about the **11 unreachable phase directions** of G_060? Identify the **alternating mode**,
the **empty channels**, the **kernel relation** and the **symmetry properties**. Test: can any existing AT operator reach
them? Goal: explain the **53 = 42 + 11** split.

**Answer: DERIVED - they are NOT special directions of the substrate: they are the modes the canonical state does not
occupy, and the split is a property of SHIFT-INVARIANCE.**

1. **The kernel relation is exact.** A contraction row is `A_d rho` with `A_d` the distance-`d` relation, and **`A_d` is
   circulant**, so on a single mode it acts as `lambda_d(c) x <rho, e>` with `lambda_0(c) = 1`. Hence **hidden if and only
   if zero occupancy**, measured over all **95** non-constant modes. Occupied modes **42**, occupied span **43**, kernel
   **53**. The phase directions are hidden because the **state** says nothing about them.
2. **The eleven:** channels **14, 19, 24, 32, 40** (both quadratures) + the **alternating mode**, all occupancies at the
   floor. **Accounting closes 7 + 4 = 11, disjoint:** **Sigma(m-1) = 51** modes are empty for ANY one-vector-per-level
   state (free room, 96 - 45); the only two degenerate levels are index **13** (lambda **12.0**, mult **5**, channels
   16, 32, 48) and index **35** (lambda **14.0**, mult **6**, channels 8, 24, 40), leaving **7** of the eleven; and two
   levels carry a **construction weight of exactly zero** (indices **8, 31**) emptying channels **14 and 19** (**4**).
3. **Reachability, both halves measured:** circulant operators (difference, centred difference) give projection
   **9.512E-015 / 3.675E-015**, rank **0**; state-dependent ones (connection `h(rho) Delta rho`, T1/T2 coupling) give
   **2.968E-003 / 1.455E-001**, rank **11** - they reach **all** of them. A circulant operator cannot mix channels;
   multiplication by a non-constant `h(rho)` can.
4. **The decisive experiment - the set MOVES.** Rebuilding from the **last** basis vector of each level gives empty
   channels **8, 14, 16, 19, 24, 32** and **12** directions, so the eleven are **not substrate-invariant**. Only the
   degeneracy count (**51**) is; the other two come from a weight that vanishes - a **chosen formula**, not substrate.
5. **Symmetry:** group order **192**, the eleven form an invariant set **as a span** (a rotation carries a mode into a
   combination of its quadratures - the test must be a span test), the alternating mode's orbit **2**, channel 32's
   cosine **3**.

**Status.** COMPLETE. Tests `Y_G_061_Tests` **7/7**. Core `AT.Core/ResearchXH/ResidualPhaseAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_061.md`. Registry: added as **Derived** (`residual-phase-audit`); group-G
consistency counts 31/**62** registry/**48** survives/**48** minimal time sector. **No reclassification.**

## ResearchY-G_060 - Phase Evolution Audit (COMPLETE, DERIVED - the promotion exists, in the norm-preserving form)

**Question.** Can the difference-generated phase push (**G_059**) be **promoted to an actual update rule**? Test
`rho(t+1) = rho(t) + eps*D*rho` and other AT-native updates. Measure the **phase evolution rank**, the **amplitude
evolution rank**, **stability** and **fixed points**. Critical: does any AT-native update generate non-trivial phase
dynamics?

**Answer: DERIVED - the promotion exists and it is the norm-preserving form; the form the question names is the one that
fails.** Every candidate is circulant, so **one complex multiplier per channel is the whole update** and nothing is
estimated.

1. **The question's own step is stable, and that is exactly why it fails.** `rho + eps*D*rho` has
   `|mu|^2 = 1 - 2 eps x (1 - eps)`, so its symmetric part is **negative semi-definite**: a **dissipative** step, not the
   skew step the name suggests. Its attractor is the kernel of D - **the constant** - measured as a deviation norm falling
   **1.005E+000 -> 2.424E-001** over 20000 steps. **The closure with G_059: the canonical state has no phase content
   because this is where the flow stops.**
2. **A claim is withdrawn.** The draft asserted a monotone decay; measured, the phase content **oscillates** on the way
   down (0.065 at 100 steps, **0.303** at 1000) while the envelope falls.
3. **The phase survives only where |mu| = 1, and exactly one form has it.** Backward difference
   (**1.000002 .. 1.002000**) and centred difference (**1.000000 .. 1.000001**) amplify every non-constant mode and leave
   the simplex in measured step counts (backward: **796 / 80 / 9** at eps = 1E-3 / 1E-2 / 1E-1); the difference's own
   exact flow `exp(eps D)` contracts like the forward step; the **UNITARY** form (the exact flow of the **centred**
   difference) has |mu| = **1.000000000 exactly**, sustains all **42** phase directions, conserves the deviation norm
   exactly (**1.005011E+000**) and the sum, and keeps every cell positive over **20000** steps.
4. **The rank is 42 of 53 by two independent routes**, and the **11 unreachable** directions are channels **14, 19, 24,
   32, 40** (both quadratures) plus **the alternating mode**. The centred family **annihilates** the alternating mode -
   `(S - S^-1) e_48 = 0` - so its fixed-point dimension is **2**; the control fixes all **96**.
5. **The AT-native positivity guard never fires** (agreement with the unclipped step to **5E-15**, minimum cell
   **7.086E-001**).
6. **Three defects recorded rather than hidden:** a guard that divided by a near-zero mean (and annihilated a zero-mean
   probe - a **probe** defect, not a finding about AT); a rank scan that counted **63**, then **54**, directions inside a
   **53**-dimensional space; and a difference-of-orbits construction with a **measured 2.329E-008 leak** into the
   unreachable directions. Reachability is now measured from the **linear** response in the phase sector's own
   coordinates.

**Status.** COMPLETE. Tests `Y_G_060_Tests` **7/7**. Core `AT.Core/ResearchXH/PhaseEvolutionAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_060.md`. Registry: added as **Derived** (`phase-evolution-audit`); group-G
consistency counts 30/**61** registry/**47** survives/**47** minimal time sector. **No reclassification.**

## ResearchY-G_059 - Flow Source Audit (COMPLETE, DERIVED - the source is the difference itself)

**Question.** What AT **object** can produce a **non-zero push rank**? Given **G_058** that all admissible dynamics have
identical local rank. Candidates: occupancy imbalance, phase imbalance, amplitude-phase coupling, actualization pressure
(uniform and local-rate readings), spectral mismatch, boundary assignment. Measure the **push vector**, the **flow
source**, the **fixed points**. Goal: the first genuine source term.

**Answer: DERIVED - the source is the DIFFERENCE ITSELF, derived exactly rather than argued.**

1. **The canonical state has no phase content: 9.246E-015**, the floating-point floor. The phase sector is not where the
   state **is** but where a source **sends** it. This explains what **G_054** recorded and refused to count - its
   addressed-state determination was a tautology because **the phase coordinates are zero**, so the statement was
   `0 = 0` - and it names **G_052's reconstruction residual (2.442E-015) as the phase part itself**.
2. **The mechanism is exact.** The cyclic difference's Fourier multiplier is `1 − e^(−iδ_c)` - a rotation **composed with
   a rescale** - so its action on a single visible mode returns exactly `{|sin δ_c|, 2 sin²(δ_c/2)}`: one amplitude part,
   one phase part, no third term. **Verified on 42 of 42 visible modes to 1E-12.** The phase share is `cot(δ_c/2)`:
   phase-heavy at channel **1** (30.6 : 1), amplitude-heavy at channel **47** (0.033 : 1); residue **2.141E-003 …
   1.998E+000**. **A filter cannot create phase content at all** - its multiplier is real and symmetric under
   `c → −c`, which is exactly what preserves the visible subspace.
3. **The push table.** occupancy imbalance 1.368E+000 / phase **7.155E-001** / rank 1; phase imbalance 9.246E-015 /
   rank 0; amplitude-phase coupling 8.295E-016 / rank 0; actualization (**uniform**) 9.798E+000 / phase **0.000E+000** /
   rank 0; actualization (**local rate**) 3.407E-001 / phase **6.969E-003** / rank 1; spectral mismatch 2.572E-002 /
   phase 2.388E-016 / rank 0; boundary assignment 1.575E-002 / phase **9.282E-003** / rank 1.
4. **Creating versus amplifying, measured** over a 192-state deterministic family: creating sources (occupancy
   imbalance, local-rate advance, seam pair) push from **any** non-uniform state and are **fixed only at the uniform
   state** (191 of 192); amplifiers (phase imbalance, amplitude-phase coupling, spectral mismatch) move **exactly** the
   106 phase-bearing states. At the canonical state only the **three creating sources** push.
5. **No candidate is a pure phase source** (phase-heaviest: the seam pair, **7.296E-001**) and **no two coincide**.
6. **A claim is withdrawn** (the difference is not a pure rotation) and **an accounting is corrected**: G_057 returned
   **0.000E+000** from **two side conditions** without measuring the time-like component it names; the uniform reading
   is **0.000E+000** (its conclusion holds) while the local clock-rate reading is **6.969E-003**, and **AT defines no
   update rule for the organisation at all**. **The number survives; the reason for it does not.**

**Status.** COMPLETE. Tests `Y_G_059_Tests` **7/7**. Core `AT.Core/ResearchXH/FlowSourceAudit.cs`; doc
`Docs/ResearchY/G_GravitySource/ResearchY-G_059.md`. Registry: added as **Derived** (`flow-source-audit`); group-G
consistency counts 29/**60** registry/**46** survives/**46** minimal time sector. **No reclassification.**

## ResearchY-G_058 - Phase Dynamics Closure Audit (COMPLETE, BOUNDARY - the requested criterion cannot discriminate)

**Question.** Can any AT **update rule** generate a **non-trivial phase evolution**? Tests: single scalar flow, multiple
coupled scalar flows, vector-valued flow, connection-driven flow, T1/T2-coupled flow. Measure the **phase-rank of the
evolution operator**. Critical: can any existing AT process reach **rank 53**?

**Answer: BOUNDARY - and the closure's first result is that the requested measure cannot discriminate.** **Three ranks
are measured, because the question asks for one.**

| rule | push | linearisation | operator |
|---|---|---|---|
| single scalar flow | 1 | 53 | 53 |
| multiple coupled scalar flows | 1 | 53 | 53 |
| vector-valued flow | 1 | 53 | 53 |
| connection-driven flow | 1 | 53 | 53 |
| T1/T2-coupled flow | 1 | 53 | 53 |
| identity update (**CONTROL**) | **0** | **0** | **53** |

1. **The warning is stated before any figure from that column is quoted.** Every generator linearises to full rank
   (**True**) and every operator rank is full (**True**) - the identity reaching **53** operator rank while pushing
   **0**. Two structural reasons: any state update is the **identity** plus a small term and the identity is invertible
   on the phase sector; and **a scalar flow's linearisation is its potential's Hessian** - positive definite, hence full
   rank on **every** subspace. **A criterion the do-nothing rule satisfies is not a criterion.**
2. **The push rank is the only column that separates.** The state moves along **one** vector per instant, so all five
   candidate rules push **1** direction and the identity pushes **0**; and **coupling several scalar flows does not
   help** - a sum of gradients is still one vector. **The ceiling belongs to being a push, not to being scalar.**
3. **The process the theory runs has nothing to push with.** Update-rule spatial part **0.000E+000**, coupling census
   **0**, **actualization push rank 0** - so no AT process reaches the phase sector, sharper than a rank count: none
   supplies a generator with phase content.
4. **An expectation is withdrawn.** The premise of a hierarchy (vector generators at **53**, scalar rules at **1**) is
   **REFUTED** - no rank measure separates the scalar from the vector-valued rules, in either column.
5. **The thread closes.** G_046-G_051: the kernel **is** the phase sector, physical not gauge. G_052-G_053: the split is
   unique and phases have unreproducible effects. G_054-G_055: freely assigned; no scalar prefers one. G_056-G_057:
   structures span all 53, processes act on none. **AT contains structures sensitive to every phase direction and runs
   none of them** - and the one rank that would show a rule acting on the phase is **0** for the process and **1** for
   every hypothetical rule alike.

**Status.** COMPLETE. Tests `Y_G_058_Tests` **6/6**; group G **434/434**. Core
`AT.Core/ResearchXH/PhaseDynamicsClosureAudit.cs`; doc `Docs/ResearchY/G_GravitySource/ResearchY-G_058.md`. Registry:
added as **Boundary** (`phase-dynamics-closure-audit`); group-G consistency counts 28/**59** registry/**45** survives/
**45** minimal time sector. **No reclassification.**

## ResearchY-G_057 - Phase Flow Audit (COMPLETE, BOUNDARY)

**Question.** Can any existing AT process change the phase coordinates? Candidates: clock flow, acceleration flow, field
flow, connection flow, T1/T2 coupling. Measure phase velocity, phase rank, selection power.

**Answer: BOUNDARY - no AT process the theory RUNS changes the phases, while five POTENTIALS would; and a flow needs a
potential, a potential is a scalar, so every candidate has phase rank 1 and selection power 1/53.**

| candidate | velocity | fraction | rank | selection power |
|---|---|---|---|---|
| clock flow | 1.241E-002 | 3.768E-003 | 1 | 0.0189 |
| acceleration flow | 2.933E-002 | 5.204E-002 | 1 | 0.0189 |
| field flow | 1.678E-003 | 8.572E-002 | 1 | 0.0189 |
| connection flow | 1.678E-003 | 8.572E-002 | 1 | 0.0189 |
| T1/T2 coupling | 4.813E-001 | 4.272E-001 | 1 | 0.0189 |

- **The running process is phase-static:** actualization phase velocity **0.000E+000** (update-rule spatial part
  **0.000E+000**, census **0**) - so **no AT process generates a phase flow**.
- **Union:** all five flows together constrain **4 of 53**, leaving **49** free.
- **Degeneracy measured:** the **field and connection flows coincide** (gap **0.000E+000**) - the field strength AT builds
  *is* `h(ρ)·Δρ` - so five names describe **4** distinct flows.
- **The series now says something complete:** G_056 showed AT contains **structures sensitive to every phase direction**;
  G_057 shows the **processes it runs are sensitive to none** - the phases are freely assigned not because the theory is
  blind to them, but because **nothing the theory does acts on what it can see**.

Registry: added as **Boundary** (`phase-flow-audit`); group-G consistency counts updated. Tests: `Y_G_057_Tests` 7/7.

## ResearchY-G_056 - Non-Scalar Selection Audit (COMPLETE, DERIVED)

**Question.** Can any existing non-scalar AT structure span the 53-dimensional phase sector? Candidates: phase vector
field, connection structure, T1/T2 sector coupling, edge-holonomy network, causal-order tensor. Require phase rank > 3.

**Answer: DERIVED - the gradient-based structures span the phase sector EXACTLY (rank 53 of 53), a factor of 17 above
G_055's scalar ceiling of 3.**

| candidate | outputs | phase rank | verdict |
|---|---|---|---|
| **phase vector field** | 96 | **53** | DERIVED |
| **connection structure** | 96 | **53** | DERIVED |
| **T1/T2 sector coupling** | 288 | **53** | DERIVED |
| edge-holonomy network | 96 | **0** | REFUTED |
| causal-order tensor | 96 | **0** | REFUTED |

- **The phase rank generalises G_055's count:** a scalar has one output so its rank cannot exceed 1.
- **Why the full rank is reached (structural, not numerical):** differentiation is **injective on every non-constant
  mode** and the phase sector contains only non-constant modes - **53 of 53** directions have a non-zero derivative.
- **Null candidates reported rather than omitted:** the **edge-holonomy** network read from the **link phases** is
  decoupled (**census 0**) and the occupancy-derived holonomy is the **same candidate renamed**; the **causal-order
  tensor** from **signs** is **piecewise constant**.
- **Two honesty notes:** the coupling's **T1 half vanishes identically** (**0.000E+000**) so its rank comes from the
  symmetric half; the candidate table carries **no verdict literals** (computed from the measured rank - project rule 6).
- **Spanning is NOT selecting:** G_054's no-process measurement holds (spatial **0.000E+000**, census **0**), so the
  phases stay **freely assigned** and G_055's deficiency of 50 is explained as a deficiency of **scalars**.
- **The sharpest statement of the thread:** AT contains structures sensitive to every phase direction and runs none of
  them.

Registry: added as **Derived** (`non-scalar-selection-audit`); group-G consistency counts updated. Tests:
`Y_G_056_Tests` 6/6.

## ResearchY-G_055 - Phase Selection Principle Audit (COMPLETE, BOUNDARY - deficiency measured)

**Question.** Can any existing AT quantity assign a preferred phase state? Candidates: entropy, free room, actualization
density, flux sector, clock functional, field functional. Critical question: why this phase instead of another?

**Answer: BOUNDARY - none can, and the deficiency is measured: a scalar functional's gradient is ONE vector, so it
constrains at most one phase direction; measured rank 3, deficiency 50.**

- **Three of the six constrain nothing at all** (stronger than weakness): free room IS the simplex constraint (constant
  gradient, phase projection **3.952E-014**), actualization density's scalar level IS that constant (**4.104E-016**),
  flux sector decoupled (**0.000E+000**, census 0).
- **The other three contribute one direction each:** entropy **3.334E-004**, clock functional **1.241E-002**, field
  functional **1.678E-003**.
- **Counting:** 53 phase dimensions, 6 candidates, **rank 3**, **deficiency 50** - free for every combination.
- **No sensitive candidate is stationary** at the audited state (2.808E-004 / 1.038E-002 / 6.252E-004), so nothing is
  extremised either.
- **The critical question:** "why this phase instead of another" has **NO answer inside AT** for the 50 unconstrained
  directions. What would break it: 53 independent scalars, or one non-scalar structure spanning the phase sector.
- **Three defects caught by the measurements:** (1) **cancellation** - free room's exactly-constant gradient differenced
  at step 1E-6 left a **2.540E-008** floor and made a phase-blind candidate look sensitive; gradients are now analytic
  where trivial, differenced at step 1E-4 otherwise; (2) the exact-zero floor (~**4E-14**) is stated with its origin
  rather than tuned away; (3) a **delegate-identity** bug (`ReferenceEquals` on a method group never matches) - the
  candidate lookup is now **by name**.

Registry: added as **Boundary** (`phase-selection-principle-audit`); group-G consistency counts updated. Tests:
`Y_G_055_Tests` 6/6.

## ResearchY-G_054 - Phase Determination Audit (COMPLETE, BOUNDARY - freely assigned)

**Question.** What fixes the 53 phase coordinates? Candidates: symmetry, occupancy, multiplicity, attractor structure,
actualization history, boundary assignment. Requirements: no new primitive; clock, acceleration and field laws
preserved.

**Answer: BOUNDARY - freely assigned.** What would have to be added: a coupling whose **gradient has a phase component**
*and* which the theory **actually runs**. AT has the first without the second.

- **The conservation theorem (centre of the audit):** invariant gradients lie in the **amplitude-plus-mean** subspace
  (G_052's identity), so an invariant-driven flow **cannot change** the phase coordinates - measured at **1.431E-015**
  (an invariant gradient's phase component: **7.625E-012**). This disposes of **attractor structure** and
  **actualization history** at once.
- **Symmetry does not fix them - it is defined by not seeing them:** its invariants ARE the amplitudes plus the mean, so
  a symmetry move changes the coordinates by **2.540E-001**, while an amplitude move changes them by **3.232E-015**.
- **Sensitivity is not determination:** the clock functional's gradient carries a phase fraction of **3.768E-003** and
  the field's **8.572E-002**, but no AT process runs such a flow (spatial part **0.000E+000**, census **0**).
- **Multiplicity fails bluntly:** the recorded spectrum (**45 levels, 96 modes**) is Laplacian-fixed, not state-dependent.
  The addressed state's determination is a **tautology**, recorded and explicitly **not counted**.
- **A bug caught by the measurement:** `DerivedSectorSplit` returns **Electric first**; my accessor read it as the
  **spatial** part (**1.424E-002**) and flipped the verdict to DERIVED. The spatial part is genuinely **0.000E+000**.

Registry: added as **Boundary** (`phase-determination-audit`); group-G consistency counts updated. Tests:
`Y_G_054_Tests` 7/7.

## ResearchY-G_053 - Phase Sector Dynamics Audit (COMPLETE, DERIVED)

**Question.** Do phase modes have independent physical effects beyond amplitudes? Construct a pure amplitude and a pure
phase perturbation; measure the clock, acceleration and field responses; which observable is uniquely phase-sensitive?

**Answer: DERIVED - the phase effects are independent, and the reason is GEOMETRIC.**

- **Orthogonal and both real:** perturbation overlap **2.168E-018**; all three readings respond to both (clock
  **6.678E-003** vs **6.772E-003**, acceleration **8.904E-004** vs **7.024E-004**, field **1.128E-004** vs
  **9.971E-005**) - so the question is not **silence** but **imitation**.
- **The independence test is a reproducibility test:** projecting each reading's phase response onto the span of its
  responses to all **42** amplitude directions (span rank **42 of 42**, verified) leaves a non-reproducible fraction of
  **1.0000 / 1.0000 / 0.9926** (clock / acceleration / field).
- **The geometry:** in channel 1 the amplitude move changes the magnitude by **3.464E-001** and the angle by
  **2.220E-015** - **machine zero** - while the phase move changes the angle by **3.463E-001** and the magnitude by
  **6.059E-002**. **The amplitude sector is exactly the no-rotation sector.** (Draft expected a 100× resize/rotate
  split; the measurement is sharper in one place, weaker in another - criterion corrected to what is measured.)
- **First uniquely phase-sensitive observable = the quadrature functional** (projection onto a hidden mode):
  **2.000E-002** on the phase move vs **3.272E-015** on every amplitude move. AT's own laws are **MIXED** (all three
  take from both sectors).

Registry: added as **Derived** (`phase-sector-dynamics-audit`); group-G consistency counts updated. Tests:
`Y_G_053_Tests` 6/6.

## ResearchY-G_052 - Amplitude Phase Audit (COMPLETE, DERIVED)

**Question.** Can rho be decomposed uniquely into an amplitude sector (42) and a phase sector (53)? Measure
orthogonality, invertibility, reconstruction accuracy, clock / acceleration / field responses. Goal: the exact
interface between visible amplitudes and physical phases.

**Answer: DERIVED - the split is unique, orthogonal and exact, and the interface is an IDENTITY.**

- **Orthogonal and exact:** amplitude **42** + phase **53** = **95**; sector overlap **4.418E-015**; reconstruction
  rho = mean + A + P at **2.442E-015**.
- **Uniqueness is checked, not inferred:** rebuilding the amplitude subspace from a different deterministic spanning
  set reproduces the projection to **3.349E-012** - canonical, not basis-dependent.
- **The interface (the goal):** the contraction row space has rank **43** = the **42** amplitude modes **+ the mean**,
  and is orthogonal to every phase mode (**7.111E-014**), so the spans are **equal**:
  **(phase) = kernel of the contractions (53)** · **(amplitude) = row space minus the mean (42 = 43 - 1)**.
- **Observables:** a **linear** functional splits exactly (**2.220E-016**); AT's readings carry a cross term
  (clock **1.128E-006**, acceleration **4.588E-007**, field **2.812E-008**) that scales as **s²** (halving ratios
  **0.2498 / 0.2503 / 0.2502**) - **second order**, i.e. curvature rather than failure. Every reading takes from **both**
  sectors (clock **1.168E-003** vs **1.031E-003**).
- **Two harness bugs caught by the measurements:** the alternative basis used 30 combinations for a 42-dim subspace
  (**0.172**), and additivity was first measured on **maxima** of responses (**0.499** spurious first order); it is now
  measured on response **vectors** with an unclamped step, giving the true **0.25**.

Registry: added as **Derived** (`amplitude-phase-audit`); group-G consistency counts updated. Tests: `Y_G_052_Tests`
7/7.

## ResearchY-G_051 - Phase Sector Audit (COMPLETE, PHYSICAL)

**Question.** Do the 53 phase-sector directions carry physical or gauge information? Measure the clock, acceleration,
field and flux-sector responses; separate observable phase directions from pure gauge ones.

**Answer: PHYSICAL - all 53 are observable and none is gauge.**

- **The responses are uniformly non-zero:** stepping along each of the 53 modes, the clock-rate **multiset** moves for
  all of them - smallest clock **2.136E-003**, acceleration **4.859E-004**, field **5.204E-005**. The multiset is the
  right object (what a law *reports*, not which cell carries what - G_048's distinction).
- **First-order, which separates physical from nearly gauge:** a second-order response would be invisible to linear
  response; halving the step shows every direction scales as the step.
- **The flux zero is a DECOUPLING, not gauge-ness:** the label reads the **link phases** and the coupling census is
  **0**, so no organisation move can change it. The verdict criterion **excludes** the flux silence - counting it would
  have made every direction in the theory look gauge the moment a decoupled probe was added.
- **The control is a real gauge direction:** a symmetry-orbit move leaves every multiset at **0.000E+000**, and the
  test finds **none** among the 53.

**What this changes.** G_050 named the phase sector; G_051 settles its status: the quadratures completing each
channel's magnitude are hidden from the **invariant distance-class algebra**, not from observation. The organisation's
phase is physics.

Registry: added as **Derived** (`phase-sector-audit`); group-G consistency counts updated. Tests: `Y_G_051_Tests` 6/6.

## ResearchY-G_050 - Kernel Structure Audit (COMPLETE, DERIVED)

**Question.** What physical structure do the 53 kernel directions represent? Measure basis vectors, symmetry classes,
multiplicity relation, clock / acceleration / field signatures.

**Answer: DERIVED - the kernel is the PHASE SECTOR: one hidden quadrature per populated doublet, both quadratures of
each empty channel, and the alternating mode - 47 + 5 + 1 = 53.**

- **Two hypotheses were refuted before the right one was named.** (a) The **dihedral** argument predicted whole doublets
  and the measurement refused it: **42 of the 47 doublets are HALF hidden**, because a real 2-dim irreducible admits a
  2x2 commutant. The correct argument is the **sharper** one - the contractions are **circulant**, so **translation
  invariance** (not reflection) forbids mixing - and measured per mode **no mode is split anywhere**. (b) The
  **short-wavelength** hypothesis was refused too: hidden and visible modes are **interleaved** (separated in
  frequency: **False**, correlation against frequency **-0.0279**).
- **The count relation names the kernel:** hidden **53** modes, visible **42** (= 95), **42** half-hidden doublets,
  **5** **empty** channels whose set is **identical** to the doubly-hidden ones: **47 + 5 + 1 = 53**, with the visible
  42 being the **magnitudes**.
- **This reconciles G_040 exactly:** its loss of **47** is the **generic** case (47 orientations, no empty channel);
  this state hides five extra channels' worth because it carries nothing in them.
- **Signatures:** every one of the 53 hidden modes responds to all three lossless readings - the interpretation does
  not depend on which reading is used; no frequency ordering exists.

Registry: added as **Derived** (`kernel-structure-audit`); group-G consistency counts updated. Tests:
`Y_G_050_Tests` 6/6.

## ResearchY-G_049 - Clock Primacy Audit (COMPLETE, LOSSLESS but NOT UNIQUE)

**Question.** Is the clock pattern the unique lossless observable of rho? Compare clock, acceleration, field-strength
and contractions on invertibility, retained information, rank, kernel. Goal: test whether time is the primary
observable of rho.

**Answer: LOSSLESS - but NOT UNIQUE. Three of the four readings are information-equivalent to rho.**

| reading | rank | retained | inverse | verdict |
|---|---|---|---|---|
| clock pattern | **95** | **1.000** | closed form rho = rate^d, residual **4.441E-016** | **LOSSLESS** |
| acceleration pattern | **95** | **1.000** | **0** collisions, residual **0.000E+000** | **LOSSLESS** |
| field-strength pattern | **95** | **1.000** | **0** collisions, residual **0.000E+000** | **LOSSLESS** |
| contraction observables | **43** | **0.453** | 52 dimensions missing | **LOSSY** |

- **The test is honest in both directions:** each inverse is attempted by projected gradient descent from three
  displaced starts with the audited state as the **control**, which recovers itself - so the machinery can succeed
  before its failures are believed.
- **A proof was withdrawn:** the intended collision (reflecting the state through its mean) failed because the
  reflection reverses differences of **rho** while the acceleration pattern uses differences of the **rate**, and the
  cube root is not linear - the reflected pair sat **1.951E-002** apart.
- **A hypothesis was NOT confirmed:** no full-rank reading was measured lossy; rank and invertibility **coincided**
  here - an **empirical outcome of this state, not a theorem**.
- **Two bugs caught by the measurement:** the search originally mutated the memoised base state in place (producing
  NaNs), and it followed configurations that left the positive simplex; it now clones the state and **rejects** invalid
  configurations.
- **What distinguishes the clock** is **invertibility in closed form** - one call per cell - not uniqueness of
  information. Time is **a** primary observable; the primacy is one of convenience.

Registry: added as **Derived** (`clock-primacy-audit`); group-G consistency counts updated. Tests:
`Y_G_049_Tests` 6/6.

## ResearchY-G_048 - Clock Completeness Audit (COMPLETE, MAXIMAL)

**Question.** Is the clock pattern the maximal observable of rho? Compare the clock, acceleration, field-strength and
contraction readings; measure kernel rank, information retained and observable dimension; decide
clock > acceleration > contraction or equivalence.

**Answer: MAXIMAL - and it is maximal AND TIED WITH TWO OTHERS.**

| reading | observable dim | information retained | kernel rank |
|---|---|---|---|
| clock pattern | **95** | **1.000** | **53** |
| acceleration pattern | **95** | **1.000** | **53** |
| field-strength pattern | **95** | **1.000** | **53** |
| contraction observables | **43** | **0.453** | **0** |

- **The clock pattern is complete and invertible:** rho = rate^d with a residual of **4.441E-016** - not merely of full
  rank.
- **The acceleration pattern ties at the top:** differences lose the global constant on an arbitrary vector, but the
  simplex already excludes it, so the difference map is injective on the tangent space.
- **The field-strength pattern ties too, and my prediction of partiality was withdrawn and recorded:** the draft
  expected a shortfall because the coupling is evaluated at each cell; the tangent map is diagonal-plus-difference with
  a positive coupling, hence generically invertible.
- **The contractions are redundant** and the only reading that loses anything: 43 against 95 - a sub-algebra of what
  the clock resolves, which is what G_040's ceiling of 48 always was (the non-addressed ceiling).
- **The control:** aggregating the same law gives observable dimension **1** of 95 - the completeness lives in
  **addressing** the cells.
- **Ordering:** clock 95 = acceleration 95 = field 95 > contractions 43, so the proposed chain is refuted on both
  strict steps and "all equivalent" is refuted by the contractions.

**What this changes.** G_040's ceiling is placed as the ceiling of the **non-addressed** sub-algebra, not of
observation; G_047's clock is shown to be complete rather than only kernel-resolving; and the ordering question is
answered with a two-level structure.

Registry: added as **Derived** (`clock-completeness-audit`); group-G consistency counts updated. Tests:
`Y_G_048_Tests` 7/7.

## ResearchY-G_047 - Kernel Observable Audit (COMPLETE, DERIVED)

**Question.** Which observable detects the kernel directions directly? Given G_040 (95 = 48 + 47) and G_046 (the hidden
set is the kernel). Requirements: responds to hidden directions, distinguishes kernel states, independent of the
contractions. Measure clock, acceleration and field change; construct the minimal observable basis.

**Answer: DERIVED - the first observable is AT's own clock law read at each cell; the minimal basis is one reading per
kernel dimension.**

- **The kernel is built at the audited state:** state **96** (95 on the simplex), contraction rank **42** (43 with the
  simplex direction), **kernel 53** against G_040's ceiling of **47**; every basis direction changes no contraction
  (**5.684E-013**).
- **The readings:** addressed clock **1.726E-002**, acceleration **3.298E-002**, field strength **6.479E-003**, while
  every contraction stays at **5.684E-013** - that pair answers the independence requirement (a quantity fixed while
  the contractions moved would be a function of them).
- **Full rank:** the reading map on the kernel has rank **53** on a **53**-dimensional kernel (the acceleration resolves
  it too); two different kernel directions differ by **2.338E-002**.
- **The minimal basis is structural, not fitted:** a single cell's clock rate has a gradient along one direction, so
  resolving d kernel dimensions needs d independent cell readings - **53 readings** from a maximum of 96 cells.
- **Against G_040:** the kernel measures **53** at the audited state against the **ceiling 47**; the basis is sized to
  the measurement, and both numbers are reported.

**What this changes.** G_046 showed the hidden 47 reach the local laws; G_047 names the observable that reads them
directly and sizes the instrument - the clock pattern, one reading per hidden dimension, no new primitive.

Registry: added as **Derived** (`kernel-observable-audit`); group-G consistency counts updated. Tests:
`Y_G_047_Tests` 7/7.

## ResearchY-G_046 - Rho Accessibility Audit (COMPLETE, OBSERVABLE)

**Question.** Can the hidden 47 dimensions of rho ever influence an observable? Given G_040 (95 = 48 retained + 47
hidden) - do they affect clocks, acceleration, flux sectors, field strengths?

**Answer: OBSERVABLE - and the first identification of the hidden directions was withdrawn.**

- **The first answer was wrong and is recorded.** The draft identified the hidden directions with the substrate-symmetry
  **orbit**; the measurement refused it - the span is **84 dimensions, not 47**. A symmetry move is gauge-like because it
  **relabels**; the hidden set is the **kernel** of the contractions.
- **Symmetry moves are relabelling-only (the gauge control):** multisets unchanged over the whole orbit (clocks
  **0.000E+000**, acceleration **0.000E+000**, fields **5.140E-016**, flux **0.000E+000**) while the addressed reports
  move (1.719E-001 / 1.603E-001 / 5.823E-002); controls hold (addressed pattern **4.937E-001**; phase moves the label
  **1.000000**).
- **The decisive measurement - a validated hidden step:** a **sum-neutral** kernel direction changes a contraction by
  **5.116E-013** and still moves the **clock-rate multiset 3.764E-003**, **acceleration 3.010E-003**, **field strength
  6.327E-004**, while the **flux sector stays 0.000E+000** (census **0**).
- **A bug caught by the measurement:** a first version renormalised the perturbed state, moving it along *observable*
  directions too and making a "hidden" step change a contraction by **2.061**; the sum-neutral projection makes the step
  honest.
- **Targets:** clocks, acceleration, field strengths **OBSERVABLE**; flux sectors **HIDDEN** - as the theory's own
  decoupling requires.
- **One number recorded against G_040:** the kernel measures **53** at the audited state against the **ceiling 47**
  (retained **43** against **48**) - so G_040's 48 is a **ceiling over states**, a refinement rather than a
  contradiction.

**What this changes.** G_040 capped the non-addressed algebra at 48 and lost 47; G_046 shows the 47 are **not**
permanent gauge - they reach the local laws. The flux sector's hidden status follows from E_013's decoupling.

Registry: added as **Derived** (`rho-accessibility-audit`), with a refinement note back to G_040. Tests:
`Y_G_046_Tests` 7/7.

## ResearchY-E_016 - Sector Population Principle Audit (COMPLETE, REFUTED)

**Question.** Do any existing AT quantities break the flat sector measure of E_015? Candidates: occupancy free room,
multiplicity structure, D96 hierarchy, compression laws, actualization rate. Goal: the first non-flat weighting without
new primitives.

**Answer: REFUTED - no existing AT quantity breaks the measure, and the detector was validated before it was trusted.**

- **The detector, validated first.** The search is a sector-restricted sum of a weight: synthetic breaker one - a
  **non-uniform local factor** on the link phase (a potential) - breaks it at **1.6E-001**; breaker two - a
  **recipe-phase bridge** - at **3.3E-001**; controls are **0.000E+000** for the trivial weight and a phase-uniform
  potential. The machinery can find a breaker and finds **none** among the five.
- **The five fail for two distinct reasons.** Three are **recipe-side** (free room, hierarchy, compression laws): the
  sector labels the **phase** half and the halves are decoupled (census **0**), so the sums **factorise** - free room
  per sector **4096, 4096, 4096, 4096**, spread **0.000E+000**. Two are **phase-side and sector-symmetric**:
  multiplicity spread **0.000E+000** (E_015's bijection) and a purely electric update (spatial **0.000E+000** against
  time-like **1.424E-002**).
- **A prediction withdrawn:** the coarse-observable conditional distribution is **forced flat** by the **half-period
  symmetry** (spread **0.000E+000**) - my first draft was wrong, and it is recorded as withdrawn.
- **The real nearest miss is a weight, not an observable:** a phase-reading weight breaks the class sums at
  **1.6E-001**. AT supplies no such weight.
- **What a breaker would have to be:** a **potential** for the link phase or a **recipe-phase coupling** - both **new
  primitives**. Open in principle, closed in practice, and now in a testable form.

**What this changes.** E_015's flat measure invited exactly this search; E_016 closes it for every quantity AT
currently has, and states the two doors a breaker would have to come through. The verdict has a **live branch**.

No group-G registry claim, so claim counts are unchanged. Registry: added as **Refuted**
(`sector-population-principle-audit`), reclassifying nothing. Tests: `Y_E_016_Tests` 7/7; group E **110/110**.

## ResearchY-G_045 - Observed vs Hidden Dimension Audit (COMPLETE, REFUTED)

**Question.** Can the apparent 3D world emerge as a PROJECTION of a higher-dimensional ACTUALIZATION SPACE?
Compared the 3D host space against the N-dimensional state space; measured observable dimension, spectral
dimension, information dimension, attractor dimension.

**Answer: REFUTED - the two "dimensions" are different INDICES, and the one that could hide is measured exactly.**

- **THE GROUND IS RECOMPUTED, NOT CITED.** Rebuilding the whole d-torus spectrum (96 cells, radius-6 Laplacian,
  tensor powers) reproduces THREE NUMBERS FROM THREE OTHER AUDITS: the ring's **45** distinct levels and the
  reachable room **51** (G_039), and the cube's **16 080** distinct levels (G_033) - which fixes the reachable room
  at d = 3 to **868 656**, the figure project memory quotes. Rooms: 51 -> 8 184 -> 868 656.
- **TWO EXACT PROBES NAME d UNAMBIGUOUSLY.** (1) **THE INFORMATION DIMENSION IS EXACTLY THE FACTOR COUNT** - box
  counting by walking every lattice site gives 2/4/8/16 occupied boxes at 2/4/8/16 boxes per axis, so
  ln N / ln n = **1.000, 2.000, 3.000** exactly. (2) **THE SYMMETRY GROUP IS EXACT TOO** - order 2^d*d! =
  **2, 8, 48**, and the order 48 occurs at exactly one dimension. A hidden fourth direction would evade neither.
- **THE SPECTRAL PROBES FAIL, AND THE AUDIT RECORDS ITS OWN FAILURE.** The gap is **DIMENSION-INDEPENDENT** -
  0.3863508934 at every d, because a single tensor factor may be excited while the others stay at zero, so a gap
  cannot count directions; and the density-of-states estimate N(mu) ~ mu^(d_s/2) is **BIASED BY UP TO 80 %** -
  1.26-1.81 (d = 1), 2.28-2.61 (d = 2), 2.99-3.72 (d = 3) against the true 1, 2, 3, wrong in BOTH directions with
  the window - and it **DIVIDES BY ZERO** on a legitimate window (the lowest six levels of the d = 3 torus are not
  distinct enough). The first implementation printed **Infinity**; that is now recorded as a degenerate window.
  So the spectral dimension is NOT usable evidence on a finite substrate (copilot-instructions rule 9).
- **THE OBSERVABLE DIMENSION IS NEITHER 3 NOR THE STATE SPACE:** families C(48 + d, d) - 1 = **48, 1 224,
  20 824** - the extra dimensions are largely RESOLVABLE in principle, not hidden behind a 3D window.
- **VERDICT - A SPECIFIC REFUTATION, NOT A GENERAL DISCOMFORT.** The apparent three IS the tensor-factor count
  (named exactly twice); the higher-dimensional actualization space is the space of **CONFIGURATIONS OVER** that
  substrate - a different index, not the same quantity at two sizes; and the only probe that genuinely cannot
  resolve the factor count is the spectral one, which is precisely the probe that certifies nothing here.
- **OPEN:** unchanged - G_043's mechanism question (why the working set begins at 3).

Registry: added as **ClockOnly -> SURVIVES**, `ScanDetectsIt: false`; counts become **32/11/3 of 46**. No
reclassification. G_033's live classifier now reads **27** substrate suites (44 classified in total).
Tests: `Y_G_045_Tests` 7/7; group G 350/350.

---

## Frontier status snapshot at 2026-09-17 (carried over verbatim from the project-memory file)

The summary the project-memory file carried at the close of the G_068-G_080 and QM_007-QM_011 sequence. Kept here
because the state file now states the current status in compact form and links here instead of repeating it.

## Status of the two research frontiers closed in this sequence (2026-09-17)

**The spectral fingerprint (group QM, QM_007 - QM_011).** The native six-shell spectrum is an **input** to the theory
rather than a consequence of a law (QM_007: the recorded trace **1152 = 2 x 6 x 96** fixes the shell count and the
**45 levels** fix which six - **exactly 1 of 63** subsets reproduces the record). It is **operative**: indispensable
for the numbers AT quotes and dispensable for the relations it states (QM_008: **5 UNCHANGED + 5 BOUNDARY +
2 REFUTED** of twelve conclusions). **No prediction fails without it** (QM_009), it carries **numerical realisation
and not physical content** (QM_010: **3 CORE + 5 FINGERPRINT + 0 ARTEFACT**), and **no genuinely physical
consequence requires it** (QM_011: **0 observables** require the native spectrum, out of a repo inventory of 11 that
names no spectral object). **The one physical difference it does make is the dispersion fold's group-velocity
reversal (23 channels against 0), and no observable reads the dispersion at high wavenumber.**

**The time sector (group G, G_068 - G_079).** The theory's unique time prediction is the surface redshift
**1 + z = exp(-x)** against GR's **(1 + 2x)^(-1/2)**, differing at **second order**. It is **decidable by one
measurement on one object** (G_072): the redshift of **J0740+6620** to **14.1422 % (3 sigma)** or **7.5524 %
(5 sigma)**, reached by a **next-generation timing** capability at **4.1374 sigma**, with a **direct surface-redshift
measurement** to 1 % at **44.8377 sigma**. **The leverage is the timing, not the mass** (the published row and the
radio-timing row both fail). And the clock law's **form** is decidable by the same programme (G_074): the logarithmic
candidate shifts the redshift by **+4.784E-002**, a third of the AT-vs-GR separation of **1.256E-001**.
**AND THE SURVIVING CONSTRAINT LIST DOES NOT FORCE THE LAW (G_075, REFUTED).** Assume only the **weak-field solar
redshift**, **GPS time dilation**, the **first-order agreement with GR** and the surviving **G_035 clock-only sector**,
with **no conformal-optics argument** and **no spatial-sector derivation**: what is forced is a **form and two
numbers** - g00 = -F(x) with F positive and monotone in the clock potential alone, **F(0) = 1** and **F'(0) = 2** -
and **nothing above the first order**, because **no realised measurement reaches it** (the solar row resolves **0**
orders beyond the first and so does the GPS row, so the solar redshift would have to improve by **~4 711x** in F''(0)
to see the AT-vs-GR difference of **4**). The **maximal family is F = 1 + 2x + x^2 G(x) with G arbitrary**, its
**J0740+6620 prediction is bounded below (1.142288) and NOT bounded above (2.266E+013)**, and **it contains GR's own
clock law** - so the surviving sector cannot separate the two theories the programme was built to decide between. The
part of **g00 = -exp(2x)** the surviving sector forces is **exactly the part AT shares with GR**, and uniqueness is
restorable only at a measured price: the single structural constraint **multiplicativity**, which admits exactly **1**
of the 9 laws.
**AND NO STRUCTURAL ROUTE FROM THE ALLOWED SOURCES SELECTS THE LAW EITHER (G_076, REFUTED - STRUCTURALLY
UNDERDETERMINED).** Of the **10 allowed sources** turned into functional equations - **composition of successive
redshifts**, **synchronization consistency**, **path independence**, **group properties**, **conservation laws**,
**locality**, the **actualization-density reading**, **multiplicativity** and **operational clock transport** - **3 are
NON-SELECTIVE** (locality, synchronization transitivity and count conservation each give **13 of 13 survivors at a
spread of 0.000E+000**, because the family is by construction a function of the potential alone), **1 is REFUTED** (the
actualization rule "all subsystems must advance" makes the rate the **minimum** of the parts while the occupancy
multiplies, contradicting the recorded rate by **6.046E-002**, while the **product** rule reproduces it exactly),
**5 are PARTIAL** and **1 SELECTS UNIQUELY - and that one is the CONJUNCTION of the composition law with the pinned
measurement**, so it selects by **assuming** the composition law. The **four rows that do any work have the SAME ZERO
SET** (the list is **one equation in four languages**), the composition law **alone** leaves **exp(kx)** with a free
constant, **approximate multiplicativity leaves an interval** (at 1e-3: **|c| ≤ 4.871E-003**), a **viable mesh
interpolant satisfies every mesh composition exactly** while differing between the points, and the **subgroup witness is
unbounded near the vacuum** (**F = 2.411E-031 at x = -3.589E-004**) so **regularity**, not the composition law, closes
it. **The cost from the infinite family to one law is THREE primitives - multiplicativity, regularity and a fixed map -
and none of the three is derived by any surviving result.**
**AND THE CLOCK HAS NO SOURCE OF ITS OWN EITHER (G_077, REFUTED): the source side of the same gap.** Starting only from
the surviving primitives, the **minimal object that changes a clock rate is ONE SCALAR** - the local occupancy: of the
sector's **6 temporal observables**, **5 take the occupancy** and **1 its derived potential**, and **0 take anything
else**. The **response ranks** are the sharpest measurement - the **clock rate reads 9 of 9 directions** while the
**acceleration reads 8 and is blind to the uniform one** - and the **redshift is a ratio of rates for an arbitrary
monotone law** (**1 + z = W(A_s)/W(A_o)**), which the audit derives **without any clock law**, the exponential being
**one member among others** in the corpus. The **decisive measurement is F-free**: the surviving source law makes the
acceleration the gradient of the **same** potential, so the **clock offset and the acceleration integral share their
zero set for every law** - while their **ratio**, the mean logarithmic derivative, is **not fixed**. The clock is
**manipulable, but only through the density** (measurably: the same perturbation moves the rate and the acceleration),
**count conservation makes the source a transport**, and the **boundary question splits in three**: a clock offset with
**no LOCAL acceleration is permitted** (the shell measures it), one with **no observable redshift is permitted only if
the total count may change - which count conservation forbids** - and one with **no acceleration on any path is
refuted**. **So time control is not impossible; it is not separate: time engineering is gravity engineering.**
**AND THE SHARED SOURCE CAN BE MOVED BUT NOT MADE (G_078, REFUTED - and the audit refutes its own headline).**
Starting only from **actualization, density transport, count conservation and causal structure** (new fields,
imported matter sectors and imported field equations excluded): **NO SOURCE TERM IS SUPPLIED**, because every natural
operator of the update class is a **DIVERGENCE** and leaks **0.000E+000** of the count on a closed ring while a
deliberately non-divergent **control** leaks **8.166E+000** - so the detector works and the class contains no source,
which makes a source term an **import**: **absent, not excluded**. **THE UPLIFT CAP IS ROBUST AND THE CONTRAST CAP IS
NOT.** Positivity caps concentration at the total, so on the canonical lattice the maximum uplift is **1.5214** in the
potential, **4.5789** in the rate and **20.9659** in the metric magnitude if everything is concentrated, against
**1.5249 / 4.5947 / 21.1113** for a **one-unit floor plus surplus** - the two readings agree to **2.270E-003
relative**, so the cap is a property of positivity rather than of the reading - and the uplift is **logarithmic** in
the count, each **e^3 = 20.09x** of count adding exactly **1.0** (so enhancement is expensive: **20 000x** the count
per unit of potential). **But the asymmetry the audit first claimed does not survive**: under a **unit floor** the
maximum contrast between a well and a hill of the **same count** is **4.5947 - the same number as the uplift cap - so
the uplift/depression asymmetry VANISHES**, and it exists **only if an EMPTY CELL is admitted**, where the contrast is
**unbounded** (**1.260E+002** at a well occupancy of **1.000E-006** with the total held fixed) and the reason is
**A = (1/d) ln rho diverging rather than anything the count does**. **A BOUNDED REGION CAN NEVER CHANGE ALONE**: the
change inside a region **equals minus its net boundary flux to 4.857E-017 over 8 cells with 0 exceptions**, so the
compensation is **compulsory** and only its **location** is free. **AND THE TWO SIGNATURES DO NOT DECOUPLE**: every
**gravitational** manipulation carries a clock signature - **9 clock laws x 4 depths, 0 counterexamples**, because an
acceleration change needs a **gradient of the occupancy** and the clock is a **strictly monotone readout** of it - and
every **clock** manipulation carries a gravitational signature too, with **exactly one exception**, the **UNIFORM**
direction, which moves every clock by **6.623E-003** and the acceleration by **0.000E+000** while changing the **total
count by 1.920E+000** - and **count conservation forbids exactly that move**. **The reachable clock contrast is a
factor of a few under a floor, and the one channel that would have separated time from gravity is the channel the
theory closes.**
**AND THE CONTINUITY LAW IS DERIVED ON THE SUBSTRATE, WITH ONE PREMISE IT CANNOT SUPPLY FOR ITSELF (G_079, BOUNDARY).**
Asked whether **∂tρ + ∇·J = 0** is forced, the audit proves it for the surviving class **in exact integer
arithmetic**: every conserving operator **is** a divergence, with its flux written down (`J_i = ρ[i]`,
`2J_i = ρ[i+1] + ρ[i]`, `J_i = ρ[i+1] − ρ[i]`), reconstructed by prefix sums and verified at **residual 0**, while
the non-divergent **control fails** the membership test - so the criterion is **live** rather than vacuous. **The flux
is a gauge:** `rank = n − 1` and **nullity 1 on every ring size** (4, 8, 16, 48, 96), so the flux is fixed only up to
**one circulation** - and that circulation is **unobservable**, since **0 of 6** temporal observables read a flux.
**But conservation does NOT imply continuity, and the counterexample is a cut:** on a connected ring the zero-sum
subspace **is** the image (**8 of 8** conserving witnesses are transports); on a substrate of c components the image
requires **each component's sum** to vanish, so a **transfer across the cut** conserves the total and **is not any
flux** - a **source/sink pair** (**2 of 10** witnesses on two rings, **4 of 12** on four). So the equivalence rests on
**connectivity**, and the audit states plainly that it does **not** derive that premise. **A source could be added
without breaking a single surviving measurement** - the pinned rows constrain the **map from state to observables**
(**9 of 9** laws carry the pinned slope) and not the dynamics - and the sector supplies a **detector** rather than a
prohibition: the **ledger residual equals the source exactly** (**0.030** for a source of `0.03`), so the identity is
**falsifiable**. **The observables need one mode the flux cannot supply:** the kernel of the divergence is the
**uniform mode**, so ratios and differences (the **redshift** and the **acceleration**) are determined and the
**absolute clock rate is not** - and the **conserved total** fixes exactly that mode on a connected substrate (**0**
free modes) while a **disconnected** one leaves **c − 1 relative levels** free that no measurement can reach. **So a
source-free conserved theory does limit manipulation to redistribution - conditionally.** The open question handed on:
**is connectivity a consequence of the theory or an input to it?**

**The two current open questions this leaves.** (1) **Does any observable read the lattice dispersion inside the
band?** If one does, the fold's position becomes observable and QM_011's verdict becomes conditional - it is stated
as conditional rather than as a theorem. (2) **Can the clock law's map be derived rather than assumed?** G_073, G_074
and G_075 all find that the form is fixed only up to the density-to-potential map, which the surviving audits take as
an input: the metric's exponential and the rate's power are **the same statement**, G_075 measures the consequence
(the surviving constraints force only the first order, and the family that remains contains GR), and the open question
is therefore sharper than before: **the answer cannot come from the weak field at all**, so it must come either from a
structural principle (**multiplicativity**, if the clock composition can be derived rather than assumed) or from the
**compact-object measurement**, the only surviving row that reaches the second order. **G_076 closes the structural
half of that question as far as the allowed sources go:** every source that is not the composition law is either
satisfied by everything, refuted, or the same equation in another language, so **the clock sector is structurally
underdetermined and the one constraint that selects presupposes the composition law it would have to derive**. The
sector's own next question is therefore now sharply stated: **why does the clock composition multiply?** **G_078 adds a
second, blunter question to that list: MAY A CELL BE EMPTY?** Every bound the new audit derives is **conditional** on
it - under a unit floor the uplift and the contrast are capped by the **same** logarithmic number, and the asymmetry
between them exists **only** if an empty cell is admissible - and **no surviving datum probes that regime**, so the
bounds are structural statements rather than measured predictions. **G_079 adds a third: IS CONNECTIVITY A CONSEQUENCE
OF THE THEORY OR AN INPUT TO IT? - AND G_080 ANSWERS IT: AN INPUT.** The continuity law's equivalence to count
conservation is **measured** to rest on it - on a connected ring **8 of 8** conserving witnesses are transports, while
on two components **2 of 10 are not** (a transfer across a cut is a **source/sink pair**) - so G_079 derives the law
**on the canonical substrate** and explicitly does **not** derive the premise. G_080 then answers the origin question
by measurement, under a licence that allows **Difference, Actualization, the generation relation and count
conservation** and forbids a **graph-connectivity axiom** and an **imported topology**: every substrate is **built**
from an explicit link set and its component count **computed** from it. **NOT DERIVED - THE FOUR ALLOWED PRIMITIVES
ARE BLIND TO THE CUT:** adjoin a **disjoint copy** of a substrate to itself and **0 of 4** change the content of
their certificate (the same differences `{-5,-3,1,2,3,4}`, ticks `{-6,-3,0,5,6,7}`, in-degrees `{0,1}`, component
sums `{47}` with leak 0), while **every** global descriptor moves (components **1 -> 2**, transport rank **7 -> 14**,
roots **1 -> 2**) - because **three of the four are LOCAL (radius one link) and the fourth is a SUM, and a sum cannot
see which component its terms came from**. **NOT EMERGENT - THE UPDATE CLASS CONSERVES THE COMPONENT COUNT INSTEAD OF
PRODUCING IT:** the class is written on the link set, so its Laplacian is **block diagonal** across a cut
(**0.000E+000** weight between components against a **live control** of **4** entries joining the two halves of the
connected ring, a partition no link set can influence), and the pair that is a **link** on the 8-cell ring and a
**cut** on two 4-cell rings has influence **1 against 0**. **The generation relation does not repair it** - one root
can generate **both** components (**1** generation component against **2** transport components) - **and the generative
rule presupposes the ring**: `(i + d) mod n` never tests a link because it computes one, so on the smallest split pair
it **manufactures the very link** whose absence froze the pair (**1** crossing link, returning **1** component where
the substrate has **2**). **THE SMALLEST DISCONNECTED SURVIVOR IS TWO CELLS AND IT IS FROZEN:** rank **0**, no
non-zero transport, the one non-trivial count-conserving change it admits is the **forbidden** transfer, while the
same two cells **connected** admit **1** direction; transport capacity is `n - c` throughout, so every cut costs
exactly one direction, and the smallest cut of the canonical substrate is **2 links or 2 cells** - **a single defect
can never cut it**. **THE SIGNATURE IS A FROZEN, UNREADABLE OFFSET:** the per-component-uniform state is an **exact
fixed point** (max update 0) with a potential gap of **0.270**; the smallest survivor has **no acceleration observable
at all** (a gradient along links, and it has none); and the **redshift between components is defined and moves** with
the free level (**-0.370 to +0.310**) while being **UNREACHABLE** - **0 links across the cut**, infinite path length,
**0 of 6** observables reading a flux. **So the COMPONENT COUNT is a conserved integer the theory never sets, and
G_080 adds a fourth question: WHAT SETS THE COMPONENT COUNT?**


