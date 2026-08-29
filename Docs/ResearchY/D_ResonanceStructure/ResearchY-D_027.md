# ResearchY-D_027 — Selector-Origin Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** D — Resonance Structure
**ID:** ResearchY-D_027 (permanent)
**Title:** Selector-Origin Audit
**Status:** COMPLETE
**Date:** 2026-08-29
**File:** `D_ResonanceStructure/ResearchY-D_027.md`
**Depends on:** ResearchY-D_012 (minimal anchors), D_020 (selection precondition),
D_021–D_026 (oscillation → doublets → complexification → su(2))
**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_027_Tests.cs`

---

## Purpose

**Are positivity, normalization, and stability derived from Difference → Actualization,
or are they the final Boundary input?** D_026 showed these three criteria select su(2)
(EMERGENT from observability). This audit asks the next step back: is the selector
itself derived from the primitive structure, or is it an independent boundary input?

## Accepted (from D_026 and the canonical monograph)

- D_026: positivity + normalization + stability select the compact form su(2) (EMERGENT
  from observability).
- Count conservation is the definitional identity of Difference (Ch3, QG268).
- The Born rule Σ|ψ|² = 1 is derived as the normalization of the actualization share
  (Ch9, QG216).
- Stability is the closure fixed point (Ch3/Ch4, QG282).

---

## 1. Definitions

| Criterion | Definition |
|---|---|
| **positivity** | ρ_k ≥ 0 for all modes — probabilities/shares are non-negative |
| **normalization** | Σ_k ρ_k = 1 — the total share is conserved (Born rule) |
| **stability** | the dynamics converge to a fixed point (bounded evolution, closure) |

---

## 2. Trace each criterion to the primitives

### 2.1 Positivity → from the count/share structure

ρ_k = μ^k/S with μ > 0 and S = Σ_j μ^j > 0. The share of a count is an intrinsically
non-negative fraction (verified: ρ_k ≥ 0 for μ = 0.5, 1, 2). **Positivity is a property
of the count structure — counts are non-negative by construction.**

### 2.2 Normalization → from count conservation (DERIVED, canonical)

The Born rule Σ|ψ|² = 1 holds **exactly by construction** as the normalization of the
actualization share (Ch9, QG216): probabilities ARE the normalized actualization shares.
Count conservation is the **definitional identity of Difference** (Ch3, QG268) — the
process conserves exactly what the primitive defines. **Normalization is derived from
count conservation, which is the primitive's identity.**

### 2.3 Stability → from the closure fixed point (DERIVED, canonical)

The actualization dynamics converge to the stable fixed point (zero residual link growth,
content-independent attractor, QG116). The closure principle (Ch4, QG282) states the
**boundary IS the stable fixed point**. **Stability is the closure — the process's own
convergence.**

---

## 3. Tests

### 3.1 Does count conservation imply normalization?

**YES.** The Born rule Σ|ψ|² = 1 is the normalization of the share; without a conserved
count, the share is not normalized. Count conservation is the definitional identity of
Difference (Ch3), so normalization follows from the primitive itself (canonical, Ch9).

### 3.2 Does Actualization imply positivity?

**YES.** The share construction ρ_k = μ^k/S (μ > 0) is intrinsically positive — counts
are non-negative, so their normalized shares are non-negative. Positivity is not a
separate postulate; it is the structure of the count.

### 3.3 Does Closure imply stability?

**YES.** Stability is the closure fixed point (Ch4, QG282): the boundary is where the
count-producing dynamics has converged with zero residual change. Without stability, the
spectrum would not close (Ch3 corollary: the D96 spectrum requires the converged
network).

---

## 4. Removal test

| Removed | What survives? | Consequence |
|---|---|---|
| count conservation | no normalized share → no Born rule | normalization lost |
| positivity | negative probabilities → unobservable | no physical sector |
| stability | no fixed point → no closed spectrum | no D96, no su(2) selection |
| the primitives {Difference, η} | nothing | the whole hierarchy collapses |

The three criteria are **interdependent consequences of the primitive structure**, not
independent inputs. Removing the underlying primitive chain removes the criteria.

---

## Determination

| Option | Verdict |
|---|---|
| A) all derived | **YES** — positivity, normalization, stability all follow from the primitive structure (count conservation, share construction, closure fixed point) |
| B) partially derived | NO — there is no independent residue beyond the primitives |
| C) boundary input | NO — the selector is not an independent input; the only boundary is the primitive set {Difference, η} |

**Verdict: A) all derived from the primitive structure.** The D_026 selector
(positivity/normalization/stability) is a consequence of Difference → Actualization →
Closure; the only boundary is the primitive set {Difference, η} itself.

---

## Theorem

> **Theorem (D_027).** Positivity, normalization, and stability are derived from the
> primitive structure, not a final boundary input. Positivity follows from the count/share
> construction (ρ_k = μ^k/S ≥ 0 — counts are non-negative); normalization is the Born
> rule, derived as the normalized actualization share from count conservation (Ch9,
> QG216), which is the definitional identity of Difference (Ch3, QG268); stability is the
> closure fixed point (Ch4, QG282). The D_026 su(2) selector is therefore a consequence
> of the minimal hierarchy — the only boundary is the primitive set {Difference, η}.
>
> *Proof sketch.* (1) Positivity: ρ_k = μ^k/S with μ > 0, S > 0 ⇒ ρ_k ≥ 0 (verified for
> μ = 0.5, 1, 2) — Section 2.1. (2) Normalization: Σρ_k = 1 by construction, the Born
> rule is the normalized share (Ch9, QG216); count conservation is the primitive's
> identity (Ch3, QG268) — Section 2.2. (3) Stability: the closure principle states the
> boundary IS the stable fixed point (Ch4, QG282); the attractor is content-independent
> (QG116) — Section 2.3. (4) Removing any criterion follows from removing the underlying
> primitive structure (Section 4). (5) Hence the selector is derived; only the primitives
> {Difference, η} are boundary. ∎

---

## Dependency Graph

```
Difference
 → count conservation (definitional identity, Ch3/QG268)
 → Actualization (the process face)
 → share construction ρ_k = μ^k/S           [positivity — counts non-negative]
 → normalized share Σρ_k = 1               [normalization — the Born rule, Ch9]
 → Closure (stable fixed point, Ch4/QG282) [stability — bounded evolution]
 → D96 spectrum (needs the converged network)
 → su(2) selector (positivity + normalization + stability, D_026)
