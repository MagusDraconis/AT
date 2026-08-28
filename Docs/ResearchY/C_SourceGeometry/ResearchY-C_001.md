# ResearchY-C_001 — Center Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** C — Source Geometry
**ID:** ResearchY-C_001 (permanent)
**Title:** Center Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `C_SourceGeometry/ResearchY-C_001.md`
**Depends on:** ResearchY-A_001…A_005, B_001, B_002
**Test suite:** `AT.Tests/ResearchY/C_SourceGeometry/Y_C_001_Tests.cs`

---

## Purpose

Determine whether a **unique center or source** is implicitly present in the
Difference → Actualization framework, and whether it is **derived, emergent, or absent**.

## Accepted (established results)

- Difference ≈ localized mode excitation; zero mode = undisturbed background (A_002).
- Propagation = branching (local generation) + spectral projection (global readout)
  (A_003 rev.2, A_004).
- Closure is necessary; circular geometry is unavoidable within the accepted class;
  the ring is the medium (B_001).
- The spectrum is algebraic; π's value is a boundary (B_002).
- Spectral projection is derived from the attractor (A_005).

---

## Research Questions

1. Can Difference exist without a center?
2. Does closure imply a center?
3. Is propagation radial?
4. Is the zero mode the source state?
5. Does C96 encode a preferred center?
6. Is center eliminated by symmetry?
7. Is the attractor centerless?
8. Does circular closure require a source?

---

## Canonical References

- **Ch1** Difference: counting difference from a uniform background; Q-event = unit.
- **Ch3** Actualization: Galton–Watson branching (root at generation 0); N=96 attractor;
  closure = fixed point.
- **Ch5** Attractor: content-independent convergence; C96 unique in the accepted class.
- **Ch6** D96 spectrum: circulant ring; Z2 pairing; moments; octave bands.
- **ResearchY-A_002** (zero mode), **A_003 rev.2** (local/global), **B_001** (closure).

---

## Assumptions

1. Canonical AT V2.0 is ground truth; nothing here modifies it.
2. "Center" = a distinguished spatial site of the medium; "source" = an origin of
   propagation (generation root).
3. The medium is the attractor ring C96 (translation-invariant, 12-regular).
4. Radial propagation would mean propagation along a radius toward/from a center; the
   ring has no radial direction.
5. No ad-hoc constants, no new primitives.

---

## Research Conclusions

**RQ1 — Can Difference exist without a center?** YES. Difference is the counting
difference from a uniform background (Ch1) — a definition that requires a *reference*
(the zero mode), not a *center*. A unit of Difference (Q-event) at any site is the same
content as one at any other site; the definition is translation-invariant.

**RQ2 — Does closure imply a center?** NO. The closure is the fixed point N = 96 — an
integer count of ring sites (Ch3/Ch5). The closed structure is a ring, which has no
center. Closure implies *boundedness* (a finite medium), not *centrality*.

**RQ3 — Is propagation radial?** NO. Propagation is branching (tree-local, generation
depth) + spectral projection (global ring readout) — A_003 rev.2. The generation depth is
a *depth in the tree*, not a radius in space. The ring has no radial direction; the only
"radial" quantity is the derived radius r = N/2π (B_001/B_002), which is a geometric
measure of the closed ring, not a propagation coordinate.

**RQ4 — Is the zero mode the source state?** NO. The zero mode (λ₀ = 0, constant
eigenvector) is the **uniform reference state** — the background against which Difference
is measured (A_002 RQ7). A source emits; the zero mode emits nothing (ω₀ = 0, no
oscillation). It is the reference, not the source.

**RQ5 — Does C96 encode a preferred center?** NO. C96 is a circulant ring: the adjacency
is translation-invariant (A[i+1,j+1] = A[i,j]), every site has the same degree (12), and
the zero-mode eigenvector is constant. No site is distinguished; the graph encodes no
preferred center.

**RQ6 — Is center eliminated by symmetry?** YES. The circulant symmetry of C96 (all
rotations are automorphisms) eliminates any distinguished spatial site. Any candidate
"center" is rotated onto any other site by an automorphism — the symmetry makes all
sites equivalent. Center is eliminated by symmetry.

**RQ7 — Is the attractor centerless?** YES. The attractor is the centerless ring: regular
(every site degree 12), translation-invariant, with no boundary and no distinguished
point. The only non-uniform structure is the spectral content (modes, octaves), which
does not single out a site.

**RQ8 — Does circular closure require a source?** NO. Closure is the fixed point of the
bounded dynamics (B_001 RQ1) — a self-consistency of the process, not an emission. The
only source-like object in the framework is the **branching root** (generation 0 of the
Galton–Watson tree): a *generation-space* origin of the count, not a spatial center.

