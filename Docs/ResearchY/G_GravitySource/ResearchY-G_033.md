# ResearchY-G_033 — Cubic Substrate Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_033 (permanent)
**Title:** Cubic Substrate Audit — does the gravitation/time search need D96³ the way the rest of physics does?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_033.md`
**Depends on:** M_012 (3D Sector Origin), M_013 (Axis-Count Selection), G_002/G_005/G_006 (D96³ controls), G_032 (conformal assumption — the locus of η), NP_037/NP_088 (the cubic spectrum), DensityField / SpectralCaseCatalog (the shared substrate)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_033_Tests.cs` (5/5 PASSED, ~0.6 s)
**Core:** `AT.Core/ResearchXH/CubicSubstrateAudit.cs`

## The question

The programme established that **D96 ⊗ D96 ⊗ D96 ("D96³", the cubic tensor product) is required to calculate and explain physics.** The canonical statement is **M_012**:

> *"The 3-ring triad D96⊗D96⊗D96 (cubic lattice) has axis point group O_h … whose defining rep on ℝ³ is irreducible … **a genuine 3D sector exists, absent from the single ring (max irrep dim 2)**."*

Does the search for **gravitation and time (group G)** show the same behaviour?

## The answer: PARTIAL — same requirement, different locus

| layer | finding |
|---|---|
| **in code** | The requirement is **era-local**: the density era computes with D96/D96³; the whole **metric / closure era** computes with none of it. |
| **in structure** | The requirement is **inherited, not eliminated**: the spatial metric itself needs the dimension-3 irrep a single D96 ring cannot supply. |
| **the locus** | It was **absorbed into the primitive η** (G_032). 3D space enters the metric era as an input rather than as a generated structure. |

## 1. In code: the substrate is era-local

The core **re-reads the group-G sources at test time** (the G_027 pattern), so the inventory is live and a new D96-dependent audit is classified automatically. Comments are separated from code. The audit excludes **itself** (`Y_G_033` computes the spectrum in order to audit it — counting itself would inflate the substrate era, a self-reference the live scanner exposed immediately).

**34 suites classified:**

| class | count | members |
|---|---|---|
| references D96 **in code** | **20** | G_002–G_011b, G_012, G_013, G_014, G_016, G_016b, G_018, G_023, G_024, G_026 |
| **comment-only** | 1 | G_001 |
| **no D96 reference at all** | **13** | **G_015, G_017, G_019, G_020, G_021, G_022, G_025, G_027, G_028, G_029, G_030, G_031, G_032** |

> **Refinement (live scan, after G_039).** This table is G_033's own snapshot. The scanner is live, so later
> audits are classified on their own. As of **G_040** the counts are **39 classified suites** → **22 in code / 1
> comment-only / 16 with no reference** (G_034 and G_035 joined the meta-audits; G_036, G_037 and G_038 added
> three D96-free suites; **G_039 and G_040 — the ρ audits — recompute the D96 ring spectrum and so belong on the
> substrate side**, which the scanner only saw for G_039 after its `Cells` constant was renamed `D96Cells`).

The 13 are exactly the **metric / closure era**: G_017 (metric coupling), G_019–G_022 (redshift/spatial metric), G_025, G_027 (literal verdict), and the whole G_028–G_032 closure sequence (clock, spatial, no-go, spatial origin, conformal assumption). Those audits are pure continuum PPN calculations — a scalar ρ, an exponent, a conformal factor, γ.

**So gravity's *late* era does not touch the substrate at all.** That is the surface answer to the user's question — and it is *not* the whole answer.

## 2. In structure: gravity needs a dimension-3 irrep

Gravity's central object is the **spatial metric g_ij** — a symmetric rank-2 tensor in d = 3:

```
6 components = 1 (trace, l = 0) + 5 (traceless, l = 2)
```

Subducting the O(3) irreps onto the octahedral rotational group O (order 24, classes E·8C3·3C2·6C4·6C2′, irreps A1·A2·E·T1·T2 of dimension 1·1·2·3·3, Σd² = 24):

