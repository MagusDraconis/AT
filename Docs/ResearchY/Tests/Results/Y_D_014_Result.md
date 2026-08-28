# ResearchY-D_014 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_014_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 8/8 tests (Duration ~17 ms)
**Filter:** `FullyQualifiedName~Y_D_014`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_014_BosonFermionSplit` | v→bosons (M_Pl=1.2234e19), m_e→fermions (m_u≈2.16 MeV) | ✅ |
| `Y_D_014_EvenOddSplit` | no canonical even/odd anchor mapping | ✅ |
| `Y_D_014_GaugeMatterSplit` | v→gauge (v=(Σm+#d)ln(span)=254.37), m_e→matter | ✅ |
| `Y_D_014_DoubletStructure` | 47 Z2 pairs spectral; no anchor-doublet link | ✅ |
| `Y_D_014_FamilyStructure` | 3 families spectral; no anchor-family link | ✅ |
| `Y_D_014_D96Consequence` | v's form D96-derived; anchor count is calibration split | ✅ |
| `Y_D_014_TwoSectors` | two anchors ↔ two sectors: EMERGENT interpretation | ✅ |
| `Y_D_014_Run` | Research report | ✅ |

## Verdicts

| Claim | Result |
|---|---|
| v = bosonic anchor | supported (M_W/M_Z/M_H/M_Pl) |
| m_e = fermionic anchor | supported (quark/lepton masses) |
| boson/fermion & gauge/matter splits | consistent (EMERGENT) |
| even/odd, doublet, family splits | no canonical anchor mapping |
| two-anchor structure from D96 | PARTIAL (v's form derived; the count is not) |
| two anchors ↔ two sectors | **EMERGENT** interpretation, not DERIVED |

## Conclusion

The two anchors {v, m_e} admit a boson/fermion (gauge/matter) interpretation, but the
two-anchor structure is **NOT a consequence of D96** — v's dimensionless form
((Σm+#d)·ln(span)) is D96-derived, while the anchor COUNT is the calibration split
(bosonic scale + fermionic scale). The two-anchor ↔ two-sector correspondence is an
**EMERGENT** interpretation, not DERIVED. **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_014"
```
