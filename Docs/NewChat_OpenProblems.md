# AT Project Memory — Open Problems

Open problems, the four questions the next prompt can begin from, the reclassified (solved) problems and the two
structural counts (generation count, mass hierarchy). Split out of `Docs/NewChat_Start.md` on 2026-10-11 and kept
current.

Current state and status table: `Docs/NewChat_Start.md`. Active programs: `Docs/NewChat_ActivePrograms.md`.
Audit catalogue: `Docs/AuditIndex.md`.

---

## Next questions — where the next prompt can begin

  1. **Does any observable read the lattice dispersion inside the band?** QM_011's verdict is conditional on "no": if
     one does, the fold's position becomes observable.
  2. **Why does the clock composition multiply?** The clock law's form is fixed only up to the density-to-potential
     map; the weak field cannot answer it, so the route is multiplicativity or the compact-object measurement (G_072).
  3. **May a cell be empty?** Every G_078 bound is conditional on it, and no surviving datum probes that regime.
  4. **What sets the component count? — ANSWERED BY G_082: NOTHING THE THEORY CONTAINS.** No allowed primitive detects
     a cut, the class **conserves** the component count and never produces it, and the union of a solution is a solution,
     so it is an **initial datum of the substrate**. And it is a **gauge for everything local**: **0** observables read
     it, changing it changes the local read shape by **0** and every ratio by **0**, and the only moving local quantity
     is a **uniform shift** (`c^(−1/d)`) that no observable reads — so `c = 1` is a **coordinate choice for 23 of 29
     claims (0.793)** and a physical statement for the **6** in the transport layer. By the stated rule — every local
     observable is `c`-invariant — **`c` is unphysical**, with one disclosed exception: a **global** reachability test
     can read it. **The question this handed on was "is the continuity law part of the theory or of the coordinate
     choice?", and G_083 answered it: COORDINATE CHOICE** — see below.
  5. **What is the surviving sector's content, after the structure is stripped?** G_084 stripped every non-observable
     layer — connectivity, the component count, the transport layer, the continuity law, the flux — and measured the
     smallest theory that reproduces every surviving observable: **one state variable and three equations**, with
     **8 of 8** readouts reproduced, **observable fidelity 1.000** and **0** loss (7 of 10 named quantities removable;
     **3 of 6** equations supply no readout; a minimum cover of 3; the cost **6 dissolved claims and 0 refutations**).
     G_085 weighed the **two maps** that core keeps and found them observable **only through the composition `W ∘ f`
     and the source `f′`** (one **gauge** — the compensated shift — two **coordinates**, one **undetermined** physical
     parameter, the free room; `MapRedundancyRatio = 3.000`). G_086 then asked whether those two objects are really
     two, and the answer is **ONE FUNCTION OF THE DENSITY**: every readout is recomputed from the **rate kernel
     `K(ρ) = W(f(ρ))`** with **no potential map and no clock map** — the potential **defined** as `ln K` (the sector's
     own statement `g00 = −K²`) and the source as its gradient — and **8 of 8** come back (six bit-identical, two at the
     1-ulp `ln∘exp` rounding). So `S = (ln K)′` holds for the sector's law to **9.021E-013** and fails by a **measured
     field** for the free room (**7.636E-002**) and a GR-shaped law (**1.308E+000**), with the failure confined to the
     source-carrying readouts; the λ family (the same rate, a different split) shows the collapse is a property of the
     sector's content, the acceleration excluding every member but `λ = 1`. **And the mismatch field is proportional to
     `ln ρ`, so it vanishes at the mean density — exactly where the pinned data live** (**1.671E-010** against
     **7.636E-002** elsewhere): the structural reason the anchors are blind to the free room. `ObservableKernelDimension
     = 1` for the sector's law and **2** in general, the deciding parameter being the free room. **And G_087 refuted the
     possibility of deciding it by measurement:** the free room's signature at **J0740+6620** is **1.166E-002** against
     the AT/GR separation **1.256E-001** — a factor **10.8 below** the effect the same programme is already chasing — the
     smallest detectable extension at 3σ is **|c| = 4.6** for the current NICER-class timing (**no constraint**) and
     **0.7** for a direct 1 % surface-redshift measurement, and **below |c| = 0.72 the recorded compactness error alone
     vetoes the decision at any redshift precision** (a unit extension mimics **16.65 %** of compactness against the
     recorded **3.661 %** error). The most sensitive probe is the **AT/GR comparison itself** (**3.109E-001** per unit
     `c`), since its own value is already second order. **So the open question is a measurement that does not yet
     exist:** a surface redshift better than about **half a per cent** at the target with a compactness known far better
     than it is — or a **second target at a different compactness**, which would break the degeneracy. Referee test: a
     signature larger than the separation (an extension of order ten or more), a precision an order of magnitude beyond
     the recorded classes, or a second target.

  G_080 also **answered** the question that previously stood here ("is connectivity a consequence of the theory or an
  input to it?"): **an input**, at the level of the allowed primitives, with both alternatives (derived, emergent)
  refuted by measurement rather than by argument.


## Reclassified Solved Problems

- **"Why three generations?" — REMOVED from open problems.**

Reason:

QG138 derives

familyCount =
floor(log2(ωmax/ωmin)) + 1

and explains the observable 3-family sector.

## Generation Count

Status: LARGELY DERIVED

Origin:

Observable-sector octave quantization.

Key Results:

- QG135: family index emerges from intra-sector octave structure
- QG137: family count follows effective size N/K
- QG138: familyCount = floor(log2(ωmax/ωmin)) + 1 (fundamental)

## Mass Hierarchy

Status: PARTIALLY DERIVED

Origin:

- QG140: mass hierarchy amplification
- QG141: spectral-density exponent origin
- QG149: sector exponents from occupation-weighted access (supersedes QG147 fit)
- QG150: mode access from isospin selection
- QG154: neutrino sector neutral-charge origin

Chain:

octave structure
→ mode density
→ hierarchy exponents
→ occupation-weighted mode access
→ sector hierarchies


## Key Open Problems

- "Why three generations?" → REMOVED (solved: QG138 derives familyCount = floor(log2(ωmax/ωmin)) + 1).
- "Generic fermion hierarchy origin" → REMOVED (largely derived: QG140/141 spectral-density amplification; QG149 occupation-weighted access; QG154 neutrino neutral-charge origin).
- "Origin of lepton hierarchy" → REPLACED by "Sector-dependent hierarchy amplification."

Open Questions:

- current working hypothesis: the minimal base is Difference → Actualization → Spectrum → Physics; Q-events and the rest of the network hierarchy are downstream realizations, and the V2.0 audit pass classifies the first-peak 5/4 factor as a documented FIT (QG297, removable QG289, no mechanism) — only ℓ₁ carries it while peak ratios are pure spectral correspondences; dimensional observables still require imported calibration anchors (v, m_e, and standard unit conversion), so units are fixed by measurement/convention rather than derived from the spectrum; N=96 is inevitable within the accepted structural class (period-3 seed, Z2 half-shift, three-family octave window), but a global proof excluding every conceivable alternative closure remains open; the ResearchY QG_008/QG_009 audits pin the state-space finiteness: finite N is a BOUNDARY required only for the AT uniform-reference information observable chain (I_occ = KL(ρ‖uniform), ΩΛ), while a CONVERGENT infinite state space is internally consistent for normalization, realized entropy (geometric ρ_k = (1−r)r^k gives Σρ = 1 and H = 2.0 bits), measurement, geometry, and gravity — the first genuine failure of infinite N is the uniform reference (no normalized uniform measure exists on a countable set), refining the "information breaks first" reading of QG_008; and QG_010 RESOLVES the finiteness question in the observable direction: observability itself requires finite distinguishability (the measurement event M_001 is a finite act with finite information capacity log₂(N_obs), M_004 — an infinite observable alphabet would need log₂(N) → ∞ bits per event), so the OBSERVABLE state space is DERIVED-finite while the underlying state-space finiteness remains BOUNDARY (QG_008); and QG_011 CLOSES the chain: finite event resolution is DERIVED from Actualization's discreteness (an event is one tick Δθ = 2πk/N, D_041, producing one outcome M_001) — the final remaining boundary is the discreteness of the actualization tick itself (canonical structure); and the S_001 capstone synthesis reconstructs the whole theory as ONE object — the count density ρ (Difference → Distinguishability → event → spectrum → complex state → ρ → {Information, Geometry}), with measurement as its readout face and gravity as its geometric face; final boundary set = 5 canonical inputs (R_001) + 3 structural (QG_008/009/011), deepest single boundary = the discrete tick; and the G_001–G_011b gravity-source chain now closes the SOURCE question from both sides: ρ is the source of gravity (G_001) and NOTHING in the theory is the source of ρ (G_011 — five of the seven candidate quantities are exact functions of ρ and the two that are independent of it, phase and synchronization, are exactly ρ-inert to 1e-15), yet ρ is completely controllable in the control-theoretic sense (the hold-drive s = (I − W)ρ* is count-neutral, unique and finite in every direction), so the missing ingredient is an IMPORTED source rather than a handle; the same operator is the suppressing term (G_006/G_007, with the derived range 0 ≤ d ≤ ½ IDENTICAL to the CFL stability bound of the explicit scheme, G_011b), the realised band is set by the Poisson counting law (G_005, Δlnρ ≤ 4.8867e-6) and not by entropy, energy or any thermodynamic cost, and the laboratory realisation (G_011b) is PRACTICAL as dynamics, ASTROPHYSICAL as a clock/gravity readout, and REFUTED as a bench metric effect because AT's prediction for any real mass arrangement is exactly Newton's; and G_012 supplies the group's FIRST ACTUATOR — a three-point, count-neutral, primitive-free INCREMENTAL LOCAL FEEDBACK (s = ρ − Wρ) that freezes ANY density configuration exactly (the closed loop is the identity, so it is the unique non-trivial member of its three-point family), yet is MARGINAL (a memory with no restoring force) so it can hold a configuration but never create one, while every creating channel is excluded: the restoring family is limited to the smoothest mode (λ ≤ 1 − μ_1), compact masks are spectrally smooth (HighKShare ≤ 2.8666657e-7·D_high/w_1²), phase locking is exactly ρ-inert even locally, and the driven lattice and any non-equilibrium steady state are the MEDIUM rather than the source (a NESS is *defined* by s = (I − W)ρ*).
- the authoritative frontier catalog is now the QG295–QG300 closure run: QG296 reconstruction completes the QG223–295 map, QG297 isolates the 5/4 exception, QG298 resolves the first peak, and the remaining frontier is primarily experimental — P1 106 GeV, P2 0νββ, P3 ladder; no gravity item remains
- CORRECTION OF RECORD (V2.0 audit pass): the QG298/QG299 "5/4 = structural boundary projection (occ₀ + zero_mode)/occ₀" reading is REJECTED — that identity is a label identity without a mechanism (the same standard used to reject Bekenstein 1/occ₀ = 1/4); the registry classifies 5/4 as FIT (QG297, removable QG289). This supersedes the QG298 DETERMINATION and the QG299 "R4 5/4 CLOSED" entry; see Docs/Research/ATQG_AUDIT_MILESTONE_V20.md
- P1 (106 GeV) status after QG199: PENDING — window 99–114 GeV neither confirmed nor excluded; 95 GeV cluster = 91.19 rung; new 152 GeV excess = 151.98 rung (P3, not P1); HL-LHC decisive
- sector ladder (P3) after QG200/QG201: 12 rungs audited — CONFIRMED 3 (SM anchors Z/H/t), SUPPORTED 1 (151.98 = ~152 GeV excess, MODERATE SUPPORT alignment 2.80σ, 1-in-386), PENDING 8, none falsified
- the anti-fit reaudit (QG214) confirms the methodology is clean: QG190-213 added 3 PRE-REGISTERED + 1 REGISTRY LOCK + 20 PREDICTION, zero retro-fit/overfit; RETRO-FIT=2 (QG140/146), OVERFIT=1 (QG147) still correct; risk confined to the fitting era QG140-148
- the QG closure audits culminated: QG215 PARTIAL QG → QG219 EFFECTIVE QG (after QG216/218: amplitude magnitude + complex structure) → QG221 NEAR-COMPLETE QG (after QG220: phase θ = 2πk/N derived) → QG223 COMPLETE QG (after QG222: native metric dynamics; ψ adjudicated as an ontological boundary + tensor-sector question, NOT a blocker); QG224 confirms MONOGRAPH READY (7/7 readiness checks, paper outline generated); QG225 verifies the phase derivation DAG is ACYCLIC (226 nodes, 1349 forward edges, no cycles, 10 correction annotations only); MONO001 assembles the complete 18-chapter monograph structure (161 distinct phases referenced); MONO003 runs a hostile-referee objection audit — STRONG (38/50 closed, 12 partial, 0 open, no FATAL survives); QG226 TOE audit: PARTIAL TOE (6.5/10); QG227 derives the INITIAL CONDITIONS (uniform critical state ρ_k = 1/K — INITIAL-CONDITION ORIGIN); QG228 derives the INFORMATION CONTENT (I = ln K − H = KL(ρ‖uniform) > 0 — INFORMATION ORIGIN); QG229 cosmology closure audit: PARTIAL COSMOLOGY (2.0/6); QG230 derives Λ (LAMBDA ORIGIN); QG231 derives STRUCTURE FORMATION (STRUCTURE ORIGIN); QG232 parameter completeness audit: PARTIAL COMPLETE (37 parameters: 29 derived / 8 partial / 0 open); QG233 remaining parameter closure audit: 6 of 8 partials resolved, remaining exact gaps = Ω_Λ and Ω_m; QG234 derives the COSMOLOGICAL DENSITY FRACTIONS (FRACTION ORIGIN — Ω_Λ = I_occ/ln K = 0.6839 (0.12%), Ω_m = 0.3161 (0.26%)) — every fundamental parameter is now DERIVED or a documented BOUNDARY; QG235 external TOE checklist audit: on GENERIC TOE requirements 23 DERIVED / 1 COMPATIBLE / 6 PARTIAL / 1 OPEN (74.2% derived, 83.9% weighted) — MISSING: Inflation; QG236 inflation necessity audit: PARTIAL INFLATION — all five motive problems are SOLVED BY AT, the epoch is REPLACED; QG237 CMB spectrum origin: PARTIAL ORIGIN — n_s = 0.96497 (0.007%) DERIVED with running = 0; QG238 acoustic peak origin: PARTIAL ORIGIN — ℓ₁ = 220.48 (0.008%), r₂₁ = 2.4368 (0.035%), r₃₁ = 3.6965 (0.058%) derived from the D96 octave hierarchy; QG239 formula selection audit: 1 UNIQUE (Λ scaling) / 3 PREFERRED / 2 RETRO-SELECTION RISK (n_s, acoustic peaks); QG240 cosmology blind reproduction: BLIND SUCCESS — n_s, ℓ₁, ℓ₂/ℓ₁, ℓ₃/ℓ₁ recomputed from D96 quantities alone, max dev 0.058%; QG241 TOE closure audit: NEAR-COMPLETE TOE (8.5/10 — 6 DERIVED / 2 PARTIAL / 2 BOUNDARY / 0 OPEN); QG242 SM dynamics audit: the gauge SYMMETRY is DERIVED (QG161) but the gauge DYNAMICS (Lagrangian, vertices, propagators) is HOSTED/OPEN; QG243 gauge dynamics origin: PARTIAL ORIGIN — the interaction dynamics IS the D96 generator action, QED/weak/strong equations derived; QG244 Lagrangian origin: LAGRANGIAN ORIGIN — L = −(1/4)F^aF^a + iψ̄γ^μD_μψ − mψ̄ψ derived from the D96 generator algebra + actualization-flow energy; QG245 Higgs Yukawa audit: SM DYNAMICS NOT COMPLETE — Higgs field PARTIAL (collective scalar QG84/169), fermion mass VALUES DERIVED but the mechanism (m_f = y_f·v) PARTIAL, Yukawa interaction OPEN, Higgs potential OPEN — these are the exact remaining SM dynamics components; the theory is complete within the minimal hierarchy (Difference → Actualization → Spectrum → Physics) as a QG theory, and a near-complete TOE

