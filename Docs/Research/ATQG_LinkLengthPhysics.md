# AT-QG Phase 91 — Physical Meaning of Link Length

**Program:** AT-QG (Unification)
**Phase:** 91 — can link length/distance encode physical parameter values?
**Status:** COMPLETED — 3/3 xUnit tests pass (276/276 AT-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Determine whether link length or link distance can encode physical parameter values. Classify: IRRELEVANT / PARTIAL / VALUE SELECTION.

---

## 2. Coupling strength vs link length, mass hierarchy (ATQG910)

Link length IS the network metric (derived from ρ). It can relate to coupling/mass via lattice-gauge and Yukawa
analogies — natural encoding mechanisms, not derivations of specific values.

---

## 3. Yukawa suppression, mixing, metric (ATQG911)

Yukawa suppression e^(−m r) and distance-suppressed mixing are COMPATIBLE mechanisms — they show HOW link length
could encode values — but the exponents (m), couplings (g), and mixing angles stay free.

---

## 4. Classification (ATQG912)

**PARTIAL.**

- NOT IRRELEVANT: link length IS the metric and can host Yukawa/lattice encoding;
- NOT VALUE SELECTION: it does not determine the specific values;
- PARTIAL: metric geometry derived; Yukawa/lattice value encoding compatible but not derivational.

---

## 5. Conclusion

Link length **PARTIALLY** encodes parameter values (geometry derived; value encoding compatible).

---

## Test program

| Test | Verdict |
|---|---|
| ATQG910 `ATQG910_CouplingAndMass` | PASS (metric + coupling/mass relation) |
| ATQG911 `ATQG911_YukawaMixingMetric` | PASS (suppression representable, values free) |
| ATQG912 `ATQG912_Classification` | PASS (PARTIAL) |

Code: `AT.Core/ResearchXH/LinkLengthPhysics.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase91_LinkLengthPhysicsTests.cs`.