| l | dim | ‖χ‖² | decomposition | parity |
|---|---|---|---|---|
| 0 | 1 | 1 | A1 | g |
| 1 | 5→3 | 1 | **T1(3)** | u |
| 2 | 5 | 2 | **E(2) + T2(3)** | g |
| 3 | 7 | 3 | A2(1) + T1(3) + T2(3) | u |
| 4 | 9 | 4 | A1(1) + E(2) + T1(3) + T2(3) | g |

This reproduces **M_012's numbers exactly** (‖χ‖² = 1, 1, 2, 3, 4; l = 2 → 2+3; l = 1 → 3).

**Therefore:**

- the **vector / p-wave sector** needs **T1u(3)** — M_012's finding;
- the **spatial metric's traceless part** needs **T2g(3)** — one multipole higher, same argument;
- a **single D96 ring** is a cycle graph C₉₆ whose symmetry group D₉₆ (dihedral) has irreps of **dimension 1 and 2 only** → it supplies **neither**;
- the **cubic D96³** lattice (O_h) supplies **both**.

**Gravity requires the cubic substrate exactly as much as the vector sector does.** And the dimension is 3 for the same derived reason M_013 gives: rotation self-duality `d(d−1)/2 = d` has the **unique** solution **d = 3** (verified: 1→0, 2→1, **3→3**, 4→6, …).

## 3. Why the metric era is D96-free: the requirement lives in η

The metric era does not instantiate a 96³ spectrum because it **imports 3D space** through the primitive **η** — the conformal reference metric that **G_032** proved is an assumed input, quoting QG289: *"a TRUE THEORY INPUT … part of the geometry, not a choice."*

> **G_032 and G_033 are one fact seen twice.** G_032 says the shape of space is an input. G_033 says that input is precisely the structure the cubic lattice would otherwise have had to supply.

So the D96³ requirement did not disappear from the gravity programme. It was **discharged by a primitive** instead of by a lattice — which is why the late-era audits look substrate-free while still depending, structurally, on a genuine 3D sector.

## 4. A defect found while checking: A₀ = 20 812 is not an invariant

Group G cites **A₀ = 20 812 eigenspaces** for D96³ (G_002, G_005, G_006, and downstream in `DensityField.CubeSpaces`, `SpectralCaseCatalog`, AT.App, AT.Book, `AT_LabBook.md`). `DensityField.cs` documents it as *"D96⊗D96⊗D96 tensor-cube eigenspaces (884 736 modes, 20 812 eigenspaces)."*

**It is an artifact of binary64 dictionary keying.** The construction enumerates the 49³ reduced-index triples and counts **distinct `double` sums**, so the answer depends on the last bits of `Math.Cos`:

| method | value |
|---|---|
| exact-double keying, .NET (`Math.Cos`) — the cited figure | **20 812** |
| exact-double keying, Python (`math.cos`), same algorithm | **20 440** |
| **tolerance-clustered (robust)** | **16 080** |
| exact keying under one-ulp (1e−16) perturbation, 6 trials | **20 488 … 21 369** (range **881**) |
| tolerant count under the same perturbation | spread **0** |
| tolerant count under 1e−13 perturbation | spread **0** |

Decimal-place profile of the tolerant count — a genuine plateau at 7–10 places, then the round-off floor takes over:

| dp | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 |
|---|---|---|---|---|---|---|---|---|---|
| n | 16 072 | **16 080** | **16 080** | **16 080** | **16 080** | 16 085 | 16 105 | 16 319 | 18 282 |

**The robust value is 16 080** — which is **M_012's own independently reported figure** ("16,080 distinct levels over 96³ states"), and the value the repository uses **elsewhere**: `NewChat_Start.md` records *"D96³ 136 005 of **868 656**"*, and 868 656 = 884 736 − **16 080**.

**The two lineages never met.** The repository carries both numbers for the same quantity:

