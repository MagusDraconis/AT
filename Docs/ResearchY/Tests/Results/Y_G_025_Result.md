# Y_G_025_Result.md — ResearchY-G_025 Optics Determinant Correction Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_025_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.04 s) — group G total **210/210 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_025"`
**Correction record:** `Docs/Research/ATQG_ConformalOpticsDeterminantCorrection.md` (AT-QG phase 320)

## Summary

**Why this audit exists:** G_024 restored the AT-QG optics resolution (QG212) **on the strength of its own documentation**. The instruction was to *not* believe the document and to try to find errors in it. This audit verified the arithmetic independently — and found **two real defects**.

**The optics conclusion survives; one premise and one origin-score basis do not.**

## Defect 1 — off-by-one in the ψ-perturbed determinant

`MetricAnsatzAudit` claimed `det g = −ρ²` for **any** ψ, and `PerturbedVolumeElement(x, d, b)` returned `Profile(x, a)` = ρ **by construction** — it never computed a determinant. **The spatial block has `d` factors, not `d − 1`.**

```
det g       = −ρ^(2(d+1)/d) · e^(−2ψ/(d−1))
√(−det g)   = ρ^((d+1)/d) · e^(−ψ/(d−1))          4-volume
√(det g_ij) = ρ · e^(−dψ/(d−1))                   spatial — the counting measure
```

| `b`, `x = 1` | former claim | corrected spatial | error | corrected 4-volume | error |
|---:|---:|---:|---:|---:|---:|
| 0.0 | 2.0000000000 | 2.0000000000 | 0.00 % | 2.5198420998 | 25.99 % |
| 0.3 | 2.0000000000 | 1.2752563032 | **36.24 %** | 2.1688481946 | 8.44 % |
| 0.5 | 2.0000000000 | 0.9447331055 | **52.76 %** | 1.9624550005 | 1.88 % |
| −3.0 | 2.0000000000 | 180.0342626010 | **8901.71 %** | 11.2931487976 | 464.66 % |

The error is **unbounded in ψ** — which is why the shipped tests (which assert `sameVolume == true`) could never catch it.

**Consequence:** *"√(−g) = ρ is preserved for ANY ψ"* is **FALSE**. **ψ = 0 is the only member of the family that preserves the counting measure** — which *strengthens* the selection argument.

## Defect 2 — γ was a hard-coded constant

```csharp
public static double GammaPsiZero()    => NonTensorLensing.ConformalGamma();  // => −1.0
public static double GammaPsiNonZero() => NonTensorLensing.GrGamma();        // => +1.0
```

No path computed γ from the metric. Origin-score items 2/3 were arithmetic on a constant — `(1+1)/2 = 1`, `Shapiro(1) = 2` — and **could not fail**. So nothing detected that **the coded metric ψ = b·x has γ ≠ +1 anywhere**:

| `b` | x = 0.1 | 0.5 | 1.0 | 2.0 |
|---:|---:|---:|---:|---:|
| 0.3 | +0.3352 | +0.0022 | −0.0930 | −0.0694 |
| 0.5 | +0.3772 | +0.1054 | +0.0112 | −0.0037 |

γ = +1 needs `ψ = −4σ`, i.e. `−(4/3)ln(1+ax²)` — **quadratic** at small x; the code uses **linear** `ψ = b·x`.

## Corrections applied (12 sites, 5 AT.Core files)

| site | before | after |
|---|---|---|
| `PerturbedVolumeElement` | returned ρ by construction | **computes** `ρ·e^(−dψ/(d−1))` |
| `PerturbedDet`, `PerturbedVolumeElement4D` | absent | **added** |
| `PsiPerturbationPreservesMeasure` | `true` for every ψ | **`false`** |
| `PsiPerturbationBreaksMeasure`, `ConformalIsTheMeasurePreservingMember` | absent | **added**, both `true` |
| `GammaPsiZero` / `GammaPsiNonZero` | constants −1.0 / +1.0 | **computed from the metric** |
| `GammaFromPsiMetric`, `GammaFromPsiMetricFirstOrder`, `PsiForGrOptics`, `GammaPsiNonZeroExact` | absent | **added** |
| `ConformalIsRestrictedSector` | rested on defect 1 | rebased on the corrected member test |
| `Redshift` doc + `PsiClockShiftFactor`, `PsiSectorShiftsClock` | "survives in BOTH sectors" | corrected: the ψ ≠ 0 clock carries `e^(ψ)` |
| `TRMCompatibilityAudit` `"metric-origin"` | **`UNCHANGED`** | **`MODIFIED`** |
| `TRMasUVCompletion`, `HawkingTemperatureWithPsi` citation | volume-preserving core; false premise | corrected |

