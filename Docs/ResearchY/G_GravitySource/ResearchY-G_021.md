# ResearchY-G_021 — Light-Propagation Audit

> ⚠ **CORRECTED BY `ResearchY-G_024` — Optics Reconciliation Audit.** This document's statements that `ψ` is
> the *“MINIMAL NEW PRIMITIVE”* and that `γ = +1` is out of derivational reach are **SUPERSEDED and WITHDRAWN**.
> The AT-QG optics resolution **QG212** (`Docs/Research/ATQG_ConformalOpticsResolution.md` — Status **COMPLETE**,
> **OPTICS RESOLVED**; tests `ATQG2120/2121/2122`) classes the `ψ = 0` conformal slice as a **RESTRICTED SECTOR**:
> the **physical sector is `ψ ≠ 0`**, which gives **`γ = +1`** with lensing, Shapiro and frame dragging at full GR
> — with **NO new primitives**, since `ψ` is the **traceless face of the one Difference** read against `η`
> (minimal primitive set `{Difference, η}`; QG285/QG286/QG292). What **STANDS** is the quantitative part: the
> `ψ = 0` slice is Cassini-excluded at **8.6957e4 σ**, and the invariant split is `ψ = 0 ⇔ A = B ⇔ γ = −1`,
> `γ = +1 ⇔ A + B = 0`. See `ResearchY-G_024.md`.


**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_021 (permanent)
**Title:** Light-Propagation Audit — does AT supply the spatial metric, and what optics does it deliver?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_021.md`
**Depends on:** G_019 (§5, the sentence this audit corrects), G_004 (`g₀₀` = 0.99600), G_009 (the clock law),
G_001 (the source law); AT-QG QG24, QG26, QG43, QG44, QG180, QG186, QG197, QG207, **QG212**;
`Docs/Audits/MetricOriginClosure.md`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_021_Tests.cs` (7/7 PASSED, ~0.03 s)

## Purpose — and a correction

G_019 ended with a claim that turns out to be **wrong in both directions**: it understated what AT derives and
understated the severity of the result.

> G_019 §5: *"AT supplies no spatial metric, so no light bending, Shapiro delay or shadow size follows from it.
> The theory's single most distinctive consequence is also its least derivable one."*

**Both sentences are false.** AT supplies the full metric; the spatial sector *is* derived; and the derived
optics are not silent but **falsified at 8.7e4 σ**.

This audit exists to (a) state the correction executably, (b) derive `γ` from AT's own ansatz, (c) compute the
observational exclusion, (d) derive exactly what the `ψ` sector must satisfy to escape, and (e) show why the
whole G-chain is nonetheless unaffected.

## 1. AT does have a spatial metric — and the chain is closed

The metric is **conformally flat**:

```text
g_uv = ρ^(2/d) η_uv          so     g₀₀ = −ρ^(2/d)      g_rr = +ρ^(2/d)
```

The project already records this as a **closed** derivation (`Docs/Audits/MetricOriginClosure.md`:
*"Full g_uv | determined | class × factor — closed"*):

| step | status |
|------|--------|
| causal order → **conformal class** | **IMPORTED** — Malament (1977) / Hawking–King–McCarthy (1976), a *proven theorem* |
| counting measure → **conformal factor** `ρ^(2/d)` | **NATIVE** — the counting measure, uniquely selected by counting-measure preservation |
| a native reconstruction of the same class (no Malament import) | also available (G4-M Phase 0) |

So the spatial sector is `g_ij = ρ^(2/d) δ_ij`. **There is no missing derivation.** This audit's first test
asserts the closure and the `g_rr = −g₀₀` identity directly.

## 2. The optics: `γ = −1` exactly

Write `σ = (1/d) ln ρ`, so `ρ^(2/d) = e^(2σ)`. AT's own source law `a = −(1/d)∇ln ρ = −∇σ` fixes the potential
identification `Φ = σ`. Then

