# ResearchY-A_001 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_001_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 5/5 tests (Duration ~26–82 ms)
**Filter:** `FullyQualifiedName~Y_A_001`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_A_001_CanonicalConstants` | 95 positive modes; multiset [42×2,5,6]; Σm=95, Σ√m=64.08, Σm²=229; span=6.40; octave bands [4,4,87]; occMom=1900.25 | ✅ |
| `Y_A_001_WaveGeometryObservables` | circumference N=96; radius N/2π=15.279; ω_min=0.6216; wavelength 96; phase closure θ_96=2π≡0; resonance groups | ✅ |
| `Y_A_001_Z2Doublets` | λ_k=λ_{96−k} for 47 pairs; self-conjugate k=48 mode λ=12 | ✅ |
| `Y_A_001_ClosureTwoPi` | λ(θ)=λ(θ+2π) periodicity; zero mode = uniform rest state | ✅ |
| `Y_A_001_Run` | Research report (deterministic reproduction of canonical values + wave observables) | ✅ |

## Key Measured Values (invariant, deterministic)

| Quantity | Measured | Canonical | Deviation |
|---|---|---|---|
| positive modes | 95 | 95 | exact |
| multiplicity groups | 44 ([42×2,5,6]) | 44 | exact |
| Σ√m | 64.0825 | 64.08 | 0.004% |
| Σm² | 229 | 229 | exact |
| octave occupancies | [4,4,87] | [4,4,87] | exact |
| occMom | 1900.25 | 1900.25 | exact |
| span ω_max/ω_min | 6.4025 | 6.40 | 0.04% |
| ω_min | 0.6216 | 0.6216 | exact |
| circumference | 96 | N=96 | exact |
| radius N/2π | 15.2789 | — | inside ladder radii 6.0–17.333 |
| Z2 doublet pairs | 47 | λ_k=λ_{96−k} | exact |

## Conclusion

All canonical D96 constants reproduce to the stated precision from the closed-form
eigenvalue formula alone (regression protection against canonical V2.0). The wave-geometry
observables — radius, circumference, wavelength, frequency, phase lattice, and resonance
structure — read directly from the same spectrum without modifying canonical AT.

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_001"
```
