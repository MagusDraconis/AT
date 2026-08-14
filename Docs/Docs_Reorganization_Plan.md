# Documentation Reorganization Plan

**Goal:** organize every `*.md` by purpose into five folders.
**Not moved:** `README.md`, `.github/copilot-instructions.md`, and gitignored `LegacyDocs/`.

---

## 1. Target Structure

| Folder | Purpose | Contents |
|---|---|---|
| `Docs/Main/` | Authoritative references | Master Reference, Encyclopedia, Coverage Report |
| `Docs/Reports/` | Analysis outputs, roadmaps, validation, predictions | `*Report.md`, `*Roadmap.md`, `*Validation`, `*Predictions` |
| `Docs/Theory/` | Theorems and derivations | `*Theory.md`, derivation docs, postulates, foundations |
| `Docs/Audits/` | Hostile audits + methodology | `*Audit.md`, phase reports, audit frameworks |
| `Docs/Archive/` | Obsolete / superseded | rejected cosmology reinterpretations |
| *(kept in place)* | Special-purpose | `Docs/NewChat_Start.md`, `Docs/TQM_LabBook.md`, `Docs/ObservationalTests/`, `Docs/ResearchXB/` |

---

## 2. Destination for Every File

### 2.1 `Docs/Main/` (3 — unchanged)

| File | Current | Destination |
|---|---|---|
| TQM_Master_Reference.md | Docs/Main | Docs/Main (keep) |
| TQM_Encyclopedia.md | Docs/Main | Docs/Main (keep) |
| Coverage_Report.md | Docs/Main | Docs/Main (keep) |

### 2.2 `Docs/Audits/`

| File | Current |
|---|---|
| AcousticGapAudit.md | Docs/Main |
| ClusterMassAudit.md | Docs/Main |
| PlanckConstraintAudit.md | Docs/Main |
| TRM_Reconciliation_Audit.md | Docs/Main |
| X01_Alternative_Foundations_Audit.md | Docs/Theory |
| TQM-Failure-Analysis-Framework-V1.md | Docs/WhitePapers |
| TQM-Framework-Revision-Protocol-V1.md | Docs/WhitePapers |

### 2.3 `Docs/Reports/`

| File | Current |
|---|---|
| CMB_Roadmap.md | Docs/Main |
| CausalSetLambdaReport.md | Docs |
| ChargeCreationStatisticsReport.md | Docs |
| ChargeExcitationReport.md | Docs |
| ChargeModeInterferenceReport.md | Docs |
| ChargePhaseDiagram.md | Docs |
| ChargeQuantizationReport.md | Docs |
| ChargeQuantumOriginReport.md | Docs |
| ChargeQuantumReport.md | Docs |
| CollectiveChargeWaveReport.md | Docs |
| CollectiveWaveFieldReport.md | Docs |
| DerivedObservables.md | Docs |
| DistinctPredictions.md | Docs |
| EvolutionUniversalityReport.md | Docs |
| GeometryHierarchyReport.md | Docs |
| InformationAttractorsReport.md | Docs |
| InformationFitnessLawReport.md | Docs |
| InformationGenesisReport.md | Docs |
| InformationInheritanceReport.md | Docs |
| InformationLandscapeReport.md | Docs |
| InformationSelectionReport.md | Docs |
| InterChargeCoherenceReport.md | Docs |
| NonlinearModeCompositionReport.md | Docs |
| ObservableConsequences.md | Docs |
| OpenEndedInnovationReport.md | Docs |
| OriginOfHReport.md | Docs |
| PhysicalObservableReport.md | Docs |
| PhysicalSpectrumReport.md | Docs |
| PhysicsCandidateValidation.md | Docs |
| Predictions.md | Docs |
| PredictionsFromChange.md | Docs |
| ProtoMatterCollectiveReport.md | Docs |
| SpectralLandscapeReport.md | Docs |
| TQMPhysicalCorrespondence.md | Docs |
| ThetaInformationDynamicsReport.md | Docs |
| ThetaInformationReport.md | Docs |
| ThetaMemoryReport.md | Docs |
| ThetaOperatorOriginReport.md | Docs |
| TheoryCompressionReport.md | Docs |
| TheoryRepairReport.md | Docs |
| UnifiedPhysicsCandidate.md | Docs |
| WhyThisH.md | Docs |
| TQM-WhitePaper-Experimental-Priorities-2025-2035-V1.md | Docs/WhitePapers |

### 2.4 `Docs/Theory/`

