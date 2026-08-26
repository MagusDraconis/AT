# AT-101: Theory Repair Report

## Date: 2026-08-04

---

## Repair Summary

**Classification: B — Partially Repaired**

The rejected {R, M} theory was repaired by replacing the linear equation with a logistic form incorporating N-scaling and variable exponents. Survival improved from **1/8 (12%) to 4/8 (50%)**. Four failure modes remain unresolved.

---

## Repair Details

### Original Theory (Rejected by AT-100)
```
dR/dt = α₀ + α₁·R + α₂·M
Survival: 1/8 (12%)
```

### Repaired Theory (AT-101)
```
dR/dt = a·N·M·R^0.50·(1-R)^1.50
Survival: 4/8 (50%)
Training R²: 0.827
```

### What Changed

| Component | Before | After | Reason |
|-----------|--------|-------|--------|
| Intercept | α₀ (free) | 0 (forced) | dR/dt should be 0 when everything is 0 |
| R term | α₁·R (linear) | R^0.50·(1-R)^1.50 (logistic with exponents) | Forces saturation at R=0 and R=1 |
| N scaling | None | N (linear) | System size affects coupling strength |
| M scaling | α₂·M (linear) | M (linear) | Unchanged — M is proportional to coupling |

---

## Attack-by-Attack Repair Analysis

| Attack | Before R² | After R² | ΔR² | Repaired? | Notes |
|--------|-----------|----------|-----|-----------|-------|
| Extreme Coherence | -13.27 | +0.28 | +13.55 | ✓ YES | R·(1-R) saturation works |
| Extreme M | -8.66 | +0.03 | +8.69 | ✗ NO | N·M scaling insufficient at extreme M |
| Mixed Topologies | -1.22 | +0.48 | +1.70 | ✓ YES | Topology independence confirmed |
| Coupling Laws | -0.42 | +0.90 | +1.32 | ✓ YES | M universality strengthened |
| Phase Noise | -1.20 | -0.15 | +1.04 | ✗ NO | Noise is fundamentally unpredictable from (R,M) |
| Large-N N=500 | -14.48 | -3.82 | +10.66 | ✗ NO | Linear N scaling insufficient |
| Small-N N=10 | -42.12 | +0.11 | +42.24 | ✓ YES | N-dependence captures N=10 |
| Out-of-Distribution | +0.95 | -4.00 | -4.95 | ✗ NO | K,λ dependence not captured by N·M alone |

---

## Remaining Failures (4/8)

### 1. Extreme Mean Coupling
M values far from training (K=0.5→M≈0.06, K=5→M≈0.19) produce biased predictions. The N·M term doesn't capture the nonlinear relationship between K and M: M ∝ K·f(λ, topology).

**Root cause**: M depends on K, λ, and topology in a complex way that is not simply proportional to K. The model assumes M captures all coupling information, but K and λ interact nonlinearly.

### 2. Phase Noise
Gaussian noise σ=0.3 rad/step breaks all predictability. The theory assumes deterministic Kuramoto dynamics.

**Root cause**: Noise introduces stochastic fluctuations that {R, M} cannot predict. This is fundamentally irreducible without a noise model.

### 3. Large-N (N=500)
Linear N scaling doesn't capture the N-dependence at large N. At N=500, the mean-field approximation (dθ/dt ∝ R) becomes more accurate, reducing the effective coupling per oscillator.

**Root cause**: The effective coupling in Kuramoto is (1/N)·Σ K_ij ≈ M (N cancels). The model uses N·M which over-estimates the coupling at large N.

### 4. Out-of-Distribution
K=0.1 and K=10 with extreme λ values produce M values 2+ orders of magnitude outside training. The model cannot extrapolate.

**Root cause**: M ranges from 0.0004 to 3.68 vs training 0.02-0.20. The functional form a·N·M·R^0.50·(1-R)^1.50 doesn't capture the K,λ dependence correctly.

---

## What We Learned

### Confirmed:
1. **R·(1-R) saturation is ESSENTIAL** — fixes extreme coherence (ΔR² = +13.55)
2. **N-dependence is ESSENTIAL** — fixes small-N (ΔR² = +42.24)
3. **M universality persists** — coupling law independence improved (R²: 0.78→0.90)

### New Insights:
1. **N·M over-counts coupling** — the Kuramoto equation uses (1/N)·Σ K_ij, so the effective coupling per oscillator is M (not N·M). The correct scaling may be M or M/N rather than N·M.
2. **K,λ are not fully compressible into M** — different (K,λ) combinations can produce similar M but different dR/dt. The coupling distribution matters, not just the mean.
3. **Noise is irreducible** — cannot be predicted from {R, M} alone.

### Failed Hypotheses:
1. **N·M is the correct scaling** — overestimates at large N, underestimates at extreme M
2. **M captures all coupling information** — K,λ interactions beyond M matter at extremes
3. **Logistic+R^0.50 is sufficient** — need higher-order K,λ terms

---

## Recommended Next Repairs

1. **Correct N scaling**: Use M (not N·M) since Kuramoto has (1/N) factor
   ```
   dR/dt = a·M·R^n·(1-R)^m
   ```

2. **Add K,λ as explicit parameters**: M alone cannot capture extreme regimes
   ```
   dR/dt = a·M·R^n·(1-R)^m · f(K, λ)
   ```
   where f(K,λ) could be K/λ or a learned function.

3. **Add noise term**: Acknowledge irreducible stochasticity
   ```
   dR/dt = deterministic(M,R) + ε·(1/N)^0.5
   ```
   with ε ~ N(0, σ²) scaling as 1/√N (finite-size fluctuations).

4. **Separate training regimes**: Fit different model for K≪1 vs K≫1 vs K≈1.
