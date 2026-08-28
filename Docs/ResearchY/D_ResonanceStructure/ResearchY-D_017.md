# ResearchY-D_017 — Scale Stability Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_017 (permanent)
**Title:** Scale Stability Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `D_ResonanceStructure/ResearchY-D_017.md`
**Depends on:** ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_017_Tests.cs`

---

## Purpose

Determine **which N generates the most stable physical scale**, and whether **λ₂ and ω₁
select N=96** more fundamentally than the family count (D_016).

## Accepted (from D_015, D_016)

- N=96 is unique by the combination of the seed symmetry (6|N) and the three-family
  window (D_015).
- The family count = 3 is a SELECTION RULE, not a necessity (D_016).

---

## 1. Scan C_N(±1..±6), N ∈ [32, 300]

The spectral metrics across the scan:

| Quantity | Behavior | N=96 value |
|---|---|---|
| λ₂ | **strictly decreasing** with N (monotone) | 0.3864 |
| ω₁ = √λ₂ | strictly decreasing with N | 0.6216 |
| span | strictly increasing with N | 6.403 |
| ω₁/span | decreasing (scale density) | 0.0971 |
| λ₂/span | decreasing | 0.0603 |
| occupancies | [4,4,87] at N=96; shifts by ±1 per N step | [4,4,87] |
| occMom | varies smoothly; 1900.25 at N=96 | 1900.25 |
| family count | 3 for N ∈ [60,120] (D_016) | 3 |

**λ₂ and ω₁ are smooth, monotone functions of N** — they do NOT single out N=96. The
scale metrics change continuously with N; N=96 is not a special point for λ₂, ω₁, span,
or their ratios.

---

## 2. Tests

### Test 1: Stability under ΔN = ±1

| N | λ₂(N+1)/λ₂(N) − 1 |
|---|---|
| 64 | 0.030 |
| 96 | 0.020 |
| 128 | 0.015 |
| 192 | 0.010 |

The relative change decreases with N (monotone). N=96 is NOT the most stable — stability
improves with N. This is a trivial scaling trend (λ₂ ~ 1/N²), not an N=96-specific
property.

### Test 2: Robustness under N±1, N±2, N±6

| N | ΔN=1 (λ₂) | ΔN=6 (λ₂) |
|---|---|---|
| 64 | 0.030 | 0.161 |
| 96 | 0.020 | 0.113 |
| 128 | 0.015 | 0.087 |
| 192 | 0.010 | 0.060 |

Same monotone trend: larger N is more robust. N=96 is not special.

### Test 3: Scale persistence

The occupancy [4,4,87] persists only near N=96 (N=95 → [4,4,86]; N=97 → [4,4,88]). This
is the N=96-specific **structural** property — the scale metrics (λ₂, ω₁) persist
smoothly across all N, but the occupancy is local to N=96.

### Test 4: Minimum excitation quality

ω₁ = √λ₂ is the minimum excitation (D_009). Its value decreases smoothly with N; the
"quality" (the gap isolation) is the same Z2 doublet structure at all N (multiplicity 2).
No N=96-specific quality.

### Test 5: Information separation

occMom varies smoothly with N (1648 at N=90, 1900 at N=96, 2170 at N=102). The
[4,4,87] occupancy is the N=96-specific value (D_015), but occMom is not a stability
extremum at N=96.

### Test 6: Spectral density around ω₁

The first octave band (ω₁ to 2ω₁) has 4 modes for all N in the window (band1 = 4). The
density around ω₁ is not N=96-specific.

---

## 3. Selection mechanism

| Option | Does it select N=96? |
|---|---|
| A) family-selected | partial — the 3-family window (D_016), but it selects N ∈ [60,120] |
| B) scale-selected | **NO** — λ₂, ω₁, span are monotone in N; no special point |
| C) resonance-selected | NO — the Z2/doublet structure exists at all N |
| D) closure-selected | **YES** — N=96 is the closure fixed point of the actualization dynamics (Ch5) |

**Verdict: D) closure-selected.** The scale metrics do NOT select N=96; the closure
(attractor fixed point, content-independent convergence) does. λ₂ and ω₁ do NOT select
N=96 "more fundamentally than the family count" — both are selection rules; neither is
scale-selected.

---

## 4. Ranking table (stability score)

Stability score = the inverse of the relative λ₂ change under ΔN=1 (smaller change =
more stable):

| N | rel ΔN=1 | stability score |
|---|---|---|
| 288 | 0.0069 | 145 |
| 256 | 0.0078 | 128 |
| 224 | 0.0089 | 112 |
| 192 | 0.0103 | 97 |
| 160 | 0.0123 | 81 |
| 128 | 0.0154 | 65 |
| 96 | 0.020 | 50 |

The stability ranking is monotonically increasing with N — a trivial scaling trend
(λ₂ ~ 1/N² ⇒ rel change ~ 2/N). **N=96 is NOT the most stable; stability is not an
N=96-selecting property.**

---

## Theorem

> **Theorem (D_017).** λ₂ and ω₁ do not select N=96; the scale metrics are monotone in
> N, and the stability of the scale improves with N. N=96 is selected by closure, not by
> scale stability.
>
> *Proof sketch.* (1) λ₂ and ω₁ are strictly decreasing functions of N (verified over
> N ∈ [32, 300]) — a smooth, monotone scale (Section 1). (2) The relative change of λ₂
> under ΔN=±1 decreases monotonically with N — stability improves with N, with no
> N=96-specific extremum (Tests 1-2). (3) The occupancy [4,4,87] is the N=96-specific
> structural value (Test 3), but the scale metrics persist smoothly across all N. (4)
> Hence N=96 is not selected by scale (B), resonance (C), or family (A) — it is selected
> by closure (D), the attractor fixed point (Ch5). ∎

---

## Dependency Graph

```
N
 → λ₂ (strictly decreasing in N)
 → ω₁ = √λ₂ (strictly decreasing)
 → span (strictly increasing)
 → scale structure (monotone; no N=96-specific point)
 → occupancy [4,4,87] (N=96-specific, structural)
 → closure (N=96 selected — the attractor fixed point)
