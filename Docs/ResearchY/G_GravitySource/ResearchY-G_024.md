# ResearchY-G_024 — Optics Reconciliation Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_024 (permanent)
**Title:** Optics Reconciliation Audit — restoring the AT-QG optics resolution and retracting three G-chain errors
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_024.md`
**Depends on:** **AT-QG QG212** (`Docs/Research/ATQG_ConformalOpticsResolution.md` — the canonical optics resolution), QG26 (γ = −1), QG186 (ψ restores frame dragging), QG207 (the ψ-completed metric), QG44 (Fierz–Pauli), QG285 / QG286 (the Difference duality; ψ as the Weyl content), QG292 (the η-removal result); and G_021, G_022, G_023 (the audits this one corrects)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_024_Tests.cs` (7/7 PASSED, ~0.03 s)

## Purpose

The AT-QG programme **resolved conformal optics before the G-chain began**. The G-chain then re-derived part of that result but **mis-stated its status**, treating the tensor field `ψ` as a new primitive and declaring `γ = +1` out of derivational reach. This audit restores the canonical result, retracts the three erroneous claims, and keeps the two G-chain results that genuinely sharpen it.

### The canonical result — AT-QG QG212

`Docs/Research/ATQG_ConformalOpticsResolution.md` — **Status: COMPLETE — "OPTICS RESOLVED"**; tests `ATQG2120`, `ATQG2121`, `ATQG2122` **all passed**; core class `AT.Core/ResearchXH/ConformalOpticsResolution.cs`.

| Sector | PPN `γ` | Lensing | Shapiro | Frame dragging | Redshift |
|---|---|---|---|---|---|
| **`ψ = 0`** (conformal, `g = ρ^(2/d)η`) | **−1** | **0** | **0** | **0** | yes |
| **`ψ ≠ 0`** (tensor, QG207 completion) | **+1** | **GR** | **GR** | **restored** | yes |

> *"**Not a numerical artifact** — γ = −1 is exact within the ψ = 0 slice. **Not physical GR** — the ψ = 0 slice is an *isotropic assumption*. **The physical sector is ψ ≠ 0** — the tensor completion restores full GR optics."*

QG212's own method line reads: *"TRM/D96 only, deterministic, **no new primitives**."*

## 1. The two-sector structure, rebuilt executably

Working in the QG207 parametrisation `g₀₀ = −ρ^(2/d)e^(2ψ)`, `g_ii = ρ^(2/d)e^(−2ψ/(d−1))` and reading `γ = h_ii/h₀₀`:

| `ψ` | `γ` |
|---|---|
| `0` | **−1 exactly** ⟹ `(1+γ)/2 = 0`: deflection = κ = shear = Shapiro = 0 |
| `−4σ` | **+1 to first order** ⟹ `(1+γ)/2 = 1`: full GR strength |

The `ψ ≠ 0` value is `+1` **to first order** — which is the order at which PPN `γ` is defined. The exact value at `ψ = −4σ` is `e^(−6x) = 1 − 6x + …`.

## 2. `ψ` is **not** a new primitive

The trace/traceless decomposition of a rank-2 difference object at `d = 3`:

```text
A_ij symmetric, d = 3 :  6 components = 1 TRACE (ρ) + 5 TRACELESS
                          of which 2 are transverse-traceless (ψ, spin-2)
```

So the minimal primitive set is **{Difference, η}** (QG292): `ρ` is the **trace face** of the one Difference, `ψ` is the **traceless face** (the Weyl content — the difference from conformal flatness, QG285), and `η` is the tensor reference against which it is read (QG286). **`ψ` is not an independent input.**

> ⚠ **RETRACTION 1.** G_021, G_022 and G_023 all describe `ψ` as *"a MINIMAL NEW PRIMITIVE (QG24)"*. That characterisation is **superseded** by QG285/QG286/QG292 and is **withdrawn**.

## 3. The `ψ` route is **not** empty

> ⚠ **RETRACTION 2.** G_022 §6 concluded *"the `ψ` route is EMPTY, not merely expensive"*, deriving `ψ = 0` from the clock law and the acceleration law.

That derivation was **arithmetically correct and premise-false**: it assumed `√(−g₀₀) = ρ^(1/d)` is non-negotiable — but that **is** the `ψ = 0` slice. QG212's verdict is that the **physical** sector is `ψ ≠ 0`, where the completion gives `√(−g₀₀) = ρ^(1/d)e^(ψ)`. **G_022 §6 is withdrawn.**

## 4. What survives — and is sharpened

QG212 states the sector *structure*; the G-chain supplies its *quantitative* form. Two results are kept:

**(a) The `ψ = 0` slice is excluded by measurement at a computable significance.** QG26 gives the bare `γ = −1`; G_021 converts it into a number:

| measurement | separation from `γ = −1` |
|---|---:|
| Cassini (Bertotti/Iess/Tortora 2003) | **8.6957e4 σ** |
| VLBA (Fomalont 2009) | 6.6660e3 σ |
| Gaia (2022) | 1.2481e2 σ |

**(b) The invariant form of the sector split.** To first order `γ = −B/A` from the two exponents `A` (time) and `B` (space), so

```text
ψ = 0   ⟺   A = B = σ        ⟺   γ = −1     (the restricted isotropic slice)
γ = +1  ⟺   A + B = 0                       (the time and space perturbations cancel: h₀₀ = h_ii)
```

**Two ways of realising `A + B = 0`:**

