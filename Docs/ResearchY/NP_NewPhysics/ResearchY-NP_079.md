# ResearchY-NP_079 — Scale-Freeness Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_079 (permanent)
**Title:** Scale-Freeness Origin Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_079.md`
**Depends on:** AT-QG QG001 (Difference, the founding primitive), QG007 (critical branching
μ=1), QG011 (discrete tick), QG194 (matter = deficit), QG222 (native dynamics), QG270
(Distinction Origin — Universal Difference Principle), QG40 (Final Boundary Audit),
AT-F1 (indifference principle), ResearchY-NP_070 (criticality), NP_078 (α=0 necessity)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_079_Tests.cs`

---

## Purpose

NP_078 concluded scale-freeness (AT-F1: "the primitives carry no intrinsic scale") is the final
boundary. NP_079 asks whether that is actually the deepest point: **can scale-freeness be derived
from Difference itself, or is it the final irreducible boundary?** Program: (1) remove AT-F1 and
replace with an intrinsic scale λ; (2) test λ>0, λ→0, multiple scales; (3) measure impact on μ,
α, criticality, Ωm, structure growth, the D96 hierarchy; (4) determine whether Difference alone
permits any preferred scale; (5) search for hidden assumptions (counting, actualization,
occupancy, normalization); (6) decide A/B/C/D. **Success criterion:** locate the deepest source of
AT-F1. No new primitives; canonical AT unchanged.

---

## 1. What AT-F1 already is — scale-freeness = renormalization invariance

AT-F1 (the indifference principle) was already audited (AT-F Phase 1): the counting measure is a
**density of weight d** (N = ∫ρ dV is invariant under x→λx with ρ→λ⁻ᵈρ), and the causal order is a
**scale-invariant partial order**. A scale-covariant abundance must satisfy n(λR)/n(R) = f(λ)
independent of R — the **power law** n ∝ R⁻ᵖ:

| profile | n(2R)/n(R) | scale-covariant? |
|---|---|---|
| power law (p = 1) | 0.500 (constant) | **YES** (self-similar) |
| power law (p = 2) | 0.250 (constant) | **YES** |
| Gaussian (width λ) | 0.223 → 0.003 → 0.000 (R-dependent) | **NO** (scale-setting) |

Scale-freeness is the **unique renormalization-invariant** abundance: power laws are the RG fixed
points; any scale-setting profile (a Gaussian bump at λ) flows away under coarse-graining. So
AT-F1 is **DERIVED conditional on renormalization invariance** — not a bare postulate.

---

## 2. Remove AT-F1 — introduce an intrinsic scale λ

| Test | Result |
|---|---|
| **λ finite (λ > 0)** | scale-setting: the abundance is a Gaussian/characteristic profile, not a power law → μ ≠ 1 (criticality breaks), α ≠ 0 (flat rotation breaks), structure gets a preferred scale |
| **λ → 0** | a single degenerate scale at the origin — still scale-setting (a UV cutoff), not scale-free |
| **λ → ∞** | the profile flattens → the scale decouples → **recovers scale-freeness** (the self-similar limit) |
| **multiple scales** | an even richer scale-setting structure — breaks renormalization invariance further |

**Impact of a finite λ:**

| Quantity | With AT-F1 (scale-free) | With intrinsic λ (scale-setting) |
|---|---|---|
| μ | 1 (marginal, unique) | ≠ 1 (sub/supercritical) |
| α | 0 (flat rotation) | ≠ 0 (rising/falling curve) |
| criticality | scale-free (L = ∞) | finite correlation length |
| Ωm = H/ln K | 0.3161 (D96 spectrum fact) | unchanged in *value* (spectrum fact), but the abundance law across octaves becomes non-uniform |
| structure growth | scale-free seed | preferred-scale seed |
| D96 hierarchy | equal-per-octave (self-similar) | a preferred rung |

The D96 *numbers* ([4,4,87], N=96, K=3) are a discrete dimensionless spectrum and do not themselves
change; what breaks is the **abundance law across octaves** (equal-per-octave → non-uniform).

