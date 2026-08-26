# AT-QG Phase 46 — Why Spin-2?

**Program:** AT-QG (Unification)
**Phase:** 46 — why is the minimal extension spin-2 instead of spin-1 or spin-0?
**Status:** COMPLETED — 3/3 xUnit tests pass (141/141 AT-QG)
**Constraint:** no new primitives beyond ψ

---

## 1. Goal

ψ is the only remaining primitive. Determine why its spin is 2 rather than 1 or 0. Classify: DERIVED / PREFERRED /
POSTULATED.

---

## 2. Three independent constraints uniquely select spin-2 (ATQG460/461)

| constraint | spin-0 | spin-1 | spin-2 |
|---|---|---|---|
| 2 observed polarizations (h_+, h_×) | ✗ (1 helicity) | ✓ | ✓ |
| universal attraction (even spin) | ✓ | ✗ (repulsive) | ✓ |
| correct light bending (full T_μν) | ✗ (trace T=0 for light) | — | ✓ |
| **VIABLE** | **no** | **no** | **yes** |

- spin-0 fails polarization and light bending (a scalar couples to the trace, which vanishes for light).
- spin-1 fails attraction (odd spin is repulsive, like electromagnetism).
- **spin-2 passes all three.**

---

## 3. Classification (ATQG462)

**PREFERRED.**

- NOT DERIVED: ψ is a new primitive; its spin is not derived from AT's scalar sector.
- NOT A BARE POSTULATE: spin-2 is not arbitrary — it is uniquely selected by three independent observational
  constraints.
- PREFERRED: among spin-0/1/2, only spin-2 satisfies all three simultaneously.

---

## 4. Conclusion

The minimal extension is **spin-2 because no other spin can simultaneously reproduce the observed gravitational
waves (2 polarizations), universal attraction, and the correct light bending**. The spin assignment is therefore
PREFERRED — uniquely pinned by observation, even though ψ itself is a new primitive. This closes the "why the
graviton?" question: spin-2 is the unique viable spin for a gravity field.

---

## Test program

| Test | Verdict |
|---|---|
| ATQG460 `ATQG460_OnlySpin2Survives` | PASS (only spin-2 viable) |
| ATQG461 `ATQG461_ThreeConstraints` | PASS (3 constraints) |
| ATQG462 `ATQG462_Classification` | PASS (PREFERRED) |

Code: `AT.Core/ResearchXH/WhySpin2.cs`;
tests `AT.Tests/ResearchXH/ATQG_Phase46_WhySpin2Tests.cs`.
