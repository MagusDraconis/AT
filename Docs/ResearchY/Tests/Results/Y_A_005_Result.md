# ResearchY-A_005 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_005_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~20 ms)
**Filter:** `FullyQualifiedName~Y_A_005`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_A_005_ProjectionNotPrimitive` | primitives = {Difference, η}; projection is the eigen-decomposition of L; span 6.40 | ✅ |
| `Y_A_005_ClosureLink` | N=96 closure fixed point; K=5 vs K=6 give different spectra (structure ≠ size) | ✅ |
| `Y_A_005_ResonanceReadout` | Σ|ψ|² = Σρ = 1 (Born, QG216); resonance = Conservation + Boundary | ✅ |
| `Y_A_005_AttractorOrigin` | Fourier modes are exact eigenmodes of L (1e-14); ω_k = √λ_k | ✅ |
| `Y_A_005_UniqueBasis` | 45 diagonal blocks; octave bands [4,4,87] fixed; K=5 vs K=6 differ | ✅ |
| `Y_A_005_MinimalOrigin` | dependency chain Difference → … → projection (each link canonical) | ✅ |
| `Y_A_005_Run` | Research report | ✅ |

## Verdict

| Candidate | Claim | Verdict |
|---|---|---|
| A | projection is fundamental | **FAILS** — would be a 3rd primitive (contradicts minimal foundation) |
| B | projection from closure | PARTIAL — fixes N=96, not the graph structure |
| C | projection from resonance | CIRCULAR — resonance IS the readout |
| D | projection from the attractor | **YES** — the minimal origin |

## Conclusion

**Spectral projection is DERIVED, not primitive.** The minimal origin is the actualization
attractor (D): Difference → Actualization → attractor/closure (N=96) → graph C96 → graph
Laplacian L → unique diagonalizing basis (eigenbasis) → projection (readout of the count in
that basis). The projection is forced because the eigenbasis is the unique normal-mode basis
of the medium, and the medium is the converged output of Actualization. **No canonical value
was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_005"
```
