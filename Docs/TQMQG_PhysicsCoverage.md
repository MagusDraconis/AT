# TQM-QG Physics Coverage

**Single source of truth for all TQM-QG physics validation.**

- Last updated: 2026-08-22
- Total phases: 193
- Tested: 180 | Partial: 12 | Untested: 0 | Audit: 1
- Weighted coverage: 96.5%

> Maintenance rule: whenever a QG phase completes, scan its classification, update
> tested/partial/untested, contradictions, open questions, predictions, and statistics.
> Historical entries are never removed. Machine-readable twin: `TQMQG_PhysicsCoverage.json`.

---

## 1. Coverage Statistics

| Metric | Value |
|---|---|
| Phases total | 193 |
| Tested | 180 |
| Partially tested | 12 |
| Untested | 0 |
| Audit (QG170) | 1 |
| Weighted coverage | 96.5% |
| SM tested | 49 |
| Gravity tested | 25 |

### Observable-level coverage (SM quantities)

| Metric | Value |
|---|---|
| Observables catalogued | 40 |
| Tested | 30 |
| Partially tested | 6 |
| Untested | 4 |
| Observable coverage | 82.5% |

> QG170's original audit (25 tested / 9 partial / 14 untested of 48 quantities, 64%)
> is superseded at observable level by QG171-182; the phase register below is the
> authoritative current source.

### By domain

| Domain | Tested | Partial | Untested | Audit | Total |
|---|---|---|---|---|---|
| Cosmology | 1 | 0 | 0 | 0 | 1 |
| High-Energy Sector | 6 | 1 | 0 | 0 | 7 |
| Foundations | 16 | 0 | 0 | 0 | 16 |
| Gravity / GR | 25 | 2 | 0 | 0 | 27 |
| Network & Spectrum | 42 | 0 | 0 | 0 | 42 |
| Predictions | 8 | 0 | 0 | 0 | 8 |
| ψ / Tensor Sector | 7 | 0 | 0 | 0 | 7 |
| Quantum Mechanics | 11 | 0 | 0 | 0 | 11 |
| Standard Model | 49 | 9 | 0 | 1 | 59 |
| TRM Dynamics | 15 | 0 | 0 | 0 | 15 |

---

## 2. Contradictions Matrix

| # | Topic | Phase A | Phase B | Status | Resolution |
|---|---|---|---|---|---|
| C1 | Lensing present vs absent | G4-O0: lensing ∝ ΔΦ KNOWN GR-LIKE | QG21/QG26: deflection = 0 (PPN γ=−1) | RESOLVED | QG21 corrects G4-O0: 'lensing' was a potential difference, not a deflection angle |
| C2 | Newton constant: magnitude free vs derived | QG6: GM_eff magnitude free, BDG−2 imported | QG181: G = 1/M_Pl² absolute from D96 | RESOLVED | QG182 bridges them: m₀=occ₀/Σm, r₀=ln span → GM_eff = 1/ln(M_Pl/v) (0.097%) |
| C3 | Hawking T: 0 d.o.f. needed vs NO MATCH | QG24: Hawking T costs 0 additional d.o.f. | QG13: NO MATCH (T∝R from E∝R^d) | RESOLVED | QG184: the M ∝ R mass-radius relation follows from the per-octave/log deficit (field a ∝ −1/r → GM_eff ∝ R), so T ∝ 1/R (Hawking) with S ∝ R^(d−1); QG13's E ∝ R^d was the compact-void assumption |
| C4 | Perihelion: tensor vs scalar | QG103: 'tensor observable' via spin-2 ψ | QG43: perihelion/γ is SCALAR; only GW pol spin-2 | PARTIALLY RESOLVED | Different questions: which sector is needed (tensor) vs which observable is spin-2 (only GW); scalar ψ also restores γ=+1 |
| C5 | No-lensing: fundamental vs artifact | QG21/QG26: no lensing is a definitive prediction | QG22: no lensing is a conformal-flatness artifact (ψ=0) | RESOLVED | QG22 supersedes: real within ψ=0, but ψ=0 is an assumption; prediction stands only in conformal sector |
| C6 | Hawking T: partial artifact vs undecided | QG22: partly conformal-flatness artifact | QG25/QG43: hawking-temperature UNDECIDED / horizon AMBIGUOUS | RESOLVED | Consistent: QG22 mechanism + QG25/43 epistemic status |
| C7 | Sector exponent law: derived vs overfit | QG147: sector exponent law (EXPONENT ORIGIN) | QG148: the law is OVERFIT | RESOLVED | QG148 validates the fit; QG149 supersedes with physical occupation-weighted origin |

---

## 3. Open Questions

| Question | Phase | Status |
|---|---|---|
| Exact neutrino mass values m1,m2,m3 (splittings derived QG172; m1=0 normal ordering) | QG172 | OPEN |
| Quark absolute mass running-scale/MS̄ conversion of the D96 mass law | QG173 | OPEN |
| Experimental validation of the 106 GeV resonance (primary falsifiable prediction; QG188A audit: INCONCLUSIVE — 95 GeV excess at 91.19 rung, 106 GeV window neither confirmed nor excluded) | QG132/QG188A | FALSIFIABLE-PENDING |
| Collider test of sector-ladder physics (energy-ladder rung states) | QG130 | PREDICTED-NO-DATA |
| Exact origin of the Bekenstein 1/4 coefficient (structure S∝A, M∝R, T∝1/R fully derived QG12/QG184; deficit first-law gives 1/2 = A/(8π); exact 1/4 needs the 2π quantum factor T = κ/(2π), not in D96/TRM; 1/occ₀=1/4 is numerological) | QG12/QG13/QG184/QG185 | PARTIALLY-OPEN |
| ψ/Weyl field: new fundamental primitive (capacity FORCED by link completeness QG56; excitation mechanism DERIVED QG57; existence observationally required QG47) — PARTIALLY SOLVED, see TQMQG_PsiOriginAudit.md | QG23/24/47/52/54/56/57 | PARTIALLY-SOLVED |
| Matter = deficit hypothesis: m = ρ̄−ρ is a hypothesis, not derived | G4-ME | OPEN |
| Metric ansatz g = ρ^(2/d)η is PREFERRED but not UNIQUE; flat η is a defining axiom | G4-A0 | OPEN-AXIOM |
| No independent matter sector: G=κT is an identity (Lovelock); kinetic T not conserved | G4-G3 | OPEN |
| Hawking temperature after ψ: no phase derives T ∝ 1/R explicitly with ψ≠0 | QG24 | OPEN |
| Flat rotation-curve α=0: SEMI-NATURAL, symmetry assumption not derived | G4-ME4 | OPEN |
| 2D native program: Einstein tensor ≡ 0 in d=2; 2D→3D bridge not in one report | G4-G0 | OPEN-BRIDGE |

