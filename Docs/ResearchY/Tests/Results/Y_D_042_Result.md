# Y_D_042_Result.md — ResearchY-D_042 Fundamental-Ratio Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_042_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_042"`

---

## Summary

**Question:** Does D96 contain a fundamental ratio analogous to circumference/diameter = π?

**Verdict:** YES for the ROLE. **span = ωmax/ω₁ = 6.4025** is the structural ratio of
the C96 ring — the natural dimensionless reference for the frequency-scale hierarchy
(D_028). The π-analogy **inverts the classification**: π is transcendental (value
BOUNDARY, B_002); span is algebraic (integer-matrix spectrum), hence **DERIVED**.
π is imported; span is derived.

## Invariance

| Test | Result |
|---|---|
| invariant under N-preserving automorphisms (k→5k,7k,11k,13k) | **YES** (spectrum multiset preserved) |
| universal across N (like π across circles) | **NO** (span ~ 0.0578·N, monotone 4.02→12.78) |

## Key measured values

| Ratio | Value (N=96) | Role |
|---|---|---|
| span = ωmax/ω₁ | 6.4025 | family count 3 (D_028) |
| λmax/λ₂ | 40.99 | scale gap |
| ω₂/ω₁ | 1.9734 | the octave (D_030) |
| A³ | 4.8094e16 | Planck content (D_007) |
| ω₁ | 0.6216 | universal dimensionless reference (D_008) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_042_FundamentalRatio` | span = ωmax/ω₁ = 6.4025 is the structural ratio | ✅ |
| `Y_D_042_InvariantScan` | ratios invariant under N-preserving automorphisms | ✅ |
| `Y_D_042_HierarchyGeneration` | span → families; ω₂/ω₁ → octave; A³ → Planck | ✅ |
| `Y_D_042_NStability` | no ratio is N-invariant (span monotone) | ✅ |
| `Y_D_042_PhysicsConnection` | ω₁, span, A³ connect to physics | ✅ |
| `Y_D_042_Run` | Research report | ✅ |

## Conclusion

D96 contains a fundamental ratio — **span = ωmax/ω₁ = 6.4025** — that plays π's
structural-role but is **DERIVED (algebraic)** where π is **BOUNDARY (transcendental)**.
It is invariant under N-preserving ring automorphisms but **NOT universal across N**.
The ratio family (span, λmax/λ₂, ω₂/ω₁, A³) generates the family/mode/scale/Planck
hierarchies — all DERIVED. π's value remains BOUNDARY (B_002, unchanged). No new
primitive; canonical AT unchanged.
