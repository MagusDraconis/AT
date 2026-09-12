# ResearchY-G_023 — Spatial Sector Closure Audit

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
**ID:** ResearchY-G_023 (permanent)
**Title:** Spatial Sector Closure Audit — can *any* spatial metric be derived without new primitives giving γ ≈ +1?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_023.md`
**Depends on:** G_022 (the reciprocal-factor cost and the empty ψ route), G_021 (the γ = −1 theorem), G_004 (the 0.99600 calibration), G_009 (the clock law), G_001 (the source law), G_016 (the D96 room structure); AT-QG QG24, QG26, QG43, QG180, QG207, QG212; `Docs/Audits/MetricOriginClosure.md`; ResearchY-D_040 (the mirror pairing)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_023_Tests.cs` (7/7 PASSED, ~0.06 s)

## Purpose

G_022 showed that `γ = +1` requires the two sectors to carry **reciprocal** factors — a non-conformally-flat metric — and that the `ψ` route is empty. G_023 closes the question by asking whether **any** route exists inside AT's *existing* inventory of primitives:

> **Can ANY spatial metric be derived WITHOUT NEW PRIMITIVES that yields `γ ≈ +1` while preserving the clock law, the source law and the acceleration law?**
> **Success criterion:** `γ ≈ +1` **without changing `g₀₀` physics.**
> Output: **DERIVED / BOUNDARY / REFUTED**.

**Answer: NO.** The obstruction is a **single conformal invariant**, `k = B − A`, and AT's own construction pins it to **zero** from two independent directions.

## 1. The framework — and why isotropic form is the right one

PPN `γ` is **defined** in isotropic form, so this is the coordinate system in which the read-off is legitimate:

```text
ds² = −e^(2A) dt² + e^(2B) (dR² + R² dΩ²)
Φ   = (e^(2A) − 1)/2                     (g₀₀ = −(1 + 2Φ))
γ   = −(e^(2B) − 1)/(e^(2A) − 1)   ≈   −1 + k/x ,     k := B − A ,   x := GM/(Rc²) = −A
```

A static, spherically symmetric metric has exactly **two** functions, `A` (time) and `B` (space), and `γ` is fixed by them. To first order it is controlled by the **one** number `k`.

## 2. `k` is exactly the conformal invariant

Under `g → Ω²g` both exponents shift by the same `ω`: `A → A + ω`, `B → B + ω`. So

```text
k = B − A       is UNCHANGED by any conformal rescaling
```

`k` is therefore **precisely the class data** that the causal-order → conformal-class step (Malament 1977) supplies. And conformal flatness means `g = Ω²η`, i.e. `e^(2A) = e^(2B)`, i.e. `A = B`, i.e. `g_rr = −g₀₀` — which **is** `k = 0`:

```text
conformal flatness   ⟺   k = 0   ⟺   γ = −1 for EVERY A
```

That is G_022's theorem, restated as the statement `k = 0`. (Verified for `ω ∈ {0, 0.37, −2.5, 1e−3}`, and that no rescaling can flatten a `k ≠ 0` metric.)

## 3. AT pins `k = 0` from two independent directions

| condition | statement | consequence |
|---|---|---|
| **clock law** | `√(−g₀₀) = ρ^(1/d) = e^σ` | `A = σ` |
| **counting measure** | `√(det g_ij) = ρ` (the spatial volume **is** the count); in isotropic form `√(det g_ij) = e^(3B)` | `e^(3B) = ρ` ⟹ `B = σ` |

Both conditions express the **same primitive** (`ρ`) and each pins its own exponent to the **same scalar** `σ`. Hence

```text
k = B − A = 0   IDENTICALLY,   hence   γ = −1   at EVERY compactness
```

