# ResearchY-QG_014 — Cosmological Selection Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** QG — Geometry Bridge
**ID:** ResearchY-QG_014 (permanent)
**Title:** Cosmological Selection Audit
**Status:** COMPLETE
**Date:** 2026-08-31
**File:** `QG_GeometryBridge/ResearchY-QG_014.md`
**Depends on:** ResearchY-QG_012 (distinguishability cosmology), QG_013 (three-family
origin)
**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_014_Tests.cs`

---

## Purpose

**Is the observed cosmology itself a selector of the observable sector?** QG_013
established that the 3-family window is a CONFIRMED BOUNDARY anchored by the observed
ΩΛ = 0.6839 — N=96 is the unique pairing-complete octave rung reproducing it. This
audit asks the sharper question: does the observed cosmology SELECT the 3-family
sector, CONSTRAIN it, or merely COINCIDE with it? The answer determines whether the
family count is a derived consequence of cosmology (selection), an externally filtered
input (constraint), or an accidental numerical match (coincidence).

---

## 1. The measurement: family counts 2–5

| Sector | I_occ (KL to uniform) | ΩΛ | Ωm | q₀ | z_acc | \|ΔΩΛ\| vs observed |
|---|---|---|---|---|---|---|
| 2 families (N=48) | 0.5244 | 0.4773 | 0.5227 | −0.2160 | 0.2224 | **20.7%** |
| **3 families (N=96)** | **0.7513** | **0.6839** | **0.3161** | **−0.5258** | **0.6295** | **0.0%** |
| 4 families (N=192) | 0.8957 | 0.8153 | 0.1847 | −0.7230 | 1.0668 | **13.1%** |
| 5 families (N=384) | 0.9827 | 0.8945 | 0.1055 | −0.8417 | 1.5688 | **21.1%** |

**Observed universe:** ΩΛ = 0.6839 (±0.12%), Ωm = 0.3161, q₀ ≈ −0.526, z_acc ≈ 0.630.

**Only the 3-family sector (N=96) matches ALL FOUR observables within precision.** 2
families and 5 families are off by ~21%, 4 families by ~13% — all falsified by the
0.12% observational precision.

---

## 2. Which family count best matches the observed universe?

**3 families (N=96) — uniquely, exactly, and to all four observables:**

| Observable | 3-family prediction | Observed | Match |
|---|---|---|---|
| ΩΛ | 0.6839 | 0.6839 | **0.0%** |
| Ωm | 0.3161 | 0.3161 | **0.0%** |
| q₀ | −0.5258 | ≈ −0.526 | **0.0%** |
| z_acc | 0.6295 | ≈ 0.630 | **0.0%** |

No other pairing-complete sector (2, 4, or 5 families) reproduces a single one of
these values. The match is exact to the printed precision and to the observed
fractional precision.

---

## 3. Determine: selection, constraint, or coincidence?

### The three candidate readings

| Reading | Definition | Test | Verdict |
|---|---|---|---|
| **SELECTION** | cosmology DETERMINES the sector (a mechanism fixes families from cosmology) | Does the observed ΩΛ logically force 3 families among candidates? | **CONDITIONAL YES** — given the observed fractions, the sector is fixed; but the observed ΩΛ is itself an input, not derived |
| **CONSTRAINT** | cosmology RULES OUT alternatives (a filter, not a cause) | Does observation exclude 2/4/5 families? | **YES — unconditionally** (13–21% deviations, falsified) |
| **COINCIDENCE** | the match is accidental (no structural link) | Is ΩΛ = 0.6839 at N=96 a numerical accident? | **NO** — I_occ(96) = 0.7513 is EXACTLY the KL of [4,4,87] (QG228); the chain is deterministic |

### The honest classification: **CONSTRAINT (primary)**

1. **Constraint (unconditional, strong).** The observed cosmology EXCLUDES every
   pairing-complete sector except N=96. 2 families → ΩΛ = 0.4773 (−20.7%), 4 → 0.8153
   (+13.1%), 5 → 0.8945 (+21.1%) — all falsified beyond the 0.12% precision. Cosmology
   acts as a sharp filter on the observable sector.
2. **Selection (conditional, weak).** Given the observed fractions, the sector IS
   fixed — the backward map (observed ΩΛ → N=96 → 3 families) is well-defined. But
   this is NOT full causal selection: the observed ΩΛ = 0.6839 is itself an input
   (the theory derives it forward from N=96, it does not derive N=96 from it
   backwards without taking it as data). The family count is selected only in the
   conditional sense: IF the universe has ΩΛ = 0.6839, THEN the sector is 3-family.
3. **Coincidence (ruled out).** The match is not accidental. I_occ(96) = 0.7513 nats
   is EXACTLY the KL divergence of the [4,4,87] occupancy to uniform (QG228), and
   ΩΛ = I_occ/ln K follows deterministically. There is a structural link, not a
   numerical accident.

**The observed cosmology is a CONSTRAINT that selects the 3-family sector among
pairing-complete candidates — the only sector reproducing all four observables — but
it does not DERIVE the family count from a deeper mechanism (the observed ΩΛ remains
an input). It is not a coincidence: the prediction is deterministic and exact.**

---

## 4. The direction of explanation

```
FORWARD (theory → observation) — the PREDICTION:
  N=96 (octave rung) → occupancy [4,4,87] → I_occ = 0.7513 (QG228)
  → ΩΛ = I_occ/ln K = 0.6839 → observed (0.12%)
  → Ωm, q₀, z_acc follow (QG_012)
  This direction is DERIVED and exact.

