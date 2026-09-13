# ResearchY-G_041 — Substrate Dimension Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_041 (permanent)
**Title:** Is the substrate dimension d = 3 selected, or assumed — and what do D96² and D96⁴ do?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_041.md`
**Depends on:** G_033 (the substrate requirement: a dimension-3 irrep, which the ring cannot supply), G_016b (the clock law ρ^(1/d), whose exponent **is** the dimension), E_003 (the dihedral budget; the graviton's traceless part is E + T2), E_004 (the vector sector is T1(3)), G_039/G_040 (which quote the d = 3 clock figure)
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_041_Tests.cs` (8/8 PASSED)
**Core:** `AT.Core/ResearchXH/SubstrateDimensionAudit.cs`

## The question

AT needs the **ring** (d = 1) and, as G_033 established, the **cube** (d = 3). What about **d = 2** and **d = 4** —
do they change anything the programme must reckon with, and is d = 3 **selected** or **assumed**?

**The family** is D96^d: the d-fold tensor product of the *same* 96-cell circulant C96(1..6). Its symmetry is the
**signed-permutation group B_d = C2^d ⋊ S_d** of order 2^d·d! — and **B₃ is the full cubic group O_h of order
48**, the group E_003/E_004/G_033 used. The d = 1 case is *not* B₁: AT's ring is **periodic**, so its symmetry is
the **dihedral group of order 192**, whose irrep budget E_003 computed.

## The answer: BOUNDARY — and the two halves point opposite ways

## 1. The ladder

The groups are **constructed** (not asserted) and checked for distinctness; the irrep dimension spectra come from
the pair-of-partitions classification with `dim(λ,μ) = C(d,|λ|)·f^λ·f^μ` (hook-length formula), and **Burnside's
sum rule Σ d_i² = |G| is verified as an identity for every d on the ladder** rather than assumed.

| d | group | order | #irreps | max irrep dim | 3-dim sector? | vector is largest? |
|---|---|---|---|---|---|---|
| 1 | D96 (ring, periodic) | 192 | 51 | **2** | no | n/a |
| 2 | B₂ | 8 | 5 | **2** | **no** | yes |
| 3 | B₃ = O_h | 48 | 10 | **3** | **yes** | yes |
| 4 | B₄ | 384 | 20 | **8** | yes | **no** |
| 5 | B₅ | 3840 | 36 | **20** | yes | no |
| 6 | B₆ | 46080 | 65 | **80** | yes | no |

Irrep dimension spectra: d=2 {1,2} · d=3 {1,2,3} · d=4 {1,2,3,4,6,8} · d=5 {1,4,5,6,10,15,20} · d=6 up to 80.

## 2. D96² fails exactly as the ring fails — this *strengthens* G_033

