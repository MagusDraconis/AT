# ResearchY-G_040 — Rho Observable Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_040 (permanent)
**Title:** Can any measurable quantity retain all 95 dimensions of ρ?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_040.md`
**Depends on:** G_016/G_016b (the density and its energy independence), G_017 (the laboratory |ψ|² identification is excluded), G_018 (the identity of ρ is the zero-loss occupancy measure), G_039 (the carrier is the occupancy of the reachable set; the spectral loss is 51), E_003 (the phase lives on links, and the dihedral irrep budget)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_040_Tests.cs` (9/9 PASSED)
**Core:** `AT.Core/ResearchXH/RhoObservableAudit.cs`

## The question

Can any measurable quantity retain **all 95 dimensions** of ρ?

**Candidates:** occupancy patterns, mode populations, detector counts, attractor occupancy, survivor occupancy.
**Test:** dimension retained, information loss, invertibility.
**Goal:** the first observable O such that O ↔ ρ is approximately invertible.

## The answer: REFUTED — and the ceiling is a symmetry theorem, not a detector limitation

G_039 identified the carrier (the occupancy of the reachable set) and found **measurability** to be the one unmet
requirement, with a spectral loss of 51. G_040 asks the sharper question — is **any** measurable quantity
lossless — and answers it by computing the retained dimension of every measurement class. The answer is no, and
the obstruction is the **substrate's own symmetry**.

## 1. The state space, recomputed

| quantity | recomputed | the record |
|---|---|---|
| cells | 96 | 96 |
| distinct levels | **45** | 45 ✓ |
| multiplicity histogram | **{1:1, 2:42, 5:1, 6:1}** | ✓ |
| Laplacian trace | **1152** = 2 × 576 links | 1152 ✓ |
| state dimension (simplex) | **95** | 95 ✓ |

## 2. The symmetry, built explicitly

The substrate's symmetry is not asserted — the 192 permutations are constructed (96 rotations i ↦ i+k, 96
reflections i ↦ k−i) and every property is computed from them:

| property | computed |
|---|---|
| group order | **192** (192 distinct permutations) |
| closed under composition | **true** (all 192² pairs, matched on all 96 images) |
| orbits on the cells | **1** — the action is transitive |
| fixed points of an element | 96 once; 2 for the 48 even reflections; 0 for the other 143 |
| Σ fix(g)² | **9408** |

**The centralizer algebra dimension**, computed four independent ways:

| route | value |
|---|---|
| orbital enumeration (all 9216 ordered pairs under all 192 elements) | **49** |
| Burnside / Reynolds (1/\|G\|) Σ fix(g)² = 9408/192 | **49** |
| multiplicity-free irrep decomposition (Σ irrep multiplicities²) | **49** |
| Σ over the 45 levels of the algebra's restricted rank | **49** |

The last route is the structural one: a level of multiplicity *m* admits *m*² operators, but the algebra reaches
only as many dimensions as there are **irreps** inside it — by Schur's lemma it must act as a scalar inside each.

| level multiplicity | levels | irreps each | rank reached | protected |
|---|---|---|---|---|
| m = 1 | 1 | 1 | 1 | 0 |
| m = 2 | 42 | 1 | 1 | 3 |
| m = 5 | 1 | 3 | 3 | 22 |
| m = 6 | 1 | 3 | 3 | 33 |
| **total** | **45** | | **49** | **181** = 230 − 49 |

## 3. The resolution ladder

Retained dimension = the rank of the datum's Jacobian restricted to the simplex tangent space (95 dimensions).

| measurement class | data | retained | loss | invertible? |
|---|---|---|---|---|
| site-addressed cell counts | 96 | **95** | **0** | trivially — it **IS ρ** |
| distance-class contractions ⟨ρ, A_d ρ⟩ | 48 | **48** | **47** | no — this is the ceiling |
| spectral level populations | 45 | **44** | **51** | no |

Two cross-checks tie the ladder to what is already on the record:

* the **spectral loss is exactly G_039's free room**, 51 = Σ(m−1) = 96 − 45;
* the difference (51 − 47 = **4**) is exactly what the two multi-irrep levels are worth — the m = 5 and m = 6
  levels each merge 3 irreps into one reading, and resolving them reclaims 2 dimensions apiece.

