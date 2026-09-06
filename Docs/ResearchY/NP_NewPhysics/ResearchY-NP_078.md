# ResearchY-NP_078 — Alpha=0 Necessity Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_078 (permanent)
**Title:** Alpha=0 Necessity Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_078.md`
**Depends on:** AT-QG QG206 (α=0 origin), QG184 (M ∝ R), QG7 (critical branching, three
criteria), QG155 (octave organization, [4,4,87]), QG194/195 (deficit matter), QG222 (native
dynamics), AT-F1 (indifference principle), G4-RHO Phase 0 (dynamical origin of ρ),
ResearchY-NP_070 (criticality μ=1), NP_077 (structure formation)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_078_Tests.cs`

---

## Purpose

NP_070 established μ=1 is DERIVED (unique) conditional on scale-freeness, and NP_077 found the
deficit forms structure only with the extra α=0 assumption. NP_078 asks **why α=0 specifically,
and not α≠0**. Program: (1) scan α ∈ [−2, +2]; (2) measure stability, criticality, halo profile,
rotation curve, and structure growth; (3) determine whether α=0 is derived / attractor / boundary
/ unique fixed point; (4) locate the earliest source of the α=0 condition. **Success criterion:**
determine whether scale-freeness is itself derived or the final boundary. No new primitives;
canonical AT unchanged.

---

## 1. The α-scan — [−2, +2]

The abundance deficit is m(r) ∝ r^(−α); the field is a ∝ r^(−α−1); the rotation-curve proxy is
v² = r·|a| ∝ r^(−α) (QG206). The enclosed-mass exponent is 1 − α (QG206 §2.5).

| α | v² slope (−α) | rotation curve | M(r) exponent | scale-free (equal/octave)? |
|---|---|---|---|---|
| −2.0 | +2.0 | steeply rising (outer-dominant) | +3.0 | NO (diverges) |
| −1.0 | +1.0 | rising | +2.0 | NO |
| −0.6 | +0.6 | rising | +1.6 | NO |
| −0.3 | +0.3 | rising | +1.3 | NO |
| **0.0** | **0.0** | **FLAT (v = const)** | **+1.0 (M ∝ R)** | **YES (equal per octave)** |
| +0.3 | −0.3 | falling | +0.7 | NO |
| +0.6 | −0.6 | falling | +1.0 → +0.4 | NO |
| +1.0 | −1.0 | Keplerian-falling | 0.0 | NO |
| +2.0 | −2.0 | steeply falling (core-dominant) | −1.0 | NO (concentrates) |

**Exactly one value — α = 0 — gives a flat rotation curve, the M ∝ R mass profile, and the
equal-deficit-per-octave (scale-free) hierarchy.** Every α ≠ 0 breaks one or more of these.

---

## 2. What α ≠ 0 does — four independent failures

| Measure | α = 0 | α < 0 | α > 0 |
|---|---|---|---|
| **stability** (octave spread) | ~0 (equal) | outer-dominant, diverges | core-dominant, concentrates |
| **criticality** | μ = 1 (marginal) | μ < 1 (extinct) | μ > 1 (runaway) |
| **halo profile** | SIS ρ ∝ r⁻², M ∝ r | rising curve, no flat halo | falling curve, Keplerian |
| **structure growth** | scale-free seed (self-similar) | breaks self-similarity | breaks self-similarity |

For α < 0 the deficit piles into the outer octaves (diverges); for α > 0 it concentrates in the
core; only α = 0 keeps every octave equal — the unique non-diverging, non-concentrating, stable
point (QG206 §2.3). And by NP_070, **μ = 1 ⟺ α = 0**: the branching criticality and the flat-
rotation condition are the *same* scale-freeness.

---

## 3. derived / attractor / boundary / unique fixed point

| Reading | Verdict |
|---|---|
| **derived** | **YES (unique)** — α=0 is uniquely selected by four coincident criteria (flat rotation, stability, criticality, max-entropy). |
| **attractor (dynamical)** | **NO.** No equation of motion produces α=0; G4-RHO Phase 0 shows conservation gives the *repulsive* ρ ∝ r⁻² and scale-freeness a *continuum*. α≠0 does not relax to α=0. |
| **boundary** | **the conditioning input.** Scale-freeness (renormalization invariance, AT-F1) is the single non-derived input. |
| **unique fixed point** | **YES.** α=0 is the unique RG fixed point of the octave renormalization (α≠0 flows away under octave rescaling). |

