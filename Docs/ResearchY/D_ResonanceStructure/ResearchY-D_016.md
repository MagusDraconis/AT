# ResearchY-D_016 — Family-Count Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_016 (permanent)
**Title:** Family-Count Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_016.md`
**Depends on:** ResearchY-D_015 (N=96 uniqueness)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_016_Tests.cs`

---

## Purpose

Audit the origin of the family-count mechanism

```
families = floor(log₂ span) + 1
```

and determine whether N=96, 6|N, span ∈ [4,8), and family count = 3 are **derived
necessities** or **selection rules**.

---

## 1. Remove the N=96 assumption: scan admissible D_N rings

Scanning the circulant rings C_N(±1..±6) for N ∈ [13, 300]:

| Quantity | Count | Notes |
|---|---|---|
| N with 3 families | 61 (N ∈ [60, 120]) | the 3-family window |
| N with 6|N AND 3 families | 11 (60, 66, 72, 78, 84, 90, 96, 102, 108, 114, 120) | the seed × window intersection |
| N=96 | 1 of 11 | NOT unique for the family count |

**N=96 is NOT the unique 3-family ring.** Many N give 3 families; N=96 is one member of
the 3-family window.

---

## 2. Test N mod 6

The period-3 seed (6|N) selects N ∈ {60, 66, …, 120} among the 3-family rings — but
**3-family rings exist WITHOUT 6|N** (e.g., N = 61, 62, 63, 64, 65 all give 3 families).
Divisibility by 6 is not necessary for 3 families.

---

## 3. Test alternative spans

The family count is a monotone function of the span:

```
span ∈ [2, 4)  → 2 families
span ∈ [4, 8)  → 3 families
span ∈ [8, 16) → 4 families
```

The 3-family window is span ∈ [4, 8) — a *choice* of window, not a necessity. N=96
(span 6.403) sits inside it, as do N=60 (span 4.023) through N=120 (span 7.999).

---

## 4. Determine when family count = 3

**family count = 3 ⟺ span ∈ [4, 8)** — a mathematical identity from the definition
floor(log₂ span)+1. The equivalence is DERIVED; the *choice* of the 3-family window (vs
2 or 4) is a SELECTION RULE.

---

## 5. Search for counterexamples

| Counterexample | Shows |
|---|---|
| N=64 (3 families, span 4.298, 6∤64) | 3 families without the seed |
| N=90 (3 families, span 6.014, 6|90) | another 3-family ring with the seed |
| N=128 (4 families, span 8.531) | the window boundary (4 families at span ≥ 8) |
| N=120 (3 families, span 7.999) | the upper edge of the 3-family window |
| N=61..119 (3 families, various spans) | 3 families is a continuum, not N=96-specific |

---

## 6. Classification

| Item | Status |
|---|---|
| A) N=96 | **SELECTION RULE** — one admissible ring, not uniquely forced by the family count |
| B) divisibility by 6 | **SELECTION RULE** — a chosen seed symmetry; not necessary for 3 families |
| C) span ∈ [4, 8) | **DERIVED** — the mathematical equivalence with family count = 3 |
| D) family count = 3 | **SELECTION RULE** — the choice of the 3-family window, not a necessity |

---

## Theorem

> **Theorem (D_016).** The family count = 3 is a selection rule, not a derived necessity;
> N=96 is one admissible ring among many.
>
> *Proof sketch.* (1) Scanning C_N(±1..±6) for N ∈ [13, 300] finds 61 rings with 3
> families (N ∈ [60, 120]) (Section 1). (2) family count = 3 ⟺ span ∈ [4, 8) is a
> mathematical identity (DERIVED equivalence), but the choice of the 3-family window is a
> selection (Section 4). (3) Divisibility by 6 is not necessary: N=61..65 give 3
> families without 6|N (Section 2). (4) N=96 is one of 11 rings with both 6|N and 3
> families (Section 1). Hence N=96, 6|N, and family count = 3 are selection rules; only
> the span-window equivalence is derived. ∎

---

## Dependency Graph

```
N (ring size)
 → span (ω_max/ω_min)
 → families = floor(log₂ span) + 1   [DERIVED equivalence]
 → family count = 3 ⟺ span ∈ [4,8)   [DERIVED]
     → the 3-family window           [SELECTION RULE]
         → N=96 (one admissible ring) [SELECTION RULE]
```

---

## Necessity Analysis

| Item | Necessary? | Why |
|---|---|---|
| span ∈ [4,8) ⟺ 3 families | YES (equivalence) | mathematical identity |
| N=96 for 3 families | NO | 61 rings give 3 families |
| 6|N for 3 families | NO | 3-family rings exist without it |
| 3-family window chosen | no (a choice) | 2/4/5-family windows are alternatives |

---

## Research Conclusions

1. **The family-count mechanism does not single out N=96.** Scanning finds 61 rings with
   3 families (N ∈ [60, 120]).
2. **family count = 3 ⟺ span ∈ [4, 8)** is a DERIVED mathematical equivalence.
3. **The choice of the 3-family window is a SELECTION RULE** (2/4-family windows are
   alternatives).
4. **Divisibility by 6 is a SELECTION RULE** — not necessary for 3 families.
5. **N=96 is one of 11 rings** with both the seed (6|N) and 3 families; it is selected
   by the additional D96 criteria (period-3 seed, Z2 half-shift, unique octave rung,
   D_015), not by the family count alone.

---

## Open Problems

1. **N=96 selection within the window (D_016 OP1).** Among the 11 rings with 6|N and 3
   families, what selects N=96 specifically? (D_015: the tested-class uniqueness; a
   global selection principle is open.)
2. **Family-window choice (D_016 OP2).** Why 3 families (span < 8) rather than 2 or 4?
   (Currently: the observed 3-family structure is the selection.)
3. **Global scan (D_016 OP3).** The scan covers [13, 300]; a full N→∞ analysis is open.

---

## Next Steps

- **ResearchY-D_017 (or synthesis):** the family-count origin audit (this) completes the
   family-structure analysis; a synthesis can map the selection-rule structure.
- **ResearchY-D_015 follow-up:** the N=96 selection within the 3-family window (OP1).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_016_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_016_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_016_RingScan` | 61 rings with 3 families (N∈[60,120]) | ✅ |
| `Y_D_016_Mod6` | 6|N not necessary for 3 families | ✅ |
| `Y_D_016_SpanScan` | span ∈ [4,8) ⟺ 3 families (identity) | ✅ |
| `Y_D_016_ThreeFamilyCondition` | family count = 3 ⟺ span ∈ [4,8) | ✅ |
| `Y_D_016_Counterexamples` | N=64, 90, 120 give 3 families; N=128 gives 4 | ✅ |
| `Y_D_016_Classification` | A/B/D SELECTION RULE; C DERIVED | ✅ |
| `Y_D_016_Run` | Research report | ✅ |

**Conclusion:** family count = 3 is a SELECTION RULE, not a derived necessity — the
span-window equivalence is DERIVED, but the choice of 3 families and of N=96 (one of 11
admissible rings with 6|N) are selection rules. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_016"`

---

## References

- ResearchY-D_015 (N=96 uniqueness), D_004 (family structure).
- Monograph V2.0: Ch5 (N=96 attractor, "Exact status" boundary), Ch6 (D96 spectrum).
- AT-QG: QG159 (D96 selection), QG160 (period-3 seed), QG210 (family index).
