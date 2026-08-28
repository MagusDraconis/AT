# ResearchY-D_003 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_003_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 6/6 tests (Duration ~21 ms)
**Filter:** `FullyQualifiedName~Y_D_003`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_003_ModeOccupation` | [4,4,87], occMom=1900.25 — derived from the spectrum | ✅ |
| `Y_D_003_ResonantPairAccess` | 47 Z2 pairs (structure derived); sector role is a mapping | ✅ |
| `Y_D_003_ZeroModeRole` | λ₀=ω₀=0, uniform reference — derived | ✅ |
| `Y_D_003_ObservableProjection` | moments derived; sector mapping emergent; anchors boundary | ✅ |
| `Y_D_003_SpectralInvariants` | span 6.40, moments, Z2 pairs, algebraic spectrum — invariant | ✅ |
| `Y_D_003_Run` | Research report | ✅ |

## Classification

| Quantity | Resonance alone? | Classification |
|---|---|---|
| mode occupation [4,4,87], occMom | YES | **DERIVED** |
| resonant pair structure | YES | **DERIVED** |
| resonant pair sector role | NO (mapping) | **EMERGENT** |
| zero-mode role | YES | **DERIVED** |
| spectral moments | YES | **DERIVED** |
| sector projection | NO (correspondence) | **EMERGENT** |
| dimensional masses/couplings | NO (anchors, fit) | **BOUNDARY** |
| spectral invariants | YES | **DERIVED** |

## Conclusion

**Resonance alone generates the SPECTRAL observables (DERIVED):** mode occupation, pair
structure, zero-mode role, spectral invariants. **It does NOT generate the PHYSICAL
observables:** the sector mapping is a correspondence (EMERGENT) and the dimensional
values require calibration anchors v, m_e and the fit 1/α_em (BOUNDARY). Resonance is the
spectral source of the numbers, not the complete generator of physical observables. **No
canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_003"
```
