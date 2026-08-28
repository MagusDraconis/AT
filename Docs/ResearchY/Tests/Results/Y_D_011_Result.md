# ResearchY-D_011 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_011_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~18 ms)
**Filter:** `FullyQualifiedName~Y_D_011`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_011_UniversalReference` | ω₁ = 0.6216 is the minimum positive frequency (dimensionless reference) | ✅ |
| `Y_D_011_Dimensions` | no physical dimension from ω₁ alone (each needs an anchor, BOUNDARY) | ✅ |
| `Y_D_011_ReferenceAnalogies` | not an atomic/c/Planck reference (dimensionless) | ✅ |
| `Y_D_011_DimensionlessRatios` | ω_max/ω₁=6.40 (span), λ_max/λ₂=40.99, span/ω₁=10.30 (DERIVED) | ✅ |
| `Y_D_011_AnchorCount` | A ω₁ only DERIVED; B +v BOUNDARY; C +multiple BOUNDARY | ✅ |
| `Y_D_011_ScaleMap` | ω₁ → ratios DERIVED → dimensions BOUNDARY → observables | ✅ |
| `Y_D_011_Run` | Research report | ✅ |

## Verdicts

| Item | Classification |
|---|---|
| ω₁ as universal dimensionless reference | **DERIVED** (ratios ω_k/ω₁, λ_k/λ₂, span/ω₁ exact) |
| ω₁ as universal physical-unit reference | **BOUNDARY** (every dimension needs an anchor) |
| ω₁ as atomic / c / Planck reference | NO (dimensionless spectral frequency) |
| A) ω₁ only | DERIVED (dimensionless) |
| B) ω₁ + one anchor (v) | BOUNDARY (energy/mass) |
| C) ω₁ + multiple anchors | BOUNDARY (SI) |

## Conclusion

**ω₁ is the universal DIMENSIONLESS reference** (all spectral ratios DERIVED), **not the
universal physical-unit reference** (every dimension requires a dimensionful anchor,
D_010, BOUNDARY). Minimal anchor count: **one (v)** for energy/mass; length/time need c
and a physical time standard. The unit-attachment model is the pair (dimensionless
reference + anchor). **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_011"
```
