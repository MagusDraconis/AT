# ResearchY-G_002 — Density Control Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_002 (permanent)
**Title:** Density Control Audit — can the actualization density change independently of mass-energy?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_002.md`
**Depends on:** ResearchY-G_001 (the source is the counting measure rho), D_047 (lock theorem, degeneracy =
free room, lock release 0.80231), D_048 (latent fraction L = (N − A₀)/N = 0.53125, random is an exact
null), D_051–D_058 (near-gap / ring family structure), T_010/T_012/T_014 (survivor counts and
per-mode ε_j), NP_037/NP_088 (D96³ tensor spectrum), NP_171 (arrangement sensitivity: canonical
g_c = 1.607 vs sorted 1.746); AT-QG QG89 (energy = actualization rate), QG194 (count conservation,
Σm = 0; matter = deficit), QG195/196 (deficit dust), QG197 (metric g = ρ^(2/d)η), QG206 (α = 0),
QG220 (phase θ = 2πk/N), QG228 (ρ is the information/content measure); G4-O3/G4-O4/G4-O5 (native
acceleration, ρ as conformal factor), G4-G2 (R = F(ρ)), G4-RHO00-02 (dynamical origin of ρ: OPEN),
G4-ME0 (matter = deficit)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_002_Tests.cs` (9/9 PASSED, 155 ms)
**Shared machinery:** `AT.Tests/Shared/DensityField.cs` (also used by G_001)

---

## Purpose

G_001 established that gravity in AT is sourced by the **actualization density ρ** (the counting
measure) and by nothing else that is independent of it. The immediate follow-up is a *control*
question:

> **Can ρ change independently of mass-energy?**

If ρ's profile were rigidly tied to the energy, the G_001 source term would carry no independent
degree of freedom and AT's gravity would be a single-parameter (energy → field) theory. If ρ's
profile has energy-free directions, then AT's gravity has a **genuinely independent variable**, and
whatever fixes it must be a dynamics (not a conservation law).

**Candidates** (each an operation on ρ): (1) spectral organization, (2) phase coherence,
(3) degeneracy structure, (4) survivor compression, (5) D96 vs random, (6) D96³ vs D96.

**Measures per operation:** ρ (the density), R(ρ) (the conformal scalar curvature), a(ρ) (the native
conformal acceleration), and ΔE (energy).

---

## 0. Model and definitions

| Object | Definition |
|---|---|
| **ρ** | the counting measure on a cell lattice, one cell per spectral mode; reference `ρ̄ = 1/N` |
| **energy** | the actualization rate (QG89): `E = Σρ` |
| **deficit mass** | `m = ρ̄ − ρ`; count conservation (QG194) gives **`Σm = 0` exactly** for the normalized measure |
| **a(ρ)** | `a = −(1/d)·∇ln ρ` (G4-O3), `d = 3`, evaluated by central differences |
| **R(ρ)** | `R = −2(d−1)ρ^(−2/d)[σ″ + ((d−2)/2)(σ′)²]`, `σ = (1/d)ln ρ` (G4-G2/QG197) |

**Labels.** **CONTROLLABLE** = an operation with `ΔE = 0` and `Δρ ≠ 0`; **CORRELATED** = `Δρ ≠ 0` only
together with `ΔE ≠ 0`; **REFUTED** = the operation leaves ρ, R and a unchanged.

**Key structural fact (the reason the question has a positive answer).** Because count conservation
makes the **total deficit vanish identically** (`Σm = 0`), no rearrangement of ρ can change the total
mass-energy. "Fixed total mass-energy" is therefore the *generic* situation, not a special one — and
the question becomes: which operations move ρ *within* that constraint?

**Baseline (canonical measure).** Spreading each eigenspace's occupancy share uniformly over its
multiplet cells gives a **uniform** ρ ≡ 1/N, i.e. the **exact zero-field configuration**:
`max|a| = 0` and `max|R| = 0` to 12 decimal places.

---

## 1. Real lattice data (shared catalog; deterministic)

| Lattice | modes N | eigenspaces A₀ | L = (N − A₀)/N | multiplicity pattern | lock release (D_047) |
|---|---|---|---|---|---|
| **D96** (C₉₆(±1..±6)) | 96 | **45** | **0.53125** | 42×2, 5, 6, 1 | **0.80231** |
| **random** (sparse p = 0.3, seed 42) | 96 | **96** | **0** | all singletons | 0 |
| **D96³** (tensor cube) | 884 736 | **20 812** | **0.97648** | max multiplicity 562 | — |