---

## 4. Predictions

| Prediction | Phase | Status |
|---|---|---|
| 106 GeV resonance (scalar sector transition) | QG132 | FALSIFIABLE — not yet observed; QG188A audit INCONCLUSIVE; QG190 PRE-REGISTERED window 99–114 GeV |
| Sector-ladder collider signatures (energy-ladder rung states) | QG130 | PREDICTED — no data |
| 0νββ rate: m_ββ = 2.02e-3 eV (Majorana neutrino) | QG179 | PREDICTED — awaiting experiment |
| Gravitational redshift WITHOUT lensing in conformal (ψ=0) sector | QG21 | FALSIFIABLE — differs from GR |
| Curvature-sourced Poisson equation (source = ρ″, not density value) | G4-O0 | TQM-SPECIFIC — testable in principle |

---

## 5. GR / Relativity Topic Coverage

| Topic | Phase | Status | Detail |
|---|---|---|---|
| Gravitational redshift | QG21/G4-O0 | tested | Δν/ν = −ΔΦ; g₀₀ varies → redshift YES; redshift WITHOUT lensing in conformal sector |
| Time dilation (gravitational) | QG187 | tested | IS the QG21 redshift law (clock ∝ ρ^(1/d) = √(−g_00)); +45.7 vs GR 45.9 μs/day |
| GPS correction | QG187 | tested | GPS ORIGIN: net +38.5 vs observed +38.6 μs/day (−0.2%); −4.465e-10 rate offset |
| Shapiro delay | QG26 | tested | = 0 in conformal (PPN γ=−1); would need ψ≠0 (QG22) |
| Light bending | QG26/G4-O0 | tested | QG26: NO MATCH (δ=0); G4-O0: weak-field lensing ∝ ΔΦ (potential diff, corrected by QG21) |
| Mercury perihelion | QG103 | tested | +42.98″/century via ψ (γ=β=+1); ρ-only retrograde |
| Frame dragging / Lense-Thirring | QG186 | tested | FRAME-DRAGGING ORIGIN: h_0i sector via ψ (ρ-only has h_0i=0); GP-B 41.1 vs 39.2 mas/yr, LAGEOS 30.7 vs ~31 |
| Black holes | QG12 | tested | S ∝ Area (conditional); S∝M² mass-radius gap resolved QG184; exact 1/4 PARTIALLY OPEN (QG185) |
| Hawking radiation | QG13/QG22 | tested | T ∝ R native (NO MATCH for 1/R); partly conformal-flatness artifact; mass-radius gap resolved QG184 (T ∝ 1/R restored); exact coefficient open (QG185) |
| Newton constant | QG6/QG181/QG182 | tested | QG6 native scale; QG181 M_Pl = v·A³ (0.2%); QG182 bridges both (0.097%) |
| Einstein equations | G4-G0/G2/G3 | tested | G_μν from ρ (exact); G=κT as identity (no independent matter) |

---

## 6. Observable Register

SM observables with current validation status. Supersedes the QG170 audit list by
incorporating QG171-182 results.

| Observable | Status | Phase | Detail |
|---|---|---|---|
| electron mass | tested | QG140 | 0.511 MeV, dev 0.2% |
| muon mass | tested | QG140 | 105.66 MeV, dev ~0% |
| tau mass | tested | QG140 | 1776.86 MeV, dev 2.9% |
| CKM |Vus| | tested | QG165 | dev 1.9% |
| CKM |Vcb| | tested | QG165 | dev 1.2% |
| CKM |Vub| | tested | QG165 | dev 0.1% |
| CKM δ_CP | tested | QG166 | 66.3°, dev 1.2% |
| Jarlskog J | tested | QG166 | dev 1.3% |
| PMNS θ12/θ23/θ13/δ_ν | tested | QG167 | dev 0.1-3% |
| 1/α_em | tested | QG162 | = Σm+#doublets = 137 |
| sin²θ_W | tested | QG162 | 0.2316 |
| MW | tested | QG168 | 80.1 GeV (phys 80.38, dev 0.3%) |
| MZ | tested | QG168 | 91.4 GeV (phys 91.19, dev 0.2%) |
| ρ parameter | tested | QG168 | 1.00000 (exact SM tree-level) |
| MH | tested | QG169 | 125.25 GeV (dev 0.003%) |
| muon g-2 a_μ | tested | QG171 | (α/2π)(1+λ₂/Σm); anomaly (α/2π)³·span^¼ |
| Δm²21 | tested | QG172 | (1/Σ√m)²/(span/2) |
| Δm²31 | tested | QG172 | sin²θ_W/Σm |
| quark masses (6) | tested | QG173 | all within 0.2% |
| θ_QCD (strong CP) | tested | QG174 | = 0 via [L,P]=0 reflection |
| sin²θ_eff | tested | QG175 | #g/(2Σm) |
| ΓZ | tested | QG175 | MH·cosθ_W/#g |
| ΓW | tested | QG175 | σ_occ²/(occMom·λ₂) |
| ΓH | tested | QG175 | λ₂/Σm |
| R_b | tested | QG175 | span·g₂·sin⁴θ_W |
| A_FB | tested | QG175 | (λ_H/λ₂)² and MH/(MW·MZ) |
| electron g-2 a_e | tested | QG178 | 1.159655e-3, dev 0.0003% |
| Majorana character | tested | QG179 | m_ββ = 2.02e-3 eV |
| oblique S,T,U | tested | QG180 | S 5.3%, T 5.3%, U=0; T=2S exact |
| Newton constant G | tested | QG181 | 6.6476e-11, dev 0.4% |
| lepton hierarchy | partial | QG142 | PARTIAL LAW |
| quark hierarchy law | partial | QG146 | PARTIAL LAW |
| family index origin | partial | QG135 | PARTIAL ORIGIN |
| golden-ratio hierarchy | partial | QG152 | PARTIAL ROBUSTNESS |
| physical calibration ladder | partial | QG129 | PARTIAL MAPPING |
| exact neutrino masses m1,m2,m3 | untested | — | splittings derived (QG172); absolute values open |
| quark running-scale/MS̄ conversion | untested | — | D96 mass law at MS̄ scale open |
| mass ordering (ν) | partial | QG179 | m1=0 normal ordering derived; experiment pending |
| 106 GeV resonance | untested | QG132/QG188A/QG190 | falsifiable prediction, not yet observed; INCONCLUSIVE evidence audit (95 GeV excess at 91.19 rung); PRE-REGISTERED window 99–114 GeV, central 106.39 GeV (QG190) |
| collider sector-ladder signatures | untested | QG130 | predicted, no data |

