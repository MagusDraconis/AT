# Audit Index

The catalogue of the audit programme: every ResearchY investigation with its identifier, title, verdict
(as the audit itself states it where it states one), research doc and xUnit suite. Generated from the
repository on 2026-10-11; the per-audit narrative lives in `Docs/Archive/NewChat_Part2_Gravity.md` …
`NewChat_Part6_RepoChangelog.md`, the current state in `Docs/NewChat_Start.md`, and the open problems in
`Docs/NewChat_OpenProblems.md`.

**Identifier rule** (from the ResearchY index): every investigation receives a permanent identifier on
creation; numbers within a group are never reused. Groups are topics, not chronological order.

**The Verdict column** is extracted mechanically — from the audit's own `**Verdict:**` / `**Status:**` header where
it states one, otherwise from the verdict the project-memory entry records. **67 of 401** audits state a single
verdict token; a blank cell means the audit states its result in prose rather than as one word, not that it has no
result.

## Group summary

| Group | Topic | Audits | Suites |
|---|---|---|---|
| A | Wave Foundations | 5 | 5 |
| B | Circular Geometry | 3 | 3 |
| C | Source Geometry | 2 | 2 |
| D | Resonance Structure | 58 | 58 |
| E | Electromagnetism | 16 | 16 |
| G | Gravity Source | 82 | 82 |
| M | Measurement | 16 | 16 |
| NP | New Physics | 172 | 167 |
| QG | Geometry Bridge | 18 | 18 |
| QM | Many-Body Correspondence | 11 | 11 |
| R | Boundary Program | 2 | 1 |
| S | Synthesis | 1 | 1 |
| T | Spectral Blueprint | 15 | 15 |

## ResearchY audits (401 investigations, 395 suites)

