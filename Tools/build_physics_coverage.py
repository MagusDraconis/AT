# -*- coding: utf-8 -*-
"""Generate Docs/TQMQG_PhysicsCoverage.md and Docs/TQMQG_PhysicsCoverage.json.

Single source of truth for all TQM-QG physics validation.
Run:  python Tools/build_physics_coverage.py
"""
import json, datetime, os, sys

sys.stdout.reconfigure(encoding='utf-8')
ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
MD = os.path.join(ROOT, "Docs", "TQMQG_PhysicsCoverage.md")
JSON = os.path.join(ROOT, "Docs", "TQMQG_PhysicsCoverage.json")

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
    dict(name="lepton hierarchy", status="partial", phase="QG142", detail="PARTIAL LAW"),
    dict(name="quark hierarchy law", status="partial", phase="QG146", detail="PARTIAL LAW"),
    dict(name="family index origin", status="partial", phase="QG135", detail="PARTIAL ORIGIN"),
    dict(name="golden-ratio hierarchy", status="partial", phase="QG152", detail="PARTIAL ROBUSTNESS"),
    dict(name="physical calibration ladder", status="partial", phase="QG129", detail="PARTIAL MAPPING"),
    dict(name="exact neutrino masses m1,m2,m3", status="untested", phase="—", detail="splittings derived (QG172); absolute values open"),
    dict(name="quark running-scale/MS̄ conversion", status="untested", phase="—", detail="D96 mass law at MS̄ scale open"),
    dict(name="mass ordering (ν)", status="partial", phase="QG179", detail="m1=0 normal ordering derived; experiment pending"),
    dict(name="106 GeV resonance", status="untested", phase="QG132/QG188A/QG190", detail="falsifiable prediction, not yet observed; INCONCLUSIVE evidence audit (95 GeV excess at 91.19 rung); PRE-REGISTERED window 99–114 GeV, central 106.39 GeV (QG190)"),
    dict(name="collider sector-ladder signatures", status="untested", phase="QG130", detail="predicted, no data"),
]

# ── GR/relativity topic-level coverage (folds in the former GR topic audit) ──
GR_TOPICS = [
    dict(topic="Gravitational redshift", phase="QG21/G4-O0", status="tested",
         detail="Δν/ν = −ΔΦ; g₀₀ varies → redshift YES; redshift WITHOUT lensing in conformal sector"),
    dict(topic="Time dilation (gravitational)", phase="QG187", status="tested",
         detail="IS the QG21 redshift law (clock ∝ ρ^(1/d) = √(−g_00)); +45.7 vs GR 45.9 μs/day"),
    dict(topic="GPS correction", phase="QG187", status="tested",
         detail="GPS ORIGIN: net +38.5 vs observed +38.6 μs/day (−0.2%); −4.465e-10 rate offset"),
    dict(topic="Shapiro delay", phase="QG26", status="tested",
         detail="= 0 in conformal (PPN γ=−1); would need ψ≠0 (QG22)"),
    dict(topic="Light bending", phase="QG26/G4-O0", status="tested",
         detail="QG26: NO MATCH (δ=0); G4-O0: weak-field lensing ∝ ΔΦ (potential diff, corrected by QG21)"),
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
    dict(question="Exact neutrino mass values m1,m2,m3 (splittings derived QG172; m1=0 normal ordering)",
         phase="QG172", status="OPEN"),
    dict(question="Quark absolute mass running-scale/MS̄ conversion of the D96 mass law",
         phase="QG173", status="OPEN"),
    dict(question="Experimental validation of the 106 GeV resonance (primary falsifiable prediction; QG188A audit: INCONCLUSIVE — 95 GeV excess at 91.19 rung, 106 GeV window neither confirmed nor excluded)",
         phase="QG132/QG188A", status="FALSIFIABLE-PENDING"),
    dict(question="Collider test of sector-ladder physics (energy-ladder rung states)",
         phase="QG130", status="PREDICTED-NO-DATA"),
    dict(question="Exact origin of the Bekenstein 1/4 coefficient: QG196 PROVES IMPOSSIBLE within D96/TRM without fitting and without importing π — the required bits-per-cell is π, and 1/occ₀=1/4 is wrong-units (1/(16π)); the exact 1/4 is a quantum/geometric statement requiring the imported 2π factor",
         phase="QG12/QG13/QG184/QG185/QG196", status="PARTIALLY-OPEN (proven impossible without imported π)"),
    dict(question="ψ/Weyl field: new fundamental primitive (capacity FORCED by link completeness QG56; excitation mechanism DERIVED QG57; existence observationally required QG47) — PARTIALLY SOLVED, see TQMQG_PsiOriginAudit.md",
         phase="QG23/24/47/52/54/56/57", status="PARTIALLY-SOLVED"),
    dict(question="Matter = deficit: RESOLVED by QG194 (DEFICIT ORIGIN) — the actualization deficit IS the energy deficit (QG89), carries rest mass, is exactly conserved (Noether), and is the unique linear form (G4-ME5)",
         phase="G4-ME/QG194", status="RESOLVED"),
    dict(question="Metric ansatz g = ρ^(2/d)η is PREFERRED but not UNIQUE; flat η is a defining axiom",
         phase="G4-A0", status="OPEN-AXIOM"),
    dict(question="No independent matter sector: RESOLVED by QG195 (MATTER ORIGIN) — the deficit dust T_μν = (ρ̄−ρ)·v_μ·v_ν is an independent, conserved matter tensor built from ρ_m and v (escapes the G4-G4 Lovelock obstruction); G = κT is a dynamical relation, not an identity",
         phase="G4-G3/QG195", status="RESOLVED"),
    dict(question="Hawking temperature after ψ: no phase derives T ∝ 1/R explicitly with ψ≠0",
         phase="QG24", status="OPEN"),
    dict(question="Flat rotation-curve α=0: SEMI-NATURAL, symmetry assumption not derived",
         phase="G4-ME4", status="OPEN"),
    dict(question="2D native program: RESOLVED by QG197 (FULL BRIDGE) — ρ and the conformal ansatz g = ρ^(2/d)η are dimension-generic; the (d−2) factor connects the 2D degeneracy (G≡0) to the non-trivial d=3 Einstein structure (same ρ, analytic continuation, Bianchi-conserved)",
         phase="G4-G0/QG197", status="RESOLVED"),
]

# ── Predictions ──
PREDICTIONS = [
    dict(prediction="106 GeV resonance (scalar sector transition)", phase="QG132", status="FALSIFIABLE — not yet observed; QG188A audit INCONCLUSIVE; QG190 PRE-REGISTERED window 99–114 GeV"),
    dict(prediction="Sector-ladder collider signatures (energy-ladder rung states)", phase="QG130", status="PREDICTED — no data; QG192 PRE-REGISTERED (9 rungs 106.4–263.4 GeV, CONFIRMED within 5% of frozen rung)"),
    dict(prediction="0νββ rate: m_ββ = 2.02e-3 eV (Majorana neutrino)", phase="QG179/QG191", status="PREDICTED — awaiting experiment; QG191 PRE-REGISTERED (CONFIRMED ±10%, FALSIFIED below 2.02 meV)"),
    dict(prediction="Gravitational redshift WITHOUT lensing in conformal (ψ=0) sector", phase="QG21", status="FALSIFIABLE — differs from GR"),
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

data = dict(
    meta=meta,
    coverage=coverage,
    contradictions=CONTRADICTIONS,
    open_questions=OPEN_QUESTIONS,
    predictions=PREDICTIONS,
    observables=OBSERVABLES,
    gr_topics=GR_TOPICS,
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
