# AT-QG Phase 297 — Exception Audit

**Status:** COMPLETE — **EXCEPTION REMAINS**
**Tests:** ATQG2970, ATQG2971, ATQG2972 (all passed)
**Core class:** `AT.Core/ResearchXH/ExceptionAudit.cs`
**Question:** is 5/4 derived, structural, an artifact, a fit, or a boundary — and can every occurrence be traced to the same source?
**Method:** no observables, no target values, D96 only, deterministic — the value, its label-identity reading, its fit ratio, and all occurrences audited.

---

## 1. The Occurrences of 5/4

| Phase | Context | Formula | Same source as ℓ₁? |
|---|---|---|---|
| QG238 | acoustic peak fit | ℓ₁ = Σm·ln(span)·(5/4) | (reference) |
| QG253 | formula-uniqueness tie candidate | 5/4·Σ√m/λ₂ (m_μ/me) | NO |
| QG253 | standard multiplier test set | {2, 3, 4, 5, 1/2, 1/3, 5/4, 4/5, √2, √3} | NO |
| QG255 | Noether rule rejection | 5/4 rejected as a free constant | NO |
| QG289 | anchor inventory | 5/4 = REMOVABLE (free constant) | NO |
| QG296 | reconstruction audit | 5/4 = REQUIRES EXTRA ASSUMPTION | NO |

**Not every occurrence traces to the same source.**

---

## 2. What IS 5/4?

| Question | Answer | Evidence |
|---|---|---|
| DERIVED? | **NO** | no beat identity equals 1.25 (Σ√m/span≈10, occMom/Σm≈20, Σm²/Σm≈12/5, occMom/Σm²≈25/3) |
| STRUCTURAL? | **NO** | "lightest-octave-relative multiplicity" = label identity (occ₀+1)/occ₀ = 5/4, no mechanism (the QG185 Bekenstein 1/occ₀ standard) |
| ARTIFACT? | PARTIAL | the QG255/238 rule inconsistency is an artifact of calibration; the 5/4 itself is a real fit |
| **FIT?** | **YES** | ℓ₁/(Σm·ln span) = 1.2501 ≈ 5/4 (fit to 0.008%) |
| BOUNDARY? | **NO** | documented REMOVABLE (QG289), not irreducible |

**5/4 = FIT.**

---

## 3. Why Not a Single Source

- The **QG238 5/4** is the **fitted factor** of the acoustic peak ℓ₁ = 220.48 → observed 220.5.
- The **QG253/255 5/4** is one of the **standard small-constant multipliers** in the formula-selection tournament (5/4·Σ√m/λ₂ was a tie candidate, rejected by Noether).
- These are the **same value in different fitting contexts** — no single D96 origin.

---

## 4. Conclusion

### **EXCEPTION REMAINS** (exception score 5/5)

**5/4 = FIT.** It is not derived (no beat identity), not structural (the (occ₀+1)/occ₀ reading is a label identity without a mechanism — the same standard that rejected Bekenstein 1/occ₀), not a genuine boundary (it is a removable fit, QG289). It is the **multiplicative factor fitted so that ℓ₁ matches observation**.

**Not every occurrence traces to the same source** — the ℓ₁ fit and the tournament multiplier are independent instances of small-rational fitting.

**The QG280 R4 meta-inconsistency stands**: QG238 uses 5/4 while QG255 rejects free constants — the exception is now characterized (a fit, not derived/structural) but not resolved.

**The reduction chain (QG260→297):**
```
Resonance Layer → … → Assignment Closed → Psi Reinterpretation → DIFFERENCE DUALITY
→ POST-DUALITY INVARIANCE → DEPENDENCY REBUILD → ANCHOR INVENTORY → FRAMEWORK INVENTORY
→ FRAMEWORK NECESSITY → FOUNDATION STRESS TEST → HIERARCHY NECESSITY → MINIMAL THEORY
→ SPECTRUM NECESSITY → RECONSTRUCTION → EXCEPTION AUDIT (5/4 = FIT, R4 exception remains)
```

**Frontier status:** the 5/4 exception is characterized as a fit (not derived/structural/boundary) but remains open (R4). Remaining frontier unchanged: temporal evidence, 5/4 derivation, ψ fundamental status, SM gaps (Bekenstein 1/4), Difference boundary, methodology.