The D96 rows reproduce the canonical D_048 values (A₀ = 45, L = 0.53125) and the D_047 lock release
(0.80231) exactly.

---

## 2. Operations and measurements

| # | Operation | ΔE/E | L1(Δρ) | max\|Δa\| | max\|ΔR\| | Verdict |
|---|---|---|---|---|---|---|
| 1 | **arrangement** (same multiset, different order) | **0.0000** | 0.7583 | 0.685714 | 53.82 | **CONTROLLABLE** |
| 2 | **phase** (global shift + mirror-pair flip) | 0.0000 | **0.0000** | **0.000000** | **0.0000** | **REFUTED** |
| 3 | **degeneracy redistribution** (inside multiplets) | **0.0000** | 0.6667 | **0.603175** | 53.82 | **CONTROLLABLE** |
| 4a | **survivor compression**, total fixed (k = 48) | **0.0000** | 2.8670 | 0.032121 | 0.0140 | **CONTROLLABLE** |
| 4a | **survivor compression**, total fixed (k = 12) | **0.0000** | 5.1569 | 0.032121 | 0.0140 | **CONTROLLABLE** |
| 4b | **survivor deletion**, total NOT fixed | **−46.81%** | — | — | — | **CORRELATED** |
| 5 | **D96 vs random** (same N, same total) | **0.0000** | 0.5313 | 0.333333 | 9.2448 | **CONTROLLABLE** |
| 6 | **D96³ vs D96** (same normalized total) | **0.0000** | 0.9990 | 0.276596 | 9057.60 | **CONTROLLABLE** |
| c | **rescaling** ρ → 3.7ρ (control) | **+270%** | 2.7000 | **0.000000** | −31.32 | **CORRELATED** |

---

## 3. Candidate-by-candidate

### 1. Spectral organization — **CONTROLLABLE**

The same occupancy **multiset** in a different **order** leaves `E` fixed exactly while ρ and both
reads move; the sharpest witness is a monotone density and its reversal, which give `a` of **opposite
sign at every interior probe** (`a(24.5) < 0` vs `> 0`, likewise at 48.5 and 72.5) at identical total
occupancy. The field is an *arrangement*, not an *amount*.

This is the density-space analogue of **NP_171**, where the canonical D96 mode-index arrangement
(g_c = 1.607) and the identical multiset sorted ascending (g_c = 1.746) differ by 9% — independent
corroboration in a completely different dynamical model (Adler/Kuramoto).

### 2. Phase coherence — **REFUTED**

`ρ = |ψ|²` is **phase-blind**: recovering the density from the complex state discards the phase
exactly. Under a global phase shift (`θ → θ + 1.234`) and under a mirror-pair relative-phase flip
(`θ → −θ`), ρ is unchanged to 12 decimal places (L1 = 0.0000) and `a` and `R` move only at the
floating-point floor of the derivative estimate (< 1e-9 and < 1e-6).

The phase is nevertheless a **real observable** — the two-mode interference term `2 + 2cos(Δθ)` still
runs 4 → 2 → 0 — so this is not "phase is meaningless"; it is "phase does not touch ρ, R or a". Phase
coherence is therefore **refuted as a density control**.

### 3. Degeneracy structure — **CONTROLLABLE** (and this is the mechanism)

