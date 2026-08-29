# Y_D_027_Result.md — ResearchY-D_027 Selector-Origin Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_027_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_027"`

---

## Summary

**Question:** Are positivity, normalization, and stability derived from Difference →
Actualization, or are they the final Boundary input?

**Verdict:** **A) all derived from the primitive structure.** Positivity is intrinsic to
the share construction (ρ_k = μ^k/S ≥ 0 — counts are non-negative); normalization is the
Born rule = normalized share, derived from count conservation (Ch9/QG216), which is the
definitional identity of Difference (Ch3/QG268); stability is the closure fixed point
(Ch4/QG282). The D_026 su(2) selector is a consequence of the minimal hierarchy — the
only boundary is the primitive set {Difference, η}.

## Origin Trace

| Criterion | Origin | Status |
|---|---|---|
| positivity | share construction ρ_k = μ^k/S ≥ 0 (counts non-negative) | **DERIVED** |
| normalization | Born rule = normalized share, from count conservation (Ch9/QG216) | **DERIVED** |
| stability | closure fixed point (Ch4/QG282) | **DERIVED** |
| su(2) selector | positivity + normalization + stability (D_026) | **DERIVED** |
| primitive set {Difference, η} | the minimal foundation | **BOUNDARY** |

## Key measured values

| Quantity | Value |
|---|---|
| ρ_k for μ = 0.5, 1, 2 | all ≥ 0, all ≤ 1 |
| Σρ_k | **1.0000000000 exactly** (for all μ) |
| Σμ^k (unnormalized) | ≠ 1 (normalization is needed) |
| stability | the closure fixed point (boundary = fixed point) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_027_PositivityOrigin` | ρ_k ≥ 0 intrinsic (share of a count) | ✅ |
| `Y_D_027_NormalizationOrigin` | Σρ_k = 1 by construction (Born rule, count conservation) | ✅ |
| `Y_D_027_StabilityOrigin` | stability = closure fixed point | ✅ |
| `Y_D_027_RemovalTest` | removing count conservation/positivity/stability/primitives | ✅ |
| `Y_D_027_DependencyTrace` | Difference → count conservation → normalization → su(2) | ✅ |
| `Y_D_027_Run` | Research report | ✅ |

## Conclusion

**The selector is A) all derived from the primitive structure; the only boundary is the
primitive set {Difference, η}.** Positivity, normalization, and stability follow from
count conservation (the definitional identity of Difference), the share construction, and
the closure fixed point. The D_026 su(2) selector is a consequence of the minimal
hierarchy, not an independent boundary input. No canonical value was changed.