| Id | Title | Verdict | Research doc | Test suite |
|---|---|---|---|---|
| A_001 | ResearchY-A_001 — Wave Origin Audit |  | [ResearchY-A_001.md](Docs/ResearchY/A_WaveFoundations/ResearchY-A_001.md) | [Y_A_001_Tests.cs](AT.Tests/ResearchY/A_WaveFoundations/Y_A_001_Tests.cs) |
| A_002 | ResearchY-A_002 — Difference Disturbance Audit |  | [ResearchY-A_002.md](Docs/ResearchY/A_WaveFoundations/ResearchY-A_002.md) | [Y_A_002_Tests.cs](AT.Tests/ResearchY/A_WaveFoundations/Y_A_002_Tests.cs) |
| A_003 | ResearchY-A_003 — Actualization Propagation Audit (rev. 2) |  | [ResearchY-A_003.md](Docs/ResearchY/A_WaveFoundations/ResearchY-A_003.md) | [Y_A_003_Tests.cs](AT.Tests/ResearchY/A_WaveFoundations/Y_A_003_Tests.cs) |
| A_004 | ResearchY-A_004 — Propagation Falsification Audit |  | [ResearchY-A_004.md](Docs/ResearchY/A_WaveFoundations/ResearchY-A_004.md) | [Y_A_004_Tests.cs](AT.Tests/ResearchY/A_WaveFoundations/Y_A_004_Tests.cs) |
| A_005 | ResearchY-A_005 — Spectral Projection Origin |  | [ResearchY-A_005.md](Docs/ResearchY/A_WaveFoundations/ResearchY-A_005.md) | [Y_A_005_Tests.cs](AT.Tests/ResearchY/A_WaveFoundations/Y_A_005_Tests.cs) |
| B_001 | ResearchY-B_001 — Circular Closure Audit |  | [ResearchY-B_001.md](Docs/ResearchY/B_CircularGeometry/ResearchY-B_001.md) | [Y_B_001_Tests.cs](AT.Tests/ResearchY/B_CircularGeometry/Y_B_001_Tests.cs) |
| B_002 | ResearchY-B_002 — Origin of π Value Audit |  | [ResearchY-B_002.md](Docs/ResearchY/B_CircularGeometry/ResearchY-B_002.md) | [Y_B_002_Tests.cs](AT.Tests/ResearchY/B_CircularGeometry/Y_B_002_Tests.cs) |
| B_003 | ResearchY-B_003 — Origin of 2π Audit |  | [ResearchY-B_003.md](Docs/ResearchY/B_CircularGeometry/ResearchY-B_003.md) | [Y_B_003_Tests.cs](AT.Tests/ResearchY/B_CircularGeometry/Y_B_003_Tests.cs) |
| C_001 | ResearchY-C_001 — Center Audit |  | [ResearchY-C_001.md](Docs/ResearchY/C_SourceGeometry/ResearchY-C_001.md) | [Y_C_001_Tests.cs](AT.Tests/ResearchY/C_SourceGeometry/Y_C_001_Tests.cs) |
| C_002 | ResearchY-C_002 — Radial Propagation Audit |  | [ResearchY-C_002.md](Docs/ResearchY/C_SourceGeometry/ResearchY-C_002.md) | [Y_C_002_Tests.cs](AT.Tests/ResearchY/C_SourceGeometry/Y_C_002_Tests.cs) |
| D_001 | ResearchY-D_001 — Standing Wave Audit |  | [ResearchY-D_001.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_001.md) | [Y_D_001_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_001_Tests.cs) |
| D_002 | ResearchY-D_002 — Standing Wave Model |  | [ResearchY-D_002.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_002.md) | [Y_D_002_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_002_Tests.cs) |
| D_003 | ResearchY-D_003 — Resonance Observables Audit |  | [ResearchY-D_003.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_003.md) | [Y_D_003_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_003_Tests.cs) |
| D_004 | ResearchY-D_004 — Sector Mapping Origin Audit |  | [ResearchY-D_004.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_004.md) | [Y_D_004_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_004_Tests.cs) |
| D_005 | ResearchY-D_005 — Moment Ordering Audit |  | [ResearchY-D_005.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_005.md) | [Y_D_005_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_005_Tests.cs) |
| D_006 | ResearchY-D_006 — Assignment Constraints Audit |  | [ResearchY-D_006.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_006.md) | [Y_D_006_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_006_Tests.cs) |
| D_007 | ResearchY-D_007 — Planck Scale Audit |  | [ResearchY-D_007.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_007.md) | [Y_D_007_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_007_Tests.cs) |
| D_008 | ResearchY-D_008 — Reference Unit Audit |  | [ResearchY-D_008.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_008.md) | [Y_D_008_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_008_Tests.cs) |
| D_009 | ResearchY-D_009 — Minimum Excitation Audit |  | [ResearchY-D_009.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_009.md) | [Y_D_009_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_009_Tests.cs) |
| D_010 | ResearchY-D_010 — Unit Anchoring Audit |  | [ResearchY-D_010.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_010.md) | [Y_D_010_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_010_Tests.cs) |
| D_011 | ResearchY-D_011 — Universal Reference Audit |  | [ResearchY-D_011.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_011.md) | [Y_D_011_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_011_Tests.cs) |
| D_012 | ResearchY-D_012 — Minimal Anchor Audit |  | [ResearchY-D_012.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_012.md) | [Y_D_012_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_012_Tests.cs) |
| D_013 | ResearchY-D_013 — Anchor Reduction Audit |  | [ResearchY-D_013.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_013.md) | [Y_D_013_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_013_Tests.cs) |
| D_014 | ResearchY-D_014 — Two-Anchor Structure Audit |  | [ResearchY-D_014.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_014.md) | [Y_D_014_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_014_Tests.cs) |
| D_015 | ResearchY-D_015 — N=96 Uniqueness Audit |  | [ResearchY-D_015.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_015.md) | [Y_D_015_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_015_Tests.cs) |
| D_016 | ResearchY-D_016 — Family-Count Origin Audit |  | [ResearchY-D_016.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_016.md) | [Y_D_016_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_016_Tests.cs) |
| D_017 | ResearchY-D_017 — Scale Stability Audit |  | [ResearchY-D_017.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_017.md) | [Y_D_017_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_017_Tests.cs) |
| D_018 | ResearchY-D_018 — Occupancy Selection Audit |  | [ResearchY-D_018.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_018.md) | [Y_D_018_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_018_Tests.cs) |
| D_019 | ResearchY-D_019 — Closure-Only Audit |  | [ResearchY-D_019.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_019.md) | [Y_D_019_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_019_Tests.cs) |
| D_020 | ResearchY-D_020 — Selection Precondition Audit |  | [ResearchY-D_020.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_020.md) | [Y_D_020_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_020_Tests.cs) |
| D_021 | ResearchY-D_021 — Oscillation Symmetry Audit |  | [ResearchY-D_021.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_021.md) | [Y_D_021_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_021_Tests.cs) |
| D_022 | ResearchY-D_022 — Weak-Isospin Entry Audit |  | [ResearchY-D_022.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_022.md) | [Y_D_022_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_022_Tests.cs) |
| D_023 | ResearchY-D_023 — SU(2) Entry Audit |  | [ResearchY-D_023.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_023.md) | [Y_D_023_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_023_Tests.cs) |
| D_024 | ResearchY-D_024 — Doublet Compatibility Audit |  | [ResearchY-D_024.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_024.md) | [Y_D_024_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_024_Tests.cs) |
| D_025 | ResearchY-D_025 — Three-Generator Audit |  | [ResearchY-D_025.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_025.md) | [Y_D_025_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_025_Tests.cs) |
| D_026 | ResearchY-D_026 — Compact-Form Audit |  | [ResearchY-D_026.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_026.md) | [Y_D_026_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_026_Tests.cs) |
| D_027 | ResearchY-D_027 — Selector-Origin Audit |  | [ResearchY-D_027.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_027.md) | [Y_D_027_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_027_Tests.cs) |
| D_028 | ResearchY-D_028 — Span-Origin Audit |  | [ResearchY-D_028.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_028.md) | [Y_D_028_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_028_Tests.cs) |
| D_029 | ResearchY-D_029 — Closure-Defect Audit |  | [ResearchY-D_029.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_029.md) | [Y_D_029_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_029_Tests.cs) |
| D_030 | ResearchY-D_030 — Octave-Rung Audit |  | [ResearchY-D_030.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_030.md) | [Y_D_030_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_030_Tests.cs) |
| D_031 | ResearchY-D_031 — Seed-Origin Audit |  | [ResearchY-D_031.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_031.md) | [Y_D_031_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_031_Tests.cs) |
| D_032 | ResearchY-D_032 — Pairing-Requirement Audit |  | [ResearchY-D_032.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_032.md) | [Y_D_032_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_032_Tests.cs) |
| D_033 | ResearchY-D_033 — Singlet-Prohibition Audit |  | [ResearchY-D_033.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_033.md) | [Y_D_033_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_033_Tests.cs) |
| D_034 | ResearchY-D_034 — Reciprocity Audit |  | [ResearchY-D_034.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_034.md) | [Y_D_034_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_034_Tests.cs) |
| D_035 | ResearchY-D_035 — Multiplet-Requirement Audit |  | [ResearchY-D_035.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_035.md) | [Y_D_035_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_035_Tests.cs) |
| D_036 | ResearchY-D_036 — Complex-State-Origin Audit |  | [ResearchY-D_036.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_036.md) | [Y_D_036_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_036_Tests.cs) |
| D_037 | ResearchY-D_037 — Reciprocity-Observability Audit |  | [ResearchY-D_037.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_037.md) | [Y_D_037_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_037_Tests.cs) |
| D_038 | ResearchY-D_038 — State-Identity Audit |  | [ResearchY-D_038.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_038.md) | [Y_D_038_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_038_Tests.cs) |
| D_039 | ResearchY-D_039 — State-Identity-Origin Audit |  | [ResearchY-D_039.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_039.md) | [Y_D_039_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_039_Tests.cs) |
| D_040 | ResearchY-D_040 — Boundary Reclassification Audit |  | [ResearchY-D_040.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_040.md) | [Y_D_040_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_040_Tests.cs) |
| D_041 | ResearchY-D_041 — Time-Origin Audit |  | [ResearchY-D_041.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_041.md) | [Y_D_041_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_041_Tests.cs) |
| D_042 | ResearchY-D_042 — Fundamental-Ratio Audit |  | [ResearchY-D_042.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_042.md) | [Y_D_042_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_042_Tests.cs) |
| D_043 | ResearchY-D_043 — Dual-Anchor-Necessity Audit |  | [ResearchY-D_043.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_043.md) | [Y_D_043_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_043_Tests.cs) |
| D_044 | ResearchY-D_044 — Anchor-Origin Audit |  | [ResearchY-D_044.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_044.md) | [Y_D_044_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_044_Tests.cs) |
| D_045 | ResearchY-D_045 — Cosmological-Anchor Audit |  | [ResearchY-D_045.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_045.md) | [Y_D_045_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_045_Tests.cs) |
| D_046 | ResearchY-D_046 — ResearchY-Predictions Audit |  | [ResearchY-D_046.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_046.md) | [Y_D_046_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_046_Tests.cs) |
| D_047 | ResearchY-D_047 — Degeneracy-Splitting Amplification Audit |  | [ResearchY-D_047.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_047.md) | [Y_D_047_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_047_Tests.cs) |
| D_048 | ResearchY-D_048 — Latent-Degeneracy Adaptability Audit |  | [ResearchY-D_048.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_048.md) | [Y_D_048_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_048_Tests.cs) |
| D_049 | ResearchY-D_049 — Adaptability–Robustness Frontier Audit |  | [ResearchY-D_049.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_049.md) | [Y_D_049_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_049_Tests.cs) |
| D_050 | ResearchY-D_050 — Spectral Predictability Audit |  | [ResearchY-D_050.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_050.md) | [Y_D_050_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_050_Tests.cs) |
| D_051 | ResearchY-D_051 — Blind Prediction Audit |  | [ResearchY-D_051.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_051.md) | [Y_D_051_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_051_Tests.cs) |
| D_052 | ResearchY-D_052 — Degeneracy Axis Audit |  | [ResearchY-D_052.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_052.md) | [Y_D_052_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_052_Tests.cs) |
| D_053 | ResearchY-D_053 — Perturbation-Family Dominance Audit |  | [ResearchY-D_053.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_053.md) | [Y_D_053_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_053_Tests.cs) |
| D_054 | ResearchY-D_054 — Low-Energy Spectral Edge Audit |  | [ResearchY-D_054.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_054.md) | [Y_D_054_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_054_Tests.cs) |
| D_055 | ResearchY-D_055 — Pair1-47 Anomaly Audit |  | [ResearchY-D_055.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_055.md) | [Y_D_055_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_055_Tests.cs) |
| D_056 | ResearchY-D_056 — Multiplicity Distribution Audit |  | [ResearchY-D_056.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_056.md) | [Y_D_056_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_056_Tests.cs) |
| D_057 | ResearchY-D_057 — Rank-Budget Law Audit |  | [ResearchY-D_057.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_057.md) | [Y_D_057_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_057_Tests.cs) |
| D_058 | ResearchY-D_058 — Recovery Mechanism Audit |  | [ResearchY-D_058.md](Docs/ResearchY/D_ResonanceStructure/ResearchY-D_058.md) | [Y_D_058_Tests.cs](AT.Tests/ResearchY/D_ResonanceStructure/Y_D_058_Tests.cs) |
| E_001 | ResearchY-E_001 — Electromagnetism Inventory Audit | BOUNDARY | [ResearchY-E_001.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_001.md) | [Y_E_001_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_001_Tests.cs) |
| E_002 | ResearchY-E_002 — Field Equation Derivation Audit | BOUNDARY | [ResearchY-E_002.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_002.md) | [Y_E_002_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_002_Tests.cs) |
| E_003 | ResearchY-E_003 — Photon Ontology Audit | BOUNDARY | [ResearchY-E_003.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_003.md) | [Y_E_003_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_003_Tests.cs) |
| E_004 | ResearchY-E_004 — Vector Sector Audit | BOUNDARY | [ResearchY-E_004.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_004.md) | [Y_E_004_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_004_Tests.cs) |
| E_005 | ResearchY-E_005 - Propagation Origin Audit | BOUNDARY | [ResearchY-E_005.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_005.md) | [Y_E_005_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_005_Tests.cs) |
| E_006 | ResearchY-E_006 - Connection Origin Audit | DERIVED | [ResearchY-E_006.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_006.md) | [Y_E_006_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_006_Tests.cs) |
| E_007 | ResearchY-E_007 - Field Strength Origin Audit | BOUNDARY | [ResearchY-E_007.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_007.md) | [Y_E_007_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_007_Tests.cs) |
| E_008 | ResearchY-E_008 - Field Excitation Audit | DERIVED | [ResearchY-E_008.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_008.md) | [Y_E_008_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_008_Tests.cs) |
| E_009 | ResearchY-E_009 - Coupling Function Audit | DERIVED | [ResearchY-E_009.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_009.md) | [Y_E_009_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_009_Tests.cs) |
| E_010 | ResearchY-E_010 - Magnetic Sector Audit | REFUTED | [ResearchY-E_010.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_010.md) | [Y_E_010_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_010_Tests.cs) |
| E_011 | ResearchY-E_011 - Flux Origin Audit | DERIVED | [ResearchY-E_011.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_011.md) | [Y_E_011_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_011_Tests.cs) |
| E_012 | ResearchY-E_012 - Flux Excitation Audit | BOUNDARY | [ResearchY-E_012.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_012.md) | [Y_E_012_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_012_Tests.cs) |
| E_013 | ResearchY-E_013 - Flux Population Audit | BOUNDARY | [ResearchY-E_013.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_013.md) | [Y_E_013_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_013_Tests.cs) |
| E_014 | ResearchY-E_014 - Sector Selection Audit | BOUNDARY | [ResearchY-E_014.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_014.md) | [Y_E_014_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_014_Tests.cs) |
| E_015 | ResearchY-E_015 - Sector Weight Audit | BOUNDARY | [ResearchY-E_015.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_015.md) | [Y_E_015_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_015_Tests.cs) |
| E_016 | ResearchY-E_016 - Sector Population Principle Audit | REFUTED | [ResearchY-E_016.md](Docs/ResearchY/E_Electromagnetism/ResearchY-E_016.md) | [Y_E_016_Tests.cs](AT.Tests/ResearchY/E_Electromagnetism/Y_E_016_Tests.cs) |
| G_001 | ResearchY-G_001 — Gravity Source Audit |  | [ResearchY-G_001.md](Docs/ResearchY/G_GravitySource/ResearchY-G_001.md) | [Y_G_001_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_001_Tests.cs) |
| G_002 | ResearchY-G_002 — Density Control Audit |  | [ResearchY-G_002.md](Docs/ResearchY/G_GravitySource/ResearchY-G_002.md) | [Y_G_002_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_002_Tests.cs) |
| G_003 | ResearchY-G_003 — Gravity Magnitude Audit |  | [ResearchY-G_003.md](Docs/ResearchY/G_GravitySource/ResearchY-G_003.md) | [Y_G_003_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_003_Tests.cs) |
| G_004 | ResearchY-G_004 — Gravity Calibration Audit |  | [ResearchY-G_004.md](Docs/ResearchY/G_GravitySource/ResearchY-G_004.md) | [Y_G_004_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_004_Tests.cs) |
| G_005 | ResearchY-G_005 — Control Realizability Audit |  | [ResearchY-G_005.md](Docs/ResearchY/G_GravitySource/ResearchY-G_005.md) | [Y_G_005_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_005_Tests.cs) |
| G_006 | ResearchY-G_006 — Suppression Mechanism Audit |  | [ResearchY-G_006.md](Docs/ResearchY/G_GravitySource/ResearchY-G_006.md) | [Y_G_006_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_006_Tests.cs) |
| G_007 | ResearchY-G_007 — Suppression Origin Audit |  | [ResearchY-G_007.md](Docs/ResearchY/G_GravitySource/ResearchY-G_007.md) | [Y_G_007_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_007_Tests.cs) |
| G_008 | ResearchY-G_008 — Controlled Suppression Audit |  | [ResearchY-G_008.md](Docs/ResearchY/G_GravitySource/ResearchY-G_008.md) | [Y_G_008_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_008_Tests.cs) |
| G_009 | ResearchY-G_009 — Clock Rate Audit |  | [ResearchY-G_009.md](Docs/ResearchY/G_GravitySource/ResearchY-G_009.md) | [Y_G_009_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_009_Tests.cs) |
| G_010 | ResearchY-G_010 — Time Control Feasibility Audit |  | [ResearchY-G_010.md](Docs/ResearchY/G_GravitySource/ResearchY-G_010.md) | [Y_G_010_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_010_Tests.cs) |
| G_011 | ResearchY-G_011 — Rho Actuator Audit |  | [ResearchY-G_011.md](Docs/ResearchY/G_GravitySource/ResearchY-G_011.md) | [Y_G_011_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_011_Tests.cs) |
| G_011 | ResearchY-G_011b — Labor Rho Audit |  | [ResearchY-G_011b.md](Docs/ResearchY/G_GravitySource/ResearchY-G_011b.md) | [Y_G_011_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_011_Tests.cs) |
| G_012 | ResearchY-G_012 — Local Rho Actuator Audit |  | [ResearchY-G_012.md](Docs/ResearchY/G_GravitySource/ResearchY-G_012.md) | [Y_G_012_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_012_Tests.cs) |
| G_013 | ResearchY-G_013 — Physical Actuator Audit |  | [ResearchY-G_013.md](Docs/ResearchY/G_GravitySource/ResearchY-G_013.md) | [Y_G_013_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_013_Tests.cs) |
| G_014 | ResearchY-G_014 — Physical Rho Mapping Audit |  | [ResearchY-G_014.md](Docs/ResearchY/G_GravitySource/ResearchY-G_014.md) | [Y_G_014_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_014_Tests.cs) |
| G_015 | ResearchY-G_015 — Rho To Metric Audit |  | [ResearchY-G_015.md](Docs/ResearchY/G_GravitySource/ResearchY-G_015.md) | [Y_G_015_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_015_Tests.cs) |
| G_016 | ResearchY-G_016 — Watch Ontology Audit |  | [ResearchY-G_016.md](Docs/ResearchY/G_GravitySource/ResearchY-G_016.md) | [Y_G_016_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_016_Tests.cs) |
| G_016 | ResearchY-G_016b — Mass Independence Audit |  | [ResearchY-G_016b.md](Docs/ResearchY/G_GravitySource/ResearchY-G_016b.md) | [Y_G_016_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_016_Tests.cs) |
| G_017 | ResearchY-G_017 — Metric Coupling Audit |  | [ResearchY-G_017.md](Docs/ResearchY/G_GravitySource/ResearchY-G_017.md) | [Y_G_017_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_017_Tests.cs) |
| G_018 | ResearchY-G_018 — Rho Identity Audit |  | [ResearchY-G_018.md](Docs/ResearchY/G_GravitySource/ResearchY-G_018.md) | [Y_G_018_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_018_Tests.cs) |
| G_019 | ResearchY-G_019 — Second-Order Signature Audit |  | [ResearchY-G_019.md](Docs/ResearchY/G_GravitySource/ResearchY-G_019.md) | [Y_G_019_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_019_Tests.cs) |
| G_020 | ResearchY-G_020 — Neutron-Star Redshift Audit |  | [ResearchY-G_020.md](Docs/ResearchY/G_GravitySource/ResearchY-G_020.md) | [Y_G_020_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_020_Tests.cs) |
| G_021 | ResearchY-G_021 — Light-Propagation Audit |  | [ResearchY-G_021.md](Docs/ResearchY/G_GravitySource/ResearchY-G_021.md) | [Y_G_021_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_021_Tests.cs) |
| G_022 | ResearchY-G_022 — Spatial Metric Audit |  | [ResearchY-G_022.md](Docs/ResearchY/G_GravitySource/ResearchY-G_022.md) | [Y_G_022_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_022_Tests.cs) |
| G_023 | ResearchY-G_023 — Spatial Sector Closure Audit |  | [ResearchY-G_023.md](Docs/ResearchY/G_GravitySource/ResearchY-G_023.md) | [Y_G_023_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_023_Tests.cs) |
| G_024 | ResearchY-G_024 — Optics Reconciliation Audit |  | [ResearchY-G_024.md](Docs/ResearchY/G_GravitySource/ResearchY-G_024.md) | [Y_G_024_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_024_Tests.cs) |
| G_025 | ResearchY-G_025 — Optics Determinant Correction Audit |  | [ResearchY-G_025.md](Docs/ResearchY/G_GravitySource/ResearchY-G_025.md) | [Y_G_025_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_025_Tests.cs) |
| G_026 | ResearchY-G_026 — Authored-Verdict Audit |  | [ResearchY-G_026.md](Docs/ResearchY/G_GravitySource/ResearchY-G_026.md) | [Y_G_026_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_026_Tests.cs) |
| G_027 | ResearchY-G_027 — Literal Verdict Audit |  | [ResearchY-G_027.md](Docs/ResearchY/G_GravitySource/ResearchY-G_027.md) | [Y_G_027_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_027_Tests.cs) |
| G_028 | ResearchY-G_028 — Clock Sector Closure Audit |  | [ResearchY-G_028.md](Docs/ResearchY/G_GravitySource/ResearchY-G_028.md) | [Y_G_028_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_028_Tests.cs) |
| G_029 | ResearchY-G_029 — Spatial Sector Closure Audit |  | [ResearchY-G_029.md](Docs/ResearchY/G_GravitySource/ResearchY-G_029.md) | [Y_G_029_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_029_Tests.cs) |
| G_030 | ResearchY-G_030 — No-Go Audit |  | [ResearchY-G_030.md](Docs/ResearchY/G_GravitySource/ResearchY-G_030.md) | [Y_G_030_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_030_Tests.cs) |
| G_031 | ResearchY-G_031 — Spatial-Origin Audit |  | [ResearchY-G_031.md](Docs/ResearchY/G_GravitySource/ResearchY-G_031.md) | [Y_G_031_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_031_Tests.cs) |
| G_032 | ResearchY-G_032 — Conformal Assumption Audit |  | [ResearchY-G_032.md](Docs/ResearchY/G_GravitySource/ResearchY-G_032.md) | [Y_G_032_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_032_Tests.cs) |
| G_033 | ResearchY-G_033 — Cubic Substrate Audit |  | [ResearchY-G_033.md](Docs/ResearchY/G_GravitySource/ResearchY-G_033.md) | [Y_G_033_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_033_Tests.cs) |
| G_034 | ResearchY-G_034 — A₀ Robustness Audit |  | [ResearchY-G_034.md](Docs/ResearchY/G_GravitySource/ResearchY-G_034.md) | [Y_G_034_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_034_Tests.cs) |
| G_035 | ResearchY-G_035 — Temporal Independence Audit |  | [ResearchY-G_035.md](Docs/ResearchY/G_GravitySource/ResearchY-G_035.md) | [Y_G_035_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_035_Tests.cs) |
| G_036 | ResearchY-G_036 — Temporal Core Test Audit |  | [ResearchY-G_036.md](Docs/ResearchY/G_GravitySource/ResearchY-G_036.md) | [Y_G_036_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_036_Tests.cs) |
| G_037 | ResearchY-G_037 — Refractive Lens Audit |  | [ResearchY-G_037.md](Docs/ResearchY/G_GravitySource/ResearchY-G_037.md) | [Y_G_037_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_037_Tests.cs) |
| G_038 | ResearchY-G_038 — Measure-Decomposition Audit |  | [ResearchY-G_038.md](Docs/ResearchY/G_GravitySource/ResearchY-G_038.md) | [Y_G_038_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_038_Tests.cs) |
| G_039 | ResearchY-G_039 — Rho Realization Audit | BOUNDARY | [ResearchY-G_039.md](Docs/ResearchY/G_GravitySource/ResearchY-G_039.md) | [Y_G_039_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_039_Tests.cs) |
| G_040 | ResearchY-G_040 — Rho Observable Audit | REFUTED | [ResearchY-G_040.md](Docs/ResearchY/G_GravitySource/ResearchY-G_040.md) | [Y_G_040_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_040_Tests.cs) |
| G_041 | ResearchY-G_041 — Substrate Dimension Audit | BOUNDARY | [ResearchY-G_041.md](Docs/ResearchY/G_GravitySource/ResearchY-G_041.md) | [Y_G_041_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_041_Tests.cs) |
| G_042 | ResearchY-G_042 — Three-Dimensionality Dependency Audit |  | [ResearchY-G_042.md](Docs/ResearchY/G_GravitySource/ResearchY-G_042.md) | [Y_G_042_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_042_Tests.cs) |
| G_043 | ResearchY-G_043 — Minimality Audit | BOUNDARY | [ResearchY-G_043.md](Docs/ResearchY/G_GravitySource/ResearchY-G_043.md) | [Y_G_043_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_043_Tests.cs) |
| G_044 | ResearchY-G_044 — Minimal Working Substrate Audit | EMERGENT | [ResearchY-G_044.md](Docs/ResearchY/G_GravitySource/ResearchY-G_044.md) | [Y_G_044_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_044_Tests.cs) |
| G_045 | ResearchY-G_045 — Observed vs Hidden Dimension Audit | REFUTED | [ResearchY-G_045.md](Docs/ResearchY/G_GravitySource/ResearchY-G_045.md) | [Y_G_045_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_045_Tests.cs) |
| G_046 | ResearchY-G_046 - Rho Accessibility Audit | OBSERVABLE | [ResearchY-G_046.md](Docs/ResearchY/G_GravitySource/ResearchY-G_046.md) | [Y_G_046_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_046_Tests.cs) |
| G_047 | ResearchY-G_047 - Kernel Observable Audit | DERIVED | [ResearchY-G_047.md](Docs/ResearchY/G_GravitySource/ResearchY-G_047.md) | [Y_G_047_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_047_Tests.cs) |
| G_048 | ResearchY-G_048 - Clock Completeness Audit | MAXIMAL | [ResearchY-G_048.md](Docs/ResearchY/G_GravitySource/ResearchY-G_048.md) | [Y_G_048_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_048_Tests.cs) |
| G_049 | ResearchY-G_049 - Clock Primacy Audit | LOSSLESS | [ResearchY-G_049.md](Docs/ResearchY/G_GravitySource/ResearchY-G_049.md) | [Y_G_049_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_049_Tests.cs) |
| G_050 | ResearchY-G_050 - Kernel Structure Audit | DERIVED | [ResearchY-G_050.md](Docs/ResearchY/G_GravitySource/ResearchY-G_050.md) | [Y_G_050_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_050_Tests.cs) |
| G_051 | ResearchY-G_051 - Phase Sector Audit | PHYSICAL | [ResearchY-G_051.md](Docs/ResearchY/G_GravitySource/ResearchY-G_051.md) | [Y_G_051_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_051_Tests.cs) |
| G_052 | ResearchY-G_052 - Amplitude Phase Audit | DERIVED | [ResearchY-G_052.md](Docs/ResearchY/G_GravitySource/ResearchY-G_052.md) | [Y_G_052_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_052_Tests.cs) |
| G_053 | ResearchY-G_053 - Phase Sector Dynamics Audit | DERIVED | [ResearchY-G_053.md](Docs/ResearchY/G_GravitySource/ResearchY-G_053.md) | [Y_G_053_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_053_Tests.cs) |
| G_054 | ResearchY-G_054 - Phase Determination Audit | BOUNDARY | [ResearchY-G_054.md](Docs/ResearchY/G_GravitySource/ResearchY-G_054.md) | [Y_G_054_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_054_Tests.cs) |
| G_055 | ResearchY-G_055 - Phase Selection Principle Audit | BOUNDARY | [ResearchY-G_055.md](Docs/ResearchY/G_GravitySource/ResearchY-G_055.md) | [Y_G_055_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_055_Tests.cs) |
| G_056 | ResearchY-G_056 - Non-Scalar Selection Audit | DERIVED | [ResearchY-G_056.md](Docs/ResearchY/G_GravitySource/ResearchY-G_056.md) | [Y_G_056_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_056_Tests.cs) |
| G_057 | ResearchY-G_057 - Phase Flow Audit | BOUNDARY | [ResearchY-G_057.md](Docs/ResearchY/G_GravitySource/ResearchY-G_057.md) | [Y_G_057_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_057_Tests.cs) |
| G_058 | ResearchY-G_058 - Phase Dynamics Closure Audit | BOUNDARY | [ResearchY-G_058.md](Docs/ResearchY/G_GravitySource/ResearchY-G_058.md) | [Y_G_058_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_058_Tests.cs) |
| G_059 | ResearchY-G_059 - Flow Source Audit | DERIVED | [ResearchY-G_059.md](Docs/ResearchY/G_GravitySource/ResearchY-G_059.md) | [Y_G_059_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_059_Tests.cs) |
| G_060 | ResearchY-G_060 - Phase Evolution Audit | DERIVED | [ResearchY-G_060.md](Docs/ResearchY/G_GravitySource/ResearchY-G_060.md) | [Y_G_060_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_060_Tests.cs) |
| G_061 | ResearchY-G_061 - Residual Phase Audit | DERIVED | [ResearchY-G_061.md](Docs/ResearchY/G_GravitySource/ResearchY-G_061.md) | [Y_G_061_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_061_Tests.cs) |
| G_062 | ResearchY-G_062 - Mode Occupation Audit | DERIVED | [ResearchY-G_062.md](Docs/ResearchY/G_GravitySource/ResearchY-G_062.md) | [Y_G_062_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_062_Tests.cs) |
| G_063 | ResearchY-G_063 - Canonical State Audit | BOUNDARY | [ResearchY-G_063.md](Docs/ResearchY/G_GravitySource/ResearchY-G_063.md) | [Y_G_063_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_063_Tests.cs) |
| G_064 | ResearchY-G_064 - Canonical Recipe Origin Audit | DERIVED | [ResearchY-G_064.md](Docs/ResearchY/G_GravitySource/ResearchY-G_064.md) | [Y_G_064_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_064_Tests.cs) |
| G_065 | ResearchY-G_065 - Phase-Free Principle Audit | BOUNDARY | [ResearchY-G_065.md](Docs/ResearchY/G_GravitySource/ResearchY-G_065.md) | [Y_G_065_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_065_Tests.cs) |
| G_066 | ResearchY-G_066 - Physical Flow Audit | REFUTED | [ResearchY-G_066.md](Docs/ResearchY/G_GravitySource/ResearchY-G_066.md) | [Y_G_066_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_066_Tests.cs) |
| G_067 | ResearchY-G_067 - Flow Selection Audit | REFUTED | [ResearchY-G_067.md](Docs/ResearchY/G_GravitySource/ResearchY-G_067.md) | [Y_G_067_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_067_Tests.cs) |
| G_068 | ResearchY-G_068 - Temporal Prediction Audit | DERIVED | [ResearchY-G_068.md](Docs/ResearchY/G_GravitySource/ResearchY-G_068.md) | [Y_G_068_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_068_Tests.cs) |
| G_069 | ResearchY-G_069 - Neutron Star Decision Audit | REACHABLE | [ResearchY-G_069.md](Docs/ResearchY/G_GravitySource/ResearchY-G_069.md) | [Y_G_069_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_069_Tests.cs) |
| G_070 | ResearchY-G_070 - Observational Decision Audit | DERIVED | [ResearchY-G_070.md](Docs/ResearchY/G_GravitySource/ResearchY-G_070.md) | [Y_G_070_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_070_Tests.cs) |
| G_071 | ResearchY-G_071 - Observational Roadmap Audit | DERIVED | [ResearchY-G_071.md](Docs/ResearchY/G_GravitySource/ResearchY-G_071.md) | [Y_G_071_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_071_Tests.cs) |
| G_072 | ResearchY-G_072 - Observational Program Audit | DERIVED | [ResearchY-G_072.md](Docs/ResearchY/G_GravitySource/ResearchY-G_072.md) | [Y_G_072_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_072_Tests.cs) |
| G_073 | ResearchY-G_073 - Exponential Uniqueness Audit | BOUNDARY | [ResearchY-G_073.md](Docs/ResearchY/G_GravitySource/ResearchY-G_073.md) | [Y_G_073_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_073_Tests.cs) |
| G_074 | ResearchY-G_074 - Clock Law Necessity Audit | BOUNDARY | [ResearchY-G_074.md](Docs/ResearchY/G_GravitySource/ResearchY-G_074.md) | [Y_G_074_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_074_Tests.cs) |
| G_075 | ResearchY-G_075 - Clock-Law Uniqueness Audit | REFUTED | [ResearchY-G_075.md](Docs/ResearchY/G_GravitySource/ResearchY-G_075.md) | [Y_G_075_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_075_Tests.cs) |
| G_076 | ResearchY-G_076 - Clock-Law Selection Audit | REFUTED | [ResearchY-G_076.md](Docs/ResearchY/G_GravitySource/ResearchY-G_076.md) | [Y_G_076_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_076_Tests.cs) |
| G_077 | ResearchY-G_077 - Clock Source Audit | REFUTED | [ResearchY-G_077.md](Docs/ResearchY/G_GravitySource/ResearchY-G_077.md) | [Y_G_077_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_077_Tests.cs) |
| G_078 | ResearchY-G_078 — Source Manipulation Audit | REFUTED | [ResearchY-G_078.md](Docs/ResearchY/G_GravitySource/ResearchY-G_078.md) | [Y_G_078_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_078_Tests.cs) |
| G_079 | ResearchY-G_079 — Continuity Law Audit | BOUNDARY | [ResearchY-G_079.md](Docs/ResearchY/G_GravitySource/ResearchY-G_079.md) | [Y_G_079_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_079_Tests.cs) |
| G_080 | ResearchY-G_080 — Connectivity Origin Audit |  | [ResearchY-G_080.md](Docs/ResearchY/G_GravitySource/ResearchY-G_080.md) | [Y_G_080_Tests.cs](AT.Tests/ResearchY/G_GravitySource/Y_G_080_Tests.cs) |
| M_001 | ResearchY-M_001 — Measurement Origin Audit |  | [ResearchY-M_001.md](Docs/ResearchY/M_Measurement/ResearchY-M_001.md) | [Y_M_001_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_001_Tests.cs) |
| M_002 | ResearchY-M_002 — Measurement Disturbance Audit |  | [ResearchY-M_002.md](Docs/ResearchY/M_Measurement/ResearchY-M_002.md) | [Y_M_002_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_002_Tests.cs) |
| M_003 | ResearchY-M_003 — Measurement Feedback Audit |  | [ResearchY-M_003.md](Docs/ResearchY/M_Measurement/ResearchY-M_003.md) | [Y_M_003_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_003_Tests.cs) |
| M_004 | ResearchY-M_004 — Measurement Information Audit |  | [ResearchY-M_004.md](Docs/ResearchY/M_Measurement/ResearchY-M_004.md) | [Y_M_004_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_004_Tests.cs) |
| M_005 | ResearchY-M_005 — Information Conservation Audit |  | [ResearchY-M_005.md](Docs/ResearchY/M_Measurement/ResearchY-M_005.md) | [Y_M_005_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_005_Tests.cs) |
| M_006 | ResearchY-M_006 — Observer Role Audit |  | [ResearchY-M_006.md](Docs/ResearchY/M_Measurement/ResearchY-M_006.md) | [Y_M_006_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_006_Tests.cs) |
| M_007 | ResearchY-M_007 — Measurement-Program Synthesis |  | [ResearchY-M_007.md](Docs/ResearchY/M_Measurement/ResearchY-M_007.md) | [Y_M_007_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_007_Tests.cs) |
| M_008 | ResearchY-M_008 — Measurement Prediction Audit |  | [ResearchY-M_008.md](Docs/ResearchY/M_Measurement/ResearchY-M_008.md) | [Y_M_008_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_008_Tests.cs) |
| M_009 | ResearchY-M_009 — Measurement Prediction Discriminator Audit |  | [ResearchY-M_009.md](Docs/ResearchY/M_Measurement/ResearchY-M_009.md) | [Y_M_009_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_009_Tests.cs) |
| M_010 | ResearchY-M_010 — Discrete Phase Lattice Audit |  | [ResearchY-M_010.md](Docs/ResearchY/M_Measurement/ResearchY-M_010.md) | [Y_M_010_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_010_Tests.cs) |
| M_011 | ResearchY-M_011 — Effective O(3) Symmetry Audit |  | [ResearchY-M_011.md](Docs/ResearchY/M_Measurement/ResearchY-M_011.md) | [Y_M_011_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_011_Tests.cs) |
| M_012 | ResearchY-M_012 — Network O(3) Symmetry Audit |  | [ResearchY-M_012.md](Docs/ResearchY/M_Measurement/ResearchY-M_012.md) | [Y_M_012_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_012_Tests.cs) |
| M_013 | ResearchY-M_013 — Axis-Count Selection Audit |  | [ResearchY-M_013.md](Docs/ResearchY/M_Measurement/ResearchY-M_013.md) | [Y_M_013_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_013_Tests.cs) |
| M_014 | ResearchY-M_014 — Automatic Ring-Size Rank Scan Audit |  | [ResearchY-M_014.md](Docs/ResearchY/M_Measurement/ResearchY-M_014.md) | [Y_M_014_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_014_Tests.cs) |
| M_015 | ResearchY-M_015 — Score-Function Robustness Audit |  | [ResearchY-M_015.md](Docs/ResearchY/M_Measurement/ResearchY-M_015.md) | [Y_M_015_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_015_Tests.cs) |
| M_016 | ResearchY-M_016 — Criterion-Independence Audit |  | [ResearchY-M_016.md](Docs/ResearchY/M_Measurement/ResearchY-M_016.md) | [Y_M_016_Tests.cs](AT.Tests/ResearchY/M_Measurement/Y_M_016_Tests.cs) |
| NP_001 | ResearchY-NP_001 — V2.2 New Physics Roadmap |  | [ResearchY-NP_001.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_001.md) | — |
| NP_002 | ResearchY-NP_002 — Highest-Value V2.2 Program |  | [ResearchY-NP_002.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_002.md) | — |
| NP_003 | ResearchY-NP_003 — Manipulation Lever Audit |  | [ResearchY-NP_003.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_003.md) | [Y_NP_003_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_003_Tests.cs) |
| NP_004 | ResearchY-NP_004 — Phase Coupling Audit |  | [ResearchY-NP_004.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_004.md) | [Y_NP_004_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_004_Tests.cs) |
| NP_005 | ResearchY-NP_005 — Missing Synchronization Mechanism Audit |  | [ResearchY-NP_005.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_005.md) | [Y_NP_005_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_005_Tests.cs) |
| NP_006 | ResearchY-NP_006 — Phase-Locking Origin Audit |  | [ResearchY-NP_006.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_006.md) | [Y_NP_006_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_006_Tests.cs) |
| NP_007 | ResearchY-NP_007 — Coupling Field Audit |  | [ResearchY-NP_007.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_007.md) | [Y_NP_007_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_007_Tests.cs) |
| NP_008 | ResearchY-NP_008 — Interference Extremum Principle Audit |  | [ResearchY-NP_008.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_008.md) | [Y_NP_008_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_008_Tests.cs) |
| NP_009 | ResearchY-NP_009 — Variational Actualization Audit |  | [ResearchY-NP_009.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_009.md) | [Y_NP_009_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_009_Tests.cs) |
| NP_010 | ResearchY-NP_010 — Second Network Layer Audit |  | [ResearchY-NP_010.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_010.md) | [Y_NP_010_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_010_Tests.cs) |
| NP_011 | ResearchY-NP_011 — Hidden Coupling Field Audit |  | [ResearchY-NP_011.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_011.md) | [Y_NP_011_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_011_Tests.cs) |
| NP_012 | ResearchY-NP_012 — Unique Prediction Search |  | [ResearchY-NP_012.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_012.md) | [Y_NP_012_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_012_Tests.cs) |
| NP_013 | ResearchY-NP_013 — Unique Spectral Prediction Audit |  | [ResearchY-NP_013.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_013.md) | [Y_NP_013_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_013_Tests.cs) |
| NP_014 | ResearchY-NP_014 — Necessity of Synchronization Audit |  | [ResearchY-NP_014.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_014.md) | [Y_NP_014_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_014_Tests.cs) |
| NP_015 | ResearchY-NP_015 — O(2) Doublet Prediction Audit |  | [ResearchY-NP_015.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_015.md) | [Y_NP_015_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_015_Tests.cs) |
| NP_016 | ResearchY-NP_016 — Mirror-Pair Observation Audit |  | [ResearchY-NP_016.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_016.md) | [Y_NP_016_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_016_Tests.cs) |
| NP_017 | ResearchY-NP_017 — Natural D96 Signature Search |  | [ResearchY-NP_017.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_017.md) | [Y_NP_017_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_017_Tests.cs) |
| NP_018 | ResearchY-NP_018 — Distinguishability Observable Audit |  | [ResearchY-NP_018.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_018.md) | [Y_NP_018_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_018_Tests.cs) |
| NP_019 | ResearchY-NP_019 — Information Cosmology Audit |  | [ResearchY-NP_019.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_019.md) | [Y_NP_019_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_019_Tests.cs) |
| NP_020 | ResearchY-NP_020 — Black Hole Information Audit |  | [ResearchY-NP_020.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_020.md) | [Y_NP_020_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_020_Tests.cs) |
| NP_021 | ResearchY-NP_021 — Information Horizon Audit |  | [ResearchY-NP_021.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_021.md) | [Y_NP_021_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_021_Tests.cs) |
| NP_022 | ResearchY-NP_022 — Unique Physics Prediction Search |  | [ResearchY-NP_022.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_022.md) | [Y_NP_022_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_022_Tests.cs) |
| NP_023 | ResearchY-NP_023 — O(2) Mirror Search |  | [ResearchY-NP_023.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_023.md) | [Y_NP_023_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_023_Tests.cs) |
| NP_024 | ResearchY-NP_024 — O(2) Mirror-Pair Physical Prediction Audit |  | [ResearchY-NP_024.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_024.md) | [Y_NP_024_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_024_Tests.cs) |
| NP_025 | ResearchY-NP_025 — K=6 Uniqueness Audit |  | [ResearchY-NP_025.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_025.md) | [Y_NP_025_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_025_Tests.cs) |
| NP_026 | ResearchY-NP_026 — Protected Block Universality Audit |  | [ResearchY-NP_026.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_026.md) | [Y_NP_026_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_026_Tests.cs) |
| NP_027 | ResearchY-NP_027 — Planck Spectrum Emergence Audit |  | [ResearchY-NP_027.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_027.md) | [Y_NP_027_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_027_Tests.cs) |
| NP_028 | ResearchY-NP_028 — Blackbody Reconstruction Audit |  | [ResearchY-NP_028.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_028.md) | [Y_NP_028_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_028_Tests.cs) |
| NP_029 | ResearchY-NP_029 — ħ Necessity Audit |  | [ResearchY-NP_029.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_029.md) | [Y_NP_029_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_029_Tests.cs) |
| NP_030 | ResearchY-NP_030 — Temperature Origin Audit |  | [ResearchY-NP_030.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_030.md) | [Y_NP_030_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_030_Tests.cs) |
| NP_031 | ResearchY-NP_031 — Structure vs Thermodynamics Audit |  | [ResearchY-NP_031.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_031.md) | [Y_NP_031_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_031_Tests.cs) |
| NP_032 | ResearchY-NP_032 — Thermal-N Search Audit |  | [ResearchY-NP_032.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_032.md) | [Y_NP_032_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_032_Tests.cs) |
| NP_033 | ResearchY-NP_033 — D96 Ensemble Audit |  | [ResearchY-NP_033.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_033.md) | [Y_NP_033_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_033_Tests.cs) |
| NP_034 | ResearchY-NP_034 — Bose Without Blackbody Audit |  | [ResearchY-NP_034.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_034.md) | [Y_NP_034_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_034_Tests.cs) |
| NP_035 | ResearchY-NP_035 — Density-of-States Origin Audit |  | [ResearchY-NP_035.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_035.md) | [Y_NP_035_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_035_Tests.cs) |
| NP_036 | ResearchY-NP_036 — 3D Emergence Audit |  | [ResearchY-NP_036.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_036.md) | [Y_NP_036_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_036_Tests.cs) |
| NP_037 | ResearchY-NP_037 — The Role of Three Audit |  | [ResearchY-NP_037.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_037.md) | [Y_NP_037_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_037_Tests.cs) |
| NP_038 | ResearchY-NP_038 — Entanglement Audit |  | [ResearchY-NP_038.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_038.md) | [Y_NP_038_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_038_Tests.cs) |
| NP_039 | ResearchY-NP_039 — Minimal Entanglement Sector Audit |  | [ResearchY-NP_039.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_039.md) | [Y_NP_039_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_039_Tests.cs) |
| NP_040 | ResearchY-NP_040 — Joint Link Formalization Audit |  | [ResearchY-NP_040.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_040.md) | [Y_NP_040_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_040_Tests.cs) |
| NP_041 | ResearchY-NP_041 — Joint Link Consequence Audit |  | [ResearchY-NP_041.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_041.md) | [Y_NP_041_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_041_Tests.cs) |
| NP_042 | ResearchY-NP_042 — Multipartite Entanglement Audit |  | [ResearchY-NP_042.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_042.md) | [Y_NP_042_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_042_Tests.cs) |
| NP_043 | ResearchY-NP_043 — Joint State Origin Audit |  | [ResearchY-NP_043.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_043.md) | [Y_NP_043_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_043_Tests.cs) |
| NP_044 | ResearchY-NP_044 — Joint State Necessity Audit |  | [ResearchY-NP_044.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_044.md) | [Y_NP_044_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_044_Tests.cs) |
| NP_045 | ResearchY-NP_045 — CHSH Reality Audit |  | [ResearchY-NP_045.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_045.md) | [Y_NP_045_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_045_Tests.cs) |
| NP_046 | ResearchY-NP_046 — Joint State Physical Necessity Audit |  | [ResearchY-NP_046.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_046.md) | [Y_NP_046_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_046_Tests.cs) |
| NP_047 | ResearchY-NP_047 — Joint State Dynamics Audit |  | [ResearchY-NP_047.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_047.md) | [Y_NP_047_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_047_Tests.cs) |
| NP_048 | ResearchY-NP_048 — Entangling Gate Origin Audit |  | [ResearchY-NP_048.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_048.md) | [Y_NP_048_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_048_Tests.cs) |
| NP_049 | ResearchY-NP_049 — Entangling Gate Necessity Audit |  | [ResearchY-NP_049.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_049.md) | [Y_NP_049_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_049_Tests.cs) |
| NP_050 | ResearchY-NP_050 — Physical Realization Audit |  | [ResearchY-NP_050.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_050.md) | [Y_NP_050_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_050_Tests.cs) |
| NP_051 | ResearchY-NP_051 — Correspondence Layer Necessity Audit |  | [ResearchY-NP_051.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_051.md) | [Y_NP_051_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_051_Tests.cs) |
| NP_052 | ResearchY-NP_052 — Quantum Primitive Completeness Audit |  | [ResearchY-NP_052.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_052.md) | [Y_NP_052_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_052_Tests.cs) |
| NP_053 | ResearchY-NP_053 — Relativistic Consistency Audit |  | [ResearchY-NP_053.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_053.md) | [Y_NP_053_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_053_Tests.cs) |
| NP_054 | ResearchY-NP_054 — Quantum Completeness Stress Test |  | [ResearchY-NP_054.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_054.md) | [Y_NP_054_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_054_Tests.cs) |
| NP_055 | ResearchY-NP_055 — Dark Energy Ontology Audit |  | [ResearchY-NP_055.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_055.md) | [Y_NP_055_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_055_Tests.cs) |
| NP_056 | ResearchY-NP_056 — Equation-of-State Audit |  | [ResearchY-NP_056.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_056.md) | [Y_NP_056_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_056_Tests.cs) |
| NP_057 | ResearchY-NP_057 — Dark Energy Meaning Audit |  | [ResearchY-NP_057.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_057.md) | [Y_NP_057_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_057_Tests.cs) |
| NP_058 | ResearchY-NP_058 — Information-to-Energy Bridge Audit |  | [ResearchY-NP_058.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_058.md) | [Y_NP_058_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_058_Tests.cs) |
| NP_059 | ResearchY-NP_059 — Actualization Rate Audit |  | [ResearchY-NP_059.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_059.md) | [Y_NP_059_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_059_Tests.cs) |
| NP_060 | ResearchY-NP_060 — Dark Energy Resource Audit |  | [ResearchY-NP_060.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_060.md) | [Y_NP_060_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_060_Tests.cs) |
| NP_061 | ResearchY-NP_061 — ΩΛ Coincidence Audit |  | [ResearchY-NP_061.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_061.md) | [Y_NP_061_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_061_Tests.cs) |
| NP_062 | ResearchY-NP_062 — High-Order Universe Audit |  | [ResearchY-NP_062.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_062.md) | [Y_NP_062_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_062_Tests.cs) |
| NP_063 | ResearchY-NP_063 — Cosmological Coincidence Audit |  | [ResearchY-NP_063.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_063.md) | [Y_NP_063_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_063_Tests.cs) |
| NP_064 | ResearchY-NP_064 — Canonical Structure Necessity Audit |  | [ResearchY-NP_064.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_064.md) | [Y_NP_064_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_064_Tests.cs) |
| NP_065 | ResearchY-NP_065 — Dark Matter Ontology Audit |  | [ResearchY-NP_065.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_065.md) | [Y_NP_065_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_065_Tests.cs) |
| NP_066 | ResearchY-NP_066 — Dark Matter Evidence Audit |  | [ResearchY-NP_066.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_066.md) | [Y_NP_066_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_066_Tests.cs) |
| NP_067 | ResearchY-NP_067 — Lensing Sector Audit |  | [ResearchY-NP_067.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_067.md) | [Y_NP_067_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_067_Tests.cs) |
| NP_068 | ResearchY-NP_068 — Psi Ontology Audit |  | [ResearchY-NP_068.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_068.md) | [Y_NP_068_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_068_Tests.cs) |
| NP_069 | ResearchY-NP_069 — Expansion Ontology Audit |  | [ResearchY-NP_069.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_069.md) | [Y_NP_069_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_069_Tests.cs) |
| NP_070 | ResearchY-NP_070 — Criticality Audit |  | [ResearchY-NP_070.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_070.md) | — |
| NP_071 | ResearchY-NP_071 — Matter Ontology Audit |  | [ResearchY-NP_071.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_071.md) | [Y_NP_071_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_071_Tests.cs) |
| NP_072 | ResearchY-NP_072 — Particle Ontology Audit |  | [ResearchY-NP_072.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_072.md) | [Y_NP_072_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_072_Tests.cs) |
| NP_073 | ResearchY-NP_073 — Resonance Selection Audit |  | [ResearchY-NP_073.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_073.md) | [Y_NP_073_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_073_Tests.cs) |
| NP_074 | ResearchY-NP_074 — Quantum Number Ontology Audit |  | [ResearchY-NP_074.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_074.md) | [Y_NP_074_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_074_Tests.cs) |
| NP_075 | ResearchY-NP_075 — Force Ontology Audit |  | [ResearchY-NP_075.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_075.md) | [Y_NP_075_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_075_Tests.cs) |
| NP_076 | ResearchY-NP_076 — Psi Dark Matter Audit |  | [ResearchY-NP_076.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_076.md) | [Y_NP_076_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_076_Tests.cs) |
| NP_077 | ResearchY-NP_077 — Structure Formation Audit |  | [ResearchY-NP_077.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_077.md) | [Y_NP_077_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_077_Tests.cs) |
| NP_078 | ResearchY-NP_078 — Alpha=0 Necessity Audit |  | [ResearchY-NP_078.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_078.md) | [Y_NP_078_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_078_Tests.cs) |
| NP_079 | ResearchY-NP_079 — Scale-Freeness Origin Audit |  | [ResearchY-NP_079.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_079.md) | [Y_NP_079_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_079_Tests.cs) |
| NP_080 | ResearchY-NP_080 — Difference Duality Audit |  | [ResearchY-NP_080.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_080.md) | [Y_NP_080_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_080_Tests.cs) |
| NP_081 | ResearchY-NP_081 — Energy Ontology Audit |  | [ResearchY-NP_081.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_081.md) | [Y_NP_081_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_081_Tests.cs) |
| NP_082 | ResearchY-NP_082 — Electron Mass Anchor Audit |  | [ResearchY-NP_082.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_082.md) | [Y_NP_082_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_082_Tests.cs) |
| NP_083 | ResearchY-NP_083 — Threefold Structure Audit |  | [ResearchY-NP_083.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_083.md) | [Y_NP_083_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_083_Tests.cs) |
| NP_084 | ResearchY-NP_084 — Eta Framework Audit |  | [ResearchY-NP_084.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_084.md) | [Y_NP_084_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_084_Tests.cs) |
| NP_085 | ResearchY-NP_085 — Completeness Frontier Audit |  | [ResearchY-NP_085.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_085.md) | [Y_NP_085_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_085_Tests.cs) |
| NP_086 | ResearchY-NP_086 — Difference Necessity Audit |  | [ResearchY-NP_086.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_086.md) | [Y_NP_086_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_086_Tests.cs) |
| NP_087 | ResearchY-NP_087 — Nuclear Structure Audit |  | [ResearchY-NP_087.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_087.md) | [Y_NP_087_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_087_Tests.cs) |
| NP_088 | ResearchY-NP_088 — D96 Network Geometry Audit |  | [ResearchY-NP_088.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_088.md) | [Y_NP_088_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_088_Tests.cs) |
| NP_089 | ResearchY-NP_089 — Rotational Symmetry Emergence Audit |  | [ResearchY-NP_089.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_089.md) | [Y_NP_089_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_089_Tests.cs) |
| NP_090 | ResearchY-NP_090 — D96 Network Ontology Audit |  | [ResearchY-NP_090.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_090.md) | [Y_NP_090_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_090_Tests.cs) |
| NP_091 | ResearchY-NP_091 — Network Geometry → Spacetime Audit |  | [ResearchY-NP_091.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_091.md) | [Y_NP_091_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_091_Tests.cs) |
| NP_092 | ResearchY-NP_092 — Network Propagation Audit |  | [ResearchY-NP_092.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_092.md) | [Y_NP_092_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_092_Tests.cs) |
| NP_093 | ResearchY-NP_093 — Actualization Selection Audit |  | [ResearchY-NP_093.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_093.md) | [Y_NP_093_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_093_Tests.cs) |
| NP_094 | ResearchY-NP_094 — Inertia Ontology Audit |  | [ResearchY-NP_094.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_094.md) | [Y_NP_094_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_094_Tests.cs) |
| NP_095 | ResearchY-NP_095 — Friction Ontology Audit |  | [ResearchY-NP_095.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_095.md) | [Y_NP_095_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_095_Tests.cs) |
| NP_096 | ResearchY-NP_096 — Heat Ontology Audit |  | [ResearchY-NP_096.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_096.md) | [Y_NP_096_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_096_Tests.cs) |
| NP_097 | ResearchY-NP_097 — Temperature Ontology Audit |  | [ResearchY-NP_097.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_097.md) | [Y_NP_097_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_097_Tests.cs) |
| NP_098 | ResearchY-NP_098 — Localization Ontology Audit |  | [ResearchY-NP_098.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_098.md) | [Y_NP_098_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_098_Tests.cs) |
| NP_099 | ResearchY-NP_099 — Classicality Emergence Audit |  | [ResearchY-NP_099.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_099.md) | [Y_NP_099_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_099_Tests.cs) |
| NP_100 | ResearchY-NP_100 — Bound Structure Audit |  | [ResearchY-NP_100.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_100.md) | [Y_NP_100_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_100_Tests.cs) |
| NP_101 | ResearchY-NP_101 — Hierarchical Structure Audit |  | [ResearchY-NP_101.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_101.md) | [Y_NP_101_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_101_Tests.cs) |
| NP_102 | ResearchY-NP_102 — Existence Ontology Audit |  | [ResearchY-NP_102.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_102.md) | [Y_NP_102_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_102_Tests.cs) |
| NP_103 | ResearchY-NP_103 — Nonexistence Ontology Audit |  | [ResearchY-NP_103.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_103.md) | [Y_NP_103_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_103_Tests.cs) |
| NP_104 | ResearchY-NP_104 — Difference Persistence Audit |  | [ResearchY-NP_104.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_104.md) | [Y_NP_104_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_104_Tests.cs) |
| NP_105 | ResearchY-NP_105 — Difference Conservation Audit |  | [ResearchY-NP_105.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_105.md) | [Y_NP_105_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_105_Tests.cs) |
| NP_106 | ResearchY-NP_106 — Actualization Necessity Audit |  | [ResearchY-NP_106.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_106.md) | [Y_NP_106_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_106_Tests.cs) |
| NP_107 | ResearchY-NP_107 — Tick Necessity Audit |  | [ResearchY-NP_107.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_107.md) | [Y_NP_107_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_107_Tests.cs) |
| NP_109 | ResearchY-NP_109 — Weakest Link Audit |  | [ResearchY-NP_109.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_109.md) | [Y_NP_109_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_109_Tests.cs) |
| NP_110 | ResearchY-NP_110 — Condensed Matter Gateway Audit |  | [ResearchY-NP_110.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_110.md) | [Y_NP_110_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_110_Tests.cs) |
| NP_111 | ResearchY-NP_111 — Observer Ontology Audit |  | [ResearchY-NP_111.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_111.md) | [Y_NP_111_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_111_Tests.cs) |
| NP_112 | ResearchY-NP_112 — Reality Appearance Audit |  | [ResearchY-NP_112.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_112.md) | [Y_NP_112_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_112_Tests.cs) |
| NP_113 | ResearchY-NP_113 — Observed Ontology Audit |  | [ResearchY-NP_113.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_113.md) | [Y_NP_113_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_113_Tests.cs) |
| NP_115 | ResearchY-NP_115 — Understanding Ontology Audit |  | [ResearchY-NP_115.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_115.md) | [Y_NP_115_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_115_Tests.cs) |
| NP_116 | ResearchY-NP_116 — Meaning Ontology Audit |  | [ResearchY-NP_116.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_116.md) | [Y_NP_116_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_116_Tests.cs) |
| NP_117 | ResearchY-NP_117 — Purpose Ontology Audit |  | [ResearchY-NP_117.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_117.md) | [Y_NP_117_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_117_Tests.cs) |
| NP_118 | ResearchY-NP_118 — Choice Ontology Audit |  | [ResearchY-NP_118.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_118.md) | [Y_NP_118_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_118_Tests.cs) |
| NP_119 | ResearchY-NP_119 — Freedom Ontology Audit |  | [ResearchY-NP_119.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_119.md) | [Y_NP_119_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_119_Tests.cs) |
| NP_120 | ResearchY-NP_120 — Responsibility Ontology Audit |  | [ResearchY-NP_120.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_120.md) | [Y_NP_120_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_120_Tests.cs) |
| NP_121 | ResearchY-NP_121 — Identity Ontology Audit |  | [ResearchY-NP_121.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_121.md) | [Y_NP_121_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_121_Tests.cs) |
| NP_122 | ResearchY-NP_122 — Consciousness Ontology Audit |  | [ResearchY-NP_122.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_122.md) | [Y_NP_122_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_122_Tests.cs) |
| NP_123 | ResearchY-NP_123 — Truth Ontology Audit |  | [ResearchY-NP_123.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_123.md) | [Y_NP_123_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_123_Tests.cs) |
| NP_124 | ResearchY-NP_124 — Wisdom Ontology Audit |  | [ResearchY-NP_124.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_124.md) | [Y_NP_124_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_124_Tests.cs) |
| NP_125 | ResearchY-NP_125 — Cooperation Ontology Audit |  | [ResearchY-NP_125.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_125.md) | [Y_NP_125_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_125_Tests.cs) |
| NP_126 | ResearchY-NP_126 — Resonance Sailing Audit |  | [ResearchY-NP_126.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_126.md) | [Y_NP_126_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_126_Tests.cs) |
| NP_127 | ResearchY-NP_127 — Resonance Engineering Audit |  | [ResearchY-NP_127.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_127.md) | [Y_NP_127_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_127_Tests.cs) |
| NP_128 | ResearchY-NP_128 — Coherence Resource Audit |  | [ResearchY-NP_128.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_128.md) | [Y_NP_128_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_128_Tests.cs) |
| NP_129 | ResearchY-NP_129 — Coherent Matter Control Audit |  | [ResearchY-NP_129.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_129.md) | [Y_NP_129_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_129_Tests.cs) |
| NP_130 | ResearchY-NP_130 — Material Sonification & Inversion Audit |  | [ResearchY-NP_130.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_130.md) | [Y_NP_130_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_130_Tests.cs) |
| NP_131 | ResearchY-NP_131 — Critical Resonance Audit |  | [ResearchY-NP_131.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_131.md) | [Y_NP_131_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_131_Tests.cs) |
| NP_132 | ResearchY-NP_132 — Reversible Softening Audit |  | [ResearchY-NP_132.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_132.md) | [Y_NP_132_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_132_Tests.cs) |
| NP_133 | ResearchY-NP_133 — Critical Mode Frequency Audit |  | [ResearchY-NP_133.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_133.md) | [Y_NP_133_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_133_Tests.cs) |
| NP_134 | ResearchY-NP_134 — Critical Mode Discovery Audit |  | [ResearchY-NP_134.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_134.md) | [Y_NP_134_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_134_Tests.cs) |
| NP_135 | ResearchY-NP_135 — Coherent Softening Experimental Audit |  | [ResearchY-NP_135.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_135.md) | [Y_NP_135_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_135_Tests.cs) |
| NP_136 | ResearchY-NP_136 — Variable Rigidity Bounds Audit |  | [ResearchY-NP_136.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_136.md) | [Y_NP_136_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_136_Tests.cs) |
| NP_137 | ResearchY-NP_137 — Real-World Evidence Audit |  | [ResearchY-NP_137.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_137.md) | [Y_NP_137_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_137_Tests.cs) |
| NP_138 | ResearchY-NP_138 — Quantized Softening Audit |  | [ResearchY-NP_138.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_138.md) | [Y_NP_138_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_138_Tests.cs) |
| NP_139 | ResearchY-NP_139 — Soft Mode Literature Audit |  | [ResearchY-NP_139.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_139.md) | [Y_NP_139_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_139_Tests.cs) |
| NP_140 | ResearchY-NP_140 — Fixed-Temperature Softening Audit |  | [ResearchY-NP_140.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_140.md) | [Y_NP_140_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_140_Tests.cs) |
| NP_141 | ResearchY-NP_141 — Yield Stress Softening Audit |  | [ResearchY-NP_141.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_141.md) | [Y_NP_141_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_141_Tests.cs) |
| NP_142 | ResearchY-NP_142 — Multi-Band Resonance Control Audit |  | [ResearchY-NP_142.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_142.md) | [Y_NP_142_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_142_Tests.cs) |
| NP_143 | ResearchY-NP_143 — Dislocation Threshold Audit |  | [ResearchY-NP_143.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_143.md) | [Y_NP_143_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_143_Tests.cs) |
| NP_144 | ResearchY-NP_144 — Defect Spectrum Fingerprint Audit |  | [ResearchY-NP_144.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_144.md) | [Y_NP_144_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_144_Tests.cs) |
| NP_145 | ResearchY-NP_145 — Waveform Control Audit |  | [ResearchY-NP_145.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_145.md) | [Y_NP_145_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_145_Tests.cs) |
| NP_146 | ResearchY-NP_146 — Energy Pathway Audit |  | [ResearchY-NP_146.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_146.md) | [Y_NP_146_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_146_Tests.cs) |
| NP_147 | ResearchY-NP_147 — Defect Writing Audit |  | [ResearchY-NP_147.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_147.md) | [Y_NP_147_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_147_Tests.cs) |
| NP_148 | ResearchY-NP_148 — Property Programming Audit |  | [ResearchY-NP_148.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_148.md) | [Y_NP_148_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_148_Tests.cs) |
| NP_149 | ResearchY-NP_149 — Defect State Optimization Audit |  | [ResearchY-NP_149.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_149.md) | [Y_NP_149_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_149_Tests.cs) |
| NP_150 | ResearchY-NP_150 — Property Programming Timescale Audit |  | [ResearchY-NP_150.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_150.md) | [Y_NP_150_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_150_Tests.cs) |
| NP_151 | ResearchY-NP_151 — AT Contribution Audit |  | [ResearchY-NP_151.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_151.md) | [Y_NP_151_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_151_Tests.cs) |
| NP_152 | ResearchY-NP_152 — Material Memory Audit |  | [ResearchY-NP_152.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_152.md) | [Y_NP_152_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_152_Tests.cs) |
| NP_153 | ResearchY-NP_153 — Material State Space Audit |  | [ResearchY-NP_153.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_153.md) | [Y_NP_153_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_153_Tests.cs) |
| NP_154 | ResearchY-NP_154 — Contact Network Softening Audit |  | [ResearchY-NP_154.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_154.md) | [Y_NP_154_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_154_Tests.cs) |
| NP_155 | ResearchY-NP_155 — Force Chain Programming Audit |  | [ResearchY-NP_155.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_155.md) | [Y_NP_155_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_155_Tests.cs) |
| NP_156 | ResearchY-NP_156 — Force Path Steering Audit |  | [ResearchY-NP_156.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_156.md) | [Y_NP_156_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_156_Tests.cs) |
| NP_157 | ResearchY-NP_157 — Organization vs Material Audit |  | [ResearchY-NP_157.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_157.md) | [Y_NP_157_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_157_Tests.cs) |
| NP_158 | ResearchY-NP_158 — Latent Organization State Audit |  | [ResearchY-NP_158.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_158.md) | [Y_NP_158_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_158_Tests.cs) |
| NP_159 | ResearchY-NP_159 — Structural Training Audit |  | [ResearchY-NP_159.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_159.md) | [Y_NP_159_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_159_Tests.cs) |
| NP_160 | ResearchY-NP_160 — Organizational Field Audit |  | [ResearchY-NP_160.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_160.md) | [Y_NP_160_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_160_Tests.cs) |
| NP_161 | ResearchY-NP_161 — Organizational Wave Audit |  | [ResearchY-NP_161.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_161.md) | [Y_NP_161_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_161_Tests.cs) |
| NP_162 | ResearchY-NP_162 — Organizational Tomography Audit |  | [ResearchY-NP_162.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_162.md) | [Y_NP_162_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_162_Tests.cs) |
| NP_163 | ResearchY-NP_163 — Organizational Path Dependence Audit |  | [ResearchY-NP_163.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_163.md) | [Y_NP_163_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_163_Tests.cs) |
| NP_164 | ResearchY-NP_164 — Organizational Amplification Audit |  | [ResearchY-NP_164.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_164.md) | [Y_NP_164_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_164_Tests.cs) |
| NP_166 | ResearchY-NP_166 — Periodic Table Emergence Audit |  | [ResearchY-NP_166.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_166.md) | [Y_NP_166_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_166_Tests.cs) |
| NP_167 | ResearchY-NP_167 — Unknown Organization States Audit |  | [ResearchY-NP_167.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_167.md) | [Y_NP_167_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_167_Tests.cs) |
| NP_168 | ResearchY-NP_168 — Yang–Mills Existence and Mass Gap Audit |  | [ResearchY-NP_168.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_168.md) | [Y_NP_168_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_168_Tests.cs) |
| NP_169 | ResearchY-NP_169 — Natural Scaling vs Mass Gap Audit |  | [ResearchY-NP_169.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_169.md) | [Y_NP_169_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_169_Tests.cs) |
| NP_170 | ResearchY-NP_170 — Lock-Lattice Phase Memory (Physical Realizability Test) |  | [ResearchY-NP_170.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_170.md) | — |
| NP_171 | ResearchY-NP_171 — Deterministic Lock-Lattice Simulator |  | [ResearchY-NP_171.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_171.md) | [Y_NP_171_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_171_Tests.cs) |
| NP_172 | ResearchY-NP_172 — Retention Null Audit |  | [ResearchY-NP_172.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_172.md) | [Y_NP_172_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_172_Tests.cs) |
| NP_173 | ResearchY-NP_173 — Discriminator Audit |  | [ResearchY-NP_173.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_173.md) | [Y_NP_173_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_173_Tests.cs) |
| NP_174 | ResearchY-NP_174 — Program Synthesis: does any discriminator remain? |  | [ResearchY-NP_174.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_174.md) | [Y_NP_174_Tests.cs](AT.Tests/ResearchY/NP_NewPhysics/Y_NP_174_Tests.cs) |
| NP_175 | ResearchY-NP_175 — Lock-Lattice Closure Audit |  | [ResearchY-NP_175.md](Docs/ResearchY/NP_NewPhysics/ResearchY-NP_175.md) | — |
| QG_001 | ResearchY-QG_001 — Information–Geometry Bridge Audit |  | [ResearchY-QG_001.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_001.md) | [Y_QG_001_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_001_Tests.cs) |
| QG_002 | ResearchY-QG_002 — Distinguishability → Geometry Audit |  | [ResearchY-QG_002.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_002.md) | [Y_QG_002_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_002_Tests.cs) |
| QG_003 | ResearchY-QG_003 — Information Reconstruction Audit |  | [ResearchY-QG_003.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_003.md) | [Y_QG_003_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_003_Tests.cs) |
| QG_004 | ResearchY-QG_004 — ρ Nature Audit |  | [ResearchY-QG_004.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_004.md) | [Y_QG_004_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_004_Tests.cs) |
| QG_005 | ResearchY-QG_005 — Count-to-Geometry Origin Audit |  | [ResearchY-QG_005.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_005.md) | [Y_QG_005_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_005_Tests.cs) |
| QG_006 | ResearchY-QG_006 — Count Conservation Origin Audit |  | [ResearchY-QG_006.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_006.md) | [Y_QG_006_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_006_Tests.cs) |
| QG_007 | ResearchY-QG_007 — Count Conservation Necessity Audit |  | [ResearchY-QG_007.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_007.md) | [Y_QG_007_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_007_Tests.cs) |
| QG_008 | ResearchY-QG_008 — Finite Distinguishability Audit |  | [ResearchY-QG_008.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_008.md) | [Y_QG_008_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_008_Tests.cs) |
| QG_009 | ResearchY-QG_009 — Infinite State Space Consistency Audit |  | [ResearchY-QG_009.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_009.md) | [Y_QG_009_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_009_Tests.cs) |
| QG_010 | ResearchY-QG_010 — Observable Finiteness Audit |  | [ResearchY-QG_010.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_010.md) | [Y_QG_010_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_010_Tests.cs) |
| QG_011 | ResearchY-QG_011 — Finite Event Principle Audit |  | [ResearchY-QG_011.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_011.md) | [Y_QG_011_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_011_Tests.cs) |
| QG_012 | ResearchY-QG_012 — Distinguishability Cosmology Audit |  | [ResearchY-QG_012.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_012.md) | [Y_QG_012_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_012_Tests.cs) |
| QG_013 | ResearchY-QG_013 — Three-Family Origin Audit |  | [ResearchY-QG_013.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_013.md) | [Y_QG_013_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_013_Tests.cs) |
| QG_014 | ResearchY-QG_014 — Cosmological Selection Audit |  | [ResearchY-QG_014.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_014.md) | [Y_QG_014_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_014_Tests.cs) |
| QG_015 | ResearchY-QG_015 — Observable World Audit |  | [ResearchY-QG_015.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_015.md) | [Y_QG_015_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_015_Tests.cs) |
| QG_016 | ResearchY-QG_016 — Tick Discreteness Origin Audit |  | [ResearchY-QG_016.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_016.md) | [Y_QG_016_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_016_Tests.cs) |
| QG_017 | ResearchY-QG_017 — Distinguishability Cosmology Extension Audit |  | [ResearchY-QG_017.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_017.md) | [Y_QG_017_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_017_Tests.cs) |
| QG_018 | ResearchY-QG_018 — Information-Cosmology Closure Audit |  | [ResearchY-QG_018.md](Docs/ResearchY/QG_GeometryBridge/ResearchY-QG_018.md) | [Y_QG_018_Tests.cs](AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_018_Tests.cs) |
| QM_001 | ResearchY-QM_001 - ManyBody Correspondence Audit | PARTIAL | [ResearchY-QM_001.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_001.md) | [Y_QM_001_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_001_Tests.cs) |
| QM_002 | ResearchY-QM_002 - Unitary Correspondence Audit | PARTIAL | [ResearchY-QM_002.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_002.md) | [Y_QM_002_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_002_Tests.cs) |
| QM_003 | ResearchY-QM_003 - Dispersion Closure Audit | BOUNDARY | [ResearchY-QM_003.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_003.md) | [Y_QM_003_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_003_Tests.cs) |
| QM_004 | ResearchY-QM_004 - Schrodinger Correspondence Audit | PARTIAL | [ResearchY-QM_004.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_004.md) | [Y_QM_004_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_004_Tests.cs) |
| QM_005 | ResearchY-QM_005 - Laplacian Dispersion Audit | BOUNDARY | [ResearchY-QM_005.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_005.md) | [Y_QM_005_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_005_Tests.cs) |
| QM_006 | ResearchY-QM_006 - Schrodinger Propagator Audit | PARTIAL | [ResearchY-QM_006.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_006.md) | [Y_QM_006_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_006_Tests.cs) |
| QM_007 | ResearchY-QM_007 - Generator Selection Audit | BOUNDARY | [ResearchY-QM_007.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_007.md) | [Y_QM_007_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_007_Tests.cs) |
| QM_008 | ResearchY-QM_008 - Spectral Necessity Audit | BOUNDARY | [ResearchY-QM_008.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_008.md) | [Y_QM_008_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_008_Tests.cs) |
| QM_009 | ResearchY-QM_009 - Fingerprint Necessity Audit | BOUNDARY | [ResearchY-QM_009.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_009.md) | [Y_QM_009_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_009_Tests.cs) |
| QM_010 | ResearchY-QM_010 - Fingerprint Load Audit | FINGERPRINT | [ResearchY-QM_010.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_010.md) | [Y_QM_010_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_010_Tests.cs) |
| QM_011 | ResearchY-QM_011 - Fingerprint Justification Audit | NONE | [ResearchY-QM_011.md](Docs/ResearchY/QM_ManyBody/ResearchY-QM_011.md) | [Y_QM_011_Tests.cs](AT.Tests/ResearchY/QM_ManyBody/Y_QM_011_Tests.cs) |
| R_001 | ResearchY-R_001 — V2.1 Boundary Program Closure Audit |  | [ResearchY-R_001.md](Docs/ResearchY/R_BoundaryProgram/ResearchY-R_001.md) | [Y_R_001_Tests.cs](AT.Tests/ResearchY/R_BoundaryProgram/Y_R_001_Tests.cs) |
| R_002 | ResearchY-R_002 — V2.1 Release Preparation |  | [ResearchY-R_002.md](Docs/ResearchY/R_BoundaryProgram/ResearchY-R_002.md) | — |
| S_001 | ResearchY-S_001 — Post-V2.1 Theory Architecture Synthesis |  | [ResearchY-S_001.md](Docs/ResearchY/S_Synthesis/ResearchY-S_001.md) | [Y_S_001_Tests.cs](AT.Tests/ResearchY/S_Synthesis/Y_S_001_Tests.cs) |
| T_001 | ResearchY-T_001 — Spectral Blueprint (Inverse Spectral Design) |  | [ResearchY-T_001.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_001.md) | [Y_T_001_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_001_Tests.cs) |
| T_002 | ResearchY-T_002 — Physical Spectrum Audit |  | [ResearchY-T_002.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_002.md) | [Y_T_002_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_002_Tests.cs) |
| T_003 | ResearchY-T_003 — General Inverse Spectral Graph Audit |  | [ResearchY-T_003.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_003.md) | [Y_T_003_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_003_Tests.cs) |
| T_004 | ResearchY-T_004 — Spectral Rigidity Audit |  | [ResearchY-T_004.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_004.md) | [Y_T_004_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_004_Tests.cs) |
| T_005 | ResearchY-T_005 — Attractor Dominance Audit |  | [ResearchY-T_005.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_005.md) | [Y_T_005_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_005_Tests.cs) |
| T_006 | ResearchY-T_006 — Darwinian Dominance Emergence |  | [ResearchY-T_006.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_006.md) | [Y_T_006_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_006_Tests.cs) |
| T_007 | ResearchY-T_007 — Bounded Innovation Audit |  | [ResearchY-T_007.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_007.md) | [Y_T_007_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_007_Tests.cs) |
| T_008 | ResearchY-T_008 — Asymptotic Diversity Limit Audit |  | [ResearchY-T_008.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_008.md) | [Y_T_008_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_008_Tests.cs) |
| T_009 | ResearchY-T_009 — Fitness-Reach Law Audit |  | [ResearchY-T_009.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_009.md) | [Y_T_009_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_009_Tests.cs) |
| T_010 | ResearchY-T_010 — D96 Survivor Compression Audit |  | [ResearchY-T_010.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_010.md) | [Y_T_010_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_010_Tests.cs) |
| T_011 | ResearchY-T_011 — Species Ceiling Audit |  | [ResearchY-T_011.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_011.md) | [Y_T_011_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_011_Tests.cs) |
| T_012 | ResearchY-T_012 — Origin of 19 Audit |  | [ResearchY-T_012.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_012.md) | [Y_T_012_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_012_Tests.cs) |
| T_013 | ResearchY-T_013 — Compression Origin Audit |  | [ResearchY-T_013.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_013.md) | [Y_T_013_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_013_Tests.cs) |
| T_014 | ResearchY-T_014 — Near-Gap Density Audit |  | [ResearchY-T_014.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_014.md) | [Y_T_014_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_014_Tests.cs) |
| T_015 | ResearchY-T_015 — Spectral Robustness Audit |  | [ResearchY-T_015.md](Docs/ResearchY/T_SpectralBlueprint/ResearchY-T_015.md) | [Y_T_015_Tests.cs](AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_015_Tests.cs) |