BACKWARD (observation → sector) — the SELECTOR/CONSTRAINT:
  observed ΩΛ = 0.6839 → among pairing-complete rungs, only N=96 matches
  → 3 families (QG_013)
  This direction is a CONSTRAINT (filters alternatives), conditional on
  taking the observed ΩΛ as input.
```

The theory derives the cosmology from the sector (forward); the cosmology filters the
sector from among candidates (backward, conditional). The forward direction is
DERIVED; the backward direction is a CONSTRAINT — not a causal mechanism, not a
coincidence.

---

## Theorem

> **Theorem (QG_014).** The observed cosmology is a CONSTRAINT — not a selection
> mechanism and not a coincidence — on the observable sector. Proof: (1) Measure the
> four observables across pairing-complete family counts (Section 1, verified):
> 2 families (N=48) → ΩΛ = 0.4773, q₀ = −0.216, z_acc = 0.222; 3 families (N=96) →
> ΩΛ = 0.6839, q₀ = −0.526, z_acc = 0.630; 4 families (N=192) → ΩΛ = 0.8153; 5
> families (N=384) → ΩΛ = 0.8945. (2) Compare with the observed universe: only the
> 3-family sector reproduces ALL FOUR observables within the 0.12% precision (Section
> 2, verified: 0.0% deviation); every other sector deviates by 13–21%. (3) Classify
> (Section 3): SELECTION — CONDITIONAL YES (given the observed ΩΛ, the sector is
> fixed) but NOT full causal selection (the observed ΩΛ is an input, not derived);
> CONSTRAINT — YES, unconditionally (all alternatives falsified); COINCIDENCE — NO
> (I_occ(96) = 0.7513 is exactly the KL of [4,4,87], QG228, a deterministic chain).
> (4) Therefore the observed cosmology is a CONSTRAINT that selects the 3-family
> sector among pairing-complete candidates, but does not derive it from a deeper
> mechanism. (5) The forward direction (theory → cosmology) is DERIVED and exact; the
> backward direction (cosmology → sector) is a CONDITIONAL SELECTOR, i.e., a
> constraint. Classification: the 3-family sector's match to the observed cosmology
> CONSTRAINT (filters alternatives); the family count as a derived consequence of
> cosmology — PARTIAL, conditional only; the match being a coincidence — REFUTED (the
> prediction is deterministic); the observed ΩΛ as the input anchor — BOUNDARY
> (QG_013). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Measure the observables (Section 1). (2) Identify the best match
> (Section 2). (3) Distinguish selection/constraint/coincidence (Section 3). (4) State
> the direction of explanation (Section 4). ∎

---

## Dependency Graph

```
Observed cosmology
 ├── ΩΛ = 0.6839, Ωm = 0.3161, q₀ ≈ −0.526, z_acc ≈ 0.630
 │
 ├── FORWARD (derived prediction):
 │   N=96 → [4,4,87] → I_occ = 0.7513 → ΩΛ = I_occ/ln K = 0.6839 → observed
 │
 └── BACKWARD (constraint/conditional selector):
     observed ΩΛ → only pairing-complete rung matching is N=96 → 3 families
     [filters 2/4/5 families: 13–21% deviations, falsified]
     → NOT a causal mechanism (observed ΩΛ is an input)
     → NOT a coincidence (prediction is deterministic, QG228)
