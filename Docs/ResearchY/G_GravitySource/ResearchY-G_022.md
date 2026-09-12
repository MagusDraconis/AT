# ResearchY-G_022 — Spatial Metric Audit

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
**ID:** ResearchY-G_022 (permanent)
**Title:** Spatial Metric Audit — can a derived spatial metric preserve the three laws *and* give γ ≈ +1?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_022.md`
**Depends on:** G_021 (the γ = −1 theorem and the ψ route), G_004 (the 0.99600 calibration), G_009 (the clock law), G_001 (the source law), G_019/G_020 (the `g₀₀`-only chain); AT-QG QG24, QG26, QG43, QG44, QG180, QG207, QG212; `Docs/Audits/MetricOriginClosure.md`
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_022_Tests.cs` (7/7 PASSED, ~0.04 s)

## Purpose

G_021 found that AT's conformal metric gives PPN `γ = −1` — excluded by Cassini at **8.6957e4 σ** — and that restoring `γ = +1` through the `ψ` field demands `ψ = −4σ`, which then contradicts AT's source law. G_022 takes the constructive question:

> **Can a spatial metric be DERIVED that preserves (1) the clock law, (2) the source law, (3) the acceleration
> law, while reproducing `γ ≈ +1`?**
> Output: **DERIVED / BOUNDARY / REFUTED**.

**Answer: NO — and the reason is structural, not numerical.** The three laws the question names are **`γ`-blind**: none of them mentions the spatial sector at all. `γ` is fixed by a *fourth* condition the question does not name — AT's conformal closure — and that condition gives `γ = −1` for **any** conformal factor. So `γ = +1` cannot be obtained by tuning the spatial metric within AT's derivation; it requires the two sectors to carry **reciprocal** factors, i.e. a **non-conformally-flat** metric, which is a **postulate**, not a derivation.

## 1. The three laws are γ-blind

With `x = GM/(Rc²) > 0` (the G_019/G_020 convention), `σ = (1/d) ln ρ = −x`, so `ρ = e^(−3x)` in `d = 3` and `ρ^(2/d) = e^(−2x)`:

| law | expression | depends on |
|---|---|---|
| **clock** | `dτ/dt = √(−g₀₀) = e^σ = e^(−x) = ρ^(1/d)` | `g₀₀` only |
| **source** | `a = −(1/d)∇ln ρ = −∇σ = +∇x` | `ρ` only |
| **acceleration** | `d²r/dt² ≈ −A′` for a slow test particle | `g₀₀` only |

None contains `B`, the spatial exponent. **The question as posed constrains nothing about `γ`.** Writing `g_rr = e^(2B)` and sliding `B` over five values leaves all three laws bitwise identical (tested).

The only place `B` enters is the **proper** acceleration of a *held* static observer, `â = A′e^(−B)`, and that is a measure convention whose deviation between the conformal (`β = +1`) and reciprocal (`β = −1`) sectors is `e^(2x) − 1 =` **1.392e−9** at the Earth's surface — below every stated precision in the programme.

## 2. The fourth condition: conformal flatness forces γ = −1, for ANY factor

AT's metric is conformally flat, `g_uv = Ω²η_uv`, so

```text
g₀₀ = −Ω²        g_rr = +Ω²        ⟹        g_rr = −g₀₀   ALWAYS
```

With `Φ = (−g₀₀ − 1)/2` (so `g₀₀ = −(1 + 2Φ)`) and `g_rr = 1 − 2γΦ`:

```text
γ = −(g_rr − 1)/(2Φ) = −(Ω² − 1)/(Ω² − 1) = −1        for every Ω² ≠ 1
```

**`γ = −1` is therefore a THEOREM about 4D conformal flatness plus `Φ = σ` — not an artefact of the counting-measure factor `ρ^(2/d)`.** Verified for five different factors, including the reciprocal `ρ^(−2/d)` and two constants:

| conformal factor `Ω²` | `Ω²` | `γ` |
|---|---:|---:|
| `ρ^(2/d)` (AT counting measure) | 0.999998 | **−1** |
| `ρ^(−2/d)` (reciprocal factor) | 1.000002 | **−1** |
| `e^(−0.6x)` (scaled) | 0.9999994 | **−1** |
| `1.5` | 1.5 | **−1** |
| `0.9` | 0.9 | **−1** |

**The factor is irrelevant.** Flipping it to `ρ^(−2/d)` still gives `γ = −1`, because conformal flatness ties the two sectors together.

