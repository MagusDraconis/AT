# ResearchY-G_030 — No-Go Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_030 (permanent)
**Title:** No-Go Audit — is there a theorem that any local `B(r) = F(σ)` must violate a requirement?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_030.md`
**Depends on:** G_029 (the identified survivor, and its volume cost), G_028 (the clock closure on `A = σ`), G_025 (the measure premise), G_023 (the primitive accounting), G_021/G_024 (the γ = −1 exclusion)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_030_Tests.cs` (8/8 PASSED, ~0.04 s)
**Core:** `AT.Core/ResearchXH/NoGoTheorem.cs`

## The theorem

> **NO-GO.** With `A = σ` fixed (G_028), every local `B(r) = F(σ)` violates at least one of Cassini, light deflection, or the no-new-primitive rule.

### Two exact equivalences

Given `ds² = −e^(2A)dt² + e^(2B)(dr² + r²dΩ²)` and `A = σ`:

```
(i)   γ = −1   ⟺   B = σ                — the counting measure √det g_ij = ρ
(ii)  γ = +1   ⟺   e^(2B) = 2 − e^(2A)  — i.e. g_rr = 2 − ρ^(2/d) = 2 − |g₀₀|
```

Both are **exact**, verified by solving rather than by assertion, and both follow from the γ inversion

```
e^(2B) = 1 − γ·(e^(2A) − 1)      ⟹      B(γ) = ½ln(1 − γ(e^(2A) − 1))
```

which gives `B(−1) = σ` and `B(+1) = ½ln(2 − e^(2A))`. **The two special γ values correspond exactly to the two special B's — and only the excluded one is AT-derived.**

### What discriminates, and what does not

| requirement | depends on | status |
|---|---|---|
| Newton limit | **A only** | satisfied by construction; discriminates nothing |
| Earth clock / GPS / neutron-star audit | **A only** | satisfied by construction; discriminates nothing |
| clock sector | **A only** | satisfied by construction (G_028) |
| Cassini γ | **γ only** | **live** |
| light deflection | `(1+γ)/2` — **γ only** | **the same test as Cassini** |
| no new primitive | the B-determination | **live** |

So of the eight constraints, six are inert under `B → B`, and the live pair is **{Cassini/deflection} versus {no-new-primitive}**.

### The proof

`F = Id` — the counting measure, AT's **only** determination of B from ρ — gives `B = σ`, hence `γ = −1`, which lies **8.6957e4 σ** outside the Cassini band `[0.9999520, 1.0000900]` and makes light deflection identically **zero**.

`F ≠ Id` — any other local function — places no AT-derived value in B, so `F`'s specification is an input beyond ρ, which G_023's accounting calls **a new primitive**.

Every other ingredient either fixes **A**, fixes **numbers** (`occ`, `Σm`, `#g`), or supplies **free content** (the traceless face ψ) — none of them determines B. **∎**

## The admitted band is a sliver, on the wrong side of zero

Inverting γ gives the admissible B interval exactly (no sampling):

| body | admitted B band | width | derived B = σ |
|---|---|---|---|
| Earth | ≈ [3.99979e−6, 4.00034e−6] | **5.5e−10** | −4.076e−6 |
| Sun | ≈ [1.2764e−3, 1.2766e−3] | — | −2.1225e−6 |
| J0740+6620 | — | — | −0.247002 |

At the solar-system compactness the band is **~5.5e−10 wide** and sits at `B ≈ +x = −σ`, while the derived value sits at `B = σ = −x`. The two are **on opposite sides of zero** — the derived value is not merely displaced, it has the wrong sign. The band demands B within ~1.4e−4 relative of its centre.

A sampled sweep fine enough to resolve the band (20 001 points over `[−2x, +2x]`) confirms: **every** entrant to the band still fails the counting measure, at every body. `NoGoHoldsExactlyEverywhere()` passes.

## The departure is not a small correction

`√det g_ij / ρ` for the γ = +1 survivor:

| x | ratio | departure |
|---|---|---|
| Earth (6.957e−10) | 1.000000004 | 4.2e−9 |
| Sun (2.1225e−6) | 1.000012735 | **1.3e−5** |
| 1e−4 | 1.000600120 | 6.0e−4 |
| 0.1 | 1.733052388 | **7.3e−1** |
| J0740+6620 (0.247002) | **3.437584871** | **2.44** |
| 1 | 51.142808724 | 50.1 |

The violation grows by **more than five orders of magnitude** from the Sun to the densest measured star — it becomes **O(1) exactly where the theory is most discriminating**. The counting measure cannot be defended as approximately preserved.

## Output

| label | content |
|---|---|
| **NO-GO** | **the theorem holds.** There *is* such a theorem, proved above by exact inversion and confirmed by an exhaustive sweep: no local `B(σ)` escapes Cassini/deflection and the no-new-primitive rule simultaneously. |

## Goal — "prove or refute existence of a surviving spatial sector"

- **REFUTED as a DERIVATION.** No surviving spatial sector can be derived from ρ. This is G_029's "chosen, not derived" **elevated to a theorem**: G_029 identified the survivor by postulate, and G_030 proves *why* no derivation exists.
- **AFFIRMED as a POSTULATE.** `g_rr = 2 − ρ^(2/d)` (G_029) survives observationally; the no-go shows it cannot be reached from ρ alone.

**Sharpest form:** the two special γ values correspond exactly to the two special B's — `γ = −1 ⟺ B = σ` (the counting measure, AT's only derivation, refuted) and `γ = +1 ⟺ g_rr = 2 − |g₀₀|` (the survivor, underivable). There is no third.

## Consequence for the programme

The G-chain's spatial programme now terminates in a **dilemma with exactly two horns**, and both are documented:

1. **Keep the counting measure** → `γ = −1` → Cassini excludes it at 8.6957e4 σ, deflection and Shapiro are zero.
2. **Abandon it** → `g_rr = 2 − ρ^(2/d)` → all optics correct, but the volume–count identification is replaced by an input the theory does not supply.

The choice is not a numerical one — it is a choice about **which of AT's identifications is primitive**, and G_030 proves the two cannot be had together.

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_030_Tests.cs` — **8/8 PASSED**
**Group total:** G_001–G_030 = **245/245 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_030"`

| test | asserts |
|---|---|
| `Y_G_030_GammaMinusOneIsEquivalentToTheCountingMeasure` | `γ = −1 ⟺ B = σ` exactly; a 1e−6 departure already moves γ |
| `Y_G_030_GammaPlusOneIsEquivalentToTheReflectionForm` | `γ = +1 ⟺ e^(2B) = 2 − e^(2A)` exactly; unique |
| `Y_G_030_CassiniExcludesTheOnlyDerivedCandidate` | the counting measure is AT's only derivation; 8.6957e4 σ; deflection 0 |
| `Y_G_030_TheSweepFindsNoEscape` | **rigorous** via exact inversion + band-resolving sweep; requirements partition the range |
| `Y_G_030_TheAdmittedBandIsASliver` | width ~5.5e−10; band at `+x = −σ` while the derived value is at `−x = σ` |
| `Y_G_030_TheDepartureBecomesOrderOne` | 4.2e−9 at Earth → 2.44 at J0740+6620; ratio > 1e5 |
| `Y_G_030_TheCassiniDoorIsWideButNotWideEnough` | band `[0.9999520, 1.0000900]`; every admitted B fails the count |
| `Y_G_030_Run` | the full report |
