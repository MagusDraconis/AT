# ResearchY-D_024 — Doublet Compatibility Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_024 (permanent)
**Title:** Doublet Compatibility Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_024.md`
**Depends on:** ResearchY-D_022 (weak-isospin entry), D_023 (SU(2) entry)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_024_Tests.cs`

---

## Purpose

**Why does SU(2) attach to spectral doublets?** Given that the spectral doublets are the
2D eigenspaces {cos, sin} (DERIVED, D_021–D_023) and SU(2) is an independent gauge input
(BOUNDARY, D_023), this audit asks whether the **doublet shape is uniquely compatible**
with weak-isospin — i.e., whether the 2D spectral multiplets are the only natural SU(2)
carrier, or whether the attachment is a choice.

## Accepted (from D_022, D_023)

- The spectral doublet {cos, sin} is a 2D SO(2)/O(2)-type real eigenspace (D_022).
- SU(2) is an independent input (BOUNDARY); the doublet is the EMERGENT attachment
  surface (D_023).

---

## 1. SU(2) irreducible representations

SU(2) irreps are labeled by j = 0, ½, 1, 3/2, 2, … with dimension **2j+1**:

| j | dim = 2j+1 | name | weak-isospin role |
|---|---|---|---|
| 0 | 1 | singlet | trivial (T₃ = 0) |
| 1/2 | **2** | **doublet** | **fundamental — fermions (T₃ = ±1/2)** |
| 1 | 3 | triplet | adjoint (T₃ = −1, 0, +1) |
| 3/2 | 4 | quadruplet | higher |
| 2 | 5 | quintuplet | higher |
| 5/2 | 6 | sextuplet | higher |

**Every dimension 2j+1 is an SU(2) irrep dimension.** The doublet (dim 2) is the
**smallest non-trivial** irrep, but it is not the only one.

---

## 2. The spectral multiplets of D96

D96 has eigenvalue multiplicities: **42 doublets (mult 2) + 1 five-fold group (mult 5) +
1 six-fold group (mult 6)**.

| Spectral multiplicity | SU(2) irrep dim (2j+1) | hosts |
|---|---|---|
| 2 (42 groups) | 2 | j = 1/2 (doublet) |
| 5 (1 group) | 5 | j = 2 (quintuplet) |
| 6 (1 group) | 6 | j = 5/2 (sextuplet) |

Every spectral multiplicity is an SU(2) rep dimension. The 5-fold and 6-fold groups are
NOT weak-isospin doublets — they would host j = 2 and j = 5/2 (or reducible
decompositions), neither of which is the fundamental fermion doublet.

---

## 3. Is the doublet shape uniquely compatible with weak-isospin?

**NO.** The doublet shape is **NECESSARY but NOT SUFFICIENT**:

1. **2D hosts multiple groups** — SO(2), O(2), and SU(2) all act on a 2D space
   (D_022/D_023). The spectral doublet provides the 2D carrier space, but the gauge
   group is the input.
2. **Every dimension is an SU(2) dim** — 2D is not special: a 3D spectral group would
   host j = 1 (triplet), a 5D group j = 2, a 6D group j = 5/2. Any spectral multiplet
   is an SU(2) carrier.
3. **The attachment is a choice** — weak-isospin attaches to the doublet because the
   doublet is the fundamental (j = 1/2) rep, but nothing in the spectral shape forces
   SU(2) rather than SO(2)/O(2) on the 2D space.

---

## Compatibility Table

| Spectral multiplet | SU(2) irrep | Weak-isospin compatible? | Why |
|---|---|---|---|
| singlet (1D, zero mode) | j = 0 | **NO** (trivial) | T₃ = 0 — no doublet |
| **doublet (2D)** | **j = 1/2** | **YES** (fundamental) | fermions T₃ = ±1/2 |
| triplet (3D) | j = 1 | NO | adjoint, T₃ = −1,0,+1 |
| quadruplet (4D) | j = 3/2 | NO | not the fundamental |
| 5-fold (5D) | j = 2 | NO | not the fundamental |
| 6-fold (6D) | j = 5/2 | NO | not the fundamental |

The doublet is the **only** spectral multiplet whose SU(2) irrep is the fundamental —
but the fundamental is chosen by the weak-isospin input, not forced by the doublet shape.

---

## 4. Necessity vs sufficiency

| Property | Status |
|---|---|
| 2D carrier space for weak-isospin doublets | **NECESSARY** (j = 1/2 is 2D) |
| 2D forces SU(2) | **NO** — SO(2), O(2), SU(2) all act on 2D (D_022/D_023) |
| doublet shape uniquely selects weak-isospin | **NO** — every dim 2j+1 is an SU(2) rep |
| weak-isospin attachment is forced | **NO** — it is a choice (EMERGENT) |

---

## Theorem

