# Y_D_039_Result.md — ResearchY-D_039 State-Identity-Origin Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_039_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 7/7 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_039"`

---

## Summary

**Question:** Why must an observable state have a unique identity?

**Verdict:** **Difference IS distinguishability.** The primitive "Difference" is the act
of distinguishing one state from another; a state space in which two modes cannot be
told apart fails the primitive itself. **State identity is the primitive applied to the
observable sector — not a separate boundary.** The real-only space collapses the 95
modes to **48 distinct real states** (47 mirror pairs + 1 self-conjugate: mirror pairs
have identical cos, so there is no Difference between them) and further to **3
magnitude buckets**. Phase-only restores 95/95 identity but loses the count content.
The complex space ψ = |ψ|·e^{iθ} **realizes Difference fully**: 95/95 distinct with the
Born rule Σρ=1 EXACT.

## Key measured values

| Quantity | Value |
|---|---|
| real-only distinct states | 48 (47 pairs + 1 self-conjugate) for 95 modes |
| magnitude-only buckets | 3 |
| mirror collapse | cos(2π(N−k)n/N)=cos(2πkn/N) — no Difference between k and N−k |
| complex-space identity | 95/95 distinct |
| phase-only probability | none (uniform |ψ|=1) |
| Born rule over shares | Σρ = 1 EXACT (μ=2, J=3) |
| complete pairing | N=96 = 3·2⁵; 6\|N |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_039_IdentityLoss` | removing identity collapses mode structure (95→48→3) | ✅ |
| `Y_D_039_Distinguishability` | Difference = distinguishability; complex 95/95; real 48 | ✅ |
| `Y_D_039_MagnitudeOnly` | magnitude-only collapses (mirror pairs identical) | ✅ |
| `Y_D_039_PhaseOnly` | phase-only identity but no probability | ✅ |
| `Y_D_039_ObservableState` | complex = full identity + Born rule exact | ✅ |
| `Y_D_039_DependencyTrace` | Difference → distinguishability → identity → complex state | ✅ |
| `Y_D_039_Run` | Research report | ✅ |

## Conclusion

**State identity is DERIVED from Difference itself** — the primitive IS
distinguishability, so a state space that cannot distinguish modes fails the primitive.
The real-only space collapses 95 modes to 48 states (mirror pairs identical); the
complex space realizes Difference fully (95/95 distinct, Born rule exact). The only
boundaries are the primitives **{Difference, η}** (D_027) and the **Z2-paired sector
requirement** (D_020). No canonical value was changed.
