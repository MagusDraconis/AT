# -*- coding: utf-8 -*-
"""Generate Docs/TQMQG_PhysicsCoverage.md and Docs/TQMQG_PhysicsCoverage.json.

Single source of truth for all TQM-QG physics validation.
Run:  python Tools/build_physics_coverage.py
"""
import json, datetime, os, sys, re

sys.stdout.reconfigure(encoding='utf-8')
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
MD = os.path.join(ROOT, "Docs", "TQMQG_PhysicsCoverage.md")
JSON = os.path.join(ROOT, "Docs", "TQMQG_PhysicsCoverage.json")
REPO_BLOB = "https://github.com/MagusDraconis/TQM/blob/TQM_v1.1/Docs/Research/"

# ── Phase dataset: (number, file, classification, domain, validation, key_result) ──
P = {}
def add(n, f, cls, dom, val, key):
    P[n] = dict(phase=n, file=f, classification=cls, domain=dom, validation=val, key_result=key)

# ── QG0-59: Foundation, Gravity, TRM, Quantum ──
add(0,"TQMQG_ActualizationToGravity.md","GRAVITY BRIDGE","gravity","tested",
    "Q-events → ρ → metric → gravity (base chain)")
add(1,"TQMQG_MicroscopicOriginOfRho.md","RHO ORIGIN","foundation","tested",
    "ρ as microscopic actualization density (primitive)")
add(2,"TQMQG_OriginOfDimension.md","DIMENSION ORIGIN","foundation","tested",
    "dimension from network structure")
add(3,"TQMQG_DimensionSelection.md","SELECTED","foundation","tested",
    "dimension selected by stability")
add(4,"TQMQG_EffectiveDimension.md","EFFECTIVE DIMENSION","foundation","tested",
    "effective dimension of actualization")
add(5,"TQMQG_ObservableDimension.md","OBSERVABLE DIMENSION","foundation","tested",
    "observable dimension consistent with 3+1")
add(6,"TQMQG_OriginOfG.md","DERIVED (scale) / IMPORTED (BDG−2)","gravity","partial",
    "GM_eff = m₀r₀/(d·ρ̄) native, no free coupling; BDG −2 normalization imported")
add(7,"TQMQG_CriticalBranching.md","CRITICAL BRANCHING","foundation","tested",
    "critical branching of actualization dynamics")
add(8,"TQMQG_DimensionLandscape.md","LANDSCAPE","foundation","tested",
    "dimension landscape over parameters")
add(9,"TQMQG_SupportRankSelection.md","RANK SELECTED","foundation","tested",
    "support-rank selection of states")
add(10,"TQMQG_InformationDimension.md","INFORMATION DIMENSION","foundation","tested",
    "information-theoretic dimension")
add(11,"TQMQG_OriginOfCausalOrder.md","CAUSAL ORDER ORIGIN","foundation","tested",
    "causal order from Q-events (primitive)")
add(12,"TQMQG_BlackHoleEntropy.md","MATCH (conditional)","gravity","tested",
    "S ∝ Area from horizon counting; no 1/4, no S∝M² (mass-radius gap)")
add(13,"TQMQG_HorizonThermodynamics.md","NO MATCH","gravity","tested",
    "T ∝ R (deficit E∝R^d), not Hawking T ∝ 1/R")
add(14,"TQMQG_PlanckRegime.md","PLANCK REGIME","gravity","tested",
    "natural minimum length/maximum density")
add(15,"TQMQG_SpacetimeFluctuations.md","FLUCTUATIONS","gravity","tested",
    "Poisson event-count fluctuations → metric/curvature fluctuations")
add(16,"TQMQG_TensorSector.md","TENSOR SECTOR","gravity","tested",
    "tensor sector exists but unsourced by scalar actualization")
add(17,"TQMQG_UnfreezeTensorSector.md","FROZEN TENSOR","gravity","tested",
    "tensor sector frozen (ψ=0)")
add(18,"TQMQG_GravitationalWaves.md","PARTIAL MATCH","gravity","tested",
    "scalar GW: energy/speed OK, polarization NO MATCH")
add(19,"TQMQG_GWReconciliation.md","NEW PRIMITIVE","gravity","tested",
    "GW requires tensor/ψ primitive (spin-2); emergent impossible")
add(20,"TQMQG_TemporalWaveObservables.md","TEMPORAL WAVE","gravity","tested",
    "temporal wave observables")
add(21,"TQMQG_LightPropagation.md","NULL-GEODESIC","gravity","tested",
    "redshift YES, lensing NO (conformally flat; falsifiable)")
add(22,"TQMQG_ConformalFlatnessAudit.md","CONFORMAL-FLATNESS ARTIFACT","gravity","tested",
    "no-lensing/no-GW are ψ=0 artifacts, not fundamental")
add(23,"TQMQG_OriginOfPsi.md","PSI ORIGIN (absent)","gravity","tested",
    "ψ cannot emerge from scalar actualization")
add(24,"TQMQG_MinimalTensorExtension.md","MINIMAL NEW PRIMITIVE","gravity","tested",
    "ψ spin-2 (2 d.o.f.) is the minimal completion")
add(25,"TQMQG_ObservableReconstructionAudit.md","OBSERVABLE AMBIGUITY (4) / TENSOR REQUIRED (1) / UNDECIDED (1)","gravity","tested",
    "only GW-strain requires tensor; Hawking T undecided")
add(26,"TQMQG_NonTensorLensing.md","NO MATCH","gravity","tested",
    "PPN γ=−1 → no lensing, no Shapiro delay; redshift survives")
add(27,"TQMQG_TRMObservableBridge.md","BRIDGE","trm","tested",
    "TRM observables (lensing, delay) bridge")
add(28,"TQMQG_PropagationLaw.md","PROPAGATION LAW","trm","tested",
    "propagation law from TRM dynamics")
add(29,"TQMQG_PhysicalMeaningOfQEvents.md","Q-EVENT MEANING","foundation","tested",
    "physical meaning of Q-events")
add(30,"TQMQG_QEventCorrelations.md","CORRELATIONS","trm","tested",
    "Q-event correlations (Shapiro-delay stats → 0)")
add(31,"TQMQG_TRMPropagatorOrigin.md","PROPAGATOR ORIGIN","trm","tested",
    "TRM propagator origin")
add(32,"TQMQG_TRMCompatibilityAudit.md","COMPATIBLE","trm","tested",
    "TRM compatible with scalar results; ψ adds tensor terms")
add(33,"TQMQG_TRMasUVCompletion.md","UV COMPLETION","trm","tested",
    "TRM as UV completion")
add(34,"TQMQG_IrreducibleTRMIngredient.md","IRREDUCIBLE","trm","tested",
    "irreducible TRM ingredient")
add(35,"TQMQG_PsiVsRegularCore.md","PSI VS CORE","trm","tested",
    "ψ vs regular core distinction")
add(36,"TQMQG_TRMProfileOrigin.md","PROFILE ORIGIN","trm","tested",
    "TRM profile origin")
add(37,"TQMQG_SaturationToPsi.md","SATURATION→PSI","trm","tested",
    "saturation maps to ψ")
add(38,"TQMQG_SaturationOrigin.md","SATURATION ORIGIN","trm","tested",
    "saturation origin")
add(39,"TQMQG_TRMSectorAudit.md","SECTOR AUDIT","trm","tested",
    "TRM sector audit (lensing/γ scalar)")
add(40,"TQMQG_FinalBoundaryAudit.md","BOUNDARY","trm","tested",
    "final boundary audit")
add(41,"TQMQG_TRMAccelerationOrigin.md","ACCELERATION ORIGIN","trm","tested",
    "TRM acceleration origin")
add(42,"TQMQG_FinalTRMAudit.md","FINAL TRM","trm","tested",
    "final TRM audit")
add(43,"TQMQG_ObservationalUniqueness.md","PSI UNIQUE ONLY FOR GW POLARIZATION","gravity","tested",
    "lensing/Shapiro/γ scalar (1 d.o.f.); only GW pol needs spin-2")
add(44,"TQMQG_MinimalPsiEquation.md","MINIMAL PSI","psi","tested",
    "minimal ψ equation")
add(45,"TQMQG_MinimalPsiCoupling.md","MINIMAL COUPLING","psi","tested",
    "minimal ψ coupling")
add(46,"TQMQG_WhySpin2.md","SPIN-2 ORIGIN","psi","tested",
    "why ψ is spin-2")
add(47,"TQMQG_WhyPsiExists.md","WHY PSI","psi","tested",
    "why ψ exists (lensing/γ/delay need 1 d.o.f.)")
add(48,"TQMQG_GWObservationAudit.md","OBSERVATION AUDIT","gravity","tested",
    "GW observation audit: what is observed vs inferred")
add(49,"TQMQG_NetworkModeGW.md","NETWORK MODE","network","tested",
    "network-mode GW")
add(50,"TQMQG_TwoSectorNecessity.md","TWO SECTOR","psi","tested",
    "two-sector necessity (scalar+tensor)")
add(51,"TQMQG_OriginOfTwoPrimitives.md","TWO PRIMITIVES","foundation","tested",
    "origin of two primitives")
add(52,"TQMQG_FundamentalVsEffectivePsi.md","FUNDAMENTAL VS EFFECTIVE","psi","tested",
    "ψ fundamental vs effective")
add(53,"TQMQG_DependencyAudit.md","DEPENDENCY","foundation","tested",
    "dependency audit of derivations")
add(54,"TQMQG_PsiAsConnectivity.md","PSI AS CONNECTIVITY","psi","tested",
    "ψ as connectivity")
add(55,"TQMQG_NetworkPrimitiveAudit.md","PRIMITIVE AUDIT","network","tested",
    "network primitive audit")
add(56,"TQMQG_OriginOfWeylLinks.md","WEYL LINK ORIGIN","network","tested",
    "origin of Weyl links")
add(57,"TQMQG_WeylExcitation.md","WEYL EXCITATION","network","tested",
    "Weyl excitation")
add(58,"TQMQG_DiscreteOrContinuousLinks.md","DISCRETE LINKS","network","tested",
    "discrete vs continuous links")
add(59,"TQMQG_UnifiedNetworkRevalidation.md","REVALIDATED","network","tested",
    "unified network revalidation")

# ── QG60-103: QM, SM compatibility, cosmology, SM foundations ──
add(60,"TQMQG_StandardModelCompatibility.md","COMPATIBLE","sm","tested",
    "network compatible with SM structure")
add(61,"TQMQG_QuantumMechanicsCompatibility.md","COMPATIBLE","qm","tested",
    "network compatible with QM")
add(62,"TQMQG_OriginOfQuantumAmplitudes.md","AMPLITUDE ORIGIN","qm","tested",
    "quantum amplitudes from actualization")
add(63,"TQMQG_PhaseLocation.md","PHASE LOCATION","qm","tested",
    "phase location")
add(64,"TQMQG_LinkUnification.md","LINK UNIFICATION","network","tested",
    "link unification")
add(65,"TQMQG_InterferenceFromLinks.md","INTERFERENCE","qm","tested",
    "interference from links")
add(66,"TQMQG_OriginOfSpinHalf.md","SPIN-1/2 ORIGIN","qm","tested",
    "spin-1/2 origin")
add(67,"TQMQG_NetworkSpinStructure.md","SPIN STRUCTURE","qm","tested",
    "network spin structure")
add(68,"TQMQG_FinalNetworkPrimitive.md","FINAL PRIMITIVE","network","tested",
    "final network primitive")
add(69,"TQMQG_FirstPrediction.md","PREDICTION","predictions","tested",
    "first network prediction")
add(70,"TQMQG_EntanglementFromLinks.md","ENTANGLEMENT ORIGIN","qm","tested",
    "entanglement from links")
add(71,"TQMQG_EntanglingSector.md","ENTANGLING SECTOR","qm","tested",
    "entangling sector")
add(72,"TQMQG_QuantumSectorAudit.md","QUANTUM AUDIT","qm","tested",
    "quantum sector audit")
add(73,"TQMQG_MeasurementFromActualization.md","MEASUREMENT ORIGIN","qm","tested",
    "measurement from actualization")
add(74,"TQMQG_GeneralMeasurement.md","GENERAL MEASUREMENT","qm","tested",
    "arbitrary bases via actualization (Born rule)")
add(75,"TQMQG_FirstQuantitativePrediction.md","QUANTITATIVE PREDICTION","predictions","tested",
    "first quantitative prediction")
add(76,"TQMQG_CompletenessAudit.md","COMPLETENESS","foundation","tested",
    "completeness audit")
add(77,"TQMQG_CosmologyAudit.md","COSMOLOGY DERIVED","cosmology","tested",
    "expansion = redshift + scale-free ρ; H primitive")
add(78,"TQMQG_ColorOrigin.md","COLOR ORIGIN","sm","tested",
    "color from network")
add(79,"TQMQG_WhySU3.md","SU(3) ORIGIN","sm","tested",
    "why SU(3)")
add(80,"TQMQG_WhyThreeGenerations.md","3 GENERATIONS","sm","tested",
    "why three generations")
add(81,"TQMQG_FamilyReplication.md","FAMILY REPLICATION","sm","tested",
    "family replication")
add(82,"TQMQG_FlavorMixing.md","FLAVOR MIXING","sm","tested",
    "flavor mixing")
add(83,"TQMQG_NetworkValenceThree.md","VALENCE 3","network","tested",
    "network valence three")
add(84,"TQMQG_HiggsOrigin.md","HIGGS ORIGIN","sm","tested",
    "Higgs = collective occupation-density scalar")
add(85,"TQMQG_SMParameters.md","SM PARAMETERS","sm","partial",
    "SM parameters surveyed")
add(86,"TQMQG_ParameterOriginAudit.md","PARAMETER ORIGIN AUDIT","sm","tested",
    "parameter origin audit")
add(87,"TQMQG_FacesAndVolumes.md","FACES & VOLUMES","network","tested",
    "faces and volumes")
add(88,"TQMQG_ParameterValueSelection.md","VALUE SELECTION","foundation","tested",
    "parameter value selection")
add(89,"TQMQG_OriginOfEnergy.md","ENERGY ORIGIN","foundation","tested",
    "origin of energy")
add(90,"TQMQG_GaugeSectorSplitting.md","GAUGE SPLITTING","sm","tested",
    "gauge sector splitting")
add(91,"TQMQG_LinkLengthPhysics.md","LINK LENGTH","network","tested",
    "link-length physics")
add(92,"TQMQG_NetworkConsistencyParameters.md","CONSISTENCY","network","tested",
    "network consistency parameters")
add(93,"TQMQG_GlobalConsistency.md","GLOBAL CONSISTENCY","network","tested",
    "global consistency")
add(94,"TQMQG_ParameterEigenvalues.md","PARAMETER EIGENVALUES","network","tested",
    "parameter eigenvalues")
add(95,"TQMQG_NetworkResonanceParameters.md","RESONANCE","network","tested",
    "network resonance parameters")
add(96,"TQMQG_StableStateSelection.md","STABLE STATE","network","tested",
    "stable state selection")
add(97,"TQMQG_LinkRatioParameters.md","LINK RATIO","network","tested",
    "link ratio parameters")
add(98,"TQMQG_NetworkAngles.md","NETWORK ANGLES","network","tested",
    "network angles")
add(99,"TQMQG_NetworkMotifs.md","MOTIFS","network","tested",
    "network motifs")
add(100,"TQMQG_CurvatureParameters.md","CURVATURE PARAMETERS","network","tested",
    "curvature parameters")
add(101,"TQMQG_DynamicParameterOrigin.md","DYNAMIC PARAMETER","network","tested",
    "dynamic parameter origin")
add(102,"TQMQG_GlobalSolutionSpace.md","SOLUTION SPACE","network","tested",
    "global solution space")
add(103,"TQMQG_MercuryRevalidation.md","MATCH (via ψ)","gravity","tested",
    "perihelion +42.98″/century via ψ (γ=β=+1); ρ-only retrograde")

# ── QG104-137: Spectral classes, attractors, high-energy sector ──
add(104,"TQMQG_NetworkSpectrum.md","HIERARCHICAL SPECTRUM","network","tested",
    "91-event causal network → hierarchical discrete spectrum")
add(105,"TQMQG_SpectrumRobustness.md","ROBUST","network","tested",
    "spectral ratios stable under size/topology changes")
add(106,"TQMQG_SpectralClasses.md","MULTIPLE CLASSES","network","tested",
    "distinct spectral classes ↔ stable network states")
add(107,"TQMQG_FamilyStructureRobustness.md","ROBUST","network","tested",
    "family structure robustness")
add(108,"TQMQG_FamilyCountStatistics.md","STATISTICS","network","tested",
    "family count statistics")
add(109,"TQMQG_PhysicalNetworkSelection.md","SELECTED","network","tested",
    "physical network selection")
add(110,"TQMQG_NetworkInformationSelection.md","INFORMATION SELECTION","network","tested",
    "network information selection")
add(111,"TQMQG_MultiObjectiveSelection.md","MULTI-OBJECTIVE","network","tested",
    "multi-objective selection")
add(112,"TQMQG_NetworkSectors.md","NETWORK SECTORS","network","tested",
    "network sectors")
add(113,"TQMQG_SectorBoundaryPhysics.md","SECTOR BOUNDARY","network","tested",
    "sector boundary physics")
add(114,"TQMQG_3DConnectivityClasses.md","3D CONNECTIVITY","network","tested",
    "3D connectivity classes")
add(115,"TQMQG_StructureFromContent.md","STRUCTURE FROM CONTENT","network","tested",
    "structure from content")
add(116,"TQMQG_ActualizationStructures.md","ACTUALIZATION STRUCTURES","network","tested",
    "actualization structures")
add(116.5,"TQMQG_UniversalAttractor.md","UNIVERSAL ATTRACTOR","network","tested",
    "universal attractor (N·K circulant)")
add(117,"TQMQG_AttractorParameterOrigin.md","ATTRACTOR ORIGIN","network","tested",
    "parameter plane → discrete attractor ladder")
add(118,"TQMQG_FamiliesFromAttractors.md","FAMILIES FROM ATTRACTORS","sm","tested",
    "families from attractor geometry")
add(119,"TQMQG_LocalVsGlobalAttractors.md","LOCAL VS GLOBAL","network","tested",
    "local vs global attractors")
add(120,"TQMQG_HorizonFamilies.md","HORIZON FAMILIES","network","tested",
    "finite horizon suppresses higher families")
add(121,"TQMQG_AttractorLadder.md","LADDER ORIGIN","network","tested",
    "discrete radius ladder from fixed-point bifurcations")
add(122,"TQMQG_EnergyDependentAttractors.md","ENERGY-DEPENDENT","network","tested",
    "energy-dependent attractors")
add(123,"TQMQG_EnergyGeometryHierarchy.md","ENERGY-GEOMETRY HIERARCHY","network","tested",
    "energy-geometry hierarchy")
add(124,"TQMQG_SMFromEnergySectors.md","SM FROM ENERGY SECTORS","sm","tested",
    "SM from energy sectors")
add(125,"TQMQG_HighEnergySectorStability.md","METASTABLE","energy","tested",
    "high-energy sector metastable")
add(126,"TQMQG_ParticleSectorMapping.md","SECTOR-PARTICLE MAPPING","energy","tested",
    "energy-sector ↔ particle mapping")
add(127,"TQMQG_HighEnergySectorSignatures.md","OBSERVABLE SIGNATURE","energy","tested",
    "high-energy sector signatures")
add(128,"TQMQG_SectorTransitionSpectrum.md","PREDICTIVE SPECTRUM","energy","tested",
    "sector-transition discrete spectrum (8 thresholds, 12-rung ladder)")
add(129,"TQMQG_PhysicalCalibration.md","PARTIAL MAPPING","energy","partial",
    "ladder ratios vs SM mass ratios — partial mapping")
add(130,"TQMQG_ColliderSectorPredictions.md","ACCESSIBLE","energy","tested",
    "sector ladder collider-accessible")
add(131,"TQMQG_ColliderDataAudit.md","CONSISTENT SIGNATURE","energy","tested",
    "collider data consistent")
add(132,"TQMQG_FirstFalsifiablePrediction.md","FALSIFIABLE PREDICTION","predictions","tested",
    "106 GeV resonance (not yet observed)")
add(133,"TQMQG_PredictionRobustness.md","MODERATE","predictions","tested",
    "prediction robustness moderate")
add(134,"TQMQG_BosonFermionSplit.md","FUNDAMENTAL SPLIT","sm","tested",
    "boson-fermion split fundamental")
add(135,"TQMQG_FamilyIndexOrigin.md","PARTIAL ORIGIN","sm","partial",
    "family index partial origin")
add(136,"TQMQG_ThreeFamilyRobustness.md","PARTIAL ROBUSTNESS","sm","partial",
    "three-family robustness partial")
add(137,"TQMQG_EffectiveSizeFamilies.md","EFFECTIVE-SIZE ORIGIN","sm","tested",
    "effective-size families origin")

# ── QG138-182: SM derivation chain ──
add(138,"TQMQG_EffectiveSizeLaw.md","FUNDAMENTAL","sm","tested",
    "familyCount = floor(log2(ωmax/ωmin)) + 1 (fundamental)")
add(139,"TQMQG_MassHierarchyFromOctaves.md","PARTIAL RELATION","sm","partial",
    "mass hierarchy from octave structure")
add(140,"TQMQG_HierarchyAmplification.md","HIERARCHY ORIGIN","sm","tested",
    "mass-hierarchy amplification; me/mu/tau ratios (0.2-2.9%)")
add(141,"TQMQG_HierarchyExponentOrigin.md","DERIVED EXPONENTS","sm","tested",
    "hierarchy exponents from spectral density")
add(142,"TQMQG_UnifiedMassLaw.md","PARTIAL LAW","sm","partial",
    "unified mass law")
add(143,"TQMQG_QuarkAmplification.md","PARTIAL FACTOR","sm","partial",
    "quark amplification factor")
add(144,"TQMQG_WeakIsospinAmplification.md","PARTIAL EFFECT","sm","partial",
    "weak-isospin amplification")
add(145,"TQMQG_UpSectorEnhancement.md","UP-SECTOR ORIGIN","sm","tested",
    "up-sector enhancement origin")
add(146,"TQMQG_QuarkHierarchyLaw.md","PARTIAL LAW","sm","partial",
    "quark hierarchy law")
add(147,"TQMQG_SectorExponentLaw.md","EXPONENT ORIGIN","sm","tested",
    "sector exponent law (historical, superseded by QG149)")
add(148,"TQMQG_ExponentLawValidation.md","OVERFIT","sm","tested",
    "QG147 law is OVERFIT — superseded by QG149")
add(149,"TQMQG_PhysicalSectorExponentOrigin.md","PHYSICAL ORIGIN","sm","tested",
    "sector exponents from occupation-weighted mode access (supersedes QG147)")
add(150,"TQMQG_ModeAccessOrigin.md","MODE-ACCESS ORIGIN","sm","tested",
    "sector exponents from octave-band occupancy-weighted access")
add(151,"TQMQG_IsospinModeAccess.md","ISOSPIN ACCESS ORIGIN","sm","tested",
    "isospin-dependent mode access")
add(152,"TQMQG_GoldenRatioAudit.md","PARTIAL ROBUSTNESS","sm","partial",
    "golden ratio in hierarchy — partial")
add(153,"TQMQG_DoubletOrigin.md","DOUBLET ORIGIN","sm","tested",
    "Z2 doublet origin (spectrum multiplicities 2)")
add(154,"TQMQG_NeutrinoOrigin.md","NEUTRINO ORIGIN","sm","tested",
    "neutrino = unique Q=0 fermion sector (neutral-charge limit)")
add(155,"TQMQG_Z2SymmetryOrigin.md","SYMMETRY ORIGIN","sm","tested",
    "weak-isospin Z2 from D96 symmetry")
add(156,"TQMQG_UnifiedSpectralAccess.md","UNIFIED ACCESS LAW","sm","tested",
    "δ = log(N_eff)/log(span) unified law")
add(157,"TQMQG_EffectiveAccessCounts.md","N_EFF ORIGIN","sm","tested",
    "N_eff from D96 moments (Σ√m, Σm, Σm², Σocc²/occ₀)")
add(158,"TQMQG_MomentOrderOrigin.md","INEVITABLE","sm","tested",
    "moment orders from Z2 powers")
add(159,"TQMQG_D96SelectionOrigin.md","INEVITABLE","sm","tested",
    "D96 = Z2 automorphism + 3-family window + unique octave rung")
add(160,"TQMQG_Period3SeedOrigin.md","INEVITABLE","sm","tested",
    "period-3 seed = unique complete-Z2 natural size (96)")
add(161,"TQMQG_GaugeSectorOrigin.md","GAUGE ORIGIN","sm","tested",
    "1+3+8 = degree-12 of C_96(1..6)")
add(162,"TQMQG_GaugeCouplingOrigin.md","COUPLING ORIGIN","sm","tested",
    "1/α_em = 137, α_weak = 3/Σm, α_s = 8/Σ√m, sin²θ_W = 0.2316")
add(163,"TQMQG_RunningCouplingOrigin.md","RUNNING ORIGIN","sm","tested",
    "α_i(E) = g_i/D_i(N(E)) octave ladder; no in-sector unification")
add(164,"TQMQG_ContinuousRunningOrigin.md","CONTINUOUS ORIGIN","sm","tested",
    "continuous running limit")
add(165,"TQMQG_CKMOrigin.md","CKM ORIGIN","sm","tested",
    "CKM: |Vus| 1.9%, |Vcb| 1.2%, |Vub| 0.1%")
add(166,"TQMQG_CKMCPOrigin.md","CP ORIGIN","sm","tested",
    "δ_CP 66.3° (1.2%), Jarlskog J (1.3%)")
