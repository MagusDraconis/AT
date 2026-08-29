# Y_D_034_Result.md — ResearchY-D_034 Reciprocity Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_034_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_034"`

---

## Summary

**Question:** Why must every observable oscillator possess a reciprocal partner?

**Verdict:** **Reciprocity = the [magnitude, phase] complex structure (QG218).** Every
observable mode must carry two independent real DOFs — magnitude |ψ| = √ρ (the branching
count, QG216, Difference's count face) and phase θ (the U(1) link connection, QG63,
Actualization's link face). The complex structure (two DOFs) is **DERIVED**: real-only
states give classical addition, complex states give interference. **Reciprocity (every
mode complex) is the EMERGENT observable requirement; complete pairing (0 unpaired) is
BOUNDARY (D_020).** Removing reciprocity breaks interference first, then the doublet
structure and weak-isospin.

## What is lost without reciprocity

| Loss | Detail |
|---|---|
| INTERFERENCE (first) | real-only → classical addition (QG218) |
| phase freedom | no spatial phase partner |
| doublet structure | no 2D representation |
| weak-isospin | no doublet for the SU(2) reading (D_022) |
| normalization | **survives** |

## Key measured values

| Quantity | Value |
|---|---|
| magnitude | |ψ| = √ρ (branching count, ≥ 0) |
| phase | |e^{iθ}| = 1 (link connection) |
| complex interference | P = 2 + 2cos(θ₁−θ₂) (varies with phase) |
| real-only addition | P = P₁ + P₂ (fixed, no interference) |
| singlet sin quadrature | sin(πn) = 0 (real-only) |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_034_ReciprocityOrigin` | magnitude (count) + phase (link) = the [magnitude, phase] pair | ✅ |
| `Y_D_034_SingletFailure` | singlet is real-only (no sin partner) — no interference | ✅ |
| `Y_D_034_PhaseFreedom` | paired mode has full phase; singlet real-only | ✅ |
| `Y_D_034_Observability` | real-only → classical addition (no interference, QG218) | ✅ |
| `Y_D_034_DependencyTrace` | Difference → count/phase → complex → reciprocity → N=96 | ✅ |
| `Y_D_034_Run` | Research report | ✅ |

## Conclusion

**Reciprocity is the [magnitude, phase] complex structure (QG218).** The two DOFs are
DERIVED (magnitude from the count, phase from the link); the complex structure is
DERIVED (real-only gives classical addition, complex gives interference). Reciprocity
(every mode complex) is **EMERGENT**; complete pairing (0 unpaired) is **BOUNDARY**
(D_020). Removing reciprocity breaks interference first, then the doublet structure and
weak-isospin. No canonical value was changed.
