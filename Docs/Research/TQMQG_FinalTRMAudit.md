# TQM-QG Phase 42 — Final TRM Decomposition

**Program:** TQM-QG (Unification)
**Phase:** 42 — what percentage of TRM is now derived from TQM?
**Status:** COMPLETED — 3/3 xUnit tests pass (129/129 TQM-QG)
**Constraint:** no new primitives (audit only)

---

## 1. Goal

After the full arc, decompose TRM into its components and classify each: DERIVED / PARTIAL / IMPORTED / NEW
PRIMITIVE — and compute the percentage that is now derived from TQM.

---

## 2. Classification table (TQMQG420)

| TRM component | classification |
|---|---|
| saturation core | **DERIVED** (Poisson Q-event counting, QG36/QG38) |
| redshift | **DERIVED** (g_00 = −ρ^(2/d), QG34) |
| Schwarzschild recovery | **PARTIAL** (scalar g_00 yes; γ=+1 needs ψ) |
| rotation-curve term √(g_N·a0) | **IMPORTED** (MOND ansatz, QG41) |
| temporal propagation (n = e^Φ) | **IMPORTED** (refractive medium, QG28) |
| ψ sector (spin-2) | **NEW PRIMITIVE** (QG23/24/37) |

**2 DERIVED / 1 PARTIAL / 2 IMPORTED / 1 NEW PRIMITIVE.**

---

## 3. Percentage derived (TQMQG421)

- fully DERIVED: **2/6 = 33.3%**
- PARTIAL: 1/6 (Schwarzschild recovery — the scalar time/redshift part is derived, the γ=+1 spatial part needs ψ)
- derived score (DERIVED + 0.5·PARTIAL): **41.7%**

---

## 4. Summary (TQMQG422)

**DERIVED from TQM:** the saturation core and the redshift — the scalar backbone.

**NOT DERIVED:** the rotation-curve term and the temporal-propagation medium (IMPORTED rules) and the ψ sector
(NEW PRIMITIVE). Schwarzschild recovery sits between: its scalar half is derived, its tensor half is not.

---

## 5. Conclusion

**~33–42% of TRM is now derived from TQM** — specifically the scalar regular-core/saturation physics and the
gravitational redshift. The remainder (lensing, the MOND rotation-curve term, the refractive temporal medium, and
the tensor sector) is **not** derivable from TQM: it is either an **imported rule** or the **new ψ primitive**.

This is the terminal accounting of the QG arc: TQM supplies the scalar backbone (saturation + redshift), while
TRM's distinctive observational payload (lensing, rotation curves, gravitational waves) requires the imported
rules and the ψ primitive identified throughout QG23–QG41.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG420 `TQMQG420_ClassificationTable` | PASS (2/1/2/1) |
| TQMQG421 `TQMQG421_PercentageDerived` | PASS (41.7%) |
| TQMQG422 `TQMQG422_DecompositionSummary` | PASS (scalar backbone derived) |

Code: `TQM.Core/ResearchXH/FinalTRMAudit.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase42_FinalTRMAuditTests.cs`.
