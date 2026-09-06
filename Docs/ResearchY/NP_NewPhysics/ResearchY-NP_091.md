# ResearchY-NP_091 — Network Geometry → Spacetime Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_091 (permanent)
**Title:** Network Geometry → Spacetime Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_091.md`
**Depends on:** ResearchY-NP_069 (expansion = branching growth), NP_080 (Difference duality),
NP_084 (η framework), NP_088 (D96 network geometry), NP_089 (rotational symmetry), NP_090
(network ontology), AT-QG QG001/270 (Difference), QG026 (PPN γ), QG043/044 (ψ spin-2), QG197
(2D→3D bridge), QG222 (native metric dynamics), QG89 (causal order), QG290 (framework inventory)
**Test suite:** `AT.Tests/ResearchY/NP_091_Tests.cs`

---

## Purpose

NP_088/090 established the D96 network is the difference structure (nodes = distinctions, links =
adjacency) that already yields emergent 3D geometry. NP_091 asks the closing question: **how does
network geometry become the observed spacetime?** Program: (1) inventory every appearance of
geometry / distance / metric / causal order / spacetime; (2) determine node / link / path; (3)
derive distance from connectivity, metric from network structure, curvature from ρ/ψ; (4) test
whether space and time are both emergent or only space; (5) determine the relation between the
branching count ρ and the metric g = ρ^(2/d)η; (6) identify light cones, causal structure,
propagation limits; (7) compare network geometry with observed spacetime. **Success criterion:**
provide the complete mapping Difference → Network → Geometry → Spacetime, and identify the first
non-derived step. No new primitives; canonical AT unchanged.

---

## 1. Inventory — geometry, distance, metric, causal order, spacetime

| Object | In AT | Source |
|---|---|---|
| **node** | a distinction (an actualization event) | NP_090/QG270 |
| **link** | the adjacency (which distinctions are neighbours) | NP_080 |
| **path** | a chain of adjacent links (a sequence of distinctions) | NP_090 |
| **distance** | the shortest path length (number of links between two distinctions) | graph distance |
| **metric** | g = ρ^(2/d)η — the trace face of the connectivity | NP_080/QG197 |
| **curvature** | ψ — the traceless (Weyl) face | NP_080/QG285 |
| **causal order** | the scale-invariant partial order (time) | QG89 |
| **spacetime** | 3 spatial dimensions (emergent) + 1 time (the tick) | QG197/QG290 |

---

## 2. Node, link, path — and distance from connectivity

```
node  = a distinction (a Q-event, a before→after difference)
link  = the adjacency (a second-order difference: "these two are neighbours")
path  = a chain of links (successive neighbours)
distance(n₁, n₂)  =  the fewest links connecting n₁ to n₂   [the graph geodesic]
```

**Distance is derived from connectivity** — it is the shortest-link count. The continuum metric
g = ρ^(2/d)η is the infinitesimal limit of this graph distance: the conformal factor ρ^(2/d) says
how densely the distinctions are packed, so a small coordinate step covers many or few nodes
depending on ρ.

---

## 3. Metric from network structure, curvature from ρ / ψ

```
connectivity A_ij  =  (1/d)Tr(A)·δ_ij  +  traceless part
                        └── ρ (the trace) ──┘   └── ψ (the traceless) ──┘
                              ↓                        ↓
                     g = ρ^(2/d)·η                  Weyl curvature
                    (the conformal metric)       (the non-conformal part)
