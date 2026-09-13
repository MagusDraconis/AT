# ResearchY-G_042 — Three-Dimensionality Dependency Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** G — Gravity Source
**ID:** ResearchY-G_042 (permanent)
**Title:** Are the selectors of d = 3 independent, or the same structure viewed differently?
**Status:** COMPLETE
**Date:** 2026-09-13
**File:** `G_GravitySource/ResearchY-G_042.md`
**Depends on:** G_041 (the substrate dimension audit, which listed the ε/Hodge accident and the polarisation match as two accidents — **refined here**), G_033 (the dimension-3 irrep requirement), E_003/E_004 (the photon and graviton sectors), G_016b (the clock law ρ^(1/d))
**Test suite:** `AT.Tests/ResearchY/G_GravitySource/Y_G_042_Tests.cs` (7/7 PASSED)
**Core:** `AT.Core/ResearchXH/ThreeDimensionalityDependencyAudit.cs`

## The question

Are the known selectors of d = 3 **independent**, or are they the same structure viewed differently — is
three-dimensionality pinned by **one root mechanism** or by **several**?

**Inputs:** (1) rotation self-duality `d(d−1)/2 = d`; (2) Hodge duality `dim(Λ²) = dim(V)`; (3) the
photon/graviton polarisation equality `d−1 = (d+1)(d−2)/2`; (4) the D96^d representation structure (T1(3),
T2(3)); (5) the clock exponent `dτ/dt = ρ^(1/d)`.

## The answer: **REDUNDANT — ONE ROOT MECHANISM.** The number of independent selectors is **1**.

## 1. The identities that decide redundancy — verified for every d

| identity | holds at every d? |
|---|---|
| photon polarisations = dim(V) − 1 = **d − 1** | **true** |
| graviton polarisations = dim(Λ²) − 1 = **dim(so(d)) − 1** | **true** |
| dim(so(d)) = dim(Λ²) — the rotations *are* the antisymmetric square | **true** |
| the three geometric conditions are **one equation** | **true** |

| d | dim V | dim Λ² = dim so(d) | photons | gravitons | difference |
|---|---|---|---|---|---|
| 1 | 1 | 0 | 0 | −1 | 1 |
| 2 | 2 | 1 | 1 | 0 | 1 |
| 3 | **3** | **3** | **2** | **2** | **0** |
| 4 | 4 | 6 | 3 | 5 | −2 |
| 5 | 5 | 10 | 4 | 9 | −5 |

**The decisive identity is the second one.** The graviton carries `dim(Λ²) − 1 = dim(so(d)) − 1` polarisations
**for every d**. Subtract one from the photon's count and "the polarisations are equal" becomes
`dim(Λ²) = dim(V)` **identically** — not a coincidence that happens to share the root, the *same equation*,
reached by a different derivation (little-group counting instead of tensor algebra).

All three geometric inputs reduce to one polynomial: **`d(d−3) = 0`**, roots **{0, 3}**.

> **The root statement:** *the number of directions equals the number of independent rotations* — the existence
> of the cross product / Hodge dual in three dimensions.

## 2. The dependency graph — computed, not drawn

Solution sets over d = 1..12, obtained by **evaluating** each predicate (never by quoting a root):

| selector | S |
|---|---|
| 1 rotation self-duality | **{3}** |
| 2 Hodge duality | **{3}** |
| 3 polarisation equality | **{3}** |
| 4a D96^d supplies a 3-dim irrep | {3, 4, …, 12} |
| 4b D96^d: the vector is the largest irrep | {1, 2, 3} |
| 5 clock law ρ^(1/d) | {1, …, 12} — **every** d |

**A implies B exactly when S(A) ⊆ S(B)** — the containment *is* the dependency, so the graph is derived. It
gives: 1 ≡ 2 ≡ 3 (equivalent); 1 ⟹ 4a, 1 ⟹ 4b, 1 ⟹ 5; and **nothing implies 1**.

**The representation conjunction.** 4a ∧ 4b = {3,4,…} ∩ {1,2,3} = **{3}** — it *reproduces the root exactly*,
which is precisely why this argument has looked like a second mechanism in G_033/G_041. But the root implies
both halves, so under the audit's own rule the conjunction is redundant too.

## 3. The classification

| status | selector | why |
|---|---|---|
| **INDEPENDENT** | 1 rotation self-duality | no other selector implies it — **the root** |
| **REDUNDANT** | 2 Hodge duality | same solution set as 1 — one mechanism, stated twice |
| **REDUNDANT** | 3 polarisation equality | same solution set as 1 — **and the same equation**, by the dim so(d) identity |
| **DERIVED FROM** | 4a supplies a 3-dim irrep | implied by 1; admits d ≥ 3 alone |
| **DERIVED FROM** | 4b the vector is the largest irrep | implied by 1; admits d ≤ 3 alone |
| **REDUNDANT** | 5 clock law ρ^(1/d) | holds at every d — a law, not a selector |

