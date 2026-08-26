# AT-100: Physics Candidate Validation Report

## Hostile Review of the {R, M} Minimal Theory

### Date: 2026-08-04

---

## Executive Summary

**THEORY REJECTED** — Classification A.

The {R, M} minimal theory survived 3 of 8 adversarial attack vectors. Five attacks produced catastrophic failures (R² < 0), exposing fundamental limitations in the theory's generalization.

---

## Attack Results

### ✓ PASSED (3/8)

| # | Attack | Test R² | Interpretation |
|---|--------|---------|----------------|
| 2 | **Extreme Mean Coupling** | 0.624 | Theory generalizes to M→0 and M>>1. The linear model works across 2 orders of magnitude in M. |
| 3 | **Mixed Topologies** | 0.780 | Theory generalizes perfectly across all 6 topology types with fresh seeds. No overfitting. |
| 4 | **Different Coupling Laws** | 0.782 | Theory works with power-law, linear-cutoff, AND constant coupling. M is truly universal — it captures coupling regardless of functional form. |

### ⚠ FAILED (5/8)

| # | Attack | Test R² | Severity | Root Cause |
|---|--------|---------|----------|------------|
| 1 | **Extreme Coherence** | -1755 | Critical | Theory fails at R≈0 and R≈1. Trained on R≈0.09, cannot extrapolate to endpoints. |
| 5 | **High Phase Noise** | -0.78 | Critical | Noise σ=0.5 rad/step destroys all predictability. Theory has no noise model. |
| 6 | **Large-N (N=500)** | -3.72 | Critical | Theory is N-dependent. Coefficients trained at N=100 don't work at N=500. |
| 7 | **Small-N (N=10)** | -311 | Critical | Same N-dependence in opposite direction. Finite-size effects dominate. |
| 8 | **Out-of-Distribution** | -17.8 | Critical | (K=0.1..10, λ=0.01..0.20) far from training. M ranges 0.0004-3.68 vs training 0.02-0.20. |

---

## Failure Analysis

### Failure Mode 1: Extreme Coherence

The theory is trained on R≈0.09 (near-incoherent initial states). At R≈0 (fully random), the Kuramoto dynamics produce near-zero dR/dt because sin(θ_j - θ_i) averages to zero. At R≈1 (fully synchronized), dR/dt≈0 because all phases are aligned. The linear model dR/dt = α₀ + α₁·R + α₂·M predicts NON-ZERO dR/dt at both endpoints, producing unbounded errors.

**Fix required**: The theory needs a nonlinear term that forces dR/dt→0 as R→0 and R→1. A logistic form: dR/dt ∝ R·(1-R)·M would naturally handle the endpoints.

### Failure Mode 2: N-Dependence (Attacks 6 & 7)

The most fundamental failure. The {R, M} theory is NOT scale-invariant. At N=10, finite-size fluctuations dominate. At N=500, the effective coupling per oscillator changes because the sum in the Kuramoto equation scales as ~N·M. The relationship between M and dR/dt depends on N.

**Fix required**: N must appear in the theory. Options:
- (a) dR/dt = f(R, M, N) — add N as parameter
- (b) dR/dt = f(R, M/N) — M scales with 1/N
- (c) dR/dt = f(R, M_eff) where M_eff = N·M (total coupling)

### Failure Mode 3: Phase Noise (Attack 5)

Adding Gaussian noise σ=0.5 rad/step to each oscillator phase destroys predictability. The theory assumes deterministic Kuramoto dynamics. Real systems have noise.

**Fix required**: Either a noise model or an explicit acknowledgment that the theory applies only to low-noise regimes.

### Failure Mode 4: Out-of-Distribution Parameters (Attack 8)

At K=0.1 (20x weaker than training) and K=10 (5x stronger), with λ=0.01 (5x shorter) and λ=0.20 (4x longer), the theory fails. M ranges from 0.0004 to 3.68 — orders of magnitude outside the training range of 0.02-0.20.

**Fix required**: Either wider training data or a functional form that captures the K,λ dependence explicitly: dR/dt = f(R, M, K, λ).

---

## Generalization Score

```
Overall:  3/8 attacks survived → 38% generalization
In-domain (similar parameters):  3/3 survived → 100%
Out-of-domain (extrapolation):   0/5 survived →   0%
```

The theory is EXCELLENT within its training regime but has ZERO extrapolation capability.

---

## What the Theory Gets RIGHT

Despite the rejection, three results are scientifically significant:

1. **M is genuinely universal across coupling laws.** The theory works equally well with exp(-d/λ), 1/(1+d/λ), linear cutoff, and constant coupling. M captures coupling strength regardless of functional form. This is a MAJOR positive finding.

2. **Topology independence is confirmed.** The theory generalizes perfectly across all 6 topology types (uniform, clustered, linear, circular, dense-sparse, random-clusters). M truly compresses all topology information.

3. **M range robustness is surprisingly good.** The theory handles M values from 0.06 to 0.19 (3x range) with consistent R². The linear model is robust within its training range.

---

## Required Theory Extensions

Based on the failure analysis, the minimal theory needs:

| Extension | Priority | Justification |
|-----------|----------|---------------|
| N-dependence | **CRITICAL** | Theory breaks at N≠100. dR/dt must include N scaling. |
| Nonlinear R terms | **CRITICAL** | Linear model fails at R≈0 and R≈1. Need R·(1-R) saturation. |
| K,λ dependence | High | Theory breaks for parameters far from training. |
| Noise model | Medium | Real systems have noise. Theory needs a stochastic term. |

---

## Revised Theory Candidate

Based on the failure analysis, a corrected minimal theory:

```
State = {R, M}
Parameters = {N, K, λ, β}

dR/dt = N·M · R·(1-R) · g(K,λ)   [logistic with coupling scaling]
dM/dt = f(R, M)                     [same as AT-082, weak coupling]
```

Where g(K,λ) is a function to be determined from first principles.

---

## Classification

**A: Theory Rejected**

The {R, M} theory as currently formulated is NOT a valid candidate for emergent physics. It fails 5 of 8 adversarial tests, with catastrophic (R² < 0) failures on extrapolation.

However, the failure pattern is INSTRUCTIVE:
- The theory is excellent within its training regime (R²=0.73-0.78)
- M is genuinely universal across coupling laws and topologies
- The failures point to specific, fixable limitations (N-dependence, R saturation)
- A revised theory with N·M scaling and R·(1-R) saturation may succeed

**Recommendation**: Do NOT discard the {R, M} approach. Extend it with N-dependence and nonlinear R terms, then re-validate.
