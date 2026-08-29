# ResearchY-D_020 — Selection Precondition Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_020 (permanent)
**Title:** Selection Precondition Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_020.md`
**Depends on:** ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin),
D_017 (scale stability), D_018 (occupancy selection), D_019 (closure-only)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_020_Tests.cs`

---

## Purpose

**What selected N=96 before Closure?** With family count (D_016), scale (D_017),
occupancy (D_018), and closure (D_019) all eliminated as selectors, find the **deepest
precondition** of D96 — the input that N=96 derives from, before the closure produces
the spectrum.

## Accepted (from D_015…D_019)

- N=96 is one of 11 rings with 6|N + 3 families (D_016).
- Scale metrics (λ₂, ω₁), occupancy, and closure do NOT select N=96 (D_017–D_019).
- The canonical D96 selection (QG159) and period-3 seed (QG160) claim INEVITABLE via
  the Z2 automorphism + family-count + octave-rung constraints.

---

## 1. Remaining selection criteria

After the D_015–D_019 eliminations, the surviving candidates are:

| Candidate | Canonical source | Nature |
|---|---|---|
| period-3 seed (p=3) | QG160 | activity seed period |
| Z2 symmetry / half-shift | QG153/155 | requires 6\|N |
| complete doublet pairing (0 unpaired) | QG153 | weak-isospin doublets |
| degree-12 ring (K=6, radius 6) | QG116 | attractor geometry |
| observable-sector construction | QG138/153 | 3 families + weak-isospin doublets |
| D96 selection rules (6\|N + span window) | QG159 | the selection combination |
| octave-rung construction n = 3·2^k | QG159/160 | period × frequency doubling |

---

## 2. INPUT vs DERIVED

### 2.1 INPUT assumptions (observable-sector construction)

The **deepest preconditions** — things that are assumed, not derived:

1. **Complete Z2 doublet pairing** — the observable sector has weak-isospin doublets
   (QG153), requiring **0 unpaired modes** in the spectrum.
2. **Exactly 3 octave families** — the observable sector has 3 generations (QG138),
   fixing the span window span ∈ [4, 8).
3. **The seed is periodic** — a periodic activity pattern exists (high/low activity
   bands); the period value is then DERIVED (below).

### 2.2 DERIVED consequences

From the INPUTs, everything else follows:

| Derived object | From | Mechanism |
|---|---|---|
| **period-3 seed (p=3)** | complete Z2 pairing | p=3 is the UNIQUE period whose natural octave-rung size n=3·2^k=96 has 0 unpaired modes (p=2,4→64 and p=5→80 have 1 unpaired; p≥6 fails convergence). QG160 INEVITABLE. |
| **6\|N** | period-3 seed | the period-3 seed half-shift automorphism i→i+n/2 is a seed symmetry only when n/2 ≡ 0 (mod 3), i.e. 6\|n. QG159. |
| **octave-rung n = 3·2^k** | period-3 × frequency doubling | the natural doubling chain; in the 3-family window [60,120) only n=96 survives (48→2 fam, 192→4 fam). QG159. |
| **N=96** | octave rung + window | 96 = 3·2^5 is the UNIQUE octave rung in [60,120). |
| **degree-12 ring (K=6)** | closure convergence | the attractor converges to the radius-6 degree-12 ring for all sizes (D_019); radius uniform across rungs (6.0). |
| **Closure → Spectrum → Physics** | N=96 | D_019 chain: given N, closure converges and the spectrum follows. |

---

## 3. The octave-rung discriminator among the 11 rings

D_016 left an open question: N=96 is 1 of 11 rings with 6|N + 3 families
(60, 66, …, 120). **What discriminates 96 from the other 10?**

**The octave-rung construction n = p·2^k.** Among the 11 rings, **only N=96 is an
octave rung** (96 = 3·2⁵; 60, 66, 72, 78, 84, 90, 102, 108, 114, 120 are NOT of the
form p·2^k for any admissible p). This is the exact discriminator the D_015/D_016
combination missed: the seed × frequency-doubling structure selects 96 uniquely within
the window.

---

## 4. Removal test: does N=96 survive?