**Determination: α=0 is DERIVED (unique) as a selection fixed point, conditional on the single
boundary input — scale-freeness (the indifference principle AT-F1).** It is NOT a dynamical
attractor (no dynamics forces it; conservation prefers the wrong sector), but it IS the unique
fixed point of the octave renormalization.

---

## 4. Reconciling QG206 with G4-RHO — "derived" vs "not dynamical"

The corpus contains an apparent tension: QG206 calls α=0 "derived" (ALPHA-ZERO ORIGIN), while
G4-RHO Phase 0 calls α=0 "PREFERRED, not derived from dynamics". The resolution is precise:

| Claim | Verdict |
|---|---|
| α=0 is derived by **selection criteria** (stability + scale-freeness + criticality + max-entropy) | **YES** (QG206/QG7/NP_070) |
| α=0 is derived by **dynamics** (a conservation/attractor equation of motion) | **NO** (G4-RHO: conservation → repulsive ρ∝r⁻², scale-freeness → continuum) |

α=0 is a **selection** fixed point, not a **dynamical** attractor. The two claims are compatible:
"derived" means *uniquely selected by the theory's criteria*, not *forced by an equation of
motion*. This is exactly the same status as μ=1 (NP_070): derived conditional on scale-freeness.

---

## 5. The earliest source of α = 0

```
Difference / Actualization primitives          [BOUNDARY]
   → AT-F1: the primitives carry no intrinsic scale  [BOUNDARY — the FINAL boundary]
   → renormalization invariance / scale-freeness     [the conditioning input]
   → octave organization (D96, K=3, [4,4,87])        [DERIVED — QG155]
   → μ = 1 (the unique scale-free point)             [DERIVED — QG7]
   → α = 0 (equal deficit per octave)                [DERIVED — QG206, = μ=1]
   → flat rotation, M ∝ R, SIS halo, structure       [DERIVED — QG184/206, NP_077]
```

**The earliest source of α = 0 is scale-freeness — the indifference principle (AT-F1) that the
primitives carry no intrinsic scale.** Given scale-freeness, μ=1 and α=0 follow uniquely.

---

## Theorem

> **Theorem (NP_078).** The universe selects α=0 because α=0 is the UNIQUE point at which four
> independent criteria coincide — flat rotation (v² ∝ r^(−α), slope 0 only at α=0), stability
> (equal-deficit-per-octave; α<0 diverges, α>0 concentrates), criticality (μ=1 ⟺ α=0), and maximum
> entropy (uniform per-octave allocation). α=0 is a DERIVED (unique) selection fixed point, but it
> is NOT a dynamical attractor (no equation of motion produces it — conservation gives the
> repulsive ρ ∝ r⁻², scale-freeness a continuum, G4-RHO) — it is the unique RG fixed point of the
> octave renormalization. The single conditioning input is scale-freeness (renormalization
> invariance), the indifference principle AT-F1. **Success criterion: scale-freeness is the FINAL
> BOUNDARY (not derived); α=0 is DERIVED (uniquely) GIVEN scale-freeness.** Classification:
> α=0 flat rotation DERIVED (QG206); μ=1 ⟺ α=0 criticality DERIVED (QG7/NP_070); scale-freeness
> (renormalization invariance) BOUNDARY (AT-F1); α=0 as a dynamical attractor REFUTED (G4-RHO);
> a canonical α≠0 universe REFUTED (no flat rotation, diverges/concentrates). No new primitive;
> canonical AT unchanged.
>
> *Proof sketch.* (1) Scan α ∈ [−2, +2] and show only α=0 is flat/scale-free/M∝R. (2) Show α≠0
> fails stability/criticality/profile/growth. (3) Determine derived/attractor/boundary/fixed-point.
> (4) Reconcile QG206 with G4-RHO. (5) Locate the root in AT-F1. ∎

