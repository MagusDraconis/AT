# AT-QG Phase 56 — Origin of Weyl-Capable Links

**Program:** AT-QG (Unification)
**Phase:** 56 — why do links carry a non-conformal (traceless) degree of freedom?
**Status:** COMPLETED — 3/3 xUnit tests pass (171/171 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG55 showed ψ = the link content of the causal network. Here we ask WHY links carry a non-conformal (traceless)
degree of freedom. Classify: FORCED / PREFERRED / CONTINGENT.

---

## 2. Rank-2 decomposition (ATQG560)

A link relation is a symmetric rank-2 (adjacency) tensor A_ij, which always decomposes as:

trace (scalar / conformal factor) + traceless (spin-2 / Weyl).

A **conformal-only** link (trace only, Weyl = 0) drops the traceless part — it is the **restricted**, not the
general, case.

---

## 3. Link completeness (ATQG561)

A **complete link** encodes the full relation between two nodes — trace AND traceless. Dropping the traceless part
(Weyl = 0) is an incomplete description. Link completeness therefore forces the **Weyl capacity**.

---

## 4. Classification (ATQG562)

**FORCED** (capacity), CONTINGENT (value).

- **FORCED (capacity):** a complete link necessarily carries the traceless (Weyl) degree of freedom; conformal-only
  links are an incomplete restriction, not the default.
- **CONTINGENT (value):** whether that Weyl degree of freedom is excited (ψ ≠ 0) is set by observation (GWs).

---

## 5. Conclusion

The Weyl content is **FORCED in its capacity and CONTINGENT in its value**. The scalar sector was the
conformally-flat (Weyl = 0) restriction; ψ is the general (complete-link) case. This closes the "why Weyl" question
within the unified network primitive (QG55): the non-conformal degree of freedom is not an ad-hoc addition but the
traceless part of the complete link relation, frozen by the conformal-flatness assumption.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG560 `ATQG560_Rank2Decomposition` | PASS (trace + traceless) |
| ATQG561 `ATQG561_LinkCompleteness` | PASS (Weyl capacity forced) |
| ATQG562 `ATQG562_Classification` | PASS (FORCED) |

Code: `AT.Core/ResearchXH/OriginOfWeylLinks.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase56_OriginOfWeylLinksTests.cs`.
