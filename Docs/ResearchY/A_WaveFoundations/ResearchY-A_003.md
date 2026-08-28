# ResearchY-A_003 — Actualization Propagation Audit (rev. 2)

**Program:** ResearchY — Wave Geometry Program
**Group:** A — Wave Foundations
**ID:** ResearchY-A_003 (permanent)
**Title:** Actualization Propagation Audit
**Status:** COMPLETE (rev. 2 — supersedes the earlier A_003 scope by incorporating
ResearchY-A_004 falsification results)
**Date:** 2026-08-28
**File:** `A_WaveFoundations/ResearchY-A_003.md`
**Depends on:** ResearchY-A_001, ResearchY-A_002, ResearchY-A_004
**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_003_Tests.cs`

> **Revision note.** This document is the deepened A_003, revised after ResearchY-A_004
> (Propagation Falsification Audit). The earlier scope (four candidate models compared)
> is folded into this version; the falsification result is now an accepted input. The
> test suite was rebuilt to the new required tests (LocalTransport, GlobalTransport,
> Z2Symmetry, OctaveOccupancies, ResonanceLocking).

---

## Purpose

Determine the **propagation law** that transforms a localized Difference excitation into
the **observed D96 resonance structure**, identify the carrier, the locality, and which
spectral features (Z2 pairing, octave occupancies, resonance locking) propagation
explains — while remaining fully compatible with canonical AT V2.0.

## Accepted (from A_001, A_002, A_004)

- **Difference ≈ mode excitation** (A_002: C5, |ψ_k|² = ρ_k exact).
- **Zero mode = uniform background** (A_002 RQ7: λ₀ = 0, ω₀ = 0, constant).
- **Branching + spectral projection survives falsification** (A_004: unique within the
  accepted D96 structure — not merely preferred).
- **Branching / diffusion / wave alone FAIL** (A_004: none generates [4,4,87], λ
  structure, or moments from its own content).

---

## Research Questions

1. What exactly propagates?
2. What is the carrier?
3. Is propagation local or global?
4. Is μ^k propagation depth?
5. Can branching be rewritten as wave transport?
6. Does propagation explain Z2 pairing?
7. Does propagation explain octave occupancies [4,4,87]?
8. Does propagation naturally produce resonance locking?

---

## Canonical References

- **Ch3** Actualization: Galton–Watson branching; ρ_k = μ^k/S; resonance =
  Conservation + Boundary; N=96 attractor.
- **Ch5/Ch6** D96: λ_k = 2Σ(1−cos 2πdk/96); ω_k = √λ_k; octave bands [4,4,87];
  multiplicity [42×2,5,6]; moments.
- **Ch7/Ch8** Operator Basis / Lock Law: Locking operator → spectral gap λ₂ (LOCKING
  read); moment-chain identity (occMom/Σm = 20.0026).
- **Ch9** Quantum Mechanics: |ψ_k|² = ρ_k; state-phase lattice θ_k = 2πk/N; magnitude
  (branching) vs phase (U(1)) separation (QG216/218).
- **MONO_PHASE002** μ^k path multiplicity.
- **ResearchY-A_001/A_002/A_004** verdicts.

---

## Assumptions

1. Canonical AT V2.0 is ground truth; nothing here modifies it.
2. A localized Difference excitation is a mode excitation on the attractor C96 (A_002).
3. The falsification result (A_004) is accepted: no single generation model reproduces
   the full spectral structure; the decomposition is branching + spectral projection.
4. "Propagation" means the rule distributing the count content (generation space,
   canonical) and, for comparison, the spatial transport on the ring (candidate models).
5. The D96 resonance structure to explain: λ_k, Z2 pairing, octave occupancies [4,4,87],
   moments, and the locking gap λ₂.
6. No ad-hoc constants, no new primitives, no new dynamics.

---

## Propagation Candidates

### P1 — Generation-space branching (canonical)

ρ_{k+1} = μ·ρ_k; ρ_k = μ^k/S, S = Σ_{j<K} μ^j. Carries the count magnitudes through the
Galton–Watson tree. Local (tree-local: each node splits to its children). This is the
canonical generation law.

### P2 — Spectral projection (canonical readout)

The modal decomposition of a localized excitation onto the eigenbasis of the attractor.
Carries the mode structure. Global: each mode φ_k spans the whole ring. This is the
readout of the graph medium.

### P3 — Wave transport (candidate, requires phase)

Modes oscillate at ω_k = √λ_k; a traveling disturbance requires the phase degree of
freedom (Ch9: phase is separate from the branching magnitude). Not canonical dynamics
(no derived wave equation, A_001 OP1).

### Comparison

| Model | Carries | Locality | Canonical? | Explains Z2/octaves/locking? |
|---|---|---|---|---|
| P1 branching | count magnitudes | local (tree) | YES | NO (scalar) |
| P2 spectral projection | mode structure | global (ring) | YES (readout) | YES (graph content) |
| P3 wave transport | displacement + phase | global (ring) | NO (needs phase/dynamics) | NO (presupposes spectrum) |

---

## Governing Equations

| Object | Equation | Status |
|---|---|---|
| branching | ρ_{k+1} = μ·ρ_k; ρ_k = μ^k/S | canonical (Ch3, QG216) |
| amplitude identity | |ψ_k|² = ρ_k | canonical (QG216) |
| eigenvalue | λ_k = 2Σ_{d=1..6}(1−cos 2πdk/96) | canonical (Ch5/Ch6) |
| mode frequency | ω_k = √λ_k | canonical |
| Z2 pairing | λ_k = λ_{N−k} (N = 96) | canonical (ring ±k degeneracy) |
| octave bands | counts of ω_k in [2^j ω_min, 2^{j+1} ω_min) | canonical (Ch6) |
| locking | λ₂ = 0.3864 (spectral gap, LOCKING read) | canonical (Ch7/Ch8) |
| lock chain | occMom/Σm = (Σm²/Σm)·(occMom/Σm²) = 20.0026 | canonical (Ch8) |
| phase lattice | θ_k = 2πk/N | canonical (Ch9) |

---

## Compatibility Matrix

| Claim | Branching (P1) | Spectral projection (P2) | Wave (P3) | Source |
|---|---|---|---|---|
| what propagates (count) | YES | read | (needs phase) | branching carries ρ |
| carrier | tree | ring/graph | ring | — |
| local vs global | local | global | global | — |
| μ^k = depth | YES | — | — | MONO_PHASE002 |
| Z2 pairing | NO | YES | (presupposes) | graph ±k symmetry |
| octaves [4,4,87] | NO | YES | (presupposes) | ω octave distribution |
| locking λ₂ | NO | YES | (presupposes) | graph spectral gap |

The compatibility matrix makes the decomposition explicit: **branching explains the
count transport and the depth; spectral projection explains every structural feature
(Z2, octaves, locking).** No single model explains all.

---

## Falsification Tests (accepted from A_004)

1. **Branching generates occupancies** → FAILS (scalar shares have no octave structure).
2. **Diffusion generates occupancies** → FAILS (relaxes to uniform; t free).
3. **Wave generates occupancies** → FAILS as independent generation (reads ω = spectral
   projection; adds non-canonical dynamics).
4. **Hybrid generates occupancies** → FAILS (coupling constant = free parameter).
5. **Any model generates λ_k** → FAILS (all operators are functions of the graph
   Laplacian; the graph is the medium).

These are accepted inputs; they are re-verified in the revised test suite where they
intersect the new questions (Z2, octaves, locking are all spectral — consistent with the
falsification verdict).

---

## Research Conclusions

**RQ1 — What exactly propagates?** The count share ρ (a unit of Difference). The mode
structure does not propagate; it is carried by the medium and read through the count.

**RQ2 — What is the carrier?** Two carriers: the Galton–Watson tree carries the count
(magnitudes) through generation space; the attractor graph (ring C96) carries the mode
structure (λ_k, ω_k). The pair is the "branching + spectral projection" decomposition.

**RQ3 — Is propagation local or global?** Both, in a precise split: **generation is
local** (branching: each node splits to its children — tree-local), **readout is
global** (spectral projection: each mode φ_k spans the whole ring, |φ_k(n)|² = 1/96 for
every site). There is no long-range coupling in the generation law; the global content
enters only through the eigenbasis.

**RQ4 — Is μ^k propagation depth?** YES (from A_002/A_003 v1, re-verified): generation k
has μ^k root-to-generation-k paths (MONO_PHASE002); the depth of propagation is the
generation index and μ^k is the path multiplicity at that depth.

**RQ5 — Can branching be rewritten as wave transport?** NO — not without new content.
Branching is a first-order scalar recurrence in the discrete generation variable
(ρ_{k+1} = μ·ρ_k); it has no spatial index and no second-order time structure, so it is
not a wave transport on the ring. A wave description requires the phase degree of
freedom, which is separate from the branching magnitude (Ch9: |ψ| from branching, θ from
U(1)). Rewriting branching as wave transport would conflate the two degrees of freedom —
the same category error A_002 RQ6 identified for "propagation instead of counting."

**RQ6 — Does propagation explain Z2 pairing?** NO — the Z2 pairing λ_k = λ_{N−k} is a
property of the circulant graph (the ring's ±k symmetry), not of propagation. Branching
shares (a geometric sequence) have no mirror symmetry. The Z2 pairing is carried by the
graph and read by spectral projection — consistent with the A_004 verdict that the mode
structure is the medium's content.

**RQ7 — Does propagation explain octave occupancies [4,4,87]?** NO — the occupancies are
the octave-band counts of the ω_k distribution (spectral, A_003 v1 RQ8 / A_004).
Propagation cannot generate them (tested). They are a property of the graph spectrum.

**RQ8 — Does propagation naturally produce resonance locking?** NO — resonance locking
(the Locking operator, spectral gap λ₂ = 0.3864; the moment-chain identity
occMom/Σm = 20.0026) is a spectral-gap structure of the graph, not a propagation output.
"Resonance = Conservation + Boundary" (Ch3) is a readout condition; the locking content
lives in the spectrum.

**Preferred model.** The propagation law is the canonical pair:

> **Generation-space branching** (local, carries the count magnitudes, μ^k = depth)
> **+ spectral projection** (global, reads the mode structure: Z2, octaves, locking).

Every structural feature of the D96 spectrum (Z2 pairing, octave occupancies, resonance
locking, moments) is carried by the graph medium and read through the propagating count.
Propagation (branching) explains the *count transport*; the *spectral structure* is the
medium's content — the falsification-surviving decomposition of A_004.

**Success criterion verdict.** The propagation model that best explains D96 structure
while remaining fully compatible with canonical V2.0 is **branching (local generation) +
spectral projection (global readout)**. No new dynamics, primitive, or constant is
introduced; every explained feature maps to a canonical object.

---

## Open Problems

1. **A_003 OP1 (graph from branching).** Can a derivation produce the attractor graph
   (hence λ_k, Z2, octaves, locking) from the branching process itself? The only open
   route to "spectral structure from propagation."
2. **A_003 OP2 (scalar-to-modal bridge).** The branching measure is 1-D (generation
   depth); the spectrum is 96-D (ring modes). The only link is |ψ_k|² = ρ_k (QG216).
3. **Phase transport (RQ5).** If the phase degree of freedom (Ch9) is included, can a
   wave transport on the ring be derived without a new primitive? (Requires the phase
   dynamics, which is open.)
4. **Locking operator form.** Can "resonance = Conservation + Boundary" be given an
   operator form that selects the octave bands and the locking gap? (Candidate; needs a
   defined operator, new work.)

---

## Next Steps

- **ResearchY-B_001 (Circular Closure):** the ring (medium) carries λ_k, Z2, octaves,
  and locking; formalize the closure and the 2π periodicity constant.
- **ResearchY-C_001 (Center Audit):** the branching root is the local source of P1; the
  ring readout is global (no center). Contrast the two.
- **ResearchY-D_001 (D96 Resonance Audit):** verify directly that Z2 pairing, octaves,
  and locking are spectral (graph) properties (RQ6–RQ8 results).
- **ResearchY-D_002 (Standing Wave Model):** the phase degree of freedom (RQ5 OP3) is
  the natural bridge to a standing-wave model.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_003_Tests.cs` (rev. 2)
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_A_003_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_A_003_PropagationDepth` | μ^k = path multiplicity at generation depth k | ✅ |
| `Y_A_003_LocalTransport` | branching is tree-local (recurrence ρ_{k+1}=μ·ρ_k; no spatial coupling) | ✅ |
| `Y_A_003_GlobalTransport` | spectral projection is global (mode support |φ_k(n)|²=1/96 on every site) | ✅ |
| `Y_A_003_Z2Symmetry` | λ_k = λ_{N−k} (47 pairs); branching shares have no mirror symmetry | ✅ |
| `Y_A_003_OctaveOccupancies` | [4,4,87] is a spectral (ω octave) property, not propagation-generated | ✅ |
| `Y_A_003_ResonanceLocking` | λ₂ = 0.3864 (LOCKING gap); lock chain occMom/Σm = 20.0026 (spectral) | ✅ |
| `Y_A_003_Run` | Research report | ✅ |

**Conclusion:** branching (local count transport, μ^k depth) + spectral projection
(global mode readout: Z2, octaves, locking) is the preferred propagation model. Every
structural feature is carried by the graph medium and read through the count. No
canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_003"`

---

## References

- Monograph V2.0: Ch3 (Actualization), Ch5/Ch6 (D96), Ch7/Ch8 (Operator/Lock),
  Ch9 (Quantum Mechanics).
- ResearchY-A_001 (R1–R8), A_002 (C5; RQ1–RQ7), A_004 (falsification verdict).
- AT-QG: QG216 (|ψ_k|² = ρ_k), QG21/QG28/QG212 (n = 1), MONO_PHASE002 (μ^k).
