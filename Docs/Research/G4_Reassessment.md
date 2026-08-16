# G4 Reassessment — Native Metric-to-Operator Coupling

**Program:** G4 (with G4-T, G4-C, G4-D, G4-E, G4-F)
**Status:** COMPLETED — publication reassessment
**Method:** synthesis of 15 completed phases (45 deterministic xUnit tests); no new physics, no new primitives.
**Question:** How much of the metric→operator coupling gap remains open?

---

## 1. The original gap (recap)

The metric was already natively determined — `Q-events → causal order (native) → conformal
class (IMPORTED Malament) → conformal factor f = ρ^(2/d) (native counting measure) → g_μν` —
but the **operator** was imported: the weighted graph Laplacian $L_W$ (Riemannian) and the BDG
operator with binomial weights (Lorentzian). G4 asked whether the operator can be made native
from $(\prec,\ \text{counting measure})$ alone.

---

## 2. What was executed

| Original area | Completed phases |
|---|---|
| 1. Density-derived weighted Laplacians | G4-T0, G4-T1, G4-C0, G4-C-Uniqueness |
| 2. Metric–operator correspondence | G4-C0, G4-C1, G4-C-Uniqueness |
| 3. Spectral curvature indicators | G4-0, G4-1, G4-2A, G4-C2, G4-C3 |
| 4. Operator emergence from event geometry | G4-F0 (interpretation), G4-C0/C1 (construction) |
| + Curvature dynamics (beyond original spec) | G4-D0, G4-E0, G4-E1, G4-E2 |

---

## 3. Area-by-area reassessment

### 3.1 Metric Origin — **SOLVED** (confidence: HIGH)

**Evidence chain.** `MetricOriginClosure` closed the metric chain before G4; G4-F0 confirmed
ρ is the counting measure (positive per-vertex scalar; flat ρ ≡ 1; curved ρ varies
1.003 → 1.452), canonically **event density**, equivalently actualization rate, with **no new
primitive**.

**Blockers.** None. The conformal-class step is imported (Malament 1977 / HKM 1976), but it is
a **proven theorem**, not a theory gap; a native re-derivation is optional.

### 3.2 Native Operator — **PARTIALLY SOLVED** (confidence: HIGH Riemannian / LOW Lorentzian)

**Evidence chain.** G4-C0 found **Lc = ρ⁻¹ L ρ⁻¹** to be the strongest native operator (sign
separation 3.12, vs 1.18/0.90). G4-C1 benchmarked it against Δ_g and **SC1–SC4 all passed**
(sign separation, degree-artifact-free, 5/5 observables monotonic, refinement-stable). G4-C
Uniqueness showed (1,1) is the **unique conformal Laplace–Beltrami representative** of a large
empirical family (only diagonal a=b is PSD; robust 22/25).

**Key refinement vs the original spec.** The winner is the **analytic-density-weighted**
ρ⁻¹Lρ⁻¹, *not* the original C2 (degree-normalized D⁻¹ᐟ²LD⁻¹ᐟ²). The counting measure ρ, not the
degree proxy, is what removes the density bias and reproduces Δ_g. This avoids F8 (no metric
tensor is presupposed — only ρ and adjacency enter).

**Blockers (Lorentzian).** The d'Alembertian / BDG sector (C5, G4-13) was **not executed**:
the binomial weights $(-1)^{k+1}\binom{d+1}{k}$ remain imported. Operator emergence as the
native diffusion generator with invariant measure = density (E2/G4-14) was also not a separate
test.

### 3.3 Curvature Reconstruction — **PARTIALLY SOLVED** (confidence: HIGH sign/ordering, MEDIUM magnitude)

**Evidence chain.** G4-C2 recovered **sign** (−1/0/+1) and **ordering** R<0<R=0<R>0 from Lc
spectra (score −3.240 / 0 / +4.335; SC1–SC4). G4-C3 recovered **magnitude ordering** (sign
10/10; magnitude 9/10 — a=1.0 is a documented profile node, not a defect). G4-0/1/2A established
spectral distinguishability (min KS 0.1322, Weyl d ≈ 2.28).

