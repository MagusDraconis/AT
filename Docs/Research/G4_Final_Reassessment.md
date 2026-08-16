# G4 Final Reassessment — Native Metric-to-Operator Coupling

**Program:** G4 (with G4-T, G4-C, G4-D, G4-E, G4-F, G4-L)
**Status:** COMPLETED — final synthesis of the complete program
**Method:** synthesis of 22 completed phases (66 deterministic xUnit tests); no new physics, no new primitives, no new experiments.
**Question:** How much of the original metric→operator coupling gap remains open?

---

## 1. The original gap (recap)

Before G4, TQM natively determined the **metric** — `Q-events → causal order (native) →
conformal class (imported Malament/HKM) → conformal factor f = ρ^(2/d) (native counting measure)
→ g_μν` — but the **operator** was imported: the weighted graph Laplacian L_W (Riemannian) and the
BDG layer operator with binomial weights (Lorentzian). G4 asked whether the operator can be built
natively from `(≺, counting measure)` alone.

---

## 2. Before / after comparison

| Element | v1.0 monograph | After G4 (v1.1) |
|---|---|---|
| Riemannian operator | **imported** (L_W over spatial coupling K) | **native** — Lc = ρ⁻¹ L ρ⁻¹ |
| Lorentzian operator | **imported** (BDG binomial weights) | **native signature** (L1–L4 indefinite); **native retarded-indefinite** hybrid (H2 = R1 + L3) |
| Curvature read-out | not established | sign + ordering + magnitude-ordering reconstructed from Lc spectra |
| Curvature dynamics | absent | closed mean-field law R = F(ρ), F′ < 0; restoring mechanisms |
| Meaning of ρ | implicit | counting measure = event density ≡ actualization rate (0 new primitives) |
| Operator→coupling gap | listed **OPEN** | **MOSTLY CLOSED** (see §6) |

---

## 3. Area-by-area reassessment

### 3.1 Metric Origin — **SOLVED** (confidence: HIGH)

**Evidence.** `Q-events → causal order → conformal class → f = ρ^(2/d) → g` was closed before G4.
G4-F0 confirmed ρ is the counting measure (flat ρ ≡ 1; curved 1.003 → 1.452), canonically **event
density (C1)**, equivalently **actualization rate (C2)** — one primitive, zero new layers.

**Blocker.** Only the conformal-**class** step is imported (Malament 1977 / HKM 1976), a **proven
theorem**, not a theory gap; only the *factor* (ρ side) is native.

### 3.2 Native Riemannian Operator — **SOLVED** (confidence: HIGH)

**Evidence.** G4-C0: **Lc = ρ⁻¹ L ρ⁻¹** is the strongest native operator (sign separation 3.12, vs
1.18 degree-normalized / 0.90). G4-C1: benchmarked against Δ_g, **SC1–SC4 all passed** (sign
separation, degree-artifact-free, 5/5 observables monotonic, refinement-stable). G4-C-Uniqueness:
(1,1) is the **unique conformal Laplace–Beltrami representative** of a large family (only the
diagonal a = b is PSD; robust 22/25). The winner is the *analytic-density-weighted* ρ⁻¹Lρ⁻¹, not
the original C2 degree-normalized proxy.

**Blocker.** Analytic continuum proof Lc → Δ_g is **numeric** (SC1–SC4), not a theorem.

### 3.3 Curvature Reconstruction — **MOSTLY SOLVED** (confidence: HIGH sign/ordering, MEDIUM magnitude)

**Evidence.** G4-C2 recovered **sign** (−1/0/+1) and **ordering** R<0<R=0<R>0 from Lc spectra
(score −3.240 / 0 / +4.335; SC1–SC4). G4-C3 recovered **magnitude ordering** (sign 10/10,
magnitude 9/10; the a = 1.0 miss is a documented profile node). G4-0/1/2A established spectral
distinguishability (min KS 0.1322; Weyl d ≈ 2.28).

**Blocker.** Reconstruction is a **signed ordinal score**, not absolute ∫R dV. The original S1
heat-trace indicator was degree-dependent (G4-2A) and superseded by the Lc-spectral score.

