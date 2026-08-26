# AT Foundation Audit

**Program:** AT-F (Foundation)
**Phase:** 0 — audit the remaining foundation assumptions.
**Status:** COMPLETED — synthesis of all completed phases (no new experiments).
**Method:** classify every assumption at the base of the derivation chain as DERIVED / PREFERRED / ASSUMED.

---

## 1. Executive Summary

The gravity program has been reduced to a short, explicit axiom list. Everything downstream — signature,
curvature, Einstein structure, geodesic acceleration, matter, long-range gravity, flat rotation curves, and
the α=0 abundance law — is now **DERIVED** from a handful of primitives and assumptions. The audit finds
**two real-underived primitives**, **two structural assumptions** (one preferred, one assumed), **one
physical postulate**, **one statistical postulate**, and **two free parameters**, plus the framework base.

---

## 2. The Derivation Chain (annotated)

```
A1  causal order (Q-events, partial order)        ── REAL-UNDERIVED primitive
       └─ conformal structure                     DERIVED (G4-M0)
       └─ Lorentzian signature (indefiniteness)   DERIVED (G4-L0)
A2  counting measure ρ                            ── REAL-UNDERIVED primitive
A3  metric origin √(−g) = ρ                       ── PREFERRED (identifies volume element = count)
A4  conformal flatness g = ρ^(2/d)η               ── ASSUMED (exponent 2/d DERIVED from A3, G4-A0)
       └─ Lc = ρ⁻¹Lρ⁻¹ → −cΔ_g + V                DERIVED (G4-P, G4-P3)
       └─ curvature R(ρ)                          DERIVED (G4-T1)
       └─ Ricci / Einstein tensor G(ρ)            DERIVED (G4-G1/G2)
       └─ geodesic a = −(1/d)∇lnρ                 DERIVED (G4-O3)
A6  matter = deficit m = ρ̄ − ρ                    DERIVED (G4-ME5; input: matter attracts)
       └─ 1/r² long-range (scale-free hierarchy)  DERIVED (G4-ME2)
       └─ flat rotation curve (log deficit)       DERIVED (G4-ME3)
       └─ α=0 selection                           DERIVED: max entropy (G4-RHO1), attractor (G4-RHO2),
                                                          max likelihood (G4-RHO3)
A5  indifference / scale-freeness                 ── POSTULATED (G4-RHO3)
A7  dimension d                                   ── SUPPLIED (free parameter)
A8  gravitational coupling G (BDG scale −2)      ── IMPORTED (G4-L12: NO MATCH)
```

---

## 3. Classification of Remaining Assumptions

### 3.1 Conformal flatness — ASSUMED (preferred by minimality)

g = ρ^(2/d)η splits into two parts (G4-A0):

- **Exponent 2/d — DERIVED.** Counting-measure preservation √(−g) = ρ gives ρ^(kd/2) = ρ, uniquely k = 2/d.
- **Flat representative η — ASSUMED.** √(−g) = ρ fixes only the determinant (one scalar condition), not the
  full metric (d(d+1)/2 components). A ψ-perturbed non-flat metric with the same √(−g) = ρ gives a *different*
  acceleration (−0.760 vs −0.230). Conformal flatness is the **minimal** choice (ρ is the only scalar
  available), but it is not mathematically forced.

**Verdict: ASSUMED (preferred).**

### 3.2 Indifference / scale-freeness — POSTULATED

