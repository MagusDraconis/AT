# ResearchY-D_015 — N=96 Uniqueness Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_015 (permanent)
**Title:** N=96 Uniqueness Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_015.md`
**Depends on:** ResearchY-D_009 (minimum excitation), D_010 (unit anchoring),
D_011 (universal reference)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_015_Tests.cs`

---

## Purpose

Determine **what property makes N=96 unique** among alternative closure sizes, and which
selection mechanism (resonance, closure, symmetry, family window, or a combination)
selects it.

---

## 1. Comparison across closure sizes

The spectral properties of the circulant C_N(±1..±6) for the candidate sizes:

| N | λ₂ | ω₁ | Z2 pairs | families | occupancy | span |
|---|---|---|---|---|---|---|
| 64 | 0.8596 | 0.9272 | 31 | 3 | [4, 39, 20] | 4.298 |
| **96** | **0.3864** | **0.6216** | **47** | **3** | **[4, 4, 87]** | **6.403** |
| 128 | 0.2182 | 0.4671 | 63 | 4 | [4, 4, 87, 32] | 8.531 |
| 192 | 0.0972 | 0.3118 | 95 | 4 | [4, 4, 8, 175] | 12.779 |
| 245 | 0.0598 | 0.2445 | 121 | 5 | [4, 4, 8, 212, 16] | 16.301 |

**N=96 is the only size with exactly 3 families AND the canonical occupancy [4,4,87]**
(span 6.403, in the [4, 8) three-family window).

---

## 2. Measurements

| Measure | N=96 | Unique? |
|---|---|---|
| λ₂ (spectral gap) | 0.3864 | not unique (decreases with N) |
| ω₁ (minimum excitation) | 0.6216 | not unique (decreases with N) |
| Z2 pairing | 47 pairs | scales with N (N/2 − 1 pairs) |
| family count | 3 | shared with N=64; lost at N≥128 |
| occupancy [4,4,87] | canonical | **unique** among the tested sizes |
| closure stability | stable fixed point | canonical (Ch5) |

The *scale* properties (λ₂, ω₁, Z2) are not unique — they shift with N. The *structural*
properties (family count 3, occupancy [4,4,87]) are what distinguish N=96.

---

## 3. Selection mechanism

| Mechanism | Does it select N=96? |
|---|---|
| A) resonance | partial — the standing-wave structure exists for all N |
| B) closure | partial — the fixed point is content-independent, but the size is the selection question |
| C) symmetry | the period-3 seed (6\|N) + Z2 half-shift: N=96 passes; N=64/128/245 fail (not divisible by 6); N=192 passes but has 4 families |
| D) family window | span < 8 (3 families): N=96 passes; N=128/192/245 fail (4-5 families) |
| **E) combination** | **the unique size satisfying both the seed symmetry (6\|N) and the three-family window (span < 8)** |

**Verdict: E) combination.** N=96 is selected by the *combination* of the period-3 seed
symmetry (6 divides N) and the three-family octave window (span in [4, 8)). This is the
canonical D96 selection (QG159/QG160).

---

## 4. If N changes, which derived structures disappear?

| N | Lost structures |
|---|---|
| N=64 | the [4,4,87] occupancy (→ [4,39,20]); fails the period-3 seed (64 mod 6 = 4) |
| N=128 | the three-family window (→ 4 families); fails the seed (128 mod 6 = 2) |
| N=192 | the three-family window (→ 4 families); passes the seed but loses the window |
| N=245 | the three-family window (→ 5 families); fails the seed (245 mod 6 = 5) |

The structures that **depend on N=96**: the three-family octave structure and the
canonical occupancy [4,4,87]. The family count, the occMom, and the spectral moments
that feed the sector assignment (D_004–D_006) change with N.

---

## 5. Scale-generating properties unique to N=96

N=96 uniquely generates the **three-family + [4,4,87]** structure:

- The span 6.403 sits in the [4, 8) window — exactly three octave families (D_004).
- The occupancy [4,4,87] is the canonical spectral content feeding occMom = 1900.25,
  the moments, and the sector assignment (D_003–D_006).
- N=96 is the unique tested size with both the period-3 seed (6|N) and the three-family
  window — the "scale-generating" property is the combination (E).

---

## Theorem

