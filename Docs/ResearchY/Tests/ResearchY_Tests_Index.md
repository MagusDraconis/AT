# ResearchY — Tests

**Program:** ResearchY — Wave Geometry Program
**Folder purpose:** maps every investigation to its xUnit validation suite and records
test-run results. The actual xUnit suites live in `AT.Tests/ResearchY/` (mirroring the
`Docs/ResearchY` group structure); this folder is the program's test registry and result
store.

## Purpose of Tests

- fast numerical verification
- reproducibility
- regression protection
- comparison against canonical D96 values
- exploration of wave/geometry models

## Rule

Every hypothesis that can be quantified must receive at least one xUnit validation test.

## Test Naming Convention

```
ResearchY-A_001  ↔  Y_A_001_Tests.cs     (AT.Tests/ResearchY/A_WaveFoundations/)
ResearchY-B_001  ↔  Y_B_001_Tests.cs
ResearchY-C_001  ↔  Y_C_001_Tests.cs
ResearchY-D_001  ↔  Y_D_001_Tests.cs
```

## Test Registry

| Investigation | Test file | Status | Last run | Result |
|---|---|---|---|---|
| ResearchY-A_001 (Wave Origin Audit) | `AT.Tests/ResearchY/A_WaveFoundations/Y_A_001_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 5/5 PASSED — `Results/Y_A_001_Result.md` |
| ResearchY-A_002 (Difference Disturbance Audit) | `AT.Tests/ResearchY/A_WaveFoundations/Y_A_002_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_A_002_Result.md` |
| ResearchY-A_003 (Actualization Propagation Audit) | `AT.Tests/ResearchY/A_WaveFoundations/Y_A_003_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_A_003_Result.md` |
| ResearchY-A_004 (Propagation Falsification Audit) | `AT.Tests/ResearchY/A_WaveFoundations/Y_A_004_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_A_004_Result.md` |
| ResearchY-A_005 (Spectral Projection Origin) | `AT.Tests/ResearchY/A_WaveFoundations/Y_A_005_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_A_005_Result.md` |
| ResearchY-B_001 (Circular Closure Audit) | `AT.Tests/ResearchY/B_CircularGeometry/Y_B_001_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_B_001_Result.md` |
| ResearchY-B_002 (Origin of π Value Audit) | `AT.Tests/ResearchY/B_CircularGeometry/Y_B_002_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_B_002_Result.md` |
| ResearchY-C_001 (Center Audit) | `AT.Tests/ResearchY/C_SourceGeometry/Y_C_001_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_C_001_Result.md` |
| ResearchY-C_002 (Radial Propagation Audit) | `AT.Tests/ResearchY/C_SourceGeometry/Y_C_002_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_C_002_Result.md` |
| ResearchY-D_001 (Standing Wave Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_001_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_001_Result.md` |

## Result Summaries

Per-investigation result summaries are stored under `Results/` and mirrored into the
investigation's "Result summary" section.