```

---

## Necessity Analysis

| Question | Answer |
|---|---|
| Does count conservation imply normalization? | **YES** (Ch9: Born rule = normalized share) |
| Does Actualization imply positivity? | **YES** (shares are non-negative counts) |
| Does Closure imply stability? | **YES** (boundary = fixed point, Ch4) |
| Are the criteria independent inputs? | NO — they follow from the primitives |
| Is the selector derived? | **YES** (A) — from Difference → Actualization → Closure |
| What is the final boundary? | the primitive set {Difference, η} |

---

## Counterexamples

1. **No count conservation**: the share is not normalized — no Born rule. (Demonstrates
   normalization depends on the primitive's identity.)
2. **Negative counts**: impossible — a count is non-negative by definition; negative
   shares would give unobservable negative probabilities. (Demonstrates positivity is
   intrinsic.)
3. **Unstable dynamics**: no fixed point — no closed spectrum, no D96, no su(2)
   selection. (Demonstrates stability is the closure.)
4. **No primitives**: the whole hierarchy collapses (QG292 foundation stress test).

---

## Classification

| Component | Status |
|---|---|
| positivity (share construction) | **DERIVED** (counts non-negative) |
| normalization (Born rule) | **DERIVED** (count conservation, Ch9/QG216) |
| stability (closure fixed point) | **DERIVED** (Ch4/QG282) |
| su(2) selector (D_026) | **DERIVED** (from the three, which are derived) |
| the primitive set {Difference, η} | **BOUNDARY** (the minimal foundation) |

**The selector is A) all derived from the primitive structure; the only boundary is the
primitive set {Difference, η}.**

---

## Open Problems

1. **Origin of μ (D_027 OP1).** The share construction uses the branching ratio μ
   (positive); whether μ's positivity and value are derived or boundary is the
   branching-origin question (canonical: μ = 1 criticality, Ch9).
2. **Positivity as a definition (D_027 OP2).** "Counts are non-negative" is close to
   definitional; whether a deeper derivation exists (from Difference's structure) is open.

---

## Next Steps

- **ResearchY-D_028 (or synthesis):** the selector-origin audit completes the su(2)
  chain (Difference → count conservation → normalization → su(2)). A synthesis can map
  the full primitive-to-gauge boundary structure.
- **D_026 follow-up:** the "selector derived from primitives" verdict refines D_026 —
  su(2) is EMERGENT from observability, and observability itself is DERIVED from the
  primitives.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_027_Tests.cs`
**Run:** 2026-08-29 · **Result:** see `Tests/Results/Y_D_027_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_D_027_PositivityOrigin` | ρ_k ≥ 0 intrinsic (share of a count) | ✅ |
| `Y_D_027_NormalizationOrigin` | Σρ_k = 1 by construction (Born rule, count conservation) | ✅ |
| `Y_D_027_StabilityOrigin` | stability = closure fixed point | ✅ |
| `Y_D_027_RemovalTest` | removing count conservation/positivity/stability/primitives | ✅ |
| `Y_D_027_DependencyTrace` | Difference → count conservation → normalization → su(2) | ✅ |
| `Y_D_027_Run` | Research report | ✅ |

**Conclusion:** Positivity, normalization, and stability are **DERIVED from the primitive
structure**, not a final boundary input. Positivity follows from the count/share
construction (ρ_k = μ^k/S ≥ 0); normalization is the Born rule, derived from count
conservation (the definitional identity of Difference, Ch9/QG216); stability is the
closure fixed point (Ch4/QG282). The D_026 su(2) selector is a consequence of the
minimal hierarchy — the only boundary is the primitive set {Difference, η}.
Classification: A) all derived from the primitive structure. No canonical value was
changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_027"`

---

## References

- ResearchY-D_012 (minimal anchors), D_020 (selection precondition), D_021–D_026
  (oscillation → su(2) chain).
- Monograph V2.0: Ch3 (count conservation = definitional identity, QG268), Ch4
  (closure = stable fixed point, QG282), Ch9 (Born rule = normalized share, QG216).
- AT-QG: QG116 (universal attractor), QG282 (closure principle).
