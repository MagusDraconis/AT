# Y_G_030_Result.md — ResearchY-G_030 No-Go Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_030_Tests.cs`
**Core:** `AT.Core/ResearchXH/NoGoTheorem.cs`
**Run:** 2026-09-13
**Result:** ✅ 8/8 PASSED (~0.04 s) — group G total **245/245 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_030"`

## The theorem — **NO-GO**

> With `A = σ` fixed (G_028), **every local `B(r) = F(σ)` violates at least one of Cassini, light deflection, or the no-new-primitive rule.**

### Two exact equivalences

```
(i)   γ = −1   ⟺   B = σ                — the counting measure √det g_ij = ρ
(ii)  γ = +1   ⟺   e^(2B) = 2 − e^(2A)  — g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|
```

Both follow from the γ inversion `B(γ) = ½ln(1 − γ(e^(2A) − 1))`, verified by solving. **The two special γ values correspond exactly to the two special B's — and only the excluded one is AT-derived.**

### Six of the eight constraints are inert

Newton, the Earth clock, GPS, the neutron-star audit and the clock sector depend **only on A** — untouched by B. Light deflection is `(1+γ)/2`, i.e. **the same test as Cassini**. The live pair is **{Cassini/deflection} vs {no-new-primitive}**.

### Proof

- `F = Id` (the counting measure — AT's **only** determination of B from ρ): `B = σ` → `γ = −1` → **8.6957e4 σ** outside the Cassini band `[0.9999520, 1.0000900]`, deflection **identically 0**.
- `F ≠ Id`: no AT-derived value in B → `F`'s specification is an input beyond ρ → **a new primitive** (G_023's accounting).

Every other ingredient fixes **A**, fixes **numbers** (`occ`, `Σm`, `#g`), or supplies **free content** (the traceless face ψ). None determines B. **∎**

### The admitted band is a sliver, on the wrong side of zero

| body | admitted B band | width | derived B = σ |
|---|---|---|---|
| Earth | ≈ [3.99979e−6, 4.00034e−6] | **5.5e−10** | −4.076e−6 |

At solar compactness the band sits at **`B ≈ +x = −σ`**, while the derived value sits at **`B = σ = −x`** — **opposite sides of zero**. The derived value is not displaced; it has the wrong sign.

**Rigorous, no sampling:** `NoGoHoldsExactly(x)` inverts γ to get the band edges and checks that the derived B is outside — true at every body. **Sampled confirmation:** 20 001 points over `[−2x, +2x]` at every body — every entrant to the band still fails the counting measure, **zero escapes**.

### The departure is not a small correction

| x | √det g_ij/ρ | departure |
|---|---|---|
| Earth | 1.000000004 | 4.2e−9 |
| Sun | 1.000012735 | 1.3e−5 |
| 0.1 | 1.733052388 | 7.3e−1 |
| **J0740+6620** | **3.437584871** | **2.44** |
| 1 | 51.142808724 | 50.1 |

Growth of **>10⁵** from the Sun to the densest measured star; **O(1) exactly where the theory is most discriminating.**

## Goal — prove or refute a surviving spatial sector

- **REFUTED as a DERIVATION:** none can be derived from ρ. This is G_029's *"chosen, not derived"* **elevated to a theorem** — G_029 found the survivor by postulate, G_030 proves why no derivation exists.
- **AFFIRMED as a POSTULATE:** `g_rr = 2 − ρ^(2/d)` survives observationally; it is simply not reachable from ρ alone.

**Sharpest form:** `γ = −1 ⟺ B = σ` (the only AT derivation — refuted) and `γ = +1 ⟺ g_rr = 2 − |g₀₀|` (the survivor — underivable). **There is no third.**

## Consequence: a dilemma with exactly two horns

1. **Keep the counting measure** → `γ = −1` → Cassini excludes it at 8.6957e4 σ; deflection and Shapiro are zero.
2. **Abandon it** → `g_rr = 2 − ρ^(2/d)` → all optics correct, but the volume–count identification is replaced by an input the theory does not supply.

The choice is not numerical — it is a choice about **which of AT's identifications is primitive**. G_030 proves the two cannot be had together.

## Tests

| test | result |
|---|---|
| `GammaMinusOneIsEquivalentToTheCountingMeasure` | ✅ |
| `GammaPlusOneIsEquivalentToTheReflectionForm` | ✅ |
| `CassiniExcludesTheOnlyDerivedCandidate` | ✅ |
| `TheSweepFindsNoEscape` | ✅ |
| `TheAdmittedBandIsASliver` | ✅ |
| `TheDepartureBecomesOrderOne` | ✅ |
| `TheCassiniDoorIsWideButNotWideEnough` | ✅ |
| `Run` | ✅ |

**Headline:** the **NO-GO holds**. With the clock closed on `A = σ`, six of the eight constraints are inert, and the remaining pair is a dilemma: AT's only derivation of B (the counting measure, `B = σ`) yields `γ = −1`, excluded at **8.6957e4 σ** with zero deflection and zero Shapiro delay — while the unique optical survivor (`g_rr = 2 − ρ^(2/d)`, G_029) sits on the **opposite side of zero** and can only be postulated. The counting-measure departure grows from **4.2e−9 at Earth to 2.44 at J0740+6620**, so it is not a correction but a replacement.
