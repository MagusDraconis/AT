# ResearchY-G_029 — Spatial Sector Closure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_029 (permanent)
**Title:** Spatial Sector Closure Audit — can any B(r) survive all constraints without a new primitive?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_029.md`
**Depends on:** G_028 (which closed the clock on `A = σ`), G_023 (the invariant `k = B − A` and the volume cost), G_021/G_024 (the conformal γ = −1 exclusion), G_025 (the corrected determinant identities), G_027 (no verdict may be a literal)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_029_Tests.cs` (7/7 PASSED, ~0.04 s)
**Core:** `AT.Core/ResearchXH/SpatialSectorClosure.cs`

## The question

With `A = σ = (1/d)ln ρ` **fixed by G_028**, can any spatial metric `B(r)` survive all eight constraints without a new primitive?

```
ds² = −e^(2A)dt² + e^(2B)(dr² + r²dΩ²)
γ = −(e^(2B) − 1)/(e^(2A) − 1)        deflection ∝ Shapiro ∝ (1 + γ)/2
counting measure:  √det g_ij = ρ  ⟺  e^(3B) = ρ  ⟺  B = σ
```

## What the eight requirements actually constrain

| requirements | depend on | consequence |
|---|---|---|
| 1 Newton limit, 2 Earth clock, 3 GPS, 4 neutron-star audit | **A only** | **passed by EVERY candidate** — A = σ is untouched |
| 5 Cassini γ, 6 light deflection, 7 Shapiro delay | **γ only** | pin **one number**, not a function |
| 8 no new primitive | the primitive base | the B-freedom is ψ's traceless face (G_024: not a new primitive) |

So the requirements constrain **one number**. The *entire nonlinear completion* of B stays free.

## The candidate table

| family | B | γ at J0740+6620 | deflection/GR | Shapiro/GR | √det g_ij/ρ | verdict |
|---|---|---|---|---|---|---|
| **B = σ** (counting measure) | −0.247002 | **−1.000000** | **0** | **0** | **1.000000** | **REFUTED** — Cassini **8.6957e4 σ** |
| **B = −σ** (linear) | +0.247002 | 1.638865 | 1.319433 | 1.319433 | 4.401793 | SURVIVES (to 1st order) |
| **B = ½ln(2−e^(2σ))** (exact) | +0.1645877 | **1.000000** | **1.000000** | **1.000000** | 3.437585 | **SURVIVES — identified** |
| B = −σ − 2σ² (polynomial) | +0.12596 | 0.350133 | 0.675067 | 0.675067 | — | SURVIVES (c₂ free) |
| rational/exponential (λ = 0.25, s = 0.05) | — | ≈1 + O(x) | ≈1 | ≈1 | — | SURVIVES (unselected) |

At the solar-system point Cassini probes (x = 4e−6): **B = σ gives γ−1 = −2**, i.e. **8.6957e4 σ**; the linear member gives γ−1 = 8e−6, i.e. **0.565 σ**.

## The unique survivor, identified

`γ = +1` at **all orders** has exactly one solution:

```
e^(2B) = 2 − e^(2A)   ⟺   g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|
```

**The spatial metric is the reflection of the temporal one about 1.** For contrast, the conformal (γ = −1) member is exactly `g_rr = |g₀₀|`. To first order `g_rr = 1 + 2x = 1 − 2γΦ` with Φ = −x and γ = +1 — the **GR** value.

**Non-degeneracy (corrected during this audit).** `2 − e^(2A)` vanishes only at `A = +½ln2`, i.e. **x = −½ln2 = −0.3465735903** — a *negative* compactness, a repulsive object. So the exact member has **no degeneracy anywhere physical**, and `g_rr = 2 − ρ^(2/d)` is bounded in **(1, 2)** for every `x > 0`, tending to 2 as x → ∞. Space is uniformly **stretched** — the GR signature — and never collapses.

## The sector's honest state

**AT derives no B.** Its only equation for B is the counting measure, and that yields `B = σ` — the member Cassini excludes at 8.6957e4 σ. The survivor above is therefore selected by **observation**, not by the theory:

