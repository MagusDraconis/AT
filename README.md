# THE Q-MODEL

*From Q to Cosmology*

**TQM**

*A Theory of Structure, Complexity and Random Actualization*

---

**Version 1.1** — *Native Metric-to-Operator Coupling (Program G4)*

TQM investigates whether matter, quantum behavior, and gravitation emerge from self-organizing oscillations of a temporal field.

## Publication

Zenodo archive: https://doi.org/10.5281/zenodo.20681734

## Naming

TQM is the project acronym.

The official theory name is **THE Q-MODEL** — *From Q to Cosmology*.

The acronym TQM is retained for historical continuity and repository compatibility.

## Core Hypothesis

Matter is not fundamental. Matter consists of dynamically stabilized wave structures inside a temporal field. Synchronization and resonance are more fundamental than particles.

## Primitives

- **Q** — the irreducible process of becoming (actualization)
- **Random Actualization** — Q-event locations are random within the causal structure
- **(ℓ, τ, ħ)** — the irreducible physical triple: where, when, and how much

## Emergence Chain

```
Q + Random Actualization
    ↓
Oscillation (ω₀ = 2π/τ = 1.17×10⁴⁴ Hz)
    ↓
Phase → Interference → QM
    ↓
Frequency Architecture → Particles → Atoms
    ↓
Phase Gradients → Geometry → Gravity
```

## Research Programs

| Program | Experiments | Status |
|---------|------------|--------|
| **DATA** — Cosmology & RAR | 10 (DATA-001→010) | Complete |
| **QM** — Quantum Foundations | 5 (QM-001→005) | Complete |
| **QG** — Quantum Gravity | 31 (QG-001→031) | Complete |
| **G4** — Native Metric-to-Operator Coupling | 57 phases / 171 tests | Complete |
| **TQM-F** — Foundation | 4 phases / 9 tests | Complete |
| **TQM-QG** — Actualization→Gravity Unification | 8 phases / 24 tests | Complete |
| **Total** | **115** | |

## Key Results

- **G is not fundamental**: G = ℓ²c³/ħ — gravity emerges from the triple
- **ℓ > 0 and τ > 0 are logically required** — not assumptions
- **(ℓ, τ, ħ) is the irreducible triple** — one process, three aspects
- **Oscillation = logical inevitability** — cannot be removed without destroying Q
- **Frequency architecture > raw energy** — organization determines physics
- **Gravity = phase gradient phenomenon** — attraction dominates, repulsion is Dark Energy
- **Gravity manipulation is NOT possible** (QG-023→031, 9 experiments, 1 conclusion)
- **RAR derivation**: g† = c·H₀/(2π) — zero free parameters
- **Parameter compression**: SM+GR (~26) → TQM (2+3) — ~5-8× reduction
- **Novel predictions**: w(z) = -1 + 0.015·(1+z)^(3/2), evolving g†(z), Euclid sensitivity
- **Native conformal operator** (G4): Lc = ρ⁻¹ L ρ⁻¹ ≈ Δ_g reconstructs curvature sign and magnitude from density and adjacency alone — no metric tensor, no Laplace–Beltrami import
- **Native curvature evolution law** (G4-E): R = F(ρ), Ṙ = F′(ρ)·ρ̇ with F′(ρ) < 0 — a closed curvature–density relation
- **Curvature feedback is anti-diffusive** (G4-E1): the naive feedback is a positive-feedback instability — a bounded cosmology requires an additional restoring term

## Build & Test

```bash
dotnet build TQM.Core/TQM.Core.csproj
dotnet test TQM.Tests/TQM.Tests.csproj
```

Requirements: .NET 10, MathNet.Numerics 5.0

## Documentation

- `Docs/NewChat_Start.md` — Primary project memory and research direction
- `Docs/TQM_QuantumGravity_Program.md` — Complete QG program summary
- `Docs/TQM_LabBook.md` — Detailed experiment results
- `Docs/Research/G4*.md` — Program G4 phase reports (native metric-to-operator coupling)
