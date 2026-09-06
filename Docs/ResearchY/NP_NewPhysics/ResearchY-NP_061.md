# ResearchY-NP_061 — ΩΛ Coincidence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_061 (permanent)
**Title:** ΩΛ Coincidence Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_061.md`
**Depends on:** AT-QG QG234 (ΩΛ = I_occ/ln K), ResearchY-NP_055–NP_060 (the dark-energy
arc), ResearchY-QG_018 (alternative-measure test), QG_013 (3-family window anchored by
observed ΩΛ), QG_239 (retro-selection risk), D_040 (classification registry)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_061_Tests.cs`

---

## Purpose

NP_055–060 established ΩΛ = I_occ/ln K = 0.6839 is a DERIVED information observable, and
that its energy interpretation is NOT derived. NP_061 asks the sharpest question: **why does
the derived number match the observed cosmological fraction to 0.12%?** Program: (1) remove
all energy language and treat ΩΛ purely as an information observable; (2) search whether the
match is necessary / emergent / accidental / numerological; (3) perturb occupancy, K, and
the entropy measure to determine sensitivity; (4) ask why a cosmic density fraction equals a
normalized entropy deficit. **Success criterion:** identify whether the match is A) deep
physical relation, B) correspondence, C) coincidence, D) unresolved boundary. No new
primitives; canonical AT unchanged.

---

## 1. Remove energy language — the match is between two numbers

With all energy language stripped (no "vacuum", no "energy density", no Λ), what remains is
a **numerical equality between two independently-defined quantities**:

```
DERIVED (information):   I_occ/ln K = KL(ρ‖uniform)/ln K = 0.6839
MEASURED (cosmology):    ΩΛ_obs = 0.6847 (Planck)
Deviation:               0.12%
```

The left side is a pure function of the D96 count density ρ = [4,4,87]/95. The right side is
an empirical property of the physical universe. **Nothing in the information structure
logically entails the cosmological measurement** — the equality is a fact about the
*correspondence* between the two, not a theorem.

---

## 2. Sensitivity analysis — how fragile is the 0.12% match?

### 2a. Family count K (the dominant sensitivity)

The denominator ln K is fixed by the family count K. Perturbing K away from 3:

| K | occupancy structure | ΩΛ | deviation from 0.6847 |
|---|---|---|---|
| 2 | [4, 43] | 0.5801 | −15.3% |
| **3** | **[4, 4, 87]** | **0.6839** | **−0.12%** ✅ |
| 4 | [4, 4, 4, 83] | 0.6263 | −8.5% |
| 5 | [4, 4, 4, 4, 79] | 0.5732 | −16.3% |

**Corpus-anchored rung ladder (QG_013):** the actual pairing-complete octave rungs give
ΩΛ = 0.4773 (N=48, 2 families), 0.6839 (N=96, 3 families), 0.8153 (N=192, 4 families),
0.8945 (N=384, 5 families) — adjacent rungs fail by ~20–30%. **The match exists only at
N=96 / K=3.**

### 2b. Occupancy perturbation (fixed K=3)

| occupancy | ΩΛ | deviation |
|---|---|---|
| [4, 4, 87] | 0.6839 | — |
| [5, 4, 86] | 0.6555 | −4.3% |
| [3, 5, 87] | 0.6863 | +0.2% |
| [2, 2, 91] | 0.8145 | +18.9% |

Moving even a handful of counts among the octaves shifts ΩΛ by several percent.

### 2c. Entropy measure (QG_018, unchanged)

| measure | ΩΛ | matches 0.6847? |
|---|---|---|
| **KL divergence** | **0.6839** | ✅ |
| squared Hellinger | 0.1917 | ❌ |
| total variation | 0.5302 | ❌ |
| chi-squared | 1.3896 | ❌ |

**Only KL reproduces the match.** The KL choice is EMERGENT (unique among tested), but it is
a *choice*, not a derivation.

**Determination:** the 0.12% match is a **fragile point-match** — it requires the specific
triple {KL measure, K = 3, [4,4,87] occupancy}. Perturb any one and the match degrades to
percent-level or worse. It is NOT a robust emergent feature.

