# Proto-Matter Abundance Theory

## AT-119: Analytic Prediction of Proto-Matter Abundance

### Abstract

We derive a closed-form expression for the expected abundance
of proto-matter condensates ⟨Q⟩ as a function of the system
parameters (K, λ, N). The derivation starts from the nucleation
condition c₀·M₀ > D_R/w² (AT-118) and uses finite-N fluctuation
statistics to compute the probability of charge creation per
independent spatial region.

### 1. Nucleation Condition

From AT-108 and AT-118, the governing PDE is:

```
∂R/∂t = c₀·M·R·(1−R²) + D_R·∇²R
```

where:
- c₀ ≈ 0.0047: empirical reaction coefficient
- M: local mean coupling
- D_R ≈ 2.5×10⁻⁵: effective diffusion coefficient
- R ∈ [0, 1]: coherence order parameter

A fluctuation of width w nucleates a condensate if:

```
c₀·M₀·R > D_R·R/w²
```

Canceling R (valid for R > 0):

```
c₀·M₀ > D_R/w²
```

This is the NUCLEATION CONDITION: reaction must overcome diffusion.

### 2. Critical Local Coupling

For a typical soliton width w ≈ 0.10:

```
M_crit = D_R/(c₀·w²) = 2.5×10⁻⁵/(4.7×10⁻³ · 1.0×10⁻²) ≈ 0.053
```

If the local coupling M₀ exceeds M_crit ≈ 0.053, a fluctuation
can nucleate a condensate.

The local coupling M₀ depends on system parameters:

```
M₀ = K · f(λ, spatial distribution)
```

For uniform spatial distribution with exponential coupling:

```
M₀ ≈ K · λ² · 40  (empirical for the tested grid)
```

### 3. Critical Fluctuation Amplitude

Define the critical fluctuation amplitude:

```
R_crit = M_crit / M₀
```

For a fluctuation to nucleate: R > R_crit locally.

### 4. Finite-N Fluctuation Statistics

For N random oscillators with random phases:

```
R²·N ~ χ²_2  (chi-squared with 2 degrees of freedom)
```

For large N, the probability that R exceeds R_crit:

```
P(R > R_crit) = P(R² > R_crit²)
              = P(χ²_2/N > R_crit²)
              ≈ exp(−N·R_crit²/2)
```

This is the key finite-size effect: the probability of a
critical fluctuation decays exponentially with N.

### 5. Independent Spatial Regions

The system of size L = 2.0 contains:

```
N_cells = L/w ≈ 2.0/0.10 = 20  (in 1D)
         = (L/w)² ≈ 400        (in 2D)
```

In 1D, each region is approximately one soliton width.
In 2D, we use the grid-based density field with ~20×20 cells.

For a conservative estimate using the 1D picture:

```
N_cells ≈ 10  (accounting for boundary effects)
```

Each region nucleates independently (validated by the Poisson
distribution if that is the best fit).

### 6. Proto-Matter Abundance Formula

The expected number of condensates is:

```
⟨Q⟩ = N_cells · P(nucleation per cell)
    = N_cells · exp(−N · R_crit²/2)
```

Substituting R_crit = M_crit/M₀:

```
⟨Q⟩ = N_cells · exp(−N/2 · (M_crit/M₀)²)
```

With M₀ ≈ K · λ² · 40:

```
⟨Q⟩ = N_cells · exp(−N/2 · (M_crit/(K·λ²·40))²)
    = N_cells · exp(−N · M_crit²/(2 · K² · λ⁴ · 1600))
```

### 7. Limiting Behavior

| Limit | ⟨Q⟩ | Physical Interpretation |
|-------|-----|------------------------|
| N → 0 | N_cells | Maximum charge: one per cell |
| N → ∞ | 0 | PDE vacuum: Q=0 absolutely stable |
| K → ∞ | N_cells | Strong coupling → barrier vanishes |
| K → 0 | 0 | No coupling → no nucleation |
| λ → 0 | 0 | Zero-range coupling → no M₀ |
| λ → ∞ | N_cells | All-to-all → M₀ = K |
| K < K_c | ≈ 0 | Below nucleation barrier |

### 8. Predicted Charge Distribution

If nucleations are independent:

```
P(Q=k) = (λᵏ e^{-λ}) / k!
```

where λ = ⟨Q⟩ from the formula above.

Key properties:
- Mean = λ
- Variance = λ
- P(Q=0) = e^{-λ}
- P(Q=1) = λ e^{-λ}
- P(Q≥2) = 1 − (1+λ)e^{-λ}

If nucleations are correlated (Negative Binomial):

```
P(Q=k) = C(k+r−1, k) pʳ (1−p)ᵏ
```

with overdispersion parameter r < ∞.

### 9. Numerical Predictions

| K | λ | N | M₀ | R_crit | λ_pred | ⟨Q⟩_pred | P(Q=0)_pred |
|---|----|---|-----|--------|--------|----------|-------------|
| 0.5 | 0.05 | 100 | 0.05 | 1.06 | 0.0 | ≈0 | ≈1.0 |
| 1.0 | 0.10 | 100 | 0.40 | 0.13 | 4.3 | 4.3 | 0.014 |
| 2.0 | 0.10 | 100 | 0.80 | 0.066 | 8.0 | 8.0 | 0.0003 |
| 5.0 | 0.10 | 100 | 2.00 | 0.027 | 9.6 | 9.6 | ~0 |
| 5.0 | 0.05 | 500 | 0.50 | 0.11 | 0.48 | 0.48 | 0.62 |
| 10.0 | 0.05 | 500 | 1.00 | 0.053 | 7.1 | 7.1 | 0.001 |

### 10. Validation

The analytic prediction should be compared to ensemble simulation
results. Key validation checks:

1. **N-dependence**: ⟨Q⟩ should decrease with N.
2. **K-dependence**: ⟨Q⟩ should increase with K, saturating at N_cells.
3. **λ-dependence**: ⟨Q⟩ should increase with λ.
4. **Distribution shape**: If Poisson, Var ≈ Mean.

### 11. Connection to AT-006

AT-006's critical density ρc ≈ 0.09 corresponds to:

```
ρc: density at which M₀ first exceeds M_crit
   = density at which K · f(λ, spatial) ≈ 0.053
```

For λ = 0.05, K = 2.0: M₀ ≈ 2.0 · 0.0025 · 40 = 0.20 > 0.053 ✓
→ Charge creation is possible.

For λ = 0.01, K = 0.5: M₀ ≈ 0.5 · 0.0001 · 40 = 0.002 < 0.053 ✗
→ Q=0 is stable.

The ρc threshold is EXACTLY the point where M₀ = M_crit in the
mean-field approximation, expressed as an oscillator density.

### 12. Conclusion

Proto-matter abundance is PREDICTABLE from the field theory
parameters. The formula:

```
⟨Q⟩ = N_cells · exp(−N/2 · (M_crit/M₀)²)
```

provides a closed-form prediction for the expected number of
condensates. The accuracy of this prediction is a test of whether
the independent-nucleation (Poisson) model is correct.