```
the counting measure  →  B = σ        →  γ = −1        →  REFUTED (8.6957e4 σ)
GR optics             →  g_rr = 2 − ρ^(2/d)  →  γ = +1  →  SURVIVES, at a volume cost of 3.437585
```

That cost **reproduces G_023's recorded 3.437585 exactly** (computed here as 3.437584871).

**And the completion is unselected.** Cassini constrains only γ, so the quadratic coefficient of `B = −σ + c₂σ²` is bounded only by `|c₂ + 2| ≲ 22.5` at 3 σ — a sweep of c₂ from −20 to +18 keeps most of its range inside Cassini. The requirements fix **γ**, not **B**.

## Output

| label | content |
|---|---|
| **SURVIVES** | the `γ = +1` family — and **exactly one** member gives `γ = +1` at all orders: **`g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|`** (equivalently `B = ½ln(2 − e^(2σ))`). It satisfies all eight requirements. |
| **BOUNDARY** | (a) the requirements pin γ, not B — the nonlinear completion is unselected (`|c₂+2| ≲ 22.5`); (b) the survivor **trades the counting measure** for GR optics (volume cost 3.437585); (c) **AT derives no B**, so the survivor is a **POSTULATE**. |
| **REFUTED** | **`B = σ`** — γ = −1, deflection 0, Shapiro 0, Cassini separation **8.6957e4 σ**; and with it the **counting measure as the equation for B**. |

## Critical outcomes

- *"If all admissible B fail, close the spatial sector."* — **all DERIVABLE B fail**: the only derivable member is `B = σ`, and it is excluded. → **the spatial sector is CLOSED as a derivation.**
- *"If exactly one survives, identify it."* — **exactly one** survives the strengthened requirement (exact γ = +1): `g_rr = 2 − ρ^(2/d)`. **Identified.**

**The spatial sector closes on a POSTULATE.** The counting measure is traded for GR optics; the price is exact and known (3.437585× the count at J0740+6620); and no AT-native principle selects the replacement. G_023 reached the same cost from the `k = B − A` direction; this audit reaches it from the observation side and names the survivor in closed form.

## Opened

1. **Is there an AT-native principle for `g_rr = 2 − |g₀₀|`?** The reflection form is suggestive (see G_022's reciprocal-factor discussion) but no derivation was found.
2. **Does the 3.437585 volume cost break anything else** — the four-scale calibration, the deficit accounting (QG181/182), the spectral identification of `occ`?
3. **Strong-field behaviour**: the exact member tends to `g_rr → 2` as x → ∞ with `|g₀₀| → 0`; the horizon structure and the Hawking-temperature chain (QG208) should be re-examined on the new B.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_029_Tests.cs` — **7/7 PASSED**
**Group total:** G_001–G_029 = **237/237 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_029"`

| test | asserts |
|---|---|
| `Y_G_029_ConformalMemberIsRefutedByCassini` | B = σ: γ = −1, deflection 0, Shapiro 0, >8e4 σ; and A is preserved by every candidate |
| `Y_G_029_ExactlyOneMemberGivesExactGrOptics` | `e^(2B) = 2 − e^(2A)` exactly; closed form agrees; the linear member overshoots |
| `Y_G_029_TheExactMemberIsNonDegenerateEverywherePhysical` | no degeneracy for x > 0; `g_rr ∈ (1,2)`; the zero is at x = −½ln2 |
| `Y_G_029_TheSectorClosesToOneNumberNotAFunction` | Cassini leaves `|c₂+2| ≳ 20`; most of a c₂ sweep is allowed |
| `Y_G_029_TheTheoryDerivesNoB` | the only derivable B does not survive; volume cost 3.437584871 |
| `Y_G_029_ExactlyOneSurvivesAndIsIdentified` | `g_rr = 2 − |g₀₀|`; conformal member is `g_rr = |g₀₀|` |
| `Y_G_029_Run` | the full report |
