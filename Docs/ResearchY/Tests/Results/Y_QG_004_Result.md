# Y_QG_004_Result.md — ResearchY-QG_004 ρ Nature Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_004_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_004"`

---

## Summary

**Question:** Why does ρ generate both geometry and information?

**Verdict:** ρ is fundamentally a **COUNT STRUCTURE** (option C) — the normalized
counting measure ρ_k = count_k/total. Geometry and information are its two derived
faces.

## Removal tests

| Removal | Survivor |
|---|---|
| remove geometry (g) | **information survives** (I needs no metric) |
| remove information (I) | **geometry survives** (g needs no KL) |
| remove count structure (ρ) | **BOTH vanish** (both are functions of ρ) |

## Primitive comparison

| Object | Other face survives without it? | Primitive? |
|---|---|---|
| geometry (g) | YES | NOT the primitive |
| information (I) | YES | NOT the primitive |
| **count structure (ρ)** | **NO — both vanish** | **YES — THE PRIMITIVE** |

## Minimal description

```
ρ_k = count_k/total,   Σρ_k = 1
```

The normalized counting measure — nothing more needed.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_004_GeometryRemoval` | information survives without geometry | ✅ |
| `Y_QG_004_InformationRemoval` | geometry survives without information | ✅ |
| `Y_QG_004_CountRemoval` | both vanish without the count structure | ✅ |
| `Y_QG_004_PrimitiveComparison` | count is the most primitive | ✅ |
| `Y_QG_004_DensityNature` | ρ is the normalized counting measure | ✅ |
| `Y_QG_004_Run` | research report | ✅ |

## Conclusion

ρ is fundamentally a count structure (option C): geometry (g = ρ^(2/d)η) and
information (I = KL(ρ‖uniform)) are its two derived faces. Remove geometry →
information survives; remove information → geometry survives; remove the count
structure → both vanish. Count is the primitive. No new primitive; canonical AT
unchanged.
