# ResearchY-B_002 — Origin of π Value Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** B — Circular Geometry
**ID:** ResearchY-B_002 (permanent)
**Title:** Origin of π Value Audit
**Status:** COMPLETE
**Date:** 2026-08-28
**File:** `B_CircularGeometry/ResearchY-B_002.md`
**Depends on:** ResearchY-A_001…A_005, ResearchY-B_001
**Test suite:** `AT.Tests/ResearchY/B_CircularGeometry/Y_B_002_Tests.cs`

---

## Purpose

Determine whether the **numerical value** π = 3.141592653… can emerge from the canonical
framework — or whether only its **geometric role** emerges while its value remains an
irreducible boundary.

## Accepted (established results)

- Difference ≈ localized mode excitation (A_002).
- Zero mode = undisturbed background (A_002).
- Closure is necessary (B_001 RQ1).
- Circular geometry is unavoidable within the accepted D96 class (B_001 RQ4).
- 2π emerges as the minimal phase-closure condition (B_001 RQ5).
- π emerges in role, not in numerical value (B_001 RQ6).
- Spectral projection is derived from the attractor (A_005).
- 2π appears as the ring-closure periodicity constant in the spectral layer (A_001 R7).

---

## Research Questions

1. What exactly remains unexplained after B_001?
2. Is π merely inherited from circle geometry?
3. Can π be reconstructed from: closure, phase, graph structure, eigenmodes,
   circumference/radius relations?
4. Does C96 contain approximate π estimators?
5. Does N/(2π) appearing in A_001 indicate emergence or only measurement?
6. Can π arise from the Fourier basis itself?
7. Is π fundamentally numerical or fundamentally geometric in AT?
8. Is there any path Difference → Actualization → Closure → Spectrum → π?
9. Does circular closure require π or only 2π?
10. Is QG291/QG196 still correct that π remains a boundary?

---

## Canonical References

- **Ch2/Ch6** π is a boundary constant (QG291); the spectrum is the eigenspectrum of the
  integer-matrix graph Laplacian (Ch5/Ch6).
- **Ch9** Phase lattice θ_k = 2πk/N; roots of unity e^{2πik/N}.
- **QG185/QG196** Bekenstein 1/4 impossibility: exact 1/4 requires imported 2π.
- **ResearchY-B_001** closure, 2π, π-role results.
- **ResearchY-A_001/A_005** spectral-layer 2π; projection derived from the attractor.

---

## Assumptions

1. Canonical AT V2.0 is ground truth; nothing here modifies it.
2. "π emerges" = some finite canonical construction outputs the value π.
3. The canonical content (the D96 spectral constants) consists of algebraic numbers:
   the graph Laplacian L is an integer matrix, so its eigenvalues are algebraic
   integers, and every finite combination of them is algebraic.
4. π is transcendental (Lindemann–Weierstrass, 1882): no algebraic number equals π.
5. No fitted constants; no new primitives; Euclidean geometry is not assumed unless
   derived.

---

## Research Conclusions

**RQ1 — What remains unexplained after B_001?** B_001 established that π's *role* (the
circle constant C/D of the closed ring) emerges, while its *value* does not. What remains
unexplained is precisely the **numerical value** π = 3.141592653…: is it an output of the
framework or an irreducible input?

**RQ2 — Is π merely inherited from circle geometry?** PARTLY. The *role* is inherited from
the closed-ring geometry (B_001: C/D = π identity holds for the ring). But the *value* is
not inherited from the D96 structure — it is carried by the mathematical constant π, which
the framework does not compute.

**RQ3 — Can π be reconstructed from closure, phase, graph structure, eigenmodes, or
circumference/radius relations?** NO for the value. Each candidate:
- **Closure:** closure gives the integer N = 96 (a count) — an integer, not π.
- **Phase:** the phase lattice θ_k = 2πk/N uses π as the parametrization of the roots of
  unity e^{2πik/N}, whose *values* are algebraic (they satisfy z^N = 1); π's value is not
  produced.
- **Graph structure:** the graph Laplacian is an integer matrix; its eigenvalues are
  algebraic integers (standard theorem: eigenvalues of an integer matrix are algebraic).
- **Eigenmodes:** the Fourier basis is the roots-of-unity basis (algebraic); no
  eigenvalue is transcendental.
- **Circumference/radius:** C/D = π is the *definitional identity* of the radius
  (r = N/2π was introduced as N/(2π)); it is a tautology, not a derivation of the value.

