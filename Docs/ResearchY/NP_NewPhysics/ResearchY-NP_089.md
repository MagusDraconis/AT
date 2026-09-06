# ResearchY-NP_089 — Rotational Symmetry Emergence Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_089 (permanent)
**Title:** Rotational Symmetry Emergence Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_089.md`
**Depends on:** ResearchY-NP_087 (nuclear structure), NP_088 (D96 network geometry), AT-QG QG114
(3D connectivity classes), QG197 (2D→3D bridge), QG2 (d ≥ 3)
**Test suite:** `AT.Tests/ResearchY/NP_089_Tests.cs`

---

## Purpose

NP_088 found the D96 ⊗ D96 ⊗ D96 network is a CUBIC lattice (octahedral O_h), not rotational O(3).
NP_089 asks the follow-up: **can the cubic network generate EFFECTIVE O(3) at large scale through
coarse-graining?** Program: (1) start from D96 / D96⊗D96 / D96⊗D96⊗D96; (2) compute connectivity
classes, degeneracies, and DOS; (3) test coarse-graining at small / medium / large scale; (4)
determine whether the cubic anisotropy survives or averages out; (5) search for emergent spherical
harmonics / O(3) / 2l+1 degeneracies; (6) re-evaluate NP_087. **Success criterion:** determine
whether rotational symmetry is A) absent, B) emergent, C) approximate only, or D) exact in the
continuum limit. No new primitives; canonical AT unchanged.

---

## 1. The symmetry of the coupled rings

| Configuration | Symmetry | DOS exponent |
|---|---|---|
| 1 × D96 | O(2) (mirror pairs) | p = 1 |
| 2 × D96 | D₄ (square) | p = 2 |
| 3 × D96 | **O_h (cubic/octahedral)** | p = 3 |

The DOS *dimension* rises to 3, but the *symmetry* rises only to the octahedral group O_h — not to
the full rotational group O(3). The question is whether large-scale coarse-graining can close that
gap.

---

## 2. The dispersion — where O(3) does and does not appear

The free Laplacian on a cubic lattice has the dispersion (a = 1):

```
ω²(k) = Σ_i 2(1 − cos k_i)  =  k² − (k_x⁴ + k_y⁴ + k_z⁴)/12 + …
```

- The **leading term k² = k_x² + k_y² + k_z²** is ISOTROPIC — it depends only on |k|, so it is
  O(3)-invariant.
- The **first correction −(k_x⁴ + k_y⁴ + k_z⁴)/12** is the CUBIC invariant — it depends on the
  direction, not just |k|, so it BREAKS O(3) down to O_h.

| | k | ω² along [100] | ω² along [111] | relative anisotropy |
|---|---|---|---|---|
| | 0.3 | 0.0893 | 0.0898 | **0.5%** (O((ka)²)) |

The anisotropy is **suppressed but never zero**: it scales as O((ka)²), so it is small at long
wavelength (ka ≪ 1) but present at every finite scale.

---

## 3. Coarse-graining — small / medium / large scale

| Scale | What happens to O(3) |
|---|---|
| **small (ka ~ 1)** | the cubic anisotropy is order-1; O(3) is strongly broken |
| **medium (ka ~ 0.3)** | the anisotropy is ~0.5% (O((ka)²)); O(3) is approximate |
| **large (ka → 0)** | the anisotropy → 0 as (ka)², but only reaches 0 exactly at a = 0 |

**The cubic anisotropy does NOT average out.** Coarse-graining suppresses it (by (ka)²), but a
finite lattice never eliminates it. Only the exact continuum limit (lattice spacing a → 0, reached
never, because the theory is DISCRETE with N = 96) would restore exact O(3).

---

## 4. Emergence of spherical harmonics / O(3) / 2l+1

| Object | Emerges? |
|---|---|
| isotropic dispersion (leading k²) | **YES — to leading order** (approximate O(3)) |
| exact 2l+1 spherical-harmonic degeneracies | **NO** — the cubic correction splits them (l = 2 (5) → 2 + 3; l = 3 (7) → 1 + 3 + 3) |
| exact O(3) | **NO** — only in the unattained a → 0 continuum |

The 2l+1 degeneracies of the isotropic harmonic oscillator — the very degeneracies that the
nuclear shell model uses to build the magic numbers — are split by the cubic correction at every
finite lattice spacing.

---

## 5. Re-evaluate NP_087

| NP_087's conclusion | Verdict after NP_089 |
|---|---|
| "nuclear structure is missing" | **SURVIVES.** The exact 2l+1 degeneracies (and hence the exact magic numbers) require exact O(3), which the cubic lattice supplies only approximately — the cubic correction splits every multiplet. |
| "the reason" | **REFINED AGAIN.** Not "1D vs 3D" (NP_087) and not merely "cubic ≠ spherical" (NP_088), but "O(3) is only approximate: the cubic anisotropy is suppressed as (ka)² but never eliminated." |

Nuclear structure remains missing: the shell closures would be smeared and shifted by the residual
cubic anisotropy, so the exact magic numbers are not reproduced.

---

## 6. A / B / C / D

| Determination | Verdict |
|---|---|
| **A) absent** | **NO.** O(3) is present to leading order (the isotropic k² term). |
| **B) emergent** | **PARTIAL.** O(3) emerges approximately at large scale, but not exactly. |
| **C) approximate only** | **YES — the answer.** O(3) is approximate: the cubic anisotropy survives as O((ka)²) corrections. |
| **D) exact in the continuum limit** | **YES in principle, but NOT REACHED.** Exact O(3) requires a → 0; the theory is DISCRETE (finite N = 96), so it never attains the continuum. |

**Determination: C — rotational symmetry is approximate only.** It emerges to leading order at
large scale, but the cubic anisotropy never fully averages out; exact O(3) is the unattained
continuum idealization.

---

## Theorem

> **Theorem (NP_089).** Rotational symmetry O(3) is only APPROXIMATELY emergent in the cubic D96
> network. The free lattice dispersion ω² = k² − (k_x⁴ + k_y⁴ + k_z⁴)/12 + … is isotropic to
> leading order (the k² term, O(3)-invariant) but the first correction (the cubic invariant
> k_x⁴+k_y⁴+k_z⁴) BREAKS O(3) down to O_h; the anisotropy scales as O((ka)²), so it is suppressed
> but never zero at any finite lattice spacing. Since the theory is DISCRETE (N = 96), the exact
> continuum limit (a → 0, where O(3) would be exact) is never reached. Therefore the exact 2l+1
> spherical-harmonic degeneracies — the building blocks of the nuclear magic numbers — are split by
> the cubic correction at every scale, and nuclear structure remains missing. Proof: (1) Symmetry
> (Section 1): the tensor product reaches O_h, not O(3). (2) Dispersion (Section 2, verified): the
> leading term is isotropic, the cubic correction is not; the relative anisotropy is O((ka)²) (~0.5%
> at ka = 0.3). (3) Coarse-graining (Section 3): the anisotropy is suppressed but never eliminated.
> (4) Degeneracies (Section 4): the 2l+1 multiplets are split (5 → 2+3, 7 → 1+3+3). (5)
> Re-evaluation (Section 5): NP_087's missing verdict survives, refined. (6) Determination
> (Section 6): C (approximate only); A refuted, B partial, D unattained. **Success criterion:
> rotational symmetry is C — approximate only — emergent to leading order but never exact, so
> nuclear structure remains missing.** Classification: the approximate O(3) EMERGENT (leading-order
> isotropy); exact O(3) BOUNDARY (the unattained continuum limit); the nuclear-shell rescue REFUTED
> (the 2l+1 degeneracies are split). No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Symmetry of the tensor product. (2) Expand the dispersion. (3) Test
> coarse-graining. (4) Trace the 2l+1 splitting. (5) Re-evaluate NP_087. (6) Decide A–D. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "O(3) is exact at large scale" | the cubic correction k_x⁴+k_y⁴+k_z⁴ is O((ka)²), never zero for a > 0 |
| "the cubic anisotropy averages out" | coarse-graining suppresses it, but a finite lattice cannot eliminate it |
| "the continuum limit is reached" | the theory is discrete (N = 96); a → 0 is never attained |
| "the 2l+1 degeneracies survive" | the cubic correction splits them (5 → 2+3, 7 → 1+3+3) |
| "nuclear structure is rescued" | the magic numbers need exact 2l+1 + spin-orbit, which the approximate O(3) does not give |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| O(3) is approximate only | an exactly isotropic dispersion on a finite cubic lattice (no cubic correction) |
| the anisotropy never vanishes | a finite lattice whose dispersion has no k⁴ anisotropy |
| nuclear structure stays missing | an exact magic-number closure derived from the cubic lattice dispersion |
| the continuum is unattained | the theory reaching a → 0 (infinite N) as its actual, non-idealized limit |

---

## 9. Classification

| Component | Status |
|---|---|
| leading-order isotropic dispersion (approximate O(3)) | **EMERGENT** (at large scale) |
| exact O(3) in the continuum limit (a → 0) | **BOUNDARY** (unattained, discrete theory) |
| exact 2l+1 degeneracies / magic numbers from the cubic lattice | **REFUTED** (split by the cubic correction) |
| NP_087's "nuclear structure missing" | **SURVIVES** (refined: O(3) approximate only) |

**Conclusion.** Rotational symmetry O(3) is **approximate only (C)** in the cubic D96 network. The
free dispersion is isotropic to leading order (k²), so O(3) is *approximately emergent* at large
scale; but the cubic correction (k_x⁴+k_y⁴+k_z⁴) breaks O(3) with an anisotropy of order (ka)² that
is suppressed yet never vanishes for a finite lattice. Because the theory is discrete (N = 96), the
exact continuum limit — where O(3) would be exact — is never reached. The exact 2l+1
spherical-harmonic degeneracies that the nuclear shell model needs are therefore split at every
scale, and **nuclear structure remains missing** (NP_087/088 survive, refined to "O(3) is only
approximate"). No new primitive; canonical AT unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_089_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_089_SymmetryOfTensorProduct` | 1×→O(2), 2×→D₄, 3×→O_h (not O(3)) | ✅ |
| `Y_NP_089_DispersionIsotropicLeading` | k² term isotropic; cubic term breaks O(3) | ✅ |
| `Y_NP_089_AnisotropyNeverVanishes` | O((ka)²), suppressed but nonzero | ✅ |
| `Y_NP_089_ContinuumUnattained` | discrete N=96 ⇒ a→0 never reached | ✅ |
| `Y_NP_089_DegeneraciesSplit` | 5 → 2+3, 7 → 1+3+3 | ✅ |
| `Y_NP_089_ABCD` | C (approximate only); A refuted, B partial, D unattained | ✅ |
| `Y_NP_089_ReevaluateNP087` | missing verdict survives, refined | ✅ |
| `Y_NP_089_Classification` | approximate O(3) EMERGENT; exact O(3) BOUNDARY; rescue REFUTED | ✅ |
| `Y_NP_089_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_089"`

---

## References

- ResearchY-NP_087 (nuclear structure), NP_088 (D96 network geometry).
- AT-QG: QG114 (3D connectivity classes), QG197 (2D→3D bridge), QG2 (d ≥ 3).
