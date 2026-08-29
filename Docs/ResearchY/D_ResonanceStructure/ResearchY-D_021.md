# ResearchY-D_021 — Oscillation Symmetry Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_021 (permanent)
**Title:** Oscillation Symmetry Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_021.md`
**Depends on:** ResearchY-D_001 (standing waves), D_002 (standing wave model),
D_009 (minimum excitation), D_020 (selection precondition)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_021_Tests.cs`

---

## Purpose

**Is complete Z2 pairing a consequence of oscillation symmetry?** Determine whether the
Z2 doublet pairing (weak-isospin doublets in the physical reading) originates from the
fundamental ± structure of standing waves (phase inversion, mirror mode) rather than
from weak-isospin as an independent input.

## Accepted (from D_001, D_002, D_009, D_020)

- The D96 standing-wave model is center-free and HYBRID: spatial harmonics
  {cos, sin} at each k, oscillating at ω_k = √λ_k (D_001, D_002).
- The fundamental doublet (k=1, N−1) carries ω₁ = 0.6216 (D_009).
- Complete Z2 pairing (0 unpaired modes) is part of the observable-sector construction
  INPUT (D_020).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **oscillation symmetry** | the invariance of a standing wave ψ = A·cos(ωt) under sign inversion of the amplitude (+A ↔ −A), phase inversion (cos(ωt) ↔ −cos(ωt)), and mode-index mirror (k ↔ N−k) |
| **Z2 pairing** | the spectral degeneracy λ_k = λ_{N−k}: two modes share one frequency |
| **phase inversion** | the map cos(ωt) → −cos(ωt) = cos(ωt + π): a half-period time shift |
| **mirror mode** | the mode at index N−k, the reflection partner of k |

---

## 2. Test: the three Z2 symmetries

### 2.1 +A ↔ −A (amplitude sign)

`+A·cos(ωt)` and `−A·cos(ωt)` are the **same oscillation with a π phase offset** —
−cos(ωt) = cos(ωt + π). This is a global sign that any single mode possesses
individually. **It does not pair two modes** — it is a phase redundancy of one
oscillator.

### 2.2 cos(ωt) ↔ −cos(ωt) (phase inversion)

`−cos(ωt) = cos(ωt + π)`, a half-period time shift. Every mode has this symmetry alone.
**It does not create pairing** — it is the temporal phase gauge.

### 2.3 k ↔ N−k (mirror mode) — THE pairing generator

The mirror map k → N−k acts on the spatial harmonics:

```
cos(2π(N−k)n/N) = cos(2πkn/N)      (identical — cos is even)
sin(2π(N−k)n/N) = −sin(2πkn/N)     (sign flip — sin is odd)
```

So the pair {cos(2πkn/N), sin(2πkn/N)} at frequency ω_k is **two quadratures of ONE
oscillation** — the two phases of the same frequency. The mirror mode N−k is the same
cos harmonic and the sign-flipped sin harmonic; the 2D eigenspace {cos, sin} is the
mirror-pair invariant subspace. Verified: λ_k = λ_{N−k} for all k (e.g. λ₁ = λ₉₅ =
0.3864).

**The Z2 pairing is the two-quadrature structure of a single real oscillation** — the
cos/sin (or ±e^{iωt}) split of a real mode into its two phase components. This is the
fundamental ± structure of the standing wave itself.

---

## 3. Is Z2 pairing oscillation necessity?

**YES for the pairing structure.** The quadrature pair {cos, sin} at a single k is the
two-phase decomposition of a real oscillation at ω_k. Both are eigenfunctions of L with
the SAME eigenvalue λ_k (verified numerically for k=1, 3, 47): the pair is intrinsic to
the standing wave, not an import.

| Option | Verdict |
|---|---|
| A) weak-isospin only | **NO** — the pairing is spectral (λ_k = λ_{N−k}), oscillation-intrinsic |
| B) spectral symmetry | **YES** — the pairing is the λ_k = λ_{N−k} degeneracy |
| C) oscillation necessity | **YES** — the cos/sin quadrature pair is the ± structure of one oscillation |

