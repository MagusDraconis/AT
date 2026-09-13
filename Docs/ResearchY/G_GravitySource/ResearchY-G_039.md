# ResearchY-G_039 — Rho Realization Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_039 (permanent)
**Title:** What physical observable can carry ρ?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_039.md`
**Depends on:** G_016 (mass is a 1-D shadow of the density), G_016b (density is free of energy, not the reverse), G_017 (the laboratory |ψ|² identification is excluded), G_018 (the identity of ρ is the zero-loss occupancy measure), G_035 (25 of 36 results need g₀₀ only), G_002/G_005/G_008/G_012/G_013 (the free room, suppression, and the actuator), E_003 (the phase lives on links)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_039_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/RhoRealizationAudit.cs`

## Note on the ID

The audit was requested as **G_036**, which is already taken by the **Temporal Core Test Audit** delivered earlier
in the same session. The group-G ID space is **permanent** and is keyed by the ResearchY index and by the G_035
classification registry, so the next free number was used: **G_039**.

## The question

What physical observable can carry ρ while remaining compatible with **G_016**, **G_016b**, **G_017**, **G_018**
and **G_035**?

**Candidates:** occupancy distributions, state populations, degeneracy occupation, attractor occupation, survivor
distributions.
**Requirements:** (1) measurable, (2) not energy, (3) not phase, (4) not information, (5) preserves the clock law.

## The answer: BOUNDARY — and the filter reduces to ONE binding requirement

## 1. The spectrum, recomputed rather than trusted

Every figure the constraint audits quote is **recomputed from the circulant**:

| quantity | recomputed | the record |
|---|---|---|
| cells | 96 | 96 |
| distinct levels A₀ | **45** | 45 ✓ |
| multiplicity histogram | **{1:1, 2:42, 5:1, 6:1}** | {1:1, 2:42, 5:1, 6:1} ✓ |
| free room Σ(m−1) | **51** = 96 − 45 | 51 ✓ |
| state dimension (simplex) | **95** | 95 ✓ |
| Laplacian trace | **1152** = 2 × 576 links | 1152 ✓ |

**All four agree.** The audit therefore stands on recomputed ground.

## 2. Requirements 2–4 fall out of a single observation

**Energy and information are each ONE number on a 95-dimensional space**, so **each is lossy by 94 dimensions**:

| functional | dimension of the object | lossy by |
|---|---|---|
| energy `E = ⟨λ,ρ⟩` (G_016) | 95 | **94** |
| entropy `H(ρ)` | 95 | **94** |

**The energy kernel splits exactly**, matching G_016b:

| part | dimension |
|---|---|
| within-multiplet (λ constant inside a multiplet) | **51** |
| level mixing (45 levels − normalisation − energy) | **43** |
| **sum** | **94** ✓ |

### The computed witness (on the unique m = 6 multiplet, λ = −2)

| quantity | value |
|---|---|
| λ inside the multiplet | **−2.000000** — constant, so ΔE = 0 **exactly** |
| energy change | **0.000×10⁰** |
| ρ change (L1, 20:1 tilt) | **1.2666666667** |
| clock shift | **0.9985774245** = **86 277.089 s/day** |

**A 20:1 tilt moves no energy at all and shifts the clock by nearly a full day per day.** An energy-carrying
observable therefore cannot carry ρ.

### The information witness (two *different* states, same entropy)

| state | entropy |
|---|---|
| A = (0.5000000000, 0.5000000000) | **0.6931471806** |
| B = (0.7729078048, 0.1135460976, 0.1135460976) | **0.6931471806** |

L1 separation **0.7729**. The entropy is a **function** of ρ but **not injective**, so it cannot carry ρ either.

### Phase is separated sectorially, not numerically

ρ counts **sites**; the phase lives on **links** (E_003: `PhaseOrigin` assigns 2π/96 per link, with a loop
holonomy and a `2 + 2cos δ` interference law). A phase rotation leaves the counts invariant — ρ change
**0.0×10⁰** — while moving the holonomy by **0.065450**.

