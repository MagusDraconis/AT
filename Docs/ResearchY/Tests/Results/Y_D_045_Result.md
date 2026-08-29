# Y_D_045_Result.md — ResearchY-D_045 Cosmological-Anchor Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_045_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_045"`

---

## Summary

**Question:** Can cosmological scaling generate v and m_e?

**Verdict:** **NO — the anchors are INDEPENDENT of the cosmological state (option A).**
The density state ρ produces **DIMENSIONLESS fractions only**: ΩΛ = I_occ/ln K =
0.7513/ln 3 = **0.6839**, Ωm = 1−ΩΛ = **0.3161** (QG234, DERIVED). No cosmological
ratio matches the anchor ratios. v = 137·ln(span) = 254.37 GeV is a **spectral**
quantity (span N-fixed, D_028), not ρ-dependent; m_e has no construction from ρ.

## Key measured values

| Quantity | Value | Matches anchor? |
|---|---|---|
| ΩΛ = I_occ/ln K | 0.6839 | NO |
| Ωm = 1−ΩΛ | 0.3161 | NO |
| ΩΛ/Ωm | 2.16 | NO (v/m_e ≈ 4.98e5) |
| I_occ | 0.7513 nats | NO |
| v/m_e | 4.98e5 | — |
| m_e/v | ~2e-6 | no ρ-quantity near it |
| ln(v/m_e) | ~13.1 | no ρ-quantity near it |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_045_DensityScaling` | ρ produces dimensionless fractions only | ✅ |
| `Y_D_045_VOrigin` | v = 137·ln(span) — spectral, not ρ-dependent | ✅ |
| `Y_D_045_ElectronOrigin` | m_e — no construction from ρ | ✅ |
| `Y_D_045_CommonSource` | no ρ-ratio matches v/m_e ≈ 5e5 | ✅ |
| `Y_D_045_RatioEvolution` | ρ change moves ΩΛ/Ωm, not v/m_e/v-m_e | ✅ |
| `Y_D_045_Run` | Research report | ✅ |

## Conclusion

Cosmological scaling does **NOT** generate v and m_e. The density state ρ produces
only dimensionless fractions (ΩΛ = 0.6839, Ωm = 0.3161, DERIVED); no ρ-ratio matches
the anchor ratios (v/m_e ≈ 4.98e5, m_e/v ≈ 2e-6, ln ≈ 13.1). v is a spectral quantity
(137·ln span), m_e is a boundary value — both independent of ρ. **Option A: anchors
independent.** No new primitive; canonical AT unchanged.
