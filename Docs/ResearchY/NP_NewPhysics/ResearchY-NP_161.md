# ResearchY-NP_161 — Organizational Wave Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_161 (permanent)
**Title:** Organizational Wave Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_161.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_154 (contact
network softening), NP_155 (force chain programming), NP_157 (organization vs material), NP_158 (latent
organization state), NP_159 (structural training), NP_160 (organizational field)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_161_Tests.cs`

---

## Purpose

NP_157–160 established that organization is a field φ(x) with latent states, training, and spatial
variation. NP_161 asks the *dynamics* question: **can organizational states propagate through a
granite-like material as waves or fronts?** Program: (1) define organizational state / gradient /
front; (2) determine whether local reorganization induces neighboring reorganization; (3) compare
static vs propagating states; (4) search for analogues (shear bands, compaction fronts, jamming fronts,
fluidization fronts); (5) determine whether organization behaves as a state or a dynamical field;
(6) estimate propagation speed / persistence / reversibility. **Success criterion:** determine whether
organizational change can travel through material independently of ordinary elastic waves. No new
primitives; canonical AT unchanged.

---

## 1. Define the three objects

| Object | Definition |
|---|---|
| **organizational state** | a contact-network configuration φ at a point |
| **organizational gradient** | a spatial variation of φ between regions (NP_160) |
| **organizational front** | a propagating boundary between two organizational states |

A front is a *moving* gradient — organizational change traveling through the material.

---

## 2. Does local reorganization induce neighboring reorganization?

**Yes.** A localized reorganization (unlock/compact/fluidize) perturbs the adjacent contact network,
which relaxes by reorganizing in turn — a **front**. This is the standard mechanism by which a shear
band or a compaction front advances: local rearrangement loads/releases the next region, which follows.

---

## 3. Static vs propagating state

| Mode | Description |
|---|---|
| **static state** | a fixed organizational configuration (NP_158/160) |
| **propagating state** | a front: the state boundary moves through the material |

The static state is the fixed point; the propagating front is its *spatiotemporal* generalization.

---

## 4. Known analogues (all KNOWN PHYSICS)

| Front | Propagation speed |
|---|---|
| **jamming front** | fast — at/above granular sound speed |
| **solitary waves** (granular chains) | fast — sonic/supersonic (nonlinear) |
| **compaction front** | intermediate — set by rearrangement rate |
| **shear band** | slow — set by local strain rate |
| **fluidization front** | intermediate (acoustic fluidization) |

These are the *known* organizational fronts: each is a propagating boundary between organizational
states, independent of ordinary (linear) elastic waves.

---

## 5. State or dynamical field?

**A dynamical field.** Organization is not a single state but a field φ(x,t) that can support fronts —
exactly the known continuum/granular picture (fabric tensor evolving with flow). The "organizational
wave" is the AT name for these propagating fabric fronts.

---

## 6. Propagation speed / persistence / reversibility

| Quantity | Estimate |
|---|---|
| **speed** | spans orders of magnitude — jamming/solitary fast, shear slow |
| **persistence** | limited by dissipation (fronts decay unless driven) |
| **reversibility** | sub-damage fronts reversible; damage-bounded fronts irreversible (NP_155) |

Fronts are transient and dissipative unless sustained by the drive; reversibility is damage-bounded.

---

## Theorem

> **Theorem (NP_161).** Organizational change CAN propagate through a granite-like material as waves or
> fronts — and this is KNOWN PHYSICS. Localized reorganization induces neighboring reorganization,
> producing propagating boundaries between organizational states: jamming fronts (at/above granular
> sound speed), compaction fronts, shear bands, fluidization fronts, and granular solitary waves — all
> distinct from ordinary elastic waves. Organization is therefore a DYNAMICAL FIELD φ(x,t), not a static
> state. AT's "organizational wave" framing is an INTERPRETATION of these known propagating fabric
> fronts; their speed spans orders of magnitude, persistence is dissipation-limited, and reversibility is
> damage-bounded (NP_155). Classification: organizational fronts KNOWN PHYSICS; dynamical-field view
> KNOWN PHYSICS; "organizational wave" framing AT INTERPRETATION. Proof: (1) Define (Section 1).
> (2) Induce (Section 2). (3) Static vs propagating (Section 3). (4) Analogues (Section 4). (5) Dynamical
> field (Section 5). (6) Speed/persistence/reversibility (Section 6). **Success criterion: organizational
> change travels as fronts — KNOWN PHYSICS.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define. (2) Induce. (3) Compare. (4) Analogues. (5) Field. (6) Estimate. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "organizational change is local-only" | jamming/compaction/shear/fluidization fronts propagate through the material |
| "fronts are just elastic waves" | they are boundaries between organizational states, not linear elastic pulses |
| "organization is a static state" | φ(x,t) supports fronts — a dynamical field |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| organizational fronts (KNOWN) | a granular/rock system where no reorganization propagates beyond the excited region |
| front ≠ elastic wave | a front whose propagation is governed by the ordinary elastic wave equation |

---

## 9. Classification

| Component | Status |
|---|---|
| organizational fronts (jamming/compaction/shear/fluidization) | **KNOWN PHYSICS** |
| organization = dynamical field φ(x,t) | **KNOWN PHYSICS** |
| "organizational wave" framing | **AT INTERPRETATION** |
| organizational waves are a new capability | **REFUTED** |

**Conclusion.** Organizational change **travels through granite-like material as fronts** — jamming
fronts, compaction fronts, shear bands, fluidization fronts, and granular solitary waves — independent
of ordinary elastic waves. Organization is a **dynamical field** φ(x,t), not a static state; this is
KNOWN PHYSICS (granular/continuum fabric dynamics), and AT's "organizational wave" is an INTERPRETATION
of it. Speed spans orders of magnitude, persistence is dissipation-limited, and reversibility is
damage-bounded (NP_155). No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_161_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_161_Define` | state / gradient / front | ✅ |
| `Y_NP_161_Induce` | local reorg induces neighboring reorg | ✅ |
| `Y_NP_161_StaticVsPropagating` | front = moving gradient | ✅ |
| `Y_NP_161_Analogues` | jamming/compaction/shear/fluidization/solitary | ✅ |
| `Y_NP_161_Field` | organization = dynamical field φ(x,t) | ✅ |
| `Y_NP_161_Estimate` | speed/persistence/reversibility | ✅ |
| `Y_NP_161_Classification` | KNOWN PHYSICS; framing INTERPRETATION | ✅ |
| `Y_NP_161_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_161"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_154 (contact network softening), NP_155
  (force chain programming), NP_157 (organization vs material), NP_158 (latent organization state),
  NP_159 (structural training), NP_160 (organizational field).
- Real-world record: jamming fronts (at/above granular sound speed); compaction fronts; shear-band
  propagation; acoustic fluidization fronts; solitary waves in granular chains (nonlinear, sonic/
  supersonic); DEM/photoelastic/X-ray tomography of propagating granular fronts.