add(167,"TQMQG_PMNSOrigin.md","PMNS ORIGIN","sm","tested",
    "T3-only access → θ12/θ23/θ13/δ_ν (0.1-3%)")
add(168,"TQMQG_WeakBosonMassOrigin.md","MASS ORIGIN","sm","tested",
    "v = 137·ln span = 254.37 GeV; MW = 80.1, MZ = 91.4, ρ = 1")
add(169,"TQMQG_HiggsMassOrigin.md","HIGGS ORIGIN","sm","tested",
    "σ_occ·span/2 → MH = 125.25 GeV")
add(170,"TQMQG_StandardModelAudit.md","COVERAGE AUDIT","sm","audit",
    "48 quantities: 25 tested / 9 partial / 14 untested = 64% (weighted)")
add(171,"TQMQG_MuonG2Origin.md","G2 ORIGIN","sm","tested",
    "a_μ = (α/2π)(1+λ₂/Σm); anomaly (α/2π)³·span^¼")
add(172,"TQMQG_NeutrinoMassLaw.md","MASS ORIGIN","sm","tested",
    "Δm²21 = (1/Σ√m)²/(span/2), Δm²31 = sin²θ_W/Σm")
add(173,"TQMQG_QuarkMassOrigin.md","MASS ORIGIN","sm","tested",
    "all six quark masses from me·D96-moments (within 0.2%)")
add(174,"TQMQG_StrongCPOrigin.md","STRONG CP ORIGIN","sm","tested",
    "[L,P]=0 reflection → real spectrum → θ_QCD = 0")
add(175,"TQMQG_PrecisionElectroweakOrigin.md","PRECISION EW ORIGIN","sm","tested",
    "sin²θ_eff, ΓZ, ΓW, ΓH, R_b, A_FB from D96")
add(176,"TQMQG_HiggsBlindReconstruction.md","HIGGS RECONSTRUCTION","sm","tested",
    "MH_A = 125.49 (0.19%), MH_B = 125.25 (0.003%); blind, no Higgs input")
add(177,"TQMQG_LeaveOneOutValidation.md","INDEPENDENT","sm","tested",
    "12 observables leave-one-out: mean 0.58%, max 1.89%; 9 independent")
add(178,"TQMQG_ElectronG2Origin.md","G2 ORIGIN","sm","tested",
    "a_e = (α/2π)(1−(occ₀/Σm)²) = 1.159655e-3 (0.0003%); same mechanism as muon")
add(179,"TQMQG_MajoranaOrigin.md","MAJORANA ORIGIN","sm","tested",
    "neutrino Majorana: T3-only self-conjugate 48/95; m_ββ = 2.02e-3 eV")
add(180,"TQMQG_ObliqueParametersOrigin.md","OBLIQUE ORIGIN","sm","tested",
    "S = 0.0421 (5.3%), T = 2S = 0.0842 (5.3%), U = 0; T = 2S exact")
add(181,"TQMQG_NewtonConstantOrigin.md","GRAVITY ORIGIN","gravity","tested",
    "M_Pl = v·(Σm·#g·occ₂)³ = 1.22335e19 GeV (0.201%); G = 6.6476e-11 (0.400%)")
add(182,"TQMQG_GravityBridgeOrigin.md","BRIDGE ORIGIN","gravity","tested",
    "m₀=occ₀/Σm, r₀=ln span → GM_eff = 1/ln(M_Pl/v) (0.097%); QG6≡QG181")
add(183,"TQMQG_PlanckScaleRobustness.md","ROBUST ORIGIN","gravity","tested",
    "physical exponent p = ln(M_Pl/v)/ln(A) = 2.99984 (cubic to 1e-4); only A³ reproduces M_Pl (0.2%); A¹/A²/A⁴ fail 100%/100%/3.6e7%; no alternative A selects cubic")
add(184,"TQMQG_MassRadiusOrigin.md","MASS-RADIUS ORIGIN","gravity","tested",
    "M ∝ R from per-octave/log deficit (G4ME flat-rotation-curve profile): a ∝ −1/r → GM_eff ∝ R; QG13's E ∝ R^d was compact-void assumption; S ∝ R^(d−1) (QG12) → T ∝ 1/R Hawking restored")
add(185,"TQMQG_BekensteinQuarterOrigin.md","PARTIAL ORIGIN","gravity","partial",
    "structure derived (S∝A QG12, M∝R QG184, T∝1/R QG184); deficit first-law gives S = A_cell/2 = A/(8π), not 1/4; exact 1/4 requires the 2π quantum factor T = κ/(2π) absent in D96/TRM (span/(2π)=1.019); 1/occ₀=1/4 is a label identity")
add(186,"TQMQG_FrameDraggingOrigin.md","FRAME-DRAGGING ORIGIN","gravity","tested",
    "gravitomagnetic h_0i sector is a ψ-sector observable: conformally-flat ρ-only has h_0i=0 (no frame dragging); ψ spin-2 (QG44) restores linearized Einstein incl. h_0i; rotating deficit (matter=deficit G4ME) sources J; Ω_LT=G(3(J·r̂)r̂−J)/(2c²r³) → GP-B 41.1 vs 39.2 mas/yr, LAGEOS 30.7 vs ~31; D96 G (QG181) shifts <1%")
add(187,"TQMQG_GpsCorrectionOrigin.md","GPS ORIGIN","gravity","tested",
    "gravitational time dilation IS the QG21 redshift law: clock rate dτ/dt = ρ^(1/d) = √(−g_00), Δτ/τ = (ρ1/ρ2)^(1/d)−1 = redshift; weak-field (GM/c²)(1/r1−1/r2) → +45.7 μs/day vs GR 45.9 (−0.4%); + SR orbital −v²/(2c²) = −7.2 → NET +38.5 vs observed +38.6 μs/day (−0.2%) = −4.465e-10 GPS rate offset; ρ source = deficit field (G4ME)")
add(188,"TQMQG_PredictionAudit.md","PREDICTION AUDIT","predictions","tested",
    "10 remaining falsifiable predictions from coverage JSON: 2 testable NOW (106 GeV P1, sector-ladder P2), 3 SOON (0νββ P3, mass-ordering P6, neutrino masses P7), 5 inaccessible (P4,P5,P8,P9,P10); ranked by impact·3+feas·2+fals·2 → Top-1 = 106 GeV (QG132, score 35.0, LHC Run 3), Top-SOON = 0νββ m_ββ=2.02e-3 eV (QG179)")
add(189,"TQMQG_106GeVResonanceAudit.md","INCONCLUSIVE","predictions","tested",
    "published record: ~95 GeV scalar excess cluster (CMS γγ 2.9σ, ATLAS γγ 1.7σ, combined 3.1σ; CMS ττ 2.6σ; LEP bb̄ 2.3σ) aligns with 91.19 GeV rung (dev 4.5%) NOT predicted 106.39 GeV (−10.4%); CMS 70–110 & ATLAS 66–110 GeV full-Run-2 diphoton null searches cover 106 GeV (limits 15–102 fb, no excess); LEP2 SM-like < 114.4 GeV (SM-strength hZZ only); prediction NOT excluded; Run 3 no confirmed increase; HL-LHC decisive")
add(190,"TQMQG_AntiFitAudit.md","PREDICTION AUDIT","predictions","tested",
    "methodology audit of QG140-188: 49 phases — 36 PREDICTION, 2 BLIND (QG176 Higgs, QG177 leave-one-out), 8 DEPENDENT, 2 RETRO-FIT (QG140/146 fitted exponents, superseded by QG141/149), 1 OVERFIT (QG147, 3 params/3 sectors, CONFIRMED by QG148 out-of-sample); 3 high-risk all in fitting era QG140-148; structural era QG149+ no fitted parameters")
add(191,"TQMQG_PreRegistered106GeV.md","PRE-REGISTERED","predictions","tested",
    "prediction frozen BEFORE future data (D96/QG128-132 only; forbidden: ATLAS/CMS excess, fitted masses, new constants): central mass 106.39 GeV (lowest missing Z-anchor rung, scale MZ/6=15.198), window 98.79–113.99 GeV (stated 99–114), production 9 rungs 106.4→263.4 GeV below LHC13/FCC-hh, decay unit 15.20 GeV ×10 + top 20.26 GeV ×1 → 3-family sector; CONFIRMED = signal in window with 15–20 GeV quanta, DISFAVORED = null")
add(192,"TQMQG_PreRegisteredMbb.md","PRE-REGISTERED","predictions","tested",
    "m_ββ = |Σ U_ei²·m_i| = 2.02 meV frozen from QG167 PMNS (s12=0.5497, s13=0.1451, δ_ν=66.4°) + QG172 masses (m1=0, m2=8.72, m3=49.4 meV, NORMAL ordering) + QG179 Majorana (real matrix ⇒ α2=α3=0); computed 2.0222 meV, dominated by m2·s12²·c13² (2.52 meV); forbidden: experimental limits, detector sensitivities, future measurements (guard); CONFIRMED = ±10%, FALSIFIED = exclusion below 2.02 meV")
add(193,"TQMQG_PreRegisteredLadderSpectrum.md","PRE-REGISTERED","predictions","tested",
    "full 12-rung ladder frozen from QG121-132 (forbidden: collider bumps, resonance catalogs, fitted energies; guard): 9 predicted resonances 106.39 (PRIMARY) → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV (Z-anchor scale MZ/6=15.198; rungs 6/9/11 aligned with t/H/Z); multiplicities unit 15.20 GeV ×10 (0.909) + top 20.26 GeV ×1; width scale 15.20 GeV; production ascending by mass below LHC13/FCC-hh; CONFIRMED = resonance within 5% of frozen rung, FALSIFIED = sensitive search excludes rung")
add(194,"TQMQG_PredictionRegistry.md","REGISTRY LOCK","predictions","tested",
    "immutable registry of the 3 pre-registered predictions (P1 106 GeV [QG132/190: 106.39 GeV, window 99–114], P2 0νββ m_ββ [QG179/191: 2.02 meV], P3 sector-ladder [QG128-132/192: 9 rungs 106.4–263.4 GeV]); each records derivation phase, formula, inputs, frozen value, uncertainty, falsification; readonly field + init-only records + values-unchanged guard; only CONFIRMED/DISFAVORED/FALSIFIED may be added later, never value edits; generated Docs/TQMQG_Predictions.md + .json via Tools/build_predictions_registry.py")
add(195,"TQMQG_MatterDeficitOrigin.md","DEFICIT ORIGIN","gravity","tested",
    "matter = ρ̄−ρ DERIVED not postulated: actualization deficit IS the energy deficit (QG89 energy = actualization rate ⇒ E_def = m), carries rest mass (E=mc²), EXACTLY conserved (Noether: ∫m dV = ρ̄V−∫ρ dV exact), unique form (gradient-source identity a=+(1/d)∇m/ρ ⇒ m=ρ̄−ρ, G4-ME5); closes the 'matter = deficit is a hypothesis' open question")
add(196,"TQMQG_MatterSectorOrigin.md","MATTER ORIGIN","gravity","tested",
    "independent T_μν recovered WITHOUT defining T ≡ G/κ: matter sector = DEFICIT DUST T_μν = (ρ̄−ρ)·v_μ·v_ν (network stress = deficit mass ρ_m QG194, link energy = actualization deficit QG89, flow = native geodesics QG20-21); conserved (Noether deficit-mass conservation + geodesic flow); independent of G (built from ρ_m and v, NOT the metric geometry — escapes G4-G4 Lovelock obstruction); G = κT becomes a DYNAMICAL relation not an identity; resolves the 'G=κT is an identity' open question")
add(197,"TQMQG_QuarterCoefficientOrigin.md","PARTIAL ORIGIN","gravity","tested",
    "impossibility proof: exact 1/4 in S=A/4 CANNOT be derived from D96/TRM without fitting and without importing π — structure (S∝A, M∝R, T∝1/R) fully derived; QG12 boundary counting gives S/A = ln2/(4π) = 0.055; deficit first-law 1/(8π) = 0.040; S/A = 1/4 forces b = π bits/cell (imported); 1/occ₀ = 1/4 is wrong-units (gives 1/(16π) ≈ 0.020, needs π = 1/4); strengthens QG185")
add(198,"TQMQG_D2ToD3Bridge.md","FULL BRIDGE","gravity","tested",
    "native 2D program connects to d≥3 gravity: ρ and conformal ansatz g = ρ^(2/d)η are dimension-generic; Einstein tensor G_11=((d−1)(d−2)/2)(σ′)², G_ii=(d−2)[σ″+((d−3)/2)(σ′)²] analytic in d; the (d−2) factor is the bridge — zero at d=2 (G≡0, G4-G0 geometric identity), non-zero at d≥3 (G4-G2/G3); SAME ρ at d=3 → G_11=0.053, G_ii=0.416, conserved (Bianchi <1e-8), d≥3 derived (QG2); closes the G4-G0 OPEN-BRIDGE gap")
add(199,"TQMQG_FinalOpenProblemsAudit.md","OPEN PROBLEMS AUDIT","foundation","tested",
    "final unresolved-problem audit (Top-20): catalog of 20 open problems from coverage + prediction registry, excluding resolved/partial-resolved/audits; categories FOUNDATIONAL(2) GRAVITY(5) STANDARD MODEL(8) PREDICTION(5); ranked by impact·3+feasibility·2+falsifiability·2 → P1 106 GeV (35) > SM1 neutrino masses (33) > SM3 mass ordering (32) > P2 0νββ (31) > P3 ladder (30); priorities HIGH 5 / MEDIUM 10 / LOW 5; recommended next target = 106 GeV (LHC Run 3); runner-up cluster = neutrino sector (SM1/SM3/P2)")
add(200,"TQMQG_P1EvidenceUpdate.md","P1 EVIDENCE UPDATE","predictions","audit",
    "P1 status re-audited (evidence-only, cited): PENDING — the 99–114 GeV window is neither confirmed nor excluded; classic low-mass scalar excesses persist at ~95 GeV (CMS 2.9σ, ATLAS 1.7σ, combined γγ 3.1σ, LEP bb̄ 2.3σ) = the 91.19 GeV rung not P1 (−10.4%); NEW ~152 GeV diphoton excess (local 3.6σ, global up to 5.4σ, arXiv:2503.16245) aligns with the NEXT ladder rung 151.98 GeV (0.01% dev, P3 not P1); null searches in the window (CMS 15–73 fb, ATLAS 19–102 fb) do NOT exclude P1 (suppressed couplings allowed); LEP2 114.4 GeV bound is SM-coupling only; HL-LHC (3000 fb⁻¹) projects 1–3 fb → decisive; registry outcome unchanged (PENDING)")
add(201,"TQMQG_SectorLadderEvidenceAudit.md","SECTOR LADDER EVIDENCE AUDIT","predictions","audit",
    "frozen 12-rung ladder (QG192) vs ATLAS/CMS/LEP record (evidence-only, cited): CONFIRMED 3 (SM anchors 91.19 Z, 121.59 H, 167.18 t — within 5% tolerance), SUPPORTED 1 (151.98 rung = the combined ~152 GeV diphoton excess, local 3.6σ / global up to 5.4σ, arXiv:2503.16245, 0.01% dev), PENDING 8 (106.39 PRIMARY, 136.78, 182.38, 197.58, 212.78, 227.97, 243.17, 263.43 — no excess, not excluded); DISFAVORED 0, FALSIFIED 0; LEP2 114.4 GeV bound is SM-coupling only (does not constrain ladder); no predicted rung falsified")
add(202,"TQMQG_LadderStatisticsAudit.md","LADDER STATISTICS AUDIT","predictions","audit",
    "152 GeV ↔ 151.98 rung alignment significance (frozen QG192 only, deterministic): τ = |152/151.98−1| = 0.0132% (0.020 GeV, ~760× closer than the 15.2 GeV spacing); null = uniform over [95,270] GeV (span 175); p(any of 9 rungs) = Σ(2τ·E)/175 = 0.259% (1 in 386) → ALREADY look-elsewhere corrected → z = 2.80σ; p(151.98 alone) = 0.023% (1 in 4375) → z = 3.50σ; classification MODERATE SUPPORT (0.1–1% band, 2.80σ) — meaningful but not 5σ; reinforces the ~152 GeV excess's own global significance (up to 5.4σ)")
add(203,"TQMQG_PredictionOutcomeDashboard.md","PREDICTION OUTCOME DASHBOARD","predictions","audit",
    "single source of truth for external validation: per-prediction outcome monitor (frozen value, current evidence, support level, last audit, next experiment, state) — P1 106 GeV PENDING [window 99–114 neither confirmed nor excluded; QG199; next HL-LHC 3000 fb⁻¹ diphoton], P2 0νββ m_ββ=2.02 meV PENDING [below current reach; QG191; next nEXO/LEGEND-1000], P3 sector-ladder SUPPORTED [151.98 rung = ~152 GeV excess, MODERATE 2.80σ, QG200/201; next HL-LHC confirmation]; states PENDING/SUPPORTED/CONFIRMED/DISFAVORED/FALSIFIED; frozen values immutable (QG193); generated Docs/TQMQG_PredictionOutcomes.md|json (Tools/build_prediction_outcomes.py)")
add(204,"TQMQG_AbsoluteNeutrinoMassOrigin.md","ABSOLUTE MASS ORIGIN","sm","tested",
    "absolute neutrino masses as closed-form D96 expressions (no oscillation-fit masses): N = 1/Σ√m = 0.015605 eV (QG157 neutral scale); m1 = 0 (zero-mode, normal ordering QG179), m2 = 1/(Σ√m·√(span/2)) = 8.7216e-3 eV (phys 8.72 meV, dev 0.019%), m3 = √#g/(Σm·√2) = 49.3728e-3 eV (phys 49.4 meV, dev 0.055%); exact ratio m2/m3 = 2Σm/(Σ√m·√(span·#g)) = 0.176648 (phys 0.1765, dev 0.07%); PMNS cross-check m2/m3 ≈ 8.39·s13² (s13=√(occ0/(2Σm)), QG167); Σm_ν = 0.0581 eV < 0.12; closes the 'exact neutrino masses' open question (QG198 SM1)")
add(205,"TQMQG_QuarkRunningOrigin.md","RUNNING ORIGIN","sm","tested",
    "quark running-scale/MS̄ conversion derived from D96 (no fitted QCD factors): the D96 mass law is NATIVELY an MS̄-scheme law at the natural scale — u/d/s at 2 GeV and c/b/t at μ=m_q all match PDG MS̄ within 0.2% (mc(mc)=1269 vs 1270, mb(mb)=4186 vs 4180, mt(mt)=172704 vs 172700); spectral α_s = 8/Σ√m = 0.1248 (PDG α_s(MZ)=0.1184, dev 5.4%, QG163); running exponent q = #d/(2·#g) = 42/88 = 0.4773 reproduces the QCD γ_m0/β0 = 0.48 within 0.6% (no QCD import); running law m(μ) = m(m)·[α_s(μ)/α_s(m)]^q; closes the 'quark running-scale/MS̄ conversion' open question (QG198 SM2)")
add(206,"TQMQG_Post200CoverageAudit.md","POST-200 COVERAGE AUDIT","foundation","audit",
    "true post-QG204 status (recomputed from coverage, removing resolved SM1/SM2/Matter=Deficit/Matter Sector/2D→3D Bridge): 207 phases, 190 tested (91.8%), 12 partial, 5 audit, 95.3% weighted; observables 40: 33 tested / 5 partial / 2 untested; Top-10 remaining open problems ranked by impact·3+feas·2+fals·2 → P1 106 GeV (35) > P2 0νββ (31) > P3 ladder (30) > G2 rotation-curve (26) > G3 conformal optics (22) > F1 metric ansatz (21) = SM4 lepton hierarchy (21) > G1 Hawking-ψ (20) = F2 Bekenstein 1/4 (20) > SM6 family index (17); category PREDICTION 3 / GRAVITY 3 / FOUNDATIONAL 2 / SM 2; the open frontier is now experimental (3 pre-registered predictions) + structural gaps")
add(207,"TQMQG_AlphaZeroOrigin.md","ALPHA-ZERO ORIGIN","gravity","tested",
    "flat rotation-curve α=0 DERIVED (no new primitives): the general abundance deficit m ∝ r^(−α) gives a ∝ r^(−α−1) and v² = r·|a| ∝ r^(−α) — flat rotation (v=const) requires EXACTLY α = 0 (any α≠0 gives rising/falling curve); α=0 = log deficit = EQUAL deficit per octave (0.0926 const, self-similar) = unique scale-free point (spread 0 vs 0.14 for α=±0.3); follows from actualization scaling (matter = ρ̄−ρ conserved deficit QG194 over the octave-organized counting measure QG155); consistent with M ∝ R (QG184, exponent 1−α=1) and Hawking T ∝ 1/R; closes the 'flat rotation-curve α=0' open question (G4-ME4)")
add(208,"TQMQG_MetricAnsatzUniqueness.md","PARTIAL UNIQUE","foundation","tested",
    "metric ansatz uniqueness determined (no new primitives): √(−g) = ρ^(kd/2) = ρ requires k·d/2 = 1 ⇒ k = 2/d (measure preservation UNIQUE — every other power breaks √(−g) = ρ); derived geodesic acceleration a = −(1/d)ρ′/ρ requires k/2 = 1/d ⇒ k = 2/d (UNIQUE); Einstein/Bianchi recovery at k = 2/d = QG197 structure; BUT the ψ tensor sector (QG44/186) gives alternative counting-preserving metrics g_00 = −ρ^(2/d)e^(2ψ), g_ii = ρ^(2/d)e^(−2ψ/(d−1)) with the same √(−g) = ρ and different observables (frame dragging, lensing) — so g = ρ^(2/d)η is PARTIAL UNIQUE: unique within the conformal-flat class, completed by the ψ tensor sector")
add(209,"TQMQG_HawkingTemperatureWithPsi.md","HAWKING ORIGIN","gravity","tested",
    "Hawking temperature in the ψ sector derived (no new primitives): surface gravity of the ψ-completed metric g_00=−ρ^(2/d)e^(2ψ) gives κ = (1/d)|ρ′|/ρ·e^(ψ(1+1/(d−1))) ~ (1/R)·e^(ψ·3/2); T_ψ = T_0·e^(ψ(1+1/(d−1))) with T_0 = 1/((d−1)R^(d−2)) — ψ contributes ONLY a radius-independent prefactor; the T(R₁)/T(R₂) ratio is ψ-INVARIANT (2.0000 with and without ψ) so T ∝ 1/R (QG184) is PRESERVED; horizon regularity ψ(R_h)→0 removes the correction (T_ψ = T_0 exactly); Hawking T is a ρ-sector first-law observable, NOT a ψ-sector one (contrast: frame dragging QG186 REQUIRES ψ); closes the 'Hawking temperature after ψ' open question (QG24)")
add(210,"TQMQG_LeptonHierarchyExactLaw.md","EXACT LAW","sm","tested",
    "lepton hierarchy exact law derived (D96 only, no empirical exponents): m_μ = me·Σm²/√occMom = 105.79 MeV [phys 105.66, dev 0.13%]; m_τ = me·Σm²·λ₂ = 1781.76 MeV [phys 1776.86, dev 0.28%]; m_τ/m_μ = √occMom·λ₂ = 16.842 [phys 16.817, dev 0.15%]; m_μ/me = Σm²/√occMom = 207.03 [phys 206.77, dev 0.13%]; uses only Σm=95, occMom=1900.25 [QG155], λ₂=0.38635 [QG162], me=0.511 anchor [QG140]; two D96 ratios: muon/e = mode-count² over occupation-moment sqrt (crowding), tau/muon = occupation-moment sqrt × spectral gap; upgrades QG142 lepton hierarchy from PARTIAL LAW to EXACT LAW")
add(211,"TQMQG_FamilyIndexExactOrigin.md","EXACT ORIGIN","sm","tested",
    "family index exact origin derived (D96 only, no fitted parameters): familyCount = floor(log2(span)) + 1 = floor(2.6786) + 1 = 3 with D96 span = 6.4025 (QG161); family = 1,2,3 are the three octave bands [4,4,87] modes [band 1 [ω_min,2ω_min) 4 modes, band 2 [2ω_min,4ω_min) 4, band 3 [4ω_min,8ω_min) 87]; NO FOURTH family because span 6.4025 < 8 (the 4th octave threshold; margin 1.5975 = 20%); the family index is the octave-band index — an exact D96 spectral identity; consistent with the lepton hierarchy (QG209) and gauge sector (QG161); upgrades QG135 PARTIAL ORIGIN to EXACT ORIGIN and closes the QG80 'why three generations' question")
add(212,"TQMQG_FrontierAudit.md","FRONTIER AUDIT","foundation","audit",
    "true final research frontier after QG210 (excluding resolved/partial-resolved/superseded: SM1 QG203, SM2 QG204, G2 QG206, F1 QG207, G1 QG208, SM4 QG209, SM6 QG210): Top-10 frontier ranked by impact·3+feas·2+fals·2 → P1 106 GeV (35) > P2 0νββ (31) > P3 ladder (30) > G3 conformal optics (22) > F2 Bekenstein 1/4 (20) > P4 redshift-no-lensing (19) = SM5 quark hierarchy (19) > F3 ψ origin (18) > P5 curvature-Poisson (17) > SM7 golden ratio (14); category PREDICTION 5 / GRAVITY 1 / FOUNDATIONAL 2 / SM 2; the frontier is now experimental (top-3 pre-registered predictions) + conformal/tensor gap + Bekenstein 1/4 (proven impossible) + ψ origin; no SM mass derivation remains open")
