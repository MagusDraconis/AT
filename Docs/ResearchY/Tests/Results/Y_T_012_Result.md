# Y_T_012_Result.md — ResearchY-T_012 Origin of 19 Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_012_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_012"`
**Note:** ~90s runtime (the D96⊗D96⊗D96 tensor spectrum has 20,811 distinct eigenvalues).

---

## Summary

**Goal:** Determine whether the historical ~19-species count emerges from D96^3 (cubic
D96 ⊗ D96 ⊗ D96) rather than the 1D D96 ring.

**Answer:** D96^3 raises the survivor count from D96's 5 to **16** (distance 3 from 19) — a
DERIVED 3D density-of-states effect — but does not hit 19 exactly; random (17, distance 2) is
the nearest natural value. The match is a CORRESPONDENCE (within the legacy ±5 tolerance), not a
derivation.

## Results (μ=0.01, β=1)

| model | modes | distinct non-zero λ | S∞ | distance from 19 |
|---|---|---|---|---|
| D96 (1D) | 96 | 44 | 5 | 14 |
| D96^3 | 884,736 | 20,811 | 16 | 3 |
| random | 96 | 95 | 17 | 2 |

Sector splitting: 1-axis 5 · 2-axis 5 · 3-axis 21.
Oh irreps: A 5 · T 6 · G 31.
Cumulative discovery (fittest-init): 8.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_012_D96CubedSurvivors` | 96³ modes; D96^3 S∞ > D96 | ✅ |
| `Y_T_012_SectorIrrepDecomposition` | 3-axis sector densest, S∞=21 within 3 of 19 | ✅ |
| `Y_T_012_Discovery` | cumulative discovery bounded | ✅ |
| `Y_T_012_DoesNineteenEmerge` | D96^3 nearer 19 than D96; random nearest; ≠19 | ✅ |
| `Y_T_012_Classification` | DERIVED raise; CORRESPONDENCE ±5; REFUTED exact 19 | ✅ |
| `Y_T_012_Run` | assumptions-first scientific report | ✅ |

## Conclusion

D96^3 raises diversity to ~16 (cubic sector 21) — DERIVED from the 3D density of states and a
CORRESPONDENCE to ~19 within ±5 — but does not exactly derive 19; random (17) remains nearest.