---

## 6. Counterexamples

| Attempt | Why it fails |
|---|---|
| "α≠0 is viable" | α<0 diverges (outer-dominant), α>0 concentrates (core-dominant); only α=0 is flat, stable, scale-free |
| "α=0 is a dynamical attractor" | no equation of motion selects it; conservation gives the repulsive ρ∝r⁻² (G4-RHO) |
| "α=0 has no conditioning input" | scale-freeness (renormalization invariance, AT-F1) is the single boundary |
| "scale-freeness is derived" | it is the indifference principle — the statement that the primitives carry no scale, which nothing more fundamental produces |
| "α=0 is postulated" | it is uniquely selected by four coincident criteria (flat rotation, stability, criticality, max-entropy) |

---

## 7. Falsification paths

| Claim | Falsification |
|---|---|
| α=0 is the unique flat-rotation point | an α≠0 deficit hierarchy with a flat rotation curve (v² ∝ r^(−α), slope 0) |
| α=0 is the unique stable point | an α≠0 equal-per-octave (non-diverging, non-concentrating) assignment |
| μ=1 ⟺ α=0 | a scale-free branching with α≠0, or a flat rotation with μ≠1 |
| scale-freeness is the final boundary | a derivation of scale-freeness (no intrinsic scale) from more fundamental primitives |

---

## 8. Classification

| Component | Status |
|---|---|
| α=0 flat rotation (v² ∝ r^(−α), unique) | **DERIVED** (QG206) |
| μ=1 ⟺ α=0 criticality | **DERIVED** (QG7/NP_070) |
| α=0 unique stable (RG fixed) point | **DERIVED** (QG206 §2.3) |
| scale-freeness (renormalization invariance, indifference principle) | **BOUNDARY** (AT-F1) — the final boundary |
| α=0 as a dynamical attractor | **REFUTED** (G4-RHO) |
| a canonical α≠0 universe | **REFUTED** (diverges/concentrates, no flat rotation) |

**Conclusion.** The universe selects α=0 because it is the **unique** point where flat rotation,
stability, criticality (μ=1), and maximum entropy coincide; every α≠0 breaks at least one. α=0
is therefore **DERIVED (unique)** — a selection fixed point (the RG fixed point of the octave
renormalization), not a dynamical attractor (no equation of motion forces it; conservation
prefers the repulsive sector). The single conditioning input is **scale-freeness — the
indifference principle AT-F1** that the primitives carry no intrinsic scale. **Scale-freeness is
the final boundary: it is not derived, and α=0 (like μ=1) is derived uniquely given it.** No new
primitive; canonical AT unchanged.

---

## 9. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_078_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_078_AlphaScan` | only α=0 gives flat rotation (v² ∝ r^(−α)) | ✅ |
| `Y_NP_078_FlatRotationUnique` | slope 0 only at α=0; M∝R exponent 1−α=1 | ✅ |
| `Y_NP_078_Stability` | α=0 equal/octave; α≠0 diverges/concentrates | ✅ |
| `Y_NP_078_CriticalityEquivalence` | μ=1 ⟺ α=0 | ✅ |
| `Y_NP_078_NotDynamicalAttractor` | conservation→repulsive ρ∝r⁻², continuum | ✅ |
| `Y_NP_078_SelectionFixedPoint` | DERIVED (unique) via 4 criteria | ✅ |
| `Y_NP_078_ScaleFreenessIsBoundary` | AT-F1 = final boundary, not derived | ✅ |
| `Y_NP_078_Classification` | α=0 DERIVED; scale-freeness BOUNDARY | ✅ |
| `Y_NP_078_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_078"`

---

## References

- AT-QG: QG206 (α=0 origin), QG184 (M ∝ R), QG7 (critical branching, three criteria), QG155
  (octave organization), QG194/195 (deficit matter), QG222 (native dynamics), AT-F1
  (indifference principle).
- G4-RHO Phase 0 (`G4RHO_DynamicalOrigin.md`).
- ResearchY-NP_070 (criticality μ=1), NP_077 (structure formation).