The physical reading (weak-isospin doublets = Z2 pairs) is the EMERGENT interpretation
of a DERIVED spectral structure — consistent with D_014 (two-anchor boson/fermion split
is EMERGENT).

---

## 4. Remove Z2 pairing: does standing-wave completeness survive?

**YES.** Standing-wave completeness is a *basis* property; Z2 pairing is an
*eigenvalue-degeneracy* property. The Fourier basis {cos_k, sin_k, zero mode} is complete
for ANY N (verified: N=64, 96, 128 each give exactly N independent real modes). Even
when eigenvalues are non-degenerate (no Z2 pairing), the standing waves still form a
complete set — completeness does not require degeneracy.

The Z2 pairing therefore does NOT determine completeness; it determines the **doublet
content** (which frequency has two modes), which is the physical reading layer
(D_003–D_006 sector assignment).

---

## 5. Fundamental source of pairing

```
oscillation (real standing wave ψ = A cos(ωt + δ))
 → two quadratures (cos, sin) at each frequency   [DERIVED — oscillation necessity]
 → spectral degeneracy λ_k = λ_{N−k}              [DERIVED — ring's reflection symmetry]
 → Z2 doublet structure (47 pairs + 5 + 6 groups) [DERIVED — spectral]
 → weak-isospin doublet reading                   [EMERGENT — physical interpretation]
```

The **complete** pairing (0 unpaired modes, self-conjugate mode k=48 in a 5-fold group)
is a separate fact: whether the self-conjugate mode shares its eigenvalue with other
modes is an **N-arithmetic** property (λ=12 at k=N/2 is 5-fold at N=96 but 1-fold at
N=64/128). This is the D_020 observable-sector INPUT (which selected N=96), not an
oscillation consequence.

---

## Theorem

> **Theorem (D_021).** Z2 pairing is the two-quadrature structure of a single real
> oscillation: the pair {cos(2πkn/N), sin(2πkn/N)} at frequency ω_k is the ± phase
> decomposition of one standing wave, forced by the spectral symmetry λ_k = λ_{N−k} of
> the ring's reflection automorphism. This pairing is DERIVED (oscillation necessity +
> spectral symmetry), not an independent weak-isospin input. Standing-wave completeness
> (a complete Fourier basis) survives removal of Z2 pairing — completeness is a basis
> property, pairing is a degeneracy property. The COMPLETENESS of pairing (0 unpaired)
> is an N-arithmetic selection (D_020), not an oscillation consequence.
>
> *Proof sketch.* (1) +A↔−A and cos↔−cos are phase symmetries of a single mode — they
> do not pair modes (Sections 2.1–2.2). (2) k↔N−k maps cos→cos, sin→−sin, giving the
> 2D eigenspace {cos, sin} at one frequency — the pairing generator (Section 2.3). (3)
> Both cos and sin are eigenfunctions of L with the SAME λ_k (verified) — the pair is
> oscillation-intrinsic. (4) The Fourier basis is complete for all N, degenerate or not
> (Section 4) — completeness survives pairing removal. (5) Whether the self-conjugate
> mode is paired (0 unpaired) depends on N arithmetic (λ=12 degeneracy), the D_020
> selection input. Hence: pairing DERIVED; completeness-of-pairing BOUNDARY; weak-isospin
> reading EMERGENT. ∎

---

## Dependency Graph

```
oscillation (ψ = A cos(ωt + δ))
 → phase inversion (cos ↔ −cos)        [DERIVED — temporal gauge, no pairing]
 → quadrature pair {cos, sin} at ω_k   [DERIVED — oscillation necessity]
     → spectral degeneracy λ_k = λ_{N−k}  [DERIVED — ring reflection symmetry]
     → Z2 doublet structure            [DERIVED]
         → weak-isospin doublets       [EMERGENT — physical reading]
     → complete pairing (0 unpaired)   [BOUNDARY — N-arithmetic, D_020 input]
```

---

## Counterexamples

1. **N=64**: the self-conjugate mode k=32 has λ=12 with multiplicity 1 — **incomplete
   pairing** (1 unpaired mode) — yet the standing-wave basis is still complete (64
   independent modes). Same oscillation structure, different N arithmetic.