| source | A₀ | free room N − A₀ | latent fraction L |
|---|---|---|---|
| group G / `DensityField` / `SpectralCaseCatalog` (code) | 20 812 | 863 924 | 0.976477 |
| M_012 doc + `NewChat_Start` (868 656) | **16 080** | **868 656** | **0.981825** |

**What survives and what does not.** The **qualitative** conclusion — that D96³ is ≈ 98 % energy-free, that its landscape is ~150× larger, that compression is dominated by A — **is unaffected** (0.976 vs 0.982). The **specific** figures (20 812, 863 924, L = 0.97648, and the lock-release value 3.948614 derived from them) do not survive as invariants, and any downstream number computed **from A₀** inherits an implementation-dependent input.

**Not fixed here, deliberately.** The keying lives in shared infrastructure (`AT.Tests/Shared/SpectralCaseCatalog.cs`, `DensityField.cs`) consumed by other research groups; changing it would renumber A₀ for D96³ programme-wide and is a programme-level decision, not a side effect of this audit. It is registered as an open item instead.

## Output

| label | content |
|---|---|
| **PARTIAL** | Gravity needs the dimension-3 sector (**SAME** requirement), but the use is era-local and the requirement is discharged by η rather than exercised — so the behaviour is the same in structure and different in locus. The verdict is **computed** from the live scan plus the subduction, never typed. |

## How this reads with the rest of group G

| audit | finding | G_033's relation |
|---|---|---|
| **M_012** | a genuine 3D sector requires D96³; a single ring tops out at dim 2 | **extended**: the spatial metric needs the same dimension-3 irrep (T2g), so gravity inherits the requirement |
| **M_013** | exactly 3 axes, by rotation self-duality | **reused**: d = 3 is the same fixed point here |
| **G_017–G_022, G_025, G_027–G_032** | metric, redshift, spatial closure, no-go, origin, conformal assumption | **explained**: all 13 are substrate-free because 3D space arrives via η |
| **G_032** | `g = Ω²η` is assumed; the imported object is η | **completed**: η is exactly where the D96³ requirement went |
| **G_002/G_005/G_006** | D96³ used as the control lattice, A₀ = 20 812 | **corrected**: the qualitative control stands; the A₀ figures are float artifacts |

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_033_Tests.cs` — **5/5 PASSED**
**Group total:** G_001–G_033 = **265/265 PASSED**
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_033"`

| test | asserts |
|---|---|
| `Y_G_033_TheSubstrateRequirementIsEraLocal` | live scan: 34 classified, 20 in code, 1 comment-only, **13 with no reference**; both classes populated |
| `Y_G_033_GravityRequiresADimension3Irrep` | 6 = 1 + 5; ‖χ‖² = 1,1,2,3,4; l = 2 → Eg(2)+T2g(3); max irrep dim 3; single ring 2; d = 3 unique |
| `Y_G_033_TheCitedA0IsAFloatingPointArtifact` | 20 812 reproduced; range 881 under one ulp; tolerant 16 080 with spread **0**; plateau; the two recorded A₀ values disagree |
| `Y_G_033_VerdictIsPartialAndTheRequirementLivesInEta` | computed verdict PARTIAL; the locus is η (G_032) |
| `Y_G_033_Run` | the full report |

**SUPERSEDED IN PART by ResearchY-G_034** (A₀ Robustness Audit): the replacement was **applied** — the
shared helper now clusters at a tolerance and reproduces D_047 on five independent figures. Six G-series
pinned values were REFUTED as artifacts (up to 21 708× their own tolerances); no qualitative conclusion
broke. Also note: D_047 already carried the robust values, so this was never "two lineages that never
met" but two different equality rules in one programme.

**Opens:** OP1 decide the shared-substrate question — should `SpectralCaseCatalog.TensorProductSpectrum96` cluster at a tolerance (A₀ = 16 080) and re-run every dependent group, or should A₀ be redefined as a robust invariant with a stated tolerance?; OP2 reconstruct the lock-release 3.948614 and the L-based figures from the robust A₀ and see which conclusions move; OP3 audit the remaining shared spectral counts for the same artifact class — any count keyed on exact `double` equality over algebraic values is suspect.
