# Y_NP_017_Result.md — ResearchY-NP_017 Natural D96 Signature Search

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_017_Tests.cs`
**Run:** 2026-08-30
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_017"`

---

## Summary

**Question:** Can D96-type spectral structure appear naturally in real systems?

**Verdict:** Nature contains an **APPROXIMATE D96 signature** in the CMB acoustic peak
ratios (the D96 octave hierarchy, <0.06%), but **NO natural realization of the exact
O(2) mirror-pair degeneracy**.

## CMB acoustic peaks (D96-derived, QG237/238)

| Quantity | Value | Deviation |
|---|---|---|
| ℓ₁ | 220.48 | 0.008% |
| r₂₁ | 2.4368 | 0.035% |
| r₃₁ | 3.6965 | 0.058% |
| n_s | 0.96497 | 0.007% |

## Mirror-pair test

| Domain | Exact pairs? |
|---|---|
| atomic (Rydberg 1/n²) | NO |
| molecular | NO |
| condensed matter (phonons) | approximate only |
| plasma / GW (damped) | NO |
| **CMB** | ratios only — not per-mode pairs |

## Candidate ranking

| Rank | Candidate | Strength |
|---|---|---|
| 1 | **CMB acoustic peaks** | STRONG (<0.06%) |
| 2 | cosmological (general) | MEDIUM |
| 3 | condensed-matter phonons | WEAK |
| 4–5 | atomic/molecular, plasma/GW | none |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_017_MirrorPairs` | no natural exact mirror pairs | ✅ |
| `Y_NP_017_OctaveHierarchy` | CMB peaks follow the D96 octaves | ✅ |
| `Y_NP_017_SpectralMatch` | the CMB is the strongest D96 match | ✅ |
| `Y_NP_017_DeviationAudit` | CMB deviations < 0.06% | ✅ |
| `Y_NP_017_CandidateRanking` | CMB ranks first | ✅ |
| `Y_NP_017_Run` | research report | ✅ |

## Conclusion

Nature contains the D96 OCTAVE HIERARCHY (in the CMB acoustic peaks, <0.06%) but not
yet the exact mirror-pair degeneracy. The CMB is the strongest candidate; no domain is
falsified. Classification: CMB peak ratios CORRESPONDENCE (D96-derived); exact mirror
pairs PREDICTION (unobserved). No new primitive; canonical AT unchanged.