**So the 95 dimensions split as 48 magnitudes + 47 intra-doublet orientations**, and the 47 lost dimensions are
not an abstraction: they are one angle per two-dimensional irrep, and D96 has exactly **47** of those.

## 4. Why invertibility fails — two computed witnesses

**(a) Rotate one doublet's orientation** (channel 7, by 1.1 rad). The rotation is orthogonal inside the irrep, so
it preserves the sum and every level norm:

| state | state moved (L1) | largest contraction moved |
|---|---|---|
| flat generic state | 6.93e−3 | **2.08e−17** |
| channel-confined (contrasty) state | **1.997e−1** | **2.78e−17** |

The size of the unseen move is a property of the **state**; its invisibility is a property of the **theorem**.

**(b) All 192 group images of a generic state**: **one** distinct reading, largest disagreement in the data
3.12e−17, while the images sit up to 9.23e−2 apart. The fibre of the measurement is therefore at least
192-to-1 before any continuous degeneracy is counted.

## 5. The candidates

| candidate | retained | loss | verdict |
|---|---|---|---|
| occupancy patterns | 95 | 0 | **REFUTED** — it *is* ρ; it presupposes naming every cell |
| mode populations | 44 | 51 | **CORRELATED** — a level-blind probe leaves the orientations free |
| detector counts | 44 | 51 | **CORRELATED** — a binned detector counts levels, not cells |
| attractor occupancy | 95 | 0 | **REFUTED** — the same object read at the attractor (G_039) |
| survivor occupancy | 95 | 0 | **REFUTED** — the same object after the transient (G_039) |
| distance-class contractions | 48 | 47 | **CORRELATED** — the closest distinct rung |

## 6. Caveats and self-caught slips

**(a) The regime caveat.** The retained dimension is a **generic-state** statement. On a state confined to four
channels the same 48 contractions collapse to **rank 4** (loss 91) — a sparse state is *less* observable, not
more. Both regimes are asserted, not just the flattering one.

**(b) A numerical slip, caught by the audit's own tests.** The rank threshold must be relative to the **whole
matrix**, not to each row. With a per-row relative tolerance, a row of pure roundoff was accepted: the
restriction of A₂₄ to channel 1 is *exactly* the zero matrix, because 2cos(2π·24/96) = 0, leaving a computed norm
of 2.8e−32 that trivially exceeded 1e-8 times itself. Every doublet then read as rank 2 and the 45 levels summed
to **143** instead of 49.

**(c) Two more caught while building the basis.** A level's real block is spanned by **one cos/sin pair per
channel**; enumerating the 96 modes with a per-mode guard doubles the ±k pair and builds a projector that is not
idempotent (the doubled block gave |P² − P| = 1/24). And a level projector must be built in the **mode** basis —
a cell-indexed mask is a different operator entirely.

**(d) The boundary of the result.** The 47-dimension gap is the price of not addressing cells. A measurement that
can name a cell — 96 site-labelled detectors — retains all 95, because it **is** ρ, and G_017 excluded exactly
that identification. What is refuted here is "some measurable quantity retains all 95 dimensions"; what is
established is "every observable AT can construct is confined to 48 of them, and the missing 47 are protected by
the substrate's symmetry".

## 7. Classification

**Registry:** added to the G_035 classification registry as **`ClockOnly`** → **SURVIVES** (every statement is
about ρ, the spectral substrate and its group), triaged `ScanDetectsIt: false`. Counts become **27 / 11 / 3 of
41**; the boundary index is unchanged; no prior classification changed.

**No reclassification.** G_016, G_016b, G_017, G_018, G_035 and G_039 are unchanged inputs. G_040 sharpens
G_039's measurability finding — it explains *why* the natural realization is unavailable — without altering its
BOUNDARY verdict. The D_040 registry is untouched; no canonical claim, value or equation changes; no new
primitive is added.

**One scanner side-effect, recorded:** the new suite recomputes the D96 ring spectrum, so G_033's live classifier
now reads **22** substrate suites instead of 21 (39 classified suites in total).
