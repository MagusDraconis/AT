# ResearchY-D_015 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_015_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 6/6 tests (Duration ~20 ms)
**Filter:** `FullyQualifiedName~Y_D_015`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_D_015_Comparison` | N=64/96/128/192/245; span window; seed 6\|N | ✅ |
| `Y_D_015_SpectralMeasures` | λ₂/ω₁ decrease with N; fam/occ per N | ✅ |
| `Y_D_015_SelectionMechanism` | E) combination: {96} = {N : 6\|N AND span<8} | ✅ |
| `Y_D_015_StructureLoss` | N≠96 loses 3-family / [4,4,87] | ✅ |
| `Y_D_015_ScaleGenerating` | N=96 unique 3-family + [4,4,87] (occMom 1900.25) | ✅ |
| `Y_D_015_Run` | Research report | ✅ |

## Comparison

| N | λ₂ | ω₁ | fam | occupancy | span |
|---|---|---|---|---|---|
| 64 | 0.8596 | 0.9272 | 3 | [4,39,20] | 4.298 |
| **96** | **0.3864** | **0.6216** | **3** | **[4,4,87]** | **6.403** |
| 128 | 0.2182 | 0.4671 | 4 | [4,4,87,32] | 8.531 |
| 192 | 0.0972 | 0.3118 | 4 | [4,4,8,175] | 12.779 |
| 245 | 0.0598 | 0.2445 | 5 | [4,4,8,212,16] | 16.301 |

## Verdict

**N=96 is unique by the COMBINATION (E)** of the period-3 seed symmetry (6|N) and the
three-family octave window (span in [4,8)): {N : 6|N AND span<8} = {96}. The scale
properties (λ₂, ω₁, Z2 pairs) are NOT unique — they shift with N; the structural
properties (3 families, [4,4,87]) ARE unique to N=96. **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_015"
```