**Independent root mechanisms: 1.**

## 4. The accidents belong to *different* dimensions — checked, not assumed

The audit also tests the opposite reading, i.e. whether "dimensional accidents" are all one accident. **They are
not:**

| coincidence | dimension | evidence |
|---|---|---|
| bivectors collapse to a **scalar** | **d = 2** | dim Λ² = 1 |
| bivectors **are** the vectors (the cross product) | **d = 3** | dim Λ² = dim V = 3 |
| bivectors split **self-dual / anti-self-dual** | **d = 4** | dim Λ² = 6 = 3 + 3 (even Λ² also at d = 5, 8, 9, 12) |
| the cross product reappears | **d = 7** | Hurwitz (octonions) — *cited, not computed here* |

So the family of special dimensions is real and each membership differs — **but only one membership selects
d = 3, and it does so once.**

## 5. The clock is not a selector

Input 5 holds at **every** dimension, so it constrains nothing about *which* d is realized. Its role is the
opposite one: `ρ^(1/d)` is the law that makes the choice **observable** (G_041: 129 415.634 / 86 277.089 /
64 707.817 s/day at d = 2/3/4). It is **REDUNDANT as a selector and INDEPENDENT in role** — the only entry on
the list a measurement could read.

## 6. Refinement owed to G_041

**G_041 called the ε/Hodge accident and the polarisation match "two independent accidents". They are ONE
condition reached by two derivations.** The number of **independent** reasons for d = 3 is therefore **1, not
2** — which is the honest correction, and it makes the case *cleaner* but *thinner* at the same time.

* G_041's doc now carries this refinement note; `SubstrateDimensionAudit.RefinementFromG042()` carries it in
  code and prints it in G_041's own report.
* **`SubstrateDimensionAudit`'s `Selectors()` statuses are NOT changed**: each of the two accidents still
  genuinely *selects* d = 3 (status DERIVED is correct per selector). What was wrong was the word
  **"independent"**, and that is what is corrected.
* **G_041's BOUNDARY verdict stands unchanged.** d = 4 is still not excluded by the representations — only by
  this single accident, by the polarisation mismatch, by the clock exponent and by cost. The correction concerns
  the *count of independent reasons*, not the verdict.

**Per the project's reclassification rule, the refinement is recorded in the new doc *and* in the canonical
surfaces** (the G_041 doc, the G_041 registry narrative in `TheoryRegistry.cs`, and `NewChat_Start.md`).

## 7. Caveats

**(a) Two of the five inputs are the same expression.** Input 1 (`d(d−1)/2 = d`) and input 2 (`dim Λ² = dim V`)
are literally the same equality, since `dim Λ² = d(d−1)/2`. They are not merely equivalent as selectors; they are
the same formula.

**(b) "One mechanism" does not mean "one derivation".** The tensor-algebra route (Λ² dual to V) and the
little-group route (both massless counts equal to a tensor dimension minus one) are different physical
arguments that land on the same condition. The audit's claim is about *logical independence*, not about
provenance.

**(c) The range.** Solution sets are computed over d = 1..12. The three geometric selectors are single-rooted at
{0, 3} algebraically, and the audit's range check is consistent with that; the representation selectors are
computed from the exact irrep-dimension spectrum (cheap at every d — the *group* construction is not, which is
why the ladder of G_041 stops at 6 while the spectra here go to 12).

**(d) The d = 7 cross product is cited, not computed.** Hurwitz's theorem is a deep result; the audit marks it
as a citation rather than pretending to a computation. It is included only to make the *point* that special
dimensions form a real family with different memberships.

## 8. Classification

**Registry:** added to the G_035 classification registry as **`ClockOnly`** → **SURVIVES** (group theory, tensor
algebra and the g₀₀ clock law), triaged `ScanDetectsIt: false`. Counts become **29 / 11 / 3 of 43**; the
boundary index is unchanged; no prior classification changed.

**No verdict changed.** G_041 is refined (the count of independent reasons), not reclassified. G_033, E_003,
E_004 and G_016b are unchanged inputs. The D_040 registry is untouched; no canonical claim, value or equation
changes; no new primitive is added.

**Scanner side-effect, recorded:** G_033's live classifier now reads **24** substrate suites (41 classified in
total), because this suite states the ring constant explicitly so that its dependence on the D96^d family is
visible to the scan rather than hidden in prose.
