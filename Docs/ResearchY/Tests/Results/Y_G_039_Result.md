# Y_G_039 Result — Rho Realization Audit

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_039_Tests.cs`
**Status:** 7/7 PASSED
**Command:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_039"`
**Group total:** G_001–G_039 = **304/304 PASSED**

**ID note:** requested as G_036, which is already taken by the Temporal Core Test Audit delivered earlier this
session. The ID space is permanent and keyed by the index and the classification registry, so G_039 was used.

## Verdict

**BOUNDARY** — four of the five requirements are met, each by computation, and **one carrier** is identified:
the **occupancy of the reachable set**. The fifth — **measurability** — is the binding constraint.

## 1. The spectrum, recomputed (all four figures reproduce the record)

| quantity | recomputed | the record |
|---|---|---|
| distinct levels A₀ | **45** | 45 ✓ |
| multiplicity histogram | **{1:1, 2:42, 5:1, 6:1}** | same ✓ |
| free room Σ(m−1) | **51** = 96 − 45 | 51 ✓ |
| state dimension | **95** | 95 ✓ |
| Laplacian trace | **1152** = 2 × 576 links | 1152 ✓ |

## 2. Requirements 2–4 fall out of ONE observation

Energy and entropy are each **one number on a 95-dimensional space** → each is **lossy by 94**.

Energy kernel, splitting exactly (G_016b): **51 within-multiplet + 43 level mixing = 94** ✓

### The witness (unique m = 6 multiplet, λ = −2)

| quantity | value |
|---|---|
| energy change (20:1 tilt) | **0.000×10⁰** |
| ρ change (L1) | **1.2666666667** |
| clock shift | **0.9985774245** = **86 277.089 s/day** |

### The information witness

| state | entropy |
|---|---|
| A = (0.5000000000, 0.5000000000) | 0.6931471806 |
| B = (0.7729078048, 0.1135460976, 0.1135460976) | 0.6931471806 |

L1 separation **0.7729** — the entropy is a function of ρ but **not injective**.

### Phase, and the clock law

ρ counts **sites**, the phase lives on **links** → ρ change **0.0×10⁰**, holonomy change **0.065450**.
`(1/d)ln 20 = 0.9985774245` → **86 277.089 s/day**, G_016b's own figure.

## 3. The candidates are decided by loss

| candidate | zero-loss | discarded dims |
|---|---|---|
| occupancy distributions | **YES** | 0 |
| state populations | **NO** | **51** |
| degeneracy occupation | **NO** | **51** |
| attractor occupation | **YES** | 0 |
| survivor distributions | **YES** | 0 |

Averaging over a multiplet discards the **51-dimensional within-multiplet room** — the **free room**, and the only
part an actuator can move. The three zero-loss candidates are **one object read at three stages**.

## The one unmet requirement

**Measurability.** G_018 could conclude DERIVED for the **identity** of ρ, because for that question G_017
removed an **identification** rather than the **quantity**. A **realization** asks for an **observable**, and the
natural laboratory realization is exactly what **G_017 excluded**; what remains is the clock signature, priced by
G_004/G_009 as real but far below local sensitivity.

## Registry

`ClockOnly` → **SURVIVES** (ρ, the source law and the clock law are all g₀₀ statements), `ScanDetectsIt: false`.
Counts become **26 / 11 / 3 of 40**; boundary index unchanged.

## Caveats

- Substrate: the 96-cell circulant of G_016/G_018; every figure recomputed here.
- The two "lossy by 94" results share a form — a single number on a 95-dimensional space — so they are stated once.
- Deterministic: closed-form spectrum, bisection for the equal-entropy witness, invariant culture.
- **Three of the audit's own slips were caught by its own tests** and recorded: the link-count arithmetic
  (288 for 576), an equality at the 9th decimal sitting exactly on the rounding boundary, and a truncated `(int)`
  cast of a floating-point trace.
- **A fourth slip was caught by G_033's live scanner**: the constants were named `Cells`/`Radius`, so the D96
  scanner filed this audit with the D96-free (metric / closure) era even though it recomputes the D96 ring
  spectrum. Renamed to `D96Cells`; G_033 counts become **23 substrate / 16 D96-free / 1 comment-only**.
