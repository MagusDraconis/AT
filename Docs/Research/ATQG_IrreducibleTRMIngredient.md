# AT-QG Phase 34 — Identify the Irreducible TRM Ingredient

**Program:** AT-QG (Unification)
**Phase:** 34 — which single ingredient is responsible for TRM's successes?
**Status:** COMPLETED — 3/3 xUnit tests pass (105/105 AT-QG)
**Constraint:** no new primitives (audit of the already-identified ψ extension)

---

## 1. Goal

TRM reproduces three successes: **redshift**, **regular black holes**, **weak-field GR recovery**. Four candidate
ingredients are examined and removed in turn: effective mass M_eff(r), the propagation kernel, temporal-rate
modification, and a UV cutoff scale. Classify each: ESSENTIAL / SECONDARY / REDUNDANT.

---

## 2. The three aliases are ONE object (ATQG340)

- effective mass **M_eff = e^Φ − 1**;
- propagation kernel **n = e^Φ** = 1 + M_eff;
- temporal-rate modification **ψ** (g_00 = −ρ^(2/d)e^{2ψ}).

These are the **same** non-conformal factor written three ways: n = 1 + M_eff, and M_eff/n are just re-parameterized
ψ. Removing one removes all three.

---

## 3. Removal analysis (ATQG341)

| success | needs ψ? | needs cutoff? |
|---|---|---|
| redshift | **no** (AT g_00 = −ρ^(2/d) already gives it) | no |
| weak-field GR recovery | **yes** (moves γ −1 → +1) | no |
| regular black hole (finite-curvature horizon) | **yes** | no |

- Remove **ψ**: 2 of 3 successes die (weak-field GR + regular BH); only redshift survives.
- Remove the **UV cutoff scale**: **nothing** dies — all three survive.

---

## 4. Classification (ATQG342)

| ingredient | classification |
|---|---|
| temporal-rate modification (ψ) | ESSENTIAL |
| effective mass M_eff(r) | ESSENTIAL (= ψ) |
| propagation kernel | ESSENTIAL (= ψ) |
| UV cutoff scale | REDUNDANT |

**3 ESSENTIAL (but ONE object) / 0 SECONDARY / 1 REDUNDANT.**

---

## 5. Conclusion

The **irreducible TRM ingredient is the temporal-rate modification ψ — the non-conformal factor.** M_eff(r) and the
propagation kernel are the same object in different clothes, so "which single ingredient?" has a unique answer:
**ψ**. The UV cutoff scale is REDUNDANT for the three successes (AT has no native cutoff, QG14, and none of the
three predictions needs one). Redshift is not even a TRM contribution — AT's own g_00 provides it. This
concentrates the entire TRM payload into the one new primitive already identified: ψ (QG23/QG24/QG28).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG340 `ATQG340_ThreeAliasesOneObject` | PASS (n = 1 + M_eff) |
| ATQG341 `ATQG341_RemovalAnalysis` | PASS (ψ kills 2/3, cutoff kills 0/3) |
| ATQG342 `ATQG342_Classification` | PASS (3 ESSENTIAL/1 REDUNDANT) |

Code: `AT.Core/ResearchXH/IrreducibleTRMIngredient.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase34_IrreducibleTRMIngredientTests.cs`.
