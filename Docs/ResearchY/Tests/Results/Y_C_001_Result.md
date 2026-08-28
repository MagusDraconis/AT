# ResearchY-C_001 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/C_SourceGeometry/Y_C_001_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 6/6 tests (Duration ~19 ms)
**Filter:** `FullyQualifiedName~Y_C_001`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_C_001_CenterNecessity` | Difference needs a reference (uniform/zero mode), not a center | ✅ |
| `Y_C_001_RadialPropagation` | branching is tree-local; radius r=N/2π is a size measure, no radial propagation | ✅ |
| `Y_C_001_ZeroModeSource` | zero mode ω₀=0, constant — reference state, not a source | ✅ |
| `Y_C_001_SymmetryCenter` | C96 translation-invariant, 12-regular, 47 Z2 pairs — no preferred site | ✅ |
| `Y_C_001_ClosureCenter` | closure = N=96 integer (centerless ring); only source = branching root (ρ₀=1/S) | ✅ |
| `Y_C_001_Run` | Research report | ✅ |

## Verdicts

| RQ | Answer |
|---|---|
| RQ1 Difference without center | YES — reference, not center |
| RQ2 closure implies center | NO — N=96 integer, ring |
| RQ3 propagation radial | NO — tree-local + global readout |
| RQ4 zero mode source | NO — reference state, ω₀=0 |
| RQ5 C96 preferred center | NO — translation-invariant |
| RQ6 center by symmetry | eliminated by symmetry |
| RQ7 attractor centerless | YES — regular ring |
| RQ8 closure needs source | NO — branching root is generation-space |

## Conclusion

**Center is ABSENT in space** (circulant symmetry eliminates any preferred site).
**Center is EMERGENT as the branching root** (generation-space source, ρ₀ = 1/S).
**The zero mode is a DERIVED reference state** (uniform background), not a source.
**No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_C_001"
```
