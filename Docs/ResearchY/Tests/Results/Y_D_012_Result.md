# ResearchY-D_012 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_012_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~77 ms)
**Filter:** `FullyQualifiedName~Y_D_012`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_012_Definitions` | dimensionless structure / physical dimension / calibration anchor | ✅ |
| `Y_D_012_Candidates` | only v is dimensionful; ω₁/λ₂/zero/N/tick dimensionless | ✅ |
| `Y_D_012_NoExternal` | no candidate becomes physical without external input | ✅ |
| `Y_D_012_MinAnchorCount` | dimensionless: 0 anchors; dimensionful: v + m_e = 2 | ✅ |
| `Y_D_012_OneAnchorRefuted` | v gives M_Pl=1.2234e19 but m_u needs m_e (one anchor refuted) | ✅ |
| `Y_D_012_Trace` | D96 → ratios → ω₁ → anchor → dimensions → observables | ✅ |
| `Y_D_012_Run` | Research report | ✅ |

## Verdicts

| Item | Classification |
|---|---|
| dimensionless observables (couplings, mixings, fractions) | **DERIVED** (0 anchors) |
| energy scale (M_Pl, M_W, M_Z, M_H) | **BOUNDARY** (anchor v) |
| fermion masses (m_u = m_e·ratio) | **BOUNDARY** (anchor m_e) |
| SI units (c, ħ, GeV↔kg) | unit-convention imports |

## Conclusion

**The minimal physical anchor is the weak scale v.** One anchor is **NOT sufficient**
(refuted): v fixes the energy scale (M_Pl = 1.2234×10¹⁹ GeV), but the absolute fermion
masses require the second anchor **m_e** (m_u = m_e·Σ√m/√Σm²). **Minimal anchor count
for all derived dimensionful observables = 2 (v, m_e)**, plus c and ħ for SI units
(unit conventions, not physics anchors). Dimensionless observables need no anchor
(DERIVED). **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_012"
```