**Corrected TRM compatibility matrix:** **4 UNCHANGED** (counting measure, matter-deficit, α=0 attractor, critical branching) · **2 MODIFIED** (metric origin, Einstein structure) · 0 BROKEN.

## Derived values — γ is now computed, not asserted

| ρ | γ(ψ=0, exact) | γ(ψ=−4σ, 1st order) | γ(ψ=−4σ, exact) |
|---:|---:|---:|---:|
| 1.000001 | **−1.0000000000** | **+1.0000000000** | +1.0000020001 |
| 1.5 | **−1.0000000000** | **+1.0000000000** | +2.2500000000 |
| 2.0 | **−1.0000000000** | **+1.0000000000** | +4.0000000000 |

Exact value at ψ = −4σ is `e^(6σ) = ρ²` at `d = 3` — γ = +1 holds **only in the weak field**.

## What survives

| claim | status |
|---|---|
| γ = −1 for `g = ρ^(2/d)η` | **STANDS** |
| γ = +1 for ψ ≠ 0 | **STANDS** — now from the **derived** ψ = −4σ |
| lensing = Shapiro = frame dragging = 0 at ψ = 0; full GR at ψ ≠ 0 | **STANDS** |
| conformal no-lensing is a **restricted sector** | **STANDS**, on corrected grounds |
| "the ψ sector preserves the counting measure" | **REFUTED** (off-by-one) |
| `metric-origin => UNCHANGED` | **REFUTED** → **MODIFIED** |
| "γ = +1 as a property of ψ = b·x" | **REFUTED** — it was a constant |
| `OPTICS RESOLVED` (4/4) | **RESTORED** — on the corrected basis, with the §5 boundary |

Scorecard of QG212's origin-score points: **1 of 4 substantive** before the fix (only γ = −1 was real and verifiable), all 4 now backed by derived arithmetic.

## Boundary exposed

1. The exact (non-linearised) γ at ψ = −4σ drifts as `ρ²` — +1 only in the weak field.
2. The same ψ shifts the **clock** law (`√(−g_00) = ρ^(1/d)e^(ψ)`) by `e^(−4σ) = ρ^(−4/3)` — 2.78e−9 at the Earth's surface (invisible; GPS tests only 0.2 %), but O(1) at compactness.

So the `O(x²)` completion is **load-bearing and still unspecified**.

## Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the corrected determinant identities; `√(det g_ij) = ρ ⟺ ψ = 0`; γ = −1 (ψ=0) and γ = +1 (1st order, ψ = −4σ) computed from the metric; the exact drift `e^(6σ) = ρ²`; the clock factor `e^(−4σ)`. |
| **BOUNDARY** | the exact (non-linearised) ψ completion — γ drifts and the clock shifts, so strong-field behaviour depends on the unspecified `O(x²)` form. |
| **REFUTED** | the off-by-one determinant and everything resting on it; the hard-coded γ; `metric-origin => UNCHANGED`. |

## Tests

| test | asserts | result |
|---|---|---|
| `Y_G_025_DeterminantCorrection` | closed forms vs the corrected determinant, 5 (x, b) values | ✅ |
| `Y_G_025_MeasureIsNotPreserved` | `PreservesMeasure = false`, `BreaksMeasure = true`, conformal is the unique member | ✅ |
| `Y_G_025_GammaIsDerivedNotAsserted` | γ matches `(g_ii−1)/(g_00+1)`; `PsiSectorChangesObservables` | ✅ |
| `Y_G_025_CodedPsiFormCannotGiveGammaPlusOne` | exact γ for `ψ = b·x` ≠ +1; `PsiForGrOptics` is quadratic | ✅ |
| `Y_G_025_PsiShiftsTheClock` | `e^(−4σ)` factor; `PsiClockShiftFactor(ρ)`; Earth at 2.78e−9 | ✅ |
| `Y_G_025_PropagationCorrected` | QG212 conclusion restored on the corrected basis | ✅ |
| `Y_G_025_Run` | the full report | ✅ |

**Headline:** the AT-QG optics arithmetic contains a **`d`-vs-`(d−1)` off-by-one** in the ψ-perturbed determinant (36 % at b = 0.3, 8902 % at b = −3, unbounded in ψ) and a **hard-coded γ** — both corrected at twelve sites, the optics conclusion restored on derived grounds, the measure-preservation premise refuted, the TRM `metric-origin` row moved to MODIFIED, and the strong-field completion left as the documented boundary.
