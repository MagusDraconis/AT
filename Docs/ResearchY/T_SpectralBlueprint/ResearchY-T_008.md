# ResearchY-T_008 — Asymptotic Diversity Limit Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_008 (permanent)
**Title:** Asymptotic Diversity Limit — what determines the saturated species count S∞?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `T_SpectralBlueprint/ResearchY-T_008.md`
**Depends on:** ResearchY-T_007 (bounded innovation), T_005 (attractor structure), T_006
(Darwinian dominance emergence)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_008_Tests.cs` (8/8 ✅)
**Core analyzer:** `AT.Core/ResearchT/BoundedInnovationAnalyzer.cs` (from T_007)

---

## Question

T_007 established that diversity saturates to a finite species count S∞ under replicator +
mutation + extinction + resource constraint. T_008 asks: **what determines that limit?**
Sweep the governing parameters — mutation rate μ, crowding β, landscape size A, fitness
variance, spectral rigidity — and derive the scaling law S∞ = F(μ, β, A, …).

## Method

Deterministic parameter sweeps of the T_007 replicator–mutator over the non-zero Laplacian
eigenspaces (fitness `w = m/λ`, crowding `f = w/(1+βx)`, ring mutation μ→k±1, extinction
threshold ε = 1e-6). S∞ = number of species above threshold at saturation.

---

## Results

### 1. Mutation rate μ (D96, β = 1)

| μ | 0.0001 | 0.001 | 0.01 | 0.1 | 0.5 |
|---|---|---|---|---|---|
| S∞ | 3 | 3 | 5 | 6 | 9 |

**∂S∞/∂μ ≥ 0.** At μ = 0 exactly, S∞ = 1 (pure selection → single fittest). Any μ > 0,
however tiny, re-seeds a finite mutation–selection balance S₀⁺ > 1.

### 2. Crowding β (D96, μ = 0.01)

| β | 0 | 0.1 | 0.5 | 1.0 | 2.0 | 10.0 |
|---|---|---|---|---|---|---|
| S∞ | 4 | 4 | 4 | 5 | 5 | 7 |

**∂S∞/∂β ≥ 0.** Crowding (self-limitation on the fittest) *levels* the fitness field, so more
modes coexist above threshold. (This is distinct from the T_007 *open-landscape* carrying
capacity, which caps the total — two different "resource" mechanisms.)

### 3. Landscape size A (circulant C_N(1..6), μ = 0.01, β = 1)

| N | 48 | 96 | 192 | 384 |
|---|---|---|---|---|
| A | 20 | 44 | 92 | 188 |
| S∞ | 5 | 5 | 4 | 4 |

**∂S∞/∂A ≈ 0.** A grows ~10× (20 → 188) while S∞ saturates at ≈ 4–5 (and even slightly
decreases). S∞ does **not** track the landscape size.

### 4. Fitness variance vs S∞ (all cases)

| model | A | Var(log w) | S∞ |
|---|---|---|---|
| D96-3D | 12 | 1.311 | 9 |
| unphysical | 3 | 1.083 | 3 |
| physical | 48 | 0.806 | 5 |
| D96 | 44 | 0.454 | 5 |
| random | 95 | 0.049 | 17 |

Fitness variance is **informative but not a complete determinant**: random (lowest variance)
has the highest S∞ (17), but D96-3D has *higher* variance than unphysical yet *more* survivors
(9 vs 3) because the A ceiling (12 vs 3) and the full distribution shape matter.

### 5. Spectral rigidity vs S∞ (all cases)

| model | A | rigidity (degeneracy) | S∞ |
|---|---|---|---|
| unphysical | 3 | 0.969 | 3 |
| D96-3D | 12 | 0.875 | 9 |
| D96 | 44 | 0.542 | 5 |
| physical | 48 | 0.500 | 5 |
| random | 95 | 0.010 | 17 |

Rigidity is **not a monotonic determinant**: D96-3D is *more* rigid than D96 yet has *more*
survivors (9 vs 5). Rigidity acts only through the fitness distribution (degeneracy m enters
`w = m/λ`).

---

## Scaling law

> **S∞ = min(A, N_fit(μ, β, {w}))**
>
> where `A` is the hard landscape ceiling and `N_fit` is the number of modes within the
> mutation–selection reach of the fittest (one mode at μ = 0, a finite set S₀⁺ ≥ 1 for any
> μ > 0, widened by μ and leveled by β).

The **DERIVED** consequences (each verified numerically):

| Law | Statement |
|---|---|
| Structural bound | 1 ≤ S∞ ≤ A |
| Mutation monotonicity | ∂S∞/∂μ ≥ 0 |
| Crowding monotonicity | ∂S∞/∂β ≥ 0 |
| Pure-selection limit | μ = 0 ⇒ S∞ = 1 |
| Landscape saturation | ∂S∞/∂A ≈ 0 (S∞ does not track A) |

---

## Classification

| Item | Classification |
|---|---|
| Structural bound 1 ≤ S∞ ≤ A | DERIVED |
| ∂S∞/∂μ ≥ 0, ∂S∞/∂β ≥ 0, μ=0 ⇒ S∞=1 | DERIVED |
| ∂S∞/∂A ≈ 0 (saturation, not tracking A) | DERIVED |
| Exact functional form F(μ, β, {w}) and coefficients | EMERGENT |
| "S∞ is set by A alone" | REFUTED (A 20→188, S∞ ≈ 4–5) |
| "Rigidity alone determines S∞" | REFUTED (D96-3D more rigid, more survivors) |
| "Variance alone determines S∞" | REFUTED (D96-3D vs unphysical) |

---

## Conclusion

The asymptotic diversity limit is set by a **mutation–selection balance** bounded by the
landscape: S∞ = min(A, N_fit(μ, β, {w})). Mutation widens the surviving cloud (∂S∞/∂μ ≥ 0),
crowding levels the field (∂S∞/∂β ≥ 0), the landscape size is a ceiling that S∞ does not
track (∂S∞/∂A ≈ 0), and — contrary to naive expectation — neither fitness variance nor
spectral rigidity alone predicts S∞. These dependencies are DERIVED at the level of
monotonicity and bounds; the exact functional form F(μ, β, {w}) is EMERGENT. No new
primitive; canonical AT unchanged.