A redistribution **inside** degenerate multiplets (80% of each multiplet's share on its first cell)
leaves every **per-multiplet total** identical to 12 decimal places — hence the spectrum, all mode
energies and every spectral observable are untouched — at `ΔE = 0` exactly, and takes the field from
**exactly zero** to `max|a| = 0.603175`.

The free room is *exactly* the degeneracy structure:

```
independent energy-free directions = Σ (m_i − 1) = N − A₀
D96:     51 / 96  = 0.53125   (= the D_048 latent fraction L exactly)
random:   0 / 96  = 0         (no free room at all)
D96³:  863 924 / 884 736 = 0.97648
```

**Cross-link (new).** The D_048 *latent fraction* `L = (N − A₀)/N` is, in the density picture,
**precisely the fraction of energy-free reconfiguration directions of the counting measure** — the
adaptability ceiling of D_048 and the free room of ρ are the same number. The D_047 lock release
(0.80231) is the entropy released when those directions are fully used.

### 4. Survivor compression — **CONTROLLABLE** (total fixed) / **CORRELATED** (total not fixed)

Compaction at fixed total (keep the *k* largest occupancy cells, replace the rest by their mean)
changes ρ by L1 up to **5.16** with `ΔE = 0` exactly, and moves both reads. The **same deletion
without fixing the total** lowers E by **46.8%** (k = 48), 73.0% (24) and 86.4% (12) — then ρ and the
energy move **together**, which is exactly the CORRELATED case. Whether "survivor compression" is a
density control or an energy change depends entirely on whether the count is restored.

### 5. D96 vs random — **CONTROLLABLE**

At the same mode count and the same total, the degeneracy-free lattice is an **exact zero-field
configuration** (uniform ρ ⇒ a = 0 and R = 0 to 12 dp — the D_048 "random is an exact null" fact,
seen geometrically), while D96 is not (`max|a| = 0.333`, `max|R| = 9.24`). Same energy, different
field: the lattice choice controls ρ.

### 6. D96³ vs D96 — **CONTROLLABLE**

The tensor cube has 884 736 modes in 20 812 eigenspaces (max multiplicity 562) versus D96's 96 modes
in 45 eigenspaces (max multiplicity 6). **97.6%** of its directions are energy-free against D96's
**53.1%**, and at the same normalized total it produces a *different* field (`Δ max|a| = 0.277`,
`Δ max|R| = 9057.6`). The cube is strictly more reconfigurable at fixed energy.

### Control. Global rescaling — **CORRELATED**

`ρ → λρ` moves E by exactly λ (×3.7 here) while `a` is **exactly invariant** (scale invariance,
G_001) and `R` merely rescales by `λ^(−2/d)` (exact to 6 decimal places). Energy moves with the
density; the *field* does not. This is not a density control — it is a units choice — and it is the
clean CORRELATED case.

---

## 4. Verdicts

| # | Candidate | Verdict |
|---|---|---|
| 1 | spectral organization | **CONTROLLABLE** |
| 2 | phase coherence | **REFUTED** |
| 3 | degeneracy structure | **CONTROLLABLE** |
| 4 | survivor compression | **CONTROLLABLE** (fixed total) / **CORRELATED** (total not fixed) |
| 5 | D96 vs random | **CONTROLLABLE** |
| 6 | D96³ vs D96 | **CONTROLLABLE** |
| — | global rescaling (control) | **CORRELATED** |

**Answer.** **Yes — ρ is CONTROLLABLE: it can change independently of mass-energy.** The reason is
structural rather than dynamical: count conservation makes the total deficit vanish identically
(`Σm = 0`, QG194), so the **arrangement** of ρ is never tied to the energy. What the arrangement *is*
tied to is the **degeneracy structure**, whose free directions number `N − A₀` (51 of 96 for D96 —
exactly the D_048 latent fraction — and 0 for a degeneracy-free lattice).

---

## 5. Consequences

1. **AT's gravity has a genuinely independent variable.** The source term of G_001 (the counting
   measure) is not a function of the total energy; it carries `N − A₀` free directions.
2. **G_001 OP1 is sharpened.** The profile of ρ cannot be fixed by conservation — it has a large
   energy-free subspace — so a **dynamical principle is genuinely required**. This is consistent with
   G4-RHO00-02 ("the dynamical origin of ρ remains OPEN") and turns that statement from a gap into a
   *counting* statement: 51 of 96 directions are unconstrained.
3. **The D_048 latent fraction acquires a geometric meaning.** `L = (N − A₀)/N` is the fraction of
   energy-free directions of the counting measure.
4. **Where a field can come from without energy:** a degeneracy-free (all-singleton) spectrum has
   *no* energy-free room — its density is forced uniform and its field vanishes identically.

---

## 6. Classification

