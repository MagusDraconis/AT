# AT-QG Phase 253 — Formula Uniqueness Audit

**Status:** COMPLETE — 1 UNIQUE / 2 NON-UNIQUE / 4 MULTIPLE MATCHES (of 7 audited formulas)
**Tests:** ATQG2530, ATQG2531, ATQG2532 (all passed)
**Core class:** `AT.Core/ResearchXH/FormulaUniquenessAudit.cs`
**Review:** QG203, QG209, QG234, QG237, QG238, QG247
**Method:** generate ALL dimensionless D96 combinations; search minimal-complexity expression per observable
**No new physics — methodology only.**

---

## 1. The Method (derivation-choice rule)

Instead of choosing a formula empirically, the audit:
1. Generates a large candidate pool (~hundreds of thousands of expressions)
   from the D96 quantities (Σm, #d, #g, span, λ₂, occ₀, occ₁, occ₃, occMom,
   Σ√m) with forms q, q², q³, √q, 1/q, ln q, affine differences, products,
   ratios, triples, 1/(affine), and a small constant set;
2. Scores each by **complexity** = distinct quantities + operators +
   (1 if a non-trivial constant);
3. Finds every candidate matching the target within 0.5%;
4. Determines whether the **published formula is the simplest**.

---

## 2. The Results

| Observable | Published | Published c | Min c | Classification |
|-----------|-----------|-------------|-------|----------------|
| r₃₁ | span/√3 | 3 | 3 | **UNIQUE** |
| m_μ/me | Σm²/√occMom | 5 | 5 | **NON-UNIQUE** |
| m_τ/m_μ | √occMom·λ₂ | 4 | 4 | **NON-UNIQUE** |
| 1−n_s | ln(span)/(Σm−#d) | 7 | 5 | **MULTIPLE MATCHES** |
| r₂₁ | (Σm−#d)·occ₁/occ₃ | 6 | 4 | **MULTIPLE MATCHES** |
| m₂/m₃ | 2Σm/(Σ√m·√(span·#g)) | 8 | 4 | **MULTIPLE MATCHES** |
| y_t/y_b | mass-law ratio | 8 | 4 | **MULTIPLE MATCHES** |

### The simpler alternatives the search found

- **1−n_s**: `1/(span·ln occ₃)` (c=5, dev 0.16%) — simpler than the published
  ln(span)/(Σm−#d) (c=7).
- **r₂₁**: `√Σm/occ₀` (c=4, dev 0.004%) — simpler AND more accurate than the
  published (c=6).
- **m₂/m₃**: `1/(occ₀·√2)` (c=4, dev 0.100%) — simpler than the published (c=8).
- **y_t/y_b**: `occ₀²/λ₂` (c=4, dev 0.37%) — simpler than the mass-law ratio.
- **m_μ/me** ties at c=5 with `#g²/√occ₃` (dev 0.26%) and `5/4·Σ√m/λ₂` (0.15%).
- **m_τ/m_μ** ties at c=4 with `√3·√Σm` (0.24%) and `√#d/λ₂` (0.40%).

---

## 3. The Determination

### **UNIQUE 1 / NON-UNIQUE 2 / MULTIPLE MATCHES 4**

Only **r₃₁ = span/√3** is the unique minimal-complexity expression. **Six of
seven** published formulas are NOT forced by a minimal-complexity
derivation-choice rule: the choice was **target-informed** (empirical), which
confirms the QG239/QG250 **RETRO-SELECTION RISK** for all but r₃₁.

**Answer to the goal:** a blind minimal-complexity search does NOT reproduce
the published formulas as the unique simplest expressions. The published
formulas are **NON-UNIQUE / MULTIPLE MATCHES** — the empirical formula choice
is not replaced by a derivation-choice rule; the search exposes that simpler
D96 combinations reproduce the same observables.

**Honest implication:** the mass/cosmology "derivations" are not uniquely
selected by simplicity — a critic can always find an equally-simple or simpler
D96 combination. This is the strongest quantitative support for the selection
risk already flagged in QG239 and QG250 (#6), and it applies to the
high-impact formulas (n_s, acoustic peaks, neutrino ratio, quark hierarchy).
