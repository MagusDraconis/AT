# ResearchY-D_013 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_013_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 8/8 tests (Duration ~16 ms)
**Filter:** `FullyQualifiedName~Y_D_013`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_013_Definitions` | anchor / projection / calibration | ✅ |
| `Y_D_013_H1` | m_e from v REFUTED (f=2e-6, not a canonical ratio) | ✅ |
| `Y_D_013_H2` | v from m_e REFUTED (g=5e5, not canonical; GeV unit independent) | ✅ |
| `Y_D_013_H3` | common A0 REFUTED (new primitive) | ✅ |
| `Y_D_013_Ratios` | v/m_e=4.98e5; v/ω₁, m_e/ω₁, v/A³, m_e/A³ — no link | ✅ |
| `Y_D_013_Invariants` | no common spectral source/moment/resonance/closure scale | ✅ |
| `Y_D_013_AnchorCount` | 2 → irreducible (v, m_e independent) | ✅ |
| `Y_D_013_Run` | Research report | ✅ |

## Verdicts

| Hypothesis | Result |
|---|---|
| H1 (v fundamental, m_e from v) | **REFUTED** — no canonical m_e = v·f |
| H2 (m_e fundamental, v from m_e) | **REFUTED** — v's GeV unit anchor independent of m_e |
| H3 (common anchor A0) | **REFUTED** — no canonical A0 (new primitive) |

## Conclusion

**v and m_e are independent, irreducible anchors.** No canonical reduction lowers the
anchor count from 2 to 1: H1, H2, and H3 all fail without new primitives, fitted
constants, or breaking D_012. No common invariant (spectral source, moment, resonance,
or closure scale) links them. **Anchor count: 2 → irreducible.** **No canonical value was
changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_013"
```
