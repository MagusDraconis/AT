# ResearchY-G_025 — Optics Determinant Correction Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_025 (permanent)
**Title:** Optics Determinant Correction Audit — verifying the AT-QG optics arithmetic and correcting it
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_025.md`
**Depends on:** G_024 (which restored QG212 on the strength of its documentation, and which this audit verifies), G_023, G_022, G_021; AT-QG **QG207** (`MetricAnsatzUniqueness`), **QG212** (`ConformalOpticsResolution`), **QG32** (`TRMCompatibilityAudit`), **QG33** (`TRMasUVCompletion`), **QG208** (`HawkingTemperatureWithPsi`), QG26, QG186, QG44, QG285/QG286/QG292
**Correction record:** `Docs/Research/ATQG_ConformalOpticsDeterminantCorrection.md` (QG320)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_025_Tests.cs` (7/7 PASSED, ~0.04 s)

## Purpose

G_024 restored the AT-QG optics resolution (QG212) **on the strength of its own documentation**. This audit does what G_024 did not: it **verifies the underlying arithmetic independently** — and finds a real defect. The instruction that motivated it was to *not* believe the document and to try to find errors in it.

**Two defects were found.** The optics *conclusion* survives; one *premise* and one *origin-score basis* do not.

## 1. Defect 1 — off-by-one in the ψ-perturbed determinant

`AT.Core/ResearchXH/MetricAnsatzAudit.cs` stated:

> *"g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)) … det g = −ρ^(2/d)e^{2ψ}·(ρ^(2/d)e^{−2ψ/(d−1)})^(d−1)
> = −ρ², so √(−g) = ρ (unchanged)"*

and `PerturbedVolumeElement(x, d, b)` returned `Profile(x, a)` = ρ **by construction**. **The spatial block has `d` factors, not `d − 1`.** Correctly:

