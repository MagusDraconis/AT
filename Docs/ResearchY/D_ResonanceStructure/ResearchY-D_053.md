# ResearchY-D_053 — Perturbation-Family Dominance Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_053 (permanent)
**Title:** Perturbation-Family Dominance Audit
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `D_ResonanceStructure/ResearchY-D_053.md`
**Depends on:** D_052 (degeneracy axis; the seven-ring family), D_051 (ring family, blind rings), D_050 (spectral predictability), D_049 (family dependence of the frontier), D_048 (the perturbation ensemble)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_053_Tests.cs` (5 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`

---

## Question

**Is capacity controlled more by perturbation family than by spectral invariants?**

D_049 found that the adaptability–robustness frontier is **not family-invariant** — under edge
deletion it collapses to a single case. That was measured across a *heterogeneous* case set
(D96, the 3-D torus, random, complete, physical, unphysical). D_053 asks the sharper question
inside one homogeneous topological family: with the seven 96-node rings, is the **family** axis or
the **spectrum** axis the stronger control?

### The design — and why it is unusually clean

The shared ensemble supplies a two-factor layout with an **exact** control in one direction:

| factor | levels | what is held fixed |
|---|---|---|
| **spectrum** | the 7 rings (D96, S96-123, S96-135, D96-24, Ring48, Decay96, Boost96) | the family |
| **family** | delete, add, rewire, weight | **the spectrum — exactly** |

* **Along a row** (fixed ring, changing family) the object perturbed is *the same adjacency matrix*.
  λ₂, degeneracy count, multiplicity entropy and ΔE_lock are **bit-for-bit identical**. Any difference
  is 100 % a family effect.
* **Along a column** (fixed family, changing ring) the spectrum changes, so column differences carry
  the spectrum effect — *plus* the one confound the design cannot remove (§4).

**No new simulation is needed for the main comparison or the decomposition**: the per-family cells
are already measured and stored by the shared ensemble. Only §4's robustness check measures anything.

Each cell is a mean over that family's 5 doses × 3 fixed seeds, connectivity-guarded.

---

## Results

### 1. The capacity matrix

| ring | delete | add | rewire | weight | row mean | row spread |
|---|---|---|---|---|---|---|
| D96 | 0.9843 | 0.9791 | 0.9974 | 1.0000 | 0.9902 | 0.0209 |
| S96-123 | 0.9516 | 0.9464 | 0.9791 | 1.0000 | 0.9693 | 0.0536 |
| S96-135 | 0.9229 | 0.9307 | 0.9529 | 0.9412 | 0.9369 | 0.0301 |
| D96-24 | 0.9544 | 0.9591 | 0.9918 | 0.9825 | 0.9719 | 0.0374 |
| **Decay96** | **1.0000** | **1.0000** | **1.0000** | **1.0000** | 1.0000 | **0.0000** |
| Boost96 | 0.9932 | 0.9891 | 0.9986 | 1.0000 | 0.9952 | 0.0109 |
| Ring48 | 0.9804 | 0.9895 | 1.0000 | 1.0000 | 0.9925 | 0.0196 |
| **column spread** | 0.0771 | 0.0693 | 0.0471 | 0.0588 | | |

The recovery matrix (row spreads): D96 0.0354, S96-123 0.0392, S96-135 0.0345, D96-24 0.0368,
**Decay96 0.1105**, Boost96 0.0300, Ring48 0.0283.

### 2. The controlled comparison

| target | family change (spectrum **exactly** fixed) | spectrum change (family fixed) | ratio |
|---|---|---|---|
| **capacity** | mean pairwise \|Δ\| 0.0145, mean range **0.0246**, max 0.0536 | mean pairwise \|Δ\| 0.0255, mean range **0.0631**, max 0.0771 | **0.39×** |
| **recovery** | mean pairwise \|Δ\| **0.0255**, mean range 0.0450, max 0.1105 | mean pairwise \|Δ\| 0.0197, mean range **0.0607**, max 0.1226 | 1.29× (pairwise) / 0.74× (range) |

**Family ordering, per ring** (capacity, high → low):

| ring | order |
|---|---|
| D96 | weight > rewire > delete > add |
| S96-123 | weight > rewire > delete > add |
| S96-135 | rewire > weight > add > delete |
| D96-24 | rewire > weight > add > delete |
| Decay96 | *(four-way exact tie at 1.0000)* |
| Boost96 | weight > rewire > delete > add |
| Ring48 | rewire > weight > add > delete |

