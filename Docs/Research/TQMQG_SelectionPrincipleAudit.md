# TQM-QG Phase 256 — Selection Principle Audit

**Status:** COMPLETE — **HIGH** selection-principle risk (1 PREFERRED / 1 ARBITRARY / 0 FORCED)
**Tests:** TQMQG2560, TQMQG2561, TQMQG2562 (all passed)
**Core class:** `TQM.Core/ResearchXH/SelectionPrincipleAudit.cs`
**Review:** QG254 (octave preservation), QG255 (moment-closure MDL)
**Method:** audit only — no physics, methodology only

---

## 1. The Question

Are the QG254/QG255 selection rules **forced by D96** or were they
**selected post-hoc**?

---

## 2. Rule 1 — Octave Preservation (QG254)

| Criterion | Verdict |
|-----------|---------|
| **Derivable** | PARTIALLY — the octave bands occ = [4,4,87] ARE D96-native (QG155/210) |
| **Necessary** | NO — competing symmetry projections exist |
| **Alternatives** | prefer occMom-based forms; band-permutation invariance (trivially true: occ₀=occ₁=4); full-spectrum usage; the λ₂ scale |
| **Consistency** | the octave STRUCTURE is derivable; the PROHIBITION FORM (no isolated band) was calibrated on the QG253 alternatives |

**Classification: PREFERRED** — D96-grounded in substance, post-hoc in form.

---

## 3. Rule 2 — Moment-Closure MDL (QG255)

| Criterion | Verdict |
|-----------|---------|
| **Derivable** | NO — MDL is imported from information theory; the moment-order ranking is conventional |
| **Necessary** | NO |
| **Alternatives** | prefer λ₂ as the mass scale; fewest distinct quantities; octave-permutation invariance; 3rd-moment closure |
| **Consistency** | **DECISIVE — INCONSISTENT**: QG255 rejects 5/4 as a "free constant", but the PUBLISHED QG238 formula ℓ₁ = Σm·ln(span)·(5/4) uses 5/4 |

**Classification: ARBITRARY** — the Noether 5/4 exclusion is post-hoc.

### The decisive inconsistency

```
QG255 excluded:   5/4·Σ√m/λ₂   (because "5/4 is a free constant")
QG238 published:  ℓ₁ = Σm·ln(span)·(5/4)   (5/4 IS a published TQM multiplier)
```

The same 5/4 that QG255 rejected as non-D96 appears in the published
acoustic-peak formula. The exclusion was calibrated on the tie candidate,
not on a uniform D96 principle.

---

## 4. The Determination

### **HIGH** selection-principle risk

- **OCTAVE PRESERVATION: PREFERRED** — grounded in the D96 octave structure,
  but the prohibition form was calibrated on the QG253 alternatives;
- **MOMENT-CLOSURE MDL: ARBITRARY** — MDL imported, moment-order ranking
  conventional, and the Noether 5/4 exclusion contradicts the published QG238
  formula (post-hoc distinction);
- **Neither rule is FORCED.**

The rules were introduced **after** QG253 revealed the non-uniqueness, so
they carry the same retro-selection character they were intended to remove —
at the meta-level. The honest status of the "selection principle" program
(QG253-255) is a reasonable heuristic narrowing, not a derivation of forced
selection rules.
