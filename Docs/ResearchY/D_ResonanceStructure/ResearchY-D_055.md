# ResearchY-D_055 — Pair1-47 Anomaly Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_055 (permanent)
**Title:** Pair1-47 Anomaly Audit
**Status:** COMPLETE
**Date:** 2026-09-11
**File:** `D_ResonanceStructure/ResearchY-D_055.md`
**Depends on:** D_054 (the anomaly and its blind ring set), D_052 (degeneracy axis), D_050 (bounds), D_048 (the perturbation ensemble), D_047 (degeneracy-lock theorem)
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_055_Tests.cs` (5 tests)
**Shared machinery:** `AT.Tests/Shared/AdaptabilityAudit.cs`

---

## Question

**Why is Pair1-47 — the ring with offsets ±1 and ±47 — the only ring in the audited family whose
capacity collapses (0.42089 against 0.93693 … 1.00000 for every other ring)?**

The brief: compare Pair1-47 with the healthy rings on the full spectrum, multiplicity structure,
spectral spacing, eigenvector localization, participation ratio and symmetry classes, and identify
the **first property uniquely possessed by Pair1-47**, rejecting every property a healthy ring also
has.

**Healthy comparison set = all 12 other audited rings** — D_051/D_052/D_053's seven rings plus
D_054's five other edge-shape rings. The wider set is required, not cosmetic: **E1 alone kills two
otherwise plausible candidates** (it has a 4.85× smaller λ₂ and half the degree while being perfectly
healthy at capacity 0.98598).

---

## Results

### 1. The property, and its exact arithmetic cause

| ring | A₀ (distinct) | headroom | max mult | share of largest level | ΔE_lock | pattern |
|---|---|---|---|---|---|---|
| **Pair1-47** | **25** | **71** | **50** | **52.1 %** | **2.3552** | **1×50, 22×2, 2×1** |
| D96 | 45 | 51 | 6 | 6.2 % | 0.8023 | 1×6, 1×5, 42×2, 1×1 |
| S96-123 | 45 | 51 | 6 | 6.2 % | 0.8023 | 1×6, 1×5, 42×2, 1×1 |
| S96-135 | 45 | 51 | 10 | 10.4 % | 0.8464 | 1×10, 42×2, 2×1 |
| Ring48 | 45 | 51 | 6 | 6.2 % | 0.8023 | 1×6, 1×5, 42×2, 1×1 |
| Boost96 | 47 | 49 | 5 | 5.2 % | 0.7337 | 1×5, 45×2, 1×1 |
| Decay96 | 49 | 47 | 2 | 2.1 % | 0.6787 | 47×2, 2×1 |
| D96-24 | 39 | 57 | 12 | 12.5 % | 1.1052 | 1×12, 1×11, 36×2, 1×1 |

**Exactly one ring has a level holding more than half its spectrum** — multiplicity 50 of 96 (52.1 %).
Every healthy ring's largest level holds at most 12 of 96 (12.5 %), and the median holds 6.

**The exact reason.** For a symmetric circulant, $\lambda_k = 2\sum_{d\in S}(1-\cos(2\pi kd/N))$. With
$S = \{1, 47\}$ and $47 = N/2 - 1$:

$$\cos(2\pi k\cdot 47/96) = \cos(\pi k - \theta_k) = (-1)^k\cos\theta_k$$

so with $\theta_k = 2\pi k/96$:

* $k$ **even**: $\lambda_k = 2[(1-\cos\theta) + (1-\cos\theta)] = 4(1-\cos\theta_k)$;
* $k$ **odd**: $\lambda_k = 2[(1-\cos\theta) + (1+\cos\theta)] = \mathbf{4}$ **exactly, for every odd $k$**.

There are **48** odd $k$ in 1…95, and $k = 24$ and $k = 72$ also give $\lambda = 4$, so the level has
multiplicity **50**. In general an offset pair $\{\pm1, \pm d\}$ produces this level exactly when
**$d \equiv \pm1 \pmod{N/2}$** — the second offset is congruent to the fundamental step modulo the
half-period. Verified: 48 odd modes, largest $|\lambda_k - 4| = 0$, and 50 eigenvalues within 1e-9 of
4 in the *actual* Laplacian spectrum.

### 2. Rejecting every shared property

Is Pair1-47's value **outside the whole interval** spanned by the 12 healthy rings?

| property | Pair1-47 | healthy min … max | outside? | verdict |
|---|---|---|---|---|
| λ₂ (algebraic connectivity) | 0.0342 | 0.0043 … 5.4042 | no | **rejected** — independent |
| min positive spectral gap | 0.0342 | 0.0002 … 0.0211 | **yes** | separates — independent (see below) |
| mean distinct-level spacing | 0.3463 | 0.0850 … 1.4474 | no | **rejected** — independent |
| degree | 4.0 | 2.0 … 24.0 | no | **rejected** — independent |
| edges \|E\| | 192 | 96 … 1152 | no | **rejected** — independent |
| ΔE_lock | 2.3552 | 0.6787 … 1.1052 | yes | separates — **restatement** |
| headroom N − A₀ | 71 | 47 … 57 | yes | separates — **restatement** |
| distinct count A₀ | 25 | 39 … 49 | yes | separates — **restatement** |
| largest multiplicity | 50 | 2 … 12 | yes | separates — **restatement** |
| largest level's SHARE | 0.5208 | 0.0208 … 0.1250 | yes | separates — **restatement** |

**Rejected in principle, not by measurement:**

* **EIGENVECTOR LOCALIZATION / PARTICIPATION RATIO.** Every ring here is a circulant, so every
  Laplacian eigenvector is a Fourier mode with $|v_i| = 1/\sqrt{N}$; the participation ratio is
  **exactly N = 96** for every mode of every ring — verified with the exact Fourier modes as
  eigenvectors. Localization cannot separate any two members of this family, whatever is measured.
* **SYMMETRY CLASS.** All eight opening rings are symmetric circulants: 96 translations, 96
  reflections, trivial vertex-stabilizers, one orbit — the same dihedral group of order 2N. Classical
  "symmetry-protected degeneracy" therefore cannot separate Pair1-47; what differs is **how much** of
  the spectrum one level holds, not whether a symmetry exists.

**The one independent separator, reported rather than hidden.** The min positive gap *does* separate
Pair1-47 (0.034221 against 0.0002 … 0.0211) — but the witness shows it is the spacing between the
level at 7.965779 (k = 46) and the singleton at 8.000000 (k = 48), i.e. **in the middle of the
spectrum, not at the bottom**, and the number merely coincides with λ₂. It is one scalar with no
route to the mechanism below. **"Unusual spacing statistic" is a true but non-explanatory separator;
the multiplicity structure is both a separator and the cause.**

### 3. The mechanism — a rank budget against the multiplicity structure

**Deleting or adding an edge subtracts a RANK-2 matrix**, and a rank-*r* perturbation acts on an
*m*-fold eigenspace as an $m\times m$ matrix of rank ≤ *r*, which has **at most *r* + 1 distinct
eigenvalues**. Hence a level of multiplicity *m* yields at most $\min(m-1, r)$ new distinct
eigenvalues:

$$\boxed{\ \Delta A \le \sum_i \min(m_i - 1,\ r)\ } \qquad r \le 2k$$

Measured, delete family:

| ring | k | rank 2k | ceiling Σ min(m−1,2k) | headroom | **ceiling capacity** | **measured** |
|---|---|---|---|---|---|---|
| Pair1-47 | 1 | 2 | 24 | 71 | **0.3380** | 0.3239 |
| Pair1-47 | 3 | 6 | 28 | 71 | 0.3944 | 0.3521 |
| Pair1-47 | 10 | 20 | 42 | 71 | 0.5915 | 0.4507 |
| Pair1-47 | 19 | 38 | 60 | 71 | 0.8451 | 0.5728 |
| D96 | 1 | 2 | 46 | 51 | **0.9020** | 0.8562 |
| D96 | 3 | 6 | 51 | 51 | 1.0000 | 0.9281 |
| D96 | 10 | 20 | 51 | 51 | 1.0000 | 0.9804 |
| D96 | 19 | 38 | 51 | 51 | 1.0000 | 1.0000 |
| S96-135 | 1 | 2 | 44 | 51 | 0.8627 | 0.8431 |
| S96-135 | 3 | 6 | 48 | 51 | 0.9412 | 0.8824 |
| S96-135 | 10 | 20 | 51 | 51 | 1.0000 | 0.9804 |
| S96-135 | 19 | 38 | 51 | 51 | 1.0000 | 1.0000 |

The ceiling is a genuine upper bound that the measurement approaches from below, and it **predicts
the collapse from the multiplicity structure alone**: at one edge deletion Pair1-47's ceiling is
0.3380 against D96's 0.9020.

**The decisive single-operation test.** One deletion is rank 2 and splits **each** degenerate level
independently, so D96 gains ≤ 42 + 2 + 2 = **46** distinct eigenvalues (its 42 doublets contribute 42
*at once*) while Pair1-47 gains ≤ 2 + 22 = **24** — its 49-slot level contributes at most **2**. The
bound holds for every ring (verified), and the measured gains are 48 (D96) and 23 (Pair1-47).

**The healthy rings are rank-efficient**: their headroom is built from *many small levels*, which one
edge operation releases together. Pair1-47's headroom is built from *one level*, which it cannot.
Capacity therefore depends on the multiplicity **distribution**, not on the multiplicity count or the
headroom — which is exactly why D_052's degeneracy count and D_050's near-gap density cannot see the
anomaly.

### 4. A defect in the shared perturbator — found, quantified, not exploited

The `weight` family should multiply every edge by an **independent** random factor, which is a
full-rank perturbation and should make every eigenvalue simple. It does not. The shared generator is
an LCG $x \leftarrow 1664525\,x + 1013904223 \pmod{2^{32}}$, and the weight family reads its sign from
the **lowest bit**. Both coefficients are odd, so $(a x + c) \bmod 2 = (x+1) \bmod 2$: **the coin
alternates deterministically for every seed.** Reproduced exactly:

```
seed  11: 0 1 0 1 0 1 0 1 0 1 0 1
seed  22: 1 0 1 0 1 0 1 0 1 0 1 0
seed  33: 0 1 0 1 0 1 0 1 0 1 0 1
```

Consequences, all measured:

* the weight family produces exactly **two** distinct weight values — Pair1-47: `0.98 × 96, 1.02 × 96`;
  D96: `0.98 × 288, 1.02 × 288`; Decay96: 12 distinct values;
* the reweighted graph is neither circulant nor shift-symmetric (both checks **False**) — so it is not
  a symmetry being preserved, but a **fixed pattern** being applied;
* its effect is **spectrum-dependent**, which is precisely what an independent reweighting must not
  be: D96 still reaches A₁ = 96, while Pair1-47 **stalls at 51 with its λ = 4 level intact**.

**Quantified with a corrected coin** (sign drawn from the high bits, an independent sign per edge,
implemented **locally** so no stored number is touched):

| ring | weight cap (shared, periodic) | weight cap (corrected) | ensemble mean: shared → corrected |
|---|---|---|---|
| **Pair1-47** | **0.3662** | **0.9812** | **0.4209 → 0.5746** |
| D96 | 1.0000 | 1.0000 | 0.9902 → 0.9902 |
| S96-123 | 1.0000 | 1.0000 | 0.9693 → 0.9693 |
| S96-135 | 0.9412 | 1.0000 | 0.9369 → 0.9516 |
| Ring48 | 1.0000 | 1.0000 | 0.9925 → 0.9925 |
| Boost96 | 1.0000 | 1.0000 | 0.9952 → 0.9952 |
| Decay96 | 1.0000 | 1.0000 | 1.0000 → 1.0000 |
| D96-24 | 0.9825 | 1.0000 | 0.9719 → 0.9763 |

**The defect depresses the anomaly by about a third and does not create it.** Even with a corrected
coin Pair1-47 sits at 0.5746 against 0.94 … 1.00 for every healthy ring, and the three edge-count
families — untouched by the defect — still show 0.41 / 0.40 / 0.50.

**Scope.** The defect affects the **weight family only**: delete, add and rewire draw their target from
the full 32-bit value, whose low-order structure does not matter, so their numbers stand. Every audit
from D_048 onward reports weight as the *strongest* family, and that direction is unaffected (a
systematic reweighting is still a real perturbation); what is unreliable is any fine structure
attributed to weight's "full-rank randomness". **This audit does not silently change the shared
generator** — that would invalidate stored numbers in D_048–D_054 — it reproduces the defect and
recommends it as the next fix.

---

## Classification

### DERIVED

* **The property is the multiplicity structure: a single degenerate level holding 52.1 % of the
  spectrum** (multiplicity 50 of 96). Nothing else separates Pair1-47 — λ₂, both spacing statistics,
  degree and edge count are all shared with a healthy ring.
* **The exact arithmetic cause:** $47 = N/2 - 1$, and in general $\{\pm1, \pm d\}$ gives this level
  precisely when $d \equiv \pm1 \pmod{N/2}$, because
  $\cos(2\pi k \cdot 47/96) = (-1)^k \cos(2\pi k/96)$ makes the two offsets' contributions exactly
  complementary for every odd $k$.
* **A₀, headroom and ΔE_lock separate only as restatements** of that same multiplicity multiset. The
  inversion is the anomaly in one line: Pair1-47 has the **largest** headroom (71) *and* the largest
  ΔE_lock (2.3552) in the family, and the **lowest** capacity — more locked entropy and more room,
  less collected.
* **Why — the rank budget:** $\Delta A \le \sum_i \min(m_i-1, r)$ with $r \le 2k$, because a rank-*r*
  perturbation acts on an *m*-fold eigenspace as a rank-≤ *r* matrix, giving at most *r* + 1 distinct
  eigenvalues. A spectrum whose headroom sits in one huge level is a **rank trap**.
* **The healthy rings are rank-efficient**: their headroom is many small levels, released together by
  one operation. Capacity depends on the multiplicity **distribution**, not its count or the headroom —
  which is why the degeneracy count and the near-gap density cannot see the anomaly.

### EMERGENT

* **The measured collapse and its magnitude:** 0.42089 overall; per family 0.4103 (delete), 0.4038
  (add), 0.5033 (rewire), 0.3662 (weight). The rank ceiling reproduces the ordering and the size of
  the shortfall.
* **The perturbator defect** (see §4): the deterministic alternating coin, the two-valued reweighting,
  the spectrum-dependent effect, and the corrected-coin comparison that isolates its contribution
  (0.4209 → 0.5746).

### REFUTED

* **"Eigenvector localization or a small participation ratio explains Pair1-47."** REFUTED **in
  principle** — every circulant eigenvector is a Fourier mode, PR = N exactly, for every ring.
* **"Pair1-47's symmetry class is different."** REFUTED — identical dihedral group of order 2N, one
  orbit, vertex-transitive.
* **"A small spectral gap, an unusual spacing statistic or a low degree distinguishes it."** PARTLY
  REFUTED and reported as such: the min gap **does** separate it (witness: 7.965779 ↔ 8.000000, mid
  spectrum), but it is a single scalar with no route to the mechanism; λ₂, the mean spacing, the degree
  and the edge count are all shared with a healthy ring (E1 has a 4.85× smaller λ₂ and half the degree).
* **"Pair1-47 is anomalous because it has many degenerate levels."** REFUTED — it has the **fewest**
  degenerate groups in the family (23, against D96's 44). Degeneracy *count* is anti-correlated with
  the collapse.
* **"The collapse is an artifact of the perturbator defect."** REFUTED — with a corrected coin the
  anomaly survives (0.5746 vs 0.94 … 1.00).

---

## Verdict

**DERIVED** — the dominant-level property, its half-period congruence cause, the rank-budget ceiling
$\Delta A \le \sum_i \min(m_i-1, r)$, and the rank-efficiency of the healthy rings.

**EMERGENT** — the measured magnitudes, and the perturbator defect with its quantified contribution.

**REFUTED** — localization, participation ratio, symmetry class, λ₂, mean spacing, degree and edge
count as candidates; "many degenerate levels" as the cause; and the defect as the source of the
anomaly.

**Summary answer.** The first property uniquely possessed by Pair1-47 is **a single degenerate level
holding more than half its spectrum**, produced exactly by the half-period congruence of its offsets
($47 \equiv \pm1 \bmod 48$). It collapses capacity because a low-rank perturbation can release that
level's 49 headroom slots only a couple at a time, while the healthy rings' headroom sits in dozens of
small levels that a single edge operation releases together. ΔE_lock, headroom and A₀ separate the ring
too, but only as restatements of that one multiplicity structure; localization and symmetry fail to
separate it at all.

No canonical AT claim, value, equation or registry entry is changed; the D_040 `ClassificationRegistry`
is untouched. No new simulation primitive is added to the shared machinery — the corrected coin is
local to this audit and used only to bound the defect.

---

## References

* **D_054** — Low-energy spectral edge audit: the anomaly (capacity 0.42089) and the held-out ring set.
* **D_052** — Degeneracy axis: why the degeneracy *count* cannot see a multiplicity-distribution effect.
* **D_050** — Spectral predictability: the derived bounds.
* **D_048** — Latent-degeneracy adaptability; the four-family perturbation ensemble and its generator.
* **D_047** — Degeneracy-lock theorem: $\Delta E_{\text{lock}} = \tfrac{1}{N}\sum_{m>1} m \ln m$.
* **D_040** — `ClassificationRegistry` (canonical classification guard; untouched).
* **Test suite** — `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_055_Tests.cs` (5 tests, all passing).
