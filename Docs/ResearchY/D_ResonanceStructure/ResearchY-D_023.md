# ResearchY-D_023 — SU(2) Entry Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_023 (permanent)
**Title:** SU(2) Entry Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_023.md`
**Depends on:** ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry),
D_022 (weak-isospin entry)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_023_Tests.cs`

---

## Purpose

**Where does SU(2) enter?** D_022 established that weak-isospin is an independent input
(SU(2) gauge), with only the doublet shape being an emergent spectral reading. This
audit asks the deeper question: is SU(2) itself truly independent, or can it emerge from
a deeper spectral structure?

## Accepted (from D_020, D_021, D_022)

- The oscillation Z2 and spectral Z2 are DERIVED (D_021).
- The {cos, sin} eigenspace is SO(2)-type, NOT SU(2) (D_022).
- Weak-isospin as a gauge reading is EMERGENT; the SU(2) gauge structure is an
  independent input (D_022).

---

## 1. The group structure: SO(2) vs O(2) vs SU(2)

| Group | Generators | Type |
|---|---|---|
| **SO(2)** | 1 continuous (J = [[0,−1],[1,0]]) | Abelian, rotations of {cos, sin} |
| **O(2)** | 1 continuous (J) + 1 discrete (P = diag(1,−1)) | rotations + reflections |
| **SU(2)** | 3 continuous (Pauli σₓ, σ_y, σ_z) | non-Abelian, det-1 unitaries |

**Generator counts:** the spectral structure (oscillation + reflection) provides
**ONE continuous generator** (J, the SO(2) rotation of the {cos, sin} pair) plus **one
discrete generator** (P, the reflection/parity). SU(2) requires **THREE continuous
non-Abelian generators**. **1 ≠ 3** — the spectral structure is short by two continuous
generators.

### The real 2×2 matrix algebra

The available operators on the {cos, sin} eigenspace span the 2×2 real matrices with
basis {I, J, P, JP}. Of these, only **J is skew-symmetric** (a rotation generator); P and
JP are symmetric. The real skew-symmetric 2×2 matrices are **1-dimensional** (only J).
SU(2) as a real 2×2 rep needs THREE skew-Hermitian generators (iσₓ, iσ_y, iσ_z); of
these, iσ_y = J is real (available), while iσₓ and iσ_z are **complex** — they are NOT
in the real spectral structure.

---

## 2. The D_n irreps: O(2)-type, not SU(2)

The canonical QG155 claim is that "the 2D irreducible representations of D_n generate the
Z2 doublets." This is **correct for the doublets** — the D_n 2D irreps ARE the {cos, sin}
pairs, with rotation part R(φ) (an SO(2) matrix) and reflection part P = diag(1,−1).
But these are real 2D reps of a **discrete** group (D_n), of **O(2)-type**. SU(2) is a
**continuous** group with 3 generators. D_n is a subgroup of O(2), not of SU(2). The
D_n irreps therefore generate the doublets but NOT the SU(2) gauge structure.

---

## 3. The minimal step: spectral doublet → ? → SU(2)

```
spectral doublet {cos, sin}
 → 1 continuous generator (J, SO(2)) + 1 discrete (P, O(2))
 → complexification (new input): the space becomes C², the algebra becomes sl(2,C)
 → impose unitarity + det=1 → SU(2) (3 generators)
```

The minimal step requires **complexification** — allowing complex linear combinations of
the real modes. But the ring spectrum is **real**; the complex structure is a **new
choice**, not an output of the spectral dynamics. Hence the minimal step requires an
extra input (complexification / gauge), confirming SU(2) does not emerge from the
spectral structure alone.

---

## 4. Can the three generators emerge from the spectral features?

| Candidate source | Continuous generators produced |
|---|---|
| paired modes {cos, sin} | 1 (J, the SO(2) rotation) |
| reflection symmetry | 1 discrete (P) — not continuous |
| 2D eigenspaces | 1 continuous each (J) — no new algebra |
| Z2 completeness | 0 (a pairing condition, not a generator source) |
| **Total from spectral structure** | **1 continuous** (needs 3) |

**The spectral structure cannot produce the three SU(2) generators.** The Z2
completeness is a pairing condition, not a generator source; reflection adds a discrete
generator only; the 2D eigenspaces each carry the same single SO(2) generator.

---

