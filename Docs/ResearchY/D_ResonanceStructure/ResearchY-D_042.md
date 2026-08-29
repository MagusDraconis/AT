# ResearchY-D_042 — Fundamental-Ratio Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_042 (permanent)
**Title:** Fundamental-Ratio Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_042.md`
**Depends on:** ResearchY-D_007 (Planck ratio), D_008 (reference unit), D_009
(minimum excitation), D_011 (universal reference), D_028 (span origin), D_041
(time origin)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_042_Tests.cs`

---

## Purpose

**Does D96 contain a fundamental ratio analogous to circumference/diameter = π?** For a
circle, C/D = π is the structural ratio — invariant under scaling, transcendental in
value. This audit searches the D96 spectrum for the deepest dimensionless ratio that
could serve as the natural reference for physical scales.

## Accepted (from D_007–D_041)

- π VALUE is BOUNDARY (transcendental, B_002/QG196); π's ROLE (closure) is EMERGENT.
- ω₁ = 0.6216 is the universal DIMENSIONLESS reference (D_008/D_011).
- span(96) = 6.4025 is DERIVED (~0.0578·N, the N=96 point; D_028).
- A³ = (Σm·#g·occ₂)³ = 4.8094e16 is the dimensionless Planck content (D_007).
- All physical units require anchors (D_010/D_012); the tick/time parameter is
  dimensionless (D_041).

---

## 1. The π-analogy structure

For a circle: **structure (closed ring) → ratio C/D = π**. The ratio is
- universal (the same for every circle, scale-invariant);
- transcendental in value (B_002: L is an integer matrix ⇒ D96 ratios are algebraic;
  π is transcendental — its value is NOT derivable, BOUNDARY).

For D96: **structure (C96 ring) → ratio = ?**. The candidates:

| Candidate | Value (N=96) | Meaning |
|---|---|---|
| **span = ωmax/ω₁** | **6.4025** | spectral extent ÷ fundamental — the natural frequency-scale reference |
| λmax/λ₂ | 40.99 | eigenvalue extent ÷ spectral gap |
| ω₂/ω₁ | 1.9734 | the octave (frequency doubling, D_030) |
| occMom/Σm² | 0.2106 | occupancy moment ratio |
| span/ω₁ | 10.30 | extent in fundamental units |
| A³ | 4.8094e16 | dimensionless Planck content (D_007) |

---

## 2. Which ratios are invariant under N-preserving transformations?

**N-preserving transformations** = the ring automorphisms of C96 (k → s·k mod N for
s coprime to N). These permute the eigenvalues (verified: k→5k, 7k, 11k, 13k all
preserve the spectrum multiset). Hence **every spectral ratio (span, λmax/λ₂, ω₂/ω₁,
moments) is invariant under N-preserving transformations** — they are genuine structural
invariants of the ring.

**BUT none is invariant ACROSS N**: span(60)=4.02, span(96)=6.40, span(192)=12.78 —
monotone in N (D_028). λmax/λ₂ varies non-monotonically (40.99 at 96, 41.10 at 192).
ω₂/ω₁ → 2 only in the continuum limit. **There is NO universal (N-independent) ratio
analogous to π's scale-independence.**

---

## 3. Does any ratio generate hierarchies?

| Ratio | Hierarchy | Source |
|---|---|---|
| span | family count = floor(log₂ span)+1 = 3 (D_028) | family hierarchy |
| ω₂/ω₁ ≈ 1.97 | the octave — mode doubling (D_030) | mode hierarchy |
| λmax/λ₂ = 40.99 | eigenvalue extent ÷ gap | scale hierarchy |
| A³ = 4.81e16 | dimensionless Planck content (D_007) | observable hierarchy |
| ω₁ | universal dimensionless reference (D_008/D_011) | reference hierarchy |

The ratios DO generate the mode/family/scale hierarchies — all DERIVED.

---

## 4. The inversion: π is BOUNDARY, span is DERIVED

This is the central result. The π-analogy inverts the classification:

| | π (circle) | span (D96) |
|---|---|---|
| structure | every circle | the N=96 ring (unique, D_040) |
| universality | scale-invariant (all circles) | N-specific (not across N) |
| value | transcendental (B_002) | algebraic (integer-matrix spectrum) |
| classification | **BOUNDARY** (value not derivable) | **DERIVED** (from the spectrum, D_028) |

**π is imported; span is derived.** The D96 structural ratio is BETTER than π in this
sense: it is a DERIVED algebraic constant, while π's value is an irreducible
transcendental boundary.

---

## 5. Does any ratio naturally serve as the natural reference?

**YES — span = ωmax/ω₁ = 6.4025 plays π's ROLE** (the structural ratio of the ring —
the natural reference for the frequency-scale hierarchy, D_028/D_041). But it is NOT a
universal constant like π: it is specific to the N=96 ring, and it is DERIVED rather
than BOUNDARY.

