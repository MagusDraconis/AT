# Y_D_031_Result.md — ResearchY-D_031 Seed-Origin Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_031_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_031"`

---

## Summary

**Question:** Why does everything begin with a period-3 seed? Is p=3 derived or the
final boundary assumption?

**Verdict:** **p=3 is DERIVED from pairing completeness + convergence**, not a final
boundary assumption. The complete-Z2-pairing requirement (0 unpaired modes, weak-isospin
doublets, D_020) applied to the natural octave-rung size n = p·2^k selects p=3 uniquely:
p=2/4→64 and p=5→80 have 1 unpaired mode (incomplete), p=6→96 fails convergence (density
1/6), and p=3→96 has 0 unpaired and converges. p=3 is the minimal complete period. The
pairing requirement is itself the D_020 observable-sector input — so p=3 is DERIVED,
while the pairing requirement is BOUNDARY.

## Seed Scan

| p | natural n | unpaired | complete Z2 | converges | defects |
|---|---|---|---|---|---|
| 2 | 64 | 1 | NO | yes | 2 |
| **3** | **96** | **0** | **YES** | **yes** | **0** |
| 4 | 64 | 1 | NO | yes | 2 |
| 5 | 80 | 1 | NO | yes | 2 |
| 6 | 96 | 3* | NO | **NO** (density 1/6) | — |

*Canonical `Period3SeedOrigin` unpaired count; the closed-form self-conjugate count
gives 0 at N=96, so p=6 is distinguished by convergence failure (QG160).

## Key measured values

| Quantity | Value |
|---|---|
| unique complete period | **p=3 only** |
| minimal complete period | p=3 (p=1, 2 are incomplete) |
| natural sizes | p=2/4→64, p=3→96, p=5→80, p=6→96 |
| p=6 exclusion | fails convergence (density 1/6) |
| seed half-shift | 3 | 48 (96/2) — holds |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_031_SeedScan` | natural sizes p=2/4→64, p=3→96, p=5→80, p=6→96 | ✅ |
| `Y_D_031_PeriodComparison` | only p=3 has 0 unpaired at the natural size (converging) | ✅ |
| `Y_D_031_PairingCompleteness` | p=2/4/5 incomplete (1 unpaired); p=3 complete | ✅ |
| `Y_D_031_DefectCount` | p=3 natural size is the only zero-defect one | ✅ |
| `Y_D_031_DependencyTrace` | Difference → observable sector → p=3 → octave → N=96 | ✅ |
| `Y_D_031_Run` | Research report | ✅ |

## Conclusion

**p=3 is DERIVED from pairing completeness + convergence.** The complete-Z2-pairing
requirement (0 unpaired modes, weak-isospin doublets, D_020) applied to the natural
octave-rung size n = p·2^k selects p=3 uniquely; p=2/4→64 and p=5→80 have 1 unpaired
(incomplete), p=6→96 fails convergence (density 1/6), and p=3→96 has 0 unpaired and
converges. p=3 is the minimal complete period. The pairing requirement is itself the
D_020 observable-sector input — so p=3 is DERIVED, while the pairing requirement is
BOUNDARY. No canonical value was changed.