---

## 7. Phase Register

All completed QG phases with classification, validation status, and key result.
Historical entries are preserved; updates are additive.

### Gravity / GR

- **QG0** — GRAVITY BRIDGE (tested) — Q-events → ρ → metric → gravity (base chain) `TQMQG_ActualizationToGravity.md`

### Foundations

- **QG1** — RHO ORIGIN (tested) — ρ as microscopic actualization density (primitive) `TQMQG_MicroscopicOriginOfRho.md`
- **QG2** — DIMENSION ORIGIN (tested) — dimension from network structure `TQMQG_OriginOfDimension.md`
- **QG3** — SELECTED (tested) — dimension selected by stability `TQMQG_DimensionSelection.md`
- **QG4** — EFFECTIVE DIMENSION (tested) — effective dimension of actualization `TQMQG_EffectiveDimension.md`
- **QG5** — OBSERVABLE DIMENSION (tested) — observable dimension consistent with 3+1 `TQMQG_ObservableDimension.md`

### Gravity / GR

- **QG6** — DERIVED (scale) / IMPORTED (BDG−2) (partial) — GM_eff = m₀r₀/(d·ρ̄) native, no free coupling; BDG −2 normalization imported `TQMQG_OriginOfG.md`

### Foundations

- **QG7** — CRITICAL BRANCHING (tested) — critical branching of actualization dynamics `TQMQG_CriticalBranching.md`
- **QG8** — LANDSCAPE (tested) — dimension landscape over parameters `TQMQG_DimensionLandscape.md`
- **QG9** — RANK SELECTED (tested) — support-rank selection of states `TQMQG_SupportRankSelection.md`
- **QG10** — INFORMATION DIMENSION (tested) — information-theoretic dimension `TQMQG_InformationDimension.md`
- **QG11** — CAUSAL ORDER ORIGIN (tested) — causal order from Q-events (primitive) `TQMQG_OriginOfCausalOrder.md`

### Gravity / GR

- **QG12** — MATCH (conditional) (tested) — S ∝ Area from horizon counting; no 1/4, no S∝M² (mass-radius gap) `TQMQG_BlackHoleEntropy.md`
- **QG13** — NO MATCH (tested) — T ∝ R (deficit E∝R^d), not Hawking T ∝ 1/R `TQMQG_HorizonThermodynamics.md`
- **QG14** — PLANCK REGIME (tested) — natural minimum length/maximum density `TQMQG_PlanckRegime.md`
- **QG15** — FLUCTUATIONS (tested) — Poisson event-count fluctuations → metric/curvature fluctuations `TQMQG_SpacetimeFluctuations.md`
- **QG16** — TENSOR SECTOR (tested) — tensor sector exists but unsourced by scalar actualization `TQMQG_TensorSector.md`
- **QG17** — FROZEN TENSOR (tested) — tensor sector frozen (ψ=0) `TQMQG_UnfreezeTensorSector.md`
- **QG18** — PARTIAL MATCH (tested) — scalar GW: energy/speed OK, polarization NO MATCH `TQMQG_GravitationalWaves.md`
- **QG19** — NEW PRIMITIVE (tested) — GW requires tensor/ψ primitive (spin-2); emergent impossible `TQMQG_GWReconciliation.md`
- **QG20** — TEMPORAL WAVE (tested) — temporal wave observables `TQMQG_TemporalWaveObservables.md`
- **QG21** — NULL-GEODESIC (tested) — redshift YES, lensing NO (conformally flat; falsifiable) `TQMQG_LightPropagation.md`
- **QG22** — CONFORMAL-FLATNESS ARTIFACT (tested) — no-lensing/no-GW are ψ=0 artifacts, not fundamental `TQMQG_ConformalFlatnessAudit.md`
- **QG23** — PSI ORIGIN (absent) (tested) — ψ cannot emerge from scalar actualization `TQMQG_OriginOfPsi.md`
- **QG24** — MINIMAL NEW PRIMITIVE (tested) — ψ spin-2 (2 d.o.f.) is the minimal completion `TQMQG_MinimalTensorExtension.md`
- **QG25** — OBSERVABLE AMBIGUITY (4) / TENSOR REQUIRED (1) / UNDECIDED (1) (tested) — only GW-strain requires tensor; Hawking T undecided `TQMQG_ObservableReconstructionAudit.md`
- **QG26** — NO MATCH (tested) — PPN γ=−1 → no lensing, no Shapiro delay; redshift survives `TQMQG_NonTensorLensing.md`

