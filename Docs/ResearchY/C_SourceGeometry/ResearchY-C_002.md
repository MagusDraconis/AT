# ResearchY-C_002 — Radial Propagation Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** C — Source Geometry
**ID:** ResearchY-C_002 (permanent)
**Title:** Radial Propagation Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `C_SourceGeometry/ResearchY-C_002.md`
**Depends on:** ResearchY-C_001 (Center Audit)
**Test suite:** `AT.Tests/ResearchY/C_SourceGeometry/Y_C_002_Tests.cs`

---

## Purpose

Determine whether propagation in C96 can be **genuinely radial** — i.e., whether any
canonical propagation law is organized around a geometric origin with a radial
coordinate — or whether radial structure is only a formal, non-derived description.

## Accepted (from C_001)

- C96 has no spatial center.
- Translation symmetry removes preferred sites.
- The zero mode is uniform.
- The generation-0 root exists only in generation space.

---

## Research Questions (required)

1. Define radial propagation rigorously.
2. Test whether any node can act as a geometric origin.
3. Compare shortest-path propagation vs radial shells.
4. Analyze automorphism symmetry.
5. Determine whether observed spreading is: radial, tree-local, resonance/global
   readout, or hybrid.

---

## Canonical References

- **C_001** center absent in space, emergent as branching root, zero mode as reference.
- **A_003 rev.2** propagation = branching (local) + spectral projection (global);
  μ^k = depth; Z2/octaves/locking are spectral.
- **A_004** no single generation model reproduces the spectrum.
- **Ch5/Ch6** C96(±1..±6), circulant ring; graph Laplacian.

---

## Assumptions

1. Canonical AT V2.0 is ground truth; nothing here modifies it.
2. Radial propagation means: propagation organized around a distinguished origin node,
   with the radial coordinate the graph distance from that origin, advancing through
   shells of increasing distance.
3. No new primitives; no fitted constants; the origin must be *derived* (not chosen) for
   propagation to be canonically radial.

---

## 1. Rigorous Definition of Radial Propagation

**Definition (radial propagation).** A propagation law P on a graph G is *radial with
origin o* iff there exists a scalar function f on the non-negative integers such that the
propagated amplitude at node v satisfies

```
P(v) = f(d(o, v))
```

where d(o, v) is the graph distance (shortest path) from o to v, and f is *monotone
advancing*: the shells S_r = {v : d(o,v) = r} are populated in order of increasing r.

A radial law therefore requires:
1. **An origin o** (a distinguished geometric node), and
2. **A radial coordinate** d(o, ·) (the shortest-path distance), and
3. **Shell ordering** (propagation through S_1, S_2, …).

If no origin is derived, no law is *canonically* radial.

---

## 2. Can Any Node Act as a Geometric Origin?

**Formally: yes — any node can be *chosen* as an origin.** The shell structure from any
node is well-defined (BFS layers). **Canonically: no node is derived as an origin.** The
graph C96 is vertex-transitive (automorphism group D96), so every node has the *same*
shell profile:

```
shell sizes from any node:  {0: 1, 1: 12, 2: 12, 3: 12, 4: 12, 5: 12, 6: 12, 7: 12, 8: 11}
```

A radial law would need a *preferred* origin; translation symmetry makes every choice
equivalent. Choosing an origin is exactly the kind of free selection the audit rules
forbid. **No node is a geometric origin canonically.**

---

## 3. Shortest-Path Propagation vs Radial Shells

**They coincide.** On C96, the radial shells S_r = {v : d(o,v) = r} are by definition the
shortest-path (BFS) layers from o. A "shortest-path propagation" and a "radial shell
propagation" are the same structure: both populate the graph by increasing graph
distance from the origin.

The C96 distance structure is therefore:
- **Diameter = 8** (= N/(2K) = 96/12), the maximum eccentricity.
- **Shell sizes nearly uniform**: 12 nodes per shell for r = 1..7, and 11 for r = 8
  (N−1 = 95 is odd).
