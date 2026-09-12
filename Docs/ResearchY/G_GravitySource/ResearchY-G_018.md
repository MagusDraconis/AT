# ResearchY-G_018 — Rho Identity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_018 (permanent)
**Title:** Rho Identity Audit — which physical quantity remains as the identity of ρ after the laboratory |ψ|² reading is excluded?
**Status:** COMPLETE
**Date:** 2026-09-12
**File:** `G_GravitySource/ResearchY-G_018.md`
**Depends on:** ResearchY-G_017 (the laboratory |ψ|² identification is experimentally excluded), G_016 (ρ is more
primitive than mass-energy; `E = ⟨λ,ρ⟩` is a rank-1 pairing with a 94-dimensional kernel), G_016b (ρ is free of
E; the degeneracy and λ-mixing rooms), G_009 (the clock law and its scale invariance), G_007 (the lattice form
and its BOUNDARY values), G_014 (the κ = 1 carrier); AT-QG QG194 (`Σρ = 1`), QG197, QG220
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_018_Tests.cs` (7/7 PASSED, ~0.07 s)

## Purpose

G_017 excluded the identification of a **laboratory** |ψ|² with ρ. That raises the identity question directly:

> **What physical quantity remains as the identity of ρ?**
> Candidates: occupancy measure, actualization count, state accessibility, degeneracy occupancy, survivor
> distribution.     Output: **DERIVED / BOUNDARY / REFUTED**.

**Answer.** What remains is the **occupancy measure**: a dimensionless, normalised **counting measure over
distinguishable states**, whose entire physical content is in the **arrangement** and none of it in the amount.
The count is a label (REFUTED); accessibility belongs to the lattice (BOUNDARY); the degeneracy occupancy is the
surviving *content* under a BOUNDARY dimension; the survivor distribution is a lossy readout (REFUTED).

## The criterion — and the provenance ledger

A quantity Q is the *identity* of ρ iff

* **(I1)** it is defined by **counting over distinguishable states** — i.e. it needs only Difference →
  distinguishability, the first primitive;
* **(I2)** it is **dimensionless** and normalised (`Σ = 1`), or a ratio of such;
* **(I3)** it **survives G_017** — its definition never referenced a laboratory quantity;
* **(I4)** it **determines ρ** — it is not a lossy functional of ρ (G_016/G_016b: functionals are readouts).

**Provenance ledger: this audit imports no constant at all.** No SI quantity, no measured number, no
laboratory system. Every assertion is a dimension count, an exact invariance, or a witness built from the
primitives. Contrast G_015 and G_017, which imported `G`, `c`, the finesse, `Q` and the polarizability — and
were thereby *falsifiable*. That contrast is itself a result (see §8).

## 1. DERIVED — the occupancy measure

| property | value |
|---|---|
| cells | 96 |
| eigenspaces `A₀` | 45 |
| free room `Σ(m−1)` | 51 |
| state affine dimension `N−1` | **95** |
| `Σρ` | 1 for every configuration |
| `ρ_min` | > 0 for every configuration tested |

**I3 is the key step:** nothing in "ρ = counting" was ever a laboratory number, so G_017's exclusion removed an
**identification**, not the quantity. The counting measure's definition held out all along.
**I4** holds trivially and uniquely: it *is* ρ, so it loses nothing — unlike every other candidate.

> The identity that remains is a **dimensionless, normalised counting measure** whose physical content is
> entirely in the **shape** of the occupancy.

## 2. REFUTED — the actualization count is a label

Scale invariance (G_009), verified exactly: `ρ` and `λ·ρ` are physically identical in every **ratio**.

| scale | `Σρ` | `max\|a\|` | clock separation |
|---|---|---|---|
| 1 | 1.000000 | **0.6031746016455657** | **0.9985774245179969** |
| 1e3 | 1000.000000 | 0.6031746016517336 | 0.9985774245179969 |
| 1e−6 | 0.000001 | 0.6031746016442414 | 0.9985774245179969 |
| 96 | 96.000000 | 0.6031746016235374 | 0.9985774245179970 |

Every observable is a **ratio**, and ratios are scale-free — so the count `N·ρ` is a **label with zero
content**. "Actualization count" is not a distinct identity; it is ρ with a tag. (Also verified:
`ρ_i/ρ_j = (7ρ_i)/(7ρ_j)` identically.)

## 3. DEGENERACY OCCUPANCY — DERIVED content, BOUNDARY dimension

The within-multiplet distribution is **exactly** the part of ρ that the energy functional cannot see:
`ΔE = 0` for the canonical witness (G_016), and a `0.012` transfer inside an m = 6 multiplet leaves `|ΔE| < 1e-13`.

| share of the state | value |
|---|---|
| invisible to **energy** | **94/95 = 98.9474 %** |
| invisible to **everything except arrangement** | **51/95 = 53.6842 %** |
| the λ-mixing room | 43 |
| the energy direction | 1 |

So it is **DERIVED as content** (a genuine part of the state) while its **dimension 51 = Σ(m−1)** is
**BOUNDARY** — inherited from the D96 lattice, not from the state. This is the project's two-level rule
(D_028/D_040) applied: a quantity may be DERIVED as a value while its requirement is BOUNDARY.

Multiplicities: one of 6, one of 5, forty-two of 2, one of 1 → `A₀ = 45`.

## 4. REFUTED — the survivor distribution is a lossy readout

| functional | dimensions kept | discarded |
|---|---|---|
| per-multiplet totals (the degeneracy distribution) | **44** of 95 | **exactly the 51-dim room** |
| `E = ⟨λ,ρ⟩` | **1** of 95 | **94** |
| survivor compaction | — | **moves `E`** (`\|ΔE\| > 1e-3`), so not even energy-blind |

And the decisive test: two **different** states share the same survivor data (a `1e-6` transfer *within* a
multiplet leaves the block sums identical to 1e-12 while `L1 > 0`). A lossy functional cannot be the identity.

## 5. BOUNDARY — state accessibility is the lattice

| K | `A₀` | free room `N − A₀` |
|---|---|---|
| 1 | 49 | 47 |
| 2 | 47 | 49 |
| 3 | 45 | 51 |
| 4 | 47 | 49 |
| 5 | 45 | 51 |
| 6 | 45 | **51** |

The **same** uniform ρ exists for every lattice (`L1 = 0`), so accessibility does **not** determine ρ. It is a
**CAPACITY** input (G_007) that **bounds** what ρ can reach without being ρ. (Its *dynamical* face — which
configurations the flow permits — is G_005's SUPPRESSED / G_008's STABLE·METASTABLE·SUPPRESSED.)

## 6. Verdicts

| label | content |
|-------|---------|
| **DERIVED** | the **occupancy measure** — a dimensionless, normalised counting measure over distinguishable states; I1–I4 all verified. Plus the **degeneracy occupancy** as *content* (exactly the energy-invisible part of the state). |
| **BOUNDARY** | **state accessibility** (`A₀` = 45, free room 51, and the K-ladder 49/47/45/47/45/45) — a lattice capacity that bounds the reachable set without determining the state; and the **dimension 51 = Σ(m−1)** that the degeneracy occupancy inherits from it. |
| **REFUTED** | the **actualization count** — a label, by exact scale invariance; and the **survivor distribution** — a lossy functional of ρ that even moves `E`. |

## 7. What remains, stated plainly

> **ρ is an arrangement.** A dimensionless counting measure whose physical meaning is entirely in the *shape*
> of the occupancy and none of it in its *size*; whose surviving content is the energy-invisible 51-dimensional
> degeneracy structure (53.6842 % of the state), and whose remaining 43 dimensions mix distinct weights with
> zero net.

Nothing in that sentence refers to energy, mass, intensity, or a probability amplitude in the Born sense. That
is what survived G_017.

## 8. The honest limit — and the provenance asymmetry

This audit imports **no constant**, which makes it the **most AT-native audit of the whole group** — and
**correspondingly unfalsifiable by itself**. A purely combinatorial identity statement makes no contact with an
experiment. Its falsifiable content lives entirely in the **BOUNDARY identifications**, and G_017 showed the
laboratory one is experimentally excluded.

That asymmetry is the group's real finding about method:

| audit type | imports | status |
|---|---|---|
| **combinatorial / invariance** (G_001, G_002, G_005–G_008, G_011–G_014, G_016, G_016b, **G_018**) | nothing | DERIVED structure — but not by itself falsifiable |
| **boundary-identified** (G_003, G_004, G_009, G_010, G_015, G_017) | `G`, `c`, GM/R, finesse, `Q`, polarizability, 1e-18 | falsifiable — and where testable, **excluded or shown to be a rank-1 shadow** |

So AT's identity content is **combinatorial**, and it becomes *physics* only when a boundary identification is
supplied — and every identification attempted so far has been either **excluded** (lab |ψ|², G_017) or shown to
be a **rank-1 shadow** of the real object (mass-energy, G_016).

## 9. Classification and caveats

**No reclassification.** G_001/G_002/G_005/G_007/G_009/G_014/G_016/G_016b/G_017 unchanged inputs. D_040
untouched; no canonical claim, value or equation changes; no new primitive. Deterministic: exact
combinatorics, no randomness, no imported constant.

* The I3 test is the audit's pivot: it distinguishes "the quantity was wrong" from "the identification was
  wrong". G_017 did the latter.
* The DERIVED-on-BOUNDARY structure of the degeneracy occupancy is deliberate and follows the project's
  two-level rule; it is not a hedge.
* Accessibility's two faces must not be conflated: the *capacity* is BOUNDARY (a lattice input), the
  *dynamical* accessibility is DERIVED in structure and BOUNDARY in its bounds (G_005/G_008).

## 10. Open problems (OP1–OP5)

1. Is there a **third** kind of audit — one that imports a constant *and* survives observation? That is the
   only route by which the identity in §7 becomes physics.
2. Can the 43-dimensional λ-mixing room be given a **boundary identification** (i.e. a measurable correlate)
   without reintroducing intensity?
3. Is the free room 51 a **physical** capacity or only a lattice artefact — does any measurement depend on
   it directly (G_016 OP1)?
4. Does the "arrangement-only" character of ρ (53.6842 % of the state) imply any **observational degeneracy**
   beyond G_016b's fixed-energy family?
5. Can the provenance asymmetry in §8 be made quantitative as a **falsifiability budget** for the theory?

## Result summary

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_018_Tests.cs` — **7/7 PASSED** (~0.07 s)
**Group total:** G_001–G_018 = **161/161 PASSED** (~1 s)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_018"`

| label | content |
|-------|---------|
| **DERIVED** | the occupancy measure (dimensionless, normalised, counting-defined, information-preserving) · the degeneracy occupancy as content |
| **BOUNDARY** | state accessibility (`A₀` = 45, free room 51) · the dimension 51 = Σ(m−1) |
| **REFUTED** | the actualization count (a label, by exact scale invariance) · the survivor distribution (a lossy functional) |

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_017.md`, `ResearchY-G_016.md`, `ResearchY-G_016b.md`,
  `ResearchY-G_014.md`, `ResearchY-G_009.md`, `ResearchY-G_007.md`
* `Docs/ResearchY/Tests/Results/Y_G_018_Result.md`
* `AT.Tests/Shared/DensityField.cs`
