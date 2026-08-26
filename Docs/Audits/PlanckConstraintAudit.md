# Planck Constraint Audit

**Question:** is current AT compatible with Planck 2018 constraints?
**Scope:** Ω_b, Ω_DM, H0, Λ, θ* (+ supplementary base parameters).
**Discipline:** no new physics; no acoustic-model invention; repository evidence only.

---

## 1. Planck Observables

| Parameter | Planck 2018 value | Description |
|---|---|---|
| Ω_b h² | 0.02237 ± 0.00015 | baryon density |
| Ω_c h² | 0.1200 ± 0.0012 | cold dark matter density |
| Ω_b | 0.0493 | baryon fraction |
| Ω_DM | 0.2640 | dark matter fraction |
| H0 | 67.36 ± 0.54 km/s/Mpc | Hubble constant |
| Ω_Λ | 0.6847 ± 0.0073 | dark energy density |
| Λ | ~1.1×10⁻⁵² m⁻² | cosmological constant (from Ω_Λ H0²) |
| 100 θ_MC | 1.04092 ± 0.00031 | acoustic-peak angular scale |
| θ* | 0.0104092 | sound-horizon / D_A |
| n_s | 0.9649 ± 0.0042 | scalar spectral index |
| ln(10¹⁰ A_s) | 3.044 ± 0.014 | primordial amplitude |
| τ | 0.0544 ± 0.0073 | reionization optical depth |
| σ₈ | 0.8111 ± 0.0060 | clustering amplitude |

---

## 2. Classification (Derived / Imported / Unknown)

| Parameter | Class | AT status | Repository evidence |
|---|---|---|---|
| Ω_b | Imported | Contingent draw (log-normal abundance) | XB002 / Phase 152: relic-density ensemble |
| Ω_DM | Imported | Natural scale ~0.1–1 derived; exact 0.27 contingent | X065 `DefectRelicAbundanceAnalyzer.cs` |
| H0 | Imported | Primitive boundary condition (arbitrary) | Phases 145–147 `OriginOfHAudit.cs` / `WhyThisHAudit.cs` |
| Λ | **Derived** | Λ ~ 1/√N genuine postdiction (α≈2.07, exponent −½) | Phase 140 `CausalSetLambdaModel.cs` |
| θ* | Unknown | Not computed; 0.5–1% peak-shift predicted | QG-081 `ModelDependenceAnalyzer.cs`, X046b/X062 |
| n_s | Unknown | No derivation | — |
| A_s | Unknown | No derivation | — |
| τ | Unknown | No derivation | — |
| σ₈ | Unknown | Growth-shift predicted (1–5%), not computed | X046b `CosmologyAudit.cs` |

---

## 3. Already Predicted vs Requires CMB Solver

| Parameter | AT already predicts? | Requires full CMB solver? |
|---|---|---|
| Ω_b | No (contingent) | No |
| Ω_DM | Partially (order of magnitude) | No |
| H0 | No (primitive) | No |
| Λ | **Yes** (scaling Λ~1/√N) | No |
| θ* | No (shift only, 0.5–1%) | **Yes** |
| n_s | No | **Yes** |
| A_s | No | **Yes** |
| τ | No | **Yes** |
| σ₈ | No (shift only) | **Yes** |

---

## 4. Compatibility Verdict

| Parameter | Compatible? | Basis |
|---|---|---|
| Ω_b | ✅ (accommodated) | value is a contingent draw; no tension claimed |
| Ω_DM | ✅ (order-of-magnitude) | Ω_DM ~ 0.1–1 natural; exact 0.27 unconstrained |
| H0 | ⚠️ (tension addressed) | H0 primitive; X046b suggests early dark energy may reconcile 67 vs 73 |
| Λ | ✅ (postdicted) | Λ ~ 1/√N reproduces the observed scale (ratio ≈ 0.48) |
| θ* | ⚠️ (prediction pending) | evolving Λ predicts 0.5–1% shift; needs precision CMB (CMB-S4) |
| n_s / A_s / τ / σ₈ | ❌ (uncomputed) | no AT derivation; require a full CMB solver |

---

## 5. Bottom Line

| Verdict | Count | Parameters |
|---|---|---|
| ✅ Compatible | 3 | Ω_b, Ω_DM, Λ |
| ⚠️ Pending | 2 | H0, θ* |
| ❌ Uncomputed | 4 | n_s, A_s, τ, σ₈ |

AT is **compatible with Planck at the background level** (Ω_b, Ω_DM, Λ — one of
which, Λ, it genuinely postdicts via Λ~1/√N), but has **no CMB-spectrum
derivation**: θ*, n_s, A_s, τ, σ₈ all require a full acoustic solver that does not
exist in the repository.
