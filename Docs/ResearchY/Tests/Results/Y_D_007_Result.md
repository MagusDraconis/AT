# ResearchY-D_007 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_007_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~25 ms)
**Filter:** `FullyQualifiedName~Y_D_007`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_007_DimensionlessRatio` | A³ = (Σm·#g·occ₂)³ = 4.8094e16 (DERIVED) | ✅ |
| `Y_D_007_Moments` | Σm=95, #g=44, occ₂=87 derived; Σ√m=64.08, Σm²=229 | ✅ |
| `Y_D_007_OccMomSpan` | occMom=1900.25, span=6.40 dimensionless invariants | ✅ |
| `Y_D_007_ResonanceStructure` | occ₂=87 dense-band resonance output | ✅ |
| `Y_D_007_ClosureInvariants` | moments, span, Z2 pairs, algebraic spectrum — invariant | ✅ |
| `Y_D_007_AbsoluteScale` | M_Pl = v·A³ = 1.2234e19 GeV requires anchor v (BOUNDARY) | ✅ |
| `Y_D_007_Run` | Research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| A) derived dimensionless Planck ratio (A³ = 4.8094e16) | **DERIVED** (exact D96 spectral content) |
| B) derived Planck scale | **NOT DERIVED** — requires anchor v |
| C) requires anchor | **YES** — weak scale v (calibration) |
| D) requires c, ħ, G import | **YES** — for the SI value of G (ħc/M_Pl², GeV↔kg) |

## Conclusion

The dimensionless Planck structure is **DERIVED** from the D96 spectral content
(A³ = (Σm·#g·occ₂)³ = 4.8094×10¹⁶, a pure number). The absolute Planck scale M_Pl = v·A³
= 1.2234×10¹⁹ GeV **requires the calibration anchor v** (weak scale, GeV unit). The SI
value G = ħc/M_Pl² **imports c, ħ, and the GeV↔kg conversion**. The Planck scale is
**calibrated, not derived** (consistent with the claim registry: gravity = calibration).
**No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_007"
```