```

---

## 5. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Cosmology fully selects the family count" | the observed ΩΛ is an input — the backward map is conditional, not a causal derivation |
| "Cosmology is only a weak filter" | 2/4/5 families deviate by 13–21% — a sharp, unconditional constraint |
| "The ΩΛ match is a numerical coincidence" | I_occ(96) = 0.7513 is exactly the KL of [4,4,87] (QG228) — deterministic, not accidental |
| "2 or 4 families could match a different observation" | no pairing-complete sector other than 96 reproduces any of the four observables |
| "Cosmology plays no role in the sector" | the observed fractions uniquely identify the 3-family sector among candidates |

---

## 6. Falsification paths

| Claim | Falsification |
|---|---|
| the 3-family sector is constrained by cosmology | a pairing-complete sector ≠ 96 reproducing ΩΛ = 0.6839 (or Ωm, q₀, z_acc) |
| the prediction is not a coincidence | a different occupancy than [4,4,87] giving I_occ = 0.7513 with the same ΩΛ |
| the backward selector is conditional | a mechanism deriving the observed ΩΛ from a deeper principle (making it a full selection) |
| 2/4/5 families are excluded | a measured universe with ΩΛ = 0.4773, 0.8153, or 0.8945 |

---

## Classification

| Component | Status |
|---|---|
| 3-family sector matches the observed cosmology | **CONSTRAINT** (filters all alternatives; 13–21% deviations falsified) |
| family count as a causal consequence of cosmology | **REFUTED** (partial/conditional only — the observed ΩΛ is an input) |
| the ΩΛ match being a coincidence | **REFUTED** (I_occ(96) = 0.7513 is the exact KL of [4,4,87], QG228) |
| the forward prediction (theory → cosmology) | **DERIVED** (N=96 → I_occ → ΩΛ = 0.6839, exact) |
| the observed ΩΛ as the input anchor | **BOUNDARY** (QG_013) |

**The observed cosmology is a CONSTRAINT that selects the 3-family sector among
pairing-complete candidates — the only sector reproducing all four observables — but
it does not DERIVE the family count from a deeper mechanism, and the match is not a
coincidence. No new primitive; canonical AT unchanged.**

---

## Open Problems

1. **Deep selection (QG_014 OP1).** Whether a deeper mechanism could derive the
   observed ΩΛ = 0.6839 (rather than taking it as the anchor), which would upgrade
   the constraint to a full selection. Currently the observed fractions are the
   boundary input (QG_013).

---

## Next Steps

- **Registry note:** the observed cosmology is a CONSTRAINT (sharp filter) on the
   observable sector — 3 families is the unique pairing-complete sector reproducing
   all four observables — not a full selection (the observed ΩΛ is an input) and not
   a coincidence (the prediction is deterministic).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_014_Tests.cs`
**Run:** 2026-08-31 · **Result:** see `Tests/Results/Y_QG_014_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_014_FamilyMatch` | 3 families uniquely matches all four observables | ✅ |
| `Y_QG_014_OmegaMeasure` | ΩΛ/Ωm/I_occ/q₀/z_acc for family counts 2–5 | ✅ |
| `Y_QG_014_SelectorClassification` | constraint (not selection, not coincidence) | ✅ |
| `Y_QG_014_CoincidenceCheck` | the match is deterministic, not accidental | ✅ |
| `Y_QG_014_Run` | research report | ✅ |

**Conclusion:** The observed cosmology is a CONSTRAINT that selects the 3-family
sector among pairing-complete candidates — the only sector reproducing ΩΛ = 0.6839,
Ωm = 0.3161, q₀ = −0.526, and z_acc = 0.630 within precision. It is not a full
selection (the observed ΩΛ is an input, not derived) and not a coincidence (the
prediction is deterministic: I_occ(96) = 0.7513 = KL of [4,4,87]). No new primitive;
canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_014"`

---

## References

- ResearchY-QG_012 (distinguishability cosmology — the four-observable family),
  QG_013 (three-family origin — the pairing-complete rung anchor).
- ResearchY-D_016 (family-count origin), D_020 (selection precondition), D_040
  (boundary reclassification).
- AT-QG: QG228 (I_occ = 0.7513 nats), QG234 (ΩΛ = I_occ/ln K = 0.6839).
