# Y_G_023_Result.md — ResearchY-G_023 Spatial Sector Closure Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_023_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.06 s) — group G total 196/196 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_023"`

## Summary

**Question:** can **ANY** spatial metric be derived **without new primitives** that yields `γ ≈ +1` while preserving the **clock law**, the **source law** and the **acceleration law**? **Success criterion:** `γ ≈ +1` **without changing `g₀₀` physics**.

**Answer: NO.** The obstruction is a **single conformal invariant**, `k = B − A`, and AT's own construction pins it to **zero** from two independent directions.

## The framework

```
isotropic form (where PPN gamma is defined):
  ds^2 = -e^(2A) dt^2 + e^(2B) (dR^2 + R^2 dOmega^2)
  Phi  = (e^(2A) - 1)/2 ,  gamma = -(e^(2B) - 1)/(e^(2A) - 1)  ~  -1 + k/x ,  k := B - A

k is the CONFORMAL INVARIANT:  g -> Omega^2 g shifts A and B by the same omega, so k is unchanged.
conformal flatness  <=>  A = B  <=>  g_rr = -g00  <=>  k = 0  =>  gamma = -1 for EVERY A

AT pins k = 0 twice:
  clock law        sqrt(-g00) = rho^(1/d) = e^sigma        =>  A = sigma
  counting measure sqrt(det g_ij) = rho  =>  e^(3B) = rho  =>  B = sigma
  =>  k = B - A = 0 identically,  gamma = -1 at EVERY compactness

gamma = +1 with A = sigma kept:  e^(2B) = 2 - e^(2A)  =>  B = (1/2) ln(2 - e^(-2x)) ,  k = 2x (linear)
```

## Detail — `k` is class data

`k` is invariant under any conformal rescaling (verified for `ω ∈ {0, 0.37, −2.5, 1e−3}`), and no rescaling can flatten a `k ≠ 0` metric. So `k` is **exactly** the datum the causal-order → conformal-class step supplies.

## Detail — the two native conditions

| `x` | `A = σ` | `B = σ` | `k` | volume/ρ | `γ` |
|---:|---:|---:|---:|---:|---:|
| 6.96133e−10 | −6.96133e−10 | −6.96133e−10 | 0 | 1.000000000 | **−1** |
| 2.122503e−6 | −2.122503e−6 | −2.122503e−6 | 0 | 1.000000000 | **−1** |
| 1e−4 | −1e−4 | −1e−4 | 0 | 1.000000000 | **−1** |
| 0.247002 | −0.247002 | −0.247002 | 0 | 1.000000000 | **−1** |

Both conditions express the **same primitive** (`ρ`) and pin their own exponent to the **same scalar** `σ`. Violating either breaks a named law (`A ≠ σ` → clock law/redshift; `B ≠ σ` → counting measure by `e^(3δ)`).

## Detail — the price of γ = +1

| `x` | `A = σ` | `B` for `γ = +1` | `k` | `k_lin = 2x` | volume/ρ = `e^(3k)` |
|---:|---:|---:|---:|---:|---:|
| 6.96133e−10 | −6.96133e−10 | 6.96133e−10 | 1.392266e−9 | 1.392266e−9 | 1.000000 |
| 2.122503e−6 | −2.122503e−6 | 2.122494e−6 | 4.244997e−6 | 4.245006e−6 | 1.000013 |
| 1e−4 | −1e−4 | 9.99800e−5 | 1.999800e−4 | 2.000000e−4 | 1.000600 |
| **0.247002** | −0.247002 | **0.1645877** | **0.4115897** | 0.494004 | **3.437585** |

`A` is kept **exactly** (so `g₀₀` physics is unchanged, and `γ = +1` holds to 1e−9), but the spatial volume becomes `3.437585 × ρ` at J0740+6620. **Changing the volume measure IS changing a primitive.**

## Detail — the primitive inventory (the closure)

