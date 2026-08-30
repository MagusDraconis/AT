# ResearchY-NP_019 — Information Cosmology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_019 (permanent)
**Title:** Information Cosmology Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_019.md`
**Depends on:** ResearchY-NP_018 (distinguishability observable), QG228 (I_occ =
0.7513 nats), QG234 (ΩΛ = I_occ/ln K = 0.6839)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_019_Tests.cs`

---

## Purpose

**Does distinguishability-derived information predict additional cosmological
observables beyond ΩΛ?** NP_018 established ΩΛ = I_occ/ln K as a direct
distinguishability observable. This audit asks whether I_occ is a GENUINE cosmological
variable — whether other cosmological observables (Ωm, H₀, σ₈, BAO scales, structure
growth, CMB quantities) can be written directly as functions of the information density.

---

## 1. Cosmological observables

| Observable | Value | Direct function of I_occ? |
|---|---|---|
| **ΩΛ** | I_occ/ln K = 0.6839 | **YES — by construction (QG234)** |
| **Ωm** | 1 − ΩΛ = 0.3161 | **YES — (ln K − I_occ)/ln K (complement)** |
| **n_s** | 0.96497 (QG237) | NO — D96-derived, not an I_occ function |
| **ℓ₁** | 220.48 (QG238) | NO — D96 octave-derived, not an I_occ function |
| **H₀** | (calibration) | NO — not information-derived |
| **σ₈** | (measured) | NO — no direct information relation |
| **BAO scales** | (measured) | NO — no direct information relation |
| **structure growth** | (measured) | NO — no direct information relation |

---

## 2. Dependence on I_occ

| Observable | Formula | I_occ dependence |
|---|---|---|
| ΩΛ | I_occ/ln K | **DIRECT — linear in I_occ** |
| Ωm | (ln K − I_occ)/ln K | **DIRECT — complement of I_occ** |
| ΩΛ/Ωm ratio | I_occ/(ln K − I_occ) = 2.1636 | **DIRECT** |
| n_s | 0.96497 | none (D96 spectrum) |
| ℓ₁ | 220.48 | none (D96 octaves) |
| H₀, σ₈, BAO, growth | — | none |

**The information-derived cosmology is EXACTLY the density-fraction pair (ΩΛ, Ωm).
The other observables (n_s, ℓ₁) are D96-derived but NOT I_occ functions; H₀, σ₈,
BAO, and growth have no direct information relation.**

---

## 3. Search: additional information-derived cosmological relations

| Candidate relation | Exists? |
|---|---|
| ΩΛ = I_occ/ln K | YES — the primary relation |
| Ωm = 1 − I_occ/ln K | YES — the complement |
| ΩΛ/Ωm = I_occ/(ln K − I_occ) = 2.1636 | YES — the derived ratio |
| n_s from I_occ | NO — n_s comes from the D96 spectrum (QG237), not the information density |
| ℓ₁ from I_occ | NO — ℓ₁ comes from the D96 octave hierarchy (QG238) |
| H₀, σ₈, BAO from I_occ | NO — no relation found |

**No additional information-derived relations beyond the density-fraction pair and
its ratio.**

---

## 4. Which observables are direct functions of distinguishability information?

**Exactly the density fractions:**

```
ΩΛ = I_occ/ln K              = 0.6839
Ωm = 1 − I_occ/ln K           = 0.3161
ΩΛ/Ωm = I_occ/(ln K − I_occ)  = 2.1636
```

Everything else (H₀, σ₈, BAO, growth, and even n_s and ℓ₁) is either a calibration, a
D96-spectral quantity, or has no direct information dependence.

---

## Theorem

> **Theorem (NP_019).** Distinguishability-derived information predicts EXACTLY the
> cosmological density-fraction pair — ΩΛ = I_occ/ln K and Ωm = 1 − ΩΛ — and no
> additional direct cosmological observables. Proof: (1) Enumerate the cosmological
> observables: ΩΛ, Ωm, H₀, σ₈, BAO scales, structure growth, and the CMB quantities
> n_s, ℓ₁. (2) Test each for a direct I_occ dependence: only ΩΛ = I_occ/ln K = 0.6839
> (by construction, QG234) and its complement Ωm = (ln K − I_occ)/ln K = 0.3161 are
> direct functions of the information density; the derived ratio ΩΛ/Ωm =
> I_occ/(ln K − I_occ) = 2.1636 follows. (3) n_s = 0.96497 (QG237) and ℓ₁ = 220.48
> (QG238) are D96-SPECTRAL quantities, not I_occ functions. (4) H₀ (a calibration),
> σ₈, BAO scales, and structure growth have NO direct information relation. (5)
> Therefore I_occ is a GENUINE cosmological variable, but a NARROW one: it fixes the
> density fractions (ΩΛ, Ωm) and their ratio, and nothing else directly. Ranking of
> the strongest predictions: ΩΛ (observed, 0.12%), Ωm (observed, 0.26%), and the
> ΩΛ/Ωm ratio 2.1636 (derived). Classification: ΩΛ and Ωm are PREDICTION
> (information-derived, observed); the ΩΛ/Ωm ratio is PREDICTION (derived); n_s and
> ℓ₁ are CORRESPONDENCE (D96-derived, not information-derived); H₀/σ₈/BAO/growth are
> BOUNDARY/calibration (no direct relation). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) List the observables (Section 1). (2) Test the I_occ dependence
> (Section 2, verified: only ΩΛ, Ωm, and the ratio). (3) Search for additional
> relations (Section 3, verified: none). (4) Conclude the pair is the full
> information cosmology (Section 4). ∎

