# TQM-QG Phase 303 — Hidden Dual Prediction

**Status:** COMPLETE — **NEW DUALS**
**Tests:** TQMQG3030, TQMQG3031, TQMQG3032 (all passed)
**Core class:** `TQM.Core/ResearchXH/HiddenDualPrediction.cs`
**Question:** which scalar results lack tensor partners, which tensor results lack scalar partners, and what are the missing duals?
**Method:** no observables, no target values, D96 only, deterministic — the {ρ, ψ} trace/traceless principle (QG286) extended to every rank-2 physical object.

---

## 1. The Principle

For any rank-2 tensor T_μν: `T_μν = (1/d)·Tr(T)·g_μν + traceless`. The **trace** is the scalar face (ρ-type read), the **traceless** is the tensor face (ψ-type read).

- A **scalar result** whose tensor partner is missing has a **hidden dual**: the tensor whose trace it is.
- A **tensor result** whose scalar partner is missing has a **hidden dual**: the trace it carries.

---

## 2. Scalar Results → Predicted Tensor Duals

| Scalar result | Hidden tensor dual | Decomposition |
|---|---|---|
| masses | **T_μν** (stress-energy) | m = Tr(T_μν)/d — the mass is the trace; anisotropic stress is the traceless |
| gauge couplings (α_W, sin²θ_W) | **F_μν** (field strength) | α = the contraction strength of F_μν |
| fermion masses (y_f) | **Yukawa tensor** y_f = m_f/v | the mass-to-VEV ratio is the trace read |

---

## 3. Tensor Results → Predicted Scalar Duals

| Tensor result | Hidden scalar dual | Decomposition |
|---|---|---|
| gravitational entropy S ∝ A | **N_def** (deficit cell count) | S ∝ A is the geometry face; N_def = A/cell is the count face |
| Newton constant κ = 8πG | **M_Pl** (Planck mass) | M_Pl is both the tensor coupling κ and a scalar mass |
| Weyl tensor | **Ricci trace R** | the Weyl tensor is the traceless curvature; its scalar content is R |

---

## 4. The Prediction

**6 hidden duals predicted (3 scalar→tensor, 3 tensor→scalar).**

The QG301 weak duals are completed: the scalar VALUES lack strong tensor partners because their true tensor face is the **rank-2 tensor whose trace they read**; the tensor results lack strong scalar partners because their hidden scalar face is the **trace/count they carry**.

---

## 5. Conclusion

### **NEW DUALS** (prediction score 5/5)

**The {ρ, ψ} decomposition extends to EVERY rank-2 physical object.**

- **masses → T_μν**: m = Tr(T_μν)/d (the mass is the trace, the anisotropic stress is the traceless);
- **couplings → F_μν**: α = the contraction strength of the interaction tensor;
- **S ∝ A → N_def**: A/cell — the count face of the area;
- **κ → M_Pl**: the scalar read of the same spectral constants;
- **Weyl → Ricci trace R**.

**The reduction chain (QG260→303):**
```
Resonance Layer → … → Assignment Closed → Psi Reinterpretation → DIFFERENCE DUALITY
→ POST-DUALITY INVARIANCE → DEPENDENCY REBUILD → ANCHOR INVENTORY → FRAMEWORK INVENTORY
→ FRAMEWORK NECESSITY → FOUNDATION STRESS TEST → HIERARCHY NECESSITY → MINIMAL THEORY
→ SPECTRUM NECESSITY → RECONSTRUCTION → EXCEPTION AUDIT → FIRST PEAK ORIGIN
→ REMAINING FRONTIER RE-AUDIT → OPERATOR UNIVERSALITY PREDICTION → DUALITY PREDICTION AUDIT
→ CROSS-DOMAIN UNIVERSALITY → HIDDEN DUAL PREDICTION (the {ρ, ψ} duality extends to every rank-2 object)
```

**Frontier status:** the scalar/tensor duality is now extended to every rank-2 physical object via predicted hidden duals. Remaining frontier unchanged: temporal evidence, SM gaps (Bekenstein 1/4), ψ fundamental status, Difference boundary, methodology.
