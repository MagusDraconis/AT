# G4-L Phase 12 — Origin of the BDG Normalization

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-L)
**Phase:** 12 — can the BDG normalization factor emerge natively?
**Status:** COMPLETED — 3/3 xUnit tests pass (39/39 G4-L)
**Constraint:** no imported coefficients; causal order, intervals, layers, counting measure only
**Classification:** **NO MATCH** (the scale −2 does not emerge from native quantities)

---

## 1. Goal

The BDG stencil {−2,+4,−2} = s·(−1,+2,−1) with s = 2; the binomial *shape* is native (G4-L11).
Investigate whether the global scale s = 2 emerges from interval-volume, causal-density,
constant-annihilation, or propagator normalization — or only from continuum (second-moment) matching.

---

## 2. Findings

### (a) Constant-annihilation leaves the scale free (G4-L120)

For the one-parameter family a(s) = s·(−1,+2,−1):

| s | Σ a_ℓ (constant) | Σ ℓ·a_ℓ (linear) | Σ ℓ²·a_ℓ (2nd moment) |
|---|---|---|---|
| 0.5…3.0 | 0 | 0 | −2s |

Constants **and** linear functions are annihilated for **every** s — the native 0th/1st-order
constraints do **not** pin the scale. Only the second moment M₂ = −2s varies with s.

### (b) Native counts do not produce −2 (G4-L121)

| grid | mean degree | −degree/2 | past-count range |
|---|---|---|---|
| 7×4 | 6.00 | −3.00 | [8, 33] |
| 9×5 | 6.00 | −3.00 | [8, 57] |

The causal-set Hasse degree is grid-independent (6) but −degree/2 = **−3 ≠ −2**; the past count is
position-dependent ([8,57]) and cannot give a constant diagonal. No native count reproduces the
constant −2.

### (c) The second-moment (continuum) condition pins s = 2 (G4-L122)

M₂(s) = −2s, so s = −M₂/2 is fixed **only** by the second moment. The BDG/continuum value s = 2
gives M₂ = −4 — the d'Alembertian normalization on flat spacetime. The BDG stencil is exactly
a(s=2) = {−2,+4,−2}.

---

## 3. Classification

| candidate | produces −2? | verdict |
|---|---|---|
| 1. interval-volume normalization | position/grid-dependent | NO MATCH |
| 2. causal-density normalization | −degree/2 = −3 | NO MATCH |
| 3. **continuum matching (second moment)** | **yes (s = 2)** | **MATCH (imported)** |
| 4. constant-annihilation constraints | under-determines (s free) | NO MATCH |
| 5. propagator normalization | grid-dependent | NO MATCH |

**Overall: NO MATCH** for native emergence. The scale −2 is pinned *uniquely* by the second-moment
(continuum d'Alembertian) matching — a conformal-scale datum, not a causal-structure datum. The
causal order + counting measure determine the operator's *shape* (the binomial second difference)
but carry no intrinsic length/scale, so the absolute normalization cannot be native.

**Bottom line (closing the G4-L coefficient story):** the native dual-object operators have the
exact BDG *shape* (binomial second difference, alternating layers); the single remaining gap is the
global scale −2, which is fixed by continuum matching and does not admit a native derivation.

---

## Test program

| Test | Verdict |
|---|---|
| G4-L120 `G4_L120_ConstantAnnihilationUnderdeterminesScale` | PASS (constants+linear annihilated for all s) |
| G4-L121 `G4_L121_NativeCountsAreUnstable` | PASS (−degree/2 = −3 ≠ −2; past count [8,57]) |
| G4-L122 `G4_L122_SecondMomentPinsTheScale` | PASS (M₂ = −2s; s = 2 unique via continuum) |

Code: `AT.Tests/ResearchXH/G4L_Phase12_BDGNormalizationTests.cs`.
