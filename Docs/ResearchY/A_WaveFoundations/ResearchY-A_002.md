# ResearchY-A_002 — Difference Disturbance Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** A — Wave Foundations
**ID:** ResearchY-A_002 (permanent)
**Title:** Difference Disturbance Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `A_WaveFoundations/ResearchY-A_002.md`
**Depends on:** ResearchY-A_001 (Wave Origin Audit)
**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_002_Tests.cs`

---

## Purpose

Determine whether **Difference** can be interpreted as a **localized disturbance** on an
initially uniform background, identify which interpretation of Difference (local
perturbation, phase displacement, graph defect, occupancy disturbance, mode excitation)
best explains the wave geometry discovered in ResearchY-A_001, and test whether a single
localized Difference can generate propagation across C96 — without modifying canonical
AT V2.0.

---

## Research Questions

1. What physically and mathematically constitutes a Difference?
2. Is Difference equivalent to: a local perturbation, a phase displacement, a graph
   defect, an occupancy disturbance, a mode excitation?
3. Can a single localized Difference generate propagation?
4. Does the disturbance naturally spread across C96?
5. Does propagation reproduce observed resonance structure?
6. Is Actualization better interpreted as propagation than counting?
7. Can the zero mode be interpreted as the undisturbed background?

---

## Canonical References

- **Ch1** The Difference: primitive; counting difference from a uniform background;
  Q-event = unit; count conservation is the definitional identity; ρ, ψ faces; zero mode
  = background (Theorem c06:thm:zero-mode).
- **Ch3** Actualization: process face of Difference; count-producing dynamics; Galton–
  Watson branching, ρ_k = μ^k/S, S = Σ_{j<K} μ^j; N=96 attractor; resonance =
  Conservation + Boundary.
- **Ch5** Inevitable Spectrum: attractor → eigenspectrum; C96(±1..±6); λ_k =
  2Σ_{d=1..6}(1−cos 2πdk/96); ω_k = √λ_k.
- **Ch6** D96 Spectrum: moments; span; octave bands [4,4,87]; multiplicity [42×2,5,6];
  Z2 doublets.
- **Ch9** Quantum Mechanics: state-phase lattice θ_k = 2πk/96; |ψ_k|² = ρ_k.
- **QG21/QG28/QG212** Light propagation: null geodesics, n = 1, redshift-without-lensing.
- **MONO_PHASE002** μ^k path multiplicity (not conserved Q-event count).
- **ResearchY-A_001** Wave Origin Audit (R1–R8 verdicts).

---

## Assumptions

1. Canonical AT V2.0 is ground truth; this investigation changes nothing in it.
2. The uniform background is the zero mode (λ₀ = 0) — per canonical Ch6 and Q7 of
   ResearchY-A_001.
3. "Localized" means supported on a small subset of the 96 ring sites (a single site for
   a point disturbance, a short segment for a compact disturbance).
4. Propagation is read in generation space (Galton–Watson branching, μ^k) and as the
   formal spreading of an eigenmode expansion over the ring — never as refractive
   spatial transport (n = 1 is canonical).
5. Each candidate interpretation is tested by whether it (a) reduces to canonical
   counting at the definitional level and (b) reproduces the A_001 wave-geometry
   observables.
6. No ad-hoc constants, no new primitives.

---

## Mathematical Candidates

### C1 — Local perturbation (occupancy bump on the ring)

A single Difference = raising one site's occupancy above the uniform level:
ρ₀(i) = 1, ρ₀(j≠i) = 0 (a Kronecker delta on the counting measure), or a small bump
δ·δ_ij on the adjacency.

- Canonical reading: this IS the canonical Q-event — a unit of Difference at one site.
- Decomposition: the delta decomposes over all 96 eigenmodes with equal weight
  (1/√N each): a delta is the "all-modes" excitation. This is the canonical content of
  "a Q-event excites the full spectrum."
- Tested in Y_A_002_LocalPerturbation, Y_A_002_ModeExcitation.

### C2 — Phase displacement (a phase twist on the ring)

A single Difference = displacing the state-phase lattice θ_k = 2πk/96 at one site.

- Canonical reading: the phase lattice is a real canonical structure (Ch9); a phase
  displacement at one site is a configuration of the phase face. It is not the count —
  the count is |ψ|².
- Wave reading: a phase twist is a circulation source — the natural carrier of a
  traveling disturbance on a ring (a phase gradient around the ring).
- Consistency: phase displacement preserves the count (|ψ|² unchanged) while changing
  the phase — the cleanest "disturbance that does not change the count."
- Tested in Y_A_002_PhaseDisplacement.

### C3 — Graph defect (a removed/added link in C96)

A single Difference = editing one link of the canonical attractor graph.

- Canonical reading: the attractor is the converged fixed point; editing a link moves
  away from the fixed point. The closure dynamics (self-reinforcing feedback bounded by
  capacity) is what repairs defects — the defect is a perturbation of the converged
  graph, not a primitive content.
- Wave reading: a local graph defect perturbs the Laplacian L → L + δL; the eigenvalue
  shifts δλ_k = ⟨φ_k|δL|φ_k⟩ are the spectral response. A single defect couples to all
  modes (rank-1 update).
- Consistency: canonical actualization converges defects away (content-independent
  attractor); a persistent graph defect is a *configuration*, not the primitive.
- Tested in Y_A_002_GraphDefect (as a rank-1 spectral response check within
  LocalPerturbation's matrix-perturbation test).

### C4 — Occupancy disturbance (redistribution of the octave occupancies)

A single Difference = moving one mode between octave bands [4,4,87].

- Canonical reading: the octave occupancies are derived spectral outputs; a disturbance
  is a re-reading, not new content. The occMom = 1900.25 is a derived read; perturbing
  the occupancy changes the moment without changing the spectrum.
- Wave reading: an occupancy disturbance is the "band population" view — it shifts the
  standing-band content of the medium.
- Consistency: occupancy is an output read, so an "occupancy disturbance" is a
  re-description of the spectrum, not an independent disturbance source.

### C5 — Mode excitation (a single eigenmode amplitude)

A single Difference = setting the amplitude of one eigenmode φ_k to 1.

- Canonical reading: |ψ_k|² = ρ_k means the amplitude of mode k IS the counting share
  of mode k — a mode excitation is literally a unit of Difference assigned to one mode.
  This is the most direct canonical identification.
- Wave reading: a single-mode excitation is a pure standing wave of the ring — the
  cleanest wave-geometry object. The A_001 wave observables (ω_k = √λ_k, Z2 ±k
  degeneracy, octave bands) are exactly the single-mode structure.
- Consistency: exact. The mode basis is canonical; exciting one mode is the canonical
  count projected onto the spectrum.
- Tested in Y_A_002_ModeExcitation.

### Comparison

| Candidate | Reduces to canonical counting? | Reproduces A_001 wave geometry? | Cleanest wave object? |
|---|---|---|---|
| C1 local perturbation | YES (the Q-event) | YES (delta = all modes) | partial |
| C2 phase displacement | YES (count preserved) | YES (phase lattice) | yes — circulation carrier |
| C3 graph defect | partial (a perturbation of the fixed point) | YES (rank-1 spectral response) | partial |
| C4 occupancy disturbance | YES (re-reading of outputs) | partial (changes occMom only) | no |
| C5 mode excitation | YES (|ψ_k|² = ρ_k exact) | YES (pure standing mode) | **yes — best** |

---

## Propagation Models

### M1 — Eigenmode propagation (spectral spreading)

A localized disturbance decomposes into modes; the formal spreading is the modal sum.
For a delta at site i:
ρ(t) = Σ_k c_k e^{−λ_k t} φ_k(i) φ_k (heat-kernel form) or
ρ(t) = Σ_k c_k cos(ω_k t) φ_k (wave form).
The disturbance spreads because it excites all modes, each with its own frequency.
Consistency: the graph heat kernel is a candidate (open problem OP5 of A_001); the wave
form requires dynamics not canonically derived (A_001 OP1). Used here only as a formal
decomposition test, not a new propagation law.

### M2 — Generation-space propagation (canonical branching)

Actualization spreads count through the Galton–Watson tree: a unit at generation 0
reaches μ^k paths at generation k, ρ_k = μ^k/S. This is the canonical propagation —
propagation in generation space. Q6 asks whether this is better read as propagation than
counting; the answer: it is the *same* dynamics read as spreading rather than as tallying
— both are the branching process. No new content.

### M3 — Phase-gradient circulation (ring transport)

A phase displacement (C2) creates a phase gradient around the ring; on a closed ring a
constant phase gradient is a persistent circulation. Consistency: the CKM CP phase is
already read as "chiral circulation / spectral circulation" (QG166) — a circulation
notion is already canonical in the spectral read. A *dynamical* traveling wave on the
ring remains a candidate (A_001 OP1), not a derived result.

### Which model is canonical?

Only M2 is fully canonical (it IS the branching process). M1 is a formal decomposition
of the canonical spectrum (the heat kernel is a candidate, not derived). M3 re-uses the
canonical circulation notion but adds no dynamics. The audit therefore concludes: the
disturbance's *content* spreads canonically in generation space (M2); its *spectral
signature* is the modal decomposition (M1, formal); its *phase* can circulate (M3,
notion only).

---

## Compatibility with Canonical AT

| Candidate/model | Compatible? | Constraint honored |
|---|---|---|
| C1 local perturbation | YES | is exactly a Q-event (count definition) |
| C2 phase displacement | YES | preserves count; phase lattice canonical |
| C3 graph defect | YES (configuration only) | attractor is the fixed point; defects repair via closure |
| C4 occupancy disturbance | YES (re-reading only) | occupancy is a derived output |
| C5 mode excitation | YES (exact) | |ψ_k|² = ρ_k is canonical (QG216) |
| M1 eigenmode propagation | FORMAL ONLY | no new propagation law claimed |
| M2 generation-space propagation | YES (exact) | is the branching process (Ch3, MONO_PHASE002) |
| M3 phase-gradient circulation | NOTION ONLY | CKM CP circulation is already canonical (QG166) |

No candidate requires a new primitive or an ad-hoc constant; all are re-readings of
canonical objects.

---

## Contradictions

| # | Risk | Canonical constraint | Resolution |
|---|---|---|---|
| 1 | "propagation" implies spatial medium | n = 1 null-geodesic propagation; conformally invariant (QG21/28/212) | Propagation is read in generation space (M2) or as formal modal decomposition (M1); no refractive medium |
| 2 | "standing wave" implies dynamics | the attractor is static; no derived wave equation | Modes are static normal modes; the wave form is a formal candidate, not a derived law |
| 3 | "graph defect" implies changing the canonical graph | the attractor C96 is the converged fixed point | A defect is a configuration/perturbation of the fixed point; the closure dynamics repairs it |
| 4 | "propagation instead of counting" | Actualization's identity IS count conservation (Ch3) | M2 is the same branching process read as spreading; counting and propagation are the same content, not alternatives |
| 5 | zero mode as "undisturbed background" | the zero mode is the uniform configuration (Ch6) | Compatible: the zero mode IS the rest state of the medium |

None of these is a contradiction of the re-reading; each is a constraint the disturbance
language must satisfy, and the audit adopts all five.

---

## Research Conclusions

**RQ1 — What constitutes a Difference?** Canonically, a Difference is a unit of count
(Q-event) — the counting difference from a uniform background. Mathematically, any of the
five candidates can *represent* a unit: as a delta on ρ (C1), as a phase twist (C2), as a
link edit (C3), as an occupancy shift (C4), or as a mode amplitude (C5). The definitional
content is the count; the representations are configurations.

**RQ2 — Best equivalence.** **C5 mode excitation is the best interpretation**: the
canonical identity |ψ_k|² = ρ_k (QG216) means a unit of Difference assigned to mode k IS a
mode excitation — exact, not merely analogous. C1 (delta = all modes) is the point-source
view; C2 (phase) is the circulation view. C3 and C4 are configuration re-readings.

**RQ3 — Can a single localized Difference generate propagation?** In generation space,
YES: one unit at the root reaches μ^k paths at generation k (M2). In spectral form, a
single delta excites all modes (M1 formal). A *dynamical* traveling disturbance on the
ring is not derived (open, A_001 OP1).

**RQ4 — Does the disturbance spread across C96?** The delta's modal decomposition covers
all 96 sites (equal |c_k|² = 1/96 per mode); the formal heat-kernel spread is
K_t(i,j) = Σ_k e^{−λ_k t} φ_k(i)φ_k(j). Canonically, the count spreads through the
branching tree, not through a spatial diffusion.

**RQ5 — Does propagation reproduce observed resonance structure?** YES as a re-reading:
the modal decomposition of any localized disturbance uses exactly the canonical modes
(ω_k = √λ_k, Z2 ±k degeneracy, octave bands [4,4,87]) — the resonance structure IS the
eigenbasis in which propagation is written. The resonance content is unchanged.

**RQ6 — Is Actualization better read as propagation than counting?** NO — and this is a
structural finding. Actualization's definitional identity is count conservation (Ch3).
"Propagation" and "counting" are two readings of the same branching process (M2): the
process tallies the count (counting) and spreads it through the generation tree
(propagation). Neither is "better"; they are the same content in two vocabularies.
Adopting "propagation instead of counting" would risk a category error (a new process
claim), so the audit keeps the canonical counting reading and adds propagation as a
parallel description.

**RQ7 — Can the zero mode be interpreted as the undisturbed background?** YES. The zero
mode (λ₀ = 0, uniform eigenvector) is canonically the background (Ch6); it is the rest
state of the medium — the state a disturbance is "against." The 95 positive modes are the
excited deviations. This is exact, not a re-reading.

**Success criterion verdict.** The interpretation of Difference that best explains the
A_001 wave geometry while remaining fully compatible with canonical AT is:

> **Difference = a unit of count whose spectral representation is a mode excitation
> (C5), whose point-source form is a delta on the counting measure (C1), and whose phase
> form is a phase displacement (C2) — propagating canonically in generation space (M2),
> with its spectral signature the canonical modal decomposition (M1).**

The zero mode is the undisturbed background (RQ7); the resonance structure is the
eigenbasis itself (RQ5); and no new primitive, constant, or dynamics is introduced.

---

## Open Problems

1. **Dynamical traveling wave (A_001 OP1).** Does a derived wave equation on the ring
   exist, or only static normal modes? The wave-form modal sum is a formal candidate.
2. **Heat-kernel propagation (A_001 OP5).** Would the graph heat kernel reproduce the
   null-geodesic law (n = 1)? The kernel is a candidate, not derived.
3. **Phase-gradient circulation as source of CKM CP.** Is the C2 circulation notion
   connected to the QG166 chiral circulation beyond the vocabulary level? (Parallel
   description or strengthening — to be determined; no claim-status change.)
4. **Defect repair dynamics.** Canonical closure repairs graph defects (content-
   independent attractor). Can the repair be quantified as a rank-1 spectral response,
   and does it have any observable signature? (Research-only question.)
5. **Delta-delta spread on C96.** What is the precise heat-kernel spread profile
   K_t(i,j) for a delta source on C96, and does it match any canonical observable?
   (Candidate, not a claim.)

---

## Next Steps

- **ResearchY-A_003 (Actualization Propagation):** examine the branching as propagation
  in generation space; test consistency with the null-geodesic law; formalize M2.
- **ResearchY-B_001 (Circular Closure):** formalize the ring closure and the 2π
  periodicity constant (building on A_001 R7 and the A_002 zero-mode/rest-state result).
- **ResearchY-C_001 (Center Audit):** the delta-source on C96 has no distinguished site
  (translation invariance); contrast with the branching root as the only natural source.
- **ResearchY-D_001 (D96 Resonance Audit):** verify that the modal decomposition of any
  localized disturbance is exactly the canonical eigenbasis (RQ5 result).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_002_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_A_002_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_A_002_UniformBackground` | zero mode is the uniform rest state | ✅ |