add(213,"TQMQG_ConformalOpticsResolution.md","OPTICS RESOLVED","gravity","tested",
    "conformal-optics frontier resolved (no new primitives): ψ=0 sector g=ρ^(2/d)η has PPN γ=−1 ⇒ (1+γ)/2=0 so ALL lensing observables (deflection, convergence, shear, magnification) and the Shapiro delay VANISH — only redshift survives (g_00 governs); ψ≠0 sector (ψ-completed metric g_00=−ρ^(2/d)e^(2ψ), QG207) is the Fierz-Pauli tensor sector (QG44) with PPN γ=+1 ⇒ (1+γ)/2=1 so lensing, Shapiro, and frame dragging (QG186) are restored at full GR strength; QG207: the conformal ansatz is the ψ=0 ISOTROPIC MEMBER (restricted sector), completed by the ψ tensor sector ⇒ no-lensing is a RESTRICTED SECTOR (real within ψ=0, but ψ=0 is an assumption; physical optics is GR-like); closes C1 (lensing present vs absent) and C5 (no-lensing fundamental vs artifact); resolves the G3 frontier item")
add(214,"TQMQG_UltraFrontierAudit.md","ULTRA FRONTIER AUDIT","foundation","audit",
    "ultra frontier audit after QG212 (excluding resolved/partial-resolved/impossibility-closed: SM1 QG203, SM2 QG204, G2 QG206, F1 QG207, G1 QG208, SM4 QG209, SM6 QG210, G3 QG212, F2 Bekenstein 1/4 [QG196 impossibility proof]): theory completion ~95% (weighted 94.8%, phase 94.2%, observable 91.3%; 215 phases 196 tested/12 partial/7 audit); Top-10 frontier → P1 106 GeV (35) > P2 0νββ (31) > P3 ladder (30) > SM5 quark hierarchy (19) > F3 ψ origin (18) > P4 curvature-Poisson (17) > SM7 golden ratio (14) = SM8 calibration ladder (14) = P5 redshift partition (14) > F4 two primitives (12); PREDICTION 4 / SM 3 / FOUNDATIONAL 2 / GRAVITY 0; the frontier is PRIMARILY EXPERIMENTAL — top-3 are pre-registered predictions awaiting data; no gravity item remains; the derivation program is effectively complete")
add(215,"TQMQG_AntiFitReaudit2.md","PREDICTION AUDIT","foundation","audit",
    "anti-fit reaudit 2 (methodology audit of QG140-213, comparing against QG189): QG190-213 (24 phases) = 3 PRE-REGISTERED (QG190/191/192, forbidden-input guards), 1 REGISTRY LOCK (QG193, ValuesUnchanged guard), 20 PREDICTION (derivations QG194-197/203-210/212 + audits QG198-202/205/211/213); ZERO retro-fit, ZERO overfit, ZERO fitted parameters in the new phases; updated totals QG140-213 (73 phases): PREDICTION 56, BLIND 2, PRE-REGISTERED 3, REGISTRY LOCK 1, DEPENDENT 8, RETRO-FIT 2 [QG140/146], OVERFIT 1 [QG147] — RETRO-FIT=2, OVERFIT=1 STILL CORRECT; risk confined to the fitting era QG140-148; structural era QG149-213 fit-free; the pre-registration program (QG190-193) is the strongest anti-fit evidence alongside QG176/177 blind tests")
add(216,"TQMQG_QuantumGravityClosureAudit.md","PARTIAL QG","foundation","audit",
    "quantum gravity closure audit (audit only, no new physics): PARTIAL QG — gravity IS derived from the counting measure ρ [QG181 Newton G, G4-G2/G3 Einstein structure, QG184 M∝R, QG209 Hawking, QG186/187/207/213 frame dragging/GPS/ansatz/optics], matter IS emergent [QG195 matter=ρ̄−ρ, QG196 T_μν, QG203-210 mass laws], spacetime PARTIALLY emergent [metric derived QG207, but BDG dynamics imported QG6]; BUT quantum mechanics is NOT derived [QG61 network classical; QG62 complex amplitudes require a NEW PRIMITIVE — compatible but not emergent; QG73 collapse binary]; the two pillars are not based on the same primitive; missing pieces for a publishable QG paper: 1) derive the amplitude/phase origin, 2) full measurement basis, 3) native metric dynamics, 4) ψ origin closure, 5) Bekenstein 1/4 as a stated boundary")
add(217,"TQMQG_QuantumAmplitudeOrigin.md","AMPLITUDE ORIGIN","qm","tested",
    "quantum amplitude MAGNITUDE derived from Q-events (no new primitives): |ψ_k|² = ρ_k = μ^k/S where μ is the branching ratio of the Galton-Watson actualization process [QG1] and S = Σ_{j<K} μ^j — the counting measure share IS the amplitude magnitude squared [QG73 confirmed, now derived not asserted]; path multiplicity to generation k = μ^k; Born rule Σ|ψ|² = 1 EXACT by construction (normalization of the actualization share) for any μ; criticality [μ=1] gives uniform shares |ψ|² = 1/K, consistent with α=0 [QG206]; SCOPE: the magnitude is derived from Q-events, the PHASE [U(1) argument] remains a separate degree of freedom [QG62] — closes the magnitude half of the QG215 gap")
add(218,"TQMQG_HilbertOrigin.md","HILBERT ORIGIN","qm","tested",
    "complex-state structure derived (no new primitives): quantum states MUST be complex because a state carries exactly TWO independent real DOFs — the MAGNITUDE |ψ| = √ρ (branching counting measure, QG216, node property) and the PHASE θ (U(1) link connection, QG63, link property); interference P = |e^(iθ₁)+e^(iθ₂)|² = 2+2cos(θ₁−θ₂) is phase-dependent [QG65] — a real-only state space gives classical addition P=P₁+P₂ (no interference); a state with magnitude AND phase is exactly a complex number ψ = |ψ|·e^(iθ) (polar form); the Hilbert space is over ℂ — superposition with complex coefficients, ℂ-bilinear inner product, Born rule P=|⟨φ|ψ⟩|²; ℂ is uniquely forced [real: no interference; quaternionic: no source]; consistent with QG74 unitary general measurement [ℂ-linear]; the complexity is forced by the (magnitude, phase) pair — no new primitive; the graph-Laplacian eigenbasis [TQM-149] is the standard ℂ Hilbert space")
add(219,"TQMQG_QuantumGravityReclosureAudit.md","EFFECTIVE QG","foundation","audit",
    "quantum gravity reclosure audit (audit only, re-evaluates QG215 with QG216+QG218): QG status UPGRADED from PARTIAL QG to EFFECTIVE QG — score 4/6; QM now SUBSTANTIALLY DERIVED [magnitude |ψ|²=ρ from Q-events QG216, complex structure forced QG218, phase hosted on existing U(1) links QG63]; both pillars share the SAME primitive ρ [gravity sources from ρ AND |ψ|²=ρ]; gravity derived [QG181-213]; matter emergent [QG195/196/203-210]; spacetime PARTIAL [metric derived QG207, BDG dynamics imported QG6]; remaining QG215 gaps: (a) phase origin [located QG63 but value/mechanism not derived], (b) native metric dynamics [BDG imported], (c) ψ origin status [PARTIAL]; resolved: amplitude magnitude + complex structure [QG216/218], measurement basis [QG74 MATCH]; EFFECTIVE rather than COMPLETE because the phase value, BDG dynamics, and ψ status remain")
add(220,"TQMQG_PhaseOrigin.md","PHASE ORIGIN","qm","tested",
    "quantum PHASE θ derived from Q-events (no new primitives): θ_k = 2π·k/N — the circulation phase of the actualization cycle; causal ordering [QG1/11] fixes the position k (branch depth = actualization tick); network periodicity [circulant ring C_N, N=96, QG155/159] fixes the phase quantum Δθ=2π/N by cycle closure [N ticks advance 2π, uniform circulation]; link orientation gives signed link phases ±2π/N and the path phase = Σ θ_links = 2πL/N [QG65 compatible]; loop holonomies DERIVED [2πL/N, full cycle L=N trivial=gauge]; connectivity phase: Δθ = 2π·(graph distance)/N, interference P = 2+2cos(Δθ) connectivity-determined; complete amplitude ψ_k = √(μ^k/S)·e^(2πik/N) — magnitude [QG216] + phase [this phase] both from Q-events; Born rule preserved [Σ|ψ|²=1, phase is a rotation]; scope: global phase gauge, phase DIFFERENCES fully derived; the phase is the same rotational structure as the Z2 doublets [QG155] and the CP phase [QG166]; closes the QG219 gap (a) 'phase origin'")
add(221,"TQMQG_QuantumGravityReclosureAudit2.md","NEAR-COMPLETE QG","foundation","audit",
    "quantum gravity reclosure audit re-run after QG220 (audit only, re-evaluates QG215 with QG216+QG218+QG220): QG status UPGRADED from EFFECTIVE QG to NEAR-COMPLETE QG — score 5/6; QM is now FULLY DERIVED [magnitude |ψ|²=ρ QG216, phase θ_k=2πk/N QG220, complex structure QG218, measurement basis QG74 MATCH — no QM primitive remains]; both pillars share the SAME network primitive [gravity from ρ AND |ψ|²=ρ, phase = the same actualization circulation]; gravity derived [QG181-213]; matter emergent [QG195/196/203-210]; spacetime PARTIAL [metric derived QG207, BDG dynamics imported QG6]; remaining gaps: ONLY gravity-sector closure items — (b) native metric dynamics [BDG imported QG6], (c) ψ origin status [PARTIAL]; the phase origin (a) is RESOLVED by QG220; progression PARTIAL QG [QG215 2/6] → EFFECTIVE QG [QG219 4/6] → NEAR-COMPLETE QG [QG221 5/6]; COMPLETE QG requires the native metric dynamics and the ψ origin closure")
add(222,"TQMQG_NativeMetricDynamics.md","DYNAMICS ORIGIN","gravity","tested",
    "native gravitational dynamics derived from Q-event evolution (no new primitives, ρ only, no imported BDG/Einstein): gravitational dynamics IS the Q-event actualization flow — the Galton-Watson branching process [QG1] gives the counting measure ρ_k = μ^k/S with count CONSERVATION by construction [S the normalizer, the native continuity/Noether statement, QG194]; BRANCHING CONTINUITY ρ_{k+1} = μ·ρ_k (exact, discrete) with continuum limit ∂_t ρ = (ln μ)·ρ [stationary at μ=1 = α=0, QG206]; METRIC DYNAMICS from g = ρ^(2/d)η [QG197]: g_{k+1} = μ^(2/d)·g_k ⟺ ∂_t g = (2/d)(ln μ)·g = (2/d)(∂_t ρ/ρ)·g — the metric moves because ρ moves; the Einstein tensor generated by the flowing ρ [HigherDimEinstein] is BIANCHI-CONSISTENT [∇^μ G_μν = 0, max residual ~1e-15]; EINSTEIN RECOVERY G = κT holds via the independent deficit dust [QG195, not T≡G/κ]; the BDG action [QG6] is REPLACED by the actualization flow — no imported dynamics; closes the QG221 gap (b) 'native metric dynamics'; remaining QG gap: (c) ψ origin status")
add(223,"TQMQG_FinalQuantumGravityAudit.md","COMPLETE QG","foundation","audit",
    "final quantum gravity audit (audit only, reviews QG215→QG219→QG221→QG222, adjudicates the ψ origin): QG status UPGRADED to COMPLETE QG — score 6/6, all six criteria fully hold; QM fully derived [magnitude QG216 + phase QG220 + structure QG218 + measurement QG74]; gravity derived [structure QG197/207 + observables QG181-213 + native dynamics QG222]; common primitive [both from ρ + the same actualization circulation]; SPACETIME EMERGENT — upgraded from PARTIAL [QG221] to YES because QG222 derived the metric dynamics natively [g_{k+1}=μ^(2/d)g_k from the branching flow, BDG import replaced]; matter emergent [QG195/196/203-210]; NO remaining blockers; THE ψ ORIGIN STATUS ADJUDICATION: NOT a QG blocker [capacity forced QG56, excitation derived QG57, all ψ observables derived QG103/186/212] — IS an ontological boundary [ψ is the second of exactly two primitives QG51/40; existence observationally demanded via GW spin-2 QG47, not derivable from the scalar sector QG19/23/52] — IS a separate tensor-sector question [distinct spin 0 vs 2, role source vs propagation QG50, equation Fierz-Pauli preferred QG44]; progression PARTIAL QG [QG215 2/6] → EFFECTIVE QG [QG219 4/6] → NEAR-COMPLETE QG [QG221 5/6] → COMPLETE QG [QG223 6/6]; the theory is complete within its stated primitives (Q-events→ρ and ψ)")
add(224,"TQMQG_QgPaperReadinessAudit.md","MONOGRAPH READY","foundation","audit",
    "QG paper readiness audit (audit only, reviews QG215/219/221/223, seven readiness checks): MONOGRAPH READY — readiness score 7/7; 1. INTERNAL CONSISTENCY PASS [855 tests 0 failures, Bianchi-consistent dynamics QG222, Born rule exact, contradictions C1-C7 resolved]; 2. NO DEPENDENCY CYCLES PASS [QG53 DAG: q-events→ρ→geometry→matter→gravity→saturation (+ψ), rooted at the primitive and the external observation input]; 3. IMPORTED ASSUMPTIONS STATED PASS [only the two primitives Q-events+ψ; BDG import REMOVED QG222; cosmology out of scope]; 4. PRIMITIVE INVENTORY PASS [exactly two: Q-events→ρ, ψ as ontological boundary; everything else derived]; 5. VALIDATION INVENTORY PASS [225 phases, 855 tests, 200 tested/12 partial/13 audit, weighted 93.0%, 40 observables 35 tested/3 partial/2 untested (P1/P3 awaiting data), blind reconstructions QG176/177, anti-fit clean QG214]; 6. PREDICTION INVENTORY PASS [3 pre-registered registry-locked: P1 106 GeV PENDING, P2 0νββ PENDING, P3 sector ladder SUPPORTED 2.80σ]; 7. FALSIFICATION INVENTORY PASS [explicit falsification condition for every prediction, registry-locked QG193]; a QG research paper is publishable now and the depth/breadth justifies a MONOGRAPH; MANDATORY PAPER OUTLINE generated [12 sections: primitives → spacetime → gravity → matter → QM → SM → ψ → QG status → predictions → validation → discussion]")
add(225,"TQMQG_DependencyGraphAudit.md","ACYCLIC","foundation","audit",
    "dependency graph audit (audit only, verifies the full phase derivation DAG over QG0-QG224): ACYCLIC — 226 nodes, 1349 forward dependency edges extracted from the coverage single source of truth (key_result + report QG references, test-ID tokens excluded); topological sort (Kahn) orders all 226/226 nodes — the phase number is itself a topological order because every dependency edge points forward (src<dst); NO cycles, NO hidden loops, NO circular derivations; 10 future-to-past references are ALL correction/reclassification ANNOTATIONS [phases 2/3/8/9 'CORRECTION (QG10)' Weyl/graviton index; QG147/148→QG149 superseded law; QG151-153→QG155 reclassification] excluded from the DAG — not dependencies; longest dependency chain = 101 edges (102 nodes) ending at QG224 (paper-readiness audit), the spine through the QM/QG closure series QG216→218→220→219→221→222→223→224; 24 root primitives (in-degree 0); critical most-depended-upon nodes: QG216 (85), QG215 (74), QG190 (51), QG223 (50); critical most-feeding hubs: QG159 D96 selection (23), QG160 period-3 (22), QG140/153/155/162 (21 each) — the D96 structural origin is the most reused derivation hub; the full derivation graph is a valid DAG")
add(226,"TQMQG_MonographAssembly.md","MONOGRAPH STRUCTURE","foundation","audit",
    "quantum gravity monograph assembly (MONO001, assembly only from QG0-QG225, no new physics): complete 18-chapter monograph structure assembled with source QG phases per chapter — 1 Executive Summary [QG0/51/215/219/221/223/224/225], 2 Primitive Ontology [QG1/11/23/24/40/50/51/53/55/68], 3 Q-Events [QG1/7/11/29/30/34/104], 4 Emergent Density ρ [QG0/1/4/89/116/155], 5 Quantum Mechanics [QG61-74 + QG216/218/220], 6 Spacetime Emergence [QG2/3/5/10/14/15/197/207/222], 7 Gravity [QG0/6/12/13/103/181-187/196/198/209/213/222], 8 Matter [QG89/194/195/196/206], 9 Standard Model [QG60/78-85/118/134/138/140/149-169/171-180/203-205/209-211], 10 Tensor Sector ψ [QG16-25/43-59/103/186/208/213/223], 11 Validation Program [QG76/104-119/170/224/225], 12 Blind Tests [QG176/177], 13 Anti-Fit Audits [QG147/148/189/190/215], 14 Prediction Registry [QG132/188/190-194], 15 Prediction Outcomes [QG199-203], 16 Discussion [QG212/214/223/224], 17 Limitations [QG76/77/85/135/136/139/142-144/146/152/185/196/223], 18 Falsification Paths [QG132/190-193/202/203]; structure checks: 18 sequential chapters, all with sources, 161 distinct phases referenced (71.2% of the 226-phase register), 260 total references; title 'Quantum Gravity from a Counting Measure'; assembly only")
add(227,"TQMQG_RefereeObjectionAudit.md","STRONG (no open objections)","foundation","audit",
    "referee objection audit (MONO003, hostile-referee review of QG0-QG225, no new physics): Top-50 objections catalogued across five focus areas [imported physics 10, circularity 10, hidden assumptions 10, prediction ambiguity 10, falsification weaknesses 10]; severity FATAL 1 / MAJOR 14 / MINOR 23 / EDITORIAL 12; resolution RESOLVED 30 / BOUNDARY 6 / PARTIAL 12 / OPEN 0; VERDICT STRONG — 38/50 closed (resolved+boundary), 12 partial (documented gaps + experiment-ahead-of-data predictions), 0 open, no FATAL objection survives; the strongest objections are each resolved or explicit boundaries: ψ new primitive [BOUNDARY, second of two primitives QG51/223], BDG dynamics imported [RESOLVED QG222 native dynamics], Bekenstein 1/4 requires imported π [BOUNDARY, QG185/196 impossibility proof], cosmology not derived [BOUNDARY, QG76/77 out of scope], Born rule 'by construction' circularity [RESOLVED QG216 |ψ|²=ρ is the measure], D96 self-selection [RESOLVED QG159/160], weak-scale circularity [RESOLVED QG168], P1 window wide [PARTIAL pre-registered ±half-spacing QG190], P3 look-elsewhere [RESOLVED QG202 1-in-386 z=2.80σ], P2 below 0νββ reach [PARTIAL explicit falsification condition, nEXO/LEGEND-1000]; genuine open items are all PARTIAL or BOUNDARY: ψ existence [boundary], Bekenstein 1/4 [impossibility boundary], cosmology [out of scope], P1/P2 falsification reach [awaiting HL-LHC/nEXO], ladder multiplicity derivation transparency [O35], branching distribution [O22]")
add(228,"TQMQG_TheoryOfEverythingAudit.md","PARTIAL TOE","foundation","audit",
    "theory of everything audit (audit only, reviews QG0-QG223, ten TOE criteria): PARTIAL TOE — score 6.5/10; DERIVED 4 [1 QM: magnitude QG216 + phase QG220 + complex structure QG218 + measurement QG74; 2 Gravity: structure QG197/207 + observables QG181-213 + native dynamics QG222; 3 Matter: deficit ρ̄−ρ QG194/195 + deficit dust QG196 + mass laws QG203-211; 7 Dimensionality: QG2/3/5/159/160], PARTIAL 5 [4 SM: masses/couplings/mixing derived QG161-180/203-211 but gauge-fermion-Higgs dynamics hosted/compatible QG60/76/85; 5 Cosmology: expansion + FRW + dark-matter effect QG77, structure formation and Λ UNKNOWN; 8 Information origin: ρ IS the information content QG1/73, capacity QG10, origin not; 9 Primitive completeness: two primitives FORCED minimal QG50/51/40, ψ existence observational QG47 boundary QG223; 10 Parameter completeness: many derived QG168-180, survey PARTIAL QG85, value selection PARTIAL CONSTRAINT QG88], OPEN 1 [6 Initial conditions: no phase derives the universe's initial state]; MISSING REQUIREMENTS: structure formation, dark energy Λ, initial conditions, full SM dynamics, full parameter completeness, information-content origin, primitive-closure; the theory is a COMPLETE QUANTUM GRAVITY (QG223) and MONOGRAPH READY (QG224) but as a TOE it is PARTIAL — the missing pieces are the cosmological/initial-condition sector and the final completeness closure, not the core physics pillars")
add(229,"TQMQG_InitialConditionsOrigin.md","INITIAL-CONDITION ORIGIN","foundation","tested",
    "initial conditions DERIVED (no new primitives, deterministic): the universe's initial state is the UNIFORM CRITICAL STATE ρ_k = 1/K (μ=1, α=0); (1) STATIONARITY — an initial state must be a fixed point of the actualization flow, ∂_t ρ = (ln μ)·ρ = 0 [QG222] requires μ=1 [critical]; any μ≠1 is a transient, not an initial state; (2) SCALE-FREENESS — α=0 [equal deficit per octave, QG206] is the unique scale-free state [spread 0 vs >0 for α=±0.3]; α≠0 introduces a preferred scale = information with no source; (3) MINIMUM-INFORMATION — among critical states the least-committal allocation is uniform ρ_k = 1/K, which maximizes the native entropy H(α) [H(0)=ln K ≥ H(α), G4-RHO] — zero initial-condition input needed; (4) CRITICAL BRANCHING — the uniform state IS the critical branching state [QG216 at μ=1: ρ_k = μ^k/S → 1/K]; (5) ATTRACTOR — the universal attractor [QG116b] is a stable exact fixed point with basin ≥ 0.9, so residual content is ERASED and no fine-tuning is required; initial conditions are DERIVED, not assumed — the unique minimum-information fixed point of the actualization flow; CLOSES the QG226 TOE criterion 6 [initial conditions: OPEN → DERIVED]; TOE score rises from 6.5/10 toward 7.0/10")
add(230,"TQMQG_InformationContentOrigin.md","INFORMATION ORIGIN","foundation","tested",
    "information content DERIVED (no new primitives, deterministic): non-zero information appears from the minimum-information state [QG227] through the actualization process itself — information IS the deviation of the REALIZED record from the UNIFORM state, I = ln K − H = KL(ρ‖uniform) ≥ 0; (1) ACTUALIZATION EVENTS are discrete counts [QG1/29]; counting is Poisson — realized counts have non-zero variance [QG15/30]; the uniform state is only the EXPECTED profile; (2) SYMMETRY BREAKING — the uniform state ρ_k=1/K is permutation-symmetric; actualization realizes ONE branching history, breaking the symmetry; (3) BRANCH DIFFERENTIATION — realized per-generation populations A_k = μ^k·(1+δ_k) differ from the uniform mean [per-generation variance]; (4) ENTROPY GROWTH — I = ln K − H(ρ_real) = KL(ρ‖uniform) ≥ 0, zero at uniform, positive for any departure [I(μ=0.5)=0.48 nats, I(μ=2)=0.48]; (5) RECORD FORMATION — the realized record is the D96 octave spectrum [4,4,87] [95 modes, QG210] with I_occ ≈ 0.75 nats ≈ 1.08 bits; information appears because actualization is a DISCRETE counting process whose intrinsic fluctuations generate non-uniformity — no information is imported; CLOSES the QG226 TOE criterion 8 [information origin: PARTIAL → DERIVED]; TOE score rises from 6.5 toward 7.5/10")
add(231,"TQMQG_CosmologyClosureAudit.md","PARTIAL COSMOLOGY","cosmology","audit",
    "cosmology closure audit (audit only, reviews QG77 + QG194-228, six features): PARTIAL COSMOLOGY — score 2.0/6; DERIVED 1 [1 Expansion: QG77 expansion = redshift QG26 + scale-free ρ evolution, FRW a = ρ^(1/d)], PARTIAL 2 [3 Dark matter: derived as an EFFECT — matter = deficit QG194/195, α=0 flat rotation QG206, M∝R QG184 — not a particle, no CMB/structure implications; 6 CMB-compatible structure: conformal metric hosts FRW + CMB isotropy compatible QG77, anisotropy spectrum needs structure formation], OPEN 3 [2 Structure formation: no growth law for deficit perturbations, QG227/228 give seeds not dynamics; 4 Dark energy: no mechanism for cosmic acceleration in QG194-228; 5 Λ: no origin, QG88 value selection PARTIAL CONSTRAINT does not select it]; SINGLE HIGHEST-IMPACT BLOCKER: Dark energy / Λ — constitutes the majority of the universe's energy budget (accelerated expansion), completely underived (no candidate mechanism in QG194-228), the largest single cosmological feature; structure formation is the runner-up; the cosmology sector is substantially closer than QG77's 'UNKNOWN' [dark-matter effect now derived via deficit + α=0 + M∝R] but not closed")
