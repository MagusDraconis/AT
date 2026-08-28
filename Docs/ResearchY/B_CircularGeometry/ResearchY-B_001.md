# ResearchY-B_001 — Circular Closure Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** B — Circular Geometry
**ID:** ResearchY-B_001 (permanent)
**Title:** Circular Closure Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `B_CircularGeometry/ResearchY-B_001.md`
**Depends on:** ResearchY-A_001, A_002, A_004, A_005
**Test suite:** `AT.Tests/ResearchY/B_CircularGeometry/Y_B_001_Tests.cs`

---

## Purpose

Determine whether **circular closure** is a necessary consequence of the chain

```
Difference → Actualization → Attractor → Graph → Laplacian → Eigenbasis
```

and whether **π and 2π emerge as consequences** of the canonical framework rather than
imported constants.

## Accepted (from A_001, A_002, A_004, A_005)

- **D96 = standing-wave content of the closed ring C96** (A_001 R5/R8); 2π appears as the
  ring-closure periodicity constant in the spectral layer (A_001 R7).
- **Difference = mode excitation; zero mode = undisturbed background** (A_002).
- **Branching + spectral projection survives falsification** (A_004); the eigenbasis is
  the unique diagonalizing basis of the graph Laplacian (A_005).
- **Spectral projection is derived from the actualization attractor** (A_005: minimal
  origin chain).

---

## Research Questions

1. Why must propagation close?
2. Does resonance require closure?
3. Does eigenmode formation require closure?
4. Is circular geometry unavoidable?
5. Is 2π the minimal phase closure?
6. Can π emerge from closure geometry?
7. Is closure encoded directly by D96?
8. Is the zero mode the closure reference state?

---

## Canonical References

- **Ch1/Ch2** Minimal foundation {Difference, η}; π is a boundary constant (QG291);
  Bekenstein 1/4 requires imported 2π (QG185, QG196).
- **Ch3** Actualization: process face; Galton–Watson branching; N=96 attractor; resonance
  = Conservation + Boundary; closure = fixed point of the dynamics (QG282).
- **Ch5** Attractor: content-independent convergence; N=96 stable fixed point; graph C96.
- **Ch6** D96 spectrum: λ_k = 2Σ(1−cos 2πdk/96); ω_k = √λ_k; octave bands; moments.
- **Ch9** State-phase lattice θ_k = 2πk/N; mixing/CP phases continuous (MONO_PHASE001).
- **QG159/QG160** D96 selection: period-3 seed, Z2 half-shift, three-family octave window.
- **ResearchY-A_001/A_002/A_004/A_005** verdicts.

---

## Assumptions

1. Canonical AT V2.0 is ground truth; nothing here modifies it.
2. "Closure" = the fixed point of the actualization dynamics (the boundary of the
   process, QG282), realized as the attractor graph.
3. "Circular geometry" = the circulant-ring structure of the attractor C96.
4. "π emerges" means its *role* as the circle constant of the closed geometry emerges;
   the *numerical value* of π (transcendental) is not computed by the framework.
5. Scope: uniqueness/necessity statements are within the accepted canonical structural
   class (as in Ch5 "Exact status"), not global proofs.
6. No ad-hoc constants, no new primitives.

---

## Research Conclusions

**RQ1 — Why must propagation close?** Propagation (branching) is the count-producing
dynamics of Difference; its identity is count conservation (Ch3). A count-producing
process whose growth is bounded must saturate: the self-reinforcing feedback
(activity → links → activity) is bounded by network capacity, so the topology saturates at
zero residual growth — the fixed point. The Closure Principle (QG282) states that the
boundary of the theory IS this fixed point. Propagation must close because it is a
bounded, self-reinforcing dynamics: an unclosed process would either grow without bound
(no boundary, no spectrum) or fail to converge (no content-independent structure).
Closure is the *convergence* of the process — necessary for any stable medium to exist.

**RQ2 — Does resonance require closure?** YES. Resonance = Conservation + Boundary (Ch3).
The Boundary IS the closure fixed point. The resonance readout (the spectral projection,
A_005) is the eigenbasis of the *closed* graph: the normal modes of the medium. Without
closure there is no fixed graph, no graph Laplacian, no eigenbasis, no resonance
structure. Resonance is defined against the closed medium; closure is its necessary
boundary condition.

