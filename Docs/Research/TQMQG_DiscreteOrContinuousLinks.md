# TQM-QG Phase 58 — Discrete or Continuous Links?

**Program:** TQM-QG (Unification)
**Phase:** 58 — are links discrete network objects or continuous fields?
**Status:** COMPLETED — 3/3 xUnit tests pass (177/177 TQM-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG55–57 established ψ = the traceless content of links. Here we ask whether links are DISCRETE or CONTINUOUS.
Classify: DISCRETE / CONTINUOUS / BOTH.

---

## 2. Microscopic discreteness (TQMQG580)

- adjacency matrix A_ij is 0/1 (quantized);
- the number of links |E| is countable;
- the traceless (Weyl) content of a finite graph is discrete (built from 0/1 entries);
- propagation on a finite graph is hopping.

Microscopically, links are **discrete network objects** — in exact parallel to the discrete Q-events.

---

## 3. Continuum limit (TQMQG581)

As N → ∞ at fixed density, the coarse-grained adjacency becomes a smooth field and its traceless content becomes
the continuous Weyl tensor ψ — just as discrete Q-events yield the continuous counting measure ρ.

---

## 4. Classification (TQMQG582)

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
| TQMQG580 `TQMQG580_MicroscopicDiscreteness` | PASS (quantized, countable) |
| TQMQG581 `TQMQG581_ContinuumLimit` | PASS (smooth ψ) |
| TQMQG582 `TQMQG582_Classification` | PASS (BOTH) |

Code: `TQM.Core/ResearchXH/DiscreteOrContinuousLinks.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase58_DiscreteOrContinuousLinksTests.cs`.
