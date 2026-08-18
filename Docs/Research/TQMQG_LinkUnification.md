# TQM-QG Phase 64 — Unify Link Content

**Program:** TQM-QG (Unification)
**Phase:** 64 — are trace/traceless/phase independent or one link object?
**Status:** COMPLETED — 3/3 xUnit tests pass (195/195 TQM-QG)
**Constraint:** no new primitives added here (audit only)

---

## 1. Goal

Links currently carry trace → ρ, traceless → ψ, and phase → U(1). Determine whether these are independent or
components of one link object. Classify: SEPARATE / PARTIAL UNIFICATION / UNIFIED.

---

## 2. The three sectors (TQMQG640)

| sector | kind |
|---|---|
| trace (ρ) | spin-0 (magnitude) |
| traceless (ψ) | spin-2 (shape) |
| phase (θ) | U(1) (phase) |

Three different representations — independent degrees of freedom.

---

## 3. One complex link (TQMQG641)

The complete link is a **single complex rank-2 object**: L_ij = a_ij · e^(iθ_ij) — magnitude a_ij (trace ρ +
traceless ψ) times phase θ (U(1)). The three sectors are components of one link object.

---

## 4. Classification (TQMQG642)

**UNIFIED** — with irreducible sectors.

- NOT SEPARATE: the three sectors are components of one complex rank-2 link;
- UNIFIED: the complete link is a single object whose decomposition gives ρ, ψ, and θ — exactly as the network
  primitive unified nodes + links (QG55);
- WITH IRREDUCIBLE SECTORS: the three remain independent d.o.f. (spin-0 / spin-2 / U(1)).

---

## 5. Conclusion

The complete link is **one complex rank-2 object** unifying the scalar (trace), tensor (traceless), and gauge
(phase) sectors — a single structure with an irreducible interior, in the same spirit as the unified network
primitive (QG55). This is the final synthesis of the link content: one link, three sectors.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG640 `TQMQG640_ThreeSectors` | PASS (three representations) |
| TQMQG641 `TQMQG641_OneComplexLink` | PASS (one complex rank-2) |
| TQMQG642 `TQMQG642_Classification` | PASS (UNIFIED) |

Code: `TQM.Core/ResearchXH/LinkUnification.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase64_LinkUnificationTests.cs`.
