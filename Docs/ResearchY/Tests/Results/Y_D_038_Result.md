# Y_D_038_Result.md — ResearchY-D_038 State-Identity Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_038_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_038"`

---

## Summary

**Question:** Why should an observable state carry both magnitude and phase?

**Verdict:** Observability requires **state identity** — each mode must be
distinguishable from every other. **Magnitude-only collapses the state space**: the
[4,4,87] occupancy groups give only **3 distinct magnitudes** for 95 modes (√(1/7),
√(2/7), √(4/7)), and the mirror pair k/N−k is identical (cos even). **Phase-only**
restores 95/95 identity but **loses probability content** (uniform |ψ|=1 — no branching
structure, the count shares ρ=1/7,2/7,4/7 are gone). The **complex state**
ψ = |ψ|·e^{iθ} is the minimal complete observable state: 95/95 injective with the
Born rule Σρ=1 EXACT over the generation shares. Two real DOFs = a complex number.

## Key measured values

| Quantity | Value |
|---|---|
| magnitude-only distinct states | 3 (for 95 modes) |
| magnitude values | √(1/7)=0.37796, √(2/7)=0.53452, √(4/7)=0.75593 |
| mirror collapse (magnitude-only) | cos(2π(N−k)n/N)=cos(2πkn/N) — identical |
| phase-only distinct phases | 95/95 |
| phase-only probability content | none (uniform |ψ|=1) |
| complex-state identity | 95/95 injective |
| Born rule over shares | Σρ = 1 EXACT (μ=2, J=3) |
| complete pairing | N=96 = 3·2⁵; min mult ≥ 2 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_038_MagnitudeOnly` | magnitude-only collapses state identity (3 states); mirror collapse | ✅ |
| `Y_D_038_PhaseOnly` | phase-only loses probability (uniform); identity survives | ✅ |
| `Y_D_038_StateIdentity` | complex map 95/95 injective; Born rule over shares exact | ✅ |
| `Y_D_038_Observability` | requires both DOFs; single DOF fails | ✅ |
| `Y_D_038_InformationContent` | minimal info structure = 2 real DOFs | ✅ |
| `Y_D_038_DependencyTrace` | Difference → magnitude; Actualization → phase; both → observability | ✅ |
| `Y_D_038_Run` | Research report | ✅ |

## Conclusion

**Observability forces the two-DOF complex structure**: state identity requires the
phase (magnitude-only gives only 3 distinct states for 95 modes; mirror pairs
collapse), while probability content requires the magnitude (phase-only is uniform,
no count structure). The complex state ψ = |ψ|·e^{iθ} — magnitude (count, QG216) and
phase (circulation, QG220) — is the **minimal complete observable state**:
95/95 injective with Born rule exact. Classification: magnitude DERIVED; phase DERIVED;
complex state DERIVED (QG218); state identity EMERGENT (information completeness);
interference/reciprocity DERIVED (D_037); Z2-paired sector requirement BOUNDARY (D_020);
N=96 DERIVED. No canonical value was changed.