> **Theorem (D_015).** N=96 is the unique closure size in the tested class satisfying
> both the period-3 seed symmetry (6|N) and the three-family octave window (span in
> [4, 8)), which generates the canonical three-family [4,4,87] structure.
>
> *Proof sketch.* (1) The seed symmetry requires 6|N: N=96 and N=192 pass; N=64, 128,
> 245 fail (Section 3C). (2) The three-family window requires span < 8: N=96 (span
> 6.403) passes; N=192 (span 12.779) fails (Section 3D). (3) Hence N=96 is the unique
> tested size in the intersection (Section 3E). (4) N=96 generates the canonical
> occupancy [4,4,87] (Section 5), which feeds the moments and the sector structure
> (D_003–D_006). ∎

---

## Dependency Graph

```
N (=96)
 → λ₂ (=0.3864, the spectral gap)
 → ω₁ (=0.6216, the minimum excitation)
 → moments (Σm, Σ√m, Σm², occMom)
 → physics (families, sectors, couplings, observables)
```

The scale-generating chain runs through the D96 spectrum; the uniqueness of N=96 is the
combination of the seed symmetry and the family window.

---

## Uniqueness Proof

**Claim:** N=96 is the unique closure size in the tested class {64, 96, 128, 192, 245}
with the three-family canonical structure.

*Proof.* (1) The three-family window (span < 8) is satisfied by N=64 (span 4.298) and
N=96 (span 6.403), but not by N=128 (8.531), N=192 (12.779), or N=245 (16.301)
(Section 1). (2) The period-3 seed symmetry (6|N) is satisfied by N=96 and N=192, but
not by N=64 (64 mod 6 = 4), N=128 (128 mod 6 = 2), or N=245 (245 mod 6 = 5). (3) The
intersection of the two constraints is {96} alone (N=192 passes the seed but fails the
window; N=64 passes the window but fails the seed). (4) N=96 uniquely yields the
occupancy [4,4,87]. Hence N=96 is unique. ∎

---

## Research Conclusions

1. **The scale properties (λ₂, ω₁, Z2 pairs) are NOT unique to N=96** — they shift
   smoothly with N.
2. **The structural properties ARE unique:** exactly 3 families and the canonical
   occupancy [4,4,87].
3. **N=96 is selected by the combination (E)** of the period-3 seed symmetry (6|N) and
   the three-family octave window (span < 8).
4. **If N changes, the three-family structure and the [4,4,87] occupancy disappear**
   (N=64 loses the occupancy; N≥128 loses the family window).
5. **The scale-generating property unique to N=96 is the combination** of the seed
   symmetry and the family window.

---

## Open Problems

1. **Global uniqueness (D_015 OP1).** The tested class {64, 96, 128, 192, 245} is finite;
   a global proof over all N is open (Ch5 "Exact status" — the accepted boundary).
2. **Scale-generating mechanism (D_015 OP2).** Does the [4,4,87] occupancy follow from
   the combination alone, or from additional structure? (It follows from the span
   window + the circulant spectrum.)
3. **Family-window robustness (D_015 OP3).** Is the [4, 8) window the unique natural
   choice for three families? (It is the octave-band definition; alternatives are open.)

---

## Next Steps

- **ResearchY-D_016 (or synthesis):** the N=96 uniqueness audit (this) completes the
   closure-size analysis; a synthesis can map the full selection structure.
- **ResearchY-B_001 follow-up:** the closure and the ring geometry connect to the
   N=96 selection.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_015_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_015_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_015_Comparison` | N=64/96/128/192/245 properties | ✅ |
| `Y_D_015_SpectralMeasures` | λ₂/ω₁/Z2/families/occupancy/span per N | ✅ |
| `Y_D_015_SelectionMechanism` | E) combination: seed symmetry + family window | ✅ |
| `Y_D_015_StructureLoss` | N≠96 loses 3-family / [4,4,87] | ✅ |
| `Y_D_015_ScaleGenerating` | N=96 unique 3-family + [4,4,87] | ✅ |
| `Y_D_015_Run` | Research report | ✅ |

**Conclusion:** N=96 is unique in the tested class by the combination (E) of the
period-3 seed symmetry (6|N) and the three-family octave window (span in [4,8)), which
generates the canonical [4,4,87] structure. The scale properties (λ₂, ω₁, Z2) are not
unique; the structural properties are. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_015"`

---

## References

- ResearchY-D_009 (minimum excitation), D_011 (universal reference), D_004 (family
  structure), D_003 (resonance observables).
- Monograph V2.0: Ch5 (N=96 attractor), Ch6 (D96 spectrum).
- AT-QG: QG159 (D96 selection), QG160 (period-3 seed), QG210 (family index).
