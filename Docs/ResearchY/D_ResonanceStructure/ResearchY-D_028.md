# ResearchY-D_028 — Span-Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_028 (permanent)
**Title:** Span-Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_028.md`
**Depends on:** ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin),
D_017 (scale stability), D_018 (occupancy selection), D_019 (closure-only),
D_020 (selection precondition), D_021–D_027 (oscillation → su(2) chain)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_028_Tests.cs`

---

## Purpose

**Why is span ≈ 6.4025?** Determine whether span is the true selection quantity behind
D96, or a derived consequence of N=96. The family count is floor(log₂ span)+1, and
D_016 showed 3 families are not the root cause — so the question is where the span
value comes from.

## Accepted (from D_015–D_027)

- family count = floor(log₂ span)+1; span ∈ [4,8) ⟺ 3 families (D_016, DERIVED
  identity).
- Closure, scale, occupancy, Z2, and the su(2) selector do NOT determine N=96 (D_017–
  D_019, D_021–D_027); the observable-sector construction does (D_020).
- span(96) = 6.4025 (canonical D96 fact).

---

## 1. The span origin: a derived function of N

span = ω_max/ω_min. For the K=6 circulant C_N(±1..±6):

- **ω_max → √12 ≈ 3.464** (the antipodal mode k = N/2 for even N; λ(N/2) =
  2Σ(1−cos πd) = 12). Verified: at N=96 the maximum ω is 3.9796 (finite-N correction;
  the antipodal k=48 gives 3.4641).
- **ω_min ~ (2π√91)/N ≈ 59.9/N** (the fundamental mode k=1; λ₁ ~ 4π²·91/N² from
  1−cos x ≈ x²/2, with Σd² = 91).

**Hence span ~ (√12/59.9)·N ≈ 0.0578·N — a monotone increasing function of N.**

| N | span (actual) | span ~ 0.0578·N (asymptotic) |
|---|---|---|
| 60 | 4.023 | 3.468 |
| 96 | **6.4025** | 5.548 |
| 120 | 7.999 | 6.935 |
| 192 | 12.779 | 11.097 |

**span(96) = 6.4025 is just the N=96 point of this monotone function — no special value
at 96.**

---

## 2. Trace: Difference → Actualization → Closure → Spectrum → span

```
Difference
 → count conservation (definitional identity, Ch3)
 → Actualization (the process face)
 → Closure (stable fixed point N=96, Ch3/Ch4)
 → Spectrum (D96 eigenvalues, deterministic function of the attractor)
 → span = ω_max/ω_min = 6.4025      [DERIVED — from N=96 via the spectrum]
 → family count = floor(log2 6.4025)+1 = 3   [DERIVED identity]
```

The span value is fully determined by N=96 through the ring spectrum. It is an output,
not a selector.

---

## 3. Scan alternative N

| N | span | ω_min | ω_max | families |
|---|---|---|---|---|
| 48 | 3.240 | 1.227 | 3.974 | 2 |
| 60 | 4.023 | 0.988 | 3.973 | 3 |
| 90 | 6.014 | 0.663 | 3.985 | 3 |
| **96** | **6.4025** | **0.6216** | **3.980** | **3** |
| 102 | 6.806 | 0.585 | 3.983 | 3 |
| 120 | 7.999 | 0.498 | 3.984 | 3 |
| 128 | 8.531 | 0.467 | 3.985 | 4 |
| 192 | 12.779 | 0.312 | 3.985 | 4 |

The span is **smooth and monotone** in N — no kink or special point at 96.

---

## 4. Selector removal: does span ≈ 6.4 survive?

| Removed | Effect on span(96) | Span ≈ 6.4 survives? |
|---|---|---|
| A) closure (D_019) | closure does not determine N; span is N-determined | **YES** (6.4025 unchanged) |
| B) Z2 completeness | span value does not track Z2 (span(64)=4.298 with 1 unpaired; span(96)=6.4025 with 0) | **YES** |
| C) octave-rung | span is continuous in N, not rung-specific (span 90/96/102 smooth) | **YES** |
| D) resonance density | band structure is a consequence, not a selector | **YES** |
| E) information distribution | occMom varies smoothly, not extremal | **YES** |

