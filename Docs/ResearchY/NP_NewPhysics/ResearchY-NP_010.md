# ResearchY-NP_010 — Second Network Layer Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_010 (permanent)
**Title:** Second Network Layer Audit
**Status:** COMPLETE
**Date:** 2026-08-30
**File:** `NP_NewPhysics/ResearchY-NP_010.md`
**Depends on:** ResearchY-NP_003–NP_009 (the phase lever, coupling, locking origin,
coupling network, extremum, variational program)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_010_Tests.cs`

---

## Purpose

**Does a second coupling network exist above Actualization?** NP_007 established the
primary coupling network (interference links, κ = 2√(ρ_Aρ_B)). NP_008/NP_009 showed the
synchronization dynamics would be a gradient flow on the interference functional — a
dynamical layer the canonical chain lacks. This audit asks whether synchronization
arises from a HIGHER interaction layer (Network 2) rather than the primary actualization
chain (Network 1).

---

## 1. Definitions

| Term | Definition |
|---|---|
| **primary network** | Network 1 — the actualization structure generating local evolution (self-rate only) |
| **secondary network** | Network 2 — a higher coupling layer carrying phase flow (cross-phase dynamics) |
| **coupling layer** | a level of the network that mediates an influence (interference, phase flow) |
| **collective dynamics** | synchronized, coherent behavior of multiple states |

---

## 2. Actualization network alone vs + coupling layer

| | Network 1 (Actualization) | Network 1 + Network 2 (coupling layer) |
|---|---|---|
| update | θ(t+1) = θ(t) + Δθ (self-rate, D_041) | θ(t+1) = θ(t) + Δθ + η·∂I/∂θ |
| phase flow | none | gradient flow on I |
| synchronization | ABSENT (unequal modes drift, NP_005) | EMERGES (rel → 0, the max of I) |
| collective modes | transient | stable attractors |

**The primary network alone cannot synchronize. A second layer carrying phase flow is
required.**

---

## 3. Search for hidden network structure

| Structure | Layer | Carries phase flow? |
|---|---|---|
| **reciprocity** (D_037) | the symmetric read basis | NO — a structural map, not dynamics |
| **interference** (cross-term) | the static link network (NP_007) | NO — links exist, no flow |
| **information flow** (M_005) | count redistribution | NO — magnitudes, not phases |
| **shared actualization events** (M_002) | one-time joint pinning | NO — pins once, drift resumes |

**No canonical mechanism carries PHASE flow.** The secondary network's link weights
exist (κ = 2√(ρ_Aρ_B)), but the flow dynamics does not — the second layer is
STRUCTURALLY present, DYNAMICALLY absent.

---

## 4. Is κ a state, link, or field/network property?

| Option | Verdict |
|---|---|
| A) state property | **NO** — κ = 2√(ρ_Aρ_B) depends on BOTH endpoints; it is not a single-state attribute (κ(ρ_A) = 2√ρ_A ≠ κ(ρ_A,ρ_B) in general) |
| B) link property | **YES** — κ is the interference cross-amplitude of the PAIR (A,B): symmetric (κ(A,B) = κ(B,A)), two-state dependent |
| C) field/network property | PARTIAL — it is a network LINK WEIGHT, but not a propagating field value (NP_007) |

**κ is a LINK property (B)** — it belongs to the connection between two states, not to
either state alone, and not to a propagating field.

---

## 5. Does synchronization emerge only with a second network level?

**YES.** The primary network (self-rate dynamics) leaves unequal modes drifting
(NP_005). Synchronization requires the phase-flow layer — the gradient term
η·∂I/∂θ (NP_009). This is a SECOND network level: Network 1 carries magnitudes and
self-rates (local actualization); Network 2 would carry relative phases and their
coupling (the cross-phase gradient). Without Network 2, no synchronization.

---

## 6. Trace

```
Difference
 → Actualization
 → Network 1 (local evolution, self-rate, counts) — DERIVED

Difference
 → Reciprocity (D_037)
 → Interference links (κ = 2√(ρ_Aρ_B)) — DERIVED
 → Network 2 (phase-flow / gradient layer) — STRUCTURALLY present,
   DYNAMICALLY absent (BOUNDARY in canonical AT)
