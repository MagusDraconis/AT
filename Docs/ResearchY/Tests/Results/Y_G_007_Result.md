# Y_G_007_Result.md — ResearchY-G_007 Suppression Origin Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_007_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 9/9 PASSED (~1 s) — group G total 58/58 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_007"`

## Summary

**Question:** Is `DiffuseStep` derived or imported? Trace: Difference → Actualization → ρ evolution →
DiffuseStep. Plus: **does time have anything to do with the suppression?**
**Answer:** The **form is DERIVED** (it is the infinitesimal form of the canonical coarse-graining and is
*unique* up to one scalar rate), the **admissible range `0 ≤ d ≤ ½` is DERIVED** from `ρ ≥ 0`, the **values
`d = 0.2`, `m = 200` (hence 34) are BOUNDARY** (= the same flow at `T = m·d = 40`), the four alternatives are
**REFUTED**, and **time is NOT the cause**.

## Detail

| question | measurement | result |
|----------|-------------|--------|
| **1. derivable?** | trace: `Σρ = 1`; `ρ_{k+1} = μρ_k` (arrangement-neutral: `|Δa|` at `10⁶ρ` = 0.0); `Σ CoarseGrain(A) = 1`; `CoarseGrainedAlpha(α) = α` exactly; `DiffuseStep` = Euler step of the Laplacian flow (tridiagonal support 3, symmetric, rows sum 1, semigroup to 1e-15) | **DERIVED** |
| **2. unique?** | axioms (locality + constant coefficients + symmetry + rows summing to 1) ⇒ the general solution `W(b) = b·left + (1−2b)·a + b·right` with **one** free parameter; `W(0.2) ≡ DiffuseStep` to 1e-15. Dropping isotropy leaks at reflecting boundaries (`Σ − 1 = (l−r)(a_0 − a_{N−1})`), so conservation **forces** isotropy; the canonical chain has no antisymmetric coupling (NP_174) | **DERIVED (1-parameter)** |
| **3. alternatives** | NN average ≡ `d = ½` (2.01×); cutoff: delta → **96 cells** (non-local), `min ρ = −4.52e-2` (non-positive); biharmonic: **5-point** stencil, `min ρ = −1.46e-2`, min eigenvalue −0.5991 at κ = 0.1 (unstable beyond κ = 1/16); identity: factor 1.00 | **REFUTED** |
| **4. sensitivity** | factor at m = 200: 16.70 (d=0.05) · 25.12 (0.10) · **33.78 (0.20)** · 36.74 (0.25) · 39.43 (0.30) · 44.34 (0.40) · **2.01 (0.50)**; at fixed `T = m·d = 40`: 33.75 / 33.76 / 33.76 / **33.78** / 33.81 → **0.17 % spread over a 20× range of `d`** | **form DERIVED, rate BOUNDARY** |
| selectivity | `\|μ₉₅\|/\|μ₁\|` = 0.800 (0.05) · 0.600 (0.10) · **0.200 (0.20)** · **0.000268 (0.25)** · 0.200 (0.30) · 0.600 (0.40) · **1.000000 (0.50)** | `\|1 − 4d\|`-like; **zero at d = ½** |
| **witnesses** | witness/observed dichotomy: DiffuseStep **32.4 HOLDS** · NN average 1.81 **FAILS** · biharmonic 7.1 (weaker) · identity 1.0 **REFUTED**; every `d ∈ (0, ½)` gives > 8 | **SUPPRESSED verdict robust** |
| **time** | `ρ_(k+1) = μρ_k` is **diagonal** (support 1 vs 3 for the relaxation), normalized profile invariant for any μ and any duration (`\|Δa\|` at `2¹⁰⁰⁰ρ` = 0.0); the factor carries no μ (scale-free to 2.34e-13); density and metric static at criticality; reaching 3.746e5 by relaxation takes ≈ **729 steps** (a horizon) | **TIME REFUTED as the cause** |

## Verdicts

* **DERIVED** — the operator's *form* (Laplacian = infinitesimal coarse-graining, exact RG invariance);
  uniqueness up to one scalar; the admissible range `0 ≤ d ≤ ½`; the qualitative suppression verdict.
* **BOUNDARY** — the values `d = 0.2`, `m = 200`, hence the number 34 (`T = m·d = 40`); and the
  index identification inherited from G_005/G_006.
* **REFUTED** — "`DiffuseStep` is imported/arbitrary"; the spectral cutoff, biharmonic and identity as
  canonical operators; the nearest-neighbour average as an equivalent suppressor; **time as the cause**.

## Test results

| # | Test | Verdict |
|---|------|---------|
| 1 | `Y_G_007_TraceAndDerivation` | ✅ PASS |
| 2 | `Y_G_007_UniqueUpToRate` | ✅ PASS |
| 3 | `Y_G_007_PositivityDerivesTheRange` | ✅ PASS |
| 4 | `Y_G_007_AlternativeOperators` | ✅ PASS |
| 5 | `Y_G_007_SensitivityIsInTheRate` | ✅ PASS |
| 6 | `Y_G_007_WitnessesUnderAlternatives` | ✅ PASS |
| 7 | `Y_G_007_TimeIsNotTheCause` | ✅ PASS |
| 8 | `Y_G_007_Verdicts` | ✅ PASS |
| 9 | `Y_G_007_Run` | ✅ PASS |

## Classification

**No reclassification.** The D_040 registry is untouched; G_006's "EMERGENT 34" is sharpened to
"BOUNDARY at fixed `T = m·d`" (the law and the range are DERIVED); G_005's ACCESSIBLE / SUPPRESSED /
FORBIDDEN verdicts are unchanged. No canonical claim, value or equation changes; no new primitive.

## Consequence

`DiffuseStep` is **not** an imported smoothing: it is the first-order (Laplacian) relaxation that the
count-conserving, isotropic, scale-free, positivity-preserving structure of the counting measure forces,
with exactly one free scalar — and that scalar, not the operator, is where the boundary lies. Time plays no
role: the suppression is a **coarse-graining** effect along the level direction, and the only temporal
reading available requires an extra assumption.
