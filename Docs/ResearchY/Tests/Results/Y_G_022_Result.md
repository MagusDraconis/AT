# Y_G_022_Result.md — ResearchY-G_022 Spatial Metric Audit

> ⚠ **CORRECTED BY `ResearchY-G_024` — Optics Reconciliation Audit.** This document's statements that `ψ` is
> the *“MINIMAL NEW PRIMITIVE”* and that `γ = +1` is out of derivational reach are **SUPERSEDED and WITHDRAWN**.
> The AT-QG optics resolution **QG212** (`Docs/Research/ATQG_ConformalOpticsResolution.md` — Status **COMPLETE**,
> **OPTICS RESOLVED**; tests `ATQG2120/2121/2122`) classes the `ψ = 0` conformal slice as a **RESTRICTED SECTOR**:
> the **physical sector is `ψ ≠ 0`**, which gives **`γ = +1`** with lensing, Shapiro and frame dragging at full GR
> — with **NO new primitives**, since `ψ` is the **traceless face of the one Difference** read against `η`
> (minimal primitive set `{Difference, η}`; QG285/QG286/QG292). What **STANDS** is the quantitative part: the
> `ψ = 0` slice is Cassini-excluded at **8.6957e4 σ**, and the invariant split is `ψ = 0 ⇔ A = B ⇔ γ = −1`,
> `γ = +1 ⇔ A + B = 0`. See `ResearchY-G_024.md`.


**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_022_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.04 s) — group G total 189/189 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_022"`

## Summary

**Question:** can a spatial metric be **derived** that preserves the **clock law**, the **source law** and the **acceleration law**, while reproducing **`γ ≈ +1`**?

**Answer: NO** — structurally, not numerically. The three laws are **`γ`-blind**: none of them mentions the spatial sector. `γ` is fixed by a *fourth* condition the question does not name — AT's conformal closure — and that gives **`γ = −1` for any conformal factor**. `γ = +1` therefore requires the two sectors to carry **reciprocal** factors, i.e. a **non-conformally-flat** metric, which is a **postulate**.

## The framework

```
x = GM/(Rc^2) > 0 ;  sigma = (1/d) ln rho = -x ;  rho = e^(-3x) ;  rho^(2/d) = e^(-2x)

clock   dtau/dt = sqrt(-g00) = e^sigma          <- g00 only     (gamma-BLIND)
source  a = -(1/d) grad ln rho = -grad sigma     <- rho only      (gamma-BLIND)
accel   d2r/dt2 ~ -A'  (slow test particle)      <- g00 only      (gamma-BLIND)

conformal flatness  g = Omega^2 eta  =>  g00 = -Omega^2 , g_rr = +Omega^2  =>  g_rr = -g00
gamma = -(g_rr - 1)/(2 Phi) = -(Omega^2 - 1)/(Omega^2 - 1) = -1     FOR ANY Omega^2 != 1

g_rr = e^(2 beta sigma)   =>   gamma = -beta
gamma = +1  <=>  g_rr = rho^(-2/d) = e^(+2x)   with   g00 = -rho^(2/d) = e^(-2x)  kept
```

## Detail — the theorem

`γ = −1` holds for **every** conformal factor tested, including the reciprocal and two constants:

| `Ω²` | value | `γ` |
|---|---:|---:|
| `ρ^(2/d)` (AT counting measure) | 0.999998000002 | **−1** |
| `ρ^(−2/d)` (reciprocal factor) | 1.000002000002 | **−1** |
| `e^(−0.6x)` | 0.999999400000 | **−1** |
| `1.5` | 1.5 | **−1** |
| `0.9` | 0.9 | **−1** |

So `γ = −1` is a **theorem about 4D conformal flatness plus `Φ = σ`** — **not** an artefact of `ρ^(2/d)`.

## Detail — the exclusion table

| `γ` | Cassini | VLBA | Gaia | verdict |
|---|---:|---:|---:|---|
| **−1** (conformal) | 8.6957e4 σ | 6.6660e3 σ | 124.8125 σ | **REFUTED** |
| **0** (flat space) | 4.3479e4 σ | 3.3327e3 σ | 62.3125 σ | **REFUTED** |
| **+1** (reciprocal) | 0.9130 σ | 0.6667 σ | 0.1875 σ | **ALLOWED** |

## Detail — the γ = +1 sector passes the question's test, but is not derivable