### TRM Dynamics

- **QG27** — BRIDGE (tested) — TRM observables (lensing, delay) bridge `TQMQG_TRMObservableBridge.md`
- **QG28** — PROPAGATION LAW (tested) — propagation law from TRM dynamics `TQMQG_PropagationLaw.md`

### Foundations

- **QG29** — Q-EVENT MEANING (tested) — physical meaning of Q-events `TQMQG_PhysicalMeaningOfQEvents.md`

### TRM Dynamics

- **QG30** — CORRELATIONS (tested) — Q-event correlations (Shapiro-delay stats → 0) `TQMQG_QEventCorrelations.md`
- **QG31** — PROPAGATOR ORIGIN (tested) — TRM propagator origin `TQMQG_TRMPropagatorOrigin.md`
- **QG32** — COMPATIBLE (tested) — TRM compatible with scalar results; ψ adds tensor terms `TQMQG_TRMCompatibilityAudit.md`
- **QG33** — UV COMPLETION (tested) — TRM as UV completion `TQMQG_TRMasUVCompletion.md`
- **QG34** — IRREDUCIBLE (tested) — irreducible TRM ingredient `TQMQG_IrreducibleTRMIngredient.md`
- **QG35** — PSI VS CORE (tested) — ψ vs regular core distinction `TQMQG_PsiVsRegularCore.md`
- **QG36** — PROFILE ORIGIN (tested) — TRM profile origin `TQMQG_TRMProfileOrigin.md`
- **QG37** — SATURATION→PSI (tested) — saturation maps to ψ `TQMQG_SaturationToPsi.md`
- **QG38** — SATURATION ORIGIN (tested) — saturation origin `TQMQG_SaturationOrigin.md`
- **QG39** — SECTOR AUDIT (tested) — TRM sector audit (lensing/γ scalar) `TQMQG_TRMSectorAudit.md`
- **QG40** — BOUNDARY (tested) — final boundary audit `TQMQG_FinalBoundaryAudit.md`
- **QG41** — ACCELERATION ORIGIN (tested) — TRM acceleration origin `TQMQG_TRMAccelerationOrigin.md`
- **QG42** — FINAL TRM (tested) — final TRM audit `TQMQG_FinalTRMAudit.md`

### Gravity / GR

- **QG43** — PSI UNIQUE ONLY FOR GW POLARIZATION (tested) — lensing/Shapiro/γ scalar (1 d.o.f.); only GW pol needs spin-2 `TQMQG_ObservationalUniqueness.md`

### ψ / Tensor Sector

- **QG44** — MINIMAL PSI (tested) — minimal ψ equation `TQMQG_MinimalPsiEquation.md`
- **QG45** — MINIMAL COUPLING (tested) — minimal ψ coupling `TQMQG_MinimalPsiCoupling.md`
- **QG46** — SPIN-2 ORIGIN (tested) — why ψ is spin-2 `TQMQG_WhySpin2.md`
- **QG47** — WHY PSI (tested) — why ψ exists (lensing/γ/delay need 1 d.o.f.) `TQMQG_WhyPsiExists.md`

### Gravity / GR

- **QG48** — OBSERVATION AUDIT (tested) — GW observation audit: what is observed vs inferred `TQMQG_GWObservationAudit.md`

### Network & Spectrum

- **QG49** — NETWORK MODE (tested) — network-mode GW `TQMQG_NetworkModeGW.md`

### ψ / Tensor Sector

- **QG50** — TWO SECTOR (tested) — two-sector necessity (scalar+tensor) `TQMQG_TwoSectorNecessity.md`

### Foundations

- **QG51** — TWO PRIMITIVES (tested) — origin of two primitives `TQMQG_OriginOfTwoPrimitives.md`

### ψ / Tensor Sector

- **QG52** — FUNDAMENTAL VS EFFECTIVE (tested) — ψ fundamental vs effective `TQMQG_FundamentalVsEffectivePsi.md`

### Foundations

- **QG53** — DEPENDENCY (tested) — dependency audit of derivations `TQMQG_DependencyAudit.md`

### ψ / Tensor Sector

- **QG54** — PSI AS CONNECTIVITY (tested) — ψ as connectivity `TQMQG_PsiAsConnectivity.md`

### Network & Spectrum

- **QG55** — PRIMITIVE AUDIT (tested) — network primitive audit `TQMQG_NetworkPrimitiveAudit.md`
- **QG56** — WEYL LINK ORIGIN (tested) — origin of Weyl links `TQMQG_OriginOfWeylLinks.md`
- **QG57** — WEYL EXCITATION (tested) — Weyl excitation `TQMQG_WeylExcitation.md`
- **QG58** — DISCRETE LINKS (tested) — discrete vs continuous links `TQMQG_DiscreteOrContinuousLinks.md`
- **QG59** — REVALIDATED (tested) — unified network revalidation `TQMQG_UnifiedNetworkRevalidation.md`

### Standard Model

- **QG60** — COMPATIBLE (tested) — network compatible with SM structure `TQMQG_StandardModelCompatibility.md`

### Quantum Mechanics

- **QG61** — COMPATIBLE (tested) — network compatible with QM `TQMQG_QuantumMechanicsCompatibility.md`
- **QG62** — AMPLITUDE ORIGIN (tested) — quantum amplitudes from actualization `TQMQG_OriginOfQuantumAmplitudes.md`
- **QG63** — PHASE LOCATION (tested) — phase location `TQMQG_PhaseLocation.md`

