# Y_NP_032_Result.md — ResearchY-NP_032 Thermal-N Search Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_032_Tests.cs`
**Run:** 2026-09-02
**Result:** ✅ 9/9 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_032"`

---

## Summary

**Question:** Is D96 specifically a structure attractor, while another D_N acts as a
thermal attractor? Does structure N ≠ thermal N?

**Verdict: FALSIFIED as a size dichotomy.** There is NO thermal-attractor ring size in
the canonical circulant family C_N(±1..±6), N = 8..512. D96 is the structure-sector
base; thermodynamics is not an N-property of the family (NP_031's added occupancy
layer).

## Scan findings (N = 8..512, C_N(±1..±6))

### UV behavior — N-independent cap
ω_max → continuum **3.9851** for ALL N (verified N = 96 → 3.9796, 512 → 3.9849,
1024/4096 → 3.9851). The cap is a property of the coupling set (±1..±6), not of N. No
ring can host a Wien tail above the band.

### DOS scaling — 1D linear, never thermal
λ_k ≈ (2πk/N)²·91 for small k ⇒ ω_k ≈ (2π√91/N)·k — an EXACT linear dispersion
(ratio ω_k/(c·k) = 1.0000 at N = 4096). Cumulative N(ω) ∝ ω (exponent ≈ 1.06–1.09).
Every ring with span ≥ 8 (392 rings) has low-DOS exponent **1.00** (first two octaves
each hold 4 modes → N(4ω₁)/N(2ω₁) = 2.00). A 2D/3D cavity needs 4.0/8.0. NO ring in
8..512 reaches it.

### Occupancy hierarchy — top-heavy at every N
First octave holds 4 modes for 478/505 N values. D96's [4,4,87] is one member of a
61-ring family in the 3-family span window [4,8) (N = 60..120); N = 92..100 all share
the (4,4,X) structure.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_032_ScanRange` | scan N = 8..512 executes; D96 w₁ = 0.6216, w_max = 3.98 | ✅ |
| `Y_NP_032_UvCapNIndependent` | ω_max → 3.9851 for ALL N (no Wien tail room) | ✅ |
| `Y_NP_032_LinearDispersion` | ω_k ∝ k exactly (ratio 1.0000, 1D chain) | ✅ |
| `Y_NP_032_DosExponentNeverThermal` | low-DOS exponent = 1.00 for every span ≥ 8 ring | ✅ |
| `Y_NP_032_OccupancyTopHeavyEveryN` | first octave = 4 for 90%+ of N; D96 top octave 87 | ✅ |
| `Y_NP_032_D96OneOfFamily` | D96 one of ≥ 61 rings in the 3-family window | ✅ |
| `Y_NP_032_ThermalOccupationNeedsDecay` | Bose needs μ<1; canonical μ=2 is N-independent | ✅ |
| `Y_NP_032_Classification` | FALSIFIED / DERIVED / BOUNDARY flags | ✅ |
| `Y_NP_032_Run` | research report | ✅ |

## Conclusion

D96 is specifically a STRUCTURE attractor (canonical N = 96 of the octave/family
window, occupancy [4,4,87]), but there is NO thermal attractor ring size. Every ring
C_N(±1..±6), N = 8..512, is a 1D chain: linear low-frequency dispersion (ω ∝ k, DOS
exponent ≈ 1), the same hard UV cap 3.9851 at every N, and top-heavy occupancy. The
ω² DOS, Wien tail, and thermal occupancy a blackbody needs are absent at every N.
Thermodynamics is not an N-property of the canonical family — it is the ADDED occupancy
layer of NP_031. The hypothesis "structure N ≠ thermal N" is FALSIFIED as a size
dichotomy because no thermal N exists; the true split is structural-layer vs
added-occupancy-layer. No new primitive; canonical AT unchanged.
