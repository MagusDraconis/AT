# Y_QG_018_Result.md — ResearchY-QG_018 Information-Cosmology Closure Audit

**Test suite:** `AT.Tests/ResearchY/QG_GeometryBridge/Y_QG_018_Tests.cs`
**Run:** 2026-09-01
**Result:** ✅ 6/6 PASSED
**Full suite:** 679/679 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_QG_018"`

---

## Summary

**Question:** Do ΩΛ, Ωm, I_occ, KL(ρ‖uniform), finite observability, and
actualization-information form a mathematically CLOSED chain?

**Verdict:** YES — closure score **90% (9/10)** within the canonical finite-N regime.

## The chain is closed

```
Difference → Distinguishability → Count → ρ → I_occ → {ΩΛ, Ωm, q₀, z_acc}
```

- **Acyclic** — no link points backward.
- **Circularity-free** — ln K independently fixed by QG227 (K ≈ 3).
- **Only the 8 canonical boundaries** as inputs (no new ones introduced).

## Alternative information measures

| Measure | Value | ΩΛ predicted | Matches 0.6839? |
|---|---|---|---|
| **KL(ρ‖uniform)** | **0.7513** | **0.6839** | ✅ |
| squared Hellinger | 0.4211 | 0.3833 | ❌ |
| total variation (½) | 0.5825 | 0.5302 | ❌ |
| chi-squared | 1.5266 | 1.3896 | ❌ |

**Only KL reproduces the observed ΩΛ** — the KL choice is EMERGENT (unique match),
not arbitrary and not forced by the chain.

## Finite vs infinite N

| Case | Closure |
|---|---|
| Finite N = 96 | ✅ CLOSED (ΩΛ = 0.6839) |
| Convergent infinite N | ❌ FAILS (no normalized uniform measure, QG_009) |

## Closure score: 9/10 = 90%

The only failure is the infinite-N case, excluded by the finite-state-space boundary
(QG_008).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_QG_018_DependencyDAG` | the chain is acyclic; only canonical boundaries | ✅ |
| `Y_QG_018_CircularityCheck` | no hidden circularity; ln K independently fixed | ✅ |
| `Y_QG_018_AlternativeMeasure` | only KL reproduces ΩΛ = 0.6839 | ✅ |
| `Y_QG_018_FiniteInfinite` | closure fails for infinite N (uniform reference) | ✅ |
| `Y_QG_018_ClosureScore` | 9/10 = 90% | ✅ |
| `Y_QG_018_Run` | research report | ✅ |

## Conclusion

The information-cosmology chain is CLOSED (90%) within the canonical finite-N regime —
acyclic, circularity-free, with KL as the unique information measure reproducing the
observed ΩΛ = 0.6839. The exact remaining boundary set is the canonical eight. No new
primitive; canonical AT unchanged.
