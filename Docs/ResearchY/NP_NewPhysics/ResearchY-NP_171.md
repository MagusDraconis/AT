# ResearchY-NP_171 — Deterministic Lock-Lattice Simulator

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_171 (permanent)
**Title:** Deterministic Lock-Lattice Simulator — the C₉₆(±1..±6) lock threshold, predicted before any hardware exists
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `NP_NewPhysics/ResearchY-NP_171.md`
**Depends on:** ResearchY-NP_170 (the hardware proposal this simulates — H1 criteria, §6 R3 protocol, §7 statistics),
ResearchY-NP_169 (the ring's gap is an imported kinematic finite-size gap),
ResearchY-NP_005 (the locking nonlinearity is ABSENT — imported BOUNDARY), ResearchY-T_014 (near-gap counting:
only 2 modes / 1 distinct eigenvalue within 2λ₂), ResearchY-T_015 (robustness is degeneracy-controlled; the
degeneracy-matched control), ResearchY-D_015/D_020 (C₉₆(±1..±6), the 3-family window), QG313 (lock structure is
universal, lock values are domain-specific), QG316 (criticality criteria — sharpness, width, ramps), QG319
(lock-classifier false-positive rate), QG120 (one-way barrier), QG126 (phase interference)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_171_Tests.cs` (4 tests, all passing)
**Simulator:** `AT.Core/Resonance/Kuramoto/LockLatticeSimulator.cs`

---

## Question

NP_170 pre-registers a hardware experiment whose pass/fail turns on **H1**: *does a lock threshold exist in the
physical coupling?* Criteria, taken verbatim from QG316 — sharpness ≥ 3 **and** width ≤ 0.4, with a hysteresis
loop **A > 0 at ≥ 5σ**. No hardware exists.

This audit asks the prior question:

> What does the deterministic Adler/Kuramoto evolution of the canonical C₉₆(±1..±6) ring predict for
> **g_c, sharpness, width and hysteresis** — with no noise, no fitting, and fixed seeds?

The answer is a **model prediction**, not a measurement, and it is falsifiable by the Tier-1 board. It is also
the cheapest possible test of H1: a simulator can already tell the hardware team whether the threshold they are
funding is sharp, where it sits, and — decisively — **whether the noiseless dynamics can produce the
pre-registered hysteresis at all.**

---

## Method — the model, stated before any number

### The lattice

The canonical circulant **C₉₆(±1..±6)**, degree 12, exactly as in NP_170 §3:

```
λ_k = 2 · Σ_{d=1..6} ( 1 − cos( 2π d k / 96 ) )        k = 0 … 95
```

Taken from the **closed form**, not an eigensolver, so that mirror pairs are degenerate to machine precision and
the multiplicity pattern is exact. Node *i* carries eigenmode *i*, so ω_i = √λ_i: the mirror pairing
λ_k = λ_{96−k} becomes a **spatial reflection of the ring**, and the zero mode k = 0 is the reference node. This
is the canonical arrangement; a second arrangement (the same multiset sorted ascending, a monotone frequency
gradient) is run as an arrangement-sensitivity variant.

### The dynamics

```
dθ_i/dτ = ω_i + g · (1/deg) · Σ_{j∈N(i)} sin( θ_j − θ_i )
```

- **Adler/Kuramoto** phase dynamics. The nonlinearity is **IMPORTED** (NP_005: the locking force is ABSENT from
  the canonical chain), and the coupling network is **engineered** (NP_007/NP_011: the static coupling network is
  REFUTED as a physical field). This audit therefore tests the lock *structure*, never a derived interaction.
- **No stochastic term.** The dynamics are a deterministic ODE. Fixed-seed LCG streams set the initial phases
  only. 30 enumerated seeds = the protocol's ≥ 30 repetitions; every seed is reported.
- Frequencies normalized by ω_max; g is the degree-normalized lock strength. **RK4**, dt = 0.1 (≈ 63 steps per
  period of the fastest mode), verified against dt = 0.05 in the suite rather than assumed.
- The phase sum is evaluated in angle-addition form (algebraically exact, 2 trigonometric calls per node).

### The lock criterion (NP_170 R3, with the T_014 refinement)

The reference is the collective phase Ψ(τ) = arg Σ_j e^{iθ_j(τ)}, tracked continuously (unwrapped). A node is
**locked** at coupling g when its mean slip over the measurement window satisfies

```
| d(θ_i − Ψ)/dτ |  <  ε
```

Two ε rules are reported, as the protocol demands: the primary rule **ε = 0.01·ω₁** (one common threshold, the
fundamental doublet's frequency, all 96 nodes) and the per-mode refinement **ε_i = 0.01·ω_i** (T_014), which is
undefined for the zero mode and therefore covers the 95 positive modes.

### The sweep and the metrics

Sweep g **upward** over the pre-registered range, then **downward**, carrying the state between steps — that
continuation is the only thing that can make A ≠ 0. Metrics verbatim from NP_170 §6 R3:

```
f(g)   = fraction of nodes with mean slip < ε
A      = ∫ | f_up(g) − f_down(g) | dg  / (g_max − g_min)      (hysteresis area, normalized)
sharp  = max|Δf| / mean|Δf|                                    (must be ≥ 3)
width  = g₉₀ − g₁₀                                             (must be ≤ 0.4)
H1     = sharp ≥ 3  AND  width ≤ 0.4  AND  A > 0 at ≥ 5σ
```

**Range:** g ∈ [0, 3.0], 40 steps each way (Δg = 0.075), coupling in units of the fastest mode's frequency
(ω_max = 1). The range was fixed after a 3-seed pilot at g_max = 4 located the transition between g ≈ 1.4 and
g ≈ 2.1. NP_170 §5.1 normalizes the *hardware* coupling so that g = 1 is the board's maximum, so **f(g = 1) is
reported separately** — that is the number the board can actually reach.

### The controls (NP_170 §6 R6 / §7 — all must fail H1)

| Control | Construction |
|---|---|
| (a) detuned | the D96 ring with frequencies shifted out of ratio, ω_i(1 + 0.5·i/95) |
| (b) random-coupling | D96 frequencies on a deterministic random 12-regular coupling (LCG seed 20260911) |
| (c) linear-ramp | the D96 ring with a linear-ramp spectrum λ_i = 12·i/95 (no crowding, no degeneracy) |
| (d) degeneracy-matched | the D96 multiplicity pattern {1, 42×2, 5, 6} carried by non-D96 values (T_015 requirement) |

---

## Results

### 1. Canonical basis, reproduced independently

| Quantity | This audit | Canonical |
|---|---|---|
| λ₂ (k = 1) | 0.386350893377790 | 0.386350893377790 ✅ |
| ω₁ = √λ₂ | 0.621571309969974 | 0.621571309969974 ✅ |
| ω_max = √λ_max | 3.979619638484919 | 3.979620 ✅ |
| span = ω_max/ω₁ | 6.402515 | 6.4025 ✅ |
| mirror pairs λ_k = λ_{96−k} | 47 (+ self-conjugate k = 48) | 47 ✅ |
| zero modes | 1 | 1 ✅ |
| multiplicity levels | 45 = 1 + 42×2 + 5 + 6 (96 modes) | [42×2, 5, 6] ✅ |
| near-gap doublet | nodes {1, 95}, ω₁ = 0.15618862 (normalized) | the k = ±1 pair ✅ |

**Corrigendum (labels only, no values).** NP_170 §3 records ω_max as "√λ_max (**k = 48, λ = 12**)". The numeric
value 3.979620 is correct, but the parenthetical is not: λ₄₈ = 12 exactly, while the spectrum maximum is
**λ_max = 15.837372467** attained at **k = 11 and k = 85** (itself a mirror pair). Nothing downstream changes —
ω_max, span and the bands are all unaffected — but the fastest mode is the 11/85 pair, not k = 48.

### 2. Determinism and integrator adequacy

- Same seed run twice → **bit-identical** f_up curves and identical A. Different seed → different curve.
- dt = 0.1 versus dt = 0.05: max |Δf_up| = 0.0000 at 16 steps (0.0000 of the lock fraction), and the transition
  location is stable. The bifurcation moves by less than one sweep step; the curve is integrator-converged.
- At g = 0 essentially nothing is locked (f < 0.05); at the top of the sweep the ring is fully locked.

### 3. H1 — the predicted numbers

| Lattice | g_c | sharpness | width | A | A/σ_A | f(g=1) | H1 |
|---|---|---|---|---|---|---|---|
| **D96 (canonical)** | **1.607 ± 0.030** | **23.69 ± 5.28** | **0.132 ± 0.083** | **0.0251 ± 0.0074** | **3.4** | **0.0000** | **fail** |
| D96-ranked (gradient) | 1.746 | 29.76 | 0.102 | 0.0148 | 3.9 | 0.0000 | fail |
| detuned (a) | 1.986 | 32.41 | 0.092 | 0.0342 | 2.5 | 0.0000 | fail |
| random-coupling (b) | 0.863 | 40.00 | 0.060 | 0.0000 | 0.0 | **1.0000** | fail |
| linear-ramp (c) | — | — | — | 0.0004 | 1.6 | 0.0000 | fail (no transition ≤ 3) |
| deg-matched (d) | — | — | — | 0.0035 | 1.7 | 0.0000 | fail (no transition ≤ 3) |

**Criterion-by-criterion (D96, 30 seeds):**

| H1 sub-criterion | Value | Threshold | Verdict |
|---|---|---|---|
| sharpness | 23.69 ± 5.28 | ≥ 3 | **PASS** |
| width | 0.132 ± 0.083 | ≤ 0.4 | **PASS** |
| hysteresis significance | A = 0.0251 ± 0.0074, A/σ = 3.38 | > 0 at ≥ 5σ | **FAIL** |

Two of the three pre-registered criteria are met **decisively** — the model's lock transition is sharp
(≈ 8× the required sharpness) and narrow (≈ 3× inside the required width). The hysteresis sub-criterion is
**not** met: a real loop exists (A ≠ 0, and it is visible directly in the curves — at g = 1.6 the upward sweep
gives f = 0.22 while the downward sweep still holds f = 0.62), but its seed-to-seed scatter puts it at 3.4σ,
below the pre-registered 5σ.

### 4. The controls

All four controls fail H1, as §7 requires — but they fail for **different reasons**, and the differences are the
interesting part:

- **random-coupling (b)** locks *earliest* (g_c = 0.863) and is the **only** lattice that is fully locked at the
  board's nominal maximum (f(1) = 1.000); its hysteresis is **exactly zero** (A = 0.0000). A randomly coupled
  12-regular graph is easier to lock than the canonical ring and shows no path dependence at all.
- **detuned (a)** locks *latest* of the in-range lattices (g_c = 1.986) and has the **largest** hysteresis area
  (A = 0.0342) — larger than D96's — but at only 2.5σ.
- **linear-ramp (c)** and **degeneracy-matched (d)** do **not** lock inside the pre-registered range at all
  (f(3) = 0.010 and 0.167). The degeneracy pattern alone therefore does **not** reproduce the D96 threshold: the
  values matter, not just the multiplicity class. This is a direct model-side analogue of the T_015 lesson, and
  it also means the deg-matched control fails H1 by *not having a transition in range* — a much blunter failure
  than a weak hysteresis.
- **Arrangement sensitivity:** the canonical mode-index arrangement (g_c = 1.607) and the monotone frequency
  gradient with the identical multiset (g_c = 1.746) differ by 9%, with the gradient locking later and showing
  less hysteresis. The threshold is a property of the *arrangement*, not only of the spectrum.

**No superiority claim.** The canonical D96 lattice does **not** lock more easily than the random-coupling
control — it needs ≈ 1.9× the coupling — and it does not have the largest hysteresis area. Consistent with T_015,
D96 is chosen for derivability, not for lock performance.

### 5. Mode-specific locking (T_014)

Onset g averaged over 12 seeds, grouped by the degeneracy class of each node's frequency level:

| class | nodes | mean ω | mean onset g |
|---|---|---|---|
| mult 1 (zero mode) | 1 | 0.0000 | 1.688 |
| mult 2 | 84 | 0.8535 | 1.643 |
| mult 5 | 5 | 0.8705 | 1.635 |
| mult 6 | 6 | 0.9402 | 1.645 |

- The **near-gap doublet {1, 95}** locks at g = 1.688, i.e. *after* the global half-lock point (g_c = 1.607):
  the fundamental — the only pair within 2λ₂ (T_014) — is **not** the first to lock. Its detuning from the
  collective frequency (ω₁ = 0.156 versus Ω = 0.851) is among the largest in the lattice, and in Adler theory
  the largest-detuning oscillator is the last to be captured. The near-gap doublet locks ≈ 5% later than the
  global transition, so a **near-gap-only** lock and a **global** lock are not the same event — which is exactly
  why T_014 demanded both be reported separately.
- The zero mode (ω = 0) locks at the same mean onset as the near-gap doublet (1.688) — it is equally far from Ω.
- The fastest modes {11, 85} (ω = 1) lock at 1.650.
- Spread across all classes is small (1.635–1.688), so the transition is a **collective** event rather than a
  sequence of isolated mode captures.

### 6. The hardware-relevant output

```
f(g = 1)  =  0.0000       (the board's nominal maximum coupling locks NOTHING)
g_c       =  1.607 ± 0.030        →  1.61 × the nominal maximum
K ≥ g_c · ω_max = 6.394 rad/time  →  1.61·ω_max  =  10.29·ω₁  =  15.17·⟨|ω_i − Ω|⟩
```

So the prediction the board must be designed against is: **the per-node lock term must reach ≈ 10.3× the
fundamental frequency ω₁ (equivalently 1.61×ω_max ≈ 15× the mean detuning) before anything locks.** A Tier-1
board whose coupling tops out at the nominal normalization (g = 1) will measure f(g) ≡ 0 across its whole
ramp — not because the lock law is absent, but because the drive is ~1.6× too weak. In units of the ring's own
frequencies (the only transferable form, QG313) that is the concrete dimensioning requirement.

---

## Classification

| Item | Classification |
|---|---|
| The canonical D96 spectrum, mirror pairing, multiplicity structure | **DERIVED** (closed form; restated, not new) |
| The metric definitions (f, A, sharpness, width) and the H1 criteria | **BOUNDARY** (QG316 pre-registration, imported as the test's specification) |
| Adler/Kuramoto dynamics and the coupling network | **IMPORTED / BOUNDARY** (NP_005, NP_007/NP_011) |
| Existence of a sharp, narrow lock threshold in the model | **EMERGENT** (a property of the imported model — standard synchronization physics, not new AT content) |
| The value g_c = 1.607 | **EMERGENT and domain-specific** (QG313). It must **not** be compared with QG316's synthetic-cohort g* ≈ 0.31, and it is **not** a hardware measurement |
| The hysteresis verdict (3.4σ < 5σ) | **EMERGENT** (model outcome; the pre-registered criterion is not satisfied) |
| The threshold's location relative to the board's nominal range (f(1) = 0; K ≥ 10.3ω₁) | **MODEL PREDICTION, UNTESTED** — a falsifiable statement about hardware, not a result of it |
| "AT predicts a physical phase transition" | **STILL OPEN** — this audit cannot confirm it; only the board can, and NP_170 §8's kill criteria then apply |
| D96 spectral/lock superiority over the controls | **REFUTED** (again, consistently with T_015) |

**No canonical AT claim, value, equation or registry entry is changed.** No reclassification of any prior
result: the D_040 ClassificationRegistry is untouched, and D_028's span/family-window classification is
unaffected. No new primitive. The energy-storage framing remains abandoned (NP_170 §1).

---

## Verdict

1. **H1 is predicted to fail — for exactly one reason.** The noiseless deterministic D96 ring produces a lock
   transition that is **sharp (23.7 ≥ 3) and narrow (width 0.132 ≤ 0.4)**, but its hysteresis loop, while real,
   reaches only **3.4σ < 5σ**. The pre-registered H1 cannot be satisfied by the ideal model alone: the board must
   supply bistability the noiseless dynamics does not produce at the required significance.
2. **The threshold is not where the board can reach.** g_c = 1.607 ± 0.030, with f(1) = 0: at the nominal
   maximum coupling nothing locks. The design requirement is K ≥ 6.394 rad/time ≈ 10.3·ω₁ ≈ 15·⟨|ω_i − Ω|⟩.
3. **The threshold is not specific to D96.** All four §6 controls fail H1 as required, but the random-coupling
   lattice locks *earlier* (0.863) and the D96 lattice needs ≈ 1.9× more coupling than it does. The
   degeneracy-matched control locks later still (no transition ≤ 3), so the multiplicity pattern alone does not
   reproduce D96 — the values matter. **H1's existence test is therefore not a discriminator between the lock
   lattice and its controls**, exactly as T_015 warned; the discriminating test must be R4 (retention against
   the degeneracy-matched control), which this simulator does not model.
4. **A label correction, no value changes:** ω_max = √λ_max with λ_max = 15.837372 at **k = 11/85**, not the
   "k = 48, λ = 12" recorded in NP_170 §3.
5. **The prediction is falsifiable and cheap to check.** If the Tier-1 board shows a sharp transition near
   g_c ≈ 1.6 in these normalized units, the model's lock picture is corroborated; if it shows a *significant*
   hysteresis loop that the noiseless model cannot produce, then the imported nonlinearity is doing more work
   than the lock structure — which is itself informative. If it shows nothing at g ≤ 1, this audit has explained
   why before the board was built.

---

## References

- `NP_NewPhysics/ResearchY-NP_170.md` — the hardware proposal this simulates (H1, §5.1 normalization, §6 R3
  protocol, §7 statistics, §8 kill criteria, §9 boundaries)
- `NP_NewPhysics/ResearchY-NP_169.md` — the ring's gap is an imported kinematic finite-size gap
- `NP_NewPhysics/ResearchY-NP_005.md` — the locking force is ABSENT (imported nonlinearity)
- `T_SpectralBlueprint/ResearchY-T_014.md` — near-gap counting: 2 modes / 1 distinct within 2λ₂
- `T_SpectralBlueprint/ResearchY-T_015.md` — robustness is degeneracy-controlled; D96 is not superior
- `D_ResonanceStructure/ResearchY-D_015.md`, `ResearchY-D_020.md` — N = 96, C₉₆(±1..±6), the 3-family window
- QG313 (lock values are domain-specific), QG316 (criticality criteria), QG319 (classifier FP/FN), QG120
  (one-way barrier), QG126 (phase interference)
- Simulator: `AT.Core/Resonance/Kuramoto/LockLatticeSimulator.cs`
- Test suite: `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_171_Tests.cs`

---

**Status:** COMPLETE (model prediction; no hardware exists). No canonical AT statement modified; no
reclassification; no new primitive.
