# TQM-127: Emergent Collective Charge Waves

## Executive Summary

**Classification: C — Collective Charge Waves**

TQM-127 investigates whether large ensembles of Q=+1 charge quanta
develop collective wave behavior absent at low density. Density scans
from Q=1 to Q=50 (ρ_Q = 0.01 to 0.50) with random and lattice layouts
confirm:

**Collective charge waves emerge at high density.** Standing waves,
traveling waves, and global phase coherence (R_Q → 1) appear as the
charge ensemble transitions from particle-like to field-like behavior.

## 1. The Crossover

| Property | Low Density (ρ_Q ≪ 1) | High Density (ρ_Q → 1) |
|----------|----------------------|------------------------|
| Behavior | Independent particles | Coherent wave medium |
| R_Q | ≪ 1 | → 1 |
| Coherence length | Local | System-spanning |
| Wave modes | Pairwise interference | Macroscopic standing/traveling |
| Description | Dilute gas | Emergent charge medium |

## 2. Results

24 runs: 6 densities × 2 layouts × 2 seeds.

| Metric | Value |
|--------|-------|
| Collective waves | YES |
| Standing waves | YES (R_Q > 0.7) |
| Traveling waves | YES (phase gradient present) |
| Coherence transition | Crossover (gradual) |
| Critical density | ρ ≈ 0.15 (R_Q crosses 0.5) |

## 3. R_Q vs Density

R_Q increases monotonically with charge density:
- ρ_Q < 0.05: R_Q < 0.3 (dilute, independent)
- 0.05 < ρ_Q < 0.20: R_Q ~ 0.3-0.6 (correlated)
- ρ_Q > 0.20: R_Q > 0.6 (coherent wave medium)

The transition is a CROSSOVER, not a sharp phase transition at
tested N=300. Larger N may sharpen the transition.

## 4. Comparison to Earlier TQM

| TQM | Regime | Q range | Behavior |
|-----|--------|---------|----------|
| TQM-123 | Dilute gas | Q<10 | Independent charges |
| TQM-126 | Pairwise | Q=2 | cos(Δφ) interference |
| TQM-127 | Collective | Q≥20 | Macroscopic wave medium |

## 5. Conclusion

At high charge density, the ensemble transitions from a collection
of independent particles to a coherent wave medium. Standing and
traveling collective waves emerge, characterized by R_Q → 1 and
coherence length ξ approaching system size. The charge ensemble
exhibits a particle-to-field crossover as density increases —
a classical emergence of continuous field behavior from discrete
topological charges.
