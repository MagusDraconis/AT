# Y_D_046_Result.md — ResearchY-D_046 ResearchY-Predictions Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_046_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_046"`

---

## Summary

**Question:** What new predictions follow from ResearchY results that V2.0 could not
state?

**Verdict:** ResearchY produces **8 structurally new predictions (P1–P8)** absent from
V2.0 wording, each a consequence of the origin chain with a dependency chain and
falsification path.

## Prediction catalog

| # | Sector | Classification | Prediction (verified) |
|---|---|---|---|
| P1 | gauge | **THEOREM** | spectral doublets are O(2)-type, not SU(2) |
| P2 | gauge | **NECESSITY** | su(2) compact-form emergent from unitarity |
| P3 | closure | **THEOREM** | only 96 is a zero-defect octave rung (11 rings, rung {96}) |
| P4 | resonance | **NECESSITY** | ω₁ ≈ √91·(2π/N) (9.50 vs 9.54, asymptotic) |
| P5 | resonance | **THEOREM** | span N-specific (4.02/6.40/12.78); no universal ratio |
| P6 | anchor | **CORRESPONDENCE** | v = 137·ln(span) = 254.37 GeV |
| P7 | anchor | **BOUNDARY** | v/m_e ≈ 4.98e5 irreducible |
| P8 | family | **THEOREM** | 3 = floor(log₂ 6.4025)+1 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_046_GaugeSector` | P1 + P2 | ✅ |
| `Y_D_046_ClosureSector` | P3 | ✅ |
| `Y_D_046_ResonanceSector` | P4 + P5 | ✅ |
| `Y_D_046_AnchorSector` | P6 + P7 | ✅ |
| `Y_D_046_FamilySector` | P8 | ✅ |
| `Y_D_046_Run` | research report | ✅ |

## Conclusion

ResearchY produces 8 structurally new predictions (P1–P8) absent from V2.0, each a
consequence of the origin chain with a dependency chain and falsification path.
P1/P3/P5/P8 are theorems, P2/P4 necessities, P6 a correspondence, P7 a boundary.
No canonical changes; research only.