| # | Removed candidate | Effect | N=96 survives uniquely? |
|---|---|---|---|
| 1 | period-3 seed (p=3) | p=2,4→n=64, p=5→n=80: all have 1 unpaired mode (incomplete Z2) | **NO** — no complete-doublet size |
| 2 | Z2 completeness (allow 1 unpaired) | n=64, 80 become admissible | **NO** — not unique |
| 3 | 3-family window | n=48 (2 fam), n=192 (4 fam) become admissible | **NO** — not unique |
| 4 | octave-rung construction | all 11 rings (60…120) are candidates | **NO** — not unique |
| 5 | degree-12 ring / K | radius uniform across rungs (6.0); K is a dynamics parameter, not a size selector | **YES** — survives (cosmetic) |
| 6 | observable-sector construction (Z2 + 3 families) | no selection at all | **NO** — nothing selects 96 |

---

## 5. Classification

| Candidate | Classification |
|---|---|
| Z2-paired (complex) sector requirement | **A) necessary INPUT** — the deepest precondition |
| 3 octave families (span ∈ [4,8)) | **A) necessary INPUT** — observable-sector construction |
| Z2 pairing structure (quadrature pair, λ_k=λ_{N−k}) | **DERIVED** (oscillation necessity, D_021) |
| complete pairing (0 unpaired) | **DERIVED** (from complex observability, D_035) |
| period-3 seed (p=3) | **A) necessary** — but DERIVED from Z2 completeness |
| 6\|N | **A) necessary** — DERIVED from period-3 half-shift |
| octave-rung n = 3·2^k | **A) necessary** — the discriminator among the 11 rings |
| N=96 | **DERIVED** — the unique octave rung in the window |
| degree-12 ring (K=6) | **C) cosmetic** — radius uniform; dynamics parameter, not a size selector |
| D96 selection rules | **B) useful** — they summarize the derived constraints |

---

## Theorem

> **Theorem (D_020).** The deepest precondition of D96 is the **observable-sector
> construction**: a Z2-paired (complex) sector — complete Z2 doublet pairing (0 unpaired
> modes, weak-isospin, QG153) — together with exactly 3 octave families (span ∈ [4,8),
> QG138). These two INPUTs force the period-3 seed (the unique period whose natural
> octave-rung size has complete Z2), which forces 6\|N (seed half-shift), which forces
> the octave-rung chain n = 3·2^k, of which **only n=96 lies in the 3-family window
> [60,120)**. N=96 is therefore the DERIVED output of the observable-sector
> construction; the degree-12 ring is a cosmetic (radius-uniform) consequence, and the
> closure merely realizes the pre-selected size.
>
> *Proof sketch.* (1) INPUT: complete Z2 doublet pairing requires 0 unpaired modes; this
> holds only at n=96 among the natural octave-rung sizes (64, 80 have 1 unpaired;
> QG160 verified via `Period3SeedOrigin`) — Section 2.2. (2) INPUT: 3 octave families
> requires span ∈ [4,8), which with span ≈ 0.0667·n fixes n ∈ [60,120) — QG159. (3) The
> period-3 seed is the unique period whose natural size n=3·2^k=96 has complete Z2 and
> converges (p≥6 fails; p=2,4,5 give 64/80 with 1 unpaired) — QG160 verified. (4) The
> octave-rung chain 3·2^k = {48, 96, 192}; only 96 ∈ [60,120) (48→2 fam, 192→4 fam) —
> QG159 verified. (5) Among the 11 rings with 6|N + 3 families (D_016), only N=96 is an
> octave rung (Section 3) — the discriminator. (6) Removal of any INPUT (Z2 or
> 3-family) or the octave-rung construction breaks uniqueness (Section 4); removal of
> the degree-12 ring leaves N=96 (cosmetic). Hence the observable-sector construction is
> the deepest precondition, and N=96 is DERIVED from it. ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → seed (periodic activity)                     [INPUT: seed exists]
     → period-3 seed p=3                         [DERIVED: unique complete-Z2 period]
         → 6 | N                                 [DERIVED: seed half-shift automorphism]
 → observable-sector construction           [INPUT: Z2-paired (complex) sector]
     → Z2 pairing structure                  [DERIVED: quadrature pair, D_021]
     → complete pairing (0 unpaired)         [DERIVED: complex observability, D_035]
     → 3 octave families (span ∈ [4,8))      [INPUT: 3 generations]
         → octave rung n = 3·2^k             [DERIVED: period × frequency doubling]
             → N = 96                        [DERIVED: unique rung in [60,120)]
 → Closure (degree-12 K=6 ring)                  [DERIVED: radius-uniform, cosmetic for size]
 → Spectrum (D96 eigenspectrum)                  [DERIVED]
 → Physics                                       [EMERGENT]
