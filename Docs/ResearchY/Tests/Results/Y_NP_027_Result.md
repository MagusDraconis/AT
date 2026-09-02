# Y_NP_027_Result.md — ResearchY-NP_027 Planck Spectrum Emergence Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_027_Tests.cs`
**Run:** 2026-09-01
**Result:** ✅ 8/8 PASSED
**Full suite:** 714/714 PASSED (ResearchY)
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_027"`

---

## Summary

**Question:** Can the full Planck spectrum be reproduced as an emergent read of the
D96 spectrum without introducing quantum postulates?

**Verdict:** The Planck FACTOR FORM n(x) = 1/(e^x − 1) **IS emergent** from the D96
geometric occupation statistics (⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1), QG194) — but the
**FULL Planck LAW** (Stefan-Boltzmann T⁴, Wien displacement at x = 2.821, continuous
density of states, Rayleigh-Jeans divergence) **does NOT emerge** from the finite
discrete 95-mode spectrum without importing temperature (a non-canonical primitive).

## The three mechanisms (A / B / C)

| Option | Verdict |
|---|---|
| **A) finite UV solely from spectral cutoff** | **YES in AT** — the 95-mode finite spectrum (ω ∈ [0.62, 3.98]) is the UV regulator; no infinite mode-count to diverge. NOT the canonical ℏ mechanism. |
| **B) Planck factor from occupation statistics** | **YES in FORM** — geometric count (QG194) → n_k = 1/(e^x − 1), with x = k·ln(1/μ). |
| **C) maximum-entropy derivation** | **CONSISTENT but incomplete** — geometric/Bose IS the max-entropy distribution, but AT has not derived the temperature/energy constraint. |

## The four classical limits (continuous Planck form)

| Limit | Continuous Planck | Verified? |
|---|---|---|
| Rayleigh-Jeans (x→0) | n → 1/x | ✅ |
| Wien (x→∞) | n → e^(−x) | ✅ |
| Stefan-Boltzmann | ∫ x³/(e^x−1) dx = π⁴/15 = 6.4939 | ✅ |
| Wien displacement | peak of x³/(e^x−1) at x = 2.821 | ✅ |

## The finite-N problem

The discrete 95-mode sum ≠ the continuous π⁴/15 integral (no T⁴); the discrete peak
is mode-dependent (not at x = 2.821); no ω→0 mode (min ω = 0.62) so no
Rayleigh-Jeans divergence. **The full Planck LAW does not emerge from the finite
discrete D96 spectrum.**

## UV regularization origin

In AT the UV is **regularized by the FINITE SPECTRUM** — there is no infinite
mode-count to diverge, so no Planck-factor cutoff is needed. This is a different
mechanism from canonical QM's quantization (ℏ) cutoff. Finite observability (QG_010)
and finite information (QG_228) are related structural bounds, but the direct UV
regulator is the finite spectrum.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_027_OccupationModel` | ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1) from the geometric count | ✅ |
| `Y_NP_027_PlanckFactor` | the Planck form with x = k·ln(1/μ) | ✅ |
| `Y_NP_027_RayleighJeans` | n → 1/x as x → 0 (continuous) | ✅ |
| `Y_NP_027_WienLimit` | n → e^(−x) as x → ∞ (continuous) | ✅ |
| `Y_NP_027_StefanBoltzmann` | ∫x³/(e^x−1)dx = π⁴/15 (continuous) | ✅ |
| `Y_NP_027_UVOrigin` | AT: finite spectrum is the UV regulator | ✅ |
| `Y_NP_027_NoGo` | the full Planck law does not emerge (finite discrete) | ✅ |
| `Y_NP_027_Run` | research report | ✅ |

## Conclusion

The Planck factor FORM n = 1/(e^x − 1) IS emergent from the D96 geometric occupation
statistics (DERIVED, QG194), and the UV is regulated by the finite spectrum — but
the FULL Planck law (Stefan-Boltzmann T⁴, Wien displacement at x = 2.821, continuous
density of states) does NOT emerge from the finite discrete spectrum without
importing temperature, which is not a canonical primitive. No new primitive;
canonical AT unchanged.
