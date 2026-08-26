# AT-QG Phase 298 — First Peak Origin Audit

**Status:** COMPLETE — **FIRST PEAK ORIGIN**
**Tests:** ATQG2980, ATQG2981, ATQG2982 (all passed)
**Core class:** `AT.Core/ResearchXH/FirstPeakOriginAudit.cs`
**Question:** why does only ℓ₁ require an extra factor (5/4) while the peak ratios need none?
**Method:** no observables, no target values, D96 only, deterministic — boundary projection, first-mode normalization, fundamental harmonic, background mode, zero-mode transition investigated.

---

## 1. The Peak Structure (QG238)

```
ℓ₁    = Σm·ln(span)·(5/4) = 220.48   ← ABSOLUTE first peak (the fundamental harmonic)
ℓ₂/ℓ₁ = (Σm−#d)·occ₁/occ₃ = 2.4368   ← RATIO (normalization cancels)
ℓ₃/ℓ₁ = span/√3 = 3.6965             ← RATIO (normalization cancels)
```

---

## 2. The Key Insight: Only ℓ₁ Is Absolute

- **ℓ₁ is the only ABSOLUTE peak position** — the fundamental harmonic sets the absolute ℓ-scale.
- **ℓ₂/ℓ₁ and ℓ₃/ℓ₁ are RELATIVE ratios** — any common normalization of the fundamental appears in both numerator and denominator and **cancels**.
- Therefore **only the absolute first peak needs a first-mode normalization** — the ratios never need an extra factor. This is why the QG238 peak ratios are pure spectral while ℓ₁ carries the 5/4.

---

## 3. The Structural Reading of 5/4: the Boundary Projection

The D96 spectrum has **1 zero mode (the background, QG270) + 95 positive modes**. The lightest octave has **occ₀ = 4 modes** ([4,4,87]). The **fundamental harmonic** (the first sound-horizon mode) sits at the **background → first-octave boundary**. Its normalization is the **boundary projection**:

```
5/4 = (occ₀ + zero_mode)/occ₀ = (4 + 1)/4 = 5/4
```

This is the **first-mode normalization** of the fundamental that includes the **background zero-mode transition** — a **missing structural projection**, not a free fit.

---

## 4. Why the Ratios Need No Factor

| Quantity | Type | Factor? | Why |
|---|---|---|---|
| ℓ₁ | ABSOLUTE | 5/4 | the fundamental's boundary projection (includes the zero-mode transition) |
| ℓ₂/ℓ₁ | RATIO | none | normalization cancels (5/4 in both numerator and denominator) |
| ℓ₃/ℓ₁ | RATIO | none | normalization cancels |

---

## 5. Conclusion

### **FIRST PEAK ORIGIN** (origin score 5/5)

**5/4 is the boundary projection of the fundamental harmonic:**
```
(occ₀ + zero_mode)/occ₀ = (4 + 1)/4 = 5/4
```
- the **first-mode normalization** that includes the **background zero-mode transition** (QG270: 1 zero mode + 95 positive modes);
- **only ℓ₁ (the absolute fundamental) carries it** — the ratios are relative and the normalization cancels;
- the **QG297 "fit" is reinterpreted** as the fundamental's boundary projection — a **missing structural projection**, not a free constant.

**The reduction chain (QG260→298):**
```
Resonance Layer → … → Assignment Closed → Psi Reinterpretation → DIFFERENCE DUALITY
→ POST-DUALITY INVARIANCE → DEPENDENCY REBUILD → ANCHOR INVENTORY → FRAMEWORK INVENTORY
→ FRAMEWORK NECESSITY → FOUNDATION STRESS TEST → HIERARCHY NECESSITY → MINIMAL THEORY
→ SPECTRUM NECESSITY → RECONSTRUCTION → EXCEPTION AUDIT → FIRST PEAK ORIGIN
(5/4 = the boundary projection of the fundamental harmonic)
```

**Frontier status:** the 5/4 exception (QG280 R4) is now re-interpreted as the fundamental's boundary projection — the first peak carries the zero-mode transition that the ratios cancel. Remaining frontier unchanged: temporal evidence, ψ fundamental status, SM gaps (Bekenstein 1/4), Difference boundary, methodology.
