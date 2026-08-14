# Metric Emergence Program — Report

**Test file:** `TQM.Tests/ResearchXC/MetricEmergenceTests.cs`
**Result:** **PASSED (4/4).**

---

## Results

| # | Test | Result | Classification |
|---|---|---|---|
| 1 | DistanceMatrix_IsMetric | causal distance matrix satisfies all four metric axioms | **PRESENT** |
| 2 | CausalVolume_DefinesConformalFactor | $\sqrt{|g|}=f^{d/2}=\rho$ round-trips for $\rho\in\{0.5,1,2,8\}$, $d\in\{2,3,4\}$ | **PRESENT** |
| 3 | ConformalMetricCandidate_IsConstructible | constant $f$: $R=0$; $f=1+\tfrac12 x^2$: $R=-0.6145$ | **CONSTRUCTIBLE** |
| 4 | MetricEmergence_PresentOrMissing | distance + factor present, candidate constructible, full $g_{\mu\nu}$ missing | **verdict** |

---

## What was reconstructed (existing content + standard geometry)

1. **Distance matrix.** `CausalUniverse.CausalVolume` + `GeometryEmergence.CausalDistance`
   produce $D[i,j]=|i-j|$ (link count) on a chain; this satisfies non-negativity, identity of
   indiscernibles, symmetry, and the triangle inequality — a genuine metric.

2. **Conformal factor.** The counting measure fixes the volume element:
   $\sqrt{|g|}=f^{d/2}=\rho\Rightarrow f=\rho^{2/d}$. Verified by exact round-trip.

3. **Conformal metric candidate.** $g=f\cdot\eta$ is symmetric and valid; it is
   **conformally flat** for constant $f$ ($R=0$) and **curved** for varying $f$
   ($R=-0.6145$ at $x=0.5$ for $f=1+\tfrac12 x^2$), verified with `EinsteinTensorBuilder`.
   This matches the Liouville result $K=-\tfrac{1}{2f}\nabla^2\ln f$ for conformally flat 2D.

## Conclusion

The existing causal-distance structure **can** generate a metric tensor candidate
**up to conformal factor**:

- **PRESENT:** distance structure (causal interval count) — numeric, exact.
- **PRESENT:** conformal factor $f=\rho^{2/d}$ from the counting measure — reconstructed.
- **CONSTRUCTIBLE:** conformally-flat candidate $g=f\cdot\eta$ — standard, verified
  ($R=0$ vs $R\neq0$).
- **MISSING:** the conformal *structure* (light cones from the full causal order), which is
  required to fix the metric beyond the conformally-flat ansatz and is imported via the
  external Malament/Hawking–King–McCarthy theorem.

So the conformal factor is genuinely *computable* from Q-event data, but the conformal class
(the light-cone geometry) is not generated — it is imported. The metric tensor candidate is
therefore **partially emergent**: the conformal factor is native, the conformal structure is
external.