## 5. Remove SU(2): what observable content survives?

| Survives (spectral, DERIVED) | Lost (gauge, BOUNDARY) |
|---|---|
| oscillation quadrature pairs {cos, sin} | weak-isospin doublets (ν,e), (u,d) |
| parity/reflection structure (O(2)) | the SU(2) connection / W, Z bosons |
| Z2 doublets, families, occMom, moments | the gauge-sector reading of the doublets |
| the standing-wave model (D_001, D_002) | — |

**The spectrum survives; the gauge layer is lost.** Removing SU(2) leaves the entire
spectral content intact — the doublets, families, moments, and standing-wave structure
are all still there. What is lost is the gauge reading (weak bosons, isospin doublets),
which was the BOUNDARY attachment.

---

## Determination

| Option | Verdict |
|---|---|
| A) SU(2) independent input | **YES** — SU(2) is an independent gauge input; the spectral structure provides only 1 of the 3 generators |
| B) SU(2) emergent attachment | **PARTIAL** — the doublet SHAPE is the spectral substrate onto which the gauge structure attaches; but the SU(2) algebra is not emergent from it |
| C) SU(2) partially derived | **NO** — no spectral quantity produces the 3-generator non-Abelian algebra |

**Verdict: A) SU(2) is an independent input (BOUNDARY).** The spectral doublet provides
the attachment surface (the 2D eigenspace), and the reading is EMERGENT, but the SU(2)
gauge algebra itself is not derivable — the spectral structure is short by 2 continuous
generators and would require an extra complexification input.

---

## Theorem

> **Theorem (D_023).** SU(2) does not emerge from the spectral structure. The oscillation
> and reflection symmetries of the ring provide exactly ONE continuous generator (J, the
> SO(2) rotation of the {cos, sin} eigenspace) plus one discrete generator (P, the
> reflection) — an O(2)-type structure. SU(2) requires THREE continuous non-Abelian
> generators (Pauli σₓ, σ_y, σ_z). The real skew-symmetric 2×2 matrices are
> 1-dimensional (only J); the missing generators iσₓ and iσ_z are complex and absent from
> the real spectral structure. The D_n 2D irreps generate the Z2 doublets (correct, QG155)
> but are O(2)-type, not SU(2). Hence SU(2) is an independent input (BOUNDARY); the
> doublet is the emergent attachment surface; removing SU(2) leaves all spectral content
> intact.
>
> *Proof sketch.* (1) The {cos, sin} eigenspace transforms under SO(2) (det-1 rotations),
> generated by a single J (D_022, verified) — Section 1. (2) Reflection adds a discrete P
> (O(2)); the real skew-symmetric matrices span only {J} — Section 1. (3) SU(2) needs 3
> continuous non-Abelian generators; the spectral structure has 1 — Sections 1, 4. (4)
> The D_n 2D irreps are O(2)-type real reps of a discrete group, not SU(2) — Section 2.
> (5) The minimal step to SU(2) requires complexification — a new input — Section 3. (6)
> Removing SU(2) leaves the spectral doublets/families/moments intact — Section 5. Hence
> SU(2) is an independent input (A); the reading is EMERGENT (B), not a derivation (C).
> ∎

---

## Dependency Graph

