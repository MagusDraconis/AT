# Y_G_024_Result.md — ResearchY-G_024 Optics Reconciliation Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_024_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.03 s) — group G total 203/203 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_024"`

> ⚠ **Cross-reference — ResearchY-G_025.** This audit restored QG212 **on the strength of its own
> documentation**. The independent verification carried out in G_025 found two real defects in that basis: an
> off-by-one in the ψ-perturbed determinant (so *"√(-g) = ρ is preserved for ANY ψ"* is FALSE —
> the error is unbounded in ψ), and a **hard-coded** γ that was never computed from the metric. The optics
> *conclusion* restored here **stands**, now on derived grounds; the measure-preservation premise and the
> hard-coded-γ origin-score basis do **not**. See `ResearchY-G_025.md`,
> `Docs/Research/ATQG_ConformalOpticsDeterminantCorrection.md` (AT-QG phase 320), and the amended QG207/QG212/QG32 docs.
> ⚠ **CORRECTION — ResearchY-G_028 (Clock Sector Closure Audit).** This audit's conclusion that the
> QG207 completion produces **"NO solar-system conflict"**, with a GPS bound `|psi| <= 2e-3` called
> **7.2e5x looser** than required, is **WRONG IN KIND**. It treated the clock shift `e^(4x) - 1 = 2.78e-9` as
> an *additive* perturbation on a clock of 1. But the observable is the redshift **relative to infinity**, and
> the conformal law already predicts `z ~ +x = 6.957e-10`. The shift 2.78e-9 is **4x the entire effect** and it
> **REVERSES ITS SIGN**: `A = sigma + psi` with `psi = -4 sigma` gives `A = +3x > 0`, so the potential becomes
> POSITIVE and every gravitating body is **blueshifted**. The correct bound is
> **`|psi| <= 1.391e-12`** (the GPS precision itself) — **1.44e9x TIGHTER** than quoted — and
> `psi = -4 sigma` is excluded at **2000 sigma** by the Earth redshift alone. It follows that QG212's
> "physical sector" (psi != 0 at psi = -4 sigma) is a REPULSIVE geometry and is behaviourally refuted, and that
> the optics fix **cannot** come from the trace-free direction. See `ResearchY-G_028.md`.
## Summary

**Why this audit exists:** the AT-QG programme **resolved conformal optics before the G-chain began** — `Docs/Research/ATQG_ConformalOpticsResolution.md` (**QG212**), Status **COMPLETE — OPTICS RESOLVED**, tests `ATQG2120/2121/2122` passing, core class `AT.Core/ResearchXH/ConformalOpticsResolution.cs`. The G-chain re-derived part of it but **mis-stated its status**. This audit restores the canonical result and retracts three erroneous claims.

## The canonical result (QG212)

| Sector | PPN `γ` | Lensing | Shapiro | Frame dragging | Redshift |
|---|---|---|---|---|---|
| `ψ = 0` (conformal `g = ρ^(2/d)η`) | −1 | 0 | 0 | 0 | yes |
| `ψ ≠ 0` (QG207 completion) | **+1** | **GR** | **GR** | **restored** | yes |

> *"Not a numerical artifact — γ = −1 is exact within the ψ = 0 slice. Not physical GR — the ψ = 0 slice is an isotropic assumption. **The physical sector is ψ ≠ 0.**"*
> Method line: *"TRM/D96 only, deterministic, **no new primitives**."*

## The three retractions

| # | where | claim | status |
|---|---|---|---|
| 1 | G_021, G_022, G_023 | `ψ` is *"a MINIMAL NEW PRIMITIVE (QG24)"* | **WITHDRAWN** — superseded by QG285/QG286/QG292 |
| 2 | G_022 §6 | *"the `ψ` route is EMPTY"* | **WITHDRAWN** — the derivation assumed the `ρ`-only clock law, which *is* the `ψ = 0` slice |
| 3 | G_023 | verdict **REFUTED** because *"γ = +1 requires a new primitive"* | **reason void** — rests on #1 |

The trace/traceless arithmetic: `A_ij` symmetric at `d = 3` has **6 components = 1 trace (ρ) + 5 traceless**, of which **2 are transverse-traceless** (`ψ`, spin-2). Minimal primitive set = **{Difference, η}** (QG292).

