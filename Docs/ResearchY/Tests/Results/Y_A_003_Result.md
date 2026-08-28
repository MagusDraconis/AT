# ResearchY-A_003 — Test Result Summary (rev. 2)

**Test suite:** `AT.Tests/ResearchY/A_WaveFoundations/Y_A_003_Tests.cs` (rev. 2)
**Run:** 2026-08-28
**Result:** ✅ PASSED — 7/7 tests (Duration ~24 ms)
**Filter:** `FullyQualifiedName~Y_A_003`

## Tests

| Test | Verifies | Result |
|---|---|---|
| `Y_A_003_PropagationDepth` | μ^k = path multiplicity at generation depth k (MONO_PHASE002) | ✅ |
| `Y_A_003_LocalTransport` | branching is tree-local: ρ_{k+1}=μ·ρ_k first-order, no site coupling, conserved | ✅ |
| `Y_A_003_GlobalTransport` | spectral projection global: |φ_k(n)|²=1/96 on every site; 1-D measure vs 96-D modes | ✅ |
| `Y_A_003_Z2Symmetry` | λ_k=λ_{N−k} (47 pairs), k=48 self-conjugate; branching shares have NO mirror symmetry | ✅ |
| `Y_A_003_OctaveOccupancies` | [4,4,87] from the ω octave spectrum; branching scaled ×95 does NOT match | ✅ |
| `Y_A_003_ResonanceLocking` | λ₂=0.3864 (LOCKING gap); lock chain occMom/Σm=20.0026 exact | ✅ |
| `Y_A_003_Run` | Research report (deterministic) | ✅ |

## Key Findings

| RQ | Verdict |
|---|---|
| RQ1 what propagates | the count share ρ (a unit of Difference) |
| RQ2 carrier | Galton–Watson tree (count) + ring C96 (mode structure) |
| RQ3 local or global | local generation (branching) + global readout (spectral projection) |
| RQ4 μ^k = depth | YES — path multiplicity at generation k |
| RQ5 branching as wave | NO — first-order scalar; phase is a separate DOF (Ch9) |
| RQ6 Z2 pairing | NO — circulant-graph property (47 pairs), not propagation |
| RQ7 octaves [4,4,87] | NO — spectral ω-octave property, not propagation-generated |
| RQ8 resonance locking | NO — spectral gap λ₂=0.3864 / lock chain 20.0026, not propagation |

## Conclusion

**Preferred model:** branching (local count transport, μ^k depth) + spectral projection
(global mode readout). Every structural feature (Z2, octaves, locking) is carried by the
graph medium and read through the propagating count — consistent with the A_004
falsification verdict. **No canonical value was changed.**

## Reproduction

```
dotnet test AT.Tests/AT.Tests.csproj --filter "FullyQualifiedName~Y_A_003"
```
