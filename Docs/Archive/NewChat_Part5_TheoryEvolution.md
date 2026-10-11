# AT Project Memory — Archive Part 5: Theory Evolution

Historical detail split out of `Docs/NewChat_Start.md` (2026-10-11), moved **verbatim**: the derived structure of the
theory (weak-isospin doublets, the fermion derivation chain QG138-QG180, the architecture summary), the V2.1
wave-geometry research program, the PRED001 prediction registry, and the V2.0 claim-classification audit pass.

Current state: `Docs/NewChat_Start.md`. Audit catalogue: `Docs/AuditIndex.md`.

---
## Major Milestones

### ResearchY: Wave Geometry Program (V2.1 research track)

Summary:

ResearchY (Docs/ResearchY/) is the V2.1 research program investigating whether
Difference, Actualization, Spectrum, resonance, circular closure, π, 2π, center, source,
and wave propagation are connected within canonical AT. Four topic groups: A Wave
Foundations (A_001 Wave Origin Audit, A_002 Difference Disturbance, A_003 Actualization
Propagation), B Circular Geometry (B_001 Circular Closure, B_002 Origin of π, B_003 Origin
of 2π), C Source Geometry (C_001 Center Audit, C_002 Radial Propagation), D Resonance
Structure (D_001 D96 Resonance Audit, D_002 Standing Wave Model). Permanent IDs, never
reused; groups are topics, not chronological order. Program indexes:
ResearchY_Index.md (permanent-ID registry), ResearchY_Status.md (investigation + test
status), Tests/ResearchY_Tests_Index.md + Tests/Results/ (test registry/result store).
Every quantifiable hypothesis gets an xUnit test: ResearchY-A_001 ↔ Y_A_001_Tests.cs
(naming convention Y_G_NNN_Tests.cs under AT.Tests/ResearchY/<Group>, mirroring the
Docs/ResearchY folder structure).

Classification-guard rule (since D_040): classifications (BOUNDARY / EMERGENT / DERIVED)
must NOT drift between audits. The canonical final classification registry is encoded
in Y_D_040_Tests.ClassificationRegistry (and summarized in ResearchY-D_040.md). Two
rules: (1) a reclassification requires a superseding audit that updates BOTH the new
doc AND the D_040 registry test — otherwise the test fails; (2) two-level rule for
derived values: a quantity may be DERIVED as a VALUE (given N) while its
WINDOW/REQUIREMENT is BOUNDARY (the input). Canonical example (D_028/D_040): the 3-family
window (span ∈ [4,8)) is BOUNDARY; the family-count VALUE 3 at N=96 is DERIVED; N=96 is
DERIVED. Older audits that tagged these differently (e.g. D_028 "window EMERGENT / N=96
BOUNDARY") carry refinement notes pointing to the superseding audit.

ResearchY-A_001 (Wave Origin Audit, COMPLETE) verdicts: Q1 Difference-as-disturbance
COMPATIBLE (as a configuration, not the definition); Q2 Actualization-as-propagation
PARTIAL (generation-space branching μ^k only; spatial propagation stays null-geodesic
n=1, no refraction); Q3 spectrum-as-resonance COMPATIBLE AS EXPLANATION
(graph-Laplacian eigenmodes ARE the standing normal modes of the closed ring C96);
Q4 center/source PARTIAL (no spatial center; branching root + ladder radii 6.0–17.333);
Q5 D96-as-standing-wave YES (static normal modes; Z2 doublets = ±k ring degeneracy);
Q6 circular propagation PARTIAL (ring topology + phase lattice θ_k=2πk/96 emerge;
traveling-wave dynamics not derived); Q7 π/2π from closure OBSERVATION (spectral layer
already uses 2π as ring-closure periodicity constant; does NOT resolve Bekenstein
boundary, QG196 unchanged); Q8 radius/circumference/wavelength/frequency/resonance
ALREADY IMPLICIT (ladder radii, N=96 ring, mode index, ω=√λ, degeneracy/octave bands).

Rules: no canonical AT V2.0 modifications; no new primitives; no ad-hoc constants;
research and audit only; assumptions recorded explicitly; consistency checked against
Difference → Actualization → Spectrum → Physics. Source note:
Docs/ResearchY/ResearchY-A_WaveOriginAudit.md (background working audit).

Test status: Y_A_001 suite (5 tests) PASSED 2026-08-28 — canonical D96 constants
reproduced exactly (Σ√m 64.0825, Σm² 229, span 6.4025, bands [4,4,87], occMom 1900.25,
ω_min 0.6216, Z2 pairs 47); wave observables read from same spectrum (circumference 96,
radius N/2π=15.279 inside ladder radii 6.0–17.333, 2π as ring-closure periodicity
constant). Result summary in Tests/Results/Y_A_001_Result.md.

ResearchY-A_002 (Difference Disturbance Audit, COMPLETE, tests 7/7 PASSED): which
interpretation of Difference is a localized disturbance? Verdict: C5 mode excitation best
(|ψ_k|² = ρ_k exact, QG216); C1 delta-on-ρ = point-source (Q-event, uniform modal weight
1/96, Parseval Σ|ψ|²=1); C2 phase displacement = count-preserving circulation carrier
(θ_k=2πk/96, |e^{iθ}|²=1); C3 graph defect = configuration only (attractor is fixed
point); C4 occupancy disturbance = re-reading only. Propagation: generation-space
branching ρ_k=μ^k/S (canonical, MONO_PHASE002) — counting and propagation are the same
branching process in two vocabularies (RQ6: NO, not "better as propagation"); spatial
transport excluded (n=1). RQ7: zero mode λ₀=0, ω₀=0, constant = undisturbed background.
No canonical value changed.

ResearchY-A_003 (Actualization Propagation Audit, rev. 2, COMPLETE, tests 7/7 PASSED):
revised after A_004 (accepted: branching + spectral projection survives falsification;
single models fail). New scope: what propagates (count share ρ); carrier (Galton–Watson
tree for count + ring C96 for modes); locality split (local generation / global readout,
|φ_k(n)|²=1/96 on every site); μ^k = propagation depth (MONO_PHASE002); RQ5 branching
NOT rewritable as wave transport (first-order scalar; phase is a separate DOF per Ch9);
RQ6 Z2 pairing NOT explained by propagation (circulant-graph property, λ_k=λ_{N−k},
47 pairs); RQ7 octave occupancies [4,4,87] NOT explained by propagation (spectral ω
octaves); RQ8 resonance locking NOT explained by propagation (spectral gap λ₂=0.3864,
lock chain occMom/Σm=20.0026). Preferred: branching (local) + spectral projection
(global). No canonical value changed.

ResearchY-A_004 (Propagation Falsification Audit, COMPLETE, tests 7/7 PASSED):
falsification attempt on A_003 conclusion "Actualization = branching + spectral
projection". Alternatives tested under no-fitting/no-target/no-new-assumption
constraints: A branching (scalar shares, no octave structure — fails), B diffusion
(relaxes to uniform; dominant-mode count 3 not [4,4,87]; t free — fails), C wave
(reproduces [4,4,87] ONLY by reading ω_k octaves = spectral projection; adds
non-canonical dynamics — fails as independent generation), D hybrid (needs coupling
constant = fitting — fails). λ structure: NO model generates λ_k (all operators are
functions of the graph Laplacian — the medium). Moments: only the multiplicity
multiset [42×2,5,6] reproduces them (spectral). VERDICT: FALSIFICATION FAILED —
"branching + spectral projection" is UNIQUE within the accepted D96 structure, not
merely preferred. No canonical value changed.

ResearchY-A_005 (Spectral Projection Origin, COMPLETE, tests 7/7 PASSED): why does
branching project onto spectral modes? Is spectral projection primitive or derived?
Tested A fundamental (FAILS — would be a 3rd primitive, contradicts minimal foundation
{Difference,η}), B closure (PARTIAL — fixes N=96, not the graph structure), C resonance
(CIRCULAR — resonance IS the projection readout, Conservation+Boundary), D attractor
(YES — minimal origin). Minimal origin chain: Difference → Actualization → attractor
(closure, N=96) → graph C96 → Laplacian L → unique eigenbasis (Fourier modes, exact to
1e-14) → spectral projection (readout of count in the modal basis). Projection is DERIVED
not primitive; it is forced because the eigenbasis is the unique diagonalizing basis of
the medium, and the medium is the converged output of Actualization. K=5 vs K=6 give
different spectra → projection is attractor-determined. Open: scalar-to-modal bridge
(A_003 OP2). No canonical value changed.

ResearchY-B_001 (Circular Closure Audit, COMPLETE, tests 7/7 PASSED): is circular
closure necessary (Difference → Actualization → Attractor → Graph → Laplacian →
Eigenbasis), and do π/2π emerge? Verdicts: RQ1 propagation must close YES (bounded
self-reinforcing dynamics saturates at fixed point, QG282); RQ2 resonance requires
closure YES (Boundary = closure); RQ3 eigenmodes require closure YES (circulant →
Fourier basis, Z2 pairs 47, octave bands [4,4,87]); RQ4 circular geometry unavoidable
YES within accepted class (QG159/160); RQ5 2π = minimal phase closure YES (θ_N=2π≡0,
Ch9); RQ6 π from closure role YES (C/D=π identity) but value NO (transcendental,
boundary QG291, Bekenstein 2π still imported QG196); RQ7 closure encoded by D96 YES
(ring, λ_{k+N}=λ_k); RQ8 zero mode = closure reference YES (λ₀=0 uniform). Success
criterion: circular closure and 2π EMERGE; π emerges in role only. No canonical value
changed.

ResearchY-B_002 (Origin of π Value Audit, COMPLETE, tests 7/7 PASSED): can the
numerical value π=3.14159... emerge from the canonical framework, or only its role?
Verdict: BOUNDARY. Decisive argument: the graph Laplacian L=D−A is an INTEGER matrix ⇒
its eigenvalues (D96 spectrum) are ALGEBRAIC integers ⇒ every finite combination of
spectral constants is algebraic; π is TRANSCENDENTAL (Lindemann) ⇒ NO finite canonical
construction outputs π's value. Candidate paths fail: closure (N=96 integer), phase
(π is a parameter of algebraic roots of unity e^{2πik/96}, role only), graph/eigenmodes
(algebraic), circumference/radius (C/D=π is definitional — r=N/2π is a unit choice,
measurement not emergence). D96 approximants (span/2≈3.20, √10≈3.16, Σm/√Σm²≈6.28≈2π)
are near-misses — selecting any = fit (forbidden). N/(2π)=15.279 in A_001 is a
coincidence of range, not emergence. RQ9: closure needs only 2π (phase-cycle role), not
π's value. QG291/QG196 confirmed and STRENGTHENED (algebraicity argument). π's role
emerges (C/D=π, B_001); its value remains an irreducible boundary. No canonical value
changed.

ResearchY-C_001 (Center Audit, COMPLETE, tests 6/6 PASSED): is a unique center/source
present in Difference→Actualization? Verdict: center is ABSENT in space — the attractor
ring C96 is centerless (12-regular, translation-invariant, zero-mode eigenvector
constant, 47 Z2 pairs, no preferred site; circulant symmetry eliminates any center);
EMERGENT as the branching root — the Galton–Watson generation-0 origin (ρ₀=1/S) is the
only source-like object, in generation space not spatial; DERIVED as the zero mode — the
uniform reference state (ω₀=0, not a source). Propagation is NOT radial (branching
tree-local + spectral global readout); radius r=N/2π is a size measure with no center
point; closure = N=96 integer requires no source. No canonical value changed.

ResearchY-C_002 (Radial Propagation Audit, COMPLETE, tests 6/6 PASSED): can propagation
in C96 be genuinely radial? Verdict: FAIL — NOT canonically radial. Radial propagation
requires a derived origin + shell ordering; C96 is D96 vertex-transitive (identical
shell profiles from every node; diameter 8 = N/(2K) = 96/12; shells {1,12,12,12,12,12,
12,12,11}; reflection symmetry d(o,k)=d(o,N−k)). Shortest-path propagation = radial
shells (identical BFS layers). Canonical spreading = tree-local (branching ρ_k=μ^k/S,
generation depth) + global (spectral readout, |φ_k(n)|²=1/96) — a NON-radial hybrid.
Radial structure is a gauge/coordinate choice, not invariant content; formal diffusion
shell model requires a chosen origin (non-canonical). No canonical value changed.

ResearchY-D_001 (Standing Wave Audit, COMPLETE, tests 7/7 PASSED): can standing waves
exist on C96 without center-based geometry? Verdict: YES. Standing wave = time-harmonic
eigenfunction ψ(n,t)=φ(n)cos(ωt+δ), Lφ=λφ, ω=√λ; modes are the Fourier harmonics
cos(2πkn/N)/sin(2πkn/N). Geometric standing wave (spatial pattern, node positions depend
only on k — no origin) vs spectral standing wave (frequency ω_k=√λ_k) are centerless
faces of one object. Zero mode λ₀=ω₀=0 = uniform rest state (degenerate standing wave,
the reference). Resonant pairs: 47 Z2 pairs (42 doublets + one 5-group + one 6-group),
fundamental doublet ω₁=0.6216. Classification: HYBRID (spatial pattern + spectral
frequency), CENTER-FREE (consistent with C_001/C_002). No canonical value changed.

ResearchY-B_003 (Origin of 2π Audit, COMPLETE, tests 6/6 PASSED): can 2π emerge as a
closure invariant without deriving π itself? Verdict: YES in role, NO in value. The
full-cycle period is forced by the finite closed ring — R^N=identity (N=96 exact),
z_k^N=1 (roots of unity, algebraic closure, φ_k(n+N)=φ_k(n)), θ_{k+N}≡θ_k (phase
periodicity) — all algebraic statements requiring no value of π. Classification: cycle
closure DERIVED (finite-group); phase periodicity EMERGENT (full cycle exists); eigenmode
rotations DERIVED (z^N=1); N=96 symmetry DERIVED (D96, 47 Z2 pairs); 2π ROLE EMERGENT
(closure invariant); 2π VALUE BOUNDARY (6.283185… transcendental, B_002; radian
normalization is a unit convention). Consistent with B_001 (role) + B_002 (value). No
canonical value changed.

ResearchY-D_002 (Standing Wave Model, COMPLETE, tests 7/7 PASSED): canonical standing
wave model of C96. Mode decomposition: Ψ(n,t)=Σ_k[a_k cos(2πkn/96)+b_k sin(2πkn/96)]
cos(ω_k t), 95 positive Fourier modes + 1 zero mode (complete, orthogonal). Resonant
pairs: 47 Z2 pairs (λ_k=λ_{96−k}, 94 paired real modes) + self-conjugate k=48 (λ=12)
= 95 positive modes; multiset 42×2+5+6; fundamental doublet ω₁=0.6216. Zero mode:
λ₀=ω₀=0, constant = uniform rest state (reference, C_001). Spatial vs spectral:
hybrid — spatial harmonics (geometric, center-free) × spectral eigenvalues (algebraic).
Closure consistency: R^N=identity, θ_{k+N}≡θ_k, z_k^N=1, algebraic spectrum (B_002),
π only in role (B_003). Classification: HYBRID (center-free). No canonical value changed.

ResearchY-D_003 (Resonance Observables Audit, COMPLETE, tests 6/6 PASSED): can resonance
alone generate physical observables? Verdict: resonance generates the SPECTRAL observables
— DERIVED (theorem-class): mode occupation [4,4,87], occMom=1900.25, spectral moments
(Σm=95, Σ√m=64.08, Σm²=229), span=6.40, 47 Z2 pair structure, zero-mode role, spectral
invariants (algebraic spectrum). It does NOT generate the PHYSICAL observables: the
sector mapping is EMERGENT (correspondence — which moment reads which sector), and the
dimensional masses/couplings are BOUNDARY (calibration anchors v/m_e; fit 1/α_em=137).
Resonance is the spectral source of the numbers, not the complete generator. Consistent
with the claim classification registry. No canonical value changed.

ResearchY-D_004 (Sector Mapping Origin Audit, COMPLETE, tests 6/6 PASSED): why do
spectral quantities map to physical sectors? Verdict: THREE-LAYER origin. (1) DERIVED
structure — occupancies [4,4,87], moment ladder (Σm=95, Σ√m=64.08, Σm²=229), locking gap
λ₂=0.3864, 47 Z2 pairs are exact spectral (theorem-class). (2) EMERGENT sector assignment
— which moment/gap/pair reads which sector is a supported correspondence (claim registry:
sector mappings = correspondence), not a unique derivation. (3) BOUNDARY dimensional
values — calibration anchors v/m_e, fit 1/α_em=137. The families are the DERIVED
exception: family count = floor(log₂ span)+1 = 3, the octave bands ARE the families
(QG210, identity not assignment). Correspondence is "supported, not unique". No canonical
value changed.

ResearchY-D_005 (Moment Ordering Audit, COMPLETE, tests 6/6 PASSED): can moment ordering
uniquely determine sector assignment? Verdict: NO. The moment ladder is DERIVED (strictly
ordered 64.08 < 95 < 229 < 1900.25), but the sector assignment is EMERGENT — a
correspondence: 4! = 24 permutations of the four moments onto the four sector roles are
possible, the spectrum excludes none, and the canonical one is selected by matching
observation (the defining property of a correspondence, not a derivation). The electron is
BOUNDARY: m_e = 0.511 MeV is a calibration anchor, not moment-selected. Family ordering:
octave band frequency order DERIVED, family labels (e/μ/τ ↔ bands) EMERGENT. Negative
uniqueness proof: the spectrum is invariant under sector-name relabeling, so it cannot
distinguish the permutations. Consistent with D_004 ("supported, not unique"). No
canonical value changed.

ResearchY-D_006 (Assignment Constraints Audit, COMPLETE, tests 6/6 PASSED): can
assignment constraints reduce the 24 sector permutations? Verdict: 24 → 6 → 2 → 1.
(1) Symmetry: occMom is DEFINED from the octave occupancies (Ch6) → octave pairing fixed
→ 24→6 (DERIVED). (2) Z2: Σm² = 229 is doublet-dominated (42×2² = 168, 73%) and the
canonical doublet-occupancy access (QG157) → doublet pairing fixed → 6→2 (DERIVED
dominance + EMERGENT assignment). (3) Calibration: Σm = 95 is the total mode count (full
access) → 2→1 (DERIVED); final match to observation is BOUNDARY. Ordering (no canonical
sector ordering) and family (octave bands = families, reinforces occMom) add no
independent reduction. Unique survivor: half→neutral, first→full, second→doublet,
octave→octave. Unique under constraints; surviving assignment remains EMERGENT
(correspondence). No canonical value changed.

ResearchY-D_007 (Planck Scale Audit, COMPLETE, tests 7/7 PASSED): can the Planck scale
be derived without calibration anchors? Verdict: dimensionless structure DERIVED,
absolute scale BOUNDARY. (A) The dimensionless Planck ratio A³ = (Σm·#g·occ₂)³ =
(95·44·87)³ = 4.8094e16 is a pure number from D96 spectral content (moments, group
count, dense band) — DERIVED. (B) The absolute Planck scale M_Pl = v·A³ = 254.37·4.8094e16
= 1.2234e19 GeV is NOT derived — requires the calibration anchor v (weak scale, GeV
unit). (C) Requires anchor: YES (v). (D) SI G = ħc/M_Pl² imports c, ħ, GeV↔kg — BOUNDARY.
Planck scale is calibrated, not derived (consistent with claim registry: gravity =
calibration). No canonical value changed.

ResearchY-D_008 (Reference Unit Audit, COMPLETE, tests 6/6 PASSED): what object in D96
plays the role of light or an atomic clock? Verdict: the first natural reference unit is
the dimensionless spectral frequency ω₁=0.6216 (fundamental doublet) — DERIVED as a
relative (ordering/ratio) reference, calibration-free. Key relation ω₁²=λ₂=0.3864 links
the doublet and the spectral gap. All six candidates (actualization tick, closure cycle
N=96, zero mode, fundamental doublet, spectral gap, 47 resonant pairs) are dimensionless.
Natural clock: dimensionless frequency EMERGENT, physical Hz BOUNDARY; ruler: dimensionless
ratios (span=6.40) EMERGENT, physical m BOUNDARY; energy unit BOUNDARY (requires v, c, ħ,
per D_007). Reference without external calibration: YES as dimensionless (DERIVED), NO as
physical (BOUNDARY). D96 provides dimensionless analogues of the atomic clock and the
light-based meter, not physical references. No canonical value changed.

ResearchY-D_009 (Minimum Excitation Audit, COMPLETE, tests 7/7 PASSED): is ω₁ the
minimum non-zero actualization? Verdict: YES. ω₁ = 0.6216 is the smallest positive
frequency — the first excitation and the first difference above the zero mode ω₀ = 0 —
DERIVED. The spectral gap λ₂ = ω₁² = 0.3864 is the smallest positive eigenvalue; verified
zero positive states in the interval (0, ω₁); multiplicity 2 (fundamental doublet, k=1
and k=N−1). Classification: A) first frequency DERIVED; B) first difference DERIVED;
C) first actualization EMERGENT (the minimum-excitation-as-count-event reading is
interpretive); D) natural clock only NO — it is more than a clock, and as a physical
clock it is BOUNDARY (D_008: dimensionless only). Property invariant under ring
automorphisms. No canonical value changed.

ResearchY-D_010 (Unit Anchoring Audit, COMPLETE, tests 7/7 PASSED): can a physical unit
be anchored to ω₁? Verdict: NO — ω₁ = 0.6216 is DIMENSIONLESS (a pure frequency ratio);
a physical unit (s/m/J) is dimensionful and requires at least one dimensionful import.
A) dimensionless reference DERIVED; B) physical clock BOUNDARY (needs a physical time
standard); C) physical ruler BOUNDARY (needs c); D) physical energy unit BOUNDARY (needs
ħ or v). Time/frequency/energy/length scales each require an import. Dependency: ω₁ only
→ dimensionless (DERIVED); ω₁+c / +ħ / +v → physical relations each importing a constant
(BOUNDARY). MINIMAL REQUIRED IMPORT: the calibration anchor v (weak scale, GeV) — the
same anchor as the Planck scale (D_007); c and ħ are additional SI imports. Atomic clock,
speed-of-light meter, and Planck units are all external physical constants. No canonical
value changed.

ResearchY-D_011 (Universal Reference Audit, COMPLETE, tests 7/7 PASSED): can ω₁ be the
universal reference for all physical units? Verdict: ω₁ is the universal DIMENSIONLESS
reference — all spectral ratios are expressed relative to it (ω_max/ω₁ = span = 6.40,
λ_max/λ₂ = 40.99, span/ω₁ = 10.30 — DERIVED, exact, invariant). It is NOT the universal
physical-unit reference: every physical dimension (time/frequency/energy/mass/length)
requires a dimensionful anchor (D_010, BOUNDARY). ω₁ does not act as an atomic transition
frequency, speed-of-light, or Planck reference (it is a dimensionless spectral frequency).
Anchor count: A) ω₁ only → DERIVED (dimensionless); B) ω₁ + one anchor (v) → BOUNDARY
(energy/mass); C) ω₁ + multiple anchors (v, c, ħ) → BOUNDARY (SI). Minimal anchor count:
1 (v). Unit-attachment model = the pair (dimensionless reference + anchor). No canonical
value changed.

ResearchY-D_012 (Minimal Anchor Audit, COMPLETE, tests 7/7 PASSED): what is the minimal
physical anchor to turn D96 structure into physical dimensions? Verdict: the weak scale
v is the ONLY dimensionful candidate (ω₁, λ₂, zero mode, N=96, actualization tick are all
dimensionless — D_010). ONE anchor is NOT sufficient (REFUTED): v fixes the energy scale
(M_Pl = v·A³ = 1.2234e19 GeV, M_W/M_Z/M_H) but the absolute fermion masses require the
second anchor m_e (m_u = m_e·Σ√m/√Σm², QG173). Minimal anchor count for ALL derived
dimensionful observables = 2 (v, m_e); c and ħ are SI unit-convention imports, not
physics anchors. Dimensionless observables (couplings, mixings, Ω_Λ/Ω_m, ratios) need 0
anchors — DERIVED. Trace: D96 → ratios → ω₁ → anchor → dimensions → observables (splits
at "anchor"). No canonical value changed.

ResearchY-D_013 (Anchor Reduction Audit, COMPLETE, tests 8/8 PASSED): are v and m_e
independent anchors or projections of a deeper anchor? Verdict: INDEPENDENT, IRREDUCIBLE.
H1 (v fundamental, m_e = v·f): REFUTED — f = m_e/v ≈ 2e-6 is not a canonical spectral
number; no canonical construction of m_e from v. H2 (m_e fundamental, v = m_e·g):
REFUTED — g = v/m_e ≈ 5e5 not canonical; v's GeV unit anchor is independent of m_e
(v's dimensionless structure (Σm+#d)·ln(span) = 254.37 needs the GeV anchor, not m_e).
H3 (common anchor A0): REFUTED — no canonical A0 exists (would be a new primitive). No
common invariant (spectral source, moment, resonance, closure scale) links the two
anchors. Anchor count: 2 → IRREDUCIBLE — any reduction introduces a new primitive, a fit,
or breaks D_012. No canonical value changed.

ResearchY-D_014 (Two-Anchor Structure Audit, COMPLETE, tests 8/8 PASSED): why does
physics require exactly two irreducible anchors {v, m_e}? Verdict: the two anchors admit
a boson/fermion (gauge/matter) interpretation — v is the bosonic anchor (M_W/M_Z/M_H/
M_Pl), m_e the fermionic anchor (quark/lepton masses, m_u = m_e·Σ√m/√Σm²). The
boson/fermion and gauge/matter splits are consistent (supported); the even/odd, Z2
doublet (47 pairs), and octave family (3) splits have NO canonical anchor mapping. The
two-anchor structure is NOT a consequence of D96: v's dimensionless form
((Σm+#d)·ln(span) = 254.37) IS D96-derived, but the anchor COUNT (2) is the calibration
split (bosonic scale + fermionic scale), and m_e has no D96 construction. Two anchors ↔
two sectors = EMERGENT interpretation, not DERIVED. No canonical value changed.

ResearchY-D_015 (N=96 Uniqueness Audit, COMPLETE, tests 6/6 PASSED): why N=96? Verdict:
N=96 is unique in the tested class {64, 96, 128, 192, 245} by the COMBINATION (E) of the
period-3 seed symmetry (6|N) and the three-family octave window (span in [4,8)):
{N : 6|N AND span<8} = {96}. N=192 passes the seed but fails the window (4 families);
N=64 passes the window but fails the seed; N=128/245 fail both. Scale properties are NOT
unique — λ₂ (0.8596→0.0598) and ω₁ (0.9272→0.2445) decrease with N, Z2 pairs scale
(N/2−1). Structural properties ARE unique: N=96 is the only size with 3 families AND the
canonical occupancy [4,4,87] (occMom = 1900.25). N≠96 loses the 3-family structure or
the canonical occupancy. Matches canonical D96 selection (QG159/QG160). No canonical
value changed.

ResearchY-D_016 (Family-Count Origin Audit, COMPLETE, tests 7/7 PASSED): audit of
families = floor(log2 span)+1. Verdict: family count = 3 ⟺ span ∈ [4,8) is DERIVED (a
mathematical identity); the 3-family window, 6|N, and N=96 are SELECTION RULES.
Removing the N=96 assumption and scanning C_N(±1..±6) for N ∈ [13,300] finds 61 rings
with 3 families (N ∈ [60,120]) — N=96 is NOT unique for the family count. 6|N is not
necessary (N=61..65 give 3 families without it); N=96 is one of 11 rings with both 6|N
and 3 families, selected by the additional D96 criteria (period-3 seed, Z2 half-shift,
unique octave rung — D_015), not by the family count alone. Counterexamples: N=64/90/120
→ 3 families, N=128 → 4, N=29 → 2. Only the span-window equivalence is derived; the
rest are selections. No canonical value changed.

ResearchY-D_017 (Scale Stability Audit, COMPLETE, tests 10/10 PASSED): which N gives
the most stable physical scale? Verdict: λ₂ and ω₁ do NOT select N=96 — they are
strictly monotone in N over [32,300] (λ₂: 0.8596@64 → 0.3864@96 → 0.0972@192), and
scale stability IMPROVES with N (relative λ₂ change under ΔN=1: 0.030@64 → 0.020@96 →
0.010@192; a trivial λ₂~1/N² trend). The [4,4,87] occupancy is N=96-specific
(structural, N=95→[4,4,86], N=97→[4,4,88]) but not a stability property. occMom varies
smoothly (1648@90 → 1900@96 → 2170@102). Spectral density around ω₁: band1 = 4 for all
N in the window. Selection: A family-selected PARTIAL (window [60,120], D_016); B
scale-selected NO; C resonance-selected NO (Z2 doublet at all N); D closure-selected
YES (Ch5 attractor fixed point). Stability scores (inverse rel change): N=96 50, N=128
65, N=192 97 — monotone. λ₂/ω₁ do NOT select N=96 more fundamentally than the family
count; the closure does. No canonical value changed.

ResearchY-D_018 (Occupancy Selection Audit, COMPLETE, tests 11/11 PASSED): why does N=96
generate occupancy [4,4,87]? Is [4,4,87] the true selection mechanism? Verdict: REFUTED
as selector. [4,4,87] is unique to N=96 only TRIVIALLY — the map N→occupancy is a
bijection over [32,300] (269 N → 269 distinct patterns; adjacent N always differ), so
every occupancy is one-of-a-kind and the rarity carries no selection information. In the
three-family window [71,120], occ(N) = [4,4,N−9] exactly (all 50 rings) — "87" = 96−9 is
a linear consequence of N. The [4,4] prefix is generic (band₁=4 for 266/269; [4,4,...]
for 230/269; all 50 window rings). occMom = (x²+32)/4 is monotone increasing (969@71 →
1900.25@96 → 3088.25@120; scan max N=300) — no extremum at 96. Occupancy is the LEAST
stable structure under ΔN (changes at every step; N=95→[4,4,86], N=97→[4,4,88]; no
plateau around 96). Conclusion: D96 is NOT occupancy-selected (a bijection carries zero
selection power); it remains closure-selected (D_017, Ch5). [4,4,87] is a DERIVED
projection of the closure selection. No canonical value changed.

ResearchY-D_019 (Closure-Only Audit, COMPLETE, tests 8/8 PASSED): if all D96-specific
selection rules are removed and only Closure remains, does Closure still produce N=96?
Verdict: NO — Closure alone does NOT determine N. The canonical closure dynamics
(QG115/116, StructureFromContent.AdaptiveNetwork / ActualizationStructures.
ReinforcingNetwork, K=6, damping=0.2, feedback=0.7) takes N as an INPUT (the activity
array length) and converges the link structure for that fixed size: 269/269 N in
[32,300] converge under the persistent/spread/uniform patterns, and the fixed point is
always the degree-12 K=6 ring (links = 6N) — a geometry class, not a size. Under the
concentrated pattern only 56/269 converge, and N=96 itself FAILS closure (growth
0.1198 > 0.05) — a direct counterexample to any content-independent closure → N=96. The
canonical Ch3/QG282 claim "N=96 is the closure fixed point" is not supported by the
implemented closure dynamics (open consistency question, D_019 OP1). N=96 is a SELECTED
closure solution (D_015: 6|N + span window), not a closure theorem. Classification:
D) Closure does not determine N. No canonical value changed.

ResearchY-D_020 (Selection Precondition Audit, COMPLETE, tests 6/6 PASSED): what
selected N=96 BEFORE Closure — the deepest precondition of D96? Verdict: the deepest
precondition is the OBSERVABLE-SECTOR CONSTRUCTION — a Z2-paired (complex) sector
(weak-isospin, 0 unpaired modes) + exactly 3 octave families — which is an INPUT/BOUNDARY
(assumed from the observable sector, not derivable from Difference alone). From these two
INPUTs everything is DERIVED: p=3 (the unique period whose natural octave-rung size
3·2^k=96 has complete Z2; p=2,4→64 and p=5→80 have 1 unpaired), 6|N (period-3 seed
half-shift), the octave-rung chain n=3·2^k={48,96,192}, and N=96 (the unique rung in the
3-family window [60,120)). The octave-rung construction is the exact discriminator that
selects 96 among the 11 rings of D_016 (only 96=3·2⁵ is a rung). The degree-12 ring
(K=6) is cosmetic — the radius is uniform across all rungs. REFINED by D_021/D_035/D_036:
the Z2 pairing STRUCTURE (quadrature {cos,sin}, λ_k=λ_{N−k}) is DERIVED (D_021) and
complete pairing (0 unpaired) is DERIVED from complex observability (D_035); the
Z2-paired (complex) sector requirement is the BOUNDARY input. Minimal selection set =
{Z2-paired sector requirement, 3 families}. Classification: observable-sector
construction BOUNDARY; p=3, 6|N, octave rung, N=96 all DERIVED. No canonical value
changed.

ResearchY-D_021 (Oscillation Symmetry Audit, COMPLETE, tests 7/7 PASSED): is complete
Z2 pairing a consequence of oscillation symmetry rather than weak-isospin? Verdict: the
Z2 PAIRING is DERIVED — the pair {cos, sin} at each ω_k is the two-quadrature structure
of ONE real oscillation (both eigenfunctions of L at the same λ_k), forced by the
spectral symmetry λ_k = λ_{N−k} of the ring's reflection. +A↔−A and cos(ωt)↔−cos(ωt)
are per-mode phase gauges (no pairing); k↔N−k is the pairing generator (cos even → same,
sin odd → sign-flip). The pairing is NOT weak-isospin-only; the weak-isospin doublet
reading is EMERGENT (D_014). Standing-wave completeness SURVIVES removal of Z2 pairing —
completeness is a basis property (complete Fourier basis at N=64/96/128), pairing is a
degeneracy property. The COMPLETENESS of pairing (0 unpaired) is BOUNDARY — an
N-arithmetic selection (λ=12 self-conjugate degeneracy: 5-fold at N=96/192, 1-fold at
N=64/128), the D_020 input. No canonical value changed.

ResearchY-D_022 (Weak-Isospin Entry Audit, COMPLETE, tests 7/7 PASSED): is weak-isospin
an emergent reading of oscillation-derived Z2 symmetry? Verdict: NO — weak-isospin is NOT
the oscillation-derived Z2. Three distinct Z2 objects: (1) oscillation Z2 (+A↔−A,
cos↔−cos) — DERIVED, universal temporal phase gauge of one mode; (2) spectral Z2
(λ_k = λ_{N−k}) — DERIVED, ring reflection; (3) weak-isospin Z2 (SU(2) doublet, T₃=±1/2)
— BOUNDARY, independent gauge input. Tests: oscillation-derived Z2 exists WITHOUT
weak-isospin (spectral Z2 + quadrature pairs at N=32..192, no gauge sector); weak-isospin
is writable WITHOUT spectral Z2 (SU(2) is a gauge group, a doublet rep needs no
degeneracy). The {cos, sin} eigenspace transforms as SO(2) (det-1 2×2 rotations,
Abelian), NOT SU(2) (3 Pauli generators, non-Abelian) — the spectral doublet is a parity
doublet {even, odd}, not an SU(2) rep. Verdict: C) weak-isospin = independent input
(SU(2) gauge structure); the doublet SHAPE read as weak-isospin is B) EMERGENT.
Classification: oscillation Z2 DERIVED, spectral Z2 DERIVED, weak-isospin SU(2) BOUNDARY,
doublet reading EMERGENT. No canonical value changed.

ResearchY-D_023 (SU(2) Entry Audit, COMPLETE, tests 6/6 PASSED): where does SU(2) enter?
Can SU(2) emerge from deeper spectral structure? Verdict: NO — SU(2) does NOT emerge
from the spectral structure. The oscillation+reflection symmetries provide exactly ONE
continuous generator (J, the SO(2) rotation of the {cos,sin} eigenspace) plus one
discrete generator (P, O(2)); SU(2) requires THREE continuous non-Abelian generators
(Pauli σₓ, σ_y, σ_z). The real skew-symmetric 2×2 matrices are 1-dimensional (only J);
the missing generators iσₓ and iσ_z are complex and absent from the real spectral
structure. The D_n 2D irreps (QG155) generate the Z2 doublets — correct — but are
O(2)-type real reps of a discrete group, NOT SU(2). The minimal step doublet→SU(2)
requires complexification (a NEW input, not from the spectrum). Removing SU(2) leaves all
spectral content intact (doublets, families, moments, standing-wave structure); only the
gauge layer (weak-isospin doublets, W/Z) is lost. Verdict: A) SU(2) = independent input
(BOUNDARY); the doublet is the EMERGENT attachment surface; C) partially derived NO.
Classification: spectral doublet O(2)-type DERIVED, SU(2) gauge algebra BOUNDARY, reading
EMERGENT. No canonical value changed.

ResearchY-D_024 (Doublet Compatibility Audit, COMPLETE, tests 7/7 PASSED): why does SU(2)
attach to spectral doublets? Is the doublet shape uniquely compatible with weak-isospin?
Verdict: the doublet shape is NECESSARY but NOT SUFFICIENT for weak-isospin. SU(2)
irreps come in every dimension 2j+1 (j=0→1D singlet, j=½→2D doublet, j=1→3D triplet,
j=3/2→4D, j=2→5D, j=5/2→6D); the spectral doublet (2D) is compatible with the
fundamental j=½ rep (T₃=±½, weak-isospin fermions), but the same 2D space hosts
SO(2)/O(2)/SU(2) (D_022/D_023), and the D96 5-fold/6-fold groups are SU(2) carrier
spaces too (j=2, j=5/2). Compatibility table: only the doublet is the weak-isospin
fundamental; singlet/triplet/quadruplet/quintuplet/sextuplet are NOT. The weak-isospin
attachment to doublets is the EMERGENT choice of the fundamental rep, not a unique
consequence of the doublet shape. Classification: spectral doublet DERIVED, SU(2) rep
dims BOUNDARY, j=½ fundamental choice BOUNDARY, attachment EMERGENT. No canonical value
changed.

ResearchY-D_025 (Three-Generator Audit, COMPLETE, tests 7/7 PASSED): why three
generators? What is the minimal structure that upgrades a spectral doublet from SO(2) to
SU(2)? Verdict: SO(2)→SU(2) is NOT possible without new input. The real spectral algebra
{I, J, P, JP} is the full real 2×2 algebra (dim 4): it contains J = iσy (real skew),
σz = P (parity/reflection), and σx = JP (reflection∘rotation) — all DERIVED (oscillation
+ reflection). But SU(2) needs the SKEW-HERMITIAN generators iσx, iσz, which require the
imaginary unit i (complexification). The Fourier phase (e^{iθ}, z^N=1, B_003) provides i
implicitly — complexification is EMERGENT from the phase (a representation choice). BUT
complexification alone gives sl(2,C) (6 real dims), which has THREE real forms: su(2),
sl(2,R), su(1,1). The real spectral structure contains the sl(2,R) generators directly
(real traceless 2×2) and LEANS toward sl(2,R), NOT su(2) — the compact-form choice
(su(2) signature) is BOUNDARY, not derived. Removing any ingredient (complexification,
parity, phase, compact-form choice) breaks SU(2). Classification: real algebra {I,J,P,JP}
DERIVED; complexification EMERGENT; compact-form choice BOUNDARY; SU(2) three generators
BOUNDARY. The upgrade needs complexification (EMERGENT) + compact-form choice (BOUNDARY).
No canonical value changed.

ResearchY-D_026 (Compact-Form Audit, COMPLETE, tests 6/6 PASSED): why is the compact form
su(2) selected? Is it physically necessary or an independent gauge input? Verdict: su(2)
is selected by the physical requirement of finite-dimensional UNITARY
(probability-preserving) representations. Among the three real forms of sl(2,C), su(2)
is the UNIQUE compact one: bounded generators (exp(5·iσy) norm 19.6 vs exp(5·H) norm
148.4 for sl(2,R)), finite-dimensional unitary irreps (the 2j+1 multiplets), and
elements preserving the Born-rule norm. sl(2,R) and su(1,1) are non-compact: their
unitary irreps are infinite-dimensional and their exponentials grow without bound
(boost-like), so there is no finite unitary doublet. The spectral observables (doublets,
families, masses, mixings) survive ANY real-form choice — they are spectrum-derived, not
group-derived; only the WEAK sector (W/Z, isospin doublets) requires finite-dim unitary
reps, which su(2) uniquely provides. Physical selection: positivity, normalization, and
stability all select su(2); closure consistency alone does not exclude sl(2,R)/su(1,1).
Verdict: the compact-form choice is EMERGENT from observability (not derived from the
D96 spectrum, not a free gauge input). Classification: spectral observables DERIVED,
su(2) compactness/unitarity EMERGENT, weak sector requires su(2) EMERGENT. No canonical
value changed.

ResearchY-D_027 (Selector-Origin Audit, COMPLETE, tests 6/6 PASSED): are positivity,
normalization, and stability derived from Difference → Actualization or the final
Boundary input? Verdict: A) all DERIVED from the primitive structure. Positivity is
intrinsic to the share construction (ρ_k = μ^k/S ≥ 0 — counts are non-negative); 
normalization is the Born rule = normalized share, derived from count conservation
(Ch9/QG216: Σρ_k = 1.0000000000 exactly), which is the definitional identity of
Difference (Ch3/QG268); stability IS the closure fixed point (Ch4/QG282: the boundary
is the stable fixed point). Count conservation implies normalization (without a
conserved count the share is not normalizable); Actualization implies positivity (the
share of a count is non-negative); Closure implies stability (without the fixed point
the spectrum would not close). The D_026 su(2) selector (positivity + normalization +
stability) is a consequence of the minimal hierarchy — the only boundary is the
primitive set {Difference, η} itself. Classification: positivity DERIVED, normalization
DERIVED, stability DERIVED, su(2) selector DERIVED, primitives BOUNDARY. No canonical
value changed.

ResearchY-D_028 (Span-Origin Audit, COMPLETE, tests 6/6 PASSED): why is span ≈ 6.4025?
Is span the true selection quantity behind D96? Verdict: span is a DERIVED monotone
function of N, NOT a selector. span = ω_max/ω_min; ω_max → √12 ≈ 3.46 (the antipodal
mode, even N) and ω_min ~ (2π√91)/N ≈ 59.9/N (the fundamental mode), so span ~
0.0578·N — monotonically increasing with no special point at 96; span(96) = 6.4025 is
just the N=96 point of this function. Removing any candidate (closure D_019, Z2
completeness, octave-rung, resonance density, information distribution) leaves span(96)
unchanged — the value is N-determined, not selector-determined. The family count =
floor(log₂ 6.4025)+1 = 3 is a DERIVED consequence of span (the D_016 identity).
REFINED by D_040 (two-level statement): the span/family-count VALUES are DERIVED (given
N), but the span ∈ [4,8) 3-family WINDOW is the observable-sector INPUT (BOUNDARY,
D_020) that selects N, and N=96 is DERIVED from the four boundary inputs (D_040).
Chain: Difference → Actualization → observable-sector construction {Z2-paired sector,
3-family window} (BOUNDARY, D_020) → N=96 (DERIVED, D_040) → Spectrum → span 6.4025
(DERIVED) → 3 families (DERIVED). Classification: span value DERIVED; span as selector
REFUTED; span ∈ [4,8) window BOUNDARY (observable-sector input, D_020); N=96 DERIVED.
No canonical value changed.

ResearchY-D_029 (Closure-Defect Audit, COMPLETE, tests 6/6 PASSED): what structure must
be closed so that no inconsistency remains? Verdict: closure removes structural defects
(unpaired modes, broken seed half-shift 6∤N, wrong family count, span ≥ 8) — producing
the ZERO-DEFECT set {60, 66, …, 120} (11 rings with 6|N + 3 families, 0 unpaired, span
< 8). Inconsistency counts: 64=2, 80=2, 128=4 (all four defects), 192=2, 245=3, 96=0.
BUT closure does NOT select N=96: N=60/90/120 are zero-defect too. The OCTAVE-RUNG
structure n = 3·2^k is the discriminator — N=96 = 3·2⁵ is the UNIQUE zero-defect octave
rung in [32,300] (48 has 2 families, 192 has 4). Prove/refute: closure = removal of a
specific structural defect is PARTIALLY supported — closure removes inconsistency
(EMERGENT zero-defect set), but the specific N=96 is a BOUNDARY selection (octave-rung,
D_020). Classification: defect removal EMERGENT; N=96 BOUNDARY. No canonical value
changed.

ResearchY-D_030 (Octave-Rung Audit, COMPLETE, tests 6/6 PASSED): why octave rungs? Is
N = p·2^k derived or a remaining boundary assumption? Verdict: the octave-rung structure
is DERIVED. The family count floor(log₂ span)+1 is itself an octave (factor-2) partition
(D_016), and the long-wavelength dispersion ω(k) ~ c·k makes mode doubling k→2k a
frequency octave (ω(2)/ω(1) = 1.97 at N=96). Hence q=2 is the natural scale step; n =
p·2^k is the discrete octave ladder. q=2 is the unique PURE scale-step base whose rung
chain hits a zero-defect ring (only 96; q=6 hits 108 but mixes the seed,
3·6^k = 3^(k+1)·2^k; q=3/4/5 have no zero-defect rung). Removing the octave rung leaves
11 zero-defect rings (96 not unique). The minimal principle: p (seed) × q^k (scale
step) with q=2 — the seed period p=3 is the BOUNDARY input (D_020). Classification:
octave structure DERIVED (dispersion + partition); q=2 base EMERGENT (linear
dispersion); seed period p=3 BOUNDARY (D_020). No canonical value changed.

ResearchY-D_031 (Seed-Origin Audit, COMPLETE, tests 6/6 PASSED): why does everything
begin with a period-3 seed? Is p=3 derived or the final boundary assumption? Verdict:
p=3 is DERIVED from pairing completeness + convergence. The complete-Z2-pairing
requirement (0 unpaired modes, weak-isospin doublets, D_020) applied to the natural
octave-rung size n = p·2^k selects p=3 uniquely: p=2/4→64 and p=5→80 have 1 unpaired
mode (incomplete), p=6→96 fails convergence (density 1/6), p=3→96 has 0 unpaired and
converges (canonical Period3SeedOrigin: INEVITABLE). p=3 is the MINIMAL period with
complete pairing. The pairing requirement is itself the D_020 observable-sector input —
so p=3 is DERIVED, while the pairing requirement is BOUNDARY. Chain: Difference →
Actualization → observable sector (BOUNDARY) → p=3 (DERIVED) → 6|N → octave ladder →
N=96. Classification: pairing completeness BOUNDARY; convergence DERIVED; p=3 DERIVED;
6|N DERIVED; N=96 DERIVED. No canonical value changed.

ResearchY-D_032 (Pairing-Requirement Audit, COMPLETE, tests 6/6 PASSED): why must the
observable sector be completely paired (0 unpaired modes)? Verdict: the pairing
STRUCTURE is DERIVED (D_021: cos/sin quadrature pairs from oscillation); the
COMPLETENESS (0 unpaired) is BOUNDARY — the observable-sector requirement that every
frequency carry full doublet/phase structure. The self-conjugate mode k=N/2 has
sin(πn)=0 (vanishing quadrature); complete pairing requires it to sit in a degenerate
group (λ(N/2)=12 is 5-fold at N=96/192 — k=16,32,48,64,80 — but 1-fold at N=64/80/128).
The unpaired mode has no weak-isospin doublet partner (phase freedom, representation
closure, symmetry closure all fail). Complete pairing is NOT required by count
conservation (B) or closure (D); it IS required by the doublet-structure observability
(A/C), the observable-sector construction (D_020). Everything downstream (p=3, N=96) is
DERIVED. Classification: pairing structure DERIVED; completeness BOUNDARY; self-conjugate
degeneracy DERIVED (N-arithmetic, 6|N); p=3/N=96 DERIVED. No canonical value changed.

ResearchY-D_033 (Singlet-Prohibition Audit, COMPLETE, tests 7/7 PASSED): why is an
unpaired self-conjugate mode physically forbidden? Verdict: the singlet is
MATHEMATICALLY allowed (L·cos₃₂ = 12·cos₃₂ verified at N=64 — a valid eigenfunction),
but PHYSICALLY excluded by the observable-sector structure. It breaks reciprocity (the
Z2 mirror maps k=N/2 to itself — no distinct partner), the spatial phase structure (no
sin harmonic, sin(πn)=0), the representation structure (1D eigenspace, no 2D doublet),
and the weak-isospin attachment (D_022). Normalization SURVIVES (the Fourier basis is
complete with or without the singlet). The prohibition is the observable-sector
requirement of a RECIPROCAL PAIR structure ("no isolated oscillator") — BOUNDARY
(D_020). The reciprocity/phase/representation closures are DERIVED consequences of the
pairing (D_021). Removal test: removing complete pairing breaks reciprocity first (one
frequency becomes a lone mode); the spectral content survives. Classification: singlet
allowed mathematically (DERIVED fact); prohibited physically (BOUNDARY, D_020); "no
isolated oscillator" principle EMERGENT. No canonical value changed.

ResearchY-D_034 (Reciprocity Audit, COMPLETE, tests 6/6 PASSED): why must every
observable oscillator possess a reciprocal partner? Verdict: reciprocity = the
[magnitude, phase] complex structure (QG218). Every observable mode must carry two
independent real DOFs — magnitude |ψ| = √ρ (the branching count, QG216, Difference's
count face) and phase θ (the U(1) link connection, QG63, Actualization's link face).
The complex structure (two DOFs) is DERIVED: real-only states give classical addition
(no interference), complex states give interference P = 2 + 2cos(θ₁−θ₂). Reciprocity
(every mode complex) is the EMERGENT observable requirement; complete pairing (0
unpaired) is BOUNDARY (D_020). What is fundamentally lost without reciprocity:
INTERFERENCE first (the singlet frequency becomes real-only/classical), then phase
freedom, doublet structure, and weak-isospin; normalization survives. Prove/refute:
observable structure (as a complex/interference structure) requires reciprocal partners
— YES. Classification: magnitude/phase/complex structure DERIVED (QG216/63/218);
reciprocity EMERGENT; complete pairing BOUNDARY; N=96 DERIVED. No canonical value
changed.

ResearchY-D_035 (Multiplet-Requirement Audit, COMPLETE, tests 6/6 PASSED): why must the
self-conjugate mode participate in a degenerate multiplet? Verdict: the self-conjugate
mode k=N/2 is REAL-ONLY (sin(πn)=0); its eigenvalue λ=12 has a 1D real eigenspace at
N=64/80/128 (an isolated singlet violating complex observability) and a 5D eigenspace
at N=96/192. Complex observability (every observable frequency must carry [magnitude,
phase], QG218/D_034) requires every eigenvalue to have multiplicity ≥ 2. At N=96 every
eigenvalue has mult ≥ 2 (complete pairing); at N=64 λ=12 has mult 1 — the real-only
singlet violates complex observability. The degenerate multiplet supplies the
phase/quadrature partners. REFINEMENT: complete pairing (0 unpaired) is DERIVED from
complex observability — the boundary moves one step deeper, from "0 unpaired" to "the
observable sector is complex" (BOUNDARY). Classification: complex structure DERIVED
(QG218); complex observability EMERGENT (the requirement); complete pairing DERIVED
from it; "observable sector is complex" BOUNDARY; N=96 DERIVED. No canonical value
changed.

ResearchY-D_036 (Complex-State-Origin Audit, COMPLETE, tests 7/7 PASSED): why must
observable states be complex? Verdict: the complex state ψ = |ψ|·e^{iθ} is DERIVED —
the two real DOFs are the two faces of the SAME actualization tick k: magnitude
|ψ| = √ρ (count face, QG216) and phase θ = 2πk/N (circulation face, QG220; link
connection, QG63). The complete amplitude ψ_k = √(μ^k/S)·e^(2πik/N) has Σ|ψ|² = 1 EXACT
(Born rule). The phase is the PAIRING DISCRIMINATOR: it distinguishes k from N−k (the
Z2 pairing, D_021) — in a magnitude-only (1-DOF real) space
cos(2π(N−k)n/N) = cos(2πkn/N), so the mirror pair collapses and no doublet/weak-isospin
sector exists. Removing the phase breaks the pairing first; removing the magnitude
empties the count structure (uniform sector). Interference P = 2 + 2cos(θ₁−θ₂) is a
DERIVED consequence, not the cause. REFINEMENT: "the observable sector is complex"
(D_035) REDUCES to the Z2-paired sector requirement (D_020) — the boundary count does
not increase. Classification: magnitude DERIVED (QG216); phase DERIVED (QG220); complex
state DERIVED (QG218); interference DERIVED; complex observability EMERGENT (= the
two-DOF sector requirement); Z2 pairing STRUCTURE DERIVED (D_021); complete pairing
DERIVED (D_035); Z2-paired (complex) sector requirement BOUNDARY (D_020); N=96 DERIVED.
No canonical value changed.

ResearchY-D_037 (Reciprocity-Observability Audit, COMPLETE, tests 7/7 PASSED): why does
observability require complete reciprocity? Verdict: observability = complete state
RECONSTRUCTION — a complex state (D_036) carries two real DOFs (magnitude, phase), so
complete observation requires measuring BOTH quadratures. The {cos, sin} pair at ω_k —
both eigenfunctions of L at λ_k = λ_{N−k}, orthogonal (Σ cos·sin = 0), equal norm
(N/2 each), spanning the 2D eigenspace — is exactly the reciprocal-pair (Re/Im)
measurement basis (D_021): from both projections the state is reconstructed exactly
(z = a + ib); from one alone the phase θ is ambiguous (same a from (|ψ|=2, θ=π/3) and
(|ψ|=1, θ=0)). An isolated singlet (1D real, sin(πn) = 0) has only one quadrature
channel — its phase is unobservable, its state underdetermined, its cycle position
(reversibility, Δθ = 2πk/N per site) lost; the singlet's phase is pinned to π.
Removing reciprocity does not remove the spectrum — it removes the second quadrature
channel, so only the real part of each state remains observable. Prove/refute:
observable structure (as complete information of a complex state) requires reciprocal
partners — YES. Classification: complex state DERIVED (D_036); observability =
information completeness EMERGENT (the requirement); reciprocity (two-quadrature basis)
EMERGENT from complex-state observability; complete pairing DERIVED; Z2-paired sector
requirement BOUNDARY (D_020); N=96 DERIVED. No canonical value changed.

ResearchY-D_038 (State-Identity Audit, COMPLETE, tests 7/7 PASSED): why should an
observable state carry both magnitude and phase? Verdict: observability requires STATE
IDENTITY — each mode must be distinguishable from every other. Magnitude-only collapses
the state space: the [4,4,87] occupancy groups give only 3 distinct magnitudes for 95
modes (√(1/7), √(2/7), √(4/7); μ=2, J=3), and the mirror pair k/N−k is identical
(cos even). Phase-only restores 95/95 identity but loses probability content (uniform
|ψ|=1 — no branching/count structure). The complex state ψ = |ψ|·e^{iθ} is the minimal
complete observable state: 95/95 injective with Born rule Σρ = 1 EXACT over the
generation shares. Two real DOFs = a complex number. Prove/refute: observable state =
magnitude + phase — YES (the minimal complete identity). Classification: magnitude
DERIVED (QG216); phase DERIVED (QG220); complex state DERIVED (QG218); state identity
EMERGENT (information completeness); probability content DERIVED; interference/
reciprocity DERIVED (D_037); Z2-paired sector requirement BOUNDARY (D_020); N=96
DERIVED. No canonical value changed.

ResearchY-D_039 (State-Identity-Origin Audit, COMPLETE, tests 7/7 PASSED): why must an
observable state have a unique identity? Verdict: Difference IS DISTINGUISHABILITY —
the primitive's semantic content is the act of distinguishing one state from another.
State identity (each mode distinguishable) is therefore the primitive APPLIED to the
state space, not a separate boundary. The real-only space collapses the 95 modes to 48
distinct real states (47 mirror pairs + 1 self-conjugate: cos(2π(N−k)n/N)=cos(2πkn/N),
so k and N−k have no Difference between them) and further to 3 magnitude buckets.
Phase-only restores 95/95 identity but loses the count content. The complex space
ψ = |ψ|·e^{iθ} realizes Difference fully: 95/95 distinct with Born rule Σρ=1 EXACT.
Prove/refute: Difference implies distinguishability — YES (by definition). The only
boundaries remain the primitives {Difference, η} (D_027) and the Z2-paired sector
requirement (D_020). Classification: Difference (primitive) BOUNDARY (D_027);
distinguishability DERIVED; state identity DERIVED (the primitive applied); complex
state DERIVED (QG218, minimal identity space); observability EMERGENT; interference/
reciprocity DERIVED (D_037); Z2-paired sector requirement BOUNDARY (D_020). No
canonical value changed.

ResearchY-D_040 (Boundary Reclassification Audit, COMPLETE, tests 7/7 PASSED): which
D_020 boundary assumptions remain after D_021–D_039? Verdict: the D-chain reduces to
exactly FOUR irreducible boundary inputs — {Difference, η} (the primitives, D_027/
D_039), {Z2-paired (complex) sector} (the observable-sector input, D_020; "observable
sector is complex" REDUCES to it, D_036), {3 octave families} (span ∈ [4,8) window,
D_020), and {SU(2) gauge + j=1/2} (weak-isospin input, D_022/D_024). RECLASSIFIED from
BOUNDARY: complete pairing → DERIVED (D_035: every eigenvalue mult ≥ 2 from complex
observability); singleton prohibition → DERIVED (D_035/D_037); p=3 → DERIVED (D_031);
N=96 → DERIVED (D_031/D_020); su(2) compact-form → EMERGENT (D_026: selected by
observability, not a free gauge input); state identity EMERGENT → DERIVED (D_039:
Difference applied). CONFIRMED BOUNDARY: Z2-paired sector, 3 octave families, SU(2)
gauge + j=1/2, {Difference, η}. CONFIRMED EMERGENT: weak-isospin doublet reading,
reciprocity, complex observability, observability, zero-defect set. All six historical
contradictions (complete pairing, p=3, N=96, singlet, su(2) compact-form, state
identity) are RESOLVED by superseding audits; the chain is monotone (boundaries only
moved downward). Final irreducible boundary set B_final = {Difference, η} ∪
{Z2-paired sector} ∪ {3 families} ∪ {SU(2) gauge + j=1/2}. No new primitive; canonical
AT unchanged.

ResearchY-D_041 (Time-Origin Audit, COMPLETE, tests 6/6 PASSED): is time the first
physical dimension? Do Actualization ticks already constitute physical time? Verdict:
the tick k is a DIMENSIONLESS branch-depth count (D_012) providing ORDERING (DERIVED,
QG220). It also serves as the natural TIME PARAMETER (EMERGENT): θ_k = 2πk/N advances
linearly per tick (Δθ = 2π/N), so N ticks close the cycle 2π (gauge trivial).
FREQUENCY EMERGES from the tick phase rate: ω₁ ≈ √91·(2π/N) = √91 ×
phase-quantum-per-tick (verified ~9.50 vs √91 ≈ 9.54); the spectral ratios ω_k/ω₁ are
exact dimensionless (ω₂/ω₁ ≈ 1.97, the octave; span = 6.4025). ENERGY does NOT emerge
without an anchor: E = ħω requires ħ (BOUNDARY, D_010/D_012); the dimensionless
energy-content ratios are DERIVED, the dimensionful scale is BOUNDARY (anchors v, m_e).
Dimensionful time (seconds) is BOUNDARY — it needs a physical clock/Hz anchor (D_008/
D_010). Prove/refute: time is the first physical dimension — NO; the tick is the first
DIMENSIONLESS parameter. Classification: ordering DERIVED (QG220); tick as time
parameter EMERGENT; dimensionless frequency EMERGENT (from the tick phase rate);
dimensionful time/frequency/energy BOUNDARY (anchors v, ħ). No canonical value changed.

ResearchY-D_042 (Fundamental-Ratio Audit, COMPLETE, tests 6/6 PASSED): does D96
contain a fundamental ratio analogous to circumference/diameter = π? Verdict: YES for
the ROLE — span = ωmax/ω₁ = 6.4025 is the structural ratio of the C96 ring (spectral
extent ÷ fundamental), the natural dimensionless reference for the frequency-scale
hierarchy (D_028). The π-analogy INVERTS the classification: π is transcendental (value
BOUNDARY, B_002 — the integer-matrix spectrum cannot output it); span is algebraic
(same integer-matrix spectrum), hence DERIVED. π is imported; span is derived.
INVARIANCE: all spectral ratios (span, λmax/λ₂ = 40.99, ω₂/ω₁ ≈ 1.97, moments) are
invariant under N-PRESERVING ring automorphisms (verified k→5k, 7k, 11k, 13k preserve
the spectrum multiset); but NO ratio is universal ACROSS N (span ~ 0.0578·N, monotone
4.02→12.78; λmax/λ₂ non-monotone 40.99→41.10) — there is no universal N-invariant ratio
analogous to π's scale-independence. HIERARCHY: span → family count floor(log₂ span)+1
= 3 (D_028); ω₂/ω₁ ≈ 1.97 → the octave (D_030); λmax/λ₂ → the scale gap; A³ =
(Σm·#g·occ₂)³ = 4.8094e16 → the dimensionless Planck content (D_007); ω₁ = 0.6216 →
the universal dimensionless reference (D_008/D_011) — all DERIVED. Classification: span
role EMERGENT (structural reference); span VALUE DERIVED (algebraic); ratio family
DERIVED; π VALUE BOUNDARY (unchanged, B_002); universal N-invariant ratio NONE. No
canonical value changed.

ResearchY-D_043 (Dual-Anchor-Necessity Audit, COMPLETE, tests 6/6 PASSED): why does a
dimensionless structure require multiple physical anchors? Is the need for {v, m_e}
fundamental or emergent? Verdict: the dual-anchor necessity is EMERGENT from sector
splitting. The D96 dimensionless structure (D_041/D_042) hosts two physically distinct
sectors: the BOSONIC (gauge/gravity: M_W/M_Z/M_H/M_Pl = v·(dimensionless)) and the
FERMIONIC (matter: m_u..m_t = m_e·(dimensionless)). Each sector's absolute scale
requires its own anchor; no canonical dimensionless factor links them (m_e/v ~ 2e-6 is
not a spectral number, D_013 H1 REFUTED). ONE anchor fails: m_u/v = (m_e/v)·(Σ√m/√Σm²)
~ 2.3e-6 requires m_e as a second independent input (D_012/D_013 irreducible). The two
sectors are distinct observables with distinct scale relations — there is no common
dimension principle. Prove/refute: multiple anchors are required whenever observables
split into physically distinct sectors — YES (each sector needs its own absolute scale;
a single anchor sets only one sector's scale). Classification: dimensionless structure
DERIVED (D_041/D_042); observable sector split DERIVED (D_014); anchor count EMERGENT
(from sector splitting); each sector's anchor (v, m_e) BOUNDARY (dimensionful input);
single-anchor failure DERIVED; dimensionful physics EMERGENT. No new primitive; No
canonical value changed.

ResearchY-D_044 (Anchor-Origin Audit, COMPLETE, tests 6/6 PASSED): what is the
physical origin of v and m_e? Can the anchor values be derived or are they irreducible
physical constants? Verdict: v and m_e are OBSERVABLE-SECTOR BOUNDARY VALUES, not
hidden outputs of a deeper process. v has a PARTIALLY-DERIVED structure:
v = 137·ln(span) = 254.37 GeV (QG168) — 137 = Σm+#d is the fine-structure denominator
(1/α_em ≈ 137.036) and ln(span) = ln 6.4025 is the derived spectral span (D_028); so
v's dimensionless VALUE is D96-derived, and only the GeV UNIT is the calibration anchor
(BOUNDARY). m_e = 0.511 MeV has NO D96 construction (D_013/D_014) — a pure boundary
value (the fermionic anchor). Neither defines the other: v/m_e ≈ 4.98e5 and ln(v/m_e) ≈
13.1 are NOT canonical spectral numbers (D_013 H1/H2/H3 REFUTED). M_Pl/v = A³ =
(Σm·#g·occ₂)³ = 4.8094e16 is DERIVED (D_007, a pure D96 ratio); m_e/ω₁ ≈ 8.2e-4 and
v/ω₁ ≈ 409 are anchor-over-reference ratios, not spectral invariants. Replacing an
anchor re-scales its sector (v → bosonic, m_e → fermionic); the dimensionless structure
(couplings, mixings, ratios) survives. So both anchors are B (sector-boundary): NOT
arbitrary calibrations (v's structure is derived), NOT hidden outputs (no deeper
mechanism), but observable-sector boundary values. Classification: v dimensionless
structure DERIVED (137·ln span, QG168); v GeV unit BOUNDARY; m_e value BOUNDARY (no
construction); anchor independence BOUNDARY (D_013); M_Pl/v DERIVED; anchor roles
EMERGENT (D_014); dimensionful physics EMERGENT. No new primitive; No canonical value
changed.

ResearchY-D_045 (Cosmological-Anchor Audit, COMPLETE, tests 6/6 PASSED): can
cosmological scaling generate v and m_e? Verdict: NO — the anchors are INDEPENDENT of
the cosmological state (option A). The density state ρ produces DIMENSIONLESS fractions
only: ΩΛ = I_occ/ln K = 0.7513/ln 3 = 0.6839, Ωm = 1−ΩΛ = 0.3161 (QG234, DERIVED,
dimensionless). No cosmological ratio matches the anchor ratios: ΩΛ/Ωm = 2.16 vs
v/m_e ≈ 4.98e5; no ρ-quantity near m_e/v ≈ 2e-6 or ln(v/m_e) ≈ 13.1. ρ is a
dimensionless counting measure (QG216) — any f(ρ) is dimensionless, so converting it to
dimensionful v/m_e would be circular (the anchor would be the input, not the output).
v = 137·ln(span) = 254.37 GeV is a SPECTRAL quantity (span is N-fixed, D_028), not
ρ-dependent; m_e has no construction from ρ (D_013/D_014). If ρ changes, the Ω
fractions change (they ARE density fractions) but v, m_e, and v/m_e are unchanged
(fixed by the anchors). So: the density state adds the dimensionless fractions
(ΩΛ/Ωm, DERIVED); it does NOT generate the anchors. Classification: density state ρ
DERIVED (QG216); ΩΛ/Ωm DERIVED from ρ (QG234, dimensionless); v structure DERIVED from
D96 (D_044); m_e BOUNDARY (no construction); anchor independence from ρ DERIVED (this
audit: no ρ-ratio matches); cosmological scaling of anchors NONE. No new primitive;
No canonical value changed.

ResearchY-R_001 (V2.1 Boundary Program Closure Audit, COMPLETE, tests 4/4 PASSED): is
the V2.1 origin program complete? Verdict: COMPLETE. The final irreducible boundary set
has exactly FIVE items — {Difference, η} (primitives, D_027/D_039), {Z2-paired (complex)
sector} (observable-sector input, D_020/D_036), {3 octave families} (span ∈ [4,8) window,
D_020), {SU(2) gauge + j=1/2} (weak-isospin input, D_022/D_024), and {v, m_e}
(dimensionful anchors, D_012/D_044). Every other object in the D_020–D_045 chain is
DERIVED or EMERGENT: complete pairing, the singleton prohibition, p=3, 6|N, N=96, span,
family count, the octave, ΩΛ/Ωm, the v structure (137·ln span), M_Pl/v, the ratio
family, and the selector criteria are DERIVED (20 objects); reciprocity, observability,
the weak-isospin reading, the su(2) compact-form, and dimensionful physics are EMERGENT
(10 objects). 13 original boundary items reclassified (7 → DERIVED, 1 → EMERGENT, 6
confirmed BOUNDARY). The five remaining boundaries are irreducible DOCUMENTED INPUTS,
not gaps. No origin question remains OPEN (0 OPEN); no new primitive; canonical AT
unchanged. Chain: Difference → Actualization → tick → count → magnitude → phase →
complex state → identity → reciprocity → pairing → p=3 → 6|N → N=96 → Closure →
Spectrum → {v, m_e} → Dimensionful Physics.

ResearchY-R_002 (V2.1 Release Preparation, COMPLETE): prepare the branch for tagging.
Deliverables: milestone summary, release notes
(Docs/Publication/RELEASE_NOTES_V2_1.md), boundary reductions (13 items reclassified:
7→DERIVED, 1→EMERGENT, 6 confirmed BOUNDARY), the derived chain (Difference → …
→ {v, m_e} → Physics), and the remaining boundaries (the five-item irreducible set from
R_001). Recommendations: TAG `v2.1-boundary-program`; changelog section
`## [2.1.0] — 2026-08-30` (added to Docs/Publication/CHANGELOG.md); migration notes
NONE (V2.1 is research/audit only, canonical AT V2.0 unchanged). Verification: 372
xUnit tests passing, AT.App 0 errors, canonical D96 values re-verified. Status: READY
FOR TAGGING.

ResearchY-NP_001 (V2.2 New Physics Roadmap, COMPLETE): identify physics not explained
in V2.0/V2.1 that can now be investigated using the derived origin chain. Candidate
topics: antimatter/baryon asymmetry, dark matter (deficit effect derived, particle
reading open), dark energy/Λ (derived QG230/234), neutrinos (masses/Majorana derived,
ordering open, P2 pending), hierarchy problems (fermion hierarchy derived, v/m_e
irreducible D_044), measurement (basis = two-quadrature reconstruction D_037, collapse
QG73 open), information structure (I = KL(ρ‖uniform) QG228 + state identity D_039),
cosmological observables (n_s QG237, peaks QG238, full CMB open). RANKED by
novelty × testability × V2.1-dependence. TOP-10 V2.2 TARGETS: T1 measurement origin
(14/15, highest V2.1 dependence); T2 information-encoded measurement (14); T3 baryon
asymmetry (12, highest novelty); T4 neutrino mass hierarchy (12, P2 pre-registered);
T5 CMB anisotropy spectrum (12); T6 dark-matter particle status (11); T7 two-anchor
ratio v/m_e (13 — test reducibility, currently BOUNDARY); T8 Λ coincidence (11);
T9 collapse/observer (11); T10 cosmological initial conditions (11). Priority sequence:
T1/T2 (measurement/information) → T3 (baryon asymmetry) → T4 (neutrinos) → T5 (CMB)
→ T6–T10. Constraints: no new primitives; canonical AT unchanged; all targets respect
the five-item boundary set (R_001). Status: each target requires its own audit + xUnit
suite when taken up.

ResearchY-D_046 (ResearchY-Predictions Audit, COMPLETE, tests 6/6 PASSED): what new
predictions follow from ResearchY results that V2.0 could not state? Verdict: ResearchY
adds 8 structurally NEW predictions absent from V2.0 wording, because the origin chain
provides derivation paths V2.0 lacked. P1 (gauge, THEOREM): the spectral doublet is
O(2)-type (SO(2) + reflection), NOT SU(2) — no 3-generator algebra from the spectrum.
P2 (gauge, NECESSITY): su(2) compact-form is EMERGENT from finite-dim unitary
observability — sl(2,R)/su(1,1) cannot host the weak sector. P3 (closure, THEOREM):
N=96 is selected by the observable-sector construction, NOT closure — only 96 is a
zero-defect octave rung (11 rings, rung {96}). P4 (resonance, NECESSITY): frequency
EMERGES from the tick phase rate, ω₁ ≈ √91·(2π/N) (9.50 vs 9.54 asymptotic). P5
(resonance, THEOREM): span = 6.4025 is the algebraic N-specific π-analogue — no
universal N-invariant ratio (spans 4.02/6.40/12.78 at N=60/96/192). P6 (anchor,
CORRESPONDENCE): v = 137·ln(span) = 254.37 GeV — structure D96-derived, GeV unit
boundary. P7 (anchor, BOUNDARY): v/m_e ≈ 4.98e5 irreducible (no canonical factor;
falsifiable by finding a construction). P8 (family, THEOREM): 3 = floor(log₂ span)+1
with span(96) = 6.4025. Each prediction has a dependency chain into the origin path
and an explicit falsification path. Experimental ranking: P6 > P7 > P4; mathematical:
P1–P3, P5, P8 (verified by the suite). No canonical changes; research only.

ResearchY-D_047 (Degeneracy-Splitting Amplification Audit, COMPLETE, tests 7/7 PASSED):
can D96 degeneracy splitting AMPLIFY weak perturbations — is G = Δ(observable)/ε
unbounded as ε → 0? Verdict: YES, divergently, but ONLY on degeneracy-indexed
(structural) observables. Setup: L(ε) = L + ε·P with P a deterministic ±1 edge-sign
Laplacian (LCG seed 12345), swept over 9 decades; observables ΔA (attractor/eigenspace
count), ΔE (spectral entropy of the basin distribution p_i = m_i/N), Δλ_rms.
DEGENERACY-LOCK THEOREM (DERIVED): full resolution gives E₁ = ln N, so the released
entropy is exactly ΔE_lock = (1/N)·Σ_{m_i>1} m_i·ln m_i — a closed form in the
multiplicity structure alone — INDEPENDENT of ε. Verified: D96 (42×2,[5],[6]) → 0.802314
nats; K96 (95-fold) → 4.506441; random (all-singleton) → 0. Headroom ΔA_max = N − A₀ =
Σ(m−1): D96 51, K96 94, D96³ 136 005 of 868 656. Because both are ε-independent STEP
functions, G_A and G_E ∝ 1/ε → DIVERGENT: G_A(D96) = 2.6e10 at ε=1e-9 vs 5.1e4 at
ε=1e-3; ΔA saturates at 51 from ε=1e-7 downward, ΔE at 0.802314 likewise. NEGATIVE
RESULTS: (a) the CONTINUOUS observable is NOT amplified — G_λ = Δλ_rms/ε is finite and
ε-independent, 0.0562 (D96) vs 0.0523 (degeneracy-free random control), i.e. the
amplification is purely a degeneracy effect; (b) RANDOM is an exact null — A ≡ 96,
ΔA ≡ ΔE ≡ 0 and G ≡ 0 for every ε up to 1.0 (min gap 0.0123 is an ε-independent
barrier); (c) D96³ is the hardest amplifier (peak G_A ≈ 3.5e12, ≳ 2000× D96) but
SATURATES at only 15.7 % of its headroom — factor-wise perturbations leave the
factor-permutation symmetry exact, so permuted triple sums λ_i+λ_j+λ_k stay exactly
degenerate (entropy recovers 57.5 % because unsplit permutation orbits carry large
multiplicities). Classifications: ΔE_lock and ΔA_max DERIVED; the ε-independence
(step response at ε = 0) EMERGENT; 'D96 amplifies its continuous spectrum' REFUTED;
case selection and the numerical resolution floor (ε* ≈ 1e-7, no intrinsic scale)
BOUNDARY. NO reclassification of any prior result; the D_040 ClassificationRegistry is
untouched. No canonical changes; research only.

ResearchY-D_048 (Latent-Degeneracy Adaptability Audit, COMPLETE, tests 7/7 PASSED): does
latent degeneracy predict adaptability? Defines the latent fraction L = (N − A₀)/N and
measures, for the six 96-node cases {D96, D96-3D (4×4×6 torus), random (sparse p=0.3),
physical (λ_m = m circulant), unphysical (5/25/60 circulant), complete (K96)}, the
reachable attractor increase ΔA, entropy increase ΔE, recovery R = 1 − ‖λ′−λ‖₂/‖λ‖₂,
and structural diversity gain ΔA/(N−A₀) under edge deletion, edge addition, rewiring and
weight perturbation at doses 0.5–10 % of |E| (3 seeds, connectivity guard; ZERO samples
excluded). Quantitative only — NO AT assumptions used. BASELINES: D96 A₀=45 λ₂=0.3864
L=0.5312; D96-3D A₀=13 λ₂=1.0 L=0.8646; random A₀=96 λ₂=17.19 L=0.0; physical A₀=49
λ₂=1.0 L=0.4896; unphysical A₀=4 λ₂=5.0 L=0.9583; complete A₀=2 λ₂=96 L=0.9792. VERDICTS
(Spearman ρ, tie-averaged ranks, n=6): H1 'adaptive capacity scales with latent
degeneracy' → PARTIALLY REFUTED: the NORMALIZED capacity is uncorrelated with L
(ρ = −0.029) even though L spans 0→0.98, while the RAW gains do scale — ρ(ΔA,L) = +0.429
and ρ(ΔE,L) = +0.829. L is the CEILING (ΔA ≤ N − A₀ by construction), not the response:
complete has the largest reservoir (L = 0.979) and the LOWEST capacity (0.286), D96 a
moderate one (0.531) and the HIGHEST (0.990). H2 'L predicts adaptability better than
λ₂' → REFUTED: ρ(capacity, λ₂) = −0.841 vs ρ(capacity, L) = −0.029 (≈29× larger |ρ|),
with the sign INVERTED — a SMALLER gap ⇒ a MORE adaptable graph; near-degeneracy, not
degeneracy count, indexes adaptability (D96's gap is 248× smaller than complete's and its
capacity 3.5× larger). H3 'random graphs have low latent capacity' → SUPPORTED: L = 0 and
capacity = 0 EXACTLY, ΔA ≡ ΔE ≡ 0 at every dose and every perturbation type. Dose
response: D96 saturates immediately (0.962 at the 0.5 % dose, 1.000 by 5 %); D96-3D and
complete are dose-limited (0.363→1.000 and 0.149→0.378); physical/unphysical plateau at
0.750; random flat zero. Type response: WEIGHT perturbation is the universally strongest
lever (1.000 on every structured case); exact zeros mark structurally impossible
perturbations (add and rewire on K96, add on the unphysical profile). SIDE FINDING:
recovery and adaptability trade off — the least adaptable graph (random, recovery 0.9792)
is nearly the most stable. Relation to D_047: the lock's RELEASE is a step in ε (D_047);
WHICH graph collects the most of its lock is set by λ₂, not by L (D_048) — proximity of
degeneracy beats its size. Classifications: the L-bound and the two rank relations
DERIVED; the two rejected hypotheses REFUTED; per-case capacities and plateaus EMERGENT;
protocol choices (case set, dose grid, seeds, connectivity guard) BOUNDARY. NO AT claim,
value, equation or registry entry changed; the D_040 ClassificationRegistry untouched.
No canonical changes; research only.

ResearchY-D_049 (Adaptability–Robustness Frontier Audit, COMPLETE, tests 7/7 PASSED): is
there a universal tradeoff Adaptability × Robustness = const? Uses the SAME two axes and
the SAME deterministic perturbation ensemble as D_048 (now shared via
AT.Tests/Shared/AdaptabilityAudit.cs) for the six 96-node cases {D96, D96³ (here the 3-D
D96 variant = 4×4×6 torus, N = 96, matching D_048; D_047's 96³-mode tensor product is a
different object), random, complete, physical, unphysical}. Fit: capacity vs recovery —
linear OLS plus the power law C = k·Δ^β in damage Δ = 1 − recovery, compared against the
conserved-product law (β = −1) forced through the same data. AXES: D96 C=0.9902 R=0.9659;
D96³ 0.7207/0.9625; random 0.0000/0.9792; physical 0.8011/0.9558; unphysical 0.7228/0.9443;
complete 0.2858/0.9868. OUTPUT: **REFUTED** — 'Adaptability × Robustness = const' is NOT a
universal law. The product spans 0 → 0.956 (the degenerate-free null gives EXACTLY 0) and
0.282 → 0.956 among the five non-null cases (span 3.39×, CV 0.326, worst deviation from the
mean > 50 %). The conserved-product law has the WRONG SIGN: capacity RISES with damage —
fitted C = 7.98·Δ^(+0.737) with R² = 0.720 — whereas C·Δ = const demands exponent −1;
forcing −1 gives R² = −3.282, worse than predicting the mean (a doubling of damage buys
only 1.666× capacity). **DERIVED** — the (capacity, recovery) Pareto frontier has EXACTLY
TWO members, D96 (max adaptability) and complete (max robustness), while D96³, random,
physical and unphysical are STRICTLY DOMINATED (they give up adaptability without buying
robustness), so a tradeoff EXISTS but it is a 2-point frontier, not a constant.
**DERIVED** (mechanism) — ONE structural quantity governs both axes with OPPOSITE signs:
the degeneracy COUNT (ρ(capacity) = +0.886, ρ(recovery) = −0.543), equivalently the inverse
gap (ρ(capacity, λ₂) = −0.841, ρ(recovery, λ₂) = +0.464, with ρ(degen, λ₂) = −0.814); the
degeneracy FRACTION L is a far weaker predictor (ρ(capacity, L) = −0.029), so the COUNT
indexes the tradeoff, not the share — a sharp refinement of D_048. **EMERGENT** — the
fitted exponent β ≈ 0.74 and constant k ≈ 7.98; the particular dominated set; and the
frontier INSTABILITY across perturbation families: edge deletion collapses the frontier to a
SINGLE case ({physical} — no tradeoff at all under that family), edge addition gives four
members {D96, physical, unphysical, complete}, rewiring three {D96, physical, complete},
weight three {D96, random, complete}. Universality therefore fails TWICE: no conserved
constant, and no family-invariant frontier. Linear fit for reference: C = 16.48 − 16.46·R,
R² = 0.475. NO AT claim, value, equation or registry entry changed; the D_040
ClassificationRegistry untouched. No canonical changes; research only.

ResearchY-D_050 (Spectral Predictability Audit, COMPLETE, tests 5/5 PASSED): can adaptability
(capacity) and robustness (recovery) be PREDICTED from spectral quantities ALONE — λ₂,
degeneracy count, near-gap density (T_014 count of positive eigenvalues ≤ 2λ₂), distinct
eigenvalue count — and what is the MINIMAL predictor set? Inputs and targets are taken
UNCHANGED from the shared D_048/D_049 ensemble (AT.Tests/Shared/AdaptabilityAudit.cs, same
4 perturbation types × 5 doses × 3 seeds, connectivity-guarded), so the D_049 tie-out is a
REGRESSION TEST, not a re-measurement: D96 capacity 0.9902 (published 0.9902), complete
0.2858 (published 0.2858), random 0.0000 (published 0.0000). DATA (capacity | recovery |
λ₂ | degen | near-gap | distinct): D96 0.9902|0.9659|0.3864|44|2|45; D96³ 0.7207|0.9625|
1.0000|11|6|13; random 0.0000|0.9792|17.1888|0|71|96; physical 0.8011|0.9558|1.0000|47|4|49;
unphysical 0.7228|0.9443|5.0000|3|32|4; complete 0.2858|0.9868|96.0000|1|95|2.
COLLINEARITY (Spearman): λ₂↔near-gap 0.986, degeneracy↔near-gap −0.886, λ₂↔degeneracy
−0.841 (D_049 measured −0.814), distinct count correlates with nothing (|ρ| ≤ 0.37) and
spans only 94 levels — the four inputs are NOT four independent facts; a four-input fit is
fitting ONE axis four times. UNIVARIATE (best LOO): capacity best = near-gap density
(ρ = −0.886, R² = 0.759, LOO 0.2962) vs degeneracy count (ρ = +0.886, R² = 0.529, LOO
0.3318) vs λ₂ (R² = 0.295, LOO 1.7979) vs distinct (R² = 0.152, LOO 0.6519); recovery best =
near-gap density (ρ = +0.486, R² = 0.525, LOO 0.0129) vs λ₂ (R² = 0.536 but LOO 0.0314) vs
degeneracy count (ρ = −0.543, R² = 0.102) vs distinct (R² = 0.067). The collinear pair
DISAGREES between the two criteria: capacity's RANK is an exact tie (|ρ| = 0.886 for both,
opposite signs) while its VALUE fit prefers the near-gap density; recovery's RANK prefers the
count (0.543 vs 0.486) while its value fit prefers the density (0.525 vs 0.102). MINIMAL
PREDICTOR SET (parsimony by leave-one-out, decided BEFORE knowing the winner): **{ near-gap
density } for capacity** (LOO 0.2962) and **{ near-gap density } for recovery** (LOO 0.0129) —
ONE spectral quantity for both targets, not four. OVERFITTING DEMONSTRATION: the all-four-input
capacity model has the BEST in-sample R² (0.972) and the WORST out-of-sample error (LOO 31.0123
= 105× the singleton's 0.2962) on five parameters over six points; its two-input variant is
worse than its own singleton (0.3164 vs 0.2962). FITTED RELATIONS: capacity =
−0.00814·(near-gap density) + 0.87153 (R² = 0.759); recovery = +0.000284·(near-gap density) +
0.95582 (R² = 0.525). RESIDUALS of the minimal capacity fit (fitted − measured): D96 −0.1349,
D96³ +0.1020, random +0.2939, physical +0.0379, unphysical −0.1117, complete −0.1872; largest =
0.2939 = 30 % of the FULL capacity range — the law gets the ORDER right while missing individual
values by up to a third of the scale, and every residual above 0.10 sits at an endpoint of the
axis. **DERIVED** — the scales 0 ≤ capacity ≤ 1 (ΔA ≤ N − A₀ headroom is an identity, D_048) and
0 ≤ recovery ≤ 1 (a normalized distance); the random case is a DERIVED null (zero degeneracy ⇒
EXACTLY zero capacity, D_047/D_048) and is exactly where the fit fails (over-predicts by 0.2939);
the degeneracy axis is DERIVED (capacity bounded by ΔE_lock = (1/N)Σ m ln m, recovery by T_015's
split statistics), so a spectral predictor can only ever be a PROXY for a combinatorially
determined quantity. **EMERGENT** — the fitted forms and coefficients (one axis, two
opposite-signed readings — D_049's mechanism), the one-predictor minimal set, and the
rank-vs-value disagreement within the collinear pair; the coefficients are ensemble-specific and
must NOT be presented as AT-derived constants. **REFUTED** — "a four-input spectral law determines
capacity and recovery" (catastrophic overfitting; three of four inputs collinear); "λ₂, near-gap
density and the distinct count are independent predictors" (λ₂↔near-gap ρ = 0.986; near-gap adds
nothing beyond its own singleton); and STRONG spectral determination itself — D_049 shows the same
spectral profile yields different capacity under different perturbation FAMILIES (edge deletion
collapses the frontier to one case), so the target is a function of spectrum AND family, and any
spectrum-only predictor is structurally incapable of being exact. ANSWER: **partly, and only as an
ORDERING** — the honest form of any future claim is "capacity and recovery are ORDERED by the
degeneracy axis", NOT "they are DETERMINED by the spectrum". NO AT claim, value, equation or
registry entry changed; the D_040 ClassificationRegistry untouched; the D_047/D_048 bounds and
D_049's family dependence are cited, not altered. No canonical changes; research only.
<!-- D_051 → D_052 open question below -->
→ D_050 left ONE open question for the D group: the residual gap (30 % of range) was attributed to
the perturbation FAMILY, which D_050 deliberately did not supply as an input. → ANSWERED by D_051
(blind prediction on six new rings): the law fails out of sample on the ring family, and the cause
is not the family input but that the predictor is CONSTANT there — the degeneracy count DID vary
across those rings (38 … 47) while the near-gap density did not. → TESTED by D_052 below.

ResearchY-D_051 (Blind Prediction Audit, COMPLETE, tests 5/5 PASSED): does D_050's spectral
prediction actually work on spectra it has never seen? Tests D_050 PROSPECTIVELY under a blind
protocol enforced by FILE ORDER IN GIT: PHASE A (commit 003a0033) generates six 96-node circulant
rings absent from D_048–D_050, reads their spectral inputs and prints the prediction — that commit's
test file contains NO measurement code; PHASE B measures them afterwards under the identical shared
ensemble (4 types × 5 doses × 3 seeds, connectivity-guarded). NEW RINGS (λ₂ | near-gap(2λ₂) |
degeneracy): S96-123 ±1..±3 (0.059822 | 2 | 44); S96-135 ±1,±3,±5 (0.148851 | 2 | 43); D96-24
±1..±12 (2.693016 | 2 | 38); Decay96 w_d = 1/d (0.089286 | 2 | 47); Boost96 w_d = d (1.870529 | 2 |
46); Ring48 ±1..±6 + ±24,±48 (5.404246 | 8 | 44). FROZEN PREDICTION (capacity | recovery): FIVE of
the six rings 0.85525 | 0.95639, Ring48 0.80641 | 0.95809 — the one-predictor law is DEGENERATE on
the ring family. OBSERVED (capacity | recovery): S96-123 0.96928 | 0.96044; S96-135 0.93693 |
0.96643; D96-24 0.97193 | 0.96937; Decay96 1.00000 | 0.91977; Boost96 0.99524 | 0.97537; Ring48
0.99248 | 0.97244. METRICS: capacity mean absolute error 0.1305, max 0.1861, against an OBSERVED
SPAN of 0.0631 — **the error is 2.07× the entire observed range of the truth**; recovery mean error
0.01617, max 0.03662, predicted span 0.00170 vs observed span 0.05560 (the law sees 3.1 % of the
real variation); distinct predicted capacities 2 vs distinct observed 6. RANK ERROR (tie-averaged):
capacity mean |Δrank| 2.00 of 5 (40 % of range), max 3.0, Spearman ρ = **−0.131** (ANTI-correlated);
recovery mean |Δrank| 1.33, ρ = +0.393. Every prediction errs on the SAME side (under-prediction),
because a ring's near-gap density sits at the MINIMUM of the predictor. **DERIVED** — near-gap = 2
is FORCED on every 1-D ring whose low-k spectrum follows the k² law (λ₂/λ₁ = 4, λ₃/λ₂ = 9/4 > 2 ⇒
only the k = ±1 doublet lies within 2λ₂, whatever the weight profile), so D_050's minimal predictor
set is a CONSTANT on the ring family — the family D96 itself belongs to — and D_050's R² = 0.759
must have been carried by its non-ring cases (random, complete, the 3-D torus); the sign of the
error is derived too; and D_050's bounds (0 ≤ capacity, recovery ≤ 1) and the zero-degeneracy null
survive intact. **EMERGENT** — all six structurally different rings adapt strongly (capacity
0.9369 … 1.0000: sparse, dense, tapered, long-range-weighted and extended rings alike collect
nearly all their headroom), and the measured recovery band is 33× wider than predicted. **REFUTED** —
the predictive claim (error 2.07× the observed range), the ranking claim (ρ = −0.131 for capacity),
and generalization beyond D_050's own ensemble; the single discrimination the law CAN make (it
orders Ring48 last on capacity) is itself wrong (observed: fourth). SUCCESS CRITERION MET — the
prediction is on record in commit 003a0033, whose test file never constructs the perturbation
ensemble, and Phase B re-derives D_050's own six cases from the frozen constants to prove they were
not adjusted after the fact. CONSEQUENCE: D_050's "ordered, not determined" answer stands WITHIN its
own ensemble but does NOT transfer to new spectra of the same family; a usable ring-family predictor
must read a quantity that actually varies across rings — the degeneracy count DID vary (38 … 47)
while the near-gap density did not, which is a concrete hypothesis for D_052. No canonical AT claim,
value, equation or registry entry changed; D_050's classifications are reaffirmed, not
reclassified; the D_040 ClassificationRegistry is untouched.

ResearchY-D_052 (Degeneracy Axis Audit, COMPLETE, tests 5/5 PASSED): does the DEGENERACY axis
predict adaptability WITHIN the ring family, where D_051 showed D_050's near-gap predictor is a
constant? Family = the seven 96-node rings (canonical D96 + D_051's six blind rings). Same blind
protocol: PHASE A (commit e5f9354f) reads four candidate predictors from each spectrum, fits ALL
coefficients on D_048/D_050's ALREADY-PUBLISHED six cases, and prints the prediction — that file
contains no measurement code; PHASE B measures the rings and compares. THE FOUR PREDICTORS (frozen
capacity slope/intercept, R² on the D_050 cases): NG near-gap 2 at k = 2 (−0.008136/0.871526, 0.759
— reproduces D_050 exactly, the integrity check); DEG degeneracy count (+0.012234/0.370622, 0.529);
ENT multiplicity entropy (+0.007055/0.568361, 0.001); LOCK D_047's ΔE_lock (±0.007055, 0.001).
RESOLVABILITY at prediction time: near-gap takes 2 distinct values across the seven rings (six 2s,
one 8), degeneracy count 5 (38, 43, 44, 46, 47), entropy and ΔE_lock 0.427 of span. OBSERVED
(capacity | recovery): D96 0.99020 | 0.96592 (reproducibility check); S96-123 0.96928 | 0.96044;
S96-135 0.93693 | 0.96643; D96-24 0.97193 | 0.96937; Decay96 1.00000 | 0.91977; Boost96 0.99524 |
0.97537; Ring48 0.99248 | 0.97244 (capacity span 0.06307, recovery span 0.05560). METRICS (R²,
Spearman, LOO RMSE). CAPACITY: DEG ρ = **0.815** vs NG 0.204 (4×), DEG refit R² 0.226 vs 0.068, DEG
frozen mean |error| **0.07399** vs NG 0.13116 (1.8×) — DEG WINS every ordering and accuracy metric;
NG wins LOO RMSE only (0.02437 vs 0.03304), because the degeneracy fit is unstable under refit while
NG is nearly constant, i.e. close to predicting the mean. RECOVERY: NG ρ = **0.408** vs DEG −0.222 —
NG WINS the ordering; DEG still wins refit R² (0.210 vs 0.066) and the frozen error marginally
(0.01275 vs 0.01522). ENT and LOCK are exactly anti-correlated (fitted slopes exact negatives) and
byte-identical everywhere; their frozen error is 5× DEG's (0.38469) — degenerate extra inputs buy
nothing (a second D_050 overfitting lesson). **DERIVED** — the degeneracy axis is NOT INJECTIVE on
rings: D96, S96-123 and Ring48 share the IDENTICAL multiplicity spectrum 1×6, 1×5, 42×2, 1×1, hence
identical count (44), entropy (3.7620) and ΔE_lock (0.8023), so every functional of the multiplicity
spectrum assigns them one number BY CONSTRUCTION, while their λ₂ values differ by a factor of 90
(0.386351 / 0.059822 / 5.404246) and their MEASURED capacities spread **0.02320 = 37 % of the whole
family span** (recovery 0.01200 = 22 %) — no degeneracy input can ever reduce that error, so the axis
is a COARSE invariant (5 groups vs near-gap's 2, but not sufficient); near-gap = 2 stays forced on
rings (λ₂/λ₁ = 4, λ₃/λ₂ = 9/4 > 2), which makes the baseline comparison a LOW bar; all bounds [0,1]
hold. **EMERGENT** — the degeneracy count IS the better capacity predictor within the ring family,
with the SAME POSITIVE sign as D_050's cross-ensemble +0.886, so "more degenerate levels ⇒ more
adaptable" now holds WITHIN one topological family and not only across unrelated graphs; all seven
rings adapt strongly (0.93693 … 1.00000) despite a 4× degree and 90× λ₂ spread; ΔE_lock adds nothing
beyond the multiplicity entropy; recovery goes the other way (DEG ρ = −0.222 vs NG +0.408, sign again
matching D_050's −0.543 but weak). **REFUTED** — the degeneracy count as a SUFFICIENT within-family
predictor (37 % of the family span sits inside one degeneracy value); the claim that the degeneracy
axis REPLACES the near-gap predictor (recovery goes the other way, and LOO stability favours near-gap
for BOTH targets); and adding degeneracy entropy / locked entropy (5× worse error). SUCCESS CRITERION
("beat the D_051 near-gap predictor") MET FOR CAPACITY, NOT MET FOR RECOVERY; prediction made before
measurement by construction. CONSEQUENCE: what remains open is not a better SPECTRAL scalar — the
multiplicity spectrum is the whole spectral-degeneracy data and is provably not injective — but a
NON-SPECTRAL input: the perturbation-family profile D_049 showed to move the frontier, which D_050
withheld and D_051 showed cannot be replaced by the spectrum. No canonical AT claim, value, equation
or registry entry changed; D_050's and D_051's classifications are reaffirmed, not reclassified; the
D_040 ClassificationRegistry is untouched.

ResearchY-D_053 (Perturbation-Family Dominance Audit, COMPLETE, tests 5/5 PASSED): is capacity
controlled MORE by the perturbation FAMILY than by spectral invariants? Two-factor design on the
seven 96-node rings × the four families (delete/add/rewire/weight), with a control that is EXACT in
one direction: a family change perturbs the SAME adjacency, so λ₂, degeneracy count, multiplicity
entropy and ΔE_lock are bit-for-bit fixed, while a spectrum change holds only the family. The main
comparison and the variance decomposition reuse per-family cells the shared ensemble ALREADY stores
(no new simulation); only §4 measures anything new. CAPACITY MATRIX (delete | add | rewire | weight):
D96 0.9843|0.9791|0.9974|1.0000; S96-123 0.9516|0.9464|0.9791|1.0000; S96-135 0.9229|0.9307|0.9529|
0.9412; D96-24 0.9544|0.9591|0.9918|0.9825; Decay96 1.0000|1.0000|1.0000|1.0000; Boost96 0.9932|
0.9891|0.9986|1.0000; Ring48 0.9804|0.9895|1.0000|1.0000. CONTROLLED COMPARISON: capacity — family
change (spectrum exactly fixed) mean range **0.0246** vs spectrum change (family fixed) **0.0631**,
ratio **0.39×**; recovery — family 0.0450 vs spectrum 0.0607 on range (0.74×) but family LARGER on
mean pairwise |Δ| (0.0255 vs 0.0197). VARIANCE DECOMPOSITION (7×4 = 28 cells, two-way layout):
CAPACITY grand mean 0.9794 — spectrum η² **74.5 %** (SS 0.011700, sd 0.0442), family **15.7 %** (SS
0.002471, sd 0.0287), interaction 9.8 %; RECOVERY grand mean 0.9614 — spectrum 39.1 %, family 30.5 %
(family main-effect sd 0.04738 EXCEEDS spectrum sd 0.03795), interaction 30.4 %. FAMILY ORDER PER RING:
D96 / S96-123 / Boost96 = weight > rewire > delete > add; S96-135 / D96-24 / Ring48 = rewire > weight >
add > delete; Decay96 = four-way exact tie. Across the six varying rings weight and rewire are ALWAYS
the top two and delete and add ALWAYS the bottom two (a consistent 2+2 split) while the order WITHIN
each pair flips — a noise effect would not preserve the split, a universal law would preserve the full
order. **DERIVED** — the control is ASYMMETRIC and the conclusion is therefore conservative: the family
axis is the perfectly controlled one and is STILL the smaller effect for capacity, so spectrum
dominance cannot be an artifact of weak control; Decay96 is EXACTLY family-invariant in capacity
(1.0000 × 4, row spread 0.0000) because its spectrum is 47×2, 2×1 — NO multiplicity greater than 2
exists, so no hard coincidence is left to resolve, making the invariance a structural statement and
not a fit; the 2+2 family split; and capacity as a SATURATING quantity, which regime-bounds the
spectrum effect. **EMERGENT** — the 74.5/15.7/9.8 split; family main effects on capacity weight 0.9891
> rewire 0.9886 > add 0.9706 > delete 0.9695 (weight perturbation breaks every symmetry at once
without removing a single edge — D_048's side finding reproduced on a new family); recovery is the
factor-balanced case; all seven rings adapt strongly (0.9369 … 1.0000) so the whole comparison lives in
the top decile of the scale. **REFUTED** — "capacity is controlled more by the family than by spectral
invariants" (η² 15.7 % vs 74.5 %; family spread 0.39× the spectrum spread); "holding spectral
quantities fixed isolates the dominant control"; mutual proxying of the two factors (9.8 % / 30.4 %
interaction means neither is a rescaling of the other); and the PURITY of the spectrum effect — §4's
fixed-ABSOLUTE-dose check shows the delete-family spectrum spread falling 0.0771 (fractional) → 0.0351
(k = 13) → 0.0065 (k = 29) → 0.0000 (k = 58), ratio 0.18×, with the FAMILY spread ALSO exactly 0.0000
at k = 58 (every sampled cell at capacity 1.0000). ANSWER: **NO** for capacity in every regime tested —
and in the saturated regime BOTH factors vanish together, so the question is ill-posed outside the
unsaturated regime (58 edges = 5 % of a degree-24 ring's edge count, 20 % of a degree-6 ring's).
REFINEMENT OF D_049: its family finding (edge deletion collapsing the frontier to one case) was
measured across a HETEROGENEOUS case set; within one homogeneous topological family the family effect,
though real and consistently ordered, is not dominant — so **family membership governs WHICH cases a
frontier contains, not how large a case's response is**. CONSEQUENCE: the family axis is the
better-BEHAVED measurement (it needs no dose matching, survives bit-for-bit spectral control, and keeps
a signature where capacity has already saturated) even though it explains less variance here. No
canonical AT claim, value, equation or registry entry changed; no new simulation primitive introduced;
the D_040 ClassificationRegistry is untouched.

ResearchY-D_054 (Low-Energy Spectral Edge Audit, COMPLETE, tests 6/6 PASSED): is capacity controlled by
the SHAPE of the low-energy spectral edge — λ₂, λ₃, λ₄, λ₅ and the ratios r3 = λ₃/λ₂, r4 = λ₄/λ₂,
r5 = λ₅/λ₂ — rather than by the near-gap density, the degeneracy count or the multiplicity spectrum?
Blind protocol: PHASE A (commit ae09d917) freezes the geometry, the coefficients (OLS on D_048/D_050's
six published cases), the decision rule (ρ > 0.815 AND LOO < 0.03304) and all predictions — no
measurement code in that file; PHASE B measures and compares. The required case set is the seven rings
of D_051–D_053, whose targets were ALREADY published, so that part is a PRE-REGISTERED REPLICATION; six
NEW edge-shape rings were declared in Phase A and measured for the first time in Phase B, which is the
genuinely blind component. **THE GEOMETRY SETTLES MOST OF THE QUESTION BEFORE ANY SIMULATION.** On a
circulant ring the eigenvalue at wavevector k equals that at N − k, so the bottom of the spectrum is a
DOUBLET: λ₂ = λ₃ and λ₄ = λ₅ in 12 OF THE 13 RINGS. Hence r3 = λ₃/λ₂ is EXACTLY 1 (two distinct values
in the whole family: 1.000000 and 1.089372, the latter only on G1248) and r5 ≡ r4. Only ONE scale-free
number survives: r4 = λ₄/λ₂, the second folded level over the first, which is 4.000000 for a pure
parabolic edge λ_k ∝ k² and therefore measures EDGE BENDING — 3.995718 (E1, barely bent) → 3.894202
(D96) → 3.617858 (D96-24) → 1.018556 (Ring48, whose ±48 offset flips the sign of cos almost every
step). Only G1248 breaks the pattern (λ₂ = 4.0, λ₃ = λ₄ = 4.35749, λ₅ = 6.0, r3 = r4 = 1.089372,
r5 = 1.5). **DERIVED BOUNDS DISQUALIFY AN INPUT FAMILY**: of 182 cells (7 inputs × 13 rings × 2 targets)
λ₂ … λ₅ leave [0,1] ZERO times — the source set spans λ₂ from 0.386 to 96, a far wider envelope than any
ring, so those lines are shallow — while the ratios leave it 12 times (r4 and r5 six each; worst E1
capacity 1.09533; r3 once at −0.49307 on G1248). Admissibility and resolution therefore pull in OPPOSITE
directions: the input that is admissible in linear form does not resolve rings, and the shape descriptor
that does resolve rings is inadmissible in linear form. METRICS ON THE REQUIRED SET (p is the EXACT
two-sided permutation p-value; all 7! = 5040 rank permutations enumerated). CAPACITY: λ₂ ρ = **0.214**
(p = 0.6615, LOO 0.02487), λ₃ identical, λ₄ = λ₅ ρ = 0.179 (p = 0.7131), r3 ρ = 0.000 (p = 1.0000),
r4 = r5 ρ = **−0.429** (six bounds violations each); baselines recomputed on the same set: near-gap
ρ = 0.204 (p = 0.8571), degeneracy ρ = **0.815** (p = 0.0310) — the edge shape LOSES on ρ by a factor
of four. RECOVERY: λ₂ … λ₅ all give ρ = **0.821** (p = **0.0341**), LOO 0.02220/0.02220/0.02069/0.02069,
beating near-gap 0.408 and degeneracy −0.222 — the FIRST D-group predictor to beat the D_052 field on
BOTH ρ and LOO — but NOTHING SURVIVES BONFERRONI correction for seven declared inputs (α/7 = 0.00714),
and the required set's near-gap LOO is not even DEFINED (its entire signal is the single ring at
near-gap 8, so dropping Ring48 leaves the input constant). HELD-OUT SET (first measurement): E1 0.98598,
E1-16-32 0.98440, G1248 0.99787, Wscale 0.99929, Two1-12 0.99574, **Pair1-47 0.42089** — capacity span
0.57840 against the required set's 0.06307 (nine-fold wider, carried by one point). INPUT RESOLUTION ON
THE HELD-OUT SET: near-gap takes 2 distinct values (2,2,13,2,2,2) and degeneracy 2 (47×5, 23) — both
separate exactly ONE ring — while λ₂ … λ₅ and r4, r5 take SIX, the only predictors that resolve every
held-out ring. AND THEY STILL LOSE: capacity ρ = 0.143 (p = 0.8028) against the degeneracy count's
0.655, LOO 0.35984 vs 0.23353 — so **RESOLUTION IS NOT PREDICTIVENESS**. **DERIVED** — the doublet
collapse (r3 ≡ 1, r5 ≡ r4, one surviving shape number); r3 varying on the calibration set and constant
on the target set, which is D_051's failure mode in MIRROR IMAGE, visible structurally with no
measurement, extrapolating to a NEGATIVE prediction (−0.49307 on G1248) and giving ρ = 0.000,
p = 1.0000; the derived bounds disqualifying r4/r5 in linear form; and λ₂ … λ₅ being ONE predictor
relabelled (identical ρ to three decimals on both targets, so adding λ₃/λ₄/λ₅ buys nothing — D_050's
lesson reproduced on a new input family). **EMERGENT** — the recovery ρ = 0.821 signal and its
directional held-out echo (ρ = 0.771, p = 0.1028); the near-total uniformity of ring capacity (12 of 13
at 0.94 … 1.00, so the case set has almost no variance left to explain); the
resolution-versus-predictiveness inversion. **REFUTED** — edge-shape control of capacity (ρ 0.214 vs
0.815, and r4/r5 NEGATIVELY correlated at −0.429); the edge shape beating the D_052 field for capacity
on either case set (the held-out set even gives the degeneracy count refit R² = 0.999); λ₃/λ₄/λ₅ adding
information beyond λ₂; and the shape ratios being usable predictors in linear form. GOAL ("beat the
near-gap, degeneracy and multiplicity-spectrum predictors"): NOT MET for capacity; MET on the required
set for recovery, but not confirmed and not surviving multiple-comparison correction. WHAT THE AUDIT
DOES ESTABLISH: the low-energy edge does not control capacity within this ring family — but the edge
GEOMETRY identifies the one ring that breaks the family: **Pair1-47 (±1 and ±47)** is the only audited
ring whose spectrum bottom is not the folded doublet pair (λ₂ at k = 2 rather than k = 1, near-gap 13,
degeneracy count 23, 25 distinct levels) and the only one whose capacity falls to 0.42089. That is an
EXISTENCE statement about the ring family, not a predictor: the family is not uniform, and the outlier
is found by its geometry rather than by any numeric fit. New open question for the D group: what makes
the non-folded edge (Pair1-47) collapse, given that every folded-edge ring saturates — the headroom
(96 − 25 = 71) is the largest in the family and yet only ~42 % of it is collected. No canonical AT
claim, value, equation or registry entry changed; no new simulation primitive (the required case set
reuses the shared cache; only the six held-out rings are measured); the D_040 ClassificationRegistry is
untouched.

ResearchY-D_055 (Pair1-47 Anomaly Audit, COMPLETE, tests 5/5 PASSED): WHY is Pair1-47 (offsets ±1 and
±47) the only ring whose capacity collapses? Compared against a healthy set of ALL 12 other audited
rings — the wider set is required, because E1 alone kills two plausible candidates: E1 has a 4.85×
SMALLER λ₂ (0.004282 vs 0.034221) and half the degree (2 vs 4) while being perfectly healthy at
capacity 0.98598. **THE PROPERTY: Pair1-47 has a SINGLE DEGENERATE LEVEL HOLDING 52.1 % OF THE
SPECTRUM** (multiplicity 50 of 96; pattern 1×50, 22×2, 2×1), against at most 12 of 96 (12.5 %, on
D96-24) for any healthy ring and 6 for the median healthy ring. **THE CAUSE IS EXACT ARITHMETIC**: with
S = {1, 47} and 47 = N/2 − 1, cos(2πk·47/96) = cos(πk − θ) = (−1)^k·cos(2πk/96), so for EVEN k
λ_k = 4(1 − cos θ_k) while for ODD k λ_k = 2[(1 − cos θ) + (1 + cos θ)] = **4 EXACTLY** — for all 48
odd k, plus k = 24 and k = 72, giving multiplicity 50 (verified: 48 odd modes, largest |λ_k − 4| = 0,
and 50 eigenvalues within 1e-9 of 4 in the actual Laplacian spectrum). In general an offset pair
{±1, ±d} produces this level exactly when d ≡ ±1 (mod N/2). REJECTIONS (is Pair1-47's value outside the
whole healthy interval?): λ₂ 0.0342 vs 0.0043…5.4042 → NO; mean distinct-level spacing 0.3463 vs
0.0850…1.4474 → NO; degree 4 vs 2…24 → NO; edges |E| 192 vs 96…1152 → NO. Rejected IN PRINCIPLE:
EIGENVECTOR LOCALIZATION / PARTICIPATION RATIO — every ring is a circulant, so every Laplacian
eigenvector is a Fourier mode with |v_i| = 1/√N and PR = **exactly N = 96** for every mode of every
ring (verified with the exact Fourier modes as eigenvectors), so no measurement of localization can
separate any two members of this family; and SYMMETRY CLASS — all eight opening rings are symmetric
circulants with the SAME dihedral group of order 2N, one orbit, vertex-transitive. ONE INDEPENDENT
SEPARATOR IS REPORTED RATHER THAN HIDDEN: the min positive gap DOES separate (0.034221 vs
0.0002…0.0211), but its witness is the spacing between the level at 7.965779 (k = 46) and the
singleton at 8.000000 (k = 48) — MID-spectrum, not the bottom — and it is a single scalar with no route
to the mechanism. THE REMAINING SEPARATORS ARE RESTATEMENTS: ΔE_lock (2.3552 vs 0.6787…1.1052),
headroom (71 vs 47…57), A₀ (25 vs 39…49), largest multiplicity (50 vs 2…12) and its share (0.5208 vs
0.0208…0.1250) are all functions of the SAME multiplicity multiset. THE INVERSION IN ONE LINE:
Pair1-47 has the LARGEST headroom AND the largest ΔE_lock in the family and the LOWEST capacity — more
locked entropy and more room, less collected. **THE MECHANISM — A RANK BUDGET**: deleting or adding an
edge subtracts a RANK-2 matrix, and a rank-r perturbation acts on an m-fold eigenspace as an m×m matrix
of rank ≤ r having at most r + 1 distinct eigenvalues, so **ΔA ≤ Σ_i min(m_i − 1, r)** with r ≤ 2k. A
spectrum whose headroom sits in ONE huge level is a RANK TRAP; the healthy rings are RANK-EFFICIENT
because their headroom is many small levels that one edge operation releases together. MEASURED (delete
family, ceiling capacity vs measured): Pair1-47 k=1 0.3380/0.3239, k=3 0.3944/0.3521, k=10
0.5915/0.4507, k=19 0.8451/0.5728; D96 k=1 0.9020/0.8562, k=3 1.0000/0.9281, k=10 1.0000/0.9804, k=19
1.0000/1.0000; S96-135 k=1 0.8627/0.8431. The ceiling predicts the collapse from the multiplicity
structure ALONE. SINGLE-OPERATION TEST: one deletion splits EACH level independently, so D96 gains
≤ 42 + 2 + 2 = 46 distinct eigenvalues while Pair1-47 gains ≤ 2 + 22 = 24 (measured 48 and 23) — its
49-slot level contributes at most 2 while D96's 42 doublets contribute 42 at once. Capacity therefore
depends on the multiplicity **DISTRIBUTION**, not the count or the headroom — which is exactly why
D_052's degeneracy count and D_050's near-gap density cannot see the anomaly; and Pair1-47 has the
FEWEST degenerate groups in the family (23 against D96's 44), so degeneracy COUNT is ANTI-correlated
with the collapse. **A DEFECT IN THE SHARED PERTURBATOR, FOUND AND QUANTIFIED**: the weight family reads
its sign from the LOW BIT of the LCG x ← 1664525x + 1013904223 (mod 2^32); both coefficients are odd, so
(a·x + c) mod 2 = (x + 1) mod 2 and the coin ALTERNATES DETERMINISTICALLY for every seed (reproduced:
0 1 0 1…, 1 0 1 0…, 0 1 0 1…). Consequences, all measured: the weight family yields exactly TWO distinct
weights (Pair1-47: 0.98×96 and 1.02×96; D96: 0.98×288 and 1.02×288; Decay96: 12 values); the
reweighted graph is neither circulant nor shift-symmetric (both checks False), so it is a FIXED pattern
rather than a preserved symmetry; and its effect is SPECTRUM-DEPENDENT — D96 still reaches A₁ = 96 while
Pair1-47 STALLS AT 51 with its λ = 4 level intact. With a CORRECTED coin (sign from the high bits, an
independent sign per edge, implemented LOCALLY so no stored number is touched): Pair1-47's weight
capacity 0.3662 → **0.9812** and its ensemble mean 0.4209 → **0.5746**, while every healthy ring is
essentially unchanged (S96-135 0.9412 → 1.0000; all others already 1.0000). So the defect DEPRESSES the
anomaly by about a third and does NOT create it — with the corrected coin the anomaly survives (0.5746
against 0.94 … 1.00 for every healthy ring), and the three edge-count families, untouched by the defect,
still show 0.41 / 0.40 / 0.50. SCOPE OF THE DEFECT: the WEIGHT family only — delete, add and rewire draw
their target from the full 32-bit value and their numbers stand; the DIRECTION of every audit since
D_048 (weight strongest) is unaffected, since a systematic reweighting is still a real perturbation;
what is unreliable is fine structure attributed to weight's "full-rank randomness". The generator is
deliberately NOT changed here, because altering it would invalidate stored numbers in D_048–D_054 — the
defect is reproduced and recommended as the next fix. DERIVED: the property and its half-period
congruence cause; the rank-budget ceiling ΔA ≤ Σ min(m_i−1, r); the rank-efficiency of the healthy
rings. EMERGENT: the measured magnitudes and the quantified defect contribution. REFUTED: localization
and participation ratio (in principle), symmetry class, λ₂, mean spacing, degree and edge count as
candidates; "many degenerate levels" as the cause; and the defect as the source of the anomaly. New open
question for the D group: does the rank budget predict the capacity of ANY graph from its multiplicity
distribution alone — a closed-form ceiling C_max(k) = Σ min(m_i − 1, 2k)/(N − A₀) — and does fixing the
perturbator's coin move the D_048–D_054 weight-family numbers enough to matter? No canonical AT claim,
value, equation or registry entry changed; no new simulation primitive added to the shared machinery;
the D_040 ClassificationRegistry is untouched.

ResearchY-D_056 (Multiplicity Distribution Audit, COMPLETE, tests 6/6 PASSED): is capacity controlled by
the FULL MULTIPLICITY DISTRIBUTION — max multiplicity, Gini(m), Entropy(m), Herfindahl(m),
largest-level share — beating the near-gap density (ρ 0.204), the degeneracy count (ρ 0.815) and λ₂
(ρ 0.214)? Blind protocol: PHASE A (commit fcd81f69) freezes the distributions, the coefficients (OLS on
D_048/D_050's six published sources), the decision rule, an outlier-removal check and every prediction —
for the required seven rings AND FOUR NEW rings; PHASE B measures. **CONVENTION, stated because it
matters: m is the multiplicity list of EVERY distinct level INCLUDING singletons, so Σm = N = 96
exactly** — dropping singletons would inflate every concentration statistic and break the normalization
Gini, entropy and Herfindahl need. **THE FIVE STATISTICS ARE ONE AXIS, NOT FIVE**: max multiplicity, Gini
and the largest share are mutually monotone and give IDENTICAL ρ (−0.964 for capacity, −0.037 for
recovery), while Entropy (+0.964) and Herfindahl (−0.964) are the exact mirror of that axis — the same
collinear-input collapse D_050 found, reproduced on a new input family. METRICS ON THE REQUIRED SET
(exact two-sided permutation p, 7! = 5040 enumerated): CAPACITY — max multiplicity ρ = **−0.964**
(p = **0.0024**, LOO 0.07781, R² 0.995); Gini −0.964 (LOO 0.12010, R² 0.987); Entropy +0.964
(LOO 0.04174, R² 0.995); Herfindahl −0.964 (LOO 0.48612, R² 0.994); largest share −0.964 (LOO 0.07781,
R² 0.995); rank ceiling (D_055) +0.906 (p = 0.0095, LOO 0.04892, R² **0.999**, best frozen error
0.19963). BASELINES recomputed on the same set: near-gap ρ = 0.204 with LOO **n/a (constant)** and
R² 0.036; degeneracy count ρ = **0.964** (p = 0.0024, LOO 0.12029, R² 0.987); λ₂ ρ = 0.536 (LOO
0.27469, R² 0.078). ⇒ THE DISTRIBUTION TIES THE DEGENERACY COUNT ON ρ EXACTLY (both 0.964, identical
p = 0.0024) and beats λ₂ and near-gap; it wins on R² (0.995 vs 0.987) and on the healthy subset; and
p = 0.0024 is the FIRST D-group result to SURVIVE MULTIPLE-COMPARISON CORRECTION (α/5 = 0.01 for five
declared inputs). The frozen rule (|ρ| > 0.815 AND LOO < 0.03304) is not met — but neither is it met by
the degeneracy count itself on this case set (0.12029), since the 0.03304 bar was inherited from D_052's
DIFFERENT case set. **RECOVERY IS NOT PREDICTED AT ALL**: every distribution statistic gives ρ = 0.037
with exact p = **0.9786** — indistinguishable from no association — while λ₂ reaches ρ = 0.821
(p = 0.0341). PRE-REGISTERED OUTLIER CHECK (Pair1-47 removed): ρ moves only from 0.964 to 0.941
(Δρ = 0.023), so the correlation does NOT rest on the single extreme ring, and on the six healthy rings
the distribution statistics (R² up to **0.784**, Herfindahl) BEAT the degeneracy count (0.528), λ₂
(0.125) and near-gap (0.058); the rank ceiling reaches **R² 0.978 and LOO 0.00511** there. NON-INJECTIVITY
INHERITED: D96, S96-123 and Ring48 share ONE multiplicity distribution exactly — identical max
multiplicity, Gini, entropy, Herfindahl and share — so every functional of the distribution assigns them
one number while their measured capacities spread 0.02320 = 4 % of the family span (the same triple
D_052 flagged). **THE NEAR-GAP BASELINE IS STRUCTURALLY UNUSABLE ON RINGS**: six of the seven rings sit
at near-gap 2 with Ring48 alone at 8, so its input becomes CONSTANT as soon as one ring is held out —
its LOO is UNDEFINED, not merely poor, so it is not a competitor that can be beaten or lose here. BLIND
TEST (four new rings, first measurement; the four were chosen to vary the distribution SHAPE rather than
its maximum): P47-16 0.69765 (dominant level partly dissolved, max multiplicity 34), P47-123 0.99618
(max m 4), H51123 0.97455 (no N/2 relationship, max m 6), and **P47-48 0.58920** — the only collapsing
blind ring. **THE SHARP FORECAST WAS CONFIRMED EXACTLY**: P47-48 was built by adding the
exactly-antipodal ±48 offset, whose odd-k contribution is 2(1 − cos πk) = 4, so the dominant level should
SURVIVE, MOVED from λ = 4 to λ = 8 — measured λ = **8.000000** with multiplicity **49** (Pair1-47:
λ = 4.000000, m = 50), and its λ₂ is **IDENTICAL** to Pair1-47's (0.034221) with near-gap 2, so the
near-gap predictor cannot see it at all. Rank agreement on the four blind rings is **PERFECT for every
input** (ρ = ±1.000; n = 4, 4! = 24 permutations, so the smallest attainable two-sided p is 2/24 =
0.0833), with the rank ceiling leading (LOO 0.01302, R² 0.999) against degeneracy 0.949, λ₂ 0.400 and
near-gap 0.258. **DERIVED** — the five-statistics collapse onto one axis with two signs; the rank-budget
ceiling leads on EVERY fidelity measure because it is DERIVED from the distribution by argument rather
than fitted from it; the inherited non-injectivity; the structural unusability of the near-gap baseline
on rings; and the λ = 8 clause. **EMERGENT** — the blind confirmation (four of four orderings, and
P47-48's collapse at λ = 8); the outlier check passing; and the poor source-fitted coefficients for every
input (mean errors 0.19 … 0.49 — D_051's lesson recurring, since coefficients fitted on D_048/D_050's
NON-RING sources extrapolate badly onto rings; the rank ceiling is least bad, 0.19963 required and
0.19593 blind). **REFUTED** — distribution control BEYOND the degeneracy count on ordering (identical ρ
and identical p); any distribution control of recovery (p = 0.9786); the independence of the five
statistics; and the near-gap baseline as a meaningful competitor on this family. SUMMARY: the multiplicity
distribution predicts ring capacity strongly and recovery not at all; its five statistics are one axis
that only TIES the degeneracy count on ordering while beating it on R² and on the healthy subset, and
reaches perfect ordering on four brand-new rings whose collapse was predicted in advance from the
distribution alone; the winner on every fidelity measure is D_055's DERIVED rank-budget ceiling. No
canonical AT claim, value, equation or registry entry changed; no new simulation primitive (the twelve
previously audited rings come from the shared cache; only the four new rings are measured); the D_040
ClassificationRegistry is untouched.

ResearchY-D_057 (Rank-Budget Law Audit, COMPLETE, tests 6/6 PASSED): does ΔA ≤ Σ_i min(m_i − 1, r),
the bound D_055 derived from the RANK of an edge operation, DIRECTLY predict capacity — replacing the
correlations of D_050–D_056 with a derived law? Three frozen law forms, in increasing fitted content:
L1 pure ceiling with ZERO parameters (C = mean_d min(1, Σ min(m_i−1, 2k_d)/(N−A₀))), L2 effective-rank
with ONE parameter per family (r_d = round(c·2k_d)), L3 tightness-corrected with ONE parameter per
family (C = L1·τ) — every parameter from D_048/D_050's six SOURCE cases, so every ring error is
OUT-OF-SAMPLE. PHASE A (commit bda8e880) verified the lemma synthetically and froze the predictions
against a frozen table of published values, running NO simulation. **THE LEMMA, VERIFIED OUTSIDE THE
ENSEMBLE**: 3200 random symmetric rank-r perturbations (m ∈ {4, 8, 16}, r = 1…6) supported on a
degenerate eigenspace, diagonalized by Jacobi — the split count reaches EXACTLY r + 1 at its maximum
with ZERO VIOLATIONS, so the bound is arithmetic on ranks. **THE BOUND ON THE REAL ENSEMBLE**: 600
samples (10 rings × 4 families × 5 doses × 3 seeds), ZERO VIOLATIONS — but INFORMATIVE ONLY WHEN r
BITES: strictly below full headroom in 100 % of delete/add/rewire samples, and VACUOUS for weight, where
r = N makes L1 = 1 by construction. **TIGHTNESS IS GOVERNED BY CONCENTRATION**: measured/ceiling runs
0.9653 … 1.0000 (mean 0.9871) for the eight SPREAD rings and 0.6633 … 0.8945 (mean 0.7789) for the two
DOMINANT-LEVEL rings, with ρ(share, tightness) = **−0.969** and ρ(max multiplicity, tightness) = −0.969
— because tens of small levels split simultaneously while one huge level consumes all of r in a single
eigenspace. The law's accuracy is therefore itself a function of the multiplicity distribution, which
EXPLAINS D_056: the distribution was predicting the TIGHTNESS, not the bound. **THE SAME-SHARE CONTROL
IS RESOLVED BY THE RANK, NOT THE DISTRIBUTION**: Pair1-47 and Half47 have IDENTICAL multiplicity
patterns (50×1, 2×22, 1×2), the same A₀ = 25 and the same headroom 71, so every functional of the
structure must give them one answer — and D_056's predictors indeed cannot separate them — yet degrees 4
and 6 (|E| = 192 vs 288) mean the SHARED FRACTIONAL dose grid hands them different ranks: at the top
dose Pair1-47 gets k = 19 (r = 38 < 49, still trapped) while Half47 gets k = 29 (r = 58 > 49, able to
split completely). THE LAW IS THE PAIR (multiplicity structure, rank), not the structure alone — which
also links back to D_053's dose-convention confound. HEAD-TO-HEAD (the laws' errors are OUT-OF-SAMPLE,
the correlational predictors' are in-family LOO — a protocol difference favouring the correlations):
L1 (0 params) ρ = **0.873**, RMSE 0.07745, mean |error| **0.03918** — matching the FITTED degeneracy
count (ρ 0.878, error 0.04052) with no parameters and no in-family information; **L2 (1 param/family)
ρ = 0.976 — the HIGHEST ordering in the audit — RMSE 0.03885, mean |error| 0.02287**, below the
degeneracy count's 0.04052, λ₂'s 0.12302 and near-gap's 0.11922; L3 (pooled τ) is the WORST of the three
at 0.21259 — the pooling error D_051 and D_056 also exposed; max multiplicity and largest share do have
a slightly smaller error (0.01762) but under the MORE GENEROUS protocol and at a lower ρ. THE HONEST
LIMIT — per-ring L1 out-of-sample error: the seven spread rings average **0.0143** (D96 0.0098,
S96-123 0.0160, S96-135 0.0337, Ring48 0.0075, Decay96 **0.0000**, Boost96 0.0048, D96-24 0.0281) versus
0.1431 for the dominant-level class — a factor of **10**. THE BLIND TEST CALLED BOTH VERDICTS
CORRECTLY: **Half47** (±1, ±24, ±47) — the ±24 term contributes 2(1 − cos(πk/2)) = 2 for odd k, so the
odd-mode sum is 2 + (2 − 2cos θ) + (2 + 2cos θ) = 6 and the level SURVIVES at a THIRD value — observed
λ = **6.000000** with multiplicity 50, capacity **0.61479** (against Pair1-47's 0.42089); **Triple47**
(±1, ±23, ±47) — 23 = N/4 − 1 gives 6 ∓ 2 sin θ, no cancellation, the level is DESTROYED — observed max
multiplicity 2, capacity **0.99823**, fully healthy. Two rings differing only in that one offset,
opposite verdicts, both correct: the law tracks the LEVEL, not any particular offset. REFUTED: the bound
as a direct equality (missed by up to 10× on concentrated structures); the distribution as a sufficient
statistic (the controlled pair differs by 0.19390 once ranks differ); L3's pooled correction; the weight
family as evidence; and rank as complete — at the 0.5 % dose BOTH rings get k = 1, r = 2 and the same
ceiling 0.3380, yet Half47 measures 0.23005 against Pair1-47's 0.32394, so a residual beyond (structure,
rank) remains. SUMMARY: the rank budget is a genuine DERIVED law — its lemma verified in 3200 synthetic
draws, its bound in every real sample, and a ZERO-PARAMETER version already ordering the rings and
landing within 0.0143 on spread structures — becoming a usable predictor with ONE rank scale per family
(ρ 0.976); but it is exact only where the multiplicity is spread, and not closed by (structure, rank)
alone. New open question for the D group: what the residual beyond (structure, rank) consists of — at
matched rank two rings with identical patterns still differ by 0.094 at the 0.5 % dose, which points at
the geometry of the perturbation's projection onto the degenerate eigenspace rather than at any scalar
of the spectrum. No canonical AT claim, value, equation or registry entry changed; no new simulation
primitive (the eight previously audited rings come from the shared cache; only the two new rings are
measured); the D_040 ClassificationRegistry is untouched.

ResearchY-D_058 (Recovery Mechanism Audit, COMPLETE, tests 7/7 PASSED): why is RECOVERY not predicted
by the rank budget, the multiplicity distribution, the degeneracy count, λ₂ or the near-gap density —
while capacity is? **THE ANSWER IS A TYPE STATEMENT, NOT A FAILURE.** Capacity is a COUNT (ΔA = A₁ − A₀
is the number of new DISTINCT eigenvalues, an integer, and the rank budget bounds exactly it); recovery
is a MAGNITUDE (1 − ‖λ′−λ‖₂/‖λ‖₂ is a relative spectral displacement), and a bound on how MANY new
levels appear says nothing about HOW FAR they move. TEST (i): ρ(ΔA, relative shift) over 480 samples =
**0.046 — NEAR-ORTHOGONAL** (per family: delete 0.296, add 0.163, rewire −0.046, weight −0.251; at ring
level −0.595). The scale contrast is the point: mean ΔA moves 29.9 … 55.4 (capacity 0.42 … 1.00) while
the relative shift moves only 0.0246 … 0.0802 (recovery 0.9198 … 0.9754). TEST (ii): the IDENTICAL rank
ceiling gives ρ = **0.873 on capacity** and ρ = **0.409 on recovery** — the count law works for the count
and has no grip on the magnitude, because the count is invariant under a rescaling of the shift and the
shift is what recovery reads. **THE MAGNITUDE LAW** (derived the same way as the rank bound, from
first-order perturbation theory δλ_i = v_iᵀ δA v_i): **1 − recovery ≈ 0.82416·(‖δA‖_F/‖λ‖₂) − 0.002242**
— ONE coefficient calibrated on D_048/D_050's six source cases only, so the ring result is
OUT-OF-SAMPLE: ρ = **0.905**, RMSE 0.00928, mean |error| **0.00767**, the best of everything tried. Per
family the slopes are 1.104 (delete), 1.240 (add), 0.850 (rewire), 0.513 (weight) with R² 0.87 / 0.90 /
0.85 / 0.80 — structural rather than calibrated to one family. **AND THE SPECTRAL NORM IS EXACT, NOT
FITTED**: for a d-regular unweighted ring trace L² = dN(d+1), so ‖λ‖₂ = √(N·d·(d+1)) identically —
measured 122.38 = 122.38 (D96, d = 12), 43.82 = 43.82 (Pair1-47, d = 4), 63.50 = 63.50 (both degree-6
rings), 240.00 = 240.00 (D96-24, d = 24); the weighted rings deviate as expected (Decay96 50.90 vs
122.38; Boost96 432.22 vs 122.38). HEAD-TO-HEAD on recovery (the law's coefficient from the sources; the
correlational candidates by in-family LOO — the more generous protocol): MAGNITUDE LAW ρ = **0.905**,
mean |error| **0.00767**; ‖δA‖_F ALONE ρ = **−0.738** with error 6.25 (the DENOMINATOR is the content —
the perturbation's size matters only relative to the spectrum's scale); λ₂ ρ = 0.833, error 0.01106 —
the best of the FIVE NAMED candidates, because it is the crudest proxy for ‖λ‖₂; near-gap ρ = 0.412,
error 0.01151; the degeneracy count, max multiplicity and largest share ALL ρ = **0.024** with errors
0.0117 … 0.0121; the perturbation family as a per-ring average ρ = 0.000. **THE BLIND TEST — THREE
FROZEN PREDICTIONS, ALL CONFIRMED**, on two new rings carrying EXACTLY D96's offset set at weight 4.0 and
0.25, verified multiplicity-IDENTICAL to D96 (pattern 6×1, 5×1, 2×42, 1×1; A₀ = 45; headroom identical;
‖λ‖₂ ratios exactly 4.0000 and 0.2500): (1) capacity IDENTICAL across the three, spread **0.00033** on
0.9902 — the count mechanism is blind to a global weight scale; (2) recovery under the edge families
rises with scale, **0.96928 > 0.95771 > 0.83034**; (3) recovery under the weight family is
scale-invariant, spread **exactly 0.00000** on 0.99055 — the cleanest single result in the audit. **AND A
SHARPER FINDING THAN PREDICTED: delete recovery is ALSO exactly scale-invariant** (0.95515 × 3), so the
scale sensitivity of the edge families comes specifically from ADD and REWIRE, because those insert an
edge at FIXED UNIT WEIGHT while deletion removes weight IN PROPORTION. Measured add-family recovery:
0.99033 (4×) → 0.95655 (1×) → 0.75829 (1/4×). The magnitude law states this exactly: a proportional
removal leaves ‖δA‖_F/‖λ‖₂ unchanged, a fixed-weight insertion does not. DERIVED: the count/magnitude
type distinction; the magnitude law with one coefficient; the exact spectral norm √(N·d·(d+1)); and the
scale-sensitivity structure. EMERGENT: all three blind predictions confirmed (with delete-invariance
sharper than predicted); λ₂'s position as the best of the named candidates; and **THE NARROW-BAND
CAVEAT** — recovery spans only 0.0556 across these rings against capacity's 0.5791, a band **10.4×
narrower**, so every ρ is inflated and every error deflated and the law's mean |error| of 0.0077 is
**14 % of the entire recovery range**, not 1 %; the right reading is that the magnitude law captures the
ORDERING and explains the SCALE SENSITIVITY, not that recovery is predicted to within a percent.
REFUTED: recovery as a failed case of the count predictors (it is outside their type); any
multiplicity-distribution grip on recovery (ρ = 0.024); the perturbation norm WITHOUT the spectral scale
(ρ = −0.738 alone); the perturbation family as a ring-level determinant (ρ = 0.000 as a per-ring average
— the family effect is real but lives WITHIN a ring, restating D_053 on the other target); and the
near-gap density as a recovery predictor. SUMMARY: recovery belongs to a **DIFFERENT MECHANISM CLASS** —
a magnitude governed by the perturbation's norm relative to the spectral norm, exactly as capacity is a
count governed by the rank budget against the multiplicity structure; both mechanisms are now derived,
both exact in their structural part (the rank lemma; trace L² = dN(d+1)), and each carries one
coefficient. New open question for the D group: whether the magnitude law's single coefficient can be
replaced by a derived value — the residual dependence on the perturbation's projection geometry, not
just its norm — and whether the same count/magnitude split organises the other D-group observables
(spectral splitting, attractor shifts) that have so far been treated as one family. No canonical AT
claim, value, equation or registry entry changed; no new simulation primitive (the eight previously
audited rings come from the shared cache; only the two scale mirrors are measured); the D_040
ClassificationRegistry is untouched.

ResearchY-NP_002 (Highest-Value V2.2 Program, COMPLETE): what is the highest-value
V2.2 physics program? Verdict: MEASUREMENT ORIGIN — it scores 19/20 (impact 5,
feasibility 4, testability 5, derived-chain dependence 5), the highest of ten
candidates, and uses the derived origin chain MOST DIRECTLY: Difference
(distinguishability, D_039), magnitude (QG216), phase (QG220), complex state (D_036),
reciprocity (two-quadrature basis, D_037), and Actualization (measurement readout).
Completing the QM measurement chain (QG73 collapse + QG74 measurement + D_037 basis +
D_039 identity) has the highest physics impact, high falsifiability
(measurement-disturbance relations, no-cloning bounds), and high experimental access
(quantum information). RANKING (10 candidates scored on impact × feasibility ×
testability × derived-chain): T1 measurement origin 19 (HIGH), T2 information-encoded
measurement 18 (HIGH), T3 v/m_e reducibility 16, T4 neutrino hierarchy 15, T5 CMB
anisotropy spectrum 15, T6 baryon asymmetry 14, T7 gauge dynamics 13, T8 gravity-native
closure 12 (LOW — QG222 near-complete), T9 cosmology initial conditions 12, T10
dark-matter status 10 (LOW). ROADMAP: Phase 1 measurement/information (T1→T2), Phase 2
anchors/neutrinos (T3→T4, P2 data pending), Phase 3 cosmology/gauge (T5→T7), Phase 4
closure items (T8→T10). Recommended first project: MEASUREMENT ORIGIN (a new
M-series audit + xUnit suite + prediction registry entry). No new primitive; canonical
AT unchanged.

ResearchY-NP_003 (Manipulation Lever Audit, COMPLETE, tests 6/6 PASSED): does the
theory contain a controllable physical lever? Verdict: EXACTLY ONE — the phase θ₀ of a
complex state. It is LOCALLY variable (B): a measurement event pins it (M_002,
phase-pinning) and the pinned phase becomes the future initial condition (M_003: θ_t =
θ₀ + t·Δθ). The lever modifies TIME BEHAVIOUR (initial condition of the trajectory)
and MEASUREMENT (readout = pinned phase), but NOT frequency (Δθ = 2πk/N fixed per
mode), NOT gravity (no metric coupling), NOT sector structure (N, pairing, families
fixed). ALL other chain quantities are FIXED: primitives {Difference, η} and anchors
{v, m_e} are BOUNDARY inputs; N=96, the spectrum, ω₁, λ₂, pairing, and the tick
structure are DERIVED and unique (D_015/D_019). No globally variable (C) parameter
exists. SMALLEST manipulable object: the phase of a single state — one angular DOF, set
by one actualization event. LEVERAGE RANKING: phase (HIGH — time + measurement);
actualization rate/N/anchors/spectrum (none — fixed). Counterexamples: "change N" fails
(N unique), "change anchors" fails (BOUNDARY), "change tick rate" fails (time IS the
tick count), "change frequency by phase" fails (Δθ fixed per mode). Classification:
phase DOF DERIVED (D_036/D_039); manipulability (measurement pinning) EMERGENT
(M_001/M_002); fixed quantities DERIVED or BOUNDARY. No new primitive; canonical AT
unchanged.

ResearchY-NP_004 (Phase Coupling Audit, COMPLETE, tests 6/6 PASSED): can two systems
exchange or synchronize θ₀? Verdict: the phase is a TRUE PHYSICAL LEVER — it couples
systems through interference (I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B), so changing θ_A
changes B's interference) and through a shared actualization event (one event pins both
phases, M_002). But synchronization is ONLY possible for identical modes: θ_A(t)−θ_B(t)
= (θ_A0−θ_B0) + t·(Δθ_A−Δθ_B), time-invariant iff Δθ_A = Δθ_B (k_A = k_B); for unequal
modes the relative phase DRIFTS linearly — the derived chain contains no phase-locking
force. Smallest interaction for phase exchange: ONE shared actualization event reading
both quadratures of both systems (joint pinning). Sustained phase relations are always
COMMON-ORIGIN correlations (shared preparation or shared event); independent
actualization events give only preparation correlations, not synchronization.
Observable consequences: interference coherence requires a definite relative phase;
synchronized trajectories require identical modes; measurement correlations require a
joint readout. COUPLING HIERARCHY: interference coupling (DERIVED, always) → common-
origin correlation (DERIVED, sustained) → shared-event pinning (EMERGENT, phase
transfer) → synchronization (EMERGENT, identical modes only). Counterexamples: "two
unequal modes spontaneously synchronize" fails (drift); "independent events correlate
beyond preparation" fails (drift); "phase is an internal label" fails (interference
observes the relative phase). Classification: interference coupling DERIVED (complex
state D_036 + Born QG216); independent drift DERIVED (fixed Δθ, D_041); common-origin
correlation DERIVED (joint preparation); phase transfer via shared event EMERGENT
(joint readout); synchronization EMERGENT (setup condition, no mechanism). No new
primitive; canonical AT unchanged.

ResearchY-NP_005 (Missing Synchronization Mechanism Audit, COMPLETE, tests 7/7
PASSED): what is missing for spontaneous phase locking? Verdict: UNEQUAL-MODE
synchronization requires a cross-phase feedback term (a Kuramoto-type coupling) that
the canonical derived chain does not contain. The canonical update θ(t+1) = θ(t) + Δθ
(D_041) has only the self-rate, so for k_A ≠ k_B the relative phase drifts linearly —
no fixed point exists. Adding κ·sin(θ_B−θ_A) to A's update (and the symmetric term to
B's) gives dψ/dt = Δθ_A − Δθ_B − 2κ·sin(ψ), with a stable fixed point ψ* =
arcsin((Δθ_A−Δθ_B)/(2κ)) exactly when κ ≥ |Δθ_A−Δθ_B|/2 (= 0.5236 for k_A=16, k_B=32);
below threshold no locking occurs; at κ = 0 the relative phase drifts forever. EQUAL
modes (k_A = k_B) synchronize TRIVIALLY — the drift term vanishes. Determination: A)
synchronization impossible — YES for unequal modes in canonical AT (no locking force);
B) synchronization requires interaction — YES (the minimal addition is ONE cross-phase
feedback term, an energy-exchange mechanism); C) synchronization emergent from
existing actualization — PARTIAL (equal modes only). What prevents synchronization:
fixed Δθ (D_041), and the MISSING feedback/interaction/energy-exchange term (none
exists). Synchronization hierarchy: independent drift DERIVED → coupling DERIVED/
EMERGENT → equal-mode sync EMERGENT (trivial) → unequal-mode sync BOUNDARY (the locking
force is not in the chain). Observable consequences if it existed: coherence,
resonance amplification (locked phase → 2√(ρ_Aρ_B) maximal), collective modes
(in-phase/anti-phase), information transfer. Falsification: equal modes showing a
time-varying relative phase; or unequal modes synchronizing without added coupling.
Classification: canonical coupling DERIVED (interference); equal-mode sync EMERGENT
(trivial); the locking force BOUNDARY (a new input, no mechanism derives it). No new
primitive; canonical AT unchanged.

ResearchY-NP_006 (Phase-Locking Origin Audit, COMPLETE, tests 6/6 PASSED): does a
phase-locking term emerge from Actualization? Verdict: the locking term's FORM and
COEFFICIENT are DERIVED, but the MECHANISM does not emerge in canonical AT. The
interference intensity I = ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B) (Born rule QG216, complex
state D_036) has gradient ∂I/∂θ_A = +2√(ρ_Aρ_B)·sin(θ_B−θ_A) — EXACTLY the Kuramoto
form, with coefficient κ = 2√(ρ_Aρ_B) (the DERIVED Born cross-amplitude, not a free
parameter). BUT the canonical phase update θ(t+1) = θ(t) + Δθ (D_041) has only the
self-rate — it never follows the gradient, so the term is NOT present in the
evolution. Smallest modification: a variational requirement (phase advances along
∂I/∂θ_A): θ_A += Δθ_A + 2η√(ρ_Aρ_B)·sin(θ_B−θ_A), locking iff 2η√(ρ_Aρ_B) ≥
|Δθ_A−Δθ_B|/2 (κ = 0.866 ≥ 0.5236 for ρ=(0.25,0.75); weak amplitudes κ=0.02 do not
lock). Determination: A) derivable — PARTIAL (form+strength derivable; the
evolution-term needs a gradient-following update); B) emergent — CONDITIONAL (only
under a variational principle); C) external boundary — YES for the mechanism in
canonical AT. Necessity: the cross-phase term is NECESSARY for unequal-mode sync
(NP_005 threshold); its natural form IS the interference gradient. Counterexamples:
shared events pin once then drift (NP_004); reciprocity is the symmetric read
(D_037), not an evolution term; information exchange (M_005) does not change the phase
advance; κ is not free — it is fixed by the amplitudes. Observable consequences (if
active): coherence, resonance amplification, collective modes, synchronized
measurements. Classification: intensity DERIVED (D_036+QG216); gradient form DERIVED
(algebra of I); coefficient κ = 2√(ρ_Aρ_B) DERIVED (Born cross-amplitude);
gradient-following phase update EMERGENT (only under a variational requirement) /
BOUNDARY in canonical AT; canonical locking term ABSENT. Open question: whether the
phase advance should follow the count-intensity gradient (a variational/least-action
principle) — the single requirement that would make phase locking self-generated. No
new primitive; canonical AT unchanged.

ResearchY-NP_007 (Coupling Field Audit, COMPLETE, tests 7/7 PASSED): does
Actualization define a coupling field between distinguishable states? Verdict: YES as
a STATIC COUPLING NETWORK, NO as a propagating field. The interference cross-term I =
ρ_A+ρ_B+2√(ρ_Aρ_B)·cos(θ_A−θ_B) (Born QG216, complex state D_036/D_039) IS the LINK
between any two superposed states; its amplitude κ = 2√(ρ_Aρ_B) is the NETWORK LINK
WEIGHT — DERIVED from the state amplitudes (Born cross-amplitude), not an external
input. Test: A) local state updates only — PARTIAL (the evolution is local, self-rate
D_041); B) link-mediated influence — YES (interference links states observably); C)
field-like propagation — NO (the network is static; no propagating field in canonical
AT). HIDDEN interaction term: the interference cross-term is ALREADY present in the
actualization network — no external coupling field needed. NETWORK PROPERTIES: count/
information flow YES (M_005, DERIVED); phase flow NO (local evolution, NP_005);
reciprocity YES (D_037, EMERGENT); collective modes YES (in-phase I=(√ρ_A+√ρ_B)²=
1.866, anti-phase I=(√ρ_A−√ρ_B)²=0.134 for ρ=(0.25,0.75), DERIVED). κ CAN emerge from
the network structure — it IS the network weight. But the network does NOT synchronize
unequal modes (no phase flow). COUPLING HIERARCHY: isolated states (DERIVED) → link
(DERIVED) → network weight κ (DERIVED) → collective modes (DERIVED) → phase flow /
synchronization (ABSENT, BOUNDARY). FALSIFICATION: link claim falsified if two
superposed states show no interference cross-term; static-network claim falsified if
delayed/spatial (propagating) coupling is observed with no added structure.
Classification: link structure DERIVED (complex state + Born); link weight κ DERIVED
(Born cross-amplitude); count/information flow DERIVED (M_005); reciprocity EMERGENT
(D_037); collective modes DERIVED (superposition algebra); propagating coupling field
BOUNDARY (absent). No new primitive; canonical AT unchanged.

ResearchY-NP_008 (Interference Extremum Principle Audit, COMPLETE, tests 7/7 PASSED):
does Actualization extremize the interference functional I? Verdict: CANONICAL
Actualization extremizes NOTHING (option D) — it follows the fixed self-rate update
θ(t+1)=θ(t)+Δθ (D_041), sweeping the relative phase through the full circle, so I
changes non-monotonically (verified: rel₀=0.5, I: 1.760 → 1.740 → 0.980) — no
increase, no decrease, no conservation. The functional I = ρ_A+ρ_B+2√(ρ_Aρ_B)·
cos(θ_A−θ_B) has ∂I/∂θ_A = +2√(ρ_Aρ_B)·sin(θ_B−θ_A) = κ·sin(θ_B−θ_A); extrema:
in-phase MAX at rel=0 (I=(√ρ_A+√ρ_B)²=1.866), anti-phase MIN at rel=π
(I=(√ρ_A−√ρ_B)²=0.134); ∂I/∂θ vanishes at both; π/2 is NOT an extremum (∂I/∂θ=−0.866
≠ 0). HIDDEN VARIATIONAL PRINCIPLE: the gradient of I IS the missing synchronization
term (NP_005/NP_006) — a variational phase update θ(t+1)=θ(t)+η·∂I/∂θ would drive the
relative phase to an extremum of I (d rel/dt = −2ηκ·sin(rel), fixed points at rel=0,π),
making synchronization explicit, coherence sustained, and collective modes stable.
Determination: A) max(I) NO; B) min(I) NO; C) stationary(I) NO; D) none — YES
(canonical evolution extremizes nothing). Necessity: ∂I/∂θ_A is necessary for
unequal-mode sync (NP_005 threshold κ ≥ |Δθ_A−Δθ_B|/2 = 0.5236; here κ=0.866 → locks);
the extremum principle is the cleanest statement of that necessity. Counterexamples:
'actualization maximizes I' fails (I drifts 1.760→0.980); 'actualization conserves I'
fails (I changes); 'π/2 is the stationary point' fails (∂I/∂θ ≠ 0); 'extremum
principle exists canonically' fails (self-rate only). Falsification: canonical
no-extremum claim falsified if a two-mode system's relative phase SEEKS an extremum of
I with no added interaction; gradient-is-locking claim falsified if the phase advance
does not follow ∂I/∂θ. Classification: functional I DERIVED (complex state D_036 +
Born QG216); extrema DERIVED (algebra of I); gradient ∂I/∂θ DERIVED; canonical drift
DERIVED (fixed Δθ); extremum principle EMERGENT (under variational requirement) /
BOUNDARY in canonical AT. No new primitive; canonical AT unchanged.

ResearchY-NP_009 (Variational Actualization Audit, COMPLETE, tests 6/6 PASSED): does
Actualization obey a hidden extremum principle? Verdict: NO (option D) — canonical
Actualization IGNORES the interference functional I: it neither increases, nor
decreases, nor conserves it (verified: I drifts 1.760 → 0.260 non-monotonically over 4
ticks for k=(16,32), rel₀=0.5). The update θ(t+1)=θ(t)+Δθ (D_041) contains no
reference to ρ or the relative phase. HIDDEN OBJECTIVE SEARCH: count (Σρ=1 conserved,
M_005, not extremized); information (log₂ 95 = 6.57 static, M_004); distinguishability
(95-state structure static, D_039); interference I (an observable, never fed back). All
ruled out — no objective function exists in the canonical update. Determination: A)
max(I) NO; B) min(I) NO; C) stationary(I) NO; D) no extremum principle — YES. SMALLEST
MODIFICATION to obtain one: add ONE gradient-following phase term θ(t+1) = θ(t) + Δθ +
η·∂I/∂θ — a gradient flow on I with d rel/dt = −2ηκ·sin(rel), κ = 2√(ρ_Aρ_B), whose
stable fixed point is rel=0 (the in-phase MAXIMUM, I = 1.866; the anti-phase rel=π is
unstable). THEREFORE: if Actualization had an extremum principle it would be max(I) —
and with it synchronization, coherence, and stable collective modes would EMERGE
(gradient update locks rel at the max; κ = 0.866 ≥ 0.5236 threshold). Falsification:
no-extremum claim falsified if a two-mode relative phase converges to an extremum of I
with no added interaction; max(I) emergence falsified if a gradient-following update
does not converge to rel=0. Classification: canonical update DERIVED (fixed Δθ,
D_041); functional I + gradient DERIVED (complex state D_036 + Born QG216);
no-extremum property DERIVED (self-rate dynamics ignores I); variational (gradient)
actualization EMERGENT (under the added requirement) / BOUNDARY in canonical AT. No new
primitive; canonical AT unchanged.

ResearchY-NP_010 (Second Network Layer Audit, COMPLETE, tests 6/6 PASSED): does a
second coupling network exist above Actualization? Verdict: synchronization requires a
SECOND network layer (the phase-flow/gradient layer) above the primary actualization
chain — structurally present, dynamically absent in canonical AT. NETWORK 1
(Actualization): local self-rate update θ(t+1)=θ(t)+Δθ (D_041); no phase flow; unequal
modes drift — cannot synchronize (NP_005). κ IS A LINK PROPERTY (B): κ = 2√(ρ_Aρ_B)
depends on BOTH endpoints (verified: κ(0.25,0.75)=0.866; 2√ρ_A alone ≠ it) and is
symmetric (κ(A,B)=κ(B,A)); NOT a state property (A) and NOT a propagating field value
(C — it is a static link weight, NP_007). HIDDEN NETWORK SEARCH: reciprocity (D_037)
is the symmetric read basis — no phase flow; interference provides static links — no
flow; information flow (M_005) redistributes counts — not phases; shared actualization
events (M_002) pin once — drift resumes. NETWORK 2 (phase-flow layer): STRUCTURALLY
present (the interference link weights are derived), DYNAMICALLY absent (no canonical
mechanism carries phase flow) → BOUNDARY in canonical AT. Does synchronization emerge
only with a second level? YES — Network 1 alone leaves unequal modes drifting; the
gradient term η·∂I/∂θ (Network 2) locks rel → 0 (max I = 1.866; κ = 0.866 ≥ 0.5236
threshold) and makes collective modes stable. Trace: Difference → Actualization →
Network 1 (local, DERIVED); Difference → Reciprocity → interference links (κ,
DERIVED) → Network 2 (phase-flow, structurally present/dynamically absent).
Falsification: Network-1-unsynchronized claim falsified if unequal modes synchronize
on the pure self-rate update; κ-link-property falsified if the coefficient depends on
one state or a propagating field; second-layer necessity falsified if synchronization
is observed with only static weights. Classification: Network 1 DERIVED (fixed Δθ);
interference link weights DERIVED (Born cross-amplitude); κ as link property DERIVED;
Network 2 EMERGENT (under variational requirement) / BOUNDARY in canonical AT. No new
primitive; canonical AT unchanged.

ResearchY-NP_011 (Hidden Coupling Field Audit, COMPLETE, tests 7/7 PASSED): is
Network 2 a genuine physical field? Verdict: REFUTED — Network 2 (the Born-derived
coupling network κ = 2√(ρ_Aρ_B)) is a MATHEMATICAL STRUCTURE, not a physical field. It
fails all five field criteria: A) state-independent existence — NO (κ = 2√(ρ_Aρ_B) = 0
whenever either state has zero amplitude; verified κ(0.25,0)=0); B) stored structure —
NO (no field variables, only derived link weights); C) information transport — NO
(information is redistributed by MEASUREMENT, M_005, not by the network); D) phase
transport — NO (no canonical phase flow, NP_005/NP_010); E) energy transport — NO
(count conserved Σρ=1, not transported). κ IS MERELY DESCRIPTIVE: the canonical update
θ(t+1)=θ(t)+Δθ (D_041) contains no reference to κ — the link weight never exerts
influence (verified: canonical evolution is κ-independent). κ would act only under the
variational requirement θ += η·∂I/∂θ (NP_009), which is absent. NO UNIQUE OBSERVABLE:
every observable of Network 2 (interference, collective modes in-phase 1.866/anti-phase
0.134, phase correlations) is already produced by the state structure alone (complex
state D_036 + Born QG216) — no observable requires the network's independent existence.
FIELD HIERARCHY: states (physical, ontic) → Network 1 (physical, acting) → Network 2
(NOT physical — a derived relation). If it WERE a field, candidate field variables: the
link weights κ and the relative phases θ_A−θ_B. Falsification: mathematical-structure
claim falsified if an observable requires Network 2's independent existence;
descriptive-κ claim falsified if phase evolution is observed to depend on κ with no
variational dynamics. Classification: network structure + link weights DERIVED (complex
state + Born); collective modes/interference DERIVED (superposition algebra);
information redistribution DERIVED (M_005); Network 2 as physical field REFUTED —
mathematical structure; a physical coupling field BOUNDARY (absent; would need κ-active
dynamics). No new primitive; canonical AT unchanged.

ResearchY-NP_012 (Unique Prediction Search, COMPLETE, tests 6/6 PASSED): what
observable prediction survives after all QM-equivalent interpretations are removed?
Verdict: the measurement and coupling programs contribute NO observationally-testable
uniquely-AT prediction. Every M-series observable is QM-equivalent (CORRESPONDENCE):
event, pinning, feedback, log₂(95) bound (AT-P043 DOWNGRADED in M_009), conservation,
observer — all reproduced by standard QM (projection, unitarity, psi-epistemic). AT-P042
(discrete tick) is the SOLE structurally-unique measurement prediction, but
observationally equivalent to continuous QM at every tick-sampled time (M_010) — its
discriminator is sub-tick, in-principle only. The NP program (coupling,
synchronization, network, field, extremum) yields negative results: coupling not
physical (NP_011), sync absent (NP_005), no hidden field. THE SURVIVING UNIQUELY-AT
PREDICTIONS are the N=96 SPECTRAL values: ω₁ = √91·(2π/N) ≈ 0.624 (a fundamental
frequency QM cannot predict — THE FIRST uniquely-AT prediction), families =
floor(log₂ span)+1 = 3 (a family count QM leaves free, observed), O(2)-type doublet
(gauge structure QM leaves free), v = 137·ln(span) = 254.37 GeV (EW-scale structure).
RANKING (impact+uniqueness+feasibility+testability /20): ω₁ 16, families 16, O(2)
doublet 13, v-structure 13, AT-P042 11. FALSIFICATION: ω₁ falsified if the measured
fundamental excitation ≠ √91·(2π/N)·scale; families falsified by a 4th family; O(2) by
a triplet with no mirror pair; v by a deviating scale relation; AT-P042 by a continuous
sub-tick phase. CLASSIFICATION: measurement observables CORRESPONDENCE (QM-equivalent);
AT-P042 PREDICTION (structural); ω₁, family count, O(2) doublet, v-structure PREDICTION
(uniquely-AT); hidden field/sync/network-2 BOUNDARY (absent). The unique predictions of
the theory live in the N=96 SPECTRUM, not the measurement program. No new primitive;
canonical AT unchanged.

ResearchY-NP_013 (Unique Spectral Prediction Audit, COMPLETE, tests 6/6 PASSED): what
observable follows uniquely from ω₁, span, family count, and O(2) doublets — and cannot
be reproduced by QM/SM/GR? Verdict: the strongest D96-specific prediction is the O(2)
EXACT DOUBLET DEGENERACY (λ_k = λ_{N−k} for every mode k, D_021) — a structural,
falsifiable claim absent from QM (fixes no spectrum), GR (fixes no frequencies), and SM
(inputs, does not derive, v and the family count). SPECTRAL QUANTITIES (N=96): ω₁ =
√91·(2π/N) ≈ 0.6244 (verified ω₁·N/(2π) = 9.50 vs √91 = 9.54, the √91 K=6 factor);
λ₂ = 2−2cos(4π/96) ≈ 0.0171; span = 6.4025 (algebraic π-analogue, D_028/D_042);
family count = floor(log₂ span)+1 = 3 (D_046 P8); v = 137·ln(span) = 254.37 GeV
(D_046 P6). TOP-5 RANKING (uniqueness × impact × feasibility): O(2) doublets 13 and
family count 13 (tied top); ω₁ 12; v-structure 11; span 10. FALSIFICATION: O(2)
doublets falsified by a measured spectrum with a mode lacking its exact mirror partner;
ω₁ by a fundamental ≠ √91·(2π/N)·scale; families by a 4th family; v by a deviating
scale relation; span by an inconsistent max/min ratio. NONE of the five is implied by
QM/GR/SM. Classification: all five PREDICTION (uniquely D96); O(2) doublet degeneracy
and family count are the strongest (structural + observed-consistent). No new
primitive; canonical AT unchanged.

ResearchY-NP_014 (Necessity of Synchronization Audit, COMPLETE, tests 7/7 PASSED):
does physics require synchronization at all? Verdict: OPTIONAL (B) — no canonical law
requires it; the canonical absence is a FEATURE preserving the full relative-phase
information channel. Compare U1 (canonical AT, no locking: θ(t+1)=θ(t)+Δθ, D_041)
with U2 (modified AT, gradient locking: θ(t+1)=θ(t)+Δθ+η·∂I/∂θ). EVERY canonical law
survives in BOTH: measurement reads both quadratures (M_002), information conserved
(Σρ=1 and log₂ 95, M_004/M_005), reciprocity (D_037), 95-state distinguishability
(D_039), complex-state identity (D_036) — identical in both universes. THE ONLY
DIFFERENCE is the relative-phase diversity: U1's relative phase drifts and explores a
CONTINUUM of values (I ranges 0.134–1.866; 6 distinct relative phases per cycle for
k=(16,32)), while U2's locks at rel=0 and reduces the relative phases to ONE value.
Does synchronization improve physics? NO — it adds no canonical law and fixes no
contradiction. Does it destroy physics? PARTIALLY — it collapses the relative-phase
information channel. COLLECTIVE modes: exist in U1 (transient); U2 only makes them
stable. COHERENCE: U1 has time-varying real interference; U2 sustains it. INFORMATION:
log₂ 95 conserved in both — unchanged. STATE DIVERSITY: U2 LOWER (one relative phase
vs continuum). Determination: A) synchronization required — NO; B) synchronization
optional — YES; C) forbidden — PARTIAL (canonical chain lacks it; enabling reduces
phase diversity). Necessity proof: every canonical observable is fully defined and
conserved in U1 — no law breaks without locking; enabling locking REMOVES a capability
(relative-phase diversity), so sync is positively costly in information terms.
Counterexamples: 'sync needed for coherence' fails (U1 has real time-varying
interference); 'sync needed for measurement' fails (M_002 identical); 'sync needed for
information' fails (log₂ 95 conserved in both); 'sync needed for collective modes'
fails (they exist in U1, transient). Falsification: optionality falsified if a
canonical observable requires synchronized relative phases; feature claim falsified if
locking increases state diversity (it does not). Classification: canonical no-locking
dynamics DERIVED (self-rate, D_041) — a feature preserving phase diversity;
synchronization as a regime EMERGENT (would require variational principle,
NP_006/NP_009); synchronization required REFUTED (optional — B); canonical absence
DERIVED. No new primitive; canonical AT unchanged.

ResearchY-NP_015 (O(2) Doublet Prediction Audit, COMPLETE, tests 5/5 PASSED): what
observable consequences follow from the exact O(2) doublet degeneracy? Verdict: the
O(2) exact doublet degeneracy (λ_k = λ_{N−k} for every mode k, D_021) predicts an
OBSERVABLE spectral signature: every non-central mode has an exact mirror partner at
identical frequency (ω_k = ω_{N−k}, ω_k/ω_{N−k} = 1 exactly), giving 47 mirror pairs
plus the central mode k=48 (verified: |Δλ| = 0 to machine precision for k=1,2,16,47).
The degeneracy is EXACT, not approximate: any |Δλ| > 0 between a claimed pair
FALSIFIES the prediction, as does a mode with no mirror partner or a triplet structure
(which would indicate SU(3)-type, not O(2)). DISTINCT FROM QM (which fixes no spectrum
— degeneracies are Hamiltonian-dependent, often accidental), SM (whose weak-isospin
doublets (u,d),(c,s),(t,b) are NON-degenerate gauge pairs, NOT degeneracies), and GR
(which has no frequencies). TOP OBSERVABLE SIGNATURES: (1) exact mirror-pair
frequencies (ω_k/ω_{N−k}=1); (2) the 47+1 doublet count; (3) the O(2) reflection
symmetry k → N−k. PREDICTION REGISTRY ENTRIES: the O(2) exact spectral degeneracy
(extends D_046 P1); ω_k/ω_{N−k}=1 exactly; 47 mirror pairs + central mode k=48.
Classification: O(2) exact doublet degeneracy PREDICTION (uniquely D96, structural);
SM weak doublets CORRESPONDENCE only approximately (gauge pairs, not degeneracies);
O(2) reflection symmetry DERIVED (algebra of cos); central mode k=48 DERIVED
(self-pairing). No new primitive; canonical AT unchanged.

ResearchY-NP_016 (Mirror-Pair Observation Audit, COMPLETE, tests 7/7 PASSED): do
natural spectra exhibit O(2) mirror-pair degeneracy? Verdict: the O(2) mirror-pair
degeneracy (λ_k = λ_{N−k}, NP_015) is NATIVE to the D96 ring modes themselves — the
observable domain is any physical system realizing the C96 ring algebra. Verified:
ω_1 = ω_95 = 0.065438, ω_16 = ω_80 = 1.000000, ω_k = ω_{N−k} for every k ≠ 48
(|Δλ| = 0 exactly, from cos(2πk/N) = cos(2π(N−k)/N)). TARGET RANKING: (1) ring
resonance spectrum — HIGH (exact mirror pairs native to the mode algebra); (2)
cosmological acoustic spectrum — MEDIUM (octave-hierarchy peak RATIOS, D96-derived —
ℓ₁=220.48, r₂₁=2.4368 — not per-mode pairs); (3) gravitational-wave spectrum — LOW
(ringdown modes are DAMPED, complex-frequency, no exact degeneracy); (4) particle (SM)
spectrum — LOW (weak doublets (u,d),(c,s),(t,b) are non-degenerate split-mass pairs);
(5) neutrino spectrum — LOW (mass ordering unresolved, no degeneracy observed).
EXPECTED DEVIATION IF AT IS FALSE: no exact mirror pairs (|Δλ| > 0 or unpaired modes),
no 47+1 structure, no k → N−k reflection symmetry, no ring-algebra frequency ratios.
FALSIFICATION STRATEGY: measure a ring-mode spectrum; require |Δλ| = 0 for every
claimed pair, 47+1 count, and reflection symmetry — any deviation falsifies.
Classification: mirror-pair degeneracy PREDICTION (uniquely D96, native to the ring
modes); cosmological acoustic ratios CORRESPONDENCE (D96-derived peak ratios, not
per-mode pairs); SM/GW/neutrino expectations CORRESPONDENCE (no exact degeneracy
predicted there). No new primitive; canonical AT unchanged.

ResearchY-NP_017 (Natural D96 Signature Search, COMPLETE, tests 6/6 PASSED): can
D96-type spectral structure appear naturally in real systems? Verdict: nature contains
an APPROXIMATE D96 signature — the CMB acoustic peak ratios follow the D96 octave
hierarchy (ℓ₁ = 220.48, 0.008%; r₂₁ = 2.4368, 0.035%; r₃₁ = 3.6965, 0.058%; n_s =
0.96497, 0.007% — QG237/QG238, a DERIVED correspondence, not accidental) — but NO
natural realization of the exact O(2) mirror-pair degeneracy (|Δλ| = 0, NP_015).
Domain tests: atomic (Rydberg E_n ~ 1/n²) — no octave, no mirror pairs; molecular
(rovibrational) — no; condensed matter (phonons ~ linear) — approximate only; plasma
(collective) — no; gravitational-wave (ringdown) — damped complex modes, no. CANDIDATE
RANKING: (1) CMB acoustic peaks — STRONG (D96-derived, <0.06%); (2) cosmological
general — MEDIUM (D96 octave structure); (3) condensed-matter phonons — WEAK
(approximate); (4) atomic/molecular — none; (5) plasma/GW — none. DEVIATION AUDIT: CMB
< 0.06%; condensed matter ~15% (not D96-specific); other domains no match.
FALSIFICATION STRATEGY: verify CMB peak ratios against the D96 octave hierarchy; search
new ring-mode spectral data for |Δλ| = 0; any CMB deviation beyond tolerance falsifies
the correspondence. Classification: CMB acoustic peak ratios CORRESPONDENCE (D96-
derived, QG237/238); exact mirror-pair degeneracy PREDICTION (no natural observation
yet); octave hierarchy in nature CORRESPONDENCE (CMB); atomic/molecular/plasma/GW
CORRESPONDENCE (no D96 structure expected); NO domain FALSIFIED. No new primitive;
canonical AT unchanged.

ResearchY-NP_018 (Distinguishability Observable Audit, COMPLETE, tests 7/7 PASSED):
does distinguishability itself generate an observable physical quantity? Verdict: YES —
distinguishability generates DIRECTLY OBSERVABLE quantities. The state space has 95
distinct states (D_039); the entropy is H = log₂(95) = 6.57 bits (M_004); the
Born-weighted information density is I_occ = 0.7513 nats (QG228); and the cosmological
fraction is written DIRECTLY from it: ΩΛ = I_occ/ln K = 0.7513/1.0986 = 0.6839 (QG234)
— MEASURED to 0.12%, with Ωm = 1−ΩΛ = 0.3161 (0.26%). OBSERVABLE CANDIDATES: the state
count (95 — structural, D_039); the entropy (log₂ 95 = 6.57 bits — DERIVED, M_004);
the information density (I_occ = 0.7513 nats — DERIVED, QG228); and the STRONGEST —
the cosmological fraction ΩΛ = 0.6839 (a PREDICTION written directly from the
distinguishability-derived information, OBSERVED to 0.12%). QM/SM/GR produce no
fundamental observable written as a function of distinguishability: QM has no predicted
state-count (entropy is derived from a given Hilbert space), SM/GR have no
distinguishability origin. COHERENCE is only CORRESPONDENCE (depends on the relative
phase, not the state count). FALSIFICATION: a measured ΩΛ deviating from I_occ/ln K
beyond 0.12% falsifies the distinguishability→information→cosmology chain; a state
space of size ≠ 95 falsifies H = log₂(95); an I_occ inconsistent with 0.7513 falsifies
the density. Classification: state count and entropy DERIVED (D_039/M_004);
information density DERIVED (QG228); ΩΛ PREDICTION (distinguishability-derived,
observed); coherence CORRESPONDENCE. The dark-energy fraction is the theory's most
direct distinguishability observable. No new primitive; canonical AT unchanged.

ResearchY-NP_019 (Information Cosmology Audit, COMPLETE, tests 5/5 PASSED): does
distinguishability-derived information predict additional cosmological observables
beyond ΩΛ? Verdict: information predicts EXACTLY the density-fraction pair and their
ratio — no additional direct observables. Testing the cosmological observables (ΩΛ,
Ωm, H₀, σ₈, BAO scales, structure growth, CMB quantities n_s/ℓ₁): only ΩΛ =
I_occ/ln K = 0.6839 (by construction, QG234) and its complement Ωm = (ln K − I_occ)/
ln K = 0.3161 are DIRECT functions of the information density; the derived ratio
ΩΛ/Ωm = I_occ/(ln K − I_occ) = 2.1636 follows (verified: ln K = I_occ/0.6839 = 1.0986,
K ≈ 3). n_s = 0.96497 (QG237) and ℓ₁ = 220.48 (QG238) are D96-SPECTRAL quantities,
NOT I_occ functions (they come from the octave hierarchy, not the information density).
H₀ (a calibration), σ₈, BAO scales, and structure growth have NO direct information
relation. RANKING: (1) ΩΛ — OBSERVED 0.12%; (2) Ωm — OBSERVED 0.26%; (3) ΩΛ/Ωm ratio
2.1636 — derived; (4) n_s — D96-spectral correspondence; (5) ℓ₁ — D96-octave
correspondence. FALSIFICATION: ΩΛ deviating from I_occ/ln K beyond 0.12%; Ωm
inconsistent with 1−I_occ/ln K; the ratio deviating from I_occ/(ln K−I_occ).
Classification: ΩΛ and Ωm PREDICTION (information-derived, observed); ΩΛ/Ωm ratio
PREDICTION (derived); n_s/ℓ₁ CORRESPONDENCE (D96-derived, not information);
H₀/σ₈/BAO/growth BOUNDARY or calibration (no direct relation). I_occ is a genuine but
NARROW cosmological variable — it fixes the density fractions and nothing else
directly. No new primitive; canonical AT unchanged.

ResearchY-NP_020 (Black Hole Information Audit, COMPLETE, tests 7/7 PASSED): does the
Difference → Information chain change black-hole information physics? Verdict: a black
hole CANNOT eliminate Difference — information is conserved through horizon formation
(M_005). Information is DERIVED from distinguishability (D_039: 95 distinct states
pre-exist) and CONSERVED through actualization (M_005: measurement reveals and
redistributes, never creates or destroys). The conserved quantities — count (Σρ = 1,
Born QG216), positivity (ρ ≥ 0), normalization, and state identity (the 95 states
remain distinct) — all survive horizon crossing; none is removed by the geometric
boundary. An event horizon removes ACCESS (external observability) but not
DISTINGUISHABILITY (D_039 is a state-space property, not geometric). INFORMATION
FATES: A) destroyed — NO (conservation, M_005); B) hidden — YES (external
inaccessibility); C) redistributed — YES (radiation/measurement re-encoding); D)
preserved — YES (H_before = H_after = log₂(95) = H_hidden + H_observer). REQUIRED
MECHANISM: HORIZON BOOKKEEPING — storage (states retain distinguishability),
redistribution (re-encoding in the external system), encoding (new hidden/accessible
partition), bookkeeping (conservation across the horizon). AT resolves the information
paradox in the UNITARITY-CONSERVING direction and grounds it in the primitives.
COMPARISON: GR — information can classically disappear; QM — unitarity implies
conservation but the mechanism is debated; black-hole thermodynamics — entropy ~
area/4 (Bekenstein-Hawking); AT — conserved through actualization, the horizon only
repartitions. FALSIFICATION: information NON-conservation across horizon formation (a
measurable H_before ≠ H_after). Classification: information conservation DERIVED
(M_005); distinguishability primitive-invariance DERIVED (D_039); horizon as a
partition (not an annihilator) EMERGENT; horizon bookkeeping PREDICTION (the
conservation mechanism); Bekenstein-Hawking entropy-area relation BOUNDARY (not
derived here). No new primitive; canonical AT unchanged.

ResearchY-NP_021 (Information Horizon Audit, COMPLETE, tests 5/5 PASSED): if
information is conserved, where is it stored across a horizon? Verdict: HORIZON
BOOKKEEPING — storage + redistribution + encoding; state-space expansion is REFUTED.
BEFORE the horizon: the total information H = log₂(95) = 6.57 bits lives in the
95-state distinguishability (D_039) — a property of the states, not the geometry;
count Σρ = 1 conserved (Born QG216). AFTER the horizon: the same information is
PARTITIONED — H = H_hidden + H_observer, the hidden part stored in the distinct states
behind the horizon, the observer part re-encoded in the external radiation (M_005); the
state space is still 95 (distinguishability intact). MECHANISM TEST: storage — YES
(in-falling states retain distinguishability, D_039); redistribution — YES (external
radiation re-encodes over time, M_005); encoding — YES (the horizon defines a new
hidden/accessible partition); state-space expansion — NO (the state space is the
PRIMITIVE distinguishability count, FIXED at 95 — it cannot grow to absorb the
information). WHICH PRESERVES DISTINGUISHABILITY: storage + redistribution + encoding —
the total log₂(95) = H_hidden + H_observer is conserved; only the partition changes.
FALSIFICATION: an accessible information content inconsistent with the partition (total
≠ log₂ 95); a measured state count ≠ 95; a mechanism that merges the hidden states; a
horizon with no re-encoding channel. Classification: information balance DERIVED
(M_005); storage in distinct states DERIVED (D_039); redistribution into the external
system EMERGENT (radiation/measurement channel); encoding (hidden/accessible partition)
EMERGENT; horizon bookkeeping as a whole PREDICTION; state-space expansion REFUTED
(state space fixed at 95). No new primitive; canonical AT unchanged.

ResearchY-NP_022 (Unique Physics Prediction Search, COMPLETE, tests 7/7 PASSED): what
observable phenomenon would AT expect that standard QM and GR would NOT expect?
Verdict: the strongest genuinely unique AT prediction is ΩΛ = I_occ/ln K = 0.6839 —
already observed to 0.12%, uniquely tied to distinguishability (QM/GR have no
observable as a function of distinguishability), and unambiguously falsifiable (any
deviation beyond 0.12%). The strongest structural prediction is the O(2) exact
mirror-pair degeneracy. INVENTORY (every surviving non-QM/non-GR claim): the
measurement program (M_001–M_010) — measurement=event (B), pinning (B), feedback
(B), log₂95 (A, QM-standard), conservation (A), observer (B), AT-P043 DOWNGRADED
(A), AT-P042 discrete lattice (D-structure, sub-tick); the coupling program
(NP_003–NP_021) — coupling network not physical (B), sync absent (negative), no
hidden field (negative), O(2) exact doublets (D), mirror pairs (D), ΩΛ (D), Ωm (D),
BH info (D-direction); the geometry-bridge program (QG_001–QG_016) — ρ bridge (C
structural), measure-preserving metric (C), finite observability (C), finite events
(C), tick discreteness (B boundary), q₀ (D-value), z_acc (D-value), 3-family anchored
by ΩΛ (C); the spectral program (D_046) — ω₁ = √91·(2π/N) (D), families = 3
(D-value), v = 137·ln span (C hosted), span (C). THE A/B/C/D FILTER: A) implied by
QM — the measurement chain (M_007/M_009, AT-P043 DOWNGRADED); B) implied by GR —
v = 137·ln span (hosted unit), BH information (unitarity direction), q₀/z_acc FORM
(hosted FRW); C) reconstruction only — the geometry bridge, finite observability,
tick discreteness; D) genuinely new — ΩΛ, Ωm, O(2) mirror pairs, ω₁, families,
q₀/z_acc VALUES, AT-P042. EXPLICIT EXPERIMENT/OBSERVATION/FALSIFICATION for each
D-candidate: ΩΛ = I_occ/ln K = 0.6839 — Planck/CMB cosmology, OBSERVED 0.12%,
falsified by deviation beyond 0.12%; O(2) mirror pairs — C96-ring resonance spectrum,
|Δλ| = 0 (ω₁ = ω₉₅ = 0.065438, ω₁₆ = ω₈₀ = 1.000000), falsified by any |Δλ| > 0,
missing pair, or triplet; ω₁ = √91·(2π/N) = 0.6244 — fundamental ring resonance
(ω₁·N/(2π) = 9.5394 ≈ √91), falsified by a fundamental ≠ √91·(2π/N)·scale; q₀ =
−0.5258 / z_acc = 0.6295 — supernova Hubble diagram/expansion history, falsified by
deviation from −0.526/0.630; families = 3 — particle-physics family searches,
falsified by a 4th family; AT-P042 — sub-tick phase resolution (in-principle, M_010),
falsified by a continuous sub-tick phase inconsistent with the lattice. RANKING
(uniqueness × observability × falsifiability × impact): (1) ΩΛ = 0.6839 — 20/20
(observed, unique, falsifiable, max impact); (2) O(2) mirror pairs — 18/20 (exact,
structural, unique); (3) ω₁ — 17/20; (4) q₀/z_acc — 16/20; (5) families = 3 — 16/20;
(6) Ωm — 15/20; (7) AT-P042 — 13/20 (sub-tick); (8) BH info — 13/20 (direction);
(9) v = 137·ln span — 12/20 (hosted); (10) finite observability — 10/20. STRONGEST
SINGLE PREDICTION: ΩΛ = I_occ/ln K = 0.6839 — the ONLY candidate scoring 20/20;
uniquely AT, already observed, sharply falsifiable, maximal impact. V2.3
RECOMMENDATION: harden the information-cosmology closures (q₀, z_acc precision) plus
a ring-mode search for O(2) mirror pairs. Counterexamples: 'the measurement chain
gives a unique prediction' fails (M_009/M_010: QM-equivalent except AT-P042
sub-tick); 'the coupling network is unique' fails (NP_011: not physical); 'BH
information is unique' fails (unitarity already conserves); 'v = 137·ln span is
unique' fails (hosted GeV unit); 'families = 3 is the strongest' fails (a known fact
— reconstruction; ΩΛ is an independently-derived OBSERVED relation); 'AT-P042 is the
strongest' fails (observationally in-principle-only). Falsification: ΩΛ deviation
beyond 0.12%; any O(2) |Δλ| > 0; ω₁ ≠ √91·(2π/N)·scale; q₀ ≠ −0.526; z_acc ≠ 0.630;
a 4th family; a continuous sub-tick phase; H_before ≠ H_after across a horizon.
Classification: ΩΛ/Ωm PREDICTION (uniquely-AT, observed); O(2) mirror pairs
PREDICTION (uniquely-AT, structural, unobserved); ω₁/families PREDICTION (uniquely-
AT, structural); q₀/z_acc values DERIVED (hosted form); AT-P042 PREDICTION
(structural, in-principle); BH information CORRESPONDENCE-direction (unitarity-
consistent); v = 137·ln span CORRESPONDENCE (hosted structure); finite observability
BOUNDARY-supported (structural). No new primitive; canonical AT unchanged.

ResearchY-QG_001 (Information–Geometry Bridge Audit, COMPLETE, tests 6/6 PASSED): how
does distinguishability become geometry? Verdict: both geometry and information EMERGE
from the SAME deeper object — the count density ρ generated by Actualization (option
C). The metric is g = ρ^(2/d)η (QG197, with native dynamics ∂_t g = (2/d)(∂_t ρ/ρ)·g,
QG222); the information is I = KL(ρ‖uniform) = I_occ = 0.7513 nats (QG228); the
cosmological fraction follows ΩΛ = I_occ/ln K = 0.6839 (QG234). Trace: Chain 1
(information) Difference → Distinguishability (D_039) → Information (KL(ρ‖uniform));
Chain 2 (geometry) Difference → Actualization → ρ → g = ρ^(2/d)η → Einstein tensor —
BOTH pass through ρ. Determination: A) information generates geometry — NO (KL is a
derived property of ρ); B) geometry generates information — NO (the metric is a
derived property of ρ); C) both emerge from ρ — YES (the bridge); D) no bridge — NO.
Tests: black holes (horizon from g; information conserved across it, NP_020/021);
ΩΛ/Ωm (from ρ's KL divergence: ΩΛ=0.6839, Ωm=0.3161); measurement (resolves ρ; reveals
pre-existing distinguishability, M_005). Counterexamples: 'information generates
geometry' fails (KL is a property OF ρ); 'geometry generates information' fails (the
metric is a function of ρ); 'no bridge' fails (both chains provably pass through ρ).
Falsification: geometry deviation from g = ρ^(2/d)η with the same ρ; information ≠
KL(ρ‖uniform); a geometry/information source independent of ρ. Classification: count
density ρ DERIVED (actualization count density); metric g = ρ^(2/d)η DERIVED (QG197);
information I = KL(ρ‖uniform) DERIVED (QG228); spacetime dynamics EMERGENT (QG222);
the bridge (ρ as common object) DERIVED. The information–geometry bridge is ρ —
geometry and information are two faces of the same actualization object. No new
primitive; canonical AT unchanged.

ResearchY-QG_002 (Distinguishability → Geometry Audit, COMPLETE, tests 6/6 PASSED):
can geometry be reconstructed directly from distinguishability? Verdict: YES —
spacetime geometry is a MANIFESTATION of distinguishability; the metric is a pure
function of the N=96 state structure. DIRECT MAPPING: Distinguishability (N=96,
D_039) → Spectrum (λ_k = 2−2cos(2πk/N)) → ρ (count density) → Metric g = ρ^(2/d)η
(QG197) → Einstein tensor (QG222). The chain is pure-functional: the state count
determines the spectrum, which determines ρ, which determines the metric — so metric
information (the conformal factor ρ^(2/d)) is INFERABLE from the distinguishability
structure alone (verified: λ₁ = 0.004282, λ₂ = 0.017110; conformal factors 0.448/0.630/
0.788 for ρ=0.3/0.5/0.7). TESTS: horizon formation (metric from g; information
conserved across it, NP_020/021); ΩΛ = I_occ/ln K = 0.6839 (information and geometry
share ρ, QG_001); measurement (resolves ρ; reveals pre-existing distinguishability,
M_005) — all consistent. COMPARISON: AT is INFORMATION-FIRST (distinguishability →
spectrum → ρ → metric), not geometry-first (which would assume an external metric).
Counterexamples: 'geometry generates distinguishability' fails (the metric is a derived
function of ρ; N=96 is the primitive); 'the metric is an external input' fails (g =
ρ^(2/d)η follows from the spectrum); 'information and geometry are independent' fails
(both share ρ, QG_001); 'the state count does not fix the geometry' fails (N → spectrum
→ ρ → g is pure-functional). Falsification: a metric deviation from g = ρ^(2/d)η with
the same N=96 spectrum; a geometry not determined by the spectrum; a measurement of the
metric not passing through ρ. Classification: distinguishability DERIVED (D_039);
spectrum DERIVED (from N); count density ρ DERIVED; metric g = ρ^(2/d)η DERIVED
(QG197); geometry as a manifestation of distinguishability DERIVED; information I =
KL(ρ‖uniform) DERIVED (QG228). AT is information-first: geometry is derived from the
primitive distinguishability. No new primitive; canonical AT unchanged.

ResearchY-QG_003 (Information Reconstruction Audit, COMPLETE, tests 6/6 PASSED): can
geometry be reconstructed from information alone? Verdict: NO — geometry is NOT
informationally complete. The information content I = KL(ρ‖uniform) = 0.7513 nats
(QG228) is a SINGLE SCALAR; ρ is a full distribution, and many distributions share the
same KL-divergence, so I does not determine ρ uniquely. ΩΛ = I_occ/ln K = 0.6839 DOES
fix ln K = I_occ/ΩΛ = 1.0986 (K ≈ 3 — the state-space SIZE), but not the distribution
ρ. The metric g = ρ^(2/d)η (QG197) requires ρ, so g is NOT reconstructible from I
alone. The reconstruction chain Information → ρ → g FAILS at the first step (I is a
scalar, ρ is a distribution). COMPARISON: Information → Geometry — NO (I is derived
FROM ρ, not the source of ρ); Geometry → Information — NO (g is also derived FROM ρ).
NEITHER direction inverts the chain: ρ is the source; information and geometry are both
derived FROM ρ. PROVE/REFUTE: geometry is informationally complete — REFUTED (a scalar
cannot determine a distribution; g requires ρ; the reconstruction requires the STATE
STRUCTURE, N=96, which is the actual primitive). CORRECT CHAIN: State structure (N=96)
→ spectrum → ρ → {I = KL(ρ‖uniform), g = ρ^(2/d)η} — forward and pure-functional
(QG_002); the inverse is not invertible. Counterexamples: 'I determines ρ' fails (many
distributions share a KL); 'g from I alone' fails (needs ρ); 'ΩΛ determines the metric'
fails (fixes ln K, the size, not the distribution). Falsification: a metric successfully
reconstructed from I alone (no state structure); two distinct ρ with the same KL
producing different g. Classification: ρ DERIVED (from the state structure); I =
KL(ρ‖uniform) DERIVED (QG228); g = ρ^(2/d)η DERIVED (QG197); the I → ρ inversion
REFUTED (a scalar cannot determine a distribution); information-only reconstruction
BOUNDARY (requires the state structure). The state structure (N=96) is the actual
primitive that fixes ρ; information and geometry are both its derived faces. No new
primitive; canonical AT unchanged.

ResearchY-QG_004 (ρ Nature Audit, COMPLETE, tests 6/6 PASSED): why does ρ generate
both geometry and information? Verdict: ρ is fundamentally a COUNT STRUCTURE (option C)
— the normalized counting measure ρ_k = count_k/total, Σρ_k = 1 (Born, QG216).
Geometry (g = ρ^(2/d)η, QG197) and information (I = KL(ρ‖uniform) = 0.7513 nats,
QG228) are its two DERIVED faces. REMOVAL TESTS: remove geometry (g) → information
survives (I = KL(ρ‖uniform) needs no metric); remove information (I) → geometry
survives (g needs no KL); remove the count structure (ρ) → BOTH g and I vanish (both
are functions of ρ). OBSERVABLES: ΩΛ = I_occ/ln K = 0.6839, Ωm = 1−ΩΛ, measurement
(resolves ρ, M_005), black-hole information (conserved in the states), and the metric
construction (g = ρ^(2/d)η) — NONE survives the removal of ρ; all pass through it.
PRIMITIVE COMPARISON: geometry — information survives without it (NOT the primitive);
information — geometry survives without it (NOT the primitive); COUNT STRUCTURE — both
g and I vanish without it (YES, the primitive). MINIMAL DESCRIPTION: ρ_k = count_k/
total, Σρ_k = 1 — a normalized counting measure; nothing more is needed. NECESSITY
PROOF: count is necessary for both faces (g and I are functions of ρ); geometry is not
necessary for information, and information is not necessary for geometry — the
asymmetry makes count the primitive. Counterexamples: 'ρ is fundamentally geometric'
fails (remove geometry → information survives); 'ρ is fundamentally informational'
fails (remove information → geometry survives); 'geometry and information are
independent' fails (both vanish without ρ). Falsification: a metric or information
content not determined by the count density; an observable (ΩΛ, metric, measurement)
surviving the removal of ρ. Classification: count structure ρ = count_k/total DERIVED
(from the state structure, D_039); geometry g = ρ^(2/d)η EMERGENT (a derived face of
ρ); information I = KL(ρ‖uniform) EMERGENT (a derived face of ρ); the count nature of
ρ DERIVED (it is a normalized counting measure). No new primitive; canonical AT
unchanged.

ResearchY-QG_005 (Count-to-Geometry Origin Audit, COMPLETE, tests 6/6 PASSED): why
does count structure generate geometry? Verdict: geometry is a NECESSARY consequence of
distinguishability counting (option C) — NOT fundamental (A) and NOT informational (B).
REMOVAL TESTS: remove the metric → the count structure survives (ρ_k = count_k/total
needs no metric); remove the count structure → geometry is UNDEFINED (g = ρ^(2/d)η
requires ρ). MINIMAL PRINCIPLE generating g = ρ^(2/d)η: MEASURE PRESERVATION (QG207) —
the unique conformal-flat metric preserving the counting measure has √(−g) = ρ^(kd/2) =
ρ ⟹ k = 2/d (verified: kd/2 = 1 for d=2,3,4), and the geodesic acceleration gives the
same (a = −(1/d)ρ′/ρ ⟹ k/2 = 1/d). WHY A DENSITY BECOMES A RULER: the metric is the
geometric object that keeps the counting measure ρ as its volume element (√(−g) = ρ) —
the ruler is REQUIRED to measure the density's own volume; geometry is not an arbitrary
postulate but the unique conformal structure that conserves the count. TESTS: horizons
(metric-defined; the count persists behind), ΩΛ = I_occ/ln K = 0.6839 (the same ρ),
measurement (resolves ρ; reveals pre-existing distinguishability, M_005), state
identity (95 states, the count domain) — all consistent. PROVE/REFUTE: geometry is the
measurement of the distinguishability density — PROVEN (distinguishability → count
density ρ → the unique conformal metric preserving it). Counterexamples: 'geometry is
fundamental' fails (remove the metric → count survives); 'geometry is informational'
fails (remove information → geometry survives, QG_004); 'the metric is an arbitrary
postulate' fails (measure preservation √(−g) = ρ forces k = 2/d uniquely); 'a density
does not need a ruler' fails (the ruler is required to measure the density's volume).
Falsification: a metric not preserving the count (√(−g) ≠ ρ); a conformal metric with a
different exponent preserving √(−g) = ρ; a defined metric with no count density.
Classification: count structure ρ DERIVED (from the state structure, D_039); metric g =
ρ^(2/d)η DERIVED (QG197, from measure preservation QG207); geometry as count-derived
DERIVED; the measure-preservation principle (√(−g) = ρ) DERIVED (the unique conformal
metric); geometry as an independent postulate REFUTED (option A). Geometry IS the
measurement of the distinguishability density. No new primitive; canonical AT unchanged.

ResearchY-QG_006 (Count Conservation Origin Audit, COMPLETE, tests 6/6 PASSED): why
must count be conserved? Verdict: count conservation (Σρ = 1) is DEFINITIONAL — built
into the counting measure via the normalizer S (ρ_k = μ^k/S, QG194) — and NECESSARY:
removing it collapses geometry, information, measurement, and black-hole bookkeeping
SIMULTANEOUSLY (there is no "first" quantity lost). DEFINITIONAL: ρ_k = μ^k/S, Σρ_k =
Σμ^k/S = 1 by construction (verified: for μ=2, S=6, ρ₁+ρ₂ = 2/6+4/6 = 1) — the
normalizer S guarantees conservation (QG194/222: count conservation is the native
Noether/continuity statement). REMOVE COUNT CONSERVATION — WHAT DISAPPEARS: geometry
(√(−g) = ρ fails — the metric is no longer a measure, QG207); information (KL(ρ‖uniform)
undefined — ρ is not a probability distribution, QG228); measurement (Born rule
Σ|ψ|² = 1 invalid, QG216); black-hole bookkeeping (H_before ≠ H_after — information not
conserved, NP_020/021). NO ORDERING: everything fails at once, because all are functions
of the normalized count — there is no "first". DEFINITIONAL OR NECESSARY: BOTH — Σρ = 1
is built into ρ's definition, and precisely for that reason every downstream structure
requires it; the definition is not arbitrary but the minimal consistent one.
Counterexamples: 'count conservation is arbitrary' fails (it is the minimal consistent
definition of a counting measure); 'geometry survives without it' fails (√(−g) = ρ
requires a normalized ρ); 'information survives without it' fails (KL requires a
probability distribution); 'black-hole information is independent' fails (H_before =
H_after IS count conservation). Falsification: a normalized counting measure without
Σρ = 1; a structure (geometry/info/measurement) functioning with Σρ ≠ 1; a quantity
that survives removal while another fails. Classification: count conservation DERIVED
(from the definition of ρ as a normalized counting measure, QG194); geometry/
information/measurement/bookkeeping DERIVED (all require the normalized count). No new
primitive; canonical AT unchanged.

ResearchY-QG_007 (Count Conservation Necessity Audit, COMPLETE, tests 5/5 PASSED): is
count conservation merely definitional or a necessary consequence of Difference?
Verdict: PROVEN — count conservation (Σρ = 1) is a NECESSARY consequence of Difference,
via the FINITENESS of the distinguishable state space. Difference IS distinguishability
(D_039) — the 95-state structure — and a count over a FINITE state space must be
normalized to define probabilities (Born rule Σ|ψ|² = 1, QG216) and measures (√(−g) =
ρ, QG207). REMOVE COUNT CONSERVATION: the QUALITY of Difference survives (the 95 states
remain distinct — distinguishability is a property of the state space, separate from the
count normalization), but information (KL(ρ‖uniform) undefined, QG228), geometry
(√(−g) = ρ no measure, QG207), and measurement (Born invalid, QG216) all FAIL —
Difference as a physical source is incoherent (only a bare quality survives).
ALTERNATIVES: no alternative primitives ({Difference, η} only, D_027); no alternative
count structures (normalization is forced by measure preservation). PROOF (Difference
logically implies count conservation): Difference → distinguishability (D_039) → FINITE
state space (95 states) → count over a finite set → normalization REQUIRED for
probabilities and measures → Σρ = 1. The finite state space is the decisive link:
finite distinguishability ⟹ normalization ⟹ conservation. Counterexamples: 'count
conservation is purely arbitrary' fails (forced by the finite state space); 'Difference
exists without conservation' fails (only as a bare quality); 'an alternative count
structure exists' fails (normalization forced by measure preservation); 'a non-normalized
count is physical' fails (cannot define probabilities or measures). Falsification: a
finite distinguishable state space with a physical unnormalized count; a probability/
measure defined over an unnormalized finite count; a physical structure from a
non-Difference primitive. Classification: count conservation DERIVED (from Difference +
finiteness); distinguishability as a quality DERIVED (D_039); information/geometry/
measurement DERIVED (require the normalized count); alternative primitives BOUNDARY
(none — {Difference, η} only). No new primitive; canonical AT unchanged.

ResearchY-QG_008 (Finite Distinguishability Audit, COMPLETE, tests 7/7 PASSED): why
must distinguishability be finite? Verdict: finite distinguishability is a BOUNDARY —
required for physics (finite information, well-defined normalization and measure) but
not logically implied by Difference. COMPARE finite vs infinite: finite N (95) —
normalization Σρ = 1 well-defined, count conservation clear, geometry √(−g) = ρ
well-defined, information log₂(95) = 6.57 bits finite; infinite N — normalization
requires a convergent series (not automatic), count conservation via a limit, geometry
needs a limit measure, information log₂(N) → ∞ DIVERGES. SURVIVAL TESTS: normalization
survives only with a convergence assumption (geometric series needs |r| < 1, sum =
1/(1−r)); count conservation survives via a limit; geometry survives with a limit
structure; INFORMATION DOES NOT SURVIVE — log₂(N) → ∞. FIRST BREAKDOWN: INFORMATION
(log₂(N) diverges); normalization, geometry, and measurement are SECOND (limit-
assumption dependent). DETERMINATION: A) finiteness required — PARTIAL (required for
information/normalization/geometry); B) finiteness emergent — NO; C) finiteness
boundary — YES (the VALUE N=96 is derived via closure, D_015/D_019; the FINITENESS
itself is a separate property of the state space — Difference produces distinguish-
ability, not a count). SMALLEST PRINCIPLE forcing finiteness: the CONSISTENCY
requirement that the state space admit a well-defined normalization (Σρ = 1), a finite
information content (log₂ N < ∞), and a measure (√(−g) = ρ) — jointly requiring N < ∞.
PROVE/REFUTE: Difference implies finite state count — REFUTED (Difference produces the
act of distinguishing; the count is a separate input). If infinite distinguishability
is allowed: first breakdown INFORMATION, then normalization/geometry/measurement.
Counterexamples: 'Difference implies finiteness' fails (produces distinguishability,
not a count); 'infinite distinguishability has finite information' fails (log₂(N)
diverges); 'normalization is automatic for infinite N' fails (needs convergence);
'geometry extends to infinite N' fails (limit measure needed). Falsification: a
derivation of finiteness from Difference alone; an infinite state space with finite
information; a different closure value. Classification: finiteness BOUNDARY (a property
of the state space, required for physics); the value N=96 DERIVED (closure);
normalization/count conservation DERIVED (from finiteness, QG_007); information DERIVED
(finite only); geometry DERIVED (needs the finite measure). No new primitive; canonical
AT unchanged.

ResearchY-QG_009 (Infinite State Space Consistency Audit, COMPLETE, tests 7/7 PASSED):
can an infinite distinguishable state space support normalization, information,
measurement, geometry, and gravity without contradiction? Verdict: YES — an infinite
state space is INTERNALLY CONSISTENT for generic physics when the count density is
CONVERGENT (summable); finiteness is unnecessary for generic consistency and required
only for the AT uniform-reference observable chain. KEY CONSTRUCTION: the geometric
distribution ρ_k = (1−r)·r^k over k = 0,1,2,… satisfies Σρ_k = (1−r)/(1−r) = 1
EXACTLY (normalization survives, verified to machine precision) and carries finite
Shannon entropy H = −log₂(1−r) − (r/(1−r))·log₂ r = 2.0 bits for r = 0.5 (closed
form, numeric match); the power-law ρ_k ∝ k^(−2) normalizes via ζ(2) = 1.6449 with
H ≈ 2.36 bits. NORMALIZATION: existence YES (geometric, power-law), uniqueness (many
valid convergent families), stability (exact for fixed r) — only the UNIFORM
assignment fails (Σ1 = ∞). INFORMATION: the CAPACITY log₂(N) diverges, but the
realized entropy of a convergent infinite distribution is FINITE (2.0 bits geometric)
— 'information breaks first' is refined. FIRST GENUINE FAILURE: the UNIFORM REFERENCE
— a normalized uniform measure on a countably infinite set does not exist (Σc = c·∞
for all c), so the AT observable I_occ = KL(ρ‖uniform) = 0.7513 nats (QG228) and
ΩΛ = I_occ/ln K = 0.6839 (QG234) are ILL-DEFINED for infinite N. MEASUREMENT: Born
weights Σ|ψ|² = Σρ = 1 exact for any convergent ρ; state identity coherent over
infinite states. GEOMETRY: √(−g) = ρ (QG207) extends to any summable density — the
conformal metric survives. DETERMINATION: A) finite required — NO (for generic
consistency); B) finite emergent — NO; C) finite unnecessary — YES (required only for
the AT uniform-reference information observable). PROVE/REFUTE: physics requires
finite distinguishability — REFUTED as a generic necessity (a convergent infinite
space is fully consistent across normalization, entropy, measurement, geometry,
gravity); upheld only for the AT KL-to-uniform observable chain. REFINES QG_008:
'information breaks first' holds for the uniform capacity and the AT KL observable,
NOT for realized information content. Counterexamples: 'infinite states ⇒ information
diverges' fails (H = 2.0 bits geometric); 'infinite states ⇒ no normalization' fails
(Σ(1−r)r^k = 1 exactly); 'infinite states ⇒ no measurement' fails (Born weights sum
to 1); 'infinite states ⇒ no geometry' fails (√(−g) = ρ extends to summable ρ);
'KL(ρ‖uniform) defined for infinite N' fails (no normalized uniform measure); 'QG_008
fully correct' — refined (only capacity and AT KL break). Falsification: an infinite
convergent ρ that cannot normalize; a convergent ρ with infinite entropy; an infinite
construction forming KL-to-uniform; an infinite I_occ. Classification: finiteness
BOUNDARY for the AT observable chain (I_occ, ΩΛ), NOT required for generic
consistency; normalization Σρ = 1 DERIVED (exact for any convergent ρ); realized
entropy H(ρ) DERIVED (finite for convergent ρ); capacity log₂(N) DERIVED (diverges —
a capacity, not an observable); uniform reference REFUTED for infinite N; AT
observable I_occ/ΩΛ BOUNDARY (finite-only); measurement/geometry/gravity DERIVED
(survive any convergent ρ). No new primitive; canonical AT unchanged.

ResearchY-QG_010 (Observable Finiteness Audit, COMPLETE, tests 5/5 PASSED): why is the
observable state space finite if infinite distinguishability is consistent? Verdict:
OBSERVABILITY REQUIRES FINITE DISTINGUISHABILITY — PROVEN. A measurement event is a
FINITE act (M_001: reads both quadratures of one complex mode, D_037; produces one
outcome) with finite information capacity log₂(N_obs) (M_004: resolving WHICH of
N_obs states is realized carries log₂(N_obs) bits; log₂(95) = 6.57 bits). COMPARE
observable finite vs infinite: finite N (95) — state identity fully resolvable
(D_039), measurement resolves 1 of 95, information gain log₂(95) = 6.57 bits/event,
distinguishability 95/95 realized; infinite N — states distinct in principle but only
finitely many resolvable per event, one event must resolve 1 of ∞ (impossible with
finite info), information gain log₂(N) → ∞ DIVERGES (verified log₂(10⁹) = 29.9 bits
and growing), observable distinguishability bounded by event capacity. THE SELECTING
PROPERTY: the FINITE INFORMATION CAPACITY OF A SINGLE MEASUREMENT EVENT — finite
event (M_001) → finite capacity log₂(N_obs) (M_004) → finite outcome alphabet →
FINITE OBSERVABLE STATE SPACE. THE FOUR FINITE LINKS: finite observer (M_006 — the
observer is itself a distinguishable subsystem), finite measurement (M_001 — both-
quadrature readout), finite resolution (M_002/M_004 — one outcome per event), finite
bookkeeping (M_005/NP_021 — finite conserved count) — ALL present. PROVE/REFUTE:
observability requires finite distinguishability — PROVEN (a finite act cannot index
an infinite alphabet; an infinite observable space would need log₂(N) → ∞ bits/event;
so N_obs < ∞). DOES NOT CONTRADICT QG_009: infinite state spaces are structurally
consistent, but the OBSERVABLE PROJECTION of any state space is finite. RESOLVES
QG_009 OP1: observability (not Difference) is what pins the finite OBSERVABLE state
space — the finite state space physics uses is the largest set a single actualization
event can resolve. Counterexamples: 'an infinite observable space has finite per-event
info' fails (log₂(N_obs) → ∞); 'a finite event can resolve 1 of ∞ outcomes' fails
(needs infinite info to index an infinite alphabet); 'observability is independent of
state-space size' fails (measurement resolves state identity — the resolvable set IS
the observable space); 'the observer can be infinite' fails (a distinguishable
subsystem, M_006); 'QG_009 makes observability infinite-consistent' fails (structural
only; observability projects onto a finite set). Falsification: an observable state
space larger than 2^(info per event); an event resolving an infinite alphabet with
finite information; per-event info exceeding the event's capacity; an infinite
observer (contradicts M_006). Classification: observable state-space finiteness
DERIVED (from the finite measurement event's information capacity, M_004); the finite
measurement event EMERGENT (M_001); finite information capacity log₂(N_obs) DERIVED
(M_004); state identity DERIVED (D_039); underlying state-space finiteness BOUNDARY
(QG_008 — required for the KL-to-uniform chain); observable distinguishability DERIVED
(bounded by the event's capacity). No new primitive; canonical AT unchanged.

ResearchY-QG_011 (Finite Event Principle Audit, COMPLETE, tests 6/6 PASSED): why must
observation occur through finite events? Verdict: FINITE EVENT RESOLUTION IS A
CONSEQUENCE OF ACTUALIZATION — one discrete step produces one outcome, so finite
observation is DERIVED (option B emergent), not a separate boundary. DEFINITIONS:
event = one actualization act (one discrete step of the phase advance, Δθ = 2πk/N per
tick, D_041); observation = an event applied to a distinguishable state (M_001);
finite outcome = one resolved state among the finite observable set (QG_010); finite
resolution = one outcome per event. COMPARE finite vs infinite-resolution event:
finite — one discrete step (D_041), ONE outcome (M_001), information log₂(95) = 6.57
bits (M_004), identity fixed, normalization Σρ = 1 well-defined, geometry √(−g) = ρ on
the finite space; infinite-resolution — would be infinitely many steps (NOT one
event), no single outcome, information log₂(N) → ∞ DIVERGES, no identity, Born sum
over ∞ outcomes (no realized state), no definite count → no measure. TEST: can one
event resolve infinitely many states? NO — one step produces one outcome (M_001); an
event that 'resolves everything' resolves nothing. FIRST INCONSISTENCY: the event's
own DEFINITION — an 'infinite-resolution event' is not one event but infinitely many
steps (contradicts the meaning of 'event' as one tick of Δθ = 2πk/N, D_041); state
identity, information gain, normalization, and geometry fail second. STRUCTURE TESTS:
state identity needs a definite outcome (single step); information gain per event =
log₂(95) finite; measurement Born Σ|ψ|² = 1 on a definite outcome; normalization
Σρ = 1 (QG_007) via finite event → finite information (geometric check Σ(1−r)r^k = 1
exact); geometry √(−g) = ρ needs a definite count. DETERMINATION: A) finite events
required as a separate principle — NO; B) finite events emergent — YES (from the
discrete actualization step, D_041); C) finite events boundary — NO for resolution
(derived), but the DISCRETENESS of actualization itself (the tick) is the final
BOUNDARY (canonical structure). MINIMAL PRINCIPLE: the discrete step structure of
Actualization (Δθ = 2πk/N per tick) — no separate 'finiteness of observation'
postulate needed. PROVE/REFUTE: Actualization implies finite event resolution —
PROVEN (one event = one step = one outcome = finite, by the meaning of 'event' in
canonical AT). Counterexamples: 'an infinite-resolution event is a single event'
fails (infinitely many steps — contradicts D_041); 'one event can resolve infinitely
many states' fails (one step produces one outcome, M_001); 'infinite-resolution
observation has finite info' fails (log₂(N_obs) → ∞, M_004 violated); 'normalization
survives infinite events' fails (Born sum over ∞ outcomes); 'finite observation is a
new boundary' fails (derived from the discrete step, D_041). Falsification: a
canonical event that is not a single tick; an event resolving multiple outcomes
simultaneously; an infinite-resolution event consistent with discrete actualization;
a derivation of the tick from something deeper. Classification: finite event
resolution DERIVED (from the discrete actualization step, D_041); the finite outcome
EMERGENT (M_001 — the actualization readout); finite information capacity DERIVED
(M_004); normalization DERIVED (QG_007); observable finiteness DERIVED (QG_010); the
discreteness of actualization (the tick) BOUNDARY (canonical structure); underlying
state-space finiteness BOUNDARY (QG_008). The QG_008–QG_011 chain is closed: the
discreteness of the actualization tick is the irreducible canonical input. No new
primitive; canonical AT unchanged.

ResearchY-S_001 (Post-V2.1 Theory Architecture Synthesis, COMPLETE, tests 4/4 PASSED):
reconstruct the entire theory from the deepest derived origin to observable physics.
Verdict: after V2.1, AT is a theory of ONE object — the count density ρ generated by
Actualization. THE CANONICAL CHAIN: Difference → Distinguishability (D_039, 95
states) → Actualization event (discrete tick, QG_011) → spectrum λ_k =
2−2cos(2πk/N) (D_041, N=96) → complex state ψ = |ψ|e^{iθ} (D_036) → count density
ρ (Σρ = 1, QG194/QG216) → ρ IS THE INFORMATION–GEOMETRY BRIDGE (QG_001):
INFORMATION face I = KL(ρ‖uniform) = 0.7513 nats (QG228) → ΩΛ = I_occ/ln K = 0.6839
(QG234, OBSERVED 0.12%), Ωm = 0.3161; GEOMETRY face g = ρ^(2/d)η (QG197) with
√(−g) = ρ ⟹ k = 2/d (QG207, QG_005) → native dynamics (QG222) → Einstein tensor →
GRAVITY (QG_002, EMERGENT). MEASUREMENT = the readout face (M_001–M_006): one event
reads both quadratures, pins the phase (M_002), feeds back (M_003), gains log₂(95) =
6.57 bits (M_004), conserves information by reveal + redistribute (M_005), observer =
epistemic recipient (M_006); the unique QM discriminator is AT-P042 (discrete tick
time-parameter, M_009). FINAL BOUNDARY SET (8 items): 5 canonical inputs (R_001 —
{Difference, η}, {Z2-paired sector}, {3 families}, {SU(2) gauge + j=1/2}, {v, m_e})
+ 3 structural (QG_008 state-space finiteness, QG_009 uniform reference, QG_011 tick
discreteness). DEEPEST SINGLE BOUNDARY: the discrete actualization tick (QG_011) —
finite events and observable finiteness follow from it (QG_011/QG_010). WHAT THEORY
IS ABOUT: physics is the normalized count structure generated by Actualization, whose
information face gives measurement and cosmology and whose geometric face gives
spacetime and gravity. V2.0: core chain + hosted QM/SM/GR + falsification frontier.
V2.1: the origin program (D_020–D_045) — N=96, complex sector, reciprocity, su(2)
compact-form, the five-item boundary set, 8 new predictions. V2.2: measurement
program (M), lever & coupling audits (NP — one lever, sync OPTIONAL, O(2) doublets
strongest prediction, information cosmology, horizon bookkeeping), geometry-bridge
group (QG — ρ is the bridge, geometry = measure-preserving metric, count primitive,
finiteness chain). Falsification: ρ-bridge (a metric/info not from ρ), ΩΛ deviation
beyond 0.12%, O(2) |Δλ|>0, ω₁ ≠ √91·(2π/N)·scale, H_before ≠ H_after, sync required,
a non-tick event. Classification: count density ρ DERIVED; spectrum/state/complex
structure DERIVED; information/geometry/measurement/gravity DERIVED (from ρ);
measurement event/observer/spacetime EMERGENT; canonical boundaries BOUNDARY. No new
primitive; canonical AT unchanged.

ResearchY-T_001 (Spectral Blueprint — Inverse Spectral Design, COMPLETE, tests 7/7
PASSED): can material properties be designed by specifying a TARGET spectrum and
reconstructing the coupling graph, instead of the forward path topology → simulation
→ observed spectrum? Verdict: SUPPORTED for circulant (ring) graphs — the Laplacian
eigenvalues are the DFT of the coupling row, so the inverse problem is CLOSED FORM:
λ_k = Σ_d w_d(1−cos 2πdk/N), w_d = −(1/N)Σ_k λ_k cos 2πdk/N. FINDINGS (N=96): (1) the
round trip spectrum→weights→spectrum closes to floating-point precision for ALL
targets (band-gap, clustered, octave-spaced, max-separated, D96) — inverse spectral
design is exact; (2) the inverse map is an ISOMETRY, ||Δw||₂ = (1/√N)||Δλ||₂
(condition number 1, Parseval) — maximally robust; (3) PHYSICALITY (non-negative
coupling) selects spectra: D96 (3 octave bands, span 6.4) reconstructs to SPARSE
all-attractive coupling (6 edges, weight 1, degree 12 — exactly the connection set
{±1..±6}); max-separated (uniform) is also all-attractive but 24 edges; band-gap,
clustered, and extreme-octave (span ~3444) spectra require DENSE + NEGATIVE
(repulsive) couplings — exotic couplings a simple oscillator material cannot
provide. Classification: inverse-map exactness DERIVED; isometry DERIVED;
physicality-selects-sparse-band-spectra CORRESPONDENCE; hypothesis SUPPORTED for
smooth moderate-span spectra / REFUTED for discontinuous or extreme-span spectra.
This opens the ResearchY-T (Technology) group: the spectral-blueprint inverse map
exists and is closed-form for rings; the general non-circulant weighted-graph
inverse problem (inverse eigenvalue problem for graphs) is left to ResearchY-T_003.
No new primitive; canonical AT unchanged.

ResearchY-T_002 (Physical Spectrum Audit, COMPLETE, tests 8/8 PASSED): which target
spectra admit SPARSE POSITIVE-weight (physical) realizations? Verdict: a spectrum is
physically realizable by a ring material iff it is a NEGATIVE-DEFINITE function on
Z_N — the physical spectra form the convex cone spanned by the single-edge generators
g_d = 2(1−cos 2πdk/N) (Bochner/Schoenberg). FINDINGS (N=96): (1) physicality score
P = (attractiveness + sparsity + stability)/3 classifies the five targets — D96
0.958 PHYSICAL (6 edges, 0 negative), band-gap 0.236 / clustered 0.235 / octave 0.190
UNPHYSICAL (dense + repulsive), max-separated 0.500 MARGINAL (24 edges, 0 negative);
(2) BOUNDARY MAP (where the smallest generator coefficient w_d crosses zero):
geometric r = 1.0110 (span ≈ 1.67 — octave spectra go unphysical below one octave),
band-gap g = 0.0000 (D96 sits exactly ON the cone boundary — any band-gap opening is
immediately unphysical), clustered sep = 0.40; (3) 0/400 random spectra are physical —
physicality is a measure-(near-)zero, structured property, not a generic one.
Classification: physical-spectra = negative-definite cone DERIVED; D96-like circulant
family always physical DERIVED; physicality-score map CORRESPONDENCE; hypothesis
SUPPORTED for smooth moderate-span band-structured spectra / REFUTED for
discontinuous or extreme-span spectra. Next step: the general non-circulant weighted
graph inverse problem (ResearchY-T_003). No new primitive; canonical AT unchanged.

ResearchY-T_003 (General Inverse Spectral Graph Audit, COMPLETE, tests 5/5 PASSED):
can a target spectrum reconstruct a physical NON-CIRCULANT weighted graph, and does
the spectrum uniquely determine the graph? Verdict: the spectrum ALONE does NOT
determine a general graph — isospectral non-isomorphic pairs exist (exhaustive search:
0 pairs on n=5, 4 pairs on n=6, with degree-sequence signatures (2,2,2,2,2,4)↔
(1,2,2,3,3,3) at 7 edges and (1,3,3,3,3,3)↔(2,2,2,3,3,4) at 8 edges). FINDINGS: (1)
full eigendecomposition L = VΛVᵀ reconstructs every graph EXACTLY (spectral error
~1e-15, graph similarity 1.00) — spectrum+eigenbasis loses nothing; (2) spectrum alone
is LOSSLY for general graphs (isospectral degeneracy); (3) circulant (ring) graphs are
spectrally RIGID — the labeled spectrum ↔ connection set is bijective (cycle C6 → {±1},
D96 → {±1..±6}, via IDFT); (4) the six named families (path, cycle, grid, random,
complete) are each spectrally unique among their size, but rigidity is NOT generic.
Classification: eigendecomposition-exactness DERIVED; spectrum-lossy-for-general-graphs
DERIVED; circulant-rigidity CORRESPONDENCE; "spectrum as complete blueprint" REFUTED
for general graphs / SUPPORTED for the circulant D96 structure. THE T-PROGRAM CONCLUSION:
the spectral blueprint (T_001/T_002) is a SPECIAL property of circulant structure — D96's
circulant geometry is what makes its spectrum a complete, unique, canonical description,
while a generic graph's spectrum is an incomplete (degenerate) description. No new
primitive; canonical AT unchanged.

ResearchY-T_004 (Spectral Rigidity Audit, COMPLETE, tests 3/3 PASSED): which graph
families are uniquely determined by their spectrum? Measure rigidity R = 1 −
P(isospectral partner) per family (exhaustive n=6, plus D96 at n=96). RIGIDITY RANKING
(n=6, labeled): circulant R=1.0000 (8 graphs, 0 degenerate), path R=1.0000 (360, 0),
cycle R=1.0000 (60, 0), grid R=1.0000 (90, 0, perturbation-stability 0.733 = FRAGILE),
complete R=1.0000 (1, 0), random-sparse R=0.9950 (200, 1), all R=0.9780 (32768, 720),
bipartite R=0.9652 (5177, 180 — the LEAST rigid). D96-derived R=1 (circulant; labeled
spectrum → IDFT → {±1..±6} unique). FINDINGS: (1) structured families (complete, path,
cycle, grid, circulant) are maximally rigid (R=1); (2) rigidity is GRADED not generic —
bipartite (3.5% degenerate) and random-sparse (0.5%) decay, consistent with "almost all
trees are isospectral"; (3) perturbation stability separates robust rigidity (complete/
path/cycle/circulant = 1.0) from fragile rigidity (grid = 0.733); (4) D96 sits in the
maximally rigid class. Classification: structured-family rigidity DERIVED; circulant/D96
rigidity DERIVED; rigidity-as-graded-property EMERGENT; "all families rigid" REFUTED.
T-PROGRAM CONCLUSION (T_001-T_004): the spectral blueprint is a SPECIAL property of
circulant (D96) structure — its spectrum is a complete, unique, canonical description,
echoing D96's role as the canonical AT attractor. No new primitive; canonical AT unchanged.

ResearchY-T_005 (Attractor Dominance Audit, COMPLETE, tests 4/4 PASSED): do a small
number of attractors dominate the state space, or is dominance a model artifact? Model:
attractors = distinct eigenspaces of the Laplacian (damped mode-locking); basin =
multiplicity fraction. DOMINANCE RANKING (N=96, by largest basin D): complete A=2 D=0.990
ATTRACTOR DOMINATED; unphysical clustered A=4 D=0.333; D96-3D (4×4×6 torus) A=13 D=0.208;
D96 A=45 D=0.062; physical max-sep / unphysical band-gap / unphysical octave A=49 D=0.021;
random-sparse A=96 D=0.010 — all others NOT DOMINATED. CRITICAL ANSWERS: (1) D96 produces
FEWER attractors than random (45 < 96) — YES; (2) one attractor dominates — NO for D96
(largest basin 6.2%), only the degenerate complete graph; (3) dominance increases with
spectral organization — YES weakly (complete 0.99 > D96 0.062 > random 0.010; D96-3D A=13
compresses more than 1D D96 A=45); (4) physical more dominant than unphysical — NOT
uniformly (unphysical clustered A=4 has fewer attractors than physical D96 A=45; physical
max-sep is IDENTICAL A=49 to unphysical band-gap/octave since basins are set by degeneracy
not physicality). Classification: attractor=distinct-eigenspace DERIVED; spectral
organization compresses count EMERGENT; "few attractors DOMINATE" REFUTED for D96;
dominance-is-a-model-artifact CONFIRMED. T-PROGRAM ARC (T_001-T_005) CONCLUSION: the
spectral blueprint/physicality/rigidity all flow from D96's circulant structure, but
attractor DOMINANCE does NOT — it requires either massive degeneracy (complete graph) or
the Darwinian selection layer (resource competition), confirming the model-dependence
question. No new primitive; canonical AT unchanged.

ResearchY-T_006 (Darwinian Dominance Emergence, COMPLETE, tests 3/3 PASSED): do dominant
attractors emerge from Darwinian resource competition rather than spectral structure?
Model-free scoring (no AT assumptions): replicator dynamics p_i(t+1)=p_i(t)w_i/Σp_j w_j
over the attractor (eigenspace) set, fitness w_i=r_i/c_i=m_i/λ_i (resource∝multiplicity,
cost∝eigenvalue). RESULTS (2000 steps): D96 A=44 D_init=0.062→D_final=1.000 t_dom=3
ratio=3.89; D96-3D A=12 D_init=0.208 t_dom=13 ratio=1.20; physical max-sep A=48 t_dom=4
ratio=2.00; unphysical clustered A=3 D_init=0.333 t_dom=2 ratio=5.00; random sparse A=95
D_init=0.010 t_dom=50 ratio=1.05; complete A=1 D_init=0.990 t_dom=1 ratio=inf.
HYPOTHESES: H1 spectral organization accelerates dominance SUPPORTED (D96 t_dom=3 vs
random 50, ~17×); H2 D96 fewer effective survivors REFUTED (both collapse N_eff≈1,
selection is universal); H3 complete degenerate SUPPORTED (A=1, already dominant).
SUCCESS CRITERION (dominance emerges only after competition) CONFIRMED: D rises 1-33% →
100% for every model. Classification: replicator→fittest DERIVED; dominance-emerges-after
competition EMERGENT; "spectral-structure-alone-determines-dominance" REFUTED. T-PROGRAM
ARC (T_001-T_006) CONCLUSION: the spectral blueprint, physicality, and rigidity all
flow from D96's circulant structure (T_001-T_004), but attractor DOMINANCE does not —
T_005 showed bare spectral structure does not dominate (largest basin 6%), and T_006 shows
the Darwinian selection layer (w=r/c resource competition) is what produces a single
dominant attractor — resolving the model-dependence question and confirming the
information-ecology mechanism as a dynamical layer beyond the eigenmode spectrum. No new
primitive; canonical AT unchanged.

ResearchY-T_007 (Bounded Innovation Audit, COMPLETE, tests 7/7 PASSED): does Darwinian
evolution (replicator + mutation + extinction + resource constraint) on a spectral
landscape produce a FINITE species count? Model: replicator–mutator over the non-zero
Laplacian eigenspaces, fitness w=m/λ, crowding f=w/(1+βx), ring mutation μ→k±1, extinction
threshold ε=1e-6, μ=0.01 β=1.0 T=20000 (deterministic; random seed 42). RESULTS (A→S∞):
D96 44→5 (N_eff 1.093, t_sat 7); D96-3D 12→9 (N_eff 2.904); random sparse 95→17
(N_eff 8.415, t_sat 268); complete 1→1; physical max-sep 48→5; unphysical clustered 3→3.
CRITICAL ANSWERS: (1) diversity saturates — YES (all reach plateau); (2) saturation
independent of runtime — YES (S∞/survivors identical at T=20000 & 40000); (3) D96 lower
asymptotic diversity — YES (S∞ 5 < random 17); (4) finite attractor landscape — YES (fixed
stable survivor set ≤ A). BOUNDED/UNBOUNDED/CONDITIONAL: finite landscapes BOUNDED;
open-landscape control UNBOUNDED without carrying capacity, BOUNDED with it (resource
constraint is the binding bound); saturation value CONDITIONAL on (μ,β,landscape).
Classification: species-count-bounded DERIVED; saturation value EMERGENT; "mutation drives
unbounded innovation" REFUTED. T-PROGRAM ARC (T_001-T_007): spectral structure compresses
(T_005), Darwinian selection dominates (T_006), and mutation + resource constraint still
saturate to a finite, D96-compressed species count (T_007) — innovation is bounded by
resources, completing the information-ecology arc as a dynamical layer beyond the
eigenmode spectrum. No new primitive; canonical AT unchanged.

ResearchY-T_008 (Asymptotic Diversity Limit Audit, COMPLETE, tests 8/8 PASSED): what
DETERMINES the saturated species count S∞ (from T_007)? Sweep μ, β, A, fitness variance,
spectral rigidity of the T_007 replicator–mutator. SCALING LAW: S∞ = min(A, N_fit(μ, β, {w}))
— a mutation–selection balance bounded by the landscape ceiling A. DERIVED laws (all
verified): 1 ≤ S∞ ≤ A; ∂S∞/∂μ ≥ 0 (μ 0.0001→0.5: S∞ 3,3,5,6,9); ∂S∞/∂β ≥ 0 (β 0→10: S∞
4,4,4,5,5,7 — crowding LEVELS the field, distinct from T_007's open-landscape carrying
capacity); μ=0 ⇒ S∞=1 (pure selection) while any μ>0 re-seeds S₀⁺>1; ∂S∞/∂A ≈ 0 (circulant
A 20→188 but S∞ ≈ 4–5, saturating). EMERGENT: the functional form F(μ, β, {w}) and its
coefficients. REFUTED: "S∞ is set by A alone"; "rigidity alone determines S∞" (D96-3D more
rigid 0.875 than D96 0.542 yet more survivors 9 vs 5); "variance alone determines S∞"
(D96-3D var 1.311 vs unphysical 1.083 yet 9 vs 3 survivors, A ceiling 12 vs 3). T-PROGRAM
ARC (T_001-T_008): spectral structure compresses (T_005) → Darwinian selection dominates
(T_006) → mutation + resource constraint bound innovation to a finite count (T_007) → and
that count is a mutation–selection balance min(A, N_fit(μ,β,{w})) whose monotonicities are
derived but whose value is emergent (T_008). No new primitive; canonical AT unchanged.

ResearchY-T_009 (Fitness-Reach Law Audit, COMPLETE, tests 6/6 PASSED): can N_fit (S∞ =
min(A, N_fit)) be expressed ANALYTICALLY? Verdict: YES in two exactly-solvable limits, NO in
general. μ=0 (pure selection + crowding): S∞ = #{w_k > Z*(β)}, Z* the unique root of
Σ_{w_k>Z}(w_k−Z) = β·Z — EXACT (matches simulation across all cases × β, within the ≤1
critical-slowing-down lag). β=0 (mutation + selection, no crowding): the map is linear, so S∞
= #{Perron(M·diag(w)) > ε} — EXACT. Small-μ uniform-gap reach: N_fit ≈ 1 + log(1/ε)/log(2δ/μ),
δ=Δw/w* — DERIVED form, EMERGENT accuracy (±1–2). REFUTED: "N_fit = F(μ, β, Δw)" with a single
scalar gap — two spectra with identical Δw=5 but different tails give different N_fit (5 vs 2
at μ=0, β=10); N_fit depends on the FULL fitness spectrum. T-PROGRAM ARC (T_001-T_009): spectral
structure compresses (T_005) → Darwinian selection dominates (T_006) → mutation + resource
constraint bound innovation (T_007) → S∞ is a mutation–selection balance (T_008) → whose N_fit
is exactly solvable at μ=0/β=0 but not by a single gap (T_009). No new primitive; canonical AT
unchanged.

ResearchY-T_010 (D96 Survivor Compression Audit, COMPLETE, tests 6/6 PASSED): why does D96
saturate near 5 survivors while random near 17? Can S∞ be predicted from spectral structure
alone? STRUCTURAL MEASURES (μ=0.01, β=1): D96 A=44 S∞=5 conc 0.362 ent 2.90 near-gap(≤2λ₂)=2
degen 0.542 maxmult 6; random A=95 S∞=17 conc 0.017 ent 4.53 near-gap=71 degen 0.010 maxmult 1;
D96-3D A=12 S∞=9; physical A=48 S∞=5; unphysical A=3 S∞=3. MECHANISM: D96's circulant symmetry
collapses 95 non-zero modes into 44 distinct eigenvalues (sparse near λ₂: only 2 modes within
2λ₂) → PEAKED fitness w=m/λ → ~5 survivors; random's dense near-degenerate spectrum (71 modes
within 2λ₂) → FLAT fitness → ~17 survivors. PREDICTABILITY: S∞ IS a function of spectral
structure alone — the β=0 Perron equilibrium S∞=#{Perron(M·diag(w))>ε} reproduces S∞ EXACTLY
for every case (T_009), and the μ=0 crowding threshold brackets the interior. Classification:
D96→5 vs random→17 split DERIVED (spectral tail via exact reach laws); precise integer EMERGENT;
"a single spectral scalar predicts S∞" REFUTED (D96 and physical BOTH give S∞=5 yet differ in
A 44 vs 48, entropy 2.90 vs 3.16, concentration 0.362 vs 0.225). T-PROGRAM ARC (T_001-T_010):
spectral structure compresses (T_005) → Darwinian selection dominates (T_006) → mutation +
resource constraint bound innovation (T_007) → S∞ is a mutation–selection balance (T_008) →
whose N_fit is exactly solvable at μ=0/β=0 (T_009) → and the D96 survivor compression is
explained quantitatively by the sparse circulant spectrum near λ₂ (T_010). No new primitive;
canonical AT unchanged.

ResearchY-T_011 (Species Ceiling Audit, COMPLETE, tests 6/6 PASSED): can the legacy AT-138/139
value (~19 stable species) emerge naturally from D96? Verdict: REFUTED. Four population measures
(transient peak, cumulative ever-alive, survivor S∞, turnover; uniform + fittest-init discovery):
D96 survivor = 5 and discovery cumulative = 5 (μ=0.01, β=1); the value ~19 is nearest the RANDOM
landscape (survivor 17, cumulative discovery 18), not D96. Mutation sweep (D96, β=1): S∞ = 3, 5,
6, 8, 9, 11, 13 at μ = 0.001..0.95 — never 19 short of the degenerate μ→1 uniform limit (S∞→A=44).
Classification: D96 species ceiling DERIVED (S∞ = min(A, N_fit) ≈ 5, T_008/T_009/T_010);
"~19 emerges naturally from D96" REFUTED; the legacy ~19 traces to a DIFFERENT model (Θ-field
pattern novelty, AT-138/139), nearest the random/near-degenerate spectrum (17–18). T-PROGRAM ARC
(T_001-T_011): spectral structure compresses (T_005) → Darwinian selection dominates (T_006) →
mutation + resource constraint bound innovation (T_007) → S∞ is a mutation–selection balance
(T_008) → N_fit exactly solvable at μ=0/β=0 (T_009) → D96 compression explained by the sparse
spectrum (T_010) → and the ~19-species legacy value does NOT trace to D96 but to a random-like
landscape (T_011). No new primitive; canonical AT unchanged.

ResearchY-T_012 (Origin of 19 Audit, COMPLETE, tests 6/6 PASSED): can the historical ~19-species
count emerge from D96^3 (cubic D96⊗D96⊗D96, NP_037/NP_088) rather than D96? Verdict: D96^3 gets
CLOSER but does not nail it. The cubic tensor spectrum (884,736 modes over 20,811 distinct
non-zero eigenvalues) gives S∞=16 (μ=0.01, β=1) — vs D96's 5 and random's 17. Sector splitting
(axis count): 1-axis 5, 2-axis 5, 3-axis 21. Oh irreps (permutation orbit): A({a,a,a}) 5,
T(two-equal) 6, G(distinct) 31. Cumulative discovery (fittest-init) = 8. Distance from 19: D96 14,
D96^3 3, random 2 — random remains nearest. Classification: D96^3 spectrum + S∞=16 DERIVED (3D
DOS raise 5→16 via T_008/T_009); 16 within ±5 of 19 CORRESPONDENCE (legacy AT-139 tolerance);
"D96^3 exactly derives 19 without tuning" REFUTED (gives 16, or 21 in the cubic sector). The
historical ~19 remains ambiguous between D96^3 (16), its cubic sector (21), and a random-like
landscape (17). T-PROGRAM ARC (T_001-T_012): compression (T_005) → dominance (T_006) → bounded
innovation (T_007) → S∞ balance (T_008) → solvable N_fit (T_009) → D96 compression (T_010) → ~19
not from D96 (T_011) → and not exactly from D96^3 either, only a CORRESPONDENCE (T_012). No new
primitive; canonical AT unchanged.

ResearchY-T_013 (Compression Origin Audit, COMPLETE, tests 5/5 PASSED): why does D96^3 compress
20,811 eigenvalues into 16 survivors? COMPRESSION MEASURES (μ=0.01, β=1): D96 A=44 S∞=5 C=8.8
conc 0.362 ent-red 0.88 near-gap 2 maxmult 6 basin 0.105; D96^3 A=20811 S∞=16 C=1300.7 conc
0.0006 ent-red 0.20 near-gap 18 maxmult 562 basin 0.0019; random A=95 S∞=17 C=5.6 conc 0.017
ent-red 0.03 near-gap 71 maxmult 1 basin 0.179. MECHANISM: D96^3's 3D sum structure gives a huge
A (20,811 distinct eigenvalues = 3-way sums of 49 reduced 1D values), while selection w=m/λ keeps
only the near-gap modes (S∞=16 ≈ the 18 modes within 2λ₂); the survivors ARE the near-gap modes.
Compression is dominated by A, NOT fitness peakedness: the 1D D96 has the HIGHEST entropy
reduction (0.88, most peaked fitness) yet compresses LEAST (C=8.8), while D96^3 has a flatter
fitness (ent-red 0.20) but compresses ~150× more (C=1300). Classification: C = A/S∞ DERIVED
(deterministic function of the spectrum; A = distinct-sum count, S∞ = reach ≈ near-gap density);
the value ~1300 EMERGENT; "C is set by fitness peakedness" REFUTED. T-PROGRAM ARC (T_001-T_013):
compression (T_005) → dominance (T_006) → bounded innovation (T_007) → S∞ balance (T_008) →
solvable N_fit (T_009) → D96 compression (T_010) → ~19 not from D96 (T_011) → nor exactly from
D96^3 (T_012) → and the compression ratio C is dominated by landscape size A, not fitness
peakedness (T_013). No new primitive; canonical AT unchanged.

ResearchY-T_014 (Near-Gap Density Audit, COMPLETE, tests 6/6 PASSED): what determines the
near-gap mode count N_gap(k) = #{λ ≤ kλ₂}? NEAR-GAP DENSITY (modes|distinct, λ₂, k=1.5/2/3/4):
D96 (λ₂=0.3864) 2|1, 2|1, 2|1, 4|2 (gap mult 2); D96^3 (λ₂=0.3864) 6|1, 18|2, 26|3, 32|4 (gap
mult 6, octahedral staircase 6+12+8+6); random (λ₂=17.19) 31|31, 71|71, 95|95, 95|95 (gap mult
1); physical (λ₂=1.0) 2|1, 4|2, 6|3, 8|4 (gap mult 2); unphysical (λ₂=5.0) 32|1 ×4 (gap mult 32,
32-fold-degenerate plateau). DIMENSION SCALING (Weyl law): D96 N_gap(4)/N_gap(1)=2.00 (∝k^1/2,
1D quadratic); D96^3 = 5.33 (∝k^3/2, 3D cubic DOS). Classification: N_gap(k)=Σ_{λ≤kλ₂}m(λ)
DERIVED (exact counting); N_gap(1)=degeneracy of λ₂ DERIVED (symmetry class); k-growth follows
the Weyl law (tensor-product dimension) DERIVED; magnitudes (2/6/32/71) EMERGENT (symmetry/
degeneracy class); "N_gap is universal in k" REFUTED. T-PROGRAM ARC (T_001-T_014): compression
(T_005) → dominance (T_006) → bounded innovation (T_007) → S∞ balance (T_008) → solvable N_fit
(T_009) → D96 compression (T_010) → ~19 not from D96 (T_011) → nor from D96^3 (T_012) → C
dominated by A (T_013) → and the near-gap density (which sets S∞ ≈ N_gap) is itself a derived
exact function of the spectrum (T_014). No new primitive; canonical AT unchanged.

ResearchY-T_015 (Spectral Robustness Audit, COMPLETE, tests 4/4 PASSED): is spectral robustness
controlled by near-gap density ρ(k) or gap size λ₂? Apply edge-removal/addition/weight-noise
perturbations to D96 / D96-3D / physical / unphysical / random / complete, measure Δλ₂/λ₂,
attractor shift ΔA, spectral L2 shift. ROBUSTNESS TABLE (λ₂, m(λ₂), ρ(2), Δλ₂/λ₂, ΔA, spectral,
degen): D96 (0.386, 2, 0.021, 0.0077, 30, 0.0073, 44); physical (1.0, 2, 0.021, 0.0343, 24,
0.0376, 47); D96-3D (1.0, 2, 0.042, 0.0446, 8, 0.0255, 11); unphysical (5.0, 32, 0.333, 0.2333,
2, 0.0505, 3); random (17.19, 1, 0.740, 0.0003, 0, 0.0028, 0); complete (96.0, 95, 0.990, 0.0403,
1, 0.0041, 1). HYPOTHESES: H1 gap size alone insufficient SUPPORTED (complete λ₂=96 MORE fragile
0.040 than D96 λ₂=0.386 0.008, yet random λ₂=17.2 MORE robust 0.0003); H2 low ρ predicts
robustness REFUTED (D96 lowest ρ=0.021 yet largest ΔA=30; complete highest ρ=0.99 yet ΔA=1); H3
D96 outperforms random REFUTED (random spectral 0.0028 < D96 0.0073). MECHANISM: robustness is
controlled by the DEGENERACY structure — ΔA counts degenerate eigenvalue levels that split under
symmetry-breaking (D96 has 44 degenerate levels → ΔA=30; random all-singleton → ΔA=0; complete
one 95-fold level → ΔA=1, a rank-1 perturbation splits one eigenvalue not 95). Classification:
ΔA = degenerate-level split count DERIVED (function of multiplicity structure); magnitudes
EMERGENT; "ρ predicts robustness" (H2), "λ₂ predicts robustness" (H1 implication), "D96 beats
random" (H3) all REFUTED. T-PROGRAM ARC (T_001-T_015): compression (T_005) → dominance (T_006) →
bounded innovation (T_007) → S∞ balance (T_008) → solvable N_fit (T_009) → D96 compression (T_010)
→ ~19 not from D96 (T_011) → nor D96^3 (T_012) → C dominated by A (T_013) → N_gap derived
(T_014) → and robustness is controlled by degeneracy, not ρ or λ₂ (T_015). No new primitive;
canonical AT unchanged.

ResearchY-QG_012 (Distinguishability Cosmology Audit, COMPLETE, tests 6/6 PASSED): is
ΩΛ uniquely privileged, or does distinguishability generate additional cosmological
observables? Verdict: ΩΛ is PRIVILEGED BUT NOT UNIQUE — distinguishability generates a
FINITE FAMILY of cosmological observables (option B): the density-fraction pair and its
deterministic closure. THE INFORMATION OBJECTS: I_occ = 0.7513 nats (KL(ρ‖uniform),
QG228), ln K = I_occ/ΩΛ = 1.098552 (derived convention, QG234), ΩΛ = 0.6839, Ωm =
0.3161, the full ρ, and the realized entropy H = ln K − I_occ = 0.3473 nats with the
identity I_occ + H = ln K (Ωm = H/ln K — the pair partitions the state-space size).
THE FINITE FAMILY: ΩΛ = I_occ/ln K = 0.6839 (observed 0.12%), Ωm = 1 − ΩΛ = 0.3161
(observed 0.26%), the ratio ΩΛ/Ωm = I_occ/(ln K − I_occ) = 2.1636, the current
deceleration q₀ = Ωm/2 − ΩΛ = −0.5258, and the turnaround redshift
z_acc = (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295 — q₀ and z_acc are deterministic closures of the
pair via the HOSTED FRW/ΛCDM relations (form CORRESPONDENCE, values DERIVED — two-level
rule). NOT INFORMATION FUNCTIONS: H₀ (dimensionful — needs an anchor), σ₈ (needs the
primordial amplitude A_s), BAO scale (needs the sound horizon Ωb/Ωr), structure growth
(needs A_s, n_s, growth index), weak lensing S₈ (inherits σ₈), horizon scale (needs
H₀), matter clustering (needs σ₈, P(k)) — so a FULL information cosmology (option C) is
REFUTED. DETERMINATION: A) ΩΛ uniquely privileged — PARTIAL (primary but not alone);
B) finite family of information observables — YES (pair + closures); C) full
information cosmology — NO. DISAPPEAR IF DISTINGUISHABILITY REMOVED: the entire family
(ΩΛ, Ωm, ratio, q₀, z_acc — I_occ undefined without the state space); the others
survive only as un-predicted measured values. COMPARE vs ΛCDM: AT turns the two free
density inputs (ΩΛ, Ωm) into PREDICTIONS and, via hosted FRW, q₀ and z_acc into DERIVED
values with fixed numbers. PREDICTION RANKING: ΩΛ (0.12%), Ωm (0.26%), ratio 2.1636,
q₀ = −0.526, z_acc = 0.630. Falsification: ΩΛ deviation beyond 0.12%; ratio deviating
from I_occ/(ln K − I_occ); q₀ deviating from −0.526; z_acc deviating from 0.630; an
amplitude/size/growth observable written as a pure function of the info objects.
Classification: ΩΛ, Ωm, ratio PREDICTION (information-derived, observed); q₀/z_acc
values DERIVED (closures via hosted FRW) with form CORRESPONDENCE; H₀/σ₈/BAO/growth/
lensing/clustering BOUNDARY/calibration (no direct relation); the finite-family
structure DERIVED. Refines NP_019: the pair is not alone — it generates a small closed
observable set. No new primitive; canonical AT unchanged.

ResearchY-QG_013 (Three-Family Origin Audit, COMPLETE, tests 6/6 PASSED): why must the
observable sector consist of exactly three families? Verdict: the 3-family WINDOW is a
CONFIRMED BOUNDARY — NOT reducible to distinguishability, count structure, or
information density — but it is ANCHORED by the observed cosmology (ΩΛ = 0.6839).
FAMILY-COUNT STRUCTURE: family count = floor(log₂(span))+1 is DERIVED (D_016);
span(96) = 6.4025 is DERIVED (D_028); N=96 = 3·2⁵ is the octave rung (D_020/D_031);
the family-count VALUE 3 at N=96 is DERIVED (two-level rule, D_040); the 3-family
WINDOW (span ∈ [4,8)) is the BOUNDARY input. REMOVE THE 3-FAMILY ASSUMPTION — THE
PAIRING TEST: the pairing-complete octave rungs 3·2^k are N=48 (2 families, span
3.240), N=96 (3, 6.403), N=192 (4, 12.779), N=384 (5, 25.537) — ALL have λ=12 mult 5
(complete pairing, verified exactly via the K=6 ring spectrum), so the Z2-paired
complex sector (D_020) does NOT select 3 families (only the non-rungs N=64/128 fail
pairing with λ=12 mult 1). INFORMATION DENSITY: I_occ (KL of the generation-share
occupancy to uniform) is strictly MONOTONE increasing in N — 0.524 (N=48) → 0.630
(N=64) → 0.7513 (N=96) → 0.820 (N=128) → 1.013 (N=192) — there is NO information-
theoretic extremum at N=96; 3 families does not minimize, maximize, or stationarize
I_occ. THE OBSERVED-COSMOLOGY SELECTOR: ΩΛ = I_occ/ln K (ln K = 1.0986, K ≈ 3) is
reproduced EXACTLY only by N=96 — ΩΛ(96) = 0.7513/1.0986 = 0.6839 (0.0% deviation,
observed 0.12%); N=48 → 0.4773 (−30.2%), N=192 → 0.8153 (+19.2%), N=384 → 0.8945
(+30.8%). WHAT FIRST FAILS WHEN FAMILY COUNT ≠ 3: the OBSERVED COSMOLOGY — the
predicted dark-energy fraction deviates by 19–31%, far beyond the 0.12% observational
precision (the pairing-complete structure survives, the state capacity grows, but the
information-density chain ΩΛ = I_occ/ln K deviates irrecoverably). DOES 3 EXTREMIZE
AN INFORMATION QUANTITY? NO — I_occ is monotone increasing; 3 is selected by the
OBSERVED ΩΛ = 0.6839, the one pairing-complete octave rung reproducing it. BOUNDARY
REDUCTION TEST: reducible to the Z2-paired complex sector — NO (N=48/192/384 all
pair); to count structure — NO (count same for every rung); to information density —
NO (I_occ monotone); to the observed cosmology — YES as an ANCHOR, not a derivation
(N=96 is the unique pairing-complete rung reproducing the observed ΩΛ). The 3-family
window remains a BOUNDARY input (D_020/D_040) now ANCHORED by the information-
cosmology chain. Counterexamples: 'pairing forces 3 families' fails (N=48/192/384
pair); 'I_occ is extremized at N=96' fails (monotone); 'the window is derived from
information' fails (selected by OBSERVED ΩΛ); 'N=64/128 are alternative 3-family
sectors' fails (they fail pairing); 'removing 3 families changes nothing' fails
(every other rung off by 19–31%). Falsification: a derivation of the window from a
deeper internal principle; a pairing-complete rung ≠ 96 reproducing ΩΛ = 0.6839; a
measured ΩΛ ≠ I_occ(96)/ln K; a non-rung sector with decreasing I_occ. Classification:
family-count VALUE 3 DERIVED (from span(96), D_016/D_046 P8); the 3-family WINDOW
BOUNDARY (observable-sector input, confirmed D_020/D_040); N=96 DERIVED (octave
rung); I_occ DERIVED (monotone, no extremum at 3); the observed ΩΛ = 0.6839 BOUNDARY
(observed anchor); cosmology→family coupling DERIVED (ΩΛ selects the rung). No new
primitive; canonical AT unchanged.

ResearchY-QG_014 (Cosmological Selection Audit, COMPLETE, tests 5/5 PASSED): is the
observed cosmology itself a selector of the observable sector? Verdict: the observed
cosmology is a CONSTRAINT — not a full selection mechanism and not a coincidence —
that selects the 3-family sector among pairing-complete candidates. THE MEASUREMENT
(family counts 2–5, pairing-complete octave rungs): 2 families (N=48) — I_occ =
0.5244, ΩΛ = 0.4773, Ωm = 0.5227, q₀ = −0.2160, z_acc = 0.2224 (−20.7% off
observed); 3 families (N=96) — I_occ = 0.7513, ΩΛ = 0.6839, Ωm = 0.3161, q₀ =
−0.5258, z_acc = 0.6295 (0.0% — EXACT match to all four observables); 4 families
(N=192) — ΩΛ = 0.8153 (+13.1%); 5 families (N=384) — ΩΛ = 0.8945 (+21.1%). WHICH
FAMILY COUNT BEST MATCHES: 3 families (N=96) — uniquely, exactly, and to all four
observables (ΩΛ, Ωm, q₀, z_acc all 0.0% deviation within the 0.12% precision); every
other pairing-complete sector deviates by 13–21%. THE CLASSIFICATION: SELECTION
(cosmology determines the sector) — CONDITIONAL YES (given the observed ΩΛ, the
sector is fixed) but NOT full causal selection (the observed ΩΛ = 0.6839 is an
input, not derived); CONSTRAINT (cosmology rules out alternatives) — YES,
unconditionally (2/4/5 families falsified by 13–21% deviations); COINCIDENCE
(accidental match) — NO (I_occ(96) = 0.7513 is EXACTLY the KL of [4,4,87] to
uniform, QG228 — a deterministic chain). PRIMARY CLASSIFICATION: CONSTRAINT. THE
DIRECTION OF EXPLANATION: FORWARD (theory → observation) — DERIVED and exact (N=96 →
[4,4,87] → I_occ = 0.7513 → ΩΛ = I_occ/ln K = 0.6839 → observed, then Ωm, q₀, z_acc
follow, QG_012); BACKWARD (observation → sector) — a CONDITIONAL SELECTOR, i.e., a
constraint (observed ΩΛ → only pairing-complete rung matching is N=96 → 3 families,
QG_013). The theory derives the cosmology from the sector (forward, DERIVED); the
cosmology filters the sector from among candidates (backward, CONDITIONAL — not a
causal mechanism, not a coincidence). Counterexamples: 'cosmology fully selects the
family count' fails (the observed ΩΛ is an input — conditional, not causal);
'cosmology is only a weak filter' fails (13–21% deviations — sharp, unconditional);
'the ΩΛ match is a numerical coincidence' fails (I_occ(96) = 0.7513 is exactly the KL
of [4,4,87]); '2 or 4 families could match a different observation' fails (no
pairing-complete sector other than 96 reproduces any of the four observables);
'cosmology plays no role in the sector' fails (the observed fractions uniquely
identify the 3-family sector). Falsification: a pairing-complete sector ≠ 96
reproducing ΩΛ = 0.6839 (or Ωm, q₀, z_acc); a different occupancy than [4,4,87]
giving I_occ = 0.7513 with the same ΩΛ; a mechanism deriving the observed ΩΛ from a
deeper principle (upgrading the constraint to full selection); a measured universe
with ΩΛ = 0.4773, 0.8153, or 0.8945. Classification: the 3-family sector's match to
the observed cosmology CONSTRAINT (filters all alternatives; 13–21% deviations
falsified); the family count as a causal consequence of cosmology REFUTED (partial/
conditional only — the observed ΩΛ is an input); the ΩΛ match being a coincidence
REFUTED (I_occ(96) = 0.7513 is the exact KL of [4,4,87], QG228); the forward
prediction (theory → cosmology) DERIVED (N=96 → I_occ → ΩΛ = 0.6839, exact); the
observed ΩΛ as the input anchor BOUNDARY (QG_013). No new primitive; canonical AT
unchanged.

ResearchY-QG_015 (Observable World Audit, COMPLETE, tests 6/6 PASSED): why does an
observable world exist? Does a distinguishable universe NECESSARILY become
observable? Verdict: NO — a distinguishable universe does NOT necessarily become
observable. Observability is EMERGENT (the actualization readout) under a BOUNDARY
input (the observable sector), not a logical consequence of Difference. DEFINITIONS:
observable world = a world whose distinguishable states are READ by actualization
events; distinguishability = the state space produced by Difference (D_039);
observation = an actualization event reading both quadratures of a state (M_001);
realization = the resolution of WHICH state is actual (M_001/M_003); actualization =
the discrete-step evolution of the phase (D_041). COMPARE U1 (distinguishable,
unobserved) vs U2 (distinguishable, observed): U1 — the 95 states EXIST, geometry
g = ρ^(2/d)η EXISTS (from ρ), information I_occ = 0.7513 nats EXISTS (KL of ρ),
cosmology ΩΛ = 0.6839 EXISTS (from info density), but NO measurement event and NO
outcome realization; U2 — all the same PLUS the measurement event (M_001 reads both
quadratures), outcome realization, phase pinning (M_002), and observer knowledge
(M_006). U1 IS FULLY CONSISTENT: everything DERIVED from the state space exists
without any observation; observation adds only RESOLUTION (which state is realized),
phase pinning, and observer access — it does NOT create the states, ρ, geometry,
information, or cosmology (M_005: reveal, not create; M_006: the observer changes
only the epistemic layer). DOES DIFFERENCE ALONE PRODUCE OBSERVATION? NO — Difference
→ distinguishability (D_039) produces the STATES; observation requires an
actualization event that READS them (M_001) — a separate act. DOES ACTUALIZATION
NECESSARILY PRODUCE OBSERVABLE OUTCOMES? PARTIAL — Actualization advances the phase
per tick (Δθ = 2πk/N, D_041), producing EVOLUTION; an 'observable outcome' is a READ
of the state (M_001); Actualization necessarily produces ticks but not necessarily a
read (U1: evolution without observation). WHAT DISAPPEARS IF OBSERVABILITY IS
REMOVED? information — NO (pre-existing, M_005); geometry — NO (from ρ); MEASUREMENT
— YES (the measurement event IS observability, M_001); cosmology — NO (from info
density). Only measurement/realization disappears; physics survives. MINIMAL
PRINCIPLE GENERATING OBSERVABLE REALITY: the OBSERVABLE-SECTOR requirement (D_020) —
the Z2-paired complex sector, the requirement that every frequency carry both
quadratures ([magnitude, phase], D_036/D_037) so states are READABLE. DETERMINATION:
A) observability derived — NO (U1 is consistent); B) observability EMERGENT — YES
(the measurement event is the actualization readout, M_001, EMERGENT; observability =
complete state reconstruction is EMERGENT, D_037); C) observability BOUNDARY — YES at
the input level (the Z2-paired complex sector requirement, D_020). TWO-LEVEL
CLASSIFICATION: observability as a CAPABILITY is EMERGENT (the readout, M_001;
reconstruction, D_037); the OBSERVABLE-SECTOR REQUIREMENT (that states be
readable/complex) is BOUNDARY (D_020). PROVE/REFUTE: a distinguishable universe must
become observable — REFUTED (U1 is fully consistent: all physics exists unobserved;
becoming observable requires the observable-sector boundary input). Counterexamples:
'Difference alone produces observation' fails (U1 — states exist with no read;
observation needs M_001); 'a distinguishable universe must be observed' fails (U1 is
consistent: geometry/info/cosmology survive unobserved); 'observability is ontic'
fails (observation adds realization + access; the ontic layer is observation-
independent, M_006); 'removing observability destroys physics' fails (only
measurement/realization disappears); 'Actualization necessarily produces observable
outcomes' fails (ticks D_041 ≠ reads M_001). Falsification: an observable or
structure that requires a read to exist (would refute U1); an ontic change
(state/geometry/info) produced by the read; a derivation of complex-readability from
Difference alone; an observable world without the actualization readout.
Classification: geometry/information/cosmology DERIVED (from ρ — observation-
independent); the measurement event EMERGENT (the actualization readout, M_001);
observability as reconstruction EMERGENT (complete state readout, D_037); the
observable-sector requirement BOUNDARY (Z2-paired complex sector, D_020);
'a distinguishable universe must become observable' REFUTED (U1 is consistent). No
new primitive; canonical AT unchanged.

ResearchY-QG_016 (Tick Discreteness Origin Audit, COMPLETE, tests 6/6 PASSED): why
must the actualization tick be discrete? Is tick discreteness a primitive boundary or
a consequence of Difference? Verdict: tick discreteness is a BOUNDARY — Difference
implies discrete STATES (a discrete set, DERIVED) but NOT discrete EVENTS (the
stepwise dynamics). THE TWO DISCRETENESSES: state-space discreteness = the 95
distinct states (a discrete SET, membership discreteness, D_039) — DERIVED from
Difference; dynamics discreteness = the TICK (Δθ = 2πk/N per step, the phase
advance, D_041) — a DYNAMIC parameter, the step of the advance. Difference produces
the states; nothing in 'distinguishing states' forces the advance to be stepwise vs
continuous. COMPARE DISCRETE TICK vs CONTINUOUS ACTUALIZATION: discrete — θ_m =
θ₀ + m·2πk/N (lattice of N/gcd(N,k) values: k=1→96, k=16→6, k=48→2, k=95→96), one
event = one step = one outcome (M_001), finite info log₂(95) = 6.57 bits (M_004),
normalization Σρ = 1, count conservation, phase advance DERIVED from the spectrum
(D_041), AT-P042 fundamental clock; continuous — θ(t) continuous (continuum of
values), no fundamental step, finite info only if sampled discretely, normalization/
count conservation still hold (count is over discrete states), phase advance
DECOUPLED from the spectrum (free continuous parameter), AT-P042 demoted to a
sampling artifact. THE DECISIVE FACT (M_010): continuous actualization with rate
ω = 2πk/(N·τ) reproduces AT-P042 EXACTLY at every tick-sampled time — phase,
recurrence (N/gcd(N,k)), interference, and finite-state orbits are identical; only
the sub-tick phase differs. OBSERVABILITY DOES NOT FORCE DISCRETE DYNAMICS: finite
events (QG_010/QG_011) require discrete READS, which can sample a continuous
evolution. FIRST INCONSISTENCY OF CONTINUOUS ACTUALIZATION: STRUCTURAL, not
observational — the phase advance Δθ = 2πk/N loses its spectral derivation (the step
was derived from the mode index k and state count N, D_041) and becomes a free
continuous parameter (a new boundary), while AT-P042 is demoted from the fundamental
clock to a sampling artifact. EFFECTS: observability survives (discrete sampling,
M_010); information gain survives (discrete read); normalization survives (discrete
count); count conservation survives (normalizer S); phase evolution works
continuously — but the DERIVED structure of the phase advance and the fundamental
status of the tick break. MINIMAL PRINCIPLE FORCING A DISCRETE EVENT: none forces
discrete DYNAMICS — Difference forces a discrete SET (membership), observability
forces finite EVENTS (discrete reads) but not discrete dynamics (M_010 equivalence),
the count structure (ρ_k = μ^k/S, QG194) is over states not over time. The discrete
event is forced by the observable-sector requirement (D_020) TOGETHER with the
canonical phase advance (D_041): the states are discrete (Difference), and the phase
advances in the derived spectral step. PROVE/REFUTE: Difference implies discrete
events — REFUTED (as a claim about dynamics): Difference implies discrete STATES (the
95-state set, D_039) but NOT discrete EVENTS; the tick (Δθ = 2πk/N) is the DYNAMIC
step of the phase advance — a property of HOW actualization evolves, not of WHAT the
states are; a continuous actualization is observationally equivalent at all
tick-sampled times (M_010). BOUNDARY REDUCTION TEST: tick discreteness → Difference —
NO (Difference produces a discrete SET, not a discrete advance); → observability — NO
(continuous + discrete sampling is observationally equivalent, M_010); → count
structure — NO (the count is over states, not over time); the step VALUE Δθ = 2πk/N
→ spectrum — YES, DERIVED (D_041). Counterexamples: 'Difference implies discrete
events' fails (produces a discrete SET; the advance could be continuous, M_010
sampling equivalent); 'observability forces discrete dynamics' fails (finite events
need discrete READS, not discrete evolution); 'continuous actualization breaks
measurement' fails (discrete sampling of continuous evolution gives the same
observable physics); 'the tick is derived from Difference' fails (the tick is a
dynamic parameter; Difference fixes the states, not the advance); 'the step value is
boundary too' fails (Δθ = 2πk/N IS derived from the spectrum, D_041 — only the
discreteness is input). Falsification: a derivation of stepwise dynamics from
Difference alone; an observable that requires a continuous evolution to exist; an
observable differing between discrete and continuous evolution at sampled times; a
phase advance not equal to 2πk/N from the spectrum. Classification: state-space
discreteness (the set) DERIVED (Difference → distinguishability, D_039); finite
events EMERGENT (observable-sector requirement, QG_010/QG_011); tick DISCRETENESS
(stepwise dynamics) BOUNDARY (canonical input, D_041); step VALUE Δθ = 2πk/N DERIVED
(from the spectrum, D_041); AT-P042 (discrete lattice) STRUCTURAL PREDICTION (M_009),
boundary-supported. The tick remains the deepest structural boundary — now precisely
located: its DISCRETENESS is input, its SIZE is derived. No new primitive; canonical
AT unchanged.

ResearchY-QG_017 (Distinguishability Cosmology Extension Audit, COMPLETE, tests 6/6
PASSED): if ΩΛ comes from distinguishability, what else must follow? (Submitted as
QG_015/QG_016, both taken; registered as QG_017 per the permanent-ID rule.) Verdict:
ΩΛ is NOT an isolated success — it is the first member of a FINITE distinguishability
cosmology family: ΩΛ, Ωm, ratio, q₀, z_acc. No full distinguishability cosmology.
THE SOURCE: the count structure {I_occ = 0.7513 nats, ln K = 1.0986, ρ}, with the
realized entropy H = ln K − I_occ = 0.3473 (the matter side). THE OBSERVED FAMILY:
ΩΛ = I_occ/ln K = 0.6839 (observed 0.12%), Ωm = (ln K − I_occ)/ln K = 0.3161
(observed 0.26%), the ratio ΩΛ/Ωm = I_occ/(ln K − I_occ) = 2.1636, and — via the
HOSTED FRW relations — the current deceleration q₀ = Ωm/2 − ΩΛ = −0.5258 and the
turnaround redshift z_acc = (2ΩΛ/Ωm)^(1/3) − 1 = 0.6295. CANDIDATE EXTENSIONS (must
they depend on the same source?): ΩΛ/Ωm/q₀/z_acc — YES (all deterministic functions
of the info objects); H₀ — NO (dimensionful, needs an anchor v/clock); σ₈ — NO
(needs the primordial amplitude A_s); BAO scale — NO (needs the sound horizon Ωb/Ωr);
structure growth — NO (needs A_s, n_s, growth index); weak lensing S₈ — NO (inherits
σ₈); horizon size — NO (needs H₀); clustering amplitude — NO (needs σ₈, P(k)).
CLOSURE RELATIONS (the family is algebraically CLOSED): the entropy identity
I_occ + H = ln K (0.7513 + 0.3473 = 1.0986); the completeness ΩΛ + Ωm = 1; the
entropy-fraction reading Ωm = H/ln K (the pair partitions the state-space size); the
ratio I_occ/H = 2.1636; q₀ = Ωm/2 − ΩΛ; and 1 + z_acc = (2ΩΛ/Ωm)^(1/3) = 1.6295.
Given {I_occ, ln K}, all five observables are fixed — no third independent
information number exists. DETERMINATION: A) ΩΛ uniquely privileged — PARTIAL
(primary but not alone); B) finite cosmology family — YES (the complete answer); C)
full distinguishability cosmology — NO (H₀/σ₈/BAO/growth/lensing/horizon/clustering
need non-information inputs). STRONGEST NEXT PREDICTION BEYOND ΩΛ: the q₀/z_acc
closure — q₀ = −0.5258 (supernova Hubble diagram, falsified by deviation from
−0.526) and z_acc = 0.6295 (expansion-history reconstruction, falsified by deviation
from 0.630); their FORM is CORRESPONDENCE (hosted GR kinematics), their VALUES are
DERIVED from the information fractions. Counterexamples: 'ΩΛ is an isolated success'
fails (the pair and its closures all follow from the same source); 'full
distinguishability cosmology' fails (H₀/σ₈/BAO/growth/lensing/horizon/clustering
need non-information inputs); 'σ₈ is info-derived' fails (needs A_s); 'BAO scale is
info-derived' fails (sound horizon needs Ωb/Ωr); 'a third independent info number
exists' fails (the family is algebraically closed); 'removing distinguishability
keeps some members' fails (I_occ undefined without the state space — the whole
family vanishes). Falsification: ΩΛ deviation beyond 0.12%; Ωm inconsistent with
(ln K − I_occ)/ln K; ratio deviating from I_occ/(ln K − I_occ); q₀ deviating from
−0.526; z_acc deviating from 0.630; an amplitude/size/growth observable as a pure
function of {I_occ, ln K, ρ}. Classification: ΩΛ/Ωm/ratio PREDICTION (information-
derived, observed); q₀/z_acc values DERIVED (closures of the pair, hosted FRW form)
with form CORRESPONDENCE; H₀/σ₈/BAO/growth/lensing/horizon/clustering BOUNDARY (need
non-information inputs); the finite-family structure DERIVED. No new primitive;
canonical AT unchanged.

ResearchY-QG_018 (Information-Cosmology Closure Audit, COMPLETE, tests 6/6 PASSED,
V2.3 closure block task 1): do ΩΛ, Ωm, I_occ, KL(ρ‖uniform), finite observability,
and actualization-information form a mathematically CLOSED chain? Verdict: YES —
closure score 90% (9/10) within the canonical finite-N regime. THE DEPENDENCY DAG:
Difference [BOUNDARY] → Distinguishability (D_039, DERIVED) → finite state space
(QG_008, BOUNDARY) → Actualization/tick (QG_011, BOUNDARY) → spectrum λ_k =
2−2cos(2πk/N), N=96 (D_041, DERIVED) → count density ρ_k = count_k/total, Σρ = 1
(QG194/QG216, DERIVED) → I_occ = KL(ρ‖uniform) = 0.7513 (QG228, DERIVED; uniform
reference BOUNDARY QG_009) → ln K = 1.0986 (QG234/QG227, DERIVED) → ΩΛ = I_occ/ln K
= 0.6839 (DERIVED, observed), Ωm = (ln K − I_occ)/ln K = 0.3161 (DERIVED, observed),
q₀ = Ωm/2 − ΩΛ = −0.5258 (DERIVED, hosted FRW form), z_acc = (2ΩΛ/Ωm)^(1/3) − 1 =
0.6295 (DERIVED, hosted FRW form); finite observability (QG_010, EMERGENT), finite
events (QG_011, EMERGENT), tick discreteness (QG_016, BOUNDARY). REMAINING IMPORTED
ASSUMPTIONS (the exact boundary set, 8 items): {Difference, η} (B1), {Z2-paired
complex sector} (B2), {3 octave families} (B3), {SU(2) gauge + j=1/2} (B4), {v, m_e}
(B5), {state-space finiteness} (B6, QG_008), {uniform reference} (B7, QG_009), {tick
discreteness} (B8, QG_016). Every other link is DERIVED or EMERGENT; the chain
introduces NO new assumption. HIDDEN-CIRCULARITY SEARCH (verified): the chain is
acyclic (no link points backward); ln K = I_occ/ΩΛ is a bookkeeping identity, NOT
circular — ln K is independently fixed by the initial uniform state (QG227, K ≈ 3);
ΩΛ is observed, not fed back into ρ's construction (QG_013/014: constraint, not
selection). ALTERNATIVE-INFORMATION-MEASURE TEST (verified): ONLY KL reproduces the
observed ΩΛ = 0.6839 — squared Hellinger 0.4211 → ΩΛ 0.3833, total variation (½)
0.5825 → 0.5302, chi-squared 1.5266 → 1.3896 all fail. The KL choice is EMERGENT
(the unique measure consistent with observation among the tested family), not forced
by the chain and not arbitrary. FINITE-N vs CONVERGENT-INFINITE-N: finite N=96 — KL
defined, ΩΛ = 0.6839, CLOSED; convergent-infinite N (geometric ρ) — normalizes
(Σ(1−r)r^k = 1) and has finite entropy (QG_009) but NO normalized uniform reference,
so I_occ/ΩΛ are ILL-DEFINED — the closure FAILS at the uniform reference (requires
B6/B7). CLOSURE SCORE: 9/10 = 90% (acyclic DAG ✓, no circularity ✓, only canonical
boundaries ✓, KL unique match ✓, finite-N closure ✓, infinite-N closure ✗ — excluded
by B6/B7, observed ΩΛ reproduced ✓, observed Ωm reproduced ✓, q₀/z_acc consistent ✓,
measurement/information conservation ✓). THE EXACT REMAINING BOUNDARY SET: the
canonical eight (B1–B8) including the three structural (state-space finiteness
QG_008, uniform reference QG_009, tick discreteness QG_016). Counterexamples: 'the
chain has hidden circularity' fails (verified acyclic; ln K fixed by QG227); 'any
divergence gives ΩΛ' fails (Hellinger/TV/χ² all fail); 'the closure survives infinite
N' fails (no normalized uniform measure, QG_009); 'the KL choice is arbitrary' fails
(unique match — EMERGENT); 'a new boundary is introduced' fails (only canonical
B1–B8). Falsification: a missing link, a new boundary, or a hidden circular
dependency; another f-divergence reproducing ΩΛ on the canonical ρ; a normalized
uniform measure on a countable infinite set; a derived quantity requiring a
non-canonical input. Classification: chain closure DERIVED (acyclic, complete); the
KL choice EMERGENT (unique match); ΩΛ/Ωm/ratio/q₀/z_acc DERIVED (observed/
consistent); the three structural boundaries BOUNDARY (QG_008/009/016); the canonical
input boundaries BOUNDARY (B1–B5); closure score 90%. No new primitive; canonical AT
unchanged.

ResearchY-NP_023 (O(2) Mirror Search, COMPLETE, tests 6/6 PASSED, V2.3 closure block
task 2; hostile audit): does the AT spectral framework contain an OVERLOOKED O(2)
symmetry, mirror branch, or degeneracy? Verdict: the D96 spectrum carries
per-frequency O(2) doublets, and the FULL degeneracy structure is {mirror pairs} ∪
{octave-ladder blocks} — both structural, symmetry-protected, with ZERO accidental
degeneracies outside them. THE D96 SPECTRUM (verified): N=96 (K=6 ring, λ_k = Σ_s
2(1−cos(2πks/N))), 95 positive modes, 44 DISTINCT eigenvalues, max multiplicity 6,
ALL eigenvalues with mult ≥ 2 (complete pairing, D_035), ZERO with mult 1, ZERO Z2
mirror mismatches (λ_k = λ_{N−k} exact). THE SEARCH RESULTS: O(2)-like continuous
rotations — the only continuous rotation is WITHIN each 2D {cos, sin} eigenspace
(the per-mode phase SO(2)); NO rotation MIXES distinct frequencies. HIDDEN MIRROR
SECTORS — every eigenvalue class is exactly the mirror pair {k, N−k}; no hidden
sector. ACCIDENTAL DEGENERACIES — ZERO outside the structural blocks: the 20
non-mirror degenerate pairs are ALL in the octave-ladder blocks (λ=12 five-fold
{16,32,48,64,80}; λ=14 six-fold {8,24,40,56,72,88}), produced by the octave
structure (D_030) and giving the canonical [4,4,87] multiplicity. PARITY/REFLECTION
SUBGROUPS — the Z2 generator is k ↔ N−k; the automorphism group (k → ak mod N, a ∈
units(96), 32 elements) acts by DISCRETE permutations within gcd classes (11
classes: gcd 1→32 modes, 2→16, 3→16, 4→8, 6→8, 8→4, 12→4, 16→2, 24→2, 32→2, 48→1)
and NEVER mixes classes — a discrete group action, NOT a continuous O(2). DUAL
DESCRIPTIONS — the {cos, sin}_k and {cos, sin}_{N−k} pairs span the SAME 2D
eigenspace (linearly related), i.e., the SAME O(2) irrep, not two distinct
descriptions. REPRESENTATION DECOMPOSITION (verified): 95 = 42 × 2D O(2)-irreps (84
modes, the mirror pairs) + the degenerate octave blocks (5-fold λ=12, 6-fold λ=14);
every mode sits in a 2D {cos, sin} eigenspace; the self-conjugate k=48 (sin(πn)=0)
sits in the 5-fold λ=12 block, satisfying complex observability (D_035).
PERTURBATIVE STABILITY (verified): a reflection-preserving perturbation keeps every
mirror pair degenerate (max split ~1e−14); only a reflection-BREAKING perturbation
splits a pair — the degeneracy is symmetry-protected, generic in the ring class.
DETERMINATION: full symmetry (the Z2 mirror + octave blocks are the FULL degeneracy
structure) — YES (DERIVED); remnant of a larger O(2) — NO (there is no larger O(2)
to be a remnant of; zero accidental degeneracies, no continuous inter-mode
rotation); emergent approximation — NO (|Δλ| = 0 exact); accidental — NO
(symmetry-forced, perturbatively protected). STRONGEST POSITIVE EVIDENCE: the exact,
symmetry-protected per-frequency O(2) doublet structure (42 × 2D irreps; |Δλ| = 0 to
machine precision). STRONGEST NO-GO THEOREM: there is NO degeneracy outside the
structural {mirror pairs} ∪ {octave-ladder blocks} — zero accidental degeneracies, no
continuous inter-mode rotation, automorphisms confined to gcd classes; hence no
larger O(2) exists and the Z2 is not a remnant. EXACT FRONTIER RECOMMENDATION FOR
V2.3: (1) primary — measure a C96-ring resonance spectrum to test the exact
mirror-pair degeneracy (NP_015/016; |Δλ| > 0, a missing pair, or a triplet falsifies
it); (2) secondary — extend the representation decomposition to higher-K rings (K =
8, 10, 12) to test whether the 'full-symmetry O(2) doublet + no larger O(2)'
structure is generic or N/K-specific; (3) tertiary — verify the octave-ladder blocks
are likewise symmetry-protected under the octave-doubling automorphism.
Counterexamples: 'a larger O(2) mixes frequencies' fails (zero accidental
degeneracies, no continuous inter-mode rotation); 'the Z2 is a remnant of a broken
O(2)' fails (no larger O(2) present); 'the mirror pairs are an approximation' fails
(|Δλ| = 0 exactly, perturbatively protected); 'the degeneracy is accidental' fails
(symmetry-forced by the ring reflection + octave structure); 'an accidental
degeneracy exists' fails (every non-mirror pair is octave-structural). Falsification:
a larger O(2) with a continuous inter-mode rotation; an accidental degeneracy
OUTSIDE the mirror + octave classes; a reflection-preserving perturbation that splits
a pair; an octave-ladder partner with a different eigenvalue; a measured C96-ring
spectrum WITHOUT exact mirror pairs; a mode with multiplicity 1 (isolated singlet).
Classification: per-frequency O(2) doublets DERIVED (spectral, D_021/D_035); the Z2
mirror as a full-symmetry component DERIVED (exact, symmetry-protected); the
octave-ladder blocks (λ=12 5-fold, λ=14 6-fold) DERIVED (octave structure, D_030);
accidental degeneracies FALSIFIED (every non-mirror pair is octave-structural); a
larger O(2) mixing frequencies FALSIFIED (no continuous inter-mode rotation);
remnant-of-larger-O(2) reading FALSIFIED; emergent-approximation reading FALSIFIED
(exact); accidental reading FALSIFIED (symmetry-forced). No new primitive; canonical
AT unchanged.

ResearchY-NP_024 (O(2) Mirror-Pair Physical Prediction Audit, COMPLETE, tests 7/7
PASSED): what observable physical consequence follows uniquely from the exact D96
symmetry O(2)_D96 = {42 mirror-pair irreps} ∪ {λ=12 five-fold block} ∪ {λ=14
six-fold block}? Verdict: the strongest falsifiable observable is the exact,
coupling-independent ratio ω(√12)/ω(√14) = √(6/7) = 0.92582 together with the
5-fold/6-fold resonance multiplicities — a C96-ring resonator must show one 5-fold
peak at ω = √12 = 3.4641, one 6-fold peak at ω = √14 = 3.7417, and the exact ratio.
This EXCEEDS NP_022's mirror-pair prediction. THE EXACT DEGENERACY ALGEBRA (verified):
95 modes = 42×2 (mirror-pair 2D irreps, {k, N−k}) + 5 (λ=12 block {16,32,48,64,80}) +
6 (λ=14 block {8,24,40,56,72,88}); the mirror pairs have ω_k/ω_{N−k} = 1 exactly and
the octave ratio ω₂/ω₁ = 1.9734 ≈ 2. PHYSICAL CONSEQUENCES: protected resonance
splitting (any reflection-preserving perturbation keeps all degeneracies exact,
~1e−14); selection rules/forbidden transitions (a resonance at ω=√12 has 5 orthogonal
modes, at ω=√14 has 6 — the multiplicity IS the observable); paired excitation
spectra (exciting k=16 co-excites {32,48,64,80} at the same frequency; k=8
co-excites the 6-fold block); and the observable frequency ratios (ω_k/ω_{N−k} = 1;
ω₂/ω₁ ≈ 2; ω(√12)/ω(√14) = √(12/14) = √(6/7) = 0.925820 EXACT and coupling-
independent — the couplings cancel in the ratio, which follows purely from the
integer eigenvalues λ=12 and λ=14). CORRESPONDENCE FILTER (verified): the mirror-pair
degeneracy is CORRESPONDENCE — generic rings, Fourier cos/sin spectra, QM central
potentials (m↔−m), and time-reversal phonons (k↔−k) all have such pairs; the
5-fold/6-fold multiplicities and the √(6/7) ratio are PREDICTION — a generic
single-coupling ring has all-distinct 2-fold eigenvalues (one singlet at the
self-conjugate k=48) with no cross-block relation, so the K=6 structure is REQUIRED.
THE STRONGEST EXPERIMENTAL DISCRIMINATOR: the C96-ring spectrum must show exactly one
5-fold resonance at ω=√12, one 6-fold at ω=√14, and the exact √(6/7) ratio. EXCEEDS
NP_022? YES — NP_022 ranked the mirror-pair degeneracy as PREDICTION #2 (18/20), but
the correspondence filter shows mirror pairs are generic (CORRESPONDENCE); NP_024
adds the 5-fold/6-fold multiplicities and the coupling-independent √(6/7) ratio,
which are more specific, uniquely K=6, and not stated by NP_022. The strongest single
prediction of the exact O(2)_D96 structure is therefore the exact inter-block
frequency ratio, not the mirror pairs. Counterexamples: 'the mirror pairs are unique
to AT' fails (generic rings, QM m↔−m, phonons k↔−k all have k↔−k degeneracy); 'a
generic ring reproduces the 5-fold/6-fold' fails (single-coupling rings have
all-distinct 2-fold eigenvalues, verified); 'the √(6/7) ratio is coupling-dependent'
fails (the couplings cancel — it is exact, √(12/14) = 0.925820); 'any degenerate
spectrum shows this' fails (the blocks require the specific K=6 ring); 'the
discriminator is weaker than NP_022' fails (multiplicity + ratio content is more
specific). Falsification: a C96-ring spectrum without a 5-fold peak at √12; without
a 6-fold peak at √14; a measured ratio deviating from √(6/7); a rotationally
symmetric system without k↔−k pairs; a single-coupling ring producing a 5-fold or
6-fold eigenvalue. Classification: mirror-pair degeneracy CORRESPONDENCE (generic:
rings, QM, phonons); the λ=12 five-fold and λ=14 six-fold blocks DERIVED (octave
structure, D_030); the exact ratio ω(√12)/ω(√14) = √(6/7) PREDICTION (coupling-
independent, uniquely K=6); the multiplicities PREDICTION (observable peak degeneracy
5 and 6); generic-ring reproduction FALSIFIED (single-coupling rings show 2-fold
only). Refines NP_022: the mirror pairs are downgraded to CORRESPONDENCE; the
octave-block structure adds the stronger √(6/7) prediction. No new primitive;
canonical AT unchanged.

ResearchY-NP_025 (K=6 Uniqueness Audit, COMPLETE, tests 6/6 PASSED): is the exact
prediction ω(√12)/ω(√14) = √(6/7) unique to K=6, or does the same protected
inter-block structure appear in other circulant rings C_N(±1..±K)? Verdict: NOT
unique to K=6 — √(6/7) is the K=6 member of the universal K-family √(K/(K+1)), an
N-INDEPENDENT protected inter-block ratio of every circulant ring C_N(±1..±K) with
K ≥ 2 (whenever the non-doublet blocks appear). THE SCAN (K=1..12 at N=96, verified):
every K ≥ 2 except K=10 produces non-doublet blocks; each ring's protected
inter-block ratio equals √(K/(K+1)) EXACTLY — K=2: √(2/3) = 0.81650, K=3: √(3/4) =
0.86603, K=4: √(4/5) = 0.89443, K=5: √(5/6) = 0.91287, K=6: √(6/7) = 0.92582, K=7:
√(7/8) = 0.93541, K=8: √(8/9) = 0.94281, K=9: √(9/10) = 0.94868, K=11: √(11/12) =
0.95743, K=12: √(12/13) = 0.96077 (K=1 is generic all-2-fold; K=10 at N=96 is
size-suppressed all-2-fold but the family exists at other N). N-DEPENDENCE (verified):
the ratio is a PURE K-PROPERTY, N-INDEPENDENT — K=6 gives √(6/7) at N=48, 96, and 192
whenever the blocks appear (blocks absent at N=64/128); the multiplicities are
N/K-DEPENDENT (K=6 gives (6,5) at N=96 but the blocks are absent at N=64/128).
MULTIPLICITY STRUCTURE (verified): the top multiplicities vary with (N,K) — K=6 gives
(6,5); K=7 gives (8,7); K=8 gives (10,9) at N=96. DETERMINATION: A) unique to K=6 —
NO (every K ≥ 2 has the protected structure); B) family of K-values — YES (the
universal √(K/(K+1)) family); C) generic phenomenon — PARTIAL (the ratio family is
generic to ALL circulant rings; the specific value √(6/7) is the K=6 member).
STRONGER DISCRIMINATOR: the ratio √(K/(K+1)) is strictly increasing (INJECTIVE) in K,
so the measured ratio UNIQUELY identifies K (the coupling order) — verified 0.81650
→ 0.96077 monotone; the multiplicities are the N-discriminator (N/K-dependent). Note:
multiplicities alone do NOT distinguish K (K=5 and K=6 share the (6,5) pair at N=96);
the ratio does (√(5/6) ≠ √(6/7)). The full observable signature is {ratio,
multiplicities} which identifies (N,K) uniquely. ELEVATION: √(6/7) is not a
D96-specific prediction but a general K-family law — this ELEVATES the prediction
priority (general law) and REFINES NP_024 (which stated √(6/7) as K=6-specific).
Counterexamples: '√(6/7) is unique to K=6' fails (every K ≥ 2 gives √(K/(K+1)) — K=5
gives √(5/6), K=7 gives √(7/8), verified); 'the multiplicities distinguish K' fails
(K=5 and K=6 share (6,5) at N=96); 'the ratio is N-dependent' fails (N-independent —
K=6 gives √(6/7) at N=48, 96, 192); 'K=10 breaks the family' fails (size suppression
at N=96, family exists at other N); 'a generic ring shows no blocks' fails (every
K ≥ 2 ring has non-doublet blocks except size-suppressed cases). Falsification: a
circulant ring C_N(±1..±K) with a different protected ratio; a ring where the ratio
changes with N; two K values with the same ratio (not injective); the same
multiplicity pair from different (N,K). Classification: the √(K/(K+1)) family DERIVED
(universal, N-independent); √(6/7) as the K=6 member DERIVED (NOT unique to K=6); the
ratio as the K-discriminator DERIVED (injective in K); the multiplicities as the
N-discriminator DERIVED (N/K-dependent); 'unique to K=6' FALSIFIED (refined to a
K-family); the {ratio, multiplicities} signature PREDICTION (identifies (N,K)
uniquely). No new primitive; canonical AT unchanged.

ResearchY-NP_026 (Protected Block Universality Audit, COMPLETE, tests 8/8 PASSED): is
R_K = √(K/(K+1)) a theorem of all circulant-ring spectra, or only of the canonical
nearest-neighbour class C_N(±1..±K)? Verdict: R_K is a theorem of the CANONICAL
NEAREST-NEIGHBOUR CIRCULANT class (determination B) — NOT of all circulants, NOT of
general graphs, NOT an approximation. THE THEOREM (canonical class): the top two
non-doublet blocks sit at λ = 2K and λ = 2K+2 with ratio √(2K/(2K+2)) = √(K/(K+1)),
for every K ≥ 2 (K=2: √(2/3) = 0.81650; …; K=6: √(6/7) = 0.92582; …; K=8: √(8/9) =
0.94281). SCAN 1 — ALTERNATIVE CIRCULANT GENERATOR SETS (verified): the canonical
±{1..±6} gives (14,6),(12,5) with √(6/7); the odd set ±{1,3,5,7,9,11} gives ONE
22-fold block at λ=12 with no partner (ratio absent); the powers-of-2 set
±{1,2,4,8,16,32} gives no non-doublet blocks — the ratio is SPECIFIC to the
consecutive generator set. SCAN 2 — WEIGHTED LINKS (verified): linear decay w=1/s,
exponential decay e^(−0.3s), and random w ∈ [0.5,1.5] all DESTROY the blocks — the
exact λ=2K/2K+2 structure requires ALL couplings equal to 1. SCAN 3 — RANDOM
PERTURBATIONS (verified): ±5% weight perturbations destroy the blocks in every trial
— the ratio is NOT perturbatively protected against weight variation (unlike the
mirror pairs, which are reflection-protected, NP_023). SCAN 4 — MISSING LINKS
(verified): dropping ±1 gives a single block; dropping ±2 gives (13,4),(9,4) with
ratio 0.83205 ≠ √(6/7); dropping ±3 gives (12,12),(8,3) with ratio 0.81650 ≠ √(6/7)
— the full consecutive set is required. SCAN 5 — NON-CIRCULANT GRAPHS (verified): the
path P_12 has at most 2-fold eigenvalues (no non-doublet block); the complete graph
K_12 has a single N-fold block (not a protected ratio); a random graph (p=0.4) has
all-distinct eigenvalues — no protected ratio anywhere. THE ANALYTIC ORIGIN
(verified): λ_{N/4} = 2·Σ(1−cos(πs/2)) = 2K+2 (period-4 sequence 1,2,1,0 sum 4 per
period; K=6 → 14) and λ_{N/6} = 2·Σ(1−cos(πs/3)) = 2K (K=6 → 12), giving the ratio
√(12/14) = √(6/7); requires the consecutive uniform generator set AND N divisible by
4 and 6. DETERMINATION: A) exact theorem of all circulants — NO (alternative
generator sets fail); B) circulant-only theorem (canonical nearest-neighbour class) —
YES; C) approximation — NO (exact within the class). TRUE MATHEMATICAL ORIGIN: the
consecutive uniform generator set (nearest-neighbour circulant) with the two special
modes k=N/4 and k=N/6. NOTABLE: the block ratio is WEIGHT-FRAGILE (destroyed by any
weight variation or missing link), whereas the mirror-pair degeneracy is
reflection-PROTECTED (NP_023) — two distinct protection classes. Counterexamples:
'the ratio holds for all circulants' fails (odd and powers-of-2 generator sets fail,
verified); 'the ratio survives weight variation' fails (any non-uniform weight
destroys the blocks); 'the ratio survives missing links' fails (dropping ±1/±2/±3
changes or destroys it); 'non-circulant graphs show it' fails (path/complete/random
show no protected ratio); 'the ratio is an approximation' fails (it is exact within
the canonical class). Falsification: a consecutive uniform ring C_N(±1..±K) with a
different top-block ratio; a canonical ring where λ_{N/4} ≠ 2K+2 or λ_{N/6} ≠ 2K; a
weighted ring still giving √(K/(K+1)); a missing-link ring still giving √(K/(K+1)).
Classification: R_K for the canonical nearest-neighbour class DERIVED (exact
theorem); same ratio for alternative generator sets FALSIFIED; for
weighted/perturbed/missing-link rings FALSIFIED; for non-circulant graphs FALSIFIED;
origin (consecutive uniform set + modes N/4, N/6) DERIVED (analytic); mirror-pair
protection (reflection) vs block-ratio protection (weight-fragile) — DIFFERENT
protection classes. No new primitive; canonical AT unchanged.

ResearchY-NP_027 (Planck Spectrum Emergence Audit, COMPLETE, tests 8/8 PASSED): can
the full Planck spectrum be reproduced as an emergent read of the D96 spectrum
without quantum postulates? Verdict: the Planck FACTOR FORM n(x) = 1/(e^x − 1) IS
emergent from the D96 geometric occupation statistics (⟨n_k⟩ =
1/(e^(k·ln(1/μ)) − 1), QG194), and the UV is regulated by the finite spectrum — but
the FULL Planck LAW (Stefan-Boltzmann T⁴ = π⁴/15, Wien displacement at x = 2.821,
continuous density of states, Rayleigh-Jeans divergence) does NOT emerge from the
finite discrete 95-mode spectrum (ω ∈ [0.62, 3.98]) without importing temperature
(x = ℏω/kT), a non-canonical primitive. MECHANISMS (A/B/C): A) finite UV solely from
spectral cutoff — YES in AT (the 95-mode finite spectrum is the UV regulator; no
infinite mode-count to diverge; NOT the canonical ℏ mechanism); B) Planck factor from
occupation statistics — YES in FORM; C) max-entropy — CONSISTENT but incomplete (the
geometric/Bose distribution IS the max-entropy distribution, but AT has not derived
the temperature/energy constraint). FOUR LIMITS (continuous form, verified):
Rayleigh-Jeans n → 1/x; Wien n → e^(−x); Stefan-Boltzmann ∫x³/(e^x−1)dx = π⁴/15 =
6.4939; Wien displacement peak at x = 2.821. FINITE-N PROBLEM (verified): the
discrete 95-mode sum ≠ the continuous π⁴/15 integral (no T⁴); the discrete peak is
mode-dependent, not at x = 2.821; no ω→0 mode (min ω = 0.62) so no Rayleigh-Jeans
divergence. UV ORIGIN: in AT the UV is regularized by the FINITE SPECTRUM (no
infinite mode-count), NOT by quantization (ℏ) — a different mechanism; the finite
observability (QG_010) and finite information (QG_228) are related structural bounds
but the direct UV regulator is the finite spectrum. Counterexamples: 'the full Planck
law emerges from D96' fails (discrete 95-mode sum ≠ π⁴/15; peak mode-dependent; no
ω→0 mode, verified); 'temperature is a canonical primitive' fails (T not in the
canonical boundary set); 'the UV is cut by quantization' fails (the finite spectrum
already regulates — no ℏ-cutoff needed); 'Stefan-Boltzmann T⁴ holds' fails (requires
the continuous integral). Falsification: a non-geometric count structure giving a
different n(ω); an infinite-spectrum AT system with a finite UV; a derivation of T
from {Difference, η, spectrum} alone; a finite discrete spectrum reproducing π⁴/15
and x = 2.821 exactly. Closure score 6/10. Classification: Planck-factor form
DERIVED (from the geometric occupation, QG194); temperature dependence (x = ℏω/kT)
BOUNDARY (temperature not a canonical primitive); finite-UV regulator DERIVED (finite
spectrum); full Planck law (T⁴, Wien displacement, continuous DOS) NOT REPRODUCED
from D96 alone (hosted content); quantum (ℏ-cutoff) UV origin REFUTED for AT (the
finite spectrum is the regulator). No new primitive; canonical AT unchanged.

ResearchY-NP_028 (Blackbody Reconstruction Audit, COMPLETE, tests 8/8 PASSED): can the
D96 mode structure reproduce the observed Planck spectrum after coarse-graining (95
positive modes, occupancy weighting, spectral density, Planck comparison, high-frequency
falloff)? Verdict: NO — coarse-graining does NOT heal the NP_027 gaps. The per-mode
occupation factor n = 1/(e^x − 1) is CORRESPONDENCE (NP_027 DERIVED form), but the
blackbody DOS (ω², 3D cavity) is FALSIFIED for D96, the Wien exponential tail is
FALSIFIED (hard cutoff), and the full observed blackbody after coarse-graining is
FALSIFIED. THREE OBSTRUCTIONS (all survive coarse-graining): (1) DOS MISMATCH — the D96
cumulative mode count grows sub-power-law ~ω^1.5 (N(2.5)/N(1.0) = 8/2 = 4.0 vs ω³ =
15.6 for a 3D cavity; N(3.0)/N(1.5) = 10/4 = 2.5 vs 8), with only 44 distinct
frequencies (mirror pairs + 5-fold λ=12 + 6-fold λ=14 blocks) — NOT the smooth ω² DOS a
blackbody needs; (2) TOP-HEAVINESS — 93.7% of the 95 modes sit above band mid (2.30) and
87.4% (83/95) in the top 20% of the band [0.622, 3.98] (span 6.40); occupancy weighting
at θ=1 leaves 65.7% of energy above ω=3.3 vs 23.2% for Planck in-band, so the weight
does not flatten the top cluster; (3) TRUNCATION / NO WIEN TAIL — hard spectral cutoff at
ω_max = 3.980 with zero modes above, and the mode density RISES into the cutoff (0 modes
in [3.0,3.1) but 6 in [3.3,3.4) and 6 in [3.9,4.0)), so the observed e^(−ω/θ) tail to ∞
cannot exist. Counterexamples: 'coarse-graining gives the blackbody continuum' fails
(binning cannot change the mode count — 44 distinct freqs in a finite band); 'occupancy
weighting flattens the spectrum' fails (D96 0.657 vs Planck 0.232 above ω=3.3 at θ=1);
'the high-frequency falloff is the Wien tail' fails (no modes above ω_max; hard edge, not
e^(−ω/θ)); 'choosing θ removes the mismatch' fails (θ rescales x but cannot create low-ω
modes, a ω² DOS, or modes beyond ω_max); 'the 5-fold/6-fold blocks supply the peak' fails
(degenerate lines at ω=3.46/3.74 make the spectrum more line-like, not a smooth
continuum). Falsification: a D96-derived DOS growing as ω² (N(ω) ∝ ω³) over ≥ 1 octave;
any mode above ω_max or an exponential decay of the top cluster; a D96 weighting whose
energy peaks away from the top 20% of the band; the coarse-grained weighted D96 spectrum
matching Planck at any θ to <10% per-bin. Classification: per-mode occupation factor
CORRESPONDENCE (NP_027 DERIVED form); blackbody DOS (ω², 3D cavity) FALSIFIED for D96;
Wien exponential high-ω tail FALSIFIED (hard cutoff at ω_max = 3.98); full observed
blackbody after coarse-graining FALSIFIED (top-heavy + truncated); temperature scale θ
BOUNDARY (cannot rescue the shape). The observed blackbody is NOT an emergent read of the
D96 spectrum — hosted/correspondence content. No new primitive; canonical AT unchanged.

ResearchY-NP_029 (ħ Necessity Audit, COMPLETE, tests 8/8 PASSED): does AT require a
fundamental ħ at all, or is ħ merely the dimensional bridge between derived frequencies
and measured energies? Verdict: AT does NOT require a fundamental ħ. Every derived
observable — the dimensionless spectrum (span 6.40, ω₁ = 0.6216), the quark/lepton
masses (m_u = m_e·Σ√m/√Σm² = 2.164 MeV, QG173), the Planck scale (M_Pl = v·A³ =
254.37·(95·44·87)³ = 1.2234e19 GeV, QG181), the lepton hierarchy (QG209), ΩΛ =
I_occ/ln K = 0.6839 (QG234), the gauge couplings (α_weak = 3/Σm, α_strong = 8/Σ√m) —
is an anchor (m_e or v, in MeV/GeV) times a dimensionless D96 ratio, never invoking ħ.
REMOVE ħ — WHAT BREAKS: nothing — the masses and Planck scale compute identically
without ħ, and the canonical ResearchY derivation chain (D_ResonanceStructure +
NP_NewPhysics) contains no ħ constant; ħ appears only in legacy ResearchQG/
ResearchDATA/ResearchXH analyzers comparing AT results to SI units (G in SI, H0 in Hz).
ENERGY = FREQUENCY: m_u/m_e = Σ√m/√Σm² = 64.0825/√229 = 4.2347 is a pure D96 ratio; in
natural units E[GeV] = ω and AT's anchors are already GeV/MeV — energy-content ratios
equal frequency ratios for all derived content. VS THE ANCHOR LOGIC: v and m_e are
irreducible physics anchors (D_012/D_013) fixing absolute scales AT measures; ħ fixes no
AT scale, carries no D96 information, and never appears in the derived chain — its only
roles are the SI conventions E = ħω and G = ħc/M_Pl² (D_007/D_041). Counterexamples:
'ħ is needed to derive the masses' fails (every mass is m_e/v × a dimensionless ratio);
'E = ħω makes ħ fundamental' fails (AT anchors energies in GeV/MeV directly); 'ħ carries
D96 information' fails (its value matches no spectral number); 'ħ emerges from
actualization' fails (no counting derivation yields a J·s constant); 'ħ is a physics
anchor like v, m_e' fails (it sets no scale and never appears in the derived chain).
Falsification: a derived observable changing when ħ is removed; a mass ratio ≠ a
dimensionless D96 ratio; a derivation of an absolute scale needing ħ (not just J↔GeV).
Classification: ħ as a fundamental constant of AT REFUTED (removing it changes no
derived observable); ħ as the frequency↔energy dimensional bridge BOUNDARY (an SI
unit-convention import, D_012 — like c); the derived ħ-free mass/energy chain DERIVED
(anchors × dimensionless ratios); the dimensionless spectrum and frequency ratios
DERIVED; v, m_e as irreducible physics anchors BOUNDARY (D_012/D_013, unchanged). No new
primitive; canonical AT unchanged.

ResearchY-NP_030 (Temperature Origin Audit, COMPLETE, tests 8/8 PASSED): what object in
AT plays the role of thermodynamic temperature? Does any canonical object generate a
mode-occupation law? Verdict: NO canonical AT object plays the thermodynamic-temperature
role — temperature is NOT DERIVED and NOT EMERGENT in AT. THE FOUR CANDIDATES (each
tested): (1) ACTUALIZATION DENSITY — canonical branching (A_003) is growth: Mu = 2,
GenerationCount = 8, ρ_{k+1} = 2·ρ_k, S = Σ2^k = 255; ρ₇/ρ₀ = 128 (population GROWS
128× across generations), ln(ρ_{k+1}/ρ_k) = ln μ = +0.6931 > 0 per step — a would-be
β = ln(1/μ) = −0.6931 < 0 (population inversion, anti-thermal); a thermal occupation
must decay. (2) OCCUPANCY DISORDER — the octave record [4, 4, 87] (95 positive modes)
is top-heavy: occupancy rises 21.75× into the top octave (87/4), the anti-thermal
direction (a thermal spectrum thins at high ω, D96 crowds). (3) INFORMATION DENSITY —
I_occ = KL(ρ‖uniform) over the octave record = 0.7513 nats (QG_228), ΩΛ = I_occ/ln K =
0.6839 (QG_234): a DERIVED order parameter, but a FIXED scalar that measures
non-uniformity — it has no scale to vary and generates no occupation law. (4) SPECTRAL
CROWDING — mode density rises into the band top (83/95 modes above ω = 3.3, the top 20%
of the band [0.622, 3.98]; 0 modes in [3.0,3.1), more in [3.9,4.0)) — anti-thermal,
same top-heavy fact as (2). MODE-OCCUPATION LAW TEST (decisive): the Bose occupation
n = 1/(e^x − 1) requires a DECAYING geometric rate μ < 1 (NP_027: ⟨n_k⟩ =
1/(e^(k·ln(1/μ)) − 1)). Substituting the canonical μ = 2 gives n_k = 1/(2^(−k) − 1) < 0
for every k ≥ 1 — NEGATIVE occupation, i.e. population inversion, NOT Bose statistics. A
valid Bose occupation needs μ < 1 — exactly the free parameter of NP_027, not fixed by
canonical AT (canonical μ = 2 is the branching count). COUNTEREXAMPLES: 'actualization
density is the temperature' fails (μ = 2 grows, ρ₇/ρ₀ = 128; would-be β < 0 is
inversion); 'occupancy disorder is the temperature' fails (top-heavy [4,4,87], 21.75×
rise into the top octave); 'I_occ is the temperature' fails (a fixed order parameter —
ΩΛ = 0.6839 — with no scale and no occupation law); 'spectral crowding is the
temperature' fails (crowding rises into the cutoff); 'the canonical branching gives the
Planck law' fails (μ = 2 gives n_k < 0 — negative occupation); 'Hawking T ∝ 1/R supplies
thermodynamic temperature' fails (a horizon first-law temperature whose coefficient
needs the imported 2π, QG_185/196 — it generates no mode-occupation law).
FALSIFICATION: a canonical AT quantity that varies continuously, is fixed by the
counting structure, and generates a decaying occupation n ∝ e^(−E/T) with a fixed scale;
a derivation fixing μ < 1 (decay) from canonical structure; an occupation law written as
a function of I_occ alone reproducing a thermal family; a canonical per-mode occupation
decaying with ω (Wien tail) without the free μ < 1. Classification: temperature as a
derived object of AT REFUTED (no canonical candidate generates a thermal mode-occupation
law); actualization density (μ = 2) as temperature source REFUTED (growth = population
inversion, would-be β = −ln 2 < 0); occupancy disorder ([4,4,87]) as temperature
REFUTED (top-heavy, anti-thermal); information density I_occ = 0.7513 as temperature
REFUTED — DERIVED as an order parameter (ΩΛ = 0.6839, QG_234); spectral crowding as
temperature REFUTED (crowding rises into the cutoff); thermal (Bose) occupation law from
canonical structure NOT GENERATED (canonical μ = 2 gives negative occupation; needs free
μ < 1, NP_027); temperature as the import scale x = ℏω/kT BOUNDARY (unchanged,
NP_027/028); Hawking T ∝ 1/R DERIVED in form (ρ-sector first law, QG_184/208) with
coefficient BOUNDARY (2π, QG_185/196) — not a mode-occupation temperature. Temperature
remains exactly what NP_027/028 classified it: a BOUNDARY import used to compare AT's
derived frequency ratios to measured thermal spectra — never a canonical object that
generates a mode-occupation law. No new primitive; canonical AT unchanged.

ResearchY-NP_031 (Structure vs Thermodynamics Audit, COMPLETE, tests 8/8 PASSED): do the
NP_027–NP_030 results indicate that D96 belongs EXCLUSIVELY to the structural layer,
while thermodynamics belongs to a separate occupancy layer? Does AT naturally split into
a Structure Sector (Difference → Actualization → Spectrum) and a Thermodynamic Sector
(Occupations → Temperature → Radiation)? Verdict: YES to a two-layer architecture, but
NOT as two autonomous sectors. THE STRUCTURE SECTOR (inventory, verified) is DERIVED
and self-contained: spectrum (95 modes, band [0.622, 3.98], span 6.40, ω₁ = 0.6216),
octave occupancy [4,4,87], moments, I_occ = KL(ρ‖uniform) = 0.7513 nats, ΩΛ = I_occ/ln K
= 0.6839 (QG_234), masses (m_u = m_e·Σ√m/√Σm² = 2.164 MeV, QG173), M_Pl = 1.2234e19
GeV, couplings, entropy H = log₂95 = 6.57 bits (M_004) — none needs a temperature, and
the ResearchY derivation chain contains NO SI thermal constant (k_B = 1.380649e-23
etc., count 0, verified). THE THERMODYNAMIC OBJECTS (inventory): temperature T is
BOUNDARY (NP_030, no canonical candidate); the Planck-factor FORM n = 1/(e^x − 1) is
EMERGENT from the geometric occupation (NP_027) but only with a free decay rate μ<1;
radiation/blackbody is FALSIFIED as emergent from D96 (NP_028); Stefan-Boltzmann T⁴ =
π⁴/15 is NOT REPRODUCED from the finite spectrum (NP_027); the Wien tail is FALSIFIED
(hard cutoff at ω_max = 3.98); ħ is a BOUNDARY unit bridge (NP_029). THE OVERLAP is
exactly ONE object: the occupation statistics ρ_k = μ^k/S — structural as the branching
count (canonical μ = 2, gens = 8, S = 255, behind the occupancy [4,4,87] and I_occ),
thermal as the SAME geometric form read with a free decay μ<1 (⟨n_k⟩ = 1/(e^(k·ln(1/μ))
− 1), QG194/NP_027). No other structural object has a thermal twin. TEST — ANY THERMAL
OBSERVABLE FROM STRUCTURE ALONE? NO (REFUTED): only state entropy (a count, M_004) and
the occupation FORM (with free μ<1) derive; the canonical μ = 2 occupation is NEGATIVE
(population inversion, NP_030); no Wien tail exists (finite spectrum), no ω² DOS
(sub-power-law growth). TEST — THERMO ADDED ONLY AS A STATE-OCCUPATION LAW? YES
(CONFIRMED): derived mode set {ω_k} + occupation n(ω) = 1/(e^x − 1) + the temperature
scale x = ℏω/kT (BOUNDARY) — structure supplies the substrate and the occupation FORM;
only the boundary parameter T converts the form into a thermodynamic state.
COUNTEREXAMPLES: 'two autonomous DERIVED pillars' fails (no derived temperature/
radiation/T⁴); 'structure alone yields radiation' fails (blackbody FALSIFIED); 'the
canonical count IS the thermal occupation' fails (μ = 2 is anti-thermal); 'temperature
is structural' fails (NP_030); 'I_occ/entropy provide the temperature' fails (fixed
order parameter / state count, not a scale); 'radiation is hosted by the same engine as
the masses' fails (masses = anchor × ratio; radiation = occupation + T). FALSIFICATION:
a structural derivation needing a Boltzmann constant or T; a canonical D96 object
producing a T-dependent spectrum without an added occupation law; a derivation of T
from {Difference, η, spectrum} alone (NP_030 falsification, still open); a second
structural object appearing in a thermal law; a D96-derived DOS ω² with a Wien tail over
an unbounded band. Classification: Structure Sector DERIVED (self-contained);
thermodynamic sector as an autonomous second sector REFUTED (no derived temperature,
radiation, or T⁴); the two-sector split as architecture DERIVED (structure closes; thermo
added as occupation over the modes); occupation statistics ρ_k = μ^k/S as count DERIVED
(A_003/QG194); Planck-factor occupation FORM EMERGENT (NP_027, free decay μ<1);
state-count entropy H = log₂95 = 6.57 bits DERIVED (M_004, a count not a function of T);
information density I_occ = 0.7513 → ΩΛ DERIVED (order parameter, NOT temperature);
temperature scale T (x = ℏω/kT) BOUNDARY (unchanged, NP_027/028/030); radiation/blackbody/
T⁴/Wien tail FALSIFIED as emergent from D96 — hosted content (NP_027/028). AT has one
closed DERIVED structural layer, and thermodynamics is an ADDED state-occupation law
over the structural modes — a DERIVED architectural fact, not a division into two
primitive pillars. No new primitive; canonical AT unchanged.

ResearchY-NP_032 (Thermal-N Search Audit, COMPLETE, tests 9/9 PASSED): is D96
specifically a structure attractor, while another D_N acts as a thermal attractor? Does
structure N ≠ thermal N? Verdict: FALSIFIED as a size dichotomy — there is NO
thermal-attractor ring size in the canonical circulant family C_N(±1..±6), N = 8..512.
THE SCAN (N = 8..512, verified): (1) UV BEHAVIOR — the band edge ω_max → the continuum
3.9851 for ALL N (N = 96 → 3.9796, N = 512 → 3.9849, N = 1024/4096 → 3.9851): the cap
is a property of the coupling set (±1..±6), not of N, so no ring can host a Wien tail
above the band; (2) DOS SCALING — λ_k ≈ (2πk/N)²·91 for small k gives ω_k ≈ (2π√91/N)·k,
an EXACT linear dispersion (ratio ω_k/(c·k) = 1.0000 at N = 4096, k = 1..4), hence
cumulative N(ω) ∝ ω (1D chain, exponent ≈ 1.06–1.09); every ring with span ≥ 8 (392
rings) has low-DOS exponent exactly 1.00 (first two octaves each hold 4 modes →
N(4ω₁)/N(2ω₁) = 2.00), while a 2D/3D thermal cavity needs 4.0/8.0 — NO ring in 8..512
reaches it; (3) OCCUPANCY HIERARCHY — top-heavy at every N (first octave holds 4 modes
for 478/505 N values; D96's [4,4,87] is one member of a 61-ring family in the 3-family
span window [4,8), N = 60..120, with N = 92..100 sharing the (4,4,X) structure);
(4) THERMAL-OCCUPANCY COMPATIBILITY — the Bose occupation n = 1/(e^x − 1) needs a
decaying rate μ < 1, but the canonical branching μ = 2 is N-independent (NP_030): no
ring size changes the branching. COUNTEREXAMPLES: 'a larger N becomes a blackbody
cavity' fails (adding sites adds modes BELOW the fixed cap — ω_max = 3.9851 for all N);
'some N gives ω² DOS' fails (linear dispersion ω ∝ k, exponent ≈ 1); 'the occupancy
flattens at some N' fails (bottom-sparse top-dense at every N); 'D96 is a thermal
attractor' fails (FALSIFIED in NP_028); 'a different coupling class is needed' fails (a
new geometry, not a D_N — out of scope). FALSIFICATION: a ring size N = 8..512 with
low-frequency cumulative DOS exponent ≥ 2.5; a ring with ω_max above the continuum
3.9851; a ring whose high-octave occupancy does not dominate; an N-dependence of the
canonical branching rate. Classification: 'another D_N is a thermal attractor' (N =
8..512, C_N(±1..±6)) FALSIFIED (no ω² DOS, no exponential tail, no thermal occupancy at
any N); structure N = 96 as the structure-sector base DERIVED (octave/family window,
occupancy [4,4,87], NP_031); 1D linear low-frequency DOS of every ring (ω_k ∝ k)
DERIVED (analytic: λ_k ≈ (2πk/N)²·91); N-independent UV cap (ω_max → 3.9851) DERIVED
(property of the coupling set ±1..±6); top-heavy occupancy at every N DERIVED
(structural; anti-thermal direction, NP_028); temperature scale T BOUNDARY (unchanged,
NP_027/028/030); thermodynamics as an added occupancy layer DERIVED (NP_031) — NOT an
N-selection; 'structure N ≠ thermal N' as a size dichotomy FALSIFIED (no thermal N
exists — the split is a LAYER split). D96 is specifically a structure attractor, but
there is no thermal N: thermodynamics is not an N-property of the canonical family, it
is the added occupancy layer of NP_031. No new primitive; canonical AT unchanged.

ResearchY-NP_033 (D96 Ensemble Audit, COMPLETE, tests 9/9 PASSED): can thermodynamic
behavior emerge from an ENSEMBLE of D96 systems even though a single D96 system has no
temperature? Do Temperature, Boltzmann weights, or Bose-like occupations emerge
statistically? Verdict: PARTIAL — the ensemble DOES generate statistical temperature,
Boltzmann weights, and Bose-like occupations (occupation exchange + entropy maximization
over the D96 mode set), but the observed blackbody radiation does NOT emerge (mode-set
obstruction persists). THE FIVE STEPS (verified): (1) SINGLE D96 RING — no temperature
(NP_030): one fixed configuration has no statistical temperature; the canonical
branching μ = 2 is anti-thermal. (2) TWO COUPLED D96 RINGS — zeroth law: total entropy
peaks at equal split (equal β): S(50/50) = 35.7306 > S(35/65) = 35.3600 > S(20/80) =
34.1459 — two D96 systems in occupation contact equilibrate to a common temperature.
(3) OCCUPATION EXCHANGE → MAX-ENTROPY: over the D96 mode set with conserved total
energy, the max-entropy occupation is the Bose distribution n_k = 1/(e^(βω_k) − 1).
(4) EMERGENT STATISTICS: the Bose occupation strictly dominates uniform/linear/bottom-
heavy alternatives at the same energy (S_Bose(1) = 17.8653 > 15.7097 > 15.2101 > 4.03);
the Boltzmann identity ln(n/(1+n)) = −βω holds EXACTLY over the D96 modes; the
microcanonical occupation-exchange marginal is geometric, P(n+1)/P(n) = Q/(Q+M−2)
exactly (verified M=5,Q=3 → 0.5; M=10,Q=10 → 0.5556; M=100,Q=100 → 0.5051); the
energy-temperature relation is monotone (E(0.5) = 73.05, E(1.0) = 12.59, E(2.0) =
1.13). (5) MODE-SET OBSTRUCTION — the ensemble thermalizes OCCUPATION over the FIXED
D96 mode set ([4,4,87] in [0.622, 3.98]); it cannot change the frequencies: octave
energy is BIMODAL at every T (cold T=0.3 → 94% in the low octave; hot T=10 → 91% in
the top octave; no T gives a broad mid-band Planck shape), Σ_k ω_k³/(e^(ω_k)−1) =
120.70 ≠ π⁴/15 = 6.494, and no modes exist above ω_max = 3.98 for a Wien tail.
COUNTEREXAMPLES: 'a single D96 ring is thermal' fails (one configuration has no
statistics, NP_030); 'the ensemble changes the mode set' fails (it changes the
occupation, not the frequencies); 'the emergent Bose occupation gives the blackbody'
fails (thermal occupation × non-thermal DOS: octave energy bimodal, Σω³/(e^ω−1) =
120.70 ≠ π⁴/15, no Wien tail); 'some ensemble T matches Planck' fails (T rescales
occupation but cannot create a ω² DOS or modes beyond 3.98); 'ensemble thermalization
is a new physics mechanism' fails (it is the standard statistical max-entropy mechanism
applied to the D96 level set). FALSIFICATION: a non-geometric max-entropy occupation;
an entropy split where S_A+S_B does not peak at equal β; an ensemble temperature at
which the D96 octave energy matches the Planck shape over a ω² DOS with a Wien tail; a
single-ring observable depending on a temperature. Classification: temperature,
Boltzmann weights, and Bose occupations as STATISTICAL ensemble objects EMERGENT
(occupation exchange + entropy maximization — standard mechanism, no new primitive);
two-D96 thermal contact (zeroth law, equal β) DERIVED; Bose occupation n_k =
1/(e^(βω_k) − 1) as the max-entropy occupation of the D96 modes EMERGENT; Boltzmann
weight e^(−βω_k) over D96 modes EMERGENT; single-D96 temperature REFUTED (unchanged,
NP_030); OBSERVED blackbody radiation from the D96 ensemble FALSIFIED (mode-set
obstruction persists: top-heavy [4,4,87], Σω³/(e^ω−1) = 120.70 ≠ π⁴/15, no Wien tail);
hypothesis (structure single-D96 / thermodynamics ensemble-D96) CONFIRMED in its
statistical part — ensemble-D96 generates thermal occupation statistics; temperature as
SI scale BOUNDARY (unchanged, NP_027/031). An ensemble of D96 systems DOES generate
thermodynamic behavior statistically — temperature (β = ∂S/∂E), Boltzmann weights
(e^(−βω_k)), and Bose occupations all emerge from occupation exchange plus entropy
maximization, exactly as in standard statistical mechanics — but the observed blackbody
is NOT reproduced because the ensemble cannot change the D96 mode set. 'Thermodynamics
is ensemble-D96' is confirmed for the occupation statistics and refuted for the
radiation spectrum. No new primitive; canonical AT unchanged.

ResearchY-NP_034 (Bose Without Blackbody Audit, COMPLETE, tests 10/10 PASSED): why
does a D96 ensemble produce Bose occupation statistics n(ω) = 1/(e^(βω) − 1) yet fail
to reproduce the observed blackbody spectrum? What is the MINIMAL obstruction? Verdict:
BOSE STATISTICS IS SUFFICIENT — the obstruction is entirely the D96 MODE SET g(ω). The
observed radiance factorizes u(ω) = g(ω)·n(ω)·ω (verified: U(1) = 12.588 over the 95
modes = Σ m_i ω_i n(ω_i) over the 44 distinct freqs; the Boltzmann identity
ln(n/(1+n)) = −βω holds exactly on every D96 mode). TEST 2 — replace the D96
occupations with exact Planck occupations: NO-OP (the D96 ensemble occupation already
IS the exact Bose/Planck occupation, NP_033 identity, verified pointwise); blackbody
still fails (Σω³/(e^ω−1) = 120.70 ≠ π⁴/15 = 6.494) ⇒ the occupation is NOT the
obstruction. TEST 3 — replace the D96 mode set with the ideal ω² DOS, keep the Bose
occupation: u(ω) = ω³/(e^(βω)−1), the EXACT Planck law (Stefan-Boltzmann π⁴/15 =
6.4939, Wien displacement x = 2.821, Rayleigh-Jeans x², Wien tail) ⇒ the mode set is
the obstruction and Bose is sufficient (answer A). TEST 4 — SENSITIVITY: (a) UV cutoff
— 40.7% of the blackbody energy lies above ω_max = 3.98 at β = 1 (0 modes above ⇒ no
Wien tail); (b) DOS exponent — D96 cumulative p ≈ 1.0 low-band / 1.51 mid-band vs the
required 3; a p = 1.51 power host integrates to 1.79 < π⁴/15/3 (>3× total suppression);
(c) mode clustering — 44 distinct freqs, 8-bin counts [2,2,2,0,2,2,33,52] (empty
interior bin, dense top cluster); (d) finite count — NOT an obstruction: an ideal
ω²-distributed 95-mode set over the same band reproduces the in-band blackbody integral
to ~0.05%. TEST 5 — MINIMAL DEFORMATION: keep the Bose occupation fixed; change ONLY
the mode set (p → 3, redistribute the 95 modes ω³-uniformly, unbind the band above
3.98) ⇒ ω² DOS, Wien tail, and π⁴/15 restored. COUNTEREXAMPLES: 'the D96 occupation is
not exactly Planck' fails (NP_033 identity, replacement no-op); 'Bose is insufficient'
fails (Bose + ω² DOS reproduces blackbody exactly); 'the 95-mode count is the
obstruction' fails (ideal ω² 95-mode set → in-band error ~0.05%); 'the UV cap is the
only obstruction' fails (exponent and clustering also wrong); 'a new occupation layer
is needed' fails (the occupation is already exact Bose; only a hosted ω² DOS is
missing). FALSIFICATION: a D96 occupation that is NOT exact Bose; Bose + ω² DOS failing
to reproduce π⁴/15/2.821/RJ/Wien; a D96-derived mode set with p = 3 and no UV cap still
failing. Classification: Bose occupation from the D96 ensemble EMERGENT (unchanged,
NP_033); occupation as the blackbody factor SUFFICIENT (answer A — the failure is
wholly in the mode set); D96 mode-set as blackbody host FALSIFIED (unchanged,
NP_028/033); UV cap + DOS exponent + clustering of the D96 mode set DERIVED as the
minimal obstruction; finite mode count NOT an obstruction; additional occupation-level
obstruction NONE (answer B refuted); new primitive/layer NOT REQUIRED (hosted ω² DOS
suffices, answer C refuted); temperature scale β BOUNDARY (unchanged, NP_027/028/030).
The paradox is resolved: the ensemble produces the exact Bose occupation, and that
occupation × the ideal ω² DOS IS the blackbody — the D96 mode set (sub-power DOS,
hard UV cap, top-heavy clustering) is the only thing standing between Bose statistics
and the observed blackbody. No new primitive; canonical AT unchanged.

ResearchY-NP_035 (Density-of-States Origin Audit, COMPLETE, tests 10/10 PASSED): why
does the D96 mode set produce g_D96(ω) (1D-chain DOS, N(ω) ∝ ω, p = 1) instead of the
observed g_BB(ω) ∝ ω² (N ∝ ω³, p = 3)? What is the exact structural origin of the
blackbody DOS mismatch? Verdict: DIMENSIONALITY. The D96 ring is a 1D structure: its
spectrum is indexed by a SINGLE integer k ∈ [1, N−1] with an exactly linear
low-frequency dispersion. ANALYTIC DOS (verified): λ_k ≈ (2πk/N)²·Σs² = (2πk/N)²·91
⇒ ω_k ≈ c·k with c = 2π√91/N (ratio → 1.000 at N = 6144, k = 1..4); equal frequency
spacing ⇒ N(ω) ∝ ω, p = 1. The octave occupancy [4,4,87] confirms it: the low
octaves hold 4 modes each (constant 1D density per unit ω); the top octave holds 87
only because the band ends at ω_max = 3.98 — the NP_028 mid-band "exponent ~1.5" is a
finite-band artifact, NOT an asymptotic DOS exponent. EXPONENT = NUMBER OF MODE
INDICES (verified): 1D lattice → p = 1, 2D → p = 2, 3D → p = 3. ORIGIN (A/B/C/D/E):
A) DIMENSIONALITY — YES, the origin (one integer index k ⇒ p = 1); B) topology — NO
(a 1D open chain also has p = 1); C) finite mode count — NO (p = 1 at N = 96..6144);
D) circulant structure — NO (p = 1 for C_N(±1..±K), K = 1..12 — K changes band width,
not the index count); E) hosted higher-layer geometry — YES for ω² (the blackbody ω²
DOS is the DOS of a genuinely 3D host, hosted content, NP_032). EXTENSIONS (verified):
larger N → p = 1; larger K → p = 1; coupled/longer rings → p = 1 (all stay 1D);
tensor products C_N^⊗2 → p ≈ 2, C_N^⊗3 → p ≈ 3 (only adding an INDEPENDENT direction
raises the exponent). MINIMAL CONSTRUCTION producing N(ω) ∝ ω³ while preserving the
D96 local rule (±1..±6 nearest-neighbour): the 3D tensor product of three D96 rings,
C_96(±1..±6)^⊗3 — separable eigenvalues Λ = λ_k1 + λ_k2 + λ_k3, ω ≈ c·|k| with k ∈ Z³,
N ∝ ω³, DOS ∝ ω² (verified; integer count matches (π/6)R³). COUNTEREXAMPLES: 'a
larger D96 ring becomes 3D' fails (p = 1 at N = 6144); 'larger K produces ω²' fails
(p = 1 for K = 1..12); 'circulant structure causes p = 1' fails (any 1D chain has
p = 1); 'finite count is the cause' fails (p = 1 to N → ∞); 'topology forces p = 1'
fails (open chain also p = 1); 'D96's mid-band ~1.5 is its DOS exponent' fails (it is
a finite-band cap artifact); 'tensor products change the local rule' fails (C_N^⊗3
applies the SAME ±1..±6 rule on each axis). FALSIFICATION: a D96-derived N(ω) growing
faster than linearly over a non-band-edge octave; a 2D/3D lattice count not scaling as
ω^d; a ring C_N(±1..±K) with low-frequency exponent > 1; a single 1D ring reproducing
the 3D DOS. Classification: 1D DOS exponent p = 1 of the D96 ring DERIVED (analytic:
single index, linear dispersion); exponent p = number of independent mode indices
DERIVED; dimensionality as the origin of the DOS mismatch DERIVED (answer A
CONFIRMED); blackbody ω² DOS as the DOS of a 3D host CORRESPONDENCE (hosted higher-
layer geometry, NP_028/034 unchanged); D96 / any C_N(±1..±K) ring as a 3D blackbody
DOS host FALSIFIED (unchanged, NP_032); finite count / topology / circulant structure
as the cause FALSIFIED; 3D tensor product C_N^⊗3 preserving the D96 local rule → N ∝
ω³ DERIVED (minimal construction). The D96 mode set is a 1D structure — one integer
mode index, linear dispersion, N(ω) ∝ ω (p = 1). The blackbody ω² DOS requires three
independent mode indices and is hosted higher-layer content, minimally realized by
the 3D tensor product of three D96 rings. Dimensionality — not topology, count,
coupling range, or circulant symmetry — is the exact structural origin of the
blackbody DOS mismatch. No new primitive; canonical AT unchanged.

ResearchY-NP_036 (3D Emergence Audit, COMPLETE, tests 9/9 PASSED): can observed 3D
physics emerge from multiple D96 structure sectors? Is the observed 3D blackbody DOS
(N(ω) ∝ ω³, g(ω) ∝ ω²) naturally explained as D96⊗D96⊗D96? Verdict: the ω³ DOS IS
reproduced by D96⊗D96⊗D96 — as a HOSTED CORRESPONDENCE, NOT an EMERGENT consequence
of a single D96 sector. DOS EXPONENTS (verified): D96 (single ring, 1 integer index)
p=1 (octave doubling); D96⊗D96 (2 axes) p→2; D96⊗D96⊗D96 (3 axes) p→3 (3D Weyl
counts: positive-octant ball exponent → 3, coefficient → (π/6)R³). COMPARISON: the
blackbody DOS g∝ω², free-field 3D mode counting, and 3D-cavity spectrum all give
N(ω)∝ω³ with the (π/6)R³ Weyl coefficient — IDENTICAL to D96⊗3; the 2D count gives
(π/4)R² (the wrong law); Stefan-Boltzmann ∫x³/(e^x−1)dx = π⁴/15 = 6.4939 ✓.
SUFFICIENCY (verified): the tensor eigenvalues are separable Λ = λ_k1+λ_k2+λ_k3, so
ω ≈ c|k| with k ∈ Z³ — three independent D96 coordinates are MINIMALLY sufficient for
N ∝ ω³ (fewer axes give R¹ or R²). "OBSERVED 3D = ONE D96 AXIS × THREE" (verified as
construction): the three-axis tensor reproduces the 3D Weyl DOS — BUT canonical AT is
ONE D96 ring (p=1 at every N, K — NP_032/035), the metric ansatz g = ρ^(2/d)η is
dimension-generic (only d ≥ 3 derived via the (d−2) Einstein factor, QG290), and
nothing derives that three independent copies must be stacked — so the observed 3D DOS
as D96⊗3 is CORRESPONDENCE (hosted 3D geometry), not EMERGENT. HIDDEN TRIPLE-FACTOR
STRUCTURE (verified): AT already contains A = Σm·#g·occ₂ = 95·44·87 = 363,660 (three
spectral counts of the single ring: mode count × distinct freqs × top-octave modes),
cubed to the dimensionless Planck content: M_Pl = v·A³ = 1.2234e19 GeV with cube
exponent p = ln(M_Pl/v)/ln(A) = 3.0000 (QG181/183) — but this is a FREQUENCY-CONTENT
triple within one ring, NOT three spatial axes; the three octave families [4,4,87]
(QG210) are likewise octave bands within one frequency coordinate. COUNTEREXAMPLES: 'a
single D96 ring produces 3D' fails (p=1 at every N, K); 'D96⊗D96 gives the blackbody'
fails (2D, ω² not ω³); 'D96⊗3 emerges from canonical AT' fails (one ring; the tensor
is a hosted construction); 'd=3 is derived from D96 count structure' fails (metric
dimension-generic, only d ≥ 3, QG290); 'the three octave families are the three axes'
fails ([4,4,87] are three octave BANDS within one frequency coordinate); 'A = 95·44·87
is a spatial triple' fails (a frequency-content triple, cubed for M_Pl). FALSIFICATION:
a 3-fold D96 tensor whose N(ω) does not scale as ω³; a single ring with DOS exponent 3
at any N, K; a canonical derivation of three independent D96 axes or d=3 from the D96
boundary set alone. Classification: DOS exponent p=d of the d-fold D96 tensor DERIVED
(Weyl lattice count); three independent D96 coordinates SUFFICIENT for N(ω)∝ω³ DERIVED
(as a construction); observed 3D blackbody DOS as D96⊗D96⊗D96 CORRESPONDENCE (hosted
3D geometry, NP_028/034/035 unchanged); 3D EMERGING from a single D96 sector
FALSIFIED (single ring p=1); spatial dimension d=3 as a canonical D96 output FALSIFIED
(metric dimension-generic, d ≥ 3 only); hidden triple A = 95·44·87 → M_Pl = v·A³
DERIVED (QG181/183 — frequency-content triple, not spatial). The observed 3D DOS is
naturally explained as D96⊗3 only as a hosted 3D construction (CORRESPONDENCE), not as
an emergent product of a single D96 sector. No new primitive; canonical AT unchanged.

ResearchY-NP_037 (The Role of Three Audit, COMPLETE, tests 11/11 PASSED): is the
recurring exponent 3 — A³ (M_Pl = v·A³, QG181/183), the blackbody DOS ω³ (N(ω) ∝ ω³),
and the spatial dimension d = 3 — a DERIVED consequence of AT (one canonical
generator) or only a CORRESPONDENCE to observed 3D physics? Verdict: success
criterion C — MULTIPLE UNRELATED APPEARANCES; no unified origin. INVENTORY +
TAXONOMY: every substantive 3/cube/triple classified as STRUCTURAL (single-ring
values: period-3 seed p=3 DERIVED D_040; N=96 = 3·2⁵ DERIVED; family count 3 =
floor(log₂ span)+1 VALUE DERIVED / window [4,8) BOUNDARY; occupancy [4,4,87] DERIVED;
A = 95·44·87 triple DERIVED; 1+3+8 gauge generators structure derived / color-3
CORRESPONDENCE; ΩΛ = I_occ/ln K ≈ 3), GEOMETRIC (hosted: ω³ DOS, d=3 exact, D96⊗3,
ρ-support dim — CORRESPONDENCE/EMERGENT except the p=d identity DERIVED), DIMENSIONAL
(A³; p=d), NUMERICAL/COINCIDENTAL (QG183 p=2.99984 read of observed M_Pl/v; color/
generation 3 QG80 coincidental; valence-3 QG83 coincidence). REMOVAL (verified):
remove the cube → A¹/A²/A⁴ fail the Planck scale by 100%/99.9997%/3.6e7% (QG183 power
test); remove the 3-family window → N=48/192/384 (2/4/5 families) exist but the QG209
lepton ratio m_μ/m_e = Σm²/√occMom matches physical 206.77 only at N=96 (N=48 → 102.3,
N=192 → 416.3) and the cosmology deviates (QG_013); remove the third axis D96⊗3 →
D96⊗2 gives the wrong 2D Weyl law (π/4)R² and loses Stefan–Boltzmann π⁴/15; remove the
exact value 3 of d → NOTHING breaks (Einstein prefactor (d−1)(d−2) is zero at d=2 and
non-zero for every d ≥ 3 — only the inequality d ≥ 3 is load-bearing, the value 3 is
hosted). COMMON-GENERATOR SEARCH — NEW RUNG-LADDER FALSIFIER: generalizing the QG183
exponent read across the pairing-complete octave rungs e(N) = ln(M_Pl/v(N))/ln A(N)
gives e = 3.735 (N=48, 2 bands), 2.9998 (N=96, 3 bands), 2.502 (N=192, 4 bands),
2.138 (N=384, 5 bands) — e(N) is monotone decreasing and equals the octave-band count
ONLY at N=96, so the QG183 "cube = three octave bands" anatomy is a VALUE COINCIDENCE
at the canonical rung, NOT a causal generator. AXIS/FAMILY DECOUPLING (verified):
single ring family=3/4/5 always has DOS p=1.000; the 3-family ring's tensor ⊗1/⊗2/⊗3
gives p=1/2/3; the cube e(N) is independent of both. UNIFIED-ORIGIN TEST: A³, ω³ and
d=3 are set by three DIFFERENT inputs — the span window [4,8) (BOUNDARY), the observed
M_Pl/v ratio (BOUNDARY input), and the hosted 3D spatial dimension (CORRESPONDENCE) —
they coincide only numerically at N=96. DERIVATION ATTEMPT: the octave/family 3 is
DERIVED from the N=96 ring alone (span); the cube exponent 3 is NOT derivable from the
ring alone (e(N) is not a structural constant); d=3 is NOT derivable (only d ≥ 3);
DOS p=3 from one ring FALSIFIED (p=1); no canonical object produces all three 3s.
Classification: family/octave VALUE 3 DERIVED (window BOUNDARY, D_040 unchanged);
triple A = 95·44·87 and cube content DERIVED (QG181/183, NP_036 frequency-content
triple); cube exponent 3 value DERIVED as a read of the observed M_Pl/v but law-level
CORRESPONDENCE (no structural generator of the power); d=3 and ω³ DOS CORRESPONDENCE
(hosted, unchanged NP_035/036); a UNIFIED structural origin of the recurring exponent
3 FALSIFIED (rung-ladder decoupling). No reclassification of prior registry entries
(D_040 guard respected); no new primitive; canonical AT unchanged.

ResearchY-NP_038 (Entanglement Audit, COMPLETE, tests 8/8 PASSED): can canonical D96
structures generate true entanglement, or only correlation? Verdict: success
criterion A — ABSENT; canonical D96 yields ONLY correlation, no genuine Bell-type
entanglement. MODEL: each sector S ∈ {A,B} is the 2-mode reduction; canonical
single-sector state ψ_S = √ρ₀e^{iθ₀}|0_S⟩ + √ρ₁e^{iθ₁}|1_S⟩ with θ_k = 2πk/96 and
shares ρ₀=1/3, ρ₁=2/3; joint coefficient c_ij = a_i·b_j. WITNESSES (verified,
deterministic 2×2 complex algebra): two-sector product ψA⊗ψB has Schmidt rank 1,
concurrence 0, CHSH=2 (DERIVED, factorizable); shared-event classical mixture
p|00⟩⟨00|+(1−p)|11⟩⟨11| (p=1/3) has MI = H(1/3) = 0.918 bits > 0 but concurrence 0
and CHSH=2 (diagonal ⇒ PPT-separable — DERIVED classical correlation, not
entanglement); single-DOF interference I = |a|²+|b|²+2Re(a·conj b) =
ρ0+ρ1+2√(ρ0ρ1)cosΔθ is the intensity of ONE complex amplitude (DERIVED as an
OBSERVABLE, NOT an entangler — tensoring with a second sector stays rank 1); Bell
state (|00⟩+|11⟩)/√2 has Schmidt rank 2, concurrence 1, CHSH = 2√2, and sweeping the
canonical product grid (sector indices × shares {1/3,1/2,2/3}) NEVER reaches rank 2
(an entangling gate / joint coherent preparation is required). CANONICAL GENERATORS
(verified): per-sector actualization (product), local phase update θ(t+1)=θ(t)+Δθ
(local unitary U_A⊗U_B), local quadrature measurement (M_001), and the interference
observable (NP_008/009) all preserve Schmidt rank 1 and concurrence 0. LEGACY
RECONCILIATION: QG070/71 (canonical) — shared link phases give classical
correlations, genuine entanglement REQUIRES a NEW SECTOR (joint link state /
entangling interaction) not in {θ, S}; QG018 — the single-DOF scalar sector gives 1
breathing mode and needs an imported tensor sector (precedent); ResearchQM-003's
DERIVED entanglement claim uses a DIFFERENT primitive base (Q-event individuation +
M² non-linearity) and does NOT transfer to the D96 chain; the canonical primitive
set {Difference, η, Z2-paired sector, 3 octave families, SU(2) gauge, v, m_e}
contains no entangler (verified). FOUR CLASSES: correlation present (DERIVED,
classical); synchronization EMERGENT only for equal modes (trivial co-rotation, a
product-state classical relation); resonance locking of unequal modes ABSENT/BOUNDARY
(NP_005/006/009/014 unchanged); genuine entanglement REFUTED/ABSENT as a D96 output;
observed entanglement CORRESPONDENCE/BOUNDARY (needs NEW entangling sector, QG70/71
unchanged). Classification: product DERIVED; shared-event correlation DERIVED;
single-DOF interference DERIVED-not-entangler; genuine entanglement from canonical
D96 REFUTED/ABSENT; entanglement needs new sector CORRESPONDENCE/BOUNDARY; success
criterion A. No new primitive; canonical AT unchanged.

ResearchY-NP_039 (Minimal Entanglement Sector Audit, COMPLETE, tests 7/7 PASSED):
what is the minimal extension required to obtain Bell-type entanglement (canonical
D96 unchanged)? Verdict: ONE new primitive — the joint link state (a coherent
two-sector amplitude c_ij with Schmidt rank ≥ 2, e.g. a Bell pair), hosted on the
DERIVED A×B tensor product; added primitive count = 1. CANDIDATES (verified,
deterministic 2×2 complex algebra): A) complex phase sector — single-DOF phase θ
(ALREADY canonical, QG220) → Schmidt rank 1, concurrence 0, CHSH=2 (interference,
not non-separability); B) tensor-state sector — the A×B product space, filled with
canonical content, holds ONLY product states (rank 1) — a DERIVED formal
construction (the host; same hosted-construction status as NP_036's D96⊗3), not an
entangler; C) shared occupancy sector — shared events / joint phase pinning → MI =
H(1/3) = 0.918 bits > 0 but concurrence 0, CHSH=2 (diagonal-separable classical
correlation); D) non-local information sector — a coherent joint amplitude (Bell
pair (|00⟩+|11⟩)/√2) → Schmidt rank 2, concurrence 1, CHSH = 2√2 — the FIRST and
ONLY candidate that entangles. ADDED PRIMITIVE COUNT (measured): A = 0 (θ already
canonical), B = 0 (formal construction), C = 0 (derived classical correlation),
D = 1 (joint link state) — minimal added primitive count = 1, attained by D alone.
LEGACY: QG70 (REQUIRES NEW SECTOR — θ + S give interference + spinor DOF but the
entangling interaction is missing) and QG71 (NEW SECTOR — the minimal additional
link content is the joint link state) are CONFIRMED and quantified; NP_038
(entanglement ABSENT, only correlation) is the direct predecessor. Classification:
A DERIVED (REFUTED as entangler); B DERIVED (host); C DERIVED (REFUTED as entangler);
D NEW PRIMITIVE (1 added); entanglement from θ + S alone REFUTED. Canonical D96
unchanged; no new primitive beyond the QG71 joint link state.

ResearchY-NP_040 (Joint Link Formalization Audit, COMPLETE, tests 8/8 PASSED): what
is the minimal mathematical object representing the QG71 Joint Link State? Verdict:
a normalized rank-2 complex 2×2 matrix — a coherent two-qubit amplitude c_ij with
det c ≠ 0 — with minimum ontology ONE NEW STATE OBJECT (NEW PRIMITIVE, per NP_039),
hosted on the 2-node link. EQUIVALENCE (verified): for a normalized pure two-qubit
state the three required properties are ONE condition — Schmidt rank 2 ⇔ concurrence
C = 2|det c| > 0 ⇔ CHSH = 2√(1+C²) > 2 ⇔ det c ≠ 0 (swept the diagonal family
a|00⟩+b|11⟩, a=cos α, b=sin α, plus a non-diagonal rank-2 example). MINIMALITY
(verified): one nonzero entry is a product (rank 1); two nonzero entries in a
full-rank arrangement (a|00⟩+b|11⟩, a,b≠0) already give rank 2 — the minimum content
is a two-term coherent superposition, canonical representative the Bell pair
(|00⟩+|11⟩)/√2. PROPERTIES (verified): symmetry (X⊗X invariant, symmetric under
A↔B, ρ_A=ρ_B=I/2); normalization (Σ|c_ij|²=1, singular values squared sum to 1);
composition (per-link primitive — two links compose by tensor product, combined
4-qubit amplitude normalized); locality (NON-LOCAL — each sector maximally mixed I/2,
joint state pure with C=1). ONTOLOGY (verified): NOT a graph edge (no amplitude), NOT
an information link (diagonal, rank 1), NOT an occupancy link (diagonal, rank 1), NOT
a phase link (single-DOF, rank 1) — it IS a new state object (a rank-2 joint
amplitude). Classification: rank-2 2×2 matrix as the minimal object DERIVED;
two-term superposition as minimal content DERIVED; joint link state as graph /
information / occupancy / phase link REFUTED; joint link state as new state object
NEW PRIMITIVE (NP_039; QG71). Formalizes QG71 with minimum ontology. Canonical D96
unchanged.

ResearchY-NP_041 (Joint Link Consequence Audit, COMPLETE, tests 8/8 PASSED): does the
Joint Link State reproduce known quantum-entanglement phenomenology, and can a rank-2
joint link generate the standard hierarchy of entangled states? Verdict: the joint
link state is a COMPLETE TWO-BODY entanglement sector, NOT a complete multipartite
sector — it is MERELY SUFFICIENT FOR BELL PAIRS (success criterion). TWO-BODY
PHENOMENOLOGY (verified, DERIVED): Bell pair (rank 2, concurrence C=1, CHSH=2√2,
entanglement entropy S(ρ_A)=1 bit); entanglement entropy (S(ρ_A)=H(a²) for
a|00⟩+b|11⟩); CKW monogamy (C²(AB)+C²(AC) ≤ 4·det(ρ_A); GHZ strict τ₃=1, W saturated
τ₃=0); teleportation fidelity (F=(2+C)/3, Bell → F=1). MULTIPARTITE PHENOMENOLOGY
(verified, NOT DERIVED from a 2-body link): GHZ (|000⟩+|111⟩)/√2 has 3-tangle τ₃=1
but SEPARABLE bipartite reductions (C=0, CHSH=2), so GHZ ≠ Bell⊗Bell — it needs a
3-body joint state / entangling gate; W (|001⟩+|010⟩+|100⟩)/√3 has τ₃=0 (a distinct
SLOCC class) with ENTANGLED bipartite reductions (C=2/3) — genuinely tripartite, not
a product of two-body links. A network of 2-body joint links is biseparable and cannot
produce genuine tripartite entanglement. Classification: Bell pair DERIVED;
entanglement entropy DERIVED; CKW monogamy DERIVED; teleportation fidelity DERIVED;
GHZ from a single rank-2 link REFUTED; W from a single rank-2 link REFUTED; genuine
multipartite entanglement CORRESPONDENCE (hosted by a 3-body joint state / entangling
gate). Success criterion: the joint link state is merely sufficient for Bell pairs,
NOT a complete entanglement sector. Canonical D96 unchanged.

ResearchY-NP_042 (Multipartite Entanglement Audit, COMPLETE, tests 8/8 PASSED): what
is the minimal extension of the Joint Link State required to obtain GHZ, W, and
genuine multipartite entanglement? Verdict: ONE new primitive — the 3-body (n-body)
joint state, the direct generalization of QG71's joint link state from a 2-node link
to a 3-node hyper-edge; it is the first structure capable of GHZ, W, and genuine
multipartite entanglement. CANDIDATE ONTOLOGIES (verified, deterministic 3-qubit
algebra): B) pairwise links only (a network of 2-body Bell pairs, Bell_AB⊗|0⟩_C) is
biseparable (3-tangle τ₃=0, pairwise concurrence C(AB)=1 but C(AC)=C(BC)=0) — 0 added
primitives but REFUTED (insufficient); A) 3-body joint state directly hosts GHZ
(τ₃=1) AND W (τ₃=0 but genuinely tripartite, pairwise C=2/3) — 1 added primitive,
sufficient for both; C) entangling composition rule (a CZ gate) generates the
3-qubit cluster/graph state (τ₃=1, LU-equivalent to GHZ) but CANNOT produce W (graph
states keep all 8 amplitudes equal-magnitude, W has a zero) — 1 added primitive,
GHZ-class only. ENTROPY PARTITIONS (verified): GHZ S(A)=S(B)=S(C)=1 bit; W
S=H(2/3)=0.918 bit; biseparable Bell_AB⊗|0⟩_C has S(A)=S(B)=1 but S(C)=0. ADDED
PRIMITIVE COUNT: B=0 (insufficient), A=1 (sufficient for both), C=1 (GHZ only) — the
minimal sufficient ontology is A (the 3-body joint state) at 1 added primitive.
Classification: Bell/CHSH/teleportation DERIVED (NP_041 unchanged); pairwise links
REFUTED; 3-body joint state NEW PRIMITIVE (1 added); entangling composition rule (CZ
gate) NEW PRIMITIVE (1 added, GHZ class only). Success criterion: the first structure
capable of GHZ, W, and multipartite entanglement = the 3-body joint state. Canonical
D96 unchanged.

ResearchY-NP_043 (Joint State Origin Audit, COMPLETE, tests 7/7 PASSED): can Joint
States be derived from existing canonical objects, or are they irreducible primitives?
Verdict: IRREDUCIBLE PRIMITIVES (NEW PRIMITIVE) — not DERIVED and not EMERGENT from any
canonical object. CANONICAL INVENTORY (verified, none entanglement-capable):
Difference (η) = real scalar; Actualization = branching μ=2 → diagonal occupancy
(classical); Occupancy = diagonal counts [4,4,87] (separable); Information = I_occ/MI
(a scalar / classical correlation — MI>0 but concurrence 0); D96 spectrum = 95 real
frequencies (no amplitude); Phase (θ=2πk/N) = single-DOF amplitude (interference,
rank 1). DERIVATION ATTEMPTS (verified): 2-body — sweeping phase products / shared
occupancy / single-DOF interference reaches max Schmidt rank 1, and the Bell state
(rank 2) is unreachable → IRREDUCIBLE; 3-body — canonical objects + 2-body links give
biseparable states (τ₃=0), while GHZ (τ₃=1) and W (pairwise C=2/3) are unreachable →
IRREDUCIBLE. PRIMITIVE COUNT (measured): canonical = 0 joint states; 2-body joint link
state = 1 (NP_039); 3-body joint state = 1 (NP_042); total = 2 added primitives.
EARLIEST APPEARANCE (verified): the 2-body joint link state (NP_039) is the first
rank-2 space; canonical D96 reaches only rank 1 (correlation only, NP_038), and no
canonical object precedes it. Classification: Difference / Actualization / Occupancy /
Information / D96 spectrum / Phase DERIVED (single-DOF, classical, or scalar); 2-body
joint state NEW PRIMITIVE (irreducible); 3-body joint state NEW PRIMITIVE
(irreducible); deriving or emerging joint states from canonical AT REFUTED. Canonical
D96 unchanged.

ResearchY-NP_044 (Joint State Necessity Audit, COMPLETE, tests 7/7 PASSED): does any
observed phenomenon force the introduction of Joint States, or can all currently
derived AT results exist without them? Verdict: the joint states are an OPTIONAL
extension (B) of AT that currently functions as a CORRESPONDENCE layer (C) — NOT
necessary physics (A) for any already-derived result. EXISTING RESULTS (verified,
joint-free): the canonical state is rank 1, and every derived observable — the D96
spectrum (95 real frequencies), A = Σm·#g·occ₂ = 95·44·87 = 363,660, M_Pl = v·A³ =
1.2234e19 GeV, the mass ratios, couplings, ΩΛ — is a scalar / spectral product; no
joint state appears anywhere. CANONICAL BOUND (verified): sweeping canonical products
gives max CHSH = 2 (no Bell violation). FIRST FORCED RESULT (verified): the Bell/CHSH
inequality violation S = 2√2 > 2 is reproduced ONLY by a rank-2 joint state (the Bell
pair), and no canonical object reaches it — this is the first empirical result that
cannot be reproduced without the joint-state primitives. AFTER BELL (verified):
teleportation (F=1, needs a Bell pair) and GHZ (τ₃=1, needs a 3-body joint state) also
require joint states, later in the empirical hierarchy. Classification: existing
derived results DERIVED (single-DOF / classical / scalar); joint states as necessary
physics (A) for current AT REFUTED; joint states as optional extension (B) CONFIRMED;
joint states as correspondence layer (C) CONFIRMED (parallel to the hosted ω² DOS of
NP_035/036); the Bell/CHSH violation CORRESPONDENCE (first forced result). Success
criterion: first non-reproducible empirical result = CHSH > 2. Canonical D96
unchanged.

ResearchY-NP_045 (CHSH Reality Audit, COMPLETE, tests 8/8 PASSED): must AT accept CHSH
violations as fundamental physics? Verdict: YES — the joint-state sector is REQUIRED
PHYSICS for a complete theory of observed entanglement. EVIDENCE (verified): Bell 1964
(local realism ⇒ |S| ≤ 2, a theorem); CHSH 1969 (operational inequality S ≤ 2); Aspect
1982 (first experimental violation S ≈ 2.7 > 2); Zeilinger (teleportation + GHZ
experiments); loophole-free 2015 (Hensen/Giustina/Shalm, S > 2 with detection + locality
closed) — four independent classes confirm the Bell/CHSH violation as a robust,
loophole-free empirical fact. CANONICAL AT (verified): sweeping canonical products
gives max CHSH = 2 — it MISSES the fact. JOINT-STATE SECTOR (verified): the Bell pair
gives CHSH = 2√2, teleportation F = 1, GHZ τ₃ = 1 — it reproduces the full hierarchy.
OPTION C (reproduce CHSH > 2 without joint states) REFUTED (no canonical object reaches
S > 2, NP_038/043). OBSERVATIONS EXPLAINED/MISSED (verified): canonical AT 0/4 (Bell,
teleportation, GHZ, W all missed); joint-state sector 4/4. CONSISTENCY (verified): both
sectors are internally consistent; adding the joint-state sector introduces no
contradiction. Classification: CHSH > 2 as a physical fact (A) CONFIRMED; reproducing
without joint states (C) REFUTED; joint states as REQUIRED PHYSICS CONFIRMED (for
observed entanglement); joint states as a CORRESPONDENCE layer CONFIRMED (hosted,
non-derived from canonical D96); NP_044 "optional extension" REFINED — optional for the
currently-derived chain, but REQUIRED for the observed Bell violation. Canonical D96
unchanged.

ResearchY-NP_046 (Joint State Physical Necessity Audit, COMPLETE, tests 8/8 PASSED):
why does nature require Joint States? Verdict: because actuality is not always
decomposable into independent single-sector actualizations — NON-SEPARABILITY IS
PRIMITIVE. COMMON FEATURE (verified): Bell, teleportation, GHZ, and W all share
non-separability — the joint state is irreducible to the states of its parts (rank > 1
for 2-body, genuine tripartiteness for 3-body). CANDIDATES (verified): A) shared
information — MI > 0 is classical and separable (already canonical, NP_038) → REFUTED;
B) shared actualization — joint phase pinning gives a product state (rank 1) → REFUTED;
C) SHARED REALITY — for a Bell pair each single sector is maximally mixed (ρ_A = I/2,
S = 1 bit, no local reality) yet the joint state is pure (S = 0); the coherence lives
ONLY in the joint object, the reality is in the relation, not the parts → CONFIRMED;
D) fundamentally new ontology — the rank-2 joint amplitude (NP_040's new state object)
→ CONFIRMED. REMOVE JOINT STATES (verified): the Bell/CHSH violation (S > 2) fails
first, then teleportation (F ≤ 2/3), then GHZ/W (τ₃ = 0). MINIMAL PHYSICAL PRINCIPLE:
the irreducibility of joint actualization to separate single-sector actualizations —
non-separability is primitive; nature must contain joint states because the Bell
violation proves two subsystems can actualize one coherent state that no single
subsystem possesses. Classification: shared information (A) REFUTED; shared
actualization (B) REFUTED; shared reality (C) CONFIRMED; new ontology (D) CONFIRMED;
non-separability as the minimal principle NEW PRIMITIVE. Canonical D96 unchanged.

ResearchY-NP_047 (Joint State Dynamics Audit, COMPLETE, tests 8/8 PASSED): how are
Joint States created, transformed, and destroyed? Verdict: full dynamics exists with
ONE added primitive — the entangling gate. THE THREE RULES (verified): CREATION
(Product → Joint) requires a non-local entangling gate — CNOT |+⟩|0⟩ = (|00⟩+|11⟩)/√2
(the Bell pair, rank 2, C=1) and CZ |+⟩|+⟩ = (|00⟩+|01⟩+|10⟩−|11⟩)/2 (rank 2) — while
local unitaries U_A⊗U_B CANNOT create entanglement (they preserve Schmidt rank, so a
product stays rank 1); STABILITY (Joint → Joint) via local unitaries preserves Schmidt
rank, concurrence, and entanglement entropy (the singular values of the coefficient
matrix are invariant), and the canonical per-sector phase update θ(t+1) = θ(t) + Δθ IS
U_A⊗U_B (NP_038/041) — so no new primitive is needed for stability; DESTRUCTION (Joint
→ Product) via local measurement (M_001 reads one quadrature) collapses to a product
(rank 1, S=0). CONSERVATION (verified): rank/concurrence/entropy are conserved by local
unitaries, created by the entangling gate, destroyed by measurement — entanglement is
not a conserved quantity under arbitrary operations. MULTIPARTITE (verified): Bell →
GHZ via CNOT + a third |0⟩ qubit (τ₃=1, achievable); GHZ → W REFUTED (distinct SLOCC
class — GHZ τ₃=1, W τ₃=0 — no CZ composition reaches W, NP_042). Classification:
creation NEW PRIMITIVE (the entangling gate, CNOT/CZ); stability DERIVED (local
unitaries, canonical phase update); destruction DERIVED (local measurement, M_001);
full dynamics exists CONFIRMED; static ontology only REFUTED; additional primitive
required CONFIRMED (exactly 1). Minimal dynamical law = {entangling gate (create) +
local unitary (stabilize, canonical) + local measurement (destroy, canonical)}.
Canonical D96 unchanged.

ResearchY-NP_048 (Entangling Gate Origin Audit, COMPLETE, tests 7/7 PASSED): is the
entangling gate itself derivable, or an irreducible primitive? Verdict: IRREDUCIBLE —
a NEW PRIMITIVE (C), neither DERIVED (A) nor EMERGENT (B). CANONICAL OPERATION
INVENTORY (verified, all local/classical): Difference (η) = real scalar; Actualization
= diagonal (branching μ=2); Occupancy = diagonal counts; Information = scalar/MI;
Phase (θ) = single-DOF local unitary; D96 Resonance = real frequency content. CONSTRUCTION
ATTEMPTS (verified, all fail): phase coupling (shared phase / joint pinning, NP_004) →
rank 1 (a local unitary U_A⊗U_B preserves Schmidt rank); resonance locking → ABSENT
(NP_005/006/009/014); equal-mode co-rotation → rank 1 (product); occupancy exchange
(NP_033) → separable (C=0); information exchange (MI>0) → separable (C=0). EXHAUSTIVE
SWEEP (verified): no canonical operation reaches Schmidt rank 2 (max rank 1) — all
canonical operations are LOCC (local + classical communication), which cannot raise
rank. THE GATE (verified): CNOT/CZ is the UNIQUE non-local rank-raising operation.
Classification: canonical operations DERIVED; gate DERIVED REFUTED; gate EMERGENT
REFUTED; gate NEW PRIMITIVE CONFIRMED. The entanglement sector therefore needs TWO
irreducible primitives — the joint state (NP_043) and the entangling gate (NP_048) —
parallel and consistent. Canonical D96 unchanged.

ResearchY-NP_049 (Entangling Gate Necessity Audit, COMPLETE, tests 7/7 PASSED): is the
Entangling Gate forced by observed quantum experiments, or could an alternative
primitive replace it? Verdict: UNIQUELY REQUIRED AS A KIND (A) — the non-local
entangling interaction — and NOT replaceable (C REFUTED). PHENOMENA (verified): Bell,
CHSH, teleportation (2-body) and GHZ, W (3-body) all require creation (rank 1 → rank 2),
which is the gate's exclusive job. REMOVE GATE (verified): retaining joint states but
removing the gate leaves them statically present (rank 2) but UNPREPARABLE — no
canonical operation creates rank 2 from a product (NP_048). ALTERNATIVES (verified, all
fail): shared actualization (phase pinning) → rank 1; non-local/shared phase → rank 1
(a genuinely non-local phase acting jointly on both qubits IS a controlled-phase = the
gate itself, so no distinct alternative); resonance coupling → ABSENT (NP_005/006);
information coupling → MI>0 but separable (C=0). EQUIVALENCE (verified): CNOT, CZ,
iSWAP, and √SWAP each create a rank-2 state from a product input — they are
LU-equivalent representatives of the SAME primitive (the non-local entangling
interaction) in different bases, not several independent primitives. PRIMITIVE COST:
gate = 1; alternatives = 0 but insufficient. Classification: gate uniquely required (A)
CONFIRMED as a kind; representative freedom (CNOT ≡ CZ ≡ iSWAP ≡ √SWAP) CONFIRMED; gate
replaceable by an alternative (C) REFUTED; gate NEW PRIMITIVE CONFIRMED (NP_047/048).
Canonical D96 unchanged.

ResearchY-NP_050 (Physical Realization Audit, COMPLETE, tests 8/8 PASSED): what
physical interaction corresponds to the Entangling Gate? Verdict: a coherent two-body
(non-local) interaction — a joint coupling Hamiltonian H_int = J·σ⊗σ generating the
non-local unitary U = e^{−i H_int t} — CORRESPONDENCE to known physics, NOT derivable
from canonical D96. INVENTORY (verified): all five known entangling mechanisms —
photon SPDC (Bell-pair creation), Heisenberg exchange (iSWAP/√SWAP), the identical-
fermion exchange interaction (singlet/triplet), cavity QED Jaynes-Cummings (effective
XX/ZZ), superconducting qubits (capacitive XX) — are genuine TWO-BODY couplings (a
joint term, not a sum of single-body terms). LOCAL VS NON-LOCAL (verified): a local
Hamiltonian H = σ_z⊗I generates a local unitary that preserves Schmidt rank 1, while a
non-local H = σ_z⊗σ_z (Ising ZZ) and H = σ_x⊗σ_x (XX) each create rank 2 — the precise
difference between the canonical phase update (local) and the gate (non-local). COMMON
STRUCTURE (verified): the entangling signature is a cross-block coupling (|00⟩↔|11⟩,
XX) or a controlled joint phase (Ising ZZ), absent in a local gate. ONE ABSTRACT
INTERACTION (verified): the non-local two-body unitary explains all five mechanisms
(LU-equivalent, NP_049). COMPARISON (verified): canonical D96 has NO two-body coupling
(NP_048); the joint state is the gate's OUTPUT (NP_040); the gate is the non-local
two-body interaction itself. Classification: known entangling interactions
CORRESPONDENCE (hosted physics); physical meaning of the gate CORRESPONDENCE (coherent
two-body interaction); gate NEW PRIMITIVE CONFIRMED; gate DERIVED from canonical D96
REFUTED. Canonical D96 unchanged.

ResearchY-NP_051 (Correspondence Layer Necessity Audit, COMPLETE, tests 6/6 PASSED):
why does nature contain the quantum correspondence layer at all? Verdict: the layer is
an UNAVOIDABLE CONSEQUENCE OF OBSERVATION ITSELF (C), not an optional convenience (A).
PHENOMENA (verified): Bell, teleportation, GHZ, and W all share non-separability
(NP_046). COMMON INVARIANT (verified): for the Bell pair each single sector is
maximally mixed (ρ_A = I/2 — no local reality) while the joint state is pure — the
whole is irreducible to its parts. REMOVE THE LAYER (verified): canonical AT alone
gives CHSH ≤ 2, which contradicts the observed Bell violation S = 2√2 > 2 — the first
empirical contradiction is exactly the Bell/CHSH violation. COMPLETION (verified):
canonical AT is internally complete (no internal contradiction) yet misses observed
non-separability; the layer supplies it (observational necessity, B) — it is NOT an
optional convenience. MINIMAL REASON: observation (measurement, M_001) reads the
ACTUAL — a joint actualization — and joint actualization is irreducible to separate
single-sector actualizations; the correspondence layer is where that irreducibility
surfaces. Classification: canonical AT DERIVED (complete WITHOUT the layer — the
single-DOF/classical derived chain); layer as optional convenience (A) REFUTED; layer
as observational necessity (B) CONFIRMED; layer as unavoidable consequence of
observation itself (C) CONFIRMED; entanglement sector CORRESPONDENCE (hosted,
non-derived). This is the capstone of the entanglement program (NP_038–NP_051):
canonical D96 unchanged.

ResearchY-NP_052 (Quantum Primitive Completeness Audit, COMPLETE, tests 9/9 PASSED):
are Joint State and Entangling Gate the complete minimal quantum extension? Verdict:
YES — the two primitives are COMPLETE (success criterion A); no third primitive, no
incompleteness. REPRODUCED (verified): Bell, CHSH, teleportation (2-body), GHZ, W
(3-body). UNTESTED FEATURES (verified, all compositions/consequences): entanglement
swapping is teleportation of half a Bell pair — the identity |Φ+⟩_AB⊗|Φ+⟩_CD = 1/2 Σᵢ
|Bellᵢ⟩_AD⊗|Bellᵢ⟩_BC holds, so each BC Bell outcome is equiprobable (1/4) and the AD
pair is always maximally entangled (C=1): a composition of {joint state, gate};
delayed choice and quantum eraser are SINGLE-DOF phase phenomena — interference
P = cos²(φ/2) on one qubit (canonical θ + M_001), no entanglement, no gate, no third
primitive; contextuality is IMPLIED by the CHSH violation (CHSH = 2√2 > 2 ⇒ no
non-contextual hidden-variable assignment) — a theorem, not a primitive; many-body
scaling is TENSOR PRODUCTS — GHZ_n = (|0…0⟩+|1…1⟩)/√2 has exactly 2 terms for every n,
built from n−1 CNOT gates, so the ontology does not grow with n. ONTOLOGY SIZE = 2
(joint state + entangling gate, NP_040/048). Classification: Bell/CHSH/teleportation/
GHZ/W/swapping/contextuality/many-body CORRESPONDENCE; delayed choice/eraser DERIVED
(single-DOF); two primitives complete (A) CONFIRMED; third primitive (B) REFUTED;
incompleteness (C) REFUTED. This completes the entanglement program: the minimal
quantum extension of canonical D96 is exactly {Joint State, Entangling Gate}. Canonical
D96 unchanged.

ResearchY-NP_053 (Relativistic Consistency Audit, COMPLETE, tests 9/9 PASSED): do
Joint States and Entangling Gates violate causality, locality, or relativistic
consistency? Verdict: FULLY COMPATIBLE — the entanglement sector is non-local in
CORRELATIONS but obeys NO-SIGNALLING. SPACELIKE SEPARATION (verified): a Bell pair's
marginals are ρ = I/2 (maximally random, S = 1 bit) — zero information about the other
party. NO-SIGNALLING (verified): ρ_A is invariant under arbitrary local unitaries on B
(bit-flip, phase-flip, Hadamard) — the no-signalling theorem. BELL/CHSH/TELEPORTATION/
GHZ (verified): the non-local correlation is observable only AFTER classical
comparison (each party alone sees maximally random outcomes); teleportation requires a
2-bit CLASSICAL channel — the four Bell outcomes are equiprobable (1/4) and without the
bits Bob's state is I/2 (zero information), so the quantum channel alone transfers
nothing. JOINT REALITY (verified): non-separability is CORRELATION, not INFORMATION
TRANSFER — the Bell pair is supported on |00⟩,|11⟩ (perfect correlation) yet each
marginal is I/2. CANONICAL VS LAYER (verified): canonical AT is trivially local
(single-DOF, CHSH ≤ 2); the correspondence layer adds non-local correlations (CHSH > 2)
but still no signalling. NO CONTRADICTION (verified): causality, Lorentz invariance
(the no-signalling bound is frame-independent), and no-signalling all hold.
Classification: Bell/CHSH/teleportation/GHZ CORRESPONDENCE; no-signalling DERIVED (the
marginal ρ=I/2 is a mathematical fact); superluminal communication REFUTED; joint
reality = information transfer REFUTED; full relativistic compatibility CONFIRMED.
Canonical D96 unchanged.

ResearchY-NP_054 (Quantum Completeness Stress Test, COMPLETE, tests 9/9 PASSED): does
{Joint State, Entangling Gate} reproduce every major quantum phenomenon currently
untested? Verdict: YES — the quantum layer is COMPLETE (success criterion A); no third
primitive, no contradiction. STRESS-TEST PHENOMENA (verified): contextuality /
Kochen-Specker is IMPLIED by non-separability (CHSH = 2√2 > 2 rules out non-contextual
hidden variables; KS is the state-independent d≥3 form and the 2-qubit joint state
lives in d=4) — needs the Joint State only; delayed choice / quantum eraser are
SINGLE-DOF phase phenomena (interference P = cos²(φ/2), canonical θ + M_001) — no
entanglement, no gate; entanglement swapping is a COMPOSITION (|Φ+⟩_AB⊗|Φ+⟩_CD = 1/2
Σᵢ |Bellᵢ⟩_AD⊗|Bellᵢ⟩_BC — each BC Bell outcome equiprobable 1/4, the AD pair always
maximally entangled C=1); Hardy paradox (NEW) — the Hardy state (|00⟩+|01⟩+|10⟩)/√3 is
non-separable (rank 2, concurrence 2/3, zero |11⟩ amplitude), and the paradox is an
"all-or-nothing" Bell-type non-locality witness that is a CONSEQUENCE of
non-separability (the joint state) + measurement — NOT a third primitive (any rank-2
state is LU-equivalent to one reachable by the gate, NP_042/047/049). ONTOLOGY SIZE =
2 (joint state + gate), unchanged after the stress test. Classification:
contextuality/KS/swapping/Hardy CORRESPONDENCE; delayed choice/eraser DERIVED
(single-DOF); quantum layer complete (A) CONFIRMED; third primitive (B) REFUTED;
contradiction (C) REFUTED. Canonical D96 unchanged.

ResearchY-NP_055 (Dark Energy Ontology Audit, COMPLETE, tests 11/11 PASSED): what is
Dark Energy physically inside AT? Verdict: ΩΛ = I_occ/ln K = 0.6839 (QG234, OBSERVED
0.12%) has a DERIVED ontology ONLY as the INFORMATION-BOOKKEEPING fraction
(interpretations B = E, identical) — and NO physical (energetic/equation-of-state)
ontology. The canonical ρ = [4,4,87]/95 reproduces I_occ = KL(ρ‖uniform) = 0.7513 nats
exactly, and the pair partitions the state-space size: I_occ + H = ln K (0.7513 + 0.3473
= 1.0986 = ln 3); ΩΛ = I_occ/ln K is the EXCESS-over-uniform fraction, Ωm = H/ln K =
0.3161 is the realized-entropy fraction. Interpretation tests: A) vacuum energy (w = −1)
NOT DERIVED — no energy density, and QG230's own Λ ∝ 1/R² (ρ_Λ ∝ a^(−2)) translates to
w = −1/3, not −1, while the hosted q₀/z_acc closures assume w = −1; B) information
content YES (literal: ΩΛ IS I_occ/ln K); C) occupancy deficit INVERTED (the deficit is
matter Ωm = H/ln K, QG195/196, not dark energy); D) actualization pressure METAPHORICAL
(the canonical μ = 2 branching is a population inversion, anti-thermal — NP_030);
E) bookkeeping YES (= B). Additional observables: the finite family {ΩΛ, Ωm, ratio
2.1633, q₀ = −0.5258, z_acc = 0.6295} — q₀/z_acc values DERIVED, forms CORRESPONDENCE
(hosted w = −1). INTERNAL TENSION (NEW): the corpus carries THREE incompatible dark-energy
equations of state — w = −1 (hosted closures), w = −1/3 (QG230 Λ ∝ 1/R²), and w ≠ −1
time-varying (legacy Λ(t) = α/√V(t), coefficient 0.015 FITTED, X046/XD001) — none
derived from ΩΛ itself. Future/ΩΛ(z): NOT derived — the information fraction is
time-independent, so future evolution and ΩΛ(z) are hosted FRW/ΛCDM. Classification:
ΩΛ value + information-bookkeeping ontology DERIVED (B = E); vacuum-energy/w = −1
CORRESPONDENCE (hosted); occupancy-deficit REFUTED as dark-energy ontology (it is
matter); actualization-pressure EMERGENT-as-metaphor / REFUTED as physical. Success
criterion: PARTIAL — a successful numerical relation plus a DERIVED informational
ontology, but NOT a physical dark-energy mechanism. No new primitive; canonical AT
unchanged.

ResearchY-NP_056 (Equation-of-State Audit, COMPLETE, tests 9/9 PASSED): can the AT
informational ontology generate a unique equation of state? Verdict: NO — ΩΛ = I_occ/ln K
= 0.6839 is a SNAPSHOT number (present-day density fraction) with no equation-of-state
content, so AT predicts only a density fraction, not a dark-energy dynamics. EXPANSION
DYNAMICS ARE DEGENERATE: the same ΩΛ = 0.6839 gives H²(z=1)/H₀² = 3.2127 (w=−1), 4.4632
(w=−1/2), 5.2644 (w=−1/3) — a 64% spread. ACCELERATION IS NOT DERIVED: q₀ = [Ωm +
ΩΛ(1+3w)]/2 requires w < −0.4874 (an extra, hosted input); QG230's own Λ ∝ 1/R² (w = −1/3)
gives q₀ = +0.158 (decelerating). Candidate equations of state: w = −1 (hosted ΛCDM, ρ ∝ a⁰),
w = −1/3 (separate M ∝ R scaling, QG184, non-accelerating), evolving w(z) (legacy Λ(t) =
α/√V(t), coefficient 0.015 FITTED) — all CORRESPONDENCE, none DERIVED. NEW RESULT — the
bookkeeping reading is dynamically inconsistent with acceleration: a time-independent
ΩΛ fraction forces ρ_Λ = (ΩΛ/Ωm)ρ_m ∝ ρ_m ∝ a^(−3) ⇒ w = 0 ⇒ q₀ = +0.5 (decelerating); the
observed acceleration requires ΩΛ(z) to EVOLVE (w < −1/3), which the fixed partition of
ln K does not do. Classification: ΩΛ = 0.6839 DERIVED (QG234 unchanged); the equation of
state w BOUNDARY (undetermined); w = −1 / w = −1/3 / evolving w(z) CORRESPONDENCE
(hosted/scaled/fitted); a unique dark-energy dynamics REFUTED; cosmic acceleration NOT
DERIVED. No new primitive; canonical AT unchanged.

ResearchY-NP_057 (Dark Energy Meaning Audit, COMPLETE, tests 9/9 PASSED): what does ΩΛ
physically represent, and why should information appear as a density fraction? Verdict:
ΩΛ = I_occ/ln K = 0.6839 is the normalized ENTROPY DEFICIT — the information surplus of
the realized top-heavy D96 occupancy over the uniform prior — and is a STATE DESCRIPTOR
(order parameter), not a cause and not an energy density. A = C = E: the five candidate
readings resolve because information-surplus (I_occ/ln K), unused-state-space
((ln K − H)/ln K), and bookkeeping are the SAME quantity (= ΩΛ), while the "deficit"
(H/ln K = Ωm) is MATTER; "pressure" is a metaphor (μ = 2 anti-thermal, NP_030). Verified
as an order parameter: ΩΛ = 0 for the uniform record, 0.6839 for [4,4,87], 0.8938 for
[1,1,93] — monotone in top-heaviness. Equivalent formulations: ΩΛ = I_occ/ln K = KL/ln K
= (ln K − H)/ln K = 1 − H/ln K = 1 − Ωm. Cause/effect/descriptor: ΩΛ drives nothing
(NP_056) — it is a snapshot order parameter of ρ. Family: entropy/information (not vacuum
energy — no derived w = −1; not a cosmological constant — QG230's Λ ∝ 1/R² is not
constant). THE OPEN BRIDGE: the information budget I_occ + H = ln K is identified with the
energy budget ΩΛ + Ωm = 1 (flatness), but the information→energy link (nats → erg/cm³) is
NOT derived — an empirical CORRESPONDENCE. Classification: ΩΛ value + "normalized entropy
deficit / information surplus" meaning DERIVED (KL structure); state-descriptor status
DERIVED (NP_030); identification with the energy fraction CORRESPONDENCE (bridge
BOUNDARY/open); vacuum-energy / cosmological-constant CORRESPONDENCE (hosted); pressure
reading REFUTED (NP_030). Strongest interpretation: ΩΛ = the realized occupancy's
information surplus normalized by the state-space capacity — a DERIVED state descriptor in
the entropic/informational family. No new primitive; canonical AT unchanged.

ResearchY-NP_058 (Information-to-Energy Bridge Audit, COMPLETE, tests 8/8 PASSED): why
does I_occ + H = ln K correspond to ΩΛ + Ωm = 1, given the information→energy bridge is
not derived? Verdict: the bridge originates at QG89, "energy = its Noether conjugate,
measured as the actualization rate (Q-event activity)" — the FIRST appearance of physical
energy language, and a DEFINITION, not a derivation. It is inherited at QG230 ("I_vac > 0
⇒ ρ_Λ > 0", inheriting QG89; and the dimensional step "Λ = 8πG·ρ_Λ", importing
G = ħc/M_Pl² which requires ħ and c — both BOUNDARY unit conventions, NP_029), and realized
as a fraction at QG234 ("ΩΛ = I_occ/ln K = 0.6839", a numeric identification matched to
Planck only by 0.12%). Every step before the bridge — ρ = [4,4,87]/95 → H = 0.3473 nats →
I_occ = KL(ρ‖uniform) = 0.7513 nats → I_occ/ln K = 0.6839 — is pure counting/information
(DERIVED), with no ħ, c, or G. Removing the bridge loses nothing on the information side:
AT still derives 0.6839; only the LABELING with the energy-density fraction is non-derived.
Observable type: ΩΛ is an INFORMATION observable (DERIVED, a function of ρ alone) and an
energy observable only by CORRESPONDENCE (hosted). Candidate bridges: A (information =
energy, QG89 postulate — the root), B (entropy deficit = vacuum, QG230 label — inherits A),
C (occupancy fraction = density fraction, QG234 numeric ID), D (pure numerical
correspondence — the residue). Classification: ρ → H → I_occ → I_occ/ln K DERIVED;
"energy = actualization rate" (QG89) BOUNDARY (irreducible definition); I→ρ_Λ and ΩΛ-label
CORRESPONDENCE (hosted); dimensional conversion G = ħc/M_Pl² BOUNDARY (imports ħ, c);
ΩΛ as an energy observable CORRESPONDENCE. No new primitive; canonical AT unchanged.

ResearchY-NP_059 (Actualization Rate Audit, COMPLETE, tests 7/7 PASSED): can "energy =
actualization rate" (QG89) be DERIVED instead of postulated? Verdict: NO — it is BOUNDARY
(an irreducible definition). What IS derived is the conserved COUNT: the actualization rate
is the normalized count density ρ with Σρ = 1 (QG194/QG216/M_005), and the deficit
m = ρ̄ − ρ is exactly conserved (Σm = 0, QG194's "Noether count"). The step from "conserved
count" to "energy" has no Noether route: AT's time is DISCRETE (Δθ = 2πk/N per tick, "time IS
the tick count", NP_003/M_009/M_010), and QG244's Lagrangian presupposes QG89's
"actualization-flow energy" rather than deriving it. Removing QG89 breaks every energy
derivation (matter = deficit QG194, Λ QG230, Lagrangian QG244, mass ladder QG117–127) but the
information chain (ρ → H → I_occ → 0.6839) and count conservation survive. The dimensionful
conversion (count → Joules/GeV) additionally needs the BOUNDARY anchors v, m_e and unit
conventions ħ, c (NP_029). Two layers: Layer 1 (DERIVED) = the actualization rate is a
conserved count (Σρ = 1; Σm = 0); Layer 2 (BOUNDARY) = "that count ≡ energy (with units)",
the QG89 definition + anchors. Deepest open step in the information-to-energy bridge: QG89's
identification of the conserved actualization count with energy, plus the anchored
dimensionful conversion (v, m_e, ħ, c). No new primitive; canonical AT unchanged.

ResearchY-NP_060 (Dark Energy Resource Audit, COMPLETE, tests 9/9 PASSED): does ΩΛ
represent a physically extractable resource, or only an informational state descriptor?
Verdict: ΩΛ = I_occ/ln K = 0.6839 CANNOT perform work — it is an informational STATE
DESCRIPTOR (and bookkeeping quantity, B = C), not a resource. It fails all three resource
requirements: no energy scale (dimensionless), no gradient (single snapshot number), no
temperature channel (NP_030). Extraction channels: (a) vacuum work d(ρV) = −p dV is HOSTED
(no derived EoS, NP_056; w = −1 grows energy, w = 0 conserves); (b) information work
W = k_B T ln 2 (Landauer/Szilard) needs T (absent); (c) free energy F = U − TS needs U and T
(neither derived). Tracking: under any extraction nothing flows — I_occ = 0.7513 and
H = 0.3473 are invariant functions of ρ, and Σρ = 1 / Σm = 0 are conserved counts (not
energy). Readings: A) extractable work REFUTED; B) state descriptor YES; C) bookkeeping YES
(= B); D) hidden conserved resource NO. Inversion: the extractable/gravitating side is Ωm
(matter = deficit, QG194), NOT ΩΛ (the surplus). Comparison: vacuum energy does work
(hosted), free energy is undefinable, ΩΛ is a pure information measure with no conversion
channel. Classification: ΩΛ information state descriptor DERIVED; ΩΛ as physical resource
REFUTED; vacuum-energy work channel CORRESPONDENCE (hosted); conserved counts DERIVED (not
resources); temperature/free energy BOUNDARY. No new primitive; canonical AT unchanged.

ResearchY-NP_061 (ΩΛ Coincidence Audit, COMPLETE, tests 8/8 PASSED): why does
ΩΛ = I_occ/ln K = 0.6839 match the observed cosmological fraction to 0.12%? Verdict: the
match is a CORRESPONDENCE (fragile point-match) with a BOUNDARY ingredient — not a deep
physical relation and not a coincidence. Stripping energy language leaves a numerical
equality between a DERIVED information number (0.6839) and an OBSERVED cosmological fraction
(0.6847, dev 0.12%). Sensitivity (the match is fragile): K sensitivity — K=2/4/5 shift ΩΛ to
0.5801/0.6263/0.5732 (corpus rung ladder: 0.4773/0.8153/0.8945 at N=48/192/384, ~20–30% off);
occupancy sensitivity — [5,4,86]→0.6555, [2,2,91]→0.8145; measure sensitivity — only KL
matches (0.6839), Hellinger 0.1917, TV 0.5302, χ² 1.3896 all fail. Determination: not
necessary (no logical entailment), not emergent (fragile), not numerological (no free
parameter). The "why" has no derived answer: the equality is the hosted QG89 identification
(information budget ↔ energy budget), and its 0.12% precision rests on two non-derived
ingredients — the KL measure (EMERGENT choice, QG_018 OP1 open) and the K=3 family window
(BOUNDARY, "anchored by observed ΩΛ", QG_013) — a documented retro-selection risk (QG_239).
Classification: deep physical relation REFUTED; pure coincidence REFUTED; CORRESPONDENCE
(fragile point-match) with a BOUNDARY ingredient. The match is real and principled but its
precision is enabled by anchored inputs, not derived end-to-end. No new primitive; canonical
AT unchanged.

ResearchY-NP_062 (High-Order Universe Audit, COMPLETE, tests 7/7 PASSED): why is the
realized D96 occupancy so highly ordered (far from uniform)? Verdict: reality is highly
ordered because the count density ρ is the normalized occupancy of a DISCRETE 1D circulant
spectrum (λ_k = 2−2cos(2πk/N), N=96) that is top-heavy BY CONSTRUCTION. Order measure:
uniform/random occupancies give ΩΛ ≈ 0 (H = ln 3 = 1.0986 max entropy); the canonical
[4,4,87] gives H = 0.3473 (only 32% of capacity) and I_occ = 0.7513 (ΩΛ = 0.6839) — 68% of
capacity unrealized as entropy. Spectral origin: the top-heaviness is UNIVERSAL across
C_N(±1..±6) — ~92% top-octave share at N=48/96/120/192 — from the 1D linear dispersion
ω_k ≈ c·k (ratios 0.62, 0.61, 0.60, 0.58) and the finite UV cap ω_max = 3.98. High order is
TYPICAL and REQUIRED (structural), NOT dynamically selected (N→occupancy is a bijection,
NP_037). [4,4,87] is a SELECTION OUTCOME (occupancy of canonical N=96), not an attractor or
fixed point; its top-heaviness DERIVED, its exact value BOUNDARY (3-family window). The
earliest source of the large information surplus is the DISCRETE SPECTRUM itself — the uniform
state (ρ = 1/K) is a max-entropy REFERENCE (QG227), not a realized baseline. Classification:
top-heavy occupancy + I_occ + ΩΛ DERIVED; high-order-being-typical/required DERIVED; uniform
state as reference BOUNDARY; exact N=96 BOUNDARY; dynamical attractor/selection REFUTED. No
new primitive; canonical AT unchanged.

ResearchY-NP_063 (Cosmological Coincidence Audit, COMPLETE, tests 7/7 PASSED): if ΩΛ_AT =
0.6839 is an information observable and ΩΛ_obs = 0.6847 is a cosmological density fraction,
why are they numerically equal? Verdict: the equality is a DESCRIPTOR link (B) via an unproven
hidden common origin (E, ρ) — AT posits the two are the information face and the energy face
of one count density ρ. Systematic link enumeration: A) causal REFUTED (a mathematical property
cannot cause a measurement); B) descriptor YES (closest — but presupposes the bridge); C)
scaling law REFUTED (no derived formula ΩΛ_obs = f(ΩΛ_AT); it is a point equality, not a law);
D) coincidence PARTIAL (principled, but fragile point-match); E) hidden common origin UNPROVEN
(valid only if the energy face is derived — it is not). Minimal assumption chain (exactly four,
all non-derived): 1) KL measure (EMERGENT choice — the FIRST non-derived step), 2) N=96/K=3
window (BOUNDARY, anchored to ΩΛ_obs), 3) "energy = actualization rate" (QG89, BOUNDARY — the
DEEPEST step), 4) dimensionful anchors v, m_e, ħ, c (BOUNDARY). Note: ΩΛ_obs is itself a
MEASUREMENT (empirical input) — no theory derives a measurement; the question is only where the
identification sits. Classification: ΩΛ_AT DERIVED; ΩΛ_obs MEASURED; equality CORRESPONDENCE
(descriptor); KL measure EMERGENT; N=96 window / QG89 / anchors BOUNDARY; causal/scaling-law/
deep-derived-relation REFUTED; hidden common origin ρ unproven. No new primitive; canonical AT
unchanged.

ResearchY-NP_064 (Canonical Structure Necessity Audit, COMPLETE, tests 7/7 PASSED): why does
the canonical structure {N=96, K=3, [4,4,87]} exist? Verdict: it is a DERIVED-BOUNDARY hybrid.
The period-3 seed (DERIVED, D_040) forces N = 3·2^k (octave rung) and 6|N; the 3-family window
[4,8) (BOUNDARY, anchored to ΩΛ_obs, QG_013) selects k=5 — together pinning N=96. Verified:
among the octave rungs 3·2^k, only k=5 (N=96) has span 6.4025 ∈ [4,8) (k=4 → 3.24, 2 families;
k=6 → 12.78, 4 families; k=7 → 25.54, 5 families). From N=96 everything downstream is DERIVED:
span 6.40 → K=3 → occupancy [4,4,87] → I_occ = 0.7513 → ΩΛ = 0.6839. Removal analysis: removing
N=96 (ΩΛ → 0.5801/0.7295, corpus rung ladder 0.4773/0.8153), K=3 (→ 0.5801/0.6263), or
perturbing [4,4,87] (→ 0.6555/0.8145) all break ΩΛ; mass content m_μ/m_e also breaks off N=96
(NP_037 R2). The structure is not arbitrary, not dynamically selected, not accidental — it is
the deterministic consequence of one derived seed and one boundary window. THE TRUE ROOT from
which ΩΛ = 0.6839 emerges: the period-3 seed (DERIVED) constrained by the 3-family window
(BOUNDARY), which together pin N=96; below them the discrete tick (QG_011) is the deepest
single boundary. Classification: period-3 seed and N=96 DERIVED (D_040); 3-family window
BOUNDARY (anchored); K=3 value DERIVED / window BOUNDARY (two-level rule); occupancy [4,4,87]
and ΩΛ DERIVED; selection/accident REFUTED. No new primitive; canonical AT unchanged.

ResearchY-NP_065 (Dark Matter Ontology Audit, COMPLETE, tests 7/7 PASSED): what is Dark
Matter (Ωm) inside AT, given ΩΛ is an information descriptor? Verdict: Dark Matter = the
matter DEFICIT m = ρ̄ − ρ — an EFFECT, not a particle — whose fraction Ωm = H/ln K = 0.3161 is
the realized-entropy fraction (the bookkeeping complement of ΩΛ), matching the observed Ωm =
0.3153 to 0.26%. The deficit is conserved (Σm = 0 exactly), positive in the under-occupied low
octaves [4,4] (0.2912 each) and negative in the over-occupied top octave [87] (−0.5825): matter
is under-density, dark energy is over-density. Readings: C (realized-state fraction) = D
(bookkeeping complement) = B (the QG194 matter identification); A (physical matter) = hosted
energy (E_def = m, QG89) + DERIVED gravity. Ontology: matter = deficit (QG194, gradient-source
∇m = −∇ρ, conserved), T_μν = (ρ̄−ρ)v_μv_ν recovered without defining matter (QG195), flat
rotation α = 0 (QG206), M ∝ R (QG184) — all DERIVED, an effect not a particle, no
CMB/structure role (QG229). THE ASYMMETRY (key): the deficit (Ωm/matter) SOURCES gravity (a
DERIVED role), while the surplus (ΩΛ/dark energy) is a descriptor with no derived effect
(NP_060) — so Ωm has STRONGER physical meaning than ΩΛ. Caveats: both share the hosted QG89
energy reading; "dark matter" as a particle (non-baryonic, structure-formation) is REFUTED; the
baryonic/dark split is BOUNDARY (not derived). Classification: Ωm = H/ln K DERIVED (QG234);
matter = deficit + Σm = 0 DERIVED (QG194); gravitational role DERIVED (QG195/206/184); energy
reading CORRESPONDENCE (hosted QG89); particle REFUTED; baryonic/dark split BOUNDARY. No new
primitive; canonical AT unchanged.

ResearchY-NP_066 (Dark Matter Evidence Audit, COMPLETE, tests 8/8 PASSED): which observed
dark-matter phenomena does the deficit m = ρ̄ − ρ reproduce? Verdict: explanatory power is
PARTIAL and sharply partitioned — 2 DERIVED / 2 CORRESPONDENCE / 2 REFUTED. The deficit
reproduces the GRAVITATIONAL-POTENTIAL half: (1) flat rotation curves (v² ∝ r^(−α), flat ⇔
α = 0, QG206) DERIVED; (6) CMB matter fraction Ωm = H/ln K = 0.3161 (0.26%) DERIVED; (3)
cluster dynamics (Coma M_vir = 8.8e14 M☉ = 6.7× baryon) CORRESPONDENCE — matched but
degenerate with ΛCDM (both need ~85% dark mass); (5) large-scale structure CORRESPONDENCE —
the Poisson seed (δ_i = 1/√⟨N⟩) and deficit-dust growth (δ ∝ a, QG231) are derived, but the
power-spectrum shape/acoustic peaks are hosted (QG238). It FAILS the particle and
light-bending half: (2) gravitational lensing REFUTED — the conformal ρ-only metric gives PPN
γ = −1 (no null-geodesic bending, QG26); lensing needs the non-conformal ψ tensor sector (a
second primitive, QG212); (4) Bullet Cluster REFUTED — the deficit is a scalar field, not a
collisionless particle, so it cannot separate from the shocked gas ("Bullet Cluster requires
particle DM; hybrid needed"). The deficit is a GRAVITATIONAL-POTENTIAL SURROGATE for dark
matter, not a full dark matter. Classification: rotation + Ωm DERIVED; cluster mass + LSS seed
CORRESPONDENCE; lensing (deficit alone) REFUTED; Bullet Cluster REFUTED. No new primitive;
canonical AT unchanged.

ResearchY-NP_067 (Lensing Sector Audit, COMPLETE, tests 9/9 PASSED): why does the deficit
source gravity but fail light-bending? Verdict: the ρ-only metric is CONFORMALLY FLAT
(g = ρ^(2/d)η), giving PPN γ = −1, which cancels the null-geodesic prefactor (1+γ)/2 = 0 — so
all lensing observables (deflection, convergence, shear, magnification) and the Shapiro delay
vanish — while the potential effects (rotation, cluster mass, redshift, Ωm) survive because
they depend on g₀₀ = −ρ^(2/d) alone. Origin of γ=−1: the conformal factor scales time and space
EQUALLY, and null geodesics are conformally invariant. The minimal fix is the ψ TENSOR SECTOR
(the second primitive): g₀₀ = −ρ^(2/d)e^(2ψ) breaks conformal flatness ⇒ γ = +1 ⇒ full lensing,
weak/strong lensing, Shapiro delay, frame dragging (QG186), and GW (QG43/44) restored at GR
strength (QG212 OPTICS RESOLVED — no-lensing is a RESTRICTED ψ=0 sector). Lensing/Shapiro/γ
need only a SCALAR ψ (1 d.o.f.); the full spin-2 is required only by GW polarization (QG43).
Primitive cost: +1 (ψ, the second primitive, QG223). Determination: B) MISSING TENSOR SECTOR —
not fatal (QG212 shows ψ=0 is an assumption, physical optics is GR-like), not
correspondence-only. Classification: conformal metric (γ=−1) DERIVED; no-lensing in ψ=0
DERIVED; ψ tensor sector BOUNDARY (second primitive); lensing restored by ψ CORRESPONDENCE (GR
strength); fatal failure REFUTED. No new primitive; canonical AT unchanged.

ResearchY-NP_068 (Psi Ontology Audit, COMPLETE, tests 8/8 PASSED): what is ψ physically?
Verdict: ψ is the SPIN-2 (WEYL-CURVATURE) GRAVITON — the tensor (traceless) face of the founding
Difference — a PRIMITIVE physical degree of freedom (massless spin-2, 2 polarizations), not
auxiliary, not hosted, not emergent. Inventory: ψ = metric completion (g₀₀ = −ρ^(2/d)e^(2ψ),
QG207), lensing (γ=+1, QG212), frame dragging (h_0i, QG186), GW polarization (spin-2, QG43/44).
Remove ψ → lensing (γ→−1), frame dragging (h_0i→0), and GW break, but the scalar potential
effects (g₀₀) survive. Interpretations: A) geometry correction PARTIAL (completes the metric,
but a full sector); B) information field NO (ρ is the information face; ψ is the geometric
face); C) curvature field YES (the Weyl/traceless curvature conformal flatness lacks); D)
physical d.o.f. YES (the graviton). ψ = C = D realized as A; not B. Status: auxiliary NO (real
observables), hosted NO (necessity derived — spin-0 cannot source spin-2, QG19), emergent
REFUTED ("no scalar saturation reaches spin 2"), primitive YES (second primitive, QG223).
Difference Duality (QG286/301) refines ψ as the TENSOR FACE of the ONE Difference: ρ = trace
(conformal, Weyl=0), ψ = traceless (Weyl curvature) — together they complete the metric.
Classification: ψ graviton PRIMITIVE (QG223); emergent route REFUTED (QG19); Difference Duality
DERIVED (QG286/301); information-field reading REFUTED; lensing/frame/GW CORRESPONDENCE (GR
strength). No new primitive; canonical AT unchanged.

ResearchY-NP_069 (Expansion Ontology Audit, COMPLETE, tests 7/7 PASSED): what does cosmic
expansion physically mean in AT, without importing ΛCDM? Verdict: expansion is the BRANCHING
GROWTH of the actualization count ρ (ρ_{k+1} = μ·ρ_k, ∂_t ρ = ln(μ)·ρ), which carries the metric
g = ρ^(2/d)η with it — the metric scale IS ρ^(2/d) (A = B). The FRW scale factor a = ρ^(1/d)
(QG77) is a HOSTED homogeneous-isotropic relabeling of the same count; q₀/z_acc are hosted FRW
closures. Criticality subtlety: at μ=2 the count doubles each generation (ln 2 = 0.6931); at μ=1
(criticality) the mean is STATIC (∂_t ρ = 0) and only the VARIANCE grows (Var(Z_k) = k·σ², the
residual pressure QG230 identifies with Λ) — there is no native accelerating scale factor.
Earliest growing object: the branching count ρ_k = μ^k/S (the actualization population, the
same quantity QG89 calls energy). Classification: branching growth + metric scale + native
dynamics + redshift DERIVED (QG1/222/197/26); FRW a = ρ^(1/d) CORRESPONDENCE (QG77, hosted);
accelerated expansion HOSTED (NP_056); a native accelerating scale factor REFUTED. No new
primitive; canonical AT unchanged.

ResearchY-NP_070 (Criticality Audit, COMPLETE, tests 7/7 PASSED): why is the canonical
universe critical (μ=1)? Verdict: μ=1 is the UNIQUE branching ratio that is simultaneously
marginal-stable (non-extinct, non-runaway), scale-free (L = 1/|ln μ| = ∞, renormalization-
invariant), and maximum-entropy (α=0, uniform per-octave) — three independent criteria that
coincide at μ=1 (QG7). Regimes: μ<1 extincts (q=1, finite total), μ>1 runs away (q<1,
exponential total), only μ=1 is marginal (linear total). μ≠1 breaks scale-freeness (α≠0), hence
flat rotation and the self-similar deficit. Criticality is DERIVED (unique), conditional on
SCALE-FREENESS (renormalization invariance, the indifference principle AT-F1: "the primitives
carry no intrinsic scale") as the single BOUNDARY input. Chain: Q-events → critical branching
(μ=1) → α=0 → ρ → gravity, with criticality itself derived. Classification: criticality μ=1
DERIVED (unique, QG7); scale-freeness BOUNDARY (AT-F1); α=0 flat rotation DERIVED (QG206);
sub/supercritical universe REFUTED. No new primitive; canonical AT unchanged.

ResearchY-NP_071 (Matter Ontology Audit, COMPLETE, tests 6/6 PASSED): what is matter
physically in AT? Verdict: matter is the DEFICIT m = ρ̄ − ρ — a stable, self-bound EXCITATION (a
deficit pattern) of the count density, realized as the under-occupancy that sources gravity and
clumps into structure. Interpretations: A) point objects REFUTED (not a particle); B) occupancies
YES; C) resonant structures PARTIAL (the D96 modes are the substrate); D) stable wave structures
YES (= B — the founding README claim "matter consists of dynamically stabilized wave structures"
made precise as the deficit); E) solitons LEGACY (the superseded TRM "proto-matter = solitons").
Category: substance NO (matter is not fundamental), process PARTIAL (arises from actualization),
pattern YES, excitation YES (the corpus's "deficit excitation": ρ real-underived, matter = deficit
excitation). Stability: only the converging (deficit) branch supports clumping — matter = deficit
is DERIVED from STABILITY (ATF MatterAttraction). Classification: matter = deficit DERIVED
(QG194); mass ladder DERIVED (QG173/209); point particle REFUTED; soliton LEGACY; fundamental
substance REFUTED. No new primitive; canonical AT unchanged.

ResearchY-NP_072 (Particle Ontology Audit, COMPLETE, tests 6/6 PASSED): if matter is a deficit
excitation, what are particles — and what is an electron? Verdict: a particle is a RESONANCE
CLASS — a mode (frequency attractor) of the D96 spectrum, organized into octave-band families,
with mass = anchor × dimensionless D96 ratio. Interpretations: A) localized deficits NO (the
deficit is bulk matter); B) resonance classes YES; C) occupancy configurations YES (= B, the
mode-access classes of [4,4,87], QG150); D) measurement observables PARTIAL (modes exist before
measurement). Matter vs particle: matter = the deficit over the modes; a particle = a single
mode. Emergence: the spectrum (95 modes), families (3 octave bands), and mass ratios (m_μ/m_e ≈
207) are DERIVED; the absolute scale m_e = 0.511 MeV is BOUNDARY (anchor, D_012/D_013). The
ELECTRON = the lightest fermion mode (the octave bottom, occ₀ = 4), whose mass is the boundary
anchor setting the fermion scale; muon/tau/quarks are derived ratios of it. Particles are
EMERGENT (derived modes), not fundamental point objects. Classification: modes + families + mass
ratios DERIVED (D_041/QG210/QG173/209); m_e BOUNDARY; localized point particle REFUTED;
fundamental REFUTED. No new primitive; canonical AT unchanged.

ResearchY-NP_073 (Resonance Selection Audit, COMPLETE, tests 6/6 PASSED): why do only specific
D96 modes appear as particles (e/μ/τ), while most of the 95 modes do not? Verdict: only 3 of the
95 modes appear as particle generations because the spectrum organizes into 3 octave bands =
3 families (QG210), and within each band only the STABLE bottom mode survives (metastability,
QG125), selected by its quantum numbers via isospin-constrained mode access (QG150, r=0.955).
The top band (87/95 = 91.6%) is the bulk deficit (matter), not individual particles. Selection
rules (all DERIVED): occupancy (3 bands), symmetry (isospin r=0.955, down=full-spectrum up=dense-
band), stability (higher modes decay), family structure (3 generations, no 4th because span < 8).
Classification: family structure + mode access + metastability DERIVED (QG210/150/125); "all 95
modes are particles" REFUTED; the precise particle→label assignment PARTIAL (QG271 frontier). No
new primitive; canonical AT unchanged.

ResearchY-NP_074 (Quantum Number Ontology Audit, COMPLETE, tests 7/7 PASSED): if particles are
resonance classes, what are quantum numbers? Verdict: quantum numbers are the GENERATORS of the
D96 automorphism group — symmetry charges (C) that act as occupancy-access rules (B). The gauge
symmetry is 1+3+8 = 12 generators (the 12 link-directions of C_96(±1..±6)). Charge = U(1) =
Z_96 rotation subgroup (the photon charge) DERIVED; isospin = SU(2) = Z2 doublet (reflection
σ_z = T3, rotation σ_y, commutator σ_x) DERIVED; color = su(3) = 3²−1 = 8 from the 3 families
(structure DERIVED, count 3 = BOUNDARY postulate, QG79); hypercharge = Y = Q − T3 (derived
combination). Interpretations: A) mode labels PARTIAL; B) occupancy-access rules YES; C) symmetry
charges YES (= B); D) correspondence objects PARTIAL (color count only). Uniqueness: charge,
isospin, and color-structure follow uniquely; the color COUNT does not (QG79). The electron's
charge (−1) = its U(1) rotation eigenvalue (the photon charge), not a SM label. Classification:
charge + isospin + color-structure + hypercharge DERIVED (QG161); color count BOUNDARY (QG79);
"quantum numbers as SM labels only" REFUTED. No new primitive; canonical AT unchanged.

ResearchY-NP_075 (Force Ontology Audit, COMPLETE, tests 7/7 PASSED): if particles are resonance
classes and quantum numbers are symmetry charges, what is a force? Verdict: a force is the ACTION
of a D96 symmetry generator — a symmetry action (B) that induces a resonance transition between
modes (D, the vertex ⟨f|T^a|i⟩). EM/weak/strong = the 1+3+8 = 12 generator actions (QG161/243);
gravity = the metric geometry (ρ + ψ), NOT a gauge force. Interpretations: A) particle exchange
PARTIAL (the boson is a link excitation, not a matter particle); B) symmetry action YES; C)
occupancy transfer PARTIAL; D) resonance transition YES (= B). Gauge bosons are the SYMMETRY
GENERATORS (link excitations, QG57), not particles, not operators. The photon = the U(1) = Z_96
rotation generator (the unique neutral rotation link excitation), coupling e = √(4π/137). Each
force = a Noether-conserved generator action (QG89: U(1)→charge, SU(2)→isospin, SU(3)→color) with
a D96-normalized coupling (QG162) and the vertex = ⟨f|T^a|i⟩. Classification: gauge forces +
bosons + couplings DERIVED (QG161/243/162); gravity DERIVED (QG197/222) + ψ primitive; "force =
matter-particle exchange" REFUTED. Forces share the same ontology as particles and quantum
numbers: the D96 structure in action. No new primitive; canonical AT unchanged.

ResearchY-NP_076 (Psi Dark Matter Audit, COMPLETE, tests 8/8 PASSED): can the ψ graviton
sector account for part of the Dark Matter signal? Verdict: NO — ψ (the massless spin-2
graviton) contributes ZERO dark-matter mass. Partition: the deficit carries the mass
(rotation/cluster/Ωm), ψ carries the optics (lensing γ=+1/frame-dragging/GW). What ψ
produces: A) propagating waves ONLY (massless spin-2, QG43/44); B) stationary bound
configurations REFUTED (m_ψ = 0, no Yukawa binding, linear field non-self-interacting);
C) effective mass density REFUTED (w = 1/3, ρ ∝ a⁻⁴, Ω_gw ≈ 10⁻⁹ ≪ Ωm = 0.3161). ψ
fails every dark-matter requirement: cold (v=c), pressureless (w=1/3), a⁻³ scaling
(a⁻⁴), Ω=0.316 (1e-9), halo clustering (no self-binding), collisionless separation (not
a particle). ψ's true role = restore LENSING so the deficit's mass is visible — the
deficit is the dark matter, ψ is the gravity (geometry) that reveals it (NP_075).
Model comparison: deficit-only = mass without optics; ψ-only = optics without mass;
deficit+ψ = full signal except Bullet, but ψ is the graviton, not dark matter.
Classification: ψ waves DERIVED (QG43/44); bound configs / mass density / mimicry all
REFUTED; deficit+ψ partition DERIVED (NP_066/067). Success criterion: Dark Matter (the
mass) = DEFICIT-ONLY; ψ is the graviton, not a dark-matter component. No new primitive;
canonical AT unchanged.

ResearchY-NP_077 (Structure Formation Audit, COMPLETE, tests 9/9 PASSED): can the deficit
field naturally produce halos, galaxy profiles, and cluster structure without particle
dark matter? Verdict: B — the deficit forms structure only with EXTRA ASSUMPTIONS. The
seed and linear growth are DERIVED (QG231): Poisson seed δ_i = 1/√⟨N⟩, scale-free critical
variance Var(2k)/Var(k)=2, pressureless deficit dust T_μν = ρ_m v_μ v_ν, linear growth
δ ∝ a, scalar tilt n_s = 0.96497 (0.007%). But the profile shape is NOT dynamical: flat
rotation (v² ≈ const ⇒ M ∝ r ⇒ ρ ∝ r⁻², the SIS) requires the α = 0 log-deficit abundance
law, which is a SYMMETRY SELECTION (PREFERRED/BOUNDARY), not a dynamical attractor
(G4-RHO Phase 0: conservation gives the repulsive ρ ∝ r⁻², scale-freeness gives a
continuum). The natural deficit profile is the SIS ρ ∝ r⁻² — STEEPER than the NFW inner
cusp (r⁻¹) and singular at the center (no core) — predicting a cuspy non-cored halo; NFW
concentration is FITTED (ClusterMassAudit); clusters match ΛCDM only degenerately (Coma
M_vir = 8.75×10¹⁴ M☉, 6.7× baryon, R_vir/NFW fitted). Determination: A ("naturally forms
halos") REFUTED; B ("extra assumptions") YES; C ("fails structure formation") REFUTED.
Classification: seed + linear growth DERIVED (QG231); scale-free critical clustering
DERIVED (QG227/228); α=0 abundance law BOUNDARY (symmetry selection, G4-RHO);
flat-rotation / M∝r halo profile CORRESPONDENCE (G4-ME); NFW/cored concentration FITTED.
No new primitive; canonical AT unchanged.

ResearchY-NP_078 (Alpha=0 Necessity Audit, COMPLETE, tests 9/9 PASSED): why must the
universe select α=0 instead of α≠0? Verdict: α=0 is the UNIQUE point where four
independent criteria coincide — flat rotation (v² ∝ r^(−α), slope 0 only at α=0),
stability (equal-deficit-per-octave; α<0 diverges, α>0 concentrates), criticality
(μ=1 ⟺ α=0, NP_070), and maximum entropy (uniform per-octave). The α-scan [−2,+2]:
only α=0 gives a flat curve and M ∝ R (exponent 1−α = 1); every α≠0 breaks one. α=0 is
DERIVED (unique) as a SELECTION fixed point (the RG fixed point of the octave
renormalization), NOT a dynamical attractor (no equation of motion produces it — G4-RHO:
conservation → repulsive ρ∝r⁻², scale-freeness → continuum). Reconciliation: QG206
"derived" = uniquely selected by criteria; G4-RHO "not dynamical" = no equation of motion
forces it — compatible. Earliest source of α=0 = scale-freeness (renormalization
invariance) = the indifference principle AT-F1 ("primitives carry no intrinsic scale").
Classification: α=0 flat rotation DERIVED (QG206); μ=1⟺α=0 criticality DERIVED
(QG7/NP_070); α=0 unique stable (RG fixed) point DERIVED (QG206); scale-freeness BOUNDARY
(AT-F1, the FINAL boundary); α=0 as a dynamical attractor REFUTED (G4-RHO); canonical α≠0
universe REFUTED. Success criterion: scale-freeness is the FINAL BOUNDARY (not derived);
α=0 is DERIVED (uniquely) given it. No new primitive; canonical AT unchanged.

ResearchY-NP_079 (Scale-Freeness Origin Audit, COMPLETE, tests 10/10 PASSED): can
scale-freeness (AT-F1 "the primitives carry no intrinsic scale") be derived from
Difference itself, or is it the final irreducible boundary? Verdict: scale-freeness is
DERIVED from Difference — NOT the final boundary (refines NP_078). Difference is a
BINARY (all-or-nothing) distinction relation carrying no metric, no magnitude, no scale,
so the primitives it grounds (Q-events = before→after differences, the counting measure
= a density of weight d, scale-covariant, the causal order = a scale-invariant partial
order) carry no intrinsic scale. Scale-freeness then follows as the unique
renormalization-invariant abundance (the power law n ∝ R⁻ᵖ, the RG fixed point):
AT-F1 = indifference = scale-freeness = renormalization invariance (all one statement).
Introducing a finite intrinsic scale λ breaks everything (μ≠1, α≠0, scale-setting
structure); λ→∞ recovers self-similarity; λ→0 is still a UV cutoff. Hidden-assumption
sweep: counting (covariant density), actualization (binary), occupancy [4,4,87]
(dimensionless), normalization (dimensionless) — no hidden scale. Determination:
A (follows from Difference) YES; B (follows from counting) YES; C (equivalent to
indifference) YES; D (irreducible) REFUTED. Deepest source of AT-F1 = DIFFERENCE itself
(binary, metric-free). Classification: scale-freeness DERIVED (conditional, QG270/AT-F1);
Difference's binary nature BOUNDARY (the true final boundary, QG270); "scale-freeness
irreducible" REFUTED; canonical preferred-scale λ universe REFUTED. Success criterion:
the deepest source of AT-F1 is Difference; the true final boundary is Difference, not
scale-freeness. No new primitive; canonical AT unchanged.

ResearchY-NP_080 (Difference Duality Audit, COMPLETE, tests 11/11 PASSED): why does Difference
split into exactly one scalar face (ρ) and one tensor face (ψ), not one or many fields? Verdict:
Difference actualizes into a SYMMETRIC rank-2 object (the connectivity/stress tensor A_ij = A_ji,
a link connects a pair), and a symmetric rank-2 tensor has an EXHAUSTIVE, UNIQUE decomposition
into exactly two irreducible representations: the spin-0 TRACE (1 component = ρ, the scalar/count/
isotropic face) and the spin-2 TRACELESS part (5 components = ψ, the tensor/orientation/Weyl face;
2 physical TT polarizations + and ×). 6 = 1 + 5 at d=3 — no third component, no vector (spin-1)
face (the antisymmetric part is zero for a symmetric object), no spin-≥3 (rank-2 caps at spin-2).
Removal: remove ψ → conformally flat (lensing/frame-dragging/GW break, scalar survives); remove ρ
→ no count/magnitude (information/Ωm/ΩΛ break). Only scalar incomplete, only tensor incomplete,
scalar+tensor complete. Group theory: trace = conformal/isotropic (ρ), traceless = Weyl/anisotropic
(ψ = the Weyl content, QG285). Determination: A (duality DERIVED — the rank-2 decomposition
theorem), not B (boundary), not C (accidental). Primitive cost = 1 (Difference): the two
"primitives" (ρ, ψ) collapse to ONE primitive with two faces (QG286). Classification: {ρ, ψ}
duality DERIVED (QG286/301); rank-2 symmetry DERIVED (connectivity links pairs); d=3 DERIVED
(QG2/197); η reference (defines trace/traceless/conformal/Weyl) FRAMEWORK/BOUNDARY (QG291); third
face REFUTED (6=1+5 exhaustive); ψ as independent primitive REFUTED (traceless face of the one
Difference). Success criterion: exactly one scalar + one tensor face because a symmetric rank-2
difference decomposes as exactly spin-0 ⊕ spin-2. No new primitive; canonical AT unchanged.

ResearchY-NP_081 (Energy Ontology Audit, COMPLETE, tests 9/9 PASSED): what is energy physically
inside AT? Verdict: energy is a RELABELING of actualization dynamics — NEITHER fundamental NOR
emergent. Removing all energy language loses nothing derived: ΩΛ = I_occ/ln K = 0.6839 is pure
information, masses are spectral ratios, forces are generator actions. The conserved quantity is
the COUNT: Σρ = 1 (normalization) and Σm = Σ(ρ̄−ρ) = 0 (deficit conservation), both DERIVED;
Noether fails (discrete time Δθ = 2πk/N, no native Lagrangian — QG244 presupposes QG89). A)
count density PARTIAL (the underlying object, but dimensionless); B) count flow NO (Σρ=1 static);
C) actualization rate YES (as a DEFINITION, QG89); D) emergent NO; E) boundary definition YES.
Energy = the conserved count, renamed (QG89 "energy = actualization rate") + unit-ized (anchors
v, m_e + unit conventions ħ, c, NP_029). Alternative formulation = count + one scale anchor
(QG289 minimal inventory: framework {η, 3+1, π} + one scale; no free constant). Classification:
conserved count DERIVED; actualization rate DERIVED; "energy = actualization rate" BOUNDARY
(definition); dimensionful energy BOUNDARY (anchors + ħ, c); energy as fundamental REFUTED;
energy as emergent REFUTED. Success criterion: energy is a relabeling — the conserved count
wearing Joules. No new primitive; canonical AT unchanged.

ResearchY-NP_082 (Electron Mass Anchor Audit, COMPLETE, tests 8/8 PASSED): why does the fermion
spectrum require the electron mass anchor? Verdict: m_e is NOT the true remaining matter-scale
boundary — it is a REPLACEABLE unit conversion. All fermion mass ratios are DERIVED (dimensionless:
m_μ/m_e = Σm²/√occMom = 207.03, m_τ/m_μ = √occMom·λ₂ = 16.842, quarks, Yukawas); the absolute
scale m_e = 0.511 MeV carries only the DIMENSION (MeV), which no derived D96 invariant can supply
(span 6.4025, occupancy [4,4,87], count Σρ=1/Σm=0, A = 95·44·87 = 363660 — all dimensionless).
Removing m_e loses absolute values but NOT the ratios. Replacements: v (another dimensionful
scale) moves the boundary but doesn't remove it; A/span/occupancy/count (all dimensionless) cannot
replace m_e. Determination: D (unit conversion only); A (fundamental boundary for m_e), B (hidden
composite), C (derived scale) REFUTED. The true boundary is the CATEGORY "one dimensionful scale
must be imported to dimensionalize the dimensionless spectrum"; m_e is a replaceable instance
(m_e ↔ M_Z ↔ v; QG289: only ONE empirical scale strictly needed). Earliest unavoidable scale = at
contact with experiment (dimensionful comparison). Classification: mass ratios DERIVED
(QG173/209); "one dimensionful scale" BOUNDARY (irreducible); m_e as a specific value
CORRESPONDENCE (replaceable calibration); m_e fundamental/unique boundary REFUTED; m_e composite/
derived REFUTED. Success criterion: m_e is NOT the true boundary — the true boundary is "one
dimensionful scale", m_e its replaceable unit-conversion instance. No new primitive; canonical AT
unchanged.

ResearchY-NP_083 (Threefold Structure Audit, COMPLETE, tests 9/9 PASSED): do the remaining
appearances of 3 (color count = 3, family count = 3) share a common origin? Verdict: B — a
HIDDEN COMMON DERIVATION. Both 3s descend from the period-3 seed p = 3 (DERIVED, D_040), which
forces the factor 3 in N = 3·2^k → 3 octave bands [4,4,87] → 3 families (value DERIVED,
floor(log₂ span)+1 = 3) AND the su(3) color algebra (8 = 3²−1 generators DERIVED from the 3
family directions, QG161/242); the color count 3 is the octave-3 IDENTIFICATION (QG79, BOUNDARY).
Two boundary residues remain: the family window [4,8) and the color identification QG79. Removal:
removing family-3 breaks su(3) (→su(2)=3/su(4)=15) AND masses/ΩΛ; removing color-3 breaks
baryons/strong force but NOT family-3 — a one-way coupling confirming the common parent.
Determination: B (hidden common derivation), not A (independent boundaries), not C (single deeper
boundary). This refines NP_037: within the D96 spectral domain the 3s share ONE seed (unlike the
coincidental cross-domain 3s d=3/ω³/A³/valence-3), but "3" is still not a single principle — one
DERIVED seed + two boundary identifications. Classification: period-3 seed DERIVED (D_040);
family value DERIVED (QG210), window BOUNDARY; su(3) algebra DERIVED (QG161/242); color count
BOUNDARY (QG79); "independent boundaries" REFUTED; "single deeper boundary" REFUTED. Success
criterion: B — color-3 and family-3 share a hidden common derivation (the period-3 seed → octave
structure), with two distinct boundary residues. No new primitive; canonical AT unchanged.

ResearchY-NP_084 (Eta Framework Audit, COMPLETE, tests 8/8 PASSED): what is η physically? Verdict:
η is the CONFORMAL REFERENCE METRIC — a FRAMEWORK boundary, irreducible and necessary, but not a
physics primitive. It is the reference against which the trace (Tr(A) = η^ij A_ij = the scalar
face ρ), the traceless part (the tensor face ψ), conformal flatness (Weyl = 0), the Weyl content
(ψ = deviation from flatness), the metric (g = ρ^(2/d)η), and PPN γ (lensing) are all DEFINED.
Removing η breaks ρ, ψ, gravity, lensing, and the decomposition — no trace, no traceless, no
conformal flatness (QG291). Interpretations: A) coordinate convention NO (can't transform away);
B) reference structure YES; C) geometry primitive PARTIAL (a metric but non-dynamical — no scale,
no energy, no propagation); D) hidden background metric NO (explicit, non-dynamical). Not
derivable from Difference: Difference gives the rank-2 object, but the trace/traceless split
presupposes η as the contraction reference — so the founding pair {Difference, η} is genuinely
two: Difference = content, η = reading (neither reduces to the other; this completes NP_080's
trace/traceless duality, which is DERIVED given η). Classification: η FRAMEWORK (QG290/291);
trace/traceless duality DERIVED (QG286, given η); metric + PPN γ DERIVED (given η); η as
coordinate convention / hidden background / derivable-from-Difference REFUTED. Success criterion:
η is a FRAMEWORK boundary — the irreducible, necessary conformal reference structure, complementary
to and not reducible to Difference. No new primitive; canonical AT unchanged.

ResearchY-NP_085 (Completeness Frontier Audit, COMPLETE, tests 8/8 PASSED): what major physical
phenomena remain outside the derived ontology? Verdict (the complete frontier map): DERIVED —
foundations (scale-freeness, criticality μ=1, α=0, duality), structure (D96, occupancy [4,4,87]),
information (I_occ, ΩΛ = 0.6839, Ωm = 0.3161), matter (deficit), particles (resonance classes,
mass ratios, quantum numbers, forces), scalar gravity; CORRESPONDENCE (hosted) — the energy
reading, cosmic acceleration (q₀, z_acc as FRW closures), lensing/frame-dragging/GW (via ψ at GR
strength), cluster mass (degenerate with ΛCDM), weak/strong couplings, Bose statistics; BOUNDARY —
the equation of state w, the temperature scale, and seven irreducible inputs ({Difference, η},
Z2-paired sector, 3-family window [4,8), SU(2)+j=1/2, color count 3) + one dimensionful scale
(m_e/v) + imported ħ, c, π, Bekenstein 1/4; MISSING — condensed-matter physics (no phonon/
superconductor/phase-transition derivation) and nuclear structure (no binding-energy/shell-model
derivation); REFUTED — the blackbody (anti-thermal D96) and the Bullet Cluster (no collisionless
particle). Success criterion: the frontier is complete — explained (structural + cosmological
core), partial (energy/acceleration/lensing/clusters/couplings), open (w, temperature, 7 inputs,
and the two MISSING domains condensed matter + nuclear). No new primitive; canonical AT unchanged.

ResearchY-NP_086 (Difference Necessity Audit, COMPLETE, tests 7/7 PASSED): is Difference itself
necessary, or could a weaker primitive generate the same ontology? Verdict: Difference is the
MINIMAL and UNIQUE primitive — the bare logical possibility of distinction, the weakest notion
that generates counting and hence everything downstream. Not derivable (nothing produces it), not
replaceable: identity INSUFFICIENT (self-identity a=a does not give a≠b; Leibniz's identity of
indiscernibles makes distinction REQUIRE a difference), relation PRESUPPOSES (distinct relata),
count/information/symmetry DOWNSTREAM (all presuppose distinguishable states). The subtle case is
identity, the logical DUAL of difference (a≠b ⇔ ¬(a=b)), which plays the OPPOSITE, HIGHER role:
identity = preservation (conservation, QG267), Difference = generation (distinction, the bottom).
Weaker search: none exists — "distinction" = Difference (synonym, QG270), plurality presupposes
difference, existence/unity generate nothing. Determination: C (minimal) = D (unique); A
(derivable) and B (replaceable) REFUTED. The founding pair {Difference (generative content), η
(reading reference)} is genuinely minimal. Classification: Difference BOUNDARY (irreducible,
minimal + unique, QG270); identity/relation/count/information/symmetry as replacements REFUTED;
weaker primitive REFUTED. Success criterion: Difference is the minimal possible primitive — the
irreducible generative seed. No new primitive; canonical AT unchanged.

ResearchY-NP_087 (Nuclear Structure Audit, COMPLETE, tests 7/7 PASSED): can AT explain nuclear
structure (binding energies, magic numbers, shell structure)? Verdict: nuclei are
CORRESPONDENCE-ONLY (D). The NUCLEONS are DERIVED (proton = uud, neutron = udd quark composites
forming an isospin doublet, NP_072/074; n−p mass gap 1.29 MeV is a small isospin-breaking/EM
effect, NOT derived), and the strong coupling is a CORRESPONDENCE (α_strong = 8/Σ√m). But the
nuclear STRUCTURE is MISSING: binding energies (deuteron 2.224 MeV, He-4 28.3 MeV), magic numbers
[2,8,20,28,50,82,126], and shell structure are all absent. Structural reason: the D96 ring is 1D
with 47 mirror-pair (O(2)) degeneracies + 1 central mode, while nuclear shells are 3D with (2l+1)
spherical-harmonic degeneracies closed by spin-orbit — a genuine mismatch; the 1D ring does not
generate the 3D shell structure. Determination: A (resonance composite) and B (deficit composite)
REFUTED; C (symmetry composite) PARTIAL (isospin doublet only); D (correspondence only) YES.
Classification: nucleons + isospin DERIVED (NP_072/074); strong coupling CORRESPONDENCE; nuclear
binding/magic numbers/shells MISSING. Success criterion: nuclei are correspondence-only — the
nucleons are mapped, the nuclei are not; nuclear structure is the clearest large domain that does
NOT follow from the 1D D96 ring (confirms NP_085). No new primitive; canonical AT unchanged.

ResearchY-NP_088 (D96 Network Geometry Audit, COMPLETE, tests 9/9 PASSED): is the ontology truly a
single 1D ring, or does the theory already contain an effective higher-dimensional network
geometry? Verdict: the theory ALREADY contains an EMERGENT 3D geometry — D96 ⊗ D96 ⊗ D96 raises
the DOS exponent to p = 3 (a 3D cubic lattice), and d = 3 is DERIVED (QG197's (d−2) bridge: G ≡ 0
at d=2, non-trivial at d≥3). So D96 is a seed AND a node (the Weyl law p = d = number of tensor
factors: 1/2/3/N rings → p = 1/2/3/N). BUT this does NOT rescue nuclear structure: the 3D network
has CUBIC (octahedral O_h) symmetry with irreps {1,2,3}, while nuclear shells need ROTATIONAL O(3)
symmetry with 2l+1 = {1,3,5,7} spherical harmonics + spin-orbit; the cubic lattice breaks the
spherical degeneracies (l=2 (5) → 2+3; l=3 (7) → 1+3+3), so the magic numbers [2,8,20,28,50,82,126]
are still not reproduced. Re-evaluation of NP_087: the "1D" diagnosis is TOO NARROW (the theory
networks to 3D); the "missing" verdict SURVIVES, refined to "cubic symmetry vs rotational spherical
symmetry". Classification: emergent 3D geometry EMERGENT/DERIVED (QG197 + NP_037 tensor product);
nuclear-shell rescue REFUTED (cubic ≠ spherical); NP_087's missing verdict survives (refined).
Success criterion: the theory already contains an emergent higher-dimensional geometry via D96
networking, but nuclear structure remains missing — now because the network is cubic, not
rotationally-symmetric. No new primitive; canonical AT unchanged.

ResearchY-NP_089 (Rotational Symmetry Emergence Audit, COMPLETE, tests 9/9 PASSED): can the cubic
D96 network generate effective O(3) at large scale? Verdict: O(3) is APPROXIMATE ONLY (C). The free
cubic-lattice dispersion ω² = k² − (k_x⁴+k_y⁴+k_z⁴)/12 + … is isotropic to leading order (the k²
term, O(3)-invariant) but the first correction (the cubic invariant k_x⁴+k_y⁴+k_z⁴) BREAKS O(3)
down to O_h; the anisotropy scales as O((ka)²) (~0.5% at ka=0.3), so it is suppressed but NEVER
zero at any finite lattice spacing. Since the theory is DISCRETE (N=96), the exact continuum limit
(a→0, where O(3) would be exact) is never reached. Therefore the exact 2l+1 spherical-harmonic
degeneracies (the building blocks of the magic numbers) are split by the cubic correction at every
scale (l=2 (5) → 2+3; l=3 (7) → 1+3+3), and nuclear structure remains missing. Determination: A
(absent) REFUTED; B (emergent) PARTIAL; C (approximate only) YES; D (exact continuum) unattained.
Re-evaluation of NP_087/088: the missing verdict survives, refined to "O(3) is only approximate —
the cubic anisotropy is suppressed as (ka)² but never eliminated". Classification: the approximate
O(3) EMERGENT (leading-order isotropy); exact O(3) BOUNDARY (the unattained continuum); the
nuclear-shell rescue REFUTED (2l+1 split). Success criterion: rotational symmetry is C — approximate
only — so nuclear structure remains missing. No new primitive; canonical AT unchanged.

ResearchY-NP_090 (D96 Network Ontology Audit, COMPLETE, tests 8/8 PASSED): what physically is the
D96 network — nodes, links, what propagates? Verdict: the D96 network is the ORGANIZED DIFFERENCE
STRUCTURE. NODES = distinctions (Difference events = actualizations: a Q-event IS a before→after
difference, QG29/270) — B = C; LINKS = the adjacency (the symmetric rank-2 connectivity A_ij = A_ji,
a second-order difference "which distinctions are neighbours", NP_080); WHAT PROPAGATES = the 95
resonance modes (the particles) AND the two faces of the connectivity — the trace ρ (scalar/count
face → the metric g = ρ^(2/d)η) and the traceless ψ (tensor/curvature face). Concrete: 96 nodes ×
degree 12 (±1..±6) = 576 links, trace Σλ = 2E = N·d = 1152. Particles are modes of ONE D96 ring
(the seed); the tensor-product network (D96⊗D96⊗D96) supplies the 3D geometry (p=3), NOT more
particles. Geometry is EMERGENT from connectivity (g = ρ^(2/d)η is the trace face), not embedded.
Chain: Difference → D96 → Network → Geometry → Matter (the deficit m = ρ̄−ρ). Interpretations: A
(nodes = D96 structures) REFUTED (circular); B = C YES; D (nodes = occupancies) REFUTED. Success
criterion: what exists = distinctions (nodes); what connects = the adjacency (links); what
propagates = the 95 modes + the two faces ρ/ψ. Classification: all DERIVED (nodes from Difference,
links from the difference structure, geometry from the trace, matter from the deficit); A/D REFUTED.
No new primitive; canonical AT unchanged.

ResearchY-NP_091 (Network Geometry → Spacetime Audit, COMPLETE, tests 9/9 PASSED): how does the D96
network become the observed spacetime? Verdict (three steps): NETWORK (nodes = distinctions, links =
adjacency, path = a chain of links; distance = the shortest-link count) → GEOMETRY (g = ρ^(2/d)η =
the trace face of the connectivity; ψ = the traceless/Weyl face = the curvature) → SPACETIME (3D
space EMERGENT via D96⊗D96⊗D96 with d=3 DERIVED through the (d−2) bridge; +1 time = the
actualization TICK, the discrete causal order QG89, hosted as the +1 Lorentzian signature, a
FRAMEWORK residue QG290). ONLY SPACE IS EMERGENT; time is the framework tick (not a fourth network
axis). ρ does double duty: it is the conformal factor of the metric (g = ρ^(2/d)η) AND its branching
growth reads as cosmic expansion (a = ρ^(1/d), hosted FRW relabeling, NP_069). Causal structure: the
light cones = null geodesics (conformally invariant); the causal order = the partial order (acyclic);
the propagation limit = the tick. First NON-DERIVED step = {Difference, η} + the time tick. Network
vs observed spacetime: 3D cubic lattice (O_h, O(3) only approximate, NP_089) vs exact O(3) + 1
Lorentzian time. Classification: 3D spatial geometry EMERGENT (d=3 derived); metric g = ρ^(2/d)η
DERIVED (from the trace ρ); time (tick + signature) FRAMEWORK/BOUNDARY; Difference + η
BOUNDARY/FRAMEWORK. Success criterion: the complete mapping Difference → Network → Geometry →
Spacetime; the first non-derived step is {Difference, η} + the tick. No new primitive; canonical AT
unchanged.
ResearchY-NP_092 (Network Propagation Audit, COMPLETE, tests 12/12 PASSED): what propagates on the
network? Verdict: propagation is NOT transport — the network is STATIC (nodes/links do not move,
NP_007/090), and nothing substantial travels node to node. The ONLY genuine movement is the
ACTUALIZATION TICK (the causal-order advance along the generation relation, QG29/31, massless null
M_eff=0), and its native propagation law is LIGHT ALONG NULL GEODESICS (n=1, independent of ρ,
DERIVED, QG28). Everything else only APPEARS to move: a particle is a resonance mode (a standing
wave, NP_090) whose localized wave-packet ENVELOPE travels at the group velocity (EMERGENT); the
graviton is a ripple in ψ (the traceless/Weyl face, NP_080); a force is link-mediated generator
action (NP_075); the count ρ merely redistributes by continuity ∂_t ρ + ∇·j = 0 (conserved count,
NP_081). Determination: A (count transport, as continuity) and D (geometry transport, as ψ ripples)
are the two real channels (DERIVED); B (phase transport) REFUTED (θ=2πk/N is a local label, no
phase flow, NP_005); C (deficit transport) reduces to A. Network analogues: VELOCITY = the group
velocity v_g=dω/dk from the cubic dispersion (≤ c, subluminal at finite k: 0.99875@k=0.1, 0.98871
@k=0.3, 0.95442@k=0.6, → 1=c at k→0); LOCALITY = the adjacency (degree-12 neighbours); CAUSALITY =
the partial order (nothing outruns the tick). Classification: null geodesics DERIVED (QG28); the
tick FRAMEWORK (QG29/31); count continuity DERIVED (NP_081); wave-packet motion EMERGENT; ψ ripple
DERIVED; phase transport REFUTED (NP_005); a propagating field REFUTED (NP_007). No new primitive;
canonical AT unchanged.
ResearchY-NP_093 (Actualization Selection Audit, COMPLETE, tests 10/10 PASSED): if nothing travels
(NP_092), what determines WHICH node actualizes? Verdict: the node-selection law is the BORN RULE —
an actualization event is one tick = ONE count realization (QG216/QG222) that realizes one count on
one node, with node k selected with probability ρ_k = |ψ_k|² (the conserved, normalized count
share, Σρ = 1 EXACT). The two real DOFs select differently: the PHASE θ = 2πk/N advances
DETERMINISTICALLY per tick (Δθ = 2πk/N, D_041) and does NOT select the outcome; the COUNT ρ
realizes PROBABILISTICALLY with Born weight and IS the selector. Competing resonance maxima resolve
statistically by Born weight (larger-ρ node wins proportionally, not always). Matches Born
statistics exactly (QG216), the M_001 measurement (one outcome with weight ρ), and the double-slit
buildup (I = ρ_A+ρ_B+2√(ρ_Aρ_B)cos Δθ is the statistical accumulation of many Born selections).
Determination: B (probabilistic selector) with a DERIVED weight (count conservation, QG216);
A (deterministic) REFUTED; C (information-weighted) partial (ρ is the occupancy; I = KL(ρ‖uniform)
= 0.7513 nats, QG228); D (boundary import) REFUTED for the weight. The ONLY boundary is the
irreducible STOCHASTIC REALIZATION itself — one outcome per tick — the discrete tick, the deepest
single boundary (QG_011). Classification: Born weight ρ = |ψ|² DERIVED (QG216); deterministic
phase DERIVED (D_041); probabilistic selection DERIVED; irreducible stochastic realization
FRAMEWORK/BOUNDARY (the tick); deterministic selector REFUTED; boundary-import selector REFUTED.
No new primitive; canonical AT unchanged.
ResearchY-NP_094 (Inertia Ontology Audit, COMPLETE, tests 10/10 PASSED): what is inertia? Verdict:
inertia = RESONANCE PERSISTENCE — the stability of a spectral eigenmode under free (generator-free)
actualization. Motion is NOT object transport (nothing travels, NP_092) but RESONANCE PROPAGATION
(B): a particle (resonance mode, NP_072) has a wave-packet envelope advancing at the group velocity
v_g = dω/dk. Momentum = the PHASE GRADIENT (B) — the wave number k (θ = 2πk/N, p = ℏk). Mass = the
REST FREQUENCY ω₀ = m/ℏ (the mode's frequency attractor). A force = a GENERATOR ACTION (a
resonance transition, NP_075) that changes k. Free actualization — the tick (NP_091) + the Born
rule (NP_093) + the deterministic phase advance (D_041) — does NOT change k; only a generator
action does. Hence Newton I (F=0 → v=const) is the network statement that a mode's phase gradient
is conserved under free actualization: no generator → k constant → v_g constant. The electron (a
matter mode, ω₀>0) has inertia and can rest (k=0, still oscillating); the photon and graviton
(massless generators, ω₀=0) have no rest inertia and always propagate at n=1. Classification:
inertia DERIVED (eigenmode stability); momentum DERIVED (phase gradient k); mass DERIVED (rest
frequency, anchor m_e BOUNDARY); force DERIVED (NP_075); inertial frame DERIVED (the
geodesic/conformal frame, NP_091); motion EMERGENT (envelope propagation, NP_092); "motion = object
transport" REFUTED; imported classical law REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_095 (Friction Ontology Audit, COMPLETE, tests 8/8 PASSED): what is friction? Verdict:
friction = RESONANCE SCATTERING realized as MODE MIXING (B = C) — the incoherent accumulation of
generator actions (resonance transitions, NP_075) between a propagating mode and the deficit
excitations (matter, NP_071) in its path. Each scatter changes the mode's wave number k (momentum);
the aggregate redistributes k into the material's modes (dissipation, D as the consequence) while
preserving the resonance class ω₀ (identity, elastic). Friction scales with the matter density n
(γ ∝ n, dk/dt = −γk → k(t) = k₀e^(−γt)): ZERO in vacuum (n=0 → k conserved, pure inertia), growing
gas → liquid → solid. This is the OPPOSITE of inertia (NP_094): inertia = no resonance transitions
(k conserved); friction = resonance transitions (k changes); both reduce to the single generator
action, acting once (a force, NP_075) or many times (friction). Why motion persists in empty space
but decays in matter: vacuum has no deficit excitations to scatter against (k conserved, Newton I);
matter IS the deficit excitations (NP_071), so more matter = more scattering = more friction.
Classification: the microscopic scatter (a generator action) DERIVED (NP_075); friction as the
macroscopic dissipation (mode mixing) EMERGENT; the vacuum/matter distinction DERIVED (NP_071);
"friction is a new primitive/force" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_096 (Heat Ontology Audit, COMPLETE, tests 8/8 PASSED): what is heat? Verdict: heat =
RANDOM PHASE = MODE MULTIPLICITY = RESONANCE DECOHERENCE (A = B = D), realized as COUNT
REDISTRIBUTION (C) under count conservation (NP_081). When friction (NP_095) scatters a propagating
mode's coherent phase gradient k off matter's deficit excitations, the lost k does NOT vanish — it
becomes INCOHERENT (random-phase) mode excitation, spreading over the material's many modes. Heat
is that random-phase, multi-mode, decohered residue. Entropy is its measure: H = −Σρ ln ρ (the mode
multiplicity — 0 for one mode, ln 95 = 4.5539 for uniform over K=95); therefore ENTROPY GROWTH IS
EXACTLY INCREASING MODE ACCESS. Heat erases information: I_occ = KL(ρ‖uniform) = ln K − H (QG228)
→ 0 as heat spreads the count toward uniformity. Vacuum (no friction) → no heat → no entropy
growth; gas → liquid → solid → more heat + faster entropy growth. One ontology: motion (coherent
phase) → friction (scattering) → heat (random phase) → entropy (mode multiplicity). Classification:
heat as the macroscopic thermal state EMERGENT (the aggregate of friction's scatterings); the
underlying count conservation DERIVED (NP_081); entropy H = −Σρ ln ρ DERIVED (the multiplicity
functional); entropy growth = increasing mode access DERIVED; "heat as a new primitive/substance"
REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_097 (Temperature Ontology Audit, COMPLETE, tests 8/8 PASSED): what is temperature?
Verdict: temperature = the COUNT-DISTRIBUTION WIDTH (D) — the thermodynamic conjugate of entropy:
T = ∂U/∂S, where U = the conserved count (energy, NP_081) and S = H = −Σρ ln ρ (entropy/mode
multiplicity, NP_096). Equivalently 1/T = ∂S/∂U (A, the mode-multiplicity density — the inverse
reading). Temperature is DERIVED from the occupancy ρ alone: for the canonical occupancy ρ_k ∝
e^(−E_k/T) over K=95 modes, T is the width parameter — cold (T=0.2) concentrates the count in 2
modes (H=0.0407), hot (T=100) spreads it over all 95 (H=4.5171). H (mode multiplicity) and the
occupancy width monotonically track T; I_occ = ln K − H (QG228) monotonically falls. The
thermodynamic identity is exact: dS/dU = β (verified 0.500000 at β=0.5, so T = dU/dS = 2.0).
B (phase randomness) is HEAT (NP_096), not temperature; C (scattering rate) is a consequence.
Classification: the dimensionless temperature (∂U/∂H) DERIVED (a functional of ρ); the ABSOLUTE
Kelvin scale BOUNDARY (the unit-conversion anchor k_B, the same "one dimensionful scale" pattern
as m_e and v); "temperature as phase randomness" REFUTED; "temperature as a new primitive" REFUTED.
No new primitive; canonical AT unchanged.
ResearchY-NP_098 (Localization Ontology Audit, COMPLETE, tests 9/9 PASSED): what is localization?
Verdict: localization = the CONSTRUCTIVE INTERFERENCE of a WAVE PACKET — a superposition of
resonance modes whose coherent addition peaks |ψ|² = ρ at one node and cancels elsewhere. A
particle is a propagating resonance (B) — its localization is the Born distribution's envelope
peak (C), its trajectory the actualization chain of Born-selected nodes (D); a single localized
node (A) is REFUTED (NP_072: a particle is a global mode, not a node). Fourier uncertainty Δx·Δk
= 1 (1/e² widths): a single mode (Δk→0) is DELOCALIZED (|ψ|² = 1/N uniform, Δx→∞); a wave packet
(Δk large) is LOCALIZED (Δx small). Position = the envelope peak; time = the tick (the envelope
advances at v_g, one link per tick at most); the Born selection (NP_093) realizes one node with
probability |ψ|² = ρ, so the observed trajectory is a stochastic chain following the envelope.
Electron: envelope at v_g = k/ω; photon/graviton (massless): envelope at n=1. Three readings: wave
packet (superposition, potential position) → actualization chain (Born selections, actual
position) → classical worldline (smooth envelope, emergent limit). Classification: the wave packet
DERIVED (superposition of modes); the Born weight |ψ|² = ρ DERIVED (QG216); localization/position/
trajectory EMERGENT (constructive interference + actualization chain); the position basis (node
labeling/geometry) FRAMEWORK (η + D96); "particle = a localized node" REFUTED. No new primitive;
canonical AT unchanged.
ResearchY-NP_099 (Classicality Emergence Audit, COMPLETE, tests 10/10 PASSED): how does classical
reality emerge from actualization? Verdict: classicality = DECOHERED LOCALIZATION (A) — a localized
resonance (wave packet, NP_098) is quantum while its interference term survives: I = ρ_A + ρ_B +
2√(ρ_Aρ_B)cos(θ_A−θ_B) shows fringes. Friction (NP_095) scatters it against the environment's
deficit excitations, randomizing the phases; at the decoherence rate γ·t ≫ 1, ⟨cos Δθ⟩ → 0 and the
fringes vanish — I → ρ_A + ρ_B (classical probabilities). The transition quantum → classical is
decoherence (A, the mechanism), realized through repeated actualization (B, the Born selections
tracing the trajectory), on a stable resonance hierarchy (C, the composite modes), with entropy
dominance (D, the high mode multiplicity making decoherence instantaneous) as the signature.
Macroscopic bodies (~10²³ modes) decohere essentially instantly, hence appear classical. The
classical limit (ℏ→0) = the decoherence-dominant limit (γ·t ≫ 1) = the large-N/high-entropy limit.
Classification: classicality EMERGENT (decoherence + repeated actualization); the interference term
DERIVED (Born, QG216); the decoherence mechanism DERIVED (friction, NP_095); "classicality as a
separate primitive" REFUTED; "classicality as a fundamental (non-emergent) regime" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_100 (Bound Structure Audit, COMPLETE, tests 8/8 PASSED): what is a bound structure?
Verdict: binding = RESONANCE LOCKING = PHASE SYNCHRONIZATION = DEFICIT CLUSTERING (A = B = D),
held by PERSISTENT GENERATOR ACTION (C, the binding force). A bound state is a STABLE MUTUAL
CONFIGURATION of two+ resonances whose relative phase is LOCKED (constant across ticks); the
binding force is the ONGOING exchange of generator actions (link excitations, NP_075), not a single
transient transition; the stable configuration is matter's deficit clustering (NP_071, the unique
self-bound, clumping branch). It persists across ticks because the lock is a STABLE FIXED POINT of
actualization — the bound-state analogue of inertia (NP_094): free actualization does not change the
locked phase, only a strong generator action (enough to break the lock) does. Cascade: modes → pair
(e⁻+p) → atom (hydrogen, 13.6 eV) → molecule (H₂), each a deficit clustering of the level below.
Free resonance = delocalized (drifts); localized = wave packet (transient); bound = locked
(persistent). Classification: binding ontology DERIVED (NP_071/075/094); the bound structure as a
persistent configuration EMERGENT; binding energies (13.6 eV etc.) BOUNDARY (imported, the m_e-anchor
pattern); "binding as a new primitive" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_101 (Hierarchical Structure Audit, COMPLETE, tests 9/9 PASSED): why do stable
structures appear at many scales? Verdict: there is ONE universal, SCALE-FREE stability principle —
a stable structure is a DEFICIT CLUSTERING (matter, the unique self-bound branch, NP_071) locked at
a GENERATOR-BALANCED FIXED POINT (resonance locking = phase synchronization, NP_100). A = B = C = D
— resonance locking = phase synchronization = deficit clustering = generator-balanced fixed points,
one principle. It is scale-free (AT-F1, from Difference's binary nature, NP_079), so it repeats
self-similarly from 10⁻¹⁵ m (particle) to 10²¹ m (galaxy) — 36 orders of magnitude — producing the
hierarchy particle → atom → molecule → crystal → planet → galaxy, each a deficit clustering of the
level below (electromagnetism binds the small scales, gravity the large). Classification: the
stability principle DERIVED (NP_071/100); scale-freeness DERIVED (NP_079); the hierarchy EMERGENT
(the self-similar repetition); specific sizes/energies BOUNDARY (imported); a scale-dependent
stability mechanism REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_102 (Existence Ontology Audit, COMPLETE, tests 8/8 PASSED): what does it mean for
something to exist? Verdict: to exist = to be a PERSISTENT, DISTINGUISHABLE structure — a
Difference (a distinction, NP_086) that endures actualization (stability, NP_094/100). Existence
has exactly two conditions: (1) DISTINGUISHABILITY (the thing is a Difference, distinct from its
surroundings — the minimal primitive, NP_086); (2) STABILITY (it persists across ticks rather than
dissolving — inertia/bound-state persistence). A = B = C: actualization persistence = stable
resonance (frequency attractor, NP_072) = bound deficit structure (matter = the deficit, NP_071/100)
— three readings of the one existing thing; D (observational accessibility) is a CONSEQUENCE, not
the condition (the 95 states pre-exist measurement, M_005). Removing stability dissolves every
structure into momentary actualizations — only the Difference structure survives, no enduring
things. The hierarchy (particle → galaxy, NP_101) is the repeated application of this one condition.
Classification: Difference (the minimal condition) BOUNDARY (NP_086); stability/persistence DERIVED
(NP_094/100); the existing thing (stable resonance/deficit structure) DERIVED (NP_072/100); the
hierarchy EMERGENT (NP_101); "existence = observation" REFUTED; "existence as a new primitive"
REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_103 (Nonexistence Ontology Audit, COMPLETE, tests 9/9 PASSED): what ceases to exist?
Verdict: nonexistence = the NEGATION of existence — a thing ceases to exist when it loses its
DISTINGUISHABILITY (Difference) or its PERSISTENCE (stability): NOT(Existence) = ¬Difference ∨
¬Persistence (from NP_102). The two full channels are DECAY (¬Persistence: the structure dissolves)
and THERMALIZATION (¬Difference: the identity merges into the uniform bath); losing LOCALIZATION (C)
or BINDING (D) is PARTIAL — form changes (a spread packet, a dissociated composite), the underlying
resonances survive. Decay/decoherence/dissociation/thermalization all REDISTRIBUTE the conserved
count (energy, NP_081): information is REDISTRIBUTED, not destroyed (Σρ = 1 conserved, M_005;
I_occ = ln K − H → 0 as distinguishability spreads into random phase, NP_096). The final stage of
decay is structure → pattern → noise → UNIFORM (ρ_k = 1/K, maximum entropy, QG227), beyond which
only DIFFERENCE (the substrate) remains — nonexistence reaches uniform noise, never "nothing".
Classification: the negation logic DERIVED (from NP_102); decay/thermalization DERIVED
(NP_095/096/100); the uniform end-state DERIVED (QG227); information redistribution DERIVED (M_005);
Difference (the substrate that never ceases) BOUNDARY (NP_086); "information is destroyed" REFUTED;
"nonexistence reaches nothing" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_104 (Difference Persistence Audit, COMPLETE, tests 8/8 PASSED): can Difference itself
ever cease? Verdict: NO — Difference is INDESTRUCTIBLE (A) and CONSERVED (C); its disappearance (B)
is REFUTED. Complete uniformity is uniform OCCUPANCY (ρ_k = 1/K), not absent distinctions: the K=95
modes remain distinct, so Difference survives uniformity. Difference cannot decay (decay is a
transition between distinct modes), cannot thermalize (thermalization uniformizes the occupancy,
not the distinctions), and cannot "become uniform" (uniform count still has distinct modes) — every
process IS a process of Difference and presupposes the distinctions it operates on. There is NO
state with no distinctions at all: "no distinctions" is the absence of the theory, not a state.
The decisive SELF-REFERENTIAL argument: "Difference ceased" is itself a Difference (ceased vs.
not-ceased), so the assertion of Difference's cessation presupposes Difference. Classification:
Difference BOUNDARY (the primitive, NP_086); Difference's indestructibility BOUNDARY (the ground of
being cannot be removed); the conservation of distinguishability DERIVED (M_005, count
conservation); "Difference can disappear" REFUTED; "a distinction-free state exists" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_105 (Difference Conservation Audit, COMPLETE, tests 8/8 PASSED): is Difference the
true conserved quantity? Verdict: YES — Difference is the PARENT conservation law (A = B = C); D
(independent) REFUTED. The invariant is the indestructible Difference structure (the 95 distinct
states, NP_104). Every conserved quantity is a projection of it: count (Σρ = 1) is the MEASURE of
Difference (QG216); information is the STRUCTURE of Difference (M_005); momentum (phase gradient k)
and charge (U(1) eigenvalue) are Noether symmetries of the Difference structure (NP_075/QG89, the
1+3+8 = 12 D96 generators). Existence is NOT conserved — it can cease (NP_102/103) — because
existence is Difference made persistent, not the invariant. Removing Difference breaks everything:
no distinct modes, no count, no information, no symmetries, no existence. The Noether/symmetry/
counting analogues coincide: the D96 automorphism group preserves the distinction structure, and
the conserved charges are its generators. Classification: Conservation of Difference BOUNDARY (the
indestructibility of the primitive, NP_104); count/information/momentum/charge conservation DERIVED
(projections); "conservation is independent" (D) REFUTED; "existence is conserved" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_106 (Actualization Necessity Audit, COMPLETE, tests 8/8 PASSED): why must Difference
actualize? Verdict: a Difference is an ACT of distinguishing, not a static state — "static
Difference" is a CONTRADICTION (an un-drawn distinction is no distinction; it collapses into
undifferentiated unity, refuted by NP_104). Therefore actualization is a LOGICAL NECESSITY (C) of
Difference: to differ is to enact the distinction, and the tick is that enactment. Actualization is
NOT an independent primitive (B refuted) — it is the dynamic face of Difference (A partial: the act
is derived; D partial: the dynamics is emergent). Removing actualization leaves nothing: no
counting, information, structure, or existence — because actualization is what Difference IS, not an
addition to it. Only the DISCRETENESS of the tick (one outcome per tick) is the BOUNDARY residue
(the deepest single boundary, QG_011). The minimal reason a conserved Difference produces events:
a distinction must be drawn to be a distinction. Classification: actualization as the act of
distinguishing DERIVED (logical necessity, C); the tick's discreteness BOUNDARY (QG_011); the
dynamics EMERGENT (D); "actualization is an independent primitive" (B) REFUTED; "a static
Difference is possible" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_107 (Tick Necessity Audit, COMPLETE, tests 8/8 PASSED): why does actualization occur
as discrete ticks? Verdict: because Difference is BINARY ("this ≠ that") — a discrete CUT, not a
continuum. The discreteness of the tick is a LOGICAL NECESSITY (A), DERIVED from the binary nature
of Difference (NP_079 already derived scale-freeness from it): the act of drawing the distinction
realizes one side of the binary per act, so one outcome per tick. Continuity fails at the very first
step (the binary relation), and every structure inherits it — counting (Σρ = 1 over discrete
modes), Born selection (one outcome per event), localization (discrete nodes), the phase lattice
(Δθ = 2πk/N, finite). A "continuous Difference" is a contradiction (a gradient blurs the cut into
undifferentiated unity, refuted by NP_104), so actualization without ticks is incoherent. This
REFINES QG_011: the deepest boundary is NOT the tick's discreteness but Difference's BINARY NATURE
itself; the tick's discreteness is DERIVED. Classification: the discreteness of the tick DERIVED
(logical necessity, A); the binary nature of Difference BOUNDARY (the content of the primitive,
NP_086/079); "the tick's discreteness is an irreducible boundary" (D) REFUTED (refining QG_011);
"continuous actualization is coherent" REFUTED; "discreteness is a free framework choice" (C)
REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_108 (Falsification Frontier Audit, COMPLETE, tests 8/8 PASSED): what observation would
directly falsify AT? Verdict: AT is FALSIFIABLE at every level. STRONGEST (tightest) falsifiers: the
single-valued DERIVED numerics — n_s = 0.96497 (0.007%), ℓ₁ = 220.48 (0.008%), ΩΛ = 0.6839 (0.12%),
Ωm = 0.3161 (0.26%), the mass ratios (m_μ/m_e = 207.03), and the pre-registered 0νββ (m_ββ = 2.02
meV, QG191). Strongest UNIQUELY-AT falsifier: AT-P042 (the discrete-tick phase lattice, absent from
QM, M_009). WEAKEST falsifiers: the ontological claims (particles = resonance classes, forces =
generator actions, matter = deficit) — structural, falsified only by a non-expressible object.
STRONGEST VULNERABILITY: nuclear structure — AT derives O(3) as only APPROXIMATE (the cubic lattice
never reaches exact rotational symmetry), so the magic numbers are NOT exactly reproduced
(NP_087/089); w = −1 is HOSTED, and the anchors (m_e, v, k_B) are imported. Ranked falsifiers:
(1) n_s outside 0.96497±σ; (2) ℓ₁ outside 220.48±σ; (3) ΩΛ outside 0.6839±σ; (4) Ωm outside
0.3161±σ; (5) a continuous phase at sub-tick resolution (falsifies AT-P042); (6) a fermion mass ratio
not a D96 ratio; (7) an intrinsic scale λ fitting better (falsifies scale-freeness); (8) a ψ-based DM
signal; (9) exact magic numbers from a cubic lattice; (10) point-particle matter; (11) m_ββ outside
±10% of 2.02 meV; (12) an interaction not a generator action. Classification: numerical predictions
FALSIFIABLE (DERIVED, sharp); the discrete tick FALSIFIABLE (unique, in-principle); ontological claims
FALSIFIABLE (structural, weak); anchors and w = −1 BOUNDARY (imported, not falsifiable); "AT is
unfalsifiable" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_109 (Weakest Link Audit, COMPLETE, tests 8/8 PASSED): where is AT most likely to fail?
Verdict: the weakest link is NUCLEAR STRUCTURE (O(3) only approximate — the magic numbers
[2,8,20,28,50,82,126] are NOT exactly reproduced, NP_087/089), with CONDENSED MATTER the largest
unmapped domain. Domain ranking by risk: foundations / particles / forces ROBUST (derived or logical
necessities, no falsifier yet); cosmology / gravity PARTIAL (numerics and metric DERIVED, but w = −1
HOSTED and ψ a framework boundary); nuclear structure / condensed matter MISSING. STRONGEST evidence:
the tight cosmology numerics (n_s = 0.96497 at 0.007%, ℓ₁ = 220.48, ΩΛ = 0.6839). WEAKEST evidence:
nuclear structure. SINGLE most-likely falsifier: an exact magic-number closure derived from the cubic
lattice — AT's own derivation (NP_089) predicts it cannot be exact, so deriving it breaks the
structural core. Classification: foundations/particles/forces ROBUST; cosmology/gravity PARTIAL;
nuclear structure/condensed matter MISSING. No new primitive; canonical AT unchanged.
ResearchY-NP_110 (Condensed Matter Gateway Audit, COMPLETE, tests 8/8 PASSED): can the Difference →
D96 ontology generate collective matter? Verdict: YES — condensed matter is the NEXT NATURAL LAYER
above binding (NP_100) and hierarchy (NP_101), NOT beyond D96. A crystal = a repeated bound network
(a lattice of phase-locked deficit clusters); a phonon = a collective mode (the low-k resonance, the
acoustic branch ω = 2c·sin(ka/2) ≈ c·k); a magnet = a phase-locked (aligned) network; a
superconductor = global phase coherence. A = B = C (resonance hierarchies = phase-locked networks =
deficit clusters, scaled up); D (new ontology) REFUTED. No new primitive, geometry, or symmetry is
needed — only LARGER networks (D96⊗D96⊗D96 already gives the cubic lattice, NP_088). The FIRST
failure point is the SAME O(3)-approximate weakness as nuclear structure (NP_089): the dispersion
ω² = k² − (k_x⁴+k_y⁴+k_z⁴)/12 is isotropic to leading order but the cubic correction breaks O(3) at
finite k, so exact isotropic phonon dispersions and exact band degeneracies are approximate.
Classification: the collective ontology (crystal/phonon/magnet/superconductor) DERIVED (larger
networks); condensed matter as the next layer EMERGENT; the exact quantitative properties PARTIAL
(the cubic anisotropy); "condensed matter is beyond D96" REFUTED; "needs a new primitive/geometry/
symmetry" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_111 (Observer Ontology Audit, COMPLETE, tests 7/7 PASSED): what is an observer?
Verdict: an observer = a PERSISTENT DIFFERENCE STRUCTURE (matter/existence, NP_071/102) that
ACTUALIZES (reads) the distinctions of its environment — a distinguisher within the network.
Observation = A = B = C = D: actualization (state selection, M_001) = information acquisition
(log₂(95) bits, M_004) = difference recognition (D_039) = persistent-structure interaction (the
detector). A particle is the observed (a resonance); a detector is a persistent structure reading
ONE distinction; an observer is a hierarchy of detectors (NP_101) integrating MANY readings — a
CONTINUOUS chain, no categorical break. Observers are special only in DEGREE (integration scale),
not in KIND — exactly matter and existence as a reading structure. This RESOLVES M_001 OP1: the
observer IS an actualization subsystem. Classification: the observer (persistent Difference
structure) DERIVED (NP_071/102); observation (the actualization/reading event) DERIVED (M_001/004);
the observer as an integrating hierarchy EMERGENT (NP_101); "the observer is fundamentally special"
REFUTED; "observation needs a new primitive" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_112 (Reality Appearance Audit, COMPLETE, tests 7/7 PASSED): why does a persistent
observer experience a stable reality? Verdict: reality = A = B = C = D — the sequence of
actualizations (A, the tick substrate) INTEGRATED (B) by a persistent structure (the observer,
NP_111) recognizing persistent structures (C, matter/binding/hierarchy) — and that integration IS
the emergent narrative (D) of a stable world. The two stabilities multiply: the world persists
(inertia NP_094, binding NP_100, hierarchy NP_101) and is decohered to a single worldline (NP_099);
the observer persists (a bound hierarchy) and is the same reader across ticks — so repeated readings
AGREE, and the world appears stable and continuous. Reality is STRUCTURE-RELATIVE: neither
observer-independent (no view from nowhere) nor observer-relative (the structures pre-exist
measurement, M_005) — it is the relation between persistent structures. Classification: the
substrate (sequence of actualizations) DERIVED (NP_106/107); the persistent structures DERIVED
(NP_071/094/100/101); the observer DERIVED (NP_111); the emergent narrative (stable reality) EMERGENT
(the decohered integration); "reality is observer-relative" REFUTED; "reality needs a new primitive"
REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_113 (Observed Ontology Audit, COMPLETE, tests 8/8 PASSED): what does "observed" mean?
Verdict: "observed" = a distinction INCORPORATED INTO A PERSISTENT OBSERVER (D) — a persistent
Difference structure (NP_111) has changed its own structure to include the distinction, altering its
subsequent actualizations (M_003 feedback). It is DISTINCT from the four near-synonyms: "actualized"
(a node realized by Born selection, NP_093 — universal, no observer), "localized" (the wave-packet
peak, NP_098 — a state-property), "measured" (an actualization reading both quadratures, M_001 — a
read event), "recorded" (the outcome persisted as a trace — a persistent copy). Determination: D the
answer; A (actualized) and C (distinguished) necessary but not sufficient; B (localized) neither.
Observation changes the OBSERVER (primarily) and the observed system's PHASE (the pinning, M_002),
but NOT the underlying persistent Difference structures (they pre-exist, M_005) — reality is
revealed, not created. Chain: actualization → localization → measurement → observation → recording,
each adding a condition. Classification: actualized DERIVED (NP_093); localized DERIVED (NP_098);
measured DERIVED (M_001); observed EMERGENT (NP_111); recorded EMERGENT; "observed = actualized"
REFUTED (over-inclusive); "observation creates reality" REFUTED. No new primitive; canonical AT
unchanged.
ResearchY-NP_114 (Knowledge Ontology Audit, COMPLETE, tests 8/8 PASSED): what is knowledge?
Verdict: knowledge = STABLE, INTEGRATED, PREDICTIVE DISTINCTIONS (B = C = D) — the persistent
information held by an observer as an anticipatory structure, realized via stored observations (A)
as raw material. Chain: Difference → observation (incorporation, NP_113) → memory (persistence,
NP_102) → knowledge (predictive integration). Knowledge is DISTINCT from information (the
distinguishability structure, M_004 — no persistence/prediction) and observation (the single event —
no persistence). Its differentiator is PREDICTION (C): knowledge shapes the observer's future
actualizations. Removing memory leaves observation but collapses knowledge and understanding.
Detector < observer < knowledge-bearing observer (observer WITH memory + prediction). Classification:
data/information DERIVED (D_039/M_004); observation EMERGENT (NP_113); memory DERIVED (NP_102);
knowledge EMERGENT (predictive integration); understanding EMERGENT (highest integration);
"knowledge = stored observations only" REFUTED; "knowledge needs a new primitive" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_115 (Understanding Ontology Audit, COMPLETE, tests 9/9 PASSED): what is understanding?
Verdict: understanding = the MODEL OF MODELS (D) = the COMPRESSION of knowledge (B) = a PREDICTIVE
HIERARCHY (C) — knowledge raised to self-reference: the observer holds a compressed model of its own
predictions, so it knows WHY it predicts (not merely WHAT next) and can re-derive, transfer, and
generalize. A (more knowledge) REFUTED — a qualitatively higher order, not a larger pile. The
epistemic chain is data → information → observation → memory → knowledge → understanding (read →
store → predict → model the prediction). Removing prediction collapses BOTH knowledge and
understanding; removing integration collapses knowledge to observations and understanding to
scattered knowledge. The MINIMUM separator between knowledge and understanding is COMPRESSION +
SELF-REFERENCE: knowledge predicts, understanding knows why it predicts. Understanding changes
future actualization MORE EFFECTIVELY (the compressed model generalizes to novel cases). The four
objects: detector (reads) < recorder (stores) < predictor (knowledge) < understander (model of
models). Classification: data/information/memory DERIVED (D_039/M_004/NP_102); observation EMERGENT
(NP_113); knowledge EMERGENT (NP_114); understanding EMERGENT (the highest epistemic level);
"understanding = more knowledge" REFUTED; "understanding needs a new primitive" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_116 (Meaning Ontology Audit, COMPLETE, tests 9/9 PASSED): what is meaning? Verdict:
meaning = VALUE-WEIGHTED PREDICTION = THE RELATION BETWEEN MODELS (C = D) — the SIGNIFICANCE of a
distinction for an observer, i.e., how much it matters for the observer's future actualizations. It
is the apex of the epistemic chain data → information → observation → knowledge → understanding →
meaning (read → structure → incorporate → predict → model → WEIGHT). Meaning is DISTINCT from
information (raw structure), knowledge (prediction), understanding (model of models): its
differentiator is SIGNIFICANCE. A (interpreted information) and B (contextual understanding) are
necessary but not sufficient (they lack value-weighting). Removing context collapses meaning
(significance is context-dependent); removing prediction collapses meaning (no future consequence =
insignificant); information survives as the most robust, meaning is the most fragile. The MINIMUM
condition is SIGNIFICANCE: the distinction must matter for the observer. The four objects: observer
(reads) < knower (predicts) < understander (models) < meaning-bearer (weights). Classification:
data/information DERIVED (D_039/M_004); observation EMERGENT (NP_113); knowledge EMERGENT (NP_114);
understanding EMERGENT (NP_115); meaning EMERGENT (the significance relation); "meaning =
information" REFUTED; "meaning needs a new primitive" REFUTED. No new primitive; canonical AT
unchanged.
ResearchY-NP_117 (Purpose Ontology Audit, COMPLETE, tests 9/9 PASSED): what is purpose? Verdict:
purpose = the SELECTED PREDICTION (B) = a FUTURE-ORIENTED ACTUALIZATION (C) = a STABLE VALUE
HIERARCHY (D) — meaning PLUS selection: the prediction the observer picks as its aim. Meaning
WEIGHS a distinction's significance (NP_116); purpose SELECTS a direction and AIMS the observer
toward it. A (weighted meaning) necessary but not sufficient — the differentiator from meaning is
SELECTION (direction). The teleological chain is observer → meaning (weighs) → purpose (aims) →
action (does). Removing value collapses both meaning and purpose (no weight, no selection criterion);
removing prediction collapses purpose (no future to aim at); information/knowledge/understanding
survive as the predictive machinery. Purpose CHANGES future actualization — it is the aim that
directs the observer's subsequent actions, converting significance into a goal. Classification:
meaning EMERGENT (NP_116); value DERIVED (the consequence weight); choice/decision EMERGENT (the
selection); purpose EMERGENT (the selected aim); action EMERGENT (the future actualization);
"purpose = meaning" REFUTED (lacks selection); "purpose needs a new primitive" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_118 (Choice Ontology Audit, COMPLETE, tests 10/10 PASSED): what is choice? Verdict:
choice = the ACT of SELECTING (A = C = D) — constrained actualization = purpose becoming action: the
observer selects one alternative, value-guided, toward its aim. B (value maximization) PARTIAL
(choice selects, it does not necessarily maximize). It is DISTINCT from purpose (the AIM — direction)
and action (the RESULT — execution): choice is the ACT between them. The agential chain is meaning
(weighs, NP_116) → purpose (aims, NP_117) → choice (selects) → action (does). Removing alternatives
collapses choice (forced, not chosen); removing values collapses choice/purpose/meaning (random, not
chosen — a raw Born draw, NP_093); removing purpose collapses choice (prediction, not choice).
The MINIMUM condition for a genuine choice is ALTERNATIVES + VALUES + PURPOSE — which separates
choice from raw actualization: actualization is a draw, choice is a selection toward an aim.
Classification: alternatives/options DERIVED; value DERIVED; purpose EMERGENT (NP_117); choice
EMERGENT (the selection act); decision EMERGENT (the settled selection); action EMERGENT (the
execution); "choice = value maximization" PARTIAL; "choice = raw actualization" REFUTED (no aim);
"choice needs a new primitive" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_119 (Freedom Ontology Audit, COMPLETE, tests 9/9 PASSED): what is freedom? Verdict:
freedom = the CAPACITY TO SELECT AMONG MEANINGS (C) = ACTUALIZATION UNDER INCOMPLETE DETERMINATION
(D) — the value-weighted, purpose-directed selection whose outcome is not predetermined but
weighted. A (number of alternatives) partial (a precondition); B (unconstrained choice) REFUTED —
freedom is value-constrained, not unconstrained. Freedom is the MIDDLE path between determinism and
randomness: forced (no alternatives) and random (alternatives without values/purpose) both lack
freedom; chosen (alternatives + values + purpose) has it. Removing any of the three collapses
freedom into determinism or randomness; raw actualization (the Born draw) survives = randomness, not
freedom. Freedom is EMERGENT, built on the BOUNDARY stochasticity of the tick (NP_107) and the
EMERGENT value structure (NP_116/117). It is COMPATIBLE BY IDENTITY with the foundations: freedom IS
Born selection (NP_093) under incomplete determination (weight = value, draw = the tick's
stochasticity), and it does not violate Difference conservation (NP_105) — freedom is selection, not
creation. This crowns the epistemic→teleological→agential arc (data → … → meaning → purpose →
choice → freedom). Classification: the irreducible stochasticity BOUNDARY (the tick, NP_107); the
value/purpose structure EMERGENT (NP_116/117); freedom EMERGENT; "freedom = randomness" REFUTED;
"freedom = unconstrained choice" REFUTED; "freedom = determinism" REFUTED; "freedom needs a new
primitive" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_120 (Responsibility Ontology Audit, COMPLETE, tests 8/8 PASSED): what is
responsibility? Verdict: responsibility = FREEDOM + PERSISTENCE — the OWNERSHIP of the action and
its consequences (A = B), grounded in the CONTINUITY OF THE OBSERVER (D), which is the persistence of
choice (C). The same observer who chose (freedom, NP_119) is the same observer who bears, because
the observer is a persistent Difference structure (NP_102/104): it does not dissolve, so it owns its
choices across ticks. Responsibility is DISTINCT from freedom (the capacity), choice (the act), and
action (the execution): its differentiator is CONTINUITY. Forced action (no alternatives) and random
action (no values/purpose) bear NO responsibility; only free action (alternatives + values, with a
persistent chooser) does. Removing alternatives or values collapses freedom (hence responsibility);
removing PERSISTENCE collapses responsibility while freedom survives — proving persistence is the
crux. The moral chain is meaning → purpose → choice → freedom → responsibility (weigh → aim → select
→ capacity → own). Classification: freedom EMERGENT (NP_119); persistence DERIVED (NP_102);
responsibility EMERGENT (freedom + persistence); "responsibility = freedom" REFUTED (lacks
continuity); "responsibility = action/consequence only" REFUTED (lacks ownership); "responsibility
needs a new primitive" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_121 (Identity Ontology Audit, COMPLETE, tests 8/8 PASSED): what is identity? Verdict:
identity = the SAMENESS of a persistent Difference structure (A = D) — the continuity face of
existence: not just that the thing IS (existence, NP_102), but that it is THE SAME across ticks.
Identity is grounded in the PERSISTENCE of the structure, not the persistence of content: the
observer's actualizations, memories, and states change every tick, but its Difference structure
persists (NP_104), so it is the same observer. B (persistent memory) and C (persistent actualization
pattern) are PARTIAL (correlates/consequences, not the identity). Removing memory leaves identity
partially intact (the structure persists); removing persistence or continuity collapses identity.
The MINIMUM condition for "the same observer" is PERSISTENCE (continuity of the Difference
structure). Identity is DISTINCT from existence (the thing IS) and responsibility (freedom +
persistence, which PRESUPPOSES identity): identity is the sameness responsibility requires. A person
changes every tick yet is the same person — structural persistence, not content persistence.
Classification: Difference/persistence DERIVED (NP_102/104); identity EMERGENT (the sameness of the
persistent structure); memory/continuity as correlates PARTIAL; "identity = memory" REFUTED;
"identity = the content/actualizations" REFUTED; "identity needs a new primitive" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_122 (Consciousness Ontology Audit, COMPLETE, tests 9/9 PASSED): what is consciousness?
Verdict: consciousness = the RECURSIVE SELF-MODEL (D) = SELF-OBSERVATION (B) — the observer's model
turned on itself, so it observes its own observing. A (observation) necessary but not sufficient (a
detector observes without being conscious); C (integrated observation) partial. The epistemic chain
culminates in recursion: observation → knowledge → understanding → identity → consciousness
(incorporate → predict → model → persist → SELF-MODEL). The four terms: awareness (the self-model
reading its own distinctions), experience (the ongoing self-observation), consciousness (the
recursive self-model), self (the self-model's object). Removing memory partially collapses
consciousness; removing integration collapses it to a detector; removing the SELF-MODEL collapses it
entirely while observation/knowledge/understanding/identity survive — proving the self-model is the
crux. The minimum condition is THE SELF-MODEL: an observer is conscious exactly when it observes
that it observes. NO new primitive — consciousness is the same Difference ontology turned recursive
(the model includes its builder). Classification: observation/knowledge/understanding/identity
EMERGENT (NP_113/114/115/121); consciousness EMERGENT (the recursive self-model); "consciousness =
observation" REFUTED; "consciousness needs a new primitive" REFUTED; "consciousness is a mystery
substance" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_123 (Truth Ontology Audit, COMPLETE, tests 9/9 PASSED): what is truth? Verdict: truth =
CORRESPONDENCE (C) = MODEL-NETWORK CORRESPONDENCE (B) = PERSISTENCE ACROSS OBSERVATIONS (D) — the
stable mapping of the observer's model's distinctions onto the world's persistent Difference
structures. A (successful prediction) necessary but not sufficient: a prediction can succeed by
luck, and "useful" ≠ "true". Truth is DISTINCT from knowledge (prediction), understanding (model of
models), and meaning (value-weighted prediction): its differentiator is CORRESPONDENCE. Grounded in
the world's persistent structures (NP_102/104), which pre-exist observation (M_005), so the
correspondence is well-defined and structure-relative (NP_112). True model = corresponds; useful
model = predicts well (pragmatic); false model = contradicted. Removing prediction leaves truth
partially intact (a static map can be true without predicting); removing observation or consistency
collapses truth. The minimum condition is CORRESPONDENCE (stable, consistent distinction mapping).
Classification: the world's persistent structures DERIVED (NP_102/104); observation/knowledge/
understanding EMERGENT (NP_113/114/115); truth EMERGENT (the correspondence relation); "truth =
successful prediction" REFUTED (useful ≠ true); "truth = meaning" REFUTED (value ≠ correspondence);
"truth needs a new primitive" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_124 (Wisdom Ontology Audit, COMPLETE, tests 9/9 PASSED): what is wisdom? Verdict:
wisdom = RESPONSIBLE ACTION GUIDED BY UNDERSTANDING (D) — the INTEGRATION of the prior epistemic/
teleological/agential/moral levels into right action. B (balanced understanding) and C (truth-
weighted purpose) necessary but not sufficient; A (accumulated knowledge) REFUTED (a knowledgeable
fool exists). Wisdom is the apex of the chain knowledge → understanding → meaning → purpose → wisdom
(predict → model → value → aim → integrate-and-act): it knows the truth (NP_123), understands why
(NP_115), values what matters (NP_116), and owns what it does (responsibility, NP_120). Removing
truth collapses wisdom (misguided); removing meaning collapses wisdom (directionless); removing
responsibility collapses wisdom (irresponsible); knowledge and understanding survive as the machinery
— proving wisdom is the INTEGRATION, not a separate level. The ladder: knowledgeable (knows) <
understanding (models why) < wise (acts responsibly). The minimum condition is INTEGRATION
(understanding + truth + meaning + responsibility → action). Classification: knowledge/understanding/
meaning/purpose/truth/consciousness EMERGENT (NP_114–123); wisdom EMERGENT (the integrative apex);
"wisdom = accumulated knowledge" REFUTED; "wisdom = a separate level/primitive" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_125 (Cooperation Ontology Audit, COMPLETE, tests 7/7 PASSED): what is cooperation?
Verdict: cooperation = ALIGNED MEANINGS = SHARED PURPOSES = INTEGRATED SELF-MODELS (A = B = C) —
multiple persistent observers whose value-weightings (meaning, NP_116) and selected predictions
(purpose, NP_117) align, and who mutually model each other (the social extension of the self-model,
NP_122). D (multi-observer optimization) PARTIAL (a consequence, not the essence). Cooperation is
the MULTI-OBSERVER extension of individual purpose: purpose is ONE observer's selected prediction;
cooperation is MULTIPLE observers' ALIGNED selected predictions. Conflict is the misaligned
counterpart. In a cooperating system, meaning is aligned, freedom is RETAINED (cooperation is
aligned choice, not force), and responsibility is DISTRIBUTED (each owns its part, NP_120). The
chain: consciousness = model of SELF; cooperation = model of OTHER + alignment. Classification:
meaning/purpose EMERGENT (NP_116/117); the self-model EMERGENT (NP_122); cooperation EMERGENT (the
multi-observer alignment); "cooperation = a new primitive" REFUTED; "cooperation = mere optimization"
REFUTED; "cooperation eliminates freedom/responsibility" REFUTED. No new primitive; canonical AT
unchanged.
ResearchY-NP_126 (Resonance Sailing Audit, COMPLETE, tests 8/8 PASSED): can a bound structure sail
on a propagating resonance? Verdict: YES — resonance sailing = RADIATION PRESSURE (DERIVED), NOT a
reactionless drive (REFUTED). A bound structure (NP_100) phase-locks to a propagating mode (photon,
graviton, phonon-like mode, or ψ wave) and gains the mode's phase gradient k (momentum, NP_094):
absorption transfers +k, reflection transfers +2k (elastic bounce). The transfer is a resonance
transition (generator action, NP_075), stable (the phase-lock is a fixed point). A spacecraft rides a
network wave as a sail rides the wind: the force is the wave's momentum flux, P = 2I/c (reflection)
or I/c (absorption); for the solar constant I = 1361 W/m², a reflecting sail gains 9.07 μN/m²,
accumulating unbounded in time. Momentum is CONSERVED (transferred, not created, NP_105); causality
holds (local transfer at the propagation limit, NP_092); thermodynamics holds (the absorbed count
redistributes as heat, NP_096). Classification: resonance sailing/radiation pressure DERIVED
(NP_094/075/100); reactionless drive REFUTED (violates momentum conservation); momentum/energy/
causality/thermodynamics CONSERVED. No new primitive; canonical AT unchanged.
ResearchY-NP_127 (Resonance Engineering Audit, COMPLETE, tests 8/8 PASSED): what technologies become
possible if matter is a bound resonance? Verdict: the Difference → Resonance ontology uniquely
suggests COHERENCE ENGINEERING — technologies that manipulate the mode properties (frequency, phase,
coupling) rather than a substance, because matter IS a bound resonance (NP_100). The four levers are
all valid (A = B = C = D): enhanced coupling (generator action, NP_075), reduced scattering (lower
friction, NP_095), coherence preservation (stable phase-locks), controlled phase locking (mode
selection). Allowed applications: propulsion (resonance sailing = radiation pressure, NP_126),
communication (phase as the information channel), sensing (mode-spectrum fingerprinting), energy
transfer (low-scattering coherent coupling), materials (phase-locked crystals, NP_110). Rejected
(conservation violations): reactionless drive (momentum, NP_105), perpetual motion (thermodynamics,
NP_096), faster-than-light (causality, NP_092), information destruction (Difference conservation,
NP_105). The unifying theme is COHERENCE: engineering is mode engineering — no new physics, only new
leverage. Classification: resonance phenomena DERIVED (NP_094/095/100/126); technology concepts
EMERGENT (coherence engineering); rejected concepts REFUTED (conservation). No new primitive;
canonical AT unchanged.
ResearchY-NP_128 (Coherence Resource Audit, COMPLETE, tests 8/8 PASSED): is coherence a physical
resource? Verdict: YES — coherence is a MEASURABLE RESOURCE (A) and an INDEPENDENT physical
capability (D): the phase-lock's persistence (NP_100), which enables interference, coupling, and
phase-locked work. B (bookkeeping) and C (hidden information) PARTIAL — coherence correlates with
information (I_occ = ln K − H) but is also the phase alignment, not just distinguishability.
Coherence is DISTINCT from information, energy, and entropy: it is the ORDER (locked phase) those
quantities presuppose. It is a DEGRADABLE resource with a fundamental trade-off coherence ↔ entropy:
friction (NP_095) converts coherence (low entropy) into entropy (high entropy, NP_096), so coherence
has a finite lifetime (γ⁻¹), a density, and a transfer cost — exactly as work degrades to heat. Every
capability (motion NP_094, binding NP_100, sailing NP_126) is coherent phase-locking, and all five
technologies (communication, sensing, power, materials, propulsion) benefit from maximizing it.
Classification: coherence DERIVED/EMERGENT/RESOURCE; the coherence ↔ entropy trade-off DERIVED
(NP_095/096); "coherence = a mere bookkeeping quantity" REFUTED; "coherence = just information"
REFUTED; "coherence is conserved" REFUTED (it degrades). No new primitive; canonical AT unchanged.
ResearchY-NP_129 (Coherent Matter Control Audit, COMPLETE, tests 6/6 PASSED): can bound matter be
created/modified/dissolved through coherent phase control rather than thermal heating? Verdict: YES —
phase engineering OUTPERFORMS thermal processing. Because binding = phase-locking (NP_100) and heat =
decoherence (NP_096), a structure is a set of phase-locked modes, and the lever that holds it (phase)
can, applied resonantly, also build or break it. Thermal melting is mode-blind (equipartition over N
modes, ΔS = N·ln2 bits, efficiency 1/N); coherent disruption is mode-selective (one resonant quantum
E_bind into the target lock, ΔS = ln2 bits, efficiency ~1). Advantage N× in energy efficiency and
entropy reduction (~95× for N = 95, the D96 count), at the cost of coherence (NP_128) — a favorable
exchange. Conservation laws and the 2nd law HOLD: coherent deposits energy (no free lunch), transfers
momentum (NP_075), and is MORE reversible (lower ΔS). Classification: coherent matter control DERIVED
(NP_100 + NP_096 + NP_128); applications EMERGENT; "coherent violates thermodynamics" REFUTED;
"thermal ≡ coherent" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_130 (Material Sonification & Inversion Audit, COMPLETE, tests 8/8 PASSED): can a
material be represented by a coherent resonance signature, and can the inverse signature modify it?
Verdict: YES — matter is a WRITABLE RESONANCE SCORE ("material music"). Binding = phase-locking
(NP_100) ⇒ a material is a set of phase-locked modes, so its fingerprint is the mode spectrum
{(ω_i, A_i, φ_i)} — one point in a ≥ 2^95 ≈ 3.96×10²⁸ identity space. Sonification is a bijective
scale map of modes onto the audible band (~212.6 Hz/mode for N=95 onto 20 Hz–20 kHz). Invertibility
is EXACT for the full fingerprint (round-trip ~10⁻¹⁴) and PARTIAL for frequency-only (phase lost).
Targeted coherent excitation (NP_129) writes the score — soften/reshape/disorder/re-order — at one
resonant quantum per mode, an N× (~95×) energy and entropy advantage over thermal melting. Crystal
and metal leverage clean/high-Q modes; glass and granite are moderate. Classification: fingerprint
DERIVED (NP_100); sonification CORRESPONDENCE; invertibility DERIVED (full) / PARTIAL (frequency-
only); "material music" EMERGENT; "matter is thermal-only" REFUTED. No new primitive; canonical AT
unchanged.
ResearchY-NP_131 (Critical Resonance Audit, COMPLETE, tests 7/7 PASSED): do materials have a small
set of critical resonance modes whose coherent excitation drastically reduces rigidity? Verdict: YES
— a resonance "master key": a small backbone set m ≪ N of critical (load-bearing) modes carries
macroscopic rigidity (binding = phase-locking, NP_100). Backbone size is material-specific:
crystal/metal m = 6 (~15.8× energy advantage), granite m = 20 (~4.8×), glass m = 40 (~2.4×) — ordered
materials have the strongest key. Rigidity is collective (percolation): unlocking one of 6 critical
modes drops R from 1.00 to 0.67; the full set drives R → 0. Coherent critical-mode excitation spends
m resonant quanta (m·E_bind) vs thermal's N·E_bind, with entropy m·ln2 ≪ N·ln2 — a transient,
gel-like softening that is low-entropy and REVERSIBLE (re-lock restores rigidity), not a melt.
Classification: critical modes DERIVED (NP_100 + NP_110); master key EMERGENT; "rigidity is uniformly
distributed" REFUTED; "thermal ≡ coherent" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_132 (Reversible Softening Audit, COMPLETE, tests 8/8 PASSED): can coherent excitation
of critical modes produce a reversible low-rigidity state without thermal melting? Verdict: YES —
materials can be temporarily softened and reshaped without heating. Softening and melting are
DISTINCT. Softening drives the m critical (backbone) modes open, dropping rigidity R → 0 while the
lattice order S SURVIVES (the N − m modes stay locked) — a gel-like (unjammed) solid with entropy
ΔS = m·ln2 bits. Melting heats all N modes, dropping R → 0 AND order S → 0 — a disordered liquid with
ΔS = N·ln2 bits. Softening is REVERSIBLE (stop the drive, re-lock the m modes, rigidity returns — no
re-nucleation); melting is IRREVERSIBLE (order must be re-grown, N·E_bind + nucleation barrier). The
reversibility advantage is N/m (~15.8× crystal/metal, ~4.8× granite, ~2.4× glass). Classification:
reversible softening DERIVED (NP_100 + NP_131); "softening ≡ melting" REFUTED (order survives vs
destroyed); "softening is irreversible" REFUTED (re-lock restores). No new primitive; canonical AT
unchanged.
ResearchY-NP_133 (Critical Mode Frequency Audit, COMPLETE, tests 7/7 PASSED): what frequency ranges
contain the critical rigidity modes? Verdict: the critical (backbone) modes are long-wavelength
acoustic phonons with fundamental frequency f_1 = c_s/(2L) — ~25–27.5 kHz for a 10 cm sample and
~2.5–2.75 kHz for 1 m (c_s ≈ 5000–5500 m/s) — so they live in the AUDIO-to-ULTRASOUND band
(~kHz–MHz). The Debye frequency f_D = c_s/(2a) ~ 8.3–9.2 THz is the individual-BOND mode, a factor
~10⁸× higher and physically distinct. Resonant coupling therefore favors ultrasound (audio for
meter-scale); THz is ~10⁸× off-resonance for rigidity and couples to bonds (heating/chemistry,
NP_096). Coherent softening is an ULTRASONIC-scale technology (audio for meter-scale), excitable by
existing piezoelectric transducers / horns / SAW / phased arrays. Classification: critical-mode band
DERIVED (NP_100 + NP_131); ultrasonic technology EMERGENT; "THz-scale rigidity control" REFUTED. No
new primitive; canonical AT unchanged.
ResearchY-NP_134 (Critical Mode Discovery Audit, COMPLETE, tests 7/7 PASSED): how to identify the
critical rigidity modes of an unknown material? Verdict: RIGIDITY-PERTURBATION RANKING — measure the
spectrum (NP_130), sweep a resonant drive across each mode, record the rigidity response R(e_i), and
rank by influence I_i = |∂R/∂e_i|; the top-m are the master key. Diagnostic signatures: C (coherence
sensitivity) and D (nonlinear coupling) YES — critical modes are those whose decoherence/nonlinear
drive most collapses rigidity (NP_131); A (amplitude) NO (uncorrelated with load-bearing role);
B (phase) PARTIAL. Backbone influence ≈ 1/(1−p_c) vs ≈ 0 for non-backbone, so ranking cleanly
separates the key. Crystal/metal show a sharp gap (m = 6 unambiguous); glass a diffuse gap (m = 40,
distributed). Minimum measurement: a single stiffness-vs-frequency sweep. Classification: discovery
procedure EMERGENT (from DERIVED critical-mode property NP_131); "amplitude identifies critical
modes" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_135 (Coherent Softening Experimental Audit, COMPLETE, tests 7/7 PASSED): what is the
simplest lab experiment that could falsify the critical-mode softening hypothesis? Verdict: a
matched-power ultrasound experiment on a 10 cm bar (aluminum f₁ = 25.5 kHz, quartz 28.75 kHz, steel
25.5 kHz). Measure baseline rigidity R₀ = f₀² (resonant ultrasound spectroscopy), then drive
OFF-resonance (thermal control) vs ON-resonance (critical mode) at matched power, reading rigidity via
the resonant-frequency shift, damping via Q, and ΔT. Thermal baseline ΔR/R₀ = −α·ΔT ≈ 0.015–0.045%/K.
Decisive criterion R = ΔR(f₁)/ΔR(f_off): PASS iff R ≫ 1 AND reversible AND frequency-selective; FAIL
(thermal-only) iff R ≈ 1. The off-resonance drive is the built-in thermal control, so any reversible
excess is coherent. Classification: experiment design EMERGENT (falsifiable test of DERIVED
NP_131/132); "coherent softening indistinguishable from heating" is the falsification condition. No
new primitive; canonical AT unchanged.
ResearchY-NP_136 (Variable Rigidity Bounds Audit, COMPLETE, tests 7/7 PASSED): what is the maximum
reversible rigidity reduction achievable through coherent critical-mode control? Verdict: bounded
only by failure modes, not the mechanism — coherent softening is a TRUE variable-rigidity technology
(10–90%+ reversible), not a minor 1% effect. Rigidity ratio follows percolation R(x) = max(0,
(1−x−p_c)/(1−p_c)): for ordered materials (crystal/metal/ceramic, m = 6), one critical mode unlocked
gives ~33% reduction, two ~67%, three ~100% (gel); granite (m = 20) steps ~10%. So 10%, 50%, 90% are
achievable (90% near-gel, unloaded); 1% is below the step size (thermal-scale). Maximum bounded by
(a) fracture (loaded vs unloaded), (b) heating (N/m efficient, ΔT small), (c) decoherence (keep order
S ≈ 1). Classification: variable-rigidity technology DERIVED (NP_131/132); "minor 1% effect" REFUTED.
No new primitive; canonical AT unchanged.
ResearchY-NP_137 (Real-World Evidence Audit, COMPLETE, tests 7/7 PASSED): do known experimental
results already contain evidence for coherent critical-mode softening? Verdict: PARTIAL. Seven
phenomena inventoried (resonant ultrasound spectroscopy, acoustic softening, nonlinear mesoscopic
elasticity, phononic crystals, ultrasonic welding, acoustic fluidization, dynamic modulus reduction).
Grade B (partially consistent): acoustic softening, nonlinear elasticity, acoustic fluidization;
grade C (thermal): welding, dynamic modulus; neutral: RUS, phononic. The signature (frequency-matched,
athermal, reversible acoustic softening) is OBSERVED — Langenecker's acoustoplastic effect lowers flow
stress without matching heating and re-hardens on removal. The magnitude (quantized 33–100% elastic
steps, NP_136) is UNTESTED: observed reversible elastic bound ΔE/E ≈ ΔG/G ≤ 0.30 (below the 0.33
step), while the 0.50–0.90 effects are plastic flow stress, not rigidity; no experiment shows a
reversible order-preserving R→0 gel. Strongest support = acoustoplastic effect; strongest
contradiction = missing quantized elastic drop / missing gel. Classification: signature
PARTIAL-CONSISTENT (B); magnitude UNTESTED; coherent≡heating CONTRADICTED (athermal excess exists);
full elastic gel REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_138 (Quantized Softening Audit, COMPLETE, tests 7/7 PASSED): do resonantly driven
materials exhibit discrete modulus steps? Verdict: REFUTED. NP_136's staircase R(x) = max(0,
(1−x−p_c)/(1−p_c)), m = 6, p_c = 0.5 predicts ΔE/E = 0.33/0.67/1.00, but every resonant/acoustic
elastic-modulus dataset is CONTINUOUS (nonlinear mesoscopic elasticity, acoustoplastic, DMA). Discrete
steps exist in nature only via (a) thermal first-order phase transitions — spin-crossover ~71%
(7→2 GPa) at T_c, ferroelastic precursor, α–β quartz — at arbitrary values, and (b) plastic slip
avalanches P(S) ~ S⁻ᵗᵃᵘ — wrong channel, values, and statistics. This satisfies NP_136's own
falsification condition ("drops continuously, not in ~33% steps"). Classification: quantized ladder
REFUTED; continuous athermal softening CORRESPONDENCE (NP_131/132 signature, NP_137); m = 6 staircase
DERIVED (idealization artifact). The "master key" is a continuous dial, not a three-position switch;
the parent claim (critical modes control rigidity, reversible coherent softening) survives. No new
primitive; canonical AT unchanged.
ResearchY-NP_139 (Soft Mode Literature Audit, COMPLETE, tests 6/6 PASSED): does known condensed-
matter physics already contain the equivalent of the AT critical-mode softening mechanism? Verdict:
B — an EXTENSION (a new interpretation + extension of known soft-mode physics), not genuinely new
physics. All four AT ingredients map onto known phenomena: "critical modes" = Cochran–Anderson soft
modes; "small mode set controls rigidity" = soft elastic constant C′ = (C₁₁−C₁₂)/2 / soft acoustic
phonon; "reversible athermal softening" = acoustic softening / nonlinear mesoscopic elasticity;
"continuous softening" = amplitude-dependent modulus reduction. The extension is operational: a
fixed-temperature, order-preserving (S ≈ 1), coherent variable-rigidity dial, whereas canonical soft
modes soften with temperature and culminate in a phase transition. Strongest correspondence = soft-mode
theory + premartensitic C′ anomaly; strongest discrepancy = T-driven transition vs fixed-T
order-preserving dial. Classification: critical modes = soft modes CORRESPONDENCE; fixed-T coherent
dial EXTENSION; genuinely new effect REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_140 (Fixed-Temperature Softening Audit, COMPLETE, tests 7/7 PASSED): does the
fixed-temperature, order-preserving softening dial actually exist? Verdict: PARTIAL. The principle
(drive-induced, fixed-T, athermal, reversible softening) is SUPPORTED — acoustic softening (plastic)
and nonlinear mesoscopic elasticity (elastic ≤ 0.30). But a large (≥50%) fixed-T ELASTIC-modulus
reduction is UNKNOWN: elastic ΔE/E ≈ ΔG/G ≤ 0.30 (typically ~0.01–0.10) at fixed T; 0.50–0.90 is
plastic flow stress; ≈1.00 is temperature-driven (soft mode / ferroelastic / martensitic, phase
transition). Against NP_136's 10/50/90% tiers: 10% partial, 50% plastic-only, 90% temperature-only.
Strongest FOR = NME + acoustoplastic; strongest AGAINST = large elastic softening only via phase
transition (elastic modulus is collective, not a few discrete modes). NP_136's "10–90% variable-
rigidity technology" should read as ≤ ~30% for fixed-T elastic rigidity. Classification: principle
SUPPORTED; large fixed-T elastic UNKNOWN; small fixed-T elastic SUPPORTED. No new primitive; canonical
AT unchanged.
ResearchY-NP_141 (Yield Stress Softening Audit, COMPLETE, tests 7/7 PASSED): can coherent critical-
mode excitation reduce yield stress far more strongly than elastic modulus? Verdict: SUPPORTED —
coherent excitation is primarily a YIELD-STRESS technology, not a rigidity technology. Elastic modulus
E (bond property, collective) softens ≤ ~30% (typically ~1–10%) at fixed T; yield stress σ_y (defect
property, dislocation-gated) softens 20–90% via preferential acoustic absorption at dislocations. A
10% E reduction accompanies a 2–9× larger σ_y reduction. Practical levers (forming 20–50%, machining
15–40%, drilling, cutting, stone shaping) are all yield-stress/fracture processes. Classification:
σ_y ≫ E SUPPORTED; dislocation/defect coupling SUPPORTED; yield-stress technology SUPPORTED;
NP_131 "critical modes = elastic backbone" REFINED (→ defect modes). This redirects the NP_131–140
"rigidity master key" into a "yield-stress lever" — the large useful fixed-T softening is plastic, not
elastic. No new primitive; canonical AT unchanged.
ResearchY-NP_142 (Multi-Band Resonance Control Audit, COMPLETE, tests 10/10 PASSED): is coherent
material control a single-frequency problem or a multi-band phase-coherence problem? Verdict: PARTIAL
— the "master key" is NEITHER a single resonance NOR a coordinated multi-band phase-coherence pattern;
it is a broadband, amplitude-driven defect coupling. The coupled objects are independent dislocations
with a broad, damped resonance spectrum (Granato–Lücke); single-tone works (amplitude is the lever)
but multi-tone is more effective (broader spectral coverage). Of {ω_i}, {A_i}, {φ_i}: amplitude
dominates, frequency (spectral coverage) second, phase third — phase does NOT matter more than
frequency. No small discrete mode set (NP_138); minimum = broadband amplitude above the dislocation
breakaway threshold. Classification: single-sharp-resonance key CONTRADICTED; broadband multi-band
defect coupling SUPPORTED; "phase coherence is the key resource" (NP_128) CONTRADICTED for this
channel — the resource is energy density, not phase. No new primitive; canonical AT unchanged.
ResearchY-NP_143 (Dislocation Threshold Audit, COMPLETE, tests 7/7 PASSED): what amplitude/energy
density triggers significant defect mobility? Verdict: SUPPORTED — laboratory-accessible, not
industrial-scale only. The Granato–Lücke breakaway threshold ε_c ≈ 10⁻⁶–10⁻⁵ (microstrain) gives
σ_c = E·ε_c ≈ 0.06–2 MPa and I_c = σ_c²/(2ρc) ≈ 1–5 W/cm² for aluminum, steel, quartz, granite.
Standard equipment crosses this comfortably: piezo ~1–10 W/cm², horn ~10–20+ W/cm², phased array
(focused); a 20 kHz horn's 5–20 μm displacement gives 5–1000× the breakaway strain. Industrial scale
is needed only for throughput, not threshold access — so the decisive NP_135 matched-power test is
buildable with standard lab ultrasonic equipment. Classification: threshold ε_c SUPPORTED; σ_c/I_c
SUPPORTED; lab equipment crosses SUPPORTED; overall laboratory-accessible SUPPORTED. No new primitive;
canonical AT unchanged.
ResearchY-NP_144 (Defect Spectrum Fingerprint Audit, COMPLETE, tests 8/8 PASSED): do different
materials possess unique defect-spectrum fingerprints usable for targeted yield-stress control?
Verdict: PARTIAL. Unique fingerprint SUPPORTED (mechanical spectroscopy: Bordoni dislocation peak in
aluminum, Snoek interstitial peak in steel, grain-boundary/Kê and NME features in granite/quartz);
universal broadband mobility response SUPPORTED (NP_142). Sensitivity: amplitude > bandwidth >
frequency. Tailored excitation is more energy-efficient (~2–5× power saving) but does not increase the
softening magnitude (~1×), because softening is set by dislocation mobility once the breakaway
threshold is crossed (NP_143). Classification: unique fingerprint SUPPORTED; broadband mobility
SUPPORTED; tailored-outperforms-generic PARTIAL (efficiency only); overall PARTIAL. The defect
fingerprint is a diagnostic + efficiency lever, not a magnitude lever. No new primitive; canonical AT
unchanged.
ResearchY-NP_145 (Waveform Control Audit, COMPLETE, tests 8/8 PASSED): does the temporal waveform
matter more than frequency for defect mobilization? Verdict: SUPPORTED — material control is primarily
an AMPLITUDE problem, not a frequency or waveform problem. Holding total energy constant, the five
waveforms (sine/burst/chirp/noise/impulse train) differ only in peak amplitude + bandwidth; there is
no independent shape degree of freedom. Defect mobilization tracks peak amplitude: bursts/impulses
outperform sine per unit energy because they concentrate energy into a higher peak that crosses the
breakaway threshold σ_c (NP_143) — the shock-loading result (plastic flow driven by peak stress +
duration, not shape). Dominance: amplitude > bandwidth > waveform. Classification: amplitude-dominant
SUPPORTED; "frequency problem" CONTRADICTED; "waveform problem" CONTRADICTED. No new primitive;
canonical AT unchanged.
ResearchY-NP_146 (Energy Pathway Audit, COMPLETE, tests 7/7 PASSED): where does the injected
ultrasonic energy go during yield-stress softening? Verdict: SUPPORTED — the dominant pathway is
DEFECT MOTION (mechanical/athermal), not heat. The injected energy partitions into elastic storage
(small, recoverable), defect motion (dominant, recoverable — dislocations in metals, microcracks/
contacts in rock), and heat (residual, via internal friction). Decisive evidence: Langenecker's result
that the softening is NOT reproduced by the equivalent temperature rise — it is athermal/mechanical.
Ordered metals (Al, steel) have the cleanest dislocation-dominated pathway; quartz/granite have more
frictional/contact dissipation. Classification: defect-motion pathway SUPPORTED; elastic storage small
SUPPORTED; heat residual SUPPORTED; "primarily thermal" CONTRADICTED. No new primitive; canonical AT
unchanged.
ResearchY-NP_147 (Defect Writing Audit, COMPLETE, tests 8/8 PASSED): can coherent ultrasonic
excitation create, erase, rearrange, or heal defect populations? Verdict: SUPPORTED — resonance
control is a TRUE defect-engineering tool, not only a transient softening lever. Four processes: motion
(transient), creation, annihilation, rearrangement (permanent). Sustained high-amplitude excitation
permanently reduces dislocation density (annihilation), refines subgrains, and heals microcracks
(crack-tip plasticity, e.g. copper 3× fatigue life), with residual hardening or softening. Metals are
the richest medium; quartz/granite are crack/contact-limited. The defect fingerprint (NP_144) changes
after conditioning — resonance writes the defect score (NP_130). Classification: defect motion
SUPPORTED; defect writing SUPPORTED; crack healing SUPPORTED; defect engineering SUPPORTED; "only
transient" CONTRADICTED. No new primitive; canonical AT unchanged.
ResearchY-NP_148 (Property Programming Audit, COMPLETE, tests 8/8 PASSED): can material properties be
programmed through controlled defect engineering? Verdict: SUPPORTED — resonance control can become
TRUE property programming (metal-centric), not simple softening. Target properties (hardness, yield
stress, fatigue life, fracture toughness, damping) each map onto a specific defect lever (dislocation
density, grain refinement, residual stress, crack closure). Parameter-controlled ultrasonic processes
(shot peening, UNSM up to −1400 MPa / fatigue limit up to 2×, USRP) move materials toward specified
properties — directed, not random, defect evolution. Open-loop read→write→verify works today (NP_144
read, NP_147 write); closed-loop convergence is PARTIAL (emerging). Metal-centric: ceramics/quartz/
granite lack dislocation/grain levers. Classification: correlation SUPPORTED; directed evolution
SUPPORTED; open-loop SUPPORTED; closed-loop PARTIAL; "random-only" CONTRADICTED. No new primitive;
canonical AT unchanged.
ResearchY-NP_149 (Defect State Optimization Audit, COMPLETE, tests 8/8 PASSED): do materials possess
optimal defect topologies for specific properties? Verdict: SUPPORTED — materials can be tuned toward
optimal property states via resonance-driven defect engineering, but optimization is multi-objective.
Each property (hardness, yield stress, fatigue life, fracture toughness, damping) has a distinct
optimal defect state (density/topology/distribution) reached by directed (not random) evolution
(NP_147/148). The optima conflict — hardness↔toughness (Hall–Petch), strength↔fatigue, strength↔
damping (Ashby) — so achievable states form a Pareto front, not a single point. read→write→verify
works open-loop; closed-loop optimize is PARTIAL (ML/Bayesian emerging). Classification: optimal states
SUPPORTED; directed tuning SUPPORTED; trade-offs SUPPORTED; closed-loop PARTIAL; "random suffices"
CONTRADICTED. No new primitive; canonical AT unchanged.
ResearchY-NP_150 (Property Programming Timescale Audit, COMPLETE, tests 8/8 PASSED): how quickly can
resonance-driven defect engineering modify material properties? Verdict: SUPPORTED — property
programming is a practical manufacturing technology (surface-confined), not a laboratory curiosity.
Defect motion/transient softening µs–ms; permanent writing (annihilation, subgrain, crack healing)
s–min; deep nanocrystallization tens of minutes. Ultrasonic peening/UNSM/USRP deliver surface hardness,
residual stress, and fatigue-life gains (30–100%) in seconds–minutes — comparable/faster than shot
peening (5–30 min) and far faster than annealing (hours) — and are industrially deployed. Caveat:
surface-confined; cannot reach bulk properties, so it replaces surface treatments, not heat treatment.
Classification: transient SUPPORTED; permanent writing SUPPORTED; surface shifts SUPPORTED; bulk
programming CONTRADICTED; overall SUPPORTED. No new primitive; canonical AT unchanged.
ResearchY-NP_151 (AT Contribution Audit, COMPLETE, tests 6/6 PASSED): which conclusions of NP_131–150
require Actualization Theory? Verdict: essentially NONE of the experimentally supported results require
AT — they are known condensed-matter/materials physics (athermal acoustic softening, soft modes,
dislocation/defect mechanics, ultrasonic peening). AT contributed an INTERPRETATION (the "critical mode
/ master key" is the Cochran soft mode re-labeled), a REFUTED prediction set (quantized 33/67/100%
ladder NP_138, phase-coherence resource NP_142, THz NP_133), and one UNTESTED unique prediction (the
fixed-T order-preserving large elastic dial NP_140). Remove AT and the supported results survive; the
AT-specific content does not. No confirmed new physics emerged from the chain. Classification: known
physics dominates; AT = reinterpretation + refuted/untested predictions; "AT contributed confirmed new
physics" REFUTED. This is a clean, honest separation of the coherent-softening chain. No new primitive;
canonical AT unchanged.
ResearchY-NP_152 (Material Memory Audit, COMPLETE, tests 8/8 PASSED): can defect-engineered materials
store information in a controllable, readable, rewritable way? Verdict: KNOWN PHYSICS — defect-state
memory is already industrialized (RRAM oxygen-vacancy filaments, phase-change memory, FeRAM/MRAM
domains, NV-center spins), with nm-scale density, 10+ year retention, 10⁴–10¹² cycles; read→write→
verify→erase is daily operation. AT's "writable resonance score" (NP_130) is an INTERPRETATION of this;
the specific resonance-driven (ultrasonic) rewritable memory is an AT QUESTION (surface-confined, no nm
addressability shown, NP_150). Classification: defect memory KNOWN PHYSICS; resonance score AT
INTERPRETATION; ultrasonic memory AT QUESTION; "new capability" REFUTED. No new primitive; canonical AT
unchanged.
ResearchY-NP_153 (Material State Space Audit, COMPLETE, tests 8/8 PASSED): do materials possess a
navigable state space rather than a single set of fixed properties? Verdict: KNOWN PHYSICS — materials
are systems with navigable state spaces. A material is a point in a high-dimensional defect/
microstructure manifold (dislocation density, grain size, phase fraction, residual stress, cracks),
navigated by thermal/mechanical/ultrasonic operations; transitions are reversible or irreversible by
path (anneal resets, cold work conditions). Materials science already navigates this space (phase
diagrams, TTT/CCT, processing maps); property optimization is the special case, state-space navigation
the general frame (NP_149 Pareto front). Tune/train/condition/restore are standard metallurgy. AT's
"writable resonance score" is an INTERPRETATION; whether resonance adds a new navigation axis is an AT
QUESTION. Classification: navigable state space KNOWN PHYSICS; resonance score AT INTERPRETATION;
resonance axis AT QUESTION; "new capability" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_154 (Contact Network Softening Audit, COMPLETE, tests 7/7 PASSED): for granite-like
materials, is rigidity controlled more by grain-contact networks than by internal crystal defects?
Verdict: KNOWN PHYSICS — contact-network engineering is the dominant lever. Granite is a jammed
granular system (grains + force chains + microcracks); rigidity is carried by the percolating contact
network and microcracks, which dominate the brittle (low-T/P) regime; intragranular dislocations are
minor (depth-limited). Temporary softening = contact-network disruption (acoustic fluidization, NP_137),
reducing friction/shear strength ~5–10× at microstrain and ~1–5 W/cm². NP_141–147's dislocation picture
governs metals; rock is contact-controlled. Classification: contact network KNOWN PHYSICS; acoustic
fluidization KNOWN PHYSICS; "defect engineering dominates granite" REFUTED. No new primitive; canonical
AT unchanged.
ResearchY-NP_155 (Force Chain Programming Audit, COMPLETE, tests 8/8 PASSED): can the force-chain
network inside a granite-like material be intentionally reconfigured? Verdict: KNOWN PHYSICS — force
chains break/create/redirect under load (DEM, photoelastic); directed anisotropy (chains align with
load/shear direction); multiple metastable jammed states + memory/aging; properties vary with state.
Control variables: static preload, vibration direction, frequency sweep, spatial phase (emerging). The
open question is reversible programming of a CONSOLIDATED block: below the damage threshold
reconfiguration is reversible (sub-damage fabric); above it reconfiguration = microcracking
(irreversible, chemistry unchanged). Classification: reconfiguration KNOWN PHYSICS; directed
anisotropy KNOWN PHYSICS; metastable states KNOWN PHYSICS; reversible consolidated AT QUESTION. No new
primitive; canonical AT unchanged.
ResearchY-NP_156 (Force Path Steering Audit, COMPLETE, tests 8/8 PASSED): can force-chain networks be
deliberately steered into preferred load paths? Verdict: KNOWN PHYSICS — directed steering (boundary
loading, preload, oriented vibration) localizes force chains into preferred paths, steering load
concentration, stiffness anisotropy, and fracture location (DEM/photoelastic, rock-mechanics stress
redistribution). This is the spatial version of NP_155: steering shapes WHERE stress flows, not just
that the network reconfigures. Controls: preload, vibration orientation, multi-point excitation
(emerging). The open question is reversible, vibration-only steering of a CONSOLIDATED block
(damage-bounded, NP_155). Classification: directed steering KNOWN PHYSICS; load-path localization KNOWN
PHYSICS; fracture-location control KNOWN PHYSICS; reversible consolidated steering AT QUESTION. No new
primitive; canonical AT unchanged.
ResearchY-NP_157 (Organization vs Material Audit, COMPLETE, tests 7/7 PASSED): in AT, does resonant
excitation act on material properties or on the organization of the difference network? Verdict: (B)
ORGANIZATION — resonance drives the locking/defect/contact network first, with material properties as
emergent readouts (binding = phase-locking, NP_100); the σ_y ≫ E asymmetry (NP_141) is the signature.
Reorganization (reversible topology change — re-lock NP_132, contact re-arrange NP_155/156) is a third
category between elastic deformation and irreversible damage. Per NP_151/153, "organization" is AT's
word for the known defect/contact/microstructure state space, so the matter/organization distinction is
an AT INTERPRETATION, not a new ontology. Classification: acts on organization AT INTERPRETATION;
properties = readouts KNOWN PHYSICS; new ontology REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_158 (Latent Organization State Audit, COMPLETE, tests 8/8 PASSED): does a granite-like
block contain multiple latent organizational states not normally explored? Verdict: KNOWN PHYSICS —
granite behaves as a family of latent organizational states, not a single material. At fixed
chemistry/mineral/T, a block possesses many metastable contact-network configurations (packing
fraction, contact density, fabric orientation) selected by history (memory/aging); natural/compacted/
vibrated/re-jammed states differ in stiffness, damping, and fracture behavior; sub-damage transitions
are reversible, damage-bounded ones are not (NP_155). AT's "latent organizational state" is an
INTERPRETATION of granular/jamming physics. Classification: multiple latent states KNOWN PHYSICS;
property differences KNOWN PHYSICS; framing AT INTERPRETATION; "single material" REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_159 (Structural Training Audit, COMPLETE, tests 8/8 PASSED): can a granite-like block be
trained into a preferred organizational state through repeated excitation and loading history?
Verdict: KNOWN PHYSICS — repeated preload/vibration/unload cycles accumulate a history-dependent
fabric (granular memory/aging); the final contact-network state depends on the loading sequence, so a
virgin block becomes anisotropic with a persistent load-path preference. Trained blocks differ from
virgin in anisotropy, damping, fracture location, and force-chain orientation. AT's "structural
training" is an INTERPRETATION of cyclic-shear/history physics (stress-force-fabric). Classification:
structural training KNOWN PHYSICS; history-dependent fabric KNOWN PHYSICS; framing AT INTERPRETATION;
new capability REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_160 (Organizational Field Audit, COMPLETE, tests 8/8 PASSED): can a granite-like block
support spatially varying organizational states? Verdict: KNOWN PHYSICS — a programmable internal
organizational field. Different training histories write different regional states (localized
compaction, peening depth profiles, oriented vibration), and distinct states coexist (hard surface vs
soft interior; shear bands), with spatially varying local stiffness/damping/fracture. Organization is a
fabric field φ(x) (fabric-tensor continuum), bounded by state diffusion/relaxation/coupling. AT's
"organizational field" is an INTERPRETATION; fine-grained reversible field programming in consolidated
rock is an AT QUESTION (damage-bounded, NP_155). Classification: spatial field KNOWN PHYSICS; regional
coexistence KNOWN PHYSICS; framing AT INTERPRETATION; reversible field programming AT QUESTION. No new
primitive; canonical AT unchanged.
ResearchY-NP_161 (Organizational Wave Audit, COMPLETE, tests 8/8 PASSED): can organizational states
propagate through a granite-like material as waves or fronts? Verdict: KNOWN PHYSICS — organizational
change travels as fronts (jamming fronts at/above granular sound speed, compaction fronts, shear bands,
fluidization fronts, granular solitary waves), independent of ordinary elastic waves; organization is a
dynamical field φ(x,t), not a static state. Speed spans orders of magnitude, persistence is
dissipation-limited, reversibility damage-bounded (NP_155). AT's "organizational wave" is an
INTERPRETATION of these known propagating fabric fronts. Classification: organizational fronts KNOWN
PHYSICS; dynamical-field view KNOWN PHYSICS; framing AT INTERPRETATION; new capability REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_162 (Organizational Tomography Audit, COMPLETE, tests 8/8 PASSED): can the organizational
field φ(x,t) be directly imaged and tracked? Verdict: KNOWN PHYSICS — contact density, force chains,
anisotropy, and local rigidity are directly measured by photoelasticity (force chains), X-ray
microtomography (grain/contact), ultrasonic tomography (stiffness), and coda-wave/nonlinear
interferometry (contact/crack evolution). Nonlinear acoustics + NCWI reveal contact/crack structure
that linear property measurements average away (NP_137), so the organizational map is strictly finer
than the material-property map. Real-time: photoelastic video + near-real-time CWI; for a 1 m granite
block, ultrasonic tomography + CWI/NCWI are standard. Classification: direct imaging KNOWN PHYSICS;
organizational map finer KNOWN PHYSICS; framing AT INTERPRETATION; new capability REFUTED. No new
primitive; canonical AT unchanged.
ResearchY-NP_163 (Organizational Path Dependence Audit, COMPLETE, tests 7/7 PASSED): does the path
through organizational state space matter more than the instantaneous excitation? Verdict: KNOWN
PHYSICS — granular/rock materials are athermal and non-ergodic: the final organizational state is a
function of loading/vibration HISTORY, not the final load. Identical end conditions via different paths
give different states (path A → X, path B → Y), differing in anisotropy/damping/fracture; unreachable
states, hysteresis, and training loops (reversal memory, rejuvenation) are standard. The true control
variable is the TRAJECTORY, not vibration/stress/frequency alone. AT's "organizational path dependence"
is an INTERPRETATION. Classification: path dependence KNOWN PHYSICS; trajectory-as-control KNOWN
PHYSICS; framing AT INTERPRETATION; new capability REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_164 (Organizational Amplification Audit, COMPLETE, tests 8/8 PASSED): can organizational
change amplify itself? Verdict: KNOWN PHYSICS — local rearrangement releases stress onto neighbors and
lowers their barrier, so organizational change self-reinforces into cascades/avalanches (power-law,
scale-free), distinct from non-amplifying fronts and elastic waves. The critical points are rigidity
percolation, the jamming transition, and self-organized criticality; near them small inputs produce
disproportionately large shifts. Amplification is bounded by the reversible–irreversible transition
(NP_155). The NP_131 "percolation master key" is itself this known rigidity-percolation physics.
Classification: amplification/avalanches KNOWN PHYSICS; criticality KNOWN PHYSICS; framing AT
INTERPRETATION; new capability REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_166 (Periodic Table Emergence Audit, COMPLETE, tests 9/9 PASSED): can the periodic
table emerge from AT resonance organization alone without solving the Schrödinger equation? Verdict:
NO — the periodic table's shell capacities 2, 8, 18, 32 = 2n² = 2·Σ(2l+1) are the hydrogenic
(central-field Coulomb) degeneracy (n² orbitals × spin-2), imported quantum-mechanical content, not a
Difference → D96 consequence. Electron shells are NOT resonance layers (D96 octave occupancy
[4,4,87] ≠ 2n²), NOT organization levels (the NP_101 hierarchy stacks structures at different
scales, not the internal layers of one atom), and NOT locking capacities (binding explains that an
atom exists, not how many electrons occupy each level). Observed shell counts H [1], He [2], Li
[2,1], Ne [2,8], Ar [2,8,8] are hydrogenic central-field facts; no AT-native count (octaves, BFS
shells, mirror pairs, mode counts) matches them. The structural reason is the same as for nuclear
shells (NP_087/089): exact (2l+1) degeneracies require exact O(3), which the 1D/cubic D96 structure
does not provide. Classification: atom binding + hierarchy DERIVED (NP_100/101 unchanged); electron
shells as resonance layers/organization levels REFUTED; shell capacities 2, 8, 18, 32 BOUNDARY-class
(imported hosted values, the 13.6 eV/m_e-anchor pattern); the periodic table CORRESPONDENCE (hosted
QM structure, confirming NP_017 and extending NP_085's frontier: chemistry is not derived); "AT
predicts 2, 8, 18, 32 naturally" REFUTED. No new primitive; canonical AT unchanged.
ResearchY-NP_167 (Unknown Organization States Audit, COMPLETE, hostile audit, tests 9/9 PASSED): do
there exist large-scale organization states of matter/network structures humanity has never
intentionally attempted to create? Verdict: criterion B MET — physically allowed, never-deliberately-
engineered organization states EXIST, but they are combinations/scales of known ordering axes
(long-range-ordered force-chain lattices, phononic structure written into a jammed contact network,
persistently driven jammed order, granular–topological hybrids), expressible in ordinary
condensed-matter terms — no state requires a genuinely new ordering variable. The strongly AT-
specific candidate — a phase ordered only in occupancy/actualization variables — has NO observable
separable from ordinary ordered matter (NP_151: AT re-labels the known state space; NP_157: no new
ontology), so criterion C is NOT met (SPECULATIVE at best, not a discovery). Criterion A (no unknown
states) REFUTED. The 8 known organization classes (crystal, glass, liquid, granular, phononic,
metamaterial, topological, force-chain) are all deliberately engineered; every AT ordering axis
(coherence, phase alignment, force-chain order, resonance locking, occupancy/actualization ordering)
has a known physics home (superconductors/BECs/phase-locked arrays; granular force chains; mode-
locked/synchronized oscillators; defect/contact state space). Ranking (detectability/reversibility/
stability/energy): force-chain lattice 12 (best unexplored) < fixed-T coherent dial 17 (known,
NP_132/140). Resonance CAN move a material between organization states without chemistry change,
reversibly sub-damage (NP_155/156), magnitude bounded by the small fixed-T dial (NP_140). Success
criterion: B (plausible-but-unexplored) — not C. Classification: known-state inventory CORRESPON-
DENCE; unexplored combinations EMERGENT (from known axes); AT-only organization phase SPECULATIVE;
"no unknown states" REFUTED; "organization is a new ontology" REFUTED (NP_157). No new primitive;
canonical AT unchanged.
ResearchY-NP_168 (Yang–Mills Existence and Mass Gap Audit, COMPLETE, hostile audit, tests 9/9
PASSED): AT has a DERIVED dimensionless finite-network kinematic gap for
\(C_{96}(1..6)\): \(\lambda_{\rm gap}=0.386350893377790\),
\(\omega_{\rm gap}=0.621571309969974\), multiplicity 2. Deterministic refinement gives
\(\lambda_{\rm gap}(N)=4\pi^2(91)N^{-2}+O(N^{-4})\), so the unscaled graph gap tends to zero;
fixed-compact-domain \(1/a^2\) scaling is only a kinematic finite-volume continuum gap. QG244's
classical/minimal Yang–Mills Lagrangian structure is not a quantized interacting construction.
Missing Clay requirements: Hilbert/measure, physical gauge-invariant sector, interacting
Hamiltonian/transfer matrix, controlled regulator limit, infinite-volume \(\mathbb R^4\) limit,
and a uniform positive physical lower bound. The graph gap is not a glueball mass gap; no physical
scale is imported. QG161/169/171/246/260/262/300's existing “mass-gap scale” usage is
sector-independent graph/operator nomenclature, not Clay evidence. Classification: finite gap
DERIVED; plausible regulator scaffold EMERGENT;
“AT solves the Clay problem” REFUTED. Canonical D-series classifications unchanged.
ResearchY-NP_169 (Natural Scaling vs Mass Gap Audit, COMPLETE, hostile audit, tests 7/7 PASSED):
given \(\lambda_{\rm gap}(N)=4\pi^2(91)N^{-2}+O(N^{-4})\), can any NATURAL Actualization scaling
\(f(N)\) preserve a positive mass gap \(m(N)=f(N)\lambda_{\rm gap}(N)\) as \(N\to\infty\)? Verdict:
NO. The limit depends only on the growth of \(f=O(N^e)\): e<2 → ZERO, e=2 → POSITIVE, e>2 →
DIVERGENT. Every existing AT primitive is \(O(1)\) or \(O(N)\), so all AT-native scalings —
occupancy (\(I_{\rm occ}=0.7513\), \(\ln K=1.0986\), \(\Omega_\Lambda=0.6839\), K=3, Σocc=95, N),
D96-family (degree 12, Σd²=91, N), and fixed-lattice-spacing physical scaling — give ZERO GAP. The
only POSITIVE limits are (a) the imported external length \(L_0\) of a fixed-compact-volume physical
scaling (a kinematic finite-volume gap, not an infinite-volume mass gap) and (b) the tautological
spectral-density renormalization \(\rho(\lambda_{\rm gap})\lambda_{\rm gap}\to1\) (Weyl law
\(N(\lambda)\approx2N\sqrt{\lambda/c}\), \(c=4\pi^2\cdot91\)). Ad-hoc \(f=N^2\) is underivable (no AT
primitive is \(\Theta(N^2)\)); \(f=N^3\) diverges. Classification: ZERO GAP for AT-native scalings;
"AT preserves a positive mass gap via a natural scaling" REFUTED. No D-series reclassification;
canonical AT unchanged.
ResearchY-M_001 (Measurement Origin Audit, COMPLETE, tests 7/7 PASSED): what is a
measurement event? Verdict: a measurement event is an ACTUALIZATION EVENT applied to a
DISTINGUISHABLE state — state selection (A) realized as distinguishability-becoming-
actual (B). A measurement reads BOTH quadratures of one complex mode (the {cos, sin}
two-quadrature reconstruction basis, D_037): z = a + ib exact, a alone ambiguous. What
changes: the state's identity transitions from POTENTIAL (in the complex amplitude) to
ACTUAL (a realized outcome with Born weight |ψ|² = ρ, QG216). Collapse (C) is the QG73
binary reading of the same event, not a separate mechanism. Removing measurement leaves
state identity, observability, probability, and interference intact — only the
actualization of a specific outcome is removed. Minimal condition: an actualization
event that reads both quadratures of one distinguishable mode. Trace: Difference →
distinguishability → state identity (D_039) → observability (D_037) → measurement (the
readout). Classification: state identity DERIVED (D_039); observability DERIVED
(D_037/D_038); Born probability DERIVED (QG216); measurement event EMERGENT (the
actualization readout); collapse EMERGENT (the event's binary reading — QG73 resolved).
This is the first M-series audit, the recommended first V2.2 project (NP_002). No new
primitive; canonical AT unchanged.

ResearchY-M_002 (Measurement Disturbance Audit, COMPLETE, tests 7/7 PASSED): if
measurement is an actualization event, what is the minimal unavoidable disturbance of
a distinguishable state? Verdict: the minimal unavoidable disturbance is PHASE-PINNING
— a DERIVED consequence of the read. Reading both quadratures of one complex mode (the
{cos, sin} basis, M_001/D_037) extracts AND fixes the phase θ. Magnitude is PRESERVED
(the read is a count), identity is ACTUALIZED (the state remains distinct, D_039), and
the Born weight |ψ|² = ρ is realized (QG216); only the measured mode's phase freedom is
consumed. Before-state: free phase, full interference; after-state: phase pinned.
Prove/refute: measurement without disturbance is IMPOSSIBLE (reading a phase IS pinning
it), but the disturbance is MINIMAL (magnitude, identity, probability survive).
PREDICTIONS: (1) repeated measurements are IDEMPOTENT (the same read, no further
disturbance — verified); (2) basis changes ROTATE the read frame while the complex
state z is BASIS-INVARIANT (verified: a′+ib′ = rotated z); (3) measuring k consumes its
free phase — k–k′ coherence lost unless the outcome is fed back; (4) reconstruction
z = a + ib exact. Classification: disturbance DERIVED (from the read — reading pins the
phase); measurement event EMERGENT (M_001); magnitude/identity/probability survive the
disturbance. Falsification: a read of both quadratures that leaves the phase free would
falsify. No new primitive; canonical AT unchanged.

ResearchY-M_003 (Measurement Feedback Audit, COMPLETE, tests 5/5 PASSED): does
measurement feed back into future state evolution? Verdict: YES — a measurement outcome
necessarily modifies subsequent actualization because the PINNED PHASE becomes the
initial condition of the deterministic future evolution. The phase advances per tick
(Δθ = 2πk/N, D_041); before measurement the phase is FREE (a superposition of all
trajectories), after measurement it is PINNED to θ₀ — so the future phase is
θ_t = θ₀ + t·Δθ (deterministic from the outcome). FEEDBACK = the outcome (θ₀) enters
the future evolution as the starting phase. Measured vs unmeasured: the measured mode's
future trajectory is FIXED; the unmeasured mode's is a superposition. A pinned phase
alters future interference (the joint coherence with an unmeasured mode is indefinite
unless the outcome is fed back), reciprocity (the conjugate partner is made definite),
and the actualization path (the realized count starts from the pinned value).
Prove/refute: measurement necessarily changes future evolution — YES (it fixes the
initial phase from which the deterministic evolution proceeds). Classification:
feedback DERIVED (the pinned phase feeds the evolution); phase-pinning DERIVED (M_002);
deterministic evolution DERIVED (D_041); measurement event EMERGENT (M_001).
Falsification: a measurement outcome that does NOT enter the future evolution (the
future remains a free superposition). No new primitive; canonical AT unchanged.

ResearchY-M_004 (Measurement Information Audit, COMPLETE, tests 6/6 PASSED): what is
the information-theoretic limit of a measurement event? Verdict: the maximum
information content of one actualization event is log₂(95) ≈ 6.57 bits — the size of
the distinguishable state space (D_039: 95/95 distinct complex states). A measurement
reads both quadratures of one complex mode (M_001), resolving which of the 95 states is
realized: information BEFORE = log₂ 95 (uncertainty over the state space), AFTER = 0
(outcome known). GAIN per event (uniform) = log₂(95) = 6.57 bits (the mode index).
FIXED: the phase (pinned, M_002) and the outcome (trajectory selected, M_003). LOST:
the phase freedom (superposition → one trajectory). Repeated measurements are
IDEMPOTENT — no additional information (M_002). Prove/refute: measurement creates
information — YES (the outcome resolves the uncertainty over the state space).
Maximum information content of one event = the state-space distinguishability
(log₂ 95). Falsification: an event revealing more than log₂(95) bits. Classification:
information DERIVED (from distinguishability, D_039); measurement event EMERGENT
(M_001); max info per event DERIVED (log₂ 95). Open: the Born-weighted refinement
(I_occ = 0.7513 nats, QG228). No new primitive; canonical AT unchanged.

ResearchY-M_005 (Information Conservation Audit, COMPLETE, tests 7/7 PASSED): does
measurement create information or reveal pre-existing information? Verdict: measurement
REVEALS pre-existing distinguishability and REDISTRIBUTES it — it does NOT create
information. The 6.57 bits are PRE-EXISTING in the state space (D_039: 95 distinct
states exist before any measurement); the measurement event reads both quadratures
(M_001), resolving WHICH state is realized (reveal), and converts the phase freedom
into a pinned outcome + observer knowledge (redistribute). INFORMATION BALANCE:
log₂ 95 (state space) = outcome (realized state) + observer (log₂ 95 gained) — total
CONSERVED. The underlying conservation is count conservation (Born rule Σ|ψ|²=1,
QG216). Test A/B/C: A) create — NO (the states pre-exist); B) reveal — YES; C)
redistribute — YES. REFINES M_004: "measurement creates information" is the OBSERVER's
gain; from the conservation view the event reveals + redistributes. Remove measurement:
the information still EXISTS (the 95 states remain distinguishable) — only its
resolution is absent. Prove/refute: information is conserved through actualization —
YES (H_before = H_outcome + H_observer). Falsification: a measurement that creates a
new distinguishable state or changes the total information. Classification:
distinguishability/information DERIVED (D_039, pre-existing); reveal EMERGENT (the
resolution event); redistribute DERIVED (phase → outcome + observer); conservation
DERIVED (count conservation, QG216). Open: the Born-weighted balance refinement
(I_occ = 0.7513 nats, QG228). No new primitive; canonical AT unchanged.

ResearchY-M_006 (Observer Role Audit, COMPLETE, tests 7/7 PASSED): what is the exact
role of the observer? Verdict: the observer is the RECIPIENT of the information
redistribution (M_005) — it changes only EPISTEMIC ACCESS, not the ONTIC state. Three
distinct objects: (1) STATE = the complex amplitude (exists independently, D_039 — the
95 distinct states pre-exist); (2) OBSERVABLE state = the two-quadrature reconstruction
structure (D_037: z = a + ib is a structural map); (3) MEASURED state = the pinned
outcome (M_002, requires the actualization read). Observer required for: existence —
NO; observability — NO; reconstruction — NO. Remove observer: the state, observability,
and reconstruction map all REMAIN; what becomes inaccessible is the redistribution's
RECIPIENT (no one gains knowledge, M_005). Reciprocity: the observer is ITSELF a
distinguishable subsystem (D_039) reading another — the read is symmetric (the
observer's state is also an observable complex amplitude). Compare: ontic state (the
invariant amplitude), epistemic access (the two-quadrature read), reconstruction map
(z = a + ib). The observer changes only the epistemic layer. INVARIANT formulation:
the ontic state (the complex amplitude / 95-state distinguishability), the
reconstruction map, and the total information log₂ 95 are invariant under any observer
transformation (including removing the observer). Classification: ontic state DERIVED
(D_039, invariant); observability DERIVED (D_037); reconstruction map DERIVED (D_037);
observer role EMERGENT (the redistribution's recipient); epistemic access EMERGENT (the
observer's relation). This completes the measurement program (M_001 event → M_002
disturbance → M_003 feedback → M_004 information → M_005 conservation → M_006
observer). No new primitive; canonical AT unchanged.

ResearchY-M_007 (Measurement-Program Synthesis, COMPLETE, tests 7/7 PASSED): synthesize
all measurement results. Verdict: the chain D96 → pairing → complex state → reciprocity
→ observability → measurement is FULLY CLASSIFIED. D96 → pairing: DERIVED (D_021
λ_k=λ_{N−k}; D_035 complete pairing from complex observability). Pairing → complex
state: DERIVED (D_036, the {cos,sin} pair IS [Re,Im]). Complex state → reciprocity:
EMERGENT (D_037, the two-quadrature measurement basis). Reciprocity → observability:
DERIVED (D_037, z = a + ib exact). Observability → measurement: EMERGENT (M_001, the
actualization event reading both quadratures). Measurement program: event EMERGENT
(M_001), disturbance = phase-pinning DERIVED (M_002), feedback DERIVED (M_003),
information log₂ 95 DERIVED (M_004), conservation = reveal + redistribute DERIVED
(M_005), observer = epistemic recipient EMERGENT (M_006). The DERIVED links are
structural consequences; the EMERGENT links are requirements/readings (the basis, the
event, the recipient). Only boundaries: the five R_001 inputs ({Difference, η},
{Z2-paired sector}, {3 families}, {SU(2) gauge}, {v, m_e}). Open questions:
Born-weighted information refinement (I_occ = 0.7513 nats, QG228), observer-network
coupling, the D_046 measurement predictions (disturbance relations, no-cloning bounds,
information uncertainty) as the program's testable output. No new primitive; canonical
AT unchanged.

ResearchY-M_008 (Measurement Prediction Audit, COMPLETE, tests 6/6 PASSED): does the
derived measurement chain predict anything beyond standard QM? Verdict: MOSTLY
equivalent to standard QM (option B), with TWO AT-SPECIFIC falsifiable signatures
(option C). Equivalent to QM (CORRESPONDENCE): repeated measurements idempotent (QM
P²=P), basis rotation (QM unitary), interference suppression via which-path (QM
complementarity), outcome statistics = Born shares (QM Born rule). AT-SPECIFIC
(PREDICTION): (1) DISCRETE TIME-PARAMETER — after a measurement the phase advances per
actualization TICK, Δθ = 2πk/N (the spectral rate, D_041/M_003), vs continuous time in
QM; (2) INFORMATION BOUND — max log₂(95) = 6.57 bits per event, conserved
(M_004/M_005), no 95-state bound in QM. PREDICTION TABLE: repeated/basis/interference/
Born = CORRESPONDENCE (no distinguishing signature); feedback time-parameter +
information bound = PREDICTION. REGISTRY ENTRIES: AT-P042 (post-measurement phase
advance per tick, Δθ = 2πk/N), AT-P043 (one event reveals at most log₂(95) bits).
FALSIFICATION: a continuous (non-tick-quantized) phase advance after measurement
(AT-P042); an event revealing more than log₂(95) bits (AT-P043). Classification:
repeated/basis/interference/Born CORRESPONDENCE; feedback + information bound
PREDICTION. The chain Difference → Actualization → Measurement → Prediction is closed
with two falsifiable AT-specific outputs. No new primitive; canonical AT unchanged.

ResearchY-M_009 (Measurement Prediction Discriminator Audit, COMPLETE, tests 6/6
PASSED): do AT-P042 and AT-P043 predict anything beyond standard QM? DISCRIMINATOR
verdict: EXACTLY ONE survives — AT-P042 is C) genuinely new; AT-P043 is A) already
implied by QM. AT-P042 (discrete tick, Δθ = 2πk/N): standard QM has continuous time
and a continuum of reachable phases; AT derives a discrete tick COUNT with a FINITE
phase lattice {θ₀ + m·2πk/N}, cardinality N/gcd(N,k) ≤ 96 (k=16 → 6; k=1 → 96; k=48 →
2), step DERIVED from the spectral rate (D_041/M_003). The lattice is mathematically
testable; experimental discrimination requires sub-tick phase resolution (in-principle
only, tick scale uncalibrated). AT-P043 (info per event ≤ log₂(95) = 6.57 bits): the
per-event bound log₂(d) is the STANDARD d-outcome Shannon entropy bound — QM imposes
exactly the same limit for any 95-state system; the only AT-specific content is the
derived VALUE d = 95 (D_039), not a new bound. FIRST uniquely-AT measurement
prediction = AT-P042. REGISTRY REFINEMENT: AT-P042 remains PREDICTION (refined: phase
lattice cardinality N/gcd(N,k)); AT-P043 DOWNGRADED from PREDICTION to CORRESPONDENCE
(standard bound, not a QM discriminator). FALSIFICATION: a continuous (non-lattice)
phase at sub-tick resolution falsifies AT-P042; no single-event experiment can
discriminate AT-P043 (QM imposes the same bound). No new primitive; canonical AT
unchanged.

ResearchY-M_010 (Discrete Phase Lattice Audit, COMPLETE, tests 6/6 PASSED): does
AT-P042 produce observable effects that continuous QM cannot reproduce? Verdict: NO
at every tick-sampled time. Continuous QM with the matching rate ω = 2πk/(N·τ)
reproduces AT-P042 exactly: phase at tick m (θ₀ + m·2πk/N), recurrence (N/gcd(N,k)
ticks, identical), interference (relative phase 2π(k₁−k₂)/N per tick, identical),
finite-state orbits (same lattice at samples). MODE ANALYSIS: k=1 → lattice 96,
recurrence 96; k=16 → 6/6; k=48 → 2/2 (binary phase flip {0, π}); high k (47,49,95) →
96. The ONLY difference is the SUB-TICK phase — continuous QM has intermediate phases,
AT has none — and discriminating requires a clock finer than the actualization tick
(in-principle-only: the tick is the theory's fundamental clock). CLASSIFICATION:
tick-sampled observables CORRESPONDENCE (QM reproduces exactly); the discrete
time-parameter is a structural PREDICTION; NOTHING FALSIFIED. PREDICTION STRENGTH:
structural — AT-P042 remains the first uniquely-AT measurement prediction, but its
observable content coincides with continuous QM at every accessible time (experimental
discriminator sub-tick, in-principle only). No new primitive; canonical AT unchanged.
ResearchY-M_011 (Effective O(3) Symmetry Audit, COMPLETE, tests 10/10 PASSED): can an
effective O(3) symmetry emerge from D96 actualization dynamics? Verdict: NO exact
effective O(3). Aut(C96(±1..±6)) = D₉₆ (dihedral, order 192 = ⟨r,s⟩; irreps 4 × 1D + 47 ×
2D, no irrep of dimension ≥ 3; the only multiplier stabilizing S = {±1..±6} is a = ±1;
φ(96) = 32 units act within gcd classes, NP_023). The spectrum (λ_k = Σ_s
2(1−cos(2πks/96))) has 95 positive modes, 44 distinct eigenvalues, multiplicities {2×42,
5, 6} (mirror doublets + octave 5-fold λ=12 and 6-fold λ=14), mirror λ_k = λ_{96−k}
exact — NOT a 2l+1 ladder. Spherical-harmonic degeneracies (2l+1 = 1,3,5,7,…) intersect
the D96 multiplicity set only at {5} (accidental octave coincidence). Shell patterns:
octave closures [4,8,95] do not match magic numbers / HO closures (only 8 coincides
trivially). π: the D96 spectrum is algebraic (integer Laplacian); π (transcendental)
never appears as a value — only its role can emerge (B_002). Horizon area: S ∝ A
structure EMERGENT (QG185); the exact Bekenstein quarter 1/4 is REFUTED (requires the
imported 2π quantum factor, QG196). Nuclear closures: magic numbers NOT reproduced —
confirms NP_087/089 and NP_109's weakest-link verdict. CLASSIFICATION: Aut = D₉₆ and the
degeneracy structure DERIVED; exact O(3)/2l+1 sectors, spherical-harmonic tower, shell
patterns, π value, exact 1/4, nuclear closures REFUTED; leading-order large-scale
isotropy (approximate O(3)) EMERGENT (NP_089, unchanged). No reclassification of prior
audits; no new primitive; canonical AT unchanged.

ResearchY-M_012 (Network O(3) Symmetry Audit, COMPLETE, tests 11/11 PASSED): can
effective O(3), spherical symmetry, or shell degeneracies emerge from a NETWORK of coupled
D96 systems (single-ring verdict M_011 = NO)? Verdict: EMERGENT for the genuine 3D
(vector/p-wave, l = 1) sector; REFUTED for exact O(3) and every higher spherical
observable. The 3-ring triad D96⊗D96⊗D96 (cubic lattice) has axis point group O_h (order
48; irreps {1,1,2,3,3,1,1,2,3,3}, Σd² = 48, 10 classes = 10 irreps) whose defining rep
on ℝ³ is irreducible (⟨χ,χ⟩ = 1) — a genuine 3D sector exists, absent from the single
ring (max irrep dim 2). Spherical-harmonic subduction to O_h keeps l = 0 (1) and l = 1
(3, T₁u) fully degenerate; character norms² = 1,1,2,3,4 for l = 0..4 (l=2 → 2+3, l=3 →
1+3+3 split). Joint spectrum (λ_a+λ_b+λ_c): 16,080 distinct levels over 96³ states;
first-excited degeneracy 6 = {±x̂,±ŷ,±ẑ} (O_h orbit); no (2l+1) ladder beyond l=1. The
p-triplet 3 = 2l+1 (l=1) is the only exact odd spherical sector (no O_h irrep of dim
5/7). Shells/magic numbers NOT reproduced (only s,p survive octahedral symmetry). π: the
joint spectrum is algebraic (sums of algebraic λ_k); no joint level equals π/2π/4π/3
(min |E−π| = 0.095); role EMERGENT (Weyl/lattice-point counting), value BOUNDARY.
Horizon: S ∝ A structure EMERGENT (QG185); exact quarter 1/4 REFUTED (needs imported 2π,
QG196). Critical answers: 3 coupled D96 → genuine 3D (p/vector) rep: YES (EMERGENT);
exact O(3) only in the approximate a→0 limit: REFUTED; spherical harmonics as collective
modes: only l=0,1; π from network topology: no value, role only. CLASSIFICATION: Aut
content (D96³⋊S₃, 42,467,328) and O_h irreps DERIVED; genuine 3D p/vector sector
EMERGENT; p-triplet (l=1 → T₁u) DERIVED; exact O(3), full 2l+1 ladder, d/f/g
degeneracies, shells/magic numbers, π value, exact 1/4 REFUTED; π role EMERGENT. M_011's
single-ring NO is confirmed, not reclassified; no prior-audit reclassification; no new
primitive; canonical AT unchanged.

ResearchY-M_013 (Axis-Count Selection Audit, COMPLETE, tests 8/8 PASSED): why does
Actualization select exactly 3 coupled D96 axes — is 3 unique or merely sufficient?
Verdict: MERELY SUFFICIENT as a hosted value — the exact axis count 3 is NOT dynamically
forced; UNIQUE in exactly one derived sense: rotation self-duality d(d−1)/2 = d ⇒ d = 3
(so(3) ≅ ℝ³; only in 3 axes do rotation generators form a vector of the same dimension
as the space). VERIFIED across 2/3/4/N coupled D96: point groups B_d = 2^d·d! (orders 2,
8, 48, 384 — no distinguished member); D₄ (d=2) max irrep dim 2 — no genuine 3D irrep;
O_h (d=3) has genuine 3D irreps; the defining/coordinate rep of B_d is irreducible of
dimension d (verified d=2..5), so the observed 3-component spatial vector needs d = 3;
INFORMATION d·log₂95 = 6.57, 13.14, 19.71, 26.28 bits is strictly additive per axis (no
extremum at 3); FAMILY count 3 is a single-ring span-window [4,8) quantity (D_020)
DECOUPLED from the axis count (NP_037: value-equality 3 = 3 is not identity; family-3 ≠
axis-3); SPECTRAL efficiency: DOS p = d identity (lattice-ball exponents 0.97, 1.95, 2.92
for d=1,2,3); distinct joint levels 44, 987, 22,562, 338,413 with ratio 0.46 → 0.11 →
0.026 → 0.0042 monotone (no extremum at 3). Gravity binds only d ≥ 3 (QG197 (d−2) factor;
removing the exact value 3 breaks nothing, NP_037). CLASSIFICATION: point groups B_d and
DOS p = d DERIVED; rotation self-duality d(d−1)/2 = d ⇒ d = 3 DERIVED (unique solution);
axis count 3 as a dynamically forced value REFUTED (no internal selector); information/
family/spectral-efficiency extremum at 3 REFUTED; axis-3 = family-3 REFUTED (NP_037);
exact value 3 as hosted spatial dimension CORRESPONDENCE (NP_036/037 unchanged); genuine
3D p/vector sector at d = 3 EMERGENT (M_012, unchanged). M_013 separates "why the coupled
construction can host the observed 3D world" (merely sufficient, CORRESPONDENCE) from "why
the hosted dimension is 3" (unique only via the self-duality identity, DERIVED). No
reclassification of NP_036/037/M_011/M_012; no new primitive; canonical AT unchanged.

ResearchY-M_014 (Automatic Ring-Size Rank Scan Audit, COMPLETE, tests 503/503 PASSED —
497 exhaustive N-cases + 6 facts): which ring size does the D96 attractor occupy when
every N in [16,512] is evaluated AUTOMATICALLY with NO manual candidate selection?
Verdict: N = 96 is the UNIQUE Score-4 maximizer over all 497 rings. Prior selection
audits used hand-picked candidates (D_029 window [32,300]; D_030 octave rungs; D_031
natural sizes n = p·2^k in [60,120)); M_014 removes ALL preselection and finds 96 by
exhaustive ranking alone. Score(N) = [0 unpaired] + [3 families ∧ span < 8] + [6|N] +
[N = 3·2^k]; distribution over N = 16..512: {4:1, 3:14, 2:101, 1:255, 0:126}. RANK
TABLE: N=96 score 4 (A B C D = 1 1 1 1; 3 families; 44 eigenvalues; multiplicity
pattern {2×42, 5×1, 6×1}; span 6.403 < 8; 96 = 3·32); score-3 = seed-3 rungs at wrong
family count (24, 48, 192, 384 with 1, 2, 4, 5 families) + the D_029 zero-defect rings
{60..120}\{96} (10 rings). DROP ANALYSIS (load-bearing criteria): removing the 3-family
window (B) admits the 5 rungs {24,48,96,192,384}; removing the octave rung (D) admits
the 11 zero-defect rings {60..120}; removing pairing (A) or 6-divisibility (C) leaves 96
unique. CLASSIFICATION: score components DERIVED (D_020/D_029/D_030 criteria); the
exhaustive non-manual scan DERIVED (methodological); unique Score-4 maximizer N = 96
DERIVED (global over the full closed range); score-3 sets DERIVED (reproduce
D_029/D_030); B and D as the load-bearing discriminators DERIVED (drop analysis). M_014
confirms D_029/D_030/D_031 without any manual candidate selection — the D96 selection is
not an artifact of the earlier manual windows/ladders. No reclassification of prior
audits; no new primitive; canonical AT unchanged.

ResearchY-M_015 (Score-Function Robustness Audit, COMPLETE, tests 32/32 PASSED — 26
scenario theories + 6 facts): is the M_014 result (N=96 unique Score-4 maximizer over
N=16..512) an artifact of the score function's precise form? For each criterion of
Score(N) = [0 unpaired] + [3 families ∧ span < 8] + [6|N] + [N = 3·2^k] M_015 removes
it, value-perturbs it, and threshold-perturbs it (±5/±10/±20%), recomputing the FULL
rank table over all 497 rings per variant (26 variants, no manual selection). Verdict:
ROBUST. Winner stability: 96 ∈ argmax at rank 1 in 24/26 variants — every removal
(remove A/C leaves {96} unique; remove B admits the 5 rungs {24,48,96,192,384}; remove
D the 11 zero-defect rings {60..120}) and every value/threshold perturbation that keeps
the physical family-3 requirement. The only two variants that displace 96 are the
family-req → 2 and → 4 re-targets, which change B into a DIFFERENT physical requirement
and select the family-2 rung 48 / family-4 rung 192 — a question change, not a
robustness failure. N=96 is the UNIQUE top in 16/26 variants and in ALL ±5/±10%
threshold variants (U ∈ [7.2,8.8], pairing ≤ 2, divisors 4/8/12 dividing 96, rung tol
≤ 0.08 oct). Score stability: Score(96) ∈ {4,3} in every variant (4 in 17, 3 in 9 =
removals + razor drops); never below 3, never beaten. Razor edges (uniqueness only):
window upper edge U must stay > span(96) = 6.4025 — the exact razor is −19.97% (U=6.40
at −20% just excludes 96 → 11-ring tie at score 3, 96 still in it); rung tolerance ≥
0.10 octaves admits {90, 96, 102}; the divisor must divide 96 (5/7 break uniqueness,
96 stays in the argmax). CLASSIFICATION: perturbation protocol (remove/perturb
value/±5,10,20% threshold) DERIVED (method, fixed before scan); full rank recomputation
per variant DERIVED (no manual selection); winner stability 24/26 with rank-1 in all
argmax variants DERIVED; score stability Score(96) ∈ {3,4}, never beaten DERIVED; razor
thresholds (U > 6.4025, rung tol < 0.10 oct, divisor | 96) DERIVED; overall ROBUST
verdict DERIVED; M_014 unique-maximizer result confirmed, not reclassified. M_015 shows
the D96 selection is not an artifact of the score function's exact thresholds — N=96
survives every reasonable perturbation of the M_014 score. No reclassification of prior
audits; no new primitive; canonical AT unchanged.

ResearchY-M_016 (Criterion-Independence Audit, COMPLETE, tests 10/10 PASSED): are the
four M_014 score criteria (A = 0 unpaired; B = 3 families ∧ span < 8; C = 6|N; D =
N = 3·2^k) independent over N = 16..512? Verdict: PARTIALLY REDUNDANT. Over all 497
rings: A/B/C/D hold on 354/61/83/5 rings; joint H = 2.017 bits vs ΣH = 2.135 → total
correlation 0.1179 bits = 5.5% of the entropy budget. DETERMINISTIC INCLUSION CHAIN
D ⊆ C ⊆ A: every seed-3 rung is 6-divisible and zero-unpaired; every 6-divisible ring in
[16,512] is zero-unpaired — the structural source of the redundancy. Pairwise MI is
small (max I(A;C) = 0.0912 bits); B is statistically near-independent (I(B;·) ≤ 0.0004
bit, phi ≤ 0.024); pairwise phi up to 0.285 (A–C) and 0.225 (C–D). Yet NO criterion is a
deterministic function of the others: unique-information fractions H(X_i|others)/H(X_i)
= 0.895 (A), 0.999 (B), 0.827 (C), 0.672 (D); only C is predictable from the others
beyond majority (+1.0%); PCA on the standardized criteria has eigenvalues [1.397, 1.001,
0.935, 0.667] (participation ratio 3.744 ≈ 4 effective dimensions, no dominant factor).
WHICH CRITERION SELECTS N = 96: the PAIR {B, D} — B is true exactly on the 3-family
window [60,120] (61 rings), D on the seed-3 rung ladder {24, 48, 96, 192, 384} (5 rings),
and B∧D = {96}; A and C add no further selection (A∧B∧D = B∧C∧D = {96}); they are implied
by D at rungs. CLASSIFICATION: marginal/joint entropies DERIVED; total correlation 5.5%
DERIVED; pairwise MI/phi DERIVED; deterministic nesting D ⊆ C ⊆ A DERIVED (exhaustive);
unique-information fractions DERIVED; PCA participation ratio 3.744 DERIVED; minimal
selector {B,D} of N=96 DERIVED (confirms M_014 drop analysis and M_015); PARTIALLY
REDUNDANT verdict DERIVED. M_016 confirms from information-theoretic and minimal-selector
angles that B and D are the load-bearing discriminators, A and C redundant for the 96
selection. No reclassification of prior audits; no new primitive; canonical AT unchanged.

### PRED001: Prediction Registry (standalone Zenodo publication)

Summary:

PRED001 created the official standalone Prediction Registry of the theory:
Docs/Publication/V2.0/ActualizationTheory_PredictionRegistry.tex (24-page
standalone document, pdflatex-clean). It records 41 predictions (AT-P001..AT-P041)
across the required structure: Part I Introduction (purpose, validation philosophy,
pre-registration principles), Part II Standard Model (gauge, CKM/PMNS, neutrino
sector, Majorana, electron g-2, oblique parameters), Part III Gravity (gravitational
sector, black-hole relations, Bekenstein boundary), Part IV Cosmology (ΩΛ, Ωm,
acoustic peaks, structure formation), Part V Universality (operator basis, lock law,
organization transition), Part VI Experimental Frontier (the pre-registered P1/P2/P3).

Statuses: CONSISTENT 33, PENDING 7, FALSIFIED 0, BOUNDARY 1. The three pre-registered
predictions (AT-P039 P1 106 GeV / AT-P040 P2 0νββ / AT-P041 P3 ladder) are recorded
with frozen values, uncertainty, evidence, and falsification criteria from the
immutable registry (QG193). Rules honored: accepted canonical derivations only; no new
physics; no new fits; no speculative predictions; no retrospective reinterpretation;
claim classifications (theorem/correspondence/calibration/hosted/fit) disclosed per
record.

Artifact:

Docs/Publication/V2.0/ActualizationTheory_PredictionRegistry.tex (+ .pdf, 24 pages).

### V2.0 Audit Pass: Claim Classification & Registry Consistency

Summary:

Six audits closed for Monograph V2.0: QM_AUDIT001 (basis separation),
D96_REPRO_AUDIT (graph provenance, exact reproduction), P2 reach wording,
claim classification (16 claims → theorem/necessity/correspondence/calibration/
hosted/fit; registry created), registry consistency (monograph + AT.App wording
aligned; wording-only patches, theorem wording preserved), and AT.App claim badges.

Correction of record:

5/4 = FIT (QG297, removable QG289, no mechanism); the earlier QG298 "boundary
projection" structural reading is rejected. Open items remain: observable-selection
non-uniqueness, sector-label non-uniqueness, gauge correspondence vs hosted
structure, and the ℓ₁ fitted normalization (5/4).

Artifacts:

Docs/Research/ATQG_ClaimClassificationRegistry.md, D96_REPRO_AUDIT.md,
ATQG_AUDIT_MILESTONE_V20.md; AT.App badge commit ef09eafd.


---

## Derived structure carried over from the project-memory file

## Weak-Isospin Doublet Structure

Status: DERIVED (symmetry origin)

QG151–152:

- Observable-sector spectrum is fully Z2 paired (95/95 modes).
- Weak-isospin mode selection is associated with this doublet structure.
- **PRIMARY: the Z2 doublet structure is generated by the D96 symmetry** (circulant ring / dihedral
  automorphism, QG155) — this is the fundamental structural result.
- **SECONDARY: the golden-ratio splitting δ(up) − δ(down) ≈ φ is a robust basin consequence** within the
  observable-dynamics basin (QG152 PARTIAL ROBUSTNESS). It must NOT be presented as a fundamental law.

## Fermion Structure Derived From Spectral Geometry

Chain:

QG138:
Family count from octave quantization

QG141:
Hierarchy exponents from spectral density

QG149:
Sector exponents from mode access

QG150:
Mode access from isospin selection

QG153:
Z2 doublet symmetry origin

QG154:
Neutrino origin from neutral-charge limit

QG155:
Weak-isospin doublets from D96 symmetry

QG156:
Unified spectral access law

QG157:
Effective access counts from D96 moments

QG158:
Moment orders from Z2 powers

QG159:
D96 selection origin

QG160:
Period-3 seed origin

QG161:
Gauge sector origin (1+3+8 from D96)

QG162:
Gauge coupling origin (1/α_em = 137 from D96)

QG163:
Running coupling origin (octave ladder, occupation flow)

QG164:
Continuous running origin (linear-in-G beta flow)

QG165:
CKM origin (doublet coupling, octave transition, occupancy ratio)

QG166:
CKM CP origin (chiral rotation, spectral circulation → δ_CP, J)

QG167:
PMNS origin (T3-only access → θ12, θ23, θ13, δ_ν)

QG168:
Weak boson mass origin (v = 137·ln span → MW, MZ)

QG169:
Higgs mass origin (σ_occ·span/2 → MH)

QG170:
Standard model audit (48 quantities: 25 tested / 9 partial / 14 untested)

QG171:
Muon g-2 origin ((α/2π)(1+λ₂/Σm) → a_μ; (α/2π)³·span^(1/4) → anomaly)

QG172:
Neutrino mass law ((1/Σ√m)²/(span/2) → Δm²21; sin²θ_W/Σm → Δm²31)

QG173:
Quark mass origin (me·Σ√m/√Σm² → mu; ×(Σ√m)²/occMom → md; occMom/Σm → s/d; (Σ√m)²/√Σm² → c/d; occMom²·Σm·#g/(Σ√m)⁴ → b/d; occMom·#d → t/u)

QG174:
Strong CP origin (reflection automorphism [L,P]=0 → real spectrum → real masses → arg det M = 0 → θ_QCD = 0)

QG175:
Precision EW origin (sin²θ_eff = #g/(2Σm); ΓZ = MH·cosθ_W/#g; ΓW = σ_occ²/(occMom·λ₂); ΓH = λ₂/Σm; R_b = span·g₂·sin⁴θ_W; A_FB^b = (λ_H/λ₂)²; A_FB^ℓ = MH/(MW·MZ))

QG176:
Higgs blind reconstruction (MH_A = v·√(λ₂·g₂) = 125.49 from pre-Higgs D96 only, 0.19%; MH_B = σ_occ·span/2 = 125.25, 0.003%; blindness audit passes)

QG177:
Leave-one-out validation (12 observables hidden & reconstructed from primitive base only; mean dev 0.58%, max 1.89%; 9 fully independent, 3 nominal chains inlined → INDEPENDENT)

QG178:
Electron g-2 origin (a_e = (α/2π)(1 − (occ₀/Σm)²) = 1.159655e-3, 0.0003%; Δa_e = (α/2π)³·span^¼·(occ₀/Σm)³ = 1.86e-13, anomaly-free; same mechanism as QG171 muon: Schwinger + lepton spectral fraction)

QG179:
Majorana origin (neutrino MAJORANA: T3-only channel self-conjugate 48/95, unique Q=0 no conserved charge, real mass matrix; m_ββ = 2.02e-3 eV within limits)

QG180:
Oblique parameters origin (S = occ₀/Σm = 0.0421, T = 2S = 0.0842, U = 0; T = 2S exact; consistent with EW global fit beyond masses/widths)

## Architecture Summary

Bosons:

Energy
→ Sector Ladder
→ Rung States

Gauge (QG161-165):

D96 Automorphisms (D96 = ⟨r,s⟩)
→ U(1) Photon (Z_96 rotation)
→ SU(2) Weak (2D irreps → doublets)
→ SU(3) Strong (3 families → color)
→ 1 + 3 + 8 = 12 = degree(C_96(1..6))
→ Couplings (QG162): 1/α_em = 137, α_weak = 3/Σm, α_strong = 8/Σ√m
→ sin²θ_W = #groups/(2Σm) = 0.2316
→ Running (QG163): α_i(E) = g_i/D_i(N(E)) via octave ladder + occupation flow
→ Continuous Running (QG164): linear-in-G beta flow (3, 2/3, √2/8) → log-like emergent flow
→ CKM (QG165): Vus = #doublets/(2Σm), Vcb = (ω0/ω2)^δd, Vub = 2·Vcb·(occ0/occ2)
→ CKM CP (QG166): sinδ = occ_top/Σm = 0.9158 (chiral rotation), δ_CP = 66.3°, J = 3.139e-5
→ PMNS (QG167): θ12 = √(#d/(Σm+#g)), θ23 = Σ√m/(2#d), θ13 = √(occ0/(2Σm)), δ_ν = 66.4°
→ Weak Masses (QG168): v = (Σm+#d)·ln span = 254 GeV, MW = g₂v/2 = 80.1, MZ = MW/cosθ_W = 91.4
→ Higgs Mass (QG169): MH = σ_occ·span/2 = 39.13·3.20 = 125.25 GeV (λ_H = λ₂·g₂/2 = 0.1217)
→ SM Audit (QG170): 25/48 tested (64%), 9 partial, 14 untested; gaps: g-2, neutrino masses, θ_QCD
→ Muon g-2 (QG171): a_μ = (α/2π)(1+λ₂/Σm) = 1.16644e-3 (0.045%); Δa_μ = (α/2π)³·span^(1/4) = 2.494e-9 (0.24%)
→ Neutrino masses (QG172): Δm²21 = (1/Σ√m)²/(span/2) = 7.607e-5 (1.02%); Δm²31 = sin²θ_W/Σm = 2.44e-3 (0.71%); m2 = 8.72e-3, m3 = 4.94e-2 eV
→ Quark masses (QG173): mu = me·Σ√m/√Σm² = 2.164 (0.18%); md = mu·(Σ√m)²/occMom = 4.676 (0.14%); ms = md·occMom/Σm = 93.5 (0.15%); mc = md·(Σ√m)²/√Σm² = 1269 (0.08%); mb = md·occMom²·Σm·#g/(Σ√m)⁴ = 4186 (0.13%); mt = mu·occMom·#d = 172704 (0.002%)
→ Strong CP (QG174): θ_QCD = arg det M = 0 exactly ([L,P]=0 reflection automorphism → real masses; Z2 Nelson-Barr suppression, no axion; weak CP sinδ = 0.916 is a chiral rotation phase, unaffected)
→ Precision EW (QG175): sin²θ_eff = #g/(2Σm) = 0.2316 (0.03%); ΓZ = MH·cosθ_W/#g = 2.4953 (0.004%); ΓW = σ_occ²/(occMom·λ₂) = 2.0852 (0.01%); ΓH = λ₂/Σm = 4.07 MeV (0.08%); R_b = span·g₂·sin⁴θ_W = 0.2163 (0.009%); A_FB^b = (λ_H/λ₂)² = 0.0992 (0.02%); A_FB^ℓ = MH/(MW·MZ) = 0.0171 (0.05%)
→ Higgs blind reconstruction (QG176): MH_A = v·√(λ₂·g₂) = (Σm+#d)·ln span·√(λ₂·√(4π·α_weak)) = 125.49 (0.19%, pre-Higgs D96 only); MH_B = σ_occ·span/2 = 125.25 (0.003%); MH/MW = 1.5663, MH/MZ = 1.3730; proves QG169 MH is PREDICTED, not fitted
→ Leave-one-out validation (QG177): 12 observables hidden & reconstructed from primitive base only (mean 0.58%, max 1.89%); 9 fully independent, MZ/a_μ/Δm²31 chains primitive-inlined; INDEPENDENT — no circularity
→ Electron g-2 (QG178): a_e = (α/2π)(1 − (occ₀/Σm)²) = 1.159655e-3 (0.0003%); Δa_e = (α/2π)³·span^¼·(occ₀/Σm)³ = 1.86e-13 (anomaly-free); same mechanism as muon (QG171) — Schwinger + lepton spectral fraction, opposite ends of spectrum
→ Majorana (QG179): neutrino MAJORANA — self-conjugate T3-only channel (48/95), unique Q=0 (no conserved charge), real mass matrix (reflection automorphism); m_ββ = 2.02e-3 eV (0νββ, within limits)
→ Oblique parameters (QG180): S = occ₀/Σm = 0.0421 (5.3%), T = 2S = 0.0842 (5.3%), U = 0 (ρ=1 exact SM tree-level); precision-EW consistency beyond masses/widths
→ Newton constant (QG181): M_Pl = v·(Σm·#g·occ₂)³ = 254.37·(95·44·87)³ = 1.22335e19 GeV (0.2006%); G = 1/M_Pl² = 6.6476e-11 m³/kg/s² (0.3999%); GRAVITY ORIGIN — weak scale × cube of occupation-weighted spectral content
→ G bridge (QG182): deficit parameters from D96 — m₀ = occ₀/Σm = 4/95 (S param, QG180), r₀ = ln(span), ρ̄ = 1 → GM_eff = 1/ln(M_Pl/v) = 1/(3·ln A) (0.0969%); identity occ₀·ln(span)·ln(Σm·#g·occ₂) = Σm; QG6 deficit and QG181 spectral descriptions are the same content; BRIDGE ORIGIN
→ Planck robustness (QG183): physical exponent p = ln(M_Pl/v)/ln(A) = 2.99984 (cubic to 1e-4); only A³ reproduces M_Pl (0.2%), A¹/A²/A⁴ fail by 100%/100%/3.6e7%, nearby 47-260%; no alternative A selects cubic; A = 3-factor content in 3-band d=3 spectrum; ROBUST ORIGIN
→ Mass-radius (QG184): observed M ∝ R from counting measure — per-octave/log deficit (G4ME flat-rotation-curve profile) gives a ∝ −1/r, GM_eff = m₀·R/(d·L·ρ̄) ∝ R; QG13's E ∝ R^d was compact-void assumption; with S ∝ R^(d−1) (QG12) → T ∝ 1/R, Hawking restored; MASS-RADIUS ORIGIN
→ Bekenstein quarter (QG185): structure derived (S ∝ A, M ∝ R, T ∝ 1/R) but deficit first-law gives S = R²/2 = A_cell/2 = A/(8π) — coefficient 1/2, not 1/4; exact 1/4 requires 2π quantum factor T = κ/(2π) (Hawking/Unruh), absent in D96/TRM (span/(2π) = 1.019 ≠ 1); 1/occ₀ = 1/4 (occ₀ = 4) is label identity, no mechanism; PARTIAL ORIGIN — open: derive 2π or a mechanism for the quarter
→ Frame dragging (QG186): gravitomagnetic h_0i sector is a ψ-sector observable — conformally-flat ρ-only metric has h_0i = 0 (NO frame dragging, analogue of QG26 no-lensing/QG103 retrograde perihelion); ψ spin-2 (QG44) restores full linearized Einstein incl. h_0i; rotating deficit field (matter = deficit, G4ME) sources J; Ω_LT = G(3(J·r̂)r̂−J)/(2c²r³) → GP-B 41.1 vs 39.2 mas/yr (+4.8%, inside measured 37.2±7.2), LAGEOS 30.7 vs ~31 (−1.1%); D96 G (QG181) shifts <1%; FRAME-DRAGGING ORIGIN — closes the GR observable chain (redshift, perihelion, G, Einstein structure, frame dragging)
→ GPS correction (QG187): gravitational time dilation IS the QG21 redshift law — clock rate dτ/dt = ρ^(1/d) = √(−g_00), Δτ/τ = (ρ1/ρ2)^(1/d)−1 = redshift; weak-field (GM/c²)(1/r1−1/r2) → +45.7 μs/day vs GR 45.9 (−0.4%); + SR orbital −v²/(2c²) = −7.2 → NET +38.5 vs observed +38.6 μs/day (−0.2%) = the −4.465e-10 GPS rate offset; ρ source = deficit field (G4ME), surface clock slower; GPS ORIGIN — the first practical application derived directly from the redshift law
→ Prediction audit (QG188): 10 remaining falsifiable predictions from coverage JSON — 2 testable NOW (106 GeV P1, sector-ladder P2 at LHC Run 3), 3 SOON (0νββ P3, mass-ordering P6, neutrino masses P7), 5 inaccessible (redshift-no-lensing P4, curvature-Poisson P5, MS̄ conversion P8, granularity P9, regular-core P10); ranked by impact·3+feas·2+fals·2 → recommended next target = 106 GeV resonance (score 35.0); top SOON = 0νββ m_ββ=2.02e-3 eV
→ 106 GeV evidence audit (QG188A): INCONCLUSIVE — published record shows a ~95 GeV scalar excess cluster (CMS γγ 2.9σ, ATLAS γγ 1.7σ, combined 3.1σ; CMS ττ 2.6σ; LEP bb̄ 2.3σ) aligning with the 91.19 GeV ladder rung (dev 4.5%) NOT the predicted 106.39 GeV (−10.4%); CMS 70–110 & ATLAS 66–110 GeV full-Run-2 diphoton null searches cover 106 GeV (limits 15–102 fb, no excess); LEP2 SM-like < 114.4 GeV (SM-strength hZZ only); prediction NOT excluded; Run 3 no confirmed increase; HL-LHC decisive
→ Anti-fit audit (QG189): methodology audit of QG140-188 — 49 phases: 36 PREDICTION, 2 BLIND RECONSTRUCTION (QG176 Higgs, QG177 leave-one-out), 8 DEPENDENT, 2 RETRO-FIT (QG140, QG146 fitted exponents, superseded by QG141/149), 1 OVERFIT (QG147, 3 params/3 sectors, CONFIRMED by QG148 out-of-sample); all 3 high-risk phases in the fitting era QG140-148; structural era QG149+ has no fitted parameters; gold-standard anti-fit evidence = the two blind tests
→ Pre-registered 106 GeV (QG190): prediction FROZEN before future data — central mass 106.39 GeV (lowest missing Z-anchor rung, radius 7.0, scale MZ/6 = 15.198 GeV/radius), window 98.79–113.99 GeV (stated 99–114, ±half rung-spacing 7.60), production hierarchy 9 rungs 106.4→263.4 GeV all below LHC13/FCC-hh, decay hierarchy unit quantum 15.20 GeV ×10 (0.909) + top 20.26 GeV ×1 → observable 3-family sector; forbidden-input guard (no ATLAS/CMS/fitted mass/new constant); CONFIRMED = signal in window with 15–20 GeV quanta, DISFAVORED = null
→ Pre-registered 0νββ (QG191): m_ββ = |Σ U_ei²·m_i| = 2.02 meV frozen from QG167 PMNS (s12=0.5497, s13=0.1451, δ_ν=66.4°) + QG172 masses (m1=0, m2=8.72, m3=49.4 meV, NORMAL ordering) + QG179 Majorana (real matrix ⇒ α2=α3=0); computed 2.0222 meV, dominated by m2·s12²·c13² term (2.52 meV); forbidden-input guard (no experimental limit/detector sensitivity/future measurement); CONFIRMED = measurement within ±10%, FALSIFIED = exclusion below 2.02 meV
→ Pre-registered sector-ladder spectrum (QG192): full 12-rung ladder frozen from QG121-132 — 9 predicted resonances 106.39 (PRIMARY) → 136.78 → 151.98 → 182.38 → 197.58 → 212.78 → 227.97 → 243.17 → 263.43 GeV (Z-anchor scale MZ/6 = 15.198 GeV/radius; rungs 6/9/11 aligned with t/H/Z, not predicted); multiplicities unit 15.20 GeV ×10 (0.909) + top 20.26 GeV ×1; width scale 15.20 GeV (metastable unit-quantum decay step); production ascending by mass, all below LHC13/FCC-hh; forbidden-input guard (no collider bump/catalog/fitted energy); CONFIRMED = new resonance within 5% of a frozen rung, FALSIFIED = sensitive search excludes a rung
→ Prediction registry lock (QG193): immutable registry of the 3 pre-registered predictions (P1 106 GeV, P2 0νββ m_ββ, P3 sector-ladder) with derivation phase, formula, inputs, frozen value, uncertainty, falsification per entry; readonly field + init-only records + values-unchanged guard (re-derives from QG190/191/192); only CONFIRMED/DISFAVORED/FALSIFIED may be added as outcome, never value edits; generated Docs/ATQG_Predictions.md + .json (immutable: true) via Tools/build_predictions_registry.py; REGISTRY LOCK — permanent prediction record
→ Matter deficit origin (QG194): matter = ρ̄−ρ DERIVED, not postulated — the actualization deficit IS the energy deficit (QG89 energy = actualization rate ⇒ E_def = m), carries rest mass (E=mc²), EXACTLY conserved (Noether: ∫m dV = ρ̄V−∫ρ dV exact), unique form (gradient-source identity ⇒ m = ρ̄−ρ, G4-ME5); closes the "matter = deficit is a hypothesis" open question; no new primitives
→ Matter sector origin (QG195): independent T_μν recovered WITHOUT defining T ≡ G/κ — the matter sector is the DEFICIT DUST T_μν = (ρ̄−ρ)·v_μ·v_ν (network stress = deficit mass ρ_m, link energy = actualization deficit QG89, flow = native geodesics QG20-21); conserved (Noether deficit-mass conservation + geodesic flow); independent of G (built from ρ_m and v, NOT the metric geometry — escapes the G4-G4 Lovelock obstruction that forces geometry-only tensors to G/κ); G = κT becomes a DYNAMICAL relation (deficit sources curvature), not an identity; resolves the "G=κT is an identity" open question; no new primitives
→ Quarter coefficient origin (QG196): IMPOSSIBILITY PROOF — the exact 1/4 in S = A/4 cannot be derived from D96/TRM without fitting and without importing π: QG12 boundary counting gives S/A = ln2/(4π) ≈ 0.055, deficit first-law gives 1/(8π) ≈ 0.040, and S/A = 1/4 forces b = π bits/cell (imported); 1/occ₀ = 1/4 is a wrong-units coincidence (gives 1/(16π) ≈ 0.020 physical, needs π = 1/4); structure fully derived, coefficient proven impossible; PARTIAL ORIGIN (strengthens QG185)
→ 2D→3D bridge (QG197): FULL BRIDGE — the native 2D program's ρ and conformal ansatz g = ρ^(2/d)η are dimension-generic; the Einstein tensor is analytic in d with the (d−2) factor the bridge (G≡0 at d=2 [G4-G0 geometric identity], G≠0 at d=3 [G4-G2/G3]); SAME ρ at d=3 gives G_11=0.053, G_ii=0.416, conserved (Bianchi <1e-8), d≥3 derived (QG2); closes the G4-G0 OPEN-BRIDGE gap; no new primitives
→ Higgs = collective occupation-density scalar

Fermions:

Period-3 Seed (QG160: unique complete-Z2 natural size)
→ D96 Selection (QG159: Z2 + 3-family window)
→ Sector Spectrum
→ Octave Bands
→ Family Count
→ Spectral Density
→ Mass Hierarchy
→ Z2 Doublets (D96 symmetry)
→ Sector Hierarchies (lepton / quark / neutrino)


---

