# ResearchY-G_034 — A₀ Robustness Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_034 (permanent)
**Title:** A₀ Robustness Audit — which gravity/time conclusions depend quantitatively on A₀?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_034.md`
**Depends on:** G_033 (the A₀ defect), G_002/G_003/G_005/G_006 (the consumers), G_027 (computed verdicts), D_047 (the independent, already-correct lineage), M_012
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_034_Tests.cs` (6/6 PASSED, ~0.8 s)
**Core:** `AT.Core/ResearchXH/A0RobustnessAudit.cs`

## The answer: qualitative conclusions UNCHANGED, six pinned values REFUTED

G_033 found that **A₀ = 20 812 was a floating-point artifact** and registered the replacement as OP1. This audit **performs the replacement** and measures every A₀-derived quantity and every G-series conclusion that consumes one.

> **No G-series conclusion breaks. Six pinned numeric values do.** That distinction is the whole result: the G-series' *content* was never resting on the artifact, but several of its *quoted numbers* were.

## The replacement

`SpectralCaseCatalog.D96CubedBreakdown` keyed its 49³ triple sums on **exact binary64 equality**; it now **clusters at a tolerance**. The tolerance is not invented — it is the rule **D_047 already documents and applies**:

```csharp
private const double TolCube = 1e-8;  // 3-factor sums carry ~1e-14 noise; tol above it
```

