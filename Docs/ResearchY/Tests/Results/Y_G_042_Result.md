# Y_G_042 — Three-Dimensionality Dependency Audit — Result

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_042_Tests.cs` — **7/7 PASSED**
**Core:** `AT.Core/ResearchXH/ThreeDimensionalityDependencyAudit.cs`
**Doc:** `Docs/ResearchY/G_GravitySource/ResearchY-G_042.md`

## Question

Are the d = 3 selectors **independent**, or the same structure viewed differently — one root mechanism or
several?

**Inputs:** 1 rotation self-duality `d(d−1)/2 = d` · 2 Hodge duality `dim(Λ²) = dim V` · 3 polarisation equality
`d−1 = (d+1)(d−2)/2` · 4 the D96^d representation structure · 5 the clock exponent `ρ^(1/d)`.

## Answer: **REDUNDANT — ONE ROOT MECHANISM. Independent selectors: 1.**

## The decisive identity

**graviton polarisations = dim(Λ²) − 1 = dim(so(d)) − 1 for every d**, against photons = dim V − 1. Subtract one
from both sides: *"the polarisations are equal"* **IS** *"dim(Λ²) = dim V"* — identically, not merely at the
shared root. So input 3 is not a coincidence that shares the root; it is the **same equation** reached by a
different derivation (little-group counting instead of tensor algebra).

All three geometric inputs reduce to **`d(d−3) = 0`**, roots **{0, 3}** — physically: *the number of directions
equals the number of independent rotations.* Inputs 1 and 2 are literally the same formula.

## The dependency graph (computed; A ⟹ B iff S(A) ⊆ S(B))

| selector | solution set over d = 1..12 |
|---|---|
| 1 rotation self-duality | **{3}** |
| 2 Hodge duality | **{3}** |
| 3 polarisation equality | **{3}** |
| 4a supplies a 3-dim irrep | {3 … 12} |
| 4b vector is the largest irrep | {1, 2, 3} |
| 5 clock law ρ^(1/d) | {1 … 12} |

**4a ∧ 4b = {3}** — the representation conjunction *reproduces the root*, which is why it looked like a second
mechanism; but the root implies both halves, so it is redundant too. Nothing implies selector 1.

## Classification

| status | selector |
|---|---|
| **INDEPENDENT** | 1 rotation self-duality — **the root** |
| **REDUNDANT** | 2 Hodge duality (same set as 1) |
| **REDUNDANT** | 3 polarisation equality (same set *and* same equation as 1) |
| **DERIVED FROM** | 4a, 4b (each strictly weaker; implied by the root) |
| **REDUNDANT** | 5 clock law (holds at every d — a law, not a selector) |

**Independent root mechanisms: 1.**

## The accidents belong to different dimensions (checked, not assumed)

bivectors collapse to a **scalar** at d = 2 · **are** the vectors at d = 3 · split **self-dual** at d = 4
(even Λ² also at d = 5, 8, 9, 12) · the cross product returns at **d = 7** (Hurwitz — cited, not computed). The
family is real and its memberships differ, but only the d = 3 membership selects d = 3.

## Refinement owed to G_041

G_041 called the ε/Hodge accident and the polarisation match **two independent accidents**. They are **one
condition with two derivations**, so the number of independent reasons for d = 3 is **1, not 2**. G_041's
positive results and its BOUNDARY verdict stand; only the count is corrected. Recorded in the G_041 doc, in
`SubstrateDimensionAudit.RefinementFromG042()` (printed in G_041's own report), in the G_041 registry narrative,
and in `NewChat_Start.md`.

## Registry

`ClockOnly` → **SURVIVES**, `ScanDetectsIt: false`. Counts become **29 / 11 / 3 of 43**; boundary index
unchanged; no prior classification changed. G_033's live classifier now reads **24** substrate suites (41
classified in total).

## Caveats

* Inputs 1 and 2 are the same formula, not merely equivalent selectors.
* "One mechanism" is a statement about **logical independence**, not about provenance — the two derivations of
  input 3 are physically different arguments.
* Solution sets computed over d = 1..12; the irrep spectra are exact at every d (the *group* construction is
  what limits G_041's ladder to 6).
* The d = 7 cross product is marked as a **citation** (Hurwitz), not a computation.
