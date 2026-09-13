# Y_G_040 — Rho Observable Audit — Result

**Suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_040_Tests.cs` — **9/9 PASSED**
**Core:** `AT.Core/ResearchXH/RhoObservableAudit.cs`
**Doc:** `Docs/ResearchY/G_GravitySource/ResearchY-G_040.md`

## Question

Can any measurable quantity retain **all 95 dimensions** of ρ?

**Candidates:** occupancy patterns, mode populations, detector counts, attractor occupancy, survivor occupancy.
**Test:** dimension retained, information loss, invertibility.

## Answer

**REFUTED.** No measurable quantity retains all 95 dimensions, and the ceiling is a **symmetry theorem** rather
than a detector limitation: every operator AT can construct commutes with the substrate's dihedral symmetry, so
the observables live in the **centralizer algebra**, whose dimension is **49** against a **95**-dimensional state
space. The 47 dimensions that remain unreachable are exactly the **intra-doublet orientations** — one angle per
two-dimensional irrep, and D96 has exactly 47 of those.

## The recomputed ground

96 cells · 45 distinct levels · histogram {1:1, 2:42, 5:1, 6:1} · trace 1152 = 2 × 576 links · state space 95.
All figures reproduced, none cited.

## The centralizer dimension — four independent routes

| route | value |
|---|---|
| orbital enumeration (9216 ordered pairs × 192 elements) | 49 |
| Burnside (1/\|G\|) Σ fix(g)² = 9408/192 | 49 |
| multiplicity-free irrep decomposition | 49 |
| Σ over levels of the restricted algebra rank (1 + 42·1 + 3 + 3) | 49 |

Structure: a level of multiplicity *m* admits *m*² operators but the algebra reaches only one dimension per
**irrep** inside it — 181 of the 230 ambient dimensions are protected.

## The ladder

| measurement class | data | retained | loss |
|---|---|---|---|
| site-addressed cell counts | 96 | **95** | **0** — but this *is* ρ |
| distance-class contractions | 48 | **48** | **47** — the ceiling |
| spectral level populations | 45 | **44** | **51** — exactly G_039's free room |
| on a four-channel state | 48 | **4** | **91** — the regime caveat |

51 − 47 = **4** = what the only two multi-irrep levels (m = 5 and m = 6, three irreps each) are worth.

## Witnesses

* Rotating one doublet's orientation by 1.1 rad moves the state by **1.997e−1** in L1 (contrasty state) or
  **6.93e−3** (flat generic state) while moving every contraction by at most **2.78e−17**.
* All **192** group images of a generic state report **one** distinct reading (spread **3.12e−17**) while sitting
  up to **9.23e−2** apart — a fibre of at least 192-to-1.

## Registry

`ClockOnly` → **SURVIVES**, `ScanDetectsIt: false`. Counts become **27 / 11 / 3 of 41**; boundary index
unchanged; no prior classification changed. G_039's BOUNDARY verdict stands — G_040 explains its measurability
finding rather than revising it.

G_033's live classifier now reads **22** substrate suites (39 classified in total), because this suite recomputes
the D96 ring spectrum.

## Self-caught slips

1. **The rank threshold must be relative to the whole matrix, not to each row.** A per-row relative tolerance
   accepted a row of pure roundoff — A₂₄ restricted to channel 1 is *exactly* zero (2cos(2π·24/96) = 0), leaving a
   norm of 2.8e−32 that exceeded 1e-8 times itself. Every doublet read as rank 2; the 45 levels summed to **143**
   instead of 49.
2. **One cos/sin pair per channel.** Enumerating the 96 modes with a per-mode guard doubles the ±k pair; the
   resulting "projector" is not idempotent (|P² − P| = 1/24).
3. **A level projector lives in the mode basis.** A cell-indexed mask is a different operator.
4. **The witness must use the invariant contraction ⟨ρ, A_d ρ⟩**, not the equivariant vector A_d ρ — the latter is
   permuted by the group and showed a spurious 2.1e−2 "difference".
5. **The regime caveat is real**: a state confined to four channels retains only 4 dimensions, so the headline is
   quoted for a state populating all 49 channels — and both regimes are asserted.
