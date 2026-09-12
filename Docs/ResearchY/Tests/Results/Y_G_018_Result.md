# Y_G_018_Result.md — ResearchY-G_018 Rho Identity Audit

**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_018_Tests.cs`
**Run:** 2026-09-12
**Result:** ✅ 7/7 PASSED (~0.07 s) — group G total 161/161 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_G_018"`

## Summary

**Question:** after G_017 excluded the identification of a laboratory |ψ|² with ρ, which physical quantity
remains as the **identity** of ρ? (candidates: occupancy measure, actualization count, state accessibility,
degeneracy occupancy, survivor distribution)
**Answer:** the **occupancy measure** — a dimensionless, normalised **counting measure over distinguishable
states**, whose entire physical content is in the **arrangement** and none in the amount.

**Provenance ledger: this audit imports NO constant.** No SI quantity, no measured number, no laboratory
system. Contrast G_015/G_017, which imported `G`, `c`, finesse, `Q`, polarizability — and were falsifiable.

## The criterion

Q is the identity of ρ iff **(I1)** defined by counting over distinguishable states (Difference →
distinguishability only); **(I2)** dimensionless and normalised; **(I3)** survives G_017 (never held a lab
number); **(I4)** determines ρ (not a lossy functional).

## Detail — DERIVED: the occupancy measure

| property | value |
|---|---|
| cells | 96 |
| eigenspaces `A₀` | 45 |
| free room `Σ(m−1)` | 51 |
| state affine dimension `N−1` | **95** |
| `Σρ` / `ρ_min` | 1 / > 0 for every configuration |

**I3 is the pivot:** nothing in "ρ = counting" was ever a laboratory number, so G_017 removed an
**identification**, not the quantity. **I4:** it *is* ρ — zero loss.

## Detail — REFUTED: the actualization count is a label

Scale invariance (G_009), exact:

| scale | `Σρ` | `max\|a\|` | clock separation |
|---|---|---|---|
| 1 | 1.000000 | **0.6031746016455657** | **0.9985774245179969** |
| 1e3 | 1000.000000 | 0.6031746016517336 | 0.9985774245179969 |
| 1e−6 | 0.000001 | 0.6031746016442414 | 0.9985774245179969 |
| 96 | 96.000000 | 0.6031746016235374 | 0.9985774245179970 |

Every observable is a ratio, so `N·ρ` carries **zero content**.

## Detail — degeneracy occupancy: DERIVED content, BOUNDARY dimension

`ΔE = 0` exactly for the canonical witness; a 0.012 transfer inside an m = 6 multiplet leaves `|ΔE| < 1e-13`.

| share | value |
|---|---|
| invisible to energy | **94/95 = 98.9474 %** |
| invisible to everything except arrangement | **51/95 = 53.6842 %** |
| λ-mixing room | 43 |
| energy direction | 1 |

DERIVED as content; its dimension `51 = Σ(m−1)` is **BOUNDARY** (lattice, G_007) — the two-level rule
(D_028/D_040).

## Detail — REFUTED: the survivor distribution is a lossy readout

| functional | kept | discarded |
|---|---|---|
| per-multiplet totals | **44** of 95 | **exactly the 51-dim room** |
| `E = ⟨λ,ρ⟩` | **1** of 95 | **94** |
| survivor compaction | — | **moves `E`** (\|ΔE\| > 1e-3) |

Decisive test: two **different** states share the same survivor data (a within-multiplet 1e-6 transfer leaves
block sums identical to 1e-12 while `L1 > 0`).

## Detail — BOUNDARY: state accessibility is the lattice

| K | `A₀` | free room |
|---|---|---|
| 1 | 49 | 47 |
| 2 | 47 | 49 |
| 3 | 45 | 51 |
| 4 | 47 | 49 |
| 5 | 45 | 51 |
| 6 | 45 | **51** |

The **same** uniform ρ exists for every lattice (`L1 = 0`), so accessibility does not determine ρ.

## What remains

> **ρ is an arrangement** — a dimensionless counting measure whose meaning is entirely in the shape of the
> occupancy, with its surviving content the energy-invisible 51-dimensional degeneracy structure (53.6842 % of
> the state) and the other 43 dimensions mixing distinct weights with zero net.

## The honest limit (§8 of the doc)

This audit imports no constant, which makes it the **most AT-native of the group** — and **unfalsifiable by
itself**. Its falsifiable content lives in the boundary identifications, and G_017 excluded the laboratory one.
The provenance asymmetry is the group's real methodological finding: **AT's identity content is combinatorial,
and becomes physics only via a boundary identification — and every one attempted so far has been excluded or
shown to be a rank-1 shadow.**

## Classification and caveats

**No reclassification.** D_040 untouched; no canonical claim, value or equation changes; no new primitive.
Deterministic: exact combinatorics, no randomness, no imported constant.

## Open problems (OP1–OP5)

1. A **third** kind of audit — imports a constant *and* survives observation?
2. Can the 43-dim λ-mixing room be boundary-identified without reintroducing intensity?
3. Is the free room 51 physical or a lattice artefact?
4. Does "arrangement-only" imply an observational degeneracy beyond G_016b's family?
5. Can §8's asymmetry be made a quantitative **falsifiability budget**?

## References

* `Docs/ResearchY/G_GravitySource/ResearchY-G_018.md`
* `AT.Tests/Shared/DensityField.cs`
