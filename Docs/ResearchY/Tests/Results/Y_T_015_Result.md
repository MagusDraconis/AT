# Y_T_015_Result.md — ResearchY-T_015 Spectral Robustness Audit

**Test suite:** `AT.Tests/ResearchY/T_SpectralBlueprint/Y_T_015_Tests.cs`
**Run:** 2026-09-09
**Result:** ✅ 4/4 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_T_015"`

---

## Summary

**Goal:** Determine whether spectral robustness is controlled by near-gap density ρ(k) or gap
size λ₂.

**Answer:** Neither — robustness is controlled by the **degeneracy structure** (number of
degenerate eigenvalue levels). H1 (λ₂ insufficient) SUPPORTED; H2 (ρ predicts robustness)
REFUTED; H3 (D96 beats random) REFUTED.

## Robustness table

| model | λ₂ | m(λ₂) | ρ(2) | Δλ₂/λ₂ | ΔA | spectral | degen |
|---|---|---|---|---|---|---|---|
| D96 | 0.386 | 2 | 0.021 | 0.0077 | 30 | 0.0073 | 44 |
| physical | 1.0 | 2 | 0.021 | 0.0343 | 24 | 0.0376 | 47 |
| D96-3D | 1.0 | 2 | 0.042 | 0.0446 | 8 | 0.0255 | 11 |
| unphysical | 5.0 | 32 | 0.333 | 0.2333 | 2 | 0.0505 | 3 |
| random | 17.19 | 1 | 0.740 | 0.0003 | 0 | 0.0028 | 0 |
| complete | 96.0 | 95 | 0.990 | 0.0403 | 1 | 0.0041 | 1 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_T_015_H1_GapSizeInsufficient` | λ₂ does not rank robustness (SUPPORTED) | ✅ |
| `Y_T_015_H2_LowRhoRefuted` | ρ anti-correlated with ΔA; degen count controls (REFUTED) | ✅ |
| `Y_T_015_H3_D96NotBetterThanRandom` | random more robust than D96 (REFUTED) | ✅ |
| `Y_T_015_Run` | assumptions-first scientific report | ✅ |

## Conclusion

ΔA (attractor shift) = number of degenerate eigenvalue levels that split — a DERIVED function of
the multiplicity structure. ρ(k) and λ₂ do not predict robustness (H2, H3 REFUTED; H1 only
"insufficient"). D96's rigid circulant symmetry is itself the fragility.
