# ResearchY-D_033 — Singlet-Prohibition Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_033 (permanent)
**Title:** Singlet-Prohibition Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_033.md`
**Depends on:** ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry),
D_031 (seed-origin), D_032 (pairing-requirement)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_033_Tests.cs`

---

## Purpose

**Why is an unpaired self-conjugate mode physically forbidden?** D_032 showed complete
pairing (0 unpaired) is the observable-sector boundary input. This audit asks whether
the prohibition of an unpaired mode follows from a deeper principle.

## Accepted (from D_020, D_021, D_031, D_032)

- The pairing STRUCTURE is DERIVED (oscillation quadrature pairs, D_021).
- The COMPLETENESS (0 unpaired) is BOUNDARY (observable sector, D_032).
- N=96 is selected by complete pairing (D_020).

---

## 1. The self-conjugate mode: paired vs unpaired

| | Paired (N=96) | Unpaired (N=64) |
|---|---|---|
| self-conjugate k | 48 | 32 |
| λ(N/2) | 12 (5-fold group) | 12 (isolated) |
| sin quadrature | provided by the group (k=16,32,64,80) | **vanishes (sin(πn)=0)** |
| mirror partner | degenerate partners | **none (mirror maps k to itself)** |

---

## 2. What the singlet lacks

| Structure | Paired mode | Unpaired singlet |
|---|---|---|
| **spatial phase freedom** | ψ = [a·cos_k + b·sin_k]cos(ωt) — full | only cos_k — no sin spatial harmonic |
| **reciprocity** | k pairs with N−k (distinct partner) | the mirror maps k=N/2 to ITSELF — no partner |
| **representation closure** | 2D eigenspace (doublet) | 1D eigenspace (no doublet) |
| **weak-isospin attachment** | the doublet reading (D_022) | no doublet for the SU(2) fundamental |
| **normalization** | survives | **survives** (the Fourier basis is complete with or without the singlet) |

---

## 3. Is the singlet mathematically allowed?

**YES.** The mode cos(πn) = (−1)ⁿ is a perfectly valid eigenfunction: verified
**L·cos₃₂ = 12·cos₃₂ at N=64** (λ(N/2) = 12, all sampled sites). The singlet is not
mathematically excluded.

---

## 4. Is the singlet physically allowed?

**As an oscillator: YES** — cos(ωt) is a valid oscillation.

**As a member of the observable sector: NO** — if the observable sector is the
weak-isospin doublet structure (D_020/D_022), a lone mode has no doublet partner and
cannot carry the doublet reading.

---

## 5. Is the singlet structurally inconsistent?

**For the observable sector: YES.** The singlet breaks the
**"every mode has a mirror partner / full quadrature"** structure. If the observable
sector IS the doublet structure, a singlet is a structural inconsistency for that
sector.

---

## 6. The deeper principle

Four candidate principles all point to the same structure:

| Principle | Content |
|---|---|
| no isolated oscillator | every oscillator belongs to a reciprocating mirror pair |
| complete reciprocity | every mode has a distinct partner |
| complete phase structure | every frequency has both quadratures |
| complete representation structure | every frequency is a 2D rep (doublet) |

**All four reduce to: the observable sector is a RECIPROCAL PAIR STRUCTURE (no lone
modes).**

---

## 7. Remove the complete-pairing rule: what survives, what breaks?

| Removed | Survives | Breaks first |
|---|---|---|
| complete pairing | the spectral content (families, moments, span); normalization | **reciprocity** — one frequency becomes a lone mode with no mirror partner and no doublet |

The first thing to break is the **reciprocity of the observable sector**.

---

## Theorem