The π-analogue:

```
circle  → structure (any circle)  → ratio C/D = π   [BOUNDARY — transcendental]
D96     → structure (N=96 ring)   → ratio span = 6.4025  [DERIVED — algebraic]
```

---

## 6. Determination

| Option | Verdict |
|---|---|
| A) no universal ratio | NO — span plays the structural-ratio role |
| B) one universal ratio | PARTIAL — span is THE structural ratio but N-specific (not universal across N) |
| C) ratio family | **YES** — a family of DERIVED algebraic ratios (span, λmax/λ₂, ω₂/ω₁, A³), each with a hierarchy role |

---

## Theorem

> **Theorem (D_042).** D96 contains a fundamental ratio ANALOGOUS to π in role but
> OPPOSITE in classification. The structural ratio of the C96 ring is span = ωmax/ω₁ =
> 6.4025 — the natural dimensionless reference for the frequency-scale hierarchy
> (D_028). It is invariant under N-preserving ring automorphisms (verified: the
> spectrum multiset is preserved), so it is a genuine structural invariant. But unlike
> π — which is scale-invariant (the same for every circle) and transcendental
> (BOUNDARY value, B_002) — span is N-specific (span ~ 0.0578·N, monotone in N) and
> ALGEBRAIC (the integer-matrix spectrum), hence DERIVED. The D96 ratios generate the
> hierarchies: span → 3 families (D_028), ω₂/ω₁ ≈ 1.97 → the octave (D_030), λmax/λ₂ =
> 40.99 → the scale gap, A³ = 4.81e16 → the Planck content (D_007), ω₁ → the universal
> dimensionless reference (D_008/D_011). There is NO universal (N-independent) ratio
> analogous to π's scale-independence. Classification: span role EMERGENT (structural
> reference); span value DERIVED (algebraic); ratio family DERIVED; π value BOUNDARY
> (unchanged, B_002); universal N-invariant ratio NONE.
>
> *Proof sketch.* (1) The ring automorphisms preserve the spectrum multiset (Section 2,
> verified k→5k/7k/11k/13k) ⇒ every spectral ratio is N-preserving-invariant. (2) Across
> N, span is monotone (4.02→12.78, D_028) ⇒ no universal ratio (Section 2). (3) The
> spectrum is an integer matrix ⇒ algebraic ratios; π is transcendental ⇒ span is
> DERIVED, π is BOUNDARY (Section 4). (4) The ratios generate the hierarchies
> (Section 3). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Spectrum (D96 eigenvalues)
 → ratio family (span, λmax/λ2, ω2/ω1, A3)   [DERIVED — algebraic invariants]
 → span = ωmax/ω1 = 6.4025                   [DERIVED — the structural ratio, D_028]
    → family count = 3                       [DERIVED — D_028]
    → mode/scale hierarchies                 [DERIVED — D_030]
 → π role (closure)                          [EMERGENT — B_003]
 → π value                                   [BOUNDARY — transcendental, B_002]
 → Physics (via ω1, span, A3)                [EMERGENT — dimensionless reference]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Does D96 have a structural ratio? | **YES** — span = ωmax/ω₁ = 6.4025 |