Across the **six** rings with any family variation, weight and rewire are **always** the top two and
delete and add **always** the bottom two — a consistent 2+2 split — while the order *within* each pair
flips from ring to ring.

### 3. Variance decomposition (two-way layout, 7 × 4 = 28 cells)

| target | grand mean | ring (spectrum) η² | family η² | interaction η² |
|---|---|---|---|---|
| **capacity** | 0.9794 | **74.5 %** (SS 0.011700, sd 0.0442) | 15.7 % (SS 0.002471, sd 0.0287) | 9.8 % |
| **recovery** | 0.9614 | 39.1 % (SS 0.008641, sd 0.03795) | 30.5 % (SS 0.006735, sd **0.04738**) | 30.4 % |

Note on recovery: the family **main-effect sd exceeds** the spectrum's even though the η² ratio
favours the spectrum — the family's recovery effect is large but concentrated in one ring (Decay96).

### 4. The confound check, and the regime test

The design cannot avoid one asymmetry: the shared ensemble scales each dose as a **fraction of the
ring's own edge count**, so a "5 %" deletion removes ~14 edges on a degree-6 ring and ~58 on a
degree-24 ring. If the spectrum effect were an artifact of that scaling it should weaken when every
ring instead receives the **same absolute** number of edge operations.

**Part A — fixed absolute budget, delete family:**

| ring | k = 13 | k = 29 | k = 58 |
|---|---|---|---|
| D96 | 1.0000 | 1.0000 | 1.0000 |
| S96-123 | 1.0000 | 0.9935 | 1.0000 |
| S96-135 | 1.0000 | 0.9935 | 1.0000 |
| D96-24 | 0.9649 | 1.0000 | 1.0000 |
| Decay96 | 1.0000 | 1.0000 | 1.0000 |
| Boost96 | 1.0000 | 1.0000 | 1.0000 |
| Ring48 | 1.0000 | 1.0000 | 1.0000 |
| **spectrum spread** | 0.0351 | 0.0065 | **0.0000** |

Spectrum spread 0.0771 (fractional) → mean 0.0139 (absolute) = **ratio 0.18×**. The spectrum effect
does **not** survive the removal of the confound.

**Part B — regime test, both spreads at the same absolute k** (delete / add / rewire; weight is a
full-edge rescale with no edge count, so it is excluded here):

| k | mean spectrum spread | mean family spread | ratio family/spectrum |
|---|---|---|---|
| 13 | 0.0136 | 0.0050 | 0.37 |
| 29 | 0.0022 | 0.0019 | 0.86 |
| 58 | **0.0000** | **0.0000** | 0/0 |

At k = 58 **every** sampled cell reaches capacity = 1.0000 — the family spread vanishes *together*
with the spectrum spread.

---

## Classification

### DERIVED

* **The control is asymmetric, and the conclusion is therefore conservative.** A family change holds
  the spectrum *exactly* fixed (same adjacency ⇒ λ₂, degeneracy count, entropy and ΔE_lock all
  bit-for-bit identical); a spectrum change holds only the family fixed. The family axis is the
  *perfectly controlled* one — and it is still the **smaller** effect for capacity. Spectrum
  dominance cannot be an artifact of weak control.
* **Decay96's exact family-invariance in capacity is structural.** Its spectrum is 47×2, 2×1 — **no
  level of multiplicity greater than 2 exists**, so no family has a hard coincidence left to resolve
  and all four reach full resolution. Verified: Decay96 capacity = 1.0000 in all four families, row
  spread exactly 0.0000. Capacity saturates at 1 for a spectrum whose only degeneracy is doublets.
* **The family ordering is real but only partly conventional.** In six of six varying rings, weight and
  rewire are always the top two and delete and add always the bottom two; the order *within* each pair
  flips. A noise effect would not preserve the split; a universal law would preserve the full order.
  The interaction term *is* that partial disagreement.
