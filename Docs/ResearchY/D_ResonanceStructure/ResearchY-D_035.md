# ResearchY-D_035 — Multiplet-Requirement Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_035 (permanent)
**Title:** Multiplet-Requirement Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_035.md`
**Depends on:** ResearchY-D_020 (selection precondition), D_032 (pairing-requirement),
D_033 (singlet-prohibition), D_034 (reciprocity)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_035_Tests.cs`

---

## Purpose

**Why must the self-conjugate mode participate in a degenerate multiplet?** This audit
asks whether complete pairing (0 unpaired modes) is derivable from complex-state
observability — refining the boundary identified in D_032/D_034.

## Accepted (from D_020, D_032, D_033, D_034)

- The pairing STRUCTURE is DERIVED (D_021); complete pairing is the observable-sector
  input (D_032).
- The singlet (lone self-conjugate mode) is mathematically valid but physically excluded
  (D_033).
- Reciprocity = the [magnitude, phase] complex structure; the two DOFs are DERIVED
  (D_034/QG218).

---

## 1. The self-conjugate mode is real-only

At k = N/2, sin(2π·(N/2)·n/N) = sin(πn) = 0 — only the cos harmonic survives (verified:
max|sin| ≈ 10⁻¹⁴ at k=N/2 for all tested N). The self-conjugate mode is **real-only**.

The eigenvalue λ(N/2) = 12 is fixed, but its **multiplicity varies**:

| N | k_sc | λ(N/2) | multiplicity | eigenspace | complex? |
|---|---|---|---|---|---|
| 64 | 32 | 12 | 1 | 1D real | **no** |
| 80 | 40 | 12 | 1 | 1D real | no |
| **96** | **48** | **12** | **5** | **5D** | **yes** |
| 128 | 64 | 12 | 1 | 1D real | no |
| 192 | 96 | 12 | 5 | 5D | yes |

---

## 2. Complex observability requires multiplicity ≥ 2

An observable frequency's eigenspace must carry the [magnitude, phase] pair
(D_034/QG218):

- a **1D real eigenspace** (the isolated singlet) is real-only — it cannot carry the
  complex structure (no sin/phase partner).
- a **2D+ eigenspace** carries the cos/sin (Re/Im) pair — the complex structure.

**Complex observability requires every eigenvalue to have multiplicity ≥ 2.**

| N | all eigenvalues mult ≥ 2? | min multiplicity |
|---|---|---|
| 64 | **NO** | 1 |
| 80 | NO | 1 |
| **96** | **YES** | 2 |
| 128 | NO | 1 |
| 192 | YES | 2 |

---

## 3. The self-conjugate mode must participate in a multiplet

The self-conjugate mode alone is real-only. To be complex-observable, its eigenvalue
must share the eigenspace with other modes — a **degenerate multiplet** that supplies
the phase/quadrature partners.

At N=96, the λ=12 group is {16, 32, 48, 64, 80} (5-fold): k=48 is real-only, but k=16,
32, 64, 80 have full cos+sin quadratures — the group as a whole carries the complex
structure.

---

## 4. Does a lone self-conjugate mode violate complex observability?

**YES.** The isolated singlet (N=64, λ=12 mult 1):
- real-only (no sin spatial harmonic);
- cannot form the complex e^{iθ} with a spatial phase partner;
- its eigenvalue has a 1D real eigenspace — real, not complex.

It violates the complex-state observability requirement (QG218/D_034).

---

## 5. Is complete pairing required by…?

| Candidate | Verdict |
|---|---|
| A) complex-state structure | **YES** — an eigenvalue must carry [magnitude, phase], so mult ≥ 2 |
| B) interference | PARTIAL — the real-only singlet loses interference for its frequency |
| C) reciprocity | **YES** — reciprocity (every mode complex) requires the multiplet |
| D) representation closure | PARTIAL — the 1D singlet has no doublet |
| E) none | NO |

**Complete pairing (mult ≥ 2 for every eigenvalue) is required by complex-state
observability (A/C).**

---

## 6. The refinement: complete pairing is DERIVED from complex observability

D_034 classified complete pairing as BOUNDARY. This audit refines that:

```
complex structure (two DOFs)   DERIVED (QG218)
  → complex observability (every eigenvalue mult ≥ 2)   EMERGENT
      → complete pairing (0 unpaired)                    DERIVED from it
      → the observable sector is complex                 BOUNDARY (the input)
```

The boundary moves **one step deeper**: from "0 unpaired modes" to
**"the observable sector is complex"** (not real-only).

---

## 7. Remove the requirement: what survives, what breaks first?

| Removed | Survives | Breaks first |
|---|---|---|
| complex observability (allow mult 1) | spectral content (families, moments, span); normalization | **complex observability of the self-conjugate frequency** — that eigenvalue becomes real-only (classical addition, no interference) |

---

## Theorem