| route | `A` | `B` | `k := B − A` |
|---|---|---|---|
| keep the clock law (`A = σ`), `B = −σ = +x` | `σ` | `−σ` | **`2x`** — G_023's case |
| the QG207 completion with `ψ = −4σ` | `−3σ` | `+3σ` | `−6x` |

So **G_023's identity `γ = −1 + k/x` is the *clock-law-preserving* form**, and its `k = 2x` is that special case — correct, but not the only route. The general invariant statement is `A + B = 0`, and **`ψ = 0 ⟺ A = B` is exactly the quantitative content of QG212's word "isotropic"**.

## 5. The one genuine open item this audit adds

In the QG207 parametrisation `γ = +1` requires `ψ = −4σ`, so the completion is **not redshift-neutral**: `√(−g₀₀) = ρ^(1/d)e^(ψ)`, a relative shift:

| location | required `ψ` | relative clock shift `e^(4x) − 1` |
|---|---:|---:|
| Earth surface | 2.784532e−9 | **2.784532e−9** |
| Sun surface | 8.490012e−6 | **8.490012e−6** |

Both are **far below the verified tests** — GPS is `+38.5` vs `+38.6 µs/day` (0.2 %, QG187) and Cassini's `γ` uncertainty is 2.3e−5. **There is no solar-system conflict**: the `ψ = 0` clock law is simply the leading form. The implied bound `|ψ| ≲ 2e−3` from GPS is **7.2e5× looser** than the required 2.78e−9 — which is precisely why the strong-field behaviour must be *computed*, not assumed.

### The strong-field flag

PPN fixes only the **first-order** `ψ = −4σ`. The leading-order redshift at that value is `z = e^(−3x) − 1`, which turns **negative**:

| `x` | `z` at `ψ = −4σ` (leading order) | `ρ`-only law `z = e^x − 1` |
|---:|---:|---:|
| 2.122503e−6 | −6.4e−6 | +2.1e−6 |
| 0.10 | **−0.259182** | +0.105171 |
| 0.172317 | **−0.403664** | +0.188054 |
| **0.247002** (J0740+6620) | **−0.523366** | +0.280182 |

A **blueshift at a bound object**. This is **not** a refutation of QG212: the `O(x²)` form of the completion decides the strong-field behaviour, and it is **uncomputed**. It is a **BOUNDARY flag**.

## 6. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the two-sector structure **as QG212 states it** (`ψ = 0` ⟹ `γ = −1` exactly; `ψ ≠ 0` ⟹ `γ = +1` to first order) · the `ψ = 0` slice's measured exclusion (Cassini 8.6957e4 σ) · the invariant split (`ψ = 0 ⟺ A = B`; `γ = +1 ⟺ A + B = 0`, with G_023's `k = 2x` and the QG207 `k = −6x` as the two realisations) · and that `ψ` introduces **no new primitive** (trace/traceless of the one Difference against `η`). |
| **BOUNDARY** | the **exact (nonlinear) completion** is unspecified — the leading-order redshift at `ψ = −4σ` is negative at compactness, so the `O(x²)` form is **load-bearing and uncomputed**; and `ψ`'s ultimate **ontological** status is itself a documented boundary item (QG299). |
| **REFUTED** | **G_022 §6** ("the `ψ` route is empty") and **G_023's verdict reason** ("`γ = +1` requires a new primitive") — **both withdrawn**. *(The `ψ = 0` slice remains excluded at 8.6957e4 σ; that is a different statement and it stands.)* |

## 7. Classification and caveats

**No reclassification of the QG212 result — this audit RESTORES it.** D_040 untouched; no canonical claim, value or equation changes; no new primitive. Deterministic: exact algebra on AT's own constructions.

* `γ = +1` in the `ψ ≠ 0` sector is a **first-order** statement, as PPN requires; the exact value at `ψ = −4σ` is `e^(−6x)`.
* The three retractions are corrections of **G-chain** claims, not of AT-QG results. G_021's *quantitative* content (the 8.6957e4 σ exclusion) and G_023's *identity* survive; only G_022 §6 and G_023's verdict reason fall.

## Open problems (OP1–OP5)

1. **What is the exact (nonlinear) `ψ` completion?** The `O(x²)` form is load-bearing for the strong-field redshift and is uncomputed.
2. **Does `z` stay positive at compactness** in that exact completion, so the NICER redshift programme (G_019/G_020) remains valid?
3. **Is the QG207 parametrisation the right gauge?** A completion that keeps `A = σ` (G_023's `k = 2x` route) is redshift-neutral but costs the counting-measure volume identity (`1/ρ`); the QG207 route costs a first-order clock shift. Which does AT require?
4. **What bound does the GPS/redshift precision place on `ψ`** across the full compactness range (not just the solar system)?
5. Is the `η`-reference import (QG292) the last irreducible import in the tensor sector?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_024_Tests.cs` — **7/7 PASSED** (~0.03 s)
**Group total:** G_001–G_024 = **203/203 PASSED** (~2 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_024"`

**Headline:** optics were resolved by **QG212** (two sectors; `ψ = 0` → `γ = −1`, `ψ ≠ 0` → `γ = +1`, "no new primitives"); the G-chain mis-stated `ψ` as a new primitive and wrongly declared the route empty — **both retracted**; the G-chain's `ψ = 0` exclusion (8.6957e4 σ) and its invariant split (`ψ = 0 ⟺ A = B`; `γ = +1 ⟺ A + B = 0`) **stand and sharpen** QG212; and one genuine open item remains — the exact completion, whose `O(x²)` form decides whether the neutron-star redshift stays positive.
