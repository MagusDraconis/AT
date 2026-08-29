# ResearchY-D_026 — Compact-Form Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_026 (permanent)
**Title:** Compact-Form Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_026.md`
**Depends on:** ResearchY-D_022 (weak-isospin entry), D_023 (SU(2) entry),
D_024 (doublet compatibility), D_025 (three-generator audit)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_026_Tests.cs`

---

## Purpose

**Why is the compact form su(2) selected?** D_025 showed the upgrade from SO(2) to SU(2)
requires complexification (EMERGENT) + a compact-form choice (BOUNDARY). This audit asks
whether that compact-form choice is physically necessary (forced by observability) or an
independent gauge input.

## Accepted (from D_022–D_025)

- The spectral doublets provide the real 2×2 algebra; complexification gives sl(2,C)
  (D_025).
- sl(2,C) has three real forms: su(2), sl(2,R), su(1,1) (D_025).
- The compact-form choice (su(2) signature) is not derived from the spectrum (D_025).

---

## 1. The three real forms compared

| Real form | Compact? | Bounded generators | Unitary reps | exp(θ·gen) behavior |
|---|---|---|---|---|
| **su(2)** | **YES** (compact) | bounded (finite volume) | **finite-dim** (2j+1) | stays on the compact group |
| sl(2,R) | NO (split) | unbounded | infinite-dim (principal/discrete series) | grows without bound (boost-like) |
| su(1,1) | NO | unbounded | infinite-dim | grows without bound (boost-like) |

**Verified numerically:** exp(5·iσx) has norm 19.57 (bounded), while exp(5·H) for sl(2,R)
has norm 148.4 (unbounded). **SU(2) is the unique compact 3-dimensional real form of
sl(2,C).**

---

## 2. Compactness and unitary representations

**Compact groups** (SU(2)): all irreducible representations are **finite-dimensional and
unitary** — exactly the 2j+1 multiplets (D_024).

**Non-compact groups** (sl(2,R), su(1,1)): unitary irreps are **infinite-dimensional**
(except the trivial rep) — the principal/discrete series. There is **no finite
-dimensional unitary doublet**.

Hence the finite-dim doublet (the weak-isospin fermion, D_024) is unitary **only under
su(2)** among the three real forms.

---

## 3. Probability preservation

Unitary evolution U = exp(iHt) preserves the norm: |Uψ|² = |ψ|².

- **SU(2) elements are unitary** (U†U = 1) — the Born rule Σ|ψ|² = 1 is preserved.
- **sl(2,R)**: exp(θ·H) = diag(e^θ, e^−θ) is NOT unitary — the norm grows without bound.
- **su(1,1)**: same — non-unitary, norm not preserved.

**SU(2) uniquely preserves probability in the finite-dimensional doublet rep.**

---

## 4. Observable survival test

| Observable | Under su(2) | Under sl(2,R)/su(1,1) |
|---|---|---|
| doublets (spectral) | survive | survive (oscillation-derived, D_021) |
| families (octave bands) | survive | survive (D_004, D_016) |
| masses (moments) | survive | survive (D_003–D_006) |
| mixings (CKM/PMNS) | survive | survive (unitary rotations, D_006) |
| **weak sector (W, Z, isospin doublets)** | **survive** | **LOST** (no finite unitary doublet) |

**The spectral observables survive ANY real-form choice** — they are spectrum-derived,
not group-derived. **Only the weak gauge sector requires the finite-dim unitary reps**
that su(2) uniquely provides.

---

## 5. Physical selection criteria

| Criterion | su(2) | sl(2,R)/su(1,1) |
|---|---|---|
| positivity (|ψ|² ≥ 0) | preserved (unitary) | not preserved |
| normalization (Σ|ψ|² = 1, Born) | preserved | not preserved (finite-dim) |
| stability (bounded evolution) | bounded (compact) | unbounded (boosts) |
| closure consistency (2D carrier) | acts on 2D | also acts on 2D — not excluded |

Positivity, normalization, and stability all select **su(2)**. Closure consistency alone
does not exclude sl(2,R)/su(1,1) — they also act on the 2D carrier.

---

## Determination

| Option | Verdict |
|---|---|
| su(2) physically necessary | **YES for the weak sector** — finite-dim unitary (probability-preserving) reps require the compact form |
| su(2) derived from the spectrum | **NO** — the selection criterion (positivity/unitarily) is a mathematical/physical requirement, not a D96 spectral output |
| su(2) a free gauge input | **NO** — it is forced by the requirement of finite-dim probability-preserving reps (not arbitrary) |
| su(2) preserves observability uniquely | **YES for the weak sector** — the spectral sector survives any real form |

**Verdict: the compact-form choice is EMERGENT from observability requirements.**
Positivity, normalization, and stability force the finite-dim unitary (compact) form for
the weak sector — a physical necessity, not an arbitrary gauge choice and not derived
from the D96 spectrum.

---

## Theorem