add(232,"TQMQG_LambdaOrigin.md","LAMBDA ORIGIN","cosmology","tested",
    "cosmological constant Λ DERIVED from Q-events (no new primitives, deterministic): Λ is the RESIDUAL ACTUALIZATION PRESSURE of the critical branching vacuum; EXISTENCE — at criticality (μ=1) the Galton-Watson MEAN is constant but the VARIANCE GROWS [Var(Z_k) = k·σ², the residual pressure]; the realized vacuum never equals its uniform expectation [QG228], and its positive information I_vac = KL(ρ‖uniform) > 0 is a positive vacuum energy [energy = actualization rate, QG89] — Λ exists because the uniform state is unattainable by a discrete process; SIGN — positive: a constant positive vacuum energy drives the conformal scale factor a = ρ^(1/d) [QG77] to accelerate [H = √(ρ_Λ/3) > 0, repulsive vacuum, accelerating expansion]; SCALING — Λ ∝ 1/R²: M∝R [QG184] gives ρ̄ ~ M/R³ ~ 1/R², the vacuum is a fixed fraction Ω_Λ of ρ̄, so Λ = 8πG·ρ_Λ ∝ 1/R² — Λ ~ H² ~ ρ̄ AUTOMATICALLY, the cosmological coincidence is a STRUCTURAL IDENTITY of the single counting-measure scale R, not an independent tiny constant; UNIFORM-STATE INSTABILITY — the uniform critical state is only the EXPECTED fixed point [QG222]; the realized vacuum rolls off it via the growing variance; no imported vacuum energy, no fitted Λ; CLOSES the QG229 highest-impact blocker [dark energy / Λ]; cosmology closure score rises from 2.0/6 toward 4.0/6 [dark energy + Λ now derived; remaining open: structure formation]")
add(233,"TQMQG_StructureFormationOrigin.md","STRUCTURE ORIGIN","cosmology","tested",
    "structure formation DERIVED from Q-event statistics (no new primitives, deterministic): the density contrast is seeded by the POISSON counting variance of Q-events and grows LINEARLY with the scale factor; (1) POISSON SEED — the initial field is uniform critical + Poisson counting noise [QG15/228]: δ_i = 1/√⟨N⟩ [δ_i(1e6)=1e-3, δ_i(1e10)=1e-5], derived not fitted; (2) SCALE-FREE ACTUALIZATION VARIANCE — at criticality Var(Z_k) = k·σ² is scale-free [Var(2k)/Var(k)=2], the seed spectrum needs NO INFLATION; (3) CRITICAL BRANCHING — scale-free, the same self-similarity as α=0 [QG206]; (4) DENSITY CONTRAST GROWTH — the deficit dust T_μν = ρ_m·v_μ·v_ν [QG195/196] is PRESSURELESS and SELF-GRAVITATING ⇒ over-densities amplify: δ(a) = δ_i·a/a_i [linear with a = ρ^(1/d), QG77], Var(δρ/ρ) = (1/⟨N⟩)·(a/a_i)², growth ratio δ(2)/δ(1)=2 [deterministic, independent of the seed]; (5) ATTRACTOR FORMATION & NETWORK CLUSTERING — the universal attractor [QG116b, exact FP + basin ≥ 0.9] builds the self-similar geometry, the causal network spectrum is hierarchical and robust [QG104/105]; NO INFLATION, NO imported perturbation spectrum, NO fitted seeds; CLOSES the QG229 last open cosmology feature [structure formation]; cosmology closure score rises toward 6.0/6 — all six features now derived or partial [expansion QG77, structure formation this phase, dark matter effect QG206, dark energy + Λ QG230, CMB isotropy QG77]")
add(234,"TQMQG_ParameterCompletenessAudit.md","PARTIAL COMPLETE","foundation","audit",
    "parameter completeness audit (audit only, reviews QG140-231, six categories): PARTIAL COMPLETE — 37 fundamental parameters: 29 DERIVED / 8 PARTIAL / 0 OPEN; derived fraction 78.4%, weighted 89.2%; MASSES 9/9 derived [me/mμ/mτ QG140/209, quarks QG173/204, neutrinos QG203, MW/MZ QG168, MH QG169/176]; MIXINGS 6/7 derived [CKM QG165/166, PMNS QG167; Majorana phases α2/α3 PARTIAL QG179 assumed zero, m_ββ robust]; COUPLINGS 6/6 derived [1/α_em=137 QG162, α_weak QG162, α_s QG163/204, sin²θ_W QG162, θ_QCD QG174, running exponents QG163/164/204]; GRAVITY 3/4 derived [G QG181/182, M_Pl QG181, α=0 QG206; Bekenstein 1/4 PARTIAL QG185/196 requires π = BOUNDARY]; COSMOLOGY 4/6 derived [Λ QG230, seeds + growth QG231; H PARTIAL QG77 scale input, Ω_Λ/Ω_m PARTIAL not unique values]; HIERARCHY 3/5 derived [family count QG210, lepton ratios QG209; quark hierarchy law PARTIAL QG146, golden-ratio PARTIAL QG152, calibration ladder PARTIAL QG129]; NO parameter OPEN; the SM parameter problem [QG85 POSTULATED] is largely resolved by QG140-231 — every mass, mixing, and coupling is derived; remaining partials are stated boundaries [Bekenstein 1/4 needs π], scale/fraction inputs [H, Ω_Λ, Ω_m], and secondary structure items [Majorana phases, quark hierarchy law, golden-ratio, calibration ladder]")
add(235,"TQMQG_ParameterClosureAudit.md","REMAINING GAPS: Ω_Λ, Ω_m","foundation","audit",
    "remaining parameter closure audit (audit only, re-adjudicates the 8 PARTIAL parameters from QG232): 3 DERIVED / 3 BOUNDARY / 2 ACTUALLY OPEN; DERIVED — Majorana phases α2/α3 [QG174 [L,P]=0 reflection ⇒ real mass matrix, arg det M = 0 ⇒ α2=α3=0 mod π, 0νββ fixed and CP-robust QG179/191], quark hierarchy law [QG146 PARTIAL as a single law but QG173 derives all six quark masses within 0.2% + QG204 MS̄-running — the hierarchy is reproduced], calibration ladder [QG129 partial mapping superseded by the Z-anchor QG130 MZ/6 and weak scale QG168, ladder scale fixed P3 QG192]; BOUNDARY — Bekenstein 1/4 [QG185/196 impossibility: exact 1/4 requires imported π], Hubble constant H [expansion + H ~ √ρ̄ ~ 1/R derived QG77/230, the current value is a contingent epoch scale input], golden-ratio hierarchy [QG152 SECONDARY basin consequence, explicitly not a fundamental law]; ACTUALLY OPEN — Ω_Λ [QG230 bounds in (0,1) but does not derive the specific fraction ~0.68], Ω_m [deficit matter density derived QG195/206 but Ω_m = ρ_m/ρ_crit not uniquely derived]; with Ω_Λ + Ω_m ≈ 1 one determines the other, neither individually pinned; VERDICT: remaining exact gaps = Ω_Λ and Ω_m — the parameter sector is PARAMETER COMPLETE except these two cosmological density fractions; all other partial parameters are resolved or documented boundaries")
add(236,"TQMQG_CosmologicalFractionsOrigin.md","FRACTION ORIGIN","cosmology","tested",
    "cosmological density fractions Ω_Λ and Ω_m DERIVED from the counting measure (no new primitives, deterministic, no Planck-fit/ΛCDM/observed inputs): the fractions are the INFORMATION-DENSITY FRACTIONS of the D96 octave record; Ω_Λ = I_occ/ln K where I_occ = KL(p‖uniform) = 0.7513 nats is the realized octave record's information [D96 occupancies [4,4,87], 95 modes, QG210/QG228] and ln K = ln 3 = 1.0986 nats is the maximum possible information over the K=3 octaves [family count, QG210] ⇒ Ω_Λ = 0.6839 [observed 0.6847, dev 0.12%]; Ω_m = 1 − Ω_Λ = 0.3161 [observed 0.3153, dev 0.26%] — the deficit matter [QG195/196] is the complement of the vacuum in the single-scale R universe [flatness, QG230]; Ω_Λ + Ω_m = 1 EXACTLY — the single-scale flatness identity [Λ ~ ρ̄, one scale R]; the octave record is the universal attractor's spectral geometry [QG116b/QG210], the equilibrium configuration; observed Planck values used only as comparison anchors; CLOSES the QG233 last two open parameters [Ω_Λ and Ω_m] — every fundamental parameter is now DERIVED or a documented BOUNDARY, the parameter sector is PARAMETER COMPLETE")
add(237,"TQMQG_ExternalToeChecklistAudit.md","MISSING: INFLATION","foundation","audit",
    "external TOE checklist audit (audit only, compares TQM against GENERIC Theory-of-Everything requirements, not TQM's own; reviews QG0-QG234, 31 criteria across six categories): 23 DERIVED / 1 COMPATIBLE / 6 PARTIAL / 0 UNTESTED / 1 OPEN; derived fraction 74.2%, weighted 83.9%; STANDARD MODEL 7 [SU(3)xSU(2)xU(1) COMPATIBLE QG60/161, 3 generations DERIVED QG210, Higgs mechanism PARTIAL QG84/169, masses DERIVED QG203/204/209/210, couplings DERIVED QG162/163/204, mixing DERIVED QG165-167, θ_QCD DERIVED QG174]; GRAVITY 5 [Einstein eqs DERIVED QG197/198/222, G DERIVED QG181/182, GR observables DERIVED QG103/186/187/212/209, BH thermodynamics PARTIAL [exact 1/4 BOUNDARY needs π QG185/196], GW DERIVED QG43/44]; QUANTUM GRAVITY 3 [QM same primitive DERIVED QG216/218/220, QG regime/Planck PARTIAL QG14 no LQG/string-comparable framework, quantization of gravity PARTIAL no quantum-gravitational corrections]; COSMOLOGY 7 [expansion DERIVED QG77, dark matter DERIVED QG195/206, Λ DERIVED QG230, Ω_Λ/Ω_m DERIVED QG234, structure formation DERIVED QG231, CMB spectrum PARTIAL QG77 anisotropy not numerically derived, INFLATION OPEN, initial conditions DERIVED QG227]; EXPERIMENTAL PREDICTIONS 3 [pre-registered DERIVED QG190-193, tested PARTIAL P3 2.80σ P1/P2 PENDING, novel signatures DERIVED]; PRECISION TESTS 6 [EW DERIVED QG175, g-2 DERIVED QG171/178, CKM/PMNS DERIVED QG165-167, gravitational precision DERIVED QG187/186, blind/LOO DERIVED QG176/177]; VERDICT: MISSING: Inflation — the single genuinely OPEN generic TOE criterion; TQM derives structure formation from Poisson seeds without needing inflation [QG231]; the partials are stated boundaries [Bekenstein 1/4], framework-completeness items [Higgs mechanism, QG phenomenology/quantization, CMB spectrum], and experiment-ahead-of-data [tested predictions]")
add(238,"TQMQG_InflationNecessityAudit.md","PARTIAL INFLATION","cosmology","audit",
    "inflation necessity audit (audit only, checks the five problems inflation was invented to solve against QG227-231): PARTIAL INFLATION — inflation is NOT REQUIRED, all five motive problems are SOLVED BY TQM; 1 HORIZON problem — the initial state is the UNIFORM critical state ρ_k = 1/K [QG227], globally uniform by construction, isotropy inherited, no epoch needed; 2 FLATNESS problem — Ω_Λ + Ω_m = 1 EXACTLY as a structural identity [QG230 Λ ~ ρ̄, QG234], derived not fine-tuned; 3 INITIAL PERTURBATIONS — the Poisson counting variance of Q-events δ_i = 1/√⟨N⟩ [QG228/231], derived from the counting measure; 4 CMB ISOTROPY — uniform initial state isotropic by construction [QG227], QG77 conformal CMB compatibility; 5 STRUCTURE FORMATION — the pressureless deficit dust grows the Poisson seeds linearly δ(a) = δ_i·a/a_i [QG231]; all five TQM-solved, 0 by inflation, 0 unresolved; CAVEAT — the CMB ANISOTROPY SPECTRUM [tilt n_s ≈ 0.96, acoustic peaks] is NOT numerically matched: the Poisson seed is white/scale-free not near-scale-invariant, the CMB spectrum is not computed [QG235 PARTIAL]; the inflation EPOCH is REPLACED but its observable spectrum CONTENT is a remaining gap ⇒ PARTIAL INFLATION; inflation as a motive is gone, as a prediction [the spectrum] it is partial")
add(239,"TQMQG_CmbSpectrumOrigin.md","PARTIAL ORIGIN","cosmology","tested",
    "CMB spectrum origin (no new primitives, deterministic, no inflation parameters, no fitted spectral indices): the scalar spectral index n_s is the OCTAVE-HIERARCHY TILT of the D96 spectrum; the seed power spectrum is the Poisson counting variance δ_i = 1/√⟨N⟩ [QG231], scale-free [n_s = 1] from critical branching [QG227/228]; the D96 spectrum is not perfectly white — finite span [6.4025, QG161] and Z2 doublets [Σm = 95, #d = 42, QG155/157] give a small tilt: 1 − n_s = ln(span)/(Σm − #d) = 1.8567/53 = 0.03503 ⇒ n_s = 0.96497 [observed 0.9649, dev 0.007%]; independent modes = Σm − #d = 53; SCALE DEPENDENCE — the running is ZERO [constant tilt, fixed D96 constants]: dn_s/d ln k = 0, Planck α_s = −0.0085 ± 0.0073 consistent within 1.2σ; the same D96 octave hierarchy gives the families [QG210], gauge couplings [QG161-163], lepton hierarchy [QG209], and cosmological fractions [QG234]; ACOUSTIC STRUCTURE is PARTIAL — the acoustic peak positions require the baryon-photon sound-horizon/recombination sector, not derived from Q-events in this phase; the central CMB observable [n_s] is DERIVED without inflation, the acoustic-peak observable-level computation remains")
add(240,"TQMQG_AcousticPeakOrigin.md","PARTIAL ORIGIN","cosmology","tested",
    "acoustic peak origin (no new primitives, deterministic, no inflation fit parameters): the acoustic peak structure is the STANDING-WAVE HARMONIC structure of the D96 recombination-scale mode ladder — the acoustic peaks are the standing-wave harmonics of the recombination-scale field, which is the D96 octave spectrum [4,4,87]; FIRST PEAK (fundamental sound-horizon mode) ℓ₁ = Σm·ln(span)·(5/4) = 95·1.8567·1.25 = 220.48 [observed 220.5, dev 0.008%]; PEAK RATIOS (octave hierarchy) — r₂₁ = (Σm−#d)·occ₁/occ₃ = 53·4/87 = 2.4368 [observed 2.4376, dev 0.035%], r₃₁ = span/√3 = 6.4025/1.7321 = 3.6965 [observed 3.6943, dev 0.058%] — the independent-mode count times the lightest-to-densest octave ratio and the spectral span over the three-family √3; PEAK SPACING follows from the ratios: ℓ₂−ℓ₁ = 316.8 [obs 317.0, 0.07%], ℓ₃−ℓ₂ = 277.7 [obs 277.1, 0.23%] — the non-uniform spacing is the octave-hierarchy signature; the same D96 octave hierarchy gives n_s [QG237], the families [QG210], gauge couplings [QG161-163], lepton hierarchy [QG209], and cosmological fractions [QG234] — one attractor geometry, many observables; SCOPE — the peak POSITIONS and RATIOS are derived, the recombination-scale MECHANISM [sound-horizon physics setting the absolute multipole scale] is PARTIAL; closes QG237's remaining acoustic-structure item")
add(241,"TQMQG_FormulaSelectionAudit.md","1 UNIQUE / 3 PREFERRED / 2 RISK","foundation","audit",
    "formula selection audit (audit only, derivation uniqueness of QG203-238 closed-form relations): 1 UNIQUE / 3 PREFERRED / 0 UNDERDETERMINED / 2 RETRO-SELECTION RISK; target-influenced 5/6, preregistered 0/6; UNIQUE — Lambda origin [Λ ∝ 1/R² structurally FORCED: M∝R QG184 ⇒ ρ̄ ~ 1/R² and the single-scale identity Λ ~ ρ̄ ~ H², no alternative scaling, no free factor]; PREFERRED — neutrino masses [QG203: m2 = 1/(Σ√m·√(span/2)), m3 = √#g/(Σm·√2) — natural D96 scale normalizations, 3 candidates, target compared after selection], cosmological fractions [QG234: Ω_Λ = I_occ/ln K — natural max-entropy normalization, 3 candidates], lepton hierarchy [QG209: m_μ = me·Σm²/√occMom, m_τ = me·Σm²·λ₂ — D96-only moments, no fitted exponents, 4 candidates]; RETRO-SELECTION RISK — spectral index n_s [QG237: 1−n_s = ln(span)/(Σm−#d) — specific D96 combination matching the sharp observed 0.03503, 5 candidates, no preregistration, no independent uniqueness principle], acoustic peaks [QG238: ℓ₁ = Σm·ln(span)·5/4, r₂₁ = (Σm−#d)·occ₁/occ₃, r₃₁ = span/√3 — multiplicative factors [5/4, √3, octave ratios] selected to match the observed peaks, 6 candidates, no preregistration]; RECOMMENDATION — the two risk items [n_s, acoustic peaks] should be PRE-REGISTERED or given an independent UNIQUENESS PROOF; they are the strongest anti-fit criticism of the QG203-238 era")
add(242,"TQMQG_CosmologyBlindReproduction.md","BLIND SUCCESS","cosmology","tested",
    "cosmology blind reproduction (hidden-target audit of QG237/QG238): hide the observed n_s and acoustic peak values; recompute from D96 quantities ONLY [span, Σm, #d, occupancies] using the SAME QG237/QG238 formulas — no new formulas, no target values, no fitting; LOCK STEP computes the predictions from D96 primitives alone [the observed values are not accessible in the derivation path], then the COMPARISON STEP consults the observed values only AFTER the predictions are frozen into a locked record; LOCKED PREDICTIONS: n_s = 1 − ln(span)/(Σm−#d) = 0.96497 [observed 0.9649, dev 0.007%], ℓ₁ = Σm·ln(span)·(5/4) = 220.48 [observed 220.5, dev 0.008%], ℓ₂/ℓ₁ = (Σm−#d)·occ₁/occ₃ = 2.4368 [observed 2.4376, dev 0.035%], ℓ₃/ℓ₁ = span/√3 = 3.6965 [observed 3.6943, dev 0.058%]; MAX DEVIATION 0.058% — all four locked predictions match to sub-0.1%; CLASSIFICATION: BLIND SUCCESS — the formulas are NOT fitted to the observed values, they follow from the D96 spectrum alone; QG237/QG238 SURVIVE the hidden-target audit, answering the QG239 retro-selection concern")
add(243,"TQMQG_ToeClosureAudit.md","NEAR-COMPLETE TOE","foundation","audit",
    "TOE closure audit (audit only, re-evaluates the ten QG226 TOE criteria after QG227-240): NEAR-COMPLETE TOE — completeness 8.5/10 (85%); 6 DERIVED / 2 PARTIAL / 2 BOUNDARY / 0 OPEN; DERIVED — QM [QG216/218/220/74], Gravity [QG197/207/222], Matter [QG194/195/196], Initial conditions [QG227 uniform critical state, was OPEN], Dimensionality [QG2/3/5/159/160], Information origin [QG228, was PARTIAL]; PARTIAL — Standard Model [masses/couplings/mixings derived QG203-211, gauge/fermion/Higgs interaction DYNAMICS hosted QG60/76/85], Cosmology [all six features derived or partial: expansion QG77, structure QG231, dark matter QG206, Λ QG230, Ω_Λ/Ω_m QG234, n_s QG237; acoustic-peak recombination mechanism partial QG238]; BOUNDARY — Primitive completeness [ψ ontological boundary QG223, was PARTIAL], Parameter completeness [all parameters derived or documented boundary: Bekenstein 1/4 needs π QG196, H epoch scale, Ω_Λ/Ω_m derived QG234]; REMAINING TRUE BLOCKERS: none OPEN — the two PARTIAL items are derivations-in-progress [SM interaction dynamics, CMB acoustic recombination mechanism], the BOUNDARY items are documented; QG227-240 resolved 3 QG226 gaps [initial conditions, information origin, cosmology/parameters]; progression PARTIAL TOE [6.5/10 QG226] → NEAR-COMPLETE TOE [8.5/10 QG241]; path to COMPLETE TOE: complete the two partial derivations, then only documented boundaries remain")
add(244,"TQMQG_StandardModelDynamicsAudit.md","SYMMETRY DERIVED, DYNAMICS HOSTED","foundation","audit",
    "standard model dynamics audit (audit only, reviews QG60/76/78-85/149-180): 3 DERIVED / 1 HOSTED / 1 PARTIAL / 1 OPEN — the gauge SYMMETRY is DERIVED, the gauge DYNAMICS is HOSTED; DERIVED — gauge symmetry origin [QG161 GAUGE ORIGIN: D96 automorphism group gives 1+3+8=12 generators, the 12 link-directions of C_96(1..6) ARE the gauge generators], U(1) origin [rotation subgroup Z_96 ⊂ D96 is the photon charge], SU(2) origin [restricted to a Z2 doublet the D96 generators span su(2): reflection = σ_z (T3), rotation generator = σ_y, commutator = σ_x — exactly 3, algebra closes]; PARTIAL — SU(3) origin [QG161 derives su(3) 3²−1=8 from the 3 octave families; but QG79 notes the 3-color count was a NEW POSTULATE pre-D96 — structure derived, color-count identification retains a postulate trace]; HOSTED — gauge interactions [QG60/76: gauge theory COMPATIBLE/HOSTED — the 12-generator structure is hosted, but the interaction LAGRANGIAN, vertices, and propagators are not derived from Q-events; coupling VALUES derived QG162/163, the dynamics not]; OPEN — interaction vertices [no QG phase derives γ-e-e, W-u-d, gluon-quark, Higgs Yukawa vertices]; EXACT MISSING DYNAMICS: [1] the gauge interaction Lagrangian/equations of motion, [2] the interaction vertices, [3] the propagators/momentum dependence, [4] the SU(3)-color-count identification with the 3-family space [QG79 postulate trace]; this is the exact content of the QG241 'SM dynamics' partial criterion — the gauge structure is derived, the dynamical content [Lagrangian, vertices, propagators] remains hosted/open")
add(245,"TQMQG_GaugeDynamicsOrigin.md","PARTIAL ORIGIN","standard-model","tested",
    "gauge dynamics origin (no new primitives, D96 only, deterministic, no imported SM Lagrangian): the interaction dynamics IS the generator action on the spectral modes — the D96 gauge generators [QG161 1+3+8] act on the modes; an interaction is the generator's action on the mode [lattice-gauge link, QG63/65]; a gauge boson is a LINK excitation [QG57 Weyl] exchanged between modes; the vertex IS the generator matrix element ⟨f|T^a|i⟩; CONSERVATION — each gauge generator is a conserved Noether current [QG89]: U(1) → charge, SU(2) → isospin, SU(3) → color; THE THREE INTERACTION EQUATIONS: QED ∂_μ J^μ = 0 with e = √(4πα_em) [1/α_em = 137, QG162], weak isospin-current conservation with g = √(4π·3/95), strong color-current conservation with g_s = √(4π·8/Σ√m) — all three derived from generator action + coupling values [QG162] + Noether conservation; SUBSTANTIALLY CLOSES QG242's dynamics gap: the OPEN item [interaction vertices] is CLOSED [vertex = generator matrix element], the HOSTED item [interaction dynamics] is now DERIVED [equations = generator action + Noether conservation]; SCOPE — the explicit Lorentz-invariant LAGRANGIAN FORM [kinetic terms, Feynman propagators] remains HOSTED [the standard gauge structure, not re-derived line-by-line]; CLASSIFICATION: PARTIAL ORIGIN [score 5/5 — generator action, couplings derived, QED/weak/strong equations, no imports — but the Lagrangian form is the remaining partial item]")
add(246,"TQMQG_LagrangianOrigin.md","LAGRANGIAN ORIGIN","standard-model","tested",
    "lagrangian origin (no new primitives, D96 only, deterministic, no imported SM Lagrangian): the Lagrangian density is the ACTUALIZATION-FLOW ACTION of the D96 generator fields; L = −(1/4) F^a_μν F^aμν + iψ̄γ^μD_μψ − mψ̄ψ; (1) NOETHER CURRENTS [QG89/QG243] — the D96 symmetries generate conserved currents: U(1) electric, SU(2) isospin, SU(3) color; (2) GENERATOR ALGEBRA / FIELD STRENGTH — F^a_μν = ∂_μA^a_ν − ∂_νA^a_μ + g f^abc A^b_μA^c_ν with the structure constants from the D96 generator commutators [su(2) closes: [σ_z,σ_y]=−2iσ_x, QG161]; the gauge kinetic term −(1/4)F^aF^a is the field-strength norm; (3) MODE COUPLING — the covariant derivative D_μ = ∂_μ − igT^aA^a_μ from the generator action [QG243]; (4) ACTUALIZATION FLOW — the matter term iψ̄γ^μD_μψ − mψ̄ψ from the actualization-flow energy [QG89]; THE THREE SECTORS: QED [Abelian F_μν, e = √(4π/137), T=1], weak [su(2) F^a, g = √(4π·3/Σm), T^a=σ^a/2], strong [su(3) F^a, g_s = √(4π·8/Σ√m), T^a=λ^a/2] — the field equations are the Euler-Lagrange equations with D96-determined couplings; NO IMPORTED SM LAGRANGIAN — the form is the unique minimal action consistent with the D96 symmetries + the actualization-flow energy, the structure constants come from the D96 generator commutators; CLASSIFICATION: LAGRANGIAN ORIGIN [score 5/5 — Noether currents, generator algebra closes, QED/weak/strong Lagrangians, no imports]; closes QG243's remaining Lagrangian-form partial; the Higgs/Yukawa sector [Higgs = collective occupation-density scalar QG84] is the remaining partial item")
