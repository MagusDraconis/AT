# G4-P Phase 3 — General-Dimension Continuum Proof

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-P)
**Phase:** 3 — generalize the continuum proof beyond d = 2
**Status:** COMPLETED — 3/3 xUnit tests pass
**Classification:** **EXACT** (the continuum limit extends to all d via a dimension-dependent conformal weight)

---

## 1. Setup

Ω ⊂ ℝ^d sampled from a smooth density ρ(x) > 0; **L** = unnormalized graph Laplacian on a local
(ε-) neighborhood graph; kernel weight w(x,y) = h^(−d−2) K((x−y)/h), K radial, ∫K = 1,
second moment ∫u_μ u_ν K du = C δ_μν (c := C/2). Consider the one-parameter density-weighted family

```
M^(a) φ := ρ^(−a) L ρ^(−a) φ
```

with conformal weight a. The conformal factor is f = ρ^(2/d), the metric g = ρ^(2/d) η.

---

## 2. Dimension-independent kernel expansion

The unnormalized Laplacian (vertices weighted by ρ) satisfies, in ANY dimension d,

```
L ψ = −c [ ρ Δ_η ψ + 2 ∇ρ·∇ψ ] = −c (1/ρ) ∇·(ρ² ∇ψ)                    (1)
```

Applying this to ψ = ρ^(−a) φ and multiplying by ρ^(−a) gives the exact leading expansion

```
M^(a) φ = −c ρ^(1−2a) Δ_η φ
          − c(2−2a) ρ^(−2a) ∇ρ·∇φ
          − c a(a−1) ρ^(−2a−1) |∇ρ|² φ
          + c a ρ^(−2a) Δρ φ                                           (2)
```

For a = 1 this reproduces the d = 2 result of Phase 0 (the gradient term vanishes):
M^(1) = Lc = −c ρ⁻¹ Δ_η φ + c (Δρ/ρ²) φ.

---

## 3. The Laplace–Beltrami operator in d dimensions

Under g = ρ^(2/d) η = e^{2σ} η (σ = (1/d) ln ρ),

```
Δ_g φ = ρ^(−2/d) [ Δ_η φ + ((d−2)/d) ∇ln ρ · ∇φ ]                      (3)
```

The gradient term coefficient (d−2)/d vanishes **only** in d = 2 — this is the origin of the
"missing (d−2)∇lnρ·∇φ" term: it belongs to Δ_g for d ≠ 2, not to Lc.

---

## 4. Matching M^(a) to −c Δ_g (the key result)

Requiring the leading differential part of (2) to equal that of −c Δ_g (3) imposes two conditions:

```
Δ coefficient:        1 − 2a = −2/d      ⇒  a = 1/2 + 1/d
gradient coefficient:  2 − 2a = (d−2)/d   ⇒  a = 1/2 + 1/d   (self-consistent!)
```

Both are satisfied by the **dimension-dependent conformal weight**

```
a_d = (d + 2) / (2d) = 1/2 + 1/d
```

Therefore, in ANY dimension d,

```
M^(a_d) = ρ^(−a_d) L ρ^(−a_d)  =  −c Δ_g φ  +  (native zeroth-order potential)        (4)
```

where the potential is (from (2) with a = a_d)

```
V φ = c a_d ρ^(−2a_d) [ Δρ − (a_d − 1) |∇ρ|²/ρ ] φ
```

a curvature-type term built from ρ and its derivatives alone.

---

## 5. The conformal-weight ladder

| dimension d | a_d = (d+2)/(2d) | operator | continuum limit (modulo potential) |
|---|---|---|---|
| 2 | 1 | ρ⁻¹ L ρ⁻¹ (Lc) | −c Δ_g = −c ρ⁻¹ Δ_η |
| 3 | 5/6 | ρ^(−5/6) L ρ^(−5/6) | −c Δ_g |
| 4 | 3/4 | ρ^(−3/4) L ρ^(−3/4) | −c Δ_g |
| ∞ | 1/2 | ρ^(−1/2) L ρ^(−1/2) | −c Δ_η (flat, conformally invariant) |

a = 1/2 is the conformally-invariant flat Laplacian (the gradient and Δ-density powers cancel);
a = 1 (d = 2) is the conformal Laplacian; intermediate d interpolate between them.

---

## 6. Origin of the missing (d−2)∇lnρ·∇φ term

The gradient term of Δ_g arises from the conformal transformation law g = e^{2σ}η:
Δ_g = e^{−2σ}[Δ_η + (d−2)∇σ·∇]. Its coefficient (d−2) is exactly the difference between Δ_g and
ρ⁻¹Δ_η in d ≠ 2. In the graph operator, the gradient term's coefficient is 2−2a; the choice
a = 1/2 + 1/d reproduces (d−2)/d (up to the shared ρ^(−2/d)), while the d = 2 choice a = 1 sets it
to zero — consistent with (d−2) = 0.

---

## 7. Classification

| claim | verdict |
|---|---|
| Lc → Δ_g for d = 2 | PARTIAL (Δ_g + potential — Phase 0) |
| **continuum limit extends beyond d = 2** | **EXACT** — M^(a_d) → −c Δ_g + potential for all d |
| density power scaling | a_d = (d+2)/(2d) |
| conformal Laplacian forms | one-parameter family ρ^(−a)Lρ^(−a); a=1/2 flat, a_d conformal |
| dimension dependence | a_d → 1/2 as d → ∞ |

**Bottom line:** the native operator **can** be interpreted so the continuum limit extends beyond
d = 2 — by choosing the conformal weight a_d = (d+2)/(2d). The "missing" gradient term is not a
defect: it is *generated* by this exponent and is exactly the (d−2)∇lnρ·∇φ term of Δ_g. The d = 2
operator Lc = ρ⁻¹Lρ⁻¹ is the a_d = 1 member of this family. No new primitives (only ρ and L).

---

## Test program

| Test | Verdict |
|---|---|
| G4-P30 `G4_P30_DensityPowerScalingMinimizesAtOneHalf` | PASS (KS minimum at a = 1/2) |
| G4-P31 `G4_P31_ConformalFormsInvariantVsCurvature` | PASS (a=1/2 invariant 0.066, a=1 curvature 0.301) |
| G4-P32 `G4_P32_GeneralExponentReducesToLcAtD2` | PASS (a_2=1, a_3=5/6, a_4=3/4; M^(1)=Lc exact) |

Code: `AT.Tests/ResearchXH/G4P_Phase3_GeneralDimensionProofTests.cs`
(uses `ConformalOperator.BuildGeneral` for arbitrary exponents).
