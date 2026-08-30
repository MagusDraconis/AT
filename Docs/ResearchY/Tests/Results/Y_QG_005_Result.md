# Y_QG_005_Result.md — ResearchY-QG_005 Count-to-Geometry Origin Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_005_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_005"`

---

## Summary

**Question:** Why does count structure generate geometry?

**Verdict:** Geometry is a **NECESSARY consequence of distinguishability counting**
(option C) — not fundamental (A), not informational (B).

## Removal tests

| Removal | Survivor |
|---|---|
| remove metric | **count structure survives** (ρ needs no g) |
| remove count | **geometry undefined** (g = ρ^(2/d)η needs ρ) |

## The minimal principle (QG207)

```
√(−g) = ρ^(kd/2) = ρ  ⟹  k = 2/d
g = ρ^(2/d)η is the UNIQUE conformal-flat metric preserving the count
```

## Prove/refute

**PROVEN — geometry IS the measurement of the distinguishability density:** the ruler
must preserve the density's volume (√(−g) = ρ).

## The split

```
Count Structure (ρ) → Geometry (g) and Information (I = KL(ρ‖uniform))
```

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_005_MetricRemoval` | count survives without the metric | ✅ |
| `Y_QG_005_CountRemoval` | geometry undefined without count | ✅ |
| `Y_QG_005_GeometryNecessity` | geometry is count-derived (option C) | ✅ |
| `Y_QG_005_DensityToMetric` | √(−g) = ρ forces k = 2/d | ✅ |
| `Y_QG_005_InformationGeometrySplit` | the split: count → {geometry, information} | ✅ |
| `Y_QG_005_Run` | research report | ✅ |

## Conclusion

Geometry is a NECESSARY consequence of distinguishability counting (option C): the
measure-preserving metric g = ρ^(2/d)η is the unique ruler required to measure the
count density (√(−g) = ρ, QG207). Geometry IS the measurement of the
distinguishability density. No new primitive; canonical AT unchanged.
