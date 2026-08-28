# ResearchY-A_002 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_002_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~75 ms)
**Filter:** `FullyQualifiedName~Y_A_002`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_A_002_UniformBackground` | zero mode λ₀ = 0, ω₀ = 0, constant eigenvector (uniform rest state) | ✅ |
| `Y_A_002_LocalPerturbation` | delta on ρ = canonical Q-event; uniform modal weight |c_k|² = 1/N; Parseval Σ|ψ|² = 1 | ✅ |
| `Y_A_002_PhaseDisplacement` | phase lattice θ_k = 2πk/96; count preserved |e^{iθ}|² = 1; 2π closure periodicity | ✅ |
| `Y_A_002_ModeExcitation` | |ψ_k|² = ρ_k = μ^k/S identity (QG216); ω₁ = 0.6216; λ₄₈ = 12 | ✅ |
| `Y_A_002_PropagationAcrossC96` | generation-space spread ρ_k = μ^k/S; uniform modal coverage of all 96 sites | ✅ |
| `Y_A_002_ZeroModeAsRestState` | zero mode = undisturbed background; all positive modes oscillate (ω > 0) | ✅ |
| `Y_A_002_Run` | Research report (deterministic) | ✅ |

## Key Measured Values (deterministic, canonical)

| Quantity | Measured | Canonical |
|---|---|---|
| λ₀ (zero mode) | 0 | 0 (rest state) |
| ω₀ = √λ₀ | 0 | 0 (no oscillation) |
| modal weight of a delta | 1/96 uniform | Σ|c_k|² = 1 |
| ω₁ | 0.6216 | √λ₁ (fundamental doublet) |
| ω₄₈ | 3.4641 | √12 (self-conjugate mode) |
| generation shares ρ_k = μ^k/S | normalized (Σ = 1) | QG216 |
| phase displacement | |e^{iθ}|² = 1 (count preserved) | Ch9 phase lattice |

## Conclusion

- **C5 mode excitation** is the best interpretation of Difference: the canonical identity
  |ψ_k|² = ρ_k (QG216) makes a unit of Difference on a mode an exact mode excitation.
- **C1 (delta = all modes)** is the point-source form; **C2 (phase)** the count-preserving
  circulation form.
- Propagation is **generation-space branching** (ρ_k = μ^k/S), not spatial transport
  (n = 1 null geodesics preserved).
- The **zero mode is the undisturbed background** (λ₀ = 0, ω₀ = 0, constant).
- **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_002"
```