## Legacy programmes (pre-ResearchY)

The AT-QG / TRM phase series predates the ResearchY identifiers. Its documents live in `Docs/Research/`
and its suites in the `AT.Tests/Research*` directories; they are catalogued by directory rather than by
identifier, because the phase numbers (for example `AT_QG086`) are the identifiers there.

| Directory | Test files | Notes |
|---|---|---|
| AT.Tests/Research | 139 | the original research suites |
| AT.Tests/ResearchDATA | 21 | data-driven suites (gated) |
| AT.Tests/ResearchQG | 118 | the AT-QG phase series |
| AT.Tests/ResearchQM | 5 | quantum-mechanics correspondence |
| AT.Tests/ResearchX | 78 | extended research programmes X* |
| AT.Tests/ResearchXB | 10 | extended research programmes X* |
| AT.Tests/ResearchXC | 26 | extended research programmes X* |
| AT.Tests/ResearchXD | 6 | extended research programmes X* |
| AT.Tests/ResearchXE | 9 | extended research programmes X* |
| AT.Tests/ResearchXF | 5 | extended research programmes X* |
| AT.Tests/ResearchXG | 1 | extended research programmes X* |
| AT.Tests/ResearchXH | 401 | AT-QG phases 200+ and the ResearchXH core audits |
| Docs/Research/ | 422 markdown documents | legacy reports and phase documents |

Not indexed (no numeric identifier in the file name): ResearchY-A_WaveOriginAudit.md.

