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
ResearchY-R_001  ↔  Y_R_001_Tests.cs     (AT.Tests/ResearchY/R_BoundaryProgram/)
ResearchY-M_001  ↔  Y_M_001_Tests.cs     (AT.Tests/ResearchY/M_Measurement/)
ResearchY-M_009  ↔  Y_M_009_Tests.cs     (AT.Tests/ResearchY/M_Measurement/)
ResearchY-M_010  ↔  Y_M_010_Tests.cs     (AT.Tests/ResearchY/M_Measurement/)
ResearchY-NP_003  ↔  Y_NP_003_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_004  ↔  Y_NP_004_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_005  ↔  Y_NP_005_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_006  ↔  Y_NP_006_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_007  ↔  Y_NP_007_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_008  ↔  Y_NP_008_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_009  ↔  Y_NP_009_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_010  ↔  Y_NP_010_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_011  ↔  Y_NP_011_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_012  ↔  Y_NP_012_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_013  ↔  Y_NP_013_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_014  ↔  Y_NP_014_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_015  ↔  Y_NP_015_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_016  ↔  Y_NP_016_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_017  ↔  Y_NP_017_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_018  ↔  Y_NP_018_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_019  ↔  Y_NP_019_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_020  ↔  Y_NP_020_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_021  ↔  Y_NP_021_Tests.cs    (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-QG_001  ↔  Y_QG_001_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_002  ↔  Y_QG_002_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_003  ↔  Y_QG_003_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_004  ↔  Y_QG_004_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_005  ↔  Y_QG_005_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_006  ↔  Y_QG_006_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_007  ↔  Y_QG_007_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_008  ↔  Y_QG_008_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_009  ↔  Y_QG_009_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_010  ↔  Y_QG_010_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_011  ↔  Y_QG_011_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_012  ↔  Y_QG_012_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_013  ↔  Y_QG_013_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_014  ↔  Y_QG_014_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_015  ↔  Y_QG_015_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_016  ↔  Y_QG_016_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_017  ↔  Y_QG_017_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-QG_018  ↔  Y_QG_018_Tests.cs     (AT.Tests/ResearchY/QG_GeometryBridge/)
ResearchY-NP_022  ↔  Y_NP_022_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_023  ↔  Y_NP_023_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_024  ↔  Y_NP_024_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_025  ↔  Y_NP_025_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_026  ↔  Y_NP_026_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_027  ↔  Y_NP_027_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_028  ↔  Y_NP_028_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_029  ↔  Y_NP_029_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_030  ↔  Y_NP_030_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_031  ↔  Y_NP_031_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_032  ↔  Y_NP_032_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-NP_033  ↔  Y_NP_033_Tests.cs     (AT.Tests/ResearchY/NP_NewPhysics/)
ResearchY-S_001  ↔  Y_S_001_Tests.cs       (AT.Tests/ResearchY/S_Synthesis/)
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
| ResearchY-D_028 (Span-Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_028_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_028_Result.md` |
| ResearchY-D_029 (Closure-Defect Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_029_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_029_Result.md` |
| ResearchY-D_030 (Octave-Rung Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_030_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_030_Result.md` |
| ResearchY-D_031 (Seed-Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_031_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_031_Result.md` |
| ResearchY-D_032 (Pairing-Requirement Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_032_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_032_Result.md` |
| ResearchY-D_033 (Singlet-Prohibition Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_033_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_033_Result.md` |
| ResearchY-D_034 (Reciprocity Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_034_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_034_Result.md` |
| ResearchY-D_035 (Multiplet-Requirement Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_035_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_035_Result.md` |
| ResearchY-D_036 (Complex-State-Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_036_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_036_Result.md` |
| ResearchY-D_037 (Reciprocity-Observability Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_037_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_037_Result.md` |
| ResearchY-D_038 (State-Identity Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_038_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_038_Result.md` |
| ResearchY-D_039 (State-Identity-Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_039_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_039_Result.md` |
| ResearchY-D_040 (Boundary Reclassification Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_040_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 7/7 PASSED — `Results/Y_D_040_Result.md` |
| ResearchY-D_041 (Time-Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_041_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_041_Result.md` |
| ResearchY-D_042 (Fundamental-Ratio Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_042_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_042_Result.md` |
| ResearchY-D_043 (Dual-Anchor-Necessity Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_043_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_043_Result.md` |
| ResearchY-D_044 (Anchor-Origin Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_044_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_044_Result.md` |
| ResearchY-D_045 (Cosmological-Anchor Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_045_Tests.cs` | ACTIVE | 2026-08-29 | ✅ 6/6 PASSED — `Results/Y_D_045_Result.md` |
| ResearchY-D_046 (ResearchY-Predictions Audit) | `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_046_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_D_046_Result.md` |
| ResearchY-R_001 (V2.1 Boundary Program Closure Audit) | `AT.Tests/ResearchY/R_BoundaryProgram/Y_R_001_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 4/4 PASSED — `Results/Y_R_001_Result.md` |
| ResearchY-M_001 (Measurement Origin Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_001_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_M_001_Result.md` |
| ResearchY-M_002 (Measurement Disturbance Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_002_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_M_002_Result.md` |
| ResearchY-M_003 (Measurement Feedback Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_003_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 5/5 PASSED — `Results/Y_M_003_Result.md` |
| ResearchY-M_004 (Measurement Information Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_004_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_M_004_Result.md` |
| ResearchY-M_005 (Information Conservation Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_005_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_M_005_Result.md` |
| ResearchY-M_006 (Observer Role Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_006_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_M_006_Result.md` |
| ResearchY-M_007 (Measurement-Program Synthesis) | `AT.Tests/ResearchY/M_Measurement/Y_M_007_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_M_007_Result.md` |
| ResearchY-M_008 (Measurement Prediction Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_008_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_M_008_Result.md` |
| ResearchY-M_009 (Measurement Prediction Discriminator Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_009_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_M_009_Result.md` |
| ResearchY-M_010 (Discrete Phase Lattice Audit) | `AT.Tests/ResearchY/M_Measurement/Y_M_010_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_M_010_Result.md` |
| ResearchY-NP_003 (Manipulation Lever Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_003_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_003_Result.md` |
| ResearchY-NP_004 (Phase Coupling Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_004_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_004_Result.md` |
| ResearchY-NP_005 (Missing Synchronization Mechanism Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_005_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_005_Result.md` |
| ResearchY-NP_006 (Phase-Locking Origin Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_006_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_006_Result.md` |
| ResearchY-NP_007 (Coupling Field Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_007_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_007_Result.md` |
| ResearchY-NP_008 (Interference Extremum Principle Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_008_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_008_Result.md` |
| ResearchY-NP_009 (Variational Actualization Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_009_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_009_Result.md` |
| ResearchY-NP_010 (Second Network Layer Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_010_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_010_Result.md` |
| ResearchY-NP_011 (Hidden Coupling Field Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_011_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_011_Result.md` |
| ResearchY-NP_012 (Unique Prediction Search) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_012_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_012_Result.md` |
| ResearchY-NP_013 (Unique Spectral Prediction Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_013_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_013_Result.md` |
| ResearchY-NP_014 (Necessity of Synchronization Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_014_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_014_Result.md` |
| ResearchY-NP_015 (O(2) Doublet Prediction Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_015_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 5/5 PASSED — `Results/Y_NP_015_Result.md` |
| ResearchY-NP_016 (Mirror-Pair Observation Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_016_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_016_Result.md` |
| ResearchY-NP_017 (Natural D96 Signature Search) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_017_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_NP_017_Result.md` |
| ResearchY-NP_018 (Distinguishability Observable Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_018_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_018_Result.md` |
| ResearchY-NP_019 (Information Cosmology Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_019_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 5/5 PASSED — `Results/Y_NP_019_Result.md` |
| ResearchY-NP_020 (Black Hole Information Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_020_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_NP_020_Result.md` |
| ResearchY-NP_021 (Information Horizon Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_021_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 5/5 PASSED — `Results/Y_NP_021_Result.md` |
| ResearchY-QG_001 (Information–Geometry Bridge Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_001_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_QG_001_Result.md` |
| ResearchY-QG_002 (Distinguishability → Geometry Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_002_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_QG_002_Result.md` |
| ResearchY-QG_003 (Information Reconstruction Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_003_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_QG_003_Result.md` |
| ResearchY-QG_004 (ρ Nature Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_004_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_QG_004_Result.md` |
| ResearchY-QG_005 (Count-to-Geometry Origin Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_005_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_QG_005_Result.md` |
| ResearchY-QG_006 (Count Conservation Origin Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_006_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_QG_006_Result.md` |
| ResearchY-QG_007 (Count Conservation Necessity Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_007_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 5/5 PASSED — `Results/Y_QG_007_Result.md` |
| ResearchY-QG_008 (Finite Distinguishability Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_008_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_QG_008_Result.md` |
| ResearchY-QG_009 (Infinite State Space Consistency Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_009_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 7/7 PASSED — `Results/Y_QG_009_Result.md` |
| ResearchY-QG_010 (Observable Finiteness Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_010_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 5/5 PASSED — `Results/Y_QG_010_Result.md` |
| ResearchY-QG_011 (Finite Event Principle Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_011_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 6/6 PASSED — `Results/Y_QG_011_Result.md` |
| ResearchY-QG_012 (Distinguishability Cosmology Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_012_Tests.cs` | ACTIVE | 2026-08-31 | ✅ 6/6 PASSED — `Results/Y_QG_012_Result.md` |
| ResearchY-QG_013 (Three-Family Origin Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_013_Tests.cs` | ACTIVE | 2026-08-31 | ✅ 6/6 PASSED — `Results/Y_QG_013_Result.md` |
| ResearchY-QG_014 (Cosmological Selection Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_014_Tests.cs` | ACTIVE | 2026-08-31 | ✅ 5/5 PASSED — `Results/Y_QG_014_Result.md` |
| ResearchY-QG_015 (Observable World Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_015_Tests.cs` | ACTIVE | 2026-08-31 | ✅ 6/6 PASSED — `Results/Y_QG_015_Result.md` |
| ResearchY-QG_016 (Tick Discreteness Origin Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_016_Tests.cs` | ACTIVE | 2026-08-31 | ✅ 6/6 PASSED — `Results/Y_QG_016_Result.md` |
| ResearchY-QG_017 (Distinguishability Cosmology Extension Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_017_Tests.cs` | ACTIVE | 2026-08-31 | ✅ 6/6 PASSED — `Results/Y_QG_017_Result.md` |
| ResearchY-QG_018 (Information-Cosmology Closure Audit) | `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_018_Tests.cs` | ACTIVE | 2026-09-01 | ✅ 6/6 PASSED — `Results/Y_QG_018_Result.md` |
| ResearchY-NP_023 (O(2) Mirror Search) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_023_Tests.cs` | ACTIVE | 2026-09-01 | ✅ 6/6 PASSED — `Results/Y_NP_023_Result.md` |
| ResearchY-NP_024 (O(2) Mirror-Pair Physical Prediction Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_024_Tests.cs` | ACTIVE | 2026-09-01 | ✅ 7/7 PASSED — `Results/Y_NP_024_Result.md` |
| ResearchY-NP_025 (K=6 Uniqueness Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_025_Tests.cs` | ACTIVE | 2026-09-01 | ✅ 6/6 PASSED — `Results/Y_NP_025_Result.md` |
| ResearchY-NP_026 (Protected Block Universality Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_026_Tests.cs` | ACTIVE | 2026-09-01 | ✅ 8/8 PASSED — `Results/Y_NP_026_Result.md` |
| ResearchY-NP_027 (Planck Spectrum Emergence Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_027_Tests.cs` | ACTIVE | 2026-09-01 | ✅ 8/8 PASSED — `Results/Y_NP_027_Result.md` |
| ResearchY-NP_028 (Blackbody Reconstruction Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_028_Tests.cs` | ACTIVE | 2026-09-02 | ✅ 8/8 PASSED — `Results/Y_NP_028_Result.md` |
| ResearchY-NP_029 (ħ Necessity Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_029_Tests.cs` | ACTIVE | 2026-09-02 | ✅ 8/8 PASSED — `Results/Y_NP_029_Result.md` |
| ResearchY-NP_030 (Temperature Origin Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_030_Tests.cs` | ACTIVE | 2026-09-02 | ✅ 8/8 PASSED — `Results/Y_NP_030_Result.md` |
| ResearchY-NP_031 (Structure vs Thermodynamics Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_031_Tests.cs` | ACTIVE | 2026-09-02 | ✅ 8/8 PASSED — `Results/Y_NP_031_Result.md` |
| ResearchY-NP_032 (Thermal-N Search Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_032_Tests.cs` | ACTIVE | 2026-09-02 | ✅ 9/9 PASSED — `Results/Y_NP_032_Result.md` |
| ResearchY-NP_033 (D96 Ensemble Audit) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_033_Tests.cs` | ACTIVE | 2026-09-02 | ✅ 9/9 PASSED — `Results/Y_NP_033_Result.md` |
| ResearchY-NP_022 (Unique Physics Prediction Search) | `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_022_Tests.cs` | ACTIVE | 2026-08-31 | ✅ 7/7 PASSED — `Results/Y_NP_022_Result.md` |
| ResearchY-S_001 (Post-V2.1 Theory Architecture Synthesis) | `AT.Tests/ResearchY/S_Synthesis/Y_S_001_Tests.cs` | ACTIVE | 2026-08-30 | ✅ 4/4 PASSED — `Results/Y_S_001_Result.md` |

## Result Summaries

Per-investigation result summaries are stored under `Results/` and mirrored into the
investigation's "Result summary" section.
