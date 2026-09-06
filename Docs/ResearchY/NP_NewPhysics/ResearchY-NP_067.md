# ResearchY-NP_067 — Lensing Sector Audit

**Program:** ResearchY — Wave Geometry Program
**Group:** NP — New Physics Roadmap
**ID:** ResearchY-NP_067 (permanent)
**Title:** Lensing Sector Audit
**Status:** COMPLETE
**Date:** 2026-09-06
**File:** `NP_NewPhysics/ResearchY-NP_067.md`
**Depends on:** AT-QG QG197/207 (metric g = ρ^(2/d)η), QG26 (PPN γ=−1, no lensing), QG212
(conformal optics resolved via ψ), QG43/44 (ψ tensor sector, GW), QG186 (frame dragging),
QG223 (ψ second primitive), QG194/195/206 (matter deficit, rotation), ResearchY-NP_065/066
(dark matter ontology/evidence)
**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_067_Tests.cs`

---

## Purpose

NP_066 established the deficit reproduces gravitational-potential phenomena (rotation,
cluster, Ωm) but fails lensing. NP_067 asks **why** — why does the deficit source gravity but
not bend light? Program: (1) derive the metric sector; (2) compute PPN γ; (3) locate the origin
of γ = −1; (4) find the minimal modification for γ → +1; (5) test whether the ψ tensor sector
restores lensing/weak/strong lensing; (6) measure the primitive cost. **Success criterion:**
determine whether the lensing failure is A) fatal, B) missing tensor sector, or C)
correspondence-only limitation. No new primitives; canonical AT unchanged.

---

## 1. The metric sector and PPN γ

The count-density metric is the **conformally flat** ansatz (QG197/207):

```
g_μν = ρ^(2/d) η_μν
   g₀₀ = −ρ^(2/d),   g_ij = ρ^(2/d) δ_ij
