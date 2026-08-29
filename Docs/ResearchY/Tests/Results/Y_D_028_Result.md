# Y_D_028_Result.md — ResearchY-D_028 Span-Origin Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_028_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_028"`

---

## Summary

**Question:** Why is span ≈ 6.4025? Is span the true selection quantity behind D96?

**Verdict:** **span is a DERIVED monotone function of N, NOT a selector.** span =
ω_max/ω_min; ω_max → √12 ≈ 3.46 (antipodal mode, even N) and ω_min ~ (2π√91)/N ≈ 59.9/N
(fundamental mode), so span ~ 0.0578·N — monotonically increasing with no special point
at 96. **span(96) = 6.4025 is the N=96 point of this function.** Removing any candidate
(closure, Z2, octave rung, resonance, information) leaves span(96) unchanged. The family
count = floor(log₂ 6.4025)+1 = 3 is a DERIVED consequence of span (D_016 identity).

## Key measured values

| Quantity | Value |
|---|---|
| span(96) | 6.4025 (the N=96 point of ~0.0578·N) |
| ω_max → √12 | 3.464 (antipodal mode, even N; finite-N 3.980) |
| ω_min ~ (2π√91)/N | 0.6216 at N=96 |
| span slope | ~0.0578 (√12/(2π√91)) |
| span(60) / span(90) / span(102) / span(120) | 4.023 / 6.014 / 6.806 / 7.999 (smooth) |
| span(95) / span(97) | 6.333 / 6.473 (no kink at 96) |
| families(96) | 3 = floor(log₂ 6.4025)+1 (DERIVED) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_028_SpanOrigin` | span ~ 0.0578·N; ω_max→√12, ω_min~(2π√91)/N | ✅ |
| `Y_D_028_AlternativeN` | span smooth/monotone across N; no special point at 96 | ✅ |
| `Y_D_028_SelectorRemoval` | removing closure/Z2/octave-rung/resonance/info leaves span(96) | ✅ |
| `Y_D_028_FamilyGeneration` | floor(log₂ 6.4025)+1 = 3 (DERIVED consequence) | ✅ |
| `Y_D_028_DependencyTrace` | Difference → Actualization → Closure → Spectrum → span → 3 families | ✅ |
| `Y_D_028_Run` | Research report | ✅ |

## Conclusion

**span is DERIVED (a consequence of N=96); the family count is DERIVED (a consequence
of span); only N=96 is BOUNDARY.** The span value 6.4025 is fully determined by N=96
through the ring spectrum (ω_max/ω_min ~ 0.0578·N); it is not a selector. The span ∈
[4,8) window is EMERGENT (the 3-family choice, D_016). No canonical value was changed.