| `Y_A_002_LocalPerturbation` | delta on ρ = canonical Q-event; decomposes over all modes | ✅ |
| `Y_A_002_PhaseDisplacement` | count-preserving phase displacement | ✅ |
| `Y_A_002_ModeExcitation` | |ψ_k|² = ρ_k; single-mode excitation | ✅ |
| `Y_A_002_PropagationAcrossC96` | generation-space spread; modal coverage | ✅ |
| `Y_A_002_ZeroModeAsRestState` | zero mode = undisturbed background | ✅ |

**Conclusion:** C5 (mode excitation) is the best interpretation of Difference, exactly
matching the canonical identity |ψ_k|² = ρ_k; C1 (delta) is the point-source form; C2
(phase) the circulation form. Propagation is generation-space branching (M2), not spatial
transport. The zero mode is the undisturbed background. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_002"`

---

## References

- Monograph V2.0: Ch1 (Difference), Ch3 (Actualization), Ch5 (Inevitable Spectrum), Ch6
  (D96 Spectrum), Ch9 (Quantum Mechanics).
- AT-QG: QG216 (Quantum Amplitude Origin, |ψ_k|² = ρ_k), QG21/QG28/QG212 (Light
  Propagation), QG166 (CKM CP chiral circulation), MONO_PHASE002 (μ^k path multiplicity).
- ResearchY-A_001 (Wave Origin Audit) — R1–R8 verdicts.