## 3. What γ = +1 actually requires: two different factors

Parameterising `g_rr = e^(2βσ)` gives the clean law

```text
γ = −β
```

| `β` | sector | `γ` | Cassini separation |
|---|---:|---:|---:|
| `+1` | AT conformal (`g_rr = ρ^(2/d)`) | **−1** | **8.6957e4 σ** ✗ |
| `0` | flat space (`g_rr = 1`) | **0** | **4.3479e4 σ** ✗ |
| `−1` | reciprocal (`g_rr = ρ^(−2/d)`) | **+1** | **0.913 σ** ✓ |

(Full table with VLBA and Gaia in §4.)

So `γ = +1` requires

```text
g₀₀ = −ρ^(2/d) = −e^(−2x)        (AT's own time sector, untouched)
g_rr = +ρ^(−2/d) = +e^(+2x)      (the RECIPROCAL factor)
```

This metric **passes every requirement the question sets**:

| requirement | status |
|---|---|
| clock law | **exact** — `√(−g₀₀) = e^(−x) = ρ^(1/d)` |
| source law | **exact** — `a = −(1/d)∇ln ρ` involves `ρ` only |
| acceleration law | **exact** — slow test particle sees `−A′` only |
| redshift | **identical to G_020** — `z_AT = e^x − 1 = 0.2801817` for J0740+6620 |
| `γ ≈ +1` | **yes** — Cassini 0.913 σ, VLBA 0.667 σ, Gaia 0.188 σ |

**But it is not conformally flat.** Conformal flatness requires `g_rr + g₀₀ = 0`; here

```text
g_rr + g₀₀ = e^(2x) − e^(−2x) = 2 sinh(2x) = 1.028687   (at x = 0.247002)
```

so the **causal-order → conformal-class step (Malament 1977) does not produce it**. Two different factors is a **postulate**.

### The price is visible

The native spatial metric is `Ω = ρ^(1/d)`, whose spatial volume measure is

```text
Ω³ = ρ^(2/d)·(3/2) … = e^(−3x) = ρ      ⟵ EXACTLY the counting measure
```

The `γ = +1` sector needs `Ω = ρ^(−1/d)`, so its volume measure is

```text
Ω³ = e^(+3x) = 1/ρ      ⟵ the RECIPROCAL of the counting measure
```

**So the choice is not "which factor?", it is "the counting measure or its reciprocal".** The native answer is forced.

## 4. The exclusion table

| `γ` | Cassini (2.3e−5) | VLBA (3.0e−4) | Gaia (1.6e−2) | verdict |
|---|---:|---:|---:|---|
| **−1** (AT conformal) | 8.6957e4 σ | 6.6660e3 σ | 124.8125 σ | **REFUTED** |
| **0** (flat space) | 4.3479e4 σ | 3.3327e3 σ | 62.3125 σ | **REFUTED** |
| **+1** (reciprocal) | 0.9130 σ | 0.6667 σ | 0.1875 σ | **ALLOWED** |

*Bertotti, Iess & Tortora (2003); Fomalont (2009); Gaia (2022).*

## 5. The ψ route is empty, not merely expensive — a refinement of G_021

G_021 obtained `γ = +1 ⇔ ψ = −4σ` in `d = 3` by letting `g₀₀` define `Φ`. G_022 applies the stricter requirement: **AT's source law fixes `Φ = σ` independently.** With `A = σ + ψ`:

| law | requirement | consequence |
|---|---|---|
| acceleration | `−A′ = −σ′` | `ψ′ = 0` ⟹ `ψ` constant |
| clock | `e^A = e^σ` | `ψ = 0` |

**`ψ = 0` is forced by both laws.** So the `ψ` completion delivers `γ = −1`, not `+1`: it cannot be the fix at all.

And at G_021's own value the damage is visible:

| location | `x` | `ψ = −4σ = +4x` | clock rate `e^(σ+ψ)` | `z_AT` |
|---|---:|---:|---:|---:|
| Earth surface | 6.961e−10 | +2.785e−9 | 1.000000002 | −2.0e−9 |
| Sun surface | 2.123e−6 | +8.490e−6 | 1.000006368 | −6.4e−6 |
| **J0740+6620** | 0.247002 | **+0.988008** | **2.098045** | **−0.523366** |

At neutron-star compactness the `ψ` completion predicts `z = e^(−3x) − 1 = −0.523366` — a **negative (blue) surface shift at a bound object**. Any positive measured neutron-star redshift refutes that outright.