| Is it invariant under N-preserving automorphisms? | **YES** (verified) |
| Is it universal across N (like π across circles)? | **NO** (span ~ 0.0578·N, monotone) |
| Is its value algebraic? | **YES** (integer-matrix spectrum) |
| Is its value DERIVED (vs π BOUNDARY)? | **YES** (D_028) |
| Does it generate hierarchies? | **YES** (families, modes, scale) |
| Is there a universal N-invariant ratio? | **NO** |

---

## Counterexamples

1. **span across N**: 4.02 (60), 5.35 (80), 6.40 (96), 8.00 (120), 12.78 (192) — NOT a
   universal constant; it is the N=96 point of a monotone function (D_028).
2. **λmax/λ₂**: 40.99 at N=96 but 41.10 at N=192 — non-monotone, not even ordered.
3. **ω₂/ω₁**: 1.97 at N=96, →2 only as N→∞ (continuum) — an emergent limit, not a fixed
   constant.
4. **π vs span**: π is the same for every circle (scale-invariant); span is specific to
   the N=96 ring — the analogy fails on universality but inverts on classification
   (π BOUNDARY, span DERIVED).

---

## Classification

| Component | Status |
|---|---|
| span as structural ratio (role) | **EMERGENT** (natural reference, D_028) |
| span VALUE 6.4025 | **DERIVED** (algebraic, from the spectrum) |
| ratio family (λmax/λ₂, ω₂/ω₁, A³, moments) | **DERIVED** (algebraic invariants) |
| invariance under N-preserving automorphisms | **DERIVED** |
| hierarchy generation | **DERIVED** |
| π role (closure) | **EMERGENT** (B_003, unchanged) |
| π value | **BOUNDARY** (transcendental, B_002, unchanged) |
| universal (N-invariant) ratio | **NONE** |

**The D96 structural ratio is span = 6.4025 — π's ROLE but DERIVED where π is
BOUNDARY. No universal N-invariant ratio exists. No new primitive; canonical AT
unchanged.**

---

## Open Problems

1. **Universal-ratio origin (D_042 OP1).** Whether any D96 ratio family member becomes
   N-independent in a deeper limit (beyond ω₂/ω₁ → 2) remains open.

---

## Next Steps

- **ResearchY-D_043 (or synthesis):** the fundamental-ratio audit completes the
  reference-chain (structure → ratio → hierarchy). A synthesis can map the full
  ratio-to-physics correspondence (ω₁, span, A³ → reference, families, Planck).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_042_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_042_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_042_FundamentalRatio` | span = ωmax/ω₁ = 6.4025 is the structural ratio | ✅ |
| `Y_D_042_InvariantScan` | ratios invariant under N-preserving automorphisms | ✅ |
| `Y_D_042_HierarchyGeneration` | span → families; ω₂/ω₁ → octave; A³ → Planck | ✅ |
| `Y_D_042_NStability` | no ratio is N-invariant (span monotone) | ✅ |
| `Y_D_042_PhysicsConnection` | ω₁, span, A³ connect to physics | ✅ |
| `Y_D_042_Run` | Research report | ✅ |

**Conclusion:** D96 contains a fundamental ratio — span = ωmax/ω₁ = 6.4025 — that plays
π's structural-role but is DERIVED (algebraic) where π is BOUNDARY (transcendental).
It is invariant under N-preserving ring automorphisms but NOT universal across N. The
ratio family (span, λmax/λ₂, ω₂/ω₁, A³) generates the family/mode/scale/Planck
hierarchies — all DERIVED. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_042"`

---

## References

- ResearchY-D_007 (Planck ratio), D_008 (reference unit), D_009 (minimum excitation),
  D_011 (universal reference), D_028 (span origin), D_041 (time origin).
- AT-QG: QG196/B_002 (π transcendental — BOUNDARY), D_028 (span derived).
- Monograph V2.0: Ch6 (D96 spectrum).