```

---

## Research Conclusions

1. **λ₂ and ω₁ are monotone in N** — the scale metrics do NOT single out N=96.
2. **Stability improves with N** (a trivial λ₂ ~ 1/N² scaling), not at N=96.
3. **The [4,4,87] occupancy is N=96-specific** (structural), but not a scale-stability
   property.
4. **N=96 is closure-selected (D)** — the attractor fixed point of the actualization
   dynamics (Ch5), not scale-selected, resonance-selected, or family-selected.
5. **λ₂ and ω₁ do NOT select N=96 more fundamentally than the family count** — both are
   monotone/selection-rule structures.

---

## Open Problems

1. **Closure-source of stability (D_017 OP1).** Why is the closure fixed point N=96,
   and does the closure endow a stability not visible in the λ₂ scan? (The attractor is
   content-independent; the closure selects it.)
2. **Occupancy localization (D_017 OP2).** The [4,4,87] occupancy is local to N=96;
   is this the scale-generating property (D_015) or a coincidence of the window?
   (Currently: structural.)
3. **Stability metric choice (D_017 OP3).** The relative-change metric ranks larger N
   higher; is there a stability metric that selects N=96? (Currently: none found.)

---

## Next Steps

- **ResearchY-D_018 (or synthesis):** the scale-stability audit (this) completes the
   scale-selection analysis; a synthesis can map the closure-selection structure.
- **ResearchY-D_015 follow-up:** the closure (Ch5) is the N=96 selector; the occupancy
   localization (OP2) connects.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_017_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_D_017_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_017_Scan` | λ₂/ω₁ monotone; occupancy [4,4,87] at N=96 | ✅ |
| `Y_D_017_DeltaNStability` | ΔN=±1 relative change decreases with N (not N=96-special) | ✅ |
| `Y_D_017_Robustness` | N±1/±2/±6 — monotone stability trend | ✅ |
| `Y_D_017_ScalePersistence` | λ₂/ω₁ persist smoothly; occupancy local to N=96 | ✅ |
| `Y_D_017_MinExcitation` | ω₁ minimum excitation; no N=96-specific quality | ✅ |
| `Y_D_017_InfoSeparation` | occMom varies smoothly; not an extremum at N=96 | ✅ |
| `Y_D_017_SpectralDensity` | band1 = 4 for all N in the window | ✅ |
| `Y_D_017_Selection` | D) closure-selected (Ch5), not scale/resonance/family | ✅ |
| `Y_D_017_StabilityScore` | stability monotonically increases with N | ✅ |
| `Y_D_017_Run` | Research report | ✅ |

**Conclusion:** λ₂ and ω₁ do NOT select N=96 — they are monotone in N, and stability
improves with N (a trivial λ₂ ~ 1/N² trend). N=96 is **closure-selected (D)**: the
attractor fixed point (Ch5). The [4,4,87] occupancy is N=96-specific (structural) but not
a scale-stability property. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_017"`

---

## References

- ResearchY-D_015 (N=96 uniqueness), D_016 (family-count origin), D_009 (minimum
  excitation).
- Monograph V2.0: Ch5 (N=96 attractor, closure), Ch6 (D96 spectrum).
- AT-QG: QG155 (D96 symmetry), QG282 (closure principle).
