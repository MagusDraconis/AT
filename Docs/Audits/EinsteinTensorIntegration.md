# Einstein Tensor Integration Program — Report

**Test file:** `AT.Tests/ResearchXC/EinsteinTensorIntegrationTests.cs`
**Result:** **PASSED (4/4).**
**Minimum code added:** `AT.Core/ResearchXC/EinsteinTensorBuilder.cs` (~200 lines, standard
differential geometry).

---

## Results (standard chain, verified numerically)

| # | Test | flat metric | unit 2-sphere | unit 3-sphere | Verdict |
|---|---|---|---|---|---|
| 1 | ChristoffelBuilder_ComputesFlatMetric | $\max\|\Gamma\|=0$ | — | — | **works** |
| 2 | ChristoffelBuilder_ComputesSphereMetric | — | $\Gamma^\theta_{\phi\phi}=-0.5000$ | — | **works** |
| 3 | RicciTensor_ComputesKnownExamples | $R=0$ | $R_{\theta\theta}=1$, $R_{\phi\phi}=0.5$, $R=2$ | — | **works** |
| 4 | EinsteinTensor_ComputesKnownExamples | — | $G_{\mu\nu}=0$ (2D) | $G=-\mathrm{diag}(1,0.5,0.25)$ | **works** |

The 3-sphere produces a **non-trivial** Einstein tensor $G_{\mu\nu}=-g_{\mu\nu}$, proving the
full chain $g\to\Gamma\to$Riemann$\to$Ricci$\to G$ is computable end-to-end with standard math
(dimension $\ge3$ required; 2D $G\equiv0$).

---

## Insertion points in existing analyzers

| Analyzer | Current content | Nature | Insertion point |
|---|---|---|---|
| `QuantumGravityEmergenceAnalyzer` | `GeoStep[]` (lines 32–36): Γ, Riemann, Ricci, Einstein, field equations **as strings** | described only | `BuildD(GeoStep[])` → add a computed column calling `EinsteinTensorBuilder` |
| `GrBridgeAnalyzer` | `CurvatureInterpretations()` (lines 102–130): "scalar R only", "loses tensor structure" | qualitative claims | new `ComputedCurvature(MetricField)` method |
| `GrBridgeAnalyzer` | `AuditBridgeSteps()` (lines 13–60): metric marked "External theorem" | no computation | unchanged — metric remains external |

## Minimum code required

`EinsteinTensorBuilder` **is** the entire missing implementation:

- `Christoffel(MetricField, x, h)` → $\Gamma^\lambda_{\mu\nu}$
- `Riemann(...)` → $R^\rho{}_{\sigma\mu\nu}$
- `Ricci(...)`, `RicciScalar(...)` → $R_{\mu\nu}$, $R$
- `Einstein(...)` → $G_{\mu\nu}=R_{\mu\nu}-\tfrac12 R g_{\mu\nu}$

No new physics — pure Riemannian geometry via central finite differences + Gauss–Jordan
inversion. The two analyzers need only ~5 lines each to call it.

## The integration boundary (what still cannot be integrated)

The builder takes a **metric field $g_{\mu\nu}(x)$ as input**. AT has **no native metric
field** — $g_{\mu\nu}$ arrives only via the *external* Malament/Hawking–King–McCarthy theorem
(`GrBridgeAnalyzer.AuditBridgeSteps`, tier 1, "External theorem"). Therefore:

- **Computable today:** the chain *after* a metric is supplied (the whole point of this program).
- **Not computable today:** the *source* of the metric from Q-events — that remains the
  external causal-set reconstruction, not AT code.

## Integration roadmap

1. **Done (this program):** minimal, verified `EinsteinTensorBuilder` (4/4 tests pass).
2. **Next (mechanical):** add a computed column to `GeoStep` output so the string claims are
   backed by `EinsteinTensorBuilder` values for any supplied metric.
3. **Blocked (scientific):** a *native* metric field $g_{\mu\nu}$ derived from Q-event counting
   (Myrheim/BDG) — without it, the builder is fed an external metric, not a AT one.

## Conclusion

Yes — the tested chain **can** be integrated. The minimum code is ~200 lines and is now
implemented and verified. Integration is **mechanical** at the analyzer level but remains
**blocked** at the one genuinely open step: producing $g_{\mu\nu}$ from Q-events (external
theorem, not AT-native).
