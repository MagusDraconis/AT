# AT-QG Phase 53 — Dependency Audit

**Program:** AT-QG (Unification)
**Phase:** 53 — which conclusions depend on which assumptions?
**Status:** COMPLETED — 3/3 xUnit tests pass (162/162 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

Map the dependency graph across the full chain and classify each result: ASSUMPTION-FREE / DERIVED /
OBSERVATION-DEPENDENT / MODEL-DEPENDENT. Identify the weakest remaining links.

---

## 2. Dependency graph (ATQG530)

| node | depends on | classification |
|---|---|---|
| Q-events | (root primitive) | ASSUMPTION-FREE |
| ρ | Q-events | DERIVED |
| geometry | ρ (+ causal order) | DERIVED |
| matter | ρ | DERIVED |
| gravity | geometry | DERIVED |
| saturation | Q-events (discreteness) | DERIVED |
| ψ | GW interpretation | MODEL-DEPENDENT |
| GW interpretation | observation + model | MODEL-DEPENDENT |

**1 ASSUMPTION-FREE / 5 DERIVED / 0 OBSERVATION-DEPENDENT / 2 MODEL-DEPENDENT.**

---

## 3. The derived chain (ATQG531)

```
Q-events ──→ ρ ──→ geometry ──→ gravity
     │          └──→ matter
     └──→ saturation
```

All five scalar nodes follow from the single Q-events primitive — no free assumptions beyond Q-events (and the
preferred η).

---

## 4. Weakest links (ATQG532)

- **ψ** — its necessity rests entirely on the spin-2 reading of the GW strain, itself model-dependent.
- **GW interpretation** — spin-2 is RECONSTRUCTED, not directly measured (QG48).

---

## 5. Conclusion

The scalar backbone is robust — derived from Q-events alone — but the **entire tensor sector hangs on a single
model-dependent link**: the spin-2 interpretation of the gravitational-wave strain. This is the terminal
epistemological map of the QG arc: one assumption-free root (Q-events), five derived consequences, and one
model-dependent branch (ψ via the GW interpretation).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG530 `ATQG530_DependencyGraph` | PASS (1/5/0/2) |
| ATQG531 `ATQG531_DerivedChain` | PASS (scalar backbone derived) |
| ATQG532 `ATQG532_WeakestLinks` | PASS (ψ + GW interpretation) |

Code: `AT.Core/ResearchXH/DependencyAudit.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase53_DependencyAuditTests.cs`.