---

## 3. Necessary / emergent / accidental / numerological?

| Reading | Verdict |
|---|---|
| **necessary** | **NO.** The information quantity does not logically entail the cosmological measurement; the equality needs the (non-derived) identification. |
| **emergent** | **NO.** An emergent feature should survive perturbation of its inputs; the match is fragile to K, occupancy, and measure (Section 2). |
| **accidental / numerological** | **NOT in the free-parameter sense.** No free parameter is tuned; the construction is principled (KL over the octave record). But the *precision* (0.12%) is a fortunate point-match, not robust. |
| **correspondence** | **YES.** Two independently-defined quantities coincide numerically — the honest classification. |

**The match is a CORRESPONDENCE, and specifically a fragile point-correspondence**, not a
deep derived relation and not a trivial coincidence.

---

## 4. Why should a cosmic density fraction equal a normalized entropy deficit?

The "why" has **no derived answer**. The equality holds because AT *identifies* the
information partition with the energy partition:

```
I_occ + H = ln K        (information budget — DERIVED)
      ⇕  identified with  ⇕   (the QG89 bridge, BOUNDARY — NP_059)
ΩΛ + Ωm = 1             (energy budget — flatness, OBSERVED)
```

This identification is the hosted "energy = actualization rate" definition (QG89) plus the
dimensionful anchors. **Remove the identification and there is no reason for the match.**

### The anchoring subtlety (the deepest issue)

The match's precision rests on two **non-derived** ingredients:

1. **The KL measure** is EMERGENT (unique match) but a *choice* (QG_018 OP1: "KL origin"
   is open).