### 3.4 Curvature Dynamics — **MOSTLY SOLVED** (confidence: HIGH mean-field, LOW field)

**Evidence.** G4-D0: Lc generates curvature dynamics (sign 17/17 through two flips; dR̂/dt
sign-consistent 16/16; Pearson(R̂,R) = 0.9796). G4-E0: a **closed native law R = F(ρ),
Ṙ = F′(ρ)·ρ̇, F′ < 0** (4 profiles collapse 67/67; size-independent, n = 24 at 94 %). G4-E1:
feedback is **anti-diffusive** (F(1)=0, F′(1)=−10.68, λ=+10.68; 2217/2217 anti-diffusive). G4-E2:
diffusion d > d\* = 10.68 stabilizes flat; logistic gives bistable finite attractors; conservation
pins flat.

**Blocker.** All dynamics is **0+1D mean-field** (single scalar ρ̄); no spatial field equation.

### 3.5 Native Lorentzian Operator — **SOLVED** (confidence: HIGH)

**Evidence.** G4-L0: causal order **alone** produces indefinite operators — L1 (36+/36−),
L2 (45+/27−), L3 (31+/41−), L4 (36+/36−) — all distinguishable from the elliptic Lc (PSD).
G4-L1: **L3 (layer operator) is the closest native BDG analogue** (KS 0.2222; only alternator;
profile (−1,+1,−1,+1)).

**Blocker.** None at the *signature* level. (Continuum limit to □ is not separately tested.)

### 3.6 Native Retarded Lorentzian Operator — **MOSTLY SOLVED** (confidence: HIGH structure, MEDIUM full causality)

**Evidence.** G4-L2: retarded propagation is native — R1 (past-only, nilpotent) matches BDG's
forward-only response; R3 (symmetric) is the indefinite baseline. G4-L3: **H2 = R1 + L3** resolves
the direction-vs-spectrum trade-off — retarded-biased (0.762 > 0.615) **and** indefinite (31+/41−)
**and** alternating **and** closer to BDG than L3 (KS 0.1389 < 0.2222). G4-L4: H2 propagates
forward-biased at finite speed (front-v ≤ 1, no superluminal) but retains a ~73 % Feynman tail.
G4-L5: the **local-degree diagonal (D4)** reduces leakage 0.759 → 0.697 (+ retardation 0.703)
while preserving indefiniteness/alternation. G4-L6: the **interval-weighted alternation (A3)** —
past full, future 1/(k+1) — reduces leakage 0.759 → 0.669 at its source, preserving
indefiniteness (31+/41−), alternation, and refinement stability (0.589 at N = 110).

**Blocker.** The residual Feynman tail is **intrinsic to the symmetric off-diagonal L3**. Full
retarded causality requires the BDG diagonal (−2) + binomial coefficients, which are **forbidden
inputs** to G4-L ("no BDG coefficients"). Native diagonals recover ~8–12 %; none eliminates the tail.

### 3.7 Remaining Blockers — **OPEN** (but narrow, enumerated)

1. **Full retarded d'Alembertian** — the BDG diagonal (−2) and binomial weights are not derived
   natively; the residual ~70 % Feynman tail remains. *(sharpest open item)*
2. **Absolute curvature magnitude** — reconstruction is ordinal (sign + ordering), not ∫R dV.
3. **Field-level dynamics** — mean-field only; no spatial field equation.
4. **Analytic continuum proof** — Lc → Δ_g (and L3/H2 → □) is numerically benchmarked, not proven.
5. *(Optional)* native re-derivation of the conformal class (Malament/HKM).

---

## 4. Classification summary

| # | Area | Classification | Confidence |
|---|---|---|---|
| 1 | Metric Origin | **SOLVED** | HIGH |
| 2 | Native Riemannian Operator | **SOLVED** | HIGH |
| 3 | Curvature Reconstruction | **MOSTLY SOLVED** | HIGH sign / MEDIUM magnitude |
| 4 | Curvature Dynamics | **MOSTLY SOLVED** | HIGH mean-field / LOW field |
| 5 | Native Lorentzian Operator | **SOLVED** | HIGH |
| 6 | Native Retarded Lorentzian Operator | **MOSTLY SOLVED** | HIGH structure / MEDIUM causality |
| 7 | Remaining Blockers | **OPEN** (5 items) | — |