```text
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

**The error is unbounded in ψ**, so the hard-coded return could never be caught by the shipped tests.

**Consequence:** *"√(−g) = ρ is preserved for ANY ψ"* is **FALSE**; so is *"det g = −ρ² is independent of ψ"*. In fact **ψ = 0 is the only member that preserves the counting measure** — which *strengthens* the selection argument.

## 2. Defect 2 — γ was a hard-coded constant

```csharp
public static double GammaPsiZero()    => NonTensorLensing.ConformalGamma();   // => −1.0  (constant)
public static double GammaPsiNonZero() => NonTensorLensing.GrGamma();         // => +1.0  (constant)
```

No path computed γ from the metric, so origin-score items 2 and 3 were arithmetic on a constant — `(1+1)/2 = 1` and `Shapiro(1) = 2` — and could not fail. Consequently nothing detected that **the coded metric ψ = b·x has γ ≠ +1 anywhere**:

| `b` | x = 0.1 | 0.5 | 1.0 | 2.0 |
|---:|---:|---:|---:|---:|
| 0.3 | +0.3352 | +0.0022 | −0.0930 | −0.0694 |
| 0.5 | +0.3772 | +0.1054 | +0.0112 | −0.0037 |

γ = +1 needs `ψ = −4σ`, i.e. `−(4/3)ln(1+ax²)` — **quadratic** at small x; the code uses `ψ = b·x`, **linear**.

## 3. Corrections applied

Twelve sites across five AT.Core files (`MetricAnsatzAudit`, `MetricAnsatzUniqueness`, `ConformalOpticsResolution`, `TRMCompatibilityAudit`, `TRMasUVCompletion`, plus the `HawkingTemperatureWithPsi` citation). The determinant now computes; γ is now **derived**; the measure premise is corrected; the TRM matrix row moves.

**Corrected TRM compatibility matrix:** **4 UNCHANGED** (counting measure, matter-deficit, α=0 attractor, critical branching) · **2 MODIFIED** (metric origin ← *was UNCHANGED*, Einstein structure) · 0 BROKEN.

## 4. What survives

| claim | status |
|---|---|
| γ = −1 for `g = ρ^(2/d)η` | **STANDS** — now computed, exactly −1 |
| γ = +1 for ψ ≠ 0 | **STANDS** — now computed, exactly +1 at the **derived** ψ = −4σ, to first order |
| lensing = Shapiro = frame dragging = 0 at ψ = 0; full GR at ψ ≠ 0 | **STANDS** |
| conformal no-lensing is a **restricted sector** | **STANDS**, on corrected grounds |
| "the ψ sector preserves the counting measure" | **REFUTED** (off-by-one) |
| `metric-origin => UNCHANGED` | **REFUTED** → **MODIFIED** |
| "γ = +1 as a property of the ψ = b·x metric" | **REFUTED** — it was a constant |
| `OPTICS RESOLVED` (4/4) | **RESTORED** — but on the corrected basis, with the boundary in §5 |

Derived values (from the metric):

| ρ | γ(ψ=0, exact) | γ(ψ=−4σ, 1st order) | γ(ψ=−4σ, exact) |
|---:|---:|---:|---:|
| 1.000001 | **−1.0000000000** | **+1.0000000000** | +1.0000020001 |
| 1.5 | **−1.0000000000** | **+1.0000000000** | +2.2500000000 |
| 2.0 | **−1.0000000000** | **+1.0000000000** | +4.0000000000 |

The exact value is `e^(6σ) = ρ²` at `d = 3`: γ = +1 holds **only in the weak field**.

## 5. The boundary this exposes

1. The **exact (non-linearised)** γ at ψ = −4σ drifts as `ρ²` — +1 only in the weak field.
2. The same ψ shifts the **clock** law (`√(−g_00) = ρ^(1/d)e^(ψ)`) by `e^(−4σ) = ρ^(−4/3)` — 2.78e−9 at the Earth's surface (invisible; GPS is a 0.2 % test), but O(1) at compactness.

So the `O(x²)` form of the completion is **load-bearing and still unspecified**. `AT.Core` now exposes `PsiClockShiftFactor`, `PsiSectorShiftsClock`, `PsiSectorBreaksMeasure` and `GammaPsiNonZeroExact` so this can be exercised rather than assumed.

## 6. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the corrected determinant identities (`det g`, 4-volume, spatial element); `√(det g_ij) = ρ ⟺ ψ = 0`; γ = −1 at ψ = 0 and γ = +1 (first order) at the derived ψ = −4σ, **both computed from the metric**; the exact drift `e^(6σ) = ρ²`; the clock shift factor `e^(−4σ)`. |
| **BOUNDARY** | the exact (non-linearised) ψ completion — γ drifts as `ρ²` and the clock shifts by `e^(−4σ)`, so the strong-field behaviour depends on the `O(x²)` completion, which is still unspecified. |
| **REFUTED** | the off-by-one determinant and everything resting on it: *"√(−g) = ρ unchanged for ANY ψ"*, *"the ψ sector gives counting-preserving alternatives"*, *"`metric-origin` ⇒ UNCHANGED"*, and the hard-coded γ. |

## 7. Classification and caveats

**The AT-QG optics conclusion is RESTORED, not overturned** — but its measure-preservation premise and one origin-score basis are withdrawn, and γ is now computed rather than asserted. Deterministic: exact algebra on AT's own constructions. No new primitive.

* The correctness of the *numerical conclusion* γ = +1 at ψ = −4σ is confirmed independently here and in G_021/G_024 — the defect was in *how* QG212 obtained it, not in the value.
* `HawkingTemperatureWithPsi`'s own ψ exponent `d/(d−1) = 3/2` was **re-derived independently and is correct**; only its citation of the false measure premise was fixed.
* The G-chain's own `g₀₀`-only results (G_004, G_009, G_015–G_020) never touched these paths and are unaffected.

## Open problems (OP1–OP5)

1. **Specify the exact (non-linearised) ψ completion** and determine whether the compactness redshift stays positive (G_024's flag).
2. **Which route does AT require** — the QG207 form (clock shift `e^(−4σ)`) or a measure-preserving alternative (if any exists)?
3. **Is the ψ family the only non-conformal candidate?** G_023's `k = B − A` freedom suggests a broader anisotropic family; its measure properties are unclassified.
4. **Sweep the remaining AT.Core determinant/volume helpers** for the same `d`-vs-`(d−1)` miscount (the G_025 scan covered `MetricAnsatzAudit`, `TRMCompatibilityAudit`, `HorizonThermodynamics`, `BlackHoleEntropy`, `InformationDimension`; the latter three are correct).
5. **Re-audit `EinsteinRecoveredAtMetricPower()`** — its body is `return g11 > 0;`, a vacuous check that nonetheless scores a full origin-score point.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_025_Tests.cs` — **7/7 PASSED** (~0.04 s)
**Group total:** G_001–G_025 = **210/210 PASSED** (~2 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_025"`

**Headline:** the AT-QG optics arithmetic contains a **`d`-vs-`(d−1)` off-by-one** in the ψ-perturbed determinant (36 % error at b = 0.3, 8902 % at b = −3, unbounded in ψ) and a **hard-coded γ** — both now corrected at twelve sites, with the optics conclusion restored on derived grounds, the measure-preservation premise refuted, the TRM `metric-origin` row moved to MODIFIED, and the strong-field completion left as the documented boundary.
