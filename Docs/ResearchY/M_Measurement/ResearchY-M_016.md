# ResearchY-M_016 — Criterion-Independence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** M — Measurement Origin
**ID:** ResearchY-M_016 (permanent)
**Title:** Criterion-Independence Audit
**Status:** COMPLETE
**Date:** 2026-09-08
**File:** `M_Measurement/ResearchY-M_016.md`
**Depends on:** ResearchY-M_014 (the four score criteria), M_015 (score robustness),
D_020 (3-family window), D_029 (zero-defect set), D_030 (octave rung), D_040
(classification registry), D_041 (D96 spectrum)
**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_016_Tests.cs`

---

## Purpose

**Are the four M_014 score criteria independent over N = 16..512?**
A = 0 unpaired modes; B = 3 families ∧ span < 8; C = 6|N; D = N = 3·2^k.
M_014 built Score(N) = A + B + C + D and found N = 96 to be the unique Score-4
maximizer. M_015 showed that verdict is robust to perturbing any single criterion.
M_016 asks the deeper question: do the four criteria carry *independent* information, or
are some of them redundant — i.e., does the score over-count one underlying physical
fact? For the population of all 497 rings N = 16..512 the audit computes: marginal and
joint entropies, total correlation, the pairwise mutual-information matrix, the phi
(Pearson-on-bits) correlation matrix, PCA on the standardized 4-bit criteria, the
conditional entropy H(X_i | others) and its unique-information fraction, and the
predictability of each criterion from the other three. It then identifies which criterion
(or which pair) actually selects N = 96. Output options: INDEPENDENT / PARTIALLY
REDUNDANT / REDUNDANT. No new primitive; canonical AT unchanged.

---

## 1. Marginal structure over N = 16..512 (497 rings)

| Criterion | Rings where true | P(true) | H (bits) |
|---|---|---|---|
| A — 0 unpaired | 354 | 0.7123 | 0.8658 |
| B — 3 families ∧ span < 8 | 61 | 0.1227 | 0.5372 |
| C — 6 \| N | 83 | 0.1670 | 0.6508 |
| D — N = 3·2^k | 5 | 0.0101 | 0.0812 |
| ΣH | | | **2.1349** |

Joint entropy H(A,B,C,D) = **2.017 bits**. Total correlation = ΣH − H_joint =
**0.1179 bits = 5.5% of the entropy budget**. If the criteria were fully redundant the
joint entropy would collapse toward a single variable; if fully independent it would equal
ΣH. The measured 5.5% redundancy is small but nonzero.

---

## 2. Pairwise mutual information and correlation

| Pair | I (bits) | phi (Pearson-on-bits) |
|---|---|---|
| A–B | 0.0000 | 0.007 |
| A–C | **0.0912** | **0.285** |
| A–D | 0.0050 | 0.064 |
| B–C | 0.0001 | 0.013 |
| B–D | 0.0004 | 0.024 |
| C–D | 0.0263 | 0.225 |

B is **statistically near-independent** of every other criterion (I(B;·) ≤ 0.0004 bit).
The only non-negligible statistical couplings are A–C (phi 0.285) and C–D (phi 0.225).

---

## 3. Deterministic inclusion chain: D ⊆ C ⊆ A

Over the full scanned range the criteria are not merely statistically coupled — they
carry a **deterministic nesting**:

- **D ⊆ A**: every seed-3 rung 3·2^k in [16,512] has zero unpaired modes (conjunction
  A∧D = D, 5 rings).
- **D ⊆ C**: every rung is divisible by 6 (C∧D = D).
- **C ⊆ A**: every 6-divisible ring in [16,512] is zero-unpaired (A∧C = C, 83 rings).

Hence **D ⇒ C ⇒ A**: knowing a ring is a seed-3 octave rung automatically tells you it is
6-divisible and zero-unpaired. B is the only criterion with no deterministic relation to
the others — except that B∧D = {96} (the unique 3-family rung).

This is the structural source of the redundancy measured in Section 1–2: A, C, D are
nested predictors, while B is an independent axis.

---

## 4. Unique information content of each criterion

For each criterion, predict it from the other three (mode within the other-3 bit
pattern) and compute the conditional entropy H(X_i | others):

| Criterion | H(X_i) | H(X_i \| others) | unique fraction | shared fraction |
|---|---|---|---|---|
| A | 0.8658 | 0.7746 | **0.895** | 0.105 |
| B | 0.5372 | 0.5368 | **0.999** | 0.001 |
| C | 0.6508 | 0.5382 | **0.827** | 0.173 |
| D | 0.0812 | 0.0546 | **0.672** | 0.328 |

No criterion is a deterministic function of the others (every unique fraction is above
0.67). B carries essentially its full entropy uniquely; D is the most shared (its
entropy is partially carried by the A–C chain it nests into), yet 67% of D remains
unpredictable from A, B, C.

---

## 5. Predictability of each criterion from the other three

| Criterion | best predictor from others | majority baseline | gain |
|---|---|---|---|
| A | 0.7123 | 0.7123 | 0.0000 |
| B | 0.8773 | 0.8773 | 0.0000 |
| C | **0.8431** | 0.8330 | **+0.0101** |
| D | 0.9899 | 0.9899 | 0.0000 |

C is the only criterion whose value is *predictable* from the others beyond the majority
baseline (+1.0%, via its nesting inside A). A, B, D gain nothing from the other criteria.

---

## 6. PCA on the standardized criteria

Eigenvalues of the 4×4 correlation matrix of (A,B,C,D): **[1.397, 1.001, 0.935, 0.667]**,
explaining 34.9% / 25.0% / 23.4% / 16.7% of the variance. Participation ratio
(Σλ)²/Σλ² = **3.744** — the four criteria span ≈ 3.74 effective dimensions. No dominant
factor exists; the set is far from a single degree of freedom. PCA confirms: **not
redundant as a set** (≈4 independent dimensions), while the Section-3 nesting shows the
redundancy is confined to the A–C–D chain.

---

## 7. Which criterion actually selects N = 96?

Scanning single criteria and pairs over all 497 rings:

- **No single criterion isolates N = 96** (A: 354 rings, B: 61, C: 83, D: 5 — each
  contains 96 together with other rings).
- **B is true exactly on the 3-family window [60,120]** (61 rings: family count 3 ⟺
  span ∈ [4,8), which holds on that window).
- **D is true exactly on the seed-3 rung ladder {24, 48, 96, 192, 384}** (5 rings).
- **The conjunction {B, D} is the singleton {96}**: N = 96 is the unique ring that is
  simultaneously a seed-3 octave rung AND inside the 3-family window.

Adding A or C to {B,D} changes nothing (A∧B∧D = B∧C∧D = B∧D = {96}), because A and C are
implied by D at rungs. **The pair {B, D} is the true selector of N = 96; A and C are
redundant for the selection once {B,D} is imposed.** This independently confirms the
M_014 drop analysis and M_015: B (3-family window) and D (octave rung) are the
load-bearing discriminators, while A (pairing) and C (6-divisibility) add no selection
power at 96 beyond being implied by D.

---

## 8. Verdict per question

| Question | Answer |
|---|---|
| Are the four criteria independent? | **NO** — deterministic nesting D ⊆ C ⊆ A; pairwise phi up to 0.285 |
| How much redundancy? | total correlation 0.118 bits = 5.5% of ΣH; shared-fraction ≤ 0.328 per criterion |
| Is any criterion redundant given the others? | **NO** — all unique fractions ≥ 0.672; PCA effective dims 3.74/4; B is 99.9% unique |
| Which criterion selects N = 96? | the **pair {B, D}** (3-family window ∧ seed-3 rung); A, C implied by D at rungs |

**Determination: PARTIALLY REDUNDANT. The four M_014 criteria are not independent — A, C,
D form a deterministic inclusion chain D ⊆ C ⊆ A that yields 5.5% total-correlation
redundancy and pairwise phi up to 0.285 — yet no criterion is redundant given the other
three (all unique-information fractions above 0.67; the criteria span ≈ 3.74 effective
dimensions). B is statistically near-independent. The ring N = 96 is selected by the pair
{B, D}; criteria A and C carry no extra selection power for 96 (they are implied by D).
This confirms, from information-theoretic and minimal-selector angles, the M_014/M_015
conclusion that B and D are the load-bearing discriminators.**

---

## Classification

| Component | Status |
|---|---|
| marginal entropies and joint entropy (497 rings) | **DERIVED** (this audit) |
| total correlation 0.1179 bits = 5.5% of ΣH | **DERIVED** (this audit) |
| pairwise mutual-information and phi matrices | **DERIVED** (this audit) |
| deterministic inclusion chain D ⊆ C ⊆ A | **DERIVED** (this audit, exhaustive over [16,512]) |
| unique-information fractions (B 0.999, A 0.895, C 0.827, D 0.672) | **DERIVED** (this audit) |
| predictability: only C gains from the others (+1.0%) | **DERIVED** (this audit) |
| PCA eigenvalues [1.397, 1.001, 0.935, 0.667], participation ratio 3.744 | **DERIVED** (this audit) |
| minimal selector {B, D} of N = 96 | **DERIVED** (this audit; confirms M_014 drop analysis, M_015) |
| verdict PARTIALLY REDUNDANT | **DERIVED** (this audit) |
| M_014/M_015 confirmed, no reclassification | **DERIVED** (consistency) |

**M_016 shows the four M_014 criteria are not independent — A, C, D are nested
(D ⊆ C ⊆ A) and B is an independent axis — yet none is redundant given the others; N = 96
is selected by the pair {B, D}, with A and C implied. Prior audits M_014, M_015, D_029,
D_030, D_031, D_040 confirmed, not reclassified; no new primitive; canonical AT
unchanged.**

---

## Theorem

> **Theorem (M_016).** Over the population N ∈ [16,512] (497 rings), let A = (0 unpaired),
> B = (family = 3 ∧ span < 8), C = (6 | N), D = (N = 3·2^k). Then: (1) the criteria carry
> a deterministic inclusion chain D ⊆ C ⊆ A (every rung is 6-divisible and zero-unpaired;
> every 6-divisible ring is zero-unpaired), giving total correlation TC = ΣH − H(A,B,C,D)
> = 0.1179 bits = 5.5% of ΣH and pairwise phi up to 0.285 (A–C). (2) No criterion is a
> function of the others: H(X_i|others)/H(X_i) = 0.895, 0.999, 0.827, 0.672 for
> A, B, C, D; the correlation-matrix PCA has eigenvalues [1.397, 1.001, 0.935, 0.667]
> (participation ratio 3.744). (3) B is near-independent of {A, C, D}
> (I(B;·) ≤ 0.0004 bit). (4) The minimal selector of N = 96 is the pair {B, D}: B holds
> exactly on [60,120] and D on {24, 48, 96, 192, 384}, and B∧D = {96}; A and C add no
> further selection (A∧B∧D = B∧C∧D = {96}). Proof: exhaustive enumeration of the 497
> rings and of all single-criterion and pair conjunctions (Sections 1–7, verified). ∎
>
> *Proof sketch.* Enumerate all rings N = 16..512; count each criterion; compute marginal
> and joint entropies, pairwise MI/phi, conditional entropies and PCA (Sections 1–6);
> enumerate single and pair conjunctions to find the minimal selector of 96 (Section 7). ∎

---

## Falsification Path

1. **"A, C, D are deterministically nested"** would be falsified by any N ∈ [16,512] with
   D=1 and C=0, D=1 and A=0, or C=1 and A=0. The exhaustive scan finds none.
2. **"no criterion is a function of the others"** would be falsified by a criterion with
   H(X_i|others) = 0; the minima are 0.0546 (D) and 0.5368 (B), all > 0.
3. **"{B,D} is the minimal selector of 96"** would be falsified by another single
   criterion or pair isolating {96}; none does (Section 7).
4. **Prior registry entries** (M_014, M_015, D_029, D_030, D_031, D_040) would be
   reclassified only by a superseding audit; M_016 confirms them and makes no
   reclassification.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/M_Measurement/Y_M_016_Tests.cs`