- **Reflection-symmetric**: d(o,k) = d(o,N−k) (the ring's mirror symmetry).

A radial shell model is a *formal* spreading model (the graph diffusion picture), not a
canonical propagation law. It requires the chosen origin and a shell-ordering rule —
both non-canonical.

---

## 4. Automorphism Symmetry

The automorphism group of C96 is the dihedral group D96 (12 rotations × 2 reflections),
which is **vertex-transitive**: for any two nodes o, o′, there is an automorphism mapping
o → o′. Consequences:

1. **The shell profile is origin-independent** — every node has the identical shell
   structure (verified: node 0 and node 5 give the same profile).
2. **No origin is distinguished** — a radial description centered on any node is
   automorphically equivalent to one centered on any other.
3. **Radial structure is a gauge choice, not a derived structure.** Any radial law
   centered at o is mapped to an identical radial law centered at o′ by an automorphism.
   The "radiality" is not invariant content; it is a coordinate choice.

---

## 5. Classification of Observed Spreading

The canonical propagation of A_003 rev.2 (branching + spectral projection) is analyzed
against the four categories:

| Category | Does canonical propagation use it? | Evidence |
|---|---|---|
| **Radial** | **NO** | No origin is derived (Section 2); canonical propagation has no graph-distance coordinate |
| **Tree-local** | **YES** | Branching ρ_k = μ^k/S is tree-local (generation depth, A_003 rev.2); the Galton–Watson tree, not graph shells |
| **Resonance/global readout** | **YES** | Spectral projection reads all modes globally (modes span the ring, |φ_k(n)|² = 1/96) |
| **Hybrid** | **YES (non-radial)** | The canonical pair (branching + spectral) is a hybrid of tree-local generation + global readout — NOT radial |

**Verdict: the observed spreading is NOT radial.** It is the hybrid
(tree-local branching + global spectral readout) established in A_003 rev.2/A_004.
Radial spreading appears only in the *formal* graph-diffusion picture (heat-kernel shell
spreading), which is a candidate model, not a canonical law (A_004: diffusion fails to
generate the spectrum; it presupposes it).

---

## Theorem-Style Verdict

> **Theorem (C_002).** Propagation in C96 is not canonically radial.
>
> *Proof sketch.* Radial propagation requires a distinguished origin o (Definition,
> Section 1). The automorphism group of C96 is D96, which is vertex-transitive
> (Section 4): no node is distinguished; every node has the identical shell profile
> (verified). Therefore no canonical origin exists, and no canonically radial law is
> possible. The canonical propagation law (branching + spectral projection, A_003
> rev.2) is tree-local in generation space and global in the spectral readout — it
> contains no graph-distance coordinate. Radial shell spreading exists only as a formal
> diffusion model with a freely chosen origin, which is not a derived law. ∎

**Corollary.** Radial structure in C96 is a *coordinate choice*, not invariant content:
any radial description is automorphically equivalent to any other (Section 4), so
"radiality" carries no canonical significance.

---

## Counterexamples

1. **Shell-count counterexample.** If propagation were radial with origin o, the
   amplitude at distance r would be a function of r alone. But the canonical branching
   amplitude ρ_k = μ^k/S is a function of *generation* k, with no graph-distance
   argument — a counterexample to "branching is radial."
2. **Origin-choice counterexample.** A radial law centered at node 0 and one centered at
   node 5 are automorphically identical (Section 4). No measurement or derivation
   distinguishes them; hence neither is canonical.
3. **Global-mode counterexample.** A single ring mode φ_k has amplitude |φ_k(n)|² = 1/96
   on *every* site — flat, not shell-ordered. The spectral readout is explicitly
   non-radial.
4. **Diffusion-formal counterexample.** The heat kernel e^{−tL} spreads through shells,
   but its operator is built from the spectrum (presupposes λ_k, A_004) and its origin
   is chosen — a formal model, not a canonical law.

---

## Pass/Fail Classification

| Claim | Classification |
|---|---|
| Propagation in C96 can be genuinely radial | **FAIL** (no derived origin; canonical law is non-radial) |
| Any node can act as a geometric origin (formally) | PASS (formally; any node has well-defined shells) |
| Any node is a canonical origin | **FAIL** (vertex-transitivity removes all preferred sites) |
| Shortest-path propagation = radial shells | PASS (identical structures on C96) |
| The canonical spreading is tree-local + global (hybrid, non-radial) | PASS (A_003 rev.2) |
| Radial structure is invariant content | **FAIL** (it is a gauge/coordinate choice) |

---

## Research Conclusions

1. **Radial propagation is rigorously defined** as origin-relative shell propagation
   (Section 1), but the definition requires a distinguished origin.
2. **No node is a canonical geometric origin** — C96 is vertex-transitive (D96
   automorphisms), every node has the identical shell profile (Section 2, 4).
3. **Shortest-path propagation and radial shells coincide** on C96 — both are the BFS
   layer structure (diameter 8 = N/(2K), near-uniform shells) — but neither is a
   canonical law.
4. **Automorphism symmetry** makes radial structure a gauge choice: any radial
   description is equivalent to any other; "radiality" is not invariant content.
5. **The observed spreading is the hybrid (tree-local branching + global spectral
   readout)**, established in A_003 rev.2 — NOT radial.

**Final verdict: radial propagation in C96 is NOT canonical.** It exists only as a formal
diffusion model with a freely chosen origin. The canonical propagation is
tree-local + global (hybrid, non-radial). No new primitives; canonical AT unchanged.

---

## Open Problems

1. **Gauge choice vs derived structure (C_002 OP1).** Is there any canonical observable
   that *selects* an origin (breaking the vertex transitivity)? Currently nothing does;
   if one emerged, radial structure would become meaningful.
2. **Shell structure significance (C_002 OP2).** The near-uniform shells (12/12/…/11) are
   a pure graph-distance fact. Does any canonical quantity (moments, occupancies) map to
   the shell sizes? (Candidate observation, not a claim.)
3. **Heat-kernel origin (A_001 OP5).** The formal diffusion model requires an origin;
   would a derived heat-kernel law select one? (Open; the kernel is a candidate.)

---

## Next Steps

- **ResearchY-D_001 (D96 Resonance Audit):** the global (non-radial) readout is the
  resonance content; verify the absence of any radial/centered observable.
- **ResearchY-A_003 follow-up:** the tree-local branching is the non-radial generation
  law; the generation depth (not graph distance) is the canonical "coordinate."

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/C_SourceGeometry/Y_C_002_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_C_002_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_C_002_RadialDefinition` | radial propagation requires a derived origin + shell ordering | ✅ |
| `Y_C_002_OriginTest` | any node has well-defined shells, but none is canonical (vertex-transitive) | ✅ |
| `Y_C_002_ShellStructure` | diameter 8 = N/(2K); near-uniform shells; reflection symmetry | ✅ |
| `Y_C_002_Automorphism` | D96 vertex-transitivity → identical shell profiles; radiality is a gauge choice | ✅ |
| `Y_C_002_SpreadingClass` | canonical spreading = tree-local + global (hybrid, NOT radial) | ✅ |
| `Y_C_002_Run` | Research report | ✅ |

**Conclusion:** propagation in C96 is NOT canonically radial — no derived origin exists
(vertex-transitivity); the canonical law is the hybrid (branching + spectral projection).
No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_C_002"`

---

## References

- ResearchY-C_001 (center audit: center absent in space).
- ResearchY-A_003 rev.2 (branching + spectral projection; μ^k depth), A_004
  (diffusion fails as independent generation).
- Monograph V2.0: Ch5/Ch6 (C96 circulant ring; graph Laplacian).
