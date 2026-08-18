# TQM-QG Phase 54 — Is ψ a Connectivity Primitive?

**Program:** TQM-QG (Unification)
**Phase:** 54 — can the spin-2 sector originate from links rather than nodes?
**Status:** COMPLETED — 3/3 xUnit tests pass (165/165 TQM-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG52 showed ψ is fundamental, but it was always modeled as a field. Here we test whether the spin-2 sector can
originate from **link (connectivity)** degrees of freedom rather than nodes. Classify: FIELD / CONNECTIVITY / BOTH /
IMPOSSIBLE.

---

## 2. Adjacency tensor carries 2 polarizations (TQMQG540)

A symmetric rank-2 adjacency tensor A_ij has d(d+1)/2 = 6 components (d=3), decomposing as:

1 (trace, scalar) + 5 (symmetric-traceless, spin-2), and the traceless part contains exactly **2
transverse-traceless polarizations**.

Connectivity CAN carry spin-2.

---

## 3. ψ = the Weyl content of the causal connectivity (TQMQG541)

The causal order fixes the conformal class (light cone); its **Weyl tensor** is the spin-2 content. The scalar
sector froze Weyl = 0 (conformal flatness). ψ = Weyl ≠ 0 is the non-conformal link content — equivalent to a
rank-2 field, but sourced by the CONNECTIVITY.

This is a re-interpretation, **not** an elimination: Weyl ≠ 0 remains a new degree of freedom.

---

## 4. Classification (TQMQG542)

**BOTH.**

- NOT IMPOSSIBLE: a rank-2 link tensor carries exactly 2 spin-2 polarizations.
- NOT FIELD-ONLY: ψ has a genuine CONNECTIVITY origin (the non-conformal Weyl content of the causal links).
- BOTH: field and connectivity are EQUIVALENT descriptions (the Weyl tensor is a rank-2 field).

---

## 5. Conclusion

ψ can be read **either** as a fundamental spin-2 field **or** as the non-conformal connectivity of the Q-event
network — the two are equivalent. This is a genuinely elegant reframing of the QG arc: the graviton is not a field
imported from outside, but the Weyl (non-conformal) content of the causal link structure that the scalar sector had
frozen to zero. It remains a new primitive, but its origin is now understood as **connectivity**.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG540 `TQMQG540_AdjacencyCarriesSpin2` | PASS (2 polarizations) |
| TQMQG541 `TQMQG541_WeylContent` | PASS (ψ = Weyl) |
| TQMQG542 `TQMQG542_Classification` | PASS (BOTH) |

Code: `TQM.Core/ResearchXH/PsiAsConnectivity.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase54_PsiAsConnectivityTests.cs`.
