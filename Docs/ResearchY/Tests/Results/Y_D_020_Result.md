# Y_D_020_Result.md — ResearchY-D_020 Selection Precondition Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_020_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_020"`

---

## Summary

**Question:** What selected N=96 before Closure? Find the deepest precondition of D96.

**Verdict:** The deepest precondition is the **observable-sector construction** —
complete Z2 doublet pairing (weak-isospin, 0 unpaired modes) plus exactly 3 octave
families. These two INPUTs derive: the period-3 seed (unique complete-Z2 period), 6|N
(seed half-shift), the octave-rung chain n = 3·2^k, and finally N=96 (the unique rung in
[60,120)). The degree-12 ring is cosmetic (radius uniform across rungs). N=96 is
**DERIVED**; the observable-sector construction is the **BOUNDARY**.

## Key measured values

| Quantity | Value |
|---|---|
| Unpaired modes at N=96 | **0** (complete Z2 pairing) |
| Unpaired modes at N=64, 80 | **1, 1** (incomplete — the Z2 discriminator) |
| Families at N=48, 96, 192 | 2, 3, 4 (3-family window) |
| Natural sizes p=2,4,5,3 | 64, 64, 80, **96** |
| Rings with 6\|N + 3 families (D_016) | 11 |
| Octave rungs among the 11 | **only 96** (96 = 3·2⁵) |
| Octave-rung chain 3·2^k | {48, 96, 192} — only 96 ∈ [60,120) |
| Degree-12 ring | uniform at all rung sizes (radius 6.0) — cosmetic |
| Span(96) / ω₁ | 6.4025 / 0.6216 (canonical) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_020_SelectionRemoval` | removing any INPUT breaks N=96 uniqueness; degree-12 is cosmetic | ✅ |
| `Y_D_020_NecessaryConditions` | complete Z2 (0 unpaired) + 3 families necessary; p=3, 6\|N derived | ✅ |
| `Y_D_020_N96Uniqueness` | only 96 is an octave rung among the 11 rings; unique in [60,120) | ✅ |
| `Y_D_020_DependencyTrace` | INPUT → p=3 → 6\|N → octave rung → N=96 → Closure → Spectrum | ✅ |
| `Y_D_020_Counterexamples` | 64/80 (1 unpaired), 48/192 (wrong families), 10 non-rung rings | ✅ |
| `Y_D_020_Run` | Research report | ✅ |

## Conclusion

**The deepest precondition of D96 is the observable-sector construction**: complete Z2
doublet pairing (weak-isospin, 0 unpaired modes) + exactly 3 octave families. These two
INPUTs derive the period-3 seed (unique complete-Z2 period), 6|N (seed half-shift), the
octave-rung chain n = 3·2^k, and finally N=96 (the unique rung in [60,120)). The
octave-rung construction is the exact discriminator that selects 96 among the 11 rings
of D_016. The degree-12 ring is cosmetic (radius-uniform); the closure (D_019) realizes
the pre-selected size. N=96 is **DERIVED**; the observable-sector construction is the
**BOUNDARY** input.

No canonical value was changed.
