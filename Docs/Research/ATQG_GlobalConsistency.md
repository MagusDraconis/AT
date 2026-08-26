# AT-QG Phase 93 — Global Network Consistency

**Program:** AT-QG (Unification)
**Phase:** 93 — can global consistency conditions reduce the freedom of SM parameters?
**Status:** COMPLETED — 3/3 xUnit tests pass (282/282 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether global consistency conditions can reduce the freedom of Standard Model parameters. Classify: NO REDUCTION / PARTIAL REDUCTION / STRONG REDUCTION.

---

## 2. Closed-loop constraints & global metric consistency (ATQG930)

Closed loops grow with network size (cyclomatic number E−V+1), and the global metric must be single-valued. A large
network becomes OVER-CONSTRAINED, collapsing link lengths to the few metric-field d.o.f. (ρ, ψ).

---

## 3. Reduction of freedom (ATQG931)

Global consistency strongly constrains the metric, but the SM parameters are only COMPATIBLY encoded in link length
(QG91), so their freedom is only partially reduced (narrowed region, correlations).

---

## 4. Classification (ATQG932)

**PARTIAL REDUCTION.**

- NOT NO REDUCTION: global consistency does narrow the allowed parameter region;
- NOT STRONG REDUCTION: the QG91 encoding is compatible, not deterministic, so the 19 values are not pinned;
- PARTIAL REDUCTION: geometric freedom collapses strongly; SM parameter freedom narrows only weakly.

---

## 5. Conclusion

Global consistency gives a **PARTIAL REDUCTION** of SM parameter freedom.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG930 `ATQG930_LoopsAndMetric` | PASS (over-constrained network) |
| ATQG931 `ATQG931_ReductionOfFreedom` | PASS (geometric strong, SM partial) |
| ATQG932 `ATQG932_Classification` | PASS (PARTIAL REDUCTION) |

Code: `AT.Core/ResearchXH/GlobalConsistency.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase93_GlobalConsistencyTests.cs`.
