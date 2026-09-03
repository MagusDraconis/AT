# Y_NP_034_Result.md — ResearchY-NP_034 Bose Without Blackbody Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_034_Tests.cs`
**Run:** 2026-09-03
**Result:** ✅ 10/10 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_034"`

---

## Summary

**Question:** Why does a D96 ensemble produce Bose occupation statistics
n(ω) = 1/(e^(βω) − 1) yet fail to reproduce the observed blackbody spectrum? Identify
the minimal obstruction.

**Verdict: Bose statistics is SUFFICIENT — the obstruction is entirely the D96
mode-set g(ω).** The D96 ensemble already produces the exact Planck occupation
(NP_033); replacing it with exact Planck occupations is a no-op and blackbody still
fails, while replacing only the mode set with the ideal ω² DOS restores the blackbody
completely with the same occupation. Answer A CONFIRMED.

## Test 1 — Factorization u(ω) = g(ω)·n(ω)·ω

The observed radiance separates into mode-set g (DOS), occupation n, and mode energy
ω. D96 supplies 95 modes / 44 distinct freqs ([4,4,87] occupancy) as g and the exact
Bose occupation as n (NP_033 identity, re-verified). U(β=1) = **12.588** factorizes
exactly between the raw 95-mode sum and the distinct-frequency × multiplicity sum.

## Test 2 — Replace occupations with exact Planck occupations

NO-OP: the D96 ensemble occupation already IS the exact Planck/Bose occupation
(ln(n/(1+n)) = −βω exact on every D96 mode). Blackbody still fails:
Σω³/(e^ω−1) = **120.70** ≠ π⁴/15 = 6.494. ⇒ **occupation is NOT the obstruction.**

## Test 3 — Replace D96 mode set with ideal ω² DOS (keep Bose n)

u(ω) = ω²·ω·1/(e^(βω)−1) = ω³/(e^(βω)−1) — the exact Planck law:

| Limit | Value | Result |
|---|---|---|
| Stefan-Boltzmann | ∫x³/(e^x−1)dx = π⁴/15 = 6.4939 | ✅ |
| Wien displacement | peak at x = 2.821 | ✅ |
| Rayleigh-Jeans | u → x² as x → 0 | ✅ |
| Wien tail | u(x) → x³e^(−x) | ✅ |

⇒ **the mode set is the obstruction; Bose is sufficient (answer A).**

## Test 4 — Sensitivity

| Mode-set property | D96 | Blackbody | Contribution |
|---|---|---|---|
| UV cutoff | cap ω_max = 3.98 | support to ∞ | **40.7%** of blackbody energy above cap at β = 1; no modes above ⇒ no Wien tail |
| DOS exponent | p ≈ 1.0 low, 1.51 mid | p = 3 | p = 1.51 host integrates to **1.79** < π⁴/15/3 (>3× suppression) |
| clustering | 44 distinct; 8-bin [2,2,2,0,2,2,33,52] | smooth ω² | lumpy, empty interior bin, dense top |
| finite count | 95 modes | — | **NOT an obstruction**: ideal ω² 95-mode set → in-band error ~0.05% |

## Test 5 — Minimal deformation

Keep the Bose occupation fixed; change ONLY the mode set: DOS exponent p: 1.0–1.51 →
3, redistribute the 95 modes ω³-uniformly (in-band error → <1%), and unbind the band
(adds the missing 40.7%). Restores ω² DOS, Wien tail, π⁴/15.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_034_Factorization` | u = g·n·ω; U(1) = 12.588; Boltzmann identity | ✅ |
| `Y_NP_034_OccupationReplacementIsNoop` | exact-Planck replacement = identity; blackbody still fails | ✅ |
| `Y_NP_034_IdealW2DOSRestoresBlackbody` | Bose + ω² DOS → π⁴/15, 2.821, RJ, Wien | ✅ |
| `Y_NP_034_UvCutoffSensitivity` | 40.7% above ω_max; no modes above cap | ✅ |
| `Y_NP_034_DosExponentSensitivity` | p ≈ 1.0–1.51 vs 3; >3× total suppression | ✅ |
| `Y_NP_034_ClusteringSensitivity` | 44 distinct; 8-bin [2,2,2,0,2,2,33,52] | ✅ |
| `Y_NP_034_FiniteCountSensitivity` | ideal ω² 95-mode set → in-band error ~0.05% | ✅ |
| `Y_NP_034_MinimalDeformation` | mode set only: p → 3, redistribute, unbind | ✅ |
| `Y_NP_034_Classification` | A CONFIRMED / B none / C no new primitive | ✅ |
| `Y_NP_034_Run` | research report | ✅ |

## Classification

| Component | Status |
|---|---|
| Bose occupation from the D96 ensemble | **EMERGENT** (unchanged, NP_033) |
| occupation as blackbody factor | **SUFFICIENT** (answer A) |
| D96 mode-set as blackbody host | **FALSIFIED** (unchanged, NP_028/033) |
| UV cap + DOS exponent + clustering of D96 mode set | **DERIVED — the minimal obstruction** |
| finite mode count | **NOT an obstruction** |
| additional occupation-level obstruction | **NONE** (answer B refuted) |
| new primitive / layer | **NOT REQUIRED** (hosted ω² DOS suffices, answer C refuted) |
| temperature scale β | **BOUNDARY** (unchanged) |

## Conclusion

A D96 ensemble produces Bose statistics (EMERGENT, NP_033) but not the blackbody
because the observed radiance is a product u = g·n·ω, and only the occupation factor
n is correct. The exact-Planck replacement changes nothing (Test 2); replacing the
mode set with the ideal ω² DOS restores the blackbody exactly with the same
occupation (Test 3). The minimal obstruction is the D96 mode-set: sub-power DOS
exponent (~1.0–1.51 vs 3), hard UV cap (40.7% of blackbody energy above ω_max at
β=1, no Wien tail), and top-heavy clustering. The finite 95-mode count is not the
problem. No new primitive; canonical AT unchanged.