> **Theorem (D_035).** The self-conjugate mode must participate in a degenerate multiplet
> because complex-state observability requires every eigenvalue to have multiplicity
> ≥ 2. The self-conjugate mode k=N/2 is real-only (sin(πn)=0); its eigenvalue λ=12 has
> a 1D real eigenspace at N=64/80/128 (an isolated singlet violating complex
> observability) and a 5D eigenspace at N=96/192 (the multiplet supplies the
> phase/quadrature partners). Complete pairing (0 unpaired) is DERIVED from complex
> observability (every eigenvalue mult ≥ 2, from the [magnitude, phase] structure of
> QG218). The boundary moves one step deeper: the requirement is not "0 unpaired modes"
> but "the observable sector is complex."
>
> *Proof sketch.* (1) The self-conjugate mode is real-only (sin(πn)=0) — Section 1. (2)
> λ(N/2)=12 is 1-fold at 64/80/128 and 5-fold at 96/192 (Section 1, verified). (3)
> Complex observability requires mult ≥ 2 (a 1D real eigenspace is real-only, Section 2,
> verified). (4) The 5-fold group supplies the phase partners for the real-only k=N/2
> (Sections 3). (5) A lone singlet violates complex observability (Section 4). (6)
> Complete pairing is DERIVED from complex observability; the boundary is "the
> observable sector is complex" (Section 6). Hence the multiplet participation is
> DERIVED; the complex-sector requirement is BOUNDARY. ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → complex state (magnitude + phase, QG218)      [DERIVED]
 → complex observability (mult ≥ 2)               [EMERGENT]
 → complete pairing (0 unpaired)                  [DERIVED from complex observability]
 → p=3 (minimal complete-pairing period)          [DERIVED]
 → N=96 (unique zero-defect octave rung)          [DERIVED]
 → the observable sector is complex               [BOUNDARY — the input]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the self-conjugate mode real-only? | **YES** (sin(πn)=0) |
| Does complex observability require mult ≥ 2? | **YES** (1D real eigenspace is real-only) |
| Does the multiplet supply the phase partners? | **YES** (the 5-fold group at N=96) |
| Does a lone singlet violate complex observability? | **YES** |
| Is complete pairing DERIVED from complex observability? | **YES** |
| What is the boundary? | "the observable sector is complex" |

---

## Counterexamples

1. **N=64**: λ=12 has multiplicity 1 — the self-conjugate mode is an isolated real-only
   singlet, violating complex observability.
2. **N=80, 128**: same — 1D real eigenspace at λ=12.
3. **N=96**: λ=12 is 5-fold — the multiplet supplies the phase partners; every eigenvalue
   has mult ≥ 2 (complete).
4. **N=192**: complete like N=96, but 4 families — complex observability alone does not
   select 96.

---

## Classification

| Component | Status |
|---|---|
| complex-state structure (two DOFs) | **DERIVED** (QG218) |
| complex observability (every eigenvalue mult ≥ 2) | **EMERGENT** (the requirement) |
| complete pairing (0 unpaired) | **DERIVED** (from complex observability) |
| self-conjugate multiplet participation | **DERIVED** (from complex observability) |
| the observable sector is complex | **BOUNDARY** (the input) |

**Complete pairing is DERIVED from complex observability; the boundary is "the
observable sector is complex."**

**Refinement (D_036):** "the observable sector is complex" reduces to the Z2-paired
sector requirement (D_020) — the same input, stated from the pairing side. The pairing
STRUCTURE itself is DERIVED (D_021); complete pairing (0 unpaired) remains DERIVED as
concluded here.

---

## Open Problems

1. **Complex-sector origin (D_035 OP1).** Why the observable sector must be complex
   (not real-only) — whether this follows from Difference itself or is the deepest
   observable input is the QG218 boundary.
2. **Interference necessity (D_035 OP2).** Whether interference (the physical
   consequence of complexity) is itself a required observable — beyond the complex
   structure — is open.

---

## Next Steps

- **ResearchY-D_036 (or synthesis):** the multiplet-requirement audit completes the
  pairing chain (Difference → complex state → complex observability → complete pairing
  → N=96). A synthesis can map the full observable-sector boundary.
- **D_032/D_034 refinement:** the boundary moves from "0 unpaired" to "the observable
  sector is complex" — a deeper statement of the same input.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_035_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_035_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_035_SelfConjugateMode` | self-conjugate k=N/2 is real-only (sin(πn)=0) | ✅ |
| `Y_D_035_DegenerateMultiplet` | λ=12 1-fold at 64/80/128, 5-fold at 96/192 | ✅ |
| `Y_D_035_PhaseFreedom` | 1D eigenspace is real-only; 5D group supplies phases | ✅ |
| `Y_D_035_InterferenceLoss` | real-only → classical addition (no interference) | ✅ |
| `Y_D_035_RepresentationClosure` | mult ≥ 2 for every eigenvalue at N=96 | ✅ |
| `Y_D_035_Run` | Research report | ✅ |

**Conclusion:** The self-conjugate mode must participate in a degenerate multiplet
because **complex-state observability requires every eigenvalue to have multiplicity
≥ 2**. The self-conjugate mode k=N/2 is real-only (sin(πn)=0); its eigenvalue λ=12 has a
1D real eigenspace at N=64/80/128 (an isolated singlet violating complex observability)
and a 5D eigenspace at N=96/192 (the multiplet supplies the phase/quadrature partners).
**Complete pairing (0 unpaired) is DERIVED from complex observability** — the boundary
moves one step deeper, to "the observable sector is complex." No canonical value was
changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_035"`

---

## References

- ResearchY-D_020 (selection precondition), D_032 (pairing-requirement), D_033
  (singlet-prohibition), D_034 (reciprocity).
- AT-QG: QG216 (amplitude = branching count), QG218 (Hilbert origin: complex states
  from the [magnitude, phase] pair).
- Monograph V2.0: Ch6 (D96 spectrum), Ch9 (quantum mechanics).
