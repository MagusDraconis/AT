# ResearchY-D_009 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_009_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~19 ms)
**Filter:** `FullyQualifiedName~Y_D_009`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_009_ZeroMode` | ω₀ = 0, constant (reference state) | ✅ |
| `Y_D_009_MinimumExcitation` | ω₁ = 0.6216 > 0, the minimum positive frequency | ✅ |
| `Y_D_009_MinimumDifference` | ω₁ is the smallest positive separation from ω₀ | ✅ |
| `Y_D_009_ActualizationEvent` | structure DERIVED; count-event identification EMERGENT | ✅ |
| `Y_D_009_NoStateBetween` | spectral gap λ₂=ω₁²=0.3864; 0 states in (0,ω₁); multiplicity 2 | ✅ |
| `Y_D_009_Classification` | A/B DERIVED; C EMERGENT; D BOUNDARY | ✅ |
| `Y_D_009_Run` | Research report | ✅ |

## Verdicts

| Option | Answer | Classification |
|---|---|---|
| A) first frequency | YES — ω₁ = min positive ω | **DERIVED** |
| B) first difference | YES — min non-zero separation from ω₀ | **DERIVED** |
| C) first actualization | PARTIAL — interpretive reading | **EMERGENT** |
| D) natural clock only | NO — more than a clock | physical clock **BOUNDARY** |

## Conclusion

**ω₁ IS the minimum non-zero excitation.** The spectral gap λ₂ = ω₁² = 0.3864 is the
smallest positive eigenvalue; the interval (0, ω₁) contains **zero** spectral states
(verified). ω₁ = 0.6216 is therefore the first frequency and first difference above the
zero mode (DERIVED, invariant under ring automorphisms). "First actualization" is an
EMERGENT interpretation of the minimum excitation; as a physical clock ω₁ is BOUNDARY
(D_008: dimensionless only). **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_009"
```