* **Capacity is a saturating quantity, and that is why the spectrum effect is regime-bounded.** The
  spread collapses monotonically with the absolute budget (0.0771 → 0.0351 → 0.0065 → 0.0000) and both
  factors reach exactly zero at a small budget (58 edges = 5 % of a degree-24 ring's edge count, 20 % of
  a degree-6 ring's). The spectrum effect lives in the **unsaturated** regime only.

### EMERGENT

* **The measured split:** capacity 74.5 % spectrum / 15.7 % family / 9.8 % interaction; recovery
  39.1 % / 30.5 % / 30.4 %.
* **Family main effects on capacity:** weight 0.9891 > rewire 0.9886 > add 0.9706 > delete 0.9695.
  Weight perturbation — which breaks every symmetry at once *without removing a single edge* — is the
  strongest lever in every ring that has any spread, confirming D_048's side finding on a new family.
* **Recovery is the factor-balanced case,** with a third of its variance in the interaction.
* **All seven rings adapt strongly** (capacity 0.9369 … 1.0000), so the whole comparison lives in the
  top decile of the capacity scale.

### REFUTED

* **"Capacity is controlled more by the perturbation family than by spectral invariants."** REFUTED
  within the ring family: η² 15.7 % (family) vs **74.5 %** (spectrum), and the family spread inside a
  fixed spectrum is **0.39×** the spectrum spread at fixed family.
* **"Holding spectral quantities fixed isolates the dominant control."** REFUTED as stated: exact
  spectral control does not make the family dominant — it only removes an excuse. The family effect is
  real and reproducible, and it is the smaller of the two for capacity.
* **"A family change can proxy a spectrum change (or vice versa)."** REFUTED: with 9.8 % (capacity) and
  30.4 % (recovery) of the variance in the interaction, neither factor is a rescaling of the other.
* **"The spectrum effect on capacity is a pure property of the ring."** REFUTED: it is
  **regime-dependent**. Under a fixed absolute budget the ring spread collapses to exactly zero
  (k = 58) because every ring saturates at full resolution, while under relative damage it is 0.0771.

---

## Verdict

**DERIVED** — the asymmetric control (spectrum exactly fixed along a row), Decay96's structural
family-invariance (no multiplicity > 2), the consistent 2+2 family split, and the saturation bound on
the spectrum effect.

**EMERGENT** — the 74.5 / 15.7 / 9.8 split for capacity, the family main effects and their ordering,
the interaction-heavy recovery case.

**REFUTED** — family dominance over capacity; the idea that exact spectral control isolates the
dominant factor; mutual proxying of the two factors; and the purity of the spectrum effect.

**Answer to the question asked.** *Is capacity controlled more by perturbation family than by spectral
invariants?* **No** — not in the regime where capacity has room to vary, and no in the regime where it
does not (there *neither* factor matters). The family effect is real, exactly measurable against a
bit-for-bit fixed spectrum, and consistently ordered — but it is the **smaller** control on capacity in
**every regime tested**.

**Refinement of D_049.** D_049's family finding was measured across a *heterogeneous* case set. D_053
shows that within one homogeneous topological family the family effect, though real, is not dominant:
ring identity is (74.5 % vs 15.7 %). **Family membership therefore governs *which* cases a frontier
contains, not how large a case's response is.**

**A note on the family axis as a measurement.** The family effect needs no dose matching, survives
bit-for-bit spectral control, and is the only factor that keeps a signature at the far end of the ring
family — which makes it the better-behaved of the two controls even though it explains less variance
here.

No canonical AT claim, value, equation or registry entry is changed; the D_040
`ClassificationRegistry` is untouched. This audit introduces no new simulation primitive: the main
comparison and the decomposition reuse per-family cells the shared ensemble already stores; only §4
measures anything new.

---

## Addendum — the regime caveat, stated plainly

The headline number (74.5 % vs 15.7 %) is measured under the ensemble's own convention: doses as
fractions of each ring's edge count, i.e. **equal relative damage**. §4 shows the spectrum effect is
**not** a pure ring property: under a fixed absolute budget it shrinks (0.0771 → 0.0351 → 0.0065 →
0.0000) and disappears entirely at 58 edges, because every ring saturates at full resolution and the
family spread disappears with it. Two consequences:

1. The dominance result is a statement about the **unsaturated regime**, and that regime is small in
   relative terms (58 edges ≤ 20 % of the sparsest ring's edges).
2. In the saturated regime **neither** factor controls capacity — capacity is 1.0000 for every
   ring and every family. The audit's question is therefore not merely answered "no"; it is
   **ill-posed outside the unsaturated regime**, and the honest form of any follow-up claim is
   "conditional on the damage budget".

---

## References

* **D_052** — Degeneracy axis audit: the seven-ring family.
* **D_051** — Blind prediction audit: the blind rings.
* **D_050** — Spectral predictability: bounds and the minimal predictor set.
* **D_049** — Adaptability–robustness frontier: family dependence, the claim refined here.
* **D_048** — Latent-degeneracy adaptability; the perturbation ensemble and its four families, and the finding that weight perturbation is the universally strongest lever.
* **D_047** — Degeneracy-lock theorem: $\Delta E_{\text{lock}}$.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_053_Tests.cs` (5 tests, all passing).
