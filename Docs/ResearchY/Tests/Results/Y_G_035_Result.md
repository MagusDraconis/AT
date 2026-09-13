# Y_G_035 Result — Temporal Independence Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_035_Tests.cs`
**Status:** 6/6 PASSED (~4 s)
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_035"`
**Group total:** G_001–G_035 = **277/277 PASSED**

## Verdict

**SURVIVES** — 25 of 36 results need g₀₀ and nothing else; the minimal time sector is non-empty and every temporal observable is B-free. Computed (G_027).

## Classification: 25 SURVIVES · 8 BOUNDARY · 3 REFUTED

| verdict | component | audits |
|---|---|---|
| **SURVIVES** | g₀₀ only | the **entire density era G_001–G_020** (+G_011b), plus the metric-free G_026, G_027, G_034 |
| **BOUNDARY** | g_ij | G_021, G_022, G_023, G_025, G_028, G_029, G_030, G_033 |
| **REFUTED** | A = B | G_024, G_031, G_032 |

## The sharp boundary

**G_020 → G_021** is the conformal boundary — computed, not chosen. Boundary index **23** of 36; temporal era **22** entries.

| era | needs |
|---|---|
| temporal (G_001–G_020) | g₀₀ = −ρ^(2/d) |
| spatial (G_021 …) | g_ij — first asks for light bending |
| conformal | A = B |

## The minimal time sector

> **1 metric function** `g₀₀ = −ρ^(2/d)` **+ 1 scalar** ρ **+ 1 exponent** `1/d` (d = 3, rotation self-duality)
> — **no spatial metric, no conformal factor, no reference η.**

```
A = ½ln(−g₀₀) = σ = (1/d)ln ρ        the clock potential = Φ/c²
dτ/dt = √(−g₀₀) = ρ^(1/d)            the clock
a = −(1/d)∇ln ρ = −∇A                 the source law — the NEGATIVE gradient of A
```

The source law and the clock law are **one statement**.

## The arity proof

Temporal observables take **no B**: `G00(rho)`, `ClockOf(rho)`, `ClockPotentialOf(rho)`, `RedshiftOf(rho)`, `SourceAccelerationOf(rhoField)`, `SecondOrderRatioOf(x)`.
Spatial observables **do**: `GammaOf(a, b)`, `AdmittedBand(x)`, `SurvivorB(x)`.
**A result that cannot be *called* with B cannot *require* B.**

Verified: g₀₀, the clock, the redshift (e^0.247002 − 1), G_019's `e^x/√(1+2x)` = 1 + x² − (4/3)x³, the source law as the exact central difference of −A, and **inward** acceleration on a peaked profile with the peak as an equilibrium.

## The scan's honesty about its own limits

**Two patterns removed after measurement** (they matched non-metric code and would have made the safety rule meaningless):

| removed | actually matched |
|---|---|
| `double b` | a least-squares slope (G_006); a tridiagonal parameter (G_007, G_016b) |
| `K(` | any method of that name |

Bare `psi` excluded on purpose — it is the **wavefunction** in the density era (`|ψ|² = ρ`) and the metric's traceless face elsewhere. Rule adopted: **a token qualifies only if it cannot plausibly mean anything else.**

**Two claims triaged `ScanDetectsIt = false` with reasons**: G_025 (dependence is an *exponent*; `psi` unusable) and G_033 (no metric vocabulary at all — its argument is group-theoretic).

**Check strength:** HARD (asserted) — `ClockOnly` ⟹ **zero** executable spatial references, holds for all 25; SOFT — spatial/conformal should show ≥1, unless triaged. Comments **and string literals** are stripped, so a narrative mention cannot mask a dependency.

## Consequence

G_031: conformal flatness **is** the counting measure. G_032: it is an **assumed primitive η**, and imposing it is **refuted** (γ = −1, Cassini 8.6957e4 σ).

> **The conformal problem is confined to the spatial sector. AT's time half is not hostage to its space half.**

## Files

- Core: `AT.Core/ResearchXH/TemporalIndependenceAudit.cs`
- Suite: `AT.Tests/ResearchY/G_GravitySource/Y_G_035_Tests.cs`
- Doc: `Docs/ResearchY/G_GravitySource/ResearchY-G_035.md`
