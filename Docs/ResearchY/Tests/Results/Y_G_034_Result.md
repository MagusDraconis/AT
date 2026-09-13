# Y_G_034 Result — A₀ Robustness Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_034_Tests.cs`
**Status:** 6/6 PASSED (~0.8 s)
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_034"`
**Group total:** G_001–G_034 = **271/271 PASSED**
**Affected-set total:** 321/321 PASSED (G group + T_008–T_014 + D_047)

## Verdict

**REFUTED** — six pinned G-series values were floating-point artifacts and fail at their own stated precision. **But no G-series conclusion breaks**: 7 UNCHANGED · 2 BOUNDARY · 6 REFUTED, and every inequality, identity and ordering survives.

## The replacement applied

`SpectralCaseCatalog.D96CubedBreakdown` now **clusters** its 49³ triple sums at a tolerance — the rule **D_047 already documents** (`TolCube = 1e-8; // 3-factor sums carry ~1e-14 noise; tol above it`).

**Reproduces D_047 on 5/5 independent figures:**

| figure | D_047 | clustered |
|---|---|---|
| A₀ | 16 080 | **16 080** |
| A₀ (m>1) | 16 079 | **16 079** |
| max multiplicity | 738 | **738** |
| lock release | 4.16569 | **4.165691** |
| Σ(m−1) | 868 656 | **868 656** |

## Root cause — mixed equality discipline

| lattice | construction | rule | A₀ | lock |
|---|---|---|---|---|
| **D96** | real eigensolver | **tolerant** | **45** | **0.802314** |
| **D96³** | `Dictionary<double,int>` | **exact** | **20 812** | **3.948614** |

Same quantity, two equality rules, one programme. D96's invariants never moved because that path was already tolerant.

## What moved

| quantity | before | after | change |
|---|---|---|---|
| A₀ | 20 812 | **16 080** | **−22.7 %** |
| A₀ (m>1) | 20 811 | **16 079** | −22.7 % |
| free room Σ(m−1) | 863 924 | **868 656** | +0.55 % |
| latent fraction L | 0.976477 | **0.981825** | +0.55 % |
| lock release (nats) | 3.948614 | **4.165691** | **+5.50 %** |
| max multiplicity | 562 | **738** | **+31.3 %** |

## Classification — 7 UNCHANGED · 2 BOUNDARY · 6 REFUTED

**REFUTED (6)** — G_002 A₀ = 20 812; G_002 max multiplicity = 562; G_003 D96³-vs-D96 ΔA = 0.276596 → **0.263158**; G_005 cube lock = 3.948614 → **4.165691**; G_005 cube L = 0.97648 → **0.981825**; G_006 the same lock restated.

**Pinned to the artifact by, in their own tolerances:** G_003 **13 438×**, G_005 lock **21 708×**, G_005 L **535×**.

**UNCHANGED (7)** — cube free fraction > 0.97 (holds at 0.981825 *and* 0.97648); G_006's L > 0.97; free room = N − A₀ = the D_048 latent fraction L (identity); cube free room > D96's (ordering); D96 A₀ = 45; D96 lock = 0.802314; D96 contraction 33.78.

**BOUNDARY (2)** — "97.6 % energy-free" → **98.2 %**; cube contraction at m = 200: **7.33 → 7.10**.

## Why nothing collapses

`ConclusionsSurvive() = true`, checked on both keyings: the cube is still ~98 % energy-free; free room is still exactly N − A₀ and still equals L; D96's invariants never moved; the ordering arrangement > degeneracy > lattice-average > survivor compression is intact; the regime is still **CONTROLLABLE**. **The refuted items are numbers, not conclusions.**

## Blast radius (shared infrastructure)

| suite | outcome |
|---|---|
| G_002/003/005/006 | 4 assertions updated to corrected values |
| G_033 | live scanner now excludes **two** meta-audits (G_033, G_034) |
| T_012 | **headline UNCHANGED** (D96³ → S∞ = 16, distance 3; random 17 nearest); sub-claim 21 → **14** |
| T_013 | assertions pass; figures updated: A **16 079**, C **1004.9**, max mult **738**, "~150×" → **~114×** |
| T_014, T_008–T_011, **D_047** | **all pass unchanged** — D_047 needed nothing, it was already correct |

## Files

- Core: `AT.Core/ResearchXH/A0RobustnessAudit.cs`
- Shared fix: `AT.Tests/Shared/SpectralCaseCatalog.cs` (`D96CubedBreakdown` now clusters)
- Suite: `AT.Tests/ResearchY/G_GravitySource/Y_G_034_Tests.cs`
- Doc: `Docs/ResearchY/G_GravitySource/ResearchY-G_034.md`