| requirement | status |
|---|---|
| clock law | **exact** — `√(−g₀₀) = e^(−x) = ρ^(1/d)` |
| source law | **exact** — involves `ρ` only |
| acceleration law | **exact** — slow test particle sees `−A′` only |
| redshift | **identical to G_020** — `z_AT = 0.2801817` for J0740+6620 |
| `γ ≈ +1` | **yes** |

**But** `g_rr + g₀₀ = 2 sinh(2x) = 1.028687` ≠ 0 at `x = 0.247002` — **not conformally flat**, so the causal-order → conformal-class step does not produce it.

**The cost:** native `Ω = ρ^(1/d)` gives `Ω³ = ρ` (**exactly the counting measure**); the `γ = +1` sector needs `Ω = ρ^(−1/d)`, giving `Ω³ = 1/ρ` (**the reciprocal**). The choice is "the counting measure or its reciprocal".

## Detail — the ψ route is empty (refines G_021)

With `A = σ + ψ`: the acceleration law `−A′ = −σ′` forces `ψ′ = 0` (`ψ` constant), and the clock law `e^A = e^σ` then forces **`ψ = 0`**. So the `ψ` completion gives `γ = −1`, not `+1`.

| location | `x` | `ψ = −4σ` | clock rate | `z_AT` |
|---|---:|---:|---:|---:|
| Earth surface | 6.961e−10 | +2.785e−9 | 1.000000002 | −2.0e−9 |
| Sun surface | 2.123e−6 | +8.490e−6 | 1.000006368 | −6.4e−6 |
| **J0740+6620** | 0.247002 | **+0.988008** | **2.098045** | **−0.523366** |

A **negative (blue) surface shift at a bound object** — refuted by any positive measured neutron-star redshift. The clock-law violation grows from **2.785e−9** at Earth to **1.685879** (169 %) at a neutron star.

## Detail — nothing earlier is damaged

All `G_001–G_020` agreements are in the `γ`-blind sector and are therefore **invariant under any spatial sector**: G_004 `0.99600`; G_009/G_015/G_016b/G_017 clock rates; G_019's `x²` signature; G_020's `z_AT = 0.2801817`; the source law and four-scale calibration. `γ` is a **separate observable class** (light bending, Shapiro delay) that no G-chain audit exercised before G_021.

## Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the `γ`-blindness of all three laws · the theorem "conformal flatness + `Φ = σ` ⟹ `γ = −1`" for **any** factor · the unique native spatial metric `Ω = ρ^(1/d)` with volume measure exactly `ρ` · the explicit `γ = +1` sector `g_rr = ρ^(−2/d)` with `g₀₀ = −ρ^(2/d)` intact |
| **BOUNDARY** | the `γ = +1` sector is a **postulate** (reciprocal factors ⟹ abandoning 4D conformal flatness); its cost is a spatial volume measure of `1/ρ`; and the `ψ` route is **empty**, not merely expensive |
| **REFUTED** | that the three laws imply or permit a **derived** `γ ≈ +1` (they are silent on `γ`) · the `ψ` completion, which predicts a surface **blueshift** at compact objects |

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive introduced by this audit. Deterministic: exact algebra on AT's own constructions.

* `γ` is a **weak-field** parameter, so the `γ = +1` assertion is made only for `x ≤ 1e−5`; at neutron-star compactness the exact `e^(2B)` behaviour matters.
* The `β`-family `g_rr = e^(2βσ)` is a one-parameter probe of the spatial sector, not a claim that AT contains such a family.
* `Expm1` is evaluated by series below `|t| = 1e−3`; a threshold of `1e−6` is too small (`2e−6` already loses `1e−11` to cancellation).

## Open problems (OP1–OP5)

1. Is there any **AT-native principle** that selects a **reciprocal** spatial factor? If not, `γ = +1` is permanently a postulate.
2. Can a **non-conformal** spatial sector be sourced by `ρ` alone — i.e. a spatial field equation AT does not have?
3. Does the reciprocal sector preserve the **four-scale calibration** once the `e^(−B)` factor propagates (`4.2e−6` at the Sun, `0.49` at a neutron star)?
4. What does `Ω³ = 1/ρ` mean for the **counting measure**? A volume measure below one cell destroys the integer-count meaning of `ρ`.
5. Is the **conformal-class import** (Malament) binding, or could a native derivation admit two factors?
