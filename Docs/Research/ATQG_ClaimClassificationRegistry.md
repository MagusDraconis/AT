# ATQG Claim Classification Registry

**Purpose:** permanent registry classifying every major AT claim by derivational status.
**Source of truth:** DERIVATION_AUDITOR (2026-08-27 hostile-referee/derivation audit pass).
**Categories:** theorem · necessity · correspondence · calibration · hosted · fit.
**Rules:** no physics changes are implied by any classification; classifications are
wording/status corrections only. This registry is the referee-safe reading for all
monograph chapters.

---

## Classification Legend

| Category | Meaning |
|---|---|
| **theorem** | mathematically verified consequence of the stated structure; reproducible |
| **necessity** | forced within the stated (possibly scoped) assumptions; may be class-scoped |
| **correspondence** | a numerical/dimensional match to observation or to a target; no derivation mechanism |
| **calibration** | value obtained only after multiplication by a measured anchor (v, m_e, SI conversion) |
| **hosted** | carried as an external structure/input; explicitly not derived |
| **fit** | value/status chosen (or tuned) to match a target; includes selection-from-candidates |

---

## Registry

| Claim | Status | Evidence | Current wording | Required wording |
|---|---|---|---|---|
| **N=96** | necessity (scoped) | QG159/QG295: unique within the canonical structural class (period-3 seed, Z2 half-shift, three-family octave window, tested comparison set); global uniqueness not proved; Ch5 "Exact status" paragraph discloses the boundary | "the inevitable stable fixed point of the dynamics, not a choice" (Ch5 Thm n96-attractor) | "necessary within the accepted structural class (period-3 seed, Z2 half-shift, three-family octave window, tested set); a global proof is not claimed" |
| **D96 spectrum** | theorem (reproducible) | D96_REPRO_AUDIT: the Laplacian of C96(±1..±6) reproduces [42×2,5,6], 95+1 modes, Σm=95, Σ√m=64.08, Σm²=229, span 6.40 (ω=√λ) — all exact | "the Laplacian eigenspectrum of the converged N=96 network, a derived output" (Ch6 Thm d96-derived) | "the Laplacian eigenspectrum of the canonical attractor graph C96(±1..±6), with the graph and eigenvalue formula λ_k = 2Σ(1−cos 2πdk/96) stated (reproducible theorem)" |
| **moment hierarchy** | theorem (values) + correspondence (sector roles) | QG157/158: Σm, Σ√m, Σm², occMom are exact spectral values; the assignment of sector access roles (neutral/full/doublet/octave) is supported, not globally unique | "the forced access-count ladder (Σ√m, Σm, Σm², occMom)" (Ch6 Thm moment-ladder) | "the moment values are exact spectral theorems; the assignment of access roles (neutral/full/doublet/octave) is a supported mapping, not a unique derivation" |
| **sector mappings** | correspondence (selection argument) | DerivationStrengthMatrix: "sector mappings are supported, not uniquely proved; phenomenological assignments on top of the forced ladder"; CurrentStatus: "sector labels are supported but not globally unique" | "the fermion sectors couple through distinct moments" (Ch11 Thm fermion-access) | "the sector labels are supported assignments over the forced moment ladder, not a globally unique mapping" |
| **1+3+8 dimensions** | correspondence | QG161 + operator-sector audit: 1 (zero mode) + 3 (Compression octaves) + 8 (4+4 light-octave modes) = 12 = connectivity; non-unique partition; the numbers are real spectral counts; the degree-12 connectivity (12 = 2K) is conditional on the selected link-length parameter K=6 (QG116b/QG117) — given K=6, the radius-6 attractor and the degree-12 connectivity are dynamical necessities | "the degree-12 structure of the C96 generator ring" (Ch11 Thm gauge-structure) | "the D96 sector counts supply a 1+3+8 partition (1 background, 3 octaves, 8 light modes) matching dim U(1)+dim SU(2)+dim SU(3) = 12 — a dimensional correspondence" |
| **gauge groups U(1)×SU(2)×SU(3)** | hosted | gauge referee audit: finite structure (Aut(C96)=D96, irreps {1,2}-dim) and operator counts cannot generate continuous Lie groups; su(3) unobtainable; groups remain external | "the 1+3+8 gauge sector is reconstructed from the D96 sector structure" (Ch11 Thm gauge-structure) | "the gauge groups and their Lie algebras are hosted; the D96 structure provides only the dimensional correspondence 1+3+8" |
| **CKM** | correspondence | QG165 + ObservableSelectionAudit: V_us=#d/(2Σm), V_cb=(ω0/ω2)^δd, V_ub=2V_cb·(occ0/occ2), mean deviation 0.58%; no free constant (fixed spectral ratios); CKM classified Secondary, medium target-selection dependence (Validation Referee: "fit" too pessimistic) | "derived as the quark mixing read …, mean deviation 0.58%" (Ch11 Thm ckm) | "the CKM elements correspond to spectral ratios matched to observation (0.58% deviation); the ratio forms are selected, not uniquely derived" |
| **PMNS** | correspondence | QG167 + ObservableSelectionAudit: θ12=33.35°, θ23=49.72°, θ13=8.34°, δν=66.4°, mean deviation 1.5%; T3-only spectral reads, no free constant; PMNS classified Secondary (Validation Referee: "fit" too pessimistic) | "derived as the neutrino mixing read …, mean deviation 1.5%" (Ch11 Thm pmns) | "the PMNS angles correspond to T3-only spectral reads matched to observation (1.5% deviation); secondary catalog match, not a unique derivation" |
| **Higgs** | calibration | QG169/176: M_H = σ_occ·(span/2) = 125.25 GeV, 0.003% match; blind reconstruction (hidden target, natural core rank 4) is the strongest anti-fit defense; σ_occ defined at first use (MONO_FREEZE001): σ_occ = √Var[4,4,87] = 39.127 (occupation-density scalar, Ch11); GeV unit from the calibrated weak scale v (Validation Referee: "fit + calibration" too pessimistic) | "M_H = σ_occ·(span/2) = 125.25 GeV, matching observation within 0.003%" (Ch11 eq:higgs) | "M_H is a calibrated reconstruction via the anchor v (blind reconstruction, natural-core status); σ_occ = √Var[4,4,87] = 39.127 defined at first use; the 0.003% match is a calibration, not a derived value" |
| **couplings** | 1/α_em → fit; α_weak → correspondence; α_strong → correspondence | QG162 + ObservableSelectionAudit: 1/α_em = Σm+#d = 137 classified PostHoc, "asserted dictionary", HIGH target-selection dependence → **fit**; α_weak = 3/Σm and α_strong = 8/Σ√m classified Secondary, medium dependence → **correspondence**; no renormalization scale specified (Validation Referee: "correspondence" too optimistic for 1/α_em) | "derived as ratios of D96 spectral constants, with no fitted parameters" (Ch11 eq:couplings) | "1/α_em is a fitted readout (post-hoc match, 137 = 95+42); α_weak and α_strong correspond to spectral ratios (3/95, 8/64.08); none carries a defined renormalization scale" |
| **neutrino splittings** | correspondence + calibration | QG172: Δm²21 = (1/Σ√m)²/(span/2), Δm²31 = sin²θ_W/Σm — closed-form D96 ratios, no free constant; matched to 7.607e-5 / 2.44e-3 eV²; dimensionful values require an implicit eV² scale → calibration (Validation Referee: "fit" too pessimistic) | "splittings are derived as closed-form D96 laws" (Ch11 Lem neutrino-masses) | "the neutrino splittings correspond to closed-form D96 ratios, calibrated to eV² by an implicit scale; the ratios are fixed, the units are calibrated" |
| **gravity** | calibration (+ hosted BH inputs) | QG181-183: M_Pl = v·(Σm·#g·occ₂)³ with v the calibration anchor; SI G additionally imports ħ, c, GeV↔kg (units boundary); black-hole relations (M∝R) import the flat-rotation-curve profile (hosted input) | "the gravitational coupling is derived from the D96 spectral content: M_Pl = v·(Σm·#g·occ₂)³" (Ch10 Thm gravity-derived) | "G is calibrated: D96 natural-unit structure × the anchor v, plus SI conversion; the black-hole relations import the flat-rotation-curve profile as an input" |
| **spacetime** | hosted (metric/signature) + theorem (conformal factor) | QG222 native dynamics + claim-hardening fix: conformal factor ρ^(2/d) and the dynamics are emergent from ρ; the metric tensor g=ρ^(2/d)η and its Lorentzian signature are primitive inputs via η | "the conformal factor and the dynamics are emergent; η and the metric signature remain primitive inputs" (Ch10 Thm spacetime-emergent, post-fix) | "conformal factor and dynamics derived from ρ; the metric tensor and its signature are primitive/hosted inputs via η" (current wording is already correct) |
| **CMB peak existence** | theorem (structural) | PEAK001: the fundamental doublet at ω_min is isolated by the dominant spectral gap (ln-gap 0.680, largest in the spectrum); a first peak is structurally guaranteed — no fitting needed for existence | "ℓ₁ = 220.48 … derived from the D96 octave hierarchy" (Ch12 Thm acoustic, conflates existence and location) | "a first peak exists as a structural theorem (the fundamental doublet is isolated by the dominant spectral gap); existence is structural, location is separate" |
| **CMB peak location** | fit | QG297 (ExceptionAudit): "5/4 = FIT" — no beat identity equals 1.25, no wave mechanism, Noether-rejected free constant (QG255), REMOVABLE (QG289); spectral search found no structural origin within 0.1% (nearest: 2^(1/3) at +0.80%) | "ℓ₁ = Σm·ln(span)·(5/4) = 220.48, derived from the D96 octave hierarchy" (Ch12 Thm acoustic) | "ℓ₁ = 220.48 requires the 5/4 factor, documented as a fitted multiplier (QG297); the location is a fit, not a derivation" |
| **peak ratios** | correspondence | QG238: r21 = (Σm−#d)·occ1/occ3 = 53·4/87 = 2.4368 (0.035%), r31 = span/√3 = 3.6965 (0.058%); pure spectral ratios, no free constant; specific forms selected (Validation Referee: confirmed — cleanest match in the set, no fitted factor) | "follow from the D96 octave hierarchy" (Ch12 Thm acoustic) | "the peak ratios are spectral correspondences (no fitted constant), matched to observation within ~0.05%; the specific ratio forms are selected, not uniquely forced" |

---

## Status Summary

| Category | Claims |
|---|---|
| theorem | D96 spectrum, moment values, CMB peak existence, conformal factor |
| necessity | N=96 (scoped) |
| correspondence | sector mappings, 1+3+8 dimensions, CKM, PMNS, couplings (α_weak, α_strong), neutrino splittings (ratios), peak ratios |
| calibration | gravity (natural-unit part), Higgs (value), neutrino splittings (eV² units), couplings (scale) |
| hosted | gauge groups, spacetime metric/signature, black-hole inputs |
| fit | 1/α_em (couplings), ℓ₁ location |

Summary counts (16 rows; couplings and neutrino splittings straddle categories): theorem 4, necessity 1, correspondence 7, calibration 4, hosted 3, fit 2.

## Audit Trail

- D96_REPRO_AUDIT (graph provenance, verified exact)
- QM_AUDIT001 (Born/interference/measurement, basis separation)
- PEAK001 (CMB first-peak existence vs location)
- gauge referee audits (finite→Lie gap; operator-sector correspondence)
- 5/4 wave/spectral audits (fit, no mechanism)
- DERIVATION_AUDITOR (this classification)

*All classifications are status/wording corrections; no physics, equation, citation, or
numerical value is changed by this registry.*
