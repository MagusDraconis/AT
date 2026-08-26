# G4-C Uniqueness Program

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-C)
**Phase:** Uniqueness (is Lc = ρ⁻¹ L ρ⁻¹ uniquely selected?)
**Status:** COMPLETED — 3/3 xUnit tests pass
**Question:** Can alternative density-weighted operators reproduce the same curvature reconstruction?
**Primitives used:** ρ · L · spectral observables. No metric tensor, no Laplace–Beltrami import.

---

## 1. Goal

Test the two-parameter family

$$M_{a,b}=\rho^{-a}\,L\,\rho^{-b}\quad(\text{symmetrized}),\qquad a,b\in[0,2]$$

for sign recovery, magnitude ordering, degree sensitivity and refinement stability, and classify
whether the conformal operator (a,b)=(1,1) is **unique**, **near-unique**, or **one member of a
large family**.

---

## 2. Results (grid a,b ∈ {0, 0.5, 1, 1.5, 2})

### 2.1 PSD (valid Laplacians)

```
        b=0   0.5   1    1.5   2
a=0     T     .     .     .     .
a=0.5   .     T     .     .     .
a=1     .     .     T     .     .
a=1.5   .     .     .     T     .
a=2     .     .     .     .     T
```

**Only the diagonal a = b is positive semi-definite.** The off-diagonal symmetrizations
(a ≠ b) are indefinite — they are **not** valid Laplacians.

### 2.2 Sign recovery (R<0 ζ2 above flat AND R>0 ζ2 below flat)

```
a=0     . . T T T
a=0.5   . T T T T
a=1     T T T T T
a=1.5   T T T T T
a=2     T T T T T        → 22 / 25
```

### 2.3 Magnitude ordering (ζ2 monotonic across strengths)

```
a=0     . T T T T
a=0.5   T T T T T
a=1     T T T T T        → 24 / 25 (only a=b=0 fails)
a=1.5   T T T T T
a=2     T T T T T
```

### 2.4 Robust region (sign + magnitude)

```
a=0     . . T T T
a=0.5   . T T T T
a=1     T T T T T        → 22 / 25
a=1.5   T T T T T
a=2     T T T T T
```

### 2.5 Refinement (n = 24, diagonal)

| a = b | neg ζ2 | flat ζ2 | pos ζ2 | sign-recovery |
|---|---|---|---|---|
| 0.5 | 1800.7 | 1062.1 | 237.6 | ✅ |
| 1.0 | 4410.7 | 1062.1 | 163.0 | ✅ |
| 1.5 | 11709.4 | 1062.1 | 125.5 | ✅ |

---

## 3. Classification

1. **Valid operators** (PSD) form the **diagonal** a = b; off-diagonal ρ⁻ᵃ L ρ⁻ᵇ are indefinite
   and thus excluded.
2. **Sign recovery among valid operators** holds for **a = b ≥ 0.5** — a **large family**
   (0.5, 1, 1.5, 2, …), **not unique to (1,1)**. Degree-sensitivity is the negation of sign
   recovery here, so every sign-recovering operator is automatically degree-insensitive.
3. **(1,1) is distinguished** as the operator with the **conformal continuum limit**
   Δ_g = ρ⁻¹Δ_η (the Laplace–Beltrami of the conformally-flat metric g = ρ·η) and the
   **largest sign separation** (Phase 0: 3.12 vs 1.18/0.90).

**Verdict:** (a,b)=(1,1) is **one member of a large family** for the empirical criteria (sign,
magnitude, refinement), but it is the **unique conformal Laplace–Beltrami representative** of
that family — the only member whose continuum limit is Δ_g.

---

## 4. Conclusion

Lc = ρ⁻¹ L ρ⁻¹ is **not uniquely selected by its spectral behaviour** — a whole diagonal family
a = b ≥ 0.5 reproduces the curvature reconstruction. Its special role is **theoretical**: it is
the unique member that converges to the conformal Laplace–Beltrami Δ_g = ρ⁻¹Δ_η, and it has the
largest empirical sign separation. This closes the G4-C program: the native conformal operator
is **a distinguished, theoretically-selected member of a large empirical equivalence class**.

---

## Test program

| Test | Verdict |
|---|---|
| G4-U00 `G4_U00_OperatorsValidAndSignRecoveryMap` | PASS |
| G4-U01 `G4_U01_MagnitudeOrderingAndDegreeSensitivityMap` | PASS |
| G4-U02 `G4_U02_RefinementAndClassification` | PASS |

`AT.Tests/ResearchXH/G4C_UniquenessTests.cs` (inherits `ResearchTestBase`, deterministic,
`StringBuilder`-composed reports).