| ingredient | type | moves `k`? |
|---|---|---|
| Q-event counts, `ρ`, `σ`, DiffuseStep `Λ = I − W`, D96 lattice, spectral `λ`, information | **scalar** | **no** — each fixes one function; any `δ = B − σ` breaks the volume by `e^(3δ)` and moves `γ` to `−1 + δ/x` |
| **causal ORDER** | **non-scalar** — supplies the conformal CLASS | **yes in principle — but AT's order is FLAT, so `k = 0`** |

Isotropy evidence for the D96 ring: **96 cells collapse onto only 45 eigenvalues** (histogram `{1:1, 2:42, 5:1, 6:1}`, free room `Σ(m−1) = 51`), while the **degeneracy-free random control has 96 distinct** values (all multiplicity 1). The complement structures that might suggest a natural `1/ρ` are all **constants** — free room 51/95 = 0.536842, the 95 → 44 → 1 chain = 2.159091, the possible/accessible factor 3.746e5, the spectral total 1152 — and none matches `1/ρ` at any compactness.

## Detail — the success criterion

| half | status |
|---|---|
| **"without changing `g₀₀` physics"** | **SATISFIABLE** — `A = σ` kept exactly; all three laws and `z_AT = 0.2801817` untouched |
| **"`γ ≈ +1`"** | **NOT SATISFIABLE** from AT's primitives — needs `k = 2x`, the factor 3.437585 |

| `γ` | Cassini | VLBA | Gaia | verdict |
|---|---:|---:|---:|---|
| **−1** (derived) | 8.6957e4 σ | 6.6660e3 σ | 124.8125 σ | **REFUTED** |
| **0** (flat space) | 4.3479e4 σ | 3.3327e3 σ | 62.3125 σ | **REFUTED** |
| **+1** (needed) | 0.9130 σ | 0.6667 σ | 0.1875 σ | allowed |

## Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the closure identity `γ = −1 + k/x` with `k = B − A` · `k` is the conformal invariant, hence exactly the class data of the causal-order step · conformal flatness ⟺ `k = 0` · both native conditions pin `A` and `B` to the same `σ`, so `k = 0` identically · the exact `γ = +1` solution `B = ½ln(2 − e^(−2x))` with `k = 2x` to first order |
| **BOUNDARY** | the single scalar of class data that would do it (`k = 2x`, i.e. the spatial volume becoming `1/ρ`, a factor **3.437585** at J0740+6620) — supplying it is a **new primitive**, and `ψ` is **empty** (G_022); the class itself is **imported** (Malament) |
| **REFUTED** | that any spatial metric derivable from AT's existing primitives gives `γ ≈ +1` while preserving the three laws · **and** the conformal sector (`γ = −1`) and flat space (`γ = 0`), each excluded by >60 σ everywhere |

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive introduced by this audit. G_021 and G_022 stand. Deterministic: exact algebra on AT's own constructions.

* `γ = −1 + k/x` is **first order**, with residual `≈ 2x`; `γ = +1` is asserted via the **exact** solution `e^(2B) = 2 − e^(2A)`.
* The D96 histogram is asserted exactly (`1:1, 2:42, 5:1, 6:1`; free room 51); the random control is degeneracy-free.
* `Expm1`/`Log1p` use 5-term series below `|t| = 1e−3` — the direct `e^t − 1` at `t ≈ 1.4e−9` loses **8 digits** to cancellation.

## Open problems (OP1–OP5)

1. Is there **any AT-native principle selecting `k ≠ 0`**? The inventory says no.
2. Can the **counting-measure volume identity** be relaxed without abandoning the counting measure?
3. **Is AT's causal order provably flat?** The isotropy evidence here is spectral; a proof would harden the closure.
4. Would `k = 2x` be observable elsewhere — does it alter the **four-scale calibration** once `e^(−B)` propagates (`4.2e−6` at the Sun, `0.49` at a neutron star)?
5. Is the **conformal-class import (Malament)** the last irreducible import in the metric chain?