**Run:** 2026-09-08 · **Result:** see `Tests/Results/Y_M_016_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_M_016_CriterionBits` | A/B/C/D on 354/61/83/5 of 497 rings; 96 satisfies all four | ✅ |
| `Y_M_016_Entropies` | ΣH = 2.135, H_joint = 2.017, total correlation = 0.118 bit | ✅ |
| `Y_M_016_MutualInformation` | pairwise MI matrix (max A–C = 0.0912; B near-independent) | ✅ |
| `Y_M_016_CorrelationMatrix` | phi matrix (A–C = 0.285, C–D = 0.225) | ✅ |
| `Y_M_016_NestingChain` | D ⊆ C ⊆ A; B∧D = {96} | ✅ |
| `Y_M_016_UniqueInformation` | H(X_i\|others) fractions ≥ 0.672 | ✅ |
| `Y_M_016_Predictability` | only C gains from the others (+1.0%) | ✅ |
| `Y_M_016_Pca` | eigenvalues [1.397, 1.001, 0.935, 0.667]; PR 3.744 | ✅ |
| `Y_M_016_SelectorOf96` | B on [60,120], D on rung ladder, {B,D} = {96} | ✅ |
| `Y_M_016_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_M_016"`

---

## References

- ResearchY-M_014 (the four criteria and the N=96 scan), M_015 (robustness of the score),
  D_020 (3-family window [4,8)), D_029 (zero-defect set), D_030 (octave rung 3·2^k),
  D_040 (classification registry).
- ResearchY-D_041 (D96 spectrum λ_k = Σ_s 2(1−cos 2πks/N)).
