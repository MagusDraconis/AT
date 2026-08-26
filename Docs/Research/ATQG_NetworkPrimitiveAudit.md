# AT-QG Phase 55 — Network Primitive Audit

**Program:** AT-QG (Unification)
**Phase:** 55 — are Q-events and ψ truly independent?
**Status:** COMPLETED — 3/3 xUnit tests pass (168/168 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

QG54 showed ψ = the Weyl content of the causal connectivity. Here we ask whether (nodes, links) can be treated as a
single primitive network structure. Classify: INDEPENDENT / DUAL / UNIFIED.

---

## 2. Completeness (ATQG550)

- node-only: a set of points with no links → no structure (incomplete);
- link-only: links with no endpoints → undefined (incomplete);
- **nodes + links = a complete network (V, E).**

A network is intrinsically the pair (V, E).

---

## 3. One network primitive (ATQG551)

- (nodes, links) is **ONE** network primitive;
- ψ (the Weyl content) remains a **NEW degree of freedom** (the scalar sector froze Weyl = 0);
- nodes (spin-0) and links (spin-2) remain two **irreducible aspects** (QG51).

---

## 4. Classification (ATQG552)

**UNIFIED** — with a DUAL interior.

- NOT INDEPENDENT: Q-events and ψ are two aspects of ONE network, not two separate primitives.
- UNIFIED: the primitive count reduces from two to ONE — the causal network primitive.
- DUAL interior: nodes (actualization) and links (Weyl) remain irreducible aspects; the scalar sector was the
  restricted case Weyl = 0, and ψ is the unfrozen Weyl content.

---

## 5. Conclusion

AT is **one network primitive with a dual (node/link) interior** — not two independent primitives. Q-events are
the nodes; ψ is the non-conformal (Weyl) content of the links; the scalar sector was the conformally-flat (Weyl = 0)
restriction. This refines the final boundary (QG40): the two "primitives" unify into a single causal-network
primitive, with ψ as the unfrozen link content.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG550 `ATQG550_Completeness` | PASS (nodes+links complete) |
| ATQG551 `ATQG551_OneNetworkPrimitive` | PASS (one primitive) |
| ATQG552 `ATQG552_Classification` | PASS (UNIFIED) |

Code: `AT.Core/ResearchXH/NetworkPrimitiveAudit.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase55_NetworkPrimitiveAuditTests.cs`.