```

Writing ρ^(2/d) = e^(2Φ) with the Newtonian potential Φ = (1/d) ln ρ, the weak-field limit is:

```
g₀₀ ≈ −(1 + 2Φ),   g_ij ≈ (1 + 2Φ) δ_ij
```

Comparing to the PPN form (g₀₀ = −1 + 2U, g_ij = (1 + 2γU) δ_ij) gives **U = −Φ** and
**γ = −1**:

| Quantity | Conformal ρ-only (ψ=0) | GR / full AT (ψ≠0) |
|---|---|---|
| PPN γ | **−1** | **+1** |
| (1+γ)/2 (lensing prefactor) | **0** | **1** |

---

## 2. The origin of γ = −1 — why potential survives but light does not bend

The conformal factor ρ^(2/d) scales **time and space equally**. This is the whole story:

- **Gravitational-potential effects** (rotation, cluster mass, redshift, M∝R, Ωm) depend on
  **g₀₀ alone** — the time-time component — which is nontrivial (−ρ^(2/d)). They survive.
- **Light bending** (lensing) depends on the **combination of the time and space distortions**
  — the null-geodesic prefactor (1+γ)/2 — which **cancels exactly** for a conformal factor
  (γ = −1).

Equivalently, null geodesics are **conformally invariant**: a conformal rescaling leaves light
paths unchanged. A conformally flat metric therefore cannot lens, while it can still pull
massive bodies (whose motion is set by g₀₀ alone). This is why the deficit reproduces
rotation/clusters/matter-fraction but not lensing.

**Verified:** every lensing observable (deflection, convergence, shear, magnification) and the
Shapiro delay scale as **(1+γ)/2**, all vanishing at γ = −1 (QG212).

---

## 3. The minimal modification for γ → +1

The conformal flatness must be **broken** — the time and space components must distort
differently. The canonical completion is the **ψ tensor sector** (the Fierz–Pauli spin-2 field,
QG44):

```
ψ-completed metric:  g₀₀ = −ρ^(2/d) e^(2ψ),   g_ij = ρ^(2/d) (δ_ij + 2ψ_ij)   (QG207)
```

With ψ ≠ 0 the metric is no longer conformal, giving **PPN γ = +1** ⇒ (1+γ)/2 = 1, restoring
lensing, the Shapiro delay, and frame dragging (QG186) **at full GR strength** (QG212).
Lensing/Shapiro/γ actually require only a **scalar ψ** (1 d.o.f.); the full spin-2 (2 d.o.f.)
is required only by gravitational-wave polarization (QG43).

---

## 4. Does ψ restore lensing / weak / strong lensing?

| Observable | ψ=0 (conformal) | ψ≠0 (tensor) |
|---|---|---|
| light deflection Δθ ∝ (1+γ)/2 | 0 | full GR |
| convergence κ | 0 | full GR |
| shear γ_s | 0 | full GR |
| magnification μ | 1 (identity) | full GR |
| Shapiro delay | 0 | full GR |
| frame dragging (h_0i) | 0 | full GR (QG186) |
| gravitational waves | absent | restored (spin-2, QG43/44) |

**YES — ψ restores the full GR optics, including weak and strong lensing, at no new content
beyond the existing ψ sector** (QG212 "OPTICS RESOLVED"). The no-lensing of the deficit is a
**restricted ψ=0 sector**, not a fundamental failure.

---

## 5. Primitive cost

| Item | Cost |
|---|---|
| ρ (count density, scalar) | the first primitive (derived from Difference) |
| **ψ (tensor sector, spin-2)** | **+1 — the SECOND primitive** (QG223) |

**The primitive cost of lensing is +1: the ψ tensor sector**, which is already counted as the
theory's second primitive (it also restores frame dragging and gravitational waves). The
deficit (ρ) alone cannot lens; lensing is a ψ-sector observable.

---

## 6. A) fatal / B) missing tensor sector / C) correspondence-only

| Reading | Verdict |
|---|---|
| **A) fatal** | **NO.** The failure has a complete, known fix (ψ), and QG212 shows no-lensing is a *restricted* ψ=0 sector — ψ=0 is an assumption, physical optics is GR-like. |
| **B) missing tensor sector** | **YES — the answer.** The deficit (scalar ρ) alone gives γ=−1; the ψ tensor sector is the missing piece that restores γ=+1 and full lensing. |
| **C) correspondence-only limitation** | **PARTIAL.** Within the ρ-only sector lensing is impossible; full AT reproduces it at GR strength, but ψ is a BOUNDARY (hand-placed second primitive, QG223). |

**Determination: B — the lensing failure is a missing tensor sector, not fatal and not a
mere correspondence.** The deficit is a scalar; light bending needs the tensor (or at least a
scalar ψ) sector.

---

## Theorem

> **Theorem (NP_067).** The deficit fails light-bending because the ρ-only metric is
> conformally flat (g = ρ^(2/d)η, PPN γ = −1), and conformal flatness cancels the
> null-geodesic combination (1+γ)/2 = 0 — while preserving the potential effects (rotation,
> cluster, redshift, Ωm) that depend on g₀₀ alone. The minimal fix is the ψ tensor sector (the
> second primitive), which breaks conformal flatness and restores γ = +1 ⇒ full lensing, weak
> and strong lensing, the Shapiro delay, frame dragging, and gravitational waves. Proof:
> (1) Metric (Section 1, verified): g₀₀ = −ρ^(2/d), g_ij = ρ^(2/d)δ_ij ⇒ γ = −1. (2) Origin
> (Section 2): the conformal factor scales time and space equally; potential effects depend on
> g₀₀ (survive), light bending on (1+γ)/2 (vanishes); null geodesics are conformally invariant.
> (3) Fix (Section 3): g₀₀ = −ρ^(2/d)e^(2ψ) breaks conformal flatness ⇒ γ = +1 (QG207/212).
> (4) Restoration (Section 4, verified): deflection/convergence/shear/magnification/Shapiro/
> frame-dragging/GW all restored at GR strength by ψ. (5) Cost (Section 5): +1 primitive (ψ,
> the second primitive). (6) Determination (Section 6): B — missing tensor sector. Classification:
> the conformal metric (γ=−1) DERIVED (QG197/207); no-lensing in ψ=0 DERIVED (QG26/212); the ψ
> tensor sector BOUNDARY (second primitive, QG223); lensing restored by ψ CORRESPONDENCE (GR
> strength, QG212); a fatal lensing failure REFUTED. **Success criterion: the lensing failure
> is B (missing tensor sector), fixed by the ψ primitive — not fatal, not a mere
> correspondence.** No new primitive; canonical AT unchanged.
>
> *Proof sketch.* (1) Derive γ=−1. (2) Explain the conformal origin. (3) Give the ψ fix.
> (4) Verify restoration. (5) Cost the primitive. (6) Classify. ∎

---

## 7. Counterexamples

| Attempt | Why it fails |
|---|---|
| "the lensing failure is fatal" | QG212: no-lensing is a restricted ψ=0 sector; ψ=0 is an assumption, physical optics is GR-like |
| "the deficit alone can lens" | conformal flatness (γ=−1) cancels all lensing observables ((1+γ)/2=0) |
| "ψ is a free addition beyond AT" | ψ is already the theory's second primitive (QG223), restoring frame dragging + GW too |
| "lensing is correspondence-only" | within the ρ-only sector it is impossible; but the fix is structural (ψ), not a fit |

---

## 8. Falsification paths

| Claim | Falsification |
|---|---|
| the conformal metric has γ = −1 | a conformally flat metric with γ ≠ −1 |
| the deficit alone cannot lens | a ρ-only metric that bends null geodesics |
| ψ restores γ = +1 | a ψ-completed metric with γ ≠ +1 |
| the fix is the ψ sector | a conformal-breaking fix with no new primitive |

---

## 9. Classification

| Component | Status |
|---|---|
| conformal metric g = ρ^(2/d)η (γ = −1) | **DERIVED** (QG197/207) |
| no-lensing in the ψ=0 sector | **DERIVED** (QG26/212) |
| the ψ tensor sector (second primitive) | **BOUNDARY** (QG223) |
| lensing/weak/strong/Shapiro restored by ψ | **CORRESPONDENCE** (GR strength, QG212) |
| a fatal lensing failure | **REFUTED** |

**Conclusion.** The deficit fails light-bending because it sources a **conformally flat**
metric (PPN γ = −1), and conformal flatness leaves null geodesics un-bent — while the
potential effects that do not depend on the time-space *difference* (rotation, cluster mass,
redshift, Ωm) survive. The minimal modification is the **ψ tensor sector** (the theory's second
primitive), which breaks conformal flatness and restores **γ = +1**, hence full lensing, weak
and strong lensing, the Shapiro delay, frame dragging, and gravitational waves — all at GR
strength. **The lensing failure is therefore B (a missing tensor sector), fixed by the ψ
primitive — not fatal, and not a mere correspondence.** No new primitive; canonical AT
unchanged.

---

## 10. Result summary

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_067_Tests.cs`

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_067_ConformalMetric` | g = ρ^(2/d)η; g₀₀/g_ij equal scaling | ✅ |
| `Y_NP_067_PPNGammaMinusOne` | γ = −1 | ✅ |
| `Y_NP_067_DeflectionVanish` | Δθ ∝ (1+γ)/2 = 0 at γ=−1 | ✅ |
| `Y_NP_067_PotentialSurvives` | g₀₀ nontrivial (rotation/cluster survive) | ✅ |
| `Y_NP_067_PsiBreaksConformality` | ψ-completed metric ⇒ γ=+1 | ✅ |
| `Y_NP_067_PsiRestoresLensing` | lensing/weak/strong/Shapiro restored | ✅ |
| `Y_NP_067_PrimitiveCost` | +1 (ψ, second primitive) | ✅ |
| `Y_NP_067_Classification` | B (missing tensor sector), not fatal | ✅ |
| `Y_NP_067_Run` | research report | ✅ |

**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_067"`

---

## References

- AT-QG: QG197/207 (metric g = ρ^(2/d)η), QG26 (PPN γ=−1), QG212 (conformal optics resolved),
  QG43/44 (ψ tensor sector, GW), QG186 (frame dragging), QG223 (ψ second primitive),
  QG194/195/206 (matter deficit, rotation).
- ResearchY-NP_065 (dark matter ontology), NP_066 (dark matter evidence).
