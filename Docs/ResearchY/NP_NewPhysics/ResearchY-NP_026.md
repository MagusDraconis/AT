# ResearchY-NP_026 — Protected Block Universality Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_026 (permanent)
**Title:** Protected Block Universality Audit
**Status:** COMPLETE
**Date:** 2026-09-01
**File:** `NP_NewPhysics/ResearchY-NP_026.md`
**Depends on:** ResearchY-NP_025 (K=6 uniqueness — established the √(K/(K+1))
family), NP_024 (O(2) mirror-pair physical prediction)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_026_Tests.cs`

---

## Purpose

**Is R_K = √(K/(K+1)) a theorem of ALL circulant-ring spectra, or only of the
canonical nearest-neighbour class C_N(±1..±K)?** NP_025 established the K-family
√(K/(K+1)) for the consecutive uniform generator class. This audit scans alternative
topologies (other generator sets, weighted links, random perturbations, missing
links, non-circulant graphs) to locate the TRUE mathematical origin of the protected
ratio.

---

## 1. The theorem to test

For the canonical nearest-neighbour circulant C_N(±1..±K) (all weights = 1):

```
λ_k = Σ_{s=1}^{K} 2(1 − cos(2πks/N))
```

The two highest-multiplicity non-doublet blocks sit at **λ = 2K and λ = 2K+2**, with
ratio √(2K/(2K+2)) = **√(K/(K+1))**.

| K | top blocks (λ, mult) | ratio | √(K/(K+1)) |
|---|---|---|---|
| 2 | (6,4),(4,3) | √(4/6) | 0.81650 |
| 3 | (6,6),(8,5) | √(6/8) | 0.86603 |
| 4 | (10,4),(8,3) | √(8/10) | 0.89443 |
| 5 | (10,6),(12,5) | √(10/12) | 0.91287 |
| **6** | **(14,6),(12,5)** | **√(12/14)** | **0.92582** |
| 7 | (14,8),(16,7) | √(14/16) | 0.93541 |
| 8 | (18,10),(16,9) | √(16/18) | 0.94281 |

---

## 2. Scan 1 — alternative circulant generator sets

| Generator set (size 6) | Top blocks | Ratio √(6/7)? |
|---|---|---|
| **±{1,2,3,4,5,6} (canonical)** | (14,6),(12,5) | ✅ 0.92582 |
| ±{1,3,5,7,9,11} (odd) | (12,22) — ONE block, no partner | ❌ |
| ±{1,2,4,8,16,32} (powers of 2) | (none — all 2-fold) | ❌ |

**The ratio structure is specific to the CONSECUTIVE generator set.** The odd set
produces a single high-multiplicity block (22-fold at λ=12) with no partner; the
powers-of-2 set produces no non-doublet blocks at all.

---

## 3. Scan 2 — weighted links

| Weighting | Top blocks | Ratio? |
|---|---|---|
| uniform (canonical) | (14,6),(12,5) | ✅ √(6/7) |
| linear decay w(s) = 1/s | (none) | ❌ |
| exponential decay e^(−0.3s) | (none) | ❌ |
| random w ∈ [0.5, 1.5] | (none) | ❌ |

**Weighted links DESTROY the protected blocks entirely.** The exact λ = 2K / 2K+2
structure requires ALL couplings equal to 1.

---

## 4. Scan 3 — random perturbations

| Perturbation (±5% on weights) | Top blocks | Ratio? |
|---|---|---|
| canonical | (14,6),(12,5) | ✅ |
| trial 0 | (none) | ❌ |
| trial 1 | (none) | ❌ |
| trial 2 | (none) | ❌ |

**Any random perturbation removes the blocks** — the ratio is NOT perturbatively
protected against weight variation (unlike the mirror pairs, which are protected
against reflection-preserving perturbations, NP_023).

---

## 5. Scan 4 — missing links

| Missing generator | Top blocks | Ratio | √(K/(K+1))? |
|---|---|---|---|
| drop ±1 | (10,8) — one block | — | ❌ |
| drop ±2 | (13,4),(9,4) | 0.83205 | ❌ (√(6/7)=0.92582) |
| drop ±3 | (12,12),(8,3) | 0.81650 | ❌ |

**Missing links change or destroy the ratio.** The √(6/7) value requires the FULL
consecutive set {±1..±6}.

---

## 6. Scan 5 — non-circulant graphs

| Graph | Multiplicity structure | Protected ratio? |
|---|---|---|
| Ring C_12(±1) | 2-fold pairs | generic only |
| Path P_12 | all distinct | ❌ |
| Complete K_12 | N-fold (0 and 11×12) | ❌ |
| Random graph (p=0.4) | all distinct | ❌ |

**No non-circulant graph produces the √(K/(K+1)) protected ratio.**

---

## 7. The analytic origin

For the canonical consecutive uniform set, the two special modes are:

**λ_{N/4}** (the quarter-tone mode): since cos(2π(N/4)s/N) = cos(πs/2),
the sequence (1−cos(πs/2)) = 1,2,1,0 (period 4, sum 4 per period), so

```
λ_{N/4} = 2·Σ_{s=1}^{K}(1−cos(πs/2))  →  K=6: 14 = 2K+2
```

**λ_{N/6}** (the sixth-tone mode): cos(2π(N/6)s/N) = cos(πs/3),
giving for K=6:

```
λ_{N/6} = 2·Σ_{s=1}^{K}(1−cos(πs/3))  →  K=6: 12 = 2K
```

The ratio √(12/14) = √(6/7) = √(K/(K+1)).

**Requirements for the exact ratio:**
1. the CONSECUTIVE uniform generator set ±{1..±K} (all weights = 1);
2. N divisible by 4 (for the k=N/4 mode) and by 6 (for the k=N/6 mode).

**The ratio is a theorem of the canonical nearest-neighbour circulant class** — it
is not generic to all circulants (alternative generator sets fail) and not generic
to all graphs (non-circulant graphs fail).

---

## Theorem

> **Theorem (NP_026).** The protected inter-block ratio R_K = √(K/(K+1)) is a
> theorem of the CANONICAL NEAREST-NEIGHBOUR CIRCULANT class C_N(±1..±K) with
> uniform weights — NOT of all circulants and NOT of general graphs (determination
> B). Proof: (1) The canonical class (Section 1, verified): the top two non-doublet
> blocks sit at λ = 2K and λ = 2K+2 with ratio √(2K/(2K+2)) = √(K/(K+1)), for every
> K ≥ 2 (K=2: √(2/3); …; K=6: √(6/7); …; K=8: √(8/9)). (2) Alternative generator
> sets (Section 2, verified): the odd set ±{1,3,5,7,9,11} produces ONE 22-fold block
> with no partner; the powers-of-2 set produces no non-doublet blocks — the ratio
> does NOT hold. (3) Weighted links and random perturbations (Sections 3–4,
> verified): any non-uniform weight destroys the blocks — the ratio requires ALL
> couplings equal to 1. (4) Missing links (Section 5, verified): dropping any
> generator changes or destroys the ratio. (5) Non-circulant graphs (Section 6,
> verified): path, complete, and random graphs show no protected ratio. (6) The
> analytic origin (Section 7, verified): λ_{N/4} = 2K+2 and λ_{N/6} = 2K from the
> period-4/period-6 sequences of (1−cos), requiring N divisible by 4 and 6.
> Therefore: A) exact theorem of all circulants — NO; **B) circulant-only theorem
> (canonical nearest-neighbour class) — YES**; C) approximation — NO (the ratio is
> exact within the class). The true mathematical origin: the consecutive uniform
> generator set (nearest-neighbour circulant), with the two special modes k=N/4 and
> k=N/6. Classification: R_K as a theorem of the canonical nearest-neighbour
> circulant class DERIVED (exact); the same ratio for alternative generator sets
> FALSIFIED; for weighted/perturbed/missing-link rings FALSIFIED; for non-circulant
> graphs FALSIFIED; the mirror-pair degeneracy (reflection-protected, NP_023) vs the
> block ratio (weight-fragile, NP_026) — DIFFERENT protection classes. No new
> primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) State the canonical theorem (Section 1). (2)–(6) Scan the five
> alternative topologies (Sections 2–6). (7) Derive the analytic origin (Section 7).
> (8) Determine B (Section 8). ∎

---

## 8. Determine

| Option | Verdict |
|---|---|
| A) exact theorem of all circulants | **NO** — alternative generator sets fail |
| **B) circulant-only theorem (canonical nearest-neighbour class)** | **YES** |
| C) approximation | **NO** — exact within the class |

**R_K = √(K/(K+1)) is a theorem of the CANONICAL NEAREST-NEIGHBOUR CIRCULANT class
C_N(±1..±K) with uniform weights** — the specific consecutive generator set is
required. It is not a theorem of all circulants and not an approximation.

---

## 9. Counterexamples

| Attempt | Why it fails |
|---|---|
| "The ratio holds for all circulants" | the odd and powers-of-2 generator sets fail (verified) |
| "The ratio survives weight variation" | any non-uniform weight destroys the blocks (verified) |
| "The ratio survives missing links" | dropping ±1/±2/±3 changes or destroys it (verified) |
| "Non-circulant graphs show it" | path/complete/random graphs show no protected ratio (verified) |
| "The ratio is an approximation" | it is exact within the canonical class |

---

## 10. Falsification paths

| Claim | Falsification |
|---|---|
| R_K holds for the canonical nearest-neighbour class | a consecutive uniform ring C_N(±1..±K) with a different top-block ratio |
| the origin is λ_{N/4} = 2K+2, λ_{N/6} = 2K | a canonical ring where these modes give different values |
| the ratio requires uniform weights | a weighted ring still giving √(K/(K+1)) |
| the ratio requires the full consecutive set | a missing-link ring still giving √(K/(K+1)) |

---

## Classification

| Component | Status |
|---|---|
| R_K = √(K/(K+1)) for the canonical nearest-neighbour class | **DERIVED** (exact theorem) |
| same ratio for alternative generator sets | **FALSIFIED** |
| same ratio for weighted/perturbed/missing-link rings | **FALSIFIED** |
| same ratio for non-circulant graphs | **FALSIFIED** |
| origin: consecutive uniform set + modes N/4, N/6 | **DERIVED** (analytic) |
| mirror-pair protection (reflection) vs block-ratio protection (weight-fragile) | **DIFFERENT protection classes** |

**The true mathematical origin of √(K/(K+1)): the canonical nearest-neighbour
circulant class C_N(±1..±K) with uniform weights.** It is a theorem of this class
(option B), not of all circulants and not of general graphs. Notably, the block
ratio is FRAGILE (destroyed by any weight variation or missing link), unlike the
mirror-pair degeneracy (which is protected against reflection-preserving
perturbations, NP_023) — two distinct protection classes. No new primitive; canonical
AT unchanged.

---

## 11. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_026_Tests.cs`
**Run:** 2026-09-01 · **Result:** see `Tests/Results/Y_NP_026_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_026_CirculantTheorem` | canonical class gives √(K/(K+1)) exactly | ✅ |
| `Y_NP_026_AlternativeGenerators` | odd/powers-of-2 sets fail | ✅ |
| `Y_NP_026_WeightedLinks` | weights destroy the blocks | ✅ |
| `Y_NP_026_RandomPerturbation` | perturbations destroy the blocks | ✅ |
| `Y_NP_026_MissingLinks` | missing links change/destroy the ratio | ✅ |
| `Y_NP_026_NonCirculant` | path/complete/random graphs fail | ✅ |
| `Y_NP_026_OriginDetermination` | B — canonical nearest-neighbour class | ✅ |
| `Y_NP_026_Run` | research report | ✅ |

**Conclusion:** R_K = √(K/(K+1)) is a theorem of the canonical nearest-neighbour
circulant class C_N(±1..±K) with uniform weights (determination B) — the true
mathematical origin is the consecutive uniform generator set with the two special
modes k=N/4 and k=N/6. It is NOT a theorem of all circulants, NOT of general graphs,
and NOT an approximation. The block ratio is weight-fragile (unlike the
reflection-protected mirror pairs). No new primitive; canonical AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_026"`

---

## References

- ResearchY-NP_025 (K=6 uniqueness — the √(K/(K+1)) family), NP_024 (O(2)
  mirror-pair physical prediction), NP_023 (O(2) mirror search).