2. **The K = 3 family window [4, 8)** is BOUNDARY — and QG_013 documents that this window is
   "ANCHORED by the observed cosmology (ΩΛ = 0.6839)". The very boundary input that makes the
   match work is anchored to the observation it "predicts" — a documented retro-selection
   risk (QG_239's "2 RETRO-SELECTION RISK" category).

So the 0.12% precision is **partly purchased by an anchored boundary input (K = 3) and a
chosen measure (KL)**, not obtained end-to-end from the primitives.

---

## Theorem

> **Theorem (NP_061).** The match ΩΛ = I_occ/ln K = 0.6839 vs ΩΛ_obs = 0.6847 (0.12%) is a
> CORRESPONDENCE with a BOUNDARY ingredient, not a deep physical relation and not a
> coincidence. Proof: (1) Remove energy language (Section 1): the match is a numerical
> equality between a DERIVED information number and an OBSERVED cosmological fraction.
> (2) Sensitivity (Section 2, verified): the match is a fragile point-match — perturbing K
> (2/4/5 → 0.5801/0.6263/0.5732, and the corpus rung ladder 0.4773/0.8153/0.8945), the
> occupancy ([5,4,86] → 0.6555; [2,2,91] → 0.8145), or the measure (Hellinger 0.1917, TV
> 0.5302, χ² 1.3896) degrades the match to percent-level or worse. (3) Necessity/emergence
> (Section 3): not necessary (no logical entailment), not emergent (fragile), not numerological
> (no free parameter). (4) The "why" (Section 4): the equality is the QG89 identification
> (BOUNDARY), and its precision rests on the non-derived KL measure (EMERGENT choice) and the
> non-derived K = 3 window (BOUNDARY, "anchored by observed ΩΛ" — QG_013). Classification:
> the match as a deep physical relation REFUTED (no derived bridge); the match as a
> coincidence REFUTED (principled construction, no free parameter); the match as a
> CORRESPONDENCE (fragile point-match) — with a BOUNDARY ingredient (the K = 3 window and KL
> measure are anchored/chosen, not derived). **Success criterion: the match is B
> (correspondence) with an explicit D (unresolved boundary) ingredient — its 0.12% precision
> is a fragile point-match enabled by anchored (non-derived) inputs.** No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Strip energy language. (2) Perturb K / occupancy / measure. (3) Rule out
> necessity, emergence, and numerology. (4) Locate the identification and its anchored inputs.
> ∎

---

## 5. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the match is necessary" | the information number does not entail the cosmological measurement; the equality needs the QG89 identification |
| "the match is a robust emergent feature" | it degrades under K, occupancy, and measure perturbation (Section 2) |
| "the match is pure numerology" | no free parameter is tuned; the construction is principled (KL over the octave record) |
| "the match is end-to-end derived" | the KL measure (choice) and the K = 3 window (boundary, anchored to ΩΛ_obs) are non-derived |
| "the match has no boundary ingredient" | QG_013 documents the 3-family window is "anchored by the observed ΩΛ" |

---

## 6. Falsification paths

| Claim | Falsification |
|---|---|
| the match is a fragile point-match | a robustness demonstration where ΩΛ stays within 1% under K/occupancy/measure perturbation |
| the KL measure is a choice | a derivation of the KL (log-likelihood) measure from Difference alone |
| the K = 3 window is a boundary | a derivation of the [4,8) family window from the primitives (without anchoring to ΩΛ) |
| the bridge is a boundary | a derivation of the information→energy identification (NP_058/059, still open) |

---

## 7. Classification

| Component | Status |
|---|---|
| ΩΛ = I_occ/ln K = 0.6839 (information value) | **DERIVED** (QG234) |
| the numerical match to ΩΛ_obs = 0.6847 (0.12%) | **CORRESPONDENCE** (fragile point-match) |
| the KL entropy measure (unique match) | **EMERGENT** (choice; origin open, QG_018 OP1) |
| the K = 3 family window [4,8) | **BOUNDARY** (anchored to observed ΩΛ, QG_013) |
| the information→energy identification | **BOUNDARY** (QG89, NP_059) |
| a deep physical relation | **REFUTED** (no derived bridge) |
| a pure coincidence | **REFUTED** (no free parameter) |

**Conclusion.** The 0.12% match is a **CORRESPONDENCE**, and specifically a *fragile
point-correspondence*: it holds only at the triple {KL measure, K = 3, [4,4,87] occupancy},
and degrades to percent-level under any single perturbation. It is not necessary, not robust,
and not numerology. Its precision rests on two non-derived ingredients — the KL measure
(EMERGENT choice) and the K = 3 family window (BOUNDARY, documented as "anchored by the
observed ΩΛ" in QG_013) — plus the hosted information→energy identification (QG89). **The
match is B (correspondence) with an explicit D (unresolved boundary) ingredient: a genuine,
principled numerical coincidence whose 0.12% precision is enabled by anchored inputs rather
than derived end-to-end.** No new primitive; canonical AT unchanged.

---

## 8. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_061_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_061_PureInformationMatch` | ΩΛ = I_occ/ln K = 0.6839 vs 0.6847 (0.12%) | ✅ |
| `Y_NP_061_K_Sensitivity` | K=2/4/5 shift ΩΛ by >5% | ✅ |
| `Y_NP_061_OccupancySensitivity` | [5,4,86]→0.6555; [2,2,91]→0.8145 | ✅ |
| `Y_NP_061_MeasureSensitivity` | only KL matches; Hellinger/TV/χ² fail | ✅ |
| `Y_NP_061_NecessityEmergence` | not necessary, not emergent, not numerology | ✅ |
| `Y_NP_061_WhyQuestion` | identification + anchored K=3 + chosen KL | ✅ |
| `Y_NP_061_Classification` | correspondence + boundary; deep relation & coincidence refuted | ✅ |
| `Y_NP_061_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_061"`

---

## References

- AT-QG: QG234 (ΩΛ = I_occ/ln K), QG228 (I_occ = KL(ρ‖uniform)), QG227 (initial uniform
  state), QG89 (energy = actualization rate).
- ResearchY: NP_055–NP_060 (dark-energy arc), QG_018 (alternative-measure test, KL origin
  OP1), QG_013 (3-family window anchored by observed ΩΛ), QG_239 (retro-selection risk),
  D_040 (classification registry), D_039 (95 states), D_041 (spectrum), A_003 (occupancy).
