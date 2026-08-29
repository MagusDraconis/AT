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
| ResearchY-B_003 (Origin of 2π Audit) | `AT.Tests/ResearchY/B_CircularGeometry/Y_B_003_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_B_003_Result.md` |
| ResearchY-D_002 (Standing Wave Model) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_002_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_002_Result.md` |
| ResearchY-D_003 (Resonance Observables Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_003_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_D_003_Result.md` |
| ResearchY-D_004 (Sector Mapping Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_004_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_D_004_Result.md` |
| ResearchY-D_005 (Moment Ordering Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_005_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_D_005_Result.md` |
| ResearchY-D_006 (Assignment Constraints Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_006_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_D_006_Result.md` |
| ResearchY-D_007 (Planck Scale Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_007_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_007_Result.md` |
| ResearchY-D_008 (Reference Unit Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_008_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_D_008_Result.md` |
| ResearchY-D_009 (Minimum Excitation Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_009_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_009_Result.md` |
| ResearchY-D_010 (Unit Anchoring Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_010_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_010_Result.md` |
| ResearchY-D_011 (Universal Reference Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_011_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_011_Result.md` |
| ResearchY-D_012 (Minimal Anchor Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_012_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_012_Result.md` |
| ResearchY-D_013 (Anchor Reduction Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_013_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 8/8 PASSED — `Results/Y_D_013_Result.md` |
| ResearchY-D_014 (Two-Anchor Structure Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_014_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 8/8 PASSED — `Results/Y_D_014_Result.md` |
| ResearchY-D_015 (N=96 Uniqueness Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_015_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 6/6 PASSED — `Results/Y_D_015_Result.md` |
| ResearchY-D_016 (Family-Count Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_016_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 7/7 PASSED — `Results/Y_D_016_Result.md` |
| ResearchY-D_017 (Scale Stability Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_017_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 10/10 PASSED — `Results/Y_D_017_Result.md` |
| ResearchY-D_018 (Occupancy Selection Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_018_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 11/11 PASSED — `Results/Y_D_018_Result.md` |
| ResearchY-D_019 (Closure-Only Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_019_Tests.cs` | ACTIVE | 2026-08-28 | ✅ 8/8 PASSED — `Results/Y_D_019_Result.md` |
| ResearchY-D_020 (Selection Precondition Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_020_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_020_Result.md` |
| ResearchY-D_021 (Oscillation Symmetry Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_021_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_021_Result.md` |
| ResearchY-D_022 (Weak-Isospin Entry Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_022_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_022_Result.md` |
| ResearchY-D_023 (SU(2) Entry Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_023_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_023_Result.md` |
| ResearchY-D_024 (Doublet Compatibility Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_024_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_024_Result.md` |
| ResearchY-D_025 (Three-Generator Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_025_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_025_Result.md` |
| ResearchY-D_026 (Compact-Form Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_026_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_026_Result.md` |
| ResearchY-D_027 (Selector-Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_027_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_027_Result.md` |

## Result Summaries

Per-investigation result summaries are stored under `Results/` and mirrored into the
investigation's "Result summary" section.
