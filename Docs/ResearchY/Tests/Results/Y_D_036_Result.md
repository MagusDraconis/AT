# Y_D_036_Result.md — ResearchY-D_036 Complex-State-Origin Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_036_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_036"`

---

## Summary

**Question:** Why must observable states be complex? Is complex state structure derived
from Difference → Actualization or the final boundary?

**Verdict:** The complex state ψ = |ψ|·e^{iθ} is **DERIVED**. The two real DOFs are the
two faces of the SAME actualization tick k: magnitude |ψ| = √ρ (count face, QG216) and
phase θ = 2πk/N (circulation face, QG220; link connection, QG63). The **phase is the
pairing discriminator** — it distinguishes k from N−k (the Z2 pairing, D_021): in a
magnitude-only (1-DOF real) space cos(2π(N−k)n/N) = cos(2πkn/N), so the mirror pair
collapses and no doublet/weak-isospin sector exists. Interference P = 2 + 2cos(θ₁−θ₂)
is a DERIVED consequence, not the cause. **REFINEMENT: "the observable sector is
complex" (D_035) reduces to the Z2 pairing input (D_020)** — the boundary count does
not increase.

## Key measured values

| Quantity | Value |
|---|---|
| magnitude face | |ψ| = √(μ^k/S) — branching count, QG216 |
| phase face | θ_k = 2πk/N — circulation, QG220 |
| complete amplitude | ψ_k = √(μ^k/S)·e^(2πik/N), Σ|ψ|² = 1 EXACT (μ=0.5/1/2) |
| cos mirror identity | cos(2π(N−k)n/N) = cos(2πkn/N) — pair collapses w/o phase |
| sin mirror identity | sin(2π(N−k)n/N) = −sin(2πkn/N) — the discriminator |
| complex modes k/N−k | conjugates, distinct for k ≠ N/2 (verified k=16/32/40) |
| self-conjugate k=N/2 | e^{iθ_{N/2}} = ±1 (real-only — needs multiplet, D_035) |
| interference | P = 2 + 2cos(θ₁−θ₂), phase-dependent (verified) |
| real-only addition | P = P₁ + P₂ (no interference) |
| Z2 pairing at N=96 | min multiplicity 2 (complete); N=64 → 1 (fails) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_036_MagnitudeOnly` | removing phase → mirror pairs collapse (cos even), classical addition; count survives (Σρ=1) | ✅ |
| `Y_D_036_PhaseOnly` | removing magnitude → uniform empty sector (content lost); interference survives | ✅ |
| `Y_D_036_Interference` | P = 2 + 2cos(θ₁−θ₂) phase-dependent; real-only P = P₁+P₂ | ✅ |
| `Y_D_036_Observability` | Born rule Σρ=1 EXACT; complete amplitude preserves it | ✅ |
| `Y_D_036_ComplexNecessity` | phase distinguishes k from N−k (conjugates); self-conjugate real-only | ✅ |
| `Y_D_036_DependencyTrace` | Difference → count → magnitude → phase → complex → pairing → N=96 | ✅ |
| `Y_D_036_Run` | Research report | ✅ |

## Conclusion

**Observable states must be complex because the complex structure is DERIVED from
Difference → Actualization**: magnitude (count face, QG216) and phase (circulation
face, QG220) are the two faces of the same actualization tick k, and the phase is the
discriminator that distinguishes k from N−k — the Z2 pairing (D_021). Removing the
phase collapses the mirror pairs; removing the magnitude empties the count structure.
**"The observable sector is complex" (D_035) reduces to the Z2 pairing input (D_020)** —
no separate boundary, no new primitive. Classification: magnitude DERIVED; phase
DERIVED; complex state DERIVED; interference DERIVED; complex observability EMERGENT
(= the Z2 pairing); Z2 pairing BOUNDARY (D_020); N=96 DERIVED. No canonical value was
changed.