```
oscillation (ψ = A cos(ωt + δ))
 → spectral Z2 (λ_k = λ_{N−k})            [DERIVED]
 → quadrature doublets {cos, sin}         [DERIVED — 1 generator J, SO(2)]
 → reflection (parity)                    [DERIVED — 1 discrete generator P, O(2)]
 → doublet attachment surface             [DERIVED — the 2D eigenspace]
 → ? (complexification)                   [NEW INPUT — not from the spectrum]
 → SU(2) (3 generators, non-Abelian)      [BOUNDARY — independent gauge input]
 → weak-isospin doublet reading           [EMERGENT]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Does the spectral structure provide 3 generators? | **NO** — 1 continuous (J) + 1 discrete (P) |
| Is the {cos, sin} rep SU(2)? | **NO** — it is O(2)-type (SO(2)+reflection) |
| Do the D_n 2D irreps give SU(2)? | **NO** — they are O(2)-type real reps of a discrete group |
| Is the minimal step free? | **NO** — complexification is a new input |
| What survives removing SU(2)? | all spectral content (doublets, families, moments) |

---

## Counterexamples

1. **N=32..192 with no gauge sector**: full spectral doublets + reflection, zero SU(2)
   structure — spectral content without SU(2).
2. **A non-degenerate spectrum with an SU(2) doublet written on it**: the gauge rep
   exists without the spectral degeneracy — SU(2) does not need the spectrum.
3. **D_n 2D irrep**: an O(2)-type real rep (rotation + reflection) — the doublet without
   any SU(2) algebra.
4. **Real 2×2 skew matrices**: only {J} — the missing two generators (iσₓ, iσ_z) are
   complex and outside the real spectral structure.

---

## Classification

| Component | Status |
|---|---|
| spectral doublet {cos, sin} (1 generator J) | **DERIVED** (oscillation) |
| reflection/parity (1 discrete generator P) | **DERIVED** (ring) |
| O(2)-type structure | **DERIVED** (SO(2) + reflection) |
| SU(2) gauge algebra (3 generators) | **BOUNDARY** (independent input) |
| doublet → SU(2) attachment | **EMERGENT** (the reading) |
| SU(2) from spectral structure | **REFUTED** (short by 2 generators) |

**SU(2) is A) an independent input (BOUNDARY).** The doublet is the emergent attachment
surface; the SU(2) algebra is not derivable from the spectral structure.

---

## Open Problems

1. **Complexification origin (D_023 OP1).** The minimal step to SU(2) requires
   complexification of the real modes. Whether the ring's phase structure (θ_k,
   B_003) provides a canonical complex structure is open — the phase lattice closes
   (θ_{k+N} ≡ θ_k) but its algebraic role is not the SU(2) gauge algebra.
2. **The third generator (D_023 OP2).** The spectral structure provides 1 of 3
   generators. Whether the phase/U(1) sector or the link structure can supply the
   missing 2 (as an emergent mechanism rather than an input) is open.
3. **Gauge-spectrum embedding (D_023 OP3).** Whether SU(2) can attach to the spectral
   doublets canonically (rather than as an independent sector) is open — the doublet
   shape is spectral, the gauge algebra is not.

---

## Next Steps

- **ResearchY-D_024 (or synthesis):** the SU(2)-entry audit separates the DERIVED
  O(2)-type spectral doublets from the BOUNDARY SU(2) gauge input. A synthesis can map
  the full gauge chain: oscillation → spectral Z2 → doublets (O(2)) → attachment → SU(2)
  (BOUNDARY) → weak-isospin (EMERGENT).
- **D_022 follow-up:** the generator-count argument (1 vs 3) sharpens the D_022 verdict
  — the SU(2) algebra is independent, not just the reading.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_023_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_023_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_023_SO2VsSU2` | {cos, sin} is SO(2) (1 generator); SU(2) needs 3 | ✅ |
| `Y_D_023_GeneratorCount` | spectral structure provides 1 continuous + 1 discrete; SU(2) needs 3 | ✅ |
| `Y_D_023_DoubletContent` | D_n 2D irreps are O(2)-type, not SU(2) | ✅ |
| `Y_D_023_RemovalTest` | removing SU(2) leaves spectral content intact | ✅ |
| `Y_D_023_DependencyTrace` | oscillation → Z2 → doublets → ? → SU(2): complexification new | ✅ |
| `Y_D_023_Run` | Research report | ✅ |

**Conclusion:** SU(2) does not emerge from the spectral structure. The oscillation and
reflection symmetries provide exactly ONE continuous generator (J, SO(2)) plus one
discrete (P, O(2)); SU(2) requires THREE continuous non-Abelian generators. The real
skew-symmetric 2×2 matrices are 1-dimensional; the missing generators are complex and
outside the real spectral structure. The D_n 2D irreps generate the doublets (O(2)-type)
but not SU(2). Removing SU(2) leaves all spectral content intact. **Verdict: A) SU(2) is
an independent input (BOUNDARY)**; the doublet is the EMERGENT attachment surface. No
canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_023"`

---

## References

- ResearchY-D_020 (selection precondition), D_021 (oscillation symmetry), D_022
  (weak-isospin entry).
- AT-QG: QG153 (doublet origin), QG155 (Z2 symmetry origin — D_n 2D irreps),
  QG670/680 (SU(2) spin sector — POSTULATED/REAL-UNDERIVED input).
- Monograph V2.0: Ch6 (D96 spectrum).