### Network & Spectrum

- **QG64** — LINK UNIFICATION (tested) — link unification `TQMQG_LinkUnification.md`

### Quantum Mechanics

- **QG65** — INTERFERENCE (tested) — interference from links `TQMQG_InterferenceFromLinks.md`
- **QG66** — SPIN-1/2 ORIGIN (tested) — spin-1/2 origin `TQMQG_OriginOfSpinHalf.md`
- **QG67** — SPIN STRUCTURE (tested) — network spin structure `TQMQG_NetworkSpinStructure.md`

### Network & Spectrum

- **QG68** — FINAL PRIMITIVE (tested) — final network primitive `TQMQG_FinalNetworkPrimitive.md`

### Predictions

- **QG69** — PREDICTION (tested) — first network prediction `TQMQG_FirstPrediction.md`

### Quantum Mechanics

- **QG70** — ENTANGLEMENT ORIGIN (tested) — entanglement from links `TQMQG_EntanglementFromLinks.md`
- **QG71** — ENTANGLING SECTOR (tested) — entangling sector `TQMQG_EntanglingSector.md`
- **QG72** — QUANTUM AUDIT (tested) — quantum sector audit `TQMQG_QuantumSectorAudit.md`
- **QG73** — MEASUREMENT ORIGIN (tested) — measurement from actualization `TQMQG_MeasurementFromActualization.md`
- **QG74** — GENERAL MEASUREMENT (tested) — arbitrary bases via actualization (Born rule) `TQMQG_GeneralMeasurement.md`

### Predictions

- **QG75** — QUANTITATIVE PREDICTION (tested) — first quantitative prediction `TQMQG_FirstQuantitativePrediction.md`

### Foundations

- **QG76** — COMPLETENESS (tested) — completeness audit `TQMQG_CompletenessAudit.md`

### Cosmology

- **QG77** — COSMOLOGY DERIVED (tested) — expansion = redshift + scale-free ρ; H primitive `TQMQG_CosmologyAudit.md`

### Standard Model

- **QG78** — COLOR ORIGIN (tested) — color from network `TQMQG_ColorOrigin.md`
- **QG79** — SU(3) ORIGIN (tested) — why SU(3) `TQMQG_WhySU3.md`
- **QG80** — 3 GENERATIONS (tested) — why three generations `TQMQG_WhyThreeGenerations.md`
- **QG81** — FAMILY REPLICATION (tested) — family replication `TQMQG_FamilyReplication.md`
- **QG82** — FLAVOR MIXING (tested) — flavor mixing `TQMQG_FlavorMixing.md`

### Network & Spectrum

- **QG83** — VALENCE 3 (tested) — network valence three `TQMQG_NetworkValenceThree.md`

### Standard Model

- **QG84** — HIGGS ORIGIN (tested) — Higgs = collective occupation-density scalar `TQMQG_HiggsOrigin.md`
- **QG85** — SM PARAMETERS (partial) — SM parameters surveyed `TQMQG_SMParameters.md`
- **QG86** — PARAMETER ORIGIN AUDIT (tested) — parameter origin audit `TQMQG_ParameterOriginAudit.md`

### Network & Spectrum

- **QG87** — FACES & VOLUMES (tested) — faces and volumes `TQMQG_FacesAndVolumes.md`

### Foundations

- **QG88** — VALUE SELECTION (tested) — parameter value selection `TQMQG_ParameterValueSelection.md`
- **QG89** — ENERGY ORIGIN (tested) — origin of energy `TQMQG_OriginOfEnergy.md`

### Standard Model

- **QG90** — GAUGE SPLITTING (tested) — gauge sector splitting `TQMQG_GaugeSectorSplitting.md`

### Network & Spectrum

- **QG91** — LINK LENGTH (tested) — link-length physics `TQMQG_LinkLengthPhysics.md`
- **QG92** — CONSISTENCY (tested) — network consistency parameters `TQMQG_NetworkConsistencyParameters.md`
- **QG93** — GLOBAL CONSISTENCY (tested) — global consistency `TQMQG_GlobalConsistency.md`
- **QG94** — PARAMETER EIGENVALUES (tested) — parameter eigenvalues `TQMQG_ParameterEigenvalues.md`
- **QG95** — RESONANCE (tested) — network resonance parameters `TQMQG_NetworkResonanceParameters.md`
- **QG96** — STABLE STATE (tested) — stable state selection `TQMQG_StableStateSelection.md`
- **QG97** — LINK RATIO (tested) — link ratio parameters `TQMQG_LinkRatioParameters.md`
- **QG98** — NETWORK ANGLES (tested) — network angles `TQMQG_NetworkAngles.md`
- **QG99** — MOTIFS (tested) — network motifs `TQMQG_NetworkMotifs.md`
- **QG100** — CURVATURE PARAMETERS (tested) — curvature parameters `TQMQG_CurvatureParameters.md`
- **QG101** — DYNAMIC PARAMETER (tested) — dynamic parameter origin `TQMQG_DynamicParameterOrigin.md`
- **QG102** — SOLUTION SPACE (tested) — global solution space `TQMQG_GlobalSolutionSpace.md`

### Gravity / GR

- **QG103** — MATCH (via ψ) (tested) — perihelion +42.98″/century via ψ (γ=β=+1); ρ-only retrograde `TQMQG_MercuryRevalidation.md`

### Network & Spectrum