2. **N=128**: same — λ=12 at k=64 is 1-fold; basis complete.
3. **N=192**: self-conjugate k=96 sits in a 5-fold λ=12 group — complete pairing, like
   N=96. Confirms pairing completeness tracks N arithmetic, not oscillation.
4. **A single non-degenerate mode**: a lone mode +A·cos(ωt) already possesses the
   oscillation symmetries (+A↔−A, cos↔−cos) without any partner — the ± structure does
   not require pairing.

---

## Classification

| Component | Status |
|---|---|
| phase inversion symmetry (+A↔−A, cos↔−cos) | **DERIVED** (temporal phase gauge of one mode) |
| quadrature pair {cos, sin} at each ω_k | **DERIVED** (oscillation necessity) |
| spectral degeneracy λ_k = λ_{N−k} | **DERIVED** (ring reflection symmetry) |
| Z2 doublet structure | **DERIVED** (spectral) |
| complete pairing (0 unpaired) | **BOUNDARY** (N-arithmetic, D_020 input) |
| weak-isospin doublet reading | **EMERGENT** (physical interpretation) |

**The pairing structure is DERIVED from oscillation; the completeness of pairing is a
BOUNDARY selection (D_020); the weak-isospin reading is EMERGENT.**

---

## Open Problems

1. **Oscillation-necessity limit (D_021 OP1).** The quadrature pair is derived from
   oscillation at a single k; whether the full 47-pair structure (multiplicities beyond
   2, e.g. the 5- and 6-fold groups) is also oscillation-derived or purely spectral
   remains a refinement.
2. **Completeness arithmetic (D_021 OP2).** The 0-unpaired condition tracks the λ=12
   self-conjugate degeneracy (N-divisibility). A closed-form characterization of which
   N give complete pairing (beyond the D_020 selection) is open.

---

## Next Steps

- **ResearchY-D_022 (or synthesis):** the oscillation-symmetry audit (this) separates
  the DERIVED pairing structure from the BOUNDARY completeness. A synthesis can map the
  full D96 chain: oscillation → quadrature → Z2 → doublets → (weak-isospin EMERGENT).
- **D_020 refinement:** the "complete Z2 pairing" input can now be stated precisely as
  the N-arithmetic completeness (λ=12 self-conjugate degeneracy), distinct from the
  DERIVED pairing structure.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_021_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_021_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_021_OscillationSymmetry` | +A↔−A, cos↔−cos are per-mode phase gauges; k↔N−k pairs | ✅ |
| `Y_D_021_MirrorMode` | cos(N−k)=cos(k), sin(N−k)=−sin(k); λ_k=λ_{N−k} | ✅ |
| `Y_D_021_QuadraturePair` | cos and sin are both eigenfunctions of L at same λ_k | ✅ |
| `Y_D_021_PairingDerived` | pairing is DERIVED (oscillation+spectral), not weak-isospin-only | ✅ |
| `Y_D_021_CompletenessSurvives` | standing-wave basis complete for all N (deg or not) | ✅ |
| `Y_D_021_CompletenessArithmetic` | 0-unpaired tracks N arithmetic, not oscillation | ✅ |
| `Y_D_021_Run` | Research report | ✅ |

**Conclusion:** Z2 pairing is the two-quadrature (cos/sin) structure of a single real
oscillation — **DERIVED** from oscillation necessity and the ring's spectral symmetry
λ_k = λ_{N−k}. It is NOT a weak-isospin-only input; the weak-isospin doublet reading is
**EMERGENT**. Standing-wave completeness survives removal of Z2 pairing (basis vs
degeneracy). Only the *completeness* of pairing (0 unpaired) is a BOUNDARY N-arithmetic
selection (D_020). No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_021"`

---

## References

- ResearchY-D_001 (standing waves), D_002 (standing wave model), D_009 (minimum
  excitation), D_020 (selection precondition).
- AT-QG: QG153 (doublet origin), QG155 (Z2 symmetry origin — dihedral group D_n,
  2D irreps), QG159 (D96 selection).
- Monograph V2.0: Ch6 (D96 spectrum).
