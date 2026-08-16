# G4-M Phase 0 — Native Conformal Structure

**Program:** G4 — Native Metric-to-Operator Coupling (branch G4-M)
**Phase:** 0 — native reconstruction of conformal structure
**Status:** COMPLETED — 3/3 xUnit tests pass
**Constraint:** no Malament theorem, no metric tensor, no imported conformal class

---

## 1. Goal

Recover conformal information from the **causal order** and the **counting measure ρ** alone, and
show these natively distinguish flat / positive / negative curvature — without invoking Malament.

The key fact: the causal order *is* the conformal class (same for all conformally-related
geometries); the counting measure ρ = 1 + a·(x/xMax)² *is* the conformal factor. Native observables
split accordingly.

---

## 2. Results (1+1D Minkowski grid, tMax = 7, xMax = 4)

| observable | flat (a=0) | positive (a=−0.8) | negative (a=+1) | distinguishes |
|---|---|---|---|---|
| causal distance (longest chain) | 8 | 8 | 8 | ✗ (invariant) |
| interval volume center−edge | 12.00 | **16.40** | 6.50 | ✅ pos > flat > neg |
| layer-0 (link) mass | 6.00 | 5.80 | **6.25** | ✅ neg > flat > pos |

### G4-M00 — interval-volume profile

The counting-measure mass inside each causal diamond, V(x₀), is center-concentrated for the
positive-curvature profile (ρ peaks at x=0) and edge-spread for the negative-curvature profile
(ρ grows with |x|): center−edge = 16.40 (pos) > 12.00 (flat) > 6.50 (neg).

### G4-M01 — causal distance is a conformal invariant

The longest chain from (0,0) → (7,0) is **8 = tMax + 1 for all three** — the causal distance
depends only on the causal order (the conformal class), not on ρ. This *is* the conformal
invariance Malament asserts, recovered natively without the theorem.

### G4-M02 — layer growth

The layer-0 (Hasse-link) mass at x = 0,±1 equals **6 + a/4** analytically, ordering
negative (6.25) > flat (6.00) > positive (5.80) — the near-center counting measure tracks the
conformal factor.

---

## 3. Conclusion

**Yes — native conformal classification from causal data alone is achieved.**

- The **causal order** natively reconstructs the **conformal class** (the causal distance is a
  conformal invariant, identical across geometries) — no Malament import needed.
- The **counting measure ρ**, read off through **interval-volume** and **layer-growth** observables,
  natively reconstructs the **conformal factor**, cleanly separating flat / positive / negative
  curvature in a deterministic, analytically-predictable order.

No metric tensor, no Malament theorem, no imported conformal class — only causal order, interval
structure, layer structure, and the event density ρ.

---

## Test program

| Test | Verdict |
|---|---|
| G4-M00 `G4_M00_IntervalVolumeDistinguishesConformalGeometries` | PASS (center−edge 16.4 / 12.0 / 6.5) |
| G4-M01 `G4_M01_CausalDistanceIsConformallyInvariant` | PASS (chain = 8 for all) |
| G4-M02 `G4_M02_LayerGrowthClassifiesConformalGeometry` | PASS (layer-0 6.25 / 6.00 / 5.80) |

Code: `TQM.Core/ResearchXH/ConformalStructure.cs`;
tests `TQM.Tests/ResearchXH/G4M_Phase0_NativeConformalStructureTests.cs`.
