# Charge Phase Diagram

## AT-123: Density × Coupling Phase Diagram for Proto-Matter

### Abstract

We construct the (density ρ_Q × coupling K) phase diagram for
proto-matter — the collective system of topological charge quanta
Q=+1. Six phases are identified: Vacuum, Dilute Gas, Correlated Gas,
Cluster Phase, Percolating Phase, and Dense Matter. Phase boundaries
are determined by nucleation probability, coupling range λ, and
charge merger rate γ.

### Phase Regions

```
ρ_Q ↑
    │  Dense Matter          │ Percolating Phase    │
    │  (ρ_Q → 1, R → 1)     │ (system-spanning)    │
    │                        │                      │
    │  Cluster Phase         │ Cluster              │
    │  (bound groups 2-5)    │ (with percolation)   │
    │                        │                      │
    │  Correlated Gas        │ Correlated Gas       │
    │  (weak g(r) peak)      │                      │
    │                        │                      │
    │  Dilute Gas            │ Dilute Gas           │
    │  (g(r)≈1, independent) │                      │
    │                        │                      │
    │  Vacuum (Q=0)          │ Vacuum               │
    └────────────────────────┴──────────────────────→ K
         K < K_c                  K > K_c
    (no nucleation)           (nucleation active)
```

### Phase Boundaries

1. **Vacuum ↔ Gas**: K_c ≈ D_R/(c₀·w²·λ²·40·N) — nucleation threshold (AT-118)
2. **Dilute Gas ↔ Correlated Gas**: d_typ ~ 5λ — coupling range crossover
3. **Correlated Gas ↔ Cluster**: ρ_Q threshold for percolation on coupling graph (~0.08 at tested N)
4. **Cluster ↔ Percolating**: clusters merge into system-spanning network at ρ_Q ~ 0.15
5. **Percolating ↔ Dense**: ρ_Q → 1, global R → 1

### Critical Parameters

| Boundary | Critical Value | Physical Meaning |
|----------|---------------|------------------|
| K_c (nucleation) | ~0.5-2.0 | Coupling needed for charge creation |
| ρ_perc (percolation) | ~0.09 | AT-006's ρc reinterpreted |
| ρ_dense | ~0.30 | Near-complete condensation |

### Relationship to AT-006

AT-006's critical density ρc ≈ 0.09 is the PERCOLATION THRESHOLD
of the charge network. Below ρc: vacuum or dilute gas. Above ρc:
charges nucleate and, at sufficient density, form a percolating
network. The AT-006 phase diagram is the R-axis projection of
the full (ρ_Q, K) phase diagram.

### Universality

The phase diagram structure is predicted to be universal across
system sizes N, with finite-size scaling effects near phase
boundaries. The percolation threshold ρ_perc may shift with λ
(spatial coupling range) and N.

### Future Directions

- Higher N (1000+) to access thermodynamic limit
- Higher K (50+) to probe dense matter
- Larger λ (0.5+) to extend interaction range
- Dynamic phase transitions (time-dependent K or density)