---

## 3. Does Difference alone permit any preferred scale?

The founding primitive is **Difference** (QG270: "the theory bottoms out in difference — the most
primitive notion"). A difference is a **binary** relation: a thing is distinguishable *insofar as it
differs from something else*. Binary distinction carries **no metric** — no "how much" different, no
magnitude, no distance, and therefore **no scale**:

```
Difference (binary, all-or-nothing)  →  no metric, no magnitude, no scale
   → Q-events (a Q-event IS a before→after difference)   [binary, scale-free]
   → counting measure ρ (a density of weight d)          [scale-covariant by definition]
   → causal order (a scale-invariant partial order)      [scale-free]
   → renormalization invariance → power law (unique RG fixed point)
   → scale-freeness = indifference (AT-F1)               [DERIVED]
```

**Difference alone permits NO preferred scale.** A binary distinction relation cannot single out a
λ: there is nothing in "a differs from b" that has the dimension of length or energy. The scale-
freeness of AT-F1 is the *re-encoding* of Difference's binary, metric-free nature.

---

## 4. Hidden assumptions — counting, actualization, occupancy, normalization

| Ingredient | Scale content | Verdict |
|---|---|---|
| **counting** | ρ is a density (weight d) — covariant by the definition of a measure | scale-covariant, no scale |
| **actualization** | a Q-event IS a difference (binary before→after) | no scale |
| **occupancy** | [4,4,87]/95 — a discrete dimensionless vector | no scale |
| **normalization** | ρ = occupancy/N — dimensionless | no scale |

**No hidden scale.** Every ingredient of the chain is dimensionless or covariant; there is no λ
waiting to be smuggled in.

---

## 5. A / B / C / D

| Determination | Verdict |
|---|---|
| **A) scale-freeness follows from Difference** | **YES.** Difference is binary (metric-free), so the primitives derived from it carry no scale. |
| **B) scale-freeness follows from counting** | **YES.** The counting measure is a density of weight d (covariant), whose unique renormalization-invariant form is the power law. |
| **C) scale-freeness is equivalent to indifference** | **YES (by definition).** AT-F1 *is* "no intrinsic scale" = scale-freeness. |
| **D) scale-freeness remains irreducible** | **REFUTED.** It is DERIVED (conditional) from Difference's binary nature + counting's covariance. |

**Determination: A + B + C — scale-freeness follows from Difference (via counting) and IS
indifference; D is refuted.** The deepest source of AT-F1 is **Difference itself** — its binary,
metric-free nature. Scale-freeness is NOT the final boundary; Difference is.

---

## Theorem

> **Theorem (NP_079).** Scale-freeness (AT-F1) is NOT the final irreducible boundary: it is DERIVED
> from Difference. Difference is a BINARY (all-or-nothing) distinction relation, which carries no
> metric, no magnitude, and no scale; therefore the primitives it grounds — Q-events (a before→after
> difference), the counting measure (a density of weight d, scale-covariant), and the causal order
> (a scale-invariant partial order) — carry no intrinsic scale. Scale-freeness is then the unique
> renormalization-invariant abundance (the power law, the RG fixed point), i.e. AT-F1 = indifference.
> Introducing an intrinsic scale λ (finite) breaks scale-freeness — μ≠1, α≠0, scale-setting structure
> — but λ is excluded by Difference's binary nature (nothing in "a differs from b" has the dimension of
> a length); only λ→∞ (scale decouples) restores self-similarity. **Success criterion: the deepest
> source of AT-F1 is Difference itself — the binary, metric-free founding primitive — and
> scale-freeness is DERIVED (conditional), not the final boundary.** This refines NP_078: the final
> boundary is Difference, not scale-freeness. Classification: scale-freeness DERIVED (conditional on
> Difference's binary nature, QG270/AT-F1); "scale-freeness irreducible" REFUTED; a canonical
> preferred-scale (λ) universe REFUTED (breaks μ=1, α=0). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) State AT-F1 = renormalization invariance. (2) Introduce λ and show it breaks
> μ=1/α=0. (3) Show Difference is binary/metric-free ⇒ no scale. (4) Sweep the hidden assumptions.
> (5) Decide A/B/C/D. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "scale-freeness is the final boundary" | it is derived from Difference's binary nature + counting's covariance (QG270/AT-F1) |
| "Difference permits a preferred scale" | a binary distinction has no metric/magnitude — nothing to set a λ |
| "an intrinsic scale λ is compatible with AT" | finite λ breaks μ=1 (criticality), α=0 (flat rotation), scale-free structure |
| "counting smuggles in a scale" | ρ is a density of weight d — covariant by definition, no preferred λ |
| "the D96 hierarchy sets a scale" | [4,4,87] is a discrete dimensionless spectrum; the abundance law stays equal-per-octave (self-similar) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| scale-freeness follows from Difference | a Difference primitive that carries a metric/magnitude (a "distance of difference") |
| counting is scale-covariant | a counting measure that is not a density of weight d |
| a finite λ breaks μ=1, α=0 | a preferred-scale universe that remains critical and flat-rotating |
| scale-freeness is not irreducible | a derivation of Difference's binary nature from something more primitive |

