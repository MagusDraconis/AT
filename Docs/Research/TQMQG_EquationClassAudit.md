# TQM-QG Phase 276 — Equation Class Audit

**Status:** COMPLETE — **EQUATION CLASS LAYER**
**Tests:** TQMQG2760, TQMQG2761, TQMQG2762 (all passed)
**Core class:** `TQM.Core/ResearchXH/EquationClassAudit.cs`
**Question:** why do different equation types exist? Where is the layer between Role and Observable?
**Method:** no observables, no target values, D96 only, deterministic.

---

## 1. The Equation Forms Per Sector

| Sector | Equation form | Example | Determined by |
|--------|---------------|---------|---------------|
| **mass** | scalar equality | m_f = me·(Σ√m/√Σm²) | VALUE class |
| **coupling** | ratio / inverse-ratio | α_weak = 3/Σm | STRENGTH class |
| **mixing** | angle + unitarity | Vus = #d/(2Σm), V†V=I | ORIENTATION class |
| **gravity** | power law | M_Pl = v·(Σm·#g·occ₂)³ | GEOMETRY class |
| **cosmology** | log-ratio | n_s = 1−ln(span)/(Σm−#d) | GLOBAL class |

**Each equation form is the measurement class's natural relation:**
- VALUE → equality (a value = a value);
- STRENGTH → ratio (a strength = a normalized ratio);
- ORIENTATION → unitary angle;
- GLOBAL → log-ratio (scale-invariant);
- GEOMETRY → power law.

---

## 2. The Form Sharing (projection classes, not fundamental)

```
m_τ/m_μ (mass)   = √occMom·λ₂ = 16.842
y_τ/y_μ (coupling) = √occMom·λ₂ = 16.842
Vus     (mixing)  = #d/(2Σm)   — same ratio form as sin²θ_W = #g/(2Σm)
```

The **ratio-equality form spans mass, coupling, AND mixing sectors** — no
equation form is sector-unique. The equation classes are **projection classes**,
not fundamental per-sector forms.

---

## 3. The Layer Structure (Role → Observable)

```
ROLE (measurement class → sector)
    → EQUATION FORM (the class's natural relation)
    → OBSERVABLE (the concrete quantity)
```

The equation form is the **bridge between the role and the observable**: the
structural relation type that the observable satisfies, set by its measurement
class.

---

## 4. Conclusion

### **EQUATION CLASS LAYER** (equation-layer score 6/6)

An equation-class layer exists **between Role and Observable**:
- the equation forms are **determined by the measurement class** (value→equality,
  strength→ratio, orientation→unitary, global→log, geometry→power);
- the forms are **projection classes** — the ratio form spans mass/coupling/
  mixing, so they are NOT fundamental.

**The reduction chain (QG260→276):**
```
Resonance Layer → Operator Layer → Same Operator Sectors
→ Single Resonance Dynamics → Single Resonance Invariant
→ Universal Conservation → Self-Consistency
→ Individuation → Difference Principle
→ Post-Resonance Integrity (frontier = assignment)
→ Sector Emergence → Partial Assignment → Measurement Class Layer
→ Partial Role Principle (role = ontological category)
→ EQUATION CLASS LAYER (ROLE → EQUATION FORM → OBSERVABLE)
```

**Where the frontier stands:** the equation form is now located — every
observable satisfies the natural relation form of its measurement class. The
residual step remains the relational-subclass role (which equation a strength
read enters), but the equation layer itself is structural and shared.