| File | Current |
|---|---|
| AutonomousInformationLayer.md | Docs |
| ChargeCoherenceTheory.md | Docs |
| CollectiveChargeWaveTheory.md | Docs |
| CosmologyDependencyGraph.md | Docs |
| DarwinianInformationEcology.md | Docs |
| EmergentChargeWaveTheory.md | Docs |
| FundamentalChargeTheory.md | Docs |
| FundamentalInformationFitness.md | Docs |
| FundamentalQuantizationLaw.md | Docs |
| InformationEvolutionTheory.md | Docs |
| InformationLandscapeTheory.md | Docs |
| InformationMatterFeedbackTheory.md | Docs |
| InformationSpeciesTheory.md | Docs |
| MicroscopicOriginOfTheta.md | Docs |
| MinimalProtoMatterTheory.md | Docs |
| NonlinearSpectralGeometry.md | Docs |
| OpenEndedInformationEvolution.md | Docs |
| OriginOfCausality.md | Docs |
| ProtoMatterAbundanceTheory.md | Docs |
| SpectralInformationTheory.md | Docs |
| ThetaFieldTheory.md | Docs |
| ThetaInformationFieldTheory.md | Docs |
| ThetaMemoryFieldTheory.md | Docs |
| TopologicalWaveQuantum.md | Docs |
| UniversalGraphInformationPhysics.md | Docs |
| UniversalInformationEvolution.md | Docs |
| XB001-Origin-Of-Abundance.md | Docs/ResearchXB |
| XB002-Universal-Abundance-Distribution.md | Docs/ResearchXB |
| *(existing numbered docs 00–13 + TQM_* stay)* | Docs/Theory |

### 2.5 `Docs/Archive/` (superseded — collapse-to-FLRW reinterpretations)

| File | Current | Superseded by |
|---|---|---|
| TimeFirstCosmology.md | Docs | QG-080/089 (time-first → FLRW) |
| TimeScaleCosmology.md | Docs | QG-080 |
| TimeScaleEquations.md | Docs | QG-080 |
| EventDrivenRedshift.md | Docs | QG-087 |
| EventStructureCosmology.md | Docs | QG-088 |
| ExpansionVsEvents.md | Docs | QG-087 |
| ModelIndependentRedshift.md | Docs | QG-081 |
| RateFirstCosmology.md | Docs | QG-089 |
| RAR_TimeInterpretation.md | Docs | QG-085/086 |
| GammaVsScaleFactor.md | Docs | QG-082 |
| UnifiedCosmologicalScaleReport.md | Docs | QG-095 |
| UnifiedEmergentScaleReport.md | Docs | QG-095/096 |

### 2.6 Kept in place (special purpose)

| File | Location | Reason |
|---|---|---|
| NewChat_Start.md | Docs | mandatory project memory |
| TQM_LabBook.md | Docs | lab book (append-only log) |
| TQM_QuantumGravity_Program.md | Docs | program summary (README-linked) |
| ObservationalTests/* (5) | Docs/ObservationalTests | already purpose-organized |
| README.md | root | do not move |

---

## 3. Move Plan (git mv, grouped)

```bash
# Audits
git mv Docs/Main/AcousticGapAudit.md            Docs/Audits/
git mv Docs/Main/ClusterMassAudit.md            Docs/Audits/
git mv Docs/Main/PlanckConstraintAudit.md       Docs/Audits/
git mv Docs/Main/TRM_Reconciliation_Audit.md    Docs/Audits/
git mv Docs/Theory/X01_Alternative_Foundations_Audit.md Docs/Audits/
git mv Docs/WhitePapers/TQM-Failure-Analysis-Framework-V1.md       Docs/Audits/
git mv Docs/WhitePapers/TQM-Framework-Revision-Protocol-V1.md      Docs/Audits/

# Reports (all *Report.md, *Roadmap.md, predictions, validation)
git mv Docs/Main/CMB_Roadmap.md Docs/Reports/
git mv Docs/CausalSetLambdaReport.md Docs/Reports/
# ... (every file in §2.3)

# Theory (all *Theory.md + derivations)
git mv Docs/FundamentalChargeTheory.md Docs/Theory/
# ... (every file in §2.4)

# Archive
git mv Docs/TimeFirstCosmology.md Docs/Archive/
# ... (every file in §2.5)
```

> Full command list is implied by the §2 tables; execute with `git mv` to preserve history.

---

## 4. Link Updates Required

| Source | Change |
|---|---|
| `README.md` | references `Docs/TQM_QuantumGravity_Program.md` (kept) — **no change** |
| `Docs/Main/Coverage_Report.md` | references `ClusterMassAudit.md` → `Docs/Audits/ClusterMassAudit.md` |
| `Docs/Main/TQM_Encyclopedia.md` | references `ClusterMassAudit.md`, `CMB_Roadmap.md` → update to `Docs/Audits/`, `Docs/Reports/` |
| `Docs/Main/TQM_Master_Reference.md` | self-contained — **no change** |
| `Docs/Main/PlanckConstraintAudit.md` | references `Coverage_Report.md`, `TQM_Encyclopedia.md` (stay in Main) — **no change** |
| cross-refs among moved audits | same folder after move — **no change** |

---

## 5. Archive Rationale

The 12 files in §2.5 are the "cosmic-clock / event / rate / structure-first"
reinterpretations audited in QG-080–089 (Phases 127–136). Each was found to
**collapse to FLRW or be falsified**; their content is superseded by the
FLRW + $w(z)$ framework and `TQM_Master_Reference.md`. They are retained for
reference, not deleted.

---

## 6. Execution Order

1. Create `Docs/Reports/`, `Docs/Audits/`, `Docs/Archive/` (Theory exists).
2. Run §3 `git mv` commands.
3. Apply §4 link updates.
4. Commit with the Co-authored-by trailer.
