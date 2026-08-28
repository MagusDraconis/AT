# ResearchY-D_004 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_004_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 6/6 tests (Duration ~79 ms)
**Filter:** `FullyQualifiedName~Y_D_004`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_004_OccupanciesFamilies` | family count = floor(log₂ span)+1 = 3; octave bands [4,4,87] = families (DERIVED) | ✅ |
| `Y_D_004_MomentsMasses` | ladder Σm=95, Σ√m=64.08, Σm²=229 DERIVED; assignment EMERGENT; values BOUNDARY | ✅ |
| `Y_D_004_GapsCouplings` | gap λ₂=0.3864 DERIVED; α_weak/α_strong EMERGENT; 1/α_em=137 FIT | ✅ |
| `Y_D_004_Z2PairsDoublets` | 47 pairs DERIVED (ring ±k degeneracy); doublet reading EMERGENT | ✅ |
| `Y_D_004_Classification` | three-layer origin: DERIVED structure + EMERGENT assignment + BOUNDARY values | ✅ |
| `Y_D_004_Run` | Research report | ✅ |

## The Four Mappings

| Mapping | Structure | Assignment | Values |
|---|---|---|---|
| occupancies → families | DERIVED | DERIVED (identity) | — |
| moments → masses | DERIVED | EMERGENT | BOUNDARY (calibration) |
| gaps → couplings | DERIVED | EMERGENT | BOUNDARY (1/α_em fit) |
| Z2 pairs → doublets | DERIVED | EMERGENT | — |

## Conclusion — Three-Layer Origin

**DERIVED:** the spectral structure (occupancies, moments, gaps, Z2 pairs) is exact.
**EMERGENT:** the sector assignment is a supported correspondence, not a unique
derivation. **BOUNDARY:** the dimensional values require calibration anchors (v, m_e) and
the 1/α_em fit. The **families are the derived exception** — the octave bands ARE the
families (floor(log₂ span)+1 = 3, QG210). The correspondence is "supported, not unique."
**No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_004"
```
