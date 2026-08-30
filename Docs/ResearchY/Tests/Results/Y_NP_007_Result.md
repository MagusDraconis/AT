# Y_NP_007_Result.md — ResearchY-NP_007 Coupling Field Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_007_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_007"`

---

## Summary

**Question:** Does Actualization define a coupling field between distinguishable states?

**Verdict:** Actualization defines a **STATIC COUPLING NETWORK** — the interference
fabric of the state space — with Born-derived link weights κ = 2√(ρ_Aρ_B), but NOT a
propagating field.

## The link structure

```
I = ρ_A + ρ_B + 2√(ρ_Aρ_B)·cos(θ_A − θ_B)   (Born QG216, complex state D_036)
κ = 2√(ρ_Aρ_B)                               (network link weight, DERIVED)
```

The interference cross-term IS the link between any two superposed states; its
amplitude is the coupling coefficient — fixed by the amplitudes, not an external field.

## Determination

| Option | Verdict |
|---|---|
| A) local state updates only | PARTIAL — evolution is local (self-rate, D_041) |
| B) link-mediated influence | **YES** — interference cross-term links states observably |
| C) field-like propagation | **NO** — static network, no propagating field |

## Network properties

| Property | Present? | Classification |
|---|---|---|
| count/information flow | YES | DERIVED (M_005) |
| reciprocity | YES | EMERGENT (D_037) |
| phase flow | NO | absent (NP_005) |
| collective modes | YES (in-phase 1.866 / anti-phase 0.134) | DERIVED |
| propagating field | NO | BOUNDARY |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_007_LinkInfluence` | interference cross-term links superposed states | ✅ |
| `Y_NP_007_CountFlow` | Born redistribution conserves Σρ | ✅ |
| `Y_NP_007_PhaseFlow` | no cross-phase flow (local evolution) | ✅ |
| `Y_NP_007_NetworkCoupling` | κ = 2√(ρ_Aρ_B) is the network link weight | ✅ |
| `Y_NP_007_CollectiveModes` | in-phase/anti-phase superpositions | ✅ |
| `Y_NP_007_DependencyTrace` | chain to the static network | ✅ |
| `Y_NP_007_Run` | research report | ✅ |

## Conclusion

Actualization defines a static coupling network — the interference fabric of the state
space — with Born-derived link weights κ = 2√(ρ_Aρ_B). Link-mediated influence (B) and
collective modes exist; no propagating field (C) and no phase flow (synchronization
remains absent). No new primitive; canonical AT unchanged.