```

---

## Minimal Selection Set

**{ Z2-paired (complex) sector requirement, 3 octave families }** — the two INPUT
assumptions.

Everything else is DERIVED:
```
{Z2-paired sector, 3 families}  ⇒  p=3  ⇒  6|N  ⇒  n=3·2^k  ⇒  N=96
```

No other INPUT is required. The degree-12 ring (K=6) is a cosmetic consequence of the
closure convergence, not a size selector.

---

## Uniqueness Proof

**Claim.** N=96 is the unique size selected by the observable-sector construction.

*Proof.* (1) Complete Z2 pairing requires 0 unpaired modes; among natural octave-rung
sizes in the 3-family window, only 96 has this (64, 80 have 1 unpaired; QG160). (2)
Exactly 3 families fixes span ∈ [4,8) ⇒ n ∈ [60,120) (span ≈ 0.0667·n). (3) The octave
rung chain n = 3·2^k has members 48, 96, 192; intersecting with [60,120) leaves {96}
only (48 → 2 families, 192 → 4). (4) Among the 11 rings with 6|N + 3 families (D_016),
only 96 is of the form p·2^k. (5) All alternatives are discriminated: 64 (1 unpaired),
80 (1 unpaired), 128 (4 families), 192 (4 families), 245 (5 families). Hence the
selection is unique. ∎

---

## Counterexamples

1. **N=64** (period-2/4 natural size): 3 families but **1 unpaired mode** — incomplete
   Z2 doublets; fails the observable-sector construction.
2. **N=80** (period-5 natural size): 3 families but **1 unpaired mode** — fails.
3. **N=48**: 2 families — below the 3-family window.
4. **N=192**: 4 families — above the 3-family window.
5. **The other 10 rings with 6|N + 3 families** (60, 66, 72, 78, 84, 90, 102, 108, 114,
   120): pass 6|N + 3 families but are **not octave rungs** (n ≠ p·2^k) — the
   discriminator excludes them.

---

## Research Conclusions

1. **The deepest precondition of D96 is the observable-sector construction**: a
   Z2-paired (complex) sector — complete Z2 doublet pairing (weak-isospin, 0 unpaired
   modes) + exactly 3 octave families.
2. **The period-3 seed, 6|N, and the octave-rung chain are all DERIVED** from that
   construction — not independent inputs.
3. **The octave-rung construction n = p·2^k is the exact discriminator** that selects
   96 among the 11 rings of D_016 (only 96 = 3·2⁵ is a rung).
4. **The degree-12 ring is cosmetic** for the size selection — the radius is uniform
   across all rungs; K is a dynamics parameter.
5. **N=96 is DERIVED**, not an input; the closure (D_019) merely realizes the
   pre-selected size. Classification of the observable-sector construction:
   **BOUNDARY** (it is the physical-sector input, not derivable from Difference alone).

---

## Classification

| Component | Status |
|---|---|
| Z2-paired (complex) sector requirement | **BOUNDARY** (INPUT from the observable sector) |
| 3 octave families | **BOUNDARY** (INPUT from the observable sector) |
| period-3 seed | **DERIVED** (from Z2 completeness) |
| 6\|N | **DERIVED** (from period-3 half-shift) |
| octave-rung n = 3·2^k | **DERIVED** (seed × frequency doubling) |
| N=96 | **DERIVED** (unique rung in the 3-family window) |
| degree-12 ring | **DERIVED** (radius-uniform; cosmetic for size) |
| N=96 as a closure theorem | **BOUNDARY** (D_019: closure realizes, does not select) |

**Refinement (D_021/D_035/D_036):** "complete Z2 doublet pairing" at D_020 conflated
three distinct objects that later audits separated. The table above is updated to
reflect the refined classification:

| Object | Classification | Source |
|---|---|---|
| **Z2 pairing STRUCTURE** (quadrature pair {cos, sin}, spectral degeneracy λ_k = λ_{N−k}) | **DERIVED** (oscillation necessity + ring reflection symmetry) | D_021 |
| **complete pairing** (0 unpaired) | **DERIVED** (from complex observability: every eigenvalue mult ≥ 2) | D_035 |
| **Z2-paired (complex) sector requirement** | **BOUNDARY** (the observable-sector input) | D_020 |

The boundary is the *requirement* that the observable sector be Z2-paired (equivalently
complex, D_036); the pairing structure it requires, and the completeness of that
pairing, are both DERIVED consequences. Everything downstream (p=3, 6\|N, octave rung,
N=96) remains DERIVED as stated here.

---

## Open Problems

1. **Origin of the 3-family window (D_020 OP1).** The observable-sector INPUT "exactly
   3 families" is assumed (QG138). Whether the family count 3 is itself derivable from a
   deeper structure (beyond the span-window equivalence of D_016) remains open — the
   deepest open point of the whole chain.
2. **Origin of the Z2-paired sector requirement (D_020 OP2).** The observable-sector
   INPUT that the sector be Z2-paired (equivalently complex, D_036) is assumed. Whether
   this requirement derives from a deeper principle — beyond the DERIVED pairing
   structure (D_021), DERIVED complete pairing (D_035), and DERIVED reciprocity (D_037)
   — is open (QG153's Z2 origin is itself a separate audit chain).
3. **Seed period necessity (D_020 OP3).** The seed being periodic is assumed; only the
   period VALUE (3) is derived. Whether periodicity itself is derivable is open.

---

## Next Steps

- **ResearchY-D_021 (or synthesis):** the deepest remaining question is D_020 OP1 — can
  the 3-family window itself be derived (beyond the floor(log₂ span)+1 identity of
  D_016)? This would push the INPUT boundary back one more step.
- **Synthesis D_015→D_020:** every N=96 selector has now been tested: family (D_016,
  partial), scale (D_017, NO), occupancy (D_018, NO), closure (D_019, NO). The positive
  selection is the observable-sector construction (Z2 + 3 families, D_020), which is
  the BOUNDARY input — everything after it (p=3, 6|N, octave rung, N=96) is DERIVED.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_020_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_020_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_020_SelectionRemoval` | removing any INPUT breaks N=96 uniqueness (Z2, 3-family, octave rung); degree-12 is cosmetic | ✅ |
