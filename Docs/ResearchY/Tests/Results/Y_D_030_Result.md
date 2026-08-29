# Y_D_030_Result.md — ResearchY-D_030 Octave-Rung Audit

**Test suite:** `AT.Tests/ResearchY/D_ResonanceStructure/Y_D_030_Tests.cs`
**Run:** 2026-08-29
**Result:** ✅ 6/6 PASSED
**Reproduction:** `dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_D_030"`

---

## Summary

**Question:** Why octave rungs? Is N = p·2^k derived or a remaining boundary assumption?

**Verdict:** **The octave-rung structure n = p·2^k is DERIVED.** The family count
floor(log₂ span)+1 is itself an octave (factor-2) partition (D_016), and the
long-wavelength dispersion ω(k) ~ c·k makes the mode-index doubling k→2k a frequency
octave (ω(2)/ω(1) = 1.97 at N=96). Hence q=2 is the natural scale step; n = p·2^k is
the discrete octave ladder. q=2 is the unique pure scale-step base whose rung chain hits
a zero-defect ring (only 96; q=6 hits 108 but mixes the seed, 3·6^k = 3^(k+1)·2^k).
Removing the octave rung leaves 11 zero-defect rings (96 not unique). The octave
structure is DERIVED; the seed period p=3 is BOUNDARY (D_020).

## Alternative Rungs

| base q | chain (3·q^k) | zero-defect rungs in [32,300] |
|---|---|---|
| **q=2** | 48, 96, 192 | **[96]** |
| q=3 | 81, 243 | none |
| q=4 | 48, 192 | none |
| q=5 | 75 | none |
| q=6 | 18, 108, 648 | [108] (mixes seed: 3·6^k = 3^(k+1)·2^k) |

## Key measured values

| Quantity | Value |
|---|---|
| ω(2)/ω(1) at N=96 | 1.97 (approaching 2 — frequency octave) |
| ω(k) long-wavelength | ~ (2π·k·√91)/N (linear in k) |
| families(96) | 3 = floor(log₂ 6.4025)+1 (octave partition) |
| zero-defect rings without rung | 11 (60…120) |
| zero-defect octave rungs | **{96} only** |

## Test results

| Test | Verifies | Result |
|---|---|---|
| `Y_D_030_OctaveNecessity` | octave rung required to select 96 (else 11 rings) | ✅ |
| `Y_D_030_AlternativeRungs` | q=2 unique pure base; q=3/4/5 fail; q=6 mixes seed | ✅ |
| `Y_D_030_DoublingLaw` | ω ~ c·k → ω(2k)/ω(k) ~ 2 (frequency octave) | ✅ |
| `Y_D_030_SelectionRemoval` | removing octave rung → 11 zero-defect rings, 96 not unique | ✅ |
| `Y_D_030_DependencyTrace` | Difference → seed p=3 → octave rung → N=96 | ✅ |
| `Y_D_030_Run` | Research report | ✅ |

## Conclusion

**The octave-rung structure n = p·2^k is DERIVED**, not a remaining boundary assumption.
The family count floor(log₂ span)+1 is itself an octave (factor-2) partition (D_016),
and the long-wavelength dispersion ω(k) ~ c·k makes the mode-index doubling k→2k a
frequency octave (ω(2)/ω(1) = 1.97 at N=96). Hence q=2 is the natural scale step; n =
p·2^k is the discrete octave ladder. q=2 is the unique pure scale-step base whose rung
chain hits a zero-defect ring (only 96). The octave structure is DERIVED (dispersion +
partition); the seed period p=3 is BOUNDARY (D_020). No canonical value was changed.