### The clock law is reproduced

`dτ/dt = ρ^(1/d)` gives `(1/d)ln 20 = 0.9985774245` with d = 3, i.e. **86 277.089 s/day** — G_016b's own figure.

## 3. The candidates are decided by LOSS

| candidate | zero-loss | discarded dims |
|---|---|---|
| occupancy distributions | **YES** | 0 |
| state populations | **NO** | **51** |
| degeneracy occupation | **NO** | **51** |
| attractor occupation | **YES** | 0 |
| survivor distributions | **YES** | 0 |

**State populations and degeneracy occupation are refuted as the identity**: averaging over a multiplet discards
precisely the **51-dimensional within-multiplet room** — which is the **free room**, and the only part an actuator
can actually move (G_002/G_012/G_013).

**The three zero-loss candidates are ONE object read at three stages** — the general occupancy, its value at the
attractor, and the set that survives the transient. Because the dynamics selects one attractor from all initial
conditions (G_005/G_008), the reachable set and the attractor coincide.

## Output

**BOUNDARY.** **Four of the five requirements are met, each by computation**, and **one carrier** is identified:
the **occupancy of the reachable set**.

**The fifth — measurability — is the binding one.** G_018 could conclude **DERIVED** for the *identity* of ρ,
because for that question G_017 removed an **identification** rather than the **quantity**. But a **realization**
asks for an **observable**, and the natural laboratory realization — reading ρ off an optical intensity — is
exactly what **G_017 excluded**. What remains is the clock signature, which G_004 and G_009 already priced as
real but orders of magnitude below local sensitivity.

**So the identity is DERIVED (G_018) and the realization is BOUNDARY — and the difference is the whole point of
asking this question separately.**

## Classification and caveats

**Registry:** added to the G_035 classification registry as **`ClockOnly`** → **SURVIVES** (ρ, the source law and
the clock law are all g₀₀ statements), triaged `ScanDetectsIt: false` — the suite's vocabulary is ρ, energy,
entropy and phase, not `B`/`g_rr`/`GammaOf`. Counts become **26 / 11 / 3 of 40**; the boundary index is
unchanged; no prior classification changed.

**Caveats.**
(1) The substrate is the 96-cell circulant of G_016/G_018; every quoted figure is recomputed here.
(2) Energy is the pairing `E = ⟨λ,ρ⟩` (G_016); information is the Shannon entropy of the same occupancy vector.
The two "lossy by 94" results share a form — a single number on a 95-dimensional space — which is why they are
stated once rather than twice.
(3) The clock law uses d = 3, the dimension G_033 requires.
(4) Deterministic: closed-form spectrum, bisection for the equal-entropy witness, invariant-culture output.

**Three of this audit's own slips were caught by its own tests and are recorded rather than absorbed:** the
link-count arithmetic in the report (288 for 576), an equality at the 9th decimal that sat exactly on the
rounding boundary, and a truncated `(int)` cast of a floating-point trace.

**A fourth slip was caught by *another* audit — G_033's live scanner.** G_033 counts D96 references in the
group-G suites and separates code from comments, so that a new audit is classified automatically rather than by
hand. This audit's constants were named `Cells`/`Radius`, so the scanner filed it with the **D96-free** audits —
the metric / closure era — even though it recomputes the D96 ring spectrum. Renaming the constant to `D96Cells`
restores the correct reading and moves the G_033 counts to **22 substrate / 16 D96-free / 1 comment-only**. The
mistake was a naming one, and it is precisely what G_033's live scan exists to expose: a classifier is only as
good as the names the audited code uses.

**No reclassification.** G_016, G_016b, G_017, G_018 and G_035 are unchanged inputs; the D_040 registry is
untouched; no canonical claim, value or equation changes; no new primitive is added.