```

**The metric is the trace face** of the connectivity (g = ρ^(2/d)η, where ρ is the count density);
**the curvature is the traceless face** (ψ = the Weyl content, the deviation from conformal
flatness). Geometry is therefore not imposed on the network — it is read off the network's own
connectivity.

---

## 4. Space and time — both emergent, or only space?

| | Status | Source |
|---|---|---|
| **space (3D)** | **EMERGENT.** The tensor product D96 ⊗ D96 ⊗ D96 raises the DOS to p = 3, and d ≥ 3 is DERIVED (the (d−2) factor: gravity is trivial in 2D, non-trivial in d ≥ 3, QG197). | DERIVED/EMERGENT |
| **time** | **NOT emergent from the network.** Time is the ACTUALIZATION TICK — the discrete causal order (QG89: "network time = causal order"). The metric hosts it as the +1 Lorentzian signature, a FRAMEWORK residue (QG290). | FRAMEWORK |

**Only space is emergent; time is the tick.** The network gives the three spatial dimensions; the
one time dimension is the actualization order itself, not a fourth network axis.

---

## 5. The branching count ρ and the metric g = ρ^(2/d)η

```
ρ (the count density = the trace)  →  g = ρ^(2/d)η  (the metric, built from ρ)
ρ's growth (branching, ∂_t ρ = ln(μ)·ρ)  →  expansion (the FRW scale factor a = ρ^(1/d), hosted)
```

ρ is both the *substance* of the metric (the conformal factor) and the *dynamics* of spacetime
(the branching growth that reads as expansion, NP_069). The count density is the single object
that does double duty: it is geometry and it is time-evolution.

---

## 6. Light cones, causal structure, propagation limits

| Item | In AT |
|---|---|
| **light cones** | the null geodesics of the conformal metric; conformal rescaling leaves them invariant, so the light cones are those of the flat reference η, scaled uniformly by ρ^(2/d) |
| **causal structure** | the scale-invariant partial order (the tick order) — no closed timelike loops, by the acyclicity of the order |
| **propagation limit** | the actualization tick — nothing changes faster than one tick; the speed of light c is the unit convention (imported) that converts tick-steps into a velocity |

The causal structure is the order of actualization; the light cones are its conformal geometry;
the propagation limit is the tick.

---

## 7. Network geometry vs observed spacetime

| Network geometry | Observed spacetime |
|---|---|
| 3D cubic lattice (D96⊗D96⊗D96), O_h symmetry | 3D Euclidean space, O(3) symmetry |
| O(3) only approximate (cubic corrections O((ka)²), NP_089) | O(3) exact |
| + 1 time = the tick (framework) | + 1 time (Lorentzian signature) |

The network reproduces **3 spatial dimensions** and the **causal order**, but its spatial symmetry
is only *approximately* O(3) at large scale (NP_089). The time signature is a framework input, not
derived.

---

## Theorem

> **Theorem (NP_091).** The D96 network becomes spacetime in three steps. (1) NETWORK: the
> difference structure — nodes = distinctions, links = adjacency (NP_090). (2) GEOMETRY: the trace
> face of the connectivity gives the metric g = ρ^(2/d)η, and the traceless face gives the curvature
> ψ (NP_080); distance is the shortest-link count, of which the metric is the continuum limit.
> (3) SPACETIME: the tensor product D96 ⊗ D96 ⊗ D96 raises the density of states to p = 3, and d = 3
> is DERIVED (the (d−2) bridge, QG197) — so the three SPATIAL dimensions are EMERGENT; the one TIME
> dimension is the actualization tick (the discrete causal order, QG89), hosted by the metric as the
> +1 Lorentzian signature, a FRAMEWORK residue (QG290). The branching count ρ does double duty: it is
> the conformal factor of the metric (g = ρ^(2/d)η) AND its growth reads as cosmic expansion
> (a = ρ^(1/d), NP_069). The causal structure is the partial order; the light cones are the null
> geodesics (conformally invariant); the propagation limit is the tick. Proof: (1) Inventory
> (Section 1). (2) Distance (Section 2, verified): the shortest-link count. (3) Metric/curvature
> (Section 3, verified): trace → g, traceless → ψ. (4) Space vs time (Section 4, verified): space
> emergent, time the tick. (5) ρ and g (Section 5, verified): ρ is geometry + dynamics. (6) Causal
> structure (Section 6). (7) Comparison (Section 7). **Success criterion: the complete mapping is
> Difference → Network → Geometry → Spacetime, and the FIRST NON-DERIVED STEP is the founding pair
> {Difference, η} plus the time tick — space is emergent, time is the framework tick.** Classification:
> the 3D spatial geometry EMERGENT (D96⊗D96⊗D96, d=3 derived); the metric g = ρ^(2/d)η DERIVED (from
> the trace ρ); time (the tick + Lorentzian signature) FRAMEWORK/BOUNDARY (QG89/QG290); Difference and
> η BOUNDARY/FRAMEWORK. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Derive distance. (3) Derive metric/curvature. (4) Test
> space-vs-time. (5) Relate ρ and g. (6) Identify causal structure. (7) Compare to observation. ∎

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "time is emergent from the network" | time is the actualization tick (the causal order), not a fourth network axis; the +1 signature is a framework residue |
| "space is a framework input" | d = 3 is derived (the (d−2) bridge), and D96⊗D96⊗D96 realizes it |
| "the metric is imposed, not derived" | g = ρ^(2/d)η is the trace face of the connectivity (NP_080) |
| "distance is primitive" | distance is the shortest-link count of the adjacency |
| "O(3) is exact in the network" | it is only approximate (cubic corrections, NP_089) |

---

## 9. Falsification paths

| Claim | Falsification |
|---|---|
| space is emergent (d=3 derived) | a non-trivial gravity in d=2 (the (d−2) factor non-vanishing) |
| time is the tick (not emergent) | a time dimension derived from the network (a fourth network axis) |
| the metric is the trace of the connectivity | a metric not expressible as ρ^(2/d)η from the count density |
| the causal order is the tick | a closed timelike loop in the network's partial order |

---

## 10. Classification

| Component | Status |
|---|---|
| the 3D spatial geometry (D96 ⊗ D96 ⊗ D96, d = 3) | **EMERGENT** (d=3 DERIVED via the (d−2) bridge) |
| the metric g = ρ^(2/d)η (the trace face) | **DERIVED** |
| the curvature ψ (the traceless/Weyl face) | **DERIVED** |
| time (the tick + Lorentzian signature) | **FRAMEWORK/BOUNDARY** (QG89/QG290) |
| Difference and η (the founding pair) | **BOUNDARY/FRAMEWORK** |

**Conclusion.** The D96 network becomes spacetime by a three-step chain: the difference structure
(nodes = distinctions, links = adjacency) → the geometry (metric g = ρ^(2/d)η as the trace face,
curvature ψ as the traceless face) → the spacetime (three spatial dimensions emergent from
D96 ⊗ D96 ⊗ D96 with d = 3 derived, plus one time dimension that is the actualization tick, hosted
as the +1 Lorentzian signature). **Only space is emergent; time is the framework tick.** The first
non-derived step is the founding pair {Difference, η} plus the tick. The branching count ρ is the
single object that does double duty — it is the conformal factor of the metric and, through its
growth, the expansion of the universe. No new primitive; canonical AT unchanged.

---

## 11. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_091_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_091_NodeLinkPath` | node = distinction, link = adjacency, path = chain | ✅ |
| `Y_NP_091_DistanceFromConnectivity` | distance = shortest-link count | ✅ |
| `Y_NP_091_MetricAndCurvature` | g = ρ^(2/d)η (trace); ψ = Weyl (traceless) | ✅ |
| `Y_NP_091_SpaceEmergentTimeTick` | space emergent (d=3); time = tick (framework) | ✅ |
| `Y_NP_091_RhoAndMetric` | ρ = conformal factor + expansion (a = ρ^(1/d)) | ✅ |
| `Y_NP_091_CausalStructure` | light cones = null geodesics; order = partial order; limit = tick | ✅ |
| `Y_NP_091_FirstNonDerivedStep` | {Difference, η} + the tick | ✅ |
| `Y_NP_091_Classification` | space EMERGENT; metric DERIVED; time FRAMEWORK | ✅ |
| `Y_NP_091_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_091"`

---

## References

- ResearchY-NP_069 (expansion), NP_080 (Difference duality), NP_084 (η framework), NP_088 (network
  geometry), NP_089 (rotational symmetry), NP_090 (network ontology).
- AT-QG: QG001/270 (Difference), QG026 (PPN γ), QG043/044 (ψ spin-2), QG197 (2D→3D bridge), QG222
  (native metric dynamics), QG89 (causal order), QG290 (framework inventory).
