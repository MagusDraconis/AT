# ResearchY-NP_162 — Organizational Tomography Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_162 (permanent)
**Title:** Organizational Tomography Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_162.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_137
(real-world evidence), NP_157 (organization vs material), NP_158 (latent organization state), NP_159
(structural training), NP_160 (organizational field), NP_161 (organizational wave)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_162_Tests.cs`

---

## Purpose

NP_157–161 established that organization is a field φ(x,t) with latent states, training, spatial
variation, and propagating fronts. NP_162 asks the *measurement* question: **can the organizational
field φ(x,t) be directly imaged and tracked, rather than inferred from bulk properties?** Program:
(1) define observables (contact density, force-chain density, anisotropy, local rigidity);
(2) determine whether φ(x,t) can be reconstructed from ultrasonic measurements, nonlinear response,
scattering, and tomography; (3) compare a material-property map vs an organizational map; (4) search
for hidden organizational structures invisible to standard property measurements; (5) determine whether
organizational changes can be visualized in real time; (6) evaluate laboratory feasibility for a 1 m
granite block. **Success criterion:** determine whether the organizational field can be measured
directly rather than inferred from bulk properties. No new primitives; canonical AT unchanged.

---

## 1. Observables for the organizational field

| Observable | What it probes |
|---|---|
| **contact density** | number of grain contacts per volume |
| **force-chain density / orientation** | the load-bearing network |
| **anisotropy** | directional fabric (NP_155/156) |
| **local rigidity** | stiffness per region (NP_160) |

These are the *field variables*; the question is whether they are directly measurable.

---

## 2. Reconstructing φ(x,t)

| Method | What it images | Direct? |
|---|---|---|
| **photoelasticity** | force chains (transparent granular) | yes — visual |
| **X-ray microtomography** | grain/contact 3D structure | yes (cm-scale) |
| **ultrasonic tomography** | velocity/attenuation → stiffness map | yes (m-scale) |
| **coda-wave interferometry (CWI)** | microdamage / contact evolution (diffuse) | yes — sub-µs sensitivity |
| **nonlinear acoustics / NCWI** | crack/contact opening-closing (nonlinear indicators) | yes — sees what linear misses |

φ(x,t) is directly reconstructible; multiple independent modalities exist.

---

## 3. Material-property map vs organizational map

| Map | Content |
|---|---|
| **material-property map** | bulk/linear readouts (modulus, velocity) |
| **organizational map** | contact/force-chain/anisotropy/rigidity field |

The organizational map is the *finer* object: it is what the linear property map averages over. It is
obtained by the nonlinear and force-chain methods above — this is the known fabric/contact field.

---

## 4. Hidden structures invisible to standard property measurements

**Yes — and this is the key result.** Nonlinear acoustics and coda-wave interferometry reveal
contact/crack structure that **linear elastic** property measurements (single modulus, single velocity)
average away: the amplitude-dependent velocity shift, higher harmonics, and coda decorrelation are
signatures of the *contact network* itself, not of the bulk modulus. This is the nonlinear mesoscopic
elasticity result (NP_137): the organizational (contact/defect) structure is invisible to standard
linear measurements but visible to nonlinear methods.

---

## 5. Real-time visualization

| Method | Real-time? |
|---|---|
| photoelasticity | yes (video-rate) |
| coda-wave / NCWI monitoring | near-real-time |
| ultrasonic tomography | seconds–minutes (scalable) |

Organizational changes can be tracked in real time or near-real time — a prerequisite for closed-loop
steering (NP_156).

---

## 6. Laboratory feasibility (1 m granite block)

| Method | Feasible at 1 m? |
|---|---|
| ultrasonic tomography | yes (standard, meter-scale) |
| CWI / NCWI | yes (standard, meter-scale concrete/rock) |
| photoelasticity | no (needs transparent model) |
| X-ray microtomography | no (cm-scale only) |

For a **1 m granite block**, ultrasonic tomography + CWI/NCWI are the practical modalities; photoelastic
and X-ray apply to model/transparent or small specimens.

---

## Theorem

> **Theorem (NP_162).** The organizational field φ(x,t) CAN be directly imaged and tracked — and this is
> KNOWN PHYSICS. Contact density, force chains, anisotropy, and local rigidity are directly measured by
> photoelasticity (force chains), X-ray microtomography (grain/contact structure), ultrasonic tomography
> (stiffness), and coda-wave/nonlinear interferometry (contact/crack evolution). Critically, nonlinear
> acoustics and NCWI reveal contact/crack structure that LINEAR property measurements average away (the
> nonlinear mesoscopic elasticity result, NP_137) — so the organizational map is strictly finer than the
> material-property map. Real-time tracking is feasible (photoelastic video, near-real-time CWI); for a
> 1 m granite block, ultrasonic tomography + CWI/NCWI are the practical, standard modalities. AT's
> "organizational field" is an INTERPRETATION of this known fabric/contact-field imaging. Classification:
> direct imaging KNOWN PHYSICS; organizational map finer than property map KNOWN PHYSICS; "organizational
> tomography" framing AT INTERPRETATION. Proof: (1) Observables (Section 1). (2) Reconstruct (Section 2).
> (3) Property vs organizational (Section 3). (4) Hidden structures (Section 4). (5) Real-time
> (Section 5). (6) Feasibility (Section 6). **Success criterion: directly measurable — KNOWN PHYSICS.**
> No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Observables. (2) Reconstruct. (3) Compare. (4) Hidden. (5) Real-time. (6) Feasible. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the organizational field is only inferable" | photoelastic/X-ray/ultrasonic/CWI measure it directly |
| "the property map equals the organizational map" | nonlinear methods see contact/crack structure that linear measurements miss (NP_137) |
| "real-time tracking is impossible" | photoelastic video and near-real-time CWI track it |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| direct imaging (KNOWN) | a contact/force-chain observable that no existing modality can measure |
| organizational map finer than property map | a contact structure that nonlinear methods cannot distinguish from a uniform modulus |

---

## 9. Classification

| Component | Status |
|---|---|
| direct imaging of the organizational field | **KNOWN PHYSICS** |
| organizational map finer than material-property map | **KNOWN PHYSICS** (nonlinear mesoscopic elasticity) |
| "organizational tomography" framing | **AT INTERPRETATION** |
| direct imaging is a new capability | **REFUTED** |

**Conclusion.** The organizational field **can be measured directly** — via photoelasticity, X-ray
microtomography, ultrasonic tomography, and coda-wave/nonlinear interferometry — rather than inferred
from bulk properties. The organizational map is strictly **finer** than the material-property map:
nonlinear acoustics and NCWI reveal contact/crack structure that linear measurements average away
(NP_137). Real-time tracking is feasible; for a 1 m granite block, ultrasonic tomography + CWI/NCWI are
the standard modalities. This is KNOWN PHYSICS; AT's "organizational tomography" is an INTERPRETATION.
No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_162_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_162_Observables` | contact/force-chain/anisotropy/rigidity | ✅ |
| `Y_NP_162_Reconstruct` | photoelastic/X-ray/ultrasonic/CWI/nonlinear | ✅ |
| `Y_NP_162_PropertyVsOrganizational` | organizational map finer | ✅ |
| `Y_NP_162_Hidden` | nonlinear sees what linear misses | ✅ |
| `Y_NP_162_RealTime` | photoelastic video + near-real-time CWI | ✅ |
| `Y_NP_162_Feasibility` | 1 m block: ultrasonic tomography + CWI/NCWI | ✅ |
| `Y_NP_162_Classification` | KNOWN PHYSICS; framing INTERPRETATION | ✅ |
| `Y_NP_162_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_162"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_137 (real-world evidence), NP_157
  (organization vs material), NP_158 (latent organization state), NP_159 (structural training), NP_160
  (organizational field), NP_161 (organizational wave).
- Real-world record: photoelastic force-chain imaging; X-ray microtomography of granular contacts;
  ultrasonic tomography (concrete/rock); coda-wave interferometry and nonlinear coda-wave interferometry
  (microdamage/contact monitoring); nonlinear mesoscopic elasticity (amplitude-dependent velocity,
  harmonics).
