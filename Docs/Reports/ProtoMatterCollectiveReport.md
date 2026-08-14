# TQM-123: Proto-Matter Collective Dynamics

## Executive Summary

**Classification: B — Weak Collective Effects**

TQM-123 investigates the collective behavior of many interacting
topological charge quanta (Q=+1). Multi-charge ensemble simulations
across density × coupling parameter space reveal that at tested
parameters, collective effects are weak — proto-matter behaves
primarily as a dilute gas of weakly-interacting charges.

However, the theoretical framework for collective phases (gas,
correlated gas, cluster, percolating, dense matter) is established
and the phase diagram construction methodology is validated.

## 1. From Single to Many Charges

TQM-117..122 established Q=+1 as the fundamental charge quantum:
a stable topological droplet of minimum width w_c ≈ 0.05.

When multiple Q=+1 droplets coexist:
- They interact via phase coupling gradients within range ~5λ
- They can merge when within coupling range (Q=2→Q=1)
- Collective phases may emerge at sufficient density

## 2. Collective Phases

Six phases were theoretically identified:

| Phase | Density | Characteristics |
|-------|---------|----------------|
| Vacuum | Q=0 | No condensates, global R<0.5 |
| Dilute Gas | Q≥1, d≫5λ | Independent charges, g(r)≈1 |
| Correlated Gas | d~3-10λ | Weak pair correlations |
| Cluster Phase | d<3λ (some) | Bound clusters of 2-5 |
| Percolating Phase | High ρ_Q | System-spanning charge network |
| Dense Matter | ρ_Q→1 | Crystalline or liquid order |

## 3. Ensemble Results

Scan: 3K × 2λ × 1N × 3Q × 2layouts × 3seeds = 108 runs.

**Phases observed**: Dilute Gas, Cluster Phase

At the tested parameters (K∈[1,10], λ∈[0.05,0.15], N=100, Q∈[1,5]):
- Dilute Gas dominates at low density
- Cluster Phase appears at higher density with clustered layout
- No percolation or dense matter phases at tested parameters
- Independent-charge behavior is the dominant mode

## 4. Collective Framework

The continuum charge density equation was derived:

```
∂ρ_Q/∂t = D_eff·∇²ρ_Q + ν·(ρ_max−ρ_Q) − γ·ρ_Q² − μ·ρ_Q
```

where:
- D_eff = effective charge diffusivity
- ν = nucleation rate (TQM-118)
- γ = binary merger rate (TQM-012)
- μ = spontaneous decay rate (≈0)

## 5. Validation

Prior experiments reinterpreted through the collective framework:

| TQM | Collective Interpretation |
|-----|--------------------------|
| TQM-005 | Resonance clusters = Cluster phase |
| TQM-006 | ρc = charge percolation threshold |
| TQM-010 | Multi-cluster = multi-charge initial states |
| TQM-012 | Two-condensate = binary collision, measures γ |
| TQM-118 | Charge creation = source term ν |
| TQM-119 | Charge statistics = counting statistics of charge gas |

## 6. Conclusion

Proto-matter collective dynamics are detected but weak at tested
parameters. The theoretical infrastructure — phase diagram,
continuum equation, phase classification — is complete and
ready for deeper parameter exploration. Increasing density
(ρ_Q → 1) or coupling strength (K → 20+) should reveal stronger
collective phases.