```

---

## Theorem

> **Theorem (NP_010).** Synchronization requires a SECOND network layer above the
> primary actualization chain, and that layer is structurally present but dynamically
> absent in canonical AT. Proof: (1) The PRIMARY network (Network 1) is the local
> actualization update θ(t+1) = θ(t) + Δθ (D_041) — it carries each state's self-rate
> and count, but no phase flow (NP_005). (2) The interference structure provides LINK
> WEIGHTS κ = 2√(ρ_Aρ_B) (NP_007) — a link property depending on both endpoints
> (verified: κ(0.25,0.75) = 0.866; κ(ρ_A) alone = 2√ρ_A ≠ κ(ρ_A,ρ_B)). (3) But the
> weights are static: no canonical mechanism moves phase along the links (reciprocity
> is a read basis, D_037; information flow redistributes counts, M_005; shared events
> pin once, M_002). (4) Synchronization is impossible on Network 1 alone (unequal
> modes drift; NP_005 threshold unmet without the coupling term). (5) A SECOND layer —
> the phase-flow/gradient network η·∂I/∂θ (NP_008/NP_009) — is the minimal structure
> that synchronizes: with it, rel → 0 (max I), κ = 0.866 ≥ 0.5236 threshold, and
> collective modes become stable. Therefore synchronization emerges ONLY when the
> second network level (the gradient layer) is introduced; in canonical AT that layer
> is structurally present (its link weights are derived) but dynamically absent (no
> phase flow), hence BOUNDARY. Classification: Network 1 (actualization) DERIVED; the
> interference link weights DERIVED (Born cross-amplitude); κ as a link property
> DERIVED; Network 2 (phase-flow layer) EMERGENT under a variational requirement,
> BOUNDARY in canonical AT. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Characterize Network 1 (Sections 2–3, verified: self-rate only).
> (2) Classify κ as a link property (Section 4, verified: two-endpoint, symmetric).
> (3) Show synchronization requires the second layer (Section 5, verified: gradient
> flow locks rel=0; κ ≥ threshold). (4) Exhibit the trace (Section 6). ∎

---

## Dependency Graph

```
Difference
 → Actualization
 → Network 1 (local self-rate, counts) — DERIVED
 → Interference link weights (κ = 2√(ρ_Aρ_B)) — DERIVED (link property)
 → Network 2 (phase-flow / gradient layer)
    → structurally present (weights derived)
    → dynamically absent in canonical AT (BOUNDARY)
 → Synchronization
    → absent on Network 1 alone
    → emerges with Network 2 (gradient flow → rel = 0)
```

---

## 7. Necessity Proof

The second layer is NECESSARY for synchronization: Network 1's update contains no
cross-phase term (NP_005: no fixed point for unequal modes; the locking coefficient
κ is required). Since κ is a link property of the interference structure, and the
phase flow that would use it is not in the canonical update, the SECOND NETWORK LAYER
is the necessary missing structure. The weights exist; the layer does not.

---

## 8. Counterexamples

| Attempt | Why it fails |
|---|---|
| "Network 1 synchronizes" | unequal modes drift on the self-rate update (NP_005) |
| "κ is a state property" | κ depends on both endpoints (κ(0.25,0.75)=0.866; κ(ρ_A) alone ≠ it) |
| "Reciprocity is the second network" | reciprocity is the symmetric read basis (D_037), a structural map — no phase flow |
| "Information flow is the second network" | it redistributes counts (M_005), not phases |
| "Shared events synchronize" | they pin once; drift resumes (M_002/NP_004) |

---

## 9. Falsification Path

1. **Network-1-unsynchronized claim** — falsified if two unequal modes synchronize on
   the pure self-rate update with no coupling layer.
2. **κ-link-property claim** — falsified if the coupling coefficient is found to depend
   on a single state (or to be a propagating field value) rather than the pair.
3. **Second-layer necessity** — falsified if synchronization is observed with only the
   static interference weights and no phase-flow dynamics.

---

## 10. Observable consequences (if Network 2 were active)

| Consequence | Signature |
|---|---|
| **synchronized trajectories** | equalized phase advance across modes |
| **coherence** | sustained interference fringes |
| **collective modes** | in-phase/anti-phase attractors |
| **resonance amplification** | 2√(ρ_Aρ_B) maximal at the locked phase |

---

## Classification

| Component | Status |
|---|---|
| Network 1 (actualization, self-rate) | **DERIVED** (fixed Δθ, D_041) |
| interference link weights κ = 2√(ρ_Aρ_B) | **DERIVED** (Born cross-amplitude) |
| κ as a link property | **DERIVED** (two-endpoint, symmetric) |
| Network 2 (phase-flow layer) | **EMERGENT** (under variational requirement) / **BOUNDARY** in canonical AT |

**Synchronization requires a second network layer above Actualization. Its link
weights are derived; its phase-flow dynamics is absent from canonical AT. No new
primitive; canonical AT unchanged.**

---

## Open Problems

1. **Activating Network 2 (NP_010 OP1).** Whether the phase-flow (gradient) layer
   should be adopted — the step that would give the theory collective dynamics
   (extends NP_006/NP_008/NP_009 OP1).

---

## Next Steps

- **Registry note:** synchronization sits on a second network layer — structurally
  present (derived link weights) but dynamically absent in canonical AT.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_010_Tests.cs`
**Run:** 2026-08-30 · **Result:** see `Tests/Results/Y_NP_010_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_010_PrimaryNetwork` | Network 1 = local self-rate (no phase flow) | ✅ |
| `Y_NP_010_SecondaryNetwork` | Network 2 structurally present, dynamically absent | ✅ |
| `Y_NP_010_LinkProperty` | κ depends on both endpoints (link, not state) | ✅ |
| `Y_NP_010_PhaseCoupling` | no canonical mechanism carries phase flow | ✅ |
| `Y_NP_010_SynchronizationLayer` | sync requires the second layer (gradient flow) | ✅ |
| `Y_NP_010_Run` | research report | ✅ |

**Conclusion:** Synchronization requires a SECOND network layer above Actualization.
The interference link weights (κ = 2√(ρ_Aρ_B)) exist and are link properties, but the
phase-flow dynamics is absent from canonical AT (BOUNDARY). No new primitive; canonical
AT unchanged.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_010"`

---

## References

- ResearchY-NP_003 (phase lever), NP_004 (coupling), NP_005 (missing sync mechanism),
  NP_006 (locking origin), NP_007 (coupling network), NP_008 (extremum), NP_009
  (variational actualization), M_002 (shared-event pinning), M_005 (information flow),
  D_037 (reciprocity), D_041 (tick rate).
- AT-QG: QG216 (Born rule).
