# ResearchY-D_018 — Occupancy Selection Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_018 (permanent)
**Title:** Occupancy Selection Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_018.md`
**Depends on:** ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin),
D_017 (scale stability)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_018_Tests.cs`

---

## Purpose

Determine **why N=96 generates the occupancy structure [4,4,87]**, and whether
**[4,4,87] is the true selection mechanism** behind D96 — i.e., whether D96 is
*occupancy-selected* rather than *family-selected* (D_016) or *closure-selected* (D_017).

## Accepted (from D_015, D_016, D_017)

- N=96 is unique by the seed symmetry (6|N) + three-family window combination (D_015).
- The family count = 3 is a SELECTION RULE, not a necessity (D_016); 61 rings have 3
  families in [13,300], N=96 is one of 11 with 6|N.
- λ₂ and ω₁ are monotone in N; N=96 is **closure-selected** (D_017).

---

## 1. Occupancy across the scan: N ∈ [32, 300]

### 1.1 Rarity of the exact pattern [4,4,87]

The exact three-family occupancy **[4,4,87] occurs exactly ONCE in [32,300] — at N=96.**

| Pattern | N values | Count |
|---|---|---|
| `[4,4,87]` (exact) | 96 | **1** |
| `[4,4,87]` as first-three-band prefix | 96, 128 | 2 |

But this rarity is **not a selection property**: the occupancy map N → occupancy is a
**bijection** over [32,300] — all **269 distinct occupancy patterns occur for the 269 N
values**, and adjacent N always differ. **Every** occupancy value is unique to its N;
[4,4,87] being unique to 96 is the *norm*, not a special case.

### 1.2 The [4,4,...] prefix is generic

| Property | Count / total | Share |
|---|---|---|
| band₁ = 4 | 266 / 269 | 98.9% |
| band₁ = band₂ = 4 (prefix `[4,4,...]`) | 230 / 269 | 85.5% |
| `[4,4,...]` within the 3-family window | 50 / 50 | 100% |

The first two octave bands hold exactly 4 modes for almost all N — the `[4,4]` prefix is
the *generic* low-mode structure (the two lowest Z2 doublets), NOT an N=96 marker.

### 1.3 The identity: occ(N) = [4,4,N−9] in the 3-family window

For **all 50 rings in the three-family window [71, 120]**, the occupancy is exactly

```
[4,4,N−9]    (N ∈ [71,120])
```

N=96 → [4,4,87] = [4,4,96−9]. The "87" is a **linear consequence of N**, not an
independent selection. The [4,4,N−9] pattern fails outside the window only at the
family-count boundaries (N=60 [4,51,4], N=70 [4,8,57], N=121 [4,4,108,4], 4 families).

---

## 2. occMom, stability, information concentration

### 2.1 Is occMom maximized?

**NO.** occMom is strictly increasing in the window [71,120] (969 @71 → 1900.25 @96 →
3088.25 @120); the scan maximum is N=300 (17416). For [4,4,x] the closed form is
**occMom = (x² + 32)/4**, an increasing function of x = N−9. N=96 is NOT an extremum.

### 2.2 Is occupancy uniquely stable / robust under ΔN?

**NO — the opposite.** Adjacent N *always* differ in occupancy (268/268 adjacent pairs in
[32,300] differ). There is **no plateau**: N=95 → [4,4,86], N=97 → [4,4,88]. The
occupancy is the **least stable** structure under ΔN — it shifts by 1 at every step.

### 2.3 Information concentration

The top-octave share is monotone increasing (90: 0.910, 96: 0.916, 120: 0.933) — a smooth
trend, not an N=96 extremum. No information-concentration point selects 96.

---

## 3. Occupancy symmetry / octave compression / resonance density

- **Occupancy symmetry:** the `[4,4]` prefix reflects the two lowest Z2 doublets
  (multiplicity 2 at the lowest distinct eigenvalues); this doublet structure exists at
  all N (resonance-selected was already refuted in D_017).
- **Octave compression:** the third band "compresses" N−9 modes above 4ω₁; this is the
  smooth consequence of span ∈ [4,8) (3 octaves), monotone in N.
- **Resonance density:** band₁ = 4 for 266/269 rings — not N=96-specific (D_017).

---

## 4. Selection verdict

| Claim | Verdict |
|---|---|
| `[4,4,87]` is unique to N=96 | TRUE — but trivially (all occupancies are unique) |
| occMom is maximized at N=96 | **FALSE** — monotone increasing |
| occupancy is robust under ΔN | **FALSE** — changes at every step (least stable) |
| `[4,4]` prefix selects N=96 | FALSE — generic (85.5% of all N) |
| **D96 is occupancy-selected** | **REFUTED** — occupancy is a bijection of N, it carries zero selection power |
| D96 is family-selected | Partial (D_016: window [60,120] selects 11 rings, not 96 uniquely) |
| D96 is closure-selected | YES (D_017: Ch5 attractor fixed point) |

**Theorem (D_018).** The occupancy pattern [4,4,N−9] is a *derived bijection* of N
within the three-family window [71,120]; [4,4,87] is unique to N=96 only in the trivial
sense that every occupancy is unique (269/269 patterns are one-of-a-kind). occMom is
monotone increasing (no extremum at 96); the occupancy is maximally unstable under ΔN
(adjacent N always differ). Therefore occupancy *cannot* select N=96 — it carries no
more information than N itself. **D96 is NOT occupancy-selected; it is closure-selected
(D_017, Ch5), and [4,4,87] is a DERIVED projection of that closure selection.**