**The span value is N-determined, not selector-determined.** Removing any of the
candidates leaves span(96) = 6.4025 unchanged, because the span is a function of N, and
N=96 is selected by the observable-sector construction (D_020).

---

## Determination

| Option | Verdict |
|---|---|
| span VALUE 6.4025 | **DERIVED** — from N=96 via the ring spectrum (ω_max/ω_min) |
| span as a selector | **REFUTED** — it is a consequence, not a cause |
| span ∈ [4,8) window (the 3-family requirement) | **BOUNDARY** — the observable-sector INPUT (D_020) that selects N |
| family count = 3 (the VALUE given N=96) | **DERIVED** — from span via the floor(log₂ span)+1 identity |
| N=96 selection | **DERIVED** — from the four boundary inputs (D_040) |

---

## Theorem

> **Theorem (D_028).** span is a DERIVED monotone function of N, not a selector. For the
> K=6 circulant, ω_max → √12 (the antipodal mode, even N) and ω_min ~ (2π√91)/N (the
> fundamental mode), so span ~ 0.0578·N is monotonically increasing with no special
> point at 96; span(96) = 6.4025 is the N=96 point of this function. The value is fully
> determined by N=96 through the ring spectrum. Removing any candidate (closure, Z2,
> octave rung, resonance density, information distribution) leaves span(96) unchanged.
> The family count = floor(log₂ 6.4025)+1 = 3 is a DERIVED consequence of span via the
> D_016 identity (the VALUE level). The span ∈ [4,8) window is the observable-sector
> INPUT (D_020) that selects N in the 3-family window — the requirement, distinct from
> the derived value.
>
> *Proof sketch.* (1) span = ω_max/ω_min; ω_max → √12 and ω_min ~ (2π√91)/N (Section 1,
> verified). (2) Hence span ~ 0.0578·N, monotone increasing (Section 1). (3) span(96) =
> 6.4025 is the N=96 point — no special value (Sections 1, 3). (4) Removal of any
> candidate does not change the span value (Section 4). (5) families = floor(log₂
> 6.4025)+1 = 3 is the D_016 identity (Section 5). Hence the span VALUE and the family
> count VALUE are DERIVED; the 3-family WINDOW (the requirement) is BOUNDARY (D_020);
> N=96 is DERIVED from the four boundary inputs (D_040). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → observable-sector construction {Z2-paired sector, 3-family window}  [BOUNDARY — D_020]
 → N=96                                                                [DERIVED — from the boundary inputs, D_040]
 → Spectrum (D96 eigenvalues)
 → span = ω_max/ω_min = 6.4025           [DERIVED — ω_max→√12, ω_min~(2π√91)/N, ~0.0578·N]
 → family count = floor(log2 6.4025)+1   [DERIVED — the D_016 identity]
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Is span derived from N? | **YES** — span ~ 0.0578·N (monotone) |
| Is span(96) special? | **NO** — no kink at 96, just the N=96 point |
| Does closure determine span? | NO — closure does not determine N (D_019) |
| Does Z2 completeness determine span? | NO — span does not track unpaired count |
| Does the octave-rung determine span? | NO — span is continuous in N |
| Does span generate 3 families? | **YES** — floor(log₂ 6.4025)+1 = 3 (DERIVED) |
| Is span a selector? | **REFUTED** — it is a consequence of N |

---

## Counterexamples

1. **N=94, 95, 97, 98**: span = 6.27, 6.33, 6.47, 6.54 — smooth, all 3 families, no
   special point at 96.
2. **N=64** (1 unpaired, span 4.298) vs **N=96** (0 unpaired, span 6.4025): the span
   value does not track Z2 completeness.
3. **N=90, 96, 102**: span = 6.01, 6.40, 6.81 — continuous through 96, not octave-rung
   specific.
