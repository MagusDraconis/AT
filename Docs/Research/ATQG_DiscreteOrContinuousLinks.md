# AT-QG Phase 58 — Discrete or Continuous Links?

**Program:** AT-QG (Unification)
**Phase:** 58 — are links discrete network objects or continuous fields?
**Status:** COMPLETED — 3/3 xUnit tests pass (177/177 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG55–57 established ψ = the traceless content of links. Here we ask whether links are DISCRETE or CONTINUOUS.
Classify: DISCRETE / CONTINUOUS / BOTH.

---

## 2. Microscopic discreteness (ATQG580)

- adjacency matrix A_ij is 0/1 (quantized);
- the number of links |E| is countable;
- the traceless (Weyl) content of a finite graph is discrete (built from 0/1 entries);
- propagation on a finite graph is hopping.

Microscopically, links are **discrete network objects** — in exact parallel to the discrete Q-events.

---

## 3. Continuum limit (ATQG581)

As N → ∞ at fixed density, the coarse-grained adjacency becomes a smooth field and its traceless content becomes
the continuous Weyl tensor ψ — just as discrete Q-events yield the continuous counting measure ρ.

---

## 4. Classification (ATQG582)

**BOTH.**

- DISCRETE microscopically (quantized adjacency, countable links);
- CONTINUOUS in the continuum limit (smooth ψ field).

This reconciles QG52 (ψ fundamental) with the network picture: ψ's microscopic form is discrete, its continuum
form is the smooth spin-2 field.

---

## 5. Conclusion

Links are **BOTH discrete and continuous** — discrete microscopically, continuous in the continuum limit — in
exact parallel to the nodes (Q-events → ρ). The full theory is a discrete causal network whose continuum limit is
the two-field theory (ρ + ψ).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG580 `ATQG580_MicroscopicDiscreteness` | PASS (quantized, countable) |
| ATQG581 `ATQG581_ContinuumLimit` | PASS (smooth ψ) |
| ATQG582 `ATQG582_Classification` | PASS (BOTH) |

Code: `AT.Core/ResearchXH/DiscreteOrContinuousLinks.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase58_DiscreteOrContinuousLinksTests.cs`.