| `x` | `A = σ` | `B = σ` | `k` | volume/ρ | `γ` |
|---:|---:|---:|---:|---:|---:|
| 6.96133e−10 (Earth) | −6.96133e−10 | −6.96133e−10 | 0 | 1.000000000 | **−1** |
| 2.122503e−6 (Sun) | −2.122503e−6 | −2.122503e−6 | 0 | 1.000000000 | **−1** |
| 1e−4 | −1e−4 | −1e−4 | 0 | 1.000000000 | **−1** |
| 0.247002 (J0740+6620) | −0.247002 | −0.247002 | 0 | 1.000000000 | **−1** |

**This is a theorem about AT's construction, not a coincidence.** And violating *either* condition breaks a named law: `A ≠ σ` breaks the clock law and the redshift; `B ≠ σ` breaks the counting measure by `e^(3δ)`.

## 4. What `γ = +1` would cost

Keeping the clock law (`A = σ`, so **`g₀₀` physics is unchanged**) and demanding `γ = +1`:

```text
−(e^(2B) − 1) = e^(2A) − 1      ⟹      e^(2B) = 2 − e^(2A)
                                ⟹      B = ½ ln(2 − e^(−2x)) ,    k = 2x to first order
```

| `x` | `A = σ` | `B` for `γ = +1` | `k = B − A` | `k_lin = 2x` | volume/ρ = `e^(3k)` |
|---:|---:|---:|---:|---:|---:|
| 6.96133e−10 | −6.96133e−10 | 6.96133e−10 | 1.392266e−9 | 1.392266e−9 | 1.000000 |
| 2.122503e−6 | −2.122503e−6 | 2.122494e−6 | 4.244997e−6 | 4.245006e−6 | 1.000013 |
| 1e−4 | −1e−4 | 9.99800e−5 | 1.999800e−4 | 2.000000e−4 | 1.000600 |
| **0.247002** | −0.247002 | **0.1645877** | **0.4115897** | 0.494004 | **3.437585** |

`γ` is `+1` to 1e−9 in every row, and **`A` is kept exactly** — so the `g₀₀` half of the success criterion is satisfied. But the **price** is immediate and computable: the spatial volume is no longer the counting measure, by a factor `e^(3k)` that is invisible in the solar system (1.000013 at the Sun) and **3.437585 at J0740+6620**. **Changing the volume measure IS changing a primitive.**

## 5. The primitive inventory — the closure

| ingredient | type | can it move `k`? |
|---|---|---|
| Q-event counts | scalar | no |
| counting measure `ρ`, `σ = (1/d) ln ρ` | scalar | no |
| DiffuseStep `Λ = I − W` | scalar (symmetric, isotropic) | no |
| D96 lattice | scalar spectrum | no |
| spectral / `λ` structure | scalar | no |
| information content | scalar | no |
| **causal ORDER** | **non-scalar** — supplies the conformal CLASS | **gives `k` — and AT's order is FLAT, so `k = 0`** |

Every scalar ingredient fixes **one** function, so any deformation `δ = B − σ` **both** breaks the volume identity (`e^(3δ)`) **and** moves `γ` to `−1 + δ/x`. None supplies `k = 2x`.

The one **non-scalar** ingredient is the causal **order** — and it is what supplies the conformal *class*. AT's order is the flat D96 ring order, verified **isotropic**:

- the D96 ring collapses **96 cells onto only 45 eigenvalues** (histogram `{1:1, 2:42, 5:1, 6:1}`, free room `Σ(m−1) = 51`), i.e. symmetry removes the anisotropy;
- the degeneracy-free random control has **96 distinct** values (all multiplicity 1).

So AT's class is `[η]`, `k = 0`, and **the order supplies no anisotropy**.

The complement structures that might suggest a natural `1/ρ` are all **constants** and cannot supply a field-valued reciprocal:

| candidate | value | matches `1/ρ` at |
|---|---:|---|
| free room 51 of 95 | 0.536842 | no compactness |
| the 95 → 44 → 1 chain | 2.159091 | no compactness |
| possible/accessible factor | 3.746e5 | no compactness |
| spectral total 1152 | 1152 | no compactness |

**No existing primitive can move `k`. CLOSURE.**

## 6. The success criterion

