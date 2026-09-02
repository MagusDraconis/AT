# Y_NP_028_Result.md — ResearchY-NP_028 Blackbody Reconstruction Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_028_Tests.cs`
**Run:** 2026-09-02
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_028"`

---

## Summary

**Question:** Can the D96 mode structure reproduce the observed Planck spectrum after
coarse-graining (weight 95 positive modes by occupancy, construct a spectral density,
compare against Planck, determine the high-frequency falloff)?

**Verdict: NO.** Coarse-graining does NOT heal the NP_027 gaps. The per-mode occupation
factor n = 1/(e^x − 1) is CORRESPONDENCE (NP_027 DERIVED form), but the blackbody DOS
(ω², 3D cavity) is FALSIFIED for D96, the Wien exponential tail is FALSIFIED (hard
spectral cutoff), and the full observed blackbody after coarse-graining is FALSIFIED.

## The three obstructions (all survive coarse-graining)

| Obstruction | D96 | Blackbody needs |
|---|---|---|
| Density of states | ~ω^1.5 sub-power-law, lumpy (44 distinct freqs) | smooth ω² (3D cavity) |
| Top-heaviness | 87.4% of modes in top 20% of band | smooth peak at x = 2.82 |
| High-frequency falloff | hard cutoff at ω_max = 3.980, zero modes above | exponential Wien tail e^(−ω/θ) to ∞ |

## Key measured values

- 95 positive mode slots, band [0.622, 3.980], span ratio 6.40.
- 44 distinct frequencies (mirror pairs + 5-fold λ=12 + 6-fold λ=14 blocks).
- Cumulative growth: N(<2.5)/N(<1.0) = 8/2 = 4.0 (ω³ blackbody would need 15.6);
  N(<3.0)/N(<1.5) = 10/4 = 2.5 (ω³ needs 8).
- 93.7% of modes above band mid (2.30); 87.4% (83/95) in the top 20% of the band.
- Occupancy-weighted energy above ω = 3.3 at θ = 1: D96 = 0.657 vs Planck in-band = 0.232.
- Mode density rises into the cutoff: 0 modes in [3.0, 3.1), 6 in [3.3, 3.4), 6 in [3.9, 4.0).

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_028_ModeInventory` | 95 modes, band [0.622,3.98], span 6.40, 44 distinct freqs | ✅ |
| `Y_NP_028_DosNotBlackbody` | cumulative growth ~ω^1.5, not ω³ | ✅ |
| `Y_NP_028_TopHeavyDOS` | 93.7% above mid, 87.4% in top 20% | ✅ |
| `Y_NP_028_OccupancyWeightedMismatch` | D96 0.657 vs Planck 0.232 energy above ω=3.3 | ✅ |
| `Y_NP_028_HighFrequencyFalloff` | hard cutoff at 3.98; density rises into it (no Wien tail) | ✅ |
| `Y_NP_028_CoarseGrainNoHeal` | binning preserves the top-heavy histogram | ✅ |
| `Y_NP_028_Classification` | form CORRESPONDENCE; DOS/Wien/full law FALSIFIED | ✅ |
| `Y_NP_028_Run` | research report | ✅ |

## Conclusion

Coarse-graining the D96 mode structure does NOT reproduce the observed Planck spectrum.
The blackbody shape needs a smooth ω² density of states over an unbounded band — D96
provides neither (top-heavy ~ω^1.5 DOS, hard cutoff at ω_max = 3.98). The per-mode
occupation factor corresponds (NP_027), but the product (occupation × DOS) is not the
observed blackbody. No new primitive; canonical AT unchanged.
