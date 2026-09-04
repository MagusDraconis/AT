# Y_NP_044_Result.md — ResearchY-NP_044 Joint State Necessity Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_044_Tests.cs`
**Run:** 2026-09-04
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_044"`

---

## Summary

**Question:** Does any observed phenomenon force the introduction of Joint States, or
can all currently derived AT results exist without them?

**Verdict: the joint states are an OPTIONAL extension (B) that currently functions as
a CORRESPONDENCE layer (C) — NOT necessary physics (A) for any already-derived result.**
The first empirical result that cannot be reproduced without them is the Bell/CHSH
inequality violation (S = 2√2 > 2).

## Existing derived results (joint-free)

| Result | Form | Joint state? |
|---|---|---|
| D96 spectrum | 95 real frequencies | no |
| A = Σm·#g·occ₂ | 95·44·87 = 363,660 | no |
| M_Pl = v·A³ | 254.37·(363,660)³ = 1.2234e19 GeV | no |
| mass ratios / couplings / ΩΛ | scalar ratios | no |

Canonical state is rank 1; sweeping canonical products gives max CHSH = 2.

## First non-reproducible empirical result

Bell/CHSH violation S = 2√2 > 2 (the Bell pair, rank 2) — no canonical object reaches
it. Teleportation (F=1) and GHZ (τ₃=1) follow, later in the hierarchy.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_044_ExistingResultsWithoutJointStates` | derived results joint-free | ✅ |
| `Y_NP_044_CanonicalNeverViolatesChsh` | canonical CHSH ≤ 2 | ✅ |
| `Y_NP_044_BellViolationRequiresJointState` | Bell CHSH = 2√2 needs rank 2 | ✅ |
| `Y_NP_044_FirstEmpiricalResult` | first forced result = CHSH > 2 | ✅ |
| `Y_NP_044_TeleportationGhzAlsoRequire` | teleportation/GHZ follow Bell | ✅ |
| `Y_NP_044_Classification` | B/C optional/correspondence | ✅ |
| `Y_NP_044_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Existing derived results | **DERIVED** (single-DOF/classical/scalar) |
| Joint states necessary (A) for current AT | **REFUTED** |
| Joint states optional extension (B) | **CONFIRMED** |
| Joint states correspondence layer (C) | **CONFIRMED** |
| Bell/CHSH violation S > 2 | **CORRESPONDENCE** (first forced result) |

## Conclusion

Joint states are optional for the current AT chain and serve as a correspondence layer
hosting observed entanglement. The first forced empirical result is CHSH > 2. Canonical
D96 unchanged.