| half | status |
|---|---|
| **"without changing `g₀₀` physics"** | **SATISFIABLE** — `A = σ` is kept exactly, so the clock law, source law, acceleration law and `z_AT = 0.2801817` are untouched (they are γ-blind, G_022) |
| **"`γ ≈ +1`"** | **NOT SATISFIABLE from AT's primitives** — `γ = −1` is forced; `γ = +1` needs `k = 2x` |

| `γ` | Cassini | VLBA | Gaia | verdict |
|---|---:|---:|---:|---|
| **−1** (what AT derives) | 8.6957e4 σ | 6.6660e3 σ | 124.8125 σ | **REFUTED** |
| **0** (flat space) | 4.3479e4 σ | 3.3327e3 σ | 62.3125 σ | **REFUTED** |
| **+1** (what is needed) | 0.9130 σ | 0.6667 σ | 0.1875 σ | allowed |

So the criterion is met only by **postulate**, and the **derived** answer is excluded at **8.6957e4 σ**.

## 7. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the closure identity `γ = −1 + k/x` with `k = B − A`; `k` is the conformal invariant and hence exactly the class data of the causal-order step; conformal flatness ⟺ `k = 0`; the two native conditions each pin `A` and `B` to the same `σ`, so `k = 0` identically and `γ = −1` at every compactness; and the exact `γ = +1` solution `B = ½ln(2 − e^(−2x))` with `k = 2x` to first order. |
| **BOUNDARY** | the single scalar of class data that would do it (`k = 2x` — equivalently the spatial volume becoming `1/ρ`, a factor **3.437585** at J0740+6620); supplying it is a **new primitive**, and the nearest existing candidate (`ψ`) is **empty** (G_022). The class itself is **imported** (Malament), so even `k` is not AT-native. |
| **REFUTED** | that any spatial metric can be derived from AT's existing primitives giving `γ ≈ +1` while preserving the three laws; **and** both the conformal sector (`γ = −1`) and flat space (`γ = 0`), each excluded by >60 σ on every measurement. |

## 8. Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation change; **no new primitive** introduced by this audit. G_021 and G_022 stand. Deterministic: exact algebra on AT's own constructions.

* `γ` is a weak-field parameter: the identity `γ = −1 + k/x` is first order, with residual `≈ 2x` (verified: the exact `γ` at `k = 2x` is `1 + 2x + …`).
* `γ = +1` is asserted via the **exact** solution `e^(2B) = 2 − e^(2A)`, not by the linear rule.
* `Expm1`/`Log1p` are evaluated by 5-term series below `|t| = 1e−3`; the direct `e^t − 1` at `t ≈ 1.4e−9` loses **8 digits** to cancellation, which is why the whole test file uses the series forms.

## Open problems (OP1–OP5)

1. **Is there any AT-native principle that selects `k ≠ 0`?** Everything in the inventory says no; if none exists, `γ = +1` is permanently out of derivational reach.
2. **Can the counting-measure volume identity be relaxed** without abandoning the counting measure itself (e.g. a volume *ratio* rather than an equality)?
3. **Is AT's causal order provably flat?** The isotropy evidence here is spectral (45 distinct values over 96 cells vs 96 for the random control); a full proof of the order's flatness would harden the closure.
4. **Would `k = 2x` be observable elsewhere** — beyond lensing, does it alter the four-scale calibration once `e^(−B)` propagates (`4.2e−6` at the Sun, `0.49` at a neutron star)?
5. **Is the conformal-class import (Malament) the last irreducible import in the metric chain?** A native derivation that admitted two factors would reopen the whole question.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_023_Tests.cs` — **7/7 PASSED** (~0.06 s)
**Group total:** G_001–G_023 = **196/196 PASSED** (~2 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_023"`

**Headline:** `γ` is controlled by ONE conformal invariant `k = B − A`; AT's clock law and counting measure each pin `A` and `B` to the same `σ`, so `k = 0` and `γ = −1` identically; `γ = +1` needs `k = 2x`, which would cost a spatial volume of `3.437585 × ρ` at J0740+6620; and **no existing primitive can move `k`**.
