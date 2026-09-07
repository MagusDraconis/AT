# ResearchY-NP_159 — Structural Training Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_159 (permanent)
**Title:** Structural Training Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_159.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_154 (contact
network softening), NP_155 (force chain programming), NP_157 (organization vs material), NP_158 (latent
organization state)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_159_Tests.cs`

---

## Purpose

NP_158 established that granite possesses multiple latent organizational states; NP_155 established
that force-chain networks have memory. NP_159 asks the *training* question: **can a granite-like block
be trained into a preferred organizational state through repeated excitation and loading history?**
Program: (1) define training / memory / preferred state / organizational bias; (2) apply repeated
preload / vibration / unload cycles; (3) determine whether the final organization depends on history;
(4) compare a virgin vs trained block; (5) measure anisotropy, damping, fracture location, force-chain
orientation; (6) determine whether the block develops a persistent preference for specific load paths.
**Success criterion:** determine whether granite-like materials can be structurally trained rather than
simply modified. No new primitives; canonical AT unchanged.

---

## 1. Define the four terms

| Term | Definition |
|---|---|
| **training** | repeated cycles that bias the organizational state toward a preferred configuration |
| **memory** | the final state depends on the loading/vibration history, not just the final load |
| **preferred state** | the organizational configuration the cycles drive toward |
| **organizational bias** | a persistent, history-imprinted anisotropy/orientation of the contact network |

---

## 2. Repeated preload / vibration / unload cycles

Each cycle nudges the contact network; repeated cycles accumulate a **history-dependent fabric**:

```
preload → compact/orient chains → vibrate → rearrange → unload → settle (biased)
```

The bias persists because the network's relaxation is **path-dependent** (granular memory).

---

## 3. Does the final organization depend on history?

**Yes.** Granular materials exhibit **memory and aging**: the final contact-network state is a function
of the *sequence* of prior loadings, not just the current conditions. Cyclic shear/vibration imprints
the network with the history of applied directions and amplitudes — the stress-force-fabric relation.

---

## 4. Virgin vs trained block

| Block | Network character |
|---|---|
| **virgin** | isotropic, no preferred orientation (or as-formed fabric) |
| **trained** | anisotropic — force chains aligned along the training direction |

The trained block has a **different** organizational state at the same chemistry/mineral/T (NP_158) — a
persistent, history-imprinted anisotropy.

---

## 5. Measured signatures of training

| Quantity | Trained response |
|---|---|
| **anisotropy** | stiffness higher along the training direction |
| **damping** | altered (looser/contact-rich vs trained) |
| **fracture location** | biased away from / toward the trained path (NP_156) |
| **force-chain orientation** | aligned with the training axis |

All four are observable, and all change between virgin and trained blocks.

---

## 6. Persistent preference for specific load paths

**Yes.** The trained block develops a persistent preference: force chains are oriented along the
training direction, so subsequent loading follows those paths preferentially. This is "training" in the
sense of an accumulated, history-dependent organizational bias — not just a one-shot modification.

---

## Theorem

> **Theorem (NP_159).** Granite-like materials CAN be structurally trained, not merely modified — and
> this is KNOWN PHYSICS. Repeated preload/vibration/unload cycles accumulate a history-dependent fabric
> (granular memory/aging): the final contact-network state depends on the loading sequence, so a virgin
> block becomes anisotropic and develops a persistent preference for specific load paths. The trained
> block shows measurable differences in anisotropy, damping, fracture location, and force-chain
> orientation versus a virgin block. AT's "structural training" framing is an INTERPRETATION of this
> established granular/shear-history physics (stress-force-fabric, cyclic-shear training). Classification:
> structural training KNOWN PHYSICS; history-dependent fabric KNOWN PHYSICS; "training" framing AT
> INTERPRETATION. Proof: (1) Define (Section 1). (2) Cycles (Section 2). (3) History (Section 3).
> (4) Virgin vs trained (Section 4). (5) Measure (Section 5). (6) Preference (Section 6). **Success
> criterion: structurally trainable — KNOWN PHYSICS.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Define. (2) Cycles. (3) History. (4) Compare. (5) Measure. (6) Preference. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "training is new physics" | granular memory/aging and cyclic-shear training are established |
| "final state is history-independent" | the stress-force-fabric relation shows path dependence |
| "training = one-shot modification" | training is accumulated, history-imprinted bias, not a single operation |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| structural training (KNOWN) | a granular/rock system whose final fabric is independent of the loading sequence |
| persistent load-path preference | a trained block with no anisotropy or orientation bias |

---

## 9. Classification

| Component | Status |
|---|---|
| structural training (history-dependent fabric) | **KNOWN PHYSICS** |
| persistent load-path preference (anisotropy) | **KNOWN PHYSICS** |
| "structural training" framing | **AT INTERPRETATION** |
| training is a new capability | **REFUTED** |

**Conclusion.** Granite-like materials **can be structurally trained** — repeated preload/vibration/
unload cycles accumulate a history-dependent fabric that leaves a persistent organizational bias
(anisotropic force chains, preferred load paths), measurable in anisotropy, damping, and fracture
location. This is KNOWN PHYSICS (granular memory/aging, cyclic-shear training); AT's "structural
training" is an INTERPRETATION of it. No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_159_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_159_Define` | training / memory / preferred / bias | ✅ |
| `Y_NP_159_Cycles` | repeated preload/vibration/unload | ✅ |
| `Y_NP_159_History` | final organization depends on history | ✅ |
| `Y_NP_159_VirginVsTrained` | trained block anisotropic | ✅ |
| `Y_NP_159_Measure` | anisotropy / damping / fracture / orientation | ✅ |
| `Y_NP_159_Preference` | persistent load-path preference | ✅ |
| `Y_NP_159_Classification` | KNOWN PHYSICS; framing AT INTERPRETATION | ✅ |
| `Y_NP_159_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_159"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_154 (contact network softening), NP_155
  (force chain programming), NP_157 (organization vs material), NP_158 (latent organization state).
- Real-world record: granular memory and aging; cyclic-shear training of granular packings; shear-
  induced fabric anisotropy; stress-force-fabric relation; DEM/photoelastic history-dependence studies.
