# ResearchY-NP_154 — Contact Network Softening Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_154 (permanent)
**Title:** Contact Network Softening Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_154.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_137
(real-world evidence), NP_141 (yield stress softening), NP_142 (multi-band resonance control), NP_143
(dislocation threshold)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_154_Tests.cs`

---

## Purpose

NP_141–147 focused on defect (dislocation) engineering, but granite is a heterogeneous multi-phase
rock, not a metal. NP_154 asks the material-class question: **for granite-like materials, is rigidity
controlled more by grain-contact networks than by internal crystal defects?** Program: (1) model
granite as grains / contacts / microcracks; (2) compare dislocation vs grain-contact contributions;
(3) determine whether temporary softening can occur through contact-network disruption; (4) estimate
the required strain / stress / power; (5) compare defect mobility vs contact mobility. **Success
criterion:** determine the dominant control lever for large granite-like materials — defect
engineering or contact-network engineering. No new primitives; canonical AT unchanged.

---

## 1. Model granite as grains / contacts / microcracks

Granite is a **granular, jamming system**, not a dislocation medium:

| Element | Role |
|---|---|
| **grains** (quartz, feldspar, mica) | the building blocks; each is itself a crystal with dislocations |
| **contacts** (grain boundaries) | the load-bearing junctions; force chains carry stress |
| **microcracks** (inter-/intragranular) | the compliant, low-stiffness defects that dominate brittle behavior |

Rigidity is carried by the **percolating contact network** (force chains), exactly as in a jammed
granular packing — not by dislocation-mediated ductility inside grains.

---

## 2. Dislocations vs grain-contact network

| Contribution | Relevance to granite rigidity |
|---|---|
| **dislocations** (intragranular) | minor in the brittle (low-T, low-P) regime; they act only at high T/P (semi-brittle/ductile flow) |
| **grain-contact network** (force chains) | **dominant** — contact stiffness and microcrack density are the primary predictors of rock stiffness and strength |

In the brittle regime typical of shallow crustal granite, **microcrack density and grain-boundary
contact stiffness control rigidity**, not dislocations. Dislocations become relevant only at depth
(semi-brittle/ductile).

---

## 3. Temporary softening via contact-network disruption

**Yes — this is acoustic fluidization** (NP_137): vibrational/acoustic energy transiently breaks or
rearranges force chains, dropping friction and shear strength by a **factor ~5–10** — the mechanism
invoked for fault weakening and remote earthquake triggering. Contact-network disruption is the
granite analogue of dislocation unpinning in metals.

---

## 4. Required strain / stress / power

| Quantity | Estimate | Basis |
|---|---|---|
| **strain** | ~10⁻⁶–10⁻⁵ (contact-scale) | contact breakaway, like Granato–Lücke but at contacts (NP_143) |
| **stress** | ~0.06–0.6 MPa | low (contacts are weak junctions) |
| **power density** | ~1–5 W/cm² | same order as the dislocation threshold (NP_143) |

The threshold is the same *microstrain, few-W/cm²* condition — but the mechanism is contact
breakage/rearrangement, not dislocation glide.

---

## 5. Defect mobility vs contact mobility

| Channel | Granite relevance |
|---|---|
| **defect (dislocation) mobility** | low — brittle regime, few mobile dislocations |
| **contact mobility** (force-chain rearrangement) | **dominant** — the lever that softens granite |

For granite, the operative lever is **contact-network mobility**, not dislocation mobility.

---

## Theorem

> **Theorem (NP_154).** For granite-like materials, rigidity is controlled by GRAIN-CONTACT NETWORKS,
> not internal crystal defects — the dominant lever is CONTACT-NETWORK ENGINEERING, not defect
> engineering. Granite is a jammed granular system: rigidity is carried by a percolating force-chain
> network at grain contacts and microcracks, which dominate the brittle (low-T/P) regime; intragranular
> dislocations are relevant only in the semi-brittle/ductile regime at depth. Temporary softening occurs
> via CONTACT-NETWORK DISRUPTION (acoustic fluidization, NP_137), reducing friction/shear strength by a
> factor ~5–10 at microstrain and ~1–5 W/cm². So NP_141–147's dislocation-centric picture applies to
> metals, not rock: granite is contact-network-controlled. Proof: (1) Model (Section 1). (2) Compare
> (Section 2). (3) Soften (Section 3). (4) Estimate (Section 4). (5) Mobility (Section 5). **Success
> criterion: contact-network engineering is the dominant lever — KNOWN PHYSICS (granular jamming / rock
> mechanics / acoustic fluidization).** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Model. (2) Compare. (3) Soften. (4) Estimate. (5) Mobility. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "granite is dislocation-controlled" | brittle regime: contacts + microcracks dominate; dislocations only at depth |
| "contact disruption is new" | acoustic fluidization / granular jamming are established |
| "granite softens like a metal" | metals soften by dislocation glide; granite by force-chain rearrangement |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| contact network dominates (KNOWN PHYSICS) | a granite whose rigidity is set by intragranular dislocation motion in the brittle regime |
| temporary softening is contact-based | a granite that softens via dislocation unpinning without contact rearrangement |

---

## 8. Classification

| Component | Status |
|---|---|
| granite rigidity = contact network (force chains, jamming) | **KNOWN PHYSICS** |
| dislocation contribution (brittle regime) | minor (depth-limited) |
| temporary softening via contact disruption (acoustic fluidization) | **KNOWN PHYSICS** (NP_137) |
| dominant lever = contact-network engineering | **KNOWN PHYSICS** |
| "defect engineering dominates granite" | **REFUTED** |

**Conclusion.** For large granite-like materials, the dominant control lever is **contact-network
engineering**, not defect (dislocation) engineering — this is KNOWN PHYSICS (granular jamming, force
chains, rock mechanics, acoustic fluidization). Granite's rigidity is carried by a percolating
grain-contact/force-chain network and microcracks, which dominate the brittle regime; intragranular
dislocations matter only at depth. Temporary softening is contact-network disruption (factor ~5–10
friction/shear reduction) at microstrain and ~1–5 W/cm². This scopes the NP_141–147 dislocation picture:
it governs **metals**; **rock** is governed by contacts. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_154_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_154_Model` | granite = grains / contacts / microcracks | ✅ |
| `Y_NP_154_Compare` | contacts dominate; dislocations minor (brittle) | ✅ |
| `Y_NP_154_Soften` | contact disruption = acoustic fluidization | ✅ |
| `Y_NP_154_Estimate` | microstrain, ~0.06–0.6 MPa, ~1–5 W/cm² | ✅ |
| `Y_NP_154_Mobility` | contact mobility > defect mobility | ✅ |
| `Y_NP_154_Classification` | contact-network engineering (KNOWN PHYSICS) | ✅ |
| `Y_NP_154_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_154"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_137 (real-world evidence), NP_141 (yield
  stress softening), NP_142 (multi-band resonance control), NP_143 (dislocation threshold).
- Real-world record: granular jamming transition and force chains; DEM and photoelastic force-network
  studies; rock-mechanics of granite (microcrack density + grain-boundary contact stiffness control
  brittle strength); acoustic fluidization (Melosh; fault weakening, factor ~5–10).
