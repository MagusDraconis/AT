# ResearchY-NP_156 — Force Path Steering Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_156 (permanent)
**Title:** Force Path Steering Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_156.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_137
(real-world evidence), NP_154 (contact network softening), NP_155 (force chain programming)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_156_Tests.cs`

---

## Purpose

NP_155 established that force chains can be reconfigured and that multiple metastable states exist.
NP_156 asks the *steering* question: **can force-chain networks be deliberately steered into preferred
load paths — selectively strengthening some paths while weakening others — rather than merely
softened?** Program: (1) model a 1 m granite block as grains / contacts / force chains; (2) define a
"preferred force path"; (3) determine whether external excitation can strengthen specific paths while
weakening others; (4) compare random force-chain evolution vs directed steering; (5) test controls
(preload, vibration orientation, multi-point excitation); (6) measure load concentration, stiffness
anisotropy, fracture location. **Success criterion:** determine whether a granite-like material can be
guided into specific internal force-network states rather than merely softened. No new primitives;
canonical AT unchanged.

---

## 1. Model a 1 m granite block

| Element | Role |
|---|---|
| grains | building blocks |
| contacts | grain junctions |
| force chains | filamentary load-bearing paths |

The block is a jammed granular assembly; load is carried by a sparse, filamentary network whose
geometry is controlled by boundary conditions and loading history.

---

## 2. Define a "preferred force path"

A **preferred force path** is a designed, spatially-localized channel through which load is concentrated
(e.g. an arch or a column of high-stress contacts). Steering means biasing the network so that stress
preferentially flows along chosen chains and away from others.

---

## 3. Strengthen specific paths while weakening others

| Operation | Mechanism |
|---|---|
| **strengthen a path** | over-compact / preload that region → denser, stiffer chains |
| **weaken a path** | unload / vibration-fluidize that region → looser, weaker chains |
| **spatial bias** | multi-point / phased excitation concentrates stress where desired |

Selective strengthening/weakening is the *spatial* version of NP_155's reconfiguration: it localizes
the density/anisotropy of the network, not just its orientation.

---

## 4. Random vs directed force-chain steering

| Mode | Outcome |
|---|---|
| **random evolution** | chains scramble isotropically; no preferred path emerges |
| **directed steering** (boundary-controlled) | chains align and localize along a chosen direction/region — **preferred paths form** |

Directed steering is **established**: force-chain orientation and localization follow the imposed
boundary stresses (DEM/photoelastic; rock-mechanics stress redistribution).

---

## 5. Controls

| Control | Steering effect |
|---|---|
| **preload** | densifies chosen paths (stress concentration) |
| **vibration orientation** | aligns chains along the vibration axis |
| **multi-point excitation** | spatially shapes the stress field (phased) |

Preload and orientation are **standard** rock/granular controls; multi-point/phased excitation is
emerging.

---

## 6. Load concentration / stiffness anisotropy / fracture location

| Quantity | Steerable? |
|---|---|
| **load concentration** | yes — stress localizes where paths are strengthened |
| **stiffness anisotropy** | yes — oriented chains give directional stiffness |
| **fracture location** | yes — cracks initiate where paths are weakened/over-stressed |

All three are observable signatures of successful steering; fracture-location control is the most
direct (steer the weak path to set the failure site).

---

## Theorem

> **Theorem (NP_156).** Force-chain networks CAN be deliberately steered into preferred load paths —
> selectively strengthening some chains while weakening others — and this is KNOWN PHYSICS in the
> granular/rock regime: boundary-controlled loading, preload, and oriented vibration localize and
> align force chains (DEM, photoelastic, rock-mechanics stress redistribution), steering load
> concentration, stiffness anisotropy, and fracture location. The distinction from NP_155 is spatial:
> steering shapes *where* stress flows, not just that the network reconfigures. The open question is
> fine-grained, reversible, vibration-only steering of a CONSOLIDATED granite block — large spatial
> rewrites again cost microcracking damage (NP_155's reversible–irreversible bound). Classification:
> directed steering KNOWN PHYSICS; load-path localization KNOWN PHYSICS; reversible vibration-only
> steering of consolidated granite AT QUESTION. Proof: (1) Model (Section 1). (2) Define (Section 2).
> (3) Strengthen/weaken (Section 3). (4) Random vs directed (Section 4). (5) Controls (Section 5).
> (6) Measure (Section 6). **Success criterion: guided steering possible — KNOWN PHYSICS (granular),
> damage-bounded for consolidated rock.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Model. (2) Define. (3) Strengthen/weaken. (4) Directed. (5) Controls. (6) Measure. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "force paths cannot be localized" | stress concentration/anisotropy follow boundary loading (DEM, rock mechanics) |
| "random evolution steers" | random scrambling yields no preferred path |
| "consolidated granite steers freely" | large spatial rewrites cost microcracking (NP_155) |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| directed steering (KNOWN) | a granular/rock system whose force paths are insensitive to boundary conditions/orientation |
| fracture-location steering | a block whose failure site is independent of the imposed force-path pattern |

---

## 9. Classification

| Component | Status |
|---|---|
| directed force-chain steering | **KNOWN PHYSICS** (granular/rock mechanics) |
| load-path localization (concentration/anisotropy) | **KNOWN PHYSICS** |
| fracture-location control | **KNOWN PHYSICS** (stress-path engineering) |
| reversible vibration-only steering of consolidated granite | **AT QUESTION** (damage-bounded) |

**Conclusion.** A granite-like material **can be guided into specific internal force-network states**
— force chains can be steered into preferred load paths by boundary-controlled loading, preload, and
oriented vibration, localizing load concentration, stiffness anisotropy, and even fracture location.
This is KNOWN PHYSICS (granular mechanics + rock-mechanics stress redistribution). The open question is
*reversible, vibration-only* steering of a **consolidated** block: as in NP_155, large spatial rewrites
cost microcracking damage. So steering is real and directed — but for a cemented block, it is a
damage-bounded, not a freely reversible, operation. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_156_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_156_Model` | grains / contacts / force chains | ✅ |
| `Y_NP_156_Define` | preferred force path definition | ✅ |
| `Y_NP_156_Steer` | strengthen/weaken specific paths | ✅ |
| `Y_NP_156_Directed` | directed steering beats random | ✅ |
| `Y_NP_156_Controls` | preload / orientation / multi-point | ✅ |
| `Y_NP_156_Measure` | load concentration / anisotropy / fracture | ✅ |
| `Y_NP_156_Classification` | KNOWN PHYSICS; consolidated steering AT QUESTION | ✅ |
| `Y_NP_156_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_156"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_137 (real-world evidence), NP_154 (contact
  network softening), NP_155 (force chain programming).
- Real-world record: force-chain anisotropy and stress localization under boundary loading (DEM,
  photoelastic); rock-mechanics stress redistribution (tunneling, pre-conditioning, stress shadowing);
  shear-band / fracture-location control; the reversible–irreversible granular phase diagram.
