# Y_D_044_Result.md — ResearchY-D_044 Anchor-Origin Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_044_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_044"`

---

## Summary

**Question:** What is the physical origin of v and m_e? Can the anchor values be
derived or are they irreducible physical constants?

**Verdict:** v and m_e are **observable-sector BOUNDARY values**, not hidden outputs.
**v has a PARTIALLY-DERIVED structure**: v = 137·ln(span) = 254.37 GeV (QG168) where
137 = Σm+#d (the fine-structure denominator) and ln(span) = ln 6.4025 are D96-derived;
only the **GeV UNIT** is the boundary anchor. **m_e = 0.511 MeV has NO D96
construction** — a pure boundary value (the fermionic anchor, D_014).

## Key measured values

| Quantity | Value | Status |
|---|---|---|
| v = 137·ln(6.4025) | 254.37 | dimensionless structure DERIVED (QG168) |
| 137 = Σm + #d | 137 (95+42) | DERIVED (1/α_em denominator) |
| ln(span) | 1.8567 | DERIVED (D_028) |
| m_e | 0.511 MeV | BOUNDARY (no construction) |
| v/m_e | ~4.98e5 | NOT canonical (D_013) |
| ln(v/m_e) | ~13.12 | NOT canonical |
| M_Pl/v = A³ | 4.8094e16 | DERIVED (D_007) |
| m_e/ω₁ | ~8.2e-4 | not spectral |
| v/ω₁ | ~409 | not spectral |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_044_VOrigin` | v = 137·ln(span) = 254.37 — structure DERIVED | ✅ |
| `Y_D_044_ElectronOrigin` | m_e has no D96 construction (BOUNDARY) | ✅ |
| `Y_D_044_AnchorReplacement` | replacing v/m_e preserves dimensionless physics | ✅ |
| `Y_D_044_RatioAnalysis` | M_Pl/v = A³ DERIVED; v/m_e not canonical | ✅ |
| `Y_D_044_DependencyTrace` | Difference → Spectrum → v/m_e → boundary | ✅ |
| `Y_D_044_Run` | Research report | ✅ |

## Conclusion

v and m_e are **observable-sector BOUNDARY values**, not hidden outputs of a deeper
process. **v's dimensionless structure (137·ln span = 254.37, QG168) is DERIVED** —
only the GeV unit is boundary; **m_e (0.511 MeV) has no D96 construction** (pure
boundary). Neither defines the other (v/m_e ≈ 5e5 not canonical, D_013). M_Pl/v = A³
is DERIVED (D_007). Replacing an anchor re-scales its sector; the dimensionless
structure survives. No new primitive; canonical AT unchanged.
