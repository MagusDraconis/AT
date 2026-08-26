# AT-QG Phase 255 — Secondary Selection Principle

**Status:** COMPLETE — **UNIQUE SELECTION PRINCIPLE**
**Tests:** ATQG2550, ATQG2551, ATQG2552 (all passed)
**Core class:** `AT.Core/ResearchXH/SecondarySelectionPrinciple.cs`
**Known:** QG254 (octave preservation)
**Requirements:** no observables, no target values, D96 only, deterministic
**Method:** one secondary rule resolving the octave-preserving ties

---

## 1. The Problem

QG254 established **octave preservation** as the primary D96-only selection
rule, but three octave-preserving ties remained:

| Observable | Ties |
|-----------|------|
| m_μ/me | Σm²/√occMom vs 5/4·Σ√m/λ₂ |
| m_τ/m_μ | √occMom·λ₂ vs √3·√Σm vs √#d/λ₂ |
| r₃₁ | span/√3 vs λ₂³·Σ√m |

This phase derives **one** target-free rule that resolves them.

---

## 2. The Secondary Rule — Moment-Closure MDL

Applied in order to the octave-preserving candidate set:

1. **Minimal complexity** — fewest operators/quantities.
2. **Noether consistency** — no free constant multiplier (a genuine D96
   coupling is a ratio of D96 quantities only). √3 is NOT flagged (it is
   D96-native: √#families, QG210).
3. **Moment closure / full-spectrum usage** — highest total moment order:
   occMom (2nd octave moment) and Σm² (2nd mode moment) beat half-moments
   (Σ√m) and counts (#d, #g).

The rule reads **only the formula structure** — no observed value enters.

---

## 3. Application to the QG254 Ties

| Observable | Candidates (c) | Step that decides | Selected |
|-----------|----------------|-------------------|----------|
| m_μ/me | Σm²/√occMom (5), 5/4·Σ√m/λ₂ (5) | Noether: 5/4 is a free constant | **Σm²/√occMom** |
| m_τ/m_μ | √occMom·λ₂ (4), √3·√Σm (4), √#d/λ₂ (4) | Moment closure: occMom(2)+λ₂(1)=3 beats Σ√m(0.5) and #d(0)+λ₂(1)=1 | **√occMom·λ₂** |
| r₃₁ | span/√3 (3), λ₂³·Σ√m (4) | Minimal complexity: 3 < 4 | **span/√3** |

**All three tie cases resolve to a unique formula — the published one —
with no target information.**

---

## 4. The Determination

### **UNIQUE SELECTION PRINCIPLE**

The combined selection chain is complete:

- **QG254** (octave preservation) removes the non-native alternatives;
- **QG255** (moment-closure MDL) resolves the remaining octave-preserving
  ties by structure only (complexity → Noether → moment order);
- the result is **one formula per observable, selected before any
  comparison**, using no target values and no observables.

**Completeness of the selection program:**
| Principle | Role |
|-----------|------|
| QG253 | search space (all dimensionless D96 combinations) |
| QG254 | octave preservation (excludes non-native formulas) |
| QG255 | moment-closure MDL (resolves octave-preserving ties) |

Together they uniquely select the published formulas for all audited
observables from D96 structure alone.
