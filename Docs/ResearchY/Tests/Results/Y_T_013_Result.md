# Y_T_013_Result.md — ResearchY-T_013 Compression Origin Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_013_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 5/5 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_013"`
**Note:** ~90s runtime (D96^3 has 20,811 distinct eigenvalues).

---

## Summary

**Goal:** Explain why D96^3 compresses 20,811 eigenvalues into 16 survivors, and derive
C = eigenvalues / survivors.

**Answer:** The 3D sum structure gives a huge A (20,811 distinct eigenvalues); selection
w = m/λ keeps only the near-gap modes (S∞ = 16 ≈ the 18 near-gap modes). C = A/S∞ = 1300 is
DERIVED from the spectrum but dominated by A, not fitness peakedness.

## Compression measures (μ=0.01, β=1)

| model | A | S∞ | C | conc | ent-red | near-gap | max mult | basin |
|---|---|---|---|---|---|---|---|---|
| D96 | 44 | 5 | 8.8 | 0.362 | 0.88 | 2 | 6 | 0.105 |
| D96^3 | 20,811 | 16 | 1300.7 | 0.0006 | 0.20 | 18 | 562 | 0.0019 |
| random | 95 | 17 | 5.6 | 0.017 | 0.03 | 71 | 1 | 0.179 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_013_CompressionRatio` | C = A/S∞ well-defined and deterministic | ✅ |
| `Y_T_013_D96CubedCompressesMost` | D96^3 largest A and C | ✅ |
| `Y_T_013_Mechanism` | S∞ ≈ near-gap; A dominates, not peakedness | ✅ |
| `Y_T_013_Classification` | DERIVED C; EMERGENT value; REFUTED peakedness-driver | ✅ |
| `Y_T_013_Run` | assumptions-first scientific report | ✅ |

## Conclusion

C = A/S∞ is DERIVED from the spectrum and dominated by the landscape size A (20,811 for D96^3),
not by fitness peakedness (D96 is most peaked yet compresses least). The value 1300 is EMERGENT.
