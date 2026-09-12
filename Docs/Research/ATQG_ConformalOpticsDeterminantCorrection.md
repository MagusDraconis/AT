# AT-QG Phase 320 — Conformal Optics Determinant Correction

**Status:** COMPLETE — **CORRECTION RECORD** (amends QG207, QG212 and QG32; issues no new physics claim)
**Date:** 2026-09-12
**Amends:** QG207 (`MetricAnsatzUniqueness`), QG212 (`ConformalOpticsResolution`), QG32 (`TRMCompatibilityAudit`),
QG208 (`HawkingTemperatureWithPsi` citation), QG33 (`TRMasUVCompletion`)
**Core:** `AT.Core/ResearchXH/MetricAnsatzAudit.cs`, `MetricAnsatzUniqueness.cs`,
`ConformalOpticsResolution.cs`, `TRMCompatibilityAudit.cs`, `TRMasUVCompletion.cs`
**Tests amended:** `ATQG_Phase207_MetricAnsatzUniquenessTests`, `ATQG_Phase212_ConformalOpticsResolutionTests`,
`ATQG_Phase32_TRMCompatibilityAuditTests`, `G4A_Phase0_MetricAnsatzAuditTests`
**Verification audit:** `ResearchY-G_025` (`Docs/ResearchY/G_GravitySource/ResearchY-G_025.md`)
**Method:** exact algebra; deterministic; no new primitives

---

## 1. Why a correction is issued

The optics resolution (QG212) was accepted in `ResearchY-G_024` on the strength of its own documentation. An
independent verification of the underlying arithmetic (**G_025**) found a real defect in the phase it rests on
(QG207), plus a second defect in how QG212 computes its headline quantity.

**The optics conclusion is NOT overturned — it is restored on derived grounds.** What is withdrawn is one
premise and one origin-score basis.

---

## 2. Defect 1 — off-by-one in the ψ-perturbed determinant

`MetricAnsatzAudit` stated, and `MetricAnsatzUniqueness` used as a premise:

> *"g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)) … det g = −ρ^(2/d)e^{2ψ}·(ρ^(2/d)e^{−2ψ/(d−1)})^(d−1)
> = −ρ², so √(−g) = ρ (unchanged)"*

and `PerturbedVolumeElement(x, d, b)` returned `Profile(x, a)` = ρ **by construction** — it never computed a
determinant, so `PsiPerturbationPreservesMeasure()` could only return `true`.

**The exponent is wrong: there are `d` spatial factors, not `d − 1`** (one `g_ii` per spatial direction).
Correctly:

```text
det g       = −ρ^(2/d)e^{2ψ} · (ρ^(2/d)e^(−2ψ/(d−1)))^d = −ρ^(2(d+1)/d) · e^(−2ψ/(d−1))
√(−det g)   = ρ^((d+1)/d) · e^(−ψ/(d−1))          4-volume
√(det g_ij) = ρ · e^(−dψ/(d−1))                   spatial — the counting measure
```

| `b` (ψ = b·x), `x = 1` | former claim | corrected spatial | error | corrected 4-volume | error |
|---:|---:|---:|---:|---:|---:|
| 0.0 | 2.0000000000 | 2.0000000000 | 0.00 % | 2.5198420998 | 25.99 % |
| 0.3 | 2.0000000000 | 1.2752563032 | **36.24 %** | 2.1688481946 | 8.44 % |
| 0.5 | 2.0000000000 | 0.9447331055 | **52.76 %** | 1.9624550005 | 1.88 % |
| −3.0 | 2.0000000000 | 180.0342626010 | **8901.71 %** | 11.2931487976 | 464.66 % |

The error is **unbounded in ψ**, which is why the shipped tests could not catch it.

**Consequence.** `√(−g) = ρ is preserved for ANY ψ` is **FALSE**. So is `det g = −ρ² is independent of ψ`.
In fact **ψ = 0 is the *only* member of the family that preserves the counting measure** — which *strengthens*
the selection argument rather than weakening it.

---

## 3. Defect 2 — γ was a hard-coded constant, never computed

`ConformalOpticsResolution` read:

```csharp
public static double GammaPsiZero()    => NonTensorLensing.ConformalGamma();   // => −1.0  (constant)
public static double GammaPsiNonZero() => NonTensorLensing.GrGamma();         // => +1.0  (constant)
```

No code path computed γ from the QG207 metric. Origin-score items 2 and 3 were therefore arithmetic on a
constant — `(1 + 1)/2 = 1` and `Shapiro(1) = (1+1)/2·2 = 2` — and could not fail.

Consequently nothing could detect that **the coded metric ψ = b·x does not have γ = +1 anywhere**:

| `b` | x = 0.1 | 0.5 | 1.0 | 2.0 |
|---:|---:|---:|---:|---:|
| 0.3 | +0.3352 | +0.0022 | −0.0930 | −0.0694 |
| 0.5 | +0.3772 | +0.1054 | +0.0112 | −0.0037 |