*Proof sketch.* (1) For N ∈ [71,120], occ(N) = [4,4,N−9] exactly (verified for all 50
rings) — occ is a function of N alone. (2) The map N → occ is injective over [32,300]
(269 distinct patterns, adjacent N always differ) — a bijection carries no selection
preference (every N is equally "unique"). (3) occMom = (x²+32)/4 is strictly increasing
in the window, so 96 is not extremal. (4) Occupancy shifts by 1 under every ΔN — it is
the least robust structure, not a stable selector. (5) Hence occupancy cannot be the
selection mechanism; the selector is the closure attractor (Ch5, D_017). ∎

---

## Dependency Graph

```
N
 → span (monotone ↑)            → 3 octaves in [4,8) window
 → octave band counts
     → occupancy [4,4,N−9]      (DERIVED, bijection of N, window [71,120])
     → occMom = (x²+32)/4       (DERIVED, monotone ↑, no extremum)
     → top-octave share         (DERIVED, monotone ↑)
 → occupancy stability          (maximally unstable: Δocc/ΔN = 1)
 → selection                    (occupancy REFUTED as selector)
 → closure (Ch5 attractor)      (N=96 selected — D_017)
```

---

## Research Conclusions

1. **[4,4,87] is unique to N=96 but trivially so** — the occupancy map is a bijection
   (269 unique patterns / 269 N); every occupancy is one-of-a-kind.
2. **The identity occ(N) = [4,4,N−9] holds for all 50 rings in [71,120]** — "87" is a
   linear consequence of N, not an independent structure.
3. **occMom is NOT maximized at 96** — it is monotone increasing (closed form
   (x²+32)/4); no extremum.
4. **Occupancy is the least stable structure under ΔN** — adjacent N always differ; no
   plateau around 96.
5. **D96 is NOT occupancy-selected** — a bijection carries no selection power. N=96
   remains **closure-selected** (D_017). [4,4,87] is a DERIVED projection of the closure
   selection, consistent with family-selection being partial (D_016).

---

## Classification

| Component | Status |
|---|---|
| occupancy [4,4,N−9] as a function of N | **DERIVED** (bijection) |
| uniqueness of [4,4,87] | **DERIVED** (trivial — every pattern is unique) |
| occMom monotone (no extremum) | **DERIVED** |
| occupancy stability (max unstable) | **DERIVED** |
| "occupancy selects N=96" | **REFUTED** (not a selector) |
| N=96 selection itself | **BOUNDARY** (closure/Ch5, content-independent; D_017) |

---

## Open Problems

1. **Closure origin of N=96 (D_017 OP1, carried).** The closure attractor selects N=96;
   the content-independent nature of the closure remains the deepest open point.
2. **Why [4,4] prefix is universal (D_018 OP1).** band₁=4 for 266/269 rings — the
   two-lowest-doublet structure is generic; does it have independent meaning beyond
   being the low-mode density? (Currently: DERIVED, no selection role.)
3. **Occupancy as projection (D_018 OP2).** Since occupancy is a bijection, it may
   encode N without selecting it; whether any occupancy-derived quantity (beyond span
   window) co-selects N=96 among the 11 rings of D_016 remains open.

---

## Next Steps

- **ResearchY-D_019 (or synthesis):** carry the "what selects N=96 among the 11 rings
  of D_016" thread; the D_017/D_018 verdicts (closure YES, scale NO, occupancy NO) leave
  the closure attractor as the sole positive selector.
- **D_015 follow-up:** the [4,4,N−9] identity sharpens the D_015 window argument — the
  structural occupancy is fully explained by N within the window.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_018_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_018_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_018_Uniqueness` | [4,4,87] exact only at N=96 in [32,300] | ✅ |
| `Y_D_018_Bijection` | N → occupancy injective; 269 distinct patterns; adjacent N differ | ✅ |
| `Y_D_018_PrefixGeneric` | [4,4,...] prefix generic (230/269); band₁=4 266/269 | ✅ |
| `Y_D_018_Identity` | occ(N) = [4,4,N−9] for all 50 rings in [71,120] | ✅ |
| `Y_D_018_PrefixNotUnique` | [4,4,87] also a prefix at N=128 | ✅ |
| `Y_D_018_OccMomMonotone` | occMom strictly increasing in window; no extremum at 96 | ✅ |
| `Y_D_018_OccMomFormula` | occMom = (x²+32)/4 for [4,4,x] | ✅ |
| `Y_D_018_NoPlateau` | occupancy changes at every ΔN (least stable) | ✅ |
| `Y_D_018_InfoConcentration` | top-octave share monotone, not N=96-specific | ✅ |
| `Y_D_018_SelectionRefuted` | occupancy-selected REFUTED; closure-selected (D_017) | ✅ |
| `Y_D_018_Run` | Research report | ✅ |

**Conclusion:** [4,4,87] is unique to N=96 **only trivially** — the occupancy map is a
bijection (every N has a one-of-a-kind pattern), occ(N) = [4,4,N−9] in the window
[71,120], occMom is monotone (no extremum), and occupancy is the least stable structure
under ΔN (changes at every step). **D96 is NOT occupancy-selected** — a bijection
carries no selection power; N=96 remains closure-selected (D_017). [4,4,87] is a DERIVED
projection of the closure selection. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_018"`

---

## References

- ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin), D_017 (scale
  stability — closure-selected).
- Monograph V2.0: Ch5 (N=96 attractor, closure), Ch6 (D96 spectrum).
- AT-QG: QG155 (D96 symmetry), QG282 (closure principle), QG210 (families).