**RQ3 — Does eigenmode formation require closure?** YES. The eigenmodes of the attractor
are the normal modes of a *closed* ring: the circulant structure gives the Fourier
eigenbasis (A_005), the Z2 ±k pairing (λ_k = λ_{N−k}), and the octave bands [4,4,87]. An
open graph (with boundaries) would not be circulant: no clean Fourier modes, no Z2
degeneracy, no octave structure. The specific spectral features that carry the physics all
depend on the ring's closed periodic topology.

**RQ4 — Is circular geometry unavoidable?** Within the accepted canonical structural
class: YES. The attractor is the circulant ring C96 (Ch5); the D96 selection program
(QG159/QG160) selects it as the unique rung in the tested class (period-3 seed, Z2
half-shift, three-family octave window). The dynamics converges to a *closed* structure
(content-independent attractor), and the only tested stable fixed points in the class are
rings. Scoped: a global proof excluding every conceivable alternative closure is not
claimed (Ch5 "Exact status").

**RQ5 — Is 2π the minimal phase closure?** YES. The state-phase lattice θ_k = 2πk/N
(Ch9) closes at k = N: θ_N = 2π ≡ 0 (mod 2π). A full cycle of the ring is the rotation by
2π radians; any positive angle strictly less than 2π is not a full cycle. 2π is therefore
the minimal positive phase closure of the discrete circle. This is not an import — it is
the definition of the full cycle applied to the closed ring's phase lattice.

**RQ6 — Can π emerge from closure geometry?** PARTIAL — the role emerges, the value does
not. The closed ring has a circumference (N sites) and a diameter (2·radius = N/π); their
ratio C/D = π is the circle constant of the closed geometry — so π *enters* the theory as
the geometric constant of the emergent circle. But the *numerical value* of π is
transcendental and is not computed by the framework; it remains a boundary constant
(QG291). Consistent with A_001 R7: closure selects the *use* of the circle constant, not
its value. The Bekenstein 1/4 coefficient still requires the imported 2π (QG185/QG196);
this audit does not overturn that boundary.

**RQ7 — Is closure encoded directly by D96?** YES. D96 IS the closed ring: N = 96 sites, the
circumference of the medium. The eigenvalue formula λ_k = 2Σ(1−cos 2πdk/96) is periodic
(λ_{k+N} = λ_k) — the closure is encoded in the ring structure itself. Z2 pairing, octave
bands, span, and moments all follow from the D96 ring alone. The closure's *necessity*
(why N=96) comes from the attractor/selection; the *encoding* of the closed structure is
D96 alone.

**RQ8 — Is the zero mode the closure reference state?** YES. λ₀ = 0 is the uniform
eigenvector — the rest state of the closed ring (A_002 RQ7). Closure produces the ring;
the zero mode is the uniform configuration on the ring against which Difference is
measured. The zero mode is the reference state of the closed medium: its frequency is
zero (no oscillation), it is the "background" of the normal-mode decomposition.

