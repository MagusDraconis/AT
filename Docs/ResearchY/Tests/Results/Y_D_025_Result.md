# Y_D_025_Result.md — ResearchY-D_025 Three-Generator Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_025_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_025"`

---

## Summary

**Question:** Why three generators? What is the minimal structure that upgrades a
spectral doublet from SO(2) to SU(2)?

**Verdict:** **SO(2) → SU(2) is NOT possible without new input.** The real spectral
algebra {I, J, P, JP} is the full real 2×2 algebra: it contains **J = iσy** (real skew),
**σz = P** (parity), and **σx = JP** (Hermitian). SU(2) needs the **skew-Hermitian**
iσx, iσz, which require the imaginary unit i (complexification). The Fourier phase
provides i (**EMERGENT**), but complexification alone gives sl(2,C), whose three real
forms include sl(2,R) — which the real spectral structure contains directly and leans
toward, **NOT su(2)**. The compact-form choice (su(2) signature) is **BOUNDARY**.

## Generator Map

| Generator | In the real spectral algebra? | Needed by SU(2)? |
|---|---|---|
| J = iσy | **YES** (real skew) | yes |
| σz = P (parity) | YES (Hermitian) | no — needs iσz |
| σx = JP | YES (Hermitian) | no — needs iσx |
| iσx, iσz | **NO** (complex) | yes |

## Key measured values

| Quantity | Value |
|---|---|
| Real spectral algebra {I, J, P, JP} | full real 2×2 (dim 4) |
| Real skew-symmetric 2×2 | 1D (only J = iσy) |
| Missing ingredient | the imaginary unit i (complexification) |
| Complexification gives | sl(2,C) (6 real dims), not SU(2) |
| Real forms of sl(2,C) | su(2), sl(2,R), su(1,1) |
| Spectral structure leans | **sl(2,R)** (real generators in the algebra), not su(2) |
| SO(2) → SU(2) without new input | **NO** |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_025_GeneratorMap` | J=iσy, σz=P, σx=JP all in the real spectral algebra | ✅ |
| `Y_D_025_SkewHermitian` | SU(2) needs iσx, iσz (complex); only iσy is real | ✅ |
| `Y_D_025_Complexification` | the Fourier i is the missing ingredient (EMERGENT) | ✅ |
| `Y_D_025_RealForms` | sl(2,C) has 3 real forms; spectrum leans sl(2,R), not su(2) | ✅ |
| `Y_D_025_RemovalTest` | removing any ingredient breaks SU(2) | ✅ |
| `Y_D_025_Verdict` | SO(2)→SU(2) not possible without complexification + compact-form | ✅ |
| `Y_D_025_Run` | Research report | ✅ |

## Conclusion

**The upgrade from SO(2) to SU(2) is not possible without new input.** The real spectral
algebra {I, J, P, JP} contains σx = JP and σz = P (Hermitian) and iσy = J (real skew),
but SU(2) needs the skew-Hermitian iσx, iσz, which require the imaginary unit i
(complexification). The Fourier phase provides i (**EMERGENT**), but complexification
alone gives sl(2,C), whose three real forms include sl(2,R) — which the real spectral
structure contains directly and leans toward, **NOT su(2)**. The compact-form choice
(su(2) signature) is **BOUNDARY**. Hence SO(2) → SU(2) requires complexification
(EMERGENT) + compact-form choice (BOUNDARY). No canonical value was changed.