- **QG104** — HIERARCHICAL SPECTRUM (tested) — 91-event causal network → hierarchical discrete spectrum `TQMQG_NetworkSpectrum.md`
- **QG105** — ROBUST (tested) — spectral ratios stable under size/topology changes `TQMQG_SpectrumRobustness.md`
- **QG106** — MULTIPLE CLASSES (tested) — distinct spectral classes ↔ stable network states `TQMQG_SpectralClasses.md`
- **QG107** — ROBUST (tested) — family structure robustness `TQMQG_FamilyStructureRobustness.md`
- **QG108** — STATISTICS (tested) — family count statistics `TQMQG_FamilyCountStatistics.md`
- **QG109** — SELECTED (tested) — physical network selection `TQMQG_PhysicalNetworkSelection.md`
- **QG110** — INFORMATION SELECTION (tested) — network information selection `TQMQG_NetworkInformationSelection.md`
- **QG111** — MULTI-OBJECTIVE (tested) — multi-objective selection `TQMQG_MultiObjectiveSelection.md`
- **QG112** — NETWORK SECTORS (tested) — network sectors `TQMQG_NetworkSectors.md`
- **QG113** — SECTOR BOUNDARY (tested) — sector boundary physics `TQMQG_SectorBoundaryPhysics.md`
- **QG114** — 3D CONNECTIVITY (tested) — 3D connectivity classes `TQMQG_3DConnectivityClasses.md`
- **QG115** — STRUCTURE FROM CONTENT (tested) — structure from content `TQMQG_StructureFromContent.md`
- **QG116** — ACTUALIZATION STRUCTURES (tested) — actualization structures `TQMQG_ActualizationStructures.md`
- **QG116.5** — UNIVERSAL ATTRACTOR (tested) — universal attractor (N·K circulant) `TQMQG_UniversalAttractor.md`
- **QG117** — ATTRACTOR ORIGIN (tested) — parameter plane → discrete attractor ladder `TQMQG_AttractorParameterOrigin.md`

### Standard Model

- **QG118** — FAMILIES FROM ATTRACTORS (tested) — families from attractor geometry `TQMQG_FamiliesFromAttractors.md`

### Network & Spectrum

- **QG119** — LOCAL VS GLOBAL (tested) — local vs global attractors `TQMQG_LocalVsGlobalAttractors.md`
- **QG120** — HORIZON FAMILIES (tested) — finite horizon suppresses higher families `TQMQG_HorizonFamilies.md`
- **QG121** — LADDER ORIGIN (tested) — discrete radius ladder from fixed-point bifurcations `TQMQG_AttractorLadder.md`
- **QG122** — ENERGY-DEPENDENT (tested) — energy-dependent attractors `TQMQG_EnergyDependentAttractors.md`
- **QG123** — ENERGY-GEOMETRY HIERARCHY (tested) — energy-geometry hierarchy `TQMQG_EnergyGeometryHierarchy.md`

### Standard Model

- **QG124** — SM FROM ENERGY SECTORS (tested) — SM from energy sectors `TQMQG_SMFromEnergySectors.md`

### High-Energy Sector

- **QG125** — METASTABLE (tested) — high-energy sector metastable `TQMQG_HighEnergySectorStability.md`
- **QG126** — SECTOR-PARTICLE MAPPING (tested) — energy-sector ↔ particle mapping `TQMQG_ParticleSectorMapping.md`
- **QG127** — OBSERVABLE SIGNATURE (tested) — high-energy sector signatures `TQMQG_HighEnergySectorSignatures.md`
- **QG128** — PREDICTIVE SPECTRUM (tested) — sector-transition discrete spectrum (8 thresholds, 12-rung ladder) `TQMQG_SectorTransitionSpectrum.md`
- **QG129** — PARTIAL MAPPING (partial) — ladder ratios vs SM mass ratios — partial mapping `TQMQG_PhysicalCalibration.md`
- **QG130** — ACCESSIBLE (tested) — sector ladder collider-accessible `TQMQG_ColliderSectorPredictions.md`
- **QG131** — CONSISTENT SIGNATURE (tested) — collider data consistent `TQMQG_ColliderDataAudit.md`

### Predictions

- **QG132** — FALSIFIABLE PREDICTION (tested) — 106 GeV resonance (not yet observed) `TQMQG_FirstFalsifiablePrediction.md`
- **QG133** — MODERATE (tested) — prediction robustness moderate `TQMQG_PredictionRobustness.md`

### Standard Model

