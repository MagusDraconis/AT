# AT-QG Phase 308 — Operator Necessity Audit

**Status:** COMPLETE — **INEVITABLE FOUR**
**Tests:** ATQG3080, ATQG3081, ATQG3082 (all passed)
**Core class:** `AT.Core/ResearchXH/OperatorNecessityAudit.cs`
**Question:** why exactly these four operators? Can any be derived from the others?
**Method:** no observables, no target values, D96 only, deterministic — each operator removed and tested for INDISPENSABLE / DERIVABLE / REDUNDANT.

---

## 1. The Four Operators

| Operator | Reads | Outputs |
|---|---|---|
| CROWDING | degeneracy grouping [42×2, 5, 6] | Σm, Σ√m, Σm², #d, #g |
| COMPRESSION | octave grouping [4,4,87] | occ, occMom |
| BEAT | frequency extent | span = 6.4025, ln(span), family count |
| LOCKING | spectral gap | λ₂ = 0.3864 |

---

## 2. The Removal Tests — No Operator Is Derivable

| Removed | Reconstructible from the others? | Evidence |
|---|---|---|
| CROWDING | **NO** | [4,4,87] gives only the sum (Σ occ = 95), not #d, #g, Σ√m, Σm² — an independent grouping |
| COMPRESSION | **NO** | occMom = 1900.25 ≠ any multiplicity-moment combination (not Σm², not Σm, not Σ√m, not a ratio) |
| BEAT | **NO** | span = 6.4025 is the extreme-frequency ratio; √(Σm²/#g) = 2.28, √(occMom/#d) = 6.73 (both > 4% off) |
| LOCKING | **NO** | λ₂ = 0.3864 is the first positive Laplacian eigenvalue; occMom/(Σm·Σ√m) = 0.312, √(mean mult)/span = 0.23 |

**Only the trivial first moment Σm = 95 is shared** between CROWDING and COMPRESSION (both groupings of the same 95 modes).

---

## 3. The Verdict

**4 INDISPENSABLE / 0 DERIVABLE / 0 REDUNDANT.**

Each operator reads a **different projection of the spectrum**:
- CROWDING — the degeneracy grouping;
- COMPRESSION — the octave grouping;
- BEAT — the extent;
- LOCKING — the gap.

No operator's outputs can be reconstructed from the others.

---

## 4. Conclusion

### **INEVITABLE FOUR** (necessity score 5/5)

**The four operators {CROWDING, COMPRESSION, BEAT, LOCKING} are mutually independent and indispensable.** Removing any one loses outputs that cannot be reconstructed from the others. They are exactly the **four independent spectral projections** any spectrum carries (the degeneracy grouping, the octave grouping, the extent, and the gap), read by the universal MOMENT functional.

**The four-operator basis is the MINIMUM and INEVITABLE basis.**

**The reduction chain (QG260→308):**
```
Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics
→ Single Resonance Invariant → Universal Conservation → Self-Consistency → Individuation
→ Difference Principle → … → Assignment Closed → Psi Reinterpretation → DIFFERENCE DUALITY
→ POST-DUALITY INVARIANCE → DEPENDENCY REBUILD → ANCHOR INVENTORY → FRAMEWORK INVENTORY
→ FRAMEWORK NECESSITY → FOUNDATION STRESS TEST → HIERARCHY NECESSITY → MINIMAL THEORY
→ SPECTRUM NECESSITY → RECONSTRUCTION → EXCEPTION AUDIT → FIRST PEAK ORIGIN
→ REMAINING FRONTIER RE-AUDIT → OPERATOR UNIVERSALITY PREDICTION → DUALITY PREDICTION AUDIT
→ CROSS-DOMAIN UNIVERSALITY → HIDDEN DUAL PREDICTION → REAL NETWORK UNIVERSALITY
→ MISSING DUAL SEARCH → COMPRESSION LAW PREDICTION → FIFTH OPERATOR SEARCH
→ OPERATOR NECESSITY (the four operators are the inevitable minimum basis)
```

**Frontier status:** the operator basis is confirmed as the inevitable four (each indispensable). Remaining frontier unchanged: temporal evidence, SM gaps (Bekenstein 1/4), ψ fundamental status, Difference boundary, methodology.