> **Theorem (D_026).** su(2) is selected by the physical requirement of finite-dimensional
> unitary (probability-preserving) representations. Among the three real forms of
> sl(2,C), su(2) is the unique compact one: its generators are bounded, its unitary
> irreps are finite-dimensional (the 2j+1 multiplets), and its elements preserve the
> norm (Born rule). sl(2,R) and su(1,1) are non-compact: their unitary irreps are
> infinite-dimensional and their exponentials grow without bound (boost-like), so there
> is no finite unitary doublet. The spectral observables (doublets, families, masses,
> mixings) survive ANY real-form choice; only the weak gauge sector (W/Z, isospin
> doublets) requires the finite-dim unitary reps that su(2) uniquely provides. Hence
> su(2) is EMERGENT from observability (positivity/normalization/stability), not derived
> from the spectrum and not a free gauge input.
>
> *Proof sketch.* (1) su(2) is the unique compact 3-dim real form of sl(2,C); its
> exponentials are bounded, while sl(2,R)/su(1,1) grow without bound (Section 1,
> verified numerically). (2) Compact groups have finite-dim unitary irreps; non-compact
> have only infinite-dim ones (Section 2). (3) Unitary elements preserve the norm —
> SU(2) preserves probability; sl(2,R)/su(1,1) do not in the finite-dim rep (Section 3).
> (4) The spectral observables are spectrum-derived and survive any real form; the weak
> sector needs finite-dim unitary reps (Section 4). (5) Positivity, normalization, and
> stability select su(2); closure consistency alone does not exclude the others (Section
> 5). Hence su(2) is EMERGENT from observability. ∎

---

## Dependency Graph

```
oscillation
 → spectral Z2 (λ_k = λ_{N−k})      [DERIVED]
 → quadrature doublets {cos, sin}   [DERIVED]
 → real algebra {I, J, P, JP}       [DERIVED]
 → complexification (Fourier i)     [EMERGENT — from the phase]
 → sl(2,C)                          [EMERGENT — complexification]
 → compact-form choice              [EMERGENT — from observability:
                                      positivity/normalization/stability]
 → su(2)                            [EMERGENT — finite-dim unitary weak sector]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is su(2) the unique compact real form? | YES |
| Do sl(2,R)/su(1,1) have finite-dim unitary doublets? | NO (infinite-dim unitary reps) |
| Does SU(2) preserve probability in the doublet? | YES (unitary, Born rule) |
| Do sl(2,R)/su(1,1) preserve the finite-dim norm? | NO (boost-like growth) |
| Do the spectral observables survive any real form? | YES |
| Does the weak sector require su(2)? | YES (finite-dim unitary) |
| Is su(2) derived from the D96 spectrum? | NO (observability requirement) |

---

## Counterexamples

1. **sl(2,R) in place of su(2)**: the finite-dim doublet is not unitary; W/Z bosons
   (finite-dim unitary reps) do not exist; probability is not conserved in the doublet.
   The spectral sector (doublets/families/masses) is unaffected.
2. **su(1,1) in place of su(2)**: same — non-compact, no finite unitary doublet, weak
   sector lost.
3. **Closure consistency alone**: the ring's D_n symmetry is O(2)-type, and sl(2,R)/
   su(1,1) also act on the 2D carrier — closure does NOT select su(2); only the
   observability criteria do.

---

## Classification

| Component | Status |
|---|---|
| spectral observables (doublets, families, masses, mixings) | **DERIVED** (from the spectrum) |
| su(2) compactness (bounded generators, finite-dim unitary) | **EMERGENT** (mathematical fact, physically required) |
| weak sector requires finite-dim unitary | **EMERGENT** (observability) |
| su(2) derived from D96 spectrum | **REFUTED** |
| su(2) free gauge input | **REFUTED** (forced by observability) |

**The compact-form choice is EMERGENT from observability — neither derived from the
spectrum nor a free gauge input.**

---

## Open Problems

1. **Origin of positivity (D_026 OP1).** The selection criterion is positivity/
   normalization/stability (Born rule). Whether these themselves are derivable from
   Difference/Actualization (vs assumed as the observability requirement) is open.
2. **Why the weak sector exists (D_026 OP2).** The spectral sector survives any real
   form; the weak sector is what forces su(2). Whether the weak sector's existence is
   derived (why is there a gauge sector at all?) is the deeper open question.

---

## Next Steps

- **ResearchY-D_027 (or synthesis):** the compact-form audit completes the gauge chain
  (oscillation → doublets → complexification → sl(2,C) → observability → su(2)). A
  synthesis can map the full gauge-sector boundary structure.
- **D_025 follow-up:** the "su(2) EMERGENT from observability" verdict refines D_025 —
  the compact-form choice is not arbitrary; it is forced by finite-dim unitarity.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_026_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_026_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_026_Compactness` | su(2) unique compact; sl(2,R)/su(1,1) non-compact (unbounded) | ✅ |
| `Y_D_026_UnitaryRepresentations` | compact → finite-dim unitary; non-compact → infinite-dim | ✅ |
| `Y_D_026_ObservableSurvival` | spectral observables survive any real form; weak sector needs su(2) | ✅ |
| `Y_D_026_ProbabilityPreservation` | SU(2) unitary preserves norm; sl(2,R) does not | ✅ |
| `Y_D_026_AlternativeRealForms` | sl(2,R)/su(1,1) break the weak sector | ✅ |
| `Y_D_026_Run` | Research report | ✅ |

**Conclusion:** su(2) is selected by the physical requirement of finite-dimensional
unitary (probability-preserving) representations. It is the unique compact real form of
sl(2,C); sl(2,R) and su(1,1) are non-compact (unbounded boosts, infinite-dim unitary
reps, no finite probability conservation). The spectral observables (doublets, families,
masses, mixings) survive ANY real-form choice; only the weak sector (W/Z, isospin
doublets) requires finite-dim unitary reps, which su(2) uniquely provides. The compact-
form choice is **EMERGENT from observability** (positivity/normalization/stability), not
derived from the spectrum and not a free gauge input. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_026"`

---

## References

- ResearchY-D_022 (weak-isospin entry), D_023 (SU(2) entry), D_024 (doublet
  compatibility), D_025 (three-generator audit).
- AT-QG: QG153 (doublet origin), QG670/680 (SU(2) spin sector — POSTULATED input).
- Monograph V2.0: Ch6 (D96 spectrum).
