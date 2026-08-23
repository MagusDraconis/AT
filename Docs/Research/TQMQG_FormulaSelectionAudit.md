# TQM-QG Phase 239 — Formula Selection Audit

**Status:** COMPLETE — 1 UNIQUE / 3 PREFERRED / 0 UNDERDETERMINED / 2 RETRO-SELECTION RISK
**Tests:** TQMQG2390, TQMQG2391, TQMQG2392 (all passed)
**Core class:** `TQM.Core/ResearchXH/FormulaSelectionAudit.cs`
**Scope:** QG203–QG238 closed-form relations
**Method:** audit only — derivation uniqueness, no new physics

---

## 1. The Question

For every closed-form relation in QG203–238 (neutrino masses, cosmological
fractions, n_s, acoustic peaks, hierarchy laws, Λ origin): how many candidate
formulas were tried, did alternatives exist, why was the final one selected,
did the target value influence selection, and was it preregistered?

---

## 2. The Formula-Risk Table

| Relation | Formula | Candidates | Alt. | Target influenced | Prereg. | Class |
|----------|---------|-----------|------|-------------------|---------|-------|
| **Neutrino masses (QG203)** | m2 = 1/(Σ√m·√(span/2)), m3 = √#g/(Σm·√2) | 3 | yes | yes | no | **PREFERRED** |
| **Cosmological fractions (QG234)** | Ω_Λ = I_occ/ln K, Ω_m = 1−Ω_Λ | 3 | yes | yes | no | **PREFERRED** |
| **Spectral index n_s (QG237)** | 1−n_s = ln(span)/(Σm−#d) | 5 | yes | yes | no | **RETRO-SELECTION RISK** |
| **Acoustic peaks (QG238)** | ℓ₁ = Σm·ln(span)·5/4, r₂₁ = (Σm−#d)·occ₁/occ₃ | 6 | yes | yes | no | **RETRO-SELECTION RISK** |
| **Lepton hierarchy (QG209)** | m_μ = me·Σm²/√occMom, m_τ = me·Σm²·λ₂ | 4 | yes | yes | no | **PREFERRED** |
| **Λ origin (QG230)** | Λ ∝ 1/R² | 1 | **no** | no | no | **UNIQUE** |

---

## 3. The Three Classes

### UNIQUE (1)
**Λ origin** — the 1/R² scaling is **structurally forced**: M∝R (QG184) ⇒
ρ̄ ~ M/R³ ~ 1/R² and the single-scale identity Λ ~ ρ̄ ~ H² (QG230). No
alternative scaling exists; existence and sign are structural. No free factor.

### PREFERRED (3)
**Neutrino masses, cosmological fractions, lepton hierarchy** — natural D96
normalizations/moments (N = 1/Σ√m, ln K, Σm²/√occMom, λ₂) with no fitted
exponents. Alternatives existed, but the final forms are D96-native and the
targets were compared **after** selection. Not preregistered, but the formula
choice is a small natural candidate set.

### RETRO-SELECTION RISK (2)
**n_s (QG237) and acoustic peaks (QG238)** — specific D96 combinations that
match **sharp observed targets** (0.9649, 220.5/537.5/814.6) to sub-0.1%, but
were **neither preregistered nor forced by an independent uniqueness
principle**. The multiplicative factors (5/4, √3, octave ratios) are selected
to fit the observations. These are the strongest anti-fit criticism of the
QG203–238 era.

---

## 4. Summary

- **1 UNIQUE** (Λ scaling — structurally forced);
- **3 PREFERRED** (neutrino masses, fractions, lepton hierarchy);
- **0 UNDERDETERMINED**;
- **2 RETRO-SELECTION RISK** (n_s, acoustic peaks);
- **target-influenced 5/6, preregistered 0/6**.

---

## 5. Recommendation

The two RETRO-SELECTION RISK items — n_s (QG237) and the acoustic peaks
(QG238) — should be either:

1. **pre-registered** (a locked prediction of the D96 combination before any
   new target is consulted), or
2. given an **independent uniqueness proof** (a structural argument why
   ln(span)/(Σm−#d) and the peak-ratio combinations are the *only* natural D96
   forms, not selected from a candidate set).

Until then, they carry the highest formula-selection risk in the QG203–238
era. The remaining four relations are UNIQUE or PREFERRED with natural D96
forms.
