# Y_D_019_Result.md — ResearchY-D_019 Closure-Only Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_019_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_019"`

---

## Summary

**Question:** If all D96-specific selection rules are removed and only Closure remains,
does Closure still produce N=96?

**Verdict:** **NO.** Closure alone does NOT determine N. The actualization closure
dynamics (QG115/116, `StructureFromContent.AdaptiveNetwork` /
`ActualizationStructures.ReinforcingNetwork`, canonical defaults K=6, damping=0.2,
feedback=0.7) takes the size N as an **input** (the activity array length) and converges
the link structure for that fixed size. Under the canonical persistent pattern, closure
(link-growth → 0) converges for **ALL 269/269 N in [32,300]** — N=96 is not selected at
all. The fixed point is always the degree-12 K=6 ring (links = 6N). Under the
concentrated pattern, only **56/269** converge and **N=96 itself FAILS** (growth
0.1198 > 0.05). N=96 is a **SELECTED closure solution** (D_015: 6|N + span window), not a
closure theorem. **Classification: D) Closure does not determine N.**

## Key measured values

| Quantity | Value |
|---|---|
| Converged N under persistent pattern (N ∈ [32,300]) | **269 / 269** (100%) |
| Converged N under spread pattern | 269 / 269 (100%) |
| Converged N under uniform pattern | 269 / 269 (100%) |
| Converged N under concentrated pattern | 56 / 269 (21%) |
| N=96 growth under concentrated pattern (40/80) | **0.1198 — FAILS** |
| N=64 growth under concentrated pattern (40/80) | 0.0000 — converges |
| Converged link counts | **links = 6N exactly** (384@64, 576@96, 1152@192, 1470@245) |
| Converged degree | uniform **12** at every N |
| N=94…98 growth under persistent pattern | 0.000000 (all identical) |
| Size role | **input** (activity array length); never changed by the dynamics |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_019_ClosureOnly` | closure converges for ALL N under persistent pattern (269/269) | ✅ |
| `Y_D_019_FixedPoints` | fixed point = degree-12 ring (links = 6N) for all tested N | ✅ |
| `Y_D_019_AttractorCount` | converging set all N (persistent/spread/uniform); 56/269 concentrated | ✅ |
| `Y_D_019_N96Uniqueness` | N=96 has no closure signature (adjacent N converge identically) | ✅ |
| `Y_D_019_Counterexamples` | N=96 FAILS closure under concentrated pattern (growth 0.1198) | ✅ |
| `Y_D_019_SizeIsInput` | size enters as the activity array length; never changed | ✅ |
| `Y_D_019_Selection` | classification D — closure does not determine N; N=96 selected (D_015) | ✅ |
| `Y_D_019_Run` | Research report | ✅ |

## Conclusion

**Closure alone does not produce N=96.** Under the canonical persistent pattern, closure
converges for all 269/269 N in [32,300] to the degree-12 K=6 ring (a geometry class,
links = 6N); the size N is an input, not an output. Under the concentrated pattern, N=96
itself fails closure (growth 0.1198). Closure convergence is content-dependent, and no
closure quantity distinguishes N=96. N=96 is therefore a **SELECTED closure solution**
(D_015/D_016 rules: 6|N + span window), not a closure theorem. Classification:
**D) Closure does not determine N** (equivalently C — an effectively infinite family of
sizes satisfies closure).

No canonical value was changed.
