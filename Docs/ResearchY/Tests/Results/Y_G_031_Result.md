# Y_G_031 Result — Spatial-Origin Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_031_Tests.cs`
**Status:** 7/7 PASSED (~0.11 s)
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_031"`
**Group total:** G_001–G_031 = **252/252 PASSED**

## Verdict

**REQUIRED** — computed, not typed.

`B = σ` is not an assumption anywhere in the theory; it is **entailed** by the conformal ansatz at the clock exponent, and it is consumed in five load-bearing places.

## The theorem (executed, not asserted)

```
g_μν = ρ^(2n)·η_μν  ⟹  A = B = n·ln ρ,  √(−g₀₀) = ρ^n,  √det g_ij = ρ^(n·d)
d = 3:  n = 1/d  ⟺  √(−g₀₀) = ρ^(1/d)  ⟺  √det g_ij = ρ
```

Clock law and counting measure are **one equation**. Exhaustive sweep 401 × 401 in `(n, ρ)`: **0 mismatches**. Excluded degenerate point `ρ = 1` (flat space, every exponent agrees) is separately asserted to be degenerate.

## Provenance chain

| step | provenance | basis |
|---|---|---|
| Difference → Density | DERIVED | QG285/QG286/QG292 |
| Density → Metric | ASSUMED | QG207 metric ansatz |
| exponent `n = 1/d` | ASSUMED | = the clock law (G_004, G_009) |
| Metric → Spatial measure | DERIVED | algebraic identity at `n = 1/d` |
| ρ as geometric volume | CORRESPONDENCE | G_018 provenance asymmetry |

## Usage inventory

5 LoadBearing — metric ansatz; `N = ∫ρ dV` (QG194/222); deficit accounting (QG181/182); horizon area→entropy (QG185/QG259); cosmological densities.
2 Neutral — `|ψ|² = ρ` (QG216); RAR scale (QG080).

## Breakage if removed (geometric ÷ count volume)

| body | ratio |
|---|---|
| Sun | 1.000012735 |
| x = 1e−4 | 1.000600120 |
| x = 0.1 | 1.733052388 |
| J0740+6620 | **3.437584871** |
| x = 1 | 51.142808724 |

Four breakages, each at magnitude **2.44** at J0740+6620: count conservation; deficit accounting; area/entropy; cosmological densities.

## Renarration of G_023

"There are two pins" is **wrong**. Conformal flatness gives `A = B`; one exponent choice gives both values. The counting measure is a **theorem**, not a premise.

**Sharper dilemma (supersedes G_030's phrasing):** not *counting measure vs optics*, but **conformal flatness + clock law vs optics**.

## Files

- Core: `AT.Core/ResearchXH/SpatialOriginAudit.cs`
- Suite: `AT.Tests/ResearchY/G_GravitySource/Y_G_031_Tests.cs`
- Doc: `Docs/ResearchY/G_GravitySource/ResearchY-G_031.md`
