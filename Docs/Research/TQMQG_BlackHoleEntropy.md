# TQM-QG Phase 12 — Black-Hole Microstate Test

**Program:** TQM-QG (Unification)
**Phase:** 12 — can horizon entropy emerge from counting statistics?
**Status:** COMPLETED — 3/3 xUnit tests pass (39/39 TQM-QG)
**Constraint:** no new primitives

---

## 1. Goal

Test whether horizon entropy S ∝ Area emerges from the counting measure, by counting horizon (boundary) events
vs bulk (volume) events. Classify: MATCH / PARTIAL MATCH / NO MATCH.

---

## 2. Results

### (a) The counting measure gives both area and volume counts (TQMQG120)

For a horizon of radius R in d=3 (3+1) spatial dimensions:

| R | area R² | volume R³ | S_horizon ∝ R² | S_bulk ∝ R³ |
|---|---|---|---|---|
| 1 | 1 | 1 | 0.69 | 0.69 |
| 2 | 4 | 8 | 2.77 | 5.55 |
| 4 | 16 | 64 | 11.09 | 44.36 |

The horizon (boundary) count scales as **area R^(d−1)** (ratio 2²=4); the bulk count scales as **volume R^d**
(ratio 2³=8). The horizon is the boundary, so its event count is area-like.

### (b) Horizon microstates give S ∝ Area (TQMQG121)

Counting 1 bit per horizon cell: S = A·ln 2 ∝ R², W = e^S. S(2R)/S(R) = 4 (area law), not 8 (volume law). The
microstate multiplicity is **exponential in the horizon area** — the Bekenstein–Hawking area law.

### (c) Classification (TQMQG122)

**MATCH (S ∝ Area from horizon counting), with two caveats.**

---

## 3. Classification: MATCH (conditional)

- The counting measure gives the horizon a boundary count ∝ R^(d−1) (area), distinct from the bulk count ∝ R^d.
- Counting 1 bit per horizon cell gives S ∝ Area and W = e^(A ln 2) — the area law.
- **Caveat 1 (holographic):** the area law requires identifying entropy with the horizon (boundary) degrees of
  freedom, not the bulk — a natural minimal choice, not a dynamical derivation.
- **Caveat 2 (mass scaling):** TQM's deficit mass (enclosed deficit) ∝ R^d, whereas Schwarzschild M ∝ R, so the
  S ∝ M² relation and the exact 1/4 coefficient are NOT reproduced — only the area law S ∝ Area (radius scaling)
  is native.

---

## 4. Conclusion

The **area law S ∝ Area emerges natively** from counting horizon (boundary) events: the counting measure
distinguishes a boundary (area, R^(d−1)) from a bulk (volume, R^d), and the horizon entropy is the boundary
count, giving W = e^(A ln 2). This is a MATCH for the scaling law, conditional on the (natural, minimal)
holographic identification that horizon entropy is boundary degrees of freedom. The exact coefficient (1/4) and
the S ∝ M² relation are NOT reproduced — those require the Schwarzschild M ∝ R mass relation, which TQM's
volume-scaled deficit mass does not (yet) provide.

---

## Test program

| Test | Verdict |
|---|---|
| TQMQG120 `TQMQG120_EventCountingAreaVsVolume` | PASS (area vs volume scaling) |
| TQMQG121 `TQMQG121_MicrostateEntropyScaling` | PASS (S ∝ Area, W = e^S) |
| TQMQG122 `TQMQG122_Classification` | PASS (MATCH, conditional) |

Code: `TQM.Core/ResearchXH/BlackHoleEntropy.cs`;
tests `TQM.Tests/ResearchXH/TQMQG_Phase12_BlackHoleEntropyTests.cs`.
