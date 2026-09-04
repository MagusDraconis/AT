# Y_NP_045_Result.md — ResearchY-NP_045 CHSH Reality Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_045_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_045"`

---

## Summary

**Question:** Must AT accept CHSH violations as fundamental physics?

**Verdict: YES — the joint-state sector is REQUIRED PHYSICS.** The Bell/CHSH violation
(S = 2√2 > 2) is a robust, loophole-free empirical fact that canonical AT (CHSH ≤ 2)
cannot reproduce. The joint-state sector reproduces it (Bell CHSH = 2√2, teleportation
F = 1, GHZ τ₃ = 1) and enters as a CORRESPONDENCE layer (hosted, non-derived).

## Evidence inventory

| Class | Year | Content |
|---|---|---|
| Bell 1964 | 1964 | local realism ⇒ \|S\| ≤ 2 |
| CHSH 1969 | 1969 | inequality S ≤ 2 |
| Aspect | 1982 | first violation S ≈ 2.7 |
| Zeilinger | 1997+ | teleportation + GHZ |
| Loophole-free | 2015 | S > 2 closed |

## Options

- A) CHSH > 2 is a physical fact → **CONFIRMED**.
- C) reproducible without joint states → **REFUTED** (canonical max CHSH = 2).

## Observations explained / missed

| Sector | Explained | Missed |
|---|---|---|
| Canonical AT | 0 / 4 | Bell, teleportation, GHZ, W |
| Joint-state sector | 4 / 4 | none |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_045_EvidenceInventory` | four evidence classes | ✅ |
| `Y_NP_045_CanonicalCannotReproduce` | canonical CHSH ≤ 2 | ✅ |
| `Y_NP_045_JointStateReproduces` | Bell/teleportation/GHZ | ✅ |
| `Y_NP_045_OptionCRefuted` | no joint-state-free reproduction | ✅ |
| `Y_NP_045_ObservationsExplainedMissed` | 0/4 vs 4/4 | ✅ |
| `Y_NP_045_Consistency` | both sectors consistent | ✅ |
| `Y_NP_045_Classification` | REQUIRED PHYSICS | ✅ |
| `Y_NP_045_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| CHSH > 2 as a physical fact | **CONFIRMED** |
| Reproducing without joint states | **REFUTED** |
| Joint states REQUIRED PHYSICS | **CONFIRMED** |
| Joint states CORRESPONDENCE layer | **CONFIRMED** |
| Joint states OPTIONAL (NP_044) | **REFINED** (required for observed Bell violation) |

## Conclusion

AT must accept CHSH violations as fundamental physics. The joint-state sector is
REQUIRED PHYSICS for a complete theory of observed entanglement, hosted as a
correspondence layer. Canonical D96 unchanged.
