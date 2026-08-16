# G4-E Phase 0 — Curvature Evolution Law

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-E)
**Phase:** 0 — search for a native curvature evolution law
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Is there a closed relationship between R, dR/dt, ρ, dρ/dt, independent of graph size?
**Primitives used:** ρ · L · Lc · spectral observables. No metric tensor, no Einstein equations, no Laplace–Beltrami import.

---

## 1. Goal

Evolve the density field through four time profiles and search for a closed native law
relating reconstructed curvature R̂, its rate R̂̇, the mean density ρ̄, and its rate ρ̄̇.

$$\rho(x,t)=1+A(t)\,x^2,\qquad R(0,t)=-4A(t),\qquad \bar\rho(t)=\frac1N\sum_i\rho_i(t)$$

---

## 2. Setup

Four time profiles of A(t) (amplitude 0.8, 17 frames each):

| profile | A(t) | purpose |
|---|---|---|
| linear | −0.8 + 1.6·(t/16) | full sign sweep |
| quadratic | −0.8 + 1.6·(t/16)² | fine spacing near −0.8 |
| oscillatory | 0.8·cos(2πt/16) | two sign flips |
| localized | 0.8·exp(−((t−8)/2.667)²) | density pulse in time |

---

## 3. Results

### 3.1 G4-E00 — R = F(ρ)

All 68 (ρ̄, R̂) points from the four profiles collapse onto a **single monotonic curve**:
**67/67** ρ̄-sorted adjacent pairs are non-increasing (perfect collapse, 0 % noise).

- ρ̄ spans [0.8137, 1.3108]; R̂ spans [−4.764, +4.335]; R̂ = 0 exactly at ρ̄ = 1 (flat).
- The profile that generated a (ρ̄, R̂) pair is irrelevant — R is a function of ρ alone.

### 3.2 G4-E01 — the rate law

At n = 16, **64/64** steps satisfy sign(R̂̇) = −sign(ρ̄̇), so

$$\dot R = F'(\rho)\,\dot\rho,\qquad F'(\rho)<0.$$

Local slope F′(ρ̄) ∈ [−151.5, −6.8] (uniformly negative; the −151 outlier is a
near-stationary-density artifact). More density ⇒ more negative curvature, consistent with
R(0) = −4A and ρ̄ = 1 + A·S(A).

### 3.3 G4-E02 — graph-size independence

| n | N | collapse | rate-law | max reversal | noise floor |
|---|---|---|---|---|---|
| 16 | 256 | 67/67 | 64/64 | 0.000 | 0.00 % |
| 24 | 576 | 63/67 | 61/64 | 0.330 | 5.29 % |

The law holds at both sizes. At n = 24 a small fine-scale non-monotonicity appears
(≤ ~5 % of the R̂ range): the ε-threshold adjacency is **piecewise-constant in A** while ρ
varies continuously, so ultra-fine A steps (quadratic/localized) expose discretization
noise — not a breakdown of the law.

---

## 4. Candidate evolution law

$$\boxed{\;\dot R = F'(\rho)\,\dot\rho,\qquad F'(\rho)<0,\qquad R=F(\rho)\;}$$

Reconstructed curvature is a single-valued, monotonically decreasing function of the mean
density; its rate is therefore closed in ρ (and ρ̇). **No dependence on R beyond ρ is
needed** — the law is of the form Ṙ = F(ρ), not Ṙ = F(R, ρ). This is a native analogue of a
curvature–density relation obtained *without* the Einstein equations or a Laplace–Beltrami
import.

---

## 5. Failure modes (documented)

- **Fine-scale discretization noise** (G4-E02): piecewise-constant adjacency ⇒ non-monotonic
  wiggles ≤ ~5 % at n = 24 under ultra-fine A stepping.
- **Slope blow-up** near stationary density (dρ̄ → 0) inflates F′; the robust statement is the
  sign of Ṙ vs ρ̇, not the instantaneous slope.

---

## Test program

| Test | Verdict |
|---|---|
| G4-E00 `G4_E00_ReconstructedCurvatureIsAFunctionOfDensity` | PASS (67/67 collapse) |
| G4-E01 `G4_E01_CurvatureRateLawIsDensityDriven` | PASS (64/64 rate law) |
| G4-E02 `G4_E02_EvolutionLawIsGraphSizeIndependent` | PASS (n=16 100 %, n=24 ≥ 90 %) |

Code: `TQM.Core/ResearchXH/CurvatureDynamics.cs` (added `MeanDensity` to `CurvatureFrame`,
`Quadratic`/`Localized` profile generators, `Pearson`); tests
`TQM.Tests/ResearchXH/G4E_Phase0_CurvatureEvolutionLawTests.cs`.