**Blockers.** The reconstruction is a **signed ordinal score**, not an absolute numerical R or
∫R dV. The original heat-trace indicator S1 (R10) was **not** established — SCI = 2t⟨λ⟩−2 was
found **degree-dependent** (G4-2A: cubic graphs χ=+2/0/−2 all ≈ −0.30), so it was superseded by
the Lc-spectral score.

### 3.4 Curvature Dynamics — **PARTIALLY SOLVED** (confidence: HIGH mean-field / LOW field)

**Evidence chain.** G4-D0: Lc generates curvature dynamics (sign 17/17 through two flips;
dR̂/dt sign-consistent 16/16; Pearson(R̂,R) = 0.9796). G4-E0: a closed native law
**R = F(ρ), Ṙ = F′(ρ)·ρ̇, F′(ρ) < 0** (4 profiles collapse 67/67; size-independent, n=24 94 %).
G4-E1: feedback is **anti-diffusive** (F(1)=0, F′(1)=−10.68, λ=+10.68, 2217/2217 anti-diffusive).
G4-E2: diffusion (d > d\* = 10.68) stabilizes flat; logistic gives bistable finite attractors;
conservation pins flat.

**Blockers.** All dynamics is **0+1D mean-field** (the single scalar ρ̄). No spatial field
dynamics, no PDE, no full spacetime evolution.

### 3.5 Physical Meaning of ρ — **SOLVED** (confidence: HIGH)

**Evidence chain.** G4-F0: ρ = counting measure, canonically **event density** (C1),
equivalently **actualization rate** (C2) — one primitive, 0 new layers. Information density
(C3, needs the Θ layer) and hybrid (C4) are rejected.

**Blockers.** None.

### 3.6 Remaining Missing Pieces — **OPEN**

1. **Lorentzian d'Alembertian** — BDG binomial weights not derived natively (C5/G4-13).
2. **Absolute curvature magnitude** — reconstruction is ordinal (sign + ordering), not ∫R dV
   (S1/R10 not established; SCI degree-dependent).
3. **Full field dynamics** — mean-field only; no spatial field equation.
4. **Analytic continuum proof** — Lc → Δ_g is numerically benchmarked (SC1–SC4), not proven.
5. **Diffusion-generator closure** — invariant measure = density (E2/G4-14) not a separate test.
6. *(Optional)* native Malament re-derivation.

---

## 4. Summary

| Area | Classification | Confidence |
|---|---|---|
| 1. Metric Origin | **SOLVED** | HIGH |
| 2. Native Operator | **PARTIALLY SOLVED** (Riemannian solved, Lorentzian open) | HIGH / LOW |
| 3. Curvature Reconstruction | **PARTIALLY SOLVED** (sign+ordering solved, magnitude open) | HIGH / MEDIUM |
| 4. Curvature Dynamics | **PARTIALLY SOLVED** (mean-field solved, field open) | HIGH / LOW |
| 5. Physical Meaning of ρ | **SOLVED** | HIGH |
| 6. Remaining Missing Pieces | **OPEN** (5 items above) | — |

## 5. Overall verdict

The metric→operator coupling gap is **MOSTLY CLOSED in the Riemannian/conformal sector**. The
imported weighted Laplacian has been replaced by a **native operator Lc = ρ⁻¹ L ρ⁻¹** built only
from the counting measure ρ and the causal adjacency, which reproduces Δ_g (SC1–SC4), reconstructs
curvature sign and ordering, generates consistent curvature dynamics with a closed evolution law
R = F(ρ), and whose ρ is the same counting measure that fixes the metric's conformal factor.

The gap remains **partially open** on three fronts: (i) the **Lorentzian** operator (BDG weights
still imported), (ii) **absolute** curvature magnitude, and (iii) **field-level** (non-mean-field)
dynamics. These are the natural next phases of G4 — none require new physics or new primitives.