> **Theorem (D_024).** The doublet shape is necessary but NOT sufficient for weak-isospin.
> SU(2) irreps come in every dimension 2j+1 (j = 0, ½, 1, …); the spectral doublet (2D)
> is compatible with the fundamental j = 1/2 rep (weak-isospin fermions, T₃ = ±1/2), but
> the same 2D space also hosts SO(2) and O(2), and the D96 5-fold/6-fold groups are SU(2)
> carrier spaces too (j = 2, j = 5/2). Hence the weak-isospin attachment to doublets is
> the EMERGENT choice of the fundamental rep, not a unique consequence of the doublet
> shape.
>
> *Proof sketch.* (1) SU(2) irreps have dim 2j+1 for every half-integer j — every integer
> is a rep dimension (Section 1). (2) D96 multiplicities are 2, 5, 6 — all SU(2) dims
> (Section 2). (3) The 2D space hosts SO(2), O(2), and SU(2) (D_022/D_023) — the shape
> does not select SU(2). (4) The fundamental (j = 1/2) is the smallest non-trivial rep;
> weak-isospin fermions sit in it, but the choice of the fundamental is the gauge input,
> not the spectral shape (Sections 3–4). Hence the doublet is compatible but not unique.
> ∎

---

## Dependency Graph

```
oscillation
 → spectral Z2 (λ_k = λ_{N−k})          [DERIVED]
 → quadrature doublets {cos, sin} (2D)  [DERIVED — carrier space]
 → SU(2) rep dimensions 2j+1            [BOUNDARY — group structure]
 → j = 1/2 fundamental (2D)             [BOUNDARY — weak-isospin input]
 → weak-isospin doublet reading         [EMERGENT — the choice]
```

---

## Counterexamples

1. **A 3D spectral group** would host SU(2) j = 1 (triplet) — compatible with the
   adjoint, NOT with weak-isospin doublets. Same compatibility logic, different multiplet.
2. **The zero mode (1D)** hosts j = 0 (singlet, T₃ = 0) — trivially not weak-isospin.
3. **The D96 5-fold and 6-fold groups** are SU(2) carrier spaces (j = 2, j = 5/2) — not
   doublets; if the doublet shape were the selector, these would be excluded by
   dimension, but they are still valid SU(2) reps.
4. **The 2D eigenspace also hosts SO(2)/O(2)** (D_022/D_023) — the doublet shape does not
   force SU(2).

---

## Classification

| Component | Status |
|---|---|
| spectral doublet (2D carrier) | **DERIVED** (oscillation) |
| SU(2) rep dimensions 2j+1 | **BOUNDARY** (group structure) |
| j = 1/2 fundamental choice | **BOUNDARY** (weak-isospin input) |
| doublet → weak-isospin attachment | **EMERGENT** (the choice, not unique) |

**The doublet shape is necessary but not sufficient for weak-isospin; the attachment is
EMERGENT.**

---

## Open Problems

1. **Why the fundamental (D_024 OP1).** Weak-isospin fermions sit in the j = 1/2
   fundamental; whether the choice of the fundamental (over the triplet/adjoint or
   higher) is derivable or is a boundary input remains open (canonical: the SU(2) spin
   sector is POSTULATED, ATQG670/680).
2. **The 5/6-fold groups (D_024 OP2).** The D96 5-fold and 6-fold groups are SU(2)
   carrier spaces but carry no weak-isospin reading. Their physical role (if any) is
   open.

---

## Next Steps

- **ResearchY-D_025 (or synthesis):** the doublet-compatibility audit completes the
  weak-isospin chain (oscillation → doublets → fundamental rep → EMERGENT attachment).
  A synthesis can map the full gauge-sector boundary structure.
- **D_023 follow-up:** the "every dim is an SU(2) dim" fact sharpens D_023 — the doublet
  is the natural fundamental carrier, but the choice remains BOUNDARY.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_024_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_024_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_024_SU2Dims` | SU(2) irreps have dim 2j+1 for every half-integer j | ✅ |
| `Y_D_024_D96Multiplicities` | D96: 42×2 + 5 + 6 — all SU(2) dims | ✅ |
| `Y_D_024_DoubletCompatible` | doublet (2D) = fundamental j = 1/2 — compatible | ✅ |
| `Y_D_024_NotUnique` | 2D hosts SO(2)/O(2)/SU(2); 5/6-fold are SU(2) dims — not unique | ✅ |
| `Y_D_024_CompatibilityTable` | singlet/triplet/quadruplet/quintuplet/sextuplet NOT weak-isospin | ✅ |
| `Y_D_024_Verdict` | doublet necessary but not sufficient; attachment EMERGENT | ✅ |
| `Y_D_024_Run` | Research report | ✅ |

**Conclusion:** The doublet shape is **necessary but NOT sufficient** for weak-isospin.
SU(2) irreps come in every dimension 2j+1; the spectral doublet (2D) is compatible with
the fundamental j = 1/2 rep (weak-isospin fermions), but the same 2D space hosts SO(2)
and O(2), and the D96 5-fold/6-fold groups are SU(2) carrier spaces too. The weak-isospin
attachment to doublets is the **EMERGENT** choice of the fundamental rep, not a unique
consequence of the doublet shape. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_024"`

---

## References

- ResearchY-D_022 (weak-isospin entry), D_023 (SU(2) entry).
- AT-QG: QG153 (doublet origin), QG670/680 (SU(2) spin sector — POSTULATED input).
- Monograph V2.0: Ch6 (D96 spectrum).