- **QG134** — FUNDAMENTAL SPLIT (tested) — boson-fermion split fundamental `TQMQG_BosonFermionSplit.md`
- **QG135** — PARTIAL ORIGIN (partial) — family index partial origin `TQMQG_FamilyIndexOrigin.md`
- **QG136** — PARTIAL ROBUSTNESS (partial) — three-family robustness partial `TQMQG_ThreeFamilyRobustness.md`
- **QG137** — EFFECTIVE-SIZE ORIGIN (tested) — effective-size families origin `TQMQG_EffectiveSizeFamilies.md`
- **QG138** — FUNDAMENTAL (tested) — familyCount = floor(log2(ωmax/ωmin)) + 1 (fundamental) `TQMQG_EffectiveSizeLaw.md`
- **QG139** — PARTIAL RELATION (partial) — mass hierarchy from octave structure `TQMQG_MassHierarchyFromOctaves.md`
- **QG140** — HIERARCHY ORIGIN (tested) — mass-hierarchy amplification; me/mu/tau ratios (0.2-2.9%) `TQMQG_HierarchyAmplification.md`
- **QG141** — DERIVED EXPONENTS (tested) — hierarchy exponents from spectral density `TQMQG_HierarchyExponentOrigin.md`
- **QG142** — PARTIAL LAW (partial) — unified mass law `TQMQG_UnifiedMassLaw.md`
- **QG143** — PARTIAL FACTOR (partial) — quark amplification factor `TQMQG_QuarkAmplification.md`
- **QG144** — PARTIAL EFFECT (partial) — weak-isospin amplification `TQMQG_WeakIsospinAmplification.md`
- **QG145** — UP-SECTOR ORIGIN (tested) — up-sector enhancement origin `TQMQG_UpSectorEnhancement.md`
- **QG146** — PARTIAL LAW (partial) — quark hierarchy law `TQMQG_QuarkHierarchyLaw.md`
- **QG147** — EXPONENT ORIGIN (tested) — sector exponent law (historical, superseded by QG149) `TQMQG_SectorExponentLaw.md`
- **QG148** — OVERFIT (tested) — QG147 law is OVERFIT — superseded by QG149 `TQMQG_ExponentLawValidation.md`
- **QG149** — PHYSICAL ORIGIN (tested) — sector exponents from occupation-weighted mode access (supersedes QG147) `TQMQG_PhysicalSectorExponentOrigin.md`
- **QG150** — MODE-ACCESS ORIGIN (tested) — sector exponents from octave-band occupancy-weighted access `TQMQG_ModeAccessOrigin.md`
- **QG151** — ISOSPIN ACCESS ORIGIN (tested) — isospin-dependent mode access `TQMQG_IsospinModeAccess.md`
- **QG152** — PARTIAL ROBUSTNESS (partial) — golden ratio in hierarchy — partial `TQMQG_GoldenRatioAudit.md`
- **QG153** — DOUBLET ORIGIN (tested) — Z2 doublet origin (spectrum multiplicities 2) `TQMQG_DoubletOrigin.md`
- **QG154** — NEUTRINO ORIGIN (tested) — neutrino = unique Q=0 fermion sector (neutral-charge limit) `TQMQG_NeutrinoOrigin.md`
- **QG155** — SYMMETRY ORIGIN (tested) — weak-isospin Z2 from D96 symmetry `TQMQG_Z2SymmetryOrigin.md`
- **QG156** — UNIFIED ACCESS LAW (tested) — δ = log(N_eff)/log(span) unified law `TQMQG_UnifiedSpectralAccess.md`
- **QG157** — N_EFF ORIGIN (tested) — N_eff from D96 moments (Σ√m, Σm, Σm², Σocc²/occ₀) `TQMQG_EffectiveAccessCounts.md`
- **QG158** — INEVITABLE (tested) — moment orders from Z2 powers `TQMQG_MomentOrderOrigin.md`
- **QG159** — INEVITABLE (tested) — D96 = Z2 automorphism + 3-family window + unique octave rung `TQMQG_D96SelectionOrigin.md`
- **QG160** — INEVITABLE (tested) — period-3 seed = unique complete-Z2 natural size (96) `TQMQG_Period3SeedOrigin.md`
- **QG161** — GAUGE ORIGIN (tested) — 1+3+8 = degree-12 of C_96(1..6) `TQMQG_GaugeSectorOrigin.md`
- **QG162** — COUPLING ORIGIN (tested) — 1/α_em = 137, α_weak = 3/Σm, α_s = 8/Σ√m, sin²θ_W = 0.2316 `TQMQG_GaugeCouplingOrigin.md`
- **QG163** — RUNNING ORIGIN (tested) — α_i(E) = g_i/D_i(N(E)) octave ladder; no in-sector unification `TQMQG_RunningCouplingOrigin.md`
- **QG164** — CONTINUOUS ORIGIN (tested) — continuous running limit `TQMQG_ContinuousRunningOrigin.md`
- **QG165** — CKM ORIGIN (tested) — CKM: |Vus| 1.9%, |Vcb| 1.2%, |Vub| 0.1% `TQMQG_CKMOrigin.md`
- **QG166** — CP ORIGIN (tested) — δ_CP 66.3° (1.2%), Jarlskog J (1.3%) `TQMQG_CKMCPOrigin.md`
- **QG167** — PMNS ORIGIN (tested) — T3-only access → θ12/θ23/θ13/δ_ν (0.1-3%) `TQMQG_PMNSOrigin.md`
- **QG168** — MASS ORIGIN (tested) — v = 137·ln span = 254.37 GeV; MW = 80.1, MZ = 91.4, ρ = 1 `TQMQG_WeakBosonMassOrigin.md`
- **QG169** — HIGGS ORIGIN (tested) — σ_occ·span/2 → MH = 125.25 GeV `TQMQG_HiggsMassOrigin.md`
- **QG170** — COVERAGE AUDIT (audit) — 48 quantities: 25 tested / 9 partial / 14 untested = 64% (weighted) `TQMQG_StandardModelAudit.md`
- **QG171** — G2 ORIGIN (tested) — a_μ = (α/2π)(1+λ₂/Σm); anomaly (α/2π)³·span^¼ `TQMQG_MuonG2Origin.md`
- **QG172** — MASS ORIGIN (tested) — Δm²21 = (1/Σ√m)²/(span/2), Δm²31 = sin²θ_W/Σm `TQMQG_NeutrinoMassLaw.md`
- **QG173** — MASS ORIGIN (tested) — all six quark masses from me·D96-moments (within 0.2%) `TQMQG_QuarkMassOrigin.md`
- **QG174** — STRONG CP ORIGIN (tested) — [L,P]=0 reflection → real spectrum → θ_QCD = 0 `TQMQG_StrongCPOrigin.md`
- **QG175** — PRECISION EW ORIGIN (tested) — sin²θ_eff, ΓZ, ΓW, ΓH, R_b, A_FB from D96 `TQMQG_PrecisionElectroweakOrigin.md`
- **QG176** — HIGGS RECONSTRUCTION (tested) — MH_A = 125.49 (0.19%), MH_B = 125.25 (0.003%); blind, no Higgs input `TQMQG_HiggsBlindReconstruction.md`
- **QG177** — INDEPENDENT (tested) — 12 observables leave-one-out: mean 0.58%, max 1.89%; 9 independent `TQMQG_LeaveOneOutValidation.md`
- **QG178** — G2 ORIGIN (tested) — a_e = (α/2π)(1−(occ₀/Σm)²) = 1.159655e-3 (0.0003%); same mechanism as muon `TQMQG_ElectronG2Origin.md`
- **QG179** — MAJORANA ORIGIN (tested) — neutrino Majorana: T3-only self-conjugate 48/95; m_ββ = 2.02e-3 eV `TQMQG_MajoranaOrigin.md`
- **QG180** — OBLIQUE ORIGIN (tested) — S = 0.0421 (5.3%), T = 2S = 0.0842 (5.3%), U = 0; T = 2S exact `TQMQG_ObliqueParametersOrigin.md`