| Component | Status |
|---|---|
| CONTROLLABLE verdict; the `N − A₀` free-direction count; the exact-`ΔE` rearrangements | **DERIVED** (permutation invariance of sums; within-multiplet block sums; QG194) |
| free room = D_048 latent fraction `L` | **DERIVED** (both are `(N − A₀)/N`) |
| uniform ρ ⇒ a = R = 0 (the zero-field baseline) | **DERIVED** (G4-O3, G4-G2) |
| phase-blindness of ρ in the conformal sector | **DERIVED** (QG220 + g = ρ^(2/d)η) |
| `ρ̄ = 1/N`, the exponent `2/d`, and the choice of cell lattice | **BOUNDARY** (lattice definitions, not dynamics) |
| the particular tilt used as the witness | **EMERGENT** (one member of a free family — the audit is existential) |

**No reclassification.** No canonical value changes; the D_040 `ClassificationRegistry` is untouched.
G_001's verdicts (SOURCE = counting measure; CORRELATED = energy and spectral density; REFUTED =
information density and `R = F(ρ)` as a source) are used, not modified.

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| ρ has energy-free directions | a conservation law that fixes ρ's arrangement (forcing `Δρ = 0` for every `ΔE = 0` operation) |
| degeneracy structure is the free room | a degeneracy-free lattice with a non-uniform ρ and `ΔE = 0` |
| phase does not touch ρ | a phase-dependent term in `g = ρ^(2/d)η` or in `R = F(ρ)` |
| compression at fixed total is a density control | a compaction that leaves ρ invariant at fixed total |
| the free room is `N − A₀` | an energy-free direction not counted by `Σ(m_i − 1)` |

---

## 8. Open problems

1. **G-002 OP1 — which direction does the dynamics choose?** The audit shows the free room exists and
   counts it; it does not say which of the `N − A₀` directions the actualization flow selects. This is
   the dynamical counterpart of G_001 OP1 and of G4-RHO.
2. **G-002 OP2 — does a physical operation realise the witness tilt?** The 80/20 within-multiplet tilt
   is an existential witness. Whether any AT process (perturbation, selection, measurement) produces
   it is open; the D_047 lock-release mechanism is the natural candidate.
3. **G-002 OP3 — the ρ̄ = 1/N boundary.** The reference and the lattice definition are BOUNDARY inputs;
   whether they can be derived (or replaced by a local spectral functional) is open, matching G_001 OP3.
4. **G-002 OP4 — observational consequence.** If ρ's arrangement is free, a density reconfiguration at
   fixed total energy is a *sourceless* change of the gravitational field: whether this is
   observationally distinguishable from a mass redistribution is open.

---

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_002_Tests.cs`
**Run:** 2026-09-12 · **Result:** `Tests/Results/Y_G_002_Result.md` (9/9 PASSED, 155 ms)

| Test | Verifies | Result |
|---|---|---|
| `Y_G_002_Baseline` | the measure, `Σm = 0`, the exact zero-field baseline, the real lattices | PASS |
| `Y_G_002_SpectralOrganizationControllable` | permutation at `ΔE = 0`; `a` sign flip | PASS |
| `Y_G_002_PhaseCoherenceRefuted` | ρ, a, R phase-blind; interference 4 → 0 | PASS |
| `Y_G_002_DegeneracyStructureControllable` | block sums fixed; field 0 → 0.603; free room `N − A₀` | PASS |
| `Y_G_002_SurvivorCompression` | `ΔE = 0` at fixed total; `ΔE ≠ 0` without | PASS |
| `Y_G_002_LatticeChoiceD96VsRandom` | random exact null; D96 not | PASS |
| `Y_G_002_LatticeChoiceD96Cubed` | same total, 97.6% vs 53.1% free, different field | PASS |
| `Y_G_002_RescalingCorrelated` | E moves, a exactly invariant, R ∝ λ^(−2/d) | PASS |
| `Y_G_002_Run` | research report | PASS |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_002"`

---

## References

- ResearchY-G_001 (Gravity Source Audit — the source is the counting measure),
  D_047 (lock theorem; degeneracy = free room; release 0.80231), D_048 (latent fraction
  L = 0.53125; random is an exact null), D_049/D_050 (the degeneracy axis), D_051–D_058 (rings,
  degeneracy count, near-gap), T_010/T_012/T_014 (survivors, per-mode ε_j), NP_171 (arrangement
  sensitivity), NP_037/NP_088 (D96³ tensor spectrum).
- AT-QG: QG89, QG194, QG195, QG196, QG197, QG206, QG220, QG228.
- G4: G2, O3, O4, O5, RHO00-02, ME0; `AT_Gravity_Reassessment.md` (the dynamical origin of ρ: OPEN).