**Success criterion verdict.** Circular closure **emerges** as a consequence of the
canonical framework: the actualization attractor converges to a closed ring, and the
ring's closure is necessary for resonance, eigenmode formation, and the spectral readout
(RQ1–RQ4). **2π emerges** as the minimal phase closure of the discrete circle (RQ5, the
spectral layer's periodicity constant). **π emerges in role** as the circle constant of
the closed geometry (RQ6: C/D = π holds identically for the ring), but its numerical value
remains a boundary (not computed). Closure is encoded directly by D96 (RQ7); the zero mode is
the closure reference state (RQ8).

---

## Governing Identities

| Identity | Value | Status |
|---|---|---|
| closure | attractor = C96, N = 96 | canonical (Ch5) |
| phase cycle | θ_N = 2πN/N = 2π ≡ 0 | canonical (Ch9) |
| circumference | N (ring sites) | canonical |
| radius | r = N/2π | derived geometry |
| circumference/diameter | C/D = N/(2r) = π | identity (role emerges) |
| spectral periodicity | λ_{k+N} = λ_k | canonical (Ch6) |
| zero mode | λ₀ = 0, uniform | canonical (Ch6) |

---

## Compatibility Matrix

| Claim | Circular closure (canonical) | Imported constant? |
|---|---|---|
| propagation must close | YES (bounded self-reinforcing dynamics) | no |
| resonance requires closure | YES (Boundary = closure) | no |
| eigenmodes require closure | YES (circulant → Fourier basis) | no |
| circular geometry unavoidable | YES (within accepted class) | no |
| 2π = minimal phase closure | YES (θ_N = 2π) | no (definition of the full cycle) |
| π role from closure | YES (C/D = π identity) | role emerges; value is a boundary |
| closure encoded by D96 | YES (ring structure) | no |
| zero mode = reference state | YES (uniform rest state) | no |

---

## Contradictions

| # | Risk | Canonical constraint | Resolution |
|---|---|---|---|
| 1 | "π derived" would overturn QG291/QG196 | π is a boundary; Bekenstein 1/4 needs imported 2π (Ch2, QG185/196) | π's *role* emerges (C/D = π identity); its *value* remains a boundary — no overturn |
| 2 | "closure is global" | N=96 necessity is scoped to the tested structural class (Ch5) | necessity within the accepted class; no global proof claimed |
| 3 | "eigenmodes need a closed ring" implies a spatial arena | spacetime is emergent; the ring is the counting medium (Ch2/Ch10) | the ring is the attractor graph of the count, not a pre-existing spatial arena |
| 4 | 2π as "derived" in the Bekenstein sense | QG196 impossibility proof | the 2π here is the phase-lattice full cycle; the Bekenstein 2π remains imported |

---

## Dependency Chain (minimal)

```
Difference
  → Actualization (bounded count-producing dynamics)
  → attractor (closure fixed point N = 96, content-independent)
  → circular graph C96 (closed ring, circumference N)
  → graph Laplacian L (periodic: λ_{k+N} = λ_k)
  → eigenbasis (Fourier modes of the closed ring; Z2 ±k pairs; octave bands)
  → phase lattice θ_k = 2πk/N (2π = minimal full-cycle phase closure)
  → circle constant π in role (C/D = π; value remains a boundary)
  → zero mode λ₀ = 0 (uniform closure reference state)
```

---

## Open Problems

1. **π value (B_001 OP1).** Can any canonical construction compute the numerical value of
   π (rather than using it as a boundary constant)? The circle constant's role is
   selected by closure; its value is transcendental — this remains the QG291/QG196
   boundary.
2. **Closure beyond the ring.** Are there closed structures other than rings within the
   dynamics that were not in the tested class (Ch5)? A global closure proof is open.
3. **Phase-lattice vs Bekenstein 2π.** Is the θ_N = 2π full cycle related to the 2π in
   T = κ/(2π) beyond vocabulary? (A_001 OP2; QG196 unchanged.)
4. **Radius status.** The radius r = N/2π is a derived quantity of the ring geometry; its
   physical role (vs the ladder radii 6.0–17.333) is open (A_001 OP3).

---

## Next Steps

- **ResearchY-B_002 (Origin of π):** the value vs role distinction (RQ6) is the core
  question; B_002 tests whether any canonical construction can produce the value.
- **ResearchY-B_003 (Origin of 2π):** the phase-lattice full cycle (RQ5) vs the Bekenstein
  2π — separate the two (OP3).
- **ResearchY-C_001 (Center Audit):** the ring has no center (translation-invariant); the
  radius r = N/2π is the first radial quantity of the closed geometry.
- **ResearchY-D_001 (D96 Resonance Audit):** the closure requirement for resonance
  (RQ2/RQ3) feeds directly into the resonance audit.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/B_CircularGeometry/Y_B_001_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_B_001_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_B_001_ClosureNecessity` | bounded self-reinforcing dynamics → closure fixed point (N=96) | ✅ |
| `Y_B_001_PhaseCycle` | θ_N = 2π ≡ 0 (minimal full-cycle phase closure) | ✅ |
| `Y_B_001_ResonanceClosure` | resonance = Conservation + Boundary; eigenbasis needs the closed ring | ✅ |
| `Y_B_001_CircularGeometry` | circumference N; radius N/2π; spectral periodicity λ_{k+N}=λ_k | ✅ |
| `Y_B_001_PiCandidate` | C/D = π identity holds; π role emerges, value is a boundary | ✅ |
| `Y_B_001_TwoPiCandidate` | 2π = minimal positive full-cycle angle; phase lattice closes | ✅ |
| `Y_B_001_Run` | Research report | ✅ |

**Conclusion:** circular closure emerges (attractor → ring); 2π emerges as the minimal
phase closure; π emerges in role (circle constant) but its value remains a boundary. No
canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_B_001"`

---

## References

- Monograph V2.0: Ch1/Ch2 (π boundary), Ch3 (closure, resonance, attractor), Ch5
  (attractor uniqueness), Ch6 (D96), Ch9 (phase lattice).
- AT-QG: QG159/QG160 (D96 selection), QG185/QG196 (Bekenstein boundary),
  QG282 (Closure Principle), QG291 (π boundary).
- ResearchY-A_001 (R5/R7/R8), A_002 (RQ7), A_004, A_005.
