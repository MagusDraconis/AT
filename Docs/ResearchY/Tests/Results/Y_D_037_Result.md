# Y_D_037_Result.md — ResearchY-D_037 Reciprocity-Observability Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_037_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_037"`

---

## Summary

**Question:** Why does observability require complete reciprocity?

**Verdict:** Observability = **complete state reconstruction**. A complex state (D_036)
carries two real DOFs (magnitude, phase), so complete observation requires measuring
BOTH quadratures. The {cos, sin} pair at ω_k — both eigenfunctions of L at
λ_k = λ_{N−k}, orthogonal, equal norm, spanning the 2D eigenspace — is exactly the
reciprocal-pair (Re/Im) measurement basis: from both projections the state is
reconstructed exactly (z = a + ib); from one alone the phase θ is ambiguous. An
isolated singlet (1D real, sin(πn) = 0) has only one quadrature channel — its phase is
unobservable, its state underdetermined, its cycle position (reversibility) lost.
**Reciprocity is EMERGENT from complex-state observability; complete pairing DERIVED;
the Z2 pairing input BOUNDARY (D_020).**

## Key measured values

| Quantity | Value |
|---|---|
| λ(k=16) vs λ(N−k=80) at N=96 | both 12 (equal — shared frequency) |
| orthogonality Σ cos·sin over N=96 | ≈ 1.7·10⁻¹⁴ (zero) |
| norms Σ cos² = Σ sin² | 48 = N/2 each (equal) |
| reconstruction z = a + ib | exact (verified at k=16, site 5) |
| single channel ambiguity | same a = 1.0 from (|ψ|=2, θ=π/3) and (|ψ|=1, θ=0) |
| mirror pair | e^{iθ_k} = conj(e^{iθ_{N−k}}); cos even, sin odd |
| phase advance per site Δθ | = 2πk/N (k=16 → π/3, k=32 → 2π/3 — the circulation) |
| singlet phase | pinned to π (k=N/2); sin channel ≡ 0 |
| complete pairing | min mult 2 at N=96; 1 at N=64 |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_037_ReciprocalMode` | {cos, sin} eigenfunctions at λ_k=λ_{N−k}, orthogonal, equal norm | ✅ |
| `Y_D_037_IsolatedMode` | singlet real-only (sin≡0), 1D eigenspace, no partner | ✅ |
| `Y_D_037_InterferenceLoss` | real-only → classical addition; complex → interference | ✅ |
| `Y_D_037_StateReconstruction` | z = a + ib exact; a alone ambiguous (θ unobservable) | ✅ |
| `Y_D_037_Observability` | complete observation = two channels; singlet channel zero; reversibility | ✅ |
| `Y_D_037_DependencyTrace` | Difference → complex state → reciprocity → complete pairing → N=96 | ✅ |
| `Y_D_037_Run` | Research report | ✅ |

## Conclusion

**Observability requires complete reciprocity** because observing a complex state
completely requires its reciprocal-pair measurement basis — the {cos, sin} (Re, Im)
quadrature pair. The phase is observable only through the partner channel (θ₁−θ₂);
a singlet's second channel is identically zero, so its phase is unobservable and its
state underdetermined. Reciprocity is **EMERGENT** (information completeness of the
complex state); complete pairing **DERIVED**; the Z2 pairing input **BOUNDARY** (D_020).
No canonical value was changed.
