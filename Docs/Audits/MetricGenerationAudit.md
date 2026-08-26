# Metric Generation Audit — Report

**Test file:** `AT.Tests/ResearchXC/MetricGenerationTests.cs`
**Result:** **PASSED (4/4).**

---

## Results

| # | Test | Finding | Classification |
|---|---|---|---|
| 1 | QEvents_DefineDistanceStructure | causal distance $N^{1/d}$ recovers depth $D$ exactly (d=2,3,4); dimension recovered = 4.000000 | **PRESENT** |
| 2 | DistanceStructure_DefinesMetricCandidate | candidate is a conformal class + factor, **text only**; GrBridge "Metric $g_{\mu\nu}$ from N" = **External theorem** | **PARTIAL** |
| 3 | MetricCandidate_IsCoordinateInvariant | Ricci scalar $R=2$ identical in two charts of the same sphere | **standard criterion holds** |
| 4 | MetricGeneration_PresentOrMissing | distance PRESENT, metric candidate PARTIAL, full $g_{\mu\nu}$ **MISSING** | **verdict** |

---

## What AT already has (numeric, functional)

`CausalUniverse.CausalVolume(D,d)=D^d` and `GeometryEmergence.CausalDistance(V,d)=V^{1/d}` —
the causal interval count **does** define a distance structure, and it is exact. Dimension is
recovered from volume growth ($d = d\ln N / d\ln D$). This is the only *computable* step in
the Q-events → metric chain.

## What AT describes but does not compute

The metric candidate is `GeometryEmergence.MetricRecovery` — a **string**:

> "causal order → light cone (conformal metric); interval volume → conformal factor"

and `GrBridgeAnalyzer.AuditBridgeSteps()` marks "Metric $g_{\mu\nu}$ from N" with
**DerivationStatus = "External theorem", IsAtNative = false**. So the conformal *recipe*
exists as text, but there is **no numeric tensor $g_{\mu\nu}$** anywhere.

## Coordinate invariance

A valid metric candidate must transform covariantly. Using the standard-geometry builder,
the Ricci scalar of the unit 2-sphere is $R=2$ in two different charts
($g=\mathrm{diag}(1,\sin^2\theta)$ vs $g=\mathrm{diag}(4,\sin^2 2\theta')$) — the scalar
invariant is preserved under the coordinate change. AT's candidate (a conformal class) is
invariant *by construction*, but this is vacuous: there is no full tensor to transform.

## Conclusion

AT does **not** contain enough information to *generate* $g_{\mu\nu}$ from Q-events.

- **PRESENT:** distance structure from Q-event causal intervals (numeric, exact).
- **PARTIAL:** metric candidate as a conformal recipe (text only).
- **MISSING:** the full metric tensor — imported via the external Malament /
  Hawking–King–McCarthy theorem, not computed from Q-events.

The metric is *described* and *imported*, but not *generated*. This is the exact source of the
"metric → operator" gap identified in CurvedSpaceProgram.md: without a native $g_{\mu\nu}$,
the EinsteinTensorBuilder (which needs a metric field as input) cannot be fed from Q-events.