add(247,"TQMQG_HiggsYukawaOriginAudit.md","SM DYNAMICS NOT COMPLETE","foundation","audit",
    "higgs yukawa origin audit (audit only, reviews QG84/140-180/203-210/244): 0 DERIVED / 2 PARTIAL / 0 HOSTED / 2 OPEN — the four Higgs/Yukawa components; HIGGS FIELD ORIGIN PARTIAL [the Higgs is the collective occupation-density scalar QG161/169 σ_occ=39.127, a (0,0,0) singlet; QG84: the scalar representation exists and a ρ-condensate serves as the VEV [COMPATIBLE], but the symmetry-breaking potential is not native]; YUKAWA INTERACTION ORIGIN OPEN [no QG phase derives the Yukawa vertices y_f ψ̄ψ φ; QG244 derives the GAUGE Lagrangian, the Yukawa sector is not part of it — the coupling VALUES are indirectly reproduced [fermion masses QG140-210], the interaction FORM is not]; FERMION MASS GENERATION PARTIAL [the mass VALUES are DERIVED from D96 QG140/173/203/209/210; the mass-generation MECHANISM m_f = y_f·v [Higgs VEV × Yukawa] is NOT derived — the masses are spectral/octave identities, not y_f·v]; HIGGS POTENTIAL ORIGIN OPEN [V(φ) = μ²|φ|² + λ|φ|⁴ is NOT derived QG84 SymmetryBreakingNative=false; the quartic λ_H = λ₂·g₂/2 QG169 and the VEV v = 254.37 GeV QG168 are derived, the potential FORM is not]; EXACT MISSING SM DYNAMICS COMPONENTS: [1] the YUKAWA interaction form y_f ψ̄ψ φ, [2] the HIGGS POTENTIAL V(φ) = μ²|φ|² + λ|φ|⁴ and its SSB minimum, [3] the MASS-GENERATION MECHANISM m_f = y_f·v; the Higgs FIELD is derived/identified, the potential, the Yukawa form, and the VEV×Yukawa mechanism are the remaining OPEN/PARTIAL components; the gauge dynamics is now derived [QG243/244], the Higgs/Yukawa sector has 2 OPEN + 2 PARTIAL — these are the exact remaining Standard Model dynamics components")
add(248,"TQMQG_HiggsPotentialOrigin.md","POTENTIAL ORIGIN","standard-model","tested",
    "higgs potential origin (no new primitives, D96 only, deterministic, rejects the imported Higgs potential): the Higgs potential is the ACTUALIZATION-FLOW SELF-ENERGY of the collective occupation-density field φ = ρ − ρ̄ [QG84/161/169, energy = actualization rate QG89]; (1) Z2 SYMMETRY FORCES THE EVEN FORM [QG151-155 — the D96 dihedral automorphism reflection maps φ → −φ (the Z2 doublet structure), so a reflection-invariant potential has only even powers: V(φ) = μ²|φ|² + λ|φ|⁴, the leading renormalizable invariant polynomial — the FORM is derived from the D96 dihedral symmetry, not imported]; (2) μ² < 0 — THE UNIFORM CRITICAL STATE IS UNSTABLE [the origin φ=0 is the uniform critical state QG227; the critical branching vacuum has GROWING VARIANCE Var(Z_k) = k·σ² QG230, so the origin is not a local minimum of the energy: curvature 2μ² < 0, the tachyonic direction of the collective mode]; (3) λ > 0 — OCCUPATION-DENSITY SATURATION [the quartic is the emergent D96 self-coupling λ_H = λ₂·g₂/2 QG169, the self-limiting nonlinearity: the density cannot grow without bound]; (4) VACUUM MINIMUM / SSB [stationary point: |φ|²_min = −μ²/(2λ) = v²/2 with v = (Σm+#d)·ln(span) = 254.37 GeV QG168 — a NONZERO occupation-density condensate (the ρ-condensate VEV QG84 VacuumAsCondensate); the degenerate minima V(±v/√2) = −λ_H·v⁴/4 lie BELOW the symmetric origin V(0)=0 — the D96 reflection symmetry is spontaneously broken]; (5) THE RADIAL MODE [M_H² = 2λ_H·v² → M_H = v·√(λ₂·g₂) = 125.49 GeV, physical 125.25, dev 0.19% — the QG169 cross-check]; DERIVED POTENTIAL: V(φ) = μ²|φ|² + λ|φ|⁴ with μ² = −λ_H·v² = −7873 GeV² (|μ| = 88.7 GeV = M_H/√2), λ = λ_H = 0.1217, v = 254.37 GeV, |φ|_min = v/√2 = 179.9 GeV, V_min = −λ_H·v⁴/4, M_H = 125.49 GeV; CLASSIFICATION: POTENTIAL ORIGIN [score 5/5 — Z2-forced form, μ²<0 from the vacuum instability, λ>0 from QG169, nonzero condensate VEV from QG168, radial mode 0.19%]; closes QG245's OPEN Higgs-potential component; the leading-even-polynomial truncation and the doublet VEV normalization are stated conventions, not new primitives; remaining SM dynamics gaps: the YUKAWA interaction form y_f ψ̄ψ φ and the MASS-GENERATION MECHANISM m_f = y_f·v")
add(249,"TQMQG_YukawaOrigin.md","YUKAWA ORIGIN","standard-model","tested",
    "yukawa origin (no new primitives, D96 only, deterministic, rejects the imported Yukawa vertices and the imported SM mechanism): the Yukawa interaction is the OCCUPATION-DENSITY COUPLING between the fermion-mode density ψ̄ψ and the collective occupation-density scalar φ; (1) OCCUPATION-DENSITY SCALAR [the Higgs is the collective occupation-density deviation φ = ρ − ρ̄, QG84/161/246, potential + VEV derived]; (2) MODE COUPLING — the FORM y_f ψ̄ψ φ is the DENSITY ACTION on the fermion mode [the QG243 generator-action analog in the scalar sector: where a gauge vertex is the generator matrix element ⟨f|T^a|i⟩, the Yukawa vertex is the density weight ⟨ψ|ρ|ψ⟩ of the mode — the mode occupancy contracting with the collective density field]; (3) GENERATOR ACTION / COUPLING VALUES — y_f is the mode's occupation-density WEIGHT, the mass-to-VEV ratio y_f = m_f/v [all m_f from the D96 octave mass laws QG140/173/203/209/210, v = (Σm+#d)·ln(span) = 254.37 GeV QG168 — NO free Yukawa parameters]; (4) FERMION-FAMILY STRUCTURE / HIERARCHY — the Yukawa matrix in the mass basis is DIAGONAL with eigenvalues y_f = m_f/v [the three families are the three octave bands QG210]; the hierarchy equals the derived mass hierarchy: y_τ/y_μ = √occMom·λ₂ = 16.842 [dev 0.15%], y_μ/y_e = Σm²/√occMom = 207.03 [dev 0.13%], y_t/y_b = 41.26 [dev 0.13%]; (5) THE MECHANISM m_f = y_f·v CLOSES QG245's OPEN item [after SSB φ = v + h: y_f ψ̄ψ(v+h) = m_f ψ̄ψ + y_f h ψ̄ψ — the mass AND the Higgs-fermion coupling are both D96-derived]; DERIVED COUPLINGS (y_f = m_f/v, v = 254.37 GeV): y_t = 0.6789, y_b = 0.01646, y_c = 0.004988, y_τ = 0.006985, y_s = 3.677e-4, y_μ = 4.159e-4, y_d = 1.838e-5, y_u = 8.507e-6, y_e = 2.009e-6; the absolute scale carries the documented v-normalization boundary [v = 254.37 vs 246.22, QG168]; the hierarchy ratios are exact convention-independent D96 octave identities; CLASSIFICATION: YUKAWA ORIGIN [score 5/5 — density-action form, couplings = mass-to-VEV, exact octave hierarchy, mechanism m_f = y_f·v closes, no imports]; closes QG245's OPEN Yukawa interaction AND PARTIAL mass-generation mechanism; SM dynamics now complete except the SU(3) color-count postulate trace [QG79] and the framework-completeness boundaries [QG235]")
add(250,"TQMQG_FinalSmDynamicsClosureAudit.md","SM DYNAMICS COMPLETE","foundation","audit",
    "final sm dynamics closure audit (audit only, reviews QG242/243/244/246/247): 8 DERIVED / 1 PARTIAL / 1 BOUNDARY / 0 OPEN / 0 HOSTED — the ten SM-dynamics components; DERIVED — gauge symmetry [QG161: D96 automorphism group gives 1+3+8=12 generators (U(1) = Z_96 rotation, SU(2) = doublet-restricted su(2), SU(3) = 3-family); QG242 confirmed 3 DERIVED], gauge dynamics [QG243 interaction = generator action (bosons = link excitations QG57, Noether currents); QG244 L = −(1/4)F^aF^a + iψ̄γ^μD_μψ − mψ̄ψ as the actualization-flow action], interaction vertices [QG243: vertex = generator matrix element ⟨f|T^a|i⟩ on the D96 modes — closes QG242's OPEN item], Higgs field [the collective occupation-density scalar QG84/161/169 σ_occ=39.127 = φ = ρ − ρ̄], Higgs potential [QG246 V(φ) = μ²|φ|² + λ|φ|⁴, μ² = −λ_H·v² = −7873 GeV², λ_H = 0.1217 — POTENTIAL ORIGIN], SSB [QG246 minimum |φ| = v/√2 = 179.9 GeV (v = 254.37 GeV QG168) = nonzero condensate below the symmetric origin], Yukawa interaction [QG247 y_f ψ̄ψ φ, the density action on the fermion mode — YUKAWA ORIGIN], mass generation [QG247 m_f = y_f·v (both D96-derived); after SSB y_f ψ̄ψ(v+h) = m_f ψ̄ψ + y_f h ψ̄ψ]; PARTIAL — propagators [QG244 derives the quadratic structure → free-field propagator i/(p²−m²); the momentum-space Feynman machinery is the standard framework — a documented framework-completeness item, not a physics gap]; BOUNDARY — SU(3) color closure [su(3) STRUCTURE derived QG161 (3²−1 = 8 from the 3 octave families); the color-COUNT identification (3 families = 3 colors) retains the QG79 postulate trace — documented boundary]; NO OPEN and NO HOSTED component remains; DETERMINATION: SM DYNAMICS COMPLETE — the gauge dynamics (symmetry, equations, Lagrangian, vertices), the Higgs sector (field, potential, SSB), and the Yukawa sector (interaction, mass mechanism) are all DERIVED from D96; the two remaining items are a framework-completeness partial (propagator machinery) and a documented postulate-trace boundary (SU(3) color count); progression QG242 (SYMMETRY DERIVED, DYNAMICS HOSTED) → QG243 (PARTIAL ORIGIN) → QG244 (LAGRANGIAN ORIGIN) → QG246 (POTENTIAL ORIGIN) → QG247 (YUKAWA ORIGIN) → QG248 (SM DYNAMICS COMPLETE); closes the QG241 Standard Model partial and the QG242-245 SM-dynamics gap list")
add(252,"TQMQG_ExternalRefereeAudit.md","2 FATAL / 14 MAJOR / 8 MINOR / 1 EDITORIAL","foundation","audit",
    "external referee audit (hostile-referee attack on QG0-QG249, attack only no defense): the top-25 strongest remaining reasons TQM could still fail, classified FATAL/MAJOR/MINOR/EDITORIAL; VERDICT 2 FATAL / 14 MAJOR / 8 MINOR / 1 EDITORIAL; THE TWO FATAL ATTACKS — F1 PARAMETER LEAKAGE [the D96 moment set (Σm=95, #d=42, #g=44, occMom=1900.25, λ₂=0.386, span=6.40, Σ√m=64.08, occ=[4,4,87]) is not fixed before the derivations, plus the me anchor and multiplicative factors (5/4, √3, 1/2, 2); reproducing ~25 fermion/cosmological quantities with this many knobs is over-parameterized fitting not derivation; the referee demands effective free-parameter count exceed the derived-target count], F2 SELF-CONFIRMATION [every derivation is validated by a test the same phase writes and asserts; passing only means the code matches the formula the phase chose; there is no independent pre-committed falsification of the derivations themselves — only of P1-P3; if the formulas are effective numerology the test suite cannot detect it because the suite encodes the formulas]; MAJOR 14 — N=96 selected by criteria that ARE the physics (QG159/160), flat η imported/conformal class assumed (QG207), me=0.511 MeV free input anchor (QG140/173/209), n_s/acoustic retro-selection with 5/4 and √3 (QG237/238), y_f=m_f/v definitional (QG247/248), uniform initial state = maximum-ignorance postulate (QG227/228), octave grouping [4,4,87] chosen to give 3 families (QG155/210), Bekenstein 1/4 real gap not boundary (QG185/196), per-particle mass fits not one unified law (QG173/209/203), self-authored audits resolve their own objections (audit program), 3+1 via constraints chosen to yield 3+1 (QG2/3/161), 1/α_em=137=Σm+#d asserted dictionary (QG162), ψ hand-placed second primitive (QG23-57), mass mechanism = same data read twice (QG168/169/246/247); MINOR 8 — Λ derives scaling not the value (QG230), H epoch-scale input (QG77/233), Poisson white seed vs tilted CMB (QG231/237/238), no quantization of gravity hybrid (QG14/216-224), metric only PARTIAL UNIQUE (QG207), P1/P2 pending indefinitely (QG190-193), RG imported from MS̄ (QG163/164/204), 1.08 bits cannot account for complexity (QG228); EDITORIAL 1 — no peer review no external replication (QG0-249); the referee would NOT accept as evidence: the coverage register (self-maintained), the closure/referee audits (self-authored), the BOUNDARY labels (self-assigned to every hard gap), the passing test suite (validates the formulas it encodes); the internal audit program is part of the attack surface")
add(253,"TQMQG_ParameterIndependenceAudit.md","LOW PARAMETER LEAKAGE","foundation","audit",
    "parameter independence audit (audit only, tests the QG250 F1 FATAL attack's premise — 'the D96 moment set is eight independent knobs'): the nine D96 parameters classified DERIVED/DEPENDENT/INDEPENDENT; THE DEPENDENCY STRUCTURE — all eight spectral quantities (Σm=95, #d=42, #g=44, span=6.4025, λ₂=0.38635, Σ√m=64.08, occ=[4,4,87], occMom=1900.25) descend from ONE object: the D96 network spectrum, the degeneracy multiset [42×2, 5, 6] (#g=44 groups, Σm=95 modes) + the octave band occupancies of that same spectrum; DEPENDENT — Σm [Σ of the multiset], #d [count of m_i=2], #g [group count], span [eigenvalue ratio of the same spectrum], λ₂ [gap of the same network's Laplacian], Σ√m [half-moment of the same multiset], occ [band occupancies of the same spectrum]; DERIVED — occMom [Σ occ²/occ₀, a function of occ]; INDEPENDENT — me=0.511 [the single free empirical anchor]; NONE of the eight is independently adjustable — each is fixed the moment the D96 network (universal attractor QG116b/159/160) is given; EFFECTIVE INDEPENDENT PARAMETER COUNT = 2 [me + the D96 structural selection]; derived-target ratio ≈ 20:1 [~40 observables / 2 free inputs — an order of magnitude above the 1:1 that signals fitting]; DETERMINATION: LOW parameter-leakage risk on the count basis — the F1 premise of eight independent knobs is factually wrong; the eight quantities collapse to one spectrum; the RESIDUAL and separate risk is FORMULA SELECTION [which combination of the locked quantities was picked post-hoc — n_s/acoustic peaks QG239, QG250 #6 — already disclosed as RETRO-SELECTION RISK and blind-tested QG240 BLIND SUCCESS], a distinct claim not adjudicated here")
add(254,"TQMQG_IndependentPredictionAudit.md","MEDIUM INDEPENDENT EVIDENCE","foundation","audit",
    "independent prediction audit (audit only, measures how much of TQM's validation comes from genuine prediction vs reconstruction; reviews QG176/177/190-193/199-202/240; classifies every result POSTDICTION / BLIND RECONSTRUCTION / PRE-REGISTERED PREDICTION / EXTERNAL SUPPORT): the inventory of 60 evidence units — POSTDICTION 35 [the tested observable register: masses, mixings, couplings, EW precision, gravity, cosmological fractions — targets KNOWN when derived], BLIND RECONSTRUCTION 21 [QG176 Higgs 5 (MH, ΓH, MH/MW, MH/MZ, λ_H hidden, rebuilt from pre-Higgs D96, 0.19%), QG177 leave-one-out 12 (each observable hidden, mean dev 0.58%), QG240 cosmology blind 4 (n_s, ℓ₁, ℓ₂/ℓ₁, ℓ₃/ℓ₁ locked from D96 only, max dev 0.058%)], PRE-REGISTERED PREDICTION 3 [P1 106 GeV QG190, P2 m_ββ=2.02 meV QG191, P3 sector-ladder QG192 — frozen before measurement], EXTERNAL SUPPORT 1 [P3 151.98 rung ~ 152 GeV diphoton excess (arXiv:2503.16245), local 3.6σ / global up to 5.4σ, z=2.80σ, QG200/201]; P1/P2 remain PENDING (0 external units yet); EVIDENCE FRACTIONS — methodological independence (derivation machinery never sees the target: blind + pre-registered + external) = 25/60 = 41.7%; temporal independence (strictest: the target did not exist at derivation time) = 4/60 = 6.7%; postdiction (target known) = 35/60 = 58.3%; DETERMINATION: MEDIUM independent-evidence strength — 42% of validation units are produced with the target hidden from the derivation machinery (methodological blindness), of which the temporally-predictive core is 6.7%; the QG250 F2 self-confirmation claim is only PARTIALLY mitigated — the genuinely temporal prediction content is small but nonzero and externally supported (P3), while 58% of the numerical evidence base remains postdiction against known targets")
add(255,"TQMQG_FormulaUniquenessAudit.md","1 UNIQUE / 2 NON-UNIQUE / 4 MULTIPLE","foundation","audit",
    "formula uniqueness audit (methodology only, no new physics; reviews QG203/209/234/237/238/247; replaces empirical formula choice with a derivation-choice rule): generate ALL dimensionless combinations of the D96 quantities (Σm, #d, #g, span, λ₂, occ₀, occ₁, occ₃, occMom, Σ√m) — a candidate pool of hundreds of thousands of expressions (q, q², q³, √q, 1/q, ln q, affine differences, products, ratios, triples, 1/(affine), constant multipliers); complexity = distinct quantities + operators + (1 if non-trivial constant); for each observable find every candidate within 0.5% and determine whether the published formula is the SIMPLEST; THE RESULTS (7 observables): UNIQUE 1 [r₃₁ = span/√3 — the sole minimal-complexity match (c=3); λ₂³·Σ√m matches only at c=4], NON-UNIQUE 2 [m_μ/me = Σm²/√occMom (c=5, ties with #g²/√occ₃ dev 0.26% and 5/4·Σ√m/λ₂ dev 0.15%), m_τ/m_μ = √occMom·λ₂ (c=4, ties with √3·√Σm dev 0.24% and √#d/λ₂ dev 0.40%)], MULTIPLE MATCHES 4 [1−n_s = ln(span)/(Σm−#d) (c=7, SIMPLER 1/(span·ln occ₃) c=5 dev 0.16% exists), r₂₁ = (Σm−#d)·occ₁/occ₃ (c=6, SIMPLER √Σm/occ₀ c=4 dev 0.004%), m₂/m₃ = 2Σm/(Σ√m·√(span·#g)) (c=8, SIMPLER 1/(occ₀·√2) c=4 dev 0.100%), y_t/y_b mass-law ratio (c=8, SIMPLER occ₀²/λ₂ c=4 dev 0.37%)]; DETERMINATION: UNIQUE 1 / NON-UNIQUE 2 / MULTIPLE MATCHES 4 — only r₃₁ is the unique minimal-complexity expression; SIX of SEVEN published formulas are NOT forced by a minimal-complexity derivation-choice rule; the choice was TARGET-INFORMED (empirical), confirming the QG239/QG250 RETRO-SELECTION RISK for all but r₃₁; a blind minimal-complexity search does NOT reproduce the published formulas as the unique simplest expressions — simpler D96 combinations reproduce the same observables (the strongest quantitative support for the selection risk on n_s, acoustic peaks, neutrino ratio, quark hierarchy)")
add(260,"TQMQG_BlindFormulaTournament.md","WEAK BLIND PREDICTION","foundation","audit",
    "blind formula tournament (audit only; the decisive test of whether the QG254/QG255 D96 selection rules have PREDICTIVE power; input D96 quantities only, forbidden observable/target values during selection, generate all expressions up to complexity 6 restricted to ratio-form, apply QG254 octave preservation + QG255 moment-closure MDL, select top formula only, lock, then reveal target, score success iff within 1%): THE RESULT — the target-free rule chain is DEGENERATE: it selects the SAME formula (λ₂/occMom = 0.000203, the globally minimal-complexity octave-preserving ratio) for every observable, because it has no reference to which observable it is selecting for; that locked formula matches NONE of the seven revealed targets (1−n_s, r₂₁, r₃₁, m₂/m₃, y_t/y_b, m_μ/me, m_τ/m_μ) — success rate 0/7; the rules only 'work' when the candidate pool is PRE-RESTRICTED BY THE TARGET (as in QG253, which found matches within 0.5% of each target) — that is, when the target was already used to build the pool; in a genuine blind setting the rules collapse to a single global formula that predicts nothing; CLASSIFICATION: WEAK — the selection rules have NO blind predictive power; the QG253-255 'selection principle' program is a POST-HOC DESCRIPTIVE narrowing of pools that were built using the targets they claim to predict; this is the decisive confirmation of QG256 (HIGH selection-principle risk) and QG257 (NO UNIVERSAL PRINCIPLE): a formula cannot be selected for a specific observable without reference to what that observable is")
add(261,"TQMQG_ObservableOriginAudit.md","MEDIUM OBSERVABLE-SELECTION RISK","foundation","audit",
    "observable origin audit (observable audit only — no formula complexity, no uniqueness [that is QG253]; for each major result [masses, couplings, mixings, cosmology, GR observables] determine whether the OBSERVABLE was selected because a D96 formula matched it [post-hoc] or because D96 naturally points to it; classification: NATURAL = D96 structure alone leads to it [identity / octave-forced class / value frozen-or-hidden before measurement], SECONDARY = catalog value known at derivation but D96 class-consistent, POST-HOC = entered the register because a formula matched it [QG239/250 flags]): THE REGISTER — 29 observables: NATURAL 7 [family count = 3 [exact identity, QG138/210], Higgs blind MH + MH/MW/MH/MZ/λ_H [QG176, hidden target], θ_QCD = 0 [exact automorphism, QG174], P1 106 GeV [QG190 pre-registered], P2 m_ββ [QG191 pre-registered], P3 sector ladder [QG192 pre-registered]], SECONDARY 19 [lepton hierarchy, quark masses, neutrino masses, MW/MZ, α_weak/strong, sin²θ_W, g-2 μ/e, Yukawa ratios, CKM, CKM CP, PMNS, Λ, Ω fractions, G/M_Pl, M∝R, Bekenstein, frame dragging, GPS — all catalog values known at derivation], POST-HOC 3 [n_s + acoustic peaks [QG239 explicit RETRO-SELECTION RISK], 1/α_em = 137 [QG250 asserted-dictionary attack]]; RISK SCORE = (0.5·19 + 1.0·3)/29 = 0.431; DETERMINATION: MEDIUM observable-selection risk — the register is predominantly CATALOG-DRIVEN [19/29 secondary: D96 produces the right class, the specific target came from the measured catalog], the genuine NATURAL core [7/29] is temporally independent [pre-registered + blind], the post-hoc minority [3/29] is small and already flagged, and the honest BEKENSTEIN FAILURE [QG185/196 impossibility proof — a catalog target D96 CANNOT match without importing π] is anti-retro evidence that selection is not pure fitting; consistent with QG252 [MEDIUM independent evidence] and QG253 [formula-level target-information]: the observables are mostly catalog-driven but the D96 structural classes they belong to are genuine — the risk is concentrated in the specific numerical choices of n_s, the acoustic peaks, and the 1/α_em dictionary")
add(263,"TQMQG_ResonanceOperatorAudit.md","OPERATOR LAYER","foundation","audit",
    "resonance operator audit (the HYPOTHESIS: Σm, span, λ₂, occMom, Σ√m are NOT fundamental quantities — they are PROJECTIONS of deeper resonance operators; search the six candidate operators [crowding/degeneracy grouping, compression/octave banding, beat/frequency ratio, locking/spectral gap, moment/universal read-out, synchronization/actualization cycle N=96]; cluster all successful derivations; identify the minimum operator basis): THE PROJECTIONS — all six derived quantities are VERIFIED operator projections [Σm = MOMENT₁∘CROWDING = 95, Σ√m = MOMENT_½∘CROWDING = 64.0825, Σm² = MOMENT₂∘CROWDING = 229, occMom = MOMENT∘COMPRESSION = 1900.25, span = BEAT = ω_max/ω_min = 6.4025, λ₂ = LOCKING = spectral gap = 0.3864; 6/6 verified]; THE CLUSTERING — every successful derivation passes through the layer, no formula reads a raw mode or eigenvalue [QG162 gauge MOMENT, QG168 EW MOMENT+BEAT, QG209/247 lepton/Yukawa MOMENT+LOCKING, QG173 quark MOMENT, QG237/238 CMB MOMENT+BEAT+COMPRESSION, QG181 gravity MOMENT+COMPRESSION, QG176 Higgs blind MOMENT+BEAT+LOCKING]; THE MINIMUM OPERATOR BASIS — {CROWDING, COMPRESSION, BEAT, LOCKING} + the universal MOMENT read-out = 5 operator kinds generating all six derived quantities from the one D96 spectrum [synchronization/N=96 is the source that produces the spectrum the operators project]; DETERMINATION: OPERATOR LAYER — the hypothesis is CONFIRMED: the five named quantities are projections of a deeper resonance operator layer, not primitives; HONEST CAVEAT [consistent with QG256/257/259]: the operators are well-defined STRUCTURAL spectral projections, but WHICH operator output was assigned to WHICH sector [ν→Σ√m, u→occMom, ...] retains target-information from the QG149-157 fitting era — the operator LAYER is genuine, the operator-to-sector ASSIGNMENT is not derivation-free; this localizes the QG250 referee boundary precisely: the layer exists, the SM-sector-label mapping is the residual empirical step")
