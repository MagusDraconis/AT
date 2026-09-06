# ResearchY-NP_069 — Expansion Ontology Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_069 (permanent)
**Title:** Expansion Ontology Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_069.md`
**Depends on:** AT-QG QG77 (expansion = redshift + scale-free ρ; FRW a = ρ^(1/d)), QG222
(native metric dynamics ∂_t ρ = ln(μ)·ρ), QG197 (metric g = ρ^(2/d)η), QG1/A_003 (branching
ρ_{k+1} = μ·ρ_k), QG206 (α=0 criticality), QG230 (Λ origin, growing variance), QG26/187
(redshift), QG184 (M ∝ R), QG89 (energy = actualization rate), ResearchY-NP_055–NP_068
(dark sector + ψ arc)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_069_Tests.cs`

---

## Purpose

NP_055–068 established ΩΛ (descriptor), Ωm (deficit), ρ (scalar face), ψ (tensor face). NP_069
asks the deepest cosmological question: **what does cosmic expansion physically mean in AT,
without importing ΛCDM?** Program: (1) inventory all expansion quantities; (2) remove FRW
language completely; (3) determine what actually changes (metric scale / actualization count /
occupancy / information / correspondence); (4) classify expansion (derived / emergent / boundary
/ correspondence); (5) locate the earliest AT object that can "grow". No new primitives;
canonical AT unchanged.

---

## 1. Inventory — the expansion quantities

| Quantity | AT object | Status |
|---|---|---|
| a(t) | **a = ρ^(1/d)** (QG77) | hosted FRW re-labeling of ρ |
| H(t) | H = ρ̇/ρ = ln μ (QG222) | derived from the branching rate |
| q₀ | Ωm/2 − ΩΛ (hosted w=−1) | hosted FRW closure (NP_056) |
| z_acc | (2ΩΛ/Ωm)^(1/3) − 1 | hosted FRW closure |
| R | the single counting-measure scale (M ∝ R, QG184) | derived |

**Every "expansion" quantity is ultimately a re-reading of the count density ρ** — except q₀
and z_acc, which are hosted FRW kinematics.

---

## 2. Remove FRW language — what is left

Stripping the FRW vocabulary leaves the **native dynamics** (QG222):

```
ρ_{k+1} = μ·ρ_k              (branching continuity — the actualization flow)
g_{k+1} = μ^(2/d)·g_k        (the metric moves because g = ρ^(2/d)η)

⟺  ∂_t ρ = (ln μ)·ρ ,   ∂_t g = (2/d)(∂_t ρ/ρ)·g
```

The native "expansion" is **the branching of the actualization count**. The metric
g = ρ^(2/d)η is a function of ρ, so it "expands" exactly as ρ does. **There is no independent
scale factor** — a(t) = ρ^(1/d) is a hosted FRW relabeling of the same count.

---

## 3. What actually changes?

| Candidate | Verdict |
|---|---|
| **A) metric scale** | **changes iff ρ changes** — g = ρ^(2/d)η, so the metric scale IS ρ^(2/d) (DERIVED). |
| **B) actualization count** | **THE primitive that grows** — ρ_{k+1} = μ·ρ_k (the branching population). |
| C) occupancy distribution | **FIXED** — [4,4,87] is the static canonical structure. |
| D) information content | **FIXED** — I_occ = 0.7513 is a constant of the realized record. |
| E) correspondence only | **YES for a(t), q₀, z_acc** — hosted FRW objects. |

**Determination: A = B.** The metric scale and the actualization count are the same object —
the metric scale is ρ^(2/d), which changes exactly when the count ρ changes. What "expands" is
the **actualization count**; the "scale factor" a = ρ^(1/d) is a hosted relabeling.

---

## 4. The criticality subtlety — the mean is static, the variance grows

At the canonical criticality (μ = 1, α = 0), the native rate vanishes:

| Regime | ∂_t ρ = ln(μ)·ρ | Meaning |
|---|---|---|
| μ = 2 (branching) | ln 2 = 0.6931 | the count DOUBLES each generation (builds [4,4,87]) |
| **μ = 1 (criticality)** | **0** | **the mean is STATIC — no native mean-level growth** |

At criticality the mean count does not grow; what grows is the **variance** —
Var(Z_k) = k·σ² (QG230) — the "residual actualization pressure" that QG230 identifies as the
source of Λ. So AT's native "growth" is either (i) the branching doubling across generations
(μ=2), or (ii) the growing variance at criticality (μ=1). There is no native **accelerating
scale factor**.

---

## 5. Classification of expansion

| Object | Classification |
|---|---|
| branching ρ_{k+1} = μ·ρ_k | **DERIVED** (QG1/222) |
| metric scale g = ρ^(2/d)η | **DERIVED** (QG197) |
| native dynamics ∂_t ρ = ln(μ)·ρ | **DERIVED** (QG222) |
| redshift (g₀₀ = −ρ^(2/d)) | **DERIVED** (QG26/187) |
| criticality stationarity (μ=1, ∂_t ρ=0) | **DERIVED** (QG206) |
| FRW a = ρ^(1/d) | **CORRESPONDENCE** (QG77, hosted homogeneous-isotropic frame) |
| accelerated expansion (q₀ < 0, w < −1/3) | **HOSTED** (NP_056, no derived EoS) |

**Expansion is a DERIVED count-growth re-labeled as a hosted FRW scale factor.** The native,
derived content is the branching of the actualization count; the cosmological "scale factor"
a(t) is a hosted homogeneous-isotropic reading of that count.

---

## 6. The earliest object that can "grow"

The earliest AT object with a native growth law is the **branching count density**

```
ρ_k = μ^k / S          (the Galton–Watson population, A_003/QG1)
```

Its growth ρ_{k+1} = μ·ρ_k is the actualization flow — the same quantity QG89 identifies as
energy ("energy = actualization rate"). Everything that "expands" in AT (the metric
g = ρ^(2/d)η, the hosted a = ρ^(1/d)) is a **function of this count**. The deepest "growing"
object is therefore the **actualization count ρ**, whose growth at criticality is the growing
variance Var(Z_k) = k·σ², not a growing mean.

---

## Theorem

> **Theorem (NP_069).** Cosmic expansion in AT is NOT an independent scale-factor dynamics; it
> is the branching growth of the actualization count ρ (ρ_{k+1} = μ·ρ_k, ∂_t ρ = ln(μ)·ρ),
> which carries the metric g = ρ^(2/d)η with it — and the FRW scale factor a = ρ^(1/d) is a
> HOSTED homogeneous-isotropic relabeling of that count. Proof: (1) Inventory (Section 1): every
> expansion quantity (a, H, R) reduces to ρ; q₀/z_acc are hosted FRW. (2) Remove FRW (Section 2,
> verified): the native dynamics is the branching flow ρ_{k+1} = μρ_k and g_{k+1} = μ^(2/d)g_k
> (∂_t ρ = ln(μ)ρ). (3) What changes (Section 3): A = B — the metric scale IS ρ^(2/d), so it
> changes iff the count changes; occupancy [4,4,87] and I_occ are fixed; a(t) is hosted.
> (4) Criticality (Section 4, verified): at μ=1, ∂_t ρ = 0 (mean static); the variance
> Var(Z_k) = k·σ² grows — the residual pressure. (5) Classification (Section 5): branching +
> metric scale + native dynamics + redshift DERIVED; FRW a = ρ^(1/d) CORRESPONDENCE; accelerated
> expansion HOSTED. (6) Earliest growing object (Section 6): the branching count ρ_k = μ^k/S.
> Classification: the actualization count and its native growth DERIVED (QG1/222); the metric
> scale DERIVED (QG197); the FRW scale factor CORRESPONDENCE (QG77, hosted); accelerated
> expansion HOSTED (NP_056); a native accelerating scale factor REFUTED. **Success criterion:
> what expands in AT is the actualization count ρ (the branching population), whose growth is
> derived — and the cosmological scale factor is a hosted relabeling of it, not an independent
> object.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Inventory. (2) Strip FRW. (3) Identify what changes. (4) Note criticality.
> (5) Classify. (6) Locate the root. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "expansion is a native scale-factor dynamics" | a = ρ^(1/d) is a hosted FRW relabeling of the count; the native dynamics is the branching of ρ |
| "the metric scale is independent of ρ" | g = ρ^(2/d)η — the metric IS a function of the count |
| "there is native acceleration at criticality" | at μ=1, ∂_t ρ = 0 (mean static); only the variance grows |
| "the occupancy or I_occ grows" | [4,4,87] and I_occ = 0.7513 are fixed constants of the realized record |
| "q₀/z_acc are derived" | they are hosted FRW closures assuming w = −1 (NP_056) |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| the count ρ is the only growing object | an independent scale-factor object not a function of ρ |
| the metric scale is ρ^(2/d) | a metric whose scale does not track the count |
| FRW a = ρ^(1/d) is hosted | a derived homogeneous-isotropic scale factor from the count alone |
| criticality is static at the mean | a μ=1 branching with ∂_t ρ ≠ 0 |