**Success criterion verdict.** The center/source status is **tripartite**:
- **Spatially: ABSENT.** The attractor ring is centerless; circulant symmetry eliminates
  any preferred site (RQ5–RQ7).
- **As a generation source: EMERGENT.** The branching root (generation 0) is the natural
  origin of the count — an emergent source in generation space (RQ8), not a primitive.
- **As a reference: DERIVED (the zero mode).** The zero mode is the uniform reference
  state of the medium (RQ4) — derived from the spectrum, not a center and not a source.

No center is *derived as a spatial object*; the only center-like structure is the
generation root (emergent), and the only reference is the zero mode (derived). **Center is
eliminated by symmetry in space and present only as the branching root in generation
space.**

---

## Compatibility Matrix

| Claim | Verdict | Canonical source |
|---|---|---|
| Difference exists without a center | YES | Ch1 definition (reference, not center) |
| closure implies a center | NO | closure = N=96 integer (ring) |
| propagation is radial | NO | branching (tree depth) + global readout |
| zero mode is the source | NO | reference state, ω₀ = 0 |
| C96 has a preferred center | NO | circulant, translation-invariant |
| center eliminated by symmetry | YES | all rotations are automorphisms |
| attractor is centerless | YES | regular ring, no distinguished site |
| closure requires a source | NO | only the branching root (generation) |

---

## Contradictions

| # | Risk | Resolution |
|---|---|---|
| 1 | "center derived from the ladder radii" | the radii 6.0–17.333 are combinatorial rung indices (A_001 R4), not spatial distances from a center |
| 2 | "the branching root is a spatial center" | the root is a generation-space origin (tree depth), not a spatial site of the ring |
| 3 | "the zero mode is a source" | the zero mode emits nothing (ω₀ = 0); it is the reference state |
| 4 | "circular closure needs a source" | closure is the fixed point of bounded dynamics (self-consistency), not an emission |

---

## Dependency Chain (minimal)

```
Difference
  → Actualization (Galton–Watson branching, root at generation 0)
  → attractor (closure fixed point N = 96)
  → ring C96 (centerless, circulant, translation-invariant)
  → spectrum (zero mode = uniform reference; modes = global content)
  → generation root = the only source (emergent, generation space)
  → zero mode = the only reference (derived)
```

---

## Open Problems

1. **Generation root as source (C_001 OP1).** Is the branching root (generation 0) a
   physical source or a formal origin of the recursion? (The count begins there, but the
   ring has no preferred site to receive it.)
2. **Radius vs center (C_001 OP2).** The ring has a radius r = N/2π but no center — what
   is the geometric meaning of a radius without a center point? (The radius measures the
   ring's size, not a distance to a distinguished point.)
3. **Symmetry breaking (C_001 OP3).** If a center is ever needed (e.g., an observable
   source), what breaks the circulant symmetry? (Currently nothing does; the ring is
   centerless.)

---

## Next Steps

- **ResearchY-C_002 (Radial Propagation):** the radius r = N/2π exists but no radial
  propagation does; test whether any canonical quantity propagates radially (expected:
  no — the ring has no radial direction).
- **ResearchY-D_001 (D96 Resonance Audit):** the centerless spectrum is the resonance
  content; verify the absence of a preferred mode/site.
- **ResearchY-A_003 follow-up:** the generation root is the local source of branching
  (A_003 rev.2 P1).

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/C_SourceGeometry/Y_C_001_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_C_001_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_C_001_CenterNecessity` | Difference needs a reference, not a center | ✅ |
| `Y_C_001_RadialPropagation` | branching is tree-local; no radial propagation on the ring | ✅ |
| `Y_C_001_ZeroModeSource` | zero mode is the reference state (ω₀=0), not a source | ✅ |
| `Y_C_001_SymmetryCenter` | circulant symmetry eliminates any preferred center | ✅ |
| `Y_C_001_ClosureCenter` | closure = N=96 (centerless ring); no source required | ✅ |
| `Y_C_001_Run` | Research report | ✅ |

**Conclusion:** center is ABSENT in space (symmetry), EMERGENT as the branching root
(generation source), and the zero mode is a DERIVED reference (not a source). No canonical
value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_C_001"`

---

## References

- Monograph V2.0: Ch1 (Difference), Ch3 (Actualization, closure), Ch5 (attractor),
  Ch6 (D96 ring).
- ResearchY-A_002 (zero mode), A_003 rev.2 (local/global), B_001 (closure),
  B_002 (radius = N/2π).