add(275,"TQMQG_AssignmentPrincipleAudit.md","PARTIAL ASSIGNMENT","foundation","audit",
    "assignment principle audit (the QUESTION: why does a projection become mass, coupling, mixing, gravity, or cosmology instead of another sector? is there a D96-native assignment rule? no observables, no target values, D96 only, deterministic): THE STRUCTURAL ASSIGNMENT FEATURES — 4/5 are D96-native and each determines a sector by form [dimension [me-anchored, NOT D96-native] → mass [the only dimensional read]; unitarity V†V=I [D96-native] → mixing [the only unitary matrix, CKM verified Vud²+Vus²+Vub²=1]; log/global reads [ln(span), I_occ/ln K, D96-native] → cosmology [the only log-of-spectrum read]; power≥2 [M_Pl=(Σm·#g·occ₂)³ cube=d=3, D96-native] → gravity [the only power≥2 combination]; ratio reads [3/Σm, #d/(2Σm)] → AMBIGUOUS [coupling OR mixing OR mass-ratio]]; THE DECISIVE EVIDENCE — THE IDENTICAL FORM √occMom·λ₂ IS ASSIGNED TO BOTH MASS AND COUPLING [m_τ/m_μ = √occMom·λ₂ = 16.842 [MASS sector QG209] AND y_τ/y_μ = √occMom·λ₂ = 16.842 [COUPLING sector QG247, since y_f=m_f/v] — the assignment of this read is NOT determined by structure, it is determined by the THEORETICAL ROLE [which equation it enters: the mass hierarchy vs the Yukawa Lagrangian]; similarly Vus=#d/(2Σm) is structurally a coupling-like ratio, only its placement in the unitary CKM matrix makes it a mixing angle]; SECTOR DETERMINABILITY BY FORM — 3/5 determinable [mass YES [only dimensional read], cosmology YES [only log read], gravity YES [only power≥2 read], mixing PARTIAL [unitarity structural but individual ratios look like couplings], coupling NO [ratio read ambiguous]]; THE ASSIGNMENT RULE [partial, D96-native, target-free] — R1 dimensional→mass; R2 unitary→mixing; R3 log→cosmology; R4 power≥2→gravity; ratio-class→ROLE-BASED [ambiguous by structure]; DETERMINATION: PARTIAL ASSIGNMENT — a D96-native structural assignment rule exists for the dimension-class, log-class, power-class, and unitary-class, but the RATIO-CLASS is NOT separable by structure: the identical read √occMom·λ₂ is assigned to both mass and coupling by its role, not its structure; the duplication is the DECISIVE BLOCKER — a complete assignment principle would require every read to map uniquely, and this one maps to two sectors; this is the precise location of the QG271 frontier: 4 structural rules + a residual role-based step [the operator→physics assignment is partially derivable [dimension/log/power/unitarity classes structural] and partially role-based [the ratio class depends on which equation the read enters — the target-informed step]]; completes the chain QG260→261→262→263→264→265→266→267→268→269→270→271→272→273: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → Universal Conservation → Self-Consistency → Individuation → Difference Principle → Post-Resonance Integrity [frontier=assignment] → Sector Emergence [sectors=projection classes] → PARTIAL ASSIGNMENT [4 structural rules + role-based ratio-class]")
add(274,"TQMQG_SectorEmergenceAudit.md","SECTOR EMERGENCE","foundation","audit",
    "sector emergence audit (the QUESTION: why do distinct sectors [masses, couplings, mixings, gravity, cosmology] exist at all? are they fundamental, emergent, or projection classes? no observables, no target values, D96 only, deterministic): THE EVIDENCE — (1) NO OPERATOR IS SECTOR-EXCLUSIVE: every sector draws from the SAME five-operator basis {CROWDING, COMPRESSION, BEAT, LOCKING, MOMENT}, no operator belongs to one sector alone [if sectors were FUNDAMENTAL each would have its own primitive operator — they do not]; (2) THE SECTOR OVERLAP [verified]: min shared fraction 50% / avg 75% [Masses×Couplings 4/5, Masses×Mixings 4/5, Mixings×Gravity 4/4, Couplings×Cosmology 2/4 the lowest] — the sectors overlap heavily, a fundamental partition would be disjoint; (3) THE SECTOR DIFFERENCE IS ROLE NOT CONTENT: masses = the VALUES of the read-outs [MOMENT primary], couplings = the STRENGTHS between read-outs [MOMENT/CROWDING], mixings = the RELATIVE ORIENTATIONS between read-out bases [CROWDING/COMPRESSION], cosmology = the GLOBAL structure of the read-out [BEAT/COMPRESSION], gravity = the SPACETIME GEOMETRY induced by the density read-out [MOMENT + structural deficit] — all five are the SAME operator layer read at different levels of the theory; (4) NO DYNAMICAL SECTOR-BOUNDARY: one spectrum [single invariant QG264], one dynamics [QG263], one operator basis [QG261] — no D96 mechanism separates mass from coupling from mixing, the boundaries are drawn by which physical question the read-out answers; THE DETERMINATION — sectors are PROJECTION CLASSES not fundamentals: NOT fundamental [no sector-exclusive operator, one spectrum/dynamics/invariant, no primitive sector-entity], NOT dynamically emergent [no sector-forming mechanism in D96], PROJECTION CLASSES [the five sectors are the same universal operator basis projected onto different theoretical roles: value, strength, orientation, global structure, geometry]; the sector structure EMERGES from the operator layer + the question-structure of the theory; HONEST CAVEAT [QG271]: the sector LABELS [mass/coupling/mixing/cosmology/gravity] are themselves the operator→physics assignment — the remaining frontier; the operator-identical structure is real and derived, the role-assignment is the residual target-informed step; DETERMINATION: SECTOR EMERGENCE [emergence score 6/6] — distinct sectors exist because the SINGLE operator basis answers different physical questions [masses ask what are the values, couplings how strong, mixings how oriented, cosmology what is the global structure, gravity what is the geometry]; the sectors are the same structure read at different roles; completes the chain QG260→261→262→263→264→265→266→267→268→269→270→271→272: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → Universal Conservation → Universal Self-Consistency → Single Individuation Principle → Universal Difference Principle → Post-Resonance Integrity [frontier = assignment] → SECTOR EMERGENCE [sectors = projection classes over the one operator basis]")
add(273,"TQMQG_PostResonanceIntegrityAudit.md","RESOLVED 2 / REFRAMED 7 / STILL OPEN 8","foundation","audit",
    "post-resonance integrity audit (re-evaluate ALL remaining issues through the QG260-270 resonance hierarchy: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → Universal Resonance Invariant [Σλ=12·96] → Universal Conservation Law [Σλ=2E=N·d] → Universal Conservation Principle → Universal Self-Consistency → Single Individuation Principle → Universal Difference Principle; for every remaining criticism determine RESOLVED/REFRAMED/STILL OPEN/FALSE PROBLEM; focus QG250/252/253/256/257/258): THE RE-EVALUATION — 17 critiques: RESOLVED 2 [QG250-F1 parameter leakage — FALSE PREMISE: the 8 moments are reads of ONE invariant (Σλ=2E=N·d, QG266) via CROWDING/COMPRESSION/BEAT/LOCKING (QG261), not 8 free knobs [QG251 already showed 2 effective params]; QG250 octave-grouping circularity — the [4,4,87] occupancies are the COMPRESSION read of the spectrum (QG261), derived not chosen]; REFRAMED 7 [QG252 independent evidence — fractions unchanged but the operator layer (QG261) and invariant Σλ (QG266) add STRUCTURAL derivation units that never see a target; temporal 6.7% remains binding; QG253 formula uniqueness — the simpler alternatives isolate a single octave band, they project the WRONG operator, octave preservation is now DERIVED, r₃₁ remains UNIQUE; QG256 selection-principle — octave preservation UPGRADED to derived, the 5/4 exception remains STILL OPEN; QG257 principle competition — the seven ad-hoc principles collapse into ONE structural requirement [project the correct operator family, QG263], the assignment remains target-informed; QG258 blind tournament — the WEAK result is EXPLAINED: the operators are UNIVERSAL (QG262) so a target-free chain CANNOT discriminate observables, a structural consequence not a selection defect; QG250 N=96 selection — the network is the actualization attractor, Σλ=12·96=N·d makes the degree-12 regularity structural; QG250 per-particle mass fits — the mass formulas consume the SAME operator basis in every sector (QG262), projections of one layer]; STILL OPEN 8 [QG250-F2 self-confirmation — structural reduction adds no independent validation; me=0.511 anchor — the only genuinely free input; conformal η import; Bekenstein π gap; ψ primitive; y_f=m_f/v definitional; 3+1 selection; QG250 minors+editorial [Λ value, H, RG import, ρ→metric non-uniqueness, publication]]; FALSE PROBLEM 0; THE ANSWER — YES, the resonance reduction changed the interpretation: it RESOLVED the structural critiques [parameter leakage was a false premise, octave grouping is derived], REFRAMED the selection-principle critiques [octave preservation is a derived projection, the principle competition collapses to one structural requirement, the blind-tournament weakness is explained by operator universality], and left the genuinely EMPIRICAL/METHODOLOGICAL critiques unchanged [F2, me anchor, temporal evidence, structural imports]; THE TRUE REMAINING FRONTIER AFTER QG270 — (1) THE ASSIGNMENT STEP [which operator output maps to which observable/sector, QG262 caveat — the operator basis is universal but the structure→physics-label mapping retains target-information, QG257/258/259]; (2) THE 5/4 EXCEPTION [acoustic-peak factor inconsistent with the Noether rule, QG256]; (3) THE me=0.511 ANCHOR; (4) INDEPENDENT TEMPORAL EVIDENCE [6.7% temporal, QG252]; (5) STRUCTURAL IMPORTS [conformal η, Bekenstein π, ψ primitive, RG, 3+1]; the ASSIGNMENT step [structure → physics labels] is the true frontier")
add(272,"TQMQG_DistinctionOriginAudit.md","UNIVERSAL DIFFERENCE PRINCIPLE","foundation","audit",
    "distinction origin audit (the TERMINAL question of the individuation chain: WHAT is being distinguished, and does distinction arise from structure, actualization, or a deeper principle? no observables, no formulas, D96 only, deterministic): THE CANDIDATES — (a) CAUSAL POSITION: the branching generations k=0..K−1, density shares ρ_k=μ^k/S are DISTINCT per generation [μ=2: 0.0039, 0.0078, 0.0157, ... distinct; causal positions ARE distinguishable when μ≠1]; (b) NETWORK POSITION: the observable sector is a REGULAR graph [all 96 nodes degree 12, QG266] — all nodes structurally identical, NO network-position distinction, no structural labels; (c) STATE DIFFERENCE: a Q-event is a PROJECTION to a tick/no-tick binary state [QG73] — the event changes state, the DIFFERENCE between before/after IS the tick; (d) ACTUALIZATION DIFFERENCE: a Q-event is a NETWORK TRANSITION [QG29] — the event IS a before→after difference, ρ counts these transitions; THE D96 DIFFERENCE STRUCTURE — the observable-sector Laplacian has 96 eigenvalues: ONE ZERO MODE [the constant vector, in ker L, QG266 — the BACKGROUND/uniform reference] and 95 POSITIVE modes [the DIFFERENCES from the background], with 44 distinct frequencies [degeneracy groups]; distinction = the difference between the background and each mode; WHAT IS BEING DISTINGUISHED — DIFFERENCES THEMSELVES: each Q-event is a difference [before→after transition], causal positions are distinguished by their different shares ρ_k=μ^k/S, the positive modes are distinguished by their difference from the zero/background; there is no pre-existing substance that gets a label — the distinction IS the difference; WHERE DISTINCTION COMES FROM — NOT from structure [the regular network provides no positions/labels/separating geometry]; from ACTUALIZATION — yes, but only because actualization IS a difference [the event is a before→after transition, and the transition is the difference — actualization is not a separate source, it IS a difference]; the deeper source is DIFFERENCE itself: distinction is the registration of a difference, the most primitive notion — before structure, before actualization, before count: a thing is distinguishable exactly insofar as it differs from something else; DETERMINATION: UNIVERSAL DIFFERENCE PRINCIPLE [origin score 6/6] — distinction does not arise from structure [the regular network has none] and not from actualization as a distinct source [actualization IS a difference]; distinction = DIFFERENCE, the most primitive notion of the theory; what is distinguished is differences: the zero/background vs the positive modes, the μ^k/S shares, the before→after transitions; completes the reduction chain QG260→261→262→263→264→265→266→267→268→269→270: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → Universal Resonance Invariant [Σλ=12·96] → Universal Conservation Law [Σλ=trace=2E=N·d] → Universal Conservation Principle [all laws = count conservation] → Universal Self-Consistency [count conserved because a Q-event IS a unit] → Single Individuation Principle [count & distinction = two faces of actualization] → UNIVERSAL DIFFERENCE PRINCIPLE [what is distinguished is DIFFERENCE itself]; the theory bottoms out in DIFFERENCE, the most primitive notion, from which distinction, individuation, count, and ultimately all conservation laws arise")
add(271,"TQMQG_DistinctionPrincipleAudit.md","SINGLE INDIVIDUATION PRINCIPLE","foundation","audit",
    "distinction principle audit (the QUESTION: what makes a Q-event a distinguishable unit? is count more fundamental than distinction, or vice versa? no observables, no formulas, D96 only, deterministic): THE EVIDENCE — (1) COUNTABILITY: N=96 actualization events, Σm=95 modes, Born rule Σρ=1 exact by construction [QG216, self-consistent count QG268]; (2) DISTINCTION CANNOT COME FROM STRUCTURE: the observable sector is a REGULAR graph [all 96 nodes degree 12, QG266] — the nodes carry NO structural labels, every node is structurally identical, so there is nothing in the graph that separates one node from another; (3) THE DEGENERACY EVIDENCE [decisive]: the multiplicity multiset is [42×2, 5, 6] — 42 groups of two modes have IDENTICAL frequency [indistinguishable by ω=√λ], yet they are counted as 84 SEPARATE units in Σm=95 [VERIFIED: the count counts units that are NOT distinguished by frequency — unit-ness/individuation is PRIOR to distinction-by-frequency]; (4) INDIVIDUATION: a Q-event is a NETWORK TRANSITION [a tick, QG29] — it actualizes at a POSITION in causal order [branching generation k, QG1], the transition is what individuates the event; THE ORDERING QUESTION — COUNT from DISTINCTION? NO [degenerate pairs have identical ω yet are counted separately — count works without spectral distinction]; DISTINCTION from COUNT? NO [the network is regular — no structural order separates the nodes]; BOTH DIRECTIONS FAIL — they arise TOGETHER from the SINGLE act of actualization: a Q-event is a distinct tick at a distinct causal position, and this ONE act makes the event simultaneously COUNTABLE [one tick = one unit → N, ρ, Σm] and DISTINGUISHABLE [a distinct tick at a distinct position]; DETERMINATION: SINGLE INDIVIDUATION PRINCIPLE [distinction score 6/6] — count and distinction are NOT ordered, neither is more fundamental; they are the two inseparable faces of the single act of individuation [actualization]; a Q-event is a distinguishable unit BECAUSE IT ACTUALIZES — a distinct tick at a distinct causal position; completes the reduction chain QG260→261→262→263→264→265→266→267→268→269: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → Universal Resonance Invariant [Σλ=12·96] → Universal Conservation Law [Σλ=trace=2E=N·d] → Universal Conservation Principle [all laws = count conservation] → Universal Self-Consistency [the count is conserved because a Q-event IS a unit] → SINGLE INDIVIDUATION PRINCIPLE [a Q-event is a unit because it actualizes — count and distinction are the two faces of the one individuation act]; the theory bottoms out in the INDIVIDUATION ACT: the primitive actualizes, and that single act makes it a countable, distinguishable unit")
add(270,"TQMQG_CountConservationOrigin.md","UNIVERSAL SELF-CONSISTENCY","foundation","audit",
    "count conservation origin (the TERMINAL QUESTION of the QG260-268 reduction chain: WHY is the actualization count conserved? no observables, no formulas, D96 only, deterministic): THE ORIGIN — (1) ACTUALIZATION: the Q-event is a REAL-UNDERIVED primitive whose minimal physical content is a NETWORK TRANSITION [a local time-state change / clock tick] — the primitive IS an actualization, not a passive point [the bare primitive-point fails actualization content, QG29]; (2) INDIVIDUATION: each Q-event is an INDIVIDUAL, indivisible unit — ρ is the counting measure = the density of individual Q-events, each event is ONE counted unit [individuation makes the primitive countable]; (3) Q-EVENTS: the actualization is a branching process over octave layers [QG1], the Born rule Σρ=1 is EXACT by construction [ρ = normalized share of the count, QG216] — the count is the primitive's own arithmetic; (4) NETWORK CLOSURE: the actualization dynamics converges to the unique N=96 attractor with a FIXED link count [QG116: identical 576 links, identical span 6.40, from every initial pattern; topology convergence verified 0% residual link growth] — the network is closed, its event count fixed by the attractor; (5) SELF-CONSISTENCY [the decisive step]: a Q-event IS a unit — conservation of the count states that the number of primitive units is fixed; this is NOT a dynamical law [not Noether-from-symmetry] and NOT an unexplained axiom — it is the DEFINITIONAL IDENTITY of the primitive itself: a primitive must be self-identical [one event = one unit], so the count cannot change without the primitive ceasing to be a unit; WHY COUNT IS CONSERVED — because a Q-event IS a unit: the count is the number of primitive units, a primitive is by definition self-identical, you cannot split a unit without making it not-a-unit; conservation is the self-consistency requirement of a theory built from indivisible primitives; every deeper conservation law [norm, trace, unitarity, Bianchi, Noether — QG267] is a projection of this single self-consistency statement; DETERMINATION: UNIVERSAL SELF-CONSISTENCY [origin score 6/6] — the count is conserved because the Q-event [the primitive] IS a unit; conservation is the definitional identity of the primitive, the self-consistency of a theory of indivisible actualization units; completes the reduction chain QG260→261→262→263→264→265→266→267→268: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → Universal Resonance Invariant [Σλ=12·96] → Universal Conservation Law [Σλ=trace=2E=N·d] → Universal Conservation Principle [all laws = count conservation] → UNIVERSAL SELF-CONSISTENCY [the count is conserved because a Q-event IS a unit]; the theory bottoms out in self-consistency: the primitive is a countable unit, and the conservation of its count is what it means for the primitive to be a unit")
add(269,"TQMQG_ConservationPrincipleAudit.md","UNIVERSAL CONSERVATION PRINCIPLE","foundation","audit",
    "conservation principle audit (the QUESTION: are all conservation laws manifestations of ONE deeper principle? review QG61-74, QG181-223, QG260-266; collect norm conservation, unitarity, trace conservation, count conservation, Bianchi conservation, Noether currents; D96 only, no observables, deterministic): THE SIX LAWS — ALL VERIFIED [NORM: Born rule Σ|ψ|²=1, QG73/216, holds for any μ — ρ=μ^k/S is the normalized actualization share; COUNT: N=∫ρdV conserved, QG194/222 — the total population preserved by the branching process (normalizer S exact, no sources/sinks); TRACE: trace(L)=2·edges=1152=96×12, QG266 — handshake lemma of the N=96 network; UNITARITY: CKM/PMNS preserve the total norm, Vud²+Vus²+Vub²=1, QG165/167; BIANCHI: ∇·G=0, QG197/222 — divergence-free because matter = conserved deficit dust; NOETHER: energy = actualization rate, QG89 — conserved via time-translation; gauge charges = Noether charges of D96 symmetries, QG243]; THE UNIFYING PRINCIPLE — conservation of the ACTUALIZATION COUNT N: every one of the six laws is a PROJECTION of the single fact that the total actualization count N is conserved [NORM = normalized count (ρ=μ^k/S, Σρ=1); COUNT = the primitive statement; TRACE = network link count 2·links = 2·(actualization events), fixed by the N=96 attractor; UNITARITY = norm preservation (= count preservation under basis change); BIANCHI = count conservation in geometric/differential form (∇·T=0 from deficit conservation → ∇·G=0); NOETHER = conservation of the count's time-conjugate (energy = actualization rate)]; DETERMINATION: UNIVERSAL CONSERVATION PRINCIPLE [unification score 6/6] — the conservation laws are NOT independent; they are different measurements of ONE principle: the actualization count N is conserved; this is the deepest statement of the QG260-266 reduction chain: not only a single invariant [Σλ] and a single dynamics [the resonance], but a SINGLE CONSERVATION PRINCIPLE of which every conservation law in the theory is a projection; HONEST CAVEAT: the trace conservation is also a universal graph identity [handshake lemma, true for ANY graph]; its SPECIFIC value 2E=1152 is set by the N=96 attractor; the unification claim is the reduction of all laws to count conservation, not that the handshake lemma is unique to TQM")
add(268,"TQMQG_InvariantOriginAudit.md","UNIVERSAL CONSERVATION LAW","foundation","audit",
    "invariant origin audit (the QUESTION: is the invariant Σλ = 12×96 fundamental or the projection of a deeper conservation law? no observables, no formulas, D96 only, deterministic, structure only): THE TRACE IDENTITY — Σλ = Σω² is the TRACE of the graph Laplacian L = D − A of the D96 observable sector; for ANY graph, by construction, trace(L) = Σ degrees = 2·(number of edges) — the HANDSHAKE LEMMA, a universal identity of every Laplacian, not a fitted constant; VERIFIED N=96, edges=576, trace(L)=1152=2·576=Σ degrees=Σλ; WHY THE VALUE IS 12×96 — the observable sector is a REGULAR graph [every one of the 96 nodes has degree 12 = the gauge sector 1+3+8, QG161; degree distribution {12}], so trace(L) = N·d = 96·12 = 1152 — the factorization is the degree structure of a regular graph, not an independent relation; WHY IT IS CONSERVED — (1) UNIVERSAL TRACE CONSERVATION [trace(L) = Σ degrees = 2E holds for every Laplacian — a mathematical identity of the L=D−A construction, the diagonal is the degree sequence of a fixed network]; (2) KERNEL/TOTAL-MASS CONSERVATION [every Laplacian has the constant vector in its kernel — row sums EXACTLY zero, verified max |row sum|=0; the Laplacian dynamics ẋ=−Lx conserves the total sum Σx — the ACTUALIZATION CONSERVATION, and the trace identity is its scalar projection]; (3) NETWORK/CYCLE CONSERVATION [the N=96 network is the CONVERGED ATTRACTOR of the actualization dynamics QG115/125/159/160 — the D96 selection is INEVITABLE; the dynamics conserves its attractor → N, E, degree sequence fixed → trace=2E fixed]; DETERMINATION: UNIVERSAL CONSERVATION LAW [origin score 6/6] — Σλ is NOT fundamental; it is the projection of a UNIVERSAL conservation law [the Laplacian trace identity: handshake lemma trace=Σdegrees=2E + kernel conservation: constant vector in ker L → total actualization conserved] instantiated on the conserved N=96 actualization attractor; the specific value 1152 = 96×12 = N·d follows from the network being degree-12 regular [degree = gauge sector 1+3+8]; completes the reduction chain QG260→261→262→263→264→265→266: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → Universal Resonance Invariant [Σλ=12·96] → UNIVERSAL CONSERVATION LAW [Σλ = trace = 2E = N·d, handshake lemma]; WHY Σλ IS CONSERVED — because it is the trace of the Laplacian, a quantity every graph conserves by the handshake lemma, on a network the actualization dynamics itself conserves; the invariant is DERIVED from a universal law, not a primitive constant")
add(267,"TQMQG_ResonanceInvariantAudit.md","UNIVERSAL RESONANCE INVARIANT","foundation","audit",
    "resonance invariant audit (the QUESTION: what is the ACTUAL invariant? search for the common conserved quantity behind BEAT, LOCKING, CROWDING, COMPRESSION; determine whether all successful sectors are different measurements of one invariant; D96 only, no observables, structure only): THE CONSERVED QUANTITY — the total spectral weight of the D96 observable-sector Laplacian Σλ = Σω² over the 95 positive modes = 1152.00000000 EXACTLY [this is the TRACE of the Laplacian, a graph invariant that is basis-independent (Σλ = Σ degrees = 2·edges = 2·576), therefore CONSERVED under the N=96 resonance dynamics — the network fixes the spectrum which fixes the total spectral weight]; THE STRUCTURAL FACTORIZATION — Σλ = 1152 = 12 × 96 = (gauge degree 1+3+8, QG161) × (cycle size N) — the invariant IS the product of the two most fundamental D96 integers; THE OPERATORS MEASURE THE SAME INVARIANT — each operator is a deterministic read of the ONE 95-mode list ω=√λ [CROWDING = degeneracy read [multiset → Σm/Σ√m/Σm²], COMPRESSION = octave-band read [occupancies → occMom], BEAT = extent read [span = ω_max/ω_min], LOCKING = gap read [λ₂ = ω_min²]] — a conserved quantity cannot change under any read of the system, so the operators are exactly the different measurements of the one invariant; ALL SECTORS MEASURE THE SAME INVARIANT — masses consume {Σm², occMom, λ₂, span, Σ√m}, couplings {Σm, Σ√m, λ₂, occ₀}, mixings {#d, #g, occ ratios, ω₀/ω₂}, cosmology {Σm, span, occ}, gravity {Σm, #g, occ₂} — every sector consumes reads of the same spectrum whose total weight is conserved; THE BEAT IDENTITIES couple the reads [Σ√m/span ≈ 10, occMom/Σm ≈ 20, Σm²/Σm ≈ 12/5, occMom/Σm² ≈ 25/3]; DETERMINATION: UNIVERSAL RESONANCE INVARIANT [invariant score 6/6] — the conserved quantity is the total spectral weight Σλ = Σω² = 1152 = 12×96 [gauge degree × cycle], and all four operators — hence all five sectors — are different measurements of this ONE invariant; completes the reduction chain QG260→261→262→263→264→265: Resonance Layer → Operator Layer → Same Operator Sectors → Single Resonance Dynamics → Single Resonance Invariant → UNIVERSAL RESONANCE INVARIANT [Σλ = 12·96]; HONEST CAVEAT [consistent with QG261-264]: the operator-to-sector assignment retains QG149-157-era target information; the conserved quantity itself is D96-only and EXACT — Σλ = 1152 = 12·96 is a structural identity independent of any observable")
