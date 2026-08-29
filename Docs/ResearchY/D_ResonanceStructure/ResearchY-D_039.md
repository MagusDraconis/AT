# ResearchY-D_039 — State-Identity-Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_039 (permanent)
**Title:** State-Identity-Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_039.md`
**Depends on:** ResearchY-D_033 (singlet-prohibition), D_034 (reciprocity),
D_035 (multiplet-requirement), D_036 (complex-state-origin), D_038 (state-identity)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_039_Tests.cs`

---

## Purpose

**Why must an observable state have a unique identity?** D_038 showed observability
requires state identity (magnitude+phase). This audit asks the deepest question of the
chain: is the identity requirement itself derived from the primitive **Difference**, or
is it a final boundary principle?

## Accepted (from D_033, D_034, D_035, D_036, D_038)

- Magnitude |ψ|=√ρ is DERIVED from the count (QG216); phase θ=2πk/N from the
  circulation (QG220) — two faces of the same tick (D_036).
- Observability requires state identity; magnitude-only collapses identity (3 states),
  phase-only loses probability; the complex state is the minimal complete identity
  (D_038).
- Complete pairing DERIVED from complex observability (D_035); reciprocity EMERGENT
  (D_037); Z2-paired sector requirement BOUNDARY (D_020).
- Only {Difference, η} is BOUNDARY among the primitives (D_027).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **state identity** | each observable state is a unique point of the state space (injective k → state) |
| **distinguishability** | two states can be told apart — there EXISTS a measurement that differs |
| **observability** | the state is fully specified AND distinguishable (D_038) |
| **Difference** | the primitive: the act of distinguishing one thing from another |

---

## 2. Difference IS distinguishability

The primitive **Difference** is not a numerical value — it is the ACT of distinguishing.
Two things "differ" iff they can be told apart. Therefore:

```
Difference (primitive)
  = distinguishability (semantic content of the primitive)
  = state identity applied to a state space
```

**State identity is the primitive Difference applied to the observable sector.** It is
not a separate input: a state space in which two modes cannot be told apart fails the
primitive itself.

---

## 3. Test: identity in the three state spaces

| Space | # distinct states (N=96) | Difference realized? | Probability? |
|---|---|---|---|
| magnitude-only | **3** | **NO** (mirror pairs collapse) | YES (shares) |
| phase-only | 95 | YES (identity) | **NO** (uniform) |
| complex | **95** | **YES** (full identity) | **YES** |

### Magnitude-only: Difference fails

The real-only (magnitude-only) space collapses the 95 modes to **48 distinct real
states** (47 mirror pairs + 1 self-conjugate): the mirror pair k/N−k has IDENTICAL cos
(canonical cos-even identity). The pair is indistinguishable — **no Difference between
them**. At the occupancy-group level, magnitude-only gives only **3 distinct states**.

### Phase-only: identity but no content

Phase-only gives 95 distinct states (full identity) but uniform |ψ|=1: no count
structure, no probability content. Difference is realized but the sector is empty of
"how much" information.

### Complex: Difference fully realized

The complex space gives **95/95 distinct states** with the Born rule Σρ=1 EXACT. Every
mode is distinguishable from every other — **Difference is fully realized**.

---

## 4. What fails first when identity is removed?

| Removed | Survives | Fails first |
|---|---|---|
| unique identity (allow indistinguishable modes) | the raw spectral content (eigenvalues λ_k exist) | **DISTINGUISHABILITY / OBSERVABILITY** — the mirror pair becomes one state (48 vs 95); then interference (needs distinct paths), reciprocity (needs a distinct partner), pairing (needs distinct doublet members), and the spectrum reading (modes collapse) |

Removing identity collapses the mode structure itself: 95 distinguishable modes → 48
real states → 3 magnitude buckets. The spectrum reading survives only as a bag of
numbers; the *structure* (which mode is which) is lost.

---

## 5. Does identity follow from…?

| Candidate | Verdict |
|---|---|
| A) Difference | **YES** — Difference IS distinguishability; identity is the primitive applied |
| B) Actualization | PARTIAL — provides the circulation/phase (the distinguishing DOF, QG220) |
| C) Count conservation | PARTIAL — provides the magnitude (count content, QG216) |
| D) Observability | YES — as the sector requirement, but that requirement IS Difference applied |
| E) none | NO |

**State identity follows from Difference itself: the primitive's semantic content is
the act of distinguishing, so a state space that cannot distinguish modes fails the
primitive. No new boundary is introduced by the identity requirement.**

---

## 6. Remove unique identity: what survives?

| Removed | Survives | Breaks |
|---|---|---|
| unique identity | the eigenvalues λ_k (raw spectrum), normalization | the mode structure (95 → 48 → 3), distinguishability, observability, interference, reciprocity, pairing, spectrum reading as a structure |

---

## 7. Prove or refute: Difference implies distinguishability

**YES — trivially, by definition.** "Difference" means two things can be told apart.
A state space in which two modes are indistinguishable contains no Difference between
them — it fails the primitive. The complex state space is the minimal space in which
Difference is fully realized: 95/95 distinct states, Born rule exact. **Difference
implies distinguishability; distinguishability forces the two-DOF complex structure.**

---

## Theorem

