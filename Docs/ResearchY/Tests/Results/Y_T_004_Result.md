# Y_T_004_Result.md — ResearchY-T_004 Spectral Rigidity Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_004_Tests.cs`
**Run:** 2026-09-08
**Result:** ✅ 3/3 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_004"`

---

## Summary

**Goal:** Rank graph families by spectral rigidity (whether the spectrum uniquely
determines the graph).

**Verdict:** Rigidity is graded. Structured families (complete, path, cycle, grid,
circulant, D96) are maximally rigid (R=1); bipartite and random-sparse families are
degenerate (R<1). D96 sits in the maximally rigid class.

## Rigidity ranking (n=6)

| Family | Count | Degenerate | Isospectral freq | Rigidity R | Perturb. stability |
|---|---|---|---|---|---|
| circulant | 8 | 0 | 0.0000 | 1.0000 | 1.000 |
| path | 360 | 0 | 0.0000 | 1.0000 | 1.000 |
| cycle | 60 | 0 | 0.0000 | 1.0000 | 1.000 |
| grid | 90 | 0 | 0.0000 | 1.0000 | 0.733 |
| complete | 1 | 0 | 0.0000 | 1.0000 | 1.000 |
| random-sparse | 200 | 1 | 0.0050 | 0.9950 | 0.986 |
| all | 32768 | 720 | 0.0220 | 0.9780 | 0.957 |
| bipartite | 5177 | 180 | 0.0348 | 0.9652 | 0.959 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_004_RigidityRanking` | complete/path/cycle/grid R=1; sorted ranking | ✅ |
| `Y_T_004_D96Rigidity` | D96 labeled spectrum → {±1..±6} | ✅ |
| `Y_T_004_Run` | research report | ✅ |

## Classification

| Item | Classification |
|---|---|
| Rigidity of structured families (R=1) | DERIVED |
| Circulant/D96 rigidity | DERIVED |
| Rigidity is graded, non-generic | EMERGENT |
| "All families rigid" | REFUTED |

## Conclusion

Spectral rigidity is graded: maximally rigid (R=1) for complete, path, cycle, grid,
circulant, D96; lower (R<1) for bipartite and random-sparse. D96's circulant spectrum is
a complete, unique, canonical description — its role as canonical attractor is echoed in
spectral rigidity.
