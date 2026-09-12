# ResearchY-G_007 — Suppression Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_007 (permanent)
**Title:** Suppression Origin Audit — is `DiffuseStep` derived or imported?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_007.md`
**Depends on:** ResearchY-G_002 (the free room), G_003/G_004 (witnesses vs observed fields),
G_005 (the 34× contraction), G_006 (the mechanism = the relaxation operator);
AT-QG QG1 (ρ_{k+1} = μρ_k), QG194 (count conservation), **QG222 / QG228** (the deficit increments and their
coarse-graining); D_047; NP_174 (reciprocity — no antisymmetric coupling in the canonical chain);
`AT.Core/ResearchXH/RhoDynamics.cs` (`DiffuseStep`, `CoarseGrain`, `CoarseGrainedAlpha`, `Increments`)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_007_Tests.cs` (9/9 PASSED, ~1 s)

## Purpose

G_006 named the suppressing term: the relaxation operator `DiffuseStep`. G_007 asks whether that operator is
**derived** from the canonical AT primitives or merely **imported**, tracing

```
Difference  →  Actualization  →  ρ evolution  →  DiffuseStep
```

and testing four questions — (1) derivability, (2) uniqueness, (3) alternative operators,
(4) sensitivity — by replacing `DiffuseStep` with a **nearest-neighbour average**, a **spectral cutoff**, a
**higher-order (biharmonic) diffusion** and the **identity**, measuring the suppression factor, the G_006
"34×" and the G_003/G_005 witnesses.

It also answers the further question: **does TIME have anything to do with this suppression?**

**Answer.** The operator's **form is DERIVED** (it is the infinitesimal form of the canonical
coarse-graining, and it is *unique* up to a single scalar rate among local, linear, isotropic,
count-conserving, scale-free maps), its **admissible range `0 ≤ d ≤ ½` is DERIVED** from `ρ ≥ 0`, and the
**values** `d = 0.2`, `m = 200` — hence the number 34 — are **BOUNDARY** (they are the same flow at fixed
`T = m·d = 40`, within 0.17 %). The four alternatives are **REFUTED** as canonical; and **time is REFUTED
as the cause**: the branching flow is diagonal on the arrangement and suppresses nothing at any μ.

## 0. The trace — every link is canonical

| link | canonical content | verified |
|------|-------------------|----------|
| **Difference** | the counting measure ρ, `Σρ = 1` exactly (QG194) | `Σρ = 1.000000000000`, `CountConserved = True` |
| **Actualization** | `ρ_{k+1} = μρ_k` (QG1), count-conserving and **arrangement-neutral** | `BranchingContinuity = True`, `|Δa|` at `10⁶ρ` = **0.00e+00** |
| **ρ evolution** | per-octave deficit increments `A_k`, and coarse-graining (merge adjacent octaves) with **exact** RG invariance (`α` is a fixed point) | `Σ CoarseGrain(A) = 1.000000000000`; `CoarseGrainedAlpha(α) = α` to 1e-12 for α = 0, 0.5, 1, 1.5, 2.5 |
| **DiffuseStep** | the **Euler step of the Laplacian flow** on that chain: `W = I − d·L`, tridiagonal, symmetric, rows summing to 1, with the **semigroup property** | verified to 1e-15 |

So `DiffuseStep` is not an import: it is the *infinitesimal* form of the canonical coarse-graining
operation, whose scale-freeness (α is an exact RG fixed point) is already a canonical statement.

## 1. Uniqueness — one free scalar, and it is the rate

Impose the axioms **nearest-neighbour support** (locality) + **constant coefficients** (no preferred
octave) + **symmetry** (isotropy) + **rows summing to 1** (count conservation). The general solution is

```
W(b)·a |_i = b·a_{i−1} + (1 − 2b)·a_i + b·a_{i+1}          (Neumann ghosts)
```

with **exactly one free parameter** `b`; the canonical `DiffuseStep` is the member `b = d = 0.2`
(verified: `W(0.2) ≡ DiffuseStep` to 1e-15).

