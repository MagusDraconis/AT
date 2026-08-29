# Y_D_040_Result.md — ResearchY-D_040 Boundary Reclassification Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_040_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_040"`

---

## Summary

**Question:** Which D_020 boundary assumptions remain after D_021–D_039?

**Verdict:** The D-chain reduces to exactly **four irreducible boundary inputs**:

```
B_final = { {Difference, η}              (primitives, D_027/D_039)
          , {Z2-paired (complex) sector} (observable-sector input, D_020)
          , {3 octave families}          (span ∈ [4,8) window, D_020)
          , {SU(2) gauge + j=1/2}        (weak-isospin gauge input, D_022/D_024) }
```

## Reclassification (old → new)

| Object | Old | New | Source |
|---|---|---|---|
| complete pairing | BOUNDARY | **DERIVED** | D_035 (mult ≥ 2 from complex observability) |
| singleton prohibition | BOUNDARY | **DERIVED** | D_035/D_037 (real-only singlet, mult < 2) |
| p=3 seed | BOUNDARY | **DERIVED** | D_031 (unique complete-pairing period) |
| N=96 | BOUNDARY | **DERIVED** | D_031/D_020 (unique zero-defect octave rung) |
| su(2) compact-form | BOUNDARY | **EMERGENT** | D_026 (selected by observability) |
| state identity | EMERGENT | **DERIVED** | D_039 (Difference applied) |
| Z2-paired sector | BOUNDARY | **BOUNDARY** | confirmed |
| 3 octave families | BOUNDARY | **BOUNDARY** | confirmed |
| SU(2) gauge + j=1/2 | BOUNDARY | **BOUNDARY** | confirmed |
| {Difference, η} | BOUNDARY | **BOUNDARY** | confirmed |

## Key measured values

| Quantity | Value |
|---|---|
| min mult N=96 | 2 (complete pairing) |
| min mult N=64/80/128 | 1 (incomplete) |
| self-conjugate λ=12 mult | 5 at N=96, 1 at N=80 |
| N=96 octave rung | 96 = 3·2⁵; 6\|96 ✓; 80 not 6\|N |
| other octave rungs | 48 (2 fam), 192 (4 fam) — excluded by 3-family window |
| final boundary set size | 4 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_040_BoundaryInventory` | the four boundary elements + reclassified set | ✅ |
| `Y_D_040_Reclassification` | old → new for complete pairing, p=3, N=96, singlet, su(2), state identity | ✅ |
| `Y_D_040_DependencyConsistency` | DAG acyclic; every DERIVED object has a path | ✅ |
| `Y_D_040_ContradictionCheck` | the six contradictions are all resolved | ✅ |
| `Y_D_040_IrreducibleBoundary` | removing any boundary element breaks selection | ✅ |
| `Y_D_040_Run` | Research report | ✅ |

## Conclusion

The D-chain reduces to exactly **four irreducible boundary inputs**: {Difference, η},
{Z2-paired (complex) sector}, {3 octave families}, and {SU(2) gauge + j=1/2}.
Complete pairing, the singleton prohibition, p=3, 6|N, and N=96 are all **DERIVED**;
the su(2) compact-form is **EMERGENT**; state identity is **DERIVED** (Difference
applied). Reciprocity, complex observability, observability, and the weak-isospin
reading remain **EMERGENT**. The chain is monotone — boundaries only moved downward or
were confirmed; no contradictions remain. No new primitive; canonical AT unchanged.
