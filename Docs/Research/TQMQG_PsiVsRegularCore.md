# TQM-QG Phase 35 — Does ψ Alone Reproduce the Regular-Core Structure?

**Program:** TQM-QG (Unification)
**Phase:** 35 — can ψ generate M_eff(r)=M(1−e^(−r³/r_c³)) without additional assumptions?
**Status:** COMPLETED — 3/3 xUnit tests pass (108/108 TQM-QG)
**Constraint:** no new primitives (audit of the already-identified ψ extension)

---

## 1. Goal

QG34 identified ψ as the irreducible TRM ingredient. Here we test whether ψ alone reproduces the regular-mass
profile M_eff(r) = M(1 − e^(−r³/r_c³)) — the canonical regular-core (Hayward/Bardeen-style) mass function.
Classify: FULL MATCH / PARTIAL MATCH / NO MATCH.

---

## 2. Target profile (TQMQG350)

M_eff(r) = M(1 − e^(−r³/r_c³)) has the two defining regular-core features:
- **M_eff(0) = 0** (finite core, no divergence);
- **M_eff(r→∞) → M** (asymptotically the Schwarzschild mass).

---

## 3. ψ qualitative vs exact (TQMQG351)

ψ is a **free field** (the new primitive). A smooth ψ with ψ(0)=0 gives a **qualitative** regular core — finite
M_eff = e^ψ−1 and finite curvature — for free. But the **specific** r³/r_c³ form is **not derivable**: it is an
ansatz imposed ON ψ and requires **two further inputs** — (1) the functional form, and (2) a new length scale r_c.

---

## 4. Classification (TQMQG352)

| aspect | classification |
|---|---|
| core regularity | FULL MATCH (smooth ψ → regular core) |
| curvature finiteness | FULL MATCH (smooth ψ → finite curvature) |
| horizon structure | PARTIAL MATCH (ψ can form horizons, not the specific r_c without assumption) |
| exact mass profile | NO MATCH (the r³/r_c³ form is an ansatz) |

**2 FULL MATCH / 1 PARTIAL MATCH / 1 NO MATCH → OVERALL: PARTIAL MATCH.**

---

## 5. Conclusion

ψ reproduces the regular-core **structure** (finite core + finite curvature) for free, but **not** the exact
M(1−e^(−r³/r_c³)) mass function: that specific form is an additional ansatz plus a new scale r_c. The irreducible
ψ ingredient (QG34) is necessary for regular black holes, but not sufficient by itself to *fix* the mass profile —
a specific ψ(r) and a core scale must be supplied as input. This keeps ψ a genuine primitive: it carries the
physics (non-conformal curvature), while the detailed regular-core shape remains a parameterization, not a
derivation.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG350 `TQMQG350_RegularMassProfile` | PASS (M_eff(0)=0, →M) |
| TQMQG351 `TQMQG351_PsiQualitativeVsExact` | PASS (qualitative yes, exact needs 2 assumptions) |
| TQMQG352 `TQMQG352_Classification` | PASS (PARTIAL MATCH) |

Code: `TQM.Core/ResearchXH/PsiVsRegularCore.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase35_PsiVsRegularCoreTests.cs`.