**Isotropy is not an extra assumption — conservation forces it here.** With reflecting boundaries an
*anisotropic* weight leaks: `Σ(W·a) − 1 = (l − r)(a_0 − a_{N−1})` (measured: 0.3/0.1 weights give
`Σ = 0.99208`-class non-conservation, and the mirrored 0.1/0.3 weights leak with the opposite sign). The
canonical chain supplies no antisymmetric coupling anyway (NP_174 reciprocity), so the directed branch is
unavailable. **Derived: the family is one-dimensional.**

## 2. The admissible range is derived by ρ ≥ 0

`b_i = d·a_{i−1} + (1 − 2d)·a_i + d·a_{i+1}` is a **convex combination** iff `0 ≤ d ≤ ½`.

| probe | result |
|-------|--------|
| `min ρ` after one step on a spike (d = 0.2) | +1.04e-2 ≥ 0 |
| d = ½ (the marginal convex case) | +1.04e-2 ≥ 0, and `max\|μ_k\| = 1.000000` exactly |
| d = 0.6 | **−8.96e-2 < 0** — outside the physical range |

At `d = ½` the fastest available mode reaches `μ₉₅ = −0.999465`: it **oscillates instead of decaying**.
The same bound gives stability (`|μ_k| ≤ 1`).

**The mechanism needs selectivity** — a spread between the slowest and fastest mode. Measured
`|μ₉₅|/|μ₁|`:

| d | 0.05 | 0.10 | **0.20** | **0.25** | 0.30 | 0.40 | **0.50** |
|---|------|------|-----|-----|------|------|------|
| selectivity | 0.800 | 0.600 | **0.200** | **0.000268** | 0.200 | 0.600 | **1.000000** |

Selectivity is `|1 − 4d|`-like: **maximal at d = ¼, zero at d = ½** — where the filter is *flat* and the
mechanism disappears.

## 3. Sensitivity lives in the rate, not in the form

| d | 0.05 | 0.10 | **0.20** | 0.25 | 0.30 | 0.40 | 0.50 |
|---|------|------|-----|------|------|------|------|
| factor at m = 200 | 16.70 | 25.12 | **33.78** | 36.74 | 39.43 | 44.34 | **2.01** |

The factor grows with `d` and then **collapses at `d = ½`**. But at fixed `T = m·d = 40`:

| d / m | 0.02 / 2000 | 0.05 / 800 | 0.10 / 400 | 0.20 / 200 | 0.40 / 100 |
|-------|-------------|------------|------------|------------|------------|
| factor | 33.75 | 33.76 | 33.76 | **33.78** | 33.81 |

a **0.17 % spread over a 20× range of d**. So `T = m·d` — not `d`, and not `m` separately — is the
physical control parameter of the flow, and the pair `(d = 0.2, m = 200)` is a **BOUNDARY** representative
of `T = 40`. The G_006 number is exactly `exp(3.5198 nats)` = the T = 40 value. **The form is DERIVED; the
rate is BOUNDARY.**

## 4. The four replacements

| replacement | local | ρ ≥ 0 | conservative | factor m = 200 | verdict |
|-------------|-------|--------|--------------|----------------|---------|
| **DiffuseStep (d = 0.2)** | yes | yes | yes | **33.78** | **CANONICAL** |
| nearest-neighbour average | yes | yes | yes | 2.01 | it *is* `d = ½` — flat filter |
| spectral cutoff (k_c = 48) | **no** | **no** | yes | 2.22 | REFUTED |
| biharmonic (κ = 0.05) | **no** | **no** | yes | 7.15 | REFUTED |
| identity | yes | yes | yes | 1.00 | REFUTED |

* **Nearest-neighbour average.** Not a different operator at all: `(left + right)/2 ≡ DiffuseStep(d = ½)`
  (verified to 1e-15). At `d = ½` the extreme arrangements keep ~90 % of their amplitude after 200 steps
  (`|μ₉₅|²⁰⁰ = 0.898`), so it **fails to suppress the witness class**.
