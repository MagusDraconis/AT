# ResearchY-A_005 — Spectral Projection Origin

**Program:** ResearchY — Wave Geometry Program
**Group:** A — Wave Foundations
**ID:** ResearchY-A_005 (permanent)
**Title:** Spectral Projection Origin
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `A_WaveFoundations/ResearchY-A_005.md`
**Depends on:** ResearchY-A_003 (rev. 2), ResearchY-A_004
**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_005_Tests.cs`

---

## Purpose

Answer the question: **why does branching project onto spectral modes?** Determine
whether **spectral projection** is a **primitive operation** or a **derived consequence**
of Difference → Actualization → Inevitable Spectrum, and give the **minimal origin** of
spectral projection.

---

## Canonical References

- **Ch1/Ch2** Minimal foundation: primitives are exactly {Difference, η}; Actualization is
  derived (MONO006); nothing else is primitive.
- **Ch3** Actualization: Galton–Watson branching (ρ_k = μ^k/S); N=96 attractor; resonance
  = Conservation + Boundary.
- **Ch5** Attractor → graph Laplacian → eigenspectrum; spectrum is derived, not primitive.
- **Ch6** D96 spectrum: λ_k = 2Σ(1−cos 2πdk/96); ω_k = √λ_k; octave bands; moments.
- **Ch9** |ψ_k|² = ρ_k (QG216); Born rule Σ|ψ|² = 1.
- **ResearchY-A_003 (rev.2), A_004** decomposition: branching (count) + spectral
  projection (mode structure); unique within accepted structure.

---

## Assumptions

1. Canonical AT V2.0 is ground truth; nothing here modifies it.
2. "Spectral projection" = the readout of the count content onto the eigenbasis of the
   attractor graph Laplacian (the modal decomposition used throughout A_001–A_004).
3. The eigenbasis is the unique diagonalizing basis of the graph Laplacian L (up to
   rotations within degenerate eigenspaces).
4. "Primitive" means an irreducible input of the theory (per the minimal-foundation
   result: only Difference and η are primitives).
5. No ad-hoc constants, no new primitives.

---

## Test: is spectral projection primitive or derived?

Four candidate answers:

| Candidate | Claim | Verdict |
|---|---|---|
| A | projection is fundamental (a primitive operation) | **FAILS** — contradicts the minimal foundation |
| B | projection emerges from closure | PARTIAL — closure fixes the graph, not the projection operation |
| C | projection emerges from resonance | CIRCULAR — resonance IS the projection readout |
| D | projection emerges from the actualization attractor | **YES** — the minimal origin |

### Candidate A — Projection is fundamental

- Dependency chain: none — it would be an input.
- Contradictions: the minimal-foundation theorem (Ch1/Ch2) fixes the primitives as
  exactly {Difference, η}. A primitive projection operation would be a third primitive,
  contradicting minimality. Moreover the eigenbasis is a *mathematical output* of the
  graph Laplacian (the diagonalizing basis of L), not an irreducible input.
- Necessity: projection is necessary as a *readout* (no physics without the spectrum,
  Ch5), but necessity of the readout is not primitiveness of the operation.
- Uniqueness: n/a (it would be unique by fiat).
- **Verdict: FAILS.**

### Candidate B — Projection emerges from closure

- Dependency chain: Difference → Actualization → closure (N=96 fixed point) → attractor
  graph C96 → Laplacian L → eigenbasis.
- Contradictions: none (closure is canonical). But closure alone (the fixed-point *size*
  N=96) does not fix the *structure* of the graph — the eigenbasis is fixed by the graph
  geometry (link lengths ±1..±6), not merely by the size. Closure is the *boundary*
  (the fixed point); the graph structure is the *content* of that fixed point.
- Necessity: necessary as the boundary condition.
- Uniqueness: the closure selects N=96; the eigenbasis follows from the resulting graph.
- **Verdict: PARTIAL — necessary but not sufficient; it is a link in the chain, not the
  origin.**

### Candidate C — Projection emerges from resonance

- Dependency chain: Conservation + Boundary → resonance → spectral readout.
- Contradictions: circularity — resonance is *defined* as the spectral readout
  (Ch3: resonance = the operator layer projecting the spectrum onto observable
  structure). "Projection emerges from resonance" is therefore "projection emerges from
  projection."
- Necessity: resonance is the readout's *role*, not its origin.
- Uniqueness: n/a (circular).
- **Verdict: CIRCULAR — explains the role, not the origin.**

### Candidate D — Projection emerges from the actualization attractor

- Dependency chain:
  Difference → Actualization → attractor (converged graph C96) → graph Laplacian L →
  eigenbasis (diagonalizing basis of L) → spectral projection (readout in that basis).
- Contradictions: none. Every link is canonical.
- Necessity: the attractor is the converged fixed point of Actualization (Ch3/Ch5); the
  graph is its content; the Laplacian its operator; the eigenbasis the unique
  diagonalizing basis; the readout is forced (no physics without the spectrum).
- Uniqueness: the eigenbasis is the unique diagonalizing basis of L (up to degenerate-
  subspace rotation); the octave bands, moments, and Z2 pairing are fixed by it. A
  different attractor graph would give a different eigenbasis (tested: K=5 vs K=6 give
  different spectra) — the projection is therefore determined by the attractor, not free.
- **Verdict: YES — the minimal origin.**

---

## Minimal Origin of Spectral Projection

> **Spectral projection is derived, not primitive.** It emerges from the actualization
> attractor via the closure: Actualization converges to the attractor graph C96 (the
> closure fixed point); the graph defines a Laplacian L; L has a unique diagonalizing
> basis (the eigenbasis, the normal modes of the medium); and the count content is read
> through that basis — the spectral projection. The chain is
>
> Difference → Actualization → attractor (closure) → graph → Laplacian → eigenbasis →
> spectral projection.

The two necessary links that make projection *forced* rather than chosen:

1. **The eigenbasis is the unique diagonalizing basis of L** (up to degenerate-subspace
   rotation). Any other basis is non-diagonal; the count readout is not free to pick a
   basis — the diagonal read is the only one in which the medium's dynamics decouples
   into normal modes.
2. **The graph is the content of the attractor.** The eigenbasis is a function of the
   graph; the graph is the converged output of Actualization. So projection inherits the
   inevitability of the attractor (content-independent convergence, Ch5).

The count-to-mode bridge |ψ_k|² = ρ_k (QG216) is the canonical amplitude identity that
assigns the branching shares to the modal amplitudes. Its *existence* is canonical; its
*deeper derivation* (why branching magnitudes become modal amplitudes) remains the open
scalar-to-modal bridge (A_003 OP2).

---

## Dependency Chain (minimal)

```
Difference
  → Actualization (derived process face)
  → attractor / closure (N = 96 fixed point, content-independent)
  → graph C96(±1..±6) (content of the fixed point)
  → graph Laplacian L
  → eigenbasis (unique diagonalizing basis; normal modes φ_k)
  → spectral projection (count readout in the eigenbasis)
  → resonance structure (octaves, moments, Z2, locking)