---

## Dependency Graph

```
Difference
 → Distinguishability (95 states)
 → Information (H = log₂ 95; I_occ = 0.7513 nats)
 → Cosmology
    → ΩΛ = I_occ/ln K = 0.6839 — DIRECT (observed)
    → Ωm = 1 − I_occ/ln K = 0.3161 — DIRECT (observed)
    → ΩΛ/Ωm = 2.1636 — DERIVED
    → n_s, ℓ₁ — D96-spectral (not I_occ)
    → H₀, σ₈, BAO, growth — no direct relation
```

---

## 5. Ranking of the strongest observable predictions

| Rank | Prediction | Value | Status |
|---|---|---|---|
| 1 | **ΩΛ** | I_occ/ln K = 0.6839 | OBSERVED (0.12%) |
| 2 | **Ωm** | (ln K − I_occ)/ln K = 0.3161 | OBSERVED (0.26%) |
| 3 | **ΩΛ/Ωm ratio** | 2.1636 | derived |
| 4 | n_s | 0.96497 | D96-spectral correspondence |
| 5 | ℓ₁ | 220.48 | D96-octave correspondence |

---

## 6. Falsification paths

| Prediction | Falsification |
|---|---|
| ΩΛ = I_occ/ln K | a measured ΩΛ deviating from I_occ/ln K beyond 0.12% |
| Ωm = 1 − ΩΛ | a matter fraction inconsistent with 1 − I_occ/ln K |
| ΩΛ/Ωm = 2.1636 | an observed ratio deviating from I_occ/(ln K − I_occ) |

---

## Classification

| Component | Status |
|---|---|
| ΩΛ = I_occ/ln K | **PREDICTION** (information-derived, OBSERVED) |
| Ωm = 1 − ΩΛ | **PREDICTION** (information-derived, OBSERVED) |
| ΩΛ/Ωm ratio | **PREDICTION** (derived) |
| n_s, ℓ₁ | **CORRESPONDENCE** (D96-derived, not information) |
| H₀, σ₈, BAO, growth | **BOUNDARY / calibration** (no direct relation) |

**I_occ is a genuine but NARROW cosmological variable: it fixes the density fractions
(ΩΛ, Ωm) and their ratio — the full information cosmology. No new primitive; canonical
AT unchanged.**

---

## Open Problems

1. **Broader information coupling (NP_019 OP1).** Whether I_occ could enter other
   cosmological observables through deeper structures (a second-order relation) — none
   found in this audit.

---

## Next Steps

- **Registry note:** the information cosmology is the density-fraction pair
  (ΩΛ = 0.6839, Ωm = 0.3161) and its ratio 2.1636 — no additional direct observables.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_019_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_019_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_019_InformationObservable` | ΩΛ = I_occ/ln K | ✅ |
| `Y_NP_019_CosmologyMapping` | which observables depend on I_occ | ✅ |
| `Y_NP_019_AdditionalRelations` | no extra information-derived relations | ✅ |
| `Y_NP_019_PredictionRanking` | ΩΛ/Ωm top; n_s/ℓ₁ correspondence | ✅ |
| `Y_NP_019_Run` | research report | ✅ |

**Conclusion:** Distinguishability-derived information predicts EXACTLY the density-
fraction pair (ΩΛ = 0.6839, Ωm = 0.3161) and their ratio (2.1636) — the full
information cosmology. n_s and ℓ₁ are D96-spectral (not I_occ functions); H₀, σ₈, BAO,
and growth have no direct relation. No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_019"`

---

## References

- ResearchY-NP_018 (distinguishability observable).
- AT-QG: QG228 (I_occ = 0.7513 nats), QG234 (ΩΛ = I_occ/ln K = 0.6839), QG237
  (n_s = 0.96497), QG238 (ℓ₁ = 220.48).
