# Y_G_029_Result.md — ResearchY-G_029 Spatial Sector Closure Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_029_Tests.cs`
**Core:** `AT.Core/ResearchXH/SpatialSectorClosure.cs`
**Run:** 2026-09-13
**Result:** ✅ 7/7 PASSED (~0.04 s) — group G total **237/237 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_029"`

## Question

With `A = σ` **fixed by G_028**, can any spatial metric `B(r)` survive all eight constraints without a new primitive?

## What the constraints actually bind

| requirements | depend on |
|---|---|
| 1 Newton limit · 2 Earth clock · 3 GPS · 4 neutron-star audit | **A only** → passed by **every** candidate |
| 5 Cassini γ · 6 deflection · 7 Shapiro | **γ only** → **one number** |
| 8 no new primitive | the primitive base — B's freedom is ψ's traceless face (G_024) |

The requirements constrain **one number**, not a function.

## Candidate table

| family | B (J0740) | γ | defl/GR | Shapiro/GR | √det g_ij/ρ | verdict |
|---|---|---|---|---|---|---|
| **B = σ** | −0.247002 | **−1.000000** | **0** | **0** | **1.000000** | **REFUTED** — Cassini **8.6957e4 σ** |
| **B = −σ** | +0.247002 | 1.638865 | 1.319433 | 1.319433 | 4.401793 | SURVIVES (1st order) |
| **B = ½ln(2−e^(2σ))** | +0.1645877 | **1.000000** | **1.000000** | **1.000000** | 3.437585 | **SURVIVES — identified** |
| B = −σ − 2σ² | +0.12596 | 0.350133 | 0.675067 | 0.675067 | — | SURVIVES (c₂ free) |
| rational/exponential | — | ≈1+O(x) | ≈1 | ≈1 | — | SURVIVES (unselected) |

Solar-system point (x = 4e−6): B = σ → γ−1 = −2, i.e. **8.6957e4 σ**; linear member → **0.565 σ**.

## The unique survivor — identified

```
e^(2B) = 2 − e^(2A)   ⟺   g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|
```

**The spatial metric is the reflection of the temporal one about 1**; the conformal (γ = −1) member is `g_rr = |g₀₀|`. To first order `g_rr = 1 + 2x` — the GR value.

**Non-degeneracy (corrected during this audit — my first derivation had a sign error).** `2 − e^(2A)` vanishes at `A = +½ln2`, i.e. **x = −½ln2**, a *negative* compactness. So the exact member is **non-degenerate everywhere physical**, with `g_rr ∈ (1, 2)` for all x > 0, tending to 2 as x → ∞.

## The sector's honest state

**AT derives no B.** Its only equation — the counting measure — gives the excluded `B = σ`. So the survivor is selected by **observation**:

```
counting measure → B = σ            → γ = −1 → REFUTED (8.6957e4 σ)
GR optics        → g_rr = 2 − ρ^(2/d) → γ = +1 → SURVIVES at a volume cost of 3.437585
```

The cost **reproduces G_023's recorded 3.437585 exactly** (computed: 3.437584871). And the completion is unselected: Cassini bounds the quadratic coefficient only by `|c₂ + 2| ≲ 22.5` at 3σ.

## Output

| label | content |
|---|---|
| **SURVIVES** | the γ = +1 family; **exactly one** member gives γ = +1 at all orders — **`g_rr = 2 − ρ^(2/d)`** |
| **BOUNDARY** | γ is pinned, **B is not** (`|c₂+2| ≲ 22.5`); the survivor **trades the counting measure** (cost 3.437585); **AT derives no B** → the survivor is a **POSTULATE** |
| **REFUTED** | **B = σ** (γ = −1, deflection 0, Shapiro 0, **8.6957e4 σ**) and the counting measure **as the equation for B** |

## Critical outcomes

- **All DERIVABLE B fail** (only `B = σ` is derivable, and it is excluded) → **the spatial sector is CLOSED as a derivation.**
- **Exactly one survives** the strengthened requirement (exact γ = +1) → **identified: `g_rr = 2 − ρ^(2/d)`.**

**The spatial sector closes on a POSTULATE**, at an exact and known price.

## Tests

| test | result |
|---|---|
| `ConformalMemberIsRefutedByCassini` | ✅ |
| `ExactlyOneMemberGivesExactGrOptics` | ✅ |
| `TheExactMemberIsNonDegenerateEverywherePhysical` | ✅ |
| `TheSectorClosesToOneNumberNotAFunction` | ✅ |
| `TheTheoryDerivesNoB` | ✅ |
| `ExactlyOneSurvivesAndIsIdentified` | ✅ |
| `Run` | ✅ |

**Headline:** with the clock closed by G_028, the spatial sector admits **exactly one** member with exact GR optics — **`g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|`**, the reflection of the temporal metric about 1. It satisfies all eight requirements, is non-degenerate everywhere physical, and costs the counting measure by the factor **3.437585** G_023 had already recorded. `B = σ` — the only member AT *derives* — is refuted at **8.6957e4 σ**. So the sector **closes on a postulate**: the theory supplies no equation for B that survives, and the requirements pin γ, not B.
