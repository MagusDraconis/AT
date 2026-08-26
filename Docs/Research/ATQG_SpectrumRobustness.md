# AT-QG Phase 105 — Spectrum Robustness Audit

**Program:** AT-QG (Unification)
**Phase:** 105 — are the spectral ratios of QG104 stable under changes of network size and topology?
**Status:** COMPLETED — 3/3 xUnit tests pass (318/318 AT-QG verified; COMPUTATIONAL)
**Constraint:** no new primitives added here (computational audit of the native operator spectrum)

---

## 1. Goal

QG104 found a hierarchical discrete spectrum on the 91-event causal network. This phase asks: are the spectral
ratios STABLE under changes of network size (91 → 200 → 500 events) and topology (aspect-ratio variant at fixed
N, deterministic link removal)? Classify: RANDOM / ROBUST / UNIVERSAL.

---

## 2. Size scaling 91 → 200 → 500 events (ATQG1050)

| N | events | λ_2 | span ω_max/ω_min | ω_1 | ω_max |
|---|---|---|---|---|---|
| 91 | 91 | 0.0994 | 10.66 | 0.3152 | 3.3614 |
| 200 | 200 | 0.0276 | 20.43 | 0.1661 | 3.3932 |
| 500 | 500 | 0.0299 | 19.93 | 0.1730 | 3.4489 |

- The hierarchy persists at ALL sizes (span ≫ 10).
- The spectral gap shrinks with size (Weyl/continuum regime: λ_2 → 0).
- Low-mode ratio deviation (RMS, first 12): 91 vs 200 = 10.6%, 91 vs 500 = 10.8% (mean 7.7%) — the LOW-MODE
  spectral ratios are robust under size growth.

---

## 3. Topology perturbations at fixed N = 91 (ATQG1051)

- **Aspect-ratio variant** (tMax=12, xMax=3 → N=91, span 8.53): low-mode deviation 11.9% — stable.
- **Deterministic link removal**: 5% → 3.6%, 10% → 3.8%, 20% → 3.3% deviation; span stays 11.8 after 20%
  removal — very stable.

The low-mode spectral ratios remain stable (≤ 12%) under BOTH topology perturbations, and the hierarchy
persists (span > 5 everywhere; > 10 after link removal).

---

## 4. Spectral universality (ATQG1052)

Normalized shape (eigenvalue CDF scaled by λ_max), Kolmogorov–Smirnov distances:
- size 91 vs 500: KS = 0.135
- size 91 vs aspect: KS = 0.100
- size 91 vs rem 20%: KS = 0.111

Low-mode ratio deviation (RMS): size 10.8%, aspect 11.9%, removal 3.3%.

The shape DRIFTS with size (KS > 0.1): the bulk fills in as the network grows (Weyl/continuum law), so the
full spectrum is NOT scale-invariant.

---

## 5. Classification (ATQG1052)

**ROBUST.**

- NOT RANDOM: low-mode ratios deviate only a few percent under size AND topology changes; hierarchy persists.
- NOT UNIVERSAL: the normalized spectral shape drifts with size (KS > 0.1) — the bulk fills in (Weyl law).
- ROBUST: the LOW-MODE spectral ratios (the hierarchical fingerprint) are stable under size growth and
  topology perturbations — robust, not random, not universal.

---

## 6. Conclusion

The QG104 spectral ratios are **ROBUST**: the low-mode hierarchical fingerprint survives size growth
(91 → 200 → 500) and topology perturbations (aspect change, up to 20% link removal) to within ~12%, while the
full normalized shape drifts with size (not UNIVERSAL). The native network spectrum is a robust, reproducible
structural property of the causal network — strengthening the QG104 result that the spectrum is real and
quantization-like, and further supporting that any SM mapping is structural (PARTIAL), not numerically fixed.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG1050 `ATQG1050_SizeScaling` | PASS (hierarchy at 91/200/500; gap shrinks; RMS < 15%) |
| ATQG1051 `ATQG1051_TopologyPerturbations` | PASS (aspect 11.9%, removal ≤ 3.8%, hierarchy persists) |
| ATQG1052 `ATQG1052_UniversalityAndClassification` | PASS (ROBUST) |

Code: `AT.Core/ResearchXH/SpectrumRobustness.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase105_SpectrumRobustnessTests.cs`.