### Gravity / GR

- **QG181** — GRAVITY ORIGIN (tested) — M_Pl = v·(Σm·#g·occ₂)³ = 1.22335e19 GeV (0.201%); G = 6.6476e-11 (0.400%) `TQMQG_NewtonConstantOrigin.md`
- **QG182** — BRIDGE ORIGIN (tested) — m₀=occ₀/Σm, r₀=ln span → GM_eff = 1/ln(M_Pl/v) (0.097%); QG6≡QG181 `TQMQG_GravityBridgeOrigin.md`
- **QG183** — ROBUST ORIGIN (tested) — physical exponent p = ln(M_Pl/v)/ln(A) = 2.99984 (cubic to 1e-4); only A³ reproduces M_Pl (0.2%); A¹/A²/A⁴ fail 100%/100%/3.6e7%; no alternative A selects cubic `TQMQG_PlanckScaleRobustness.md`
- **QG184** — MASS-RADIUS ORIGIN (tested) — M ∝ R from per-octave/log deficit (G4ME flat-rotation-curve profile): a ∝ −1/r → GM_eff ∝ R; QG13's E ∝ R^d was compact-void assumption; S ∝ R^(d−1) (QG12) → T ∝ 1/R Hawking restored `TQMQG_MassRadiusOrigin.md`
- **QG185** — PARTIAL ORIGIN (partial) — structure derived (S∝A QG12, M∝R QG184, T∝1/R QG184); deficit first-law gives S = A_cell/2 = A/(8π), not 1/4; exact 1/4 requires the 2π quantum factor T = κ/(2π) absent in D96/TRM (span/(2π)=1.019); 1/occ₀=1/4 is a label identity `TQMQG_BekensteinQuarterOrigin.md`
- **QG186** — FRAME-DRAGGING ORIGIN (tested) — gravitomagnetic h_0i sector is a ψ-sector observable: conformally-flat ρ-only has h_0i=0 (no frame dragging); ψ spin-2 (QG44) restores linearized Einstein incl. h_0i; rotating deficit (matter=deficit G4ME) sources J; Ω_LT=G(3(J·r̂)r̂−J)/(2c²r³) → GP-B 41.1 vs 39.2 mas/yr, LAGEOS 30.7 vs ~31; D96 G (QG181) shifts <1% `TQMQG_FrameDraggingOrigin.md`
- **QG187** — GPS ORIGIN (tested) — gravitational time dilation IS the QG21 redshift law: clock rate dτ/dt = ρ^(1/d) = √(−g_00), Δτ/τ = (ρ1/ρ2)^(1/d)−1 = redshift; weak-field (GM/c²)(1/r1−1/r2) → +45.7 μs/day vs GR 45.9 (−0.4%); + SR orbital −v²/(2c²) = −7.2 → NET +38.5 vs observed +38.6 μs/day (−0.2%) = −4.465e-10 GPS rate offset; ρ source = deficit field (G4ME) `TQMQG_GpsCorrectionOrigin.md`

### Predictions

- **QG188** — PREDICTION AUDIT (tested) — 10 remaining falsifiable predictions from coverage JSON: 2 testable NOW (106 GeV P1, sector-ladder P2), 3 SOON (0νββ P3, mass-ordering P6, neutrino masses P7), 5 inaccessible (P4,P5,P8,P9,P10); ranked by impact·3+feas·2+fals·2 → Top-1 = 106 GeV (QG132, score 35.0, LHC Run 3), Top-SOON = 0νββ m_ββ=2.02e-3 eV (QG179) `TQMQG_PredictionAudit.md`
- **QG189** — INCONCLUSIVE (tested) — published record: ~95 GeV scalar excess cluster (CMS γγ 2.9σ, ATLAS γγ 1.7σ, combined 3.1σ; CMS ττ 2.6σ; LEP bb̄ 2.3σ) aligns with 91.19 GeV rung (dev 4.5%) NOT predicted 106.39 GeV (−10.4%); CMS 70–110 & ATLAS 66–110 GeV full-Run-2 diphoton null searches cover 106 GeV (limits 15–102 fb, no excess); LEP2 SM-like < 114.4 GeV (SM-strength hZZ only); prediction NOT excluded; Run 3 no confirmed increase; HL-LHC decisive `TQMQG_106GeVResonanceAudit.md`
- **QG190** — PREDICTION AUDIT (tested) — methodology audit of QG140-188: 49 phases — 36 PREDICTION, 2 BLIND (QG176 Higgs, QG177 leave-one-out), 8 DEPENDENT, 2 RETRO-FIT (QG140/146 fitted exponents, superseded by QG141/149), 1 OVERFIT (QG147, 3 params/3 sectors, CONFIRMED by QG148 out-of-sample); 3 high-risk all in fitting era QG140-148; structural era QG149+ no fitted parameters `TQMQG_AntiFitAudit.md`
- **QG191** — PRE-REGISTERED (tested) — prediction frozen BEFORE future data (D96/QG128-132 only; forbidden: ATLAS/CMS excess, fitted masses, new constants): central mass 106.39 GeV (lowest missing Z-anchor rung, scale MZ/6=15.198), window 98.79–113.99 GeV (stated 99–114), production 9 rungs 106.4→263.4 GeV below LHC13/FCC-hh, decay unit 15.20 GeV ×10 + top 20.26 GeV ×1 → 3-family sector; CONFIRMED = signal in window with 15–20 GeV quanta, DISFAVORED = null `TQMQG_PreRegistered106GeV.md`
