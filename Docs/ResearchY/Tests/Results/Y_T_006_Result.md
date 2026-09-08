# Y_T_006_Result.md — ResearchY-T_006 Darwinian Dominance Emergence

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_006_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 3/3 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_006"`

---

## Summary

**Goal:** Test whether dominant attractors emerge from Darwinian resource competition
rather than spectral structure alone.

**Verdict:** Dominance emerges only after competition — model-free fitness `w=m/λ` drives
every model to a single fittest eigenspace (D_init 1–33% → D_final 100%). H1 (D96 faster
than random) SUPPORTED; H2 (fewer survivors) REFUTED; H3 (complete degenerate) SUPPORTED.

## Competition results

| Model | A | D_init | D_final | N_eff | Extinct | t_dom | w_max/w_2nd |
|---|---|---|---|---|---|---|---|
| D96 | 44 | 0.062 | 1.000 | 1.000 | 0.977 | 3 | 3.89 |
| D96-3D | 12 | 0.208 | 1.000 | 1.000 | 0.917 | 13 | 1.20 |
| physical max-sep | 48 | 0.021 | 1.000 | 1.000 | 0.979 | 4 | 2.00 |
| unphysical clustered | 3 | 0.333 | 1.000 | 1.000 | 0.667 | 2 | 5.00 |
| random sparse | 95 | 0.010 | 1.000 | 1.000 | 0.989 | 50 | 1.05 |
| complete | 1 | 0.990 | 1.000 | 1.000 | 0.000 | 1 | ∞ |

## Hypotheses

| H | Claim | Result |
|---|---|---|
| H1 | spectral organization accelerates dominance | SUPPORTED (D96 3 vs random 50 steps) |
| H2 | D96 fewer effective survivors than random | REFUTED (both N_eff≈1) |
| H3 | complete graph degenerate | SUPPORTED (A=1, D_init=0.99) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_006_Competition` | dominance emerges (D_init→D_final); complete degenerate | ✅ |
| `Y_T_006_Hypotheses` | H1 supported, H2 refuted (N_eff→1) | ✅ |
| `Y_T_006_Run` | research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| Replicator converges to fittest eigenspace | DERIVED |
| Dominance emerges only after competition | EMERGENT |
| Spectral structure alone determines dominance | REFUTED |
| D96 produces fewer effective survivors | REFUTED |

## Conclusion

Dominant attractors emerge from Darwinian resource competition, not spectral structure.
This resolves the T_005 arc: spectral organization compresses/accelerates, but dominance
itself is the selection (w=r/c) layer.
