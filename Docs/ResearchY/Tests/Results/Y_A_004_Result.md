# ResearchY-A_004 — Test Result Summary

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_004_Tests.cs`
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~25 ms)
**Filter:** `FullyQualifiedName~Y_A_004`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_A_004_BranchingOccupancies` | branching (μ=1 uniform, μ=2 geometric ×95) does NOT produce [4,4,87] | ✅ |
| `Y_A_004_DiffusionOccupancies` | dominant-mode threshold count = 3 (not [4,4,87]); relaxes to uniform | ✅ |
| `Y_A_004_WaveOccupancies` | octave count of ω_k = [4,4,87] — but ONLY as spectral read (wave frequencies = √λ_k) | ✅ |
| `Y_A_004_HybridOccupancies` | hybrid needs a coupling constant (free parameter) — forbidden | ✅ |
| `Y_A_004_LambdaStructure` | λ_k = 2Σ(1−cos) fixed by graph (N,K); all operators presuppose it; N=64 differs | ✅ |
| `Y_A_004_MomentStructure` | moments Σm=95, Σ√m=64.08, Σm²=229 from the multiset [42×2,5,6] (spectral) | ✅ |
| `Y_A_004_Run` | Falsification report | ✅ |

## Key Results

| Alternative | [4,4,87]? | λ structure? | Moments? | Independent generation? |
|---|---|---|---|---|
| A branching | NO | NO | NO | no (scalar, no bands) |
| B diffusion | NO | NO (presupposes) | NO (presupposes) | no (erases; t free) |
| C wave | YES* | NO (presupposes) | NO (presupposes) | no (*reads ω = spectral projection) |
| D hybrid | NO | NO (presupposes) | NO (presupposes) | no (coupling = free parameter) |

## Verdict

**FALSIFICATION FAILED — the A_003 conclusion survives.**

- **λ structure:** no model generates λ_k — every propagation operator (L, e^{−tL}, √L)
  is a function of the graph Laplacian (the medium).
- **[4,4,87]:** only the eigenfrequency (ω_k) octave read reproduces it — which IS the
  spectral-projection half of the conclusion.
- **Moments:** only the multiplicity multiset [42×2,5,6] reproduces them — spectral.

**Conclusion:** "Actualization = branching + spectral projection" is **UNIQUE within the
accepted D96 structure** (not merely preferred). No canonical value was changed.

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_004"
```