**RQ4 — Does C96 contain approximate π estimators?** Yes — several D96 ratios are near π
(span/2 ≈ 3.2013, dev 1.9%; √10 ≈ 3.1623, dev 0.66%; sm/√sm2 ≈ 6.2778 ≈ 2π, dev 0.09%).
But (a) none equals π exactly, and (b) **selecting any one of them as "the derivation" is a
fit** — choosing which ratio "is π" from a menu of near-misses is exactly the target-driven
selection the audit forbids. Approximants are coincidences, not derivations.

**RQ5 — Does N/(2π) indicate emergence or only measurement?** Only **measurement**. The
quantity r = N/(2π) = 15.279 is a radius *defined* by dividing the circumference by 2π — a
unit/convention choice, not an emergent length. The ladder radii 6.0–17.333 bracket 15.279
by coincidence of range, not by derivation. A_001's observation that 15.279 lies inside the
ladder-radii interval is a numerical coincidence of two independent constructions, not an
emergence of π.

**RQ6 — Can π arise from the Fourier basis itself?** NO. The Fourier basis of the circulant
is the roots-of-unity basis z_k = e^{2πik/96}, all of which are algebraic (z_k^96 = 1). The
basis is determined by the *algebraic* equation z^N = 1 — the parameter 2π/N is a
conventional parametrization of the roots, not a source of the constant π. The Fourier
basis is algebraic; π is transcendental; the basis cannot produce π.

**RQ7 — Is π fundamentally numerical or geometric in AT?** **Geometric in role, boundary
in value.** π functions as the geometric constant of the closed ring (role, emerges with
closure); numerically it is an irreducible boundary constant (QG291). The framework is
geometric/algebraic; π's value is transcendental and outside its closure.

**RQ8 — Is there any path Difference → Actualization → Closure → Spectrum → π?** NO. The
chain produces: Difference → actualization → closure (N=96, integer) → graph (integer
matrix) → Laplacian → algebraic eigenvalues → algebraic spectral constants. Every output
of the chain is algebraic. π is transcendental. **No algebraic chain can output a
transcendental value.** The only place π appears is as a *parameter in the formula*
(cos 2πdk/96), where it parametrizes algebraic roots of unity — a role, not an output.

**RQ9 — Does circular closure require π or only 2π?** **Only 2π.** Closure is the fixed
point N = 96 (an integer count); the phase closure is θ_N = 2π (the full-cycle constant,
which emerges as the minimal phase closure, B_001). The value of π itself is not required
by any closure condition — only the circle constant's role (as the half of 2π, or as the
C/D ratio of the already-closed ring).

**RQ10 — Is QG291/QG196 still correct?** YES — and the audit *strengthens* them with an
arithmetic argument. QG291 declares π a boundary; QG196 proves the Bekenstein 1/4 cannot be
derived without importing 2π. The present audit adds: **the canonical content is algebraic
and π is transcendental, so no finite canonical construction can output π's value.** π is a
genuine boundary of the framework, not a derivable output.

---

## Required Output

### Candidate derivation paths

| Path | Construction | Result | Verdict |
|---|---|---|---|
| closure | N = 96 (integer) | 96 ≠ π | FAILS |
| phase | θ_k = 2πk/N (roots of unity) | algebraic values; π is a parameter | FAILS (role only) |
| graph | L integer matrix → algebraic eigenvalues | no eigenvalue = π | FAILS |
| eigenmodes | Fourier basis = roots of unity | algebraic basis | FAILS |
| circumference/radius | C/D = π (definitional) | tautology | FAILS (role only) |
| spectral ratios | span/2, √10, sm/√sm2 | near-misses (0.09–2%) | FAILS (fit if selected) |

### Failed derivation paths

1. **Algebraic construction.** Any finite combination of D96 spectral constants is
   algebraic; π is transcendental ⇒ cannot equal π. (Decisive.)
2. **Approximant selection.** Choosing a near-miss ratio as "the" π is target-driven
   selection (a fit) — forbidden.
3. **Fourier-basis derivation.** The basis is algebraic (roots of unity); it cannot
   output a transcendental constant.

### Dependency graph

```
Difference
  → Actualization
  → Attractor (closure, N = 96) — integer
  → Graph C96 — integer matrix
  → Laplacian L — eigenvalues algebraic integers
  → Eigenbasis — algebraic (roots of unity)
  → Spectral constants — algebraic
  → π value — TRANSCENDENTAL ⇒ OUTSIDE the graph
```

### Compatibility with canonical AT

| Claim | Compatible? | Notes |
|---|---|---|
| π's role emerges (B_001) | YES | C/D = π identity for the closed ring |
| π's value is a boundary | YES | QG291 consistent |
| Bekenstein 1/4 needs 2π | YES | QG196 consistent; not overtaken |
| the spectrum is algebraic | YES | integer-matrix Laplacian (Ch5/Ch6) |
| closure needs only 2π | YES | θ_N = 2π minimal phase closure (B_001) |

