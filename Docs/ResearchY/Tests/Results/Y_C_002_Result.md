# ResearchY-C_002 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/C_SourceGeometry/Y_C_002_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 6/6 tests (Duration ~21 ms)
**Filter:** `FullyQualifiedName~Y_C_002`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_C_002_RadialDefinition` | radial propagation requires derived origin + shell ordering; diameter 8 | ✅ |
| `Y_C_002_OriginTest` | any node formally an origin; identical shell profiles (vertex-transitive) | ✅ |
| `Y_C_002_ShellStructure` | diameter 8 = N/(2K); shells 12/12/…/11; reflection symmetry d(o,k)=d(o,N−k) | ✅ |
| `Y_C_002_Automorphism` | D96 vertex-transitivity → radiality is a gauge/coordinate choice | ✅ |
| `Y_C_002_SpreadingClass` | canonical spreading = tree-local (branching) + global (readout), NOT radial | ✅ |
| `Y_C_002_Run` | Research report | ✅ |

## Key Facts

| Fact | Value |
|---|---|
| diameter | 8 = N/(2K) = 96/12 |
| shells from any node | {1, 12, 12, 12, 12, 12, 12, 12, 11} |
| automorphism group | D96 (vertex-transitive) |
| shell profile origin-dependence | none (identical for all origins) |
| shortest-path vs radial shells | identical (both are BFS layers) |

## Classification

| Category | Canonical? |
|---|---|
| radial | **NO** (no derived origin; no distance coordinate) |
| tree-local | YES (branching ρ_k = μ^k/S, generation depth) |
| resonance/global | YES (spectral projection, |φ_k(n)|²=1/96) |
| hybrid | YES (tree-local + global) — NOT radial |

## Verdict: FAIL (not canonically radial)

Radial propagation requires a preferred origin; C96's D96 vertex-transitivity removes all
preferred sites (C_001). The canonical law is tree-local + global (hybrid). Radial shells
exist only as a formal diffusion model with a chosen origin. **No canonical value was
changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_C_002"
```