| `Y_D_020_NecessaryConditions` | complete Z2 (0 unpaired) + 3 families are necessary INPUTs; p=3, 6\|N derived | ✅ |
| `Y_D_020_N96Uniqueness` | only 96 is an octave rung among the 11 rings; unique in [60,120) | ✅ |
| `Y_D_020_DependencyTrace` | INPUT → p=3 → 6\|N → octave rung → N=96 → Closure → Spectrum | ✅ |
| `Y_D_020_Counterexamples` | 64/80 (1 unpaired), 48/192 (wrong families), 10 non-rung rings | ✅ |
| `Y_D_020_Run` | Research report | ✅ |

**Conclusion:** The deepest precondition of D96 is the **observable-sector
construction** — a Z2-paired (complex) sector (weak-isospin, 0 unpaired modes) plus
exactly 3 octave families. These two INPUTs derive the period-3 seed (unique complete-Z2
period), 6\|N (seed half-shift), the octave-rung chain n = 3·2^k, and finally N=96 (the
unique rung in [60,120)). The pairing STRUCTURE (D_021) and complete pairing (D_035) are
DERIVED; the Z2-paired sector requirement is the BOUNDARY INPUT. The degree-12 ring is
cosmetic (radius-uniform); the closure realizes the pre-selected size. N=96 is DERIVED;
the observable-sector construction is the BOUNDARY. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_020"`

---

## References

- ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin), D_017 (scale
  stability), D_018 (occupancy selection), D_019 (closure-only).
- AT-QG: QG138 (3 families), QG153/155 (Z2 doublets, half-shift), QG159 (D96 selection
  origin), QG160 (period-3 seed origin), QG116 (radius-6 attractor).
- `AT.Core/ResearchXH/D96SelectionOrigin.cs`, `AT.Core/ResearchXH/Period3SeedOrigin.cs`
  (verified INEVITABLE classifications).