---

## 9. Classification

| Component | Status |
|---|---|
| branching growth ρ_{k+1} = μ·ρ_k | **DERIVED** (QG1/222) |
| metric scale g = ρ^(2/d)η | **DERIVED** (QG197) |
| native dynamics ∂_t ρ = ln(μ)·ρ | **DERIVED** (QG222) |
| redshift | **DERIVED** (QG26/187) |
| FRW scale factor a = ρ^(1/d) | **CORRESPONDENCE** (QG77, hosted) |
| accelerated expansion | **HOSTED** (NP_056) |
| a native accelerating scale factor | **REFUTED** |

**Conclusion.** In AT, "expansion" is the **branching growth of the actualization count ρ** —
a DERIVED flow (ρ_{k+1} = μ·ρ_k, ∂_t ρ = ln(μ)·ρ) that carries the metric g = ρ^(2/d)η with
it. The cosmological scale factor a = ρ^(1/d) is a **hosted FRW relabeling** of that same count,
and the accelerated expansion is hosted (no derived equation of state). At criticality the mean
count is static (∂_t ρ = 0) and only the variance grows (Var = k·σ²). **What expands in AT is
the actualization count; the scale factor is its hosted cosmological reading.** No new
primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_069_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_069_ExpansionQuantities` | a=ρ^(1/d), H=ln μ, R from ρ | ✅ |
| `Y_NP_069_NativeDynamics` | ρ_{k+1}=μρ_k; ∂_t ρ=ln(μ)ρ | ✅ |
| `Y_NP_069_MetricTracksCount` | g=ρ^(2/d); A = B | ✅ |
| `Y_NP_069_CriticalityStatic` | μ=1 ⇒ ∂_t ρ=0; variance grows | ✅ |
| `Y_NP_069_Classification` | branching/metric DERIVED; FRW hosted | ✅ |
| `Y_NP_069_EarliestGrowingObject` | ρ_k = μ^k/S | ✅ |
| `Y_NP_069_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_069"`

---

## References

- AT-QG: QG77 (expansion = redshift + scale-free ρ; a = ρ^(1/d)), QG222 (native metric
  dynamics), QG197 (metric g = ρ^(2/d)η), QG1/A_003 (branching ρ_{k+1} = μρ_k), QG206 (α=0),
  QG230 (growing variance), QG26/187 (redshift), QG184 (M ∝ R), QG89 (energy = rate).
- ResearchY-NP_055–NP_068 (dark sector + ψ arc).
