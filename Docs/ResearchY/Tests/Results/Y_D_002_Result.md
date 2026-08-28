# ResearchY-D_002 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_002_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~18 ms)
**Filter:** `FullyQualifiedName~Y_D_002`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_002_ModeDecomposition` | 95 positive Fourier modes + 1 zero mode; complete, periodic | ✅ |
| `Y_D_002_ResonantPairs` | λ_k=λ_{N−k} (Z2 degenerate); ω_k=ω_{N−k} | ✅ |
| `Y_D_002_ZeroMode` | λ₀=ω₀=0, constant — uniform rest state | ✅ |
| `Y_D_002_Z2Pairs` | 47 pairs → 94 + self-conjugate (k=48, λ=12) = 95; multiset 42×2+5+6 | ✅ |
| `Y_D_002_SpatialSpectral` | hybrid: spatial harmonics × spectral eigenvalues | ✅ |
| `Y_D_002_ClosureConsistency` | R^N=id; θ_{k+N}≡θ_k; z_k^N=1; algebraic spectrum | ✅ |
| `Y_D_002_Run` | Research report | ✅ |

## Model Summary

| Component | Content |
|---|---|
| mode decomposition | Ψ = Σ[a cos + b sin]cos(ωt); 95+1 modes, complete/orthogonal |
| resonant pairs | 47 Z2 pairs (λ_k=λ_{N−k}); 94 paired + 1 self-conjugate (k=48) |
| zero mode | ω₀=0 uniform rest state (reference) |
| 47 Z2 analysis | 42 doublets + 5-group + 6-group; doublet = ring degeneracy |
| spatial/spectral | hybrid — pattern (geometric) × frequency (spectral) |
| closure | R^N=id, θ_{k+N}≡θ_k, z^N=1; algebraic spectrum |

## Classification: HYBRID (center-free)

The canonical standing wave model is the center-free hybrid decomposition of the closed
ring's algebraic spectrum: spatial harmonics × spectral eigenvalues, 47 Z2 pairs +
self-conjugate mode, zero mode as reference, closure-consistent (C_001/C_002/D_001/
B_002/B_003). **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_002"
```
