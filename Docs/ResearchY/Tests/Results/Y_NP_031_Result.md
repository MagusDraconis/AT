# Y_NP_031_Result.md — ResearchY-NP_031 Structure vs Thermodynamics Audit

**Test suite:** `AT.Tests/ResearchY/NP_NewPhysics/Y_NP_031_Tests.cs`
**Run:** 2026-09-02
**Result:** ✅ 8/8 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_NP_031"`

---

## Summary

**Question:** Do the NP_027–NP_030 results indicate that D96 belongs EXCLUSIVELY to the
structural layer, while thermodynamics belongs to a separate occupancy layer? Does AT
naturally split into a Structure Sector (Difference → Actualization → Spectrum) and a
Thermodynamic Sector (Occupations → Temperature → Radiation)?

**Verdict: YES to a two-layer architecture, but NOT as two autonomous sectors.** The
Structure Sector is DERIVED and self-contained (spectrum, occupancy [4,4,87], moments,
I_occ = 0.7513, ΩΛ = 0.6839, masses, M_Pl, couplings, entropy H = log₂95 = 6.57 bits —
none needs a temperature; the derivation chain contains no SI thermal constant).
Thermodynamics exists only as an ADDED state-occupation law over the structural modes.

## Structure sector (DERIVED, self-contained)

| Structural object | Value | Source |
|---|---|---|
| spectrum ω_k = √λ_k | 95 modes, band [0.622, 3.98], span 6.40, ω₁ = 0.6216 | D_008/D_030 |
| octave occupancy | [4, 4, 87] | A_003/D_030 |
| information density | I_occ = KL(ρ‖uniform) = 0.7513 nats | QG_228 |
| cosmological fractions | ΩΛ = 0.6839, Ωm = 0.3161 | QG_234 |
| state-count entropy | H = log₂(95) = 6.57 bits | M_004 |
| u-quark mass | m_u = m_e·Σ√m/√Σm² = 2.164 MeV | QG_173 |

The structural derivation chain contains NO SI thermal constant (k_B = 1.380649e-23
etc. — count 0 across D_ResonanceStructure + NP_NewPhysics derivation tests).

## Thermodynamic objects (not reproduced from structure)

| Object | Status |
|---|---|
| temperature scale T | BOUNDARY (NP_030 — no canonical candidate) |
| Planck-factor FORM n = 1/(e^x−1) | EMERGENT from geometric occupation (NP_027) — free decay μ<1 |
| blackbody / radiation | FALSIFIED as emergent (NP_028) — hosted |
| Stefan-Boltzmann T⁴ = π⁴/15 | NOT REPRODUCED (discrete sum ≠ π⁴/15) (NP_027) |
| Wien tail | FALSIFIED (hard cutoff at ω = 3.98) (NP_027/028) |
| ħ | BOUNDARY unit bridge (NP_029) |

## Overlap = occupation statistics ρ_k = μ^k/S

- **Structural reading:** canonical branching μ = 2, gens = 8, S = 255 — the count
  behind the occupancy [4,4,87] and I_occ.
- **Thermal reading:** the same geometric form with a free decay μ<1 gives the Planck
  occupation FORM ⟨n_k⟩ = 1/(e^(k·ln(1/μ)) − 1).

No other structural object has a thermal twin.

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_NP_031_StructureInventory` | closed structural layer (spectrum, I_occ, ΩΛ, masses) | ✅ |
| `Y_NP_031_ThermoInventory` | thermal objects each BOUNDARY/FALSIFIED/NOT REPRODUCED | ✅ |
| `Y_NP_031_OverlapIsOccupationStatistics` | sole overlap = ρ_k = μ^k/S | ✅ |
| `Y_NP_031_NoThermalObservableFromStructure` | only entropy + FORM derive; canonical μ=2 negative | ✅ |
| `Y_NP_031_ThermoAddedAsStateOccupationLaw` | mode set + occupation + scale T (BOUNDARY) | ✅ |
| `Y_NP_031_StructureSectorClosed` | derivation chain has no SI thermal constant | ✅ |
| `Y_NP_031_Classification` | DERIVED / REFUTED / BOUNDARY / FALSIFIED flags | ✅ |
| `Y_NP_031_Run` | research report | ✅ |

## Conclusion

AT splits into ONE closed DERIVED structural layer and an ADDED occupancy layer.
Structure (Difference → Actualization → Spectrum → every structural observable) needs
no temperature; thermal content enters only as a state-occupation law over the derived
modes, with the temperature scale as a BOUNDARY parameter. No thermal observable
derives from structure alone (REFUTED). The two-sector split is a DERIVED architectural
fact, not a division into two primitive pillars. No new primitive; canonical AT
unchanged.