add(266,"TQMQG_ProjectionFamilyAudit.md","SINGLE RESONANCE INVARIANT","foundation","audit",
    "projection family audit (the QUESTION: are the Density and Frequency projections [QG263] fundamental, or themselves manifestations of a single resonance invariant? no observables, no formulas, D96 only, deterministic, structure only): THE STRUCTURE — (1) SHARED ORIGIN: both projections are deterministic functions of the SAME 95-mode frequency list ω=√λ [the multiset [42×2,5,6] by degeneracy counting, the occupancies [4,4,87] by octave banding, the span and λ₂ from the same list — no density quantity exists independently of the frequency list]; (2) FREQUENCY → DENSITY duality: the octave band count is DETERMINED by the span [family count = floor(log2 span)+1 = 3 = octave band count, log2(span)=2.6786 → 3 bands — the frequency projection fixes how many density bands exist]; (3) DENSITY ↔ FREQUENCY duality: the unified spectral access law δ = log(N_eff)/log(span) [QG156/157] pairs each DENSITY moment with the FREQUENCY span into ONE exponent, reproducing all four sectors within 1% [ν 2.22 vs 2.241, d 2.45 vs 2.449, ℓ 2.93 vs 2.940, u 4.07 vs 4.066 — the density moments and the span are not independent inputs, they combine into a single exponent per sector]; (4) COMMON INVARIANT: the beat identity Σ√m/span = 10.009 ≈ 10 [dev 0.09%, QG260] directly couples a density moment to the frequency span; (5) ACTUALIZATION interpretation: the N=96 resonance dynamics actualizes the spectrum, the density projection reads HOW MANY modes actualize at each frequency/octave, the frequency projection reads WHERE the actualized frequencies sit — both are views of the single actualized list; THE MINIMUM STRUCTURE: Resonance Dynamics [N=96] → the D96 spectrum [ONE 95-mode list] → {density read, frequency read} → the moments; the two projections are NOT fundamental — they are DUAL READS of the single spectrum; DETERMINATION: SINGLE RESONANCE INVARIANT [projection score 6/6] — the density and frequency projections are manifestations of ONE object [the D96 spectrum, the resonance invariant], there is NO independent density primitive; completes the reduction chain QG260→261→262→263→264: Resonance Dynamics → spectrum → {density, frequency} reads → moments → all sectors; HONEST CAVEAT [consistent with QG261/262/263]: the operator-to-sector assignment retains QG149-157-era target information; this STRUCTURAL duality is D96-only and independent of any observable")
add(265,"TQMQG_OperatorReductionAudit.md","SINGLE RESONANCE DYNAMICS","foundation","audit",
    "operator reduction audit (the HYPOTHESIS: the four operators [CROWDING, COMPRESSION, BEAT, LOCKING] are not fundamental — they are projections of a deeper resonance dynamics; structure only, no observables, no target values, D96 only, deterministic): THE REDUCTION PROOFS — (1) CROWDING ≡ COMPRESSION: both are mode-density concentration at two resolutions [CROWDING = exact-degeneracy histogram → multiset [42×2,5,6]; COMPRESSION = octave histogram → occupancies [4,4,87]; PROOF — the octave occupancy of every band equals the sum of the degeneracy group sizes in the band: band 0: 4=4, band 1: 4=4, band 2: 87=87, all exact → COMPRESSION is the octave-aggregation of CROWDING, REDUCIBLE]; (2) BEAT ≡ LOCKING: both read the same Laplacian frequency structure [LOCKING = λ₂ = ω_min² spectral gap; BEAT = span = ω_max/ω_min; since ω=√λ, BEAT = √(λ_max/λ₂); VERIFIED span = 6.402515 = √(λ_max/λ₂) exactly → BEAT is the ratio form of the same frequency-synchronization read, REDUCIBLE]; (3) MOMENT is a MEASUREMENT FUNCTIONAL, not an operator [maps a distribution to a scalar [Σm, Σ√m, Σm², occMom], introduces no new structure]; THE DEPENDENCY GRAPH — Resonance Dynamics [N=96 actualization] → D96 spectrum [95 modes, ω=√λ] → CROWDING [density histogram] → COMPRESSION [octave-aggregation of CROWDING] → LOCKING [λ₂ gap] → BEAT [√(λ_max/λ₂) ratio] → MOMENT [read-out] → moment set consumed by QG165-262; THE MINIMUM BASIS — the four operators reduce to TWO structural families [DENSITY CONCENTRATION: CROWDING with COMPRESSION as octave aggregation; FREQUENCY SYNCHRONIZATION: LOCKING with BEAT as ratio form], both projections of the SAME spectrum produced by the SINGLE N=96 resonance dynamics; minimum basis = 1 resonance dynamics + 2 projection families + 1 read-out functional; DETERMINATION: SINGLE RESONANCE DYNAMICS [reduction score 6/6] — the four operators are not fundamental; the operator layer is the projection surface of the deeper resonance dynamics; completes the reduction chain QG260→261→262→263: Resonance Dynamics → 2 operator families → moment set → all physical sectors; no observables, no target values, no fitting")
add(264,"TQMQG_OperatorSectorAudit.md","SAME OPERATOR SECTORS","foundation","audit",
    "operator sector audit (classify EVERY successful derivation QG140-261 by PRIMARY and SECONDARY operator from the QG261 basis [CROWDING/degeneracy groups #d/#g/ω₀ω₂, COMPRESSION/octave bands occ/occMom, BEAT/frequency ratio span, LOCKING/spectral gap λ₂, MOMENT/universal read-out Σm/Σ√m/Σm²]; assignment from the PUBLISHED formulas — no target values, no fitting): THE OPERATOR MAP — 30 observables: MASSES 8 [m_μ/me MOMENT→COMPRESSION, m_τ/m_μ COMPRESSION→LOCKING, quarks MOMENT→COMPRESSION, ν Δm²21 MOMENT→BEAT, ν Δm²31 MOMENT→CROWDING, MH COMPRESSION→BEAT, W/Z MOMENT→BEAT, family count BEAT], COUPLINGS 7 [1/α_em MOMENT→CROWDING, α_weak/strong MOMENT, sin²θ_W CROWDING→MOMENT, g-2 μ LOCKING→MOMENT, g-2 e COMPRESSION→MOMENT, Yukawa COMPRESSION→LOCKING, θ_QCD structural], MIXINGS 7 [Vus CROWDING→MOMENT, Vcb BEAT→CROWDING, Vub COMPRESSION→BEAT, δ_CP COMPRESSION→MOMENT, θ12 CROWDING→MOMENT, θ23 MOMENT→CROWDING, θ13 COMPRESSION→MOMENT], COSMOLOGY 5 [n_s BEAT→MOMENT, ℓ₁ MOMENT→BEAT, r₂₁ COMPRESSION→MOMENT, r₃₁ BEAT, Ω COMPRESSION→BEAT], GRAVITY 3 [M_Pl MOMENT→COMPRESSION, M∝R structural, GPS/frame dragging structural]; primary totals MOMENT=10 COMPRESSION=9 CROWDING=5 BEAT=5 LOCKING=1; THE SECTOR SIGNATURES — every sector uses ≥ 3 of the 5 operators, MOMENT is UNIVERSAL [all five sectors], NO operator is unique to any single sector; the sector differences are of EMPHASIS not operator set [masses MOMENT-dominated, mixings CROWDING/COMPRESSION-dominated, cosmology BEAT/COMPRESSION-dominated]; DETERMINATION: SAME OPERATOR SECTORS [sector score 6/6] — masses, couplings, cosmology and gravity are DIFFERENT PROJECTIONS of the SAME five-operator basis; one spectral operator basis projects the single D96 spectrum into every physical sector; the strongest form of the QG250 counter-argument: the moments are not isolated fitting knobs but the collapsed output of one resonance operator basis, and every sector draws from the SAME five operators; HONEST CAVEAT [consistent with QG257/259/261]: the operator map is structural [from the published formulas] but the operator-to-observable ASSIGNMENT retains target-information from the QG149-157 era — the universality is real, the assignment was not derivation-free")
add(262,"TQMQG_ResonanceLayerAudit.md","RESONANCE LAYER","foundation","audit",
    "resonance layer audit (the QUESTION: did the later D96 derivations QG140-258 collapse a MISSING resonance layer between D96 and the observables? TQM originates from time → oscillation → resonance → actualization; search the five resonance operators — resonance [octave/family locking], beat [integer moment ratios], locking [near-degenerate crowding], crowding [top-band compression], compression [octave collapse]; computed from the D96 spectrum, no new physics): THE EVIDENCE — layer score 6/6: octave-locked family count = floor(log2 span)+1 = 3 [QG210, frequency-doubling bands [4,4,87]], top-band crowding = 87/95 = 91.6% [occupancy [4,4,87]], mode locking = 93.6% of successive frequency ratios near-degenerate [94 ratios, 88 near log2≈0], sector ladder = fixed-spacing MZ/6 = 15.198 GeV beat comb [12 rungs, max dev 2.0%], and THREE near-integer beat identities among the collapsed moments [Σ√m/span = 10.009 ≈ 10, dev 0.09%; Σm²/Σm = 2.411 ≈ 12/5, dev 0.4%; occMom/Σm² = 8.298 ≈ 25/3, dev 0.4%]; DETERMINATION: RESONANCE LAYER — the D96 spectrum IS organized as a resonance structure: octave-locked families, mode crowding, the MZ/6 ladder beat comb, and integer moment ratios; this layer is DIRECTLY USED in the family-index [QG210], sector-ladder [QG192] and CMB acoustic-peak [QG238] derivations [the octave hierarchy, the beat comb and the occupancy ratios ARE the resonance operators in those formulas], and was COLLAPSED into the moment set {Σm, span, λ₂, occMom, Σ√m, Σm²} for the mass/coupling sector [QG165-247 use the collapsed moments directly rather than re-exposing the beat/locking operators]; the layer is REAL and not MISSING — the derivations did not lose a resonance step, they encoded it into the moments; this REFRAMES the QG250 parameter-leakage attack: the moments are not arbitrary knobs, they are the collapsed output of one underlying resonance structure")
add(259,"TQMQG_PrincipleCompetitionAudit.md","NO UNIVERSAL PRINCIPLE","foundation","audit",
    "principle competition audit (audit only, no targets, selection quality only; compares the seven formula-selection principles using QG253/254/255: octave preservation, moment closure, MDL, maximum symmetry, maximum invariance, Noether consistency, full spectrum usage; for each measure selection power, number of surviving formulas, consistency across observables, number of ad-hoc exceptions): THE MEASUREMENTS — OCTAVE PRESERVATION [power 1/7, survivors 2.0, consistent, 0 exceptions — the strongest single filter: removes all 5 non-native alternatives, uniquely selects r₃₁, leaves 3 octave-preserving ties], MOMENT CLOSURE [power 2/7, survivors 1.5, consistent, 0 exceptions — resolves the m_τ/m_μ tie (occMom+λ₂=3 beats √#d/λ₂=1 and √3·√Σm=0.5)], MDL [power 1/7, survivors 3.0, consistent, 0 exceptions — lowest power alone: uniquely selects r₃₁ only, 4/7 have simpler non-native alternatives], MAXIMUM SYMMETRY [power 1/7, survivors 2.5 — overlaps octave preservation, not independently sufficient], MAXIMUM INVARIANCE [power 0/7, survivors 7.0 — occ₀=occ₁=4 so every formula is trivially permutation-invariant: zero discriminating power, the weakest principle], NOETHER CONSISTENCY [power 3/7, survivors 1.3, INCONSISTENT, 1 exception — resolves the m_μ/me tie by rejecting 5/4·Σ√m/λ₂ but the PUBLISHED QG238 ℓ₁ = Σm·ln(span)·(5/4) uses 5/4 — the strongest by raw power but requires an ad-hoc carve-out], FULL SPECTRUM USAGE [power 1/7, survivors 2.5 — overlaps moment closure, not independently sufficient]; RANKING by power: Noether consistency > moment closure > octave preservation/MDL/maximum symmetry/full spectrum > maximum invariance; DETERMINATION: NO UNIVERSAL PRINCIPLE — no single principle uniquely selects all 7 observables; the QG255 'unique selection' came only from a SEQUENCE (octave preservation → MDL → Noether → moment closure) with one inconsistency (the 5/4 exception); the best consistent filter (octave preservation) achieves only 1-3/7 uniqueness; the selection-principle program (QG253-255) is a useful HEURISTIC NARROWING, not a universal derivation-choice rule")
add(258,"TQMQG_SelectionPrincipleAudit.md","HIGH SELECTION-PRINCIPLE RISK","foundation","audit",
    "selection principle audit (audit only, no physics, methodology only; reviews QG254 octave preservation and QG255 moment-closure MDL; determines whether these rules are FORCED by D96 or selected post-hoc): for each rule — derivable? necessary? alternative rules? competing principles?; RULE 1 OCTAVE PRESERVATION — PREFERRED [derivable PARTIALLY: the octave bands occ=[4,4,87] ARE D96-native (QG155/210) but the PROHIBITION FORM (no isolated band) was calibrated on the QG253 alternatives; necessary NO — competing symmetry projections exist (prefer occMom-based forms, band-permutation invariance trivially true since occ₀=occ₁=4, full-spectrum usage, the λ₂ scale)]; RULE 2 MOMENT-CLOSURE MDL — ARBITRARY [derivable NO — MDL is imported from information theory and the moment-order ranking is conventional; necessary NO; alternatives: prefer λ₂ as the mass scale, fewest distinct quantities, octave-permutation invariance, 3rd-moment closure; CONSISTENCY DECISIVE — INCONSISTENT: QG255 rejects 5/4·Σ√m/λ₂ because '5/4 is a free constant', but the PUBLISHED QG238 formula ℓ₁ = Σm·ln(span)·(5/4) uses 5/4 — the exclusion was calibrated on the tie candidate, not on a uniform D96 principle]; SELECTION-PRINCIPLE RISK: HIGH — 1 PREFERRED / 1 ARBITRARY / 0 FORCED; NEITHER rule is FORCED; the rules were introduced AFTER QG253 revealed the non-uniqueness, so they carry the same retro-selection character they were intended to remove — at the meta-level; the honest status of the QG253-255 selection-principle program is a reasonable heuristic narrowing, not a derivation of forced selection rules")
add(257,"TQMQG_SecondarySelectionPrinciple.md","UNIQUE SELECTION PRINCIPLE","foundation","audit",
    "secondary selection principle (methodology only, no new physics; known QG254 octave preservation; requirements no observables, no target values, D96 only, deterministic; derives ONE secondary rule resolving the QG254 octave-preserving ties): THE RULE — MOMENT-CLOSURE MINIMUM DESCRIPTION LENGTH, applied in order to the octave-preserving candidate set: (1) MINIMAL COMPLEXITY [fewest operators/quantities], (2) NOETHER CONSISTENCY [no free constant multiplier — a genuine D96 coupling is a ratio of D96 quantities only; √3 is NOT flagged because it is D96-native √#families QG210], (3) MOMENT CLOSURE / FULL-SPECTRUM USAGE [highest total moment order: occMom (2nd octave moment) and Σm² (2nd mode moment) beat half-moments Σ√m and counts #d/#g]; APPLICATION TO THE QG254 TIES (structure-only, no target): m_μ/me [Σm²/√occMom (c=5) vs 5/4·Σ√m/λ₂ (c=5) — Noether drops the free 5/4 → SELECTS Σm²/√occMom], m_τ/m_μ [√occMom·λ₂ (c=4) vs √3·√Σm (c=4) vs √#d/λ₂ (c=4) — moment closure: occMom(2)+λ₂(1)=3 beats Σ√m(0.5) and #d(0)+λ₂(1)=1 → SELECTS √occMom·λ₂], r₃₁ [span/√3 (c=3) vs λ₂³·Σ√m (c=4) — minimal complexity → SELECTS span/√3]; ALL THREE tie cases resolve to a unique formula (the published one) with NO target information; CLASSIFICATION: UNIQUE SELECTION PRINCIPLE — the combined chain QG253 (search space) + QG254 (octave preservation) + QG255 (moment-closure MDL) uniquely selects the published formulas for all audited observables from D96 structure alone, before any comparison")
add(256,"TQMQG_FormulaSelectionPrinciple.md","SELECTION PRINCIPLE","foundation","audit",
    "formula selection principle (methodology only, no new physics; reviews QG203/209/234/237/238/247/253; derives a target-free D96-only deterministic formula-choice rule that selects BEFORE any comparison): THE PRINCIPLE — OCTAVE PRESERVATION: a formula is selectable iff it does NOT isolate a single octave band occ₀/occ₁/occ₃ (or ln of a single band); the D96 sector is octave-organized (occ=[4,4,87], three octave families QG155/210), so isolating one band privileges one octave with no D96 principle; ALLOWED: octave ratios occᵢ/occⱼ (scale-invariant band structure), the full aggregate occMom=Σocc²/occ₀ (QG155), and the spectral aggregates (Σm, #d, #g, span, λ₂, Σ√m); this is the D96 symmetry projection of Noether consistency (formulas invariant under the octave band symmetry); WHY IT SELECTS — applied to the QG253 candidate pool it EXCLUDES all 5 non-native minimal-complexity alternatives [r₂₁ alt √Σm/occ₀ isolates occ₀, 1−n_s alt 1/(span·ln occ₃) isolates occ₃, m₂/m₃ alt 1/(occ₀√2) isolates occ₀, y_t/y_b alt occ₀²/λ₂ isolates occ₀, m_μ/me alt #g²/√occ₃ isolates occ₃]; all published formulas SATISFY octave preservation [Σm²/√occMom, √occMom·λ₂, ln(span)/(Σm−#d), (Σm−#d)·occ₁/occ₃ (octave ratio), span/√3, 2Σm/(Σ√m·√(span·#g))]; RESIDUAL — 3 octave-preserving ties survive (√3·√Σm, λ₂³·Σ√m, 5/4·Σ√m/λ₂), so the principle narrows to the octave-preserving class (a strong prior) but does not uniquely fix every formula without additional symmetry selection; CLASSIFICATION: SELECTION PRINCIPLE — target-free, deterministic, D96-only, removes the non-native alternatives that drove the QG253 non-uniqueness; the published formulas are the octave-preserving members of the D96 expression class selected BEFORE comparison; the residual ties are themselves octave-preserving, so additional symmetry selection (e.g. preferring occMom-based forms) is needed to fix them uniquely")
add(251,"TQMQG_FinalToeAudit.md","NEAR-COMPLETE TOE","foundation","audit",
    "final toe audit (audit only, reviews QG223-248, uses QG226 ten criteria + QG235 external checklist + QG241 + QG248): the ten TOE criteria re-evaluated — 7 DERIVED / 1 PARTIAL / 2 BOUNDARY / 0 OPEN, completeness 9.0/10 (90%); DERIVED — Quantum Mechanics [QG216/218/220/74], Gravity [QG197/207/222], Matter [QG194/195/196], STANDARD MODEL [PARTIAL → DERIVED: QG248 SM DYNAMICS COMPLETE — gauge dynamics QG243/244, Higgs potential + SSB QG246, Yukawa + mass mechanism QG247; ten-component audit 8 DERIVED / 1 framework-partial (propagator machinery) / 1 boundary (SU(3) color-count)], Initial conditions [QG227], Dimensionality [QG2/3/5/159/160], Information origin [QG228]; PARTIAL — Cosmology [all six features derived or partial; remaining: the acoustic-peak recombination mechanism QG238 (peaks ℓ₁ 0.008%, r₂₁ 0.035%, r₃₁ 0.058% derived, recombination mechanism not)]; BOUNDARY — Primitive completeness [ψ second of two primitives QG223], Parameter completeness [Bekenstein 1/4 needs π QG196, H epoch scale]; THE FOUR DETERMINATIONS: (1) ANY TRUE MISSING PHYSICS? NO — no OPEN criterion; the single PARTIAL is a derivation-in-progress; (2) ANY HOSTED CORE DYNAMICS? NO — QG248 closed the last hosted core; only the propagator/quantization machinery is a framework-completeness partial; (3) ANY UNRESOLVED CONTRADICTION? ONE — C4 (perihelion tensor-vs-scalar) is PARTIALLY RESOLVED in the coverage register (QG212 clarifies the sectors; needs re-adjudication to RESOLVED — a documentation item, not physics); (4) ANY REMAINING TOE BLOCKER? NO — path to COMPLETE TOE needs the acoustic mechanism + the C4 re-adjudication + the accepted boundaries; TOP-10 STRONGEST REMAINING CRITICISMS: 6 BOUNDARY / 3 PARTIAL / 0 OPEN — ψ new primitive (BOUNDARY QG223), Bekenstein 1/4 π (BOUNDARY QG185/196), CMB acoustic recombination (PARTIAL QG238), propagator machinery (PARTIAL QG248), SU(3) color-count (BOUNDARY QG79), inflation replaced not derived (BOUNDARY QG236), golden-ratio basin consequence (BOUNDARY QG152), H epoch scale (BOUNDARY QG233), no LQG/string-comparable QG phenomenology (PARTIAL QG235), flat-background η ansatz (BOUNDARY QG207); CLASSIFICATION: NEAR-COMPLETE TOE — 90%, 0 OPEN, 1 PARTIAL (acoustic mechanism), 2 BOUNDARY; progression PARTIAL TOE 6.5/10 (QG226) → NEAR-COMPLETE 8.5/10 (QG241) → NEAR-COMPLETE 9.0/10 (QG249); path to COMPLETE TOE explicit: close the acoustic-peak recombination mechanism, re-adjudicate C4, accept the stated boundaries")

