# ResearchY-D_017 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_017_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 10/10 tests (Duration ~87 ms)
**Filter:** `FullyQualifiedName~Y_D_017`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_017_Scan` | λ₂/ω₁ strictly decreasing; [4,4,87] at N=96 | ✅ |
| `Y_D_017_DeltaNStability` | ΔN=±1 rel change decreases with N (not N=96-special) | ✅ |
| `Y_D_017_Robustness` | N±1/±2/±6 — monotone stability trend | ✅ |
| `Y_D_017_ScalePersistence` | λ₂/ω₁ smooth; occupancy local to N=96 | ✅ |
| `Y_D_017_MinExcitation` | ω₁ min excitation; Z2 doublet at all N | ✅ |
| `Y_D_017_InfoSeparation` | occMom smooth; not an extremum at N=96 | ✅ |
| `Y_D_017_SpectralDensity` | band1 = 4 for all N in the window | ✅ |
| `Y_D_017_Selection` | D) closure-selected, not scale/resonance/family | ✅ |
| `Y_D_017_StabilityScore` | stability increases with N (trivial trend) | ✅ |
| `Y_D_017_Run` | Research report | ✅ |

## Selection

| Option | Verdict |
|---|---|
| A) family-selected | partial (window [60,120], D_016) |
| B) scale-selected | **NO** (λ₂/ω₁ monotone in N) |
| C) resonance-selected | NO (Z2 doublet at all N) |
| D) closure-selected | **YES** (Ch5 attractor fixed point) |

## Conclusion

**λ₂ and ω₁ do NOT select N=96** — they are strictly monotone in N, and scale stability
improves with N (a trivial λ₂ ~ 1/N² trend; stability scores 50→145 from N=96→288).
N=96 is **closure-selected (D)**: the attractor fixed point of the actualization dynamics
(Ch5). The [4,4,87] occupancy is N=96-specific (structural) but not a stability
property. **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_017"
```
