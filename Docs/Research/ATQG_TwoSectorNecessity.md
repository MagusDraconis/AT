# AT-QG Phase 50 — Necessity of Two Sectors

**Program:** AT-QG (Unification)
**Phase:** 50 — why does nature need both a scalar and a tensor sector?
**Status:** COMPLETED — 3/3 xUnit tests pass (153/153 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

Q-events and ψ appear as independent primitives. Determine whether the two-sector structure is arbitrary or
minimal. Classify: FORCED / PREFERRED / CONTINGENT.

---

## 2. Division of roles (ATQG500)

| sector | role | intrinsic spin |
|---|---|---|
| scalar (Q-events → ρ) | ACTUALIZATION / SOURCE (counting measure, matter, redshift, attraction) | 0 |
| tensor (ψ) | PROPAGATION / GEOMETRY (GWs, lensing, horizons, +/×) | 2 |

The two roles are **irreducible**: information/counting vs geometry/propagation.

---

## 3. Minimality (ATQG501)

- a single scalar cannot propagate spin-2 (QG23/37/49);
- a bare tensor cannot count discrete events;
- the smallest complete structure is **exactly two** sectors.

The structure is **MINIMAL, not arbitrary**.

---

## 4. Classification (ATQG502)

**FORCED** (minimal), tiered:

- the **scalar half** is FORCED by the nature of actualization (counting is intrinsically spin-0);
- the **tensor half** is CONTINGENT on the spin-2 GW observation (itself model-dependent, QG48);
- exactly two sectors is the minimal complete structure, not a free choice.

---

## 5. Conclusion

Nature requires two sectors because actualization (information) and propagation (geometry) are irreducible roles:
the first is intrinsically scalar, the second intrinsically spin-2. The two-sector structure is **minimal** and
**FORCED** — with the tensor half contingent on the single model-dependent observation that motivates it. This is
the terminal structural statement of the QG arc: **one scalar source + one tensor propagator = the minimal complete
universe.**

---

## Test program

| Test | Verdict |
|---|---|
| ATQG500 `ATQG500_DivisionOfRoles` | PASS (two irreducible roles) |
| ATQG501 `ATQG501_Minimality` | PASS (minimal, not arbitrary) |
| ATQG502 `ATQG502_Classification` | PASS (FORCED) |

Code: `AT.Core/ResearchXH/TwoSectorNecessity.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase50_TwoSectorNecessityTests.cs`.
