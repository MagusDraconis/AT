# G4-P Phase 0 — Analytic Continuum Limit of Lc = ρ⁻¹ L ρ⁻¹

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-P)
**Status:** COMPLETED — formal asymptotic derivation (no new primitives, no new experiments)
**Question:** What differential operator appears as graph spacing h → 0? Is it Lc → Δ_g for g = ρ^(2/d) η?
**Classification:** **PARTIAL**

---

## 1. Setup

- Domain Ω ⊂ ℝ^d (the code uses d = 2), sampled from a smooth density **ρ(x) > 0** (the counting
  measure / event density), connected as a local (ε-/h-) neighborhood graph.
- **L** = unnormalized combinatorial graph Laplacian, L = D − A:
  (L ψ)(x_i) = Σ_{j∼i} (ψ_i − ψ_j).
- **Lc = ρ⁻¹ L ρ⁻¹**: (Lc φ)_i = ρ_i⁻¹ Σ_j L_ij ρ_j⁻¹ φ_j = ρ_i⁻¹ Σ_{j∼i} (φ_i/ρ_i − φ_j/ρ_j).
- Conformal factor f = ρ^(2/d), metric **g = ρ^(2/d) η** (η = Euclidean).

Kernel convention: weight w(x,y) = h^(−d−2) K((x−y)/h), K radial symmetric, ∫K = 1,
second moment ∫u_μ u_ν K(u) du = C δ_μν. Write c := C/2.

---

## 2. Graph-Laplacian expansion

For smooth ψ, Taylor-expanding ψ(y) and ρ(y) about x (u = (y−x)/h, dy = h^d du):

```
(L ψ)(x) = ∫ w(x,y)(ψ(x) − ψ(y)) ρ(y) dy
         = h⁻² ∫ K(u) [−h ∇ψ·u − (h²/2) uᵀH_ψ u] [ρ + h ∇ρ·u] du + O(h)
```

The O(h⁻¹) term vanishes (∫K(u) u_μ du = 0). The O(1) terms give

```
L ψ  =  −c [ ρ Δ_η ψ + 2 ∇ρ·∇ψ ] + O(h)              (1)
     =  −c (1/ρ) ∇·(ρ² ∇ψ) + O(h)                     (1′)
```

*(ρ ≡ 1 recovers the flat result L → −c Δ_η.)*

---

## 3. Density-weighted expansion

Lc = ρ⁻¹ L ρ⁻¹ means (Lc φ)(x) = ρ(x)⁻¹ · (L(φ/ρ))(x). Substituting ψ = φ/ρ into (1′):

```
ρ² ∇(φ/ρ) = ρ ∇φ − φ ∇ρ
∇·(ρ ∇φ − φ ∇ρ) = ρ Δ_η φ − φ Δ_η ρ
⇒  Lc φ  =  −c [ ρ⁻¹ Δ_η φ − (Δ_η ρ / ρ²) φ ] + O(h)   (2)
```

---

## 4. Leading-order operator and the identification with Δ_g

For **d = 2**, the Laplace–Beltrami operator of g = ρ^(2/d) η = ρ η is

```
Δ_g φ = ρ⁻¹ Δ_η φ                                        (d = 2)
```

so (2) reads

```
Lc φ  =  −c Δ_g φ  +  c (Δ_η ρ / ρ²) φ  + O(h)          (3)
```

**The leading differential (second-order) operator is exactly −c Δ_g.** But there is an
**unavoidable zeroth-order potential** V(x) = c·Δ_η ρ(x)/ρ(x)² that is *not* part of Δ_g.

---

## 5. Error terms and the potential

- **Taylor error:** O(h) from truncating the kernel expansion (plus O(h²) from ρ(y)).
- **The potential Δρ/ρ² is NOT an error** — it is a genuine zeroth-order term that survives the
  h → 0 limit. It vanishes iff Δρ = 0 (ρ harmonic; in particular ρ = const, the flat case).

For the code's profile ρ = 1 + a x² (d = 2): Δρ = 2a, so V(0) = c·2a = −(c/2) R(0), since the
analytic curvature is R(0) = −4a. **The potential is proportional to the scalar curvature at the
origin** — it is a native curvature read-out, not a defect.

---

## 6. General d caveat

For d ≠ 2, Δ_g = ρ^(−2/d)[Δ_η φ + (d−2)(∇ln ρ)·∇φ]. The result (2) has density power ρ⁻¹ (not
ρ^(−2/d)) and **no** first-order gradient term, so Lc does **not** reproduce Δ_g for d ≠ 2. The
identification is special to d = 2 (where the conformal factor f = ρ^(2/d) = ρ and the gradient
term of Δ_g vanishes).

---

## 7. Classification: PARTIAL

| claim | verdict |
|---|---|
| Lc → Δ_g (exact, d = 2) | **PARTIAL** — leading differential part is −c Δ_g, but with an additive zeroth-order potential c·Δρ/ρ² |
| Lc → Δ_g (d ≠ 2) | **FAILED** — wrong density power and missing gradient term |
| Lc is native (no new primitives) | ✅ ρ and L only |
| curvature read-out is analytic | ✅ potential ∝ R (at the origin) |

**Bottom line:** Lc = ρ⁻¹ L ρ⁻¹ converges to **−c Δ_g + c(Δρ/ρ²)** — the Laplace–Beltrami operator
of g = ρ η plus a native, curvature-proportional zeroth-order potential. The bare Laplace–Beltrami
is recovered only for harmonic/constant ρ. This *analytically explains* the empirical G4-C results:
Lc's curvature-sign separation is driven by the Δρ/ρ² potential (carrying the sign of Δρ, hence of
R), while the Δ_g part provides the differential structure. The metric→operator correspondence is
therefore **Δ_g + native curvature potential**, not Δ_g alone.

---

## 8. Verification against existing G4 results (no new experiments)

- G4-C1: Lc's curvature-consistent ordering and sign separation (SC1–SC4) — *now explained* by the
  analytic Δ_g + (Δρ/ρ²) structure; the potential carries the sign of R.
- G4-C-Uniqueness: (a,b) = (1,1) is the PSD, unique conformal representative — consistent with
  Lc being the symmetric ρ⁻¹Lρ⁻¹ (a = b = 1), the only member of the family with the clean
  Δ_g + potential limit derived here.
- G4-P Phase 1 (next): numerically confirm (3) on the ρ = 1 + a x² grid (apply Lc to a smooth test
  φ and compare against −c Δ_g φ + c(Δρ/ρ²) φ).
