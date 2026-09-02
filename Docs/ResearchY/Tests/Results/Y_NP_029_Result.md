# Y_NP_029_Result.md — ResearchY-NP_029 ħ Necessity Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_029_Tests.cs`
**Run:** 2026-09-02
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_029"`

---

## Summary

**Question:** Does AT require a fundamental ħ at all, or is ħ merely the dimensional
bridge between derived frequencies and measured energies?

**Verdict: AT does NOT require a fundamental ħ.** Every derived observable is an anchor
(m_e or v, in MeV/GeV) times a dimensionless D96 ratio — never invoking ħ. ħ as a
fundamental constant is REFUTED; ħ as the frequency↔energy dimensional bridge is
BOUNDARY (an SI unit-convention import, D_012 — like c).

## Remove ħ — nothing breaks

| Derived observable | Formula | ħ used? |
|---|---|---|
| dimensionless spectrum | ω_k = √λ_k, span 6.40, ω₁ = 0.6216 | NO |
| u-quark mass | m_u = m_e·Σ√m/√Σm² = 2.164 MeV (QG173) | NO |
| Planck scale | M_Pl = v·A³ = 1.2234e19 GeV (QG181) | NO |
| lepton hierarchy | m_μ = m_e·(D96 law) (QG209) | NO |
| ΩΛ = I_occ/ln K | 0.6839 (QG234) | NO |

The canonical ResearchY derivation chain (D_ResonanceStructure + NP_NewPhysics)
contains no ħ constant. ħ appears only in legacy ResearchQG/ResearchDATA/ResearchXH
analyzers comparing AT results to SI units (G in SI, H0 in Hz) — the unit-convention
role.

## Energy = frequency

- m_u/m_e = Σ√m/√Σm² = 64.0825/√229 = 4.2347 — a pure D96 ratio.
- In natural units E[GeV] = ω; AT's anchors are already GeV/MeV.

## v and m_e vs ħ

| | v | m_e | ħ |
|---|---|---|---|
| physics anchor | yes (energy) | yes (masses) | no |
| needed for a derived observable | yes | yes | **no** |
| in the derived chain | no (import) | no (import) | **never** |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_029_RemoveHbar` | masses/Planck scale derive with no ħ | ✅ |
| `Y_NP_029_NoHbarInDerivedChain` | D/NP chain has no ħ constant | ✅ |
| `Y_NP_029_DimensionlessSurvives` | dimensionless structure is ħ-free | ✅ |
| `Y_NP_029_EnergyIsFrequency` | mass ratio = pure D96 ratio (4.2347) | ✅ |
| `Y_NP_029_VsAnchorLogic` | v/m_e anchors vs ħ unit convention | ✅ |
| `Y_NP_029_WhatBreaks` | only the SI J↔GeV conversion needs ħ | ✅ |
| `Y_NP_029_Classification` | REFUTED / BOUNDARY / DERIVED | ✅ |
| `Y_NP_029_Run` | research report | ✅ |

## Conclusion

AT does not require a fundamental ħ. The two physics anchors v and m_e, multiplied by
dimensionless D96 ratios, produce every derived mass and energy without ħ. ħ is the
dimensional bridge between derived frequencies and SI energies — a unit-convention
import (like c), classified BOUNDARY. No new primitive; canonical AT unchanged.