## What survives — and is sharpened

**(a)** The `ψ = 0` slice is excluded **by measurement** at a computable significance: **Cassini 8.6957e4 σ**, VLBA 6.6660e3 σ, Gaia 1.2481e2 σ. QG26 gives the bare `γ = −1`; G_021 gives the number.

**(b)** The invariant split. To first order `γ = −B/A` from the two exponents, so

```
ψ = 0   ⟺   A = B = σ    ⟺   γ = −1     (the restricted isotropic slice)
γ = +1  ⟺   A + B = 0                   (h₀₀ = h_ii: the perturbations cancel)
```

| route to `A + B = 0` | `A` | `B` | `k := B − A` |
|---|---|---|---|
| keep the clock law, `B = −σ = +x` | `σ` | `−σ` | **`2x`** — G_023's case |
| QG207 completion, `ψ = −4σ` | `−3σ` | `+3σ` | `−6x` |

So **G_023's `γ = −1 + k/x` with `k = 2x` is the clock-law-preserving special case** — correct but not the only route; the general statement is `A + B = 0`, and `ψ = 0 ⟺ A = B` **is** QG212's "isotropic assumption".

## The one genuine open item this audit adds

`γ = +1` requires `ψ = −4σ`, so the completion is **not redshift-neutral** (`√(−g₀₀) = ρ^(1/d)e^(ψ)`):

| location | `ψ` required | relative clock shift `e^(4x) − 1` |
|---|---:|---:|
| Earth surface | 2.784532e−9 | **2.784532e−9** |
| Sun surface | 8.490012e−6 | **8.490012e−6** |

Both far below the verified tests (GPS 0.2 %; Cassini 2.3e−5) ⟹ **no solar-system conflict**; the implied bound `|ψ| ≲ 2e−3` from GPS is **7.2e5× looser** than required.

**But** the leading-order redshift `z = e^(−3x) − 1` turns **negative** at compactness:

| `x` | `z` at `ψ = −4σ` | `ρ`-only `z = e^x − 1` |
|---:|---:|---:|
| 0.10 | **−0.259182** | +0.105171 |
| 0.172317 | **−0.403664** | +0.188054 |
| **0.247002** | **−0.523366** | +0.280182 |

PPN fixes only the **first-order** `ψ = −4σ`, so the `O(x²)` form of the completion is **load-bearing and uncomputed** — a **BOUNDARY flag**, not a refutation.

## Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the two-sector structure as QG212 states it · the `ψ = 0` slice's measured exclusion · the invariant split (`ψ = 0 ⟺ A = B`; `γ = +1 ⟺ A + B = 0`) with G_023's `k = 2x` and the QG207 `k = −6x` · `ψ` introduces no new primitive |
| **BOUNDARY** | the exact (nonlinear) completion is unspecified — the `O(x²)` form is load-bearing for the strong-field redshift; and `ψ`'s ontological status is a documented boundary (QG299) |
| **REFUTED** | G_022 §6 and G_023's verdict reason — **withdrawn**. *(The `ψ = 0` slice remains excluded at 8.6957e4 σ; that stands.)* |

## Classification and caveats

**No reclassification of the QG212 result — this audit RESTORES it.** D_040 untouched; no canonical claim, value or equation changes; no new primitive. Deterministic: exact algebra on AT's own constructions.

* `γ = +1` in the `ψ ≠ 0` sector is a **first-order** statement; the exact value at `ψ = −4σ` is `e^(−6x)`.
* The retractions correct **G-chain** claims, not AT-QG results.

## Open problems (OP1–OP5)

1. **What is the exact (nonlinear) `ψ` completion?** The `O(x²)` form is load-bearing and uncomputed.
2. **Does `z` stay positive at compactness**, keeping the NICER redshift programme (G_019/G_020) valid?
3. **Which route does AT require** — `A = σ` (redshift-neutral, volume cost `1/ρ`) or QG207 (first-order clock shift)?
4. What bound does the **GPS/redshift precision** place on `ψ` across the full compactness range?
5. Is the **`η`-reference import** (QG292) the last irreducible import in the tensor sector?