```text
g₀₀ = −e^(2σ) ≈ −(1 + 2Φ)          g_rr = +e^(2σ) ≈ +(1 + 2σ) = 1 − 2γΦ
```

Matching the PPN form `g_rr = 1 − 2γΦ` gives, since `−2γσ = +2σ`,

```text
γ = −1        (1 + γ)/2 = 0
```

and this is **exact**, not a first-order coincidence: taking `Φ = (1 − g₀₀)/2 = (e^(2σ) − 1)/2` in full,

```text
γ = −(g_rr − 1)/(2Φ) = −(e^(2σ) − 1)/(e^(2σ) − 1) = −1   for every σ ≠ 0
```

Every lensing observable is proportional to `(1 + γ)/2` (QG26), so **deflection, convergence `κ`, shear and
magnification change all vanish identically**, and so does the **Shapiro delay**. The **redshift survives**,
because it is governed by `g₀₀` alone.

> **AT predicts redshift without lensing.** This is a *derived* prediction — a falsification, not a gap.

## 3. The exclusion

| measurement | published `γ` | σ separation from `γ = −1` |
|---|---:|---:|
| **Cassini** (Bertotti, Iess & Tortora 2003) — Shapiro delay | `1.0000210 ± 2.3e−5` | **8.6957e4 σ** |
| **VLBA** (Fomalont 2009) — solar deflection | `0.9998000 ± 3.0e−4` | **6.6660e3 σ** |
| **Gaia** (2022) — solar deflection | `0.9970000 ± 1.6e−2` | **1.2481e2 σ** |

Plus the qualitative fact that gravitational lensing — arcs, Einstein rings, microlensing, cluster shear — is
observed in thousands of systems. **The `ψ = 0` conformal sector is REFUTED as a description of light
propagation.**

## 4. The escape, and its price

The project's resolution (QG207/QG212) completes the metric with the `ψ` field:

```text
g₀₀ = −ρ^(2/d) e^(2ψ)              g_ii = ρ^(2/d) e^(−2ψ/(d−1))
```

the isotropic member of the **Fierz–Pauli** family (QG44), whose linearized limit is GR: `γ = +1`, frame
dragging restored (QG186). **This audit derives the requirement explicitly.** A timelike geodesic's
acceleration is fixed by `g₀₀` alone, so the potential becomes `Φ = σ + ψ`, and

```text
γ(ψ) = −[ σ − ψ/(d−1) ] / ( σ + ψ )            γ = −1 at ψ = 0
γ = +1   <=>   ψ = −2σ(d−1)/(d−2)
```

| `d` | required `ψ` |
|---|---|
| **3** | **`ψ = −4σ`** |
| 4 | `ψ = −3σ` |
| 2 | **no finite solution** — consistent with `G_uv ≡ 0` in `d = 2` (QG180) |

Along the required direction the exact `γ` is `e^(6σ)` for `d = 3`, i.e. `+1` to first order with a residual
that is *second* order — precisely the order at which PPN `γ` is defined.

**And the requirement is not free.** With `ψ = −4σ` the potential is `Φ = σ + ψ = −3σ`, not `σ`. But AT's
native source law forces `Φ = σ` (that identification is what produces G_004's 0.99600 agreement with the
measured Earth-surface rate). So

> **restoring `γ = +1` necessarily changes the SOURCING relation** — the `ψ` sector is not a free optics
> patch. The project itself calls `ψ` a **MINIMAL NEW PRIMITIVE** (QG24), and QG43 notes that lensing,
> Shapiro delay and `γ` need only a **1-d.o.f. scalar** `ψ` (only GW polarization needs spin-2).

## 5. Why the whole G-chain is blind to this — and unaffected

Every audit in group G used **`g₀₀` only**:

