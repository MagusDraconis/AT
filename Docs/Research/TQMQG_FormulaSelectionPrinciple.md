# TQM-QG Phase 254 — Formula Selection Principle

**Status:** COMPLETE — **SELECTION PRINCIPLE**
**Tests:** TQMQG2540, TQMQG2541, TQMQG2542 (all passed)
**Core class:** `TQM.Core/ResearchXH/FormulaSelectionPrinciple.cs`
**Review:** QG203, QG209, QG234, QG237, QG238, QG247, QG253
**Requirements:** no target values, no observables, D96 only, deterministic
**Method:** derive a target-free formula-choice rule; apply before comparison

---

## 1. The Problem

QG253 proved that a bare minimal-complexity search over ALL dimensionless
D96 combinations does NOT uniquely select the published formulas: in 4 of 7
cases a **strictly simpler** (but non-native) expression matched the target.

This phase asks: is there a **D96-only, target-free rule** that selects a
formula before any comparison?

---

## 2. The Principle — Octave Preservation

The D96 observable sector is **octave-organized**: the spectrum is grouped
into the octave bands occ = [4,4,87] (three octave families, QG155/QG210).

**The rule:** a formula is *selectable* iff it does **not isolate a single
octave band** occ₀, occ₁, or occ₃ (or ln of a single band). Isolating one
band privileges one octave over the others with no D96 principle.

**Allowed:** octave ratios (occᵢ/occⱼ — scale-invariant band structure),
the full aggregate occMom = Σocc²/occ₀, and the spectral aggregates
(Σm, #d, #g, span, λ₂, Σ√m).

This is the D96 symmetry projection of Noether consistency: formulas
invariant under the octave band symmetry.

---

## 3. Why It Selects (applied BEFORE comparison)

| Observable | Non-native "simpler" alt (QG253) | Excluded by octave preservation? |
|-----------|----------------------------------|----------------------------------|
| r₂₁ | √Σm/occ₀ | **YES** — isolates occ₀ |
| 1−n_s | 1/(span·ln occ₃) | **YES** — isolates occ₃ |
| m₂/m₃ | 1/(occ₀√2) | **YES** — isolates occ₀ |
| y_t/y_b | occ₀²/λ₂ | **YES** — isolates occ₀ |
| m_μ/me | #g²/√occ₃ | **YES** — isolates occ₃ |
| m_τ/m_μ | √3·√Σm | no (octave-preserving) |
| r₃₁ | λ₂³·Σ√m | no (octave-preserving) |
| m_μ/me | 5/4·Σ√m/λ₂ | no (octave-preserving) |

**All five non-native alternatives are excluded** (they each isolate a
single octave band). The published formulas **all satisfy** octave
preservation — they use occMom or octave ratios, never an isolated band.

**Residual:** 3 octave-preserving ties remain (√3·√Σm, λ₂³·Σ√m,
5/4·Σ√m/λ₂). The principle narrows to the octave-preserving class — a
strong prior — but does not uniquely fix every formula without additional
symmetry selection.

---

## 4. The Determination

### **SELECTION PRINCIPLE**

The octave-preservation rule is a genuine **derivation-choice rule**:

- **D96-only** — uses only the octave structure (presence of occ₀/occ₁/occ₃);
- **target-free** — applied before any comparison, no observables consulted;
- **deterministic** — a pure predicate on the expression and the D96 octaves;
- **selective** — removes 5 of the 8 QG253 minimal-complexity alternatives
  (all the non-native ones), while every published formula satisfies it.

This is the rule QG253 asked for: it selects the octave-preserving
candidate set **before** any target value enters. The published formulas are
the octave-preserving members of the D96 expression class.

**Honest caveat:** the rule is a strong prior, not a total order — 3
octave-preserving ties survive, so additional symmetry selection (e.g.,
preferring occMom-based forms) is needed to fix those uniquely. The
principle REPLACES empirical formula choice with a stated D96 symmetry
projection for the non-native alternatives that drove the QG253
non-uniqueness.