The selection of α=0 ultimately rests on indifference: actualization is unbiased across scales, so all
microstates are equiprobable and the maximum-microstate (uniform) allocation is the equilibrium (G4-RHO3).
This is the **one statistical postulate**. It is natural (AT's temporal field has no preferred scale), but it
is a postulate, not a theorem.

**Verdict: POSTULATED (natural).**

### 3.3 Hidden / structural assumptions

| Assumption | Class | Evidence |
|---|---|---|
| **Metric origin √(−g) = ρ** | PREFERRED | identifies the counting measure with the invariant volume element; natural but a definition, not derived |
| **Matter attracts** (a points toward the deficit) | POSTULATE | the one physical input of G4-ME5; the standard gravitational principle |
| **Spacetime dimension d** | SUPPLIED | the program is dimension-generic but d is not derived (LabBook open problem #5) |
| **Newton's G / BDG scale −2** | IMPORTED | the exact continuum normalization does not emerge natively (G4-L12: NO MATCH) |
| **ρ > 0 (positivity)** | NATURAL | required for a non-degenerate metric; satisfied by the counting measure |
| **Static / spherical-symmetric profiles** | SIMPLIFICATION | a testing convenience, not a fundamental assumption |

---

## 4. Minimal Remaining Axiom Set

**Primitives (real-underived):**
1. **Causal order** — Q-events with a partial order.
2. **Counting measure ρ** — actualization/event density.

**Structural assumptions:**
3. **Metric origin** — √(−g) = ρ (the counting measure is the invariant volume element). *PREFERRED.*
4. **Conformal flatness** — g = ρ^(2/d)η. *ASSUMED (minimal).*

**Physical/statistical postulates:**
5. **Matter attracts** — a points toward the actualization deficit. *POSTULATE.*
6. **Indifference (scale-freeness)** — actualization unbiased across scales. *POSTULATE.*

**Free parameters (not derived):**
7. **Spacetime dimension d.**
8. **Gravitational coupling G** (BDG scale −2).

**Framework base:**
9. **The temporal field** — matter/quantum behavior/gravity emerge from self-organizing oscillations
   (the founding hypothesis of AT; carried by the QM/QG programs, not the gravity program).

---

## 5. What is NOT an assumption (already derived)

- **Lorentzian signature** — DERIVED from causal order (G4-L0).
- **Conformal structure** — DERIVED from causal order (G4-M0).
- **Curvature R(ρ), Lc → Δ_g** — DERIVED (G4-P, G4-P3, G4-T1).
- **Einstein tensor G(ρ), Bianchi/Gauss–Bonnet** — DERIVED (G4-G0/G1/G2).
- **Geodesic acceleration a = −(1/d)∇lnρ** — DERIVED (G4-O3).
- **Matter = deficit m = ρ̄ − ρ** — DERIVED (G4-ME5).
- **1/r² and flat rotation curves** — DERIVED (G4-ME2/G4-ME3, given the α=0 hierarchy).
- **α = 0** — DERIVED (maximum entropy + attractor + maximum likelihood, G4-RHO1/2/3).

---

## 6. Confidence Assessment

| Layer | Status | Confidence |
|---|---|---|
| Geometry (signature, conformal structure) | DERIVED | HIGH |
| Curvature / Einstein structure | DERIVED | HIGH |
| Matter / Newton-like gravity | DERIVED (given postulates) | MEDIUM–HIGH |
| Flat rotation curves / α=0 | DERIVED (given indifference) | MEDIUM |
| Metric origin + conformal flatness | PREFERRED / ASSUMED | the load-bearing assumption |
| d and G | SUPPLIED / IMPORTED | not derived |

---

## 7. Bottom Line

The gravity derivation now rests on **two primitives** (causal order, counting measure), **two structural
assumptions** (metric origin √(−g)=ρ — preferred; conformal flatness — assumed), **two postulates** (matter
attracts; indifference), **two free parameters** (d, G), and the **temporal-field framework**. The sharpest
remaining gap is the **conformal-flatness assumption** (the one place a genuinely non-trivial metric degree of
freedom is frozen out), and the **non-derivation of d and G** (dimensionality and the coupling constant). The
next foundational question is whether conformal flatness and the metric origin can themselves be relaxed or
derived — e.g., by admitting a dynamical ψ-field or a causal-set–style conformal-class argument that picks out
η from the conformal structure.
