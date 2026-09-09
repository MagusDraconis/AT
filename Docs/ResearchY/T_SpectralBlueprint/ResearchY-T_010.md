# ResearchY-T_010 — D96 Survivor Compression Audit

**Program:** ResearchY — Technology Program
**Group:** T — Spectral Blueprint
**ID:** ResearchY-T_010 (permanent)
**Title:** D96 Survivor Compression — why does D96 saturate near 5 survivors?
**Status:** COMPLETE
**Date:** 2026-09-09
**File:** `T_SpectralBlueprint/ResearchY-T_010.md`
**Depends on:** ResearchY-T_007 (bounded innovation), T_008 (asymptotic diversity limit),
T_009 (fitness-reach law)
**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_010_Tests.cs` (6/6 ✅)
**Shared:** `AT.Tests/Shared/SpectralCaseCatalog.cs`, `AT.Tests/Shared/FitnessReachLaw.cs`

---

## Question

Why does D96 saturate near **5** survivors while a random sparse landscape saturates near
**17**? Can S∞ be predicted from spectral structure alone?

## Method

Measure, per case: survivor count S∞ (μ=0.01, β=1), spectral tail shape (near-gap mode
density), fitness entropy, multiplicity structure (degeneracy), and dominant-mode
concentration. All deterministic; random uses the fixed seed 42.

## Results

| case | A | S∞ | conc (w*/Σw) | HHI | entropy | near-gap (≤2λ₂) | degeneracy | max mult |
|---|---|---|---|---|---|---|---|---|
| D96 | 44 | **5** | 0.362 | 0.149 | 2.903 | **2** | 0.542 | 6 |
| D96-3D | 12 | 9 | 0.201 | 0.127 | 2.199 | 6 | 0.875 | 20 |
| random | 95 | **17** | 0.017 | 0.011 | 4.529 | **71** | 0.010 | 1 |
| physical | 48 | 5 | 0.225 | 0.082 | 3.162 | 4 | 0.500 | 2 |
| unphysical | 3 | 3 | 0.781 | 0.638 | 0.657 | 32 | 0.969 | 32 |

---

## The mechanism

**D96's circulant symmetry collapses its spectrum.** The 95 non-zero modes of
\(C_{96}(1..6)\) collapse into only **44 distinct eigenvalues** (degeneracy fraction 0.542),
so near the spectral gap \(\lambda_2\) the spectrum is **sparse**: just **2 modes** within
\(2\lambda_2\). Fitness \(w=m/\lambda\) is therefore **peaked** — a few high-fitness modes
separated by large gaps (concentration 0.362, entropy 2.90 nats).

**The random sparse graph is spectrally dense.** With ~95 distinct eigenvalues (degeneracy
0.010), **71 modes** sit within \(2\lambda_2\). Fitness \(w=1/\lambda\) is **flat** — many
near-equal modes (concentration 0.017, entropy 4.53 nats).

The mutation–selection balance keeps few survivors on a peaked field (D96 ≈ 5) and many on a
flat field (random ≈ 17). Crowding β amplifies the contrast: it levels a flat field into many
coexisting species (random 7 → 17) but barely changes a peaked field (D96 4 → 5).

---

## Predictability

**S∞ is a function of spectral structure alone.** At β = 0 the equilibrium is the Perron
vector of `M·diag(w)` — a pure function of the fitness spectrum `{w}` (hence of the spectral
structure) — and reproduces S∞ **exactly** for every case (T_009). At μ = 0 the crowding
threshold `#{w_k > Z*(β)}` does the same. The interior (μ>0, β>0) is bracketed by these two
exact boundaries.

## Classification

| Item | Classification |
|---|---|
| D96→~5 vs random→~17 split follows from the spectral tail (sparse/peaked vs dense/flat) | DERIVED |
| S∞ is a function of the full spectral structure (exact at β=0 / μ=0) | DERIVED |
| The precise integer (5 vs 17) at finite (μ, β) | EMERGENT |
| "A single spectral scalar predicts S∞" | REFUTED |

**The single-scalar refutation is concrete:** D96 and physical *both* saturate at S∞ = 5, yet
differ in landscape size (44 vs 48), fitness entropy (2.90 vs 3.16), and concentration (0.362
vs 0.225). No one number (A, λ₂, entropy, concentration, variance, rigidity) is a bijection
onto S∞ — only the **full fitness spectrum** (via the exact reach laws) determines it.

---

## Conclusion

D96 compresses survivors to ~5 because its circulant (highly degenerate) spectrum is **sparse
near the gap**, producing a peaked fitness field that the mutation–selection balance keeps to
a handful of species. The random graph's dense near-degenerate spectrum produces a flat field
that keeps ~17. The split is a **DERIVED** consequence of the spectral tail (via the exact
β=0 Perron and μ=0 threshold laws), the precise integers are **EMERGENT**, and any
single-scalar predictor is **REFUTED** — closing the T_005→T_010 arc: spectral structure
*compresses* (T_005), *dominates* (T_006), *bounds* (T_007/T_008), and *quantitatively
explains* (T_009/T_010) the diversity, all from the fitness spectrum `w = m/λ`. No new
primitive; canonical AT unchanged.