```

---

## Contradictions Summary

| Candidate | Contradiction | Severity |
|---|---|---|
| A fundamental | violates the minimal foundation {Difference, η} | CRITICAL |
| B closure | insufficient alone (size ≠ structure); necessary but not the origin | MINOR |
| C resonance | circular (resonance IS the projection readout) | MODERATE |
| D attractor | none — all links canonical | NONE |

---

## Necessity and Uniqueness

**Necessity.** Spectral projection is necessary as a readout (no physics without the
spectrum, Ch5) but is NOT a primitive. Its necessity is inherited: the readout is forced
by the closure (the boundary) and by the conservation of the count (the content). The
pair "branching (content) + spectral projection (readout)" is necessary; neither alone is
sufficient (A_004).

**Uniqueness.** Given the attractor graph, the projection is unique: the eigenbasis is
the unique diagonalizing basis of L (up to degenerate-subspace rotation), hence the
normal-mode readout is fixed. The octave bands [4,4,87], the moments, the Z2 pairing, and
the locking gap are all determined by this one basis. A different attractor graph would
give a different eigenbasis (verified: K=5 vs K=6 give different spectra), so the
projection is *determined by the attractor* — its uniqueness is the attractor's
uniqueness (Ch5).

---

## Research Conclusions

1. **Spectral projection is NOT a primitive operation.** It is a mathematical readout of
   the graph Laplacian of the attractor — a derived consequence, consistent with the
   minimal foundation {Difference, η} (Ch1/Ch2).
2. **The minimal origin is the actualization attractor (candidate D).** The chain
   Difference → Actualization → attractor/closure → graph → Laplacian → eigenbasis →
   projection is the minimal derivation: every link is canonical and the final readout is
   forced by the uniqueness of the diagonalizing basis.
3. **Closure (B) is a necessary link, not the origin.** It fixes the size N=96; the
   eigenbasis follows from the graph *structure* of the fixed point.
4. **Resonance (C) is circular as an origin.** Resonance is the readout's role
   (Conservation + Boundary), not its cause.
5. **The projection inherits the inevitability of the attractor.** Content-independent
   convergence (Ch5) makes the graph — and hence the eigenbasis and the projection —
   unique within the accepted structural class.

**Answer to the question.** *Why does branching project onto spectral modes?* Because the
spectral modes are the unique diagonalizing basis of the medium's Laplacian — the medium
(the attractor graph) is the converged output of Actualization, so the projection onto
its normal modes is the forced readout of the branching count. Projection is the *shadow*
of the attractor: it exists because the count must be read against a medium, and the
medium has a unique modal basis.

---

## Open Problems

1. **Scalar-to-modal bridge (A_003 OP2).** Why does the 1-D branching measure become the
   96-D modal amplitudes? The identity |ψ_k|² = ρ_k (QG216) is canonical but its deeper
   derivation is open.
2. **Degenerate-subspace rotation.** The eigenbasis is unique up to rotations within the
   degenerate eigenspaces (the Z2 pairs, the large multiplicities). Is the actual
   readout basis pinned by any canonical structure, or is the degeneracy rotation
   physically inert? (Candidate observation, not a claim.)
3. **Eigenbasis vs. other natural bases.** Are there other canonical bases (e.g., the
   adjacency eigenbasis, the octave-projection basis) that give different natural
   readouts, and is the Laplacian eigenbasis singled out by the dynamics (ẍ = −Lx)?
   (The normal-mode argument picks L; the adjacency basis would be a different choice —
   not currently used.)

---

## Next Steps

- **ResearchY-B_001 (Circular Closure):** the attractor graph is the medium of the
  projection; formalize the closure that fixes N=96.
- **ResearchY-D_001 (D96 Resonance Audit):** verify the resonance readout is the
  eigenbasis readout (the D result) directly against the octave structure.
- **ResearchY-D_002 (Standing Wave Model):** the eigenbasis as normal modes — the
  natural standing-wave content of the medium.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_005_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_A_005_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_A_005_ProjectionNotPrimitive` | primitives = {Difference, η}; projection is the eigen-decomposition of L | ✅ |
