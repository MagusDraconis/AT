# Y_D_026_Result.md — ResearchY-D_026 Compact-Form Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_026_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_026"`

---

## Summary

**Question:** Why is the compact form su(2) selected? Is it physically necessary or an
independent gauge input?

**Verdict:** **su(2) is selected by the physical requirement of finite-dimensional
UNITARY (probability-preserving) representations.** Among the three real forms of
sl(2,C), su(2) is the unique compact one: its generators are bounded, its unitary irreps
are finite-dimensional (the 2j+1 multiplets), and its elements preserve the norm (Born
rule). sl(2,R) and su(1,1) are non-compact (unbounded boosts, infinite-dim unitary reps,
no finite probability conservation). The spectral observables survive ANY real-form
choice; only the weak sector requires finite-dim unitary reps, which su(2) uniquely
provides. The compact-form choice is **EMERGENT from observability** — not derived from
the spectrum, not a free gauge input.

## Comparison Table

| Real form | Compact? | Bounded? | Unitary reps | Weak sector |
|---|---|---|---|---|
| **su(2)** | **YES** | bounded (norm 19.6 at θ=5) | finite-dim (2j+1) | **survives** |
| sl(2,R) | NO | unbounded (norm 148.4 at θ=5) | infinite-dim | lost |
| su(1,1) | NO | unbounded | infinite-dim | lost |

## Key measured values

| Quantity | Value |
|---|---|
| exp(5·iσy) norm (su(2)) | 19.57 (bounded) |
| exp(5·H) norm (sl(2,R)) | 148.4 (unbounded) |
| SU(2) probability after evolution | preserved (1.0) |
| sl(2,R) probability after evolution | not preserved |
| Spectral observables under any real form | survive |
| Weak sector under sl(2,R)/su(1,1) | **lost** |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_026_Compactness` | su(2) unique compact; sl(2,R)/su(1,1) non-compact (unbounded) | ✅ |
| `Y_D_026_UnitaryRepresentations` | compact → finite-dim unitary; non-compact → infinite-dim | ✅ |
| `Y_D_026_ObservableSurvival` | spectral observables survive any real form; weak sector needs su(2) | ✅ |
| `Y_D_026_ProbabilityPreservation` | SU(2) unitary preserves norm; sl(2,R) does not | ✅ |
| `Y_D_026_AlternativeRealForms` | sl(2,R)/su(1,1) break the weak sector | ✅ |
| `Y_D_026_Run` | Research report | ✅ |

## Conclusion

**su(2) is EMERGENT from observability (positivity/normalization/stability).** It is the
unique compact real form of sl(2,C); the spectral observables (doublets, families,
masses, mixings) survive any real-form choice, but the weak sector (W/Z, isospin
doublets) requires finite-dim unitary reps that only su(2) provides. The compact-form
choice is not derived from the D96 spectrum and not a free gauge input — it is forced by
the requirement of finite-dim probability-preserving representations. No canonical value
was changed.