γ = +1 requires `ψ = −2σ(d−1)/(d−2) = −4σ` at `d = 3`, which is `−(4/3)ln(1+ax²)` — **quadratic** at small x,
whereas the code uses `ψ = b·x`, **linear**.

---

## 4. Corrections applied

| # | site | before | after |
|---|---|---|---|
| 1 | `MetricAnsatzAudit.PerturbedVolumeElement` | returned ρ by construction | **computes** `ρ·e^(−dψ/(d−1))` |
| 2 | `MetricAnsatzAudit.PerturbedDet` / `PerturbedVolumeElement4D` | absent | **added** — the two closed forms |
| 3 | `MetricAnsatzUniqueness.PsiPerturbationPreservesMeasure` | `true` for every ψ | **`false`** |
| 4 | `MetricAnsatzUniqueness.PsiPerturbationBreaksMeasure` / `ConformalIsTheMeasurePreservingMember` | absent | **added**, both `true` |
| 5 | `MetricAnsatzUniqueness` class doc + `Classify` reason | "ψ provides counting-preserving alternatives" | corrected to "ψ alternatives are NOT counting-preserving; ψ = 0 is the unique measure-preserving member" |
| 6 | `ConformalOpticsResolution.GammaPsiZero` / `GammaPsiNonZero` | constants −1.0 / +1.0 | **computed from the metric** |
| 7 | `ConformalOpticsResolution.GammaFromPsiMetric` / `GammaFromPsiMetricFirstOrder` / `PsiForGrOptics` / `GammaPsiNonZeroExact` | absent | **added** |
| 8 | `ConformalOpticsResolution.ConformalIsRestrictedSector` | rested on defect 1 | rebased on `ConformalIsTheMeasurePreservingMember ∧ PsiSectorChangesObservables` |
| 9 | `ConformalOpticsResolution.Redshift` doc | "survives in BOTH sectors (g_00 = −ρ^(2/d) alone)" | corrected: the ψ ≠ 0 clock carries `e^(ψ)`; `PsiClockShiftFactor` / `PsiSectorShiftsClock` **added** |
| 10 | `TRMCompatibilityAudit.MetricOriginPreserved` + matrix row | `"metric-origin" => "UNCHANGED"` | **`"MODIFIED"`** |
| 11 | `TRMasUVCompletion.TrmCoreVolumeElement` doc | "volume-preserving → regular core unchanged" | corrected |
| 12 | `HawkingTemperatureWithPsi` class doc | cited "preserves √(−g) = ρ" | corrected (**its own ψ exponent `d/(d−1)` was already correct**) |

---

## 5. What survives

| claim | status |
|---|---|
| γ = −1 for the ψ = 0 conformal slice (`g = ρ^(2/d)η`) | **STANDS** — now computed, exactly −1 |
| γ = +1 for the ψ ≠ 0 sector | **STANDS** — now computed, exactly +1 at the **derived** ψ = −4σ, to first order |
| lensing = Shapiro = frame dragging = 0 at ψ = 0; full GR at ψ ≠ 0 | **STANDS** |
| conformal no-lensing is a **restricted sector** | **STANDS**, on corrected grounds |
| "the ψ sector preserves the counting measure" | **REFUTED** (off-by-one) |
| "ψ preserves the metric origin" / `metric-origin => UNCHANGED` | **REFUTED** → **MODIFIED** |
| "γ = +1 as a property of the coded ψ = b·x metric" | **REFUTED** — it was a constant |
| the two-sector classification `OPTICS RESOLVED` | **RESTORED** (4/4) — but on the corrected basis, with the boundary below |

**Corrected TRM compatibility matrix:** 4 UNCHANGED (counting measure, matter-deficit, α=0 attractor, critical
branching) · **2 MODIFIED** (metric origin, Einstein structure) · 0 BROKEN.

---

## 6. The boundary this exposes

In the QG207 parametrisation, γ = +1 requires ψ = −4σ, and then:

* the **exact** (non-linearised) γ is `e^(6σ) = ρ²` at `d = 3` — equal to +1 **only in the weak field**;
* the **clock** picks up `√(−g_00) = ρ^(1/d)e^(ψ)`, i.e. a factor `e^(−4σ) = ρ^(−4/3)` — 2.78e−9 at the Earth's
  surface (invisible), but O(1) at compactness.

So the `O(x²)` form of the completion is **load-bearing and still unspecified**. This is the ψ sector's
documented boundary (`AT.Core` now carries `PsiClockShiftFactor` / `PsiSectorBreaksMeasure` / the exact-γ
accessor so it can be exercised rather than assumed).

---

## 7. Scope

**No new physics claim is issued by this phase.** It corrects arithmetic and rewires γ to be computed. The
optics conclusion of QG212 is restored; QG207's measure-preservation premise, its determinant identity, the
TRM `metric-origin` row, and QG212's hard-coded γ are withdrawn and replaced.

**Downstream: none of the earlier committed phases loses a claim it derived from γ or from the measure in any
way other than those listed above** — the G-chain's own g₀₀-only results (G_004, G_009, G_015–G_020) never
touched these paths.