| `Y_A_005_ClosureLink` | N=96 is the closure fixed point; the graph fixes λ_k | ✅ |
| `Y_A_005_ResonanceReadout` | resonance = Conservation + Boundary; |ψ_k|² = ρ_k, Σ|ψ|² = 1 | ✅ |
| `Y_A_005_AttractorOrigin` | eigenbasis diagonalizes L; Fourier modes are exact eigenmodes | ✅ |
| `Y_A_005_UniqueBasis` | unique diagonalizing basis; different graph (K=5) → different spectrum | ✅ |
| `Y_A_005_MinimalOrigin` | dependency chain Difference → … → projection (each link canonical) | ✅ |
| `Y_A_005_Run` | Research report | ✅ |

**Conclusion:** spectral projection is DERIVED, not primitive — the minimal origin is the
actualization attractor (candidate D), via closure → graph → Laplacian → eigenbasis.
No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_005"`

---

## References

- Monograph V2.0: Ch1/Ch2 (minimal foundation), Ch3 (actualization, attractor, closure),
  Ch5 (spectrum from attractor), Ch6 (D96), Ch9 (|ψ_k|² = ρ_k).
- ResearchY-A_003 (rev.2), A_004 (falsification verdict).
- AT-QG: QG216 (amplitude origin), MONO006 (actualization derived), MONO_PHASE002 (μ^k).