---

## 8. Classification

| Component | Status |
|---|---|
| scale-freeness = renormalization invariance = indifference (AT-F1) | **DERIVED** (conditional, QG270/AT-F1) |
| counting measure as a density of weight d (covariant) | **DERIVED** (definition of a measure) |
| Difference's binary (metric-free) nature | **BOUNDARY** (the founding primitive, QG270) — the true final boundary |
| "scale-freeness irreducible" | **REFUTED** |
| a canonical preferred-scale (λ) universe | **REFUTED** (breaks μ=1, α=0) |

**Conclusion.** Scale-freeness is **not** the final boundary — it is **DERIVED** from Difference. The
founding primitive is a *binary* distinction relation, which carries no metric and no scale; so the
primitives it grounds (Q-events, the counting measure as a covariant density, the causal order) carry
no intrinsic scale, and scale-freeness follows as the unique renormalization-invariant abundance
(the power law). AT-F1 = indifference = scale-freeness = renormalization invariance, all the same
statement. Introducing a finite intrinsic scale λ would break everything (μ≠1, α≠0, scale-setting
structure), but λ is excluded by Difference's binary nature. **The deepest source of AT-F1 is
Difference itself; the true final boundary is Difference, not scale-freeness — refining NP_078.** No
new primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_078_Tests.cs` → `Y_NP_079_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_079_PowerLawScaleCovariant` | n(2R)/n(R) = 2⁻ᵖ constant | ✅ |
| `Y_NP_079_GaussianScaleSetting` | Gaussian ratio R-dependent | ✅ |
| `Y_NP_079_CountingCovariance` | N = ∫ρ dV invariant (density of weight d) | ✅ |
| `Y_NP_079_LambdaBreaksScaleFreeness` | finite λ ⇒ μ≠1, α≠0 | ✅ |
| `Y_NP_079_LambdaInfinityRecovers` | λ→∞ ⇒ scale-free limit | ✅ |
| `Y_NP_079_DifferenceBinaryMetricFree` | Difference carries no metric/scale | ✅ |
| `Y_NP_079_HiddenAssumptionsSweep` | counting/actualization/occupancy/normalization all scale-free | ✅ |
| `Y_NP_079_ABCD` | A+B+C YES; D REFUTED | ✅ |
| `Y_NP_079_Classification` | scale-freeness DERIVED; Difference BOUNDARY | ✅ |
| `Y_NP_079_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_079"`

---

## References

- AT-QG: QG001 (Difference), QG007 (critical branching), QG011 (discrete tick), QG194 (matter =
  deficit), QG222 (native dynamics), QG270 (Distinction Origin), QG40 (Final Boundary Audit).
- AT-F1 (`ATF_IndifferencePrinciple.md`).
- ResearchY-NP_070 (criticality), NP_078 (α=0 necessity).