* **Spectral cutoff.** A delta spreads to **all 96 cells in one step** (DiffuseStep: 3) — action at a
  distance, so it is not local; and the projection makes ρ **negative** on a spike (**−4.52e-2**), which is
  a physical contradiction. Its "factor" is an on/off artefact, not a graded decay.
* **Biharmonic (higher-order) diffusion.** A **5-point** (next-nearest) stencil, not a convex combination
  (negative ±2 weights), so positivity is not protected: `min ρ = −1.46e-2` at κ = 0.05, and the step is
  **unstable** beyond the edge `κ = 1/16` (at κ = 0.1 the fastest eigenvalue is −0.5991). As a suppressor
  it is *weaker* (7.15 vs 33.78).
* **Identity.** No suppression at all: the G_003 witnesses would survive at full amplitude, contradicting
  G_004's requirement of ≥ 3.746e5.

## 5. Do the G_005/G_006 witnesses survive the substitution?

The two objects that matter are the **witness** (within-multiplet, high-k) and the **observed smooth field**
(the k = 1 mode / G4-ME21's one void per octave):

| operator | witness | observed | dichotomy | verdict |
|----------|---------|----------|-----------|---------|
| DiffuseStep (d = 0.2) | 33.78 | 1.044 | **32.4** | **HOLDS** |
| nearest-neighbour (d = ½) | 2.01 | 1.113 | 1.81 | FAILS |
| biharmonic (κ = 0.05) | 7.15 | 1.000 | 7.1 | holds, weaker |
| identity | 1.00 | 1.000 | 1.0 | REFUTED |
| spectral cutoff (k_c = 48) | high-k removed in one step | survives | — | REFUTED (ρ < 0) |

**The SUPPRESSED verdict is robust across the whole admissible family**: the dichotomy ratio exceeds 8 for
every `d ∈ (0, ½)` tested (24.6 at d = 0.1, 32.4 at 0.2, 34.8 at 0.25, ~39 at 0.35). It is destroyed by the
identity and by the marginal `d = ½`. Magnitudes, however, vary by > 20× depending on the member.

## 6. Does TIME have anything to do with the suppression?

**No.** Four structural reasons:

1. **The time-like flow is diagonal on the arrangement.** `ρ_(k+1) = μρ_k` multiplies *every* cell by the
   same scalar: one generation keeps a unit on its own cell (support 1), and the normalised profile is
   exactly invariant for **any μ over any number of generations** (`|Δa|` at `2¹⁰⁰⁰ρ` = **0.00e+00**).
   Time alone suppresses nothing.
2. **The suppressing operator is tridiagonal in the occupancy index** — it mixes *adjacent levels*
   (a unit at cell 40 spreads to 3 cells), i.e. it is a **coarse-graining (scale) operator**, not an
   evolution in time.
3. **The factor contains no rate, no μ and no time**: it is a function of `m` (steps) and `d` only, and it
   is scale-free (pre-scaling ρ by 7 changes it by 2.34e-13).
4. **At criticality (μ = 1) the density and the metric are static** — time does nothing at all.

`m` is therefore a **coarse-graining horizon** ("how far one has zoomed out"), not a duration. Reaching the
G_003 suppression 3.746e5 by relaxation alone takes about **729 steps** — a horizon, not a time. The only
temporal readings are extra assumptions: (a) identify one relaxation step with one generation — then the
witnesses fade over ~200 generations, but the branching rate μ *still cancels* from the scalar factor; or
(b) read `m` through the entropy production, which G_006 showed to be *downstream* of the energy decay
rather than a clock for it.

## 7. Verdict

| claim | verdict |
|-------|---------|
| `DiffuseStep`'s **form** is derivable from canonical primitives (Laplacian = infinitesimal coarse-graining; exact RG invariance) | **DERIVED** |
| **Uniqueness**: local + linear + isotropic + conservative + scale-free ⇒ a **one-parameter** family | **DERIVED** |
| The **admissible range** `0 ≤ d ≤ ½` (from `ρ ≥ 0` and `|μ| ≤ 1`) | **DERIVED** |
| The **qualitative** suppression verdict (the witness/observed dichotomy) | **DERIVED** (all `0 < d < ½`) |
| The **values** `d = 0.2`, `m = 200`, and hence the number 34 (`T = m·d = 40`) | **BOUNDARY** |
| "`DiffuseStep` is imported / arbitrary" | **REFUTED** |
| Spectral cutoff, biharmonic, identity as canonical operators | **REFUTED** |
| The nearest-neighbour average as an equivalent suppressor | **REFUTED** (`d = ½`, flat filter) |
| **TIME** as the cause of the suppression | **REFUTED** |

## 8. Classification and caveats

**No reclassification.** The D_040 `ClassificationRegistry` is untouched; G_005's and G_006's verdicts
stand and are here *qualified*: G_006 called the number 34 "EMERGENT"; G_007 shows the sharper statement —
the **law** is DERIVED, the admissible range is DERIVED, and the number is **BOUNDARY at fixed `T = m·d`**.
No canonical claim, value or equation changes; no new primitive.

* The axioms used for uniqueness (locality, symmetry, conservation, scale-freeness) are all *canonical*
  statements (QG194 for conservation; the α fixed point for scale-freeness; NP_174 for reciprocity). The
  one non-canonical ingredient is the *identification of the relaxation with the octave/occupancy index*,
  inherited from G_005/G_006 (declared there as assumption A1).
* The positivity argument fixes the range but **not** the value: any `d ∈ (0, ½)` reproduces the mechanism.

## 9. Open problems (OP1–OP5)

1. What fixes **`d = 0.2`** (or equivalently `T = 40`)? Nothing in the canonical chain does so here.
2. What fixes the **number of coarse-graining steps** (`m ≈ 200`)? Only the G_005/G_006 horizon convention.
3. Is the **index identification** (occupancy index ↔ octave chain) exact, or is it itself BOUNDARY?
4. Do **biharmonic-like deformations** occur anywhere in the canonical chain? (None found; the canonical
   relaxation is first-order.)
5. Could the observed **0.17 %T-invariance** be tightened to an exact statement — i.e. is the factor a
   *function of `T` alone* in the continuum limit, with the `d`-dependence purely a discretisation artefact?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_007_Tests.cs` — **9/9 PASSED** (~1 s)
**Group total:** G_001–G_007 = **58/58 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_007"`

| verdict | content |
|---------|---------|
| DERIVED | the operator's form (Laplacian = infinitesimal coarse-graining, exact RG invariance); uniqueness up to one scalar (local + linear + isotropic + conservative + scale-free); the admissible range `0 ≤ d ≤ ½` from `ρ ≥ 0`; the qualitative suppression verdict for every `0 < d < ½` |
| BOUNDARY | `d = 0.2`, `m = 200`, hence 34 = `exp(3.5198)` at `T = m·d = 40` (0.17 % spread across a 20× range of `d` at fixed `T`); the index identification inherited from G_005/G_006 |
| REFUTED | "`DiffuseStep` is imported/arbitrary"; the spectral cutoff (non-local, non-positive), the biharmonic (next-nearest, non-positive, unstable beyond κ = 1/16), the identity (no suppression), the nearest-neighbour average (`d = ½`, flat filter); and **time as the cause** |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_005.md`, `ResearchY-G_006.md`
* `Docs/ResearchY/Tests/Results/Y_G_007_Result.md`
* `AT.Core/ResearchXH/RhoDynamics.cs` (`DiffuseStep`, `CoarseGrain`, `CoarseGrainedAlpha`, `Increments`),
  `NativeMetricDynamics.cs`
* ResearchY-NP_174 (reciprocity — no antisymmetric coupling)