### Contradiction analysis

| # | Claim | Contradiction | Resolution |
|---|---|---|---|
| 1 | "π value derived from the spectrum" | eigenvalues are algebraic; π transcendental | impossible (arithmetic) |
| 2 | "π = a D96 ratio" | no ratio equals π; selection would be a fit | approximants are coincidences |
| 3 | "N/(2π) shows π emerges" | r = N/2π is definitional (unit choice) | measurement, not emergence |
| 4 | "π emerges from the Fourier basis" | the basis is algebraic (roots of unity) | the basis cannot output π |

### Boundary analysis

π's **value** is an irreducible boundary of the framework:
- **Arithmetic:** the canonical content is algebraic; π is transcendental — no finite
  construction outputs it.
- **Geometric:** π's role (circle constant) is selected by closure, but the constant's
  value is not computed.
- **QG291/QG196:** confirmed and strengthened (arithmetic argument added).

### Final verdict

> **BOUNDARY.** The value π = 3.141592653… is an irreducible boundary constant of the
> canonical framework. Its *role* (the circle constant of the closed ring, C/D = π)
> emerges with circular closure; its *numerical value* is transcendental and cannot be
> produced by the algebraic content of the framework (Difference → Actualization →
> Closure → Spectrum). QG291/QG196 remain correct.

---

## Success Criterion

**Only π's role emerges; its value remains an irreducible boundary.**

The numerical value of π does not emerge from the canonical framework. The chain
Difference → Actualization → Closure → Spectrum is algebraic; π is transcendental; no
path from the framework outputs the value. π is geometric in role (with closure) and
boundary in value (irreducible).

---

## Open Problems

1. **2π vs π as the primary constant (B_002 OP1).** Closure requires only 2π (the
   phase-cycle constant). Is 2π also a boundary in value, or does the framework
   distinguish the full-cycle constant from the half-cycle constant? (The value of 2π is
   equally transcendental; the question is which *role* is primary.)
2. **Transcendence as boundary principle (B_002 OP2).** Does the algebraicity of the
   spectrum generalise to a principle: *the framework outputs only algebraic values;
   any transcendental constant entering physics is a boundary*? (Candidate principle for
   the boundary layer.)
3. **Roots of unity as the true spectral basis (B_002 OP3).** The Fourier basis is the
   roots-of-unity basis (algebraic). Is the parametrization by 2π/N a *convention*, and
   the algebraic equation z^N = 1 the *content*? (Supports OP2.)

---

## Next Steps

- **ResearchY-B_003 (Origin of 2π):** separate the full-cycle constant (2π, emergent in
  role, B_001) from the half-cycle constant (π, boundary in value, this audit); test
  whether 2π's value is equally a boundary.
- **ResearchY-D_001 (D96 Resonance Audit):** the algebraic spectrum is the resonance
  content; the π boundary is independent of it.

---

## Result Summary

**Test suite:** `AT.Tests/ResearchY/B_CircularGeometry/Y_B_002_Tests.cs`
**Run:** 2026-08-28 · **Result:** see `Tests/Results/Y_B_002_Result.md`

| Test | Verifies | Result |
|---|---|---|
| `Y_B_002_PiFromClosure` | closure gives integer N=96, not π | ✅ |
| `Y_B_002_PiFromCircle` | C/D=π is the definitional radius identity (measurement, not derivation) | ✅ |
| `Y_B_002_PiFromFourierBasis` | the Fourier basis is algebraic (roots of unity) | ✅ |
| `Y_B_002_PiFromSpectrum` | L integer matrix → algebraic eigenvalues; no eigenvalue = π | ✅ |
| `Y_B_002_PiApproximants` | natural ratios are near-misses; none equals π; selection = fit | ✅ |
| `Y_B_002_BoundaryConsistency` | QG291/QG196 consistent; π value is a boundary | ✅ |
| `Y_B_002_Run` | Research report | ✅ |

**Verdict: BOUNDARY.** π's value is an irreducible boundary (algebraic framework vs
transcendental constant); only its role emerges. No canonical value was changed.

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_B_002"`

---

## References

- Monograph V2.0: Ch2 (π boundary, QG291), Ch5/Ch6 (integer-matrix Laplacian, spectrum),
  Ch9 (phase lattice).
- AT-QG: QG185/QG196 (Bekenstein boundary), QG291 (π boundary).
- ResearchY-B_001 (π role), A_001 (R7), A_005 (algebraic spectral projection).