The maximum irrep dimension is **2** for the ring and **2** for the square torus. A dimension-3 sector needs a
dimension-3 irrep (G_033: the photon needs T1(3), the metric's trace-free part needs T2(3)), so **d = 2 supplies
none**. The first dimension that works is **d = 3** — computed, not chosen:

> **the cube is the MINIMAL working dimension, not merely the selected one.**

## 3. But the irrep-supply argument does *not* select d = 3 — the negative result

Character inner products over the group, computed for every d:

| d | ⟨V,V⟩ | ⟨W,W⟩ | ⟨V,W⟩ | ⟨V,A⟩ | dim V | dim W | dim A |
|---|---|---|---|---|---|---|---|
| 2 | 1 | 2 | 0 | 0 | 2 | 2 | 1 |
| 3 | 1 | 2 | 0 | 0 | 3 | 5 | 3 |
| 4 | 1 | 2 | 0 | 0 | 4 | 9 | 6 |
| 5 | 1 | 2 | 0 | 0 | 5 | 14 | 10 |
| 6 | 1 | 2 | 0 | 0 | 6 | 20 | 15 |

V = vector, W = traceless symmetric rank-2, A = antisymmetric rank-2.

* ⟨V,V⟩ = 1 — the vector is a **single irrep at every d**.
* ⟨W,W⟩ = 2 — the traceless sector **always splits into exactly two pieces** (at d = 3 that is E + T2, exactly
  E_003's finding — so E_003's result was never a d = 3 coincidence).
* ⟨V,W⟩ = 0 — the two sectors **never mix**.

**The signature is dimension-blind.** Nothing in it distinguishes d = 3 from d = 4, so the claim that the
irrep-supply argument **selects** d = 3 is **REFUTED as a selector**: it selects **d ≥ 3**. What breaks only later
is that the **vector stops being the largest irrep** — max irrep dim equals d for d = 1, 2, 3, then jumps to **8**
at d = 4 and **20** at d = 5.

> **REFINEMENT (G_042 → G_041).** G_042 computes that these two accidents are **one condition with two
> derivations**, not two independent ones: the identity
> `graviton polarisations = dim(Λ²) − 1 = dim(so(d)) − 1` holds for **every** d, so subtracting one from the
> photon's count turns "the polarisations are equal" into `dim(Λ²) = dim(V)` **identically** rather than
> coincidentally. The **number of independent reasons for d = 3 is therefore 1, not 2.** Everything else in this
> audit stands unchanged, including its verdict: d = 4 is still not excluded by the representations, only by
> this single accident, by the polarisation mismatch, by the clock exponent and by cost. The heading below is
> left in its original form so the correction is visible rather than silent; read "accidents" as "faces of one
> accident". Carried in code as `SubstrateDimensionAudit.RefinementFromG042()`.
## 4. Two independent accidents *do* pin d = 3

| selector | holds at | d = 2 | d = 4 |
|---|---|---|---|
| **ε/Hodge:** dim Λ² = dim V | **d = 3 only** (3 = 3) | Λ² = **1** — a *scalar* | Λ² = 6 |
| **polarisation match:** photon d−1 = graviton (d+1)(d−2)/2 | **d = 3 only** (2 = 2) | **1 vs 0** — no propagating graviton | 3 vs 5 |

Both are computed over the whole ladder and both select d = 3 **uniquely**. The ε/Hodge accident is precisely the
axial structure T2(3) that E_003 and G_033 require; the polarisation match is the reason electromagnetism and
gravity share the two-polarisation story in four dimensions.

## 5. The consequence: the choice is *observable*

The clock law is `dτ/dt = ρ^(1/d)` (G_016b), so the **exponent is the dimension**:

| d | exponent | clock rate for the same 20:1 contrast | modes |
|---|---|---|---|
| 2 | 0.500000 | 129 415.634 s/day | 9 216 |
| 3 | 0.333333 | **86 277.089 s/day** ← the published figure | 884 736 |
| 4 | 0.250000 | 64 707.817 s/day | 84 934 656 |

**The figure G_016b, G_039 and G_040 quote is a d = 3 number**, and every observable that inherits the clock law
inherits the dimension. So "which d" is falsifiable rather than conventional.

## 6. Caveats

**(a) d = 4 is not excluded by the representations.** Its vector is irreducible, its traceless sector splits into
two, and it carries irreps of dimension 4 and 8. What argues against it is the two accidents, the polarisation
mismatch (3 against 5), the clock exponent, and cost: **84 934 656 modes against 884 736** (96×).

**(b) The d = 1 row uses a different group**, because the ring is periodic — dihedral of order 192, not B₁ of
order 2. Treating the ring as B₁ would be a category error, and it is the reason the ring's own failure (max
irrep 2) is *not* the same argument as d = 2's.

**(c) The irrep spectra are exact**, not tolerance-clustered: this audit counts *representations of a finite
group*, so it is immune to the A₀ robustness problem G_033 documented (which concerns spectral *level counting*
on a large lattice). At d = 4 that problem is not made better — 96× the modes means far more clustering — but it
is not what this audit measures.

**(d) Polarisation counts are the standard massless counts** in d spatial dimensions: spin-1 carries d−1 and
spin-2 carries (d+1)(d−2)/2. At d = 1 the spin-2 count is negative, i.e. there is no propagating graviton in a
2-dimensional spacetime at all.

## 7. Classification

**No reclassification, and one hypothesis WEAKENED — recorded rather than smoothed over.** G_033's positive
finding stands and is **strengthened** (d = 2 fails like the ring, so the cube is minimal). What is weakened is
any reading of G_033/E_003 that treats "the cube supplies T1(3) and T2(3)" as *selecting* d = 3: the computed
signature is dimension-blind, and d = 4 also supplies a vector sector. The programme's claim to d = 3 therefore
rests on the ε accident, the polarisation match and the clock exponent — all computed here — and **not** on the
irrep-supply argument alone. Per the project's memory rules this is recorded in `NewChat_Start.md`.

**Registry:** added to the G_035 classification registry as **`ClockOnly`** → **SURVIVES** (group theory plus the
g₀₀ clock law), triaged `ScanDetectsIt: false`. Counts become **28 / 11 / 3 of 42**; the boundary index is
unchanged; no prior classification changed.

**Scanner side-effect, recorded:** G_033's live classifier now reads **24** substrate suites (40 classified in
total), because this suite works with the D96ᵈ family explicitly.
