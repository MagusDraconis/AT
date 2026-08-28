# ResearchY-D_001 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_001_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~22 ms)
**Filter:** `FullyQualifiedName~Y_D_001`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_001_FormalDefinition` | standing wave = time-harmonic eigenfunction; mode periodic | ✅ |
| `Y_D_001_EigenmodeExpression` | L φ_k=λ_k φ_k; ω_k=√λ_k; cos/sin harmonics; Z2 pair λ_k=λ_{N−k} | ✅ |
| `Y_D_001_GeometricVsSpectral` | geometric (pattern, node at n=24 for k=1) vs spectral (ω) — both centerless | ✅ |
| `Y_D_001_ZeroMode` | λ₀=ω₀=0, constant — uniform rest state (degenerate standing wave) | ✅ |
| `Y_D_001_ResonantPairs` | 47 Z2 pairs; 42 doublets + 5 + 6 groups; fundamental doublet ω₁=0.6216 | ✅ |
| `Y_D_001_Classification` | hybrid (spatial + spectral), translation-invariant mode set | ✅ |
| `Y_D_001_Run` | Research report | ✅ |

## Verdict

**Standing waves exist on C96 without center-based geometry — YES.**

| Claim | Result |
|---|---|
| standing waves on C96 | YES — Fourier modes are time-harmonic eigenfunctions |
| center required | NO — modes translation-invariant (node positions depend only on k) |
| zero mode | ω₀=0 uniform rest state (degenerate standing wave) |
| resonant pairs | YES — 42 Z2 doublets + 5 + 6 groups (degenerate) |
| spatial-only / spectral-only | NO — the standing structure needs both pattern AND frequency |
| classification | **HYBRID, center-free** (spatial harmonics + spectral frequencies) |

## Conclusion

Standing waves on C96 are the eigenmodes of the graph Laplacian — time-harmonic Fourier
harmonics with frequencies ω_k = √λ_k. Both the spatial pattern (translation-invariant)
and the spectrum (rotation-invariant) are centerless, consistent with C_001/C_002. The
standing structure is a **center-free hybrid**. **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_001"
```
