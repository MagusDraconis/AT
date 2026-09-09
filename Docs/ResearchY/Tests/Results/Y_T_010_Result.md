# Y_T_010_Result.md — ResearchY-T_010 D96 Survivor Compression Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_010_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_010"`

---

## Summary

**Goal:** Explain why D96 saturates near 5 survivors while random near 17, and whether S∞ is
predictable from spectral structure alone.

**Answer:** D96's circulant symmetry collapses 95 modes into 44 distinct eigenvalues → a
sparse, peaked fitness field near λ₂ (2 modes within 2λ₂) → ~5 survivors. Random's dense
near-degenerate spectrum (71 modes within 2λ₂) → flat field → ~17 survivors. S∞ is a function
of the full spectral structure (exact at β=0 Perron / μ=0 threshold); no single scalar works.

## Structural measures (μ=0.01, β=1)

| case | A | S∞ | conc | HHI | entropy | near-gap | degen | max mult |
|---|---|---|---|---|---|---|---|---|
| D96 | 44 | 5 | 0.362 | 0.149 | 2.903 | 2 | 0.542 | 6 |
| D96-3D | 12 | 9 | 0.201 | 0.127 | 2.199 | 6 | 0.875 | 20 |
| random | 95 | 17 | 0.017 | 0.011 | 4.529 | 71 | 0.010 | 1 |
| physical | 48 | 5 | 0.225 | 0.082 | 3.162 | 4 | 0.500 | 2 |
| unphysical | 3 | 3 | 0.781 | 0.638 | 0.657 | 32 | 0.969 | 32 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_010_D96Compresses` | D96 more concentrated, lower entropy, fewer survivors | ✅ |
| `Y_T_010_SpectralCause` | D96 more degenerate, sparser near-gap than random | ✅ |
| `Y_T_010_PredictableFromSpectrum` | β=0 Perron = S∞ exactly (spectral structure alone) | ✅ |
| `Y_T_010_SingleScalarRefuted` | D96 & physical share S∞=5 but differ in every scalar | ✅ |
| `Y_T_010_Classification` | DERIVED / EMERGENT / REFUTED verdicts | ✅ |
| `Y_T_010_Run` | assumptions-first scientific report | ✅ |

## Conclusion

The D96→5 vs random→17 split is DERIVED from the spectral tail (sparse/peaked vs dense/flat),
the precise integers are EMERGENT, and any single-scalar predictor is REFUTED. S∞ is a
function of the full fitness spectrum w = m/λ via the exact reach laws.