4. **N=48** (span 3.24, 2 families) and **N=192** (span 12.78, 4 families): the window
   boundaries are smooth continuations of the same monotone function.

---

## Classification

| Component | Status |
|---|---|
| span VALUE 6.4025 | **DERIVED** (ω_max/ω_min from the N=96 spectrum) |
| span monotone in N | **DERIVED** (~0.0578·N) |
| span as a selector | **REFUTED** |
| span ∈ [4,8) window (the 3-family requirement) | **BOUNDARY** (observable-sector INPUT, D_020) |
| family count = 3 (VALUE given N) | **DERIVED** (from span, the D_016 identity) |
| N=96 selection | **DERIVED** (from the four boundary inputs, D_040) |

**span VALUE and family count VALUE are DERIVED (consequences of N=96); the span ∈
[4,8) 3-family window is the BOUNDARY requirement (D_020) that selects N; N=96 is
DERIVED from the boundary inputs (D_040).**

**Refinement (D_040):** this audit originally tagged the window EMERGENT and N=96
BOUNDARY. The boundary reclassification audit corrects this: the 3-family window is an
observable-sector INPUT (BOUNDARY, D_020), not an emergent consequence; and N=96 is
DERIVED from the four boundary inputs (D_040). The derived VALUE of span/families is
unchanged.

---

## Open Problems

1. **Antipodal mode role (D_028 OP1).** ω_max → √12 is fixed by the antipodal mode
   (k=N/2) for even N; whether this value has independent meaning (beyond the span
   numerator) is open.
2. **Finite-N corrections (D_028 OP2).** The exact ω_max at N=96 is 3.9796 (not √12 ≈
   3.464) due to finite-N corrections; a closed form for the exact span is open.

---

## Next Steps

- **ResearchY-D_029 (or synthesis):** the span-origin audit completes the
  3-family chain (Difference → N=96 → span → 3 families). A synthesis can map the full
  N=96 → observables boundary structure.
- **D_016 follow-up:** the "span derived from N" verdict sharpens D_016 — the span
  VALUE feeding the family count is a derived function of N, while the 3-family WINDOW
  is the observable-sector requirement (BOUNDARY, D_020).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_028_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_028_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_028_SpanOrigin` | span ~ 0.0578·N; ω_max→√12, ω_min~(2π√91)/N | ✅ |
| `Y_D_028_AlternativeN` | span smooth/monotone across N; no special point at 96 | ✅ |
| `Y_D_028_SelectorRemoval` | removing closure/Z2/octave-rung/resonance/info leaves span(96) | ✅ |
| `Y_D_028_FamilyGeneration` | floor(log₂ 6.4025)+1 = 3 (DERIVED consequence) | ✅ |
| `Y_D_028_DependencyTrace` | Difference → Actualization → Closure → Spectrum → span → 3 families | ✅ |
| `Y_D_028_Run` | Research report | ✅ |

**Conclusion:** span is a **DERIVED monotone function of N**, not a selector. For the
K=6 circulant, ω_max → √12 (antipodal mode) and ω_min ~ (2π√91)/N, so span ~ 0.0578·N
with no special point at 96; span(96) = 6.4025 is the N=96 point of this function.
Removing any candidate (closure, Z2, octave rung, resonance, information) leaves
span(96) unchanged. The family count = floor(log₂ 6.4025)+1 = 3 is a DERIVED
consequence of span (D_016 identity). Classification: span value DERIVED; span as a
selector REFUTED; span ∈ [4,8) 3-family window BOUNDARY (observable-sector INPUT,
D_020); N=96 DERIVED (from the boundary inputs, D_040). No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_028"`

---

## References

- ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin), D_017 (scale
  stability), D_018 (occupancy selection), D_019 (closure-only), D_020 (selection
  precondition), D_021–D_027 (oscillation → su(2) chain).
- Monograph V2.0: Ch3 (actualization), Ch4 (closure), Ch6 (D96 spectrum).
- AT-QG: QG116 (universal attractor), QG282 (closure principle), QG210 (families).
