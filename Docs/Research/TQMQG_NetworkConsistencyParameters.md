# TQM-QG Phase 92 — Network Consistency Constraints

**Program:** TQM-QG (Unification)
**Phase:** 92 — do consistency conditions restrict link lengths and therefore parameter values?
**Status:** COMPLETED — 3/3 xUnit tests pass (279/279 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether network-consistency conditions restrict allowable link lengths — and therefore the parameter values encoded in them. Classify: NO EFFECT / PARTIAL CONSTRAINT / VALUE RELATIONS.

---

## 2. Triangle inequalities & loop consistency (TQMQG920)

The metric must be a valid distance — triangle inequalities bound triples of lengths, and closed loops impose
holonomy consistency. Both restrict allowable link lengths.

---

## 3. Neighbor constraints, global stability, correlations (TQMQG921)

Length restrictions (via QG91 encoding) induce bounds/relations among parameters, but the specific values remain
free within the allowed region.

---

## 4. Classification (TQMQG922)

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
| TQMQG920 `TQMQG920_TriangleAndLoop` | PASS (triangle/loop constrain) |
| TQMQG921 `TQMQG921_NeighborStabilityCorrelations` | PASS (correlations, not values) |
| TQMQG922 `TQMQG922_Classification` | PASS (PARTIAL CONSTRAINT) |

Code: `TQM.Core/ResearchXH/NetworkConsistencyParameters.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase92_NetworkConsistencyParametersTests.cs`.