---

## 5. Evidence chain (condensed)

```
Q-events → causal order → conformal class → f = ρ^(2/d) → g            [Metric Origin: SOLVED]
                             │
                             └─ ρ (counting measure)  ──────────────── [ρ meaning: SOLVED, G4-F0]
                             │
              ┌──────────────┴──────────────┐
              │ Riemannian                  │ Lorentzian
        Lc = ρ⁻¹ L ρ⁻¹                      L1–L4 (indefinite)          [signature: SOLVED, G4-L0/1]
              │                              │
        SC1–SC4 → Δ_g                       R1 (retarded, nilpotent)     [retarded: SOLVED, G4-L2]
        unique (a=b)                        H2 = R1 + L3                 [retarded-indefinite: SOLVED, G4-L3]
              │                              │
        R sign + ordering                    D4 diagonal / A3 interval   [tail suppression: PARTIAL, G4-L5/6]
        (score −3.24/0/+4.34)                (leak 0.759→0.697/0.669)    [full causality: OPEN, BDG diag]
              │
        R = F(ρ), F′ < 0                     (mean-field, 0+1D)
        anti-diffusive; diffusion/logistic restore
```

---

## 6. Overall verdict

**The metric→operator coupling gap is MOSTLY CLOSED.**

- The **Riemannian sector is CLOSED**: the imported weighted Laplacian is replaced by the native
  operator **Lc = ρ⁻¹ L ρ⁻¹** (counting measure only), which reproduces Δ_g, reconstructs
  curvature sign + ordering, and generates a closed mean-field curvature-dynamics law R = F(ρ)
  with restoring mechanisms.
- The **Lorentzian sector is MOSTLY SOLVED** (previously OPEN): causal order alone yields native
  indefinite operators (L1–L4), a native retarded operator (R1), a native retarded-**indefinite**
  hybrid (H2 = R1 + L3) that is closer to BDG than L3, and two native Feynman-tail reducers
  (degree diagonal D4, interval-weighted alternation A3). What remains is the **final causal
  kernel** — the BDG diagonal (−2) + binomial weights — whose absence leaves an irreducible ~70 %
  Feynman tail.

**Confidence assessment.** High for the structural claims (native operator, signature, retarded
structure, sign/ordering, mean-field law) — each is backed by multiple deterministic tests with
refinement stability. Medium for absolute magnitude, full causality, and field-level dynamics,
which remain ordinal / tail-limited / mean-field respectively.

**Bottom line.** The original blocker — "the operator is imported" — is **resolved**: both a
native Riemannian operator and a native retarded-indefinite Lorentzian operator now exist,
constructed from `(≺, counting measure)` with **no new primitives**. The single remaining import
is the BDG diagonal coefficient that closes the Feynman tail into a fully causal propagator.

---

## 7. Test inventory (66 xUnit tests, 22 phases)

| Program | Phases | Tests | Focus |
|---|---|---|---|
| G4 (0–2) | 3 | 9 | spectral curvature indicators, graph-spectra distinguishability |
| G4-T | 2 | 6 | topology comparison, density weighting |
| G4-C (+Uniqueness) | 5 | 15 | Lc construction, Δ_g benchmark, uniqueness, sign/ordering reconstruction |
| G4-D | 1 | 3 | curvature dynamics generated by Lc |
| G4-E (0–2) | 3 | 9 | evolution law R = F(ρ), feedback, restoring mechanisms |
| G4-F | 1 | 3 | physical meaning of ρ |
| G4-L (0–6) | 7 | 21 | native Lorentzian operators, retarded operator, retarded-indefinite hybrid, wave propagation, diagonal/alternation tail reduction |
| **Total** | **22** | **66** | |