The clock-law violation `|e^ψ − 1|` is **2.785e−9** at the Earth's surface (invisible) but **1.685879** (169 %) at a neutron star.

**This refines G_021**: the `ψ` sector is not an expensive fix, it is an **empty** one. The `γ = +1` sector must be the reciprocal spatial factor instead — and *that* is a postulate.

## 6. Why the earlier agreements with GR are NOT damaged

This was the question that motivated the audit. Every agreement the G-chain achieved lives in the **`γ`-blind** sector, so G_022's theorem proves they are **invariant under any spatial sector**:

| audit | agreement | sector |
|---|---|---|
| G_004 | `a_AT/a_GR = 0.99600` = `G_SI/G_CODATA` | `g₀₀` / source law |
| G_009, G_015, G_016b, G_017 | clock rates, `Δτ = 86 277.089 s/day` | `g₀₀` |
| G_019 | the signature `AT/GR = 1 + x² − (4/3)x³` | `g₀₀` |
| G_020 | `z_AT = e^x − 1`, J0740+6620 = **0.2801817** | `g₀₀` |
| G_001–G_005 | the source law and the four-scale calibration / MOND phenomenology | `ρ` |

**There is no contradiction: the numbers that agreed still agree.** `γ` is a *separate observable class* — light bending and the Shapiro delay — that **no G-chain audit exercised before G_021**. The failure G_021 and G_022 expose is confined to that class.

## 7. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the **`γ`-blindness** of all three named laws; the **theorem** "4D conformal flatness + `Φ = σ` ⟹ `γ = −1`" **for any conformal factor**; the unique native spatial metric `Ω = ρ^(1/d)` with volume measure exactly `ρ`; and the explicit `γ = +1` sector `g_rr = ρ^(−2/d)` with `g₀₀ = −ρ^(2/d)` intact. |
| **BOUNDARY** | the `γ = +1` sector is a **postulate**: it needs **reciprocal factors in the two sectors**, i.e. abandoning 4D conformal flatness, so the causal-order → conformal-class derivation no longer applies; its cost is a spatial volume measure of **`1/ρ`** instead of `ρ`. And the `ψ` route is **empty** (`ψ = 0` is forced), not merely expensive. |
| **REFUTED** | that the three laws imply or permit a **derived** `γ ≈ +1` — they are silent on `γ`; and the `ψ` completion, which predicts a surface **blueshift** at compact objects. |

## 8. Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; **no new primitive** introduced by this audit. G_021's other results stand; its `ψ = −4σ` derivation is *refined* here — valid under "`Φ` defined by `g₀₀`", but empty once AT's source law fixes `Φ = σ`. Deterministic: exact algebra on AT's own constructions.

* `γ` is a **weak-field** parameter. The `γ = +1` assertion is therefore made only where `x ≤ 1e−5`; at neutron-star compactness the exact `e^(2B)` behaviour matters and `γ` is not the meaningful description.
* The conformal-flatness theorem holds for any `Ω² ≠ 1`; the degenerate `Ω² = 1` case is flat space (`γ` undefined).
* The `β`-family `g_rr = e^(2βσ)` is a one-parameter probe of the spatial sector, not a claim that AT contains such a family.

## Open problems (OP1–OP5)

1. **Is there any AT-native principle that selects a reciprocal spatial factor?** If not, `γ = +1` is permanently a postulate.
2. **Can a non-conformal spatial sector be sourced by `ρ` alone?** That would be a spatial field equation AT does not currently have.
3. **Does the reciprocal sector preserve the four-scale calibration** once the `e^(−B)` factor propagates (`2x = 4.2e−6` at the Sun, `0.49` at a neutron star)?
4. **What does `Ω³ = 1/ρ` mean for the counting measure?** A spatial volume measure below one cell destroys the integer-count meaning of `ρ`; the regime where this matters must be quantified.
5. **Is the conformal-class import (Malament) binding, or could a native derivation admit two factors?**

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_022_Tests.cs` — **7/7 PASSED** (~0.04 s)
**Group total:** G_001–G_022 = **189/189 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_022"`

**Headline:** the three laws are `γ`-blind; conformal flatness forces `γ = −1` for *any* factor; `γ = +1` needs reciprocal factors in the two sectors, which is a postulate whose cost is a volume measure of `1/ρ`; the `ψ` route is empty; and nothing that previously agreed with GR is affected.