**The corrected spectrum reproduces D_047 on five independent figures** (and M_012's 16 080):

| figure | D_047 (independent) | clustered | match |
|---|---|---|---|
| A₀ | 16 080 | **16 080** | ✓ |
| A₀ (m > 1) | 16 079 | **16 079** | ✓ |
| max multiplicity | 738 | **738** | ✓ |
| lock release | 4.16569 | **4.165691** | ✓ |
| Σ(m−1) free room | 868 656 | **868 656** | ✓ |

**5/5 match.** The G-series is now consistent with the D-series instead of contradicting it.

## Root cause: a mixed equality discipline

| lattice | construction | equality rule | A₀ | lock release |
|---|---|---|---|---|
| **D96** (1D) | `AttractorDominanceAnalyzer.Eigenspaces` — a real eigensolver | **tolerant** | **45** | **0.802314** |
| **D96³** (cube) | `D96CubedBreakdown` — `Dictionary<double,int>` | **exact** | **20 812** | **3.948614** |

The same programme used **two different equality rules for the same kind of quantity** — which is exactly why D96's own invariants never moved while the cube's did. D96 already carried the tolerant answers (A₀ = 45, lock 0.802314, free room 51); only the cube was mis-keyed.

## What moved

| quantity | exact-double (before) | clustered (after) | change |
|---|---|---|---|
| A₀ (eigenspaces) | 20 812 | **16 080** | **−22.7 %** |
| A₀ (m > 1) | 20 811 | **16 079** | −22.7 % |
| free room Σ(m−1) | 863 924 | **868 656** | +0.55 % |
| latent fraction L | 0.976477 | **0.981825** | +0.55 % |
| lock release (nats) | 3.948614 | **4.165691** | **+5.50 %** |
| max multiplicity | 562 | **738** | **+31.3 %** |

All six move. Note the asymmetry: A₀ moves 23 %, but the *fractions* move only 0.55 % — which is why the conclusions survive while the counts do not.

## Every G-series conclusion that consumes one

Fifteen rows, classified by the computation (never typed):

| impact | audit | claim | before | after |
|---|---|---|---|---|
| **REFUTED** | G_002 | the cube's eigenspace count | 20 812 | 16 080 |
| **REFUTED** | G_002 | the cube's largest multiplicity | 562 | 738 |
| **REFUTED** | G_003 | the D96³-vs-D96 density delta | 0.276596 | **0.263158** |
| **REFUTED** | G_005 | the cube's lock release | 3.948614 | 4.165691 |
| **REFUTED** | G_005 | the cube's latent fraction | 0.97648 | 0.981825 |
| **REFUTED** | G_006 | the cube's lock release (restated) | 3.948614 | 4.165691 |
| UNCHANGED | G_002 | the cube is overwhelmingly energy-free (> 0.97) | 0.97648 | 0.981825 |
| UNCHANGED | G_006 | the cube has plenty of free room (> 0.97) | 0.97648 | 0.981825 |
| UNCHANGED | G_002 | free room = N − A₀ = the D_048 latent fraction L | identity | identity |
| UNCHANGED | G_002 | the cube's free room exceeds D96's | ordering | ordering |
| UNCHANGED | G_005 | D96's own invariants are exact | 45 | 45 |
| UNCHANGED | G_005 | D96's lock release | 0.802314 | 0.802314 |
| BOUNDARY | G_002 | "97.6 % of the cube's directions are energy-free" | 0.976477 | 0.981825 |
| BOUNDARY | G_006 | the cube's contraction factor at m = 200 | 7.33 | **7.10** |
| UNCHANGED | G_006 | D96's contraction factor at m = 200 | 33.78 | 33.78 |

**7 UNCHANGED · 2 BOUNDARY · 6 REFUTED.**

**How badly were the refuted values pinned?** Measured in *their own stated tolerances*:

| claim | movement | in its own tolerance |
|---|---|---|
| G_003 ΔA = 0.276596 (tol 1e−6) | 0.013438 | **13 438×** |
| G_005 lock = 3.948614 (tol 1e−5) | 0.217077 | **21 708×** |
| G_005 L = 0.97648 (tol 1e−5) | 0.005345 | **535×** |
| G_002 A₀ = 20 812 (exact integer) | 4 732 | exact failure |
| G_002 max multiplicity = 562 (exact) | 176 | exact failure |

## Why nothing collapses — `ConclusionsSurvive() = true`

Every non-pinned row is UNCHANGED, and that is checked, not asserted:

- the cube is still **~98 % energy-free** (0.981825 > 0.97 under either keying — and 0.97648 > 0.97 before);
- **free room is still exactly N − A₀** and still equals the D_048 latent fraction L — an algebraic identity, true for either A₀;
- **D96's own invariants never moved** (A₀ = 45, lock 0.802314, free room 51) because that path was already tolerant;
- the ordering **arrangement > degeneracy > lattice-average > survivor compression** is intact;
- the regime is still classified **CONTROLLABLE** (G_002 row 6).

**The refuted items are numbers, not conclusions.** The G-series' qualitative content — that ρ is the source, that its controllable part is real but statistically inaccessible, that D96³ is the energy-free lattice, that the lattice choice is not what suppresses — is untouched.

## The blast radius (honest accounting)

The change is in **shared infrastructure**, so it reaches further than group G. Measured, not assumed:

| suite | outcome |
|---|---|
| **G_002, G_003, G_005, G_006** | 4 assertions updated to the corrected values (listed above) |
| **G_033** | its live scanner now excludes **two** meta-audits (G_033 and G_034), which compute the spectrum in order to audit it |
| **T_012** | **headline UNCHANGED** — D96³ still gives S∞ = **16** (distance 3 from 19) and random 17 is still nearest; only the sub-claim "the 3-axis sector gives 21 (distance 2)" moved to **14 (distance 5)**, so that sector is no longer the sub-count nearest 19 |
| **T_013** | assertions all pass; quoted figures updated: **A = 16 079** (was 20 811), **C = 1004.9** (was 1300.7), max multiplicity **738** (was 562), "~150×" → **~114×** |
| **T_014, T_008–T_011, D_047** | **all pass unchanged** — D_047 needed nothing, because it was already correct |

## Output

| label | content |
|---|---|
| **REFUTED** | six pinned G-series values were artifacts and fail at their own stated precision (up to **21 708×** their tolerance). The verdict is **computed** from the claim registry. |
| *(scope)* | **no G-series conclusion breaks** — 7 UNCHANGED, 2 BOUNDARY, and every inequality, identity and ordering survives. |

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_034_Tests.cs` — **6/6 PASSED**
**Group total:** G_001–G_034 = **271/271 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_034"`

| test | asserts |
|---|---|
| `Y_G_034_TheClusteredSpectrumMatchesTheIndependentLineage` | 5/5 D_047 figures; M_012's 16 080; free room 868 656 |
| `Y_G_034_TheLiveSharedHelperWasRepaired` | the **production** helper now yields 16 080 / 738 / 16 079 / lock 4.165691, and D96's own path was already tolerant |
| `Y_G_034_TheDerivedQuantitiesAndTheirMovement` | all six quantities move, with their relative changes |
| `Y_G_034_TheClassificationsAreComputedAndConclusionsSurvive` | 7/2/6 split; verdict REFUTED; only pinned values in the refuted set |
| `Y_G_034_UnchangedClaimsAreGenuinelyInvariant` | invariance verified on both keyings, not assumed |
| `Y_G_034_Run` | the full report |

**Opens:** OP1 re-run the **D-series** figures that were derived from A₀ on the corrected spectrum, in case any depend on it the way G_002/005 did; OP2 audit the remaining shared spectral counts for the same artifact class — **any** count keyed on exact `double` equality over algebraic values is suspect (this is now the second such defect found in one session); OP3 re-derive T_012's "ambiguity between 16, 21 and a random-like value" now that the cubic sector gives 14, and T_013's compression ranking on the corrected A; OP4 record the equality-discipline rule in the shared catalog so a future construction cannot reintroduce exact-double keying.