> **Theorem (D_039).** Difference implies distinguishability, and distinguishability is
> exactly state identity. The primitive "Difference" is the act of distinguishing one
> state from another; a state space in which two modes cannot be told apart fails the
> primitive itself. Hence state identity is not a boundary principle — it is the
> primitive Difference applied to the observable sector. The real-only space collapses
> the 95 modes to 48 distinct real states (mirror pairs have identical cos — no
> Difference between them) and further to 3 magnitude buckets; phase-only loses the
> count content. The complex space ψ = |ψ|·e^{iθ} realizes Difference fully: 95/95
> distinct with the Born rule exact. Therefore: Difference (the primitive) is BOUNDARY;
> state identity (distinguishability) is DERIVED from it; the complex state is the
> DERIVED minimal space realizing it; observability is EMERGENT; the Z2-paired sector
> requirement is BOUNDARY (D_020).
>
> *Proof sketch.* (1) Difference = distinguishability (Section 2, definitional). (2)
> Magnitude-only: 48 real states / 3 magnitude buckets — mirror pairs collapse (Section
> 3, verified). (3) Phase-only: 95/95 identity but no probability (Section 3, verified).
> (4) Complex: 95/95 + Born rule exact (Section 3, verified). (5) Removing identity
> collapses the mode structure (Section 4). (6) Hence identity is the primitive applied,
> not a new input (Sections 5–6). ∎

---

## Dependency Graph

```
Difference (primitive)
 → distinguishability                    [DERIVED — the primitive's semantic content]
 → state identity (injective k → state)  [DERIVED — the primitive applied]
 → observability (fully specified + distinguishable)  [EMERGENT]
 → complex state ψ = |ψ|·e^{iθ}          [DERIVED — the minimal space realizing it]
   magnitude (count, QG216)              [DERIVED]
   phase (circulation, QG220)            [DERIVED]
 → probability content (Born rule Σρ=1)  [DERIVED]
 → reciprocity / interference            [DERIVED — D_037]
 → complete pairing                      [DERIVED — D_035]
 → Z2-paired sector requirement          [BOUNDARY — D_020]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is Difference = distinguishability? | **YES** (definitional) |
| Is state identity the primitive applied? | **YES** |
| Does magnitude-only realize Difference? | NO (48 states / 3 buckets; mirror collapse) |
| Does phase-only carry probability? | NO (uniform) |
| Does complex space realize Difference fully? | **YES** (95/95 + Born rule exact) |
| Is state identity a separate boundary? | **NO** — it is the primitive applied |
| What fails first without identity? | **distinguishability / observability** (mode structure collapses) |
| Does Difference imply distinguishability? | **YES** (by definition) |

---

## Counterexamples

1. **Magnitude-only (N=96)**: mirror pair k=16 / N−k=80 have identical cos — 48 real
   states / 3 magnitude buckets; Difference is NOT realized between the pair.
2. **Phase-only (N=96)**: 95 distinct states but uniform |ψ|=1 — Difference is
   realized but the count content (probability) is absent.
3. **Complex (N=96)**: 95/95 distinct + Born rule exact — Difference fully realized.
4. **N=64 singlet**: phase pinned to π — the mode has no free phase, so its identity
   (as a two-DOF state) is incomplete without the degenerate multiplet (D_035).

---

## Classification

| Component | Status |
|---|---|
| Difference (primitive) | **BOUNDARY** (the primitive itself, D_027) |
| distinguishability | **DERIVED** (= Difference's semantic content) |
| state identity | **DERIVED** (the primitive applied to the state space) |
| magnitude (count) / phase (circulation) | **DERIVED** (QG216/QG220) |
| complex state (minimal identity space) | **DERIVED** (QG218) |
| observability | **EMERGENT** |
| interference / reciprocity | **DERIVED** (D_037) |
| Z2-paired sector requirement | **BOUNDARY** (D_020) |

**State identity is DERIVED from Difference itself — the primitive IS
distinguishability. The only boundaries remain the primitives {Difference, η} (D_027)
and the Z2-paired sector requirement (D_020).**

---

## Open Problems

1. **Observable-sector boundary (D_039 OP1).** Why the observable sector is the
   Z2-paired (complex) sector at all — the boundary (D_020) that the primitive
   Difference is applied to — remains open. The identity requirement itself no longer
   adds a boundary; the sector choice does.

---

## Next Steps

- **ResearchY-D_040 (or synthesis):** the state-identity-origin audit closes the
  identity chain (Difference → distinguishability → identity → complex state). A
  synthesis can map the complete boundary: {Difference, η} + Z2-paired sector
  requirement, with everything downstream DERIVED.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_039_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_039_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_039_IdentityLoss` | removing identity collapses mode structure (95→48→3) | ✅ |
| `Y_D_039_Distinguishability` | Difference = distinguishability; complex 95/95; real 48 | ✅ |
| `Y_D_039_MagnitudeOnly` | magnitude-only collapses (mirror pairs identical) | ✅ |
| `Y_D_039_PhaseOnly` | phase-only identity but no probability | ✅ |
| `Y_D_039_ObservableState` | complex = full identity + Born rule exact | ✅ |
| `Y_D_039_DependencyTrace` | Difference → distinguishability → identity → complex state | ✅ |
| `Y_D_039_Run` | Research report | ✅ |

**Conclusion:** State identity is DERIVED from Difference itself — the primitive IS
distinguishability, so a state space that cannot distinguish modes fails the primitive.
The real-only space collapses 95 modes to 48 states (mirror pairs identical); the
complex space realizes Difference fully (95/95 distinct, Born rule exact). The only
boundaries are the primitives {Difference, η} (D_027) and the Z2-paired sector
requirement (D_020). No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_039"`

---

## References

- ResearchY-D_033 (singlet-prohibition), D_034 (reciprocity), D_035
  (multiplet-requirement), D_036 (complex-state-origin), D_038 (state-identity).
- AT-QG: QG216 (amplitude = branching count), QG218 (Hilbert origin), QG220 (phase
  origin), QG153/155 (Z2 doublets), D_027 (selector-origin: {Difference, η} boundary).
- Monograph V2.0: Ch6 (D96 spectrum), Ch9 (quantum mechanics — Born rule).
