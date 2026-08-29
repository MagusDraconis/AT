# Y_D_035_Result.md — ResearchY-D_035 Multiplet-Requirement Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_035_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_035"`

---

## Summary

**Question:** Why must the self-conjugate mode participate in a degenerate multiplet?

**Verdict:** The self-conjugate mode k=N/2 is **REAL-ONLY** (sin(πn)=0); its eigenvalue
λ=12 has a **1D real eigenspace** at N=64/80/128 (an isolated singlet violating complex
observability) and a **5D eigenspace** at N=96/192. **Complex observability** (every
observable frequency must carry [magnitude, phase], QG218/D_034) requires every
eigenvalue to have **multiplicity ≥ 2**. At N=96 every eigenvalue has mult ≥ 2 (complete
pairing); at N=64 λ=12 has mult 1 — the real-only singlet violates complex observability.
The degenerate multiplet supplies the phase/quadrature partners. **REFINEMENT: complete
pairing is DERIVED from complex observability** — the boundary moves one step deeper,
from '0 unpaired' to 'the observable sector is complex'.

## Self-conjugate eigenspace

| N | λ(N/2) | multiplicity | complex? |
|---|---|---|---|
| 64 / 80 / 128 | 12 | 1 | **NO** (1D real) |
| **96 / 192** | **12** | **5** | **YES** (5D) |

## Key measured values

| Quantity | Value |
|---|---|
| self-conjugate sin quadrature | sin(πn) = 0 (real-only) |
| λ=12 group at N=96 | {16, 32, 48, 64, 80} |
| min multiplicity N=96/192 | 2 (all eigenvalues mult ≥ 2) |
| min multiplicity N=64/80/128 | 1 (violates complex observability) |
| lone singlet consequence | real-only → classical addition (no interference) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_035_SelfConjugateMode` | self-conjugate k=N/2 is real-only (sin(πn)=0) | ✅ |
| `Y_D_035_DegenerateMultiplet` | λ=12 1-fold at 64/80/128, 5-fold at 96/192 | ✅ |
| `Y_D_035_PhaseFreedom` | 1D eigenspace is real-only; 5D group supplies phases | ✅ |
| `Y_D_035_InterferenceLoss` | real-only → classical addition (no interference) | ✅ |
| `Y_D_035_RepresentationClosure` | mult ≥ 2 for every eigenvalue at N=96 | ✅ |
| `Y_D_035_Run` | Research report | ✅ |

## Conclusion

**The self-conjugate mode must participate in a degenerate multiplet because
complex-state observability requires every eigenvalue to have multiplicity ≥ 2.** The
self-conjugate mode k=N/2 is real-only (sin(πn)=0); its eigenvalue λ=12 has a 1D real
eigenspace at N=64/80/128 (an isolated singlet violating complex observability) and a 5D
eigenspace at N=96/192 (the multiplet supplies the phase/quadrature partners).
**Complete pairing (0 unpaired) is DERIVED from complex observability** — the boundary
moves one step deeper, to "the observable sector is complex." No canonical value was
changed.