> **Theorem (D_033).** An unpaired self-conjugate mode is mathematically allowed but
> physically excluded by the observable-sector structure. The singlet cos(πn) = (−1)ⁿ is
> a valid eigenfunction (L·cos = 12·cos verified at N=64) — it is not mathematically
> forbidden. But it breaks reciprocity (the Z2 mirror maps k=N/2 to itself, giving no
> distinct partner), the spatial phase structure (no sin harmonic), the representation
> structure (no 2D doublet), and the weak-isospin attachment. Normalization survives (the
> Fourier basis is complete with or without the singlet). The prohibition is the
> observable-sector requirement that the sector be a RECIPROCAL PAIR structure ("no
> isolated oscillator") — BOUNDARY (D_020). The reciprocity/phase/representation closures
> are DERIVED consequences of the pairing.
>
> *Proof sketch.* (1) The singlet is a valid eigenfunction (Section 3, verified L·cos₃₂ =
> 12·cos₃₂). (2) Its mirror maps k=N/2 to itself — no distinct partner (Section 2). (3)
> It has no sin spatial harmonic, no 2D rep, no weak-isospin attachment (Sections 2, 4).
> (4) Normalization survives (the basis is complete regardless) (Section 2). (5) The
> prohibition is the observable-sector requirement of a reciprocal pair structure
> (Section 6) — BOUNDARY (D_020). Hence the singlet is mathematically allowed but
> physically excluded; the closures are DERIVED consequences. ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → observable sector (reciprocal pair structure)   [BOUNDARY — D_020]
 → complete pairing (0 unpaired)                    [BOUNDARY — the requirement]
 → no isolated oscillator                           [EMERGENT — from the paired sector]
 → reciprocity closure                              [DERIVED — mirror pairing]
 → phase/representation closure                     [DERIVED — quadrature/doublet]
 → N=96 (selected by complete pairing)              [DERIVED]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is the singlet mathematically allowed? | **YES** (valid eigenfunction, L·cos = 12·cos) |
| Does the singlet break normalization? | **NO** (the Fourier basis is complete) |
| Does the singlet break reciprocity? | **YES** (no mirror partner) |
| Does the singlet break phase structure? | **YES** (no sin spatial harmonic) |
| Does the singlet break representation closure? | **YES** (no 2D doublet) |
| Does the singlet break weak-isospin attachment? | **YES** (D_022) |
| Is the prohibition derived? | NO — it is the observable-sector requirement (BOUNDARY) |
| Is "no isolated oscillator" derived? | **EMERGENT** (from the paired observable sector) |

---

## Counterexamples

1. **N=64**: the singlet k=32 is a valid eigenfunction (L·cos₃₂ = 12·cos₃₂) — it is NOT
   mathematically forbidden; the prohibition is structural.
2. **A lone oscillator cos(ωt)**: a valid oscillation — the singlet is only excluded as
   a member of the doublet observable sector.
3. **Normalization at N=64**: the Fourier basis is complete (63 positive + zero = 64) —
   the singlet does not break normalization.
4. **N=192**: complete pairing like N=96, but 4 families — completeness alone does not
   select 96.

---

## Classification

| Component | Status |
|---|---|
| singlet mathematically allowed | **DERIVED** (valid eigenfunction) |
| reciprocity closure (mirror pairing) | **DERIVED** (spectral) |
| phase structure closure (quadrature) | **DERIVED** (oscillation, D_021) |
| representation closure (doublet) | **DERIVED** (pairing) |
| "no isolated oscillator" principle | **EMERGENT** (from the paired observable sector) |
| prohibition of the singlet | **BOUNDARY** (observable-sector requirement, D_020) |

**The singlet is mathematically allowed but physically excluded; the prohibition is the
BOUNDARY observable-sector requirement, with the closures DERIVED.**

---

## Open Problems

1. **Reciprocity origin (D_033 OP1).** Why the observable sector must be a reciprocal
   pair structure (weak-isospin doublets) — the D_014/D_022 boundary question.
2. **Degenerate-group structure (D_033 OP2).** The singlet is avoided by the 5-fold
   group at N=96; the physical role of the higher (5/6-fold) degeneracies beyond the
   doublet is open.

---

## Next Steps

- **ResearchY-D_034 (or synthesis):** the singlet-prohibition audit completes the
  pairing chain (Difference → observable sector → reciprocal pair structure → N=96). A
  synthesis can map the full observable-sector boundary.
- **D_032 follow-up:** the "no isolated oscillator" principle sharpens D_032 — the
  completeness requirement is the reciprocity of the observable sector.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_033_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_033_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_033_SingletMode` | singlet is a valid eigenfunction (L·cos = 12·cos at N=64) | ✅ |
| `Y_D_033_PairedMode` | paired mode has the full quadrature (cos, sin) | ✅ |
| `Y_D_033_PhaseFreedom` | singlet lacks the sin spatial harmonic | ✅ |
| `Y_D_033_RepresentationClosure` | singlet is 1D (no doublet); paired is 2D+ | ✅ |
| `Y_D_033_Observability` | singlet excluded by the doublet observable sector | ✅ |
| `Y_D_033_DependencyTrace` | Difference → observable sector → reciprocal pairs → N=96 | ✅ |
| `Y_D_033_Run` | Research report | ✅ |

**Conclusion:** An unpaired self-conjugate mode is **mathematically allowed but
physically excluded**. The singlet cos(πn) = (−1)ⁿ is a valid eigenfunction (verified
L·cos₃₂ = 12·cos₃₂ at N=64). It breaks reciprocity (no mirror partner), phase structure
(no sin harmonic), representation closure (no doublet), and weak-isospin attachment;
normalization survives. The prohibition is the observable-sector requirement of a
**RECIPROCAL PAIR structure** ("no isolated oscillator") — **BOUNDARY** (D_020). The
reciprocity/phase/representation closures are DERIVED consequences of the pairing. No
canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_033"`

---

## References

- ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry), D_031
  (seed-origin), D_032 (pairing-requirement).
- Monograph V2.0: Ch3 (actualization), Ch4 (closure), Ch6 (D96 spectrum).
- AT-QG: QG153 (doublet origin), QG159/160 (D96 selection, period-3 seed).
