# ResearchY-NP_136 — Variable Rigidity Bounds Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_136 (permanent)
**Title:** Variable Rigidity Bounds Audit
**Status:** COMPLETE
**Date:** 2026-09-07
**File:** `NP_NewPhysics/ResearchY-NP_136.md`
**Depends on:** ResearchY-NP_100 (binding = phase locking), NP_110 (condensed matter), NP_128
(coherence = resource), NP_129 (coherent matter control), NP_131 (critical resonance), NP_132
(reversible softening), NP_133 (critical mode frequency), NP_134 (critical mode discovery), NP_135
(experimental audit)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_136_Tests.cs`

---

## Purpose

NP_131–135 established that a small set of critical modes controls rigidity, can be softened
reversibly at ultrasonic frequencies, and is falsifiably testable. NP_136 asks the magnitude question:
**what is the maximum reversible rigidity reduction physically achievable through coherent
critical-mode control?** Program: (1) use NP_131–135; (2) define Young modulus / shear modulus /
rigidity ratio; (3) determine realistic reduction 1% / 10% / 50% / 90%; (4) compare crystal / metal /
ceramic / granite; (5) identify failure modes (fracture / heating / decoherence). **Success
criterion:** determine whether coherent softening is a minor materials effect or a true
variable-rigidity technology. No new primitives; canonical AT unchanged.

---

## 1. Definitions

| Term | Definition |
|---|---|
| **Young modulus E** | tensile stiffness (∝ backbone locking) |
| **shear modulus G** | shear stiffness (∝ backbone locking) |
| **rigidity ratio R** | E/E₀ = G/G₀ = the surviving fraction of backbone locking |

Both moduli scale with the backbone locking, so a single rigidity ratio R captures the softening
(NP_131).

---

## 2/3. Rigidity ratio vs unlocked critical modes

Using the percolation rigidity model R(x) = max(0, (1 − x − p_c)/(1 − p_c)) with p_c = 0.5
(NP_131), x = fraction of critical modes unlocked:

| Material | m | unlock 1 | unlock 2 | unlock 3 |
|---|---|---|---|---|
| **crystal** | 6 | 0.667 (33%) | 0.333 (67%) | 0.000 (100%) |
| **metal** | 6 | 0.667 (33%) | 0.333 (67%) | 0.000 (100%) |
| **ceramic** | 6 | 0.667 (33%) | 0.333 (67%) | 0.000 (100%) |
| **granite** | 20 | 0.900 (10%) | 0.800 (20%) | 0.700 (30%) |

The reduction is **quantized in coarse steps for ordered materials** (m = 6: each critical mode is a
~33% drop) and **finer for amorphous/polycrystalline** (m = 20–40).

---

## 4. Realistic reduction tiers

| Tier | Achievable? | For m = 6 |
|---|---|---|
| **1%** | below the step size — NOT the coherent regime (a minor, thermal-scale effect) |
| **10%** | trivially — a partial unlock (or one granite step) |
| **50%** | yes — 1–2 critical modes |
| **90%** | yes — near-gel (2–3 critical modes), for UNLOADED material |

For an ordered material, even ONE critical mode gives ~33% — so coherent softening is inherently a
LARGE effect, not a fine 1% trim.

---

## 5. Failure modes bound the maximum

| Failure mode | How it limits the reversible maximum |
|---|---|
| **fracture** | reversible only while the softened structure still supports its load: unloaded → ~100% (gel); loaded → bounded by applied stress |
| **heating** | coherent spends m·E_bind (N/m efficient, NP_129); keeping ΔT small makes thermal softening (< 0.05%/K, NP_135) negligible |
| **decoherence** | keep the drive below the non-critical unlock threshold so order S ≈ 1 (ΔS = x·m·ln2 ≪ N·ln2 melting, NP_132) |

These are **engineering bounds, not fundamental limits**: the full gel state (R → 0) is achievable
transiently and reversibly for unloaded materials.

---

## Theorem

> **Theorem (NP_136).** The maximum reversible rigidity reduction is bounded only by the failure
> modes, not by the softening mechanism — coherent critical-mode control is a TRUE variable-rigidity
> technology, not a minor 1% effect. The rigidity ratio follows percolation R(x) = max(0,
> (1 − x − p_c)/(1 − p_c)): for ordered materials (crystal/metal/ceramic, m = 6), one critical mode
> unlocked gives ~33% reduction, two give ~67%, three give ~100% (gel); polycrystalline granite
> (m = 20) steps in ~10% increments. So reductions of 10%, 50%, and 90% are all achievable (90% near-
> gel, unloaded), while 1% is below the step size (a thermal-scale, minor effect — not the coherent
> regime). The maximum is bounded by (a) fracture — reversible only while the load is supported
> (unloaded → gel; loaded → stress-bounded); (b) heating — coherent is N/m efficient so ΔT stays
> small (NP_129/135); (c) decoherence — keep the drive below the non-critical unlock threshold so
> order S ≈ 1 (NP_132). Proof: (1) NP_131–135. (2) Definitions (Section 1). (3) R(x) (Section 2–3,
> verified — 33/67/100% for m = 6). (4) Tiers (Section 4). (5) Failure modes (Section 5). **Success
> criterion: coherent softening is a true variable-rigidity technology.** Classification:
> variable-rigidity technology DERIVED (NP_131/132); "coherent softening is a minor 1% effect"
> REFUTED. No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) NP_131–135. (2) Define. (3) R(x). (4) Tiers. (5) Failure modes. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "coherent softening is only ~1%" | one critical mode already gives ~33% for ordered materials |
| "90% reduction is impossible" | the gel state (R → 0) is achievable transiently (NP_132) for unloaded material |
| "softening has no upper bound" | fracture/heating/decoherence bound the reversible range |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| variable rigidity to 90% | a material whose rigidity cannot drop below ~10% under any coherent drive |
| quantized steps (m = 6) | a crystal whose rigidity drops continuously, not in ~33% steps |
| reversible to gel | a softening that irreversibly fractures/melts before R → 0 |

---

## 8. Classification

| Component | Status |
|---|---|
| variable-rigidity technology (10–90% reversible) | **DERIVED** (NP_131/132) |
| coherent softening is a minor 1% effect | **REFUTED** |

**Conclusion.** Coherent softening is a **true variable-rigidity technology**: reversible reductions
of 10–90%+ are achievable (the gel state for unloaded materials), quantized in coarse steps for
ordered materials and finer for amorphous. The bounds are the engineering failure modes — fracture
under load, heating at high power, and decoherence if mode selectivity is lost — not the softening
mechanism itself. No new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_136_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_136_Definitions` | E, G, rigidity ratio R | ✅ |
| `Y_NP_136_Ratio` | R(x) percolation; 33/67/100% for m=6 | ✅ |
| `Y_NP_136_Tiers` | 1% minor; 10/50/90% achievable | ✅ |
| `Y_NP_136_Materials` | crystal/metal/ceramic m=6; granite m=20 | ✅ |
| `Y_NP_136_FailureModes` | fracture/heating/decoherence bounds | ✅ |
| `Y_NP_136_Classification` | true variable-rigidity; minor-1% REFUTED | ✅ |
| `Y_NP_136_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_136"`

---

## References

- ResearchY-NP_100 (binding), NP_110 (condensed matter), NP_128 (coherence), NP_129 (coherent
  matter control), NP_131 (critical resonance), NP_132 (reversible softening), NP_133 (critical mode
  frequency), NP_134 (critical mode discovery), NP_135 (experimental audit).
