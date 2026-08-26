# AT-QG Phase 92 — Network Consistency Constraints

**Program:** AT-QG (Unification)
**Phase:** 92 — do consistency conditions restrict link lengths and therefore parameter values?
**Status:** COMPLETED — 3/3 xUnit tests pass (279/279 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether network-consistency conditions restrict allowable link lengths — and therefore the parameter values encoded in them. Classify: NO EFFECT / PARTIAL CONSTRAINT / VALUE RELATIONS.

---

## 2. Triangle inequalities & loop consistency (ATQG920)

The metric must be a valid distance — triangle inequalities bound triples of lengths, and closed loops impose
holonomy consistency. Both restrict allowable link lengths.

---

## 3. Neighbor constraints, global stability, correlations (ATQG921)

Length restrictions (via QG91 encoding) induce bounds/relations among parameters, but the specific values remain
free within the allowed region.

---

## 4. Classification (ATQG922)

**PARTIAL CONSTRAINT.**

- NOT NO EFFECT: the conditions DO restrict lengths and hence parameters;
- NOT VALUE RELATIONS alone: the conditions are bounds/inequalities, not equations that fix values;
- PARTIAL CONSTRAINT: consistency induces bounds + correlations among parameters, not specific values.

---

## 5. Conclusion

Network consistency **PARTIALLY** constrains parameter values (bounds + correlations).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG920 `ATQG920_TriangleAndLoop` | PASS (triangle/loop constrain) |
| ATQG921 `ATQG921_NeighborStabilityCorrelations` | PASS (correlations, not values) |
| ATQG922 `ATQG922_Classification` | PASS (PARTIAL CONSTRAINT) |

Code: `AT.Core/ResearchXH/NetworkConsistencyParameters.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase92_NetworkConsistencyParametersTests.cs`.
