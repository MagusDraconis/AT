# Y_T_007_Result.md — ResearchY-T_007 Bounded Innovation Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_007_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_007"`

---

## Summary

**Goal:** Test whether Darwinian evolution (replicator + mutation + extinction + resource
constraint) on a spectral landscape produces a finite, saturated species count.

**Verdict:** **BOUNDED.** All six landscapes saturate to a stable mutation–selection balance
that is independent of runtime. D96 concentrates diversity (5 species) relative to random
(17). Removing the carrying capacity (open-landscape control) makes innovation genuinely
unbounded — resources are what bound innovation.

## Evolution results

| Model | A | S∞ | N_eff | D | H | ext | col | t_sat |
|---|---|---|---|---|---|---|---|---|
| D96 | 44 | 5 | 1.093 | 0.985 | 0.089 | 39 | 0 | 7 |
| D96-3D (4×4×6) | 12 | 9 | 2.904 | 0.559 | 1.066 | 3 | 0 | 29 |
| random sparse | 95 | 17 | 8.415 | 0.230 | 2.130 | 78 | 0 | 268 |
| complete | 1 | 1 | 1.000 | 1.000 | 0.000 | 0 | 0 | 1 |
| physical max-sep | 48 | 5 | 1.290 | 0.938 | 0.254 | 43 | 0 | 14 |
| unphysical clustered | 3 | 3 | 1.088 | 0.986 | 0.084 | 0 | 0 | 1 |

## Critical answers

| # | Question | Answer |
|---|---|---|
| C1 | Does diversity saturate? | YES — every model reaches a plateau (t_sat ≥ 0) |
| C2 | Is saturation independent of runtime? | YES — S∞ and survivors identical at T=20000 and T=40000 |
| C3 | Does D96 produce lower asymptotic diversity? | YES — S∞(D96)=5 < S∞(random)=17 |
| C4 | Is a finite attractor landscape observed? | YES — survivors are a fixed, finite, stable set (≤ A) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_007_Saturation` | diversity reaches a plateau for all models | ✅ |
| `Y_T_007_RuntimeIndependence` | S∞ / survivors / entropy identical at 2× runtime | ✅ |
| `Y_T_007_D96LowerDiversity` | S∞(D96) < S∞(random) | ✅ |
| `Y_T_007_FiniteAttractor` | survivors form a finite stable set; complete degenerate | ✅ |
| `Y_T_007_UnboundedControl` | open landscape: UNBOUNDED without cap, BOUNDED with cap | ✅ |
| `Y_T_007_Classification` | boundedness DERIVED; value EMERGENT | ✅ |
| `Y_T_007_Run` | assumptions-first research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| Species count is bounded (finite landscape + crowding) | DERIVED |
| Saturated diversity value | EMERGENT |
| "Mutation drives unbounded innovation" on a finite landscape | REFUTED |
| Diversity saturates independent of runtime | DERIVED |
| Resource constraint bounds innovation | DERIVED |

## Conclusion

Darwinian evolution on a spectral landscape produces a finite species count: diversity
saturates to a stable mutation–selection balance, independent of runtime and at or below the
landscape size. Boundedness is DERIVED; the value EMERGENT; "unbounded innovation" REFUTED.
D96's sharp fitness gaps concentrate diversity (5 vs 17 random), completing the T_005→T_006→T_007
arc: compression → dominance → bounded, compressed coexistence.