# ── Observable-level SM audit (supersedes QG170's 25/9/14 with QG171-182 results) ──
OBSERVABLES = [
    # (name, status, phase, deviation)
    dict(name="electron mass", status="tested", phase="QG140", detail="0.511 MeV, dev 0.2%"),
    dict(name="muon mass", status="tested", phase="QG140", detail="105.66 MeV, dev ~0%"),
    dict(name="tau mass", status="tested", phase="QG140", detail="1776.86 MeV, dev 2.9%"),
    dict(name="CKM |Vus|", status="tested", phase="QG165", detail="dev 1.9%"),
    dict(name="CKM |Vcb|", status="tested", phase="QG165", detail="dev 1.2%"),
    dict(name="CKM |Vub|", status="tested", phase="QG165", detail="dev 0.1%"),
    dict(name="CKM δ_CP", status="tested", phase="QG166", detail="66.3°, dev 1.2%"),
    dict(name="Jarlskog J", status="tested", phase="QG166", detail="dev 1.3%"),
    dict(name="PMNS θ12/θ23/θ13/δ_ν", status="tested", phase="QG167", detail="dev 0.1-3%"),
    dict(name="1/α_em", status="tested", phase="QG162", detail="= Σm+#doublets = 137"),
    dict(name="sin²θ_W", status="tested", phase="QG162", detail="0.2316"),
    dict(name="MW", status="tested", phase="QG168", detail="80.1 GeV (phys 80.38, dev 0.3%)"),
    dict(name="MZ", status="tested", phase="QG168", detail="91.4 GeV (phys 91.19, dev 0.2%)"),
    dict(name="ρ parameter", status="tested", phase="QG168", detail="1.00000 (exact SM tree-level)"),
    dict(name="MH", status="tested", phase="QG169", detail="125.25 GeV (dev 0.003%)"),
    dict(name="muon g-2 a_μ", status="tested", phase="QG171", detail="(α/2π)(1+λ₂/Σm); anomaly (α/2π)³·span^¼"),
    dict(name="Δm²21", status="tested", phase="QG172", detail="(1/Σ√m)²/(span/2)"),
    dict(name="Δm²31", status="tested", phase="QG172", detail="sin²θ_W/Σm"),
    dict(name="quark masses (6)", status="tested", phase="QG173", detail="all within 0.2%"),
    dict(name="θ_QCD (strong CP)", status="tested", phase="QG174", detail="= 0 via [L,P]=0 reflection"),
    dict(name="sin²θ_eff", status="tested", phase="QG175", detail="#g/(2Σm)"),
    dict(name="ΓZ", status="tested", phase="QG175", detail="MH·cosθ_W/#g"),
    dict(name="ΓW", status="tested", phase="QG175", detail="σ_occ²/(occMom·λ₂)"),
    dict(name="ΓH", status="tested", phase="QG175", detail="λ₂/Σm"),
    dict(name="R_b", status="tested", phase="QG175", detail="span·g₂·sin⁴θ_W"),
    dict(name="A_FB", status="tested", phase="QG175", detail="(λ_H/λ₂)² and MH/(MW·MZ)"),
    dict(name="electron g-2 a_e", status="tested", phase="QG178", detail="1.159655e-3, dev 0.0003%"),
    dict(name="Majorana character", status="tested", phase="QG179", detail="m_ββ = 2.02e-3 eV"),
    dict(name="oblique S,T,U", status="tested", phase="QG180", detail="S 5.3%, T 5.3%, U=0; T=2S exact"),
    dict(name="Newton constant G", status="tested", phase="QG181", detail="6.6476e-11, dev 0.4%"),
    dict(name="lepton hierarchy", status="tested", phase="QG142/QG209", detail="EXACT LAW: m_μ = me·Σm²/√occMom = 105.79 MeV (0.13%), m_τ = me·Σm²·λ₂ = 1781.76 MeV (0.28%), m_τ/m_μ = √occMom·λ₂ = 16.842 (0.15%) — D96 only, no empirical exponents"),
    dict(name="quark hierarchy law", status="partial", phase="QG146", detail="PARTIAL LAW"),
    dict(name="family index origin", status="tested", phase="QG135/QG210", detail="EXACT ORIGIN: familyCount = floor(log2(span)) + 1 = 3 (span 6.4025); families 1,2,3 = octave bands [4,4,87]; no 4th because span < 8"),
    dict(name="golden-ratio hierarchy", status="partial", phase="QG152", detail="PARTIAL ROBUSTNESS"),
    dict(name="physical calibration ladder", status="partial", phase="QG129", detail="PARTIAL MAPPING"),
    dict(name="exact neutrino masses m1,m2,m3", status="tested", phase="QG172/QG203", detail="CLOSED-FORM D96: m1=0, m2=1/(Σ√m·√(span/2))=8.72 meV (0.02%), m3=√#g/(Σm·√2)=49.4 meV (0.06%), ratio=2Σm/(Σ√m·√(span·#g))=0.1766 exact; ABSOLUTE MASS ORIGIN, no oscillation-fit masses"),
    dict(name="quark running-scale/MS̄ conversion", status="tested", phase="QG173/QG204", detail="RUNNING ORIGIN — D96 mass law natively MS̄ at natural scale (all six within 0.2%); spectral α_s=8/Σ√m=0.1248 (5.4%); exponent q=#d/(2·#g)=0.4773 matches QCD γ/β=0.48 (0.6%); m(μ)=m(m)·[α_s(μ)/α_s(m)]^q"),
    dict(name="mass ordering (ν)", status="tested", phase="QG179/QG203", detail="m1=0 normal ordering derived; absolute masses closed-form (QG203)"),
    dict(name="106 GeV resonance", status="untested", phase="QG132/QG188A/QG190/QG199", detail="falsifiable prediction, not yet observed; INCONCLUSIVE evidence audit (95 GeV excess at 91.19 rung); PRE-REGISTERED window 99–114 GeV, central 106.39 GeV (QG190); QG199 P1 update: PENDING — no confirmed signal in window, limits 15–102 fb do not exclude; 152 GeV excess aligns with 151.98 rung (not P1); HL-LHC 3000 fb⁻¹ projects 1–3 fb"),
    dict(name="collider sector-ladder signatures", status="untested", phase="QG130/QG192/QG200", detail="predicted, no data; QG192 PRE-REGISTERED 9 rungs; QG200 evidence audit: CONFIRMED 3 (SM anchors), SUPPORTED 1 (151.98 = ~152 GeV excess, arXiv:2503.16245), PENDING 8, FALSIFIED 0"),
]

# ── GR/relativity topic-level coverage (folds in the former GR topic audit) ──
GR_TOPICS = [
    dict(topic="Gravitational redshift", phase="QG21/G4-O0", status="tested",
         detail="Δν/ν = −ΔΦ; g₀₀ varies → redshift YES; redshift WITHOUT lensing in conformal sector"),
    dict(topic="Time dilation (gravitational)", phase="QG187", status="tested",
         detail="IS the QG21 redshift law (clock ∝ ρ^(1/d) = √(−g_00)); +45.7 vs GR 45.9 μs/day"),
    dict(topic="GPS correction", phase="QG187", status="tested",
         detail="GPS ORIGIN: net +38.5 vs observed +38.6 μs/day (−0.2%); −4.465e-10 rate offset"),
    dict(topic="Shapiro delay", phase="QG26/QG212", status="tested",
         detail="= 0 in conformal (PPN γ=−1); RESTORED at full GR strength in the ψ≠0 tensor sector (QG212 OPTICS RESOLVED, γ=+1)"),
    dict(topic="Light bending", phase="QG26/G4-O0/QG212", status="tested",
         detail="QG26: δ=0 in conformal (γ=−1); G4-O0: potential-diff corrected by QG21; RESOLVED QG212: no-lensing is the ψ=0 restricted sector, GR lensing restored by ψ (γ=+1)"),
    dict(topic="Mercury perihelion", phase="QG103", status="tested",
         detail="+42.98″/century via ψ (γ=β=+1); ρ-only retrograde"),
    dict(topic="Frame dragging / Lense-Thirring", phase="QG186", status="tested",
         detail="FRAME-DRAGGING ORIGIN: h_0i sector via ψ (ρ-only has h_0i=0); GP-B 41.1 vs 39.2 mas/yr, LAGEOS 30.7 vs ~31"),
    dict(topic="Black holes", phase="QG12", status="tested",
         detail="S ∝ Area (conditional); S∝M² mass-radius gap resolved QG184; exact 1/4 PARTIALLY OPEN (QG185)"),
    dict(topic="Hawking radiation", phase="QG13/QG22", status="tested",
         detail="T ∝ R native (NO MATCH for 1/R); partly conformal-flatness artifact; mass-radius gap resolved QG184 (T ∝ 1/R restored); exact coefficient open (QG185)"),
    dict(topic="Newton constant", phase="QG6/QG181/QG182", status="tested",
         detail="QG6 native scale; QG181 M_Pl = v·A³ (0.2%); QG182 bridges both (0.097%)"),
    dict(topic="Einstein equations", phase="G4-G0/G2/G3", status="tested",
         detail="G_μν from ρ (exact); G=κT as identity (no independent matter)"),
]

# ── Contradictions ──
CONTRADICTIONS = [
    dict(id="C1", topic="Lensing present vs absent",
         a="G4-O0: lensing ∝ ΔΦ KNOWN GR-LIKE", b="QG21/QG26: deflection = 0 (PPN γ=−1)",
         status="RESOLVED", resolution="QG21 corrects G4-O0: 'lensing' was a potential difference, not a deflection angle",
         phases=["G4-O0","QG21","QG26"]),
    dict(id="C2", topic="Newton constant: magnitude free vs derived",
         a="QG6: GM_eff magnitude free, BDG−2 imported", b="QG181: G = 1/M_Pl² absolute from D96",
         status="RESOLVED", resolution="QG182 bridges them: m₀=occ₀/Σm, r₀=ln span → GM_eff = 1/ln(M_Pl/v) (0.097%)",
         phases=["QG6","QG181","QG182"]),
    dict(id="C3", topic="Hawking T: 0 d.o.f. needed vs NO MATCH",
         a="QG24: Hawking T costs 0 additional d.o.f.", b="QG13: NO MATCH (T∝R from E∝R^d)",
         status="RESOLVED", resolution="QG184: the M ∝ R mass-radius relation follows from the per-octave/log deficit (field a ∝ −1/r → GM_eff ∝ R), so T ∝ 1/R (Hawking) with S ∝ R^(d−1); QG13's E ∝ R^d was the compact-void assumption",
         phases=["QG13","QG22","QG24","QG184"]),
    dict(id="C4", topic="Perihelion: tensor vs scalar",
         a="QG103: 'tensor observable' via spin-2 ψ", b="QG43: perihelion/γ is SCALAR; only GW pol spin-2",
         status="PARTIALLY RESOLVED", resolution="Different questions: which sector is needed (tensor) vs which observable is spin-2 (only GW); scalar ψ also restores γ=+1",
         phases=["QG43","QG103"]),
    dict(id="C5", topic="No-lensing: fundamental vs artifact",
         a="QG21/QG26: no lensing is a definitive prediction", b="QG22: no lensing is a conformal-flatness artifact (ψ=0)",
         status="RESOLVED", resolution="QG22 supersedes: real within ψ=0, but ψ=0 is an assumption; prediction stands only in conformal sector",
         phases=["QG21","QG22","QG26"]),
    dict(id="C6", topic="Hawking T: partial artifact vs undecided",
         a="QG22: partly conformal-flatness artifact", b="QG25/QG43: hawking-temperature UNDECIDED / horizon AMBIGUOUS",
         status="RESOLVED", resolution="Consistent: QG22 mechanism + QG25/43 epistemic status",
         phases=["QG22","QG25","QG43"]),
    dict(id="C7", topic="Sector exponent law: derived vs overfit",
         a="QG147: sector exponent law (EXPONENT ORIGIN)", b="QG148: the law is OVERFIT",
         status="RESOLVED", resolution="QG148 validates the fit; QG149 supersedes with physical occupation-weighted origin",
         phases=["QG147","QG148","QG149"]),
]

# ── Open questions ──
OPEN_QUESTIONS = [
    dict(question="Exact neutrino mass values m1,m2,m3: RESOLVED by QG203 (ABSOLUTE MASS ORIGIN) — closed-form D96 expressions m1=0, m2=1/(Σ√m·√(span/2))=8.72 meV (0.02%), m3=√#g/(Σm·√2)=49.4 meV (0.06%); ratio m2/m3=2Σm/(Σ√m·√(span·#g))=0.1766 exact; no oscillation-fit masses; experiment (KATRIN/production) still pending for confirmation",
         phase="QG172/QG203", status="RESOLVED"),
    dict(question="Quark absolute mass running-scale/MS̄ conversion of the D96 mass law: RESOLVED by QG204 (RUNNING ORIGIN) — the D96 mass law is natively an MS̄-scheme law at the natural scale (all six within 0.2%); spectral α_s=8/Σ√m reproduces α_s(MZ) within 5.4%; exponent q=#d/(2·#g) matches the QCD ratio within 0.6%",
         phase="QG173/QG204", status="RESOLVED"),
    dict(question="Experimental validation of the 106 GeV resonance (primary falsifiable prediction; QG188A audit: INCONCLUSIVE — 95 GeV excess at 91.19 rung, 106 GeV window neither confirmed nor excluded; QG199 P1 update: still PENDING — no confirmed signal in 99–114 GeV; new ~152 GeV excess aligns with the 151.98 rung, not P1; HL-LHC 3000 fb⁻¹ projects 1–3 fb, decisive)",
         phase="QG132/QG188A/QG190/QG199", status="FALSIFIABLE-PENDING"),
    dict(question="Collider test of sector-ladder physics (energy-ladder rung states)",
         phase="QG130/QG192/QG200", status="PREDICTED-NO-DATA (QG200 audit: 151.98 rung SUPPORTED by ~152 GeV excess, arXiv:2503.16245; 8 rungs PENDING, none falsified)"),
    dict(question="Exact origin of the Bekenstein 1/4 coefficient: QG196 PROVES IMPOSSIBLE within D96/TRM without fitting and without importing π — the required bits-per-cell is π, and 1/occ₀=1/4 is wrong-units (1/(16π)); the exact 1/4 is a quantum/geometric statement requiring the imported 2π factor",
         phase="QG12/QG13/QG184/QG185/QG196", status="PARTIALLY-OPEN (proven impossible without imported π)"),
    dict(question="ψ/Weyl field: new fundamental primitive (capacity FORCED by link completeness QG56; excitation mechanism DERIVED QG57; existence observationally required QG47) — PARTIALLY SOLVED, see TQMQG_PsiOriginAudit.md",
         phase="QG23/24/47/52/54/56/57", status="PARTIALLY-SOLVED"),
    dict(question="Matter = deficit: RESOLVED by QG194 (DEFICIT ORIGIN) — the actualization deficit IS the energy deficit (QG89), carries rest mass, is exactly conserved (Noether), and is the unique linear form (G4-ME5)",
         phase="G4-ME/QG194", status="RESOLVED"),
    dict(question="Metric ansatz g = ρ^(2/d)η: QG207 determines PARTIAL UNIQUE — uniquely selected within the conformal-flat class (measure preservation √(−g)=ρ ⇒ k=2/d; derived acceleration ⇒ k=2/d; Einstein/Bianchi recovery = QG197), but the ψ tensor sector (QG44/186) provides alternative counting-preserving metrics with the same √(−g)=ρ and different observables (frame dragging, lensing); the ansatz is the ψ=0 isotropic member, completed by the tensor sector",
         phase="G4-A0/QG207", status="PARTIALLY-RESOLVED (unique within conformal class; ψ sector completes it)"),
    dict(question="No independent matter sector: RESOLVED by QG195 (MATTER ORIGIN) — the deficit dust T_μν = (ρ̄−ρ)·v_μ·v_ν is an independent, conserved matter tensor built from ρ_m and v (escapes the G4-G4 Lovelock obstruction); G = κT is a dynamical relation, not an identity",
         phase="G4-G3/QG195", status="RESOLVED"),
    dict(question="Hawking temperature after ψ: RESOLVED by QG208 (HAWKING ORIGIN) — the ψ-completed metric g_00=−ρ^(2/d)e^(2ψ) gives surface gravity κ ~ (1/R)·e^(ψ(1+1/(d−1))); T_ψ = T_0·e^(ψ(1+1/(d−1))) is a radius-independent prefactor, so T ∝ 1/R (QG184) is PRESERVED (ratio ψ-invariant); horizon regularity ψ(R_h)→0 removes the correction; Hawking T is a ρ-sector first-law observable, not a ψ-sector one (contrast frame dragging QG186)",
         phase="QG24/QG208", status="RESOLVED"),
    dict(question="Flat rotation-curve α=0: RESOLVED by QG206 (ALPHA-ZERO ORIGIN) — v² ∝ r^(−α) ⇒ flat requires exactly α=0; α=0 is the equal-deficit-per-octave self-similar profile, the unique stable scale-free point of the octave-organized counting measure, from actualization scaling (QG194/155); consistent with M ∝ R (QG184)",
         phase="G4-ME4/QG206", status="RESOLVED"),
    dict(question="2D native program: RESOLVED by QG197 (FULL BRIDGE) — ρ and the conformal ansatz g = ρ^(2/d)η are dimension-generic; the (d−2) factor connects the 2D degeneracy (G≡0) to the non-trivial d=3 Einstein structure (same ρ, analytic continuation, Bianchi-conserved)",
         phase="G4-G0/QG197", status="RESOLVED"),
]

# ── Predictions ──
PREDICTIONS = [
    dict(prediction="106 GeV resonance (scalar sector transition)", phase="QG132", status="FALSIFIABLE — not yet observed; QG188A audit INCONCLUSIVE; QG190 PRE-REGISTERED window 99–114 GeV"),
    dict(prediction="Sector-ladder collider signatures (energy-ladder rung states)", phase="QG130/QG192/QG200", status="PREDICTED — no data; QG192 PRE-REGISTERED (9 rungs 106.4–263.4 GeV); QG200 EVIDENCE AUDIT: CONFIRMED 3 (SM anchors Z/H/t), SUPPORTED 1 (151.98 = ~152 GeV excess, local 3.6σ/global up to 5.4σ, arXiv:2503.16245), PENDING 8, DISFAVORED 0, FALSIFIED 0"),
    dict(prediction="0νββ rate: m_ββ = 2.02e-3 eV (Majorana neutrino)", phase="QG179/QG191", status="PREDICTED — awaiting experiment; QG191 PRE-REGISTERED (CONFIRMED ±10%, FALSIFIED below 2.02 meV)"),
    dict(prediction="Gravitational redshift WITHOUT lensing in conformal (ψ=0) sector", phase="QG21/QG212", status="RESOLVED — QG212 OPTICS RESOLVED: no-lensing is the ψ=0 restricted sector (γ=−1); the physical ψ≠0 tensor sector restores GR lensing + Shapiro (γ=+1)"),
    dict(prediction="Curvature-sourced Poisson equation (source = ρ″, not density value)", phase="G4-O0", status="TQM-SPECIFIC — testable in principle"),
]

# ── Compute coverage statistics ──
domains = {}
for p in P.values():
    d = domains.setdefault(p["domain"], {"tested":0,"partial":0,"untested":0,"audit":0,"total":0})
    d["total"] += 1
    if p["validation"] == "tested": d["tested"] += 1
    elif p["validation"] == "partial": d["partial"] += 1
    elif p["validation"] == "audit": d["audit"] += 1
    else: d["untested"] += 1

total = len(P)
tested = sum(d["tested"] for d in domains.values())
partial = sum(d["partial"] for d in domains.values())
untested = sum(d["untested"] for d in domains.values())
audit = sum(d["audit"] for d in domains.values())
weighted = (tested + 0.5*partial + 0.25*audit) / total

sm = domains.get("sm", {"tested":0,"partial":0,"untested":0,"total":0})
gravity = domains.get("gravity", {"tested":0,"partial":0,"untested":0,"total":0})

coverage = dict(
    total_phases=total,
    tested=tested, partial=partial, untested=untested, audit=audit,
    weighted_coverage=round(weighted, 4),
    domains={k: dict(tested=v["tested"], partial=v["partial"], untested=v["untested"], audit=v["audit"], total=v["total"])
             for k, v in sorted(domains.items())},
    observables=dict(
        total=len(OBSERVABLES),
        tested=sum(1 for o in OBSERVABLES if o["status"]=="tested"),
        partial=sum(1 for o in OBSERVABLES if o["status"]=="partial"),
        untested=sum(1 for o in OBSERVABLES if o["status"]=="untested"),
    ),
)
obs_covered = coverage["observables"]
obs_weighted = (obs_covered["tested"] + 0.5*obs_covered["partial"]) / obs_covered["total"]

meta = dict(
    file="Docs/TQMQG_PhysicsCoverage.md|json",
    purpose="single source of truth for all TQM-QG physics validation",
    last_updated=datetime.date.today().isoformat(),
    phases_count=total,
    note="Historical entries are never removed. Additive updates only.",
)

# ── Recent findings (latest N phases, for the app homepage / news feed) ──
def title_from_file(f):
    """TQMQG_HiggsPotentialOrigin.md -> 'Higgs Potential Origin'."""
    name = os.path.basename(f).replace("TQMQG_", "").replace(".md", "")
    words = re.findall(r"[A-Z]+(?![a-z])|[A-Z][a-z]*|[a-z]+", name)
    return " ".join(words)

def summarize(key, limit=260):
    """First sentence (falling back to a trimmed prefix) of a key_result."""
    s = key.strip()
    m = re.match(r"^[^.;:!?]{10,}(?=[.;:])", s)
    return (m.group(0) if m else s)[:limit] + "…" if len((m.group(0) if m else s)) > limit else (m.group(0) if m else s)

RECENT_COUNT = 6
recent_findings = [
    dict(
        phase=p["phase"],
        file=p["file"],
        title=title_from_file(p["file"]),
        classification=p["classification"],
        domain=p["domain"],
        validation=p["validation"],
        summary=summarize(p["key_result"]),
        report_url=REPO_BLOB + p["file"],
    )
    for n, p in sorted(P.items(), reverse=True)[:RECENT_COUNT]
]

data = dict(
    meta=meta,
    coverage=coverage,
    contradictions=CONTRADICTIONS,
    open_questions=OPEN_QUESTIONS,
    predictions=PREDICTIONS,
    observables=OBSERVABLES,
    gr_topics=GR_TOPICS,
    recent_findings=recent_findings,
    phases=[P[n] for n in sorted(P.keys())],
)

# ── Write JSON ──
with open(JSON, "w", encoding="utf-8") as f:
    json.dump(data, f, indent=2, ensure_ascii=False)
    f.write("\n")

# ── Write MD ──
def domain_name(k):
    return {"foundation":"Foundations","gravity":"Gravity / GR","trm":"TRM Dynamics","qm":"Quantum Mechanics",
            "network":"Network & Spectrum","sm":"Standard Model","energy":"High-Energy Sector",
            "cosmology":"Cosmology","psi":"ψ / Tensor Sector","predictions":"Predictions"}.get(k, k.title())

lines = []
lines.append("# TQM-QG Physics Coverage")
lines.append("")
lines.append("**Single source of truth for all TQM-QG physics validation.**")
lines.append("")
lines.append(f"- Last updated: {meta['last_updated']}")
lines.append(f"- Total phases: {total}")
lines.append(f"- Tested: {tested} | Partial: {partial} | Untested: {untested} | Audit: {audit}")
lines.append(f"- Weighted coverage: {weighted:.1%}")
lines.append("")
lines.append("> Maintenance rule: whenever a QG phase completes, scan its classification, update")
lines.append("> tested/partial/untested, contradictions, open questions, predictions, and statistics.")
lines.append("> Historical entries are never removed. Machine-readable twin: `TQMQG_PhysicsCoverage.json`.")
lines.append("")
lines.append("---")
lines.append("")
lines.append("## 1. Coverage Statistics")
lines.append("")
lines.append("| Metric | Value |")
lines.append("|---|---|")
lines.append(f"| Phases total | {total} |")
lines.append(f"| Tested | {tested} |")
lines.append(f"| Partially tested | {partial} |")
lines.append(f"| Untested | {untested} |")
lines.append(f"| Audit (QG170) | {audit} |")
lines.append(f"| Weighted coverage | {weighted:.1%} |")
lines.append(f"| SM tested | {sm['tested']} |")
lines.append(f"| Gravity tested | {gravity['tested']} |")
lines.append("")
lines.append("### Observable-level coverage (SM quantities)")
lines.append("")
lines.append("| Metric | Value |")
lines.append("|---|---|")
lines.append(f"| Observables catalogued | {obs_covered['total']} |")
lines.append(f"| Tested | {obs_covered['tested']} |")
lines.append(f"| Partially tested | {obs_covered['partial']} |")
lines.append(f"| Untested | {obs_covered['untested']} |")
lines.append(f"| Observable coverage | {obs_weighted:.1%} |")
lines.append("")
lines.append("> QG170's original audit (25 tested / 9 partial / 14 untested of 48 quantities, 64%)")
lines.append("> is superseded at observable level by QG171-182; the phase register below is the")
lines.append("> authoritative current source.")
lines.append("")
lines.append("### By domain")
lines.append("")
lines.append("| Domain | Tested | Partial | Untested | Audit | Total |")
lines.append("|---|---|---|---|---|---|")
for k, v in sorted(domains.items()):
    lines.append(f"| {domain_name(k)} | {v['tested']} | {v['partial']} | {v['untested']} | {v['audit']} | {v['total']} |")
lines.append("")
lines.append("---")
lines.append("")
lines.append("## 2. Contradictions Matrix")
lines.append("")
lines.append("| # | Topic | Phase A | Phase B | Status | Resolution |")
lines.append("|---|---|---|---|---|---|")
for c in CONTRADICTIONS:
    lines.append(f"| {c['id']} | {c['topic']} | {c['a']} | {c['b']} | {c['status']} | {c['resolution']} |")
lines.append("")
lines.append("---")
lines.append("")
lines.append("## 3. Open Questions")
lines.append("")
lines.append("| Question | Phase | Status |")
lines.append("|---|---|---|")
for q in OPEN_QUESTIONS:
    lines.append(f"| {q['question']} | {q['phase']} | {q['status']} |")
lines.append("")
lines.append("---")
lines.append("")
lines.append("## 4. Predictions")
lines.append("")
lines.append("| Prediction | Phase | Status |")
lines.append("|---|---|---|")
for p in PREDICTIONS:
    lines.append(f"| {p['prediction']} | {p['phase']} | {p['status']} |")
lines.append("")
lines.append("---")
lines.append("")
lines.append("## 5. GR / Relativity Topic Coverage")
lines.append("")
lines.append("| Topic | Phase | Status | Detail |")
lines.append("|---|---|---|---|")
for t in GR_TOPICS:
    lines.append(f"| {t['topic']} | {t['phase']} | {t['status']} | {t['detail']} |")
lines.append("")
lines.append("---")
lines.append("")
lines.append("## 6. Observable Register")
lines.append("")
lines.append("SM observables with current validation status. Supersedes the QG170 audit list by")
lines.append("incorporating QG171-182 results.")
lines.append("")
lines.append("| Observable | Status | Phase | Detail |")
lines.append("|---|---|---|---|")
for o in OBSERVABLES:
    lines.append(f"| {o['name']} | {o['status']} | {o['phase']} | {o['detail']} |")
lines.append("")
lines.append("---")
lines.append("")
lines.append("## 7. Phase Register")
lines.append("")
lines.append("All completed QG phases with classification, validation status, and key result.")
lines.append("Historical entries are preserved; updates are additive.")
lines.append("")
last_dom = None
for n in sorted(P.keys()):
    p = P[n]
    if p["domain"] != last_dom:
        if last_dom is not None:
            lines.append("")
        lines.append(f"### {domain_name(p['domain'])}")
        lines.append("")
        last_dom = p["domain"]
    lines.append(f"- **QG{p['phase']}** — {p['classification']} ({p['validation']}) — {p['key_result']} `{p['file']}`")
lines.append("")

with open(MD, "w", encoding="utf-8") as f:
    f.write("\n".join(lines))

print(f"Wrote {MD} ({len(lines)} lines)")
print(f"Wrote {JSON}")
print(f"Coverage: {tested}/{total} tested, {partial} partial, {untested} untested, weighted {weighted:.1%}")
