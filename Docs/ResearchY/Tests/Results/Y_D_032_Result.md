# Y_D_032_Result.md — ResearchY-D_032 Pairing-Requirement Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_032_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_032"`

---

## Summary

**Question:** Why must the observable sector be completely paired (0 unpaired modes)?

**Verdict:** The pairing **STRUCTURE** is **DERIVED** (D_021: cos/sin quadrature pairs
from oscillation); the **COMPLETENESS** (0 unpaired) is **BOUNDARY** — the
observable-sector requirement that every frequency carry a full doublet/phase structure.
The self-conjugate mode k=N/2 has sin(πn)=0 (vanishing quadrature); complete pairing
requires it to sit in a degenerate group (λ=12 5-fold at N=96/192, 1-fold at
N=64/80/128). The unpaired mode has no weak-isospin doublet partner. Not required by
count conservation (B) or closure (D); required by the doublet-structure observability
(the observable-sector construction, D_020).

## Self-conjugate degeneracy

| N | λ(N/2) | multiplicity | unpaired | pairing |
|---|---|---|---|---|
| 64 | 12 | 1 | 1 | INCOMPLETE |
| 80 | 12 | 1 | 1 | INCOMPLETE |
| **96** | **12** | **5** | **0** | **COMPLETE** |
| 128 | 12 | 1 | 1 | INCOMPLETE |
| 192 | 12 | 5 | 0 | COMPLETE |

## Key measured values

| Quantity | Value |
|---|---|
| self-conjugate sin quadrature | sin(πn) = 0 (vanishing) |
| λ=12 group at N=96 | {16, 32, 48, 64, 80} (5-fold) |
| every eigenvalue mult ≥ 2 at N=96 | YES |
| unpaired mode consequence | no doublet partner (weak-isospin reading fails) |
| required by count conservation | NO |
| required by closure | NO |
| required by doublet observability | YES (D_020) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_032_UnpairedModeTest` | self-conjugate k=N/2 has vanishing sin; unpaired at 64/80/128, not 96/192 | ✅ |
| `Y_D_032_ObservableCompleteness` | every eigenvalue has mult ≥ 2 at N=96 | ✅ |
| `Y_D_032_RepresentationClosure` | λ=12 5-fold at 96/192, 1-fold at 64/80/128 | ✅ |
| `Y_D_032_SymmetryClosure` | reflection maps cos→cos; group supplies partners | ✅ |
| `Y_D_032_DependencyTrace` | Difference → observable sector → complete pairing → p=3 → N=96 | ✅ |
| `Y_D_032_Run` | Research report | ✅ |

## Conclusion

**The pairing structure is DERIVED (oscillation, D_021); the completeness (0 unpaired)
is the BOUNDARY observable-sector requirement.** The self-conjugate mode k=N/2 has
sin(πn)=0; complete pairing requires it to sit in a degenerate group (λ=12 5-fold at
N=96/192, 1-fold at N=64/80/128). The unpaired mode has no weak-isospin doublet partner.
Complete pairing is required by the doublet-structure observability (D_020), not by
count conservation or closure. Everything downstream (p=3, N=96) is DERIVED. No
canonical value was changed.