| audit | `g₀₀`-only content |
|---|---|
| G_001, G_004 | the source law and the 0.99600 calibration |
| G_009, G_015, G_016b, G_017 | the clock law `dτ/dt = ρ^(1/d)` and the fixed-energy `Δτ` results |
| G_010, G_019 | the galactic cross-check (0.99668) and the `x²` signature |
| G_020 | the neutron-star redshift `z = e^x − 1` |

The conformal spatial part never entered any of them. **Those results stand**; the chain is *silent* on optics
rather than wrong about them.

One consequence to record: with `ψ ≠ 0` the clock law becomes

```text
dτ/dt = √(−g₀₀) = ρ^(1/d) e^(ψ)
```

and `e^ψ = 1 + ψ + …` is **first order** in `ψ` — *lower* than G_019's `x²` signature (at `ψ = 1e−3` the
contamination is 1000.5× the `x²` term). So **any nonzero `ψ` swamps the second-order signature the G-chain
proposes to test.** The G-chain's clock results are the `ψ = 0` limit.

## 6. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | AT's metric has a spatial part `g_ij = ρ^(2/d) δ_ij` (class × factor closed); **`γ = −1` exactly**, so `(1+γ)/2 = 0` and deflection, `κ`, shear, `μ − 1` and the Shapiro delay all vanish while the redshift survives; and `γ = +1 ⇔ ψ = −4σ` in `d = 3` (no `d = 2` solution), with exact `γ = e^(6σ)` along the required direction. |
| **BOUNDARY** | the **conformal class** (imported Malament theorem) and the **`ψ` sector** (a minimal new primitive, QG24); and the conflict that `ψ = −4σ` also moves `Φ` from `σ` to `−3σ`, breaking the native source identification. |
| **REFUTED** | the `ψ = 0` conformal optics: **8.6957e4 σ** (Cassini), 6.6660e3 σ (VLBA), 1.2481e2 σ (Gaia), plus the observed existence of lensing. |

## 7. Classification and caveats

**No reclassification.** G_019's own numbers are `g₀₀`-only and stand; D_040 untouched; no canonical claim,
value or equation changes; no new primitive is introduced by this audit (the `ψ` sector is pre-existing and
already flagged as a primitive by the project). Deterministic: exact algebra on imported published values.

* The `γ` values are central values; the σ separations use the published uncertainties as given.
* `γ = −1` follows from the **conformal** structure plus the `Φ = σ` identification. If the identification were
  different the read-off would change — but that identification is what makes G_004 work.
* `ψ = −4σ` is derived to first order and verified exactly along the required direction; the residual is second
  order, i.e. below the order at which PPN `γ` is defined.

## Open problems (OP1–OP5)

1. **Is `ψ` physical?** If `γ = +1` requires it, `ψ` must be nonzero — and this audit derives *exactly* how
   nonzero (`ψ = −4σ = −(4/3)ln ρ` in `d = 3`).
2. **Can the `ψ` sector be sourced without breaking `a = −(1/d)∇ln ρ`?** The `Φ = −3σ` result says the
   completion must change the sourcing relation, so a new source law must be derived, not assumed.
3. **What bound does the clock/precision work set on `ψ`?** G_019/G_020 assumed `ψ = 0`; if `e^ψ` enters at
   first order, the existing `x²` tests become bounds on `ψ`. This cross-check has never been done.
4. **Is the conformal-class import irreducible?** A fully native derivation of the class would move the last
   imported cog out of the metric chain.
5. **Can the horizon corollary survive?** With `g₀₀ = −ρ^(2/d)e^(2ψ)`, does `g₀₀` still never vanish, or does
   `ψ` reintroduce a clock-stopping surface?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_021_Tests.cs` — **7/7 PASSED** (~0.03 s)
**Group total:** G_001–G_021 = **182/182 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_021"`

**Correction carried:** G_019's "no spatial metric / least derivable" sentences are **withdrawn**. The spatial
metric is derived, and its consequence is a falsification rather than a gap.
